using SimulatorRecorder.Modules;
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
        
        public static bool errorOccurred = false;

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
    }
}
