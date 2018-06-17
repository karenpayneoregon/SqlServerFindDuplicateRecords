namespace DuplicateRecordsProject.Classes
{
    /// <summary>
    /// This class defines methods for constructing a sql statement
    /// for locating duplicate records in a SQL-Server table.
    /// </summary>
    public abstract class SqlDuplicateBuilder
    {
        public abstract void CreateA();
        public abstract void CreateSelectPart();
        public abstract void CreateGroupPart();
        public abstract void CreateHavingPart();
        public abstract void CreateB();
        public abstract void OrderBy();
        public abstract DuplicateStatement GetResult();
    }

}
