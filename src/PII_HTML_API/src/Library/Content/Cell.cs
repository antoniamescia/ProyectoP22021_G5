
namespace PII_HTML_API
{

    /// <summary>
    /// Representa una celda en una tabla
    /// </summary>
    public class Cell : ITablePart
    {
        /// <summary>
        /// Devuelve el contenido de la celda.
        /// </summary>
        /// <value></value>
        public string Content { get; private set; }

        public int ColumnSpan { get; private set; }
        public Cell(string content, int colspan = 1)
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