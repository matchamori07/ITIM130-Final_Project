Imports MySql.Data.MySqlClient

Public Class frmAdmin

    Private Sub frmAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyRolePermissions()
    End Sub

    Private Sub ApplyRolePermissions()
        lblLoggedInUser.Text = "Logged in as: " & SessionFullName & " (" & SessionRole & ")"

        If SessionRole = "Staff" Then
            Label8.Enabled = False
            Label9.Enabled = False
            Label16.Enabled = False
            Label15.Enabled = False
            Label17.Enabled = False
            PictureBox4.Enabled = False

        ElseIf SessionRole = "Limited" Then
            Label1.Enabled = False
            Label7.Enabled = False
            Label3.Enabled = False
            Label14.Enabled = False
            Label13.Enabled = False
            Label17.Enabled = False
            PictureBox4.Enabled = False

        End If
    End Sub

    Private Sub MenuLabels_Click(sender As Object, e As EventArgs) Handles Label1.Click, Label3.Click, Label7.Click, Label8.Click, Label9.Click, Label13.Click, Label14.Click, Label15.Click, Label16.Click, Label17.Click, Label18.Click
        Dim clickedLabel = CType(sender, Label)
        SetActiveMenuTab(clickedLabel)
        ShowPanel(clickedLabel)
    End Sub

    Private Sub ShowPanel(clickedLabel As Label)
        pnlProduct.Visible = False
        pnlCategory.Visible = False
        pnlAccount.Visible = False

        If clickedLabel Is Label7 Then
            pnlProduct.Visible = True
            pnlProduct.BringToFront()
        End If

        If clickedLabel Is Label3 Then
            pnlCategory.Visible = True
            pnlCategory.BringToFront()
        End If

    End Sub

    Private Sub SetActiveMenuTab(clickedLabel As Label)
        Dim allMenuLabels As Label() = {Label1, Label3, Label7, Label8, Label9, Label13, Label14, Label15, Label16, Label17, Label18}

        For Each lbl As Label In allMenuLabels
            lbl.ForeColor = Color.FromArgb(252, 231, 244)
            lbl.BackColor = Color.FromArgb(177, 64, 82)
            lbl.BorderStyle = BorderStyle.None
        Next

        clickedLabel.ForeColor = Color.FromArgb(177, 64, 82)
        clickedLabel.BackColor = Color.FromArgb(252, 231, 244)
        clickedLabel.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Close()
    End Sub
    ' tbl_products
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT p.product_id AS 'Product ID', " &
              "c.category_name AS 'Category', " &
              "p.product_name AS 'Name', " &
              "p.item_type AS 'Item Type', " &
              "p.quantity_in_stock AS 'Quantity', " &
              "p.unit_price AS 'Unit Price', " &
              "CASE WHEN p.status = 1 THEN 'Active' ELSE 'Inactive' END AS 'Status' " &
              "FROM tbl_products p " &
              "INNER JOIN tbl_categories c ON p.category_id = c.category_id " &
              "ORDER BY p.product_id ASC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "products")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "products"
        Catch ex As Exception
            MsgBox("Error loading grid: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("Sale")
        ComboBox3.Items.Add("Rental")

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Active")
        ComboBox2.Items.Add("Inactive")

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT product_id FROM tbl_products ORDER BY product_id ASC"
            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            ComboBox1.Items.Clear()
            While dbread.Read()
                ComboBox1.Items.Add(dbread("product_id"))
            End While
        Catch ex As Exception
            MsgBox("Error loading product IDs: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT category_id FROM tbl_categories ORDER BY category_id ASC"
            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            ComboBox4.Items.Clear()
            While dbread.Read()
                ComboBox4.Items.Add(dbread("category_id"))
            End While
        Catch ex As Exception
            MsgBox("Error loading category IDs: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT * FROM tbl_products WHERE product_id = " & Val(ComboBox1.Text)
            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            If dbread.Read() Then
                ComboBox4.Text = dbread("category_id").ToString()
                TextBox2.Text = dbread("product_name").ToString()
                ComboBox3.SelectedItem = dbread("item_type").ToString()
                NumericUpDown1.Value = Convert.ToDecimal(dbread("quantity_in_stock"))
                TextBox5.Text = dbread("unit_price").ToString()

                If dbread("status").ToString = "1" Then
                    ComboBox2.SelectedItem = "Active"
                Else
                    ComboBox2.SelectedItem = "Inactive"
                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Insert Product
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        If ComboBox4.Text.Trim = "" OrElse TextBox2.Text.Trim = "" OrElse
           ComboBox3.SelectedIndex = -1 OrElse NumericUpDown1.Text.Trim = "" OrElse
           TextBox5.Text.Trim = "" OrElse ComboBox2.SelectedIndex = -1 Then
            MsgBox("Please fill in all fields before inserting.")
            Exit Sub
        End If

        Dim statusVal = If(ComboBox2.SelectedItem.ToString = "Active", 1, 0)

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO tbl_products (category_id, product_name, item_type, quantity_in_stock, unit_price, status) " &
                  $"VALUES ({Val(ComboBox4.Text)}, '{TextBox2.Text.Trim}', '{ComboBox3.SelectedItem.ToString}', " &
                  $"{Val(NumericUpDown1.Text)}, {Convert.ToDecimal(TextBox5.Text)}, {statusVal})"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery

            If i > 0 Then
                MsgBox("Product inserted successfully.")
                ClearFields()
            Else
                MsgBox("Product was not inserted.")
            End If

        Catch ex As MySqlException
            MsgBox("Database error: " & ex.Message)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Refresh/View Product
    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Label7_Click(Nothing, Nothing)
        ClearFields()
        MsgBox("Product list refreshed.")
    End Sub

    ' Update Product
    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Please select a Product ID to edit.")
            Exit Sub
        End If

        If ComboBox4.Text.Trim = "" OrElse TextBox2.Text.Trim = "" OrElse
           ComboBox3.SelectedIndex = -1 OrElse NumericUpDown1.Text.Trim = "" OrElse
           TextBox5.Text.Trim = "" OrElse ComboBox2.SelectedIndex = -1 Then
            MsgBox("Please fill in all fields before updating.")
            Exit Sub
        End If

        Dim statusVal = If(ComboBox2.SelectedItem.ToString = "Active", 1, 0)
        Dim confirm = MsgBox("Are you sure you want to update this product?", MsgBoxStyle.YesNo, "Confirm Update")

        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "UPDATE tbl_products SET " &
                  $"category_id = {Val(ComboBox4.Text)}, " &
                  $"product_name = '{TextBox2.Text.Trim}', " &
                  $"item_type = '{ComboBox3.SelectedItem.ToString}', " &
                  $"quantity_in_stock = {Val(NumericUpDown1.Text)}, " &
                  $"unit_price = {Convert.ToDecimal(TextBox5.Text)}, " &
                  $"status = {statusVal} " &
                  $"WHERE product_id = {Val(ComboBox1.SelectedItem)}"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery

            If i > 0 Then
                MsgBox("Product updated successfully.")
                ClearFields()

            Else
                MsgBox("Product was not updated.")
            End If

        Catch ex As MySqlException
            MsgBox("Database error: " & ex.Message)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Delete Product (soft delete)
    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Please select a Product ID to delete.")
            Exit Sub
        End If

        Dim confirm = MsgBox("Are you sure you want to deactivate this product?" & vbCrLf &
                                             "It will be hidden from new transactions but history is kept.",
                                             MsgBoxStyle.YesNo, "Confirm Deactivate")
        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = $"UPDATE tbl_products SET status = 0 WHERE product_id = {Val(ComboBox1.SelectedItem)}"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery

            If i > 0 Then
                MsgBox("Product deactivated successfully.")
                ClearFields()

            Else
                MsgBox("Product was not deactivated.")
            End If

        Catch ex As MySqlException
            MsgBox("Database error: " & ex.Message)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub ClearFields()
        ComboBox1.SelectedIndex = -1
        ComboBox1.Text = ""
        ComboBox2.SelectedIndex = -1
        ComboBox2.Text = ""
        ComboBox3.SelectedIndex = -1
        ComboBox3.Text = ""
        ComboBox4.SelectedIndex = -1
        ComboBox4.Text = ""
        TextBox2.Clear()
        TextBox5.Clear()
        NumericUpDown1.Value = 0
    End Sub

    ' Search Products
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT p.product_id AS 'Product ID', " &
                  "c.category_name AS 'Category', " &
                  "p.product_name AS 'Name', " &
                  "p.item_type AS 'Item Type', " &
                  "p.quantity_in_stock AS 'Quantity', " &
                  "p.unit_price AS 'Unit Price', " &
                  "CASE WHEN p.status = 1 THEN 'Active' ELSE 'Inactive' END AS 'Status' " &
                  "FROM tbl_products p " &
                  "INNER JOIN tbl_categories c ON p.category_id = c.category_id " &
                  $"WHERE p.product_name LIKE '%{TextBox3.Text}%' " &
                  "ORDER BY p.product_id ASC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet
            DataAdapter1.Fill(ds, "products search")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "products search"

        Catch ex As MySqlException
            MsgBox("Error in collecting data from Database. Error is :" & ex.Message)
        Catch ex As Exception
            MsgBox("Error in collecting data from Database. Error is :" & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' tbl_categories

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        LoadCategoryGrid()
        LoadCategoryIDs()
    End Sub

    Private Sub LoadCategoryGrid(Optional keyword As String = "")
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT category_id AS 'Category ID', category_name AS 'Category Name', description AS 'Description' " &
              "FROM tbl_categories " &
              $"WHERE category_name LIKE '%{keyword}%' " &
              "ORDER BY category_id ASC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "categories")
            DataGridView2.DataSource = ds
            DataGridView2.DataMember = "categories"
        Catch ex As Exception
            MsgBox("Error loading categories: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadCategoryIDs()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT category_id FROM tbl_categories ORDER BY category_id ASC"
            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            ComboBox5.Items.Clear()
            While dbread.Read()
                ComboBox5.Items.Add(dbread("category_id"))
            End While
        Catch ex As Exception
            MsgBox("Error loading category IDs: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Search Categories
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        LoadCategoryGrid(TextBox1.Text.Trim())
    End Sub

    ' Refresh/View Categories
    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click
        LoadCategoryGrid()
        LoadCategoryIDs()
        ClearCategoryFields()
        MsgBox("Category list refreshed.")
    End Sub

    ' Insert Categories
    Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click
        If TextBox6.Text.Trim() = "" OrElse TextBox4.Text.Trim() = "" Then
            MsgBox("Please fill in Category Name and Description.")
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO tbl_categories (category_name, description) VALUES ('" &
              TextBox6.Text.Trim() & "', '" & TextBox4.Text.Trim() & "')"
            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Category inserted successfully.")
                ClearCategoryFields()
                LoadCategoryGrid()
                LoadCategoryIDs()
            Else
                MsgBox("Category was not inserted.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Update Categories
    Private Sub Label36_Click(sender As Object, e As EventArgs) Handles Label36.Click
        If ComboBox5.SelectedIndex = -1 Then
            MsgBox("Please select a Category ID to update.")
            Exit Sub
        End If

        If TextBox6.Text.Trim() = "" OrElse TextBox4.Text.Trim() = "" Then
            MsgBox("Please fill in all fields before updating.")
            Exit Sub
        End If

        Dim confirm = MsgBox("Are you sure you want to update this category?", MsgBoxStyle.YesNo, "Confirm Update")
        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "UPDATE tbl_categories SET category_name = '" & TextBox6.Text.Trim() & "', " &
              "description = '" & TextBox4.Text.Trim() & "' " &
              "WHERE category_id = " & Val(ComboBox5.Text)
            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Category updated successfully.")
                ClearCategoryFields()
                LoadCategoryGrid()
            Else
                MsgBox("Category was not updated.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Delete Categories
    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click
        If ComboBox5.SelectedIndex = -1 Then
            MsgBox("Please select a Category ID to delete.")
            Exit Sub
        End If

        Dim confirm = MsgBox("Are you sure you want to delete this category?", MsgBoxStyle.YesNo, "Confirm Delete")
        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "DELETE FROM tbl_categories WHERE category_id = " & Val(ComboBox5.Text)
            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Category deleted successfully.")
                ClearCategoryFields()
                LoadCategoryGrid()
                LoadCategoryIDs()
            Else
                MsgBox("Category was not deleted.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ClearCategoryFields()
        ComboBox5.SelectedIndex = -1
        ComboBox5.Text = ""
        TextBox6.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.SelectedIndex = -1 Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT category_name, description FROM tbl_categories WHERE category_id = " & Val(ComboBox5.Text)
            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            If dbread.Read() Then
                TextBox6.Text = dbread("category_name").ToString()
                TextBox4.Text = dbread("description").ToString()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub











    ' pnlAccount 
    Private Sub LoadUsersGrid()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT user_id AS 'User ID', " &
              "CONCAT(first_name, ' ', last_name) AS 'Full Name', " &
              "username AS 'Username', " &
              "password AS 'Password', " &
              "role AS 'Role', " &
              "CASE WHEN status = 1 THEN 'Active' ELSE 'Inactive' END AS 'Status' " &
              "FROM tbl_users ORDER BY user_id ASC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "users")
            DataGridView3.DataSource = ds
            DataGridView3.DataMember = "users"
        Catch ex As Exception
            MsgBox("Error loading users: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadUserRoles()
        ComboBox6.Items.Clear()
        ComboBox6.Items.Add("Admin")
        ComboBox6.Items.Add("Staff")
        ComboBox6.Items.Add("Limited")
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If DataGridView3.SelectedRows.Count = 0 Then Exit Sub

        Dim row = DataGridView3.SelectedRows(0)
        Dim fullName As String = row.Cells("Full Name").Value.ToString()
        Dim nameParts = fullName.Split(" "c)

        TextBox7.Text = nameParts(0)
        TextBox8.Text = If(nameParts.Length > 1, nameParts(1), "")
        TextBox9.Text = row.Cells("Username").Value.ToString()
        TextBox10.Text = ""  ' never show existing password
        ComboBox6.SelectedItem = row.Cells("Role").Value.ToString()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If SessionRole <> "Admin" Then
            MsgBox("Access denied. Only Admin can access account settings.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        pnlAccount.Visible = True
        pnlAccount.BringToFront()

        LoadUsersGrid()
        LoadUserRoles()
    End Sub

    ' Save — insert new account
    Private Sub Label41_Click(sender As Object, e As EventArgs) Handles Label41.Click
        If TextBox7.Text.Trim() = "" OrElse TextBox8.Text.Trim() = "" OrElse
           TextBox9.Text.Trim() = "" OrElse TextBox10.Text.Trim() = "" OrElse
           ComboBox6.SelectedIndex = -1 Then
            MsgBox("Please fill in all fields.")
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO tbl_users (first_name, last_name, username, password, role, status) VALUES ('" &
                  TextBox7.Text.Trim() & "', '" & TextBox8.Text.Trim() & "', '" &
                  TextBox9.Text.Trim() & "', '" & TextBox10.Text.Trim() & "', '" &
                  ComboBox6.SelectedItem.ToString() & "', 1)"
            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Account created successfully.")
                ClearUserFields()
                LoadUsersGrid()
            Else
                MsgBox("Account was not created.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Update — edit name, role, or reset password (cannot edit own account)
    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click
        If DataGridView3.SelectedRows.Count = 0 Then
            MsgBox("Please select a user to update.")
            Exit Sub
        End If

        Dim selectedUserId As Integer = Convert.ToInt32(DataGridView3.SelectedRows(0).Cells("User ID").Value)

        ' Prevent admin from changing their own role
        If selectedUserId = SessionUserId Then
            MsgBox("You cannot edit your own account.")
            Exit Sub
        End If

        If TextBox7.Text.Trim() = "" OrElse TextBox8.Text.Trim() = "" OrElse
           ComboBox6.SelectedIndex = -1 Then
            MsgBox("Please fill in all required fields.")
            Exit Sub
        End If

        Dim confirm = MsgBox("Update this account?", MsgBoxStyle.YesNo, "Confirm Update")
        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Update password only if a new one was typed
            If TextBox10.Text.Trim() <> "" Then
                sql = "UPDATE tbl_users SET first_name = '" & TextBox7.Text.Trim() & "', " &
                      "last_name = '" & TextBox8.Text.Trim() & "', " &
                      "role = '" & ComboBox6.SelectedItem.ToString() & "', " &
                      "password = '" & TextBox10.Text.Trim() & "' " &
                      "WHERE user_id = " & selectedUserId
            Else
                sql = "UPDATE tbl_users SET first_name = '" & TextBox7.Text.Trim() & "', " &
                      "last_name = '" & TextBox8.Text.Trim() & "', " &
                      "role = '" & ComboBox6.SelectedItem.ToString() & "' " &
                      "WHERE user_id = " & selectedUserId
            End If

            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Account updated successfully.")
                ClearUserFields()
                LoadUsersGrid()
            Else
                MsgBox("Account was not updated.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Deactivate — set status = 0
    Private Sub Label43_Click(sender As Object, e As EventArgs) Handles Label43.Click
        If DataGridView3.SelectedRows.Count = 0 Then
            MsgBox("Please select a user to deactivate.")
            Exit Sub
        End If

        Dim selectedUserId As Integer = Convert.ToInt32(DataGridView3.SelectedRows(0).Cells("User ID").Value)

        If selectedUserId = SessionUserId Then
            MsgBox("You cannot deactivate your own account.")
            Exit Sub
        End If

        Dim confirm = MsgBox("Deactivate this account? They will no longer be able to log in.",
                             MsgBoxStyle.YesNo, "Confirm Deactivate")
        If confirm = MsgBoxResult.No Then Exit Sub

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "UPDATE tbl_users SET status = 0 WHERE user_id = " & selectedUserId
            dbcomm = New MySqlCommand(sql, conn)
            Dim i = dbcomm.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Account deactivated successfully.")
                ClearUserFields()
                LoadUsersGrid()
            Else
                MsgBox("Account was not deactivated.")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Refresh
    Private Sub Label40_Click(sender As Object, e As EventArgs) Handles Label40.Click
        LoadUsersGrid()
        ClearUserFields()
        MsgBox("User list refreshed.")
    End Sub

    ' Clear fields
    Private Sub ClearUserFields()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        ComboBox6.SelectedIndex = -1
    End Sub

End Class