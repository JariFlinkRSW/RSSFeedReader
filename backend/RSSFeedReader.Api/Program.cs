var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<RSSFeedReader.Api.Services.ISubscriptionStore, RSSFeedReader.Api.Services.InMemorySubscriptionStore>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("DevCors", policy =>
	{
		policy
			.WithOrigins(
				"http://localhost:5213",
				"https://localhost:7026")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHttpsRedirection();
}

app.UseCors("DevCors");

app.MapControllers();

app.Run();

public partial class Program
{
}
