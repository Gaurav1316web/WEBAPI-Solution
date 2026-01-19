Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class CancelPurchaseInvoiceReport
    Inherits FrmMainTranScreen

    Dim tblCanDel As String = Nothing
    Dim colCanDel As String = Nothing

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            colCanDel = Nothing
            If rbtnCancel.Checked Then
                colCanDel = "Cancel"
            Else
                colCanDel = "Delete"
            End If
            Dim qry As String = " select Ref_No as TenderNo, Remarks as RefName from TSPL_GRN_HEAD_" & colCanDel & "_Data "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "TenderNo", "", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
    '    Try
    '        Dim qry As String = "select Ref_No as TenderNo from TSPL_PI_HEAD_Cancel_Data"
    '        TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm(
    '        "TransTypeMulSel",
    '        qry,
    '        "TenderNo",
    '        "",
    '        TxtRAL.arrValueMember,
    '        Nothing
    '    )

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    'Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
    '    Try
    '        If TxtRAL Is Nothing Then
    '            MessageBox.Show("TxtRAL is not initialized.")
    '            Exit Sub
    '        End If

    '        Dim qry As String = "SELECT DISTINCT Ref_No AS TenderNo FROM TSPL_PI_HEAD_Cancel_Data"
    '        Dim currentValues = If(TxtRAL.arrValueMember, "")

    '        TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm(
    '        "TransTypeMulSel",
    '        qry,
    '        "TenderNo",
    '        "",
    '        currentValues,
    '        Nothing
    '    )

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " Select Item_Code as Code,Item_Desc as Name,Short_Description,Structure_Code from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funreset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.arrValueMember = Nothing
        TxtRAL.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        TxtVendor.arrValueMember = Nothing

        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        EnableDisableCtrl(True)
    End Sub

    Private Sub CancelPurchaseInvoiceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE
            txtToDate.Value = clsCommon.GETSERVERDATE
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Close()
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Cancel Purchase Invoice Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Cancel Purchase Invoice Report", gv1, arrHeader, "Cancel Purchase Invoice Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub


    'Private Sub LoadData()
    '    'Dim qry As String = Nothing
    '    'Dim qryy As String = Nothing

    '    Try
    '        Dim Whr As String = ""
    '        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '            Whr += " and TSPL_LOCATION_MASTER.Location_Code In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
    '        End If

    '        If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
    '            Whr += " and TSPL_PI_HEAD_Cancel_Data.Ref_No In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ")"
    '        End If

    '        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
    '            Whr += " and TSPL_ITEM_MASTER.Item_Code In (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
    '        End If

    '        If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
    '            Whr += " and TSPL_VENDOR_MASTER.Vendor_Code In (" + clsCommon.GetMulcallString(TxtVendor.arrValueMember) + ")"
    '        End If

    '        If chkAllData.Checked Then
    '            Dim qryy As String = "SELECT DISTINCT TSPL_GRN_HEAD.GRN_No AS [GRN No],
    '                            CASE  WHEN TSPL_GRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_GRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [GRN Status],
    '                            TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No],
    '                            CASE  WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [Weighment Status],
    '                            TSPL_MRN_HEAD.MRN_No AS [MRN No],
    '                            CASE  WHEN TSPL_MRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_MRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [MRN Status],
    '                            TSPL_NIR_QC.Document_No as[NIR QC NO],
    '                            CASE  WHEN TSPL_NIR_QC.Status = 0 THEN 'Pending' WHEN TSPL_NIR_QC.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [NIR QC Status],
    '                            TSPL_QC_CHECK_SRN_DETAIL.Document_Code as [Wet Qc No],
    '				TSPL_QC_CHECK_DETAIL.QC_Status as [Incoming Qc Status],
    '                            TSPL_SRN_HEAD.SRN_No AS [SRN No],
    '                            CASE  WHEN TSPL_SRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_SRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [SRN Status],
    '					TSPL_TENDER_PENALTY_DETAIL.Document_No as [Ral Penalty],
    '				TSPL_PI_DETAIL.PO_ID AS [PO No],
    '                            TSPL_PI_HEAD.PI_No AS [PI No],
    '                            CASE  WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 0 THEN 'Pending' WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 1 THEN 'Cancel' 
    '                             ELSE 'Unknown' END AS [PI Status],TSPL_LOCATION_MASTER.Location_Code as [Location Code],
    '                            TSPL_PI_HEAD_Cancel_Data.Ref_No as [Tender],TSPL_ITEM_MASTER.Item_Code as [Item],TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code]
    '                             FROM TSPL_PI_DETAIL_Cancel_Data
    '					left outer join TSPL_PI_HEAD_Cancel_Data on TSPL_PI_HEAD_Cancel_Data.Against_GRN=TSPL_PI_DETAIL_Cancel_Data.GRN_ID
    '                             left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_HEAD_Cancel_Data.PI_No
    '                                LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD_Cancel_Data.PI_No
    '                             left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_PI_HEAD_Cancel_Data.Against_GRN
    '                             left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
    '                             left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD_Cancel_Data.Against_SRN
    '                             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
    '                             left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PI_HEAD_Cancel_Data.Vendor_Code
    '                             left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PO_WEIGHTMENT_HEAD.Location_Code
    '					left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.Item_Code=TSPL_PI_DETAIL_Cancel_Data.Item_Code
    '					left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.SRN_No=TSPL_PI_DETAIL_Cancel_Data.SRN_Id
    '                                WHERE  (Convert(date,TSPL_PI_HEAD_Cancel_Data.Cancel_On ,103) BETWEEN convert(date,'" + txtFromDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103)) " + Whr + " "
    '        Else
    '            Dim qry As String = "SELECT DISTINCT TSPL_GRN_HEAD.GRN_No AS [GRN No],
    '                            CASE  WHEN TSPL_GRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_GRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [GRN Status],
    '                            TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No],
    '                            CASE  WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [Weighment Status],
    '                            TSPL_MRN_HEAD.MRN_No AS [MRN No],
    '                            CASE  WHEN TSPL_MRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_MRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [MRN Status],
    '                            TSPL_NIR_QC.Document_No as[NIR QC NO],
    '                            CASE  WHEN TSPL_NIR_QC.Status = 0 THEN 'Pending' WHEN TSPL_NIR_QC.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [NIR QC Status],
    '                            TSPL_QC_CHECK_SRN_DETAIL.Document_Code as [Wet Qc No],
    '				TSPL_QC_CHECK_DETAIL.QC_Status as [Incoming Qc Status],
    '                            TSPL_SRN_HEAD.SRN_No AS [SRN No],
    '                            CASE  WHEN TSPL_SRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_SRN_HEAD.Status = 1 THEN 'Approved' 
    '                             ELSE 'Unknown' END AS [SRN Status],
    '					TSPL_TENDER_PENALTY_DETAIL.Document_No as [Ral Penalty],
    '				TSPL_PI_DETAIL_Cancel_Data.PO_ID AS [PO No],
    '                            TSPL_PI_DETAIL_Cancel_Data.PI_No AS [PI No],
    '                            CASE  WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 0 THEN 'Pending' WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 1 THEN 'Cancel' 
    '                             ELSE 'Unknown' END AS [PI Status],TSPL_LOCATION_MASTER.Location_Code as [Location Code],
    '                            TSPL_PI_HEAD_Cancel_Data.Ref_No as [Tender],TSPL_ITEM_MASTER.Item_Code as [Item],TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code]
    '                             FROM TSPL_PI_DETAIL_Cancel_Data
    '					left outer join TSPL_PI_HEAD_Cancel_Data on TSPL_PI_HEAD_Cancel_Data.Against_GRN=TSPL_PI_DETAIL_Cancel_Data.GRN_ID
    '                             left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_HEAD_Cancel_Data.PI_No
    '                             left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_PI_HEAD_Cancel_Data.Against_GRN
    '                             left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
    '                             left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
    '                             left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD_Cancel_Data.Against_SRN
    '                             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
    '                             left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PI_HEAD_Cancel_Data.Vendor_Code
    '                             left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PO_WEIGHTMENT_HEAD.Location_Code
    '					left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.Item_Code=TSPL_PI_DETAIL_Cancel_Data.Item_Code
    '					left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.SRN_No=TSPL_PI_DETAIL_Cancel_Data.SRN_Id
    '                                WHERE  (Convert(date,TSPL_PI_HEAD_Cancel_Data.Cancel_On ,103) BETWEEN convert(date,'" + txtFromDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103)) " + Whr + " "
    '        End If
    '        Dim dt = clsDBFuncationality.GetDataTable(qry)

    '        gv1.DataSource = Nothing
    '        gv1.Rows.Clear()
    '        gv1.Columns.Clear()
    '        gv1.GroupDescriptors.Clear()
    '        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        gv1.MasterView.Refresh()

    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            gv1.DataSource = dt
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            gv1.EnableFiltering = True
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
    '        End If
    '        gv1.BestFitColumns()

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub



    Private Sub LoadData()
        Try
            Dim Whr As String = ""
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Whr &= " AND TSPL_LOCATION_MASTER.Location_Code IN (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                Whr &= " AND TSPL_GRN_HEAD.Ref_No IN (" & clsCommon.GetMulcallString(TxtRAL.arrValueMember) & ")"
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                Whr &= " AND TSPL_ITEM_MASTER.Item_Code IN (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ")"
            End If
            If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
                Whr &= " AND TSPL_VENDOR_MASTER.Vendor_Code IN (" & clsCommon.GetMulcallString(TxtVendor.arrValueMember) & ")"
            End If
            If objCommonVar.ApplyLocationFilterBasedOnPermission AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                Whr &= " and TSPL_LOCATION_MASTER.Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If

            tblCanDel = Nothing
            colCanDel = Nothing
            If rbtnCancel.Checked Then
                tblCanDel = "_Cancel_Data"
                colCanDel = "Cancel"
            Else
                tblCanDel = "_Delete_Data"
                colCanDel = "Delete"
            End If

            Dim qry As String = "SELECT TSPL_GRN_HEAD.GRN_No AS [GRN No],Convert(Varchar(10),TSPL_GRN_HEAD.GRN_Date,103) As [GRN Date],Convert(Decimal(18,2),TSPL_GRN_HEAD.GRN_Qty) As [GRN Qty] ,
CASE WHEN TSPL_GRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_GRN_HEAD.Status = 1 THEN 'Approved' ELSE Null END AS [GRN Status],
Convert(Varchar(10),TSPL_GRN_HEAD." & colCanDel & "_On,103) As [GRN " & colCanDel & " Date],TSPL_GRN_HEAD." & colCanDel & "_By As [GRN " & colCanDel & " By],
TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code AS [Weighment No],Convert(Varchar(10),TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) As [Weighment Date],TSPL_PO_WEIGHTMENT_HEAD.Gross_Weight As [Gross Weight],
TSPL_PO_WEIGHTMENT_HEAD.Tare_Weight As [Tare Weight],TSPL_PO_WEIGHTMENT_HEAD.Net_Weight As [Net Weight],
CASE WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 1 THEN 'Approved'  ELSE Null END AS [Weighment Status],
Convert(Varchar(10),TSPL_PO_WEIGHTMENT_HEAD." & colCanDel & "_On,103) As [PO Weighment " & colCanDel & " Date],TSPL_PO_WEIGHTMENT_HEAD." & colCanDel & "_By As [PO Weighment " & colCanDel & " By],
TSPL_MRN_HEAD.MRN_No AS [MRN No],Convert(Varchar(10),TSPL_MRN_HEAD.MRN_Date,103) As [MRN Date],Convert(Decimal(18,2),TSPL_MRN_HEAD.MRN_Qty) As [MRN Qty],
CASE WHEN TSPL_MRN_HEAD.Status = 0 THEN 'Pending'  WHEN TSPL_MRN_HEAD.Status = 1 THEN 'Approved'  ELSE Null END AS [MRN Status],
Convert(Varchar(10),TSPL_MRN_HEAD." & colCanDel & "_On,103) As [MRN " & colCanDel & " Date],TSPL_MRN_HEAD." & colCanDel & "_By As [MRN " & colCanDel & " By],
TSPL_NIR_QC.Document_No AS [NIR QC No],Convert(Varchar(10),TSPL_NIR_QC.Document_Date,103) AS [NIR QC Date],
CASE WHEN TSPL_NIR_QC.Status = 0 THEN 'Pending'  WHEN TSPL_NIR_QC.Status = 1 THEN 'Approved' ELSE Null END AS [NIR QC Status],
Convert(Varchar(10),TSPL_NIR_QC." & colCanDel & "_On,103) As [NIR QC " & colCanDel & " Date],TSPL_NIR_QC." & colCanDel & "_By As [NIR QC " & colCanDel & " By] ,
TSPL_QC_CHECK_DETAIL.Document_Code AS [Wet QC No],TSPL_QC_CHECK_DETAIL.QC_Status AS [Incoming QC Status],
Convert(Varchar(10),TSPL_QC_CHECK_DETAIL." & colCanDel & "_On,103) As [WET QC " & colCanDel & " Date],TSPL_QC_CHECK_DETAIL." & colCanDel & "_By As [WET QC " & colCanDel & " By],
TSPL_SRN_HEAD.SRN_No AS [SRN No],Convert(Varchar(10),TSPL_SRN_HEAD.SRN_Date,103) As [SRN Date],Convert(Decimal(18,2),TSPL_SRN_HEAD.SRN_Qty) As [SRN Qty],
CASE WHEN TSPL_SRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_SRN_HEAD.Status = 1 THEN 'Approved' ELSE Null END AS [SRN Status],
Convert(Varchar(10),TSPL_SRN_HEAD." & colCanDel & "_On,103) As [SRN " & colCanDel & " Date],TSPL_SRN_HEAD." & colCanDel & "_By As [SRN " & colCanDel & " By],
TSPL_TENDER_PENALTY.Document_No AS [RAL Penalty],
CASE WHEN TSPL_TENDER_PENALTY.Status = 0 THEN 'Pending'WHEN TSPL_TENDER_PENALTY.Status = 1 THEN 'Approved' ELSE Null END AS [RAL Penalty Status],
Convert(Varchar(10),TSPL_TENDER_PENALTY." & colCanDel & "_On,103) As [RAL Penalty " & colCanDel & " Date],TSPL_TENDER_PENALTY." & colCanDel & "_By As [RAL Penalty " & colCanDel & " By],
TSPL_PI_HEAD.PI_No AS [PI No],Convert(Varchar(10),TSPL_PI_HEAD.PI_Date,103) As [PI Date],Convert(Decimal(18,2),TSPL_PI_HEAD.PI_Qty) As [PI Qty],
CASE WHEN TSPL_PI_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_PI_HEAD.Status = 1 THEN 'Approved' ELSE Null END AS [PI Status],
Convert(Varchar(10),TSPL_PI_HEAD." & colCanDel & "_On,103) As [PI " & colCanDel & " Date],TSPL_PI_HEAD." & colCanDel & "_By As [PI " & colCanDel & " By],
TSPL_ITEM_MASTER.Item_Code AS [Item Code],TSPL_ITEM_MASTER.Item_Desc As [Item Desc],
TSPL_VENDOR_MASTER.Vendor_Code AS [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name],TSPL_LOCATION_MASTER.Location_Code AS [Location Code],
TSPL_LOCATION_MASTER.Location_Desc AS [Location Desc]
FROM 
(select TSPL_GRN_HEAD.GRN_No,TSPL_GRN_HEAD.GRN_Date,TSPL_GRN_DETAIL.GRN_Qty,TSPL_GRN_Detail.Item_Code,TSPL_GRN_Detail.Unit_code,TSPL_GRN_HEAD.Vendor_Code,TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Status,
Null As " & colCanDel & "_On,Null As " & colCanDel & "_By 
from TSPL_GRN_DETAIL
Left Outer Join TSPL_GRN_HEAD On TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No
where 1=1
and CONVERT(date, TSPL_GRN_HEAD.GRN_Date, 103) >= CONVERT(date, '" & txtFromDate.Value & "', 103) 
and CONVERT(date, TSPL_GRN_HEAD.GRN_Date, 103) <= CONVERT(date, '" & txtToDate.Value & "', 103)  
Union All
select TSPL_GRN_HEAD" & tblCanDel & ".GRN_No,TSPL_GRN_HEAD" & tblCanDel & ".GRN_Date,GRN_Qty,TSPL_GRN_DETAIL" & tblCanDel & ".Item_Code,TSPL_GRN_DETAIL" & tblCanDel & ".Unit_code,Vendor_Code,TSPL_GRN_HEAD" & tblCanDel & ".Bill_To_Location,TSPL_GRN_HEAD" & tblCanDel & ".Status,TSPL_GRN_HEAD" & tblCanDel & "." & colCanDel & "_On,TSPL_GRN_HEAD" & tblCanDel & "." & colCanDel & "_By 
from TSPL_GRN_Detail" & tblCanDel & " 
Left Outer Join TSPL_GRN_HEAD" & tblCanDel & " On TSPL_GRN_HEAD" & tblCanDel & ".GRN_No=TSPL_GRN_Detail" & tblCanDel & ".GRN_No
where 1=1
and CONVERT(date, TSPL_GRN_HEAD" & tblCanDel & "." & colCanDel & "_On, 103) >= CONVERT(date, '" & txtFromDate.Value & "', 103) 
and CONVERT(date, TSPL_GRN_HEAD" & tblCanDel & "." & colCanDel & "_On, 103) <= CONVERT(date, '" & txtToDate.Value & "', 103)  
)TSPL_GRN_HEAD
Left Outer Join( 
Select TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL.Item_Code,TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight,
TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,
TSPL_PO_WEIGHTMENT_HEAD.Status ,Null As " & colCanDel & "_On,Null As " & colCanDel & "_By,TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No 
from TSPL_PO_WEIGHTMENT_DETAIL 
LEFT Outer Join TSPL_PO_WEIGHTMENT_HEAD On TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
Union All
Select TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & ".Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & ".Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".Item_Code,TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".UOM,TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".Gross_Weight,
TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".Tare_Weight,TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".Net_Weight,
TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & ".Status ,TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & "." & colCanDel & "_On,TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & "." & colCanDel & "_By,TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & ".Against_GRN_No 
from TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & " 
LEFT Outer Join TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & " On TSPL_PO_WEIGHTMENT_HEAD" & tblCanDel & ".Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL" & tblCanDel & ".Weighment_Code)TSPL_PO_WEIGHTMENT_HEAD On TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No 
--And  TSPL_PO_WEIGHTMENT_HEAD.Item_Code=TSPL_GRN_HEAD.Item_Code And  TSPL_PO_WEIGHTMENT_HEAD.UOM=TSPL_GRN_HEAD.Unit_code

LEFT Outer Join(
Select TSPL_MRN_HEAD.MRN_No,TSPL_MRN_HEAD.MRN_Date,TSPL_MRN_DETAIL.Item_Code,TSPL_MRN_DETAIL.Unit_code,TSPL_MRN_DETAIL.MRN_Qty,TSPL_MRN_HEAD.Status,Null As " & colCanDel & "_On,Null As " & colCanDel & "_By,TSPL_MRN_HEAD.Against_GRN
from TSPL_MRN_DETAIL  
LEFT OUTER JOIN TSPL_MRN_HEAD  ON TSPL_MRN_HEAD.MRN_No = TSPL_MRN_DETAIL.MRN_No
Union All
Select TSPL_MRN_HEAD" & tblCanDel & ".MRN_No,TSPL_MRN_HEAD" & tblCanDel & ".MRN_Date,TSPL_MRN_DETAIL" & tblCanDel & ".Item_Code,TSPL_MRN_DETAIL" & tblCanDel & ".Unit_code,TSPL_MRN_DETAIL" & tblCanDel & ".MRN_Qty,
TSPL_MRN_HEAD" & tblCanDel & ".Status,TSPL_MRN_HEAD" & tblCanDel & "." & colCanDel & "_On,TSPL_MRN_HEAD" & tblCanDel & "." & colCanDel & "_By,TSPL_MRN_HEAD" & tblCanDel & ".Against_GRN
from TSPL_MRN_DETAIL" & tblCanDel & "  
LEFT OUTER JOIN TSPL_MRN_HEAD" & tblCanDel & "  ON TSPL_MRN_HEAD" & tblCanDel & ".MRN_No = TSPL_MRN_DETAIL" & tblCanDel & ".MRN_No)TSPL_MRN_HEAD On TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No And  TSPL_MRN_HEAD.Item_Code=TSPL_PO_WEIGHTMENT_HEAD.Item_Code And  TSPL_MRN_HEAD.Unit_code=TSPL_PO_WEIGHTMENT_HEAD.UOM
LEFT Outer Join(
Select Document_No,Document_Date,MRN_No,Status,Null As " & colCanDel & "_On,Null As " & colCanDel & "_By from TSPL_NIR_QC --Where MRN_No=TSPL_MRN_HEAD.MRN_No
Union All
Select Document_No,Document_Date,MRN_No,Status," & colCanDel & "_On," & colCanDel & "_By from TSPL_NIR_QC" & tblCanDel & " --Where MRN_No=TSPL_MRN_HEAD.MRN_No
)TSPL_NIR_QC On TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No

Left Outer Join(
Select TSPL_QC_CHECK_DETAIL.Document_Code,TSPL_QC_CHECK_DETAIL.QC_Status,TSPL_QC_CHECK_DETAIL.Item_Code,TSPL_QC_CHECK_DETAIL.Unit_Code,
TSPL_QC_CHECK_DETAIL.MRN_No,Null As " & colCanDel & "_On,Null As " & colCanDel & "_By  from TSPL_QC_CHECK_SRN_DETAIL                 
LEFT OUTER JOIN TSPL_QC_CHECK_DETAIL  ON TSPL_QC_CHECK_DETAIL.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code And TSPL_QC_CHECK_DETAIL.Line_No=TSPL_QC_CHECK_SRN_DETAIL.Line_No
Union All
Select TSPL_QC_CHECK_DETAIL" & tblCanDel & ".Document_Code,TSPL_QC_CHECK_DETAIL" & tblCanDel & ".QC_Status,TSPL_QC_CHECK_DETAIL" & tblCanDel & ".Item_Code,TSPL_QC_CHECK_DETAIL" & tblCanDel & ".Unit_Code,TSPL_QC_CHECK_DETAIL" & tblCanDel & ".MRN_No,TSPL_QC_CHECK_DETAIL" & tblCanDel & "." & colCanDel & "_On,TSPL_QC_CHECK_DETAIL" & tblCanDel & "." & colCanDel & "_By 
from TSPL_QC_CHECK_SRN_DETAIL                  
LEFT OUTER JOIN TSPL_QC_CHECK_DETAIL" & tblCanDel & "  ON TSPL_QC_CHECK_DETAIL" & tblCanDel & ".Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code
And TSPL_QC_CHECK_DETAIL" & tblCanDel & ".Line_No=TSPL_QC_CHECK_SRN_DETAIL.Line_No
)TSPL_QC_CHECK_DETAIL On TSPL_QC_CHECK_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No
--And TSPL_QC_CHECK_DETAIL.Item_Code=TSPL_MRN_HEAD.Item_Code And TSPL_QC_CHECK_DETAIL.Unit_Code=TSPL_MRN_HEAD.Unit_code


LEFT Outer Join (
Select TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date,TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.SRN_Qty,
TSPL_SRN_HEAD.Status,
Null As " & colCanDel & "_On,Null As " & colCanDel & "_By,TSPL_SRN_HEAD.Against_GRN,TSPL_SRN_HEAD.Against_MRN
from  TSPL_SRN_DETAIL 
LEFT OUTER JOIN TSPL_SRN_HEAD On TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
Union All
Select TSPL_SRN_HEAD" & tblCanDel & ".SRN_No,TSPL_SRN_HEAD" & tblCanDel & ".SRN_Date,TSPL_SRN_DETAIL" & tblCanDel & ".Item_Code,TSPL_SRN_DETAIL" & tblCanDel & ".Unit_code,TSPL_SRN_DETAIL" & tblCanDel & ".SRN_Qty,
TSPL_SRN_HEAD" & tblCanDel & ".Status,
TSPL_SRN_HEAD" & tblCanDel & "." & colCanDel & "_On,TSPL_SRN_HEAD" & tblCanDel & "." & colCanDel & "_By,TSPL_SRN_HEAD" & tblCanDel & ".Against_GRN,TSPL_SRN_HEAD" & tblCanDel & ".Against_MRN
from  TSPL_SRN_DETAIL" & tblCanDel & "  
LEFT OUTER JOIN TSPL_SRN_HEAD" & tblCanDel & " On TSPL_SRN_HEAD" & tblCanDel & ".SRN_No = TSPL_SRN_DETAIL" & tblCanDel & ".SRN_No)TSPL_SRN_HEAD On TSPL_SRN_HEAD.Against_MRN=TSPL_MRN_HEAD.MRN_No And TSPL_SRN_HEAD.Item_Code=TSPL_MRN_HEAD.Item_Code And TSPL_SRN_HEAD.Unit_code=TSPL_MRN_HEAD.Unit_code

LEFT Outer Join(
Select TSPL_TENDER_PENALTY.Document_No,TSPL_TENDER_PENALTY.Status,Null As " & colCanDel & "_On,Null As " & colCanDel & "_By,TSPL_TENDER_PENALTY_DETAIL.SRN_No,TSPL_TENDER_PENALTY.Item_Code 
From TSPL_TENDER_PENALTY_DETAIL
LEFT Outer Join TSPL_TENDER_PENALTY On TSPL_TENDER_PENALTY.Document_No=TSPL_TENDER_PENALTY_DETAIL.Document_No
Union All
Select TSPL_TENDER_PENALTY" & tblCanDel & ".Document_No,TSPL_TENDER_PENALTY" & tblCanDel & ".Status,
TSPL_TENDER_PENALTY" & tblCanDel & "." & colCanDel & "_On,TSPL_TENDER_PENALTY" & tblCanDel & "." & colCanDel & "_By,TSPL_TENDER_PENALTY_DETAIL" & tblCanDel & ".SRN_No,TSPL_TENDER_PENALTY" & tblCanDel & ".Item_Code  
From TSPL_TENDER_PENALTY_DETAIL" & tblCanDel & "
LEFT Outer Join TSPL_TENDER_PENALTY" & tblCanDel & " On TSPL_TENDER_PENALTY" & tblCanDel & ".Document_No=TSPL_TENDER_PENALTY_DETAIL" & tblCanDel & ".Document_No)TSPL_TENDER_PENALTY On TSPL_TENDER_PENALTY.SRN_No=TSPL_SRN_HEAD.SRN_No --And TSPL_TENDER_PENALTY.Item_Code=TSPL_SRN_HEAD.Item_Code

LEFT outer join( 
Select TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_DETAIL.PI_Qty,TSPL_PI_HEAD.Status ,
Null As " & colCanDel & "_On,Null As " & colCanDel & "_By,TSPL_PI_DETAIL.SRN_Id,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Unit_code
from TSPL_PI_DETAIL 
LEFT OUTER JOIN TSPL_PI_HEAD On TSPL_PI_HEAD.PI_No = TSPL_PI_DETAIL.PI_No
Union All
Select TSPL_PI_HEAD" & tblCanDel & ".PI_No,TSPL_PI_HEAD" & tblCanDel & ".PI_Date,TSPL_PI_DETAIL" & tblCanDel & ".PI_Qty,TSPL_PI_HEAD" & tblCanDel & ".Status ,
TSPL_PI_HEAD" & tblCanDel & "." & colCanDel & "_On,TSPL_PI_HEAD" & tblCanDel & "." & colCanDel & "_By,TSPL_PI_DETAIL" & tblCanDel & ".SRN_Id,TSPL_PI_DETAIL" & tblCanDel & ".Item_Code,TSPL_PI_DETAIL" & tblCanDel & ".Unit_code
from TSPL_PI_DETAIL" & tblCanDel & "  
LEFT OUTER JOIN TSPL_PI_HEAD" & tblCanDel & " On TSPL_PI_HEAD" & tblCanDel & ".PI_No = TSPL_PI_DETAIL" & tblCanDel & ".PI_No)TSPL_PI_HEAD On TSPL_PI_HEAD.SRN_Id=TSPL_SRN_HEAD.SRN_No --And TSPL_PI_HEAD.Item_Code=TSPL_SRN_HEAD.Item_Code And TSPL_PI_HEAD.Unit_code=TSPL_SRN_HEAD.Unit_code

LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_GRN_HEAD.Item_Code
LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_GRN_HEAD.Bill_To_Location
WHERE 1=1 " & Whr
            '          qry &= " and CONVERT(date, TSPL_GRN_HEAD.GRN_Date, 103) >= CONVERT(date, '" & txtFromDate.Value & "', 103) 
            'and CONVERT(date, TSPL_GRN_HEAD.GRN_Date, 103) <= CONVERT(date, '" & txtToDate.Value & "', 103) "
            qry &= " And (IsNull(TSPL_GRN_HEAD." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_PO_WEIGHTMENT_HEAD." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_MRN_HEAD." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_NIR_QC." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_QC_CHECK_DETAIL." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_SRN_HEAD." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_TENDER_PENALTY." & colCanDel & "_By,'')<>'' Or IsNull(TSPL_PI_HEAD." & colCanDel & "_By,'')<>'')"
            qry &= " Order By TSPL_GRN_HEAD.GRN_No "

            Dim dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            gv1.ReadOnly = True
            gv1.ShowGroupPanel = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.EnableFiltering = True
                EnableDisableCtrl(False)
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
End Class