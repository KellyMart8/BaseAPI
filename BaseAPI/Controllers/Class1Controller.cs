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
    public class Class1Controller : ControllerBase
    {
        private readonly ILogger<Class1Controller> _logger;
        private readonly IClass1Repository _cpRepo;
        private readonly IMapper _mapper;

        public Class1Controller(ILogger<Class1Controller> logger, IClass1Repository c1Repo, IMapper mapper)
        {
            _logger = logger;
            _cpRepo = c1Repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Class1Dto>>> Getprop()
        {
            _logger.LogInformation("Obtener ");

            var elementosList = await _cpRepo.GetAll();

            return Ok(_mapper.Map<IEnumerable<Class1Dto>>(elementosList));
        }

        [HttpGet("{id:int}", Name = "Getprop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Class1Dto>> GetElementos(int id)
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

            return Ok(_mapper.Map<Class1Dto>(elemnto));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Class1Dto>> AddProp([FromBody] Class1CreateDto class1CreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _cpRepo.Get(s => s.Name.ToLower() == class1CreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("PropExiste", "¡El elemento con ese propiedad ya existe!");
                return BadRequest(ModelState);
            }

            if (class1CreateDto == null)
            {
                return BadRequest(class1CreateDto);
            }

            Class1 modelo = _mapper.Map<Class1>(class1CreateDto);

            //Student modelo = new()
            //{
            //    StudentName = studentCreateDto.StudentName
            //};

            await _cpRepo.Add(modelo);

            return CreatedAtRoute("Getprop", new { id = modelo.Id }, modelo);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteProp(int id)
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
        public async Task<IActionResult> UpdateClass1(int id, [FromBody] Class1UpdateDto class1UpdateDto)
        {
            if (class1UpdateDto == null || id != class1UpdateDto.Id)
            {
                return BadRequest();
            }

            Class1 modelo = _mapper.Map<Class1>(class1UpdateDto);

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
        public async Task<IActionResult> UpdatePartialProp(int id, JsonPatchDocument<Class1UpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var clase1 = await _cpRepo.Get(s => s.Id == id, tracked: false);

            Class1UpdateDto class1UpdateDto = _mapper.Map<Class1UpdateDto>(clase1);
            //StudentUpdateDto studentUpdateDto = new()
            //{
            //    StudentId = student.StudentId,
            //    StudentName = student.StudentName
            //};
            if (clase1 == null) return BadRequest();

            patchDto.ApplyTo(class1UpdateDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Class1 modelo = _mapper.Map<Class1>(class1UpdateDto);
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

