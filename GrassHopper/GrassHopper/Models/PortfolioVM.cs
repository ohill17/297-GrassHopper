using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    [NotMapped]
    public class PortfolioVM
    {
        public PortfolioVM(Portfolio portfolio)
        {
            PortfolioID = portfolio.PortfolioId;
            Name = portfolio.PortfolioName;
            Summary = portfolio.PortfolioSummary;
            Date = portfolio.PortfolioDate;
            if (portfolio.PortfolioThumbnail != null)
                Thumbnail = new(portfolio.PortfolioThumbnail, PhotoSize.Small);
            foreach (PhotoGroup g in portfolio.PortfolioPGroups)
            {
                Groups.Add(new(g, PhotoSize.Medium));
            }
            Format = portfolio.PortfolioFormat;
        }
        public int PortfolioID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateOnly Date { get; set; }
        public PhotoVM? Thumbnail { get; set; }
        public List<GroupVM> Groups { get; set; } = new List<GroupVM>();
        public PortfolioFormats Format { get; set; }
    }
}
