using GameOfDrones.API.Models;

namespace GameOfDrones.API.Dtos
{
    public class AuditToSendDto: Audit
    {
        public string AaName { get; set; }
    }
}