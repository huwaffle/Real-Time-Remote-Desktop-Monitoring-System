Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Drawing.Imaging

Module BroadcastManager

    Public BroadcastRunning As Boolean = False

    Private listener As TcpListener

    Private clients As New List(Of TcpClient)

    Public BroadcastToAll As Boolean = True

    Public SelectedClientIP As String = ""

    Public Sub StartBroadcast()
        If BroadcastRunning Then Exit Sub

        BroadcastRunning = True

        listener = New TcpListener(IPAddress.Any, 6790)

        listener.Start()

        Dim broadcastThread As New Thread(AddressOf BroadcastScreen)
        broadcastThread.IsBackground = True
        broadcastThread.Start()

        Dim acceptThread As New Thread(AddressOf AcceptClients)
        acceptThread.IsBackground = True
        acceptThread.Start()

    End Sub

    Private Sub AcceptClients()

        While BroadcastRunning

            Try

                Dim c As TcpClient = listener.AcceptTcpClient()

                SyncLock clients

                    If BroadcastToAll Then

                        clients.Add(c)

                    Else

                        Dim ip As String =
        CType(c.Client.RemoteEndPoint, IPEndPoint).Address.ToString()

                        If ip = SelectedClientIP Then

                            clients.Add(c)

                        Else

                            c.Close()

                        End If

                    End If

                End SyncLock

            Catch

            End Try

        End While

    End Sub

    Private Sub BroadcastScreen()

        While BroadcastRunning

            Try

                ScreenCapture.CurrentScreen()

                Dim bmp As Bitmap = ScreenCapture.oBitMap

                If bmp Is Nothing Then Continue While

                Dim resized As New Bitmap(1280, 720)

                Using g As Graphics = Graphics.FromImage(resized)
                    g.DrawImage(bmp, 0, 0, 1280, 720)
                End Using

                Using ms As New MemoryStream()

                    resized.Save(ms, ImageFormat.Jpeg)

                    Dim imgBytes() As Byte = ms.ToArray()

                    Dim clientList As List(Of TcpClient)

                    SyncLock clients
                        clientList = New List(Of TcpClient)(clients)
                    End SyncLock

                    For Each client As TcpClient In clientList

                        Try

                            Dim writer As New BinaryWriter(client.GetStream())

                            writer.Write(imgBytes.Length)
                            writer.Write(imgBytes)
                            writer.Flush()

                        Catch

                            SyncLock clients

                                If clients.Contains(client) Then

                                    client.Close()
                                    clients.Remove(client)

                                End If

                            End SyncLock

                        End Try

                    Next

                End Using

                resized.Dispose()

                Thread.Sleep(50)   '≈20 FPS

            Catch

            End Try

        End While

    End Sub

    Public Sub StopBroadcast()

        BroadcastRunning = False

        Try
            listener.Stop()
        Catch
        End Try

        SyncLock clients

            For Each c As TcpClient In clients

                Try
                    c.Close()
                Catch
                End Try

            Next

            clients.Clear()

        End SyncLock

    End Sub
End Module