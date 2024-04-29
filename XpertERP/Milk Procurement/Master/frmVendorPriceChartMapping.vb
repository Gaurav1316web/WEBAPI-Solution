'===============BM00000007844===============================
Imports common
Imports System.Data.SqlClient

Public Class frmVendorPriceChartMapping
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
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        fndPriceCode.Value = ""
        isLoadData = False
        isValueChanged = True
        LoadBlankGrid()
        isNewEntry = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
        If AllowMultiplePricewithMultipleVendor = 1 Then
            LoadPriceData()
            LoadVendor(False, "", "")
        End If
    End Sub

    Sub LoadData()
        Try
            LoadBlankGrid()
            isNewEntry = True
            Dim qry As String = "select count(*) from tspl_vendor_Price_Chart_mapping where PriceCode='" & fndPriceCode.Value & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select tspl_vendor_Price_Chart_mapping.VendorCode,isnull(tspl_vendor_Price_Chart_mapping.isDefault,0) as isDefault from tspl_vendor_Price_Chart_mapping where priceCode='" & fndPriceCode.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                isLoadData = True
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    isNewEntry = False
                    For Each dr As DataRow In dt.Rows
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value = clsCommon.myCstr(dr("VendorCode"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colVendorDesc).Value = clsVendorMaster.GetName(gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsDefault).Value = IIf(clsCommon.myCdbl(dr("isDefault")) = 1, True, False)
                    Next
                    btnsave.Text = "Update"
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.F12 Then
            Dim frm As New frmVendorPriceChartMappingUDL
            frm.SetUserMgmt(MyBase.Form_ID)
            'frm.MdiParent = MDI
            frm.Show()
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
        If IsItemMilkType = 1 Then
            If IsItemMilkType = 1 Then
                strMilkType = " and TSPL_Bulk_Price_MASTER.milk_type_code <> '' "
            End If
            strqry = "sELECT cast(0 as bit) as Sel,TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date], " & _
                "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code as [Milk Grade code],TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE] FROM " & _
                "TSPL_Bulk_Price_MASTER  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                "left outer join TSPL_MILK_GRADE_MASTER on TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  where TSPL_Bulk_Price_MASTER.Posted=1  " & strMilkType & ""
        Else
            strqry = "sELECT cast(0 as bit) as Sel,TSPL_Bulk_Price_MASTER.Price_Code As [Price Code],TSPL_Bulk_Price_MASTER.Price_Date as [Price Date] FROM TSPL_Bulk_Price_MASTER  where TSPL_Bulk_Price_MASTER.Posted=1  "
        End If
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

        If IsItemMilkType = 1 Then
            gvPriceCode.Columns("Milk Grade code").IsVisible = True
            gvPriceCode.Columns("GRADE TYPE").IsVisible = True
        End If

        gvPriceCode.AllowAddNewRow = False
        gvPriceCode.ShowGroupPanel = False
        gvPriceCode.AllowColumnReorder = False
        gvPriceCode.AllowRowReorder = False
        gvPriceCode.EnableSorting = False
        gvPriceCode.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPriceCode.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Sub LoadVendor(ByVal NewVal As Boolean, ByVal PriceCode As String, ByVal MilkGradeCode As String)
        GvVendor.DataSource = Nothing
        Dim qry As String = Nothing
        If IsPriceChartGradeWise = 1 Then
            qry = "Select Final.* from (sELECT cast(1 as bit) as Sel,Tspl_Vendor_Price_Chart_mapping.SequenceNo,Tspl_Vendor_Price_Chart_mapping.VendorCode As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM Tspl_Vendor_Price_Chart_mapping  left outer join TSPL_VENDOR_MASTER on Tspl_Vendor_Price_Chart_mapping.VendorCode=TSPL_VENDOR_MASTER.vendor_code  where TSPL_VENDOR_MASTER.Status='N' and Tspl_Vendor_Price_Chart_mapping.PriceCode ='" & PriceCode & "'  and Milk_Grade_Code='" & MilkGradeCode & "' " & _
      " union all " & _
      "  SELECT cast(0 as bit) as Sel,0 as SequenceNo,TSPL_VENDOR_MASTER.vendor_code As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM TSPL_VENDOR_MASTER  where TSPL_VENDOR_MASTER.Status='N' and    TSPL_VENDOR_MASTER.vendor_code not in (sELECT Tspl_Vendor_Price_Chart_mapping.VendorCode FROM Tspl_Vendor_Price_Chart_mapping where Tspl_Vendor_Price_Chart_mapping.PriceCode= '" & PriceCode & "' and Milk_Grade_Code='" & MilkGradeCode & "' )) Final ORDER BY fINAL.[Vendor Code]  "
        Else
            qry = "Select Final.* from (sELECT cast(1 as bit) as Sel,Tspl_Vendor_Price_Chart_mapping.SequenceNo,Tspl_Vendor_Price_Chart_mapping.VendorCode As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM Tspl_Vendor_Price_Chart_mapping  left outer join TSPL_VENDOR_MASTER on Tspl_Vendor_Price_Chart_mapping.VendorCode=TSPL_VENDOR_MASTER.vendor_code  where TSPL_VENDOR_MASTER.Status='N' and  Tspl_Vendor_Price_Chart_mapping.PriceCode ='" & PriceCode & "' " & _
      " union all " & _
      "  SELECT cast(0 as bit) as Sel,0 as SequenceNo,TSPL_VENDOR_MASTER.vendor_code As [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as Description FROM TSPL_VENDOR_MASTER  where  TSPL_VENDOR_MASTER.Status='N' and   TSPL_VENDOR_MASTER.vendor_code not in (sELECT Tspl_Vendor_Price_Chart_mapping.VendorCode FROM Tspl_Vendor_Price_Chart_mapping where Tspl_Vendor_Price_Chart_mapping.PriceCode= '" & PriceCode & "' )) Final ORDER BY fINAL.[Vendor Code]  "
        End If

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
                    clsCommon.MyMessageBoxShow(Me, "Select only one location at a time.", Me.Text)
                    Return False
                End If


                For i As Integer = 0 To GvVendor.Rows.Count - 1
                    dblsequenceno = clsCommon.myCdbl(GvVendor.Rows(i).Cells("SequenceNo").Value)
                    If dblsequenceno <> 0 Then
                        For j As Integer = i + 1 To GvVendor.Rows.Count - 1
                            dblsequencenoInter = clsCommon.myCdbl(GvVendor.Rows(j).Cells("SequenceNo").Value)
                            If dblsequenceno = dblsequencenoInter Then
                                clsCommon.MyMessageBoxShow(Me, "Sequence no should not be same for two customers.", Me.Text)
                                Return False
                            End If
                        Next
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Function AllowToImport(ByVal gv1 As RadGridView) As Boolean
        Try
            Dim Vendorcode As String = ""
            Dim PriceCode As String = Nothing
            Dim Vendorcode1 As String = ""
            Dim PriceCode1 As String = Nothing
            Dim isDefault As Integer = 0
            If AllowMultiplePricewithMultipleVendor = 0 Then

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

                    If clsCommon.myLen(PriceCode) <= 0 Then
                        Throw New Exception("please fill Price Code  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    isDefault = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Default(Yes/No)").Value), "Yes"), 1, 0)

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

                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        Vendorcode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("VendorCode").Value)
                        PriceCode1 = clsCommon.myCstr(gv1.Rows(jj).Cells("PriceCode").Value)
                        If ii <> jj AndAlso clsCommon.myLen(PriceCode) > 0 AndAlso clsCommon.myLen(Vendorcode) > 0 AndAlso clsCommon.CompairString(Vendorcode, Vendorcode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(PriceCode, PriceCode1) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Values At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                        End If
                    Next

                Next
            Else
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
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Sub SaveData()
        Dim strpriceCode As String = ""
        Dim strMilkGradeCode As String = ""
        Dim qry As String = ""

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVendorPriceChartMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arr As New List(Of clsVendorPriceChartMapping)
            If AllowMultiplePricewithMultipleVendor = 0 Then
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsVendorPriceChartMapping
                    obj.VendorCode = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                    obj.Pricecode = clsCommon.myCstr(fndPriceCode.Value)
                    obj.isDefault = clsCommon.myCdbl(grow.Cells(colIsDefault).Value)
                    If clsCommon.myLen(obj.VendorCode) > 0 Then
                        arr.Add(obj)
                    End If
                Next
            Else

                For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
                    If clsCommon.myCBool(gvPriceCode.Rows(ii).Cells("Sel").Value) Then
                        strpriceCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)
                        strMilkGradeCode = clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade Code").Value)
                        Exit For
                    End If
                Next
                Dim intCount As Integer = 0
                For ii As Integer = 0 To GvVendor.Rows.Count - 1
                    intCount = 0
                    If clsCommon.myCBool(GvVendor.Rows(ii).Cells("Sel").Value) Then
                        Dim obj As clsVendorPriceChartMapping = New clsVendorPriceChartMapping()
                        obj.VendorCode = clsCommon.myCstr(GvVendor.Rows(ii).Cells("Vendor Code").Value)
                        obj.SequenceNo = clsCommon.myCdbl(GvVendor.Rows(ii).Cells("SequenceNo").Value)
                        obj.Pricecode = strpriceCode
                        obj.Milk_Grade_Code = strMilkGradeCode
                        Dim strPriceCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(( select distinct  ' ['+Tspl_Vendor_Price_Chart_mapping.PriceCode+' ] '  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & obj.VendorCode & "' and PriceCode not in ('" & strpriceCode & "')  for xml path('')),'')  as DocNo  ", trans))
                        If clsCommon.myLen(strPriceCodeExist) > 0 Then
                            Dim strSRNNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull((Select distinct '['+TSPL_Bulk_milk_srn.srn_no+']  ' from TSPL_Bulk_milk_srn left outer join " & _
                            "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.srn_no=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.srn_no " & _
                            "where TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Price_Code not in ('" & strpriceCode & "') and isPosted=0 and " & _
                            "TSPL_Bulk_milk_srn.Vendor_Code='" & obj.VendorCode & "'   for xml path('')),'')  as DocNo ", trans))
                            'If clsCommon.myLen(strSRNNo) > 0 Then
                            '    clsCommon.MyMessageBoxShow("This Vendor is already mapped with Unposted SRN. Please post These transaction " & strSRNNo & " Before mapping with new priceCode.")
                            '    Exit Sub
                            'End If
                            'Dim intPriceType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & strpriceCode & "'", trans))
                            'Dim strPriceTypeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct price_code from TSPL_Bulk_Price_MASTER where Price_Code in (select PriceCode from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & obj.VendorCode & "' and PriceCode not in ('" & strpriceCode & "')) and IsPrice_GradeWise='" & IIf(intPriceType = 1, 0, 1) & "'", trans))
                            'If clsCommon.myLen(strPriceTypeExist) > 0 Then
                            '    clsCommon.MyMessageBoxShow("This Vendor is already mapped with " & strPriceCodeExist & "  with different price type  ." + Environment.NewLine + "Please unselect  then mapped with this new price code")
                            '    Exit Sub
                            'End If
                        End If
                        intCount += 1
                        arr.Add(obj)
                    End If               
                Next
                If intCount = 0 Then
                    Dim intPriceType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & strpriceCode & "'", trans))
                    If intPriceType = 0 Then
                        qry = "Delete from tspl_Vendor_price_chart_mapping where PriceCode='" & strpriceCode & "' "
                    Else
                        qry = "Delete from tspl_Vendor_price_chart_mapping where PriceCode='" & strpriceCode & "' and  Milk_Grade_Code='" & strMilkGradeCode & "'"
                    End If
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            If clsVendorPriceChartMapping.SaveData(strpriceCode, arr, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then

                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                btnsave.Text = "Update"
                btndelete.Enabled = True
                LoadData()
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndPriceCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select a Price code to delete", Me.Text)
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            Dim qry As String = " delete from tspl_vendor_Price_Chart_mapping where priceCode='" & fndPriceCode.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            fndPriceCode.Value = ""
            LoadData()
            myMessages.delete()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_vendor_Price_Chart_mapping "
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If AllowMultiplePricewithMultipleVendor = 0 Then
            If check > 0 Then
                qry = " select Tspl_Vendor_Price_Chart_mapping.PriceCode,TSPL_Bulk_Price_MASTER.Price_Date as [Price Date],TSPL_Bulk_Price_MASTER.Fat_Percentage as [Fat %],TSPL_Bulk_Price_MASTER.Snf_Percentage as [SNF %],TSPL_Bulk_Price_MASTER.Fat_Weightage as [Fat Weightage],TSPL_Bulk_Price_MASTER.Snf_Weightage as [SNF Weightage],TSPL_Bulk_Price_MASTER.Standard_Rate as [Standard Rate],Tspl_Vendor_Price_Chart_mapping.VendorCode,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],case when isnull(Tspl_Vendor_Price_Chart_mapping.isDefault,0)=0 then 'No' else 'Yes' end as  [Default(Yes/No)]  from Tspl_Vendor_Price_Chart_mapping left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=Tspl_Vendor_Price_Chart_mapping.PriceCode left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=Tspl_Vendor_Price_Chart_mapping.VendorCode "
            Else
                qry = "  select '' as PriceCode,'' as [Price Date],'' as [Fat %],'' as [SNF %],'' as [Fat Weightage],'' as [SNF Weightage],'' as [Standard Rate],'' as VendorCode,''  as [Vendor Name],'' as  [Default(Yes/No)]"
            End If
        Else
            If check > 0 Then
                qry = " select Tspl_Vendor_Price_Chart_mapping.PriceCode,Tspl_Vendor_Price_Chart_mapping.VendorCode,Tspl_Vendor_Price_Chart_mapping.Milk_Grade_Code as [Milk Grade Code] from Tspl_Vendor_Price_Chart_mapping "
            Else
                qry = "  select '' as PriceCode,'' as VendorCode,'' as [Milk Grade Code]"
            End If
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = Nothing
        If AllowMultiplePricewithMultipleVendor = 0 Then
            If transportSql.importExcel(gv1, "PriceCode", "VendorCode", "Default(Yes/No)") Then
                Try
                    clsCommon.ProgressBarShow()
                    If AllowToImport(gv1) Then
                        trans = clsDBFuncationality.GetTransactin()
                        Dim arr As New List(Of clsVendorPriceChartMapping)
                        For Each grow As GridViewRowInfo In gv1.Rows
                            Dim obj As New clsVendorPriceChartMapping
                            obj.Pricecode = clsCommon.myCstr(grow.Cells("PriceCode").Value)
                            obj.VendorCode = clsCommon.myCstr(grow.Cells("VendorCode").Value)
                            obj.isDefault = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default(Yes/No)").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                            If clsCommon.myLen(obj.VendorCode) > 0 AndAlso clsCommon.myLen(obj.Pricecode) > 0 Then
                                arr.Add(obj)
                            End If
                        Next
                        If clsVendorPriceChartMapping.SaveData("", arr, trans) Then
                            trans.Commit()
                            clsCommon.ProgressBarHide()
                            clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                        Else
                            trans.Rollback()
                            clsCommon.ProgressBarHide()
                            clsCommon.MyMessageBoxShow(Me, "No Data Transfer", Me.Text)
                        End If
                    End If
                    clsCommon.ProgressBarHide()
                    Reset()
                    LoadData()
                Catch ex As Exception
                    Try
                        trans.Rollback()
                    Catch exx As Exception
                    End Try
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If

        Else

            If transportSql.importExcel(gv1, "PriceCode", "VendorCode", "Milk Grade Code") Then
                Try
                    clsCommon.ProgressBarShow()
                    If AllowToImport(gv1) Then
                            trans = clsDBFuncationality.GetTransactin()
                            Dim arr As New List(Of clsVendorPriceChartMapping)
                            For Each grow As GridViewRowInfo In gv1.Rows
                                Dim obj As New clsVendorPriceChartMapping
                                obj.Pricecode = clsCommon.myCstr(grow.Cells("PriceCode").Value)
                                obj.VendorCode = clsCommon.myCstr(grow.Cells("VendorCode").Value)
                                obj.Milk_Grade_Code = clsCommon.myCstr(grow.Cells("Milk Grade Code").Value)
                               
                                If IsPriceChartGradeWise = 1 Then
                                    If clsCommon.myLen(obj.VendorCode) > 0 AndAlso clsCommon.myLen(obj.Pricecode) > 0 AndAlso clsCommon.myLen(obj.Milk_Grade_Code) > 0 Then
                                        arr.Add(obj)
                                    End If
                                Else
                                    If clsCommon.myLen(obj.VendorCode) > 0 AndAlso clsCommon.myLen(obj.Pricecode) > 0 Then
                                        arr.Add(obj)
                                    End If
                                End If
                                Dim isNewEntry As Boolean = False
                                If clsCommon.myLen(obj.Pricecode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from tspl_Vendor_price_chart_mapping where Pricecode ='" & obj.Pricecode & "' and VendorCode ='" & obj.VendorCode & "' and Milk_Grade_Code='" & obj.Milk_Grade_Code & "'", trans) > 0 Then
                                    isNewEntry = False
                                Else
                                    isNewEntry = True
                                End If
                                    Dim coll As New Hashtable()
                                    clsCommon.AddColumnsForChange(coll, "Pricecode", obj.Pricecode)
                                    clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
                                    clsCommon.AddColumnsForChange(coll, "isDefault", obj.isDefault)
                                    clsCommon.AddColumnsForChange(coll, "Milk_Grade_Code", obj.Milk_Grade_Code)
                                    clsCommon.AddColumnsForChange(coll, "SequenceNo", obj.SequenceNo)

                                    If isNewEntry Then
                                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_Vendor_price_chart_mapping", OMInsertOrUpdate.Insert, "", trans)
                                        'Else
                                        '    clsCommonFunctionality.UpdateDataTable(coll, "tspl_Vendor_price_chart_mapping", OMInsertOrUpdate.Update, "tspl_Vendor_price_chart_mapping.Location_Code='" & obj.Pricecode & "' and  tspl_Vendor_price_chart_mapping.Customer_Code ='" & strCustomerCode & "' ", trans)
                                    End If
                                                             
                            Next
                      
                        trans.Commit()
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                    End If
                    clsCommon.ProgressBarHide()
                    Reset()
                    LoadData()
                Catch ex As Exception
                    Try
                        trans.Rollback()
                    Catch exx As Exception
                    End Try
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        End If


        
        Me.Controls.Remove(gv1)

    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoadData Then '------when on loaddata then it should not run
            isValueChanged = True
            If isValueChanged Then
                isValueChanged = False
                If gv.CurrentColumn Is gv.Columns(colVendorCode) Then
                    gv.CurrentRow.Cells(colVendorCode).Value = clsVendorMaster.getFinder("Coalesce(form_Type,'')='All' AND Status='N' ", gv.CurrentRow.Cells(colVendorCode).Value, False)
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
            LoadData()
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub gvPriceCode_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gvPriceCode.ValueChanging
        Try
            If Not isInsideLoadData Then
                If gvPriceCode.CurrentColumn Is gvPriceCode.Columns("Sel") Then
                    If e.NewValue Then
                        Dim strcount As Integer = 0
                        Dim strPriceCode As String = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Price Code").Value)
                        Dim strMilkGradeCode As String = clsCommon.myCstr(gvPriceCode.CurrentRow.Cells("Milk Grade code").Value)
                        LoadVendor(e.NewValue, strPriceCode, strMilkGradeCode)
                        isInsideLoadData = True
                        For ii As Integer = 0 To gvPriceCode.Rows.Count - 1
                            If Not (clsCommon.CompairString(strPriceCode, clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Price Code").Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMilkGradeCode, clsCommon.myCstr(gvPriceCode.Rows(ii).Cells("Milk Grade code").Value)) = CompairStringResult.Equal) Then
                                gvPriceCode.Rows(ii).Cells("Sel").Value = False
                            End If
                        Next
                        isInsideLoadData = False
                    Else
                        LoadVendor(e.NewValue, "", "")
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
End Class
