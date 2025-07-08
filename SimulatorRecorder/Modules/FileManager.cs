using SimulatorRecorder.Modules;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SimulatorRecorder.Modules
{
    internal static class FileManager
    {
        private static string folderPath = null!;
        private static string filePath = null!;
        private static int count;
        
        public static bool errorOccurred = false;

        private static Dictionary<string, string> settings = ReadSettings(MyConstant.settingFilePath);

        public static void Init()
        {
            count = 1;
            LoadEnvFile();
            filePath = Path.Combine(folderPath, $"recordLog_{count++}.csv");
        }

        private static void SetSetting(string key, string value)
        {
            if(settings.ContainsKey(key))
            {
                settings[key] = value;
            }
        }
        public static void SaveEnvFile()
        {
            using (var writer = new StreamWriter(MyConstant.settingFilePath))
            {
                foreach (var kv in settings)
                {
                    writer.WriteLine($"{kv.Key}={kv.Value}");
                }
            }
        }
        private static void EnsureEnvFile()
        {
            if(!File.Exists(MyConstant.settingFilePath))
            {
                using (var writer = new StreamWriter(MyConstant.settingFilePath))
                {
                    writer.WriteLine($"folderPath={MyConstant.basePath}");
                    writer.WriteLine($"windowsPlayerPath={MyConstant.baseWindowsPlayerPath}");
                    settings["folderPath"] = MyConstant.basePath;
                    settings["windowsPlayerPath"] = MyConstant.baseWindowsPlayerPath;
                }
            }
         }
        private static void LoadEnvFile()
        {
            EnsureEnvFile();
            if (settings.ContainsKey("folderPath"))
            {
                folderPath = settings["folderPath"];
            }

            if (settings.ContainsKey("windowsPlayerPath"))
            {
                ProgramModule.SetFilePath(settings["windowsPlayerPath"]);
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
            SetSetting("folderPath", folderPath);
            return true;
        }

        public static string?[] GetFilePath(string title)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null!;
            }
            string fullPath = openFileDialog.FileName;
            string[] result = new string[2];
            result[0] = Path.GetDirectoryName(fullPath)!;
            result[1] = Path.GetFileName(fullPath)!;

            return result;
        }

        public static string GetFolderPath()
        {
            return folderPath;
        }

        public static bool WriteFile(List<OutputValue> outputs)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    String t = "";
                    for (int i = 0; i < MyConstant.outputsName.Length; i++)
                    {
                        t += MyConstant.outputsName[i];
                        if (i < MyConstant.outputsName.Length - 1)
                        {
                            t += ",";
                        }
                    }
                    writer.WriteLine(t);

                    for (int i = 0; i < outputs.Count; i++)
                    {
                        Dictionary<string, int> outputData = outputs[i].GetOutputDictionary();
                        List<string> row = new List<string>();
                        for (int j = 0; j < outputData.Count; j++)
                        {
                            int value = outputData[MyConstant.outputsName[j]];
                            row.Add(value.ToString());
                        }
                        writer.WriteLine($"{string.Join(",", row)}");
                    }
                }
                Console.WriteLine("location: " + filePath);
                Console.WriteLine("done");
                filePath = Path.Combine(folderPath, $"recordLog_{count++}.csv");
                errorOccurred = false;
                return true;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                Console.WriteLine("error: " + ex.Message);
                Console.WriteLine("error type: " + ex.GetType().Name);
                MessageBoxHelper.ShowTopMost(
                    $"파일 저장에 실패했습니다. 확인 후 종료 버튼을 눌러주세요.\n{ex.Message}",
                    ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        private static Dictionary<string, string> ReadSettings(string settingFilePath)
        {
            var dict = new Dictionary<string, string>();

            if (!File.Exists(settingFilePath))
                return dict;

            var lines = File.ReadAllLines(settingFilePath);

            foreach (var line in lines)
            {
                var trimmed = line.Trim();

                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#") || trimmed.StartsWith(";"))
                    continue;

                var splitIndex = trimmed.IndexOf('=');
                if (splitIndex < 0)
                    continue;

                var key = trimmed.Substring(0, splitIndex).Trim();
                var value = trimmed.Substring(splitIndex + 1).Trim();

                dict[key] = value;
            }

            return dict;
        }
    }
}
