using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.VM;

namespace Models.ViewModels.User
{
    public class UserProfile : ViewModelBase
    {
        #region Fields

        Guid m_id;

        int m_Number;

        StarFleetRanks m_rank;

        bool m_MaleFemale;

        int m_MissionsCount;

        string m_Name;

        #endregion

        #region Properties

        public Guid Id { get => m_id; set => m_id = value; }

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public int Number { get => m_Number; set => Set(ref m_Number, value); }

        public bool MaleFemale { get => m_MaleFemale; set => Set(ref m_MaleFemale, value); }

        public StarFleetRanks Rank { get => m_rank; set => Set(ref m_rank, value); }

        public int MissionsCount { get => m_MissionsCount; set => Set(ref m_MissionsCount, value); }

        #endregion

        #region Ctor
        public UserProfile(int number, string Name, bool MaleFemale, StarFleetRanks rank, int MissionsCount)
        {
            m_Number = number;

            m_MaleFemale = MaleFemale;

            m_rank = rank;

            m_MissionsCount = MissionsCount;

            m_Name = Name;
        }

        public UserProfile()
        {

        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Name} {MaleFemale} {Rank} {MissionsCount}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_id, m_Name);
        }

        public override bool Equals(object? obj)
        {
            var user = obj as UserProfile;

            if (user is null) return false;

            return user.m_id.Equals(this.m_id);
        }

        #endregion

        #region Operators

        public static bool operator == (UserProfile l, UserProfile r)
        { 
            return l.Equals(r);
        }

        public static bool operator != (UserProfile l, UserProfile r)
        { 
            return !(l == r); 
        }

        #endregion
    }
}
