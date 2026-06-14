// namespace StateMachine.ServiceCore;

// public class ServiceProvider : IServiceProvider
// {
//     private Dictionary<Type, ServiceDescriptor> _descriptors { get; } = new();
//     private Dictionary<Type, object> _singletons { get; } = new();
//     private Dictionary<Type, object> _scoped { get; } = new();


//     public object CreateService(ServiceDescriptor descriptor)
//     {
//         if (descriptor.Lifetime == ServiceLifetime.Singleton)
//         {
//             if (_singletons.TryGetValue(descriptor.ServiceType, out var value))
//             {
//                 return value;
//             }
//             if (descriptor.ImplementationType is Type type)
//             {
//                 Activator.CreateInstance(type);
//             }
//             Type instanceType = descriptor.ImplementationType ?? descriptor.ServiceType;

//         }
//     }

//     public void CreateInstance(Type type)
//     {
//         type.ge
//     }
// }