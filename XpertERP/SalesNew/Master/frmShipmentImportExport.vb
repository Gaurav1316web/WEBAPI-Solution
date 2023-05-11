'==========Update by Rohit July 3,2014 on 11:21 (remark : Add Code For user Permission.If user have Permission to Save this Form Then User can Import and Save Excel ELse not.)======
Imports common
Imports System.Data.SqlClient

Public Class frmShipmentImportExport
    Inherits FrmMainTranScreen

#Region "Variables"
    Public Podate As DateTime = Nothing
    Public Form_38_No As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing 'Not a table field
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As DateTime? = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Carrier As String = Nothing
    Public VehicleNo As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Add_Charge_Code1 As String = Nothing
    Public Add_Charge_Name1 As String = Nothing
    Public Add_Charge_Amt1 As Double = 0
    Public Add_Charge_Code2 As String = Nothing
    Public Add_Charge_Name2 As String = Nothing
    Public Add_Charge_Amt2 As Double = 0
    Public Add_Charge_Code3 As String = Nothing
    Public Add_Charge_Name3 As String = Nothing
    Public Add_Charge_Amt3 As Double = 0
    Public Add_Charge_Code4 As String = Nothing
    Public Add_Charge_Name4 As String = Nothing
    Public Add_Charge_Amt4 As Double = 0
    Public Add_Charge_Code5 As String = Nothing
    Public Add_Charge_Name5 As String = Nothing
    Public Add_Charge_Amt5 As Double = 0
    Public Add_Charge_Code6 As String = Nothing
    Public Add_Charge_Name6 As String = Nothing
    Public Add_Charge_Amt6 As Double = 0
    Public Add_Charge_Code7 As String = Nothing
    Public Add_Charge_Name7 As String = Nothing
    Public Add_Charge_Amt7 As Double = 0
    Public Add_Charge_Code8 As String = Nothing
    Public Add_Charge_Name8 As String = Nothing
    Public Add_Charge_Amt8 As Double = 0
    Public Add_Charge_Code9 As String = Nothing
    Public Add_Charge_Name9 As String = Nothing
    Public Add_Charge_Amt9 As Double = 0
    Public Add_Charge_Code10 As String = Nothing
    Public Add_Charge_Name10 As String = Nothing
    Public Add_Charge_Amt10 As Double = 0
    Public Total_Add_Charge As Double = 0
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_Date As DateTime? = Nothing
    Public Inv_No As String = Nothing
    Public Inv_Date As DateTime? = Nothing
    Public Against_Sales_Order As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType

    Public Is_Create_Auto_Invoice As Boolean = False
    Public Sale_Invoice_No As String = Nothing
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Against_POS As String = Nothing

    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsSNShipmentDetail) = Nothing

    Public strForm_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing

    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public TotCashDiscAmt As Double = 0

    Public Mannual_Invoice_No As Integer = 0
    Dim DtHead, DtDetail As New DataTable
    Public arrList As New ArrayList


    '===================================Detail variables======================
    Public Document_Detail_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Bar_Code As String = Nothing
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Free_Qty As Double = 0
    Public Order_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public strLocation As String = Nothing
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0
    Public TAX1_Detail As String = Nothing
    Public TAX1_Base_Amt_Detail As Double = 0
    Public TAX1_Rate_Detail As Double = 0
    Public TAX1_Amt_Detail As Double = 0
    Public TAX2_Detail As String = Nothing
    Public TAX2_Base_Amt_Detail As Double = 0
    Public TAX2_Rate_Detail As Double = 0
    Public TAX2_Amt_Detail As Double = 0
    Public TAX3_Detail As String = Nothing
    Public TAX3_Base_Amt_Detail As Double = 0
    Public TAX3_Rate_Detail As Double = 0
    Public TAX3_Amt_Detail As Double = 0
    Public TAX4_Detail As String = Nothing
    Public TAX4_Base_Amt_Detail As Double = 0
    Public TAX4_Rate_Detail As Double = 0
    Public TAX4_Amt_Detail As Double = 0
    Public TAX5_Detail As String = Nothing
    Public TAX5_Base_Amt_Detail As Double = 0
    Public TAX5_Rate_Detail As Double = 0
    Public TAX5_Amt_Detail As Double = 0
    Public TAX6_Detail As String = Nothing
    Public TAX6_Base_Amt_Detail As Double = 0
    Public TAX6_Rate_Detail As Double = 0
    Public TAX6_Amt_Detail As Double = 0
    Public TAX7_Detail As String = Nothing
    Public TAX7_Base_Amt_Detail As Double = 0
    Public TAX7_Rate_Detail As Double = 0
    Public TAX7_Amt_Detail As Double = 0
    Public TAX8_Detail As String = Nothing
    Public TAX8_Base_Amt_Detail As Double = 0
    Public TAX8_Rate_Detail As Double = 0
    Public TAX8_Amt_Detail As Double = 0
    Public TAX9_Detail As String = Nothing
    Public TAX9_Base_Amt_Detail As Double = 0
    Public TAX9_Rate_Detail As Double = 0
    Public TAX9_Amt_Detail As Double = 0
    Public TAX10_Detail As String = Nothing
    Public TAX10_Base_Amt_Detail As Double = 0
    Public TAX10_Rate_Detail As Double = 0
    Public TAX10_Amt_Detail As Double = 0
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt_Detail As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Status_detail As Integer = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks_detail As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Is_Mannual_Amt As Integer = Nothing
    'Public Unit_Cogs As Double = 0
    Public SRNTax_Group As String = Nothing 'Not a Table Field

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing


    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public Price_Amount1 As Double = 0
    Public Price_Amount2 As Double = 0
    Public Price_Amount3 As Double = 0
    Public Price_Amount4 As Double = 0
    Public Price_Amount5 As Double = 0
    Public Price_Amount6 As Double = 0
    Public Price_Amount7 As Double = 0
    Public Price_Amount8 As Double = 0
    Public Price_Amount9 As Double = 0
    Public Price_Amount10 As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public FOC_Item As Double = 0
    Public Price_Date As String = Nothing

    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Markup_On As String = Nothing
    Public Markup_Percent As Double = 0
    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public Bin_No As String = Nothing
    '======================================================================
#End Region
    Private Sub HeadImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetailImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "S No", "Document Date", "Customer Code", "Tax Group", "Bill To Location", "Salesman Code") Then

            Try
                gvHead.DataSource = Nothing
                gvHead.Columns.Clear()
                If Not IsNothing(DtHead) Then
                    DtHead.Columns.Clear()
                    DtHead.Rows.Clear()
                End If
                For Each col As GridViewColumn In gv.Columns
                    DtHead.Columns.Add(col.Name.ToString)
                Next
                For Each row As GridViewRowInfo In gv.Rows
                    Dim dr As DataRow = DtHead.NewRow()
                    For Each col As GridViewColumn In gv.Columns
                        dr(col.Name.ToString) = row.Cells(col.Name.ToString).Value
                    Next
                    DtHead.Rows.Add(dr)
                    DtHead.AcceptChanges()
                Next
                DtHead.Columns.Add("Error", GetType(String))
                If Not DtHead.Columns.Contains("Document Code") Then
                    DtHead.Columns.Add("Document Code", GetType(String))
                End If
                gvHead.DataSource = DtHead.DefaultView
                For Each col As GridViewColumn In gvHead.Columns
                    'DtHead.Columns.Add(col.Name.ToString)
                    col.Width = 100
                Next
                'clsCommon.ProgressBarShow()
                'For Each grow As GridViewRowInfo In gv.Rows
                '    Dim obj As New ClsRegionMaster()

                '    Dim strDocumentDate As String = clsCommon.myCstr(grow.Cells(0).Value)
                '    If (String.IsNullOrEmpty(strDocumentDate)) Then 'strDocumentDate.Length > 30 Or
                '        Throw New Exception("Code can not be blank or incorrect.")
                '    End If
                '    obj.code = strDocumentDate

                '    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                '    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                '        Throw New Exception("Name can not be blank or incorrect.")
                '    End If
                '    obj.name = strName

                '    obj.SaveData(obj, obj.code)
                'Next
                'clsCommon.ProgressBarHide()
                'common.clsCommon.MyMessageBoxShow("Data Upload Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        ' Me.Controls.Remove(gv)
    End Sub

    Private Sub DetailImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeadImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "S No", "Item Code", "Quantity", "UOM", "Location", "Item Cost", "MRP") Then

            Try
                gvDetail.DataSource = Nothing
                gvDetail.Columns.Clear()

                If Not IsNothing(DtDetail) Then
                    DtDetail.Columns.Clear()
                    DtDetail.Rows.Clear()
                End If
                For Each col As GridViewColumn In gv.Columns
                    DtDetail.Columns.Add(col.Name.ToString)
                Next
                For Each row As GridViewRowInfo In gv.Rows
                    Dim dr As DataRow = DtDetail.NewRow()
                    For Each col As GridViewColumn In gv.Columns
                        dr(col.Name.ToString) = row.Cells(col.Name.ToString).Value
                    Next
                    DtDetail.Rows.Add(dr)
                    DtDetail.AcceptChanges()
                Next
                DtDetail.Columns.Add("Error", GetType(String))
                If Not DtDetail.Columns.Contains("Document Code") Then
                    DtDetail.Columns.Add("Document Code", GetType(String))
                End If
                gvDetail.DataSource = DtDetail
                For Each col As GridViewColumn In gvDetail.Columns
                    'DtDetail.Columns.Add(col.Name.ToString)
                    col.Width = 100
                Next
                'clsCommon.ProgressBarShow()
                'For Each grow As GridViewRowInfo In gv.Rows
                '    Dim obj As New ClsRegionMaster()


                '    Dim strDocumentDate As String = clsCommon.myCstr(grow.Cells(0).Value)
                '    If (String.IsNullOrEmpty(strDocumentDate)) Then 'strDocumentDate.Length > 30 Or
                '        Throw New Exception("Code can not be blank or incorrect.")
                '    End If
                '    obj.code = strDocumentDate

                '    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                '    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                '        Throw New Exception("Name can not be blank or incorrect.")
                '    End If
                '    obj.name = strName

                '    obj.SaveData(obj, obj.code)
                'Next
                'clsCommon.ProgressBarHide()
                'common.clsCommon.MyMessageBoxShow("Data Upload Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        ' Me.Controls.Remove(gv)
    End Sub

    Private Sub frmShipmentImportExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            btnSave.Enabled = False
            BtnExportDetail.Enabled = False
            BtnExportHead.Enabled = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSNShipmentImportExport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDetailImport.Visible = MyBase.isModifyFlag
        HeadImport.Visible = MyBase.isModifyFlag
    End Sub

    Public Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If DtHead.Select("Error=''").Length <= 0 Then
        '    clsCommon.MyMessageBoxShow("No Data to Save", "Save Data", MessageBoxButtons.OK, RadMessageIcon.Question)
        '    Exit Sub
        'End If
        For Each row As GridViewRowInfo In gvHead.Rows()
            If row.Cells("Error").Value = "" And String.IsNullOrEmpty(row.Cells("Document Code").Value.ToString) Then
                Dim frm As New frmSNShipment
                frm.gvExcel = gvHead
                frm.row_index = row.Index
                frm.IsDataImported = True
                frm.DtExcel = DtDetail
                'frm.LoadImportData(gvHead, row.Index)
                frm.ShowDialog()
                row.Cells("Document Code").Value = frm.DocumentNo
                For Each rowdetail As DataRow In DtDetail.Select("[S No]=" & row.Cells("S No").Value)
                    rowdetail("Document Code") = frm.DocumentNo
                Next
            End If
        Next
        clsCommon.MyMessageBoxShow("Data Saved Successfully", "ERP", MessageBoxButtons.OK, RadMessageIcon.Info)
        'gvHead.DataSource = Nothing
        'gvDetail.DataSource = Nothing
    End Sub

    Public Sub SaveData(ByVal gv As RadGridView, ByVal rowindex As Integer, ByVal DtExcel As DataTable)
        '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        For ix As Integer = rowindex To rowindex 'gv.Rows.Count - 1
            ' Dim obj = New frmShipmentImportExport()
            Document_Code = clsCommon.myCstr(gv.Rows(ix).Cells("Document Code").Value)
            Document_Date = clsCommon.myCDate(gv.Rows(ix).Cells("Document Date").Value)
            Customer_Code = clsCommon.myCstr(gv.Rows(ix).Cells("Customer Code").Value)
            Customer_Name = clsDBFuncationality.getSingleValue("Select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_code='" & Customer_Code & "' ")
            Status = 0
            On_Hold = False
            Is_Internal = False
            Ref_No = ""
            Description = clsCommon.myCstr(gv.Rows(ix).Cells("Description").Value)
            Remarks = clsCommon.myCstr(gv.Rows(ix).Cells("Remarks").Value)
            Bill_To_Location = clsCommon.myCstr(gv.Rows(ix).Cells("Bill To Location").Value)
            Ship_To_Location = clsCommon.myCstr(gv.Rows(ix).Cells("Ship To Location").Value)
            Tax_Group = clsCommon.myCstr(gv.Rows(ix).Cells("Tax Group").Value)
            TAX1 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX1").Value)
            TAX1_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX1_Rate").Value)
            TAX1_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX1_Base_Amt").Value)
            TAX1_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX1_Amt").Value)
            TAX2 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX2").Value)
            TAX2_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX2_Rate").Value)
            TAX2_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX2_Base_Amt").Value)
            TAX2_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX2_Amt").Value)
            TAX3 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX3").Value)
            TAX3_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX3_Base_Amt").Value)
            TAX3_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX3_Rate").Value)
            TAX3_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX3_Amt").Value)
            TAX4 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX4").Value)
            TAX4_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX4_Rate").Value)
            TAX4_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX4_Base_Amt").Value)
            TAX4_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX4_Amt").Value)
            TAX5 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX5").Value)
            TAX5_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX5_Rate").Value)
            TAX5_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX5_Base_Amt").Value)
            TAX5_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX5_Amt").Value)
            TAX6 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX6").Value)
            TAX6_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX6_Rate").Value)
            TAX6_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX6_Base_Amt").Value)
            TAX6_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX6_Amt").Value)
            TAX7 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX7").Value)
            TAX7_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX7_Rate").Value)
            TAX7_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX7_Base_Amt").Value)
            TAX7_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX7_Amt").Value)
            TAX8 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX8").Value)
            TAX8_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX8_Rate").Value)
            TAX8_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX8_Base_Amt").Value)
            TAX8_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX8_Amt").Value)
            TAX9 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX9").Value)
            TAX9_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX9_Rate").Value)
            TAX9_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX9_Base_Amt").Value)
            TAX9_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX9_Amt").Value)
            TAX10 = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("TAX10").Value)
            TAX10_Rate = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX10_Rate").Value)
            TAX10_Base_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX10_Base_Amt").Value)
            TAX10_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("TAX10_Amt").Value)
            Total_Tax_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Total_Tax_Amt").Value)
            Discount_Base = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Discount_Base").Value)
            Discount_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Discount_Amt").Value)
            Amount_Less_Discount = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Amount_Less_Discount").Value)
            Total_Amt = 0.0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Total_Amt").Value)
            Comments = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("Comments").Value)
            Comp_Code = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("Comp_Code").Value)
            Terms_Code = 0.0 'clsCommon.myCstr(gv.Rows(ix).Cells("Terms_Code").Value)
            PROJECT_ID = clsCommon.myCstr(gv.Rows(ix).Cells("PROJECT ID").Value)

            'If gv.Rows(ix).Cells("Due_Date") IsNot DBNull.Value Then
            Due_Date = Today.Date 'clsCommon.myCstr(gv.Rows(ix).Cells("Due_Date").Value)
            'End If




            BillToLocationName = clsDBFuncationality.getSingleValue("Select Location_desc  from TSPL_LOcation_Master where Location_code='" & Bill_To_Location & "' ")
            ShipToLocationName = clsDBFuncationality.getSingleValue("Select Location_desc  from TSPL_LOcation_Master where Location_code='" & Ship_To_Location & "' ")
            TaxGroupName = clsDBFuncationality.getSingleValue("Select Tax_Group_desc  from Tspl_Tax_Group_Master where Tax_Group_code='" & Tax_Group & "' ")
            TermsName = ""

            'If gv.Rows(ix).Cells("Posting_Date") IsNot DBNull.Value Then
            Posting_Date = Nothing
            'End If


            Challan_No = Nothing 'clsCommon.myCdbl(gv.Rows(ix).Cells("Challan_No").Value)
            Carrier = clsCommon.myCstr(gv.Rows(ix).Cells("Carrier").Value)
            VehicleNo = clsCommon.myCstr(gv.Rows(ix).Cells("VehicleNo").Value)
            Vehicle_Code = clsCommon.myCstr(gv.Rows(ix).Cells("Vehicle Code").Value)
            GRNo = clsCommon.myCstr(gv.Rows(ix).Cells("GRNo").Value)
            GENo = clsCommon.myCstr(gv.Rows(ix).Cells("GENo").Value)
            If String.IsNullOrEmpty(gv.Rows(ix).Cells("GEDate").ToString) Then
                GEDate = clsCommon.myCDate(gv.Rows(ix).Cells("GEDate").Value)
            End If




            Dept = clsCommon.myCstr(gv.Rows(ix).Cells("Dept").Value)
            Dept_Desc = clsDBFuncationality.getSingleValue("Select Department_name  from TSPL_Department_Master where Department_code='" & Dept & "' ")
            Item_Type = "Item"

            Against_Sales_Order = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Against_Sales_Order").Value)


            Add_Charge_Code1 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code1").Value)
            Add_Charge_Name1 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code1").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name1").Value)
            Add_Charge_Amt1 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt1").Value)

            Add_Charge_Code2 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code2").Value)
            Add_Charge_Name2 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code2").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name2").Value)
            Add_Charge_Amt2 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt2").Value)

            Add_Charge_Code3 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code3").Value)
            Add_Charge_Name3 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code3").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name3").Value)
            Add_Charge_Amt3 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt3").Value)

            Add_Charge_Code4 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code4").Value)
            Add_Charge_Name4 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code4").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name4").Value)
            Add_Charge_Amt4 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt4").Value)

            Add_Charge_Code5 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code5").Value)
            Add_Charge_Name5 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code5").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name5").Value)
            Add_Charge_Amt5 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt5").Value)

            Add_Charge_Code6 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code6").Value)
            Add_Charge_Name6 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code6").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name6").Value)
            Add_Charge_Amt6 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt6").Value)

            Add_Charge_Code7 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code7").Value)
            Add_Charge_Name7 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code7").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name7").Value)
            Add_Charge_Amt7 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt7").Value)

            Add_Charge_Code8 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code8").Value)
            Add_Charge_Name8 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code8").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name8").Value)
            Add_Charge_Amt8 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt8").Value)

            Add_Charge_Code9 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code9").Value)
            Add_Charge_Name9 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code9").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name9").Value)
            Add_Charge_Amt9 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt9").Value)

            Add_Charge_Code10 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code10").Value)
            Add_Charge_Name10 = clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Code10").Value) 'clsCommon.myCstr(gv.Rows(ix).Cells("Add_Charge_Name10").Value)
            Add_Charge_Amt10 = clsCommon.myCdbl(gv.Rows(ix).Cells("Add_Charge_Amt10").Value)

            Total_Add_Charge = 0 'clsCommon.myCdbl(gv.Rows(ix).Cells("Total_Add_Charge").Value)
            Inv_No = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Inv_No").Value)
            'If gv.Rows(ix).Cells("Challan_Date") IsNot DBNull.Value Then

            Challan_Date = Nothing 'clsCommon.GetPrintDate((gv.Rows(ix).Cells("Challan_Date").Value), "dd/MMM/yyyy")
            'End If

            'If gv.Rows(ix).Cells("Inv_Date") IsNot DBNull.Value Then
            Inv_Date = Nothing 'clsCommon.GetPrintDate((gv.Rows(ix).Cells("Inv_Date").Value), "dd/MMM/yyyy")
            ' End If

            Tax_Calculation_Type = EnumTaxCalucationType.Automatic 'IIf(clsCommon.myCdbl(gv.Rows(ix).Cells("Tax_Calculation_Type").Value) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            Is_Create_Auto_Invoice = False 'IIf(clsCommon.myCdbl(gv.Rows(ix).Cells("Is_Create_Auto_Invoice").Value) = 1, True, False)
            Sale_Invoice_No = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Sale_Invoice_No").Value)
            Is_Create_Auto_Receipt = False 'IIf(clsCommon.myCdbl(gv.Rows(ix).Cells("Is_Create_Auto_Receipt").Value) = 1, True, False)

            Salesman_Code = clsCommon.myCstr(gv.Rows(ix).Cells("Salesman Code").Value)
            Salesman_Name = clsCommon.myCstr(gv.Rows(ix).Cells("Salesman Name").Value)
            Price_Code = clsCommon.myCstr(gv.Rows(ix).Cells("Price Code").Value)
            Route_No = clsCommon.myCstr(gv.Rows(ix).Cells("Route No").Value)
            Route_Desc = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Route_Desc").Value)
            HeadDisc_Per = Nothing 'clsCommon.myCdbl(gv.Rows(ix).Cells("HeadDisc_Per").Value)
            HeadDisc_Amt = Nothing 'clsCommon.myCdbl(gv.Rows(ix).Cells("HeadDisc_Amt").Value)
            TotCashDiscAmt = Nothing 'clsCommon.myCdbl(gv.Rows(ix).Cells("TotCashDiscAmt").Value)
            Invoice_Type = Nothing ' clsCommon.myCstr(gv.Rows(ix).Cells("Invoice_Type").Value)
            Price_Group_Code = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Price_Group_Code").Value)
            Cust_PO_No = Nothing ' clsCommon.myCstr(gv.Rows(ix).Cells("Cust_PO_No").Value)
            'If gv.Rows(ix).Cells("cust_po_date") IsNot Nothing Then
            Podate = Nothing 'clsCommon.myCDate(gv.Rows(ix).Cells("cust_po_date").Value)
            'End If
            Form_38_No = Nothing 'clsCommon.myCstr(gv.Rows(ix).Cells("Form_38_No").Value)
            Mannual_Invoice_No = Nothing 'clsCommon.myCdbl(gv.Rows(ix).Cells("Mannual_Invoice_No").Value)
            '' CURRENCYCONVERSION 
            CURRENCY_CODE = Nothing ' clsCommon.myCstr(gv.Rows(ix).Cells("CURRENCY_CODE").Value)
            ConvRate = Nothing ''clsCommon.myCdbl(gv.Rows(ix).Cells("ConvRate").Value)
            'If IsDBNull(gv.Rows(ix).Cells("ApplicableFrom").Value) = True Then
            ApplicableFrom = Nothing
            'Else
            'ApplicableFrom = clsCommon.GetPrintDate(gv.Rows(ix).Cells("ApplicableFrom").Value, "dd/MMM/yyyy")
            'End If
            '' END CURRENCYCONVERSION 
            Invoice_No = Nothing 'clsDBFuncationality.getSingleValue("Select Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" & Document_Code & "' ")
            'If Not IsNothing(Document_Code) Then
            '    If (DtExcel.Select("[Document Code]='" & Document_Code & "'").Length > 0) Then
            '        Dim ObjTr As New frmShipmentImportExport
            '        For Each dr As DataRow In DtExcel.Select("[Document Code]='" & Document_Code & "'")
            '            ObjTr.Document_Code = clsCommon.myCstr(dr("Document Code"))
            '            ObjTr.Row_Type = "Item"
            '            ObjTr.Line_No = Nothing
            '            ObjTr.Status_detail = Nothing
            '            ObjTr.Item_Code = clsCommon.myCstr(dr("Item Code"))
            '            ObjTr.Bar_Code = Nothing
            '            ObjTr.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_desc  from TSPL_ITEM_MASTER where Item_Code='" & ObjTr.Item_Code & "' ") 'clsCommon.myCstr(dr("Item_Desc"))
            '            ObjTr.Qty = clsCommon.myCdbl(dr("Quantity"))


            '            ObjTr.Free_Qty = Nothing 'clsCommon.myCdbl(dr("Free_Qty"))
            '            ObjTr.Order_Code = Nothing 'clsCommon.myCstr(dr("Order_Code"))

            '            ObjTr.Balance_Qty = clsCommon.myCdbl(dr("Quantity")) ' clsCommon.myCdbl(dr("Balance_Qty"))
            '            ObjTr.Unit_code = clsCommon.myCstr(dr("UOM")) 'clsCommon.myCstr(dr("Unit_code"))
            '            ObjTr.Location = clsCommon.myCstr(dr("Location"))
            '            ObjTr.LocationName = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_Location_MASTER where Location_Code='" & ObjTr.Location & "' ") 'clsCommon.myCstr(dr("LocationName"))
            '            ObjTr.Item_Cost = clsCommon.myCdbl(dr("Item Cost"))
            '            ObjTr.TAX1 = Nothing 'clsCommon.myCstr(dr("TAX1"))
            '            ObjTr.TAX1_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX1_Base_Amt"))
            '            ObjTr.TAX1_Rate = Nothing 'clsCommon.myCdbl(dr("TAX1_Rate"))
            '            ObjTr.TAX1_Amt = Nothing 'clsCommon.myCdbl(dr("TAX1_Amt"))
            '            ObjTr.TAX2 = Nothing 'clsCommon.myCstr(dr("TAX2"))
            '            ObjTr.TAX2_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX2_Base_Amt"))
            '            ObjTr.TAX2_Rate = Nothing 'clsCommon.myCdbl(dr("TAX2_Rate"))
            '            ObjTr.TAX2_Amt = Nothing 'clsCommon.myCdbl(dr("TAX2_Amt"))
            '            ObjTr.TAX3 = Nothing 'clsCommon.myCstr(dr("TAX3"))
            '            ObjTr.TAX3_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX3_Base_Amt"))
            '            ObjTr.TAX3_Rate = Nothing 'clsCommon.myCdbl(dr("TAX3_Rate"))
            '            ObjTr.TAX3_Amt = Nothing 'clsCommon.myCdbl(dr("TAX3_Amt"))
            '            ObjTr.TAX4 = Nothing 'clsCommon.myCstr(dr("TAX4"))
            '            ObjTr.TAX4_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX4_Base_Amt"))
            '            ObjTr.TAX4_Rate = Nothing 'clsCommon.myCdbl(dr("TAX4_Rate"))
            '            ObjTr.TAX4_Amt = Nothing 'clsCommon.myCdbl(dr("TAX4_Amt"))
            '            ObjTr.TAX5 = Nothing 'clsCommon.myCstr(dr("TAX5"))
            '            ObjTr.TAX5_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX5_Base_Amt"))
            '            ObjTr.TAX5_Rate = Nothing 'clsCommon.myCdbl(dr("TAX5_Rate"))
            '            ObjTr.TAX5_Amt = Nothing 'clsCommon.myCdbl(dr("TAX5_Amt"))
            '            ObjTr.TAX6 = Nothing 'clsCommon.myCstr(dr("TAX6"))
            '            ObjTr.TAX6_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX6_Base_Amt"))
            '            ObjTr.TAX6_Rate = Nothing 'clsCommon.myCdbl(dr("TAX6_Rate"))
            '            ObjTr.TAX6_Amt = Nothing 'clsCommon.myCdbl(dr("TAX6_Amt"))
            '            ObjTr.TAX7 = Nothing 'clsCommon.myCstr(dr("TAX7"))
            '            ObjTr.TAX7_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX7_Base_Amt"))
            '            ObjTr.TAX7_Rate = Nothing 'clsCommon.myCdbl(dr("TAX7_Rate"))
            '            ObjTr.TAX7_Amt = Nothing 'clsCommon.myCdbl(dr("TAX7_Amt"))
            '            ObjTr.TAX8 = Nothing 'clsCommon.myCstr(dr("TAX8"))
            '            ObjTr.TAX8_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX8_Base_Amt"))
            '            ObjTr.TAX8_Rate = Nothing 'clsCommon.myCdbl(dr("TAX8_Rate"))
            '            ObjTr.TAX8_Amt = Nothing 'clsCommon.myCdbl(dr("TAX8_Amt"))
            '            ObjTr.TAX9 = Nothing 'clsCommon.myCstr(dr("TAX9"))
            '            ObjTr.TAX9_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX9_Base_Amt"))
            '            ObjTr.TAX9_Rate = Nothing 'clsCommon.myCdbl(dr("TAX9_Rate"))
            '            ObjTr.TAX9_Amt = Nothing 'clsCommon.myCdbl(dr("TAX9_Amt"))
            '            ObjTr.TAX10 = Nothing 'clsCommon.myCstr(dr("TAX10"))
            '            ObjTr.TAX10_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX10_Base_Amt"))
            '            ObjTr.TAX10_Rate = Nothing 'clsCommon.myCdbl(dr("TAX10_Rate"))
            '            ObjTr.TAX10_Amt = Nothing 'clsCommon.myCdbl(dr("TAX10_Amt"))
            '            ObjTr.Amount = Nothing 'clsCommon.myCdbl(dr("Amount"))
            '            ObjTr.Disc_Per = Nothing 'clsCommon.myCdbl(dr("Disc_Per"))
            '            ObjTr.Disc_Amt = Nothing 'clsCommon.myCdbl(dr("Disc_Amt"))
            '            ObjTr.Amt_Less_Discount = Nothing 'clsCommon.myCdbl(dr("Amt_Less_Discount"))
            '            ObjTr.Total_Tax_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Tax_Amt"))
            '            ObjTr.Item_Net_Amt = Nothing 'clsCommon.myCdbl(dr("Item_Net_Amt"))


            '            ObjTr.Is_Mannual_Amt = Nothing '.myCdbl(dr("Is_Mannual_Amt"))

            '            ' ''objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
            '            ' ''objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

            '            ObjTr.MRP = clsCommon.myCdbl(dr("MRP"))
            '            ObjTr.Assessable = Nothing 'clsCommon.myCdbl(dr("Assessable"))
            '            ObjTr.AssessableAmt = Nothing 'clsCommon.myCdbl(dr("AssessableAmt"))
            '            ObjTr.Batch_No = Nothing 'clsCommon.myCstr(dr("Batch_No"))
            '            ' If dr("MFG_Date") IsNot DBNull.Value Then
            '            ObjTr.MFG_Date = Nothing 'clsCommon.myCDate(dr("MFG_Date"))
            '            '        End If
            '            'If dr("Expiry_Date") IsNot DBNull.Value Then
            '            ObjTr.Expiry_Date = Nothing 'clsCommon.myCDate(dr("Expiry_Date"))
            '            'End If
            '            ObjTr.Specification = clsCommon.myCstr(dr("Specification"))
            '            ObjTr.Remarks = clsCommon.myCstr(dr("Remarks"))
            '            'objTr.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))

            '            ObjTr.Scheme_Applicable = Nothing 'clsCommon.myCstr(dr("Scheme_Applicable"))
            '            ObjTr.Scheme_Code = Nothing 'clsCommon.myCstr(dr("Scheme_Code"))
            '            ObjTr.Scheme_Item = Nothing 'clsCommon.myCstr(dr("Scheme_Item"))
            '            ObjTr.Item_Tax = Nothing 'clsCommon.myCdbl(dr("Item_Tax"))
            '            ObjTr.Total_MRP_Amt = Nothing 'clsCommon.myCdbl(dr("Total_MRP_Amt"))
            '            ObjTr.Total_Basic_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Basic_Amt"))
            '            ObjTr.Total_Disc_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Disc_Amt"))
            '            ObjTr.Cust_Discount = Nothing 'clsCommon.myCdbl(dr("Cust_Discount"))
            '            ObjTr.Total_Cust_Discount = Nothing 'clsCommon.myCdbl(dr("Total_Cust_Discount"))
            '            ObjTr.ActualRate = Nothing 'clsCommon.myCdbl(dr("ActualRate"))
            '            ObjTr.Cust_DiscountQty = Nothing 'clsCommon.myCdbl(dr("Cust_DiscountQty"))
            '            ObjTr.Price_Code = Nothing 'clsCommon.myCstr(dr("Price_code"))
            '            If dr("Price Date") IsNot DBNull.Value Then
            '                ObjTr.Price_Date = clsCommon.myCDate(dr("Price Date"))
            '            End If

            '            ObjTr.Abatement_Per = Nothing 'clsCommon.myCdbl(dr("Abatement_Per"))
            '            ObjTr.Abatement_Amt = Nothing 'clsCommon.myCdbl(dr("Abatement_Amt"))
            '            ObjTr.FOC_Item = Nothing 'clsCommon.myCdbl(dr("FOC_Item"))
            '            ObjTr.Markup_On = Nothing 'clsCommon.myCstr(dr("Markup_On"))
            '            ObjTr.Markup_Percent = Nothing 'clsCommon.myCdbl(dr("Markup_Percent"))
            '            ObjTr.Landing_Cost = Nothing 'clsCommon.myCdbl(dr("Landing_Cost"))
            '            ObjTr.HeadDiscAmt = Nothing 'clsCommon.myCdbl(dr("HeadDiscAmt"))
            '            ObjTr.CustDiscPer = Nothing 'clsCommon.myCdbl(dr("CustDiscPer"))
            '            ObjTr.CasdDiscScheme_Code = Nothing 'clsCommon.myCstr(dr("CasdDiscScheme_Code"))
            '            ObjTr.Item_Weight = Nothing 'clsCommon.myCdbl(dr("Item_Weight"))
            '            ObjTr.TotalItem_Weight = Nothing 'clsCommon.myCdbl(dr("TotalItem_Weight"))
            '            ObjTr.Purchase_Cost = Nothing 'clsCommon.myCdbl(dr("Purchase_Cost"))
            '            ObjTr.OrgRate = clsCommon.myCdbl(dr("Item Cost"))
            '            ObjTr.Conv_Factor = 1 'clsCommon.myCdbl(dr("Conv_Factor"))
            '            ObjTr.PrincipleCode = Nothing 'clsCommon.myCstr(dr("PrincipleCode"))
            '            ObjTr.PrincipleDesc = Nothing 'clsCommon.myCstr(dr("PrincipleDesc"))
            '            ObjTr.vendor_code = Nothing 'clsCommon.myCstr(dr("vendor_code"))
            '            ObjTr.vendor_desc = Nothing 'clsCommon.myCstr(dr("vendor_desc"))
            '            ObjTr.Bin_No = Nothing 'clsCommon.myCstr(dr("Bin_No"))
            '            ' ObjTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", ObjTr.Document_Code, ObjTr.Item_Code, ObjTr.Line_No, trans)
            '            arrList.Add(ObjTr)
            '        Next
            '    End If
            'Else
            If (DtExcel.Select("[S No]='" & gv.Rows(ix).Cells("S No").Value & "'").Length > 0) Then
                Dim ObjTr As frmShipmentImportExport
                For Each dr As DataRow In DtExcel.Select("[S No]='" & gv.Rows(ix).Cells("S No").Value & "'")
                    ObjTr = New frmShipmentImportExport
                    ObjTr.Document_Code = Nothing
                    ObjTr.Row_Type = "Item"
                    ObjTr.Line_No = Nothing
                    ObjTr.Status_detail = Nothing
                    ObjTr.Item_Code = clsCommon.myCstr(dr("Item Code"))
                    ObjTr.Bar_Code = Nothing
                    ObjTr.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_desc  from TSPL_ITEM_MASTER where Item_Code='" & ObjTr.Item_Code & "' ") 'clsCommon.myCstr(dr("Item_Desc"))
                    ObjTr.Qty = clsCommon.myCdbl(dr("Quantity"))


                    ObjTr.Free_Qty = Nothing 'clsCommon.myCdbl(dr("Free_Qty"))
                    ObjTr.Order_Code = Nothing 'clsCommon.myCstr(dr("Order_Code"))

                    ObjTr.Balance_Qty = clsCommon.myCdbl(dr("Quantity")) ' clsCommon.myCdbl(dr("Balance_Qty"))
                    ObjTr.Unit_code = clsCommon.myCstr(dr("UOM")) 'clsCommon.myCstr(dr("Unit_code"))
                    ObjTr.strLocation = clsCommon.myCstr(dr("Location"))
                    ObjTr.LocationName = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_Location_MASTER where Location_Code='" & ObjTr.strLocation & "' ") 'clsCommon.myCstr(dr("LocationName"))
                    ObjTr.Item_Cost = clsCommon.myCdbl(dr("Item Cost"))
                    ObjTr.TAX1 = Nothing 'clsCommon.myCstr(dr("TAX1"))
                    ObjTr.TAX1_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    ObjTr.TAX1_Rate = Nothing 'clsCommon.myCdbl(dr("TAX1_Rate"))
                    ObjTr.TAX1_Amt = Nothing 'clsCommon.myCdbl(dr("TAX1_Amt"))
                    ObjTr.TAX2 = Nothing 'clsCommon.myCstr(dr("TAX2"))
                    ObjTr.TAX2_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    ObjTr.TAX2_Rate = Nothing 'clsCommon.myCdbl(dr("TAX2_Rate"))
                    ObjTr.TAX2_Amt = Nothing 'clsCommon.myCdbl(dr("TAX2_Amt"))
                    ObjTr.TAX3 = Nothing 'clsCommon.myCstr(dr("TAX3"))
                    ObjTr.TAX3_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    ObjTr.TAX3_Rate = Nothing 'clsCommon.myCdbl(dr("TAX3_Rate"))
                    ObjTr.TAX3_Amt = Nothing 'clsCommon.myCdbl(dr("TAX3_Amt"))
                    ObjTr.TAX4 = Nothing 'clsCommon.myCstr(dr("TAX4"))
                    ObjTr.TAX4_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    ObjTr.TAX4_Rate = Nothing 'clsCommon.myCdbl(dr("TAX4_Rate"))
                    ObjTr.TAX4_Amt = Nothing 'clsCommon.myCdbl(dr("TAX4_Amt"))
                    ObjTr.TAX5 = Nothing 'clsCommon.myCstr(dr("TAX5"))
                    ObjTr.TAX5_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    ObjTr.TAX5_Rate = Nothing 'clsCommon.myCdbl(dr("TAX5_Rate"))
                    ObjTr.TAX5_Amt = Nothing 'clsCommon.myCdbl(dr("TAX5_Amt"))
                    ObjTr.TAX6 = Nothing 'clsCommon.myCstr(dr("TAX6"))
                    ObjTr.TAX6_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    ObjTr.TAX6_Rate = Nothing 'clsCommon.myCdbl(dr("TAX6_Rate"))
                    ObjTr.TAX6_Amt = Nothing 'clsCommon.myCdbl(dr("TAX6_Amt"))
                    ObjTr.TAX7 = Nothing 'clsCommon.myCstr(dr("TAX7"))
                    ObjTr.TAX7_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    ObjTr.TAX7_Rate = Nothing 'clsCommon.myCdbl(dr("TAX7_Rate"))
                    ObjTr.TAX7_Amt = Nothing 'clsCommon.myCdbl(dr("TAX7_Amt"))
                    ObjTr.TAX8 = Nothing 'clsCommon.myCstr(dr("TAX8"))
                    ObjTr.TAX8_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    ObjTr.TAX8_Rate = Nothing 'clsCommon.myCdbl(dr("TAX8_Rate"))
                    ObjTr.TAX8_Amt = Nothing 'clsCommon.myCdbl(dr("TAX8_Amt"))
                    ObjTr.TAX9 = Nothing 'clsCommon.myCstr(dr("TAX9"))
                    ObjTr.TAX9_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    ObjTr.TAX9_Rate = Nothing 'clsCommon.myCdbl(dr("TAX9_Rate"))
                    ObjTr.TAX9_Amt = Nothing 'clsCommon.myCdbl(dr("TAX9_Amt"))
                    ObjTr.TAX10 = Nothing 'clsCommon.myCstr(dr("TAX10"))
                    ObjTr.TAX10_Base_Amt = Nothing 'clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    ObjTr.TAX10_Rate = Nothing 'clsCommon.myCdbl(dr("TAX10_Rate"))
                    ObjTr.TAX10_Amt = Nothing 'clsCommon.myCdbl(dr("TAX10_Amt"))
                    ObjTr.Amount = Nothing 'clsCommon.myCdbl(dr("Amount"))
                    ObjTr.Disc_Per = Nothing 'clsCommon.myCdbl(dr("Disc_Per"))
                    ObjTr.Disc_Amt = Nothing 'clsCommon.myCdbl(dr("Disc_Amt"))
                    ObjTr.Amt_Less_Discount = Nothing 'clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    ObjTr.Total_Tax_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    ObjTr.Item_Net_Amt = Nothing 'clsCommon.myCdbl(dr("Item_Net_Amt"))


                    ObjTr.Is_Mannual_Amt = Nothing '.myCdbl(dr("Is_Mannual_Amt"))

                    ' ''objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    ' ''objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

                    ObjTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    ObjTr.Assessable = Nothing 'clsCommon.myCdbl(dr("Assessable"))
                    ObjTr.AssessableAmt = Nothing 'clsCommon.myCdbl(dr("AssessableAmt"))
                    ObjTr.Batch_No = Nothing 'clsCommon.myCstr(dr("Batch_No"))
                    ' If dr("MFG_Date") IsNot DBNull.Value Then
                    ObjTr.MFG_Date = Nothing 'clsCommon.myCDate(dr("MFG_Date"))
                    '        End If
                    'If dr("Expiry_Date") IsNot DBNull.Value Then
                    ObjTr.Expiry_Date = Nothing 'clsCommon.myCDate(dr("Expiry_Date"))
                    'End If
                    ObjTr.Specification = clsCommon.myCstr(dr("Specification"))
                    ObjTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    'objTr.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))

                    ObjTr.Scheme_Applicable = Nothing 'clsCommon.myCstr(dr("Scheme_Applicable"))
                    ObjTr.Scheme_Code = Nothing 'clsCommon.myCstr(dr("Scheme_Code"))
                    ObjTr.Scheme_Item = Nothing 'clsCommon.myCstr(dr("Scheme_Item"))
                    ObjTr.Item_Tax = Nothing 'clsCommon.myCdbl(dr("Item_Tax"))
                    ObjTr.Total_MRP_Amt = Nothing 'clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    ObjTr.Total_Basic_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    ObjTr.Total_Disc_Amt = Nothing 'clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    ObjTr.Cust_Discount = Nothing 'clsCommon.myCdbl(dr("Cust_Discount"))
                    ObjTr.Total_Cust_Discount = Nothing 'clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    ObjTr.ActualRate = Nothing 'clsCommon.myCdbl(dr("ActualRate"))
                    ObjTr.Cust_DiscountQty = Nothing 'clsCommon.myCdbl(dr("Cust_DiscountQty"))
                    ObjTr.Price_Code = Nothing 'clsCommon.myCstr(dr("Price_code"))
                    If dr("Price Date") IsNot DBNull.Value Then
                        ObjTr.Price_Date = clsCommon.myCDate(dr("Price Date"))
                    End If

                    ObjTr.Abatement_Per = Nothing 'clsCommon.myCdbl(dr("Abatement_Per"))
                    ObjTr.Abatement_Amt = Nothing 'clsCommon.myCdbl(dr("Abatement_Amt"))
                    ObjTr.FOC_Item = Nothing 'clsCommon.myCdbl(dr("FOC_Item"))
                    ObjTr.Markup_On = Nothing 'clsCommon.myCstr(dr("Markup_On"))
                    ObjTr.Markup_Percent = Nothing 'clsCommon.myCdbl(dr("Markup_Percent"))
                    ObjTr.Landing_Cost = Nothing 'clsCommon.myCdbl(dr("Landing_Cost"))
                    ObjTr.HeadDiscAmt = Nothing 'clsCommon.myCdbl(dr("HeadDiscAmt"))
                    ObjTr.CustDiscPer = Nothing 'clsCommon.myCdbl(dr("CustDiscPer"))
                    ObjTr.CasdDiscScheme_Code = Nothing 'clsCommon.myCstr(dr("CasdDiscScheme_Code"))
                    ObjTr.Item_Weight = Nothing 'clsCommon.myCdbl(dr("Item_Weight"))
                    ObjTr.TotalItem_Weight = Nothing 'clsCommon.myCdbl(dr("TotalItem_Weight"))
                    ObjTr.Purchase_Cost = Nothing 'clsCommon.myCdbl(dr("Purchase_Cost"))
                    ObjTr.OrgRate = clsCommon.myCdbl(dr("Item Cost"))
                    ObjTr.Conv_Factor = 1 'clsCommon.myCdbl(dr("Conv_Factor"))
                    ObjTr.PrincipleCode = Nothing 'clsCommon.myCstr(dr("PrincipleCode"))
                    ObjTr.PrincipleDesc = Nothing 'clsCommon.myCstr(dr("PrincipleDesc"))
                    ObjTr.vendor_code = Nothing 'clsCommon.myCstr(dr("vendor_code"))
                    ObjTr.vendor_desc = Nothing 'clsCommon.myCstr(dr("vendor_desc"))
                    ObjTr.Bin_No = Nothing 'clsCommon.myCstr(dr("Bin_No"))
                    ' ObjTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", ObjTr.Document_Code, ObjTr.Item_Code, ObjTr.Line_No, trans)
                    arrList.Add(ObjTr)
                Next
            End If
            ' End If



        Next
    End Sub
    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            If DtHead.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Import Shipment Head Details....", "Message", MessageBoxButtons.OK, RadMessageIcon.Info)
                Exit Sub
            End If
            If DtDetail.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Import Shipment Item Details....", "Message", MessageBoxButtons.OK, RadMessageIcon.Info)
                Exit Sub
            End If

            For Each row As DataRow In DtHead.Rows
                row("Error") = ""
                If String.IsNullOrEmpty(row("Document Date").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Document Date", ",Document Date")
                End If
                If String.IsNullOrEmpty(row("Customer Code").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Customer Code", ",Customer Code")
                End If
                If String.IsNullOrEmpty(row("Bill To Location").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Bill To Location", ",Bill To Location")
                End If

                'If String.IsNullOrEmpty(row("Ship To Location").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Ship To Location", ",Ship To Location")
                'End If

                If String.IsNullOrEmpty(row("Tax Group").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Tax Group", ",Tax Group")
                End If

                'If String.IsNullOrEmpty(row("Project Id").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Project Id", ",Project Id")
                'End If

                'If String.IsNullOrEmpty(row("Carrier").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Carrier", ",Carrier")
                'End If

                'If String.IsNullOrEmpty(row("VehicleNo").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "VehicleNo", ",VehicleNo")
                'End If

                'If String.IsNullOrEmpty(row("Vehicle Code").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Vehicle Code", ",Vehicle Code")
                'End If

                'If String.IsNullOrEmpty(row("GRNo").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "GRNo", ",GRNo")
                'End If

                'If String.IsNullOrEmpty(row("GENo").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "GENo", ",GENO")
                'End If

                'If String.IsNullOrEmpty(row("GEDate").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "GEDATE", ",GEDATE")
                'End If

                'If String.IsNullOrEmpty(row("Dept").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Dept", ",Dept")
                'End If

                If String.IsNullOrEmpty(row("Salesman Code").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Salesman Code", ",Salesman Code")
                End If

                If String.IsNullOrEmpty(row("Salesman Name").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Salesman Name", ",Salesman Name")
                End If

                'If String.IsNullOrEmpty(row("Price Code").ToString) Then
                '    row("Error") &= IIf(row("Error") = "", "Price Code", ",Price Code")
                'End If

                If String.IsNullOrEmpty(row("S No").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "S No", ",S No")
                End If
                'If Not String.IsNullOrEmpty(row("Document Code").ToString) Then
                '    If DtDetail.Select("[Document Code]='" & row("Document Code").ToString & "'").Length <= 0 Then
                '        row("Error") &= IIf(row("Error") = "", "Shipment Detail", ",Shipping Detail")
                '    End If
                'Else
                If DtDetail.Select("[S No]='" & row("S No").ToString & "'").Length <= 0 Then
                    row("Error") &= IIf(row("Error") = "", "Shipment Detail", ",Shipping Detail")
                End If
                ' End If

            Next
            DtHead.AcceptChanges()




            For Each row As DataRow In DtDetail.Rows()
                row("Error") = ""
                If String.IsNullOrEmpty(row("Item Code").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Item Code", ",Item Code")
                End If

                If String.IsNullOrEmpty(row("Price Date").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Price Date", ",Price Date")
                End If

                If String.IsNullOrEmpty(row("UOM").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "UOM", ",UOM")
                End If

                If String.IsNullOrEmpty(row("Quantity").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Quantity", ",Quantity")
                End If

                If String.IsNullOrEmpty(row("MRP").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "MRP", ",MRP")
                End If

                If String.IsNullOrEmpty(row("Item Cost").ToString) Then
                    row("Error") &= IIf(row("Error") = "", "Item Cost", ",Item Cost")
                End If

                'If Not String.IsNullOrEmpty(row("Document Code").ToString) Then
                '    If DtHead.Select("[Document Code]='" & row("Document Code").ToString & "'").Length <= 0 Then
                '        row("Error") &= IIf(row("Error") = "", "Shipment Detail", ",Shipping Detail")
                '    End If
                'Else
                If DtHead.Select("[S No]='" & row("S No").ToString & "'").Length <= 0 Then
                    row("Error") &= IIf(row("Error") = "", "Shipment Detail", ",Shipping Detail")
                End If

                If DtDetail.Select("[S No]='" & row("S No").ToString & "' and [Item Code]='" & row("Item Code") & "'").Length > 1 Then
                    row("Error") &= IIf(row("Error") = "", "Item Detail Contains Duplicate Rows", ",Item Detail Contains Duplicate Rows")
                End If
                'End If
            Next
            If DtHead.Select("Error<>''").Length <= 0 And DtDetail.Select("Error<>''").Length <= 0 Then
                btnSave.Enabled = True
            End If
            BtnExportDetail.Enabled = True
            BtnExportHead.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub gvHead_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gvHead.RowFormatting, gvDetail.RowFormatting
        Try
            If e.RowElement.RowInfo.Cells("Error").Value = "" Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.BackColor = Color.LightGreen
            Else
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.BackColor = Color.LightCoral
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCLose.Click
        Me.Close()
    End Sub

    Dim arrHeader As List(Of String) = New List(Of String)()

    Private Sub BtnExportHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportHead.Click
        arrHeader.Clear()
        arrHeader.Add("If Data Contains Document Code then it is Imported ,If it Contains Error then User have to Modify Those Errors To Import")
        clsCommon.MyExportToExcelGrid("Shipment Head Detail", gvHead, arrHeader, Me.Text, True)
    End Sub

    Private Sub BtnExportDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportDetail.Click
        arrHeader.Clear()
        arrHeader.Add("If Data Contains Document Code then it is Imported ,If it Contains Error then User have to Modify Those Errors To Import")
        clsCommon.MyExportToExcelGrid("Shipment Item Detail", gvDetail, arrHeader, Me.Text, True)
    End Sub
End Class