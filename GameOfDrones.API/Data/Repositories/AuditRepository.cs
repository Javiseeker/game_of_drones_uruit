using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfDrones.API.Data.Interfaces;
using GameOfDrones.API.Dtos;
using GameOfDrones.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfDrones.API.Data.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly DataContext _context;
        public AuditRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<List<AuditToSendDto>> GetAuditTransactions()
        {
            var auditTransactions = await _context.Audit
            .Join(
                _context.AuditAction,
                auditTransaction => auditTransaction.AUid,
                auditAction => auditAction.AaUid,
                (auditTransaction, auditAction) => new AuditToSendDto{
                    AUid = auditTransaction.AUid,
                    AAaUid = auditTransaction.AAaUid,
                    ADescription = auditTransaction.ADescription,
                    ADateCreated = auditTransaction.ADateCreated,
                    AaName = auditAction.AaName
                }
            )
            .OrderBy(at => at.ADateCreated)
            .ToListAsync();

            return auditTransactions;
            
        }

        public async Task<AuditToSendDto> GetAuditTransaction(int id)
        {
            var auditTransaction = await _context.Audit
            .Join(
                _context.AuditAction,
                auditTransaction => auditTransaction.AUid,
                auditAction => auditAction.AaUid,
                (auditTransaction, auditAction) => new AuditToSendDto{
                    AUid = auditTransaction.AUid,
                    AAaUid = auditTransaction.AAaUid,
                    ADescription = auditTransaction.ADescription,
                    ADateCreated = auditTransaction.ADateCreated,
                    AaName = auditAction.AaName
                }
            )
            .FirstOrDefaultAsync(at=> at.AUid == id);

            return auditTransaction;
        }


        public async Task<Audit> CreateAuditTransaction(Audit auditTransaction)
        {
            var result = await _context.Audit.AddAsync(auditTransaction);
            await _context.SaveChangesAsync();
            return auditTransaction;
        }


        public async Task<Audit> UpdateAuditTransaction(Audit auditTransaction)
        {
            var auditTransactionInDB = await _context.Audit.FirstOrDefaultAsync(x => x.AUid == auditTransaction.AUid);

            if (auditTransactionInDB != null)
            {
                auditTransactionInDB.AAaUid = auditTransaction.AAaUid;
                auditTransactionInDB.ADescription = auditTransaction.ADescription;

                _context.Audit.Update(auditTransactionInDB);
                await _context.SaveChangesAsync();
            }

            return auditTransactionInDB;
        }
        public async Task<bool> DeleteAuditTransaction(Audit auditTransaction)
        {
            var deletionSuccess = false;

            var auditTransactionInDB = await _context.Audit.FirstOrDefaultAsync(x => x.AUid == auditTransaction.AUid);

            if (auditTransactionInDB != null)
            {
                _context.Audit.Remove(auditTransactionInDB);
                await _context.SaveChangesAsync();
                deletionSuccess = true;
            }

            return deletionSuccess;
        }

    }
}