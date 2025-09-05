Imports common
Public Class frmCustomerTender
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colRatePerLtr As String = "colRatePerLtr"
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub LoadBlankGrid()
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
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Customer Code"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 100
        repoCustCode.ReadOnly = True
        repoCustCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Customer Name"
        repoCustName.Name = colCustName
        repoCustName.Width = 100
        repoCustName.ReadOnly = True
        repoCustName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCustName)
        Dim repoRatePerLtr As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRatePerLtr = New GridViewDecimalColumn()
        repoRatePerLtr.FormatString = "{0:n2}"
        repoRatePerLtr.HeaderText = "Rate Per Ltr"
        repoRatePerLtr.Name = colRatePerLtr
        repoRatePerLtr.Width = 50
        repoRatePerLtr.Minimum = 0
        repoRatePerLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRatePerLtr)
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns()
    End Sub
    Private Sub frmCustomerTender_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CreateTable()
        AddNew()
    End Sub
    Private Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        txtItemCode.Value = ""
        lblItemDesc.Text = ""
        txtCustomerGroup.Value = ""
        lblCustomerGroupDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtDate.Value
        txtToDate.Value = txtDate.Value
        txtTotalQty.Text = 0
        txtTolerance.Text = 0
        chkTaxInclusive.Checked = False
        chkTPTInclude.Checked = False
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        btnAmendment.Enabled = False
        LoadBlankGrid()
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("From_Date", "datetime not NULL")
        coll.Add("To_Date", "datetime not NULL")
        coll.Add("Location_Code", "Varchar(12) NOt NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Item_Code", "Varchar(50) not null references TSPL_Item_MASTER(Item_Code)")
        coll.Add("Customer_Group", "Varchar(50) Not null")
        coll.Add("Total_Qty", "decimal(18,2) Not null")
        coll.Add("Tolerance", "decimal(18,2) null")
        coll.Add("Inclusive_Tax", "Integer not null default 0")
        coll.Add("Inclusive_TPT", "Integer not null default 0")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER", coll, "", True, False, "", "Document_Code", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL REFERENCES TSPL_CUSTOMER_TENDER(Document_Code)")
        coll.Add("Cust_Code", "Varchar(12) not null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Item_Rate", "decimal(18,2) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER_DETAIL", coll, "", True, False, "TSPL_CUSTOMER_TENDER", "Document_Code", "", True)
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Category not in('MCC')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("CUST-TENDLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim strQry As String = ""
            strwherecls = Xtra.CustomerPermission()
            strQry = "select count(*) from TSPL_CUSTOMER_TENDER where Document_Code='" & txtDocNo.Value & "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            Dim qry As String = "select TSPL_CUSTOMER_TENDER.Document_Code as Code,TSPL_CUSTOMER_TENDER.Location_Code as Location_Code, 
TSPL_CUSTOMER_TENDER.Item_Code as [Item Code]
from TSPL_CUSTOMER_TENDER "
            Dim whrClas As String = ""
            LoadData(clsCommon.ShowSelectForm("Cust_TendDocfnd", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "TSPL_CUSTOMER_TENDER.Document_Date"), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Try
            Dim qry As String = ""
            Dim WhrCls As String = ""
            qry = " select Item_code as Code,Item_Desc,Short_Description  from TSPL_ITEM_MASTER "
            WhrCls = " active=1 and TypeOfItm in('M','P','O') "
            txtItemCode.Value = clsCommon.ShowSelectForm("CUST-TENDItemFndr", qry, "Code", WhrCls, txtItemCode.Value, "Code", isButtonClicked)
            lblItemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & txtItemCode.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCustomerGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomerGroup._MYValidating
        Try
            Dim qry As String = ""
            Dim WhrCls As String = ""
            qry = " select Cust_Group_Code as Code,Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER"
            txtCustomerGroup.Value = clsCommon.ShowSelectForm("CUST-TENDCustGFnd", qry, "Code", WhrCls, txtCustomerGroup.Value, "Code", isButtonClicked)
            lblCustomerGroupDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" & txtCustomerGroup.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsCustomerTender()
            obj = clsCustomerTender.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then

                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                btnGo.Enabled = False
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
                txtItemCode.Value = obj.Item_Code
                lblItemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & txtItemCode.Value & "'"))
                txtCustomerGroup.Value = obj.Customer_Group
                lblCustomerGroupDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" & txtCustomerGroup.Value & "'"))
                txtTotalQty.Text = obj.Total_Qty
                txtTolerance.Text = obj.Tolerance
                chkTaxInclusive.Checked = IIf(obj.Inclusive_Tax = 1, True, False)
                chkTPTInclude.Checked = IIf(obj.Inclusive_TPT = 1, True, False)
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomerTenderDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = sl
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Cust_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRatePerLtr).Value = objTr.Item_Rate
                        sl += 1
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtCustomerGroup.Value) > 0 Then
                Dim strQry As String = "select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Group_Code='" & txtCustomerGroup.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ii As Integer = 0
                    LoadBlankGrid()
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        gv1.Rows(ii).Cells(colLineNo).Value = ii + 1
                        gv1.Rows(ii).Cells(colCustCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                        gv1.Rows(ii).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                        ii += 1
                    Next
                Else
                    Throw New Exception("No Data Found!")
                End If
            Else
                Throw New Exception("Please Select Customer Group")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please Select Location")
            End If
            If clsCommon.myLen(txtItemCode.Value) <= 0 Then
                Throw New Exception("Please Select Item Code")
            End If
            If clsCommon.myLen(txtCustomerGroup.Value) <= 0 Then
                Throw New Exception("Please Select Customer Group")
            End If
            If txtFromDate.Value >= txtToDate.Value Then
                Throw New Exception("From Date should be less then To Date")
            End If
            If clsCommon.myCdbl(txtTotalQty.Text) <= 0 Then
                Throw New Exception("Please enter Total Qty>0")
            End If
            If clsCommon.myLen(txtTolerance.Text) <= 0 Then
                Throw New Exception("Please enter Tolerance>0")
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRatePerLtr).Value) <= 0 Then
                    Throw New Exception("Item Rate should be greather then 0 at line no -" & clsCommon.myCstr(ii + 1))
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsCustomerTender()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Location_Code = txtLocation.Value
                obj.Item_Code = txtItemCode.Value
                obj.Customer_Group = txtCustomerGroup.Value
                obj.Total_Qty = txtTotalQty.Text
                obj.Tolerance = txtTolerance.Text
                obj.Inclusive_Tax = IIf(chkTaxInclusive.Checked, 1, 0)
                obj.Inclusive_TPT = IIf(chkTPTInclude.Checked, 1, 0)
                obj.Arr = GetTRData()
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetTRData() As List(Of clsCustomerTenderDetail)
        Dim Arr As New List(Of clsCustomerTenderDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colCustCode).Value) > 0 Then
                Dim objTr As New clsCustomerTenderDetail()
                objTr.Cust_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value)
                objTr.Item_Rate = clsCommon.myCstr(gv1.Rows(ii).Cells(colRatePerLtr).Value)
                Arr.Add(objTr)
                'End If
            End If
        Next
        Return Arr
    End Function
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                clsCustomerTender.DeleteData(txtDocNo.Value)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Public Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocNo.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsCustomerTender.PostData(clsCommon.myCstr(txtDocNo.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocNo.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmCustomerTender_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.F10 Then
            If Not btnPost.Enabled AndAlso clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIR
                frm.strCode = clsFixedParameterCode.CustomerTenderAmendment
                frm.ShowDialog()
                If frm.isPasswordCorrect AndAlso Not btnPost.Enabled Then
                    btnAmendment.Enabled = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Document or Post Document")
            End If
        End If

    End Sub

    Private Sub btnAmendment_Click(sender As Object, e As EventArgs) Handles btnAmendment.Click
        Try
            Dim frm As New FrmAmendment()
            frm.strDocCode = clsCommon.myCstr(txtDocNo.Value)
            frm.strtotalQty = clsCommon.myCdbl(txtTotalQty.Text)
            frm.strToDate = clsCommon.myCDate(txtToDate.Value)
            frm.ShowDialog()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class