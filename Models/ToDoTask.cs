namespace ToDoTasksApi.Models;

using System.ComponentModel.DataAnnotations;

public class ToDoTask
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(40, ErrorMessage = "Title can't exceed 40 characters.")]
    public string Title { get; set; }

    [StringLength(400, ErrorMessage = "Description can't exceed 400 characters.")]
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;
    public PriorityLevel TaskPriorityLevel { get; set; }
    public DateTime DueDate { get; set; }
    public TimeSpan DueTime { get; set; }
}