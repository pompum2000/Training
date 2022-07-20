using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Deltas;
using log4net;
using Training.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Training.Hubs;

namespace Training.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly DataContext _db;
        private readonly IHubContext<SignalrServer> _signalrHub;
        ILog log = LogManager.GetLogger(typeof(NhanViensController));
        public NhanViensController(DataContext dbContext, IHubContext<SignalrServer> signalrHub)
        {
            _db = dbContext;
            _signalrHub = signalrHub;
        }
        [HttpGet]
        
        
        [HttpGet]
        [EnableQuery]
        public IQueryable<NhanVien> Get()
        {
            return _db.NhanVien;
        }
        [EnableQuery]
        public SingleResult<NhanVien> Get([FromODataUri] int key)
        {
            var result = _db.NhanVien.Where(c => c.IdNv == key);
            return SingleResult.Create(result);
        }
        public async Task<IActionResult> Post([FromBody] NhanVien nhanVien)
        {
            _db.NhanVien.Add(nhanVien);
            await _db.SaveChangesAsync();
            log.Info("Thêm nhân viên thành công!:" + _db.NhanVien);
            log4net.GlobalContext.Properties["NhanVien"] = _db.NhanVien;
            await _signalrHub.Clients.All.SendAsync("LoadNhanVien");
            return Ok(nhanVien);
        }

       

        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            Random rnd = new Random();
            string[] Name = { "Tri", "Tu", "Tai", "Teo" };
            string[] Position = { "Admin", "User" };
            nhanVien.IdDepartment = rnd.Next(1, 3);
            int name = rnd.Next(Name.Length);
            int position = rnd.Next(Position.Length);
            nhanVien.Name = Name[name];
            nhanVien.Position = Position[position];
            _db.NhanVien.Add(nhanVien);
            _db.SaveChanges();
            return Ok(nhanVien);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<NhanVien> nhanvien)
        {
            var a = nhanvien.GetInstance();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = _db.NhanVien.FirstOrDefault(item => item.IdNv == key);
            if (existingNote == null)
            {
                return NotFound();
            }
            nhanvien.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
                log.Info("Sửa thành công!:" + existingNote.IdNv);
                log4net.GlobalContext.Properties["NhanVien"] = existingNote.IdNv;
                await _signalrHub.Clients.All.SendAsync("LoadNhanVien");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            log.Info("Sửa nhân viên thành công!:" + existingNote.IdNv);
            log4net.GlobalContext.Properties["NhanVien"] = existingNote.IdNv;
            return Ok(existingNote);
        }
        private bool NhanVienExists(int key)
        {
            return _db.NhanVien.Any(p => p.IdNv == key);
        }
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var existingNote = _db.NhanVien.FirstOrDefault(item => item.IdNv == key);
            if (existingNote == null)
            {
                return NotFound();
            }
            log.Info("Xóa thành công!:" + existingNote.IdNv);
            log4net.GlobalContext.Properties["NhanVien"] = existingNote.IdNv;
            _db.NhanVien.Remove(existingNote);
            
            await _db.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadNhanVien");
            return Ok();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.NhanVien.ToList());
        }
    }
}
