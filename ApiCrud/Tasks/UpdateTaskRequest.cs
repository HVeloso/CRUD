namespace ApiCrud.Tasks
{
    // Usado pra receber os dados necessários na alteração da tarefa

    public record UpdateTaskRequest (int Id, string Name, string Description, bool IsCompleted);
}
