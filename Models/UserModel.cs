using System.Text.Json.Serialization;

namespace SistemaLanHouseBackEnd.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        private string name;
        public string Name 
        {
            get { return name; }
            set { name = value?.ToUpper(); }
        }

        [JsonIgnore]
        public ICollection<RegisterModel> Registers { get; set; } = new List<RegisterModel>();
    }
}
