Imports common

'' Updated By Abhishek as on 28 Nov 2012 For Date Range Filter
Public Class FrmCEAllocationRpt
    Inherits FrmMainTranScreen


    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CEAllocationReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCEAllocationRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
    Private Sub FrmCEAllocationRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")


        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CE-ALOC-RPT"
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

    Public Sub reset()
        ddlhier.Text = "ALL"
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        LoadRoute()
        chkAllroute.IsChecked = True
    End Sub

    Sub LoadRoute()
        Dim qry As String = " select Route_No,Route_Desc from TSPL_ROUTE_MASTER"
        cgvRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvRoute.ValueMember = "Route_No"
        cgvRoute.DisplayMember = "Route_Desc"
    End Sub

    Private Sub chkAllroute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllroute.ToggleStateChanged
        cgvRoute.Enabled = Not chkAllroute.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()

    End Sub
    Sub print()
        Try
            Dim levelCode As String = ""
            Dim levelSDate As String = ""
            Dim levelEDate As String = ""
            Dim HierType As String = ""
            Dim qry As String = ""


            If chkrotesel.IsChecked = True AndAlso cgvRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one route Category or select ALL ")
                Return
            End If
            If clsCommon.CompairString(ddlhier.Text, "ALL") = CompairStringResult.Equal Then
                qry = " select * from  (select 'HOS' as hier ,TSPL_SALESMAN_MAPPING.Salesman_Code as RouteId, TSPL_ROUTE_MASTER.Route_Desc as RDesc,TSPL_ROUTE_MASTER.Type, " & _
                      " (TSPL_SALESMAN_MAPPING.Level1_Code) as HOS ,emp1.Emp_Name as HOSName," & _
                      " (TSPL_SALESMAN_MAPPING.Level2_Code) as TDM ,emp2.Emp_Name as TDMName, " & _
                      "  (TSPL_SALESMAN_MAPPING.Level3_Code) as ADC , emp3.Emp_Name as ADCName, " & _
                      "  (TSPL_SALESMAN_MAPPING.Level4_Code) as CE ,emp4.Emp_Name as CEName, " & _
                      "   convert(varchar(10),TSPL_SALESMAN_MAPPING.L1SDate,103) as HOSDate, " & _
                      "   convert(varchar(10),TSPL_SALESMAN_MAPPING.L2SDate,103) as TDMDate, " & _
                      "   convert(varchar(10),TSPL_SALESMAN_MAPPING.L3SDate,103) as ADCDate, " & _
                      " convert(varchar(10),TSPL_SALESMAN_MAPPING.L4SDate,103) as CEDate, " & _
                      " '' as HOSEDate ,'' as TDMEDate ,'' as ADCEDate ,'' as CEEDate , " & _
                      "  TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1 " & _
                      "  from TSPL_SALESMAN_MAPPING " & _
                      " left outer join TSPL_ROUTE_MASTER on TSPL_SALESMAN_MAPPING.Salesman_Code=TSPL_ROUTE_MASTER.Route_No  " & _
                      " left  outer join TSPL_EMPLOYEE_MASTER as EMP1 on EMP1.EMP_CODE=TSPL_SALESMAN_MAPPING.Level1_Code  " & _
                      "  left  outer join TSPL_EMPLOYEE_MASTER as EMP2 on EMP2.EMP_CODE=TSPL_SALESMAN_MAPPING.Level2_Code " & _
                      " left  outer join TSPL_EMPLOYEE_MASTER as EMP3 on EMP3.EMP_CODE=TSPL_SALESMAN_MAPPING.Level3_Code " & _
                      "   left  outer join TSPL_EMPLOYEE_MASTER as EMP4 on EMP4.EMP_CODE=TSPL_SALESMAN_MAPPING.Level4_Code  " & _
                      "  left outer Join TSPL_COMPANY_MASTER on TSPL_SALESMAN_MAPPING.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where TSPL_SALESMAN_MAPPING .L1SDate >=Convert(Date,'" + dtpFrmDate.Value + "',103) and  TSPL_SALESMAN_MAPPING .L1SDate <=Convert(Date,'" + dtpToDate.Value + "',103)  " & _
                      "   union all " & _
                      "  select 'HOS' as hier , TSPL_SALESMAN_MAPPING_HISTORY.Salesman_Code as RouteId, TSPL_ROUTE_MASTER.Route_Desc as RDesc,TSPL_ROUTE_MASTER.Type, " & _
                      " (TSPL_SALESMAN_MAPPING_HISTORY.Level1_Code) as HOS, emp1.Emp_Name as HOSName, " & _
                      " (TSPL_SALESMAN_MAPPING_HISTORY.Level2_Code) as TDM, emp2.Emp_Name as TDMName, " & _
                      " (TSPL_SALESMAN_MAPPING_HISTORY.Level3_Code) as ADC,  emp3.Emp_Name as ADCName, " & _
                      "(TSPL_SALESMAN_MAPPING_HISTORY.Level4_Code) as CE,emp4.Emp_Name as CEName, " & _
                      " convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L1SDate,103) as HOSDate,  " & _
                      " convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L2SDate,103) as TDMDate,  " & _
                      "  convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L3SDate,103) as ADCDate,  " & _
                      "  convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L4SDate,103) as CEDate,  " & _
                      "  convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L1EDate,103) as HOSEDate , " & _
                      "  convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L2EDate,103) as TDMEDate , " & _
                      "   convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L3EDate,103) as ADCEDate , " & _
                      "   convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY.L4EDate,103) as CEEDate , " & _
                      "       TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1  " & _
                      "    from TSPL_SALESMAN_MAPPING_HISTORY  " & _
                      "  left outer join TSPL_ROUTE_MASTER on TSPL_SALESMAN_MAPPING_HISTORY.Salesman_Code=TSPL_ROUTE_MASTER.Route_No   " & _
                      " left  outer join TSPL_EMPLOYEE_MASTER as EMP1 on EMP1.EMP_CODE=TSPL_SALESMAN_MAPPING_HISTORY.Level1_Code   " & _
                      "   left  outer join TSPL_EMPLOYEE_MASTER as EMP2 on EMP2.EMP_CODE=TSPL_SALESMAN_MAPPING_HISTORY.Level2_Code  " & _
                      "  left  outer join TSPL_EMPLOYEE_MASTER as EMP3 on EMP3.EMP_CODE=TSPL_SALESMAN_MAPPING_HISTORY.Level3_Code  " & _
                      " left  outer join TSPL_EMPLOYEE_MASTER as EMP4 on EMP4.EMP_CODE=TSPL_SALESMAN_MAPPING_HISTORY.Level4_Code    " & _
                      " left outer Join TSPL_COMPANY_MASTER on TSPL_SALESMAN_MAPPING_HISTORY.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code   where TSPL_SALESMAN_MAPPING_HISTORY.L1EDate <> '' and TSPL_SALESMAN_MAPPING_HISTORY .L1SDate >=Convert(Date,'" + dtpFrmDate.Value + "',103) and  TSPL_SALESMAN_MAPPING_HISTORY .L1SDate <=Convert(Date,'" + dtpToDate.Value + "',103)  ) abc  " & _
                      " where Type <> ''    "
                If chkrotesel.IsChecked Then
                    qry += " and  RouteId in  (" + clsCommon.GetMulcallString(cgvRoute.CheckedValue) + ")"
                End If

                qry += " order by RouteId ,convert(date,HoSDate,103),convert(date,TDMDate,103),convert(date,ADCDate,103),convert(date,CEDate,103) "



                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptCEAllocationALl", " CE Allocation Report")

            Else
                If clsCommon.CompairString(ddlhier.Text, "HOS") = CompairStringResult.Equal Then
                    levelCode = "Level1_Code"
                    levelSDate = "L1SDate"
                    levelEDate = "L1EDate"
                    HierType = "HOS"
                ElseIf clsCommon.CompairString(ddlhier.Text, "TDM") = CompairStringResult.Equal Then
                    levelCode = "Level2_Code"
                    levelSDate = "L2SDate"
                    levelEDate = "L2EDate"
                    HierType = "TDM"
                ElseIf clsCommon.CompairString(ddlhier.Text, "ADC") = CompairStringResult.Equal Then
                    levelCode = "Level3_Code"
                    levelSDate = "L3SDate"
                    levelEDate = "L3EDate"
                    HierType = "ADC"
                ElseIf clsCommon.CompairString(ddlhier.Text, "CE") = CompairStringResult.Equal Then
                    levelCode = "Level4_Code"
                    levelSDate = "L4SDate"
                    levelEDate = "L4EDate"
                    HierType = "CE"
                End If

                qry = "select * from  (select '" + HierType + "' as hier ,TSPL_SALESMAN_MAPPING.Salesman_Code as RouteId, TSPL_ROUTE_MASTER.Route_Desc as RDesc,TSPL_ROUTE_MASTER.Type,(TSPL_SALESMAN_MAPPING." + levelCode + ") as HCode ,TSPL_EMPLOYEE_MASTER.Emp_Name as HName,convert(varchar(10),TSPL_SALESMAN_MAPPING." + levelSDate + ",103) as HSDate,'' as HEDate ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 from TSPL_SALESMAN_MAPPING " & _
                                    " left outer join TSPL_ROUTE_MASTER on TSPL_SALESMAN_MAPPING.Salesman_Code=TSPL_ROUTE_MASTER.Route_No " & _
                                    " left  outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALESMAN_MAPPING." + levelCode + "  " & _
                                    " left outer Join TSPL_COMPANY_MASTER on TSPL_SALESMAN_MAPPING.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where  TSPL_SALESMAN_MAPPING." + levelSDate + " >=Convert(date,'" + dtpFrmDate.Value + "',103) and TSPL_SALESMAN_MAPPING." + levelSDate + "<=Convert(Date,'" + dtpToDate.Value + "',103)   " & _
                                    "  union all " & _
                                    " select '" + HierType + "' as hier , TSPL_SALESMAN_MAPPING_HISTORY.Salesman_Code as RouteId, TSPL_ROUTE_MASTER.Route_Desc as RDesc,TSPL_ROUTE_MASTER.Type,(TSPL_SALESMAN_MAPPING_HISTORY." + levelCode + ") as HCode,(TSPL_EMPLOYEE_MASTER.Emp_Name) as HName,convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY." + levelSDate + ",103) as HSDate, " & _
                                    " convert(varchar(10),TSPL_SALESMAN_MAPPING_HISTORY." + levelEDate + ",103) as HEDate , " & _
                                   " TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 from TSPL_SALESMAN_MAPPING_HISTORY " & _
                                   " left outer join TSPL_ROUTE_MASTER on TSPL_SALESMAN_MAPPING_HISTORY.Salesman_Code=TSPL_ROUTE_MASTER.Route_No " & _
                                   " left  outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALESMAN_MAPPING_HISTORY." + levelCode + " " & _
                                   " left outer Join TSPL_COMPANY_MASTER on TSPL_SALESMAN_MAPPING_HISTORY.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code   where TSPL_SALESMAN_MAPPING_HISTORY." + levelSDate + " >=COnvert(Date,'" + dtpFrmDate.Value + "',103) and TSPL_SALESMAN_MAPPING_HISTORY." + levelSDate + "<=Convert(Date,'" + dtpToDate.Value + "',103) and TSPL_SALESMAN_MAPPING_HISTORY." + levelEDate + " <> '' ) abc where Type <> ''  "


                If chkrotesel.IsChecked Then
                    qry += " and  RouteId in  (" + clsCommon.GetMulcallString(cgvRoute.CheckedValue) + ")"
                End If

                qry += "  order by RouteId ,convert(date,HSDate,103)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptCEAllocation", " CE Allocation Report")

            End If



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
