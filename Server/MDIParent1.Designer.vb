<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MDIParent1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIParent1))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnHide = New System.Windows.Forms.Button()
        Me.btnStopShareScreen = New System.Windows.Forms.Button()
        Me.btnShareScreen = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstActivityLog = New System.Windows.Forms.ListBox()
        Me.btnBlackScreen = New System.Windows.Forms.Button()
        Me.btnCaptureScreen = New System.Windows.Forms.Button()
        Me.btnOpenCalculator = New System.Windows.Forms.Button()
        Me.btnDeletePC = New System.Windows.Forms.Button()
        Me.btnEditPC = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAddPC = New System.Windows.Forms.Button()
        Me.txtIp = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSendMsg = New System.Windows.Forms.Button()
        Me.trvComputers = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnShutdownPC = New System.Windows.Forms.Button()
        Me.btnLockPC = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRestartPC = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnLayout = New System.Windows.Forms.Button()
        Me.btnControlPC = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.AutoSize = False
        Me.MenuStrip.BackColor = System.Drawing.Color.Maroon
        Me.MenuStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1659, 83)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Maroon
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.trvComputers)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(353, 939)
        Me.Panel1.TabIndex = 9
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnHide)
        Me.Panel2.Controls.Add(Me.btnStopShareScreen)
        Me.Panel2.Controls.Add(Me.btnShareScreen)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lstActivityLog)
        Me.Panel2.Controls.Add(Me.btnBlackScreen)
        Me.Panel2.Controls.Add(Me.btnCaptureScreen)
        Me.Panel2.Controls.Add(Me.btnOpenCalculator)
        Me.Panel2.Controls.Add(Me.btnDeletePC)
        Me.Panel2.Controls.Add(Me.btnEditPC)
        Me.Panel2.Controls.Add(Me.txtName)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnAddPC)
        Me.Panel2.Controls.Add(Me.txtIp)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.btnSendMsg)
        Me.Panel2.Location = New System.Drawing.Point(13, 292)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(295, 1052)
        Me.Panel2.TabIndex = 34
        '
        'btnHide
        '
        Me.btnHide.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnHide.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHide.ForeColor = System.Drawing.Color.Linen
        Me.btnHide.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnHide.Location = New System.Drawing.Point(0, 987)
        Me.btnHide.Margin = New System.Windows.Forms.Padding(4)
        Me.btnHide.Name = "btnHide"
        Me.btnHide.Size = New System.Drawing.Size(281, 55)
        Me.btnHide.TabIndex = 43
        Me.btnHide.Text = "Hide"
        Me.btnHide.UseVisualStyleBackColor = False
        '
        'btnStopShareScreen
        '
        Me.btnStopShareScreen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStopShareScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnStopShareScreen.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopShareScreen.ForeColor = System.Drawing.Color.DarkRed
        Me.btnStopShareScreen.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnStopShareScreen.Location = New System.Drawing.Point(-1, 679)
        Me.btnStopShareScreen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStopShareScreen.Name = "btnStopShareScreen"
        Me.btnStopShareScreen.Size = New System.Drawing.Size(282, 55)
        Me.btnStopShareScreen.TabIndex = 42
        Me.btnStopShareScreen.Text = "Stop Share Screen"
        Me.btnStopShareScreen.UseVisualStyleBackColor = False
        '
        'btnShareScreen
        '
        Me.btnShareScreen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShareScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnShareScreen.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShareScreen.ForeColor = System.Drawing.Color.LimeGreen
        Me.btnShareScreen.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnShareScreen.Location = New System.Drawing.Point(0, 616)
        Me.btnShareScreen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnShareScreen.Name = "btnShareScreen"
        Me.btnShareScreen.Size = New System.Drawing.Size(282, 55)
        Me.btnShareScreen.TabIndex = 28
        Me.btnShareScreen.Text = "Share Screen"
        Me.btnShareScreen.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 761)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 30)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "ACTIVITY LOG"
        '
        'lstActivityLog
        '
        Me.lstActivityLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lstActivityLog.Font = New System.Drawing.Font("VT323", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstActivityLog.ForeColor = System.Drawing.Color.White
        Me.lstActivityLog.FormattingEnabled = True
        Me.lstActivityLog.HorizontalScrollbar = True
        Me.lstActivityLog.ItemHeight = 26
        Me.lstActivityLog.Location = New System.Drawing.Point(-1, 794)
        Me.lstActivityLog.Name = "lstActivityLog"
        Me.lstActivityLog.Size = New System.Drawing.Size(281, 186)
        Me.lstActivityLog.TabIndex = 35
        '
        'btnBlackScreen
        '
        Me.btnBlackScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnBlackScreen.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBlackScreen.ForeColor = System.Drawing.Color.Black
        Me.btnBlackScreen.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnBlackScreen.Location = New System.Drawing.Point(1, 553)
        Me.btnBlackScreen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBlackScreen.Name = "btnBlackScreen"
        Me.btnBlackScreen.Size = New System.Drawing.Size(281, 55)
        Me.btnBlackScreen.TabIndex = 40
        Me.btnBlackScreen.Text = "Black Screen"
        Me.btnBlackScreen.UseVisualStyleBackColor = False
        '
        'btnCaptureScreen
        '
        Me.btnCaptureScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnCaptureScreen.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCaptureScreen.ForeColor = System.Drawing.Color.Gold
        Me.btnCaptureScreen.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnCaptureScreen.Location = New System.Drawing.Point(0, 490)
        Me.btnCaptureScreen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCaptureScreen.Name = "btnCaptureScreen"
        Me.btnCaptureScreen.Size = New System.Drawing.Size(281, 55)
        Me.btnCaptureScreen.TabIndex = 39
        Me.btnCaptureScreen.Text = "Capture Screen"
        Me.btnCaptureScreen.UseVisualStyleBackColor = False
        '
        'btnOpenCalculator
        '
        Me.btnOpenCalculator.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnOpenCalculator.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenCalculator.ForeColor = System.Drawing.Color.Linen
        Me.btnOpenCalculator.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.cmd
        Me.btnOpenCalculator.Location = New System.Drawing.Point(1, 427)
        Me.btnOpenCalculator.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenCalculator.Name = "btnOpenCalculator"
        Me.btnOpenCalculator.Size = New System.Drawing.Size(281, 55)
        Me.btnOpenCalculator.TabIndex = 38
        Me.btnOpenCalculator.Text = "Open Calculator"
        Me.btnOpenCalculator.UseVisualStyleBackColor = False
        '
        'btnDeletePC
        '
        Me.btnDeletePC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnDeletePC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeletePC.ForeColor = System.Drawing.Color.Linen
        Me.btnDeletePC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.redstone
        Me.btnDeletePC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDeletePC.Location = New System.Drawing.Point(0, 301)
        Me.btnDeletePC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDeletePC.Name = "btnDeletePC"
        Me.btnDeletePC.Size = New System.Drawing.Size(281, 55)
        Me.btnDeletePC.TabIndex = 37
        Me.btnDeletePC.Text = "Delete PC"
        Me.btnDeletePC.UseVisualStyleBackColor = False
        '
        'btnEditPC
        '
        Me.btnEditPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnEditPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditPC.ForeColor = System.Drawing.Color.White
        Me.btnEditPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.lapiz
        Me.btnEditPC.Location = New System.Drawing.Point(1, 238)
        Me.btnEditPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditPC.Name = "btnEditPC"
        Me.btnEditPC.Size = New System.Drawing.Size(281, 55)
        Me.btnEditPC.TabIndex = 36
        Me.btnEditPC.Text = "Edit PC"
        Me.btnEditPC.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.White
        Me.txtName.Location = New System.Drawing.Point(0, 121)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(282, 34)
        Me.txtName.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 87)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 30)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "PC NAME:"
        '
        'btnAddPC
        '
        Me.btnAddPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnAddPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPC.ForeColor = System.Drawing.Color.White
        Me.btnAddPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.emerald
        Me.btnAddPC.Location = New System.Drawing.Point(1, 175)
        Me.btnAddPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAddPC.Name = "btnAddPC"
        Me.btnAddPC.Size = New System.Drawing.Size(281, 55)
        Me.btnAddPC.TabIndex = 35
        Me.btnAddPC.Text = "Add PC"
        Me.btnAddPC.UseVisualStyleBackColor = False
        '
        'txtIp
        '
        Me.txtIp.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.txtIp.Enabled = False
        Me.txtIp.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIp.ForeColor = System.Drawing.Color.White
        Me.txtIp.Location = New System.Drawing.Point(0, 38)
        Me.txtIp.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIp.Name = "txtIp"
        Me.txtIp.Size = New System.Drawing.Size(282, 34)
        Me.txtIp.TabIndex = 29
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 4)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 30)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "IP ADDRESS:"
        '
        'btnSendMsg
        '
        Me.btnSendMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnSendMsg.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendMsg.ForeColor = System.Drawing.Color.Linen
        Me.btnSendMsg.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnSendMsg.Location = New System.Drawing.Point(1, 364)
        Me.btnSendMsg.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSendMsg.Name = "btnSendMsg"
        Me.btnSendMsg.Size = New System.Drawing.Size(281, 55)
        Me.btnSendMsg.TabIndex = 3
        Me.btnSendMsg.Text = "Send a Message"
        Me.btnSendMsg.UseVisualStyleBackColor = False
        '
        'trvComputers
        '
        Me.trvComputers.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.trvComputers.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvComputers.ForeColor = System.Drawing.Color.White
        Me.trvComputers.HideSelection = False
        Me.trvComputers.ImageIndex = 0
        Me.trvComputers.ImageList = Me.ImageList1
        Me.trvComputers.LineColor = System.Drawing.Color.White
        Me.trvComputers.Location = New System.Drawing.Point(13, 0)
        Me.trvComputers.Margin = New System.Windows.Forms.Padding(4)
        Me.trvComputers.Name = "trvComputers"
        Me.trvComputers.SelectedImageIndex = 0
        Me.trvComputers.Size = New System.Drawing.Size(295, 284)
        Me.trvComputers.TabIndex = 33
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "computer.png")
        '
        'btnAbout
        '
        Me.btnAbout.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnAbout.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbout.ForeColor = System.Drawing.Color.Linen
        Me.btnAbout.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.oak
        Me.btnAbout.Location = New System.Drawing.Point(532, 13)
        Me.btnAbout.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(171, 55)
        Me.btnAbout.TabIndex = 12
        Me.btnAbout.Text = "About"
        Me.btnAbout.UseVisualStyleBackColor = False
        '
        'btnShutdownPC
        '
        Me.btnShutdownPC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShutdownPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnShutdownPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShutdownPC.ForeColor = System.Drawing.Color.Red
        Me.btnShutdownPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.iron
        Me.btnShutdownPC.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnShutdownPC.Location = New System.Drawing.Point(1117, 13)
        Me.btnShutdownPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnShutdownPC.Name = "btnShutdownPC"
        Me.btnShutdownPC.Size = New System.Drawing.Size(171, 55)
        Me.btnShutdownPC.TabIndex = 14
        Me.btnShutdownPC.Text = "SHUTDOWN PC"
        Me.btnShutdownPC.UseVisualStyleBackColor = False
        '
        'btnLockPC
        '
        Me.btnLockPC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLockPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnLockPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLockPC.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnLockPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.iron
        Me.btnLockPC.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLockPC.Location = New System.Drawing.Point(1475, 13)
        Me.btnLockPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLockPC.Name = "btnLockPC"
        Me.btnLockPC.Size = New System.Drawing.Size(171, 55)
        Me.btnLockPC.TabIndex = 18
        Me.btnLockPC.Text = "LOCK PC"
        Me.btnLockPC.UseVisualStyleBackColor = False
        '
        'btnReturn
        '
        Me.btnReturn.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnReturn.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReturn.ForeColor = System.Drawing.Color.Linen
        Me.btnReturn.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.oak
        Me.btnReturn.Location = New System.Drawing.Point(353, 13)
        Me.btnReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(171, 55)
        Me.btnReturn.TabIndex = 20
        Me.btnReturn.Text = "Return"
        Me.btnReturn.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("VT323", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.diamond
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label2.Location = New System.Drawing.Point(18, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(290, 55)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "WELCOME TO SERVER"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRestartPC
        '
        Me.btnRestartPC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRestartPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnRestartPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestartPC.ForeColor = System.Drawing.Color.OrangeRed
        Me.btnRestartPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.iron
        Me.btnRestartPC.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRestartPC.Location = New System.Drawing.Point(1296, 13)
        Me.btnRestartPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRestartPC.Name = "btnRestartPC"
        Me.btnRestartPC.Size = New System.Drawing.Size(171, 55)
        Me.btnRestartPC.TabIndex = 24
        Me.btnRestartPC.Text = "RESTART PC"
        Me.btnRestartPC.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnRefresh.BackgroundImage = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnRefresh.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.Linen
        Me.btnRefresh.Location = New System.Drawing.Point(580, 13)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(171, 55)
        Me.btnRefresh.TabIndex = 26
        Me.btnRefresh.Text = "Refresh PCs"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnLayout
        '
        Me.btnLayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLayout.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnLayout.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLayout.ForeColor = System.Drawing.Color.Linen
        Me.btnLayout.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.stone
        Me.btnLayout.Location = New System.Drawing.Point(759, 13)
        Me.btnLayout.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLayout.Name = "btnLayout"
        Me.btnLayout.Size = New System.Drawing.Size(171, 55)
        Me.btnLayout.TabIndex = 28
        Me.btnLayout.Text = "Layout (5)"
        Me.btnLayout.UseVisualStyleBackColor = False
        '
        'btnControlPC
        '
        Me.btnControlPC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnControlPC.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnControlPC.Font = New System.Drawing.Font("VT323", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnControlPC.ForeColor = System.Drawing.Color.DarkOrchid
        Me.btnControlPC.Image = Global.ServerDemo___Liquiran.My.Resources.Resources.iron
        Me.btnControlPC.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnControlPC.Location = New System.Drawing.Point(938, 13)
        Me.btnControlPC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnControlPC.Name = "btnControlPC"
        Me.btnControlPC.Size = New System.Drawing.Size(171, 55)
        Me.btnControlPC.TabIndex = 30
        Me.btnControlPC.Text = "CONTROL PC"
        Me.btnControlPC.UseVisualStyleBackColor = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "ALCANTARA Server"
        Me.NotifyIcon1.Visible = True
        '
        'MDIParent1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.ServerDemo___Liquiran.My.Resources.Resources.mc_planks1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1659, 1022)
        Me.Controls.Add(Me.btnControlPC)
        Me.Controls.Add(Me.btnLayout)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnRestartPC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnLockPC)
        Me.Controls.Add(Me.btnShutdownPC)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip)
        Me.ForeColor = System.Drawing.Color.White
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "MDIParent1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ALCANTARA SERVER-CLIENT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents Panel1 As Panel
    Friend WithEvents trvComputers As TreeView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnSendMsg As Button
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtIp As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnAbout As Button
    Friend WithEvents btnShutdownPC As Button
    Friend WithEvents btnLockPC As Button
    Friend WithEvents btnReturn As Button
    Friend WithEvents btnAddPC As Button
    Friend WithEvents btnDeletePC As Button
    Friend WithEvents btnEditPC As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btnRestartPC As Button
    Friend WithEvents btnOpenCalculator As Button
    Friend WithEvents btnCaptureScreen As Button
    Friend WithEvents btnBlackScreen As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnShareScreen As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lstActivityLog As ListBox
    Friend WithEvents btnLayout As Button
    Friend WithEvents btnControlPC As Button
    Friend WithEvents btnStopShareScreen As Button
    Friend WithEvents btnHide As Button
    Friend WithEvents NotifyIcon1 As NotifyIcon
End Class
