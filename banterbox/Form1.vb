Imports MySql.Data.MySqlClient
Public Class Form1
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=;database=banterboxdb")
    Dim dr As MySqlDataReader
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM logintbl WHERE username = '" & txtUsername.Text & "' AND password = '" & txtPassword.Text & "'", conn)
            dr = cmd.ExecuteReader
            If dr.HasRows = True Then
                MessageBox.Show("Login successfully!", "Banterbox", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Hide()
                banterbox.Show()
            ElseIf txtUsername.Text = "" And txtPassword.Text = "" Then
                MessageBox.Show("Please enter your Username and Password", "Banterbox", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("The Username and Password is incorrect", "Banterbox", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            dr.Close()
        End Try
    End Sub
End Class
