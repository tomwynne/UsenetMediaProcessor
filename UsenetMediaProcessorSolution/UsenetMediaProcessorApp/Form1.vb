Imports System.ComponentModel
Imports System.IO
Imports System.Net

Public Class Form1

    Private LastError = ""

    Private TVFolders() = {
        "\\Plex-server\d\TV_Comedy",
        "\\Plex-server\d\TV_Crime",
        "\\Plex-server\d\TV_Drama",
        "\\Synology64\tv1\TV_Action",
        "\\Synology64\tv1\TV_Animation",
        "\\Synology64\tv2\TV_Documentary",
        "\\Synology64\tv1\TV_Fantasy",
        "\\Synology64\tv1\TV_Horror",
        "\\Synology64\tv1\TV_Sci-Fi",
        "\\Synology64\tv1\TV_Superheroes",
        "\\Synology64\tv2\TV_Reality"
    }

    Private MovieFolders() = {
        "\\Plex-server\d\Movies"
    }

    Private NewsLeecherPath = "G:\NewsLeecher"

    Private WithEvents wc As WebClient

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        AddToLibrary()
        'MoveFiles("e:\downloads", "e:\downloads720p", "*720p*.*")
        'MoveFiles("e:\downloads", "e:\downloads_x265", "*x265*.*")
        If chkX.Checked Then
            ExtractArchives(NewsLeecherPath & "\alt.binaries.erotica", "e:\x")      ' part01.rar
            ExtractArchives2(NewsLeecherPath & "\alt.binaries.erotica", "e:\x")     ' .rar
        End If
        AddStatus("Done.")
        'Timer1.Enabled = True
    End Sub

    Private Sub btnFix_Click(sender As Object, e As EventArgs) Handles btnFix.Click
        AddStatus("FIX: Renaming files...")
        For Each sFilename In Directory.GetFiles(txtPathname.Text)
            If sFilename.EndsWith(".mkv") Then
                Dim sNewFilename = Path.GetDirectoryName(sFilename) & "\" & FixFilename(Path.GetFileNameWithoutExtension(sFilename)) & ".mkv"
                If RenameFile(sFilename, sNewFilename) Then
                    AddStatus(sNewFilename)
                Else
                    AddStatus("Error Renaming:" & sFilename & " - " & LastError)
                End If
            End If
        Next
        AddStatus("FIX: Complete.")
    End Sub

    Private Function RenameFile(Filename As String, NewFilename As String) As Boolean
        Try
            If Filename = NewFilename Then Return True
            'If NewFilename = "nfo" Then
            '    MessageBox.Show("nfo file")
            'End If
            AddStatus("Rename File: " & Filename & " as " & NewFilename)
            Rename(Filename, NewFilename)
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

    Private Function FixFilename(Filename As String) As String
        Try
            Filename = Filename.Replace(" ", ".")
            Filename = Filename.ToLower()
            Filename = Filename.Replace("repack.", "")
            ' Better.Things.S02E10.Graduation.720p.AMZN.WEB-DL.DDP5.1.H.264-NTb
            Filename = RemoveAfter(Filename, "WEB-DL")
            Filename = RemoveAfter(Filename, "WEBRip")
            Filename = RemoveAfter(Filename, "WEB.X264")
            Filename = RemoveAfter(Filename, "WEB.H264")
            Filename = UpperCaseWords(Filename)
            Filename = Filename.Replace("Ncis", "NCIS").Replace("La.", "LA.")
            Return Filename
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function RemoveAfter(s As String, FindString As String) As String
        If s.ToLower().Contains(FindString.ToLower()) Then
            Return s.Substring(0, s.IndexOf(FindString.ToLower()) + FindString.Length)
        End If
        Return s
    End Function
    Private Function UpperCaseWords(s As String) As String
        Dim sParts() = s.Split(".")
        s = ""
        For Each sPart In sParts
            If s <> "" Then
                s = s & "." & Capitalize(sPart)
            Else
                s = Capitalize(sPart)
            End If
        Next
        Return s
    End Function

    Private Function Capitalize(s As String) As String
        If s.Length = 0 Then Return ""
        If s.Length = 1 Then Return s.ToUpper()
        If s.ToLower() = "web-dl" Then Return "WEB-DL"
        If s.ToLower() = "webrip" Then Return "WEBRip"
        If s.ToLower() = "web.x264" Then Return "WEB.x264"
        If s.ToLower() = "web.h264" Then Return "WEB.h264"
        If s.Length = 6 Then
            If s.Substring(0, 1).ToLower() = "s" And s.Substring(3, 1).ToLower() = "e" Then      ' S01E06
                Return "S" & s.Substring(1, 2) & "E" & s.Substring(4)
            End If
        End If
        If s.Length = 9 Then
            If IsNumeric(s.Substring(1, 2)) Then
                If s.Substring(0, 1).ToLower() = "s" And s.Substring(3, 1).ToLower() = "e" And s.Substring(6, 1).ToLower() = "e" Then      ' S01E01E02
                    Return s.Replace("s", "S").Replace("e", "E")
                End If
            End If
        End If
        Return s.Substring(0, 1).ToUpper() & s.Substring(1)
    End Function

    Private Sub ClearStatus()
        ListBox1.Items.Clear()
    End Sub

    Private Sub AddStatus(s As String)
        ListBox1.Items.Add(s)
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        ListBox1.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub UpdateStatus(s As String)
        If ListBox1.Items.Count = 0 Then Return
        ListBox1.Items.Item(ListBox1.Items.Count - 1) = s
        ListBox1.Refresh()
        Application.DoEvents()
    End Sub
    Private Sub UpdateStatusPercent(i As Integer)
        Static l As Integer = -1
        If i = l Then Return
        If ListBox1.Items.Count = 0 Then Return
        Dim s As String = ListBox1.Items.Item(ListBox1.Items.Count - 1)
        Dim p As Integer = s.IndexOf(" - ")
        If p = 0 Or p = -1 Then
            s = s & " - " & Str(i) & "%"
        Else
            s = s.Substring(0, p) & " - " & Str(i) & "%"
        End If
        ListBox1.Items.Item(ListBox1.Items.Count - 1) = s
        'ListBox1.Refresh()
        Application.DoEvents()

    End Sub

    Private Sub AppendStatus(s As String)
        ListBox1.Items.Item(ListBox1.Items.Count - 1) &= s
        ListBox1.Refresh()
    End Sub

    Private Sub btnPasteFilename_Click(sender As Object, e As EventArgs) Handles btnPasteFilename.Click
        txtPathname.Text = Clipboard.GetText()
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        AddToLibrary()
    End Sub

    Private Function GetSeason(s As String) As String
        For i = 1 To 99
            If s.Contains(".S" & i.ToString("00") & "E") Then
                Return i.ToString("00")
            End If
        Next
        Return ""
    End Function

    Private Function GetEpisode(s As String) As String
        For i = 1 To 99
            If s.Contains("E" & i.ToString("00") & ".") Then
                Return i.ToString("00")
            End If
        Next
        Return ""
    End Function

    Private Function GetShowName(sFilename As String) As String
        Dim s = sFilename.Substring(0, sFilename.IndexOf("S" & GetSeason(sFilename))).Replace(".", " ").TrimEnd()
        Return s
    End Function

    Private Function IsATVShow(sFilename As String) As Boolean
        If sFilename.EndsWith(".mkv") Then
            ' check if a TV show
            For i = 1 To 99
                If sFilename.Contains(".S" & i.ToString("00") & "E") Then
                    Return True
                End If
            Next
            For i = 0 To 600
                Dim d = DateTime.Today.AddDays(-i)
                If sFilename.Contains(d.Year.ToString("00") & "." & d.Month.ToString("00") & "." & d.Day.ToString("00")) Then
                    Return True
                ElseIf sFilename.Contains(d.Year.ToString("00") & "-" & d.Month.ToString("00") & "-" & d.Day.ToString("00")) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    ' \\Plexserver\e\TV_Horror\Z Nation [2014]\Season 04\Z.Nation.S04E01.Black.Rainbow.1080p.Amzn.WEB-DL.mkv
    Private Function GetTVFolder(sFilename As String) As String
        Dim sSeason = GetSeason(sFilename)
        If sSeason <> "" Then
            Dim sEpisode = GetEpisode(sFilename)
            If sEpisode <> "" Then
                Dim sShowName = GetShowName(sFilename)
                Dim sTVFolder = FindTVFolder(sShowName)
                If sTVFolder <> "" Then
                    Return sTVFolder & "\Season " & sSeason & "\"
                End If
            End If
        End If

        Return ""
    End Function

    Private Function IsAMovie(sFilename As String) As Boolean
        If sFilename.EndsWith(".mkv") Then
            If Not IsATVShow(sFilename) Then
                For iYear = 1910 To DateTime.Today.Year
                    If sFilename.Contains(iYear.ToString) And (sFilename.Contains("BDRip") Or sFilename.Contains("DVDRip") Or sFilename.Contains("720p") Or sFilename.Contains("1080p")) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    ' \\Synology01\media1\Movies03\2017\Baby Driver [2017]\Baby.Driver.2017.1080p.BluRay.mkv
    Private Function GetMovieFolder(sFilename As String) As String
        If sFilename.EndsWith(".mkv") Then
            If IsAMovie(sFilename) Then
                For iYear = DateTime.Today.Year To 1910 Step -1
                    If sFilename.LastIndexOf("." & iYear.ToString & ".") <> -1 And (sFilename.Contains("DVDRip") Or sFilename.Contains("BDRip") Or sFilename.Contains("720p") Or sFilename.Contains("1080p")) Then
                        Return FindMovieFolder(iYear) & "\" & GetMovieName(sFilename) & " [" & iYear.ToString() & "]" & "\"
                    End If
                Next
            End If

        End If
        Return ""
    End Function


    Private Function FindTVFolder(ShowName As String) As String
        For iYear = 1980 To DateTime.Today.Year
            If ShowName.Contains(" " & iYear.ToString()) Then       ' if show name contains year: The Flash 2014
                ShowName = ShowName.Replace(" " & iYear.ToString, " [" & iYear.ToString() & "]")
                Exit For
            End If
        Next
        For Each sTVFolder In TVFolders
            For Each sFolder In Directory.EnumerateDirectories(sTVFolder)
                Dim sFolderShow = sFolder.Replace("'", "").Replace(",", "").Replace(".", "").ToLower()
                If sFolder.ToLower().Contains("\" & ShowName.ToLower() & " [") Or sFolderShow.Contains("\" & ShowName.ToLower() & " [") Then
                    Return sFolder
                End If
            Next
        Next
        Return ""
    End Function

    Private Function FindMovieFolder(ReleaseYear As Integer) As String

        For Each sMovieFolder In MovieFolders
            If Directory.Exists(sMovieFolder & "\" & ReleaseYear.ToString) Then
                Return sMovieFolder & "\" & ReleaseYear.ToString
            End If
        Next
        Return ""
    End Function

    Private Function GetMovieYear(sFilename As String) As Integer
        For iYear = DateTime.Today.Year To 1910 Step -1
            If sFilename.Contains("." & iYear.ToString & ".") Then
                Return iYear
            End If
        Next
        Return 0
    End Function

    ' Baby.Driver.2017.1080p.BluRay.mkv -> Baby Driver
    Private Function GetMovieName(sFilename As String) As String
        sFilename = Path.GetFileNameWithoutExtension(sFilename)
        Dim iYear = GetMovieYear(sFilename)
        Return sFilename.Substring(0, sFilename.LastIndexOf("." & iYear.ToString & ".")).Replace(".", " ").Trim()
    End Function

    Private Sub btnTest_Click(sender As Object, e As EventArgs) 
        TestArchive("N:\\171018_18\171018_18.part01.rar", "171018")
    End Sub

    Private Function TestArchive(ArchiveFile As String) As Boolean
        Return TestArchive(ArchiveFile, "")
    End Function

    Private Function TestArchive(ArchiveFile As String, Password As String) As Boolean
        If Not ArchiveFile.Contains(".rar") Then Return False
        AddStatus("Testing archive '" & ArchiveFile & "'...")
        Dim oProcess As New Process()
        Dim oStartInfo As ProcessStartInfo
        If Password = "" Then
            oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "t -p- " & ArchiveFile)
        Else
            oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "t -p" & Password & " " & ArchiveFile)
        End If
        oStartInfo.CreateNoWindow = True
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        Dim sOutput As String = ""
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            While Not oStreamReader.EndOfStream()
                sOutput = oStreamReader.ReadLine()
                If sOutput.StartsWith("...") Then
                    Dim sPercent = sOutput.Substring(sOutput.LastIndexOf(" ") + 1)
                    Dim sFilename = sOutput.Substring(12)
                    sFilename = sFilename.Substring(0, sFilename.IndexOf(" "))
                    UpdateStatus("Testing archive " & ArchiveFile & ": " & sFilename & " - " & sPercent)
                End If
            End While
            'sOutput = oStreamReader.ReadToEnd()
        End Using
        If sOutput.Contains("All OK") Then
            AppendStatus("Tested OK")
            Return True
        Else
            AppendStatus("Test Failed")
            Return False
        End If

    End Function

    Private Function ExtractArchive(ArchiveFile As String, DestinationDirectory As String) As Boolean
        Return ExtractArchive(ArchiveFile, "", DestinationDirectory)
    End Function

    Private Function ExtractArchive(ArchiveFile As String, Password As String, DestinationDirectory As String) As Boolean
        Dim sOutput As String = ""

        If Not ArchiveFile.Contains(".rar") Then Return False
        AddStatus("Extracting archive '" & ArchiveFile & "'...")
        Using oProcess = New Process()
            Dim oStartInfo As ProcessStartInfo
            If Password = "" Then
                oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "e -y -p- " & ArchiveFile & " " & DestinationDirectory)
            Else
                oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "e -y -p" & Password & " " & ArchiveFile & " " & DestinationDirectory)
            End If
            oStartInfo.CreateNoWindow = True
            oStartInfo.UseShellExecute = False
            oStartInfo.RedirectStandardOutput = True
            oProcess.StartInfo = oStartInfo
            oProcess.Start()

            Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
                While Not oStreamReader.EndOfStream()
                    sOutput = oStreamReader.ReadLine()
                    If sOutput.StartsWith("...") Then
                        Dim sPercent = sOutput.Substring(sOutput.LastIndexOf(" ") + 1)
                        Dim sFilename = sOutput.Substring(12)
                        sFilename = sFilename.Substring(0, sFilename.IndexOf(" "))
                        UpdateStatus("Extracting archive " & ArchiveFile & ": " & sFilename & " - " & sPercent)
                    End If
                End While
                'sOutput = oStreamReader.ReadToEnd()
            End Using
            oProcess.Dispose()
        End Using

        If sOutput.Contains("All OK") Then
            AppendStatus("Extracted OK")
            Return True
        Else
            AppendStatus("Extract failed")
            Return False
        End If
    End Function

    Private Function GetArchiveContents(ArchiveFile As String, Password As String) As String
        If Not ArchiveFile.Contains(".rar") Then Return ""
        
        Dim oProcess As New Process()
        Dim oStartInfo As ProcessStartInfo
        If Password = "" Then
            oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "lb -p- " & ArchiveFile)
        Else
            oStartInfo = New ProcessStartInfo("C:\Program Files\WinRAR\rar.exe", "lb -p" & Password & " " & ArchiveFile)
        End If
        oStartInfo.CreateNoWindow = True
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        Dim sOutput As String = ""
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using
        Return sOutput

    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtFind.Clear()
        txtReplace.Clear()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        txtFind.Text = Clipboard.GetText()
    End Sub


    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        ClearStatus()
        For Each sFilename In Directory.GetFiles(txtPathname.Text)
            Dim sNewFilename = Path.GetDirectoryName(sFilename) & "\" & Path.GetFileName(sFilename).Replace(txtFind.Text, txtReplace.Text)
            If RenameFile(sFilename, sNewFilename) Then
                AddStatus(sNewFilename)
            Else
                AddStatus("Error Renaming:" & sFilename & " - " & LastError)
            End If
        Next
    End Sub

    Private Sub btnNzbget_Click(sender As Object, e As EventArgs) Handles btnNzbget.Click
        Dim sRarFilename As String = ""

        AddStatus("NZBGET: Scanning for new archives...")
        For Each DirectoryName In Directory.GetDirectories("e:\nzbget")
            If DirectoryName.Contains("_") Or DirectoryName.Contains(".#") Then
                If DirectoryName.Contains(".") Then
                    sRarFilename = Path.Combine(DirectoryName, Path.GetFileName(DirectoryName).Split(".")(0)) & ".part01.rar"
                Else
                    sRarFilename = Path.Combine(DirectoryName, Path.GetFileName(DirectoryName)) & ".part01.rar"
                End If
                If File.Exists(sRarFilename) Then
                    Dim sPassword = Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
                    sPassword = sPassword.Replace("REPOST-", "")
                    sPassword = sPassword.Replace("REPOST2-", "")
                    Dim bOK = False
                    If TestArchive(sRarFilename, sPassword) Then
                        bOK = True
                    Else
                        sPassword = "20" & sPassword
                        If TestArchive(sRarFilename, sPassword) Then
                            bOK = True
                        End If
                    End If
                    If bOK Then
                        If ExtractArchive(sRarFilename, sPassword, "e:\downloads") Then
                            Directory.Delete(DirectoryName, True)
                        End If
                    End If
                End If
            End If
        Next
        AddStatus("NZBGET: Complete.")
    End Sub

    Private Function CopyFile(SourceFilename As String, DestFilename As String) As Boolean
        Try
            If DestFilename = "nfo" Then
                MessageBox.Show("nfo file")
            End If
            If File.Exists(DestFilename) Then
                File.Delete(DestFilename)
            End If
            AddStatus("Copying File: " & SourceFilename & " to " & DestFilename)
            File.Copy(SourceFilename, DestFilename)
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

    Private Function CopyFileProgress(SourceFilename As String, DestFilename As String) As Boolean
        Try
            If File.Exists(DestFilename) Then
                File.Delete(DestFilename)
            End If
            AddStatus("Copying File: " & SourceFilename & " to " & DestFilename)
            wc.DownloadFileAsync(New Uri(SourceFilename), DestFilename)
            While wc.IsBusy
                Application.DoEvents()
            End While
            'File.Copy(SourceFilename, DestFilename)
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

    Private Sub DownloadProgress(sender As Object, e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        UpdateStatusPercent(e.ProgressPercentage)
    End Sub


    Private Function MoveFile(SourceFilename As String, DestFilename As String) As Boolean
        Try
            If File.Exists(DestFilename) Then
                File.Delete(DestFilename)
            End If
            If DestFilename = "nfo" Then
                MessageBox.Show("nfo file")
            End If
            AddStatus("Moving File: " & SourceFilename & " to " & DestFilename)
            File.Move(SourceFilename, DestFilename)
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

    Private Function MoveFileProgress(SourceFilename As String, DestFilename As String) As Boolean

        Try
            If File.Exists(DestFilename) Then
                File.Delete(DestFilename)
            End If
            AddStatus("Moving File: " & SourceFilename & " to " & DestFilename)
            wc.DownloadFileAsync(New Uri(SourceFilename), DestFilename)
            While wc.IsBusy
                Application.DoEvents()
            End While
            File.Delete(SourceFilename)
            Return True
        Catch ex As Exception
            LastError = ex.Message
            Return False
        End Try
    End Function

    Private Function CheckDirectory(DirectoryName As String) As Boolean
        If Directory.Exists(DirectoryName) Then
            AddStatus("Directory EXISTS: " & DirectoryName)
            Return True
        End If
        Try
            AddStatus("Create Directory: " & DirectoryName)
            Directory.CreateDirectory(DirectoryName)
            Return True
        Catch ex As Exception
            AddStatus("Error creating Directory: " & ex.Message)
            LastError = ex.Message
            Return False
        End Try

    End Function


    Private Sub btnPurge720p_Click(sender As Object, e As EventArgs) Handles btnPurge720p.Click
        AddStatus("PURGE 720p: Deleting files...")
        For Each sFilename In Directory.GetFiles(txtPathname.Text)
            If sFilename.EndsWith(".mkv") And sFilename.ToLower().Contains("720p") Then
                Try
                    File.Delete(sFilename)
                Catch ex As Exception
                    AddStatus("PURGE 720p: Error deleting " & sFilename)
                End Try
            End If
        Next
        AddStatus("PURGE 720p: Complete.")
    End Sub

    Private Sub MoveFiles()
        AddStatus("MOVEFILES: Moving files...")
        MoveFiles("e:\downloads\TV _ HD", "e:\downloads", "*.mkv")
        AddStatus("MOVEFILES: Complete.")
    End Sub

    Private Sub MoveFiles(SourceDirectory As String, DestinationDirectory As String, Wildcard As String)
        If Not Directory.Exists(SourceDirectory) Then Return
        For Each sFilename In Directory.GetFiles(SourceDirectory, Wildcard)
            Dim sDestFilename = Path.Combine(DestinationDirectory, Path.GetFileName(sFilename))
            If Not IsATVShow(Path.GetFileName(sFilename)) And Not IsAMovie(Path.GetFileName(sFilename)) Then
                sDestFilename = Path.Combine(DestinationDirectory, SourceDirectory.Replace("e:\downloads\TV _ HD\", "") & ".mkv")
            End If
            AddStatus("Moving file: " & sFilename & " to " & sDestFilename)
            'If Not MoveFile(sFilename, sDestFilename) Then
            If Not MoveFileProgress(sFilename, sDestFilename) Then
                AddStatus("Error Moving file: " & sDestFilename & " Error: " & LastError)
                Return
            End If
        Next
        For Each DirectoryName In Directory.GetDirectories(SourceDirectory)
            MoveFiles(DirectoryName, DestinationDirectory, Wildcard)
        Next
        'Try
        '    If SourceDirectory <> "e:\downloads" Then
        '        Directory.Delete(SourceDirectory, True)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Move720pFiles(SourceDirectory As String, DestinationDirectory As String)
        If Not Directory.Exists(SourceDirectory) Then Return
        For Each sFilename In Directory.GetFiles(SourceDirectory, "*720p*.*")
            Dim sDestFilename = Path.Combine(DestinationDirectory, Path.GetFileName(sFilename))
            AddStatus("Moving file: " & sFilename & " to " & sDestFilename)
            If Not MoveFile(sFilename, sDestFilename) Then
                AddStatus("Error Moving file: " & sDestFilename & " Error: " & LastError)
                Return
            End If
        Next
    End Sub
    Private Function RemoveYear(Filename As String) As String
        For i = 2000 To 2018
            If Filename.Contains("." & i & ".") Then
                Return Filename.Replace("." & i, "")
            End If
        Next
        Return Filename
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        btnStart.PerformClick()
        Timer1.Enabled = True

    End Sub

    ' filename.part01.rar
    Private Sub ExtractArchives(FolderName As String, DownloadFolder As String)
        ExtractArchives(FolderName, DownloadFolder, "part01.rar")
        ExtractArchives(FolderName, DownloadFolder, "part001.rar")
    End Sub

    Private Sub ExtractArchives(FolderName As String, DownloadFolder As String, FirstArchive As String)
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim sPassword As String = ""
        Dim bOK = False
        If Not Directory.Exists(FolderName) Then Exit Sub
        AddStatus("Extracting Archives in " & FolderName)
        For Each sRarFilename In Directory.GetFiles(FolderName, "*." & FirstArchive)
            sRarFile = Path.GetFileName(sRarFilename).Replace("." & FirstArchive, "")  ' Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
            sPassword = ""
            If TestArchive(sRarFilename) Then
                bOK = True
            Else
                ' failed, try password as the name of the file
                If sRarFile.StartsWith("unknown") Then
                    sPassword = "paranoid06-does-the-usenet"
                Else
                    ' rar file is a number ex. 19071519
                    sPassword = sRarFile.Replace("REPOST-", "")
                    sPassword = sPassword.Replace("REPOST2-", "")
                End If
                If TestArchive(sRarFilename, sPassword) Then
                    bOK = True
                Else
                    sPassword = "20" & sPassword            ' try with new 20 in front for dmca ones
                    If TestArchive(sRarFilename, sPassword) Then
                        bOK = True
                    Else
                        bOK = False
                    End If
                End If
            End If
            If bOK Then
                If ExtractArchive(sRarFilename, sPassword, DownloadFolder) Then
                    'Try
                    '    If sRarFilename.StartsWith("unknownweb") Then
                    '        MoveFile(sRarFilename, Path.Combine("e:\unknownweb", Path.GetFileName(sRarFilename)))
                    '    ElseIf sRarFilename.StartsWith("unknowntv") Then
                    '        MoveFile(sRarFilename, Path.Combine("e:\unknowntv", Path.GetFileName(sRarFilename)))
                    '    Else
                    '        MoveFile(sRarFilename, Path.Combine("e:\unknown", Path.GetFileName(sRarFilename)))
                    '    End If
                    Kill(Path.Combine(FolderName, sRarFile & "*.*"))
                    '    Kill(Path.Combine(DownloadFolder, sRarFile & ".nfo"))
                    'Catch ex As Exception

                    'End Try
                End If
            End If
        Next
        AddStatus("Complete.")
    End Sub
    ' filename.rar
    Private Sub ExtractArchives2(FolderName As String, DownloadFolder As String)
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim bOK = False

        AddStatus("Extracting Archives in " & FolderName)
        For Each sRarFilename In Directory.GetFiles(FolderName, "*.rar")
            sRarFile = Path.GetFileNameWithoutExtension(sRarFilename)
            If TestArchive(sRarFilename) Then
                bOK = True
            Else
                bOK = False
            End If
            If bOK Then
                If ExtractArchive(sRarFilename, DownloadFolder) Then
                    Try
                        Kill(Path.Combine(FolderName, sRarFile & ".*"))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Next
        AddStatus("Complete.")
    End Sub

    Private Sub GetAllArchivesContents(FolderName As String, FirstArchive As String)
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim sPassword As String = ""
        Dim sArchiveContents As String = ""
        Dim bOK = False

        If Not Directory.Exists(FolderName) Then Exit Sub
        For Each sRarFilename In Directory.GetFiles(FolderName, "*." & FirstArchive)
            sRarFile = Path.GetFileName(sRarFilename).Replace("." & FirstArchive, "")  ' Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
            sPassword = "paranoid06-does-the-usenet"
            sArchiveContents = GetArchiveContents(sRarFilename, sPassword)
            If sArchiveContents <> "" Then
                If sArchiveContents.ToUpper().Contains("1080p") Then
                    CopyFile(sRarFilename, "E:\unknown\" & Path.GetFileName(sRarFilename))
                    For i = 1966 To 1985
                        If sArchiveContents.Contains("." & i & ".") Then
                            AddStatus(sRarFile & vbTab & sArchiveContents)
                        End If
                    Next
                Else
                    MoveFile(sRarFilename, "E:\unknown\" & Path.GetFileName(sRarFilename))
                End If
            End If
        Next

    End Sub
    Private Sub AddToLibrary()
        AddStatus("MOVE: Copying files to library...")
        For Each sFilename In Directory.GetFiles(txtPathname.Text)
            If (sFilename.Contains("1080p") Or sFilename.Contains("BDRip") Or sFilename.Contains("DVDRip")) And sFilename.EndsWith(".mkv") And
                (Not sFilename.ToLower().Contains(".multi.")) And
                (Not sFilename.ToLower().Contains("nordic")) And
                (Not sFilename.ToLower().Contains("dksubs")) And
                (Not sFilename.ToLower().Contains("french")) And
                (Not sFilename.ToLower().Contains("danish")) And
                (Not sFilename.ToLower().Contains("german")) Then

                Dim sDestFolder = ""
                If IsATVShow(sFilename) Then
                    sDestFolder = GetTVFolder(Path.GetFileName(RemoveYear(sFilename)))
                ElseIf IsAMovie(sFilename) Then
                    sDestFolder = GetMovieFolder(Path.GetFileName(sFilename))
                End If
                If sDestFolder <> "" Then
                    If CheckDirectory(sDestFolder) Then
                        Dim sDestFilename = Path.Combine(sDestFolder, Path.GetFileName(IIf(IsATVShow(sFilename), RemoveYear(sFilename), sFilename)))
                        'AddStatus("Moving File: " & sFilename & " to " & sDestFilename)
                        If File.Exists(sDestFilename) Then
                            AddStatus("File exists: " & sDestFilename)
                            Try
                                Kill(sFilename)
                            Catch ex As Exception

                            End Try
                        Else
                            'If Not CopyFile(sFilename, sDestFilename) Then
                            If Not CopyFileProgress(sFilename, sDestFilename) Then
                                AddStatus("ERROR Moving File: " & LastError)
                            End If
                            ' add to backup
                            If Not MoveFile(sFilename, Path.Combine("e:\backup\", Path.GetFileName(sDestFilename))) Then
                                AddStatus("ERROR Moving File: " & LastError)
                            End If

                        End If
                    Else
                        AddStatus("ERROR Creating Directory '" & sDestFolder & "' : " & LastError)
                    End If
                Else
                    AddStatus("File not added '" & sFilename & "'")
                End If
            End If
        Next
        AddStatus("MOVE: Complete.")
    End Sub

    Private Sub CheckForDuplicates()
        ClearStatus()
        For Each sTVFolder In TVFolders
            For Each sShowFolder In Directory.EnumerateDirectories(sTVFolder)
                For Each sSeasonFolder In Directory.EnumerateDirectories(sShowFolder)
                    Dim SeasonNumber As Integer = sSeasonFolder.Substring(sSeasonFolder.Length - 2, 2)
                    Dim EpisodeNumber As Integer = 1
                    For Each EpisodeFile In Directory.GetFiles(sSeasonFolder)
                        If EpisodeFile.Contains(GetEpisodeNumber(SeasonNumber, EpisodeNumber)) Then
                            EpisodeNumber = EpisodeNumber + 1
                        ElseIf EpisodeFile.Contains(GetEpisodeNumberCombo(SeasonNumber, EpisodeNumber)) Then
                            EpisodeNumber = EpisodeNumber + 2
                        ElseIf EpisodeFile.Contains(GetEpisodeNumber(SeasonNumber, EpisodeNumber - 1)) Then
                            AddStatus("DUPLICATE: " + EpisodeFile)
                        Else
                            AddStatus("MISSING: " + sSeasonFolder + " Episode " & EpisodeNumber)
                            EpisodeNumber = EpisodeNumber + 1
                        End If
                    Next
                Next
            Next
        Next
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        CheckForDuplicates()
    End Sub

    ' S03E07
    Private Function GetEpisodeNumber(SeasonNumber As Integer, EpisodeNumber As Integer) As String
        Return ".S" & String.Format("{0:00}", SeasonNumber) & "E" & String.Format("{0:00}", EpisodeNumber) & "."
    End Function

    ' S03E01E02
    Private Function GetEpisodeNumberCombo(SeasonNumber As Integer, EpisodeNumber As Integer) As String
        Return ".S" & String.Format("{0:00}", SeasonNumber) & "E" & String.Format("{0:00}", EpisodeNumber) & "E" & String.Format("{0:00}", EpisodeNumber + 1) & "."
    End Function



    Private Sub AddToDatabase()
        'ClearStatus()
        'For Each sMainMovieFolder In MovieFolders
        '    For Each sYearFolder In Directory.GetDirectories(sMainMovieFolder)
        '        For Each sMovie In Directory.GetDirectories(sYearFolder)
        '            Dim sMovieFolder = Path.GetFileName(sMovie)
        '            Dim sTitle = FixMovieTitle(sMovieFolder.Substring(0, sMovieFolder.IndexOf("[") - 1))
        '            Dim sYear = sMovieFolder.Substring(sMovieFolder.IndexOf("[") + 1, 4)
        '            If Not MovieExistsInColdStorage(sTitle, sYear) Then
        '                AddStatus("Adding: " & Path.GetFileName(sMovie))
        '                AddToColdStorage(sTitle, sYear)
        '            End If
        '        Next
        '    Next
        'Next
    End Sub

    Private Function FixMovieTitle(sTitle As String) As String
        If sTitle.StartsWith("The ") Then
            sTitle = sTitle.Substring(4)
            sTitle = sTitle & ", The"
        End If
        If sTitle.StartsWith("A ") Then
            sTitle = sTitle.Substring(2)
            sTitle = sTitle & ", A"
        End If
        Return sTitle
    End Function

    ' check for DMCA files ########.mkv in path
    '       18090102.mkv
    '       move to e:\list
    ' check for nfo files in n:\Newsleecher\alt.binaries.hdtv.x264
    '       18090102 Bull.2016.S03E06.Fool.Me.Twice.1080p.AMZN.WEB-DL.DD+5.1.H.264-AJP69.nfo
    '       move to e:\list
    ' match up first 8 characters, rename mkv file and remove leading 9 characters (with space)
    Private Sub ProcessDMCA()
        If Not Directory.Exists(txtPathname.Text) Then Return
        ' move e:\downloads\########.mkv to e:\list
        For Each sFilePath In Directory.GetFiles("e:\downloads", "*.mkv")
            Dim sFilename = Path.GetFileName(sFilePath)
            If sFilename.Length = 12 Then
                Dim sDestFilename = Path.Combine("e:\list", sFilename)
                MoveFile(sFilePath, sDestFilename)
            End If
        Next
        ' move n:\Newsleecher\alt.binaries.hdtv.x264\*.nfo to e:\list
        ' For Each sFilePath In Directory.GetFiles(NewsLeecherPath & "\alt.binaries.hdtv.x264", "*.nfo")
        For Each sFilePath In Directory.GetFiles(NewsLeecherPath, "*.nfo")
            Dim sFilename = Path.GetFileName(sFilePath)
            Dim n = Val(sFilename.Substring(0, 8))
            If n <> 0 Then
                Dim sDestFilename = Path.Combine("e:\list", sFilename)
                MoveFile(sFilePath, sDestFilename)
            End If
        Next
        ' move n:\Newsleecher\alt.binaries.tv\*.nfo to e:\list
        'For Each sFilePath In Directory.GetFiles(NewsLeecherPath & "\alt.binaries.tv", "*.nfo")
        For Each sFilePath In Directory.GetFiles(NewsLeecherPath, "*.nfo")
            Dim sFilename = Path.GetFileName(sFilePath)
            Dim n = Val(sFilename.Substring(0, 8))
            If n <> 0 Then
                Dim sDestFilename = Path.Combine("e:\list", sFilename)
                MoveFile(sFilePath, sDestFilename)
            End If
        Next
        ' match numbers and rename files and move to e:\downloads
        ' move n:\Newsleecher\alt.binaries.hdtv.x264\*.nfo to e:\list
        For Each sFilePath In Directory.GetFiles("e:\list", "*.nfo")
            Dim sFilename = Path.GetFileName(sFilePath)
            Dim n = Val(sFilename.Substring(0, 8))
            If n <> 0 And sFilename.Length > 12 Then        ' make sure first 8 are a number and the nfo file is more than just the number.nfo
                Dim sMKVFilename = Path.Combine("e:\list", n.ToString() & ".mkv")
                If File.Exists(sMKVFilename) Then
                    ' 18090102 Bull.2016.S03E06.Fool.Me.Twice.1080p.AMZN.WEB-DL.DD+5.1.H.264-AJP69.mkv
                    Dim sProperMKVFilename = Path.Combine("e:\list", Path.GetFileName(sFilename).Substring(9)).Replace(".nfo", ".mkv")
                    ' rename from 18090102.mkv to Bull.2016.S03E06.Fool.Me.Twice.1080p.AMZN.WEB-DL.DD+5.1.H.264-AJP69.mkv
                    RenameFile(sMKVFilename, sProperMKVFilename)
                    ' move e:\list\Bull.2016.S03E06.Fool.Me.Twice.1080p.AMZN.WEB-DL.DD+5.1.H.264-AJP69.mkv to e:\downloads
                    MoveFile(sProperMKVFilename, Path.Combine(txtPathname.Text, Path.GetFileName(sProperMKVFilename)))
                End If
            End If
        Next

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        wc = New WebClient()
        Console.WriteLine(DateTime.Now.ToString())

        'Dim obj As Object = Nothing

        'If (obj IsNot Nothing) AndAlso obj.ToString() = "hello" Then
        '    MsgBox("test")
        'End If
    End Sub

    Private Sub wc_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted

    End Sub

    Private Sub BtnMoveUnknown_Click(sender As Object, e As EventArgs) Handles btnMoveUnknown.Click
        ListBox1.Items.Clear()
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim sPassword As String = ""
        Dim sArchiveContents As String = ""
        Dim bOK = False

        For Each sRarFilename In Directory.GetFiles("E:\NewsLeecher\alt.binaries.hdtv.german", "unknown*.part01.rar")
            MoveFile(sRarFilename, "E:\unknown\" & Path.GetFileName(sRarFilename))
        Next
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        If e.Control And e.KeyCode = Keys.KeyCode.C Then
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            Clipboard.SetText(ListBox1.SelectedItem.ToString())
        End If
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Clipboard.SetText(ListBox1.SelectedItem.ToString())
    End Sub

    Private Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        'Timer1.Enabled = False
        ListBox1.Items.Clear()
        'btnNzbget.PerformClick()
        ExtractArchives(NewsLeecherPath, "e:\downloads")
        'ExtractArchives(NewsLeecherPath & "\alt.binaries.tv", "e:\downloads")
        'ExtractArchives(NewsLeecherPath & "\alt.binaries.hdtv", "e:\downloads")
        'ExtractArchives(NewsLeecherPath & "\alt.binaries.hdtv.x264", "e:\downloads")
        'ExtractArchives(NewsLeecherPath & "\alt.binaries.hdtv.german", "e:\downloads")
        ProcessDMCA()
        'MoveFiles()
        AddStatus("Done.")
    End Sub

    Private Sub btnListUnknown_Click(sender As Object, e As EventArgs) Handles btnListUnknown.Click
        Dim FolderName As String = "e:\unknown"
        Dim FirstArchive As String = "part01.rar"
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim sPassword As String = ""
        Dim sArchiveContents As String = ""
        Dim bOK = False

        For Each sRarFilename In Directory.GetFiles(FolderName, "*." & FirstArchive)
            sRarFile = Path.GetFileName(sRarFilename).Replace("." & FirstArchive, "")  ' Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
            sPassword = "paranoid06-does-the-usenet"
            sArchiveContents = GetArchiveContents(sRarFilename, sPassword)
            'If sArchiveContents <> "" Then
            If sArchiveContents.ToUpper().Contains("1080P") Then

                For i = 1966 To 1985
                    If sArchiveContents.Contains("." & i & ".") Then
                        AddStatus(sRarFile & vbTab & vbTab & sArchiveContents)
                    End If
                Next

                'End If
            End If
        Next

    End Sub

    Private Sub btnUnkownToText_Click(sender As Object, e As EventArgs) Handles btnUnkownToText.Click

        Dim FirstArchive As String = "part01.rar"
        Dim sRarFilename As String = ""
        Dim sRarFile As String = ""
        Dim sPassword As String = ""
        Dim sArchiveContents As String = ""
        Dim bOK = False
        File.Delete("e:\unknown.csv")
        Dim f = File.CreateText("e:\unknown.csv")
        For Each sRarFilename In Directory.GetFiles("e:\unknown", "*." & FirstArchive)
            sRarFile = Path.GetFileName(sRarFilename).Replace("." & FirstArchive, "")  ' Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
            sPassword = "paranoid06-does-the-usenet"
            sArchiveContents = GetArchiveContents(sRarFilename, sPassword)
            'If sArchiveContents <> "" Then
            If sArchiveContents.ToUpper().Contains("1080P") Then

                'For i = 1966 To 1985
                'If sArchiveContents.Contains("." & i & ".") Then
                AddStatus(sRarFile & vbTab & GetMovieYear(sArchiveContents) & vbTab & sArchiveContents)
                f.Write(sRarFile & vbTab & GetMovieYear(sArchiveContents) & vbTab & sArchiveContents)
                'End If
                'Next

                'End If
            End If
        Next
        For Each sRarFilename In Directory.GetFiles("e:\unknownweb", "*." & FirstArchive)
            sRarFile = Path.GetFileName(sRarFilename).Replace("." & FirstArchive, "")  ' Path.GetFileNameWithoutExtension(sRarFilename).Split(".")(0)
            sPassword = "paranoid06-does-the-usenet"
            sArchiveContents = GetArchiveContents(sRarFilename, sPassword)
            'If sArchiveContents <> "" Then
            If sArchiveContents.ToUpper().Contains("1080P") Then

                'For i = 1966 To 1985
                'If sArchiveContents.Contains("." & i & ".") Then
                AddStatus(sRarFile & vbTab & GetMovieYear(sArchiveContents) & vbTab & sArchiveContents)
                f.Write(sRarFile & vbTab & GetMovieYear(sArchiveContents) & vbTab & sArchiveContents)
                'End If
                'Next

                'End If
            End If
        Next
        f.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        File.Delete("e:\movielist.csv")
        Dim f = File.CreateText("e:\movielist.csv")
        For iYear = 1915 To DateTime.Today.Year
            Dim sMovieFolder = FindMovieFolder(iYear)
            For Each sFilename In Directory.GetFiles(sMovieFolder, "*.mkv", SearchOption.AllDirectories)
                Dim sMovieName = Path.GetFileName(sFilename)
                sMovieName = sMovieName.Substring(0, sMovieName.IndexOf("." & iYear & "."))
                sMovieName = sMovieName.Replace(".", " ")
                AddStatus(sMovieName & vbTab & iYear & vbTab & Path.GetFileName(sFilename))
                f.WriteLine(sMovieName & vbTab & iYear & vbTab & Path.GetFileName(sFilename))
            Next
        Next
        f.Close()

    End Sub
End Class

