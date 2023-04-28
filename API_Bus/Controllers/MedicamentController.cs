
using API_Bus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API_Bus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController
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
           /* if (_databaseContext.Medicaments == null)
            {
                return NotFoundResult();
            }
           */
            return await _databaseContext.Medicaments.ToListAsync();
         }

        [HttpGet("{id}")]
        [Route("GetOne")]
        public async Task<ActionResult<Medicament>> getMedicament(int id)
        {
            /* if (_databaseContext.Medicaments == null)
             {
                 return NotFoundResult();
             }
            */
            var Medoc = await _databaseContext.Medicaments.FindAsync(id);
            return Medoc;
        }
    }
    
}
