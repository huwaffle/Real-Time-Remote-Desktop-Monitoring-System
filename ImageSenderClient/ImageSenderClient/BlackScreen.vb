Public Class BlackScreen
    Private Sub BlackScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Black Screen"

        Me.WindowState = FormWindowState.Maximized

        Me.FormBorderStyle = FormBorderStyle.Sizable

        Me.TopMost = True

    End Sub
End Class