<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtPfad = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.txtUnterverzeichnis = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.LabelBol = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtUSB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 167)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(457, 48)
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.UseWaitCursor = True
        Me.ProgressBar1.Visible = False
        '
        'txtPfad
        '
        Me.txtPfad.Location = New System.Drawing.Point(12, 65)
        Me.txtPfad.Name = "txtPfad"
        Me.txtPfad.Size = New System.Drawing.Size(457, 20)
        Me.txtPfad.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Ziel Hauptverzeichnis"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(386, 221)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(83, 23)
        Me.btnReset.TabIndex = 3
        Me.btnReset.Text = "Zurücksetzen"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'txtUnterverzeichnis
        '
        Me.txtUnterverzeichnis.Location = New System.Drawing.Point(12, 12)
        Me.txtUnterverzeichnis.Name = "txtUnterverzeichnis"
        Me.txtUnterverzeichnis.Size = New System.Drawing.Size(457, 20)
        Me.txtUnterverzeichnis.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Unterverzeichnis"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(12, 221)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Schließen"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelBol})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 259)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(481, 22)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'LabelBol
        '
        Me.LabelBol.Name = "LabelBol"
        Me.LabelBol.Size = New System.Drawing.Size(119, 17)
        Me.LabelBol.Text = "ToolStripStatusLabel1"
        '
        'txtUSB
        '
        Me.txtUSB.Location = New System.Drawing.Point(12, 118)
        Me.txtUSB.Name = "txtUSB"
        Me.txtUSB.Size = New System.Drawing.Size(265, 20)
        Me.txtUSB.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "USB Name"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 281)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUSB)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUnterverzeichnis)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPfad)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "USB File Copy"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents txtPfad As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnReset As Button
    Friend WithEvents txtUnterverzeichnis As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents LabelBol As ToolStripStatusLabel
    Friend WithEvents txtUSB As TextBox
    Friend WithEvents Label3 As Label
End Class
