using Models.DAL.Entities.User;
using SpaceAvenger.ViewModels.UserProfile;

namespace SpaceAvanger_xUnit_Tests.Models.ViewModels.UserProfileTests
{
    public class UserProfile_Tests
    {
        [Fact]
        public void Method_Equals_Success_WhenTwoObjectsAreEqual()
        {
            // Arrange
            UserProfileVM p1 = new UserProfileVM(1, 
                Guid.NewGuid(), 
                "Test",
                true, StarFleetRanks.Cadet4thGrade, 0, DateTime.Now);

            UserProfileVM p2 = new UserProfileVM(2, 
                p1.User.Id, 
                "Test", 
                true, StarFleetRanks.Cadet4thGrade, 0, DateTime.Now);

            // Act
            var r = p1.Equals(p2);

            // Assert
            Assert.True(r);
        }

        [Fact]
        public void Method_Equals_Fails_WhenObjectsAreNotEqual()
        {
            // Arrange
            UserProfileVM p1 = new UserProfileVM(1,
                Guid.NewGuid(), 
                "Test", 
                true, StarFleetRanks.Cadet4thGrade, 0, DateTime.Now);

            UserProfileVM p2 = new UserProfileVM(2, 
                Guid.NewGuid(), 
                "Test", 
                true, StarFleetRanks.Cadet4thGrade, 0, DateTime.Now);

            // Act
            var r = p1.Equals(p2);

            // Assert
            Assert.False(r);
        }           
    }   
}