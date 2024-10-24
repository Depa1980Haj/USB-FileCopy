Imports System.IO
Imports System.Management
Imports System.Configuration
Imports System.Threading.Tasks
Imports System.Windows.Forms
Public Class Main
    Private WithEvents watcher As ManagementEventWatcher
    Private targetDirectory As String
    Dim bolCheck As Boolean = False
    Private trayIcon As NotifyIcon
    Private trayMenu As ContextMenu




    ' Füge den Fortschrittsbalken als Mitglied der Form-Klasse hinzu
    Public Sub New()
        ' Initialisiere die Windows Form-Komponenten (Fortschrittsbalken etc.)
        InitializeComponent()
        LabelBol.Text = "Aktiv"
        Me.txtPfad.Text = My.Settings.Pfad
        Me.txtUSB.Text = My.Settings.USBName
        ' Lese den Zielpfad aus den App-Einstellungen
        targetDirectory = ConfigurationManager.AppSettings("TargetDirectory")
        If String.IsNullOrEmpty(targetDirectory) Then
            targetDirectory = "C:\tmp"
        End If

        ' Überwache USB-Geräte
        Dim query As New WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2")
        watcher = New ManagementEventWatcher(query)
        watcher.Start()

        ' Zeige den aktuellen Zielpfad an
        Console.WriteLine("Aktueller Zielpfad: " & targetDirectory)
    End Sub

    ' Wenn ein USB-Gerät erkannt wird
    Private Sub watcher_EventArrived(sender As Object, e As EventArrivedEventArgs) Handles watcher.EventArrived
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            If drive.DriveType = DriveType.Removable AndAlso drive.IsReady Then
                ' Überprüfe den Namen des USB-Sticks
                If drive.VolumeLabel = "Test" And bolCheck = False Then
                    bolCheck = True
                    Console.WriteLine("USB-Stick erkannt: " & drive.Name)

                    AskForSubdirectoryAndCopyFiles(drive.Name)
                    LabelBol.Text = "Inaktiv"
                End If
            End If
        Next
    End Sub

    ' Fordere den Benutzer auf, ein Unterverzeichnis einzugeben, und kopiere dann die Dateien
    Private Sub AskForSubdirectoryAndCopyFiles(usbPath As String)
        Try
            Dim subDir As String
            ' Frage den Benutzer nach einem Unterverzeichnis
            If Len(Me.txtUnterverzeichnis.Text) > 0 Then

                subDir = Me.txtUnterverzeichnis.Text
            Else
                subDir = InputBox("Bitte geben Sie den Namen des Unterverzeichnisses ein:", "Unterverzeichnis wählen", "NeuesUnterverzeichnis")
                'Me.txtUnterverzeichnis.Text = subDir
            End If
            ' Erstelle den vollständigen Zielpfad mit Unterverzeichnis
            Dim destDir As String = Path.Combine(targetDirectory, subDir)

            ' Stelle sicher, dass das Zielverzeichnis existiert
            If Not Directory.Exists(destDir) Then
                Directory.CreateDirectory(destDir)
            End If

            ' Überprüfe freien Speicherplatz vor dem Kopieren
            If Not HasEnoughFreeSpace(destDir, usbPath) Then
                MessageBox.Show("Nicht genügend freier Speicherplatz auf dem Ziellaufwerk!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Kopiere die Dateien und zeige den Fortschrittsbalken an
            CopyFilesFromUSBWithProgress(usbPath, destDir)

        Catch ex As Exception
            Console.WriteLine("Fehler beim Kopieren: " & ex.Message)
        End Try
    End Sub

    ' Prüft, ob genügend Speicherplatz vorhanden ist
    Private Function HasEnoughFreeSpace(destDir As String, sourceDir As String) As Boolean
        Try
            Dim destDrive As DriveInfo = New DriveInfo(Path.GetPathRoot(destDir))
            Dim totalSize As Long = Directory.GetFiles(sourceDir).Sum(Function(f) New FileInfo(f).Length)

            ' Prüfe, ob genügend freier Speicherplatz vorhanden ist
            If destDrive.AvailableFreeSpace >= totalSize Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("Fehler bei der Speicherplatzprüfung: " & ex.Message)
            Return False
        End Try
    End Function

    ' Kopiert Dateien vom USB-Stick in das Zielverzeichnis und zeigt den Fortschrittsbalken


    Private Sub CopyFilesFromUSBWithProgress(usbPath As String, destDir As String)
        Try


            ' Starte den Kopiervorgang in einem separaten Task, um das UI nicht zu blockieren
            Task.Run(Sub()
                         ' Zähle die Anzahl der Dateien und die Gesamtgröße
                         Dim totalFiles As Integer = 0
                         Dim totalSize As Long = 0

                         ' Rekursives Kopieren aller Dateien und Verzeichnisse
                         CopyDirectoryRecursively(usbPath, destDir, totalFiles, totalSize)

                         ' Ausgabe der Anzahl kopierter Dateien und der Gesamtgröße in GB
                         Invoke(Sub()
                                    MessageBox.Show("Anzahl kopierter Dateien: " & totalFiles & vbCrLf &
                                                    "Gesamtgröße: " & (totalSize / (1024 * 1024 * 1024)).ToString("F2") & " GB",
                                                    "Kopieren abgeschlossen")
                                End Sub)
                     End Sub)

        Catch ex As Exception
            MessageBox.Show("Fehler beim Kopieren: " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub CopyDirectoryRecursively(sourceDir As String, destDir As String, ByRef fileCount As Integer, ByRef totalSize As Long)
        Try
            ' Erstelle das Zielverzeichnis, wenn es nicht existiert
            If Not Directory.Exists(destDir) Then
                Directory.CreateDirectory(destDir)
            End If

            ' Kopiere alle Dateien im aktuellen Verzeichnis
            For Each file As String In Directory.GetFiles(sourceDir)
                Try
                    Dim destFile As String = Path.Combine(destDir, Path.GetFileName(file))

                    ' Kopiere die Datei und überschreibe, wenn sie bereits existiert
                    System.IO.File.Copy(file, destFile, True)

                    ' Statistik sammeln
                    fileCount += 1
                    totalSize += New FileInfo(file).Length

                Catch ex As UnauthorizedAccessException
                    ' Bei Zugriffsverweigerung die Datei überspringen
                    Continue For
                Catch ex As Exception
                    ' Bei anderen Fehlern ggf. informieren und fortfahren
                    MessageBox.Show("Fehler beim Kopieren der Datei: " & file & vbCrLf & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Continue For
                End Try
            Next

            ' Rekursion für alle Unterverzeichnisse
            For Each dir As String In Directory.GetDirectories(sourceDir)
                Try
                    Dim destSubDir As String = Path.Combine(destDir, Path.GetFileName(dir))
                    CopyDirectoryRecursively(dir, destSubDir, fileCount, totalSize)

                Catch ex As UnauthorizedAccessException
                    ' Bei Zugriffsverweigerung das Verzeichnis überspringen
                    Continue For
                Catch ex As Exception
                    ' Bei anderen Fehlern ggf. informieren und fortfahren
                    MessageBox.Show("Fehler beim Zugriff auf das Verzeichnis: " & dir & vbCrLf & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Continue For
                End Try
            Next

        Catch ex As UnauthorizedAccessException
            ' Bei Zugriffsverweigerung das Verzeichnis überspringen
            Return
        Catch ex As Exception
            ' Bei anderen Fehlern ggf. informieren und fortfahren
            MessageBox.Show("Fehler beim Zugriff auf das Verzeichnis: " & sourceDir & vbCrLf & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        bolCheck = False
        LabelBol.Text = "Aktiv"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub txtPfad_DoubleClick(sender As Object, e As EventArgs) Handles txtPfad.DoubleClick
        Dim folderDialog As New FolderBrowserDialog()

        ' Optional: Setze den aktuellen Ordner als Startordner
        folderDialog.SelectedPath = "C:\"

        ' Zeige den Dialog an und prüfe, ob der Benutzer auf OK geklickt hat
        If folderDialog.ShowDialog() = DialogResult.OK Then
            ' Zeige den ausgewählten Ordner in der TextBox an
            txtPfad.Text = folderDialog.SelectedPath
            My.Settings.Pfad = Me.txtPfad.Text
        End If
    End Sub



    Private Sub txtUSB_LostFocus(sender As Object, e As EventArgs) Handles txtUSB.LostFocus
        If Len(Me.txtUSB.Text) > 0 Then
            My.Settings.USBName = Me.txtUSB.Text
        End If
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        trayMenu = New ContextMenu()
        trayMenu.MenuItems.Add("Öffnen", AddressOf Me.ShowForm)
        trayMenu.MenuItems.Add("Beenden", AddressOf Me.ExitApp)

        ' Erstelle das Tray Icon
        trayIcon = New NotifyIcon()
        trayIcon.Text = "USB File Copy"
        trayIcon.Icon = My.Resources.USB_Icon ' Hier kannst du ein Icon (.ico) hinzufügen

        ' Verknüpfe das Menü und zeige es im System Tray an
        trayIcon.ContextMenu = trayMenu
        trayIcon.Visible = True

        ' Hinzufügen des Klick-Ereignisses für das Tray-Icon
        AddHandler trayIcon.DoubleClick, AddressOf Me.ShowForm
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            trayIcon.Visible = True
        End If
    End Sub

    ' Zeige das Formular wieder und verstecke das Tray Icon
    Private Sub ShowForm(sender As Object, e As EventArgs)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        trayIcon.Visible = False
    End Sub

    ' Beende die Anwendung über das Tray-Menü
    Private Sub ExitApp(sender As Object, e As EventArgs)
        trayIcon.Visible = False
        Application.Exit()
    End Sub
End Class