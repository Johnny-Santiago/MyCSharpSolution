using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interface;
using System.Data;
using Extensions;
using DataAccess;

namespace Business
{
    public class SQLJob : ISQLJob
    {
        private DataTable _Info;

        private String _JobID;
        private String _JobName;
        private Nullable<DateTime> _RunDate;
        private Nullable<Int32> _RunTime;
        private Nullable<Int32> _JobHistoryID;
        private Nullable<Int32> _RunStatus;
        private Nullable<Int32> _RunDuration;
        private String _Status;
        private String _StatusMessage;
        private String _Duration;

        public SQLJob()
        {
        }

        public SQLJob(string jobName)
        {
            _Info = Retrieve(jobName);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new Exception("An error is encountered during initialization of stock taking.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _JobID = row["job_id"] == DBNull.Value ? string.Empty : Convert.ToString(row["job_id"]);
            _JobHistoryID = row["instance_id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["instance_id"]);
            //_JobName = row["Period"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Period"]);
            _RunDate = row["run_requested_date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["run_requested_date"]);
            _RunDuration = row["run_duration"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["run_duration"]);
            _RunStatus = row["run_status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["run_status"]);
            _RunTime = row["run_time"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["run_time"]);
            _StatusMessage = row["message"] == DBNull.Value ? string.Empty : Convert.ToString(row["message"]);

        }

        public DataTable Retrieve(string jobName)
        {
            return SQLJobDac.Retrieve(jobName).Tables[0];
        }

        public void RunJob(string jobName)
        {
            SQLJobDac.Execute(jobName);
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public string JobID
        {
            get
            {
                return _JobID;
            }
            set
            {
                _JobID = value;
            }
        }

        public string JobName
        {
            get
            {
                return _JobName;
            }
            set
            {
                _JobName = value;
            }
        }

        public Nullable<DateTime> RunDate
        {
            get
            {
                return _RunDate;
            }
            set
            {
                _RunDate = value;
            }
        }

        public Nullable<Int32> RunTime
        {
            get
            {
                return _RunTime;
            }
            set
            {
                _RunTime = value;
            }
        }

        public Nullable<Int32> JobHistoryID
        {
            get
            {
                return _JobHistoryID;
            }
            set
            {
                _JobHistoryID = value;
            }
        }

        public Nullable<Int32> RunStatus
        {
            get
            {
                return _RunStatus;
            }
            set
            {
                _RunStatus = value;
            }
        }

        public Nullable<Int32> RunDuration
        {
            get
            {
                return _RunDuration;
            }
            set
            {
                _RunDuration = value;
            }
        }

        public string Status
        {
            get
            {
                switch (RunStatus)
                {
                    case 1: 
                        _Status = "Successfully Generated";
                        break;
                    case 0: 
                        _Status = "Failed";
                        break;
                    case 4:
                        _Status = "Cancelled";
                        break;
                    case 3:
                        _Status = "Manual Stopped";
                        break;
                }
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public string StatusMessage
        {
            get
            {
                return _StatusMessage;
            }
            set
            {
                _StatusMessage = value;
            }
        }

        public string Duration
        {
            get
            {
                _Duration = Convert.ToString(RunDuration);
                //_Duration = _Duration.Substring(0, 2) + ":" + _Duration.Substring(2, 2);
                switch (RunDuration.ToString().Length)
                {
                    case 1:
                        _Duration = _Duration + " Seconds";
                        break;
                    case 2:
                        _Duration = _Duration + " Seconds";

                        break;
                    case 3:
                        _Duration = _Duration.Substring(0, 1) + ":" + _Duration.Substring(1, 2) + " Minutes";

                        break;
                    case 4:
                        _Duration = _Duration.Substring(0, 2) + ":" + _Duration.Substring(2, 2) + " Minutes";

                        break;

                    case 5:
                        _Duration = _Duration.Substring(0, 1) + ":" +
                            _Duration.Substring(2, 2) + ":" +
                            _Duration.Substring(3, 2) + " Hours";

                        break;

                    case 6:
                        _Duration = _Duration.Substring(0, 2) + ":" +
                            _Duration.Substring(2, 2) + ":" +
                            _Duration.Substring(3, 2) + " Hours";

                        break;

                    //default:
                    //    _Duration = _Duration;
                    //    break;
                }

                return _Duration;
            }
            set
            {
                _Duration = value;
            }
        }
    }
}