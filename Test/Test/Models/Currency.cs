using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyMvcApi.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "numeric(18,2)")]
        public decimal Value { get; set; }

        [Required]
        public DateTime A_Date { get; set; }
    }
}