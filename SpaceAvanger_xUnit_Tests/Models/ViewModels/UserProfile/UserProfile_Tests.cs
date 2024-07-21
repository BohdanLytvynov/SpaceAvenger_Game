using Models.ViewModels.User;
namespace SpaceAvanger_xUnit_Tests.Models.ViewModels.UserProfileTests
{
    public class UserProfile_Tests
    {
        [Fact]
        public void Method_Equals_Success_WhenTwoObjectsAreEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() {Id = p1.Id };

            // Act
            var r = p1.Equals(p2);

            // Assert
            Assert.True(r);
        }

        [Fact]
        public void Method_Equals_Fails_WhenObjectsAreNotEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() { Id = Guid.NewGuid() };

            // Act
            var r = p1.Equals(p2);

            // Assert
            Assert.False(r);
        }

        [Fact]
        public void Operator_Equal_SuccessWhenObjectsAreEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() { Id = p1.Id };

            // Act
            var r = p1 == p2;

            // Assert
            Assert.True(r);
        }

        [Fact]
        public void Operator_Equal_FailWhenObjectsAreNotEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() { Id = Guid.NewGuid() };

            // Act
            var r = p1 == p2;

            // Assert
            Assert.False(r);
        }

        [Fact]
        public void Operator_NotEqual_SuccessWhenObjectsAreNotEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() { Id = Guid.NewGuid() };

            // Act
            var r = p1 != p2;

            // Assert
            Assert.True(r);
        }

        [Fact]
        public void Operator_NotEqual_FailWhenObjectsAreEqual()
        {
            // Arrange
            UserProfile p1 = new UserProfile() { Id = Guid.NewGuid() };

            UserProfile p2 = new UserProfile() { Id = p1.Id };

            // Act
            var r = p1 != p2;

            // Assert
            Assert.False(r);
        }
    }   
}