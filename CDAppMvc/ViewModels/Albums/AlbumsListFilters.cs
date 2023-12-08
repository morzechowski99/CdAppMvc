using CDAppMvc.Enums;

namespace CDAppMvc.ViewModels.Albums;

public class AlbumsListFilters : ListFiltersBase<AlbumsSortOrder>
{
    public AlbumsListFilters()
    {
        SortOrder = AlbumsSortOrder.Default;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? ReleaseYearFrom { get; set; }
    public int? ReleaseYearTo { get; set; }
}