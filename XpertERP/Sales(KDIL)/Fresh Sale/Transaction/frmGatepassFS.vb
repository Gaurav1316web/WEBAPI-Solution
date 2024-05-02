' '' '' '' ''last modified by 05/07/2013 10:20 pm
''Last Modify by Dipti Waila 16/07/2013 12:10 pm for Select/Unselect Button.
''Last Modify by priti 23/10/2013 02:00 pm  
'''' for bug no BM00000000880

Imports common
Imports System.Data.SqlClient

Public Class FrmGatePassFS
    Inherits FrmMainTranScreen
    Dim strQuery As String
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
        '=====sanjeet(Check Alternate Vechile For Gate Pass)==========
        If AlternateVechileforGatePass.Equals(1) Then

            Dim StrChkAvQuery As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " & _
                "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " & _
                "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " & _
                "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" & txtVehicle.Value & "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(StrChkAvQuery)
            If dt.Rows.Count > 0 Then

                strQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " & _
                    "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " & _
                    "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                    "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " & _
                    "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" & txtVehicle.Value & "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "
            Else
                strQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " & _
                   "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " & _
                   "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                   "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                   "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                   "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " & _
                   "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" & txtVehicle.Value & "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "
            End If
        Else
            strQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " & _
                    "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " & _
                    "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                    "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " & _
                    "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" & txtVehicle.Value & "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "
        End If

        '=====================================================
        Return strQuery
    End Function
    Private Function GetQuery(ByVal strItemCode As String, ByVal strGPCode As String) As String
        Dim strItem As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
        End If

        strQuery = "select TSPL_GATEPASS_MASTER_FRESHSALE.GPCode as [Document No],GPDate as [Document Date],'' as Customer_Code,'' as Customer_Name, " & _
            "TSPL_GATEPASS_DETAIL_FRESHSALE.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_GATEPASS_DETAIL_FRESHSALE.Unit_code as Unit,Qty,TSPL_GATEPASS_DETAIL_FRESHSALE.HSN_Code  " & _
            "from TSPL_GATEPASS_MASTER_FRESHSALE left outer join TSPL_GATEPASS_DETAIL_FRESHSALE on TSPL_GATEPASS_MASTER_FRESHSALE.GPCode=TSPL_GATEPASS_DETAIL_FRESHSALE.GPCode  " & _
            "left outer join TSPL_ITEM_MASTER on TSPL_GATEPASS_DETAIL_FRESHSALE.Item_Code=TSPL_ITEM_MASTER.Item_Code   " & _
            "where TSPL_GATEPASS_MASTER_FRESHSALE.GPCode='" & strGPCode & "' "
        Return strQuery
    End Function
    Private Sub funFillGrid()
        Try

            LoadBlankGrid()
            Dim qry As String = LoadQuery("")
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

            Dim obj As New clsGatePassEntry()
            obj = clsGatePassEntry.GetData(strCode, NavTyep, "FS")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GPCode) > 0) Then
                isNewEntry = False
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
                cmbitemtype.Text = obj.Item_Type
                txtTransporter.Text = obj.Transporter
                txtComments.Text = obj.Comments
                txtRemarks.Text = obj.Remarks
                isInsideLoadData = True
                txtDate.Value = obj.GPDate
                txtLocCode.Value = obj.Location_Code
                txtLocDesc.Text = obj.Location_Desc

                funLoadGrid(txtCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtVehicle.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Vehicle No")
        '    txtVehicle.Focus()
        '    Return False
        'End If'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If

        Return funvalidatevehicle()
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


                Dim obj As New clsGatePassEntry()

                obj.GPCode = txtCode.Value
                obj.GPDate = clsCommon.myCDate(txtDate.Value)
                obj.Vehicle_Id = txtVehicle.Value
                obj.Vehicle_Number = lblVehicleDesc.Text
                obj.Item_Type = cmbitemtype.Text
                obj.Transporter = txtTransporter.Text
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComments.Text
                obj.Location_Code = txtLocCode.Value
                obj.Location_Desc = txtLocDesc.Text
                obj.Arr = New List(Of clsGPDetail)
                For Each grow As GridViewRowInfo In Gv1.Rows
                    Dim objTr As New clsGPDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.HSN_Code = clsCommon.myCstr(grow.Cells(colHSNCode).Value)
                    obj.Arr.Add(objTr)

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Document", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, "FS")) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
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
        'txtTransporter.Text = ""
        'txtRemarks.Text = ""
        'txtComments.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        LoadBlankGrid()
        isNewEntry = True
        cmbitemtype.Text = "Select"
        isInsideLoadData = False
    End Sub

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Try
    '        PrintGatePass(txtCode.Value)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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
        strQuery = "select distinct Bill_To_Location as Code,Location_Desc as Description from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code"
        txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "Trans_Type='FS'", txtLocCode.Value, "Code", isButtonClicked)
        txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
    End Sub



    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If isInsideLoadData = False Then

            LoadBlankGrid()
            isNewEntry = True
            isInsideLoadData = False
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

    Private Sub FrmGatePassENtry1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        'txtDate.Value = clsCommon.GETSERVERDATE()
        cmbitemtype.Text = "Select"
        txtTransporter.MaxLength = 100
        txtRemarks.MaxLength = 200
        txtComments.MaxLength = 200
        'btnPost.Enabled = True
        'btnPost.Visible = True
        '===Sanjeet====
        AlternateVechileforGatePass = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAlternateVechileforFreshSale, clsFixedParameterCode.ShowAlternateVechileforFreshSale, Nothing))
        '=========
        funFillGrid()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtVehicle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicle._MYValidating

        strQuery = "select distinct Vehicle_Code as Code,Description from ( " & _
        "select distinct Vehicle_Code,TSPL_VEHICLE_MASTER.Description from TSPL_SD_SHIPMENT_HEAD left outer join " & _
        "TSPL_VEHICLE_MASTER on TSPL_SD_SHIPMENT_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' " & _
        "union all " & _
        "select distinct Vehicle_Code,TSPL_VEHICLE_MASTER.Description from TSPL_SD_SHIPMENT_HEAD left outer join " & _
        "TSPL_VEHICLE_MASTER on TSPL_SD_SHIPMENT_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' and AlternateVehicle <> '' " & _
        "union all " & _
        "select distinct ManualVehicle,'' as Description from TSPL_SD_SHIPMENT_HEAD where TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' and ManualVehicle <> '' ) final"
        txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", txtVehicle.Value, "Code", isButtonClicked)
        lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")


    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_GATEPASS_MASTER_FRESHSALE where GPCode='" + txtCode.Value + "' "
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
        Dim qry As String = " SELECT  GPCode,convert(varchar(10),GPDate,103)  as GPDate,Vehicle_Id,Vehicle_Number FROM  TSPL_GATEPASS_MASTER_FRESHSALE"

        LoadData(clsCommon.ShowSelectForm("GatepassEntry", qry, "GPCode", "", txtCode.Value, "convert(datetime,GPDate,103) desc", isButtonClicked), NavigatorType.Current)

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
        cmbitemtype.Enabled = True
        txtDate.Enabled = True
    End Sub



    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Post Record", Me.Text)
            Exit Sub
        End If
        If myMessages.postConfirm() Then
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_GATEPASS_MASTER_FRESHSALE set post='Y' where gpcode='" & txtCode.Value & "'")
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
    '============================Changes by preeti gupta [09/01/2017]
    Private Function GetAttachQry(ByVal StrCode As String) As String
        Dim Qry As String = " Select Final.* ,tbl_Brand.Brand,tbl_Brand.BRANDDESC FROM ( select TSPL_COMPANY_MASTER.comp_name,TSPL_GATEPASS_DETAIL_FRESHSALE.unit_code,TSPL_GATEPASS_DETAIL_FRESHSALE.qty," & _
           " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end as Comp_Address," & _
          " tspl_location_master.add1 +case when len(tspl_location_master.add2)>0 then ', '+tspl_location_master.add2 else '' end +case when LEN(isnull(tspl_location_master.Add3,''))>0 then ', '+isnull(tspl_location_master.Add3,'') else ' ' end as Loc_add," & _
           " TSPL_GATEPASS_MASTER_FRESHSALE.GPCode,convert(varchar,TSPL_GATEPASS_MASTER_FRESHSALE.GPDate,103) as GPDate,(select top 1 Case When ISNULL(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_HEAD.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' Then (select TSPL_VEHICLE_MASTER.description from TSPL_VEHICLE_MASTER where Vehicle_id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle) Else TSPL_SD_SHIPMENT_HEAD.vehicleNo End as Vehicle from TSPL_SD_SHIPMENT_HEAD where GPCode ='" + StrCode + "') as vehicle_id,(select top 1 Case When ISNULL(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_HEAD.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' Then (select TSPL_VEHICLE_MASTER.description from TSPL_VEHICLE_MASTER where Vehicle_id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle) Else TSPL_SD_SHIPMENT_HEAD.vehicleNo End as Vehicle from TSPL_SD_SHIPMENT_HEAD where GPCode ='" + StrCode + "') as VehicleDesc,TSPL_GATEPASS_MASTER_FRESHSALE.location_code,tspl_location_master.Location_desc,TSPL_GATEPASS_MASTER_FRESHSALE.transporter,TSPL_GATEPASS_MASTER_FRESHSALE.remarks,TSPL_GATEPASS_MASTER_FRESHSALE.comments,TSPL_GATEPASS_MASTER_FRESHSALE.post,TSPL_GATEPASS_DETAIL_FRESHSALE.Item_code,tspl_item_master.item_desc,tspl_item_master.short_description,tspl_item_master.sku_seq,TSPL_TRANSPORT_MASTER.Transporter_Name as TranporterNameFromMaster,TSPL_GATEPASS_DETAIL_FRESHSALE.HSN_Code from TSPL_GATEPASS_DETAIL_FRESHSALE " & _
           " left outer join TSPL_GATEPASS_MASTER_FRESHSALE on TSPL_GATEPASS_MASTER_FRESHSALE.GPCode=TSPL_GATEPASS_DETAIL_FRESHSALE.GPCode " & _
           " left outer join tspl_vehicle_master on tspl_vehicle_master.Vehicle_id=TSPL_GATEPASS_MASTER_FRESHSALE.vehicle_id " & _
            " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_GATEPASS_MASTER_FRESHSALE.location_code " & _
           " left outer join tspl_company_master on tspl_company_master.comp_code=TSPL_GATEPASS_MASTER_FRESHSALE.comp_code " & _
             " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GATEPASS_DETAIL_FRESHSALE.Item_code " & _
            " left outer join  TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id " & _
            "  where 2=2 " & _
            "  and  TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = '" + StrCode + "' " & _
            " ) AS Final left outer join  ( select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND], " & _
            " max([DESCRP]) as [DESCRP],max([PACK]) as [PACK], max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA], " & _
            " max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW], max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC]) " & _
            " as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC], max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC]," & _
            " max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC], max([SCRAPDESC]) as [SCRAPDESC] " & _
            " from ( select * from (   select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   ," & _
            " TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER    left outer join TSPL_ITEM_MASTER_CATEGORY on " & _
            " TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " & _
            " TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values   where 2=2 )xx  Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE], " & _
            " [CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP])  ) Pivt   Pivot  ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC], [BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC]," & _
            " [CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC])  ) Pivt1 ) xxx  group by Item_Code )  as tbl_Brand on tbl_Brand.Item_Code=Final.Item_Code " & _
             " order by Final.sku_seq"

        Return Qry

    End Function
    Public Sub funPrint(ByVal Code As String)
        Try
            atchqry = GetAttachQry(Code)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptGatePassEntry", "GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
        Else
            funPrint(txtCode.Value)
        End If
    End Sub
End Class
