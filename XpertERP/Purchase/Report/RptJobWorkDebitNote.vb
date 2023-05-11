Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class RptJobWorkDebitNote
    Inherits FrmMainTranScreen
    Dim arrLoc As String = Nothing
    Const colDocNo As String = "colDocNo"
    Const colDocDate As String = "colDocDate"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUOM As String = "colUOM"
    Const colselect As String = "COLSELECT"
    Const colQty As String = "colQty"
    Const colFAT As String = "colFAT"
    Const colSNF As String = "colSNF"
    Const colrate As String = "colRate"
    Const colUnitprice As String = "price"
    Private isNewEntry As Boolean = True
    Public isInsideLoadData As Boolean = False
    Dim dt1 As DataTable
    Dim dt2 As DataTable


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)

    End Sub

    Public Sub SetUserMgmtNew()
        Try
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmJobWorkDebitNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        LOCATIONRIGTHS()
        'LoadBlankGrid()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        txtFromDate.Text = clsCommon.GETSERVERDATE()
        txtToDate.Text = clsCommon.GETSERVERDATE()
        fndcustNo.Value = ""
        fndLocation.Value = ""
        txtcustdesc.Text = ""
        txtlocation.Text = ""
        LoadBlankGrid()
        btnGo.Enabled = True
    End Sub
    Private Sub fndcustNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustNo._MYValidating
        fndcustNo.Focus()
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location")
            fndLocation.Focus()
            Exit Sub
        End If

        Dim qry As String = " SELECT Vendor_Code as Code,Vendor_Name AS Name FROM TSPL_VENDOR_MASTER "
        Dim WhrCls As String = "1=2"
        fndcustNo.Value = clsCommon.ShowSelectForm("CustmrMstrI1", qry, "Code", WhrCls, fndcustNo.Value, "Code", isButtonClicked)

        qry = "   SELECT Vendor_Code as Code,Vendor_Name AS Name FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + fndcustNo.Value + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtcustdesc.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))

        Else
            txtcustdesc.Text = ""

        End If

    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstrFND1", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'")
            Else
                'ShowData()
                gv1.BestFitColumns()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    'Sub ShowData()
    '    Try
    '        LoadBlankGrid()
    '        Dim arr As New List(Of clsJobWorkDebitNote)
    '        Dim location As String = clsCommon.myCstr(fndLocation.Value)
    '        Dim Customer As String = clsCommon.myCstr(fndcustNo.Value)
    '        Dim fromdate As String = clsCommon.myCstr(txtFromDate.Text)
    '        Dim todate As String = clsCommon.myCstr(txtToDate.Text)
    '        arr = clsJobWorkDebitNote.GetData(location, Customer, fromdate, todate, Nothing)
    '        For Each obj As clsJobWorkDebitNote In arr
    '            gv1.Rows.AddNew()
    '            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(obj.DocumentNo)
    '            gv1.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(obj.DocumentDate)
    '            gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(obj.ItemCode)
    '            gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(obj.ItemName)
    '            gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(obj.Qty)
    '            gv1.CurrentRow.Cells(colFAT).Value = clsCommon.myCdbl(obj.FatPer)
    '            gv1.CurrentRow.Cells(colSNF).Value = clsCommon.myCdbl(obj.SnfPer)
    '            gv1.CurrentRow.Cells(colUnitprice).Value = clsCommon.myCdbl(obj.NetAmt)
    '            gv1.CurrentRow.Cells(colrate).Value = clsCommon.myCdbl(obj.rate)
    '        Next
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheck.FormatString = ""
        repoCheck.HeaderText = " "
        repoCheck.Name = colselect
        repoCheck.Width = 60
        repoCheck.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCheck)

        Dim repoDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocNo.FormatString = ""
        repoDocNo.HeaderText = "Document No"
        repoDocNo.Name = colDocNo
        repoDocNo.Width = 100
        repoDocNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocNo)

        Dim repoDocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocDate.FormatString = ""
        repoDocDate.HeaderText = "Document Date"
        repoDocDate.Name = colDocDate

        repoDocDate.Width = 100
        repoDocDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocDate)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 100
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repofat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofat.FormatString = ""
        repofat.HeaderText = "Fat %"
        repofat.Name = colFAT
        repofat.IsVisible = True
        repofat.Minimum = 0
        repofat.DecimalPlaces = 2
        repofat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repofat.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofat)

        Dim repoSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSnf.FormatString = ""
        repoSnf.HeaderText = "SNF %"
        repoSnf.Name = colSNF
        repoSnf.IsVisible = True
        repoSnf.Minimum = 0
        repoSnf.DecimalPlaces = 2
        repoSnf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSnf.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSnf)

        Dim reporate As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.HeaderText = "Job Work Rate"
        reporate.Name = colrate
        reporate.IsVisible = True
        reporate.Minimum = 0
        reporate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        reporate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reporate)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colUnitprice
        repoAmount.IsVisible = True
        repoAmount.Minimum = 0
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmount)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        Reset()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
        End If
    End Sub
    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Try
    '        LoadBlankGrid()
    '        isInsideLoadData = True
    '        btnGo.Enabled = False
    '        Dim obj As New clsJobWorkDebitNoteHead()
    '        obj = clsJobWorkDebitNote.LoadData(strCode, NavTyep, arrLoc)
    '        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '            For Each objTr As clsJobWorkDebitNote In obj.Arr
    '                gv1.Rows.AddNew()
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.DocumentNo
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = objTr.DocumentDate
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ItemCode
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.ItemName
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = objTr.FatPer
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colselect).Value = objTr.IsSelect
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = objTr.SnfPer
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colrate).Value = objTr.rate
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitprice).Value = objTr.NetAmt
    '            Next
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        isInsideLoadData = False
    '    End Try
    'End Sub
    Public Sub LoadDataQuery() 'As ArrayList
        Dim qry1 As String = Nothing
        Dim qry2 As String = Nothing
        dt1 = Nothing
        dt2 = Nothing
        qry1 = "with WholeData as (Select  DISTINCT xx.Group_Date,xx.[Second_Date], XX.Comp_Name,XX.Comp_Phone,XX.Comp_Address,XX.Pincode,xx.Tin_No,xx.Pan_No," & _
           " Line_No, xx.[To MCC or Plant Code], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.Vendor_Address,xx.[Challan No],xx.[Challan Date], coalesce(xx.[SRN No],'') AS [SRN No],  xx.[SRN Status] as [SRN Status],  coalesce(xx.[SRN Date],'' ) as [SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.Gate_Entry_Type,xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],  xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty],xx.[Gross Weight],xx.[Tare Weight],xx.[Net Weight],  xx.MIKL_TYPE_CODE as [Milk Type],xx.Milk_Grade_code as [Grade Code],xx.GRADE_TYPE as [Grade Type], xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No], coalesce(xx.[Secondary QC No],'') as [Secondary QC No], XX.[Unloading Date Time],  XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],  xx.[FAT %],xx.[SNF %], xx.CLR,xx.[Basic Rate], xx.incentive ,xx.[Special Deduction] , xx.[Net Rate],  coalesce(xx.[FAT Rate],0) as [FAT Rate], coalesce(xx.[SNF Rate],0) as [SNF Rate],  case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total Amount],xx.[FAT Weightage & SNF Weightage],  xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] ," & _
            " case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return], " & _
           " Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, " & _
            "  CASE WHEN xx.MIKL_TYPE_CODE='MIX' THEN Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100))END  As FATKG_in_Mixed, " & _
            "  CASE WHEN xx.MIKL_TYPE_CODE='SKMILK' THEN Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100))END  As FATKG_in_SkimMilk," & _
             "  CASE WHEN xx.MIKL_TYPE_CODE not in('MIX','SKMILK') THEN Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100))END  As FATKG_in_Others," & _
          "   Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG " & _
           "  From ( Select  row_number() over(order by  Secondry_Tbl.date) as r ,case when DENSE_RANK() over(order by Secondry_Tbl.date)%8<>0 then DENSE_RANK() over(order by Secondry_Tbl.date)/8 else (DENSE_RANK() over(order by Secondry_Tbl.date)/8)-1 end  as Group_Date ,  Secondry_Tbl.date as [Second_Date]," & _
           " TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2," & _
            " TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.Pincode, TSPL_COMPANY_MASTER.Tin_No,TSPL_COMPANY_MASTER.TinNo_Issue_Date,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.PanNo_Issue_Date," & _
            " TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2 ,(case when isnull(TSPL_COMPANY_MASTER.Phone1,'')<>'' then TSPL_COMPANY_MASTER.Phone1 +', ' end  + case when isnull(TSPL_COMPANY_MASTER.Phone1,'')<>'' then TSPL_COMPANY_MASTER.Phone1 end ) as Comp_Phone, TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 ," & _
            " (case when isnull(TSPL_COMPANY_MASTER.Add1,'')<>'' then TSPL_COMPANY_MASTER.Add1   end   + Case when isnull(TSPL_COMPANY_MASTER.Add2,'')<>'' then ', '+ TSPL_COMPANY_MASTER.Add2 else '' end + case when isnull(TSPL_COMPANY_MASTER.State,'')<>'' then ', ' + TSPL_COMPANY_MASTER.State else '' end  ) as Comp_Address, " & _
             " TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name],(case when isnull(TSPL_VENDOR_MASTER.Add1,'')<>'' then TSPL_VENDOR_MASTER.Add1 end   + Case when isnull(TSPL_VENDOR_MASTER.Add2,'')<>'' then ', ' + TSPL_VENDOR_MASTER.Add2 else '' end + case when isnull(TSPL_VENDOR_MASTER.State,'')<>'' then ', ' + TSPL_VENDOR_MASTER.State else '' end + case when isnull(TSPL_VENDOR_MASTER.Pin_Code,'')<>'' then ', ' + TSPL_VENDOR_MASTER.Pin_Code else '' end  ) as Vendor_Address, " & _
              " Tspl_Gate_Entry_Details.Challan_No As [Challan No],  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status],  Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No],Tspl_Gate_Entry_Details.Gate_Entry_Type, Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No],  Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) as Weighment_date,   TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] " & _
             "   From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No  " & _
             " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Tspl_Gate_Entry_Details.Comp_Code " & _
            " right  join (sELECT  TOP (DATEDIFF(DAY,convert(date,'" + txtFromDate.Text + "',103),DATEADD(day,1, convert(date,'" + txtToDate.Text + "' ,103)) )) " & _
                    " Date = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1,convert(date,'" + txtFromDate.Text + "',103)) " & _
           " FROM    sys.all_objects a CROSS JOIN sys.all_objects b ) as Secondry_Tbl on convert(date,Secondry_Tbl.Date,103)=convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) " & _
             " where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Text + "',103) " & _
                  " and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Text + "' ,103)" & _
                      " and Tspl_Gate_Entry_Details.location_Code  ='" + fndLocation.Value + "' " & _
                      " and Tspl_Gate_Entry_Details.Vendor_Code ='" + fndcustNo.Value + "' " & _
              " and Tspl_Gate_Entry_Details.Doc_Type in ('BulkProc')  and Gate_Entry_Type='J' ) As xx)"

        qry1 += " select  max(Comp_Name) as Comp_Name,max(Comp_Phone) as Comp_Phone,max(Comp_Address) as Comp_Address,max(Pincode) as Pincode,max(Tin_No) as Tin_No,max(Pan_No) as Pan_No,max([Vendor Name]) as [Vendor Name],max(Vendor_Address) as Vendor_Address " & _
            ",sum([Net Weight]) as [Milk Received Qty (Kg)],isnull(sum(FATKG_in_Mixed),0) as [Qty of Fat (In Mixed Milk (Kg))],isnull(sum(FATKG_in_SkimMilk),0) as [Qty of Fat (In Skim Milk (Kg))],isnull(sum(FATKG_in_Others),0) as [Qty of Fat (In Others (Kg))],isnull(sum(SNFKG),0) as [Qty of SNF (Kg)], CASE WHEN DATEPART(YYYY,min(Second_Date))<>DATEPART(YYYY,dateadd(dd,7,min(Second_Date))) " & _
                    " THEN CAST(DATEPART(DD,min(Second_Date)) AS VARCHAR)+' ' +DATENAME(MONTH,min(Second_Date))+' ' +DATENAME(YYYY,min(Second_Date))+' - '+ " & _
                   " CAST(DATEPART(DD,dateadd(dd,7,min(Second_Date))) AS VARCHAR)+' ' +DATENAME(MM,dateadd(dd,7,min(Second_Date)))+' ' +DATENAME(YYYY,dateadd(dd,7,min(Second_Date))) " & _
                   "  else CAST(DATEPART(DD,min(Second_Date)) AS VARCHAR)+' '+DATENAME(MM,min(Second_Date))+' - '+CAST(DATEPART(DD,dateadd(dd,7,min(Second_Date))) AS VARCHAR)+' '+DATENAME(MM,dateadd(dd,7,min(Second_Date)))+' '+ DATENAME(YYYY,dateadd(dd,7,min(Second_Date)) ) END  as Date_Range,'' as From_Date,'' as To_Date " & _
                     " from wholeData  group by Group_Date "

        qry2 = "with JobWork_Dispatch as(select TSPL_SCRAPSALE_HEAD.cust_Code,VirtualCategoryTabel.BRAND,VirtualCategoryTabel.[SUB BRAND],TSPL_SCRAPSALE_Detail.shipment_No ,TSPL_SCRAPSALE_Detail.shipped_Qty,TSPL_SCRAPSALE_Detail.SNF,TSPL_SCRAPSALE_Detail.FAT,TSPL_SCRAPSALE_Detail.ItemAmt,TSPL_SCRAPSALE_Detail.ItemNetAmt,TSPL_SCRAPSALE_Detail.TotalAmt,convert(varchar(20),TSPL_SCRAPSALE_HEAD.Shipment_Date,103) as Shipment_Date,TSPL_SCRAPSALE_HEAD.ship_Total_Amt,TSPL_ITEM_UOM_DETAIL.Job_Work_Rate ,TSPL_ITEM_MASTER.Rate,TSPL_SCRAPSALE_Detail.price, " & _
                " isnull(TSPL_SCRAPSALE_Detail.price,0)*((TSPL_SCRAPSALE_Detail.shipped_Qty* case when ISNULL(FATSNFConvertedUnit.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor end )/case when ISNULL(ConvertedUnit_KG.Conversion_Factor,0)=0 then 1 else ConvertedUnit_KG.Conversion_Factor end ) as New_Amt " & _
                " ,((TSPL_SCRAPSALE_Detail.shipped_Qty* case when ISNULL(FATSNFConvertedUnit.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor end )/case when ISNULL(ConvertedUnit_KG.Conversion_Factor,0)=0 then 1 else ConvertedUnit_KG.Conversion_Factor end ) as QtyKg," & _
                "  (((TSPL_SCRAPSALE_Detail.shipped_Qty* case when ISNULL(FATSNFConvertedUnit.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor end )/case when ISNULL(ConvertedUnit_KG.Conversion_Factor,0)=0 then 1 else ConvertedUnit_KG.Conversion_Factor end ) *(case when isnull(fat,0)=0 then 1 else fat end ))/100 as FatKg," & _
              "  (((TSPL_SCRAPSALE_Detail.shipped_Qty* case when ISNULL(FATSNFConvertedUnit.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor end )/case when ISNULL(ConvertedUnit_KG.Conversion_Factor,0)=0 then 1 else ConvertedUnit_KG.Conversion_Factor end ) *(case when isnull(snf,0)=0 then 1 else snf end ))/100 as SnfKg " & _
               " from TSPL_SCRAPSALE_HEAD left join TSPL_SCRAPSALE_Detail on TSPL_SCRAPSALE_Detail.shipment_No =TSPL_SCRAPSALE_HEAD.shipment_No  " & _
                " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_SCRAPSALE_HEAD.Loc_Code  " & _
                " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code  " & _
               " left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code  " & _
                " and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=TSPL_SCRAPSALE_Detail.Item_Code   and FATSNFConvertedUnit.UOM_Code= TSPL_SCRAPSALE_Detail.Unit_code " & _
                 "left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit_KG on ConvertedUnit_KG.Item_Code=TSPL_SCRAPSALE_Detail.Item_Code and ConvertedUnit_KG.UOM_Code=   'KG' " & _
                  "  LEFT OUTER JOIN  (select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND],max([DESCRP]) as [DESCRP],max([PACK]) as [PACK],max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA],max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW],max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC]) as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC],max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC],max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC],max([SCRAPDESC]) as [SCRAPDESC]  from ( " & _
                     "  select * from ( select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code  " & _
                       ",TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " & _
                " from TSPL_ITEM_MASTER left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
              " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
           " where 2=2 )xx " & _
       " Pivot( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE],[CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP]) " & _
            " ) Pivt  Pivot ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC],[BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC],[CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC]) " & _
            ") Pivt1 ) xxx  group by Item_Code)AS VirtualCategoryTabel ON TSPL_SCRAPSALE_Detail.item_Code=VirtualCategoryTabel.Item_Code " & _
            " where TSPL_SCRAPSALE_HEAD.Loc_Code='" + fndLocation.Value + "' " & _
            " and TSPL_SCRAPSALE_HEAD.Cust_Code='" + fndcustNo.Value + "' " & _
           " and TSPL_SCRAPSALE_HEAD.shipment_Date>=convert(date,'" + txtFromDate.Text + "',103) and TSPL_SCRAPSALE_HEAD.shipment_Date<=convert(date,'" + txtToDate.Text + "',103) ) "

        qry2 += " select [SUB BRAND], SUM(QtyKg) AS QtyKg,sum(TotalAmt) as Amount,sum(TotalAmt)/sum(QtyKg) as Rate from JobWork_Dispatch GROUP BY [SUB BRAND] "

        dt1 = clsDBFuncationality.GetDataTable(qry1)
        dt1 = clsDBFuncationality.GetDataTable(qry2)
        'Dim QryLst As New ArrayList
        'QryLst.Add(qry1)
        'QryLst.Add(qry2)
        'Return QryLst
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        LoadDataQuery()
        If dt1.Rows.Count > 0 Then
            frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt1, dt2, "RptJobWorkDebitNot", "Job Work Debit Note", "RptJobWorkProductManufactured")
        End If

    End Sub
End Class
