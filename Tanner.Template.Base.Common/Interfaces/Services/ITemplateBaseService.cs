namespace Tanner.Template.Base.Common.Interfaces.Services;

public interface ITemplateBaseService
{
    /// <summary>
    /// Obtiene el mensaje de saludo
    /// </summary>
    /// <returns></returns>
    string SayHi();

    /// <summary>
    /// Obtiene el objeto en base al Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ExampleObject> GetExampleByIdAsync(int id);

    /// <summary>
    /// Inserta objeto de ejemplo
    /// </summary>
    /// <param name="exampleObject"></param>
    /// <returns></returns>
    Task<ExampleObject> InsertExampleAsync(ExampleRequest exampleObject);

    /// <summary>
    /// Obtiene todos los ejemplos
    /// </summary>
    /// <param name="exampleObjectParams"></param>
    /// <returns></returns>
    Task<(IEnumerable<ExampleObject>, int)> GetAllExamplesAsync(ExampleObjectParameters exampleObjectParams);

    /// <summary>
    /// Suma dos números enteros
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int Sum(int a, int b);

    /// <summary>
    /// Obtiene datos de servicio externo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IEnumerable<ExternalServiceResponse>> GetExternalDataAsync();
}