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
        btnDelete.Visible = False
        btnDelete.Enabled = False
        btnCancel.Visible = False
        btnCancel.Enabled = False
        btnPrintMultipleInvoice.Visible = False
        btnPrintMultipleInvoice.Enabled = False
        txtFromDate.Value = clsCommon.GETSERVERDATE().AddDays(-10)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
        txtInvoiceNo.Value = ""
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
                AddNew()
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
            LoadData(False)
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
                        Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                    Else
                        Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
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
    Public Sub fndLoadData(ByVal strInvoiceNo As String, ByVal NavType As NavigatorType)
        Try
            txtInvoiceNo.Value = strInvoiceNo
            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_NO,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Docuemnt_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as Status,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.total_tax_Amt,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt
from TSPL_SD_SALE_INVOICE_HEAD 
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where isMultipleInvoice = 1  "
            Dim whrCls As String = ""
            whrCls = " and isMultipleInvoice = 1 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " And TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD WHERE 1=1 " + whrCls + ") "
                Case NavigatorType.Last
                    strQry += " And TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD WHERE 1=1 " + whrCls + ")"
                Case NavigatorType.Current
                    strQry += " And TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strInvoiceNo + "' "
                Case NavigatorType.Next
                    strQry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code>'" + strInvoiceNo + "' " + whrCls + ")"
                Case NavigatorType.Previous
                    strQry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code<'" + strInvoiceNo + "' " + whrCls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            isInvoice = True
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                btnPrintMultipleInvoice.Visible = True
                btnPrintMultipleInvoice.Enabled = True
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
                btnDelete.Visible = True
                btnDelete.Enabled = True
                btnCancel.Visible = True
                btnCancel.Enabled = True
                btnSave.Enabled = False
                btnGo.Enabled = False
            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal isLoadData As Boolean)
        Try
            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Invoice No],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Docuemnt Date],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No],TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location],case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as Status,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount as [Amount Less Discount],TSPL_SD_SALE_INVOICE_HEAD.total_tax_Amt as [Total Tax Amt],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amt]
from TSPL_SD_SALE_INVOICE_HEAD 
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
where isMultipleInvoice=1"
            If isLoadData Then
                strQry += " And convert(date,document_date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
            Else
                strQry += " And convert(date,document_date,103)='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "'"
            End If
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
                btnPrintMultipleInvoice.Visible = True
                btnPrintMultipleInvoice.Enabled = True
            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        LoadData(True)
    End Sub
    Private Sub frmMultipleInvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnLoadData.Visible = True
                'btnDelete.Visible = True
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
                LoadData(False)
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
    Private Sub txtInvoiceNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtInvoiceNo._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim qry As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) = 0 Then
                qry = "select count(*) from tspl_sd_sale_invoice_head where Document_Code='" + txtInvoiceNo.Value + "' and IsMultipleInvoice=1 "
            Else
                qry = "select count(*) from tspl_sd_sale_invoice_head where Document_Code='" + txtInvoiceNo.Value + "' and IsMultipleInvoice=1 and tspl_sd_sale_invoice_head.Customer_Code in (" + strwherecls + ")"
            End If
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtInvoiceNo.MyReadOnly = False
            Else
                txtInvoiceNo.MyReadOnly = True
            End If
            fndLoadData(txtInvoiceNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtInvoiceNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtInvoiceNo._MYValidating
        Try
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            Dim strDONo As String = Nothing
            Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Code,TSPL_SD_SALE_INVOICE_HEAD.Route_No, CONVERT(varchar(10), TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103 )+ ' ' + CONVERT(varchar(5),TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 114) as Date,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code], Customer_Name as Customer, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], 
  Location_Desc as [Location Name], TSPL_SD_SALE_INVOICE_HEAD.Comments, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Amount,  case when TSPL_SD_SALE_INVOICE_HEAD.Status = 0 then 'Pending' else 'Approved' end as [Status],  case when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=0 then 'NT' else 'T' end as [Taxable - NonTaxable],  TSPL_SD_SALE_INVOICE_HEAD.Document_Date as FilterDate 
from 
  TSPL_SD_SALE_INVOICE_HEAD 
  Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
  left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER.Location_Code"
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and  TSPL_SD_SALE_INVOICE_HEAD.IsMultipleInvoice=1 and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and  TSPL_SD_SALE_INVOICE_HEAD.IsMultipleInvoice=1 "
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") and TSPL_SD_SALE_INVOICE_HEAD.IsMultipleInvoice=1 "
            Else
                whrClas = " TSPL_SD_SALE_INVOICE_HEAD.IsMultipleInvoice=1  "
            End If
            fndLoadData(clsCommon.ShowSelectForm("fndmultiInvoice", qry, "Code", whrClas, txtInvoiceNo.Value, "Code", isButtonClicked, "TSPL_SD_SALE_INVOICE_HEAD.Document_Date"), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Do you want to Cancel ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For intRow As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
                        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value), "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "TSPL_SD_SALE_INVOICE_DETAIL", "Document_Code", Nothing)
                        Dim status As Boolean = clsPSInvoiceHead.CancelData(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                        If status Then
                            Dim Qry As String = " update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='' where Sale_Invoice_No =('" + clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) + "')"
                            clsDBFuncationality.ExecuteNonQuery(Qry)
                        End If
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Cancel Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintMultipleInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintMultipleInvoice.Click
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim fromdate As String = ""
            Dim todate As String = ""
            Dim strInvoiceno As New List(Of String)

            For intRow As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
                    strInvoiceno.Add(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                End If
            Next

            Dim qry As String = ""
            If strInvoiceno.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow(" Do you want to print Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                    qry = "select  TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.Access_Officer as FassiLicNo,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3 as CompAddress,TSPL_COMPANY_MASTER.Phone1,xxx.*
from(
select xx.Document_Code,max(xx.Document_Date) as BillDate,max(xx.Route_No)as Route_No,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,sum(xx.QtyInLtr) as QtyInLtr,sum(xx.QtyInPouch) as QtyInPouch,max(xx.Rate_Per_Pouch) as Rate_Per_Pouch,sum(xx.Item_Net_Amt) as Item_Net_Amt,max(xx.Customer_Name) as Customer_Name,max(xx.Add1) as Add1,max(xx.Add2)as Add2,max(xx.add3) as Add3,
max(xx.GST_STATE_Code) as GST_STATE_Code,max(xx.STATE_NAME) as STATE_NAME,max(xx.GSTNO)as GSTNO,sum(xx.Transporter_Commission_TotalAmt)as TCAmt,min(xx.GPDate) as GPFromDate,max(xx.GPDate) as GPTodate
,STUFF(( SELECT DISTINCT ',' + GPCode FROM (SELECT DISTINCT RIGHT(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode, 6) AS GPCode FROM TSPL_SD_SALE_INVOICE_DETAIL
              LEFT JOIN TSPL_SD_SHIPMENT_DETAIL ON TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code
              LEFT JOIN TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL ON TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID = TSPL_SD_SHIPMENT_DETAIL.PK_ID
              WHERE TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = xx.Document_Code ) AS subquery
        FOR XML PATH('')), 1, 1, '') AS GPCodes
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)) as Rate_Per_Pouch,CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR,
TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3 ,TSPL_STATE_MASTER.GST_STATE_Code, TSPL_STATE_MASTER.STATE_NAME ,TSPL_CUSTOMER_MASTER.GSTNO,isnull(TSPL_SD_SALE_INVOICE_HEAD.Transporter_Commission_TotalAmt,0) as Transporter_Commission_TotalAmt,
TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,
TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode
from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State
left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_DETAIL.unit_code  
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code  and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
left join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode
where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in (" + clsCommon.GetMulcallString(strInvoiceno) + ")
) xx
group by xx.Document_Code,xx.Item_Code
)XXX
left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='UDP'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptMultipleInvoicePrint", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())

                    End If
                Else

                    frmCRV = Nothing
                End If

            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
