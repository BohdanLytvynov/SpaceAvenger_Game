using Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL.Entities.User
{
    public class User : IIdEntity
    {
        #region Properties

        public Guid Id { get; set; }

        public StarFleetRanks Rank { get; set; }

        public bool MaleFemale { get; set; }

        public int MissionsCount { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }

        #endregion

        #region Ctor

        public User()
        {
            UserName = string.Empty;
        }

        public User(Guid id, string userName, bool maleFemale, int missionsCount, StarFleetRanks rank, DateTime created)
        {
            Id = id;
            UserName = userName;
            MaleFemale = maleFemale;
            MissionsCount = missionsCount;
            Rank = rank;
            Created = created;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} {UserName} {MaleFemale} {MissionsCount} {Rank} {Created.ToShortDateString()}";
        }

        #endregion
    }
}
