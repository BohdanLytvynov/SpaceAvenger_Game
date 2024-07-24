using Models.DAL.Entities.User;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.UserProfile
{
    public class UserProfileVM : ViewModelBase, IEquatable<UserProfileVM>
    {
        #region Events
        public event Func<User, Task>? OnUserProfileConfirmedEvent;
        #endregion

        #region Fields

        private User m_user;
        
        private int m_Number;

        private bool m_Confirmed;

        private DateTime m_enlistedDate;
        
        #endregion

        #region Properties

        public User User { get=> m_user; set => Set(ref m_user, value); }
        
        public int Number { get => m_Number; set => Set(ref m_Number, value); }

        public bool Confirmed 
        { 
            get=> m_Confirmed;
            set
            {
                Set(ref m_Confirmed, value);
                
                if(value != m_user.Confirmed)
                    m_user.Confirmed = value;
            } 
        }

        public DateTime EnlistedDate { get=> m_enlistedDate; set => Set(ref m_enlistedDate, value); }

        #endregion

        #region Commands
        public ICommand? OnConfirmButtonPressed { get; }
        #endregion

        #region
        
        public UserProfileVM(int number, User user)
        {            
            m_user = user;

            m_enlistedDate = m_user.CreatedDate;

            m_Number = number;

            m_Confirmed = user.Confirmed;

            #region Init Commands
            OnConfirmButtonPressed = new Command(
                canExecute: CanOnConfirmedButtonPressedExecute,
                execute: OnConfirmButtonPressedExecute);
            #endregion
        }

        #endregion

        #region Methods

        private void OnUserProfileConfirmed(User user)
        { 
            var temp = Volatile.Read(ref OnUserProfileConfirmedEvent);

            temp?.Invoke(user);
        }

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

            EnlistedDate = DateTime.UtcNow;

            m_user.CreatedDate = EnlistedDate;
            
            OnUserProfileConfirmed(m_user);
        }

        public bool Equals(UserProfileVM? other)
        {
            if(other == null) return false;

            return other.User.Id.Equals(this.User.Id);
        }
        #endregion
    }
}
