using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class DayLevel
    {
        public string Date { get; set; }
        public List<DataLevel> dataStorage { get; set; }

        public DayLevel()
        {
            dataStorage = new List<DataLevel>();
        }
        public void AddOrUpdateData(DataLevel newData)
        {
            var existingData = dataStorage.FirstOrDefault(d => d.Path == newData.Path);

            if (existingData != null)
            {
                // Обновляем существующие данные
                existingData.Size = newData.Size;
                existingData.type = newData.type;
                existingData.Quantity = newData.Quantity;
                existingData.status = newData.status;
                existingData.checkTime = newData.checkTime;
                existingData.lastWriteTime = newData.lastWriteTime;
            }
            else
            {
                // Добавляем новые данные
                dataStorage.Add(newData);
            }
        }
        public static List<DayLevel> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<DayLevel>();

            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<DayLevel>>(jsonData);
        }
        public static void SaveToFile(List<DayLevel> dayLevels, string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(dayLevels, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
    public class DataLevel
    {
        public string Path { get; set; }
        public List<string> Size { get; set; }
        public string type { get; set; }
        public int Quantity { get; set; }
        public List<bool> status { get; set; }
        public List<DateTime> checkTime { get; set; }
        public List<DateTime> lastWriteTime { get; set; }
        public DataLevel()
        {
            Size = new List<string>();
            status = new List<bool>();
            checkTime = new List<DateTime>();
            lastWriteTime = new List<DateTime>();
        }
    }
}