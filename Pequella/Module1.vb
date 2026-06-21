Imports MySql.Data.MySqlClient
Imports System.Data

Module Module1
    Public conn As New MySqlConnection("Server=localhost;Database=party_rental_db;Uid=root;Pwd=;")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Public CartTable As New DataTable
    Public subtotal As Decimal

    Public SessionUserId As Integer
    Public SessionFullName As String
    Public SessionRole As String

End Module