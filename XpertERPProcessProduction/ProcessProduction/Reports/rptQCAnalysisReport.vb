Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptQCAnalysisReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String

    Private Sub rptQCAnalysisReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()
        rbtnQCdate.IsChecked = True
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim whrcls As String = ""

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsLocation.getFinder(whrcls, txtLocation.Value, isButtonClicked)

    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.Value = ""
        txtVendor.Value = ""
        txtRALNo.arrValueMember = Nothing
        txtItemCode.Value = Nothing
        rbtnQCdate.IsChecked = True
        EnableDisableControl(True)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            Dim whrcls As String = "where 1 = 1 and TSPL_QC_CHECK_HEAD.posted=1 and TSPL_QC_CHECK_SRN_DETAIL.Mandatory = 1 "

            If rbtnQCdate.IsChecked = True Then
                whrcls += " And convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) <= convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "

            End If
            If rbtnWeighmentDate.IsChecked = True Then
                whrcls += " and convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) >= convert(date,('" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'),103) and convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) <= convert(date,('" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'),103) "
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Location_Code='" & txtLocation.Value & "'  "
            End If
            If clsCommon.myLen(txtItemCode.Value) > 0 then
           whrcls+ = "and  TSPL_QC_CHECK_SRN_DETAIL.Item_Code = '"& txtItemCode.value &"' "
                End if
              If clsCommon.myLen(txtVendor.Value) > 0 Then
                whrcls += "and TSPL_QC_CHECK_HEAD.Vendor_Code = '" & txtVendor.Value & "'  "
            End If
            If txtRALNo.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_GRN_HEAD.Ref_No in (" + clsCommon.GetMulcallString(txtRALNo.arrValueMember) + ")"
            End If

            Dim qry As String = "select TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code,max(case when len(tspl_qc_log_sheet_master.AliasName)>0 then tspl_qc_log_sheet_master.AliasName else tspl_qc_log_sheet_master.description end) as param_desc  from TSPL_QC_CHECK_SRN_DETAIL
        inner join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard' 
        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
        left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = TSPL_QC_CHECK_SRN_DETAIL.MRN_No and TSPL_MRN_DETAIL.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code
        left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No = TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No	left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_DETAIL.GRN_No
        " & whrcls &" group by QC_Param_Code"
            Dim dtParam As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim paramNameInput As String = Nothing
            Dim paramNameDed As String = Nothing
            Dim SumParamInput As String = Nothing
            Dim SumParamDed As String = Nothing
            Dim TotalDed As String = Nothing
            Dim paramInputPivot As String = Nothing
            Dim paramDedPivot As String = Nothing
            If dtParam.Rows.Count > 0 Then
                For i As Integer = 0 To dtParam.Rows.Count - 1
                    If i = 0 Then
                        paramInputPivot += "[" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "] "
                        paramDedPivot += "[" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded] "
                        TotalDed += "[" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded] "
                        paramNameInput += "IsNull([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "],0) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]"
                        paramNameDed += "IsNull([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded],0) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded]"
                        SumParamInput += "SUM([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]"
                        SumParamDed += "SUM([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded]) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded ]"

                    Else
                        paramInputPivot += ", [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "] "
                        paramDedPivot += ", [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded] "
                        TotalDed += "+" + "[" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded] "
                        paramNameInput += ", IsNull([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "],0) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]"
                        paramNameDed += ",IsNull([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded],0) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded]"
                        SumParamInput += ",SUM([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "]"
                        SumParamDed += ",SUM([" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded]) As [" + clsCommon.myCstr(dtParam.Rows(i)("param_desc")) + "Ded ]"

                    End If
                Next
            Else
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            Dim strDate As String = Nothing
            If rbtnQCdate.IsChecked Then
                strDate = "QC Date"
            Else
                strDate = "Weighment Date"
            End If
            qry = ""
            qry = "select '" & strDate & "' as [" & strDate & "] ,  max(Location_Code)Location_Code,max(Location_Desc)[Location Desc],max(Vendor_Code)Vendor_Code,max(Vendor_Name)[Vendor Name], max(Item_Code)Item_Code,max(Item_Desc)[Item Desc],max(RAL_NO)RAL_NO , Document_Code,max(Document_Date)Document_Date,
            MAX(Item_Desc)[Item Name] ,max(Weighment_Code)Weighment_Code , max(Weighment_Date)Weighment_Date,max(VehicleNo)VehicleNo ," & SumParamInput & ", '' as Qc," & SumParamDed & ", sum(" & TotalDed & ") as [Total Deduction]
            from ( select VehicleNo,Item_Desc,Location_Desc,Vendor_Name, Location_Code,Document_Code,Document_Date, Vendor_Code ,Item_Code , Weighment_Code , Weighment_Date , QC_Param_Code ,Gate_Entry_No, Gate_Entry_Date,RAL_NO ," & paramNameInput & ", " & paramNameDed & "
            from ( select max(VehicleNo)VehicleNo, max(Location_Code)Location_Code,max(Location_Desc)Location_Desc , Document_Code,max(Document_Date)Document_Date,max(Vendor_Code)Vendor_Code,max(Vendor_Name)Vendor_Name, max(Item_Code)Item_Code,max(Weighment_Code)Weighment_Code , max(Weighment_Date)Weighment_Date , max(QC_Param_Code)QC_Param_Code ,
            max(Gate_Entry_No)Gate_Entry_No , max(Gate_Entry_Date)Gate_Entry_Date ,MAX(Item_Desc)Item_Desc, max(RAL_NO)RAL_NO, sum(InputData)InputData , sum(InputDataDeductionPer)InputDataDeductionPer , max(param_desc)param_desc , max(param_desc_Ded)param_desc_Ded
            from  ( select TSPL_MRN_Head.VehicleNo, TSPL_LOCATION_MASTER.Location_Code  , TSPL_LOCATION_MASTER.Location_Desc ,  TSPL_QC_CHECK_HEAD.Document_Code, convert (varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as Document_Date ,TSPL_QC_CHECK_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name ,
            TSPL_QC_CHECK_SRN_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc , TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code , convert (varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date , TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code, (case when len(tspl_qc_log_sheet_master.AliasName)>0 then tspl_qc_log_sheet_master.AliasName else tspl_qc_log_sheet_master.description end) as param_desc,
            (case when len(tspl_qc_log_sheet_master.AliasName)>0 then tspl_qc_log_sheet_master.AliasName else tspl_qc_log_sheet_master.description end) + 'Ded' as param_desc_Ded,TSPL_QC_CHECK_SRN_DETAIL.InputData , TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer
            ,TSPL_QC_CHECK_HEAD.Gate_Entry_No,TSPL_QC_CHECK_HEAD.Gate_Entry_Date,TSPL_QC_CHECK_HEAD.QC_Status,TSPL_GRN_HEAD.Ref_No as RAL_NO from TSPL_QC_CHECK_SRN_DETAIL 
            inner join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code
            left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = TSPL_QC_CHECK_SRN_DETAIL.MRN_No and TSPL_MRN_DETAIL.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No = TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code
            left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_DETAIL.GRN_No left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard'
            left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_MASTER.Comp_code left outer join TSPL_MRN_Head on TSPL_MRN_DETAIL.MRN_No = TSPL_MRN_Head.MRN_No
            " & whrcls & "  ) xx  group by Document_Code , QC_Param_Code ) xxx
           PIVOT ( SUM(InputData) FOR param_desc IN (" & paramInputPivot & ") ) AS pivot_input
         PIVOT (sum(InputDataDeductionPer) FOR param_desc_Ded IN (" & paramDedPivot & " ) ) AS pivot_ded  ) xxxx
		  group by Document_Code order by Document_Code "
            Dim dt As DataTable = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableControl(False)
                View()
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next

        gv1.Columns("Location_Code").HeaderText = "Location Code"
        gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"
        gv1.Columns("Item_Code").HeaderText = "Item Code"
        gv1.Columns("RAL_NO").HeaderText = "RAL NO"
        gv1.Columns("Document_Code").HeaderText = "Document Code"
        gv1.Columns("Document_Date").HeaderText = "Document Date"
        gv1.Columns("Weighment_Code").HeaderText = "Weighment Code"
        gv1.Columns("Weighment_Date").HeaderText = "Weighment Date"
        gv1.Columns("VehicleNo").HeaderText = "Vehicle No"

        For ii As Integer = gv1.Columns("Qc").Index + 1 To gv1.Columns("Total Deduction").Index - 1
            gv1.Columns(ii).HeaderText = (gv1.Columns(ii).HeaderText).Replace("Ded", "")
        Next
        gv1.Columns("Qc").IsVisible = False

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("QC Analysis Data"))
                view.ColumnGroups.Add(New GridViewColumnGroup("QC Input Data"))
                view.ColumnGroups.Add(New GridViewColumnGroup("QC Deduction Data"))

                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())

                For col As Integer = 0 To 13
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next

                For col As Integer = 14 To gv1.Columns("Qc").Index - 1
                    view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next

                For col As Integer = gv1.Columns("Qc").Index + 1 To gv1.Columns.Count - 1
                    view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next

                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If clsCommon.myLen(txtLocation.Value) > 0 Then
                arrHeader.Add("Location : " & txtLocation.Value)
            End If

            If clsCommon.myLen(txtItemCode.Value) > 0 Then
                arrHeader.Add("Item : " & txtItemCode.Value)
            End If
            If gv1.Rows.Count > 0 Then
                clsCommon.MyExportToPDF("QC Analysis Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptQCAnalysisReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    arrHeader.Add("Location : " & txtLocation.Value)
                End If
                If clsCommon.myLen(txtVendor.Value) > 0 Then
                    arrHeader.Add("Vendor : " & txtVendor.Value)
                End If
                If clsCommon.myLen(txtItemCode.Value) > 0 Then
                    arrHeader.Add("Item : " & txtItemCode.Value)
                End If
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Try
            Dim obj As clsItemMaster = clsItemMaster.FinderForItem(txtItemCode.Value, "R", isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                txtItemCode.Value = obj.Item_Code
            Else
                txtItemCode.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendor._MYValidating
        Try
            txtVendor.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N'", txtVendor.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRALNo__My_Click(sender As Object, e As EventArgs) Handles txtRALNo._My_Click
        If clsCommon.myLen(txtVendor.Value) <= 0 AndAlso clsCommon.myLen(txtVendor.Value) <= 0 Then
            Dim qry1 As String = "select  tspl_tender_header.DocumentCode as RALNO ,max(tspl_tender_header.DocumentDate) as DocumentDate  from tspl_tender_header
                               left outer join TSPL_TENDER_DETAIL on TSPL_TENDER_DETAIL.DocumentCode=tspl_tender_header.DocumentCode "
            qry1 += " where TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "' group by tspl_tender_header.DocumentCode "
            txtRALNo.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "QCAnalysisRpt", qry1, "RALNO", "", txtRALNo.arrValueMember, Nothing)

        Else
            Dim qry As String = "select  tspl_tender_header.DocumentCode as RALNO ,max(tspl_tender_header.DocumentDate) as DocumentDate  from tspl_tender_header
                            left outer join TSPL_TENDER_DETAIL on TSPL_TENDER_DETAIL.DocumentCode=tspl_tender_header.DocumentCode "
            qry += "where TSPL_TENDER_DETAIL.Vendor_Code = '" + txtVendor.Value + "'
                                 and  TSPL_TENDER_DETAIL.Item_Code = '" + txtItemCode.Value + "'
                                 and TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "' group by tspl_tender_header.DocumentCode "
            txtRALNo.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "QCAnalysisRpt", qry, "RALNO", "", txtRALNo.arrValueMember, Nothing)
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
End Class