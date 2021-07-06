using System.Collections.Generic;
using System.Linq;

namespace PII_HTML_API
{
    public class HeaderRow : ITablePart
    {
        /// <typeparam name="HeaderCell">Las celdas del encabezado de la tabla.</typeparam>
        private List<HeaderCell> headerCells = new List<HeaderCell>();

        /// <value>Las celdas de la tabla enumerables.</value>
        public IEnumerable<HeaderCell> HeaderCells
        {
            get
            {
                return headerCells.AsEnumerable();
            }
        }

        public HeaderRow(List<HeaderCell> headerCells)
        {
            this.headerCells = headerCells;
        }

        public void Accept(ITableVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}