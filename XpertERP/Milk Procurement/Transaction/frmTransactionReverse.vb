''updation by richa agarwal against ticket no BM00000005678,BM00000005756,BM00000005943
Imports common
Imports System.Data.SqlClient
Public Class frmTransactionReverse
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isnewentry As Boolean = False
    Public Const colSlNo As String = "colSlNo"
    Public Const colDocNo As String = "colDocNo"
    Public Const colDocDate As String = "colDocDate"
    Public Const colSelect As String = "colSelect"
    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colbtnCol As String = "colbtnCol"
    Public Const QualityCheck As String = "Quality Check"
    Public Const Weighment As String = "Weighment"
    Public Const Unloading As String = "Unloading"
    Public Const Cleaning As String = "Cleaning"
    Public Const GateEntry As String = "Gate Entry"
    Public Const GateEntryBS As String = "Gate Entry Bulk Sale"
    Public Const WeighmentBS As String = "Weighment Bulk Sale"
    Public Const LoadingBS As String = "Loading Bulk Sale"
    Public Const QualityCheckBS As String = "Quality Check Bulk Sale"
    Dim isWeighmentHistoryBulkSaleDelete As Boolean = False

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmTranReverse)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub openScreenFinder()
        Dim qry As String = " select '" & QualityCheck & "' as Name union all select '" & Weighment & "' as Name union all select '" & Cleaning & "' as Name union all select '" & Unloading & "' as Name union all select '" & GateEntry & "' as Name  union all select '" & GateEntryBS & "' as Name union all select '" & WeighmentBS & "' as Name union all select '" & LoadingBS & "' as Name union all select '" & QualityCheckBS & "' as Name"
        fndScreen.Value = clsCommon.ShowSelectForm("ScreenName", qry, "Name", "", "Name")
    End Sub
    Sub loadBlankGrid()
        Try
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            colChkBox.HeaderText = "Select "
            colChkBox.Name = colSelect
            colChkBox.ReadOnly = False
            colChkBox.Width = 50
            colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(colChkBox)

            Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "SL.No"
            repoSLNo.Name = colSlNo
            repoSLNo.Width = 60
            repoSLNo.ReadOnly = True
            repoSLNo.BestFit()
            Gv1.MasterTemplate.Columns.Add(repoSLNo)



            Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTnkrNo.FormatString = ""
            repoTnkrNo.HeaderText = "Tanker No"
            repoTnkrNo.Name = colTankerNo
            repoTnkrNo.Width = 100
            repoTnkrNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoTnkrNo)

            Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Document No"
            repoSRNNO.Name = colDocNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "Document Date"
            repoSRNDate.Name = colDocDate
            repoSRNDate.Width = 100
            repoSRNDate.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNDate)


            Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLCode.FormatString = ""
            repoLCode.HeaderText = "Loc Code"
            repoLCode.Name = colLocCode
            repoLCode.Width = 100
            repoLCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLCode)



            Dim repoLDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLDesc.FormatString = ""
            repoLDesc.HeaderText = "Loc Desc"
            repoLDesc.Name = colLocDesc
            repoLDesc.Width = 100
            repoLDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLDesc)


            Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVCode.FormatString = ""
            repoVCode.HeaderText = "Vendor Code/Customer Code"
            repoVCode.Name = colVendorCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Vendor Desc/Customer Desc "
            repoVDesc.Name = colVendorDesc
            repoVDesc.Width = 100
            repoVDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVDesc)


            Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnCol.HeaderText = "Details "
            RepobtnCol.Name = colbtnCol
            RepobtnCol.ReadOnly = False
            RepobtnCol.Width = 150
            RepobtnCol.DefaultText = "Click Here..."
            RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnCol)

            Gv1.AllowAddNewRow = False
            Gv1.AllowColumnChooser = True
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = True
            Gv1.AllowRowReorder = True
            Gv1.EnableSorting = True
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            Gv1.MasterTemplate.ShowColumnHeaders = True
            Gv1.EnableAlternatingRowColor = True
            Gv1.TableElement.TableHeaderHeight = 20
            Gv1.EnableFiltering = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadDATA()
        Try
            Dim qry As String = ""
            If clsCommon.CompairString(QualityCheck, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " select TSPL_QUALITY_CHECK.QC_No as DocNO,TSPL_QUALITY_CHECK.QC_In_Date_Time as DocDate,TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_QUALITY_CHECK.Tanker_No   from TSPL_QUALITY_CHECK  LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QUALITY_CHECK.VENDOR_CODE lEFT OUTER join tspl_location_master on tspl_location_master.location_code=TSPL_QUALITY_CHECK.location_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.QC_No=TSPL_QUALITY_CHECK.QC_No and isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')='' left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.QC_No=TSPL_QUALITY_CHECK.QC_No  where TSPL_QUALITY_CHECK.isPosted=1 and isnull(tspl_bulk_milk_srn.srn_no,'')='' and isnull(tspl_milk_unloading.qc_no,'')='' and TSPL_QUALITY_CHECK .QC_In_Date_Time  between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103)  "
            ElseIf clsCommon.CompairString(Weighment, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " select TSPL_Weighment_Detail.Weighment_No as DocNo,TSPL_Weighment_Detail.Weighment_date as DocDate,TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_Weighment_Detail.Tanker_No  from TSPL_Weighment_Detail   LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Weighment_Detail.VENDOR_CODE lEFT OUTER join tspl_location_master on tspl_location_master.location_code=TSPL_Weighment_Detail.location_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.Weighment_No =TSPL_Weighment_Detail.Weighment_No and isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Weighment_No =TSPL_Weighment_Detail.Weighment_No   where TSPL_Weighment_Detail.isPosted=1 and isnull(tspl_bulk_milk_srn.srn_no,'')='' and isnull(tspl_milk_unloading.qc_no,'')='' and TSPL_Weighment_Detail .Weighment_date   between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) "
            ElseIf clsCommon.CompairString(Unloading, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " select TSPL_MILK_UNLOADING.Unloading_No as DocNo,TSPL_MILK_UNLOADING.Unloading_Date_Time as DocDate,TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_MILK_UNLOADING.Tanker_No from TSPL_MILK_UNLOADING   left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_MILK_UNLOADING.Gate_Entry_No   LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=Tspl_Gate_Entry_Details .Vendor_Code  lEFT OUTER join tspl_location_master on tspl_location_master.location_code=Tspl_Gate_Entry_Details .location_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.Gate_Entry_No   =Tspl_Gate_Entry_Details .Gate_Entry_No   and isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''   where TSPL_MILK_UNLOADING.isPosted=1 and isnull(tspl_bulk_milk_srn.srn_no,'')='' and isnull(Tspl_Gate_Entry_Details .Gate_Entry_No ,'')<>''  and TSPL_MILK_UNLOADING .Unloading_Date_Time    between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103)"
            ElseIf clsCommon.CompairString(GateEntry, fndScreen.Value) = CompairStringResult.Equal Then
                qry = "select Tspl_Gate_Entry_Details.Gate_Entry_No as DocNo,Tspl_Gate_Entry_Details.Date_And_Time as DocDate,TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,Tspl_Gate_Entry_Details.Tanker_No  from Tspl_Gate_Entry_Details    left outer join TSPL_QUALITY_CHECK  on TSPL_QUALITY_CHECK.Gate_Entry_No =Tspl_Gate_Entry_Details .Gate_Entry_No   LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=Tspl_Gate_Entry_Details .Vendor_Code  lEFT OUTER join tspl_location_master on tspl_location_master.location_code=Tspl_Gate_Entry_Details .location_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.Gate_Entry_No   =Tspl_Gate_Entry_Details .Gate_Entry_No  and isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No    where Tspl_Gate_Entry_Details .isPosted=1 and isnull(tspl_bulk_milk_srn.srn_no,'')='' and isnull(TSPL_QUALITY_CHECK .Gate_Entry_No ,'')='' and isnull(TSPL_Weighment_Detail.Gate_Entry_No,'')=''  and Tspl_Gate_Entry_Details  .Date_And_Time     between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103)  "
            ElseIf clsCommon.CompairString(Cleaning, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " select TSPL_Cleaning.Doc_No as DocNo,TSPL_Cleaning.Start_Date_Time as DocDate,TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_Cleaning.Tanker_No from TSPL_Cleaning    left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Cleaning .Gate_Entry_No    LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=Tspl_Gate_Entry_Details .Vendor_Code  lEFT OUTER join tspl_location_master on tspl_location_master.location_code=Tspl_Gate_Entry_Details .location_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.Gate_Entry_No   =Tspl_Gate_Entry_Details .Gate_Entry_No and isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''     where TSPL_Cleaning.isPosted=1 and isnull(tspl_bulk_milk_srn.srn_no,'')='' and isnull(Tspl_Gate_Entry_Details .Gate_Entry_No ,'')<>''  and TSPL_Cleaning .Start_Date_Time     between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) "
            ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No  as DocNo,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date  as DocDate,TSPL_CUSTOMER_MASTER.Cust_Code as Vendor_Code ,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No" & _
                      " from TSPL_Quality_Check_BulkSale Left Outer Join TSPL_Dispatch_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code Left Outer Join  TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_Quality_Check_BulkSale.Weighment_No = TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_Quality_Check_BulkSale.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Quality_Check_BulkSale.Customer_Code  where TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted =1 and  TSPL_Quality_Check_BulkSale.QC_No not in (Select QC_Code from TSPL_Dispatch_BulkSale where Document_No<>'' ) and convert(date,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) between CONVERT(date ,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date ,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103)"
            ElseIf clsCommon.CompairString(LoadingBS, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as DocNo,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date as DocDate,TSPL_CUSTOMER_MASTER.Cust_Code as Vendor_Code ,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Name ,TSPL_LOCATION_MASTER .Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE left outer join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No left outer join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No =TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No left outer join TSPL_GATEENTRY_SALE  on TSPL_GATEENTRY_SALE.Document_No  =TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_GATEENTRY_SALE.Customer_Code " & _
                      " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted =1 and convert(date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103) between CONVERT(date ,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date ,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103) and not exists (Select 1 from TSPL_Quality_Check_BulkSale where TSPL_Quality_Check_BulkSale.LoadingTanker_No =TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No)"
            ElseIf clsCommon.CompairString(QualityCheckBS, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " Select TSPL_Quality_Check_BulkSale.QC_No as DocNo,TSPL_Quality_Check_BulkSale.QC_Date as DocDate,TSPL_Quality_Check_BulkSale.Customer_Code as Vendor_Code ,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Name ,TSPL_Quality_Check_BulkSale.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_Quality_Check_BulkSale.Tanker_No from TSPL_Quality_Check_BulkSale " & _
                      " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Quality_Check_BulkSale.Customer_Code " & _
                      " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Quality_Check_BulkSale.Location_Code " & _
                      " Left outer join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No =TSPL_Quality_Check_BulkSale.Weighment_No " & _
                      " where TSPL_Quality_Check_BulkSale.Posted =1 and TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted =0 and convert(date,TSPL_Quality_Check_BulkSale.QC_Date,103) between CONVERT(date ,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date ,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103) "
            ElseIf clsCommon.CompairString(GateEntryBS, fndScreen.Value) = CompairStringResult.Equal Then
                qry = " Select TSPL_GATEENTRY_SALE.Document_No as DocNo,TSPL_GATEENTRY_SALE.Document_Date as DocDate,TSPL_GATEENTRY_SALE.Customer_Code as Vendor_Code ,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Name ,TSPL_GATEENTRY_SALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_GATEENTRY_SALE.Tanker_No from TSPL_GATEENTRY_SALE " & _
                      " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_GATEENTRY_SALE.Customer_Code " & _
                      " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GATEENTRY_SALE.Location_Code where TSPL_GATEENTRY_SALE.Posted =1 and IsSaleReturn='N' " & _
                      " and convert(date,TSPL_GATEENTRY_SALE.Document_Date,103) between CONVERT(date ,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date ,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103)" & _
                      " and not exists (Select 1 from TSPL_WEIGHMENT_DETAIL_BULKSALE where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No)"

            End If

            Dim whrcls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls = " and tspl_location_master.location_code in ( " & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colDocNo).Value = dt.Rows(i)("DocNo")
                    Gv1.Rows(i).Cells(colDocDate).Value = dt.Rows(i)("DocDate")
                    Gv1.Rows(i).Cells(colTankerNo).Value = dt.Rows(i)("Tanker_No")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("Location_Code")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("Location_Desc")
                    Gv1.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    Gv1.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                Next
            Else
                loadBlankGrid()
            End If
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Reset()
        Dim dt As Date = clsCommon.GETSERVERDATE()
        'fndScreen.Value = ""
        dtpFromDate.Value = clsCommon.GetPrintDate(DateAdd(DateInterval.Month, -1, dt), "dd/MM/yyyy hh:mm:ss tt")
        dtpToDate.Value = dt
        loadBlankGrid()
        isWeighmentHistoryBulkSaleDelete = False
    End Sub

    Function AllowToSave() As Boolean
        Dim isSaved As Boolean = False
        Try
            If clsCommon.myLen(fndScreen.Value) <= 0 Then
                Throw New Exception(" Please Select Screen Name ")
            End If
            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            If Gv1 Is Nothing OrElse Gv1.Rows.Count = 0 Then
                Throw New Exception("No Document Found")
            End If
            Dim c As Integer = 0
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    c = c + 1
                End If
            Next
            If c = 0 Then
                Throw New Exception("Please select at least  one Document")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Function
    Private Sub FrmBulkMilkSRNReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnReturn.Enabled AndAlso MyBase.isModifyFlag Then
            btnReturn.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isModifyFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
    End Sub
    Private Sub FrmBulkMilkSRNReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnReturn, "Press Alt+S for Reverse & Unpost")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for Reset ")
 
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        Try
            If clsCommon.myLen(fndScreen.Value) <= 0 Then
                Throw New Exception(" Please Select Screen Name ")
            End If
            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            LoadDATA()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub Gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellClick
        If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
            If clsCommon.CompairString(QualityCheck, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmQualityCheck
                frm.SetUserMgmt(clsUserMgtCode.frmQualityCheck)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(Weighment, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmWeighment
                frm.SetUserMgmt(clsUserMgtCode.frmWeighment)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(Unloading, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmUnloading
                frm.SetUserMgmt(clsUserMgtCode.frmUnloading)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(GateEntry, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmGateEntry
                frm.SetUserMgmt(clsUserMgtCode.frmGateEntry)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(Cleaning, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmCleaning
                frm.SetUserMgmt(clsUserMgtCode.frmCleaning)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmWeighmentEntry
                frm.SetUserMgmt(clsUserMgtCode.FrmWeighmentEntry)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(LoadingBS, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmLoadingTanker
                frm.SetUserMgmt(clsUserMgtCode.FrmLoadingTanker)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(QualityCheckBS, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmQualityCheckBulkSale
                frm.SetUserMgmt(clsUserMgtCode.FrmQualityCheckBulkSale)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            ElseIf clsCommon.CompairString(GateEntryBS, fndScreen.Value) = CompairStringResult.Equal Then
                Dim frm As New FrmGateEntrySale
                frm.SetUserMgmt(clsUserMgtCode.FrmGateEntrySale)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Dim cnt As Integer = 0
        If AllowToSave() Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    cnt = cnt + 1
                End If
            Next
            If clsCommon.MyMessageBoxShow("Are you sure want unpost " & cnt & " " & fndScreen.Value & " Document(s)", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                SaveAndUnPostData()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim cnt As Integer = 0
        If AllowToSave() Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    cnt = cnt + 1
                End If
            Next
            If clsCommon.MyMessageBoxShow("Are you sure want Delete " & cnt & " " & fndScreen.Value & " Document(s)", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                DeleteData()
            End If
        End If
    End Sub
    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
        Dim isSaved As Boolean = False
        Dim arrDocLict As List(Of String) = New List(Of String)
        Dim qry As String = ""
        Dim ChllanNo As String = ""
        Dim objQc As clsQualityCheck = Nothing
        isWeighmentHistoryBulkSaleDelete = True
        Try
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    arrDocLict.Add(Gv1.Rows(i).Cells(colDocNo).Value)
                End If
            Next
            If arrDocLict IsNot Nothing AndAlso arrDocLict.Count > 0 Then
                trans = clsDBFuncationality.GetTransactin()
                If AddToHistory(arrDocLict, trans) Then
                    If clsCommon.CompairString(QualityCheck, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            objQc = clsQualityCheck.getData(arrDocLict.Item(j), common.NavigatorType.Current, trans)
                            If objQc IsNot Nothing Then
                                If clsCommon.CompairString(objQc.Doc_Type, "BulkProc") <> CompairStringResult.Equal Then
                                    isSaved = clsQCPaperSealDetail.DeleteData(objQc.Challan_No, trans)
                                    isSaved = clsQCManualSealDetail.DeleteData(objQc.Challan_No, trans)
                                End If
                                isSaved = clsQualityCheck.deleteData(False, arrDocLict.Item(j), trans)
                            End If
                        Next
                    ElseIf clsCommon.CompairString(Weighment, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsWeighment.deleteData(False, arrDocLict.Item(j), trans)
                        Next
                    ElseIf clsCommon.CompairString(Unloading, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsUnloading.deleteData(False, arrDocLict.Item(j), trans)
                        Next
                    ElseIf clsCommon.CompairString(GateEntry, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsGateEntry.deleteData(False, arrDocLict.Item(j), trans)
                        Next
                    ElseIf clsCommon.CompairString(Cleaning, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsCleaning.deleteData(False, arrDocLict.Item(j), trans)
                        Next
                    ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No in (Select QC_No  from TSPL_Quality_Check_BulkSale  where Weighment_No='" + arrDocLict.Item(j) + "')", trans)
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QUALITY_CHECK_BULKSALE where Weighment_No='" + arrDocLict.Item(j) + "'", trans)
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_LOADING_TANKER_DETAIL_BULKSALE where Weighment_No='" + arrDocLict.Item(j) + "'", trans)
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + arrDocLict.Item(j) + "'", trans)
                        Next
                    ElseIf clsCommon.CompairString(LoadingBS, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No='" + arrDocLict.Item(j) + "'", trans)
                        Next
                    ElseIf clsCommon.CompairString(QualityCheckBS, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" + arrDocLict.Item(j) + "'", trans)
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Quality_Check_BulkSale where QC_No='" + arrDocLict.Item(j) + "'", trans)
                        Next
                    ElseIf clsCommon.CompairString(GateEntryBS, fndScreen.Value) = CompairStringResult.Equal Then
                        For j As Integer = 0 To arrDocLict.Count - 1
                            isSaved = clsDBFuncationality.ExecuteNonQuery("delete from TSPL_GATEENTRY_SALE where Document_No='" + arrDocLict.Item(j) + "'", trans)
                        Next
                    End If
                End If
            End If
            If isSaved Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Deleted Successfully.")
                btnGO.PerformClick()
            Else
                trans.Rollback()
                Throw New Exception("Could Not Deleted.")
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isWeighmentHistoryBulkSaleDelete = False

        End Try

    End Sub
    Sub SaveAndUnPostData()
        Dim trans As SqlTransaction = Nothing
        Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
        Dim isSaved As Boolean = False
        Dim DocNoList As String = ""
        Dim arrDocLict As List(Of String) = New List(Of String)
        Dim qry As String = ""
        Try
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    DocNoList = DocNoList & "'" & Gv1.Rows(i).Cells(colDocNo).Value & "',"
                    arrDocLict.Add(Gv1.Rows(i).Cells(colDocNo).Value)
                End If
            Next
            If clsCommon.myLen(DocNoList) > 0 Then
                DocNoList = Microsoft.VisualBasic.Left(DocNoList, Microsoft.VisualBasic.Len(DocNoList) - 1)
            End If

            If clsCommon.myLen(DocNoList) > 0 Then
                If clsCommon.CompairString(QualityCheck, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update tspl_quality_Check set isposted=0 where qc_no in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(Weighment, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update TSPL_Weighment_Detail set isposted=0 where weighment_No in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(Unloading, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update tspl_milk_unloading set isposted=0 where unloading_no in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(GateEntry, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update Tspl_Gate_Entry_Details set isposted=0 where gate_entry_no in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(Cleaning, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update tspl_cleaning set isposted=0 where Doc_No in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update TSPL_WEIGHMENT_DETAIL_BULKSALE set Posted=0 where Weighment_No in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(LoadingBS, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update TSPL_LOADING_TANKER_DETAIL_BULKSALE set Posted=0 where LoadingTanker_No in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(QualityCheckBS, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update TSPL_Quality_Check_BulkSale set Posted=0 where QC_No in( " & DocNoList & ")"
                ElseIf clsCommon.CompairString(GateEntryBS, fndScreen.Value) = CompairStringResult.Equal Then
                    qry = " update TSPL_GATEENTRY_SALE set Posted=0 where Document_No in( " & DocNoList & ")"

                End If
            Else
                qry = ""
            End If

            If clsCommon.myLen(qry) > 0 Then
                trans = clsDBFuncationality.GetTransactin()
                If AddToHistory(arrDocLict, trans) Then
                    isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            If isSaved Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Reversed Successfully.")
                btnGO.PerformClick()
            Else
                trans.Rollback()
                Throw New Exception("Could Not Reversed.")
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Function AddToHistory(ByVal arrDocList As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim obj = Nothing
        If clsCommon.CompairString(QualityCheck, fndScreen.Value) = CompairStringResult.Equal Then

            Dim ChllanNo As String = ""
            Dim arrPaperSeal As List(Of clsQCPaperSealDetail) = Nothing
            Dim arrManualSeal As List(Of clsQCManualSealDetail) = Nothing
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsQualityCheck.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    If clsQualityCheck.saveData(obj, trans, True) Then
                        If clsCommon.CompairString(obj.Doc_Type, "BulkProc") <> CompairStringResult.Equal Then
                            arrPaperSeal = clsQCPaperSealDetail.getData(obj.Challan_No, trans)
                            arrManualSeal = clsQCManualSealDetail.getData(obj.Challan_No, trans)
                            If arrPaperSeal IsNot Nothing AndAlso arrPaperSeal.Count > 0 Then
                                isSaved = clsQCPaperSealDetail.SaveData(arrPaperSeal, trans, True)
                            End If
                            If arrManualSeal IsNot Nothing AndAlso arrManualSeal.Count > 0 Then
                                isSaved = clsQCManualSealDetail.SaveData(arrManualSeal, trans, True)
                            End If
                        End If
                    End If
                End If
            Next
        ElseIf clsCommon.CompairString(Weighment, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsWeighment.getData(arrDocList.Item(i), NavigatorType.Current, False, trans)
                If obj IsNot Nothing Then
                    isSaved = clsWeighment.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Unloading, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsUnloading.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsUnloading.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(GateEntry, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsGateEntry.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsGateEntry.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Cleaning, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsCleaning.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsCleaning.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal AndAlso isWeighmentHistoryBulkSaleDelete = False Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsWeighmentEntry.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsWeighmentEntry.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(LoadingBS, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsLoadingTanker.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsLoadingTanker.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(QualityCheckBS, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsQualityCheckBulkSale.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsQualityCheckBulkSale.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(GateEntryBS, fndScreen.Value) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsGateEntrySale.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsGateEntrySale.SaveDataHistory(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(WeighmentBS, fndScreen.Value) = CompairStringResult.Equal AndAlso isWeighmentHistoryBulkSaleDelete = True Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsWeighmentEntry.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsWeighmentEntry.SaveData(obj, trans, True)
                End If

                Dim strLoadingNo As String = clsDBFuncationality.getSingleValue("Select LoadingTanker_No  from TSPL_LOADING_TANKER_DETAIL_BULKSALE where Weighment_No ='" & arrDocList.Item(i) & "'", trans)
                obj = ClsLoadingTanker.GetData(strLoadingNo, "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsLoadingTanker.SaveData(obj, trans, True)
                End If
                Dim strQCNo As String = clsDBFuncationality.getSingleValue("Select QC_No  from TSPL_Quality_Check_BulkSale  where Weighment_No ='" & arrDocList.Item(i) & "'", trans)
                obj = ClsQualityCheckBulkSale.GetData(strQCNo, "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsQualityCheckBulkSale.SaveData(obj, trans, True)
                End If
            Next
        End If
        Return True
    End Function
    Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckAll()
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        UnCheckAll()
    End Sub
    Sub UnCheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = False
            Next
        End If
    End Sub
    Sub CheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = True
            Next
        End If
    End Sub

    Private Sub fndScreen__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndScreen._MYValidating
        openScreenFinder()
        loadBlankGrid()
    End Sub
End Class
