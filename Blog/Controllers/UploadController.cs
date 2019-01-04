using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment evn;
        public UploadController(IHostingEnvironment evn)
        {
            this.evn = evn;

        }


        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> CKImage()
        {


            string callback = Request.Query["CKEditorFuncNum"];
            var upimage = Request.Form.Files[0];
            string tpl = "<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");</script>";
            if (upimage == null)
                return Json(new { uploaded = false, url = "" });
            var data = Request.Form.Files["upload"];
            string filePath = evn.WebRootPath + @"\UpLoad\Images";
            string imgname = DateTime.Now.ToString("yyyyMMdd") + "\\" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(upimage.FileName);
            string path = Path.Combine(filePath, imgname);
            string strDir = Path.GetDirectoryName(path);
            try
            {

                if (!Directory.Exists(strDir))
                    Directory.CreateDirectory(strDir);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            data.CopyTo(fs);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { uploaded = false, url = "" });
            }

            return Json(new { uploaded = true, url = @"\UpLoad\Images\" + imgname });
        }



        public IActionResult File()
        {

            return null;
        }


    }
}