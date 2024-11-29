using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ToDoTasks  // Переименовано с Task на ToDoTask
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; }

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Due date is required.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Due time is required.")]
    [DataType(DataType.Time)]
    public TimeSpan DueTime { get; set; }

    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }

    [Required]
    public int Priority { get; set; }  // Новое поле для приоритета

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dueDate && dueDate > DateTime.Now)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(ErrorMessage);
        }
    }
}
