using System.Data.Entity;
using TestNinja.Mocking;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            //Check out this refactoring - I've kept the old code in to see the changes to the EmployeeStorage file.
            //Remember that we split out queries to a seperate repository; however, because this method contains a
            //Save function we will not bring into a repository.
            _employeeStorage.DeleteEmployee(id);
            return RedirectToAction("Employees");

            //var employee = _db.Employees.Find(id);
            //_db.Employees.Remove(employee);
            //_db.SaveChanges();
            //return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}