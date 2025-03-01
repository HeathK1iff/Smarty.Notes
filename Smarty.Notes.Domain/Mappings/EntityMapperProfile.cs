using AutoMapper;
using Smarty.Notes.Domain.Entities.Aggregates;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Mappings
{
    public class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<NoteAggregate, Note>()
                .ForMember(dest => dest.Id, source => source.MapFrom(v => v.Id))
                .ForMember(dest => dest.Content, source => source.MapFrom(v => v.Content))
                .ForMember(dest => dest.Created, source => source.MapFrom(v => v.Created))
                .ForMember(dest => dest.CreatedBy, source => source.MapFrom(v => v.CreatedBy));

        }
    }
}