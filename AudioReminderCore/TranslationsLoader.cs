using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore
{
    public class TranslationsLoader
    {
        public virtual Dictionary<string, string> LoadTranslations(string languageName)
        {
            string filePath = GetFullFilePath(languageName);

            Log.Logger.Information($"Started loading translations [Language = {languageName}, path = {filePath}]");

            if (!File.Exists(filePath))
            {
                Log.Logger.Error($"File with translations not found: [Language = {languageName}, path = {filePath}].");
                throw new NotImplementedException();//TODO: retry ENG, or fail if ENG already
            }

            string[] lines = File.ReadAllLines(filePath);

            List<string> linesExpceptComments = RemoveCommentLines(lines);

            Dictionary<string,string> translations = ParseTranslationLines(linesExpceptComments);

            Log.Logger.Information($"Loaded {translations.Count} translations [Language = {languageName}, path = {filePath}]");
            return translations;
        }

        public virtual List<string> GetListOfLanguages()
        {
            
            DirectoryInfo translationsDir = new DirectoryInfo(FilePathHelper.GetTranslationsDir());
            string translationsFileNamePattern = GetTranslationFileName("*");

            FileInfo[] translationFiles = translationsDir.GetFiles(translationsFileNamePattern);
            
            List<string> languageNames = new List<string>();
            foreach (FileInfo translationFile in translationFiles)
            {
                string languageName = ExtractLanguageNameFromFile(translationFile.Name);
                if(languageName!= null)
                {
                    languageNames.Add(languageName);
                }
            }

            Log.Logger.Information($"Found {languageNames.Count} translations");
            return languageNames;
        }

        private static string ExtractLanguageNameFromFile(string translationFileName)
        {
            //valid example: translations-something.ini

            //check if not too short
            if(translationFileName.Length< TranslationPrefix.Length + TranslationExtension.Length + 1)
            {
                Log.Logger.Warning($"Ignoring translation file with invalid filename. No language name was present in filename: [fileName = {translationFileName}]");
                return null;
            }

            //check if ok format
            if (!translationFileName.StartsWith(TranslationPrefix) || !translationFileName.EndsWith(TranslationExtension))
            {
                Log.Logger.Warning($"Ignoring translation file with invalid filename. [fileName = {translationFileName}]");
                return null;
            }

            int startIndex = TranslationPrefix.Length;
            int length = translationFileName.Length - (TranslationPrefix.Length + TranslationExtension.Length);
            string languageName = translationFileName.Substring(startIndex, length);

            return languageName;
        }

        protected virtual string GetFullFilePath(string languageName)
        {
            string translationsDir = FilePathHelper.GetTranslationsDir();
            string translationsFileName = GetTranslationFileName(languageName);
            string translationFullPath = System.IO.Path.Combine(translationsDir, translationsFileName);

            return translationFullPath;
        }

        protected const string TranslationPrefix = "translations-";
        protected const string TranslationExtension = ".ini";
        protected virtual string GetTranslationFileName(string languageName)
        {
            return TranslationPrefix + languageName + TranslationExtension;
        }

        /// <summary>
        /// Parse cleaned up lines (with no comments and empty lines) to collection of translations
        /// </summary>
        protected virtual Dictionary<string, string> ParseTranslationLines(List<string> lines)
        {
            Dictionary<string, string> translations = new Dictionary<string, string>();

            string translationKey = null;
            string translationValue = null;

            foreach (var line in lines)
            {
                bool isTranslationKey = line.StartsWith("$");

                if (isTranslationKey)
                {
                    //storing the previous translation pair (if there was one in progress)
                    TryToStoreTranslationPair(translations, ref translationKey, ref translationValue);
                    translationValue = "";

                    translationKey = line.Remove(0,1); //they key shouldn't contain the '$' char so it's removed
                }
                else // else its a translation value
                {
                    //check if there's a key to which the the value should be added
                    if (translationKey != null)
                    {
                        if (!string.IsNullOrEmpty(translationValue))
                        {
                            translationValue += Environment.NewLine;
                        }

                        translationValue += line;

                    }
                    else
                    {
                        Log.Logger.Warning($"Discarding translation value which doesn't have a translation key before it.");
                    }

                }
            }

            //storing the previous translation pair (if there was one in progress)
            TryToStoreTranslationPair(translations, ref translationKey, ref translationValue);

            return translations;
        }

        protected virtual void TryToStoreTranslationPair(Dictionary<string, string> translations, ref string translationKey, ref string translationValue)
        {
            //check if there is a key is waiting to be stored
            if (translationKey == null)
            {
                return;
            }

            //check if we had some value for that key
            if (string.IsNullOrEmpty(translationValue))
            {
                Log.Logger.Warning($"Discarding translation key line with no value line [translationKey = {translationKey}].");
                return;
            }

            if (string.IsNullOrWhiteSpace(translationKey))
            {
                Log.Logger.Warning($"Discarding translation pair with empty translation key [translationKey = {translationKey}, translationValue = {translationValue}].");
                return;
            }

            if (translations.ContainsKey(translationKey))
            {
                Log.Logger.Warning($"Discarding translation pair with already existing (duplicate) translation key [translationKey = {translationKey}, translationValue = {translationValue}].");
                return;
            }
            
            translations.Add(translationKey, translationValue);
        }

        protected virtual List<string> RemoveCommentLines(string[] lines)
        {
            List<string> linesExpceptComments = new List<string>();

            foreach (string line in lines)
            {
                if(!IsCommentOrWhiteSpace(line))
                {
                    linesExpceptComments.Add(line);
                }
            }

            return linesExpceptComments;
        }

        protected virtual bool IsCommentOrWhiteSpace(string line)
        {
            if (line.StartsWith("#"))
            {
                //Lines starting with # can be used for comments
                return true;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                //An empty line
                return true;
            }

            return false;
        }
    }
}
