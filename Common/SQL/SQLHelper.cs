using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SQLHelper
    {
        private static string Conn = XmlHelper.ReadText(XMLPath.SQLConnection);


        public static string GetValue(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                CommandDefinition cmd = new CommandDefinition(sql, new
                {

                }, null, 30, CommandType.Text);
                return conn.ExecuteScalar<string>(cmd);

            }
        }

        public static T Query<T>(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                CommandDefinition cmd = new CommandDefinition(sql, new
                {
                    
                }, null, 30, CommandType.Text);
                return conn.Query<T>(cmd).SingleOrDefault();

            }
        }

        public static IList<T> QueryList<T>(string sql, string manuId)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                CommandDefinition cmd = new CommandDefinition(sql, new
                {
                    manuId = manuId
                }, null, 30, CommandType.Text);
                return conn.Query<T>(cmd).ToList();

            }
        }
    }
}
