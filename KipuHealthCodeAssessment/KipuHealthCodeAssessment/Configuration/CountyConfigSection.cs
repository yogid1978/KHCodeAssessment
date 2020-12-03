using System.Configuration;

namespace KipuHealthCodeAssessment.Configuration
{
    public sealed class CountyConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("taxRate")]
        public decimal TaxRate
        {
            get => (decimal)this["taxRate"];
        }

        [ConfigurationProperty("markUp")]
        public decimal MarkUp
        {
            get => (decimal)this["markUp"];
        }
    }
}
