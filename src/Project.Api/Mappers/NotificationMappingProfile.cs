using AutoMapper;
using Project.Application.Dtos.Notifications;
using Project.Domain.Entities;

namespace Project.Api.Mappers;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<Notification, NotificationGetDto>();
        CreateMap<NotificationDto, Notification>();
        CreateMap<NotificationMultipleChannelDto, Notification>();
    }
}