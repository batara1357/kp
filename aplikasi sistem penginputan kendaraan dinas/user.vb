Imports MySql.Data.MySqlClient
Imports System.Data
Public Class user
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
        Dim myadapter As New MySqlDataAdapter("select* from tbl_user", conn)
        Dim mydata As New DataTable
        myadapter.Fill(mydata)
        DataGridView1.DataSource = mydata
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub user_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        opentable()
        kondisiawal()
        sts()
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Enabled = False
        TextBox3.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "INPUT"
        Button2.Text = "HAPUS"
        Button3.Text = "EDIT"
    End Sub
    Sub isi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        ComboBox1.Enabled = True
        TextBox3.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            Call isi()
        Else
                Call koneksi()
            Dim InputData As String = "insert into tbl_user values('" & TextBox1.Text & "','" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & ComboBox1.Text & "')"
            cmd = New MySqlCommand(InputData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("input data berhasil")
                Call kondisiawal()


            End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "HAPUS" Then
            Button2.Text = "DELETE"
            Call isi()
        Else
            Call koneksi()
            Dim hapusdata As String = "DELETE from tbl_user where status='" & ComboBox1.Text & "'"
            cmd = New MySqlCommand(hapusdata, conn)
            cmd.ExecuteNonQuery()
            opentable()
            MsgBox("hapus data berhasil")
            Call kondisiawal()

        End If
    End Sub
    'koneksi database

    Sub sts()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New MySqlCommand("select * from tbl_user", conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(3))
        Loop
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "EDIT" Then
            Button3.Text = "UPDATE"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("silahkan isi field")
            Else
                Call koneksi()
                Dim editData As String = "update tbl_user set admin_master='" & TextBox2.Text & "', password='" & TextBox3.Text & "', status='" & ComboBox1.Text & "' where nrp='" & TextBox1.Text & "'"
                cmd = New MySqlCommand(editData, conn)
                cmd.ExecuteNonQuery()
                opentable()
                MsgBox("update data berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub
    'mengklik data yang di pilih
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox1.Text = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        TextBox2.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
        TextBox3.Text = DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value
        ComboBox1.Text = DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value


    End Sub
End Class