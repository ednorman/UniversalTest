using Xunit;
using FluentAssertions;

namespace GRM_Dev_Test.UnitTests
{
    public class ProductServiceTests
    {
        ProductService _productService;
        public ProductServiceTests()
        {
            _productService = PartnerProductService.Instance;
        }
        [Fact]
        public void Test1()
        {
            //Arrange
            var input = "ITunes 1st March 2012";
            var expected = @" Artist|Title|Usage|StartDate|EndDate
 Monkey Claw|Black Mountain|digital download|1st Feb 2012|
 Monkey Claw|Motor Mouth|digital download|1st Mar 2011|
 Tinie Tempah|Frisky (Live from SoHo)|digital download|1st Feb 2012|
 Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012|
";
            //Act
            var result = _productService.TemplateMethod(input);
            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Test2()
        {
            //Arrange
            var input = "YouTube 1st April 2012";
            var expected = @" Artist|Title|Usage|StartDate|EndDate
 Monkey Claw|Motor Mouth|streaming|1st Mar 2011|
 Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|
";
            //Act
            var result = _productService.TemplateMethod(input);
            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Test3()
        {
            //Arrange
            var input = "YouTube 27th Dec 2012";
            var expected = @" Artist|Title|Usage|StartDate|EndDate
 Monkey Claw|Christmas Special|streaming|25st Dec 2012|31st Dec 2012
 Monkey Claw|Iron Horse|streaming|1st June 2012|
 Monkey Claw|Motor Mouth|streaming|1st Mar 2011|
 Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012|
";
            //Act
            var result = _productService.TemplateMethod(input);
            //Assert
            result.Should().Be(expected);
        }
    }
}
