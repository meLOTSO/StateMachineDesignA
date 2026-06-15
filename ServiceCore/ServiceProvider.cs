using System.Reflection;
using StateMachine.Abstracts;

namespace StateMachine.ServiceCore;

public class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, ServiceDescriptor> _descriptors = new();
    private readonly Dictionary<Type, object> _singletons = new();
    private readonly Dictionary<Type, object> _scoped = new();

    public ServiceProvider(IServiceCollection services)
    {
        _descriptors = services.Descriptors.ToDictionary((desc) => desc.ServiceType);
    }

    public object? GetService(Type serviceType)
    {
        if (_descriptors.TryGetValue(serviceType, out var descriptor))
        {
            return CreateInstance(descriptor);
        }
        return null;
    }
    public T GetRequiredService<T>() where T : notnull
    {
        if (GetService(typeof(T)) is T value)
            return value;
        throw new InvalidOperationException($"Service of type {typeof(T)} not registered");
    }

    public object CreateService(ServiceDescriptor descriptor)
    {
        if (descriptor.Lifetime == ServiceLifetime.Singleton)
        {
            if (_singletons.TryGetValue(descriptor.ServiceType, out var cached))
                return cached;
        }
        if (descriptor.Lifetime == ServiceLifetime.Scoped)
        {
            if (_scoped.TryGetValue(descriptor.ServiceType, out var cached))
                return cached;
        }

        object instance = CreateInstance(descriptor);

        if (descriptor.Lifetime == ServiceLifetime.Singleton)
            _singletons[descriptor.ServiceType] = instance;
        else if (descriptor.Lifetime == ServiceLifetime.Scoped)
            _singletons[descriptor.ServiceType] = instance;

        return instance;
    }

    public object CreateInstance(ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationFactory != null)
            return descriptor.ImplementationFactory(this);

        if (descriptor.ImplementationInstance != null)
            return descriptor.ImplementationInstance;

        Type implementationType = descriptor.ImplementationType ?? descriptor.ServiceType;

        var constructor = implementationType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .OrderByDescending(c => c.GetParameters().Length)
            .First();

        var parameters = constructor.GetParameters();
        var arguments = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            var paramType = parameters[i].ParameterType;
            arguments[i] = GetService(paramType)
                ?? throw new InvalidOperationException(
                    $"Cannot resolve parameter '{paramType}' for '{implementationType}'");
        }
        return Activator.CreateInstance(implementationType, arguments)!;
    }
}