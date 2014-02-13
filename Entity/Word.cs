using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations;


namespace Entity
{
    public class Word
    {
        [Key]
        public string Text { set; get; }
        public string Language { set; get; }
        public decimal PercentOfReliability { set; get; }
    }

}
