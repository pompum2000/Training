//using Microsoft.AspNetCore.Mvc;
//using Training.Models;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.AspNetCore.OData.Results;
//using Microsoft.AspNetCore.OData.Formatter;

//namespace Training.Controllers
//{
//    public class SelectAllidController : Controller
//    {
//        private readonly DataContext _db;
//        public SelectAllidController(DataContext dbContext)
//        {
//            _db = dbContext;

//        }

      

//        [HttpGet]
//        [EnableQuery]
//        public IEnumerable<SelectAllid> Get()
//        {
//            return _db.SelectAllid( IdNv, IdNv2);
//        }
//        /*EnableQuery]*/
//        //public   SingleResult<SelectAllid> Get([FromODataUri] int key)
//        //{
//        //    var result =  _db.SelectAllid(IdNv, IdNv2).Where(c => c.IdNv == key);
//        //    return  SingleResult.Create(result);
//        //}
//    }
//}
