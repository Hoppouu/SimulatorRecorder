using Silk.NET.XInput;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SimulatorRecorder.Modules
{
    internal static class FileManager
    {
        private static string settingFilePath;
        private static string folderPath;
        private static string filePath;
        private static int count;

        static FileManager()
        {
            settingFilePath = Directory.GetCurrentDirectory() + "\\setting.env";
            count = 1;
            folderPath = LoadFolderPath();
            filePath = Path.Combine(folderPath, $"recordLog_{count++}.csv");
        }
        public static void SaveFolderPath()
        {
            File.WriteAllText(settingFilePath, folderPath);
        }

        private static string LoadFolderPath()
        {
            if (File.Exists(settingFilePath))
            {
                return File.ReadAllText(settingFilePath).Trim();
            }
            else
            {
                return Directory.GetCurrentDirectory();
            }
        }

        public static bool SetFilePath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = folderPath;

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            folderPath = folderBrowserDialog.SelectedPath;
            filePath = Path.Combine(folderPath, $"recordLog_{count}.csv");

            return true;
        }
        public static string GetFolderPath()
        {
            return folderPath;
        }

        public static void WriteFile(List<GamepadInput> input)
        {
            if (input.Count == 0)
            {
                return;
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    String t = "Time";
                    for (int i = 0; i < input[0].values.Count; i++)
                    {
                        t += ",";
                        t += input[0].values[i].buttonName;
                    }
                    writer.WriteLine(t);


                    for (int i = 0; i < input.Count; i++)
                    {
                        List<string> row = new List<string>();
                        row.Add(input[i].time.ToString("F1"));
                        for (int j = 0; j < input[i].values.Count; j++)
                        {
                            float value = input[i].values[j].buttonValue;
                            row.Add(value.ToString("F2"));
                        }
                        writer.WriteLine($"{string.Join(",", row)}");
                    }
                }
                Console.WriteLine("location: " + filePath);
                Console.WriteLine("done");
                filePath = Path.Combine(folderPath, $"recordLog_{count++}.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
                Console.WriteLine("error type: " + ex.GetType().Name);
            }
        }
    }
}
