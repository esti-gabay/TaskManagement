using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using lesson1.Interfaces;


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
        public IEnumerable<Task> Get()
        {
            return service.GetAll();

        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var task = service.Get(id);
            if (task == null)
                return NotFound();
             return task;
        }
       
       [HttpPost]
        public ActionResult Post(Task task)
        {
            service.Add(task);
            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task task)
        {
            if (! service.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! service.Delete(id))
                return NotFound();
            return NoContent();            
        }
}

    
