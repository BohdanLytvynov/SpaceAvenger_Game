using JsonDataProvider;
using Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    public class ChooseProfileViewModel : ViewModelBase
    {
        #region Fields

        ObservableCollection<UserProfile> m_profileList;

        JDataProvider m_jdataProvider;

        bool m_EnableCollectionChangedEvent;

        string m_pathToProfiles;

        #endregion

        #region Properties

        public ObservableCollection<UserProfile> ProfileList { get=> m_profileList; set=> m_profileList = value; }

        #endregion

        #region Commands

        public ICommand OnAddNewProfileButtonPressed { get; }

        #endregion

        #region Ctor
        public ChooseProfileViewModel()
        {
            m_jdataProvider = new JDataProvider();

            m_pathToProfiles = JDataProvider.pathToEnv + @"\DataBase\UserProfiles.json";

            m_profileList = new ObservableCollection<UserProfile>();

            m_profileList = m_jdataProvider.DeserializeObject<ObservableCollection<UserProfile>>(m_pathToProfiles);

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
            ProfileList.Add(new UserProfile(ProfileList.Count+1, "Enter your name Commander", true, StarFleetRanks.Cadet4thGrade, 0));
        }
        #endregion

        #endregion
    }
}
