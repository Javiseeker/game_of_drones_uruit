using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfDrones.API.Data.Repositories
{
    public class AuditActionRepository : IAuditActionRepository
    {
        private readonly DataContext _context;
        public AuditActionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AuditAction>> GetAuditActions()
        {
            var auditActions = await _context.AuditAction.ToListAsync();
            return auditActions;
        }

        public async Task<AuditAction> GetAuditAction(int id)
        {
            var auditAction = await _context.AuditAction.FirstOrDefaultAsync(x => x.AaUid == id);
            return auditAction;
        }


        public async Task<AuditAction> CreateAuditAction(AuditAction auditAction)
        {
            var result = await _context.AuditAction.AddAsync(auditAction);
            await _context.SaveChangesAsync();
            return auditAction;
        }

        public async Task<AuditAction> UpdateAuditAction(AuditAction auditAction)
        {
            var auditActionInDB = await _context.AuditAction.FirstOrDefaultAsync(x => x.AaUid == auditAction.AaUid);

            if (auditActionInDB != null)
            {
                auditActionInDB.AaName = auditAction.AaName;
                _context.AuditAction.Update(auditActionInDB);
                await _context.SaveChangesAsync();
            }

            return auditActionInDB;
        }

        public async Task<bool> DeleteAuditAction(AuditAction auditAction)
        {
            var deletionSuccess = false;

            var auditActionInDB = await _context.AuditAction.FirstOrDefaultAsync(x => x.AaUid == auditAction.AaUid);

            if (auditActionInDB != null)
            {
                _context.AuditAction.Remove(auditActionInDB);
                await _context.SaveChangesAsync();
                deletionSuccess = true;
            }

            return deletionSuccess;
        }
    }
}