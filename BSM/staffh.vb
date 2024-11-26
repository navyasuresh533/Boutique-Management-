Imports System.Data.SqlClient

Public Class staffh
    Dim connectionString As String = "Data Source=LAPTOP-GBJSR462\SQLEXPRESS;Initial Catalog=BSM;Integrated Security=True"

    Private Sub staffh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            ' Add a button column to the DataGridView
            Dim deleteButtonColumn As New DataGridViewButtonColumn()
            deleteButtonColumn.HeaderText = "Delete"
            deleteButtonColumn.Text = "Delete"
            deleteButtonColumn.Name = "btnDelete"
            deleteButtonColumn.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Add(deleteButtonColumn)
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PopulateComboBox()
        Try
            ' Populate ComboBox1 with unique designations from the database
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "SELECT DISTINCT [designation] FROM SD;"
                Using command As New SqlCommand(query, connection)
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Add each designation to ComboBox1
                            ComboBox1.Items.Add(reader("designation").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while populating ComboBox: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Retrieve details based on ComboBox selection and display in DataGridView
            If ComboBox1.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(ComboBox1.SelectedItem.ToString()) Then
                Dim selectedDesignation As String = ComboBox1.SelectedItem.ToString()
                Debug.WriteLine("Selected Designation: " & selectedDesignation) ' Debugging message

                Using connection As New SqlConnection(connectionString)
                    Dim query As String = "SELECT * FROM SD WHERE [designation] = @designation;"
                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@designation", selectedDesignation)
                        connection.Open()
                        Dim adapter As New SqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)
                        DataGridView1.DataSource = dataTable
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while retrieving data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        dashboard.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            ' Check if the clicked cell is in the button column
            If e.ColumnIndex = DataGridView1.Columns("btnDelete").Index AndAlso e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim idToDelete As String = row.Cells("staffid").Value.ToString() ' Replace "staffid" with the actual column name of the unique identifier for your data
                ' Perform deletion operation using the ID or other unique identifier
                DeleteData(idToDelete)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while deleting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Function to delete data from the database
    Private Sub DeleteData(id As String)
        Try
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "DELETE FROM SD WHERE staffid = @id;"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@id", id)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Record deleted successfully.")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while deleting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
