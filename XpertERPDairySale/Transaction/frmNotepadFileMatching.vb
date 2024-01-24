''created By Richa Agarwal 
Imports common
Imports System.Data.SqlClient
Public Class frmNotepadFileMatching
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim colTSLine As String = "colTSLine"
    Dim colTSDetail As String = "colTSDetail"


    Dim colTSISNo As String = "colTSISNo"
    Dim colTSIPSNo As String = "colTSIPSNo"
    Dim colTSIZone As String = "colTSIZone"
    Dim colTSIRoute As String = "colTSIRoute"
    Dim colTSIBoothID As String = "colTSIBoothID"
    Dim colTSIType As String = "colTSIType"
    Dim colTSICR As String = "colTSICR"
    Dim colTSICD As String = "colTSICD"
    Dim colTSISO As String = "colTSISO"
    Dim colTSICash As String = "colTSICash"
    Dim colTSIAmount As String = "colTSIAmount"



    Dim colTSMISNo As String = "colTSMISNo"
    Dim colTSMIPSNoMilkososft As String = "colTSMIPSNoMilkososft"
    Dim colTSMIPSNoTecxpert As String = "colTSMIPSNoTecxpert"
    Dim colTSMIBoothID As String = "colTSMIBoothID"
    Dim colTSMIType As String = "colTSMIType"
    Dim colTSMICR As String = "colTSMICR"
    Dim colTSMICD As String = "colTSMICD"
    Dim colTSMISO As String = "colTSMISO"
    Dim colTSMICash As String = "colTSMICash"
    Dim colTSMIAmount As String = "colTSMIAmount"
















    Dim colGPSNo As String = "colGPSNo"
    Dim colGPFile As String = "colGPFile"
    Dim colGPFileSNo As String = "colGPFileSNo"
    Dim colGPDetail As String = "colTSDetail"



    Dim colGPISNo As String = "colGPISNo"
    Dim colGPIPSNo As String = "colGPIPSNo"
    Dim colGPIDate As String = "colGPIDate"
    Dim colGPIShift As String = "colGPIShift"
    Dim colGPIRoute As String = "colGPIRoute"
    Dim colGPIBoothID As String = "colGPIBoothID"
    Dim colGPIType As String = "colGPIType"
    Dim colGPICash As String = "colGPICash"
#End Region
    Private Sub frmMCCMaterialSaleUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage2
        RadPageView2.SelectedPage = RadPageViewPage1
        RadPageView3.SelectedPage = RadPageViewPage7

        Dim coll As New Dictionary(Of String, String)
        coll.Add("Company", "VARCHAR(20)")
        coll.Add("Booth", "VARCHAR(20)")
        coll.Add("Type", "VARCHAR(20)")
        coll.Add("CR", "decimal(18,2)")
        coll.Add("CD", "decimal(18,2)")
        coll.Add("SO", "decimal(18,2)")
        coll.Add("Cash", "decimal(18,2)")
        coll.Add("Amount", "decimal(18,2)")
        coll.Add("PNO", "decimal(18,2)")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_Notepad_Read_Mismatch", coll)

        coll = New Dictionary(Of String, String)
        coll.Add("Company", "VARCHAR(20)")
        coll.Add("Booth", "VARCHAR(20)")
        coll.Add("Type", "VARCHAR(20)")
        coll.Add("Date", "VARCHAR(20)")
        coll.Add("Shift", "VARCHAR(20)")
        coll.Add("Route", "VARCHAR(20)")
        coll.Add("Cash", "decimal(18,2)")
        coll.Add("PNO", "decimal(18,2)")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_Notepad_Read_Mismatch_GP", coll)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        OpenFile(txtBrowse)
    End Sub
    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        OpenFile(txtBrowseTecxpert)
    End Sub
    Sub OpenFile(ByVal txt As common.Controls.MyTextBox)
        OpenFileDialog.FileName = ""
        OpenFileDialog.Filter = "Text Files (*.TXT)|*.TXT"
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txt.Text = OpenFileDialog.FileName
            Me.Tag = OpenFileDialog.SafeFileName
        Else
            Exit Sub
        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        FileGo(txtBrowse, gvTS)
    End Sub
    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        FileGo(txtBrowseTecxpert, gvTSTecxpert)
    End Sub
    Sub FileGo(ByVal txt As common.Controls.MyTextBox, ByVal gv As RadGridView)
        Try
            If clsCommon.myLen(txt.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If txt.Text.Contains(".TXT") = True Or txt.Text.Contains(".txt") = True Or txt.Text.Contains(".Txt") Then
                LoadBlankGridTS(gv)
                Dim path As String = txt.Text
                Try
                    Dim lineCount = System.IO.File.ReadAllLines(path).Length
                    Dim sr As System.IO.StreamReader = New System.IO.StreamReader(path)
                    Dim line As String = ""
                    clsCommon.ProgressBarPercentShow()
                    Do While sr.Peek() >= 0
                        line = sr.ReadLine()
                        gv.Rows.AddNew()
                        clsCommon.ProgressBarPercentUpdate(gv.Rows.Count * 100 / lineCount, "Loading selected File " + clsCommon.myCstr(gv.Rows.Count) + "/" + clsCommon.myCstr(lineCount))

                        gv.Rows(gv.Rows.Count - 1).Cells(colTSLine).Value = gv.Rows.Count
                        gv.Rows(gv.Rows.Count - 1).Cells(colTSDetail).Value = line
                        gv.Refresh()
                    Loop
                    clsCommon.ProgressBarPercentHide()
                    sr.Close()
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error in File Reading " + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridTS(ByVal gv As RadGridView)
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colTSLine
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Details"
        repoLineNo.Name = colTSDetail
        repoLineNo.Width = 1000
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoLineNo)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        ReadTSFile(txtBrowse, gvTS, gvTSItem, False)
    End Sub
    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        ReadTSFile(txtBrowseTecxpert, gvTSTecxpert, gvTSItemTecxpert, True)
    End Sub
    Sub ReadTSFile(ByVal txt As common.Controls.MyTextBox, ByVal gv As RadGridView, ByVal gvItem As RadGridView, ByVal isTecxpertGatePass As Boolean)
        Try
            If clsCommon.myLen(txt.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If gv.Rows.Count > 0 Then
                LoadBlankGridTSItems(gvItem)
                Dim ii As Integer = 0
                Dim arrExcept As New List(Of String)
                Try
                    Dim qry As String = "select Name from TSPL_Noetpad_Read_Except"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            arrExcept.Add(clsCommon.myCstr(dr("Name")))
                        Next
                    End If

                    Dim strZone As String = Nothing
                    Dim strRoute As String = Nothing
                    Dim strBoothID As String = Nothing
                    Dim strZone1 As String = Nothing
                    Dim strRoute1 As String = Nothing
                    Dim strBoothID1 As String = Nothing
                    Dim isPreviousLineDash As Boolean = False
                    Dim isPreviousToPReviousLineDash As Boolean = False
                    Dim isLastLineStar As Boolean = False
                    Dim isRouteTotalStart As Boolean = False
                    Dim BookingCount As Integer = 0
                    clsCommon.ProgressBarPercentShow()
                    For ii = 0 To gv.Rows.Count - 1
                        If ii = 2314 - 1 Then
                            Dim x As Integer = 0
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1) / gv.Rows.Count * 100, "Separating " & (ii + 1) & "  of  Total " & gv.Rows.Count)
                        gvItem.Refresh()
                        Dim strLine As String = (gv.Rows(ii).Cells(colTSDetail).Value)
                        If strLine.Contains("THE TELANGANA STATE DAIRY") Or
                            strLine.StartsWith("Page:") Or
                            strLine.StartsWith("SPECIAL GHEE MYSORE") Or
                               strLine.StartsWith("( Including TCS Amount") Or
                             strLine.StartsWith("Booth Name") Then
                            Continue For
                        End If
                        Dim strTemp As String = strLine
                        If clsCommon.myLen(strLine) > 200 Then
                            strTemp = strLine.Substring(0, 199)
                        End If

                        If arrExcept IsNot Nothing AndAlso arrExcept.Count > 0 Then
                            Dim isContinueFor As Boolean = False
                            If arrExcept.Contains(strTemp) Then
                                isContinueFor = True
                            Else
                                For Each strT As String In arrExcept
                                    If strTemp.StartsWith(strT) Then
                                        isContinueFor = True
                                        Exit For
                                    End If
                                Next
                            End If
                            If isContinueFor Then
                                isPreviousLineDash = False
                                isPreviousToPReviousLineDash = False
                                Continue For
                            End If
                        End If


                        If strLine.Contains("-------------------------") Then
                            isLastLineStar = False
                            isPreviousLineDash = True
                            Continue For
                        End If
                        If strLine.EndsWith("*") Then
                            isPreviousLineDash = False
                            isPreviousToPReviousLineDash = False
                            isLastLineStar = True
                            Continue For
                        End If

                        If strLine.StartsWith("ZONE") Then
                            strZone = Microsoft.VisualBasic.Mid(strLine, 7, 15)
                            Continue For
                        End If


                        If strLine.StartsWith("ROUTE TOTAL") Then
                            isRouteTotalStart = True
                            Continue For
                        End If
                        If strLine.Contains("Q.P.S") And strLine.Contains("LOADED BY") And strLine.Contains("SECURITY") And strLine.Contains("RMRD") Then
                            isRouteTotalStart = False
                            Continue For
                        End If
                        If isRouteTotalStart Then
                            Continue For
                        End If

                        If strLine.StartsWith("ROUTE") Then
                            strRoute = Microsoft.VisualBasic.Mid(strLine, 8, 15)
                            Continue For
                        End If
                        Dim ischangePreviousCustomer As Boolean = True
                        If isPreviousLineDash AndAlso isTecxpertGatePass Then
                            strTemp = Microsoft.VisualBasic.Mid(strLine, 1, 18)
                            If strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/") Then
                                isPreviousToPReviousLineDash = True
                                ischangePreviousCustomer = False
                            End If
                        End If
                        If isPreviousToPReviousLineDash Then
                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 1, 5)) > 0 Then
                                strBoothID = Microsoft.VisualBasic.Mid(strLine, 1, 5)
                                If strBoothID.Contains("(") Then
                                    Dim strTempBreak As String() = strBoothID.Split("(")
                                    strBoothID = strTempBreak(0)
                                End If
                                If ischangePreviousCustomer Then
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIBoothID).Value = strBoothID
                                End If
                                isPreviousLineDash = False
                                isPreviousToPReviousLineDash = False
                            End If
                        End If
                        If strLine.Contains(":") AndAlso isTecxpertGatePass Then
                            Continue For
                        End If
                        If Not isLastLineStar Then
                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 21, 9)) > 0 Then
                                gvItem.Rows.AddNew()
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSISNo).Value = gvItem.Rows.Count
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIPSNo).Value = gv.Rows(ii).Cells(colTSLine).Value

                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIZone).Value = strZone
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIRoute).Value = strRoute
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIBoothID).Value = strBoothID

                                strTemp = Microsoft.VisualBasic.Mid(strLine, 21, 10)
                                strTemp = strTemp.Replace("         ", " ")
                                strTemp = strTemp.Replace("        ", " ")
                                strTemp = strTemp.Replace("       ", " ")
                                strTemp = strTemp.Replace("      ", " ")
                                strTemp = strTemp.Replace("     ", " ")
                                strTemp = strTemp.Replace("    ", " ")
                                strTemp = strTemp.Replace("   ", " ")
                                strTemp = strTemp.Replace("  ", " ")

                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIType).Value = strTemp


                                Try
                                    strTemp = strLine.Substring(30).TrimStart()
                                    strTemp = strTemp.Replace("               ", "#")
                                    strTemp = strTemp.Replace("              ", "#")
                                    strTemp = strTemp.Replace("             ", "#")
                                    strTemp = strTemp.Replace("            ", "#")
                                    strTemp = strTemp.Replace("           ", "#")
                                    strTemp = strTemp.Replace("          ", "#")
                                    strTemp = strTemp.Replace("         ", "#")
                                    strTemp = strTemp.Replace("        ", "#")
                                    strTemp = strTemp.Replace("       ", "#")
                                    strTemp = strTemp.Replace("      ", "#")
                                    strTemp = strTemp.Replace("     ", "#")
                                    strTemp = strTemp.Replace("    ", "#")
                                    strTemp = strTemp.Replace("   ", "#")
                                    strTemp = strTemp.Replace("  ", "#")
                                    strTemp = strTemp.Replace(". ", ".")
                                    strTemp = strTemp.Replace(" ", "#")

                                    Dim strTempBreak As String() = strTemp.Split("#")
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSICR).Value = strTempBreak(0)
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSICD).Value = strTempBreak(1)
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSISO).Value = strTempBreak(2)
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSICash).Value = strTempBreak(3)
                                    If strTempBreak.Length > 6 Then
                                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTSIAmount).Value = strTempBreak(6)
                                    End If


                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If

                        isPreviousToPReviousLineDash = isPreviousLineDash
                        isPreviousLineDash = False
                    Next
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at " & ii + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridTSItems(ByVal gvItem As RadGridView)
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colTSISNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo"
        repoLineNo.Name = colTSIPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Zone"
        repoLineNo.Name = colTSIZone
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Route"
        repoLineNo.Name = colTSIRoute
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booth ID"
        repoLineNo.Name = colTSIBoothID
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Type"
        repoLineNo.Name = colTSIType
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CR"
        repoTaxBaseAmt.Name = colTSICR
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CD"
        repoTaxBaseAmt.Name = colTSICD
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "SO"
        repoTaxBaseAmt.Name = colTSISO
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Cash"
        repoTaxBaseAmt.Name = colTSICash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)


        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Amount"
        repoTaxBaseAmt.Name = colTSIAmount
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        gvItem.AllowDeleteRow = True
        gvItem.AllowAddNewRow = False
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = False
        gvItem.AllowRowReorder = False
        gvItem.EnableSorting = False
        gvItem.EnableFiltering = True
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.TableElement.TableHeaderHeight = 40
    End Sub


    Sub SaveDataTS(ByVal StrCompany As String, ByVal gvItem As RadGridView, ByVal tran As SqlTransaction)
        For ii As Integer = 0 To gvItem.Rows.Count - 1
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Company", StrCompany)
            clsCommon.AddColumnsForChange(coll, "Booth", clsCommon.myCstr(gvItem.Rows(ii).Cells(colTSIBoothID).Value))
            clsCommon.AddColumnsForChange(coll, "Type", clsCommon.myCstr(gvItem.Rows(ii).Cells(colTSIType).Value))
            clsCommon.AddColumnsForChange(coll, "CR", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSICR).Value))
            clsCommon.AddColumnsForChange(coll, "CD", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSICD).Value))
            clsCommon.AddColumnsForChange(coll, "SO", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSISO).Value))
            clsCommon.AddColumnsForChange(coll, "Cash", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSICash).Value))
            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSIAmount).Value))
            clsCommon.AddColumnsForChange(coll, "PNO", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTSISNo).Value))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Notepad_Read_Mismatch", OMInsertOrUpdate.Insert, "", tran)
        Next
    End Sub
    Private Sub RadButton10_Click(sender As Object, e As EventArgs) Handles RadButton10.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            'clsCommon.ProgressBarPercentShow()

            'LoadBlankGridTSMismatch()
            Dim qry As String = "TRUNCATE TABLE TSPL_Notepad_Read_Mismatch"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            'clsCommon.ProgressBarPercentUpdate(10, "Blank Table and mismatch grid...")
            SaveDataTS("Milkosoft", gvTSItem, tran)
            'clsCommon.ProgressBarPercentUpdate(30, "Reading Milkosoft Data...")
            SaveDataTS("Tecxpert", gvTSItemTecxpert, tran)
            'clsCommon.ProgressBarPercentUpdate(50, "Reading Tecxpert Data...")

            qry = "select Booth,Type
,max(case when Company='Milkosoft' then PNO else null end) as [Milkosoft PSNO], max( case when Company='Tecxpert' then PNO else null end) as [Tecxpert PSNo] 
,sum(case when Company='Milkosoft' then CR else null end) as [Milkosoft CR], max( case when Company='Tecxpert' then CR else null end) as [Tecxpert CR]
,sum(case when Company='Milkosoft' then CD else null end) as [Milkosoft CD], max( case when Company='Tecxpert' then CD else null end) as [Tecxpert CD]
,sum(case when Company='Milkosoft' then SO else null end) as [Milkosoft SO], max( case when Company='Tecxpert' then SO else null end) as [Tecxpert SO]
,sum(case when Company='Milkosoft' then Cash else null end) as [Milkosoft Cash], max( case when Company='Tecxpert' then Cash else null end) as [Tecxpert Cash]
,sum(case when Company='Milkosoft' then Amount else null end) as [Milkosoft Amount], max( case when Company='Tecxpert' then Amount else null end) as [Tecxpert Amount]
from TSPL_Notepad_Read_Mismatch  
group by Booth,Type 
having 
sum(CR * case when Company='Milkosoft' then 1 else 0 end)<>sum(CR * case when Company='Tecxpert' then 1 else 0 end)
or sum(CD * case when Company='Milkosoft' then 1 else 0 end)<>sum(CD * case when Company='Tecxpert' then 1 else 0 end)
or sum(SO * case when Company='Milkosoft' then 1 else 0 end)<>sum(SO * case when Company='Tecxpert' then 1 else 0 end)
or sum(Cash * case when Company='Milkosoft' then 1 else 0 end)<>sum(Cash * case when Company='Tecxpert' then 1 else 0 end)
or abs(sum(Amount * case when Company='Milkosoft' then 1 else 0 end)-sum(Amount * case when Company='Tecxpert' then 1 else 0 end))>1"
            'clsCommon.ProgressBarPercentUpdate(70, "Analyzing Mismatch Data...")
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            tran.Commit()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvTSMismatch.DataSource = dt
                For ii As Integer = 0 To gvTSMismatch.Columns.Count - 1
                    gvTSMismatch.Columns(ii).ReadOnly = True
                    gvTSMismatch.Columns(ii).BestFit()
                Next

                gvTSMismatch.AllowDeleteRow = True
                gvTSMismatch.AllowAddNewRow = False
                gvTSMismatch.ShowGroupPanel = False
                gvTSMismatch.AllowColumnReorder = False
                gvTSMismatch.AllowRowReorder = False
                gvTSMismatch.EnableSorting = False
                gvTSMismatch.EnableFiltering = True
                gvTSMismatch.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gvTSMismatch.MasterTemplate.ShowRowHeaderColumn = False
                gvTSMismatch.TableElement.TableHeaderHeight = 40
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Mismatched", Me.Text)
            End If
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridTSMismatch()
        gvTSMismatch.Rows.Clear()
        gvTSMismatch.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colTSMISNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo Milkosoft"
        repoLineNo.Name = colTSMIPSNoMilkososft
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo Tecxpert"
        repoLineNo.Name = colTSMIPSNoTecxpert
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoLineNo)



        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booth ID"
        repoLineNo.Name = colTSMIBoothID
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSMismatch.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Type"
        repoLineNo.Name = colTSMIType
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSMismatch.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CR"
        repoTaxBaseAmt.Name = colTSICR
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CD"
        repoTaxBaseAmt.Name = colTSICD
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "SO"
        repoTaxBaseAmt.Name = colTSISO
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Cash"
        repoTaxBaseAmt.Name = colTSICash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoTaxBaseAmt)


        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Amount"
        repoTaxBaseAmt.Name = colTSMIAmount
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSMismatch.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        gvTSMismatch.AllowDeleteRow = True
        gvTSMismatch.AllowAddNewRow = False
        gvTSMismatch.ShowGroupPanel = False
        gvTSMismatch.AllowColumnReorder = False
        gvTSMismatch.AllowRowReorder = False
        gvTSMismatch.EnableSorting = False
        gvTSMismatch.EnableFiltering = True
        gvTSMismatch.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTSMismatch.MasterTemplate.ShowRowHeaderColumn = False
        gvTSMismatch.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton11_Click(sender As Object, e As EventArgs) Handles RadButton11.Click
        LoadBlankGridTS(gvTS)
        LoadBlankGridTSItems(gvTSItem)
        LoadBlankGridTS(gvTSTecxpert)
        LoadBlankGridTSItems(gvTSItemTecxpert)
        'LoadBlankGridTSMismatch()
    End Sub

    Private Sub RadButton12_Click(sender As Object, e As EventArgs) Handles RadButton12.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Truck Sheet mismatch Milkosoft Vs Tecxpert")
            PageSetupReport_ID = "NMis"
            transportSql.QuickExportToExcel(gvTSMismatch, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnFolderBrowse_Click(sender As Object, e As EventArgs) Handles btnFolderBrowse.Click
        Try
            txtFolderBrowse.Text = ""
            FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
            FolderBrowserDialog1.ShowNewFolderButton = False
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                txtFolderBrowse.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            If clsCommon.myLen(txtFolderBrowse.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If clsCommon.myLen(txtFolderBrowse.Text) > 0 Then
                LoadBlankGridGP()
                Dim strFileSize As String = ""
                Dim di As New IO.DirectoryInfo(txtFolderBrowse.Text)
                Dim aryFi As IO.FileInfo() = di.GetFiles("*.txt")
                Dim fi As IO.FileInfo
                Dim ii As Integer = 0
                Dim Total As Integer = aryFi.Count
                clsCommon.ProgressBarPercentShow()
                For Each fi In aryFi
                    Try
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Files [" & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "]")
                        Try
                            Dim path As String = fi.FullName
                            Dim lineCount = System.IO.File.ReadAllLines(path).Length
                            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(path)
                            Dim line As String = ""
                            Dim FileSno As Integer = 1
                            Do While sr.Peek() >= 0
                                line = sr.ReadLine()
                                gvGP.Rows.AddNew()
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPSNo).Value = gvGP.Rows.Count
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPFile).Value = fi.Name
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPFileSNo).Value = FileSno
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPDetail).Value = line
                                FileSno += 1
                                gvGP.Refresh()
                            Loop
                            sr.Close()
                        Catch ex As Exception
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Error in File Reading " + Environment.NewLine + ex.ToString())
                        End Try

                    Catch ex As Exception
                        Throw New Exception("Error in File " + fi.Name + Environment.NewLine + ex.Message)
                    End Try
                Next
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridGP()
        gvGP.Rows.Clear()
        gvGP.Columns.Clear()


        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colGPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "File"
        repoLineNo.Name = colGPFile
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "File SNo"
        repoLineNo.Name = colGPFileSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Details"
        repoLineNo.Name = colGPDetail
        repoLineNo.Width = 1000
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        gvGP.AllowDeleteRow = True
        gvGP.AllowAddNewRow = False
        gvGP.ShowGroupPanel = False
        gvGP.AllowColumnReorder = False
        gvGP.AllowRowReorder = False
        gvGP.EnableSorting = False
        gvGP.EnableFiltering = True
        gvGP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvGP.MasterTemplate.ShowRowHeaderColumn = False
        gvGP.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(txtFolderBrowse.Text) <= 0 Then
                Throw New Exception("Please Select folder to upload")
            End If
            If gvGP.Rows.Count > 0 Then
                LoadBlankGridGPItems(gvGPItem)
                Dim ii As Integer = 0
                Try
                    Dim isReadFile As Boolean = False
                    Dim strFileName As String = ""
                    Dim strDate As String = Nothing
                    Dim strShift As String = Nothing
                    Dim strRoute As String = Nothing
                    Dim strBoothID As String = Nothing
                    Dim strTemp As String = Nothing
                    clsCommon.ProgressBarPercentShow()
                    For ii = 0 To gvGP.Rows.Count - 1
                        If ii = 10 - 1 Then
                            Dim x As Integer = 0
                        End If
                        If Not clsCommon.CompairString(strFileName, clsCommon.myCstr(gvGP.Rows(ii).Cells(colGPFile).Value)) = CompairStringResult.Equal Then
                            isReadFile = True
                            strFileName = clsCommon.myCstr(gvGP.Rows(ii).Cells(colGPFile).Value)
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1) / gvGP.Rows.Count * 100, "Separating " & (ii + 1) & "  of  Total " & gvGP.Rows.Count)
                        gvGPItem.Refresh()
                        Dim strLine As String = (gvGP.Rows(ii).Cells(colGPDetail).Value)
                        If clsCommon.myLen(strLine) <= 0 Then
                            Continue For
                        End If
                        strTemp = strLine.Trim()
                        If strTemp.Contains("THE TELANGANA STATE DAIRY") Or
                            strTemp.StartsWith("DES") Or
                            strTemp.StartsWith("01-04-11") Or
                            strTemp.StartsWith("Time :") Or
                            strTemp.StartsWith("Booth Name") Then
                            Continue For
                        End If
                        If strLine.Contains("DISTRIBUTOR WISE") Then
                            strDate = Microsoft.VisualBasic.Mid(strLine, 63, 10)
                            strShift = Microsoft.VisualBasic.Mid(strLine, 29, 7)
                            Continue For
                        End If
                        If strLine.Contains("-------------------------") Then
                            strTemp = (gvGP.Rows(ii - 1).Cells(colGPDetail).Value)
                            If clsCommon.myLen(strTemp) <= 0 Or (strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/")) Then
                                strTemp = (gvGP.Rows(ii + 1).Cells(colGPDetail).Value)
                                If clsCommon.myLen(strTemp) <= 0 Then
                                    isReadFile = False
                                End If
                            End If
                            Continue For
                        End If
                        If strLine.StartsWith("Route :") Then
                            strRoute = Microsoft.VisualBasic.Mid(strLine, 8, 10)
                        End If

                        If isReadFile Then
                            strTemp = Microsoft.VisualBasic.Mid(strLine, 1, 18)
                            If strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/") Then
                                Dim strTempBreak As String() = strTemp.Split("(")
                                strBoothID = strTempBreak(0).Trim
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID
                            End If

                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 21, 9)) > 0 Then
                                gvGPItem.Rows.AddNew()
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPISNo).Value = gvGPItem.Rows.Count
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIPSNo).Value = gvGP.Rows(ii).Cells(colGPSNo).Value
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIDate).Value = strDate
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIShift).Value = strShift
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIRoute).Value = strRoute
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID

                                strTemp = Microsoft.VisualBasic.Mid(strLine, 20, 10)
                                strTemp = strTemp.Replace("         ", " ")
                                strTemp = strTemp.Replace("        ", " ")
                                strTemp = strTemp.Replace("       ", " ")
                                strTemp = strTemp.Replace("      ", " ")
                                strTemp = strTemp.Replace("     ", " ")
                                strTemp = strTemp.Replace("    ", " ")
                                strTemp = strTemp.Replace("   ", " ")
                                strTemp = strTemp.Replace("  ", " ")

                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIType).Value = strTemp


                                Try
                                    strTemp = strLine.Substring(31).TrimStart()
                                    strTemp = strTemp.Replace("               ", "#")
                                    strTemp = strTemp.Replace("              ", "#")
                                    strTemp = strTemp.Replace("             ", "#")
                                    strTemp = strTemp.Replace("            ", "#")
                                    strTemp = strTemp.Replace("           ", "#")
                                    strTemp = strTemp.Replace("          ", "#")
                                    strTemp = strTemp.Replace("         ", "#")
                                    strTemp = strTemp.Replace("        ", "#")
                                    strTemp = strTemp.Replace("       ", "#")
                                    strTemp = strTemp.Replace("      ", "#")
                                    strTemp = strTemp.Replace("     ", "#")
                                    strTemp = strTemp.Replace("    ", "#")
                                    strTemp = strTemp.Replace("   ", "#")
                                    strTemp = strTemp.Replace("  ", "#")
                                    strTemp = strTemp.Replace(" ", "#")
                                    Dim strTempBreak As String() = strTemp.Split("#")
                                    For index As Integer = 0 To 6
                                        If clsCommon.myCdbl(strTempBreak(index)) > 0 Then
                                            gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPICash).Value = strTempBreak(index)
                                            Exit For
                                        End If
                                    Next

                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at " & ii + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridGPItems(ByVal gvGPItem As RadGridView)
        gvGPItem.Rows.Clear()
        gvGPItem.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colGPISNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo"
        repoLineNo.Name = colGPIPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Date"
        repoLineNo.Name = colGPIDate
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Shift"
        repoLineNo.Name = colGPIShift
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Route"
        repoLineNo.Name = colGPIRoute
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booth ID"
        repoLineNo.Name = colGPIBoothID
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Type"
        repoLineNo.Name = colGPIType
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Cash"
        repoTaxBaseAmt.Name = colGPICash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)



        gvGPItem.AllowDeleteRow = True
        gvGPItem.AllowAddNewRow = False
        gvGPItem.ShowGroupPanel = False
        gvGPItem.AllowColumnReorder = False
        gvGPItem.AllowRowReorder = False
        gvGPItem.EnableSorting = False
        gvGPItem.EnableFiltering = True
        gvGPItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvGPItem.MasterTemplate.ShowRowHeaderColumn = False
        gvGPItem.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        FileGo(txtFolderBrowseTecxpert, gvGPTecxpert)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        OpenFile(txtFolderBrowseTecxpert)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(txtFolderBrowseTecxpert.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If gvGPTecxpert.Rows.Count > 0 Then
                LoadBlankGridGPItems(gvGPItemTecxpert)
                Dim ii As Integer = 0
                Try
                    Dim DashCounter As Integer = 0
                    Dim strDate As String = Nothing
                    Dim strShift As String = Nothing
                    Dim strRoute As String = Nothing
                    Dim strBoothID As String = Nothing
                    Dim strTemp As String = Nothing
                    Dim isRouteTotalStart As Boolean = False
                    Dim isPreviousLineDash As Boolean = False
                    clsCommon.ProgressBarPercentShow()
                    For ii = 0 To gvGPTecxpert.Rows.Count - 1
                        If ii = 10 - 1 Then
                            Dim x As Integer = 0
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1) / gvGPTecxpert.Rows.Count * 100, "Separating " & (ii + 1) & "  of  Total " & gvGPTecxpert.Rows.Count)
                        gvGPItemTecxpert.Refresh()
                        Dim strLine As String = (gvGPTecxpert.Rows(ii).Cells(colTSDetail).Value)
                        If clsCommon.myLen(strLine) <= 0 Then
                            Continue For
                        End If
                        If strLine.Contains("THE TELANGANA STATE DAIRY") Or
                                strLine.StartsWith("Page:") Or
                                strLine.StartsWith("( Including") Or
                                strLine.StartsWith("ZONE :") Then
                            Continue For
                        End If

                        If strLine.StartsWith("ROUTE :") AndAlso strLine.Contains("TRUCK SHEET") Then

                            strTemp = strLine
                            strTemp = strTemp.Replace("                    ", " ")
                            strTemp = strTemp.Replace("                   ", " ")
                            strTemp = strTemp.Replace("                  ", " ")
                            strTemp = strTemp.Replace("                 ", " ")
                            strTemp = strTemp.Replace("                ", " ")
                            strTemp = strTemp.Replace("               ", " ")
                            strTemp = strTemp.Replace("              ", " ")
                            strTemp = strTemp.Replace("             ", " ")
                            strTemp = strTemp.Replace("            ", " ")
                            strTemp = strTemp.Replace("           ", " ")
                            strTemp = strTemp.Replace("          ", " ")
                            strTemp = strTemp.Replace("         ", " ")
                            strTemp = strTemp.Replace("        ", " ")
                            strTemp = strTemp.Replace("       ", " ")
                            strTemp = strTemp.Replace("      ", " ")
                            strTemp = strTemp.Replace("     ", " ")
                            strTemp = strTemp.Replace("    ", " ")
                            strTemp = strTemp.Replace("   ", " ")
                            strTemp = strTemp.Replace("  ", " ")

                            Dim strTempBreak As String() = strTemp.Split(" ")
                            strRoute = strTempBreak(2)
                            strShift = strTempBreak(3)
                            strDate = strTempBreak(7)

                            'strDate = Microsoft.VisualBasic.Mid(strLine, 51, 10)
                            'strShift = Microsoft.VisualBasic.Mid(strLine, 28, 7)
                            'strRoute = Microsoft.VisualBasic.Mid(strLine, 8, 10)
                            Continue For
                        End If
                        If strLine.StartsWith("Booth Name") Then
                            DashCounter = 0
                            Continue For
                        End If
                        If strLine.Contains("-------------------------") Then
                            isPreviousLineDash = True
                            DashCounter += 1
                            If Not isRouteTotalStart Then
                                Continue For
                            End If
                        End If

                        If strLine.StartsWith("ROUTE TOTAL") Then
                            DashCounter = 0
                            isRouteTotalStart = True
                            Continue For
                        End If
                        If isRouteTotalStart AndAlso DashCounter = 4 Then
                            isRouteTotalStart = False
                            DashCounter = 0
                            Continue For
                        End If
                        If isRouteTotalStart Then
                            Continue For
                        End If




                        If True Then
                            strTemp = Microsoft.VisualBasic.Mid(strLine, 1, 18)
                            If strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/") Then
                                Dim strTempBreak As String() = strTemp.Split("(")
                                strBoothID = strTempBreak(0).Trim
                                If Not isPreviousLineDash Then
                                    gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID
                                End If
                            End If

                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 21, 9)) > 0 Then
                                DashCounter = 0
                                gvGPItemTecxpert.Rows.AddNew()
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPISNo).Value = gvGPItemTecxpert.Rows.Count
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIPSNo).Value = gvGPTecxpert.Rows(ii).Cells(colTSLine).Value
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIDate).Value = strDate
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIShift).Value = strShift
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIRoute).Value = strRoute
                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID

                                strTemp = Microsoft.VisualBasic.Mid(strLine, 20, 10)
                                strTemp = strTemp.Replace("         ", " ")
                                strTemp = strTemp.Replace("        ", " ")
                                strTemp = strTemp.Replace("       ", " ")
                                strTemp = strTemp.Replace("      ", " ")
                                strTemp = strTemp.Replace("     ", " ")
                                strTemp = strTemp.Replace("    ", " ")
                                strTemp = strTemp.Replace("   ", " ")
                                strTemp = strTemp.Replace("  ", " ")

                                gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPIType).Value = strTemp


                                Try
                                    strTemp = strLine.Substring(31).TrimStart()
                                    strTemp = strTemp.Replace("               ", "#")
                                    strTemp = strTemp.Replace("              ", "#")
                                    strTemp = strTemp.Replace("             ", "#")
                                    strTemp = strTemp.Replace("            ", "#")
                                    strTemp = strTemp.Replace("           ", "#")
                                    strTemp = strTemp.Replace("          ", "#")
                                    strTemp = strTemp.Replace("         ", "#")
                                    strTemp = strTemp.Replace("        ", "#")
                                    strTemp = strTemp.Replace("       ", "#")
                                    strTemp = strTemp.Replace("      ", "#")
                                    strTemp = strTemp.Replace("     ", "#")
                                    strTemp = strTemp.Replace("    ", "#")
                                    strTemp = strTemp.Replace("   ", "#")
                                    strTemp = strTemp.Replace("  ", "#")
                                    strTemp = strTemp.Replace(". ", ".")
                                    strTemp = strTemp.Replace(" ", "#")
                                    Dim strTempBreak As String() = strTemp.Split("#")
                                    For index As Integer = 0 To 6
                                        If clsCommon.myCdbl(strTempBreak(index)) > 0 Then
                                            gvGPItemTecxpert.Rows(gvGPItemTecxpert.Rows.Count - 1).Cells(colGPICash).Value = strTempBreak(index)
                                            Exit For
                                        End If
                                    Next

                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                            isPreviousLineDash = False
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at " & ii + Environment.NewLine + ex.ToString())
                End Try
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton13_Click(sender As Object, e As EventArgs) Handles RadButton13.Click

    End Sub

    Private Sub RadButton15_Click(sender As Object, e As EventArgs) Handles RadButton15.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Gate Pass mismatch Milkosoft Vs Tecxpert")
            PageSetupReport_ID = "GPMis"
            transportSql.QuickExportToExcel(gvGPMismatch, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton14_Click(sender As Object, e As EventArgs) Handles RadButton14.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim qry As String = "TRUNCATE TABLE TSPL_Notepad_Read_Mismatch_GP"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            SaveDataGP("Milkosoft", gvGPItem, tran)
            SaveDataGP("Tecxpert", gvGPItemTecxpert, tran)

            qry = "select Booth,Type,Date
,max(case when Company='Milkosoft' then PNO else null end) as [Milkosoft PSNO], max( case when Company='Tecxpert' then PNO else null end) as [Tecxpert PSNo] 
,sum(case when Company='Milkosoft' then Cash else null end) as [Milkosoft Cash], max( case when Company='Tecxpert' then Cash else null end) as [Tecxpert Cash]
from TSPL_Notepad_Read_Mismatch_GP  
group by Booth,Type,Date
having sum(Cash * case when Company='Milkosoft' then 1 else 0 end)<>sum(Cash * case when Company='Tecxpert' then 1 else 0 end)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            tran.Commit()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvGPMismatch.DataSource = dt
                For ii As Integer = 0 To gvGPMismatch.Columns.Count - 1
                    gvGPMismatch.Columns(ii).ReadOnly = True
                    gvGPMismatch.Columns(ii).BestFit()
                Next

                gvGPMismatch.AllowDeleteRow = True
                gvGPMismatch.AllowAddNewRow = False
                gvGPMismatch.ShowGroupPanel = False
                gvGPMismatch.AllowColumnReorder = False
                gvGPMismatch.AllowRowReorder = False
                gvGPMismatch.EnableSorting = False
                gvGPMismatch.EnableFiltering = True
                gvGPMismatch.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gvGPMismatch.MasterTemplate.ShowRowHeaderColumn = False
                gvGPMismatch.TableElement.TableHeaderHeight = 40
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Mismatched", Me.Text)
            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SaveDataGP(ByVal StrCompany As String, ByVal gvItem As RadGridView, ByVal tran As SqlTransaction)
        For ii As Integer = 0 To gvItem.Rows.Count - 1
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Company", StrCompany)
            clsCommon.AddColumnsForChange(coll, "Booth", clsCommon.myCstr(gvItem.Rows(ii).Cells(colGPIBoothID).Value))
            clsCommon.AddColumnsForChange(coll, "Type", clsCommon.myCstr(gvItem.Rows(ii).Cells(colGPIType).Value))
            clsCommon.AddColumnsForChange(coll, "Date", clsCommon.myCstr(gvItem.Rows(ii).Cells(colGPIDate).Value))
            clsCommon.AddColumnsForChange(coll, "Shift", clsCommon.myCstr(gvItem.Rows(ii).Cells(colGPIShift).Value))
            clsCommon.AddColumnsForChange(coll, "Route", clsCommon.myCstr(gvItem.Rows(ii).Cells(colGPIRoute).Value))
            clsCommon.AddColumnsForChange(coll, "Cash", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colGPICash).Value))
            clsCommon.AddColumnsForChange(coll, "PNO", clsCommon.myCdbl(gvItem.Rows(ii).Cells(colGPISNo).Value))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Notepad_Read_Mismatch_GP", OMInsertOrUpdate.Insert, "", tran)
        Next
    End Sub
End Class