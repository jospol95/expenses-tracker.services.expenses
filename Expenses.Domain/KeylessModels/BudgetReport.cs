using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Domain.KeylessModels
{
    // [Keyless]
    public class BudgetReport
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("month")]
        public int Month { get; set; }
        
        [Column("year")]
        public int Year { get; set; }
        
        [Column("total")]
        public decimal Total { get; set; }
        
        [Column("user_id")]
        public string UserId { get; set; }
    }
}