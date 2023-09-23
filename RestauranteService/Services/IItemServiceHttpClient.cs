using RestauranteService.Dtos;

namespace RestauranteService.Services
{
    public interface IItemServiceHttpClient
    {

        public void EnviaRestauranteParaItemService(RestauranteReadDto readDto);
    }
}
