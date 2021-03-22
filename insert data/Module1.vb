Imports System.Data.Odbc
Module Module1
    Public conn As New OdbcConnection
    Public da As New OdbcDataAdapter
    Public ds As New DataSet
    Public Mydb As String
    Public cmd As OdbcCommand
    Public dr As OdbcDataReader
    Public DMlsql As New OdbcCommand
    Public GRID As New OdbcDataAdapter
    Public record As New BindingSource
    Public Sub koneksi()
        Mydb = "Driver={Mysql ODBC 3.51 Driver};Database=informasi_mahasiswa;server=localhost;uid=root"
        conn = New OdbcConnection(Mydb)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

End Module
