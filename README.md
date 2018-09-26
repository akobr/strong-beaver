# StrongBeaver library/framework

The StrongBeaver is .NET application pattern and tools mainly designed for Xamarin, but can be used with any platform and any layered architecture.

![Strong beaver image](doc/design/img-strong-beaver.png)

**The library is migrating to .NET standard and going to be split to small and more specific libraries!**

**The library is in early stage (ALFA version). A lot of stuff can be changed and unit testing is coming. Can be used for testing and as a playground, but for a real deployment please wait for a stable release.**

> Simplicity is the ultimate sophistication.
>
> &mdash; *Leonardo da Vinci*

## Work in progress

- [X] Make the library more .net standard friendly
- [X] Split library to multiple libraries
- [X] Remove service locator as a anti-pattern from the library core
- [ ] Update this documentation

## Introduction

The architecture pattern is called **TVMS** from ***T**idy **V**iew and **M**odel **S**eparation* and has been designed to be able to use it with any View-Model layered application.
The middle layer is universally named **separation**, which can be used with any well-known patterns, e.g. [MVC](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller), [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel), or any [MVP](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93presenter).

> The pattern **TVMS** has been created to help us to simplify application design and guide us how to separate concerns of layers and minimise coupling inside sub-systems.

The library as one product is called **StrongBeaver**. The concepts and base interfaces can be used for any .NET application, but the library is mainly created for *[Xamarin](https://www.xamarin.com/) and WPF platform*.

## Architecture pattern diagrams

The library is mainly designed for MVVM friendly technologies, but can be used with any layered architecture. See diagrams below.

### MVVM with universal XAML binder

![overview](doc/implementation/architecture/architecture-mvvm.png)

### Generic MVVM

![overview](doc/implementation/architecture/architecture-generic-mvvm.png)

### MVC (model-view-controller)

![overview](doc/implementation/architecture/architecture-mvc.png)

### The PureMVC

![overview](doc/implementation/architecture/architecture-puremvc.png)

## Concepts

Concept | Description
--- | ---
Activator | Static public class which will be called on application start for initialisation purposes. This is the perfect place for filing dependency injection container. Should contains entire [Composition root](http://blog.ploeh.dk/2011/07/28/CompositionRoot/).
Binder *(MVVM)* | Middle-man responsible for data and commands binding between view and view model.
IoC and DI | Inversion of Control and Dependency Injection should be the skeleton of an application. We are recommending to use [Register-Resolve-Release](http://blog.ploeh.dk/2010/09/29/TheRegisterResolveReleasepattern/) (RRR or 3R) pattern.
Factory | When we speak about factories we mostly mean an [Abstract Factory](https://en.wikipedia.org/wiki/Abstract_factory_pattern) which is a generic type, and the return type of the *Create* method is determined by the type of the factory itself. In other words, *factory can only return instances of a single type*.
Manager | Long-life and stateful service inside a specific instance of object.
MessageBus | Message bus for a specific group of objects, e.g. *services* or *view models (MVVM)*.
Provider | The provider / facade / locator for a specific type of objects, e.g. *services*, *view models* and more.
Service | Stateful or stateless, singleton and long-life service, which is registered in system.
*ServiceLocator (IoC)* | *[ServiceLocator](https://msdn.microsoft.com/en-us/library/ff648968.aspx) is a generic factory for multiple types. Nevertheless, **we don't recommended it**, because it is an [anti-pattern](http://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/) and an abstract factories [should be used instead](http://blog.ploeh.dk/2010/11/01/PatternRecognitionAbstractFactoryorServiceLocator/).*
Store | In memory storage, responsible for caching and reusability of objects. Should be used with business and view objects.
Strategy | Lightweight service / behaviour inside a instance of object, which is used in *pure unit of work* approach.
TVMS (Tidy View and Model Separation) | Universal architectural pattern for any layered application.
View Model | Bindable model of view, bricks for MVVM and binding is a grout. Under the name **view models** are hidden top level models for *pages / forms / windows* and smaller objects with shorter life-time are called **view objects**, for better distinction.
XAML | In the Microsoft solution stack, the universal binder is a markup language called XAML.

## Library basics

> Everything is implemented with the main focus on **extensibility** and **flexibility**.

The frame of an application should be **Dependency Injection with IoC container**. This abstraction is here not just to allow us to use any container but mostly for forcing us to use it only for storing *services*, *stores*, *providers* and *factories*, because all registered types are singletons by default and the interface is purposefully simple and lightweight. If no concrete container is specified the **SimpleIoc** container from [MVVM Light Toolkit](http://www.mvvmlight.net/) will be used. We are recommending to use [Register-Resolve-Release](http://blog.ploeh.dk/2010/09/29/TheRegisterResolveReleasepattern/) (RRR or 3R) pattern with single [Composition root](http://blog.ploeh.dk/2011/07/28/CompositionRoot/). Our IoC container is simplified to very thin facility to register and receive singleton based on type or string key. Anything else shouldn't be needed and if so then a new factory or provider should be created and register with the container.

The main concept is about **services**, which should be registered in service provider and for less coupling, they can communicate through a shared **message bus**. The services itself and their message bus can be accessible via facade **ServiceProvider**. Respectively each layer is realised by own provider with main object type, e.g. the view model layer contains many top level **ViewModels** and **ViewModelProvider**.

With a problem of *data caching* or *in-memory storage* can help the concept of **stores**. A store can cache, handle *life-time* and *destruction of stored objects*. Stores are designed to manage objects which are necessary during runtime. Communication between them and a layer of persistent storage should be realised by service(s).

For better code management and low coupling, we are recommending to use [strategy pattern](https://en.wikipedia.org/wiki/Strategy_pattern) as much as possible. We are even splitting this pattern into more detailed cases. The first one we called same like pattern itself, therefore **strategy** and should be used as *a pure unit of work*, all calls of *an executive method* are independent, when *an executive method* is called again the next run can't be affected by previous one(s). The Second case is when you need behaviour with state or any type of longer lifetime, but only per one instance of an object, in this case we call it **manager**. The *singleton* strategy (stateful or stateless) which can be used in many places in the system is realised by already spoken **service** concept.

> We are working on concepts of generic **Behaviours** and **States** of any object but mainly used for UI components.

## Recommendations and guidelines

With the architecture pattern and the library, we are coming with recommendations, as well. These guidelines can be followed or not. They can help with better and cleaner application design and code base.

> We will add more recommendations and specify *code standards* with *naming convention* during the library growth.

* Each class should have **one and only one responsibility**.
* Every public type which can be used or extended outside a namespace needs to **be realised and referenced as an interface** instead of just as a class.
* Strategy pattern should be used **as a pure unit of work**, in other cases, we are speaking about a Manager or a Service.
* If a behaviour / responsibility of an object **needs longer lifetime or a state**, then should be designed as **a Manager**.
* When **a shared behaviour / responsibility** is needed, then **a Service** should be created.
* If a Provider contains a message bus, then the bus should be **used only by objects which provider provides**.

> Our codding bible is [Clean Code](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882) and we follow [SOLID](https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)) principle.

## Dependencies

Dependency | Description
--- | ---
[MVVM Light Toolkit](http://www.mvvmlight.net/) | The library takes a lot of inspiration from this great and powerful toolkit. *We would like to integrate engaged subset deeply to the library, to be able to do this everything is already abstracted.*
[Json.NET](https://www.newtonsoft.com/json) | The JSON is the most popular data transfer format used on the network and for communication between systems.

## Service list

>* A service written in **Bold** is already done.
>* A service written in *Italic* will be implemented.

* *Accounts* - Service for authenticating users. Support for *OAuth protocols* and third-party services, e.g. *Facebook*, *Google* and *Microsoft* account. *Implementation is planned for next phase.*
* **Device**
  * **Connectivity** - Network connectivity information, e.g. if a connection is available.
  * *CurrentPosition* - This service simplifies access to the current location of the device to one method. Combination of in-memory cache storage, permissions service and KeyValues persistent storage service. *Implementation is in progress.*
  * **Geolocator** - Get GPS location of a device.
  * *Sensors and Motion* - Provides access to Accelerometer, Gyroscope, Magnetometer, and Compass.
* **Dialog** - The service for showing dialogues and toasts.
* **Localisation** *(Xamarin specific)* - Retrieving and setting culture to an application.
* **Logging**
  * **Debug** - The simple logging service which uses the *System.Diagnostics.Debug* class.
  * *NLog* - More useful logging service wrapping NLog. *Implementation is planned for next phase.*
* **Navigation** - Manages application navigation between pages or screens.
* **Network**
  * **Http** - The service for sending HTTP requests.
  * **Rest** - The generic service for sending and processing REST requests.
  * *Transfer* - Download or upload files in the background. *Implementation is planned for next phase.*
* *Notifications*
  * *We are planning to integrate a system, local and push notifications into the library.*
* **Permissions** - Manage permission on any platform.
* *Progress* - Generic way how to show progress of any background operation on UI. *Implementation is planned for next phase.*
* **Reflection** - Simple service for runtime object creation (object/type instantiation).
* **Serialisation**
  * *XML* - For serialisation and deserialisation of an object from XML. *Implementation is scheduled, the default serialisation concept of .NET will be used.*
  * **JSON** - For serialisation and deserialisation of objects from JSON.
* **Storage** (Persistent)
  * *Data* - Universal persistent data storage service. The service interface supports transition and all basic CRUD operations. All operations can be used synchronously or asynchronously.
    * *Entity Framework (Core)* - *In development*
  * **JSON** - Universal persistent storage service for JSON format. Objects will be automatically serialised / deserialised by JSON serialisation service. *On Xamarin platform JSON objects are stored in the application dictionary.*
  * **KeyValues** - Storage for key / value pairs. *On Xamarin platform the application dictionary is used.*
  * *File* - Universal file storage service. *Implementation is in progress.*

> A number of services will grow, with new versions of the library. *More services = more power :)*

## Integration with Xamarin

> Most of implementation for Xamarin is realised by [Xamarin.Essentials](https://docs.microsoft.com/en-us/xamarin/essentials/) library which is current in preview.

* **Dialog** - Service for showing dialogues. *Partially powered by [Popup Page Plugin for Xamarin](https://github.com/rotorgames/Rg.Plugins.Popup)*.
* **Toast** - Shows toasts. *Spiked from XXX.*
* **Permissions** - Manage permissions on any platform. *This service is realised by [Permissions Plugin for Xamarin](https://github.com/jamesmontemagno/PermissionsPlugin)*.
* **Storage** (Persistent)
  * **Base (KeyValue, JSON)** - Storage for string key/value pairs and JSON objects. *Using build-in [Application.Properties](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.application.properties?view=xamarin-forms)*.

> A number of Xamarin services will grow. If you want to help us, you can create one. It is a great opportunity to learn how to use the library and how it works under the hood.

## Showroom application

> Current showroom application is written with **Xamarin.Forms** UI, but right now is supported only iOS platform.

## Project(s) based on the library

* [Queue Tracker](http://beaversoft.cz/en-US/app/queue-tracker)
  * Observe queues and waiting times for amusement parks, border crossings, authorities and other institutions.
  * *Not fully migrated to the library, yet.*
* Textum.UI
  * Platform independent text base graphic user interface subsystem. Driven by commands and actions.

## Nuget packages

* Framework
  * [BeaverSoft.StrongBeaver.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Xamarin/)
    * Main package with the framework. Contains implementation for *Xamarin* platform and all important services.
  * [BeaverSoft.StrongBeaver.Core](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Core/)
    * Core concepts of the framework and platform independent implementations of base services.
* Services
  * [BeaverSoft.StrongBeaver.Services.Connectivity.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Connectivity.Xamarin/)
    * The service with network connectivity information. *Powered by [Connectivity Plugin for Xamarin](https://github.com/jamesmontemagno/ConnectivityPlugin)*.
  * [BeaverSoft.StrongBeaver.Services.Geolocator.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Geolocator.Xamarin/)
    * Get GPS location of a device. *Realised by [Geolocator Plugin for Xamarin](https://github.com/jamesmontemagno/GeolocatorPlugin)*.
  * [BeaverSoft.StrongBeaver.Services.Permissions.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Permissions.Xamarin/)
    * The service for managing permissions on any platform. *Realised by [Permissions Plugin for Xamarin](https://github.com/jamesmontemagno/PermissionsPlugin)*.
  * [BeaverSoft.StrongBeaver.Services.DataStorage.SQLite.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.DataStorage.SQLite.Xamarin/)
    * Persistent storage base od SQLite, this service is using *[SQLite.Net-PCL library](https://github.com/oysteinkrog/SQLite.Net-PCL)*.
  * *BeaverSoft.StrongBeaver.Services.DataStorage.EFC*
    * Persistent storage service by *[Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore)*. *Development in progress.*  
  * *BeaverSoft.StrongBeaver.Services.DataStorage.Embedded.Xamarin*
    * Unpack embedded resource cross-platform. *Realised by [EmbeddedResource Plugin for Xamarin](https://github.com/JosephHill/EmbeddedResourcePlugin)*. *Development in progress.*

### Package versions

Nuget package | version
--- | ---
[BeaverSoft.StrongBeaver.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Xamarin/) | 0.9.0-alfa
[BeaverSoft.StrongBeaver.Core](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Core/) |  0.9.0-alfa
[BeaverSoft.StrongBeaver.Services.Connectivity.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Connectivity.Xamarin/) | 0.9.0-alfa
[BeaverSoft.StrongBeaver.Services.Geolocator.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Geolocator.Xamarin/) | 0.9.0-alfa
[BeaverSoft.StrongBeaver.Services.Permissions.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.Permissions.Xamarin/) | 0.9.0-alfa
[BeaverSoft.StrongBeaver.Services.DataStorage.SQLite.Xamarin](https://www.nuget.org/packages/BeaverSoft.StrongBeaver.Services.DataStorage.SQLite.Xamarin/) | 0.9.0-alfa
BeaverSoft.StrongBeaver.Services.DataStorage.EFC | -
BeaverSoft.StrongBeaver.Services.DataStorage.Embedded.Xamarin | -

## Examples

Here is some useful code by examples.

> As a detailed example can be used the *Showroom* application which is part of the framework solution.

### Create Activator

This example shows how to initialise Xamarin application with the *StrongBeaver* library.

#### Shared Activator static class

```cs
namespace MyNamespace
{
  public static class Activator
  {
    public static void Initialise(ISimpleIoc container)
    {
      // Do your specific initialisation here
    }

    public static void InitialiseIoc(ISimpleIoc container)
    {
      // Register services and stores
      // Add your own types to IoC container
    }
  }
}
```

#### iOS app intialisation

```cs
public static class Activator
{
  public static void Initialise(UIApplication application)
  {
    ISimpleIoc serviceLocator = SimpleIoc.Default;

    InitializeIoc(serviceLocator);

    DispatcherHelper.Initialize(application);

    Core.Activator.Initialise(serviceLocator);
    MyNamespace.Activator.Initialise(serviceLocator);
  }

  private static void InitializeIoc(ISimpleIoc container)
  {
    Core.IosActivator.InitialiseIoc(container);
    MyNamespace.Activator.InitialiseIoc(container);

    InitialisePlatform(container);
    InitialiseServices(container);
  }

  private static void InitialisePlatform(ISimpleIoc container)
  {
    container.Register<IApplicationInfo, MyIosApplicationInfo>();
  }

  private static void InitialiseServices(ISimpleIoc container)
  {
    // Register iOS specific services
  }
}

public class MyIosApplicationInfo : BaseIosApplicationInfo
{
  public override bool IsBackgroundTask => false;

  public override string Name => "MyApplication";

  public override Guid Identifier => new Guid("42a4e765-8e57-4031-849b-9f726a57559b");

  public override string Secret => "d93e3af0-a36b-44b3-aabb-3e43719a6e50";
}
```

#### Android app initialisation

> TODO: Add Android initialisation example.

### Create Service

```cs
public class NewFirstService : BaseService
{
  // Implement your new service

  protected override void OnDispose(bool disposing)
  {
    // Implement overrided method or delete
  }
}

// OR

public class NewSecondService : IService
{
  // Implement your new service

  public void Dispose()
  {
    // Implement Dispose pattern by your own
  }
}
```

### Create Service with ability to use message bus

```cs
public interface INewServiceMessage : IServiceMessage
{
  // Specify the members
  void PerformMessage(INewMessageBusService service);
}

public interface INewMessageBusService : IMessageBusService<INewServiceMessage>
{
  // Implement your service interface
}

public class NewMessageBusService : BaseService, INewMessageBusService
{
  // Implement your service

  public void ProcessMessage(INewServiceMessage message)
  {
    message.PerformMessage(this);
  }
}
```

### Create Store

```cs
public class NewItem
{
  public int Id { get; }
}

public class NewStore : SimpleStore<int, NewItem>
{
  // Create a new store class only if you need some extra logic
  // for it or simply use SimpleStore class directly

  public NewStore()
    : base((item) => item.Id) // Item key factory
  {
    // No operation
  }
}
```

### Create Store for View Model

The store for model or view model layer can be totally same, but we're expecting that for view model would be nice to handle a problem of *object lifetime*, *initialisation*, and *auto updates*, when changes in a model object have been received.

> A Lifetime of an object is managed by **ILifetimeManager**, the system contains one *manual* and *auto (reference counting)* lifetime manager.

For this reasons, the framework contains more complex store named **ComplexStore** for any items which implement **IComplexStoreItem** interface.

```cs
public class NewViewModelStore : ComplexStore<int, NewObservableItem>
{
  // Create a new store class isn't necessary

  public NewViewModelStore()
    : base((item) => item.Id) // Factory for item key
  {
    // No operation
  }
}

public class NewObservableItem : ObservableObject, IComplexStoreItem<NewObservableItem>
{
  // The base class ObservableObject is only recomendation for databinding,
  // mandatory is only IComplexStoreItem interface

  // Item key property
  public int Id { get; set; }

  public void Update(NewObservableItem newItem)
  {
    // Update item with new content
  }

  public void Initialise()
  {
    // Implement item initialisation
    // (calculate view properties)
  }

  public void Dispose()
  {
    // Dospose item
  }
}
```

### Create ViewModelLocator

This example will create new *ViewModelLocator* for easier referencing of view models in **Xaml**.

#### View Model Provider

```cs
public interface IViewModelLocator : IBaseViewModelLocator
{
  INewViewModel NewViewModel { get; }
}

public class ViewModelLocator : BaseViewModelLocator, IViewModelLocator
{
  public ViewModelLocator()
    : base(ServiceLocator.Current.GetInstance<IPlatformInfo>())
  {
    if (BaseViewModel.IsInDesignModeStatic)
    {
      // Or rather do it through IoC
      NewViewModel = new NewViewModelDesignMock();
    }
    else
    {
      NewViewModel = ServiceLocator.GetInstance<INewViewModel>();
    }
  }

  public static IViewModelLocator Current { get; private set; }

  public INewViewModel NewViewModel { get; }

  public static void SetCurrentLocator(IViewModelLocator newLocator)
  {
    Current = newLocator;
  }
}
```

#### View Model

```cs
public interface INewViewModel : IViewModel
{
  string Text { get; }
}

public class NewViewModel : BaseViewModel, INewViewModel
{
  // Implementation of the view model
}

public class NewViewModelDesignMock : BaseViewModel, INewViewModel
{
  public string Text => "Lorem ipsum";
}
```

#### App.xaml

```xml
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModel="clr-namespace:StrongBeaver.Showroom.ViewModel"
             x:Class="StrongBeaver.Showroom.App">
    <Application.Resources>
        <ResourceDictionary>
            <viewModel:ViewModelLocator x:Name="ViewModelLocator" />
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

#### App.cs

```cs
public partial class App : Application 
{
  private const string VIEW_MODEL_LOCATOR_RESOURCE_KEY = "ViewModelLocator";

  public App()
  {
    InitializeComponent();
    SetViewModelProvider();
    // ...
  }

  private static IViewModelLocator ViewModelLocator { get; private set; }

  private void SetViewModelProvider()
  {
    ViewModelLocator = (IViewModelLocator)Resources[VIEW_MODEL_LOCATOR_RESOURCE_KEY];
    SimpleIoc.Default.Register<IViewModelLocator>(() => { return ViewModelLocator; });
    ViewModel.ViewModelLocator.SetCurrentProvider(ViewModelLocator);
  }
}
```

### Create own Provider

Sometimes can be handy to create your own layer or type Provider (Facade) which can contain for example static reference to the objects inside.

####  Own Service Provider

```cs
public interface IOwnServiceProvider : IServiceProvider
{
  INavigationService Navigation { get; }

  IDialogService Dialog { get; }

  IPermissionsService Permissions { get; }
}

public class OwnServiceProvider : ServiceProvider, IOwnServiceProvider
{
  public OwnServiceProvider(ISimpleIoc container, ILogService logService)
    : base(container, logService)
  {
    Navigation = container.GetInstance<INavigationService>();
    Dialog = container.GetInstance<IDialogService>();
    Permissions = container.GetInstance<IPermissionsService>();
  }

  public INavigationService Navigation { get; }

  public IDialogService Dialog { get; }

  public IPermissionsService Permissions { get; }

  public new static IOwnServiceProvider Current { get; private set; }

  public static void SetDefaultProvider()
  {
    SetProvider(ServiceLocator.Current.GetInstance<IOwnServiceProvider>());
  }

  public static void SetProvider(IOwnServiceProvider newProvider)
  {
    Current = newProvider;
  }
}
```

#### Action Provider (pureMVC)

```cs
public interface IAction
{
    // Implementation of action interface
}

public class ActionProvider : IProvider<IAction>
{
  private SimpleIoc container;

  public ActionProvider(SimpleIoc container)
  {
      this.container = container;
  }

  public TInterface Get<TInterface>()
    where TInterface : IAction
  {
    // Always create a new instance of an action (command)
    return container.GetInstanceWithoutCaching<TInterface>();
  }

  public TInterface Get<TInterface>(string key)
    where TInterface : IAction
  {
    // Always create a new instance of an action (command)
    return container.GetInstanceWithoutCaching<TInterface>(key);
  }

  public void Register<TInterface, TClass>()
    where TInterface : class, IAction
    where TClass : class, TInterface
  {
    container.Register<TInterface, TClass>();
  }

  public void Register<TInterface>(Func<TInterface> factory, string key)
    where TInterface : class, IAction
  {
    container.Register<TInterface>(factory, key);
  }

  public void Unregister<TInterface>()
    where TInterface : class, IAction
  {
    container.Unregister<TInterface>();
  }

  public void Unregister<TInterface>(TInterface item)
    where TInterface : class, IAction
  {
    container.Unregister<TInterface>(item);
  }

  public void Unregister<TInterface>(string key)
    where TInterface : class, IAction
  {
    container.Unregister<TInterface>(key);
  }
}
```

## Thanks

We gladly thank designers, developers, and co-workers of:

* [.NET](https://www.microsoft.com/net/) developer platform
* [Xamarin](https://www.xamarin.com/) and [Plugins of Xamarin](https://github.com/xamarin/XamarinComponents)
* [MVVM Light Toolkit](http://www.mvvmlight.net/)
* [Json.NET](https://www.newtonsoft.com/json)
* [The PureMVC Framework](http://puremvc.org/)

## Contact

![BeaverSoft logo](doc/design/logo-beaversoft-border.png)

<address>
  <a href="http://www.beaversoft.cz/" target="blank">Beaver soft</a><br/>
  <strong>Aleš Kobr</strong><br/>
  <br/>
  <a href="mailto:kobr.ales@hotmail.cz" class="mt-small">kobr.ales@hotmail.cz</a>
  <br/>
  <a href="tel:+420724979498">+420 724 97 94 98</a>
  <br/>
  <a href="tel:+447448503302">+44 744 850 33 02</a>
</address>

## Licence and Notes

Created by [BeaverSoft](http://www.beaversoft.cz/).

Entire framework is under [MIT licence](https://github.com/akobr/strong-beaver/blob/master/LICENSE).

The strong beaver image is <a href="http://www.freepik.com">designed by Ajipebriana / Freepik</a>.
