namespace KipuHealthCodeAssessment.County
{
    class County : ICounty
    {
        private County()
        { }

        public County(string name, decimal salesTax, decimal markup)
        {
            Name = name;
            SalesTax = salesTax;
            MarkUp = markup;
        }

        public string Name { get; }
        public decimal SalesTax { get; }
        public decimal MarkUp { get; }
    }
}
