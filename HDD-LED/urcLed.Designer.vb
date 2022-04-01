<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class urcLed
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelLeggi = New System.Windows.Forms.Label()
        Me.LabelScrivi = New System.Windows.Forms.Label()
        Me.LabelCaption = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.LabelLeggi, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelScrivi, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCaption, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(150, 150)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LabelLeggi
        '
        Me.LabelLeggi.AutoSize = True
        Me.LabelLeggi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelLeggi.Location = New System.Drawing.Point(0, 50)
        Me.LabelLeggi.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelLeggi.Name = "LabelLeggi"
        Me.LabelLeggi.Size = New System.Drawing.Size(150, 50)
        Me.LabelLeggi.TabIndex = 0
        Me.LabelLeggi.Text = "0.0"
        '
        'LabelScrivi
        '
        Me.LabelScrivi.AutoSize = True
        Me.LabelScrivi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelScrivi.Location = New System.Drawing.Point(0, 100)
        Me.LabelScrivi.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelScrivi.Name = "LabelScrivi"
        Me.LabelScrivi.Size = New System.Drawing.Size(150, 50)
        Me.LabelScrivi.TabIndex = 1
        Me.LabelScrivi.Text = "0.0"
        '
        'LabelCaption
        '
        Me.LabelCaption.AutoSize = True
        Me.LabelCaption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCaption.Location = New System.Drawing.Point(0, 0)
        Me.LabelCaption.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelCaption.Name = "LabelCaption"
        Me.LabelCaption.Size = New System.Drawing.Size(150, 50)
        Me.LabelCaption.TabIndex = 2
        Me.LabelCaption.Text = "LED"
        '
        'urcLed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "urcLed"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Public WithEvents LabelLeggi As Label
    Public WithEvents LabelScrivi As Label
    Public WithEvents LabelCaption As Label
End Class
