namespace StarshipGame.Test;
using Moq;

public class TorpedoTests
{
    [Fact]
    public void CorrectTorpedoCreate()
    {
        var pos = new Vector (new int[] {0, 0});
        var vel = new Vector (new int[] {0, 0});

        var torpedo = new Torpedo(pos, vel);

        Assert.NotNull(torpedo);
        Assert.IsType<Torpedo>(torpedo);
    }
}
