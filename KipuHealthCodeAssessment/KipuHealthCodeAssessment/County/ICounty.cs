namespace KipuHealthCodeAssessment.County
{
    public interface ICounty
    {
        string Name { get; }
        decimal SalesTax { get; }
        decimal MarkUp { get; }
    }
}
