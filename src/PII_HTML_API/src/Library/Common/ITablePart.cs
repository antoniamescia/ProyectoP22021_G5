namespace PII_HTML_API
{
    public interface ITablePart 
    {
        void Accept(ITableVisitor visitor);
    }
}