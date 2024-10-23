This ASP.NET Core WebAPI application includes the following:

1.  The extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions() has been created to scan the provided assembiles to register all classes that have matching interfaces, following a naming convention (e.g. IClassName for ClassName), adds them to the dependency injection container with a transient lifetime - a new instance of the calls is created each time it is requested.

2.  This extension method is called from the Program.cs static main method, which passes to it the application assembiles to scan and automatically register services in the dependency injection container.

3.  Added controller to ensure that enum values are serialized and deserialized as strings instead of their default numeric value.  This allows enum strings to be passed to WebAPI endpoints.  This logic is implemented in the program.cs file.

4.  Added class library that supports OpenAI Chat Completions controller endpoints.  These endpoints are in the OpenAiChatCompletionsController.cs file.

5.  Added AppServiceCore library that includes the various interfaces and DTO models that are used by the application.  It also implements the CommandResult object that is returned by the respective endpoints to the caller.

6.  Added several class libraries that implement quiz questions that I found on various tech interview sites.  Currently, active in adding to these libraries.

7.  Adding NUnit tests for the various class libraries that are included in this application.
