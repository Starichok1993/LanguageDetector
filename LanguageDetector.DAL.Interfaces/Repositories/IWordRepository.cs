using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace LanguageDetector.DAL.Interfaces
{
    public interface IWordRepository
    {
        IQueryable<Word> All { get; }       // get all collection from database

        Word Find(Word word);             // return entity if exist

	    Word FindByText(string text);		// return entity if this text contains

        void InsertOrUpdate(Word obj);      // insert if object doesn't exist or update else

        void Delete(string text);           // delete word from database

        void Save();                        // save changes

    }
}
