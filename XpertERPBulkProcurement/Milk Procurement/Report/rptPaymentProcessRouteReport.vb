Imports System.IO
Imports common


Public Class rptPaymentProcessRouteReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strDocumentCodeDetails As String = Nothing
    Dim strVSPCodeDetails As String = Nothing
    Dim SettPickBulkRoute As Boolean = False
    Dim SettShowMultipleLegers As Boolean = False
    Dim isLoad As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Function setFormId() As String
        Dim fromId As String = ""
        If rdbSummary.IsChecked = True Then
            fromId = MyBase.Form_ID + "_S"
        ElseIf rdbDetails.IsChecked = True OrElse clscommon.myLen(strDocumentCodeDetails) > 0 OrElse clscommon.myLen(strVSPCodeDetails) > 0 Then
            fromId = MyBase.Form_ID + "_D"
            'Else
            '    fromId = MyBase.Form_ID + "_N"
        End If
        Return fromId
    End Function

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        isLoad = True
        SettPickBulkRoute = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickBulkRoute, clsFixedParameterCode.PickBulkRoute, Nothing)) = 1)
        SettShowMultipleLegers = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMultipleLegers, clsFixedParameterCode.ShowMultipleLegers, Nothing)) = 1)
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        dtpDailySummaryToDate.Value = clsCommon.GETSERVERDATE()
        dtpDailySummaryFromDate.Value = dtpDailySummaryToDate.Value.AddMonths(-1)
        dtpLedgerToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select EOMONTH(getdate())")) 'clsCommon.GETSERVERDATE()
        dtpLedgerFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select DATEFROMPARTS(year(getdate()),month(getdate()),'01')")) 'ToDate.Value.AddMonths(-1)
        dtpFromDCS_Ledger.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select EOMONTH(getdate())"))
        dtpToDCS_Ledger.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select DATEFROMPARTS(year(getdate()),month(getdate()),'01')"))
        dtpGainLossFromDate.Value = clsCommon.GETSERVERDATE()
        dtpGainLossToDate.Value = clsCommon.GETSERVERDATE()
        dtpDCSWiseAvgFatSnfToDate.Value = clsCommon.GETSERVERDATE()
        dtpDCSWiseAvgFatSnfFromDate.Value = dtpDCSWiseAvgFatSnfToDate.Value.AddMonths(-1)
        Reset()
        gbSummaryReportType.Visible = False
        lblMCC.Visible = False
        If SettShowMultipleLegers Then
            gbLedger.Visible = True
            btnGo.Visible = False
            btnReset.Visible = False
            RadSplitExp.Visible = False
        Else
            gbLedger.Visible = False
            btnGo.Visible = True
            btnReset.Visible = True
            RadSplitExp.Visible = True
        End If
        isLoad = False
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        If SettShowMultipleLegers = True Then
            txtMonth.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            txtToDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
        End If

        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        btnBack.Visible = False
        txtVSP.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtDocumentNo.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Public Sub LoadData()
        Try
            PageSetupReport_ID = setFormId()
            TemplateGridview = Gv1
            If clsCommon.myLen(txtMcc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select MCC Code first", Me.Text)
                Return
            End If
            If clsCommon.myLen(txtPaymentCycleCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Cycle Code first", Me.Text)
                Return
            End If
            Dim qry As String = ""
            Dim whr As String = "  "
            Dim dt As New DataTable
            Dim strMCCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Location_Code   from TSPL_Location_MASTER where Loc_Segment_Code = '" + txtMcc.Value + "' "))
            Dim strFromDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) as From_Date  from TSPL_PAYMENT_CYCLE_GENERATED where MCC_Code = '" + strMCCCode + "' and name ='" + txtPaymentCycleCode.Value + "'"))
            Dim strToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) as To_Date from TSPL_PAYMENT_CYCLE_GENERATED where MCC_Code = '" + strMCCCode + "' and name ='" + txtPaymentCycleCode.Value + "'"))

            Dim strDeduAddition_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME('(+)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail 
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))

            Dim strDeduAddition_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +' isnull ( '  + QUOTENAME('(+)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) + ',0) as ' + QUOTENAME('(+)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail 
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduAddition_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +' isnull ( '  + QUOTENAME('(+)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) + ',0) '  as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            If clsCommon.myLen(strDeduAddition_Column) > 0 Then
                strDeduAddition_ColumnWithIsNull = " , " + strDeduAddition_ColumnWithIsNull
            End If

            Dim strDeduSubtration_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME('(-)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail 
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  "))
            Dim strDeduSubtration_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +' isnull ( '  + QUOTENAME('(-)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) + ',0) as ' + QUOTENAME('(-)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail 
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduSubtration_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +' isnull ( '  + QUOTENAME('(-)'+ TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode) + ',0) '  as Alies_Name FROM TSPL_MULTIPLE_DEDUCTION_detail
                                                   left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'   
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

            If clsCommon.myLen(strDeduSubtration_Column) > 0 Then
                strDeduSubtration_ColumnWithIsNull = " , " + strDeduSubtration_ColumnWithIsNull
            End If
            Dim strMatrialSaleSubtration_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) as Alies_Name FROM TSPL_SD_SHIPMENT_DETAIL 
                                                   left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                                               left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                                   where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
					                               and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
            Dim strMatrialSaleSubtration_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + 'isnull(' + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) + ',0) as ' + QUOTENAME('(-)'+ TSPL_ITEM_MASTER.Item_Desc)  as Alies_Name FROM TSPL_SD_SHIPMENT_DETAIL 
                                                   left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                                               left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                                   where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
					                               and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
            Dim strMatrialSaleSubtration_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' + 'isnull(' + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) + ',0)  '   as Alies_Name FROM TSPL_SD_SHIPMENT_DETAIL 
                                                   left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                                               left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                                   where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
					                               and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                                                   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))

            If clsCommon.myLen(strMatrialSaleSubtration_Column) > 0 Then
                strMatrialSaleSubtration_ColumnWithIsNull = " , " + strMatrialSaleSubtration_ColumnWithIsNull
            End If
            If chkDetailReportType.Checked = True Then
                qry = " select TSPL_MCC_ROUTE_MASTER.Route_Name as [Route] , SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) as [Soc Val] 
                                   " + strDeduAddition_ColumnWithIsNull + "
                                   " + strDeduSubtration_ColumnWithIsNull + "
                                   " + strMatrialSaleSubtration_ColumnWithIsNull + " 
                                    , SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) " + IIf(clsCommon.myLen(strDeduAddition_ColumnTotal) > 0, " +( " + strDeduAddition_ColumnTotal + ") ", " ") + "  " + IIf(clsCommon.myLen(strDeduSubtration_ColumnTotal) > 0, " -( " + strDeduSubtration_ColumnTotal + ") ", " ") + "   " + IIf(clsCommon.myLen(strMatrialSaleSubtration_ColumnTotal) > 0, " - (" + strMatrialSaleSubtration_ColumnTotal + ") ", "") + "   as [Net Pay]
                                from (
                                select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  SRN_Net_Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                                left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                                where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                                group by Route_Code
                                )XXXFinal 
                                left outer join (select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Commissin from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                                left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                                left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                                where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                                group by TSPL_VLC_MASTER_HEAD.Route_Code) TBL_Commission on   TBL_Commission.Route_Code = XXXFinal.Route_Code

                               left outer join (
                               select Route_Code  " + strDeduAddition_ColumnWithIsNull + "   , " + IIf(clsCommon.myLen(strDeduAddition_Column), " " + strDeduAddition_ColumnTotal + " as DeductionTotal_Add ", " 0 as DeductionTotal_Add ") + "  from (
                               select TSPL_VLC_MASTER_HEAD.Route_Code,  '(+)'+TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                               left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                               where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                               and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                               and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'
                               group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode ) as TBL_Deduction
                                 " + IIf(clsCommon.myLen(strDeduAddition_Column) > 0, " pivot (  sum(Amount) for ded_Code in ( " + strDeduAddition_Column + " ) ) as zpivot ", " ") + " 
                               ) TBL_Deduction_Addition on TBL_Deduction_Addition.Route_Code = XXXFinal.Route_Code


                               left outer join (
                               select Route_Code  " + strDeduSubtration_ColumnWithIsNull + "   , " + IIf(clsCommon.myLen(strDeduSubtration_Column), " " + strDeduSubtration_ColumnTotal + " as DeductionTotal_Subtraction ", " 0 as DeductionTotal_Subtraction ") + "  from (
                               select TSPL_VLC_MASTER_HEAD.Route_Code,  '(-)'+TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                               left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                               where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                               and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                               and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'
                               group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode ) as TBL_Deduction
                                 " + IIf(clsCommon.myLen(strDeduSubtration_Column) > 0, " pivot (  sum(Amount) for ded_Code in ( " + strDeduSubtration_Column + " ) ) as zpivot ", " ") + " 
                               ) TBL_Deduction_Subtraction on TBL_Deduction_Subtraction.Route_Code = XXXFinal.Route_Code

                               left outer join ( select Route_Code  " + strMatrialSaleSubtration_ColumnWithIsNull + ", " + IIf(clsCommon.myLen(strMatrialSaleSubtration_Column) > 0, " " + strMatrialSaleSubtration_ColumnTotal + "   as Total_Matrial_Sale ", " 0 as Total_Matrial_Sale ") + "   from (
                            select TSPL_VLC_MASTER_HEAD.Route_Code,max('(-)'+TSPL_ITEM_MASTER.Item_Desc) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL 
                               left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
                            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                               where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
                and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                            group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            ) XXMatrial 
                            " + IIf(clsCommon.myLen(strMatrialSaleSubtration_Column) > 0, " pivot (  sum(Amount) for Alies_Name in ( " + strMatrialSaleSubtration_Column + " ) ) as zpivot ", " ") + "  
                               ) TBL_MaterialSale on TBL_MaterialSale.Route_Code = XXXFinal.Route_Code
                               left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code = XXXFinal.Route_Code 

                             "
            ElseIf chkSummaryReportType.Checked = True AndAlso chkAddition.Checked = True Then
                qry = " select Name, sum (Amount) as Amount from ( select 'MILK BUY&COM' as Name , sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE                     
                               where  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "'  and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'

                               Union All
                               select 'MILK BUY&COM' as Name, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Amount from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                               left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                               where  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and  TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                               group by TSPL_VLC_MASTER_HEAD.Route_Code

                               Union All
                               select   'MILK BUY&COM'  as Name, sum( case when TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction' then -1 * TSPL_MULTIPLE_DEDUCTION_detail.Amount else TSPL_MULTIPLE_DEDUCTION_detail.Amount end ) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type in ( 'Addition','Deduction') 
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_DEDUCTION_MASTER.Is_Milk = 1
                                                   group by  TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode


                               ) PPP group by Name

                               Union all
                               select   TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode + '(+)' as Name, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                                                   left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                                                   left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                                                   inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                                                   inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                                   left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                                                   where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                                                   and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                                                   and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                                                   and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                                                   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_DEDUCTION_MASTER.Is_Milk = 0
                                                   group by  TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode 

                                "
            ElseIf chkSummaryReportType.Checked = True AndAlso chkAddition.Checked = False Then

                qry = " select 'NET' as [Name]            

                               , sum( SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) " + IIf(clsCommon.myLen(strDeduAddition_ColumnTotal) > 0, " +( " + strDeduAddition_ColumnTotal + ") ", " ") + "  " + IIf(clsCommon.myLen(strDeduSubtration_ColumnTotal) > 0, " -( " + strDeduSubtration_ColumnTotal + ") ", " ") + "   " + IIf(clsCommon.myLen(strMatrialSaleSubtration_ColumnTotal) > 0, " - (" + strMatrialSaleSubtration_ColumnTotal + ") ", "") + "  ) as [Amount]

                               from (

                               select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  SRN_Net_Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                                left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                                where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                                group by Route_Code
                                )XXXFinal 
                                left outer join (select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Commissin from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                                left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                                left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                                where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                                group by TSPL_VLC_MASTER_HEAD.Route_Code) TBL_Commission on   TBL_Commission.Route_Code = XXXFinal.Route_Code

                               left outer join (
                               select Route_Code  " + strDeduAddition_ColumnWithIsNull + "   , " + IIf(clsCommon.myLen(strDeduAddition_Column), " " + strDeduAddition_ColumnTotal + " as DeductionTotal_Add ", " 0 as DeductionTotal_Add ") + "  from (
                               select TSPL_VLC_MASTER_HEAD.Route_Code,  '(+)'+TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                               left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                               where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Addition'
                               and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                               and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'
                               group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode ) as TBL_Deduction
                                 " + IIf(clsCommon.myLen(strDeduAddition_Column) > 0, " pivot (  sum(Amount) for ded_Code in ( " + strDeduAddition_Column + " ) ) as zpivot ", " ") + " 
                               ) TBL_Deduction_Addition on TBL_Deduction_Addition.Route_Code = XXXFinal.Route_Code


                               left outer join (
                               select Route_Code  " + strDeduSubtration_ColumnWithIsNull + "   , " + IIf(clsCommon.myLen(strDeduSubtration_Column), " " + strDeduSubtration_ColumnTotal + " as DeductionTotal_Subtraction ", " 0 as DeductionTotal_Subtraction ") + "  from (
                               select TSPL_VLC_MASTER_HEAD.Route_Code,  '(-)'+TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail 
                               left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                               left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                               left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                               where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                               and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                               and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "'
                               group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode ) as TBL_Deduction
                                 " + IIf(clsCommon.myLen(strDeduSubtration_Column) > 0, " pivot (  sum(Amount) for ded_Code in ( " + strDeduSubtration_Column + " ) ) as zpivot ", " ") + " 
                               ) TBL_Deduction_Subtraction on TBL_Deduction_Subtraction.Route_Code = XXXFinal.Route_Code

                               left outer join ( select Route_Code  " + strMatrialSaleSubtration_ColumnWithIsNull + ", " + IIf(clsCommon.myLen(strMatrialSaleSubtration_Column) > 0, " " + strMatrialSaleSubtration_ColumnTotal + "   as Total_Matrial_Sale ", " 0 as Total_Matrial_Sale ") + "   from (
                            select TSPL_VLC_MASTER_HEAD.Route_Code,max('(-)'+TSPL_ITEM_MASTER.Item_Desc) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL 
                               left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
                            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                               inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                               inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                               left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                               where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
                               and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                               and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                               and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                               and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                            group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_SD_SHIPMENT_DETAIL.Item_Code
                            ) XXMatrial 
                            " + IIf(clsCommon.myLen(strMatrialSaleSubtration_Column) > 0, " pivot (  sum(Amount) for Alies_Name in ( " + strMatrialSaleSubtration_Column + " ) ) as zpivot ", " ") + "  
                               ) TBL_MaterialSale on TBL_MaterialSale.Route_Code = XXXFinal.Route_Code
                               left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code = XXXFinal.Route_Code 
 

                              Union All
                              select Name, sum(Amount) as Amount from (
                              select max(case when TSPL_DEDUCTION_MASTER.Is_Milk = 1 then 'MILK' when .TSPL_DEDUCTION_MASTER.Is_Feed = 1 then 'FEED' when TSPL_DEDUCTION_MASTER.Is_Ghee = 1 then 'Ghee' else  TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode  end)   as Name, sum(TSPL_MULTIPLE_DEDUCTION_detail.Amount) as Amount  from TSPL_MULTIPLE_DEDUCTION_detail
                              left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_detail.Document_No = TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No
                              left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode
                              left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_CODE
                              inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                              inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                              left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)

                              where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1  and TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type = 'Deduction'
                              and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + strFromDate + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + strToDate + "'),103)
                              and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code = '" + txtMcc.Value + "' 
                              and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                              and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                              and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and  TSPL_DEDUCTION_MASTER.Is_Milk = 0
                              group by  TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode  

                              Union All
                              select max(case when TSPL_ITEM_MASTER.Structure_Code = 'Milk' then 'MILK'  when TSPL_ITEM_MASTER.Structure_Code = 'GHEE' then 'GHEE' when TSPL_ITEM_MASTER.Structure_Code = 'FEED' then 'FEED' else TSPL_ITEM_MASTER.Item_Desc  end) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL
                              left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                          left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                          left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                              inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                              inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                              left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                              where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' 
					          and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103)
                              and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code = '" + txtMcc.Value + "' 
                              and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                              and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' 
                              group by  TSPL_SD_SHIPMENT_DETAIL.Item_Code                             

                              ) as TBL_SECOND group by TBL_SECOND.Name

                            "

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

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"

                '================================
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim summaryDr As New GridViewSummaryItem()
                'If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                '    summaryDr.Name = "Dr"
                '    summaryDr.AggregateExpression = "sum(Dr) - sum(DrPE)"
                '    summaryRowItem.Add(summaryDr)
                'Else
                '    Dim itemDr As New GridViewSummaryItem("Dr", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemDr)
                'End If

                'Dim itemCr As New GridViewSummaryItem("Cr", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemCr)
                'If rdbSummary.IsChecked = True Then
                '    Dim itemPayableAmt As New GridViewSummaryItem("Payable Amt", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemPayableAmt)
                'End If
                ''================================

                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom


                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 1 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)

                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData_Old()
        Try
            PageSetupReport_ID = setFormId()
            TemplateGridview = Gv1
            If clsCommon.myLen(txtMcc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select MCC Code first", Me.Text)
                Return
            End If
            If clsCommon.myLen(txtPaymentCycleCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Cycle Code first", Me.Text)
                Return
            End If
            Dim qry As String = ""
            Dim whr As String = "  "
            Dim dt As New DataTable

            Dim strDeduAddition_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME('(+)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0  and TSPL_DEDUCTION_MASTER.Is_Addition = 1   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduAddition_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +' isnull ( '  + QUOTENAME('(+)'+ TSPL_DEDUCTION_MASTER.code) + ',0) as ' + QUOTENAME('(+)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 1   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduAddition_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +' isnull ( '  + QUOTENAME('(+)'+ TSPL_DEDUCTION_MASTER.code) + ',0) '  as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 1   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

            Dim strDeduSubtration_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduSubtration_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + 'isnull ('  + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) + ',0 ) as ' +  QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0     FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strDeduSubtration_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' + ' isnull ('  + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) + ',0 )  '  as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

            Dim strPashuVikasSubtration_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strPashuVikasSubtration_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','+' isnull('  + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) + ',0) as ' + QUOTENAME('(-)'+ TSPL_DEDUCTION_MASTER.code) as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            Dim strPashuVikasSubtration_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+'+' isnull('  + QUOTENAME( '(-)'+ TSPL_DEDUCTION_MASTER.code) + ',0)  '  as Alies_Name FROM TSPL_DEDUCTION_MASTER where TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

            Dim strMatrialSaleSubtration_Column As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ','  + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) as Alies_Name FROM TSPL_ITEM_MASTER where 2=2    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
            Dim strMatrialSaleSubtration_ColumnWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + 'isnull(' + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) + ',0) as ' + QUOTENAME('(-)'+ TSPL_ITEM_MASTER.Item_Desc)  as Alies_Name FROM TSPL_ITEM_MASTER where 2=2    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
            Dim strMatrialSaleSubtration_ColumnTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' + 'isnull(' + QUOTENAME( '(-)'+ TSPL_ITEM_MASTER.Item_Desc) + ',0)  '   as Alies_Name FROM TSPL_ITEM_MASTER where 2=2    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))

            Dim strMCCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Location_Code   from TSPL_Location_MASTER where Loc_Segment_Code = '" + txtMcc.Value + "' "))
            Dim strFromDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) as From_Date  from TSPL_PAYMENT_CYCLE_GENERATED where MCC_Code = '" + strMCCCode + "' and name ='" + txtPaymentCycleCode.Value + "'"))
            Dim strToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) as To_Date from TSPL_PAYMENT_CYCLE_GENERATED where MCC_Code = '" + strMCCCode + "' and name ='" + txtPaymentCycleCode.Value + "'"))
            If chkDetailReportType.Checked = True Then


                qry = " select TSPL_MCC_ROUTE_MASTER.Route_Name as [Route], SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) as [Soc Val]
                    ," + strDeduAddition_ColumnWithIsNull + ",

                     " + strDeduSubtration_ColumnWithIsNull + "
                     , " + strMatrialSaleSubtration_ColumnWithIsNull + "
                     ," + strPashuVikasSubtration_ColumnWithIsNull + " 
                     
 
                     , SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) +( " + strDeduAddition_ColumnTotal + ") - (" + strDeduSubtration_ColumnTotal + ") - (" + strPashuVikasSubtration_ColumnTotal + ") - (" + strMatrialSaleSubtration_ColumnTotal + ") as [Net Pay]

                     from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  SRN_Net_Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by Route_Code
                     )XXXFinal 
                    left outer join (select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Commissin from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					 left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code) TBL_Commission on   TBL_Commission.Route_Code = XXXFinal.Route_Code
                    left outer join (
                    select Route_Code , " + strDeduAddition_ColumnWithIsNull + " , " + strDeduAddition_ColumnTotal + " as DeductionTotal_Add from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code,  '(+)'+TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 1
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in ( " + strDeduAddition_Column + " ) ) as zpivot
                    ) TBL_Deduction_Addition on TBL_Deduction_Addition.Route_Code = XXXFinal.Route_Code

 

                    left outer join (
                    select Route_Code , " + strDeduSubtration_ColumnWithIsNull + " , " + strDeduSubtration_ColumnTotal + "   as DeductionTotal_Subtraction from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code,  '(-)'+TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0
                      where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                    group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in (" + strDeduSubtration_Column + ") ) as zpivot
                    ) TBL_Deduction_Subtraction on TBL_Deduction_Subtraction.Route_Code = XXXFinal.Route_Code



                    left outer join (
                    select Route_Code ,  " + strPashuVikasSubtration_ColumnWithIsNull + ", " + strPashuVikasSubtration_ColumnTotal + "    as PashuVikasTotal_Subtraction from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code, '(-)'+ TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code , sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                     left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                    inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in ( " + strPashuVikasSubtration_Column + " ) ) as zpivot
                    ) TBL_Deduction_PashuVikas on TBL_Deduction_PashuVikas.Route_Code = XXXFinal.Route_Code
                    
                    left outer join ( select Route_Code, " + strMatrialSaleSubtration_ColumnWithIsNull + ", " + strMatrialSaleSubtration_ColumnTotal + "   as Total_Matrial_Sale from (
	                select TSPL_VLC_MASTER_HEAD.Route_Code,max('(-)'+TSPL_ITEM_MASTER.Item_Desc) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103) 
	                group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_SD_SHIPMENT_DETAIL.Item_Code
	                ) XXMatrial 
	                pivot (  sum(Amount) for Alies_Name in ( " + strMatrialSaleSubtration_Column + " ) ) as zpivot) TBL_MaterialSale on TBL_MaterialSale.Route_Code = XXXFinal.Route_Code
                    left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code = XXXFinal.Route_Code 

                  "
            ElseIf chkSummaryReportType.Checked = True AndAlso chkAddition.Checked = True Then
                qry = " select Name, sum (Amount) as Amount from ( select 'MILK BUY&COM' as Name , sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE                     
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "'  and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     
                     Union All
                     select 'MILK BUY&COM' as Name, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Amount from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					 left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code
                     ) PPP group by Name

                     Union all
					
                     select   TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code + '(+)' as Name, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 1
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "'  and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by  TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code   

                      "
            ElseIf chkSummaryReportType.Checked = True AndAlso chkAddition.Checked = False Then

                qry = " select 'NET' as [Name]            
                     
                     , sum( SRN_Net_Amount + isnull(TBL_Commission.Commissin,0) +( " + strDeduAddition_ColumnTotal + ") - (" + strDeduSubtration_ColumnTotal + ") - (" + strPashuVikasSubtration_ColumnTotal + ") - (" + strMatrialSaleSubtration_ColumnTotal + ") ) as [Amount]

                     from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_DETAIL.SRN_Net_Amount) as  SRN_Net_Amount from TSPL_PAYMENT_PROCESS_DETAIL left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code
                     )XXXFinal 

                     left outer join (select TSPL_VLC_MASTER_HEAD.Route_Code, sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as  Commissin from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					 left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "' and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code) TBL_Commission on   TBL_Commission.Route_Code = XXXFinal.Route_Code

                    left outer join (
                    select Route_Code , " + strDeduAddition_ColumnWithIsNull + " , " + strDeduAddition_ColumnTotal + " as DeductionTotal_Add from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code,  '(+)'+TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 1
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in ( " + strDeduAddition_Column + " ) ) as zpivot
                    ) TBL_Deduction_Addition on TBL_Deduction_Addition.Route_Code = XXXFinal.Route_Code

 

                    left outer join (
                    select Route_Code , " + strDeduSubtration_ColumnWithIsNull + " , " + strDeduSubtration_ColumnTotal + "   as DeductionTotal_Subtraction from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code,  '(-)'+TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0
                      where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                    group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in (" + strDeduSubtration_Column + ") ) as zpivot
                    ) TBL_Deduction_Subtraction on TBL_Deduction_Subtraction.Route_Code = XXXFinal.Route_Code



                    left outer join (
                    select Route_Code ,  " + strPashuVikasSubtration_ColumnWithIsNull + ", " + strPashuVikasSubtration_ColumnTotal + "    as PashuVikasTotal_Subtraction from (
                     select TSPL_VLC_MASTER_HEAD.Route_Code, '(-)'+ TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as Ded_Code , sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                     left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                     left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                    inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code) as TBL_Deduction
                     pivot (  sum(Amount) for ded_Code in ( " + strPashuVikasSubtration_Column + " ) ) as zpivot
                    ) TBL_Deduction_PashuVikas on TBL_Deduction_PashuVikas.Route_Code = XXXFinal.Route_Code
                    
                    left outer join ( select Route_Code, " + strMatrialSaleSubtration_ColumnWithIsNull + ", " + strMatrialSaleSubtration_ColumnTotal + "   as Total_Matrial_Sale from (
	                select TSPL_VLC_MASTER_HEAD.Route_Code,max('(-)'+TSPL_ITEM_MASTER.Item_Desc) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103) 
	                group by TSPL_VLC_MASTER_HEAD.Route_Code, TSPL_SD_SHIPMENT_DETAIL.Item_Code
	                ) XXMatrial 
	                pivot (  sum(Amount) for Alies_Name in ( " + strMatrialSaleSubtration_Column + " ) ) as zpivot) TBL_MaterialSale on TBL_MaterialSale.Route_Code = XXXFinal.Route_Code
                    left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code = XXXFinal.Route_Code 

                    Union All
                    select Name, sum(Amount) as Amount from (
                    select max(case when TSPL_DEDUCTION_MASTER.Is_Milk = 1 then 'MILK' when .TSPL_DEDUCTION_MASTER.Is_Feed = 1 then 'FEED' when TSPL_DEDUCTION_MASTER.Is_Ghee = 1 then 'Ghee' else  TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code  end)   as Name, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=0 and TSPL_DEDUCTION_MASTER.Is_Addition = 0
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "'  and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by  TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code  

                    Union All
                    select max(case when TSPL_ITEM_MASTER.Structure_Code = 'Milk' then 'MILK'  when TSPL_ITEM_MASTER.Structure_Code = 'GHEE' then 'GHEE' when TSPL_ITEM_MASTER.Structure_Code = 'FEED' then 'FEED' else TSPL_ITEM_MASTER.Item_Desc  end) as Alies_Name ,sum(TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount from TSPL_SD_SHIPMENT_DETAIL left outer join  TSPL_SD_SHIPMENT_HEAD  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
	                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
	                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    where TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" + strMCCCode + "' and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert (date,'" + strFromDate + "',103) and convert (date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date,'" + strToDate + "',103) 
	                group by  TSPL_SD_SHIPMENT_DETAIL.Item_Code   

                    Union All
                    select  max( case when TSPL_DEDUCTION_MASTER.Is_Milk = 1 then 'MILK' when .TSPL_DEDUCTION_MASTER.Is_Feed = 1 then 'FEED' when TSPL_DEDUCTION_MASTER.Is_Ghee = 1 then 'Ghee' else  TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code  end) as Name, sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount  from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert (varchar,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) = convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)
                     inner join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                     and TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos=1 
                     where TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code  = '" + txtMcc.Value + "'  and TSPL_PAYMENT_CYCLE_GENERATED.Name = '" + txtPaymentCycleCode.Value + "' and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = '" + strMCCCode + "'
                     group by  TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code 
                    ) as TBL_SECOND group by TBL_SECOND.Name

                  "

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

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"

                '================================
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim summaryDr As New GridViewSummaryItem()
                'If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                '    summaryDr.Name = "Dr"
                '    summaryDr.AggregateExpression = "sum(Dr) - sum(DrPE)"
                '    summaryRowItem.Add(summaryDr)
                'Else
                '    Dim itemDr As New GridViewSummaryItem("Dr", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemDr)
                'End If

                'Dim itemCr As New GridViewSummaryItem("Cr", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemCr)
                'If rdbSummary.IsChecked = True Then
                '    Dim itemPayableAmt As New GridViewSummaryItem("Payable Amt", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemPayableAmt)
                'End If
                ''================================

                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom


                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 1 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)

                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
            Gv1.BestFitColumns()
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
            If clsCommon.myLen(setFormId()) > 0 Then
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtDocumentNo._My_Click

        Dim str As String = ""
        Dim qry As String = ""
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1) = True Then
            qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [Plant Code],TSPL_GL_SEGMENT_CODE.description as [Plant Name], isnull (TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected,'') as [MCC Code]  , isnull (TSPL_MCC_MASTER.MCC_NAME,'') as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] " &
                  " From TSPL_PAYMENT_PROCESS_HEAD left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code " &
                  " left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code " &
                  " left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected where FarmType='PP'  "
        Else
            qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_GL_SEGMENT_CODE.description as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] " &
                  " From TSPL_PAYMENT_PROCESS_HEAD left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code " &
                  " left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code  where FarmType='PP' " &
                  "  "
        End If

        If rbFromDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        ElseIf rbToDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        ElseIf rbBothFromToDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)   and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        End If

        txtDocumentNo.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@DocNo", qry, "DocumentNo", "DocumentNo", txtDocumentNo.arrValueMember, txtDocumentNo.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Dim qry As String
        qry = "  select Cust_Code as 'Code' , Customer_Name as Name from TSPL_CUSTOMER_MASTER where Cust_Group_Code = 'VSP'  order by Cust_Code "
        txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@VSP", qry, "Code", "Name", txtVSP.arrValueMember, txtVSP.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Name],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country  from TSPL_Location_MASTER   where    Rejected_Type='N' and Location_Category='MCC' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@LocFinder", qry, "LocationSegmentCode", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = setFormId()
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
        clsGridLayout.DeleteData(setFormId(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If clsCommon.myLen(txtPaymentCycleCode.Value) > 0 Then
                arrHeader.Add("Cycle Code : " + txtPaymentCycleCode.Value)
            End If
            If clsCommon.myLen(txtMcc.Value) > 0 Then
                arrHeader.Add("Location : " + txtMcc.Value)
            End If

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            'If txtVSP.arrDispalyMember IsNot Nothing AndAlso txtVSP.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrDispalyMember))
            'End If
            'If txtDocumentNo.arrDispalyMember IsNot Nothing AndAlso txtDocumentNo.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocumentNo.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Route Payment Process Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Route Payment Process Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        'Try
        '    If rdbSummary.IsChecked = True AndAlso clsCommon.myLen(strDocumentCodeDetails) <= 0 Then
        '        strDocumentCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("Document No").Value)
        '        strVSPCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("VSP Code").Value)
        '        LoadData(strDocumentCodeDetails, strVSPCodeDetails)
        '        btnBack.Visible = True
        '        btnGo.Enabled = False
        '        Enabledisablecontrol(False)
        '        'strDocumentCodeDetails = ""
        '        'strVSPCodeDetails = ""
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        'End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Dim strDocumentCodeDetails As String = ""
        'Dim strVSPCodeDetails As String = ""
        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        LoadData()
        btnBack.Visible = False
        btnGo.Enabled = True
        Enabledisablecontrol(True)
    End Sub

    Private Sub rbFromDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbFromDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Sub ControlEmpty()

        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        btnBack.Visible = False
        'txtVSP.arrValueMember = Nothing
        'txtVSP.arrDispalyMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtLocation.arrDispalyMember = Nothing
        txtDocumentNo.arrValueMember = Nothing
        txtDocumentNo.arrDispalyMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rbToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbBothFromToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbBothFromToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbNone_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbNone.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles fromDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ToDate_ValueChanged(sender As Object, e As EventArgs) Handles ToDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Public Sub Enabledisablecontrol(ByVal isEnable As Boolean)
        gbDateRangeApply.Enabled = isEnable
        RadGroupBox3.Enabled = isEnable
        RadGroupBox2.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtLocation.Enabled = isEnable
        txtVSP.Enabled = isEnable
        txtDocumentNo.Enabled = isEnable
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub rdbDetails_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbDetails.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub chkBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBoth.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkUnPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUnPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub txtPaymentCycleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleCode._MYValidating
        Try
            If clsCommon.myLen(txtMcc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select MCC first..")
                Return
            End If
            Dim strMccCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Location_Code from  TSPL_Location_MASTER where Loc_Segment_Code = '" + txtMcc.Value + "'"))
            Dim qry As String = " select Name as Code ,MCC_Code  ,Fiscal_Code , convert (varchar, From_Date,103) as [From Date] , convert (varchar,To_Date,103) as [To Date] from TSPL_PAYMENT_CYCLE_GENERATED "
            txtPaymentCycleCode.Value = clsCommon.ShowSelectForm("Route@paymentCycleReport", qry, "Code", " MCC_Code = '" + strMccCode + "' ", txtPaymentCycleCode.Value, "Code", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMcc._MYValidating
        Try
            Dim qry As String = " select  Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code   from TSPL_Location_MASTER      "
            txtMcc.Value = clsCommon.ShowSelectForm("Route@MCCFinder@PCR", qry, "Loc_Segment_Code", " 1=1  and   Rejected_Type='N' and Location_Category='MCC' ", txtMcc.Value, "Loc_Segment_Code", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkSummaryReportType_CheckedChanged(sender As Object, e As EventArgs) Handles chkSummaryReportType.CheckedChanged
        If chkSummaryReportType.Checked = True Then
            gbSummaryReportType.Visible = True
        Else
            gbSummaryReportType.Visible = False
        End If
    End Sub

    Private Sub chkOther_CheckedChanged(sender As Object, e As EventArgs) Handles chkOther.CheckedChanged
        'If chkOther.Checked = True Then
        '    gbSummaryReportType.Visible = False
        'Else
        '    gbSummaryReportType.Visible = True
        'End If
    End Sub

    Private Sub btnLedgerPrint_Click(sender As Object, e As EventArgs) Handles btnLedgerPrint.Click
        Try
            Load_Report_Paymnet_RCDF()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Load_Report_Paymnet_RCDF()
        Dim companyADD, CompName, CompCode As String

        Dim sQuery As String = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = dtpLedgerFromDate.Value
        Dim Todate As String = dtpLedgerToDate.Value


        Dim whrcls As String = " where 2=2 "
        Dim whrcls1 As String = " where 2=2 "
        Dim whrclsItemWise As String = " where 2=2 "

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpLedgerToDate.Value + "'),103) "
        'whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        'If clsCommon.myLen(fndLoc.Value) > 0 Then
        '    whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        'End If
        whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpLedgerToDate.Value + "'),103)   " ' and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
        'whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        'whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        'If clsCommon.myLen(fndLoc.Value) > 0 Then
        '    whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        'End If
        'whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpLedgerToDate.Value + "'),103) "
        Dim strCompanyCityName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"))
        Dim strPC_FATValue As String = 0
        Dim strPC_SNFValue As String = 0
        Dim dtPC_FAT_SNF As DataTable = clsDBFuncationality.GetDataTable("select  top 1 round ( Price_Chart_FAT_Ratio * Price_Chart_Rate / nullif (Price_Chart_FAT_Per,0) , 2,1 ) as FAT_PCValue , round (  Price_Chart_SNF_Ratio * Price_Chart_Rate / nullif (Price_Chart_SNF_Per,0),2,1)  as SNF_PCValue  from TSPL_PRICE_CHART_PLANNING order by convert (date, Planning_Date,103)")
        If dtPC_FAT_SNF IsNot Nothing AndAlso dtPC_FAT_SNF.Rows.Count > 0 Then
            strPC_FATValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("FAT_PCValue"))
            strPC_SNFValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("SNF_PCValue"))
        End If
        Dim CycleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select max(Name) as CycleNo from TSPL_PAYMENT_CYCLE_GENERATED where convert(varchar, From_Date,103) = convert(varchar, '" + dtpLedgerFromDate.Value + "',103) "))
        Dim BaseQry As String = ""
        BaseQry = "select '" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue, "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode, /* TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2, */ coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
        'If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
        '    BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        'Else
        BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        'End If
        BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
, case when isnull (TSPL_MILK_REJECT_DETAIL.Reject_Type,'') = '' then  'SWEET' else upper (TSPL_MILK_REJECT_DETAIL.Reject_Type) end  as QBD " + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
        BaseQry += " inner join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty   
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo , convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
		  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + fromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + Todate + "'),103)  ) as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE =
          TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  
          "
        BaseQry += "  " & whrcls & " " ' where Doc_No = '" + fndDocNo.Value + "'
        Dim dt As New DataTable
        sQuery = BaseQry '+ " order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"

        Dim legerMainQuery As String = " select '" + strCompanyCityName + "' as strCompanyCityName , max(fromDate) as fromDate,max(Todate) as Todate,  XXXFinal.ROUTE_CODE , Route_Name , VSP_CODE, max(Vendor_Name) as Vendor_Name , max(Type) as Type ,sum( Qty) as Qty , sum(case when QBD = 'SWEET' then   Qty else 0 end) as SweetQty ,sum(case when QBD = 'CURD' then   Qty else 0 end) as CurdQty , sum(case when QBD = 'SOUR' then   Qty else 0 end) as SourQty , sum(FATQTY) * 100 / sum( Qty)  as FAT_PER , sum(SNFQTY) * 100 / sum( Qty) as SNF_PER, sum(FATQTY) as  FATQTY, sum(SNFQTY) as SNFQTY, sum (SRN_Net_Amount) as SRN_Net_Amount, CowBuffalo_Type,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader from (  " + sQuery + " ) XXXFinal group by  XXXFinal.ROUTE_CODE , Route_Name , VSP_CODE , CowBuffalo_Type "
        dt = clsDBFuncationality.GetDataTable(legerMainQuery)

        '        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
        '"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
        '"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
        '"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission' " + Environment.NewLine + ' and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')
        '"union all " + Environment.NewLine +
        '"select case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
        '" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
        '"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
        '" where  (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine + ' TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and
        '")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        '        " ) as final " + Environment.NewLine +
        '        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        '        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        '        sQuery += " " & whrclsItemWise & " "
        '        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)





        '        sQuery = " select * from ( select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount , AP_Invoice_Date,  isnull(TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos,0) as Is_Default_Pashu_Vikash_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code from (" + Environment.NewLine +
        '        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,(item_code) as	item_code	, max(Item_Desc) as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF , max( AP_Invoice_Date) as AP_Invoice_Date from(" + Environment.NewLine +
        '        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF  , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date,103) else '' end as AP_Invoice_Date  " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
        '"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code   /* where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  =  */ " + Environment.NewLine +
        '")xxx group by customer_code,item_code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date,103) else '' end as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
        '"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
        '"union all
        'select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,  '' as AP_Invoice_Date from(
        'select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
        'from TSPL_PAYMENT_PROCESS_ASSET_LOST 
        ' /* where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  =  */
        ')xxx group by Vendor_Code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF , '' as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF  , '' as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF, AP_Invoice_Date  from( " + Environment.NewLine +
        '"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
        '"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, convert (varchar,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date,103) as AP_Invoice_Date   " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
        '"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where /* TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no =   and  */ not ( RefDocType='MILK-REJ')" + Environment.NewLine +     ' TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or
        '")xxx group by RefDocType,Vendor_CODE,Item_Desc,AP_Invoice_Date " + Environment.NewLine
        '        sQuery += " ) as final "
        '        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        '        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE

        '         left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Description=final.Item_Desc
        '         left outer join (select distinct VSP_Code ,VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = Item_Code

        '         "
        '        sQuery += " " & whrclsItemWise & "  Union All  "
        '        sQuery += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
        '                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
        '                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
        '                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
        '                    where /* TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no in () and  */
        '                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
        '                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' 
        '                    ) Final order by Is_Default_Pashu_Vikash_Kos desc , trans_type desc "
        '        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        'sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        '"left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        '"where /* TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no =  and */ RefDocType='MILK-REJ'"
        'Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        '        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
        '",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull(TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
        '"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
        '" /* where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  =  */ "
        '        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        '        sQuery = " select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME, Final.Item_Desc as Addition , sum(Amount) as [Amount]  from (
        'select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
        '                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
        '                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
        '                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
        '					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
        '                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpLedgerToDate.Value + "'),103)  and
        '                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
        '                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' ) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc "

        Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + dtpLedgerToDate.Value + "'),103)"))
        sQuery = "SELECT ZZ.VSP_Uploader_Code,ZZ.VSP_Code,ZZ.Vendor_NAME,ZZ.Addition,SUM(ZZ.Amount) AS Amount
                 FROM(
                select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME,Final.Item_Desc as Addition, sum(Amount) as [Amount]  from (
                select TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, 
                case when isnull(TSPL_MULTIPLE_DEDUCTION_head.trans_type,'')='Addition' then TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description  end as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
                ,TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
                left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No in ('" + strDocNo + "') 
                ) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc 
                union all
                SELECT TT.VSP_Uploader_Code,TT.VSP_Code,TT.Vendor_NAME,coalesce (mapping.mmDescription, TT.Addition) AS Addition
                 ,TT.Amount
                 FROM (
                select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,'' as Vendor_NAME,TSPL_DCS_ADDITION_DEDUCTION.Description as Addition,
                TSPL_DCS_ADDITION_DEDUCTION.Code,
                (TSPL_VENDOR_INVOICE_HEAD.Document_Total) as [Amount]  from TSPL_PAYMENT_PROCESS_SAVING 
                left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
                left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE
                left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                where  TSPL_PAYMENT_PROCESS_SAVING.Doc_No in  ('" + strDocNo + "') 
                )TT
                left join
                (select MAPPING.Code mmCode,MAPPING.Description mmDescription,DEDUCTION.CODE AS ddCode from TSPL_DCS_ADDITION_DEDUCTION as MAPPING
                 left join TSPL_DCS_ADDITION_DEDUCTION as DEDUCTION
                 on  DEDUCTION.Code=MAPPING.MappingCode
                 WHERE  len(isnull(MAPPING.MappingCode,''))>0)mapping on mapping.ddCode=TT.Code
                union all
                select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,'' as Vendor_NAME
                 ,'Milk Purchase Expense' as Addition,Balance_Amt as Amount from 
                 TSPL_PAYMENT_PROCESS_DETAIL INNER JOIN
                 TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No
                 =TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                 left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE
                 where Document_Type='C' and RefDocType='Milk_HE'  
                 and Balance_Amt<>0 AND TSPL_PAYMENT_PROCESS_DETAIL.Doc_No in ('" + strDocNo + "')
                union all
                SELECT TT.VSP_Uploader_Code,TT.VSP_Code,TT.Vendor_NAME,coalesce (mapping.mmDescription, TT.Addition) AS Addition
                ,TT.Amount FROM (
                select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,'' as Vendor_NAME
                ,TSPL_DCS_ADDITION_DEDUCTION.Description as Addition,TSPL_DCS_ADDITION_DEDUCTION.Code,
                (TSPL_VENDOR_INVOICE_HEAD.Document_Total) as [Amount] 
                from TSPL_PAYMENT_PROCESS_COMPULSORY left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_COMPULSORY.AP_Invoice_No 
                left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_CODE
                left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                where TSPL_PAYMENT_PROCESS_COMPULSORY.Doc_No in ('" + strDocNo + "') 
                 )TT
                left join
                (select MAPPING.Code mmCode,MAPPING.Description mmDescription,DEDUCTION.CODE AS ddCode from TSPL_DCS_ADDITION_DEDUCTION as MAPPING
                left join TSPL_DCS_ADDITION_DEDUCTION as DEDUCTION
                on  DEDUCTION.Code=MAPPING.MappingCode
                 WHERE  len(isnull(MAPPING.MappingCode,''))>0)mapping on mapping.ddCode=TT.Code
                 )ZZ GROUP BY ZZ.VSP_Uploader_Code,ZZ.VSP_Code,ZZ.Vendor_NAME,ZZ.Addition"
        Dim dtAddition As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = " select Final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code , Final.Ded_Desc , sum(Amount) as [Amount] from (
select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code , TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc , TSPL_PAYMENT_PROCESS_DEDUCTION.Amount from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpLedgerFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpLedgerToDate.Value + "'),103) 
) Final group by  final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code,Final.Ded_Desc  "
        Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable(sQuery)


        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtAddition, "crptPaymentProcessLeger", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeduction)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub

    Private Sub Load_DCS_Ledger_Report_RCDF()
        Dim companyADD, CompName, CompCode As String
        Dim sQueryDH As String = ""
        Dim sQueryAH As String = ""
        Dim sQueryDD As String = ""
        Dim sQueryAD As String = ""
        Dim sQuery As String = ""
        Dim RowCount As Integer = 0
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = dtpFromDCS_Ledger.Value
        Dim Todate As String = dtpToDCS_Ledger.Value


        Dim whrcls As String = " where 2=2 "
        Dim whrcls1 As String = " where 2=2 "
        Dim whrclsItemWise As String = " where 2=2 "

        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) "
        'whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        'If clsCommon.myLen(fndLoc.Value) > 0 Then
        '    whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
        'End If
        whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1  "
        'whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        'whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
        'If clsCommon.myLen(fndLoc.Value) > 0 Then
        '    whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
        'End If
        'whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
        whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) "
        Dim strCompanyCityName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"))
        Dim strPC_FATValue As String = 0
        Dim strPC_SNFValue As String = 0
        Dim dtPC_FAT_SNF As DataTable = clsDBFuncationality.GetDataTable("select  top 1 round ( Price_Chart_FAT_Ratio * Price_Chart_Rate / nullif (Price_Chart_FAT_Per,0) , 2,1 ) as FAT_PCValue , round (  Price_Chart_SNF_Ratio * Price_Chart_Rate / nullif (Price_Chart_SNF_Per,0),2,1)  as SNF_PCValue  from TSPL_PRICE_CHART_PLANNING order by convert (date, Planning_Date,103)")
        If dtPC_FAT_SNF IsNot Nothing Or dtPC_FAT_SNF.Rows.Count > 0 Then
            strPC_FATValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("FAT_PCValue"))
            strPC_SNFValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("SNF_PCValue"))
        End If
        Dim CycleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select max(Name) as CycleNo from TSPL_PAYMENT_CYCLE_GENERATED where convert(varchar, From_Date,103) = convert(varchar, '" + dtpFromDCS_Ledger.Value + "',103) "))
        Dim BaseQry As String = ""
        BaseQry = "select '" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue, "
        BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode, /* TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2, */ coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
        BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
        ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
        ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
        BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
        'If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
        '    BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
        'Else
        BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
        'End If
        BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
, case when isnull (TSPL_MILK_REJECT_DETAIL.Reject_Type,'') = '' then  'SWEET' else upper (TSPL_MILK_REJECT_DETAIL.Reject_Type) end  as QBD " + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
        BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
        BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
        BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
        BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
        BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
        BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
        BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
        BaseQry += " inner join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
        BaseQry += " ) as PaymentProcess on "
        BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine
        BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
        BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
        BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
        " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty   
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo , convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
		  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + fromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + Todate + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 ) as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE =
          TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  
          "
        BaseQry += "  " & whrcls & " " ' where Doc_No = '" + fndDocNo.Value + "'
        Dim dt As New DataTable
        sQuery = BaseQry '+ " order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"

        Dim legerMainQuery As String = " select '" + strCompanyCityName + "' as strCompanyCityName , max(fromDate) as fromDate,max(Todate) as Todate,  XXXFinal.ROUTE_CODE , Route_Name , VSP_CODE, max(Vendor_Name) as Vendor_Name , max(Type) as Type ,sum( Qty) as Qty , sum(case when QBD = 'SWEET' then   Qty else 0 end) as SweetQty ,sum(case when QBD = 'CURD' then   Qty else 0 end) as CurdQty , sum(case when QBD = 'SOUR' then   Qty else 0 end) as SourQty , sum(FATQTY) * 100 / sum( Qty)  as FAT_PER , sum(SNFQTY) * 100 / sum( Qty) as SNF_PER, sum(FATQTY) as  FATQTY, sum(SNFQTY) as SNFQTY, sum (SRN_Net_Amount) as SRN_Net_Amount, CowBuffalo_Type,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader from (  " + sQuery + " ) XXXFinal group by  XXXFinal.ROUTE_CODE , Route_Name , VSP_CODE , CowBuffalo_Type "
        dt = clsDBFuncationality.GetDataTable(legerMainQuery)

        '        sQuery = "select Vendor_CODE as VSP_Code,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0) as Amount from (" + Environment.NewLine +
        '"select doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from( " + Environment.NewLine +
        '"select case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No" + Environment.NewLine +
        '"where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.Description = 'AP Credit Note For VSP Commission' " + Environment.NewLine + ' and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no  = ('" + fndDocNo.Value + "')
        '"union all " + Environment.NewLine +
        '"select case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
        '" TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo" + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
        '"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine +
        '" where  (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or RefDocType='MILK-REJ')" + Environment.NewLine + ' TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no = ('" + fndDocNo.Value + "') and
        '")xxx group by doc_no, Vendor_CODE,SNo,trans_type" + Environment.NewLine +
        '        " ) as final " + Environment.NewLine +
        '        " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        '        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Vendor_CODE"
        '        sQuery += " " & whrclsItemWise & " "
        '        Dim dtFATNSFDCNote As DataTable = clsDBFuncationality.GetDataTable(sQuery)





        '        sQuery = " select * from ( select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc,0 as FAT_Amount,0 as SNF_Amount,coalesce(Amount,0) as Amount , AP_Invoice_Date,  isnull(TSPL_DEDUCTION_MASTER.Is_Default_Pashu_Vikash_Kos,0) as Is_Default_Pashu_Vikash_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code from (" + Environment.NewLine +
        '        "select max(trans_type) as trans_type,max(doc_no) as doc_no,max(shipment_doc_no) as AP_Invoice_No,(item_code) as	item_code	, max(Item_Desc) as	Item_Desc,customer_code	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF , max( AP_Invoice_Date) as AP_Invoice_Date from(" + Environment.NewLine +
        '        "select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Item_net_amt*(-1) as Amount ,0 as Show_FAT_SNF  , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_Date,103) else '' end as AP_Invoice_Date  " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
        '"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code   /* where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  =  */ " + Environment.NewLine +
        '")xxx group by customer_code,item_code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF , case when len (TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date) > 0 then convert (varchar,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.AR_Invoice_Date,103) else '' end as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE_Return  " + Environment.NewLine +
        '"left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code   " + Environment.NewLine +
        '"union all
        'select max(trans_type) as trans_type,max(doc_no) as doc_no,max(Payment_No) as AP_Invoice_No,'' as	item_code	,'Asset Lost' as Item_Desc,Vendor_Code as customer_code,0 as	Paymnet_Amount	,sum(Amount) as	Amount	,max(Show_FAT_SNF) as	Show_FAT_SNF,  '' as AP_Invoice_Date from(
        'select 'Asset Lost' as trans_type,TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_Code,TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount*(-1) as Amount ,0 as Show_FAT_SNF 
        'from TSPL_PAYMENT_PROCESS_ASSET_LOST 
        ' /* where TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No  =  */
        ')xxx group by Vendor_Code" + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt ,0 as Show_FAT_SNF , '' as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt,0 as Show_FAT_SNF  , '' as AP_Invoice_Date " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN  " + Environment.NewLine +
        '"left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code    " + Environment.NewLine +
        '"union all " + Environment.NewLine +
        '"select max(trans_type) as trans_type,max(doc_no) as doc_no,max(AP_Invoice_No) as AP_Invoice_No,max(Vendor_CODE) as	Vendor_CODE	,Item_Desc,Vendor_CODE	,sum(Paymnet_Amount) as	Paymnet_Amount	,sum(Item_Net_AMount) as	Item_Net_AMount	,max(Show_FAT_SNF) as	Show_FAT_SNF, AP_Invoice_Date  from( " + Environment.NewLine +
        '"select RefDocType,'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, " + Environment.NewLine +
        '"case when TSPL_DEDUCTION_MASTER.Code is not null then TSPL_DEDUCTION_MASTER.Description else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc end as Item_Desc ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount ,TSPL_DEDUCTION_MASTER.Show_FAT_SNF, convert (varchar,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date,103) as AP_Invoice_Date   " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1" + Environment.NewLine +
        '"left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" + Environment.NewLine + " where /* TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no =   and  */ not ( RefDocType='MILK-REJ')" + Environment.NewLine +     ' TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 or
        '")xxx group by RefDocType,Vendor_CODE,Item_Desc,AP_Invoice_Date " + Environment.NewLine
        '        sQuery += " ) as final "
        '        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no" + Environment.NewLine +
        '        "left outer join (select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (" + BaseQry + ")x group by vsp_code)Tab on tab.VSP_CODE=final.Customer_CODE

        '         left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Description=final.Item_Desc
        '         left outer join (select distinct VSP_Code ,VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = Item_Code

        '         "
        '        sQuery += " " & whrclsItemWise & "  Union All  "
        '        sQuery += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
        '                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
        '                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
        '                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
        '                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
        '                    where /* TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no in () and  */
        '                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
        '                    and TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' 
        '                    ) Final order by Is_Default_Pashu_Vikash_Kos desc , trans_type desc "
        '        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        'sQuery = "select  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,convert(varchar, TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_DETAIL.Reject_Type,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,UOM_Code,TSPL_MILK_REJECT_DETAIL.RATE, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount   " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_DEDUCTION " + Environment.NewLine +
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No" + Environment.NewLine +
        '"left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo " + Environment.NewLine +
        '"left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE" + Environment.NewLine +
        '"where /* TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no =  and */ RefDocType='MILK-REJ'"
        'Dim dtRej As DataTable = clsDBFuncationality.GetDataTable(sQuery)

        '        sQuery = "select TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,  TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc" + Environment.NewLine +
        '",TSPL_SD_SHIPMENT_detail.Qty as MILK_WEIGHT,case when isnull(TSPL_SD_SHIPMENT_detail.Qty,0)<=0 then 0 else cast(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount/TSPL_SD_SHIPMENT_detail.Qty as decimal(18,2)) end as RATE,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount  " + Environment.NewLine +
        '"from TSPL_PAYMENT_PROCESS_MCC_SALE " + Environment.NewLine +
        '"left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " + Environment.NewLine +
        '"left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code    " + Environment.NewLine +
        '" /* where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no  =  */ "
        '        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(sQuery)


        sQueryAH = " select distinct coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc) as Addition 
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no
					left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and
                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and (TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' or TSPL_VENDOR_INVOICE_HEAD.Document_Type='C') order by coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc)"

        Dim dtAdditionHeader As DataTable = clsDBFuncationality.GetDataTable(sQueryAH)

        sQueryAH = " select coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc) as Addition,sum(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount) as Amount 
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no
					left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and
                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and (TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' or TSPL_VENDOR_INVOICE_HEAD.Document_Type='C')
                    group by coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc)
                    order by coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc)"


        Dim dtAdditionTotal As DataTable = clsDBFuncationality.GetDataTable(sQueryAH)

        sQueryDH = " select distinct (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) as Ded_Code from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                    where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                    order by (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) "
        Dim dtDeductionHeader As DataTable = clsDBFuncationality.GetDataTable(sQueryDH)

        sQueryDH = " select (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) as Ded_Code,sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount) as Amount from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                    where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                   group by (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) 
order by (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) "

        Dim dtDeductionTotal As DataTable = clsDBFuncationality.GetDataTable(sQueryDH)

        sQueryAD = " select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME, Final.Item_Desc as Addition , sum(Amount) as [Amount]  from (
select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc) as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no
					left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and
                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and (TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' or TSPL_VENDOR_INVOICE_HEAD.Document_Type='C') ) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc "

        'sQueryAD = ""

        Dim dtAdditionTemp As DataTable = clsDBFuncationality.GetDataTable(sQueryAD)
        ''''''''''''''
        Dim AmtAdd As Decimal = 0.0
        Dim dtAddition As DataTable = New DataTable()
        dtAddition.Columns.Add("VSP_CODE", GetType(String))
        dtAddition.Columns.Add("Addition", GetType(String))
        dtAddition.Columns.Add(New DataColumn("Amount", System.Type.GetType("System.Decimal")))
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim dtVSP As DataTable = dt.DefaultView.ToTable(True, "VSP_CODE")
            For v As Integer = 0 To dtVSP.Rows.Count - 1
                RowCount = 0
                For d As Integer = 0 To dtAdditionHeader.Rows.Count - 1
                    AmtAdd = clsCommon.myCdbl(dtAdditionTemp.Compute("sum([Amount])", "[VSP_CODE]='" + clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")) + "' and [Addition]='" + clsCommon.myCstr(dtAdditionHeader.Rows(d).Item("Addition")) + "'"))
                    dtAddition.Rows.Add(clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")), clsCommon.myCstr(dtAdditionHeader.Rows(d).Item("Addition")), AmtAdd)
                    RowCount = RowCount + 1
                Next
                If RowCount < 4 Then
                    For i As Integer = RowCount To 3
                        dtAddition.Rows.Add(clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")), "", clsCommon.myCdbl("0.00"))
                    Next
                End If
            Next
        End If
        ''''''''''''''


        sQueryDD = " select Final.Vendor_CODE as VSP_CODE, Final.Ded_Code , sum(Amount) as [Amount] from (
select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME,(case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) as Ded_Code , TSPL_PAYMENT_PROCESS_DEDUCTION.Amount from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
) Final group by  final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code "

        Dim dtDeductionTemp As DataTable = clsDBFuncationality.GetDataTable(sQueryDD)

        ''''''''''''''

        Dim Amt As Decimal = 0.0
        Dim dtDeduction As DataTable = New DataTable()
        dtDeduction.Columns.Add("VSP_CODE", GetType(String))
        dtDeduction.Columns.Add("Ded_Code", GetType(String))
        dtDeduction.Columns.Add(New DataColumn("Amount", System.Type.GetType("System.Decimal")))
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim dtVSP As DataTable = dt.DefaultView.ToTable(True, "VSP_CODE")
            For v As Integer = 0 To dtVSP.Rows.Count - 1
                RowCount = 0
                For d As Integer = 0 To dtDeductionHeader.Rows.Count - 1
                    Amt = clsCommon.myCdbl(dtDeductionTemp.Compute("sum([Amount])", "[VSP_CODE]='" + clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")) + "' and Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(d).Item("Ded_Code")) + "'"))
                    dtDeduction.Rows.Add(clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")), clsCommon.myCstr(dtDeductionHeader.Rows(d).Item("Ded_Code")), Amt)
                    RowCount = RowCount + 1
                Next
                If RowCount < 4 Then
                    For i As Integer = RowCount To 3
                        dtDeduction.Rows.Add(clsCommon.myCstr(dtVSP.Rows(v).Item("VSP_CODE")), "", clsCommon.myCdbl("0.00"))
                    Next
                End If
            Next
        End If
        ''''''''''''''


        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtAddition, "crptDCSLedgerReport", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeduction, "subAdditionHeader1.rpt", dtAdditionHeader, "subDeductionHeader11.rpt", dtDeductionHeader, "subAdditionTotal.rpt", dtAdditionTotal, "subDeductionTotal.rpt", dtDeductionTotal)
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub

    Private Sub Load_DCS_Ledger_Report_RCDF_PDF()
        Try
            PageSetupReport_ID = MyBase.Form_ID + "DCS"
            If clsCommon.GetDateWithEndTime(dtpToDCS_Ledger.Value) < clsCommon.GetDateWithStartTime(dtpFromDCS_Ledger.Value) Then
                clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim sQueryDH As String = ""
            Dim sQueryAH As String = ""
            Dim sQueryDD As String = ""
            Dim sQueryAD As String = ""
            Dim sQuery As String = ""
            Dim RowCount As Integer = 0
            Dim newBlankRow1 As DataRow = Nothing
            Dim newBlankRow2 As DataRow = Nothing
            Dim RowIndexForTotal As Integer = 0

            Dim fromDate As String = dtpFromDCS_Ledger.Value
            Dim Todate As String = dtpToDCS_Ledger.Value

            Dim whrcls As String = " where 2=2 "
            Dim whrcls1 As String = " where 2=2 "
            strQry = ""
            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                strQry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")"
            End If

            If txtRouteName.arrValueMember IsNot Nothing AndAlso txtRouteName.arrValueMember.Count > 0 Then
                strQry += " and TSPL_BULK_ROUTE_MASTER.Route_Name in (" + clsCommon.GetMulcallString(txtRouteName.arrValueMember) + ")"
            End If

            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) "
            whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "
            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1  "

            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                whrcls1 += " and tspl_vlc_master_head.mcc in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ") "
            End If

            If txtRouteName.arrValueMember IsNot Nothing AndAlso txtRouteName.arrValueMember.Count > 0 Then
                whrcls1 += " and tspl_bulk_route_master_mcc.route_no in (" + clsCommon.GetMulcallString(txtRouteName.arrValueMember) + ") "
            End If


            Dim BaseQry As String = ""
            BaseQry = "select TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, "
            BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
            BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
            ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
            ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
            ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
            BaseQry += " TSPL_BULK_ROUTE_MASTER.ROUTE_NO as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_BULK_ROUTE_MASTER.Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
            BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
                ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
            BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,TSPL_MILK_SRN_DETAIL.FAT_KG as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,TSPL_MILK_SRN_DETAIL.SNF_KG as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
, case when isnull (TSPL_MILK_REJECT_DETAIL.Reject_Type,'') = '' then  'SWEET' else upper (TSPL_MILK_REJECT_DETAIL.Reject_Type) end  as QBD, TBL_BILL_DETAILS.DOCNO" + Environment.NewLine +
            " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
            BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code  " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine

            If SettPickBulkRoute Then
                BaseQry += " LEFT OUTER JOIN TSPL_BULK_ROUTE_MASTER_MCC ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_BULK_ROUTE_MASTER_MCC.MCC_CODE
                        left join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO =TSPL_BULK_ROUTE_MASTER.ROUTE_NO  " + Environment.NewLine
            Else
                BaseQry += " left join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine
            End If

            BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
            BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
            BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
            BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
            BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
            BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
            " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and  TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.sample_no
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo ,TSPL_PAYMENT_PROCESS_DETAIL.Doc_No as DOCNO, convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
		  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + fromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + Todate + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 "

            'If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
            '          BaseQry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ") "
            ' End If

            'If txtRouteName.arrValueMember IsNot Nothing AndAlso txtRouteName.arrValueMember.Count > 0 Then
            'BaseQry += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO in (" + clsCommon.GetMulcallString(txtRouteName.arrValueMember) + ") "
            'End If


            BaseQry += ") as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code"
            '





            BaseQry += "  " & whrcls & " " + strQry

            Dim dt As New DataTable
            sQuery = BaseQry

            Dim legerMainQuery As String = " select  XXXFinal.ROUTE_CODE , DOCNO, Route_Name , VSP_CODE, max(Vendor_Name) as Vendor_Name , max(Type) as Type ,sum( Qty) as Qty , sum(case when QBD = 'SWEET' then   Qty else 0 end) as SweetQty ,sum(case when QBD = 'CURD' then   Qty else 0 end) as CurdQty , sum(case when QBD = 'SOUR' then   Qty else 0 end) as SourQty , sum(FATQTY) * 100 / sum( Qty)  as FAT_PER , sum(SNFQTY) * 100 / sum( Qty) as SNF_PER, sum(FATQTY) as  FATQTY, sum(SNFQTY) as SNFQTY, sum (SRN_Net_Amount) as SRN_Net_Amount,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,VLC_NO from (  " + sQuery + " ) XXXFinal group by  XXXFinal.ROUTE_CODE , DOCNO, Route_Name , VSP_CODE,VLC_NO  "
            legerMainQuery = " select Invoice.ROUTE_CODE, DOCNO, Invoice.Route_Name,Invoice.VSP_CODE,	Invoice.Vendor_Name,Invoice.Type,Invoice.Qty,Invoice.SweetQty,Invoice.CurdQty,Invoice.SourQty,Invoice.FAT_PER
            ,Invoice.SNF_PER,Invoice.FATQTY,Invoice.SNFQTY,Invoice.SRN_Net_Amount,Invoice.VLC_Code_VLC_Uploader,Invoice.VLC_NO
            ,  coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1)  as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount
            from    (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount,sum(Payable_Amount) as Payable_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  " & whrcls1 & "  ) as pp group by VSP_CODE,VLC_Code ) as PaymentProcess
            left join (  " + legerMainQuery + " )Invoice on   PaymentProcess.vsp_code = Invoice.vsp_code And PaymentProcess.VLC_Code = Invoice.VLC_NO  "



            dt = clsDBFuncationality.GetDataTable(legerMainQuery)



            'Header
            Dim HeadingPAYMENTRowCount As Double = 0
            Dim HeadingDEDUCTIONRowCount As Double = 0
            Dim HeadingRowCount As Double = 0

            sQueryAH = "select Addition from( select distinct coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc) as Addition 
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no
                    and TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
					left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and
                     TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and (TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' or TSPL_VENDOR_INVOICE_HEAD.Document_Type='C'))x where Addition is not null order by Addition "

            Dim dtAdditionHeader As DataTable = clsDBFuncationality.GetDataTable(sQueryAH)
            HeadingPAYMENTRowCount = clsCommon.myCdbl(IIf(Math.Ceiling(dtAdditionHeader.Rows.Count / 4) <= 4, 4, Math.Ceiling(dtAdditionHeader.Rows.Count / 4)))


            sQueryDH = " select distinct (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) as Ded_Code from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                    where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1
                    order by (case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) "
            Dim dtDeductionHeader As DataTable = clsDBFuncationality.GetDataTable(sQueryDH)
            HeadingDEDUCTIONRowCount = clsCommon.myCdbl(IIf(Math.Ceiling(dtDeductionHeader.Rows.Count / 4) <= 4, 4, Math.Ceiling(dtDeductionHeader.Rows.Count / 4)))
            HeadingRowCount = clsCommon.myCdbl(Math.Max(HeadingPAYMENTRowCount, HeadingDEDUCTIONRowCount))

            Dim HeadingDCS As String = "" + Environment.NewLine + "BILL NO" + Environment.NewLine + "DCS NAME" + Environment.NewLine + "DCS CODE" + Environment.NewLine + "DCS ROUTE"
            Dim HeadingDCSparts() As String = HeadingDCS.Split(Environment.NewLine)
            Dim HeadingDCSNumberOfLines As Integer = HeadingDCSparts.Length
            For t As Integer = HeadingDCSNumberOfLines To HeadingRowCount
                HeadingDCS = HeadingDCS + Environment.NewLine + " "
            Next

            Dim HeadingMilkQty1 As String = "<QTY>" + Environment.NewLine + "SWEET" + Environment.NewLine + " SOUR" + Environment.NewLine + " CURD" + Environment.NewLine + "TOTAL"
            Dim HeadingMilkQty1parts() As String = HeadingMilkQty1.Split(Environment.NewLine)
            Dim HeadingMilkQty1NumberOfLines As Integer = HeadingMilkQty1parts.Length
            For t As Integer = HeadingMilkQty1NumberOfLines To HeadingRowCount
                HeadingMilkQty1 = HeadingMilkQty1 + Environment.NewLine + " "
            Next

            Dim HeadingMilkQty2 As String = "<QTY>" + Environment.NewLine + "KGFAT" + Environment.NewLine + "KGSNF" + Environment.NewLine + " FAT%" + Environment.NewLine + " SNF%"
            Dim HeadingMilkQty2parts() As String = HeadingMilkQty2.Split(Environment.NewLine)
            Dim HeadingMilkQty2NumberOfLines As Integer = HeadingMilkQty2parts.Length
            For t As Integer = HeadingMilkQty2NumberOfLines To HeadingRowCount
                HeadingMilkQty2 = HeadingMilkQty2 + Environment.NewLine + " "
            Next

            Dim HeadingMilkAmt As String = "<PAYMENT>" + Environment.NewLine + "Milk Payment" + Environment.NewLine + "Head Load Amount"
            Dim HeadingMilkAmtParts() As String = HeadingMilkAmt.Split(Environment.NewLine)
            Dim HeadingMilkAmtNumberOfLines As Integer = HeadingMilkAmtParts.Length
            For t As Integer = HeadingMilkAmtNumberOfLines To HeadingRowCount
                HeadingMilkAmt = HeadingMilkAmt + Environment.NewLine + " "
            Next


            Dim HeadingTotal As String = "<TOTAL>" + Environment.NewLine + "TOTAL PAYMENT" + Environment.NewLine + "TOTAL DEDUCTION" + Environment.NewLine + "NET PAYABLE"
            Dim HeadingTotalparts() As String = HeadingTotal.Split(Environment.NewLine)
            Dim HeadingTotalNumberOfLines As Integer = HeadingTotalparts.Length
            For t As Integer = HeadingTotalNumberOfLines To HeadingRowCount
                HeadingTotal = HeadingTotal + Environment.NewLine + " "
            Next

            Dim HeadingPayment1 As String = "<PAYMENT>"
            Dim HeadingPayment2 As String = "<PAYMENT>"
            Dim HeadingPayment3 As String = "<PAYMENT>"
            Dim HeadingPayment4 As String = "<PAYMENT>"
            Dim TempReminder As Integer = 0
            For t As Integer = 0 To dtAdditionHeader.Rows.Count - 1
                Math.DivRem((t + 1), 4, TempReminder)
                If TempReminder = 1 Then
                    HeadingPayment1 = HeadingPayment1 + Environment.NewLine + dtAdditionHeader.Rows(t).Item("Addition")
                ElseIf TempReminder = 2 Then
                    HeadingPayment2 = HeadingPayment2 + Environment.NewLine + dtAdditionHeader.Rows(t).Item("Addition")
                ElseIf TempReminder = 3 Then
                    HeadingPayment3 = HeadingPayment3 + Environment.NewLine + dtAdditionHeader.Rows(t).Item("Addition")
                ElseIf TempReminder = 0 Then
                    HeadingPayment4 = HeadingPayment4 + Environment.NewLine + dtAdditionHeader.Rows(t).Item("Addition")
                End If
            Next

            Dim HeadingPayment1parts() As String = HeadingPayment1.Split(Environment.NewLine)
            Dim HeadingPayment1NumberOfLines As Integer = HeadingPayment1parts.Length
            For t As Integer = HeadingPayment1NumberOfLines To HeadingRowCount
                HeadingPayment1 = HeadingPayment1 + Environment.NewLine + " "
            Next
            Dim HeadingPayment2parts() As String = HeadingPayment2.Split(Environment.NewLine)
            Dim HeadingPayment2NumberOfLines As Integer = HeadingPayment2parts.Length
            For t As Integer = HeadingPayment2NumberOfLines To HeadingRowCount
                HeadingPayment2 = HeadingPayment2 + Environment.NewLine + " "
            Next
            Dim HeadingPayment3parts() As String = HeadingPayment3.Split(Environment.NewLine)
            Dim HeadingPayment3NumberOfLines As Integer = HeadingPayment3parts.Length
            For t As Integer = HeadingPayment3NumberOfLines To HeadingRowCount
                HeadingPayment3 = HeadingPayment3 + Environment.NewLine + " "
            Next
            Dim HeadingPayment4parts() As String = HeadingPayment4.Split(Environment.NewLine)
            Dim HeadingPayment4NumberOfLines As Integer = HeadingPayment4parts.Length
            For t As Integer = HeadingPayment4NumberOfLines To HeadingRowCount
                HeadingPayment4 = HeadingPayment4 + Environment.NewLine + " "
            Next

            Dim HeadingDeduction1 As String = "<DEDUCTION>"
            Dim HeadingDeduction2 As String = "<DEDUCTION>"
            Dim HeadingDeduction3 As String = "<DEDUCTION>"
            Dim HeadingDeduction4 As String = "<DEDUCTION>"

            For t As Integer = 0 To dtDeductionHeader.Rows.Count - 1
                Math.DivRem((t + 1), 4, TempReminder)
                If TempReminder = 1 Then
                    HeadingDeduction1 = HeadingDeduction1 + Environment.NewLine + dtDeductionHeader.Rows(t).Item("Ded_Code")
                ElseIf TempReminder = 2 Then
                    HeadingDeduction2 = HeadingDeduction2 + Environment.NewLine + dtDeductionHeader.Rows(t).Item("Ded_Code")
                ElseIf TempReminder = 3 Then
                    HeadingDeduction3 = HeadingDeduction3 + Environment.NewLine + dtDeductionHeader.Rows(t).Item("Ded_Code")
                ElseIf TempReminder = 0 Then
                    HeadingDeduction4 = HeadingDeduction4 + Environment.NewLine + dtDeductionHeader.Rows(t).Item("Ded_Code")
                End If
            Next

            Dim HeadingDeduction1parts() As String = HeadingDeduction1.Split(Environment.NewLine)
            Dim HeadingDeduction1NumberOfLines As Integer = HeadingDeduction1parts.Length
            For t As Integer = HeadingDeduction1NumberOfLines To HeadingRowCount
                HeadingDeduction1 = HeadingDeduction1 + Environment.NewLine + " "
            Next
            Dim HeadingDeduction2parts() As String = HeadingDeduction2.Split(Environment.NewLine)
            Dim HeadingDeduction2NumberOfLines As Integer = HeadingDeduction2parts.Length
            For t As Integer = HeadingDeduction2NumberOfLines To HeadingRowCount
                HeadingDeduction2 = HeadingDeduction2 + Environment.NewLine + " "
            Next
            Dim HeadingDeduction3parts() As String = HeadingDeduction3.Split(Environment.NewLine)
            Dim HeadingDeduction3NumberOfLines As Integer = HeadingDeduction3parts.Length
            For t As Integer = HeadingDeduction3NumberOfLines To HeadingRowCount
                HeadingDeduction3 = HeadingDeduction3 + Environment.NewLine + " "
            Next
            Dim HeadingDeduction4parts() As String = HeadingDeduction4.Split(Environment.NewLine)
            Dim HeadingDeduction4NumberOfLines As Integer = HeadingDeduction4parts.Length
            For t As Integer = HeadingDeduction4NumberOfLines To HeadingRowCount
                HeadingDeduction4 = HeadingDeduction4 + Environment.NewLine + " "
            Next


            sQueryAD = " select  Final.VSP_Uploader_Code, Final.VSP_Code ,'' as Vendor_NAME, Final.Item_Desc as Addition,Final.ROUTE_NO as ROUTE_CODE, sum(Amount) as [Amount]  from (
select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VSP_Code ,TSPL_MULTIPLE_DEDUCTION_head.trans_type   , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as VLC_Code_VLC_Uploader, coalesce(TSPL_DCS_ADDITION_DEDUCTION.Description,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc) as Item_Desc , 0 as FAT_Amount,0 as SNF_Amount , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Amount ,Convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as  AP_Invoice_Date,  0 as Is_Default_Pashu_Vikas_Kos, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no
                    and TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
					left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
                    left outer join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_MULTIPLE_DEDUCTION_head on TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                    LEFT OUTER JOIN TSPL_BULK_ROUTE_MASTER_MCC ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_BULK_ROUTE_MASTER_MCC.MCC_CODE
					inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
                    where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and
                    TSPL_VENDOR_INVOICE_HEAD.Description <> 'AP Credit Note For VSP Commission'
                    and (TSPL_MULTIPLE_DEDUCTION_head.trans_type = 'Addition' or TSPL_VENDOR_INVOICE_HEAD.Document_Type='C') "

            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                sQueryAD += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ") "
            End If

            If txtRouteName.arrValueMember IsNot Nothing AndAlso txtRouteName.arrValueMember.Count > 0 Then
                sQueryAD += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO in (" + clsCommon.GetMulcallString(txtRouteName.arrValueMember) + ") "
            End If


            sQueryAD += " ) Final group by Final.VSP_Uploader_Code, Final.VSP_Code , Final.Item_Desc,Final.ROUTE_NO "

            'sQueryAD = ""

            Dim dtAdditionTemp As DataTable = clsDBFuncationality.GetDataTable(sQueryAD)

            sQueryDD = " select Final.Vendor_CODE as VSP_CODE, Final.Ded_Code,Final.ROUTE_NO as ROUTE_CODE, sum(Amount) as [Amount] from (
select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME,(case when len(TSPL_DCS_ADDITION_DEDUCTION.Description)>0 then TSPL_DCS_ADDITION_DEDUCTION.Description
when len(TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc)>0 then TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc else TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code end) as Ded_Code,TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO , (ISNULL (TSPL_PAYMENT_PROCESS_DEDUCTION.Amount ,0)-ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) ) as 'AMOUNT' from TSPL_PAYMENT_PROCESS_DEDUCTION inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
LEFT OUTER JOIN TSPL_BULK_ROUTE_MASTER_MCC ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_BULK_ROUTE_MASTER_MCC.MCC_CODE
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.code =TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
where convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDCS_Ledger.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDCS_Ledger.Value + "'),103) and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1"
            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                sQueryDD += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ") "
            End If

            If txtRouteName.arrValueMember IsNot Nothing AndAlso txtRouteName.arrValueMember.Count > 0 Then
                sQueryDD += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO in (" + clsCommon.GetMulcallString(txtRouteName.arrValueMember) + ") "
            End If

            sQueryDD += ") Final group by  final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code,Final.ROUTE_NO "

            Dim dtDeductionTemp As DataTable = clsDBFuncationality.GetDataTable(sQueryDD)


            Dim dtROUTE1 As DataTable = New DataTable()
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                dtROUTE1 = dt.DefaultView.ToTable(True, "ROUTE_CODE")
            End If


            Dim dtMain As DataTable = New DataTable()
            dtMain.Columns.Add("DCS", GetType(String))
            dtMain.Columns.Add(New DataColumn("MilkQty1", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("MilkQty2", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("MilkAmt", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Payment1", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Payment2", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Payment3", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Payment4", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Deduction1", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Deduction2", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Deduction3", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Deduction4", System.Type.GetType("System.Decimal")))
            dtMain.Columns.Add(New DataColumn("Total", System.Type.GetType("System.Decimal")))

            ''
            Dim SumSWEET1 As Decimal = 0
            Dim SumSOUR1 As Decimal = 0
            Dim SumCURD1 As Decimal = 0
            Dim SumQty1 As Decimal = 0
            Dim SumKGFAT1 As Decimal = 0
            Dim SumKGSNF1 As Decimal = 0
            Dim AVGFAT1 As Decimal = 0
            Dim AVGSNF1 As Decimal = 0
            Dim MilkAmt As Decimal = 0
            Dim HeadLoadAmt As Decimal = 0
            Dim SumPayment1 As Decimal = 0
            Dim SumDeduction1 As Decimal = 0
            Dim SumNETPAYABLE1 As Decimal = 0
            Dim dtVSP1 As DataTable = Nothing

            If dtROUTE1 IsNot Nothing And dtROUTE1.Rows.Count > 0 Then
                For R As Integer = 0 To dtROUTE1.Rows.Count - 1
                    Dim dr As DataRow() = dt.Select("ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'")
                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
                        dtVSP1 = dr.CopyToDataTable()
                        dtMain.Rows.Add("R-Code: " + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "-" + clsCommon.myCstr(dtVSP1.Rows(0).Item("Route_Name")), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                        For V As Integer = 0 To dtVSP1.Rows.Count - 1
                            SumSWEET1 = clsCommon.myCdbl(dt.Compute("sum(SweetQty)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumSOUR1 = clsCommon.myCdbl(dt.Compute("sum(SourQty)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumCURD1 = clsCommon.myCdbl(dt.Compute("sum(CurdQty)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumQty1 = clsCommon.myCdbl(dt.Compute("sum(Qty)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumKGFAT1 = clsCommon.myCdbl(dt.Compute("sum(FATQTY)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumKGSNF1 = clsCommon.myCdbl(dt.Compute("sum(SNFQTY)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            MilkAmt = clsCommon.myCdbl(dt.Compute("sum(SRN_Net_Amount)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            HeadLoadAmt = clsCommon.myCdbl(dt.Compute("sum(Head_Load_Amount)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            AVGFAT1 = Math.Round(clsCommon.myCdbl(dt.Compute("MAX(FAT_PER)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'")), 2)
                            AVGSNF1 = Math.Round(clsCommon.myCdbl(dt.Compute("MAX(SNF_PER)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'")), 2)
                            SumPayment1 = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumDeduction1 = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            SumNETPAYABLE1 = clsCommon.myCdbl(dt.Compute("sum(Payable_Amount)", "VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                            'clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "-" + 
                            dtMain.Rows.Add(clsCommon.myCstr(dtVSP1.Rows(V).Item("DOCNO")), SumSWEET1, SumKGFAT1, MilkAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumPayment1)
                            dtMain.Rows.Add(clsCommon.myCstr(dtVSP1.Rows(V).Item("Vendor_Name")), SumSOUR1, SumKGSNF1, HeadLoadAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumDeduction1)
                            dtMain.Rows.Add(clsCommon.myCstr(dtVSP1.Rows(V).Item("VLC_Code_VLC_Uploader")), SumCURD1, AVGFAT1, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumNETPAYABLE1)
                            dtMain.Rows.Add(clsCommon.myCstr(dtVSP1.Rows(V).Item("ROUTE_CODE")), SumQty1, AVGSNF1, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                            If HeadingRowCount > 4 Then
                                For t As Integer = 4 To HeadingRowCount - 1
                                    dtMain.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                                Next
                            End If

                            ''Payment
                            RowIndexForTotal = 0
                            If dtAdditionTemp IsNot Nothing AndAlso dtAdditionTemp.Rows.Count > 0 Then
                                RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                                For t As Integer = 0 To dtAdditionHeader.Rows.Count - 1
                                    Math.DivRem((t + 1), 4, TempReminder)
                                    If TempReminder = 1 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Payment1") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 2 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Payment2") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 3 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Payment3") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 0 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Payment4") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                        RowIndexForTotal = RowIndexForTotal + 1
                                    End If
                                Next
                            End If

                            ''Deduction
                            RowIndexForTotal = 0
                            If dtDeductionTemp IsNot Nothing AndAlso dtDeductionTemp.Rows.Count > 0 Then
                                RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                                For t As Integer = 0 To dtDeductionHeader.Rows.Count - 1
                                    Math.DivRem((t + 1), 4, TempReminder)
                                    If TempReminder = 1 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Deduction1") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 2 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Deduction2") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 3 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Deduction3") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                    ElseIf TempReminder = 0 Then
                                        dtMain.Rows(RowIndexForTotal).Item("Deduction4") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND VSP_CODE='" + clsCommon.myCstr(dtVSP1.Rows(V).Item("VSP_CODE")) + "'"))
                                        RowIndexForTotal = RowIndexForTotal + 1
                                    End If
                                Next
                            End If
                            newBlankRow1 = dtMain.NewRow
                            dtMain.Rows.Add(newBlankRow1)
                            newBlankRow2 = dtMain.NewRow
                            dtMain.Rows.Add(newBlankRow2)
                        Next

                        'Route Total
                        newBlankRow1 = dtMain.NewRow
                        dtMain.Rows.Add(newBlankRow1)
                        newBlankRow2 = dtMain.NewRow
                        dtMain.Rows.Add(newBlankRow2)

                        SumSWEET1 = clsCommon.myCdbl(dt.Compute("sum(SweetQty)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumSOUR1 = clsCommon.myCdbl(dt.Compute("sum(SourQty)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumCURD1 = clsCommon.myCdbl(dt.Compute("sum(CurdQty)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumQty1 = clsCommon.myCdbl(dt.Compute("sum(Qty)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumKGFAT1 = clsCommon.myCdbl(dt.Compute("sum(FATQTY)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumKGSNF1 = clsCommon.myCdbl(dt.Compute("sum(SNFQTY)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))

                        MilkAmt = clsCommon.myCdbl(dt.Compute("sum(SRN_Net_Amount)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        HeadLoadAmt = clsCommon.myCdbl(dt.Compute("sum(Head_Load_Amount)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))


                        If SumQty1 > 0 Then
                            AVGFAT1 = Math.Round(clsCommon.myCdbl((SumKGFAT1 * 100) / SumQty1), 2)
                            AVGSNF1 = Math.Round(clsCommon.myCdbl((SumKGSNF1 * 100) / SumQty1), 2)
                        Else
                            AVGFAT1 = 0
                            AVGSNF1 = 0
                        End If
                        SumPayment1 = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumDeduction1 = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        SumNETPAYABLE1 = clsCommon.myCdbl(dt.Compute("sum(Payable_Amount)", "ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                        dtMain.Rows.Add("R-Total: " + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "-" + clsCommon.myCstr(dtVSP1.Rows(0).Item("Route_Name")), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                        dtMain.Rows.Add(DBNull.Value, SumSWEET1, SumKGFAT1, MilkAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumPayment1)
                        dtMain.Rows.Add(DBNull.Value, SumSOUR1, SumKGSNF1, HeadLoadAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumDeduction1)
                        dtMain.Rows.Add(DBNull.Value, SumCURD1, AVGFAT1, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, SumNETPAYABLE1)
                        dtMain.Rows.Add(DBNull.Value, SumQty1, AVGSNF1, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                        If HeadingRowCount > 4 Then
                            For t As Integer = 4 To HeadingRowCount - 1
                                dtMain.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                            Next
                        End If

                        ''Payment
                        RowIndexForTotal = 0
                        If dtAdditionTemp IsNot Nothing AndAlso dtAdditionTemp.Rows.Count > 0 Then
                            RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                            For t As Integer = 0 To dtAdditionHeader.Rows.Count - 1
                                Math.DivRem((t + 1), 4, TempReminder)
                                If TempReminder = 1 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Payment1") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 2 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Payment2") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 3 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Payment3") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 0 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Payment4") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                    RowIndexForTotal = RowIndexForTotal + 1
                                End If
                            Next
                        End If

                        ''Deduction
                        RowIndexForTotal = 0
                        If dtDeductionTemp IsNot Nothing AndAlso dtDeductionTemp.Rows.Count > 0 Then
                            RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                            For t As Integer = 0 To dtDeductionHeader.Rows.Count - 1
                                Math.DivRem((t + 1), 4, TempReminder)
                                If TempReminder = 1 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Deduction1") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 2 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Deduction2") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 3 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Deduction3") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                ElseIf TempReminder = 0 Then
                                    dtMain.Rows(RowIndexForTotal).Item("Deduction4") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "' AND ROUTE_CODE='" + clsCommon.myCstr(dtROUTE1.Rows(R).Item("ROUTE_CODE")) + "'"))
                                    RowIndexForTotal = RowIndexForTotal + 1
                                End If
                            Next
                        End If

                        newBlankRow1 = dtMain.NewRow
                        dtMain.Rows.Add(newBlankRow1)
                        newBlankRow2 = dtMain.NewRow
                        dtMain.Rows.Add(newBlankRow2)

                        'Route Total
                    End If
                Next


            End If

            ''

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                newBlankRow1 = dtMain.NewRow
                dtMain.Rows.Add(newBlankRow1)
                newBlankRow2 = dtMain.NewRow
                dtMain.Rows.Add(newBlankRow2)
                Dim GSumSWEET As Decimal = clsCommon.myCdbl(dt.Compute("sum(SweetQty)", ""))
                Dim GSumSOUR As Decimal = clsCommon.myCdbl(dt.Compute("sum(SourQty)", ""))
                Dim GSumCURD As Decimal = clsCommon.myCdbl(dt.Compute("sum(CurdQty)", ""))
                Dim GSumQty As Decimal = clsCommon.myCdbl(dt.Compute("sum(Qty)", ""))
                Dim GSumKGFAT As Decimal = clsCommon.myCdbl(dt.Compute("sum(FATQTY)", ""))
                Dim GSumKGSNF As Decimal = clsCommon.myCdbl(dt.Compute("sum(SNFQTY)", ""))

                Dim GMilkAmt As Decimal = clsCommon.myCdbl(dt.Compute("sum(SRN_Net_Amount)", ""))
                Dim GHeadLoadAmt As Decimal = clsCommon.myCdbl(dt.Compute("sum(Head_Load_Amount)", ""))

                Dim GAVGFAT As Decimal = 0
                Dim GAVGSNF As Decimal = 0
                If GSumQty > 0 Then
                    GAVGFAT = Math.Round(clsCommon.myCdbl((GSumKGFAT * 100) / GSumQty), 2)
                    GAVGSNF = Math.Round(clsCommon.myCdbl((GSumKGSNF * 100) / GSumQty), 2)
                End If
                Dim GSumPayment As Decimal = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", ""))
                Dim GSumDeduction As Decimal = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", ""))
                Dim GSumNETPAYABLE As Decimal = clsCommon.myCdbl(dt.Compute("sum(Payable_Amount)", ""))
                dtMain.Rows.Add("G-TOTAL:", GSumSWEET, GSumKGFAT, GMilkAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, GSumPayment)
                dtMain.Rows.Add(DBNull.Value, GSumSOUR, GSumKGSNF, GHeadLoadAmt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, GSumDeduction)
                dtMain.Rows.Add(DBNull.Value, GSumCURD, GAVGFAT, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, GSumNETPAYABLE)
                dtMain.Rows.Add(DBNull.Value, GSumQty, GAVGSNF, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                If HeadingRowCount > 4 Then
                    For t As Integer = 4 To HeadingRowCount - 1
                        dtMain.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
                    Next
                End If


                RowIndexForTotal = 0
                If dtAdditionTemp IsNot Nothing AndAlso dtAdditionTemp.Rows.Count > 0 Then
                    RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                    For t As Integer = 0 To dtAdditionHeader.Rows.Count - 1
                        Math.DivRem((t + 1), 4, TempReminder)
                        If TempReminder = 1 Then
                            dtMain.Rows(RowIndexForTotal).Item("Payment1") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "'"))
                        ElseIf TempReminder = 2 Then
                            dtMain.Rows(RowIndexForTotal).Item("Payment2") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "'"))
                        ElseIf TempReminder = 3 Then
                            dtMain.Rows(RowIndexForTotal).Item("Payment3") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "'"))
                        ElseIf TempReminder = 0 Then
                            dtMain.Rows(RowIndexForTotal).Item("Payment4") = clsCommon.myCdbl(dtAdditionTemp.Compute("sum(Amount)", "Addition='" + clsCommon.myCstr(dtAdditionHeader.Rows(t).Item("Addition")) + "'"))
                            RowIndexForTotal = RowIndexForTotal + 1
                        End If
                    Next
                End If

                If dtDeductionTemp IsNot Nothing AndAlso dtDeductionTemp.Rows.Count > 0 Then
                    RowIndexForTotal = dtMain.Rows.Count - HeadingRowCount
                    For t As Integer = 0 To dtDeductionHeader.Rows.Count - 1
                        Math.DivRem((t + 1), 4, TempReminder)
                        If TempReminder = 1 Then
                            dtMain.Rows(RowIndexForTotal).Item("Deduction1") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "'"))
                        ElseIf TempReminder = 2 Then
                            dtMain.Rows(RowIndexForTotal).Item("Deduction2") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "'"))
                        ElseIf TempReminder = 3 Then
                            dtMain.Rows(RowIndexForTotal).Item("Deduction3") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "'"))
                        ElseIf TempReminder = 0 Then
                            dtMain.Rows(RowIndexForTotal).Item("Deduction4") = clsCommon.myCdbl(dtDeductionTemp.Compute("sum(Amount)", "Ded_Code='" + clsCommon.myCstr(dtDeductionHeader.Rows(t).Item("Ded_Code")) + "'"))
                            RowIndexForTotal = RowIndexForTotal + 1
                        End If
                    Next
                End If

            End If

            newBlankRow1 = dtMain.NewRow
            dtMain.Rows.Add(newBlankRow1)
            newBlankRow2 = dtMain.NewRow
            dtMain.Rows.Add(newBlankRow2)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
                Gv1.Rows.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.ShowGroupPanel = False

                Gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.DataSource = dtMain

                ''''''Formatting
                Gv1.TableElement.TableHeaderHeight = 60
                Gv1.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).IsVisible = True
                    Gv1.Columns(ii).BestFit()
                Next

                'Gv1.Columns("Document_No").IsVisible = False
                Gv1.Columns("DCS").HeaderText = HeadingDCS
                Gv1.Columns("MilkQty1").HeaderText = HeadingMilkQty1
                Gv1.Columns("MilkQty2").HeaderText = HeadingMilkQty2
                Gv1.Columns("MilkAmt").HeaderText = HeadingMilkAmt
                Gv1.Columns("Payment1").HeaderText = HeadingPayment1
                'If clsCommon.myLen(HeadingPayment2) > 0 Then
                If HeadingPayment2NumberOfLines > 1 Then
                    Gv1.Columns("Payment2").HeaderText = HeadingPayment2
                    Gv1.Columns("Payment2").IsVisible = True
                Else
                    Gv1.Columns("Payment2").IsVisible = False
                End If
                If HeadingPayment3NumberOfLines > 1 Then
                    Gv1.Columns("Payment3").HeaderText = HeadingPayment3
                    Gv1.Columns("Payment3").IsVisible = True
                Else
                    Gv1.Columns("Payment3").IsVisible = False
                End If
                If HeadingPayment4NumberOfLines > 1 Then
                    Gv1.Columns("Payment4").HeaderText = HeadingPayment4
                    Gv1.Columns("Payment4").IsVisible = True
                Else
                    Gv1.Columns("Payment4").IsVisible = False
                End If

                Gv1.Columns("Deduction1").HeaderText = HeadingDeduction1
                If HeadingDeduction2NumberOfLines > 1 Then
                    Gv1.Columns("Deduction2").HeaderText = HeadingDeduction2
                    Gv1.Columns("Deduction2").IsVisible = True
                Else
                    Gv1.Columns("Deduction2").IsVisible = False
                End If
                If HeadingDeduction3NumberOfLines > 1 Then
                    Gv1.Columns("Deduction3").HeaderText = HeadingDeduction3
                    Gv1.Columns("Deduction3").IsVisible = True
                Else
                    Gv1.Columns("Deduction3").IsVisible = False
                End If
                If HeadingDeduction4NumberOfLines > 1 Then
                    Gv1.Columns("Deduction4").HeaderText = HeadingDeduction4
                    Gv1.Columns("Deduction4").IsVisible = True
                Else
                    Gv1.Columns("Deduction4").IsVisible = False
                End If

                Gv1.Columns("Total").HeaderText = HeadingTotal
                '''''''
                Gv1.BestFitColumns()
            Else
                Gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If



            'PDF
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDCS_Ledger.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDCS_Ledger.Value, "dd/MM/yyyy")) + " ")
                clsCommon.MyExportToPDF("DCS LEDGER", Gv1, arrHeader, "DCS LEDGER", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try



    End Sub

    Private Sub txtMultiMCC_My_Click(sender As Object, e As EventArgs) Handles txtMultiMCC._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where  Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        txtMultiMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Name", txtMultiMCC.arrValueMember, txtMultiMCC.arrDispalyMember)
    End Sub

    Private Sub txtRouteName_My_Click(sender As Object, e As EventArgs) Handles txtRouteName._My_Click
        Dim qry As String = " select ROUTE_NO as RouteNo, ROUTE_NAME as RouteName from TSPL_BULK_ROUTE_MASTER"
        txtRouteName.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "RouteNo", "RouteName", txtRouteName.arrValueMember, txtRouteName.arrDispalyMember)
    End Sub


    Private Sub dtpLedgerFromDate_Leave(sender As Object, e As EventArgs) Handles dtpLedgerFromDate.Leave
        If SettShowMultipleLegers = True Then
            SetToDate()
        End If
    End Sub

    Sub SetToDate()
        If Not isLoad Then

            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpLedgerFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day")
                    dtpLedgerFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    'dtpLedgerToDate.Value = dtpLedgerFromDate.Value
                    Exit Sub
                End If
                'dtpLedgerToDate.Value = dtpLedgerFromDate.Value.AddDays(PaymentCycleValue - 1)

                If dtpLedgerFromDate.Value.Month <> dtpLedgerToDate.Value.Month Then
                    'dtpLedgerToDate.Value = New Date(dtpLedgerFromDate.Value.Year, dtpLedgerFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpLedgerToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpLedgerFromDate.Value.Month <> dtNxtPay.Month Then
                    'dtpLedgerToDate.Value = New Date(dtpLedgerFromDate.Value.Year, dtpLedgerFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpLedgerFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpLedgerFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpLedgerToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpLedgerToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpLedgerFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpLedgerFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    dtpLedgerFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpLedgerToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpLedgerToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpLedgerFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpLedgerFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpLedgerFromDate.Value = today.AddDays(-dayDiff)
                dtpLedgerToDate.Value = dtpLedgerFromDate.Value.AddDays(6)
            End If


        End If
    End Sub

    Private Sub dtpLedgerFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpLedgerFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpLedgerToDate_Leave(sender As Object, e As EventArgs) Handles dtpLedgerToDate.Leave
        Try
            If SettShowMultipleLegers = True AndAlso isLoad = False Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Payment Cycle found")
                    Exit Sub
                End If
                Dim PaymentCycleType As String = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                Dim PaymentCycleValue As String = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal AndAlso clsCommon.CompairString(PaymentCycleValue, "15") = CompairStringResult.Equal Then
                    dtpLedgerToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select (CASE WHEN convert(int,day(convert(date,'" + dtpLedgerToDate.Value + "',103)))<=15 then convert(date,(select CONCAT('15','/',DATENAME(MONTH , convert(date,'" + dtpLedgerToDate.Value + "',103)),'/',YEAR(convert(date,'" + dtpLedgerToDate.Value + "',103)))) ,103 ) else EOmONTH(convert(date,'" + dtpLedgerToDate.Value + "',103)) end) as ToDate "))
                ElseIf clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal AndAlso clsCommon.CompairString(PaymentCycleValue, "10") = CompairStringResult.Equal Then
                    dtpLedgerToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select 
                                            (CASE WHEN convert(int,day(convert(date,'" + dtpLedgerToDate.Value + "',103)))<=10 then 

                                            convert(date,
                                            (select CONCAT('10','/',DATENAME(MONTH , convert(date,'" + dtpLedgerToDate.Value + "',103)),'/',YEAR(convert(date,'" + dtpLedgerToDate.Value + "',103))))
                                            ,103
                                            )

                                            WHEN convert(int,day(convert(date,'" + dtpLedgerToDate.Value + "',103)))<=20 then 

                                            convert(date,
                                            (select CONCAT('20','/',DATENAME(MONTH , convert(date,'" + dtpLedgerToDate.Value + "',103)),'/',YEAR(convert(date,'" + dtpLedgerToDate.Value + "',103))))
                                            ,103
                                            )


                                            else
                                            EOmONTH(convert(date,'" + dtpLedgerToDate.Value + "',103)) end) as ToDate "))
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Sub setFromAndToDate()
        txtFromDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
        txtToDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
    End Sub

    Private Sub txtMonth_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMonth.Validating
        If SettShowMultipleLegers = True Then
            setFromAndToDate()
        End If
    End Sub

    Private Sub btnDCSSummaryPrint_Click(sender As Object, e As EventArgs) Handles btnDCSSummaryPrint.Click
        Try
            Dim companyADD, CompName, CompCode As String
            Dim sQuery As String = ""
            sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            companyADD = dt1.Rows(0).Item("comp_address")

            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompName = dt2.Rows(0).Item("Comp_Name")


            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompCode = dt5.Rows(0).Item("Comp_Code")

            Dim fromDate As String = txtFromDate.Value
            Dim Todate As String = txtToDate.Value


            Dim whrcls As String = " where 2=2 "
            Dim whrcls1 As String = " where 2=2 "
            Dim whrclsItemWise As String = " where 2=2 "
            'txtFromDate
            'txtToDate
            whrcls += " and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + txtToDate.Value + "'),103) "
            'whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            'If clsCommon.myLen(fndLoc.Value) > 0 Then
            '    whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
            'End If
            whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + txtToDate.Value + "'),103)   "
            'whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            'whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
            'If clsCommon.myLen(fndLoc.Value) > 0 Then
            '    whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
            'End If
            'whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
            whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + txtToDate.Value + "'),103) "
            Dim strPC_FATValue As String = 0
            Dim strPC_SNFValue As String = 0
            Dim dtPC_FAT_SNF As DataTable = clsDBFuncationality.GetDataTable("select  top 1 round ( Price_Chart_FAT_Ratio * Price_Chart_Rate / nullif (Price_Chart_FAT_Per,0) , 2,1 ) as FAT_PCValue , round (  Price_Chart_SNF_Ratio * Price_Chart_Rate / nullif (Price_Chart_SNF_Per,0),2,1)  as SNF_PCValue  from TSPL_PRICE_CHART_PLANNING order by convert (date, Planning_Date,103)")
            If dtPC_FAT_SNF IsNot Nothing Or dtPC_FAT_SNF.Rows.Count > 0 Then
                strPC_FATValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("FAT_PCValue"))
                strPC_SNFValue = clsCommon.myCdbl(dtPC_FAT_SNF.Rows(0)("SNF_PCValue"))
            End If
            Dim CycleNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select max(Name) as CycleNo from TSPL_PAYMENT_CYCLE_GENERATED where convert(varchar, From_Date,103) = convert(varchar, '" + txtFromDate.Value + "',103) "))
            Dim BaseQry As String = ""
            BaseQry = "select '" + CycleNo + "' as CycleNo, TBL_BILL_DETAILS.BillNo, TBL_BILL_DETAILS.BillDate, '" + strPC_FATValue + "' as PC_FATValue, '" + strPC_SNFValue + "' as PC_SNFValue, "
            BaseQry += " isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
            BaseQry += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
            BaseQry += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode, /* TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2, */ coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
            BaseQry += " ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate" + Environment.NewLine +
            ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate" + Environment.NewLine +
            ",case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate" + Environment.NewLine +
            ",TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
            BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
                ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
            'If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            '    BaseQry += " ,coalesce(TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_SRN_HEAD.VEHICLE_CODE) as VEHICLE_CODE "
            'Else
            BaseQry += " ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE "
            'End If
            BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,case when isnull (TSPL_MILK_REJECT_DETAIL.Reject_Type,'') = '' then  'SWEET' else upper (TSPL_MILK_REJECT_DETAIL.Reject_Type) end as QBD  " + Environment.NewLine +
            " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
            BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
            BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
            BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
            BaseQry += " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine
            BaseQry += " inner join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
            BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
            BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
            BaseQry += " ) as pp group by VSP_CODE,VLC_Code"
            BaseQry += " ) as PaymentProcess on "
            BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine
            BaseQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " + Environment.NewLine
            BaseQry += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "
            BaseQry += " left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code " + Environment.NewLine +
            " left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty   
          left outer join (select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE , TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No as BillNo, convert(varchar,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103) as BillDate from TSPL_PAYMENT_PROCESS_DETAIL inner join TSPL_PAYMENT_PROCESS_HEAD on  TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
		  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + fromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + Todate + "'),103)    ) as TBL_BILL_DETAILS on TBL_BILL_DETAILS.VSP_CODE =
          TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  
          "
            BaseQry += "  " & whrcls & " " ' where Doc_No = '" + fndDocNo.Value + "'
            Dim dt As New DataTable
            sQuery = BaseQry '+ " order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103),shift desc"

            Dim DCSSummaryQuery As String = "  ; with CTE as  
                                                (  
                                                 select 1 Number  
                                                 union all  
                                                 select Number +1 from CTE where Number<31 
                                                 ) 
 select '" & CompName & "'  as CompName,'" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate, VSP_CODE,max(Vendor_Name) as Vendor_Name, sum(MorningSweetQty) as MorningSweetQty ,sum(MorningSoreQty) as MorningSoreQty,sum(MorningCurdQty) as MorningCurdQty,sum(EveningSweetQty) as EveningSweetQty,sum(EveningSoreQty) as EveningSoreQty ,sum(EveningCurdQty) as EveningCurdQty ,sum(TotalSweetQty) as TotalSweetQty ,sum(TotalSoreQty) as TotalSoreQty ,sum(TotalCurdQty) as TotalCurdQty,sum(FATQTY) * 100 / (sum(TotalSweetQty) + sum(TotalSoreQty) + sum(TotalCurdQty) )  as FATPer, Sum(SNFQTY)* 100 / (sum(TotalSweetQty) + sum(TotalSoreQty) + sum(TotalCurdQty) ) as SNFPer,max(DAYS_Total) as DAYS_Total, (sum(TotalSweetQty) + sum(TotalSoreQty) + sum(TotalCurdQty) ) /max(DAYS_Total) as AVG_QTY, (sum(TotalSweetQty) + sum(TotalSoreQty) + sum(TotalCurdQty) ) as TotalQty ,Sum([1]) as [1],Sum([2]) as [2],Sum([3]) as [3],Sum([4]) as [4],Sum([5]) as [5],Sum([6]) as [6],Sum([7]) as [7],Sum([8]) as [8],Sum([9]) as [9],Sum([10]) as [10],Sum([11]) as [11],Sum([12]) as [12],Sum([13]) as [13],Sum([14]) as [14],Sum([15]) as [15],Sum([16]) as [16],Sum([17]) as [17],Sum([18]) as [18],Sum([19]) as [19],Sum([20]) as [20],Sum([21]) as [21],Sum([22]) as [22],Sum([23]) as [23],Sum([24]) as [24],Sum([25]) as [25],Sum([26]) as [26],Sum([27]) as [27],Sum([28]) as [28],Sum([29]) as [29],Sum([30]) as [30],Sum([31]) as [31] from (
 select Number2, VSP_CODE, Vendor_Name ,MorningSweetQty,MorningSoreQty,MorningCurdQty,EveningSweetQty,EveningSoreQty,EveningCurdQty,TotalSweetQty,TotalSoreQty,TotalCurdQty
 ,FATQTY,SNFQTY,DAYS_Total
 , isnull([1],0) as [1] ,isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6],isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12],isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18],isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24],isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30],isnull([31],0) as [31]  from (
 select Number,Number as Number2, VSP_CODE, Vendor_Name, SHIFT, QBD , ROUTE_CODE , Qty, FAT_PER , SNF_PER , FATQTY, SNFQTY, SRN_Net_Amount,
 
 Case when SHIFT = 'M' and QBD = 'SWEET' then Qty else 0 end as MorningSweetQty,
 Case when SHIFT = 'M' and QBD = 'SORE' then Qty else 0 end as MorningSoreQty,
 Case when SHIFT = 'M' and QBD = 'CURD' then Qty else 0 end as MorningCurdQty,

 Case when SHIFT = 'E' and QBD = 'SWEET' then Qty else 0  end  as EveningSweetQty,  
 Case when SHIFT = 'E' and QBD = 'SORE'  then Qty else 0  end  as EveningSoreQty,
 Case when SHIFT = 'E' and QBD = 'CURD'  then Qty else 0  end  as EveningCurdQty,

 Case when  QBD = 'SWEET' then Qty else 0  end  as TotalSweetQty,
 Case when  QBD = 'SORE'  then Qty else 0  end  as TotalSoreQty,
 Case when  QBD = 'CURD'  then Qty else 0  end  as TotalCurdQty  
 ,count(VSP_CODE ) over (PARTITION BY VSP_CODE) as DAYS_Total 
 from (
select * from CTE left outer join  
 (  select DAY( convert (date,DOC_DATE,103)) as DocDay ,  DOC_DATE,VSP_CODE,max(Vendor_Name) as Vendor_Name,  SHIFT,QBD,XXXFinal.ROUTE_CODE, sum( Qty) as Qty , sum(FATQTY) * 100 / sum( Qty)  as FAT_PER , sum(SNFQTY) * 100 / sum( Qty) as SNF_PER, sum(FATQTY) as  FATQTY, sum(SNFQTY) as SNFQTY, sum (SRN_Net_Amount) as SRN_Net_Amount from ( 

" + sQuery + "
   ) XXXFinal group by DOC_DATE, VSP_CODE,QBD,SHIFT,XXXFinal.ROUTE_CODE 
			 )XXXFinal on XXXFinal.DocDay = CTE.Number ) XXXPivot
			 ) XXXXMain

 PIVOT
(
 SUM(Qty)
 FOR Number IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])
) AS PivotTable
) Final where VSP_CODE is not null	group by VSP_CODE "

            dt = clsDBFuncationality.GetDataTable(DCSSummaryQuery)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, Nothing, "rptDCSSummaryMontholyWise", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintDailySummary_Click(sender As Object, e As EventArgs) Handles btnPrintDailySummary.Click
        Try
            Dim qry As String = " select Comp_Name,Comp_City_Name,FAT_KG,SNF_KG, XXXFinal.Doc_Date , XXXFinal.Quantity , cast ( ( XXXFinal.FAT_KG * 100 /XXXFinal.Quantity) as decimal(18,2)) as FATPer , cast ( (XXXFinal.SNF_KG * 100 /XXXFinal.Quantity) as decimal(18,2)) as SNFPer from (
                                  SELECT max(TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, max(TSPL_COMPANY_MASTER.City_Code) as Comp_City_Name , CONVERT(varchar,TSPL_MILK_SRN_HEAD.Doc_Date,103) as Doc_Date, sum( cast(TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2))) AS Quantity ,sum(TSPL_MILK_SRN_DETAIL.FAT_KG) as FAT_KG, sum( TSPL_MILK_SRN_DETAIL.SNF_KG) as SNF_KG  from TSPL_MILK_SRN_DETAIL left outer join  TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                                   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MILK_SRN_HEAD.Comp_Code 
                                   left join  TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                                  where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)>=convert(date,'" + dtpDailySummaryFromDate.Value + "',103)  and CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)<=convert(date,'" + dtpDailySummaryToDate.Value + "',103) group by CONVERT(varchar,TSPL_MILK_SRN_HEAD.Doc_Date,103) ) XXXFinal  order by convert (datetime, Doc_Date,103) asc  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, Nothing, "rptDailySummaryReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btn_DCS_Ledger_Report_Click(sender As Object, e As EventArgs) Handles btn_DCS_Ledger_Report.Click
        Try
            Load_DCS_Ledger_Report_RCDF_PDF()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dtpFromDCS_Ledger_Leave(sender As Object, e As EventArgs) Handles dtpFromDCS_Ledger.Leave
        If SettShowMultipleLegers = True Then
            SetToDateDCS()
        End If
    End Sub

    Private Sub dtpFromDCS_Ledger_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDCS_Ledger.Validating
        SetToDateDCS()
    End Sub

    Private Sub dtpToDCS_Ledger_Leave(sender As Object, e As EventArgs) Handles dtpToDCS_Ledger.Leave
        Try
            If SettShowMultipleLegers = True AndAlso isLoad = False Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Payment Cycle found")
                    Exit Sub
                End If
                Dim PaymentCycleType As String = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                Dim PaymentCycleValue As String = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal AndAlso clsCommon.CompairString(PaymentCycleValue, "15") = CompairStringResult.Equal Then
                    dtpToDCS_Ledger.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select (CASE WHEN convert(int,day(convert(date,'" + dtpToDCS_Ledger.Value + "',103)))<=15 then convert(date,(select CONCAT('15','/',DATENAME(MONTH , convert(date,'" + dtpToDCS_Ledger.Value + "',103)),'/',YEAR(convert(date,'" + dtpToDCS_Ledger.Value + "',103)))) ,103 ) else EOmONTH(convert(date,'" + dtpToDCS_Ledger.Value + "',103)) end) as ToDate "))
                ElseIf clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal AndAlso clsCommon.CompairString(PaymentCycleValue, "10") = CompairStringResult.Equal Then
                    dtpToDCS_Ledger.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select 
                                            (CASE WHEN convert(int,day(convert(date,'" + dtpToDCS_Ledger.Value + "',103)))<=10 then 

                                            convert(date,
                                            (select CONCAT('10','/',DATENAME(MONTH , convert(date,'" + dtpToDCS_Ledger.Value + "',103)),'/',YEAR(convert(date,'" + dtpToDCS_Ledger.Value + "',103))))
                                            ,103
                                            )

                                            WHEN convert(int,day(convert(date,'" + dtpToDCS_Ledger.Value + "',103)))<=20 then 

                                            convert(date,
                                            (select CONCAT('20','/',DATENAME(MONTH , convert(date,'" + dtpToDCS_Ledger.Value + "',103)),'/',YEAR(convert(date,'" + dtpToDCS_Ledger.Value + "',103))))
                                            ,103
                                            )


                                            else
                                            EOmONTH(convert(date,'" + dtpToDCS_Ledger.Value + "',103)) end) as ToDate "))
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Sub SetToDateDCS()
        If Not isLoad Then

            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDCS_Ledger.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day")
                    dtpFromDCS_Ledger.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    'dtpToDCS_Ledger.Value = dtpFromDCS_Ledger.Value
                    Exit Sub
                End If
                'dtpToDCS_Ledger.Value = dtpFromDCS_Ledger.Value.AddDays(PaymentCycleValue - 1)

                If dtpFromDCS_Ledger.Value.Month <> dtpToDCS_Ledger.Value.Month Then
                    'dtpToDCS_Ledger.Value = New Date(dtpFromDCS_Ledger.Value.Year, dtpFromDCS_Ledger.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDCS_Ledger.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDCS_Ledger.Value.Month <> dtNxtPay.Month Then
                    'dtpToDCS_Ledger.Value = New Date(dtpFromDCS_Ledger.Value.Year, dtpFromDCS_Ledger.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDCS_Ledger.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDCS_Ledger.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDCS_Ledger.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDCS_Ledger.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDCS_Ledger.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDCS_Ledger.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    dtpFromDCS_Ledger.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDCS_Ledger.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDCS_Ledger.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDCS_Ledger.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDCS_Ledger.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDCS_Ledger.Value = today.AddDays(-dayDiff)
                dtpToDCS_Ledger.Value = dtpFromDCS_Ledger.Value.AddDays(6)
            End If


        End If
    End Sub

    'Private Sub btn_GainLoss_Click(sender As Object, e As EventArgs) Handles btn_GainLoss.Click
    '    Try
    '        PageSetupReport_ID = MyBase.Form_ID + "GL"
    '        If clsCommon.GetDateWithEndTime(dtpGainLossToDate.Value) < clsCommon.GetDateWithStartTime(dtpGainLossFromDate.Value) Then
    '            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
    '            Exit Sub
    '        End If
    '        Dim qry As String = Nothing
    '        Dim dt As DataTable = New DataTable()
    '        Dim dtMCCDetail As DataTable = Nothing
    '        Dim dtMCCHead As DataTable = Nothing

    '        dt.Columns.Add("SNo", GetType(String))
    '        dt.Columns.Add("Date", GetType(String))
    '        dt.Columns.Add("Document_No", GetType(String))
    '        dt.Columns.Add(New DataColumn("Temp", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("Truck No", GetType(String)))
    '        dt.Columns.Add(New DataColumn("HeadWeight", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("HeadFAT", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("HeadSNF", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("HeadFATKG", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("HeadSNFKG", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add("BMC", GetType(String))
    '        dt.Columns.Add(New DataColumn("DetailWeight", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DetailFAT", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DetailSNF", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DetailFATKG", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DetailSNFKG", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DiffWeight", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DiffFATKG", System.Type.GetType("System.Decimal")))
    '        dt.Columns.Add(New DataColumn("DiffSNFKG", System.Type.GetType("System.Decimal")))

    '        'Mcc Detail
    '        qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_MCC_DETAIL.* from TSPL_MILK_COLLECTION_MCC_DETAIL
    '            left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
    '            left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
    '            where 1=1"

    '        qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
    '                ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

    '        dtMCCDetail = clsDBFuncationality.GetDataTable(qry)

    '        'Mcc Head
    '        qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,TSPL_MILK_COLLECTION_MCC.Entered_Qty
    '                ,(case when Entered_Qty>0 then (Entered_FATKg*100)/Entered_Qty else 0 end) as FAT
    '                ,(case when Entered_Qty>0 then (Entered_SNFKg*100)/Entered_Qty else 0 end) as SNF,TSPL_MILK_COLLECTION_MCC.Entered_FATKg,TSPL_MILK_COLLECTION_MCC.Entered_SNFKg,TSPL_MILK_COLLECTION_MCC.Temp from TSPL_MILK_COLLECTION_MCC
    '            where 1=1"

    '        qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
    '                 ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

    '        dtMCCHead = clsDBFuncationality.GetDataTable(qry)

    '        Dim TempdtMCCDetail As DataTable = Nothing
    '        Dim VariationQty As Decimal = 0
    '        Dim VariationFATKG As Decimal = 0
    '        Dim VariationSNFKG As Decimal = 0
    '        Dim SumQty As Decimal = 0
    '        Dim SumFATKG As Decimal = 0
    '        Dim SumSNFKG As Decimal = 0
    '        Dim AVGFAT As Decimal = 0
    '        Dim AVGSNF As Decimal = 0
    '        Dim GSumQtyHead As Decimal = 0
    '        Dim GSumFATKGHead As Decimal = 0
    '        Dim GSumSNFKGHead As Decimal = 0
    '        Dim GAVGFATHead As Decimal = 0
    '        Dim GAVGSNFHead As Decimal = 0
    '        Dim GSumQtyDetail As Decimal = 0
    '        Dim GSumFATKGDetail As Decimal = 0
    '        Dim GSumSNFKGDetail As Decimal = 0
    '        Dim GAVGFATDetail As Decimal = 0
    '        Dim GAVGSNFDetail As Decimal = 0
    '        Dim GVariationQty As Decimal = 0
    '        Dim GVariationFATKG As Decimal = 0
    '        Dim GVariationSNFKG As Decimal = 0

    '        If dtMCCHead IsNot Nothing And dtMCCHead.Rows.Count > 0 Then
    '            For i As Integer = 0 To dtMCCHead.Rows.Count - 1

    '                TempdtMCCDetail = Nothing
    '                Dim dr1 As DataRow() = dtMCCDetail.Select("[Document_No]='" + clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_No")) + "'")
    '                If dr1 IsNot Nothing AndAlso dr1.Length > 0 Then
    '                    TempdtMCCDetail = dr1.CopyToDataTable()

    '                    dt.Rows.Add(DBNull.Value, DBNull.Value, dtMCCHead.Rows(i).Item("Document_No"), dtMCCHead.Rows(i).Item("Temp"), dtMCCHead.Rows(i).Item("Vehicle_No"), Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), Math.Round(dtMCCHead.Rows(i).Item("FAT"), 2), Math.Round(dtMCCHead.Rows(i).Item("SNF"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), TempdtMCCDetail.Rows(0).Item("MCC_NAME"), TempdtMCCDetail.Rows(0).Item("Qty"), TempdtMCCDetail.Rows(0).Item("FAT"), TempdtMCCDetail.Rows(0).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(0).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(0).Item("SNFKG"), 2), DBNull.Value, DBNull.Value, DBNull.Value)
    '                    For j As Integer = 1 To TempdtMCCDetail.Rows.Count - 1
    '                        dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, TempdtMCCDetail.Rows(j).Item("MCC_NAME"), TempdtMCCDetail.Rows(j).Item("Qty"), TempdtMCCDetail.Rows(j).Item("FAT"), TempdtMCCDetail.Rows(j).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(j).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(j).Item("SNFKG"), 2), DBNull.Value, DBNull.Value, DBNull.Value)
    '                    Next

    '                    SumQty = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([Qty])", " [Qty] is not null")), 2)
    '                    SumFATKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
    '                    SumSNFKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)
    '                    VariationQty = Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty") - SumQty, 2)
    '                    VariationFATKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg") - SumFATKG, 2)
    '                    VariationSNFKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg") - SumSNFKG, 2)

    '                    If SumQty > 0 Then
    '                        AVGFAT = Math.Round(clsCommon.myCdbl((SumFATKG * 100) / SumQty), 2)
    '                        AVGSNF = Math.Round(clsCommon.myCdbl((SumSNFKG * 100) / SumQty), 2)
    '                    Else
    '                        AVGFAT = 0
    '                        AVGSNF = 0
    '                    End If

    '                    dt.Rows.Add(clsCommon.myCstr(i + 1), clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_Date")), DBNull.Value, DBNull.Value, "Total", Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), DBNull.Value, DBNull.Value, Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), "Total", SumQty, AVGFAT, AVGSNF, SumFATKG, SumSNFKG, VariationQty, VariationFATKG, VariationSNFKG)

    '                End If
    '            Next

    '            'Grand Total
    '            GSumQtyHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_Qty])", "")), 2)
    '            GSumFATKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_FATKg])", "")), 2)
    '            GSumSNFKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_SNFKg])", "")), 2)
    '            If GSumQtyHead > 0 Then
    '                GAVGFATHead = Math.Round(clsCommon.myCdbl((GSumFATKGHead * 100) / GSumQtyHead), 2)
    '                GAVGSNFHead = Math.Round(clsCommon.myCdbl((GSumSNFKGHead * 100) / GSumQtyHead), 2)
    '            Else
    '                GAVGFATHead = 0
    '                GAVGSNFHead = 0
    '            End If


    '            GSumQtyDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "")), 2)
    '            GSumFATKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "")), 2)
    '            GSumSNFKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "")), 2)
    '            If GSumQtyDetail > 0 Then
    '                GAVGFATDetail = Math.Round(clsCommon.myCdbl((GSumFATKGDetail * 100) / GSumQtyDetail), 2)
    '                GAVGSNFDetail = Math.Round(clsCommon.myCdbl((GSumSNFKGDetail * 100) / GSumQtyDetail), 2)
    '            Else
    '                GAVGFATDetail = 0
    '                GAVGSNFDetail = 0
    '            End If

    '            GVariationQty = Math.Round(GSumQtyHead - GSumQtyDetail, 2)
    '            GVariationFATKG = Math.Round(GSumFATKGHead - GSumFATKGDetail, 2)
    '            GVariationSNFKG = Math.Round(GSumSNFKGHead - GSumSNFKGDetail, 2)

    '            dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, "G.Total", GSumQtyHead, GAVGFATHead, GAVGSNFHead, GSumFATKGHead, GSumSNFKGHead, "Total", GSumQtyDetail, GAVGFATDetail, GAVGSNFDetail, GSumFATKGDetail, GSumSNFKGDetail, GVariationQty, GVariationFATKG, GVariationSNFKG)
    '        End If

    '        If dt IsNot Nothing And dt.Rows.Count > 0 Then
    '            Gv1.DataSource = Nothing
    '            Gv1.Columns.Clear()
    '            Gv1.Rows.Clear()
    '            Gv1.GroupDescriptors.Clear()
    '            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '            Gv1.ShowGroupPanel = False

    '            Gv1.EnableFiltering = True

    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            Gv1.DataSource = dt
    '            SetGridFormationOFGV1()
    '            Gv1.BestFitColumns()
    '        Else
    '            Gv1.DataSource = Nothing
    '            clsCommon.MyMessageBoxShow("No Data Found")
    '            Exit Sub
    '        End If



    '        'PDF
    '        If Gv1.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpGainLossFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpGainLossToDate.Value, "dd/MM/yyyy")) + " ")
    '            clsCommon.MyExportToPDF("Daily Gain Loss Report", Gv1, arrHeader, "Daily Gain Loss Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.Columns("Document_No").IsVisible = False
        Gv1.Columns("HeadWeight").HeaderText = "Weight"
        Gv1.Columns("HeadFAT").HeaderText = "FAT %"
        Gv1.Columns("HeadSNF").HeaderText = "SNF %"
        Gv1.Columns("HeadFATKG").HeaderText = "FAT KG"
        Gv1.Columns("HeadSNFKG").HeaderText = "SNF KG"
        Gv1.Columns("BMC").HeaderText = "Name of BMC"
        Gv1.Columns("DetailWeight").HeaderText = "Weight"
        Gv1.Columns("DetailFAT").HeaderText = "FAT %"
        Gv1.Columns("DetailSNF").HeaderText = "SNF %"
        Gv1.Columns("DetailFATKG").HeaderText = "FAT KG"
        Gv1.Columns("DetailSNFKG").HeaderText = "SNF KG"
        Gv1.Columns("DiffWeight").HeaderText = "Weight"
        Gv1.Columns("DiffFATKG").HeaderText = "FAT KG"
        Gv1.Columns("DiffSNFKG").HeaderText = "SNF KG"

    End Sub

    Private Sub btnDCSWiseAvgFatSnfPrint_Click(sender As Object, e As EventArgs) Handles btnDCSWiseAvgFatSnfPrint.Click
        Try


            Dim qry As String = " SELECT UPPER ( max( TSPL_COMPANY_MASTER.Add1 + case when len ( TSPL_COMPANY_MASTER.Add2 ) > 0 then ' '+TSPL_COMPANY_MASTER.Add2  end +  case when len( TSPL_COMPANY_MASTER.Add3) > 0 then  ' '+  TSPL_COMPANY_MASTER.Add3 end + case when len (TSPL_COMPANY_MASTER.City_Code) > 0 then ' '+ TSPL_COMPANY_MASTER.City_Code end + case when len ( TSPL_COMPANY_MASTER.Pincode ) > 0 then '-'+ TSPL_COMPANY_MASTER.Pincode end ) ) as compAddress  , max(TSPL_COMPANY_MASTER.Comp_Name) as  Comp_Name, '" + dtpDCSWiseAvgFatSnfFromDate.Text + "' as FromDate, '" + dtpDCSWiseAvgFatSnfToDate.Text + "' as ToDate, TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code,sum (TSPL_MILK_COLLECTION_DCS_DETAIL.Qty) as Qty, cast( round ((sum(TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG) * 100 / nullif (sum (TSPL_MILK_COLLECTION_DCS_DETAIL.Qty),0)),2,1)  as decimal(18,2)) as FAT , cast( round ((sum(TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG) * 100 / nullif (sum (TSPL_MILK_COLLECTION_DCS_DETAIL.Qty),0)),2,1)  as decimal(18,2)) as SNF,sum(TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG) as FATKG, sum(TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG) as SNFKG ,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name, max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ) as  VLC_Code_VLC_Uploader
                                  FROM TSPL_MILK_COLLECTION_DCS_DETAIL 
                                  left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                                  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
                                  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'
                                  where convert (date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) > = convert (date,'" + dtpDCSWiseAvgFatSnfFromDate.Value + "',103) and convert (date, TSPL_MILK_COLLECTION_DCS.Document_Date,103) < = convert (date,'" + dtpDCSWiseAvgFatSnfToDate.Value + "',103)
                                  group by TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, Nothing, "rptDCSWiseAvgFatSnfPrint", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
    '    Export(EnumExportTo.Excel)
    'End Sub

    Private Sub RadMenuItem2_Click_1(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.Excel)

        Try
            PageSetupReport_ID = MyBase.Form_ID + "GL"
            If clsCommon.GetDateWithEndTime(dtpGainLossToDate.Value) < clsCommon.GetDateWithStartTime(dtpGainLossFromDate.Value) Then
                clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
                Exit Sub
            End If
            Dim qry As String = Nothing
            Dim dt As DataTable = New DataTable()
            Dim dtMCCDetail As DataTable = Nothing
            Dim dtMCCHead As DataTable = Nothing

            dt.Columns.Add("SNo", GetType(String))
            dt.Columns.Add("Date", GetType(String))
            dt.Columns.Add("Document_No", GetType(String))
            dt.Columns.Add(New DataColumn("Temp", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("Truck No", GetType(String)))
            dt.Columns.Add(New DataColumn("HeadWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadFAT", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadSNF", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadSNFKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add("BMC", GetType(String))
            dt.Columns.Add(New DataColumn("DetailWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailFAT", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailSNF", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailSNFKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("BMC Temp", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffSNFKG", System.Type.GetType("System.Decimal")))

            'Mcc Detail
            qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_MCC_DETAIL.* from TSPL_MILK_COLLECTION_MCC_DETAIL
                left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
                left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
                where 1=1"

            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
                    ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

            dtMCCDetail = clsDBFuncationality.GetDataTable(qry)

            'Mcc Head
            qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,TSPL_MILK_COLLECTION_MCC.Entered_Qty
                    ,(case when Entered_Qty>0 then (Entered_FATKg*100)/Entered_Qty else 0 end) as FAT
                    ,(case when Entered_Qty>0 then (Entered_SNFKg*100)/Entered_Qty else 0 end) as SNF,TSPL_MILK_COLLECTION_MCC.Entered_FATKg,TSPL_MILK_COLLECTION_MCC.Entered_SNFKg,TSPL_MILK_COLLECTION_MCC.Temp from TSPL_MILK_COLLECTION_MCC
                where 1=1"

            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
                     ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

            dtMCCHead = clsDBFuncationality.GetDataTable(qry)

            Dim TempdtMCCDetail As DataTable = Nothing
            Dim VariationQty As Decimal = 0
            Dim VariationFATKG As Decimal = 0
            Dim VariationSNFKG As Decimal = 0
            Dim SumQty As Decimal = 0
            Dim SumFATKG As Decimal = 0
            Dim SumSNFKG As Decimal = 0
            Dim AVGFAT As Decimal = 0
            Dim AVGSNF As Decimal = 0
            Dim GSumQtyHead As Decimal = 0
            Dim GSumFATKGHead As Decimal = 0
            Dim GSumSNFKGHead As Decimal = 0
            Dim GAVGFATHead As Decimal = 0
            Dim GAVGSNFHead As Decimal = 0
            Dim GSumQtyDetail As Decimal = 0
            Dim GSumFATKGDetail As Decimal = 0
            Dim GSumSNFKGDetail As Decimal = 0
            Dim GAVGFATDetail As Decimal = 0
            Dim GAVGSNFDetail As Decimal = 0
            Dim GVariationQty As Decimal = 0
            Dim GVariationFATKG As Decimal = 0
            Dim GVariationSNFKG As Decimal = 0

            If dtMCCHead IsNot Nothing And dtMCCHead.Rows.Count > 0 Then
                For i As Integer = 0 To dtMCCHead.Rows.Count - 1

                    TempdtMCCDetail = Nothing
                    Dim dr1 As DataRow() = dtMCCDetail.Select("[Document_No]='" + clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_No")) + "'")
                    If dr1 IsNot Nothing AndAlso dr1.Length > 0 Then
                        TempdtMCCDetail = dr1.CopyToDataTable()

                        dt.Rows.Add(DBNull.Value, DBNull.Value, dtMCCHead.Rows(i).Item("Document_No"), dtMCCHead.Rows(i).Item("Temp"), dtMCCHead.Rows(i).Item("Vehicle_No"), Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), Math.Round(dtMCCHead.Rows(i).Item("FAT"), 2), Math.Round(dtMCCHead.Rows(i).Item("SNF"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), TempdtMCCDetail.Rows(0).Item("MCC_NAME"), TempdtMCCDetail.Rows(0).Item("Qty"), TempdtMCCDetail.Rows(0).Item("FAT"), TempdtMCCDetail.Rows(0).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(0).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(0).Item("SNFKG"), 2), Math.Round(TempdtMCCDetail.Rows(0).Item("Temp"), 2), DBNull.Value, DBNull.Value)
                        For j As Integer = 1 To TempdtMCCDetail.Rows.Count - 1
                            dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, TempdtMCCDetail.Rows(j).Item("MCC_NAME"), TempdtMCCDetail.Rows(j).Item("Qty"), TempdtMCCDetail.Rows(j).Item("FAT"), TempdtMCCDetail.Rows(j).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(j).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(j).Item("SNFKG"), 2), Math.Round(TempdtMCCDetail.Rows(j).Item("Temp"), 2), DBNull.Value, DBNull.Value)
                        Next

                        SumQty = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                        SumFATKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                        SumSNFKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)
                        VariationQty = Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty") - SumQty, 2)
                        VariationFATKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg") - SumFATKG, 2)
                        VariationSNFKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg") - SumSNFKG, 2)

                        If SumQty > 0 Then
                            AVGFAT = Math.Round(clsCommon.myCdbl((SumFATKG * 100) / SumQty), 2)
                            AVGSNF = Math.Round(clsCommon.myCdbl((SumSNFKG * 100) / SumQty), 2)
                        Else
                            AVGFAT = 0
                            AVGSNF = 0
                        End If

                        dt.Rows.Add(clsCommon.myCstr(i + 1), clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_Date")), DBNull.Value, DBNull.Value, "Total", Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), DBNull.Value, DBNull.Value, Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), "Total", SumQty, AVGFAT, AVGSNF, SumFATKG, SumSNFKG, DBNull.Value, VariationQty, VariationFATKG, VariationSNFKG)

                    End If
                Next

                'Grand Total
                GSumQtyHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_Qty])", "")), 2)
                GSumFATKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_FATKg])", "")), 2)
                GSumSNFKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_SNFKg])", "")), 2)
                If GSumQtyHead > 0 Then
                    GAVGFATHead = Math.Round(clsCommon.myCdbl((GSumFATKGHead * 100) / GSumQtyHead), 2)
                    GAVGSNFHead = Math.Round(clsCommon.myCdbl((GSumSNFKGHead * 100) / GSumQtyHead), 2)
                Else
                    GAVGFATHead = 0
                    GAVGSNFHead = 0
                End If


                GSumQtyDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "")), 2)
                GSumFATKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "")), 2)
                GSumSNFKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "")), 2)
                If GSumQtyDetail > 0 Then
                    GAVGFATDetail = Math.Round(clsCommon.myCdbl((GSumFATKGDetail * 100) / GSumQtyDetail), 2)
                    GAVGSNFDetail = Math.Round(clsCommon.myCdbl((GSumSNFKGDetail * 100) / GSumQtyDetail), 2)
                Else
                    GAVGFATDetail = 0
                    GAVGSNFDetail = 0
                End If

                GVariationQty = Math.Round(GSumQtyHead - GSumQtyDetail, 2)
                GVariationFATKG = Math.Round(GSumFATKGHead - GSumFATKGDetail, 2)
                GVariationSNFKG = Math.Round(GSumSNFKGHead - GSumSNFKGDetail, 2)

                dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, "G.Total", GSumQtyHead, GAVGFATHead, GAVGSNFHead, GSumFATKGHead, GSumSNFKGHead, "Total", GSumQtyDetail, GAVGFATDetail, GAVGSNFDetail, GSumFATKGDetail, GSumSNFKGDetail, DBNull.Value, GVariationQty, GVariationFATKG, GVariationSNFKG)
            End If

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
                Gv1.Rows.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.ShowGroupPanel = False

                Gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
                Gv1.BestFitColumns()
            Else
                Gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If



            ''PDF
            'If Gv1.Rows.Count > 0 Then
            '    Dim arrHeader As List(Of String) = New List(Of String)()
            '    arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpGainLossFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpGainLossToDate.Value, "dd/MM/yyyy")) + " ")
            '    clsCommon.MyExportToPDF("Daily Gain Loss Report", Gv1, arrHeader, "Daily Gain Loss Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + "GL"
            If clsCommon.GetDateWithEndTime(dtpGainLossToDate.Value) < clsCommon.GetDateWithStartTime(dtpGainLossFromDate.Value) Then
                clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
                Exit Sub
            End If
            Dim qry As String = Nothing
            Dim dt As DataTable = New DataTable()
            Dim dtMCCDetail As DataTable = Nothing
            Dim dtMCCHead As DataTable = Nothing

            dt.Columns.Add("SNo", GetType(String))
            dt.Columns.Add("Date", GetType(String))
            dt.Columns.Add("Document_No", GetType(String))
            dt.Columns.Add(New DataColumn("Temp", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("Truck No", GetType(String)))
            dt.Columns.Add(New DataColumn("HeadWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadFAT", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadSNF", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("HeadSNFKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add("BMC", GetType(String))
            dt.Columns.Add(New DataColumn("DetailWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailFAT", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailSNF", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DetailSNFKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffWeight", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffFATKG", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("DiffSNFKG", System.Type.GetType("System.Decimal")))

            'Mcc Detail
            qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_MCC_DETAIL.* from TSPL_MILK_COLLECTION_MCC_DETAIL
                left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
                left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
                where 1=1"

            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
                    ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

            dtMCCDetail = clsDBFuncationality.GetDataTable(qry)

            'Mcc Head
            qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.document_date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,TSPL_MILK_COLLECTION_MCC.Entered_Qty
                    ,(case when Entered_Qty>0 then (Entered_FATKg*100)/Entered_Qty else 0 end) as FAT
                    ,(case when Entered_Qty>0 then (Entered_SNFKg*100)/Entered_Qty else 0 end) as SNF,TSPL_MILK_COLLECTION_MCC.Entered_FATKg,TSPL_MILK_COLLECTION_MCC.Entered_SNFKg,TSPL_MILK_COLLECTION_MCC.Temp from TSPL_MILK_COLLECTION_MCC
                where 1=1"

            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + dtpGainLossFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + dtpGainLossToDate.Value + "'),103) 
                     ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Document_No"

            dtMCCHead = clsDBFuncationality.GetDataTable(qry)

            Dim TempdtMCCDetail As DataTable = Nothing
            Dim VariationQty As Decimal = 0
            Dim VariationFATKG As Decimal = 0
            Dim VariationSNFKG As Decimal = 0
            Dim SumQty As Decimal = 0
            Dim SumFATKG As Decimal = 0
            Dim SumSNFKG As Decimal = 0
            Dim AVGFAT As Decimal = 0
            Dim AVGSNF As Decimal = 0
            Dim GSumQtyHead As Decimal = 0
            Dim GSumFATKGHead As Decimal = 0
            Dim GSumSNFKGHead As Decimal = 0
            Dim GAVGFATHead As Decimal = 0
            Dim GAVGSNFHead As Decimal = 0
            Dim GSumQtyDetail As Decimal = 0
            Dim GSumFATKGDetail As Decimal = 0
            Dim GSumSNFKGDetail As Decimal = 0
            Dim GAVGFATDetail As Decimal = 0
            Dim GAVGSNFDetail As Decimal = 0
            Dim GVariationQty As Decimal = 0
            Dim GVariationFATKG As Decimal = 0
            Dim GVariationSNFKG As Decimal = 0

            If dtMCCHead IsNot Nothing And dtMCCHead.Rows.Count > 0 Then
                For i As Integer = 0 To dtMCCHead.Rows.Count - 1

                    TempdtMCCDetail = Nothing
                    Dim dr1 As DataRow() = dtMCCDetail.Select("[Document_No]='" + clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_No")) + "'")
                    If dr1 IsNot Nothing AndAlso dr1.Length > 0 Then
                        TempdtMCCDetail = dr1.CopyToDataTable()

                        dt.Rows.Add(DBNull.Value, DBNull.Value, dtMCCHead.Rows(i).Item("Document_No"), dtMCCHead.Rows(i).Item("Temp"), dtMCCHead.Rows(i).Item("Vehicle_No"), Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), Math.Round(dtMCCHead.Rows(i).Item("FAT"), 2), Math.Round(dtMCCHead.Rows(i).Item("SNF"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), TempdtMCCDetail.Rows(0).Item("MCC_NAME"), TempdtMCCDetail.Rows(0).Item("Qty"), TempdtMCCDetail.Rows(0).Item("FAT"), TempdtMCCDetail.Rows(0).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(0).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(0).Item("SNFKG"), 2), DBNull.Value, DBNull.Value, DBNull.Value)
                        For j As Integer = 1 To TempdtMCCDetail.Rows.Count - 1
                            dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, TempdtMCCDetail.Rows(j).Item("MCC_NAME"), TempdtMCCDetail.Rows(j).Item("Qty"), TempdtMCCDetail.Rows(j).Item("FAT"), TempdtMCCDetail.Rows(j).Item("SNF"), Math.Round(TempdtMCCDetail.Rows(j).Item("FATKG"), 2), Math.Round(TempdtMCCDetail.Rows(j).Item("SNFKG"), 2), DBNull.Value, DBNull.Value, DBNull.Value)
                        Next

                        SumQty = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                        SumFATKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                        SumSNFKG = Math.Round(clsCommon.myCdbl(TempdtMCCDetail.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)
                        VariationQty = Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty") - SumQty, 2)
                        VariationFATKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg") - SumFATKG, 2)
                        VariationSNFKG = Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg") - SumSNFKG, 2)

                        If SumQty > 0 Then
                            AVGFAT = Math.Round(clsCommon.myCdbl((SumFATKG * 100) / SumQty), 2)
                            AVGSNF = Math.Round(clsCommon.myCdbl((SumSNFKG * 100) / SumQty), 2)
                        Else
                            AVGFAT = 0
                            AVGSNF = 0
                        End If

                        dt.Rows.Add(clsCommon.myCstr(i + 1), clsCommon.myCstr(dtMCCHead.Rows(i).Item("Document_Date")), DBNull.Value, DBNull.Value, "Total", Math.Round(dtMCCHead.Rows(i).Item("Entered_Qty"), 2), DBNull.Value, DBNull.Value, Math.Round(dtMCCHead.Rows(i).Item("Entered_FATKg"), 2), Math.Round(dtMCCHead.Rows(i).Item("Entered_SNFKg"), 2), "Total", SumQty, AVGFAT, AVGSNF, SumFATKG, SumSNFKG, VariationQty, VariationFATKG, VariationSNFKG)

                    End If
                Next

                'Grand Total
                GSumQtyHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_Qty])", "")), 2)
                GSumFATKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_FATKg])", "")), 2)
                GSumSNFKGHead = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_SNFKg])", "")), 2)
                If GSumQtyHead > 0 Then
                    GAVGFATHead = Math.Round(clsCommon.myCdbl((GSumFATKGHead * 100) / GSumQtyHead), 2)
                    GAVGSNFHead = Math.Round(clsCommon.myCdbl((GSumSNFKGHead * 100) / GSumQtyHead), 2)
                Else
                    GAVGFATHead = 0
                    GAVGSNFHead = 0
                End If


                GSumQtyDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "")), 2)
                GSumFATKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "")), 2)
                GSumSNFKGDetail = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "")), 2)
                If GSumQtyDetail > 0 Then
                    GAVGFATDetail = Math.Round(clsCommon.myCdbl((GSumFATKGDetail * 100) / GSumQtyDetail), 2)
                    GAVGSNFDetail = Math.Round(clsCommon.myCdbl((GSumSNFKGDetail * 100) / GSumQtyDetail), 2)
                Else
                    GAVGFATDetail = 0
                    GAVGSNFDetail = 0
                End If

                GVariationQty = Math.Round(GSumQtyHead - GSumQtyDetail, 2)
                GVariationFATKG = Math.Round(GSumFATKGHead - GSumFATKGDetail, 2)
                GVariationSNFKG = Math.Round(GSumSNFKGHead - GSumSNFKGDetail, 2)

                dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, "G.Total", GSumQtyHead, GAVGFATHead, GAVGSNFHead, GSumFATKGHead, GSumSNFKGHead, "Total", GSumQtyDetail, GAVGFATDetail, GAVGSNFDetail, GSumFATKGDetail, GSumSNFKGDetail, GVariationQty, GVariationFATKG, GVariationSNFKG)
            End If

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
                Gv1.Rows.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.ShowGroupPanel = False

                Gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
                Gv1.BestFitColumns()
            Else
                Gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If



            'PDF
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpGainLossFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpGainLossToDate.Value, "dd/MM/yyyy")) + " ")
                clsCommon.MyExportToPDF("Daily Gain Loss Report", Gv1, arrHeader, "Daily Gain Loss Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
