using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Word
    {
        [Key]
        public string Text { set; get; }

        public virtual List<ChanceOfLanguage> Languages { set; get; }
    }

    public class ChanceOfLanguage
    {
        [Key]
        public int ChanceOfLanguageId { set; get; }
        public string Language { set; get; }
        public decimal Chance { set; get; }


        public string WordId { set; get; }
        public virtual Word Word { set; get; }
    }

}
