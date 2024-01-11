Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
Imports XpertERPEngine


Public Class frmAssetRequisition
    Inherits FrmMainTranScreen
    Dim arrDBName As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim dr As DataTable
    Private isNewEntry As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim colAssetCode As String = "AssetCode"
    Dim colAssetDesc As String = "AssetDesc"
    Dim colAssetQty As String = "AssetQty"
    Dim colAssetSize As String = "AssetSize"
    Dim colOutletStatus As String = "OutletStatus"
    Dim obj As New clsAssetRequisition
    Private ObjList As New List(Of clsAssetRequisition)

    'This Cunstructer is used to send usercode and compcode data in table.
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Sub LoadGridColumns()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim Status As New ArrayList
        Status.Add("Mixed")
        Status.Add("PCI Exclusive")
        Status.Add("CCX Exclusive")
        Status.Add("Other")

        Dim AssetCode As New GridViewTextBoxColumn
        Dim AssetDesc As New GridViewTextBoxColumn
        Dim AssetQty As New GridViewDecimalColumn
        Dim AssetSize As New GridViewTextBoxColumn
        Dim OutletStatus As New GridViewComboBoxColumn

        AssetCode.FormatString = ""
        AssetCode.HeaderText = "Asset Code"
        AssetCode.Name = colAssetCode
        AssetCode.Width = 100
        'ItemCategoryCode.ReadOnly = True
        AssetCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(AssetCode)

        AssetDesc.FormatString = ""
        AssetDesc.HeaderText = "Description"
        AssetDesc.Name = colAssetDesc
        AssetDesc.Width = 100
        'AssetDesc.ReadOnly = True
        AssetDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(AssetDesc)


        AssetQty.FormatString = ""
        AssetQty.HeaderText = "Quantity"
        AssetQty.Name = colAssetQty
        AssetQty.Width = 100
        AssetQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(AssetQty)

        AssetSize.FormatString = ""
        AssetSize.HeaderText = "Size"
        AssetSize.Name = colAssetSize
        AssetSize.Width = 100
        AssetSize.ReadOnly = True
        AssetSize.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(AssetSize)

        OutletStatus.DataSource = Status
        OutletStatus.FormatString = ""
        OutletStatus.HeaderText = "Outlet Status"
        OutletStatus.Name = colOutletStatus
        OutletStatus.Width = 100
        OutletStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(OutletStatus)

    End Sub
    Public Sub SetLength()
        txtCode.MyMaxLength = 12
        txtRemarks.MaxLength = 50
        txtCustname.MaxLength = 100
        'txtVisiSize.MaxLength = 50
        'rdtxtchassisnum.MaxLength = 12
        'rdtxtmake.MaxLength = 50
        'txtModelNo.MaxLength = 12
    End Sub
    Private Sub frmAssetRequisition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")


        LoadGridColumns()
        Me.ddlMoveType.DataSource = GetcboMoveTypeDataTable()
        Me.ddlMoveType.DisplayMember = "Name"
        Me.ddlMoveType.ValueMember = "Code"
        funReset()

    End Sub
    Public Function GetcboMoveTypeDataTable() As DataTable
        Dim DT_Cbo As DataTable = New DataTable
        DT_Cbo.Columns.Add("Code", GetType(String))
        DT_Cbo.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Cbo.NewRow()
        DR("Name") = "Installation Only"
        DR("Code") = "Installation Only"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Pull Out and Installation"
        DR("Code") = "Pull Out and Installation"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Pull Out Only"
        DR("Code") = "Pull Out Only"
        DT_Cbo.Rows.Add(DR)

        DT_Cbo.AcceptChanges()
        Return DT_Cbo
    End Function

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAssetRequisition)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Save() = True Then
            clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        '===============Preeti==================================
        If AllowFutureDateTransaction(dtpReqDate.Value, Nothing) = False Then
            dtpReqDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(txtCode.Value) = 0 Then
            'myMessages.blankValue("Code")
            'Return False
        ElseIf clsCommon.myLen(txtRemarks.Text) = 0 Then
            myMessages.blankValue("Remarks")
            Return False
        ElseIf Me.ddlMoveType.SelectedIndex = -1 Then
            myMessages.blankValue("Move Type")
            Return False
        ElseIf clsCommon.myLen(fndLocationCode.Value) = 0 Then
            myMessages.blankValue("Location Code")
            Return False
        ElseIf clsCommon.myLen(fndcustomerCode.Value) = 0 Then
            myMessages.blankValue("Customer Code")
            Return False
        End If
        Return True
    End Function
    Public Function Save() As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New clsAssetRequisition
                obj.ASSET_REQ_CODE = clsCommon.myCstr(txtCode.Value)
                obj.REQ_DATE = clsCommon.GetPrintDate(dtpReqDate.Value, "dd/MMM/yyyy")
                obj.LOCATION_CODE = clsCommon.myCstr(fndLocationCode.Value)
                obj.Customer_Code = clsCommon.myCstr(fndcustomerCode.Value)
                obj.CREATED_BY = objCommonVar.CurrentUserCode
                obj.MOVE_TYPE = Me.ddlMoveType.Text
                obj.REMARKS = txtRemarks.Text
                obj.LOCATION_ADDRESS = txtLocationAddress.Text
                obj.LOCATION_CHANNEL = txtLocChannel.Text
                obj.LOCATION_CONTACT_PERSON = txtLocContactPerson.Text
                obj.LOCATION_TELEPHONE = txtLocTelephone.Text
                obj.LOCATION_TOWN = txtLocTown.Text
                Dim obj1 As clsAssetRequisition
                ObjList = New List(Of clsAssetRequisition)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAssetCode).Value)) > 0 Then
                        obj1 = New clsAssetRequisition()
                        obj1.Asset_Code = grow.Cells(colAssetCode).Value
                        obj1.ASSET_DESC = grow.Cells(colAssetDesc).Value
                        obj1.QUANTITY = clsCommon.myCdbl(grow.Cells(colAssetQty).Value)
                        obj1.ASSET_SIZE = clsCommon.myCstr(grow.Cells(colAssetSize).Value)
                        obj1.OUTLET_STATUS = clsCommon.myCstr(grow.Cells(colOutletStatus).Value)
                        ObjList.Add(obj1)
                    End If
                Next
                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))
                LoadData(obj.ASSET_REQ_CODE, NavigatorType.Current)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsAssetRequisition.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        obj = clsAssetRequisition.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ASSET_REQ_CODE) > 0) Then

            isNewEntry = False
            btnSave.Text = "Update"
            If obj.POSTED Then
                btnSave.Enabled = False
                'btnPost.Enabled = False
                btnDelete.Enabled = False
                'UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                'btnPost.Enabled = True
                'UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.ASSET_REQ_CODE
            Me.txtRemarks.Text = clsCommon.myCstr(obj.REMARKS)
            Me.dtpReqDate.Value = obj.REQ_DATE

            txtRemarks.Text = obj.REMARKS
            Me.fndLocationCode.Value = obj.LOCATION_CODE
            Me.txtLocationAddress.Text = obj.LOCATION_ADDRESS
            Me.txtLocationDesc.Text = obj.LOCATION_Desc
            Me.txtLocChannel.Text = obj.LOCATION_CHANNEL
            Me.txtLocContactPerson.Text = obj.LOCATION_CONTACT_PERSON
            Me.txtLocTelephone.Text = obj.LOCATION_TELEPHONE
            Me.txtLocTown.Text = obj.LOCATION_TOWN

            Me.fndcustomerCode.Value = obj.Customer_Code
            Me.txtCustAddress.Text = obj.CUST_ADDRESS
            Me.txtCustChannel.Text = obj.CUST_CHANNEL
            Me.txtCustContactPerson.Text = obj.CUST_CONTACT_PERSON
            Me.txtCustname.Text = obj.Customer_Name
            Me.txtCustTelephone.Text = obj.CUST_TELEPHONE
            Me.txtCustTown.Text = obj.CUST_TOWN
            Me.ddlMoveType.SelectedValue = obj.MOVE_TYPE


            If (clsAssetRequisition.ObjList IsNot Nothing AndAlso clsAssetRequisition.ObjList.Count > 0) Then
                For Each obj As clsAssetRequisition In clsAssetRequisition.ObjList
                    gv1.Rows.AddNew()


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = obj.Asset_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetDesc).Value = obj.ASSET_DESC
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetQty).Value = obj.QUANTITY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetSize).Value = obj.ASSET_SIZE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOutletStatus).Value = obj.OUTLET_STATUS

                Next
            Else
                gv1.Rows.AddNew()
            End If
        End If

    End Sub


    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        Me.dtpReqDate.Value = clsCommon.GETSERVERDATE
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        ddlMoveType.SelectedIndex = -1
        txtRemarks.Text = ""
        fndLocationCode.Value = Nothing
        txtLocationDesc.Text = ""
        txtLocationAddress.Text = ""
        txtLocTown.Text = ""
        txtLocChannel.Text = ""
        txtLocContactPerson.Text = ""
        txtLocTelephone.Text = ""

        fndcustomerCode.Value = Nothing
        txtCustname.Text = ""
        txtCustAddress.Text = ""
        txtCustTown.Text = ""
        txtCustChannel.Text = ""
        txtCustContactPerson.Text = ""
        txtCustTelephone.Text = ""
        gv1.Rows.Clear()
        gv1.Rows.AddNew()

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub frmAssetRequisition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub fndcustomerid__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustomerCode._MYValidating
        'If isButtonClicked Then
        Dim qry As String = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name],M.Add1 AS [Address], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account"
        fndcustomerCode.Value = clsCommon.ShowSelectForm("POProjctfND", qry, "Code", "", fndcustomerCode.Value, "", isButtonClicked)
        Dim objCust As clsCustomerInfo
        objCust = clsCustomerInfo.GetData(clsCommon.myCstr(fndcustomerCode.Value), Nothing)
        If Not objCust Is Nothing Then
            Me.txtCustname.Text = objCust.Customer_Name
            Me.txtCustAddress.Text = objCust.Add1
            Me.txtCustTown.Text = objCust.City_Code
            Me.txtCustChannel.Text = "" '' channel code does not exist in class but exist in database
            Me.txtCustContactPerson.Text = objCust.Contact_Person_Name
            Me.txtCustTelephone.Text = objCust.Contact_Person_Phone

        End If



    End Sub

    Private Sub fndLocationCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocationCode.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, fndLocationCode.Value, "Code", isButtonClicked)
            txtLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocationCode.Value + "'"))

            Dim dtLoc As DataTable
            Dim strqLoc As String
            strqLoc = "select Location_Code,Location_Desc,Add1,City_Code,Telphone from TSPL_LOCATION_MASTER where location_code='" & clsCommon.myCstr(Me.fndLocationCode.Value) & "'"
            dtLoc = clsDBFuncationality.GetDataTable(strqLoc)
            If dtLoc.Rows.Count > 0 Then
                Me.txtLocationAddress.Text = clsCommon.myCstr(dtLoc.Rows(0).Item("Add1"))
                txtLocTown.Text = clsCommon.myCstr(dtLoc.Rows(0).Item("City_Code"))
                txtLocTelephone.Text = clsCommon.myCstr(dtLoc.Rows(0).Item("Telphone"))

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEndEdit
        If gv1.CurrentRow Is Nothing Then
            Exit Sub
        End If


        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv1.Columns(colAssetCode) Then
                Dim strq As String = ""
                strq = "select TSPL_ACQUISITION_DETAIL.Asset_Code as Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Asset_Specification," & _
                    " TSPL_ACQUISITION_HEAD.Acquisition_Code, Convert(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as Date from TSPL_ACQUISITION_DETAIL" & _
                    " inner join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 " & _
                " UNION ALL" & _
                " Select Asset_No, VisiMake, ISNULL(Model_No,'')+' - '+ISNULL(Visi_Size,'') as Specification, Visi_Id, Created_Date from TSPL_VISI_MASTER "
                gv1.CurrentRow.Cells(colAssetCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value))
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                If (clsCommon.myLen(txtCode.Value) > 0) Then
                    gv1.Rows.AddNew()
                    'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_ASSET_REQUISITION where ASSET_REQ_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select TSPL_ASSET_REQUISITION.ASSET_REQ_CODE,TSPL_ASSET_REQUISITION.REQ_DATE,TSPL_ASSET_REQUISITION.MOVE_TYPE,TSPL_ASSET_REQUISITION.REMARKS from TSPL_ASSET_REQUISITION "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_ASSET_REQUISITION", qry, "ASSET_REQ_CODE", "", txtCode.Value, "TSPL_ASSET_REQUISITION.REQ_DATE desc", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub
End Class
