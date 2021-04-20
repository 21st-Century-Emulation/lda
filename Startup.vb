Imports Microsoft.AspNetCore
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Routing
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection

Public Class Startup

    Sub ConfigureServices(services as IServiceCollection)
        services.AddControllers()
        services.AddHttpClient()
        services.AddHealthChecks()
    End Sub

    Sub Configure(app as IApplicationBuilder)
        app.UseRouting()

        app.UseEndpoints(Function(endpoints as IEndpointRouteBuilder)
          endpoints.MapHealthChecks("/status")
          endpoints.MapControllers()
          Return endpoints
        End Function)
    End Sub
End Class