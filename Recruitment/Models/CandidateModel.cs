using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CandidateModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sur_name { get; set; }
        public string position { get; set; }
        public string curriculum { get; set; }
    }
}