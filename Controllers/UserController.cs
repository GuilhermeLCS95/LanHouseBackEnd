using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaLanHouseBackEnd.Data;
using SistemaLanHouseBackEnd.Models;

namespace SistemaLanHouseBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _dataContext.Users.ToList();
        }


        [HttpPost]
        public UserModel PostUser([FromBody] UserModel user)
        {

           _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return user;
        }
    }
}
