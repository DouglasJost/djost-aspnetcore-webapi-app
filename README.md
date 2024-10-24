The ASP.NET Core WebAPI application that I wrote includes the following:

* Dynamic dependency injection registration.  Dynamic registration of interface and implementation pairs that follow the naming convention (MyClass : IMyClass) with the ASP.NET Core dependency injection container.

  The extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions(this IServiceCollection services) was added to the startup project and is called by the static main function in Program.cs.  This extension method dynamically "scans" the loaded assemblies, excluding System and Microsoft assemblies for the ServiceRegistration.GetServices() method that each application library implements.  GetServices() returns a list of interfaceType/implementationType pairs that match the default naming convention (MyClass : IMyClass) that the class library wants registered with the ASP.NET Core dependency injection (DI) container.  Each pair is then registered as Transient - create new instance every time requested.

  Please reference the extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions(this IServiceCollection services) and comments contain in the method for more details and assumptions that were made in implementing this logic.

* Added support to ensure that enum values are serialized and deserialized as strings instead of their default numeric value when enum are passed to and returned from WebAPI endpoints.  This logic is implemented in the program.cs file.

* Added class library that supports OpenAI Chat Completions controller endpoints.  These endpoints are in the OpenAiChatCompletionsController.cs file.

* Added AppServiceCore library that includes the various interfaces and DTO models that are used by the application.  It also implements the CommandResult object that is returned by the respective endpoints to the caller.

* Added several class libraries that implement quiz questions that I found on various tech interview sites.  Currently, active in adding to these libraries.

* Adding NUnit tests for the various class libraries that are included in this application.
