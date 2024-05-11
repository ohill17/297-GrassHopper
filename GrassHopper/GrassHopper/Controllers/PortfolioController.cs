using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GrassHopper.Controllers
{
	public class PortfolioController : Controller
	{
		private readonly IPortfolioRepository portRepository;
		private readonly AppDbContext dbContext;

		public PortfolioController (IPortfolioRepository p, AppDbContext c)
		{
			portRepository = p;
			dbContext = c;
		}
		public async Task<IActionResult> Index()
		{
			List<PortfolioVM> portfolios = await portRepository.GetAllPortfolios();
			return View(portfolios);
		}

		[HttpGet]
		public async Task<IActionResult> CreatePortfolio()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePortfolio(Portfolio p)
		{
			await portRepository.AddPortfolio(p);
			return RedirectToAction("Index");
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
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> HidePortfolio(int id)
		{
			await portRepository.HidePortfolio(id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> HiddenPortfolios()
		{
			List<PortfolioVM> portfolios = await portRepository.GetHiddenPortfolios();
			return View(portfolios);
		}

		public async Task<IActionResult> RestorePortfolio(int id)
		{
			await portRepository.RestorePortfolio(id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> DeletePortfolio(int id)
		{
			return View();
		}

		public async Task<IActionResult> ConfirmDeletePortfolio(int id)
		{
			await portRepository.DeletePortfolio(id);
			return RedirectToAction("Index");
		}
	}
}
