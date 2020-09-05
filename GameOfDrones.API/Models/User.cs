using System;
using System.Collections.Generic;

namespace GameOfDrones.API.Models
{
    public partial class User
    {
        public User()
        {
            GameStatistics = new HashSet<GameStatistics>();
        }

        public int UUid { get; set; }
        public string UName { get; set; }

        public virtual ICollection<GameStatistics> GameStatistics { get; set; }
    }
}
