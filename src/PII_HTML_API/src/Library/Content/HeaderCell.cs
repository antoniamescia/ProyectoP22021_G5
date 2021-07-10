
namespace PII_HTML_API
{

    /// <summary>
    /// Representa una celda en una en el encabezado de la tabla
    /// </summary>
    public class HeaderCell : ITablePart
    {
        /// <summary>
        /// Devuelve el contenido de la celda.
        /// </summary>
        /// <value></value>
        public string Content { get; private set; }
        public int ColumnSpan { get; private set; }
        public HeaderCell(string content, int colspan = 1)
        {
            this.Content = content;
            this.ColumnSpan = colspan;
        }


        /// <summary>
        /// Permite que la clase sea "visitada" por un ITableVisitor
        /// </summary>
        /// <param name="visitor"></param>
        public void Accept(ITableVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}