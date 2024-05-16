Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'================Create by Preeti Gupta=========

Public Class rptSalesHierarchyReport

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable

    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLevel1 As ArrayList
    Public arrLevel2 As ArrayList
    Public arrLevel3 As ArrayList
    Public arrLevel4 As ArrayList
    Public arrLevel5 As ArrayList
    Public arrLevel6 As ArrayList
    Public arrLevel7 As ArrayList
    Public arrLevel8 As ArrayList
    Public arrLevel9 As ArrayList
    '' new filters

    'Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptSalesHierarchyReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        'btnExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub LoadTypes()
        dt = New DataTable
        Dim qry As String = "select Level_Code as Code,Seq_No as Value from TSPL_Sales_Hierarchy_Levels  order by Seq_No"
        dt = clsDBFuncationality.GetDataTable(qry)
        'dt.Columns.Add("Code", GetType(String))
        'dt.Rows.Add("Total Sale")
        'dt.Rows.Add("Location Wise")
        'dt.Rows.Add("Item Group Wise")
        'dt.Rows.Add("Customer Group Wise")
        'dt.Rows.Add("Item Wise")
        'dt.Rows.Add("Customer Wise")
        'dt.Rows.Add("Document Wise")
        'dt.Rows.Add("Document Detail")

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Value"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            If clsCommon.myLen(ddlReportType.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select any Report Type", Me.Text)
                Exit Sub
            End If
           
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("UOM : " + txtUOM.Value)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Sales Hierarchy Report:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sales Hierarchy Report" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sales Hierarchy Report", True)
                Exit Sub
            End If

            clsCommon.ProgressBarShow()
            ddlReportType.Enabled = False
            txtLevel2.Enabled = False
            txtLevel1.Enabled = False
            txtLevel3.Enabled = False
            txtLevel4.Enabled = False
            txtLevel6.Enabled = False
            txtLevel5.Enabled = False
            txtLevel7.Enabled = False
            txtMultLevel8.Enabled = False
            txtMultLevel9.Enabled = False
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = ReturnQuery()

            If ddlReportType.SelectedValue = 1 Then
                strRunQuery = "select  Level1,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by Level1 "
            ElseIf ddlReportType.SelectedValue = 2 Then
                strRunQuery = "select  Level1,Level2,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by  Level1,Level2 "
            ElseIf ddlReportType.SelectedValue = 3 Then
                strRunQuery = "select  Level1,Level2,Level3,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by  Level1 ,Level2,Level3"
            ElseIf ddlReportType.SelectedValue = 4 Then
                strRunQuery = "select  Level1 ,Level2,Level3,Level4,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by   Level1 ,Level2,Level3,Level4 "
            ElseIf ddlReportType.SelectedValue = 5 Then
                strRunQuery = "select   Level1 ,Level2,Level3,Level4,Level5,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by  Level1 ,Level2,Level3,Level4,Level5"
            ElseIf ddlReportType.SelectedValue = 6 Then
                strRunQuery = "select Level1 ,Level2,Level3,Level4,Level5,Level6,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by Level1 ,Level2,Level3,Level4,Level5,Level6 "
            ElseIf ddlReportType.SelectedValue = 7 Then
                strRunQuery = "select Level1 ,Level2,Level3,Level4,Level5,Level6,Level7,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by Level1 ,Level2,Level3,Level4,Level5,Level6,Level7 "
            ElseIf ddlReportType.SelectedValue = 8 Then
                strRunQuery = "select Level1 ,Level2,Level3,Level4,Level5,Level6,Level7,Level8,count(distinct [Document No]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by Level1 ,Level2,Level3,Level4,Level5,Level6,Level7,Level8 "
            ElseIf ddlReportType.SelectedValue = 9 Then
                strRunQuery = strMain '"select  Level1.Struct_Code as Level1,Level2.Struct_Code as Level2,Level3.Struct_Code as Level3,Level4.Struct_Code as Level4,Level5.Struct_Code as Level5,Level6.Struct_Code as Level6,Level7.Struct_Code as Level7,Level8.Struct_Code as Level8,count([Customer Code]) as [Total Sale Count],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by  Level1.Struct_Code ,Level2.Struct_Code,Level3.Struct_Code,Level4.Struct_Code,Level5.Struct_Code,Level6.Struct_Code,Level7.Struct_Code,Level8.Struct_Code"
            Else
                clsCommon.MyMessageBoxShow(Me, "Select any Report Type", Me.Text)
                Exit Sub
            End If
            RadPageViewPage2.Text = ddlReportType.Text

            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            Gv1.Tag = ddlReportType.Text

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            FindAndRestoreGridLayout(Me)
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2





        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try


    End Sub
    Function ReturnQuery() As String
        Dim Qry As String = ""
        Dim qryList As ArrayList
        Dim obj As New clsSaleRegisterParameterType
        Qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
        Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(Qry)
        Dim arrTrans As New ArrayList
        For Each dr As DataRow In dtTrans.Rows
            arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
        Next
        obj.Trans_Type_List = arrTrans
        
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No            
        End If
        obj.Unit_Code = txtUOM.Value
        obj.ReportType = ddlReportType.SelectedValue
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
        qryList = XpertERPEngine.clsPSInvoiceHead.ReturnQuery(obj)
        Dim strMCCMaterial As String = qryList(0)
        If btnPosted.IsChecked Then
            strMCCMaterial += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            strMCCMaterial += " and xx.Status=0  "
        End If

        strPivotForFinalOuterQuery = qryList(1)
        'Dim ParentQry As String = "select TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Parent_Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Level_Code," & _
        '      " TSPL_Sales_Hierarchy_Levels.Is_First_Level,TSPL_Sales_Hierarchy_Levels.Is_Last_Level,TSPL_Sales_Hierarchy_Levels.Level_Type,TSPL_Sales_Hierarchy_Levels.Seq_No, " & _
        '      " TSPL_Sales_Hierarchy_Structure_Hist.Updated_Date from  TSPL_Sales_Hierarchy_Structure_Hist " & _
        '      " left join ( " & _
        '      " select Hist.Struct_Code,Hist.Level_Code,Hist.Parent_Struct_Code,max(Hist_Id) as Hist_Id from TSPL_Sales_Hierarchy_Structure_Hist as Hist " & _
        '      " inner join TSPL_Sales_Hierarchy_Levels as HLevel on Hist.Level_Code=HLevel.Level_Code  WHERE  Hist.updated_Date<=CURRENT_TIMESTAMP and HLevel.Level_Type='Sales Person' " & _
        '      " GROUP BY Hist.Struct_Code,Hist.Level_Code,Hist.Parent_Struct_Code HAVING MAX(Hist.updated_Date) <=CURRENT_TIMESTAMP ) Hier " & _
        '      " on Hier.Hist_Id=TSPL_Sales_Hierarchy_Structure_Hist.Hist_Id " & _
        '      " inner join TSPL_Sales_Hierarchy_Levels on Hier.Level_Code=TSPL_Sales_Hierarchy_Levels.Level_Code "
        Dim ParentQry As String = " select Hier1.*,Hier2.Updated_Date as Updated_Date2 from ( " & _
                                  " select TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Description as Struct_Desc,TSPL_Sales_Hierarchy_Structure_Hist.Parent_Struct_Code, " & _
                                  " TSPL_Sales_Hierarchy_Structure_Hist.Updated_Date,(ROW_NUMBER()  over (PARTITION BY TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code " & _
                                  " order by TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Updated_Date)) as Update_Version " & _
                                  " from  TSPL_Sales_Hierarchy_Structure_Hist " & _
                                  " inner join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Structure_Hist.Level_Code=TSPL_Sales_Hierarchy_Levels.Level_Code) as Hier1 " & _
                                  " left join (select TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Parent_Struct_Code, " & _
                                  " TSPL_Sales_Hierarchy_Structure_Hist.Updated_Date,(ROW_NUMBER()  over (PARTITION BY TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code " & _
                                  " order by TSPL_Sales_Hierarchy_Structure_Hist.Struct_Code,TSPL_Sales_Hierarchy_Structure_Hist.Updated_Date)) as Update_Version " & _
                                  " from  TSPL_Sales_Hierarchy_Structure_Hist " & _
                                  " inner join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Structure_Hist.Level_Code=TSPL_Sales_Hierarchy_Levels.Level_Code) as Hier2 " & _
                                  " on Hier1.Struct_Code=Hier2.Struct_Code and Hier1.Update_Version=(Hier2.Update_Version-1) "

       

        Dim qryFinalParent As String = ""
        Dim qryFinalParentSelect As String = ""
        'Dim intloop As Integer = Me.ddlReportType.Items.Count
        For intloop As Integer = Me.ddlReportType.Items.Count To 1 Step -1
            If intloop = Me.ddlReportType.Items.Count Then
                qryFinalParent += " left join (" & ParentQry & ") as Parent" & intloop & " on xx.Struct_Code=Parent" & intloop & ".Struct_Code " & _
                " and convert(date,xx.Document_Date,103)>=  Parent" & intloop & ".Updated_Date  " & _
                " and convert(date,xx.Document_Date,103)< coalesce(Parent" & intloop & ".Updated_Date2,current_timestamp)"
                qryFinalParentSelect += " ,Parent" & intloop & ".Struct_Desc as " & ddlReportType.Items(intloop - 1).Text & " "
            Else
                qryFinalParent += " left join (" & ParentQry & ") as Parent" & intloop & " on Parent" & (intloop + 1) & ".Parent_Struct_Code=Parent" & intloop & ".Struct_Code " & _
                " and convert(date,xx.Document_Date,103)>=  Parent" & intloop & ".Updated_Date  " & _
                " and convert(date,xx.Document_Date,103)< coalesce(Parent" & intloop & ".Updated_Date2,current_timestamp)"
                qryFinalParentSelect += " ,Parent" & intloop & ".Struct_Desc as " & ddlReportType.Items(intloop - 1).Text & " "
            End If
        Next
        Qry = "select xx.* " & qryFinalParentSelect & "  from (" & strMCCMaterial & ") as xx " & qryFinalParent

        '" left join (" & ParentQry & ") as Parent9 on Parent10.Parent_Struct_Code=Parent9.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent8 on Parent9.Parent_Struct_Code=Parent8.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent7 on Parent8.Parent_Struct_Code=Parent7.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent6 on Parent7.Parent_Struct_Code=Parent6.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent5 on Parent6.Parent_Struct_Code=Parent5.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent4 on Parent5.Parent_Struct_Code=Parent4.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent3 on Parent4.Parent_Struct_Code=Parent3.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent2 on Parent3.Parent_Struct_Code=Parent2.Struct_Code " & _
        '" left join (" & ParentQry & ") as Parent1 on Parent2.Parent_Struct_Code=Parent1.Struct_Code "
        Qry += " where  2=2 "
        If txtLevel1.arrValueMember IsNot Nothing AndAlso txtLevel1.arrValueMember.Count > 0 Then
            Qry += " and Parent1.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel1.arrValueMember) + ") "
            End If
        If txtLevel2.arrValueMember IsNot Nothing AndAlso txtLevel2.arrValueMember.Count > 0 Then
            Qry += " and Parent2.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel2.arrValueMember) + ") "
            End If
        If txtLevel3.arrValueMember IsNot Nothing AndAlso txtLevel3.arrValueMember.Count > 0 Then
            Qry += " and Parent3.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel3.arrValueMember) + ") "
            End If
        If txtLevel4.arrValueMember IsNot Nothing AndAlso txtLevel4.arrValueMember.Count > 0 Then
            Qry += " and Parent4.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel4.arrValueMember) + ") "
            End If
        If txtLevel5.arrValueMember IsNot Nothing AndAlso txtLevel5.arrValueMember.Count > 0 Then
            Qry += " and Parent5.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel5.arrValueMember) + ") "
            End If
        If txtLevel6.arrValueMember IsNot Nothing AndAlso txtLevel6.arrValueMember.Count > 0 Then
            Qry += " and Parent6.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel6.arrValueMember) + ") "
            End If
        If txtLevel7.arrValueMember IsNot Nothing AndAlso txtLevel7.arrValueMember.Count > 0 Then
            Qry += " and Parent7.Struct_Code in (" + clsCommon.GetMulcallString(txtLevel7.arrValueMember) + ") "
        End If
        If txtMultLevel8.arrValueMember IsNot Nothing AndAlso txtMultLevel8.arrValueMember.Count > 0 Then
            Qry += " and Parent8.Struct_Code in (" + clsCommon.GetMulcallString(txtMultLevel8.arrValueMember) + ") "
        End If
        If txtMultLevel9.arrValueMember IsNot Nothing AndAlso txtMultLevel9.arrValueMember.Count > 0 Then
            Qry += " and Parent9.Struct_Code in (" + clsCommon.GetMulcallString(txtMultLevel9.arrValueMember) + ") "
        End If


        Return Qry
    End Function
    Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function
    Sub SetGridFormationOFGV1()


        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False

        Next

        If ddlReportType.SelectedValue = "7" Then

           Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            Gv1.Columns("Level4").IsVisible = True
            Gv1.Columns("Level5").IsVisible = True
            Gv1.Columns("Level6").IsVisible = True
            Gv1.Columns("Level7").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next


        ElseIf ddlReportType.SelectedValue = "1" Then

            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
               
                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "2" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "3" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "4" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            Gv1.Columns("Level4").IsVisible = True

            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "5" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            Gv1.Columns("Level4").IsVisible = True
            Gv1.Columns("Level5").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "6" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            Gv1.Columns("Level4").IsVisible = True
            Gv1.Columns("Level5").IsVisible = True
            Gv1.Columns("Level6").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        
        ElseIf ddlReportType.SelectedValue = "8" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Amount").IsVisible = True
            Gv1.Columns("Level1").IsVisible = True
            Gv1.Columns("Level2").IsVisible = True
            Gv1.Columns("Level3").IsVisible = True
            Gv1.Columns("Level4").IsVisible = True
            Gv1.Columns("Level5").IsVisible = True
            Gv1.Columns("Level6").IsVisible = True
            Gv1.Columns("Level7").IsVisible = True
            Gv1.Columns("Level8").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "9" Then
            'Gv1.Columns("Total FAT KG").IsVisible = True
            'Gv1.Columns("Total SNF KG").IsVisible = True
            'Gv1.Columns("Total Amount").IsVisible = True
            'Gv1.Columns("Level1").IsVisible = True
            'Gv1.Columns("Level2").IsVisible = True
            'Gv1.Columns("Level3").IsVisible = True
            'Gv1.Columns("Level4").IsVisible = True
            'Gv1.Columns("Level5").IsVisible = True
            'Gv1.Columns("Level6").IsVisible = True
            'Gv1.Columns("Level7").IsVisible = True
            'Gv1.Columns("Level8").IsVisible = True
            'Gv1.Columns("Level9").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In Gv1.Columns
            If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

            ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item)
            End If
        Next

        'If ddlReportType.SelectedValue = "Document Detail" Then
        '    Dim item1 As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item1)
        '    Dim item2 As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item2)
        '    'Else
        '    '    Dim item3 As New GridViewSummaryItem("Total FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        '    '    summaryRowItem.Add(item3)
        '    '    Dim item4 As New GridViewSummaryItem("Total SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        '    '    summaryRowItem.Add(item4)

        'End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        'txtUOM.Enabled = False
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtUOM.Value = ""
        LoadTypes()
        ddlReportType.SelectedValue = "Total Sale"
        'LoadCategory()
        txtLevel2.arrValueMember = Nothing
        txtLevel1.arrValueMember = Nothing
        txtLevel3.arrValueMember = Nothing
        txtLevel4.arrValueMember = Nothing
        txtLevel6.arrValueMember = Nothing
        txtLevel5.arrValueMember = Nothing
        txtLevel7.arrValueMember = Nothing
        txtMultLevel8.arrValueMember = Nothing
        txtMultLevel9.arrValueMember = Nothing
        'rbtnCategoryAll.IsChecked = True

        ddlReportType.Enabled = True
        txtLevel2.Enabled = True
        txtLevel1.Enabled = True
        txtLevel3.Enabled = True
        txtLevel4.Enabled = True
        txtLevel6.Enabled = True
        txtLevel5.Enabled = True
        txtLevel7.Enabled = True
        txtMultLevel8.Enabled = True
        txtMultLevel9.Enabled = True
        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.Text
    End Sub
    Sub updateLevelFilter()
        Dim level1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=1"))
        If clsCommon.myLen(level1) > 0 Then
            lblLevel1.Text = level1
        End If
        Dim level2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=2"))
        If clsCommon.myLen(level2) > 0 Then
            lblLevel2.Text = level2
        End If
        Dim level3 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=3"))
        If clsCommon.myLen(level3) > 0 Then
            lblLevel3.Text = level3
        End If
        Dim level4 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=4"))
        If clsCommon.myLen(level4) > 0 Then
            lblLevel4.Text = level4
        End If
        Dim level5 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=5"))
        If clsCommon.myLen(level5) > 0 Then
            lblLevel5.Text = level5
        End If
        Dim level6 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=6"))
        If clsCommon.myLen(level6) > 0 Then
            lblLevel6.Text = level6
        End If
        Dim level7 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=7"))
        If clsCommon.myLen(level7) > 0 Then
            lblLevel7.Text = level7
        End If
        Dim level8 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=8"))
        If clsCommon.myLen(level8) > 0 Then
            lblLevel8.Text = level8
        End If
        Dim level9 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Level_Code from TSPL_Sales_Hierarchy_Levels where Seq_No=9"))
        If clsCommon.myLen(level9) > 0 Then
            lblLevel9.Text = level9
        End If


    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
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


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click

    End Sub

    'Private Sub rbtnCustomerAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    'End Sub

    'Private Sub rbtnItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgItem.Enabled = rbtnItemSelect.IsChecked
    'End Sub

    'Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("UOM : " + txtUOM.Value)

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdata(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1)) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub rmSend_Click(sender As Object, e As EventArgs) Handles rmSend.Click
        'Try
        '    Dim repotype As String = ""
        '    Dim invtype As String = ""
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
        '        Return
        '    End If

        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.RptFreshSaleRegister1)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Try

        '        Dim strEmail As String = ""


        '        If Process.GetProcessesByName("OutLook").Length < 1 Then
        '            'restarts the Process
        '            Process.Start("OutLook.exe")
        '        End If
        '        Dim oApp As New Outlook.Application()
        '        Dim oMsg As Outlook.MailItem

        '        'If chkAll.IsChecked Then
        '        '    invtype = ""
        '        'ElseIf chkTax.IsChecked Then
        '        '    invtype = "Tax Invoice"
        '        'ElseIf chkRetail.IsChecked Then
        '        '    invtype = "Retail Invoice"
        '        'End If

        '        If rdbDetail.IsChecked Then
        '            repotype = "Detail Report"
        '        Else
        '            repotype = "Summary Report"
        '        End If

        '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

        '        Try
        '            If strEmail.Substring(0, 1) = "," Then
        '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        If clsCommon.myLen(strEmail) <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
        '            Return
        '        End If

        '        oMsg.Body = obj.mailbody

        '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        oMsg.Subject = obj.mailsubjct

        '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If

        '        '------------------------code for attchament-------------------------------------
        '        If obj.atchmnt = "Y" Then
        '            Dim sDisplayname As [String] = "MyAttachment"
        '            If oMsg.Body Is Nothing Then
        '                oMsg.Body = " "
        '            End If
        '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
        '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

        '            Dim strRptPath As String = ""

        '            Dim oAttachment As Outlook.Attachment = Nothing
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '            If dt.Rows.Count > 0 Then
        '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

        '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

        '                If (IsExists = False) Then

        '                    System.IO.Directory.CreateDirectory(subPath)
        '                End If
        '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
        '                transportSql.exportdata(Gv1, strRptPath, "Sheet1")
        '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
        '            End If
        '        End If
        '        '---------------------------------------------------------------------------


        '        oMsg.Recipients.Add(strEmail)
        '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
        '        oMsg.Send()
        '        oMsg = Nothing
        '        oApp = Nothing

        '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try

        '    Try
        '        Dim client As New System.Net.WebClient()

        '        If clsCommon.myLen(obj.smsbody) <= 0 Then
        '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
        '        End If

        '        Dim strMes As String = ""

        '        strMes = obj.smsbody
        '        strMes = strMes.Replace("'", " ").Replace("`", "/")

        '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

        '        Try
        '            If strphone.Substring(0, 1) = "," Then
        '                strphone = strphone.Substring(1, strphone.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '        'Dim data As Stream = client.OpenRead(baseurl)
        '        'Dim reader As StreamReader = New StreamReader(data)
        '        'Dim s As String = reader.ReadToEnd()
        '        'data.Close()
        '        'reader.Close()


        '        Dim UserId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim Paswd As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SenderId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SMS_Provider As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))

        '        If clsCommon.CompairString(SMS_Provider, "Bulk SMS") = CompairStringResult.Equal Then
        '            '================send sms through PerfectBulkSMS====================
        '            Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '            Dim str As String = "http://www.perfectbulksms.in/Sendsmsapi.aspx?USERID=" + UserId + "&PASSWORD=" + Paswd + "&SENDERID=" + SenderId + "&TO=" & strphone & "&MESSAGE=" & strMes & ""
        '            Dim wrquest As HttpWebRequest = WebRequest.Create(str)
        '            Dim getresponse As HttpWebResponse = Nothing
        '            getresponse = wrquest.GetResponse()

        '            Dim objStream As Stream = getresponse.GetResponseStream()
        '            Dim objSR As StreamReader = New StreamReader(objStream, encode, True)
        '            Dim strResponse As String = objSR.ReadToEnd()
        '            'clsCommon.MyMessageBoxShow(getresponse.StatusDescription)

        '            objSR.Close()
        '            objStream.Close()
        '            getresponse.Close()
        '            '===========================================================
        '        ElseIf clsCommon.CompairString(SMS_Provider, "BSWS") = CompairStringResult.Equal Then
        '            Dim consumeWebService As BSWS.BSWS
        '            consumeWebService = New BSWS.BSWS
        '            Dim xmlResult As XmlElement
        '            xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
        '        End If

        '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
    '    If (Gv1.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    Print(Exporter.PDF)
    'End Sub

    Private Sub rptSalesHierarchyReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rptSalesHierarchyReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        updateLevelFilter()
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtUOM.Value = Unit_Code

            txtLevel1.arrValueMember = arrLevel1
            txtLevel2.arrValueMember = arrLevel2
            txtLevel3.arrValueMember = arrLevel3
            txtLevel4.arrValueMember = arrLevel4
            txtLevel5.arrValueMember = arrLevel5
            txtLevel6.arrValueMember = arrLevel6
            txtLevel7.arrValueMember = arrLevel7
            txtMultLevel8.arrValueMember = arrLevel8
            txtMultLevel9.arrValueMember = arrLevel9

            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

  

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub
    Sub DrillDown()
        '=======================Added by preeti gupta=============Ticket No[BM00000009831]
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "1") = CompairStringResult.Equal Then
                If Not arrBack.Contains("1") Then
                    arrBack.Add("1")
                End If
                ddlReportType.SelectedValue = 2
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level1").Value))
                txtLevel1.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "2") = CompairStringResult.Equal Then
                If Not arrBack.Contains("2") Then
                    arrBack.Add("2")
                End If
                ddlReportType.SelectedValue = 3
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level2").Value))
                txtLevel2.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "3") = CompairStringResult.Equal Then
                If Not arrBack.Contains("3") Then
                    arrBack.Add("3")
                End If
                ddlReportType.SelectedValue = 4
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level3").Value))
                txtLevel3.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "4") = CompairStringResult.Equal Then
                If Not arrBack.Contains("4") Then
                    arrBack.Add("4")
                End If
                ddlReportType.SelectedValue = 5
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level4").Value))
                txtLevel4.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "5") = CompairStringResult.Equal Then
                If Not arrBack.Contains("5") Then
                    arrBack.Add("5")
                End If
                ddlReportType.SelectedValue = 6
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level5").Value))
                txtLevel5.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "6") = CompairStringResult.Equal Then
                If Not arrBack.Contains("6") Then
                    arrBack.Add("6")
                End If
                ddlReportType.SelectedValue = 7
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level6").Value))
                txtLevel6.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "7") = CompairStringResult.Equal Then
                If Not arrBack.Contains("7") Then
                    arrBack.Add("7")
                End If
                ddlReportType.SelectedValue = 8
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level7").Value))
                txtLevel7.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "8") = CompairStringResult.Equal Then
                If Not arrBack.Contains("8") Then
                    arrBack.Add("8")
                End If
                ddlReportType.SelectedValue = 9
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Level8").Value))
                txtMultLevel8.arrValueMember = tmp
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "9") = CompairStringResult.Equal Then
                '    If Not arrBack.Contains("7") Then
                '        arrBack.Add("7")
                '    End If
                '    'ddlReportType.SelectedValue = "Document Detail"
                '    Document_No_Old = Document_No

                '    Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                '    Print(Exporter.Refresh)
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Fresh Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strTransCode)
                        Case "Product Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleInvoiceProductSale, strTransCode)
                        Case "Export Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strTransCode)
                        Case "MCC Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strTransCode)
                        Case "CSA Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                        Case "Fresh Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strTransCode)
                        Case "Product Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, strTransCode)
                        Case "Export Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strTransCode)
                        Case "CSA Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                        Case "MCC Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strTransCode)
                        Case "Bulk Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Scrap Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strTransCode)

                    End Select

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    
    
   

    Public Sub New()
        'Me.Visible = False
        ' This call is required by the designer.
        InitializeComponent()
        'Me.Visible = True
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    'Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtLevel3._My_Click
    '    Dim qry As String = " select Value as [Code],Description as Name from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
    '    txtLevel3.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Code", "Name", txtLevel3.arrValueMember, txtLevel3.arrDispalyMember)
    'End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "1") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "2") = CompairStringResult.Equal AndAlso arrBack.Contains("1") Then
                arrBack.Remove("1")
                ddlReportType.SelectedValue = 1
                txtLevel1.arrValueMember = arrLevel1
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "3") = CompairStringResult.Equal AndAlso arrBack.Contains("2") Then
                arrBack.Remove("2")
                ddlReportType.SelectedValue = 2
                txtLevel2.arrValueMember = arrLevel2
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "4") = CompairStringResult.Equal AndAlso arrBack.Contains("3") Then
                arrBack.Remove("3")
                ddlReportType.SelectedValue = 3
                txtLevel3.arrValueMember = arrLevel3
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "5") = CompairStringResult.Equal AndAlso arrBack.Contains("4") Then
                arrBack.Remove("4")
                ddlReportType.SelectedValue = 4
                txtLevel4.arrValueMember = arrLevel4
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "6") = CompairStringResult.Equal AndAlso arrBack.Contains("5") Then
                arrBack.Remove("5")
                ddlReportType.SelectedValue = 5
                txtLevel5.arrValueMember = arrLevel5
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "7") = CompairStringResult.Equal AndAlso arrBack.Contains("6") Then
                arrBack.Remove("6")
                ddlReportType.SelectedValue = 6
                txtLevel6.arrValueMember = arrLevel6
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "8") = CompairStringResult.Equal AndAlso arrBack.Contains("7") Then
                arrBack.Remove("7")
                ddlReportType.SelectedValue = 7
                txtLevel7.arrValueMember = arrLevel7
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "9") = CompairStringResult.Equal AndAlso arrBack.Contains("8") Then
                arrBack.Remove("8")
                ddlReportType.SelectedValue = 8
                txtMultLevel8.arrValueMember = arrLevel8
                Print(Exporter.Refresh)
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "10") = CompairStringResult.Equal AndAlso arrBack.Contains("6") Then
                '    arrBack.Remove("9")
                '    ddlReportType.SelectedValue = 9
                '    txtMultLevel9.arrValueMember = arrLevel9
                '    Print(Exporter.Refresh)
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
                '    arrBack.Remove("Document Wise")
                '    ddlReportType.SelectedValue = "Document Wise"
                '    Document_No = Document_No_Old
                '    'txtCustomer.arrValueMember = arrCustomer
                '    Print(Exporter.Refresh)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtLevel5._My_Click
    '    Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
    '    txtLevel5.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtLevel5.arrValueMember, txtLevel5.arrDispalyMember)
    'End Sub

    Private Sub txtLevel1_My_Click(sender As Object, e As EventArgs) Handles txtLevel1._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='1' "
        txtLevel1.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel1", qry, "Code", "Name", txtLevel1.arrValueMember, txtLevel1.arrDispalyMember)
    End Sub

    Private Sub txtLevel2_My_Click(sender As Object, e As EventArgs) Handles txtLevel2._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='2' "
        txtLevel2.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel2", qry, "Code", "Name", txtLevel2.arrValueMember, txtLevel2.arrDispalyMember)
    End Sub
    Private Sub txtLevel3_My_Click(sender As Object, e As EventArgs) Handles txtLevel3._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='3' "
        txtLevel3.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel3", qry, "Code", "Name", txtLevel3.arrValueMember, txtLevel3.arrDispalyMember)
    End Sub
    Private Sub txtLevel4_My_Click(sender As Object, e As EventArgs) Handles txtLevel4._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='4' "
        txtLevel4.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel4", qry, "Code", "Name", txtLevel4.arrValueMember, txtLevel4.arrDispalyMember)
    End Sub
    Private Sub txtLevel5_My_Click(sender As Object, e As EventArgs) Handles txtLevel5._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='5' "
        txtLevel5.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel5", qry, "Code", "Name", txtLevel5.arrValueMember, txtLevel5.arrDispalyMember)
    End Sub
    Private Sub txtLevel6_My_Click(sender As Object, e As EventArgs) Handles txtLevel6._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='6' "
        txtLevel6.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel6", qry, "Code", "Name", txtLevel6.arrValueMember, txtLevel6.arrDispalyMember)
    End Sub
    Private Sub txtLevel7_My_Click(sender As Object, e As EventArgs) Handles txtLevel7._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
            " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
            " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='7' "
        txtLevel7.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel7", qry, "Code", "Name", txtLevel7.arrValueMember, txtLevel7.arrDispalyMember)
    End Sub

   
    Private Sub txtMultLevel8__My_Click(sender As Object, e As EventArgs) Handles txtMultLevel8._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
           " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
           " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='8' "
        txtMultLevel8.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel8", qry, "Code", "Name", txtMultLevel8.arrValueMember, txtMultLevel8.arrDispalyMember)
    End Sub

    Private Sub txtMultLevel9__My_Click(sender As Object, e As EventArgs) Handles txtMultLevel9._My_Click
        Dim qry As String = " SELECT TSPL_Sales_Hierarchy_Structure.Struct_Code AS Code,TSPL_Sales_Hierarchy_Structure.Description as Name,Parent_Struct_Code as Parent " & _
           " FROM TSPL_Sales_Hierarchy_Structure INNER JOIN TSPL_Sales_Hierarchy_Levels ON TSPL_Sales_Hierarchy_Structure.Level_Code =TSPL_Sales_Hierarchy_Levels.Level_Code " & _
           " WHERE  TSPL_Sales_Hierarchy_Levels.Seq_No='9' "
        txtMultLevel9.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel9", qry, "Code", "Name", txtMultLevel9.arrValueMember, txtMultLevel9.arrDispalyMember)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("UOM : " + txtUOM.Value)

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdata(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1)) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
