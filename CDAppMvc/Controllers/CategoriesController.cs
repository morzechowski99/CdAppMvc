using CDAppMvc.Contracts;
using CDAppMvc.DAL;
using CDAppMvc.Enums;
using CDAppMvc.Extensions;
using CDAppMvc.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDAppMvc.Controllers;

[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ApplicationDbContext context, ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        return View(await _categoriesService.GetCategoriesAsync());
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _categoriesService.GetCategoryDetailsAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] CreateUpdateCategoryViewModel category)
    {
        if (!ModelState.IsValid) return View(category);
        category.OwnerId = User.GetUserId();
        await _categoriesService.CreateCategoryAsync(category);
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _categoriesService.GetCategoryToUpdateAsync(id.Value);
        if (category == null)
            return NotFound();
        if (category.OwnerId != User.GetUserId())
            return Unauthorized();
        return View(category);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,Description")] CreateUpdateCategoryViewModel category)
    {
        if (!ModelState.IsValid) return View(category);

        var result = await _categoriesService.UpdateAsync(id, category, User.GetUserId());
        return result != ActionStatus.Ok ? GetActionResult(result) : RedirectToAction(nameof(Index));
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var category = await _categoriesService.GetCategoryDetailsAsync(id.Value);
        if (category == null)
            return NotFound();
        if (category.OwnerId != User.GetUserId())
            return Unauthorized();

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _categoriesService.DeleteAsync(id, User.GetUserId());
        return result != ActionStatus.Ok ? GetActionResult(result) : RedirectToAction(nameof(Index));
    }
}