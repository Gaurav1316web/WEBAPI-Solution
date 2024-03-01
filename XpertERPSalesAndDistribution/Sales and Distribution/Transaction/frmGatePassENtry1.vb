' '' '' '' ''last modified by 05/07/2013 10:20 pm
''Last Modify by Dipti Waila 16/07/2013 12:10 pm for Select/Unselect Button.
''Last Modify by priti 23/10/2013 02:00 pm  
'''' for bug no BM00000000880
Imports XpertERPEngine
Imports common
Imports System.Data.SqlClient

Public Class FrmGatePassENtry1
    Inherits FrmMainTranScreen
    Dim strQuery As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDate"
    Const ColToSalesmanCode As String = "ColToSalesmanCode"
    Const ColToSalesmanname As String = "ColToSalesmanname"
    Const ColRoute_No As String = "ColRoute_No"
    Const ColRoute_Desc As String = "ColRoute_Desc"
    Const ColType As String = "ColType"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColPriceDesc As String = "ColPriceDesc"

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
        docNo.HeaderText = "Document no"
        docNo.Name = ColDocNo
        docNo.Width = 100
        docNo.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(docNo)

        Dim Docdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Docdate.FormatString = ""
        Docdate.HeaderText = "Doc Date"
        Docdate.Name = ColDocDate
        Docdate.Width = 70
        Docdate.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Docdate)

        Dim Tocod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Tocod.FormatString = ""
        Tocod.HeaderText = "To Code"
        Tocod.Name = ColToSalesmanCode
        Tocod.Width = 100
        Tocod.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Tocod)

        Dim Toname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Toname.FormatString = ""
        Toname.HeaderText = "To Desc"
        Toname.Width = 200
        Toname.Name = ColToSalesmanname
        Toname.ReadOnly = True
        Toname.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(Toname)

        Dim Routeno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Routeno.FormatString = ""
        Routeno.HeaderText = "Route No"
        Routeno.Width = 100
        Routeno.Name = ColRoute_No
        Routeno.ReadOnly = True
        Routeno.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(Routeno)


        Dim RouteDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RouteDesc.FormatString = ""
        RouteDesc.HeaderText = "Route Desc"
        RouteDesc.Name = ColRoute_Desc
        RouteDesc.Width = 100
        RouteDesc.ReadOnly = True
        RouteDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(RouteDesc)

        Dim PriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PriceCode.FormatString = ""
        PriceCode.HeaderText = "Price Code"
        PriceCode.Name = ColPriceCode
        PriceCode.Width = 100
        PriceCode.ReadOnly = True
        PriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(PriceCode)

        Dim PriceDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PriceDesc.FormatString = ""
        PriceDesc.HeaderText = "Price Desc"
        PriceDesc.Name = ColPriceDesc
        PriceDesc.Width = 100
        PriceDesc.ReadOnly = True
        PriceDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(PriceDesc)

        Dim Type As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Type.FormatString = ""
        Type.HeaderText = "Type"
        Type.Name = ColType
        Type.Width = 100
        Type.ReadOnly = True
        'Type.IsVisible = False
        Type.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(Type)

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
            If clsCommon.CompairString(cmbitemtype.Text, "Select") = CompairStringResult.Equal Then
            Else

                LoadBlankGrid()
                'Dim Whrcls As String
                'If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                '    Whrcls = ""
                'Else
                '    Whrcls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
                'End If
                ' Dim strItemType As String
                Dim strTransferLoc As String
                Dim strSalelOc As String

                If clsCommon.myLen(txtLocCode.Value) > 0 Then
                    strTransferLoc = " and  from_location in (select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" + txtLocCode.Value + "' and Location_Type='Physical')"
                    strSalelOc = " and Location in (select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" + txtLocCode.Value + "' and Location_Type='Physical')"
                Else
                    strTransferLoc = ""
                    strSalelOc = ""
                End If
                strQuery = "select Transfer_No as DOCNo,Transfer_Date as Docdate,To_Location as Tocode ,ToLoc_Desc as ToDesc,Route_No,Route_Desc, " & _
                "0  as Status,'LO' as Type,Price_code,Price_Desc from TSPL_TRANSFER_HEAD where not( Location_Type ='logical' and Reference_Doc_No <> '') and  Transfer_Type='LO' and " & _
                "Transfer_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND  " & _
                "Item_Type='" & cmbitemtype.Text & "' And " & _
                "Transfer_No not in (select DOCno From tspl_gatepass_detail ) " & strTransferLoc & " " & _
                "union all " & _
                "select Sale_Invoice_No as DOCNo,Sale_Invoice_Date as Docdate,Cust_Code as Tocode, " & _
                "Cust_Name as Todesc,Route_No,Route_Desc,0 as Status,'Sale' as Type,TSPL_SALE_INVOICE_HEAD.Price_Code, " & _
                "(select distinct Price_Code_Desc from  TSPL_PRICE_COMPONENT_MAPPING  where price_code=TSPL_SALE_INVOICE_HEAD.price_code) as Price_Desc from TSPL_SALE_INVOICE_HEAD " & _
                " where Shipment_Type='sale' and  " & _
                "Sale_Invoice_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and " & _
                "Sale_Invoice_No not in (select DOCno From tspl_gatepass_detail ) " & strSalelOc & " "


                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strQuery)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        Gv1.Rows.AddNew()
                        If clsCommon.myCstr(dr("Status")) = "1" Then
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Else
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = False
                        End If
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = clsCommon.myCstr(dr("DOCNo"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = clsCommon.myCstr(dr("Docdate"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColToSalesmanCode).Value = clsCommon.myCstr(dr("Tocode"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColToSalesmanname).Value = clsCommon.myCstr(dr("Todesc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRoute_No).Value = clsCommon.myCstr(dr("Route_No"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRoute_Desc).Value = clsCommon.myCstr(dr("Route_Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColType).Value = clsCommon.myCstr(dr("Type"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColPriceCode).Value = clsCommon.myCstr(dr("Price_code"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColPriceDesc).Value = clsCommon.myCstr(dr("Price_Desc"))
                        cmbitemtype.Enabled = False
                        txtDate.Enabled = False

                    Next
                End If
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
            obj = clsGatePassEntry.GetData(strCode, NavTyep, "")
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
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsGPDetail In obj.Arr
                        Gv1.Rows.AddNew()

                        If clsCommon.myCstr(objTr.Status) = "1" Then
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Else
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColApply).Value = False
                        End If
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocNo).Value = objTr.DocNo
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColDocDate).Value = objTr.Docdate
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColToSalesmanCode).Value = objTr.ToSalesmanCode
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColToSalesmanname).Value = objTr.ToSalesmanname
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRoute_No).Value = objTr.Route_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColRoute_Desc).Value = objTr.Route_Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColType).Value = objTr.Type
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColPriceCode).Value = objTr.Price_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColPriceDesc).Value = objTr.Price_Desc
                    Next
                    isInsideLoadData = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtVehicle.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Vehicle No")
        '    txtVehicle.Focus()
        '    Return False
        'End If

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
                    If clsCommon.myCBool(grow.Cells(ColApply).Value) Then
                        Dim objTr As New clsGPDetail()
                        objTr.DocNo = clsCommon.myCstr(grow.Cells(ColDocNo).Value)
                        objTr.Docdate = clsCommon.myCstr(grow.Cells(ColDocDate).Value)
                        objTr.ToSalesmanCode = clsCommon.myCstr(grow.Cells(ColToSalesmanCode).Value)
                        objTr.ToSalesmanname = clsCommon.myCstr(grow.Cells(ColToSalesmanname).Value)
                        objTr.Route_No = clsCommon.myCstr(grow.Cells(ColRoute_No).Value)
                        objTr.Route_Desc = clsCommon.myCstr(grow.Cells(ColRoute_Desc).Value)
                        objTr.Type = clsCommon.myCstr(grow.Cells(ColType).Value)
                        objTr.Vehicle_Id = txtVehicle.Value
                        objTr.Vehicle_Number = lblVehicleDesc.Text
                        objTr.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
                        objTr.Price_Desc = clsCommon.myCstr(grow.Cells(ColPriceDesc).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Document")
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, "")) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.GPCode, NavigatorType.Current)
                End If


            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Addnew()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        'txtVehicle.Value = ""
        'lblVehicleDesc.Text = ""
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
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Public Sub PrintGatePass(ByVal GPNo As String, ByVal PrePrinted As String)
        Dim qry, qry1, qry2 As String
        Dim blnSubqry As Boolean = False
        Try
            Dim strItemType, strInvoice, strTransfer, struni, subQry, strGlass As String
            struni = ""
            If cmbitemtype.Text = "Empty" Then
                strItemType = "EB"
            Else
                strItemType = "FB"
            End If

            If clsCommon.myLen(GPNo) > 0 Then
                qry1 = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as Time, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name, XXXX.*, "
                qry1 += " (Select Class_Desc from TSPL_ITEM_DETAILS Where Class_Name ='Size' AND Item_Code=XXXX.Item_Code) as [Pack], "
                qry1 += " (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress,'" & cmbitemtype.Text & "' as Type,emptytype,MRP "
                qry1 += " from ( "

                qry1 += " Select Cust_Name,isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as DocNo, MAX( TSPL_GATEPASS_MASTER.Created_By) as Created_By ,max(TSPL_GATEPASS_MASTER.Modified_By) as Modified_By , max(Ship_To) as Ship_To,max(Ship_To_Desc) as Ship_To_Desc,max(ShipToCity) as ShipToCity,max(ShipToPin) as ShipToPin, " & _
                "case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then MAX(ShipToadd) else 'NA' end as DeliveryTo , " & _
                "max(XXX.Salescode) as Salescode,case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then  max(XXX.Salesname) else case when max(xxx.Trans_Type)='Route' then max(ToLoc_Desc) else 'NA' end  end as DriverName, " & _
                "case when max(TSPL_GATEPASS_DETAIL.Type)='Sale' then MAX(Cust_Name) else " & _
                "case when max(TSPL_GATEPASS_MASTER.Item_Type)='Full' then case when max(xxx.Trans_Type)='Excise' then " & _
                "case when  max(TSPL_GATEPASS_DETAIL.Route_No) <> '' then max(TSPL_GATEPASS_DETAIL.Route_Desc)  else max(ToLoc_Desc) end " & _
                "when max(xxx.Trans_Type)='Route' and max(XXX.Location_Type)='logical' and max(XXX.Reference_Doc_No)='' then max(TSPL_GATEPASS_DETAIL.Route_Desc) " & _
                "when max(XXX.Trans_Type)='Depot' then max(ToLoc_Desc) " & _
                "else max(ToLoc_Desc) end End end  as PartyName, " & _
                "MAX(TSPL_GATEPASS_MASTER.Transporter) as TransporterName, " & _
                "MAX(TSPL_GATEPASS_MASTER.Remarks) as Remarks, MAX(TSPL_GATEPASS_MASTER.Comments) as Comments, " & _
                "case when ((Select min(Route_No) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "'  Group By GPCode)) = (Select max(Route_No) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "'  ) then " & _
                "MAX(TSPL_GATEPASS_DETAIL.Route_Desc) else '' end as Routeheader , " & _
                "'' as PriceCode, " & _
                "'' as PriceDesc, " & _
                " max(From_Location) as FromLoc," & _
                " max(FromLoc_Desc) as FromLocDesc, " & _
                " max(To_Location) as ToLoc, " & _
                " max(ToLoc_Desc) as ToLocDesc, " & _
                " Case When (Select COUNT(*) from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "' and   TSPL_GATEPASS_DETAIL.Type = 'LO') = 0 Then max(Cust_Name) Else " & _
                "(Select top 1 ToSalesmanname from TSPL_GATEPASS_DETAIL Where  TSPL_GATEPASS_DETAIL.GPCode = '" + txtCode.Value + "' and " & _
                " TSPL_GATEPASS_DETAIL.Type = 'LO')  END as custname,max(Transporter_Name) as Transport_Id,TSPL_GATEPASS_MASTER.GPCode, " & _
                " CONVERT(date,MAX(GPDate),103) as GPDate,CONVERT(time,MAX(GPDate),103) as GPTime, MAX(TSPL_GATEPASS_MASTER.Vehicle_Number) as VehicleNo, "
                qry1 += " XXX.Item_Code, MAX(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc, Sum(XXX.Qty) as Qty, MAX(TSPL_GATEPASS_DETAIL.Route_No) as Route_No, "
                qry1 += " MAX(TSPL_GATEPASS_DETAIL.Route_Desc) as Route_Desc, MAX(TSPL_GATEPASS_DETAIL.ToSalesmanCode) as ToSalesmanCode, "
                qry1 += " MAX(TSPL_GATEPASS_DETAIL.ToSalesmanname) as ToSalesmanname, MAX(TSPL_GATEPASS_MASTER.Comp_Code) as CompCode, "
                qry1 += " Case When (Select COUNT(*) from TSPL_GATEPASS_DETAIL Where TSPL_GATEPASS_DETAIL.GPCode = TSPL_GATEPASS_MASTER.GPCode)=1 Then  " & _
                "MAX(xxx.DocNo) Else '' END as [InvoiceNo] ,XXX.emptytype,XXX.MRP, " & _
                "Sum(XXX.Glass) as Glass,Sum(XXX.Cartons) as Cartons"
                qry1 += " from ("

                strInvoice = " Select Ship_To,TSPL_SHIP_TO_LOCATION.Ship_To_Desc,(TSPL_SHIP_TO_LOCATION.Add1+ TSPL_SHIP_TO_LOCATION.Add2 + Add3 + add4) as ShipToadd, " & _
                "TSPL_SHIP_TO_LOCATION.City_Code as ShipToCity,TSPL_SHIP_TO_LOCATION.Pin_Code as ShipToPin ,'' as Salescode,Emp_Name as Salesname,'' as Reference_Doc_No,'' as Location_Type,'' as Trans_Type,'' as From_Location,'' as FromLoc_Desc,'' as To_Location,'' as ToLoc_Desc,Cust_Name,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, TSPL_SALE_INVOICE_DETAIL.Item_Code, Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Qty, " & _
                "1 as [InvCount],case when Two_Count_Status='Y' then 'RGB' else 'Others' end as emptytype, " & _
                "case when TSPL_SALE_INVOICE_DETAIL.Unit_code='" & strItemType & "' then MRP_Amt else MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor end as MRP, " & _
                "case when Two_Count_Status='Y' then Invoice_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Glass,case when Two_Count_Status='N' then Invoice_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end as Cartons  from "
                strInvoice += " TSPL_SALE_INVOICE_DETAIL  Left Outer Join TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join "
                strInvoice += " TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on "
                strInvoice += " TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
                "left outer join TSPL_ITEM_UOM_DETAIL  as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                                " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SALE_INVOICE_HEAD.Ship_To=TSPL_SHIP_TO_LOCATION.Ship_To_Code "
                strInvoice += " Where  Shipment_Type='Sale'  and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='" & strItemType & "'  "
                struni += " UNION all "
                strTransfer = " Select '' as Ship_To,'' as Ship_To_Desc,'' as ShipToadd,City_Code as ShipToCity,'' as ShipToPin  ,Salesmancode as Salescode,Emp_Name as Salesname,Reference_Doc_No,TSPL_TRANSFER_HEAD.Location_Type,Trans_Type,From_Location,FromLoc_Desc, " & _
                "case when GIT_Type='Y' then (select top 1 Location_Code from TSPL_LOCATION_MASTER where GIT_Location=To_Location) else To_Location end as To_Location, " & _
                "case when GIT_Type='Y' then (select top 1 Location_Desc from TSPL_LOCATION_MASTER where GIT_Location=To_Location) else ToLoc_Desc end as ToLoc_Desc, " & _
                "'' as Cust_Name,TSPL_TRANSFER_HEAD.Transfer_No As DocNo, TSPL_TRANSFER_DETAIL.Item_Code, Item_Qty as Qty, 1 as [InvCount], "
                strTransfer += " case when Two_Count_Status='Y' then 'RGB' else 'Others' end as emptytype,case when TSPL_TRANSFER_DETAIL.Uom='" & strItemType & "' then MRP "
                strTransfer += " else MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor end as MRP,  " & _
                " case when Two_Count_Status='Y' then Item_Qty else 0 end as Glass,case when Two_Count_Status='N' then Item_Qty else 0 end as Cartons "
                strTransfer += " from TSPL_TRANSFER_DETAIL  LEFT OUTER JOIN TSPL_TRANSFER_HEAD On TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No "
                strTransfer += "  left outer join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on"
                strTransfer += " TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_TRANSFER_DETAIL.Uom=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  left outer join  " & _
                "TSPL_EMPLOYEE_MASTER on TSPL_TRANSFER_HEAD.Salesmancode=TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
                "left outer join TSPL_ITEM_UOM_DETAIL  as TSPL_ITEM_UOM_DETAIL_1 on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code "
                strTransfer += "Where Transfer_Type='LO' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='" & strItemType & "'  "

                qry2 = " ) XXX LEFT OUTER JOIN TSPL_GATEPASS_DETAIL ON TSPL_GATEPASS_DETAIL.DocNo=XXX.DocNo "
                qry2 += " LEFT OUTER JOIN TSPL_GATEPASS_MASTER ON TSPL_GATEPASS_MASTER.GPCode=TSPL_GATEPASS_DETAIL.GPCode"
                qry2 += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=XXX.Item_Code " & _
                "left outer join TSPL_VEHICLE_MASTER on TSPL_GATEPASS_MASTER.Vehicle_Id=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                "left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  " & _
                "CROSS APPLY  (SELECT DocNo + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL  where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and ToSalesmanname=Cust_Name  FOR XML PATH(''))el(files)  "
                qry2 += " WHERE TSPL_GATEPASS_MASTER.GPCode='" + txtCode.Value + "' Group By TSPL_GATEPASS_MASTER.GPCode, xxx.Item_Code ,xxx.MRP ,xxx.emptytype,el.files  ,Cust_Name  "

                qry2 += " ) XXXX LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=XXXX.CompCode"
            Else
                Throw New Exception("Gate Pass No. not found to print.")
            End If

            qry = qry1 & strInvoice & struni & strTransfer & qry2
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            If PrePrinted Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "crptGatePassPreprinted", "GatePass Report", True)
            Else
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Vizag") = CompairStringResult.Equal Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, EnumTecxpertPaperSize.NA, "crptGatePassVizag", "GatePass Report", False)

                Else
                    strGlass = " and Two_Count_Status='Y' "
                    subQry = "select Cust_Name,(isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile') ) as DocNo, " & _
                    "(isnull(LEFT(ey.files ,LEN(ey.files )-1),'NoFile') ) as Qty,max(TSPL_GATEPASS_DETAIL.GPCode) as GPCode from (" & strInvoice & strGlass & struni & strTransfer & strGlass & " ) XXX  " & _
                    "LEFT OUTER JOIN TSPL_GATEPASS_DETAIL ON TSPL_GATEPASS_DETAIL.DocNo=XXX.DocNo  LEFT OUTER JOIN " & _
                    "TSPL_GATEPASS_MASTER ON TSPL_GATEPASS_MASTER.GPCode=TSPL_GATEPASS_DETAIL.GPCode " & _
                    "CROSS APPLY  (SELECT distinct DocNo + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL left outer join  " & _
                    "TSPL_SALE_INVOICE_DETAIL on TSPL_GATEPASS_DETAIL.DocNo=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join " & _
                    "TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                    "where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and Two_Count_Status='Y' and ToSalesmanname=Cust_Name  FOR XML PATH(''))el(files)  " & _
                    "CROSS APPLY  (SELECT  convert(varchar(30), sum(convert(decimal(18,2),Invoice_Qty/Conversion_Factor))) + ' , ' AS [text()]   FROM TSPL_GATEPASS_DETAIL left outer join " & _
                    "TSPL_SALE_INVOICE_DETAIL on TSPL_GATEPASS_DETAIL.DocNo=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join " & _
                    "TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
                    "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                    "where TSPL_GATEPASS_DETAIL.GPCode='" + txtCode.Value + "' and Two_Count_Status='Y' and ToSalesmanname=Cust_Name  " & _
                    "group by ToSalesmanname,DocNo  FOR XML PATH(''))ey(files)  " & _
                    "WHERE TSPL_GATEPASS_MASTER.GPCode='" + txtCode.Value + "' Group By TSPL_GATEPASS_MASTER.GPCode,xxx.emptytype,el.files  ,Cust_Name,ey.files  "
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funsubreport(CrystalReportFolder.SalesReport, qry, subQry, "", "", "", "crptGatePassGuntur", "GatePass", "crptGatePassSubReport1.rpt")
                End If
            End If
            'FrmSalerReport.funreport(dt, "crptGatePass", "Gate Pass")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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








    Private Sub txtLocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocCode._MYValidating
        strQuery = "select Segment_code as Code,Description  from TSPL_GL_SEGMENT_CODE"
        txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "Seg_No='7'", txtLocCode.Value, "Code", isButtonClicked)
        txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code ='" & txtLocCode.Value & "'")
    End Sub



    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If isInsideLoadData = False Then
            If clsCommon.CompairString(cmbitemtype.Text, "Select") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Please select Item type")
            Else
                LoadBlankGrid()
                isNewEntry = True
                isInsideLoadData = False
                funFillGrid()
            End If
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmGatePassENtry1)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        RadSplitButton1.Visible = MyBase.isPrintFlag

    End Sub

    Private Sub FrmGatePassENtry1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        cmbitemtype.Text = "Select"
        txtTransporter.MaxLength = 100
        txtRemarks.MaxLength = 200
        txtComments.MaxLength = 200
        btnPost.Enabled = True
        btnPost.Visible = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtVehicle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicle._MYValidating
        If clsCommon.CompairString(cmbitemtype.Text, "Select") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please select Item type")
        Else
            strQuery = "select Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
            txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", txtVehicle.Value, "Code", isButtonClicked)
            lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")
            'If clsCommon.myLen(txtVehicle.Value) > 0 Then
            '    funFillGrid()
            'End If

        End If

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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = " SELECT  GPCode,GPDate,Vehicle_Id,Vehicle_Number FROM  tspl_gatepass_master"

        LoadData(clsCommon.ShowSelectForm("GatepassEntry", qry, "GPCode", "", txtCode.Value, "GPCode", isButtonClicked), NavigatorType.Current)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Addnew()
        cmbitemtype.Enabled = True
        txtDate.Enabled = True
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        PrintGatePass(txtCode.Value, True)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        PrintGatePass(txtCode.Value, False)
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Post Record")
            Exit Sub
        End If
        If myMessages.postConfirm() Then
            clsDBFuncationality.ExecuteNonQuery("Update tspl_gatepass_master set post='Y' where gpcode='" & txtCode.Value & "'")
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
End Class
