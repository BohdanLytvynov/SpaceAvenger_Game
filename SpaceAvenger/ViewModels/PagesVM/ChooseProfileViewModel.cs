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
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    public class ChooseProfileViewModel : ViewModelBase
    {
        #region Fields

        UserRepository m_userRepository;

        ObservableCollection<UserProfileVM> m_profileList;

        JDataProvider m_jdataProvider;

        bool m_EnableCollectionChangedEvent;

        string m_pathToProfiles;

        #endregion

        #region Properties

        public ObservableCollection<UserProfileVM> ProfileList { get=> m_profileList; set=> m_profileList = value; }

        #endregion

        #region Commands

        public ICommand OnAddNewProfileButtonPressed { get; }

        #endregion

        #region Ctor
        public ChooseProfileViewModel()
        {
            m_userRepository = new UserRepository(
                new SpaceAvengerDbContext(Environment.CurrentDirectory + 
                Path.DirectorySeparatorChar + 
                "DataBase" + Path.DirectorySeparatorChar + "Local.db"));

            //m_jdataProvider = new JDataProvider();

            m_pathToProfiles = JDataProvider.pathToEnv + @"\DataBase\UserProfiles.json";

            m_profileList = new ObservableCollection<UserProfileVM>();

            // m_profileList = m_userRepository.GetAllAsync().Result;

            ProfileList.CollectionChanged += ProfileList_CollectionChanged;

            OnAddNewProfileButtonPressed = new Command(
                OnAddNewProfileButtonPressedExecute,
                CanOnAddNewProfileButtonPressedExecute
                );
        }

        private void ProfileList_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            m_jdataProvider.SerializeObject(m_pathToProfiles ,ProfileList);
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
            //ProfileList.Add(
            //    new UserProfileVM(
            //        ProfileList.Count+1, 
            //        Guid.Empty, 
            //        "Enter your name Commander",
            //        true, 
            //        StarFleetRanks.Cadet4thGrade, 
            //        0, DateTime.UtcNow));
        }
        #endregion

        #endregion
    }
}
