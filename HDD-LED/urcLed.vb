Imports System.ComponentModel

Public Class urcLed
    Private PerformanceCounterLetturaSec As PerformanceCounter = Nothing
    Private PerformanceCounterScritturaSec As PerformanceCounter = Nothing
    Private DeviceAudio As NAudio.CoreAudioApi.MMDevice = Nothing

    Private WithEvents TimerLeggi As Timer

    Public Property IName As String = String.Empty

    Public Property Tipo As enmTipo = enmTipo.none

    Public Class EventArgs_log
        Inherits EventArgs
        Public Property Messaggio As String = String.Empty
    End Class
    Public Event Log(sender As Object, e As EventArgs_log)

    Private Class classDati
        Public Property Lettura As Single = 0
        Public Property Scrittura As Single = 0

        Public Function Leggi() As Boolean
            Return Lettura > 0
        End Function
        Public Function Scrivi() As Boolean
            Return Scrittura > 0
        End Function

        Public Function Clone() As classDati
            Return DirectCast(Me.MemberwiseClone(), classDati)
        End Function

        Public Sub Clear()
            Lettura = 0
            Scrittura = 0
        End Sub

    End Class

    Private DatiBuffer As New classDati
    Private DatiOut As classDati

    Public Enum enmTipo
        none
        Drive
        Lan
        CPU
        Audio
    End Enum

    Public Sub SetInterval(interval As Integer)
        TimerLeggi.Interval = interval
        TimerLeggi.Enabled = True

    End Sub

    Private Sub Nuovo()
        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        TimerLeggi = New Timer With {
            .Interval = 10
        }
    End Sub

    Public Sub New()

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

    Public Sub New(MMDeviceX As NAudio.CoreAudioApi.MMDevice)
        Nuovo()
        Tipo = enmTipo.Audio
        DeviceAudio = MMDeviceX
        IName = MMDeviceX.ToString
    End Sub

    Public Sub New(strIName As String, entTipo As enmTipo)
        Nuovo()
        IName = strIName
        Tipo = entTipo

        If Tipo = enmTipo.Audio Then
            Exit Sub
        End If

        PerformanceCounterLetturaSec = New PerformanceCounter()
        CType(PerformanceCounterLetturaSec, ISupportInitialize).BeginInit()

        Select Case Tipo
            Case enmTipo.Drive
                PerformanceCounterLetturaSec.CategoryName = "Disco logico"
                PerformanceCounterLetturaSec.CounterName = "Letture disco/sec"

            Case enmTipo.Lan
                PerformanceCounterLetturaSec.CategoryName = "Interfaccia di rete"
                PerformanceCounterLetturaSec.CounterName = "Byte ricevuti/sec"

            Case enmTipo.CPU
                PerformanceCounterLetturaSec.CategoryName = "Processore"
                PerformanceCounterLetturaSec.CounterName = "% Tempo processore"

        End Select

        PerformanceCounterLetturaSec.MachineName = "."
        PerformanceCounterLetturaSec.InstanceName = IName
        CType(PerformanceCounterLetturaSec, ISupportInitialize).EndInit()

        PerformanceCounterScritturaSec = New PerformanceCounter()
        CType(PerformanceCounterScritturaSec, ISupportInitialize).BeginInit()

        Select Case Tipo
            Case enmTipo.Drive
                PerformanceCounterScritturaSec.CategoryName = "Disco logico"
                PerformanceCounterScritturaSec.CounterName = "Scritture disco/sec"

            Case enmTipo.Lan
                PerformanceCounterScritturaSec.CategoryName = "Interfaccia di rete"
                PerformanceCounterScritturaSec.CounterName = "Byte inviati/sec"

            Case enmTipo.CPU
                PerformanceCounterScritturaSec.CategoryName = "Processore"
                PerformanceCounterScritturaSec.CounterName = "% Tempo processore" ' "% Tempo inattività"

        End Select

        PerformanceCounterScritturaSec.MachineName = "."
        PerformanceCounterScritturaSec.InstanceName = IName
        CType(PerformanceCounterScritturaSec, ISupportInitialize).EndInit()

    End Sub

    Public Sub Inizializza()

        If Tipo = enmTipo.Drive Then
            For Each d As IO.DriveInfo In IO.DriveInfo.GetDrives()
                If d.Name.StartsWith(IName) Then
                    LabelCaption.Text = $"{IName} ({d.VolumeLabel})"
                    Exit Sub
                End If
            Next

        Else
            LabelCaption.Text = IName

        End If

    End Sub

    Public Sub Aggiorna()
        Try
            If Tipo = enmTipo.Audio Then
                DatiBuffer.Lettura = CSng(Math.Round(Math.Max(DatiBuffer.Lettura, DeviceAudio.AudioMeterInformation.MasterPeakValue * 100), 1))
                DatiBuffer.Scrittura = DatiBuffer.Lettura

            Else
                DatiBuffer.Lettura = CSng(Math.Round(Math.Max(DatiBuffer.Lettura, PerformanceCounterLetturaSec.NextValue), 1))
                DatiBuffer.Scrittura = CSng(Math.Round(Math.Max(DatiBuffer.Scrittura, PerformanceCounterScritturaSec.NextValue), 1))

            End If

        Catch ex As Exception
            Throw

        End Try

    End Sub

    Public Function ColoreConLuminosita(Luminosita As Integer) As Color

        Select Case Tipo
            Case enmTipo.Drive, enmTipo.Lan
                Return Color.FromArgb(255, If(DatiOut.Scrivi AndAlso Not DatiOut.Leggi, Luminosita, 0), If(Not DatiOut.Scrivi AndAlso DatiOut.Leggi, Luminosita, 0), If(DatiOut.Scrivi AndAlso DatiOut.Leggi, Luminosita, 0))

            Case enmTipo.CPU
                Dim l As Integer = Math.Min(255, Math.Max(0, CInt((Luminosita * DatiOut.Lettura) / 100)))

                Return Color.FromArgb(255, l, l, l)

            Case enmTipo.Audio
                Dim l As Integer = Math.Min(255, Math.Max(0, CInt((Luminosita * ((60 * DatiOut.Lettura) / 100) / 100))))

                Return Color.FromArgb(255, l, l, l)

        End Select

    End Function

    Public Function Colore() As Color
        Return ColoreConLuminosita(255)

    End Function

    Delegate Sub Delegate_Mostra()
    Public Sub Mostra()
        If InvokeRequired Then
            BeginInvoke(New Delegate_Mostra(AddressOf Mostra))
            Exit Sub
        End If

        DatiOut = DatiBuffer.Clone

        SuspendLayout()

        BackColor = Colore()
        LabelCaption.ForeColor = ColorInverter(BackColor)
        LabelCaption.BackColor = BackColor

        LabelLeggi.ForeColor = ColorInverter(BackColor)
        LabelLeggi.BackColor = BackColor

        LabelScrivi.ForeColor = ColorInverter(BackColor)
        LabelScrivi.BackColor = BackColor

        Dim strLeggi As String = String.Empty
        Dim strScrivi As String = String.Empty

        Select Case Tipo
            Case enmTipo.Drive
                strLeggi = $"L : {DatiBuffer.Lettura:#0.0}"
                strScrivi = $"S : {DatiBuffer.Scrittura:#0.0}"

            Case enmTipo.Lan
                strLeggi = $"D : {DatiBuffer.Lettura:#0.0}"
                strScrivi = $"U : {DatiBuffer.Scrittura:#0.0}"

            Case enmTipo.CPU
                strLeggi = $"P : {DatiBuffer.Lettura:#0.0} %"
                LabelCaption.Text = "CPU"

            Case enmTipo.Audio
                strLeggi = $"V : {DatiBuffer.Lettura:#0.0}"
                LabelCaption.Text = DeviceAudio.FriendlyName

        End Select

        LabelLeggi.Text = strLeggi
        LabelScrivi.Text = strScrivi

        ResumeLayout()

    End Sub

    Public Sub Reset()
        DatiBuffer.Clear()

    End Sub

    Private Function ColorInverter(ColorX As Color) As Color
        Dim clrX As Color = Color.FromArgb(ColorX.A, InCo(ColorX.R), InCo(ColorX.G), InCo(ColorX.B))

        If Math.Max(clrX.ToArgb(), ColorX.ToArgb()) - Math.Min(clrX.ToArgb(), ColorX.ToArgb()) < 197380 Then
            clrX = Color.Black

        End If

        Return clrX
    End Function

    Private Function InCo(ByVal intX As Integer) As Integer
        Return 127 + (128 - intX)
    End Function

    Private Sub TimerLeggi_Tick(sender As Object, e As EventArgs) Handles TimerLeggi.Tick
        Try
            Aggiorna()
        Catch ex As Exception
            TimerLeggi.Enabled = False
            Visible = False
            RaiseEvent Log(Me, New EventArgs_log() With {.Messaggio = ex.Message})
        End Try
    End Sub

End Class
