'--------Created By Richa 23/07/2014 Against Ticket No BM00000003237

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

Public Class FrmVendorPermission
    Inherits FrmMainTranScreen
#Region "Variables"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim IsLoadData As Boolean = False


#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region


    Private Sub FrmVendorPermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
        GrpVendorGroup.Enabled = Not chkVendorGroupAll.IsChecked
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorPermission)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub VendorGroupCode()
        Try
            Dim dt As DataTable
            Dim i As Integer = 0

            Dim qry As String = "Select CAST(0 as BIT) as [Select],Ven_Group_Code as [Ven Group Code] ,Group_Desc as [Group Desc] from TSPL_VENDOR_GROUP "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.Columns("Select").Width = 60
                gv.Columns("Ven Group Code").Width = 100
                gv.Columns("Group Desc").Width = 140
                gv.Columns("Ven Group Code").ReadOnly = True
                gv.Columns("Group Desc").ReadOnly = True
                gv.Columns("Select").ReadOnly = False
                gv.AllowEditRow = True
                GridviewdataVendorCode()
            Else
                gv.DataSource = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub
   
 
    Public Sub GridviewdataVendorCode()
        Try
            Dim StrVendorGroupCode As String = ""
            'Dim dt As DataTable
            Dim i As Integer = 0
            For ii As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(ii).Cells(0).Value = True Then
                    StrVendorGroupCode = StrVendorGroupCode & "'" & clsCommon.myCstr(gv.Rows(ii).Cells(1).Value).Replace("'", "''").ToString() & "'" & ","
                End If
            Next

            If StrVendorGroupCode <> "" Then
                
                StrVendorGroupCode = StrVendorGroupCode.Substring(0, StrVendorGroupCode.Length - 1)
                Dim qry As String
              
                qry = "Select CAST(Case When ISNULL(XXX.Vendor_Code,'')='' Then 0 Else 1 End as BIT) as [Select], TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name]," & _
            " TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group Code] from (Select Vendor_Code from TSPL_VENDOR_MAPPING WHERE User_Code='" + fndUser_Name.Value + "')" & _
            " XXX RIGHT OUTER JOIN TSPL_VENDOR_MASTER ON XXX.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code" & _
            " WHERE TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + StrVendorGroupCode + ")"
                GvVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
                GvVendor.Columns("Select").Width = 60
                GvVendor.Columns("Vendor Code").Width = 100
                GvVendor.Columns("Vendor Name").Width = 150
                GvVendor.Columns("Vendor Group Code").Width = 120
                GvVendor.Columns("Vendor Code").ReadOnly = True
                GvVendor.Columns("Vendor Name").ReadOnly = True
                GvVendor.Columns("Vendor Group Code").ReadOnly = True
                GvVendor.Columns("Select").ReadOnly = False
                GvVendor.AllowEditRow = True
              
            Else
                GvVendor.DataSource = Nothing

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
                Throw New Exception("Please Select Atleast one Vendor Group")
            End If
            For ii As Integer = 0 To GvVendor.Rows.Count - 1
                If GvVendor.Rows(ii).Cells(0).Value = True Then
                    Flag1 = Flag1 + 1
                End If
            Next

            If Flag1 <= 0 Then
                Throw New Exception("Please Select Atleast one Vendor")
            End If
            Dim arrCustomer As New List(Of ClsVendorPermission)
            Dim obj As ClsVendorPermission = Nothing
            'Dim CustomerGrpCode As New List(Of String)
            Dim UserCode As String
            Dim i As Integer = 0
            Dim j As Integer = 0
            UserCode = clsCommon.myCstr(fndUser_Name.Value)
            For Each row As GridViewRowInfo In GvVendor.Rows
                If row.Cells(0).Value = True Then
                    obj = New ClsVendorPermission()
                    obj.Vendor_Code = clsCommon.myCstr(row.Cells(1).Value)
                    obj.Vendor_Group_Code = clsCommon.myCstr(row.Cells(3).Value)
                    obj.User_Code = clsCommon.myCstr(fndUser_Name.Value)
                    arrCustomer.Add(obj)
                End If
            Next

            If arrCustomer.Count > 0 Then
                If obj.SaveData(UserCode, arrCustomer) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            'VendorGroupCode()
            textchangedsub()
            fndUser_NameLeave()

        End If
    End Sub

    Private Sub fndUser_Name__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndUser_Name._MYNavigator
        Dim qst As String = "select User_Code as [User Code],User_Name as [User Name] from TSPL_USER_MASTER   where  2=2"
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
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
                common.clsCommon.MyMessageBoxShow("User Code does not exist Master Table")
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

        Dim strUser_Code As String = "select User_Code from TSPL_VENDOR_MAPPING where User_Code='" + fndUser_Name.Value + "'"
        Dim strvalue As String
        strvalue = clsDBFuncationality.getSingleValue(strUser_Code)


        If (strvalue <> "") Then
            funfill()
            btnsave.Text = "&Update"
            rbtnDelete.Enabled = True

        Else
            VendorGroupCode()
            btnsave.Text = "&Save"
            TxtUserName.Text = " "
        End If
        TxtUserName.Text = strvalue1
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()

        Dim dt As DataTable
        Dim strQuery As String
        strQuery = " Select distinct CAST(Case When ISNULL(XXX.Vendor_Group_Code,'')='' Then 0 Else 1 End as BIT) as [Select], TSPL_VENDOR_GROUP.Ven_Group_Code as [Ven Group Code], TSPL_VENDOR_GROUP.Group_Desc as [Group Desc] from (Select Vendor_Group_Code  from TSPL_VENDOR_MAPPING WHERE User_Code='" + fndUser_Name.Value + "')" & _
         " XXX RIGHT OUTER JOIN TSPL_VENDOR_GROUP ON XXX.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code "
        dt = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            gv.DataSource = clsDBFuncationality.GetDataTable(strQuery)
            gv.Columns("Select").Width = 60
            gv.Columns("Ven Group Code").Width = 100
            gv.Columns("Group Desc").Width = 140
            gv.Columns("Ven Group Code").ReadOnly = True
            gv.Columns("Group Desc").ReadOnly = True

            gv.Columns("Select").ReadOnly = False

            gv.AllowEditRow = True
            GridviewdataVendorCode()
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
            ChkVendorSelect.IsChecked = True
            chkVendorGroupSelect.IsChecked = True
            VendorGroupCode()
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
                If (ClsVendorPermission.DeleteData(fndUser_Name.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkVendorGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorGroupAll.ToggleStateChanged
        Try
            IsLoadData = True
            For Each row As GridViewRowInfo In gv.Rows
                row.Cells(0).Value = chkVendorGroupAll.IsChecked
            Next
        Catch ex As Exception
        Finally
            IsLoadData = False
            GridviewdataVendorCode()
        End Try
    End Sub

    Private Sub ChkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkVendorAll.ToggleStateChanged
        If ChkVendorAll.IsChecked Then
            For ii As Integer = 0 To GvVendor.Rows.Count - 1
                GvVendor.Rows(ii).Cells(0).Value = True
            Next
        Else
            For ii As Integer = 0 To GvVendor.Rows.Count - 1
                GvVendor.Rows(ii).Cells(0).Value = False
            Next

        End If
    End Sub

    Private Sub FrmVendorPermission_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub


    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not IsLoadData Then
            If e.Column Is gv.Columns(0) Then
                GridviewdataVendorCode()
            End If
        End If
    End Sub
End Class
