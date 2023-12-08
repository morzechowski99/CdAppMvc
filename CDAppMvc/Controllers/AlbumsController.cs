using CDAppMvc.Contracts;
using CDAppMvc.Enums;
using CDAppMvc.ViewModels.Albums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CDAppMvc.Controllers;

public class AlbumsController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    private readonly IAlbumsService _albumsService;

    public AlbumsController(ICategoriesService categoriesService, IAlbumsService albumsService)
    {
        _categoriesService = categoriesService;
        _albumsService = albumsService;
    }

    // GET: Albums
    public IActionResult Index()
    {
        return View();
    }

    // GET: Albums/GetList
    public IActionResult GetList(AlbumsListFilters? filters)
    {
        if (filters is null)
            return BadRequest();

        return ViewComponent("AlbumsList", filters);
    }

    // GET: Albums/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = await _albumsService.GetAlbumDetailsAsync(id.Value);
        if (album == null)
        {
            return NotFound();
        }

        return View(album);
    }

    // GET: Albums/Create
    public async Task<IActionResult> Create()
    {
        ViewData["CategoryId"] = new SelectList(await _categoriesService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    // POST: Albums/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Author,CategoryId,ReleaseYear")] CreateUpdateAlbumViewModel album)
    {
        if (!ModelState.IsValid)
        {
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetCategoriesAsync(), "Id", "Name");
            return View(album);
        }
        await _albumsService.CreateAlbumAsync(album);
        return RedirectToAction(nameof(Index));
    }

    // GET: Albums/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = await _albumsService.GetAlbumToUpdateAsync(id.Value);
        if (album == null)
            return NotFound();
        ViewData["CategoryId"] = new SelectList(await _categoriesService.GetCategoriesAsync(), "Id", "Name");
        return View(album);
    }

    // POST: Albums/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Author,CategoryId,ReleaseYear")] CreateUpdateAlbumViewModel album)
    {
        if (!ModelState.IsValid)
        {
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetCategoriesAsync(), "Id", "Name");
            return View(album);
        }

        var result = await _albumsService.UpdateAsync(id, album);
        return result != ActionStatus.Ok ? GetActionResult(result) : RedirectToAction(nameof(Index));
    }

    // GET: Albums/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = await _albumsService.GetAlbumDetailsAsync(id.Value);
        if (album == null)
        {
            return NotFound();
        }

        return View(album);
    }

    // POST: Albums/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = await _albumsService.DeleteAsync(id);
        return result != ActionStatus.Ok ? GetActionResult(result) : RedirectToAction(nameof(Index));
    }
}