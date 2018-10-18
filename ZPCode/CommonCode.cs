using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPCode
{
    public class CommonCode
    {
        static string manuId = Lib.ReadCookie("ManuID");
        static string custId = Lib.ReadCookie("CustID");
        static string sql = "";//用于后面不用在定义sql 免得错乱

        #region 所有SQL语句备份 如根据custId获取catalogId等
        public static void SQLStringHelper()
        {
            sql = string.Format(@"SELECT b.name FROM tb_customer_{0} a
LEFT JOIN dbo.tb_customer_catalog b ON a.customer_catalog_id=b.tb_customer_catalogID WHERE a.tb_customerID={1}", manuId, custId);


        }
        #endregion

        #region 数据库中所有SQL语句备份 如判断代理商是否审核
        public static void DBSQLStringHelper()
        {
            sql = string.Format(@"SELECT   COUNT(*)
                   FROM     tb_customer_audit_
                   WHERE    to_audit_customer_id = @cust_id");  //数据库用
        }
        #endregion

        #region 获取当前代理商等级,并判断最低拿货量  最低提货 public static string CheckMinTiHuo(string quantity)
        public static string CheckMinPickUp(string quantity)
        {

            string sql = string.Format(@"SELECT customer_catalog_id FROM tb_customer_{0} WHERE tb_customerID={1}", manuId, custId);
            var catalogId = SQL.GetValue(sql);
            sql = string.Format(@"SELECT value FROM tb_setting WHERE manufacturer_id=10609 AND [key]='tihuoyaoqiu' AND setting_catalog_code={1}", manuId, catalogId);
            var targetQuantity = SQL.GetValue(sql);
            if (int.Parse(quantity) < int.Parse(targetQuantity))
            {
                return "不能低于最低提货数量！";
            }
            return "";
        }
        #endregion

        #region 判断首次提货库存是否足够 public bool CheckFristPickUpQuantity(int quantity, string typeKey = "fristnahuo")
        public bool CheckFristPickUpQuantity(int quantity, string typeKey = "fristnahuo")
        {
            var manuId = Lib.ReadCookie("ManuID");
            var custId = Lib.ReadCookie("CustID");
            string sql = string.Format(@"SELECT customer_catalog_id FROM  tb_customer_{0} WHERE tb_customerID={1}", manuId, custId);
            var catalogId = SQL.GetValue(sql);
            sql = string.Format(@"SELECT value FROM tb_setting WHERE manufacturer_id={0} AND [key]='{1}' AND setting_catalog_code='{2}'", manuId, typeKey, catalogId);
            var val = SQL.GetValue(sql);
            sql = string.Format(@"SELECT COUNT(*) FROM tb_order_{0} WHERE cust_id={1} AND order_type=1", manuId, custId);
            var orderCount = SQL.GetValue(sql);
            if (orderCount == "0" && quantity < int.Parse(val))
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
