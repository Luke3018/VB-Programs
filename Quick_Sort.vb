Public Class Form1
    Dim iArray(7) As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        iArray(0) = 5
        iArray(1) = 2
        iArray(2) = 7
        iArray(3) = 6
        iArray(4) = 1
        iArray(5) = 9
        iArray(6) = 4
        iArray(7) = 8

        partition(iArray, 0, 7)


        Dim stOut As String
        For i = 0 To 7
            stOut = stOut & vbNewLine & iArray(i)
        Next
        MsgBox(stOut)


    End Sub
    Function partition(ByRef iarray As Integer(), lPointer As Integer, rPointer As Integer) As Integer
        Dim iPoivot As Integer
        Dim stCurrentPointer As String
        Dim stNew As String

        iPoivot = iarray(lPointer)
        stCurrentPointer = iarray(1)
        stCurrentPointer = "Right"

        Do While lPointer <> rPointer
            If stCurrentPointer = "Right" Then
                If iarray(rPointer) < iPoivot Then
                    iarray(lPointer) = iarray(rPointer)
                    lPointer = lPointer + 1
                    stCurrentPointer = "Left"
                Else
                    rPointer = rPointer - 1
                End If
            ElseIf stCurrentPointer = "Left" Then
                If iarray(lPointer) > iPoivot Then
                    iarray(rPointer) = iarray(lPointer)
                    rPointer = rPointer - 1
                    stCurrentPointer = "Right"
                Else
                    lPointer = lPointer + 1
                End If
            End If
        Loop

        iarray(lPointer) = iPoivot
        Return lPointer


    End Function

End Class
