Imports System.Data.SqlClient
Imports common
Imports System.Text

Public Class frmOutgoingQCEntry
    Inherits FrmMainTranScreen
    'Const colSelect As String = "Select"
    Const colSno As String = "Sno"
    Const colProductCode As String = "Productcode"
    Const colproductdate As String = "Productdate"
    Const colitemcode As String = "ItemCode"
    Const colitemname As String = "itemname"
    Const colQCCode As String = "QCCode"
    Const colQCName As String = "QCName"
    Const colLrange As String = "Lrange"
    Const colUrange As String = "Urange"
    Const colbomcode As String = "bomcode"
    Const colobservedvalue As String = "ObservedValue"
    Const collotno As String = "Lotno"
    Const colbagunit As String = "Bagunit"
    Const colProductQty As String = "ProductQty"
    Const colshift As String = "Shift"
    Const colItemDesc As String = "Itemdesc"
    Const coldescription As String = "Description"
    Const colremarks As String = "remarks"
    Const colCompiledUn As String = "Compiled"
    Const colUnitCode As String = "UnitCode"
    Const colInputValue As String = "InputValue"
    Const colProductioncode As String = "ProductionCode"
    Const colBomcodedetail As String = "Bomcodedetail"
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Public Template_Status As String = Nothing

    Private Sub frmOutgoingQCEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtlocation.Value = ""
        TxtFgcode.Value = ""
        docDate.Value = clsCommon.GETSERVERDATE()
        LoadBlankGrid()
        addnew()
        brnSave.Enabled = True
        btnPost.Enabled = False
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL  identity NOT FOR REPLICATION")
        coll.Add("Document_Code", "varchar(30) not null References TSPL_PROD_QC_CHECK_HEAD(Document_Code)")
        coll.Add("Item_Code", "Varchar(50) NOT NULL  References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_Code", "varchar(12) not NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE)")
        coll.Add("QC_Param_Code", "varchar(30) not null REFERENCES TSPL_QC_LOG_SHEET_MASTER(Code)")
        coll.Add("Param_L_Range", "float null")
        coll.Add("Param_U_Range", "float null")
        coll.Add("Param_Status", "varchar(3) null")
        coll.Add("Param_Value", "varchar(max) null")
        coll.Add("Param_QC_Status", "varchar(10) null")
        coll.Add("Measured", "varchar(300) null")
        coll.Add("Remarks", "varchar(200) null")
        coll.Add("InputData", "float null")
        coll.Add("Description", "varchar(500) null")
        coll.Add("Description_Status", "varchar(15) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_QC_CHECK_PARA_DETAIL", coll, Nothing, False, False, "", "", "")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub addnew()
        isNewEntry = True
        txtlocation.Value = ""
        TxtFgcode.Value = ""
        docDate.Value = clsCommon.GETSERVERDATE()
        lblLocation.Text = ""
        lblfgcode.Text = ""
        txtDocNo.Value = ""
        gv1.Rows.Clear()
        ' gv1.Rows.AddNew()
        gv1.MasterTemplate.FilterDescriptors.Clear()
        brnSave.Enabled = True
        btnPost.Enabled = False
        brnSave.Text = "Save"
        txtprodCode.arrValueMember = Nothing
        gv1.MasterTemplate.FilterDescriptors.Clear()
        txtComment.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub txtlocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtlocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtlocation.Value = clsCommon.ShowSelectForm("outgoingqc", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtlocation.Value + "'"))
    End Sub

    Private Sub TxtFgcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFgcode._MYValidating
        Dim qry As String = "select item_code as Code , item_desc  as [Item Name] from tspl_item_master"
        Dim WhrCls As String = "structure_code='FG'"
        TxtFgcode.Value = clsCommon.ShowSelectForm("fgcode", qry, "Code", WhrCls, TxtFgcode.Value, "Code", isButtonClicked)
        lblfgcode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master  where Item_Code='" + TxtFgcode.Value + "'"))
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        addnew()
    End Sub
    Public Sub LoadGridData()
        Dim dt As New DataTable
        Dim whrD As String = ""
        Try
            gv1.Rows.Clear()
            Dim strQry As String = ""

            '        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            '            clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            '            Exit Sub
            '        End If
            '        If clsCommon.myLen(TxtFgcode.Value) <= 0 Then
            '            clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
            '            Exit Sub
            '        End If
            If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
                whrD = " Where TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ")) "
            Else
                whrD = " Where TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ")) "
            End If
            strQry = " select  Row_Number() Over (Order By (SELECT 1) Asc) as [S No],TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code ,TSPL_QC_LOG_SHEET_MASTER.Description AS qc_Param_name,TSPL_PARAMETER_RANGE_MASTER_QC.upper_range ,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_range,TSPL_PARAMETER_RANGE_MASTER_QC.Description 
                        from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER 
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.ITEM_CODE
                        left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code
                        left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.QC_Param_Code=TSPL_QC_LOG_SHEET_MASTER.Code
                        where  TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code in (select distinct item_code from TSPL_SPP_PRODUCTION_ENTRY_DETAIL  " + whrD + " "
            '        strQry = "Select  Row_Number() Over (Order By (Select 1) Asc) As [S No], TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,BOM_CODE,Batch_Code_Manual,FINAL_PRODUCTION_QTY* TSPL_ITEM_UOM_DETAIL.Conversion_Factor / UnitBag.Conversion_Factor As BagUnit ,COMMENTS,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_SPP_PRODUCTION_ENTRY.DESCRIPTION,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code from TSPL_SPP_PRODUCTION_ENTRY
            '                Left outer join TSPL_SPP_PRODUCTION_ENTRY_DETAIL on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
            '                Left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE
            '                Left outer join TSPL_ITEM_UOM_DETAIL  UnitBag on UnitBag.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE And UnitBag.UOM_Code='Bag'  where POSTED=1 and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE ='" + TxtFgcode.Value + "'  AND TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + txtlocation.Value + "' "
            dt = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isInsideLoadData = True
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(dr("S No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQCCode).Value = clsCommon.myCstr(dr("QC_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQCName).Value = clsCommon.myCstr(dr("qc_Param_name"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLrange).Value = clsCommon.myCstr(dr("Lower_range"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUrange).Value = clsCommon.myCstr(dr("upper_range"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(coldescription).Value = clsCommon.myCstr(dr("Description"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
        End Try
    End Sub
    Sub LoadBlankGrid()


        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()

        'repoChk = New GridViewCheckBoxColumn()
        'repoChk.FormatString = ""
        'repoChk.HeaderText = "Select"
        'repoChk.Name = colSelect
        'repoChk.Width = 60
        'gv1.MasterTemplate.Columns.Add(repoChk)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colSno
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colitemcode
        repoStr.Width = 130
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Name"
        repoStr.Name = colitemname
        repoStr.Width = 130
        repoStr.IsVisible = False
        'repoStr.CustomFormat = True
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "UOM"
        repoStr.Name = colUnitCode
        repoStr.Width = 130
        repoStr.IsVisible = False
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "QC Para Code"
        repoStr.Name = colQCCode
        repoStr.Width = 100
        repoStr.IsVisible = False
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "QC Para Name"
        repoStr.Name = colQCName
        repoStr.Width = 180
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = " Lower Range"
        repoStr.Name = colLrange
        repoStr.Width = 120
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = " Upper Range"
        repoStr.Name = colUrange
        repoStr.Width = 120
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Description"
        repoStr.Name = coldescription
        repoStr.Width = 150
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Input Value"
        repoStr.Name = colInputValue
        repoStr.Width = 120
        repoStr.ReadOnly = False
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Compiled/Uncompiled"
        repoStr.Name = colCompiledUn
        repoStr.Width = 150
        repoStr.ReadOnly = False
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Remarks"
        repoStr.Name = colremarks
        repoStr.Width = 150
        repoStr.ReadOnly = False
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)



        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        repoStr = Nothing
        repoInt = Nothing
        repoChk = Nothing
    End Sub
    'Private Sub btngo_Click_1(sender As Object, e As EventArgs)
    '    LoadGridData()
    'End Sub
    Private Sub brnSave_Click(sender As Object, e As EventArgs) Handles brnSave.Click
        SaveData(False)
    End Sub
    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New ClsOutgoingQcEntry()
        Dim objpd As New ClsTSPL_PROD_QC_CHECK_DETAIL
        Try
            If AllowToSave() Then
                obj.document_code = clsCommon.myCstr(txtDocNo.Value)
                obj.document_date = clsCommon.myCDate(docDate.Text)
                obj.bill_to_location = clsCommon.myCstr(txtlocation.Value)
                obj.comments = clsCommon.myCstr(txtComment.Text)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                obj.Item_code = clsCommon.myCstr(TxtFgcode.Value)
                If rbtnApp.IsChecked Then
                    obj.Template_Status = "A"
                ElseIf rbtnRej.IsChecked Then
                    obj.Template_Status = "R"
                ElseIf rbtnUD.IsChecked Then
                    obj.Template_Status = "U"
                End If
                obj.Arr_Pd = New List(Of ClsTSPL_PROD_QC_CHECK_DETAIL)
                For Each grow As GridViewRowInfo In gv1.Rows
                    objpd = New ClsTSPL_PROD_QC_CHECK_DETAIL()
                    objpd.item_code = (clsCommon.myCstr(grow.Cells(colitemcode).Value))
                    objpd.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objpd.QC_Param_Code = clsCommon.myCstr(grow.Cells(colQCCode).Value)
                    objpd.Param_L_Range = clsCommon.myCDecimal(grow.Cells(colLrange).Value)
                    objpd.Param_U_Range = clsCommon.myCDecimal(grow.Cells(colUrange).Value)
                    objpd.Description = clsCommon.myCstr(grow.Cells(coldescription).Value)
                    objpd.InputData = clsCommon.myCdbl(grow.Cells(colInputValue).Value)
                    objpd.Remarks = clsCommon.myCstr(grow.Cells(colremarks).Value)
                    objpd.Description_Status = clsCommon.myCstr(grow.Cells(colCompiledUn).Value)
                    If clsCommon.myLen(objpd.item_code) > 0 Then
                        obj.Arr_Pd.Add(objpd)
                    End If
                Next
                'Dim arrUserType As New List(Of String)
                'If txtprodCode.arrValueMember IsNot Nothing Then
                '    For i As Integer = 0 To txtprodCode.arrValueMember.Count - 1
                '        arrUserType.Add(txtprodCode.arrValueMember(i))
                '    Next
                'Else
                '    clsCommon.MyMessageBoxShow(Me, "Please select atleast one Production Code", Me.Text)
                '    Exit Sub
                'End If
                'obj.Arr_Prod = New List(Of clsProductionEntry)
                'For i As Integer = 0 To arrUserType.Count - 1
                '    Dim objtr As New clsProductionEntry
                '    objtr.ProdcutionCode = arrUserType(i)
                '    obj.Arr_Prod.Add(objtr)
                'Next
                Dim arrUserType As New List(Of String)

                If txtprodCode.arrValueMember IsNot Nothing Then
                    For i As Integer = 0 To txtprodCode.arrValueMember.Count - 1
                        arrUserType.Add(txtprodCode.arrValueMember(i))
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select at least one Production Code", Me.Text)
                    Exit Sub
                End If

                obj.Arr_Prod = New List(Of clsProductionEntry)
                For i As Integer = 0 To arrUserType.Count - 1
                    Dim objtr As New clsProductionEntry
                    objtr.ProdcutionCode = arrUserType(i)
                    obj.Arr_Prod.Add(objtr)
                Next

                If ClsOutgoingQcEntry.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If
                    LoadData(obj.document_code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            objpd = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New ClsOutgoingQcEntry()
        Try
            addnew()
            obj = ClsOutgoingQcEntry.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.document_code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtDocNo.Value = obj.document_code
                docDate.Value = obj.document_date
                txtlocation.Value = obj.bill_to_location
                TxtFgcode.Value = obj.Item_code
                txtComment.Text = obj.comments
                txtRemarks.Text = obj.Remarks
                lblLocation.Text = obj.Locationdesd
                lblfgcode.Text = obj.Item_desc
                UsLock1.Status = ERPTransactionStatus.Pending
                rbtnApp.IsChecked = IIf(obj.Template_Status = "A", True, False)
                rbtnRej.IsChecked = IIf(obj.Template_Status = "R", True, False)
                rbtnUD.IsChecked = IIf(obj.Template_Status = "U", True, False)
                brnSave.Text = "Update"
                btnPost.Enabled = True
                If obj.Status = 1 Then
                    brnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                If obj.Arr_Pd IsNot Nothing AndAlso obj.Arr_Pd.Count > 0 Then
                    For Each objtr As ClsTSPL_PROD_QC_CHECK_DETAIL In obj.Arr_Pd
                        gv1.Rows.AddNew()
                        'Dim linno As Integer
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = linno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCName).Value = objtr.QCparamNAme
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLrange).Value = objtr.Param_L_Range
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUrange).Value = objtr.Param_U_Range
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coldescription).Value = objtr.Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInputValue).Value = objtr.InputData
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCompiledUn).Value = objtr.Description_Status
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colremarks).Value = objtr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = objtr.Line_No
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colcomments).Value = objtr.comments
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colDesc).Value = objtr.Description
                        'linno += 1
                    Next
                End If
                Dim dtProductionCode As DataTable = clsDBFuncationality.GetDataTable("select PROD_ENTRY_CODE from TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY  where Document_Code='" + obj.document_code + "'")

                Dim Arr_Prod As New ArrayList
                For Each dr As DataRow In dtProductionCode.Rows
                    Arr_Prod.Add(dr("PROD_ENTRY_CODE"))
                Next
                'If obj.Arr_Prod IsNot Nothing AndAlso obj.Arr_Prod.Count > 0 Then
                '    For Each objPRD As clsProductionEntry In obj.Arr_Prod
                '        For i As Integer = 0 To txtprodCode.arrValueMember.Count - 1
                '            ' Assuming Arr_Prod is a List(Of String)
                '            Arr_Prod.Add(txtprodCode.arrValueMember(i))
                '        Next
                '    Next
                txtprodCode.arrValueMember = Arr_Prod

            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " select Document_Code as Code,Document_Date,Case when status=0 then 'Pending' else 'Approved' end as 'Status' from TSPL_PROD_QC_CHECK_HEAD "
        LoadData(clsCommon.ShowSelectForm("OutgoingQC", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_PROD_QC_CHECK_HEAD where Document_Code ='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                If (ClsOutgoingQcEntry.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    brnSave.Enabled = False
                    btnPost.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean 'ByVal isPost As Boolean
        Try
            If AllowFutureDateTransaction(docDate.Value, Nothing) = False Then
                docDate.Focus()
                Return False
            End If
            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                txtlocation.Focus()
                txtlocation.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Location", Me.Text)
                Return False
            End If

            If clsCommon.myLen(TxtFgcode.Value) <= 0 Then
                TxtFgcode.Focus()
                TxtFgcode.Select()
                clsCommon.MyMessageBoxShow(Me, "Select  Item", Me.Text)
                Return False
            End If
            If rbtnApp.IsChecked = False AndAlso rbtnRej.IsChecked = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select Accepted Rejected first", Me.Text)
                Return False
            End If
            Dim arrpd As New List(Of String)
            Dim arr As New List(Of String)

            Dim icode As String = ""
            Dim status As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colitemcode).Value)
                If clsCommon.myLen(icode) > 0 Then
                    status += 1
                End If
                arrpd.Add(icode)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    'Private Sub btntemplate_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim arrIcode As New List(Of String)
    '        arrIcode = New List(Of String)

    '        For Each grow As GridViewRowInfo In gv1.Rows
    '            Dim Productcode As String = clsCommon.myCstr(grow.Cells(colProductCode).Value)
    '            If clsCommon.myLen(Productcode) > 0 Then
    '                If Not arrIcode.Contains(Productcode) Then
    '                    arrIcode.Add(Productcode)
    '                End If
    '            End If
    '        Next
    '        If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
    '            Throw New Exception("No record found.")
    '        End If
    '        'Dim frm As New frmQCTemplateEntry()
    '        ' clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, CustRoute)

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub txtprodCode__My_Click(sender As Object, e As EventArgs) Handles txtprodCode._My_Click
        Dim qry As String = "select tspl_spp_production_entry_detail.PROD_ENTRY_CODE as Code ,PROD_DATE as Date from tspl_spp_production_entry
                                left outer join tspl_spp_production_entry_detail on tspl_spp_production_entry_detail.prod_entry_code=tspl_spp_production_entry.prod_entry_code
                                --left outer join TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY on TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE<>tspl_spp_production_entry.prod_entry_code
                             where  POSTED=1 and item_code = '" + TxtFgcode.Value + "' and tspl_spp_production_entry_detail.LOCATION_CODE='" + txtlocation.Value + "' "
        txtprodCode.arrValueMember = clsCommon.ShowMultipleSelectForm("PMMulSel", qry, "Code", "Date", txtprodCode.arrValueMember, txtprodCode.arrDispalyMember)
        LoadGridData()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim whr As String
            If clsCommon.myLen(txtDocNo) <= 0 Then
                Throw New Exception("Document number not found")
            End If
            If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
                whr = " where TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + "))"
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " select  tSPL_PROD_QC_CHECK_HEAD.document_code,tSPL_PROD_QC_CHECK_HEAD.document_date,
                            tspl_location_master.location_desc,tspl_location_master.add1,tspl_location_master.division_code,division_Name,division_address,TSPL_SPP_PRODUCTION_ENTRY1.prod_date_from, prod_date_to,TSPL_QC_LOG_SHEET_MASTER.description as QCPARAMNAME,TSPL_QC_LOG_SHEET_MASTER.clause_ref,TSPL_QC_LOG_SHEET_MASTER.is_ref,TSPL_QC_CHECK_PARA_DETAIL.inputdata,param_L_range,Param_U_range,TSPL_QC_CHECK_PARA_DETAIL.remarks,TSPL_PARAMETER_RANGE_MASTER_QC.description,TSPL_ITEM_MASTER.item_desc ,TSPL_SPP_PRODUCTION_ENTRY1.batch_code from tSPL_PROD_QC_CHECK_HEAD --header
                            left outer join TSPL_QC_CHECK_PARA_DETAIL  on TSPL_QC_CHECK_PARA_DETAIL.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code --save prooduction code
                            left outer join tspl_location_master on tspl_location_master.location_code=tSPL_PROD_QC_CHECK_HEAD.location_code
                            left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code=tSPL_PROD_QC_CHECK_HEAD.item_code and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code=TSPL_QC_CHECK_PARA_DETAIL.qc_param_code 
                            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.ITEM_CODE
                            left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code
                            left outer join TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY on TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code
                            left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_LOG_SHEET_MASTER.code
                            left outer join (select min(prod_date) as prod_date_from,max(prod_date) as prod_date_to,max(prod_entry_code) as prod_entry_code,max(batch_code) as batch_code from TSPL_SPP_PRODUCTION_ENTRY " + whr + " TSPL_SPP_PRODUCTION_ENTRY1 on TSPL_SPP_PRODUCTION_ENTRY1.prod_entry_code=TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE 
            where tSPL_PROD_QC_CHECK_HEAD.document_code='" + txtDocNo.Value + "' and TSPL_SPP_PRODUCTION_ENTRY1.PROD_ENTRY_CODE is not null"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "rptOutgoingQCCheckEntry", "Outgoing Quality Control Report")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class