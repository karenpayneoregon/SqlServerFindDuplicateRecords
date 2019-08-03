using UtilityLibrary;

namespace SupportLibrary
{
    /// <summary>
    /// Top level for creating a duplicate statement
    /// </summary>
    public class DuplicateManager
    {
        public string Statement { get; }
        public DuplicateManager(DuplicateItemContainer container)
        {

            var director = new DuplicateDirector();
            var builder = new ConcreteSqlDuplicateBuilder(container);

            director.Construct(builder);

            DuplicateStatement duplicateStatement = builder.GetResult();

            duplicateStatement.FinishedStatement();
            Statement =  duplicateStatement.Statement;
        }
    }
    /// <summary>
    /// Responsible for constructing the duplicate statement in parts
    /// </summary>
    public class DuplicateDirector
    {
        public void Construct(SqlDuplicateBuilder builder)
        {
            builder.CreateInnerJoin();
            builder.CreateSelectStatement();
            builder.CreateGroup();
            builder.CreateHaving();
            builder.CreateInnerSelect();
            builder.OrderBy();
        }
    }
}
