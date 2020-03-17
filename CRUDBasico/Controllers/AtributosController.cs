using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CRUDBasico.Infrastructure.BD.Repository;
using CRUDBasico.Infrastructure.Specification;
using CRUDBasico.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace CRUDBasico.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AtributosController : ControllerBase
    {

        private readonly ILogger<AtributosController> _logger;
        private readonly IAtributosSpecification _specification;
        private readonly IAtributosRepository _repo;
        private readonly IMapper _mapper;

        public AtributosController(
            ILogger<AtributosController> logger,
            IAtributosSpecification specification,
            IAtributosRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _specification = specification;
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Atributos
        /// </summary>
        /// <returns>Lista de atributos</returns>
        [Route("/atributos")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AtributoDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAtributos()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Atributo, AtributoDto>());
            List<Atributo> atributos = _repo.GetElements();
            List<AtributoDto> peopleVM = _mapper.Map<List<Atributo>, List<AtributoDto>>(atributos);

            return Ok(peopleVM);
        }

        /// <summary>
        /// Get atributos ID.
        /// </summary>
        /// <param name="atributoID">Atributo ID.</param>
        /// <returns>List of atributes</returns>
        [Route("/atributos/{atributoID}")]
        [HttpGet]
        [ProducesResponseType(typeof(AtributoDto), (int)HttpStatusCode.OK)]
        public IActionResult GetAtributoID(int atributoID)
        {
            Atributo atributo = _repo.GetElement(_specification.GetAtributoById(atributoID));
            if (atributo != null && atributo.atributoId == atributoID)
            {
                AtributoDto atributodto = _mapper.Map<Atributo, AtributoDto>(atributo);
                return Ok(atributodto);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Add atributo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /atributos
        ///     {
        ///        "AtributoId": 1,
        ///        "Descripcion": "Liquidame"
        ///     }
        /// </remarks>
        /// <param name="request">AtributoRequest</param>
        /// <returns>Atributo Creado</returns>
        [Route("/atributos")]
        [HttpPut]
        [ProducesResponseType(typeof(AtributoDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterAtributo([FromBody]AtributoDto request)
        {
            Atributo tt = _mapper.Map<AtributoDto, Atributo>(request);
            Atributo atributo = _repo.GetElement(_specification.GetAtributoById(request.id));
            if (atributo == null)
            {
                await _repo.AddAsync(tt);
                return Created(string.Empty, request);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Modificar atributo
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /atributos
        ///     {
        ///        "AtributoId": 1,
        ///        "Descripcion": "Liquidame"
        ///     }
        /// </remarks>
        /// <param name="request">AtributoRequest</param>
        /// <returns>Atributo Creado</returns>
        [Route("/atributos")]
        [HttpPost]
        [ProducesResponseType(typeof(AtributoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ModificarAtributo([FromBody]AtributoDto request)
        {
            Atributo atributoExistente = _mapper.Map<AtributoDto, Atributo>(request);
            Atributo atributo = _repo.GetElement(_specification.GetAtributoById(request.id));
            if (atributo != null)
            {
                Atributo modificado = await _repo.ModifyAsync(atributoExistente);
                AtributoDto resultado = _mapper.Map<Atributo, AtributoDto>(modificado);
                return Ok(resultado);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete atributo
        /// </summary>
        /// <param name="atributoID">Atributo ID.</param>
        /// <returns></returns>
        [Route("/atributos")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAtributo(int atributoID)
        {
            //var atr = await _mediator.Send(new AtributosDeleteCommand(atributoID));
            Atributo atributoExistente = _repo.GetElement(_specification.GetAtributoById(atributoID));
            if (atributoExistente != null && atributoExistente.atributoId == atributoID)
            {
                AtributoDto atributoExistenteDto = _mapper.Map<AtributoDto>(atributoExistente);
                await _repo.RemoveAsync(atributoExistente);
                return Ok(atributoExistenteDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
