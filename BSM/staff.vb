
Imports System.Data.SqlClient

Public Class staff
    Private connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get the values entered by the user
        Dim staffid As String = TextBox1.Text
        Dim name As String = TextBox2.Text
        Dim sex As String = ComboBox1.SelectedItem
        Dim email As String = TextBox3.Text
        Dim designation As String = ComboBox2.SelectedItem.ToString()

        ' Add the values to the DataGridView
        DataGridView1.Rows.Add(staffid, name, sex, email, designation)

        ' Save the data into the SQL database
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim query As String = "INSERT INTO SD (StaffID, Name, Sex, Email, Designation) VALUES (@StaffID, @Name, @Sex, @Email, @Designation)"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@StaffID", staffid)
                command.Parameters.AddWithValue("@Name", name)
                command.Parameters.AddWithValue("@Sex", sex)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@Designation", designation)
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Data added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear the TextBoxes and ComboBoxes for next entry
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dashboard.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            ' Populate TextBoxes and ComboBoxes with selected row's data
            TextBox1.Text = selectedRow.Cells(0).Value.ToString() ' Assuming StaffID is in the first column
            TextBox2.Text = selectedRow.Cells(1).Value.ToString() ' Name
            ComboBox1.SelectedItem = selectedRow.Cells(2).Value.ToString() ' Sex
            TextBox3.Text = selectedRow.Cells(3).Value.ToString() ' Email
            ComboBox2.SelectedItem = selectedRow.Cells(4).Value.ToString() ' Designation
        Else
            MessageBox.Show("Please select a row to update.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Check if any row is selected
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            ' Get the staff ID of the selected row
            Dim staffId As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)

            ' Get the updated values from TextBoxes and ComboBoxes
            Dim name As String = TextBox2.Text
            Dim sex As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "")
            Dim email As String = TextBox3.Text
            Dim designation As String = If(ComboBox2.SelectedItem IsNot Nothing, ComboBox2.SelectedItem.ToString(), "")

            ' Validate data
            If name.Trim() = "" Then
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If sex = "" Then
                MessageBox.Show("Please select a sex.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If email.Trim() = "" Then
                MessageBox.Show("Please enter an email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If designation = "" Then
                MessageBox.Show("Please select a designation.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Update the selected row in the DataGridView
            selectedRow.Cells(1).Value = name
            selectedRow.Cells(2).Value = sex
            selectedRow.Cells(3).Value = email
            selectedRow.Cells(4).Value = designation

            ' Update the corresponding row in the database
            Try
                Using connection As New SqlConnection(connectionString)
                    connection.Open()
                    Dim query As String = "UPDATE SD SET Name = @name, Sex = @sex, Email = @email, Designation = @designation WHERE StaffID = @staffId"
                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@name", name)
                        command.Parameters.AddWithValue("@sex", sex)
                        command.Parameters.AddWithValue("@email", email)
                        command.Parameters.AddWithValue("@designation", designation)
                        command.Parameters.AddWithValue("@staffId", staffId)
                        command.ExecuteNonQuery()
                        MessageBox.Show("Row updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a row to update.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class