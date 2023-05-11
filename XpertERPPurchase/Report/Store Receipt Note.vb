Imports common
'' --- Query Updated By abhishek as on 11 jan 2013 3:00 pm
'----by vipin for date format on 08/11/2012
Public Class Store_Receipt_Note
    Inherits FrmMainTranScreen
    Dim qry As String

    Private Sub chkMDoc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMDoc.ToggleStateChanged, chkMItem.ToggleStateChanged, chkMVendor.ToggleStateChanged, chkMPI.ToggleStateChanged
        Dim qry As String = ""
        If chkMDoc.IsChecked Then
            qry = "select SRN_No as CODE,CAST(convert(varchar(100),SRN_Date,103)as nvarchar)  as [DESC] from TSPL_SRN_HEAD "
            funDocument()
        ElseIf chkMVendor.IsChecked Then
            qry = "select Vendor_Code as CODE ,Vendor_Name as [DESC]   from TSPL_VENDOR_MASTER  "
            funVendor()
        ElseIf chkMItem.IsChecked Then
            funItem()
        ElseIf chkMPI.IsChecked Then
            qry = "select PI_No as CODE,CAST(convert(varchar(100),PI_Date,103)as nvarchar)  as [DESC] from TSPL_PI_HEAD  "
            funPurchaseInvoice()
        End If
        'cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgDoc.ValueMember = "CODE"
        'cbgDoc.DisplayMember = "DESC"
    End Sub

    'Private Sub chkMItem_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMItem.ToggleStateChanged
    '    funItem()
    'End Sub
    Sub funLocation()
        qry = "select Location_Code as CODE,Location_Desc as Description from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Sub funDocument()
        'cbgDoc.DataSource.Clear()
        qry = "select SRN_No as CODE,convert(date,SRN_Date,103)  as [DESC] from TSPL_SRN_HEAD where Convert(Date,SRN_Date,103) >=convert(date,'" + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + "',103) and CONVERT (Date,SRN_Date ,103)<=convert(date,'" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + "',103) "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "CODE"
        cbgDoc.DisplayMember = "DESC"
    End Sub
    Sub funItem()
        cbgDoc.DataSource.Clear()
        If chkFnshdGoods.IsChecked = True Then
            qry = "select Item_Code as CODE ,Item_Desc as [DESC]  from TSPL_ITEM_MASTER Where Item_Type='F'  "
        ElseIf chkOthers.IsChecked = True Then
            qry = "select Item_Code as CODE ,Item_Desc as [DESC]  from TSPL_ITEM_MASTER Where Item_Type<>'F'  "
        Else
            qry = "select Item_Code as CODE ,Item_Desc as [DESC]  from TSPL_ITEM_MASTER "
        End If

        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "CODE"
        cbgDoc.DisplayMember = "DESC"
    End Sub

    'Private Sub chkMVendor_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMVendor.ToggleStateChanged
    '    funVendor()
    'End Sub
    Sub funVendor()
        cbgDoc.DataSource.Clear()
        qry = "select Vendor_Code as CODE ,Vendor_Name as [DESC]  from TSPL_VENDOR_MASTER  "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "CODE"
        cbgDoc.DisplayMember = "DESC"
    End Sub

    'Private Sub chkMPI_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMPI.ToggleStateChanged
    '    funPurchaseInvoice()
    'End Sub
    Sub funPurchaseInvoice()
        qry = "select PI_No as CODE,convert(varchar(10),PI_Date,103)  as [DESC] from TSPL_PI_HEAD  "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "CODE"
        cbgDoc.DisplayMember = "DESC"
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Store_Receipt_Note)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub
    Private Sub Store_Receipt_Note_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()

        chkFnshdGoods.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkdocall.IsChecked = True
        'chkMItem.IsChecked = True
        'chkMVendor.IsChecked = True
        chkMPI.IsChecked = True
        funLocation()
        chkLocalAll.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If objCommonVar.IsDemoERP Then
            grpItemType.Visible = False
            chkOthers.IsChecked = True
        End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DLY-REC-N"
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

    Private Sub chkdocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocall.ToggleStateChanged, chk_doc_select.ToggleStateChanged
        cbgDoc.Enabled = Not chkdocall.IsChecked = True
    End Sub


    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        funPrint()
    End Sub
    Sub funPrint()
        Try
            Dim location As String = ""
            Dim DocNo As String = ""
            Dim status As String = ""
            Dim Strlocation As String = ""
            Dim StrDocNo As String = ""
            Dim list As String = ""
            Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
            Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
            If chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count > 0 Then
                DocNo = "'" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + "'"
                StrDocNo = DocNo.Replace("'", "")
            End If
            

            If chkLocalSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                Strlocation = location.Replace("'", "")
            End If
            If chkFnshdGoods.IsChecked Then
                status = "Finishied Goods"
            ElseIf chkOthers.IsChecked Then
                status = "Others"
            End If

            If chkMDoc.IsChecked Then
                list = "DocumentNo"
            End If
            If chkMItem.IsChecked Then
                list = "Item"
            End If
          
            If chkMVendor.IsChecked Then
                list = "Vendor"
            End If
            
            If chkMPI.IsChecked Then
                list = "PurchaseInvoice"
            End If

            If chkLocalAll.IsChecked Or cbgLocation.CheckedValue.Count > 1 Then
                qry = "select '" + fromdate + "' as  FromDate,'" + todate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + Strlocation + "' as Strlocation,'" + status + "' as status, '" + list + "' as list , * from (Select *,(select top 1  TSPL_PI_HEAD.PI_No  from   TSPL_PI_DETAIL left outer join  tspl_PI_head on TSPL_PI_HEAD.PI_No =TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL .SRN_Id =xxx.SrnNo  ) as PINumber" & _
                      " ,(select top 1   convert(varchar(10),TSPL_PI_HEAD.PI_Date,103)from   TSPL_PI_DETAIL left outer join  tspl_PI_head on TSPL_PI_HEAD.PI_No =TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL .SRN_Id =xxx.SrnNo) as PIDate " & _
                      " from  ( " & _
                      " select TSPL_COMPANY_MASTER.Comp_Name as CompanyName ," & _
                      " Case when len(tspl_company_master.Add1)>0 then tspl_company_master.Add1 else '' end +case when len(tspl_company_master.Add2)>0 then ','  else  '' end  +case when len(tspl_company_master.Add2)>0 then tspl_company_master.Add2  else  '' end + case when len(tspl_company_master.Add3)>0 then ','  else  '' end +case when len(tspl_company_master.Add3)>0 then tspl_company_master.Add3  else  '' end as CompanyAddress," & _
                      " TSPL_SRN_HEAD.SRN_No as SrnNo ,convert(varchar(10),TSPL_SRN_HEAD.SRN_Date,103) as SrnDate ,TSPL_SRN_DETAIL.Item_Code as ItemCode," & _
                      " TSPL_SRN_DETAIL.item_desc  as ItemDescription ,TSPL_SRN_HEAD.Vendor_Code  as VendorCode,TSPL_SRN_HEAD.Vendor_Name as Vendorname," & _
                      " TSPL_SRN_DETAIL.Unit_code as Uom, TSPL_SRN_DETAIL.MRP  as MRP, TSPL_SRN_DETAIL.SRN_Qty+ TSPL_SRN_DETAIL.Rejected_Qty as ReceivedQty," & _
                      "  TSPL_SRN_DETAIL.Rejected_Qty as RejectedQty ,TSPL_SRN_DETAIL.SRN_Qty  as NetQty,TSPL_SRN_HEAD.GENo as GeNumber," & _
                      "  convert(varchar(10),TSPL_SRN_HEAD.GEDate,103) as  GeDate ," & _
                      "  TSPL_SRN_DETAIL  .landed_Cost_Amount as landedAmount,TSPL_SRN_DETAIL .Landed_Cost_Rate as LandedRate," & _
                      "  (TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty ) as BrkLkgQty, TSPL_SRN_DETAIL.Short_Qty as ShortQty " & _
                      "  from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL .SRN_No left outer join TSPL_COMPANY_MASTER on TSPL_SRN_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code Left Outer Join TSPL_ITEM_MASTER on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  where 1=1 "
            ElseIf chkLocalSelect.IsChecked And cbgLocation.CheckedValue.Count = 1 Then
                qry = "select  '" + fromdate + "' as  FromDate,'" + todate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + Strlocation + "' as Strlocation,'" + status + "' as status, '" + list + "' as list , * from (Select *,(select top 1  TSPL_PI_HEAD.PI_No  from   TSPL_PI_DETAIL left outer join  tspl_PI_head on TSPL_PI_HEAD.PI_No =TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL .SRN_Id =xxx.SrnNo  ) as PINumber" & _
                      " ,(select top 1   convert(varchar(10),TSPL_PI_HEAD.PI_Date,103)from   TSPL_PI_DETAIL left outer join  tspl_PI_head on TSPL_PI_HEAD.PI_No =TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL .SRN_Id =xxx.SrnNo) as PIDate " & _
                      " from  ( " & _
                      " select TSPL_COMPANY_MASTER.Comp_Name as CompanyName ," & _
                       "Case when len(TSPL_LOCATION_MASTER.Add1)>0 then TSPL_LOCATION_MASTER.Add1 else '' end +case when len(TSPL_LOCATION_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_LOCATION_MASTER.Add2)>0 then TSPL_LOCATION_MASTER.Add2  else  '' end + case when len(TSPL_LOCATION_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_LOCATION_MASTER.Add3)>0 then TSPL_LOCATION_MASTER.Add3  else  '' end as CompanyAddress," & _
                       " TSPL_SRN_HEAD.SRN_No as SrnNo ,convert(varchar(10),TSPL_SRN_HEAD.SRN_Date,103) as SrnDate ,TSPL_SRN_DETAIL.Item_Code as ItemCode," & _
                      " TSPL_SRN_DETAIL.item_desc  as ItemDescription ,TSPL_SRN_HEAD.Vendor_Code  as VendorCode,TSPL_SRN_HEAD.Vendor_Name as Vendorname," & _
                      " TSPL_SRN_DETAIL.Unit_code as Uom, TSPL_SRN_DETAIL.MRP  as MRP, TSPL_SRN_DETAIL.SRN_Qty+ TSPL_SRN_DETAIL.Rejected_Qty as ReceivedQty," & _
                      "  TSPL_SRN_DETAIL.Rejected_Qty as RejectedQty ,TSPL_SRN_DETAIL.SRN_Qty  as NetQty,TSPL_SRN_HEAD.GENo as GeNumber," & _
                      "  convert(varchar(10),TSPL_SRN_HEAD.GEDate,103) as  GeDate ," & _
                      "  TSPL_SRN_DETAIL  .landed_Cost_Amount as landedAmount,TSPL_SRN_DETAIL .Landed_Cost_Rate as LandedRate," & _
                      "  (TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty ) as BrkLkgQty, TSPL_SRN_DETAIL.Short_Qty as ShortQty " & _
                      "  from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL .SRN_No left outer join TSPL_COMPANY_MASTER on TSPL_SRN_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code" & _
                      "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SRN_HEAD.Bill_To_Location Left Outer Join TSPL_ITEM_MASTER on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code     where 1=1"
            End If

            If chkMDoc.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Srn No.")
                Exit Sub
            End If
            If chkMDoc.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_HEAD.SRN_No  in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ") "
            End If

            If chkMItem.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                Exit Sub
            End If
            If chkMItem.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ") "
            End If
            If chkMVendor.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                Exit Sub
            End If
            If chkMVendor.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ") "
            End If

            If chkLocalSelect.IsChecked And cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Exit Sub
            End If
            If chkLocalSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                qry += " AND TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"


            End If

            'Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
            ' Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
           
            qry += " and  TSPL_SRN_HEAD.SRN_Date  >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpfromdate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and   TSPL_SRN_HEAD.SRN_Date <=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtptodate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SRN_DETAIL.Row_Type<>'Misc' "


            'Puran Singh Negi - Ticket No- BM00000003423

            'If chkFnshdGoods.IsChecked = True Then
            '    qry += "AND TSPL_ITEM_MASTER.Item_Type='F'  "
            'ElseIf chkOthers.IsChecked = True Then
            '    qry += "AND TSPL_ITEM_MASTER.Item_Type<>'F' "
            'End If
            qry += ")as xxx   )as finalQRy"

            qry += " where finalQRy .PINumber <>''  "
            If chkMPI.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Invoice")
                Exit Sub
            End If
            If chkMPI.IsChecked And chk_doc_select.IsChecked And cbgDoc.CheckedValue.Count > 0 Then
                qry += " and finalQRy .PINumber in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ") "

            End If

            Dim frmCRV As New frmCrystalReportViewer()
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If chkFnshdGoods.IsChecked = True Then
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SrnReceipt", "StroreReceipNote")
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "SrnReceipt1", "StroreReceipNote1")
            End If
            frmCRV = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()

        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkMPI.IsChecked = True
        chkdocall.IsChecked = True
        chkLocalAll.IsChecked = True
    End Sub

    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub

    Private Sub chkLocalAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocalAll.ToggleStateChanged, chkLocalSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocalAll.IsChecked = True
    End Sub
    Private Sub dtpfromdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfromdate.ValueChanged
        funDocument()
    End Sub

    Private Sub dtptodate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtptodate.ValueChanged
        funDocument()
    End Sub

    Private Sub chkFnshdGoods_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkFnshdGoods.ToggleStateChanged
        If chkMItem.IsChecked Then
            funItem()
        End If
    End Sub

    Private Sub chkOthers_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkOthers.ToggleStateChanged
        If chkMItem.IsChecked = True Then
            funItem()
        End If
    End Sub

    Private Sub Store_Receipt_Note_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub
End Class
