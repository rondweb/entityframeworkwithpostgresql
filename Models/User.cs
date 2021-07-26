using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entityFramework.Models
{
    [Index(nameof(Id))]
    [Table("tb_user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(256)")]
        public string Email { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(256)")]
        public string Name { get; set; }

        [Required]
        [Column("address", TypeName = "varchar(2056)")]
        public string Address { get; set; }

        [Column("created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }

        [Column("updated")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
        
    }

}