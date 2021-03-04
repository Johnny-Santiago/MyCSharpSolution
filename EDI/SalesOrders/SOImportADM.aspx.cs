using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Excel;
using Business;
using Extensions;
using System.IO;
using System.Globalization;
using System.Text;


namespace EDI.SalesOrders
{
    public partial class SOImportADM : System.Web.UI.Page
    {
        private static string sheet = "Sheet1";
        private string[] formats = { "dd/MM/yyyy HH:mm:ss tt", "dd/MM/yyyy HH:mm", "dd/MM/yyyy", "yyyyMMdd", "MM/dd/yyyy HH:mm", "MM/dd/yyyy", "MM/dd/yyyy HH:mm:ss" };
        private static Dictionary<string, string> columns = new Dictionary<string, string>();

        private void InitializeColumns()
        {
            try
            {
                columns.Add("OrderDate", "Order Date");
                columns.Add("DeliveryDate", "Del. Date");
                columns.Add("ShopCode", "Shop Code");
                columns.Add("YourRef", "Job No");
                columns.Add("ManifestNo", "Order No");
                columns.Add("DeliveryOrderNo", "PO Number");
                columns.Add("RIT", "Del. Cycle");
                columns.Add("CustomerPartNo", "Part No");
                columns.Add("CustomerPartName", "Part Name");
                columns.Add("Quantity", "Order(Pcs)");
             }
            catch
            {
            }
        }

        private DataTable CreateTable()
        {
            InitializeColumns();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");

            foreach (KeyValuePair<string, string> entry in columns)
            {
                dt.Columns.Add(entry.Key);
            }

            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FileUpload1.Attributes["onchange"] = "UploadFile(this)";
            }
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                ltlMessage.Text = "No file is selected. Please select Customer EDI or Excel file.".ToErrorMessageFormat();
                return;
            }

            if (!(FileUpload1.FileName.EndsWith(".xlsx")))
            {
                ltlMessage.Text = "The file is not in the right format.".ToErrorMessageFormat();
                return;
            }

            txtFilename.Text = FileUpload1.FileName;

            ValidateAndUploadFile();
        }

        private void ValidateAndUploadFile()
        {
            try
            {
                ViewState["xls"] = FileUpload1.FileBytes;
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(FileUpload1.PostedFile.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet xls = excelReader.AsDataSet();

                ViewState["Valid"] = true;

                DataTable CustomerFile = CreateTable();

                List<string> colNames = new List<string>(columns.Values);
                List<string> colKeys = new List<string>(columns.Keys);

                Int32 ID = 1;
                CustomerFile = xls.Tables[sheet].AsEnumerable().Select(r =>
                {
                    var row = CustomerFile.NewRow();
                    row.ItemArray = new object[] { 
                        ID++,
                        r[colNames[0]].ToString() == string.Empty ? r[colNames[0]] : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[colNames[0]].ToString())),
                        r[colNames[1]].ToString() == string.Empty ? r[colNames[1]] : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(r[colNames[1]].ToString())),
                        r[colNames[2]],
                        r[colNames[3]],	
                        r[colNames[4]],	
                        r[colNames[5]],	
                        r[colNames[6]],	
                        r[colNames[7]],
                        r[colNames[8]],
                        r[colNames[9]]
                    };
                    return row;
                }).CopyToDataTable();
                 
                CustomerFile.AsEnumerable().Where(r => r[columns.FirstOrDefault(x => x.Value == "Part No").Key] == DBNull.Value
                    | r[columns.FirstOrDefault(x => x.Value == "Order(Pcs)").Key] == DBNull.Value).ToList().ForEach(r => r.Delete());
                CustomerFile.AcceptChanges();

                CustomerFile = CustomerFile.AsEnumerable().Where(
                    r => r[colKeys[0]].ToString() != string.Empty |
                    r[colKeys[1]].ToString() != string.Empty |
                    r[colKeys[2]].ToString() != string.Empty |
                    r[colKeys[3]].ToString() != string.Empty |
                    r[colKeys[4]].ToString() != string.Empty |
                    r[colKeys[5]].ToString() != string.Empty |
                    r[colKeys[6]].ToString() != string.Empty |
                    r[colKeys[7]].ToString() != string.Empty |
                    r[colKeys[8]].ToString() != string.Empty |
                    r[colKeys[9]].ToString() != string.Empty
                ).CopyToDataTable();
                CustomerFile.TableName = "xml";

                MemoryStream s = new MemoryStream();
                CustomerFile.WriteXml(s, true);
                s.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(s);
                string xml = sr.ReadToEnd();
                sr.Close();

                DataTable order = SOKanbanFile.Validate_ADM(xml);
               
                order.TableName = "Orders";

                gvSheet.DataSource = order;
                gvSheet.DataBind();

                gvSheet.Visible = !(bool)ViewState["Valid"];

                if ((bool)ViewState["Valid"])
                {
                    SOKanbanFile skf = new SOKanbanFile();

                    skf.Name = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    skf.ContentType = FileUpload1.PostedFile.ContentType;
                    skf.Data = (byte[])ViewState["xls"];

                    order.TableName = "Orders";
                    s = new MemoryStream();
                    order.WriteXml(s, true);
                    s.Seek(0, SeekOrigin.Begin);
                    sr = new StreamReader(s);
                    skf.Xml = sr.ReadToEnd();
                    sr.Close();

                    skf.SysCreator = HttpContext.Current.User.Identity.Name;
                    Int32 refID = skf.Import_ADM(); 

                    ltlMessage.Text = string.Format("<img src='../App_Themes/Default/images/listing/ico-accept.png' /> Successfuly uploaded file for processing to Exact. Our Reference Number is {0}.", refID).ToSuccessMessageFormat();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("does not belong to table"))
                {
                    ltlMessage.Text = "File is invalid. Please choose the customer EDI or Excel file containing Sales Orders per Kanban.".ToErrorMessageFormat();
                }
                else
                {
                    ltlMessage.Text = string.Format("The file you are uploading is invalid. {0}.", ex.Message.ToString()).ToErrorMessageFormat();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                SOKanbanFile skf = new SOKanbanFile(13);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = skf.ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + skf.Name + "\"");
                Response.BinaryWrite(skf.Data);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    String ID = gvSheet.DataKeys[e.Row.RowIndex].Values["ID"].ToString();

                    StringBuilder sb = new StringBuilder();
                    String OrderDate = gvSheet.DataKeys[e.Row.RowIndex].Values["OrderDate"].ToString();
                    if (OrderDate.Equals(String.Empty))
                    {
                        sb.Append("- Order Date should not be empty.<br>");
                    }
                    else
                    {
                        try
                        {
                            Convert.ToDateTime(OrderDate);
                        }
                        catch
                        {
                            OrderDate = string.Empty;
                            sb.Append("- Order Date is invalid.<br>");
                        }
                    }

                    String DeliveryDate = gvSheet.DataKeys[e.Row.RowIndex].Values["DeliveryDate"].ToString();
                    if (OrderDate.Equals(String.Empty))
                    {
                        sb.Append("- Del. Date should not be empty.<br>");
                    }
                    else
                    {
                        try
                        {
                            Convert.ToDateTime(DeliveryDate);
                        }
                        catch
                        {
                            DeliveryDate = string.Empty;
                            sb.Append("- Del. Date is invalid.<br>");
                        }
                    }

                    string ShopCode = gvSheet.DataKeys[e.Row.RowIndex].Values["ShopCode"].ToString();
                    string OrderedBy = gvSheet.DataKeys[e.Row.RowIndex].Values["OrderedBy"].ToString();
                    string OrderedByX = gvSheet.DataKeys[e.Row.RowIndex].Values["OrderedByX"].ToString();
                    string DeliverTo = gvSheet.DataKeys[e.Row.RowIndex].Values["DeliverTo"].ToString();
                    string DeliverToX = gvSheet.DataKeys[e.Row.RowIndex].Values["DeliverToX"].ToString();
                    string YourRef = gvSheet.DataKeys[e.Row.RowIndex].Values["YourRef"].ToString();
                    string YourRefX = gvSheet.DataKeys[e.Row.RowIndex].Values["YourRefX"].ToString();
                    string DeliveryOrderNo = gvSheet.DataKeys[e.Row.RowIndex].Values["DeliveryOrderNo"].ToString();
                    string CustomerPartNo = gvSheet.DataKeys[e.Row.RowIndex].Values["CustomerPartNo"].ToString();
                    string IchikohPartNo = gvSheet.DataKeys[e.Row.RowIndex].Values["IchikohPartNo"].ToString();
                    decimal Quantity = Convert.ToDecimal(gvSheet.DataKeys[e.Row.RowIndex].Values["Quantity"]);
                    decimal SalesPrice = Convert.ToDecimal(gvSheet.DataKeys[e.Row.RowIndex].Values["SalesPrice"]);

                    if (!(OrderDate.Equals(String.Empty) && OrderDate.Equals(String.Empty)))
                    {
                        if (Convert.ToDateTime(DeliveryDate) < Convert.ToDateTime(OrderDate))
                        {
                            sb.Append("- Del. Date should not be earlier than Order Date.<br>");
                        }
                    }

                    if (OrderedByX.Equals(String.Empty))
                    {
                        sb.Append(string.Format("- Shop Code {0} is unknown.<br>", ShopCode));
                    }

                    if (DeliverToX.Equals(String.Empty))
                    {
                        sb.Append(string.Format("- Shop Code {0} is unknown.<br>", ShopCode));
                    }

                    if (!YourRefX.Equals(String.Empty))
                    {
                        sb.Append(string.Format("- Order No {0} already exists.<br>", DeliveryOrderNo));
                    }

                    if (YourRef.Equals(String.Empty))
                    {
                        sb.Append("- Job No should not be empty.<br>");
                    }

                    if (DeliveryOrderNo.Equals(String.Empty))
                    {
                        sb.Append("- Order No should not be empty.<br>");
                    }

                    if (IchikohPartNo.Equals(String.Empty))
                    {
                        sb.Append(string.Format(" - Part No {0} does not exist.<br>", CustomerPartNo));
                    }
                    else if (IchikohPartNo.Equals("DUPLICATE"))
                    {
                        sb.Append(string.Format(" - Part No {0} appears more than once in Customer Items Master.<br>", CustomerPartNo));
                    }

                    if (Quantity <= 0)
                    {
                        sb.Append("- Order(Pcs) must be greater than zero.<br>");
                    }

                    if (SalesPrice <= 0)
                    {
                        sb.Append("No Sales Price is set in price agreement and/or standard sales price.<br>");
                    }

                    if (!sb.ToString().Equals(string.Empty))
                    {
                        ViewState["Valid"] = false;

                        foreach (TableCell item in e.Row.Cells)
                        {
                            item.ForeColor = System.Drawing.Color.Red;
                        }

                        this.ltlMessage.Text += (string.Format("Error on Line {0}:<br>", ID) + sb.ToString()).ToErrorMessageFormat();
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }
    }
}