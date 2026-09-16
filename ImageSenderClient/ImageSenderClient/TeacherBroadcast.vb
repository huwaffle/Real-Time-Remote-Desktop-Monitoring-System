Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Public Class TeacherBroadcast

    Private Const BroadcastPort As Integer = 6790

    Private receiveThread As Thread

    Private Sub TeacherBroadcast_Load(sender As Object,
                                  e As EventArgs) Handles MyBase.Load

        Me.Text = "Teacher Screen"

        Me.StartPosition = FormStartPosition.CenterScreen

        Me.WindowState = FormWindowState.Normal

        Me.FormBorderStyle = FormBorderStyle.Sizable

        Me.TopMost = True

        receiveThread = New Thread(AddressOf ReceiveBroadcast)
        receiveThread.IsBackground = True
        receiveThread.Start()

    End Sub

    Private Sub ReceiveBroadcast()

        Try
            'CHANGE TO SERVER PCS IP
            Dim client As New TcpClient("192.168.100.47", BroadcastPort)

            Using ns As NetworkStream = client.GetStream()

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

                                           End Sub)

                        End Using

                    End While

                End Using

            End Using

        Catch ex As Exception

            Me.BeginInvoke(Sub()

                               Me.Close()

                           End Sub)

        End Try

    End Sub
End Class