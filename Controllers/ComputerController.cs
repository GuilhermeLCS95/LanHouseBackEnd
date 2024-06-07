using Microsoft.AspNetCore.Mvc;
using SistemaLanHouseBackEnd.Data;
using SistemaLanHouseBackEnd.Models;

namespace SistemaLanHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ComputerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<ComputerModel> Get()
        {
            return _dataContext.Computers.ToList();
        }

        [HttpPost]
        public ComputerModel PostComputers([FromBody] ComputerModel computer)
        {
            _dataContext.Computers.Add(computer);
            _dataContext.SaveChanges();
            return computer;
        }

      
    }
}
