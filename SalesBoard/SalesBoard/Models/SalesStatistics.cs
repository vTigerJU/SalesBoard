using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel;

namespace SalesBoard.Models
{
    public class SalesStatistics
    {
        public int Id { get; set; }
        public double Commission { get; set; }
        [DisplayName("Commission Rate")]
        public double CommissionRate { get; set; }
        [DisplayName("Sales Made")]
        public int SalesMade    { get; set; }

    }
}
