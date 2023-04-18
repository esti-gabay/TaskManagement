using Microsoft.AspNetCore.Mvc;
using lesson1.Interfaces;
using System.Security.Claims;
using lesson1.Services;
using Microsoft.AspNetCore.Authorization;
namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService service;
    public UserController(IUserService usersSer)
    {
        this.service = usersSer;
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public IEnumerable<User> Get()
    {
        return service.GetAll();
    }
    [HttpGet("{id}")]
    [Authorize(Policy = "Agent")]
    public ActionResult<User> Get(string id)
    {
        var user = service.Get(id);
        if (user == null)
            return NotFound();
        return user;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User user)
    {
        service.Add(user);
        return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Put(string id, User user)
    {
        if (!service.Update(id, user))
            return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(string id)
    {

        if (!service.Delete(id))
            return NotFound();
        return NoContent();
    }



    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        User authUser = service.Login(User);

        if (authUser == null)
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
{
new Claim("UserName",authUser.UserName),
new Claim("Id", authUser.Id),
new Claim("Classification",authUser.Classification),
};

        var token = TokenService.GetToken(claims);
        string myToken = HttpContext.Request.Headers["Authorization"];

        return new OkObjectResult(new { Token = TokenService.WriteToken(token), UserId = authUser.Id, Classification = authUser.Classification });
    }


}


