using Hangfire;
using HangfireProject.HangfireJobs;

namespace HangfireProject.HangfireConfg
{
	public static class HangfireExcuteJobs
	{
		public static void RecurringJobs()
		{
			Jobs jobs = new();
			var turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

			RecurringJob.RemoveIfExists(nameof(jobs.SendMonthlyReport));

			RecurringJob.AddOrUpdate<IJobs>(nameof(jobs.SendMonthlyReport), c => c.SendMonthlyReport(), "0 10 5 * *", new RecurringJobOptions
			{

				TimeZone = turkeyTimeZone,

			});//crontab : her ayın 5 in de saat 10 TR


			//RecurringJob.AddOrUpdate<IJobs>(nameof(jobs.SendMonthlyReport), c => c.SendMonthlyReport(), Cron.Monthly(5,10), new RecurringJobOptions
			//{

			//	TimeZone = turkeyTimeZone,

			//});//cron : her ayın 5 in de saat 10 TR
		}

		

	}
}
