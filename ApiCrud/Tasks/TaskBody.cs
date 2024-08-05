using Microsoft.IdentityModel.Tokens;

namespace ApiCrud.Tasks
{
    public class TaskBody
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; set; }

        public TaskBody(string name, string description)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateTaskStatus(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}
