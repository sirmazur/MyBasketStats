﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBasketStats.API.Entities
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SalaryInUsd { get; set; }
        public ICollection<ContractSeason> ContractSeasons { get; set; } = new List<ContractSeason>();
    }
}
