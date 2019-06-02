Public Class Form1
    Dim iArray(7) As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim stOut As String
        Dim arrleft() As Integer
        Dim arrRight() As Integer
        Dim middle As Integer
        Dim iPointer As Integer
        Dim iPointFill As Integer
        Dim i As Integer

        iArray(0) = 66
        iArray(1) = 44
        iArray(2) = 63
        iArray(3) = 8
        iArray(4) = 10
        iArray(5) = 12
        iArray(6) = 144
        iArray(7) = 106

        middle = Math.Floor((UBound(iArray)) / 2)
        ReDim arrleft(middle)

        Do Until iPointer > middle
            arrleft(iPointer) = iArray(iPointer)
            iPointer = iPointer + 1
        Loop

        iPointFill = 0

        ReDim arrRight(UBound(iArray) - UBound(arrleft) - 1)

        Do Until iPointer > UBound(iArray)
            arrRight(iPointFill) = iArray(iPointer)
            iPointFill = iPointFill + 1
            iPointer = iPointer + 1
        Loop


        For i = 0 To UBound(arrleft)
            stOut = stOut & arrleft(i) & vbNewLine
        Next
        MsgBox(stOut)

        stOut = ""
        For i = 0 To UBound(arrRight)
            stOut = stOut & arrRight(i) & vbNewLine

        Next
        MsgBox(stOut)

        Dim Arr3() As Integer

        ReDim Arr3(UBound(arrleft) + UBound(arrRight) + 1)

        Dim Ptr1 As Integer
        Dim Ptr2 As Integer
        Dim Ptr3 As Integer
        Dim stOut2 As String

        Do While (Ptr3 <= UBound(Arr3)) And (Ptr1 <= UBound(arrleft)) And (Ptr2 <= UBound(arrRight))
            If arrleft(Ptr1) < arrRight(Ptr2) Then
                Arr3(Ptr3) = arrleft(Ptr1)
                Ptr1 = Ptr1 + 1
            Else
                Arr3(Ptr3) = arrRight(Ptr2)
                Ptr2 = Ptr2 + 1
            End If
            Ptr3 = Ptr3 + 1
        Loop

        If Ptr1 <= UBound(arrleft) Then
            Do Until Ptr3 = UBound(Arr3) + 1
                Arr3(Ptr3) = arrleft(Ptr1)
                Ptr1 = Ptr1 + 1
                Ptr3 = Ptr3 + 1
            Loop
        ElseIf Ptr2 <= UBound(arrRight) Then
            Do Until Ptr3 = UBound(Arr3) + 1
                Arr3(Ptr3) = arrRight(Ptr2)
                Ptr2 = Ptr2 + 1
                Ptr3 = Ptr3 + 1
            Loop
        End If
        stOut2 = stOut2 & Arr3()
        MsgBox(stOut2)
    End Sub
End Class
