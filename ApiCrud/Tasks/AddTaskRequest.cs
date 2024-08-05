namespace ApiCrud.Tasks
{
    // Usado pra receber os dados necessários para a criação de uma tarefa

    public record AddTaskRequest(string Name, string Description);
}
