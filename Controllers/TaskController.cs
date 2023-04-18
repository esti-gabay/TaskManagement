using Microsoft.AspNetCore.Mvc;
using lesson1.Interfaces;
using lesson1.Services;
using Microsoft.AspNetCore.Authorization;

namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Agent")]
public class TaskController : ControllerBase
{
    private ITaskService service;
    public TaskController(ITaskService tasksSer)
    {
        this.service = tasksSer;
    }


   [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {     
        string token = HttpContext.Request.Headers["Authorization"];
        List<Task> resultList = service.Get(token);
        Task task=resultList.FirstOrDefault(t=>t.Id == id);
        if (task == null)
            return NotFound();
        return task;
    }
    [HttpGet]
    public ActionResult<List<Task>> Get()
    {
        System.Console.WriteLine("in get tasks");
        string token = HttpContext.Request.Headers["Authorization"];
        List<Task> resultList = service.Get(token);
        if (resultList == null)
            return NotFound();
        return resultList;
    }

    [HttpPost]
    public ActionResult Post(Task task)
    {
        System.Console.WriteLine($" taskName: {task.Name}");
        string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        task.User = userId;
        service.Add(task);
        return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Task task)
    {   
        string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        task.User=userId;
        if (!service.Update(id, task))
           return BadRequest();
           System.Console.WriteLine($" Update user: {userId}");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        System.Console.WriteLine($"in delete user:{userId}");
        if (!service.Delete(id,userId))
            return NotFound();
        return NoContent();
    }
}


