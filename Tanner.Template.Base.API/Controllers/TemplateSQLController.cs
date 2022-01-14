// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tanner.Template.Base.API.Controllers
{
    /// <summary>
    /// Controlador SQL
    /// </summary>
    [SwaggerTag("Endpoints de servicio X")]
    [Route("api/template/sql")]
    [ApiController]
    public class TemplateSQLController : CustomControllerBase
    {
        private readonly ITemplateBaseService _services;
        private const string RouteGet = "GeExampleObject";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Servicio para SQL</param>
        public TemplateSQLController(ITemplateBaseService service)
        {
            _services = service;
        }

        /// <summary>
        /// Método Hola Mundo
        /// </summary>
        /// <returns></returns>
        [HttpGet("hello")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public string SayHi()
        {
            return _services.SayHi();
        }

        /// <summary>
        /// Obtiene example por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = RouteGet)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<ExampleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseObjectResponse<ExampleResponse>>> GetExampleAsync(int id)
        {
            ExampleObject result = await _services.GetExampleByIdAsync(id);
            return CustomOk(new ExampleResponse(result));
        }

        /// <summary>
        /// Obtiene todos los examples según paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<ExampleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseListResponse<ExampleResponse>>> GetAllAsync([FromQuery] ExampleObjectParameters exampleObjectParams)
        {
            (IEnumerable<ExampleObject> result, int total) =  await _services.GetAllExamplesAsync(exampleObjectParams);
            IEnumerable<ExampleResponse> response = result.Select(e => new ExampleResponse(e));

            return CustomOk(response.ToList(), total);
        }

        /// <summary>
        /// Inserta un nuevo example en la BD
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<ExampleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseObjectResponse<ExampleResponse>>> InsertExampleAsync([FromBody]ExampleRequest example)
        {
            ExampleObject newExample = await _services.InsertExampleAsync(example);
            ExampleResponse response = new(newExample);

            return CustomCreate(RouteGet, response, new { id = response.Id }, "Se creó el ExampleObject correctamente.");
        }

        /// <summary>
        /// Suma dos números enteros
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("sum")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(int))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public int Sum(int a, int b)
        {
            return _services.Sum(a, b);
        }

        /// <summary>
        /// Obtiene información desde un endpoint externo de pruebas
        /// </summary>
        /// <returns></returns>
        [HttpGet("externaldata")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<ExternalDataResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<IEnumerable<ExternalDataResponse>>> GetExternalDataAsync()
        {
            IEnumerable<ExternalServiceResponse> examples = await _services.GetExternalDataAsync();
            IEnumerable<ExternalDataResponse> output = examples.Select(e => new ExternalDataResponse(e));

            return Ok(output);
        }
    }
}
