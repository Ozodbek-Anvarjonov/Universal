using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.Filters;
using Project.Application.Common.Notifications.Services;
using Project.Application.Dtos.Notifications;
using Project.Application.Services;
using Project.Domain.Entities;

namespace Project.Api.Controllers;

public class NotificationsController(
    IMapper mapper,
    INotificationSenderService senderService,
    IUserService userService,
    INotificationService notificationService
    ) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] NotificationFilter filter, CancellationToken cancellationToken)
    {
        var data = await notificationService.GetAsync(filter, cancellationToken: cancellationToken);

        return Ok(mapper.Map<IEnumerable<NotificationGetDto>>(data));
    }

    [HttpPost("send")]
    public async ValueTask<IActionResult> Send([FromBody] NotificationDto dto, CancellationToken cancellationToken = default)
    {
        var notification = mapper.Map<Notification>(dto);
        notification.ReceiverUser = await userService.GetByIdAsync(dto.ReceiverUserId, false, cancellationToken);

        var result = await senderService.SendAsync(notification, null, cancellationToken);

        return Ok(mapper.Map<NotificationGetDto>(result));
    }

    [HttpPost("send-multiple")]
    public async ValueTask<IActionResult> Send([FromBody] NotificationMultipleChannelDto dto, CancellationToken cancellationToken = default)
    {
        var notification = mapper.Map<Notification>(dto);
        notification.ReceiverUser = await userService.GetByIdAsync(dto.ReceiverUserId, false, cancellationToken);

        var result = await senderService.SendAsync(notification, null, dto.ChannelTypes, cancellationToken);

        return Ok(mapper.Map<IEnumerable<NotificationGetDto>>(result));
    }
}