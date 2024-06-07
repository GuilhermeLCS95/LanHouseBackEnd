using System.Text.Json.Serialization;

namespace SistemaLanHouseBackEnd.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public long TimeBeingUsed { get; set; }
        public DateTime StartUsingPc { get; set; }
        public DateTime EndUsingPc { get; set; }
        public int ComputerId { get; set; }
        public ComputerModel Computer { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

    }
}
