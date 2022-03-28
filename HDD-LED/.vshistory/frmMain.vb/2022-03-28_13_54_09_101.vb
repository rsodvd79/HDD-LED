Imports LibUsbDotNet
Imports LibUsbDotNet.Main
Imports System.ComponentModel

Public Class frmMain
    Dim MyUsbDevice As UsbDevice = Nothing

    Dim lstDrivers As List(Of clsDriver)

    Private Sub TimerLeggiValori_Tick(sender As Object, e As EventArgs) Handles TimerLeggiValori.Tick

        For intX As Integer = 0 To lstDrivers.Count - 1
            Try
                lstDrivers(intX).update()

            Catch ex As Exception
                addListi("Removed " & lstDrivers(intX).IName & " " & ex.Message)
                FlowLayoutPanelLeds.Controls.Remove(lstDrivers(intX).Control)
                lstDrivers.RemoveAt(intX)
                Exit For

            End Try

        Next

    End Sub

    Private Sub Me_Load(sender As Object, e As EventArgs) Handles Me.Load

        TrackBarLuminosita.Width = 0

        FlowLayoutPanelLeds.Width = 0
        FlowLayoutPanelLeds.Height = 0

        ListBoxLog.Width = 0
        ListBoxLog.Height = 0
        ListBoxLog.Items.Clear()

        Dim drcW As Integer = 80
        Dim drcH As Integer = drcW

        lstDrivers = New List(Of clsDriver) From {
            New clsDriver("_Total", clsDriver.enmTipo.CPU) With {.Control = New urcLed() With {.BorderStyle = BorderStyle.FixedSingle, .Width = drcW, .Height = drcH}}
        }

        For Each drive As IO.DriveInfo In My.Computer.FileSystem.Drives
            addListi("Found Driver " & drive.Name & " " & drive.DriveType.ToString & " " & If(drive.DriveType = IO.DriveType.Fixed OrElse drive.DriveType = IO.DriveType.Removable, String.Empty, "IGNORED"))

            If drive.DriveType = IO.DriveType.Fixed OrElse drive.DriveType = IO.DriveType.Removable Then
                lstDrivers.Add(New clsDriver(drive.Name.Substring(0, 2), clsDriver.enmTipo.Drive) With {.Control = New urcLed() With {.BorderStyle = BorderStyle.FixedSingle, .Width = drcW, .Height = drcH}})

            End If

        Next

        For Each adapter As Net.NetworkInformation.NetworkInterface In Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
            Dim properties As Net.NetworkInformation.IPInterfaceProperties = adapter.GetIPProperties()
            addListi("Found Network " & adapter.Description & " " & adapter.OperationalStatus.ToString)
            If adapter.OperationalStatus = Net.NetworkInformation.OperationalStatus.Up Then
                lstDrivers.Add(New clsDriver(adapter.Description.Replace("(", "[").Replace(")", "]"), clsDriver.enmTipo.Lan) With {.Control = New urcLed() With {.BorderStyle = BorderStyle.FixedSingle, .Width = drcW, .Height = drcH}})

            End If

        Next

        For Each cldX As clsDriver In lstDrivers
            FlowLayoutPanelLeds.Controls.Add(cldX.Control)

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

        TimerScriviValori.Interval = TrackBarScrivi.Value
        TimerScriviValori.Enabled = True

        TimerLeggiValori.Interval = TrackBarLeggi.Value
        TimerLeggiValori.Enabled = True

    End Sub

    Public Sub addListi(testo As String)
        For Each strX As String In testo.Replace(vbLf, String.Empty).Split(Chr(13))
            ListBoxLog.Items.Add(strX)

        Next

    End Sub

    Private Sub Me_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
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
        Threading.Tasks.Task.Factory.StartNew(
            Sub()
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
        )


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
            lstDrivers(intX).show(TrackBarLuminosita.Value)
            Dim clrX As Color = lstDrivers(intX).Control.BackColor
            sendUsbDevice(CByte(intX + 1), clrX.R, clrX.G, clrX.B)

            grpX.FillRectangle(New SolidBrush(clrX), CSng(intX * (myBitmap.Width / lstDrivers.Count)), 0, CSng(myBitmap.Width / lstDrivers.Count), myBitmap.Height)
            grpX.DrawLine(New Pen(Color.White, 1), CSng(intX * (myBitmap.Width / lstDrivers.Count)), 0, CSng(intX * (myBitmap.Width / lstDrivers.Count)), myBitmap.Height)

            lstDrivers(intX).reset()

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

    End Sub

    Private Sub TrackBarScrivi_Scroll(sender As Object, e As EventArgs) Handles TrackBarScrivi.Scroll
        TimerScriviValori.Interval = TrackBarScrivi.Value
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
