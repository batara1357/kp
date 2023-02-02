Imports MySql.Data.MySqlClient
Imports System.Data
Public Class satang
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
        Dim myadapter As New MySqlDataAdapter("select * from kendaraandinas order by nama asc", conn)
        Dim mydata As New DataTable
        myadapter.Fill(mydata)
        DataGridView1.DataSource = mydata
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.Sort(DataGridView1.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
    End Sub
    Private Sub menuutama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        opentable()
        Call kondisiawal()
        Call databarang()
    End Sub
    'text tidak bisa di isi
    Sub kondisiawal()
        TextBox1.Text = ""
        ComboBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox2.Text = ""
        TextBox1.Enabled = False
        ComboBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        ComboBox2.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button1.Text = "INPUT"
        Button2.Text = "EDIT"
        Button3.Text = "HAPUS"
        Button4.Text = "TUTUP"
    End Sub
    'text bisa di isi
    Sub isi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        ComboBox1.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        ComboBox2.Enabled = True
    End Sub
    'input data
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim InputData As String = "insert into kendaraandinas values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "' , '" & TextBox3.Text & "', '" & TextBox4.Text & "', '" & TextBox5.Text & "','" & TextBox6.Text & "', '" & TextBox7.Text & "', '" & ComboBox2.Text & "')"
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
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim editData As String = "update kendaraandinas set nrp='" & TextBox2.Text & "', satker='" & ComboBox1.Text & "', nomer_al='" & TextBox3.Text & "', nomer_pusat='" & TextBox4.Text & "', merktype_kendaraan='" & TextBox5.Text & "', tahun_pembuatan='" & TextBox6.Text & "', nomer_mesin='" & TextBox7.Text & "', kondisi='" & ComboBox2.Text & "' where nama='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()

            End If
        End If
    End Sub
    'tutup form
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "TUTUP" Then
            Me.Close()
        Else
            Call kondisiawal()
        End If
    End Sub
    'hapus data
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "HAPUS" Then
            Button3.Text = "DELETE"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim hapusdata As String = "DELETE from kendaraandinas where nama='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(hapusdata, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("hapus data berhasil")
                Call kondisiawal()

            End If
        End If
    End Sub
    'pencarian data
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim adapter As New MySqlDataAdapter("select * from Kendaraandinas where tahun_pembuatan like '%" & TextBox8.Text & "%'", conn)
        Dim table1 As New DataTable
        adapter.Fill(table1)
        DataGridView1.DataSource = table1
        Call databarang()
    End Sub
    'melihat banyaknya data pada datagridview
    Sub databarang()
        Dim jmldata
        jmldata = DataGridView1.RowCount
        Label12.Text = jmldata - 1
    End Sub
    'mengklik data yang di pilih
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox1.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        TextBox2.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        ComboBox1.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        TextBox3.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value
        TextBox4.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value
        TextBox5.Text = DataGridView1.Item(5, DataGridView1.CurrentRow.Index).Value
        TextBox6.Text = DataGridView1.Item(6, DataGridView1.CurrentRow.Index).Value
        TextBox7.Text = DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value
        ComboBox2.Text = DataGridView1.Item(8, DataGridView1.CurrentRow.Index).Value
    End Sub
    'validasi hanya angka
    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'cetak laporan menggunakan crystal report
    Sub cetaklaporan()
        cetaklaporanrundis.CrystalReportViewer1.ReportSource = cetaklaporanrundis.laporanrundis1
        cetaklaporanrundis.Show()
        DataGridView1.Sort(DataGridView1.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Call cetaklaporan()
    End Sub
    'Refresh form
    Sub bersih()
        TextBox1.Text = ""
        ComboBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox2.Text = ""
        Label12.Text = ""
        databarang()
        opentable()
    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        bersih()
    End Sub


End Class