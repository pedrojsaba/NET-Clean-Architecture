using Banking.Application;
using System.Linq;
using System.Web.Mvc;

namespace Banking.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dataBaseService = new DataBaseService();
            dataBaseService.CreateBanking(Funciones.GetConnectionString());

            BankingApplicationService bankingApplicationService = new BankingApplicationService();
            ViewBag.TotalCustomers= bankingApplicationService.GetAll().Count();    

            return View();
        }
    }
}
