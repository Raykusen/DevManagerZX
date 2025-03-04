namespace DevManager.Data.Request;

public class TareaRequest
{
    public int TaskID { get; set; }
    public int ProjectID { get; set; }
    public string TaskName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
    public string Status { get; set; } = "Pending";
    public string Priority { get; set; } = "Normal";
    public int CreatedBy { get; set; }
    public int? AssignedTo { get; set; }
    
}
