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

namespace EDI.Forecast
{
    public partial class ForecastImport : System.Web.UI.Page
    {
        private static string sheet = "ForecastTemplate";
        private static string[] formats = { "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm", "dd/MM/yyyy", "MM/dd/yyyy", "dd/MM/yyyy HH:mm:ss", "MM/dd/yyyy HH:mm:ss" };
        private static Dictionary<string, string> columns = new Dictionary<string, string>();

        private static int PeriodCoverage = SIOP.GetPeriodCoverage();
        private static List<string> Periods = new List<string>();
        private static string PeriodN = SIOP.GetPeriod(0);

        private void InitializeColumns()
        {
            try
            {
                columns.Add("Customer Code", "Customer Code");
                columns.Add("Customer Name", "Customer Name");
                columns.Add("Ichikoh Part No", "Ichikoh Part No");
                columns.Add("Customer Part No", "Customer Part No");
                columns.Add("Description", "Description");
                columns.Add("Unit", "Unit");

                for (int i = 0; i < PeriodCoverage; i++)
                {
                    string period = SIOP.GetPeriod(i);
                    Periods.Add(period);
                    columns.Add(period, period);
                }
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
                ltlMessage.Text = "No file is selected. Please choose the customer forecast template or Excel file containing customer forecast orders.".ToErrorMessageFormat();
                return;
            }

            if (!(FileUpload1.FileName.EndsWith(".xlsx")))
            {
                ltlMessage.Text = "The file is not in the right format.".ToErrorMessageFormat();
                return;
            }

            if (!(FileUpload1.FileName.EndsWith(".xlsx")))
            {
                ltlMessage.Text = "The file is not in the right format.".ToErrorMessageFormat();
                return;
            }

            string period = FileUpload1.FileName.Substring(0, 6).Insert(4,"/");
            if (!PeriodN.Equals(period))
            {
                ltlMessage.Text = string.Format("The file is not in the right period. The file period should be {0}.", PeriodN).ToErrorMessageFormat();
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
                        r[colNames[0]],
                        r[colNames[1]],
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

                CustomerFile.AsEnumerable().Where(r => r[columns.FirstOrDefault(x => x.Value == "Customer Code").Key] == DBNull.Value
                    | r[columns.FirstOrDefault(x => x.Value == "Ichikoh Part No").Key] == DBNull.Value).ToList().ForEach(r => r.Delete());
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

                DataTable order = QuotationFile.Validate(xml);

                colNames.Add("ID");
                colNames.Add("Customer CodeX");
                colNames.Add("Ichikoh Part NoX");

                gvSheet.DataKeyNames = colNames.ToArray(); 

                gvSheet.DataSource = order;
                gvSheet.DataBind();

                gvSheet.Visible =  !(bool)ViewState["Valid"];

                if ((bool)ViewState["Valid"])
                {
                    QuotationFile qf = new QuotationFile();

                    qf.Name = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    qf.ContentType = FileUpload1.PostedFile.ContentType;
                    qf.Data = (byte[])ViewState["xls"];

                    order.TableName = "xml";
                    s = new MemoryStream();
                    order.WriteXml(s, true);
                    s.Seek(0, SeekOrigin.Begin);
                    sr = new StreamReader(s);
                    qf.Xml = sr.ReadToEnd();
                    sr.Close();

                    qf.SysCreator = HttpContext.Current.User.Identity.Name;
                    Int32 refID = qf.Import();

                    ltlMessage.Text = "<img src='../App_Themes/Default/images/listing/ico-accept.png' /> Successfuly uploaded file.".ToSuccessMessageFormat();

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("does not belong to table"))
                {
                    ltlMessage.Text = "File is invalid. Please choose the customer forecast template or Excel file containing customer forecast orders.".ToErrorMessageFormat();
                }
                else
                {
                    ltlMessage.Text = string.Format("The file you are uploading is invalid. {0}.", ex.Message.ToString()).ToErrorMessageFormat();
                }
            }
        }

        protected void gvSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells.Cast<DataControlFieldCell>()
                    .Where(c => c.ContainingField.HeaderText == "Customer CodeX"
                             || c.ContainingField.HeaderText == "Ichikoh Part NoX")
                    .ToList()
                    .ForEach(c => c.ContainingField.Visible = false);

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell item in e.Row.Cells)
                    {
                        item.CssClass = "NoWrappedText";
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        TableCell item = e.Row.Cells[i]; 
                        item.CssClass = "NoWrappedText";

                        if (i >= e.Row.Cells.Count - PeriodCoverage - 2)
                        {
                            item.Attributes["style"] = "text-align: right;";
                        }
                    }

                    StringBuilder sb = new StringBuilder();
                    String ID = gvSheet.DataKeys[e.Row.RowIndex].Values["ID"].ToString();
                    string CustomerCode = gvSheet.DataKeys[e.Row.RowIndex].Values["Customer Code"].ToString().Trim();
                    string CustomerCodeX = gvSheet.DataKeys[e.Row.RowIndex].Values["Customer CodeX"].ToString().Trim();
                    string IchikohPartNo = gvSheet.DataKeys[e.Row.RowIndex].Values["Ichikoh Part No"].ToString();
                    string IchikohPartNoX = gvSheet.DataKeys[e.Row.RowIndex].Values["Ichikoh Part NoX"].ToString();

                    if (CustomerCodeX.Equals(String.Empty))
                    {
                        sb.Append(string.Format("Error on Line {0}: Customer Code {1} does not exist.<br>", ID, CustomerCode));
                    }

                    if (IchikohPartNoX.Equals(String.Empty))
                    {
                        sb.Append(string.Format("Error on Line {0}: Ichikoh Part No. {1} does not exist.<br>", ID, IchikohPartNo));
                    }

                    for (int i = 0; i < PeriodCoverage; i++)
                    {
                        string quantity = gvSheet.DataKeys[e.Row.RowIndex].Values[Periods[i]].ToString();

                        if (!quantity.Equals(string.Empty))
                        {
                            try
                            {
                                int number; 
                                bool canConvert = int.TryParse(quantity, out number); 

                                if (!canConvert || number <= 0)
                                {
                                    throw new InvalidCastException();
                                }
                            }
                            catch
                            {
                                sb.Append(string.Format("Error on Line {0}: Quantity {1} for {2} must be a positive whole number.<br>", ID, quantity, Periods[i]));
                            }
                        }
                    }

                    if (!sb.ToString().Equals(string.Empty))
                    {
                        ViewState["Valid"] = false;

                        foreach (TableCell item in e.Row.Cells)
                        {
                            item.ForeColor = System.Drawing.Color.Red;
                        }

                        this.ltlMessage.Text += sb.ToString().ToErrorMessageFormat();
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