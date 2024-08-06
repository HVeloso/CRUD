using ApiCrud.Data;
using ApiCrud.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

// Adicione o serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:51601")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});     

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configurando os endpoints
app.AddTaskEndpoints();

// Use o middleware CORS
app.UseCors("AllowSpecificOrigin");

app.Run();
