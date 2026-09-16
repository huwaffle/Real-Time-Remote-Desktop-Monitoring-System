Public Class welcome

    Private Sub welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterControls()
    End Sub

    Private Sub welcome_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        CenterControls()
    End Sub

    Private Sub CenterControls()

        ' Center horizontally
        lblTitle.Left = (Me.ClientSize.Width - lblTitle.Width) \ 2
        btnEnter.Left = (Me.ClientSize.Width - btnEnter.Width) \ 2
        btnExit.Left = (Me.ClientSize.Width - btnExit.Width) \ 2

        ' Center vertically
        lblTitle.Top = (Me.ClientSize.Height \ 2) - 100
        btnEnter.Top = lblTitle.Bottom + 30
        btnExit.Top = btnEnter.Bottom + 15

    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        MDIParent1.Show()
        Me.Hide()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

End Class