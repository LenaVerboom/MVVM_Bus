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
                Name = "Test",
                Description = "double test"
            },
            new Medicament()
            {
                Name = "Test2",
                Description = "double test2"
            },
            };

            databaseContext.Medicaments.AddRange(Medicaments);
            databaseContext.SaveChanges();

        }
    }
}
