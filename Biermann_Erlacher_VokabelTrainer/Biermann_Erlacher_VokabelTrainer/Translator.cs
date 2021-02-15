using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Biermann_Erlacher_VokabelTrainer
{
    class Translator
    {
        #region members
        private string[] _languages;
        private string[] _translations;
        #endregion

        #region constructor
        public Translator()
        {

        }
        public Translator(string[] languages, string[] translations)
        {
            _languages = languages;
            _translations = translations;
        }
        #endregion


        #region properties
       
        public string[] Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
            }
        }
       
        public string[] Translations
        {
            get
            {
                return _translations;
            }
            set
            {
                _translations = value;
            }
        }

        #endregion


        #region methods
        public string GetTranslations(int index)
        {
            return Translations[index];
        }

        
        #endregion
    }
}
