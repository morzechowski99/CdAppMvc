using CDAppMvc.Contracts;
using CDAppMvc.DAL;
using CDAppMvc.DAL.DbModels;
using CDAppMvc.Enums;
using CDAppMvc.Mapping;
using CDAppMvc.ViewModels;
using CDAppMvc.ViewModels.Albums;
using Microsoft.EntityFrameworkCore;

namespace CDAppMvc.Services;

public class AlbumsService : IAlbumsService
{
    private readonly AlbumMapper _albumMapper;
    private readonly ApplicationDbContext _context;

    public AlbumsService(AlbumMapper albumMapper, ApplicationDbContext context)
    {
        _albumMapper = albumMapper;
        _context = context;
    }

    public async Task CreateAlbumAsync(CreateUpdateAlbumViewModel album)
    {
        var albumDb = _albumMapper.CreateUpdateAlbumViewModelToAlbum(album);
        _context.Albums.Add(albumDb);
        await _context.SaveChangesAsync();
    }

    public async Task<AlbumDetailsViewModel?> GetAlbumDetailsAsync(Guid id)
    {
        var album = await _context.Albums.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return album is null ? null : _albumMapper.AlbumToAlbumDetailsViewModel(album);
    }

    public async Task<CreateUpdateAlbumViewModel?> GetAlbumToUpdateAsync(Guid id)
    {
        var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
        return album is null ? null : _albumMapper.AlbumToCreateUpdateAlbumViewModel(album);
    }

    public async Task<ActionStatus> UpdateAsync(Guid id, CreateUpdateAlbumViewModel album)
    {
        var albumDb = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
        if (albumDb is null)
            return ActionStatus.NotFound;
        _albumMapper.CreateUpdateAlbumViewModelToAlbum(album, albumDb);
        _context.Albums.Update(albumDb);
        await _context.SaveChangesAsync();
        return ActionStatus.Ok;
    }

    public async Task<ActionStatus> DeleteAsync(Guid id)
    {
        var albumDb = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
        if (albumDb is null)
            return ActionStatus.NotFound;
        _context.Albums.Remove(albumDb);
        await _context.SaveChangesAsync();
        return ActionStatus.Ok;
    }

    public async Task<PagedList<AlbumListItemViewModel>> GetAlbumsByFiltersAsync(AlbumsListFilters filters)
    {
        var albums = _context.Albums.Include(x => x.Category).AsNoTracking();

        if (filters.Description is not null)
        {
            var descriptionFilter = filters.Description.ToLower();
            albums = albums.Where(x => x.Description != null && x.Description.ToLower().Contains(descriptionFilter));
        }
        if (filters.Name is not null)
        {
            var nameFilter = filters.Name.ToLower();
            albums = albums.Where(x => x.Name.ToLower().Contains(nameFilter));
        }
        if (filters.ReleaseYearFrom.HasValue)
            albums = albums.Where(x => x.ReleaseYear.HasValue && x.ReleaseYear >= filters.ReleaseYearFrom);
        if (filters.ReleaseYearTo.HasValue)
            albums = albums.Where(x => x.ReleaseYear.HasValue && x.ReleaseYear <= filters.ReleaseYearTo);

        albums = filters.SortOrder switch
        {
            AlbumsSortOrder.Default => albums,
            AlbumsSortOrder.NameAsc => albums.OrderBy(x => x.Name),
            AlbumsSortOrder.NameDesc => albums.OrderByDescending(x => x.Name),
            AlbumsSortOrder.Newest => albums.OrderByDescending(x => x.ReleaseYear),
            AlbumsSortOrder.Oldest => albums.OrderBy(x => x.ReleaseYear),
            _ => albums
        };

        var list = await PagedList<Album>
            .ToPagedList(albums, filters.Page ?? 1, filters.PageSize ?? 25);
        var listView = list.Select(_albumMapper.AlbumToAlbumAlbumListItemViewModel);

        return new PagedList<AlbumListItemViewModel>(listView, await albums.CountAsync(), list.CurrentPage,
            list.PageSize);

    }
}