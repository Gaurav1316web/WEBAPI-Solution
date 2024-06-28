Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System.Globalization
Imports System.Text.RegularExpressions
'Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq

Public Class frmDemandUploader
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Const colSNo As String = "colSNo"
    Const colQtyIn As String = "colQtyIn"
    Const colRoute As String = "colRoute"
    Const colBooth As String = "colBooth"
    Const colItemCode As String = "colItemCode"
    Const colTM500 As String = "colTM500"
    Const colTM1LT As String = "colTM1LT"
    Const colSM500 As String = "colSM500"
    Const colSM1LT As String = "colSM1LT"
    Const colGM500 As String = "colGM500"
    Const colGM1LT As String = "colGM1LT"
    Const colCHHACH As String = "colCHHACH"
    Const colTM6LT As String = "colTM6LT"
    Const colGM6LT As String = "colGM6LT"
    Const colSL400 As String = "colSL400"
    Const colSL6LT As String = "colSL6LT"
    Const colTotalAmount As String = "colTotalAmount"
    Dim lstObj As List(Of clsDemandUploader)
    Dim lstDUD As List(Of clsDemandUploaderDetails)
    Dim DU_No As String = ""
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If


    End Sub
    Private Sub frmDemandUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.isDeleteTheAttachment = False
        UcAttachment1.settAutoAttachment = True
        'UcAttachment1.MandatoryPDFFileAny = True
        'CreateTable()
        SetUserMgmtNew()
        AddNew()
    End Sub
    Public Sub AddNew()
        isNewEntry = True
        UcAttachment1.BlankAllControls()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnMorning.IsChecked = True
        'gv1.Rows.Clear()
        'gv1.Columns.Clear()
        gv1.DataSource = Nothing
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        EnableDisable(False)
        DU_No = ""
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub EnableDisable(ByVal flag As Boolean)
        btnUpload.Enabled = Not flag
        btnValidate.Enabled = flag
        btnSavePost.Enabled = flag
        txtDate.Enabled = Not flag
        txtLocation.Enabled = Not flag
        rgbShift.Enabled = Not flag
    End Sub
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Import()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Import()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsDemandUploader)
            lstObj = New List(Of clsDemandUploader)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "S.No.", "DATE", "Shift", "Qty In", "Route", "Booth", "TM 500", "TM 1LT", "SM 500", "SM 1LT", "GM 500", "GM 1LT", "CHHACH", "TM 6LT", "GM 6LT", "SL400", "SL6 LT", "Total Amount") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try
                    clsCommon.ProgressBarShow()
                    For Each item As GridViewColumn In gv.Columns
                        Dim Arr As New clsDemandUploader
                        Arr.Key = item.HeaderText
                        Arr.lstValue = New List(Of clsDemandItemDetails)
                        For Each row As GridViewRowInfo In gv.Rows
                            Dim objtr As New clsDemandItemDetails
                            If Not TypeOf row Is GridViewNewRowInfo Then
                                If Not String.IsNullOrEmpty(row.Cells(item.Name).Value.ToString()) Then
                                    objtr.Value = row.Cells(item.Name).Value.ToString()
                                    Arr.lstValue.Add(objtr)
                                End If
                            End If
                        Next
                        obj.Add(Arr)
                        lstObj.Add(Arr)
                    Next
                    clsCommon.ProgressBarHide()
                    Dim dt As New DataTable
                    For Each items As clsDemandUploader In obj
                        dt.Columns.Add(items.Key)
                    Next
                    Dim maxRows As Integer = obj.Max(Function(x) x.lstValue.Count)
                    For i As Integer = 0 To maxRows - 1
                        Dim row As DataRow = dt.NewRow()
                        For jj As Integer = 0 To obj.Count - 1
                            row(obj(jj).Key) = obj(jj).lstValue(i).Value
                        Next
                        dt.Rows.Add(row)
                    Next
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.DataSource = dt
                    gv1.AllowDeleteRow = False
                    gv1.AllowAddNewRow = False
                    ' For ii As Integer = 0 To gv1.Rows.Count
                    For jj As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(jj).ReadOnly = True
                    Next
                    'Next
                    EnableDisable(True)
                    btnSavePost.Enabled = False
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            Else
                Throw New Exception("Excel Sheet is not in expected format")
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            ValidateGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub ValidateGrid()
        Try
            btnSavePost.Enabled = False
            Dim mess As String = ""
            Dim lineNo As Integer = 1
            lstDUD = New List(Of clsDemandUploaderDetails)
            clsCommon.ProgressBarShow()
            For Each grow As GridViewRowInfo In gv1.Rows
                clsCommon.ProgressBarUpdate("Validating, Please wait..." & (grow.Index + 1) & "/" & gv1.Rows.Count)
                If (Not String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("S.No.").Value))) Then
                    If (ValidateBooth(grow.Cells("Route").Value, grow.Cells("Booth").Value)) Then
                        If (ValidateDate(clsCommon.GetPrintDate(grow.Cells("DATE").Value))) Then
                            If (ValidateShift(grow.Cells("Shift").Value)) Then
                                Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(grow.Cells("Route").Value) + "' and convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value) + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "'"))
                                If clsCommon.myLen(DocumentNo) > 0 Then
                                    Throw New Exception("Document already created for Route No -" & clsCommon.myCstr(grow.Cells("Route").Value) & " Shift " + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "")
                                End If
                                For i As Integer = 6 To lstObj.Count - 2
                                    Dim objtr As New clsDemandUploaderDetails
                                    objtr.Qty = clsCommon.myCdbl(grow.Cells(clsCommon.myCstr(lstObj(i).Key)).Value)
                                    If objtr.Qty > 0 Then
                                        objtr.Unit_Code = clsCommon.myCstr(grow.Cells("Qty In").Value)
                                        objtr.Route = clsCommon.myCstr(grow.Cells("Route").Value)
                                        objtr.Booth = clsCommon.myCstr(grow.Cells("Booth").Value)
                                        objtr.Vehicle_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select vehicle_id from TSPL_VEHICLE_MASTER left join tspl_route_master on tspl_route_master.vehicle_code=TSPL_VEHICLE_MASTER.vehicle_id where tspl_route_master.route_no ='" & objtr.Route & "'"))
                                        objtr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_ITEM_MASTER where Short_Description='" + lstObj(i).Key + "'"))
                                        'objtr.Qty = clsCommon.myCdbl(grow.Cells(clsCommon.myCstr(lstObj(i).Key)).Value)
                                        objtr.Price_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" & objtr.Booth & "'"))
                                        objtr.Item_Rate = GetItemRate(objtr.Price_code, objtr.Unit_Code, objtr.Item_Code, lineNo, objtr.Booth, clsCommon.myCstr(lstObj(i).Key))
                                        objtr.ItemNetAmount = objtr.Qty * objtr.Item_Rate
                                        Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objtr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                        Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objtr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objtr.Unit_Code) & "' "))
                                        Dim dblTotalLitreRowWise As Double = 0
                                        Dim dblTotalCrateRowWise As Double = 0
                                        If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objtr.Qty) * ItemConvFactor_Ltr
                                            If DispatchQty >= CrateConvFactor_Ltr Then
                                                dblTotalLitreRowWise = Math.Floor(DispatchQty / CrateConvFactor_Ltr)
                                            Else
                                                dblTotalLitreRowWise = 0
                                            End If

                                        End If
                                        objtr.TotalLtr_ItemWise = 0
                                        If clsCommon.CompairString(objtr.Unit_Code, "Crate") = CompairStringResult.Equal Then
                                            objtr.TotalCrates_ItemWise = objtr.Qty
                                        End If
                                        lstDUD.Add(objtr)
                                    End If
                                Next
                            Else
                                mess += " Error at Line No:" + clsCommon.myCstr(lineNo) + " Shift Type Mismatched " & Environment.NewLine

                            End If
                        Else
                            mess += " Error at Line No:" + clsCommon.myCstr(lineNo) + " Date Mismatched " & Environment.NewLine

                        End If
                        'objtr.Sno = lineNo


                    Else
                        mess += " Error at Line No:" + clsCommon.myCstr(lineNo) + " Route: [ " + clsCommon.myCstr(grow.Cells("Route").Value) + " ] Booth: [ " + clsCommon.myCstr(grow.Cells("Booth").Value) + " ] does not exist in ERP" & Environment.NewLine
                    End If
                    lineNo += 1
                End If
            Next
            clsCommon.ProgressBarHide()
            If Not String.IsNullOrEmpty(mess) Then
                Throw New Exception(mess)
                btnSavePost.Enabled = False
            Else
                btnSavePost.Enabled = True
                btnValidate.Enabled = False
                clsCommon.MyMessageBoxShow(Me, "Validate Successfully.", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()

            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function ValidateBooth(ByVal Route As String, ByVal BoothCode As String) As Boolean
        Try
            Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code='" + BoothCode + "' and Route_No='" + Route + "' and Status='N'"))
            If count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ValidateShift(ByVal Shift As String) As Boolean
        Try
            Dim ShiftType As String = ""

            If rbtnMorning.IsChecked Then
                ShiftType = "M"
            ElseIf rbtnEvening.IsChecked Then
                ShiftType = "E"

            End If
            If Not (clsCommon.CompairString(Shift, ShiftType) = CompairStringResult.Equal) Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ValidateDate(ByVal DocDate As DateTime) As Boolean
        Try
            If DocDate = clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Sub btnSavePost_Click(sender As Object, e As EventArgs) Handles btnSavePost.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SaveData()
        Try



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Docno As New List(Of String)
            Dim status As Boolean = False
            Dim groupedByRoute = From obj In lstDUD
                                 Group obj By obj.Route Into Group
                                 Select Route, Group
            clsCommon.ProgressBarShow()
            For Each routeGroup In groupedByRoute
                Dim obj As New clsDemandBookingSale()
                Dim route As String = routeGroup.Route
                Dim shift As Integer = 0
                Dim items As IEnumerable(Of clsDemandUploaderDetails) = routeGroup.Group
                clsCommon.ProgressBarUpdate("Saving Data for Route No - " & route & ", Please Wait...")

                obj.Route_No = route
                obj.Document_Date = txtDate.Value
                obj.Location_Code = txtLocation.Value
                If rbtnMorning.IsChecked Then
                    obj.ShiftType = "Morning"
                    shift = 1
                ElseIf rbtnEvening.IsChecked Then
                    obj.ShiftType = "Evening"
                    shift = 2
                End If
                obj.ItemType = "Fresh"
                obj.IsIndividualCustomer = 0
                obj.City_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Code from TSPL_ROUTE_MASTER where Route_No='" & route & "'", trans))

                obj.Arr = New List(Of clsDemandBookingSaleDetail)
                Dim docamt As Double = 0
                Dim LineNo As Integer = 1
                For Each item In items
                    Dim objTr As New clsDemandBookingSaleDetail()
                    objTr.Line_No = LineNo
                    objTr.Trip_No = 1
                    objTr.Cust_Code = item.Booth
                    objTr.ShiftType = obj.ShiftType
                    objTr.Item_Code = item.Item_Code
                    objTr.Unit_code = item.Unit_Code
                    objTr.Qty = item.Qty
                    objTr.Rate = item.Item_Rate
                    objTr.ItemNetAmount = item.ItemNetAmount
                    objTr.Vehicle_Code = item.Vehicle_Code
                    objTr.Price_Code = item.Price_code
                    objTr.TotalLtr_ItemWise = item.TotalLtr_ItemWise
                    objTr.TotalCrates_ItemWise = item.TotalCrates_ItemWise
                    docamt += item.ItemNetAmount
                    LineNo += 1
                    obj.Arr.Add(objTr)
                Next
                obj.DocumentAmount = docamt
                If clsDemandBookingSale.SaveData(obj, True, trans) Then
                    Docno.Add(obj.Document_No)
                    status = clsDemandBookingSale.PostData(Me.Form_ID, obj.Document_No, shift, False, trans)
                Else
                    Throw New Exception("Error : Route :" + route)
                End If
            Next

            If Docno IsNot Nothing AndAlso Docno.Count > 0 Then
                Dim obj As clsDemandUploaderSave
                If isNewEntry Then
                    Dim Filename As String = clsCommon.MyExportToExcelGridPath("Demand Uploader", gv1, Nothing, Me.Text, False, "", "")
                    Dim SafeFileName As String = "DemandUploader.xls"
                    UcAttachment1.AddAttachment(Filename, SafeFileName)
                    obj = New clsDemandUploaderSave
                    obj.Document_Date = txtDate.Value
                    If rbtnMorning.IsChecked Then
                        obj.ShiftType = "Morning"
                    ElseIf rbtnEvening.IsChecked Then
                        obj.ShiftType = "Evening"
                    End If
                    obj.Location_Code = txtLocation.Value

                    obj.SaveData(obj, isNewEntry, trans)
                    UcAttachment1.SaveData(obj.Document_No, False, trans)
                    DU_No = obj.Document_No
                    txtDocNo.Value = DU_No

                End If
                If clsCommon.myLen(DU_No) > 0 Then
                    Dim updateDoc As String = "update TSPL_DEMAND_BOOKING_MASTER set UploderDocNo='" + DU_No + "' where Document_No in(" + clsCommon.GetMulcallString(Docno) + ")"
                    clsDBFuncationality.ExecuteNonQuery(updateDoc, trans)
                End If
            End If

            clsCommon.ProgressBarHide()
            trans.Commit()


            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            btnSavePost.Enabled = False
        Catch ex As Exception
            clsCommon.ProgressBarHide()

            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function GetItemRate(ByVal strPriceCode As String, ByVal Unit_Code As String, ByVal Item_Code As String, ByVal LineNo As Integer, ByVal Booth As String, ByVal ItemDesc As String) As Double
        Dim ItemRate As Double = 0
        Try
            Dim qry As String = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                   " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                   "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                   " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                   " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                   " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                   " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                   "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                   "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                   "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                   "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                   " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                   "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                   "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                   "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & Unit_Code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                   ") XXXE WHERE RowNo=1  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ItemRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                If ItemRate = 0 Then
                    Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(ItemDesc) & " at LineNo -" & clsCommon.myCstr(LineNo) & Environment.NewLine)
                End If
                Return ItemRate
            Else
                Throw New Exception("Please create Price chart for Customer " & Booth & " , item " & ItemDesc & " at LineNo - " & clsCommon.myCstr(LineNo))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ItemRate
    End Function
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
        '        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        '        Try
        '            Dim strqry As String = "select max(TSPL_BOOKING_MATSER_Hist_Data.Against_DemandBooking_No) as Against_DemandBooking_No,max(TSPL_BOOKING_DETAIL_Hist_Data.Against_DemandBooking_TR_Code) as TR_Code,max(TSPL_BOOKING_DETAIL_Hist_Data.Line_No) as Line_NO,TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code as Cust_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name,
        'TSPL_BOOKING_DETAIL_Hist_Data.Item_Code,max(TSPL_ITEM_MASTER.Short_Description) as ShortDesc,
        'TSPL_BOOKING_DETAIL_Hist_Data.Unit_code,
        'max(TSPL_BOOKING_DETAIL_Hist_Data.Booking_Qty) as Booking_Qty,
        'max(TSPL_BOOKING_DETAIL_Hist_Data.route_no) as Route_No

        'from TSPL_BOOKING_MATSER_Hist_Data
        'left join TSPL_BOOKING_DETAIL_Hist_Data on TSPL_BOOKING_MATSER_Hist_Data.Document_No=TSPL_BOOKING_DETAIL_Hist_Data.Document_No
        'left join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL_Hist_Data.Item_Code=TSPL_ITEM_MASTER.Item_Code
        'left join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
        'where convert(date,TSPL_BOOKING_MATSER_Hist_Data.Document_Date,103)='28-Jun-2024' and TSPL_BOOKING_MATSER_Hist_Data.GatePass_Type='AM' 
        ' --and TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code='5260'
        'group by TSPL_BOOKING_DETAIL_Hist_Data.Item_Code,TSPL_BOOKING_DETAIL_Hist_Data.Unit_code,TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code
        'order by TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code "
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For Each dr As DataRow In dt.Rows
        '                    Dim obj As New clsDemandBookingSaleDetail
        '                    obj.Document_No = clsCommon.myCstr(dr("Against_DemandBooking_No"))
        '                    obj.TR_CODE = clsCommon.myCstr(dr("TR_Code"))
        '                    obj.Line_No = clsCommon.myCdbl(dr("Line_NO"))
        '                    obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
        '                    obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
        '                    obj.Qty = clsCommon.myCdbl(dr("Booking_Qty"))
        '                    obj.Cust_Code = clsCommon.myCdbl(dr("Cust_Code"))
        '                    Dim str1 As String = "insert into TSPL_DEMAND_BOOKING_DETAIL values('" + clsCommon.myCstr(obj.TR_CODE) + "','" + clsCommon.myCstr(obj.Document_No) + "','" + clsCommon.myCstr(obj.Line_No) + "','" + clsCommon.myCstr(obj.Cust_Code) + "','" + clsCommon.myCstr(obj.Item_Code) + "','" + clsCommon.myCstr(obj.Qty) + "','" + clsCommon.myCstr(obj.Unit_code) + "','',1,'','Morning',0,0,0,0,'N','N','','','N',NULL)"
        '                    clsDBFuncationality.ExecuteNonQuery(str1, trans)
        '                Next
        '            End If
        '            trans.Commit()
        '            clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
        '        Catch ex As Exception
        '            trans.Rollback()
        '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '        End Try
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "VARCHAR(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "Datetime NOT NULL")
        coll.Add("ShiftType", "VARCHAR(30) NOT NULL")
        coll.Add("Location_Code", "VARCHAR(30) NOT NULL")
        coll.Add("Created_By", "varchar(20)  NULL ")
        coll.Add("Created_Date", "Datetime  NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DEMAND_UPLOADER", coll, Nothing, True, False, "", "Document_No", "")

    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DEMAND_UPLOADER where Document_No='" + txtDocNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_DEMAND_UPLOADER.Document_No as DocumentCode,convert(varchar(12),TSPL_DEMAND_UPLOADER.Document_date,103) as DocumentDate,TSPL_DEMAND_UPLOADER.ShiftType from TSPL_DEMAND_UPLOADER "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentCode", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DEMAND_UPLOADER.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsDemandUploaderSave
            'Dim intRow As Integer
            obj = clsDemandUploaderSave.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                AddNew()
                'EnableDisable(False)
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = clsCommon.GetPrintDate(obj.Document_Date)
                If clsCommon.CompairString(obj.ShiftType, "Morning") = CompairStringResult.Equal Then
                    rbtnMorning.IsChecked = True
                ElseIf clsCommon.CompairString(obj.ShiftType, "Evening") = CompairStringResult.Equal Then
                    rbtnEvening.IsChecked = True

                End If
                txtLocation.Value = obj.Location_Code
                UcAttachment1.LoadData(obj.Document_No)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class
