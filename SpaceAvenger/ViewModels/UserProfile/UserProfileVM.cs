using Models.DAL.Entities.User;
using System;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.UserProfile
{
    public class UserProfileVM : ViewModelBase
    {
        #region Fields

        private User m_user;
        
        int m_Number;      
        
        #endregion

        #region Properties

        public User User { get=> m_user; set => Set(ref m_user, value); }
        
        public int Number { get => m_Number; set => Set(ref m_Number, value); }

        #endregion

        #region Commands
       
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

            #region Init Commands
            
            #endregion
        }
       
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{m_Number}) {m_user.UserName} {m_user.MaleFemale} {m_user.Rank} {m_user.MissionsCount} {m_user.CreatedDate.ToShortDateString()}";
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
