using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using api.Models.Notifications.Requests;
using api.Models.Notifications.Responses;
using api.Services.Notifications;
using Moq;
using static api.Models.Notifications.ErrorMessages;
using static api.Services.CoreFunctions.Core;

namespace api.tests.Controllers
{
    public class SmsControllerTests
    {
        private Mock<ISmsNotificationService> _notificationServiceMock;

        public SmsControllerTests()
        {
            _notificationServiceMock = new Mock<ISmsNotificationService>();
        }

        private NotificationActionResults<SmsItem> GetMockSuccessItems()
        {
            return ActionResults(new List<SmsItem>(){new SmsItem()
            {
                Content = "Test Content", 
                Recipient = "07852556556",
                SentDateUtc = DateTime.UtcNow
            }}, true, new List<string>());
        }

        private NotificationActionResults<SmsItem> GetMockFailedItems()
        {
            return ActionResults(new List<SmsItem>(), false, new List<string>()
            {
                Empty_Content
            });
        }

        private NotificationActionResult GetMockNotificationSuccessObject()
        {
            return ActionResult(true, new List<string>());
        }

        private NotificationActionResult GetMockNotificationFailedObject()
        {
            return ActionResult(false, new List<string>(){ Null_Request });
        }

        [Fact]
        public void GetSmsHistoryAsync_ReturnsActionResult_WithCollectionOfSmsItem()
        {
            var date = "17-07-2022";
            var smsController = new SmsController(_notificationServiceMock.Object);
            _notificationServiceMock
                .Setup(_ => _.GetMessagesAsync(It.IsAny<SmsHistoryRequest>()))
                .ReturnsAsync(GetMockSuccessItems());
            var result = smsController.GetSmsHistoryAsync(date, date);

            //// Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsAssignableFrom<NotificationActionResults<SmsItem>>(
                viewResult.Value);

            Assert.True(model.Success);
            Assert.Single(model.Items);
            Assert.Empty(model.ErrorMessages);
        }

        [Fact]
        public void GetSmsHistoryAsync_ReturnsActionResult_WithCollectionOfErrorMessages()
        {
            var date = "17-07-2022";
            var smsController = new SmsController(_notificationServiceMock.Object);
            _notificationServiceMock
                .Setup(_ => _.GetMessagesAsync(It.IsAny<SmsHistoryRequest>()))
                .ReturnsAsync(GetMockFailedItems());
            var result = smsController.GetSmsHistoryAsync(date, date);

            //// Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            var model = Assert.IsAssignableFrom<NotificationActionResults<SmsItem>>(
                viewResult.Value);

            Assert.False(model.Success);
            Assert.Empty(model.Items);
            Assert.Single(model.ErrorMessages);
        }

        [Fact]
        public void SendSmsAsync_ReturnsActionResult_WithSuccessObject()
        {
            var smsController = new SmsController(_notificationServiceMock.Object);
            _notificationServiceMock
                .Setup(_ => _.SendSmsAsync(It.IsAny<SmsRequest>()))
                .ReturnsAsync(GetMockNotificationSuccessObject());
            var result = smsController.SendSmsAsync(new SmsRequest());

            //// Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsAssignableFrom<NotificationActionResult>(
                viewResult.Value);

            Assert.True(model.Success);
            Assert.Empty(model.ErrorMessages);
        }

        [Fact]
        public void SendSmsAsync_ReturnsActionResult_WithFailedObject()
        {
            var smsController = new SmsController(_notificationServiceMock.Object);
            _notificationServiceMock
                .Setup(_ => _.SendSmsAsync(It.IsAny<SmsRequest>()))
                .ReturnsAsync(GetMockNotificationFailedObject());
            var result = smsController.SendSmsAsync(new SmsRequest());

            //// Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            var model = Assert.IsAssignableFrom<NotificationActionResult>(
                viewResult.Value);

            Assert.False(model.Success);
            Assert.Single(model.ErrorMessages);
        }
    }
}
