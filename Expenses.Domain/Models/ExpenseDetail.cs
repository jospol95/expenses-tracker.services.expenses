using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Domain.Models
{
    [Table(("expense_detail"))]
    public class ExpenseDetail
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ExpenseId { get; set; }
    }
}