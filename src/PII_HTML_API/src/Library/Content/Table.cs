using System.Linq;
using System.Collections.Generic;
using Aspose.Html;

namespace PII_HTML_API
{
    /// <summary>
    /// Representa una tabla de contenido.
    /// Se compone de filas (TableContentRow).
    /// </summary>
    public class Table : IContent, ITablePart
    {
        /// <typeparam name="Row">Las filas de la tabla.</typeparam>
        private List<Row> rows;

        /// <summary>
        /// Devuelve la fila del encabezado de la tabla.
        /// </summary>
        /// <value></value>
        public HeaderRow HeaderRow { get; private set; }


        /// <summary>
        /// Devuelve la fila del pie de la tabla.
        /// </summary>
        /// <value></value>
        public FooterRow FooterRow { get; private set; }


        /// <value>Las filas de la table enumerables.</value>
        public IEnumerable<Row> Rows
        {
            get
            {
                return rows.AsEnumerable();
            }
        }

        public Table(HeaderRow headerRow, List<Row> rows, FooterRow footerRow)
        {
            this.HeaderRow = headerRow;
            this.rows = rows;
            this.FooterRow = footerRow;
        }

        /// <summary>
        /// Agrega la tabla a un cuerpo de documento
        /// </summary>
        /// <param name="document">El cuerpo del documento</param>

        public void Add(HTMLDocument document)
        {
            TableVisitor visitor = new TableVisitor(document);
            this.Accept(visitor);
            document.Body.AppendChild(visitor.Table);

        }

        public void Accept(ITableVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}