using System;
using System.ServiceModel.Syndication;
using AutoMapper;
using Models.RSS;
using Models.ViewModels;

namespace RSSter
{
    public class MapperConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<SyndicationItem, Item>()
                .ForMember(dest => dest.Description, opts => opts.MapFrom(item => item.Summary.Text))
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Url, opts => opts.MapFrom(item => item.Links[0].Uri.ToString()))
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(item => item.PublishDate.DateTime))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(item => item.Title.Text));

            Mapper.CreateMap<Item, CompleteItemInfo>()
                .ForMember(dest => dest.ItemId, opts => opts.MapFrom(item => item.Id))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(item => item.Url))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(item => item.Description))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(item => item.Title))
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(item => item.PublishDate))
                .ForMember(dest => dest.RatingPlusCount, opts => opts.MapFrom(item => item.RatingPlus))
                .ForMember(dest => dest.RatingMinusCount, opts => opts.MapFrom(item => item.RatingMinus));

            Mapper.CreateMap<UserItem, CompleteItemInfo>()
                .ForMember(dest => dest.UserItemId, opts => opts.MapFrom(uitem => uitem.Id))
                .ForMember(dest => dest.Read, opts => opts.MapFrom(uitem => uitem.Read))
                .ForMember(dest => dest.RatingPlus, opts => opts.MapFrom(uitem => uitem.RatingPlus))
                .ForMember(dest => dest.RatingMinus, opts => opts.MapFrom(uitem => uitem.RatingMinus));
        }
    }
}