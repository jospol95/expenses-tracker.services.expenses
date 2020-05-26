using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Domain.Models
{
    [Table(("expense"))]
    public class Expense
    {
        [Column("id")]
        [Key]
        public string Id{get;set;}
        
        [Column("title")]
        public string Title{get;set;}
        
        [Column("full_date")]
        public DateTime FullDate{get;set;}
        
        [Column("category_id")]
        public int CategoryId{get;set;}
        
        [Column("user_id")]
        public string UserId{get;set;}
        
        [Column("is_paid")]
        public Boolean Paid { get; set; }

        public Expense (string title, DateTime fullDate, int categoryId, string userId)
        {
            Id = Guid.NewGuid().ToString();
            FullDate = fullDate;
            CategoryId = categoryId;
            UserId = userId;
        }

        public void SetPaidStatus(Boolean paid)
        {
            Paid = paid;
        }

        // public string ExpenseDetailId{get;set;}
    }
}