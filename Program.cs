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
	DashboardTitle = "Ceren Akyýldýz Hangfire Dashboard",
	AppPath = "/Home/HangfireAbout",
	DarkModeEnabled = true,
	
});//hangfire dashboard ayarlarý


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseHangfireServer(new BackgroundJobServerOptions
//{
//    SchedulePollingInterval = TimeSpan.FromMinutes(1), //hangfire server ,planlanan iþleri sýralarýna göre sýralamak için zamanlamayý düzenli olarak denetler.Varsayýlan 15 sn.
//    WorkerCount = Environment.ProcessorCount * 5 //arka planda çalýþacal job sayýsý
//});

app.Services.CreateScope();

HangfireExcuteJobs.RecurringJobs(); //Hangfire recurring job 

app.Run();
