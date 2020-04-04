﻿using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder
{
    public class FilePersistanceAdapter<TEntity>
    {
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

        public FilePersistanceAdapter(List<TEntity> defaultValues = null)
        {
            LoadDataFromStorage(defaultValues);
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
            string servicePath = AppDomain.CurrentDomain.BaseDirectory; //TODO: extract both occuranecs of this
            string fileExtension = ".xml";
            string fileName = typeof(TEntity).ToString(); //this prevents multiple lists of same type, but do we need that
            string nameNameWithExtension = fileName + fileExtension; //storageFilName; 

            string fullPath = Path.Combine(servicePath, nameNameWithExtension);
            string fullPathNonRelative = Path.GetFullPath(fullPath);

            return fullPathNonRelative;
        }
        private void LoadRemindersFromFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Loading reminders from file [filename = {filePath}]");
            string xmlString = File.ReadAllText(filePath);
            Entities = UserInterfaceCommunication.FromXmlString<List<TEntity>>(xmlString);
            //reminderEntities = reminderEntitiesArray.ToList();

            Log.Logger.Information($"Loading reminders from file done");
        }

        ////TODO: also move to persistency handler
        //~FilePersistanceAdapter()
        //{
        //    SaveRemindersToFile();
        //}

        public void SaveRemindersToFile()
        {
            string filePath = GetFilePath();

            Log.Logger.Information($"Saving reminders to file [filename = {filePath}]");
            //ReminderEntity[] remindersArray = reminderEntities.ToArray();

            string xmlString = UserInterfaceCommunication.ToXmlString(Entities);

            File.WriteAllText(filePath, xmlString);

            Log.Logger.Information($"Saving reminders to file done");
        }
    }
}
