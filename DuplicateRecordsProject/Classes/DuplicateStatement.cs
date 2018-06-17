using System.Collections.Generic;
using System.Text;

namespace DuplicateRecordsProject.Classes
{
    public class DuplicateStatement
    {
        private readonly List<string> _parts = new List<string>();
        private StringBuilder _statement;
        /// <summary>
        /// Used to add each part of the SQL statement one part
        /// at a time.
        /// </summary>
        /// <param name="part"></param>
        public void Add(string part)
        {
            _parts.Add(part);
        }
        /// <summary>
        /// Connect all the parts together
        /// </summary>
        public void FinishedStatement()
        {
            _statement = new StringBuilder();
            foreach (string part in _parts)
                _statement.Append(part);
        }
        /// <summary>
        /// This is the complete SQL statement
        /// </summary>
        public string Statement => _statement.ToString();
    }
}
