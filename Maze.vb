Public Class Maze
    Inherits Control
    'defines the direction the character can move from cell to cell (square to square)
    Public Enum MoveVector
        None
        Left 'moveVector cells left of character
        Right 'moveVector cells rigth of character 
        Down 'moveVector cells down of character
        Up 'moveVector cells up of character
    End Enum
    'sets the global variableas they will be used all ove the class
    Dim Rows As Integer
    Dim Columns As Integer
    Dim cWidth As Integer
    Dim cHeight As Integer
    Dim Square As New Dictionary(Of String, cell) 'is the array of cells that make the maze
    Dim Tower As New Stack(Of cell) 'sets the stack to push and pop cells on and off 
    Public Maze As Image 'declares the maze as an image

    Public Event MazeComplete(Maze As Image) 'set the event which outputs the maze to be stored as an image
    Private Event CallComplete(Maze As Image) 'set the event which outputs the maze to be stored as an image
    Public Shadows ReadOnly Property Bounds As Rectangle
        Get
            Dim rect As New Rectangle(0, 0, Width, Height) 'set size of cells 
            Return rect 'return the value
        End Get
    End Property

    Dim WithEvents printDoc As New Printing.PrintDocument()
    Private Sub PrintImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        Dim nonprinters As List(Of String) = ({"Send To OneNote 2013", "PDFCreator", "PDF Architect 4",
                                       "Microsoft XPS Document Writer", "Microsoft Print to PDF", "Fax", "-"}).ToList 'send value of nonprinter to onenote and make a file
        Dim printerName As String = "none"
        For Each a As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters 'sets the printer properties of the maze
            If nonprinters.IndexOf(a) > -1 Then Continue For 'if nonprinters is greater than one then printer name is a
            printerName = a
        Next
        If printerName = "none" Then Exit Sub
        printDoc.PrinterSettings.PrinterName = printerName
        Dim imageLeft As Integer = CInt(e.PageBounds.Width / 2) - CInt(Maze.Width / 2) 'workout page width boundaries of maze 
        Dim imageTop As Integer = CInt(e.PageBounds.Height / 2) - CInt(Maze.Height / 2) 'workout page height boundaries of maze
        e.Graphics.DrawImage(Maze, imageLeft, imageTop)
    End Sub
    Public Sub PrintMaze()
        printDoc.Print()
    End Sub
    Public Sub Generate()
        Dim c As Integer = 0 'set c to equal zero 
        Dim r As Integer = 0
        For y As Integer = 0 To Height Step cHeight 'get height of cell
            For x As Integer = 0 To Width Step cWidth 'get width of cell
                Dim cell As New cell(New Point(x, y), New Size(cWidth, cHeight), Square, r, c, (Rows - 1), (Columns - 1)) 'get point of cell to make path
                c += 1
            Next
            c = 0 : r += 1 'c is equal to zero plus one to r
        Next
        Dim thread As New Threading.Thread(AddressOf Dig) 'create a new thread of dig
        thread.Start()
    End Sub
    Private Sub Dig()
        Dim r As Integer = 0
        Dim c As Integer = 0
        Dim key As String = "c" & 5 & "r" & 5 'holds key of each cell 
        Dim startCell As cell = Square(key)
        Tower.Clear()
        startCell.Seen = True 'set first cell to be true and marked
        While (startCell IsNot Nothing)
            startCell = startCell.Dig(Tower)
            If startCell IsNot Nothing Then
                startCell.Seen = True
                startCell.Wall = Pens.Black 'make wall of rows and columns 
            End If
        End While
        Tower.Clear() 'clear stack 
        Dim Maze As New Bitmap(Width, Height) 'set maze a bitmap and store
        Using g As Graphics = Graphics.FromImage(Maze) 'add graphics to image
            g.Clear(Color.White)
            If Square.Count > 0 Then
                For r = 0 To Me.Rows - 1
                    For c = 0 To Me.Columns - 1
                        Dim cell As cell = Square("c" & c & "r" & r) 'add cell to dictionary
                        cell.draw(g) 'draw path from cells 
                    Next
                Next
            End If
        End Using
        Me.Maze = Maze
        RaiseEvent CallComplete(Maze) 'call event callcomplete
    End Sub
    Delegate Sub dComplete(maze As Image)
    Private Sub Call_Complete(maze As Image) Handles Me.CallComplete
        If Me.InvokeRequired Then 'see if invoke is required 
            Me.Invoke(New dComplete(AddressOf Call_Complete), maze) 'set a new invoke for callComplete
        Else
            RaiseEvent MazeComplete(maze) 'call event mazeComplete
        End If
    End Sub

    Public Sub View(ByRef strStructure As String)
        Dim r As Integer = 0
        Dim c As Integer = 0

        If Square.Count > 0 Then
            For r = 0 To Me.Rows - 1 'loop while r is zero and minus one from rows 
                For c = 0 To Me.Columns - 1 'loop while c is zero and minus one from columns
                    Dim cell As cell = Square("c" & c & "r" & r)
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                    'create structure of rows and columns between cells
                    strStructure = strStructure & cell.Row.ToString() & "," & cell.Column.ToString() & ":" & cell.Borders.Left.ToString() & "," & cell.Borders.Top.ToString() & vbCrLf
                Next
            Next
        End If
    End Sub
    Public Function VerifyLimits(ObjLeft As Integer, ObjTop As Integer, ObjWidth As Integer, objHeight As Integer, MoveDirection As MoveVector, MoveDistance As Integer) As Boolean
        Const m_MinPos As Integer = 1 'sets minmium postion character is allowed to go
        Const m_MaxPos As Integer = 700 'sets maximum postion character is allowed to go
        Dim MoveOK As Boolean = False

        'case to see if move is valid for character
        Select Case MoveDirection
            Case MoveVector.Up 'up case, checks for walls above character
                If (ObjTop - MoveDistance) >= m_MinPos Then
                    MoveOK = True
                End If

            Case MoveVector.Down 'down case, checks for walls below character
                If (ObjTop + objHeight + MoveDistance) <= m_MaxPos Then
                    MoveOK = True
                End If

            Case MoveVector.Left 'left case, checks for walls left of character
                If (ObjLeft - MoveDistance) >= m_MinPos Then
                    MoveOK = True
                End If

            Case MoveVector.Right 'rigth case, checks for walls rigth of character
                If (ObjLeft + ObjWidth + MoveDistance) <= m_MaxPos Then
                    MoveOK = True
                End If

        End Select

        VerifyLimits = MoveOK 'if no wall found then move is valid

    End Function
    Public Function VerifyMove(ObjLeft As Integer, ObjTop As Integer, ObjWidth As Integer, objHeight As Integer, MoveDirection As MoveVector, imove As Integer, ByRef strErrMsg As String) As Boolean
        Dim MoveOK As Boolean = False
        strErrMsg = ""
        Try
            'Find current cell
            Dim Col As Integer = 0
            Dim Row As Integer = 0
            Dim NextCol As Integer = 0
            Dim NextRow As Integer = 0

            ' Object is already positioned across two rows.
            Dim LowerRow As Integer = 0

            If ObjLeft > 0 Then
                Col = CInt(Fix(ObjLeft / cWidth))
            End If

            If ObjTop > 0 Then
                Row = CInt(Fix(ObjTop / cHeight))
            End If

            Dim CurrentCell As cell = Square("c" & Col & "r" & Row)

            If IsNothing(CurrentCell) Then
                strErrMsg = "Error : failed to find cell"
            Else

                'strErrMsg = "Cell Row" & cell.Row & "Cell Col" & cell.Column

                Select Case MoveDirection
                    Case MoveVector.Left
                        'Check if object top-left point will stay within the bounds of the current cell.
                        If ((ObjLeft - imove) >= CurrentCell.Borders.Left) Then
                            MoveOK = True
                        Else
                            'Check if object top left is moving out of current cell and it has a left hand wall.
                            If ((ObjLeft - imove) < CurrentCell.Borders.Left) And CurrentCell.leftLine Then
                                'Can't move there !
                            Else
                                'object top-left is moving into an adjacent cell in the current row, to the left hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft - imove) / cWidth))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & Row)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the current row across it's bottom wall or right hand wall.
                                    ElseIf ((ObjTop + objHeight) > (NextCell.Borders.Top + cHeight)) And (NextCell.bottomLine Or NextCell.RightLine) Then
                                        'Can't move here, as would move onto next cells bottom line.
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If

                            'If top-left is ok, then check bottom left
                            'Check if object bottom left point is not moving out of current cell
                            If MoveOK And ((ObjTop + objHeight) > CurrentCell.Borders.Bottom) Then
                                'Need to run checks for object bottom right, assume fail until prove otherwise.
                                MoveOK = False

                                'object bottom left is moving into an adjacent cell, to the left hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft - imove) / cWidth))
                                LowerRow = CInt(Fix((ObjTop + objHeight) / cHeight))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & LowerRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row across it's top wall or right hand wall.
                                    ElseIf (NextCell.TopLine Or NextCell.RightLine) Then
                                        'Can't move here, as would move onto next cells bottom line.
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If
                        End If

                    Case MoveVector.Right
                        'Check if object top-right point is not moving out of current cell
                        If ((ObjLeft + ObjWidth + imove) <= CurrentCell.Borders.Right) Then
                            MoveOK = True
                        Else
                            'Check if object top right is moving out of current cell and it has a right hand wall.
                            If ((ObjLeft + ObjWidth + imove) > CurrentCell.Borders.Right) And CurrentCell.RightLine Then
                                'Can't move there !
                            Else
                                'object top-right is moving into an adjacent cell in the current row, to the right hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft + ObjWidth + imove) / cWidth))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & Row)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the current row across it's bottom wall or left hand wall.
                                    ElseIf ((ObjTop + objHeight) > (NextCell.Borders.Top + cHeight)) And (NextCell.bottomLine Or NextCell.leftLine) Then
                                        'Can't move here, as would move onto next cells bottom or left hand wall.
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If

                            'If top-right is ok, then check bottom right
                            'Check if object bottom right  point is not moving out of current cell
                            If MoveOK And ((ObjTop + objHeight) > CurrentCell.Borders.Bottom) Then
                                'Need to run checks for object bottom right, assume fail until prove otherwise.
                                MoveOK = False

                                'object bottom right is moving into an adjacent cell, to the left hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft + ObjWidth + imove) / cWidth))
                                LowerRow = CInt(Fix((ObjTop + objHeight) / cHeight))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & LowerRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row across it's top wall or left hand wall.
                                    ElseIf (NextCell.TopLine Or NextCell.leftLine) Then
                                        'Can't move here, as would move onto next cells top or left hand wall.
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If
                        End If

                    Case MoveVector.Up

                        'Check if object top-left point will stay within the bounds of the current cell.
                        If ((ObjTop - imove) >= CurrentCell.Borders.Top) Then
                            MoveOK = True
                        Else
                            'Check if object top left is moving out of current cell and it has a top wall.
                            If ((ObjTop - imove) < CurrentCell.Borders.Top) And CurrentCell.TopLine Then
                                'Can't move there !
                            Else
                                'object top-left is moving into an adjacent cell in the row above.
                                '(hence find this row to run some checks.)
                                NextRow = CInt(Fix((ObjTop - imove) / cHeight))

                                If (NextRow = Row) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & Col & "r" & NextRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row above across it's bottom wall.
                                    ElseIf NextCell.bottomLine Then
                                        'Error
                                        'Check if moving across the right hand wall of the adjacent cell in the row above.
                                    ElseIf ((ObjLeft + ObjWidth) > (NextCell.Borders.Right)) And NextCell.RightLine Then
                                        'Error
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If

                            'If top-left is ok, then check top right 
                            'Check if object top right point is not moving out of current cell
                            If MoveOK And ((ObjLeft + ObjWidth) > CurrentCell.Borders.Right) Then
                                'Need to run checks for object top right, assume fail until prove otherwise.
                                MoveOK = False

                                'object top right is moving into an adjacent cell, to the right hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft + ObjWidth) / cWidth))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & NextRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row across it's bottom or left hand wall.
                                    ElseIf (NextCell.bottomLine Or NextCell.leftLine) Then
                                        'Can't move here, as would move onto next cells bottom or left hand wall. 
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If
                        End If

                    Case MoveVector.Down

                        'Check if object top-left point will stay within the bounds of the current cell.
                        If ((ObjTop + objHeight + imove) <= (CurrentCell.Borders.Bottom)) Then
                            MoveOK = True
                        Else
                            'Check if object bottom left is moving out of current cell and it has a bottom wall.
                            If ((ObjTop + objHeight + imove) > CurrentCell.Borders.Bottom) And CurrentCell.bottomLine Then
                                'Can't move there !
                            Else
                                'object bottom-left is moving into an adjacent cell in the row below.
                                '(hence find this row to run some checks.)
                                NextRow = CInt(Fix((ObjTop + objHeight + imove) / cHeight))

                                If (NextRow = Row) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & Col & "r" & NextRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row below across it's top wall.
                                    ElseIf NextCell.TopLine Then
                                        'Error
                                        'Check if moving across the right hand wall of the adjacent cell in the row below.
                                    ElseIf ((ObjLeft + ObjWidth) > (NextCell.Borders.Right)) And NextCell.RightLine Then
                                        'Error
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If

                            'If bottom-left is ok, then check bottom right 
                            'Check if object bottom right point is not moving out of current cell
                            If MoveOK And ((ObjLeft + ObjWidth) > CurrentCell.Borders.Right) Then
                                'Need to run checks for object top right, assume fail until prove otherwise.
                                MoveOK = False

                                'object bottom right is moving into an adjacent cell, to the right hand side.
                                '(hence find this column to run some checks.)
                                NextCol = CInt(Fix((ObjLeft + ObjWidth) / cWidth))

                                If (NextCol = Col) Then
                                    ' Ok, in same column - don't expect to get here !
                                    Debug.Assert(False)
                                    MoveOK = True
                                Else
                                    Dim NextCell As cell = Square("c" & NextCol & "r" & NextRow)

                                    If IsNothing(NextCell) Then
                                        Debug.Assert(False)
                                        strErrMsg = "Error : failed to find next cell"
                                        'Check if moving into adjacent cell in the row across it's top or left hand wall.
                                    ElseIf (NextCell.TopLine Or NextCell.leftLine) Then
                                        'Can't move here, as would move onto next cells bottom or left hand wall. 
                                    Else
                                        MoveOK = True
                                    End If
                                End If
                            End If
                        End If

                End Select

            End If

        Catch ex As Exception
            strErrMsg = "Error " & ex.Message
        End Try

        VerifyMove = MoveOK

    End Function
    Sub New(rows As Integer, columns As Integer, cellWidth As Integer, cellHeight As Integer)
        Me.Rows = rows
        Me.Columns = columns
        Me.cWidth = cellWidth
        Me.cHeight = cellHeight
        Me.Width = (Me.Columns * Me.cWidth) + 1
        Me.Height = (Me.Rows * Me.cHeight) + 1
        Me.CreateHandle()
    End Sub
End Class