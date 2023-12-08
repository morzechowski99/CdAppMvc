using CDAppMvc.DAL.DbModels;
using CDAppMvc.ViewModels.Albums;
using Riok.Mapperly.Abstractions;

namespace CDAppMvc.Mapping;

[Mapper]
public partial class AlbumMapper
{
    public partial Album CreateUpdateAlbumViewModelToAlbum(CreateUpdateAlbumViewModel album);
    public partial void CreateUpdateAlbumViewModelToAlbum(CreateUpdateAlbumViewModel albumViewModel, Album albumDb);
    public partial CreateUpdateAlbumViewModel AlbumToCreateUpdateAlbumViewModel(Album album);

    [MapProperty(nameof(@Album.Category.Name), nameof(AlbumDetailsViewModel.Category))]
    public partial AlbumDetailsViewModel AlbumToAlbumDetailsViewModel(Album album);
    [MapProperty(nameof(@Album.Category.Name), nameof(AlbumListItemViewModel.Category))]
    public partial AlbumListItemViewModel AlbumToAlbumAlbumListItemViewModel(Album album);
}