using SimpleCalculator.Business.OperatorBusiness.Operators;

namespace TestProject;

public class MultiplyTest
{
    private readonly MultiplyOperator _sut;

    public MultiplyTest()
    {
        _sut = new MultiplyOperator();
    }

    #region MultiplyScenarios

    [Theory]
    [InlineData(10, 20, 200)]
    [InlineData(7, 9, 63)]
    public void Mult_PositiveNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(-10, -20, 200)]
    [InlineData(-7, -9, 63)]
    public void Mult_NegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(-10, 20, -200)]
    [InlineData(7, -9, -63)]
    public void Mult_PositiveAndNegativeNumbers_ReturnTrue(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(0, 20, 0)]
    [InlineData(-7, 0, 0)]
    public void Mult_ZeroWithOtherNumbers_ReturnZero(int first, int second, int expected)
    {
        var result = _sut.Calculate(first, second);
        
        Assert.Equal(expected, result);
    }

    #endregion
}