using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Factory
    {
        InterTest TestClass { get; set; }

        /// <summary>
        ///在这里初始化   可以去配置文件中读取配置来用IOC注入
        /// </summary>
        public Factory()
        {
            this.TestClass = new InterTest();
        }

        public void SetName(string Name)
        {
            TestClass.Name = Name;
        }

        public void SayHi()
        {
            TestClass.SayHi();
        }
    }
}
