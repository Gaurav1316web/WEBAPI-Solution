'-06/11/2012-12:31PM--Updation By--[Pankaj Kumar]--Added Two New COlumns(Created_By, Modify_By) as Well as (Prepared By, Checked By)----Fwd By--Ranjan Mam
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmCashVoucher
    Inherits FrmMainTranScreen
    Dim qry As String

    Private Sub FrmCashVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        chkBankAll.IsChecked = True
        chkLocationAll.IsChecked = True
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        LoadBankCode()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCashVoucher)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPrint.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Segment_code  as Location,Description  from TSPL_GL_SEGMENT_CODE  where Seg_No =7 "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Public Sub LoadBankCode()
        Dim qry As String = "select BANK_CODE as Code  ,DESCRIPTION   from TSPL_BANK_MASTER "
        cbgBankCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBankCode.ValueMember = "Code"
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData()
    End Sub
    Public Sub printData()
        Try
            Dim fbankcode As String = ""
            Dim flocation As String = ""
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")

            Dim BankCodeArr As ArrayList = cbgBankCode.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim address As String
            If chkBankSelect.IsChecked = True AndAlso cbgBankCode.CheckedValue.Count > 0 Then
                fbankcode = clsCommon.GetMulcallString(BankCodeArr)
                fbankcode = fbankcode.Replace("'", "")
            End If
            If chkBankSelect.IsChecked = True AndAlso cbgBankCode.CheckedValue.Count > 0 Then
                flocation = clsCommon.GetMulcallString(locationArr)
                flocation = flocation.Replace("'", "")
            End If
            If cbgLocation.CheckedValue.Count = 1 Then
                address = " (select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER.State_Name )> 0 then ',' else '' end  +TSPL_TDS_STATE_MASTER.State_Name ) from tspl_location_master left outer join TSPL_TDS_STATE_MASTER on TSPL_LOCATION_MASTER.State =TSPL_TDS_STATE_MASTER .State_Code  where loc_segment_code=TSPL_GL_ACCOUNTS .Account_Seg_Code7) "

            Else
                address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "

            End If
            qry = "select '" + fbankcode + "' as fbankfilter,'" + flocation + "' as LocFilter, '" + fbankcode + "' as Fbankcode,'" + flocation + "' as flocation,'" + fromdate + "'as Fromdate,'" + Todate + "' as Todate, Transfer_No as TransferNum ,From_Bank_Acc_No as FromBankCode,From_Bank_Name as FrombankName,To_Bank_Acc_No  as ToAccCode ,To_Bank_Name as ToBankName,tspl_bank_transfer.Description as Description,tspl_bank_transfer.Cheque_No," & _
                  "  Deposit_Amount as Amount,TSPL_GL_ACCOUNTS .Account_Seg_Code7 as Location,GlAcc .Account_Seg_Code7 as FromLocation,Transfer_Posting_Date as VoucherDate ," & _
                  "  TSPL_BANK_TRANSFER .Comp_Code as CompCode ,TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1" & _
                  "   , TSPL_COMPANY_MASTER.Logo_Img2 as Image2," + address + "  as address " & _
                  "   , (case TSPL_BANK_MASTER .Bank_type when 'B'then 'Bank Transfer'when 'C'then 'Cash Transfer' when 'P'then 'Petty Cash' when 'O' then 'Other' else'' end ) as  BankType, TSPL_BANK_TRANSFER.Created_By, TSPL_BANK_TRANSFER.Modify_By    from TSPL_BANK_TRANSFER  left outer join TSPL_GL_ACCOUNTS on TSPL_BANK_TRANSFER .To_Bank_Acc_No  =TSPL_GL_ACCOUNTS .Account_Code left outer join TSPL_GL_ACCOUNTS as GlAcc  on TSPL_BANK_TRANSFER .From_Bank_Acc_No =GlAcc .Account_Code   " & _
                  "   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = TSPL_BANK_TRANSFER .Comp_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_TRANSFER .From_Bank_Code =TSPL_BANK_MASTER .BANK_CODE  where Convert(Date,Transfer_Date,103) >= CONVERT(Date,'" + dtpFromdate1.Value + "',103) and Convert(date,Transfer_Date,103) <= CONVERT (Date,'" + dtpToDate.Value + "' ,103) "
            If chkBankSelect.IsChecked = True AndAlso cbgBankCode.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Bank Code")
                Return
            ElseIf chkBankSelect.IsChecked = True AndAlso cbgBankCode.CheckedValue.Count > 0 Then
                qry += " and    From_Bank_Code in (" + clsCommon.GetMulcallString(BankCodeArr) + ")"
            End If


            If chkLocationSelect.IsChecked = True Then
                'AndAlso cbgLocation.CheckedValue.Count = 0 Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                    Return
                    ' ElseIf cbgLocation.CheckedValue.Count > 0 Then
                Else
                    qry += " and TSPL_GL_ACCOUNTS .Account_Seg_Code7 in (" + clsCommon.GetMulcallString(locationArr) + ")"
                End If
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else

                Dim FRMcrys As New frmCrystalReportViewer
                FRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "CashVoucher", "Cash Voucher")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkBankAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBankAll.ToggleStateChanged
        cbgBankCode.Enabled = Not chkBankAll.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkBankAll.IsChecked = True
        chkLocationAll.IsChecked = True

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CSH-VCHR"
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
    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function

    Private Sub FrmCashVoucher_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            printData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    
End Class
