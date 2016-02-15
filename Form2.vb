Imports System.Net
Imports System.IO
Imports System.Environment
Imports System.Threading
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Ionic.Zip

Public Class Form2
    Dim SW As Stopwatch
    Dim WithEvents WC As New WebClient
    Dim file2 As String = ""
    Dim type2 As String = ""
    Dim count As Integer = 0
    Dim arch As String
    Dim sizefil As Int64 = 0
    Dim assets As String = ""
    Dim dokumenty As String = GetFolderPath(SpecialFolder.MyDocuments)
    Dim SCdata As String = dokumenty & "\StartCraft\"

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

    Public Sub New(ByVal type As String, ByVal file As String, ByVal archi As String)
        InitializeComponent()
        type2 = type
        file2 = file
        arch = archi
    End Sub

    Public Function style()
        If theme = "night" Then
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark
            Me.BackColor = ColorTranslator.FromHtml("#111111")
        ElseIf theme = "day" Then
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light
            Me.BackColor = Color.White
        Else

        End If
        Return Nothing
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        MetroProgressBar1.UseStyleColors = True
        MetroProgressBar2.UseStyleColors = True
        style()
        MetroLabel2.Text = "Downloading: " & type2 & " " & file2
        MetroLabel6.Text = type2 & " " & file2 & " status:"
        Threading.Thread.Sleep(100)
        calc()
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub WC_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        MetroProgressBar1.Value = e.ProgressPercentage
        Dim totalbytes As Long = e.TotalBytesToReceive / 1024
        Dim mtotalbytes As Long = totalbytes / 1024
        Dim bytes As Long = e.BytesReceived / 1024
        Dim mbytes As Long = bytes / 1024
        If totalbytes < 1 Then totalbytes = totalbytes + 1
        If bytes < 1 Then bytes = bytes + 1
        If totalbytes > 1024 Then
            MetroLabel9.Text = mbytes.ToString & " MB of " & mtotalbytes.ToString & " MB"

        Else
            MetroLabel9.Text = bytes.ToString & " KB of " & totalbytes.ToString & " KB"
        End If
        MetroLabel7.Text = "Download Speed: " & FormatNumber(e.BytesReceived / SW.ElapsedMilliseconds / 1000, 2).ToString & " MB/Sec"
    End Sub

    Public Function calc()
        MetroLabel1.Text = "File Downloader - Calculating..."
        Select Case type2
            Case "Minecraft"
                count = count + 2
                Dim client As New WebClient
                client.DownloadFileAsync(New Uri("http://s3.amazonaws.com/Minecraft.Download/versions/" & file2 & "/" & file2 & ".json"), maindir & "versions/" & file2 & "/" & file2 & ".json")
                Do
                    If Not client.IsBusy Then
                        Dim json2 As String = File.ReadAllText(maindir & "versions/" & file2 & "/" & file2 & ".json")
                        Dim parsed As JObject = JObject.Parse(json2)
                        Dim libraries As JArray = parsed.Item("libraries")
                        If json2.Contains("assets""") Then
                            assets = parsed.Item("assets")
                        Else
                            assets = "legacy"
                        End If
                        For Each obj As JObject In libraries
                            Dim name = obj.Item("name")
                            count = count + 1
                        Next
                        Exit Do
                    End If
                Loop
                client.DownloadFileAsync(New Uri("https://s3.amazonaws.com/Minecraft.Download/indexes/" & assets & ".json"), maindir & "assets/indexes/" & assets & ".json")
                Do

                    If Not client.IsBusy Then
                        Dim json As String = File.ReadAllText(maindir & "assets/indexes/" & assets & ".json")
                        Dim parsed As JObject = JObject.Parse(json)
                        Dim myItems = JsonConvert.DeserializeObject(Of Dictionary(Of String, MinecraftItem))(parsed("objects").ToString)
                        For Each kvp As KeyValuePair(Of String, MinecraftItem) In myItems
                            count = count + 1
                        Next
                        Exit Do
                    End If
                Loop
                MetroLabel8.Text = String.Format("0/{0}", count)
                MetroLabel3.Text = "Files count: " & count
                MetroProgressBar2.Maximum = count
        End Select
        Return Nothing
    End Function

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        MetroLabel1.Text = "File Downloader - Downloading..."
        SW = Stopwatch.StartNew
        Dim json2 As String = File.ReadAllText(maindir & "versions/" & file2 & "/" & file2 & ".json")
        Dim json As String = File.ReadAllText(maindir & "assets/indexes/" & assets & ".json")
        Dim parsed As JObject = JObject.Parse(json)
        Dim parsed2 As JObject = JObject.Parse(json2)
        Dim libraries As JArray = parsed2.Item("libraries")
        Dim objects = JsonConvert.DeserializeObject(Of Dictionary(Of String, MinecraftItem))(parsed("objects").ToString)
        Dim count_dirs As Integer
        Dim cfil As Integer = 1
        Dim nativ As String = ""
        WC.DownloadFileAsync(New Uri("http://s3.amazonaws.com/Minecraft.Download/versions/" & file2 & "/" & file2 & ".jar"), maindir & "versions/" & file2 & "/" & file2 & ".jar")
        For Each obj As JObject In libraries
            Do
                If Not WC.IsBusy Then
                    Dim nat As Boolean = False
                    nativ = ""
                    cfil = cfil + 1
                    MetroLabel8.Text = String.Format(cfil & "/{0}", count)
                    MetroProgressBar2.Value = cfil
                    Dim name = obj.Item("name")
                    Dim vals() As String = name.ToString.Split(":")
                    Dim loc As String = vals(0).Replace(".", "/")
                    Dim dirs() As String = loc.Split("/")
                    count_dirs = loc.Split("/").Length - 1
                    Dim natives = obj.Item("natives")
                    If natives IsNot Nothing Then
                        Dim os = natives.Item("windows")
                        If os IsNot Nothing Then
                            If os.ToString.Contains("arch") Then
                                nativ = nativ & "-natives-windows-" & arch
                            Else
                                nativ = nativ & "-natives-windows"
                            End If
                        End If
                    End If
                    Try
                        Select Case count_dirs
                            Case 0
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & vals(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & vals(1) & "/" & vals(2))
                                WC.DownloadFileAsync(New Uri("https://libraries.minecraft.net/" & dirs(0) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar"), maindir & "libraries/" & dirs(0) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar")
                            Case 1
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & vals(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & vals(1) & "/" & vals(2))
                                WC.DownloadFileAsync(New Uri("https://libraries.minecraft.net/" & dirs(0) & "/" & dirs(1) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar"), maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar")
                            Case 2
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & vals(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & vals(1) & "/" & vals(2))
                                WC.DownloadFileAsync(New Uri("https://libraries.minecraft.net/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar"), maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar")
                            Case 3
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & vals(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & vals(1) & "/" & vals(2))
                                WC.DownloadFileAsync(New Uri("https://libraries.minecraft.net/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar"), maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar")
                            Case 4
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & dirs(4))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & dirs(4) & "/" & vals(1))
                                System.IO.Directory.CreateDirectory(maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & dirs(4) & "/" & vals(1) & "/" & vals(2))
                                WC.DownloadFileAsync(New Uri("https://libraries.minecraft.net/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & dirs(4) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar"), maindir & "libraries/" & dirs(0) & "/" & dirs(1) & "/" & dirs(2) & "/" & dirs(3) & "/" & dirs(4) & "/" & vals(1) & "/" & vals(2) & "/" & vals(1) & "-" & vals(2) & nativ & ".jar")
                        End Select
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                    Exit Do
                End If
            Loop
        Next
        For Each kvp As KeyValuePair(Of String, MinecraftItem) In objects
            Dim obj2 As String = kvp.Key
            Dim hash As String = kvp.Value.hash
            Dim size As String = kvp.Value.size.ToString
            Dim hash2 As String = hash.Substring(0, 2)
            Do
                If Not WC.IsBusy Then
                    cfil = cfil + 1
                    MetroLabel8.Text = String.Format(cfil & "/{0}", count)
                    MetroProgressBar2.Value = cfil
                    'If Not System.IO.File.Exists(maindir & "assets/objects/" & hash2 & "/" & hash) Then
                    System.IO.Directory.CreateDirectory(maindir & "assets/objects/" & hash2)
                    WC.DownloadFileAsync(New Uri("http://resources.download.minecraft.net/" & hash2 & "/" & hash), maindir & "assets/objects/" & hash2 & "/" & hash)
                    'End If
                    Exit Do
                End If
            Loop
        Next
        Do
            If Not WC.IsBusy Then
                cfil = cfil + 1
                MetroLabel8.Text = String.Format(cfil & "/{0}", count)
                MetroProgressBar2.Value = cfil
                WC.DownloadFileAsync(New Uri("http://startcraft.pl/launcher/dll/natives.zip"), maindir & "versions/natives/natives.zip")
                Exit Do
            End If
        Loop
        SW.Stop()
        Threading.Thread.Sleep(1000)
        MetroLabel1.Text = "File Downloader - Extracting..."

        Dim ZipToUnpack As String = maindir & "versions/natives/natives.zip"
        Dim UnpackDirectory As String = maindir & "versions/"
        Try
            Do
                If Not WC.IsBusy Then
                    Using zip As ZipFile = ZipFile.Read(ZipToUnpack)
                        Dim entry As ZipEntry
                        zip.TempFileFolder = System.IO.Path.GetTempPath()
                        MetroProgressBar1.Value = 0
                        MetroProgressBar1.Maximum = zip.Entries.Count
                        MetroProgressBar2.Value = 0
                        MetroProgressBar2.Maximum = zip.Entries.Count
                        MetroLabel2.Text = "Extracting: Natives"
                        MetroLabel3.Text = "Files count: " & zip.Entries.Count
                        Dim n = 0
                        For Each entry In zip
                            n = n + 1
                            MetroProgressBar2.Value = MetroProgressBar2.Value + 1
                            MetroLabel8.Text = String.Format(n & "/{0}", zip.Entries.Count)
                            If System.IO.File.Exists(UnpackDirectory & entry.FileName.ToString) Then
                                System.IO.File.Delete(UnpackDirectory & entry.FileName.ToString)
                                Threading.Thread.Sleep(50)
                                entry.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
                                System.Threading.Thread.Sleep(100)
                            Else
                                entry.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
                                System.Threading.Thread.Sleep(100)
                            End If
                            Threading.Thread.Sleep(50)
                        Next
                    End Using
                    Exit Do
                End If
            Loop
        Catch ex1 As Exception
            MsgBox(ex1.ToString)
        End Try
        Threading.Thread.Sleep(1000)
        Me.Close()
    End Sub
End Class

Public Class MinecraftContainer
    Public objects As Dictionary(Of String, MinecraftItem)
End Class

Public Class MinecraftItem
    Public Property hash As String
    Public Property size As Int32
End Class