
Imports common
Public Class FrmWTDRpt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DetailofWtdPriceofRawMaterial)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub


    Private Sub FrmWTDRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")

    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Public Sub loadItem()
        Dim qry As String = " select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER where Item_Type='R'"
        cgvItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvItem.ValueMember = "Code"
        cgvItem.DisplayMember = "Description"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()

        If objCommonVar.CurrentUserCode <> "ADMIN" Then
            If funSetUserAccess() = False Then Exit Sub
        End If
    End Sub

    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "WTD-RPT"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access

            End If
            If strTemp(2) = "0" Then 'Grant modify access

            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
        End Try
    End Function

    Public Sub reset()
        Try

            dtpfmonth.Value = clsCommon.GETSERVERDATE()
            dtptmonth.Value = clsCommon.GETSERVERDATE()
            dtpfyear.Value = clsCommon.GETSERVERDATE()
            dtptyear.Value = clsCommon.GETSERVERDATE()
            chkLocAll.IsChecked = True
            chkIAll.IsChecked = True
            chkLocAll.IsChecked = True
            loadItem()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIAll.ToggleStateChanged
        cgvItem.Enabled = Not chkIAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Try
            Dim Item As String
            Dim location As String
            Dim StrItem As String = ""
            Dim Strlocation As String = ""

            If chkIselect.IsChecked AndAlso cgvItem.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item", Me.Text)
                Return
            End If

            Dim FMon As String = clsCommon.GetPrintDate(dtpfmonth.Value, "MM")
            Dim FMonName As String = clsCommon.GetPrintDate(dtpfmonth.Value, "MMM")

            Dim Tmon As String = clsCommon.GetPrintDate(dtptmonth.Value, "MM")
            Dim TmonNAme As String = clsCommon.GetPrintDate(dtptmonth.Value, "MMM")

            Dim FYear As String = clsCommon.GetPrintDate(dtpfyear.Value, "yyyy")
            Dim TYear As String = clsCommon.GetPrintDate(dtptyear.Value, "yyyy")
            Dim first As String = FYear + FMon
            Dim second As String = TYear + Tmon
            Dim locationArr As ArrayList = cbgLocation.CheckedValue

          
            If (dtpfyear.Value > dtptyear.Value) Then
                common.clsCommon.MyMessageBoxShow("Fromyear should be less than Toyear", Me.Text)
                Return
            End If

            If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
                Strlocation = location.Replace("'", "")

            End If

            If chkIselect.IsChecked And cgvItem.CheckedValue.Count > 0 Then
                Item = "'" + clsCommon.GetMulcallString(cgvItem.CheckedValue) + "'"
                StrItem = Item.Replace("'", "")
            End If

            Dim qry As String

            ' ''  qry = " select '" + FMonName + "' as FMonName,'" + TmonNAme + "' as TmonNAme ,'" + FYear + "' as FYear,'" + TYear + "' as TYear,Mdate,Ydate,max(SRN_No) as SRN_No,Item_Code,Item_Desc,sum(SRN_Qty) as SRN_Qty,sum(Amount) as Amount,sum(ExciseDuty) as ExciseDuty,sum(EDCess) as EDCess,sum(HCess) as HCess,sum(TX4) as TX4,sum(VAT) as VAT,sum(CST) as CST,sum(AddCh) as AddCh,sum(RExciseDuty) as RExciseDuty,sum(REDCess) as REDCess ,sum(RHCess) as RHCess,sum(RTX4) as RTX4,sum(RVAT) as RVAT,sum(RCST) as RCST ,MAX(Comp_Name) as Comp_Name,MAX(add1) as add1  from ( select (month(TSPL_SRN_HEAD.SRN_Date)) as Mdate,year(TSPL_SRN_HEAD.SRN_Date) as Ydate,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.SRN_Qty,Amount , ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and (TSPL_SRN_DETAIL.TAX1 like ('%BED%')OR TSPL_SRN_DETAIL.TAX1 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX1_Amt else 0  end) as ExciseDuty, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E' and (TSPL_SRN_DETAIL.TAX2 like ('%BED%')OR TSPL_SRN_DETAIL.TAX2 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX2_Amt else 0  end) as EDCess, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX3 like ('%BED%')OR TSPL_SRN_DETAIL.TAX3 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as HCess, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX4 like ('%BED%')OR TSPL_SRN_DETAIL.TAX4 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX4 like ('HCESS')  ) then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as TX4, ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='V'  then TSPL_SRN_DETAIL.TAX1_Amt   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='V'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='V'   then TSPL_SRN_DETAIL.TAX3_Amt " & _
            ' ''     " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='V'  then TSPL_SRN_DETAIL.TAX4_Amt   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='V'   then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end) as VAT," & _
            ' ''   " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='C'  then TSPL_SRN_DETAIL.TAX1_Amt  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='C'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='C'   then TSPL_SRN_DETAIL.TAX3_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='C'   then TSPL_SRN_DETAIL.TAX4_Amt  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='C'  then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end)as CST, " & _
            ' ''    " isnull((TSPL_SRN_HEAD.Add_Charge_Amt1+TSPL_SRN_HEAD.Add_Charge_Amt2+TSPL_SRN_HEAD.Add_Charge_Amt3+TSPL_SRN_HEAD.Add_Charge_Amt4+TSPL_SRN_HEAD.Add_Charge_Amt5+TSPL_SRN_HEAD.Add_Charge_Amt6 +  isnull((select SUM(Amount) from TSPL_SRN_DETAIL where Row_Type='misc' and SRN_No=TSPL_SRN_HEAD.SRN_No) ,0 )),0)   as AddCh , " & _
            ' ''   "( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and (TSPL_SRN_DETAIL.TAX1 like ('%BED%')OR TSPL_SRN_DETAIL.TAX1 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX1_Amt else 0  end) as RExciseDuty, ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E' and (TSPL_SRN_DETAIL.TAX2 like ('%BED%')OR TSPL_SRN_DETAIL.TAX2 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX2_Amt else 0  end) as REDCess,  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  " & _
            ' ''  " and (TSPL_SRN_DETAIL.TAX3 like ('%BED%')OR TSPL_SRN_DETAIL.TAX3 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as RHCess,( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX4 like ('%BED%')OR TSPL_SRN_DETAIL.TAX4 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX4 like ('HCESS')  ) then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as RTX4, ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='V'   then TSPL_SRN_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='V'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='V'   then TSPL_SRN_DETAIL.TAX3_Amt  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='V'   then TSPL_SRN_DETAIL.TAX4_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='V'   then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end) as RVAT, " & _
            ' ''  " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='y' and " & _
            ' '' " (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='C'  then TSPL_SRN_DETAIL.TAX1_Amt   " & _
            ' '' " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='C'  then TSPL_SRN_DETAIL.TAX2_Amt " & _
            ' '' " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='C'  then TSPL_SRN_DETAIL.TAX3_Amt " & _
            ' '' "  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='C' then TSPL_SRN_DETAIL.TAX4_Amt " & _
            ' ''"  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='C' then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end)" & _
            ' ''" as RCST ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 from TSPL_SRN_HEAD " & _
            ' ''" left outer join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SRN_HEAD.Comp_Code where  TSPL_SRN_DETAIL.Row_Type<>'misc' and TSPL_SRN_DETAIL.SRN_Qty<>0 and TSPL_SRN_DETAIL.Amount<>0  and  (month(TSPL_SRN_HEAD.SRN_Date))>='" + FMon + "' and  (month(TSPL_SRN_HEAD.SRN_Date))<='" + Tmon + "' and  (year(TSPL_SRN_HEAD.SRN_Date))>='" + FYear + "' and  (year(TSPL_SRN_HEAD.SRN_Date))<='" + TYear + "' "


            ' ''  If chkIselect.IsChecked Then
            ' ''      qry += " and TSPL_SRN_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(cgvItem.CheckedValue) + ")  "
            ' ''  End If



            ' ''  qry += " )final group by Mdate,final.Item_Code,final.Item_Desc,Ydate "



            '-------------------------------------------------------------------------------------------------------


            ''            qry = " select '" + FMonName + "' as FMonName,'" + TmonNAme + "' as TmonNAme ,'" + FYear + "' as FYear,'" + TYear + "' as TYear,Mdate,Ydate,max(SRN_No) as SRN_No,Item_Code,Item_Desc,sum(SRN_Qty) as SRN_Qty,sum(Amount) as Amount,sum(ExciseDuty) as ExciseDuty,sum(EDCess) as EDCess,sum(HCess) as HCess,sum(TX4) as TX4,sum(VAT) as VAT,sum(CST) as CST,sum(AddCh) as AddCh,sum(RExciseDuty) as RExciseDuty,sum(REDCess) as REDCess ,sum(RHCess) as RHCess,sum(RTX4) as RTX4,sum(RVAT) as RVAT,sum(RCST) as RCST ,MAX(Comp_Name) as Comp_Name,MAX(add1) as add1  from ( select (month(TSPL_SRN_HEAD.SRN_Date)) as Mdate,year(TSPL_SRN_HEAD.SRN_Date) as Ydate,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.SRN_Qty,Amount , ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and (TSPL_SRN_DETAIL.TAX1 like ('%BED%')OR TSPL_SRN_DETAIL.TAX1 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX1_Amt else 0  end)  as ExciseDuty, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E' and (TSPL_SRN_DETAIL.TAX2 like ('%BED%')OR TSPL_SRN_DETAIL.TAX2 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX2_Amt else 0  end) as EDCess, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX3 like ('%BED%')OR TSPL_SRN_DETAIL.TAX3 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as HCess, ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX4 like ('%BED%')OR TSPL_SRN_DETAIL.TAX4 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX4 like ('HCESS')  ) then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as TX4, ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='V'  then TSPL_SRN_DETAIL.TAX1_Amt   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='V'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='V'   then TSPL_SRN_DETAIL.TAX3_Amt " & _
            ''             " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='V'  then TSPL_SRN_DETAIL.TAX4_Amt   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='V'   then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end) as VAT," & _
            ''           " ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='C'  then TSPL_SRN_DETAIL.TAX1_Amt  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='C'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='C'   then TSPL_SRN_DETAIL.TAX3_Amt   else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='C'   then TSPL_SRN_DETAIL.TAX4_Amt  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='C'  then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end)as CST, " & _
            ''            " isnull((TSPL_SRN_HEAD.Add_Charge_Amt1+TSPL_SRN_HEAD.Add_Charge_Amt2+TSPL_SRN_HEAD.Add_Charge_Amt3+TSPL_SRN_HEAD.Add_Charge_Amt4+TSPL_SRN_HEAD.Add_Charge_Amt5+TSPL_SRN_HEAD.Add_Charge_Amt6 +  isnull((select SUM(Amount) from TSPL_SRN_DETAIL where Row_Type='misc' and SRN_No=TSPL_SRN_HEAD.SRN_No) ,0 )),0)   as AddCh , " & _
            ''           "( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and (TSPL_SRN_DETAIL.TAX1 like ('%BED%')OR TSPL_SRN_DETAIL.TAX1 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX1_Amt else 0  end) as RExciseDuty, ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E' and (TSPL_SRN_DETAIL.TAX2 like ('%BED%')OR TSPL_SRN_DETAIL.TAX2 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_SRN_DETAIL.TAX2_Amt else 0  end) as REDCess,  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  " & _
            ''          " and (TSPL_SRN_DETAIL.TAX3 like ('%BED%')OR TSPL_SRN_DETAIL.TAX3 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as RHCess,( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3)='E'  and (TSPL_SRN_DETAIL.TAX4 like ('%BED%')OR TSPL_SRN_DETAIL.TAX4 like ('%ECESS%') Or TSPL_SRN_DETAIL.TAX4 like ('HCESS')  ) then TSPL_SRN_DETAIL.TAX3_Amt else 0  end) as RTX4, ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='V'   then TSPL_SRN_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='V'   then TSPL_SRN_DETAIL.TAX2_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='V'   then TSPL_SRN_DETAIL.TAX3_Amt  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='V'   then TSPL_SRN_DETAIL.TAX4_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='V'   then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end) as RVAT, " & _
            ''          " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='y' and " & _
            ''         " (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX1  )='C'  then TSPL_SRN_DETAIL.TAX1_Amt   " & _
            ''         " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX2  )='C'  then TSPL_SRN_DETAIL.TAX2_Amt " & _
            ''         " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX3  )='C'  then TSPL_SRN_DETAIL.TAX3_Amt " & _
            ''         "  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX4  )='C' then TSPL_SRN_DETAIL.TAX4_Amt " & _
            ''        "  else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_SRN_DETAIL.TAX5  )='C' then TSPL_SRN_DETAIL.TAX5_Amt else 0  end end end end end)" & _
            ''        " as RCST ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 from TSPL_SRN_HEAD " & _
            ''        " left outer join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SRN_HEAD.Comp_Code where  TSPL_SRN_DETAIL.Row_Type<>'misc' and TSPL_SRN_DETAIL.SRN_Qty<>0 and TSPL_SRN_DETAIL.Amount<>0  and  (month(TSPL_SRN_HEAD.SRN_Date))>='" + FMon + "' and  (month(TSPL_SRN_HEAD.SRN_Date))<='" + Tmon + "' and  (year(TSPL_SRN_HEAD.SRN_Date))>='" + FYear + "' and  (year(TSPL_SRN_HEAD.SRN_Date))<='" + TYear + "' "

            ''            qry = " select '" + FMonName + "' as FMonName,'" + TmonNAme + "' as TmonNAme ,'" + FYear + "' as FYear,'" + TYear + "' as TYear,Mdate,Ydate,max(SRN_No) as SRN_No,Item_Code,Item_Desc,sum(SRN_Qty) as SRN_Qty,sum(Amount) as Amount,sum(ExciseDuty) as ExciseDuty,sum(EDCess) as EDCess,sum(HCess) as HCess,sum(TX4) as TX4,sum(VAT) as VAT,sum(CST) as CST,sum(AddCh) as AddCh,sum(RExciseDuty) as RExciseDuty,sum(REDCess) as REDCess ,sum(RHCess) as RHCess,sum(RTX4) as RTX4,sum(RVAT) as RVAT,sum(RCST) as RCST ,MAX(Comp_Name) as Comp_Name,MAX(add1) as add1  from ( " & _
            ''                   "   select (month(convert(date,TSPL_PI_HEAD.pi_date,103))) as Mdate,year(convert(date,TSPL_PI_HEAD.PI_Date,103)) as Ydate,TSPL_PI_HEAD.PI_No as SRN_No,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Item_Desc,TSPL_PI_DETAIL.PI_Qty as SRN_Qty,Amount , " & _
            ''  " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and (TSPL_PI_DETAIL.TAX1 like ('%BED%')OR TSPL_PI_DETAIL.TAX1 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX1_Amt else 0  end) as ExciseDuty, " & _
            '' " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E' and (TSPL_PI_DETAIL.TAX2 like ('%BED%')OR TSPL_PI_DETAIL.TAX2 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX2_Amt else 0  end) as EDCess, " & _
            '' " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX3 like ('%BED%')OR TSPL_PI_DETAIL.TAX3 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_PI_DETAIL.TAX3_Amt else 0  end) as HCess, " & _
            '' " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX4 like ('%BED%')OR TSPL_PI_DETAIL.TAX4 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX4 like ('HCESS')  ) then TSPL_PI_DETAIL.TAX3_Amt else 0  end) as TX4, " & _
            '' " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='V'  then TSPL_PI_DETAIL.TAX1_Amt   " & _
            '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='V'   then TSPL_PI_DETAIL.TAX2_Amt  " & _
            '' " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='V'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
            '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='V'  then TSPL_PI_DETAIL.TAX4_Amt  " & _
            '' " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='V'   then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end) as VAT," & _
            '' "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='C'  then TSPL_PI_DETAIL.TAX1_Amt  " & _
            ''"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='C'   then TSPL_PI_DETAIL.TAX2_Amt   " & _
            ''"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='C'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
            '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='C'   then TSPL_PI_DETAIL.TAX4_Amt  " & _
            '' " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='C'  then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end)as CST, " & _
            ''  "  isnull((TSPL_PI_HEAD.Add_Charge_Amt1+TSPL_PI_HEAD.Add_Charge_Amt2+TSPL_PI_HEAD.Add_Charge_Amt3+TSPL_PI_HEAD.Add_Charge_Amt4+TSPL_PI_HEAD.Add_Charge_Amt5+TSPL_PI_HEAD.Add_Charge_Amt6 +  isnull((select SUM(Amount) from TSPL_PI_DETAIL where Row_Type='misc' and PI_NO=TSPL_PI_HEAD.PI_NO) ,0 )),0)   as AddCh , " & _
            ''   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and (TSPL_PI_DETAIL.TAX1 like ('%BED%')OR TSPL_PI_DETAIL.TAX1 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX1_Amt else 0  end) as RExciseDuty, " & _
            ''   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E' and (TSPL_PI_DETAIL.TAX2 like ('%BED%')OR TSPL_PI_DETAIL.TAX2 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX2_Amt else 0  end) as REDCess,  " & _
            ''   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'   and (TSPL_PI_DETAIL.TAX3 like ('%BED%')OR TSPL_PI_DETAIL.TAX3 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_PI_DETAIL.TAX3_Amt else 0  end) as RHCess, " & _
            ''    " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX4 like ('%BED%')OR TSPL_PI_DETAIL.TAX4 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX4 like ('HCESS')  ) then TSPL_PI_DETAIL.TAX3_Amt else 0  end) as RTX4, " & _
            ''    " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='V'   then TSPL_PI_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='V'   then TSPL_PI_DETAIL.TAX2_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='V'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
            ''" else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='V'   then TSPL_PI_DETAIL.TAX4_Amt  " & _
            '' " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='V'   then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end) as RVAT, " & _
            ''  "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='C'  then TSPL_PI_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='C'  then TSPL_PI_DETAIL.TAX2_Amt " & _
            ''  " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='C'  then TSPL_PI_DETAIL.TAX3_Amt   " & _
            '' " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='C' then TSPL_PI_DETAIL.TAX4_Amt   " & _
            '' " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='C' then TSPL_PI_DETAIL.TAX5_Amt " & _
            ''"  else 0  end end end end end) as RCST ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 " & _
            ''    "        from TSPL_PI_HEAD " & _
            ''  " left outer join TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_NO=TSPL_PI_DETAIL.PI_NO  " & _
            ''  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PI_HEAD.Comp_Code " & _
            ''  " where  TSPL_PI_DETAIL.Row_Type<>'misc' and TSPL_PI_DETAIL.PI_QTY<>0 and TSPL_PI_DETAIL.Amount<>0  " & _
            ''  " and  (month(convert(date,TSPL_PI_HEAD.pi_date,103)))>='" + FMon + "' and  (month(convert(date,TSPL_PI_HEAD.pi_date,103)))<='" + Tmon + "' " & _
            ''  " and  (year(convert(date,TSPL_PI_HEAD.PI_Date,103)))>='" + FYear + "' and  (year(convert(date,TSPL_PI_HEAD.PI_Date,103)))<='" + TYear + "'  and TSPL_PI_HEAD.Status='1' "




            qry = " select '" + FMonName + "' as FMonName,'" + TmonNAme + "' as TmonNAme ,'" + FYear + "' as FYear,'" + TYear + "' as TYear,Mdate,Ydate,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,max(SRN_No) as SRN_No,Item_Code,Item_Desc,sum(SRN_Qty) as SRN_Qty,sum(Amount) as Amount,sum(ExciseDuty) as ExciseDuty,sum(EDCess) as EDCess,sum(HCess) as HCess,sum(TX4) as TX4,sum(VAT) as VAT,sum(CST) as CST,sum(AddCh) as AddCh,sum(RExciseDuty) as RExciseDuty,sum(REDCess) as REDCess ,sum(RHCess) as RHCess,sum(RTX4) as RTX4,sum(RVAT) as RVAT,sum(RCST) as RCST ,MAX(Comp_Name) as Comp_Name,MAX(add1) as add1  from ( " & _
                   "   select (month(convert(date,TSPL_PI_HEAD.pi_date,103))) as Mdate,year(convert(date,TSPL_PI_HEAD.PI_Date,103)) as Ydate,TSPL_PI_HEAD.PI_No as SRN_No,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Item_Desc,TSPL_PI_DETAIL.PI_Qty as SRN_Qty,Amount , " & _
  " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and (TSPL_PI_DETAIL.TAX1 like ('%BED%')OR TSPL_PI_DETAIL.TAX1 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX1_Amt else 0  end) /TSPL_PI_DETAIL.PI_Qty as ExciseDuty, " & _
 " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E' and (TSPL_PI_DETAIL.TAX2 like ('%BED%')OR TSPL_PI_DETAIL.TAX2 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX2_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as EDCess, " & _
 " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX3 like ('%BED%')OR TSPL_PI_DETAIL.TAX3 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_PI_DETAIL.TAX3_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as HCess, " & _
 " ( case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX4 like ('%BED%')OR TSPL_PI_DETAIL.TAX4 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX4 like ('HCESS')  ) then TSPL_PI_DETAIL.TAX3_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as TX4, " & _
 " ( case when     (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='V'  then TSPL_PI_DETAIL.TAX1_Amt   " & _
 " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='V'   then TSPL_PI_DETAIL.TAX2_Amt  " & _
 " else case when      (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='V'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
 " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='V'  then TSPL_PI_DETAIL.TAX4_Amt  " & _
 " else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='V'   then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end)/TSPL_PI_DETAIL.PI_Qty as VAT," & _
 "  ( case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='C'  then TSPL_PI_DETAIL.TAX1_Amt  " & _
"  else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='C'   then TSPL_PI_DETAIL.TAX2_Amt   " & _
"  else case when   (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='C'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
 " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='C'   then TSPL_PI_DETAIL.TAX4_Amt  " & _
 " else case when    (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='C'  then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end)/TSPL_PI_DETAIL.PI_Qty as CST, " & _
  "  isnull((TSPL_PI_HEAD.Add_Charge_Amt1+TSPL_PI_HEAD.Add_Charge_Amt2+TSPL_PI_HEAD.Add_Charge_Amt3+TSPL_PI_HEAD.Add_Charge_Amt4+TSPL_PI_HEAD.Add_Charge_Amt5+TSPL_PI_HEAD.Add_Charge_Amt6 +  isnull((select SUM(Amount) from TSPL_PI_DETAIL where Row_Type='misc' and PI_NO=TSPL_PI_HEAD.PI_NO) ,0 )),0) /TSPL_PI_DETAIL.PI_Qty  as AddCh , " & _
   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and (TSPL_PI_DETAIL.TAX1 like ('%BED%')OR TSPL_PI_DETAIL.TAX1 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX1 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX1_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as RExciseDuty, " & _
   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E' and (TSPL_PI_DETAIL.TAX2 like ('%BED%')OR TSPL_PI_DETAIL.TAX2 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX2 like ('%HCESS%')  ) then TSPL_PI_DETAIL.TAX2_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as REDCess,  " & _
   "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'   and (TSPL_PI_DETAIL.TAX3 like ('%BED%')OR TSPL_PI_DETAIL.TAX3 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX3 like ('%HCESS%')  )  then TSPL_PI_DETAIL.TAX3_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as RHCess, " & _
    " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1)='E' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2)='E'and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3)='E'  and (TSPL_PI_DETAIL.TAX4 like ('%BED%')OR TSPL_PI_DETAIL.TAX4 like ('%ECESS%') Or TSPL_PI_DETAIL.TAX4 like ('HCESS')  ) then TSPL_PI_DETAIL.TAX3_Amt else 0  end)/TSPL_PI_DETAIL.PI_Qty as RTX4, " & _
    " ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='V'   then TSPL_PI_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='V'   then TSPL_PI_DETAIL.TAX2_Amt   else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='V'   then TSPL_PI_DETAIL.TAX3_Amt  " & _
" else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='V'   then TSPL_PI_DETAIL.TAX4_Amt  " & _
 " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='V'   then TSPL_PI_DETAIL.TAX5_Amt else 0  end end end end end)/TSPL_PI_DETAIL.PI_Qty as RVAT, " & _
  "  ( case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='y' and  (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX1  )='C'  then TSPL_PI_DETAIL.TAX1_Amt    else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX2  )='C'  then TSPL_PI_DETAIL.TAX2_Amt " & _
  " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX3  )='C'  then TSPL_PI_DETAIL.TAX3_Amt   " & _
 " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX4  )='C' then TSPL_PI_DETAIL.TAX4_Amt   " & _
 " else case when   (select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='y' and (select Type from TSPL_TAX_MASTER where Tax_Code=TSPL_PI_DETAIL.TAX5  )='C' then TSPL_PI_DETAIL.TAX5_Amt " & _
"  else 0  end end end end end)/TSPL_PI_DETAIL.PI_Qty as RCST ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 " & _
    "        from TSPL_PI_HEAD " & _
  " left outer join TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_NO=TSPL_PI_DETAIL.PI_NO   left outer join TSPL_ITEM_MASTER on TSPL_PI_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   " & _
  " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PI_HEAD.Comp_Code " & _
  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code = TSPL_PI_DETAIL.Location " & _
  " where  TSPL_PI_DETAIL.Row_Type<>'misc' and TSPL_PI_DETAIL.PI_QTY<>0 and TSPL_PI_DETAIL.Amount<>0  " & _
" and (YEAR(TSPL_PI_HEAD.pi_date) * 100 + MONTH(TSPL_PI_HEAD.pi_date)) >= " + first + " " & _
  " AND (YEAR(TSPL_PI_HEAD.pi_date) * 100 + MONTH(TSPL_PI_HEAD.pi_date)) <= " + second + "  "
            '" and  (year(convert(date,TSPL_PI_HEAD.PI_Date,103)))>='" + FYear + "' and  (year(convert(date,TSPL_PI_HEAD.PI_Date,103)))<='" + TYear + "'  and TSPL_PI_HEAD.Status='1'  and  TSPL_ITEM_MASTER.Item_Type='O' "

            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If

            If chkIselect.IsChecked Then
                qry += " and TSPL_PI_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(cgvItem.CheckedValue) + ")  "
            End If



            qry += " )final group by Mdate,final.Item_Code,final.Item_Desc,Ydate "









            '----------------------------------------------------------------------------------------------------------




            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "WTDPrice", "Detail of WTD.Price of Raw Material")
                frmCRV = Nothing

            End If




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmWTDRpt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class
