using CDAppMvc.Enums;
using CDAppMvc.ViewModels;

namespace CDAppMvc.Contracts;

public interface ICategoriesService
{
    Task<IReadOnlyList<CategoryListItemViewModel>> GetCategoriesAsync();
    Task CreateCategoryAsync(CreateUpdateCategoryViewModel category);
    Task<CategoryDetailsViewModel?> GetCategoryDetailsAsync(int id);
    Task<CreateUpdateCategoryViewModel?> GetCategoryToUpdateAsync(int id);
    Task<ActionStatus> UpdateAsync(int categoryId, CreateUpdateCategoryViewModel category, string userId);
    Task<ActionStatus> DeleteAsync(int categoryId, string userId);
}