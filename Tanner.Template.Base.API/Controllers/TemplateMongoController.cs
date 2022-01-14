// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tanner.Template.Base.API.Controllers
{
    /// <summary>
    /// Controlador Mongo
    /// </summary>
    [SwaggerTag("Endpoints de servicio X")]
    [Route("api/template/mongo")]
    [ApiController]
    public class TemplateMongoController : CustomControllerBase
    {
        private readonly ITemplateMongoService _services;
        private const string RouteGet = "GeItemObject";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Servicio para mongo</param>
        public TemplateMongoController(ITemplateMongoService service)
        {
            _services = service;
        }

        /// <summary>
        /// Obtiene item por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = RouteGet)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<ItemObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseObjectResponse<ItemObjectResponse>>> GetItemByIdAsync(string id)
        {
            ItemObject result = await _services.GetItemByIdAsync(id);
            return CustomOk(new ItemObjectResponse(result));
        }

        /// <summary>
        /// Obtiene todos los items según paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseListResponse<ItemObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseListResponse<ItemObjectResponse>>> GetAllAsync([FromQuery] ItemObjectParameters itemObjectParams)
        {
            (IEnumerable<ItemObject> result, int total) = await _services.GetAllAsync(itemObjectParams);
            IEnumerable<ItemObjectResponse> output = result.Select(e => new ItemObjectResponse(e));

            return CustomOk(output.ToList(), total);
        }

        /// <summary>
        /// Inserta un nuevo item en la BD
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse(200, "Success", typeof(BaseObjectResponse<ItemObjectResponse>))]
        public async Task<ActionResult<BaseObjectResponse<ItemObjectResponse>>> InsertItemAsync([FromBody]ItemObjectRequest item)
        {
            ItemObject newItem = new ItemObject()
            {
                Active = item.Active,
                Name = item.Name,
                Year = item.Year
            };

            newItem = await _services.InsertItemAsync(newItem);
            ItemObjectResponse response = new(newItem);

            return CustomCreate(RouteGet, response, new { id = response.Id }, "Se creó el ItemObject correctamente."); ;
        }

        /// <summary>
        /// Actualiza item en la BD
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse(200, "Success", typeof(BaseObjectResponse<ItemObjectResponse>))]
        public async Task<ActionResult<BaseObjectResponse<ItemObjectResponse>>> UpdateItemAsync(string id, [FromBody] ItemObjectRequest item)
        {
            ItemObject newItem = new()
            {
                Active = item.Active,
                Name = item.Name,
                Year = item.Year,
                Id = id
            };

            newItem = await _services.InsertItemAsync(newItem);
            ItemObjectResponse response = new(newItem);

            return CustomOk(response);
        }
    }
}
