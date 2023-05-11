Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptVSPMilkNotSold
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim isLoad As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        isLoad = True
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = dtpToDate.Value.AddMonths(-1)
        isLoad = False
    End Sub
    Sub Reset()
        fndLoc.Value = ""
        txtLocName.Text = ""
        txtMCC.Text = ""
        lblMCC.Text = ""
        txtPaymentCycleCode.Value = ""

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the Location first")
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the Cycle Code first")
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
            Dim CodeValue As String = clsCommon.GetMulcallString(txtMultiDeduction.arrValueMember)
            If chkDeduction.Checked Then
                If clsCommon.myLen(txtMultiDeduction.arrValueMember) > 0 Then

                    dedQry = "Select Distinct Code,Description from TSPL_DEDUCTION_MASTER 
                                                Left Outer Join TSPL_PAYMENT_PROCESS_DEDUCTION ON  TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                                                Where TSPL_DEDUCTION_MASTER.Code In(" + CodeValue + ")"
                    dtDedName = clsDBFuncationality.GetDataTable(dedQry)
                    If dtDedName.Rows.Count > 0 Then
                        For i As Integer = 0 To dtDedName.Rows.Count - 1
                            If clsCommon.myLen(DedName) > 0 AndAlso clsCommon.myLen(DedCode) > 0 Then
                                DedCode += "," + clsCommon.myCstr(dtDedName.Rows(i)("Code"))
                                DedName += "," + "Max(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                DedName1 += "," + "[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                OpntmpName += ",(" + "tmp1.[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]" + "+(Case When tmpOpening.IsOpening=1 Then tmpOpening.[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "] Else 0 End)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                tDedAmt += "+[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                            Else
                                DedCode = clsCommon.myCstr(dtDedName.Rows(i)("Code"))
                                DedName = ",Max(IsNull([" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "],0)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                DedName1 = "[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                OpntmpName = "(tmp1.[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]" + "+(Case When tmpOpening.IsOpening=1 Then tmpOpening.[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "] Else 0 End)) As [" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                                tDedAmt = "[" + clsCommon.myCstr(dtDedName.Rows(i)("Description")) + "]"
                            End If
                        Next
                    End If

                End If
                qry = "Select *,(" + tDedAmt + ") As Total from (Select Max([VLC Uploader Code]) As [VLC Uploader Code],max([VSP Name]) As [VSP Name],Max(MCC) As MCC,Max([MCC Name]) As [MCC Name]" + DedName + "
                         from ( 
                         select TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.MCC ,
                         TSPL_Location_MASTER.Location_Desc as [MCC Name] ,TSPL_Location_MASTER.Loc_Segment_Code as [SegmentCode],
                         TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code], '" + dtpFromDate.Text + "' as PaymentCycleFrom, '" + dtpToDate.Text + "' PaymentCycleTo
                         ,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt
                         ,tmp1.Vendor_CODE," + OpntmpName + "
                        from TSPL_VLC_MASTER_HEAD 
                        left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code
                        left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_VLC_MASTER_HEAD.MCC and TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC'
                        left outer join TSPL_PAYMENT_PROCESS_DEDUCTION on TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE=TSPL_VLC_MASTER_HEAD.VSP_Code
                        left outer join 
                        (Select * from (SELECT  Vendor_CODE," + DedName1 + " FROM
                        (SELECT Vendor_CODE,Ded_Code, Ded_Desc,Reduce_Deduc_Amt FROM TSPL_PAYMENT_PROCESS_DEDUCTION)Tab1
                        PIVOT(SUM(Reduce_Deduc_Amt) FOR Ded_Desc IN (" + DedName1 + ")) AS Tab2 )tmp)tmp1 ON tmp1.Vendor_CODE=TSPL_VLC_MASTER_HEAD.VSP_Code
                        left outer join
                        (Select * from (SELECT  Vendor_CODE,IsOpening," + DedName1 + " FROM
                        (SELECT Vendor_CODE,DeductionCode, Deduction_Desc,TSPL_MULTIPLE_DEDUCTION_Detail.Amount,IsOpening FROM TSPL_MULTIPLE_DEDUCTION_Detail Left Outer Join TSPL_MULTIPLE_DEDUCTION_head On TSPL_MULTIPLE_DEDUCTION_head.Document_No=TSPL_MULTIPLE_DEDUCTION_Detail.Document_No)Tab1
                        PIVOT(SUM(Amount) FOR Deduction_Desc IN (" + DedName1 + ")) AS Tab2 )tmp)As tmpOpening On tmpOpening.Vendor_Code=tmp1.Vendor_CODE 
                        where TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC.Text) + "' and TSPL_VLC_MASTER_HEAD.Active =1 and Loc_Segment_Code = '" + fndLoc.Value + "' and TSPL_VLC_MASTER_HEAD.VSP_Code not in (
                    select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No where convert (date, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) >= convert (date, '" + dtpFromDate.Value + "',103) and convert (date, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + dtpToDate.Value + "',103) and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + fndLoc.Value + "') 
                        )xx group by xx.[VSP Code])Abc where (" + tDedAmt + ")>0"
            Else
                qry = "select TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.MCC ,TSPL_Location_MASTER.Location_Desc as [MCC Name] ,TSPL_Location_MASTER.Loc_Segment_Code as [SegmentCode],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],  '" + dtpFromDate.Text + "' as PaymentCycleFrom, '" + dtpToDate.Text + "' PaymentCycleTo  from TSPL_VLC_MASTER_HEAD 
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code
                    left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_VLC_MASTER_HEAD.MCC and TSPL_Location_MASTER.Rejected_Type='N' and TSPL_Location_MASTER.Location_Category='MCC'
                    where TSPL_VLC_MASTER_HEAD.MCC='" + clsCommon.myCstr(txtMCC.Text) + "' and TSPL_VLC_MASTER_HEAD.Active =1 and Loc_Segment_Code = '" + fndLoc.Value + "' and TSPL_VLC_MASTER_HEAD.VSP_Code not in (
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
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
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
                clsCommon.MyExportToPDF("VSP Milk NOT Sold", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub



    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Reset()
        Dim whrCls As String = " 1=1 "
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
        '    End If
        'End If

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
        'If SettShowMCCFinder Then
        txtMCC.Text = clsCommon.myCstr(dr("Code"))
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Text + "'"))
        'End If



        'If clsCommon.myLen(fndLoc.Value) > 0 Then


        '    'If Not isLoad Then
        '    Dim PaymentCycleType As String = ""
        '        Dim PaymentCycleValue As Integer = 0

        '        If clsCommon.myLen(fndLoc.Value) <= 0 Then
        '            clsCommon.MyMessageBoxShow("Please select the Location first")
        '            Exit Sub
        '        End If
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
        '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
        '            Exit Sub
        '        End If
        '        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        '        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        '        'isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
        '        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
        '            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
        '                clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
        '                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                Exit Sub
        '            End If
        '            dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
        '            If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
        '                dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
        '                dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)
        '            End If
        '        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
        '            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
        '                clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
        '                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                Exit Sub
        '            End If
        '            dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
        '        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
        '            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
        '                clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
        '                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        '                Exit Sub
        '            End If
        '            dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
        '        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
        '            Dim today As Date = dtpFromDate.Value
        '            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
        '            dtpFromDate.Value = today.AddDays(-dayDiff)
        '            dtpToDate.Value = dtpFromDate.Value.AddDays(6)
        '        End If

        '        'End If
        '    End If
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
                clsCommon.MyMessageBoxShow("Please select the Location first")
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
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
                clsCommon.MyMessageBoxShow("Please select Location first..")
                Return
            End If
            'Dim strMccCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Location_Code from  TSPL_Location_MASTER where Loc_Segment_Code = '" + txtMCC.Value + "'"))
            Dim qry As String = " select Name as Code ,MCC_Code  ,Fiscal_Code , convert (varchar, From_Date,103) as [From Date] , convert (varchar,To_Date,103) as [To Date] from TSPL_PAYMENT_CYCLE_GENERATED "
            txtPaymentCycleCode.Value = clsCommon.ShowSelectForm("Route@paymentCycleReport", qry, "Code", " MCC_Code = '" + txtMCC.Text + "' ", txtPaymentCycleCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtPaymentCycleCode.Value) > 0 Then
                dtpFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select From_Date from  TSPL_PAYMENT_CYCLE_GENERATED where TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + txtMCC.Text + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'"))
                dtpToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select To_Date from  TSPL_PAYMENT_CYCLE_GENERATED where TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + txtMCC.Text + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' "))
                dtpFromDate.Enabled = False
                dtpToDate.Enabled = False
            Else
                dtpFromDate.Enabled = True
                dtpToDate.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkDeduction_Click(sender As Object, e As EventArgs) Handles chkDeduction.Click
        Try
            If chkDeduction.Checked = False Then
                MyLabel3.Visible = True
                txtMultiDeduction.Visible = True
            Else
                MyLabel3.Visible = False
                txtMultiDeduction.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMultiDeduction__My_Click(sender As Object, e As EventArgs) Handles txtMultiDeduction._My_Click
        Try
            Dim qry As String = "Select Distinct Code,Description From TSPL_DEDUCTION_MASTER Left Outer Join TSPL_PAYMENT_PROCESS_DEDUCTION On TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code"
            txtMultiDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMilkDedMulSelect", qry, "Code", "Description", txtMultiDeduction.arrValueMember, txtMultiDeduction.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
