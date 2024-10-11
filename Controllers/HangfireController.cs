using Hangfire;
using HangfireProject.HangfireJobs;
using Microsoft.AspNetCore.Mvc;

namespace HangfireProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HangfireController : ControllerBase
	{
		private readonly IJobs jobs;

		public HangfireController(IJobs jobs)
		{
			this.jobs = jobs;
		}

		[HttpPost]
		[Route("FireAndForgetJob")]
		public IActionResult FireAndForgetJob()
		{
			//burada senaryo bir kuallanıcı sistemize kayıt olduktan sonra arka planda email gönderme işini yapan bi api.
			Console.WriteLine($"Kullanıcı kaydedildi:  email");


			string email = "Merhaba , Ailemize hoş geldiniz. Keyifli Günler dileriz..";

			BackgroundJob.Enqueue<IJobs>(job => job.SendWelcomeEmail(email));

			return Ok("Kullanıcı kaydedildi ve iş zamanlamaları yapıldı.");
		}
		[HttpPost]
		[Route("DelayedJob")]
		public IActionResult DelayedJob(int setTimeForDay)
		{
			//burada senaryo bir kuallanıcı sistemize kayıt olduktan sonra arka planda belli süre sonra hesap aktifliği için doğrulama/hatırlatma maili.

			//TimeSpan delay = TimeSpan.FromSeconds(setTimeForDay);
			TimeSpan delay = TimeSpan.FromDays(setTimeForDay);

			BackgroundJob.Schedule<IJobs>(job => job.SendReminderAccountApproveEmail(), delay);

			return Ok($"Kullanıcı ya hatırlatma maili  {delay.TotalMinutes} sonra gönderilecek");
		}	
		
		[HttpPost]
		[Route("ContinuationJob")]
		public IActionResult ContinuationJob()
		{
			//burada senaryo bir kuallanıcı sistemize kayıt olduktan sonra arka planda jhoşgeldin maili sonrası hespa aktifliği onaylama maili atmak için..
			string email = "Merhaba , Ailemize hoş geldiniz. Keyifli Günler dileriz..";
			string parentJobId = BackgroundJob.Enqueue<IJobs>(job => job.SendWelcomeEmail(email));
			 BackgroundJob.ContinueJobWith<IJobs>(parentJobId, jobs => jobs.SendEmailToActivateAccount());
			
			return Ok($"Kullanıcıya hoşgeldin ve aktivasyon maili yolllandı.İlk çalışan Jobid: {parentJobId} - ContinuationJob");
		}
	}
}
