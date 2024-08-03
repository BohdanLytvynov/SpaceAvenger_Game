using Models.DAL.Entities.User;
using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Services.Interfaces.Message;
using SpaceAvenger.Services.Interfaces.MessageBus;
using SpaceAvenger.Services.Realizations.Message;
using SpaceAvenger.ViewModels.Base;
using SpaceAvenger.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    [ViewModelType(ViewModelUsage.Page)]
    internal class UserProfileInfo_ViewModel : SubscriptableViewModel, IDisposable
    {
        #region Fields
        private string m_userName;

        private StarFleetRanks m_Rank;

        private DateTime m_enlisted;

        private int m_missionsCount;

        private float m_Points;

        private bool m_male_Female;

        private Image m_userImage;

        private IMessageBus m_messageBus;
        
        #endregion

        #region Properties

        public string UserName 
        { get=> m_userName; set=> Set(ref m_userName, value); }

        public  StarFleetRanks Rank 
        { get=> m_Rank; set=> Set(ref m_Rank, value); }

        public DateTime Enlisted 
        { get=> m_enlisted; set => Set(ref m_enlisted, value); }

        public int MissionsCount 
        { get=> m_missionsCount; set=> Set(ref m_missionsCount, value); }

        public float Points 
        { get=> m_Points; set=> Set(ref m_Points, value); }

        public bool MaleFemale 
        { 
            get=> m_male_Female;
            set 
            {
                Set(ref m_male_Female, value);

                if (m_male_Female)// Male
                {
                    UserImage = LoadImageUsingUri("pack://siteoforigin:,,,/Images/Chars/Commanders/CommandersM.png", UriKind.RelativeOrAbsolute);
                }
                else
                {
                    UserImage = LoadImageUsingUri("pack://siteoforigin:,,,/Images/Chars/Commanders/CommandersF.png", UriKind.RelativeOrAbsolute);
                }
            } 
        }

        public Image UserImage 
        { get=> m_userImage; set=>Set(ref m_userImage, value); }

        #endregion
        public UserProfileInfo_ViewModel()
        {
            #region Init Fields

            m_userName = string.Empty;

            m_userImage = new Image();

            MaleFemale = false;
            
            #endregion
        }

        #region Ctor
        public UserProfileInfo_ViewModel(IMessageBus messageBus) : this()
        {
            m_messageBus = messageBus;

            #region Create Subscription

            Subscriptions.Add(m_messageBus.RegisterHandler<ChooseProfileMessage_User, User>(OnMessageRecieved));

            #endregion
        }
        #endregion

        #region Methods

        private void OnMessageRecieved(ChooseProfileMessage_User msg)
        { 
            UserName = msg.Content.UserName;
            MaleFemale = msg.Content.MaleFemale;
            Rank = msg.Content.Rank;
            Enlisted = msg.Content.CreatedDate;
            MissionsCount = msg.Content.MissionsCount;
            Points = msg.Content.Points;
        }

        private Image LoadImageUsingUri(string uri, UriKind kind)
        {             
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri, kind);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            Image image = new Image();
            image.Source = bitmap;
            return image;
        }

        public void Dispose()
        {
            Unsubscribe();
        }
        #endregion
    }
}
