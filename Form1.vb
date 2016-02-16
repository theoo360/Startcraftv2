Imports Microsoft.VisualBasic.Devices
Imports System.Management
Imports System
Imports Microsoft.Win32
Imports System.IO
Imports System.Environment
Imports System.Net
Public Class Form1
    Dim dokumenty As String = GetFolderPath(SpecialFolder.MyDocuments)
    Dim SCdata As String = dokumenty & "\StartCraft\"

#Region "Colors"
    Private Sub MetroLabel2_Click(sender As Object, e As EventArgs) Handles MetroLabel2.Click
        Me.MetroContextMenu1.Show(Me.MetroLabel2, New Point(0, 19))
    End Sub

    Private Sub DefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Default
    End Sub

    Private Sub BlackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlackToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Black
    End Sub

    Private Sub WhiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WhiteToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.White
    End Sub

    Private Sub SilverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SilverToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Silver
    End Sub

    Private Sub BlueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlueToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue
    End Sub

    Private Sub GreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GreenToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Green
    End Sub

    Private Sub LimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimeToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Lime
    End Sub

    Private Sub TealToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TealToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Teal
    End Sub

    Private Sub OrangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrangeToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
    End Sub

    Private Sub BrownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrownToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Brown
    End Sub

    Private Sub PinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PinkToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Pink
    End Sub

    Private Sub MagentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MagentaToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Magenta
    End Sub

    Private Sub PurpleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurpleToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple
    End Sub

    Private Sub RedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Red
    End Sub

    Private Sub YellowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YellowToolStripMenuItem.Click
        MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Yellow
    End Sub
#End Region

#Region "StateButtons"
    Private Sub MetroLink1_Click(sender As Object, e As EventArgs) Handles MetroLink1.Click
        Close()
    End Sub

    Private Sub MetroLink2_Click(sender As Object, e As EventArgs) Handles MetroLink2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If theme = "day" Then
            theme = "night"
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark
            Me.BackColor = ColorTranslator.FromHtml("#111111")
            BetterListView1.BackColor = ColorTranslator.FromHtml("#111111")
            BetterListView1.ForeColor = Color.White
            BetterListViewColumnHeader1.ForeColor = Color.Black
            BetterListViewColumnHeader2.ForeColor = Color.Black
            BetterListViewColumnHeader3.ForeColor = Color.Black
            BetterListViewColumnHeader4.ForeColor = Color.Black
            PictureBox2.Image = My.Resources.sun
        ElseIf theme = "night" Then
            theme = "day"
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light
            Me.BackColor = Color.White
            BetterListView1.BackColor = Color.White
            BetterListView1.ForeColor = Color.Black
            PictureBox2.Image = My.Resources.moon
        Else

        End If
        savexml()
    End Sub

    Public Function style()
        If theme = "night" Then
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark
            Me.BackColor = ColorTranslator.FromHtml("#111111")
            BetterListView1.BackColor = ColorTranslator.FromHtml("#111111")
            BetterListView1.ForeColor = Color.White
            BetterListViewColumnHeader1.ForeColor = Color.Black
            BetterListViewColumnHeader2.ForeColor = Color.Black
            BetterListViewColumnHeader3.ForeColor = Color.Black
            BetterListViewColumnHeader4.ForeColor = Color.Black
            PictureBox2.Image = My.Resources.sun
        ElseIf theme = "day" Then
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light
            Me.BackColor = Color.White
            BetterListView1.BackColor = Color.White
            BetterListView1.ForeColor = Color.Black
            PictureBox2.Image = My.Resources.moon
        Else

        End If
        savexml()
        Return Nothing
    End Function
#End Region

#Region "Drag&drop"
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            MyBase.OnPaint(e)
        Catch ex As Exception
            Me.Invalidate()
        End Try
    End Sub
    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, MetroTile1.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub
    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, MetroTile1.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, MetroTile1.MouseUp
        drag = False
    End Sub
#End Region

#Region "PC Info"
    Enum InfoTypes
        OperatingSystemName
        ProcessorName
        AmountOfMemory
        VideocardName
        VideocardMem
        ProcessorArch
    End Enum

    Public Function GetInfo(ByVal InfoType As InfoTypes) As String
        Dim Info As New ComputerInfo : Dim Value, vganame, vgamem, proc As String
        Dim searcher As New Management.ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_VideoController")
        Dim searcher1 As New Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        If InfoType = InfoTypes.OperatingSystemName Then
            Value = Info.OSFullName
        ElseIf InfoType = InfoTypes.ProcessorName Then
            For Each queryObject As ManagementObject In searcher1.Get
                proc = queryObject.GetPropertyValue("Name").ToString
            Next
            Value = proc
        ElseIf InfoType = InfoTypes.VideocardName Then
            For Each queryObject As ManagementObject In searcher.Get
                vganame = queryObject.GetPropertyValue("Name").ToString
            Next
            Value = vganame
        ElseIf InfoType = InfoTypes.VideocardMem Then
            For Each queryObject As ManagementObject In searcher.Get
                vgamem = queryObject.GetPropertyValue("AdapterRAM").ToString
            Next
            Value = Math.Round((((CDbl(Convert.ToDouble(Val(vgamem))) / 1024)) / 1024), 2) & " MB"
        ElseIf InfoType = InfoTypes.ProcessorArch Then
            Dim arch As Boolean = Environment.Is64BitOperatingSystem
            Dim arch2 As String
            If arch = True Then
                arch2 = "64"
            Else
                arch2 = "32"
            End If
            Value = arch2
        End If
        Return Value
    End Function

    Public Function PCinfo() As String
        'Dim listView As New BetterListView

        'Dim item As New BetterListViewItem("new item")

        'listView.Items.Add(item)

        'listView.View = BetterListViewView.List

        MetroTextBox4.Text = vbCrLf _
            & "Computer Name: " & My.Computer.Name & vbCrLf _
            & "User Name: " & My.User.Name & vbCrLf _
            & "OS Name: " & My.Computer.Info.OSFullName & vbCrLf _
            & "OS Platform: " & My.Computer.Info.OSPlatform & vbCrLf _
            & "OS Version: " & My.Computer.Info.OSVersion & vbCrLf _
            & "OS Architecture: " & GetInfo(InfoTypes.ProcessorArch) & vbCrLf _
            & "Total Physical Memory: " & Math.Round((((CSng(Convert.ToDouble(Val(My.Computer.Info.TotalPhysicalMemory))) / 1024)) / 1024), 0) & " MB" & vbCrLf _
            & "Total Virtual Memory: " & Math.Round((((CSng(Convert.ToDouble(Val(My.Computer.Info.TotalVirtualMemory))) / 1024)) / 1024), 0) & " MB" & vbCrLf _
            & "OS Language: " & My.Computer.Info.InstalledUICulture.TwoLetterISOLanguageName.ToUpper & vbCrLf _
            & "Video Card: " & GetInfo(InfoTypes.VideocardName) & vbCrLf _
            & "Video Card Memory: " & GetInfo(InfoTypes.VideocardMem) & vbCrLf _
            & "Processor: " & GetInfo(InfoTypes.ProcessorName) & vbCrLf _
            & "Java Version: " & GetJavaInfo.General.CurrentJavaVersion & vbCrLf _
            & "Java Path: " & GetJavaInfo.General.CurrentJavaPath & "\bin\" & vbCrLf
        If jvmdir = "" Then
            MetroTextBox13.Text = GetJavaInfo.General.CurrentJavaPath & "\bin\"
        End If
        Return Nothing
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MetroProgressSpinner1.Value = ((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / My.Computer.Info.TotalPhysicalMemory) * 100
        MetroLabel3.Text = MetroProgressSpinner1.Value & "%"

        MetroProgressSpinner2.Value = PerformanceCounter1.NextValue
        MetroLabel4.Text = MetroProgressSpinner2.Value.ToString & "%"

        If MetroProgressSpinner1.Value >= 80 Then
            MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red
        ElseIf MetroProgressSpinner1.Value >= 50 Then
            MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Orange
        ElseIf MetroProgressSpinner1.Value >= 30 Then
            MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Green
        Else
            MetroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Blue
        End If

        If MetroProgressSpinner2.Value >= 80 Then
            MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Red
        ElseIf MetroProgressSpinner2.Value >= 50 Then
            MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Orange
        ElseIf MetroProgressSpinner2.Value >= 30 Then
            MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Green
        Else
            MetroProgressSpinner2.Style = MetroFramework.MetroColorStyle.Blue
        End If
    End Sub
#End Region

#Region "PClog"
    Public Function pclog(sender As String) As String
        console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Generating PC log file." & vbCrLf)
        Dim TargetFile As StreamWriter
        Dim plik As String = DateTime.Now.ToString("yyyy-MM-dd") & "_" & DateTime.Now.ToString("HH-mm-ss")
        Try
            System.IO.File.Create(SCdata & "logs/" & plik & ".SClog").Dispose()
            TargetFile = New StreamWriter(SCdata & "logs\" & plik & ".SClog", True)
        Catch ex As Exception
            console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[ERROR] Could not open PC log file. Something gone wrong." & vbCrLf & ex.Message & vbCrLf)
        End Try

        Try
            TargetFile.Write(" ######     ####            ######   ##   ##  #######    ###    " & vbCrLf & _
                             " ##   ##   ##  ##             ##     ###  ##  ##        ## ##   " & vbCrLf & _
                             " ##   ##  ##                  ##     ###  ##  ##       ##   ##  " & vbCrLf & _
                             " ######   ##                  ##     ## # ##  #####    ##   ##  " & vbCrLf & _
                             " ##       ##                  ##     ## # ##  ##       ##   ##  " & vbCrLf & _
                             " ##        ##  ##             ##     ##  ###  ##        ## ##   " & vbCrLf & _
                             " ##         ####            ######   ##   ##  ##         ###    " & vbCrLf & vbCrLf)
            TargetFile.WriteLine(Now())
            TargetFile.Write(MetroTextBox4.Text)
            TargetFile.WriteLine()
            TargetFile.WriteLine()
            TargetFile.Write("    ####     ###    ##   ##   #####     ###    ##       ####### " & vbCrLf & _
                             "  ##  ##   ## ##   ###  ##  ##   ##   ## ##   ##       ##       " & vbCrLf & _
                             " ##       ##   ##  ###  ##  ##       ##   ##  ##       ##       " & vbCrLf & _
                             " ##       ##   ##  ## # ##   #####   ##   ##  ##       #####    " & vbCrLf & _
                             " ##       ##   ##  ## # ##       ##  ##   ##  ##       ##       " & vbCrLf & _
                             "  ##  ##   ## ##   ##  ###  ##   ##   ## ##   ##       ##       " & vbCrLf & _
                             "   ####     ###    ##   ##   #####     ###    ######   #######  " & vbCrLf & vbCrLf)
            TargetFile.WriteLine(Now())
            TargetFile.Write(console.Text)
            TargetFile.WriteLine()
            TargetFile.WriteLine()
        Catch ex As Exception
            console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[ERROR] Could not save PC log file. Something gone wrong." & vbCrLf & ex.Message & vbCrLf)
        End Try

        TargetFile.Close()
        console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] PC log file created successfully." & vbCrLf)
        console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] File: " & SCdata & "logs\" & plik & ".SClog" & vbCrLf)
        If sender = "button2" Then
            Process.Start("explorer.exe", "/select," & SCdata & "logs\" & plik & ".SClog")
        End If
        Return Nothing
    End Function

    Private Sub Button_PClog(sender As Object, e As EventArgs) Handles MetroButton2.Click, MetroButton3.Click
        If sender.name = "MetroButton2" Then
            pclog("button1")
        Else
            pclog("button2")
        End If
    End Sub
#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ParseCommandLineArgs()
        Try
            console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Console logs started." & vbCrLf)
            PCinfo()
            console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] PC log read successfull." & vbCrLf)
            xmlread()
            net()
            style()
            Select Case logtype
                Case 0
                    console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] NonPremium account type choosen." & vbCrLf)
                    MetroComboBox1.SelectedIndex = 0
                Case 1
                    console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] StartCraft account type choosen." & vbCrLf)
                    MetroComboBox1.SelectedIndex = 1
                Case 2
                    console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Premium account type choosen." & vbCrLf)
                    MetroComboBox1.SelectedIndex = 2
                Case Else
                    console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[WARNING] No account type specifed!" & vbCrLf)
            End Select
            console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Welcome " & nick & "!" & vbCrLf)

            If nick = "" Or nick = "Guest" Or nick = "guest" Then
                Nick8.Text = "Guest"
            Else
                Nick8.Text = nick
                MetroTextBox1.Text = nick
            End If
            checkboxvers()

            MetroTextBox10.Text = maindir
            MetroTextBox9.Text = version
            MetroTextBox11.Text = rammin
            MetroTextBox12.Text = rammax
            MetroToggle1.Checked = dzwiek
            PictureBox4.Image = Image.FromFile(SCdata & "icons/warning.gif")
            PictureBox5.Image = Image.FromFile(SCdata & "icons/true.gif")
            PictureBox6.Image = Image.FromFile(SCdata & "icons/false.gif")
            
            verlist()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=XUBYF6J4XG36W")
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        If Not wersja = "" Or Not nick = "" Then
            nick = MetroTextBox1.Text
            If MetroCheckBox1.Checked = True Then
                haslo = MetroTextBox2.Text
            End If
            'Select Case MetroComboBox1.SelectedIndex
            '    Case 0
            '        If tmp_net = True Then
            '            Try
            '                Dim client As New WebClient
            '                Dim response As New StreamReader(client.OpenRead("http://startcraft.pl/launcher/chck/versions.SCdata"))
            '                Select Case response
            '                    Case 0

            '                    Case 1

            '                End Select
            '            Catch ex As Exception
            '                net()
            '            End Try
            '        Else
            '        End If
            '    Case 1

            '    Case 2

            '    Case Else

            'End Select
            savexml()
            console.AppendText(vbCrLf)
            startmc("Start", "Craft")
        End If
    End Sub

    Private Sub MetroComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroComboBox3.SelectedIndexChanged
        verlist()
    End Sub

    Private Sub MetroButton8_Click(sender As Object, e As EventArgs) Handles MetroButton8.Click
        maindir = MetroTextBox10.Text
        rammin = MetroTextBox11.Text
        rammax = MetroTextBox12.Text
        dzwiek = MetroToggle1.Checked
        jvmdir = MetroTextBox13.Text
        Threading.Thread.Sleep(100)
        savexml()
    End Sub

    Private Sub MetroComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroComboBox2.SelectedIndexChanged
        wersja = MetroComboBox2.SelectedItem.ToString
    End Sub

    Public Sub StartWrapServer()

        If System.IO.Directory.Exists(maindir & "\" & wersja) Then
        Else
            System.IO.Directory.CreateDirectory(maindir & "\" & wersja)
        End If
        Using p As New Process
            AddHandler p.OutputDataReceived, AddressOf Process_OutputDataReceived
            With p.StartInfo
                .FileName = jvmdir & "javaw.exe"
                .Arguments = mcstart.code
                .CreateNoWindow = True
                .UseShellExecute = False
                .RedirectStandardInput = True
                .RedirectStandardOutput = True
            End With
            p.Start()
            p.BeginOutputReadLine()
        End Using
    End Sub

    Private Delegate Sub LogDelegate(ByVal message)

    Private Sub AppendLogLine(ByVal message As String)
        With console
            If .InvokeRequired Then
                .Invoke(New LogDelegate(AddressOf AppendLogLine), message)
            Else
                .AppendText(message & vbNewLine)
            End If
        End With
    End Sub

    Private Sub Process_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
        AppendLogLine(e.Data)
    End Sub

    Private Sub MetroButton7_Click(sender As Object, e As EventArgs) Handles MetroButton7.Click
        If Not Directory.Exists(maindir & "versions/") Then
            Directory.CreateDirectory(maindir & "versions/")
        End If
        If Not Directory.Exists(maindir & "versions/" & BetterListView1.SelectedItems(0).Text) Then
            Directory.CreateDirectory(maindir & "versions/" & BetterListView1.SelectedItems(0).Text)
        End If
        If Not Directory.Exists(maindir & "versions/natives") Then
            Directory.CreateDirectory(maindir & "versions/natives")
        End If
        If Not Directory.Exists(maindir & "assets/") Then
            Directory.CreateDirectory(maindir & "assets/")
        End If
        If Not Directory.Exists(maindir & "assets/indexes/") Then
            Directory.CreateDirectory(maindir & "assets/indexes/")
        End If
        Dim pass As String = "Head"
        Dim frm As New Form2("Minecraft", BetterListView1.SelectedItems(0).Text, GetInfo(InfoTypes.ProcessorArch))
        frm.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        checkboxvers()
        verlist()
    End Sub

    Private Sub ParseCommandLineArgs()
        If Not My.Application.CommandLineArgs.Contains("f8450bee5dbfbae9d704e17f1631da9953d3d046241cfeec06dc4b4b6ead2b36") Then
            Me.Close()
            MsgBox("Sorry, run updater first." & vbCrLf & "If you ran this by updater please download new version from: StartCraft.pl")
        End If
    End Sub

    Private Sub MetroButton12_Click(sender As Object, e As EventArgs) Handles MetroButton12.Click
        lang = MetroTextBox17.Text
        savexml()
    End Sub
End Class

