﻿using System.Text;

namespace CSV4net;
public class ReadingFile
{
    private static List<string> ReadFile(string path, string encoding="utf-8")
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(encoding)))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        return list;
    }

    public static List<string[]> GetCSV(string path, char separate=',', string encoding="utf-8")
    {
        var returnList = new List<string[]>();
        var list = ReadFile(path, encoding);

        var separateList = new List<string>();
        StringBuilder sb = new StringBuilder();
        bool isDoubleQuotation = false;
        foreach(var line in list)
        {
            foreach(char c in line)
            {
                if(c == separate && !isDoubleQuotation)
                {  
                    separateList.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                else
                {
                    sb.Append(c);
                    if(c == '"')
                    {
                        isDoubleQuotation = !isDoubleQuotation;
                    }
                }
            }

            if(sb.Length != 0 && !isDoubleQuotation)
            {
                separateList.Add(sb.ToString());
                sb = new StringBuilder();

                var strArray = new string[separateList.Count];
                foreach (var item in separateList.Select((value, index) => new { value, index }))
                {
                    strArray[item.index] = item.value;
                }
                separateList = new List<string>();
                returnList.Add(strArray);
            }
        }
        return returnList;
    }
}
