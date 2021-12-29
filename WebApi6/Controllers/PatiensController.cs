using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi6.Data;
using WebApi6.Repository;

namespace WebApi6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatiensController : ControllerBase
    {
        private IPatienRepository _repo;
        private ILogger _logger;
        public PatiensController(ILogger logger, IPatienRepository patienRepository)
        {
            _repo = patienRepository;
            _logger = logger;
        }

        [HttpGet("GetAllPatiens")]
        public IEnumerable<Patien> GetAllPatiens()
        {
            return _repo.GetPatiens();
        }
        [HttpGet("GetPatiensAsync")]
        public async Task<IEnumerable<Patien>> GetPatiensAsync()
        { 
            return await Task.FromResult(_repo.GetPatiens());
        }

        [HttpGet("GetPatienById/{id}")]
        public IActionResult GetPatien(int id)
        {
            var p = _repo.GetPatiens().FirstOrDefault(x => x.Id == id);

            if (p == null)
                return NotFound();
            else
                return Ok(p);
        }

        [HttpDelete("DeletePatien/{id}")]
        public IActionResult DeletePatien( int id)
        {
            var p = _repo.GetPatiens().FirstOrDefault(x => x.Id == id);

            if (p == null)
                return BadRequest();

            return Ok(_repo.DeletePatien(id));
        }

        [HttpPost("AddPatien")]
        public IActionResult AddPatien([FromBody] Patien patien)
        {
            if(!patien.Email.Contains('@')) 
                return BadRequest();

            _repo.AddPatien(patien);
            return Ok();
        }

        [HttpPut("EditPatien")]
        public IActionResult EditPatien([FromBody] Patien patien)
        { 
            if(!patien.Email.Contains('@') || !_repo.GetPatiens().Any(x => x.Id == patien.Id))
                return BadRequest();

            _repo.UpdatePatien(patien);
            return Ok(true);
        }
    }
}
