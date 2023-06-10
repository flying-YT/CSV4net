using System.Text;

namespace CSV4net;
public class ReadingFile
{
    private static List<string> ReadFile(string path)
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        return list;
    }

    public static List<string[]> GetCSV(string path, char sepalate=',', string encoding="utf-8")
    {
        var list = ReadFile(path);
        var returnList = new List<string[]>();
        var sepalateList = new List<string>();
        StringBuilder sb = new StringBuilder();
        bool isBool = false;
        foreach(var line in list)
        {
            foreach(char c in line)
            {
                if(c == sepalate && !isBool)
                {  
                    sepalateList.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                else
                {
                    sb.Append(c);
                    if(c == '"')
                    {
                        isBool = !isBool;
                    }
                }
            }

            if(sb.Length != 0 && !isBool)
            {
                sepalateList.Add(sb.ToString());
                sb = new StringBuilder();

                var strArray = new string[sepalateList.Count];
                int index = 0;
                foreach(var str in sepalateList)
                {
                    strArray[index] = str;
                    index++;
                }
                sepalateList = new List<string>();
                returnList.Add(strArray);
            }
        }
        return returnList;
    }

/*
    public static List<string> GetCSV(string path)
    {
        bool isNormal = true;
        using (StreamReader sr = new StreamReader(path))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if(line.Contains("\",\""))
                {
                    isNormal = false;
                }
                else
                {
                    isNormal = true;
                }
                break;
            }
        }

        if(isNormal)
        {
            return NormalCSV(path);
        }
        else
        {
            return DoubleQuotationCSV(path);
        } 
    }
*/
    private static List<string> NormalCSV(string path)
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        return list;
    }

    private static List<string> DoubleQuotationCSV(string path)
    {
        List<string> list = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line = "";
            StringBuilder sb = new StringBuilder();
            while ((line = sr.ReadLine()) != null)
            {
                if(sb.Length == 0)
                {
                    sb.Append(line);
                }
                else
                {
                    if(line[0] != '"')
                    {
                        sb.Append(line);
                    }
                    else
                    {
                        list.Add(sb.ToString());
                        sb = new StringBuilder(line);
                    }
                }
            }

            if(sb.Length != 0)
            {
                list.Add(sb.ToString());
            }
        }
        return list;
    }
}
