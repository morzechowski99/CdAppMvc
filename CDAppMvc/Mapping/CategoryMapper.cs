using CDAppMvc.DAL.DbModels;
using CDAppMvc.ViewModels;
using Riok.Mapperly.Abstractions;

namespace CDAppMvc.Mapping;

[Mapper]
public partial class CategoryMapper
{
    public partial CategoryListItemViewModel CategoryToCategoryListItem(Category category);

    public partial Category CreateOrUpdateCategoryToCategory(CreateUpdateCategoryViewModel category);

    public partial void CreateOrUpdateCategoryToCategory(CreateUpdateCategoryViewModel categoryDto, Category category);

    [MapProperty(nameof(@Category.Owner.Email), nameof(CategoryDetailsViewModel.Owner))]
    public partial CategoryDetailsViewModel CategoryToCategoryDetails(Category category);

    public partial CreateUpdateCategoryViewModel CategoryToCreateOrUpdateCategory(Category category);
}