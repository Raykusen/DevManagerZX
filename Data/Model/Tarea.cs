using DevManager.Data.Request;
using DevManager.Data.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevManager.Data.Models
{
    public class Tarea
    {
        [Key]
        public int TaskID { get; set; }
        public int ProjectID { get; set; } // FK -> ProjectID
        [Required]
        public string TaskName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Normal";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; } // FK -> UserID
        public int? AssignedTo { get; set; } // FK -> UserID

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; } = null!;

        public static Tarea Crear(TareaRequest tarea) => new()
        {
            ProjectID = tarea.ProjectID,
            TaskName = tarea.TaskName,
            Description = tarea.Description,
            StartDate = tarea.StartDate,
            DueDate = tarea.DueDate,
            Status = tarea.Status,
            Priority = tarea.Priority,
            CreatedBy = tarea.CreatedBy,
            AssignedTo = tarea.AssignedTo
        };

        public bool Modificar(TareaRequest task)
        {
            var cambio = false;
            if (ProjectID != task.ProjectID) ProjectID = task.ProjectID; cambio = true;
            if (TaskName != task.TaskName) TaskName = task.TaskName; cambio = true;
            if (Description != task.Description) Description = task.Description; cambio = true;
            if (StartDate != task.StartDate) StartDate = task.StartDate; cambio = true;
            if (DueDate != task.DueDate) DueDate = task.DueDate; cambio = true;
            if (Status != task.Status) Status = task.Status; cambio = true;
            if (Priority != task.Priority) Priority = task.Priority; cambio = true;
            if (CreatedBy != task.CreatedBy) CreatedBy = task.CreatedBy; cambio = true;
            if (AssignedTo != task.AssignedTo) AssignedTo = task.AssignedTo; cambio = true;
            return cambio;
        }

        public TareaResponse ToResponse() => new()
        {
            TaskID = TaskID,
            ProjectID = ProjectID,
            TaskName = TaskName,
            Description = Description,
            StartDate = StartDate,
            DueDate = DueDate,
            Status = Status,
            Priority = Priority,
            CreatedBy = CreatedBy,
            AssignedTo = AssignedTo,
            Project = Project.ToResponse(),
        };
    }
}
