<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TimerLeggiValori = New System.Windows.Forms.Timer(Me.components)
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.TimerScriviValori = New System.Windows.Forms.Timer(Me.components)
        Me.TrackBarLuminosita = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanelLeds = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanelCommand = New System.Windows.Forms.TableLayoutPanel()
        Me.TrackBarLeggi = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TrackBarScrivi = New System.Windows.Forms.TrackBar()
        Me.ToolTipMain = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.TrackBarLuminosita, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.TableLayoutPanelCommand.SuspendLayout()
        CType(Me.TrackBarLeggi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarScrivi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerLeggiValori
        '
        Me.TimerLeggiValori.Interval = 12
        '
        'ListBoxLog
        '
        Me.ListBoxLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.Location = New System.Drawing.Point(3, 307)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(987, 140)
        Me.ListBoxLog.TabIndex = 2
        '
        'TimerScriviValori
        '
        Me.TimerScriviValori.Interval = 125
        '
        'TrackBarLuminosita
        '
        Me.TrackBarLuminosita.Dock = System.Windows.Forms.DockStyle.Top
        Me.TrackBarLuminosita.Location = New System.Drawing.Point(154, 3)
        Me.TrackBarLuminosita.Maximum = 255
        Me.TrackBarLuminosita.Minimum = 1
        Me.TrackBarLuminosita.Name = "TrackBarLuminosita"
        Me.TrackBarLuminosita.Size = New System.Drawing.Size(830, 45)
        Me.TrackBarLuminosita.TabIndex = 3
        Me.TrackBarLuminosita.Value = 126
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 51)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Luminosita"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanelLeds
        '
        Me.FlowLayoutPanelLeds.AutoSize = True
        Me.FlowLayoutPanelLeds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanelLeds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowLayoutPanelLeds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanelLeds.Location = New System.Drawing.Point(3, 162)
        Me.FlowLayoutPanelLeds.Name = "FlowLayoutPanelLeds"
        Me.FlowLayoutPanelLeds.Size = New System.Drawing.Size(987, 139)
        Me.FlowLayoutPanelLeds.TabIndex = 5
        '
        'TableLayoutPanelMain
        '
        Me.TableLayoutPanelMain.AutoSize = True
        Me.TableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanelMain.ColumnCount = 1
        Me.TableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanelMain.Controls.Add(Me.TableLayoutPanelCommand, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.ListBoxLog, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.FlowLayoutPanelLeds, 0, 1)
        Me.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelMain.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.TableLayoutPanelMain.RowCount = 3
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelMain.Size = New System.Drawing.Size(993, 450)
        Me.TableLayoutPanelMain.TabIndex = 6
        '
        'TableLayoutPanelCommand
        '
        Me.TableLayoutPanelCommand.AutoSize = True
        Me.TableLayoutPanelCommand.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanelCommand.ColumnCount = 2
        Me.TableLayoutPanelCommand.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanelCommand.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelCommand.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanelCommand.Controls.Add(Me.TrackBarLuminosita, 1, 0)
        Me.TableLayoutPanelCommand.Controls.Add(Me.TrackBarLeggi, 1, 1)
        Me.TableLayoutPanelCommand.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanelCommand.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanelCommand.Controls.Add(Me.TrackBarScrivi, 1, 2)
        Me.TableLayoutPanelCommand.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanelCommand.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanelCommand.Name = "TableLayoutPanelCommand"
        Me.TableLayoutPanelCommand.RowCount = 3
        Me.TableLayoutPanelCommand.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelCommand.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelCommand.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelCommand.Size = New System.Drawing.Size(987, 153)
        Me.TableLayoutPanelCommand.TabIndex = 7
        '
        'TrackBarLeggi
        '
        Me.TrackBarLeggi.Dock = System.Windows.Forms.DockStyle.Top
        Me.TrackBarLeggi.Location = New System.Drawing.Point(154, 54)
        Me.TrackBarLeggi.Maximum = 1000
        Me.TrackBarLeggi.Minimum = 10
        Me.TrackBarLeggi.Name = "TrackBarLeggi"
        Me.TrackBarLeggi.Size = New System.Drawing.Size(830, 45)
        Me.TrackBarLeggi.TabIndex = 10
        Me.TrackBarLeggi.Value = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!)
        Me.Label2.Location = New System.Drawing.Point(3, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 51)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Leggi"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!)
        Me.Label3.Location = New System.Drawing.Point(3, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(145, 51)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Scrivi"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TrackBarScrivi
        '
        Me.TrackBarScrivi.Dock = System.Windows.Forms.DockStyle.Top
        Me.TrackBarScrivi.Location = New System.Drawing.Point(154, 105)
        Me.TrackBarScrivi.Maximum = 1000
        Me.TrackBarScrivi.Minimum = 20
        Me.TrackBarScrivi.Name = "TrackBarScrivi"
        Me.TrackBarScrivi.Size = New System.Drawing.Size(830, 45)
        Me.TrackBarScrivi.TabIndex = 8
        Me.TrackBarScrivi.Value = 125
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 450)
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HDD LED"
        CType(Me.TrackBarLuminosita, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.TableLayoutPanelCommand.ResumeLayout(False)
        Me.TableLayoutPanelCommand.PerformLayout()
        CType(Me.TrackBarLeggi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarScrivi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerLeggiValori As Timer
    Friend WithEvents ListBoxLog As ListBox
    Friend WithEvents TimerScriviValori As Timer
    Friend WithEvents TrackBarLuminosita As TrackBar
    Friend WithEvents Label1 As Label
    Friend WithEvents FlowLayoutPanelLeds As FlowLayoutPanel
    Friend WithEvents TableLayoutPanelMain As TableLayoutPanel
    Friend WithEvents TableLayoutPanelCommand As TableLayoutPanel
    Friend WithEvents TrackBarLeggi As TrackBar
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TrackBarScrivi As TrackBar
    Friend WithEvents ToolTipMain As ToolTip
End Class
