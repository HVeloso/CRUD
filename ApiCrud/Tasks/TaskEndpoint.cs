using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiCrud.Tasks
{
    public static class TaskEndpoint
    {
        public static void AddTaskEndpoints(this WebApplication app)
        {
            var taskEndpoints = app.MapGroup("ToDoList");

            #region Create Task
            
            taskEndpoints.MapPost("Create-Task",
                async (AddTaskRequest request, AppDbContext context, CancellationToken ct) =>
            {
                // Verifica se os dados são válidas
                if (request.Name.IsNullOrEmpty())
                    return Results.BadRequest("Name can not be null");

                // Criar nova task
                var newTask = new TaskBody(request.Name, request.Description);

                // Adicionar a nova task no banco de dados
                await context.ToDoList.AddAsync(newTask, ct);
                await context.SaveChangesAsync(ct);

                return Results.Ok(newTask.Id);
            });

            #endregion

            #region Read Task

            taskEndpoints.MapGet("Get-All-Tasks",
                async (AppDbContext context, CancellationToken ct) =>
            {
                // Pega uma lista com todas as tasks
                var tasks = await context.ToDoList
                .Select(task => new TaskDto(task.Id, task.Name, task.Description, task.IsCompleted))
                .ToListAsync(ct);

                return tasks;
            });

            taskEndpoints.MapGet("Get-Completed-Tasks",
               async (AppDbContext context, CancellationToken ct) =>
               {
                   // Pega uma lista com todas as tasks completas
                   var tasks = await context.ToDoList
                    .Where(task => task.IsCompleted)
                    .Select(task => new TaskDto(task.Id, task.Name, task.Description, task.IsCompleted))
                    .ToListAsync(ct);

                   return tasks;
               });

            taskEndpoints.MapGet("Get-Incompleted-Tasks",
               async (AppDbContext context, CancellationToken ct) =>
               {
                   // Pega uma lista com todas as tasks incompletas
                   var tasks = await context.ToDoList
                    .Where(task => !task.IsCompleted)
                    .Select(task => new TaskDto(task.Id, task.Name, task.Description, task.IsCompleted))
                    .ToListAsync(ct);

                   return tasks;
               });

            #endregion

            #region Update Task

            taskEndpoints.MapPut("Update-{id:Guid}", 
                async (Guid id, UpdateTaskRequest request, AppDbContext context, CancellationToken ct) =>
            {
                // Verifica se o Id existe na lista
                var task = await context.ToDoList.SingleOrDefaultAsync(task => task.Id == id, ct);
                if (task == null) return Results.NotFound();

                // Verifica se as mudanças são válidas
                if (request.Name.IsNullOrEmpty())
                    return Results.BadRequest("Name can not be null");

                // Atualiza a task
                task.UpdateName(request.Name);
                task.UpdateDescription(request.Description);
                task.UpdateTaskStatus(request.IsCompleted);

                // Salva as mudanças
                await context.SaveChangesAsync(ct);
                var returnTask = new TaskDto(task.Id, task.Name, task.Description, task.IsCompleted);
                return Results.Ok(returnTask);
            });

            #endregion

            #region Delete Task

            taskEndpoints.MapDelete("Delete-{id:Guid}", 
                async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                // Verifica se o Id existe na lista
                var task = await context.ToDoList.SingleOrDefaultAsync(task => task.Id == id, ct);
                if (task == null) return Results.NotFound();

                // Deleta a task
                var returnTask = new TaskDto(task.Id, task.Name, task.Description, task.IsCompleted);
                context.ToDoList.Remove(task);

                // Salva as mudanças no banco de dados
                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });

            #endregion
        }
    }
}
