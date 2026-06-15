namespace StateMachine.Abstracts;
using StateMachine.ServiceCore;

public interface IServiceCollection
{
    List<ServiceDescriptor> Descriptors { get; }

    void AddService<TService, TImplementation>(ServiceLifetime lifetime) where TImplementation : TService;
    void AddService<TService, TImplementation>(TImplementation instance, ServiceLifetime lifetime) where TImplementation : TService;
    void AddService<TService>(ServiceLifetime lifetime) where TService : class;
    void AddService<TService>(TService instance, ServiceLifetime lifetime) where TService : class;

    void AddSingleton<TService, TImplementation>() where TImplementation : TService;
    void AddSingleton<TService, TImplementation>(TImplementation instance) where TImplementation : TService;
    void AddSingleton<TService>() where TService : class;
    void AddSingleton<TService>(TService instance) where TService : class;

    void AddScoped<TService, TImplementation>() where TImplementation : TService;
    void AddScoped<TService, TImplementation>(TImplementation instance) where TImplementation : TService;
    void AddScoped<TService>() where TService : class;
    void AddScoped<TService>(TService instance) where TService : class;

    void AddTransient<TService, TImplementation>() where TImplementation : TService;
    void AddTransient<TService, TImplementation>(TImplementation instance) where TImplementation : TService;
    void AddTransient<TService>() where TService : class;
    void AddTransient<TService>(TService instance) where TService : class;
}