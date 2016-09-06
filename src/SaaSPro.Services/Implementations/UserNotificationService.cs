using System;
using System.Linq;
using SaaSPro.Common;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.SubscriptionsService;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;
using AutoMapper;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Web.Common;


namespace SaaSPro.Services.Implementations
{
	public class UserNotificationService : IUserNotificationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly EFDbContext _session;

		public UserNotificationService(IUnitOfWork unitOfWork, EFDbContext session)
		{
			_unitOfWork = unitOfWork;
			_session = session;
		}

		public NotificationsListModel GetCurrentUserNotifications(GetCurrentUserNotificationsRequest request)
		{
		    NotificationsListModel model = new NotificationsListModel
		    {
		        Notifications = Mapper.Engine.MapPaged<NotificationMessage, NotificationsListModel.Notification>
		            (GetNotifications(request.Page - 1, request.PageSize, request.UserId))
		    };
		    return model;
		}

		public bool Delete(Guid id, Guid userId)
		{
			var notification = _session.UserNotifications
				.FirstOrDefault(n => n.Message.Id == id && n.User.Id == userId);

			if (notification != null)
			{
				_session.UserNotifications.Remove(notification);
				_unitOfWork.Commit();
			}

			return true;
		}

		private IPagedList<NotificationMessage> GetNotifications(int page, int pageSize, Guid userId)
		{
			var query = _session.UserNotifications.Where(n => n.User.Id == userId);
			var notificationsCount = _session.UserNotifications.Count();

			return new PagedList<NotificationMessage>(
				query.Select(n => n.Message)
					.OrderByDescending(m => m.CreatedOn)
					.Skip(pageSize*page)
					.Take(pageSize),
				page,
				pageSize,
				notificationsCount);
		}
	}
}
