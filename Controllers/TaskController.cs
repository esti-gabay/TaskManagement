using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using System.Linq;
// using Microsoft.Extensions.Logging;


namespace lesson1.Controllers;

  [ApiController]
  [Route("[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
        public IEnumerable<Task> Get()
        {
            return TaskService.GetAll();

        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var task = TaskService.Get(id);
            if (task == null)
                return NotFound();
             return task;
        }
       
       [HttpPost]
        public ActionResult Post(Task task)
        {
            TaskService.Add(task);

            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task task)
        {
            if (! TaskService.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! TaskService.Delete(id))
                return NotFound();
            return NoContent();            
        }



}
   

    
