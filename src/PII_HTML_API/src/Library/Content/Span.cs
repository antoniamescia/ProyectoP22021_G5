using System.IO;
using Aspose.Html;
using Aspose.Html.Dom;

namespace PII_HTML_API
{
    /// <summary>
    /// Representa una porción de contenido de texto.
    /// </summary>
    public class Span : IContent
    {
        /// <summary>
        /// El contenido.
        /// </summary>
        private string content;

        public Span(string content)
        {
            this.content = content;
        }

        /// <summary>
        /// Agrega el contenido al cuerpo del documento indicado en la posición
        /// indicada.
        /// </summary>
        /// <param name="body">El cuerpo del documento</param>
        public void Add(HTMLDocument document)
        {
            Element element = document.CreateElement("span");
            element.TextContent = this.content;
            document.Body.AppendChild(element);
        }
    }
}