using API_Bus.Models;

namespace API_Bus
{
    public class Seed
    {
        private readonly DatabaseContext databaseContext;

        public Seed(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        public void SeedDataContext()
        {
            var Medicaments = new List<Medicament> {
            new Medicament()
            {
                Name = "Test"
            },
            new Medicament()
            {
                Name = "Test2"
            },
            };

            databaseContext.Medicaments.AddRange(Medicaments);
            databaseContext.SaveChanges();

        }
    }
}
