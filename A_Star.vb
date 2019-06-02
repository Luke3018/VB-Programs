
Public Class Form1
        Dim g As New Graph
        Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            g.AddVertex(0, "A", 0, 8)
            g.AddVertex(1, "B", 2, 11)
            g.AddVertex(2, "C", 3, 8)
            g.AddVertex(3, "D", 4, 12)
            g.AddVertex(4, "E", 4, 4)
            g.AddVertex(5, "F", 1, 3)
            g.AddVertex(6, "G", 6, 1)
            g.AddVertex(7, "H", 7, 6)
            g.AddVertex(8, "I", 9, 5)
            g.AddVertex(9, "J", 12, 4)
            g.AddVertex(10, "K", 12, 8)
            g.AddVertex(11, "L", 12, 11)
            g.AddVertex(12, "M", 12, 14)
            g.AddVertex(13, "N", 14, 3)
            g.AddVertex(14, "O", 15, 12)
            g.AddVertex(15, "P", 16, 8)

            g.AddEdge(0, 1, 5)
            g.AddEdge(0, 2, 5)
            g.AddEdge(1, 0, 5)
            g.AddEdge(1, 2, 4)
            g.AddEdge(1, 3, 3)
            g.AddEdge(2, 0, 5)
            g.AddEdge(2, 1, 4)
            g.AddEdge(2, 3, 7)
            g.AddEdge(2, 4, 7)
            g.AddEdge(2, 7, 8)
            g.AddEdge(3, 1, 3)
            g.AddEdge(3, 2, 7)
            g.AddEdge(3, 7, 11)
            g.AddEdge(3, 10, 16)
            g.AddEdge(3, 11, 13)
            g.AddEdge(3, 12, 14)
            g.AddEdge(4, 2, 7)
            g.AddEdge(4, 5, 4)
            g.AddEdge(4, 7, 5)
            g.AddEdge(5, 4, 4)
            g.AddEdge(5, 6, 9)
            g.AddEdge(6, 5, 9)
            g.AddEdge(6, 13, 12)
            g.AddEdge(7, 2, 8)
            g.AddEdge(7, 3, 11)
            g.AddEdge(7, 4, 5)
            g.AddEdge(7, 8, 3)
            g.AddEdge(8, 7, 3)
            g.AddEdge(8, 9, 4)
            g.AddEdge(9, 8, 4)
            g.AddEdge(9, 13, 3)
            g.AddEdge(9, 15, 8)
            g.AddEdge(10, 3, 16)
            g.AddEdge(10, 11, 5)
            g.AddEdge(10, 13, 7)
            g.AddEdge(10, 15, 4)
            g.AddEdge(11, 3, 13)
            g.AddEdge(11, 10, 5)
            g.AddEdge(11, 12, 9)
            g.AddEdge(11, 14, 4)
            g.AddEdge(12, 3, 14)
            g.AddEdge(12, 11, 9)
            g.AddEdge(12, 14, 5)
            g.AddEdge(13, 6, 12)
            g.AddEdge(13, 9, 3)
            g.AddEdge(13, 10, 7)
            g.AddEdge(13, 15, 7)
            g.AddEdge(14, 11, 4)
            g.AddEdge(14, 12, 5)
            g.AddEdge(15, 9, 8)
            g.AddEdge(15, 10, 4)
        g.AddEdge(15, 13, 7)
    End Sub

    Private Sub btnGetShortestPath_Click(sender As Object, e As EventArgs) Handles btnGetShortestPath.Click
        Dim shortestpath As List(Of String)
        shortestpath = g.AStar(0, 15)

        Dim stOut As String
        For Each n In shortestpath
            stOut = stOut & n & "   "
        Next

        ListBox1.Items.Add(stOut)

    End Sub
End Class
Public Class Vertex
        Public Value As String      'payload of the vertex
        Public Index As Integer     'every vertex has an ID number
        Private X As Integer        'grid co-ordinate 
        Private Y As Integer        'grid o-ordinate 
        Public g As Integer         'g value (path distance from start) for use by A*
        Public f As Integer         'f value (g + h) for use by A*
        Public Parent As Vertex     'previous vertex for use by A*

        Public Sub New(ByVal Index As Integer, ByVal Value As String, ByVal X As Integer, ByVal Y As Integer)
            Me.Index = Index
            Me.Value = Value
            Me.X = X
            Me.Y = Y
        End Sub

        Public Function h(destination As Vertex) As Integer
            'calculate Manhattan distance from this vertex to the desination vertex
            Return (destination.X - Me.X) + (destination.Y - Me.Y)

            'Return Math.Sqrt(((destination.X - Me.X) ^ 2 + (destination.Y - Me.Y) ^ 2))
        End Function

    End Class
    Public Class Graph
        Private Const TOTAL_VERTICES As Integer = 52
        Public vertices() As Vertex                   'array of verex objects
        Private adjacencyMatrix(,) As Integer
        Private iVertex As Integer                    'for scanning vertices array

        Public Sub New()
            ReDim vertices(TOTAL_VERTICES)
            ReDim adjacencyMatrix(TOTAL_VERTICES, TOTAL_VERTICES)

            'initialise the adjacency matrix
            Dim x, y As Integer
            For x = 0 To TOTAL_VERTICES - 1
                For y = 0 To TOTAL_VERTICES - 1
                    adjacencyMatrix(x, y) = 0
                Next
            Next
            iVertex = 0
        End Sub

        Public Sub AddVertex(ByVal index As Integer, ByVal value As String, ByVal X As Integer, ByVal Y As Integer)
            vertices(iVertex) = New Vertex(index, value, X, Y)
            iVertex += 1
        End Sub

        Public Sub AddEdge(ByVal StartVertex As Integer, ByVal EndVertex As Integer, Weight As Double)
            adjacencyMatrix(StartVertex, EndVertex) = Weight
            adjacencyMatrix(EndVertex, StartVertex) = Weight   'undirected graph is symetrical
        End Sub

    Public Function AStar(start As Integer, destination As Integer) As List(Of String)
        Dim openVertices As New List(Of Integer)
        Dim closedVertices As New List(Of Integer)
        Dim unvisitedVertices As New List(Of Integer)
        Dim currentVertex As Vertex

        For i As Integer = 0 To TOTAL_VERTICES - 1
            unvisitedVertices.Add(i)
        Next

        unvisitedVertices.Remove(start)
        currentVertex = vertices(start)

        vertices(start).g = 0
        vertices(start).f = vertices(start).g + vertices(start).h(vertices(destination))

        While currentVertex IsNot vertices(destination)
            For i As Integer = 0 To TOTAL_VERTICES - 1
                If adjacencyMatrix(currentVertex.Index, i) > 0 Then
                    If (Not (closedVertices.Contains(i))) And (Not (openVertices.Contains(i))) Then
                        unvisitedVertices.Remove(i)
                        openVertices.Add(i)
                        vertices(i).Parent = currentVertex
                    End If

                    Dim g As Integer
                    Dim f As Integer
                    g = currentVertex.g + adjacencyMatrix(currentVertex.Index, i)
                    f = g + vertices(i).h(vertices(destination))

                    If (vertices(i).f = 0) Or (f < vertices(i).f) Then
                        vertices(i).f = f
                        vertices(i).g = g
                        vertices(i).Parent = currentVertex
                    End If

                End If
            Next

            closedVertices.Add(currentVertex.Index)

            Dim iSmallestf As Integer = 1000
            Dim iNextCurrent As Integer
            For Each i In openVertices
                If vertices(i).f < iSmallestf Then
                    iSmallestf = vertices(i).f
                    iNextCurrent = i
                End If
            Next
            openVertices.Remove(iNextCurrent)
            currentVertex = vertices(iNextCurrent)

        End While

        Dim shortestpath As New List(Of String)

        Dim v As Vertex
        v = vertices(destination)
        Do Until v Is Nothing
            shortestpath.Add(v.Value)
            v = v.Parent
        Loop

        shortestpath.Reverse()

        shortestpath.Add(currentVertex.g)

        Return shortestpath

    End Function

End Class
