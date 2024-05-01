'--Updation by--[Pankaj Kumar Chaudhary] against ticket no--[BM00000002123]
'------------BM00000003039 By Monika 02/07/2014
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common

Public Class frmVendorItemDetails
    Inherits FrmMainTranScreen
    Dim userCode As String = objCommonVar.CurrentUserCode
    Dim companyCode As String = objCommonVar.CurrentCompanyCode
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry As String
    Dim dt As DataTable
    Dim isOneItemOneVendor As Boolean = False
    Dim isnlevelcate As String = "N"
    Dim Is_stdpurrate_check As String = "0"
    Dim IsProceed As String = ""

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colitemno As String = "Item No"
    Const coldesc As String = "Description"
    Const coluom As String = "UOM"
    Const colisMRPMandatory As String = "colisMRPMandatory"
    Const colmrp As String = "MRP"
    Const colrate As String = "Item Rate"
    Const colAbatementRate As String = "AbatementRate"
    Const colvenitem As String = "Vendor Item No"
    Const colStartDate As String = "StartDate"
    Const colEndDate As String = "EndDate"
    Const colLocationCode As String = "Location Code"
    Const colLocationName As String = "Location Name"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsVendorItemDetail()
                obj.vendor_code = fndvendor.Value
                obj.vendor_desc = txtdesc.Text
                obj.comp_code = companyCode
                Dim qry As String = "select ISNULL(MAX(version), 0) from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + fndvendor.Value + "' "
                obj.version = clsDBFuncationality.getSingleValue(qry)
                If (obj.version <= 0 Or clsCommon.myLen(obj.version) <= 0) Then
                    obj.version = 1
                Else
                    obj.version = obj.version + 1
                End If

                Dim Arr As New List(Of clsVendorItemDetail)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsVendorItemDetail()
                    objTr.item_code = clsCommon.myCstr(grow.Cells(colitemno).Value)
                    objTr.item_desc = clsCommon.myCstr(grow.Cells(coldesc).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(coluom).Value)
                    objTr.MRP = clsCommon.myCdbl(grow.Cells(colmrp).Value)
                    objTr.item_rate = clsCommon.myCdbl(grow.Cells(colrate).Value)
                    objTr.AbatementRate = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                    objTr.vendor_item_no = clsCommon.myCstr(grow.Cells(colvenitem).Value)
                    '--------------------Added By Pankaj Kumar----on------14/03/2012-------------------
                    If clsCommon.myLen(grow.Cells(colStartDate).Value) <= 0 Then
                        objTr.Start_Date = Nothing
                    Else
                        objTr.Start_Date = clsCommon.myCDate(grow.Cells(colStartDate).Value, "dd-MMM-yyyy")
                    End If

                    If clsCommon.myLen(grow.Cells(colEndDate).Value) <= 0 Then
                        objTr.End_Date = Nothing
                    Else
                        objTr.End_Date = clsCommon.myCDate(grow.Cells(colEndDate).Value, "dd-MMM-yyyy")
                    End If

                    objTr.version = obj.version
                    '---------------------------------Code Ends Here-----------------------------------

                    '---------------By Monika 13/06/2014-------------------------
                    objTr.loccode = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    objTr.locname = clsCommon.myCstr(grow.Cells(colLocationName).Value)
                    '----------------End Here----------------------------------------
                    If (clsCommon.myLen(objTr.item_code) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next


                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Item", Me.Text)
                    Return
                End If

                'Dim objHist As New clsVendorItemDetailHistory()
                'objHist.SaveDataHistory(fndvendor.Value)

                If (obj.SaveData(fndvendor.Value, txtdesc.Text, companyCode, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    'LoadData(obj.vendor_code, NavigatorType.Current)

                    If clsCommon.myLen(IsProceed) > 0 Then
                        '-------------------do saving of notification record-------------------
                        If clsfrmNotificationScreen.DisplayNotification(Me.Module_Code, Me.Form_ID, clsCommon.GETSERVERDATE(), "SAVE", clsCommon.myCstr(fndvendor.Value), IsProceed) Then
                        End If
                        '-----------------------------------
                    End If
                    IsProceed = ""
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(fndvendor.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor No", Me.Text)
                fndvendor.Focus()
                Return False
            End If
            isOneItemOneVendor = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseOneItemOneVendor, clsFixedParameterCode.PurchaseOneItemOneVendor, Nothing)) = 1, True, False)

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colitemno).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myCBool(dgvitem.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(dgvitem.Rows(ii).Cells(colmrp).Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter MRP for " + strICode + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If Not clsCommon.myCBool(dgvitem.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(dgvitem.Rows(ii).Cells(colmrp).Value) > 0 Then
                        common.clsCommon.MyMessageBoxShow("MRP Not Accepted on non mrp item  for " + strICode + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If

                    Dim strprice As Integer = clsCommon.myCdbl(dgvitem.Rows(ii).Cells(colmrp).Value)
                    Dim strIName As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(coldesc).Value)
                    If clsCommon.myLen(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colStartDate).Value)) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Enter Start date For Item '" + strICode + "'")
                        Return False
                    End If
                    If isOneItemOneVendor Then
                        Dim qry As String = "select vendor_code from TSPL_VENDOR_ITEM_DETAIL where item_no='" + strICode + "' and vendor_code not in ('" + fndvendor.Value + "')"
                        Dim dtI As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtI IsNot Nothing AndAlso dtI.Rows.Count > 0 Then
                            common.clsCommon.MyMessageBoxShow("Item:" + strICode + " is reserved from vendor  '" + clsCommon.myCstr(dtI.Rows(0)("vendor_code")) + "'")
                            Return False
                        End If
                    End If
                    'richa 24/07/2014 Ticket No BM00000003314
                    Dim strILocation As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colLocationCode).Value)
                    '-------------------------
                    If clsCommon.myLen(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colitemno).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colLocationCode).Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Select Location Code/Name At Line No " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        Return False
                    End If


                    For jj As Integer = 0 To dgvitem.Rows.Count - 1
                        If (ii = jj) Then
                            Continue For
                        End If
                        'richa 24/07/2014 Ticket No BM00000003314
                        'If (clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) Then

                        If ((clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) And (clsCommon.CompairString(strILocation, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colLocationCode).Value)) = CompairStringResult.Equal)) Then
                            If ((clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Equal) Or (clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Greater) Or (clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Less)) Then
                                common.clsCommon.MyMessageBoxShow("MRP of two same Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + " should not be same or different for same location")
                                Return False
                            End If
                            'If (clsCommon.CompairString(strprice, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colmrp).Value)) = CompairStringResult.Equal) Then
                            '    common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                            '    Return False
                            'End If
                        End If
                    Next
                    '-------------------------------------------------
                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                End If
            Next
           

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData(ByVal vendorcode As String, ByVal Desc As String)
        Try
            btnsave.Enabled = True

            btndelete.Enabled = True
            isInsideLoadData = True

            'btnsave.Text = "Update"

            'funreset()
            LoadBlankGrid()

            Dim Arr As List(Of clsVendorItemDetail) = clsVendorItemDetail.GetData(vendorcode)
            'fndvendor.Value = vendorcode
            txtdesc.Text = Desc
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsVendorItemDetail In Arr
                    dgvitem.Rows.AddNew()
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colitemno).Value = objTr.item_code
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coldesc).Value = objTr.item_desc
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coluom).Value = objTr.UOM
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colmrp).Value = objTr.MRP
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colrate).Value = objTr.item_rate
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colAbatementRate).Value = objTr.AbatementRate
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colvenitem).Value = objTr.vendor_item_no
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colStartDate).Value = objTr.Start_Date
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colEndDate).Value = objTr.End_Date
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.item_code)

                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colLocationCode).Value = objTr.loccode
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colLocationName).Value = objTr.locname
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub



    'Public Sub funfill()
    '    Try

    '        dgvitem.AutoGenerateColumns = False

    '        Dim qry As String = "select item_no as [Item No],item_desc as [Description],uom as [UOM],MRP as [MRP],item_rate as [Item Rate],vendor_item_no as [Vendor Item No] from TspL_vendor_item_detail WHERE vendor_code = '" + fndvendor.Value + "'"
    '        Dim con As String = connectSql.SqlCon
    '        Dim da As New SqlDataAdapter(qry, con)
    '        Dim dt As New DataTable()

    '        da.Fill(dt)

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            'dgvitem.Rows.Add()
    '            dgvitem.Rows(i).Cells(0).Value = dt.Rows(i)(0).ToString
    '            dgvitem.Rows(i).Cells(1).Value = dt.Rows(i)(1).ToString
    '            dgvitem.Rows(i).Cells(2).Value = dt.Rows(i)(2).ToString
    '            dgvitem.Rows(i).Cells(3).Value = dt.Rows(i)(3).ToString
    '            dgvitem.Rows(i).Cells(4).Value = dt.Rows(i)(4).ToString
    '            dgvitem.Rows(i).Cells(5).Value = dt.Rows(i)(5).ToString

    '        Next



    '        dgvitem.DataSource = dt
    '        dgvitem.AllowAddNewRow = True

    '        btndelete.Enabled = True
    '        btnsave.Text = "Update"
    '        'dgvitem.Columns(o).ReadOnly = True
    '        'dgvitem.Columns(2).ReadOnly = True
    '        'dgvitem.Columns(4).ReadOnly = True

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Public Sub funreset()
        fndvendor.Value = ""
        txtdesc.Text = ""
        isInsideLoadData = False
        LoadBlankGrid()
        btnsave.Text = "Save"
        IsProceed = ""
        'btndelete.Enabled = False
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        delete()
    End Sub
    Public Sub delete()
        Try
            Dim str As String = "select count(*) from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + fndvendor.Value + "'"

            Dim no1 As Integer = CInt(connectSql.RunScalar(str))
            If no1 = 0 Then
                clsCommon.MyMessageBoxShow("Data Not Found.", "Vender", MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                If fndvendor.Value = "" Then
                    myMessages.blankValue(Me, "Vendor No", Me.Text)
                ElseIf myMessages.deleteConfirm() Then
                    fundelete()
                    funreset()
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
    Private Sub fndvendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendor._MYValidating
        Dim qry As String = "select vendor_code as Code,vendor_name as Description,ISNULL(alies_name,'') As [Alies Name] from tspl_vendor_master"
        fndvendor.Value = clsCommon.ShowSelectForm("VendorMasFND", qry, "Code", "", fndvendor.Value, "Code", isButtonClicked)
        txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + fndvendor.Value + "'"))
        LoadData(fndvendor.Value, txtdesc.Text)
    End Sub
    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    Dim XR As Integer = dgvitem.CurrentRow.Index

                    If e.Column Is dgvitem.Columns(colitemno) Then
                        OpenICodeList(False)
                    ElseIf (e.Column Is dgvitem.Columns(coldesc)) Then
                        OpenICodeDescList(False)
                    ElseIf e.Column Is dgvitem.Columns(coluom) Then
                        openuom(False)
                    ElseIf (e.Column Is dgvitem.Columns(colLocationCode)) Then
                        OpenLocation(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Sub OpenLocation(ByVal isButtonClick As Boolean)
        qry = "select location_desc from tspl_location_master where location_code='" + clsCommon.myCstr(dgvitem.CurrentRow.Cells(colLocationCode).Value) + "'"
        Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If clsCommon.myLen(value) <= 0 Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],(TSPL_LOCATION_MASTER.Add1+' '+TSPL_LOCATION_MASTER.Add2+' '+TSPL_LOCATION_MASTER.Add3+' '+TSPL_LOCATION_MASTER.Add4) as Address,TSPL_LOCATION_MASTER.City_Code as [City],TSPL_LOCATION_MASTER.State,TSPL_LOCATION_MASTER.Country,TSPL_LOCATION_MASTER.Location_Type as [Location Type] from TSPL_LOCATION_MASTER"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("LOCFND", qry)

            If dr IsNot Nothing Then
                dgvitem.CurrentRow.Cells(colLocationCode).Value = clsCommon.myCstr(dr("Code"))
                dgvitem.CurrentRow.Cells(colLocationName).Value = clsCommon.myCstr(dr("Location Name"))
            Else
                dgvitem.CurrentRow.Cells(colLocationCode).Value = ""
                dgvitem.CurrentRow.Cells(colLocationName).Value = ""
            End If
        Else
            dgvitem.CurrentRow.Cells(colLocationCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where location_code='" + clsCommon.myCstr(dgvitem.CurrentRow.Cells(colLocationCode).Value) + "'"))
            dgvitem.CurrentRow.Cells(colLocationName).Value = value
        End If
    End Sub
    Sub openuom(ByVal isButtonClick As Boolean)
        dgvitem.CurrentRow.Cells(coluom).Value = clsItemMaster.FinderForuom(clsCommon.myCstr(dgvitem.CurrentRow.Cells(coluom).Value), clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), isButtonClick)
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""
        '------------Check Screen Notification-----Ticket No-BM00000002123---
        qry = "Select Notification, Validation from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Screen_Code='VEN-ITEM-DET' AND Level='Screen' AND Scheduling='Y'"
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            whrcls = "TSPL_ITEM_MASTER.Item_Code NOT IN (Select Distinct item_no from TSPL_VENDOR_ITEM_DETAIL WHERE End_Date>='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + "' OR ISNULL(End_Date,'')='')"
        End If
        '-------------------------------------------------------

        If isnlevelcate = "1" Then
            qry = ""
            If dt.Rows.Count > 0 Then
                whrcls = "a.Item_Code NOT IN (Select Distinct item_no from TSPL_VENDOR_ITEM_DETAIL WHERE End_Date>='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + "' OR ISNULL(End_Date,'')='')"
            End If
            ''richa 22/07/2014 Ticket No BM00000003295 add itf code,Weight uom,weight value,unitcode in item code finder
            qry = "select a.Item_Code as Code,a.Item_Desc as Name,a.ITF_CODE as [ITF Code],a.Weight_Value as [Weight Value] ,a.Weight_UOM as [Weight UOM] ,a.Unit_Code as UOM,(select distinct((select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=a.Item_Code for XML path('')))) as [Item N-Level Category] from  TSPL_ITEM_MASTER a"
        Else
            qry = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],TSPL_ITEM_MASTER.ITF_CODE as [ITF Code],TSPL_ITEM_MASTER.Weight_Value as [Weight Value] ,TSPL_ITEM_MASTER.Weight_UOM as [Weight UOM] ,TSPL_ITEM_MASTER.Unit_Code as UOM  from  TSPL_ITEM_MASTER left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category   left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category"
        End If
        dgvitem.CurrentRow.Cells(colitemno).Value = clsCommon.ShowSelectForm("itmfinder@VID", qry, "Code", whrcls, dgvitem.CurrentRow.Cells(colitemno).Value, "Code", False)
        qry = "select Item_Code,Item_Desc,Unit_Code,Is_Serial_Item,Is_Pick_Auto_SrNo,Is_MRP from TSPL_ITEM_MASTER where Item_Code='" + dgvitem.CurrentRow.Cells(colitemno).Value + "' "
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dgvitem.CurrentRow.Cells(coldesc).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            dgvitem.CurrentRow.Cells(coluom).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            dgvitem.CurrentRow.Cells(colisMRPMandatory).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
        Else
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
            dgvitem.CurrentRow.Cells(coluom).Value = ""
            dgvitem.CurrentRow.Cells(coluom).Value = False
        End If
        dgvitem.CurrentRow.Cells(colAbatementRate).Value = GeDefaultAbatementRate(clsCommon.GETSERVERDATE)
    End Sub
    Sub OpenICodeDescList(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""

        '------------Check Screen Notification-----Ticket No-BM00000002123---
        qry = "Select Notification, Validation from TSPL_SCREEN_NOTIFICATION_SETTING WHERE Screen_Code='VEN-ITEM-DET' AND Level='Screen' AND Scheduling='Y'"
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            whrcls = "TSPL_ITEM_MASTER.item_desc NOT IN (Select Distinct TSPL_ITEM_MASTER.item_desc from TSPL_VENDOR_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_VENDOR_ITEM_DETAIL.item_no WHERE TSPL_VENDOR_ITEM_DETAIL.End_Date>='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + "' OR ISNULL(TSPL_VENDOR_ITEM_DETAIL.End_Date,'')='')"
        End If
        '-------------------------------------------------------
        qry = "select Item_Code as Code,replace(Item_Desc,''''','`') as Name,unit_code as [Unit Code],is_mrp as [MRP] ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category]  from  TSPL_ITEM_MASTER left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category   left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category"
        If isnlevelcate = "1" Then
            If dt.Rows.Count > 0 Then
                whrcls = "a.item_desc NOT IN (Select Distinct TSPL_ITEM_MASTER.item_desc from TSPL_VENDOR_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_VENDOR_ITEM_DETAIL.item_no WHERE TSPL_VENDOR_ITEM_DETAIL.End_Date>='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + "' OR ISNULL(TSPL_VENDOR_ITEM_DETAIL.End_Date,'')='')"
            End If
            qry = "select a.Item_Code as Code,replace(a.Item_Desc,''''','`') as Name,a.unit_code as [Unit Code],a.is_mrp as [MRP],(select distinct((select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=a.Item_Code for XML path('')))) as [Item N-Level Category] from  TSPL_ITEM_MASTER a"
        End If
        Dim srdesc As String = ""
        dgvitem.CurrentRow.Cells(coldesc).Value = clsCommon.myCstr(dgvitem.CurrentRow.Cells(coldesc).Value).Replace("'", "`")
        'srdesc = clsCommon.ShowSelectForm("itmfinder1@VID", qry, "Name", whrcls, srdesc, "Name", False)

        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("FNDITEM", qry, "Name", dgvitem.CurrentRow.Cells(coldesc).Value)
        If dr IsNot Nothing Then
            dgvitem.CurrentRow.Cells(colitemno).Value = clsCommon.myCstr(dr("code"))
            dgvitem.CurrentRow.Cells(coldesc).Value = clsCommon.myCstr(dr("name"))
            dgvitem.CurrentRow.Cells(coluom).Value = clsCommon.myCstr(dr("Unit Code"))
            dgvitem.CurrentRow.Cells(colisMRPMandatory).Value = IIf(clsCommon.myCdbl(dr("MRP")) = 1, True, False)
        End If
        'dgvitem.CurrentRow.Cells(colitemno).Value = srdesc
        'qry = "select Item_Code,Item_Desc,Unit_Code,Is_Serial_Item,Is_Pick_Auto_SrNo,Is_MRP from TSPL_ITEM_MASTER where item_code='" + dgvitem.CurrentRow.Cells(colitemno).Value + "' "
        'dt = clsDBFuncationality.GetDataTable(qry)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    dgvitem.CurrentRow.Cells(coldesc).Value = clsCommon.myCstr(dt.Rows(0)("item_desc"))
        '    dgvitem.CurrentRow.Cells(coluom).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
        '    dgvitem.CurrentRow.Cells(colisMRPMandatory).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1, True, False)
        'Else
        '    dgvitem.CurrentRow.Cells(colitemno).Value = ""
        '    dgvitem.CurrentRow.Cells(coluom).Value = ""
        '    dgvitem.CurrentRow.Cells(coluom).Value = False
        'End If
        dgvitem.CurrentRow.Cells(colAbatementRate).Value = GeDefaultAbatementRate(clsCommon.GETSERVERDATE)
    End Sub



    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()
        dgvitem.EnableFiltering = False

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.HeaderText = "Item No"
        item_code.Name = colitemno
        item_code.Width = 90
        item_code.ReadOnly = False
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        item_code.HeaderImage = Global.ERP.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 200
        item_desc.ReadOnly = False
        item_desc.TextImageRelation = TextImageRelation.TextBeforeImage
        item_desc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim uom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        uom.FormatString = ""
        uom.HeaderText = "UOM"
        uom.Name = coluom
        uom.Width = 70
        uom.ReadOnly = False
        uom.TextImageRelation = TextImageRelation.TextBeforeImage
        uom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        uom.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(uom)

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        Dim MRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        MRP.FormatString = ""
        MRP.HeaderText = "MRP"
        MRP.Name = colmrp
        MRP.Width = 70
        MRP.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(MRP)

        Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemrate.FormatString = ""
        itemrate.HeaderText = "Item Rate"
        itemrate.Name = colrate
        itemrate.Width = 70
        itemrate.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(itemrate)

        '' new added field abatement rate [Panch Raj]
        Dim AbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Abatement Rate"
        AbatementRate.Name = colAbatementRate
        AbatementRate.Width = 70
        AbatementRate.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(AbatementRate)

        Dim venitem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        venitem.FormatString = ""
        venitem.HeaderText = "Vendor Item No"
        venitem.Name = colvenitem
        venitem.Width = 150
        venitem.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(venitem)

        Dim StartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        StartDate.Format = DateTimePickerFormat.Custom
        StartDate.CustomFormat = "dd-MM-yyyy"
        StartDate.HeaderText = "Start Date"
        StartDate.FormatString = "{0:d}"
        StartDate.Name = colStartDate
        StartDate.WrapText = True
        StartDate.ReadOnly = False
        StartDate.Width = 80
        dgvitem.MasterTemplate.Columns.Add(StartDate)

        Dim EndDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        EndDate.Format = DateTimePickerFormat.Custom
        EndDate.CustomFormat = "dd-MM-yyyy"
        EndDate.HeaderText = "End Date"
        EndDate.FormatString = "{0:d}"
        EndDate.Name = colEndDate
        EndDate.WrapText = True
        EndDate.ReadOnly = False
        EndDate.Width = 80
        dgvitem.MasterTemplate.Columns.Add(EndDate)

        Dim repoloc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoloc.FormatString = ""
        repoloc.Name = colLocationCode
        repoloc.HeaderText = "Location Code"
        repoloc.Width = 80
        repoloc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoloc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoloc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(repoloc)

        Dim repolocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname.FormatString = ""
        repolocname.Name = colLocationName
        repolocname.HeaderText = "Location Name"
        repolocname.Width = 120
        repolocname.ReadOnly = True
        repolocname.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(repolocname)

        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = True
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.VendorItemDetails)
        If Not (MyBase.isReadFlag) Then

            '--------------richa 15/07/2014 Ticket No BM00000003124---------
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 11/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            Export.Enabled = True
            Import.Enabled = True
        Else
            Export.Enabled = False
            Import.Enabled = False
        End If
        '--------------------------------------------------
        ' btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub CheckNlevelCate()
        Dim qry As String = "select IsNLevelCatForItem from TSPL_INV_PARAMETERS"
        isnlevelcate = clsDBFuncationality.getSingleValue(qry)
    End Sub

    Private Sub frmVendorItemDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVersion.Visible = False
        lblVersionDesc.Visible = False
        LoadBlankGrid()
        CheckNlevelCate()
        btndelete.Enabled = True
        btnsave.Enabled = True
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        checkfixedparam()
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D to Delete")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
    End Sub

    Sub checkfixedparam() '-----------by Monika 02/07/2014
        Dim qry As String = "select description from tspl_fixed_parameter where code='" + clsFixedParameterCode.STDPURRATE + "' and type='" + clsFixedParameterType.STDPURRATE + "'"
        Is_stdpurrate_check = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = clsUserMgtCode.VendorItemDetails
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                btnsave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                btndelete.Enabled = False
            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
        End Try
    End Function

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Public Sub fundelete()
        Try
            Dim strqry As String = "delete from tspl_vendor_item_detail where vendor_code='" + fndvendor.Value + "'"
            connectSql.RunSql(strqry)
            myMessages.delete()
            'btnsave.Text = "&Save"
            'btndelete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub


    Sub BlankAllControls()
        fndvendor.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Function GeDefaultAbatementRate(ByVal startDate As Date) As Decimal
        Dim Qry As String = "select Abatement_Percent  from TSPL_ABATEMENT_MASTER where  YEAR(start_date)=YEAR('" & clsCommon.GetPrintDate(startDate, "dd/MMM/yyyy") & "') order by Abatement_Code desc "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Return clsCommon.myCdbl(dt.Rows(0).Item("Abatement_Percent"))
        Else
            Return 0
        End If
    End Function
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub Export_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select vendor_code as [Vendor Code],vendor_desc as [Vendor Description] ,item_no as [Item No],item_desc as [Item Description],uom as [UOM],MRP as [MRP],item_rate as [Rate],vendor_item_no as [Vendor Item No], REPLACE( Convert(varchar(11) ,Start_Date,102),'.','-') as [Start Date], REPLACE( Convert(varchar(11) ,End_Date,102),'.','-') as [End Date],AbatementRate as [Abatement Rate],Location_Code as [Location Code],[Location_Name] as [Location Name] from TSPL_VENDOR_ITEM_DETAIL  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub Import_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        isOneItemOneVendor = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseOneItemOneVendor, clsFixedParameterCode.PurchaseOneItemOneVendor, Nothing)) = 1, True, False)

        If transportSql.importExcel(gv, "Vendor Code", "Vendor Description", "Item No", "Item Description", "UOM", "MRP", "Rate", "Vendor Item No", "Start Date", "End Date", "Abatement Rate", "Location Code", "Location Name") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                'clsCommon.ProgressBarShow()

             

                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    Dim VndrCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim vencode As String
                    If VndrCode.Length > 0 Then
                        vencode = "select vendor_code from tspl_vendor_master where vendor_code='" + VndrCode + "'"
                        vencode = clsDBFuncationality.getSingleValue(vencode, trans)
                        If clsCommon.myLen(vencode) <= 0 Then
                            Throw New Exception("The Vendor '" + VndrCode + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("Vendor Code can not be blank at line " + LineNo + " .")
                    End If

                    Dim vendesc As String = clsDBFuncationality.getSingleValue("Select Vendor_Name FROM TSPL_VENDOR_MASTER WHERE Vendor_Code='" + vencode + "'", trans)

                    Dim ItmNo As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim itemno As String
                    If clsCommon.myLen(ItmNo) > 0 Then
                        itemno = "select item_code from TSPL_ITEM_MASTER where item_code='" + ItmNo + "'"
                        itemno = clsDBFuncationality.getSingleValue(itemno, trans)
                        If clsCommon.myLen(itemno) <= 0 Then
                            Throw New Exception("The Item '" + ItmNo + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("Item Code can not be left blank at line " + LineNo + " .")
                    End If

                    Dim qryDesc As String = "select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + itemno + "' "
                    Dim itemdesc As String = clsDBFuncationality.getSingleValue(qryDesc, trans)
                    itemdesc = itemdesc.Replace("'", "''")

                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim uom As String
                    If clsCommon.myLen(UnitCode) > 0 Then
                        uom = "Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + itemno + "' AND UOM_Code='" + UnitCode + "'"
                        uom = clsDBFuncationality.getSingleValue(uom, trans)
                        If clsCommon.myLen(uom) <= 0 Then
                            Throw New Exception("The UOM '" + UnitCode + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("UOM can not be left blank at line " + LineNo + ".")
                    End If

                    Dim mrp As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If mrp.Length < 18 And IsNumeric(mrp) Then
                    Else
                        Throw New Exception("Check the value of 'Item MRP'.")
                    End If
                    Dim rate As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If rate.Length < 18 And IsNumeric(rate) Then
                    Else
                        Throw New Exception("Check the value of 'Item Rate'.")
                    End If

                    Dim AbatementRate As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Abatement Rate").Value))
                    'If AbatementRate.Length < 4 And IsNumeric(AbatementRate) Then
                    'Else
                    '    Throw New Exception("Check the value of 'Abatement Rate'.")
                    'End If


                    Dim venitemno As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If venitemno.Length > 50 Then
                        Throw New Exception("Check the length of 'Vendor Item No'.")
                    End If

                    Dim StrstartDate As String = Nothing
                    If (grow.Cells(8).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(8).Value) > 0 And clsCommon.myLen(clsCommon.GetPrintDate(grow.Cells(8).Value, "dd/MMM/yyyy")) < 12) Then
                        StrstartDate = clsCommon.GetPrintDate((grow.Cells(8).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-MM-dd)")
                    End If

                    Dim StrEndDate As String = Nothing
                    If (grow.Cells(9).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(9).Value) > 0) Then
                        StrEndDate = clsCommon.GetPrintDate((grow.Cells(9).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-mm-dd)")
                    End If

                    Dim isMRPMAndatory As Boolean = clsItemMaster.IsMRPItem(itemno, trans)
                    If isMRPMAndatory AndAlso clsCommon.myCdbl(mrp) <= 0 Then
                        Throw New Exception("Please enter MRP for " + itemno)

                    End If
                    If Not isMRPMAndatory AndAlso clsCommon.myCdbl(mrp) > 0 Then
                        Throw New Exception("MRP Not Accepted on non mrp item for " + itemno)
                    End If

                    '------------------------
                    Dim loccode As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    Dim locname As String = clsCommon.myCstr(grow.Cells("Location Name").Value)

                    If clsCommon.myLen(itemno) > 0 AndAlso clsCommon.myLen(loccode) <= 0 AndAlso clsCommon.myLen(locname) <= 0 Then
                        Throw New Exception("Please Fill Location Code And Name At Line No. " + clsCommon.myCstr(LineNo) + "")
                    End If

                    qry = "select count(*) from tspl_location_master where location_code='" + loccode + "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                    If check <= 0 Then
                        qry = "select count(*) from tspl_location_master where location_name='" + locname + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Location Code And Name Does Not Exist,Make Its Master First,See At Line No. " + clsCommon.myCstr(LineNo) + "")
                        End If
                    End If
                    '-------------------------------------------

                    If isOneItemOneVendor Then
                        Dim qry As String = "select vendor_code from TSPL_VENDOR_ITEM_DETAIL where item_no='" + itemno + "' and location_code='" + loccode + "' and vendor_code not in ('" + VndrCode + "')"
                        Dim dtI As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtI IsNot Nothing AndAlso dtI.Rows.Count > 0 Then
                            Throw New Exception("Item:" + itemno + " is reserved from vendor  '" + clsCommon.myCstr(dtI.Rows(0)("vendor_code")) + "' at location " + locname + "")
                        End If
                    End If

                    'richa 24/07/2014 Ticket No BM00000003314
                    'Dim sql1 As String = "select count(*) from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + vencode + "' and item_no='" + itemno + "' and MRP= '" + Convert.ToString(mrp) + "'"  'and location_code='" + loccode + "'
                    Dim sql1 As String = "select count(*) from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + vencode + "' and item_no='" + itemno + "' and location_code='" + loccode + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_VENDOR_ITEM_DETAIL( vendor_code ,vendor_desc  ,item_no ,item_desc ,uom ,MRP ,item_rate ,vendor_item_no ,comp_code, Start_Date, End_Date ,AbatementRate,location_code,location_name) values('" + Convert.ToString(vencode) + "','" + Convert.ToString(vendesc) + "','" + Convert.ToString(itemno) + "','" + Convert.ToString(itemdesc) + "','" + Convert.ToString(uom) + "','" + Convert.ToString(mrp) + "','" + Convert.ToString(rate) + "','" + Convert.ToString(venitemno) + "','" + Convert.ToString(companyCode) + "'," + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + " ," + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + " ,'" & AbatementRate & "','" + loccode + "','" + locname + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        'richa 24/07/2014 Ticket No BM00000003314
                        'Dim qry As String = "update TSPL_VENDOR_ITEM_DETAIL set vendor_desc= '" + Convert.ToString(vendesc) + "'  ,item_desc= '" + Convert.ToString(itemdesc) + "',uom= '" + Convert.ToString(uom) + "',MRP= '" + Convert.ToString(mrp) + "',item_rate='" + Convert.ToString(rate) + "' ,vendor_item_no='" + Convert.ToString(venitemno) + "' ,comp_code='" + Convert.ToString(companyCode) + "', Start_Date=" + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + ", End_Date=" + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + ",AbatementRate='" & AbatementRate & "',location_code='" + loccode + "' , location_name='" + locname + "' where vendor_code= '" + Convert.ToString(vencode) + "' and item_no='" + Convert.ToString(itemno) + "' and MRP= '" + Convert.ToString(mrp) + "'"
                        Dim qry As String = "update TSPL_VENDOR_ITEM_DETAIL set vendor_desc= '" + Convert.ToString(vendesc) + "'  ,item_desc= '" + Convert.ToString(itemdesc) + "',uom= '" + Convert.ToString(uom) + "',MRP= '" + Convert.ToString(mrp) + "',item_rate='" + Convert.ToString(rate) + "' ,vendor_item_no='" + Convert.ToString(venitemno) + "' ,comp_code='" + Convert.ToString(companyCode) + "', Start_Date=" + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + ", End_Date=" + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + ",AbatementRate='" & AbatementRate & "',location_code='" + loccode + "' , location_name='" + locname + "' where vendor_code= '" + Convert.ToString(vencode) + "' and item_no='" + Convert.ToString(itemno) + "' and location_code= '" + Convert.ToString(loccode) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                'clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                'clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    ''Private Sub frmVendorItemDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    ''    If e.KeyCode = Keys.F2 AndAlso dgvitem.CurrentCell IsNot Nothing Then
    ''        isCellValueChangedOpen = True
    ''        If dgvitem.CurrentColumn Is dgvitem.Columns(colitemno) Then
    ''            'dgvitem.CurrentColumn = dgvitem.Columns(colTo)
    ''            OpenFromList(True)
    ''            dgvitem.CurrentColumn = dgvitem.Columns(coldesc)

    ''        End If
    ''    End If
    ''End Sub

    ''Sub OpenFromList(ByVal isButtonClick As Boolean)
    ''    Dim qry As String = "SELECT item_no  as Code,Description from tspl_vendor "
    ''    dgvitem.CurrentRow.Cells(colitemno).Value = clsCommon.ShowSelectForm("Items", qry, "Code", "item_no='" + clsCommon.myCstr(dgvitem.CurrentRow.Cells("colitemno").Value) + "'", clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), "Code", isButtonClick)
    ''End Sub



    Private Sub frmVendorItemDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            Print()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()

        If clsCommon.myLen(fndvendor.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Vendor", Me.Text)
            fndvendor.Focus()
            Return
        End If
        Try
            Dim Qry As String = "Select '" + clsCommon.GETSERVERDATE() + "' as PrintDate,  (vendor_code+' - '+ convert(varchar, vendor_desc, 103)) as Vendor, item_no, item_desc, uom, MRP, item_rate, vendor_item_no, Convert(date,Start_Date, 103) as Start_Date , COnvert(date,End_Date, 103) as End_Date, Comp_Name, Logo_Img, Logo_Img2,CONVERT(date, History_Date, 103) as History_Date,TSPL_VENDOR_ITEM_DETAIL_HIST.location_code,TSPL_VENDOR_ITEM_DETAIL_HIST.location_name    from TSPL_VENDOR_ITEM_DETAIL_HIST Left outer join TSPL_COMPANY_MASTER on TSPL_VENDOR_ITEM_DETAIL_HIST.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  Where vendor_code='" + fndvendor.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Exit Sub
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptVendorItemHistory", "Vendor Item History Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles dgvitem.CellFormatting
        Try
            If e.Column Is dgvitem.Columns(colmrp) Then
                If clsCommon.myCBool(dgvitem.CurrentRow.Cells(colisMRPMandatory).Value) Then
                    dgvitem.CurrentRow.Cells(colmrp).ReadOnly = False
                Else
                    dgvitem.CurrentRow.Cells(colmrp).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub dgvitem_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvitem.CellValidating
        Try
            If (e.Column Is dgvitem.Columns(colrate) Or e.Column Is dgvitem.Columns(colAbatementRate) Or e.Column Is dgvitem.Columns(colitemno)) Then
                Dim rate As Decimal = clsCommon.myCdbl(dgvitem.CurrentRow.Cells(colrate).Value)
                Dim itemcode As String = clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value)

                If rate > 0 Then
                    Dim qry As String = "select purchase_price from tspl_item_master where item_code='" + itemcode + "'"
                    Dim purrate As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                    If purrate <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Set Standard Purchase Price In Item Master", Me.Text)
                        Return
                    End If
                    If clsCommon.CompairString(Is_stdpurrate_check, "1") = CompairStringResult.Equal AndAlso rate > purrate Then
                        If Not clsCommon.MyMessageBoxShow("Item Rate Is Higher Than Standard Purchase Price,Would You Like To Proceed With Changed Rate?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            dgvitem.CurrentRow.Cells(colrate).Value = "0"
                            Return
                        End If
                        IsProceed = IsProceed + ", " + itemcode + "[" + clsCommon.myCstr(dgvitem.CurrentRow.Cells(coldesc).Value) + "]"

                        If IsProceed.Substring(0, 1) = "," Then
                            IsProceed = IsProceed.Substring(1, IsProceed.Length - 1)
                        End If
                    ElseIf clsCommon.CompairString(Is_stdpurrate_check, "1") <> CompairStringResult.Equal AndAlso rate > purrate Then '----if validate setting is off and rate is more than it does not allow rate change
                        clsCommon.MyMessageBoxShow("Please Change Item Rate,Is Higher Than Standard Purchase Price Set In 'Item Master'" + Environment.NewLine + "(i.e. Standard Price : " + clsCommon.myCstr(purrate) + ")", Me.Text)
                        dgvitem.CurrentRow.Cells(colrate).Value = "0"
                        Return
                    End If
                    
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
