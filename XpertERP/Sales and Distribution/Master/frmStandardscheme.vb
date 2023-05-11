' Developed  by pradeep on 11/09/2013 ---- BM00000000443
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Data.SqlClient
Imports common
Public Class frmStandardscheme
    Inherits FrmMainTranScreen
    Dim PageMode As String
    Dim change As Boolean = True

    Const colLineNo As String = "LineNo"
    Const colMainItemCode As String = "MainItemCode"
    Const colMainDescription As String = "MainDescription"
    Const colMainUnit As String = "MainUnit"
    Const colMainRate As String = "MainRate"
    Const colMainMrp As String = "MainMrp"

    Const colschItemCode As String = "schItemCode"
    Const colschDescription As String = "schDescription"
    Const colschUnit As String = "schUnit"
    Const colschRate As String = "schRate"
    Const colschMrp As String = "schMrp"

    Private isCellValueChangedOpen As Boolean = False
    Dim userCode, companyCode As String
    Dim isNewEntry As Boolean

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmStandardscheme)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
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
        Me.Text = "Standard Scheme"
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
        Try
            If ValidateSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmStandardscheme, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsStandardScheme
                Dim ObjList As New List(Of clsStandardSchemeDetail)
                obj.StdSchCode = clsCommon.myCstr(fndCode.Value)
                obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
                obj.FomeDate = clsCommon.myCDate(dtpStart.Value)
                obj.Descraption = txtDesc.Text
                If dtpEnd.Checked Then
                    obj.IsValied_Date = dtpEnd.Checked
                    obj.Valied_Date = clsCommon.myCDate(dtpEnd.Value)
                Else
                    obj.IsValied_Date = False
                    obj.Valied_Date = Nothing
                End If
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colMainItemCode).Value)) > 0 Then
                        Dim objtr As New clsStandardSchemeDetail()
                        objtr.StdSchCode = clsCommon.myCstr(fndCode.Value)
                        objtr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        objtr.MainItem_Code = clsCommon.myCstr(grow.Cells(colMainItemCode).Value)
                        objtr.MainDescraption = clsCommon.myCstr(grow.Cells(colMainDescription).Value)
                        objtr.MainUnit = clsCommon.myCstr(grow.Cells(colMainUnit).Value)
                        objtr.MainRate = clsCommon.myCdbl(grow.Cells(colMainRate).Value)
                        objtr.MainMRP = clsCommon.myCdbl(grow.Cells(colMainMrp).Value)
                        objtr.SchItem_Code = clsCommon.myCstr(grow.Cells(colschItemCode).Value)
                        objtr.SchDescraption = clsCommon.myCstr(grow.Cells(colschDescription).Value)
                        objtr.SchUnit = clsCommon.myCstr(grow.Cells(colschUnit).Value)
                        objtr.SchRate = clsCommon.myCdbl(grow.Cells(colschRate).Value)
                        objtr.SchMRP = clsCommon.myCdbl(grow.Cells(colschMrp).Value)
                        ObjList.Add(objtr)
                    End If
                Next
                clsStandardScheme.ObjList = ObjList
                If obj.SaveData(obj, isNewEntry, Nothing) Then
                    LoadData(obj.StdSchCode, NavigatorType.Current)
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        fndCode.MyReadOnly = True
        Dim obj As New clsStandardScheme
        obj = clsStandardScheme.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.StdSchCode) > 0) Then
            ResetScreen()
            isNewEntry = False
            btnSave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGrid()
            fndCode.Value = obj.StdSchCode
            fndCustomer.Value = obj.Cust_Code
            dtpStart.Value = clsCommon.myCDate(obj.FomeDate)
            txtDesc.Text = clsCommon.myCstr(obj.Descraption)
            If clsCommon.myCBool(obj.IsValied_Date) Then
                dtpEnd.Checked = True
                dtpEnd.Value = clsCommon.myCDate(obj.Valied_Date)
            Else
                dtpEnd.Checked = False
                dtpEnd.Value = Nothing
            End If

            If (clsStandardScheme.ObjList IsNot Nothing AndAlso clsStandardScheme.ObjList.Count > 0) Then
                isCellValueChangedOpen = True
                For Each objtr As clsStandardSchemeDetail In clsStandardScheme.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objtr.Line_No)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainItemCode).Value = objtr.MainItem_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainDescription).Value = objtr.MainDescraption
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainUnit).Value = objtr.MainUnit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainRate).Value = objtr.MainRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainMrp).Value = objtr.MainMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colschItemCode).Value = objtr.SchItem_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colschDescription).Value = objtr.SchDescraption
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colschUnit).Value = objtr.SchUnit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colschRate).Value = objtr.SchRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colschMrp).Value = objtr.SchMRP
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
                clsCommon.MyMessageBoxShow("Valied Till Date should be greater than or equal to start date.")
                Return False
            End If
        End If
        If clsCommon.myLen(fndCustomer.Value) < 1 Then
            clsCommon.MyMessageBoxShow("Please select a Customer. ")
            Return False
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
                If clsStandardScheme.DeleteData(fndCode.Value) Then
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
                If e.Column Is gv1.Columns(colMainItemCode) Then
                    OpenICodeList(True)
                End If
                If e.Column Is gv1.Columns(colschItemCode) Then
                    OpenSchemeICodeList(True)
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = Nothing
        Dim str_Item_Price_ID = ""
        Dim qry As String = ""
        qry += " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
        qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "

        Dim WhrCls As String = ""
        'If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
        '    WhrCls = "T1.Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "'"
        'End If

        str_Item_Price_ID = clsCommon.ShowSelectForm("ItemFinder", qry, "Item_Price_ID", "", "", "Item_Price_ID", isButtonClick)
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry += " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "
            qry += " where T1.Item_Price_ID ='" + str_Item_Price_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colMainItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                gv1.CurrentRow.Cells(colMainDescription).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                gv1.CurrentRow.Cells(colMainUnit).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                gv1.CurrentRow.Cells(colMainMrp).Value = clsCommon.myCstr(dt.Rows(0)("Item_Basic_Net"))
            Else
                gv1.CurrentRow.Cells(colMainItemCode).Value = ""
                gv1.CurrentRow.Cells(colMainDescription).Value = ""
                gv1.CurrentRow.Cells(colMainUnit).Value = ""
                gv1.CurrentRow.Cells(colMainMrp).Value = ""
            End If
        End If
    End Sub

    Sub OpenSchemeICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = Nothing
        Dim str_Item_Price_ID = ""
        Dim qry As String = ""
        qry += " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
        qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "

        Dim WhrCls As String = ""
        'If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
        '    WhrCls = "T1.Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "'"
        'End If

        str_Item_Price_ID = clsCommon.ShowSelectForm("ItemFinder", qry, "Item_Price_ID", "", "", "Item_Price_ID", isButtonClick)
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry += " select DISTINCT  T1.Item_Price_ID ,T1.Item_Code, T1.Item_Basic_Net,T2.Item_Desc,T2.Unit_Code from TSPL_ITEM_PRICE_MASTER T1 "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T1.Item_Code = T2.Item_Code  "
            qry += " where T1.Item_Price_ID ='" + str_Item_Price_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colschItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                gv1.CurrentRow.Cells(colschDescription).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                gv1.CurrentRow.Cells(colschUnit).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                gv1.CurrentRow.Cells(colschMrp).Value = clsCommon.myCstr(dt.Rows(0)("Item_Basic_Net"))
            Else
                gv1.CurrentRow.Cells(colschItemCode).Value = ""
                gv1.CurrentRow.Cells(colschDescription).Value = ""
                gv1.CurrentRow.Cells(colschUnit).Value = ""
                gv1.CurrentRow.Cells(colschMrp).Value = ""
            End If
        End If
    End Sub

    Private Sub exportmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exportmenu.Click
        Dim query As String = "SELECT TSPL_SCHEME_MASTER.Scheme_Code as 'Scheme Code', TSPL_SCHEME_MASTER.Scheme_Desc as 'Scheme Description', TSPL_SCHEME_MASTER.Start_Date as 'Start Date',TSPL_SCHEME_MASTER.End_Date as 'End date', TSPL_SCHEME_MASTER.Scheme_Type as 'Scheme Type', TSPL_SCHEME_MASTER.Main_Item_Code as 'Main Itme Code', TSPL_SCHEME_MASTER.Main_Item_desc as 'Main Item Description', TSPL_SCHEME_MASTER.Main_Item_Qty as 'Main Item Qty', TSPL_SCHEME_MASTER.Amount as 'Amount',TSPL_SCHEME_MASTER.Comments as 'Comments', TSPL_SCHEME_MASTER.Main_Item_UOM as 'Main Item Uom', TSPL_SCHEME_MASTER.MRP as 'MRP' ,TSPL_SCHEME_DETAILS.Scheme_Code_Auto as 'Scheme Code Auto',TSPL_SCHEME_DETAILS.Scheme_Item_Code as 'Scheme Item Code', TSPL_SCHEME_DETAILS.Scheme_Item_Desc as 'Scheme Item Description', TSPL_SCHEME_DETAILS.Qty as 'Qty',TSPL_SCHEME_DETAILS.Remarks as 'Remarks', TSPL_SCHEME_DETAILS.UOM as 'UOM', TSPL_SCHEME_DETAILS.MRP AS Mrp1,TSPL_SCHEME_DETAILS.Price_Date as 'Price date'FROM  TSPL_SCHEME_MASTER INNER JOIN TSPL_SCHEME_DETAILS ON TSPL_SCHEME_MASTER.Scheme_Code = TSPL_SCHEME_DETAILS.Scheme_Code"
        transportSql.ExporttoExcel(query, Me)
    End Sub

    Private Sub importmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importmenu.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Scheme Code", "Scheme Description", "Start Date", "End date", "Scheme Type", "Main Itme Code", "Main Item Description", "Main Item Qty", "Amount", "Comments", "Main Item Uom", "MRP", "Scheme Code Auto", "Scheme Item Code", "Scheme Item Description", "Qty", "Remarks", "UOM", "Mrp1", "Price date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                ''connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    Dim strscheme_code As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    If clsCommon.myLen(strscheme_code) > 12 Or String.IsNullOrEmpty(strscheme_code) Then
                        Throw New Exception("Check the length of scheme Code/Blank")

                    End If
                    Dim strscheme_description As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    If clsCommon.myLen(strscheme_description) > 100 Then
                        Throw New Exception("Check the length of Scheme Description / Blank")

                    End If
                    Dim strscheme_type As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    If clsCommon.myLen(strscheme_type) > 10 Or strscheme_type = "" Then
                        Throw New Exception("Check the Length of Scheme type/Blank ")

                    End If
                    Dim strmain_item_code As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    If clsCommon.myLen(strmain_item_code) > 50 Then
                        Throw New Exception("Check the Length of main item code/Blank")

                    End If
                    Dim strmain_itemcode_desc As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                    If clsCommon.myLen(strmain_itemcode_desc) > 100 Then
                        Throw New Exception("Check the length of main item code description/blank")

                    End If
                    Dim strmain_item_qty As Decimal = CDec(dgrv.Cells(7).Value.ToString())
                    If strmain_item_qty < 0 Then
                        Throw New Exception("main item qty should not be blank")

                    End If
                    Dim stramount As Decimal = CDec(dgrv.Cells(8).Value.ToString())
                    If stramount < 0 Then
                        Throw New Exception("Amount should not be blank")

                    End If
                    Dim strcomments As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                    If clsCommon.myLen(strcomments) > 200 Then
                        Throw New Exception("Check the length of Comments")

                    End If
                    Dim strmainitem_uom As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                    If clsCommon.myLen(strmainitem_uom) > 12 Then
                        Throw New Exception("Check the length of Main item Uom")

                    End If
                    Dim strschemeitem_code As String = clsCommon.myCstr(dgrv.Cells(13).Value)
                    If clsCommon.myLen(strschemeitem_code) > 12 Or String.IsNullOrEmpty(strschemeitem_code) Then
                        Throw New Exception("Check the length of Scheme Item Code/blank")

                    End If
                    Dim strschemeitem_desc As String = clsCommon.myCstr(dgrv.Cells(14).Value)
                    If clsCommon.myLen(strschemeitem_desc) > 100 Then
                        Throw New Exception("Check the length of scheme itme descriiption")

                    End If
                    Dim strremarks As String = clsCommon.myCstr(dgrv.Cells(16).Value)
                    If clsCommon.myLen(strremarks) > 200 Then
                        Throw New Exception("Check the length of Remarks")

                    End If
                    Dim struom As String = clsCommon.myCstr(dgrv.Cells(17).Value)
                    If clsCommon.myLen(struom) > 12 Then
                        Throw New Exception("Check the length of Uom")

                    End If
                    Dim mrp1 As Double = clsCommon.myCdbl(dgrv.Cells(18).Value)
                    Dim start_date As String = Format(dgrv.Cells(2).Value, "dd/MMM/yyyy")
                    Dim end_date As String = Nothing
                    If clsCommon.myLen(dgrv.Cells(3).Value) > 0 Then
                        end_date = Format(dgrv.Cells(3).Value, "dd/MMM/yyyy")
                    End If


                    Dim mrp As Double = clsCommon.myCdbl(dgrv.Cells(11).Value)
                    Dim schemecode_auto As Integer = CInt(dgrv.Cells(12).Value.ToString)
                    Dim qty As Integer = CInt(dgrv.Cells(15).Value.ToString)


                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "scheme_code", strscheme_code)
                        clsCommon.AddColumnsForChange(coll, "scheme_desc", strscheme_description)
                        clsCommon.AddColumnsForChange(coll, "start_date", start_date)
                        clsCommon.AddColumnsForChange(coll, "end_date", end_date, True)
                        clsCommon.AddColumnsForChange(coll, "scheme_type", strscheme_type)
                        clsCommon.AddColumnsForChange(coll, "main_item_code", strmain_item_code)
                        clsCommon.AddColumnsForChange(coll, "main_item_desc", strmain_itemcode_desc)
                        clsCommon.AddColumnsForChange(coll, "main_item_qty", strmain_item_qty)
                        clsCommon.AddColumnsForChange(coll, "amount", stramount)
                        clsCommon.AddColumnsForChange(coll, "comments", strcomments)
                        clsCommon.AddColumnsForChange(coll, "created_by", userCode)
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
                        clsCommon.AddColumnsForChange(coll, "modify_by", userCode)
                        clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
                        clsCommon.AddColumnsForChange(coll, "comp_code", companyCode)
                        clsCommon.AddColumnsForChange(coll, "main_item_uom", strmainitem_uom)
                        clsCommon.AddColumnsForChange(coll, "mrp", mrp)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        'Dim query As String = "insert into TSPL_SCHEME_MASTER (scheme_code,scheme_desc,start_date,end_date,scheme_type,main_item_code,main_item_desc,main_item_qty,amount,comments,created_by,created_date,modify_by,modify_date,comp_code,main_item_uom,mrp)values('" + clsCommon.myCstr(strscheme_code) + "','" + clsCommon.myCstr(strscheme_description) + "','" + start_date + "','" + end_date + "','" + clsCommon.myCstr(strscheme_type.ToString()) + "','" + clsCommon.myCstr(strmain_item_code) + "','" + clsCommon.myCstr(strmain_itemcode_desc) + "','" + strmain_item_qty + "','" + stramount + "','" + strcomments.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + clsCommon.myCstr(strmainitem_uom) + "','" + clsCommon.myCstr(mrp) + "')"
                        ''connectSql.RunSqlTransaction(trans, query)
                    Catch ex As Exception
                        Dim s1 As String = ex.ToString().Substring(0, 46)
                        If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " OrElse ex.Message.ToString().Substring(0, 35) = "Violation of PRIMARY KEY constraint" Then
                            'Dim query1 As String = "update TSPL_SCHEME_MASTER set scheme_desc='" + strscheme_description + "',start_date='" + start_date.ToString() + "',End_date='" + end_date.ToString() + "',scheme_type='" + strscheme_type.ToString() + "',main_item_code='" + strmain_item_code.ToString() + "',main_item_desc='" + strmain_itemcode_desc.ToString() + "',main_item_qty='" + strmain_item_qty.ToString() + "',Amount='" + stramount.ToString() + "',commnets='" + strcomments.ToString() + "',main_item_uom='" + strmainitem_uom.ToString() + "',mrp='" + mrp.ToString() + "'where scheme_code='" + strscheme_code.ToString() + "'"
                            'connectSql.RunSqlTransaction(trans, query1)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Update, "scheme_code='" + strscheme_code + "'", trans)
                        End If
                    End Try
                    'Dim col1 As New Hashtable
                    'Try
                    '    'clsCommon.AddColumnsForChange(col1, "")
                    '    ''    Dim query2 As String = "insert into tspl_scheme_details values('" + schemecode_auto.ToString() + "','" + strscheme_code.ToString() + "','" + strscheme_description.ToString() + "','" + strschemeitem_code.ToString() + "','" + strschemeitem_desc.ToString() + "','" + qty.ToString() + "','" + strremarks.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + struom.ToString() + "','" + mrp1.ToString() + "','" + price_date1 + "'"
                    '    ''    connectSql.RunSqlTransaction(trans, query2)
                    'Catch ex As Exception
                    '    ''    Dim s1 As String = ex.ToString().Substring(0, 46)
                    '    ''    If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " Then
                    '    ''      
                    '    ''    End If
                    'End Try
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)


            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

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
        repoItemCode.HeaderText = "Main Item Code"
        repoItemCode.Name = colMainItemCode
        repoItemCode.Width = 100
        repoItemCode.ReadOnly = False
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc.FormatString = ""
        repoDesc.HeaderText = "Description"
        repoDesc.Name = colMainDescription
        repoDesc.Width = 200
        repoDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDesc)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colMainUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoMRP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMainMrp
        repoMRP.Width = 100
        repoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colMainRate
        repoRate.Width = 100
        repoRate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim SchrepoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchrepoItemCode.FormatString = ""
        SchrepoItemCode.HeaderText = "Scheme Item Code"
        SchrepoItemCode.Name = colschItemCode
        SchrepoItemCode.Width = 100
        SchrepoItemCode.ReadOnly = False
        SchrepoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(SchrepoItemCode)

        Dim SchrepoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchrepoDesc.FormatString = ""
        SchrepoDesc.HeaderText = "Description"
        SchrepoDesc.Name = colschDescription
        SchrepoDesc.Width = 200
        SchrepoDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SchrepoDesc)

        Dim SchrepoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchrepoUnit.FormatString = ""
        SchrepoUnit.HeaderText = "Unit"
        SchrepoUnit.Name = colschUnit
        SchrepoUnit.Width = 100
        SchrepoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SchrepoUnit)

        Dim SchrepoMRP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchrepoMRP.FormatString = ""
        SchrepoMRP.HeaderText = "MRP"
        SchrepoMRP.Name = colschMrp
        SchrepoMRP.Width = 100
        SchrepoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SchrepoMRP)

        Dim SchrepoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        SchrepoRate.FormatString = ""
        SchrepoRate.HeaderText = "Rate"
        SchrepoRate.Name = colschRate
        SchrepoRate.Width = 100
        SchrepoRate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(SchrepoRate)

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

        'Dim Qry As String = "select StdSchCode as [Code],FomeDate as [Date],Valied_Date as [Valied Till Date],Cust_Code as [Customer],Descraption from TSPL_STANDARD_SCHEME "
        'fndCode.Value = clsCommon.ShowSelectForm("frmStandardscheme", Qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
        fndCode.Value = clsStandardScheme.getFinder("", fndCode.Value, isButtonClicked)
        If clsCommon.myLen(fndCode.Value) > 0 Then
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
        'Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "' "
    End Sub

    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim Qry As String = "select Cust_Code as [Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(case when Status='N' then 'Active' else 'InActive' end ) as [Status] from TSPL_CUSTOMER_MASTER"
        fndCustomer.Value = clsCommon.ShowSelectForm("fmParent_Customer_No", Qry, "Code", "", fndCustomer.Value, "Code", isButtonClicked)
        Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "' "
    End Sub

End Class