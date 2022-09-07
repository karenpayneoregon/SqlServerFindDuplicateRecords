namespace UtilityLibrary
{
    public class SqlColumn
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnNameBracketed => $"[{ColumnName}]";
        public bool IsIdentity { get; set; }
        public bool HasSpaces { get; set; }
        public override string ToString()
        {
            return ColumnName + (IsIdentity ? " (pk)" : "");
        }
    }
}
