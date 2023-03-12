using SimpleCalculator.Business.OperatorBusiness.Operators;

namespace TestProject;

public class SumTest
{
    private readonly SumOperator _sut;

    public SumTest()
    {
        _sut = new SumOperator();
    }

    #region SumScenarios
    
    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(3, 2, 5)]
    public void Sum_PositiveNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-10, -20, -30)]
    [InlineData(-4, -1, -5)]
    public void Sum_NegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, -20, -10)]
    [InlineData(3, -2, 1)]
    public void Sum_PositiveAndNegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    public void Sum_Zeros_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);

        Assert.Equal(expected, result);
    }
    
    #endregion
}