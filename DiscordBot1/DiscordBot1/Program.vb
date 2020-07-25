Imports System
Imports System.IO
Imports Discord
Imports Discord.Net.WebSockets
Imports Discord.WebSocket
Imports Discord.Net
Imports System.Reflection.Metadata.Ecma335

'Activation
'https://discordpy.readthedocs.io/en/latest/discord.html

Module Program

    Dim DiscordSocketClient As DiscordSocketClient

    Sub Main(args As String())
        DiscordSocketClient = New DiscordSocketClient(New DiscordSocketConfig With {
                                                       .LogLevel = LogSeverity.Verbose
                                                      })
        AddHandler DiscordSocketClient.LoggedOut, AddressOf DiscordSocketClient_LoggedOut
        AddHandler DiscordSocketClient.Log, AddressOf DiscordSocketClient_Log
        AddHandler DiscordSocketClient.Ready, AddressOf DiscordSocketClient_Ready
        AddHandler DiscordSocketClient.MessageReceived, AddressOf DiscordSocketClient_MessageReceived

        RunBot().GetAwaiter().GetResult()
    End Sub

    Async Function RunBot() As Task
        Await DiscordSocketClient.LoginAsync(TokenType.Bot, "xxxxxxxxxxxxxxxxxxxxxxxx.yyyyyy.zzzzzzzzzzzzzzzzzzzzzzzzzzz")
        Console.WriteLine($"{DiscordSocketClient.LoginState}")
        Await DiscordSocketClient.StartAsync()
        Console.WriteLine($"{DiscordSocketClient.Status}")
        Await Task.Delay(-1)
    End Function

    Private Async Function DiscordSocketClient_MessageReceived(arg As SocketMessage) As Task
        Console.WriteLine($"{arg.Author.Username}:{arg.Author.Id} {arg.Content}")
        Dim Msg = New EmbedBuilder()
        Msg.AddField("1", arg.Content, True)
        'ignore 50007 message
        Await arg.Author.SendMessageAsync("echo", False, Msg.Build)
    End Function

    Private Function DiscordSocketClient_Ready() As Task
        Console.WriteLine("Ready")
        Return Task.CompletedTask
    End Function

    Private Function DiscordSocketClient_Log(arg As LogMessage) As Task
        Console.WriteLine(arg.Message)
        Return Task.CompletedTask
    End Function

    Private Function DiscordSocketClient_LoggedOut() As Task
        Console.WriteLine("LoggedOut")
        Return Task.CompletedTask
    End Function

End Module
