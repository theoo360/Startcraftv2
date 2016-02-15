Imports System.Xml
Imports System.Environment
Imports System.Net
Imports System.IO
Module conf_read
    Public Sub xmlread()
        Dim dokumenty As String = GetFolderPath(SpecialFolder.MyDocuments)
        Dim SCdata As String = dokumenty & "\StartCraft\"
        version = System.IO.File.ReadAllText(SCdata & "config/current.SCdata")
        If System.IO.File.Exists(SCdata & "config\" & version & ".SCdata") Then
            Try
                Dim ds As New DataSet
                ds.Clear()
                ds.ReadXml(SCdata & "config\" & version & ".SCdata")
                Threading.Thread.Sleep(50)
                maindir = ds.Tables(0).Rows(0)("maindir")
                theme = ds.Tables(0).Rows(0)("theme")
                lang = ds.Tables(0).Rows(0)("lang")
                nick = ds.Tables(0).Rows(0)("nick")
                haslo = ds.Tables(0).Rows(0)("haslo")
                wersja = ds.Tables(0).Rows(0)("wersja")
                wersjaserv = ds.Tables(0).Rows(0)("wersjaserv")
                logtype = ds.Tables(0).Rows(0)("logtype")
                rammax = ds.Tables(0).Rows(0)("rammax")
                rammin = ds.Tables(0).Rows(0)("rammin")
                dzwiek = ds.Tables(0).Rows(0)("dzwiek")
                jvmdir = ds.Tables(0).Rows(0)("jvmdir")
                res = ds.Tables(0).Rows(0)("res")
                resx = ds.Tables(0).Rows(0)("resx")
                resy = ds.Tables(0).Rows(0)("resy")
                minimize = ds.Tables(0).Rows(0)("minimize")
                ds.EndInit()
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[INFO] StartCraft config read successfull" & vbCrLf)
                If nick = "" Then
                    nick = "Guest"
                End If
            Catch ex As Exception
                Form1.console.AppendText(Now.ToString("<" & "yyyy-MM-dd") & " " & Now.ToString("HH-mm-ss") & ">" & "[ERROR] Can not read config file!" & vbCrLf)
                MessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub
End Module
