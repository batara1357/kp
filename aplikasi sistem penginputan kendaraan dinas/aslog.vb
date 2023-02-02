Imports MySql.Data.MySqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.VisualBasic

Public Class aslog
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
        Dim myadapter As New MySqlDataAdapter("select* from service", conn)
        Dim mydata As New DataTable
        myadapter.Fill(mydata)
        DataGridView1.DataSource = mydata
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub satanginput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        opentable()
        kondisiawal()
        persediaan()

    End Sub
    'menampilkan data otomatis
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * From kendaraandinas where nama='" & TextBox1.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("nama tidak ada")
            Else
                TextBox1.Text = rd.Item("nama")
                TextBox2.Text = rd.Item("nrp")
                ComboBox1.Text = rd.Item("satker")
                TextBox3.Text = rd.Item("nomer_al")
                TextBox4.Text = rd.Item("nomer_pusat")
                TextBox5.Text = rd.Item("merktype_kendaraan")
                TextBox6.Text = rd.Item("tahun_pembuatan")
                TextBox7.Text = rd.Item("nomer_mesin")
                TextBox12.Text = rd.Item("nrp")
            End If
        End If
    End Sub
    'text tidak bisa di isi
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox12.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox8.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox12.Enabled = False
        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
        TextBox8.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button6.Enabled = True
        Button1.Text = "INPUT"
        Button2.Text = "HAPUS"
        Button6.Text = "EDIT"
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
        TextBox12.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        TextBox8.Enabled = True
        TextBox10.Enabled = True
        TextBox11.Enabled = True
    End Sub
    'input data
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox2.Text = "" Or ComboBox3.Text = "" Or TextBox8.Text = "" Or TextBox10.Text = "" Or Label6.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim InputData As String = "insert into service values('" & TextBox12.Text & "','" & ComboBox2.Text & "', '" & ComboBox3.Text & "', '" & TextBox8.Text & "','" & TextBox11.Text & "', '" & TextBox10.Text & "','" & DateTimePicker1.Value & "','" & DateTimePicker2.Value & "')"
                cmd = New MySqlCommand(InputData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("input data berhasil")
                Call kondisiawal()


            End If
        End If
    End Sub
    'hapus data
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "HAPUS" Then
            Button2.Text = "DELETE"
            Call isi()
        Else
            Call koneksi()
            Dim hapusdata As String = "DELETE from service where nrp='" & TextBox12.Text & "'"
            cmd = New MySqlCommand(hapusdata, conn)
            cmd.ExecuteNonQuery()
            opentable()
            MsgBox("hapus data berhasil")
            Call kondisiawal()

        End If
    End Sub
    'menentukan tanggal otomatis
    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        DateTimePicker2.Value = DateAdd(DateInterval.Day, Val(TextBox9.Text), DateTimePicker1.Value.Date)
    End Sub
    'memanggil data dari data base menggunakan combo box
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Call koneksi()
        cmd = New MySqlCommand("Select * From stok where sparepart='" & Microsoft.VisualBasic.Left(ComboBox3.Text, 5) & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()

        ComboBox3.Text = rd.Item("sparepart")
        TextBox8.Text = rd.Item("kode_barang")
        TextBox11.Text = rd.Item("qty")

    End Sub
    'mengkoneksikan combobox dengan data base
    Sub persediaan()
        Call koneksi()
        ComboBox3.Items.Clear()
        cmd = New MySqlCommand("select * from stok", conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox3.Items.Add(rd.Item(1))
        Loop
    End Sub
    'validasi angka jumlah hari
    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'validasi angka qty
    Private Sub TextBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox11.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'cetak laporan
    Sub cetaklaporan()
        cetaklaporanservice.CrystalReportViewer1.ReportSource = cetaklaporanservice.laporanservice1
        cetaklaporanservice.Show()
    End Sub
    'mengklik data yang di pilih
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox12.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        ComboBox2.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        ComboBox3.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        TextBox8.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value
        TextBox11.Text = DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value
        TextBox10.Text = DataGridView1.Item(5, DataGridView1.CurrentRow.Index).Value
        DateTimePicker1.Value = DataGridView1.Item(6, DataGridView1.CurrentRow.Index).Value
        DateTimePicker2.Value = DataGridView1.Item(7, DataGridView1.CurrentRow.Index).Value
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call cetaklaporan()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "TUTUP" Then
            Me.Close()
        Else
            Call kondisiawal()
        End If
    End Sub


    Sub cetak()
        cmd = New MySqlCommand("select * from service where tanggal_masuk = '" & DateTimePicker1.Text & "' ", conn)
        myadapter = New MySqlDataAdapter(cmd)
        myadapter.Fill(service)

        conn.Close()
        conn.Dispose()

    End Sub

    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    Dim lap As New ReportDocument
    '    lap.Load("laporanservice.rpt")

    '    cetaklaporanservice.CrystalReportViewer1.SelectionFormula = "{service.tanggal_masuk}= Date(" & Year(DateTimePicker1.Value) & "," & Month(DateTimePicker1.Value) & "," & Day(DateTimePicker1.Value) & ")"
    '    cetaklaporanservice.CrystalReportViewer1.ReportSource = lap
    '    cetaklaporanservice.CrystalReportViewer1.Refresh()
    '    cetaklaporanservice.Show()
    'End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Button6.Text = "EDIT" Then
            Button6.Text = "UPDATE"
            Call isi()
        Else

            Call koneksi()
                Dim editData As String = "update service set jenis_service='" & ComboBox2.Text & "', sparepart='" & ComboBox3.Text & "', kode_barang='" & TextBox8.Text & "', jumlah='" & TextBox11.Text & "', keluhan='" & TextBox10.Text & "', tanggal_masuk='" & DateTimePicker1.Value & "', tanggal_kembali='" & DateTimePicker2.Value & "' where nrp='" & TextBox12.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()
            End If

    End Sub


End Class
