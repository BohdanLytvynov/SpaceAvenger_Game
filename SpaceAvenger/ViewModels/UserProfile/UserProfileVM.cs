using Models.DAL.Entities.User;
using System;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.UserProfile
{
    public class UserProfileVM : ViewModelBase
    {
        #region Fields

        private User m_user;

        //Guid m_id;

        int m_Number;

        //StarFleetRanks m_rank;

        //bool m_MaleFemale;

        //int m_MissionsCount;

        //string m_Name;

        #endregion

        #region Properties

        public User User { get=> m_user; set => Set(ref m_user, value); }

        //public Guid Id { get => m_id; set => m_id = value; }

        //public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public int Number { get => m_Number; set => Set(ref m_Number, value); }

        //public bool MaleFemale { get => m_MaleFemale; set => Set(ref m_MaleFemale, value); }

        //public StarFleetRanks Rank { get => m_rank; set => Set(ref m_rank, value); }

        //public int MissionsCount { get => m_MissionsCount; set => Set(ref m_MissionsCount, value); }

        #endregion

        #region Ctor
        public UserProfileVM(
            int number, 
            Guid id, 
            string Name, 
            bool MaleFemale, 
            StarFleetRanks rank, 
            int MissionsCount,
            DateTime created)
        {
            m_user = new User(id, Name, MaleFemale, MissionsCount, rank, created);

            m_Number = number;

            //m_MaleFemale = MaleFemale;

            //m_rank = rank;

            //m_MissionsCount = MissionsCount;

            //m_Name = Name;
        }

        public UserProfileVM()
        {
            m_user = new User();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{m_Number}) {m_user.UserName} {m_user.MaleFemale} {m_user.Rank} {m_user.MissionsCount} {m_user.Created.ToShortDateString()}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_user.Id, m_user.UserName);
        }

        public override bool Equals(object? obj)
        {
            var user = obj as UserProfileVM;

            if (user is null) return false;

            return user.User.Id.Equals(this.User.Id);
        }

        #endregion        
    }
}
