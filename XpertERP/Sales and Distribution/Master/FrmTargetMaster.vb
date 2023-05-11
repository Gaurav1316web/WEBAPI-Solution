'--preeti gupta-ticket no.[BM00000003133]
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common
Public Class FrmTargetMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strTemp() As String
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colItemCode As String = "Item Code"
    Const colDesc As String = "Description"
    Const colUOM As String = "UOM"
    Const colQty As String = "Quantity"
    Const colBalQty As String = "Balance Quantity"
    Const colBalQtyinBtl As String = "Bal Qty In Bottle"
    Const colPost As String = "Post"
    Const colAmount As String = "Amount"
    Const colNewAmount As String = "New Amount"
    Const colBalAmt As String = "Balance Amount"


    Private Sub FrmTargetMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDiscTypeDesc.MaxLength = 50
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        SetUserMgmtNew()
        txtCustCode.Focus()
        dtpMonthnYear.Value = clsCommon.GETSERVERDATE
        dtpMonthnYear.Format = DateTimePickerFormat.Custom
        dtpMonthnYear.CustomFormat = "MMMM, yyyy"
        btnDelete.Enabled = True
        txtAmount.Text = ""
        txtNewAmount.Text = ""
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        isNewEntry = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnTargetMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '  btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTargetMaster()
                obj.Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.NewAmount = clsCommon.myCdbl(txtNewAmount.Text)
                If (clsTargetMaster.SaveData(txtCustCode.Value, txtDiscType.Value, txtCustGroupCode.Text, dtpMonthnYear.Value.Date, obj)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")

                End If
                LoadData(txtCustCode.Value, txtDiscType.Value, dtpMonthnYear.Value)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtCustCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Customer Code")
                txtCustCode.Focus()
                Exit Function
            ElseIf clsCommon.myLen(txtDiscType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Discount Type")
                txtDiscType.Focus()
                Exit Function
            ElseIf clsCommon.myCdbl(txtAmount.Text) <= 0 AndAlso clsCommon.myCdbl(txtNewAmount.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Exactly Single Amount Among 'Amount To Enter OR New Amount'")
                txtAmount.Focus()
                Exit Function
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGO.Click
        LoadData(txtCustCode.Value, txtDiscType.Value, dtpMonthnYear.Value)
    End Sub

    Sub LoadData(ByVal Cust_Code As String, ByVal Discount_Type As String, ByVal Month_Year As String)
        Try
            If clsCommon.myLen(txtCustCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Cust Code")
                txtCustCode.Focus()
                Exit Sub
            ElseIf clsCommon.myLen(txtDiscType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Discount Type")
                txtDiscType.Focus()
                Exit Sub
            ElseIf clsCommon.myLen(dtpMonthnYear.Value.Date) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Month & Year")
                dtpMonthnYear.Focus()
                Exit Sub
            End If
            isCellValueChangedOpen = True
            'LoadBlankGrid()
            ' Dim obj As clsTargetMaster
            'Dim Arr As List(Of clsTargetMaster) = clsTargetMaster.GetData(Cust_Code, Discount_Type, Month_Year)
            'txtCustCode.Value = Cust_Code
            'txtDiscType.Value = Discount_Type
            'dtpMonthnYear.Value = Month_Year
            'If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            '    For Each objTr As clsTargetMaster In Arr
            '        'dgvItem.Rows.AddNew()
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colDesc).Value = objTr.Item_Desc
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Quantity
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colBalQty).Value = objTr.Balance_Qty
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colBalQtyinBtl).Value = objTr.Bal_Qty_InBtl
            '        dgvItem.Rows(dgvItem.Rows.Count - 1).Cells(colPost).Value = objTr.Post
            '        dgvItem.Rows.AddNew()
            '    Next
            'End If
            Dim Arr As List(Of clsTargetMaster) = clsTargetMaster.GetData(Cust_Code, Discount_Type, Month_Year)
            txtCustCode.Value = Cust_Code
            txtDiscType.Value = Discount_Type
            dtpMonthnYear.Value = Month_Year
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsTargetMaster In Arr
                    txtAmount.Text = ""
                    txtNewAmount.Text = ""
                    lblCurrentAmount.Text = clsCommon.myCdbl(objTr.Amount)
                    lblBalAmount.Text = clsCommon.myCdbl(objTr.Bal_Amount)
                Next
            Else
                txtAmount.Text = ""
                txtNewAmount.Text = ""
                lblCurrentAmount.Text = "0"
                lblBalAmount.Text = "0"
            End If
            isNewEntry = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isCellValueChangedOpen = False
            'dgvItem.Rows.AddNew()
        End Try
    End Sub


    Public Sub funreset()
        txtCustCode.Value = ""
        txtCustDesc.Text = ""
        txtCustCode.Focus()
        txtCustGroupCode.Text = ""
        txtCustGroupCodeDesc.Text = ""
        txtDiscType.Value = ""
        txtDiscTypeDesc.Text = ""
        txtAmount.Text = ""
        txtNewAmount.Text = ""
        lblCurrentAmount.Text = ""
        lblBalAmount.Text = ""
    End Sub



    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        delete()
    End Sub

    Public Sub delete()

        Try
            If clsCommon.myLen(txtCustCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Provide Custome Code")
                Exit Sub
            ElseIf clsCommon.myLen(txtDiscType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Provide Discount Type")
                Exit Sub
            ElseIf clsCommon.myLen(dtpMonthnYear.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Provide Month & Date")
                Exit Sub
            End If

            Dim Qry As String = "select * from TSPL_TARGET_MASTER where Cust_code='" + txtCustCode.Value + "' AND Discount_Type='" + txtDiscType.Value + "' And DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(dtpMonthnYear.Value.Date, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(dtpMonthnYear.Value.Date, "dd/MMM/yyy") + "')  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found To Delete")
                Exit Sub
                'End If
                'Dim post As Integer = clsDBFuncationality.getSingleValue(Qry)
                'If post = 1 Then
                '    common.clsCommon.MyMessageBoxShow("Document Is Already Posted, Cann't Be Deleted")
            Else
                clsTargetMaster.DeleteData(txtCustCode.Value, txtDiscType.Value, dtpMonthnYear.Value)
                myMessages.delete()
                funreset()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub FrmTargetMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
      
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            print()

        End If
    End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String

    '        Dim strProgCode = "TRGT-MSTR"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Finders"
    Private Sub txtCustCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustCode._MYValidating
        Dim qry As String = "select Cust_Code as Code, Customer_Name as Description from TSPL_CUSTOMER_MASTER"
        txtCustCode.Value = clsCommon.ShowSelectForm("CustMRMastrFND", qry, "Code", "Status='N' AND OnHold='N'", txtCustCode.Value, "Code", isButtonClicked)
        LoadCustomer()
        txtDiscType.Focus()
    End Sub

    Public Sub LoadCustomer()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_CUSTOMER_MASTER.Customer_Name as CustName, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as CustGroupCode, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as CustGroupDESC from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code=TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code where TSPL_CUSTOMER_MASTER.Status='N' and  TSPL_CUSTOMER_MASTER.Cust_Code='" + txtCustCode.Value + "'")
        If dt.Rows.Count <= 0 Then
            txtCustDesc.Text = ""
            txtCustGroupCode.Text = ""
            txtCustGroupCodeDesc.Text = ""
        Else
            txtCustDesc.Text = clsCommon.myCstr(dt.Rows(0)("CustName"))
            txtCustGroupCode.Text = clsCommon.myCstr(dt.Rows(0)("CustGroupCode"))
            txtCustGroupCodeDesc.Text = clsCommon.myCstr(dt.Rows(0)("CustGroupDESC"))
        End If

    End Sub

    Private Sub txtDiscType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDiscType._MYValidating
        Dim qry As String = "select Code, Description from TSPL_Discount_Master"
        txtDiscType.Value = clsCommon.ShowSelectForm("DiscounterFND", qry, "Code", "", txtDiscType.Value, "Code", isButtonClicked)
        FillDiscTypeDesc()
    End Sub

    Public Sub FillDiscTypeDesc()
        txtDiscTypeDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_Discount_Master where code='" + txtDiscType.Value + "' ")
    End Sub

#End Region


#Region "Old"


    'Private Sub dgvItem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
    '    Try

    '        If Not isCellValueChangedOpen Then
    '            isCellValueChangedOpen = True
    '            If e.Column Is dgvItem.Columns(colItemCode) Then
    '                OpenICodeList(False)
    '            ElseIf e.Column Is dgvItem.Columns(colUOM) Then
    '                openUOM(False)
    '            End If
    '            isCellValueChangedOpen = False
    '            If e.Column Is dgvItem.Columns(colQty) Then
    '                For i As Integer = 0 To dgvItem.Rows.Count - 1
    '                    If clsCommon.myLen(dgvItem.CurrentRow.Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(dgvItem.CurrentRow.Cells(colQty).Value) > 0 Then
    '                        Dim COnverSionFactor As Double = clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + dgvItem.CurrentRow.Cells(colItemCode).Value + "' And UOM_Code  <>'" + dgvItem.CurrentRow.Cells(colUOM).Value + "'")
    '                        'Dim COnverSionFactor As Double = clsItemMaster.GetConvertionFactor(dgvItem.CurrentRow.Cells(colItemCode).Value, dgvItem.CurrentRow.Cells(colUOM).Value, Nothing)
    '                        If dgvItem.CurrentRow.Cells(colUOM).Value = "FC" Or dgvItem.CurrentRow.Cells(colUOM).Value = "EC" Then
    '                            dgvItem.CurrentRow.Cells(colBalQty).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value)) * 1
    '                            dgvItem.CurrentRow.Cells(colBalQtyinBtl).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value) * COnverSionFactor)
    '                        ElseIf dgvItem.CurrentRow.Cells(colUOM).Value = "FB" Or dgvItem.CurrentRow.Cells(colUOM).Value = "EB" Then
    '                            dgvItem.CurrentRow.Cells(colBalQty).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value) * 1)
    '                            dgvItem.CurrentRow.Cells(colBalQtyinBtl).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value) * 1)
    '                        Else
    '                            Dim COnverSionFactor1 As Double = clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + dgvItem.CurrentRow.Cells(colItemCode).Value + "' And UOM_Code  = '" + dgvItem.CurrentRow.Cells(colUOM).Value + "'")
    '                            dgvItem.CurrentRow.Cells(colBalQty).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value) * 1)
    '                            dgvItem.CurrentRow.Cells(colBalQtyinBtl).Value = clsCommon.myCdbl((dgvItem.CurrentRow.Cells(colQty).Value) * COnverSionFactor1)
    '                        End If

    '                    End If
    '                Next
    '            End If
    '            setGridFocus()
    '        End If

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally

    '    End Try
    'End Sub



    'Sub OpenICodeList(ByVal isButtonClick As Boolean)

    '    Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(dgvItem.CurrentRow.Cells(colItemCode).Value), "", isButtonClick)
    '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
    '        dgvItem.CurrentRow.Cells(colItemCode).Value = obj.Item_Code
    '        dgvItem.CurrentRow.Cells(colDesc).Value = obj.Item_Desc
    '        dgvItem.CurrentRow.Cells(colUOM).Value = obj.Unit_Code
    '    Else
    '        dgvItem.CurrentRow.Cells(colItemCode).Value = ""
    '        dgvItem.CurrentRow.Cells(colDesc).Value = ""
    '    End If

    'End Sub
    'Sub openUOM(ByVal isButtonClick As Boolean)
    '    dgvItem.CurrentRow.Cells(colUOM).Value = clsItemMaster.FinderForuom(clsCommon.myCstr(dgvItem.CurrentRow.Cells(colUOM).Value), clsCommon.myCstr(dgvItem.CurrentRow.Cells(colItemCode).Value), isButtonClick)
    'End Sub

    'Private Sub dgvItem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
    '    If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
    '        e.Cancel = True
    '    ElseIf clsCommon.myCstr(dgvItem.CurrentRow.Cells(colPost).Value) = 1 Then
    '        common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item Has Posted Already Under this Customer")
    '        e.Cancel = True
    '    End If
    'End Sub

    'Private Sub setGridFocus()
    '    Dim intCurrRow As Integer = dgvItem.CurrentRow.Index
    '    If intCurrRow = dgvItem.Rows.Count - 1 Then
    '        dgvItem.Rows.AddNew()
    '        dgvItem.CurrentRow = dgvItem.Rows(intCurrRow)
    '    End If

    'End Sub

#End Region

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsTargetMaster.PostData(txtCustCode.Value, txtDiscType.Value, dtpMonthnYear.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtCustCode.Value, txtDiscType.Value, dtpMonthnYear.Value)
                End If
                'btnPost.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplicate.Click
        Replicate()
    End Sub

    Public Sub Replicate()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'Dim i As Integer
            'Dim Arr As New List(Of clsTargetMaster)
            ''Dim Qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code from TSPL_CUSTOMER_MASTER left OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + txtCustGroupCode.Text + "' AND Cust_Code NOt IN (select Cust_code from TSPL_TARGET_MASTER where Discount_Type='" + txtDiscType.Value + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(dtpMonthnYear.Value.Date, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(dtpMonthnYear.Value.Date, "dd/MMM/yyy") + "') AND post=1) "
            Dim Qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code from TSPL_CUSTOMER_MASTER left OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + txtCustGroupCode.Text + "' AND Cust_Code NOt IN ('" + txtCustCode.Value + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            Dim isSaved As Boolean = True
            Dim obj As New clsTargetMaster()
            obj.Amount = clsCommon.myCdbl(txtAmount.Text)

            For Each dr As DataRow In dt.Rows
                Dim strACode As String = clsCommon.myCstr(dr("Cust_Code"))
                'Arr.Clear()
                'For ii As Integer = 0 To dgvItem.Rows.Count - 1
                '    'Arr = New List(Of clsTargetMaster)
                '    Dim strICode As String = clsCommon.myCstr(dgvItem.Rows(ii).Cells(colItemCode).Value)
                '    If (clsCommon.myLen(strICode) > 0) Then
                '        Dim objTr As New clsTargetMaster()
                '        objTr.Item_Code = strICode
                '        objTr.UOM = clsCommon.myCstr(dgvItem.Rows(ii).Cells(colUOM).Value)
                '        objTr.Quantity = clsCommon.myCdbl(dgvItem.Rows(ii).Cells(colQty).Value)
                '        objTr.Balance_Qty = clsCommon.myCdbl(dgvItem.Rows(ii).Cells(colBalQty).Value)
                '        objTr.Bal_Qty_InBtl = clsCommon.myCdbl(dgvItem.Rows(ii).Cells(colBalQtyinBtl).Value)
                '        Arr.Add(objTr)
                '    End If
                'Next
                isSaved = isSaved AndAlso clsTargetMaster.SaveData(strACode, txtDiscType.Value, txtCustGroupCode.Text, dtpMonthnYear.Value, trans, obj)
                'trans.Commit()

            Next

            If (isSaved) Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Replicated Successfully")
                btnSave.Text = "Update"
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try

            If clsCommon.myLen(txtDiscType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Discount Type")
                Exit Sub
                txtDiscType.Focus()
            ElseIf clsCommon.myLen(dtpMonthnYear.Value.Date.Year) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Provide Year")
                Exit Sub
                dtpMonthnYear.Focus()
            End If
            Dim Qry As String = "select MAX(DiscType) as DiscType, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, '" + clsCommon.myCstr(dtpMonthnYear.Value.Date.Year) + "' as Year, Cust_Code,max(Customer_Name) as CustName, SUM(Month1) as Month1, SUM(Month2) as Month2, " & _
        " SUM(Month3) as Month3, SUM(Month4) as Month4, SUM(Month5) as Month5, SUM(Month6) as Month6, SUM(Month7) as Month7, SUM(Month8) as Month8, " & _
        " SUM(Month9) as Month9, SUM(Month10) as Month10, SUM(Month11) as Month11 , SUM(Month12) as Month12, SUM(Amount) as [TotalAmt], SUM(Bal_Amount) as [TotalBalAmt], (Sum(Amount)-Sum(Bal_Amount)) as [PaiDBal] " & _
        " from ( " & _
        " select TSPL_Discount_Master.Description as DiscType, TSPL_TARGET_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name " & _
        " ,case when  DatePart(Month, Month_Year) =1 then Amount else 0 end Month1" & _
        " ,case when  DatePart(Month, Month_Year) =2 then Amount else 0 end Month2" & _
        " ,case when  DatePart(Month, Month_Year) =3 then Amount else 0 end Month3" & _
        " ,case when  DatePart(Month, Month_Year) =4 then Amount else 0 end Month4" & _
        " ,case when  DatePart(Month, Month_Year) =5 then Amount else 0 end Month5 " & _
        " ,case when  DatePart(Month, Month_Year) =6 then Amount else 0 end Month6" & _
        " ,case when  DatePart(Month, Month_Year) =7 then Amount else 0 end Month7" & _
        " ,case when  DatePart(Month, Month_Year) =8 then Amount else 0 end Month8" & _
        " ,case when  DatePart(Month, Month_Year) =9 then Amount else 0 end Month9" & _
        " ,case when  DatePart(Month, Month_Year) =10 then Amount else 0 end Month10" & _
        " ,case when  DatePart(Month, Month_Year) =11 then Amount else 0 end Month11" & _
        " ,case when  DatePart(Month, Month_Year) =12 then Amount else 0 end Month12" & _
        " , Amount, Bal_Amount" & _
            " from TSPL_TARGET_MASTER " & _
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_TARGET_MASTER.Cust_Code Left Outer Join TSPL_Discount_Master on TSPL_Discount_Master.Code=TSPL_TARGET_MASTER.Discount_Type " & _
        " where DatePart(Year, TSPL_TARGET_MASTER.Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(dtpMonthnYear.Value.Date) + "') And Discount_Type='" + txtDiscType.Value + "'" & _
        " )xxx" & _
        " group by Cust_code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptTarget", "Target Report")
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub RMIExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIExit.Click
        Me.Close()
    End Sub

    Private Sub RMIImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIImport.Click
        ExportData()
    End Sub
    Public Sub ExportData()
        Dim str As String
        str = "select Cust_Code as[Customer Code], Cust_Group_Code as [Customer Group Code], Discount_Type as [Discount Type], Month_Year as [Month & Year], Amount, Bal_Amount as [Balance Amount] from  TSPL_TARGET_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RMIExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIExport.Click
        ImportData()
    End Sub
    Public Sub ImportData()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Customer Group Code", "Discount Type", "Month & Year", "Amount", "Balance Amount") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim Lineno As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    Lineno = Lineno + 1
                    Dim CustCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(CustCode) <= 0 Then
                        Throw New Exception("The Customer Code At The Line No '" + Lineno + "' Cann't Be Left Blank")
                        Exit Sub
                    Else
                        Dim Qry11 As String = "select COUNT(Cust_Code) from TSPL_CUSTOMER_MASTER Where Cust_Code='" + CustCode + "'"
                        Dim Count1 As Integer = clsDBFuncationality.getSingleValue(Qry11, trans)
                        If Count1 < 1 Then
                            Throw New Exception("The Customer Code '" + CustCode + "' At The Line No '" + Lineno + "' Is Not Present in Master")
                            Exit Sub
                        End If
                    End If

                    Dim Qry1 As String = "Select Cust_Group_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + CustCode + "'"
                    Dim CustGroupCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry1, trans))

                    Dim DiscType As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(DiscType) <= 0 Then
                        Throw New Exception("Discount Code Against Customer '" + clsCommon.myCstr(CustCode) + "' At '" + clsCommon.myCstr(Lineno) + "' Cann't Be Left Blank")
                        Exit Sub
                    Else
                        Dim qryDiscType As String = "Select COUNT(*) from TSPL_Discount_Master Where Code='" + DiscType + "' "
                        Dim Check As Integer = clsDBFuncationality.getSingleValue(qryDiscType, trans)
                        If Check <= 0 Then
                            Throw New Exception("Discount Code With Customer '" + clsCommon.myCstr(CustCode) + "' At '" + clsCommon.myCstr(Lineno) + "' is Not Present In Master")
                            Exit Sub
                        End If
                    End If

                    Dim MonthYear As String = clsCommon.GetPrintDate(grow.Cells(3).Value, "dd/MMM/yyyy")
                    If clsCommon.myLen(MonthYear) <= 0 Or clsCommon.myLen(MonthYear) > 11 Then
                        Throw New Exception("Please Enter Mmonth&Year Having Format 'DD/MM/YYYY' Against Customer '" + CustCode + "' At '" + Lineno + "'")
                        Exit Sub
                    Else
                        MonthYear = clsCommon.GetPrintDate(MonthYear, "dd/MMM/yyyy")
                    End If
                    Dim Amount As Double
                    If clsCommon.myCdbl(grow.Cells(4).Value) = 0 Then
                        Amount = Nothing
                    Else
                        Amount = clsCommon.myCdbl(grow.Cells(4).Value)
                    End If

                    Dim balAmt As Double = clsCommon.myCdbl(grow.Cells(5).Value)
                    If clsCommon.myLen(balAmt) <= 0 Or balAmt = 0 Then
                        balAmt = Amount
                    End If
                    Dim qry As String = "Delete from TSPL_TARGET_MASTER where Cust_code='" + CustCode + "' AND Discount_Type='" + DiscType + "' AND  DatePart(Month, Month_Year) = DatePart(Month, '" + clsCommon.GetPrintDate(MonthYear, "dd/MMM/yyy") + "') And DatePart(Year, Month_Year) = DatePart(Year, '" + clsCommon.GetPrintDate(MonthYear, "dd/MMM/yyy") + "')   "
                    clsDBFuncationality.getSingleValue(qry, trans)
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", CustCode)
                    clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", CustGroupCode)
                    clsCommon.AddColumnsForChange(coll, "Discount_Type", DiscType)
                    clsCommon.AddColumnsForChange(coll, "Month_Year", clsCommon.GetPrintDate(MonthYear, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")).ToString())
                    clsCommon.AddColumnsForChange(coll, "Amount", Amount)
                    clsCommon.AddColumnsForChange(coll, "Bal_Amount", balAmt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TARGET_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        txtNewAmount.Text = ""
    End Sub

    Private Sub txtNewAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNewAmount.TextChanged
        txtAmount.Text = ""
    End Sub


End Class
