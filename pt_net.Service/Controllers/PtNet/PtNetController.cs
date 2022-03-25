using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using pt_net.Manager.PtNet;
using pt_net.Entity.EntityModels;
using System.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pt_net.Service.Controllers.PtNet
{
    [Route("api/[controller]")]
    [ApiController]
    public class PtNetController : ControllerBase
    {
        private readonly IPtNetService ptNetManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PtNetController(IWebHostEnvironment hostEnvironment, IPtNetService _ptNetManager)
        {
            this._hostEnvironment = hostEnvironment;
            ptNetManager = _ptNetManager;

        }

        //PtNetManager ptNetManager = new PtNetManager();

        /*
         * http://localhost:54889/api/pt/generate
        */
        [Route("/api/pt/generate")]
        [HttpGet]
        public PtNetModel ptString(int fileSize)
        {
            PtNetModel _model = new PtNetModel();
            if (fileSize > 0)
            {
                fileSize *= 1024;

                string folder = "File/Generated";
                string fileName = "PT.txt";

                try
                {
                    if (Directory.Exists(folder))
                    {
                        Directory.Delete(folder, true);
                    }
                    Directory.CreateDirectory(folder);

                    var logPath = Path.Combine(_hostEnvironment.ContentRootPath, folder, fileName);
                    if (!System.IO.File.Exists(logPath))
                    {
                        using (var stream = System.IO.File.Create(logPath)) { }
                    }

                    int fileLength = Convert.ToInt32(new System.IO.FileInfo(logPath).Length);

                    while (fileLength <= fileSize)
                    {
                        _model = GenerateRandoms();
                        string text = _model.floatNumber + "," + _model.numeric + "," + _model.alphanumeric  ;
                        using (var writer = System.IO.File.AppendText(logPath))
                        {
                            writer.WriteLine(text);
                        }
                        fileLength = _model.fileSize;

                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return _model;

        }

        //private void CreateDirectoryFile(int fileSize)
        //{
        //    string folder = "File/Generated";
        //    string fileName = "PT.txt";
        //    string fullPath = folder + fileName;            

        //    try
        //    {
        //        if (!Directory.Exists(folder))
        //        {
        //            Directory.CreateDirectory(folder);
        //        }

        //        var logPath = Path.Combine(_hostEnvironment.ContentRootPath, folder, fileName);
        //        if (!System.IO.File.Exists(logPath))
        //        {
        //            using (var stream = System.IO.File.Create(logPath)) { }
        //        }

                
        //        int fileLength = Convert.ToInt32(new System.IO.FileInfo(logPath).Length);

        //        while (fileLength <= fileSize)
        //        {
        //            PtNetModel _model = GenerateRandoms();
        //            string text = _model.numeric + "," + _model.alphanumeric + "," + _model.floatNumber + "\n";
        //            using (var writer = System.IO.File.AppendText(logPath))
        //            {
        //                writer.WriteLine(text);
        //            }
        //            fileLength = _model.fileSize;
        //        }

                
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
            

        //}

        private PtNetModel GenerateRandoms()
        {
            PtNetModel _model = new PtNetModel();

            _model.numeric = ptNetManager.GenerateNumeric();
            _model.alphanumeric = ptNetManager.GenerateAlphanumeric();
            _model.floatNumber = ptNetManager.GenerateFloat();

            var logPath = Path.Combine(_hostEnvironment.ContentRootPath, "File/Generated", "PT.txt");

            _model.fileSize = Convert.ToInt32(new System.IO.FileInfo(logPath).Length);

            return _model;
        }






        /*
         * http://localhost:54889/api/pt/distribution
         */
        [Route("/api/pt/distribution")]
        [HttpGet]
        public List<PtNetDistribution> Distribution()
        {
            var logPath = Path.Combine(_hostEnvironment.ContentRootPath, "File/Generated", "PT.txt");

            List<string> lines = System.IO.File.ReadAllLines(logPath).Take(20).ToList();
            List<PtNetDistribution> modelList = new List<PtNetDistribution>();

            foreach (string line in lines)
            {
                String[] splitObj = line.Split(",");
                modelList.Add(
                    new PtNetDistribution()
                    {
                        floatNumber = splitObj[0] + " - float",
                        numeric = splitObj[1] + " - numeric",
                        alphanumeric = splitObj[2].Trim() + " - alphanumeric"
                    }
                    );
            }

            return modelList;
        }

    }
}
