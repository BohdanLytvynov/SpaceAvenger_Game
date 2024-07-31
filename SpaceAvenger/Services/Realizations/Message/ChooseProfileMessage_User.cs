using Models.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAvenger.Services.Realizations.Message
{
    internal class ChooseProfileMessage_User : Message<User>
    {
        public ChooseProfileMessage_User(User user) : base(user)
        {
                
        }
    }
}
