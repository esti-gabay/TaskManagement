using Microsoft.AspNetCore.Mvc;
using lesson1.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private ITaskService service;
    public TaskController(ITaskService tasksSer)
    {
        this.service = tasksSer;
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public IEnumerable<Task> Get()
    {
        return service.GetAll();
    }
    [HttpGet]
    [Route("GetUserTasks")]
    [Authorize(Policy = "Agent")]
    public ActionResult<List<Task>> GetUserTasks()
    {
        string token = HttpContext.Request.Headers["Authorization"];
        System.Console.WriteLine(token);
        List<Task> resultList = service.Get(token);
        if (resultList == null)
            return NotFound();
        return resultList;
    }

    [HttpPost]
    [Authorize(Policy = "Agent")]
    public ActionResult Post(Task task)
    {
        service.Add(task);
        return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Agent")]
    public ActionResult Put(int id, Task task)
    {
        if (!service.Update(id, task))
            return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Agent")]
    public ActionResult Delete(int id)
    {
        if (!service.Delete(id))
            return NotFound();
        return NoContent();
    }
}


