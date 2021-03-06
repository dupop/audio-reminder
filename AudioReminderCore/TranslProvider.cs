﻿using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    /// <summary>
    /// Translations provider
    /// </summary>
    public static class TranslationProvider
    {
        static  Dictionary<string, string> TranslationsForCurrentLanguage;

        /// <summary>
        /// Currently selected and loaded language
        /// </summary>
        public static string Language { get; private set; }

        static TranslationProvider()
        {
            Language = "english";


            //TODO: remove this from here, this is just for test
            TranslationProvider.LoadNewLanguage("serbian-latin");
        }

        /// <summary>
        /// Finds translation for a translation key
        /// </summary>
        public static string Tr(string translationKey)
        {
            if (TranslationsForCurrentLanguage == null)
            {
                LoadTranslations();
            }

            if(!TranslationsForCurrentLanguage.ContainsKey(translationKey))
            {
                //TODO: try to find all missing translations in english? don't load file each time

                string rawTranslationKeyToDisplay = '<' + translationKey + '>';

                return rawTranslationKeyToDisplay;
            }

            return TranslationsForCurrentLanguage[translationKey];
        }

        public static void LoadNewLanguage(string language)
        {
            Language = language;
            LoadTranslations();
        }

        private static void LoadTranslations()
        {
            TranslationsForCurrentLanguage = new TranslationsLoader().LoadTranslations(Language);
        }
    }
}
