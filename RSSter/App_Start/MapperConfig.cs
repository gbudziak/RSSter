using System;
using System.ServiceModel.Syndication;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.RSS;
using Models.ViewModels;

namespace RSSter
{
    public static class MapperConfig
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
                .ForMember(dest => dest.RatingMinusCount, opts => opts.MapFrom(item => item.RatingMinus))
                .ForMember(dest => dest.RatingMinus, opt => opt.Ignore())
                .ForMember(dest => dest.RatingPlus, opt => opt.Ignore());

            Mapper.CreateMap<UserItem, CompleteItemInfo>()
                .ForMember(dest => dest.UserItemId, opts => opts.MapFrom(uitem => uitem.Id))
                .ForMember(dest => dest.Read, opts => opts.MapFrom(uitem => uitem.Read))
                .ForMember(dest => dest.RatingPlus, opts => opts.MapFrom(uitem => uitem.RatingPlus))
                .ForMember(dest => dest.RatingMinus, opts => opts.MapFrom(uitem => uitem.RatingMinus));

            Mapper.CreateMap<Channel, ShowUserChannelsViewModel>()
                .ForMember(dest => dest.ChannelId, opts => opts.MapFrom(channel => channel.Id))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(channel => channel.Url))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(channel => channel.ImageUrl))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(channel => channel.Title));

            Mapper.CreateMap<UserChannel, ShowUserChannelsViewModel>()
                .ForMember(dest => dest.UserChannelId, opt => opt.MapFrom(uchannel => uchannel.Id));

            Mapper.CreateMap<Item, ShowAllUserItemsViewModel>()
              .ForMember(dest => dest.ItemId, opts => opts.MapFrom(item => item.Id))
              .ForMember(dest => dest.Url, opts => opts.MapFrom(item => item.Url))
              .ForMember(dest => dest.Description, opts => opts.MapFrom(item => item.Description))
              .ForMember(dest => dest.Title, opts => opts.MapFrom(item => item.Title))
              .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(item => item.PublishDate))
              .ForMember(dest => dest.RatingPlusCount, opts => opts.MapFrom(item => item.RatingPlus))
              .ForMember(dest => dest.RatingMinusCount, opts => opts.MapFrom(item => item.RatingMinus));

            Mapper.CreateMap<UserItem, ShowAllUserItemsViewModel>()
                .ForMember(dest => dest.UserItemId, opts => opts.MapFrom(uitem => uitem.Id))
                .ForMember(dest => dest.Read, opts => opts.MapFrom(uitem => uitem.Read))
                .ForMember(dest => dest.RatingPlus, opts => opts.MapFrom(uitem => uitem.RatingPlus))
                .ForMember(dest => dest.RatingMinus, opts => opts.MapFrom(uitem => uitem.RatingMinus));

            Mapper.CreateMap<Channel, ShowAllUserItemsViewModel>()
                .ForMember(dest => dest.ChannelTitle, opt => opt.MapFrom(channel => channel.Title))
                .ForMember(x => x.Url, opts => opts.Ignore())
                .ForMember(dest => dest.Title, opt => opt.Ignore());

            Mapper.CreateMap<Channel, UserItemsViewModel>()
                .ForMember(dest => dest.Url, opts => opts.MapFrom(channel => channel.Url))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(channel => channel.ImageUrl))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(channel => channel.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(channel => channel.Description))
                .ForMember(dest => dest.Readers, opt => opt.MapFrom(channel => channel.Readers))
                .ForMember(dest => dest.ChannelId, opt => opt.MapFrom(channel => channel.Id))
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            Mapper.CreateMap<UserChannel, UserItemsViewModel>()
                .ForMember(dest => dest.UserChannelId, opt => opt.MapFrom(userChannel => userChannel.Id))
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            Mapper.CreateMap<Channel, SearchChannel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(channel => channel.Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(channel => channel.ImageUrl))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(channel => channel.Url))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(channel => channel.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(channel => channel.Description))
                .ForMember(dest => dest.Readers, opt => opt.MapFrom(channel => channel.Readers));

            Mapper.CreateMap<IdentityUser, SubscriptionViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(user => user.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(user => user.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(user => user.UserName));

        }
    }
}