using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GrassHopper.Controllers
{
	public class PortfoliosController : Controller
	{
		private readonly IPortfolioRepository portRepository;
		private readonly IPhotoRepository photoRepository;
		private readonly AppDbContext dbContext;

		public PortfoliosController (IPortfolioRepository po, IPhotoRepository ph, AppDbContext c)
		{
			portRepository = po;
			photoRepository = ph;
			dbContext = c;
		}
		public async Task<IActionResult> Index()
		{
			List<PortfolioVM> portfolios = await portRepository.GetAllPortfolios();
			return View(portfolios);
		}
		
		public async Task<IActionResult> PortfolioAdmin()
		{
			List<Portfolio> portfolios = await portRepository.GetAllPortfoliosAdmin();
			return View(portfolios);
		}

		[HttpGet]
		public async Task<IActionResult> CreatePortfolio()
		{
            ViewBag.PhotoGroups = await photoRepository.GetAllGroups(PhotoSize.Medium);
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePortfolio(Portfolio p, int pGroupId)
		{
			PhotoGroup photoGroup = await photoRepository.GetPhotoGroup(pGroupId);
			p.PortfolioPGroups.Add(photoGroup);
			await portRepository.AddPortfolio(p);
			return RedirectToAction("PortfolioAdmin");
		}

		[HttpGet]
		public async Task<IActionResult> EditPortfolio(int id)
		{
			Portfolio p = await portRepository.GetPortfolio(id);
			return View(p);
		}

		[HttpPost]
		public async Task<IActionResult> EditPortfolio(Portfolio p)
		{
			await portRepository.UpdatePortfolio(p);
			return RedirectToAction("PortfolioAdmin");
		}

		public async Task<IActionResult> HidePortfolio(int id)
		{
			await portRepository.HidePortfolio(id);
			return RedirectToAction("PortfolioAdmin");
		}

		public async Task<IActionResult> HiddenPortfolios()
		{
			List<PortfolioVM> portfolios = await portRepository.GetHiddenPortfolios();
			return View(portfolios);
		}

		public async Task<IActionResult> RestorePortfolio(int id)
		{
			await portRepository.RestorePortfolio(id);
			return RedirectToAction("PortfolioAdmin");
		}

		public async Task<IActionResult> DeletePortfolio(int id)
		{
			return View();
		}

		public async Task<IActionResult> ConfirmDeletePortfolio(int id)
		{
			await portRepository.DeletePortfolio(id);
			return RedirectToAction("PortfolioAdmin");
		}
	}
}
