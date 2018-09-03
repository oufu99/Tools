using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class IISHelp
    {
        /// <summary> 
        /// 创建一个新的站点
        /// </summary> 
        /// <param name="siteName">站点名称</param> 
        /// <param name="bindingInfo">绑定信息（IP地址、端口、域名） eg：string.Format(@"{0}:{1}:{2}", ip, 端口, 域名);</param> 
        /// <param name="physicalPath">站点物理路径</param> 
        public static void CreateSite(string siteName, string bindingInfo, string physicalPath)
        {
            createSite(siteName, "http", bindingInfo, physicalPath, true, siteName, ProcessModelIdentityType.NetworkService, null, null, ManagedPipelineMode.Integrated, null);
        }

        /// <summary>
        /// 创建一个新的站点
        /// </summary>
        /// <param name="siteName">站点名称</param>
        /// <param name="protocol">协议</param>
        /// <param name="bindingInformation">绑定信息（协议、IP地址、端口）</param>
        /// <param name="physicalPath">站点物理路径</param>
        /// <param name="createAppPool">是否创建新的应用程序池</param>
        /// <param name="appPoolName">应用程序池名称</param>
        /// <param name="identityType"></param>
        /// <param name="appPoolUserName"></param>
        /// <param name="appPoolPassword"></param>
        /// <param name="appPoolPipelineMode"></param>
        /// <param name="managedRuntimeVersion"></param>
        private static void createSite(string siteName, string protocol, string bindingInformation, string physicalPath, bool createAppPool, string appPoolName, ProcessModelIdentityType identityType,
                string appPoolUserName, string appPoolPassword, ManagedPipelineMode appPoolPipelineMode, string managedRuntimeVersion)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                //创建站点
                Site site = serverManager.Sites.Add(siteName, protocol, bindingInformation, physicalPath);

                //是否创建新的应用程序池
                if (createAppPool)
                {
                    //创建应用程序池
                    ApplicationPool pool = serverManager.ApplicationPools.Add(appPoolName);
                    if (pool.ProcessModel.IdentityType != identityType)
                    {
                        pool.ProcessModel.IdentityType = identityType;
                    }
                    if (!String.IsNullOrEmpty(appPoolUserName))
                    {
                        pool.ProcessModel.UserName = appPoolUserName;
                        pool.ProcessModel.Password = appPoolPassword;
                    }
                    if (appPoolPipelineMode != pool.ManagedPipelineMode)
                    {
                        pool.ManagedPipelineMode = appPoolPipelineMode;
                    }
                    pool.ManagedRuntimeVersion = "v4.0";
                    site.Applications["/"].ApplicationPoolName = pool.Name;
                }

                serverManager.CommitChanges();
            }
        }


        /// <summary> 
        /// 删除站点
        /// </summary> 
        /// <param name="siteName">站点名称</param> 
        public static void DeleteSite(string siteName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Site site = serverManager.Sites[siteName];
                if (site != null)
                {
                    serverManager.Sites.Remove(site);
                    serverManager.CommitChanges();
                }
            }
        }

        /// <summary> 
        /// 删除应用程序池
        /// </summary> 
        /// <param name="appPoolName">应用程序池名称</param> 
        public static void DeletePool(string appPoolName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                ApplicationPool pool = serverManager.ApplicationPools[appPoolName];
                if (pool != null)
                {
                    serverManager.ApplicationPools.Remove(pool);
                    serverManager.CommitChanges();
                }
            }
        }

        /// <summary>
        /// 检查站点名称是否存在
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static bool IsExistWebSite(string siteName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                if (serverManager.Sites != null && serverManager.Sites.Count > 0)
                {
                    if (serverManager.Sites.FirstOrDefault(p => p.Name.ToUpper() == siteName.ToUpper()) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查应用程序池是否已存在
        /// </summary>
        /// <param name="poolName">应用程序池名称</param>
        /// <returns></returns>
        public static bool IsExistsAppPool(string poolName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                if (serverManager.ApplicationPools != null && serverManager.ApplicationPools.Count > 0)
                {
                    if (serverManager.ApplicationPools.FirstOrDefault(p => p.Name.ToUpper() == poolName.ToUpper()) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 停止站点
        /// </summary>
        /// <param name="siteName"></param>
        public static void StopSite(string siteName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                serverManager.Sites[siteName].Stop();
            }
        }

        /// <summary>
        /// 获取应用程序池和站点的状态
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="webName">站点名称</param>
        /// <returns></returns>
        public static string GetWebState(string serverIP, string webName)
        {
            ObjectState poolState = ObjectState.Unknown;
            ObjectState siteState = ObjectState.Unknown;
            using (ServerManager sm = ServerManager.OpenRemote(serverIP))
            {
                //应用程序池
                ApplicationPool appPool = sm.ApplicationPools.FirstOrDefault(x => x.Name.ToUpper() == webName.ToUpper());
                if (appPool != null)
                {
                    poolState = appPool.State;
                }

                //Site
                Site site = sm.Sites.FirstOrDefault(x => x.Name.ToUpper() == webName.ToUpper());
                if (site != null)
                {
                    siteState = site.State;
                }
            }
            return $"{poolState.ToString()} | {siteState.ToString()}";
        }
    }
}
