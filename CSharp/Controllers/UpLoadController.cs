using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
   public class UpLoadController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UpLoadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }        
        [HttpPost]
        public string Post([FromForm] IFormCollection formCollection)
        {
            string result = "";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            foreach (IFormFile file in filelist)
            {
                String Tpath = "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                string name = file.FileName;
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string FilePath = webRootPath + Tpath;

                string type = System.IO.Path.GetExtension(name);
                DirectoryInfo di = new DirectoryInfo(FilePath);


                if (!di.Exists) { di.Create(); }

                using (FileStream fs = System.IO.File.Create(FilePath + FileName + type))
                {
                    // 复制文件
                    file.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
                result = "1";
            }
            return result;
        }
    }
}