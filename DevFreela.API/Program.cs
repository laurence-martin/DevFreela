using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllComments;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetCommentByCommentId;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infra.AuthService;
using DevFreela.Infra.Persistence.Repositories;
using DevFreela.Infra.Persistense;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<DevFreelaDbContext>(
                     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevFreelaCS")));

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
    conf.AutomaticValidationEnabled = false;
});


//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

ConfigureRepositories();

ConfigureCommands();

ConfigureOpenApi();

ConfigureJwtSecurity();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();


void ConfigureRepositories()
{
    builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    builder.Services.AddScoped<ISkillRepository, SkillRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
}

void ConfigureCommands()
{
    builder.Services.AddMediatR(typeof(CreateProjectCommand));
    builder.Services.AddMediatR(typeof(DeleteProjectCommand));
    builder.Services.AddMediatR(typeof(UpdateProjectCommand));
    builder.Services.AddMediatR(typeof(StartProjectCommand));
    builder.Services.AddMediatR(typeof(FinishProjectCommand));
    
    builder.Services.AddMediatR(typeof(CreateProjectCommentCommand));
    builder.Services.AddMediatR(typeof(GetAllCommentsByProjectQuery));
    builder.Services.AddMediatR(typeof(GetCommentByCommentIdQuery));

    builder.Services.AddMediatR(typeof(CreateSkillCommand));
    builder.Services.AddMediatR(typeof(GetProjectByIdQuery));
    builder.Services.AddMediatR(typeof(GetAllProjectsQuery));
    builder.Services.AddMediatR(typeof(GetAllSkillsQuery));
    builder.Services.AddMediatR(typeof(GetSkillByIdQuery));

    builder.Services.AddMediatR(typeof(CreateUserCommand));
    builder.Services.AddMediatR(typeof(GetUserByIdQuery));
}

void ConfigureOpenApi()
{
    builder.Services.AddSwaggerGen(x =>
    {
        x.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DevFreela.API",
            Version = "v1.0",
            TermsOfService = new Uri("http://www.laurence_martin.com/terms_of_service"),
            Contact = new OpenApiContact
            {
                Name = "Laurence Martin",
                Email = "laurence_martin@outlook.com",
                Url = new Uri("http://www.laurence_martin.com")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://laurence_martin.com/license")
            },
            Description = "API do curso de .net do Método .NET Direto ao Ponto"
        });

        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header"
        });

        x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{ }
            }
        });
    }); 
}
void ConfigureJwtSecurity()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
}