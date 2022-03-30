Imports System.ComponentModel

Public Class clsDriver
    Private PerformanceCounterLetturaSec As PerformanceCounter
    Private PerformanceCounterScritturaSec As PerformanceCounter

    Private Leggi As Boolean = False
    Private Scrivi As Boolean = False

    Private Lettura As Single = 0
    Private Scrittura As Single = 0
    Private DeviceAudio As NAudio.CoreAudioApi.MMDevice = Nothing

    Public Property IName As String = String.Empty

    Public Property Control As urcLed = Nothing

    Public Property Tipo As enmTipo = enmTipo.none

    Public Enum enmTipo
        none
        Drive
        Lan
        CPU
        Audio
    End Enum

    Public Sub New(MMDeviceX As NAudio.CoreAudioApi.MMDevice)
        Tipo = enmTipo.Audio
        DeviceAudio = MMDeviceX
        IName = MMDeviceX.ToString
    End Sub

    Public Sub New(strIName As String, entTipo As enmTipo)
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

    Public Sub Update()
        Try
            If Tipo = enmTipo.Audio Then
                Lettura = CSng(Math.Round(DeviceAudio.AudioMeterInformation.MasterPeakValue * 100, 1))
                Scrittura = Lettura

            Else
                Lettura = CSng(Math.Round(Math.Max(Lettura, PerformanceCounterLetturaSec.NextValue), 1))
                Scrittura = CSng(Math.Round(Math.Max(Scrittura, PerformanceCounterScritturaSec.NextValue), 1))

            End If

        Catch ex As Exception
            Throw

        End Try

        Leggi = Leggi OrElse Lettura > 0
        Scrivi = Scrivi OrElse Scrittura > 0

        Show()

    End Sub

    Public Function ColoreConLuminosita(Luminosita As Integer) As Color
        Select Case Tipo
            Case enmTipo.Drive, enmTipo.Lan
                Return Color.FromArgb(255, If(Scrivi AndAlso Not Leggi, Luminosita, 0), If(Not Scrivi AndAlso Leggi, Luminosita, 0), If(Scrivi AndAlso Leggi, Luminosita, 0))

            Case enmTipo.CPU, enmTipo.Audio
                Dim l As Integer = Math.Min(255, Math.Max(0, CInt((Luminosita * Lettura) / 100)))

                Return Color.FromArgb(255, l, l, l)

        End Select

    End Function

    Public Function Colore() As Color
        Return ColoreConLuminosita(255)

    End Function

    Public Sub Show()

        Control.SuspendLayout()

        Control.BackColor = Colore()
        Control.LabelCaption.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelCaption.BackColor = Control.BackColor

        Control.LabelLeggi.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelLeggi.BackColor = Control.BackColor

        Control.LabelScrivi.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelScrivi.BackColor = Control.BackColor

        Control.LabelCaption.Text = IName

        Dim strLeggi As String = String.Empty
        Dim strScrivi As String = String.Empty

        Select Case Tipo
            Case enmTipo.Drive
                strLeggi = $"L : {Lettura:#0.0}"
                strScrivi = $"S : {Scrittura:#0.0}"

            Case enmTipo.Lan
                strLeggi = $"D : {Lettura:#0.0}"
                strScrivi = $"U : {Scrittura:#0.0}"

            Case enmTipo.CPU
                strLeggi = $"{Lettura:#0.0} %"
                Control.LabelCaption.Text = "CPU"

            Case enmTipo.Audio
                strLeggi = $"{Lettura:#0.0}"
                Control.LabelCaption.Text = DeviceAudio.FriendlyName

        End Select

        Control.LabelLeggi.Text = strLeggi
        Control.LabelScrivi.Text = strScrivi

        Control.ResumeLayout()

    End Sub

    Public Sub Reset()
        Leggi = False
        Scrivi = False
        Lettura = 0
        Scrittura = 0

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

End Class

