Imports MySql.Data.MySqlClient
Imports System.Data
Public Class dataanggota

    Public conn As MySqlConnection
    Dim cmd As New MySqlCommand
    Dim myadapter As New MySqlDataAdapter
    Dim mydata As New DataTable
    Dim rd As MySqlDataReader
    Dim ds As DataSet
    Dim service As New DataTable
    Sub koneksi()
        conn = New MySqlConnection("server='localhost';user id='root';password='';database='project'")
        conn.Open()
    End Sub
    Sub opentable()
        Dim myadapter As New MySqlDataAdapter("select* from tbl_anggota", conn)
        Dim mydata As New DataTable
        myadapter.Fill(mydata)
        DataGridView1.DataSource = mydata
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub dataanggota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        opentable()
        kondisiawal()
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From kendaraandinas where nrp='" & TextBox1.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("nama tidak ada")
            Else
                TextBox1.Text = rd.Item("nrp")
                TextBox2.Text = rd.Item("nama")
                ComboBox1.Text = rd.Item("satker")
            End If
        End If
    End Sub
    'text tidak bisa di isi
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Enabled = False


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
        ComboBox1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim InputData As String = "insert into tbl_anggota values('" & TextBox1.Text & "','" & TextBox2.Text & "', '" & ComboBox1.Text & "')"
                cmd = New MySqlCommand(InputData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("input data berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "EDIT" Then
            Button2.Text = "UPDATE"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim editData As String = "update tbl_anggota set nama='" & TextBox2.Text & "', satker='" & ComboBox1.Text & "' where nrp='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "HAPUS" Then
            Button3.Text = "DELETE"
            Call isi()
        Else
            Call koneksi()
            Dim hapusdata As String = "DELETE from tbl_anggota where nrp='" & TextBox1.Text & "'"
            cmd = New MySqlCommand(hapusdata, conn)
            cmd.ExecuteNonQuery()
            opentable()
            MsgBox("hapus data berhasil")
            Call kondisiawal()
        End If
    End Sub
    'mengklik data yang di pilih
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox1.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        TextBox2.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        ComboBox1.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value

    End Sub
    'pencarian data
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim adapter As New MySqlDataAdapter("select * from tbl_anggota where nrp like '%" & TextBox3.Text & "%'", conn)
        Dim table1 As New DataTable
        adapter.Fill(table1)
        DataGridView1.DataSource = table1
    End Sub
End Class