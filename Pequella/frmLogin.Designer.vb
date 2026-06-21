<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Panel1 = New Panel()
        Button13 = New Button()
        txtPassword = New TextBox()
        Label4 = New Label()
        txtUsername = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(255), CByte(248), CByte(240))
        Panel1.Controls.Add(Button13)
        Panel1.Controls.Add(txtPassword)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(txtUsername)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Label2)
        Panel1.Location = New Point(12, 12)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(355, 354)
        Panel1.TabIndex = 0
        ' 
        ' Button13
        ' 
        Button13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button13.ForeColor = Color.FromArgb(CByte(155), CByte(49), CByte(97))
        Button13.Location = New Point(143, 224)
        Button13.Name = "Button13"
        Button13.Size = New Size(173, 31)
        Button13.TabIndex = 17
        Button13.Text = "LOGIN"
        Button13.UseVisualStyleBackColor = True
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(144, 158)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(173, 27)
        txtPassword.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        Label4.Location = New Point(33, 161)
        Label4.Name = "Label4"
        Label4.Size = New Size(92, 20)
        Label4.TabIndex = 6
        Label4.Text = "PASSWORD:"
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(143, 107)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(173, 27)
        txtUsername.TabIndex = 3
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(32, 110)
        Label1.Name = "Label1"
        Label1.Size = New Size(93, 20)
        Label1.TabIndex = 2
        Label1.Text = "USERNAME:"
        ' 
        ' Label2
        ' 
        Label2.BackColor = Color.FromArgb(CByte(252), CByte(179), CByte(188))
        Label2.Font = New Font("Bernard MT Condensed", 22.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = SystemColors.ControlLightLight
        Label2.Location = New Point(0, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(355, 56)
        Label2.TabIndex = 1
        Label2.Text = "LOGIN"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        ClientSize = New Size(379, 378)
        Controls.Add(Panel1)
        Name = "frmLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button13 As Button
End Class
