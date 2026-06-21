Imports MySql.Data.MySqlClient
Public Class frmProductSearch

    Dim conn As New MySqlConnection("Data Source=localhost;Database=party_rental_db;User=root;Password=")
    Public SelectedProductId As Integer = 0
    Public SelectedProductName As String = ""
    Public SelectedUnitPrice As Decimal = 0
    Public SelectedQuantity As Integer = 1
    Public ItemType As String = ""
    Private Sub frmProductSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
    End Sub

    Private Sub LoadProducts(Optional keyword As String = "")
        Try
            conn.Open()
            Dim sql As String = "SELECT product_id, product_name, unit_price, quantity_in_stock FROM tbl_products 
                                WHERE status = 1 AND item_type = @itemType AND product_name LIKE @kw 
                                ORDER BY product_name"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@itemType", ItemType)
            cmd.Parameters.AddWithValue("@kw", "%" & keyword & "%")

            Dim da As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds, "products")

            DataGridView100.DataSource = ds
            DataGridView100.DataMember = "products"
        Catch ex As Exception
            MsgBox("Error loading products: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Label97_Click(sender As Object, e As EventArgs) Handles Label97.Click
        LoadProducts(TextBox1.Text.Trim())
    End Sub

    Private Sub Label100_Click(sender As Object, e As EventArgs) Handles Label100.Click
        If DataGridView100.SelectedRows.Count = 0 Then
            MsgBox("Please select a product first.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim selectedRow = DataGridView100.SelectedRows(0)
        Dim stockAvailable As Integer = Convert.ToInt32(selectedRow.Cells("quantity_in_stock").Value)

        If NumericUpDown1.Value > stockAvailable Then
            MsgBox("Not enough stock. Available: " & stockAvailable, MsgBoxStyle.Exclamation)
            Return
        End If

        SelectedProductId = Convert.ToInt32(selectedRow.Cells("product_id").Value)
        SelectedProductName = selectedRow.Cells("product_name").Value.ToString()
        SelectedUnitPrice = Convert.ToDecimal(selectedRow.Cells("unit_price").Value)
        SelectedQuantity = Convert.ToInt32(NumericUpDown1.Value)

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DataGridView100_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView100.CellContentClick

    End Sub
End Class