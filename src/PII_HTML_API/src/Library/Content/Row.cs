using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace PII_HTML_API
{
    /// <summary>
    /// Representa una fila de la tabla.
    /// Se compone de celdas (Cell).
    /// </summary>
    public class Row : ITablePart
    {
        /// <typeparam name="Cell">Las celdas de la tabla.</typeparam>
        private List<Cell> cells = new List<Cell>();

        /// <value>Las celdas de la tabla enumerables.</value>
        public IEnumerable<Cell> Cells
        {
            get
            {
                return cells.AsEnumerable();
            }
        }

        public Row(List<Cell> cells)
        {
            this.cells = cells;
        }

        public void Accept(ITableVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}