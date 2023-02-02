Public Class Form1

    Sub terbuka()
        LogoutToolStripMenuItem.Enabled = True
        aslogToolStripMenuItem.Enabled = True
        SatangToolStripMenuItem1.Enabled = True
        MasterToolStripMenuItem.Enabled = True
        If Label6.Text = "SLOG" Then
            MasterToolStripMenuItem.Enabled = False
        Else
            MasterToolStripMenuItem.Enabled = True
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label8.Text = Today

        Call terbuka()
    End Sub
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click

        loginadmin.Show()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        End
    End Sub

    Private Sub InputDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputDataToolStripMenuItem.Click
        aslog.ShowDialog()

    End Sub

    Private Sub StokBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StokBarangToolStripMenuItem.Click
        stokbarangsatang.ShowDialog()


    End Sub

    Private Sub RandisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RandisToolStripMenuItem.Click
        satang.ShowDialog()

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub InputRundisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles userToolStripMenuItem.Click
        user.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label10.Text = TimeOfDay
    End Sub

    Private Sub StatusAnggotaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusAnggotaToolStripMenuItem.Click
        statuskendaraan.ShowDialog()
    End Sub

    Private Sub DataAnggotaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataAnggotaToolStripMenuItem.Click
        dataanggota.ShowDialog()
    End Sub


End Class
