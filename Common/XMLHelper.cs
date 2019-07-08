﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Common
{
    public class XMLHelper
    {
        //xml格式前面没有勾看着不爽...改成config后缀一样能被读取只要格式一样就可以了
        private static string filePath = @"D:\Tools\Common\config.config";



        /// <summary>
        /// 根据传入路径读取出XML的值  我加的
        /// </summary>
        /// <param name="xPath">遵循xPath规则可以一路查下去  范例: @"Skill/First/SkillItem"</param>
        /// <returns></returns>
        public static string GetNodeText(string xPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                return xn.FirstChild.InnerText; //得到该节点的子节点

            }
            catch
            {
                return null;
            }
        }

        public static void UpdateXMLList(List<string> list, string path, string listItem = "")
        {
            //如果为空,就说明我是想直接更新list而已
            if (listItem != "")
            {
                //如果已经存在就不处理,不存在就添加
                if (!list.Contains(listItem))
                {
                    if (list.Count == 5)
                    {
                        list.RemoveAt(0);
                        list.Insert(0, listItem);
                    }
                    else
                    {
                        list.Insert(0, listItem);
                    }
                }
                else
                {
                    //如果存在要把他提到最前,按钮也会生成到第一个  可能会造成有些是 .last的报错,直接改成frist就可以了
                    list.Remove(listItem);
                    list.Insert(0, listItem);
                }
            }
            // List 转成json以后会自带一个, 
            var jsonStr = JsonHelper.SerializeObject(list);
            UpdateNodeInnerText(path, jsonStr);
        }

        /// <summary>
        /// 修改节点的InnerText的值
        /// </summary>
        /// <param name="filePath">XML文件绝对路径</param>
        /// <param name="xPath">范例: @"Skill/First/SkillItem"</param>
        /// <param name="value">节点的值</param>
        /// <returns></returns>
        public static bool UpdateNodeInnerText(string xPath, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                XmlElement xe = (XmlElement)xn;
                xe.InnerText = value;
                doc.Save(filePath);
            }
            catch
            {
                return false;
            }
            return true;
        }




        public static string ReadText(string xPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                return xn.FirstChild.InnerText; //得到该节点的子节点

            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 如果要自己写又出现了解耦不完全的境界,脱裤子放屁
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetPathTest(string name)
        {
            var t = typeof(XMLPath);
            var ass = Activator.CreateInstance(t);
            var prop = t.GetField(name);
            var xmlpath = prop.GetValue(ass).ToString();
            return XMLHelper.GetNodeText(xmlpath);
        }

        /// <summary>
        /// 读取XML的所有子节点
        /// </summary>
        /// <param name="xPath">遵循xPath规则可以一路查下去  范例: @"Skill/First/SkillItem"</param>
        /// <returns></returns>
        public static XmlNodeList ReadNodes(string xPath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                XmlNodeList xnList = xn.ChildNodes; //得到该节点的子节点
                return xnList;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 追加节点
        /// </summary>
        /// <param name="filePath">XML文档绝对路径</param>
        /// <param name="xPath">范例: @"Skill/First/SkillItem"</param>
        /// <param name="xmlNode">XmlNode节点</param>
        /// <returns></returns>
        public static bool AppendChild(string xPath, XmlNode xmlNode)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);
                XmlNode n = doc.ImportNode(xmlNode, true);
                xn.AppendChild(n);
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }




        /// <summary>
        /// 读取XML文档
        /// </summary>
        /// <param name="filePath">XML文件绝对路径</param>
        /// <returns></returns>
        public static XmlDocument LoadXmlDoc(string filePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                return doc;
            }
            catch
            {
                return null;
            }
        }




    }
}