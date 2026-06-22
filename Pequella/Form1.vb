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

    ' CALCU TOTAL
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        If ListView1.Items.Count = 0 Then
            MsgBox("No items in cart.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim total As Decimal = 0
        For Each item As ListViewItem In ListView1.Items
            Dim qty As Integer = Convert.ToInt32(item.SubItems(1).Text)
            Dim price As Decimal = Convert.ToDecimal(item.SubItems(2).Text)
            total += qty * price
        Next

        Label6.Text = total.ToString("F2")
    End Sub

    ' PROCEED BTN / SAVE TRANSAC FOR SALE
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If ListView1.Items.Count = 0 Then
            MsgBox("Please add at least one product to the cart.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Label6.Text = "0.00" OrElse Label6.Text = "" Then
            MsgBox("Please click Calculate first.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim totalAmount As Decimal = Convert.ToDecimal(Label6.Text)
        Dim amountPaid As Decimal = 0

        If TextBox2.Text.Trim() = "" Then
            MsgBox("Please enter the amount paid.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not Decimal.TryParse(TextBox2.Text.Trim(), amountPaid) Then
            MsgBox("Amount paid must be a valid number.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If amountPaid < totalAmount Then
            MsgBox("Amount paid is less than the total." & vbCrLf &
                   "Total: ₱" & totalAmount.ToString("F2") & vbCrLf &
                   "Amount Paid: ₱" & amountPaid.ToString("F2"), MsgBoxStyle.Exclamation)
            Exit Sub
        End If



        Dim customerId As Integer = 0

        ' INSERT A NEW RETAIL CUSTOMER ROW EVERY TIME NA SELECTED YUNG CHECKBOX
        If CheckBox1.Checked Then
            Try
                If conn.State = ConnectionState.Closed Then conn.Open()
                sql = "INSERT INTO tbl_customers (first_name, last_name, status) VALUES ('Retail', 'Customer', 1)"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.ExecuteNonQuery()

                dbcomm = New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
                customerId = Convert.ToInt32(dbcomm.ExecuteScalar())
            Catch ex As Exception
                MsgBox("Error creating retail customer: " & ex.Message, MsgBoxStyle.Critical)
                Exit Sub
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        Else

            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select a customer from the list, or check 'Retail Customer'.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            customerId = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("customer_id").Value)
        End If




        For Each item As ListViewItem In ListView1.Items
            Dim productId As Integer = Convert.ToInt32(item.Tag)
            Dim requestedQty As Integer = Convert.ToInt32(item.SubItems(1).Text)

            Try
                If conn.State = ConnectionState.Closed Then conn.Open()
                sql = "SELECT quantity_in_stock, product_name FROM tbl_products WHERE product_id = " & productId
                dbcomm = New MySqlCommand(sql, conn)
                dbread = dbcomm.ExecuteReader()

                If dbread.Read() Then
                    Dim stock As Integer = Convert.ToInt32(dbread("quantity_in_stock"))
                    Dim productName As String = dbread("product_name").ToString()
                    dbread.Close()
                    conn.Close()

                    If requestedQty > stock Then
                        MsgBox("Not enough stock for: " & productName & vbCrLf &
                               "Available: " & stock & " | Requested: " & requestedQty,
                               MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                MsgBox("Stock check error: " & ex.Message)
                Exit Sub
            Finally
                If dbread IsNot Nothing AndAlso Not dbread.IsClosed Then dbread.Close()
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        Next



        ' START TRANSAC THEN SAVE TO DB
        Dim newSalesId As Integer = 0

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' INSERT tbl_sales
                sql = "INSERT INTO tbl_sales (customer_id, sales_date) VALUES (" & customerId & ", NOW())"
                dbcomm = New MySqlCommand(sql, conn, transaction)
                dbcomm.ExecuteNonQuery()

                ' Get the new sales_id
                dbcomm = New MySqlCommand("SELECT LAST_INSERT_ID()", conn, transaction)
                newSalesId = Convert.ToInt32(dbcomm.ExecuteScalar())

                ' INSERT tbl_sales_details + UPDATE stock per item
                For Each item As ListViewItem In ListView1.Items
                    Dim productId As Integer = Convert.ToInt32(item.Tag)
                    Dim qty As Integer = Convert.ToInt32(item.SubItems(1).Text)

                    ' Insert sales detail
                    sql = "INSERT INTO tbl_sales_details (sales_id, product_id, quantity) VALUES (" &
                          newSalesId & ", " & productId & ", " & qty & ")"
                    dbcomm = New MySqlCommand(sql, conn, transaction)
                    dbcomm.ExecuteNonQuery()

                    ' Deduct stock
                    sql = "UPDATE tbl_products SET quantity_in_stock = quantity_in_stock - " & qty &
                          " WHERE product_id = " & productId
                    dbcomm = New MySqlCommand(sql, conn, transaction)
                    dbcomm.ExecuteNonQuery()
                Next

                ' INSERT tbl_payments
                sql = "INSERT INTO tbl_payments (sales_id, rental_id, payment_date, amount_paid) VALUES (" &
                      newSalesId & ", NULL, NOW(), " & amountPaid & ")"
                dbcomm = New MySqlCommand(sql, conn, transaction)
                dbcomm.ExecuteNonQuery()

                ' COMMIT
                transaction.Commit()

                ' Show change
                Dim change As Decimal = amountPaid - totalAmount
                MsgBox("Sale saved successfully!" & vbCrLf &
                       "Sales ID: " & newSalesId & vbCrLf &
                       "Total: ₱" & totalAmount.ToString("F2") & vbCrLf &
                       "Amount Paid: ₱" & amountPaid.ToString("F2") & vbCrLf &
                       "Change: ₱" & change.ToString("F2"), MsgBoxStyle.Information, "Transaction Complete")

                ListView1.Items.Clear()
                Label6.Text = "0.00"
                TextBox2.Clear()
                CheckBox1.Checked = False
                DataGridView1.ClearSelection()

            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Transaction failed. Rolled back." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
            End Try

        Catch ex As Exception
            MsgBox("Database error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

End Class
