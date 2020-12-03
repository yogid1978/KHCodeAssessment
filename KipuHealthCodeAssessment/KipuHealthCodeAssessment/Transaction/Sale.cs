using KipuHealthCodeAssessment.County;
using System;
using System.Text;

namespace KipuHealthCodeAssessment.Transaction
{
    class Sale
    {
        public static (decimal totalCostPrice, decimal totalSalePrice, decimal totalSalePriceWithTax, decimal profit) 
            ProcessTransactionByCounty(Product product, int unitsSold, ICounty county)
        {
            var result = (totalCostPrice: 0m, totalSalePrice: 0m, totalSalePriceWithTax: 0m, totalSaleProfit: 0m);
            result.totalCostPrice = decimal.Round(product.CostPrice * unitsSold, 2, MidpointRounding.AwayFromZero);
            result.totalSalePrice = decimal.Round(result.totalCostPrice * (1 + (county.MarkUp / 100)), 2, MidpointRounding.AwayFromZero);
            result.totalSalePriceWithTax = decimal.Round(result.totalSalePrice * (1 + (county.SalesTax / 100)), 2, MidpointRounding.AwayFromZero);
            result.totalSaleProfit = decimal.Round(result.totalSalePrice - result.totalCostPrice, 2, MidpointRounding.AwayFromZero);
            return result;
        }

        public static string GenerateTransactionReportByCounty(string countyName, (decimal totalCostPrice, decimal totalSalePrice, decimal totalSalePriceWithTax, decimal profit) transaction)
        {
            StringBuilder countyTransaction = new StringBuilder();
            countyTransaction.Append("County " + countyName + ": ");
            countyTransaction.Append("Total CostPrice " + transaction.totalCostPrice + ", ");
            countyTransaction.Append("Total SalePrice " + transaction.totalSalePrice + ", ");
            countyTransaction.Append("Total SalePrice plus Tax " + transaction.totalSalePriceWithTax + ", ");
            countyTransaction.Append("Profit " + transaction.profit + "\n");

            return countyTransaction.ToString();
        }

        public static (decimal totalProfit, decimal totalProfitPercent) CalculateTotalProfit(decimal totalCostPrice, decimal totalSalePrice)
        {
            var result = (totalProfit: 0m, totalProfitPercent: 0);
            result.totalProfit = totalSalePrice - totalCostPrice;
            result.totalProfitPercent = (int)((result.totalProfit / totalCostPrice) * 100);
            return result;
        }

        public static string GenerateTotalProfitReport(decimal totalCostPrice, decimal totalSalePrice, Product product,
            (decimal totalProfit, decimal totalProfitPercent) transaction)
        {
            StringBuilder totalTransaction = new StringBuilder();          

            totalTransaction.Append("Total Sale: ");
            totalTransaction.Append("Total CostPrice " + totalCostPrice + ", ");
            totalTransaction.Append("Total SalePrice " + totalSalePrice + ", ");
            totalTransaction.Append("Total Profit " + transaction.totalProfit + ", ");
            totalTransaction.Append("Total Profit Percent " + transaction.totalProfitPercent + "%");

            return totalTransaction.ToString();
        }
    }
}
