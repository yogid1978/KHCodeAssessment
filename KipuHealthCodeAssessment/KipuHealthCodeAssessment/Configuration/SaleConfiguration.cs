using KipuHealthCodeAssessment.County;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KipuHealthCodeAssessment.Configuration
{
    static class SaleConfiguration
    {
        public static Dictionary<Counties, ICounty> SaleConfigurationByCounty { get; private set; }

        static SaleConfiguration()
        {
            SetupSaleConfigurationByCounty();
        }

        private static void SetupSaleConfigurationByCounty()
        {
            string countyName;
            decimal taxRate, markUp;
            SaleConfigurationByCounty = new Dictionary<Counties, ICounty>();

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            IEnumerable<CountyConfigSection> sections = from value in config.Sections.OfType<CountyConfigSection>()
                                                        select value;

            foreach (CountyConfigSection countyConfiguration in sections)
            {
                countyName = countyConfiguration.SectionInformation.Name;
                taxRate = countyConfiguration.TaxRate;
                markUp = countyConfiguration.MarkUp;

                if (Enum.TryParse(countyName, out Counties county))
                    SaleConfigurationByCounty.Add(county, new County.County(countyName, taxRate, markUp));
            }
        }
    }
}
