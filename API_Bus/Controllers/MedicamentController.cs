
using API_Bus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var medicament = await _databaseContext.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return NotFound();
            }
            return medicament;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicament>> DeleteMedicament(int id)
        {
            if (_databaseContext.Medicaments == null)
            {
                return NotFound();
            }
            var medicament = await _databaseContext.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return NotFound();
            }
            _databaseContext.Medicaments.Remove(medicament);
            await _databaseContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Medicament>> PostMedicament(Medicament medoc)
        {
            if (_databaseContext.Medicaments == null)
            {
                return NotFound();
            }
            
           
            _databaseContext.Medicaments.Add(medoc);
            await _databaseContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedicament),new {id = medoc.MedocId},medoc) ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicament(int id, Medicament medoc)
        {
            if (id != medoc.MedocId)
            {
                return BadRequest();
            }
            // update database
            _databaseContext.Entry(medoc).State = EntityState.Modified;

            try
            {
                await _databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
            return Ok();

        }
    }
    
}
