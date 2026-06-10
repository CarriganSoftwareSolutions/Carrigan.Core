using Carrigan.Core.DataStructures;

namespace Carrigan.Core.Test.DataStructures;

public class BlackWhiteListTests
{
    [Fact]
    public void OneInConstructorAllowed()
    {
        BlackWhiteList<string> blackWhiteList = new(["1"], []);
        Assert.True(blackWhiteList.IsAllowed("1"));
    }

    [Fact]
    public void ZeroInConstructorNotAllowed()
    {
        BlackWhiteList<string> blackWhiteList = new();
        Assert.False(blackWhiteList.IsAllowed("1"));
    }

    [Fact]
    public void MultipleInConstructorAllowed()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2, 3, 4]);
        Assert.False(blackWhiteList.IsAllowed(5));
    }

    [Fact]
    public void MultipleInConstructorNotAllowed()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2, 3, 4]);
        Assert.True(blackWhiteList.IsAllowed(3));
    }

    [Fact]
    public void WhiteBlackConflict()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2, 3, 4]);
        try
        {
            blackWhiteList.AddBlackListValues(1);
            Assert.Fail();
        }
        catch (InvalidOperationException ex)
        {
            Assert.Equal($"The item {1} cannot be added to the blacklist because it already exists in the whitelist.", ex.Message);
        }
    }

    [Fact]
    public void BlackWhileConflict()
    {
        BlackWhiteList<int> blackWhiteList = new([]);
        blackWhiteList.AddBlackListValues(1);
        try
        {
            blackWhiteList.AddWhiteListValues(1);
            Assert.Fail();
        }
        catch (InvalidOperationException ex)
        {
            Assert.Equal($"The item {1} cannot be added to the whitelist because it already exists in the blacklist.", ex.Message);
        }
    }

    [Fact]
    public void MultipleAllowed1()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2]);
        blackWhiteList.AddWhiteListValues(3, 4);
        blackWhiteList.AddBlackListValues(5, 6);
        Assert.True(blackWhiteList.IsAllowed(1));
    }

    [Fact]
    public void WhiteBlackConstructor()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2, 3, 4], [5,6]);
        Assert.True(blackWhiteList.IsAllowed(1));
        Assert.True(blackWhiteList.IsAllowed(2));
        Assert.True(blackWhiteList.IsAllowed(3));
        Assert.True(blackWhiteList.IsAllowed(4));
        Assert.False(blackWhiteList.IsAllowed(5));
        Assert.False(blackWhiteList.IsAllowed(6));
    }

    [Fact]
    public void MultipleAllowed3()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2]);
        blackWhiteList.AddWhiteListValues(3, 4);
        blackWhiteList.AddBlackListValues(5, 6);
        Assert.True(blackWhiteList.IsAllowed(3));
    }

    [Fact]
    public void MultipleBlocked()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2]);
        blackWhiteList.AddWhiteListValues(3, 4);
        blackWhiteList.AddBlackListValues(5, 6);
        Assert.False(blackWhiteList.IsAllowed(6));
    }

    [Fact]
    public void ArrayAllowed1()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2] );
        blackWhiteList.AddWhiteListValues([3, 4]);
        blackWhiteList.AddBlackListValues([5, 6]);
        Assert.True(blackWhiteList.IsAllowed(1));
    }

    [Fact]
    public void ArrayAllowed3()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2]);
        blackWhiteList.AddWhiteListValues([3, 4]);
        blackWhiteList.AddBlackListValues([5, 6]);
        Assert.True(blackWhiteList.IsAllowed(3));
    }

    [Fact]
    public void ArrayBlocked()
    {
        BlackWhiteList<int> blackWhiteList = new([1, 2]);
        blackWhiteList.AddWhiteListValues([3, 4]);
        blackWhiteList.AddBlackListValues([5, 6]);
        Assert.False(blackWhiteList.IsAllowed(6));
    }
}
