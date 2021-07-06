
namespace PII_HTML_API
{
    public interface ITableVisitor
    {
        void Visit(Table table);

        void Visit(HeaderRow header);

        void Visit(Row row);

        void Visit(Cell cell);

        void Visit(HeaderCell headerCell);
        
        void Visit(FooterRow footerRow);
        
        void Visit(FooterCell footerCell);
    }
}