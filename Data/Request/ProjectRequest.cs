namespace DevManager.Data.Request;

public class ProjectRequest
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now.AddDays(30);
    public int CreatedBy { get; set; }
}
