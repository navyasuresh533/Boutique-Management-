
Imports System.Data.SqlClient

Public Class salesbill
    ' Define your connection string
    Private connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Get the values entered by the user
            Dim [Date] As Date = DateTimePicker1.Value
            Dim billno As String = TextBox1.Text
            Dim customername As String = TextBox2.Text
            Dim clothid As String = TextBox3.Text
            Dim clothname As String = TextBox4.Text
            Dim rate As Decimal = Decimal.Parse(TextBox5.Text)
            Dim quantity As Integer = Integer.Parse(TextBox6.Text)
            Dim totalAmount As Decimal = rate * quantity ' Calculate the total amount

            ' Add the values to the DataGridView
            DataGridView1.Rows.Add([Date], billno, customername, clothid, clothname, rate, quantity)

            ' Update the total amount TextBox and Label
            TextBox7.Text = totalAmount.ToString()
            Label9.Text = "Total Amount: " & totalAmount.ToString()

            ' Clear the TextBoxes and ComboBox for next entry
            DateTimePicker1.Value = DateTime.Now
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()

            ' Save the data into the SQL database
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO SBB (Date, Billno, Customername, Clothid, Clothname, Rate, Quantity) VALUES (@Date, @Billno, @Customername, @Clothid, @Clothname, @Rate, @Quantity)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Date", [Date])
                    command.Parameters.AddWithValue("@Billno", billno)
                    command.Parameters.AddWithValue("@Customername", customername)
                    command.Parameters.AddWithValue("@Clothid", clothid)
                    command.Parameters.AddWithValue("@Clothname", clothname)
                    command.Parameters.AddWithValue("@Rate", rate)
                    command.Parameters.AddWithValue("@Quantity", quantity)

                    command.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Data added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dashboard.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim pd As New Printing.PrintDocument
        AddHandler pd.PrintPage, AddressOf PrintPageHandler
        pd.Print()
    End Sub

    ' Event handler for printing the form content
    Private Sub PrintPageHandler(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs)
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
        e.Graphics.DrawImage(bmp, 0, 0)
    End Sub
End Class




