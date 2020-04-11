using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderService.Persistence
{
    public class FilePersistenceAdapter<TEntity>
    {
        //TODO: Add Valite method to TEntity so that we fix or remove incorrect entities after load from file

        //sinelgton code

        //private static FilePersistanceAdapter singleton;

        //public static FilePersistanceAdapter Singleton => singleton ?? InitializeSingleton();

        ///// <summary>
        ///// Provides eager singleton initialization
        ///// </summary>
        ///// <returns></returns>
        //public static FilePersistanceAdapter InitializeSingleton()
        //{
        //    singleton = new FilePersistanceAdapter();

        //    return singleton;
        //}

        //private const string storageFilName = "reminders.xml";
        public List<TEntity> Entities;
        public ServiceSettingsDto[] settingsDtos;

        public FilePersistenceAdapter(List<TEntity> defaultValues = null)
        {
            LoadDataFromStorage(defaultValues);

            EntitiesChanged += PersistEntityChanges;
        }

        protected virtual void PersistEntityChanges()
        {
            SaveRemindersToFile();
        }

        protected void LoadDataFromStorage(List<TEntity> defaultValues)
        {
            if (File.Exists(GetFilePath()))
            {
                LoadRemindersFromFile();
            }
            else
            {
                CreateNewReminderList(defaultValues);
            }
        }

        private void CreateNewReminderList(List<TEntity> defaultValues)
        {
            Log.Logger.Information($"Using empty (mock for now instead) list of reminders");

            if(defaultValues == null)
            {
                Entities = new List<TEntity>();
            }
            else
            {
                Entities = defaultValues;
            }
            Entities = defaultValues;
            //File.WriteAllText(storageFilePath, "");
        }

        protected string GetFilePath()
        {
            string serviceDir = AppDomain.CurrentDomain.BaseDirectory; //TODO: extract both occuranecs of this
            string persistenceSubDir = "persistence";
            string peristenceDir = Path.Combine(serviceDir, persistenceSubDir);

            string fileExtension = ".xml";
            string fileName = typeof(TEntity).ToString(); //this prevents multiple lists of same type, but do we need that
            string nameNameWithExtension = fileName + fileExtension; //storageFilName; 

            string fullPath = Path.Combine(peristenceDir, nameNameWithExtension);
            string fullPathNonRelative = Path.GetFullPath(fullPath);

            return fullPathNonRelative;
        }
        private void LoadRemindersFromFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Loading reminders from file [filename = {filePath}]");
            string xmlString = File.ReadAllText(filePath);
            Entities = SerializationHelper.FromXmlString<List<TEntity>>(xmlString);
            //reminderEntities = reminderEntitiesArray.ToList();

            Log.Logger.Information($"Loading reminders from file done");
        }

        public void SaveRemindersToFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Saving reminders to file [filename = {filePath}]");
            //ReminderEntity[] remindersArray = reminderEntities.ToArray();

            string xmlString = SerializationHelper.ToXmlString(Entities);

            File.WriteAllText(filePath, xmlString);

            Log.Logger.Information($"Saving reminders to file done");
        }

        /// <summary>
        /// Fire this event every time when list of entities is modified. Initial loading of data does not fire this event.
        /// </summary>
        public event Action EntitiesChanged;


        /// <summary>
        /// Causes flushing entites to file and rising EntitiesChanged event.
        /// </summary>
        public void OnEntitesChanged()
        {
            EntitiesChanged?.Invoke();
        }
    }
}
