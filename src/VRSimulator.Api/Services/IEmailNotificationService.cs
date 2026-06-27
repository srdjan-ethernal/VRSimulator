using VRSimulator.Api.Domain;
using VRSimulator.Api.Models;

namespace VRSimulator.Api.Services;

public interface IEmailNotificationService
{
    Result<ReminderResponse> SendReminder(Worker worker, string subject, string message);
}
