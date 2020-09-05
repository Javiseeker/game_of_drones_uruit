using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameOfDrones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditActionController : ControllerBase
    {
        private readonly IAuditActionRepository _repo;
        public AuditActionController(IAuditActionRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAuditActions()
        {
            var auditActions = await _repo.GetAuditActions();

            return Ok(auditActions);
        }

    }
}