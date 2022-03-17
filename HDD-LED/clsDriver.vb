Imports System.ComponentModel

Public Class clsDriver
    Dim PerformanceCounterLetturaSec As PerformanceCounter
    Dim PerformanceCounterScritturaSec As PerformanceCounter

    Public Leggi As Boolean = False
    Public Scrivi As Boolean = False

    Public Lettura As Single = 0
    Public Scrittura As Single = 0

    Public IName As String = String.Empty

    Public Control As urcLed = Nothing

    Public Tipo As enmTipo = enmTipo.none

    Public Enum enmTipo
        none
        Drive
        Lan
        CPU
    End Enum

    Public Sub New(strIName As String, entTipo As enmTipo)
        IName = strIName
        Tipo = entTipo

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

    Public Sub update()
        Try
            Lettura = CSng(Math.Round(Math.Max(Lettura, PerformanceCounterLetturaSec.NextValue), 1))
            Scrittura = CSng(Math.Round(Math.Max(Scrittura, PerformanceCounterScritturaSec.NextValue), 1))

        Catch ex As Exception
            Throw

        End Try

        Leggi = Leggi OrElse Lettura > 0
        Scrivi = Scrivi OrElse Scrittura > 0

    End Sub

    Public Sub show(Luminosita As Integer)

        Control.SuspendLayout()
        Control.LabelCaption.SuspendLayout()
        Control.LabelLeggi.SuspendLayout()
        Control.LabelScrivi.SuspendLayout()

        Control.BackColor = Color.FromArgb(255, If(Scrivi, Luminosita, 0), If(Leggi, Luminosita, 0), If(Leggi AndAlso Scrivi, Luminosita, 0))
        Control.LabelCaption.Text = IName

        Dim strLeggi As String = String.Empty
        Dim strScrivi As String = String.Empty

        Select Case Tipo
            Case enmTipo.Drive
                strLeggi = "L : " & Lettura.ToString
                strScrivi = "S : " & Scrittura.ToString

            Case enmTipo.Lan
                strLeggi = "D : " & Lettura.ToString
                strScrivi = "U : " & Scrittura.ToString

            Case enmTipo.CPU
                strLeggi = Lettura.ToString & " %"

                Dim l As Integer = CInt((Luminosita * Lettura) / 100)

                Control.BackColor = Color.FromArgb(255, l, l, l)
                Control.LabelCaption.Text = "CPU"

        End Select

        Control.LabelCaption.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelCaption.BackColor = Control.BackColor

        Control.LabelLeggi.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelLeggi.BackColor = Control.BackColor

        Control.LabelScrivi.ForeColor = ColorInverter(Control.BackColor)
        Control.LabelScrivi.BackColor = Control.BackColor

        Control.LabelLeggi.Text = strLeggi
        Control.LabelScrivi.Text = strScrivi

        Control.LabelScrivi.ResumeLayout()
        Control.LabelLeggi.ResumeLayout()
        Control.LabelCaption.ResumeLayout()
        Control.ResumeLayout()

    End Sub

    Public Sub reset()
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

