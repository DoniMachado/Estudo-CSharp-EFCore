using EFCore.Application.Core.CommandsAndHandlers;
using EFCore.Application.Core.QueriesAndHandlers;
using EFCore.Domain.Common.ValueObject;
using EFCore.Domain.Core.Entities;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EFCore.WebAPI.Controllers
{
    public class HeroController : ApiController
    {        
        public HeroController(ILogger<HeroController> logger):base(logger)
        {
             
        }

        [HttpGet()]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultPaginationVO))]
        public async Task<ActionResult<ResultPaginationVO>> GetPaginated([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 25, [FromQuery] string name = null)
        {
            var response = await Mediator.Send(new GetHeroPaginatedCommandQuery(pageIndex, pageSize, name));
            return GetReturnForPagination(response);
        }

        [HttpGet("Id/{id}")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(HeroResponseModel))]
        public async Task<ActionResult<HeroResponseModel>> GetById(long id)
        {
            var response = await Mediator.Send(new GetHeroByIdCommandQuery(id));
            if(response is not null && response.Result is not null)
                response.SetResult(HeroResponseModel.ConvertFromEntity(response.Result as Hero));

            return GetReturn<HeroResponseModel>(response);   
        }

        [HttpGet("Name/{name}")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(HeroResponseModel))]
        public async Task<ActionResult<HeroResponseModel>> GetByName(string name)
        {
            var response = await Mediator.Send(new GetHeroByNameCommandQuery(name));
            if(response is not null && response.Result is not null)
                response.SetResult(HeroResponseModel.ConvertFromEntity(response.Result as Hero));

            return GetReturn<HeroResponseModel>(response); 
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<ActionResult<HeroResponseModel>> Put(long id, AlterHeroCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            var response = await Mediator.Send(command);
            return GetReturn<HeroResponseModel>(response, false);
        }

        [HttpPost()]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<ActionResult<HeroResponseModel>> Post(RegisterHeroCommand command)
        {
            var response = await Mediator.Send(command);
            return GetReturn<HeroResponseModel>(response, false);
        }


        [HttpDelete("{id}")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<ActionResult<HeroResponseModel>> Delete(long id)
        {
            var response = await Mediator.Send(new RemoveHeroCommand(id));
            return GetReturn<HeroResponseModel>(response,false);
        }
    }
}
