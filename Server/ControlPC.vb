Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class ControlPC
    Public Shared ActiveControlForm As ControlPC
    Public ClientIP As String
    Public PCName As String

    Private Const RequestPort As Integer = 6789

    Private clientSocket As TcpClient
    Private screenThread As Thread

    Private Sub ControlPC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Me.Focus()
        PictureBox1.Cursor = Cursors.Cross
        ActiveControlForm = Me

        Me.Text = "Remote Control - " & PCName & " - Connecting..."

        screenThread = New Thread(AddressOf ReceiveScreen)
        screenThread.IsBackground = True
        screenThread.Start()
    End Sub

    Private Sub ReceiveScreen()

        Try

            clientSocket = New TcpClient(ClientIP, RequestPort)

            Using ns As NetworkStream = clientSocket.GetStream()

                Using reader As New BinaryReader(ns)

                    While True

                        Dim length As Integer = reader.ReadInt32()

                        Dim bytes() As Byte = reader.ReadBytes(length)

                        Using ms As New MemoryStream(bytes)

                            Dim bmp As Bitmap =
                                CType(Bitmap.FromStream(ms).Clone(), Bitmap)

                            Me.BeginInvoke(Sub()

                                               If PictureBox1.Image IsNot Nothing Then
                                                   PictureBox1.Image.Dispose()
                                               End If

                                               PictureBox1.Image = bmp

                                               Me.Text = "Remote Control - " & PCName & " - Connected"

                                           End Sub)

                        End Using

                    End While

                End Using

            End Using

        Catch

            Me.BeginInvoke(Sub()
                               Me.Text = "Remote Control - " & PCName & " - Disconnected"
                           End Sub)

        End Try

    End Sub

    Public Sub UpdateImage(img As Image)

        If img Is Nothing Then Exit Sub

        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If

        PictureBox1.Image = CType(img.Clone(), Image)

    End Sub

    Private Sub ControlPC_FormClosing(sender As Object,
                                  e As FormClosingEventArgs) Handles Me.FormClosing

        If ActiveControlForm Is Me Then
            ActiveControlForm = Nothing
        End If

        If MDIParent1.Instance IsNot Nothing Then
            MDIParent1.Instance.AddLog("Ended remote control session with " & PCName & ".")
        End If

    End Sub
    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove

        If e.Button <> MouseButtons.None Then Exit Sub

        Dim remoteX As Integer =
            CInt((e.X / PictureBox1.Width) * 1920)

        Dim remoteY As Integer =
            CInt((e.Y / PictureBox1.Height) * 1080)

        SendControlCommand("MOUSEMOVE|" & remoteX & "|" & remoteY)

    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object,
                                  e As MouseEventArgs) _
                                  Handles PictureBox1.MouseDown

        Me.Focus()

        If e.Button = MouseButtons.Left Then
            SendControlCommand("LEFTDOWN")
        ElseIf e.Button = MouseButtons.Right Then
            SendControlCommand("RIGHTDOWN")
        End If

    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object,
                                e As MouseEventArgs) _
                                Handles PictureBox1.MouseUp

        If e.Button = MouseButtons.Left Then
            SendControlCommand("LEFTUP")
        ElseIf e.Button = MouseButtons.Right Then
            SendControlCommand("RIGHTUP")
        End If

    End Sub

    Private Sub SendControlCommand(command As String)

        Try

            Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp)

            clsSocket.Connect(ClientIP, 1977)

            Dim data As Byte() =
                Encoding.ASCII.GetBytes(command)

            clsSocket.Send(data)

            clsSocket.Close()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ControlPC_KeyDown(sender As Object,
                                  e As KeyEventArgs) Handles Me.KeyDown

        e.SuppressKeyPress = True
        SendControlCommand("KEYDOWN|" & CInt(e.KeyCode))

    End Sub

    Private Sub ControlPC_KeyUp(sender As Object,
                                e As KeyEventArgs) Handles Me.KeyUp

        SendControlCommand("KEYUP|" & CInt(e.KeyCode))

    End Sub
End Class
