using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;


namespace LanguageDetector.DAL.Repository
{
    public class LanguageDetectorContext : DbContext
    {
        public LanguageDetectorContext() : base("LanguageDetectorContext")
        {
            //Db(new System.Data.SQLite.SQLiteFactory());
            
        }

        public DbSet<Word> Words { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove
                <OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
