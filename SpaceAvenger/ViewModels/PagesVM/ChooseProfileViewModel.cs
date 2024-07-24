using Data.DataBase;
using Data.Repositories.Realizations.UserRep;
using JsonDataProvider;
using LiteDB;
using Models.DAL.Entities.User;
using SpaceAvenger.ViewModels.UserProfile;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    public class ChooseProfileViewModel : ViewModelBase
    {
        #region Fields

        private UserRepository m_userRepository;

        private ObservableCollection<UserProfileVM> m_profileList;

        private int m_SelectedUserIndex;
                        
        #endregion

        #region Properties

        public ObservableCollection<UserProfileVM> ProfileList 
        { get=> m_profileList; set=> m_profileList = value; }

        public int SelectedUserIndex 
        { get=> m_SelectedUserIndex; set => Set(ref m_SelectedUserIndex, value); }

        #endregion

        #region Commands

        public ICommand OnAddNewProfileButtonPressed { get; }

        public ICommand OnEditUserProfileButtonPressed { get; }

        #endregion

        #region Ctor
        public ChooseProfileViewModel()
        {
            m_SelectedUserIndex = -1;

            m_userRepository = new UserRepository(
                new SpaceAvengerDbContext(Environment.CurrentDirectory + 
                Path.DirectorySeparatorChar + 
                "DataBase" + Path.DirectorySeparatorChar + "Local.db"));
                        
            m_profileList = new ObservableCollection<UserProfileVM>();

            var users = m_userRepository.GetAllAsync().Result;

            foreach (var user in users)
            {
                m_profileList.Add(new UserProfileVM(m_profileList.Count + 1, user));
            }
            
            OnAddNewProfileButtonPressed = new Command(
                OnAddNewProfileButtonPressedExecute,
                CanOnAddNewProfileButtonPressedExecute
                );

            OnEditUserProfileButtonPressed = new Command(
                OnEditUserProfileButtonpressedExecute,
                CanOnEditUserProfileButtonpressedExecute
                );
        }

        
        #endregion

        #region Methods

        #region On Add New Profile Button Pressed
        private bool CanOnAddNewProfileButtonPressedExecute(object p)
        {
            return true;
        }

        private void OnAddNewProfileButtonPressedExecute(object p)
        {
            var up = new UserProfileVM(
                    ProfileList.Count + 1,
                    new User(Guid.Empty,
                    "Enter your name Commander",
                    true, 0,
                    StarFleetRanks.Cadet_4th_Grade,
                    default));

            up.OnUserProfileConfirmedEvent += Up_OnUserProfileConfirmedEvent; 

            ProfileList.Add(up);
        }

        private async Task Up_OnUserProfileConfirmedEvent(User obj)
        {
            if (obj.Id.Equals(Guid.Empty))
            {
                var r = await m_userRepository.AddAsync(obj);
            }
            else
            {
                var r = await m_userRepository.UpdateAsync(obj);
            }
            
        }

        #endregion

        #region On Edit User Profile Button Pressed

        private bool CanOnEditUserProfileButtonpressedExecute(object p)
        {
            return m_SelectedUserIndex >= 0;
        }

        private void OnEditUserProfileButtonpressedExecute(object p)
        {
            ProfileList[m_SelectedUserIndex]!.Confirmed = false;                         
        }

        #endregion

        #endregion
    }
}
