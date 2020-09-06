using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Dtos;
using GameOfDrones.API.Models;

namespace GameOfDrones.API.Data.Interfaces
{
    public interface IAuditRepository
    {
        Task<List<AuditToSendDto>> GetAuditTransactions();
        Task<AuditToSendDto> GetAuditTransaction(int id);
        Task<Audit> CreateAuditTransaction(Audit auditTransaction);
        Task<Audit> UpdateAuditTransaction(Audit auditTransaction);
        Task<bool> DeleteAuditTransaction(Audit auditTransaction);
    }
}