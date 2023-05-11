' ----------------- Created By Anubhooti On 16-Oct-2015 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Imports common
Imports System.IO

Public Class FrmSolutionKnowledgeBase
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public ObjRtnSol As ClsSolutionKnowledgeBase
    Public Instance As Boolean = False
    Public frm As FrmServiceCall
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSolutionKnowledgeBase)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub AddNew()
        TxtCode.Value = ""
        TxtItemCode.Value = ""
        LblItemName.Text = ""
        LblUpdatedBy.Text = objCommonVar.CurrentUserCode
        LblUpdatedOn.Text = clsCommon.GETSERVERDATE()
        TxtSolution.Text = ""
        TxtSymptom.Text = ""
        TxtCause.Text = ""
        TxtRemarks.Text = ""
        txtcode.MyReadOnly = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        txtcode.Focus()
        If Instance = True Then
            btnsave.Text = "Ok"
        Else
            btnsave.Text = "Save"
        End If

        btndelete.Enabled = False
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            btnsave.Focus()
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue("Code")
                txtcode.Focus()
                txtcode.Select()
                errorControl.SetError(txtcode, "Code")
                Return False
            Else
                errorControl.ResetError(txtcode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtItemCode.Value)) <= 0 Then
                myMessages.blankValue("Item code")
                TxtItemCode.Focus()
                TxtItemCode.Select()
                errorControl.SetError(TxtItemCode, "Item code")
                Return False
            Else
                errorControl.ResetError(TxtItemCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtSolution.Text)) <= 0 Then
                myMessages.blankValue("Solution")
                TxtSolution.Focus()
                TxtSolution.Select()
                errorControl.SetError(TxtSolution, "Solution")
                Return False
            Else
                errorControl.ResetError(TxtSolution)
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            btnsave.Focus()
            If AllowToSave() Then
                Dim obj As New ClsSolutionKnowledgeBase()
                obj.Document_Code = txtcode.Value
                obj.Item_Code = clsCommon.myCstr(TxtItemCode.Value)
                obj.Updated_By = clsCommon.myCstr(LblUpdatedBy.Text)
                obj.Updated_On = clsCommon.myCstr(LblUpdatedOn.Text)
                obj.Solution = clsCommon.myCstr(TxtSolution.Text)
                obj.Symptom = clsCommon.myCstr(TxtSymptom.Text)
                obj.Cause = clsCommon.myCstr(TxtCause.Text)
                obj.Remarks = clsCommon.myCstr(TxtRemarks.Text)

                Dim qry As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(Document_Code) FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE WHERE Document_Code='" + obj.Document_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsSolutionKnowledgeBase.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(txtcode.Value)
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsSolutionKnowledgeBase = ClsSolutionKnowledgeBase.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            txtcode.Value = obj.Document_Code
            TxtItemCode.Value = obj.Item_Code
            If clsCommon.myLen(clsCommon.myCstr(TxtItemCode.Value)) > 0 Then
                LblItemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" + TxtItemCode.Value + "'"))
            Else
                LblItemName.Text = ""
            End If
            LblUpdatedBy.Text = obj.Updated_By
            LblUpdatedOn.Text = obj.Updated_On
            TxtSolution.Text = obj.Solution
            TxtSymptom.Text = obj.Symptom
            TxtCause.Text = obj.Cause
            TxtRemarks.Text = obj.Remarks

            UcAttachment1.LoadData(txtcode.Value)

            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(TxtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + TxtCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_SW_SOLUTION_KNOWLEDGE_BASE WHERE Document_Code='" + txtcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub

    Private Sub FrmSolutionKnowledgeBase_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmSolutionKnowledgeBase_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        LoadCallData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub LoadCallData()
        If clsCommon.myLen(txtcode.Value) > 0 Then
            Dim RCount As Integer = frm.gvSol.Rows.Count
            Dim CallColSNo As String = FrmServiceCall.ColSNo
            Dim CallColCode As String = FrmServiceCall.ColCode
            Dim CallColUpdatedBy As String = FrmServiceCall.ColUpdatedBy
            Dim CallColUpdatedOn As String = FrmServiceCall.ColUpdatedOn
            Dim CallColSol As String = FrmServiceCall.ColSolution
            Dim CallColSymptom As String = FrmServiceCall.ColSymptom

            frm.gvSol.Rows.AddNew()
            frm.gvSol.Rows(RCount).Cells(CallColSNo).Value = RCount + 1
            frm.gvSol.Rows(RCount).Cells(CallColCode).Value = txtcode.Value
            frm.gvSol.Rows(RCount).Cells(CallColUpdatedBy).Value = LblUpdatedBy.Text
            frm.gvSol.Rows(RCount).Cells(CallColUpdatedOn).Value = LblUpdatedOn.Text
            frm.gvSol.Rows(RCount).Cells(CallColSol).Value = TxtSolution.Text
            frm.gvSol.Rows(RCount).Cells(CallColSymptom).Value = TxtSymptom.Text
        End If
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
        'ObjRtnSol = New ClsSolutionKnowledgeBase()
        'ObjRtnSol.Document_Code = txtcode.Value
        'ObjRtnSol.Updated_On = LblUpdatedOn.Text
        'ObjRtnSol.Updated_By = LblUpdatedBy.Text
        'ObjRtnSol.Solution = TxtSolution.Text
        'ObjRtnSol.Symptom = TxtSymptom.Text
        'ObjRtnSol.Item_Code = TxtItemCode.Value
        'ObjRtnSol.Remarks = TxtRemarks.Text
        'ObjRtnSol.Cause = TxtCause.Text
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(TxtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_SW_SOLUTION_KNOWLEDGE_BASE where Document_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            TxtCode.MyReadOnly = False
        Else
            TxtCode.MyReadOnly = True
        End If

        If TxtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            txtcode.Value = ClsSolutionKnowledgeBase.GetFinder("", txtcode.Value, isButtonClicked)
            ' txtcode.Value = clsCommon.ShowSelectForm("SWActTF", qry, "Code", "", txtcode.Value, "", isButtonClicked)
            If clsCommon.myLen(TxtCode.Value) > 0 Then
                Dim objOT As ClsSolutionKnowledgeBase
                objOT = ClsSolutionKnowledgeBase.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(TxtCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub TxtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItemCode._MYValidating
        Try

            Dim Qry As String = String.Empty
            Qry = " SELECT DISTINCT TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AS [Code],TSPL_ITEM_MASTER.Item_Desc As [Item Desp],TSPL_ITEM_MASTER.Short_Description AS [Short Description],TSPL_ITEM_MASTER.Item_Type AS [Item Type],ISNULL(TSPL_ITEM_MASTER.item_category,'') AS [Item Category],ISNULL(TSPL_MF_BOM_HEAD.REVISION_NO,'') AS [Revision No] FROM TSPL_SD_SALE_INVOICE_HEAD " & _
                  " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                  " LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "

            TxtItemCode.Value = clsCommon.ShowSelectForm("SEVehNam", Qry, "Code", "  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='ALL' AND TSPL_MF_BOM_HEAD.POSTED=1 AND TSPL_SD_SALE_INVOICE_HEAD.Status=1  ", TxtItemCode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(TxtItemCode.Value) > 0 Then
                LblItemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Desc FROM TSPL_ITEM_MASTER WHERE Item_Code='" & TxtItemCode.Value & "'"))
            Else
                LblItemName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class