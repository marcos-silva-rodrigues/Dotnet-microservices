using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestauranteService.Data;
using RestauranteService.Dtos;
using RestauranteService.Models;
using RestauranteService.Services;

namespace RestauranteService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestauranteController : ControllerBase
{
    private readonly IRestauranteRepository _repository;
    private readonly IMapper _mapper;
    private readonly IItemServiceHttpClient _itemService;

    public RestauranteController(
        IRestauranteRepository repository,
        IMapper mapper,
        IItemServiceHttpClient itemService)
    {
        _repository = repository;
        _mapper = mapper;
        _itemService = itemService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestauranteReadDto>> GetAllRestaurantes()
    {

        var restaurantes = _repository.GetAllRestaurantes();

        return Ok(_mapper.Map<IEnumerable<RestauranteReadDto>>(restaurantes));
    }

    [HttpGet("{id}", Name = "GetRestauranteById")]
    public ActionResult<RestauranteReadDto> GetRestauranteById(int id)
    {
        var restaurante = _repository.GetRestauranteById(id);
        if (restaurante != null)
        {
            return Ok(_mapper.Map<RestauranteReadDto>(restaurante));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<RestauranteReadDto>> CreateRestaurante(RestauranteCreateDto restauranteCreateDto)
    {
        var restaurante = _mapper.Map<Restaurante>(restauranteCreateDto);
        _repository.CreateRestaurante(restaurante);
        _repository.SaveChanges();

        var restauranteReadDto = _mapper.Map<RestauranteReadDto>(restaurante);
        _itemService.EnviaRestauranteParaItemService(restauranteReadDto);

        return CreatedAtRoute(nameof(GetRestauranteById), new { restauranteReadDto.Id }, restauranteReadDto);
    }
}
