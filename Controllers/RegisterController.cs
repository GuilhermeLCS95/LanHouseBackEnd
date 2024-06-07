using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLanHouseBackEnd.Data;
using SistemaLanHouseBackEnd.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SistemaLanHouseBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RegisterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<RegisterModel> Get()
        {
            return _dataContext.Registers
                .Include(r => r.User)
                .Include(r => r.Computer)
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var register = await _dataContext.Registers
                .Include(r => r.User)
                .Include(r => r.Computer)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (register == null)
            {
                return NotFound();
            }

            return Ok(register);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel register)
        {
            var selectedComputer = _dataContext.Computers.FirstOrDefault(c => c.Id == register.ComputerId);

            if (selectedComputer == null || selectedComputer.IsItBeingUsed)
            {
                return BadRequest("O computador selecionado não está disponível.");
            }

            selectedComputer.IsItBeingUsed = true;
            _dataContext.Entry(selectedComputer).State = EntityState.Modified;

            register.Computer = selectedComputer;
            _dataContext.Registers.Add(register);

            await _dataContext.SaveChangesAsync();

            return Ok(register);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var register = await _dataContext.Registers.FindAsync(id);

            if (register == null)
            {
                return NotFound();
            }

            var computer = await _dataContext.Computers.FindAsync(register.ComputerId);
            if (computer != null)
            {
                computer.IsItBeingUsed = false;
                _dataContext.Entry(computer).State = EntityState.Modified;
            }

            var user = await _dataContext.Users.FindAsync(register.UserId);
            if (user != null)
            {
                _dataContext.Users.Remove(user);
            }
            _dataContext.Registers.Remove(register);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterModel register)
        {

            var existingRegister = await _dataContext.Registers
                .Include(r => r.Computer)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            var selectedComputer = await _dataContext.Computers.FindAsync(register.ComputerId);
            if (selectedComputer == null || selectedComputer.IsItBeingUsed)
            {
                return BadRequest("O computador selecionado não está disponível.");
            }

            existingRegister.TimeBeingUsed = register.TimeBeingUsed;

            existingRegister.User.Name = register.User.Name;

            if (existingRegister.ComputerId != register.ComputerId)
            {
                var previousComputer = await _dataContext.Computers.FindAsync(existingRegister.ComputerId);
                if (previousComputer != null)
                {
                    previousComputer.IsItBeingUsed = false;
                    _dataContext.Entry(previousComputer).State = EntityState.Modified;
                }

                selectedComputer.IsItBeingUsed = true;
                _dataContext.Entry(selectedComputer).State = EntityState.Modified;

                existingRegister.ComputerId = register.ComputerId;
                existingRegister.Computer = selectedComputer;
            }

            _dataContext.Entry(existingRegister).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(existingRegister);
        }


    }
}
