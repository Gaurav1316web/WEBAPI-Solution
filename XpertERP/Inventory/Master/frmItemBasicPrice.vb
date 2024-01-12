
Imports common
Imports System.Data.SqlClient

Public Class FrmItemBasicPrice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ItemCode, MRPCode As String
#End Region
    Public Sub New(ByVal Item As String, ByVal MRP As String)
        InitializeComponent()
        ItemCode = Item
        MRPCode = MRP
    End Sub

    Private Sub FrmItemBasicPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
    End Sub
    '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.inventorySetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag

        If btnSave.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If

        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    '--------------------------------------------------
    Private Sub btnsave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        SaveData()
    End Sub
    Function AllowToSave() As Boolean

        If clsCommon.myLen(fndItemCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Item Code", Me.Text)
            TxtSubCategoryCode.Focus()
            Return False
        End If

        If clsCommon.myLen(fndMRPCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select MRP Code", Me.Text)
            txtCategory.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCost) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter the Item Cost", Me.Text)
            txtCategory.Focus()
            Return False
        End If

        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsItemBasicPrice()
                obj.Item_Code = fndItemCode.Value
                obj.Basic_Price = txtCost.Text
                obj.MRP = clsCommon.myCdbl(fndMRPCode.Value)

                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsItemBasicPrice.DeleteData(fndItemCode.Value, fndMRPCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        CloseForm()
    End Sub

    Private Sub fndItemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndItemCode._MYValidating
        Dim qry As String = "select Item_Code  as [Code],Item_Desc  as [Description] from TSPL_ITEM_MASTER"
        fndItemCode.Value = clsCommon.ShowSelectForm("Item Code", qry, "Code", "", fndItemCode.Value, "Code", isButtonClicked)
        fndMRPCode.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(Item_MRP)  from TSPL_ITEM_PRICE_MASTER where Item_Code='" + fndItemCode.Value + "'"))
        txtCost.Text = clsItemBasicPrice.GetBasicPrice(fndItemCode.Value, clsCommon.myCdbl(fndMRPCode.Value))
    End Sub

    Private Sub fndMRPCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndMRPCode._MYValidating
        Dim qry As String = "select distinct(Item_MRP) as [MRP],Start_Date as [Date]  from TSPL_ITEM_PRICE_MASTER "
        fndMRPCode.Value = clsCommon.ShowSelectForm("MRP Code", qry, "MRP", " Item_Code='" + fndItemCode.Value + "'", fndMRPCode.Value, "", isButtonClicked)
        txtCost.Text = clsItemBasicPrice.GetBasicPrice(fndItemCode.Value, clsCommon.myCdbl(fndMRPCode.Value))
    End Sub

    Private Sub btnAddNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton4.Click
        AddNew()
    End Sub
    Sub AddNew()
        fndItemCode.Value = Nothing
        txtCost.Text = 0
        fndMRPCode.Value = ""
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True

    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub FrmItemBasicPrice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        End If
    End Sub
End Class
