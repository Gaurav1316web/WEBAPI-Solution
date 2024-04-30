' Developed  by ppradeep on 11/09/2013  --- ticket no BM00000000442 
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports common
Public Class frmStandardRateItem
    Inherits FrmMainTranScreen
    Dim PageMode As String
    Dim change As Boolean = True
    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colDescription As String = "Description"
    Const colUnit As String = "Unit"
    Const colRate As String = "Rate"
    Const colMrp As String = "Mrp"
    Const colpurchprice As String = "Purchase Price Before VAT/CST"
    Const colCST As String = "CST on purchase"
    Const colVAT As String = "VAT on Purchase"
    Const colExcise As String = "Excise on Purchase Rs"
    Const colExciseonPricePrcnt As String = "Excise on Purchase %"
    Const colFreightcharges As String = "Frieght Charges on Purchase Rs"
    Const colothercharges As String = "Other Charges on Purchase Rs"
    Const colTotalLandingCost As String = "Total Landing Cost"
    Dim headimportfirst As Boolean = False
    Dim isCust As Boolean = True
    Private isCellValueChangedOpen As Boolean = False
    Dim userCode, companyCode As String
    Dim isNewEntry As Boolean

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmStandardRateItem)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003121 16/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            importmenu.Enabled = True
            exportmenu.Enabled = True
        Else
            importmenu.Enabled = False
            exportmenu.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fndCode.MyMaxLength = 12
        txtDesc.MaxLength = 200
    End Sub
    Private Sub frmStandardRateItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        PageMode = "New"
        Me.Text = "Standard Rate of Item"
        ResetScreen()
        LoadGrid()
        gv1.Rows.AddNew()
        dtpEnd.Value = Date.MinValue
        fndCode.MyMaxLength = 12
        fndCode.MyCharacterCasing = CharacterCasing.Upper
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
    End Sub
    
    Public Sub savedata()
        Dim isSaved As Boolean = False
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ValidateSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmStandardRateItem, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsStandardRateItem
                Dim ObjList As New List(Of clsStandardRateItemDetail)
                obj.StdRateCode = clsCommon.myCstr(fndCode.Value)
                obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
                obj.FomeDate = clsCommon.myCDate(dtpStart.Value)
                obj.Descraption = txtDesc.Text
                obj.isCust = radioIsCust.IsChecked
                If dtpEnd.Checked Then
                    obj.IsValied_Date = dtpEnd.Checked
                    obj.Valied_Date = clsCommon.myCDate(dtpEnd.Value)
                Else
                    obj.IsValied_Date = False
                    obj.Valied_Date = Nothing
                End If
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        Dim objtr As New clsStandardRateItemDetail()
                        objtr.StdRateCode = clsCommon.myCstr(fndCode.Value)
                        objtr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objtr.Descraption = clsCommon.myCstr(grow.Cells(colDescription).Value)
                        objtr.Unit = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objtr.rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objtr.MRP = clsCommon.myCdbl(grow.Cells(colMrp).Value)
                        objtr.PurchasepricebeforeVAT = clsCommon.myCdbl(grow.Cells(colpurchprice).Value)
                        objtr.CST = clsCommon.myCdbl(grow.Cells(colCST).Value)
                        objtr.VAT = clsCommon.myCdbl(grow.Cells(colVAT).Value)
                        objtr.exciseonPurhaseRS = clsCommon.myCdbl(grow.Cells(colExcise).Value)
                        objtr.exciseOnPurchasePercnt = clsCommon.myCdbl(grow.Cells(colExciseonPricePrcnt).Value)
                        objtr.Frieghtcharges = clsCommon.myCdbl(grow.Cells(colFreightcharges).Value)
                        objtr.othercharges = clsCommon.myCdbl(grow.Cells(colothercharges).Value)
                        objtr.totallandingCost = clsCommon.myCdbl(grow.Cells(colTotalLandingCost).Value)
                        ObjList.Add(objtr)
                    End If
                Next
                obj.ObjList = ObjList

                If obj.SaveData(obj, isNewEntry, Nothing) Then
                    LoadData(obj.StdRateCode, NavigatorType.Current)
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        fndCode.MyReadOnly = True
        Dim obj As New clsStandardRateItem
        obj = obj.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.StdRateCode) > 0) Then
            ResetScreen()
            isNewEntry = False
            btnSave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGrid()
            fndCode.Value = obj.StdRateCode
            fndCustomer.Value = obj.Cust_Code
            dtpStart.Value = clsCommon.myCDate(obj.FomeDate)
            txtDesc.Text = clsCommon.myCstr(obj.Descraption)
            If obj.isCust Then
                radioIsCust.IsChecked = True
            Else
                radioIsVendor.IsChecked = True
            End If

            If clsCommon.myCBool(obj.IsValied_Date) Then
                dtpEnd.Checked = True
                dtpEnd.Value = clsCommon.myCDate(obj.Valied_Date)
            Else
                dtpEnd.Checked = False
                dtpEnd.Value = Nothing
            End If
        
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                isCellValueChangedOpen = True
                For Each objtr As clsStandardRateItemDetail In obj.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objtr.Line_No)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = objtr.Descraption
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMrp).Value = objtr.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpurchprice).Value = objtr.PurchasepricebeforeVAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCST).Value = objtr.CST
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVAT).Value = objtr.VAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colExcise).Value = objtr.exciseonPurhaseRS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colExciseonPricePrcnt).Value = objtr.exciseOnPurchasePercnt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFreightcharges).Value = objtr.Frieghtcharges

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colothercharges).Value = objtr.othercharges
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalLandingCost).Value = objtr.totallandingCost

                Next
                isCellValueChangedOpen = False
            End If
            gv1.Rows.AddNew()
        End If
    End Sub

    Private Sub ResetScreen()
        dtpEnd.Value = Nothing
        dtpEnd.Checked = False
        dtpStart.Value = clsCommon.GETSERVERDATE()
        txtDesc.MaxLength = 200
        txtDesc.Text = ""
        fndCustomer.Value = Nothing
        fndCode.Value = Nothing
        txtDesc.Text = ""
        isNewEntry = True
        radioIsCust.IsChecked = True
        fndCustomer.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        btnSave.Text = "Save"
        LoadGrid()
        gv1.Rows.AddNew()
        fndCode.MyReadOnly = False
    End Sub

    Private Function ValidateSave() As Boolean
        If dtpEnd.Value.Date = dtpEnd.MinDate Then
        Else
            If dtpStart.Value > dtpEnd.Value Then
                clsCommon.MyMessageBoxShow("Valid Till Date should be greater than or equal to start date.")
                Return False
            End If
        End If
        If clsCommon.myLen(fndCustomer.Value) < 1 Then
            If isCust Then
                clsCommon.MyMessageBoxShow("Please select a Customer. ")
            Else
                clsCommon.MyMessageBoxShow("Please select a Vendor. ")
            End If
            Return False
        ElseIf clsCommon.myLen(fndCustomer.Value) >= 1 Then
            Dim qry As String = " select count(*) from "
            If isCust Then
                qry = qry & " TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(fndCustomer.Value) & "'"
            Else
                qry = qry & " TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(fndCustomer.Value) & "'"
            End If
            If clsDBFuncationality.getSingleValue(qry) <= 0 Then
                If isCust Then
                    clsCommon.MyMessageBoxShow("Invalid  Customer. ")
                Else
                    clsCommon.MyMessageBoxShow("Invalid Vendor. ")
                End If
                fndCustomer.Focus()
                Return False
            End If
        ElseIf gv1.Rows.Count = 0 Then
            clsCommon.MyMessageBoxShow("Insert at least one Item.")
            Return False
        End If
            Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Public Sub funDelete()
        If clsCommon.myLen(fndCode.Value) < 1 Then
            clsCommon.MyMessageBoxShow("Please select Code to Delete.")
            Return
        ElseIf myMessages.deleteConfirm Then
            Try
                If clsStandardRateItem.DeleteData(fndCode.Value) Then
                    myMessages.delete()
                    ResetScreen()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colItemCode) Then
                    OpenICodeList(True)
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = Nothing
        Dim str_Item_Price_ID = ""
        Dim qry As String = ""
        qry = " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
        qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "

        Dim WhrCls As String = ""
        'If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
        '    WhrCls = "T1.Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "'"
        'End If

        str_Item_Price_ID = clsCommon.ShowSelectForm("ItemFinder", qry, "Item_Price_ID", "", "", "Item_Price_ID", isButtonClick)
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry = " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "
            qry += " where T1.Item_Price_ID ='" + str_Item_Price_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                gv1.CurrentRow.Cells(colDescription).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                gv1.CurrentRow.Cells(colMrp).Value = clsCommon.myCstr(dt.Rows(0)("Item_Basic_Net"))
            Else
                gv1.CurrentRow.Cells(colItemCode).Value = ""
                gv1.CurrentRow.Cells(colDescription).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colMrp).Value = ""
            End If
        End If
    End Sub
    'Private Sub exportmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exportmenu.Click
    '    Dim query As String = 
    '    transportSql.ExporttoExcel(query, Me)
    'End Sub

    'Private Sub importmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importmenu.Click
    '    Dim dgv As New RadGridView
    '    Me.Controls.Add(dgv)
    '    If transportSql.importExcel(dgv, "Scheme Code", "Scheme Description", "Start Date", "End date", "Scheme Type", "Main Itme Code", "Main Item Description", "Main Item Qty", "Amount", "Comments", "Main Item Uom", "MRP", "Scheme Code Auto", "Scheme Item Code", "Scheme Item Description", "Qty", "Remarks", "UOM", "Mrp1", "Price date") Then
    '        Dim trans As SqlTransaction
    '        Try
    '            ''connectSql.OpenConnection()
    '            trans = clsDBFuncationality.GetTransactin()
    '            clsCommon.ProgressBarShow()
    '            For Each dgrv As GridViewRowInfo In dgv.Rows
    '                Dim strscheme_code As String = clsCommon.myCstr(dgrv.Cells(0).Value)
    '                If clsCommon.myLen(strscheme_code) > 12 Or String.IsNullOrEmpty(strscheme_code) Then
    '                    Throw New Exception("Check the length of scheme Code/Blank")

    '                End If
    '                Dim strscheme_description As String = clsCommon.myCstr(dgrv.Cells(1).Value)
    '                If clsCommon.myLen(strscheme_description) > 100 Then
    '                    Throw New Exception("Check the length of Scheme Description / Blank")

    '                End If
    '                Dim strscheme_type As String = clsCommon.myCstr(dgrv.Cells(4).Value)
    '                If clsCommon.myLen(strscheme_type) > 10 Or strscheme_type = "" Then
    '                    Throw New Exception("Check the Length of Scheme type/Blank ")

    '                End If
    '                Dim strmain_item_code As String = clsCommon.myCstr(dgrv.Cells(5).Value)
    '                If clsCommon.myLen(strmain_item_code) > 50 Then
    '                    Throw New Exception("Check the Length of main item code/Blank")

    '                End If
    '                Dim strmain_itemcode_desc As String = clsCommon.myCstr(dgrv.Cells(6).Value)
    '                If clsCommon.myLen(strmain_itemcode_desc) > 100 Then
    '                    Throw New Exception("Check the length of main item code description/blank")

    '                End If
    '                Dim strmain_item_qty As Decimal = CDec(dgrv.Cells(7).Value.ToString())
    '                If strmain_item_qty < 0 Then
    '                    Throw New Exception("main item qty should not be blank")

    '                End If
    '                Dim stramount As Decimal = CDec(dgrv.Cells(8).Value.ToString())
    '                If stramount < 0 Then
    '                    Throw New Exception("Amount should not be blank")

    '                End If
    '                Dim strcomments As String = clsCommon.myCstr(dgrv.Cells(9).Value)
    '                If clsCommon.myLen(strcomments) > 200 Then
    '                    Throw New Exception("Check the length of Comments")

    '                End If
    '                Dim strmainitem_uom As String = clsCommon.myCstr(dgrv.Cells(10).Value)
    '                If clsCommon.myLen(strmainitem_uom) > 12 Then
    '                    Throw New Exception("Check the length of Main item Uom")

    '                End If
    '                Dim strschemeitem_code As String = clsCommon.myCstr(dgrv.Cells(13).Value)
    '                If clsCommon.myLen(strschemeitem_code) > 12 Or String.IsNullOrEmpty(strschemeitem_code) Then
    '                    Throw New Exception("Check the length of Scheme Item Code/blank")

    '                End If
    '                Dim strschemeitem_desc As String = clsCommon.myCstr(dgrv.Cells(14).Value)
    '                If clsCommon.myLen(strschemeitem_desc) > 100 Then
    '                    Throw New Exception("Check the length of scheme itme descriiption")

    '                End If
    '                Dim strremarks As String = clsCommon.myCstr(dgrv.Cells(16).Value)
    '                If clsCommon.myLen(strremarks) > 200 Then
    '                    Throw New Exception("Check the length of Remarks")

    '                End If
    '                Dim struom As String = clsCommon.myCstr(dgrv.Cells(17).Value)
    '                If clsCommon.myLen(struom) > 12 Then
    '                    Throw New Exception("Check the length of Uom")

    '                End If
    '                Dim mrp1 As Double = clsCommon.myCdbl(dgrv.Cells(18).Value)
    '                Dim start_date As String = Format(dgrv.Cells(2).Value, "dd/MMM/yyyy")
    '                Dim end_date As String
    '                If clsCommon.myLen(dgrv.Cells(3).Value) > 0 Then
    '                    end_date = Format(dgrv.Cells(3).Value, "dd/MMM/yyyy")
    '                End If


    '                Dim mrp As Double = clsCommon.myCdbl(dgrv.Cells(11).Value)
    '                Dim schemecode_auto As Integer = CInt(dgrv.Cells(12).Value.ToString)
    '                Dim qty As Integer = CInt(dgrv.Cells(15).Value.ToString)


    '                Dim coll As New Hashtable()
    '                Try
    '                    clsCommon.AddColumnsForChange(coll, "scheme_code", strscheme_code)
    '                    clsCommon.AddColumnsForChange(coll, "scheme_desc", strscheme_description)
    '                    clsCommon.AddColumnsForChange(coll, "start_date", start_date)
    '                    clsCommon.AddColumnsForChange(coll, "end_date", end_date, True)
    '                    clsCommon.AddColumnsForChange(coll, "scheme_type", strscheme_type)
    '                    clsCommon.AddColumnsForChange(coll, "main_item_code", strmain_item_code)
    '                    clsCommon.AddColumnsForChange(coll, "main_item_desc", strmain_itemcode_desc)
    '                    clsCommon.AddColumnsForChange(coll, "main_item_qty", strmain_item_qty)
    '                    clsCommon.AddColumnsForChange(coll, "amount", stramount)
    '                    clsCommon.AddColumnsForChange(coll, "comments", strcomments)
    '                    clsCommon.AddColumnsForChange(coll, "created_by", userCode)
    '                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
    '                    clsCommon.AddColumnsForChange(coll, "modify_by", userCode)
    '                    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
    '                    clsCommon.AddColumnsForChange(coll, "comp_code", companyCode)
    '                    clsCommon.AddColumnsForChange(coll, "main_item_uom", strmainitem_uom)
    '                    clsCommon.AddColumnsForChange(coll, "mrp", mrp)
    '                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Insert, "", trans)
    '                    'Dim query As String = "insert into TSPL_SCHEME_MASTER (scheme_code,scheme_desc,start_date,end_date,scheme_type,main_item_code,main_item_desc,main_item_qty,amount,comments,created_by,created_date,modify_by,modify_date,comp_code,main_item_uom,mrp)values('" + clsCommon.myCstr(strscheme_code) + "','" + clsCommon.myCstr(strscheme_description) + "','" + start_date + "','" + end_date + "','" + clsCommon.myCstr(strscheme_type.ToString()) + "','" + clsCommon.myCstr(strmain_item_code) + "','" + clsCommon.myCstr(strmain_itemcode_desc) + "','" + strmain_item_qty + "','" + stramount + "','" + strcomments.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + clsCommon.myCstr(strmainitem_uom) + "','" + clsCommon.myCstr(mrp) + "')"
    '                    ''connectSql.RunSqlTransaction(trans, query)
    '                Catch ex As Exception
    '                    Dim s1 As String = ex.ToString().Substring(0, 46)
    '                    If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " OrElse ex.Message.ToString().Substring(0, 35) = "Violation of PRIMARY KEY constraint" Then
    '                        'Dim query1 As String = "update TSPL_SCHEME_MASTER set scheme_desc='" + strscheme_description + "',start_date='" + start_date.ToString() + "',End_date='" + end_date.ToString() + "',scheme_type='" + strscheme_type.ToString() + "',main_item_code='" + strmain_item_code.ToString() + "',main_item_desc='" + strmain_itemcode_desc.ToString() + "',main_item_qty='" + strmain_item_qty.ToString() + "',Amount='" + stramount.ToString() + "',commnets='" + strcomments.ToString() + "',main_item_uom='" + strmainitem_uom.ToString() + "',mrp='" + mrp.ToString() + "'where scheme_code='" + strscheme_code.ToString() + "'"
    '                        'connectSql.RunSqlTransaction(trans, query1)
    '                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Update, "scheme_code='" + strscheme_code + "'", trans)
    '                    End If
    '                End Try
    '                'Dim col1 As New Hashtable
    '                'Try
    '                '    'clsCommon.AddColumnsForChange(col1, "")
    '                '    ''    Dim query2 As String = "insert into tspl_scheme_details values('" + schemecode_auto.ToString() + "','" + strscheme_code.ToString() + "','" + strscheme_description.ToString() + "','" + strschemeitem_code.ToString() + "','" + strschemeitem_desc.ToString() + "','" + qty.ToString() + "','" + strremarks.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + struom.ToString() + "','" + mrp1.ToString() + "','" + price_date1 + "'"
    '                '    ''    connectSql.RunSqlTransaction(trans, query2)
    '                'Catch ex As Exception
    '                '    ''    Dim s1 As String = ex.ToString().Substring(0, 46)
    '                '    ''    If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " Then
    '                '    ''      
    '                '    ''    End If
    '                'End Try
    '            Next
    '            trans.Commit()
    '            clsCommon.ProgressBarHide()
    '            common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
    '        Catch ex As Exception
    '            trans.Rollback()
    '            clsCommon.ProgressBarHide()

    '            myMessages.myExceptions(ex)


    '        End Try
    '    End If
    '    Me.Controls.Remove(dgv)
    'End Sub

    Sub LoadGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colItemCode
        repoItemCode.Width = 100
        repoItemCode.ReadOnly = False
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc.FormatString = ""
        repoDesc.HeaderText = "Description"
        repoDesc.Name = colDescription
        repoDesc.Width = 200
        repoDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDesc)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoMRP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMrp
        repoMRP.Width = 100
        repoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 100
        repoRate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repopricebfrVATCST As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopricebfrVATCST.FormatString = ""
        repopricebfrVATCST.HeaderText = "Purchase Price Before VAT/CST"
        repopricebfrVATCST.Name = colpurchprice
        repopricebfrVATCST.Width = 100
        repopricebfrVATCST.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repopricebfrVATCST)

        Dim repoCST As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCST.FormatString = ""
        repoCST.HeaderText = "CST on purchase"
        repoCST.Name = colCST
        repoCST.Width = 100
        repoCST.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCST)


        Dim repoVAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVAT.FormatString = ""
        repoVAT.HeaderText = "VAT on Purchase"
        repoVAT.Name = colVAT
        repoVAT.Width = 100
        repoVAT.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoVAT)

        Dim repoexcseonprchRS As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoexcseonprchRS.FormatString = ""
        repoexcseonprchRS.HeaderText = "Excise on Purchase Rs"
        repoexcseonprchRS.Name = colExcise
        repoexcseonprchRS.Width = 100
        repoexcseonprchRS.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoexcseonprchRS)


        Dim repoexcihseonprchprcnt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoexcihseonprchprcnt.FormatString = ""
        repoexcihseonprchprcnt.HeaderText = "Excise on Purchase %"
        repoexcihseonprchprcnt.Name = colExciseonPricePrcnt
        repoexcihseonprchprcnt.Width = 100
        repoexcihseonprchprcnt.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoexcihseonprchprcnt)


        Dim repofrieghtchrges As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofrieghtchrges.FormatString = ""
        repofrieghtchrges.HeaderText = "Frieght Charges on Purchase Rs"
        repofrieghtchrges.Name = colFreightcharges
        repofrieghtchrges.Width = 100
        repofrieghtchrges.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repofrieghtchrges)


        Dim repoothrchrges As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoothrchrges.FormatString = ""
        repoothrchrges.HeaderText = "Other Charges on Purchase Rs"
        repoothrchrges.Name = colothercharges
        repoothrchrges.Width = 100
        repoothrchrges.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoothrchrges)


        Dim repolandingcost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolandingcost.FormatString = ""
        repolandingcost.HeaderText = "Total Landing Cost"
        repolandingcost.Name = colTotalLandingCost
        repolandingcost.Width = 100
        repolandingcost.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repolandingcost)










    End Sub

    Private Sub frmStandardRateItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            savedata()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub fndScheme__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating

        'Dim Qry As String = "select StdRateCode as [Code],FomeDate as [Date],Valied_Date as [Valied Till Date],Cust_Code as [Customer/Vendor],Descraption from TSPL_STANDARD_RATE_ITEM"
        'fndCode.Value = clsCommon.ShowSelectForm("fmParent_Customer_No", Qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
        fndCode.Value = clsStandardRateItem.getFinder("", fndCode.Value, isButtonClicked)
        If clsCommon.myLen(fndCode.Value) > 0 Then
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
        'Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "' "
    End Sub
    
    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        If isCust Then
            Dim Qry As String = "select Cust_Code as [Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(case when Status='N' then 'Active' else 'InActive' end ) as [Status] from TSPL_CUSTOMER_MASTER"
            fndCustomer.Value = clsCommon.ShowSelectForm("fmParent_Customer_No", Qry, "Code", "", fndCustomer.Value, "Code", isButtonClicked)
            Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "' "
        Else
            Dim Qry As String = "select Vendor_Code as [Code],Vendor_Name as [Name],Vendor_Group_Code as [Vendor Group],(case when Status='N' then 'Active' else 'InActive' end ) as [Status] from TSPL_Vendor_MASTER"
            fndCustomer.Value = clsCommon.ShowSelectForm("fmParent_Vendor_No", Qry, "Code", "", fndCustomer.Value, "Code", isButtonClicked)
            Dim qryDesc As String = "select Vendor_name From TSPL_Vendor_MASTER where Vendor_Code='" + fndCustomer.Value + "' "
        End If
    End Sub

    Private Sub radioIsCust_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioIsCust.ToggleStateChanged
        If radioIsCust.IsChecked Then
            isCust = True
            RadLabel4.Text = "Customer"

        ElseIf radioIsVendor.IsChecked Then
            isCust = False
            RadLabel4.Text = "Vendor"

        End If
    End Sub
    Private Sub btnimprtHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimprtHead.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Doc Code", "From Date", "Is Valid Date", "Valid Till", "Code", "Description", "Is Customer") Then

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Doc_Code As String = ""
                Dim FromDate As String = ""
                Dim Is_ValidDate As String = ""
                Dim Valid_Till As String = ""
                Dim Code As String = ""
                Dim Description As String = ""
                Dim IsCustomer As String = ""

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 0
                Dim codecheck As String = ""
                Dim qry As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    counter += 1
                    Doc_Code = clsCommon.myCstr(grow.Cells("Doc Code").Value)

                    If clsCommon.myLen(Doc_Code) <= 0 Then
                        Exit For

                    ElseIf clsCommon.myLen(Doc_Code) > 30 Then
                        Throw New Exception("Length Of Standard Document Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    FromDate = clsCommon.myCstr(grow.Cells("From Date").Value)

                    If clsCommon.myLen(FromDate) <= 0 Then
                        Throw New Exception("From Date Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the From Date  ")
                    ElseIf clsCommon.myLen(Description) > 50 Then
                        Throw New Exception("Length Of Description Should Not Exceed 50 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    FromDate = clsCommon.myCstr(clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy"))

                    Is_ValidDate = clsCommon.myCstr(grow.Cells("Is Valid Date").Value)
                    If ((clsCommon.CompairString(Is_ValidDate, "false") = CompairStringResult.Equal) Or (clsCommon.CompairString(Is_ValidDate, "true") = CompairStringResult.Equal)) Then

                    Else

                        Throw New Exception("fill is valid date as either true or false , check  At Line No. " + clsCommon.myCstr(counter))
                       
                    End If

                    IsCustomer = clsCommon.myCstr(grow.Cells("Is Customer").Value)
                    If (((clsCommon.CompairString(IsCustomer, "Y") = CompairStringResult.Equal) Or (clsCommon.CompairString(IsCustomer, "N") = CompairStringResult.Equal)) Or ((clsCommon.CompairString(IsCustomer, "N") = CompairStringResult.Equal) Or (clsCommon.CompairString(IsCustomer, "n") = CompairStringResult.Equal))) Then
                    Else
                        Throw New Exception("Is Customer Can only have either 'Y' or 'N'...check,At Line No. " + clsCommon.myCstr(counter))
                    End If




                    Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If ((clsCommon.CompairString(IsCustomer, "Y") = CompairStringResult.Equal) Or (clsCommon.CompairString(IsCustomer, "y") = CompairStringResult.Equal)) Then
                        codecheck = "select Count(*)from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(Code) + "'"
                        If clsCommon.myLen(codecheck) = 0 Then
                            Throw New Exception("The Code for Customer Does Not Exist At Line No. " + clsCommon.myCstr(counter))
                        End If
                    ElseIf ((clsCommon.CompairString(IsCustomer, "N") = CompairStringResult.Equal) Or (clsCommon.CompairString(IsCustomer, "n") = CompairStringResult.Equal)) Then
                        codecheck = "select Count(*)from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(Code) + "'"
                        If clsCommon.myLen(codecheck) = 0 Then
                            Throw New Exception("The Code for Item Does Not Exist At Line No. " + clsCommon.myCstr(counter))
                        End If
                    End If
                    If clsCommon.myLen(Code) <= 0 Then
                        Throw New Exception("Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code Correctly")
                    ElseIf clsCommon.myLen(Code) > 30 Then
                        Throw New Exception("Length Of Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter))
                    End If


                    Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(Description) <= 0 Then
                        Throw New Exception("Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code Correctly")
                    ElseIf clsCommon.myLen(Description) > 90 Then
                        Throw New Exception("Length Of Description Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If


                    Valid_Till = clsCommon.myCstr(grow.Cells("Valid Till").Value)
                    If (Is_ValidDate IsNot Nothing AndAlso (clsCommon.CompairString(Is_ValidDate, "true") = CompairStringResult.Equal)) Then
                        Valid_Till = clsCommon.myCstr(clsCommon.GetPrintDate(Valid_Till, "dd/MMM/yyyy"))
                        If clsCommon.myLen(Valid_Till) <= 0 Then
                            Throw New Exception("Valid date can not be Blank if 'is Valid Date' is 1")
                        End If
                    End If




                    Dim isSaved As Boolean = True
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "StdRateCode", Doc_Code)
                    clsCommon.AddColumnsForChange(coll, "FomeDate", FromDate)
                    clsCommon.AddColumnsForChange(coll, "IsValied_Date", Is_ValidDate)
                    clsCommon.AddColumnsForChange(coll, "Valied_Date", Valid_Till)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", Code)
                    clsCommon.AddColumnsForChange(coll, "Descraption", Description)
                    clsCommon.AddColumnsForChange(coll, "IsCustomer", IsCustomer)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))


                    Dim count As Integer = (clsDBFuncationality.getSingleValue("select  Count(*) from TSPL_STANDARD_RATE_ITEM where StdRateCode = '" + clsCommon.myCstr(Doc_Code) + "'", trans))
                    If count = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM", OMInsertOrUpdate.Update, " StdRateCode  = '" + clsCommon.myCstr(Doc_Code) + "'", trans)

                    End If
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                headimportfirst = True
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub BtnimportDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnimportDetails.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Doc No", "Line No", "Item Code", "Description", "Unit", "MRP", "Rate", "price before CST VAT", "CST", "VAT", "Excise on Purchase Rs", "Excise on Purchase Percent", "Freight Charges", "Other Charges", "Total landing Cost") Then


            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try


                Dim Doc_Code As String = ""
                Dim Line_No As String = ""
                Dim Item_Code As String = ""
                Dim Description As String = ""
                Dim Unit As String = ""
                Dim MRP As String = ""
                Dim Rate As String = ""
                Dim price_before_CST_VAT As String = ""
                Dim CST As String = ""
                Dim VAT As String = ""
                Dim Excise_on_Purchase_Rs As String = ""
                Dim Excise_on_Purchase_Percent As String = ""
                Dim Other_Charges As String = ""
                Dim Freight_Charges As String = ""
                Dim Total_landing_Cost As String = ""

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 0
                Dim codecheck As String
                Dim qry As String = ""
                If (headimportfirst = True) Then
                    Dim qrynew As String = "delete from  TSPL_STANDARD_RATE_ITEM_DETAIL where StdRateCode = '" + clsCommon.myCstr(Doc_Code) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qrynew, trans)


                    For Each grow As GridViewRowInfo In gv.Rows
                        counter += 1
                        Doc_Code = clsCommon.myCstr(grow.Cells("Doc No").Value)
                        If clsCommon.myLen(Doc_Code) <= 0 Then
                            Exit For
                        ElseIf clsCommon.myLen(Doc_Code) > 30 Then
                            Throw New Exception("Length Of Standard Document Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Line_No = clsCommon.myCstr(grow.Cells("Line No").Value)

                        If clsCommon.myLen(Line_No) <= 0 Then
                            Throw New Exception("From Date Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the From Date  ")
                        End If

                        Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        codecheck = "select Count(*) from tspl_item_master where Item_code='" + Item_Code + "'"
                        If clsCommon.myLen(codecheck) = 0 Then
                            Throw New Exception("Item Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code Correctly")
                        ElseIf clsCommon.myLen(Item_Code) > 90 Then
                            Throw New Exception("Length Of Description Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If


                        Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(Description) <= 0 Then
                            Throw New Exception("Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code Correctly")
                        ElseIf clsCommon.myLen(Description) > 90 Then
                            Throw New Exception("Length Of Description Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Unit = clsCommon.myCstr(grow.Cells("Unit").Value)
                        If clsCommon.myLen(Unit) <= 0 Then
                            Throw New Exception("Unit Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ", Enter Numeric value")
                        End If

                        MRP = clsCommon.myCstr(grow.Cells("MRP").Value)
                        If clsCommon.myLen(MRP) <= 0 Then
                            Throw New Exception("MRP Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If

                        Rate = clsCommon.myCstr(grow.Cells("Rate").Value)
                        If clsCommon.myLen(Rate) <= 0 Then
                            Throw New Exception("Rate Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If

                        price_before_CST_VAT = clsCommon.myCstr(grow.Cells("price before CST VAT").Value)
                        If clsCommon.myLen(price_before_CST_VAT) <= 0 Then
                            Throw New Exception("Purchase Price before CST VAT Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If

                        CST = clsCommon.myCstr(grow.Cells("CST").Value)
                        If clsCommon.myLen(CST) <= 0 Then
                            Throw New Exception("CST Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If


                        VAT = clsCommon.myCstr(grow.Cells("VAT").Value)
                        If clsCommon.myLen(VAT) <= 0 Then
                            Throw New Exception("VAT Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If



                        Excise_on_Purchase_Rs = clsCommon.myCstr(grow.Cells("Excise on Purchase Rs").Value)
                        If clsCommon.myLen(Excise_on_Purchase_Rs) <= 0 Then
                            Throw New Exception("Excise on Purchase Rs Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If


                        Excise_on_Purchase_Percent = clsCommon.myCstr(grow.Cells("Excise on Purchase Percent").Value)
                        If clsCommon.myLen(Excise_on_Purchase_Percent) <= 0 Then
                            Throw New Exception("Excise on Purchase Percent Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If

                        Other_Charges = clsCommon.myCstr(grow.Cells("Other Charges").Value)
                        If clsCommon.myLen(Other_Charges) <= 0 Then
                            Throw New Exception("Other Charges Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If



                        Freight_Charges = clsCommon.myCstr(grow.Cells("Freight Charges").Value)
                        If clsCommon.myLen(Freight_Charges) <= 0 Then
                            Throw New Exception("Freight Charges Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If



                        Total_landing_Cost = clsCommon.myCstr(grow.Cells("Total landing Cost").Value)
                        If clsCommon.myLen(Total_landing_Cost) <= 0 Then
                            Throw New Exception("Total landing Cost Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",Enter Numeric value")
                        End If


                        Dim isSaved As Boolean = True
                        Dim coll As New Hashtable()


                        clsCommon.AddColumnsForChange(coll, "StdRateCode", Doc_Code)
                        clsCommon.AddColumnsForChange(coll, "Line_No", Line_No)
                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll, "Descraption", Description)
                        clsCommon.AddColumnsForChange(coll, "Unit", Unit)
                        clsCommon.AddColumnsForChange(coll, "MRP", MRP)
                        clsCommon.AddColumnsForChange(coll, "Rate", Rate)
                        clsCommon.AddColumnsForChange(coll, "PurchasePriceBeforeVATnCST", price_before_CST_VAT)

                        clsCommon.AddColumnsForChange(coll, "CSTonPurchase", CST)
                        clsCommon.AddColumnsForChange(coll, "VATonPurchase", VAT)
                        clsCommon.AddColumnsForChange(coll, "ExciseOnPurchaseRs", Excise_on_Purchase_Rs)
                        clsCommon.AddColumnsForChange(coll, "ExciseOnPurchasePrcnt", Excise_on_Purchase_Percent)
                        clsCommon.AddColumnsForChange(coll, "OtherChargesOnPurchase", Other_Charges)
                        clsCommon.AddColumnsForChange(coll, "FreightChargesOnPurchaseRs", Freight_Charges)
                        clsCommon.AddColumnsForChange(coll, "TotalLandingCost", Total_landing_Cost)


                     
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    Next

                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)


                End If
                clsCommon.ProgressBarHide()
                headimportfirst = False
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnExportStdhead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportStdhead.Click
        Dim str As String
        str = "select StdRateCode as [Doc Code],FomeDate as [From Date] ,IsValied_Date as [Is Valid Date] ,Valied_Date as [Valid Till] ,Cust_Code as [Code] ,Descraption  as [Description],IsCustomer as [Is Customer]  from TSPL_STANDARD_RATE_ITEM"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub BtnExportstdDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportstdDetails.Click
        Dim str As String
        str = "select StdRateCode as[Doc No] ,Line_No as [Line No],Item_Code as [Item Code],Descraption as [Description] ,Unit,MRP,Rate,PurchasePriceBeforeVATnCST as [price before CST VAT],CSTonPurchase as [CST],VATonPurchase as [VAT],ExciseOnPurchaseRs as [Excise on Purchase Rs],ExciseOnPurchasePrcnt as [Excise on Purchase Percent],FreightChargesOnPurchaseRs as [Freight Charges],OtherChargesOnPurchase as [Other Charges],TotalLandingCost as [Total landing Cost] from TSPL_STANDARD_RATE_ITEM_DETAIL"
        transportSql.ExporttoExcel(str, Me)
    End Sub


End Class