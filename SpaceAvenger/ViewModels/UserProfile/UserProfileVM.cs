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
        
        private int m_Number;

        private bool m_Confirmed;
        
        #endregion

        #region Properties

        public User User { get=> m_user; set => Set(ref m_user, value); }
        
        public int Number { get => m_Number; set => Set(ref m_Number, value); }

        public bool Confirmed { get=> m_Confirmed; set=> Set(ref m_Confirmed, value); }

        #endregion

        #region Commands
        public ICommand OnConfirmButtonPressed { get; }
        #endregion

        #region Ctor
        public UserProfileVM(
            int number, 
            Guid id, 
            string Name, 
            bool MaleFemale, 
            StarFleetRanks rank, 
            int MissionsCount,
            DateTime created,
            bool confirmed = false)
        {
            m_user = new User(id, Name, MaleFemale, MissionsCount, rank, created);

            m_Number = number;

            m_Confirmed = confirmed;

            #region Init Commands
            OnConfirmButtonPressed = new Command(
                canExecute: CanOnConfirmedButtonPressedExecute,
                execute: OnConfirmButtonPressedExecute);
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

        private bool CanOnConfirmedButtonPressedExecute(object p) => true;

        private void OnConfirmButtonPressedExecute(object p)
        { 
            Confirmed = true;
        }
        #endregion
    }
}
