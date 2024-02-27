using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.DataAccess.SeedData
{
    public class SeedOccupations
    {
        public List<Occupation> Occupations { get; set; }

        public SeedOccupations()
        {
            Occupations = new List<Occupation>()
            {
                new() { Id = 1, Name = "Tailor" },
                new() { Id = 2, Name = "Worker" },
                new() { Id = 3, Name = "Carpenter" },
                new() { Id = 4, Name = "Laborer" },
                new() { Id = 5, Name = "Bricklayer" },
                new() { Id = 6, Name = "Mason" },
                new() { Id = 7, Name = "Welder" },
                new() { Id = 8, Name = "Miner" },
                new() { Id = 9, Name = "Printer" },
                new() { Id = 10, Name = "Plater" },
                new() { Id = 11, Name = "Quarryman" },
                new() { Id = 12, Name = "Blacksmith" },
                new() { Id = 13, Name = "Painter" },
                new() { Id = 14, Name = "Molder" },
                new() { Id = 15, Name = "Fisher" },
                new() { Id = 16, Name = "Farmer" },
                new() { Id = 17, Name = "Planer" },
                new() { Id = 18, Name = "Potter" },
                new() { Id = 19, Name = "Ploughman" },
                new() { Id = 20, Name = "Craftman" },
            };
        }
    }
}
