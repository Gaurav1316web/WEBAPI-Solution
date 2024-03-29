Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class rptVSPMilkNotSold
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim PickAllMCC As Boolean = False
    Dim strQry As String = ""
    Dim isLoad As Boolean = False
    Dim AreaWiseBilling As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        isLoad = True
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
        '    ''Dim qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where TSPL_DEDUCTION_MASTER.Code In('22','31','33','36')"
        '    Dim Qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where Code In (Select DeductionCode from TSPL_MULTIPLE_DEDUCTION_DETAIL where TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount>0)"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim arr As New ArrayList
        '        For Each dr As DataRow In dt.Rows
        '            arr.Add(clsCommon.myCstr(dr("Code")))
        '        Next
        '        txtMultiDeduction.arrValueMember = arr
        '    End If
        'End If
        PickAllMCC = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickAllBMC, clsFixedParameterCode.PickAllBMC, Nothing)) = 1, True, False)
        If PickAllMCC Then
            txtMCC.Visible = False
            lblMCC.Visible = False
            TxtMCCMultifnd.Visible = True
            TxtMCCMultifnd.Location = New System.Drawing.Point(65, 40)
        Else
            txtMCC.Visible = True
            lblMCC.Visible = True
            TxtMCCMultifnd.Visible = False
            TxtMCCMultifnd.Location = New System.Drawing.Point(265, 160)
        End If
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = dtpToDate.Value.AddMonths(-1)
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Visible = AreaWiseBilling
        lblArea.Visible = AreaWiseBilling
        isLoad = False
    End Sub
    Sub Reset()
        fndLoc.Value = ""
        txtLocName.Text = ""
        txtMCC.Text = ""
        lblMCC.Text = ""
        txtPaymentCycleCode.Value = ""
        If clsCommon.myLen(TxtMCCMultifnd.arrValueMember) > 0 Then
            TxtMCCMultifnd.arrValueMember.Clear()
        End If
        fndArea.Value = ""
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        chkDeduction.Checked = False
        If clsCommon.myLen(txtMultiDeduction.arrValueMember) > 0 Then
            txtMultiDeduction.arrValueMember.Clear()
        End If
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
        '    ''Dim qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where TSPL_DEDUCTION_MASTER.Code In('22','31','33','36')"
        '    Dim Qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where Code In (Select DeductionCode from TSPL_MULTIPLE_DEDUCTION_DETAIL where TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount>0)"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim arr As New ArrayList
        '        For Each dr As DataRow In dt.Rows
        '            arr.Add(clsCommon.myCstr(dr("Code")))
        '        Next
        '        txtMultiDeduction.arrValueMember = arr
        '    End If
        'End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Cycle Code first", Me.Text)
                Exit Sub
            End If
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim dedQry As String = Nothing
            Dim DedName As String = Nothing
            Dim DedName1 As String = Nothing
            Dim tmpName As String = Nothing
            Dim OpntmpName As String = Nothing
            Dim DedCode As String = Nothing
            Dim tDedAmt As String = Nothing
            Dim dtDedName As DataTable = Nothing
            Dim Area As String = Nothing
            Area = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from TSPL_MCC_MASTER WHERE Area_Location_Code = '" + fndArea.Value + "' "))
            If chkDeduction.Checked Then
                If txtMultiDeduction.arrValueMember.Count > 0 Then
                    dedQry = "Select Code,Description from TSPL_DEDUCTION_MASTER where 2=2 "
                    If txtMultiDeduction.arrValueMember IsNot Nothing AndAlso txtMultiDeduction.arrValueMember.Count > 0 Then
                        dedQry += " and TSPL_DEDUCTION_MASTER.Code In (" + clsCommon.GetMulcallString(txtMultiDeduction.arrValueMember) + ")"
                    End If
                    dtDedName = clsDBFuncationality.GetDataTable(dedQry)
                    If dtDedName.Rows.Count > 0 Then
                        For i As Integer = 0 To dtDedName.Rows.Count - 1
                            If clsCommon.myLen(DedName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                                DedCode += "," + clsCommon.myCstr(dtDedName.Rows(i)("Code"))
                                DedName += "," + "Sum(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                DedName1 += "," + "[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                tDedAmt += "+(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0))"
                            Else
                                DedCode = clsCommon.myCstr(dtDedName.Rows(i)("Code"))
                                DedName = ",Sum(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                DedName1 = "[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                tDedAmt = "(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0))"
                            End If
                        Next
                    End If
                    Dim dtCycleDate As DataTable = Nothing
                    Dim strPreCycleDate As String = "select top(1) From_Date,To_Date from TSPL_Payment_Cycle_Generated where 1=1"
                    If PickAllMCC Then
                        If clsCommon.myLen(TxtMCCMultifnd.arrValueMember) > 0 Then
                            strPreCycleDate += "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code in( " + clsCommon.GetMulcallString(TxtMCCMultifnd.arrValueMember) + ")"
                        End If
                    Else
                        strPreCycleDate += "  and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + clsCommon.myCstr(txtMCC.Text) + "'"
                    End If
                    strPreCycleDate += " and TSPL_PAYMENT_CYCLE_GENERATED.From_Date < convert (date, '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "',103) Order By From_Date desc"
                    dtCycleDate = clsDBFuncationality.GetDataTable(strPreCycleDate)
                    qry = "Select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As 'DCS Code', Vendor_Name AS 'DCS Name'" + DedName + ",Sum(" + tDedAmt + ") As Total
                        from 
                        (SELECT  Vendor_CODE,Vendor_Name, " + DedName1 + " FROM
                        (SELECT Vendor_CODE,Vendor_Name,DeductionCode, Deduction_Desc,TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount 
                        FROM TSPL_MULTIPLE_DEDUCTION_DETAIL
					    left outer join TSPL_MULTIPLE_DEDUCTION_HEAD On TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
					    where TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' And TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date  <=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' "
                    'If txtMultiDeduction.arrValueMember IsNot Nothing AndAlso txtMultiDeduction.arrValueMember.Count > 0 Then
                    If clsCommon.myLen(txtMultiDeduction.arrValueMember) > 0 Then
                        qry += " and TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode in (" + clsCommon.GetMulcallString(txtMultiDeduction.arrValueMember) + ")"
                    End If
                    qry += ")Tab1
                         PIVOT(SUM(Amount) FOR Deduction_Desc IN (" + DedName1 + ")) AS Tab2 )ClosedDCS
					    Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=ClosedDCS.Vendor_Code
                        left outer join TSPL_MCC_MASTER ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_Code
					    Where ClosedDCS.Vendor_Code Not In(Select VSP_Code from TSPL_MILK_SRN_HEAD where Doc_Date >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' And Doc_Date  <=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "') "

                    If PickAllMCC Then
                        If clsCommon.myLen(TxtMCCMultifnd.arrValueMember) > 0 Then
                            qry += " and  TSPL_VLC_MASTER_HEAD.MCC in ( " + clsCommon.GetMulcallString(TxtMCCMultifnd.arrValueMember) + ")"
                        End If
                    Else
                        If AreaWiseBilling Then
                            If clsCommon.myLen(fndArea.Value) > 0 Then
                                qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                            End If
                        Else
                            qry += "  And TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC.Text) + "'"
                        End If
                    End If

                    qry += " Group By TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,Vendor_Name
                        Having Sum(" + tDedAmt + ")>0
					    Order  By Cast(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As int) Asc"
                End If
            Else
                qry = "select TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.MCC ,TSPL_Location_MASTER.Location_Desc as [MCC Name] ,'" + Area + "' as Area ,TSPL_Location_MASTER.Loc_Segment_Code as [SegmentCode],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],  '" + dtpFromDate.Text + "' as PaymentCycleFrom, '" + dtpToDate.Text + "' PaymentCycleTo  from TSPL_VLC_MASTER_HEAD 
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code
                    left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_VLC_MASTER_HEAD.MCC and TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC'
                    left outer join TSPL_MCC_MASTER ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_Code
                    where 1=1 "
                'If clsCommon.myLen(fndArea.Value) > 0 Then
                '    qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                'End If
                If PickAllMCC Then
                    If clsCommon.myLen(TxtMCCMultifnd.arrValueMember) > 0 Then
                        qry += " and  TSPL_VLC_MASTER_HEAD.MCC in ( " + clsCommon.GetMulcallString(TxtMCCMultifnd.arrValueMember) + ") "
                    End If
                Else
                    If AreaWiseBilling Then
                        If clsCommon.myLen(fndArea.Value) > 0 Then
                            qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                        End If
                    Else
                        qry += "  And TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC.Text) + "'"
                    End If
                    ' qry += " and TSPL_VLC_MASTER_HEAD.MCC ='" + clsCommon.myCstr(txtMCC.Text) + "'"
                End If
                qry += "  and TSPL_VLC_MASTER_HEAD.Active =1 and Loc_Segment_Code = '" + fndLoc.Value + "' and TSPL_VLC_MASTER_HEAD.VSP_Code not in (
                    select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No where convert (date, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) >= convert (date, '" + dtpFromDate.Value + "',103) and convert (date, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + dtpToDate.Value + "',103) and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + fndLoc.Value + "')"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim itemQty As New GridViewSummaryItem("Qty in Stocking UOM", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemQty)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            SetGridFormat()
            'ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim columnName As String = Nothing
        If Gv1.Rows.Count > 0 Then
            Dim Count As Integer = 0
            If Gv1.Columns.Count > 0 Then
                For ic As Integer = 0 To Gv1.Columns.Count - 1
                    If ic > 1 Then
                        columnName = Gv1.Columns(ic).Name
                        If Gv1.Columns(columnName).FormatString = "" Then
                            Gv1.Columns(columnName).FormatString = "{0:n2}"
                            summaryRowItem.Add(New GridViewSummaryItem(columnName, "{0:n2}", GridAggregateFunction.Sum))
                        End If
                    End If
                Next
            End If
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By Type"))
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.Columns(columnName).Width = 200
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptVSPMilkNotsold & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("VSP Milk NOT Sold", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF("VSP Milk NOT Sold", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Dim style As New GridPrintStyle()
                'style.FitWidthMode = PrintFitWidthMode.FitPageWidth
                'style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                'style.PrintSummaries = False
                'style.PrintHeaderOnEachPage = True
                'style.PrintHiddenColumns = False
                Gv1.PrintStyle = style
                Dim doc As New clsMyPrintDocument()
                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1
                doc.DocumentName = objCommonVar.CurrentCompanyName
                Dim dtCompDetails As DataTable = Nothing
                Dim strCompDetails As String = "select Phone1,Regn_No from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                dtCompDetails = clsDBFuncationality.GetDataTable(strCompDetails)
                Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Text + "'"))
                doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine + "Area" + " " + strLocation + " " + "Phone No." + clsCommon.myCstr(dtCompDetails.Rows(0)("Phone1")) + " " + "Reg No. " + clsCommon.myCstr(dtCompDetails.Rows(0)("Regn_No")) + Environment.NewLine + "Societywise Deduction Balance Report" + " " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " "
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)
                'doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                'doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)
                doc.AssociatedObject = Gv1
                'doc.Print()
                doc.RightFooter = "Page [Page #] of [Total Pages]"
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show()
                doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Try
            Reset()
            If PickAllMCC Then
                fndLoc.Value = clsCommon.ShowSelectForm("LocationSeg", "select distinct Loc_Segment_Code as Code from TSPL_LOCATION_MASTER", "Code", "Rejected_Type='N' and Location_Category='MCC'", fndLoc.Value, "Code", isButtonClicked)
                txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
            Else
                Dim whrCls As String = " 1=1 "
                whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
                Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
                If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
                    fndLoc.Value = ""
                    txtMCC.Text = ""
                    lblMCC.Text = ""
                    txtLocName.Text = ""
                    Exit Sub
                End If
                fndLoc.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
                txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
                txtMCC.Text = clsCommon.myCstr(dr("Code"))
                lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Text + "'"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        'SetToDate()
    End Sub
    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        'SetToDate()
    End Sub
    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    dtpToDate.Value = dtpFromDate.Value
                    Exit Sub
                End If
                dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)
                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            End If
            ' End If
        End If
    End Sub
    Private Sub txtPaymentCycleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleCode._MYValidating
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location first..", Me.Text)
                Return
            End If
            'Dim strMccCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Location_Code from  TSPL_Location_MASTER where Loc_Segment_Code = '" + txtMCC.Value + "'"))
            Dim qry As String = " select Name as Code ,MCC_Code  ,Fiscal_Code , convert (varchar, From_Date,103) as [From Date] , convert (varchar,To_Date,103) as [To Date] from TSPL_PAYMENT_CYCLE_GENERATED"
            If Not PickAllMCC Then
                qry += " where  MCC_Code = '" + txtMCC.Text + "'"
            End If
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("Route@paymentCycleReport", qry, "Code", txtPaymentCycleCode.Value)
            If dr IsNot Nothing Then
                txtPaymentCycleCode.Value = clsCommon.myCstr(dr("Code"))
                dtpFromDate.Value = clsCommon.myCDate(dr("From Date"))
                dtpToDate.Value = clsCommon.myCDate(dr("To Date"))
                dtpFromDate.Enabled = False
                dtpToDate.Enabled = False
            Else
                dtpFromDate.Enabled = True
                dtpToDate.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtMultiDeduction__My_Click(sender As Object, e As EventArgs) Handles txtMultiDeduction._My_Click
        Try
            Dim qry As String = "Select Distinct Code,Description From TSPL_DEDUCTION_MASTER Left Outer Join TSPL_PAYMENT_PROCESS_DEDUCTION On TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code"
            txtMultiDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMilkDedMulSelect", qry, "Code", "Description", txtMultiDeduction.arrValueMember, txtMultiDeduction.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkDeduction_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDeduction.CheckStateChanged
        Try
            If chkDeduction.Checked Then
                If clsCommon.myLen(txtPaymentCycleCode.Value) > 0 Then
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JDH") = CompairStringResult.Equal Then
                        ''Dim qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where TSPL_DEDUCTION_MASTER.Code In('22','31','33','36')"
                        Dim Qry As String = "Select Code from TSPL_DEDUCTION_MASTER Where Code In (Select  TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode from TSPL_MULTIPLE_DEDUCTION_DETAIL
                                        Inner Join TSPL_MULTIPLE_DEDUCTION_HEAD On TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                                        Inner Join TSPL_VLC_MASTER_HEAD ON  TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MULTIPLE_DEDUCTION_DETAIL.Vendor_Code
                                        where TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount>0 And 
				                    	TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' And TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date  <=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Text + "' 
		                                And TSPL_VLC_MASTER_HEAD.VSP_Code Not In(Select VSP_Code from TSPL_MILK_SRN_HEAD where Doc_Date >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "' And Doc_Date  <=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy HH:mm:ss tt") + "')) "
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim arr As New ArrayList
                            For Each dr As DataRow In dt.Rows
                                arr.Add(clsCommon.myCstr(dr("Code")))
                            Next
                            txtMultiDeduction.arrValueMember = arr
                        End If
                    End If
                    MyLabel3.Visible = True
                    txtMultiDeduction.Visible = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "Select Payment Cycle", Me.Text)
                    chkDeduction.Checked = False
                    MyLabel3.Visible = False
                    txtMultiDeduction.Visible = False
                End If
            Else
                MyLabel3.Visible = False
                txtMultiDeduction.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub TxtMCCMultifnd__My_Click(sender As Object, e As EventArgs) Handles TxtMCCMultifnd._My_Click
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER where Rejected_Type='N' and Location_Category='MCC' "
            TxtMCCMultifnd.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMCCMulSelect", qry, "Code", "Description", TxtMCCMultifnd.arrValueMember, TxtMCCMultifnd.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Reset()
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)

            fndLoc.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code ='" + fndArea.Value + "' "))
            txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
            txtMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from tspl_mcc_master where mcc_Code='" + fndArea.Value + "'"))
            lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + fndArea.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
