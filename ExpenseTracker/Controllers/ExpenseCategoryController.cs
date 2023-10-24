using ExpenseTracker.AppDbContext;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ExpenseCategoryController : Controller
    {
        ExpensesDataAcessLayer objexpense;

        public ExpenseCategoryController(ExpensesDataAcessLayer objexpense)
        {
            this.objexpense = objexpense;
        }

        public IActionResult Index()
        {
            
            List<ExpenseCategory> listExCat = new List<ExpenseCategory>();
            listExCat = objexpense.GetAllExpenseCategories().ToList();
            return View(listExCat);
        }

        public IActionResult Create(ExpenseCategory expenseCategory)
        {
            if(ModelState.IsValid)
            {
                objexpense.AddExpenseCategory(expenseCategory);
                return RedirectToAction("Index");
            }

            return View(expenseCategory);
        }

        public IActionResult GetExpenseCategoryForUpdate(int Id)
        {
            var exepensecat = objexpense.GetExpenseCatData(Id);
            if(exepensecat == null)
            {
                return NotFound();
            }
            return View(exepensecat);
        }

        [HttpPost]
        public IActionResult Update(ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                objexpense.UpdateExpenseCategory(expenseCategory);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                objexpense.DeleteExpenseCategoory(Id);
                return RedirectToAction("Index");
            }
            return View() ;
        }

    }
}
