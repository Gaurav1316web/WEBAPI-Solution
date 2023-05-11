Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmTranspoterDeduction
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strQuery As String
    Dim strQueryCANCRate As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False
    ' Shortage of crates ,No of crates , Amount
    ' Excess loading of milk , Qty(Ltrs)
    ' Vehicle Condition , Top Less , Logo , Inner Body Painting , Cleaniness, Bottom Damage , Shelf, Light
    ' Late Vehicle Report : Late Vehicle Report
    ' Shortage of loading staff/supervisors  : Shortage of loading staff/supervisors

    'colDate,colShortageOfCrates_NoOfCrates, colShortageOfCrates_Amount ,colExcessLoadingOfMilk_Qty,colExcessLoadingOfMilk_Amount
    'colVehicleCondition_TopLess,colVehicleCondition_Logo,colVehicleCondition_InnerBodyPainting,colVehicleCondition_Cleaniness,colVehicleCondition_BottomDamage,colVehicleCondition_Shelf,colVehicleCondition_Light,colVehicleCondition_TotalAmount
    'colLateVehicleReport,colShortageOfLoadingStaffSupervisors,colTotalAmount,colRemarks
    Const colLineNo As String = "colLineNo"
    Const colDate As String = "colDate"
    Const colShortageOfCrates_NoOfCrates As String = "colShortageOfCrates_NoOfCrates"
    Const colShortageOfCrates_Amount As String = "colShortageOfCrates_Amount"

    Const colExcessLoadingOfMilk_Qty As String = "colExcessLoadingOfMilk_Qty"
    Const colExcessLoadingOfMilk_Amount As String = "colExcessLoadingOfMilk_Amount"

    Const colVehicleCondition_TopLess As String = "colVehicleCondition_TopLess"
    Const colVehicleCondition_Logo As String = "colVehicleCondition_Logo"
    Const colVehicleCondition_InnerBodyPainting As String = "colVehicleCondition_InnerBodyPainting"
    Const colVehicleCondition_Cleaniness As String = "colVehicleCondition_Cleaniness"
    Const colVehicleCondition_BottomDamage As String = "colVehicleCondition_BottomDamage"
    Const colVehicleCondition_Shelf As String = "colVehicleCondition_Shelf"
    Const colVehicleCondition_Light As String = "colVehicleCondition_Light"
    Const colVehicleCondition_TotalAmount As String = "colVehicleCondition_TotalAmount"

    Const colLateVehicleReport As String = "colLateVehicleReport"
    Const colShortageOfLoadingStaffSupervisors As String = "colShortageOfLoadingStaffSupervisors"
    Const colTotalAmount As String = "colTotalAmount"
    Const colRemarks As String = "colRemarks"

#End Region
    ''Checked in 20200617 by richa.
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverseAndUnpost.Enabled = True
        Else
            btnReverseAndUnpost.Enabled = False
        End If
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        'colDate,colShortageOfCrates_NoOfCrates, colShortageOfCrates_Amount ,colExcessLoadingOfMilk_Qty,colExcessLoadingOfMilk_Amount
        'colVehicleCondition_TopLess,colVehicleCondition_Logo,colVehicleCondition_InnerBodyPainting,colVehicleCondition_Cleaniness,colVehicleCondition_BottomDamage,colVehicleCondition_Shelf,colVehicleCondition_Light,colVehicleCondition_TotalAmount
        'colLateVehicleReport,colShortageOfLoadingStaffSupervisors,colTotalAmount,colRemarks

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim strDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        strDate.FormatString = ""
        strDate.HeaderText = "Date"
        strDate.Name = colDate
        strDate.Width = 100
        strDate.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(strDate)


        Dim repoNoOfCrates As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNoOfCrates = New GridViewDecimalColumn()
        repoNoOfCrates.FormatString = ""
        repoNoOfCrates.HeaderText = "Shortage Of Crates" + Environment.NewLine + "No Of Crates"
        repoNoOfCrates.Name = colShortageOfCrates_NoOfCrates
        repoNoOfCrates.Width = 120
        repoNoOfCrates.Minimum = 0
        repoNoOfCrates.ReadOnly = False
        repoNoOfCrates.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoNoOfCrates)


        Dim repoShortageOfCrates_Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortageOfCrates_Amount = New GridViewDecimalColumn()
        repoShortageOfCrates_Amount.FormatString = ""
        repoShortageOfCrates_Amount.HeaderText = "Shortage Of Crates" + Environment.NewLine + "Amount in Rs."
        repoShortageOfCrates_Amount.Name = colShortageOfCrates_Amount
        repoShortageOfCrates_Amount.Width = 120
        repoShortageOfCrates_Amount.Minimum = 0
        repoShortageOfCrates_Amount.ReadOnly = False
        repoShortageOfCrates_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoShortageOfCrates_Amount)


        ' colExcessLoadingOfMilk_Qty,colExcessLoadingOfMilk_Amount

        Dim repoExcessLoadingOfMilk_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoExcessLoadingOfMilk_Qty = New GridViewDecimalColumn()
        repoExcessLoadingOfMilk_Qty.FormatString = ""
        repoExcessLoadingOfMilk_Qty.HeaderText = "Excess Loading Of Milk" + Environment.NewLine + "Qty(Ltrs)"
        repoExcessLoadingOfMilk_Qty.Name = colExcessLoadingOfMilk_Qty
        repoExcessLoadingOfMilk_Qty.Width = 120
        repoExcessLoadingOfMilk_Qty.Minimum = 0
        repoExcessLoadingOfMilk_Qty.ReadOnly = False
        repoExcessLoadingOfMilk_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoExcessLoadingOfMilk_Qty)


        Dim repoExcessLoadingOfMilk_Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoExcessLoadingOfMilk_Amount = New GridViewDecimalColumn()
        repoExcessLoadingOfMilk_Amount.FormatString = ""
        repoExcessLoadingOfMilk_Amount.HeaderText = "Excess Loading Of Milk" + Environment.NewLine + "Amount in Rs."
        repoExcessLoadingOfMilk_Amount.Name = colExcessLoadingOfMilk_Amount
        repoExcessLoadingOfMilk_Amount.Width = 120
        repoExcessLoadingOfMilk_Amount.Minimum = 0
        repoExcessLoadingOfMilk_Amount.ReadOnly = False
        repoExcessLoadingOfMilk_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoExcessLoadingOfMilk_Amount)

        'colVehicleCondition_TopLess,colVehicleCondition_Logo,colVehicleCondition_InnerBodyPainting,colVehicleCondition_Cleaniness,colVehicleCondition_BottomDamage,colVehicleCondition_Shelf,colVehicleCondition_Light,colVehicleCondition_TotalAmount

        Dim repoVehicleCondition_TopLess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_TopLess = New GridViewDecimalColumn()
        repoVehicleCondition_TopLess.FormatString = ""
        repoVehicleCondition_TopLess.HeaderText = "Vehicle Condition" + Environment.NewLine + "TopLess"
        repoVehicleCondition_TopLess.Name = colVehicleCondition_TopLess
        repoVehicleCondition_TopLess.Width = 120
        repoVehicleCondition_TopLess.Minimum = 0
        repoVehicleCondition_TopLess.ReadOnly = False
        repoVehicleCondition_TopLess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_TopLess)

        Dim repoVehicleCondition_Logo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_Logo = New GridViewDecimalColumn()
        repoVehicleCondition_Logo.FormatString = ""
        repoVehicleCondition_Logo.HeaderText = "Vehicle Condition" + Environment.NewLine + "Logo"
        repoVehicleCondition_Logo.Name = colVehicleCondition_Logo
        repoVehicleCondition_Logo.Width = 120
        repoVehicleCondition_Logo.Minimum = 0
        repoVehicleCondition_Logo.ReadOnly = False
        repoVehicleCondition_Logo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_Logo)

        Dim repoVehicleCondition_InnerBodyPainting As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_InnerBodyPainting = New GridViewDecimalColumn()
        repoVehicleCondition_InnerBodyPainting.FormatString = ""
        repoVehicleCondition_InnerBodyPainting.HeaderText = "Vehicle Condition" + Environment.NewLine + "Inner Body Painting"
        repoVehicleCondition_InnerBodyPainting.Name = colVehicleCondition_InnerBodyPainting
        repoVehicleCondition_InnerBodyPainting.Width = 120
        repoVehicleCondition_InnerBodyPainting.Minimum = 0
        repoVehicleCondition_InnerBodyPainting.ReadOnly = False
        repoVehicleCondition_InnerBodyPainting.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_InnerBodyPainting)

        ' colVehicleCondition_Cleaniness,colVehicleCondition_BottomDamage,

        Dim repoVehicleCondition_Cleaniness As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_Cleaniness = New GridViewDecimalColumn()
        repoVehicleCondition_Cleaniness.FormatString = ""
        repoVehicleCondition_Cleaniness.HeaderText = "Vehicle Condition" + Environment.NewLine + "Cleaniness"
        repoVehicleCondition_Cleaniness.Name = colVehicleCondition_Cleaniness
        repoVehicleCondition_Cleaniness.Width = 120
        repoVehicleCondition_Cleaniness.Minimum = 0
        repoVehicleCondition_Cleaniness.ReadOnly = False
        repoVehicleCondition_Cleaniness.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_Cleaniness)


        Dim repoVehicleCondition_BottomDamage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_BottomDamage = New GridViewDecimalColumn()
        repoVehicleCondition_BottomDamage.FormatString = ""
        repoVehicleCondition_BottomDamage.HeaderText = "Vehicle Condition" + Environment.NewLine + "BottomDamage"
        repoVehicleCondition_BottomDamage.Name = colVehicleCondition_BottomDamage
        repoVehicleCondition_BottomDamage.Width = 120
        repoVehicleCondition_BottomDamage.Minimum = 0
        repoVehicleCondition_BottomDamage.ReadOnly = False
        repoVehicleCondition_BottomDamage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_BottomDamage)

        ' colVehicleCondition_Shelf,colVehicleCondition_Light,colVehicleCondition_TotalAmount

        Dim repoVehicleCondition_Shelf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_Shelf = New GridViewDecimalColumn()
        repoVehicleCondition_Shelf.FormatString = ""
        repoVehicleCondition_Shelf.HeaderText = "Vehicle Condition" + Environment.NewLine + "Shelf"
        repoVehicleCondition_Shelf.Name = colVehicleCondition_Shelf
        repoVehicleCondition_Shelf.Width = 120
        repoVehicleCondition_Shelf.Minimum = 0
        repoVehicleCondition_Shelf.ReadOnly = False
        repoVehicleCondition_Shelf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_Shelf)

        Dim repoVehicleCondition_Light As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_Light = New GridViewDecimalColumn()
        repoVehicleCondition_Light.FormatString = ""
        repoVehicleCondition_Light.HeaderText = "Vehicle Condition" + Environment.NewLine + "Light"
        repoVehicleCondition_Light.Name = colVehicleCondition_Light
        repoVehicleCondition_Light.Width = 120
        repoVehicleCondition_Light.Minimum = 0
        repoVehicleCondition_Light.ReadOnly = False
        repoVehicleCondition_Light.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_Light)

        ' colVehicleCondition_TotalAmount

        Dim repoVehicleCondition_TotalAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVehicleCondition_TotalAmount = New GridViewDecimalColumn()
        repoVehicleCondition_TotalAmount.FormatString = ""
        repoVehicleCondition_TotalAmount.HeaderText = "Vehicle Condition" + Environment.NewLine + "Total Amount"
        repoVehicleCondition_TotalAmount.Name = colVehicleCondition_TotalAmount
        repoVehicleCondition_TotalAmount.Width = 120
        repoVehicleCondition_TotalAmount.Minimum = 0
        repoVehicleCondition_TotalAmount.ReadOnly = True
        repoVehicleCondition_TotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoVehicleCondition_TotalAmount)

        ' ',,,colRemarks

        Dim repoLateVehicleReport As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLateVehicleReport = New GridViewDecimalColumn()
        repoLateVehicleReport.FormatString = ""
        repoLateVehicleReport.HeaderText = "Late Vehicle Report"
        repoLateVehicleReport.Name = colLateVehicleReport
        repoLateVehicleReport.Width = 120
        repoLateVehicleReport.Minimum = 0
        repoLateVehicleReport.ReadOnly = False
        repoLateVehicleReport.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLateVehicleReport)

        Dim repoShortageOfLoadingStaffSupervisors As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortageOfLoadingStaffSupervisors = New GridViewDecimalColumn()
        repoShortageOfLoadingStaffSupervisors.FormatString = ""
        repoShortageOfLoadingStaffSupervisors.HeaderText = "Shortage Of Loading" + Environment.NewLine + "Staff Supervisors"
        repoShortageOfLoadingStaffSupervisors.Name = colShortageOfLoadingStaffSupervisors
        repoShortageOfLoadingStaffSupervisors.Width = 120
        repoShortageOfLoadingStaffSupervisors.Minimum = 0
        repoShortageOfLoadingStaffSupervisors.ReadOnly = False
        repoShortageOfLoadingStaffSupervisors.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoShortageOfLoadingStaffSupervisors)

        Dim repoTotalAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmount = New GridViewDecimalColumn()
        repoTotalAmount.FormatString = ""
        repoTotalAmount.HeaderText = "Total Amount"
        repoTotalAmount.Name = colTotalAmount
        repoTotalAmount.Width = 120
        repoTotalAmount.Minimum = 0
        repoTotalAmount.ReadOnly = True
        repoTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoTotalAmount)

        Dim strRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        strRemarks.FormatString = ""
        strRemarks.HeaderText = "Remarks"
        strRemarks.Name = colRemarks
        strRemarks.Width = 150
        strRemarks.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(strRemarks)



        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AllowAddNewRow = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub

    Private Sub funFillGrid()
        Try
            LoadBlankGrid()
            'strQuery = " Select  TSPL_SD_SALE_INVOICE_DETAIL.Line_No,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
            '           " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = '" + strInvoiceNo + "' and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N'  order By  TSPL_SD_SALE_INVOICE_DETAIL.Line_No asc "
            'strQuery = " Select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code , sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Qty from TSPL_SD_SALE_INVOICE_DETAIL  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
            '           " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = '" + strInvoiceNo + "' and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code "
            Dim fromdate As String = ""
            Dim Todate As String = ""
            fromdate = clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy")

            strQuery = " select convert ( varchar, thedate,103) as thedate from  dbo.ExplodeDates( convert (date,'" + fromdate + "', 103),convert (date, '" + Todate + "', 103))  "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            isInsideLoadData = True
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dr("thedate"))

                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item_Desc"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemShortDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Qty"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageQty).Value = clsCommon.myCstr(dr("Qty"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageUOM).Value = clsCommon.myCstr(dr("Unit_Code"))
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageQty).Value = 0
                Next
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            common.clsCommon.MyMessageBoxShow(ex.Message, "Transpoter Deduction", MessageBoxButtons.OK)
        End Try
    End Sub

    Function AllowToSave() As Boolean


        If clsCommon.myLen(fndTranspoter.Value) <= 0 Then
            fndTranspoter.Focus()
            Throw New Exception("Please select Transpoter")
        End If
        If mulRoute.arrValueMember Is Nothing OrElse mulRoute.arrValueMember.Count <= 0 Then
            mulRoute.Focus()
            Throw New Exception("Please select atlest one Route")
        End If
        'If clsCommon.CompairString(cboType.SelectedValue, "Quailty") = CompairStringResult.Equal Then
        '    If clsCommon.myLen(fndInvoice.Value) <= 0 Then
        '        Throw New Exception("Plese select Invoice")
        '    End If
        'End If
        'Dim totalDamageQty As Double = 0
        'For ii As Integer = 0 To Gv1.Rows.Count - 1
        '    Dim qty As Double = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colQty).Value)
        '    Dim Uom As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUnit).Value)
        '    Dim DamageQty As Double = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colDamageQty).Value)
        '    Dim DamageUom As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colDamageUOM).Value)
        '    Dim ItemCode As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colItemCode).Value)
        '    If DamageQty > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(ii).Cells(colCustomerComplain).Value)) <= 0 Then
        '        Throw New Exception("Please Select Complain Code For Item Code '" + ItemCode + "'")
        '    End If

        '    If DamageQty > 0 AndAlso clsCommon.myLen(DamageUom) <= 0 Then
        '        Throw New Exception("Please Select Damage UOM For Item Code '" + ItemCode + "'")
        '    End If
        '    If DamageQty > 0 Then
        '        Dim dblValidMaxQty As Double = GetItemConvQty(ItemCode, Uom, DamageUom, qty)
        '        If DamageQty > dblValidMaxQty Then
        '            Throw New Exception("Invalid Damage Qty for " + ItemCode + " (with Damage Uom " + DamageUom + " maiximum qty " + clsCommon.myCstr(dblValidMaxQty) + " Allowed).")
        '        End If
        '    End If
        '    totalDamageQty = totalDamageQty + DamageQty
        'Next
        'If clsCommon.CompairString(cboType.SelectedValue, "Quailty") = CompairStringResult.Equal Then
        '    If totalDamageQty <= 0 Then
        '        Throw New Exception("Please enter Damage Qty atleast for one item")
        '    End If
        'End If
        Return True
    End Function



    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTransporterDeductionHead()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = clsCommon.myCDate(txtDocDate.Value)
                obj.Transporter_code = fndTranspoter.Value

                'obj.Route_No = fndRoute.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Remarks = txtRemarks.Text
                obj.arrRoute = mulRoute.arrValueMember
                obj.Arr = New List(Of clsTransporterDeductionDetail)
                For Each grow As GridViewRowInfo In Gv1.Rows
                    Dim objTr As New clsTransporterDeductionDetail()
                    objTr.SNo = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    ' Document_Code,Deduction_Date,SOC_NoOfCrates,SOC_NoOfCrates_Amount,ELOM_QTY,ELOM_Amount,VC_TopLess,VC_LOGO,VC_InnerBodyPainting,VC_Cleaniness
                    ' VC_BottomDamage , VC_Shelf ,VC_Light,VC_Amount,LateVehicleReport_Amount , ShortageOfLoadingStaffSupervisors_Amount , Net_Amount , Remarks

                    objTr.Deduction_Date = clsCommon.myCDate(grow.Cells(colDate).Value)
                    objTr.SOC_NoOfCrates = clsCommon.myCdbl(grow.Cells(colShortageOfCrates_NoOfCrates).Value)
                    objTr.SOC_NoOfCrates_Amount = clsCommon.myCdbl(grow.Cells(colShortageOfCrates_Amount).Value)
                    objTr.ELOM_QTY = clsCommon.myCdbl(grow.Cells(colExcessLoadingOfMilk_Qty).Value)
                    objTr.ELOM_Amount = clsCommon.myCdbl(grow.Cells(colExcessLoadingOfMilk_Amount).Value)
                    objTr.VC_TopLess = clsCommon.myCdbl(grow.Cells(colVehicleCondition_TopLess).Value)
                    objTr.VC_LOGO = clsCommon.myCdbl(grow.Cells(colVehicleCondition_Logo).Value)
                    objTr.VC_InnerBodyPainting = clsCommon.myCdbl(grow.Cells(colVehicleCondition_InnerBodyPainting).Value)

                    objTr.VC_Cleaniness = clsCommon.myCdbl(grow.Cells(colVehicleCondition_Cleaniness).Value)
                    objTr.VC_BottomDamage = clsCommon.myCdbl(grow.Cells(colVehicleCondition_BottomDamage).Value)
                    objTr.VC_Shelf = clsCommon.myCdbl(grow.Cells(colVehicleCondition_Light).Value)
                    objTr.VC_Light = clsCommon.myCdbl(grow.Cells(colVehicleCondition_InnerBodyPainting).Value)
                    objTr.VC_Amount = clsCommon.myCdbl(grow.Cells(colVehicleCondition_TotalAmount).Value)
                    objTr.LateVehicleReport_Amount = clsCommon.myCdbl(grow.Cells(colLateVehicleReport).Value)
                    objTr.ShortageOfLoadingStaffSupervisors_Amount = clsCommon.myCdbl(grow.Cells(colShortageOfLoadingStaffSupervisors).Value)
                    objTr.Net_Amount = clsCommon.myCstr(grow.Cells(colTotalAmount).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                    obj.Arr.Add(objTr)
                    'If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.myCdbl(objTr.Damage_Qty) > 0 Then
                    '    obj.Arr.Add(objTr)
                    'End If
                Next
                'If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                '    common.clsCommon.MyMessageBoxShow("Please Fill at list one Document")
                '    Return
                'End If
                If (obj.SaveData(obj, isNewEntry, Nothing)) Then
                    common.clsCommon.MyMessageBoxShow(Gv1, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        txtDocNo.Value = ""
        txtDocDate.Value = clsCommon.GETSERVERDATE
        fndTranspoter.Value = ""
        lblTranspoter.Text = ""
        ' fndRoute.Value = ""
        'lblRoute.Text = ""

        txtRemarks.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnSave.Text = "Save"
        LoadBlankGrid()
        isNewEntry = True
        isInsideLoadData = False
        btnDelete.Enabled = True
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        mulRoute.arrValueMember = Nothing

        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Document No not found to Post")
            Exit Sub
        End If

        Dim isPost As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code = '" + txtDocNo.Value + "' and IsPosted = 'Y'"))
        If isPost = True Then
            common.clsCommon.MyMessageBoxShow("Record Already posted.")
            Exit Sub
        End If
        If myMessages.postConfirm() Then
            If (clsTransporterDeductionHead.PostData(clsUserMgtCode.frmTranspoterDeduction, txtDocNo.Value)) Then
                '' auto creation of Dairy dispatch as replacement by richa 
                '    Dim frm As New frmShipmentDairy
                '    frm.SetUserMgmt(clsUserMgtCode.frmSaleDispatchDairy)
                '    frm.Show()
                '    frm.chkReplacement.Checked = True
                '    frm.RadLabel24.Text = "Invoice No"
                '    frm.txtReqNo.Visible = False
                '    frm.TxtInvoiceNoForReplacement.Visible = True
                '    frm.txtReqNo.Value = ""
                '    frm.txtCustomerComplaintNo.Text = ""
                '    frm.txtCustomerComplaintNo.Text = txtDocNo.Value
                '    frm.txtVendorNo.Value = fndTranspoter.Value
                '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'")), "0") = CompairStringResult.Equal Then
                '        frm.cmbDisItemType.SelectedValue = "NT"
                '    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'")), "1") = CompairStringResult.Equal Then
                '        frm.cmbDisItemType.SelectedValue = "T"
                '    End If
                '    frm.txtDate.Value = txtDocDate.Value
                '    frm.SelectInvoiceNo()
                '    frm.showSavedMessage = False
                '    frm.SaveData(False, Nothing)
                '    Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD where isnull(Customer_Complaint_No,'')='" & txtDocNo.Value & "'"))
                '    If clsCommon.myLen(strShipmentNo) > 0 Then
                '        If clsPSShipmentHead.PostData(clsUserMgtCode.frmSaleDispatchDairy, strShipmentNo, True) Then
                '            frm.Close()
                '        End If
                '    End If
                '    ' frm.TxtInvoiceNoForReplacement.Value = txtDocNo.Value
                '    ''frm.txtMccPlantCode.Value = lblLocationCodeBulk.Text
                '    ''richa agarwal 9 Jan,2019 milk transfer in date should be same as weighment tare date as per ranjana mam ERO/09/01/19-000460
                '    ''frm.dtpRcptDateAndTime.Value = dtpTareWeight.Value
                '    ''------------------
                '    ''frm.loadDispatchData(lblChallanNoBulk.Text)
                '    'frm.SaveData(False, True)
                '    'Dim MilkTrasferInNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Receipt_Challan_No from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No='" & lblChallanNoBulk.Text & "'"))
                '    'If clsCommon.myLen(MilkTrasferInNo) > 0 Then
                '    '    If clsMilkTransferIn.postData(MilkTrasferInNo) Then
                '    '        frm.Close()
                '    '    End If
            End If
            ''end of work-----------------
            common.clsCommon.MyMessageBoxShow(Gv1, "Successfully Posted", Me.Text)
            LoadData(txtDocNo.Value, NavigatorType.Current)
            btnSave.Enabled = False
            btnPost.Enabled = False
        End If
        ' End If
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        'Try
        '    Dim strItem = Gv1.Rows(e.RowIndex).Cells(1).Value
        '    strQuery = LoadDoubleClickQuery(strItem)
        '    Dim frmStock As New FrmStockDetail
        '    frmStock.LoadDispatchData(strQuery)
        '    frmStock.Show()
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsTransporterDeductionHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
    '            If clsDairyGatePassEntry.ReverseAndUnpost(txtCode.Value) Then
    '                common.clsCommon.MyMessageBoxShow("Successfully Reversed", Me.Text)
    '                LoadData(txtCode.Value, NavigatorType.Current)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub
    '--============================================================================================================================================================
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " Select Document_Code as Code, Convert (varchar,Document_Date,103) as Date, Transporter_code as [Transporter Code] ,convert (varchar,From_Date,103) as [From Date],convert (varchar,To_Date,103) as [To Date],case when isnull(TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER.IsPosted,'')='Y' then 'Approved' else 'Pending' end as Status from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER "
        LoadData(clsCommon.ShowSelectForm("TranspoterDeductionEntryCode@Fnd", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER where Document_Code='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomerComplain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Public Document_Code As String = Nothing
        'Public Document_Date As DateTime
        'Public Transporter_code As String = Nothing
        'Public Route_No As String = Nothing
        'Public Remarks As String = Nothing
        'Public From_Date As Date? = Nothing
        'Public To_Date As Date? = Nothing
        'Public IsPosted As String = Nothing
        'Public PostedDate As Date? = Nothing

        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL primary Key")
        coll.Add("Document_Date", "datetime not null")
        coll.Add("Transporter_code", "Varchar(30) null")
        'coll.Add("Route_No", "Varchar(30) null ") ' REFERENCES TSPL_Customer_Master (Cust_Code)
        coll.Add("From_Date", "datetime null")
        coll.Add("To_Date", "datetime null")
        coll.Add("Remarks", "Varchar(500)  null")
        'coll.Add("Complaint_Code", "Varchar(30) null REFERENCES TSPL_CUSTOMER_COMPLAINT_MASTER(Code)")
        coll.Add("IsPosted", "Varchar(1) DEFAULT 'N'")
        coll.Add("Created_By", "varchar(12) null")
        coll.Add("Created_Date", "datetime null")
        coll.Add("Modified_By", "varchar(12) null")
        coll.Add("Modified_Date", "datetime null")
        coll.Add("Comp_Code", "varchar(12) null")
        coll.Add("PostedDate", "datetime null")

        clsCommonFunctionality.CreateOrAlterTable("TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("TR_Code", "varchar(30) NOT NULL primary Key")
        coll.Add("Document_Code", "Varchar(30) null REFERENCES TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER(Document_Code)")
        coll.Add("SNo", "Integer")
        coll.Add("Deduction_Date", "datetime not null")
        coll.Add("SOC_NoOfCrates", "decimal(18, 2) NOT NULL DEFAULT 0")
        coll.Add("SOC_NoOfCrates_Amount", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("ELOM_QTY", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("ELOM_Amount", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_TopLess", "decimal(18, 2) NOT NULL DEFAULT 0")
        coll.Add("VC_LOGO", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_InnerBodyPainting", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_Cleaniness", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_BottomDamage", "decimal(18, 2) NOT NULL DEFAULT 0")
        coll.Add("VC_Shelf", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_Light", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("VC_Amount", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("LateVehicleReport_Amount", "decimal(18, 2) NOT NULL DEFAULT 0")
        coll.Add("ShortageOfLoadingStaffSupervisors_Amount", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("Net_Amount", "decimal(18, 2) NOT NULL DEFAULT 0 ")
        coll.Add("Remarks", "Varchar(500) null ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_DETAIL", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("TR_Code", "varchar(30) NOT NULL primary Key")
        coll.Add("Document_Code", "Varchar(30) null REFERENCES TSPL_TRANSPOTER_DEDUCTION_ENTRY_HEADER(Document_Code)")
        coll.Add("Route_Code", "Varchar(30) null ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_TRANSPOTER_DEDUCTION_ENTRY_ROUTE", coll, Nothing, False, False)


        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDocDate.Value = clsCommon.GETSERVERDATE()
        'LoadType()
        txtRemarks.MaxLength = 500
        btnPost.Visible = True
        btnPost.Enabled = False
        'funFillGrid()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Gv1.AllowDeleteRow = False
        Gv1.AllowAddNewRow = False
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()
            If clsCommon.myLen(strCode) <= 0 Then
                Exit Sub
            End If
            Dim obj As New clsTransporterDeductionHead()
            obj = clsTransporterDeductionHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.IsPosted = "Y" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                End If
                txtDocNo.Value = obj.Document_Code
                txtDocDate.Value = obj.Document_Date
                fndTranspoter.Value = obj.Transporter_code
                lblTranspoter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter_Name from tspl_transport_Master WHERE Transport_Id='" + fndTranspoter.Value + "'"))
                ' fndRoute.Value = obj.Route_No
                'lblRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_No from TSPL_ROUTE_MASTER WHERE Route_No='" + fndRoute.Value + "'"))
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtRemarks.Text = obj.Remarks
                mulRoute.arrValueMember = obj.arrRoute
                isInsideLoadData = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsTransporterDeductionDetail In obj.Arr
                        Gv1.Rows.AddNew()
                        'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        ' Document_Code,Deduction_Date,SOC_NoOfCrates,SOC_NoOfCrates_Amount,ELOM_QTY,ELOM_Amount,VC_TopLess,VC_LOGO,VC_InnerBodyPainting,VC_Cleaniness
                        ' VC_BottomDamage , VC_Shelf ,VC_Light,VC_Amount,LateVehicleReport_Amount , ShortageOfLoadingStaffSupervisors_Amount , Net_Amount , Remarks
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDate).Value = objTr.Deduction_Date
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colShortageOfCrates_NoOfCrates).Value = objTr.SOC_NoOfCrates
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colShortageOfCrates_Amount).Value = objTr.SOC_NoOfCrates_Amount
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExcessLoadingOfMilk_Qty).Value = objTr.ELOM_QTY
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExcessLoadingOfMilk_Amount).Value = objTr.ELOM_Amount

                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_TopLess).Value = objTr.VC_TopLess
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_Logo).Value = objTr.VC_LOGO
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_InnerBodyPainting).Value = objTr.VC_InnerBodyPainting
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_Cleaniness).Value = objTr.VC_Cleaniness

                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_BottomDamage).Value = objTr.VC_BottomDamage
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_Shelf).Value = objTr.VC_Shelf
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_Light).Value = objTr.VC_Light
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVehicleCondition_TotalAmount).Value = objTr.VC_Amount
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLateVehicleReport).Value = objTr.LateVehicleReport_Amount
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colShortageOfLoadingStaffSupervisors).Value = objTr.ShortageOfLoadingStaffSupervisors_Amount
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTotalAmount).Value = objTr.Net_Amount
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks

                    Next
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub frmCustomerComplain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndUnpost.Visible = True
            End If
        End If
    End Sub
    'Sub LoadType()

    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))
    '    Dim dr As DataRow = dt.NewRow()

    '    dr("Code") = ""
    '    dr("Name") = "Select"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Quailty"
    '    dr("Name") = "Quailty"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Service"
    '    dr("Name") = "Service "
    '    dt.Rows.Add(dr)

    '    cboType.DataSource = dt
    '    cboType.ValueMember = "Code"
    '    cboType.DisplayMember = "Name"
    '    cboType.SelectedIndex = 0
    'End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    'Private Sub fndInvoice__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    strQuery = " Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [InvoiceDate] , TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], TSPL_Location_Master.Location_Desc as [Location Name] from TSPL_SD_SALE_INVOICE_HEAD " &
    '               " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
    '               " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
    '    Dim whr As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
    '    If clsCommon.myLen(fndTranspoter.Value) > 0 Then
    '        whr += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndTranspoter.Value + "'  "
    '    End If
    '    whr += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"
    '    fndInvoice.Value = clsCommon.ShowSelectForm("Invoice@Customer@Complain", strQuery, "InvoiceNo", whr, fndInvoice.Value, "InvoiceNo", isButtonClicked)
    '    lblInvocieDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
    '    fndTranspoter.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Customer_Code from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
    '    lblTranspoter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndTranspoter.Value + "' "))
    '    If clsCommon.myLen(fndInvoice.Value) > 0 Then
    '        funFillGrid(fndInvoice.Value)
    '    End If
    'End Sub

    'Private Sub fndCustom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTranspoter._MYValidating
    '    strQuery = " select Cust_Code as Code , Customer_Name as Name from TSPL_CUSTOMER_MASTER "
    '    fndTranspoter.Value = clsCommon.ShowSelectForm("CustomerMaster@Complain", strQuery, "Code", "", fndTranspoter.Value, "Code", isButtonClicked)
    '    lblTranspoter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndTranspoter.Value + "' "))
    '    LoadPreviousinvoice(isButtonClicked)
    'End Sub

    'Sub LoadPreviousinvoice(ByVal isButtonClick As Boolean)
    '    strQuery = " Select count (distinct TSPL_SD_SALE_INVOICE_HEAD.Document_Code) as InvoiceNo from TSPL_SD_SALE_INVOICE_HEAD " &
    '               " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
    '               " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
    '    Dim whr As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
    '    If clsCommon.myLen(fndTranspoter.Value) > 0 Then
    '        whr += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndTranspoter.Value + "'  "
    '    End If
    '    whr += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"
    '    Dim dblcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("" & strQuery & " where " & whr & " "))
    '    Dim strfndQuery As String = " Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [InvoiceDate] , TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], TSPL_Location_Master.Location_Desc as [Location Name] from TSPL_SD_SALE_INVOICE_HEAD " &
    '               " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
    '               " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
    '    Dim whrcls As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
    '    If clsCommon.myLen(fndTranspoter.Value) > 0 Then
    '        whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndTranspoter.Value + "'  "
    '    End If
    '    whrcls += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"

    '    If dblcount > 1 Then
    '        fndInvoice.Value = clsCommon.ShowSelectForm("Invoice@Customer@Complain", strfndQuery, "InvoiceNo", whrcls, fndInvoice.Value, "InvoiceNo", isButtonClick)
    '    ElseIf dblcount = 1 Then
    '        fndInvoice.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select InvoiceNo from (" & strfndQuery & " where " & whrcls & ")z"))
    '    Else
    '        fndInvoice.Value = ""
    '    End If

    '    lblInvocieDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
    '    fndTranspoter.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Customer_Code from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
    '    lblTranspoter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndTranspoter.Value + "' "))
    '    If clsCommon.myLen(fndInvoice.Value) > 0 Then
    '        funFillGrid(fndInvoice.Value)
    '    End If
    'End Sub
    'Private Function funComplainCode() As DataTable
    '    Dim dt As DataTable = Nothing
    '    strQuery = "select Code from TSPL_CUSTOMER_COMPLAINT_MASTER "
    '    Dim whr As String = ""
    '    If clsCommon.CompairString(cboType.SelectedValue, "") <> CompairStringResult.Equal Then
    '        whr = " Type = '" + cboType.SelectedValue + "' "
    '    End If
    '    dt = clsDBFuncationality.GetDataTable(strQuery)
    '    Return dt
    'End Function
    'Private Sub fndComplainCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    strQuery = "select Code ,Description,Type  from TSPL_CUSTOMER_COMPLAINT_MASTER "
    '    Dim whr As String = ""
    '    If clsCommon.CompairString(cboType.SelectedValue, "") <> CompairStringResult.Equal Then
    '        whr = " Type = '" + cboType.SelectedValue + "' "
    '    End If
    '    fndComplainCode.Value = clsCommon.ShowSelectForm("ComplainMaster@Complain", strQuery, "Code", whr, fndComplainCode.Value, "Code", isButtonClicked)
    '    lblComplainCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_CUSTOMER_COMPLAINT_MASTER where Code = '" + fndComplainCode.Value + "' "))
    '    If clsCommon.myLen(fndComplainCode.Value) > 0 Then
    '        cboType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Type from TSPL_CUSTOMER_COMPLAINT_MASTER where Code = '" + fndComplainCode.Value + "' "))
    '        cboType.ReadOnly = True
    '    End If
    'End Sub

    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try


            If e.Column Is Gv1.Columns(colShortageOfCrates_NoOfCrates) AndAlso isInsideLoadData = False Then
                If clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_NoOfCrates).Value) > 0 Then
                    'OpenUOMList(False)
                    Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_NoOfCrates).Value) * GetDeductionAmount("Shortage of crates", "No of crates")

                Else
                    Gv1.CurrentRow.Cells(colShortageOfCrates_NoOfCrates).Value = ""
                    Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value = ""
                End If
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)
            ElseIf e.Column Is Gv1.Columns(colShortageOfCrates_Amount) AndAlso isInsideLoadData = False Then
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)
            ElseIf e.Column Is Gv1.Columns(colExcessLoadingOfMilk_Qty) AndAlso isInsideLoadData = False Then
                If clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Qty).Value) > 0 Then

                    Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Qty).Value) * GetDeductionAmount("Excess loading of milk", "Qty(Ltrs)")

                Else
                    Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Qty).Value = ""
                    Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value = ""
                End If
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)
            ElseIf e.Column Is Gv1.Columns(colExcessLoadingOfMilk_Amount) AndAlso isInsideLoadData = False Then
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)

            ElseIf (e.Column Is Gv1.Columns(colVehicleCondition_TopLess) OrElse e.Column Is Gv1.Columns(colVehicleCondition_Logo) OrElse e.Column Is Gv1.Columns(colVehicleCondition_InnerBodyPainting) OrElse e.Column Is Gv1.Columns(colVehicleCondition_Cleaniness) OrElse e.Column Is Gv1.Columns(colVehicleCondition_BottomDamage) OrElse e.Column Is Gv1.Columns(colVehicleCondition_Shelf) OrElse e.Column Is Gv1.Columns(colVehicleCondition_Light)) AndAlso isInsideLoadData = False Then
                If clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) > 0 OrElse clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) > 0 Then
                    Gv1.CurrentRow.Cells(colVehicleCondition_TotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value)
                    ' Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)

                Else
                    Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value = ""
                End If
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)
            ElseIf e.Column Is Gv1.Columns(colLateVehicleReport) AndAlso isInsideLoadData = False Then
                ' Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)

            ElseIf e.Column Is Gv1.Columns(colShortageOfLoadingStaffSupervisors) AndAlso isInsideLoadData = False Then
                Gv1.CurrentRow.Cells(colTotalAmount).Value = clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colExcessLoadingOfMilk_Amount).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_TopLess).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Logo).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_InnerBodyPainting).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Cleaniness).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_BottomDamage).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Shelf).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colVehicleCondition_Light).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colLateVehicleReport).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfLoadingStaffSupervisors).Value) '+ clsCommon.myCdbl(Gv1.CurrentRow.Cells(colShortageOfCrates_Amount).Value)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub UpdateNetTotal()

    End Sub
    Public Function GetDeductionAmount(ByVal deduction_Category As String, ByVal Type As String) As Double
        Dim qry As String = " select Amount from TSPL_TRANSPOTER_DEDUCTION_DETAIL left outer join TSPL_TRANSPOTER_DEDUCTION_HEADER on TSPL_TRANSPOTER_DEDUCTION_DETAIL.DEDUCTION_CODE =  TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE where TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CATEGORY = '" + deduction_Category + "' and TYPE = '" + Type + "'"

        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    'Sub OpenUOMList(ByVal isButtonClick As Boolean)
    '    Dim strICode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells(colItemCode).Value)
    '    If clsCommon.myLen(strICode) > 0 Then
    '        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
    '        Dim whrCls As String = "Item_Code='" + strICode + "'"
    '        Gv1.CurrentRow.Cells(colDamageUOM).Value = clsCommon.ShowSelectForm("DamageUomfndnder", qry, "Code", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colDamageUOM).Value), "Code", isButtonClick)
    '    End If
    'End Sub

    'Public Shared Function GetItemConvQty(ByVal strItem As String, ByVal strCurrentUnit As String, ByVal strConvertedUnit As String, ByVal dblQty As Double) As Double
    '    Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strConvertedUnit & "'"))
    '    Dim dblConvQty As Double = 0
    '    If clsCommon.myLen(strConvertedUnit) > 0 Then
    '        Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
    '        If dblCurrentConvF > 0 Then
    '            dblConvQty = Math.Round(Math.Round((dblOrgConvF / dblCurrentConvF), 2) * dblQty, 6)
    '        End If
    '    End If
    '    Return dblConvQty
    'End Function

    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsTransporterDeductionHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTranspoter__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTranspoter._MYValidating
        Try
            Dim qry As String = " select Transport_Id as Code, Transporter_Name as Name from TSPL_TRANSPORT_MASTER"
            fndTranspoter.Value = clsCommon.ShowSelectForm("TransFnder@Finder@TransDeductionEntry", qry, "Code", "", fndTranspoter.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndTranspoter.Value) > 0 Then
                lblTranspoter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter_Name from tspl_transport_Master WHERE Transport_Id='" + fndTranspoter.Value + "'"))
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRoute._MYValidating
        'Try
        '    Dim qry As String = " Select Route_No as Code, Route_Desc as Name from TSPL_ROUTE_MASTER "
        '    fndRoute.Value = clsCommon.ShowSelectForm("TranspoterDeductionEntry@Fnd", qry, "Code", "", fndRoute.Value, "Code", isButtonClicked)
        '    If clsCommon.myLen(fndRoute.Value) > 0 Then
        '        lblRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_No from TSPL_ROUTE_MASTER WHERE Route_No='" + fndRoute.Value + "'"))
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        funFillGrid()
    End Sub

    Private Sub mulRoute__My_Click(sender As Object, e As EventArgs) Handles mulRoute._My_Click
        Try
            If clsCommon.myLen(fndTranspoter.Value) <= 0 Then
                fndTranspoter.Focus()
                Throw New Exception("Please select Transpoter")
            End If

            Dim qry As String = " select TSPL_ROUTE_MASTER.Route_No as Code, TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code = TSPL_VEHICLE_MASTER.Vehicle_Id  where TSPL_VEHICLE_MASTER.Transport_Id = '" + fndTranspoter.Value + "'"
            mulRoute.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "TranspoterDeduMulRoute@fnd", qry, "Code", "", mulRoute.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
