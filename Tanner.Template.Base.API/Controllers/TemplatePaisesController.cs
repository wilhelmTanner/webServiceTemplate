
using CountryService;
using ServicioDemo;
using Tanner.Template.Base.DataAccess;
using Tanner.Template.Base.DataAccess.Oracle;

namespace Tanner.Template.Base.API.Controllers
{

    /// <summary>
    /// Controlador Mongo
    /// </summary>
    [SwaggerTag("Endpoints de servicio X")]
    [Route("api/web/services")]
    [ApiController]
    public class TemplatePaisesController : CustomControllerBase
    {
        private const string RouteGet = "GetOracleObject"; 

        private TemplatePaisesRepository _repository;
        public TemplatePaisesController() => _repository = new TemplatePaisesRepository();

        /// <summary>
        /// Obtiene item por Id
        /// </summary>
        [HttpGet]
        [Route("{id}", Name = RouteGet)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<ItemObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ItemObject> GetByName(string id)
        {
            string result = await _repository.GetName("PEPE");
            return new ItemObject { Id = "ssss", Name = "d" };
        }


        /// <summary>
        /// Obtiene pais por codigo
        /// </summary>
        [HttpGet]
        [Route("pais/{code}")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<Country>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseObjectResponse<Country>>> GetCountryByCode(string code)
        {
            string result = await _repository.GetCountryByCode(code);
            Country c =  new() { Code = code, Name = result };

            return CustomOk(c);
         }

        /// <summary>
        /// Obtiene los continentes
        /// </summary>
        [HttpGet]
        [Route("continents")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(BaseObjectResponse<tContinent[]>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(BaseErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<BaseObjectResponse<tContinent[]>>> GetContinents()
        {
            tContinent[] result = await _repository.GetContinents();
            return CustomOk(result);
        }

    }
}
