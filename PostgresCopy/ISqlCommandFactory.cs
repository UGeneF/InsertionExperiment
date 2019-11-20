namespace PostgresCopy
{
    public interface ISqlCommandFactory
    {
        string GetSql<T>();
    }
}