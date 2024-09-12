''created by Monika
Imports common
Imports System.Data.SqlClient


Public Class frmQCTemplateEntry
    Inherits FrmMainTranScreen

#Region "variables"
    Public AllowDeductionPers As Boolean = False
    Public QC_Type As String = Nothing
    Public FORMTYPE As String = Nothing
    Public Arr_Item As List(Of clsQualityCheckForSRNDetail) = Nothing
    Public Acceptedstatus As String = Nothing
    Public Template_Remarks As String = Nothing
    Public Template_Status As String = Nothing
    Public partial_rejected As Int16 = 0
    Public strVendorCode As String = Nothing

    Dim ButtonToolTip As New ToolTip()
    Dim isCellValueChanged As Boolean = False
    Public isInsideLoadData As Boolean = False
    Dim ReportID As String = "QCTempEn"

    Const colMandatorySelect As String = "MandatorySelect"
    Const colLineNo As String = "LineNo"
    Const colMRNNo As String = "MRNNO"
    Const colPONo As String = "PONO"
    Const colRowType As String = "RowType"
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colDrawingNo As String = "DrawingNo"
    Const colPartNo As String = "PartNo"
    Const colUnit As String = "Unit"
    Const colQty As String = "Qty"
    Const colParamCode As String = "ParamCode"
    Const colParamDesc As String = "ParamDesc"
    Const colDeductionPers As String = "colDeductionPers"
    Const colLRange As String = "Lrange"
    Const colURange As String = "URange"
    Const colStatus As String = "Status"
    Const colValue As String = "Value"
    Const colQCStatus As String = "QCStatus"
    Const colOkQty As String = "OkQty"
    Const colRejQty As String = "RejQty"
    Const colFinalRange1 As String = "FRange1"
    Const colFinalRange2 As String = "FRange2"
    Const colFinalRange3 As String = "FRange3"
    Const colFinalRange4 As String = "FRange4"
    Const colFinalRange5 As String = "FRange5"
    Const colFinalRange6 As String = "FRange6"
    Const colFinalRange7 As String = "FRange7"
    Const colFinalRange8 As String = "FRange8"
    Const colFinalRange9 As String = "FRange9"
    Const colFinalRange10 As String = "FRange10"
    Const colNetResult As String = "Netresult"
    Const colAutoValue As String = "AutoValue" 'pick from machine if connected
    Const colRemarks As String = "Remraks"
    Const colSpecification As String = "Specification"
    Const colInputData As String = "colInputData"
    Const colInputDataDeductionPer As String = "colInputDataDeductionPer"
    Public SettItemWiseQualityCheckInGeneralPurchase As Boolean = False
#End Region

    Private Sub LoadBlankItemGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoCombo As New GridViewComboBoxColumn()
        Dim repoChk As New GridViewCheckBoxColumn()

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colLineNo
        repoStr.Width = 70
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "MRN No."
        repoStr.Name = colMRNNo
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "PO NO"
        repoStr.Name = colPONo
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Type"
        repoStr.Name = colRowType
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colItemCode
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Description"
        repoStr.Name = colItemName
        repoStr.Width = 230
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "UOM"
        repoStr.Name = colUnit
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Drawing No"
        repoStr.Name = colDrawingNo
        repoStr.Width = 80
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Part No"
        repoStr.Name = colPartNo
        repoStr.Width = 80
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colQty
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        gv.MasterTemplate.Columns.Add(repoInt)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Param Code"
        repoStr.Name = colParamCode
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Specification"
        repoStr.Name = colParamDesc
        repoStr.Width = 120
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Deduction%"
        repoInt.Name = colDeductionPers
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        repoInt.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Lower Range"
        repoInt.Name = colLRange
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        repoInt.IsVisible = False
        repoInt.VisibleInColumnChooser = True
        gv.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Upper Range"
        repoInt.Name = colURange
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        repoInt.IsVisible = False
        repoInt.VisibleInColumnChooser = True
        gv.MasterTemplate.Columns.Add(repoInt)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Status"
        repoStr.Name = colStatus
        repoStr.Width = 80
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        repoStr.VisibleInColumnChooser = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Value"
        repoStr.Name = colValue
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        repoStr.VisibleInColumnChooser = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Standard" 'QC Status
        repoStr.Name = colQCStatus
        repoStr.Width = 80
        repoStr.ReadOnly = True
        repoStr.VisibleInColumnChooser = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Ok Quantity"
        repoInt.Name = colOkQty
        repoInt.DecimalPlaces = 2
        repoInt.Width = 80
        repoInt.ReadOnly = True
        'repoInt.ReadOnly = Not AllowDeductionPers
        repoInt.IsVisible = Not SettItemWiseQualityCheckInGeneralPurchase
        gv.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Reject Quantity"
        repoInt.Name = colRejQty
        repoInt.DecimalPlaces = 2
        repoInt.Width = 80
        repoInt.ReadOnly = True
        'repoInt.ReadOnly = Not AllowDeductionPers
        repoInt.IsVisible = Not SettItemWiseQualityCheckInGeneralPurchase
        gv.MasterTemplate.Columns.Add(repoInt)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observed Value"
        repoStr.Name = colFinalRange1
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.WrapText = True
        repoStr.ReadOnly = SettItemWiseQualityCheckInGeneralPurchase
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 2" 'Measurement
        repoStr.WrapText = True
        repoStr.Name = colFinalRange2
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 3"
        repoStr.WrapText = True
        repoStr.Name = colFinalRange3
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 4"
        repoStr.Name = colFinalRange4
        repoStr.WrapText = True
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 5"
        repoStr.Name = colFinalRange5
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 6"
        repoStr.Name = colFinalRange6
        repoStr.WrapText = True
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 7"
        repoStr.Name = colFinalRange7
        repoStr.WrapText = True
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 8"
        repoStr.WrapText = True
        repoStr.Name = colFinalRange8
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 9"
        repoStr.WrapText = True
        repoStr.Name = colFinalRange9
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Observation 10"
        repoStr.WrapText = True
        repoStr.Name = colFinalRange10
        repoStr.Width = 80
        repoStr.MaxLength = 300
        repoStr.VisibleInColumnChooser = False
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Net Result"
        repoStr.Name = colNetResult
        repoStr.WrapText = True
        repoStr.Width = 80
        repoStr.ReadOnly = True
        repoStr.MaxLength = 300
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Auto Observation"
        repoStr.WrapText = True
        repoStr.Name = colAutoValue
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.MaxLength = 300
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Reject Remarks"
        repoStr.Name = colSpecification
        repoStr.Width = 100
        repoStr.MaxLength = 200
        repoStr.TextAlignment = ContentAlignment.TopLeft
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Remarks"
        repoStr.Name = colRemarks
        repoStr.Width = 100
        repoStr.MaxLength = 200
        repoStr.TextAlignment = ContentAlignment.TopLeft
        gv.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Input Data"
        repoInt.Name = colInputData
        repoInt.DecimalPlaces = 4
        'repoInt.ReadOnly = True
        repoInt.IsVisible = SettItemWiseQualityCheckInGeneralPurchase
        repoInt.Width = 80
        gv.MasterTemplate.Columns.Add(repoInt)



        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Input Data Deduction Per"
        repoInt.Name = colInputDataDeductionPer
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.IsVisible = SettItemWiseQualityCheckInGeneralPurchase
        repoInt.Width = 80
        gv.MasterTemplate.Columns.Add(repoInt)

        repoChk = New GridViewCheckBoxColumn()
        repoChk.FormatString = ""
        repoChk.HeaderText = "Mandatory"
        repoChk.Name = colMandatorySelect
        repoChk.IsVisible = SettItemWiseQualityCheckInGeneralPurchase
        repoChk.Width = 60
        gv.MasterTemplate.Columns.Add(repoChk)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim InputDataDeductionPerSum As New GridViewSummaryItem(colInputDataDeductionPer, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(InputDataDeductionPerSum)
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        repoStr = Nothing
        repoInt = Nothing
        repoCombo = Nothing

        ReStoreGridLayout()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(FORMTYPE)
        'If Not (MyBase.isReadFlag) Then  by balwinder no need to user management.
        '    Throw New Exception("Permission Denied")
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub frmQCTemplateEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                btnNew.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                clsERPFuncationality.closeForm(Me)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmQCTemplateEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowDeductionPers = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDeductionPercentOnIncoming, clsFixedParameterCode.AllowDeductionPercentOnIncoming, Nothing)) = "1", True, False)
        SettItemWiseQualityCheckInGeneralPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1)
        If SettItemWiseQualityCheckInGeneralPurchase Then
            rbtnUD.Text = "Accept with Penalty"
            rbtnApp.Enabled = True
            rbtnRej.Enabled = True
            rbtnUD.Enabled = True
            gv.Columns(colSpecification).HeaderText = "Observation"
            gv.Columns(colRemarks).HeaderText = "Comments"
        End If
        If SettItemWiseQualityCheckInGeneralPurchase = True Then
            rbtnApp.Enabled = False
            rbtnRej.Enabled = False
            rbtnUD.Enabled = False
            btnRefreshParam.Visible = True
        Else
            btnRefreshParam.Visible = False
        End If

    End Sub

    Public Sub LoadEvent()
        SetUserMgmtNew()
        LoadBlankItemGrid()
        Funreset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")

        If clsCommon.CompairString(QC_Type, "Incoming") = CompairStringResult.Equal Then
            MyLabel4.Text = "INCOMING QUALITY CHECK"
        ElseIf clsCommon.CompairString(QC_Type, "Outgoing") = CompairStringResult.Equal Then
            MyLabel4.Text = "OUTGOING QUALITY CHECK"
        ElseIf clsCommon.CompairString(QC_Type, "InProcess") = CompairStringResult.Equal Then
            MyLabel4.Text = "IN-PROCESS QUALITY CHECK"
        End If

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Funreset()
    End Sub

    Private Sub Funreset()
        gv.Rows.Clear()
        btnsave.Text = "Save"
        txtDocNo.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtRemarks.Text = ""
        rbtnApp.IsChecked = False
        rbtnRej.IsChecked = False
        rbtnUD.IsChecked = False
        txtAccept.Visible = False
        If Not SettItemWiseQualityCheckInGeneralPurchase Then
            rbtnApp.Enabled = False
            rbtnRej.Enabled = False
            rbtnUD.Enabled = False
        End If
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsQualityCheckForSRNHead()
        Try
            Funreset()
            obj = clsQualityCheckForSRNHead.GetData(strCode, QC_Type, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isInsideLoadData = True

                txtDocNo.Value = obj.Document_Code
                dtpDate.Text = obj.Document_Date
                txtRemarks.Text = obj.Template_Remarks
                If SettItemWiseQualityCheckInGeneralPurchase Then
                    isInsdieToggle = True
                End If
                rbtnApp.IsChecked = IIf(obj.Template_Status = "A", True, False)
                rbtnRej.IsChecked = IIf(obj.Template_Status = "R", True, False)
                rbtnUD.IsChecked = IIf(obj.Template_Status = "U", True, False)
                chkStatus.Checked = IIf(obj.partial_rejected = 0, False, True)
                If SettItemWiseQualityCheckInGeneralPurchase Then
                    isInsdieToggle = False
                End If




                gv.Rows.Clear()

                If obj.Arr_item IsNot Nothing AndAlso obj.Arr_item.Count > 0 Then
                    For Each objtr As clsQualityCheckForSRNDetail In obj.Arr_item
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objtr.Line_no)
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRNNo).Value = objtr.MRN_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colPONo).Value = objtr.PO_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(objtr.Row_Type)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colDrawingNo).Value = objtr.Drawing_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colPartNo).Value = objtr.Part_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.MRN_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colDeductionPers).Value = clsQualityCheckForSRNHead.GetDeductionPers(objtr.QC_Param_Code, obj.Vendor_Code, objtr.Item_Code, clsCommon.myCstr(objtr.Measured_1), IIf((clsCommon.myLen(objtr.Param_Value) > 0 OrElse clsCommon.myLen(objtr.Param_Status) > 0), False, True), IIf(clsCommon.myLen(objtr.Param_Value) > 0, True, False), Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colParamCode).Value = objtr.QC_Param_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colParamDesc).Value = objtr.Param_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colLRange).Value = objtr.Param_L_Range
                        gv.Rows(gv.Rows.Count - 1).Cells(colURange).Value = objtr.Param_U_Range
                        gv.Rows(gv.Rows.Count - 1).Cells(colStatus).Value = objtr.Param_Status
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = objtr.Param_Value
                        gv.Rows(gv.Rows.Count - 1).Cells(colQCStatus).Value = objtr.Param_QC_Status
                        gv.Rows(gv.Rows.Count - 1).Cells(colOkQty).Value = objtr.Ok_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRejQty).Value = objtr.Reject_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange1).Value = objtr.Measured_1
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange2).Value = objtr.Measured_2
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange3).Value = objtr.Measured_3
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange4).Value = objtr.Measured_4
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange5).Value = objtr.Measured_5
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange6).Value = objtr.Measured_6
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange7).Value = objtr.Measured_7
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange8).Value = objtr.Measured_8
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange9).Value = objtr.Measured_9
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalRange10).Value = objtr.Measured_10
                        gv.Rows(gv.Rows.Count - 1).Cells(colNetResult).Value = objtr.Net_Measurement
                        gv.Rows(gv.Rows.Count - 1).Cells(colAutoValue).Value = objtr.Auto_Measured
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colSpecification).Value = objtr.Reject_Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colInputData).Value = objtr.InputData
                        gv.Rows(gv.Rows.Count - 1).Cells(colInputDataDeductionPer).Value = objtr.InputDataDeductionPer
                        gv.Rows(gv.Rows.Count - 1).Cells(colMandatorySelect).Value = IIf(objtr.Mandatory = 1, True, False)
                        If Not SettItemWiseQualityCheckInGeneralPurchase Then
                            UpdateQCStatus(gv.Rows(gv.Rows.Count - 1))
                        End If
                    Next
                End If

                btnsave.Text = "Update"

                txtAccept.Text = obj.QC_Status
                txtAccept.Visible = True

                If clsCommon.CompairString(obj.QC_Status, "Accepted") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Green
                ElseIf clsCommon.CompairString(obj.QC_Status, "Under Deviation") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Yellow
                    txtAccept.Enabled = True
                ElseIf clsCommon.CompairString(obj.QC_Status, "Rejected") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Red
                End If
            Else
                Funreset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            Dim qty As Decimal = Nothing
            Dim netqty As Decimal = Nothing
            Dim countr1 As Integer = 0
            Dim countr2 As Integer = 0
            Dim arr As New List(Of String)

            Dim icode As String = ""

            If rbtnRej.Enabled AndAlso rbtnUD.Enabled AndAlso rbtnUD.IsChecked AndAlso clsCommon.myLen(txtRemarks.Text) <= 0 Then
                txtRemarks.Focus()
                txtRemarks.Select()
                Throw New Exception("Fill remarks for under deviation.")
            End If

            For Each grow As GridViewRowInfo In gv.Rows
                icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                netqty = clsCommon.myCdbl(grow.Cells(colOkQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If Not arr.Contains(icode) Then
                        arr.Add(icode)
                    End If

                    If SettItemWiseQualityCheckInGeneralPurchase = True Then
                    Else
                        If qty <> netqty Then
                            gv.CurrentRow = gv.Rows(grow.Index)
                            Throw New Exception("sum of Ok and Reject quantity should be equal to " + clsCommon.myCstr(qty) + " at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myLen(grow.Cells(colStatus).Value) > 0 Then 'yes/no
                        If SettItemWiseQualityCheckInGeneralPurchase = True Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "NA") <> CompairStringResult.Equal Then
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill only Yes/No/NA observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange1).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "NA") <> CompairStringResult.Equal) Then
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill only Yes/No/NA observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange2).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange2).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange3).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange3).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange4).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange4).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange5).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange5).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange6).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange6).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange7).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange7).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange8).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange8).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange9).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange9).Value), "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange10).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange10).Value), "No") <> CompairStringResult.Equal Then
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill only Yes/No observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange1).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange1).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange2).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange2).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange2).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange3).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange3).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange3).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange4).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange4).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange4).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange5).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange5).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange5).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange6).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange6).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange6).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange7).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange7).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange7).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange8).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange8).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange8).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange9).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange9).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange9).Value), "No") <> CompairStringResult.Equal) OrElse (clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFinalRange10).Value)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange10).Value), "Yes") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFinalRange10).Value), "No") <> CompairStringResult.Equal) Then
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill only Yes/No observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        End If

                    ElseIf clsCommon.myLen(grow.Cells(colValue).Value) > 0 Then
                        Dim cnt As Integer = 0
                        For ii As Integer = 1 To 10
                            If clsCommon.myLen(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value) <= 0 Then
                                cnt += 1
                            End If
                        Next
                        If cnt = 10 Then
                            gv.CurrentRow = gv.Rows(grow.Index)
                            Throw New Exception("Select atleast one observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    Else
                        Dim cnt As Integer = 0
                        For ii As Integer = 1 To 10
                            If clsCommon.myLen(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value) <= 0 Then
                                cnt += 1
                            End If
                            If clsCommon.myLen(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value) > 0 AndAlso Not IsNumeric(clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value)) Then
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill numeric observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        Next
                        If cnt = 10 Then
                            gv.CurrentRow = gv.Rows(grow.Index)
                            Throw New Exception("Fill atleast one observation at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myCdbl(grow.Cells(colRejQty).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colSpecification).Value) <= 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        Throw New Exception("Fill rejection remarks at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If 'rej rem.

                End If
            Next

            If arr.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Item details found for QC process.", Me.Text)
                Return False
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData()
        Dim obj As New clsQualityCheckForSRNDetail()
        Arr_Item = New List(Of clsQualityCheckForSRNDetail)
        Try
            If AllowToSave() Then
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsQualityCheckForSRNDetail()

                    obj.Document_Code = clsCommon.myCstr(txtDocNo.Value)


                    obj.Line_no = CInt(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                    obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj.MRN_No = clsCommon.myCstr(grow.Cells(colMRNNo).Value)
                    obj.PO_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    obj.Row_Type = clsQualityCheckForSRNHead.CodeOfItemType(clsCommon.myCstr(grow.Cells(colRowType).Value))
                    obj.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    obj.MRN_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    obj.QC_Param_Code = clsCommon.myCstr(grow.Cells(colParamCode).Value)
                    obj.Param_L_Range = clsCommon.myCdbl(grow.Cells(colLRange).Value)
                    obj.Param_U_Range = clsCommon.myCdbl(grow.Cells(colURange).Value)
                    obj.Param_Status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                    obj.Param_Value = clsCommon.myCstr(grow.Cells(colValue).Value)
                    obj.Param_QC_Status = clsCommon.myCstr(grow.Cells(colQCStatus).Value)
                    obj.Ok_Qty = clsCommon.myCdbl(grow.Cells(colOkQty).Value)
                    obj.Reject_Qty = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
                    obj.Measured_1 = clsCommon.myCstr(grow.Cells(colFinalRange1).Value)
                    obj.Measured_2 = clsCommon.myCstr(grow.Cells(colFinalRange2).Value)
                    obj.Measured_3 = clsCommon.myCstr(grow.Cells(colFinalRange3).Value)
                    obj.Measured_4 = clsCommon.myCstr(grow.Cells(colFinalRange4).Value)
                    obj.Measured_5 = clsCommon.myCstr(grow.Cells(colFinalRange5).Value)
                    obj.Measured_6 = clsCommon.myCstr(grow.Cells(colFinalRange6).Value)
                    obj.Measured_7 = clsCommon.myCstr(grow.Cells(colFinalRange7).Value)
                    obj.Measured_8 = clsCommon.myCstr(grow.Cells(colFinalRange8).Value)
                    obj.Measured_9 = clsCommon.myCstr(grow.Cells(colFinalRange9).Value)
                    obj.Measured_10 = clsCommon.myCstr(grow.Cells(colFinalRange10).Value)
                    obj.Net_Measurement = clsCommon.myCstr(grow.Cells(colNetResult).Value)
                    obj.Auto_Measured = clsCommon.myCstr(grow.Cells(colAutoValue).Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    obj.Reject_Remarks = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    obj.InputData = clsCommon.myCdbl(grow.Cells(colInputData).Value)
                    obj.InputDataDeductionPer = clsCommon.myCdbl(grow.Cells(colInputDataDeductionPer).Value)
                    obj.Mandatory = IIf(clsCommon.myCBool(grow.Cells(colMandatorySelect).Value) = True, 1, 0)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        Arr_Item.Add(obj)
                    End If
                Next

                If Arr_Item Is Nothing OrElse Arr_Item.Count <= 0 Then
                    Throw New Exception("No data found for save.")
                End If

                Template_Remarks = txtRemarks.Text.Replace("'", "`")
                If rbtnApp.IsChecked Then
                    Template_Status = "A"
                ElseIf rbtnRej.IsChecked Then
                    Template_Status = "R"
                ElseIf rbtnUD.IsChecked Then
                    Template_Status = "U"
                End If
                If chkStatus.Checked = True Then
                    partial_rejected = 1
                Else
                    partial_rejected = 0
                End If

                Acceptedstatus = txtAccept.Text
                If Not SettItemWiseQualityCheckInGeneralPurchase Then
                    '' added funcionality 13/10/2017
                    If clsCommon.CompairString(txtAccept.Text, "Under Deviation") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Not able to save, Item is Under Deviation", Me.Text)
                    End If
                    '' End functionality
                End If


                clsERPFuncationality.closeForm(Me)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colFinalRange1) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange2) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange3) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange4) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange5) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange6) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange7) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange8) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange9) OrElse gv.CurrentColumn Is gv.Columns(colFinalRange10)) Then
                ''open value form
                If clsCommon.myLen(gv.CurrentRow.Cells(colValue).Value) > 0 Then
                    gv.CurrentCell.Value = clsCommon.myCstr(OpenParameterValueList(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentCell.Value)))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function OpenParameterValueList(ByVal code As String, ByVal strValue As String) As String
        Dim strRetValue As String = String.Empty
        Dim table_name As String = "TSPL_QC_LOG_SHEET_MASTER"

        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "))
        If cnt <> 0 Then
            Dim frm As FrmCheckBoxGrid = New FrmCheckBoxGrid()
            Dim strValueList() As String = strValue.Split(",")
            frm.qry = " select distinct Value as Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
            frm.qry = frm.qry
            frm.arrValue = New List(Of String)
            For i As Integer = 0 To strValueList.Count - 1
                frm.arrValue.Add(strValueList(i))
            Next
            frm.ShowDialog()

            If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
                For i As Integer = 0 To frm.arrValue.Count - 1
                    strRetValue = strRetValue & frm.arrValue(i).ToString
                    If i <> frm.arrValue.Count - 1 Then
                        strRetValue = strRetValue & ","
                    End If
                Next
            End If
        Else

            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from " + table_name + " where code='" & code & "' and nature='A'")) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
            End If
        End If
        Return strRetValue
    End Function

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If e.Column IsNot Nothing Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colValue).Value) > 0 Then
                    gv.CurrentRow.Cells(colFinalRange1).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange2).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange3).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange4).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange5).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange6).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange7).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange8).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange9).ReadOnly = True
                    gv.CurrentRow.Cells(colFinalRange10).ReadOnly = True
                Else
                    gv.CurrentRow.Cells(colFinalRange1).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange2).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange3).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange4).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange5).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange6).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange7).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange8).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange9).ReadOnly = False
                    gv.CurrentRow.Cells(colFinalRange10).ReadOnly = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    Dim countr1 As Integer = 0
                    Dim countr2 As Integer = 0
                    Dim decimalvalue As Decimal = 0

                    If e.Column Is gv.Columns(colOkQty) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colOkQty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) Then
                            gv.CurrentRow.Cells(colOkQty).Value = 0
                            gv.CurrentRow.Cells(colRejQty).Value = 0
                            Throw New Exception("Ok quantity cannot be greater than MRN quantity i.e. (" + clsCommon.myCstr(gv.CurrentRow.Cells(colQty).Value) + ")")
                        End If
                        gv.CurrentRow.Cells(colRejQty).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colOkQty).Value)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colRejQty) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colRejQty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) Then
                            gv.CurrentRow.Cells(colOkQty).Value = 0
                            gv.CurrentRow.Cells(colRejQty).Value = 0
                            Throw New Exception("Reject quantity cannot be greater than MRN quantity i.e. (" + clsCommon.myCstr(gv.CurrentRow.Cells(colQty).Value) + ")")
                        End If
                        gv.CurrentRow.Cells(colOkQty).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colRejQty).Value)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colFinalRange1) OrElse e.Column Is gv.Columns(colFinalRange2) OrElse e.Column Is gv.Columns(colFinalRange3) OrElse e.Column Is gv.Columns(colFinalRange4) OrElse e.Column Is gv.Columns(colFinalRange5) OrElse e.Column Is gv.Columns(colFinalRange6) OrElse e.Column Is gv.Columns(colFinalRange7) OrElse e.Column Is gv.Columns(colFinalRange8) OrElse e.Column Is gv.Columns(colFinalRange9) OrElse e.Column Is gv.Columns(colFinalRange10) Then
                        isCellValueChanged = True

                        If e.Column Is gv.Columns(colFinalRange1) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange1).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange2) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange2).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange3) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange3).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange4) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange4).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange5) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange5).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange6) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange6).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange7) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange7).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange8) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange8).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange9) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange9).Value), gv.CurrentRow.Index)
                        ElseIf e.Column Is gv.Columns(colFinalRange10) Then
                            GetResultedRange(clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colFinalRange10).Value), gv.CurrentRow.Index)
                        End If
                        UpdateQCStatus(gv.CurrentRow)
                        If SettItemWiseQualityCheckInGeneralPurchase = True Then
                            UpdateStausNewInput(gv.CurrentRow)
                        End If
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colInputData) Then
                        If SettItemWiseQualityCheckInGeneralPurchase = True Then
                            isCellValueChanged = True
                            'If clsCommon.myCdbl(gv.CurrentRow.Cells(colInputData).Value) > 0 Then
                            '    gv.CurrentRow.Cells(colOkQty).Value = 0
                            '    gv.CurrentRow.Cells(colRejQty).Value = 0
                            '    Throw New Exception("Ok quantity cannot be greater than MRN quantity i.e. (" + clsCommon.myCstr(gv.CurrentRow.Cells(colQty).Value) + ")")
                            'End If
                            'gv.CurrentRow.Cells(colRejQty).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colOkQty).Value)
                            'If SettItemWiseQualityCheckInGeneralPurchase = True Then
                            '    gv.CurrentRow.Cells(colInputDataDeductionPer).Value = 0
                            '    UpdateStausNew(gv.CurrentRow)
                            'End If
                            UpdateStausNewInput(gv.CurrentRow)

                            'Mapping work
                            Dim dtParameter As DataTable = Nothing
                            Dim str As String = "select * from TSPL_PARAMETER_MAPPING_QC
                            left outer join  TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER
                            on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code=TSPL_PARAMETER_MAPPING_QC.QC_Param_Code
                            where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'
                            and TSPL_PARAMETER_MAPPING_QC.Mapped_QC_Param_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value) + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Dim str1 As String = "select TSPL_PARAMETER_MAPPING_QC.QC_Param_Code,TSPL_PARAMETER_MAPPING_QC.Mapped_QC_Param_Code from TSPL_PARAMETER_MAPPING_QC  
                                        left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER
                                        on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code=TSPL_PARAMETER_MAPPING_QC.QC_Param_Code
                                        where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'"

                                Dim dtParam As DataTable = clsDBFuncationality.GetDataTable(str1)
                                If dtParam IsNot Nothing AndAlso dtParam.Rows.Count > 0 Then
                                    Dim TempTotal As Decimal = 0
                                    For ii As Integer = 0 To dtParam.Rows.Count - 1
                                        For jj As Integer = 0 To gv.Rows.Count - 1
                                            If clsCommon.CompairString(dtParam.Rows(ii).Item("Mapped_QC_Param_Code"), gv.Rows(jj).Cells(colParamCode).Value) = CompairStringResult.Equal Then
                                                TempTotal += clsCommon.myCDecimal(gv.Rows(jj).Cells(colInputData).Value)
                                                Exit For
                                            End If
                                        Next
                                    Next
                                    For kk As Integer = 0 To gv.Rows.Count - 1
                                        If clsCommon.CompairString(dtParam.Rows(0).Item("QC_Param_Code"), gv.Rows(kk).Cells(colParamCode).Value) = CompairStringResult.Equal Then
                                            gv.Rows(kk).Cells(colInputData).Value = TempTotal
                                            Dim grow As GridViewRowInfo = TryCast(gv.Rows(kk), GridViewRowInfo)
                                            UpdateStausNewInput(grow)
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If

                            isCellValueChanged = False
                        End If
                    ElseIf e.Column Is gv.Columns(colMandatorySelect) Then
                        If SettItemWiseQualityCheckInGeneralPurchase = True Then
                            isCellValueChanged = True
                            UpdateStausNewMandatory(gv.CurrentRow)
                            isCellValueChanged = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateStausNewInput(ByVal grow As GridViewRowInfo)
        Try
            If clsCommon.myCDecimal(grow.Cells(colInputData).Value) >= 0 AndAlso clsCommon.myCBool(grow.Cells(colMandatorySelect).Value) = True Then

                Dim qry As String = "select (TSPL_PARAMETER_RANGE_MASTER_QC.lower_range) as lower_range,(TSPL_PARAMETER_RANGE_MASTER_QC.upper_range) as upper_range,(TSPL_PARAMETER_RANGE_MASTER_QC.status) as status,(TSPL_PARAMETER_RANGE_MASTER_QC.value1) as value1,(TSPL_PARAMETER_RANGE_MASTER_QC.qc_status) as qc_status,(TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Per) as Deduction_Per
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio2
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3
                ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio3
                 ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Method
                 from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code and TSPL_PARAMETER_RANGE_MASTER_QC.trans_id='standard' left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code 
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code = '" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "'  
                and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_code='" + clsCommon.myCstr(grow.Cells(colParamCode).Value) + "'"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim TempInputData As Decimal = 0
                Dim TempDedPercentage As Decimal = 0
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    TempInputData = clsCommon.myCDecimal(grow.Cells(colInputData).Value)
                    If TempInputData >= clsCommon.myCDecimal(dt.Rows(0)("lower_range")) AndAlso TempInputData <= clsCommon.myCDecimal(dt.Rows(0)("upper_range")) Then
                        grow.Cells(colFinalRange1).Value = "Yes"
                        grow.Cells(colNetResult).Value = "Yes"
                        grow.Cells(colInputDataDeductionPer).Value = 0
                    ElseIf TempInputData >= clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range")) AndAlso TempInputData <= clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range")) Then
                        grow.Cells(colFinalRange1).Value = "Yes"
                        grow.Cells(colNetResult).Value = "Yes"
                        If clsCommon.myCDecimal(dt.Rows(0)("Deduction_Method")) = 0 Then
                            If clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range")) > clsCommon.myCDecimal(dt.Rows(0)("upper_range")) Then
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((TempInputData - clsCommon.myCDecimal(dt.Rows(0)("upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")), 2, MidpointRounding.AwayFromZero)
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range")) < clsCommon.myCDecimal(dt.Rows(0)("lower_range")) Then
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")), 2, MidpointRounding.AwayFromZero)
                            Else
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")), 2, MidpointRounding.AwayFromZero)
                            End If
                        Else
                            grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")), 2, MidpointRounding.AwayFromZero)
                        End If
                        ''''
                    ElseIf TempInputData >= clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range2")) AndAlso TempInputData <= clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range2")) Then
                        grow.Cells(colFinalRange1).Value = "Yes"
                        grow.Cells(colNetResult).Value = "Yes"
                        If clsCommon.myCDecimal(dt.Rows(0)("Deduction_Method")) = 0 Then
                            If clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range2")) > clsCommon.myCDecimal(dt.Rows(0)("upper_range")) Then
                                'grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((TempInputData - clsCommon.myCDecimal(dt.Rows(0)("upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2")), 2, MidpointRounding.AwayFromZero)
                                TempDedPercentage = ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range")) - clsCommon.myCDecimal(dt.Rows(0)("upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")))
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(TempDedPercentage + ((TempInputData - clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2"))), 2, MidpointRounding.AwayFromZero)
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range2")) < clsCommon.myCDecimal(dt.Rows(0)("lower_range")) Then
                                TempDedPercentage = ((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")))
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(TempDedPercentage + ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2"))), 2, MidpointRounding.AwayFromZero)
                            Else
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2")), 2, MidpointRounding.AwayFromZero)
                            End If
                        Else
                            grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2")), 2, MidpointRounding.AwayFromZero)
                        End If
                    ElseIf TempInputData >= clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range3")) AndAlso TempInputData <= clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range3")) Then
                        grow.Cells(colFinalRange1).Value = "Yes"
                        grow.Cells(colNetResult).Value = "Yes"
                        If clsCommon.myCDecimal(dt.Rows(0)("Deduction_Method")) = 0 Then
                            If clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range3")) > clsCommon.myCDecimal(dt.Rows(0)("upper_range")) Then
                                'grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((TempInputData - clsCommon.myCDecimal(dt.Rows(0)("upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio3")), 2, MidpointRounding.AwayFromZero)
                                TempDedPercentage = ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range")) - clsCommon.myCDecimal(dt.Rows(0)("upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")))
                                TempDedPercentage += ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range2")) - clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2")))
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(TempDedPercentage + ((TempInputData - clsCommon.myCDecimal(dt.Rows(0)("Deduction_upper_range2"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio3"))), 2, MidpointRounding.AwayFromZero)
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range3")) < clsCommon.myCDecimal(dt.Rows(0)("lower_range")) Then
                                TempDedPercentage = ((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio")))
                                TempDedPercentage += ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range")) - clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range2"))) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio2")))
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(TempDedPercentage + ((clsCommon.myCDecimal(dt.Rows(0)("Deduction_lower_range2")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio3"))), 2, MidpointRounding.AwayFromZero)
                            Else
                                grow.Cells(colInputDataDeductionPer).Value = System.Math.Round((clsCommon.myCDecimal(dt.Rows(0)("lower_range")) - TempInputData) * clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio3")), 2, MidpointRounding.AwayFromZero)
                            End If
                        Else
                            grow.Cells(colInputDataDeductionPer).Value = System.Math.Round(clsCommon.myCDecimal(dt.Rows(0)("Deduction_Ratio3")), 2, MidpointRounding.AwayFromZero)
                        End If
                        ''''
                    Else
                        grow.Cells(colFinalRange1).Value = "No"
                        grow.Cells(colNetResult).Value = "No"
                        grow.Cells(colInputDataDeductionPer).Value = 0
                    End If
                Else
                    grow.Cells(colFinalRange1).Value = "No"
                    grow.Cells(colNetResult).Value = "No"
                    grow.Cells(colInputDataDeductionPer).Value = 0
                End If
            ElseIf clsCommon.myCDecimal(grow.Cells(colInputData).Value) < 0 AndAlso clsCommon.myCBool(grow.Cells(colMandatorySelect).Value) = True Then
                grow.Cells(colFinalRange1).Value = "No"
                grow.Cells(colNetResult).Value = "No"
                grow.Cells(colInputDataDeductionPer).Value = 0
            ElseIf clsCommon.myCBool(grow.Cells(colMandatorySelect).Value) = False Then
                grow.Cells(colFinalRange1).Value = "Yes"
                grow.Cells(colNetResult).Value = "Yes"
                grow.Cells(colInputDataDeductionPer).Value = 0
            End If

            CalculateFinalStatus()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub UpdateStausNewMandatory(ByVal grow As GridViewRowInfo)
        Try
            If clsCommon.myCBool(grow.Cells(colMandatorySelect).Value) = True Then
                grow.Cells(colFinalRange1).Value = "No"
                grow.Cells(colNetResult).Value = "No"
                'grow.Cells(colInputData).Value = 0
                'grow.Cells(colInputDataDeductionPer).Value = 0
                UpdateStausNewInput(grow)
            Else
                grow.Cells(colFinalRange1).Value = "NA"
                grow.Cells(colNetResult).Value = "NA"
                'grow.Cells(colInputData).Value = 0
                'grow.Cells(colInputDataDeductionPer).Value = 0
            End If
            CalculateFinalStatus()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CalculateFinalStatus()
        Try
            Dim strTempResult As String = "Yes"
            Dim strTempUD As Boolean = False
            For Each grow1 As GridViewRowInfo In gv.Rows
                If clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(colNetResult).Value), "No") = CompairStringResult.Equal Then
                    strTempResult = "No"
                End If
                If clsCommon.myCdbl(grow1.Cells(colInputDataDeductionPer).Value) > 0 Then
                    strTempUD = True
                End If
                If clsCommon.myLen(grow1.Cells(colSpecification).Value) <= 0 Then
                    grow1.Cells(colSpecification).Value = clsCommon.myCstr(grow1.Cells(colStatus).Value)
                End If
            Next

            If clsCommon.CompairString(strTempResult, "Yes") = CompairStringResult.Equal Then
                If strTempUD = True Then
                    rbtnUD.IsChecked = True
                    rbtnApp.IsChecked = False
                    rbtnRej.IsChecked = False
                    txtAccept.Text = "Under Deviation"
                    txtAccept.BackColor = Color.Yellow
                    'chkStatus.Enabled = False
                ElseIf strTempUD = False Then
                    rbtnApp.IsChecked = True
                    rbtnUD.IsChecked = False
                    rbtnRej.IsChecked = False
                    txtAccept.Text = "Accepted"
                    txtAccept.BackColor = Color.Green
                    'chkStatus.Enabled = False
                End If

            ElseIf clsCommon.CompairString(strTempResult, "No") = CompairStringResult.Equal Then
                rbtnRej.IsChecked = True
                rbtnApp.IsChecked = False
                rbtnUD.IsChecked = False
                txtAccept.Text = "Rejected"
                txtAccept.BackColor = Color.Red
                'chkStatus.Enabled = True
            End If
            txtAccept.Visible = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub UpdateQCStatus(ByVal grow As GridViewRowInfo)
        Dim countr1 As Integer = 0
        Dim countr2 As Integer = 0
        Dim decimalvalue As Decimal
        GetResultedRange(clsCommon.myCstr(grow.Cells(colParamCode).Value), clsCommon.myCstr(grow.Cells(colFinalRange1).Value), grow.Index)

        If clsCommon.myLen(grow.Cells(colStatus).Value) > 0 Then
            For ii As Integer = 1 To 10
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value), "Yes") = CompairStringResult.Equal Then
                    countr1 += 1
                End If
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value), "No") = CompairStringResult.Equal Then
                    countr2 += 1
                End If
            Next
            If countr1 > countr2 Then
                grow.Cells(colNetResult).Value = "YES"
                grow.Cells(colNetResult).Style.BackColor = Color.Green
                grow.Cells(colNetResult).Style.ForeColor = Color.Green
            ElseIf countr1 < countr2 Then
                grow.Cells(colNetResult).Value = "NO"
                grow.Cells(colNetResult).Style.BackColor = Color.Red
                grow.Cells(colNetResult).Style.ForeColor = Color.Red
            ElseIf countr1 = countr2 Then
                grow.Cells(colNetResult).Value = "UD" 'under deviation
                grow.Cells(colNetResult).Style.BackColor = Color.Yellow
                grow.Cells(colNetResult).Style.ForeColor = Color.Yellow
            End If

        ElseIf clsCommon.myLen(grow.Cells(colValue).Value) > 0 Then
            Dim dt As New DataTable()
            dt.Columns.Add("Value", GetType(String))
            dt.Columns.Add("No", GetType(Integer))
            Dim dr As DataRow = Nothing
            Dim xvalue As String = ""

            For ii As Integer = 1 To 10
                If clsCommon.myLen(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value) > 0 Then
                    xvalue = clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value)
                    For jj As Integer = ii + 1 To 10
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value), clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(jj)).Value)) = CompairStringResult.Equal Then
                            countr1 += 1 'if duplicate found
                        End If
                    Next


                    dr = dt.NewRow()
                    dr("Value") = clsCommon.myCstr(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value)
                    dr("No") = countr1
                    dt.Rows.Add(dr)


                End If
            Next

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim no As Integer = CInt(clsCommon.myCstr(dt.Compute("max(no)", "")))

                grow.Cells(colNetResult).Value = clsCommon.myCstr(dt.Compute("max(Value)", "No='" + clsCommon.myCstr(no) + "'"))
            Else
                grow.Cells(colNetResult).Value = xvalue
            End If

            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colValue).Value), clsCommon.myCstr(grow.Cells(colNetResult).Value)) = CompairStringResult.Equal Then
                grow.Cells(colNetResult).Style.BackColor = Color.Green
            Else
                grow.Cells(colNetResult).Style.BackColor = Color.Red
            End If
        Else 'range
            decimalvalue = 0
            For ii As Integer = 1 To 10
                If clsCommon.myCdbl(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value) <> 0 Then
                    countr1 += 1
                    decimalvalue += clsCommon.myCdbl(grow.Cells("FRange" + clsCommon.myCstr(ii)).Value)
                End If
            Next

            If countr1 > 0 Then
                decimalvalue = System.Math.Round(decimalvalue / countr1, 2)
            End If
            grow.Cells(colNetResult).Value = clsCommon.myCstr(decimalvalue)

            If decimalvalue >= clsCommon.myCdbl(grow.Cells(colLRange).Value) AndAlso decimalvalue <= clsCommon.myCdbl(grow.Cells(colURange).Value) AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCStatus).Value), "OK") = CompairStringResult.Equal Then
                grow.Cells(colNetResult).Style.BackColor = Color.Green
                grow.Cells(colNetResult).Style.ForeColor = Color.Green
            ElseIf decimalvalue <> clsCommon.myCdbl(grow.Cells(colLRange).Value) AndAlso decimalvalue <> clsCommon.myCdbl(grow.Cells(colURange).Value) AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCStatus).Value), "NOT OK") = CompairStringResult.Equal Then
                grow.Cells(colNetResult).Style.BackColor = Color.Red
                grow.Cells(colNetResult).Style.ForeColor = Color.Red
            Else
                grow.Cells(colNetResult).Style.BackColor = Color.Yellow
                grow.Cells(colNetResult).Style.ForeColor = Color.Yellow
            End If
        End If ''status cond.

        QualityCheckStatus()
    End Sub

    Private Sub QualityCheckStatus()
        Try
            Dim cuntr_GRN As Integer = 0
            Dim cuntr_RED As Integer = 0
            Dim cuntr_YLW As Integer = 0

            For Each grow As GridViewRowInfo In gv.Rows
                If grow.Cells(colNetResult).Style.BackColor = Color.Green Then
                    cuntr_GRN += 1 'accepted
                ElseIf grow.Cells(colNetResult).Style.BackColor = Color.Red Then
                    cuntr_RED += 1 'rejected
                ElseIf grow.Cells(colNetResult).Style.BackColor = Color.Yellow Then
                    cuntr_YLW += 1 'under deviation
                End If
            Next
            If Not SettItemWiseQualityCheckInGeneralPurchase Then
                rbtnApp.Enabled = False
                rbtnRej.Enabled = False
                rbtnUD.Enabled = False
            End If

            rbtnApp.IsChecked = False
            rbtnRej.IsChecked = False
            rbtnUD.IsChecked = False

            txtAccept.Visible = True
            If cuntr_GRN > cuntr_RED AndAlso cuntr_GRN > cuntr_YLW Then
                txtAccept.Text = "Accepted"
                txtAccept.BackColor = Color.Green
                If Not SettItemWiseQualityCheckInGeneralPurchase Then
                    rbtnApp.Enabled = True
                    rbtnRej.Enabled = False
                    rbtnUD.Enabled = False
                End If
                rbtnApp.IsChecked = True
                rbtnRej.IsChecked = False
                rbtnUD.IsChecked = False
            End If
            If cuntr_RED > cuntr_GRN AndAlso cuntr_RED > cuntr_YLW Then
                txtAccept.Text = "Rejected"
                txtAccept.BackColor = Color.Red
                If Not SettItemWiseQualityCheckInGeneralPurchase Then
                    rbtnApp.Enabled = False
                    rbtnRej.Enabled = True
                    rbtnUD.Enabled = True
                End If

                rbtnApp.IsChecked = False
                rbtnRej.IsChecked = True
                rbtnUD.IsChecked = False
            End If
            If (cuntr_YLW > cuntr_GRN AndAlso cuntr_YLW > cuntr_RED) Then
                txtAccept.Text = "Under Deviation"
                txtAccept.BackColor = Color.Yellow
                If Not SettItemWiseQualityCheckInGeneralPurchase Then
                    rbtnApp.Enabled = False
                    rbtnRej.Enabled = False
                    rbtnUD.Enabled = True
                End If
                rbtnApp.IsChecked = False
                rbtnRej.IsChecked = False
                rbtnUD.IsChecked = True
            End If
            If (cuntr_GRN > 0 AndAlso (cuntr_GRN = cuntr_RED OrElse cuntr_GRN = cuntr_YLW)) OrElse (cuntr_RED > 0 AndAlso (cuntr_GRN = cuntr_RED OrElse cuntr_RED = cuntr_YLW)) OrElse (cuntr_YLW > 0 AndAlso (cuntr_GRN = cuntr_YLW OrElse cuntr_RED = cuntr_YLW)) Then
                txtAccept.Text = "Under Deviation"
                txtAccept.BackColor = Color.Yellow
                If Not SettItemWiseQualityCheckInGeneralPurchase Then
                    rbtnApp.Enabled = False
                    rbtnRej.Enabled = False
                    rbtnUD.Enabled = True
                End If
                rbtnApp.IsChecked = False
                rbtnRej.IsChecked = False
                rbtnUD.IsChecked = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub GetResultedRange(ByVal Param_Code As String, ByVal Observed_Range As String, ByVal IntRow As Integer)
        Try
            Dim qry As String = "select (TSPL_PARAMETER_RANGE_MASTER_QC.lower_range) as lower_range,(TSPL_PARAMETER_RANGE_MASTER_QC.upper_range) as upper_range,(TSPL_PARAMETER_RANGE_MASTER_QC.status) as status,(TSPL_PARAMETER_RANGE_MASTER_QC.value1) as value1,(TSPL_PARAMETER_RANGE_MASTER_QC.qc_status) as qc_status,(TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Per) as Deduction_Per from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code and TSPL_PARAMETER_RANGE_MASTER_QC.trans_id='standard' left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.document_code"
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code where TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code = '" + clsCommon.myCstr(gv.Rows(IntRow).Cells(colItemCode).Value) + "' and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code='" + strVendorCode + "' and TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code='" + Param_Code + "' "
            If clsCommon.myLen(gv.Rows(IntRow).Cells(colStatus).Value) > 0 Then
                qry += " and TSPL_PARAMETER_RANGE_MASTER_QC.status='" + Observed_Range + "' "
            ElseIf clsCommon.myLen(gv.Rows(IntRow).Cells(colValue).Value) > 0 Then
                qry += " and TSPL_PARAMETER_RANGE_MASTER_QC.value1 like '%" + Observed_Range + "%' "
            Else
                qry += " and '" + Observed_Range + "' >= TSPL_PARAMETER_RANGE_MASTER_QC.lower_range and '" + Observed_Range + "' <= TSPL_PARAMETER_RANGE_MASTER_QC.upper_range "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.Rows(IntRow).Cells(colLRange).Value = clsCommon.myCdbl(dt.Rows(0)("lower_range"))
                gv.Rows(IntRow).Cells(colURange).Value = clsCommon.myCdbl(dt.Rows(0)("upper_range"))
                gv.Rows(IntRow).Cells(colStatus).Value = clsCommon.myCstr(dt.Rows(0)("status"))
                gv.Rows(IntRow).Cells(colValue).Value = clsCommon.myCstr(dt.Rows(0)("value1"))
                gv.Rows(IntRow).Cells(colQCStatus).Value = clsCommon.myCstr(dt.Rows(0)("qc_status"))
                gv.Rows(IntRow).Cells(colDeductionPers).Value = clsCommon.myCdbl(dt.Rows(0)("Deduction_Per"))
            End If

            gv.Rows(IntRow).Cells("OkQty").Value = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colQty).Value) - System.Math.Round(clsCommon.myCdbl(gv.Rows(IntRow).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(IntRow).Cells(colDeductionPers).Value) / 100, 2)
            gv.Rows(IntRow).Cells("RejQty").Value = System.Math.Round(clsCommon.myCdbl(gv.Rows(IntRow).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(IntRow).Cells(colDeductionPers).Value) / 100, 2)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnApp_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnApp.ToggleStateChanged
        'ToggleRadioButton()
    End Sub

    Private Sub rbtnRej_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnRej.ToggleStateChanged
        'ToggleRadioButton()
    End Sub

    Private Sub rbtnUD_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnUD.ToggleStateChanged
        'ToggleRadioButton()
    End Sub

    Dim isInsdieToggle As Boolean = False
    Sub ToggleRadioButton()
        If SettItemWiseQualityCheckInGeneralPurchase Then

            If Not isInsdieToggle Then
                isInsideLoadData = True
                isInsdieToggle = True
                Dim val As String = "Yes"
                If rbtnRej.IsChecked Then
                    val = "No"
                End If
                For ii As Integer = 0 To gv.Rows.Count - 1
                    gv.Rows(ii).Cells(colNetResult).Value = val
                    'colFinalRange1
                    gv.Rows(ii).Cells(colFinalRange1).Value = val
                    If clsCommon.myLen(gv.Rows(ii).Cells(colSpecification).Value) <= 0 Then
                        gv.Rows(ii).Cells(colSpecification).Value = gv.Rows(ii).Cells(colStatus).Value
                    End If
                Next
                If rbtnApp.IsChecked Then
                    txtAccept.Text = "Accepted"
                ElseIf rbtnRej.IsChecked Then
                    txtAccept.Text = "Rejected"
                ElseIf rbtnUD.IsChecked Then
                    txtAccept.Text = "Under Deviation"
                Else
                    txtAccept.Text = ""
                End If




                'QualityCheckStatus()
                isInsdieToggle = False
                isInsideLoadData = False
            End If
        End If
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New System.IO.MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try

            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub btnRefreshParam_Click(sender As Object, e As EventArgs) Handles btnRefreshParam.Click
        Try
            Dim strQry As String = ""
            Dim strRangeDesc As String = ""
            For kk As Integer = 0 To gv.Rows.Count - 1
                strQry = "select (convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3)) as qc_status
              from TSPL_PARAMETER_RANGE_MASTER_QC where QC_Param_Code='" + clsCommon.myCstr(gv.Rows(kk).Cells(colParamCode).Value) + "'"
                strRangeDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry))
                gv.Rows(kk).Cells(colQCStatus).Value = strRangeDesc
                gv.Rows(kk).Cells(colStatus).Value = strRangeDesc
                gv.Rows(kk).Cells(colSpecification).Value = strRangeDesc
                Dim grow As GridViewRowInfo = TryCast(gv.Rows(kk), GridViewRowInfo)
                UpdateStausNewInput(grow)
            Next
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub txtAccept_TextChanged(sender As Object, e As EventArgs) Handles txtAccept.TextChanged
        Try
            If txtAccept.Text = "Accepted" Then
                chkStatus.Enabled = False
                chkStatus.Checked = False
            ElseIf txtAccept.Text = "Rejected" Then
                chkStatus.Enabled = True
            Else
                chkStatus.Enabled = True
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub
End Class
