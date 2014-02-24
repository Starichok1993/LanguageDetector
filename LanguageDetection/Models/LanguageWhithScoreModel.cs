using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDetection.Models
{
    public class LanguageWhithScoreModel
    {
        public string Language { set; get; }
        public decimal Score { set; get; }
    }
}