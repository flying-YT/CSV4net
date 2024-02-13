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
                    dataList.Add(new DataModel(item.index-1, data));
                    listIndex++;
                }
            }
        }
    }

    public void SortDescendingOrderIntValue(string columName)
    {
        int keyIndex = keyDict[columName];

        int[] indexArray = new int[columDataList[keyIndex].Count];
        int[] valueArray = new int[columDataList[keyIndex].Count];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            indexArray[item.index] = item.value.Index;
            valueArray[item.index] = int.Parse(item.value.Data.ToString());
        }

        for(int i=0;i<valueArray.Length;i++)
        {
            for(int j=0;j<valueArray.Length;j++)
            {
                if (valueArray[i] > valueArray[j])
                {
                    int tempValue = valueArray[j];
                    valueArray[j] = valueArray[i];
                    valueArray[i] = tempValue;

                    int tempIndex = indexArray[j];
                    indexArray[j] = indexArray[i];
                    indexArray[i] = tempIndex;
                }
            }
        }

        CompareSortIndex(IntArrayToList(indexArray), true);
    }

    public void SortAscendingOrderIntValue(string columName)
    {
        int keyIndex = keyDict[columName];

        int[] indexArray = new int[columDataList[keyIndex].Count];
        int[] valueArray = new int[columDataList[keyIndex].Count];
        foreach(var item in columDataList[keyIndex].Select((value, index) => new { value, index }))
        {
            indexArray[item.index] = item.value.Index;
            valueArray[item.index] = int.Parse(item.value.Data.ToString());
        }

        for(int i=0;i<valueArray.Length;i++)
        {
            for(int j=0;j<valueArray.Length;j++)
            {
                if (valueArray[i] < valueArray[j])
                {
                    int tempValue = valueArray[j];
                    valueArray[j] = valueArray[i];
                    valueArray[i] = tempValue;

                    int tempIndex = indexArray[j];
                    indexArray[j] = indexArray[i];
                    indexArray[i] = tempIndex;
                }
            }
        }

        CompareSortIndex(IntArrayToList(indexArray), true);
    }

    private List<int> IntArrayToList(int[] array)
    {
        List<int> indexList = new List<int>();
        foreach(int i in array)
        {
            indexList.Add(i);
        }
        return indexList;
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

    private void CompareSortIndex(List<int> list, bool isIndexUpdate=false)
    {
        if(sortIndexList == null)
        {
            sortIndexList = new List<int>(list);
            return;
        }
        
        List<int> tempList = new List<int>();
        if(isIndexUpdate)
        {
            foreach(int i in list)
            {
                foreach(int v in sortIndexList)
                {
                    if(i == v)
                    {
                        tempList.Add(i);
                    }
                }
            }
        }
        else
        {
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
        }
        sortIndexList = new List<int>(tempList);
    }

    public void ResetSortIndex()
    {
        sortIndexList = new List<int>();
        for(int i=0;i<columDataList[0].Count;i++)
        {
            sortIndexList.Add(i);
        }
    }

    public List<string[]> GetSortData()
    {
        if(sortIndexList == null)
        {
            ResetSortIndex();
        }

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