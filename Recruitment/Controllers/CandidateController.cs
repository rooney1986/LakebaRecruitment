using Recruitment.Data;
using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Recruitment.Controllers
{
    public class CandidateController : ApiController
    {
        public IEnumerable<candidate> GetAll()
        {
            return candidate.GetAll();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/Assets/Uploads");
            Directory.CreateDirectory(root);
            var provider = new CustomMultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            
            if (result.FormData["model"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var model = result.FormData["model"];
            var serializer = new JavaScriptSerializer();
            CandidateModel modelToAdd = serializer.Deserialize<CandidateModel>(model);

            string pattern = @"^[A-Za-z ]+$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(modelToAdd.name))
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Name must only contains letters and white space");
                throw new HttpResponseException(response);
            }

            if (!regex.IsMatch(modelToAdd.sur_name))
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Surname must only contains letters and white space");
                throw new HttpResponseException(response);
            }

            candidate obj = new candidate();
            obj.name = modelToAdd.name;
            obj.sur_name = modelToAdd.sur_name;
            obj.position = modelToAdd.position;

            //get the files
            //TODO: Do something with each uploaded file
            foreach (var file in result.FileData)
            {
                obj.curriculum = Path.GetFileName(file.LocalFileName);
            }
            obj.Insert();

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Edit()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/Assets/Uploads");
            Directory.CreateDirectory(root);
            var provider = new CustomMultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            if (result.FormData["model"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var model = result.FormData["model"];
            var serializer = new JavaScriptSerializer();
            CandidateModel modelToAdd = serializer.Deserialize<CandidateModel>(model);

            string pattern = @"^[A-Za-z ]+$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(modelToAdd.name))
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Name must only contains letters and white space");
                throw new HttpResponseException(response);
            }

            if (!regex.IsMatch(modelToAdd.sur_name))
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Surname must only contains letters and white space");
                throw new HttpResponseException(response);
            }

            candidate obj = candidate.GetById(modelToAdd.id);
            if(obj != null)
            {
                obj.name = modelToAdd.name;
                obj.sur_name = modelToAdd.sur_name;
                obj.position = modelToAdd.position;
            }

            //get the files
            if (result.FileData.Count > 0)
            {
                //TODO: Do something with each uploaded file
                if (obj != null)
                {
                    foreach(var file in result.FileData)
                    {
                        obj.curriculum = Path.GetFileName(file.LocalFileName);
                    }
                    obj.Update();
                }
            }
            else
            {
                obj.Update();
            }

            IEnumerable<candidate> all = candidate.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, all);
        }

        [HttpDelete]
        public candidate Delete(int id)
        {
            candidate obj = candidate.GetById(id);
            if (obj != null)
            {
                obj.Delete();
            }

            return obj;
        }
    }
}
