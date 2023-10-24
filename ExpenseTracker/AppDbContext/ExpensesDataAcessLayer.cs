using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.AppDbContext
{
    public class ExpensesDataAcessLayer
    {
        ExpenseDBContext db;

        public ExpensesDataAcessLayer(ExpenseDBContext db)
        {
            this.db = db;
        }

        public IEnumerable<ExpenseReport> GetAllExpenses()
        {
            try
            {
                return db.ExpenseReport.ToList();
            }
            catch
            {
                throw;
            }
        }

        // To filter out the records based on the search string 
        public IEnumerable<ExpenseReport> GetSearchResult(string searchString)
        {
            List<ExpenseReport> exp = new List<ExpenseReport>();
            try
            {
                exp = GetAllExpenses().ToList();
                return exp.Where(x => x.ItemName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }

        //To get Expense Category through Foreign Key
        public ExpenseCategory getExpenseCat(ExpenseReport obj)
        {
            ExpenseCategory cat = db.ExpenseCategory.FirstOrDefault(u => u.ExpenseCategoryId == obj.ExpenseCategoryId);
            return cat;
        }

        //get Expense Categories from ExpenseCategory
        public IEnumerable<SelectListItem> getExpenseCatList()
        {
            try
            {
                IEnumerable<SelectListItem> list =
                db.ExpenseCategory.Select(i => new SelectListItem
                {
                    Text = i.ExpenseCategoryName,
                    Value = i.ExpenseCategoryId.ToString()
                });
                return list;
            }
            catch
            {
                throw;
            }
        }
        //To Add new Expense record       
        public void AddExpense(ExpenseReport expense)
        {
            try
            {
                db.ExpenseReport.Add(expense);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar expense  
        public int UpdateExpense(ExpenseReport expense)
        {
            try
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the data for a particular expense  
        public ExpenseReport GetExpenseData(int id)
        {
            try
            {
                ExpenseReport expense = db.ExpenseReport.Find(id);
                return expense;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular expense  
        public void DeleteExpense(int id)
        {
            try
            {
                ExpenseReport emp = db.ExpenseReport.Find(id);
                db.ExpenseReport.Remove(emp);
                db.SaveChanges();

            }
            catch
            {
                throw;
            }
        }


        // To calculate last six months expense
        public Dictionary<string, decimal> CalculateMonthlyExpense()
        {
            
            List<ExpenseReport> lstEmployee = new List<ExpenseReport>();

            Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();
            List<string> categories = new List<string>();
            IEnumerable<ExpenseCategory> cats = db.ExpenseCategory.ToList();

            foreach (ExpenseCategory cat in cats)
            {
                categories.Add(cat.ExpenseCategoryName);
            }

            foreach (string category in categories)
            {
                decimal sum = db.ExpenseReport.Where(cat => cat.Category.ExpenseCategoryName == category && cat.ExpenseDate > DateTime.Now.AddDays(-28))
                .Select(cat => cat.Amount)
                .Sum();
                dictMonthlySum.Add(category, sum);

            }

            return dictMonthlySum;
        }

        // To calculate last four weeks expense
        public Dictionary<string, decimal> CalculateWeeklyExpense()
        {
            
            List<ExpenseReport> lstEmployee = new List<ExpenseReport>();

            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();
            List<string> categories = new List<string>();
            IEnumerable<ExpenseCategory> cats = db.ExpenseCategory.ToList();

            foreach (ExpenseCategory cat in cats)
            {
                categories.Add(cat.ExpenseCategoryName);
            }

            foreach (string category in categories)
            {
                decimal sum = db.ExpenseReport.Where(cat => cat.Category.ExpenseCategoryName == category && cat.ExpenseDate > DateTime.Now.AddDays(-7))
                .Select(cat => cat.Amount)
                .Sum();
                dictWeeklySum.Add(category, sum);

            }

            return dictWeeklySum;
        }


        //For Expense Category Table

        //get All
        public IEnumerable<ExpenseCategory> GetAllExpenseCategories()
        {
            try
            {
                return db.ExpenseCategory.ToList();
            }
            catch
            {
                throw;
            }
        }

        //Add Expense Category
        public void AddExpenseCategory(ExpenseCategory expense)
        {
            try
            {
                db.ExpenseCategory.Add(expense);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //get Expense Category using id
        public ExpenseCategory GetExpenseCatData(int id)
        {
            try
            {
                ExpenseCategory expense = db.ExpenseCategory.Find(id);
                return expense;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateExpenseCategory(ExpenseCategory expensecat)
        {
            try
            {
                db.Entry(expensecat).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteExpenseCategoory(int id)
        {
            try
            {
                ExpenseCategory excat = db.ExpenseCategory.Find(id);
                db.ExpenseCategory.Remove(excat);
                db.SaveChanges();

            }
            catch
            {
                throw;
            }
        }


        //UserProfile Table OPerations

        public UserProfile getUserByEmailPassword(string email, string pass)
        {
            UserProfile user = db.UserProfile.FirstOrDefault
                (a => a.Email == email && a.Password == pass);
            return user;
        }

        public void AddUser(UserProfile user)
        {
            try
            {
                db.UserProfile.Add(user);
                db.SaveChanges();
            } catch
            {
                throw;
            }
        }
    }
        

}
