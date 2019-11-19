using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameProject.Models
{
    public class ChangeMovieName : ChangeBase
    {
        public ChangeMovieName(string path)
            : base(path) { }

        public override string GetNewFileName(string name)
        {
            var oldName = FileHelper.GetFileNameByFullPath(name);
            var oldPath = FileHelper.GetFilePathByFullPath(name);
            string newName = ConvertName(oldName);
            return Path.Combine(TargetPath, newName);
            
        }
        public override string ConvertName(string name)
        {
            //大明王朝1566.EP01.2007.D9.480P.AC3.X264.mkv
            var houZhui = FileHelper.GetHouZhui(name);
            var newName = name.Replace(@".EP", " ");
            var lastIndex = newName.IndexOf(".");
            var firstName = newName.Substring(0, lastIndex);
            return firstName + "." + houZhui;
        }
    }
}
