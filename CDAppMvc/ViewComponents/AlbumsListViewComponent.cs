using CDAppMvc.Contracts;
using CDAppMvc.ViewModels.Albums;
using Microsoft.AspNetCore.Mvc;

namespace CDAppMvc.ViewComponents;

public class AlbumsListViewComponent : ViewComponent
{
    private readonly IAlbumsService _albumsService;

    public AlbumsListViewComponent(IAlbumsService albumsService)
    {
        _albumsService = albumsService;
    }

    public async Task<IViewComponentResult> InvokeAsync(AlbumsListFilters filters)
    {
        var albums = await _albumsService.GetAlbumsByFiltersAsync(filters);

        return View("AlbumsList", albums);
    }
}