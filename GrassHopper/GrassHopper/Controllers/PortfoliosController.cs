using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrassHopper.Controllers
{
	[Authorize(Roles = "Admin")]
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

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			List<PortfolioVM> portfolios = await portRepository.GetAllPortfolios();
			return View(portfolios);
		}

		[AllowAnonymous]
		public async Task<IActionResult> PortfolioDetails(int id)
		{
			PortfolioVM portfolio = new(await portRepository.GetPortfolio(id));
			return View(portfolio);
		}
		
		public async Task<IActionResult> PortfolioAdmin()
		{
			List<Portfolio> portfolios = await portRepository.GetAllPortfoliosAdmin();
			return View(portfolios);
		}

		[HttpGet]
		public async Task<IActionResult> CreatePortfolio()
		{
            ViewBag.PhotoGroups = await photoRepository.GetAllGroups(PhotoSize.Small);
			ViewBag.Photos = await photoRepository.GetAllPhotos(PhotoSize.Small);
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePortfolio(Portfolio p, int pGroupId, int thumbnailId)
		{
			if (pGroupId > 0)
				p.PortfolioPGroups.Add(await photoRepository.GetPhotoGroup(pGroupId));
			if (thumbnailId > 0)
				p.PortfolioThumbnail = await photoRepository.GetPhoto(thumbnailId);
            await portRepository.AddPortfolio(p);
			return RedirectToAction("PortfolioAdmin");
		}

		[HttpGet]
		public async Task<IActionResult> EditPortfolioItem(int id)
		{
            ViewBag.PhotoGroups = await photoRepository.GetAllGroups(PhotoSize.Small);
            ViewBag.Photos = await photoRepository.GetAllPhotos(PhotoSize.Small);
            Portfolio p = await portRepository.GetPortfolio(id);
			return View(p);
		}

		[HttpPost]
		public async Task<IActionResult> EditPortfolioItem(Portfolio p, int pGroupId, int thumbnailId, bool shouldHide)
		{
            if (pGroupId > 0)
                p.PortfolioPGroups.Add(await photoRepository.GetPhotoGroup(pGroupId));
            if (thumbnailId > 0)
                p.PortfolioThumbnail = await photoRepository.GetPhoto(thumbnailId);
			if (shouldHide)
			{
				if (!p.IsHidden)
					await portRepository.HidePortfolio(p.PortfolioId);
			}
			else if (p.IsHidden)
				await portRepository.RestorePortfolio(p.PortfolioId);
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
			Portfolio portfolio = await portRepository.GetPortfolio(id);
			return View(portfolio);
		}

		public async Task<IActionResult> ConfirmDeletePortfolio(int id)
		{
			await portRepository.DeletePortfolio(id);
			return RedirectToAction("PortfolioAdmin");
		}
	}
}
