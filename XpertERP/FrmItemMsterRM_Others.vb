Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Imports common
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices.Marshal

Imports System.IO
Imports System.Configuration
Imports System

Imports System.Collections.Generic
Imports System.ComponentModel

Imports Excel = Microsoft.Office.Interop.Excel

Public Class FrmItemMsterRM_Others
    Inherits FrmMainTranScreen

    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim btntooltip As ToolTip = New ToolTip()
    Dim QrySheet As String
    Dim QrySpcl As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Const colitemno As String = "itemCode"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub FrmItemMsterRM_Others_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmItemMasterRMOther)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave1.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete1.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Dim gv1 As New RadGridView()
        Dim gv2 As New RadGridView()
        Dim isSaved As Boolean = True
        Me.Controls.Add(gv)
        Me.Controls.Add(gv1)
        Me.Controls.Add(gv2)

        Dim ofd As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String
        ofd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            filePath = ofd.FileName
        Else
            Return
        End If
        clsCommon.ProgressBarShow()
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        Try

            trans = clsDBFuncationality.GetTransactin()
            ''-Insertig for ITem MAster
            Dim COUNTER As Integer = 0
            If transportSql.importExcelForItemMaster("ItemMaster$", gv, gv1, gv2, filePath, "Item Code", "Item Description", "Structure Code", "Structure Description", "Purchase Class Code", "Sales Class Code", "Unit Code", "Default Price", "Father Code", "Father Quantity", "Cheapter Heads", "Mother Code", "Mother Quantity", "Opening Balance", "Two Count Status", "Three Count Status", "Server Type", "Mfg Date", "Batch Number", "Best Before Use Date", "Item Type", "Flavor Seq", "Pack Seq", "Sku Seq", "Show", "Item Category", "Tolerance", "Shelf Life", "Cost", "Sub Item Category", "Type Of Item", "No MRP") Then
                For Each grow As GridViewRowInfo In gv.Rows
                    COUNTER += 1
                    Dim stritemcode As String = clsCommon.myCstr(grow.Cells(0).Value).Trim()
                    Dim stritemdesc As String = clsCommon.myCstr(grow.Cells(1).Value).Trim()
                    Dim strstructcode As String = clsCommon.myCstr(grow.Cells(2).Value).Trim()
                    Dim strstructdesc As String = clsCommon.myCstr(grow.Cells(3).Value).Trim()
                    Dim strpurchaseclasscode As String = clsCommon.myCstr(grow.Cells(4).Value).Trim()
                    Dim strsaleclasscode As String = clsCommon.myCstr(grow.Cells(5).Value).Trim()
                    Dim strunitcode As String = clsCommon.myCstr(grow.Cells(6).Value).Trim()
                    Dim strdefaultprice As String = clsCommon.myCstr(grow.Cells(7).Value).Trim()
                    Dim strfathercode As String = clsCommon.myCstr(grow.Cells(8).Value).Trim()
                    Dim strfatherqty As String = clsCommon.myCstr(grow.Cells(9).Value).Trim()
                    Dim strchapterhead As String = clsCommon.myCstr(grow.Cells(10).Value).Trim()
                    Dim strmothercode As String = clsCommon.myCstr(grow.Cells(11).Value).Trim()
                    Dim strmotherqty As String = clsCommon.myCstr(grow.Cells(12).Value).Trim()
                    Dim stropnbal As String = clsCommon.myCstr(grow.Cells(13).Value).Trim()
                    Dim strtwocount As String = clsCommon.myCstr(grow.Cells(14).Value).Trim()
                    Dim strthreecount As String = clsCommon.myCstr(grow.Cells(15).Value).Trim()
                    Dim strservetype As String = clsCommon.myCstr(grow.Cells(16).Value).Trim()
                    Dim strmfg_date As String = clsCommon.myCstr(grow.Cells(17).Value).Trim()
                    Dim strbatch_number As String = clsCommon.myCstr(grow.Cells(18).Value).Trim()
                    Dim strbest_before_usedate As String = clsCommon.myCstr(grow.Cells(19).Value).Trim()
                    Dim stritemtype As String = clsCommon.myCstr(grow.Cells(20).Value).Trim()
                    Dim strflavourseq As String = clsCommon.myCstr(grow.Cells(21).Value).Trim()
                    Dim strpackseq As String = clsCommon.myCstr(grow.Cells(22).Value).Trim()
                    Dim strskuseq As String = clsCommon.myCstr(grow.Cells(23).Value).Trim()
                    Dim strshow As String = clsCommon.myCstr(grow.Cells(24).Value.ToString()).Trim()
                    Dim strItemCategory As String = clsCommon.myCstr(grow.Cells(25).Value.ToString()).Trim()
                    Dim strTolerance As String = clsCommon.myCstr(grow.Cells(26).Value).Trim()
                    Dim strshelf As Double = clsCommon.myCdbl(grow.Cells(27).Value)
                    Dim strcost As Double = clsCommon.myCdbl(grow.Cells(28).Value)
                    Dim strSubItemCategory As String = clsCommon.myCstr(grow.Cells(29).Value.ToString()).Trim()
                    Dim stritemoftype As String = clsCommon.myCstr(grow.Cells(30).Value.ToString()).Trim()
                    Dim strnomrp As String = clsCommon.myCstr(grow.Cells(31).Value.ToString()).Trim()
                    Dim Morning As Integer = grow.Cells(37).Value

                    If clsCommon.myLen(strflavourseq) <= 0 Then
                        strflavourseq = 0
                    End If
                    If clsCommon.myLen(strpackseq) <= 0 Then
                        strpackseq = 0
                    End If
                    If clsCommon.myLen(strskuseq) <= 0 Then
                        strskuseq = 0
                    End If
                    If clsCommon.myLen(stritemcode) = 0 OrElse clsCommon.myLen(stritemcode) > 50 Then

                        Throw New Exception("Item Code has some incorrect values at Row -- " + clsCommon.myCstr(COUNTER + 2) + " ")
                    End If
                    If clsCommon.myLen(stritemdesc) <= 0 OrElse clsCommon.myLen(stritemdesc) > 100 Then
                        Throw New Exception("Item Description has some incorrect values")
                    End If
                    If clsCommon.myLen(strstructcode) <= 0 OrElse clsCommon.myLen(strstructcode) > 12 Then
                        Throw New Exception("Structure Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strstructdesc) <= 0 OrElse clsCommon.myLen(strstructdesc) > 50 Then
                        Throw New Exception("Structure Description has some incorrect values")
                    End If
                    If clsCommon.myLen(strpurchaseclasscode) <= 0 OrElse clsCommon.myLen(strpurchaseclasscode) > 6 Then
                        Throw New Exception("Purchase Class Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strsaleclasscode) <= 0 OrElse clsCommon.myLen(strsaleclasscode) > 50 Then
                        Throw New Exception("Sales Class Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strunitcode) <= 0 OrElse clsCommon.myLen(strunitcode) > 12 Then
                        Throw New Exception("Unit Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strdefaultprice) > 18 Then
                        Throw New Exception("Default Price has some incorrect values")
                    End If
                    If clsCommon.myLen(strfathercode) > 50 Then
                        Throw New Exception("Father code has some incorrect values")
                    End If
                    If clsCommon.myLen(strfatherqty) > 18 Then
                        Throw New Exception("Father Quantity has some incorrect values")
                    End If
                    If clsCommon.myLen(strchapterhead) > 50 Then
                        Throw New Exception("Chapter Head has some incorrect values")
                    End If
                    If clsCommon.myLen(strmothercode) > 50 Then
                        Throw New Exception("Mother Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strmotherqty) > 18 Then
                        Throw New Exception("Mother  Quantity has some incorrect values")
                    End If
                    If clsCommon.myLen(stropnbal) > 18 Then
                        Throw New Exception("Opening Balance has some incorrect values")
                    End If
                    If clsCommon.myLen(strtwocount) > 1 Then
                        Throw New Exception("Two Count Status has some incorrect values")
                    End If
                    If clsCommon.myLen(strthreecount) > 1 Then
                        Throw New Exception("Three Count Status has some incorrect values")
                    End If
                    If clsCommon.myLen(stritemtype) <= 0 Then
                        Throw New Exception("Item  Type has some incorrect values")
                    End If
                    If clsCommon.myLen(strflavourseq) <= 0 OrElse clsCommon.myLen(strflavourseq) > 16 Then
                        Throw New Exception("Flavour Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(strpackseq) <= 0 OrElse clsCommon.myLen(strpackseq) > 16 Then
                        Throw New Exception("Pack Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(strskuseq) <= 0 OrElse clsCommon.myLen(strskuseq) > 16 Then
                        Throw New Exception("SKU Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(companyCode) <= 0 Then
                        Throw New Exception("Please Enter Company Code")
                    End If

                    If clsCommon.myLen(stritemoftype) > 2 Then
                        Throw New Exception("Type of Item have incorrect value")
                    End If
                    If clsCommon.myLen(strnomrp) > 1 Then
                        Throw New Exception("No MRP have incorrect value")
                    End If
                    If clsCommon.myLen(Morning) > 1 Then
                        Throw New Exception("Please Insert Morning as Integer (1 or 0)")
                    End If


                    Dim qry As String = "select 1 from TSPL_ITEM_MASTER where Item_Code ='" + stritemcode + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", stritemdesc)
                    clsCommon.AddColumnsForChange(coll, "Structure_Code", strstructcode)
                    clsCommon.AddColumnsForChange(coll, "Structure_Desc", strstructdesc)
                    clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", strpurchaseclasscode)
                    clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", strsaleclasscode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", strunitcode)
                    clsCommon.AddColumnsForChange(coll, "Deafult_Price", strdefaultprice)
                    clsCommon.AddColumnsForChange(coll, "Father_Code", strfathercode)
                    clsCommon.AddColumnsForChange(coll, "Father_QTy", strfatherqty)
                    clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", strchapterhead)
                    clsCommon.AddColumnsForChange(coll, "Mother_Code", strmothercode)
                    clsCommon.AddColumnsForChange(coll, "Mother_Qty", strmotherqty)
                    clsCommon.AddColumnsForChange(coll, "Opening_Balance", stropnbal)
                    clsCommon.AddColumnsForChange(coll, "Two_Count_Status", strtwocount)
                    clsCommon.AddColumnsForChange(coll, "Three_Count_Status", strthreecount)
                    clsCommon.AddColumnsForChange(coll, "Server_Type", strservetype)
                    clsCommon.AddColumnsForChange(coll, "Mfg_date", strmfg_date)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", strbatch_number)
                    clsCommon.AddColumnsForChange(coll, "Best_Befor_UseDate", strbest_before_usedate)
                    clsCommon.AddColumnsForChange(coll, "item_type", stritemtype)
                    clsCommon.AddColumnsForChange(coll, "Created_By", userCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", userCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", companyCode)
                    clsCommon.AddColumnsForChange(coll, "Flavour_Seq", strflavourseq)
                    clsCommon.AddColumnsForChange(coll, "Pack_Seq", strpackseq)
                    clsCommon.AddColumnsForChange(coll, "Sku_Seq", strskuseq)
                    clsCommon.AddColumnsForChange(coll, "show", strshow)
                    clsCommon.AddColumnsForChange(coll, "item_category", strItemCategory)
                    clsCommon.AddColumnsForChange(coll, "tolerence", clsCommon.myCdbl(strTolerance))
                    clsCommon.AddColumnsForChange(coll, "tech_shelf_life", strshelf)
                    clsCommon.AddColumnsForChange(coll, "Cost", strcost)
                    clsCommon.AddColumnsForChange(coll, "Sub_item_category", strSubItemCategory)
                    clsCommon.AddColumnsForChange(coll, "TypeOfItm", stritemoftype)
                    clsCommon.AddColumnsForChange(coll, "NOMRP", strnomrp)
                    clsCommon.AddColumnsForChange(coll, "Morning", Morning)

                    If isNewEntry Then

                        clsCommon.AddColumnsForChange(coll, "Item_Code", stritemcode)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "Item_Code='" + stritemcode + "'", trans)
                    End If
                Next
            End If

            ''-Insertig for ITem Details
            If transportSql.importExcelForItemMaster("ItemDetails$", gv, gv1, gv2, filePath, "Item Code", "Class Code", "Class Name", "Class Description", "Created By", "Created Date", "Modify By", "Modify Date", "Comp Code") Then
                Dim counter1 As Integer = 0
                For Each grow1 As GridViewRowInfo In gv1.Rows
                    counter1 += 1
                    Dim strIDItemCode As String = clsCommon.myCstr(grow1.Cells(0).Value)
                    Dim strIDClassCode As String = clsCommon.myCstr(grow1.Cells(1).Value)
                    Dim strIDClassName As String = clsCommon.myCstr(grow1.Cells(2).Value)
                    Dim strIDClassDesc As String = clsCommon.myCstr(grow1.Cells(3).Value)

                    Dim sql2 As String = "select 1 from TSPL_ITEM_DETAILS where Item_Code = '" + strIDItemCode + "' and class_code='" + strIDClassCode + "'and class_name='" + strIDClassName + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql2, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "class_code", strIDClassCode)
                    clsCommon.AddColumnsForChange(coll, "class_name", strIDClassName)
                    clsCommon.AddColumnsForChange(coll, "class_desc", strIDClassDesc)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", strIDItemCode)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + strIDItemCode + "'and class_code='" + strIDClassCode + "'and class_name='" + strIDClassName + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_DETAILS", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
            End If

            ''-Insertig for ITem UOM Details
            If transportSql.importExcelForItemMaster("ItemUOMDetails$", gv, gv1, gv2, filePath, "Item Code", "UOM Code", "UOM_Description", "Conversion_Factor", "Stocking_Unit") Then
                Dim counter2 As Integer = 0
                For Each grow2 As GridViewRowInfo In gv2.Rows
                    counter2 += 1
                    Dim strIUDIemCode As String = clsCommon.myCstr(grow2.Cells(0).Value)
                    Dim strIUDUomCode As String = clsCommon.myCstr(grow2.Cells(1).Value)
                    Dim strIUDUomDesc As String = clsCommon.myCstr(grow2.Cells(2).Value)
                    Dim strIUDConvFact As String = clsCommon.myCstr(grow2.Cells(3).Value)
                    Dim strIUDStockingUnit As String = clsCommon.myCstr(grow2.Cells(4).Value)

                    Dim sql3 As String = "select 1 from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strIUDIemCode + "' and uom_code='" + strIUDUomCode + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql3, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "UOM_Description", strIUDUomDesc)
                    clsCommon.AddColumnsForChange(coll, "Conversion_Factor", strIUDConvFact)
                    clsCommon.AddColumnsForChange(coll, "Stocking_Unit", strIUDStockingUnit)

                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", strIUDIemCode)
                        clsCommon.AddColumnsForChange(coll, "UOM_Code", strIUDUomCode)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + strIUDIemCode + "' and uom_code='" + strIUDUomCode + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
            End If

            If isSaved Then
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Imported Successfully ...", Me.Text)
            Else
                trans.Rollback()
                Throw New Exception("Error in Import")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
            Me.Controls.Remove(gv1)
            Me.Controls.Remove(gv2)
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub RMIExportBlank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIExportBlank.Click
        QrySheet = " Where 1=2"
        QrySpcl = "Blank"
        ExportItem(QrySheet)
    End Sub

    Private Sub RMIexportRaw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIexportRaw.Click
        QrySheet = " Where Item_Code in (Select Item_Code from TSPL_ITEM_MASTER Where Item_Type='R')"
        ExportItem(QrySheet)
    End Sub

    Private Sub RMIOthers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIOthers.Click
        QrySheet = " "
        ExportItem(QrySheet)
    End Sub
    Public Sub ExportItem(ByVal qry1 As String)
        clsCommon.ProgressBarShow()
        Dim x As Integer
        ' x = dataGrd.Rows.Count - 1
        Dim y As Integer
        ' y = dataGrd.Columns.Count
        Dim i As Integer
        Dim j As Integer
        x = 2
        y = 2
        i = 0
        j = 0
        Dim intRow As Integer
        Dim intColumn As Integer

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlWorkSheetBiLL As Excel.Worksheet
        Dim xlWorkSheetBiLLS As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)



        xlWorkSheet = xlWorkBook.Sheets("sheet1")
        xlWorkSheetBiLL = xlWorkBook.Sheets("sheet2")
        xlWorkSheetBiLLS = xlWorkBook.Sheets("sheet3")


        Dim myDataTable1 As DataTable
        Dim myDataTable2 As DataTable
        Dim myDataTable3 As DataTable

        Dim strQ1 As String
        Dim strQ2 As String
        Dim strQ3 As String
        strQ1 = "select * from TSPL_ITEM_MASTER " + qry1 + ""

        myDataTable1 = clsDBFuncationality.GetDataTable(strQ1)


        If myDataTable1.Rows.Count > 0 Or QrySpcl = "Blank" Then
            ' xlWorkSheet.Cells(1, 6) = "Item Master"
            xlWorkSheet.Cells(2, 1) = "Item Code"
            xlWorkSheet.Cells(2, 2) = "Item Description"
            xlWorkSheet.Cells(2, 3) = "Structure Code"
            ' xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
            xlWorkSheet.Cells(2, 4) = "Structure Description"
            xlWorkSheet.Cells(2, 5) = "Purchase Class Code"
            xlWorkSheet.Cells(2, 6) = "Sales Class Code"
            xlWorkSheet.Cells(2, 7) = "Unit Code"
            xlWorkSheet.Cells(2, 8) = "Default Price"
            xlWorkSheet.Cells(2, 9) = "Father Code"
            xlWorkSheet.Cells(2, 10) = "Father Quantity"
            xlWorkSheet.Cells(2, 11) = "Cheapter Heads"
            xlWorkSheet.Cells(2, 12) = "Mother Code"
            xlWorkSheet.Cells(2, 13) = "Mother Quantity"
            xlWorkSheet.Cells(2, 14) = "Opening Balance"
            xlWorkSheet.Cells(2, 15) = "Two Count Status"
            xlWorkSheet.Cells(2, 16) = "Three Count Status"
            xlWorkSheet.Cells(2, 17) = "Server Type"
            xlWorkSheet.Cells(2, 18) = "Mfg Date"
            xlWorkSheet.Cells(2, 19) = "Batch Number"
            xlWorkSheet.Cells(2, 20) = "Best Before Use Date"
            xlWorkSheet.Cells(2, 21) = "Item Type"
            xlWorkSheet.Cells(2, 22) = "Created By"
            xlWorkSheet.Cells(2, 23) = "Created Date"
            xlWorkSheet.Cells(2, 24) = "Mofify By"
            xlWorkSheet.Cells(2, 25) = "Modify Date"
            xlWorkSheet.Cells(2, 26) = "Comp Code"
            xlWorkSheet.Cells(2, 27) = "Flavor Seq"
            xlWorkSheet.Cells(2, 28) = "Pack Seq"
            xlWorkSheet.Cells(2, 29) = "Sku Seq"
            xlWorkSheet.Cells(2, 30) = "Show"
            xlWorkSheet.Cells(2, 31) = "Item Category"
            xlWorkSheet.Cells(2, 32) = "Tolerance"
            xlWorkSheet.Cells(2, 33) = "Shelf Life"
            xlWorkSheet.Cells(2, 34) = "Cost"
            xlWorkSheet.Cells(2, 35) = "Sub Item Category"
            xlWorkSheet.Cells(2, 36) = "Type Of Item"
            xlWorkSheet.Cells(2, 37) = "No MRP"
            xlWorkSheet.Cells(2, 38) = "Morning"

            For intRow = 0 To myDataTable1.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable1.Columns.Count - 1
                    y = y + 1
                    Dim test As String = myDataTable1.Rows(i).Item(j).ToString()
                    xlWorkSheet.Cells(x, y) = test

                    j += 1
                Next
                i = i + 1
            Next
        Else
            Exit Sub
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

        'xlWorkSheet.Cells(1, 1) = dataGrd.Item(1, 0).Value 

        ' strQ2 = "select * from TSPL_ITEM_DETAILS"
        strQ2 = "select Item_Code,CAST (class_code as varchar(100)) as class_code,Class_Name ,Class_Desc ,Created_By ,Created_Date ,Modify_By ,Modify_Date ,Comp_Code   from TSPL_ITEM_DETAILS " + QrySheet + ""

        myDataTable2 = clsDBFuncationality.GetDataTable(strQ2)

        If myDataTable2.Rows.Count > 0 Or QrySpcl = "Blank" Then
            xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
            ' xlWorkSheetBiLL.Cells(1, 6) = "Item Details"
            xlWorkSheetBiLL.Cells(2, 1) = "Item Code"
            xlWorkSheetBiLL.Cells(2, 2) = "Class Code"
            xlWorkSheetBiLL.Cells(2, 2).ToString()
            xlWorkSheetBiLL.Cells(2, 3) = "Class Name"
            'xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
            xlWorkSheetBiLL.Cells(2, 4) = "Class Description"
            xlWorkSheetBiLL.Cells(2, 5) = "Created By"
            xlWorkSheetBiLL.Cells(2, 6) = "Created Date"
            xlWorkSheetBiLL.Cells(2, 7) = "Modify By"
            xlWorkSheetBiLL.Cells(2, 8) = "Modify Date"
            xlWorkSheetBiLL.Cells(2, 9) = "Comp Code"

            x = 2
            y = 2
            i = 0
            j = 0

            For intRow = 0 To myDataTable2.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable2.Columns.Count - 1
                    y = y + 1
                    'If y = 4 Then
                    '    y = y + 1
                    'End If
                    Dim test As String = myDataTable2.Rows(i).Item(j).ToString()
                    xlWorkSheetBiLL.Cells(x, y) = test
                    j += 1
                Next
                i = i + 1
            Next

            xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
            ' xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "General")
            '            Dim misValue1 = System.Reflection.Missing.Value
            'Dim  range  As  Excel.Range = (Excel.Range)xlWorkSheetBiLL.Columns["B", misValue1] = "@"
            '            Rang()
            ' Range("A1:A100").NumberFormat = "@"
            'xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "@"




        End If


        strQ3 = "select * from tspl_item_uom_detail " + QrySheet + ""
        myDataTable3 = clsDBFuncationality.GetDataTable(strQ3)

        If myDataTable3.Rows.Count > 0 Or QrySpcl = "Blank" Then
            ' xlWorkSheetBiLLS.Cells(1, 6) = "Item UOM Details"
            xlWorkSheetBiLLS.Cells(2, 1) = "Item Code"
            xlWorkSheetBiLLS.Cells(2, 2) = "UOM Code"
            xlWorkSheetBiLLS.Cells(2, 3) = "UOM_Description"
            '  xlWorkSheetBiLLS.Cells.EntireColumn(4).Hidden = True
            xlWorkSheetBiLLS.Cells(2, 4) = "Conversion_Factor"
            xlWorkSheetBiLLS.Cells(2, 5) = "Stocking_Unit"
            x = 2
            y = 2
            i = 0
            j = 0
            For intRow = 0 To myDataTable3.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable3.Columns.Count - 1
                    y = y + 1
                    'If y = 4 Then
                    '    y = y + 1
                    'End If
                    Dim test As String = myDataTable3.Rows(i).Item(j).ToString()
                    xlWorkSheetBiLLS.Cells(x, y) = test
                    j += 1
                Next
                i = i + 1
            Next

        End If
        'xlWorkBook.Sheets("Sheet1").Range("a1").Value = "Item Master"
        'xlWorkBook.Sheets("Sheet2").Range("a1").Value = "Item Details"
        'xlWorkBook.Sheets("Sheet3").Range("a1").Value = "Item UOM Details"

        'Dim excelWorkSheet As Excel.Worksheet

        xlWorkSheet = xlWorkBook.Sheets(1)
        xlWorkSheet.Name = "ItemMaster"

        xlWorkSheetBiLL = xlWorkBook.Sheets(2)
        xlWorkSheetBiLL.Name = "ItemDetails"

        xlWorkSheetBiLLS = xlWorkBook.Sheets(3)
        xlWorkSheetBiLLS.Name = "ItemUOMDetails"

        'xlWorkSheet.Name = "Item Master"
        'xlWorkSheetBiLL.Name = "Item Details"
        'xlWorkSheetBiLLS.Name = "Item UOM Details"




        Dim sfd As SaveFileDialog = New SaveFileDialog()
        ' Dim path As String
        sfd.FileName = Me.Text
        Dim strFileName As String
        sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        clsCommon.ProgressBarHide()
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strFileName = sfd.FileName
            clsCommon.ProgressBarShow()
            xlWorkSheet.SaveAs("" + strFileName + "")

            xlWorkBook.Close()
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkSheetBiLL)
            releaseObject(xlWorkSheetBiLLS)

            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, "Data Exported Successfully ...", Me.Text)
        Else
            MsgBox("No Record")
        End If
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Class





