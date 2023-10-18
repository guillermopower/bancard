var AllowSpecific = "AllowSpecific";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add Cors
builder.Services.AddCors(options =>
         options.AddPolicy("AllowSpecific",
        builder => builder.WithOrigins("http://localhost:4200", "http://mywebsite.com")
            .AllowAnyHeader()
            .AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowSpecific);
app.UseCors(AllowSpecific);

app.UseAuthorization();

app.MapControllers();

app.Run();
