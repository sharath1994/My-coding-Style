Imports System.IO.Ports
Imports System.Threading
Imports System.ComponentModel
Imports System.Text
Imports System.IO
Imports System

Public Class Form1

    Dim myPort As Array

    Public comOpen As Boolean

    Private readBuffer As String = String.Empty

    Dim stopMe As Boolean

    Delegate Sub SetTextCallback(ByVal [text] As String) 'Calling safe Thread

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myPort = IO.Ports.SerialPort.GetPortNames() 'Geting list of all opened ports
        ComboBox1.Items.AddRange(myPort)
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SerialPort1.PortName = ComboBox1.Text
        SerialPort1.BaudRate = ComboBox2.Text
        Button2.Enabled = True
        Button3.Enabled = True
        TextBox1.Select()
        Try

            SerialPort1.Open() 'Connecting to Serialport
            SerialPort1.ReadTimeout = 10000
            comOpen = SerialPort1.IsOpen
            'Dim speech
            'speech = CreateObject("sapi.spvoice")
            'speech.speak("Connected to serialport Sucessful")

        Catch ex As Exception
            comOpen = False
            MsgBox("Error Open: " & ex.Message) 'Exception if Port is not open
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = Nothing Then
            'Dim speech
            'speech = CreateObject("sapi.spvoice")
            'speech.speak("No text has been entered")
        End If
        SerialPort1.Write(TextBox2.Text) 'concatenate with \n
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button1.Enabled = True
        Button2.Enabled = False
        If MessageBox.Show("Do you really want to close the Serialport", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Try
                SerialPort1.DiscardInBuffer()
                SerialPort1.Close()
            Catch ex As IOException
                'Dim speech
                'speech = CreateObject("sapi.spvoice")
                'speech.speak("Serialport Closed")
            End Try
        Else
            Dim t As New Thread(AddressOf ClosePort)
            t.Start()
        End If
    End Sub
    Private Sub ClosePort()
        If SerialPort1.IsOpen Then SerialPort1.Close()
    End Sub

    Private Sub SerialPort1_ErrorReceived(sender As Object, e As SerialErrorReceivedEventArgs) Handles SerialPort1.ErrorReceived
        Try
            SerialPort1.Close()

        Catch ex As IOException

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Button1.Enabled = True
        ComboBox2.Text = "9600"
    End Sub


    Public Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try

            Dim i As Integer = SerialPort1.BytesToRead() 'Declearing Number of bytes to read

            If i >= 9 Then
                Dim data As Byte() = New Byte(i) {}

                'For Each item As Byte In data
                SerialPort1.Read(data, 0, i) 'Reading data from Serialport
                'If comOpen = True Then
                ReceivedText((data(0))) 'Sending the recieved data to another thread for displaying
                ReceivedText((data(1)))
                ReceivedText((data(2)))
                ReceivedText((data(3)))
                ReceivedText((data(4)))
                ReceivedText((data(5)))
                ReceivedText((data(6)))
                ReceivedText((data(7)))
                ReceivedText((data(8)))
                ReceivedText((data(9)))

            End If
            'Next
            'End If
        Catch ex As IOException
        Catch ex As IndexOutOfRangeException
        End Try
    End Sub

    Public Sub ReceivedText(ByVal [text] As String) 'displaying the data in text box using safe thread operation
        If Me.TextBox1.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.TextBox1.Text &= [text] - 48 & vbCrLf  'append text

        End If

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            SerialPort1.Write(TextBox2.Text)
        End If
    End Sub
End Class
