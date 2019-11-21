namespace PostgresCopy
{
    public interface ISqlFactory
    {
        string GetSql<T>();
    }
}