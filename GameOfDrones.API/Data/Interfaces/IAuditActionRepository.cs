using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Models;

namespace GameOfDrones.API.Data.Interfaces
{
    public interface IAuditActionRepository
    {
        Task<List<AuditAction>> GetAuditActions();
        Task<AuditAction> GetAuditAction(int id);
        Task<AuditAction> CreateAuditAction(AuditAction auditAction);
        Task<AuditAction> UpdateAuditAction(AuditAction auditAction);
        Task<bool> DeleteAuditAction(AuditAction auditAction);
    }
}