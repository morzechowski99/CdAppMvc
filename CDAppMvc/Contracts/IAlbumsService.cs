using CDAppMvc.Enums;
using CDAppMvc.ViewModels;
using CDAppMvc.ViewModels.Albums;

namespace CDAppMvc.Contracts;

public interface IAlbumsService
{
    Task CreateAlbumAsync(CreateUpdateAlbumViewModel album);
    Task<AlbumDetailsViewModel?> GetAlbumDetailsAsync(Guid id);
    Task<CreateUpdateAlbumViewModel?> GetAlbumToUpdateAsync(Guid id);
    Task<ActionStatus> UpdateAsync(Guid id, CreateUpdateAlbumViewModel album);
    Task<ActionStatus> DeleteAsync(Guid id);
    Task<PagedList<AlbumListItemViewModel>> GetAlbumsByFiltersAsync(AlbumsListFilters filters);
}