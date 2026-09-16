Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1

    <DllImport("user32.dll")>
    Private Shared Function RegisterHotKey(hWnd As IntPtr,
                                         id As Integer,
                                         fsModifiers As UInteger,
                                         vk As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnregisterHotKey(hWnd As IntPtr,
                                             id As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr,
                                       nCmdShow As Integer) As Boolean
    End Function

    Private Const HOTKEY_ID As Integer = 1
    Private Const WM_HOTKEY As Integer = &H312

    Private Const MOD_ALT As Integer = &H1
    Private Const MOD_CONTROL As Integer = &H2

    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

    Private ClientHidden As Boolean = False
    Private IsReconnecting As Boolean = False ' Blocks screenshot connections during IP switch

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function LockWorkStation() As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetCursorPos(x As Integer, y As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Sub mouse_event(dwFlags As Integer,
                                  dx As Integer,
                                  dy As Integer,
                                  dwData As Integer,
                                  dwExtraInfo As Integer)
    End Sub

    <DllImport("user32.dll")>
    Private Shared Sub keybd_event(
        bVk As Byte,
        bScan As Byte,
        dwFlags As Integer,
        dwExtraInfo As Integer)
    End Sub

    Private Const KEYEVENTF_KEYUP As Integer = &H2

    Private Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Private Const MOUSEEVENTF_LEFTUP As Integer = &H4
    Private Const MOUSEEVENTF_RIGHTDOWN As Integer = &H8
    Private Const MOUSEEVENTF_RIGHTUP As Integer = &H10

    Const ConnectionPort As Int16 = 9876 ' Connection Port Number
    Const RequestPort As Int16 = 6789 ' Request Port Number
    Const ControlPort As Int16 = 6790

    Dim ServerIp As String

    Dim NetStream As NetworkStream
    Dim NetStream2 As NetworkStream
    Dim myReader As BinaryReader
    Dim myWriter As BinaryWriter

    Dim Look4Request As Thread = Nothing
    Dim Look4MsgRequest As Thread = Nothing

    Dim infiniteCounter As Integer
    Dim readData As String

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.NotifyIcon1.Visible = False
        Me.NotifyIcon1.Dispose()
        System.Environment.Exit(System.Environment.ExitCode) ' Informs to the Server When it is Closed
    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        ' Read the Server IP from serverip.txt
        Dim ipFile As String = Path.Combine(Application.StartupPath, "serverip.txt")

        If Not File.Exists(ipFile) Then
            MsgBox("serverip.txt was not found.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End
        End If

        ServerIp = File.ReadAllText(ipFile).Trim()

        If ServerIp = "" Then
            MsgBox("Server IP address is empty.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End
        End If

        ' Informs the Image Receiver That it is Connected
        Try
            Dim myClient As New TcpClient
            myClient.Connect(ServerIp, ConnectionPort)

            Dim writer As New BinaryWriter(myClient.GetStream())
            writer.Write("CONNECT")
            writer.Flush()

            myClient.Close()

        Catch ex As Exception
            MsgBox("Please Start the Receiver", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)

            Me.NotifyIcon1.Dispose()
            End

        End Try

        ' Creates a Thread To Listen For the Request of the Receiver
        Look4Request = New Thread(New ThreadStart(AddressOf WaitForRequest))
        Look4Request.Start()

        ' Creates a Thread To Listen For Messages
        Look4MsgRequest = New Thread(New ThreadStart(AddressOf ReceiveMsg))
        Look4MsgRequest.Start()

        ' Creates the Control Thread
        Dim ControlThread As New Thread(AddressOf ReceiveControl)
        ControlThread.IsBackground = True
        ControlThread.Start()

    End Sub

    Sub ReceiveControl()

    End Sub

    Sub WaitForRequest()

        Dim listener As New TcpListener(IPAddress.Any, RequestPort)
        listener.Start()

        Try

            While True

                Dim client As TcpClient = listener.AcceptTcpClient()

                ' Reject the connection while switching server IP
                ' This keeps the old server's card RED during the reconnect window
                If IsReconnecting Then
                    client.Close()
                    Continue While
                End If

                Try

                    NetStream = client.GetStream()

                    While client.Connected

                        Try

                            Send_Screen_Shot()

                        Catch ex As IOException
                            Exit While

                        Catch ex As SocketException
                            Exit While

                        Catch
                            Exit While

                        End Try

                        Thread.Sleep(100)

                    End While

                Finally

                    If NetStream IsNot Nothing Then
                        NetStream.Dispose()
                        NetStream = Nothing
                    End If

                    client.Close()

                End Try

            End While

        Catch ex As Exception

        Finally

            listener.Stop()

        End Try

    End Sub

    Sub Send_Screen_Shot()

        Try

            ' Capture the latest screen
            ScreenCapture.CurrentScreen()

            Dim bmp As Bitmap = ScreenCapture.oBitMap

            If bmp Is Nothing Then Exit Sub

            ' Resize to 1280x720
            Dim resized As New Bitmap(1280, 720)

            Using g As Graphics = Graphics.FromImage(resized)
                g.DrawImage(bmp, 0, 0, 1280, 720)
            End Using

            Using ms As New MemoryStream()

                resized.Save(ms, Imaging.ImageFormat.Jpeg)

                Dim imgBytes As Byte() = ms.ToArray()

                Dim writer As New BinaryWriter(NetStream)

                writer.Write(imgBytes.Length)
                writer.Write(imgBytes)
                writer.Flush()

            End Using

            resized.Dispose()

        Catch ex As IOException
            Exit Sub

        Catch ex As SocketException
            Exit Sub

        Catch ex As Exception
            Exit Sub

        End Try

    End Sub

    Sub ReceiveMsg()
        ' ALWAYS CHANGE IP TO CLIENT 192.168.100.47
        Dim clsEndpoint As IPEndPoint = New IPEndPoint(New IPAddress(New Byte() {192, 168, 100, 47}), 1977)
        Dim clsServerSocket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ' bind to the IP address and port - this blocks other listeners from using the same combination...   
        clsServerSocket.Bind(clsEndpoint)
        ' listen for incoming connections...   
        clsServerSocket.Listen(CInt(Fix(SocketOptionName.MaxConnections)))
        Do
            ' poll for connections - this allows us to keep listening so the thread doesn't block...   
            If clsServerSocket.Poll(10000, SelectMode.SelectRead) Then
                ' a message is being sent - create a socket to read it...   
                Dim clsSocket As Socket = clsServerSocket.Accept()
                ' read the message from the buffer...   
                Dim bReadBuffer As Byte() = New Byte(255) {}
                clsSocket.Receive(bReadBuffer, bReadBuffer.Length, SocketFlags.None)
                ' convert to a string...   
                Dim sMessage As String =
                       Encoding.ASCII.GetString(bReadBuffer).Trim(Chr(0))

                Dim parts() As String = sMessage.Split("|"c)

                Select Case parts(0).ToUpper()

                    Case "SHUTDOWN"

                        Process.Start("shutdown", "/s /t 0")

                    Case "RESTART"

                        Process.Start("shutdown", "/r /t 0")

                    Case "LOCK"

                        LockWorkStation()

                    Case "CALCULATOR"

                        Process.Start("calc.exe")

                    Case "BLACKSCREEN"

                        Me.BeginInvoke(Sub()
                                           Dim alreadyOpen As Boolean = False

                                           For Each f As Form In Application.OpenForms
                                               If TypeOf f Is BlackScreen Then
                                                   alreadyOpen = True
                                                   Exit For
                                               End If
                                           Next

                                           If Not alreadyOpen Then
                                               Dim frm As New BlackScreen
                                               frm.Show()
                                           End If
                                       End Sub)

                    Case "UNBLACKSCREEN"

                        Me.BeginInvoke(Sub()
                                           For Each f As Form In Application.OpenForms
                                               If TypeOf f Is BlackScreen Then
                                                   f.Close()
                                                   Exit For
                                               End If
                                           Next
                                       End Sub)

                    Case "MOUSEMOVE"

                        If parts.Length >= 3 Then

                            Dim x As Integer = Integer.Parse(parts(1))
                            Dim y As Integer = Integer.Parse(parts(2))

                            SetCursorPos(x, y)

                        End If

                    Case "LEFTDOWN"

                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)

                    Case "LEFTUP"

                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)

                    Case "RIGHTDOWN"

                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)

                    Case "RIGHTUP"

                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)

                    Case "KEYDOWN"

                        If parts.Length >= 2 Then

                            Dim vk As Byte = Byte.Parse(parts(1))

                            keybd_event(vk, 0, 0, 0)

                        End If

                    Case "KEYUP"

                        If parts.Length >= 2 Then

                            Dim vk As Byte = Byte.Parse(parts(1))

                            keybd_event(vk, 0, KEYEVENTF_KEYUP, 0)

                        End If

                    Case "STARTBROADCAST"

                        Me.BeginInvoke(Sub()
                                           Dim frm As New TeacherBroadcast
                                           frm.Show()
                                       End Sub)

                    Case "STOPBROADCAST"

                        Me.BeginInvoke(Sub()
                                           For Each frm As Form In Application.OpenForms
                                               If TypeOf frm Is TeacherBroadcast Then
                                                   frm.Close()
                                                   Exit For
                                               End If
                                           Next
                                       End Sub)

                    Case Else

                        MsgBox(sMessage, MsgBoxStyle.Information, "Incoming Message")

                End Select
                clsSocket.Close()
                clsSocket = Nothing
            End If
        Loop
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        RegisterHotKey(Me.Handle,
               HOTKEY_ID,
               MOD_CONTROL,
               Keys.H)
    End Sub

    ' -------------------------------------------------------
    ' Shows a custom IP editor dialog with Edit & Save buttons
    ' -------------------------------------------------------
    Private Sub ShowIPEditor()

        ' --- Build the dialog form programmatically ---
        Dim dlg As New Form()
        dlg.Text = "Image Receiver IP Address"
        dlg.Size = New Size(360, 185)
        dlg.StartPosition = FormStartPosition.CenterScreen
        dlg.FormBorderStyle = FormBorderStyle.FixedDialog
        dlg.MaximizeBox = False
        dlg.MinimizeBox = False

        ' Label
        Dim lbl As New Label()
        lbl.Text = "IP Address:"
        lbl.Location = New Point(16, 16)
        lbl.AutoSize = True
        lbl.Font = New Font(lbl.Font, FontStyle.Bold)

        ' TextBox — starts as read-only (display mode)
        Dim txtIP As New TextBox()
        txtIP.Text = ServerIp
        txtIP.Location = New Point(16, 40)
        txtIP.Width = 308
        txtIP.ReadOnly = True
        txtIP.BackColor = Color.FromArgb(230, 230, 230)
        txtIP.Font = New Font("Consolas", 10)

        ' Status label
        Dim lblStatus As New Label()
        lblStatus.Text = "Press Edit to modify the IP address."
        lblStatus.Location = New Point(16, 70)
        lblStatus.AutoSize = True
        lblStatus.ForeColor = Color.Gray

        ' Edit button
        Dim btnEdit As New Button()
        btnEdit.Text = "Edit"
        btnEdit.Location = New Point(16, 100)
        btnEdit.Size = New Size(90, 30)

        ' Save button (disabled until Edit is clicked)
        Dim btnSave As New Button()
        btnSave.Text = "Save"
        btnSave.Location = New Point(120, 100)
        btnSave.Size = New Size(90, 30)
        btnSave.Enabled = False
        btnSave.BackColor = Color.FromArgb(0, 120, 215)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat

        ' Cancel button
        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.Location = New Point(234, 100)
        btnCancel.Size = New Size(90, 30)

        ' --- Wire up Edit button ---
        AddHandler btnEdit.Click, Sub(s, ev)
                                      txtIP.ReadOnly = False
                                      txtIP.BackColor = Color.White
                                      txtIP.Focus()
                                      txtIP.SelectAll()
                                      btnSave.Enabled = True
                                      lblStatus.Text = "Edit the IP address, then click Save."
                                      lblStatus.ForeColor = Color.DarkBlue
                                  End Sub

        ' --- Wire up Save button ---
        AddHandler btnSave.Click, Sub(s, ev)
                                      Dim newIP As String = txtIP.Text.Trim()

                                      If newIP = "" Then
                                          MsgBox("IP Address cannot be empty.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Validation")
                                          txtIP.Focus()
                                          Return
                                      End If

                                      ' Capture old IP before overwriting
                                      Dim oldIp As String = ServerIp

                                      ' Save new IP to memory
                                      ServerIp = newIP

                                      ' Save to bin\Debug\serverip.txt (runtime path)
                                      Dim ipFile As String = Path.Combine(Application.StartupPath, "serverip.txt")
                                      File.WriteAllText(ipFile, ServerIp)

                                      ' Also save to project root serverip.txt (two levels up from bin\Debug)
                                      Try
                                          Dim projectFile As String = Path.GetFullPath(
                                              Path.Combine(Application.StartupPath, "..\..\serverip.txt"))
                                          If File.Exists(projectFile) Then
                                              File.WriteAllText(projectFile, ServerIp)
                                          End If
                                      Catch
                                          ' Silently ignore if project root file is not accessible
                                      End Try

                                      lblStatus.Text = "Saved! Reconnecting to " & ServerIp & "..."
                                      lblStatus.ForeColor = Color.DarkOrange

                                      ' Lock the field and disable buttons while reconnecting
                                      txtIP.ReadOnly = True
                                      txtIP.BackColor = Color.FromArgb(230, 230, 230)
                                      btnSave.Enabled = False
                                      btnEdit.Enabled = False
                                      btnCancel.Enabled = False

                                      ' Reconnect on a background thread so the UI does not freeze
                                      Dim reconnectThread As New Thread(
                                          Sub()
                                              ' Step 1: Block new screenshot connections so the old server cannot immediately reconnect and stays red
                                              IsReconnecting = True

                                              ' Close the active screenshot stream — server detects Offline immediately
                                              Try
                                                  If NetStream IsNot Nothing Then
                                                      NetStream.Close()
                                                      NetStream = Nothing
                                                  End If
                                              Catch
                                              End Try

                                              ' Send DISCONNECT signal to old server
                                              Try
                                                  Dim oldClient As New TcpClient()
                                                  oldClient.Connect(oldIp, ConnectionPort)
                                                  Dim oldWriter As New BinaryWriter(oldClient.GetStream())
                                                  oldWriter.Write("DISCONNECT")
                                                  oldWriter.Flush()
                                                  oldClient.Close()
                                              Catch
                                                  ' Old server may already be unreachable
                                              End Try

                                              Thread.Sleep(600) ' Hold red state so it is clearly visible on the server card

                                              ' Step 2: Allow screenshot connections again, then connect to new server
                                              IsReconnecting = False

                                              Try
                                                  Dim newClient As New TcpClient()
                                                  newClient.Connect(ServerIp, ConnectionPort)
                                                  Dim newWriter As New BinaryWriter(newClient.GetStream())
                                                  newWriter.Write("CONNECT")
                                                  newWriter.Flush()
                                                  newClient.Close()

                                              Catch ex As Exception
                                                  IsReconnecting = False ' Always re-enable connections on failure
                                                  Me.BeginInvoke(Sub()
                                                                     MsgBox("Could not connect to: " & ServerIp & vbCrLf & ex.Message,
                                                                            MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Connection Failed")
                                                                 End Sub)
                                              End Try
                                          End Sub)

                                      reconnectThread.IsBackground = True
                                      reconnectThread.Start()

                                      ' Close the dialog immediately so the user can press Ctrl+H again
                                      dlg.Close()
                                  End Sub

        ' --- Wire up Cancel button ---
        AddHandler btnCancel.Click, Sub(s, ev)
                                        dlg.Close()
                                    End Sub

        ' Add controls and show
        dlg.Controls.AddRange(New Control() {lbl, txtIP, lblStatus, btnEdit, btnSave, btnCancel})
        dlg.ShowDialog(Me)

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = WM_HOTKEY AndAlso
       m.WParam.ToInt32() = HOTKEY_ID Then

            ' Open the custom IP editor dialog instead of InputBox
            ShowIPEditor()

        End If

        MyBase.WndProc(m)

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        UnregisterHotKey(Me.Handle, HOTKEY_ID)

        Try
            Dim client As New TcpClient()
            client.Connect(ServerIp, ConnectionPort)

            Dim writer As New BinaryWriter(client.GetStream())
            writer.Write("DISCONNECT")
            writer.Flush()

            client.Close()

        Catch
            ' Ignore if server is already closed
        End Try

    End Sub

    Private Sub NotifyIcon2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon2.MouseDoubleClick

        If ClientHidden Then

            ShowWindow(Me.Handle, SW_SHOW)

            Me.WindowState = FormWindowState.Normal

            Me.BringToFront()

            Me.Activate()

            ClientHidden = False

        End If
    End Sub
End Class