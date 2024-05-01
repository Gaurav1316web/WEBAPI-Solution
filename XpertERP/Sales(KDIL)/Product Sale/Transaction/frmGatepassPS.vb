' '' '' '' ''last modified by 05/07/2013 10:20 pm
''Last Modify by Dipti Waila 16/07/2013 12:10 pm for Select/Unselect Button.
''Last Modify by priti 23/10/2013 02:00 pm  
'''' for bug no BM00000000880
'created by preeti gupta Against ticket no[UDL/08/01/19-000249,UDL/10/01/19-000252,UDL/15/01/19-000255,UDL/28/01/19-000263,UDL/07/02/19-000265,UDL/10/04/19/000287]
Imports common
Imports System.Data.SqlClient

Public Class FrmGatePassPS
    Inherits FrmMainTranScreen
    Dim strQuery As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDate"
    Const colVehicleCode As String = "colVehicleCode"
    Const colVehicleName As String = "colVehicleName"
    Const ColCustCode As String = "ColCustCode"
    Const ColCustName As String = "ColCustName"
    Const colTransporterCode As String = "colTransporterCode"
    Const coltransporterName As String = "coltransporterName"
    Const colGrossWeight As String = "colGrossWeight"
    Const colDocumentAmount As String = "colDocumentAmount"
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False



    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        Dim gvSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvSelect.FormatString = ""
        gvSelect.Name = ColApply
        gvSelect.HeaderText = "Select"
        gvSelect.Width = 50
        gvSelect.ReadOnly = False
        gvSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Gv1.MasterTemplate.Columns.Add(gvSelect)


        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "Invoice no"
        docNo.Name = ColDocNo
        docNo.Width = 100
        docNo.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(docNo)

        Dim Docdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Docdate.FormatString = ""
        Docdate.HeaderText = "Invoice Date"
        Docdate.Name = ColDocDate
        Docdate.Width = 70
        Docdate.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Docdate)

        Dim CustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustCode.FormatString = ""
        CustCode.HeaderText = "Customer Code"
        CustCode.Name = ColCustCode
        CustCode.Width = 100
        CustCode.ReadOnly = True
        CustCode.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(CustCode)

        Dim Custname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Custname.FormatString = ""
        Custname.HeaderText = "Customer Desc"
        Custname.Width = 200
        Custname.Name = ColCustName
        Custname.ReadOnly = True
        Custname.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(Custname)

        Dim TansporterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TansporterCode.FormatString = ""
        TansporterCode.HeaderText = "Transporter Code"
        TansporterCode.Name = colTransporterCode
        TansporterCode.Width = 100
        TansporterCode.ReadOnly = True
        TansporterCode.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(TansporterCode)

        Dim TransporterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TransporterName.FormatString = ""
        TransporterName.HeaderText = "Transporter Name"
        TransporterName.Width = 200
        TransporterName.Name = coltransporterName
        TransporterName.ReadOnly = True
        TransporterName.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(TransporterName)

        Dim VehicleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VehicleCode.FormatString = ""
        VehicleCode.HeaderText = "Vehicle Code"
        VehicleCode.Width = 100
        VehicleCode.Name = colVehicleCode
        VehicleCode.ReadOnly = True
        VehicleCode.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(VehicleCode)


        Dim VehicleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VehicleName.FormatString = ""
        VehicleName.HeaderText = "vehicle Name"
        VehicleName.Name = colVehicleName
        VehicleName.Width = 100
        VehicleName.ReadOnly = True
        VehicleName.IsVisible = True
        VehicleName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(VehicleName)

        
        '===================
        Dim GrossWeight As GridViewDecimalColumn = New GridViewDecimalColumn()
        GrossWeight = New GridViewDecimalColumn()
        GrossWeight.FormatString = ""
        GrossWeight.HeaderText = "Gross Weight"
        GrossWeight.Name = colGrossWeight
        GrossWeight.Width = 80
        GrossWeight.Minimum = 0
        GrossWeight.ShowUpDownButtons = False
        GrossWeight.Step = 0
        GrossWeight.ReadOnly = True
        GrossWeight.IsVisible = True
        GrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(GrossWeight)

        Dim DocumentAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        DocumentAmount = New GridViewDecimalColumn()
        DocumentAmount.FormatString = ""
        DocumentAmount.HeaderText = "Document Amount"
        DocumentAmount.Name = colDocumentAmount
        DocumentAmount.Width = 80
        DocumentAmount.Minimum = 0
        DocumentAmount.ShowUpDownButtons = False
        DocumentAmount.Step = 0
        DocumentAmount.ReadOnly = True
        DocumentAmount.IsVisible = True
        DocumentAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(DocumentAmount)
        '===================

        
       Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub

    Private Sub funFillGrid()
        Try
            
            LoadBlankGrid()
            txtGrossWeight.Value = 0
            Dim strLocationCode As String
            'Dim strVehicleCode As String
            Dim strCityCode As String
            Dim strTransporterCode As String
            Dim strIsOwnVehicle As String

            If clsCommon.myLen(fndLocationCode.Value) > 0 Then
                strLocationCode = " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + fndLocationCode.Value + "' "
            Else
                strLocationCode = ""
            End If

            'If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
            '    strVehicleCode = " and TSPL_SD_SHIPMENT_HEAD.VehicleNo='" + fndVehicleCode.Value + "' "
            'Else
            '    strVehicleCode = ""
            'End If

            If clsCommon.myLen(fndCityCode.Value) > 0 Then
                strCityCode = " and TSPL_CUSTOMER_MASTER.city_code='" + fndCityCode.Value + "' "
            Else
                strCityCode = ""
            End If


            If clsCommon.myLen(fndTransporterCode.Value) > 0 AndAlso chkOwnVehicle.Checked = False Then
                strTransporterCode = " and TSPL_SD_SHIPMENT_HEAD.Transport_Id='" + fndTransporterCode.Value + "' "
            Else
                strTransporterCode = ""
            End If
            If chkOwnVehicle.Checked = True Then
                strIsOwnVehicle = " and is_ownvehicle=1  "
            Else
                strIsOwnVehicle = ""
            End If




            strQuery = "  select TSPL_SD_SALE_INVOICE_HEAD.Document_Code, convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date, " & _
                       " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,  TSPL_SD_SHIPMENT_HEAD.VehicleNo as Vehicle_Code, TSPL_SD_SHIPMENT_HEAD.Transport_Id as Transport_Code, TSPL_SD_SHIPMENT_HEAD.Gross_Item_Wt ," & _
                         "  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt, TSPL_TRANSPORT_MASTER.Transporter_Name, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_VEHICLE_MASTER.Description" & _
                         " from TSPL_SD_SALE_INVOICE_HEAD" & _
                        " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" & _
                        " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SD_SHIPMENT_HEAD.Transport_Id " & _
                         " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                     " LEFT JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.VehicleNo " & _
            " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' " & _
            " and Cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy") + "'" & _
                " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in (select Invoice_No From TSPL_GATEPASS_Detail_ProductSale )  " & strLocationCode & "    " & strTransporterCode & " " & strCityCode & " " & strIsOwnVehicle & ""

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                fndLocationCode.Enabled = False
                'fndVehicleCode.Enabled = False
                fndTransporterCode.Enabled = False
                btnGo.Enabled = False
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = False
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = clsCommon.myCstr(dr("Document_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = clsCommon.myCstr(dr("Document_Date"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustCode).Value = clsCommon.myCstr(dr("Customer_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTransporterCode).Value = clsCommon.myCstr(dr("Transport_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(coltransporterName).Value = clsCommon.myCstr(dr("Transporter_Name"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCode).Value = clsCommon.myCstr(dr("Vehicle_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleName).Value = clsCommon.myCstr(dr("Description"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(dr("Gross_Item_Wt"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDocumentAmount).Value = clsCommon.myCdbl(dr("Total_Amt"))

                    dtpFromDate.Enabled = False
                    dtpToDate.Enabled = False
                Next
            Else
                Throw New Exception("No Data Found to display")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "GatePass Entry", MessageBoxButtons.OK)
        End Try
    End Sub






    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            Addnew()

            Dim obj As New clsGatePassHeadPS()
            obj = clsGatePassHeadPS.GetData(strCode, NavTyep, "PS")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GPCode) > 0) Then
                isNewEntry = False
                fndLocationCode.Enabled = False
                'fndVehicleCode.Enabled = False
                fndTransporterCode.Enabled = False
                btnGo.Enabled = False
                btnSave.Text = "Update"
                txtCode.Value = obj.GPCode
                txtDate.Value = obj.GPDate
                dtpFromDate.Value = obj.From_Date
                dtpToDate.Value = obj.To_Date
                fndLocationCode.Value = obj.Location_Code
                txtLocationName.Text = obj.Location_Desc
                fndVehicleCode.Value = obj.Vehicle_Id
                txtVehicleName.Text = obj.Vehicle_Number
                fndTransporterCode.Value = obj.Transporter_code
                txtTransporterName.Text = obj.Transporter_Name
                txtComments.Text = obj.Comments
                txtRemarks.Text = obj.Remarks
                txtGrossWeight.Value = obj.Gross_Weight
                fndCityCode.Value = obj.City_Code
                txtCityName.Text = obj.City_Desc
                txtVehicleCapacity.Value = obj.Capacity
                txtProvisionDocNo.Text = obj.Provision_No
                NumProvisionAmount.Value = obj.Provision_Amt
                txtManualVehicleNo.Text = obj.Man_Vehicle_Number
                chkOwnVehicle.Checked = IIf(obj.Is_Own_Vehicle, True, False)

                If obj.Post = "Y" Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If

                isInsideLoadData = True

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsGatePassDetailPS In obj.Arr
                        Gv1.Rows.AddNew()
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = objTr.Invoice_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = objTr.Invoice_date
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustCode).Value = objTr.cust_code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColCustName).Value = objTr.cust_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTransporterCode).Value = objTr.Transporter_code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(coltransporterName).Value = objTr.Transporter_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCode).Value = objTr.Vehicle_Id
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleName).Value = objTr.Vehicle_Number
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGrossWeight).Value = objTr.Gross_Weight
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDocumentAmount).Value = objTr.Documnet_Amount

                    Next
                    isInsideLoadData = True
                End If
            End If
            EnableDisableVehicleonmanualVehicle()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(fndLocationCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location")
            fndLocationCode.Focus()
            Return False
            'ElseIf clsCommon.myLen(fndVehicleCode.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Vehicle No")
            '    fndVehicleCode.Focus()
            '    Return False
        ElseIf clsCommon.myLen(fndCityCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select City")
            fndCityCode.Focus()
            Return False
        ElseIf clsCommon.myLen(fndTransporterCode.Value) <= 0 AndAlso chkOwnVehicle.Checked = False Then
            common.clsCommon.MyMessageBoxShow("Please select Transporter")
            fndTransporterCode.Focus()
            Return False
        End If
        Dim qry As String = ""
        Dim dt As DataTable
        '===========Added by preeti Gupta==============
        If chkOwnVehicle.Checked = False Then

            qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + fndLocationCode.Value + "' and city_code='" + fndCityCode.Value + "' and transport_id='" + fndTransporterCode.Value + "' and TransType='P' and type='KG' and capacitymt='" + clsCommon.myCstr(txtVehicleCapacity.Value) + "' order by effective_date desc"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Else
                qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + fndLocationCode.Value + "' and city_code='" + fndCityCode.Value + "' and capacitymt='" + clsCommon.myCstr(txtVehicleCapacity.Value) + "' and transport_id='" + fndTransporterCode.Value + "' and TransType='P' and type='MT' order by effective_date desc"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Else
                    common.clsCommon.MyMessageBoxShow("No mapping exist in Route freight detail")
                    Return False
                End If
            End If
        End If
        '================================================
        'If clsCommon.myCdbl(txtVehicleCapacity.Value) > 0 Then
        '    qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + fndLocationCode.Value + "' and city_code='" + fndCityCode.Value + "' and capacitymt='" + clsCommon.myCstr(txtVehicleCapacity.Value) + "' and transport_id='" + fndTransporterCode.Value + "' and TransType='P' and type='MT' order by effective_date desc"
        'Else
        '    qry = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + fndLocationCode.Value + "' and city_code='" + fndCityCode.Value + "' and transport_id='" + fndTransporterCode.Value + "' and TransType='P' and type='KG' order by effective_date desc"
        'End If
        'dt = clsDBFuncationality.GetDataTable(qry)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        'Else
        '    common.clsCommon.MyMessageBoxShow("No mapping exist in Route freight detail")
        '    Return False
        'End If
        Return True
    End Function

    Private Function funvalidatevehicle() As Boolean
        'Dim count As Decimal = 0
        'Dim segno As String = ""
        'Dim strvehiclenum As String = lblVehicleDesc.Text
        'Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(fndVehicleCode.Value) + "' "
        'If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
        '    sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + fndVehicleCode.Value + "'"
        '    lblVehicleDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
        '    Return True
        'Else
        '    Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
        '    strmessage += "Do you want to continue "



        '    If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

        '        If clsCommon.myLen(lblVehicleDesc.Text) <= 0 Then
        '            lblVehicleDesc.Focus()
        '            Throw New Exception("Please Enter Vehicle No")
        '        End If


        '        fndVehicleCode.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")))
        '        'strvehiclenum = txtVehicle.Text
        '        sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
        '        segno = CStr(connectSql.RunScalar(sql))
        '        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        '        Try
        '            connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", fndVehicleCode.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
        '            connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", fndVehicleCode.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

        '            trans.Commit()
        '        Catch ex As Exception
        '            fndVehicleCode.Value = ""
        '            trans.Rollback()
        '            Throw New Exception(ex.Message)
        '        End Try

        '        'lblVehicleDesc.Text = txtVehicle.Text + "-Hired"
        '        fndVehicleCode.Text = fndVehicleCode.Value
        '        Return True
        '    Else
        '        fndVehicleCode.Value = String.Empty
        '        fndVehicleCode.Text = fndVehicleCode.Value
        '        Return False
        '    End If
        'End If
    End Function
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            If (AllowToSave()) Then


                Dim obj As New clsGatePassHeadPS()

                obj.GPCode = txtCode.Value
                obj.GPDate = clsCommon.myCDate(txtDate.Value)
                obj.From_Date = clsCommon.myCDate(dtpFromDate.Value)
                obj.To_Date = clsCommon.myCDate(dtpToDate.Value)
                obj.Location_Code = fndLocationCode.Value
                obj.City_Code = fndCityCode.Value
                obj.Vehicle_Id = fndVehicleCode.Value
                obj.Transporter_code = fndTransporterCode.Value
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComments.Text
                obj.Gross_Weight = 0
                obj.Capacity = txtVehicleCapacity.Value
                obj.Man_Vehicle_Number = txtManualVehicleNo.Text
                obj.Is_Own_Vehicle = clsCommon.myCdbl(IIf(chkOwnVehicle.Checked, 1, 0))
                obj.Arr = New List(Of clsGatePassDetailPS)
                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myCBool(grow.Cells(ColApply).Value) Then
                        Dim objTr As New clsGatePassDetailPS()
                        objTr.Invoice_No = clsCommon.myCstr(grow.Cells(ColDocNo).Value)
                        objTr.Invoice_date = clsCommon.myCDate(grow.Cells(ColDocDate).Value)
                        objTr.cust_code = clsCommon.myCstr(grow.Cells(ColCustCode).Value)
                        objTr.Transporter_code = clsCommon.myCstr(grow.Cells(colTransporterCode).Value)
                        objTr.Vehicle_Id = clsCommon.myCstr(grow.Cells(colVehicleCode).Value)
                        objTr.Gross_Weight = clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                        objTr.Documnet_Amount = clsCommon.myCdbl(grow.Cells(colDocumentAmount).Value)
                        obj.Gross_Weight += clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Document")
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, "PS")) Then
                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If

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
        dtpFromDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        fndVehicleCode.Value = ""
        txtVehicleName.Text = ""
        fndTransporterCode.Value = ""
        txtTransporterName.Text = ""
        fndLocationCode.Value = ""
        txtLocationName.Text = ""
        txtRemarks.Text = ""
        txtComments.Text = ""
        txtGrossWeight.Value = 0
        fndCityCode.Value = ""
        txtCityName.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True
        fndLocationCode.Enabled = True
        fndVehicleCode.Enabled = True
        fndTransporterCode.Enabled = True
        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        btnGo.Enabled = True
        btnSave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        txtVehicleCapacity.Value = 0
        txtProvisionDocNo.Text = ""
        NumProvisionAmount.Value = 0
        txtManualVehicleNo.Text = ""
        chkOwnVehicle.Checked = False
        fndTransporterCode.Enabled = True
        txtTransporterName.Enabled = True
        'EnableDisableVehicleOrManVehicleNo()
        'EnableDisableVehicleOrManVehicleNo
        isNewEntry = True

        isInsideLoadData = False
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsGatePassHeadPS.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub PrintGatePass(ByVal GPNo As String, ByVal PrePrinted As String)
        '    Dim qry, qry1, qry2 As String
        '    Dim blnSubqry As Boolean = False
        '    Try
        '        Dim strItemType, strInvoice, strTransfer, struni, subQry, strGlass As String
        '        struni = ""
        '        If cmbitemtype.Text = "Empty" Then
        '            strItemType = "EB"
        '        Else
        '            strItemType = "FB"
        '        End If

        '        If clsCommon.myLen(GPNo) > 0 Then
        '            qry1 = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as Time, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name, XXXX.*, "
        '            qry1 += " (Select Class_Desc from TSPL_ITEM_DETAILS Where Class_Name ='Size' AND Item_Code=XXXX.Item_Code) as [Pack], "
        '            qry1 += " (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress,'" & cmbitemtype.Text & "' as Type,emptytype,MRP "
        '            qry1 += " from ( "

        '            qry1 += " Select Cust_Name,isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as DocNo, MAX( TSPL_GATEPASS_MASTER.Created_By) as Created_By ,max(TSPL_GATEPASS_MASTER.Modified_By) as Modified_By , max(Ship_To) as Ship_To,max(Ship_To_Desc) as Ship_To_Desc,max(ShipToCity) as ShipToCity,max(ShipToPin) as ShipToPin, " & _
        '            "case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then MAX(ShipToadd) else 'NA' end as DeliveryTo , " & _
        '            "max(XXX.Salescode) as Salescode,case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then  max(XXX.Salesname) else case when max(xxx.Trans_Type)='Route' then max(ToLoc_Desc) else 'NA' end  end as DriverName, " & _
        '            "case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then MAX(Cust_Name) else " & _
        '            "case when max(TSPL_GATEPASS_MASTER.Item_Type)='Full' then case when max(xxx.Trans_Type)='Excise' then " & _
        '            "case when  max(TSPL_GATEPASS_DETAIL.Route_No) <> '' then max(TSPL_GATEPASS_DETAIL.Route_Desc)  else max(ToLoc_Desc) end " & _
        '            "when max(xxx.Trans_Type)='Route' and max(XXX.Location_Type)='logical' and max(XXX.Reference_Doc_No)='' then max(TSPL_GATEPASS_DETAIL.Route_Desc) " & _
        '            "when max(XXX.Trans_Type)='Depot' then max(ToLoc_Desc) " & _
        '            "else max(ToLoc_Desc) end End end  as PartyName, " & _
        '            "MAX(TSPL_GATEPASS_MASTER.Transporter) as TransporterName, " & _
        '            "MAX(TSPL_GATEPASS_MASTER.Remarks) as Remarks, MAX(TSPL_GATEPASS_MASTER.Comments) as Comments, " & _
        '            "case when ((Select min(Route_No) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "'  Group By GPCode)) = (Select max(Route_No) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "'  ) then " & _
        '            "MAX(TSPL_GATEPASS_DETAIL.Route_Desc) else '' end as Routeheader , " & _
        '            "'' as PriceCode, " & _
        '            "'' as PriceDesc, " & _
        '            " max(From_Location) as FromLoc," & _
        '            " max(FromLoc_Desc) as FromLocDesc, " & _
        '            " max(To_Location) as ToLoc, " & _
        '            " max(ToLoc_Desc) as ToLocDesc, " & _
        '            " Case When (Select COUNT(*) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "' and   TSPL_GATEPASS_DETAIL.Type = 'LO') = 0 Then max(Cust_Name) Else " & _
        '            "(Select top 1 ToSalesmanname from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "' and " & _
        '            " TSPL_GATEPASS_DETAIL.Type = 'LO')  END as custname,max(Transporter_Name) as Transport_Id,TSPL_GATEPASS_MASTER.GPCode, " & _
        '            " CONVERT(date,MAX(GPDate),103) as GPDate,CONVERT(time,MAX(GPDate),103) as GPTime, MAX(TSPL_GATEPASS_MASTER.Vehicle_Number) as VehicleNo, "
        '            qry1 += " XXX.Item_Code, MAX(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc, Sum(XXX.Qty) as Qty, MAX(TSPL_GATEPASS_DETAIL.Route_No) as Route_No, "
        '            qry1 += " MAX(TSPL_GATEPASS_DETAIL.Route_Desc) as Route_Desc, MAX(TSPL_GATEPASS_DETAIL.ToSalesmanCode) as ToSalesmanCode, "
        '            qry1 += " MAX(TSPL_GATEPASS_DETAIL.ToSalesmanname) as ToSalesmanname, MAX(TSPL_GATEPASS_MASTER.Comp_Code) as CompCode, "
        '            qry1 += " Case When (Select COUNT(*) from TSPL_GATEPASS_DETAIL Where TSPL_GATEPASS_DETAIL.GPCode = TSPL_GATEPASS_MASTER.GPCode)=1 Then  " & _
        '            "MAX(xxx.DocNo) Else '' END as [InvoiceNo] ,XXX.emptytype,XXX.MRP, " & _
        '            "Sum(XXX.Glass) as Glass,Sum(XXX.Cartons) as Cartons"
        '            qry1 += " from ("

        '            strInvoice = " Select Ship_To,TSPL_SHIP_TO_LOCATION.Ship_To_Desc,(TSPL_SHIP_TO_LOCATION.Add1+ TSPL_SHIP_TO_LOCATION.Add2 + Add3 + add4) as ShipToadd, " & _
        '            "TSPL_SHIP_TO_LOCATION.City_Code as ShipToCity,TSPL_SHIP_TO_LOCATION.Pin_Code as ShipToPin ,'' as Salescode,Emp_Name as Salesname,'' as Reference_Doc_No,'' as Location_Type,'' as Trans_Type,'' as From_Location,'' as FromLoc_Desc,'' as To_Location,'' as ToLoc_Desc,Cust_Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, TSPL_SALE_INVOICE_DETAIL.Item_Code, Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty, " & _
        '            "1 as [InvCount],case when Two_Count_Status='Y' then 'RGB' else 'Others' end as emptytype, " & _
        '            "case when TSPL_SALE_INVOICE_DETAIL.Unit_code='" & strItemType & "' then MRP_Amt else MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor end as MRP, " & _
        '            "case when Two_Count_Status='Y' then Invoice_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Glass,case when Two_Count_Status='N' then Invoice_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Cartons  from "
        '            strInvoice += " TSPL_SALE_INVOICE_DETAIL  Left Outer Join TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join "
        '            strInvoice += " TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on "
        '            strInvoice += " TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
        '            "left outer join TSPL_ITEM_UOM_DETAIL  as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
        '                            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SALE_INVOICE_HEAD.Ship_To=TSPL_SHIP_TO_LOCATION.Ship_To_Code "
        '            strInvoice += " Where  Shipment_Type='Sale'  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='" & strItemType & "'  "
        '            struni += " UNION all "
        '            strTransfer = " Select '' as Ship_To,'' as Ship_To_Desc,'' as ShipToadd,City_Code as ShipToCity,'' as ShipToPin  ,Salesmancode as Salescode,Emp_Name as Salesname,Reference_Doc_No,TSPL_TRANSFER_HEAD.Location_Type,Trans_Type,From_Location,FromLoc_Desc, " & _
        '            "case when GIT_Type='Y' then (select top 1 Location_Code from TSPL_LOCATION_MASTER where GIT_Location=To_Location) else To_Location end as To_Location, " & _
        '            "case when GIT_Type='Y' then (select top 1 Location_Desc from TSPL_LOCATION_MASTER where GIT_Location=To_Location) else ToLoc_Desc end as ToLoc_Desc, " & _
        '            "'' as Cust_Name,TSPL_TRANSFER_HEAD.Transfer_No As DocNo, TSPL_TRANSFER_DETAIL.Item_Code, Item_Qty as Qty, 1 as [InvCount], "
        '            strTransfer += " case when Two_Count_Status='Y' then 'RGB' else 'Others' end as emptytype,case when TSPL_TRANSFER_DETAIL.Uom='" & strItemType & "' then MRP "
        '            strTransfer += " else MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor end as MRP,  " & _
        '            " case when Two_Count_Status='Y' then Item_Qty else 0 end as Glass,case when Two_Count_Status='N' then Item_Qty else 0 end as Cartons "
        '            strTransfer += " from TSPL_TRANSFER_DETAIL  LEFT OUTER JOIN TSPL_TRANSFER_HEAD On TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No "
        '            strTransfer += "  left outer join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on"
        '            strTransfer += " TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        '            "left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer join  " & _
        '            "TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.Salesmancode=TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
        '            "left outer join TSPL_ITEM_UOM_DETAIL  as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code "
        '            strTransfer += "Where Transfer_Type='LO' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='" & strItemType & "'  "

        '            qry2 = " ) XXX LEFT OUTER JOIN TSPL_GATEPASS_DETAIL ON TSPL_GATEPASS_DETAIL.DocNo=XXX.DocNo "
        '            qry2 += " LEFT OUTER JOIN TSPL_GATEPASS_MASTER ON TSPL_GATEPASS_MASTER.GPCode=TSPL_GATEPASS_DETAIL.GPCode"
        '            qry2 += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=XXX.Item_Code " & _
        '            "left outer join TSPL_VEHICLE_MASTER on TSPL_GATEPASS_MASTER.Vehicle_Id=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
        '            "left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  " & _
        '            "CROSS APPLY  (SELECT DocNo + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL  where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and ToSalesmanname=Cust_Name  FOR XML PATH(''))el(files)  "
        '            qry2 += " WHERE TSPL_GATEPASS_MASTER.GPCode='" + txtCode.Value + "' Group By TSPL_GATEPASS_MASTER.GPCode, xxx.Item_Code ,xxx.MRP ,xxx.emptytype,el.files  ,Cust_Name  "

        '            qry2 += " ) XXXX LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=XXXX.CompCode"
        '        Else
        '            Throw New Exception("Gate Pass No. not found to print.")
        '        End If

        '        qry = qry1 & strInvoice & struni & strTransfer & qry2
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '        Dim frmCRV As New frmCrystalReportViewer()
        '        If PrePrinted Then
        '            frmCRV.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "crptGatePassPreprinted", "GatePass Report", True)
        '        Else
        '            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
        '                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.NA, "crptGatePassVizag", "GatePass Report", False)

        '            Else
        '                strGlass = " and Two_Count_Status='Y' "
        '                subQry = "select Cust_Name,(isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile') ) as DocNo, " & _
        '                "(isnull(LEFT(ey.files ,LEN(ey.files )-1),'NoFile') ) as Qty,max(TSPL_GATEPASS_DETAIL.GPCode) as GPCode from (" & strInvoice & strGlass & struni & strTransfer & strGlass & " ) XXX  " & _
        '                "LEFT OUTER JOIN TSPL_GATEPASS_DETAIL ON TSPL_GATEPASS_DETAIL.DocNo=XXX.DocNo  LEFT OUTER JOIN " & _
        '                "TSPL_GATEPASS_MASTER ON TSPL_GATEPASS_MASTER.GPCode=TSPL_GATEPASS_DETAIL.GPCode " & _
        '                "CROSS APPLY  (SELECT distinct DocNo + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL left outer join  " & _
        '                "TSPL_SALE_INVOICE_DETAIL on TSPL_GATEPASS_DETAIL.DocNo=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join " & _
        '                "TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        '                "where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and Two_Count_Status='Y' and ToSalesmanname=Cust_Name  FOR XML PATH(''))el(files)  " & _
        '                "CROSS APPLY  (SELECT  convert(varchar(30), sum(convert(decimal(18,2),Invoice_Qty/Conversion_Factor))) + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL left outer join " & _
        '                "TSPL_SALE_INVOICE_DETAIL on TSPL_GATEPASS_DETAIL.DocNo=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join " & _
        '                "TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
        '                "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        '                "where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and Two_Count_Status='Y' and ToSalesmanname=Cust_Name  " & _
        '                "group by ToSalesmanname,DocNo  FOR XML PATH(''))ey(files)  " & _
        '                "WHERE TSPL_GATEPASS_MASTER.GPCode='" + txtCode.Value + "' Group By TSPL_GATEPASS_MASTER.GPCode,xxx.emptytype,el.files  ,Cust_Name,ey.files  "
        '                frmCRV.funsubreport(CrystalReportFolder.SalesReport, qry, subQry, "", "", "", "crptGatePassGuntur", "GatePass", "crptGatePassSubReport1.rpt")
        '            End If
        '        End If
        '        frmCRV = Nothing
        '        'FrmSalerReport.funreport(dt, "crptGatePass", "Gate Pass")
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
    End Sub



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





    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If isInsideLoadData = False Then

            LoadBlankGrid()
            isNewEntry = True
            isInsideLoadData = False
            If (AllowToSave()) Then
                funFillGrid()
            End If


        End If
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.FrmGatePassPS)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag


    End Sub

    Private Sub FrmGatePassPS_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then

            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnDeleteInvoiceafterPost.Visible = True
            End If
        End If
    End Sub

    Private Sub FrmGatePassENtry1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        Addnew()
        btnPost.Enabled = True
        btnPost.Visible = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from tspl_gatepass_master where GPCode='" + txtCode.Value + "' "
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
        Dim qry As String = " select GPCode ,convert(varchar,GPDate,103) as GPDate ,Location_Code ,Transporter_code ,Total_Gross_Weight  from TSPL_GATEPASS_master_ProductSale"

        LoadData(clsCommon.ShowSelectForm("GatepassEntry", qry, "GPCode", "", txtCode.Value, "GPCode", isButtonClicked), NavigatorType.Current)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Addnew()

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PrintGatePass(txtCode.Value, True)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PrintGatePass(txtCode.Value, False)
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsGatePassHeadPS.PostData(txtCode.Value)) Then
                    msg = "Successfully Posted"

                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
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

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        strQuery = "select TSPL_LOCATION_MASTER.location_code as Code,TSPL_LOCATION_MASTER.location_desc as Name from TSPL_LOCATION_MASTER "
        fndLocationCode.Value = clsCommon.ShowSelectForm("LocationGP", strQuery, "Code", "", fndLocationCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            txtLocationName.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            txtLocationName.Text = ""
        End If

    End Sub

    Private Sub fndCityCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCityCode._MYValidating
        strQuery = "select TSPL_CITY_MASTER.City_Code as code ,TSPL_CITY_MASTER.City_Name as Name  from TSPL_CITY_MASTER "
        fndCityCode.Value = clsCommon.ShowSelectForm("City", strQuery, "Code", "", fndCityCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndCityCode.Value) > 0 Then
            txtCityName.Text = clsDBFuncationality.getSingleValue("select TSPL_CITY_MASTER.City_Name  from TSPL_CITY_MASTER  where City_Code='" & fndCityCode.Value & "'")
        Else
            txtCityName.Text = ""
        End If
    End Sub

    Private Sub fndVehicleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVehicleCode._MYValidating
        strQuery = "select Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
        fndVehicleCode.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", fndVehicleCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
            txtVehicleName.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & fndVehicleCode.Value & "'")
        Else
            txtVehicleName.Text = ""
        End If
        EnableDisableVehicle()
    End Sub

    Private Sub fndTransporterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTransporterCode._MYValidating
        Dim qry As String = "select Transport_Id,Transporter_Name from TSPL_TRANSPORT_MASTER"
        fndTransporterCode.Value = clsCommon.ShowSelectForm("Transport No", qry, "Transport_Id", "", fndTransporterCode.Value, "Transport_Id", isButtonClicked)
        If clsCommon.myLen(fndTransporterCode.Value) > 0 Then
            txtTransporterName.Text = clsDBFuncationality.getSingleValue("Select Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id = '" + Convert.ToString(fndTransporterCode.Value) + "'")
        Else
            txtTransporterName.Text = ""
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub print(ByVal GPCode As String)
        Dim qry As String = "select TSPL_GATEPASS_Master_ProductSale.GPCode ,convert(varchar,TSPL_GATEPASS_Master_ProductSale.GPDate,103) as GPDate," & _
                            " TSPL_GATEPASS_Master_ProductSale.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2," & _
                             " TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.City_Code as Loc_City_code,Loc_City.City_Name as Loc_City_Name,TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No," & _
                                " TSPL_LOCATION_MASTER.State as Loc_State_code,Loc_State.STATE_NAME as Loc_State_Name,Loc_State.GST_STATE_Code as Loc_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GST_No," & _
                             " TSPL_GATEPASS_Master_ProductSale.Transporter_code ,TSPL_TRANSPORT_MASTER.Transporter_Name ,TSPL_GATEPASS_Master_ProductSale.City_code  as Trans_City_code," & _
                              " Trans_City.City_Name as Trans_City_Name ,TSPL_GATEPASS_Master_ProductSale.Vehicle_Id ,TSPL_GATEPASS_Master_ProductSale.Man_Vehicle_Number,TSPL_GATEPASS_Master_ProductSale.post,TSPL_VEHICLE_MASTER.Description as Vehicle_Name," & _
                            " TSPL_GATEPASS_Master_ProductSale.Vehicle_Capacity ,TSPL_GATEPASS_DETAIL_PRODUCTSALE.Invoice_No ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.document_date,103) as Invoice_Date" & _
                            " ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.customer_name,TSPL_ITEM_MASTER.HSN_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt " & _
                            ",TSPL_GATEPASS_Master_ProductSale.Created_By ,TSPL_GATEPASS_Master_ProductSale.Modified_By ,convert(varchar,TSPL_GATEPASS_Master_ProductSale.Modified_Date,103) as Modified_Date ,convert(varchar,TSPL_GATEPASS_Master_ProductSale.Created_Date,103) as Created_Date " & _
                            ",Created_User.user_Name as CreatedUserName,Modify_User.user_Name as Modify_User_Name,TSPL_GATEPASS_Master_ProductSale.total_gross_weight" & _
                            " from TSPL_GATEPASS_Master_ProductSale" & _
                            " left join TSPL_GATEPASS_DETAIL_PRODUCTSALE on TSPL_GATEPASS_DETAIL_PRODUCTSALE.GPCode =TSPL_GATEPASS_Master_ProductSale.GPCode " & _
                            " left join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_GATEPASS_DETAIL_PRODUCTSALE.Invoice_No " & _
                            " left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=TSPL_SD_SALE_INVOICE_HEAD.document_code" & _
                            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_GATEPASS_Master_ProductSale.Location_Code " & _
                            " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_GATEPASS_Master_ProductSale.comp_code" & _
                            " left join TSPL_CITY_MASTER as Loc_City on Loc_City.City_Code =TSPL_LOCATION_MASTER.City_Code" & _
                            " left join TSPL_STATE_MASTER as Loc_State on Loc_State.STATE_CODE =TSPL_LOCATION_MASTER.State " & _
                            " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_GATEPASS_Master_ProductSale.Transporter_code " & _
                            " left join TSPL_CITY_MASTER as Trans_City on Trans_City.City_Code =TSPL_GATEPASS_Master_ProductSale.City_code " & _
                            " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.vehicle_id=TSPL_GATEPASS_Master_ProductSale.Vehicle_Id " & _
                            " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code" & _
                            " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" & _
                            " left join TSPL_USER_MASTER as Created_User on Created_User.user_code=TSPL_GATEPASS_Master_ProductSale.created_by" & _
                            " left join TSPL_USER_MASTER as Modify_User on Modify_User.user_code=TSPL_GATEPASS_Master_ProductSale.Modified_By" & _
                            " where TSPL_GATEPASS_Master_ProductSale.GPCode ='" & GPCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        If dt.Rows.Count > 0 Then
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassReturnProductSale", "Gate Pass Return", clsCommon.myCDate(dt.Rows(0)("GPDate")), "rptCompanyAddress.rpt")
        End If


    End Sub
    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "GP not found to Print", Me.Text)
        Else
            print(txtCode.Value)
        End If
    End Sub
    Private Sub ReverseAndUnPost()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'If (deleteConfirm()) Then
            '    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_GATEPASS_MASTER_PRODUCTSALE where GPCode ='" & txtCode.Value & "' ", trans) > 0 Then
            '        If (clsGatePassHeadPS.SaveDataForHistory(txtCode.Value, txtProvisionDocNo.Text, trans)) Then
            '            common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
            '            trans.Commit()
            '            LoadData(txtCode.Value, NavigatorType.Current)
            '        End If
            '    Else
            '        Throw New Exception("Document " & txtCode.Value & " can't be deleted,")
            '    End If
            'End If
            If common.clsCommon.MyMessageBoxShow("Do you want to Reverse ?", "Gate-Pass", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            Else
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_GATEPASS_MASTER_PRODUCTSALE where GPCode ='" & txtCode.Value & "' ", trans) > 0 Then
                    If (clsGatePassHeadPS.SaveDataForHistory(txtCode.Value, txtProvisionDocNo.Text, trans)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                        trans.Commit()
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                Else
                    Throw New Exception("Document " & txtCode.Value & " can't be deleted,")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '=added by preeti gupta Against ticket no[UDL/21/01/19-000259]
    Private Sub btnDeleteInvoiceafterPost_Click(sender As Object, e As EventArgs) Handles btnDeleteInvoiceafterPost.Click
        ReverseAndUnPost()
    End Sub
    '=====done by preeti gupta Against ticket no [UDL/05/04/19-000286]
    Sub EnableDisableVehicleonmanualVehicle()
        If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
            txtManualVehicleNo.Enabled = False
            fndVehicleCode.Enabled = True
            txtVehicleName.Enabled = True
        Else
            txtManualVehicleNo.Enabled = True
            fndVehicleCode.Enabled = False
            txtVehicleName.Enabled = False
        End If
    End Sub

    Sub EnableDisableVehicle()
        If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
            txtManualVehicleNo.Enabled = False
            fndVehicleCode.Enabled = True
            txtVehicleName.Enabled = True
        Else
            txtManualVehicleNo.Enabled = True
            fndVehicleCode.Enabled = True
            txtVehicleName.Enabled = True
        End If

    End Sub
    Sub EnableDisablerManVehicleNo()
        If clsCommon.myLen(txtManualVehicleNo.Text) > 0 Then
            txtManualVehicleNo.Enabled = True
            fndVehicleCode.Enabled = False
            txtVehicleName.Enabled = False

        Else
            txtManualVehicleNo.Enabled = True
            fndVehicleCode.Enabled = True
            txtVehicleName.Enabled = True
        End If

    End Sub
    Private Sub txtManualVehicleNo_TextChanged(sender As Object, e As EventArgs) Handles txtManualVehicleNo.TextChanged
        EnableDisablerManVehicleNo()
    End Sub

    Private Sub chkOwnVehicle_CheckedChanged(sender As Object, e As EventArgs) Handles chkOwnVehicle.CheckedChanged
        If chkOwnVehicle.Checked = True Then
            fndTransporterCode.Enabled = False
            txtTransporterName.Enabled = False
        Else
            fndTransporterCode.Enabled = True
            txtTransporterName.Enabled = True
        End If
    End Sub
End Class
