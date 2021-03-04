using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAccess;
using Extensions;
using Interface;

namespace Business
{
    public class MorningMeetingImage : IMorningMeetingImage
    {
        private Nullable<Int32> _Id;
        private string _Department;
        private string _Caption;
        private string _ImageName;
        private byte[] _Photo;
        private Nullable<bool> _Status; 
        private string _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _SysCreated2;
        private string _SysModifier;
        private Nullable<DateTime> _SysModified;
        private Nullable<DateTime> _SysModified2;
        private DataTable _Info;

        public Nullable<Int32> Id 
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        public string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }

        public byte[] Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }

        public Nullable<bool> Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }

        public Nullable<DateTime> SysCreated
        {
            get { return _SysCreated; }
            set { _SysCreated = value; }
        }

        public Nullable<DateTime> SysCreated2
        {
            get { return _SysCreated2; }
            set { _SysCreated2 = value; }
        }

        public string SysModifier
        {
            get { return _SysModifier; }
            set { _SysModifier = value; }
        }

        public Nullable<DateTime> SysModified
        {
            get { return _SysModified; }
            set { _SysModified = value; }
        }

        public Nullable<DateTime> SysModified2
        {
            get { return _SysModified2; }
            set { _SysModified2 = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public MorningMeetingImage()
        {
        }

        public MorningMeetingImage(int Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
                return;
            }

            throw new KeyNotFoundException("Id not found.");
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = ((row["ID"] == DBNull.Value) ? (Nullable<Int32>)null : (Convert.ToInt32(row["ID"])));
            _Caption = ((row["Caption"] == DBNull.Value) ? string.Empty : Convert.ToString(row["Caption"]));
            _ImageName = ((row["ImageName"] == DBNull.Value) ? string.Empty : Convert.ToString(row["ImageName"]));
            _Department = ((row["Department"] == DBNull.Value) ? string.Empty : Convert.ToString(row["Department"]));
            _Photo = ((row["Photo"] == DBNull.Value) ? null : ((byte[])row["Photo"]));
            _Status = ((row["Status"] == DBNull.Value) ? (Nullable<bool>)null : (Convert.ToBoolean(row["Status"])));
            _SysCreator = ((row["SysCreator"] == DBNull.Value) ? string.Empty : Convert.ToString(row["SysCreator"]));
            _SysCreated = ((row["SysCreated"] == DBNull.Value) ? (Nullable<DateTime>)null : (Convert.ToDateTime(row["SysCreated"])));
            _SysModifier = ((row["SysModifier"] == DBNull.Value) ? string.Empty : Convert.ToString(row["SysModifier"]));
            _SysModified = ((row["SysModified"] == DBNull.Value) ? (Nullable<DateTime>)null : (Convert.ToDateTime(row["SysModified"])));
        }

        public int Insert()
        {
            return MorningMeetingImageDac.Insert(this);
        }

        public void Update()
        {
            MorningMeetingImageDac.Update(this);
        }

        public void ChangeStatus()
        {
            MorningMeetingImageDac.ChangeStatus(this);
        }

        public void Delete(int Id)
        {
            MorningMeetingImageDac.Delete(Id);
        }
        public DataTable RetrieveAll()
        {
            return MorningMeetingImageDac.Retrieve().Tables[0];
        }
        public DataTable Retrieve()
        {
            return MorningMeetingImageDac.Retrieve(this).Tables[0];
        }
        public DataTable Retrieve(int Id)
        {
            return MorningMeetingImageDac.Retrieve(Id).Tables[0];
        }
    }
}