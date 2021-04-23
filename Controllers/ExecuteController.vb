Imports System.Net.Http
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Options

Public Class ExecuteController
    Inherits ControllerBase

    Private Readonly mClientFactory As IHttpClientFactory

    Private Readonly mReadMemoryUrl As String

    Public Sub New(httpClientFactory As IHttpClientFactory)
        mClientFactory = httpClientFactory
        mReadMemoryUrl = Environment.GetEnvironmentVariable("READ_MEMORY_API")
    End Sub
    
    <Route("/api/v1/execute")>
    <HttpPost()>
    Public Async Function Execute(operand1 As Byte, operand2 As Byte, <FromBody()> cpu As Cpu) As Task(Of Cpu)
        Dim byteArray = New Byte() { operand1, operand2 }
        Dim address = BitConverter.ToUInt16(byteArray, 0)
        Console.WriteLine(address)
        Dim client = mClientFactory.CreateClient()
        Dim result = await client.GetAsync($"{mReadMemoryUrl}?address={address}")
        cpu.State.A = Byte.Parse(await result.Content.ReadAsStringAsync())
        cpu.State.Cycles = cpu.State.Cycles + 13
        Return cpu
    End Function

#if DEBUG
    <Route("/api/v1/readMemory")>
    <HttpGet()>
    Public Function ReadMemory(address As UShort) As Byte
        return 10
    End Function
#End If

End Class