﻿using System.Collections.Generic;
using System.IO;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database
{
    public static class JsonBackupManager
    {
        private const string FilePath = @"Info.json";
        private const string CardsName = @"Cards.json";

        private static void CheckFileExist(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                using (FileStream file = File.Create(filePath))
                {
                    file.Close();
                }
            }
        }
    
        public static void SaveToJson(List<LimpUser> users)
        {
            CheckFileExist(FilePath);

            string jsonString = JsonConvert.SerializeObject(users);
            File.WriteAllText(FilePath, jsonString);
        }

        #region Card data

        public static StudyGroup LoadCardUserList(string cardTitle)
        {
            string filePath = $"card_{cardTitle}.json";
            CheckFileExist(filePath);
            string jsonData = File.ReadAllText(filePath);
            StudyGroup group = JsonConvert.DeserializeObject<StudyGroup>(jsonData);
            return group;
        }
        public static void SaveCardName(string cardTitle)
        {
            CheckFileExist(CardsName);
            var jsonData = File.ReadAllText(CardsName);
            List<string> names = JsonConvert.DeserializeObject<List<string>>(jsonData) ?? new List<string>();
            if (names.Contains(cardTitle) == false)
                names.Add(cardTitle);
            File.WriteAllText(CardsName, JsonConvert.SerializeObject(names));
        }
        public static List<string> LoadCardName()
        {
            var jsonData = File.ReadAllText(CardsName);
            List<string> names = JsonConvert.DeserializeObject<List<string>>(jsonData) ?? new List<string>();
            return names;
        }

        public static void SaveCardUserList(StudyGroup group, string cardTitle)
        {
            string filePath = $"card_{cardTitle}.json";
            string jsonString = JsonConvert.SerializeObject(group);
            File.WriteAllText(filePath, jsonString);
        }

        #endregion


        public static List<LimpUser> LoadFromJson()
        {
            CheckFileExist(FilePath);

            string jsonString = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<LimpUser>>(jsonString) ?? new List<LimpUser>();
        }
     }
}