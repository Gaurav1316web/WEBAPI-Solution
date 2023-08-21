Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class rptAutoMultipleAdditionDeduction
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub rptAutoMultipleAdditionDeduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        'txtLocation.arrValueMember = Nothing
        txtMultiVSP.arrValueMember = Nothing
        TxtDeductionCode.arrValueMember = Nothing
        'TxtItem.arrDispalyMember = Nothing
        chkDCSWise.Checked = False
        'LoadTypes()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub txtMultiVSP__My_Click(sender As Object, e As EventArgs) Handles txtMultiVSP._My_Click

        Dim qry As String = " select M.Vendor_Code AS [Code], m.Vendor_Name as [Name],ISNULL(m.alies_name,'') As [Alies Name],TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [Plant Name],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code " &
                               " left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = M.Vendor_Code " &
                               " Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC " &
                               " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code where m.Status='N' "
        txtMultiVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "Name", txtMultiVSP.arrValueMember, txtMultiVSP.arrDispalyMember)

    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click

    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where  Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
    '    'Dim qry As String = "select MCC_Code as Code ,MCC_NAME  as Name from TSPL_MCC_MASTER"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)

    'End Sub

    Private Sub TxtDeductionCode__My_Click(sender As Object, e As EventArgs) Handles TxtDeductionCode._My_Click

        Dim qry As String = " Select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Ded_Grp_Code As [Deduction Group Code],TSPL_DEDUCTION_GROUP.Ded_Description As [Deduction Group Description] ,TSPL_DEDUCTION_MASTER.GL_Account_Code As [GL Account],TSPL_GL_ACCOUNTS.Description As [GL Account Desc],Security  from TSPL_DEDUCTION_MASTER  left outer join TSPL_DEDUCTION_GROUP On TSPL_DEDUCTION_GROUP.Ded_Code=TSPL_DEDUCTION_MASTER.Ded_Grp_Code  left outer join TSPL_GL_ACCOUNTS On TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code    where Security ='0' "
        TxtDeductionCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DEDMulSel", qry, "Code", "Description", TxtDeductionCode.arrValueMember, TxtDeductionCode.arrDispalyMember)

    End Sub

    'Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click

    '    Dim qry As String = " Select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Ded_Grp_Code As [Deduction Group Code],TSPL_DEDUCTION_GROUP.Ded_Description As [Deduction Group Description] ,TSPL_DEDUCTION_MASTER.GL_Account_Code As [GL Account],TSPL_GL_ACCOUNTS.Description As [GL Account Desc],Security  from TSPL_DEDUCTION_MASTER  left outer join TSPL_DEDUCTION_GROUP On TSPL_DEDUCTION_GROUP.Ded_Code=TSPL_DEDUCTION_MASTER.Ded_Grp_Code  left outer join TSPL_GL_ACCOUNTS On TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code    where Security ='0' "
    '    TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemDMulSel", qry, "Code", "Description", TxtItem.arrValueMember, TxtItem.arrDispalyMember)

    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(clsUserMgtCode.rptAutoMultipleAdditionDeduction) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.rptAutoMultipleAdditionDeduction, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whrV As String = ""
            Dim whrD As String = ""
            Dim User_Name As String = objCommonVar.CurrentUser
            Dim dt As New DataTable

            If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                whrV = "  where xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
            End If

            If TxtDeductionCode.arrValueMember IsNot Nothing AndAlso TxtDeductionCode.arrValueMember.Count > 0 Then
                whrD = " and xx.[Deduction Code] in (" + clsCommon.GetMulcallString(TxtDeductionCode.arrValueMember) + ")"
            End If

            If chkDCSWise.Checked = True Then
                qry = " select '" & User_Name & "' as User_Name, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "'  As ToDate, max(xx.RegNo) as RegNo ,max(xx.Phone) as Phone , max (xx.[Comp Name]) as [Comp Name] , xx.[Vendor Code],max (xx.[DCS Code]) as [DCS Code] ,max (xx.[Document No]) as [Document No] ,max (xx.[Document Date]) as [Document Date] ,sum (xx.Formula) as Formula ,sum (xx.[Base Amount/Quantity]) as [Base Amount/Quantity] ,sum (xx.[Addition/Deduction Amount]) as [Addition/Deduction Amount] ,xx.[Addition/Deduction Description],
                        xx.Type,sum (xx.Addition) as Addition ,sum (xx.Deduction) as Deduction ,max (xx.[Deduction Code]) as [Deduction Code] 
                        from
                        (Select 'Query1' as Query,Phone1 as Phone,Regn_No as RegNo,Comp_Name as [Comp Name],Vendor_Code as [Vendor Code] ,[VLC Uploader Code] as [DCS Code],Document_No as [Document No],Document_Date as [Document Date],0 as [Formula],0 AS [Base Amount/Quantity] ,0 As [Addition/Deduction Amount],Type as Type,Addition as Addition,Deduction as Deduction,DeductionCode as [Deduction Code] ,Deduction_Desc as [Addition/Deduction Description] 
                        from 
                        ( select TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Regn_No, TSPL_COMPANY_MASTER.Comp_Name, TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code, TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.amount  end as Deduction,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code]  from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)  )x

                        union all

                        select 'Query2' as Query,Phone1 as	Phone,Regn_No as RegNo,Comp_Name as [Comp Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [ Vendor Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],'' as [Document No],'' as [Document Date],Applicable_Value as [Formula],CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                                            cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                             when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                                            cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                            else 0 end AS [Base Amount/Quantity] ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount], '' as Type, 0 as Addition,
                        0 as Deduction,'' as [Deduction Code] , TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]                           
                                                                 from TSPL_VENDOR_INVOICE_DETAIL
                                                            LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                                            left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_INVOICE_HEAD.Comp_Code
                                                            LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                                            left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                                            WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>= '" & fromDate.Value & "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<= '" & ToDate.Value & "'  
									                        )xx " + whrV + " " + whrD + "     group by xx.[Addition/Deduction Description],xx.Type,XX.[Vendor Code] "
            Else

                qry = " select '" & User_Name & "' as User_Name,   '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "'  As ToDate, max(xx.RegNo) as RegNo ,max(xx.Phone) as Phone ,  max (xx.[Comp Name]) as [Comp Name] , max (xx.[Vendor Code]) as [Vendor Code] ,max (xx.[DCS Code]) as [DCS Code] ,max (xx.[Document No]) as [Document No] ,max (xx.[Document Date]) as [Document Date] ,sum (xx.Formula) as Formula ,sum (xx.[Base Amount/Quantity]) as [Base Amount/Quantity] ,sum (xx.[Addition/Deduction Amount]) as [Addition/Deduction Amount] ,xx.[Addition/Deduction Description],
                    xx.Type,sum (xx.Addition) as Addition ,sum (xx.Deduction) as Deduction ,max (xx.[Deduction Code]) as [Deduction Code] 
                    from
                    (Select 'Query1' as Query, Phone1 as Phone,Regn_No as RegNo, Comp_Name as [Comp Name],Vendor_Code as [Vendor Code] ,[VLC Uploader Code] as [DCS Code],Document_No as [Document No],Document_Date as [Document Date],0 as [Formula],0 AS [Base Amount/Quantity] ,0 As [Addition/Deduction Amount],Type as Type,Addition as Addition,Deduction as Deduction,DeductionCode as [Deduction Code] ,Deduction_Desc as [Addition/Deduction Description] 
                    from 
                    ( select TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Regn_No,  TSPL_COMPANY_MASTER.Comp_Name, TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code, TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.amount  end as Deduction,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code]  from TSPL_MULTIPLE_DEDUCTION_HEAD 
                    LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                    LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code
                    left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                    where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                    and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)  )x

                    union all

                    select 'Query2' as Query,Phone1 as	Phone,Regn_No as RegNo, Comp_Name as [Comp Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [ Vendor Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],'' as [Document No],'' as [Document Date],Applicable_Value as [Formula],CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                                        cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                         when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                                        cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                        else 0 end AS [Base Amount/Quantity] ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount], '' as Type, 0 as Addition,
                                                        0 as Deduction,'' as [Deduction Code] , TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]                                
                                                        from TSPL_VENDOR_INVOICE_DETAIL
                                                        LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                                        left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_INVOICE_HEAD.Comp_Code
                                                        LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                                        left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                                        WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>=  '" & fromDate.Value & "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<= '" & ToDate.Value & "'  
									                    )xx " + whrV + " " + whrD + "    group by xx.[Addition/Deduction Description],xx.Type	 "

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
                    'Gv1.Rows.Add()
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True

                FormatGrid()
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        Gv1.Columns("User_Name").IsVisible = False
        Gv1.Columns("User_Name").Width = 100
        Gv1.Columns("User_Name").HeaderText = "User Name"

        Gv1.Columns("FromDate").IsVisible = False
        Gv1.Columns("FromDate").Width = 100
        Gv1.Columns("FromDate").HeaderText = "From Date"

        Gv1.Columns("ToDate").IsVisible = False
        Gv1.Columns("ToDate").Width = 100
        Gv1.Columns("ToDate").HeaderText = "To Date"

        Gv1.Columns("RegNo").IsVisible = False
        Gv1.Columns("RegNo").Width = 100
        Gv1.Columns("RegNo").HeaderText = "RegNo"

        Gv1.Columns("Phone").IsVisible = False
        Gv1.Columns("Phone").Width = 100
        Gv1.Columns("Phone").HeaderText = "Phone"

        Gv1.Columns("Comp Name").IsVisible = False
        Gv1.Columns("Comp Name").Width = 100
        Gv1.Columns("Comp Name").HeaderText = "Comp Name"

        Gv1.Columns("Vendor Code").IsVisible = True
        Gv1.Columns("Vendor Code").Width = 100
        Gv1.Columns("Vendor Code").HeaderText = "Vendor Code"

        Gv1.Columns("DCS Code").IsVisible = True
        Gv1.Columns("DCS Code").Width = 100
        Gv1.Columns("DCS Code").HeaderText = "DCS Code"

        Gv1.Columns("Document No").IsVisible = True
        Gv1.Columns("Document No").Width = 100
        Gv1.Columns("Document No").HeaderText = "Document Number"

        Gv1.Columns("Document Date").IsVisible = True
        Gv1.Columns("Document Date").Width = 100
        Gv1.Columns("Document Date").HeaderText = "Document Date"

        Gv1.Columns("Formula").IsVisible = True
        Gv1.Columns("Formula").Width = 100
        Gv1.Columns("Formula").HeaderText = "Formula"

        Gv1.Columns("Base Amount/Quantity").IsVisible = True
        Gv1.Columns("Base Amount/Quantity").Width = 100
        Gv1.Columns("Base Amount/Quantity").HeaderText = "Base Amount/Quantity"

        Gv1.Columns("Addition/Deduction Amount").IsVisible = True
        Gv1.Columns("Addition/Deduction Amount").Width = 100
        Gv1.Columns("Addition/Deduction Amount").HeaderText = "Addition/Deduction Amount"

        Gv1.Columns("Addition/Deduction Description").IsVisible = True
        Gv1.Columns("Addition/Deduction Description").Width = 100
        Gv1.Columns("Addition/Deduction Description").HeaderText = "Addition/Deduction Description"

        Gv1.Columns("Type").IsVisible = True
        Gv1.Columns("Type").Width = 100
        Gv1.Columns("Type").HeaderText = "Type"

        Gv1.Columns("Addition").IsVisible = True
        Gv1.Columns("Addition").Width = 100
        Gv1.Columns("Addition").HeaderText = "Addition"

        Gv1.Columns("Deduction").IsVisible = True
        Gv1.Columns("Deduction").Width = 100
        Gv1.Columns("Deduction").HeaderText = "Deduction"

        Gv1.Columns("Deduction Code").IsVisible = True
        Gv1.Columns("Deduction Code").Width = 100
        Gv1.Columns("Deduction Code").HeaderText = "Deduction Code"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAutoMultipleAdditionDeduction & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtMultiVSP.arrDispalyMember IsNot Nothing AndAlso txtMultiVSP.arrDispalyMember.Count > 0 Then
                arrHeader.Add("VSP Code : " + clsCommon.GetMulcallStringWithComma(txtMultiVSP.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, clsUserMgtCode.rptMultipleDeductionReport)
                clsCommon.MyExportToExcelGrid("Auto Multiple Addition Deduction", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, clsUserMgtCode.rptMultipleDeductionReport)
                clsCommon.MyExportToPDF("Auto Multiple Addition Deduction", Gv1, arrHeader, Me.Text, clsUserMgtCode.rptAutoMultipleAdditionDeduction, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = clsUserMgtCode.rptAutoMultipleAdditionDeduction
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
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(clsUserMgtCode.rptAutoMultipleAdditionDeduction, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

            Dim qry As String = ""
            Dim whrV As String = ""
            Dim whrD As String = ""
            Dim User_Name As String = objCommonVar.CurrentUser
            'Dim dt As New DataTable

            If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                whrV = "  where xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
            End If

            If TxtDeductionCode.arrValueMember IsNot Nothing AndAlso TxtDeductionCode.arrValueMember.Count > 0 Then
                whrD = " and xx.[Deduction Code] in (" + clsCommon.GetMulcallString(TxtDeductionCode.arrValueMember) + ")"
            End If

            If chkDCSWise.Checked = True Then
                qry = " select '" & User_Name & "' as User_Name,  '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "'  As ToDate, max(xx.RegNo) as RegNo ,max(xx.Phone) as Phone , max (xx.[Comp Name]) as [Comp Name] , xx.[Vendor Code],max (xx.[DCS Code]) as [DCS Code] ,max (xx.[Document No]) as [Document No] ,max (xx.[Document Date]) as [Document Date] ,sum (xx.Formula) as Formula ,sum (xx.[Base Amount/Quantity]) as [Base Amount/Quantity] ,sum (xx.[Addition/Deduction Amount]) as [Addition/Deduction Amount] ,xx.[Addition/Deduction Description],
                        xx.Type,sum (xx.Addition) as Addition ,sum (xx.Deduction) as Deduction ,max (xx.[Deduction Code]) as [Deduction Code] 
                        from
                        (Select 'Query1' as Query,Phone1 as Phone,Regn_No as RegNo,Comp_Name as [Comp Name],Vendor_Code as [Vendor Code] ,[VLC Uploader Code] as [DCS Code],Document_No as [Document No],Document_Date as [Document Date],0 as [Formula],0 AS [Base Amount/Quantity] ,0 As [Addition/Deduction Amount],Type as Type,Addition as Addition,Deduction as Deduction,DeductionCode as [Deduction Code] ,Deduction_Desc as [Addition/Deduction Description] 
                        from 
                        ( select TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Regn_No, TSPL_COMPANY_MASTER.Comp_Name, TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code, TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.amount  end as Deduction,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code]  from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)  )x

                        union all

                        select 'Query2' as Query,Phone1 as	Phone,Regn_No as RegNo,Comp_Name as [Comp Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [ Vendor Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],'' as [Document No],'' as [Document Date],Applicable_Value as [Formula],CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                                            cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                             when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                                            cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                            else 0 end AS [Base Amount/Quantity] ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount], '' as Type, 0 as Addition,
                        0 as Deduction,'' as [Deduction Code] , TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]                           
                                                                 from TSPL_VENDOR_INVOICE_DETAIL
                                                            LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                                            left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_INVOICE_HEAD.Comp_Code
                                                            LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                                            left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                                            WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>= '" & fromDate.Value & "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<= '" & ToDate.Value & "'  
									                        )xx " + whrV + " " + whrD + "     group by xx.[Addition/Deduction Description],xx.Type,XX.[Vendor Code] "
            Else

                qry = " select  '" & User_Name & "' as User_Name,  '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "'  As ToDate, max(xx.RegNo) as RegNo ,max(xx.Phone) as Phone ,  max (xx.[Comp Name]) as [Comp Name] , max (xx.[Vendor Code]) as [Vendor Code] ,max (xx.[DCS Code]) as [DCS Code] ,max (xx.[Document No]) as [Document No] ,max (xx.[Document Date]) as [Document Date] ,sum (xx.Formula) as Formula ,sum (xx.[Base Amount/Quantity]) as [Base Amount/Quantity] ,sum (xx.[Addition/Deduction Amount]) as [Addition/Deduction Amount] ,xx.[Addition/Deduction Description],
                    xx.Type,sum (xx.Addition) as Addition ,sum (xx.Deduction) as Deduction ,max (xx.[Deduction Code]) as [Deduction Code] 
                    from
                    (Select 'Query1' as Query, Phone1 as Phone,Regn_No as RegNo, Comp_Name as [Comp Name],Vendor_Code as [Vendor Code] ,[VLC Uploader Code] as [DCS Code],Document_No as [Document No],Document_Date as [Document Date],0 as [Formula],0 AS [Base Amount/Quantity] ,0 As [Addition/Deduction Amount],Type as Type,Addition as Addition,Deduction as Deduction,DeductionCode as [Deduction Code] ,Deduction_Desc as [Addition/Deduction Description] 
                    from 
                    ( select TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Regn_No,  TSPL_COMPANY_MASTER.Comp_Name, TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code, TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.amount  end as Deduction,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code]  from TSPL_MULTIPLE_DEDUCTION_HEAD 
                    LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                    LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code
                    left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                    where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                    and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103)  )x

                    union all

                    select 'Query2' as Query,Phone1 as	Phone,Regn_No as RegNo, Comp_Name as [Comp Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [ Vendor Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],'' as [Document No],'' as [Document Date],Applicable_Value as [Formula],CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                                        cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                         when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                                        cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                                        else 0 end AS [Base Amount/Quantity] ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount], '' as Type, 0 as Addition,
                                                        0 as Deduction,'' as [Deduction Code] , TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]                                
                                                        from TSPL_VENDOR_INVOICE_DETAIL
                                                        LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                                        left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_VENDOR_INVOICE_HEAD.Comp_Code
                                                        LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                                        left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                                        WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>=  '" & fromDate.Value & "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<= '" & ToDate.Value & "'  
									                    )xx " + whrV + " " + whrD + "    group by xx.[Addition/Deduction Description],xx.Type	 "

            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptAutoMultipleAdditionDeduction", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class