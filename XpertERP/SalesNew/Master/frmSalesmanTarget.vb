Imports common
Imports System.Data.SqlClient

Public Class FrmSalesmanTarget
    Inherits FrmMainTranScreen
    Dim Qry As String = ""
    Dim dt As DataTable
    Dim ArrItem As New List(Of String)
    Dim IsNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Public IsFormLoad As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    Const colQty As String = "Quantity"
    Const colRate As String = "Rate"
    Const colAmt As String = "Amount"
    Public Code As String = ""

    Private Sub FrmSalesmanTarget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData(txtTargetNo.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub Frm_User_Customer_Rate_Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dtpTargetDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        Reset()
        IsFormLoad = True
        LoadType()
        IsFormLoad = False
        txtAmount.ReadOnly = True
        If clsCommon.myLen(Code) > 0 Then
            LoadData(Code, NavigatorType.Current)
        End If
    End Sub

    Private Sub Reset()
        btnSave.Text = "Save"
        IsNewEntry = True
        LoadBlankGrid()
        txtTargetNo.Value = ""
        txtSalesmanCode.Value = ""
        lblSalesmanName.Text = ""
        txtAmount.Text = "0"
        ArrItem.Clear()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalesmanTarget)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadBlankGrid()
        dgvItem.Rows.Clear()
        dgvItem.Columns.Clear()

        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 71
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvItem.MasterTemplate.Columns.Add(LineNo)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 151
        ItemCode.ReadOnly = False
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvItem.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Description"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 351
        ItemDesc.ReadOnly = True
        ItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvItem.MasterTemplate.Columns.Add(ItemDesc)

        Dim Quantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        Quantity.FormatString = ""
        Quantity.HeaderText = "Quantity"
        Quantity.Name = colQty
        Quantity.Width = 100
        Quantity.ReadOnly = False
        Quantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvItem.MasterTemplate.Columns.Add(Quantity)

        Dim Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        Rate.FormatString = ""
        Rate.HeaderText = "Rate"
        Rate.Name = colRate
        Rate.Width = 100
        Rate.ReadOnly = True
        Quantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvItem.MasterTemplate.Columns.Add(Rate)

        Dim Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        Amount.FormatString = ""
        Amount.HeaderText = "Amount"
        Amount.Name = colAmt
        Amount.Width = 150
        Amount.ReadOnly = True
        Quantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvItem.MasterTemplate.Columns.Add(Amount)


        dgvItem.AllowDeleteRow = True
        dgvItem.Rows.AddNew()
        dgvItem.ShowGroupPanel = False
        dgvItem.AllowColumnReorder = False
        dgvItem.AllowRowReorder = False
        dgvItem.EnableSorting = False
        dgvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvItem.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub LoadType()
        Dim dtType As New DataTable
        dtType.Columns.Add("Code", GetType(String))
        dtType.Columns.Add("Desc", GetType(String))
        dtType.Rows.Add("I", "Item Wise")
        dtType.Rows.Add("A", "Amount Wise")
        ddlType.DataSource = dtType
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Desc"
    End Sub

    Private Sub tstSalesmanCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesmanCode._MYValidating
        Qry = "Select EMP_CODE as Code, Emp_Name as Description from TSPL_EMPLOYEE_MASTER"
        txtSalesmanCode.Value = clsCommon.ShowSelectForm("SalesmanFinder@Target", Qry, "Code", " Emp_Type ='Service Dealer' ", txtSalesmanCode.Value, "Code", isButtonClicked)
        lblSalesmanName.Text = GetSalesmanName(txtSalesmanCode.Value)
    End Sub

    Private Function GetSalesmanName(ByVal SalesmanCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + SalesmanCode + "'"))
    End Function

    Private Function GetItemDesc(ByVal ItemCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER WHERE Item_Code='" + ItemCode + "'"))
    End Function

    

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsSalesmanTargetHeader()
                obj.Code = clsCommon.myCstr(txtTargetNo.Value)
                obj.MonthYear = dtpTargetDate.Value
                obj.Target_Type = clsCommon.myCstr(ddlType.SelectedValue)
                obj.Salesman_Code = clsCommon.myCstr(txtSalesmanCode.Value)
                obj.Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.Arr = New List(Of clsSalesmanTargetDetail)

                If ddlType.SelectedValue = "I" Then
                    For Each grow As GridViewRowInfo In dgvItem.Rows
                        If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                            Dim objTr As New clsSalesmanTargetDetail()
                            objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            objTr.Qty = clsCommon.myCstr(grow.Cells(colQty).Value)
                            objTr.Cost = clsCommon.myCstr(grow.Cells(colRate).Value)
                            objTr.Amount = clsCommon.myCstr(grow.Cells(colAmt).Value)
                            obj.Arr.Add(objTr)
                        End If
                    Next
                End If

                If (obj.SaveData(obj, IsNewEntry)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If ddlType.SelectedValue = "I" Then
            If dgvItem.Rows.Count > 0 Then
                For Each grow As GridViewRowInfo In dgvItem.Rows
                    If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                        If clsCommon.myCdbl(grow.Cells(colQty).Value) <= 0 Then
                            clsCommon.MyMessageBoxShow("Please enter Quantity against Item " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " at line " + clsCommon.myCstr(grow.Cells(colLineNo).Value) + "")
                            Return False
                        End If
                        '===========================
                        Dim qry As String = "select count(*)  from TSPL_SD_SALESMAN_TARGET_HEADER "
                        qry += " left join TSPL_SD_SALESMAN_TARGET_DETAIL  on TSPL_SD_SALESMAN_TARGET_HEADER.code=TSPL_SD_SALESMAN_TARGET_DETAIL.code where Salesman_Code ='" + txtSalesmanCode.Value + "' "
                        qry += " and year(MonthYear) ='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpTargetDate.Text), "yyyy") + "' and month(MonthYear) ='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpTargetDate.Text), "MM") + "' "

                        qry += " and Item_Code ='" + grow.Cells(colItemCode).Value + "'  and TSPL_SD_SALESMAN_TARGET_HEADER.Code <>'" + txtTargetNo.Value + "' "
                        Dim Count As Integer = clsDBFuncationality.getSingleValue(qry)
                        If Count > 0 Then
                            clsCommon.MyMessageBoxShow("Already this item '" + grow.Cells(colItemCode).Value + "' mapped")
                            Return False
                        End If
                        '============================
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow("Please enter atleast single Item.")
                Return False
            End If
        ElseIf ddlType.SelectedValue = "A" Then
            If clsCommon.myCdbl(txtAmount.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please enter Amount.")
                txtAmount.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Reset()
            Dim obj As New clsSalesmanTargetHeader()
            obj = clsSalesmanTargetHeader.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtTargetNo.Value = obj.Code
                dtpTargetDate.Value = obj.MonthYear
                ddlType.SelectedValue = obj.Target_Type
                txtSalesmanCode.Value = obj.Salesman_Code
                lblSalesmanName.Text = GetSalesmanName(obj.Salesman_Code)
                txtAmount.Text = clsCommon.myCstr(obj.Amount)
                If obj.Target_Type = "I" Then
                    dgvItem.Visible = True
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        Dim LineNo As Integer = 0
                        For Each objTr As clsSalesmanTargetDetail In obj.Arr
                            LineNo += 1
                            dgvItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(LineNo)
                            dgvItem.CurrentRow.Cells(colItemCode).Value = objTr.Item_Code
                            dgvItem.CurrentRow.Cells(colItemDesc).Value = GetItemDesc(objTr.Item_Code)
                            dgvItem.CurrentRow.Cells(colQty).Value = objTr.Qty
                            dgvItem.CurrentRow.Cells(colRate).Value = objTr.Cost
                            dgvItem.CurrentRow.Cells(colAmt).Value = objTr.Amount
                            dgvItem.Rows.AddNew()
                            ArrItem.Add(objTr.Item_Code)
                        Next

                    End If
                Else
                    dgvItem.Visible = False
                End If
                IsNewEntry = False
                btnSave.Text = "Update"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvItem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvItem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                Dim Amount As Double = 0
                If e.Column Is dgvItem.Columns(colItemCode) Then
                    OpenItem(False)
                    dgvItem.CurrentRow.Cells(colRate).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Cost from TSPL_ITEM_MASTER WHERE Item_Code='" + clsCommon.myCstr(dgvItem.CurrentRow.Cells(colItemCode).Value) + "'"))
                ElseIf e.Column Is dgvItem.Columns(colQty) Then
                    dgvItem.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(dgvItem.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(dgvItem.CurrentRow.Cells(colRate).Value)
                End If
                ArrItem.Clear()
                For Each grow As GridViewRowInfo In dgvItem.Rows
                    Amount += clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                        ArrItem.Add(clsCommon.myCstr(grow.Cells(colItemCode).Value))
                    End If
                Next
                txtAmount.Text = clsCommon.myCstr(Amount)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenItem(ByVal isButtonClick As Boolean)

        Qry = " Select Item_Code as [Code], Item_Desc as [Description] from TSPL_ITEM_MASTER "
        Dim Whrcls As String = "Item_Type='F'"
        'If ArrItem.Count > 0 Then
        '    Whrcls += " AND Item_Code Not in (" + clsCommon.GetMulcallString(ArrItem) + ")"
        'End If
        dgvItem.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder(Whrcls, clsCommon.myCstr(dgvItem.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        dgvItem.CurrentRow.Cells(colItemDesc).Value = GetItemDesc(dgvItem.CurrentRow.Cells(colItemCode).Value)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(txtTargetNo.Value)
    End Sub

    Private Sub DeleteData(ByVal TargetNo As String)
        Try
            If clsSalesmanTargetHeader.DeleteData(TargetNo) Then
                Reset()
                clsCommon.MyMessageBoxShow("Data deleted successfully.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles dgvItem.CurrentColumnChanged
        If dgvItem.RowCount > 0 Then
            Dim intCurrRow As Integer = dgvItem.CurrentRow.Index
            dgvItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = dgvItem.Rows.Count - 1 Then
                dgvItem.Rows.AddNew()
                dgvItem.CurrentRow = dgvItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtTargetNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTargetNo._MYValidating
        'Dim qry As String = "Select Code, Salesman, Type, Month+' - '+CONVERT(VARCHAR,Year,103) as MonthYear from "
        'qry += " (Select Code, TSPL_EMPLOYEE_MASTER.Emp_Name as [Salesman], Case When DATEPART(MM ,MonthYear)=1 Then 'JAN' When DATEPART(MM ,MonthYear)=2 Then 'FEB' When DATEPART(MM ,MonthYear)=3 Then 'MAR' When DATEPART(MM ,MonthYear)=4 Then 'APR' When DATEPART(MM ,MonthYear)=5 Then 'MAY' When DATEPART(MM ,MonthYear)=6 Then 'JUN' When DATEPART(MM ,MonthYear)=7 Then 'JUL' When DATEPART(MM ,MonthYear)=8 Then 'AUG' When DATEPART(MM ,MonthYear)=9 Then 'SEP' When DATEPART(MM ,MonthYear)=10 Then 'OCT' When DATEPART(MM ,MonthYear)=11 Then 'NOV' When DATEPART(MM ,MonthYear)=12 Then 'DEC' End as [Month],  DATEPART(YYYY, MonthYear ) as Year, Case When Target_Type='A' Then 'Amount Wise' Else 'Item Wise' END as [Type] from TSPL_SD_SALESMAN_TARGET_HEADER Left Outer Join TSPL_EMPLOYEE_MASTER ON TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE) XXX "
        'LoadData(clsCommon.ShowSelectForm("ShipmentCofnd", qry, "Code", "", txtTargetNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        LoadData(clsSalesmanTargetHeader.getFinder("", txtTargetNo.Value, isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtTargetNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtTargetNo._MYNavigator
        Try
            LoadData(txtTargetNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub ddlType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlType.SelectedValueChanged
        If Not IsFormLoad Then
            dgvItem.Visible = False
            txtAmount.Text = "0"
            If clsCommon.CompairString(clsCommon.myCstr(ddlType.SelectedValue), "I") = CompairStringResult.Equal Then
                dgvItem.Visible = True
                LoadBlankGrid()
                txtAmount.ReadOnly = True
            Else
                txtAmount.ReadOnly = False
            End If
        End If
    End Sub
End Class
