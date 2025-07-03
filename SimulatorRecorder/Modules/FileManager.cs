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
        public static void WriteFile(List<GamepadInput> input)
        {
            if (input.Count == 0)
            {
                return;
            }
            string folderPath = ".\\";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = folderPath;
            saveFileDialog.Filter = "CSV 파일 (*.csv)|*.csv";
            saveFileDialog.FileName = "gamepad_log.csv";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                string filePath = saveFileDialog.FileName;

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
                Console.WriteLine("error type: " + ex.GetType().Name);
            }
        }
    }
}
