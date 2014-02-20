using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Entity;
using LanguageDetector.BLL.Interface.Manager;
using LanguageDetector.DAL.Repository;
using YandexLinguistics.NET;

namespace LanguageDetector.BLL.Implementation.Manager
{
    public class WordManager:IWordManager
    {
        private readonly WordRepository _wordRepository;
        private readonly string _translatorKey;
        private readonly List<string> _languageList; 

        public WordManager(string translatorKey)
        {
            _translatorKey = translatorKey;
            _languageList = new List<string> { "English", "Spanish", "Portuguese", "Bulgarian", "Russian" };
            _wordRepository = new WordRepository(new LanguageDetectorContext());
        }

        public IEnumerable<Word> GetAll()
        {
            return _wordRepository.All;
        }

        public Word GetWordByText(string text)
        {
            var resultWord = _wordRepository.FindByText(text);

            if (resultWord == null)
            {
                //var translator = new Translator(_translatorKey);
                //var langKey = translator.DetectLang(text);

                var result = WorkWithText(text);
                
//                _wordRepository.InsertOrUpdate(resultWord);
                _wordRepository.Save();
            }

            return resultWord;
        }

        private Dictionary<string, decimal> WorkWithText(string text)
        {
            List<string> ngramms = GetNgrammFromWord(text, 3);
            var languageWithScoreDictionary = new Dictionary<string, double>();
            double totalScore = 0;

            foreach (var lang in _languageList)
            {
                string docText = GetTextFromDocument(lang + ".txt");
                double score = GetScore(docText, ngramms);
                totalScore += score;
                languageWithScoreDictionary.Add(lang, score);
            }

            return languageWithScoreDictionary.ToDictionary(item => item.Key,
                item => (decimal) ((item.Value/totalScore)*100));
        }

        private string GetTextFromDocument(string docName)
        {
            var resultString = "";

            try
            {
                Encoding enc = Encoding.GetEncoding(1251);
                var streamReader = new StreamReader(@"C:\Users\Alex\Documents\Visual Studio 2013\Projects\LanguageDetection\LanguageDetection\App_Data\" + docName, enc);
                while (!streamReader.EndOfStream)
                {
                    resultString += streamReader.ReadLine();
                }

                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultString;
        }

        private double GetScore(string text, List<string> ngramms)
        {
            double resultScore = 0.0;
            var listWords = getWordsFromText(text);

            foreach (var word in listWords)
            {
                var listNgramms = GetNgrammFromWord(word, 3);

                if (listNgramms != null)
                {
                    foreach (var ngramm in listNgramms)
                    {
                        if (ngramms.Contains(ngramm))
                        {
                            resultScore++;
                        }
                    }
                }
            }

            return resultScore/text.Length;
        }

        private List<string> getWordsFromText(string text)
        {
            
            var pattern = @"\w+";
            var result = new List<String>();

            MatchCollection list = Regex.Matches(text, pattern);

            foreach (var item in list)
            {
                result.Add(item.ToString());
            }

            return result;
        }

        public void InsertWord(Word word)
        {
            _wordRepository.InsertOrUpdate(word);
            _wordRepository.Save();
        }

        private List<string> GetNgrammFromWord(string text, int n)   //get ngrammList from input text, n- how match length of gramm
        {
            if (text.Length < n)
            {
                return null;
            }

            var ngrammList = new List<string>();
            var normalText = text.ToLower();
            for (int i = 0; i + n <= normalText.Length; i++)
            {
                ngrammList.Add(normalText.Substring(i, n));
            }

            return ngrammList;
        }
        
    }
}
