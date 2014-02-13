using System;
using System.Collections.Generic;
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

        public WordManager(string translatorKey)
        {
            _translatorKey = translatorKey;
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
                var translator = new Translator(_translatorKey);
                var langKey = translator.DetectLang(text);
                

                resultWord = new Word
                {
                    Text = text,
                    Language = LanguageList.GetLanguage(langKey.ToString()),
                    PercentOfReliability = 100
                };
                _wordRepository.InsertOrUpdate(resultWord);
                _wordRepository.Save();
            }

            return resultWord;
        }

        public void InsertWord(Word word)
        {
            _wordRepository.InsertOrUpdate(word);
            _wordRepository.Save();
        }
    }
}
