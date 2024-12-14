namespace AppBrowser.Model;

public class Rent
{
    public int RentId { get; set; }
    public int ProviderId { get; set; }
    public int ExternalRentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public RentStatus Status { get; set; }
    public virtual Offer Offer { get; set; }
    
    public enum RentStatus
    {
        WaitingForConfirmation,
        Started,
        WaitingForEmployeeApproval,
        Finished
    }
}