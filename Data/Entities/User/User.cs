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

        public DateTime CreatedDate { get; set; }

        public bool Confirmed { get; set; }

        #endregion

        #region Ctor

        public User()
        {
            
        }

        public User(
            Guid id, 
            string userName, 
            bool maleFemale, 
            int missionsCount, 
            StarFleetRanks rank, 
            DateTime created, 
            bool Confirmed = false)
        {
            Id = id;
            UserName = userName;
            MaleFemale = maleFemale;
            MissionsCount = missionsCount;
            Rank = rank;
            CreatedDate = created;
            this.Confirmed = Confirmed;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} {UserName} M/F: {MaleFemale} Missions: {MissionsCount} Rank: {Rank} Enlisted: {CreatedDate.ToShortDateString()} Conf: {Confirmed}";
        }

        #endregion
    }
}
