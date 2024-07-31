using Data.DataBase;
using Data.Repositories.Realizations.UserRep;
using JsonDataProvider;
using LiteDB;
using Models.DAL.Entities.User;
using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.Message;
using SpaceAvenger.Services.Interfaces.MessageBus;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.Services.Realizations;
using SpaceAvenger.Services.Realizations.Message;
using SpaceAvenger.ViewModels.UserProfile;
using SpaceAvenger.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    [ViewModelType(ViewModelUsage.Page)]
    internal class ChooseProfile_ViewModel : ViewModelBase
    {
        #region Fields

        private UserRepository m_userRepository;

        private ObservableCollection<UserProfileVM> m_profileList;

        private int m_SelectedUserIndex;

        private IPageManagerService<FrameType> m_PageManager;

        private IMessageBus m_messageBus;
                                
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

        public ICommand OnDeleteUserProfileButtonPressed { get; }

        #endregion

        #region Ctor

        public ChooseProfile_ViewModel()
        {
            
        }

        public ChooseProfile_ViewModel(IPageManagerService<FrameType> pageManager,
            IMessageBus messageBus) : this()
        {
            m_messageBus = messageBus;

            m_PageManager = pageManager;

            m_SelectedUserIndex = -1;

            m_userRepository = new UserRepository(
                new SpaceAvengerDbContext(Environment.CurrentDirectory + 
                Path.DirectorySeparatorChar + 
                "DataBase" + Path.DirectorySeparatorChar + "Local.db"));
                        
            m_profileList = new ObservableCollection<UserProfileVM>();

            var users = m_userRepository.GetAllAsync().Result;

            foreach (var user in users)
            {
                var upvm = new UserProfileVM(m_profileList.Count + 1, user);
                upvm.OnUserProfileConfirmedEvent += Up_OnUserProfileConfirmedEvent;
                upvm.OnUserProfileSelectedEvent += Up_OnUserProfileSelectedEvent;
                m_profileList.Add(upvm);
            }
            
            OnAddNewProfileButtonPressed = new Command(
                OnAddNewProfileButtonPressedExecute,
                CanOnAddNewProfileButtonPressedExecute
                );

            OnEditUserProfileButtonPressed = new Command(
                OnEditUserProfileButtonpressedExecute,
                CanOnEditUserProfileButtonpressedExecute
                );

            OnDeleteUserProfileButtonPressed = new Command(
                OnDeleteUserProfileButtonPressedExecute,
                CanOnDeleteUserProfileButtonPressedExxecute
                );

            #region Subscriptions
            
            #endregion
        }

        private void Up_OnUserProfileSelectedEvent(User obj)
        {
            m_PageManager.SwitchPage(nameof(Main_Page), FrameType.MainFrame);

            m_messageBus.Send<ChooseProfileMessage_User, User>(new ChooseProfileMessage_User(obj));
        }


        #endregion

        #region Methods

        #region On Recieve Message

        

        #endregion

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

        #region On Delete User Profile Button Presssed
        private bool CanOnDeleteUserProfileButtonPressedExxecute(object p)
        {
            return m_SelectedUserIndex >= 0;
        }

        private void OnDeleteUserProfileButtonPressedExecute(object p)
        {
            var r = m_userRepository.Remove(ProfileList[SelectedUserIndex].User);

            if(r)
                ProfileList.RemoveAt(SelectedUserIndex);
        }       
        #endregion

        #endregion
    }
}
