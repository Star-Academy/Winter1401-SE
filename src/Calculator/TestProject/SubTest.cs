using SimpleCalculator.Business.OperatorBusiness.Operators;

namespace TestProject;

public class SubTest
{
    private readonly SubOperator _sut;

    public SubTest()
    {
        _sut = new SubOperator();
    }

    #region SubtractionScenarions
    
    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(30, 10, 20)]
    public void Sub_PositiveNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-10, 10, -20)]
    [InlineData(-20, 10, -30)]
    [InlineData(20, -5, 25)]
    public void Sub_PositiveAndNegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-10, -10, 0)]
    [InlineData(-20, -10, -10)]
    [InlineData(-30, -60, 30)]
    public void Sub_NegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    public void Sub_Two_Zeros(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }
    
    #endregion
}