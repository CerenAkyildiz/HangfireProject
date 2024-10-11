using Hangfire;
using System.Diagnostics;

namespace HangfireProject.HangfireJobs
{
	public class Jobs : IJobs
	{
		public static readonly string[] Summaries = new[]
	   {
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	   };

		//[Queue("default")]
		//[JobDisplayName("Display current date and time")]
		//public void DisplayCurrentDateTime(PerformedContext context)
		//{
		//	context.WriteLine($"The current date and time is: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}");
		//	context.WriteLine($"The current UTC date and time is: {DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)}");
		//}

		//[Queue("helloworld")]
		//[JobDisplayName("say hello to world")]
		//public void HelloWorld(PerformedContext context)
		//{
		//	context.WriteLine("hello ceff");
		//}

		//public void ReportWeather()
		//{
		//	var array = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateTime.Now.AddDays(index),
		//		TemperatureC = Random.Shared.Next(-20, 55),
		//		Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		//	})
		//	.ToArray();
		//	foreach (var item in array)
		//	{
		//		Debug.Write(item.Date + " | ");
		//		Debug.Write(item.TemperatureC + " | ");
		//		Debug.WriteLine(item.Summary);
		//		Debug.WriteLine("".PadRight(40, '*'));
		//	}
		//}

		[JobDisplayName("Aylık rapor gonderimi")]
		public void SendMonthlyReport()
		{
			//Burası bir data toplayıp aylık bu datayı mail yardımıyla gönderir.
			//business işleri kodlanır.

			Debug.WriteLine("Aylık rapor gönderildi!");
		}

		[JobDisplayName("FireaAndForgetJob")]
		public string SendWelcomeEmail(string email)
		{
			//ilk kayıt olunduğunda gönderilen mail.
			//business işleri kodlanır.
			var txt = $"Merhaba/Hoşgeldiniz maili gönderildi. {email}";
			Debug.WriteLine(txt);
			return txt;
		}

		[JobDisplayName("DelayedJob")]
		public string SendEmailToActivateAccount()
		{
			//iilk kayıt sonrası hoşgeldin maili sonrası hesap aktifliği için onaylama maili
			//business işleri kodlanır.
			var txt = $"Merhaba, Lütfen hesabınızı onaylayınız.";
			Debug.WriteLine(txt);
			return txt;
		}

		[JobDisplayName("ContinuationJob")]
		public string SendReminderAccountApproveEmail()
		{
			//kayıt sonrası hesap onaylama için hatırlatma maili gönderme
			//business işleri kodlanır.
			var txt = $"Merhaba, lütfen hesabınızın aktif olabilmesi için aşağıdaki linkten emailinizi doğrulayınız";
			Debug.WriteLine(txt);
			return txt;
		}
	}
}