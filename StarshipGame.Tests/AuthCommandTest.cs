namespace StarshipGame.Tests;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;
using Moq;

public class AuthCommandTests
{
    public AuthCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", 
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }

    [Fact]
    public void PlayerCanInteract_WhenObjectIsInAllowedList()
    {
        // Arrange
        var playerId = 42;
        var targetObjectId = (object)100;

        var playerMock = new Mock<IObjectInfo>();
        playerMock.Setup(p => p.PlayerId).Returns(playerId);

        var targetMock = new Mock<IObjectInfo>();
        targetMock.Setup(t => t.ObjectId).Returns(targetObjectId);
        targetMock.Setup(t => t.PlayerId).Returns(playerId);
        
        var allowedObjectMock = new Mock<IObjectInfo>();
        allowedObjectMock.Setup(a => a.PlayerId).Returns(playerId);
        allowedObjectMock.Setup(a => a.ObjectId).Returns(targetObjectId);

        IoC.Resolve<ICommand>("IoC.Register", "GameItem.GetByPlayerId", 
            (object[] args) => args[0].Equals(playerId) ? new[] { allowedObjectMock.Object } : Enumerable.Empty<IObjectInfo>()
        ).Execute();

        var authCommand = new AuthCommand(playerMock.Object, targetMock.Object);

        // Act & Assert
        var exception = Record.Exception(() => authCommand.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void PlayerCannotInteract_WhenObjectNotInAllowedList()
    {
        // Arrange
        var playerId = 42;
        var targetObjectId = (object)100;

        var playerMock = new Mock<IObjectInfo>();
        playerMock.Setup(p => p.PlayerId).Returns(playerId);

        var targetMock = new Mock<IObjectInfo>();
        targetMock.Setup(t => t.ObjectId).Returns(targetObjectId);
        targetMock.Setup(t => t.PlayerId).Returns(playerId);

        IoC.Resolve<ICommand>("IoC.Register", "GameItem.GetByPlayerId", 
            (object[] args) => args[0].Equals(playerId) ? Enumerable.Empty<IObjectInfo>() : throw new Exception("Wrong player ID")
        ).Execute();

        var authCommand = new AuthCommand(playerMock.Object, targetMock.Object);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => authCommand.Execute());
    }
}
