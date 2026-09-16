Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Net.Sockets
Imports System.IO
Imports System.Drawing
Imports System.Threading

Public Class Form1
    Private streamThread As Thread
    Private clientSocket As TcpClient
    Private ipAddress As String
    Const RequestPort As Integer = 6789
    Private connectedSeconds As Integer = 0
    Private isConnected As Boolean = False

    Private borderRadius As Integer = 30
    Private borderSize As Integer = 3
    Private borderColor As Color = Color.FromArgb(56, 56, 56)

    Public Property ClientIP As String
    Public ReadOnly Property StreamRunning As Boolean
        Get
            Return streamThread IsNot Nothing AndAlso streamThread.IsAlive
        End Get
    End Property
    Public Sub StartImageStream(ip As String)
        If streamThread IsNot Nothing AndAlso streamThread.IsAlive Then
            Exit Sub
        End If

        ipAddress = ip

        streamThread = New Thread(AddressOf showImage)
        streamThread.IsBackground = True
        streamThread.Start()

    End Sub

    Private Sub showImage()

        Try

            clientSocket = New TcpClient(ipAddress, RequestPort)

            Using netStream As NetworkStream = clientSocket.GetStream()

                Using reader As New BinaryReader(netStream)

                    While True

                        Dim imgLength As Integer = reader.ReadInt32()
                        Dim imgData As Byte() = reader.ReadBytes(imgLength)

                        Using ms As New MemoryStream(imgData)

                            Dim bmp As Bitmap = CType(Bitmap.FromStream(ms).Clone(), Bitmap)

                            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then
                                bmp.Dispose()
                                Exit While
                            End If

                            Me.BeginInvoke(Sub()

                                               If PictureBox1.Image IsNot Nothing Then
                                                   PictureBox1.Image.Dispose()
                                               End If

                                               PictureBox1.Image = bmp

                                               If ControlPC.ActiveControlForm IsNot Nothing Then
                                                   If ControlPC.ActiveControlForm.ClientIP = ClientIP Then
                                                       ControlPC.ActiveControlForm.UpdateImage(CType(bmp.Clone(), Bitmap))
                                                   End If
                                               End If

                                               ToolStripStatusLabel1.Text = "Online " & ClientIP
                                               BackColor = Color.FromArgb(2, 78, 32)

                                               If Not isConnected Then
                                                   isConnected = True
                                                   connectedSeconds = 0
                                                   tmrConnection.Start()
                                               End If

                                           End Sub)

                        End Using

                    End While

                End Using

            End Using

        Catch ex As Exception

            If Not Me.IsDisposed AndAlso Me.IsHandleCreated Then
                Me.BeginInvoke(Sub()
                                   SetOffline()
                               End Sub)
            End If

        End Try

    End Sub
    Public Sub New()

        InitializeComponent()

        Me.FormBorderStyle = FormBorderStyle.None
        Me.Padding = New Padding(borderSize)
        Me.PanelTitleBar.BackColor = borderColor
        Me.BackColor = borderColor
    End Sub


    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Sub panelTitleBar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PanelTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112, &HF012, 0)
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or &H20000
            Return cp
        End Get
    End Property
    Private Function GetRoundedPath(rect As Rectangle, radius As Single) As GraphicsPath
        Dim path As GraphicsPath = New GraphicsPath()
        Dim curveSize As Single = radius * 2.0F
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90)
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90)
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90)
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90)
        path.CloseFigure()
        Return path
    End Function
    Private Sub FormRegionAndBorder(form As Form, radius As Single, graph As Graphics, borderColor As Color, borderSize As Single)
        If Me.WindowState <> FormWindowState.Minimized Then
            Using roundPath As GraphicsPath = GetRoundedPath(form.ClientRectangle, radius)
                Using penBorder As Pen = New Pen(borderColor, borderSize)
                    Using transform As Matrix = New Matrix()

                        graph.SmoothingMode = SmoothingMode.AntiAlias
                        form.Region = New Region(roundPath)
                        If borderSize >= 1 Then
                            Dim rect As Rectangle = form.ClientRectangle
                            Dim scaleX As Single = 1.0F - ((borderSize + 1) / rect.Width)
                            Dim scaleY As Single = 1.0F - ((borderSize + 1) / rect.Height)
                            transform.Scale(scaleX, scaleY)
                            transform.Translate(borderSize / 1.6F, borderSize / 1.6F)
                            graph.Transform = transform
                            graph.DrawPath(penBorder, roundPath)
                        End If

                    End Using
                End Using
            End Using
        End If
    End Sub
    Private Sub ControlRegionAndBorder(control As Control, radius As Single, graph As Graphics, borderColor As Color)
        Using roundPath As GraphicsPath = GetRoundedPath(control.ClientRectangle, radius)

            Using penBorder As Pen = New Pen(borderColor, 1)
                graph.SmoothingMode = SmoothingMode.AntiAlias
                control.Region = New Region(roundPath)
                graph.DrawPath(penBorder, roundPath)
            End Using
        End Using
    End Sub
    Private Structure FormBoundsColors
        Public TopLeftColor As Color
        Public TopRightColor As Color
        Public BottomLeftColor As Color
        Public BottomRightColor As Color
    End Structure
    Private Function GetFormBoundsColors() As FormBoundsColors
        Dim fbColor = New FormBoundsColors()
        Using bmp = New Bitmap(1, 1)
            Using graph As Graphics = Graphics.FromImage(bmp)
                Dim rectBmp As New Rectangle(0, 0, 1, 1)
                'Top Left
                rectBmp.X = Me.Bounds.X - 1
                rectBmp.Y = Me.Bounds.Y
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size)
                fbColor.TopLeftColor = bmp.GetPixel(0, 0)
                'Top Right
                rectBmp.X = Me.Bounds.Right
                rectBmp.Y = Me.Bounds.Y
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size)
                fbColor.TopRightColor = bmp.GetPixel(0, 0)
                'Bottom Left
                rectBmp.X = Me.Bounds.X
                rectBmp.Y = Me.Bounds.Bottom
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size)
                fbColor.BottomLeftColor = bmp.GetPixel(0, 0)
                'Bottom Right
                rectBmp.X = Me.Bounds.Right
                rectBmp.Y = Me.Bounds.Bottom
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size)
                fbColor.BottomRightColor = bmp.GetPixel(0, 0)
            End Using
        End Using
        Return fbColor
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        For Each C In Me.Controls
            If TypeOf C Is StatusStrip Then
                C.BackColor = Color.FromArgb(30, 30, 30)
                C.ForeColor = Color.White
                Exit For
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If

        Me.Close()
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click

        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub tmrConnection_Tick(sender As Object, e As EventArgs) Handles tmrConnection.Tick

        connectedSeconds += 1

        Dim ts As TimeSpan = TimeSpan.FromSeconds(connectedSeconds)

        lblConnectionTime.Text = "Connected Time: " & ts.ToString("hh\:mm\:ss")

    End Sub

    Public Sub SetOffline()

        isConnected = False
        connectedSeconds = 0

        tmrConnection.Stop()

        lblConnectionTime.Text = "Connected Time: 00:00:00"

    End Sub
End Class