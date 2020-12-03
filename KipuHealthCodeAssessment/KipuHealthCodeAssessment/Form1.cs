using KipuHealthCodeAssessment.Configuration;
using KipuHealthCodeAssessment.County;
using KipuHealthCodeAssessment.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KipuHealthCodeAssessment
{
    public partial class Form1 : Form
    {
        IList<Counties> countyList;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcProfit_Click(object sender, EventArgs e)
        {
            countyList = new List<Counties>();
            if (ValidateInput())
                CalculateProfit(tboxBrandName.Text, tboxProductName.Text, tboxCostPrice.Text, tboxUnitsSold.Text);
        }

        private bool ValidateInput()
        {
            if (cboxMiamiDade.Checked)
                countyList.Add(Counties.MiamiDade);
            if (cboxPalmBeach.Checked)
                countyList.Add(Counties.PalmBeach);
            if (cboxBroward.Checked)
                countyList.Add(Counties.Broward);

            if (countyList.Count == 0)
            {
                MessageBox.Show("No counties have been selected for sale.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tboxCostPrice.Text) && string.IsNullOrWhiteSpace(tboxUnitsSold.Text))
            {
                MessageBox.Show("Cost Price and Units Sold are required fields.");
                return false;
            }

            return true;
        }

        private void CalculateProfit(string brandName, string productName, string costPrice, string units)
        {
            tboxTransactionReport.Text = "";
            int unitsSold = 0;
            unitsSold = int.TryParse(units, out unitsSold) ? int.Parse(units) : 0;

            decimal productCostPrice = 0m;
            Product product = new Product
            {
                BrandName = !string.IsNullOrWhiteSpace(brandName) ? brandName : "Default Brand Name",
                ProductName = !string.IsNullOrWhiteSpace(productName) ? productName : "Default Product Name",
                CostPrice = decimal.TryParse(costPrice, out productCostPrice) ? decimal.Round(decimal.Parse(costPrice), 2, MidpointRounding.AwayFromZero) : 0m
            };

            var totalCostPrice = 0m;
            var totalSalePrice = 0m;
            StringBuilder transactionReport = new StringBuilder();
            transactionReport.AppendLine(String.Format("St. Bernard SALE REPORT for product {0} and brand {1}. \n\n", product.ProductName, product.BrandName));

            try
            {
                foreach (Counties countyEnum in countyList)
                {
                    var transaction = Sale.ProcessTransactionByCounty(product, unitsSold,
                        SaleConfiguration.SaleConfigurationByCounty.Values.Where(county => county.Name == countyEnum.ToString()).First());
                    transactionReport.AppendLine(Sale.GenerateTransactionReportByCounty(countyEnum.ToString(), transaction));

                    totalCostPrice += transaction.totalCostPrice;
                    totalSalePrice += transaction.totalSalePrice;
                }
                
                var transactionProfit = Sale.CalculateTotalProfit(totalCostPrice, totalSalePrice);
                transactionReport.AppendLine(Sale.GenerateTotalProfitReport(totalCostPrice, totalSalePrice, product, transactionProfit));

                tboxTransactionReport.Text = transactionReport.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in processing the Sale transaction: " + ex.Message);
            }
        }
    }
}
