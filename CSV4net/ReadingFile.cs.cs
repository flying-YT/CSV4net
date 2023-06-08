using System.Text;

namespace CSV4net;
public class ReadingFile
{
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
