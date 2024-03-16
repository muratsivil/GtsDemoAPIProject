using System.Reflection;
using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Update;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Update;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services){
        // AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // MediatR (CQRS Pattern Implementation) 
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });
        
        services.AddScoped<IValidator<CreateMemberCommand>, CreateMemberCommandValidator>();
        services.AddScoped<IValidator<UpdateMemberCommand>, UpdateMemberCommandValidator>();

        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

        services.AddValidatorsFromAssemblyContaining<UpdateMemberCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateMemberCommandValidator>();

        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidator>();
      
        return services;
    }
    
}
