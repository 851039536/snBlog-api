using Snblog.Models;

namespace Snblog.Service.Extensions;

public static class PhotoGalleryExtensions
{
    public static IQueryable<PhotoGalleryDto> SelectPhotoGallery(this IQueryable<PhotoGallery> ret)
    {
        return ret.AsNoTracking().Select(e => new PhotoGalleryDto {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Img = e.Img,
            Tag = e.Tag,
            Give = e.Give,
               
            UserId = e.UserId,
            User = new User {
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            TypeId = e.TypeId,
            Type = new PhotoGalleryType {
                Name = e.Type.Name
            },
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
        });
    }
        
}