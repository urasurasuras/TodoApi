namespace TodoApi.Models
{
    public class TodoItemDTO
    {
        public string? Secret { get; set; }
        public TodoItem? todoItem { get; set; }
    }
}