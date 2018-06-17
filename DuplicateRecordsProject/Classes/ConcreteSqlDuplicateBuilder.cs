using System.Collections.Generic;
using System.Linq;
using UtilityLibrary;

namespace DuplicateRecordsProject.Classes
{
    public class ConcreteSqlDuplicateBuilder : SqlDuplicateBuilder
    {
        private readonly DuplicateStatement _item = new DuplicateStatement();
        private readonly List<SqlColumn> _sqlColumnList;
        private string _tableName;
        private string _orderBy;

        public ConcreteSqlDuplicateBuilder(DuplicateItemContainer pColumnsInformation)
        {
            _sqlColumnList = pColumnsInformation.Columns;
            _tableName = _sqlColumnList.First().TableName;
            _orderBy = pColumnsInformation.OrderBy;
        }

        public override void CreateA()
        {
            _item.Add($"SELECT A.* FROM {_tableName} A INNER JOIN (SELECT ");
        }
        public override void CreateSelectPart()
        {
            _item.Add(string.Join(",", _sqlColumnList.Select(col => col.ColumnName).ToArray()) + $" FROM {_tableName} ");
        }
        public override void CreateGroupPart()
        {
            _item.Add("GROUP BY " + string.Join(",", _sqlColumnList.Select(col => col.ColumnName).ToArray()) + " ");
        }
        public override void CreateHavingPart()
        {
            _item.Add("HAVING COUNT(*) > 1");
        }
        public override void CreateB()
        {
            var joined = _sqlColumnList.Select(item => $"A.{item.ColumnName} = B.{item.ColumnName}").ToArray().JoinCondition();
            _item.Add(") B ON " + joined);
        }
        public override void OrderBy()
        {
            if (!string.IsNullOrWhiteSpace(_orderBy))
            {
                if (_orderBy != "None")
                {
                    _item.Add($" ORDER BY {_orderBy}");
                }               
            }
        }

        public override DuplicateStatement GetResult()
        {
            return _item;
        }
    }
}
