using DevManager.Data.Request;
using DevManager.Data.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevManager.Data.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; } // FK -> UserID
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; } = null!;

        public static Project Crear(ProjectRequest project) => new()
        {
            ProjectName = project.ProjectName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedBy = project.CreatedBy
        };

        public bool Modificar(ProjectRequest project)
        {
            var cambio = false;
            if (ProjectName != project.ProjectName) ProjectName = project.ProjectName; cambio = true;
            if (Description != project.Description) Description = project.Description; cambio = true;
            return cambio;
        }

        public ProjectResponse ToResponse() => new()
        {
            ProjectID = ProjectID,
            ProjectName = ProjectName,
            Description = Description,
            StartDate = StartDate,
            EndDate = EndDate,
            CreatedBy = CreatedBy,
            CreatedAt = CreatedAt,
            Creator = Creator.ToResponse()
        };
    }
}
