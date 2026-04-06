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
        btnDelete.Visible = False
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
        btnDelete.Visible = False
    End Sub
    Public Sub AddNew()
        isInvoice = False
        btnDelete.Visible = False
        'btnDelete.Enabled = False
        btnCancel.Visible = False
        btnCancel.Enabled = False
        btnPrintMultipleInvoice.Visible = False
        btnPrintMultipleInvoice.Enabled = False
        txtFromDate.Value = clsCommon.GETSERVERDATE().AddDays(-10)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtInvoiceDate.Value = txtToDate.Value
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

        'LoadShipmentReturn_Go()
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
                (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
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
    '    Private Sub LoadShipment_Go()
    '        Try
    '            If txtFromDate.Value <= txtToDate.Value AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
    '                Dim WhrCls As String = ""
    '                Dim FromShift As String = ""
    '                Dim ToShift As String = ""
    '                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
    '                    FromShift = "PM"
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal Then
    '                    FromShift = "AM"
    '                End If
    '                If clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
    '                    ToShift = "PM"
    '                ElseIf clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    ToShift = "AM"
    '                End If
    '                Dim Qry As String = "select TSPL_SD_SHIPMENT_HEAD.Customer_Code as Customer_Code,TSPL_SD_SHIPMENT_HEAD.Route_No as Route_No
    'from TSPL_SD_SHIPMENT_HEAD
    'left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
    ' where 2=2 "
    '                Qry += "  and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No='' "
    '                Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
    '                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
    '                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
    '        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')) "
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    Qry += " AND ( 
    '        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
    '            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + FromShift + "') OR
    '        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
    '            or (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_HEAD.Shift_Type = '" + ToShift + "') ) "
    '                End If
    '                If rbtnNonTaxable.Checked Then
    '                    Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
    '                Else
    '                    Qry += " and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
    '                End If
    '                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
    '                    Qry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
    '                End If
    '                Qry += "  group by TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Route_No "
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '                gv1.DataSource = Nothing
    '                gv1.Rows.Clear()
    '                gv1.Columns.Clear()
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    gv1.DataSource = dt
    '                    gv1.GroupDescriptors.Clear()
    '                    gv1.ShowGroupPanel = False
    '                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '                    gv1.EnableFiltering = True
    '                    gv1.AllowAddNewRow = False
    '                    For ii As Integer = 0 To gv1.Columns.Count - 1
    '                        gv1.Columns(ii).ReadOnly = True
    '                        gv1.Columns(ii).IsVisible = True
    '                    Next
    '                    gv1.BestFitColumns()
    '                    btnGo.Enabled = False
    '                    rgbItemType.Enabled = False
    '                    txtLocation.Enabled = False
    '                    txtCustomer.Enabled = False
    '                    txtToDate.Enabled = False
    '                    txtFromDate.Enabled = False
    '                    txtFromShift.Enabled = False
    '                    txtToShift.Enabled = False
    '                Else
    '                    Throw New Exception("Data Not Found!")
    '                End If
    '            Else
    '                Throw New Exception("Invalid Date Range!")
    '            End If
    '        Catch ex As Exception
    '            'clsCommon.ProgressBarPercentHide()
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub LoadShipmentReturn_Go()
    '        Try
    '            If txtFromDate.Value <= txtToDate.Value AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
    '                Dim WhrCls As String = ""
    '                Dim FromShift As String = ""
    '                Dim ToShift As String = ""
    '                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
    '                    FromShift = "PM"
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal Then
    '                    FromShift = "AM"
    '                End If
    '                If clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
    '                    ToShift = "PM"
    '                ElseIf clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    ToShift = "AM"
    '                End If
    '                Dim Qry As String = "select TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code as Customer_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.Route_No as Route_No
    'from TSPL_SD_SHIPMENT_RETURN_HEAD
    'left join TSPL_SD_SHIPMENT_RETURN_DETAIL on TSPL_SD_SHIPMENT_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code
    ' where 2=2 "
    '                Qry += "  and TSPL_SD_SHIPMENT_RETURN_HEAD.Status=1  "
    '                Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
    '                If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
    '                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + FromShift + "') OR
    '        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')) "
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    Qry += " AND ( 
    '        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
    '            or (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + ToShift + "') ) "
    '                ElseIf clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '                    Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + FromShift + "') OR
    '        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
    '            or (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + ToShift + "') ) "
    '                End If
    '                If rbtnNonTaxable.Checked Then
    '                    Qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.is_Taxable=0 and TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
    '                Else
    '                    Qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.is_Taxable=1 and TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
    '                End If
    '                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
    '                    Qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
    '                End If
    '                Qry += "  group by TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.Route_No "
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '                gv1.DataSource = Nothing
    '                gv1.Rows.Clear()
    '                gv1.Columns.Clear()
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    gv1.DataSource = dt
    '                    gv1.GroupDescriptors.Clear()
    '                    gv1.ShowGroupPanel = False
    '                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '                    gv1.EnableFiltering = True
    '                    gv1.AllowAddNewRow = False
    '                    For ii As Integer = 0 To gv1.Columns.Count - 1
    '                        gv1.Columns(ii).ReadOnly = True
    '                        gv1.Columns(ii).IsVisible = True
    '                    Next
    '                    gv1.BestFitColumns()
    '                    btnGo.Enabled = False
    '                    rgbItemType.Enabled = False
    '                    txtLocation.Enabled = False
    '                    txtCustomer.Enabled = False
    '                    txtToDate.Enabled = False
    '                    txtFromDate.Enabled = False
    '                    txtFromShift.Enabled = False
    '                    txtToShift.Enabled = False
    '                Else
    '                    Throw New Exception("Data Not Found!")
    '                End If
    '            Else
    '                Throw New Exception("Invalid Date Range!")
    '            End If
    '        Catch ex As Exception
    '            'clsCommon.ProgressBarPercentHide()
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
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
                            'Dim RetrunDocNO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_Sale_RETURN_head where Against_Invoice_No='" & clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) & "'"))
                            'clsDSSalesReturnHead.PostData(Me.Form_ID, RetrunDocNO)
                        Else
                            Throw New Exception("Already Posted")
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
            Throw New Exception(ex.Message)
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
        (CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_HEAD.Supply_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
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
                        Dim ObjInv As clsPSInvoiceHead = clsMultipleInvoice.ConvertShipmentToSaleInvoice(obj, txtInvoiceDate.Value, True, trans)
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

    Private Sub SaleReturnSaveData(ByVal trans As SqlTransaction)
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
            If gv1.Rows.Count > 0 Then
                For intRow As Integer = 0 To gv1.Rows.Count - 1
                    Dim DocExists As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(Document_Code) from TSPL_SD_Sale_RETURN_head where Against_Invoice_No='" & clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) & "'", trans))
                    Dim isTaxable As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Is_Taxable from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) & "'", trans))
                    If DocExists = 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value)) > 0 Then
                            Dim Qry As String = "select Document_Code from TSPL_SD_SHIPMENT_RETURN_HEAD
            where Customer_Code='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(2).Value) + "' and Route_No='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(4).Value) + "'  
             and TSPL_SD_SHIPMENT_RETURN_HEAD.Status=1 "
                            Qry += " and CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
                                Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')) "
                            ElseIf clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                                Qry += " AND ( 
        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + ToShift + "') ) "
                            ElseIf clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                                Qry += " AND ( (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + FromShift + "') OR
        (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) > '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) < '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')  
            or (CONVERT(date, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date, 103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type = '" + ToShift + "') ) "
                            End If
                            If isTaxable = 0 Then
                                Qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Is_Taxable=0 and TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                            Else
                                Qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Is_Taxable=1 and TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                            End If
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Dim obj As New clsShipmentReturnHead
                                Dim lstDocument As New List(Of String)
                                For Each dr As DataRow In dt.Rows
                                    lstDocument.Add(clsCommon.myCstr(dr("Document_Code")))
                                Next
                                obj = clsMultipleInvoice.GetShipmentReturnHead(lstDocument, trans)
                                Dim ObjInv As clsDSSalesReturnHead = clsMultipleInvoice.ConvertShipmentReutnToSaleReturn(obj, True, trans)
                                Dim status As Boolean = ObjInv.SaveData(ObjInv, True, trans)
                                If status Then
                                    Qry = "update TSPL_SD_Sale_Return_HEAD set Against_Invoice_No='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) + "' where Document_Code='" & ObjInv.Document_Code & "'"
                                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                Else
                                    Throw New Exception("Something went worng while creating invoice!")
                                End If
                                clsDSSalesReturnHead.PostData(Me.Form_ID, ObjInv.Document_Code, trans)
                            Else
                                Throw New Exception("Data not found!")
                            End If

                        End If
                    Else
                        Throw New Exception("Document Already Exists, Against Invoice no [ " & clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) & " ]")
                    End If

                Next
            Else
                Throw New Exception("Data not found!")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub fndLoadData(ByVal strInvoiceNo As String, ByVal NavType As NavigatorType)
        Try
            txtInvoiceNo.Value = strInvoiceNo
            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_NO,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Docuemnt_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as Status,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.total_tax_Amt,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt "
            Dim DeductTPTFromDocAmt As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DeductTPTFromDocAmt, clsFixedParameterCode.DeductTPTFromDocAmt, Nothing)) = 1, True, False)
            If DeductTPTFromDocAmt Then
                strQry += " ,TSPL_SD_SALE_INVOICE_HEAD.TotalSubsidyAmt as TPT_AMT,TSPL_SD_SALE_INVOICE_HEAD.Gross_Amount"
            End If
            strQry += " ,Null As [Tax Type]
from TSPL_SD_SALE_INVOICE_HEAD 
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
 where isMultipleInvoice = 1  "
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
                    gv1.Columns("Tax Type").IsVisible = False
                Next
                gv1.BestFitColumns()
                btnSave.Enabled = False
                btnPost.Enabled = True
                'btnDelete.Visible = True
                'btnDelete.Enabled = True
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
            Dim strQry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Invoice No],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Docuemnt Date],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No],TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location],case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as Status,TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount as [Amount Less Discount],TSPL_SD_SALE_INVOICE_HEAD.total_tax_Amt as [Total Tax Amt],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amt] "
            Dim DeductTPTFromDocAmt As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DeductTPTFromDocAmt, clsFixedParameterCode.DeductTPTFromDocAmt, Nothing)) = 1, True, False)
            If DeductTPTFromDocAmt Then
                strQry += " ,TSPL_SD_SALE_INVOICE_HEAD.TotalSubsidyAmt as TPT_AMT,TSPL_SD_SALE_INVOICE_HEAD.Gross_Amount"
            End If
            strQry +=" ,Case 
When TAX1.Type ='IGST' Then TAX1.Type 
When TAX2.Type ='IGST' Then TAX2.Type 
When TAX3.Type ='IGST' Then TAX3.Type 
When TAX4.Type ='IGST' Then TAX4.Type 
When TAX5.Type ='IGST' Then TAX5.Type 
When TAX6.Type ='IGST' Then TAX6.Type 
When TAX7.Type ='IGST' Then TAX7.Type 
When TAX8.Type ='IGST' Then TAX8.Type 
When TAX9.Type ='IGST' Then TAX9.Type 
When TAX10.Type='IGST' Then TAX10.Type Else Null End As [Tax Type] from TSPL_SD_SALE_INVOICE_HEAD 
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
Left Outer Join TSPL_TAX_MASTER As TAX1 On TAX1.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX1
Left Outer Join TSPL_TAX_MASTER As TAX2 On TAX2.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX2
Left Outer Join TSPL_TAX_MASTER As TAX3 On TAX3.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX3
Left Outer Join TSPL_TAX_MASTER As TAX4 On TAX4.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX4
Left Outer Join TSPL_TAX_MASTER As TAX5 On TAX5.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX5
Left Outer Join TSPL_TAX_MASTER As TAX6 On TAX6.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX6
Left Outer Join TSPL_TAX_MASTER As TAX7 On TAX7.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX7
Left Outer Join TSPL_TAX_MASTER As TAX8 On TAX8.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX8
Left Outer Join TSPL_TAX_MASTER As TAX9 On TAX9.Type=TSPL_SD_SALE_INVOICE_HEAD.TAX9
Left Outer Join TSPL_TAX_MASTER As TAX10 On TAX10.Type=TSPL_SD_SALE_INVOICE_HEAD.Tax10
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
                    gv1.Columns("Tax Type").IsVisible = False
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
        'Try
        '    If common.clsCommon.MyMessageBoxShow(Me, "Do you want to delete ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
        '        For intRow As Integer = 0 To gv1.Rows.Count - 1
        '            If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
        '                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(6).Value), "Approved") = CompairStringResult.Equal Then
        '                    clsPSInvoiceHead.ReverseAndUnpost(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
        '                End If
        '                Dim status As Boolean = clsPSInvoiceHead.DeleteData(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
        '                If status Then
        '                    Dim Qry As String = " update TSPL_SD_SHIPMENT_HEAD set Sale_Invoice_No='' where Sale_Invoice_No =('" + clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value) + "')"
        '                    clsDBFuncationality.ExecuteNonQuery(Qry)
        '                End If
        '            End If
        '        Next
        '        clsCommon.MyMessageBoxShow(Me, "Delete Successfully", Me.Text)
        '        LoadData(False)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub
    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If isInvoice Then
                If gv1.CurrentColumn Is gv1.Columns(0) Then
                    Dim frm As New FrmInvoiceDetails()
                    frm.strInvoiceNo = clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value)
                    frm.strInvoiceDate = clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value)
                    frm.strCustCode = clsCommon.myCstr(gv1.CurrentRow.Cells(2).Value)
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
            Dim objMultPrintInvoice As New FrmPrintFreshInvoice
            Dim fromdate As String = ""
            Dim todate As String = ""
            Dim strInvoiceno As New List(Of String)
            Dim strIgstInv As New List(Of String)
            For intRow As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(intRow).Cells("Tax Type").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells("Tax Type").Value), "IGST") = CompairStringResult.Equal Then
                    strIgstInv.Add(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                Else
                    If clsCommon.myLen(gv1.Rows(intRow).Cells(0).Value) > 0 Then
                        strInvoiceno.Add(clsCommon.myCstr(gv1.Rows(intRow).Cells(0).Value))
                    End If
                End If
            Next

            If strIgstInv IsNot Nothing AndAlso strIgstInv.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Are you sure to print IGST Invoice ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    strInvoiceno = Nothing
                    strInvoiceno = New List(Of String)
                    For Each strInv In strIgstInv
                        strInvoiceno.Add(clsCommon.myCstr(strInv))
                    Next
                End If
            End If


            Dim qry As String = ""
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JSL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrComp_Code1, "NAG") = CompairStringResult.Equal Then
                If strInvoiceno.Count > 0 Then
                    If common.clsCommon.MyMessageBoxShow(" Do you want to print Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                        Dim dtDocdate As Date?
                        dtDocdate = Nothing
                        Dim StrSql As String = Nothing

                        StrSql = "Select Document_Code,Document_Date,Customer_Code,Bill_To_Location,is_taxable,Tax_Group from  TSPL_SD_SALE_INVOICE_HEAD where Document_Code in (" + clsCommon.GetMulcallString(strInvoiceno) + ")"

                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
                        If dt1.Rows.Count > 0 Then
                            'IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
                            dtDocdate = clsCommon.myCDate(dt1.Rows(0)("Document_Date"))
                        End If
                        qry = objMultPrintInvoice.PrintInvoiceForAll(clsCommon.GetMulcallString(strInvoiceno), clsCommon.myCDate(gv1.Rows(0).Cells(1).Value), clsCommon.myCstr(gv1.Rows(0).Cells(2).Value))
                        qry = "select max(XXFinal.Report_Status) as Report_Status, max(XXFinal.PaymentTerms) as PaymentTerms,max(XXFinal.Is_Distributor) as Is_Distributor, max(XXFinal.Is_BPL) as Is_BPL, max(XXFinal.Is_CashSale) as Is_CashSale, max(XXFinal.BPL_Coupon_Code) as BPL_Coupon_Code, max(XXFinal.BPL_Name) as BPL_Name, max(XXFinal.BPL_Remark) as BPL_Remark, max(XXFinal.BPL_Coupon_Date) as BPL_Coupon_Date, max(XXFinal.BPL_Category) as BPL_Category, max(XXFinal.PO_Indent_No) as PO_Indent_No, max(XXFinal.PO_Indent_Date) as PO_Indent_Date, max(XXFinal.Booking_OpeningBal) as Booking_OpeningBal, max(XXFinal.Booking_DrAmt) as Booking_DrAmt, max(XXFinal.Booking_CrAmt) as Booking_CrAmt, max(XXFinal.Booking_ClosingBal) as Booking_ClosingBal, max(XXFinal.Booking_ChequeNo) as Booking_ChequeNo, max(XXFinal.Is_DCS) as Is_DCS, max(XXFinal.Booking_Type) as Booking_Type, max(XXFinal.CST_LST) as CST_LST, max(cast(BarCode_Img as varbinary(max))) as BarCode_Img, max(XXFinal.DocumentTime) as DocumentTime, max(XXFinal.Manual_VehicleNo) as Manual_VehicleNo, max(XXFinal.Payment_Terms) as Payment_Terms, max(XXFinal.ReceiverName) as ReceiverName, sum(XXFinal.Amt_Less_Discount) as Amt_Less_Discount, max(XXFinal.Dispatch_OpeningBal) as Dispatch_OpeningBal, max(XXFinal.Dispatch_DrAmt) as Dispatch_DrAmt, max(XXFinal.Dispatch_CrAmt) as Dispatch_CrAmt, max(XXFinal.Dispatch_ClosingBal) as Dispatch_ClosingBal, max(XXFinal.Security_TotalAmt) as Security_TotalAmt, max(XXFinal.Supply_Date) as Supply_Date, max(XXFinal.Shift_Type) as Shift_Type, sum(XXFinal.QTY_LTRKG) as QTY_LTRKG, max(XXFinal.ITAX1) as ITAX1, max(XXFinal.ITAX1_RATE) as ITAX1_RATE, sum(Floor(XXFinal.ITAX1_Amt*100)/100) as ITAX1_Amt, max(XXFinal.ITAX1_Base_Amt) as ITAX1_Base_Amt, max(XXFinal.ITAX2) as ITAX2, max(XXFinal.ITAX2_RATE) as ITAX2_RATE, sum(Floor(XXFinal.ITAX2_Amt*100)/100) as ITAX2_Amt, sum(XXFinal.ITAX2_Base_Amt) as ITAX2_Base_Amt, max(XXFinal.ITAX3) as ITAX3, max(XXFinal.ITAX3_Rate) as ITAX3_Rate, sum(Floor(XXFinal.ITAX3_Amt*100)/100) as ITAX3_Amt, sum(XXFinal.ITAX3_Base_Amt) as ITAX3_Base_Amt, max(XXFinal.ITAX4) as ITAX4, max(XXFinal.ITAX4_RATE) as ITAX4_RATE, sum(Floor(XXFinal.ITAX4_Amt*100)/100) as ITAX4_Amt, sum(XXFinal.ITAX4_Base_Amt) as ITAX4_Base_Amt, max(XXFinal.ITAX5) as ITAX5, max(XXFinal.ITAX5_RATE) as ITAX5_RATE, sum(Floor(XXFinal.ITAX5_Amt*100)/100) as ITAX5_Amt, sum(XXFinal.ITAX5_Base_Amt) as ITAX5_Base_Amt, max(XXFinal.ITAX6) as ITAX6, max(XXFinal.ITAX6_RATE) as ITAX6_RATE, sum(Floor(XXFinal.ITAX6_Amt*100)/100) as ITAX6_Amt, sum(XXFinal.ITAX6_Base_Amt) as ITAX6_Base_Amt, max(XXFinal.ITAX7) as ITAX7, max(XXFinal.ITAX7_Rate) as ITAX7_Rate, sum(Floor(XXFinal.ITAX7_Amt*100)/100) as ITAX7_Amt, sum(XXFinal.ITAX7_Base_Amt) as ITAX7_Base_Amt, max(XXFinal.ITAX8) as ITAX8, max(XXFinal.ITAX8_RATE) as ITAX8_RATE, sum(Floor(XXFinal.ITAX8_Amt*100)/100) as ITAX8_Amt, sum(XXFinal.ITAX8_Base_Amt) as ITAX8_Base_Amt, max(XXFinal.ITAX9) as ITAX9, max(XXFinal.ITAX9_Rate) as ITAX9_Rate, sum(Floor(XXFinal.ITAX9_Amt*100)/100) as ITAX9_Amt, sum(XXFinal.ITAX9_Base_Amt) as ITAX9_Base_Amt, max(XXFinal.ITAX10) as ITAX10, max(XXFinal.ITAX10_RATE) as ITAX10_RATE, sum(Floor(XXFinal.ITAX10_Amt*100)/100) as ITAX10_Amt, sum(XXFinal.ITAX10_Base_Amt) as ITAX10_Base_Amt, max(XXFinal.IRN_No) as IRN_No, max(XXFinal.Zone_Code) as Zone_Code, max(XXFinal.CF) as CF, max(XXFinal.ConversionFactor) as ConversionFactor, max(XXFinal.EInvoice_Type) as EInvoice_Type, max(XXFinal.LeakageDeduction_Freshsale) as LeakageDeduction_Freshsale, max(XXFinal.LeakageDeduction) as LeakageDeduction, max(XXFinal.SCM) as SCM, max(XXFinal.DIS_MARGIN) as DIS_MARGIN, max(XXFinal.NearestCity) as NearestCity, max(XXFinal.Location_Desc) as Location_Desc, max(XXFinal.Loc_Short_Name) as Loc_Short_Name, max(XXFinal.Loc_Pin) as Loc_Pin, max(XXFinal.Loc_Phone) as Loc_Phone, max(XXFinal.Loc_Eamil) as Loc_Eamil, max(XXFinal.Loc_Website) as Loc_Website, max(XXFinal.ISO_No) as ISO_No, max(XXFinal.Invoice_No) as Invoice_No, max(XXFinal.Invoice_Date) as Invoice_Date, max(XXFinal.Cust_City) as Cust_City, max(XXFinal.Against_Shipment_No) as Against_Shipment_No, max(XXFinal.Cust_Gst_StateCode) as Cust_Gst_StateCode, max(XXFinal.Electronic_Ref_No) as Electronic_Ref_No, max(XXFinal.CustGSTNo) as CustGSTNo, max(XXFinal.Area_Code) as Area_Code, max(XXFinal.gst_state_code) as gst_state_code, max(XXFinal.LocGstNo) as LocGstNo, max(XXFinal.EWayBillNo) as EWayBillNo, max(XXFinal.EWayBillDate) as EWayBillDate, max(XXFinal.HSN_Code) as HSN_Code, max(XXFinal.InvRemarks) as InvRemarks, max(XXFinal.Delivery_Code) as Delivery_Code, max(XXFinal.Conversion_factor) as Conversion_factor, sum(XXFinal.QTY_Box) as QTY_Box, max(XXFinal.Sale_Invoice_No) as Sale_Invoice_No, max(XXFinal.vehicleNo) as vehicleNo, max(XXFinal.Sale_Invoice_Date) as Sale_Invoice_Date, sum(XXFinal.RoundOffAmount) as RoundOffAmount, sum(XXFinal.Total_Amt) as Total_Amt, max(XXFinal.Loc_ADd1) as Loc_ADd1, max(XXFinal.LOC_ADD2) as LOC_ADD2, max(XXFinal.LOC_ADD3) as LOC_ADD3, max(XXFinal.LocationState) as LocationState, max(XXFinal.LOCPhone) as LOCPhone, max(XXFinal.Loc_TIN_NO) as Loc_TIN_NO, XXFinal.Document_Code as Document_Code, max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Description) as Description, max(XXFinal.Lorry_No) as Lorry_No, max(XXFinal.Sku_Seq) as Sku_Seq, XXFinal.Item_Code as Item_Code, max(XXFinal.Line_No) as Line_No, max(XXFinal.Item_Desc) as Item_Desc, sum(XXFinal.QtyCrates) as QtyCrates, max(XXFinal.ConvFactInCrate) as ConvFactInCrate, max(XXFinal.ConvQtyInCrate) as ConvQtyInCrate, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Qty_Default) as Qty_Default, max(XXFinal.Rate_Default) as Rate_Default, sum(XXFinal.QtyPCS) as QtyPCS, sum(XXFinal.free_qty) as free_qty, max(XXFinal.FreeSchemeInLitres) as FreeSchemeInLitres, max(XXFinal.RatePerPcs) as RatePerPcs, sum(XXFinal.valueInRs) as valueInRs, max(XXFinal.comp_add2) as comp_add2, max(XXFinal.comp_add3) as comp_add3, max(XXFinal.CompPhone) as CompPhone, max(XXFinal.Cash_Scheme_Amount) as Cash_Scheme_Amount, sum(XXFinal.schemeInCrates) as schemeInCrates, max(XXFinal.GrandTotalCrates) as GrandTotalCrates, max(XXFinal.Comp_Code) as Comp_Code, max(XXFinal.Comp_Name) as Comp_Name, max(XXFinal.comp_add1) as comp_add1, max(XXFinal.comp_Fax) as comp_Fax, max(XXFinal.comp_Email) as comp_Email, max(XXFinal.comp_tinNo) as comp_tinNo, max(XXFinal.cust_Code) as cust_Code, max(XXFinal.Customer_Name) as Customer_Name, max(XXFinal.cust_add1) as cust_add1, max(XXFinal.cust_add2) as cust_add2, max(XXFinal.cust_add3) as cust_add3, max(XXFinal.CustPhone) as CustPhone, max(XXFinal.cust_fax) as cust_fax, max(XXFinal.Cust_state) as Cust_state, max(XXFinal.cust_Statename) as cust_Statename, max(XXFinal.cust_Email) as cust_Email, max(XXFinal.cust_website) as cust_website, max(XXFinal.Customer_Pan) as Customer_Pan, max(XXFinal.Ack_No) as Ack_No, max(XXFinal.Ack_Date) as Ack_Date, max(XXFinal.TaxableNonTaxable) as TaxableNonTaxable, max(XXFinal.TAX1) as TAX1, max(XXFinal.TaxType1) as TaxType1, sum(Floor(XXFinal.TAX1_Amt *100)/100) as TAX1_Amt, max(XXFinal.TAX1_Rate) as TAX1_Rate, sum(Floor(XXFinal.TAX1Amt*100)/100) as TAX1Amt, max(XXFinal.TaxType2) as TaxType2, max(XXFinal.TAX2) as TAX2, max(Floor(XXFinal.TAX2_Amt*100)/100) as TAX2_Amt, max(XXFinal.TAX2_Rate) as TAX2_Rate, max(Floor(XXFinal.TAX2Amt*100)/100) as TAX2Amt, max(XXFinal.TaxType3) as TaxType3, max(XXFinal.TAX3) as TAX3, max(Floor(XXFinal.TAX3_Amt*100)/100) as TAX3_Amt, max(XXFinal.TAX3_Rate) as TAX3_Rate, max(Floor(XXFinal.TAX3Amt*100)/100) as TAX3Amt, max(XXFinal.TaxType4) as TaxType4, max(XXFinal.TAX4) as TAX4, max(Floor(XXFinal.TAX4_Amt*100)/100) as TAX4_Amt, max(XXFinal.TAX4_Rate) as TAX4_Rate, max(Floor(XXFinal.TAX4Amt*100)/100) as TAX4Amt, max(XXFinal.TaxType5) as TaxType5, max(XXFinal.TAX5) as TAX5, max(Floor(XXFinal.TAX5_Amt*100)/100) as TAX5_Amt, max(XXFinal.TaxType6) as TaxType6, max(XXFinal.TAX6) as TAX6, max(Floor(XXFinal.TAX6_Amt*100)/100) as TAX6_Amt, max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc, sum(XXFinal.Distributor_Commission_TotalAmt) as Distributor_Commission_TotalAmt, sum(XXFinal.Transporter_Commission_TotalAmt) as Transporter_Commission_TotalAmt, max(XXFinal.Transport_Id) as Transport_Id, max(XXFinal.Transporter_Name) as Transporter_Name, max(XXFinal.Against_Delivery_Code) as Against_Delivery_Code, max(XXFinal.batchNO) as batchNO, max(XXFinal.Credit_Customer) as Credit_Customer, max(XXFinal.Ship_To_Code) as Ship_To_Code, max(XXFinal.Ship_To_Desc) as Ship_To_Desc, max(XXFinal.Ship_Address) as Ship_Address, max(XXFinal.Ship_City) as Ship_City, max(XXFinal.Ship_State) as Ship_State, max(XXFinal.Ship_Pin_Code) as Ship_Pin_Code, max(XXFinal.Ship_PAN) as Ship_PAN, max(XXFinal.Ship_GSTNO) as Ship_GSTNO, sum(XXFinal.Booth_Security_Amt) as Booth_Security_Amt, max(XXFinal.Billing_Unit_code) as Billing_Unit_code, sum(XXFinal.Billing_Qty) as Billing_Qty, max(XXFinal.BulkCF) as BulkCF, sum(XXFinal.Total_Basic_Amt) as Total_Basic_Amt, max(XXFinal.Brand) as Brand, max(XXFinal.BRANDDESC) as BRANDDESC, max(XXFinal.Particulars) as Particulars, max(XXFinal.Crate_No) as Crate_No, max(cast(Logo_Img as varbinary(max))) as Logo_Img, max(XXFinal.CopyType) as CopyType, max(XXFinal.SellerGST) as SellerGST, max(XXFinal.Pan_No) as Pan_No, max(XXFinal.Bank_Name) as Bank_Name, max(XXFinal.BankAccountNo) as BankAccountNo, max(XXFinal.BankBranchAddress) as BankBranchAddress, max(XXFinal.BankIFSCCode) as BankIFSCCode, max(XXFinal.Tcan_No) as Tcan_No, max(XXFinal.RateLtr) as RateLtr, max(XXFinal.Company_Name) as Company_Name, max(XXFinal.Address2) as Address2, max(XXFinal.Regn_No) as Regn_No, max(XXFinal.FSSAI_NO) as FSSAI_NO, max(XXFinal.Receipt_No) as Receipt_No, max(XXFinal.Receipt_Date) as Receipt_Date, sum(XXFinal.Receipt_Amount) as Receipt_Amount, max(XXFinal.Payment_Code) as Payment_Code, max(XXFinal.cheque_No) as cheque_No, max(XXFinal.Cheque_Date) as Cheque_Date, max(XXFinal.OpeningBal) as OpeningBal, max(XXFinal.ClosingBal) as ClosingBal,max(XXFinal.ismultipleInvoice) as ismultipleInvoice from ( " + qry + "   ) XXFinal   group by XXFinal.Item_Code,XXFinal.Document_Code "


                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JSL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrComp_Code1, "NAG") = CompairStringResult.Equal Then
                            'If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX7")) = "IGST"  Then
                            If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" Then
                                frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceIGST", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            Else
                                frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceGNG1", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            End If                    '


                            'Else
                            '    filePath = frmCRV.funsubreportWithdt(MyBase.Form_ID, isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoice", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                        End If
                        'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoice", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                    Else
                        frmCRV = Nothing

                    End If
                Else
                    Throw New Exception("Invoice not found")
                End If
            Else
                If strInvoiceno.Count > 0 Then
                    If common.clsCommon.MyMessageBoxShow(" Do you want to print Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                        qry = "select  '" & objCommonVar.CurrentUser & "' As PrintBy,TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.Access_Officer as FassiLicNo,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3 as CompAddress,TSPL_COMPANY_MASTER.Phone1,xxx.*
from(
select xx.Document_Code,max(xx.Document_Date) as BillDate,max(xx.Route_No)as Route_No,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,
Sum(Case When xx.QtyInLtr IS Null Then xx.QtyInKG Else xx.QtyInLtr end) as QtyInLtr,
Sum(Case When xx.QtyInPouch IS Null Then xx.QtyInPack Else xx.QtyInPouch end) as QtyInPouch,
Max(Case When xx.Rate_Per_Pouch IS Null Then xx.Rate_Per_Pack Else xx.Rate_Per_Pouch end) as Rate_Per_Pouch,
sum(xx.Item_Net_Amt) as Item_Net_Amt,max(xx.Customer_Name) as Customer_Name,max(xx.Add1) as Add1,max(xx.Add2)as Add2,max(xx.add3) as Add3,
max(xx.GST_STATE_Code) as GST_STATE_Code,max(xx.STATE_NAME) as STATE_NAME,max(xx.GSTNO)as GSTNO,max(xx.Transporter_Commission_TotalAmt)as TCAmt,min(xx.GPDate) as GPFromDate,max(xx.GPDate) as GPTodate
,STUFF(( SELECT DISTINCT ',' + GPCode FROM (SELECT DISTINCT RIGHT(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode, 6) AS GPCode FROM TSPL_SD_SALE_INVOICE_DETAIL
              LEFT JOIN TSPL_SD_SHIPMENT_DETAIL ON TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code
              LEFT JOIN TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL ON TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID = TSPL_SD_SHIPMENT_DETAIL.PK_ID
              WHERE TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = xx.Document_Code ) AS subquery
        FOR XML PATH('')), 1, 1, '') AS GPCodes
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,
((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,
((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,
((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInKG.Conversion_Factor) as QtyInKG,
((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPACK.Conversion_Factor) as QtyInPack,
TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,
(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)) as Rate_Per_Pouch,
(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPACK.Conversion_Factor)) as Rate_Per_Pack,
CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR,
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
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='PACK') as ItemConversionInPACK on ItemConversionInPACK.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code  and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code 
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
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSaleReturn_Click(sender As Object, e As EventArgs) Handles btnSaleReturn.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaleReturnSaveData(trans)
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Return Saved Successfully", Me.Text)
            'LoadData(False)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
