using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.DataAccess.SeedData
{
    public class SeedEthicGroups
    {
        public List<EthnicGroup> EthicGroups { get; set; }

        public SeedEthicGroups()
        {
            EthicGroups = new List<EthnicGroup>()
            {
                new() { Id = 1, Name = "Vietnamese" },
                new() { Id = 2, Name = "Thai" },
                new() { Id = 3, Name = "Han" },
                new() { Id = 4, Name = "Indo-Aryan" },
                new() { Id = 5, Name = "Russian" },
                new() { Id = 6, Name = "European" },
                new() { Id = 7, Name = "Malay" },
                new() { Id = 8, Name = "Arab" },
                new() { Id = 9, Name = "Javanese" },
                new() { Id = 10, Name = "Telugu" },
                new() { Id = 11, Name = "Marathi" },
                new() { Id = 12, Name = "African" },
                new() { Id = 13, Name = "Turkish" },
                new() { Id = 14, Name = "Korean" },
                new() { Id = 15, Name = "German" },
                new() { Id = 16, Name = "Italian" },
                new() { Id = 17, Name = "Persian" },
                new() { Id = 18, Name = "Gujarati" },
                new() { Id = 19, Name = "Hausa" },
                new() { Id = 20, Name = "Kurdish" },
            };
        }
    }
}
