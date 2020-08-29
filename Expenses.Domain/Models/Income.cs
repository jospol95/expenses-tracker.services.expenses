using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Domain.Models
{
    [Table(("income"))]
    public class Income
    {
        [Column("id")]
        [Key]
        public string Id{get;set;}
        
        [Column("title")]
        public string Title{get;set;}
        
        [Column("amount", TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }
        
        [Column("date")]
        public DateTime Date{get; set;}
        
        // [Column("category_id")]
        // public int CategoryId{get;set;}
        
        [Column("user_id")]
        public string UserId{get; set;}
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("account_id")]
        public int? AccountId {get; set;}


        public Income(string id, string title, decimal amount, DateTime date, 
            string userId, string description, int? accountId)
        {
            Id = id;
            Title = title;
            Amount = amount;
            Date = date;
            UserId = userId;
            Description = description;
            AccountId = accountId;
        }
    }
}