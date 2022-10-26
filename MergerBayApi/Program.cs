using MergerBayApi.Extensions;
using MergerBayApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

String[] origins = new string[4] { "https://localhost:4200", "http://localhost:4200", "https://mergerbayclienttest1.azurewebsites.net", "http://mergerbayclienttest1.azurewebsites.net" };
// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
    builder.WithOrigins(origins)
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

//Register Application Services in this extension method
builder.Services.AddApplicationSerives(builder.Configuration);
//Register Identity Related Services in this extension method
builder.Services.AddIdentityServices(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
