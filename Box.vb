Public Class Box
    Public WithEvents btn As New Button
    Public frame As New Label

    Public id As Integer
    Public minesAround As Integer
    Public isOpen As Boolean
    Public isMine As Boolean
    Public isFlaggedForMine As Boolean

    Public Sub New()

    End Sub

    Private Sub Box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        isOpen = True
        If Not Form1.Timer1.Enabled Then
            Form1.Timer1.Enabled = True
        End If
        If (isFlaggedForMine) Or (mineBoxes.isGameOver) Then
            'btn.Enabled = False
            Exit Sub
        End If
        If (isMine) Then
            'btn.BackColor = Drawing.Color.Red
            openAllMines()
            mineBoxes.isGameOver = True
            Form1.Timer1.Enabled = False
            MsgBox("You Lost!! Click on Reset to play again!!")
            Exit Sub
        ElseIf (minesAround = 0) Then
            'btn.FlatStyle = FlatStyle.Popup
            'btn.Enabled = False
            'btn.Text = minesAround
            mineBoxes.openBox(id)
            mineBoxes.openPossibleBoxes(id)
        Else
            mineBoxes.openBox(id)
            'btn.FlatStyle = FlatStyle.Popup
            'btn.Enabled = False
            'btn.Text = minesAround
        End If
        findIfAllMinesFound()
    End Sub
    Private Sub Box_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn.MouseDown
        If (e.Button = MouseButtons.Right) Then
            'btn.BackColor = Drawing.Color.Purple
            If (isFlaggedForMine = False) Then
                'btn.Image = System.Drawing.Bitmap.FromFile("C:\Documents and Settings\VijayaShankar_Konam\My Documents\Visual Studio Projects\MineSweeper\Images\Flag.ico")
                btn.Image = System.Drawing.Bitmap.FromFile("Flag.ico")
                isFlaggedForMine = True
                mineBoxes.flaggedMineCount = mineBoxes.flaggedMineCount + 1
                Form1.lblMinesRemainig.Text = (Form1.lblMinesRemainig.Text) - 1
            Else
                btn.Image = Nothing
                isFlaggedForMine = False
                mineBoxes.flaggedMineCount = mineBoxes.flaggedMineCount - 1
                Form1.lblMinesRemainig.Text = (Form1.lblMinesRemainig.Text) + 1
            End If
        End If
    End Sub
    'Private Sub btn_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn.MouseMove
    '    If (e.Button = MouseButtons.Left And MouseButtons.Right) Then  ' = MouseButtons.Left) And (e.Button = MouseButtons.Right) Then
    '        MsgBox("Hi")
    '    End If
    'End Sub

    Private Sub findIfAllMinesFound()
        Dim i As Integer
        Dim flag As Boolean = True

        'If (mineBoxes.flaggedMineCount = mineBoxes.noOfMines) Then
        For i = 1 To mineBoxes.Count
            If (mineBoxes.mineBox(i).isMine = False) And _
            (mineBoxes.mineBox(i).isOpen <> True) Then
                Exit Sub
            End If
        Next i
        'If (flag) Then
        Form1.Timer1.Enabled = False
        MsgBox("Congratulations!! You Have sucessfully found all the mines!!")
        mineBoxes.findIfBestTime()
        'End If
        ' End If
    End Sub
    Private Sub openAllMines()
        Dim i As Integer
        For i = 1 To mineBoxes.Count
            If (mineBoxes.mineBox(i).isMine) And (Not mineBoxes.mineBox(i).isFlaggedForMine) Then
                'mineBoxes.mineBox(i).btn.BackColor = System.Drawing.Color.Red
                mineBoxes.mineBox(i).btn.Image = System.Drawing.Image.FromFile("Mine.ico")
            ElseIf (Not mineBoxes.mineBox(i).isMine) And (mineBoxes.mineBox(i).isFlaggedForMine) Then
                'mineBoxes.mineBox(i).btn.BackColor = System.Drawing.Color.Purple
                mineBoxes.mineBox(i).btn.Image = System.Drawing.Image.FromFile("WrongMine.ico")
            End If
        Next
        mineBoxes.mineBox(id).btn.Image = System.Drawing.Image.FromFile("RedMine.ico")
    End Sub


End Class
