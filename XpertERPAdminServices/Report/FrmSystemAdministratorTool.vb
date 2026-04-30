Imports System.Management
Imports common
Public Class FrmSystemAdministratorTool
#Region "Variables"
    Const colUserName As String = "colUserName"
    Const colPname As String = "colPname"
    Const colProcID As String = "colProcID"
    Const colProcper As String = "colProcper"
    Const colMem As String = "colMem"
    Const colstatus As String = "colstatus"
#End Region
    Private cpuCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total")
    Private ramCounter As New PerformanceCounter("Memory", "Available MBytes")
    Private cpuPrev As Single = 0
    Private Sub FrmSystemAdministratorTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBlankGrid()
    End Sub
    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoUName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUName = New GridViewTextBoxColumn()
        repoUName.FormatString = ""
        repoUName.HeaderText = "User Name"
        repoUName.Name = colUserName
        repoUName.Width = 200
        repoUName.ReadOnly = True
        repoUName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoUName)
        Dim repoPName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPName = New GridViewTextBoxColumn()
        repoPName.FormatString = ""
        repoPName.HeaderText = "Process Name"
        repoPName.Name = colPname
        repoPName.Width = 350
        repoPName.ReadOnly = True
        repoPName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoPName)

        Dim repoProcID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProcID.FormatString = ""
        repoProcID.HeaderText = "Process ID"
        repoProcID.Name = colProcID
        repoProcID.IsVisible = True
        repoProcID.ReadOnly = True
        repoProcID.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoProcID)

        Dim repoProcper As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProcper.FormatString = ""
        repoProcper.HeaderText = "Process Percentage"
        repoProcper.Name = colProcper
        repoProcper.IsVisible = True
        repoProcper.ReadOnly = True
        repoProcper.Width = 200
        repoProcper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoProcper)

        Dim repoMem As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMem.FormatString = ""
        repoMem.HeaderText = "Memory Uses in MB"
        repoMem.Name = colMem
        repoMem.IsVisible = True
        repoMem.ReadOnly = True
        repoMem.Width = 200
        repoMem.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMem)
        Dim repoStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = colstatus
        repoStatus.IsVisible = True
        repoStatus.ReadOnly = True
        repoStatus.Width = 200
        repoStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoStatus)

        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
        gv1.BestFitColumns()
        gv1.Rows.AddNew()
    End Sub
    Private Sub LoadProcesses()
        LoadBlankGrid()
        Dim rowCount As Integer = 0
        Dim totalCupPer As Double = 0
        Dim totalramMB As Double = 0

        For Each proc In Process.GetProcesses()
            Try
                If clsCommon.CompairString(clsCommon.myCstr(proc.ProcessName), "ERP") = CompairStringResult.Equal Then
                    Dim cpu As Single = cpuCounter.NextValue
                    totalCupPer += clsCommon.myCdbl((cpuPrev + cpu) / 2)
                    Dim cpuper As String = $"{(cpuPrev + cpu) / 2:F1}%"

                    totalramMB += clsCommon.myCdbl((proc.WorkingSet64 / 1024 / 1024))
                    Dim ramMB As Decimal = clsCommon.myRoundOFF((proc.WorkingSet64 / 1024 / 1024), 2, 4)
                    Dim status As String = IIf(proc.Responding, "Running", "Hung")
                    gv1.Rows(rowCount).Cells(colUserName).Value = clsCommon.myCstr(ProcessHelpers.GetProcessUser(proc.Id))
                    gv1.Rows(rowCount).Cells(colPname).Value = clsCommon.myCstr(proc.ProcessName)
                    gv1.Rows(rowCount).Cells(colProcID).Value = clsCommon.myCstr(proc.Id)
                    gv1.Rows(rowCount).Cells(colProcper).Value = clsCommon.myCstr(cpuper)
                    If ramMB >= 1024 Then
                        gv1.Rows(rowCount).Cells(colMem).Value = clsCommon.myCdbl(ramMB)
                        gv1.Rows(rowCount).Cells(colMem).Style.BackColor = Color.Red
                    Else
                        gv1.Rows(rowCount).Cells(colMem).Value = clsCommon.myCdbl(ramMB)
                        gv1.Rows(rowCount).Cells(colMem).Style.BackColor = Color.White
                    End If
                    gv1.Rows(rowCount).Cells(colstatus).Value = clsCommon.myCstr(status)
                    gv1.Rows.AddNew()
                    rowCount += 1
                End If

            Catch ex As Exception
                ' Skip inaccessible processes
            End Try
        Next
        gv1.Rows(rowCount).Cells(colPname).Value = "Total "
        gv1.Rows(rowCount).Cells(colProcper).Value = clsCommon.myCstr(clsCommon.myRoundOFF(totalCupPer, 2, 4)) & " %"
        gv1.Rows(rowCount).Cells(colMem).Value = clsCommon.myCdbl(clsCommon.myRoundOFF(totalramMB, 2, 4))
    End Sub
    'Public Function GetProcessUser(procId As Integer) As String
    '    Try
    '        Dim query As New ObjectQuery("SELECT * FROM Win32_Process WHERE ProcessId=" & procId)
    '        Using searcher As New ManagementObjectSearcher(query)
    '            For Each proc As ManagementObject In searcher.Get()
    '                Dim args() As Object = {Nothing, Nothing}
    '                proc.InvokeMethod("GetOwner", args)
    '                Dim owner As String = TryCast(args(0), String)
    '                Dim domain As String = TryCast(args(1), String)

    '                If Not String.IsNullOrEmpty(owner) Then
    '                    Return If(Not String.IsNullOrEmpty(domain), domain & "\" & owner, owner)
    '                End If


    '            Next
    '        End Using
    '    Catch
    '    End Try
    '    Return "N/A"
    'End Function
    'Private Sub UpdateResources()
    '    ' CPU (smooth avg)
    '    Dim cpu As Single = cpuCounter.NextValue
    '    lblCPU.Text = $"{(cpuPrev + cpu) / 2:F1}%"
    '    pbCPU.Value = Math.Min(100, CInt((cpuPrev + cpu) / 2))
    '    cpuPrev = cpu

    '    ' RAM
    '    Dim availMB As Single = ramCounter.NextValue
    '    Dim totalGB As Double = My.Computer.Info.TotalPhysicalMemory / 1024 / 1024 / 1024
    '    Dim usedPct As Single = (100 * (totalGB * 1024 - availMB)) / (totalGB * 1024)
    '    lblRAM.Text = $"{usedPct:F1}% ({(totalGB * 1024 - availMB):F0}/{totalGB * 1024:F0} MB)"
    '    pbRAM.Value = Math.Min(100, CInt(usedPct))
    'End Sub

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    UpdateResources()
    'End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefersh.Click
        LoadProcesses()
    End Sub


    Private Sub btnKill_Click(sender As Object, e As EventArgs) Handles btnKill.Click
        If gv1.SelectedRows.Count = 0 Then
            Dim procId As Integer = CInt(gv1.CurrentRow.Cells(colProcID).Value)
            Try
                Dim proc As Process = Process.GetProcessById(procId)
                If MessageBox.Show("Kill " & proc.ProcessName & "?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    proc.Kill()
                    LoadProcesses()
                End If
            Catch ex As Exception
                MessageBox.Show("Cannot kill: " & ex.Message)
            End Try
        End If

    End Sub
End Class