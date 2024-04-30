'--------Created By Richa 10/12/2014 Against Ticket No BM00000004983

Imports common
Imports System.Data.SqlClient
Public Class FrmMerchantPaymentTermsGroup
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
#End Region
#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmMerchantPaymentTermsGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.Focus()
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmMerchantPaymentTermsGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
    End Sub
    Public Sub closeform()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub fungridfill()
        dgv_Groupmapping.AutoGenerateColumns = False
        Try
            Dim strQuery As String = "select Code,Description from TSPL_PAYMENT_TERMS_MASTER_MT"
            transportSql.FillGridView(strQuery, dgv_Groupmapping)
            dgv_Groupmapping.Columns(0).FieldName = "Code"
            dgv_Groupmapping.Columns(1).FieldName = "Description"
            dgv_Groupmapping.Select()
            textbox_lostfocus()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    Sub textbox_lostfocus()
        dgv_Groupmapping.Select()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmMerchantPaymentTermsGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        If btnsave.Visible = True Then
            RMExport.Enabled = True
            RMImport.Enabled = True
        Else
            RMExport.Enabled = False
            RMImport.Enabled = False
        End If
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        fndGroupCode.Value = ""
        TxtDescription.Text = ""
        fndGroupCode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        isNewEntry = True
        Dim desc As String = ""
        fungridfill()
    End Sub

    Private Sub fndGroupCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGroupCode._MYNavigator
        Try
            LoadData(fndGroupCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndGroupCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGroupCode._MYValidating
        Dim str As String = "select count(*) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code ='" + fndGroupCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndGroupCode.MyReadOnly = False
        Else
            fndGroupCode.MyReadOnly = True
        End If
        If fndGroupCode.MyReadOnly OrElse isButtonClicked Then
            fndGroupCode.Value = ClsMerchantPaymentTermsGroup.getFinder("", fndGroupCode.Value, isButtonClicked)
            If fndGroupCode.Value <> "" Then
                LoadData(fndGroupCode.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        End If
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsMerchantPaymentTermsGroup()
                obj.Group_Code = fndGroupCode.Value
                obj.Description = TxtDescription.Text

                Dim i As Integer = 0
                Dim objDetail As New ClsMerchantPaymentTermsGroupDetail
                obj.arrPaymentTermsgroupDetail = New List(Of ClsMerchantPaymentTermsGroupDetail)
                For i = 0 To dgv_Groupmapping.Rows.Count - 1

                    If CBool(dgv_Groupmapping.Rows(i).Cells(2).Value) Then
                        objDetail = New ClsMerchantPaymentTermsGroupDetail
                        objDetail.Group_Code = clsCommon.myCstr(obj.Group_Code)
                        objDetail.Terms_Code = clsCommon.myCstr(dgv_Groupmapping.Rows(i).Cells(0).Value)
                        obj.arrPaymentTermsgroupDetail.Add(objDetail)
                    End If
                Next

                If (ClsMerchantPaymentTermsGroup.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    LoadData(obj.Group_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsMerchantPaymentTermsGroup = ClsMerchantPaymentTermsGroup.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndGroupCode.Value = obj.Group_Code
            TxtDescription.Text = obj.Description
          
            fungridfill()
            Dim strtermscode As String = ""
            If obj.arrPaymentTermsgroupDetail IsNot Nothing Then
                For i As Integer = 0 To obj.arrPaymentTermsgroupDetail.Count - 1
                    strtermscode = obj.arrPaymentTermsgroupDetail(i).Terms_Code
                    For j As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
                        If dgv_Groupmapping.Rows(j).Cells(0).Value = strtermscode Then
                            dgv_Groupmapping.Rows(j).Cells(2).Value = True
                        End If
                    Next
                Next
            End If

            fndGroupCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
         
        Else
            Reset()

        End If
    End Sub
    Private Function AllowToSave() As Boolean
        Dim count As Integer = 0
        If clsCommon.myLen(fndGroupCode.Value) <= 0 Then
            fndGroupCode.Focus()
            Throw New Exception("Code cannot be left blank.")
        End If
        If clsCommon.myCstr(TxtDescription.Text) = "" Then
            TxtDescription.Focus()
            Throw New Exception("Description cannot be left blank.")
        End If
        For i As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
            If CBool(dgv_Groupmapping.Rows(i).Cells(2).Value) Then
                count = count + 1
            End If
        Next

        If count = 0 Then
            Throw New Exception("Please select atleast one Terms code.")
        End If

        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsMerchantPaymentTermsGroup.DeleteData(fndGroupCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data deleted successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        closeform()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Group Code", "Description", "Terms Code") Then
            Dim linno As Integer = 1
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()

                Dim check As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsMerchantPaymentTermsGroup()

                    Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    Dim strGroupdescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim strtermscode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)


                    linno += 1

                    If strgroupCode = String.Empty Then
                        Throw New Exception("Group Code cannot be left blank at Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If strGroupdescription = String.Empty Then
                        Throw New Exception("Description cannot be left blank at Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If strtermscode = String.Empty Then
                        Throw New Exception("Terms Code cannot be left blank at Line No. " + clsCommon.myCstr(linno) + ".")
                    Else
                        check = clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_PAYMENT_TERMS_MASTER_MT where Code ='" & strtermscode & "'", trans)
                        If check <= 0 Then
                            Throw New Exception("Terms Code not exist in Payment Terms master main data at Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                 

                    If strgroupCode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Group Code length cannot be more than 12")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If strGroupdescription.Length > 200 Then
                        common.clsCommon.MyMessageBoxShow("Description length cannot be more than 200")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strtermscode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Terms Code length cannot be more than 12")
                        trans.Rollback()
                        Exit Sub
                    End If




                    obj.Group_Code = strgroupCode
                    obj.Description = strGroupdescription

                    If clsCommon.myLen(strgroupCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT where Group_Code='" + strgroupCode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    If IsNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Group_Code", obj.Group_Code.ToUpper())
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT", OMInsertOrUpdate.Update, "TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code='" + obj.Group_Code + "'", trans)
                    End If

                    ''richa 
                    If clsCommon.myLen(strgroupCode) > 0 AndAlso clsCommon.myLen(strtermscode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT where Group_Code='" & strgroupCode & "' and Terms_Code='" & strtermscode & "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    Dim coll1 As New Hashtable()
                    clsCommon.AddColumnsForChange(coll1, "Group_Code", obj.Group_Code.ToUpper())
                    clsCommon.AddColumnsForChange(coll1, "Terms_Code", strtermscode.ToUpper())
                    If IsNewEntry Then
                        clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT", OMInsertOrUpdate.Update, "TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code='" + obj.Group_Code + "'  and Terms_Code='" & strtermscode & "'", trans)
                    End If

                    ''-----------


                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code as [Group Code],TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Description ,TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code as [Terms Code] from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT Left Outer Join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code "
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
