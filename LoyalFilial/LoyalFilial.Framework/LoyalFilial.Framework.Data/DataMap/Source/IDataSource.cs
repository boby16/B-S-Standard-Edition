namespace LoyalFilial.Framework.Data.DataMap.Source
{
    public interface IDataSource
    {
        bool HasField(string fieldName);

        object GetFieldValue(string fieldName);
    }
}
