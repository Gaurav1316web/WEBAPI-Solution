'' Create New Screen agaist ticket no. BHA/14/08/18-000424,BHA/20/08/18-000464
Imports common
Imports System.Data.SqlClient

Public Class frmMccGatePass
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strQuery As String
    Dim strQueryCANCRate As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDatet"
    Const ColToSalesmanCode As String = "ColToSalesmanCode"
    Const ColToSalesmanname As String = "ColToSalesmanname"
    Const ColRoute_No As String = "ColRoute_No"
    Const ColRoute_Desc As String = "ColRoute_Desc"
    Const ColType As String = "ColType"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColPriceDesc As String = "ColPriceDesc"
    Const ColCustCode As String = "ColCustCode"
    Const ColCustName As String = "ColCustName"
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnit As String = "colUnit"
    Const colQty As String = "colQty"
    Const colLineNo As String = "colLineNo"
    Const colHSNCode As String = "colHSNCode"
    Dim atchqry As String = ""
    Dim AlternateVechileforGatePass As Double


    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemDesc)

        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "Unit"
        Unit.Name = colUnit
        Unit.Width = 100
        Unit.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Unit)

        Dim HSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        HSN.FormatString = ""
        HSN.HeaderText = "HSN Code"
        HSN.Name = colHSNCode
        HSN.Width = 100
        HSN.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(HSN)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoQty)


        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub
    Private Function LoadQuery(ByVal strItemCode As String) As String

        Dim strItem As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
        End If
        Dim StrChkAvQuery As String = ""
        If clsCommon.myLen(cmbtype) <= 0 Then
            Throw New Exception("select Type")
        End If
        If clsCommon.myLen(txtLocCode.Value) <= 0 Then
            Throw New Exception("select Location")
        End If


        If cmbtype.SelectedIndex = 1 Then

            StrChkAvQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " & _
                "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " & _
                "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " '"
            If clsCommon.myLen(strItemCode) <= 0 Then
                StrChkAvQuery += " And   isnull(GPCode,'') = '' "
            Else
                StrChkAvQuery += "  and TSPL_SD_SHIPMENT_DETAIL.Item_Code  ='" & strItemCode & "' "
            End If

            StrChkAvQuery += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "'  " & _
                    "  and TSPL_SD_SHIPMENT_head.Trans_Type = 'MCC'"
            If txtmultiBooking.arrValueMember IsNot Nothing AndAlso txtmultiBooking.arrValueMember.Count > 0 Then
                StrChkAvQuery += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ") " + Environment.NewLine
            End If
            'If clsCommon.myLen(txtVehicle.Value) > 0 Then
            '    StrChkAvQuery += "  and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" + txtVehicle.Value + "'"
            'End If


        Else
            If clsCommon.myLen(txtVehicle.Value) <= 0 Then
                Throw New Exception("select Vehicle")
            End If
            StrChkAvQuery = "select TSPL_SCRAPSALE_HEAD.shipment_No as [Document No],shipment_Date as [Document Date],TSPL_SCRAPSALE_HEAD.Cust_Code as Customer_Code,TSPL_SCRAPSALE_HEAD.cust_Name as Customer_Name, TSPL_SCRAPSALE_DETAIL.Item_Code as [Item Code],TSPL_SCRAPSALE_DETAIL.Item_Desc as [Item Desc],TSPL_SCRAPSALE_DETAIL.Unit_code as Unit,TSPL_SCRAPSALE_DETAIL.shipped_Qty as qty,TSPL_ITEM_MASTER.HSN_Code  from TSPL_SCRAPSALE_HEAD "
            StrChkAvQuery += " left outer join TSPL_SCRAPSALE_DETAIL on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No "
            StrChkAvQuery += " left outer join TSPL_ITEM_MASTER on TSPL_SCRAPSALE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code "
            StrChkAvQuery += " left outer join TSPL_CUSTOMER_MASTER on TSPL_SCRAPSALE_HEAD.cust_Name=TSPL_CUSTOMER_MASTER.Cust_Code  "
            StrChkAvQuery += " where convert(date,shipment_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' "

            If clsCommon.myLen(strItemCode) <= 0 Then
                StrChkAvQuery += " And   isnull(GPCode,'') = ''  "
            Else
                StrChkAvQuery += "  and TSPL_SCRAPSALE_DETAIL.Item_Code ='" & strItemCode & "' "
            End If
            StrChkAvQuery += " and TSPL_SCRAPSALE_HEAD.Loc_Code='" & txtLocCode.Value & "'  " & _
                  "   and TSPL_SCRAPSALE_HEAD.Vehicle_Id='" + txtVehicle.Value + "' "
            If txtmultiBooking.arrValueMember IsNot Nothing AndAlso txtmultiBooking.arrValueMember.Count > 0 Then
                StrChkAvQuery += " and TSPL_SCRAPSALE_HEAD.shipment_No in (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ") " + Environment.NewLine
            End If
        End If
        strQuery = StrChkAvQuery
    
        '=====================================================
        Return strQuery
    End Function
    Private Function GetQuery(ByVal strItemCode As String, ByVal strGPCode As String) As String
        Dim strItem As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
        End If

        strQuery = "select TSPL_MCC_SCRAP_GATEPASS_Master.GPCode as [Document No],GPDate as [Document Date],'' as Customer_Code,'' as Customer_Name, " & _
            "TSPL_MCC_SCRAP_GATEPASS_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_MCC_SCRAP_GATEPASS_DETAIL.Unit_code as Unit,Qty,TSPL_MCC_SCRAP_GATEPASS_DETAIL.HSN_Code,TSPL_MCC_SCRAP_INVOICE_GATEPASS_DETAIL.InvoiceNo  " & _
            "from TSPL_MCC_SCRAP_GATEPASS_Master left outer join TSPL_MCC_SCRAP_GATEPASS_DETAIL on TSPL_MCC_SCRAP_GATEPASS_Master.GPCode=TSPL_MCC_SCRAP_GATEPASS_DETAIL.GPCode  " & _
            "left outer join TSPL_ITEM_MASTER on TSPL_MCC_SCRAP_GATEPASS_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   " & _
            "left outer join TSPL_MCC_SCRAP_INVOICE_GATEPASS_DETAIL on TSPL_MCC_SCRAP_INVOICE_GATEPASS_DETAIL.GPCode=TSPL_MCC_SCRAP_GATEPASS_Master.GPCode " & _
            "where TSPL_MCC_SCRAP_GATEPASS_Master.GPCode='" & strGPCode & "' "
        Return strQuery
    End Function
    Private Sub funFillGrid()
        Try

            LoadBlankGrid()
            Dim qry As String = LoadQuery("")
            If clsCommon.myLen(qry) > 0 Then
                strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code,max(Customer_Code) as Customer_Code,max(Customer_Name) as Customer_Name  from ( " & qry & " ) final group by [Item Code],Unit "
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    txtDate.Enabled = False

                Next
                ' **************************************************************************************************
                txtmultiBooking.Enabled = True
                'Dim strAllDoc As String = " select STUFF((SELECT ',' + Document_Code from (select distinct PPPP.Document_Code from  ( " & qry & "    ) As PPPP  ) Final FOR XML PATH('')), 1, 1, '') "
                Dim strAllDoc As String = " select distinct PPPP.[Document No] from  ( " & qry & "    ) As PPPP   "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strAllDoc)
                Dim list As New ArrayList
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        list.Add(dr("Document No"))
                    Next
                End If
                txtmultiBooking.arrValueMember = list
                ' **************************************************************************************************
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "GatePass Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub funLoadGrid(ByVal strGPCOde As String)
        Try
            LoadBlankGrid()
            Dim qry As String = GetQuery("", strGPCOde)
            strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code from ( " & qry & " ) final group by [Item Code],Unit "

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    txtDate.Enabled = False

                Next
                Dim strAllDoc As String = " select distinct PPPP.InvoiceNo from  ( " & qry & "    ) As PPPP   "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strAllDoc)
                Dim list As New ArrayList
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        list.Add(dr("InvoiceNo"))
                    Next
                End If
                txtmultiBooking.arrValueMember = list
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "GatePass Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()

            Dim obj As New clsMccScrapGatePass()
            obj = clsMccScrapGatePass.GetData(strCode, NavTyep, "Mcc")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GPCode) > 0) Then
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.Post = "Y" Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                End If
                txtCode.Value = obj.GPCode
                txtVehicle.Value = obj.Vehicle_Id
                lblVehicleDesc.Text = obj.Vehicle_Number
                cmbtype.Text = obj.Item_Type

                txtComments.Text = obj.Comments
                txtRemarks.Text = obj.Remarks
                isInsideLoadData = True
                txtDate.Value = obj.GPDate
                txtLocCode.Value = obj.Location_Code
                txtLocDesc.Text = obj.Location_Desc

                cmbtype.Enabled = False

                funLoadGrid(txtCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.CompairString(cmbtype.SelectedIndex, 2) = CompairStringResult.Equal Then
            If clsCommon.myLen(txtVehicle.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vehicle No", Me.Text)
                txtVehicle.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtLocCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            txtLocCode.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = ""
        Dim strvehiclenum As String = lblVehicleDesc.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicle.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicle.Value + "'"
            lblVehicleDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "



            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVehicleDesc.Text) <= 0 Then
                    lblVehicleDesc.Focus()
                    Throw New Exception("Please Enter Vehicle No")
                End If


                txtVehicle.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")))
                'strvehiclenum = txtVehicle.Text
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = CStr(connectSql.RunScalar(sql))
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", txtVehicle.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                    connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicle.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

                    trans.Commit()
                Catch ex As Exception
                    txtVehicle.Value = ""
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

                'lblVehicleDesc.Text = txtVehicle.Text + "-Hired"
                txtVehicle.Text = txtVehicle.Value
                Return True
            Else
                txtVehicle.Value = String.Empty
                txtVehicle.Text = txtVehicle.Value
                Return False
            End If
        End If
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMccScrapGatePass()
                obj.GPCode = txtCode.Value
                obj.GPDate = clsCommon.myCDate(txtDate.Value)
                obj.Vehicle_Id = txtVehicle.Value
                obj.Vehicle_Number = lblVehicleDesc.Text
                obj.Item_Type = cmbtype.Text

                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComments.Text
                obj.Location_Code = txtLocCode.Value
                obj.Location_Desc = txtLocDesc.Text
                
                'obj.AgainstDocumentCode = clsCommon.GetMulcallString(txtmultiBooking.arrValueMember)
                '=======================================================
                obj.Arr = New List(Of clsMccScrapGatepassDetail)
                obj.InvoiceArr = New List(Of clsMccScrapGatepassDetail)
                '' Invoice added
                For Each Multi As String In txtmultiBooking.arrValueMember
                    Dim objTr As New clsMccScrapGatepassDetail()
                    objTr.InvoiceNo = Multi
                    obj.InvoiceArr.Add(objTr)
                Next
                '' End
                For Each grow As GridViewRowInfo In Gv1.Rows
                    Dim objTr As New clsMccScrapGatepassDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.HSN_Code = clsCommon.myCstr(grow.Cells(colHSNCode).Value)
                    obj.Arr.Add(objTr)

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Document", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, "MCC")) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    LoadData(obj.GPCode, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        txtVehicle.Value = ""
        lblVehicleDesc.Text = ""
        txtLocCode.Value = ""
        txtLocDesc.Text = ""

        txtRemarks.Text = ""
        txtComments.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnSave.Text = "Save"
        LoadBlankGrid()
        isNewEntry = True
        cmbtype.Text = "Select"
        isInsideLoadData = False


        txtmultiBooking.arrValueMember = Nothing
    End Sub

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Try
    '        PrintGatePass(txtCode.Value)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbitemtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        'If isInsideLoadData = False Then
        '    txtVehicle.Value = ""
        '    lblVehicleDesc.Text = ""
        '    txtTransporter.Text = ""
        '    txtRemarks.Text = ""
        '    txtComments.Text = ""
        '    LoadBlankGrid()
        '    isNewEntry = True
        '    isInsideLoadData = False
        '    funFillGrid()
        'End If
    End Sub
    Private Sub txtDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If isInsideLoadData = False Then
        '    If clsCommon.CompairString(cmbitemtype.Text, "Select") = CompairStringResult.Equal Then
        '        clsCommon.MyMessageBoxShow("Please select Item type")
        '    Else
        '        funFillGrid()
        '    End If
        'End If
    End Sub

    Private Sub Gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtLocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocCode._MYValidating
        strQuery = "select distinct Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER "
        txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "", txtLocCode.Value, "Code", isButtonClicked)
        txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If isInsideLoadData = False Then

            LoadBlankGrid()
            isNewEntry = True
            isInsideLoadData = False

            If clsCommon.myLen(txtLocCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Location", Me.Text)
                Exit Sub
            End If
            If clsCommon.CompairString(cmbtype.SelectedIndex, -1) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Select Type", Me.Text)
                Exit Sub
            End If

            funFillGrid()

        End If


    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmGatePassFS)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag


    End Sub

    Private Sub frmDairyGatePass_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            btnNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "TSPL_DAIRYSALE_GATEPASS_MASTER " + Environment.NewLine + _
                                         "TSPL_DAIRYSALE_GATEPASS_DETAIL  ")
        End If

    End Sub

    Private Sub FrmGatePassENtry1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        cmbtype.Text = "Select"

        txtRemarks.MaxLength = 200
        txtComments.MaxLength = 200
        txtVehicle.Enabled = False

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        txtmultiBooking.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtVehicle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicle._MYValidating
        strQuery = "select distinct Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
        txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", txtVehicle.Value, "Code", isButtonClicked)
        lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MCC_SCRAP_GATEPASS_MASTER where GPCode='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If

            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = " SELECT  GPCode,convert(varchar(10),GPDate,103)  as GPDate,Vehicle_Id,Vehicle_Number FROM  TSPL_MCC_SCRAP_GATEPASS_MASTER"

        LoadData(clsCommon.ShowSelectForm("GatepassEntry", qry, "GPCode", "", txtCode.Value, "GPCode", isButtonClicked), NavigatorType.Current)

        If clsCommon.myLen(txtCode.Value) > 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        funLoadGrid(txtCode.Value)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Addnew()
        cmbtype.Enabled = True
        txtDate.Enabled = True
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Post Record", Me.Text)
            Exit Sub
        End If
        If myMessages.postConfirm() Then
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_DAIRYSALE_GATEPASS_MASTER set post='Y' where gpcode='" & txtCode.Value & "'")
            btnSave.Enabled = False
            btnPost.Enabled = False
        End If
    End Sub

    Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim I As Integer = 0
        If (Gv1.Rows.Count > 0) AndAlso clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In Gv1.Rows
                grow.Cells(ColApply).Value = True
            Next
            btnSelect.Text = "Unselect All"
        ElseIf (Gv1.Rows.Count > 0) AndAlso clsCommon.CompairString(btnSelect.Text, "Unselect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In Gv1.Rows
                grow.Cells(ColApply).Value = False
            Next
            btnSelect.Text = "Select All"
        End If
    End Sub

    Private Sub Gv1_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles Gv1.CurrentRowChanged

    End Sub
    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            Dim strItem = Gv1.Rows(e.RowIndex).Cells(1).Value
            strQuery = LoadQuery(strItem)
            Dim frmStock As New FrmStockDetail
            frmStock.LoadDispatchData(strQuery)
            frmStock.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetAttachQry(ByVal StrCode As String) As String
        Dim Qry As String = "  Select Final.* ,tbl_Brand.Brand,tbl_Brand.BRANDDESC FROM ( select TSPL_COMPANY_MASTER.comp_name,TSPL_MCC_SCRAP_GATEPASS_DETAIL.unit_code,TSPL_MCC_SCRAP_GATEPASS_DETAIL.qty, TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end as Comp_Address, tspl_location_master.add1 +case when len(tspl_location_master.add2)>0 then ', '+tspl_location_master.add2 else '' end +case when LEN(isnull(tspl_location_master.Add3,''))>0 then ', '+isnull(tspl_location_master.Add3,'') else ' ' end as Loc_add, TSPL_MCC_SCRAP_GATEPASS_MASTER.Totalcrate,TSPL_MCC_SCRAP_GATEPASS_MASTER.TotalCan,TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode,convert(varchar,TSPL_MCC_SCRAP_GATEPASS_MASTER.GPDate,103) as GPDate,FORMAT(TSPL_MCC_SCRAP_GATEPASS_MASTER.GPDate,'hh:mm tt') as GPTime"
        Qry += " ,TSPL_MCC_SCRAP_GATEPASS_MASTER.Vehicle_Id as vehicle_id,TSPL_MCC_SCRAP_GATEPASS_MASTER.Vehicle_Number as VehicleDesc,TSPL_MCC_SCRAP_GATEPASS_MASTER.location_code,tspl_location_master.Location_desc,TSPL_MCC_SCRAP_GATEPASS_MASTER.remarks,TSPL_MCC_SCRAP_GATEPASS_MASTER.comments,TSPL_MCC_SCRAP_GATEPASS_MASTER.post,TSPL_MCC_SCRAP_GATEPASS_DETAIL.Item_code,tspl_item_master.item_desc,tspl_item_master.short_description,tspl_item_master.sku_seq,TSPL_MCC_SCRAP_GATEPASS_DETAIL.HSN_Code "
        Qry += " from TSPL_MCC_SCRAP_GATEPASS_DETAIL  left outer join TSPL_MCC_SCRAP_GATEPASS_MASTER on TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode=TSPL_MCC_SCRAP_GATEPASS_DETAIL.GPCode  "
        Qry += " left outer join tspl_vehicle_master on tspl_vehicle_master.Vehicle_id=TSPL_MCC_SCRAP_GATEPASS_MASTER.vehicle_id  "
        Qry += " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_SCRAP_GATEPASS_MASTER.location_code  "
        Qry += " left outer join tspl_company_master on tspl_company_master.comp_code=TSPL_MCC_SCRAP_GATEPASS_MASTER.comp_code  "
        Qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MCC_SCRAP_GATEPASS_DETAIL.Item_code  "
        Qry += " where 2=2   and  TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = '" + StrCode + "'  ) AS Final "
        Qry += "  left outer join  ( select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND],  max([DESCRP]) as [DESCRP],max([PACK]) as [PACK], max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA],  max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW], max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC])  as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC], max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC], max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC], max([SCRAPDESC]) as [SCRAPDESC]  from ( select * from (   select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   , TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER    left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and  TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values   where 2=2 )xx  Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE],  [CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP])  ) Pivt   Pivot  ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC], [BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC], [CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC])  ) Pivt1 ) xxx  group by Item_Code )  as tbl_Brand on tbl_Brand.Item_Code=Final.Item_Code  order by Final.sku_seq"

        Return Qry

    End Function
    Public Sub funPrint(ByVal Code As String)
        Try
            atchqry = GetAttachQry(Code)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntry", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("No data found to Print")
        Else
            funPrint(txtCode.Value)
        End If
    End Sub

   

    Private Sub txtmultiBooking__My_Click(sender As Object, e As EventArgs) Handles txtmultiBooking._My_Click
        Try
            Dim qry As String = LoadQuery("")
        Dim strAllDoc As String = " select distinct PPPP.[Document No] as [Document Code], convert (varchar, PPPP .[Document Date], 103) as [Document Date],pppp.[Customer_Code] as [Code],PPPP.[Customer_Name] as [Name] from  ( " & qry & "    ) As PPPP   "
        txtmultiBooking.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel", strAllDoc, "Document Code", "Document code", txtmultiBooking.arrValueMember, txtmultiBooking.arrDispalyMember)
            funFillGrid2()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub funFillGrid2()
        Try

            LoadBlankGrid()
            Dim qry As String = LoadQuery("")
            strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code  from ( select xxx.* from(  " & qry & " )xxx  )  final group by [Item Code],Unit "

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    txtDate.Enabled = False
                Next
                ' **************************************************************************************************
                txtmultiBooking.Enabled = True
                'Dim strAllDoc As String = " select STUFF((SELECT ',' + Document_Code from (select distinct PPPP.Document_Code from  ( " & qry & "    ) As PPPP  ) Final FOR XML PATH('')), 1, 1, '') "
                Dim strAllDoc As String = " select distinct PPPP.[Document No] from  ( " & qry & "    ) As PPPP   "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strAllDoc)
                Dim list As New ArrayList
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        list.Add(dr("Document No"))
                    Next
                End If
                txtmultiBooking.arrValueMember = list
                ' **************************************************************************************************
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "GatePass Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbtype.SelectedIndexChanged
        If cmbtype.SelectedIndex = 1 Then
            txtVehicle.Enabled = False
        Else
            txtVehicle.Enabled = True
        End If
    End Sub
End Class
