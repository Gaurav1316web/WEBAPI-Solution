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
    Public colNature As String = "Nature"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnMRN)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        brnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub
    Private Sub frmOutgoingQCEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtlocation.Value = ""
        TxtFgcode.Value = ""
        docDate.Value = clsCommon.GETSERVERDATE()
        QcStartdata.Value = clsCommon.GETSERVERDATE()
        QCEnddate.Value = clsCommon.GETSERVERDATE()
        LoadBlankGrid()
        SetUserMgmtNew()
        Addnew()
        brnSave.Enabled = True
        btnPost.Enabled = False
        rbtnApp.IsChecked = False
        rbtnRej.IsChecked = False

    End Sub
    Private Sub frmOutgoingQCEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub Addnew()
        btnReverse.Visible = False
        isNewEntry = True
        txtlocation.Value = ""
        TxtFgcode.Value = ""
        docDate.Value = clsCommon.GETSERVERDATE()
        lblLocation.Text = ""
        lblfgcode.Text = ""
        txtDocNo.Value = ""
        QcStartdata.Value = clsCommon.GETSERVERDATE()
        QCEnddate.Value = clsCommon.GETSERVERDATE()
        gv1.Rows.Clear()
        ' gv1.Rows.AddNew()
        gv1.MasterTemplate.FilterDescriptors.Clear()
        brnSave.Enabled = True
        btnPost.Enabled = False
        btndelete.Enabled = False
        brnSave.Text = "Save"
        txtAccept.Text = "Pending"
        txtAccept.BackColor = Color.Yellow
        rbtnApp.IsChecked = False
        rbtnRej.IsChecked = False
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
        Addnew()
    End Sub
    Public Sub LoadGridData()
        Dim dt As New DataTable
        Dim whrD As String = ""
        isInsideLoadData = True
        Try
            gv1.Rows.Clear()
            Dim strQry As String = ""

            If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
                whrD = " Where TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ") "
            Else
                whrD = " Where TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ") "
            End If
            strQry = " select  Row_Number() Over (Order By (SELECT 1) Asc) as [S No],TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code ,TSPL_QC_LOG_SHEET_MASTER.Description AS qc_Param_name,TSPL_PARAMETER_RANGE_MASTER_QC.upper_range ,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_range,TSPL_PARAMETER_RANGE_MASTER_QC.Description ,TSPL_QC_LOG_SHEET_MASTER.Nature
                        from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER 
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.ITEM_CODE
                        left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code
                        left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.QC_Param_Code=TSPL_QC_LOG_SHEET_MASTER.Code
                        where  TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code in (select distinct item_code from TSPL_SPP_PRODUCTION_ENTRY_DETAIL  " + whrD + ") "
            '        strQry = "Select  Row_Number() Over (Order By (Select 1) Asc) As [S No], TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,BOM_CODE,Batch_Code_Manual,FINAL_PRODUCTION_QTY* TSPL_ITEM_UOM_DETAIL.Conversion_Factor / UnitBag.Conversion_Factor As BagUnit ,COMMENTS,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_SPP_PRODUCTION_ENTRY.DESCRIPTION,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code from TSPL_SPP_PRODUCTION_ENTRY
            '                Left outer join TSPL_SPP_PRODUCTION_ENTRY_DETAIL on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
            '                Left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE
            '                Left outer join TSPL_ITEM_UOM_DETAIL  UnitBag on UnitBag.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE And UnitBag.UOM_Code='Bag'  where POSTED=1 and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE ='" + TxtFgcode.Value + "'  AND TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + txtlocation.Value + "' "
            dt = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(dr("S No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQCCode).Value = clsCommon.myCstr(dr("QC_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQCName).Value = clsCommon.myCstr(dr("qc_Param_name"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLrange).Value = clsCommon.myCstr(dr("Lower_range"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUrange).Value = clsCommon.myCstr(dr("upper_range"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(coldescription).Value = clsCommon.myCstr(dr("Description"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNature).Value = clsCommon.myCstr(dr("Nature"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No QC Parameter Mapped ", Me.Text)
            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
        End Try
    End Sub
    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        Dim RowTypeItem As String = "Compiled"
        Dim RowTypeItem1 As String = "Uncompiled"
        Dim RowTypeItem2 As String = "OK"
        Dim RowTypeItem3 As String = "NotOK"
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeItem
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeItem1
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeItem2
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeItem3
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub LoadBlankGrid()


        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()


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
        repoStr.HeaderText = "Nature Type"
        repoStr.Name = colNature
        repoStr.Width = 150
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoStr)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Compiled/Uncompiled"
        repoRowType.Name = colCompiledUn
        repoRowType.Width = 150
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)

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
        'isInsideLoadData = True
        Try
            If AllowToSave() Then
                obj.document_code = clsCommon.myCstr(txtDocNo.Value)
                obj.document_date = clsCommon.myCDate(docDate.Text)
                obj.bill_to_location = clsCommon.myCstr(txtlocation.Value)
                obj.comments = clsCommon.myCstr(txtComment.Text)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                obj.Item_code = clsCommon.myCstr(TxtFgcode.Value)
                obj.QC_Start_date = clsCommon.myCDate(QcStartdata.Text)
                obj.QC_end_date = clsCommon.myCDate(QCEnddate.Text)
                'obj.QC_Status = clsCommon.myCstr(txtAccept.Text)
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
                    objpd.QC_Param_Code = clsCommon.myCstr(grow.Cells(colQCCode).Value)
                    objpd.Param_L_Range = clsCommon.myCDecimal(grow.Cells(colLrange).Value)
                    objpd.Param_U_Range = clsCommon.myCDecimal(grow.Cells(colUrange).Value)
                    objpd.Description = clsCommon.myCstr(grow.Cells(coldescription).Value)
                    If clsCommon.myLen((grow.Cells(colInputValue).Value)) > 0 Then
                        objpd.InputData = clsCommon.myCDecimal(grow.Cells(colInputValue).Value)
                    End If
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
            Addnew()
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
                QcStartdata.Value = obj.QC_Start_date
                QCEnddate.Value = obj.QC_end_date
                lblLocation.Text = obj.Locationdesd
                lblfgcode.Text = obj.Item_desc
                'txtAccept.Text = obj.qc
                UsLock1.Status = ERPTransactionStatus.Pending
                rbtnApp.IsChecked = IIf(obj.Template_Status = "A", True, False)
                rbtnRej.IsChecked = IIf(obj.Template_Status = "R", True, False)
                rbtnUD.IsChecked = IIf(obj.Template_Status = "U", True, False)
                brnSave.Text = "Update"
                btndelete.Enabled = True
                btnPost.Enabled = True
                If obj.Status = 1 Then
                    brnSave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                If clsCommon.CompairString(obj.Template_Status, "A") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Green
                    txtAccept.Text = "Accepted"
                ElseIf clsCommon.CompairString(obj.Template_Status, "U") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Yellow
                ElseIf clsCommon.CompairString(obj.Template_Status, "R") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Red
                    txtAccept.Text = "Rejected"
                End If
                If obj.Arr_Pd IsNot Nothing AndAlso obj.Arr_Pd.Count > 0 Then
                    For Each objtr As ClsTSPL_PROD_QC_CHECK_DETAIL In obj.Arr_Pd
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCName).Value = objtr.QCparamNAme
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLrange).Value = objtr.Param_L_Range
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUrange).Value = objtr.Param_U_Range
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coldescription).Value = objtr.Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInputValue).Value = objtr.InputData
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCompiledUn).Value = objtr.Description_Status
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colremarks).Value = objtr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = objtr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCCode).Value = objtr.QC_Param_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = objtr.item_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNature).Value = objtr.Nature
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
            Dim obj As New ClsOutgoingQcEntry()
            If AllowFutureDateTransaction(docDate.Value, Nothing) = False Then
                docDate.Focus()
                Return False
            End If
            ' If QCEnddate.Value  QcStartdata.Value Then
            If obj.QC_end_date > clsCommon.GETSERVERDATE Then
                QCEnddate.Focus()
                Throw New Exception("QC End date should be less than Server Date")
            Else
                obj.QC_end_date = clsCommon.myCDate(QCEnddate.Value, "dd/MM/yyyy")
            End If
            If obj.QC_Start_date > clsCommon.GETSERVERDATE Then
                QcStartdata.Focus()
                Throw New Exception("QC End date should be less than Server Date")
            Else
                obj.QC_Start_date = clsCommon.myCDate(QcStartdata.Value, "dd/MM/yyyy")
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
    Private Sub txtprodCode__My_Click(sender As Object, e As EventArgs) Handles txtprodCode._My_Click
        Dim qry As String = "select tspl_spp_production_entry_detail.PROD_ENTRY_CODE as Code ,PROD_DATE as Date,tspl_spp_production_entry.Shift_Code  from tspl_spp_production_entry
                                left outer join tspl_spp_production_entry_detail on tspl_spp_production_entry_detail.prod_entry_code=tspl_spp_production_entry.prod_entry_code
								left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_spp_production_entry_detail.LOCATION_CODE
                             where  POSTED=1 and item_code = '" + TxtFgcode.Value + "' and tspl_spp_production_entry_detail.LOCATION_CODE='" + txtlocation.Value + "' 
							and tspl_spp_production_entry.prod_entry_code not in ( select   TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE from  TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY) and tspl_spp_production_entry.PROD_DATE>= TSPL_LOCATION_MASTER.QCStartDate "
        txtprodCode.arrValueMember = clsCommon.ShowMultipleSelectForm("PDMulSel", qry, "Code", "Date", txtprodCode.arrValueMember, txtprodCode.arrDispalyMember)
        If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
            For jj As Integer = 0 To txtprodCode.arrDispalyMember.Count - 1
                If clsCommon.CompairString(clsCommon.GetPrintDate(txtprodCode.arrDispalyMember(0)), clsCommon.GetPrintDate(txtprodCode.arrDispalyMember(jj))) <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, " Production should be of same date ")
                    Exit Sub
                End If
            Next
            LoadGridData()
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim whr As String
            Dim dt As DataTable = Nothing
            Dim strBatch As String = Nothing
            If clsCommon.myLen(txtDocNo) <= 0 Then


                Throw New Exception("Document number not found")
            End If

            If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
                whr = " where TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ")"
            End If
            Dim batch As String = " select  batch_code_manual from TSPL_SPP_PRODUCTION_ENTRY " + whr
            dt = clsDBFuncationality.GetDataTable(batch)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each btch In dt.Rows
                    If clsCommon.myLen(strBatch) > 0 Then
                        strBatch += "," + (clsCommon.myCstr(btch("batch_code_manual")))
                    Else
                        strBatch = (clsCommon.myCstr(btch("batch_code_manual")))
                    End If
                Next
            End If

            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = "  select  '" + strBatch + "' as [Batch_Code],tSPL_PROD_QC_CHECK_HEAD.document_code,tSPL_PROD_QC_CHECK_HEAD.document_date,
                            tspl_location_master.location_desc,tspl_location_master.add1,tspl_location_master.division_code,division_Name,division_address,TSPL_SPP_PRODUCTION_ENTRY1.prod_date_from, prod_date_to,TSPL_QC_LOG_SHEET_MASTER.description as QCPARAMNAME,TSPL_QC_LOG_SHEET_MASTER.clause_ref,TSPL_QC_LOG_SHEET_MASTER.is_ref,TSPL_QC_CHECK_PARA_DETAIL.inputdata,param_L_range,Param_U_range,TSPL_QC_CHECK_PARA_DETAIL.remarks,TSPL_PARAMETER_RANGE_MASTER_QC.description,TSPL_ITEM_MASTER.item_desc ,TSPL_QC_CHECK_PARA_DETAIL.Description_Status,TSPL_QC_LOG_SHEET_MASTER.AliasName,tSPL_PROD_QC_CHECK_HEAD.qc_start_date,tSPL_PROD_QC_CHECK_HEAD.qc_end_date,tspl_location_master.CMA_CML,tspl_location_master.QC_IS,tspl_location_master.ValidUpto,tspl_location_master.GradeType,TSPL_QC_LOG_SHEET_MASTER.Nature from tSPL_PROD_QC_CHECK_HEAD --header
                            left outer join TSPL_QC_CHECK_PARA_DETAIL  on TSPL_QC_CHECK_PARA_DETAIL.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code --save prooduction code
                            left outer join tspl_location_master on tspl_location_master.location_code=tSPL_PROD_QC_CHECK_HEAD.location_code
                            left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code=tSPL_PROD_QC_CHECK_HEAD.item_code and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code=TSPL_QC_CHECK_PARA_DETAIL.qc_param_code 
                            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.ITEM_CODE
                            left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code
                            left outer join TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY on TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code
                            left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_LOG_SHEET_MASTER.code
                            left outer join (select min(prod_date) as prod_date_from,max(prod_date) as prod_date_to,max(prod_entry_code) as prod_entry_code from TSPL_SPP_PRODUCTION_ENTRY " + whr + " ) TSPL_SPP_PRODUCTION_ENTRY1 on TSPL_SPP_PRODUCTION_ENTRY1.prod_entry_code=TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE 
            where tSPL_PROD_QC_CHECK_HEAD.document_code='" + txtDocNo.Value + "' and TSPL_SPP_PRODUCTION_ENTRY1.PROD_ENTRY_CODE is not null order by TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO"

            dt = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "rptOutgoingQCCheckEntry", "Outgoing Quality Control Report")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (ClsOutgoingQcEntry.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Addnew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    ClsOutgoingQcEntry.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CalculateFinalStatus(ByVal grow As GridViewRowInfo)
        Try
            Dim strTempResult As String = ""
            For Each grow1 As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(colCompiledUn).Value), "NotOK") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(colCompiledUn).Value), "Uncompiled") = CompairStringResult.Equal Then
                    strTempResult = "Rejected"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(colCompiledUn).Value), "Compiled") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(colCompiledUn).Value), "OK") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strTempResult, "Rejected") <> CompairStringResult.Equal Then
                        strTempResult = "Accepted"
                    End If
                End If
            Next
            ' If strTempResult > 0 Then
            If clsCommon.myLen(strTempResult) > 0 Then
                If clsCommon.CompairString(strTempResult, "Accepted") = CompairStringResult.Equal Then
                    rbtnApp.IsChecked = True
                    rbtnUD.IsChecked = False
                    rbtnRej.IsChecked = False
                    txtAccept.Text = "Accepted"
                    txtAccept.BackColor = Color.Green
                Else
                    rbtnRej.IsChecked = True
                    rbtnApp.IsChecked = False
                    rbtnUD.IsChecked = False
                    txtAccept.Text = "Rejected"
                    txtAccept.BackColor = Color.Red
                End If
                txtAccept.Visible = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        ' CalculateFinalStatus(gv1.CurrentRow)
        If Not isInsideLoadData Then
            UpdateStausNewInput(gv1.CurrentRow)
        End If
    End Sub

    Private Sub UpdateStausNewInput(ByVal grow As GridViewRowInfo)
        Try
            If clsCommon.myCDecimal(grow.Cells(colInputValue).Value) >= 0 Then

                Dim qry As String = "select (TSPL_PARAMETER_RANGE_MASTER_QC.lower_range) as lower_range,(TSPL_PARAMETER_RANGE_MASTER_QC.upper_range) as upper_range,(TSPL_PARAMETER_RANGE_MASTER_QC.status) as status,(TSPL_PARAMETER_RANGE_MASTER_QC.value1) as value1,(TSPL_PARAMETER_RANGE_MASTER_QC.qc_status) as qc_status,(TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Per) as Deduction_Per
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio3
                 ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Method
                ,TSPL_QC_LOG_SHEET_MASTER.Nature
                 from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code and TSPL_PARAMETER_RANGE_MASTER_QC.trans_id='standard' left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code 
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code = '" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "'  
                and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code='" + clsCommon.myCstr(grow.Cells(colQCCode).Value) + "'"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim natureValue As String = clsCommon.myCstr(dt.Rows(0)("Nature"))
                If natureValue = "R" Then
                    Dim repoStr As New GridViewTextBoxColumn()
                    repoStr.Name = colCompiledUn
                    repoStr.ReadOnly = True
                    Dim TempInputData As Decimal = 0
                    Dim TempDedPercentage As Decimal = 0
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        TempInputData = clsCommon.myCDecimal(grow.Cells(colInputValue).Value)
                        If TempInputData >= clsCommon.myCDecimal(dt.Rows(0)("lower_range")) AndAlso TempInputData <= clsCommon.myCDecimal(dt.Rows(0)("upper_range")) Then
                            rbtnApp.IsChecked = True
                            rbtnUD.IsChecked = False
                            rbtnRej.IsChecked = False
                            txtAccept.Text = "Accepted"
                            txtAccept.BackColor = Color.Green
                        Else
                            rbtnRej.IsChecked = True
                            rbtnApp.IsChecked = False
                            rbtnUD.IsChecked = False
                            txtAccept.Text = "Rejected"
                            txtAccept.BackColor = Color.Red
                        End If
                    End If
                Else
                    If natureValue = "A" Then
                        Dim repoStr As New GridViewTextBoxColumn()
                        repoStr.Name = colLrange
                        repoStr.ReadOnly = True
                        repoStr.Name = colUrange
                        repoStr.ReadOnly = True
                        CalculateFinalStatus(gv1.CurrentRow)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting

        If e.Column IsNot Nothing Then
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colNature).Value, "R") = CompairStringResult.Equal Then
                gv1.CurrentRow.Cells(colCompiledUn).ReadOnly = True
            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colNature).Value, "A") = CompairStringResult.Equal Then
                gv1.CurrentRow.Cells(colInputValue).ReadOnly = True
            End If
        End If
    End Sub
End Class
