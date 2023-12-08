using CDAppMvc.Contracts;
using CDAppMvc.DAL;
using CDAppMvc.Enums;
using CDAppMvc.Mapping;
using CDAppMvc.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CDAppMvc.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ApplicationDbContext _context;
    private readonly CategoryMapper _categoryMapper;

    public CategoriesService(ApplicationDbContext context, CategoryMapper categoryMapper)
    {
        _context = context;
        _categoryMapper = categoryMapper;
    }

    public async Task<IReadOnlyList<CategoryListItemViewModel>> GetCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories.Select(_categoryMapper.CategoryToCategoryListItem).ToList();
    }

    public async Task CreateCategoryAsync(CreateUpdateCategoryViewModel category)
    {
        var categoryDb = _categoryMapper.CreateOrUpdateCategoryToCategory(category);
        _context.Categories.Add(categoryDb);
        await _context.SaveChangesAsync();
    }

    public async Task<CategoryDetailsViewModel?> GetCategoryDetailsAsync(int id)
    {
        var category = await _context.Categories.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == id);
        return category is null ? null : _categoryMapper.CategoryToCategoryDetails(category);
    }

    public async Task<CreateUpdateCategoryViewModel?> GetCategoryToUpdateAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        return category is null ? null : _categoryMapper.CategoryToCreateOrUpdateCategory(category);
    }

    public async Task<ActionStatus> UpdateAsync(int categoryId, CreateUpdateCategoryViewModel category, string userId)
    {
        var categoryDb = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        if (categoryDb is null)
            return ActionStatus.NotFound;
        if (categoryDb.OwnerId != userId)
            return ActionStatus.Unauthorized;
        _categoryMapper.CreateOrUpdateCategoryToCategory(category, categoryDb);
        categoryDb.OwnerId = userId;
        _context.Update(categoryDb);
        await _context.SaveChangesAsync();
        return ActionStatus.Ok;
    }

    public async Task<ActionStatus> DeleteAsync(int categoryId, string userId)
    {
        var categoryDb = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        if (categoryDb is null)
            return ActionStatus.NotFound;
        if (categoryDb.OwnerId != userId)
            return ActionStatus.Unauthorized;

        _context.Categories.Remove(categoryDb);
        await _context.SaveChangesAsync();
        return ActionStatus.Ok;
    }
}