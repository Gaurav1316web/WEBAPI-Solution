''created By Richa Agarwal 
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmLeakageReplacementUploader
    Inherits FrmMainTranScreen

    Dim AllowZeroQtyOnDairyBookingUploader As Boolean = False
    Public Const colSlno As String = "colSlno"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colDispatchCode As String = "colDispatchCode"
    Public Const colSNRNO As String = "colSNRNO"

    Public colCrateQty As String = "colCrateQty"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colInvoiceNo As String = "colInvoiceNo"

    Public TextCol As GridViewTextBoxColumn = Nothing
    Public DecCol As GridViewDecimalColumn = Nothing
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing




    Private Sub frmMCCMaterialSaleUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("Location_Code", "VARCHAR(12) NULL REFERENCES TSPL_LOCATION_MASTER(Location_Code)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_COMPLAINT_HEAD", coll, Nothing, False, False, "", "Complaint_No", "Complaint_Date")


        coll = New Dictionary(Of String, String)()
        coll.Add("IsTaxable", "integer not NULL default 0 ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_COMPLAINT_DETAIL", coll, Nothing, False, False, "TSPL_CUSTOMER_COMPLAINT_HEAD", "Complaint_No", "")


        RadPageView1.SelectedPage = RadPageViewPage1

        Gv1.Visible = True
        rdbAgainstLeakageReplacement.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()

        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtLocation.Value + "' "))
        End If
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        Gv1.Columns.Clear()
        Gv1.DataSource = Nothing
        transportSql.importExcelWithoutReadColumnName(Gv1, "")
        If Gv1.Columns.Count > 0 Then
            TextCol = New GridViewTextBoxColumn()
            TextCol.Name = colSlno
            TextCol.HeaderText = "SL. No."
            TextCol.ReadOnly = True
            Gv1.MasterTemplate.Columns.Insert(0, TextCol)

            ChkBoxColumn = New GridViewCheckBoxColumn()
            ChkBoxColumn.Name = colIsValidated
            ChkBoxColumn.HeaderText = "Validated"
            ChkBoxColumn.ReadOnly = True
            Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

            TextCol = New GridViewTextBoxColumn()
            TextCol.Name = colErrorStatus
            TextCol.HeaderText = "Error Status"
            TextCol.ReadOnly = True
            Gv1.MasterTemplate.Columns.Insert(2, TextCol)

            TextCol = New GridViewTextBoxColumn()
            TextCol.Name = colInvoiceDate
            TextCol.HeaderText = "InvoiceDate"
            TextCol.ReadOnly = True
            TextCol.IsVisible = True
            Gv1.MasterTemplate.Columns.Insert(3, TextCol)

            TextCol = New GridViewTextBoxColumn()
            TextCol.Name = colInvoiceNo
            TextCol.HeaderText = "InvoiceNO"
            TextCol.ReadOnly = True
            TextCol.IsVisible = True
            Gv1.MasterTemplate.Columns.Insert(3, TextCol)

            'TextCol = New GridViewTextBoxColumn()
            'TextCol.Name = colZeroQtyForAllItem
            'TextCol.HeaderText = "ZeroQtyForAllItem"
            'TextCol.ReadOnly = True
            'TextCol.IsVisible = False
            'Gv1.MasterTemplate.Columns.Insert(5, TextCol)


            For i As Integer = 0 To Gv1.Rows.Count - 1
                Gv1.Rows(i).Cells(colSlno).Value = (i + 1)
                Gv1.Rows(i).Cells(colIsValidated).Value = False
                ValidatedCount = 0
                Gv1.Rows(i).Cells(colErrorStatus).Value = ""
            Next
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).ReadOnly = True
            Next
            Gv1.AllowAddNewRow = False
            Gv1.AllowDeleteRow = True
            Gv1.EnableFiltering = True
            Gv1.EnableSorting = False
            Gv1.EnableGrouping = False
            Gv1.AllowColumnChooser = False
            Gv1.AllowColumnReorder = True
            Gv1.BestFitColumns()
            Gv1.AutoSizeRows = True
            Gv1.TableElement.TableHeaderHeight = 30
        End If

    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()

    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim qry As String = String.Empty
        qry = "select '' as BoothId,'' as ComplainType"
        transportSql.ExporttoExcel(qry, Me)

        qry = Nothing
    End Sub

    Private Sub btnExportInvalid_Click(sender As Object, e As EventArgs) Handles btnExportInvalid.Click
        Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
        Dim dirName As String = "c:\ERPTempFolder"

        If Not System.IO.Directory.Exists(dirName) Then
            System.IO.Directory.CreateDirectory(dirName)
        End If
        transportSql.QuickExportToExcel(Gv1, "", Me.Text)
        Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
    End Sub

    Private Sub btnSaveAndPost_Click(sender As Object, e As EventArgs) Handles btnSaveAndPost.Click
        SaveAndPost()

    End Sub
    Sub CheckAndValidate()
        Dim ValidateStatus As String = String.Empty

        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("There is no row in grid please select a sheet to import")
        End If
        If ValidatedCount = Gv1.Rows.Count Then
            clsCommon.MyMessageBoxShow("All Rows are already validated")
            Exit Sub
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location")
            Exit Sub
        End If
        ValidatedCount = 0
        Dim custCounter As Integer = 0
        Dim strCellValue
        If rdbAgainstLeakageReplacement.IsChecked Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                custCounter = 1
                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer No not found in master" & Environment.NewLine
                End If

                '                Dim strTaxable As String = Nothing
                '                If rbtnNonTaxable.IsChecked = True Then
                '                    strTaxable = " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=0 "
                '                Else
                '                    strTaxable = " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 "
                '                End If

                '                Dim qry As String = "SELECT top 1 * FROM (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [InvoiceDate] , TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], TSPL_Location_Master.Location_Desc as [Location Name],tspl_booking_matser.Booking_type,case when tspl_booking_matser.Booking_type='CASH' THEN 0  when tspl_booking_matser.Booking_type='CR' THEN 1 when tspl_booking_matser.Booking_type='SO' THEN 2 ELSE 3 END AS OrderPrefernce   from TSPL_SD_SALE_INVOICE_HEAD  
                'Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  
                'Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location 
                'left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
                'left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no= TSPL_SD_SHIPMENT_HEAD.against_delivery_code
                'left outer join tspl_booking_matser on tspl_booking_matser.document_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_no
                'WHERE TSPL_SD_SALE_INVOICE_HEAD.Status =1 and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type in ('PS','FS') " & strTaxable & " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1)
                'and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" & strCellValue & "' and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" & txtDate.Value.AddDays(-1) & "',103) )Z order by OrderPrefernce "

                '                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                '                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                '                    Gv1.Rows(i).Cells(colInvoiceNo).Value = clsCommon.myCstr(dt1.Rows(0)("InvoiceNo"))
                '                    Gv1.Rows(i).Cells(colInvoiceDate).Value = clsCommon.myCstr(dt1.Rows(0)("InvoiceDate"))
                '                Else
                '                    ValidateStatus = ValidateStatus & "Sale Invoice not found " & Environment.NewLine
                '                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("ComplainType").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Complaint Type Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_COMPLAINT_MASTER where Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Complaint Type not found in master" & Environment.NewLine
                End If


                Dim dblTotalQty As Double = 0
                Dim counterstart As Integer = 0


                counterstart = 7

                For k As Integer = counterstart To Gv1.Columns.Count - 1
                    strCellValue = clsCommon.myCstr(Gv1.Columns(k).Name)
                    If clsCommon.myLen(strCellValue) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item Desc Must not be Blank" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Short_Description='" & strCellValue & "'  and  isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item code not found for item desc '" & strCellValue & "' in master according to sale criteria." & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                    dblTotalQty = dblTotalQty + clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                    If strCellValue < 0 Then
                        ValidateStatus = ValidateStatus & "QTY Must not be Negative" & Environment.NewLine
                    End If

                Next


                If dblTotalQty <= 0 Then
                    ValidateStatus = ValidateStatus & "Plz enter qty in any column of item" & Environment.NewLine
                End If


                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    Gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
                Else
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.Red
                End If

            Next
        End If

        Gv1.BestFitColumns()
        Gv1.AutoSizeRows = True
        Gv1.Columns(colSlno).PinPosition = PinnedColumnPosition.Left
        Gv1.Columns(colIsValidated).PinPosition = PinnedColumnPosition.Left
        Gv1.Columns(colErrorStatus).PinPosition = PinnedColumnPosition.Left
        clsCommon.ProgressBarPercentHide()
    End Sub

    Sub SaveAndPost()

        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim trans As SqlTransaction = Nothing
        Try

            If ValidatedCount > 0 Then
                clsCommon.ProgressBarPercentShow()
                trans = clsDBFuncationality.GetTransactin()

                objCommonVar.CurrentUserCode = CurrentUserCode
                BookingSaveData(trans)

                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                Gv1.Columns.Clear()
            Else
                Throw New Exception("No Validated Rows found to save")
            End If
        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message & " At Row No " & (i + 1))
        End Try
    End Sub

    Sub BookingSaveData(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsCustomerComplainDetail = Nothing
        Dim obj As clsCustomerComplainHead = Nothing

        Dim LineNo As Integer = 1
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colIsValidated).Value = True Then
                    j += 1
                    clsCommon.ProgressBarPercentUpdate(j * 100 / Gv1.Rows.Count, " Creating Leakage Replacement Records " & j & " of Total " & Gv1.Rows.Count)
                    Dim intCurrInvNo As Integer = clsCommon.myCdbl(Gv1.Rows(i).Cells(colSlno).Value)

                    If clsCommon.CompairString(strcountno, clsCommon.myCstr(Gv1.Rows(i).Cells(colSlno).Value)) <> CompairStringResult.Equal Then
                        LineNo = 1
                        DocuAmount = 0
                        Tax1Amt = 0
                        Tax2Amt = 0
                        TaxBaseAmount = 0
                        TotalCrate = 0
                        obj = New clsCustomerComplainHead()



                        obj.Complaint_Date = clsCommon.myCDate(txtDate.Value)
                        'obj.Invoice_No = clsCommon.myCstr(Gv1.Rows(i).Cells(colInvoiceNo).Value)
                        'If clsCommon.myLen(obj.Invoice_No) > 0 Then
                        '    obj.Invoice_Date = clsCommon.myCDate(clsCommon.myCstr(Gv1.Rows(i).Cells(colInvoiceDate).Value))

                        'End If
                        obj.Location_Code = clsCommon.myCstr(txtLocation.Value)
                        obj.Invoice_Date = obj.Complaint_Date
                        obj.Cust_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)

                        obj.Arr = New List(Of clsCustomerComplainDetail)
                        Dim counterstart As Integer = 0
                        counterstart = 7

                        For k As Integer = counterstart To Gv1.Columns.Count - 1
                            If clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value) > 0 Then
                                objTr = New clsCustomerComplainDetail()
                                objTr.SNo = LineNo

                                objTr.Damage_Qty = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                                objTr.Complaint_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("ComplainType").Value).ToUpper.ToString


                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code as UOM_Code ,tspl_item_master.IsTaxable,
case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code 
where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(Gv1.Columns(k).Name) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Damage_Uom = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                                        objTr.HSN_Code = clsCommon.myCstr(dr("HSN_Code"))
                                        objTr.isTaxable = clsCommon.myCstr(dr("IsTaxable"))

                                    Next
                                End If

                                'qry = " select unit_code,Qty from TSPL_SD_SALE_INVOICE_DETAIL where document_code='" + clsCommon.myCstr(Gv1.Rows(i).Cells(colInvoiceNo).Value) + "' and Item_Code='" & objTr.Item_Code & "'  "

                                'dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                '    For Each dr As DataRow In dt1.Rows
                                '        objTr.Unit_Code = clsCommon.myCstr(dr("unit_code"))
                                '        objTr.Qty = clsCommon.myCdbl(dr("Qty"))

                                '    Next
                                'End If


                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1

                                '''' for zero qty doc

                            End If
                        Next
                    End If
                    LineNo = 0
                    strcountno = intCurrInvNo
                    Dim intNextInvNo As Integer = -1

                    If intCounter + 1 < Gv1.Rows.Count Then
                        intNextInvNo = clsCommon.myCdbl(Gv1.Rows(intCounter + 1).Cells(colSlno).Value)
                    End If

                    If Not (intCurrInvNo = intNextInvNo) Then

                        obj.SaveData(obj, True, Nothing, trans)
                        'If (clsCustomerComplainHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                        '    '' auto creation of Dairy dispatch as replacement by richa 
                        '    Dim frm As New frmShipmentDairy
                        '    frm.SetUserMgmt(clsUserMgtCode.frmSaleDispatchDairy)
                        '    frm.Show()
                        '    frm.chkReplacement.Checked = True
                        '    frm.RadLabel24.Text = "Invoice No"
                        '    frm.txtReqNo.Visible = False
                        '    frm.TxtInvoiceNoForReplacement.Visible = True
                        '    frm.txtReqNo.Value = ""
                        '    frm.txtCustomerComplaintNo.Text = ""
                        '    frm.txtCustomerComplaintNo.Text = obj.Complaint_No
                        '    frm.txtVendorNo.Value = obj.Cust_Code
                        '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & obj.Invoice_No & "'", trans)), "0") = CompairStringResult.Equal Then
                        '        frm.cmbDisItemType.SelectedValue = "NT"
                        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & obj.Invoice_No & "'", trans)), "1") = CompairStringResult.Equal Then
                        '        frm.cmbDisItemType.SelectedValue = "T"
                        '    End If
                        '    frm.txtDate.Value = obj.Complaint_Date
                        '    frm.txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bill_To_Location  from TSPL_SD_SALE_INVOICE_HEAD  where document_code='" & obj.Invoice_No & "'", trans))
                        '    frm.txtSubLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_location_code  from TSPL_SD_SALE_INVOICE_HEAD  where document_code='" & obj.Invoice_No & "'", trans))
                        '    frm.SelectInvoiceNo()
                        '    frm.showSavedMessage = False
                        '    frm.SaveData(False, Nothing)
                        '    Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD where isnull(Customer_Complaint_No,'')='" & txtDocNo.Value & "'"))
                        '    If clsCommon.myLen(strShipmentNo) > 0 Then
                        '        If clsPSShipmentHead.PostData(clsUserMgtCode.frmSaleDispatchDairy, strShipmentNo, True) Then
                        '            frm.Close()
                        '        End If
                        '    End If

                        'End If



                    End If
                    intCounter += 1
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub
End Class