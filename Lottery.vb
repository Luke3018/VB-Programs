Public Class Form1
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Array(4) As String
        Dim Number1 As Integer = CInt(Int((99 * Rnd()) + 1))
        Dim Number2 As Integer = CInt(Int((99 * Rnd()) + 1))
        Dim number3 As Integer = CInt(Int((99 * Rnd()) + 1))
        Dim number4 As Integer = CInt(Int((99 * Rnd()) + 1))
        Dim number5 As Integer = CInt(Int((99 * Rnd()) + 1))

        Label1.Text = Number1
        Label2.Text = Number2
        Label3.Text = number3
        Label4.Text = number4
        Label5.Text = number5


        MsgBox("the winning Numbers are " & Number1 & " " & Number2 & " " & number3 & " " &
               number4 & " " & number5)


    End Sub
End Class
