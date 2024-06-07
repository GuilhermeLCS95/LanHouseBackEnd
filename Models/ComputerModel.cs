using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;

namespace SistemaLanHouseBackEnd.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public bool IsItBeingUsed { get; set; }

        [JsonIgnore]
        public ICollection<RegisterModel> Registers { get; set; } = new List<RegisterModel>();         
    }
    
}
