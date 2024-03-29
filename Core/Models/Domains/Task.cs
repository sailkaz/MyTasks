﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTasks.Core.Models.Domains
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Pole tytuł jest wymagane.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Pole opis jest wymagane.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole kategoria jest wymagane.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Termin")]
        public DateTime? Term {get; set; }

        [Display(Name = "Zrealizowane")]
        public bool IsExecuted { get; set; }
         
        public string UserId { get; set; }


        public Category Category { get; set; }

        public ApplicationUser User { get; set; }
    }
} 
