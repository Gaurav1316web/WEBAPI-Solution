'--------Created By Richa 21/07/2014 Against Ticket No BM00000003236

Imports common
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
'Check in sanjay 22/06/2020
Public Class FrmCustomerPermission
    Inherits FrmMainTranScreen
#Region "Variables"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Const colSelect As String = "Select"

    Dim IsLoadData As Boolean = False
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmCustomerPermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
        GrpCustomerGroup.Enabled = Not chkCustomerGroupAll.IsChecked
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerPermission)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub CustomerGroupCode()
        Try
            Dim dt As DataTable
            Dim i As Integer = 0

            Dim qry As String = "Select CAST(0 as BIT) as [Select],Cust_Group_Code as [Cust Group Code] ,Cust_Group_Desc as [Cust Group Desc] from TSPL_CUSTOMER_GROUP_MASTER "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.Columns("Select").Width = 40
                gv.Columns("Cust Group Code").Width = 100
                gv.Columns("Cust Group Desc").Width = 140
                gv.Columns("Cust Group Code").ReadOnly = True
                gv.Columns("Cust Group Desc").ReadOnly = True
                gv.Columns("Select").ReadOnly = False
                gv.AllowEditRow = True
                GridviewdataCustomerCode()
            Else
                gv.DataSource = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
  
  
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkCustomerGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerGroupAll.ToggleStateChanged
        Try
            IsLoadData = True
            For Each row As GridViewRowInfo In gv.Rows
                row.Cells(0).Value = chkCustomerGroupAll.IsChecked
            Next
        Catch ex As Exception
        Finally
            IsLoadData = False
            GridviewdataCustomerCode()
        End Try

    End Sub

    Private Sub FrmCustomerPermission_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub
    Sub closeForm()
        Me.Close()
    End Sub


    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not IsLoadData Then
            If e.Column Is gv.Columns(0) Then
                GridviewdataCustomerCode()
            End If
        End If
    End Sub
    Public Sub GridviewdataCustomerCode()
        Try
            Dim StrCustomerGroupCode As String = ""
            '  Dim dt As DataTable
            Dim i As Integer = 0
            For ii As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(ii).Cells(0).Value = True Then
                    StrCustomerGroupCode = StrCustomerGroupCode & "'" & clsCommon.myCstr(gv.Rows(ii).Cells(1).Value).Replace("'", "''").ToString() & "'" & ","
                End If
            Next

            If StrCustomerGroupCode <> "" Then
                StrCustomerGroupCode = StrCustomerGroupCode.Substring(0, StrCustomerGroupCode.Length - 1)
                Dim qry As String

                qry = "Select CAST(Case When ISNULL(XXX.Cust_Code,'')='' Then 0 Else 1 End as BIT) as [Select], TSPL_CUSTOMER_MASTER.Cust_Code as [Cust Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name]," & _
            " TSPL_CUSTOMER_MASTER.Cust_Group_Code as [Cust Group Code],TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Name]  from (Select Cust_Code  from TSPL_CUSTOMER_MAPPING WHERE User_Code='" + fndUser_Name.Value + "')" & _
            " XXX RIGHT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.Cust_Code =TSPL_CUSTOMER_MASTER.Cust_Code" & _
            " left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code" & _
            " WHERE TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + StrCustomerGroupCode + ")"

                GvCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
                GvCustomer.Columns("Select").Width = 50
                GvCustomer.Columns("Cust Code").Width = 90
                GvCustomer.Columns("Customer Name").Width = 150
                GvCustomer.Columns("Cust Group Code").Width = 100
                GvCustomer.Columns("Cust Code").ReadOnly = True
                GvCustomer.Columns("Customer Name").ReadOnly = True
                GvCustomer.Columns("Cust Group Code").ReadOnly = True
                GvCustomer.Columns("Select").ReadOnly = False
                GvCustomer.AllowEditRow = True
                GvCustomer.BestFitColumns()
            Else
                GvCustomer.DataSource = Nothing

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try



    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try

       
            Dim Flag As Integer = 0
            Dim Flag1 As Integer = 0
            If (String.IsNullOrEmpty(fndUser_Name.Value)) Then
                Throw New Exception("Please Select User")
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(ii).Cells(0).Value = True Then
                    Flag = Flag + 1
                End If
            Next

            If Flag <= 0 Then
                Throw New Exception("Please Select Atleast one Customer Group")
            End If
            For ii As Integer = 0 To GvCustomer.Rows.Count - 1
                If GvCustomer.Rows(ii).Cells(0).Value = True Then
                    Flag1 = Flag1 + 1
                End If
            Next

            If Flag1 <= 0 Then
                Throw New Exception("Please Select Atleast one Customer")
            End If
            Dim arrCustomer As New List(Of ClsCustomerPermission)
            Dim obj As ClsCustomerPermission = Nothing
            'Dim CustomerGrpCode As New List(Of String)
            Dim UserCode As String
            Dim i As Integer = 0
            Dim j As Integer = 0
            UserCode = clsCommon.myCstr(fndUser_Name.Value)
            For Each row As GridViewRowInfo In GvCustomer.Rows
                If row.Cells(0).Value = True Then
                    obj = New ClsCustomerPermission()
                    obj.Cust_Code = clsCommon.myCstr(row.Cells(1).Value)
                    obj.Cust_Group_Code = clsCommon.myCstr(row.Cells(3).Value)
                    obj.User_Code = clsCommon.myCstr(fndUser_Name.Value)
                    arrCustomer.Add(obj)
                End If
            Next
           
            If arrCustomer.Count > 0 Then
                If obj.SaveData(UserCode, arrCustomer) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ChkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkCustomerAll.ToggleStateChanged
        If ChkCustomerAll.IsChecked Then
            For ii As Integer = 0 To GvCustomer.Rows.Count - 1
                GvCustomer.Rows(ii).Cells(0).Value = True
            Next
        Else
            For ii As Integer = 0 To GvCustomer.Rows.Count - 1
                GvCustomer.Rows(ii).Cells(0).Value = False
            Next            
        End If
    End Sub

    


    Private Sub fndUser_Name__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUser_Name._MYValidating
        Dim str As String = "select count(*) from TSPL_USER_MASTER where User_Code ='" + fndUser_Name.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndUser_Name.MyReadOnly = False
        Else
            fndUser_Name.MyReadOnly = True
        End If

        If fndUser_Name.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select User_Code as [UserCode],User_Name as [User Name] from TSPL_USER_MASTER "
            fndUser_Name.Value = clsCommon.ShowSelectForm("fmUser_Name", qry, "UserCode", "", fndUser_Name.Value, "", isButtonClicked)
            TxtUserName.Text = clsDBFuncationality.getSingleValue("select User_Name from TSPL_USER_MASTER where User_Code= '" + fndUser_Name.Value + "'")
            textchangedsub()
            fndUser_NameLeave()
        End If
    End Sub

    Private Sub fndUser_Name__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndUser_Name._MYNavigator
        Dim qst As String = "select User_Code as [User Code],User_Name as [User Name] from TSPL_USER_MASTER   where  2=2"
        Select Case NavigatorType
            Case NavigatorType.Current
               
            Case NavigatorType.Next
                qst += "and User_Code in (select min(User_Code) from TSPL_USER_MASTER where User_Code>'" + fndUser_Name.Value + "' ) "

            Case NavigatorType.First
                qst += "and User_Code in (select MIN(User_Code) from TSPL_USER_MASTER )"

            Case NavigatorType.Last
                qst += "and User_Code in (select Max(User_Code) from TSPL_USER_MASTER  )"
            Case NavigatorType.Previous
                qst += "and User_Code in (select max(User_Code) from TSPL_USER_MASTER where User_Code<'" + fndUser_Name.Value + "'  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndUser_Name.Value = clsCommon.myCstr(dt.Rows(0)("User Code"))
            TxtUserName.Text = clsCommon.myCstr(dt.Rows(0)("User Name"))
        End If    
        textchangedsub()
        fndUser_NameLeave()
    End Sub
    Sub fndUser_NameLeave()
        If fndUser_Name.Value = "" Then
        Else
            Dim strUser_Code As String = "select User_Code,User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "'"
            Dim strvalue As String
            strvalue = clsDBFuncationality.getSingleValue(strUser_Code)

          
            If strvalue <> "" Then
            Else : strUser_Code = ""
                common.clsCommon.MyMessageBoxShow(Me, "User Code does not exist Master Table", Me.Text)
                fndUser_Name.Value = ""
                TxtUserName.Text = ""
                fndUser_Name.Focus()
            End If
        End If
    End Sub
    Sub textchangedsub()
        Dim strname As String = "select User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "'"
        Dim strvalue1 As String
        strvalue1 = clsDBFuncationality.getSingleValue(strname)

        Dim strUser_Code As String = "select User_Code from TSPL_CUSTOMER_MAPPING where User_Code='" + fndUser_Name.Value + "'"
        Dim strvalue As String
        strvalue = clsDBFuncationality.getSingleValue(strUser_Code)


        If (strvalue <> "") Then
            funfill()
            btnsave.Text = "&Update"
            rbtnDelete.Enabled = True

        Else
            CustomerGroupCode()
            btnsave.Text = "&Save"
            TxtUserName.Text = " "
        End If
        TxtUserName.Text = strvalue1
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()

        Dim dt As DataTable
        Dim strQuery As String
        strQuery = " Select distinct CAST(Case When ISNULL(XXX.Cust_Group_Code ,'')='' Then 0 Else 1 End as BIT) as [Select], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as [Cust Group Code] , TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc]  from (Select Cust_Group_Code from TSPL_CUSTOMER_MAPPING WHERE User_Code='" + fndUser_Name.Value + "')" & _
         " XXX RIGHT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON XXX.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code "

        dt = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            gv.DataSource = clsDBFuncationality.GetDataTable(strQuery)
            gv.Columns("Select").Width = 40
            gv.Columns("Cust Group Code").Width = 100
            gv.Columns("Cust Group Desc").Width = 140
            gv.Columns("Cust Group Code").ReadOnly = True
            gv.Columns("Cust Group Desc").ReadOnly = True

            gv.Columns("Select").ReadOnly = False

            gv.AllowEditRow = True
            GridviewdataCustomerCode()
        Else
            gv.DataSource = Nothing
        End If


    End Sub


    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        funReset()
    End Sub
    Private Sub funReset()
        Try
            fndUser_Name.MyReadOnly = False
            fndUser_Name.Value = ""
            rbtnDelete.Enabled = False
            btnsave.Text = "&Save"
            TxtUserName.Text = ""
            ChkCustomerSelect.IsChecked = True
            chkCustomerGroupSelect.IsChecked = True
            CustomerGroupCode()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsCustomerPermission.DeleteData(fndUser_Name.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        Try
            Dim str As String
            str = " select TSPL_CUSTOMER_MAPPING.user_code as [User Code],TSPL_USER_MASTER.User_Name as [User Name],TSPL_CUSTOMER_MAPPING.Cust_Group_Code as [Customer Group Code],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Customer Group Name],TSPL_CUSTOMER_MAPPING.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] " & _
                " from TSPL_CUSTOMER_MAPPING left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_CUSTOMER_MAPPING.User_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MAPPING.Cust_Group_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code = TSPL_CUSTOMER_MAPPING.cust_code "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "User Code", "User Name", "Customer Group Code", "Customer Group Name", "Customer Code", "Customer Name") Then
            Dim trans As SqlTransaction = Nothing

            Dim linno As Integer = 1
            Dim TempNewRecord As Boolean = False
            Try

                clsCommon.ProgressBarShow()
                Dim dt As DataTable = New DataTable()
                dt.Columns.Add(New DataColumn("User_Code", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Cust_Group_Code", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Cust_Code", System.Type.GetType("System.String")))
                Dim arrUserCode As New ArrayList
                Dim strUserCode As String = ""
                Dim strCustomerGroupCode As String = ""
                Dim strCustomerCode As String = ""
                Dim arrCustomerPermission As New List(Of ClsCustomerPermission)
                Dim obj As ClsCustomerPermission = Nothing
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    strUserCode = clsCommon.myCstr(grow.Cells("User Code").Value)
                    strCustomerGroupCode = clsCommon.myCstr(grow.Cells("Customer Group Code").Value)
                    strCustomerCode = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    If (String.IsNullOrEmpty(strUserCode)) Then
                        Throw New Exception("User Code can not be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strUserCode) > 30 Then
                        Throw New Exception("Length of User Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TempUserCode As String = ""
                    TempUserCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select user_Code from TSPL_USER_MASTER where user_Code='" + strUserCode + "'", trans))
                    If clsCommon.myLen(TempUserCode) <= 0 Then
                        Throw New Exception("Not a valid User Code at line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strCustomerGroupCode)) Then
                        Throw New Exception("Customer Group Code can not be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCustomerGroupCode) > 30 Then
                        Throw New Exception("Length of Customer Group Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TempCustomerGroupCode As String = ""
                    TempCustomerGroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" + strCustomerGroupCode + "'", trans))
                    If clsCommon.myLen(TempCustomerGroupCode) <= 0 Then
                        Throw New Exception("Not a valid Customer Group Code at line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strCustomerCode)) Then
                        Throw New Exception("Customer Code can not be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCustomerCode) > 30 Then
                        Throw New Exception("Length of Customer Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TempCustomerCode As String = ""
                    TempCustomerCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_code from TSPL_CUSTOMER_MASTER where cust_code='" + strCustomerCode + "'", trans))
                    If clsCommon.myLen(TempCustomerCode) <= 0 Then
                        Throw New Exception("Not a valid Customer Code at line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim rows As DataRow() = dt.Select("User_Code='" + clsCommon.myCstr(strUserCode) + "' AND Cust_Group_Code='" + clsCommon.myCstr(strCustomerGroupCode) + "' AND Cust_Code='" + clsCommon.myCstr(strCustomerCode) + "'")
                    If rows Is Nothing OrElse rows.Length <= 0 Then
                        dt.Rows.Add(strUserCode, strCustomerGroupCode, strCustomerCode)
                        obj = New ClsCustomerPermission()
                        obj.User_Code = clsCommon.myCstr(strUserCode)
                        obj.Cust_Group_Code = clsCommon.myCstr(strCustomerGroupCode)
                        obj.Cust_Code = clsCommon.myCstr(strCustomerCode)
                        arrCustomerPermission.Add(obj)
                    End If

                    If Not arrUserCode.Contains(strUserCode) Then
                        arrUserCode.Add(strUserCode)
                    End If
                Next

                Dim MultiUserCode As String = clsCommon.GetMulcallString(arrUserCode)

                If clsCommon.myLen(MultiUserCode) > 0 And arrCustomerPermission.Count > 0 Then
                    obj.SaveData(MultiUserCode, arrCustomerPermission, True)
                End If
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class
