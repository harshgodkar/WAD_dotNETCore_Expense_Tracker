using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int ExpenseCategoryId { get; set; }

        [Required]
        public string ExpenseCategoryName { get; set; }
    }
}
