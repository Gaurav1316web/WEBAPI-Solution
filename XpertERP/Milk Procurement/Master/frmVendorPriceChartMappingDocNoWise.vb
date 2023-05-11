'===============BM00000007844===============================
Imports common
Imports System.Data.SqlClient

Public Class frmVendorPriceChartMappingDocNoWise
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim AllowMultiplePricewithMultipleVendor As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim IsItemMilkType As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colVendorCode As String = "colVendorCode"
    Const colVendorDesc As String = "colVendorDesc"
    Const colIsDefault As String = "colIsDefault"
    Dim isNewEntry As Boolean = True
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = True
    Dim isInsideLoadData As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVendorPriceChartMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        fndcode.Value = ""
        txtDocDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndPriceCode.Value = ""
        isLoadData = False
        isValueChanged = True
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True
        LoadPriceData()
        LoadVendor(False, "", "")
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As clsVendorPriceChartMappingDocNoWise = clsVendorPriceChartMappingDocNoWise.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                fndcode.MyReadOnly = True
                isLoadData = True
                'LoadBlankGrid()
                fndcode.Value = obj.Document_No
                txtDocDate.Value = obj.Document_Date
                LoadPriceData()
                LoadVendor()
                btnsave.Text = "Update"
                If obj.Posted = 1 Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                    btndelete.Enabled = True
                End If

            End If
            isLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isLoadData = False
        End Try
    End Sub

    Private Sub FrmParameterRangeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterRangeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AllowMultiplePricewithMultipleVendor = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkPriceChartMultiplepriceToMultipleVendor, clsFixedParameterCode.AllowBulkPriceChartMultiplepriceToMultipleVendor, Nothing))
        IsPriceChartGradeWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'RadPageView1.SelectedPage = RadPageViewPage1

        If AllowMultiplePricewithMultipleVendor = 1 Then
            SplitContainer3.Panel1Collapsed = True
            RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
            RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
            RadPageView1.SelectedPage = RadPageViewPage1
        End If
    End Sub
    Sub LoadPriceData()
        gvPriceCode.DataSource = Nothing
        Dim strqry As String = String.Empty
        Dim strMilkType As String = Nothing
     
        strqry = "select distinct cast(1 as bit) as Sel,TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date], " & _
               "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE]  from " & _
               "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER left outer join TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL on " & _
               "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER.Document_No=TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Document_No " & _
               "left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.PriceCode " & _
               "left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code and " & _
               "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Milk_Grade_Code=TSPL_BULK_PRICE_DETAIL.Milk_Grade_code  left outer join " & _
               "TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE " & _
               "where TSPL_Bulk_Price_MASTER.Posted=1 and TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER.Document_No='" & fndcode.Value & "'" & _
                 " union all " & _
                "sELECT cast(0 as bit) as Sel,TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date], " & _
                "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE] FROM " & _
                "TSPL_Bulk_Price_MASTER  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                "left outer join TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE " & _
                "where TSPL_Bulk_Price_MASTER.Posted=1   and (TSPL_BULK_PRICE_DETAIL.Price_Code + TSPL_BULK_PRICE_DETAIL.Milk_Grade_code) not in " & _
                "(select (TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.PriceCode + Milk_Grade_code) from  TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL where Document_No='" & fndcode.Value & "') order by [Price Code]"

               
        gvPriceCode.DataSource = clsDBFuncationality.GetDataTable(strqry)

        gvPriceCode.Columns("Sel").HeaderText = " "
        gvPriceCode.Columns("Sel").Width = 50
        gvPriceCode.Columns("Sel").ReadOnly = False

        gvPriceCode.Columns("Price Code").HeaderText = "Price Code"
        gvPriceCode.Columns("Price Code").Width = 100
        gvPriceCode.Columns("Price Code").ReadOnly = True

        gvPriceCode.Columns("Price Date").HeaderText = "Price date"
        gvPriceCode.Columns("Price Date").Width = 200
        gvPriceCode.Columns("Price Date").ReadOnly = True

        gvPriceCode.Columns("Milk Grade code").HeaderText = "Grade Code"
        gvPriceCode.Columns("Milk Grade code").Width = 100
        gvPriceCode.Columns("Milk Grade code").ReadOnly = True
        gvPriceCode.Columns("Milk Grade code").IsVisible = False

        gvPriceCode.Columns("GRADE TYPE").HeaderText = "Grade Type"
        gvPriceCode.Columns("GRADE TYPE").Width = 100
        gvPriceCode.Columns("GRADE TYPE").ReadOnly = True
        gvPriceCode.Columns("GRADE TYPE").IsVisible = False


        gvPriceCode.Columns("Milk Grade code").IsVisible = True
        gvPriceCode.Columns("GRADE TYPE").IsVisible = True


        gvPriceCode.AllowAddNewRow = False
        gvPriceCode.ShowGroupPanel = False
        gvPriceCode.AllowColumnReorder = False
        gvPriceCode.AllowRowReorder = False
        gvPriceCode.EnableSorting = False
        gvPriceCode.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPriceCode.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Sub LoadVendor(Optional ByVal NewVal As Boolean = False, Optional ByVal PriceCode As String = Nothing, Optional ByVal MilkGradeCode As String = Nothing)
        GvVendor.DataSource = Nothing
        Dim qry As String = Nothing

        qry = "Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.SequenceNo, " & _
            "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.VendorCode As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description " & _
            "FROM TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL  left outer join TSPL_VENDOR_MASTER on " & _
            "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.VendorCode=TSPL_VENDOR_MASTER.vendor_code  where TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Document_No ='" & fndcode.Value & "'" & _
         " union all " & _
         "  SELECT cast(0 as bit) as Sel,0 as SequenceNo,TSPL_VENDOR_MASTER.vendor_code As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM TSPL_VENDOR_MASTER  where " & _
         "TSPL_VENDOR_MASTER.vendor_code not in (sELECT TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.VendorCode FROM  " & _
         "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL where TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Document_No ='" & fndcode.Value & "')) Final ORDER BY fINAL.[Vendor Code]  "


        GvVendor.DataSource = clsDBFuncationality.GetDataTable(qry)

        GvVendor.Columns("Sel").HeaderText = " "
        GvVendor.Columns("Sel").Width = 50
        GvVendor.Columns("Sel").ReadOnly = False

        GvVendor.Columns("SequenceNo").HeaderText = "Seq No"
        GvVendor.Columns("SequenceNo").Width = 100
        GvVendor.Columns("SequenceNo").ReadOnly = False
        GvVendor.Columns("SequenceNo").IsVisible = False
        GvVendor.Columns("SequenceNo").FormatString = "{0:n0}"


        GvVendor.Columns("Vendor Code").HeaderText = "Vendor Code"
        GvVendor.Columns("Vendor Code").Width = 100
        GvVendor.Columns("Vendor Code").ReadOnly = True

        GvVendor.Columns("Description").HeaderText = "Vendor Name"
        GvVendor.Columns("Description").Width = 200
        GvVendor.Columns("Description").ReadOnly = True

        GvVendor.AllowAddNewRow = False
        GvVendor.ShowGroupPanel = False
        GvVendor.AllowColumnReorder = False
        GvVendor.AllowRowReorder = False
        GvVendor.EnableSorting = False
        GvVendor.Enabled = True
        GvVendor.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvVendor.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Sub LoadBlankGrid()

        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colVendorCode
        repocode.Width = 150
        repocode.HeaderText = "Vendor Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)


        repocode = New GridViewTextBoxColumn()
        repocode.Name = colVendorDesc
        repocode.Width = 300
        repocode.HeaderText = "Vendor Desc"
        repocode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repocode)

        Dim selCol As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        selCol.Name = colIsDefault
        selCol.Width = 100
        selCol.HeaderText = " Default"
        gv.MasterTemplate.Columns.Add(selCol)


        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = True
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
        gv.EnableSorting = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Function AllowToSave() As Boolean
        Try
            If AllowMultiplePricewithMultipleVendor = 0 Then
                Dim VendorCode As String = ""
                Dim VendorCode1 As String = ""
                Dim VendorDesc As String = Nothing
                VendorCode = clsCommon.myCstr(gv.Rows(0).Cells(colVendorCode).Value)
                If clsCommon.myLen(fndPriceCode.Value) <= 0 Then
                    Throw New Exception("Please select price code")
                End If
                If clsCommon.myLen(VendorCode) <= 0 Then
                    Throw New Exception("Please fill atleast one vendor Detail")
                End If
                For ii As Integer = 0 To gv.Rows.Count - 1
                    VendorCode = clsCommon.myCstr(gv.Rows(ii).Cells(colVendorCode).Value)
                    If clsCommon.myLen(VendorCode) <= 0 Then
                        Throw New Exception("please fill Vendor  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    For jj As Integer = 0 To gv.Rows.Count - 1
                        VendorCode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colVendorCode).Value)
                        If ii <> jj AndAlso clsCommon.myLen(VendorCode) > 0 AndAlso clsCommon.CompairString(VendorCode, VendorCode1) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Vendor Code At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                        End If
                    Next
                Next
            Else
                Dim dblsequenceno As Double = 0
                Dim dblsequencenoInter As Double = 0

                Dim strcount As Integer = 0
                For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
                    If clsCommon.myCBool(gvPriceCode.Rows(ii).Cells("Sel").Value) Then
                        strcount = strcount + 1
                    End If
                Next
                If strcount > 1 Then
                    clsCommon.MyMessageBoxShow("Select only one location at a time.")
                    Return False
                End If


                For i As Integer = 0 To GvVendor.Rows.Count - 1
                    dblsequenceno = clsCommon.myCdbl(GvVendor.Rows(i).Cells("SequenceNo").Value)
                    If dblsequenceno <> 0 Then
                        For j As Integer = i + 1 To GvVendor.Rows.Count - 1
                            dblsequencenoInter = clsCommon.myCdbl(GvVendor.Rows(j).Cells("SequenceNo").Value)
                            If dblsequenceno = dblsequencenoInter Then
                                clsCommon.MyMessageBoxShow("Sequence no should not be same for two customers.")
                                Return False
                            End If
                        Next
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Function AllowToImport(ByVal gv1 As RadGridView) As Boolean
        Try
            Dim Vendorcode As String = ""
            Dim PriceCode As String = Nothing
            Dim Vendorcode1 As String = ""
            Dim PriceCode1 As String = Nothing
            Dim isDefault As Integer = 0
          
                Dim MilkGradeCode As String = Nothing
                PriceCode = clsCommon.myCstr(gv1.Rows(0).Cells("PriceCode").Value)
                If clsCommon.myLen(PriceCode) <= 0 Then
                    Throw New Exception("Please fill atleast one Mapping Details")
                End If

                For ii As Integer = 0 To gv1.Rows.Count - 1
                    PriceCode = clsCommon.myCstr(gv1.Rows(ii).Cells("PriceCode").Value)
                    If clsCommon.myLen(PriceCode) <= 0 Then
                        Throw New Exception("please fill Price Code At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    Vendorcode = clsCommon.myCstr(gv1.Rows(ii).Cells("VendorCode").Value)
                    If clsCommon.myLen(Vendorcode) <= 0 Then
                        Throw New Exception("please fill Vendor Code  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    Dim qry1 As String = " select count(*) from tspl_Vendor_Master where vendor_code='" & Vendorcode & "'"
                    Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1))
                    If cnt <= 0 Then
                        Throw New Exception("Vendor Code Not Found In master  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    qry1 = " select count(*) from TSPL_Bulk_Price_MASTER  where Price_Code='" & PriceCode & "' "
                    cnt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1))
                    If cnt <= 0 Then
                        Throw New Exception("Price Code Not Found In master  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If IsPriceChartGradeWise = 1 Then
                        MilkGradeCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Milk Grade Code").Value)
                        qry1 = " select count(*) from tspl_milk_grade_master  where milk_grade_code='" & MilkGradeCode & "' "
                        cnt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1))
                        If cnt <= 0 Then
                            Throw New Exception("Milk Grade Code Not Found In master  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        End If
                    End If

                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        Vendorcode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("VendorCode").Value)
                        PriceCode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("PriceCode").Value)
                        Dim MilkGradeCode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("Milk Grade Code").Value)
                        If ii <> jj AndAlso clsCommon.myLen(PriceCode) > 0 AndAlso clsCommon.myLen(Vendorcode) > 0 AndAlso clsCommon.CompairString(Vendorcode, Vendorcode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(PriceCode, PriceCode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(MilkGradeCode, MilkGradeCode1) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Values At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                        End If
                    Next

                Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Sub SaveData()
        Dim strpriceCode As String = ""
        Dim strMilkGradeCode As String = ""
        Dim qry As String = ""

     
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As New clsVendorPriceChartMappingDocNoWise
            obj.Document_No = fndcode.Value
            obj.Document_Date = txtDocDate.Value
          
            Dim arr As New List(Of clsVendorPriceChartMappingDocNoWise)

                For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
                    If clsCommon.myCBool(gvPriceCode.Rows(ii).Cells("Sel").Value) Then
                        strpriceCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
                        strMilkGradeCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade Code").Value)
                        Exit For
                    End If
                Next
            Dim intCount As Integer = 0
            obj.Arr = New List(Of clsVendorPriceChartMappingDocNoWiseDetail)
                For ii As Integer = 0 To GvVendor.Rows.Count - 1
                    If clsCommon.myCBool(GvVendor.Rows(ii).Cells("Sel").Value) Then
                    Dim objTr As clsVendorPriceChartMappingDocNoWiseDetail = New clsVendorPriceChartMappingDocNoWiseDetail()
                    objTr.VendorCode = clsCommon.myCstr(GvVendor.Rows(ii).Cells("Vendor Code").Value)
                    objTr.SequenceNo = clsCommon.myCdbl(GvVendor.Rows(ii).Cells("SequenceNo").Value)
                    objTr.Pricecode = strpriceCode
                    objTr.Milk_Grade_Code = strMilkGradeCode
                    If (clsCommon.myLen(objTr.Pricecode) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    End If
                Next
              
            If clsVendorPriceChartMappingDocNoWise.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then

                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If
                btnsave.Text = "Update"
                btndelete.Enabled = True
                LoadData(obj.Document_No, NavigatorType.Current)
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try

            If (deleteConfirm()) Then
                If (clsVendorPriceChartMappingDocNoWise.DeleteData(fndcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

   

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoadData Then '------when on loaddata then it should not run
            isValueChanged = True
            If isValueChanged Then
                isValueChanged = False
                If gv.CurrentColumn Is gv.Columns(colVendorCode) Then
                    gv.CurrentRow.Cells(colVendorCode).Value = clsVendorMaster.getFinder("Coalesce(form_Type,'')='All'", gv.CurrentRow.Cells(colVendorCode).Value, False)
                    gv.CurrentRow.Cells(colVendorDesc).Value = clsVendorMaster.GetName(gv.CurrentRow.Cells(colVendorCode).Value, Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub
    Private Sub fndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceCode._MYValidating
        fndPriceCode.Value = clsPriceChartBulkProc.getFinder("", fndPriceCode.Value, isButtonClicked)
        Dim qry As String = " select count(*) from tspl_vendor_price_chart_mapping where priceCode='" & fndPriceCode.Value & "'"
        Dim cnt As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If cnt > 0 Then
            ' LoadData()
        Else
            LoadBlankGrid()
        End If
    End Sub

    Private Sub gvPriceCode_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvPriceCode.CellValueChanged
        Try
            'If Not isInsideLoadData Then
            '    If e.Column Is gvPriceCode.Columns("Sel") Then
            '        Dim strcount As Integer = 0
            '        Dim strPriceCode As String = String.Empty
            '        Dim strMilkGradeCode As String = String.Empty
            '        For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
            '            If clsCommon.myCBool(gvPriceCode.Rows(ii).Cells("Sel").Value) Then
            '                strPriceCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
            '                If AllowMultiplePricewithMultipleVendor Then
            '                    strMilkGradeCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)
            '                End If
            '                strcount = strcount + 1
            '            End If
            '        Next
            '        If strcount > 1 Then
            '            isInsideLoadData = True
            '            gvPriceCode.CurrentRow.Cells("Sel").Value = False
            '            Throw New Exception("Select only one location at a time.")
            '        Else
            '            LoadVendor(strPriceCode, strMilkGradeCode)
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub gvPriceCode_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gvPriceCode.ValueChanging
        Try
            If Not isInsideLoadData Then
                If gvPriceCode.CurrentColumn Is gvPriceCode.Columns("Sel") Then
                    'If e.NewValue Then
                    '    Dim strcount As Integer = 0
                    '    Dim strPriceCode As String = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Price Code").Value)
                    '    Dim strMilkGradeCode As String = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Milk Grade code").Value)
                    '    LoadVendor(e.NewValue, strPriceCode, strMilkGradeCode)
                    '    isInsideLoadData = True
                    '    For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
                    '        If Not (clsCommon.CompairString(strPriceCode, clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMilkGradeCode, clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)) = CompairStringResult.Equal) Then
                    '            gvPriceCode.Rows(ii).Cells("Sel").Value = False
                    '        End If
                    '    Next
                    '    isInsideLoadData = False
                    'Else
                    '    LoadVendor(e.NewValue, "", "")
                    'End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        If chkVendorAll.IsChecked = True Then
            For ii As Integer = 0 To GvVendor.RowCount - 1
                GvVendor.Rows(ii).Cells("SEL").Value = True
            Next
        Else
            For ii As Integer = 0 To GvVendor.RowCount - 1
                GvVendor.Rows(ii).Cells("SEL").Value = False
            Next
        End If
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        Try
            Dim strMilkType As String = Nothing
            Dim qry As String = Nothing
         
            qry = "select count(*) from TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER where  Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndcode.MyReadOnly = False
            End If

            LoadData(fndcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim strMilkType As String = Nothing
        Dim qry As String = Nothing
        
        qry = "select * from (select distinct TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER.Document_No as Code,TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER.Document_Date as Date, " & _
            "TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date], " & _
            "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE]  from " & _
            "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER left outer join TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL on " & _
            "TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_MASTER.Document_No=TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Document_No " & _
            "left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.PriceCode " & _
            "left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code and TSPL_VENDOR_PRICE_CHART_MAPPING_DOCWISE_DETAIL.Milk_Grade_Code=TSPL_BULK_PRICE_DETAIL.Milk_Grade_code " & _
            "left outer join TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE ) aa"
        fndcode.Value = clsCommon.ShowSelectForm("VenPriceMASTER", qry, "Code", "", fndcode.Value, "", isButtonClicked)
        LoadData(fndcode.Value, NavigatorType.Current)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then

                If (clsVendorPriceChartMappingDocNoWise.PostData(MyBase.Form_ID, fndcode.Value)) Then
                    msg = "Successfully Posted"
                Else

                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndcode.Value, NavigatorType.Current)


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Reset()
    End Sub
End Class
