using Hangfire.Server;

namespace HangfireProject.HangfireJobs
{
    public interface IJobs
    {
        //void DisplayCurrentDateTime(PerformedContext context);

        //void HelloWorld(PerformedContext context);

        //void ReportWeather();

        void SendMonthlyReport(); //recurringjob
        string SendWelcomeEmail(string email); //fire and forgert job

		string SendEmailToActivateAccount(); //continuation job

		string SendReminderAccountApproveEmail(); //delayed job



	}
}