using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Entity;
using LanguageDetector.BLL.Interface.Manager;
using LanguageDetector.DAL.Repository;

namespace LanguageDetector.BLL.Implementation.Manager
{
    public class WordManager:IWordManager
    {
        private readonly WordRepository _wordRepository;
        private readonly List<string> _languageList;


        public WordManager()
        {
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
                var result = WorkWithText(text);

                resultWord = new Word
                {
                    Text = text,
                    Languages = new List<ChanceOfLanguage>()
                };

                foreach (var chanceOfLanguage in result)
                {
                    chanceOfLanguage.WordId = text;
                    resultWord.Languages.Add(chanceOfLanguage);
                }

                _wordRepository.InsertOrUpdate(resultWord);
                _wordRepository.Save();
            }

            return resultWord;
        }

        private IEnumerable<ChanceOfLanguage> WorkWithText(string text)
        {
            var ngramms = GetNgrammFromWord(text, 3);
            if (ngramms == null)
            {
                return null;
            }
            var languageWithScoreDictionary = new Dictionary<string, double>();

            double totalScore = 0;

            foreach (var lang in _languageList)
            {
                var docText = GetTextFromDocument(@"C:\Users\Alex\Documents\Visual Studio 2013\Projects\LanguageDetection\LanguageDetection\App_Data\" + lang + ".txt");
                var score = GetScore(docText, ngramms);

                totalScore += score;
                languageWithScoreDictionary.Add(lang, score);
            }

            if (Math.Abs(totalScore) < 0.0000005)
            {
                totalScore = 1;
            }

            return languageWithScoreDictionary.Select(d =>
                new ChanceOfLanguage {Language = d.Key, Chance = (decimal) ((d.Value/totalScore)*100)}).ToList();
        }
        private string GetTextFromDocument(string docName)
        {
            var resultString = "";
            try
            {
                Encoding enc = Encoding.GetEncoding(1251);
                //var streamReader = new StreamReader(@"C:\Users\Alex\Documents\Visual Studio 2013\Projects\LanguageDetection\LanguageDetection\App_Data\" + docName, enc);
                var streamReader = new StreamReader(docName, enc);
                
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
        private double GetScore(string text, ICollection<string> ngramms)
        {
            double resultScore = 0.0;
            var listWords = GetWordsFromText(text);

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
        private IEnumerable<string> GetWordsFromText(string text)
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
