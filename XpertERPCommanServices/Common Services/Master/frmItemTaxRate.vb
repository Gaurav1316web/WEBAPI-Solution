'Developed By -  Panch Raj
'Start Date - 28/06/2017
'Table Used - TSPL_ITR_HEAD,TSPL_ITR_DETAIL

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Public Class frmItemTaxRate
    Inherits FrmMainTranScreen
    Const colLineNo As String = "colLineNo"
    Const colTAX_GROUP_CODE As String = "colTAX_GROUP_CODE"
    Const colTAX_GROUP_Desc As String = "colTAX_GROUP_Desc"
    Const colITEM_CODE As String = "colITEM_CODE"
    Const colITEM_Desc As String = "colITEM_Desc"
    'Const colRATE As String = "colRATE"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String   
    Private isInsideLoadData As Boolean = False
    Private isCellValueChanged As Boolean = False
    Private isNewEntry As Boolean = False

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroup = New GridViewTextBoxColumn()
        repoTaxGroup.FormatString = ""
        repoTaxGroup.HeaderText = "Tax Group Code"
        repoTaxGroup.Name = colTAX_GROUP_CODE
        repoTaxGroup.Width = 100
        repoTaxGroup.ReadOnly = True
        repoTaxGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaxGroup.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        gv1.MasterTemplate.Columns.Add(repoTaxGroup)

        Dim repoTaxGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroupDesc = New GridViewTextBoxColumn()
        repoTaxGroupDesc.FormatString = ""
        repoTaxGroupDesc.HeaderText = "Tax Group Desc"
        repoTaxGroupDesc.Name = colTAX_GROUP_Desc
        repoTaxGroupDesc.Width = 100
        repoTaxGroupDesc.ReadOnly = True
        repoTaxGroupDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaxGroupDesc.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        gv1.MasterTemplate.Columns.Add(repoTaxGroupDesc)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colITEM_CODE
        repoItemCode.Width = 100
        repoItemCode.ReadOnly = True
        repoItemCode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Desc"
        repoItemDesc.Name = colITEM_Desc
        repoItemDesc.Width = 100
        repoItemDesc.ReadOnly = True
        repoItemDesc.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        '' create columns for all taxes
        Dim qry As String = "select Tax_Code,Tax_Code_Desc from TSPL_TAX_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim repoTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        For Each drTax As DataRow In dt.Rows
            repoTax = New GridViewDecimalColumn()
            repoTax.FormatString = ""
            repoTax.HeaderText = clsCommon.myCstr(drTax.Item("Tax_Code_Desc"))
            repoTax.Tag = clsCommon.myCstr(drTax.Item("Tax_Code"))
            repoTax.Name = clsCommon.myCstr(drTax.Item("Tax_Code"))
            repoTax.Width = 100
            repoTax.ReadOnly = True
            repoTax.IsVisible = False
            repoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoTax)
        Next

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        
        If Not isInsideLoadData Then
            ' isCellValueChanged = False
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If e.Column Is gv1.Columns(colTAX_GROUP_CODE) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    OpenTaxGroupCode(True)
                    isCellValueChanged = False
                End If
                If e.Column Is gv1.Columns(colITEM_CODE) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    OpenICode(True)
                    isCellValueChanged = False
                End If
            End If
          
        End If
    End Sub
    Sub OpenICode(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(colITEM_CODE).Value = clsItemMaster.getFinder("", gv1.CurrentRow.Cells(colITEM_CODE).Value, isButtonClick)
    End Sub
    Sub OpenTaxGroupCode(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Tax_Group_Code,Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Active=1"
        isInsideLoadData = True
        gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = clsTaxGroupMaster.getFinder("", gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value, isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) > 0 Then
            '' visible tax columns
            qry = "select Tax_Code,Tax_Code_Desc from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            For Each col As GridViewColumn In gv1.Columns
                For Each drCol As DataRow In dt.Rows
                    If col.Tag = clsCommon.myCstr(drCol.Item("Tax_Code")) Then
                        col.IsVisible = True
                    End If
                Next
            Next
        End If
        isInsideLoadData = False
    End Sub


    Private Sub frmItemTaxRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBlankGrid()
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmItemTaxRate, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return False
                End If
            End If

            Dim obj As New clsItemTaxRate()
            obj.ITR_CODE = fndCode.Value
            obj.Description = txtDesc.Text
            obj.APPLICABLE_FROM = dtpToDate.Value
            obj.POSTED = "N"
            If (obj.SaveData(obj, isNewEntry)) Then
                LoadData(obj.ITR_CODE, NavigatorType.Current)
                Return True
            End If
        End If
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        'isNewEntry = False
        'Dim obj As New clsLockMPPaymentCycle()
        'obj = clsLockMPPaymentCycle.GetData(strCode, NavTyep)
        'If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LOCK_CODE) > 0) Then
        '    funReset()
        '    isNewEntry = False
        '    btnSave.Text = "Update"

        '    If obj.POSTED = "Y" Then
        '        btnSave.Enabled = False
        '        btnPost.Enabled = False
        '        btnDelete.Enabled = False
        '        UsLock1.Status = ERPTransactionStatus.Approved
        '    Else
        '        btnSave.Enabled = True
        '        btnDelete.Enabled = True
        '        btnPost.Enabled = True
        '        UsLock1.Status = ERPTransactionStatus.Pending
        '    End If
        '    txtCode.Value = obj.LOCK_CODE
        '    txtCode.MyReadOnly = True
        '    fndLoc.Value = obj.MCC_Code
        '    txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from tspl_location_master where tspl_location_master.Location_Code='" + obj.MCC_Code + "' "))
        '    txtDescription.Text = obj.Description
        '    dtpFromDate.Value = obj.FROM_DATE
        '    dtpToDate.Value = obj.TO_DATE
        'End If

    End Sub

    Function AllowToSave() As Boolean

        ' If isNewEntry = False Then
        '     Dim QryStr As String = "select POSTED from TSPL_ITR_HEAD where ITR_CODE = '" & fndCode.Value & "' "
        '     Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
        '     If chkpost = "1" Then
        '         clsCommon.MyMessageBoxShow("Transection already posted")
        '         Return False
        '     End If
        ' End If

        'If clsCommon.myLen(dtpToDate.Value.ToLongDateString()) <= 0 Then
        '     myMessages.blankValue("To Date")
        '     dtpToDate.Focus()
        '     Return False
        ' ElseIf clsCommon.myLen(dtpFromDate.Value.ToLongDateString()) <= 0 Then
        '     myMessages.blankValue("From Date")
        '     dtpFromDate.Focus()
        '     Return False
        ' End If
        ' Dim strchk As String = "select LOCK_CODE from TSPL_LOCK_MP_PC where ( FROM_DATE  between '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' or TO_DATE  between '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' ) and LOCK_CODE <> '" + txtCode.Value + "' and MCC_Code='" & fndLoc.Value & "' "
        ' Dim chkPayPeriod As String = clsDBFuncationality.getSingleValue(strchk)
        ' If clsCommon.myLen(chkPayPeriod) > 0 Then
        '     clsCommon.MyMessageBoxShow("From or To date overlapped with Lock Code " + chkPayPeriod + " . Overlapping Lock periods can not be created.")
        '     Return False
        ' End If
       
        Return True
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class
