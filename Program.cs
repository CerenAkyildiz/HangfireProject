using Hangfire;
using HangfireProject.HangfireConfg;
using HangfireProject.HangfireJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IJobs, Jobs>();

builder.ConfigureHangfireService();//hangfire build conf


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHangfireDashboard("/cakyildizhangfire", new DashboardOptions
{
	DashboardTitle = "Ceren Aky�ld�z Hangfire Dashboard",
	AppPath = "/Home/HangfireAbout",
	DarkModeEnabled = true,
	
});//hangfire dashboard ayarlar�


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseHangfireServer(new BackgroundJobServerOptions
//{
//    SchedulePollingInterval = TimeSpan.FromMinutes(1), //hangfire server ,planlanan i�leri s�ralar�na g�re s�ralamak i�in zamanlamay� d�zenli olarak denetler.Varsay�lan 15 sn.
//    WorkerCount = Environment.ProcessorCount * 5 //arka planda �al��acal job say�s�
//});

app.Services.CreateScope();

HangfireExcuteJobs.RecurringJobs(); //Hangfire recurring job 

app.Run();
