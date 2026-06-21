Imports MySql.Data.MySqlClient

Public Class frmLogin

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If txtUsername.Text.Trim() = "" OrElse txtPassword.Text.Trim() = "" Then
            MsgBox("Please enter your username and password.")
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT user_id, first_name, last_name, role " &
                  "FROM tbl_users " &
                  "WHERE username = '" & txtUsername.Text.Trim() & "' " &
                  "AND password = '" & txtPassword.Text.Trim() & "' " &
                  "AND status = 1"

            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            If dbread.Read() Then
                ' set session ONLY here, after a successful login
                SessionUserId = dbread("user_id")
                SessionFullName = dbread("first_name").ToString() & " " & dbread("last_name").ToString()
                SessionRole = dbread("role").ToString()

                dbread.Close()
                frmAdmin.Show()
                Me.Close()
            Else
                MsgBox("Invalid username or password.", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            If dbread IsNot Nothing Then dbread.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

End Class