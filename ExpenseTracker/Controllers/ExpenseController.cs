using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.AppDbContext;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        ExpensesDataAcessLayer objexpense;

        public ExpenseController(ExpensesDataAcessLayer objexpense)
        {
            this.objexpense = objexpense;
        }

        public IActionResult Index(string searchString)
        {
            IEnumerable<ExpenseReport> expenseReports = objexpense.GetAllExpenses();

            if (!String.IsNullOrEmpty(searchString))
            {
                expenseReports = objexpense.GetSearchResult(searchString).ToList();
            }
            foreach(var obj in expenseReports)
            {
                obj.Category = objexpense.getExpenseCat(obj);
            }
            return View(expenseReports);
        }

        public ActionResult AddEditExpenses(int itemId)
        {
            ExpenseReport model = new ExpenseReport();
            IEnumerable<SelectListItem> GetExpenseCategoryList = objexpense.getExpenseCatList();
                
            ViewBag.PopulateExpCategory = GetExpenseCategoryList;
            if (itemId > 0)
            {
                model = objexpense.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }

        [HttpPost]
        public ActionResult Create(ExpenseReport newExpense)
        {
            
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    objexpense.UpdateExpense(newExpense);
                }
                else
                {
                    objexpense.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            objexpense.DeleteExpense(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }

        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = objexpense.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }

        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = objexpense.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}
