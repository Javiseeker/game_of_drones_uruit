using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditAction(int id)
        {
            var auditAction = await _repo.GetAuditAction(id);

            return Ok(auditAction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuditAction(AuditAction auditAction)
        {
            var auditActionToCreate = await _repo.CreateAuditAction(auditAction);

            return Created($"api/AuditAction/{auditActionToCreate.AaUid}", auditActionToCreate);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAuditActiom( AuditAction auditAction)
        {
            var auditActionToUpdate = await _repo.UpdateAuditAction(auditAction);

            if(auditActionToUpdate != null)
            {
                return Ok(auditActionToUpdate);
            }
            else
            {
                return NotFound($"Couldn't perform the update operation successfully on requested item. ({auditActionToUpdate})");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAuditAction( AuditAction auditAction)
        {
            var operation = await _repo.DeleteAuditAction(auditAction);

            if(operation == true)
            {
                return Ok("The item was successfully deleted.");
            }
            else
            {
                return NotFound($"Couldn't delete audit action with the given id. (id:{auditAction.AaUid})");
            }
        }

    }
}