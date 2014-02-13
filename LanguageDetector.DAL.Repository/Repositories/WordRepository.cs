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
		protected readonly DbSet<Word> DbSet;

		public WordRepository(LanguageDetectorContext context)
		{
			Context = context;
			DbSet = context.Set<Word>();
		}

		public IQueryable<Word> All
		{
			get { return DbSet; }
		}

		public Word Find(Word word)
		{
			return DbSet.Find(word);
		}

		public Word FindByText(string text)
		{
			return DbSet.FirstOrDefault(p => p.Text == text);			
		}

		public void InsertOrUpdate(Word obj)
		{
			Context.Entry(obj).State = DbSet.FirstOrDefault(p => p.Text == obj.Text) == null ?
				EntityState.Added : EntityState.Modified;

			//if (DbSet.Find(obj) != null)
			//{
			//	Context.Entry(obj).State
				
			//}
		}

		public void Delete(string text)
		{
			Word obj = DbSet.FirstOrDefault(p => p.Text == text);
			DbSet.Remove(obj);
		}

		public void Save()
		{
			Context.SaveChanges();
		}
	}
}
