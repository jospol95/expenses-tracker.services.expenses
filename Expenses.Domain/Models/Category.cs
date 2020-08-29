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
        private string Name { get; set; }
        [Column("description")]
        private string Description { get; set; }
        [Column("user_id")]
        private string UserId { get; set; }
        
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
        //testing pull requ

        public void Create(string name, string description, string userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}