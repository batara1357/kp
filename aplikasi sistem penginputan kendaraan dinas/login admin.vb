Imports mysql.data.MySqlClient

Public Class loginadmin
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
    Sub terbuka()
        Form1.Label2.Text = rd!nrp
        Form1.Label4.Text = rd!admin_master
        Form1.Label6.Text = rd!status

        Form1.LogoutToolStripMenuItem.Enabled = True
        Form1.aslogToolStripMenuItem.Enabled = True
        Form1.SatangToolStripMenuItem1.Enabled = True
        Form1.MasterToolStripMenuItem.Enabled = True
        If Form1.Label6.Text = "SLOG" Then
            Form1.MasterToolStripMenuItem.Enabled = False
        Else
            Form1.MasterToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub Formlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("kode admin dan password tidak boleh kosong")
        Else
            Call koneksi()
            cmd = New MySqlCommand("select * from tbl_user where admin_master='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                Me.Hide()

                Form1.Show()


                Call terbuka()
                MsgBox("login berhasil")
            Else
                MsgBox("nama dan password salah")
            End If
        End If





    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = CChar("")
        Else
            TextBox2.PasswordChar = CChar("x")
        End If
    End Sub

    Private Sub loginadmin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

End Class