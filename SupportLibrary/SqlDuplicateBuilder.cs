namespace SupportLibrary
{
    /// <summary>
    /// This class defines methods for constructing a sql statement
    /// for locating duplicate records in a SQL-Server table.
    /// </summary>
    public abstract class SqlDuplicateBuilder
    {
        public abstract void CreateInnerJoin();
        public abstract void CreateSelectStatement();
        public abstract void CreateGroup();
        public abstract void CreateHaving();
        public abstract void CreateInnerSelect();
        public abstract void OrderBy();
        public abstract DuplicateStatement GetResult();
    }

}
