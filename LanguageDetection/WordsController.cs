﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entity;
using LanguageDetector.BLL.Implementation;
using LanguageDetector.BLL.Implementation.Manager;
using System.Configuration;

namespace LanguageDetection
{
    public class WordsController : ApiController
    {
        private readonly WordManager _wordManager = new WordManager(ConfigurationManager.AppSettings["TranslatorKey"]);

        // GET api/<controller>
        public IEnumerable<Word> Get()
        {
	        List<Word> list = _wordManager.GetAll().ToList();
            _wordManager.GetWordByText("gracias");
            return _wordManager.GetAll();
        }

        // GET api/<controller>/5
        public Word Get(string text)
        {
            var resultWord = _wordManager.GetWordByText(text);
            return resultWord;
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