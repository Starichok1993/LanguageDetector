using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Entity;
using LanguageDetection.Models;
using LanguageDetector.BLL.Implementation.Manager;
using System.Configuration;

namespace LanguageDetection
{
    public class WordsController : ApiController
    {
        private readonly WordManager _wordManager = new WordManager();

        // GET api/<controller>
        public IEnumerable<Word> Get()
        {
            return null;
        }

        // GET api/<controller>/5
        public List<LanguageWhithScoreModel> Get(string text)
        {
            var resultWord = _wordManager.GetWordByText(text);
            var resultToClient = new List<LanguageWhithScoreModel>();

            if (resultWord == null) return resultToClient;

            resultToClient.AddRange(resultWord.Languages.Select(item =>
                new LanguageWhithScoreModel {Language = item.Language, Score = item.Chance}));
            return resultToClient;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}