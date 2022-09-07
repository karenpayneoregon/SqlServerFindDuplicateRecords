using System.Collections.Generic;
using System.Linq;
using UtilityLibrary;

namespace SupportLibrary
{
    public class ConcreteSqlDuplicateBuilder : SqlDuplicateBuilder
    {
        private readonly DuplicateStatement _item = new DuplicateStatement();
        private readonly List<SqlColumn> _sqlColumnList;
        private readonly string _tableName;
        private readonly string _orderBy;

        public ConcreteSqlDuplicateBuilder(DuplicateItemContainer pColumnsInformation)
        {
            _sqlColumnList = pColumnsInformation.Columns;
            _tableName = _sqlColumnList.First().TableName;
            _orderBy = pColumnsInformation.OrderBy;
        }

        public override void CreateInnerJoin()
        {
            _item.Add($"SELECT A.* FROM {_tableName} A INNER JOIN (SELECT ");
        }
        public override void CreateSelectStatement()
        {
            _item.Add(string.Join(",", _sqlColumnList.Select(col => col.ColumnNameBracketed).ToArray()) + $" FROM {_tableName} ");
        }
        public override void CreateGroup()
        {
            _item.Add("GROUP BY " + string.Join(",", _sqlColumnList.Select(col => col.ColumnNameBracketed).ToArray()) + " ");
        }
        public override void CreateHaving()
        {
            _item.Add("HAVING COUNT(*) > 1");
        }
        public override void CreateInnerSelect()
        {
            var joined = _sqlColumnList.Select(item => $"A.{item.ColumnNameBracketed} = B.{item.ColumnNameBracketed}").ToArray().JoinCondition();
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