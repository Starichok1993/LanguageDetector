using System;
using System.Collections.Generic;
using Entity;
using LanguageDetector.BLL.Interface.Manager;
using LanguageDetector.DAL.Repository;

namespace LanguageDetector.BLL.Implementation.Manager
{
    public class WordManager:IWordManager
    {
        private readonly WordRepository _wordRepository;

        public WordManager()
        {
            _wordRepository = new WordRepository(new LanguageDetectorContext());
        }

        public IEnumerable<Word> GetAll()
        {
            return _wordRepository.All;
        }

        public Word GetWordByText(string text)
        {
            return _wordRepository.FindByText(text);
        }

        public void InsertWord(Word word)
        {
            _wordRepository.InsertOrUpdate(word);
            _wordRepository.Save();
        }
    }
}
