using Models.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    internal class UserProfileInfoViewModel : ViewModelBase
    {
        #region Fields
        private string m_userName;

        private StarFleetRanks m_Rank;

        private DateTime m_enlisted;

        private int m_missionsCount;

        private float m_Points;
        #endregion

        #region Properties

        #endregion

        #region Ctor
        public UserProfileInfoViewModel()
        {
            #region Init Fields

            m_userName = string.Empty;

            #endregion
        }
        #endregion

        #region Methods

        #endregion
    }
}
