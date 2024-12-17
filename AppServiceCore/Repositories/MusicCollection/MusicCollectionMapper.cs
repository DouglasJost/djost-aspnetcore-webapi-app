using AppServiceCore.Models.MusicCollection;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Repositories.MusicCollection
{
    public class MusicCollectionMapper : Profile
    {
        public MusicCollectionMapper()
        {
            CreateMap<MusicCollectionBandResult, MusicCollectionBandDto>()
                .ForMember(d => d.BandId, o => o.MapFrom(s => s.BandId))
                .ForMember(d => d.BandName, o => o.MapFrom(s => s.BandName))
                .ForMember(d => d.FormationDate, o => o.MapFrom(s => s.FormationDate))
                .ForMember(d => d.DisbandDate, o => o.MapFrom(s => s.DisbandDate))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));
        }
    }
}
