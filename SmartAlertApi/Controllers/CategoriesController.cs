using Api.Dtos;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public ICategoriesRepository _categoryRepository { get; set; }
        public IMapper _mapper { get; set; }

        public CategoriesController(ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Get()
        {
            var types = await _categoryRepository.GetAllTypes();
            return Ok(_mapper.Map<List<CategoryDto>>(types));
        }

    }
}
