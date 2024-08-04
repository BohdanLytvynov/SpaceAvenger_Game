using Models.DAL.Entities.User;

namespace WPF.UI.Services.Realizations.Message
{
    internal class ChooseProfileMessage_User : Message<User>
    {
        public ChooseProfileMessage_User(User user) : base(user)
        {
                
        }
    }
}
