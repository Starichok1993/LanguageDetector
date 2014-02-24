using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace LanguageDetector.DAL.Repository
{
    public class LanguageDetectorInitializer :
        DropCreateDatabaseAlways<LanguageDetectorContext>
    {
        protected override void Seed(LanguageDetectorContext context)
        {
            base.Seed(context);

            //context.Words.Add(new Word
            //{
            //    Language = "English",
            //    Text = "end",
            //    PercentOfReliability = 100
            //});

            //context.Words.Add(new Word
            //{
            //    Text = "Язык",
            //    Language = "Русский",
            //    PercentOfReliability = 100
            //});

            //context.Words.Add(new Word
            //{
            //    Text = "Язык",
            //    Languages = new List<ChanceOfLanguage>
            //    {
            //       new ChanceOfLanguage{}
            //    }
            //})
        }
    }
}
