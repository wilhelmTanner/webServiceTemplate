namespace Tanner.Template.Base.Test.Controllers;

public class TemplateSQLControllerTest
{
    private Mock<ITemplateBaseService> service;

    public TemplateSQLControllerTest()
    {
        service = new Mock<ITemplateBaseService>();
    }


    [Fact]
    public void Test_Sayhi()
    {
        //Arrange
        string message = "hola Mundo";
        service.Setup(a => a.SayHi()).Returns(message);

        //Act
        TemplateSQLController controller = new TemplateSQLController(service.Object);
        string result = controller.SayHi();

        //Assert
        Assert.Equal(message, result);
    }

    [Fact]
    public void Test_SumIntegers()
    {
        //Arrange
        int a = 1;
        int b = 4;
        int sum = a + b;
        service.Setup(setup => setup.Sum(a, b)).Returns(sum);

        //Act
        TemplateSQLController controller = new(service.Object);
        int result = controller.Sum(a, b);

        //Assert
        Assert.Equal(sum, result);
    }

}