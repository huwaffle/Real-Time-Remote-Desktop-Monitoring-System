Public Class ScreenShare

    Public SourcePictureBox As PictureBox
    Public BroadcastMode As Boolean = False

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If BroadcastMode Then
            Me.Text = "Broadcast Mode"
        Else
            Me.Text = "Single Mode"
        End If

        If BroadcastMode Then

            Dim bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width,
                              Screen.PrimaryScreen.Bounds.Height)

            Using g As Graphics = Graphics.FromImage(bmp)
                g.CopyFromScreen(0, 0, 0, 0, bmp.Size)
            End Using

            If PictureBox2.Image IsNot Nothing Then
                PictureBox2.Image.Dispose()
            End If

            PictureBox2.Image = bmp
            PictureBox2.Refresh()

        Else

            If SourcePictureBox Is Nothing Then Exit Sub
            If SourcePictureBox.Image Is Nothing Then Exit Sub

            If PictureBox2.Image IsNot Nothing Then
                PictureBox2.Image.Dispose()
            End If

            PictureBox2.Image = CType(SourcePictureBox.Image.Clone(), Image)

        End If

    End Sub

    Private Sub ScreenShare_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
End Class