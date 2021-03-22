Imports System.Data.Odbc
Imports System.Data.DataTable
Public Class Form1
    Public conn As New OdbcConnection
    Public da As New OdbcDataAdapter
    Public ds As New DataSet
    Public Mydb As String
    Public cmd As OdbcCommand
    Public dttbl As DataTable
    Dim index As Integer
    Public reader As OdbcDataReader

    Sub kondisiawal()
        MaskedTextBox1.Text = ""
        MaskedTextBox2.Text = ""
        MaskedTextBox3.Text = ""
        MaskedTextBox4.Text = ""
        Button1.Text = "simpan"
        Button2.Text = "ubah"
        Button3.Text = "backspace"
        Button4.Text = "delet"
        Button5.Text = "all data"
    End Sub

    Public Sub kosongkan()
        MaskedTextBox1.Clear()
        MaskedTextBox2.Clear()
        MaskedTextBox3.Clear()
        MaskedTextBox4.Clear()
        MaskedTextBox1.Focus()

    End Sub

    Public Sub Databaru()
        MaskedTextBox2.Clear()
        MaskedTextBox3.Clear()
        MaskedTextBox4.Clear()
        MaskedTextBox2.Focus()
    End Sub
    Public Sub koneksi()
        Mydb = "Driver={Mysql ODBC 3.51 Driver};Database=informasi_mahasiswa;server=localhost;uid=root"
        conn = New OdbcConnection(Mydb)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Call koneksi()
        da = New OdbcDataAdapter("select * from tbl_biodata", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_biodata")
        DataGridView1.DataSource = (ds.Tables("tbl_biodata"))
        conn.Close()

        Call koneksi()
        Try
            Dim str As String
            str = "insert into tbl_biodata values ('" & MaskedTextBox1.Text & "','" & MaskedTextBox2.Text & "', '" & MaskedTextBox3.Text & "', '" & MaskedTextBox4.Text & "')"
            cmd = New OdbcCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Insert Data Siswa Berhasil Dilakukan")

        Catch ex As Exception
            MessageBox.Show("Insert data siswa gagal dilakukan.")
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MaskedTextBox1.Text = "" Or MaskedTextBox2.Text = "" Or MaskedTextBox3.Text = "" Or MaskedTextBox4.Text = "" Then
            MsgBox("pastikan semua terisi")
        End If

        Call koneksi()
        Try
            Dim editdata As String
            editdata = "Update tbl_biodata set id= '" & MaskedTextBox1.Text & "',nama='" & MaskedTextBox2.Text & "',sekolah= '" & MaskedTextBox3.Text & "',alamat='" & MaskedTextBox4.Text & "'where id='" & MaskedTextBox1.Text & "'"
            cmd = New OdbcCommand(editdata, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Update Data Siswa Berhasil Dilakukan")
            Call kondisiawal()
        Catch ex As Exception
            MessageBox.Show("Update data siswa gagal dilakukan.")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If MaskedTextBox1.Text = "" Then
            MsgBox("harus di isi dulu")
            MaskedTextBox1.Focus()
            Exit Sub
        Else
            Call koneksi()
            Dim hapus As String = "Delete from informasi_mahasiswa where tbl_biodata ='" & MaskedTextBox1.Text & "','" & MaskedTextBox2.Text & "', '" & MaskedTextBox3.Text & "', '" & MaskedTextBox4.Text & "'"
            cmd = New OdbcCommand(hapus, conn)
            Call kosongkan()

        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If MaskedTextBox1.Text = "" Or MaskedTextBox2.Text = "" Or MaskedTextBox3.Text = "" Or MaskedTextBox4.Text = "" Then
            MsgBox("pastikan semua terisi")
        End If

        Call koneksi()
            Dim delete As String
        delete = "delete from  tbl_biodata  where id='" & MaskedTextBox1.Text & "'"
            cmd = New OdbcCommand(delete, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("delete Data Siswa Berhasil Dilakukan")
            Call kondisiawal()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Call koneksi()
        da = New OdbcDataAdapter("select * from tbl_biodata", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_biodata")
        DataGridView1.DataSource = (ds.Tables("tbl_biodata"))
        conn.Close()

    End Sub

    Private Sub MaskedTextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select * from  tbl_biodata where id='" & MaskedTextBox1.Text & "'", conn)
            reader = cmd.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                MaskedTextBox2.Text = reader.Item("nama")
                MaskedTextBox3.Text = reader.Item("sekolah")
                MaskedTextBox4.Text = reader.Item("alamat")
            Else
                MsgBox("data tidak ada")

            End If
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kondisiawal()

    End Sub
End Class
