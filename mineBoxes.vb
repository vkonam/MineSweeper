Public Class mineBoxes
    Public Shared noOfMines As Integer
    Public Shared Count As Integer
    Public Shared countPerRow As Integer
    Public Shared isGameOver As Boolean

    Friend Shared mineBox() As Box
    Friend Shared flaggedMineCount As Integer

    Sub New(ByVal countPerRow As Integer, ByVal noOfMines As Integer)
        mineBoxes.countPerRow = countPerRow
        mineBoxes.Count = countPerRow * countPerRow
        mineBoxes.noOfMines = noOfMines

        'Declare a temp array for dynamic creation
        'use this to initialize the actual minebox array
        Dim temp(mineBoxes.Count) As Box
        mineBox = temp
        'MsgBox(mineBox.Length())
        initialize()
    End Sub
    Public Shared Sub initialize()

        isGameOver = False

        Dim left As Integer = BOX_WIDTH / 2
        Dim top As Integer = BOX_HEIGHT / 2
        flaggedMineCount = 0

        Dim i As Integer = 0
        For i = 1 To Count
            mineBox(i) = New Box
            With mineBox(i).btn
                .Visible = True
                .Left = left
                .Top = top
                .Width = BOX_WIDTH
                .Height = BOX_HEIGHT
                .TabIndex = i
                .TabStop = False
                .ImageAlign = ContentAlignment.MiddleCenter
            End With

            'mineBox(i).frame = New Label
            With mineBox(i).frame
                .Visible = False
                .BorderStyle = BorderStyle.FixedSingle
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = New System.Drawing.Font("Verdana", 10, FontStyle.Bold, GraphicsUnit.Point)
                .Left = left
                .Top = top
                .Width = BOX_WIDTH
                .Height = BOX_HEIGHT
                .BackColor = System.Drawing.Color.Gray
                '.TabIndex = i
                .TabStop = False
            End With

            mineBox(i).id = i
            mineBox(i).isMine = False
            mineBox(i).isOpen = False
            mineBox(i).minesAround = 0
            mineBox(i).isFlaggedForMine = False

            If (i Mod countPerRow) = 0 Then
                left = BOX_WIDTH / 2
                top = top + BOX_HEIGHT
            Else
                left = left + BOX_WIDTH
            End If
        Next i
    End Sub
    Public Shared Sub fillMines()
        Randomize() ' Initialize random-number generator.
        Dim i As Integer
        For i = 1 To noOfMines
            Dim temp As Integer
            temp = CInt(Int((Count * Rnd()) + 1))
            'mineBox(temp).btn.Text = "A" ' Generate random value between 1 and Count.
            mineBox(temp).isMine = True
            'mineBox(temp).btn.BackColor = System.Drawing.Color.Red
        Next i

        'Random might give you duplicate values
        'Count for actual number of mine
        Dim actualMineCount As Integer = 0
        For i = 1 To Count
            If (mineBox(i).isMine = True) Then
                actualMineCount = actualMineCount + 1
            End If
        Next i
        'Now fill the mines as per the deficiency
        'If (actualMineCount < noOfMines) Then
        While (actualMineCount < noOfMines)
            'For i = 1 To noOfMines
            Dim temp As Integer
            temp = CInt(Int((Count * Rnd()) + 1))
            If (mineBox(temp).isMine = False) Then
                mineBox(temp).isMine = True
                'mineBox(temp).btn.BackColor = System.Drawing.Color.Red
                actualMineCount = actualMineCount + 1
                'If (actualMineCount = noOfMines) Then
                'Exit For
                'End If
            End If
            'Next i
        End While
        'End If

    End Sub
    Public Shared Sub fillMineCounts()
        Dim i As Integer
        For i = 1 To Count
            If (mineBox(i).isMine <> True) Then
                Dim mineCount As Integer = 0
                If ((i + 1) > 0 And (i + 1) <= Count) Then
                    If ((i + 1) Mod countPerRow <> 1) Then
                        If mineBox(i + 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If

                If ((i - 1) > 0 And (i - 1) <= Count) Then
                    If ((i - 1) Mod countPerRow <> 0) Then
                        If mineBox(i - 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If

                If ((i + countPerRow) > 0 And (i + countPerRow) <= Count) Then
                    If mineBox(i + countPerRow).isMine = True Then
                        mineCount = mineCount + 1
                    End If
                End If

                If ((i - countPerRow) > 0 And (i - countPerRow) <= Count) Then
                    If mineBox(i - countPerRow).isMine = True Then
                        mineCount = mineCount + 1
                    End If
                End If

                If ((i + countPerRow + 1) > 0 And (i + countPerRow + 1) <= Count) Then
                    If ((i + countPerRow + 1) Mod countPerRow <> 1) Then
                        If mineBox(i + countPerRow + 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If

                If ((i + countPerRow - 1) > 0 And (i + countPerRow - 1) <= Count) Then
                    If ((i + countPerRow - 1) Mod countPerRow <> 0) Then
                        If mineBox(i + countPerRow - 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If

                If ((i - countPerRow + 1) > 0 And (i - countPerRow + 1) <= Count) Then
                    If ((i - countPerRow + 1) Mod countPerRow <> 1) Then
                        If mineBox(i - countPerRow + 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If
                If ((i - countPerRow - 1) > 0 And (i - countPerRow - 1) <= Count) Then
                    If ((i - countPerRow - 1) Mod countPerRow <> 0) Then
                        If mineBox(i - countPerRow - 1).isMine = True Then
                            mineCount = mineCount + 1
                        End If
                    End If
                End If
                mineBox(i).minesAround = mineCount
                'mineBox(i).btn.Text = mineCount
            End If
        Next
    End Sub
    Public Shared Sub openPossibleBoxes(ByVal i As Integer)
        Dim j As Integer

        j = i - countPerRow - 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 0) Then
                If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If
        j = i - countPerRow
        If ((j) > 0 And (j) <= Count) Then
            If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                openBox(j)
                If mineBox(j).minesAround = 0 Then
                    openPossibleBoxes(j)
                End If
            End If
        End If
        j = i - countPerRow + 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 1) Then
                If mineBox(j).isOpen = False Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If
        j = i - 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 0) Then
                If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If

        j = i + 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 1) Then
                If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If

        j = i + countPerRow - 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 0) Then
                If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If

        j = i + countPerRow
        If ((j) > 0 And (j) <= Count) Then
            If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                openBox(j)
                If mineBox(j).minesAround = 0 Then
                    openPossibleBoxes(j)
                End If
            End If
        End If

        j = i + countPerRow + 1
        If ((j) > 0 And (j) <= Count) Then
            If ((j) Mod countPerRow <> 1) Then
                If mineBox(j).isOpen = False And Not mineBox(j).isFlaggedForMine Then
                    openBox(j)
                    If mineBox(j).minesAround = 0 Then
                        openPossibleBoxes(j)
                    End If
                End If
            End If
        End If
    End Sub
    Public Shared Sub openBox(ByVal j As Integer)
        mineBox(j).isOpen = True
        'mineBox(j).btn.FlatStyle = FlatStyle.Popup
        'mineBox(j).btn.Enabled = False
        mineBox(j).btn.Visible = False
        mineBox(j).frame.Visible = True
        If (mineBox(j).minesAround > 0) Then
            'mineBox(j).btn.Text = mineBox(j).minesAround
            mineBox(j).frame.Text = mineBox(j).minesAround
        End If
    End Sub
    Public Shared Sub findIfBestTime()
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\AjanthaDigitals\\MineSweeper", True)
        Dim recTime As Integer = 0
        Dim newRecPlayer As String
        Select Case mineBoxes.Count
            Case 81
                recTime = regKey.GetValue("Beginner", 0)
                If (recTime > CInt(Form1.lblTimer.Text)) Then
                    recTime = CInt(Form1.lblTimer.Text)
                    newRecPlayer = InputBox("Congratulations again!! You have completed the game in the shortest span of time.. Please enter your name..", "MineSweeper New record")
                    If (newRecPlayer = "") Then
                        MsgBox("Please enter your name")
                        findIfBestTime()
                        Exit Sub
                    End If
                    Try
                        regKey.SetValue("Beginner", recTime)
                        regKey.SetValue("BeginnerName", newRecPlayer)
                        MsgBox("Thankyou!!")
                    Catch e As Exception
                        MsgBox("Error.. Please Report to Vijay!!")
                    End Try
                End If
            Case 256
                    recTime = regKey.GetValue("Intermediate", 0)
                    If (recTime > CInt(Form1.lblTimer.Text)) Then
                        recTime = CInt(Form1.lblTimer.Text)
                        newRecPlayer = InputBox("Congratulations again!! You have completed the game in the shortest span of time.. Please enter your name..", "MineSweeper New record")
                    If (newRecPlayer = "") Then
                        MsgBox("Please enter your name")
                        findIfBestTime()
                        Exit Sub
                    End If
                    Try
                        regKey.SetValue("Intermediate", recTime)
                        regKey.SetValue("IntermediateName", newRecPlayer)
                        MsgBox("Thankyou!!")
                    Catch e As Exception
                        MsgBox("Error.. Please Report to Vijay!!")
                    End Try
                End If
            Case 625
                    recTime = regKey.GetValue("Expert", 0)
                    If (recTime > CInt(Form1.lblTimer.Text)) Then
                        recTime = CInt(Form1.lblTimer.Text)
                        newRecPlayer = InputBox("Congratulations again!! You have completed the game in the shortest span of time.. Please enter your name..", "MineSweeper New record")
                    If (newRecPlayer = "") Then
                        MsgBox("Please enter your name")
                        findIfBestTime()
                        Exit Sub
                    End If
                    Try
                        regKey.SetValue("Expert", recTime)
                        regKey.SetValue("ExpertName", newRecPlayer)
                        MsgBox("Thankyou!!")
                    Catch e As Exception
                        MsgBox("Error.. Please Report to Vijay!!")
                    End Try
                End If
        End Select
    End Sub
    'Private Shared Sub openThe8Around(ByVal i As Integer)
    '    Dim j As Integer

    '    j = i - countPerRow - 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        If (j Mod countPerRow <> 0) Then
    '            mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(j).btn.Text = mineBox(j).minesAround
    '            mineBox(j).btn.Enabled = False
    '            mineBox(j).isOpen = True
    '        End If
    '    End If

    '    j = i - countPerRow
    '    If ((j) > 0 And (j) <= Count) Then
    '        mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '        mineBox(j).btn.Text = mineBox(j).minesAround
    '        mineBox(j).btn.Enabled = False
    '        mineBox(j).isOpen = True
    '    End If

    '    j = i - countPerRow + 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        If (j Mod countPerRow <> 0) Then
    '            mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(j).btn.Text = mineBox(j).minesAround
    '            mineBox(j).btn.Enabled = False
    '            mineBox(j).isOpen = True
    '        End If
    '    End If

    '    j = i - 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        If (j Mod countPerRow <> 0) Then
    '            mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(j).btn.Text = mineBox(j).minesAround
    '            mineBox(j).btn.Enabled = False
    '            mineBox(j).isOpen = True
    '        End If
    '    End If

    '    j = i + 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        If (j Mod countPerRow <> 1) Then
    '            mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(j).btn.Text = mineBox(j).minesAround
    '            mineBox(j).btn.Enabled = False
    '            mineBox(j).isOpen = True
    '        End If
    '    End If

    '    j = i + countPerRow - 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        If (j Mod countPerRow <> 0) Then
    '            mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '            mineBox(j).btn.Text = mineBox(j).minesAround
    '            mineBox(j).btn.Enabled = False
    '            mineBox(j).isOpen = True
    '        End If
    '    End If

    '    j = i + countPerRow
    '    If ((j) > 0 And (j) <= Count) Then
    '        mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '        mineBox(j).btn.Text = mineBox(j).minesAround
    '        mineBox(j).btn.Enabled = False
    '        mineBox(j).isOpen = True
    '    End If

    '    j = i + countPerRow + 1
    '    If ((j) > 0 And (j) <= Count) Then
    '        mineBox(j).btn.FlatStyle = FlatStyle.Popup
    '        mineBox(j).btn.Text = mineBox(j).minesAround
    '        mineBox(j).btn.Enabled = False
    '        mineBox(j).isOpen = True
    '    End If
    'End Sub
End Class
