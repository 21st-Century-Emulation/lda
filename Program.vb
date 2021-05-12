Imports System
Imports Microsoft.AspNetCore
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging

Module Program
    Sub Main(args As String())
        BuildWebHost(args).Run()
    End Sub

    Function BuildWebHost(args As String()) As IWebHost
        Return WebHost.CreateDefaultBuilder(args).UseStartup(Of Startup)().ConfigureLogging(Function (log as ILoggingBuilder)
                log.AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning).AddConsole()
                Return log
            End Function).Build()
    End Function
End Module