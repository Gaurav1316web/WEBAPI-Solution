Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Data

Public Class RptSecurityMatrix
    Inherits FrmMainTranScreen

#Region "Variable"
    Const colRead As String = "colRead"
    Const colModify As String = "colModify"
    Const colPrint As String = "colPrint"
    Const colAuthrized As String = "colAuthrized"
    Const colCancel As String = "colCancel"
    Const colDelete As String = "colDelete"
    Const colExport As String = "colExport"
    Const colQExport As String = "colQExport"
    Const colCancelPosting As String = "colCancelPosting"
    Const colReverse As String = "colReverse"
#End Region


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            Dim qry As String = ""
            Dim dt As New DataTable()

            qry = " select TSPL_PROGRAM_MASTER.Program_Name,TSPL_USER_MASTER.User_Name,(TSPL_GROUP_PROGRAM_MAPPING.Program_Code) as [Screen Code],TSPL_PROGRAM_MASTER.Program_Name as [Screen Name],(TSPL_USER_MASTER.User_Code) as [User Code],TSPL_USER_MASTER.User_Name as [User Name],case when (TSPL_GROUP_PROGRAM_MAPPING.Read_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Read]"
            qry += " ,case when (TSPL_GROUP_PROGRAM_MAPPING.Modify_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Modify] "
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Cancel_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Cancel]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Print_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Print]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Delete_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Delete]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Export_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Export]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.QucikExport_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Quick Export]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Authorized]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Cancel_Flag_After_Posting)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Cancel After Posting]"
            qry += ",case when(TSPL_GROUP_PROGRAM_MAPPING.Reverse_Flag)=1 then CAST(1 as BIT ) else CAST(0 as BIT ) end as [Reverse]  "
            qry += "    from TSPL_GROUP_PROGRAM_MAPPING"
            qry += " left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_GROUP_PROGRAM_MAPPING.Program_Code"
            qry += " left outer join TSPL_USER_GROUP_MAPPING on TSPL_USER_GROUP_MAPPING.Group_Code=TSPL_GROUP_PROGRAM_MAPPING.Group_Code"
            qry += " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_USER_GROUP_MAPPING.User_Code where TSPL_USER_MASTER.User_Code is not null "

            If txtMultScreen.arrValueMember IsNot Nothing AndAlso txtMultScreen.arrValueMember.Count > 0 Then
                qry += " and TSPL_GROUP_PROGRAM_MAPPING.Program_Code in (" + clsCommon.GetMulcallString(txtMultScreen.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtMultUser.arrValueMember IsNot Nothing AndAlso txtMultUser.arrValueMember.Count > 0 Then
                qry += " and TSPL_USER_MASTER.User_Code in (" + clsCommon.GetMulcallString(txtMultUser.arrValueMember) + ") " + Environment.NewLine
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Gv1.DataSource = Nothing

                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt

                Gv1.GroupDescriptors.Clear()

                If rbtnScreenWise.IsChecked Then
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("Program_Name as ScreenName format ""{0}: {1}"" Group By Program_Name"))
                Else
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("User_Name as UserName format ""{0}: {1}"" Group By User_Name"))
                End If

                Gv1.Columns("Program_Name").IsVisible = False
                Gv1.Columns("Screen Code").IsVisible = False
                Gv1.Columns("User Code").IsVisible = False
                Gv1.Columns("User_Name").IsVisible = False
                Gv1.ReadOnly = True
                Gv1.BestFitColumns()
                Gv1.AutoExpandGroups = True
                Gv1.ShowGroupPanel = False
                Gv1.ShowRowHeaderColumn = False

                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub Reset()
        rbtnUserWise.IsChecked = True
        txtMultUser.arrValueMember = Nothing
        txtMultScreen.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

        Gv1.Columns.Clear()
        UserScreenWiseEnable()
    End Sub

    Private Sub RptSecurityMatrix_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtMultUser__My_Click(sender As Object, e As EventArgs) Handles txtMultUser._My_Click
        Try
            Dim qry As String = "select User_Code as Code,User_Name as Name from TSPL_USER_MASTER"
            txtMultUser.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "Code", "Name", txtMultUser.arrValueMember, txtMultUser.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMultScreen__My_Click(sender As Object, e As EventArgs) Handles txtMultScreen._My_Click
        Try
            Dim qry As String = "select program_code as [Code],program_name as [Screen Name] from TSPL_PROGRAM_MASTER"
            txtMultScreen.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "Code", "Screen Name", txtMultScreen.arrValueMember, txtMultScreen.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub UserScreenWiseEnable()
        If rbtnScreenWise.IsChecked Then
            txtMultScreen.Enabled = True
            txtMultUser.Enabled = False
            txtMultUser.arrValueMember = Nothing
        Else
            txtMultScreen.Enabled = False
            txtMultUser.Enabled = True
            txtMultScreen.arrValueMember = Nothing
        End If
    End Sub

    Private Sub rbtnUserWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnUserWise.ToggleStateChanged
        UserScreenWiseEnable()
    End Sub

    Private Sub rbtnScreenWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnScreenWise.ToggleStateChanged
        UserScreenWiseEnable()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim qry As String = ""
            Dim dt As New DataTable()

            qry = " select TSPL_PROGRAM_MASTER.Program_Name,TSPL_USER_MASTER.User_Name,(TSPL_GROUP_PROGRAM_MAPPING.Program_Code) as [Screen Code],TSPL_PROGRAM_MASTER.Program_Name as [Screen Name],(TSPL_USER_MASTER.User_Code) as [User Code],TSPL_USER_MASTER.User_Name as [User Name],TSPL_GROUP_PROGRAM_MAPPING.Read_Flag as [Read]"
            qry += " ,TSPL_GROUP_PROGRAM_MAPPING.Modify_Flag as [Modify] "
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Cancel_Flag as [Cancel]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Print_Flag as [Print]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Delete_Flag as [Delete]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Export_Flag as [Export]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.QucikExport_Flag as [Quick Export]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag end as [Authorized]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Cancel_Flag_After_Posting as [Cancel After Posting]"
            qry += ",TSPL_GROUP_PROGRAM_MAPPING.Reverse_Flag as [Reverse]  "
            qry += "    from TSPL_GROUP_PROGRAM_MAPPING"
            qry += " left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_GROUP_PROGRAM_MAPPING.Program_Code"
            qry += " left outer join TSPL_USER_GROUP_MAPPING on TSPL_USER_GROUP_MAPPING.Group_Code=TSPL_GROUP_PROGRAM_MAPPING.Group_Code"
            qry += " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_USER_GROUP_MAPPING.User_Code where TSPL_USER_MASTER.User_Code is not null "

            If txtMultScreen.arrValueMember IsNot Nothing AndAlso txtMultScreen.arrValueMember.Count > 0 Then
                qry += " and TSPL_GROUP_PROGRAM_MAPPING.Program_Code in (" + clsCommon.GetMulcallString(txtMultScreen.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtMultUser.arrValueMember IsNot Nothing AndAlso txtMultUser.arrValueMember.Count > 0 Then
                qry += " and TSPL_USER_MASTER.User_Code in (" + clsCommon.GetMulcallString(txtMultUser.arrValueMember) + ") " + Environment.NewLine
            End If

            transportSql.BulkExport("RptSecurityMatrix", qry, "", "xls", "")
           
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class