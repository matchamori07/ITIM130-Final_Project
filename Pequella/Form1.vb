Imports System.Data.Common
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = False
        Panel2.Visible = False

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel1.Visible = False
        Panel2.Visible = True

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Panel1.Visible = True
        Panel2.Visible = False
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        frmLogin.Show()
    End Sub


    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click
        Dim picker As New frmProductSearch()
        picker.ItemType = "Sale"

        If picker.ShowDialog() = DialogResult.OK Then
            Dim row As New ListViewItem(picker.SelectedProductName)
            row.SubItems.Add(picker.SelectedQuantity.ToString())
            row.SubItems.Add(picker.SelectedUnitPrice.ToString("F2"))
            row.Tag = picker.SelectedProductId
            ListView1.Items.Add(row)
        End If
    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Please select an item to delete.", MsgBoxStyle.Exclamation)
            Return
        End If

        ListView1.Items.Remove(ListView1.SelectedItems(0))
    End Sub

    Private Sub Label97_Click(sender As Object, e As EventArgs) Handles Label97.Click
        Dim searchText As String = TextBox1.Text.Trim()
        Dim customer_id As Integer

        If String.IsNullOrEmpty(searchText) Then
            MsgBox("Please enter an ID or keyword to search.", MsgBoxStyle.Information, "Empty Search")
            Return
        End If

        Try
            conn.Open()

            If Integer.TryParse(searchText, customer_id) Then

                sql = "SELECT customer_id, first_name, last_name, contact_number, address, CASE WHEN status = 1 THEN 'Active' 
            ELSE 'Inactive' END AS status_display FROM tbl_customers WHERE customer_id = " & customer_id & "AND status = 1"
                dbcomm = New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(sql, conn)
                Dim searchDs As New DataSet()
                da.Fill(searchDs, "search_results")

                DataGridView1.DataSource = searchDs
                DataGridView1.DataMember = "search_results"

                If searchDs.Tables("search_results").Rows.Count = 0 Then
                    MsgBox("No records found matching: " & searchText, MsgBoxStyle.Information, "No Matches")
                End If
            Else

                sql = $"SELECT customer_id, first_name, last_name, contact_number, address, CASE WHEN status = 1 THEN 'Active' 
            ELSE 'Inactive' END AS status_display FROM tbl_customers WHERE status = 1 AND (first_name LIKE '%{searchText}%' 
            OR last_name LIKE '%{searchText}%' OR address LIKE '%{searchText}%' OR contact_number LIKE '%{searchText}%')
            ORDER BY customer_id DESC"

                Dim da As New MySqlDataAdapter(sql, conn)
                Dim searchDs As New DataSet()
                da.Fill(searchDs, "search_results")

                DataGridView1.DataSource = searchDs
                DataGridView1.DataMember = "search_results"

                If searchDs.Tables("search_results").Rows.Count = 0 Then
                    MsgBox("No records found matching: " & searchText, MsgBoxStyle.Information, "No Matches")
                End If

            End If

            TextBox1.Text = "Search by ID, first name, last name, phone, address, or status"
            TextBox1.ForeColor = Color.Gray
            TextBox1.Font = New Font(TextBox1.Font, FontStyle.Italic)

        Catch ex As Exception
            MsgBox("Search Error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.Text = ""
        TextBox1.ForeColor = Color.FromArgb(177, 64, 82)
        TextBox1.Font = New Font(TextBox1.Font, FontStyle.Regular)
    End Sub

End Class
