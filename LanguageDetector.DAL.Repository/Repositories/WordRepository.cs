using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using LanguageDetector.DAL.Interfaces;

namespace LanguageDetector.DAL.Repository
{
	public class WordRepository : IWordRepository
	{
		protected readonly LanguageDetectorContext Context;
		protected readonly DbSet<Word> DbSetWords;
	    protected readonly DbSet<ChanceOfLanguage> DbSetCOLs;

		public WordRepository(LanguageDetectorContext context)
		{
			Context = context;
			DbSetWords = context.Set<Word>();
		    DbSetCOLs = context.Set<ChanceOfLanguage>();
		}

		public IQueryable<Word> All
		{
			get { return DbSetWords; }
		}

		public Word Find(Word word)
		{
			return DbSetWords.Find(word);
		}

		public Word FindByText(string text)
		{
			return DbSetWords.FirstOrDefault(p => p.Text == text);			
		}

		public void InsertOrUpdate(Word obj)
		{
			Context.Entry(obj).State = DbSetWords.FirstOrDefault(p => p.Text == obj.Text) == null ?
				EntityState.Added : EntityState.Modified;

			//if (DbSet.Find(obj) != null)
			//{
			//	Context.Entry(obj).State
				
			//}
		}

		public void Delete(string text)
		{
			Word obj = DbSetWords.FirstOrDefault(p => p.Text == text);
			DbSetWords.Remove(obj);
		}


	    public void Save()
		{
			Context.SaveChanges();
		}
	}
}
