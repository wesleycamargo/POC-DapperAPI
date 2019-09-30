using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using TarefasPendentes.Models;

namespace TarefasPendentes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private IConfiguration config;

        public TodoItemController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/TodoItem
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            using (SqlConnection connection = new SqlConnection(this.config.GetConnectionString("Tarefas")))
            {
                return connection.Query<TodoItem>("select * from tarefa");
            }
            
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}", Name = "Get")]
        public TodoItem Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(this.config.GetConnectionString("Tarefas")))
            {
                return connection.QueryFirst<TodoItem>("select * from tarefa where id = @id", new { id });
            }
        }

        // POST: api/TodoItem
        [HttpPost]
        public void Post([FromBody] TodoItem value)
        {
            using (SqlConnection connection = new SqlConnection(this.config.GetConnectionString("Tarefas")))
            {
                connection.Open();
                var execute = connection.Execute("INSERT INTO [dbo].[Tarefa] (Title,isCompleted) VALUES (@Title,@IsCompleted)", value);
            }
        }

        // PUT: api/TodoItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
