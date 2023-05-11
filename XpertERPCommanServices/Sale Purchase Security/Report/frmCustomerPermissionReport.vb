Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class frmCustomerPermissionReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try

            Dim qry As String = Nothing
            qry = "  select TSPL_CUSTOMER_MAPPING.user_code as [User Code],tspl_user_master.User_Name AS [User Name] " & _
                     ",TSPL_CUSTOMER_MAPPING.Cust_Group_Code as [Customer Group Code],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Customer Group Name] " & _
                     ",TSPL_CUSTOMER_MAPPING.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] " & _
                     " from TSPL_CUSTOMER_MAPPING " & _
                     " left outer join tspl_user_master on tspl_user_master.user_code=TSPL_CUSTOMER_MAPPING.user_code " & _
                     " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MAPPING.Cust_Group_Code " & _
                     " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_MAPPING.Cust_Code "
            qry += " where 2=2 "

            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_MAPPING.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")  "
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_MAPPING.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
            End If

            If txtUser.arrValueMember IsNot Nothing AndAlso txtUser.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_MAPPING.User_Code in (" + clsCommon.GetMulcallString(txtUser.arrValueMember) + ")  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True

                gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()

            gv1.BestFitColumns()

            ReStoreGridLayout()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub Reset()
        txtCustGroup.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtUser.arrValueMember = Nothing
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()

    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Customer Permission Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                If txtUser.arrValueMember IsNot Nothing AndAlso txtUser.arrValueMember.Count > 0 Then
                    arrHeader.Add("User : " + clsCommon.GetMulcallString(txtUser.arrValueMember))
                End If

                If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer Group : " + clsCommon.GetMulcallString(txtCustGroup.arrValueMember))
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallString(txtCustomer.arrValueMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Customer Permission Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


                If txtUser.arrValueMember IsNot Nothing AndAlso txtUser.arrValueMember.Count > 0 Then
                    arrHeader.Add("User : " + clsCommon.GetMulcallString(txtUser.arrValueMember))
                End If

                If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer Group : " + clsCommon.GetMulcallString(txtCustGroup.arrValueMember))
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallString(txtCustomer.arrValueMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer Permission Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Try
            Dim qry As String = "select Cust_Group_Code as [Code],Cust_Group_Desc as [Name] from TSPL_CUSTOMER_GROUP_MASTER"
            txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@CustGroup", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as [Code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@Customer", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtUser__My_Click(sender As Object, e As EventArgs) Handles txtUser._My_Click
        Try
            Dim qry As String = "select User_Code as [Code],User_Name as [Name] from tspl_user_master"
            txtUser.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@User", qry, "Code", "Name", txtUser.arrValueMember, txtUser.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
