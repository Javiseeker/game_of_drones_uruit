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
            throw new System.NotImplementedException();
        }


        public async Task<AuditAction> CreateAuditAction(AuditAction auditAction)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuditAction> UpdateAuditAction(AuditAction auditAction)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteAuditAction(AuditAction auditAction)
        {
            throw new System.NotImplementedException();
        }
    }
}