using Microsoft.AspNetCore.Mvc;
using Training.Models;
using Microsoft.AspNetCore.OData.Query;

namespace Training.Controllers
{
    public class SelectAllController : Controller
    {
        private readonly DataContext _db;
        public SelectAllController(DataContext dbContext)
        {
            _db = dbContext;

        }
        [HttpGet]
        [EnableQuery]
        public IEnumerable<SelectAll_Result> Get()
        {
            return _db.SelectAll();
        }
    }
}
