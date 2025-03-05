using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class IoCRegRepositoryTest
{
    public IoCRegRepositoryTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        new IoCRegRepository().Execute();
    }

    [Fact]
    public void ObjectAddToRepositoryAndFindByIdAndPlayer()
    {
        Mock<IObjectInfo> objectInfo = new Mock<IObjectInfo>();
        objectInfo.Setup(m => m.ObjectId).Returns("ship");
        objectInfo.Setup(m => m.PlayerId).Returns("player");

        IoC.Resolve<ICommand>("GameItem.Add", objectInfo.Object).Execute();
        var objectById = IoC.Resolve<IObjectInfo>("GameItem.GetByObjectId", "ship");
        var objectByPlayer = IoC.Resolve<IObjectInfo[]>("GameItem.GetByPlayerId", "player")[0];

        Assert.Equal(objectInfo.Object, objectById);
        Assert.Equal(objectInfo.Object, objectByPlayer);
    }

    [Fact]
    public void ObjectWithoutObjectIdThrows()
    {
        Mock<IObjectInfo> objectInfo = new Mock<IObjectInfo>();
        objectInfo.SetupGet(m => m.ObjectId).Throws(new InvalidOperationException());

        Assert.Throws<InvalidOperationException>(() => 
            IoC.Resolve<ICommand>("GameItem.Add", objectInfo.Object).Execute());
    }
    
    [Fact]
    public void ObjectWithoutPlayerIdThrows()
    {
        Mock<IObjectInfo> objectInfo = new Mock<IObjectInfo>();
        objectInfo.Setup(m => m.ObjectId).Returns("ship");
        objectInfo.SetupGet(m => m.PlayerId).Throws(new InvalidOperationException());

        Assert.Throws<InvalidOperationException>(() => 
            IoC.Resolve<ICommand>("GameItem.Add", objectInfo.Object).Execute());
    }
}