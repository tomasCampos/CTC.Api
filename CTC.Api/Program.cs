using CTC.Api.Auth;
using CTC.Application;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

const string allowedOrigins = "CtcApiAllowedOrigins";
builder.Services.AddCors(options => 
{
    options.AddPolicy(allowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://ctcconstrucoes.vercel.app")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// For running in Railway
var portVar = Environment.GetEnvironmentVariable("PORT");
if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options => { options.ListenAnyIP(port); }); 
}

builder.Services.AddControllers();
builder.Services.AddSingleton(FirebaseApp.Create());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add project Services
builder.Services.AddApplication();

var app = builder.Build();

//Add cors policy
app.UseCors(allowedOrigins);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
