

Imports System.Data.SqlClient

Public Class purchaseentry
    ' Define your connection string
    Private connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Get the values entered by the user
            Dim [Date] As Date = DateTimePicker1.Value
            Dim purchaseid As String = TextBox1.Text
            Dim vendorname As String = TextBox2.Text
            Dim category As String = ComboBox1.SelectedItem.ToString()
            Dim rate As Decimal = Decimal.Parse(TextBox3.Text)
            Dim quantity As Integer = Integer.Parse(TextBox4.Text)
            Dim totalamount As Decimal = Decimal.Parse(TextBox5.Text)

            ' Add the values to the DataGridView
            DataGridView1.Rows.Add([Date], purchaseid, vendorname, category, rate, quantity, totalamount)

            ' Clear the TextBoxes and ComboBox for next entry
            DateTimePicker1.Value = DateTime.Now
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            ComboBox1.SelectedIndex = -1

            ' Save the data into the SQL database
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO PD (Date, PurchaseID, VendorName, Category, Rate, Quantity, TotalAmount) VALUES (@Date, @PurchaseID, @VendorName, @Category, @Rate, @Quantity, @TotalAmount)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Date", [Date])
                    command.Parameters.AddWithValue("@PurchaseID", purchaseid)
                    command.Parameters.AddWithValue("@VendorName", vendorname)
                    command.Parameters.AddWithValue("@Category", category)
                    command.Parameters.AddWithValue("@Rate", rate)
                    command.Parameters.AddWithValue("@Quantity", quantity)
                    command.Parameters.AddWithValue("@TotalAmount", totalamount)
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


End Class



