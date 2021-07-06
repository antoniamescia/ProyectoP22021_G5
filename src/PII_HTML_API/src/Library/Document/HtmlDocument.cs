using System;
using System.IO;
using System.Linq;
using Aspose.Html;
using Aspose.Html.Saving;
using Aspose.Html.Services;

namespace PII_HTML_API
{
    /// <summary>
    /// Representa un documento de Word.
    /// 
    /// Permite agregar contenido en distintos formatos (texto y tabla).
    /// </summary>
    public class HtmlDocument
    {
        /// <summary>
        /// La ruta al archivo.
        /// </summary>
        private string path;
        private string title;

        /// <param name="path">La ruta del archivo</param>
        public HtmlDocument(string path, string title)
        {
            this.path = path;
            this.title = title;
        }

        /// <summary>
        /// Si el documento no existe, se crea automáticamente.
        /// </summary>
        /// <param name="content"></param>
        public void AddContent(IContent content)
        {
            HTMLDocument document = null;
            try
            {

                document = File.Exists(this.path)
                    ? new HTMLDocument(this.path)
                        : new HTMLDocument();



                document.Title = this.title;

                content.Add(document);

                document.Save(this.path);
            }
            finally
            {
                if (document != null)
                {
                    document.Dispose();
                }
            }
        }

    }
}