Imports MySql.Data.MySqlClient
Imports System.Data
Public Class stokbarangsatang
    Public conn As MySqlConnection
    Dim cmd As New MySqlCommand
    Dim myadapter As New MySqlDataAdapter
    Dim mydata As New DataTable
    Dim rd As MySqlDataReader
    Dim ds As DataSet
    Sub koneksi()
        conn = New MySqlConnection("server='localhost';user id='root';password='';database='project'")
        conn.Open()
    End Sub
    Sub opentable()
        Dim myadapter As New MySqlDataAdapter("select* from stok", conn)
        Dim mydata As New DataTable
        myadapter.Fill(mydata)
        DataGridView1.DataSource = mydata
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub stokbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        opentable()
        Call kondisiawal()
    End Sub
    'text tidak bisa di isi
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "INPUT"
        Button2.Text = "EDIT"
        Button3.Text = "HAPUS"
    End Sub
    'text bisa di isi
    Sub isi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
    End Sub
    'input data
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim InputData As String = "insert into stok values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "')"
                cmd = New MySqlCommand(InputData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("input data berhasil")
                Call kondisiawal()
            End If

        End If
    End Sub
    'edit data
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "EDIT" Then
            Button2.Text = "UPDATE"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim editData As String = "update stok set sparepart='" & TextBox2.Text & "',qty='" & TextBox3.Text & "' where kode_barang='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub
    'klik data yang di pilih
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox1.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        TextBox2.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
    End Sub
    'hapus data 
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "HAPUS" Then
            Button3.Text = "DELETE"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim editData As String = "DELETE from stok where kode_barang='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()

            End If
        End If
    End Sub
    'validasi angka jumlah stok
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class