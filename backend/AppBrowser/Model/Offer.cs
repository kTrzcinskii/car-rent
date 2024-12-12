namespace AppBrowser.Model;

public class Offer
{
    public int OfferId { get; set; }
    public int ProviderId { get; set; }
    public int ExternalOfferId { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }
    public virtual Car Car { get; set; }

}