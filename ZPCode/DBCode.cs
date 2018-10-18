using SERP3.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ZPCode
{
    public class DBCode
    {
        static string manuId = Lib.ReadCookie("ManuID");
        static string custId = Lib.ReadCookie("CustID");
        static string sql = "";//用于后面不用在定义sql 免得错乱

        #region 获取自定义权限 GetCustomPermissionItem
        /// <summary>
        /// 获取自定义权限
        /// </summary>
        /// <param name="manuId"></param>
        /// <param name="catalogCode">分类码</param>
        /// <returns></returns>
        public static bool GetCustomPermissionItem(string manuId, string catalogCode)
        {
            int roleId = GetManuRoleId(manuId);
            var sql = string.Format(@"SELECT value  FROM role_permission_extension 
                                     WHERE roleId={0} AND [key]='{1}'", roleId, catalogCode);
            var dt = SQL.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0
                   && dt.Rows[0].Field<byte>("value") == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取厂商的角色id
        /// </summary>
        /// <param name="manuId"></param>
        /// <returns></returns>
        public static int GetManuRoleId(string manuId)
        {
            string sql = "SELECT tb_roleID FROM dbo.tb_role WHERE manufacturer_id=" + manuId + " AND code='-10'";
            var obj = SQL.GetSingle(sql) ?? "";
            int val;
            int.TryParse(obj.ToString(), out val);

            return val;
        }
        #endregion

        #region 执行审核后存储过程 ProcessManuMappingProcedure
        internal static void ProcessManuMappingProcedure(string type, object[] array)
        {
            var manuId = Lib.ReadCookie("ManuId");
            var procedureSql = string.Format("SELECT procedure_name FROM dbo.tb_manu_mapping_procedure  WHERE type='{0}' AND manu_id={1}  order  by sort ", type, manuId);
            var dt = SQL.GetDataTable(procedureSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                var emDt = dt.AsEnumerable();
                var para = array ?? new object[] { };
                foreach (var emDr in emDt)
                {
                    var procedureName = emDr.Field<string>("procedure_name");
                    var proSQL = ExecPro.Run(procedureName, para, true);
                    var msg = proSQL.Err;
                    if (!string.IsNullOrWhiteSpace(msg) && msg != "1")
                    {
                        Log4Helper.ErrorInfo("ProcessManuMappingProcedureLogger", string.Format("manuId:{0},type:{1},param:{2},msg:{3}", manuId, type, Newtonsoft.Json.JsonConvert.SerializeObject(para), msg));
                    }

                }
            }

        } 
        #endregion
    }
}