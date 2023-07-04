using AutoMapper;
using BaseAPI.Data;
//using BaseAPI.Filters;
//using BaseAPI.Helpers;
using BaseAPI.Models.Dto;
using BaseAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    //[TypeFilter(typeof(MySampleExceptionFilter))]
    //public class TestExceptionController : ControllerBase
    //{
    //    public string Get()
    //    {
    //        //throw new NotImplementedException();
    //        throw new MyException("My Exception");
    //    }
    //}

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClasePrincipalController : ControllerBase
    {
        private readonly ILogger<ClasePrincipalController> _logger;
        private readonly IClasePrincipalRepository _cpRepo;
        private readonly IMapper _mapper;

        public ClasePrincipalController(ILogger<ClasePrincipalController> logger, IClasePrincipalRepository cpRepo, IMapper mapper)
        {
            _logger = logger;
            _cpRepo = cpRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ClasePrincipalDto>>> GetElementos()
        {
            _logger.LogInformation("Obtener ");

            var elementosList = await _cpRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<ClasePrincipalDto>>(elementosList));
        }

        [HttpGet("{id:int}", Name = "GetElementos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClasePrincipalDto>> GetElementos(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al traer elementos con Id {id}");
                return BadRequest();
            }
            var elemnto = await _cpRepo.Get(s => s.Id == id);

            if (elemnto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasePrincipalDto>(elemnto));
        }
       

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClasePrincipalDto>> AddProp([FromBody] ClasePrincipalCreateDto clasePrincipalCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _cpRepo.Get(s => s.Prop.ToLower() == clasePrincipalCreateDto.Prop.ToLower()) != null)
            {
                ModelState.AddModelError("ElementosExiste", "¡El elemento con ese propiedad ya existe!");
                return BadRequest(ModelState);
            }

            if (clasePrincipalCreateDto == null)
            {
                return BadRequest(clasePrincipalCreateDto);
            }

            ClasePrincipal modelo = _mapper.Map<ClasePrincipal>(clasePrincipalCreateDto);

            //Student modelo = new()
            //{
            //    StudentName = studentCreateDto.StudentName
            //};

            await _cpRepo.Add(modelo);

            return CreatedAtRoute("GetElementos", new { id = modelo.Id }, modelo);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteElemento(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var elemento = await _cpRepo.Get(s => s.Id == id);

            if (elemento == null)
            {
                return NotFound();
            }

            _cpRepo.Remove(elemento);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClasePrincipal(int id, [FromBody] ClasePrincipalUpdateDto clasePrincipalUpdateDto)
        {
            if (clasePrincipalUpdateDto == null || id != clasePrincipalUpdateDto.Id)
            {
                return BadRequest();
            }

            ClasePrincipal modelo = _mapper.Map<ClasePrincipal>(clasePrincipalUpdateDto);

            //Student modelo = new()
            //{
            //    StudentId = studentUpdateDto.StudentId,
            //    StudentName = studentUpdateDto.StudentName
            //};

            _cpRepo.Update(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialProp(int id, JsonPatchDocument<ClasePrincipalUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var claseP = await _cpRepo.Get(s => s.Id == id, tracked: false);

            ClasePrincipalUpdateDto clasePrincipalUpdateDto = _mapper.Map<ClasePrincipalUpdateDto>(claseP);
            //StudentUpdateDto studentUpdateDto = new()
            //{
            //    StudentId = student.StudentId,
            //    StudentName = student.StudentName
            //};
            if (claseP == null) return BadRequest();

            patchDto.ApplyTo(clasePrincipalUpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClasePrincipal modelo = _mapper.Map<ClasePrincipal>(clasePrincipalUpdateDto);
            //Student modelo = new()
            //{
            //    StudentId = studentUpdateDto.StudentId,
            //    StudentName = studentUpdateDto.StudentName
            //};
            _cpRepo.Update(modelo);

            return NoContent();
        }
    }
}
