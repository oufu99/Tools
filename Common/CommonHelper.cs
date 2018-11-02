using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonHelper
    {
        public static void UpdateTempList(List<string> list, string manuId, string path)
        {
            //如果已经存在就不处理,不存在就添加
            if (!list.Contains(manuId))
            {
                if (list.Count() == 5)
                {
                    list.RemoveAt(0);
                    list.Add(manuId);
                }
                else
                {
                    list.Add(manuId);
                }
                UpdateXML(list, path);
            }
            else
            {
                //如果存在要把他移到最后
                list.Remove(manuId);
                list.Add(manuId);
                UpdateXML(list, path);
            }

        }

        public static void UpdateXML(object list, string path)
        {
            var jsonStr = JsonHelper.SerializeObject(list);
            XMLHelper.UpdateNodeInnerText(path, jsonStr);
        }
    }
}
