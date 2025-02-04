using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Posts.API.Data;
using Posts.API.Mappings;
using Posts.API.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("pgsqlConnectionString")));
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnectionString")));


builder.Services.AddScoped<IPostRepository, SQLPostRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


builder.Services.AddIdentityCore<IdentityUser>()
		.AddRoles<IdentityRole>()
		.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("postAuth")
		.AddEntityFrameworkStores<AuthDbContext>()
		.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
	
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	   .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
	   {
		   ValidateIssuer = true,
		   ValidateAudience = true,
		   ValidateLifetime = true,
		   ValidateIssuerSigningKey = true,
		   ValidIssuer = builder.Configuration["Jwt:Issuer"],
		   ValidAudience = builder.Configuration["Jwt:Audience"],
		   IssuerSigningKey = new SymmetricSecurityKey(Encoding
		   .UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
