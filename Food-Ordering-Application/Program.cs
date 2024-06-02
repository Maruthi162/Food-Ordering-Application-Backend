
using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Food_Ordering_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuring DbContext
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FlashFoodsContext>(options => options.UseSqlServer(connectionString));

            //Configuring Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FlashFoodsContext>()
                .AddDefaultTokenProviders();

            //Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

           
            // Add services to the container.
            builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
            builder.Services.AddScoped<IRestaurantRepo, RestaurantRepo>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<ICartRepo, CartServices>();
            builder.Services.AddScoped<IOrderDetailsServices, OrderDetailsServices>();
            builder.Services.AddScoped<IOrderRepo,OrderServices>();
            builder.Services.AddScoped<NotificationService>();

            builder.Services.AddSingleton(new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(
                configuration["EmailSettings:Email"],
                configuration["EmailSettings:Password"]),
                EnableSsl = true
            });

            builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
            });



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger with login locked
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                     }
                 });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            // Make sure Rdirection -> Authentication -> Authorization order should be same

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            


            app.MapControllers();

            app.Run();
        }
    }
}
