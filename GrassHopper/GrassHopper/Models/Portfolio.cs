namespace GrassHopper.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; } = null!; //Name of portfolio item
        public string PortfolioSummary { get; set; } = null!; //Summary of what the portfolio item is
        public DateOnly PortfolioDate { get; set; } //Date portfolio item completed (? might not be needed ?)
        public Photo? PortfolioThumbnail { get; set; } //Main photo for the portfolio item (if applicable)
        public List<PhotoGroup> PortfolioPGroups { get; set; } = new List<PhotoGroup>(); //List of photo groups for the portfolio
                                                                                         //Multiple groups allows for separate before and after groups
        public bool IsHidden { get; set; } = false;
        public List<Tag> PortfolioTags { get; set; } = new List<Tag>();
    }
}
