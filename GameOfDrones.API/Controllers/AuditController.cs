
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOfDrones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _repo;
        public AuditController(IAuditRepository  repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditTransactions()
        {
            var auditTransactions = await _repo.GetAuditTransactions();

            return Ok(auditTransactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditTransaction(int id)
        {
            var auditTransaction = await _repo.GetAuditTransaction(id);

            return Ok(auditTransaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuditTransaction(Audit auditTransaction)
        {
            var auditTransactionToCreate = await _repo.CreateAuditTransaction(auditTransaction);

            return Created($"api/Audit/{auditTransactionToCreate.AUid}", auditTransactionToCreate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuditTransaction( Audit auditTransaction)
        {
            var auditTransactionToUpdate = await _repo.UpdateAuditTransaction(auditTransaction);

            if(auditTransactionToUpdate != null)
            {
                return Ok(auditTransactionToUpdate);
            }
            else
            {
                return NotFound($"Couldn't perform the update operation successfully on requested item. ({auditTransactionToUpdate})");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuditTransaction( Audit auditTransaction)
        {
            var operation = await _repo.DeleteAuditTransaction(auditTransaction);

            if(operation == true)
            {
                return Ok("The item was successfully deleted.");
            }
            else
            {
                return NotFound($"Couldn't delete audit action with the given id. (id:{auditTransaction.AUid})");
            }
        }

    }
}