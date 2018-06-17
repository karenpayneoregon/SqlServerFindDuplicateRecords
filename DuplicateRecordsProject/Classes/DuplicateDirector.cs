using UtilityLibrary;

namespace DuplicateRecordsProject.Classes
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
            SqlDuplicateBuilder builder = new ConcreteSqlDuplicateBuilder(container);
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
            builder.CreateA();
            builder.CreateSelectPart();
            builder.CreateGroupPart();
            builder.CreateHavingPart();
            builder.CreateB();
            builder.OrderBy();
        }
    }
}
