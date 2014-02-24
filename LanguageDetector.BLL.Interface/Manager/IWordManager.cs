using System.Collections.Generic;
using Entity;

namespace LanguageDetector.BLL.Interface.Manager
{
    public interface IWordManager
    {
        IEnumerable<Word> GetAll(); //get all collection of word
        Word GetWordByText (string text); //get word by text
    }
}
