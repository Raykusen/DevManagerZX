using DevManager.Data.Models;
using DevManager.Data.Request;

namespace DevManager.Data.Response;
public class TareaResponse
{
    public int TaskID { get; set; }
    public int ProjectID { get; set; }
    public string TaskName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public int CreatedBy { get; set; }
    public int? AssignedTo { get; set; }
    public ProjectResponse Project { get; set; } = null!;

    public TareaRequest ToRequest()
    {
        return new TareaRequest
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
            AssignedTo = AssignedTo
        };
    }
}