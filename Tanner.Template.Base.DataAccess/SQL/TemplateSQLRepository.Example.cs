namespace Tanner.Template.Base.DataAccess.SQL;

public partial class TemplateSQLRepository
{
    /// <summary>
    /// Inserta un registro
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task<int> InsertExampleAsync(ExampleObject item)
    {
        var query = @" INSERT INTO EXAMPLE(Descripcion, Valor)
                            VALUES(@Descripcion, @Valor);

                            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = new SqlConnection(this.GetConnectionString());
        await connection.OpenAsync();
        IEnumerable<int> result = await connection.QueryAsync<int>(query, item);

        return result.Single();
    }

    /// <summary>
    /// Actualiza un registro
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task UpdateExampleAsync(ExampleObject item)
    {
        var query = @" UPDATE EXAMPLE
                           SET Descripcion = @Descripcion,
                               Valor = @Valor
                           WHERE Id = @Id";

        using var connection = new SqlConnection(this.GetConnectionString());
        await connection.OpenAsync();
        await connection.QueryAsync(query, item);
    }

    /// <summary>
    /// Obtiene un registro 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ExampleObject> GetExampleByIdAsync(int id)
    {
        var query = @" SELECT Id, Descripcion, Valor FROM EXAMPLE WHERE Id = @id";

        using var connection = new SqlConnection(this.GetConnectionString());
        await connection.OpenAsync();
        IEnumerable<ExampleObject> items = await connection.QueryAsync<ExampleObject>(query, new { id });

        return items.ToDefaultList().FirstOrDefault();
    }


    /// <summary>
    /// Obtiene todos los registros
    /// Para el filtrado y paginado se utilizó a modo de ejemplo
    /// la librería https://github.com/DapperLib/Dapper/tree/main/Dapper.SqlBuilder
    /// </summary>
    /// <param name="exampleObjectParameters"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<ExampleObject>, int)> GetAllExamplesAsync(ExampleObjectParameters exampleObjectParameters)
    {
        var start = exampleObjectParameters.Offset + 1;
        var finish= exampleObjectParameters.Offset + exampleObjectParameters.Limit;

        var builder = new SqlBuilder();

        var selectTemplate = builder.AddTemplate(@"SELECT X.* FROM (
            SELECT us.*, ROW_NUMBER() OVER (/**orderby**/) AS RowNumber 
            FROM EXAMPLE us 
            /**where**/
            ) AS X 
            WHERE RowNumber BETWEEN @start AND @finish", new { start, finish });

        var countTemplate = builder.AddTemplate(@"SELECT COUNT(*) FROM EXAMPLE /**where**/");

        builder.Where("Valor >= @minValue", new { minValue = (int)exampleObjectParameters.MinValue });
        builder.Where("Valor <= @maxValue", new { maxValue = (int)exampleObjectParameters.MaxValue });
        if (!string.IsNullOrEmpty(exampleObjectParameters.SearchTerm))
        {
            builder.Where("Descripcion LIKE @search", new { search = exampleObjectParameters.SearchTerm.ToSqlLikeSearch() });
        }

        if (string.IsNullOrWhiteSpace(exampleObjectParameters.Sort))
            builder.OrderBy("Id asc");

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ExampleObject>(exampleObjectParameters.Sort, OrderTypeEnum.Dapper);

        if (string.IsNullOrWhiteSpace(orderQuery))
            builder.OrderBy("Id asc");

        builder.OrderBy(orderQuery);

        using var connection = new SqlConnection(this.GetConnectionString());
        var items = await connection.QueryAsync<ExampleObject>(selectTemplate.RawSql, selectTemplate.Parameters);
        var total = await connection.ExecuteScalarAsync<int>(countTemplate.RawSql, countTemplate.Parameters);

        return (items, total);
    }
}