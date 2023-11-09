Imports common
Public Class frmRptTransfer
    Inherits FrmMainTranScreen
    Dim strqry As String
    Dim isEvenNumber As Integer = 0
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("TRANSFER-RPT")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkTransferAll.IsChecked = True
        LoadTransferExcisable()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Sub LoadTransferExcisable()
        strqry = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD where Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))>0"
        cbg.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbg.ValueMember = "Code"
        cbg.DisplayMember = "Date"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim arrList As ArrayList = Nothing
        If chkTransferSelect.IsChecked Then
            arrList = cbg.CheckedValue
            If arrList.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one transaction to Print")
                Exit Sub
            End If
        End If
        Try
            Dim strType As String = ""
            If rbtnLoadin.IsChecked Then
                strType = "LI"
            ElseIf rbtnExcisable.IsChecked Then
                strType = "LOT"
            ElseIf rbtnNonExcisable.IsChecked Then
                strType = "LON"
            End If
            funTransfer(clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd"), clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd"), strType, arrList)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Public Shared Function funTransfer(ByVal FromDate As String, ByVal ToDate As String, ByVal strType As String, ByVal arrList As ArrayList) As Boolean
        Dim StrQuery As String = "select TSPL_TRANSFER_HEAD.Tax_Group,TSPL_TRANSFER_HEAD.Transfer_No AS DocNo,CONVERT(varchar(11),TSPL_TRANSFER_HEAD.Transfer_Date,103) as DocDate,FromLocation.Location_Desc as FromLocName,ToLocation.Location_Desc as ToLocName,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_ROUTE_MASTER.Route_Desc as RouteName,(case when  TSPL_TRANSFER_HEAD.Transfer_Type='LI' then 'Loadin Transfer Detail' else case when  TSPL_TRANSFER_HEAD.Transfer_Type='LO' and len(rtrim(TSPL_TRANSFER_HEAD.Tax_Group))>0 then 'Excise Challan' else 'Loadout Transfer Detail' end end) as TransType,tax2.Tax_Code_Desc as Tax2Name,isnull(TSPL_TRANSFER_HEAD.TAX2_Amt,0) as TAX2Amt,tax3.Tax_Code_Desc as Tax3Name,isnull(TSPL_TRANSFER_HEAD.TAX3_Amt,0) as TAX3Amt,tax4.Tax_Code_Desc as Tax4Name,isnull(TSPL_TRANSFER_HEAD.TAX4_Amt,0) as TAX4Amt,tax5.Tax_Code_Desc as Tax5Name,isnull(TSPL_TRANSFER_HEAD.TAX5_Amt,0) as TAX5Amt,tax6.Tax_Code_Desc as Tax6Name,isnull(TSPL_TRANSFER_HEAD.TAX6_Amt,0) as TAX6Amt,tax7.Tax_Code_Desc as Tax7Name,isnull(TSPL_TRANSFER_HEAD.TAX7_Amt,0) as TAX7Amt,tax8.Tax_Code_Desc as Tax8Name,isnull(TSPL_TRANSFER_HEAD.TAX8_Amt,0) as TAX8Amt,tax9.Tax_Code_Desc as Tax9Name,isnull(TSPL_TRANSFER_HEAD.TAX9_Amt,0) as TAX9Amt,tax10.Tax_Code_Desc as Tax10Name,isnull(TSPL_TRANSFER_HEAD.TAX10_Amt,0) as TAX10Amt,TSPL_TRANSFER_HEAD.Item_Amount as TotAmt, TSPL_TRANSFER_HEAD.Total_Tax_Amount as TotTaxAmt,TSPL_TRANSFER_HEAD.Total_Item_Amount as RAmt,TSPL_TRANSFER_DETAIL.Item_Code as ICode,TSPL_TRANSFER_DETAIL.Item_Desc as IName,(case when TSPL_TRANSFER_HEAD.Transfer_Type='LI' then TSPL_TRANSFER_DETAIL.LoadIn_Qty else TSPL_TRANSFER_DETAIL.Item_Qty end) as Qty,(ISNULL(TSPL_TRANSFER_DETAIL.TPT_Value,0)+ISNULL(TSPL_TRANSFER_DETAIL.Empty_Value,0) +ISNULL(TSPL_TRANSFER_DETAIL.BasicPrice_WithTax,0)) as Rate,((case when TSPL_TRANSFER_HEAD.Transfer_Type='LI' then TSPL_TRANSFER_DETAIL.LoadIn_Qty else TSPL_TRANSFER_DETAIL.Item_Qty end) * (ISNULL(TSPL_TRANSFER_DETAIL.TPT_Value,0)+ISNULL(TSPL_TRANSFER_DETAIL.Empty_Value,0) +ISNULL(TSPL_TRANSFER_DETAIL.BasicPrice_WithTax,0))) as Amt,isnull(TSPL_TRANSFER_DETAIL.TAX1_Amt,0) as TaxAmt,ISNULL(TSPL_TRANSFER_DETAIL.TPT_Value,0)as TPT_value,ISNULL(TSPL_TRANSFER_DETAIL.Empty_Value,0) as EmptyVal,ISNULL(TSPL_TRANSFER_DETAIL.BasicPrice_WithTax,0) as BasicWTax,(((case when TSPL_TRANSFER_HEAD.Transfer_Type='LI' then isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0) else isnull(TSPL_TRANSFER_DETAIL.Item_Qty,0) end) * isnull(TSPL_TRANSFER_DETAIL.Item_Price,0))+isnull(TSPL_TRANSFER_DETAIL.TAX1_Amt,0)) as AmtAfterTax,FromLocation.TIN_No,TSPL_TRANSFER_DETAIL.Tax1_Assessable_Amt ,(ISNULL(FromLocation.ADD1,'') + case when len(RTRIM(ISNULL(FromLocation.Add2,'')))>0 then +', '+FromLocation.Add2 else '' end+ case when LEN(RTRIM(IsNull(FromLocation.ADD3,'')))>0 then + ', '+FromLocation.ADD3 else '' end + case when LEN(RTRIM(ISNULL(FromLocation.ADD4,'')))>0 then + ', '+FromLocation.ADD4 else '' end + case when len(RTRIM(ISNULL(FromLocation.City_Code,'')))>0 then  + ', '+FromLocation.City_Code else '' end + case when len(RTRIM(ISNULL(FromLocation.State,'')))>0 then  + ', '+FromLocation.State else '' end + case when len(RTRIM(ISNULL(FromLocation.Country,'')))>0 then  + ', '+FromLocation.Country else '' end + case when ISNULL(FromLocation.Pin_Code,0)>0 then  + '- '+CAST(FromLocation.Pin_Code as Varchar) else '' end ) as FromLocationAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress,TSPL_TRANSFER_HEAD.Trip_No,TSPL_TRANSFER_DETAIL.Pending_Qty,TSPL_TRANSFER_DETAIL.Breakage,TSPL_TRANSFER_HEAD.Transfer_Type from TSPL_TRANSFER_DETAIL left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No left outer join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code=TSPL_TRANSFER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as ToLocation on ToLocation.Location_Code=TSPL_TRANSFER_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_TRANSFER_HEAD.Salesmancode left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_TRANSFER_HEAD.Route_No left outer join TSPL_TAX_MASTER as Tax2 on Tax2.Tax_Code=TSPL_TRANSFER_HEAD.TAX2 left outer join TSPL_TAX_MASTER as Tax3 on Tax3.Tax_Code=TSPL_TRANSFER_HEAD.TAX3 left outer join TSPL_TAX_MASTER as Tax4 on Tax4.Tax_Code= TSPL_TRANSFER_HEAD.TAX4 left outer join TSPL_TAX_MASTER as Tax5 on Tax5.Tax_Code=TSPL_TRANSFER_HEAD.TAX5 left outer join TSPL_TAX_MASTER as Tax6 on Tax6.Tax_Code=TSPL_TRANSFER_HEAD.TAX6 left outer join TSPL_TAX_MASTER as Tax7 on Tax7.Tax_Code=TSPL_TRANSFER_HEAD.TAX7 left outer join TSPL_TAX_MASTER as Tax8 on Tax8.Tax_Code=TSPL_TRANSFER_HEAD.TAX8 left outer join TSPL_TAX_MASTER as Tax9 on Tax9.Tax_Code=TSPL_TRANSFER_HEAD.TAX9 left outer join TSPL_TAX_MASTER as Tax10 on Tax10.Tax_Code=TSPL_TRANSFER_HEAD.TAX10 left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code where 2=2 "

        If arrList IsNot Nothing AndAlso arrList.Count > 0 Then
            StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_No in (" + clsCommon.GetMulcallString(arrList) + ") "
        Else
            StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_Date >= '" + FromDate + "' and TSPL_TRANSFER_HEAD.Transfer_Date <= '" + ToDate + "' "
        End If

        Dim StrOrderCls As String = " order by TSPL_TRANSFER_HEAD.Transfer_No,TSPL_TRANSFER_DETAIL.Line_No "

        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.CompairString(strType, "LI") = CompairStringResult.Equal Then
            StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_Type='LI'"
            frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(StrQuery + StrOrderCls), "rptTransferLoadin", "Loadin Transfer Report")
        ElseIf clsCommon.CompairString(strType, "LOT") = CompairStringResult.Equal Then
            StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_Type='LO' and len(rtrim(isnull(TSPL_TRANSFER_HEAD.Tax_Group,'')))>0"
            frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(StrQuery + StrOrderCls), "rptCustomTransfer", "Transfer Report")
        ElseIf clsCommon.CompairString(strType, "LON") = CompairStringResult.Equal Then
            StrQuery += " and TSPL_TRANSFER_HEAD.Transfer_Type='LO' and len(rtrim(isnull(TSPL_TRANSFER_HEAD.Tax_Group,'')))<=0"
            frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(StrQuery + StrOrderCls), "rptTransferWithouTax", "Loadout Transfer Report")
        End If
        frmCRV = Nothing
        Return True
    End Function

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TRANSFER-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub rbtnExcisable_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnExcisable.ToggleStateChanged, rbtnNonExcisable.ToggleStateChanged, rbtnLoadin.ToggleStateChanged
        Try
            isEvenNumber = isEvenNumber + 1
            If isEvenNumber Mod 2 = 0 Then
                Dim qry As String = ""
                If rbtnExcisable.IsChecked Then
                    qry = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD where Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))>0"
                ElseIf rbtnNonExcisable.IsChecked = True Then
                    qry = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD  where Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))<=0"
                ElseIf rbtnLoadin.IsChecked Then
                    qry = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD  where Transfer_Type='LI'"
                End If
                LoadData(qry)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub LoadData(ByVal qry As String)
        If clsCommon.myLen(qry) > 0 Then
            cbg.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbg.ValueMember = "Code"
        End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim qry As String = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD where Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))>0"
        LoadData(qry)
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim qry As String = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD  where Transfer_Type='LO' and LEN(RTRIM(isnull(Tax_Group,'')))<=0"
        LoadData(qry)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim qry As String = "select Transfer_No as Code,CONVERT(varchar(11),Transfer_Date,103) as IDate from TSPL_TRANSFER_HEAD  where Transfer_Type='LI'"
        LoadData(qry)
    End Sub

    Private Sub chkTransferAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransferAll.ToggleStateChanged, chkTransferSelect.ToggleStateChanged
        cbg.Enabled = chkTransferSelect.IsChecked
    End Sub
End Class
