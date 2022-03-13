using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "MVCDemoDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                
                return con.Query<T>(sql).ToList();
            }
        }
        public static List<T> LoadDataWithParam<T>(string sql, object p1)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {

                return con.Query<T>(sql, new { param = p1 }).ToList();
            }
        }

        public static List<T> LoadRoomsData<T>(string sql, object p1, object p2)
           // public static List<T> LoadRoomsData<T>(string sql, DateTime? DateFrom, DateTime? DateTo)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.Query<T>(sql,new { BookingFromDate = p1, BookingToDate =p2 }).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.Execute(sql,data);
            }
        }

        public static int DeleteData<T>(string sql, T data)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                return con.Execute(sql, data);
            }
        }

        //internal static List<T> LoadRoomsData<T>(string sql, object p1, object p2)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
