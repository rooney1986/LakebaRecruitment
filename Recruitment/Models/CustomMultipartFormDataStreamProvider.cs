using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;

namespace Recruitment.Models
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string newFileName = headers.ContentDisposition.FileName;
            if (headers.ContentDisposition.FileName.Contains("."))
            {
                string[] arrFileName = headers.ContentDisposition.FileName.Split('.');
                newFileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "." + arrFileName[arrFileName.Length - 1];
            }
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? newFileName : "NoName";

            return name.Replace("\"", string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }
}