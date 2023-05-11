Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class RptCapexBudgetRevHis

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Const ReportID As String = "CapexHis"
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub
    Sub funReset()
        dtFromDate.Value = clsCommon.GETSERVERDATE
        dtToDate.Value = clsCommon.GETSERVERDATE
        txtmulticapex.arrValueMember = Nothing
        txtmultiSubCapex.arrValueMember = Nothing
        chkCapexBudget.IsChecked = True
        'gv3.Rows.Clear()
        'gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub LoadData()
        Try
            Dim dt As DataTable
            Dim strFromDate As String = clsCommon.GetPrintDate(dtFromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(dtToDate.Value, "dd/MMM/yyyy")
            Dim capexarr As ArrayList = txtmulticapex.arrValueMember
            Dim Subcapexarr As ArrayList = txtmultiSubCapex.arrValueMember
            Dim strQuery As String
            Dim strbaseQuery As String
            Dim strInnerQuery As String
            If chksubcapexBudget.IsChecked = True Then

                strbaseQuery = " select *,ROW_NUMBER ()  over (PARTITION  by [Capex Code],[Capex Sub Code],[Revision Number] order by [Capex Code],[Capex Sub Code],[Revision Number] ) as RowNum from ( select TSPL_CAPEX_BUDGET_MASTER.Code as [Capex Sub Code],TSPL_CAPEX_BUDGET_MASTER.description,convert(varchar,TSPL_CAPEX_BUDGET_MASTER.doc_date,103) as Rev_date,TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [Capex Code]," & _
                                                         " TSPL_CAPEX_BUDGET_MASTER.Revision_No as [Revision Number],TSPL_CAPEX_BUDGET_MASTER.budget as [Current Budget],TSPL_CAPEX_BUDGET_MASTER.Tolerence," & _
                                                         " ((case when TSPL_CAPEX_BUDGET_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER.revised_budget,0) end )*isnull(TSPL_CAPEX_BUDGET_MASTER.Tolerence,0))/100 as [Tolerence Amount]," & _
                                                         " TSPL_CAPEX_BUDGET_MASTER.inc_Budget as [Incremental Amount],TSPL_CAPEX_BUDGET_MASTER.revised_budget as [Revised Budget]," & _
                                                         "case when TSPL_CAPEX_BUDGET_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER.revised_budget,0) end as Bal ," & _
                                                        " convert(varchar,TSPL_CAPEX_BUDGET_MASTER.doc_date,103) as [Revised Date],0 as Hist_Version,'' as Status from TSPL_CAPEX_BUDGET_MASTER  where 2=2 " & _
                                                        " and  Cast(TSPL_CAPEX_BUDGET_MASTER.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_BUDGET_MASTER.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER.Capex_Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If
                If Subcapexarr IsNot Nothing AndAlso Subcapexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER.Code  IN (" + clsCommon.GetMulcallString(Subcapexarr) + ")"
                End If
                strbaseQuery += " UNION ALL "
                strbaseQuery += "select TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code as [Capex Sub Code],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.description,convert(varchar,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date,103) as Rev_date,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Capex_Code as [Capex Code]," & _
                                         " TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Revision_No as [Revision Number],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget as [Current Budget],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Tolerence," & _
                                         " ((case when TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget,0) end )*isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Tolerence,0))/100 as [Tolerence Amount]," & _
                                         " TSPL_CAPEX_BUDGET_MASTER_Hist_Data.inc_Budget as [Incremental Amount],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget as [Revised Budget]," & _
                                          "case when TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget,0) end as Bal ," & _
                                         " convert(varchar,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date,103) as [Revised Date],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Hist_Version,case when isnull(status.CODE ,'')='' then '' else 'Deleted' end as Status from TSPL_CAPEX_BUDGET_MASTER_Hist_Data " & _
                                         " left join (select TSPL_CAPEX_BUDGET_MASTER_Hist_Data.code,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date from TSPL_CAPEX_BUDGET_MASTER_Hist_Data " & _
                                        " WHERE not EXISTS (SELECT CODE FROM TSPL_CAPEX_BUDGET_MASTER as capex WHERE capex.CODE = TSPL_CAPEX_BUDGET_MASTER_Hist_Data.CODE " & _
                                        ") " & _
                                      " and  Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'  "
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Capex_Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If
                If Subcapexarr IsNot Nothing AndAlso Subcapexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(Subcapexarr) + ")"
                End If
                strbaseQuery += ") as status on status.CODE =TSPL_CAPEX_BUDGET_MASTER_Hist_Data.CODE " & _
                                        " where 2=2 " & _
                                " and  Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'  "
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Capex_Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If
                If Subcapexarr IsNot Nothing AndAlso Subcapexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(Subcapexarr) + ")"
                End If
                strbaseQuery += " )as xx "

                strInnerQuery = " select [Capex Sub Code], [Capex Code],[Revision Number],max(RowNum) as RowNum from (" & _
                    "" & strbaseQuery & "" & _
                    " ) as yy group by  [Capex Sub Code],[Capex Code],[Revision Number]"



                strQuery = "select  Final.[Revised Date], Final.[Capex Sub Code],Final.description,Final.[Capex Code],(((case when tspl_capex_master.revised_budget=0 then isnull(tspl_capex_master.budget,0) else isnull(tspl_capex_master.revised_budget,0) end )*isnull(tspl_capex_master.Tolerence,0))/100)+isnull(tspl_capex_master.revised_budget,0) as [Main Capex Budget],Final.[Revision Number],Final.[Current Budget],Final.[Incremental Amount],Final.[Revised Budget],Final.Tolerence,Final.[Tolerence Amount],Final.[Tolerence Amount]+Final.Bal as [Total Budget],Status from (" & _
                 "" & strbaseQuery & "" & _
                 " ) as Final " & _
                " INNER JOIN ( " & strInnerQuery & " ) AS SUBTABLE ON SUBTABLE.[Capex Code] =FINAL.[Capex Code] AND SUBTABLE.[Capex Sub Code] =FINAL.[Capex Sub Code] AND SUBTABLE .RowNum =FINAL.RowNum AND SUBTABLE.[Revision Number] =FINAL.[Revision Number] " & _
               " left join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE =Final.[Capex Code]" & _
                " order by [Capex Code],[Capex Sub Code],[Revision Number] "

                'strbaseQuery = " select TSPL_CAPEX_BUDGET_MASTER.Code as [Capex Sub Code],TSPL_CAPEX_BUDGET_MASTER.description,convert(varchar,TSPL_CAPEX_BUDGET_MASTER.doc_date,103) as Rev_date,TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [Capex Code]," & _
                '                                         " TSPL_CAPEX_BUDGET_MASTER.Revision_No as [Revision Number],TSPL_CAPEX_BUDGET_MASTER.budget as [Current Budget],TSPL_CAPEX_BUDGET_MASTER.Tolerence," & _
                '                                         " ((case when TSPL_CAPEX_BUDGET_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER.revised_budget,0) end )*isnull(TSPL_CAPEX_BUDGET_MASTER.Tolerence,0))/100 as [Tolerence Amount]," & _
                '                                         " TSPL_CAPEX_BUDGET_MASTER.inc_Budget as [Incremental Amount],TSPL_CAPEX_BUDGET_MASTER.revised_budget as [Revised Budget]," & _
                '                                         "case when TSPL_CAPEX_BUDGET_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER.revised_budget,0) end as Bal ," & _
                '                                        " convert(varchar,TSPL_CAPEX_BUDGET_MASTER.Doc_Date,103) as [Revised Date],0 as Hist_Version from TSPL_CAPEX_BUDGET_MASTER  where 2=2 " & _
                '                                        " and  Cast(TSPL_CAPEX_BUDGET_MASTER.Doc_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_BUDGET_MASTER.Doc_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                'If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER.Capex_Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                'End If
                'If Subcapexarr IsNot Nothing AndAlso Subcapexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER.Code  IN (" + clsCommon.GetMulcallString(Subcapexarr) + ")"
                'End If
                'strbaseQuery += " UNION ALL "
                'strbaseQuery += "select TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code as [Capex Sub Code],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.description,convert(varchar,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.doc_date,103) as Rev_date,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Capex_Code as [Capex Code]," & _
                '                         " TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Revision_No as [Revision Number],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget as [Current Budget],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Tolerence," & _
                '                         " ((case when TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget,0) end )*isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Tolerence,0))/100 as [Tolerence Amount]," & _
                '                         " TSPL_CAPEX_BUDGET_MASTER_Hist_Data.inc_Budget as [Incremental Amount],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget as [Revised Budget]," & _
                '                          "case when TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.revised_budget,0) end as Bal ," & _
                '                         " convert(varchar,TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Doc_Date,103) as [Revised Date],TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Hist_Version from TSPL_CAPEX_BUDGET_MASTER_Hist_Data   " & _
                '                         " INNER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code=TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code where 2=2 " & _
                '                        " and  Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Doc_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Doc_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                'If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Capex_Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                'End If
                'If Subcapexarr IsNot Nothing AndAlso Subcapexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_BUDGET_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(Subcapexarr) + ")"
                'End If
                'strQuery = "select  Final.[Revised Date], Final.[Capex Sub Code],Final.description,Final.[Capex Code],Final.[Revision Number],Final.[Current Budget] as [Original Budget],Final.[Incremental Amount],Final.[Revised Budget],Final.Tolerence,Final.[Tolerence Amount],Final.[Tolerence Amount]+Final.Bal as [Total Budget] from (" & _
                ' "" & strbaseQuery & "" & _
                ' " ) as Final order by Final.[Capex Code],Final.[Capex Sub Code],Final.[Revision Number] "

            Else

                strbaseQuery = " select *,ROW_NUMBER ()  over (PARTITION  by [Capex Code],[Revision Number] order by [Capex Code],[Revision Number] ) as RowNum from  ( select TSPL_CAPEX_MASTER.Code as [Capex Code],TSPL_CAPEX_MASTER.description, convert(varchar,TSPL_CAPEX_MASTER.doc_date,103) as Rev_date," & _
                                                     "  TSPL_CAPEX_MASTER.Revision_No as [Revision Number],TSPL_CAPEX_MASTER.budget as [Current Budget],TSPL_CAPEX_MASTER.Tolerence," & _
                                                     " ((case when TSPL_CAPEX_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_MASTER.budget,0) else isnull(TSPL_CAPEX_MASTER.revised_budget,0) end )*isnull(TSPL_CAPEX_MASTER.Tolerence,0))/100 as [Tolerence Amount]," & _
                                                     " TSPL_CAPEX_MASTER.inc_Budget as [Incremental Amount],TSPL_CAPEX_MASTER.revised_budget as [Revised Budget]," & _
                                                      " case when TSPL_CAPEX_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_MASTER.budget,0) else isnull(TSPL_CAPEX_MASTER.revised_budget,0) end as Bal ," & _
                                                    " convert(varchar,TSPL_CAPEX_MASTER.doc_date,103) as [Revised Date],0 as Hist_Version,'' as Status from TSPL_CAPEX_MASTER  where 2=2 " & _
                                                    " and  Cast(TSPL_CAPEX_MASTER.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_MASTER.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_MASTER.Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If


                'strbaseQuery = " select TSPL_CAPEX_MASTER.Code as [Capex Code],TSPL_CAPEX_MASTER.description, convert(varchar,TSPL_CAPEX_MASTER.doc_date,103) as Rev_date," & _
                '                                    " TSPL_CAPEX_MASTER.Revision_No as [Revision Number],TSPL_CAPEX_MASTER.budget as [Current Budget],TSPL_CAPEX_MASTER.Tolerence," & _
                '                                    " ((case when TSPL_CAPEX_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_MASTER.budget,0) else isnull(TSPL_CAPEX_MASTER.revised_budget,0) end )*isnull(TSPL_CAPEX_MASTER.Tolerence,0))/100 as [Tolerence Amount]," & _
                '                                    " TSPL_CAPEX_MASTER.inc_Budget as [Incremental Amount],TSPL_CAPEX_MASTER.revised_budget as [Revised Budget]," & _
                '                                     " case when TSPL_CAPEX_MASTER.revised_budget=0 then isnull(TSPL_CAPEX_MASTER.budget,0) else isnull(TSPL_CAPEX_MASTER.revised_budget,0) end as Bal ," & _
                '                                   " convert(varchar,TSPL_CAPEX_MASTER.doc_date,103) as [Revised Date],0 as Hist_Version from TSPL_CAPEX_MASTER  where 2=2 " & _
                '                                   " and  Cast(TSPL_CAPEX_MASTER.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_MASTER.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                'If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_MASTER.Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                'End If
                'strbaseQuery += " UNION ALL "
                'strbaseQuery += "select TSPL_CAPEX_MASTER_Hist_Data.Code as [Capex Code],TSPL_CAPEX_MASTER_Hist_Data.description, convert(varchar,TSPL_CAPEX_MASTER_Hist_Data.doc_date,103) as Rev_date," & _
                '                        " TSPL_CAPEX_MASTER_Hist_Data.Revision_No as [Revision Number],TSPL_CAPEX_MASTER_Hist_Data.budget as [Current Budget],TSPL_CAPEX_MASTER_Hist_Data.Tolerence," & _
                '                        " ((case when TSPL_CAPEX_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_MASTER_Hist_Data.revised_budget,0) end )*isnull(TSPL_CAPEX_MASTER_Hist_Data.Tolerence,0))/100 as [Tolerence Amount]," & _
                '                        " TSPL_CAPEX_MASTER_Hist_Data.inc_Budget as [Incremental Amount],TSPL_CAPEX_MASTER_Hist_Data.revised_budget as [Revised Budget]," & _
                '                         " case when TSPL_CAPEX_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_MASTER_Hist_Data.revised_budget,0) end as Bal ," & _
                '                        " convert(varchar,TSPL_CAPEX_MASTER_Hist_Data.doc_date,103) as [Revised Date],TSPL_CAPEX_MASTER_Hist_Data.Hist_Version from TSPL_CAPEX_MASTER_Hist_Data " & _
                '                         " INNER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code=TSPL_CAPEX_MASTER_Hist_Data.Code where 2=2 " & _
                ' " and  Cast(TSPL_CAPEX_MASTER_Hist_Data.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_MASTER_Hist_Data.hist_on as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "'"
                'If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                '    strbaseQuery += " AND TSPL_CAPEX_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                'End If

                strbaseQuery += " UNION ALL "
                strbaseQuery += "select TSPL_CAPEX_MASTER_Hist_Data.Code as [Capex Code],TSPL_CAPEX_MASTER_Hist_Data.description, convert(varchar,TSPL_CAPEX_MASTER_Hist_Data.doc_date,103) as Rev_date," & _
                                        " TSPL_CAPEX_MASTER_Hist_Data.Revision_No as [Revision Number],TSPL_CAPEX_MASTER_Hist_Data.budget as [Current Budget],TSPL_CAPEX_MASTER_Hist_Data.Tolerence," & _
                                        " ((case when TSPL_CAPEX_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_MASTER_Hist_Data.revised_budget,0) end )*isnull(TSPL_CAPEX_MASTER_Hist_Data.Tolerence,0))/100 as [Tolerence Amount]," & _
                                        " TSPL_CAPEX_MASTER_Hist_Data.inc_Budget as [Incremental Amount],TSPL_CAPEX_MASTER_Hist_Data.revised_budget as [Revised Budget]," & _
                                         " case when TSPL_CAPEX_MASTER_Hist_Data.revised_budget=0 then isnull(TSPL_CAPEX_MASTER_Hist_Data.budget,0) else isnull(TSPL_CAPEX_MASTER_Hist_Data.revised_budget,0) end as Bal ," & _
                                        " convert(varchar,TSPL_CAPEX_MASTER_Hist_Data.doc_date,103) as [Revised Date],TSPL_CAPEX_MASTER_Hist_Data.Hist_Version ,case when isnull(status.CODE ,'')='' then '' else 'Deleted' end as Status from TSPL_CAPEX_MASTER_Hist_Data " & _
                                        " left join (select TSPL_CAPEX_MASTER_Hist_Data.code,TSPL_CAPEX_MASTER_Hist_Data.doc_date from TSPL_CAPEX_MASTER_Hist_Data " & _
                                        " WHERE not EXISTS (SELECT CODE FROM TSPL_CAPEX_MASTER as capex WHERE capex.CODE = TSPL_CAPEX_MASTER_Hist_Data.CODE " & _
                                        ") " & _
                                       " and  Cast(TSPL_CAPEX_MASTER_Hist_Data.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_MASTER_Hist_Data.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "' "
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If
                strbaseQuery += ") as status on status.CODE =TSPL_CAPEX_MASTER_Hist_Data.CODE " & _
                                        " where 2=2 " & _
                 " and  Cast(TSPL_CAPEX_MASTER_Hist_Data.doc_date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_CAPEX_MASTER_Hist_Data.doc_date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate.Value), "dd/MMM/yyyy") + "' "
                If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                    strbaseQuery += " AND TSPL_CAPEX_MASTER_Hist_Data.Code  IN (" + clsCommon.GetMulcallString(capexarr) + ")"
                End If
                strbaseQuery += " )as xx "

                strInnerQuery = " select  [Capex Code],[Revision Number],max(RowNum) as RowNum from (" & _
                   "" & strbaseQuery & "" & _
                   " ) as yy group by [Capex Code],[Revision Number]"


                strQuery = "select Final.[Revised Date],Final.[Capex Code],Final.description,Final.[Revision Number],Final.[Current Budget],Final.[Incremental Amount],Final.[Revised Budget],Final.Tolerence,Final.[Tolerence Amount],Final.[Tolerence Amount]+Final.Bal as [Total Budget],Status from ( " & _
                 "" & strbaseQuery & "" & _
                 " ) as Final " & _
                " INNER JOIN ( " & strInnerQuery & " ) AS SUBTABLE ON SUBTABLE.[Capex Code] =FINAL.[Capex Code]  AND SUBTABLE .RowNum =FINAL.RowNum AND SUBTABLE.[Revision Number] =FINAL.[Revision Number] " & _
                " order by [Capex Code],[Revision Number] "

                'strQuery = "select Final.[Revised Date],Final.[Capex Code],Final.description,Final.[Revision Number],Final.[Current Budget] as [Original Budget] ,Final.[Incremental Amount],Final.[Revised Budget],Final.Tolerence,Final.[Tolerence Amount],Final.[Tolerence Amount]+Final.Bal as [Total Budget] from ( " & _
                ' "" & strbaseQuery & "" & _
                ' " ) as Final order by Final.[Capex Code],Final.[Revision Number] "

            End If
            dt = clsDBFuncationality.GetDataTable(strQuery)
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.DataSource = dt
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.SummaryRowsBottom.Clear()
            gv3.MasterTemplate.BestFitColumns()
            gv3.EnableFiltering = True

            gv3.ReadOnly = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")

            End If
            ReStoreGridLayout()
            'FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            'btnGenrate.Enabled = True
        End Try
    End Sub
    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv3.TableElement.TableHeaderHeight = 25
        gv3.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).IsVisible = True
            gv3.Columns(ii).FormatString = "{0:n2}"
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Current Budget", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Tolerence", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Tolerence Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Total Budget", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Revised Budget", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        gv3.ShowGroupPanel = False
        gv3.MasterTemplate.AutoExpandGroups = True

        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Private Sub RptCapexBudgetRevHis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
    End Sub

    Private Sub RptCapexBudgetRevHis_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub btnGenrate_Click(sender As Object, e As EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkCapexBudget.IsChecked = True, "CB", "SCB")
        TemplateGridview = gv3
        LoadData()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtmulticapex__My_Click(sender As Object, e As EventArgs) Handles txtmulticapex._My_Click

        Dim qry As String = "select TSPL_CAPEX_MASTER.CODE as Code ,TSPL_CAPEX_MASTER.DESCRIPTION as Name from TSPL_CAPEX_MASTER "
        txtmulticapex.arrValueMember = clsCommon.ShowMultipleSelectForm("CapexMulSel", qry, "Code", "Name", txtmulticapex.arrValueMember, txtmulticapex.arrDispalyMember)
    End Sub

    Private Sub txtmultiSubCapex__My_Click(sender As Object, e As EventArgs) Handles txtmultiSubCapex._My_Click
        Dim qry As String = "select code as code,description as name from TSPL_CAPEX_BUDGET_MASTER "
        If txtmulticapex.arrValueMember IsNot Nothing AndAlso txtmulticapex.arrValueMember.Count > 0 Then
            qry += " where Capex_code IN (" + clsCommon.GetMulcallString(txtmulticapex.arrValueMember) + ")"
        End If
        txtmultiSubCapex.arrValueMember = clsCommon.ShowMultipleSelectForm("subcapexPur", qry, "code", "Name", txtmultiSubCapex.arrValueMember, txtmultiSubCapex.arrDispalyMember)
    End Sub

    Private Sub RmiExcel_Click(sender As Object, e As EventArgs) Handles RmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arr As New List(Of String)()
                arr.Add("Company : " & objCommonVar.CurrentCompanyName)
                arr.Add("Name : Capex Budget Revision History Report")
                arr.Add("Report Type : " + IIf(chkCapexBudget.IsChecked = True, "Capex Budget", "Sub Capex Budget"))
                arr.Add(("Date Range: " + clsCommon.GetPrintDate(dtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtToDate.Value, "dd/MM/yyyy")) + " ")
                If txtmulticapex.arrValueMember IsNot Nothing AndAlso txtmulticapex.arrValueMember.Count > 0 Then
                    arr.Add("Capex Budget : " + clsCommon.GetMulcallStringWithComma(txtmulticapex.arrDispalyMember))
                End If
                If txtmultiSubCapex.arrValueMember IsNot Nothing AndAlso txtmultiSubCapex.arrValueMember.Count > 0 Then
                    arr.Add("Sub Capex Budget : " + clsCommon.GetMulcallStringWithComma(txtmultiSubCapex.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv3, "", Me.Text, , arr)
                    'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arr)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Capex Budget Revision History Report", gv3, arr, "Capex Budget Revision History Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete Layout saved successfully", "Information")
    End Sub
End Class
