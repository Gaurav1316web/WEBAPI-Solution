'--preeti gupta-ticket no.[BM00000003133]
Imports common

Public Class frmCustomerTargetFixing
    Inherits FrmMainTranScreen
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colTarget As String = "Item_Target"
    Const colIncentive As String = "Item_Incentive"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True

    Dim obj As New clsCustomerTargetFixing
    Private ObjList As New List(Of clsCustomerTargetFixing)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        gvItems.Rows.Clear()
        gvItems.Columns.Clear()


        Dim ItemCode As New GridViewTextBoxColumn
        Dim ItemName As New GridViewTextBoxColumn
        Dim target As New GridViewDecimalColumn
        Dim incentive As New GridViewDecimalColumn


        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        'ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItems.Columns.Add(ItemCode)

        ItemName.FormatString = ""
        ItemName.HeaderText = "Item Name"
        ItemName.Name = colItemName
        ItemName.Width = 200
        ItemName.ReadOnly = True
        ItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItems.Columns.Add(ItemName)

        target.FormatString = ""
        target.HeaderText = "Target"
        target.Name = colTarget
        target.Width = 100
        'target.ReadOnly = True
        target.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItems.Columns.Add(target)


        incentive.FormatString = ""
        incentive.HeaderText = "Incentive"
        incentive.Name = colIncentive
        incentive.Width = 100
        incentive.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItems.Columns.Add(incentive)

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")

        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerTargetFixing)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtCustomerCode.Value = Nothing
        txtDescription.Text = ""
        dtpMonthYear.Value = Today
        lblCustomerName.Text = ""

        lblRemarks.Text = ""
        Me.txtIncentive.Text = ""
        Me.txtTarget.Text = ""
        Me.rdbItemwise.IsChecked = True

        btnsave.Text = "Save"

        btnsave.Enabled = True
        btndelete.Enabled = True

        Me.gvItems.Rows.Clear()
        Me.gvItems.Rows.AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
       
        obj = clsCustomerTargetFixing.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TARGET_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.TARGET_CODE

           
            txtCustomerCode.Value = clsCommon.myCstr(obj.CUST_CODE)
            lblCustomerName.Text = clsCommon.myCstr(obj.CUST_NAME)
            txtDescription.Text = obj.DESCRIPTION
            dtpMonthYear.Value = obj.MONTH_YEAR
            txtTarget.Text = obj.TARGET
            txtIncentive.Text = obj.INCENTIVE
            If obj.TARGET_TYPE = "ITEM" Then
                Me.rdbItemwise.IsChecked = True
            ElseIf obj.TARGET_TYPE = "PACK" Then
                Me.rdbPackwise.IsChecked = True
            Else

                Me.rdbFlavourwise.IsChecked = True
            End If

            If (clsCustomerTargetFixing.ObjList IsNot Nothing AndAlso clsCustomerTargetFixing.ObjList.Count > 0) Then
                For Each obj As clsCustomerTargetFixing In clsCustomerTargetFixing.ObjList
                    gvItems.Rows.AddNew()

                    gvItems.Rows(gvItems.Rows.Count - 1).Cells(colItemCode).Value = obj.ITEM_CODE
                    gvItems.Rows(gvItems.Rows.Count - 1).Cells(colItemName).Value = obj.ITEM_NAME
                    gvItems.Rows(gvItems.Rows.Count - 1).Cells(colTarget).Value = obj.ITEM_TARGET
                    gvItems.Rows(gvItems.Rows.Count - 1).Cells(colIncentive).Value = obj.ITEM_INCENTIVE


                Next
            Else
                gvItems.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_SALES_CUSTOMER_TARGET where TARGET_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select TARGET_CODE as Code, CUST_CODE,DESCRIPTION from TSPL_SALES_CUSTOMER_TARGET "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_SALES_CUSTOMER_TARGET", qry, "Code", "", txtCode.Value, "TARGET_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As clsCustomerTargetFixing = Nothing
            ObjList = New List(Of clsCustomerTargetFixing)
            For Each grow As GridViewRowInfo In gvItems.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                    obj = New clsCustomerTargetFixing()

                    obj.TARGET_CODE = txtCode.Value

                    obj.CUST_CODE = clsCommon.myCstr(txtCustomerCode.Value)
                    obj.CUST_NAME = clsCommon.myCstr(lblCustomerName.Text)
                    obj.MONTH_NO = Me.dtpMonthYear.Value.Month
                    obj.MONTH_NAME = MonthName(dtpMonthYear.Value.Month)
                    obj.YEAR_NO = Me.dtpMonthYear.Value.Year
                    obj.MONTH_YEAR = clsCommon.myCDate(1 & "/" & MonthName(dtpMonthYear.Value.Month) & "/" & dtpMonthYear.Value.Year)
                    obj.TARGET = clsCommon.myCdbl(txtTarget.Text)
                    obj.INCENTIVE = clsCommon.myCdbl(txtIncentive.Text)
                    obj.DESCRIPTION = Me.txtDescription.Text
                    Dim TARGET_TYPE As String
                    If Me.rdbItemwise.IsChecked = True Then
                        TARGET_TYPE = "ITEM"
                    ElseIf Me.rdbPackwise.IsChecked = True Then
                        TARGET_TYPE = "PACK"
                    Else
                        TARGET_TYPE = "FLVR"
                    End If
                    obj.TARGET_TYPE = TARGET_TYPE
                    obj.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj.ITEM_TARGET = clsCommon.myCdbl(grow.Cells(colTarget).Value)
                    obj.ITEM_INCENTIVE = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
                    
                    ObjList.Add(obj)
                End If
            Next
            If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                LoadData(obj.TARGET_CODE, NavigatorType.Current)
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Return True
            End If
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_SALES_CUSTOMER_TARGET where TARGET_CODE = '" + txtCode.Value + "' "

        End If
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        End If
        
        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvItems.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                ii += 1
                If clsCommon.myCdbl(grow.Cells(colIncentive).Value) = 0 Or clsCommon.myCdbl(grow.Cells(colTarget).Value) = 0 Then
                    Return False
                End If
                ObjList.Add(obj)
            End If
        Next
        If ObjList Is Nothing Then
            myMessages.blankValue("Item Code.")
            Return False
        End If
        Return True
    End Function

    Private Sub gvItems_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItems.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvItems.Columns(colItemCode) Then
                'Dim strq As String

                'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gvItems.CurrentRow.Cells(colItemCode).Value), "F", False)
                Dim strCode As String = ""
                Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category] from  TSPL_ITEM_MASTER"
                qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
                qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

                Dim WhrCls As String = " ITEM_CODE NOT IN (SELECT DISTINCT T1.ITEM_CODE FROM TSPL_SALES_CUSTOMER_TARGET_DETAIL T1 INNER JOIN TSPL_SALES_CUSTOMER_TARGET T2 ON"
                WhrCls += " T1.TARGET_CODE=T2.TARGET_CODE WHERE T2.CUST_CODE='" & Me.txtCustomerCode.Value & "' AND T2.MONTH_NO='" & Me.dtpMonthYear.Value.Month & "' AND  T2.YEAR_NO='" & Me.dtpMonthYear.Value.Year & "') AND Item_Type='F' "

                strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, clsCommon.myCstr(gvItems.CurrentRow.Cells(colItemCode).Value), "Code", False)
                If clsCommon.myLen(strCode) > 0 Then
                    gvItems.CurrentRow.Cells(colItemCode).Value = strCode
                    gvItems.CurrentRow.Cells(colItemName).Value = clsItemMaster.FinderForItem(strCode, "F", False).Item_Desc
                    If clsCommon.myCdbl(txtIncentive.Text) > 0 Then
                        gvItems.CurrentRow.Cells(colIncentive).Value = clsCommon.myCdbl(txtIncentive.Text)

                    End If



                End If

            End If

            isCellValueChangedOpen = False
        End If
        'Me.txtTarget.Text = calculateTarget()
    End Sub
    Function calculateTarget() As Double
        Dim target As Double
        For intloop As Integer = 0 To Me.gvItems.Rows.Count - 1
            If clsCommon.myLen(gvItems.Rows(intloop).Cells(colItemCode).Value) > 0 Then
                target = target + gvItems.Rows(intloop).Cells(colTarget).Value
            End If
        Next
        Return target
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCustomerTargetFixing.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCustCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCustomerCode._MYValidating
        Dim qry As String = "SELECT CUST_CODE as Code,CUSTOMER_NAME as Name FROM TSPL_CUSTOMER_MASTER "
        txtCustomerCode.Value = clsCommon.ShowSelectForm("TSPL_CUSTOMER_MASTER", qry, "Code", "", txtCustomerCode.Value, "", isButtonClicked)
        Dim clscust As clsCustomerMaster

        clscust = clsCustomerMaster.GetData(txtCustomerCode.Value, Nothing)
        lblCustomerName.Text = clscust.Customer_Name

    End Sub



    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    Private Sub gvItems_CurrentCellChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentCellChangedEventArgs) Handles gvItems.CurrentCellChanged
        Me.txtTarget.Text = calculateTarget()
    End Sub
End Class