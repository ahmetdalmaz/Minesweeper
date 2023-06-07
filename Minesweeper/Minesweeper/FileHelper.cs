using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class FileHelper
    {
        private static string _directoryPath = string.Concat(Directory.GetCurrentDirectory(), @"\Settings\");
        private static string _filePath = string.Concat(_directoryPath, "game.json");
        private static string CreateJsonFile()
        {

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            if (!File.Exists(_filePath))
            {

                using (FileStream stream = File.Create(_filePath))
                {
                    stream.Flush();
                }
            }


            return _filePath;
        }

        public static void WriteJson(string jsonResult)
        {
            string filePath = CreateJsonFile();
            using (FileStream stream = File.Open(filePath, FileMode.Truncate))
            {
                byte[] data = new UTF8Encoding(true).GetBytes(jsonResult);
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }

        public static Game ReadJson()
        {
            CreateJsonFile();
            using (FileStream stream = File.OpenRead(_filePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<Game>(reader.ReadToEnd());
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Bir hata meydana geldi. Tekrar deneyin","Son Oynananlar",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                       
                        return null;

                    }
                    

                }
            }

        }

    }
}
