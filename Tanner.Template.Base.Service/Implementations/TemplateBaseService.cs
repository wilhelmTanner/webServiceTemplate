namespace Tanner.Template.Base.Service.Implementations;

public class TemplateBaseService : ITemplateBaseService
{
    private readonly ITemplateSQLRepository _templateRepository;
    private readonly IFakeEndpointClient _fakeEndpointClient;

    public TemplateBaseService(ITemplateSQLRepository templateRepository,
        IFakeEndpointClient fakeEndpointClient)
    {
        _templateRepository = templateRepository;
        _fakeEndpointClient = fakeEndpointClient;
    }

    /// <summary>
    /// Obtiene todos los ejemplos
    /// </summary>
    /// <returns></returns>
    public async Task<(IEnumerable<ExampleObject>, int)> GetAllExamplesAsync(ExampleObjectParameters exampleObjectParams)
    {
        (IEnumerable<ExampleObject> items, int total) = await _templateRepository.GetAllExamplesAsync(exampleObjectParams);
        return (items, total);
    }

    /// <summary>
    /// Obtiene el objeto en base al Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<ExampleObject> GetExampleByIdAsync(int id)
    {
        return _templateRepository.GetExampleByIdAsync(id);
    }

    public async Task<IEnumerable<ExternalServiceResponse>> GetExternalDataAsync()
    {
        IEnumerable<ExternalServiceResponse> response = await _fakeEndpointClient.GetAllData();
        return response;
    }

    /// <summary>
    /// Inserta Ejemplo
    /// </summary>
    /// <param name="exampleObject"></param>
    /// <returns></returns>
    public async Task<ExampleObject> InsertExampleAsync(ExampleRequest exampleObject)
    {
        ExampleObject newExample = new ExampleObject()
        {
            Descripcion = exampleObject.Description,
            Valor = exampleObject.Value
        };

        newExample.Id=  await _templateRepository.InsertExampleAsync(newExample);
        return newExample;
    }

    /// <summary>
    /// Obtiene el mensaje de saludo
    /// </summary>
    /// <returns></returns>
    public string SayHi()
    {
        return "Hola mundo!";
    }


    /// <summary>
    /// Suma dos números enteros
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Sum(int a, int b)
    {
        return a + b;
    }


}