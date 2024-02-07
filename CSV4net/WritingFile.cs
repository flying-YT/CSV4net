using System.Text;

namespace CSV4net;
public class WritingFile
{
    public static string version = "1.1";

    public static void WriteFile(List<string> list, string path, string encoding="utf-8")
    {
        using(StreamWriter sr = new StreamWriter(path, false, Encoding.GetEncoding(encoding)))
        {
            foreach(string str in list)
            {
                sr.WriteLine(str);
            }
        }
    }

    public static void WriteCSV(List<string[]> list, string path, bool isDoubleQuotation=false, char separate=',', string encoding="utf-8")
    {
        list<string> writeList = new list<string>();
        foreach (string[] strArray in list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string str in strArray)
            {
                if(isDoubleQuotation)
                {
                    sb.Append("\"" + str + "\"" + separate);
                }
                else
                {
                    if(str.Contains("\r\n") || str.Contains("\n"))
                    {
                        sb.Append("\"" + str + "\"" + separate);
                    }
                    else
                    {
                        sb.Append(str + separate);
                    }
                }
            }
            writeList.Add(sb.ToString().[..^1]);
        }
        WriteFile(writeList, path, encoding);
    }

}