<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductSearch
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label2 = New Label()
        Panel1 = New Panel()
        PictureBox2 = New PictureBox()
        Label1 = New Label()
        Label100 = New Label()
        Label14 = New Label()
        NumericUpDown1 = New NumericUpDown()
        DataGridView100 = New DataGridView()
        Label97 = New Label()
        TextBox1 = New TextBox()
        Panel1.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView100, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.BackColor = Color.FromArgb(CByte(252), CByte(179), CByte(188))
        Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.Font = New Font("Bernard MT Condensed", 22.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = SystemColors.ControlLightLight
        Label2.Location = New Point(-1, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(906, 93)
        Label2.TabIndex = 1
        Label2.Text = "PARTY SUPPLIES SHOP && EVENT EQUIPMENT RENTAL"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(255), CByte(248), CByte(240))
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Label100)
        Panel1.Controls.Add(Label14)
        Panel1.Controls.Add(NumericUpDown1)
        Panel1.Controls.Add(DataGridView100)
        Panel1.Controls.Add(Label97)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(Label2)
        Panel1.Location = New Point(17, 15)
        Panel1.Margin = New Padding(2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(897, 675)
        Panel1.TabIndex = 2
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackgroundImage = My.Resources.Resources.Untitled_design__4_
        PictureBox2.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox2.Location = New Point(298, 145)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(23, 30)
        PictureBox2.TabIndex = 29
        PictureBox2.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        Label1.BorderStyle = BorderStyle.FixedSingle
        Label1.Font = New Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.ControlLightLight
        Label1.Location = New Point(523, 606)
        Label1.Name = "Label1"
        Label1.Size = New Size(96, 38)
        Label1.TabIndex = 28
        Label1.Text = "Cancel"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label100
        ' 
        Label100.BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        Label100.BorderStyle = BorderStyle.FixedSingle
        Label100.Font = New Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label100.ForeColor = SystemColors.ControlLightLight
        Label100.Location = New Point(414, 606)
        Label100.Name = "Label100"
        Label100.Size = New Size(96, 38)
        Label100.TabIndex = 27
        Label100.Text = "Select"
        Label100.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label14
        ' 
        Label14.BackColor = Color.FromArgb(CByte(255), CByte(248), CByte(240))
        Label14.Cursor = Cursors.Hand
        Label14.Font = New Font("Berlin Sans FB", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.ForeColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        Label14.Location = New Point(209, 613)
        Label14.Name = "Label14"
        Label14.Size = New Size(75, 28)
        Label14.TabIndex = 26
        Label14.Text = "Quantity:"
        Label14.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Location = New Point(289, 612)
        NumericUpDown1.Margin = New Padding(2)
        NumericUpDown1.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(108, 27)
        NumericUpDown1.TabIndex = 25
        NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' DataGridView100
        ' 
        DataGridView100.AllowUserToAddRows = False
        DataGridView100.AllowUserToDeleteRows = False
        DataGridView100.AllowUserToOrderColumns = True
        DataGridView100.AllowUserToResizeColumns = False
        DataGridView100.AllowUserToResizeRows = False
        DataGridView100.BackgroundColor = Color.MistyRose
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DataGridView100.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView100.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ActiveBorder
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        DataGridView100.DefaultCellStyle = DataGridViewCellStyle2
        DataGridView100.EnableHeadersVisualStyles = False
        DataGridView100.GridColor = SystemColors.MenuText
        DataGridView100.Location = New Point(120, 189)
        DataGridView100.Margin = New Padding(2)
        DataGridView100.MultiSelect = False
        DataGridView100.Name = "DataGridView100"
        DataGridView100.ReadOnly = True
        DataGridView100.RowHeadersVisible = False
        DataGridView100.RowHeadersWidth = 62
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(255), CByte(243), CByte(225))
        DataGridViewCellStyle3.ForeColor = Color.Black
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        DataGridViewCellStyle3.SelectionForeColor = Color.Black
        DataGridView100.RowsDefaultCellStyle = DataGridViewCellStyle3
        DataGridView100.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView100.Size = New Size(646, 395)
        DataGridView100.TabIndex = 24
        ' 
        ' Label97
        ' 
        Label97.BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        Label97.BorderStyle = BorderStyle.FixedSingle
        Label97.Font = New Font("Bernard MT Condensed", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label97.ForeColor = SystemColors.ControlLightLight
        Label97.Location = New Point(527, 148)
        Label97.Name = "Label97"
        Label97.Size = New Size(92, 27)
        Label97.TabIndex = 23
        Label97.Text = "Search"
        Label97.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        TextBox1.Location = New Point(326, 148)
        TextBox1.Margin = New Padding(2)
        TextBox1.Name = "TextBox1"
        TextBox1.PlaceholderText = "Search a product"
        TextBox1.Size = New Size(196, 27)
        TextBox1.TabIndex = 2
        ' 
        ' frmProductSearch
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(177), CByte(64), CByte(82))
        ClientSize = New Size(930, 710)
        Controls.Add(Panel1)
        Margin = New Padding(2)
        Name = "frmProductSearch"
        Text = "frmProductSearch"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView100, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label97 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents DataGridView100 As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label100 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
