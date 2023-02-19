using SimpleCalculator.Business.OperatorBusiness.Operators;

namespace TestProject;

public class DivisionTest
{
    private readonly DivisionOperator _sut;

    public DivisionTest()
    {
        _sut = new DivisionOperator();
    }

    #region DivisionScenarios

    [Theory]
    [InlineData(10, 0)]
    [InlineData(-20, 0)]
    [InlineData(0, 0)]
    public void Division_byZero_ReturnException(int first, int second)
    {
        Assert.Throws<DivideByZeroException>(() => _sut.Calculate(first, second));
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(0, -50, 0)]
    public void Division_ZeroByNonZero_ReturnZero(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(10, 10, 1)]
    [InlineData(250, 50, 5)]
    public void Division_PositiveNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(-10, 10, -1)]
    [InlineData(300, -50, -6)]
    public void Division_PositiveAndNegative_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(-10, -10, 1)]
    [InlineData(-5000, -50, 100)]
    public void Division_NegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }

    #endregion
}