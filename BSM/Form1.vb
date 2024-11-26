Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        ' Connection string for the SQL Server database
        Dim connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"


        ' Hardcoded username and password for validation
        Dim username As String = "admin"
        Dim password As String = "cloth"

        ' Get the username and password entered by the user
        Dim enteredUsername As String = TextBox1.Text
        Dim enteredPassword As String = TextBox2.Text

        ' Check if the entered username and password match the hardcoded values
        If enteredUsername = username AndAlso enteredPassword = password Then
            MessageBox.Show("Login successful!")
            Me.Hide()
            dashboard.Show()


        Else
            MessageBox.Show("Invalid username or password. Please try again.")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Clear text in TextBoxes
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class

