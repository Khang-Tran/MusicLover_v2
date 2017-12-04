using AutoMapper;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;

namespace MusicLover.WebApp.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<SavedGigResource, Gig>();
            CreateMap<Gig, SavedGigResource>();
        }
    }
}
