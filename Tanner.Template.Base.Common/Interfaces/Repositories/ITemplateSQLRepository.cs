namespace Tanner.Template.Base.Common.Interfaces.Repositories;

public interface ITemplateSQLRepository
{
    /// <summary>
    /// Se inserta example.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task<int> InsertExampleAsync(ExampleObject item);

    /// <summary>
    /// Se actualiza example.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task UpdateExampleAsync(ExampleObject item);

    /// <summary>
    /// Obtener example por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ExampleObject> GetExampleByIdAsync(int id);

    /// <summary>
    /// Obtener todos los examples.
    /// </summary>
    /// <param name="itemObjectParams"></param>
    /// <returns></returns>
    Task<(IEnumerable<ExampleObject>, int)> GetAllExamplesAsync(ExampleObjectParameters itemObjectParams);
}