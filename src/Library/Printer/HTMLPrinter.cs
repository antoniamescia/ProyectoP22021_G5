using System;
using System.Collections.Generic;
using PII_HTML_API;
using Aspose.Html;
using System.IO;

namespace BankerBot
{
    public class HTMLPrinter : IPrinter
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por IPrinter.
        Cumple con ISP porque solo implementa una interfaz (IPrinter).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con las responsabilidades otorgadas.
        Cumple con Polymorphism porque usa los métodos polimórficos PrintAccountBalance, PrintTransactions y PrintSavingsGoal.
        */

        public HTMLPrinter()
        {
        }

        private HeaderRow Header(List<Transaction> list)
        {
            HeaderRow header = new HeaderRow(
                    new List<HeaderCell>()
                    {
                        new HeaderCell("CurrencyType"),
                        new HeaderCell("Amount"),
                        new HeaderCell("Date"),
                        new HeaderCell("Description")
                    });

            return header;
        }
        private List<Row> Rows(List<Transaction> list)
        {
            List<Row> rows = new List<Row>();
            foreach (Transaction item in list)
            {
                List<Cell> cells = new List<Cell>();

                cells.Add(new Cell(item.Currency.Type));
                cells.Add(new Cell(item.Amount.ToString()));
                cells.Add(new Cell(item.Date.ToString("dd/MM/yyyy")));
                cells.Add(new Cell(item.Description));

                rows.Add(new Row(cells));
            }

            return rows;
        }

        public string Print(List<Transaction> list, string fileName)
        {

            string path = $@".\..\..\docs\{fileName}.html";
            //La idea era eliminar la informacion vieja del archivo antes de enviarlo
            // if (File.Exists(path))
            // {
            //     File.WriteAllText(path, "");
            // }
            HtmlDocument doc = new HtmlDocument(path, "Transaction Record");
            doc.AddContent(new Span("Transaction Record"));
            doc.AddContent(new Table(

                Header(list),

                Rows(list),

                new FooterRow(
                new List<FooterCell>() {
                    new FooterCell("")
                })
            ));
            return path;
        }
    }
}
