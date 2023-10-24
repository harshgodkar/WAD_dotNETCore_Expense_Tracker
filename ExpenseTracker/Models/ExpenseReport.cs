using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace ExpenseTracker.Models
{
    public class ExpenseReport
    {
        [Key]
        public int ItemId { get; set; }
        
        [Required(ErrorMessage = "Please Enter expense given to details")]
        [MinLength(3, ErrorMessage = "The name of the expense given to is to SHORT.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please Enter Expense Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Please Enter a valid Expense Amount.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="Please Select Expense Date.")]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        [Display(Name = "Expense Category")]
        public int ExpenseCategoryId { get; set; }

        [ForeignKey("ExpenseCategoryId")]
        public virtual ExpenseCategory? Category { get; set; }
    }
}
