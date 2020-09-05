using AutoMapper;
using PixelAPI.Models;
using PixelAPI.ViewModels;

namespace PixelAPI.Profiles
{
    public class MapperProfile  : Profile
    {
        public MapperProfile()
        {
            CreateMap<Map, MapView>();
            CreateMap<Player, PlayerView>();
            CreateMap<Server, ServerView>();
            CreateMap<Record, RecordView>()
                .ForMember(view => view.PlayerName, opt => opt.MapFrom(record => record.Player.Name))
                .ForMember(view => view.ModeName, opt => opt.MapFrom(record => record.Mode.ToString("f")))
                .ForMember(view => view.Map, opt => opt.MapFrom(rec => rec.MapName))
                .ForMember(view => view.ServerName, opt => opt.MapFrom(rec => rec.Server.Name));
;
            CreateMap<long, string>().ConvertUsing(int64 => int64.ToString());
        }
    }
}