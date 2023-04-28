
using API_Bus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API_Bus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public MedicamentController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Medicament>>> getMedicaments()
        {
           if (_databaseContext.Medicaments == null)
            {
                return NotFound();
            }
           
            return await _databaseContext.Medicaments.ToListAsync();
         }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicament>> GetMedicament(int id)
        {
            if (_databaseContext.Medicaments == null)
            {
                return NotFound();
            }
            var artist = await _databaseContext.Medicaments.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return artist;
        }
    }
    
}
