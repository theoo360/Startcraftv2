Imports System.Xml
Imports System.Environment
Module conf_save
    Public Sub savexml()
        Dim dokumenty As String = GetFolderPath(SpecialFolder.MyDocuments)
        Dim SCdata As String = dokumenty & "\StartCraft\"
        If System.IO.File.Exists(SCdata & "config\" & version & ".SCdata") Then
            Dim ds As New DataSet
            ds.Clear()
            ds.ReadXml(SCdata & "config\" & version & ".SCdata")
            ds.Tables(0).Rows(0)("maindir") = maindir
            ds.Tables(0).Rows(0)("theme") = theme
            ds.Tables(0).Rows(0)("lang") = lang
            ds.Tables(0).Rows(0)("nick") = nick
            ds.Tables(0).Rows(0)("haslo") = haslo
            ds.Tables(0).Rows(0)("wersja") = wersja
            ds.Tables(0).Rows(0)("wersjaserv") = wersjaserv
            ds.Tables(0).Rows(0)("logtype") = logtype
            ds.Tables(0).Rows(0)("rammax") = rammax
            ds.Tables(0).Rows(0)("rammin") = rammin
            ds.Tables(0).Rows(0)("dzwiek") = dzwiek
            ds.Tables(0).Rows(0)("jvmdir") = jvmdir
            ds.Tables(0).Rows(0)("res") = res
            ds.Tables(0).Rows(0)("resx") = resx
            ds.Tables(0).Rows(0)("resy") = resy
            ds.Tables(0).Rows(0)("minimize") = minimize
            ds.WriteXml(SCdata & "config\" & version & ".SCdata")
        End If
    End Sub
End Module
