Option Strict On
Option Explicit On
Option Infer Off
Public Class Form1
    Dim maze As Maze 'declaring global variable that will be used throughout Form1
    Dim total As Integer
    Dim moveValue As Integer
    Const imove As Integer = 10 'variablesconstant value is 10 as it is the move value
    Dim m_MoveCount As Integer = 0
    Dim m_MoveDirection As Maze.MoveVector = Maze.MoveVector.Right
    Dim N As Integer = 0

    Sub dolayout()
        Panel2.Top = 0 'sets up panel2 inner panel on user form
        Panel2.Left = 0
        Panel2.Height = Me.ClientRectangle.Height - Panel2.Top 'sets height of maze based on dimentions inputed in to NumericalUpDown boxes on top of form 
        Panel2.Width = Me.ClientRectangle.Width 'sets width of maze based on dimentions inputed in to NumericalUpDown boxes on top of form 
        Panel2.BorderStyle = BorderStyle.None
    End Sub
    Private Sub Form1_load(sender As Object, e As EventArgs) Handles MyBase.Load
        dolayout() 'calles sub procedure dolayout
    End Sub
    Private Sub Form_resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        dolayout()  'calles sub procedure dolayout
    End Sub
    Private Sub btnLevel1_Click(sender As Object, e As EventArgs) Handles btnLevel1.Click
        Column.Value = 10 'sets level1 as 50 x 50 and with 10 cloumns and 10 rows
        Width.Value = 10
        PixelH.Value = 50
        PixelW.Value = 50
        PbCharacter.Size = New Size(30, 30) 'sets size of character to be 30 x 30 
        moveValue = 2010 'sets move limit to 201 moves as iMove is 10
    End Sub
    Private Sub btnLevle2_Click(sender As Object, e As EventArgs) Handles btnLevle2.Click
        Column.Value = 20 'sets level2 as 29 x 29 and with 20 columns and 20 rows 
        Width.Value = 20
        PixelH.Value = 29
        PixelW.Value = 29
        PbCharacter.Size = New Size(15, 15) 'sets size of character to be 20 x 20
        moveValue = 4010 'sets move limit to 401 moves as iMove is 10
    End Sub

    Private Sub btnLevle3_Click(sender As Object, e As EventArgs) Handles btnLevle3.Click
        Column.Value = 42 'sets level3 as 15 x 15 and with 42 columns and 42 rows
        Width.Value = 42
        PixelH.Value = 15
        PixelW.Value = 15
        PbCharacter.Size = New Size(6, 6) ' sets size of character to 6 x 6 
        moveValue = 8010 'sets move limit to 801 moves as iMove is 10
    End Sub

    Private Sub BtnGenerate_click(sender As Object, e As EventArgs) Handles BtnGenerate.Click
        'stops the user from moving the character with out a maze being present
        If Column.Value = 0 Then
        Else
            N = 1
            Panel1.Visible = True
        End If

        'sets the move limit to each level depending on the value in column
        If Column.Value = 10 Then 'if column equals 10 then number of moves is 210
            moveValue = 2010
        ElseIf Column.Value = 20 Then 'if column equals 20 then number of moves is 410
            moveValue = 4010
        ElseIf Column.Value = 42 Then 'if column equals 42 then number of moves is 810
            moveValue = 8010
        End If

        Label6.Text = ""
        If Column.Value = 0 Or Width.Value = 0 Or PixelH.Value = 0 Or PixelW.Value = 0 Then 'stops maze being generated if dimentions are zreo
            MessageBox.Show("Error, maze can't generate with value 0f zero")
            Column.Value = 0
            Width.Value = 0 'sets the value of width to stay at zero if already zero
            PixelW.Value = 0
            PixelH.Value = 0
        Else
            maze = New Maze(CInt(Column.Value), CInt(Width.Value), CInt(PixelH.Value), CInt(PixelW.Value)) 'new maze equals values in all four NumericalUpDown boxes 
            AddHandler maze.MazeComplete, Sub(m As Image)
                                              Panel1.BackgroundImage = m 'sets backgroundImage for Panel1 
                                              Panel1.BackgroundImageLayout = ImageLayout.None
                                              Panel1.Width = m.Width 'sets width of Panel1 to be the value of m.Width
                                              Panel1.Height = m.Height
                                              PbCharacter.Location = New Point(1, 1) 'starts character location as 1,1 once maze is generated 
                                          End Sub
            maze.Generate()
        End If

        'sets the finish point of the maze depending on the level selected
        If Column.Value = 10 And Width.Value = 10 Then 'sets finish point of level1 to 460,460
            lblFinish.Location = New Point(460, 460)
        ElseIf Column.Value = 20 And Width.Value = 20 Then 'level2 sets finish point to 550,550
            lblFinish.Location = New Point(550, 550)
        ElseIf Column.Value = 42 And Width.Value = 42 Then 'level3 sets finish point to 600,600
            lblFinish.Location = New Point(600, 600)
        End If

    End Sub
    Public Sub btnMove_KeyDown(sender As Object, e As KeyEventArgs) Handles btnMove.KeyDown
        Dim MoveDirection As Maze.MoveVector = Maze.MoveVector.None 'declars moveVector corner of charaqcter used to validate move
        Dim strErrMsg As String = ""

        'allows the user to move PbCharacter using W,A,S,D moves character wehn key is pressed
        Select Case e.KeyCode
            Case Keys.W
                MoveDirection = Maze.MoveVector.Up 'case if W is pressed down move character up by 10 and takes 10 from moveValue
                moveValue = moveValue + total - imove
            Case Keys.S
                MoveDirection = Maze.MoveVector.Down 'case if S is pressed down move character left by 10 and takes 10 from moveValue
                moveValue = moveValue + total - imove
            Case Keys.A
                MoveDirection = Maze.MoveVector.Left 'case if A is pressed down move character left by 10 and takes 10 from moveValue
                moveValue = moveValue + total - imove
            Case Keys.D
                MoveDirection = Maze.MoveVector.Right 'case if D is pressed down move character left by 10 and takes 10 from moveValue
                moveValue = moveValue + total - imove
        End Select
        Label6.Text = CType(moveValue, String)

        If moveValue = 0 Then 'if maze is not completed in moves given
            MsgBox("Didn't complete the maze in the set moves")
            'asks user if they want to restart the maze or quit
            Dim ExitMaze As Integer = MessageBox.Show("Would you like to restert the maze", "", MessageBoxButtons.YesNo)
            If ExitMaze = DialogResult.Yes Then
                'if yes is picked the maze will resart
                MessageBox.Show("Maze will restart")
                If Column.Value = 10 Then 'if column equals 10 then number of moves is 210
                    moveValue = 2010
                ElseIf Column.Value = 20 Then 'if column equals 20 then number of moves is 410
                    moveValue = 4010
                ElseIf Column.Value = 42 Then 'if column equals 42 then number of moves is 810
                    moveValue = 8010
                End If
                PbCharacter.Location = New Point(1, 1)
                Exit Sub
            ElseIf ExitMaze = DialogResult.No Then
                'if no is clicked the form will close
                MessageBox.Show("Hope you enjoyed the game!")
                Close()
            End If

        End If


        'statment for allowing for line witdh vs Man position
        If Not maze.VerifyLimits(PbCharacter.Left, PbCharacter.Top, PbCharacter.Width, PbCharacter.Height, MoveDirection, imove) Then
            Beep()
        ElseIf Not maze.VerifyMove(PbCharacter.Left, PbCharacter.Top, PbCharacter.Width, PbCharacter.Height, MoveDirection, imove, strErrMsg) Then
            Beep()
        Else
            'moving the character from cell to cell by using a point on the character
            Select Case MoveDirection
                Case Maze.MoveVector.Left
                    PbCharacter.Left -= imove 'going left will decrease the vaule its position on the vertiacl axis
                Case Maze.MoveVector.Right
                    PbCharacter.Left += imove 'going Right will increase the vaule its position on the vertiacl axis
                Case Maze.MoveVector.Up
                    PbCharacter.Top -= imove 'going Up will decrease the vaule its position on the horizontal axis
                Case Maze.MoveVector.Down
                    PbCharacter.Top += imove 'going left will decrease the vaule its position on the vertiacl axis
            End Select
        End If

        If moveValue = 0 And Column.Value = 10 Then
            moveValue = 2000
        ElseIf moveValue = 0 And Column.Value = 20 Then
            moveValue = 4000
        ElseIf moveValue = 0 And Column.Value = 42 Then
            moveValue = 8000
        End If

        'messagebox displayed when the user reaches the end of the maze 
        If (PbCharacter.Bounds.IntersectsWith(lblFinish.Bounds)) Then 'check pbCharacter reaches the finish point
            MessageBox.Show("Well Done on completing the maze")
            Dim PlayAgain As Integer = MessageBox.Show("would you like to play again", "", MessageBoxButtons.YesNo)
            If PlayAgain = DialogResult.Yes Then 'yes to continue playing the game
                MsgBox("please click Generate to play again")
                Panel1.Visible = False
            ElseIf PlayAgain = DialogResult.No Then 'no to end the game and close form
                MsgBox("Hope you enjoyed the game!")
                Close()
                End If
            End If


    End Sub

    Private Sub Column_ValueChanged(sender As Object, e As EventArgs) Handles Column.ValueChanged
        If Column.Value = 1 Then 'if the value is greater than zero then keep it at zreo
            MsgBox("Error, the number is invalid")
            Column.Value = 0
        End If
        If Column.Value = 11 Or Column.Value = 9 Then 'if level1 is selected keep the value of column at 10 
            MsgBox("Error, level1 value is 10")
            Column.Value = 10
        End If
        If Column.Value = 21 Or Column.Value = 19 Then 'if level2 is selected keep the value of column at 20
            MsgBox("Error, level2 value is 20")
            Column.Value = 20
        End If
        If Column.Value = 43 Or Column.Value = 41 Then 'if level3 is selected keep the value of column at 42
            MsgBox("Error, level3 value is 42")
            Column.Value = 42
        End If
    End Sub

    Private Sub Width_ValueChanged(sender As Object, e As EventArgs) Handles Width.ValueChanged
        If Width.Value = 1 Then 'if the value is greater than zero then keep it at zreo
            MsgBox("Error, number is invalid")
            Width.Value = 0
        End If
        If Width.Value = 11 Or Width.Value = 9 Then 'if level1 is selected keep the value of width at 10 
            MsgBox("Error, level1 value is 10")
            Width.Value = 10
        End If
        If Width.Value = 21 Or Width.Value = 19 Then 'if level2 is selected keep the value of width at 20 
            MsgBox("Error, level2 value is 20")
            Width.Value = 20
        End If
        If Width.Value = 43 Or Width.Value = 41 Then 'if level3 is selected keep the value of width at 42 
            MsgBox("Error, level3 value is 42")
            Width.Value = 42
        End If
    End Sub

    Private Sub PixelH_ValueChanged(sender As Object, e As EventArgs) Handles PixelH.ValueChanged
        If PixelH.Value = 1 Then 'if the value is greater than zero then keep it at zreo
            MsgBox("Error, number is invalid")
            PixelH.Value = 0
        End If
        If PixelH.Value = 49 Or PixelH.Value = 51 Then 'if level1 is selected keep the value of PixelH at 50
            MsgBox("Error, level1 value is 50")
            PixelH.Value = 50
        End If
        If PixelH.Value = 28 Or PixelH.Value = 30 Then 'if level2 is selected keep the value of PixelH at 29
            MsgBox("Error, level2 value is 29")
            PixelH.Value = 29
        End If
        If PixelH.Value = 14 Or PixelH.Value = 16 Then 'if level3 is selected keep the value of PixelH at 15
            MsgBox("Error, level3 value is 15")
            PixelH.Value = 15
        End If
    End Sub

    Private Sub PixelW_ValueChanged(sender As Object, e As EventArgs) Handles PixelW.ValueChanged
        If PixelW.Value = 1 Then 'if the value is greater than zero then keep it at zreo
            MsgBox("Error, number is invalid")
            PixelW.Value = 0
        End If
        If PixelW.Value = 49 Or PixelW.Value = 51 Then 'if level1 is selected keep the value of PixelW at 50
            MsgBox("Error, level1 value is 50")
            PixelW.Value = 50
        End If
        If PixelW.Value = 28 Or PixelW.Value = 30 Then 'if level2 is selected keep the value of PixelW at 29
            MsgBox("Error, level2 value is 29")
            PixelW.Value = 29
        End If
        If PixelW.Value = 14 Or PixelW.Value = 16 Then 'if level1 is selected keep the value of PixelW at 15
            MsgBox("Error, level3 value is 15")
            PixelW.Value = 15
        End If
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        If N = 0 Then 'don't allow the user to move the character with out there being a maze 
            MsgBox("error, maze not generated")
        End If
    End Sub

End Class
Public Class GraphicsPanel
    Inherits Panel 'sets up the panel for the maze
    Sub New()
        Me.DoubleBuffered = True
    End Sub
End Class