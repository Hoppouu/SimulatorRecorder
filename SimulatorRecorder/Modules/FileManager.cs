using SimulatorRecorder.Modules;
using System.Collections.ObjectModel;
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
            if (settings.ContainsKey(key))
            {
                settings[key] = value;
            }
        }

        private static void LoadEnvFile()
        {
            EnsureEnvFile();
            if (settings.ContainsKey(MyConstant.FolderPath))
            {
                if (settings[MyConstant.FolderPath] == MyConstant.basePath)
                {
                    folderPath = Path.Combine(MyConstant.basePath, MyConstant.Outputs);
                }
                else
                {
                    folderPath = settings[MyConstant.FolderPath];
                }
            }

            if (settings.ContainsKey(MyConstant.WindowsPlayerPath))
            {
                MyConstant.SetwindowsPlayerPath(settings[MyConstant.WindowsPlayerPath]);
            }

            if (settings.ContainsKey(MyConstant.DeadZone))
            {
                if (float.TryParse(settings[MyConstant.DeadZone], out float value))
                {
                    MyConstant.SetDeadZone(value);
                }
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

        public static bool WriteFile(List<OutputValue> outputs)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
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
                        ReadOnlyDictionary<string, int> outputData = outputs[i].OutputData;
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

        public static bool SetUnityFilePath(string title)
        {
            string? fullPath = GetPathWithFileDialog();
            if (fullPath == null)
            {
                return false;
            }

            MyConstant.SetwindowsPlayerPath(fullPath);
            SetSetting(MyConstant.WindowsPlayerPath, MyConstant.GetwindowsPlayerPath());
            return true;
        }
        public static bool SetSaveFilePath()
        {
            string? selectedPath = GetPathWithFolderDialog();
            if (selectedPath == null)
            {
                return false;
            }

            filePath = Path.Combine(selectedPath, $"recordLog_{count}.csv");
            SetSetting(MyConstant.FolderPath, selectedPath);
            return true;
        }

        public static void SetDeadZone()
        {
            int value = MessageBoxHelper.InputBox("데드존 설정", "값 설정 (범위 : 0 ~ 100 사이 값)", (int)(MyConstant.GetDeadZone() * 100));

            if (!(0 <= value && value <= 100))
            {
                return;
            }

            float deadZone = (float)value / 100f;
            MyConstant.SetDeadZone(deadZone);
            SetSetting(MyConstant.DeadZone, deadZone.ToString("F2"));
        }

        public static string?[] SetFilePath(string title)
        {
            string? fullPath = GetPathWithFileDialog();
            if (fullPath == null)
            {
                return null!;
            }

            string[] result = new string[2];
            result[0] = Path.GetDirectoryName(fullPath)!;
            result[1] = Path.GetFileName(fullPath)!;

            return result;
        }

        public static string GetFolderPath()
        {
            return folderPath;
        }

        private static void EnsureEnvFile()
        {
            if (!File.Exists(MyConstant.settingFilePath))
            {
                using (var writer = new StreamWriter(MyConstant.settingFilePath))
                {
                    writer.WriteLine($"{MyConstant.FolderPath}={MyConstant.baseWindowsPlayerPath}");
                    writer.WriteLine($"{MyConstant.WindowsPlayerPath}={MyConstant.GetwindowsPlayerPath()}");
                    writer.WriteLine($"{MyConstant.DeadZone}={MyConstant.GetDeadZone().ToString("F2")}");
                    settings[MyConstant.FolderPath] = MyConstant.basePath;
                    settings[MyConstant.WindowsPlayerPath] = MyConstant.baseWindowsPlayerPath;
                    settings[MyConstant.DeadZone] = MyConstant.GetDeadZone().ToString("F2");
                }
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

        public static List<OutputValue> ReadCSV(string title)
        {
            string? fullPath = GetPathWithFileDialog("CSV file", "csv");
            if (fullPath == null)
            {
                return new List<OutputValue>();
            }
            return CSVReader.Read(fullPath);
        }

        public static string? GetPathWithFileDialog(string title = "ALL Files", string extension = "")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = $"{title}|*.{extension}*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return openFileDialog.FileName;
        }
        public static string? GetPathWithFolderDialog()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = folderPath;

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return folderBrowserDialog.SelectedPath;
        }
    }
}
