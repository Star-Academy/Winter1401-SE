using System.Runtime.InteropServices;
using SimpleCalculator.Business.Enums;
using SimpleCalculator.Business.OperatorBusiness;
using SimpleCalculator.Business.OperatorBusiness.Operators;

namespace TestProject;

public class OperatorProviderTest
{
    private readonly OperatorProvider _sut;

    public OperatorProviderTest()
    {
        _sut = new OperatorProvider();
    }//

    #region DifferentOperators

    [Theory]
    [InlineData(OperatorEnum.sum)]
    public void OperatorProvider_Sum_ReturnSumOperator(OperatorEnum operatorEnum)
    {
        Assert.IsType<SumOperator>(_sut.GetOperator(operatorEnum));
    }
    
    
    [Theory]
    [InlineData(OperatorEnum.sub)]
    public void OperatorProvider_Sub_ReturnSubOperator(OperatorEnum operatorEnum)
    {
        Assert.IsType<SubOperator>(_sut.GetOperator(operatorEnum));
    }
    
    
    [Theory]
    [InlineData(OperatorEnum.multiply)]
    public void OperatorProvider_Mult_ReturnMultOperator(OperatorEnum operatorEnum)
    {
        Assert.IsType<MultiplyOperator>(_sut.GetOperator(operatorEnum));
    }
    
    
    [Theory]
    [InlineData(OperatorEnum.division)]
    public void OperatorProvider_Div_ReturnDivOperator(OperatorEnum operatorEnum)
    {
        Assert.IsType<DivisionOperator>(_sut.GetOperator(operatorEnum));
    }

    [Theory]
    [InlineData((OperatorEnum)8)]
    public void OperatorProvider_NotSupported_ReturnException(OperatorEnum operatorEnum)
    {
        Assert.Throws<NotSupportedException>(() =>_sut.GetOperator(operatorEnum));
    }
    
    #endregion
}