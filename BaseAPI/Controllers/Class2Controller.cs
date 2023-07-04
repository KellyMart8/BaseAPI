using AutoMapper;
using BaseAPI.Data;
using BaseAPI.Models.Dto;
using BaseAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Class2Controller : ControllerBase
    {
        private readonly ILogger<Class2Controller> _logger;
        private readonly IClass2Repository _cpRepo;
        private readonly IMapper _mapper;

        public Class2Controller(ILogger<Class2Controller> logger, IClass2Repository c2Repo, IMapper mapper)
        {
            _logger = logger;
            _cpRepo = c2Repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Class2Dto>>> GetE()
        {
            _logger.LogInformation("Obtener ");

            var elementosList = await _cpRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<Class1Dto>>(elementosList));
        }

        [HttpGet("{id:int}", Name = "GetE")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Class2Dto>> GetE(int id)
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

            return Ok(_mapper.Map<Class2Dto>(elemnto));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Class2Dto>> AddE([FromBody] Class2CreateDto class2CreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _cpRepo.Get(s => s.Name.ToLower() == class2CreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("PropExiste", "¡El elemento con ese propiedad ya existe!");
                return BadRequest(ModelState);
            }

            if (class2CreateDto == null)
            {
                return BadRequest(class2CreateDto);
            }

            Class2 modelo = _mapper.Map<Class2>(class2CreateDto);

            //Student modelo = new()
            //{
            //    StudentName = studentCreateDto.StudentName
            //};

            await _cpRepo.Add(modelo);

            return CreatedAtRoute("GetE", new { id = modelo.Id }, modelo);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteE(int id)
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
        public async Task<IActionResult> UpdateClass2(int id, [FromBody] Class2UpdateDto class2UpdateDto)
        {
            if (class2UpdateDto == null || id != class2UpdateDto.Id)
            {
                return BadRequest();
            }

            Class2 modelo = _mapper.Map<Class2>(class2UpdateDto);

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
        public async Task<IActionResult> UpdatePartialE(int id, JsonPatchDocument<Class2UpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var clase2 = await _cpRepo.Get(s => s.Id == id, tracked: false);

            Class2UpdateDto class2UpdateDto = _mapper.Map<Class2UpdateDto>(clase2);
            //StudentUpdateDto studentUpdateDto = new()
            //{
            //    StudentId = student.StudentId,
            //    StudentName = student.StudentName
            //};
            if (clase2 == null) return BadRequest();

            patchDto.ApplyTo(class2UpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Class2 modelo = _mapper.Map<Class2>(class2UpdateDto);
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
