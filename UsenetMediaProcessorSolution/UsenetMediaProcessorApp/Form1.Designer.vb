<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPathname = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnUnkownToText = New System.Windows.Forms.Button()
        Me.btnListUnknown = New System.Windows.Forms.Button()
        Me.btnExtract = New System.Windows.Forms.Button()
        Me.btnMoveUnknown = New System.Windows.Forms.Button()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.btnPurge720p = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnNzbget = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtReplace = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.btnPasteFilename = New System.Windows.Forms.Button()
        Me.btnFix = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 25
        Me.ListBox1.Location = New System.Drawing.Point(0, 385)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(1691, 679)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 36)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 25)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Pathname:"
        '
        'txtPathname
        '
        Me.txtPathname.Location = New System.Drawing.Point(156, 31)
        Me.txtPathname.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtPathname.Name = "txtPathname"
        Me.txtPathname.Size = New System.Drawing.Size(487, 31)
        Me.txtPathname.TabIndex = 2
        Me.txtPathname.Text = "e:\#done"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.btnUnkownToText)
        Me.Panel1.Controls.Add(Me.btnListUnknown)
        Me.Panel1.Controls.Add(Me.btnExtract)
        Me.Panel1.Controls.Add(Me.btnMoveUnknown)
        Me.Panel1.Controls.Add(Me.btnCheck)
        Me.Panel1.Controls.Add(Me.btnPurge720p)
        Me.Panel1.Controls.Add(Me.btnStart)
        Me.Panel1.Controls.Add(Me.btnNzbget)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.txtReplace)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnRename)
        Me.Panel1.Controls.Add(Me.txtFind)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnMove)
        Me.Panel1.Controls.Add(Me.btnPasteFilename)
        Me.Panel1.Controls.Add(Me.btnFix)
        Me.Panel1.Controls.Add(Me.txtPathname)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1691, 368)
        Me.Panel1.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1060, 195)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(259, 44)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "Movies to CSV"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnUnkownToText
        '
        Me.btnUnkownToText.Location = New System.Drawing.Point(1060, 142)
        Me.btnUnkownToText.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnUnkownToText.Name = "btnUnkownToText"
        Me.btnUnkownToText.Size = New System.Drawing.Size(259, 44)
        Me.btnUnkownToText.TabIndex = 24
        Me.btnUnkownToText.Text = "Unknown to CSV"
        Me.btnUnkownToText.UseVisualStyleBackColor = True
        '
        'btnListUnknown
        '
        Me.btnListUnknown.Location = New System.Drawing.Point(1060, 86)
        Me.btnListUnknown.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnListUnknown.Name = "btnListUnknown"
        Me.btnListUnknown.Size = New System.Drawing.Size(259, 44)
        Me.btnListUnknown.TabIndex = 23
        Me.btnListUnknown.Text = "List Unknown"
        Me.btnListUnknown.UseVisualStyleBackColor = True
        '
        'btnExtract
        '
        Me.btnExtract.Location = New System.Drawing.Point(731, 195)
        Me.btnExtract.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnExtract.Name = "btnExtract"
        Me.btnExtract.Size = New System.Drawing.Size(187, 44)
        Me.btnExtract.TabIndex = 22
        Me.btnExtract.Text = "Extract"
        Me.btnExtract.UseVisualStyleBackColor = True
        '
        'btnMoveUnknown
        '
        Me.btnMoveUnknown.Location = New System.Drawing.Point(1060, 28)
        Me.btnMoveUnknown.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnMoveUnknown.Name = "btnMoveUnknown"
        Me.btnMoveUnknown.Size = New System.Drawing.Size(259, 44)
        Me.btnMoveUnknown.TabIndex = 21
        Me.btnMoveUnknown.Text = "Move Unknown"
        Me.btnMoveUnknown.UseVisualStyleBackColor = True
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(20, 264)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(240, 44)
        Me.btnCheck.TabIndex = 18
        Me.btnCheck.Text = "Check For Duplicates"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'btnPurge720p
        '
        Me.btnPurge720p.Location = New System.Drawing.Point(290, 208)
        Me.btnPurge720p.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnPurge720p.Name = "btnPurge720p"
        Me.btnPurge720p.Size = New System.Drawing.Size(124, 44)
        Me.btnPurge720p.TabIndex = 16
        Me.btnPurge720p.Text = "Purge 720p"
        Me.btnPurge720p.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(731, 251)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(187, 44)
        Me.btnStart.TabIndex = 15
        Me.btnStart.Text = "Add to Library"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnNzbget
        '
        Me.btnNzbget.Location = New System.Drawing.Point(20, 208)
        Me.btnNzbget.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnNzbget.Name = "btnNzbget"
        Me.btnNzbget.Size = New System.Drawing.Size(124, 44)
        Me.btnNzbget.TabIndex = 14
        Me.btnNzbget.Text = "nzbget"
        Me.btnNzbget.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(656, 128)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(38, 44)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "X"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtReplace
        '
        Me.txtReplace.Location = New System.Drawing.Point(156, 131)
        Me.txtReplace.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtReplace.Name = "txtReplace"
        Me.txtReplace.Size = New System.Drawing.Size(487, 31)
        Me.txtReplace.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 136)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 25)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Replace:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(656, 78)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(38, 44)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "V"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnRename
        '
        Me.btnRename.Location = New System.Drawing.Point(731, 125)
        Me.btnRename.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(124, 44)
        Me.btnRename.TabIndex = 9
        Me.btnRename.Text = "Rename"
        Me.btnRename.UseVisualStyleBackColor = True
        '
        'txtFind
        '
        Me.txtFind.Location = New System.Drawing.Point(156, 81)
        Me.txtFind.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(487, 31)
        Me.txtFind.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 86)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 25)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Find:"
        '
        'btnMove
        '
        Me.btnMove.Location = New System.Drawing.Point(156, 208)
        Me.btnMove.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(124, 44)
        Me.btnMove.TabIndex = 5
        Me.btnMove.Text = "Move"
        Me.btnMove.UseVisualStyleBackColor = True
        '
        'btnPasteFilename
        '
        Me.btnPasteFilename.Location = New System.Drawing.Point(656, 28)
        Me.btnPasteFilename.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnPasteFilename.Name = "btnPasteFilename"
        Me.btnPasteFilename.Size = New System.Drawing.Size(38, 44)
        Me.btnPasteFilename.TabIndex = 4
        Me.btnPasteFilename.Text = "V"
        Me.btnPasteFilename.UseVisualStyleBackColor = True
        '
        'btnFix
        '
        Me.btnFix.Location = New System.Drawing.Point(731, 28)
        Me.btnFix.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btnFix.Name = "btnFix"
        Me.btnFix.Size = New System.Drawing.Size(124, 44)
        Me.btnFix.TabIndex = 3
        Me.btnFix.Text = "Fix"
        Me.btnFix.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 900000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1691, 1064)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ListBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Form1"
        Me.Text = "Usenet Media Processor"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPathname As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnFix As Button
    Friend WithEvents btnPasteFilename As Button
    Friend WithEvents btnMove As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents txtReplace As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btnRename As Button
    Friend WithEvents txtFind As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnNzbget As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents btnPurge720p As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnCheck As Button
    Friend WithEvents btnMoveUnknown As Button
    Friend WithEvents btnExtract As Button
    Friend WithEvents btnListUnknown As Button
    Friend WithEvents btnUnkownToText As Button
    Friend WithEvents Button2 As Button
End Class
