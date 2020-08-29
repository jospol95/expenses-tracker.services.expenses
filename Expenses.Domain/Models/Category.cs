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
    }
}