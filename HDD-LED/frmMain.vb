Imports LibUsbDotNet
Imports LibUsbDotNet.Main
Imports System.ComponentModel

Public Class frmMain
    Dim MyUsbDevice As UsbDevice = Nothing
    Dim Configurazione As ClassConfig
    Dim lstDrivers As List(Of urcLed)

    Private Sub Me_Load(sender As Object, e As EventArgs) Handles Me.Load
        Configurazione = ClassConfig.Carica

        TrackBarLuminosita.Value = Configurazione.Luminosita
        TrackBarLeggi.Value = Configurazione.Leggi
        TrackBarScrivi.Value = Configurazione.Scrivi

        TrackBarLuminosita.Width = 0

        FlowLayoutPanelLeds.Width = 0
        FlowLayoutPanelLeds.Height = 0

        ListBoxLog.Width = 0
        ListBoxLog.Height = 0
        ListBoxLog.Items.Clear()

        Dim drcW As Integer = 100
        Dim drcH As Integer = 80

        lstDrivers = New List(Of urcLed) From {
            New urcLed("_Total", urcLed.enmTipo.CPU)
        }

        For Each drive As IO.DriveInfo In My.Computer.FileSystem.Drives
            addListi("Found Driver " & drive.Name & " " & drive.DriveType.ToString & " " & If(drive.DriveType = IO.DriveType.Fixed OrElse drive.DriveType = IO.DriveType.Removable, String.Empty, "IGNORED"))

            If drive.DriveType = IO.DriveType.Fixed OrElse drive.DriveType = IO.DriveType.Removable Then
                lstDrivers.Add(New urcLed(drive.Name.Substring(0, 2), urcLed.enmTipo.Drive))

            End If

        Next

        For Each adapter As Net.NetworkInformation.NetworkInterface In Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
            Dim properties As Net.NetworkInformation.IPInterfaceProperties = adapter.GetIPProperties()
            addListi("Found Network " & adapter.Description & " " & adapter.OperationalStatus.ToString)
            If adapter.OperationalStatus = Net.NetworkInformation.OperationalStatus.Up Then
                lstDrivers.Add(New urcLed(adapter.Description.Replace("(", "[").Replace(")", "]"), urcLed.enmTipo.Lan))

            End If

        Next

        Dim MMDeviceEnumeratorX As New NAudio.CoreAudioApi.MMDeviceEnumerator
        'For Each MMDeviceX As NAudio.CoreAudioApi.MMDevice In MMDeviceEnumeratorX.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.Active)
        Dim MMDeviceX As NAudio.CoreAudioApi.MMDevice = MMDeviceEnumeratorX.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia)
        lstDrivers.Add(New urcLed(MMDeviceX))
        'Next

        For Each cldX As urcLed In lstDrivers
            cldX.BorderStyle = BorderStyle.FixedSingle
            cldX.Width = drcW
            cldX.Height = drcH
            cldX.Inizializza()
            cldX.SetInterval(TrackBarLeggi.Value)

            AddHandler cldX.Log, Sub(_sender, _e)
                                     addListi(_e.Messaggio)
                                 End Sub

            FlowLayoutPanelLeds.Controls.Add(cldX)

        Next

        Try
            MyUsbDevice = UsbDevice.OpenUsbDevice(New UsbDeviceFinder(&H16C0, &H5DF))

            If MyUsbDevice Is Nothing Then
                addListi("Device Not Found.")

            Else
                addListi("Device Found.")

                If MyUsbDevice.Open() Then
                    addListi("Device Open.")

                Else
                    addListi("Device Not Open.")
                    MyUsbDevice = Nothing

                End If

            End If

        Catch ex As Exception
            addListi(ex.Message)

        End Try

        TrackBarScrivi_Scroll(TrackBarScrivi, New EventArgs())
        TimerScriviValori.Enabled = True

    End Sub

    Public Sub addListi(testo As String)
        For Each strX As String In testo.Replace(vbLf, String.Empty).Split(Chr(13))
            ListBoxLog.Items.Add(strX)

        Next

    End Sub

    Private Sub Me_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Configurazione Is Nothing Then
            Configurazione = ClassConfig.Carica

        End If

        Configurazione.Luminosita = TrackBarLuminosita.Value
        Configurazione.Leggi = TrackBarLeggi.Value
        Configurazione.Scrivi = TrackBarScrivi.Value
        Configurazione.Salva()

        TimerLeggiValori.Enabled = False
        TimerScriviValori.Enabled = False

        sendUsbDevice(0, 0, 0, 0)
        sendUsbDevice(0, 0, 0, 0)

        If MyUsbDevice IsNot Nothing Then
            If MyUsbDevice.IsOpen Then
                MyUsbDevice.Close()
                addListi("Device Close.")

            End If
            'MyUsbDevice = Nothing

            UsbDevice.[Exit]()

        End If

    End Sub

    Private Sub sendUsbDevice(intI As Byte, intR As Byte, intG As Byte, intB As Byte)

        If MyUsbDevice Is Nothing Then
            Exit Sub
        End If

        Dim numBytesTransferred As Integer = 0

        MyUsbDevice.ControlTransfer(New UsbSetupPacket(
            UsbCtrlFlags.RequestType_Class Or UsbCtrlFlags.Recipient_Device Or UsbCtrlFlags.Direction_Out,
            &H9, &H300, intI, 0), Nothing, 0, numBytesTransferred)

        MyUsbDevice.ControlTransfer(New UsbSetupPacket(
            UsbCtrlFlags.RequestType_Class Or UsbCtrlFlags.Recipient_Device Or UsbCtrlFlags.Direction_Out,
            &H9, &H300, intR, 0), Nothing, 0, numBytesTransferred)

        MyUsbDevice.ControlTransfer(New UsbSetupPacket(
            UsbCtrlFlags.RequestType_Class Or UsbCtrlFlags.Recipient_Device Or UsbCtrlFlags.Direction_Out,
            &H9, &H300, intG, 0), Nothing, 0, numBytesTransferred)

        MyUsbDevice.ControlTransfer(New UsbSetupPacket(
            UsbCtrlFlags.RequestType_Class Or UsbCtrlFlags.Recipient_Device Or UsbCtrlFlags.Direction_Out,
            &H9, &H300, intB, 0), Nothing, 0, numBytesTransferred)

    End Sub

    'Declare Function DestroyIcon Lib "user32" (ByVal hIcon As IntPtr) As Integer

    <System.Runtime.InteropServices.DllImportAttribute("user32.dll")>
    Private Shared Function DestroyIcon(ByVal handle As IntPtr) As Boolean
    End Function

    Private Sub TimerScriviValori_Tick(sender As Object, e As EventArgs) Handles TimerScriviValori.Tick

        Dim myBitmap As Bitmap = New Bitmap(64, 64) ' Me.Icon.ToBitmap
        Dim grpX As Graphics = Graphics.FromImage(myBitmap)
        grpX.Clear(Color.Black)

        For intX As Integer = 0 To lstDrivers.Count - 1
            lstDrivers(intX).Mostra()

            Dim clrX As Color = lstDrivers(intX).ColoreConLuminosita(TrackBarLuminosita.Value)
            sendUsbDevice(CByte(intX + 1), clrX.R, clrX.G, clrX.B)

            grpX.FillRectangle(New SolidBrush(lstDrivers(intX).Colore), CSng(intX * (myBitmap.Width / lstDrivers.Count)), 0, CSng(myBitmap.Width / lstDrivers.Count), myBitmap.Height)
            grpX.DrawLine(New Pen(Color.White, 1), CSng(intX * (myBitmap.Width / lstDrivers.Count)), 0, CSng(intX * (myBitmap.Width / lstDrivers.Count)), myBitmap.Height)

            lstDrivers(intX).Reset()

        Next

        grpX.DrawRectangle(New Pen(Color.White, 1), 0, 0, myBitmap.Width - 1, myBitmap.Height - 1)

        'Me.Icon = Icon.FromHandle(myBitmap.GetHicon())

        Dim y As Boolean = DestroyIcon(Me.Icon.Handle)

        Dim x As IntPtr = myBitmap.GetHicon()

        Me.Icon = Icon.FromHandle(x)

        'DestroyIcon(x)

    End Sub

    Private Sub TrackBarLeggi_Scroll(sender As Object, e As EventArgs) Handles TrackBarLeggi.Scroll
        TimerLeggiValori.Interval = TrackBarLeggi.Value
        ToolTipMain.SetToolTip(TrackBarLeggi, TrackBarLeggi.Value.ToString)

        For Each cldX As urcLed In lstDrivers
            cldX.SetInterval(TrackBarLeggi.Value)

        Next

    End Sub

    Private Sub TrackBarScrivi_Scroll(sender As Object, e As EventArgs) Handles TrackBarScrivi.Scroll
        TimerScriviValori.Interval = TrackBarScrivi.Value
        TrackBarLeggi.Maximum = TrackBarScrivi.Value - 1
        ToolTipMain.SetToolTip(TrackBarScrivi, TrackBarScrivi.Value.ToString)

    End Sub

    Private Sub TrackBarLuminosita_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarLuminosita.ValueChanged
        ToolTipMain.SetToolTip(TrackBarLuminosita, TrackBarLuminosita.Value.ToString)
    End Sub

    Private Sub Me_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ToolTipMain.SetToolTip(TrackBarLeggi, TrackBarLeggi.Value.ToString)
        ToolTipMain.SetToolTip(TrackBarScrivi, TrackBarScrivi.Value.ToString)
        ToolTipMain.SetToolTip(TrackBarLuminosita, TrackBarLuminosita.Value.ToString)

    End Sub

End Class
