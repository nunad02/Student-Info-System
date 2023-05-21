Imports System.Net
Imports MySql.Data.MySqlClient
Public Class banterbox
    Dim conn As MySqlConnection
    Dim command As MySqlCommand
    Dim dr As MySqlDataReader
    Public Sub loadMessage()
        conn = New MySqlConnection
        conn.ConnectionString = "server=" & txtServerIP.Text & ";port=3306;userid=root;password='';database=banterboxdb"
        Try
            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If
            Dim cmd As New MySqlCommand("SELECT * FROM `banterboxtbl` ", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                rtbMessage.Text = dr.Item("message")
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub banterbox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim host_name As String = Dns.GetHostName()
        Dim ip_address As String = Dns.GetHostEntry(host_name).AddressList(0).ToString()
        Me.Text = "SYSTEM IP: " & ip_address
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call loadMessage()
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If txtUser.Text = "" Then MsgBox("Type a User name!!") : Exit Sub
        loadMessage()
        conn = New MySqlConnection
        conn.ConnectionString = "server=" & txtServerIP.Text & ";port=3306;userid=root;password='';database=banterboxdb"
        Try
            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If
            Dim cmd As New MySqlCommand("UPDATE `banterboxtbl` SET `message`=@message", conn)
            cmd.Parameters.Clear()
            If btnConnect.Text = "Connect" Then
                cmd.Parameters.AddWithValue("message", UCase(txtUser.Text) & " | " & TimeOfDay.ToString("h:mm:ss tt") & " Connected!" & vbCrLf & vbCrLf &
                rtbMessage.Text)
                cmd.ExecuteNonQuery()
                btnConnect.Text = "Disconnect"
                txtServerIP.Enabled = False
                txtUser.Enabled = False
                loadMessage()
                Timer1.Enabled = True
            Else
                cmd.Parameters.AddWithValue("message", UCase(txtUser.Text) & " | " & TimeOfDay.ToString("h:mm:ss tt") & " Disconnected!" & vbCrLf & vbCrLf &
                rtbMessage.Text)
                cmd.ExecuteNonQuery()
                btnConnect.Text = "Connect"
                txtMessage.Text = ""
                rtbMessage.Text = ""
                txtServerIP.Enabled = True
                txtUser.Enabled = True
                Timer1.Enabled = False
            End If
        Catch ex As Exception
            Timer1.Enabled = False
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If txtUser.Text = "" Then MsgBox("Type a User name!!") : Exit Sub
        If txtMessage.Text = "" Then MsgBox("Type a Message") : Exit Sub
        conn = New MySqlConnection
        conn.ConnectionString = "server=" & txtServerIP.Text & ";port=3306;userid=root;password='';database=banterboxdb"
        Try
            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If
            Dim cmd As New MySqlCommand("UPDATE `banterboxtbl` SET `message`=@message", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("message", UCase(txtUser.Text) & " | " & TimeOfDay.ToString("h:mm:ss tt") & vbCrLf & "- " & txtMessage.Text & " -" &
            vbCrLf & vbCrLf & rtbMessage.Text)
            cmd.ExecuteNonQuery()
            loadMessage()
            txtMessage.Text = ""
            txtMessage.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class