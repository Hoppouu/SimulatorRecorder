using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimulatorRecorder.Modules;




public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    //public static List<Dictionary<string, object>> Read(string file)
    public static List<OutputValue> Read(string file)
    {
        //var list = new List<Dictionary<string, object>>();

        List<OutputValue> list = new List<OutputValue>();
        string data = File.ReadAllText(file);

        //Debug.Log(data);

        var lines = Regex.Split(data, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            //var entry = new Dictionary<string, object>();

            OutputValue outputValue = new OutputValue();

            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }

                for(int k = 0; k < MyConstant.outputsName.Length; k++)
                {
                    if (header[j] == MyConstant.outputsName[k])
                    {
                        outputValue.Set(MyConstant.outputsName[k], (int)finalvalue);
                    }
                }
                //entry[header[j]] = finalvalue;
            }
            list.Add(outputValue);
            //list.Add(entry);
        }
        return list;
    }
}
