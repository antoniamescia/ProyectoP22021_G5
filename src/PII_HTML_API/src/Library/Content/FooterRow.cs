using System.Collections.Generic;
using System.Linq;

namespace PII_HTML_API
{
    public class FooterRow : ITablePart
    {
        /// <typeparam name="HeaderCell">Las celdas del encabezado de la tabla.</typeparam>
        private List<FooterCell> footerCells = new List<FooterCell>();

        /// <value>Las celdas de la tabla enumerables.</value>
        public IEnumerable<FooterCell> FooterCells
        {
            get
            {
                return footerCells.AsEnumerable();
            }
        }
        public FooterRow(List<FooterCell> footerCells )
        {
            this.footerCells = footerCells;
        }

        public void Accept(ITableVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}