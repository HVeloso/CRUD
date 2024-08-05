namespace ApiCrud.Tasks
{
    // Dto - Objeto de transferência de dados
    // Usado pra transmitir apenas os dados interessantes pro front-end

    public record TaskDto(int Id, string Name, string Description, bool IsCompleted);
}
