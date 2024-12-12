namespace AppBrowser.Model;

public class Rent
{
    public int RentId { get; set; }
    public int UserId { get; set; }
    public int ProviderId { get; set; }
    public int ExternalRentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public RentStatus Status { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }
    public virtual Car Car { get; set; }
    
    public enum RentStatus
    {
        WaitingForConfirmation,
        Started,
        WaitingForEmployeeApproval,
        Finished
    }
}