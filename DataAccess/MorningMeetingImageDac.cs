using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class MorningMeetingImageDac : Configuration
    {
        public static int Insert(IMorningMeetingImage MorningMeetingImage)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Department", MorningMeetingImage.Department),
				new SqlParameter("@Caption", MorningMeetingImage.Caption),
                new SqlParameter("@ImageName", MorningMeetingImage.ImageName),
				new SqlParameter("@Photo", MorningMeetingImage.Photo),
				new SqlParameter("@Status", MorningMeetingImage.Status),
				new SqlParameter("@SysCreator", MorningMeetingImage.SysCreator)
			};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Configuration.ConnectionString, "sp_MorningMeetingImages_InsertImage", _parameters));
        }
        public static void Update(IMorningMeetingImage MorningMeetingImage)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", MorningMeetingImage.Id),
				new SqlParameter("@Department", MorningMeetingImage.Department),
				new SqlParameter("@Caption", MorningMeetingImage.Caption),
                new SqlParameter("@ImageName", MorningMeetingImage.ImageName),
				new SqlParameter("@Photo", MorningMeetingImage.Photo),
				new SqlParameter("@Status", MorningMeetingImage.Status),
				new SqlParameter("@SysModifier", MorningMeetingImage.SysModifier)
			};
            SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, "sp_MorningMeetingImages_UpdateById", _parameters);
        }
        public static void ChangeStatus(IMorningMeetingImage MorningMeetingImage)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", MorningMeetingImage.Id),
				new SqlParameter("@Status", MorningMeetingImage.Status),
				new SqlParameter("@SysModifier", MorningMeetingImage.SysModifier)
			};
            SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, "sp_MorningMeetingImages_ChangeStatusById", _parameters);
        }
        public static void Delete(int Id)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", Id)
			};
            SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, "sp_MorningMeetingImages_DeleteById", _parameters);
        }
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_MorningMeetingImages_SelectByDepartment", new object[0]);
        }
        public static DataSet Retrieve(IMorningMeetingImage MorningMeetingImage)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Department", MorningMeetingImage.Department),
				new SqlParameter("@Caption", MorningMeetingImage.Caption),
				new SqlParameter("@Status", MorningMeetingImage.Status),
				new SqlParameter("@SysCreator", MorningMeetingImage.SysCreator),
				new SqlParameter("@SysCreatedFrom", MorningMeetingImage.SysCreated),
				new SqlParameter("@SysCreatedTo", MorningMeetingImage.SysCreated2),
				new SqlParameter("@SysModifier", MorningMeetingImage.SysModifier),
				new SqlParameter("@SysModifiedFrom", MorningMeetingImage.SysModified),
				new SqlParameter("@SysModifiedTo", MorningMeetingImage.SysModified2)
			};
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_MorningMeetingImages_SearchMorningMeetingImages", _parameters);
        }
        public static DataSet Retrieve(int Id)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", Id)
			};
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_MorningMeetingImages_SelectById", _parameters);
        }
    }
}