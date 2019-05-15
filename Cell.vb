Public Class cell
    'these are the global variables for the class they need to be global as they are used in multiple subprocedures and functions
    Public TopLine As Boolean = True
    Public bottomLine As Boolean = True
    Public leftLine As Boolean = True
    Public RightLine As Boolean = True
    Public tag As String
    Public Wall As Pen = Pens.Black 'declare walls and sets colour to be black
    Public Borders As Rectangle
    Public Square As Dictionary(Of String, cell) 'declars a dictionay of cells to be added to and stored 
    Public Column As Integer
    Public Row As Integer
    Public NTopLineID As String
    Public NBottomLineID As String
    Public NRightLineID As String
    Public NLeftLineID As String
    Public Seen As Boolean = False
    Public Tower As Stack(Of cell) 'decalres stack to push and pop cells 
    Public pbcharacter As PictureBox 'declares character as picturebox
    Public Sub draw(g As Graphics)
        'creates the visbial wall of the maze tha the user will see
        If TopLine Then g.DrawLine(Wall, New Point(Borders.Left, Borders.Top), New Point(Borders.Right, Borders.Top)) 'line on the top of each cell(square) 
        If bottomLine Then g.DrawLine(Wall, New Point(Borders.Left, Borders.Bottom), New Point(Borders.Right, Borders.Bottom)) 'line on the bottom of each cell(square)
        If leftLine Then g.DrawLine(Wall, New Point(Borders.Left, Borders.Top), New Point(Borders.Left, Borders.Bottom)) 'line on the left of each cell(square)
        If RightLine Then g.DrawLine(Wall, New Point(Borders.Right, Borders.Top), New Point(Borders.Right, Borders.Bottom)) 'line on the rigth of each cell(square)
    End Sub
    Sub New(location As Point, size As Size, ByRef cellList As Dictionary(Of String, cell), r As Integer, c As Integer, maxR As Integer, maxC As Integer)
        Me.Borders = New Rectangle(location, size) 'sets the outer edges of the maze 
        Me.Column = c
        Me.Row = r
        Me.tag = "c" & c & "r" & r
        Dim rowNort As Integer = r - 1 'declares the north rows of the maze 
        Dim rowSout As Integer = r + 1 'declares the south rows of the maze
        Dim colEast As Integer = c + 1 'declares the east columns of the maze
        Dim colWest As Integer = c - 1 'declares the west columns of the maze
        NTopLineID = "c" & c & "r" & rowNort 'builds the top rows of the maze going northwards
        NBottomLineID = "c" & c & "r" & rowSout 'build the bottom rows of the maze going southwards
        NRightLineID = "c" & colEast & "r" & r 'builds the rigth columns of the maze going eastwards
        NLeftLineID = "c" & colWest & "r" & r 'builds the left columns of the maze going westwasrds 
        If rowNort < 0 Then NTopLineID = "none"
        If rowSout > maxR Then NBottomLineID = "none"
        If colEast > maxC Then NRightLineID = "none"
        If colWest < 0 Then NLeftLineID = "none"
        Me.Square = cellList
        Me.Square.Add(Me.tag, Me)
    End Sub
    Function getNeighbor() As cell
        Dim c As New List(Of cell)
        'sets the current cell(square) as visited and looks for a neighbour cell to join to 
        If Not NTopLineID = "none" AndAlso Square(NTopLineID).Seen = False Then c.Add(Square(NTopLineID)) 'checks to see if there is an unvisited cell north of the current cell
        If Not NBottomLineID = "none" AndAlso Square(NBottomLineID).Seen = False Then c.Add(Square(NBottomLineID)) 'checks to see if there is an unvisited cell south of the current cell
        If Not NRightLineID = "none" AndAlso Square(NRightLineID).Seen = False Then c.Add(Square(NRightLineID)) 'checks to see if there is an unvisited cell east of the current cell
        If Not NLeftLineID = "none" AndAlso Square(NLeftLineID).Seen = False Then c.Add(Square(NLeftLineID)) 'checks to see if there is an unvisited cell west of the current cell
        Dim max As Integer = c.Count
        Dim currentCell As cell = Nothing
        'marks current cell as visited and randomly choses an unvisited cell to go to and removes wall between cells 
        If c.Count > 0 Then
            Randomize()
            Dim index As Integer = CInt(Int(c.Count * Rnd())) 'randomly find an unvisted cell 
            currentCell = c(index)
        End If
        Return currentCell
    End Function
    Function Dig(ByRef stack As Stack(Of cell)) As cell
        Me.Tower = stack
        'looks for unvisited cells to put on the stack and off the stack untill all cells are visited
        Dim nextCell As cell = getNeighbor()
        If Not nextCell Is Nothing Then
            stack.Push(nextCell)
            If nextCell.tag = NTopLineID Then 'removes top walls of cells from current to chosen
                TopLine = False
                nextCell.bottomLine = False
            ElseIf nextCell.tag = NBottomLineID Then 'removes bottom walls of cells from current to chosen
                bottomLine = False
                nextCell.TopLine = False
            ElseIf nextCell.tag = NRightLineID Then 'removes right walls of cells from current to chosen
                RightLine = False
                nextCell.leftLine = False
            ElseIf nextCell.tag = NLeftLineID Then 'removes left walls of cells from current to chosen
                leftLine = False
                nextCell.RightLine = False
            End If
        ElseIf Not stack.Count = 0 Then
            nextCell = Tower.Pop
        Else
            Return Nothing
        End If
        Return nextCell
    End Function
End Class
