using DevManager.Data.Request;

namespace DevManager.Data.Response;

public class ProjectResponse
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TareaResponse> Tareas { get; set; } = new();

    // Propiedad para obtener los datos del usuario creador
    public UserResponse? Creator { get; set; }
    public ProjectRequest ToRequest()
    {
        return new ProjectRequest
        {
            ProjectID = ProjectID,
            ProjectName = ProjectName,
            Description = Description,
            StartDate = StartDate,
            EndDate = EndDate,
            CreatedBy = CreatedBy
        };
    }
}
