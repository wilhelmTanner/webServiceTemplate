namespace Tanner.Template.Base.Test.Services;

public class TemplateBaseServiceTest
{
    private Mock<ITemplateSQLRepository> templateRepository;
    private Mock<IFakeEndpointClient> fakeEndpointClient;

    private TemplateBaseService service;

    public TemplateBaseServiceTest()
    {
        templateRepository = new Mock<ITemplateSQLRepository>();
        fakeEndpointClient = new Mock<IFakeEndpointClient>();

        service = new(templateRepository.Object, fakeEndpointClient.Object);
    }

    [Fact]
    public async Task Test_GetObjectByIdAsync()
    {
        int objectId = 1;
        ExampleObject example = new ExampleObject()
        {
            Id = objectId,
            Descripcion = "Test",
            Valor = 1000000
        };

        templateRepository.Setup(a => a.GetExampleByIdAsync(1)).ReturnsAsync(example);
        ExampleObject result = await service.GetExampleByIdAsync(objectId);


        Assert.Equal(example.Descripcion, result.Descripcion);
        Assert.Equal(example.Id, result.Id);
        Assert.Equal(example.Valor, result.Valor);
    }

    [Fact]
    public void Test_SayHi()
    {
        string result = service.SayHi();
        Assert.Equal("Hola mundo!", result);
    }

    [Fact]
    public void Test_Sum()
    {
        int a = 4;
        int b = 3;
        int sum = a + b;
        int result = service.Sum(a, b);


        Assert.Equal(sum, result);
    }
}