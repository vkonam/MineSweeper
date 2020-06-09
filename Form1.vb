Public Class Form1
    Inherits System.Windows.Forms.Form

    'Public Shared WithEvents btnReset As New Button
    Public Shared mines As New mineBoxes(9, 10)
    Shared timerCount As Double
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grpBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grpBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents GameMenu As System.Windows.Forms.MainMenu
    Friend WithEvents mnuBeginner As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExpert As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    'My Code
    Friend Shared WithEvents lblMinesRemainig As New System.Windows.Forms.Label
    Friend Shared WithEvents lblTimer As New System.Windows.Forms.Label
    Friend Shared WithEvents Timer1 As New System.Windows.Forms.Timer
    Friend WithEvents mnuHighScores As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.grpBox2 = New System.Windows.Forms.GroupBox
        Me.grpBox1 = New System.Windows.Forms.GroupBox
        Me.btnReset = New System.Windows.Forms.Button
        Me.GameMenu = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuBeginner = New System.Windows.Forms.MenuItem
        Me.mnuInter = New System.Windows.Forms.MenuItem
        Me.mnuExpert = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mnuHighScores = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.grpBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBox2
        '
        Me.grpBox2.Location = New System.Drawing.Point(8, 104)
        Me.grpBox2.Name = "grpBox2"
        Me.grpBox2.Size = New System.Drawing.Size(320, 272)
        Me.grpBox2.TabIndex = 1
        Me.grpBox2.TabStop = False
        '
        'grpBox1
        '
        Me.grpBox1.Controls.Add(Me.btnReset)
        Me.grpBox1.Location = New System.Drawing.Point(8, 8)
        Me.grpBox1.Name = "grpBox1"
        Me.grpBox1.Size = New System.Drawing.Size(320, 80)
        Me.grpBox1.TabIndex = 2
        Me.grpBox1.TabStop = False
        '
        'btnReset
        '
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.Location = New System.Drawing.Point(136, 24)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(24, 24)
        Me.btnReset.TabIndex = 0
        '
        'GameMenu
        '
        Me.GameMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuBeginner, Me.mnuInter, Me.mnuExpert, Me.MenuItem2, Me.mnuHighScores, Me.MenuItem4, Me.MenuItem3})
        Me.MenuItem1.Text = "&Game"
        '
        'mnuBeginner
        '
        Me.mnuBeginner.Index = 0
        Me.mnuBeginner.RadioCheck = True
        Me.mnuBeginner.Text = "&Beginner"
        '
        'mnuInter
        '
        Me.mnuInter.Index = 1
        Me.mnuInter.RadioCheck = True
        Me.mnuInter.Text = "&Intermediate"
        '
        'mnuExpert
        '
        Me.mnuExpert.Index = 2
        Me.mnuExpert.RadioCheck = True
        Me.mnuExpert.Text = "&Expert"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 6
        Me.MenuItem3.Text = "E&xit"
        '
        'mnuHighScores
        '
        Me.mnuHighScores.Index = 4
        Me.mnuHighScores.Text = "&View High Scores"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 5
        Me.MenuItem4.Text = "-"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(336, 361)
        Me.Controls.Add(Me.grpBox1)
        Me.Controls.Add(Me.grpBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Menu = Me.GameMenu
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MineSweeper By Vijay"
        Me.grpBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mnuBeginner.Checked = True

        'Check for registry keys existance..
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\AjanthaDigitals\\MineSweeper", True)
        If regKey Is Nothing Then
            regKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\AjanthaDigitals\\MineSweeper")
            regKey.SetValue("Beginner", 1000)
            regKey.SetValue("Intermediate", 1000)
            regKey.SetValue("Expert", 5000)
            regKey.SetValue("BeginnerName", "None")
            regKey.SetValue("IntermediateName", "None")
            regKey.SetValue("ExpertName", "None")
        End If
        mineSweeperLoad()
    End Sub
    Public Sub mineSweeperLoad()
        mines.initialize()
        mines.fillMines()
        mines.fillMineCounts()
        Me.btnReset.Select()
        'Controls.Remove(btnReset)

        Timer1.Interval = 1000
        Timer1.Enabled = False
        timerCount = 0

        Dim i As Integer
        grpBox2.Top = BOX_HEIGHT * 2
        grpBox2.Left = BOX_WIDTH
        grpBox2.Width = mineBoxes.countPerRow * BOX_WIDTH + BOX_WIDTH
        grpBox2.Height = mineBoxes.countPerRow * BOX_HEIGHT + BOX_HEIGHT

        grpBox1.Left = BOX_WIDTH
        grpBox1.Top = 0 'BOX_HEIGHT
        grpBox1.Width = grpBox2.Width
        grpBox1.Height = 2 * BOX_HEIGHT

        'grpBox1.Controls.Add(btnReset)
        btnReset.Left = (grpBox1.Width - BOX_WIDTH) / 2
        btnReset.Top = (grpBox1.Height - BOX_HEIGHT) / 2
        'btnReset.Width = BOX_WIDTH*2
        With lblMinesRemainig
            .Left = BOX_WIDTH / 2
            .Top = BOX_HEIGHT / 2
            .Width = 50
            .Height = 25
            .BorderStyle = BorderStyle.FixedSingle
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New System.Drawing.Font("Verdana", 10, FontStyle.Bold, GraphicsUnit.Point)
            .ForeColor = System.Drawing.Color.Red
        End With
        grpBox1.Controls.Add(lblMinesRemainig)
        lblMinesRemainig.Text = mines.noOfMines

        With lblTimer
            .Left = mines.countPerRow * BOX_WIDTH - BOX_WIDTH * 2
            .Top = 10
            .Width = 50
            .Height = 25
            .BorderStyle = BorderStyle.FixedSingle
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New System.Drawing.Font("Verdana", 10, FontStyle.Bold, GraphicsUnit.Point)
            .ForeColor = System.Drawing.Color.Red
        End With
        grpBox1.Controls.Add(lblTimer)
        lblTimer.Text = 0

        'btnReset.BringToFront()
        For i = 1 To mineBoxes.Count
            grpBox2.Controls.Add(mines.mineBox(i).btn)
            grpBox2.Controls.Add(mines.mineBox(i).frame)
        Next i

        Me.Width = grpBox2.Width + 2 * BOX_WIDTH
        Me.Height = grpBox1.Height + grpBox2.Height + 3 * BOX_HEIGHT

    End Sub
    'Public Sub openPossibleBoxes(ByRef mineBox As Box(), ByVal i As Integer)

    '    If ((i + 1) > 0 And (i + 1) <= Count) Then
    '        If mineBox(i + 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        Else
    '            openPossibleBoxes(mineBox, i)
    '        End If
    '    End If

    '    If ((i - 1) > 0 And (i - 1) <= Count) Then
    '        If mineBox(i - 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    If ((i + countPerRow) > 0 And (i + Box.countPerRow) <= Count) Then
    '        If mineBox(i + Box.countPerRow).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    If ((i - Box.countPerRow) > 0 And (i - Box.countPerRow) <= Count) Then
    '        If mineBox(i - Box.countPerRow).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    If ((i + Box.countPerRow + 1) > 0 And (i + Box.countPerRow + 1) <= Count) Then
    '        If mineBox(i + Box.countPerRow + 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    If ((i + Box.countPerRow - 1) > 0 And (i + Box.countPerRow - 1) <= Count) Then
    '        If mineBox(i + Box.countPerRow - 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    If ((i - Box.countPerRow + 1) > 0 And (i - Box.countPerRow + 1) <= Count) Then
    '        If mineBox(i - Box.countPerRow + 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If
    '    If ((i - Box.countPerRow - 1) > 0 And (i - Box.countPerRow - 1) <= Count) Then
    '        If mineBox(i - Box.countPerRow - 1).minesAround = 0 Then
    '            mineBox(i).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(i).btn.Enabled = False
    '        End If
    '    End If

    '    'mineBox(i).btn.Text = count

    'End Sub
    Private Sub btnReset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        grpBox2.Controls.Clear()
        mineSweeperLoad()
        grpBox1.Controls.Add(btnReset)
    End Sub

    Private Sub mnuBeginner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBeginner.Click
        mnuBeginner.Checked = True
        mnuInter.Checked = False
        mnuExpert.Checked = False
        grpBox2.Controls.Clear()
        mines = New mineBoxes(9, 10)

        mineSweeperLoad()
    End Sub

    Private Sub mnuInter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInter.Click
        mnuBeginner.Checked = False
        mnuInter.Checked = True
        mnuExpert.Checked = False
        grpBox2.Controls.Clear()
        mines = New mineBoxes(16, 40)
        mineSweeperLoad()
    End Sub

    Private Sub mnuExpert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExpert.Click
        mnuBeginner.Checked = False
        mnuInter.Checked = False
        mnuExpert.Checked = True
        grpBox2.Controls.Clear()
        mines = New mineBoxes(25, 125)
        mineSweeperLoad()
    End Sub
    Private Shared Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTimer.Text = Form1.timerCount
        Form1.timerCount = Form1.timerCount + 1
    End Sub

    
    Private Sub mnuHighScores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHighScores.Click
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\AjanthaDigitals\\MineSweeper", True)
        MsgBox("Beginner - " & regKey.GetValue("BeginnerName", 0) & " - " & regKey.GetValue("Beginner", 0) & vbCrLf & _
        "Intermediate - " & regKey.GetValue("IntermediateName", 0) & " - " & regKey.GetValue("Intermediate", 0) & vbCrLf & _
        "Expert - " & regKey.GetValue("ExpertName", 0) & " - " & regKey.GetValue("Expert", 0))

    End Sub
End Class
