namespace CDAppMvc.ViewModels;

public class ListFiltersBase<T> where T : Enum
{
    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public required T SortOrder { get; set; }
}