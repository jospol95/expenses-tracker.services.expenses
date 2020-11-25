using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Domain.Models
{
    [Table("category")]
    public class Category
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        
        [Column("budget_assigned", TypeName = "decimal(19,4)")]
        public decimal? BudgetAssigned { get; set; }
        
        public void Update(string name, string description, decimal budgetAssigned)
        {
            Name = name;
            Description = description;
            BudgetAssigned = budgetAssigned;
        }
        //testing pull requ

        public void Create(string name, string description, string userId, decimal? budgetAssigned)
        {
            Name = name;
            Description = description;
            UserId = userId;
            BudgetAssigned = budgetAssigned;
        }
    }
}