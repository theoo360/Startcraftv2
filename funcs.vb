Imports System.IO
Imports System.Net
Imports System.Environment
Imports ComponentOwl.BetterListView

Module funcs
    Dim dokumenty As String = GetFolderPath(SpecialFolder.MyDocuments)
    Dim SCdata As String = dokumenty & "\StartCraft\"
    Public Function checkboxvers()
        If System.IO.Directory.Exists(maindir & "versions") Then
            Dim di As New DirectoryInfo(maindir & "versions")
            Form1.MetroComboBox2.Items.Clear()
            If di.Exists = True Then
                For Each subDirectory As System.IO.DirectoryInfo In di.GetDirectories.OrderByDescending(Function(s) s.FullName)
                    If Not subDirectory.Name.ToString = "natives" Then
                        Form1.MetroComboBox2.Items.Add(subDirectory.Name.ToString)
                    End If
                Next
            End If
        End If
        For Each item In Form1.MetroComboBox2.Items
            If item.ToString = wersja Then
                Form1.MetroComboBox2.SelectedItem = item
            End If
        Next
        Return Nothing
    End Function

    Public Function verlist()
        If tmp_net = True Then
            Dim client As New WebClient
            Dim mcvers As New StreamReader(client.OpenRead("http://startcraft.pl/launcher/chck/versions.SCdata"))
            Dim line As String = ""
            Dim split() As String
            Form1.BetterListView1.Items.Clear()
            Form1.BetterListView1.View = View.Details
            Do While mcvers.Peek() > -1
                line = mcvers.ReadLine()
                Dim veritem As New BetterListViewItem
                Dim tmp_cat As Boolean
                split = line.Split(";")
                veritem.Text = split(1)

                Select Case Form1.MetroComboBox3.SelectedIndex
                    Case 0
                        tmp_cat = True
                    Case 1
                        If split(2) = "full" Then
                            tmp_cat = True
                        Else
                            tmp_cat = False
                        End If
                    Case 2
                        If split(2) = "beta" Then
                            tmp_cat = True
                        Else
                            tmp_cat = False
                        End If
                    Case 3
                        If split(2) = "alpha" Then
                            tmp_cat = True
                        Else
                            tmp_cat = False
                        End If
                    Case Else
                        tmp_cat = True
                End Select

                If tmp_cat = True Then
                    If split(0) = "t" Then
                        If lang = "f44e64e75f3948e9f73f8dfa94721c4ce8cbb4f265c4790c702b2d41cfbf2753" Then
                            Form1.BetterListView1.Items.Add(veritem)
                            If split(0) = "b" Then
                                veritem.Image = Image.FromFile(SCdata & "icons/warning.gif")
                            End If
                            veritem.UseItemStyleForSubItems = False
                            If split(3) = "true" Then
                                veritem.SubItems.Add("")
                                veritem.SubItems(1).Image = Image.FromFile(SCdata & "icons/true.gif")
                            Else
                                veritem.SubItems.Add("")
                                veritem.SubItems(1).Image = Image.FromFile(SCdata & "icons/false.gif")
                            End If
                            If split(4) = "true" Then
                                veritem.SubItems.Add("")
                                veritem.SubItems(2).Image = Image.FromFile(SCdata & "icons/true.gif")
                            Else
                                veritem.SubItems.Add("")
                                veritem.SubItems(2).Image = Image.FromFile(SCdata & "icons/false.gif")
                            End If
                            If Form1.MetroComboBox2.Items.Contains(split(1)) Then
                                veritem.SubItems.Add("")
                                veritem.SubItems(3).Image = Image.FromFile(SCdata & "icons/true.gif")
                            Else
                                veritem.SubItems.Add("")
                                veritem.SubItems(3).Image = Image.FromFile(SCdata & "icons/false.gif")
                            End If
                        End If
                    Else
                        Form1.BetterListView1.Items.Add(veritem)
                        If split(0) = "b" Then
                            veritem.Image = Image.FromFile(SCdata & "icons/warning.gif")
                        End If
                        veritem.UseItemStyleForSubItems = False
                        If split(3) = "true" Then
                            veritem.SubItems.Add("")
                            veritem.SubItems(1).Image = Image.FromFile(SCdata & "icons/true.gif")
                        Else
                            veritem.SubItems.Add("")
                            veritem.SubItems(1).Image = Image.FromFile(SCdata & "icons/false.gif")
                        End If
                        If split(4) = "true" Then
                            veritem.SubItems.Add("")
                            veritem.SubItems(2).Image = Image.FromFile(SCdata & "icons/true.gif")
                        Else
                            veritem.SubItems.Add("")
                            veritem.SubItems(2).Image = Image.FromFile(SCdata & "icons/false.gif")
                        End If
                        If Form1.MetroComboBox2.Items.Contains(split(1)) Then
                            veritem.SubItems.Add("")
                            veritem.SubItems(3).Image = Image.FromFile(SCdata & "icons/true.gif")
                        Else
                            veritem.SubItems.Add("")
                            veritem.SubItems(3).Image = Image.FromFile(SCdata & "icons/false.gif")
                        End If
                    End If
                End If
            Loop
            mcvers.Close()
        End If
        Return Nothing
    End Function

    Public Function net()
        If My.Computer.Network.Ping("www.google.com") Then
            tmp_net = True
            Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Internet connection working." & vbCrLf)
            Form1.MetroLabel7.Text = "Online Mode"
            Form1.MetroLabel7.Style = MetroFramework.MetroColorStyle.Lime
            If Form1.console.Text.Contains("launcher stats") Then
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] Some launcher stats:" & vbCrLf)
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "Users registred: 600" & vbCrLf)
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "Currently logged: 1642 users" & vbCrLf)
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "Launcher starts: 324032 times" & vbCrLf)
            End If
        Else
            Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[WARNING] No internet connection, some fuctions need connection to work." & vbCrLf)
            Form1.MetroLabel7.Text = "Offline Mode"
            Form1.MetroLabel7.Style = MetroFramework.MetroColorStyle.Red
        End If
        Return Nothing
    End Function
End Module
