Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class frmMultipleInvoice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isInvoice As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnPrint.Visible = MyBase.isPrintFlag
        'btnDelete.Visible = False
        'If MyBase.isReverse Then
        '    btnReverseAndUnpost.Enabled = True
        'Else
        '    btnReverseAndUnpost.Enabled = False
        'End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub frmMultipleInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
    End Sub
    Public Sub AddNew()
        isInvoice = False
        txtFromDate.Value = clsCommon.GETSERVERDATE().AddDays(-10)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        rbtnTaxable.Checked = True
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnLoadData.Visible = False
        btnDelete.Visible = False
        btnGo.Enabled = True
        rgbItemType.Enabled = True
        txtLocation.Enabled = True
        txtCustomer.Enabled = True
        txtToDate.Enabled = True
        txtFromDate.Enabled = True
        txtFromShift.Enabled = True
        txtToShift.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If txtFromDate.Value <= txtToDate.Value AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim WhrCls As String = ""
                Dim FromShift As String = ""
                Dim ToShift As String = ""
                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                    FromShift = "PM"
                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal Then
                    FromShift = "AM"
                End If
                If clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
                    ToShift = "PM"
                ElseIf clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                    ToShift = "AM"
                End If
                Dim Qry As String = "select TSPL_SD_SHIPMENT_HEAD.Customer_Code as Customer_Code,TSPL_SD_SHIPMENT_HEAD.Route_No as Route_No
from TSPL_SD_SHIPMENT_HEAD
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
 where 2=2 "
                Qry += "  and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No='' "
                Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')) "

                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                    Qry += " AND ( 
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "

                ElseIf clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "
                End If
                If rbtnNonTaxable.Checked Then
                    Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                Else
                    Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                End If
                Qry += "  group by TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Route_No "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.DataSource = dt
                    gv1.GroupDescriptors.Clear()
                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.EnableFiltering = True
                    gv1.AllowAddNewRow = False
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                        gv1.Columns(ii).IsVisible = True
                    Next
                    gv1.BestFitColumns()
                    btnGo.Enabled = False
                    rgbItemType.Enabled = False
                    txtLocation.Enabled = False
                    txtCustomer.Enabled = False
                    txtToDate.Enabled = False
                    txtFromDate.Enabled = False
                    txtFromShift.Enabled = False
                    txtToShift.Enabled = False
                Else
                    Throw New Exception("Data Not Found!")
                End If
            Else
                Throw New Exception("Invalid Date Range!")
            End If
        Catch ex As Exception
            'clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = "  location_code in (select distinct loc_code from tspl_booking_detail) and Location_Category not in('MCC')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("DS-MILocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Do you want to Post ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For intRow As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(6).Value), "Pending") = CompairStringResult.Equal Then
                            clsPSInvoiceHead.PostData(Me.Form_ID, clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value), True)
                        End If
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Posted Successfully", Me.Text)
                LoadData()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = ""
            Dim strWhrClause As String = ""
            qry = "select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman  " &
        ",TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Number,TSPL_VEHICLE_MASTER.Capacity "
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
            qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
            qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
            qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'" &
        "left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " &
        "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " &
         " where TSPL_CUSTOMER_MASTER.Status='N' "
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCode@MultipleInvoice", qry, "Code", "Code", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(trans)
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Invoice Saved Successfully", Me.Text)
            LoadData()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SaveData(ByVal trans As SqlTransaction)
        Try
            Dim FromShift As String = ""
            Dim ToShift As String = ""
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                FromShift = "PM"
            ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal Then
                FromShift = "AM"
            End If
            If clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
                ToShift = "PM"
            ElseIf clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                ToShift = "AM"
            End If
            For intRow As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value)) > 0 Then
                    Dim Qry As String = "select Document_Code from TSPL_SD_SHIPMENT_HEAD
            where Customer_Code='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) + "' and Route_No='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(1).Value) + "'  
             and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No='' "

                    Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                    If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
                        Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')) "

                    ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                        Qry += " AND ( 
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "

                    ElseIf clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                        Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "
                    End If


                    If rbtnNonTaxable.Checked Then
                        Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                    Else
                        Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim obj As New clsPSShipmentHead
                        Dim lstDocument As New List(Of String)
                        For Each dr As DataRow In dt.Rows
                            lstDocument.Add(clsCommon.myCstr(dr("Document_Code")))
                        Next
                        obj = clsMultipleInvoice.GetShipmentDetail(lstDocument, trans)
                        Dim ObjInv As clsPSInvoiceHead = clsMultipleInvoice.ConvertShipmentToSaleInvoice(obj, True, trans)
                        Dim status As Boolean = ObjInv.SaveData(ObjInv, True, trans, True)
                        If status Then
                            Qry = "update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='" + ObjInv.Document_Code + "' where Document_Code in(" + clsCommon.GetMulcallString(lstDocument) + ")"
                            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Else
                            Throw New Exception("Something went worng while creating invoice!")
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub LoadData()
        Try
            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_NO,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Docuemnt_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as Status,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.total_tax_Amt,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt
from TSPL_SD_SALE_INVOICE_HEAD 
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
where isMultipleInvoice=1 and convert(date,document_date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            isInvoice = True
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()
                btnSave.Enabled = False
                btnPost.Enabled = True
            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        LoadData()
    End Sub
    Private Sub frmMultipleInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnLoadData.Visible = True
                btnDelete.Visible = True
            End If
        End If
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Do you want to delete ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For intRow As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(6).Value), "Approved") = CompairStringResult.Equal Then
                            clsPSInvoiceHead.ReverseAndUnpost(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                        End If
                        Dim status As Boolean = clsPSInvoiceHead.DeleteData(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                        If status Then
                            Dim Qry As String = " update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='' where Sale_Invoice_No =('" + clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) + "')"
                            clsDBFuncationality.ExecuteNonQuery(Qry)
                        End If

                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Delete Successfully", Me.Text)
                LoadData()
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If isInvoice Then
                If gv1.CurrentColumn Is gv1.Columns(0) Then
                    Dim frm As New FrmInvoiceDetails()
                    frm.strInvoiceNo = clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value)
                    frm.strInvoiceDate = clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value)
                    frm.ShowDialog()
                End If
            Else
                If gv1.CurrentColumn Is gv1.Columns(0) Then
                    Dim frm As New frmShipmentDetails()
                    frm.strFromDate = txtFromDate.Value
                    frm.strToDate = txtToDate.Value
                    frm.strFromShift = clsCommon.myCstr(txtFromShift.Text)
                    frm.strToshift = clsCommon.myCstr(txtToShift.Text)
                    frm.strCustCode = clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value)
                    frm.strItemType = IIf(rbtnNonTaxable.Checked, "NT", "T")
                    frm.strRouteNo = clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value)
                    frm.strLocation = clsCommon.myCstr(txtLocation.Value)
                    frm.ShowDialog()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
