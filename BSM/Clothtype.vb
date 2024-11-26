Imports System.Data.SqlClient

Public Class Clothtype
    ' Define your connection string
    Private connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Get the values entered by the user
            Dim stockid As String = TextBox1.Text
            Dim clothname As String = TextBox2.Text
            Dim category As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "")
            Dim rate As String = TextBox3.Text
            Dim quantity As String = TextBox4.Text

            ' Add the values to the DataGridView
            DataGridView1.Rows.Add(stockid, clothname, category, rate, quantity)

            ' Clear the TextBoxes and ComboBoxes for next entry
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox4.Clear()

            ' Save the data into the SQL database
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO CD (StockID, ClothName, Category, Rate, Quantity) VALUES (@StockID, @ClothName, @Category, @Rate, @Quantity)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@StockID", stockid)
                    command.Parameters.AddWithValue("@ClothName", clothname)
                    command.Parameters.AddWithValue("@Category", category)
                    command.Parameters.AddWithValue("@Rate", rate)
                    command.Parameters.AddWithValue("@Quantity", quantity)
                    command.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Data added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Log the exception
            Console.WriteLine("Error occurred in Button1_Click: " & ex.ToString())
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) 
        Try
            ' Get the selected row index
            Dim selectedRowIndex As Integer = If(DataGridView1.CurrentCell IsNot Nothing, DataGridView1.CurrentCell.RowIndex, -1)

            If selectedRowIndex >= 0 Then
                ' Get the values from the selected row in the DataGridView
                Dim stockid As String = DataGridView1.Rows(selectedRowIndex).Cells(0).Value.ToString()
                Dim clothname As String = DataGridView1.Rows(selectedRowIndex).Cells(1).Value.ToString()
                Dim category As String = DataGridView1.Rows(selectedRowIndex).Cells(2).Value.ToString()
                Dim rate As String = DataGridView1.Rows(selectedRowIndex).Cells(3).Value.ToString()
                Dim quantity As String = DataGridView1.Rows(selectedRowIndex).Cells(4).Value.ToString()

                ' Populate the values into the input fields
                TextBox1.Text = stockid
                TextBox2.Text = clothname
                ComboBox1.SelectedItem = category
                TextBox3.Text = rate
                TextBox4.Text = quantity

                MessageBox.Show("Data loaded for editing", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Please select a row to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Log the exception
            Console.WriteLine("Error occurred in Button3_Click: " & ex.ToString())
        End Try
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    Try
        ' Get the selected row index
        Dim selectedRowIndex As Integer = If(DataGridView1.CurrentCell IsNot Nothing, DataGridView1.CurrentCell.RowIndex, -1)

        If selectedRowIndex >= 0 Then
            ' Get the stock ID from the selected row in the DataGridView
            Dim stockid As String = DataGridView1.Rows(selectedRowIndex).Cells(0).Value.ToString()

            ' Delete the record from the SQL database
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "DELETE FROM CD WHERE StockID = @StockID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@StockID", stockid)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Remove the selected row from the DataGridView
            DataGridView1.Rows.RemoveAt(selectedRowIndex)

            MessageBox.Show("Data deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Please select a row to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    Catch ex As Exception
        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' Log the exception
        Console.WriteLine("Error occurred in Button4_Click: " & ex.ToString())
    End Try
End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dashboard.Show()
    End Sub
End Class



