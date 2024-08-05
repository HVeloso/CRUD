namespace ApiCrud.Tasks
{
    // Usado pra receber os dados usados na alteração da tarefa

    public record UpdateTaskRequest (string Name, string Description, bool IsCompleted);
}
