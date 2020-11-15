using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

// m.ahmed anis ep1850060

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["request"];
            Subject[] Subjects = JsonConvert.DeserializeObject<Subject[]>(data);

            Subject minSubs = Subjects.First(x => x.marks == Subjects.Min(y => y.marks));
            Subject maxSubs = Subjects.First(x => x.marks == Subjects.Max(y => y.marks));

            decimal numOfSubs = Subjects.Count();
            decimal totMarks = numOfSubs * 100;
            decimal totMarksObt = Subjects.Sum(x => x.marks);
            decimal percent = (totMarksObt / totMarks) * 100;

            Result result = new Result()
            {
                MinimumSubject = minSubs,
                MaximumSubject = maxSubs,
                Percentage = percent
            };

            return JsonConvert.SerializeObject(result);
        }

        public class Subject
        {
            public string name { get; set; }
            public int marks { get; set; }

        }

        public class Result
        {
            public Subject MinimumSubject { get; set; }
            public Subject MaximumSubject { get; set; }
            public decimal Percentage { get; set; }
        }
    }
    internal class StripMethodAttribute : Attribute
    {
        private ResponseFormat json;
        private bool UseHttpGet;

        public StripMethodAttribute(ResponseFormat json, bool UseHttpGet)
        {
            this.json = json;
            this.UseHttpGet = UseHttpGet;
        }
    }
}

