Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.Remoting.Messaging
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class MDIParent1
    Dim mReader As BinaryReader
    Dim mWriter As BinaryWriter = Nothing
    Const ListenPort As Int16 = 9876
    Const RequestPort As Int16 = 6789
    Shared NoofClients As Int16 = 0
    Private clientForms As New Dictionary(Of String, Form1)
    Private blackScreenStatus As New Dictionary(Of String, Boolean)
    Private ViewColumns As Integer = 5
    Public Shared Instance As MDIParent1

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

    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

    Private ServerHidden As Boolean = False

    Private Const HOTKEY_ID As Integer = 1
    Private Const WM_HOTKEY As Integer = &H312

    Private Const MOD_ALT As Integer = &H1
    Private Const MOD_CONTROL As Integer = &H2

    Sub newForm()
        Dim a As Int16 = 0
        For Each nodez As TreeNode In trvComputers.Nodes(0).Nodes

            If nodez.Tag IsNot Nothing AndAlso nodez.Tag.ToString() = "ALL" Then
                Continue For
            End If
            Dim ChildForm As New Form1 With {
                .MdiParent = Me
            }
            ChildForm.Show()
            ChildForm.lblPcName.Text = nodez.Text.ToString()

            ChildForm.ToolStripStatusLabel1.Text = "Offline"
            ChildForm.BackColor = Color.FromArgb(78, 2, 32) ' red (offline)
            clientForms(nodez.Tag.ToString()) = ChildForm
            If Not blackScreenStatus.ContainsKey(nodez.Tag.ToString()) Then
                blackScreenStatus.Add(nodez.Tag.ToString(), False)
            End If



            Dim Margin As Integer = 10
            Dim columns As Integer = ViewColumns

            Dim col As Integer = a Mod columns
            Dim row As Integer = a \ columns

            ChildForm.Location = New Point(
    Margin + col * (ChildForm.Width + Margin),
    Margin + row * (ChildForm.Height + Margin)
)

            a += 1
        Next


    End Sub

    Sub RefreshPCWindows()

        'Close existing PC windows
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next

        clientForms.Clear()

        'Recreate windows
        newForm()

    End Sub
    Public Sub reloadIP()
        trvComputers.Nodes.Clear()

        Dim tNode As TreeNode = trvComputers.Nodes.Add("Computer Laboratory")
        Dim allNode As TreeNode = tNode.Nodes.Add("All Computers")
        allNode.Tag = "ALL"

        Using objStreamReader As New StreamReader("test.txt")
            Dim strLine As String = objStreamReader.ReadLine()

            Do While strLine IsNot Nothing

                If strLine.Trim() <> "" Then
                    Dim parts As String() = strLine.Split(";"c)

                    If parts.Length >= 2 Then
                        Dim name As String = parts(0).Trim()
                        Dim ip As String = parts(1).Trim()

                        Dim cNode As TreeNode = tNode.Nodes.Add(name)
                        cNode.Tag = ip
                    End If
                End If

                strLine = objStreamReader.ReadLine()
            Loop
        End Using

        trvComputers.ExpandAll()
    End Sub
    Private Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        newForm()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles Me.Load

        RegisterHotKey(Me.Handle,
               HOTKEY_ID,
               MOD_CONTROL Or MOD_ALT,
               Keys.H)


        Instance = Me

        Me.KeyPreview = True

        cleartxt()

        Dim C As Control

        For Each C In Me.Controls
            If TypeOf C Is MdiClient Then
                C.BackColor = Color.FromArgb(45, 45, 45)
                Exit For
            End If
        Next
        For Each C In Me.Controls
            If TypeOf C Is MenuStrip Then
                C.BackColor = Color.FromArgb(30, 30, 30)
                C.ForeColor = Color.White
                Exit For
            End If
        Next
        For Each C In Me.Controls
            If TypeOf C Is ToolStrip Then
                C.BackColor = Color.FromArgb(60, 60, 60)
                C.ForeColor = Color.White
                Exit For
            End If
        Next
        For Each C In Me.Controls
            If TypeOf C Is StatusStrip Then
                C.BackColor = Color.FromArgb(30, 30, 30)
                C.ForeColor = Color.White
                Exit For
            End If
        Next
        C = Nothing
        reloadIP()
        newForm()

        btnLayout.Text = "Layout (" & ViewColumns & ")"

        Dim ListenThread As New Thread(New ThreadStart(AddressOf ListenAlways))
        ListenThread.Start()

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = WM_HOTKEY AndAlso
       m.WParam.ToInt32() = HOTKEY_ID Then

            If Not ServerHidden Then

                ShowWindow(Me.Handle, SW_HIDE)
                ServerHidden = True
                NotifyIcon1.Visible = True

                NotifyIcon1.BalloonTipTitle = "Classroom Server"
                NotifyIcon1.BalloonTipText = "Server is hidden. Press Ctrl + Alt + H to restore."
                NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info

                NotifyIcon1.ShowBalloonTip(1)

            Else

                ShowWindow(Me.Handle, SW_SHOW)

                Me.WindowState = FormWindowState.Normal
                Me.Activate()
                Me.BringToFront()

                ServerHidden = False

            End If

        End If

        MyBase.WndProc(m)

    End Sub



    Private m_ChildFormNumber As Integer

    Sub ListenAlways()
        'ALWAYS CHANGE IP TO SERVER
        Dim listener As New TcpListener(IPAddress.Parse("192.168.100.47"), ListenPort)
        listener.Start()

        While True
            Try
                Dim client As TcpClient = listener.AcceptTcpClient()
                Dim clientEndPoint As IPEndPoint =
    CType(client.Client.RemoteEndPoint, IPEndPoint)

                Dim clientIP As String = clientEndPoint.Address.ToString()

                Dim message As String = ""

                Try
                    Dim reader As New BinaryReader(client.GetStream())
                    message = reader.ReadString()
                Catch
                End Try

                If clientForms.ContainsKey(clientIP) Then

                    Dim form = clientForms(clientIP)
                    Dim pcName As String = form.lblPcName.Text

                    If message = "DISCONNECT" Then

                        form.BeginInvoke(Sub()

                                             form.SetOffline()
                                             form.ToolStripStatusLabel1.Text = "Offline"
                                             form.BackColor = Color.FromArgb(78, 2, 32)

                                         End Sub)

                        Me.BeginInvoke(Sub()
                                           AddLog(pcName & " disconnected.")
                                       End Sub)

                        MessageBox.Show(pcName & " has disconnected.")

                    Else

                        form.BeginInvoke(Sub()

                                             form.ClientIP = clientIP

                                             If Not form.StreamRunning Then
                                                 form.StartImageStream(clientIP)
                                             End If

                                         End Sub)

                        Me.BeginInvoke(Sub()
                                           AddLog(pcName & " connected.")
                                       End Sub)

                    End If

                End If

                client.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            For Each kvp In clientForms
                Dim ip = kvp.Key
                Dim form = kvp.Value
                If Not IsClientConnected(ip) Then
                    form.Invoke(Sub()
                                    form.SetOffline()
                                    form.ToolStripStatusLabel1.Text = "Offline"
                                    form.BackColor = Color.FromArgb(78, 2, 32)
                                End Sub)
                End If
            Next
            Thread.Sleep(3000)
        End While
    End Sub
    Function IsClientConnected(ip As String) As Boolean
        Try
            Dim ping As New Net.NetworkInformation.Ping()
            Dim reply = ping.Send(ip, 1000)
            Return reply.Status = Net.NetworkInformation.IPStatus.Success
        Catch
            Return False
        End Try
    End Function

    Private Sub MDIParent1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        UnregisterHotKey(Me.Handle, HOTKEY_ID)
        System.Environment.Exit(System.Environment.ExitCode)

    End Sub

    Private Function ExtractIPAddress(text As String) As String
        Return text.Split("-"c)(0).Trim()
    End Function
    Private Sub btnSendMsg_Click(sender As Object, e As EventArgs) Handles btnSendMsg.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer or All Computers first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text

        Dim message As String = customInput.ShowInput(
        "Enter message:",
        "Send Message")

        If String.IsNullOrWhiteSpace(message) Then Exit Sub

        Dim bMessage As Byte() = Encoding.ASCII.GetBytes(message)
        Dim sentCount As Integer = 0

        '=========================
        ' ALL COMPUTERS SELECTED
        '=========================
        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         Dim count As Integer = 0

                         Parallel.ForEach(trvComputers.Nodes(0).Nodes.Cast(Of TreeNode)(),
                            Sub(node)

                                If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Exit Sub

                                Try

                                    Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                                  SocketType.Stream,
                                                                  ProtocolType.Tcp)

                                        clsSocket.SendTimeout = 1000
                                        clsSocket.ReceiveTimeout = 1000

                                        clsSocket.Connect(node.Tag.ToString(), 1977)
                                        clsSocket.Send(bMessage)

                                    End Using

                                    Interlocked.Increment(count)

                                Catch
                                    'Ignore offline PCs
                                End Try

                            End Sub)


                         Me.Invoke(Sub()

                                       AddLog("Message sent to ALL")

                                       MessageBox.Show("Message sent to " &
                                               count &
                                               " computer(s).")

                                   End Sub)

                     End Sub)


        Else

            '=========================
            ' SINGLE COMPUTER
            '=========================
            Try

                Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                    SocketType.Stream,
                                    ProtocolType.Tcp)

                clsSocket.Connect(trvComputers.SelectedNode.Tag.ToString(), 1977)
                clsSocket.Send(bMessage)
                clsSocket.Close()

                AddLog("Message sent to " & pcName)

                MessageBox.Show("Message sent to " & trvComputers.SelectedNode.Text)

            Catch ex As Exception

                MessageBox.Show(ex.Message)

            End Try

        End If

    End Sub

    Private Sub trvComputers_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvComputers.AfterSelect
        If e.Node.Parent Is Nothing Then
            cleartxt()
            Exit Sub
        End If
        txtName.Text = e.Node.Text
        txtIp.Text = If(e.Node.Tag IsNot Nothing, e.Node.Tag.ToString(), "")
    End Sub
    Sub cleartxt()
        txtName.Text = ""
        txtIp.Text = ""
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim frm As New About()
        frm.ShowDialog()
    End Sub

    Private Sub btnShutdownPC_Click(sender As Object, e As EventArgs) Handles btnShutdownPC.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer or All Computers first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text

        If MessageBox.Show("Are you sure you want to shut down the selected computer(s)?",
                   "Confirm Shutdown",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Warning) = DialogResult.No Then
            Exit Sub
        End If

        Dim command As Byte() = Encoding.ASCII.GetBytes("SHUTDOWN")
        Dim successCount As Integer = 0

        '=========================
        'ALL COMPUTERS
        '=========================

        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         Dim count As Integer = 0

                         Parallel.ForEach(trvComputers.Nodes(0).Nodes.Cast(Of TreeNode)(),
                            Sub(node)

                                If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Exit Sub

                                Try

                                    Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                                  SocketType.Stream,
                                                                  ProtocolType.Tcp)

                                        clsSocket.SendTimeout = 1000
                                        clsSocket.ReceiveTimeout = 1000

                                        clsSocket.Connect(node.Tag.ToString(), 1977)
                                        clsSocket.Send(command)

                                    End Using

                                    Interlocked.Increment(count)

                                Catch
                                    'Ignore offline PCs
                                End Try

                            End Sub)

                         Me.Invoke(Sub()
                                       AddLog("Shutdown command sent to all computers.")

                                       MessageBox.Show("Shutdown command sent to " &
                                               count &
                                               " computer(s).",
                                               "Success",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Information)
                                   End Sub)

                     End Sub)

            Exit Sub

        Else

            '=========================
            'SINGLE COMPUTER
            '=========================
            Try

                Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                    SocketType.Stream,
                                    ProtocolType.Tcp)

                clsSocket.Connect(trvComputers.SelectedNode.Tag.ToString(), 1977)

                clsSocket.Send(command)
                clsSocket.Close()

                AddLog(pcName & " was sent a shutdown command.")

                MessageBox.Show("Shutdown command sent to " & pcName,
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show("Unable to connect to " &
                        pcName &
                        vbCrLf & ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            End Try

        End If

    End Sub

    Private Sub btnLockPC_Click(sender As Object, e As EventArgs) Handles btnLockPC.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer or All Computers first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text

        'Confirm
        If MessageBox.Show("Are you sure you want to lock the selected computer(s)?",
                       "Confirm Lock",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Dim command As Byte() = Encoding.ASCII.GetBytes("LOCK")
        Dim successCount As Integer = 0

        '=========================
        'ALL COMPUTERS
        '=========================

        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         Dim count As Integer = 0

                         Parallel.ForEach(trvComputers.Nodes(0).Nodes.Cast(Of TreeNode)(),
                            Sub(node)

                                If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Exit Sub

                                Try

                                    Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                                  SocketType.Stream,
                                                                  ProtocolType.Tcp)

                                        clsSocket.SendTimeout = 1000
                                        clsSocket.ReceiveTimeout = 1000

                                        clsSocket.Connect(node.Tag.ToString(), 1977)
                                        clsSocket.Send(command)

                                    End Using

                                    Interlocked.Increment(count)

                                Catch
                                    'Ignore offline PCs
                                End Try

                            End Sub)


                         Me.Invoke(Sub()

                                       AddLog("Lock command sent to all computers.")

                                       MessageBox.Show("Lock command sent to " &
                                                       count &
                                                       " computer(s).",
                                                       "Success",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information)

                                   End Sub)

                     End Sub)

        Else

            '=========================
            'SINGLE COMPUTER
            '=========================
            Try
                Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp)

                clsSocket.Connect(trvComputers.SelectedNode.Tag.ToString(), 1977)

                clsSocket.Send(command)
                clsSocket.Close()

                AddLog(pcName & " was locked.")

                MessageBox.Show("Lock command sent to " & pcName,
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show("Unable to connect to " &
                            trvComputers.SelectedNode.Text &
                            vbCrLf & ex.Message)

            End Try

        End If

    End Sub

    Private Sub btnRestartPC_Click(sender As Object, e As EventArgs) Handles btnRestartPC.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer or All Computers first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MessageBox.Show("Are you sure you want to restart the selected computer(s)?",
                       "Confirm Restart",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning) = DialogResult.No Then
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text

        Dim successCount As Integer = 0

        Dim command As Byte() = Encoding.ASCII.GetBytes("RESTART")

        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         Dim count As Integer = 0

                         Parallel.ForEach(trvComputers.Nodes(0).Nodes.Cast(Of TreeNode)(),
                     Sub(node)

                         If node.Tag Is Nothing Then Exit Sub
                         If node.Tag.ToString() = "ALL" Then Exit Sub

                         Try

                             Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                           SocketType.Stream,
                                                           ProtocolType.Tcp)

                                 clsSocket.SendTimeout = 1000
                                 clsSocket.ReceiveTimeout = 1000

                                 clsSocket.Connect(node.Tag.ToString(), 1977)

                                 clsSocket.Send(command)

                             End Using

                             Interlocked.Increment(count)

                         Catch
                             'Ignore offline PCs
                         End Try

                     End Sub)

                         Me.Invoke(Sub()

                                       AddLog("Restart command sent to all computers.")

                                       MessageBox.Show("Restart command sent to " &
                                               count &
                                               " computer(s).",
                                               "Success",
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Information)

                                   End Sub)

                     End Sub)

            Exit Sub

        Else

            Try

                Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                      SocketType.Stream,
                                      ProtocolType.Tcp)

                    clsSocket.Connect(trvComputers.SelectedNode.Tag.ToString(), 1977)

                    clsSocket.Send(command)

                End Using

                AddLog(pcName & " was restarted.")

                MessageBox.Show("Restart command sent to " & pcName,
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show("Unable to connect to " &
                        pcName &
                        vbCrLf &
                        ex.Message)

            End Try

        End If

    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        welcome.Show()
        Me.Hide()
    End Sub


    Private Sub btnAddPC_Click(sender As Object, e As EventArgs) Handles btnAddPC.Click
        'Ask for PC Name
        Dim pcName As String = customInput.ShowInput(
        "Enter computer name:",
        "Add Computer")

        If String.IsNullOrWhiteSpace(pcName) Then
            MessageBox.Show("Computer name cannot be empty.")
            Exit Sub
        End If

        'Ask for IP Address
        Dim pcIP As String = customInput.ShowInput(
        "Enter IP address:",
        "Add Computer")

        If String.IsNullOrWhiteSpace(pcIP) Then
            MessageBox.Show("IP address cannot be empty.")
            Exit Sub
        End If

        '=========================
        'CHECK FOR DUPLICATES
        '=========================
        For Each node As TreeNode In trvComputers.Nodes(0).Nodes

            If node.Text.Equals(pcName, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("A computer with this name already exists.",
                            "Duplicate Name",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                Exit Sub
            End If

            If node.Tag IsNot Nothing AndAlso node.Tag.ToString() = pcIP Then
                MessageBox.Show("This IP address is already assigned to another computer.",
                            "Duplicate IP",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                Exit Sub
            End If

        Next

        'Add to TreeView
        Dim newNode As TreeNode = trvComputers.Nodes(0).Nodes.Add(pcName)

        'Store IP in Tag
        newNode.Tag = pcIP

        'Save to test.txt
        SaveTree()

        RefreshPCWindows()

        AddLog(pcName & " was added to the computer list.")

        MessageBox.Show("Computer added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
    End Sub

    Private Sub btnEditPC_Click(sender As Object, e As EventArgs) Handles btnEditPC.Click
        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Exit Sub
        End If

        If trvComputers.SelectedNode.Tag IsNot Nothing AndAlso
   trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            MessageBox.Show("You cannot edit or delete the All Computers node.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Exit Sub
        End If


        Dim selectedNode As TreeNode = trvComputers.SelectedNode


        'Get current information

        Dim currentName As String = selectedNode.Text
        Dim currentIP As String = selectedNode.Tag.ToString()


        'Edit PC Name
        Dim newName As String = customInput.ShowInput(
            "Enter new computer name:",
            "Edit Computer Name",
            currentName)

        If String.IsNullOrWhiteSpace(newName) Then
            MessageBox.Show("Computer name cannot be empty.")
            Exit Sub
        End If


        'Edit IP Address
        Dim newIP As String = customInput.ShowInput(
            "Enter new IP address:",
            "Edit IP Address",
            currentIP)

        If String.IsNullOrWhiteSpace(newIP) Then
            MessageBox.Show("IP address cannot be empty.")
            Exit Sub
        End If

        '=========================
        'CHECK FOR DUPLICATES
        '=========================
        For Each node As TreeNode In trvComputers.Nodes(0).Nodes

            'Skip the computer being edited
            If node Is selectedNode Then Continue For

            If node.Text.Equals(newName, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("A computer with this name already exists.",
                        "Duplicate Name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                Exit Sub
            End If

            If node.Tag IsNot Nothing AndAlso node.Tag.ToString() = newIP Then
                MessageBox.Show("This IP address is already assigned to another computer.",
                        "Duplicate IP",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                Exit Sub
            End If

        Next


        'Update TreeView
        selectedNode.Text = newName
        selectedNode.Tag = newIP


        'Save changes
        SaveTree()
        'Update PC Windows
        RefreshPCWindows()

        If currentName = newName AndAlso currentIP = newIP Then
            AddLog(currentName & " information was opened for editing (no changes made).")
        Else
            AddLog(currentName & " was updated to " & newName &
           " (" & currentIP & " → " & newIP & ").")
        End If


        MessageBox.Show("Computer information updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
    End Sub

    Private Sub btnDeletePC_Click(sender As Object, e As EventArgs) Handles btnDeletePC.Click
        If trvComputers.SelectedNode.Tag IsNot Nothing AndAlso
        trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            MessageBox.Show("You cannot edit or delete the All Computers node.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Exit Sub

        End If

        'Check if a computer is selected
        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text
        Dim selectedNode As TreeNode = trvComputers.SelectedNode


        'Confirm deletion
        Dim result As DialogResult = MessageBox.Show(
        "Are you sure you want to delete " & selectedNode.Text & "?",
        "Delete Computer",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    )


        If result = DialogResult.Yes Then

            'Remove from TreeView
            selectedNode.Remove()

            'Save changes to test.txt
            SaveTree()

            'Update PC Windows
            RefreshPCWindows()


            'Clear displayed information
            cleartxt()

            AddLog(pcName & " was removed from the computer list.")

            MessageBox.Show("Computer deleted successfully.",
                        "Deleted",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)

        End If
    End Sub

    Sub SaveTree()

        Using writer As New StreamWriter("test.txt")

            For Each node As TreeNode In trvComputers.Nodes(0).Nodes

                If node.Tag IsNot Nothing AndAlso node.Tag.ToString() = "ALL" Then
                    Continue For
                End If

                writer.WriteLine(node.Text & ";" & node.Tag.ToString())

            Next

        End Using

    End Sub

    Private Sub btnOpenCalculator_Click(sender As Object, e As EventArgs) Handles btnOpenCalculator.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer or All Computers first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text

        'ALL COMPUTERS
        If trvComputers.SelectedNode.Tag IsNot Nothing AndAlso
   trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         For Each node As TreeNode In trvComputers.Nodes(0).Nodes

                             If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Continue For

                             Try
                                 Using s As New Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp)

                                     s.SendTimeout = 1000
                                     s.ReceiveTimeout = 1000

                                     s.Connect(node.Tag.ToString(), 1977)
                                     s.Send(Encoding.ASCII.GetBytes("CALCULATOR"))

                                 End Using

                             Catch
                                 ' Ignore offline PCs
                             End Try

                         Next

                         Me.Invoke(Sub()
                                       AddLog("Calculator Opened for All")
                                       MessageBox.Show("Calculator command sent to all computers.")
                                   End Sub)

                     End Sub)

            Exit Sub
        End If

        'ONE COMPUTER
        Dim targetIP As String = trvComputers.SelectedNode.Tag.ToString()

        Try

            Dim s As New Socket(AddressFamily.InterNetwork,
                            SocketType.Stream,
                            ProtocolType.Tcp)

            s.Connect(targetIP, 1977)
            s.Send(Encoding.ASCII.GetBytes("CALCULATOR"))
            s.Close()

            AddLog(" Calculator opened on " & pcName)

            MessageBox.Show("Calculator opened on " & pcName & ".")

        Catch ex As Exception

            MessageBox.Show("Unable to connect to " & pcName & vbCrLf & ex.Message)

        End Try

    End Sub

    Private Sub btnCaptureScreen_Click(sender As Object, e As EventArgs) Handles btnCaptureScreen.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer first.",
                        "No Computer Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        'ADD THIS HERE
        If trvComputers.SelectedNode.Tag IsNot Nothing AndAlso
       trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            MessageBox.Show("Screen capture is only available for individual computers.",
                        "Invalid Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim pcName As String = trvComputers.SelectedNode.Text
        Dim ip As String = trvComputers.SelectedNode.Tag.ToString()

        If Not clientForms.ContainsKey(ip) Then
            MessageBox.Show("Computer window not found.")
            Exit Sub
        End If

        Dim frm As Form1 = clientForms(ip)

        If frm.PictureBox1.Image Is Nothing Then
            MessageBox.Show("No image available.")
            Exit Sub
        End If

        Dim sfd As New SaveFileDialog
        sfd.Filter = "JPEG Image|*.jpg"
        sfd.FileName = trvComputers.SelectedNode.Text & "_" &
                   DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".jpg"

        AddLog("Screen Capture successful on " & pcName)

        If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

        Try
            Using bmp As New Bitmap(frm.PictureBox1.Image)
                bmp.Save(sfd.FileName, Imaging.ImageFormat.Jpeg)
            End Using

            MessageBox.Show("Screenshot saved successfully.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnBlackScreen_Click(sender As Object, e As EventArgs) Handles btnBlackScreen.Click

        If trvComputers.SelectedNode Is Nothing OrElse trvComputers.SelectedNode.Parent Is Nothing Then
            MessageBox.Show("Please select a computer first.",
                    "No Computer Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Exit Sub
        End If


        'ALL COMPUTERS
        If trvComputers.SelectedNode.Tag IsNot Nothing AndAlso
   trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            Task.Run(Sub()

                         For Each node As TreeNode In trvComputers.Nodes(0).Nodes

                             If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Continue For

                             Try

                                 Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                       SocketType.Stream,
                                                       ProtocolType.Tcp)

                                     clsSocket.SendTimeout = 1000
                                     clsSocket.ReceiveTimeout = 1000

                                     clsSocket.Connect(node.Tag.ToString(), 1977)

                                     Dim data As Byte() = Encoding.ASCII.GetBytes("BLACKSCREEN")

                                     clsSocket.Send(data)

                                 End Using

                             Catch
                                 'Ignore offline computers
                             End Try

                         Next


                         Me.Invoke(Sub()
                                       AddLog("All Computers Black Screened.")
                                       MessageBox.Show("Black Screen command sent to all computers.")
                                   End Sub)

                     End Sub)

            Exit Sub

        End If


        'ONE COMPUTER
        Dim ip As String = trvComputers.SelectedNode.Tag.ToString()
        Dim pcName As String = trvComputers.SelectedNode.Text

        Dim command As String

        If blackScreenStatus(ip) Then
            command = "UNBLACKSCREEN"
            blackScreenStatus(ip) = False
        Else
            command = "BLACKSCREEN"
            blackScreenStatus(ip) = True
        End If


        Try

            Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp)

            clsSocket.Connect(ip, 1977)

            Dim data As Byte() = Encoding.ASCII.GetBytes(command)

            clsSocket.Send(data)
            clsSocket.Close()

            AddLog(pcName & " Blackscreened / Unblackscreened.")

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        reloadIP()
        RefreshPCWindows()
    End Sub

    Private Sub btnShareScreen_Click(sender As Object, e As EventArgs) Handles btnShareScreen.Click

        If trvComputers.SelectedNode Is Nothing OrElse
       trvComputers.SelectedNode.Parent Is Nothing Then

            MessageBox.Show("Please select a computer first.",
                        "No Computer Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub

        End If

        '=========================
        'ALL COMPUTERS
        '=========================
        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            BroadcastManager.BroadcastToAll = True

            If Not BroadcastManager.BroadcastRunning Then
                BroadcastManager.StartBroadcast()
            End If

            Task.Run(Sub()

                         Dim successCount As Integer = 0

                         Parallel.ForEach(trvComputers.Nodes(0).Nodes.Cast(Of TreeNode)(),
                 Sub(node)

                     If node.Tag Is Nothing Then Exit Sub
                     If node.Tag.ToString() = "ALL" Then Exit Sub

                     Try

                         Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                       SocketType.Stream,
                                                       ProtocolType.Tcp)

                             clsSocket.SendTimeout = 1000
                             clsSocket.ReceiveTimeout = 1000

                             clsSocket.Connect(node.Tag.ToString(), 1977)

                             Dim command As Byte() =
                                 Encoding.ASCII.GetBytes("STARTBROADCAST")

                             clsSocket.Send(command)

                         End Using

                         Interlocked.Increment(successCount)

                     Catch

                     End Try

                 End Sub)

                         Me.BeginInvoke(Sub()

                                            AddLog("Started broadcast to all computers.")

                                            MessageBox.Show(
                                    "Broadcast started on " &
                                    successCount &
                                    " computer(s).")

                                        End Sub)

                     End Sub)

            Exit Sub

            Exit Sub

        End If

        '=========================
        'SINGLE COMPUTER
        '=========================

        Dim pcName As String = trvComputers.SelectedNode.Text
        Dim ip As String = trvComputers.SelectedNode.Tag.ToString()

        BroadcastManager.BroadcastToAll = False
        BroadcastManager.SelectedClientIP = ip

        If Not BroadcastManager.BroadcastRunning Then
            BroadcastManager.StartBroadcast()
        End If

        Try

            Dim clsSocket As New Socket(AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp)

            clsSocket.Connect(ip, 1977)

            Dim command As Byte() =
        Encoding.ASCII.GetBytes("STARTBROADCAST")

            clsSocket.Send(command)

            clsSocket.Close()

            AddLog("Started screen sharing with " & pcName)

            MessageBox.Show("Broadcast started with " & pcName)

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub MDIParent1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F5 Then
            btnRefresh.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.M Then
            btnSendMsg.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.A Then
            btnSendMsg.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.L Then
            btnLockPC.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.B Then
            btnBlackScreen.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.C Then
            btnOpenCalculator.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.R Then
            btnRestartPC.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnShutdownPC.PerformClick()
        End If

        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnCaptureScreen.PerformClick()
        End If

        If e.KeyCode = Keys.Delete Then
            btnDeletePC.PerformClick()
        End If

        If e.KeyCode = Keys.F1 Then
            btnAbout.PerformClick()
        End If

    End Sub

    Public Sub AddLog(action As String)

        lstActivityLog.Items.Add(
            DateTime.Now.ToString("hh:mm:ss tt") &
            " | " &
            action)

        lstActivityLog.TopIndex = lstActivityLog.Items.Count - 1

    End Sub

    Private Sub btnLayout_Click(sender As Object, e As EventArgs) Handles btnLayout.Click
        Select Case ViewColumns

            Case 5
                ViewColumns = 4

            Case 4
                ViewColumns = 3

            Case Else
                ViewColumns = 5

        End Select

        btnLayout.Text = "Layout (" & ViewColumns & ")"

        RefreshPCWindows()

    End Sub

    Private Sub btnControlPC_Click(sender As Object, e As EventArgs) Handles btnControlPC.Click

        'Must select a computer
        If trvComputers.SelectedNode Is Nothing OrElse
       trvComputers.SelectedNode.Parent Is Nothing Then

            MessageBox.Show("Please select a computer first.",
                        "No Computer Selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        'Cannot control All Computers
        If trvComputers.SelectedNode.Tag.ToString() = "ALL" Then

            MessageBox.Show("Please select an individual computer.",
                        "Invalid Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim ip As String = trvComputers.SelectedNode.Tag.ToString()
        Dim pcName As String = trvComputers.SelectedNode.Text

        If MessageBox.Show("Do you want to start remote control of " & pcName & "?",
                   "Remote Control",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        'Check if the computer window exists
        If Not clientForms.ContainsKey(ip) Then

            MessageBox.Show("Computer window not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
            Exit Sub

        End If

        'Check if the PC is online
        Dim frmClient As Form1 = clientForms(ip)

        If Not frmClient.StreamRunning Then

            MessageBox.Show(pcName & " is offline.",
                        "Offline",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
            Exit Sub

        End If

        'Open the control window
        Dim frmControl As New ControlPC

        frmControl.ClientIP = ip
        frmControl.PCName = pcName

        frmControl.Show()

        AddLog("Started remote control session with " & pcName & ".")

    End Sub

    Private Sub btnStopShareScreen_Click(sender As Object, e As EventArgs) Handles btnStopShareScreen.Click

        If Not BroadcastManager.BroadcastRunning Then

            MessageBox.Show("Screen sharing is not running.",
                        "Stop Sharing",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

            Exit Sub

        End If


        'Stop teacher broadcast
        BroadcastManager.StopBroadcast()


        'Tell all clients to close broadcast window
        Task.Run(Sub()

                     For Each node As TreeNode In trvComputers.Nodes(0).Nodes

                         If node.Tag Is Nothing OrElse node.Tag.ToString() = "ALL" Then Continue For

                         Try

                             Using clsSocket As New Socket(AddressFamily.InterNetwork,
                                                       SocketType.Stream,
                                                       ProtocolType.Tcp)

                                 clsSocket.Connect(node.Tag.ToString(), 1977)

                                 Dim command As Byte() =
                                 Encoding.ASCII.GetBytes("STOPBROADCAST")

                                 clsSocket.Send(command)

                             End Using

                         Catch
                             'Ignore offline PCs
                         End Try

                     Next

                 End Sub)


        AddLog("Stopped screen sharing.")

        MessageBox.Show("Screen sharing stopped.",
                    "Screen Share",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

    End Sub

    Private Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHide.Click
        Me.Hide()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If ServerHidden Then

            ShowWindow(Me.Handle, SW_SHOW)

            Me.WindowState = FormWindowState.Normal

            Me.BringToFront()

            Me.Activate()

            ServerHidden = False

        End If
    End Sub
End Class
