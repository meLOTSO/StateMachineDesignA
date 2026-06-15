using StateMachine.Abstracts;

namespace StateMachine.ServiceCore;

public class ServiceCollection : IServiceCollection
{
    public List<ServiceDescriptor> Descriptors { get; } = [];

    public void AddService<TService, TImplementation>(ServiceLifetime lifetime) where TImplementation : TService
    {
        var descriptor = new ServiceDescriptor(
            typeof(TService),
            typeof(TImplementation),
            lifetime);
        Descriptors.Add(descriptor);
    }
    public void AddService<TService, TImplementation>(TImplementation instance, ServiceLifetime lifetime) where TImplementation : TService
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));

        var descriptor = new ServiceDescriptor(
            typeof(TService),
            instance,
            lifetime);
        Descriptors.Add(descriptor);
    }
    public void AddService<TService>(ServiceLifetime lifetime) where TService : class
    {
        var descriptor = new ServiceDescriptor(
            typeof(TService),
            lifetime);
        Descriptors.Add(descriptor);
    }
    public void AddService<TService>(TService instance, ServiceLifetime lifetime) where TService : class
    {
        var descriptor = new ServiceDescriptor(
            typeof(TService),
            instance,
            lifetime);
        Descriptors.Add(descriptor);
    }

    public void AddSingleton<TService, TImplementation>() where TImplementation : TService
        => AddService<TService, TImplementation>(ServiceLifetime.Singleton);

    public void AddSingleton<TService, TImplementation>(TImplementation instance) where TImplementation : TService
        => AddService<TService, TImplementation>(instance, ServiceLifetime.Singleton);

    public void AddSingleton<TService>() where TService : class
        => AddService<TService>(ServiceLifetime.Singleton);

    public void AddSingleton<TService>(TService instance) where TService : class
        => AddService<TService>(instance, ServiceLifetime.Singleton);

    public void AddScoped<TService, TImplementation>() where TImplementation : TService
        => AddService<TService, TImplementation>(ServiceLifetime.Scoped);

    public void AddScoped<TService, TImplementation>(TImplementation instance) where TImplementation : TService
        => AddService<TService, TImplementation>(instance, ServiceLifetime.Scoped);

    public void AddScoped<TService>() where TService : class
        => AddService<TService>(ServiceLifetime.Scoped);

    public void AddScoped<TService>(TService instance) where TService : class
        => AddService<TService>(instance, ServiceLifetime.Scoped);

    public void AddTransient<TService, TImplementation>() where TImplementation : TService
        => AddService<TService, TImplementation>(ServiceLifetime.Transient);

    public void AddTransient<TService, TImplementation>(TImplementation instance) where TImplementation : TService
        => AddService<TService, TImplementation>(instance, ServiceLifetime.Transient);

    public void AddTransient<TService>() where TService : class
        => AddService<TService>(ServiceLifetime.Transient);

    public void AddTransient<TService>(TService instance) where TService : class
        => AddService<TService>(instance, ServiceLifetime.Transient);
}