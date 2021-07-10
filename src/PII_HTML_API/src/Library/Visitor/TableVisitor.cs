using System;
using Aspose.Html;
using Aspose.Html.Dom;
using Aspose.Html.Dom.Css;

namespace PII_HTML_API
{
    /// <summary>
    /// Visita una tabla de contenido para construir el elemento
    /// Tabla de Word.
    /// </summary>
    internal class TableVisitor : ITableVisitor
    {
        private HTMLDocument document;
        private Element currentRow;

        internal TableVisitor(Element table)
        {
            this.Table = table;

        }
        public Element Table { get; }

        public TableVisitor(HTMLDocument document)
        {
            this.document = document;
            this.Table = this.document.CreateElement("table");
            ICSSStyleDeclaration style = ((HTMLTableElement)this.Table).Style;
            style.Width = "100%";
            style.TextAlign = "left";
            style.Border = "1px solid #1C6EA4";
        }


        /// <summary>
        /// Construye la tabla (la raíz).
        /// </summary>
        /// <param name="table">La tabla de contenido.</param>
        public void Visit(Table table)
        {
            table.HeaderRow.Accept(this);

            foreach (var row in table.Rows)
            {
                row.Accept(this);
            }

            table.FooterRow.Accept(this);
        }

        /// <summary>
        /// Construye una fila de la tabla.
        /// </summary>
        /// <param name="row"></param>
        public void Visit(Row row)
        {
            HTMLTableRowElement tableRow = (HTMLTableRowElement)this.document.CreateElement("tr");
            this.currentRow = tableRow;

            foreach (var cell in row.Cells)
            {
                cell.Accept(this);
            }

            this.Table.AppendChild(tableRow);
        }

        /// <summary>
        /// Construye una celda de la tabla y la agrega a la
        /// fila actual.
        /// </summary>
        /// <param name="cell"></param>
        public void Visit(Cell cell)
        {
            HTMLTableCellElement tableCell = (HTMLTableCellElement)this.document.CreateElement("td");
            ICSSStyleDeclaration style = tableCell.Style;
            style.Border = "1px solid grey";
            style.Padding = "3px 2px";
            style.FontSize = "13px";

            tableCell.SetAttribute("colspan", cell.ColumnSpan.ToString());
            tableCell.TextContent = cell.Content;
            this.currentRow.AppendChild(tableCell);
        }


        /// <summary>
        /// Construye una fila del encabezado de la tabla.
        /// </summary>
        /// <param name="headerRow"></param>

        public void Visit(HeaderRow headerRow)
        {

            Element tableHead = this.document.CreateElement("thead");
            ICSSStyleDeclaration style = ((HTMLElement)tableHead).Style;
            style.BackgroundColor = "#1C6EA4";
            style.BorderBottom = "2px solid #444444";
            style.FontSize = "15px";
            style.FontWeight = "bold";
            style.Color = "#FFFFFF";
            style.BorderLeft = "2px solid #D0E4F5";

            Element tableHeadRow = this.document.CreateElement("tr");
            tableHead.AppendChild(tableHeadRow);
            this.Table.AppendChild(tableHead);
            this.currentRow = tableHeadRow;

            foreach (var headerCell in headerRow.HeaderCells)
            {
                headerCell.Accept(this);
            }
        }

        /// <summary>
        /// Construye una celda del encabezado de la tabla y la agrega a la
        /// fila del encabezado.
        /// </summary>
        /// <param name="headerCell"></param>
        public void Visit(HeaderCell headerCell)
        {
            Element tableHeaderCell = this.document.CreateElement("th");
            ICSSStyleDeclaration style = ((HTMLElement)tableHeaderCell).Style;
            style.Border = "1px solid #AAAAAA";
            style.Padding = "3px 2px";
            tableHeaderCell.TextContent = headerCell.Content;
            tableHeaderCell.SetAttribute("colspan", headerCell.ColumnSpan.ToString());
            this.currentRow.AppendChild(tableHeaderCell);
        }

        public void Visit(FooterRow footerRow)
        {
            Element tableFoot = this.document.CreateElement("tfoot");
            ICSSStyleDeclaration style = ((HTMLElement)tableFoot).Style;
            style.FontSize = "15px";
            style.FontWeight = "bold";
            style.BorderTop = "2px solid #444444";
            style.Background = "#D0E4F5";

            Element tableFootRow = this.document.CreateElement("tr");
            tableFoot.AppendChild(tableFootRow);
            this.Table.AppendChild(tableFoot);
            this.currentRow = tableFootRow;

            foreach (var footerCell in footerRow.FooterCells)
            {
                footerCell.Accept(this);
            }
        }

        public void Visit(FooterCell footerCell)
        {
            HTMLTableCellElement tableCell = (HTMLTableCellElement)this.document.CreateElement("td");
            ICSSStyleDeclaration style = tableCell.Style;
            style.Border = "1px solid grey";
            style.Padding = "3px 2px";
            style.FontSize = "13px";
            tableCell.SetAttribute("colspan", footerCell.ColumnSpan.ToString());

            tableCell.TextContent = footerCell.Content;
            this.currentRow.AppendChild(tableCell);
        }
    }
}