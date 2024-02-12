namespace CSV4net.Model;
public class DataModel
{
    public int Index { set; get; }
    public object Data { set; get; }

    public DataModel(int index, object data)
    {
        Index = index;
        Data = data;
    }
}