using System;
using System.ServiceModel.Syndication;
using AutoMapper;
using Models.RSS;

namespace RSSter
{
    public class MapperConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<SyndicationItem, Item>()
                .ForMember(dest => dest.Description, opts => opts.MapFrom(item => item.Summary.Text))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opts => opts.MapFrom(item => item.Links[0].Uri.ToString()))
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(item => item.PublishDate.DateTime))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(item => item.Title.Text));
        }
    }
}