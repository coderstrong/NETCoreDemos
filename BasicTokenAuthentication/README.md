# NETCoreDemos
Implement the options class inheriting from AuthenticationSchemeOptions
Create the handler, inherit from AuthenticationHandler<TOptions>
Implement the constructor and HandleAuthenticateAsync in the handler
Use the static methods of AuthenticateResult to create different results (None, Fail or Success)
Override other methods to change standard behaviour
Register the scheme with AddScheme<TOptions, THandler>(string, Action<TOptions>) on the AuthenticationBuilder, which you get by calling AddAuthentication on the service collection


Refer: 
https://joonasw.net/view/creating-auth-scheme-in-aspnet-core-2
https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication?view=aspnetcore-2.1