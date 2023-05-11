Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmjobWorkDebitNote
    Inherits FrmMainTranScreen
    Dim arrLoc As String = Nothing
    Const colDocNo As String = "colDocNo"
    Const colDocDate As String = "colDocDate"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUOM As String = "colUOM"
    Const colselect As String = "COLSELECT"
    Const colQty As String = "colQty"
    Const colFAT As String = "colFAT"
    Const colSNF As String = "colSNF"
    Const colrate As String = "colRate"
    Const colUnitprice As String = "price"
    Private isNewEntry As Boolean = True
    Public isInsideLoadData As Boolean = False


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Public Sub SetUserMgmtNew()
        Try
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If
            btnSave.Visible = MyBase.isModifyFlag
            btnDelete.Visible = MyBase.isDeleteFlag
            btnPost.Visible = MyBase.isPostFlag
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmJobWorkDebitNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        LOCATIONRIGTHS()
        'LoadBlankGrid()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        txtFromDate.Text = clsCommon.GETSERVERDATE()
        txtToDate.Text = clsCommon.GETSERVERDATE()
        txtDocNo.Value = ""
        fndcustNo.Value = ""
        fndLocation.Value = ""
        btnSave.Enabled = False
        btnPost.Enabled = False
        btnDelete.Enabled = False
        txtcustdesc.Text = ""
        txtlocation.Text = ""
        LoadBlankGrid()
        btnGo.Enabled = True
        btnSave.Text = "Save"
        isNewEntry = True
        btnSave.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending

    End Sub
    Private Sub fndcustNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustNo._MYValidating
        fndcustNo.Focus()
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location")
            fndLocation.Focus()
            Exit Sub
        End If

        Dim qry As String = "  select TSPL_customer_MASTER.cust_code as Code,TSPL_customer_MASTER.Customer_Name as Name,TSPL_customer_MASTER.Terms_Code as [Term Code] ,TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,TSPL_customer_MASTER.Tax_Group as [Tax Group],TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as [Tax Group Description] from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        Dim WhrCls As String = "TSPL_TAX_GROUP_MASTER.Tax_Group_Type='s' and  TSPL_customer_MASTER.Status ='N'"
        fndcustNo.Value = clsCommon.ShowSelectForm("CustmrMstrI1", qry, "Code", WhrCls, fndcustNo.Value, "Code", isButtonClicked)

        qry = "  select TSPL_customer_MASTER.cust_code ,TSPL_customer_MASTER.Customer_Name ,TSPL_customer_MASTER.Terms_Code  ,TSPL_TERMS_MASTER.Terms_Desc  ,TSPL_customer_MASTER.Tax_Group ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_customer_MASTER.Inter_Branch from TSPL_customer_MASTER  left outer join  TSPL_TERMS_MASTER on TSPL_customer_MASTER.Terms_Code=TSPL_TERMS_MASTER.Terms_Code  left outer join  TSPL_TAX_GROUP_MASTER on TSPL_customer_MASTER.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code where TSPL_customer_MASTER.cust_code='" + fndcustNo.Value + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtcustdesc.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
        
        Else
            txtcustdesc.Text = ""
          
        End If

     


    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
       
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND1", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
      
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'")
            Else
                ShowData()
                gv1.BestFitColumns()
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
       
    End Sub
    Sub ShowData()
        Try
            LoadBlankGrid()
            Dim arr As New List(Of clsJobWorkDebitNote)
            Dim location As String = clsCommon.myCstr(fndLocation.Value)
            Dim Customer As String = clsCommon.myCstr(fndcustNo.Value)
            Dim fromdate As String = clsCommon.myCstr(txtFromDate.Text)
            Dim todate As String = clsCommon.myCstr(txtToDate.Text)
            arr = clsJobWorkDebitNote.GetData(location, Customer, fromdate, todate, Nothing)
            For Each obj As clsJobWorkDebitNote In arr
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(obj.AgainstDocumentNo)
                gv1.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(obj.DocumentDate)
                gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(obj.ItemCode)
                gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(obj.ItemName)
                gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(obj.Qty)
                gv1.CurrentRow.Cells(colFAT).Value = clsCommon.myCdbl(obj.FatPer)
                gv1.CurrentRow.Cells(colSNF).Value = clsCommon.myCdbl(obj.SnfPer)
                gv1.CurrentRow.Cells(colUnitprice).Value = clsCommon.myCdbl(obj.NetAmt)
                gv1.CurrentRow.Cells(colrate).Value = clsCommon.myCdbl(obj.rate)
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
   
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheck.FormatString = ""
        repoCheck.HeaderText = " "
        repoCheck.Name = colselect
        repoCheck.Width = 60
        repoCheck.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCheck)

        Dim repoDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocNo.FormatString = ""
        repoDocNo.HeaderText = "Document No"
        repoDocNo.Name = colDocNo
        repoDocNo.Width = 100
        repoDocNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocNo)

        Dim repoDocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocDate.FormatString = ""
        repoDocDate.HeaderText = "Document Date"
        repoDocDate.Name = colDocDate
      
        repoDocDate.Width = 100
        repoDocDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocDate)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 100
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repofat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofat.FormatString = ""
        repofat.HeaderText = "Fat %"
        repofat.Name = colFAT
        repofat.IsVisible = True
        repofat.Minimum = 0
        repofat.DecimalPlaces = 2
        repofat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repofat.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofat)

        Dim repoSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSnf.FormatString = ""
        repoSnf.HeaderText = "SNF %"
        repoSnf.Name = colSNF
        repoSnf.IsVisible = True
        repoSnf.Minimum = 0
        repoSnf.DecimalPlaces = 2
        repoSnf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSnf.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSnf)

        Dim reporate As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.HeaderText = "Job Work Rate"
        reporate.Name = colrate
        reporate.IsVisible = True
        reporate.Minimum = 0
        reporate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        reporate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reporate)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colUnitprice
        repoAmount.IsVisible = True
        repoAmount.Minimum = 0
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmount)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub ReStoreGridLayout()
         Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles mnuSaveLayout.Click
         If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
   
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim lineno As Integer = 0
            'Dim arr As New List(Of clsJobWorkDebitNote)
            'Dim obj As clsJobWorkDebitNote = Nothing
            Dim objTr As New clsJobWorkDebitNoteHead()
            objTr.Arr = New List(Of clsJobWorkDebitNote)

            objTr.Document_Date = txtToDate.Value
            objTr.Location = fndLocation.Value
            objTr.Customer = fndcustNo.Value
            objTr.ToDate = txtToDate.Value
            objTr.FromDate = txtFromDate.Value
            objTr.DocumentNo = txtDocNo.Value

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim obj As New clsJobWorkDebitNote()
                lineno += 1
                If clsCommon.myCBool(grow.Cells(colselect).Value) = True Then
                    obj.AgainstDocumentNo = clsCommon.myCstr(grow.Cells(colDocNo).Value)
                    obj.DocumentDate = clsCommon.myCstr(grow.Cells(colDocDate).Value)
                    obj.ItemName = clsCommon.myCstr(grow.Cells(colIName).Value)
                    obj.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                    obj.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    obj.FatPer = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                    obj.SnfPer = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    obj.NetAmt = clsCommon.myCdbl(grow.Cells(colUnitprice).Value)
                    obj.rate = clsCommon.myCdbl(grow.Cells(colrate).Value)
                    objTr.Arr.Add(obj)
                End If
            Next
            If (objTr.Arr Is Nothing OrElse objTr.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill/Select at list one Item")
            Else
                If objTr.SaveData(objTr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully.")
                End If
                txtDocNo.Value = objTr.DocumentNo
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No for Posting")
            End If
            If (myMessages.postConfirm()) Then
                clsJobWorkDebitNote.PostData(Form_ID, txtDocNo.Value, arrLoc)
                clsCommon.MyMessageBoxShow("Data Posted Successfully")
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            Dim qst As String = "select count(*) from TSPL_JOBWORK_DEBIT_NOTE_HEAD where Document_No='" + txtDocNo.Value + "'"
           
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating

        Dim qry As String = "select Document_No,tspl_location_master.Location_Desc,CustomerName,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_JOBWORK_DEBIT_NOTE_HEAD left outer join TSPL_LOCATION_MASTER on tspl_location_master.Location_Code=TSPL_JOBWORK_DEBIT_NOTE_HEAD.Location left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_JOBWORK_DEBIT_NOTE_HEAD.CustomerName"


        Dim whrClas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " Location in (" + arrLoc + ") "
        End If

        LoadData(clsCommon.ShowSelectForm("JWDebitNote", qry, "Document_No", whrClas, txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            LoadBlankGrid()
            btnSave.Text = "Update"
            isInsideLoadData = True
            btnGo.Enabled = False
            btnSave.Enabled = False

            isNewEntry = False
            Dim obj As New clsJobWorkDebitNoteHead()
            obj = clsJobWorkDebitNote.LoadData(strCode, NavTyep, arrLoc)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DocumentNo) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                txtcustdesc.Text = obj.CustomerName
                fndcustNo.Value = obj.Customer
                fndLocation.Value = obj.Location
                txtlocation.Text = obj.LocationName
                txtFromDate.Text = clsCommon.myCDate(obj.FromDate)
                txtToDate.Text = clsCommon.myCDate(obj.ToDate)
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.DocumentNo
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsJobWorkDebitNote In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.AgainstDocumentNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = objTr.DocumentDate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ItemCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.ItemName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = objTr.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colselect).Value = objTr.IsSelect
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = objTr.SnfPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colrate).Value = objTr.rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitprice).Value = objTr.NetAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colselect).ReadOnly = True
                    Next
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
End Class
