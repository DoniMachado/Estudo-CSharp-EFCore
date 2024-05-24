using EFCore.Domain.Entities;
using EFCore.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroiController : ControllerBase
    {
        //private readonly ILogger<HeroiController> _logger;
        private readonly HeroContext _heroiContext;    

        public HeroiController(HeroContext heroiContext)
        {
           // _logger = logger;
            _heroiContext = heroiContext;     
        }

        //[HttpGet()]
        //public async Task<ActionResult> Get([FromQuery] string name = null)
        //{
        //    var data =  _heroiContext.Herois.AsNoTracking().AsQueryable();

        //    if (!string.IsNullOrEmpty(name))
        //        data = data.Where(p => p.Nome.Contains(name));

        //    var list = await data.ToListAsync();

        // //var list = await (from heroi in _heroiContext.Herois
        // //           select heroi).ToListAsync();

        //  return Ok(list);
        //}

        //[HttpGet("Id/{id}")]
        //public async Task<ActionResult> GetById(long id)
        //{
        //    var hero = await _heroiContext.Herois.Where(p => p.Id == id).FirstOrDefaultAsync();

        //    //var hero = await (from heroi in _heroiContext.Herois
        //    //                   where heroi.Id == id
        //    //                   select heroi).FirstOrDefaultAsync();

        //    if (hero is null)
        //        return NotFound();

        //    return Ok(hero);
        //}
        //[HttpGet("Name/{name}")]
        //public async Task<ActionResult> GetByName(string name)
        //{
        //    var hero = await _heroiContext.Herois.Where(p => p.Nome.Contains(name)).FirstOrDefaultAsync();            

        //    //var hero = await (from heroi in _heroiContext.Herois
        //    //                  where heroi.Nome.Contains(name)
        //    //                  select heroi).FirstOrDefaultAsync();

        //    if (hero is null)
        //        return NotFound();

        //    return Ok(hero);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(long id,[FromBody] string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return BadRequest();

        //    Hero hero  = await _heroiContext.Herois.FirstOrDefaultAsync(p => p.Id == id);

        //    if (hero is null)
        //        return NotFound();

        //    hero.Nome = name;

        //    _heroiContext.Update(hero);
        //    _heroiContext.SaveChanges();

        //    return Ok(hero);
        //}

        //[HttpPost()]
        //public ActionResult Post([FromBody] string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return BadRequest();

        //    Hero hero = new()
        //    {
        //        Nome = name
        //    };

        //    _heroiContext.Add(hero);
        //    _heroiContext.SaveChanges();

        //    return Ok(hero);
        //}


        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(long id)
        //{
        //    Hero hero = await _heroiContext.Herois.FirstOrDefaultAsync(p => p.Id == id);

        //    if (hero is null)
        //        return NotFound();            

        //    _heroiContext.Remove(hero);
        //    _heroiContext.SaveChanges();

        //    return Ok();
        //}
    }
}
