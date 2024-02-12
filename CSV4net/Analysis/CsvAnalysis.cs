using System.Text;
using CSV4net.Model;

namespace CSV4net.Analysis;
public class CsvAnalysis
{
    private List<int> sortIndexList; 

    private List<List<DataModel>> columDataList;
    private Dictionary<string, int> keyDict;

    public CsvAnalysis(List<string[]> list)
    {
        columDataList = new List<List<DataModel>>();
        keyDict = new Dictionary<string, int>();

        foreach (var item in list.Select((value, index) => new { value, index }))
        {
            if(item.index == 0)
            {
                int keyIndex = 0;
                foreach(var data in item.value)
                {
                    columDataList.Add(new List<DataModel>());
                    keyDict.Add(data, keyIndex);
                    keyIndex++;
                }
            }
            else
            {
                int listIndex = 0;
                foreach(var data in item.value)
                {
                    var dataList = columDataList[listIndex];
                    dataList.Add(new DataModel(item.index, data));
                    listIndex++;
                }
            }
        }
    }

    public void SortEqualTextValue(string columName, string text)
    {
        List<int> indexList = new List<int>();
        int keyIndex = keyDict[columName];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            if(item.value.Data.ToString() == text)
            {
                indexList.Add(item.index);
            }
        }
        CompareSortIndex(indexList);
    }

    public void SortContainsTextValue(string columName, string text)
    {
        List<int> indexList = new List<int>();
        int keyIndex = keyDict[columName];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            if(item.value.Data.ToString().Contains(text))
            {
                indexList.Add(item.index);
            }
        }
        CompareSortIndex(indexList);
    }

    public void SortRangeIntValue(string columName, int max, int min)
    {
        List<int> indexList = new List<int>();
        int keyIndex = keyDict[columName];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            if(min <= int.Parse(item.value.Data.ToString()) && int.Parse(item.value.Data.ToString()) <= max)
            {
                indexList.Add(item.index);
            }
        }
        CompareSortIndex(indexList);
    }

    public void SortRangeDateTimeValue(string columName, DateTime startDateTime, DateTime endDateTime)
    {
        List<int> indexList = new List<int>();
        int keyIndex = keyDict[columName];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            if(startDateTime <= DateTime.Parse(item.value.Data.ToString()) && DateTime.Parse(item.value.Data.ToString()) <= endDateTime)
            {
                indexList.Add(item.index);
            }
        }
        CompareSortIndex(indexList);
    }

    private void CompareSortIndex(List<int> list)
    {
        if(sortIndexList == null)
        {
            sortIndexList = new List<int>(list);
        }
        else
        {
            List<int> tempList = new List<int>();
            foreach(int i in sortIndexList)
            {
                foreach(int v in list)
                {
                    if(i == v)
                    {
                        tempList.Add(v);
                    }
                }
            }
            sortIndexList = new List<int>(tempList);
        }
    }

    public List<string[]> GetSortData()
    {
        List<string[]> list = new List<string[]>();

        string[,] strArray = new string[sortIndexList.Count, keyDict.Count];
        foreach(var item in columDataList.Select((value, index) => new { value, index }))
        {
            int count = 0;
            foreach(int y in sortIndexList)
            {
                strArray[count, item.index] = item.value[y].Data.ToString();
                count++;
            }
        }

        for(int y=0;y<strArray.GetLength(0);y++)
        {
            string[] array = new string[strArray.GetLength(1)];
            for(int x=0;x<strArray.GetLength(1);x++)
            {
                array[x] = strArray[y, x];
            }
            list.Add(array);
        }

        return list;
    }
}