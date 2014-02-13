using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public static class LanguageList
    {
        private static readonly Dictionary<string, string> _langList = new Dictionary<string, string>{{"En","English"}, {"Ru","Русский"}, {"Es","Spanish"}, {"Bg", "Bolgarian"}, {"Pt", "Portuguese"}};

        public static string GetLanguage(string langKey)
        {
            return _langList[langKey];
 
        }
    }
}
