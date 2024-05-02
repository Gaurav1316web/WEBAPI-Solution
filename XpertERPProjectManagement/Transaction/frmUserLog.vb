'--Created by--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001538]
Imports common
Imports System.Data.SqlClient

Public Class FrmUserLog
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim dtTemp As DataTable
    Dim qry As String
    Dim IsSelectAll As Boolean = False
    Private Sub FrmUserLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gv.AllowEditRow = True
            FillData()
            btnSelect.Text = "Select All"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Reset()
        Try
            btnSelect.Text = "Select All"
            FillData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FillData()
        Try
            qry = "Select CAST(0 as Bit) as 'Select', PROJECT_CODE as ProjectCode, SPECIFICATION as ProjDesc, PROJECT_STATUS, TSPL_CUSTOMER_MASTER.Customer_Name as Customer, U1.User_Name as Created_By, "
            qry += " TSPL_PJC_PROJECT.Created_Date as Created_Date, U2.User_Name As Modified_By, Modified_Date, Project_Manager, Sale_Order_No,Project_Type, "
            qry += " Project_Type_Value, Account_Method, U3.User_Name AS Approve_By, Approve_Date, Approve_Date_Actual, U4.User_Name As On_Hold_By, "
            qry += " On_Hold_Date,U5.User_Name as Open_By, Open_Date, Open_Date_Actual, U6.User_Name As Close_By, Close_Date, U7.User_Name As Completed_By, Completed_Date, Completed_Date_Actual, Inactive_By, Inactive_Date, Comment"
            qry += " From TSPL_PJC_PROJECT"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U1 On U1.User_Code=TSPL_PJC_PROJECT.Created_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U2 On U2.User_Code=TSPL_PJC_PROJECT.Modified_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U3 On U3.User_Code=TSPL_PJC_PROJECT.Approve_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U4 On U4.User_Code=TSPL_PJC_PROJECT.On_Hold_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U5 On U5.User_Code=TSPL_PJC_PROJECT.Open_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U6 On U6.User_Code=TSPL_PJC_PROJECT.Close_By"
            qry += " LEFT OUTER JOIN TSPL_USER_MASTER U7 On U7.User_Code=TSPL_PJC_PROJECT.Completed_By"

            qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_PJC_PROJECT.Cust_Code"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                gv.DataSource = dt
                btnSelect.Enabled = True
                btnExport.Enabled = True
                FormatGv()
            Else
                btnSelect.Enabled = False
                btnExport.Enabled = False
                Throw New Exception("No Data Found.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGv()
        Try
            For Each col As GridViewColumn In gv.Columns
                If Not col Is gv.Columns("Select") Then
                    col.ReadOnly = True
                End If
            Next

            gv.Columns("ProjectCode").HeaderText = "Project Code"
            gv.Columns("ProjectCode").Width = 100
            gv.Columns("ProjDesc").HeaderText = "Project Description"
            gv.Columns("ProjDesc").Width = 250

            gv.Columns("PROJECT_STATUS").HeaderText = "Status"
            gv.Columns("PROJECT_STATUS").Width = 70

            gv.Columns("Customer").HeaderText = "Customer"
            gv.Columns("Customer").Width = 100

            gv.Columns("Created_By").HeaderText = "Created By"
            gv.Columns("Created_By").Width = 100
            gv.Columns("Created_Date").HeaderText = "Created Date"
            gv.Columns("Created_Date").Width = 140

            gv.Columns("Modified_By").HeaderText = "Modified By"
            gv.Columns("Modified_By").Width = 100
            gv.Columns("Modified_Date").HeaderText = "Modified Date"
            gv.Columns("Modified_Date").Width = 140

            gv.Columns("Project_Manager").HeaderText = "Project Manager"
            gv.Columns("Project_Manager").Width = 100

            gv.Columns("Sale_Order_No").HeaderText = "Sale Order No"
            gv.Columns("Sale_Order_No").Width = 100

            gv.Columns("Project_Type").HeaderText = "Project Type"
            gv.Columns("Project_Type").Width = 70

            gv.Columns("Project_Type_Value").HeaderText = "Project Value"
            gv.Columns("Project_Type_Value").Width = 70

            gv.Columns("Account_Method").HeaderText = "Account Method"
            gv.Columns("Account_Method").Width = 70

            gv.Columns("Approve_By").HeaderText = "Approved By"
            gv.Columns("Approve_By").Width = 100
            gv.Columns("Approve_Date").HeaderText = "Approved Date"
            gv.Columns("Approve_Date").Width = 140
            gv.Columns("Approve_Date_Actual").HeaderText = "Approved Actual Date"
            gv.Columns("Approve_Date_Actual").Width = 140

            gv.Columns("On_Hold_By").HeaderText = "Hold By"
            gv.Columns("On_Hold_By").Width = 100
            gv.Columns("On_Hold_Date").HeaderText = "Hold Date"
            gv.Columns("On_Hold_Date").Width = 140

            gv.Columns("Open_By").HeaderText = "Opened By"
            gv.Columns("Open_By").Width = 100
            gv.Columns("Open_Date").HeaderText = "Opened Date"
            gv.Columns("Open_Date").Width = 140
            gv.Columns("Open_Date_Actual").HeaderText = "Opened Date Actual"
            gv.Columns("Open_Date_Actual").Width = 140

            gv.Columns("Close_By").HeaderText = "Closed By"
            gv.Columns("Close_By").Width = 100
            gv.Columns("Close_Date").HeaderText = "Closed Date"
            gv.Columns("Close_Date").Width = 140

            gv.Columns("Completed_By").HeaderText = "Completed By"
            gv.Columns("Completed_By").Width = 100
            gv.Columns("Completed_Date").HeaderText = "Completed Date"
            gv.Columns("Completed_Date").Width = 140
            gv.Columns("Completed_Date_Actual").HeaderText = "Completed Date Actual"
            gv.Columns("Completed_Date_Actual").Width = 140

            gv.Columns("Inactive_By").HeaderText = "Inactive By"
            gv.Columns("Inactive_By").Width = 100
            gv.Columns("Inactive_Date").HeaderText = "Inactive Date"
            gv.Columns("Inactive_Date").Width = 140

            gv.Columns("Comment").Width = 200
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatgvTemp()
        Try
            gvTemp.Columns("ProjectCode").HeaderText = "Project Code"
            gvTemp.Columns("ProjectCode").Width = 100
            gvTemp.Columns("ProjDesc").HeaderText = "Project Description"
            gvTemp.Columns("ProjDesc").Width = 250

            gvTemp.Columns("PROJECT_STATUS").HeaderText = "Status"
            gvTemp.Columns("PROJECT_STATUS").Width = 70

            gvTemp.Columns("Customer").HeaderText = "Created By"
            gvTemp.Columns("Customer").Width = 150

            gvTemp.Columns("Created_By").HeaderText = "Created By"
            gvTemp.Columns("Created_By").Width = 100
            gvTemp.Columns("Created_Date").HeaderText = "Created Date"
            gvTemp.Columns("Created_Date").Width = 140

            gvTemp.Columns("Modified_By").HeaderText = "Modified By"
            gvTemp.Columns("Modified_By").Width = 100
            gvTemp.Columns("Modified_Date").HeaderText = "Modified Date"
            gvTemp.Columns("Modified_Date").Width = 140

            gvTemp.Columns("Project_Manager").HeaderText = "Project Manager"
            gvTemp.Columns("Project_Manager").Width = 100

            gvTemp.Columns("Sale_Order_No").HeaderText = "Sale Order No"
            gvTemp.Columns("Sale_Order_No").Width = 100

            gvTemp.Columns("Project_Type").HeaderText = "Project Type"
            gvTemp.Columns("Project_Type").Width = 70

            gvTemp.Columns("Project_Type_Value").HeaderText = "Project Value"
            gvTemp.Columns("Project_Type_Value").Width = 70

            gvTemp.Columns("Account_Method").HeaderText = "Account Method"
            gvTemp.Columns("Account_Method").Width = 70

            gvTemp.Columns("Approve_By").HeaderText = "Approved By"
            gvTemp.Columns("Approve_By").Width = 100
            gvTemp.Columns("Approve_Date").HeaderText = "Approved Date"
            gvTemp.Columns("Approve_Date").Width = 140
            gvTemp.Columns("Approve_Date_Actual").HeaderText = "Approved Actual Date"
            gvTemp.Columns("Approve_Date_Actual").Width = 140

            gvTemp.Columns("On_Hold_By").HeaderText = "Hold By"
            gvTemp.Columns("On_Hold_By").Width = 100
            gvTemp.Columns("On_Hold_Date").HeaderText = "Hold Date"
            gvTemp.Columns("On_Hold_Date").Width = 140

            gvTemp.Columns("Open_By").HeaderText = "Opened By"
            gvTemp.Columns("Open_By").Width = 100
            gvTemp.Columns("Open_Date").HeaderText = "Opened Date"
            gvTemp.Columns("Open_Date").Width = 140
            gvTemp.Columns("Open_Date_Actual").HeaderText = "Opened Date Actual"
            gvTemp.Columns("Open_Date_Actual").Width = 140

            gvTemp.Columns("Close_By").HeaderText = "Closed By"
            gvTemp.Columns("Close_By").Width = 100
            gvTemp.Columns("Close_Date").HeaderText = "Closed Date"
            gvTemp.Columns("Close_Date").Width = 140

            gvTemp.Columns("Completed_By").HeaderText = "Completed By"
            gvTemp.Columns("Completed_By").Width = 100
            gvTemp.Columns("Completed_Date").HeaderText = "Completed Date"
            gvTemp.Columns("Completed_Date").Width = 140
            gvTemp.Columns("Completed_Date_Actual").HeaderText = "Completed Date Actual"
            gvTemp.Columns("Completed_Date_Actual").Width = 100

            gvTemp.Columns("Inactive_By").HeaderText = "Inactive By"
            gvTemp.Columns("Inactive_By").Width = 100
            gvTemp.Columns("Inactive_Date").HeaderText = "Inactive Date"
            gvTemp.Columns("Inactive_Date").Width = 140

            gvTemp.Columns("Comment").Width = 100
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
                For Each grow As GridViewRowInfo In gv.Rows
                    grow.Cells("Select").Value = True
                Next
                btnSelect.Text = "UnSelect All"
            Else
                For Each grow As GridViewRowInfo In gv.Rows
                    grow.Cells("Select").Value = False
                Next
                btnSelect.Text = "Select All"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Reset()
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            FillTempGrid()
            clsCommon.MyExportToExcelGrid("User Log", gvTemp, Nothing, "UserLog", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FillTempGrid()
        gvTemp.DataSource = Nothing
        dtTemp = dt.Clone()
        Try
            Dim dr As DataRow
            For Each grow As GridViewRowInfo In gv.Rows
                If grow.Cells("Select").Value = True Then
                    dr = dtTemp.NewRow()
                    dr("ProjectCode") = clsCommon.myCstr(grow.Cells("ProjectCode").Value)
                    dr("ProjDesc") = clsCommon.myCstr(grow.Cells("ProjDesc").Value)
                    dr("PROJECT_STATUS") = clsCommon.myCstr(grow.Cells("PROJECT_STATUS").Value)
                    dr("Customer") = clsCommon.myCstr(grow.Cells("Customer").Value)
                    dr("Created_By") = clsCommon.myCstr(grow.Cells("Created_By").Value)
                    dr("Created_Date") = clsCommon.myCstr(grow.Cells("Created_Date").Value)
                    dr("Modified_By") = clsCommon.myCstr(grow.Cells("Modified_By").Value)
                    dr("Modified_Date") = clsCommon.myCstr(grow.Cells("Modified_Date").Value)
                    dr("Project_Manager") = clsCommon.myCstr(grow.Cells("Project_Manager").Value)
                    dr("Sale_Order_No") = clsCommon.myCstr(grow.Cells("Sale_Order_No").Value)
                    dr("Project_Type") = clsCommon.myCstr(grow.Cells("Project_Type").Value)
                    dr("Project_Type_Value") = clsCommon.myCdbl(grow.Cells("Project_Type_Value").Value)
                    dr("Account_Method") = clsCommon.myCstr(grow.Cells("Account_Method").Value)
                    dr("Approve_By") = clsCommon.myCstr(grow.Cells("Approve_By").Value)
                    If clsCommon.myLen(grow.Cells("Approve_Date_Actual").Value) > 0 Then
                        dr("Approve_Date") = clsCommon.myCstr(grow.Cells("Approve_Date").Value)
                    End If

                    If clsCommon.myLen(grow.Cells("Approve_Date_Actual").Value) > 0 Then
                        dr("Approve_Date_Actual") = clsCommon.myCstr(grow.Cells("Approve_Date_Actual").Value)
                    End If
                    dr("On_Hold_By") = clsCommon.myCstr(grow.Cells("On_Hold_By").Value)
                    If clsCommon.myLen(grow.Cells("On_Hold_Date").Value) > 0 Then
                        dr("On_Hold_Date") = clsCommon.myCstr(grow.Cells("On_Hold_Date").Value)
                    End If
                    dr("Open_By") = clsCommon.myCstr(grow.Cells("Open_By").Value)
                    If clsCommon.myLen(grow.Cells("Open_Date").Value) > 0 Then
                        dr("Open_Date") = clsCommon.myCstr(grow.Cells("Open_Date").Value)
                    End If
                    If clsCommon.myLen(grow.Cells("Open_Date_Actual").Value) > 0 Then
                        dr("Open_Date_Actual") = clsCommon.myCstr(grow.Cells("Open_Date_Actual").Value)
                    End If
                    dr("Close_By") = clsCommon.myCstr(grow.Cells("Close_By").Value)
                    If clsCommon.myLen(grow.Cells("Close_Date").Value) > 0 Then
                        dr("Close_Date") = clsCommon.myCstr(grow.Cells("Close_Date").Value)
                    End If
                    dr("Completed_By") = clsCommon.myCstr(grow.Cells("Completed_By").Value)
                    If clsCommon.myLen(grow.Cells("Completed_Date").Value) > 0 Then
                        dr("Completed_Date") = clsCommon.myCstr(grow.Cells("Completed_Date").Value)
                    End If
                    If clsCommon.myLen(grow.Cells("Completed_Date_Actual").Value) > 0 Then
                        dr("Completed_Date_Actual") = clsCommon.myCstr(grow.Cells("Completed_Date_Actual").Value)
                    End If
                    dr("Inactive_By") = clsCommon.myCstr(grow.Cells("Inactive_By").Value)
                    If clsCommon.myLen(grow.Cells("Inactive_Date").Value) > 0 Then
                        dr("Inactive_Date") = clsCommon.myCstr(grow.Cells("Inactive_Date").Value)
                    End If
                    dr("Comment") = clsCommon.myCstr(grow.Cells("Comment").Value)
                    dtTemp.Rows.Add(dr)
                End If
            Next
            If dtTemp.Rows.Count <= 0 Then
                Throw New Exception("Please select atleast single Project.")
            Else
                dtTemp.Columns.Remove("Select")
                gvTemp.DataSource = dtTemp
                FormatgvTemp()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
