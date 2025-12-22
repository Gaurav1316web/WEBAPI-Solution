Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class BMC_Transporter_Bill
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Const colDate As String = "colDate"
    Const colDocumentNo As String = "colDocumentNo"
    Const ColCategory As String = "ColCategory"
    Const ColTrip As String = "ColTrip"
    Const ColKM As String = "ColKM"
    Const ColQuantity As String = "ColQuantity"
    Const ColAmount As String = "ColAmount"
    Const ColDiesel As String = "ColDiesel"
    Const ColStation As String = "ColStation"
    Const ColStation2 As String = "ColStation2"
    Const ColStation3 As String = "ColStation3"
    Const ColStation4 As String = "ColStation4"
    Const ColGPSKM As String = "ColGPSKM"
    Dim TotalAmount As Decimal = 0
    Dim TotalDiesel As Decimal = 0
    Dim TotalQuantity As Decimal = 0
    Dim TotalBMCQuantity As Decimal = 0
    Dim Total_Toll_Tax As Decimal = 0
    Dim Total_Ice_Charge As Decimal = 0
    Dim Total_BMC_TOTAL As Decimal = 0
    Dim Total_fat_snf_shortage As Decimal = 0
    Dim Total_Amount As Decimal = 0


#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Dim qry As String = "SELECT Distinct TSPL_MILK_COLLECTION_MCC.Tanker_No as [Tanker No] FROM TSPL_MILK_COLLECTION_MCC "
        txtTankerNo.Value = clsCommon.ShowSelectForm("RoutMastrCodFND", qry, "Tanker No", "", txtTankerNo.Value, "", isButtonClicked)
        lblTankerDesc.Text = clsDBFuncationality.getSingleValue(" select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No='" + txtTankerNo.Value + "'")
        Dim qry1 As String = clsDBFuncationality.getSingleValue(" select Tanker_Transporter_Code from TSPL_TANKER_MASTER where Tanker_No='" + txtTankerNo.Value + "'")
        txtTransporter.Text = clsDBFuncationality.getSingleValue(" select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + qry1 + "'")
        TxtBarelCap.Text = clsDBFuncationality.getSingleValue(" select Storage_Capacity from TSPL_TANKER_MASTER where Tanker_No='" + txtTankerNo.Value + "'")
    End Sub

    Private Sub BMC_Transporter_Bill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "datetime NOT NULL")
        coll.Add("From_Date", "datetime NOT NULL")
        coll.Add("To_Date", "datetime NOT NULL")
        coll.Add("Status", "integer null")
        coll.Add("Tanker_No", "varchar(20) NULL  References TSPL_TANKER_MASTER(Tanker_No)")
        'coll.Add("Transporter_Name", "varchar(60) NULL")
        coll.Add("Toll_Tax", "decimal(18,2) NULL ")
        coll.Add("Ice_Charge", "decimal(18,2) NULL ")
        coll.Add("Fat_Shortage", "decimal(18,2) NULL ")
        coll.Add("Snf_Shortage", "decimal(18,2) NULL ")
        coll.Add("Fat_Snf_Shortage", "decimal(18,2) NULL ") 'fat and snf shortage to be added
        coll.Add("Fat_Rate", "decimal(18,2) NULL ")
        coll.Add("Snf_Rate", "decimal(18,2) NULL ")
        coll.Add("Tanker_Capacity", "decimal(18,2) NULL ")
        coll.Add("KM_Rate", "decimal(18,2) NULL ")
        coll.Add("Total_Amount", "decimal(18,2) NULL ")
        coll.Add("Gross_Amount", "decimal(18,2) NULL ")
        coll.Add("Diesel_Rate_Plus", "decimal(18,2) NULL ")
        coll.Add("Diesel_Rate_Minus", "decimal(18,2) NULL ")
        coll.Add("Total_Diesel", "decimal (18,2) NULL")
        coll.Add("Prorata_Amt", "decimal (18,2) NULL")
        coll.Add("Total_Before_Calc", "decimal (18,2) NULL")
        coll.Add("Created_By", "varchar(12)  NOT NULL")
        coll.Add("Created_Date", "datetime  NOT NULL")
        coll.Add("Modify_By", "varchar(12)  NOT NULL")
        coll.Add("Modify_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BMC_TRANSPORTER_BILL_HEAD", coll, Nothing, True, False, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION PRIMARY KEY")
        coll.Add("Document_Code", "varchar(30) NOT NULL References TSPL_BMC_TRANSPORTER_BILL_HEAD(Document_Code)")
        coll.Add("MCC_Document_Code", "varchar(30) NOT NULL UNIQUE References TSPL_MILK_COLLECTION_MCC(Document_No)")
        'coll.Add("Document_Date", "datetime NOT NULL")
        'coll.Add("Category", "varchar(40)  NOT NULL")
        coll.Add("Station_1", "varchar(40)  NULL")
        coll.Add("Station_2", "varchar(40)  NULL")
        coll.Add("Station_3", "varchar(40)  NULL")
        coll.Add("Station_4", "varchar(40)  NULL")
        coll.Add("Trip", "decimal (18,2) NULL")
        coll.Add("GPS_KM", "decimal (18,2) NULL")
        coll.Add("KM", "decimal (18,2) NULL") ' add gps km column
        coll.Add("Quantity_KG", "decimal (18,2) NULL")
        coll.Add("Amount", "decimal (18,2) NULL")
        coll.Add("Diesel_RD", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BMC_TRANSPORTER_BILL_DETAIL", coll, Nothing, True, False, "TSPL_BMC_TRANSPORTER_BILL_HEAD", "Document_Code", "", True)


        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        RadGroupBox3.Enabled = False
        ReStoreGridLayout()
        LoadHeadData()

    End Sub

    Sub LoadHeadData()
        Try
            TxtKMRate.Text = clsDBFuncationality.getSingleValue(" Select top 1 KM_Rate from TSPL_BMC_TRANSPORTER_BILL_HEAD order by Document_Date desc")
            TxtDieselMinus.Text = clsDBFuncationality.getSingleValue(" Select top 1 Diesel_Rate_Minus from TSPL_BMC_TRANSPORTER_BILL_HEAD order by Document_Date desc")
            txtDieselplus.Text = clsDBFuncationality.getSingleValue(" Select top 1 Diesel_Rate_Plus from TSPL_BMC_TRANSPORTER_BILL_HEAD order by Document_Date desc")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Width = 150
        repoDate.ReadOnly = True
        repoDate.IsVisible = True

        ' Set only the date format (e.g., "dd/MM/yyyy")
        repoDate.FormatString = "{0:dd/MM/yyyy}"
        repoDate.FormatInfo = Globalization.CultureInfo.InvariantCulture
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoDocumentNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocumentNo.FormatString = ""
        repoDocumentNo.HeaderText = "Document No"
        repoDocumentNo.Name = colDocumentNo
        repoDocumentNo.Width = 150
        repoDocumentNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDocumentNo)

        Dim repoCategory As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategory.FormatString = ""
        repoCategory.HeaderText = "Category"
        repoCategory.Name = ColCategory
        repoCategory.Width = 150
        repoCategory.IsVisible = True
        repoCategory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCategory)

        Dim repoStation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStation.FormatString = ""
        repoStation.HeaderText = "Station"
        repoStation.Name = ColStation
        repoStation.Width = 150
        repoStation.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStation)

        Dim repoStation2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStation2.FormatString = ""
        repoStation2.HeaderText = "Station 2"
        repoStation2.Name = ColStation2
        repoStation2.Width = 150
        repoStation2.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStation2)

        Dim repoStation3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStation3.FormatString = ""
        repoStation3.HeaderText = "Station 3"
        repoStation3.Name = ColStation3
        repoStation3.Width = 150
        repoStation3.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStation3)

        Dim repoStation4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStation4.FormatString = ""
        repoStation4.HeaderText = "Station 4"
        repoStation4.Name = ColStation4
        repoStation4.Width = 150
        repoStation4.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStation4)

        Dim repoTrip As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTrip.FormatString = ""
        repoTrip.HeaderText = "Trip"
        repoTrip.Name = ColTrip
        repoTrip.Width = 150
        repoTrip.IsVisible = True
        repoTrip.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTrip)

        Dim repoKM As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoKM = New GridViewDecimalColumn()
        repoKM.FormatString = ""
        repoKM.HeaderText = "KM"
        repoKM.WrapText = True
        repoKM.Name = ColKM
        repoKM.Width = 150
        repoKM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoKM.VisibleInColumnChooser = False
        repoKM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoKM)

        Dim repoGPSKM As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGPSKM = New GridViewDecimalColumn()
        repoGPSKM.FormatString = ""
        repoGPSKM.HeaderText = "GPSKM"
        repoGPSKM.WrapText = True
        repoGPSKM.Name = ColGPSKM
        repoGPSKM.Width = 150
        repoGPSKM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoGPSKM.VisibleInColumnChooser = False
        repoGPSKM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoGPSKM)

        Dim repoQuantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQuantity = New GridViewDecimalColumn()
        repoQuantity.FormatString = ""
        repoQuantity.HeaderText = "Quantity(KG)"
        repoQuantity.WrapText = True
        repoQuantity.Name = ColQuantity
        repoQuantity.Width = 150
        repoQuantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQuantity.VisibleInColumnChooser = False
        repoQuantity.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoQuantity)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.WrapText = True
        repoAmount.Name = ColAmount
        repoAmount.Width = 150
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.VisibleInColumnChooser = False
        repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmount)

        Dim repoDiesel As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiesel = New GridViewDecimalColumn()
        repoDiesel.FormatString = ""
        repoDiesel.HeaderText = "Diesel(RD)"
        repoDiesel.WrapText = True
        repoDiesel.Name = ColDiesel
        repoDiesel.Width = 150
        repoDiesel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDiesel.VisibleInColumnChooser = False
        repoDiesel.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDiesel)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False



        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ' gv1.Rows.AddNew()

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
        LoadHeadData()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        ReStoreGridLayout()
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtTankerNo.Value = ""
        lblTankerDesc.Text = ""
        txtTransporter.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        TxtDieselMinus.Text = ""
        txtDieselplus.Text = ""
        TxtTollTax.Text = ""
        TxtIceCharge.Text = ""
        TxtKMRate.Text = ""
        TxtRateprorata.Text = ""
        TxtTDR.Text = ""
        TxtTankerprorata.Text = ""
        TxtBarelCap.Text = ""
        txtFatShortage.Text = ""
        TxtSnfShortage.Text = ""
        TxtFatRate.Text = ""
        TxtSnfRate.Text = ""
        TxtGrossAmount.Text = ""
        TxtTotalFatSnfShortage.Text = ""
        TxtTotalIceCharge.Text = ""
        TxtTotalTollTax.Text = ""
        TxtTotalAmount.Text = ""
        TxtBMCProrataamt.Text = ""
        TxtBMCDiesel.Text = ""
        TxtBMCTotal.Text = ""
        isNewEntry = True
        btnGo.Enabled = True
        gv1.Rows.Clear()

        gv1.SummaryRowsBottom.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadBlankGrid()
        loadGridData()
        FatSnfShortageDetail()
        FatSnfRate()
        FatSnfCalculation()
        IceCharge()
        AddSummaryRowToGrid()

    End Sub

    Sub FatSnfCalculation()
        Dim FatShortage As Double = clsCommon.myCdbl(txtFatShortage.Text)
        Dim SnfShortage As Double = clsCommon.myCdbl(TxtSnfShortage.Text)
        Dim fatrate As Double = clsCommon.myCdbl(TxtFatRate.Text)
        Dim snfRate As Double = clsCommon.myCdbl(TxtSnfRate.Text)
        Dim FatAmt As Double = clsCommon.myCdbl(FatShortage * fatrate)
        Dim SnfAmt As Double = clsCommon.myCdbl(SnfShortage * snfRate)
        TxtTotalFatSnfShortage.Text = clsCommon.myCdbl(FatAmt + SnfAmt)
    End Sub

    Sub IceCharge()
        Try
            Dim rowCount As Integer = gv1.Rows.Count
            Dim GrossAmt As Double = 0
            Dim totalAmt As Double = 0
            Dim Icecharge As Double = clsCommon.myCdbl(TxtIceCharge.Text)
            If rowCount > 0 Then
                TxtTotalIceCharge.Text = clsCommon.myCdbl(Icecharge * rowCount)
                GrossAmt = clsCommon.myCdbl(TxtGrossAmount.Text)
                totalAmt = clsCommon.myCdbl(TxtTotalAmount.Text)
                TxtTotalAmount.Text = totalAmt + clsCommon.myCdbl(TxtTotalIceCharge.Text)
                TxtGrossAmount.Text = GrossAmt + clsCommon.myCdbl(TxtTotalIceCharge.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FatSnfRate()
        Try
            Dim qry As String = ""
            'qry = " SELECT TOP 1  Loss_FAT_Rate,Loss_SNF_Rate FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE Tanker_Rate = 1  AND convert(date,Start_Date,103) <= convert(date,'" + txtDate.Value + "',103) and End_Date is null 
            'ORDER BY Start_Date DESC "
            qry = " SELECT TOP 1  Loss_FAT_Rate,Loss_SNF_Rate FROM TSPL_OWN_BMC_GAIN_LOSS_RATE WHERE convert(date,Start_Date,103) <= convert(date,'" + txtDate.Value + "',103) and End_Date is null 
                    ORDER BY Start_Date DESC "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim Fat_Rate As Decimal = 0
            Dim Snf_Rate As Decimal = 0
            For Each row As DataRow In dt.Rows
                'If clsCommon.myCdbl(row("ADJFATKG")) < 0 Then
                Fat_Rate += clsCommon.myCdbl(row("Loss_FAT_Rate"))
                'End If
                'If clsCommon.myCdbl(row("ADJSNFKG")) < 0 Then
                Snf_Rate += clsCommon.myCdbl(row("Loss_SNF_Rate"))
                'End If
            Next
            TxtFatRate.Text = Fat_Rate
            TxtSnfRate.Text = Snf_Rate


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FatSnfShortageDetail()
        Try
            Dim qry As String = ""
            qry = " select (sum(Original_FATKg) * 0.25/100) + sum(FATKG) as ADJFATKG,(sum(Original_SNFKg) * 0.25/100) + sum(SNFKG) as ADJSNFKG from 
                (Select * from 
                (Select * FROM 
                ( select MAX(convert(Varchar,TSPL_MILK_COLLECTION_MCC.Document_Date,103)) AS DocumentDate,
                (TSPL_MILK_COLLECTION_MCC.Tanker_No)Tanker_No,(TSPL_MILK_COLLECTION_MCC.Route_Code)Route_Code,
                sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Qty) AS Original_Qty,
                sum(TSPL_MILK_COLLECTION_MCC_DETAIL.FATKg)Original_FATKg,
                sum(TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKg)Original_SNFKg,
 (max(TSPL_MILK_COLLECTION_MCC.Entered_FATKg)-sum(TSPL_MILK_COLLECTION_MCC_DETAIL.FATKg)) AS FATKG,
 (max(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg)-sum(TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKg)) AS SNFKG
from TSPL_MILK_COLLECTION_MCC 
LEFT OUTER JOIN TSPL_MILK_COLLECTION_MCC_DETAIL ON TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No
left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_MCC.Tanker_No
where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
and convert(date,Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' ,103) 
and TSPL_MILK_COLLECTION_MCC.Tanker_No in ('" + clsCommon.myCstr(txtTankerNo.Value) + "') GROUP BY Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,Document_Date)ZZZ)xxxx)zzzz group by zzzz.Route_Code,zzzz.Tanker_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim negativeFatKg As Decimal = 0
            Dim negativeSnfKg As Decimal = 0
            For Each row As DataRow In dt.Rows
                If clsCommon.myCdbl(row("ADJFATKG")) < 0 Then
                    negativeFatKg += clsCommon.myCdbl(row("ADJFATKG"))
                End If
                If clsCommon.myCdbl(row("ADJSNFKG")) < 0 Then
                    negativeSnfKg += clsCommon.myCdbl(row("ADJSNFKG"))
                End If
            Next
            txtFatShortage.Text = negativeFatKg
            TxtSnfShortage.Text = negativeSnfKg
            TxtTotalFatSnfShortage.Text = negativeFatKg + negativeSnfKg

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub loadGridData()
        Try
            'isInsideLoadData = True
            Dim qry As String = ""
            Dim qry1 As String = ""
            Dim qry2 As String = ""

            If clsCommon.myLen(txtDieselplus.Text) = 0 AndAlso clsCommon.myLen(TxtDieselMinus.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Enter Diesel Amount", Me.Text)
                txtDieselplus.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(TxtKMRate.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Enter KMRate Amount", Me.Text)
                TxtKMRate.Focus()
                Exit Sub
            End If

            qry = " WITH RankedLocations AS (SELECT TSPL_MILK_COLLECTION_MCC.Document_No,CAST(TSPL_MILK_COLLECTION_MCC.Document_Date AS DATE) AS Document_Date,Route_Code,
                    Trip_No,TSPL_TANKER_MASTER.Storage_Capacity,TSPL_BULK_ROUTE_MASTER.Distance,TSPL_BULK_route_master_location.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,ROW_NUMBER() OVER (
                    PARTITION BY TSPL_MILK_COLLECTION_MCC.Document_No ORDER BY TSPL_BULK_route_master_location.Location_Code) AS LocRank FROM TSPL_MILK_COLLECTION_MCC  
                    LEFT JOIN TSPL_TANKER_MASTER ON TSPL_TANKER_MASTER.Tanker_No = TSPL_MILK_COLLECTION_MCC.Tanker_No
                    LEFT JOIN TSPL_BULK_ROUTE_MASTER ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code
                    LEFT JOIN TSPL_BULK_route_master_location ON TSPL_BULK_route_master_location.BULK_ROUTE_No = TSPL_MILK_COLLECTION_MCC.Route_Code
                    left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_route_master_location.Location_Code
                    WHERE CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and
                    CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103)
                    AND TSPL_MILK_COLLECTION_MCC.Tanker_No = '" + clsCommon.myCstr(txtTankerNo.Value) + "')
                    SELECT Document_No,Document_Date,Route_Code,Trip_No,Storage_Capacity,Distance,MAX(CASE WHEN LocRank = 1 THEN Location_Desc END) AS Station_1,
                    MAX(CASE WHEN LocRank = 2 THEN Location_Desc END) AS Station_2,MAX(CASE WHEN LocRank = 3 THEN Location_Desc END) AS Station_3,MAX(CASE WHEN LocRank = 4 THEN Location_Desc END) AS Station_4
                    FROM RankedLocations GROUP BY Document_No,Document_Date,Route_Code,Trip_No,Storage_Capacity,Distance  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry1 = " Select max(Distance)Distance from TSPL_BULK_ROUTE_MASTER where Tanker_No='" + clsCommon.myCstr(txtTankerNo.Value) + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
            qry2 = " Select Storage_Capacity from TSPL_TANKER_MASTER where Tanker_No='" + clsCommon.myCstr(txtTankerNo.Value) + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
            For ii As Integer = 0 To dt.Rows.Count - 1
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colDocumentNo).Value = clsCommon.myCstr(dt.Rows(ii)("Document_No"))
                gv1.CurrentRow.Cells(colDate).Value = clsCommon.myCstr(dt.Rows(ii)("Document_Date"))
                gv1.CurrentRow.Cells(ColCategory).Value = "BMC"
                gv1.CurrentRow.Cells(ColStation).Value = "Jaipur>>" & clsCommon.myCstr(dt.Rows(0)("Station_1"))
                gv1.CurrentRow.Cells(ColStation2).Value = clsCommon.myCstr(dt.Rows(0)("Station_2"))
                gv1.CurrentRow.Cells(ColStation3).Value = clsCommon.myCstr(dt.Rows(0)("Station_3"))
                gv1.CurrentRow.Cells(ColStation4).Value = clsCommon.myCstr(dt.Rows(0)("Station_4"))
                gv1.CurrentRow.Cells(ColTrip).Value = clsCommon.myCstr(dt.Rows(ii)("Trip_No"))
                gv1.CurrentRow.Cells(ColKM).Value = clsCommon.myCdbl(dt.Rows(ii)("Distance"))
                gv1.CurrentRow.Cells(ColQuantity).Value = clsCommon.myCdbl(dt.Rows(ii)("Storage_Capacity"))
                gv1.CurrentRow.Cells(ColAmount).Value = clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Distance"))) * clsCommon.myCdbl(TxtKMRate.Text)
                'gv1.CurrentRow.Cells(ColAmount).Value = clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Distance")))
                'gv1.CurrentRow.Cells(ColDiesel).Value = clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Distance")))
                If clsCommon.myLen(txtDieselplus.Text) > 0 Then
                    gv1.CurrentRow.Cells(ColDiesel).Value = clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Distance")) * clsCommon.myCdbl(txtDieselplus.Text))
                Else
                    gv1.CurrentRow.Cells(ColDiesel).Value = -1 * clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Distance")) * clsCommon.myCdbl(TxtDieselMinus.Text))
                End If

            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub TxtIceCharge_TextChanged(sender As Object, e As EventArgs) Handles TxtIceCharge.TextChanged
        IceCharge()
        'Dim fromDate As Date = txtFromDate.Value
        'Dim toDate As Date = txtToDate.Value
        'Dim totalDays As Integer = DateDiff(DateInterval.Day, fromDate, toDate) + 1
        ''Console.WriteLine("Total Days (exclusive): " & totalDays)

        ''TxtTotalIceCharge.Text = clsCommon.myCdbl(TxtIceCharge.Text * totalDays)
        '' Check if input is a valid number
        'Dim iceCharge As Double
        'If Double.TryParse(TxtIceCharge.Text, iceCharge) Then
        '    TxtTotalIceCharge.Text = clsCommon.myCdbl(iceCharge * totalDays).ToString()
        'Else
        '    TxtTotalIceCharge.Text = "0"
        'End If
    End Sub

    Private Sub txtToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtToDate.ValueChanged
        RadGroupBox3.Enabled = True
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    UpdateCurrentRow(gv1.CurrentRow.Index, Nothing)
                    UpdateAllTotals(Nothing)
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateAllTotals(ByVal trans As SqlTransaction)
        Try
            Dim BMCProrataAmt As Double = 0
            Dim BMCDiesel As Double = 0
            Dim BMCTotalAmt As Double = 0
            Dim TollTaxAmt As Double = 0
            Dim IceCharge As Double = 0
            Dim FatSnfShortage As Double = 0
            Dim AmtBfrGross As Double = 0

            For ii As Integer = 0 To gv1.Rows.Count - 1
                BMCProrataAmt = BMCProrataAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(ColAmount).Value)
                BMCDiesel = BMCDiesel + clsCommon.myCdbl(gv1.Rows(ii).Cells(ColDiesel).Value)
            Next
            TxtBMCProrataamt.Text = clsCommon.myCdbl(BMCProrataAmt)
            TxtBMCDiesel.Text = clsCommon.myCdbl(BMCDiesel)
            If BMCDiesel < 0 Then
                TxtBMCTotal.Text = clsCommon.myCdbl(BMCProrataAmt + BMCDiesel)
                '    'TxtBMCTotal.Text = TotalBMCQuantity
            Else
                TxtBMCTotal.Text = (BMCProrataAmt - BMCDiesel)
                '    'TxtBMCTotal.Text = TotalBMCQuantity
            End If
            BMCTotalAmt = clsCommon.myCdbl(TxtBMCTotal.Text)
            TollTaxAmt = clsCommon.myCdbl(TxtTollTax.Text)
            IceCharge = clsCommon.myCdbl(TxtTotalIceCharge.Text)
            TxtTotalTollTax.Text = clsCommon.myCdbl(TollTaxAmt)
            'TxtTotalAmount.Text = clsCommon.myCdbl(BMCTotalAmt + TollTaxAmt + IceCharge)
            TxtTotalAmount.Text = clsCommon.myCdbl(BMCTotalAmt + TollTaxAmt)
            AmtBfrGross = clsCommon.myCdbl(TxtTotalAmount.Text)
            FatSnfShortage = clsCommon.myCdbl(TxtTotalFatSnfShortage.Text)
            TxtGrossAmount.Text = clsCommon.myCdbl(AmtBfrGross + FatSnfShortage)


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer, ByVal trans As SqlTransaction)
        Try
            Dim dblKM As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColKM).Value)
            Dim dblGPSKM As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColGPSKM).Value)
            Dim KMRate As Double = clsCommon.myCdbl(TxtKMRate.Text)
            Dim DieselPlus As Double = clsCommon.myCdbl(txtDieselplus.Text)
            Dim DieselMinus As Double = -Math.Abs(clsCommon.myCdbl(TxtDieselMinus.Text))
            Dim dblBasicAmt As Double = 0
            Dim dblBasicDiesel As Double = 0
            If dblGPSKM > 0 Then
                dblBasicAmt = KMRate * dblGPSKM
            Else
                dblBasicAmt = KMRate * dblKM
            End If
            If DieselPlus > 0 AndAlso dblGPSKM > 0 Then
                dblBasicDiesel = DieselPlus * dblGPSKM
            ElseIf DieselPlus > 0 AndAlso dblKM > 0 Then
                dblBasicDiesel = DieselPlus * dblKM
            ElseIf DieselMinus < 0 AndAlso dblGPSKM > 0 Then
                dblBasicDiesel = DieselMinus * dblGPSKM
            ElseIf DieselMinus < 0 AndAlso dblKM > 0 Then
                dblBasicDiesel = DieselMinus * dblKM
            End If

            gv1.Rows(IntRowNo).Cells(ColAmount).Value = clsCommon.myCdbl(dblBasicAmt)
            gv1.Rows(IntRowNo).Cells(ColDiesel).Value = clsCommon.myCdbl(dblBasicDiesel)

            isCellValueChangedOpen = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtDate.Value)

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBMCTransporterBill()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Tanker_No = txtTankerNo.Value
                obj.Toll_Tax = clsCommon.myCdbl(TxtTotalTollTax.Text)
                obj.Ice_Charge = clsCommon.myCdbl(TxtTotalIceCharge.Text)
                obj.Fat_Shortage = clsCommon.myCdbl(txtFatShortage.Text)
                obj.Snf_Shortage = clsCommon.myCdbl(TxtSnfShortage.Text)
                obj.Fat_Snf_Shortage = clsCommon.myCdbl(TxtTotalFatSnfShortage.Text)
                obj.Fat_Rate = clsCommon.myCdbl(TxtFatRate.Text)
                obj.Snf_Rate = clsCommon.myCdbl(TxtSnfRate.Text)
                obj.Tanker_Capacity = clsCommon.myCdbl(TxtBarelCap.Text)
                obj.KM_Rate = clsCommon.myCdbl(TxtKMRate.Text)
                obj.Total_Amount = clsCommon.myCdbl(TxtTotalAmount.Text)
                obj.Gross_Amount = clsCommon.myCdbl(TxtGrossAmount.Text)
                obj.Diesel_Rate_Plus = clsCommon.myCdbl(txtDieselplus.Text)
                obj.Diesel_Rate_Minus = clsCommon.myCdbl(TxtDieselMinus.Text)
                obj.Total_Diesel = clsCommon.myCdbl(TxtBMCDiesel.Text)
                obj.Prorata_Amt = clsCommon.myCdbl(TxtBMCProrataamt.Text)
                obj.Total_Before_Calc = clsCommon.myCdbl(TxtBMCTotal.Text)

                Dim totalTrip As Double = 0
                Dim totalGPSKM As Double = 0
                Dim totalKM As Double = 0
                Dim totalQty As Double = 0
                Dim totalAmt As Double = 0
                Dim totalDiesel As Double = 0


                obj.Arr = New List(Of clsBMCTransporterBillDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBMCTransporterBillDetail()
                    objTr.MCC_Document_Code = clsCommon.myCstr(grow.Cells(colDocumentNo).Value)
                    objTr.Station_1 = clsCommon.myCstr(grow.Cells(ColStation).Value)
                    objTr.Station_2 = clsCommon.myCstr(grow.Cells(ColStation2).Value)
                    objTr.Station_3 = clsCommon.myCstr(grow.Cells(ColStation3).Value)
                    objTr.Station_4 = clsCommon.myCstr(grow.Cells(ColStation4).Value)
                    objTr.Trip = clsCommon.myCdbl(grow.Cells(ColTrip).Value)
                    objTr.GPS_KM = clsCommon.myCdbl(grow.Cells(ColGPSKM).Value)
                    objTr.KM = clsCommon.myCdbl(grow.Cells(ColKM).Value)
                    objTr.Quantity_KG = clsCommon.myCdbl(grow.Cells(ColQuantity).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(ColAmount).Value)
                    objTr.Diesel_RD = clsCommon.myCdbl(grow.Cells(ColDiesel).Value)

                    totalTrip += objTr.Trip
                    totalGPSKM += objTr.GPS_KM
                    totalKM += objTr.KM
                    totalQty += objTr.Quantity_KG
                    totalAmt += objTr.Amount
                    totalDiesel += objTr.Diesel_RD


                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDocumentNo).Value)) > 0 Then
                        obj.Arr.Add(objTr)

                    End If
                Next
                AddSummaryRow(totalTrip, totalGPSKM, totalKM, totalQty, totalAmt, totalDiesel)


                If (obj.SaveData(obj, isNewEntry)) Then
                    If Not isPost Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub AddSummaryRow(totalTrip As Double, totalGPSKM As Double, totalKM As Double, totalQty As Double, totalAmt As Double, totalDiesel As Double)
        Try
            ' Remove existing summary rows
            gv1.SummaryRowsBottom.Clear()

            ' Enable footer and summary
            gv1.ShowColumnHeaders = True

            Dim summaryRow As New GridViewSummaryRowItem()

            summaryRow.Add(New GridViewSummaryItem(colDocumentNo, "Grand Total", GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColTrip, totalTrip.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColGPSKM, totalGPSKM.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColKM, totalKM.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColQuantity, totalQty.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColAmount, totalAmt.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColDiesel, totalDiesel.ToString("N2"), GridAggregateFunction.Sum))

            gv1.SummaryRowsBottom.Add(summaryRow)
        Catch ex As Exception
            MessageBox.Show("Error adding summary row: " & ex.Message)
        End Try
    End Sub




    Sub LoadData(ByVal strDocumentNo As String, NavType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = "Update"
            txtDocNo.MyReadOnly = True
            isInsideLoadData = True
            BlankAllControls()
            LoadBlankGrid()
            isNewEntry = False

            'btnGo.Enabled = False

            Dim obj As New clsBMCTransporterBill()
            obj = clsBMCTransporterBill.GetData(strDocumentNo, NavType, True, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtTankerNo.Value = obj.Tanker_No
                TxtTotalTollTax.Text = obj.Toll_Tax
                TxtTotalIceCharge.Text = obj.Ice_Charge
                txtFatShortage.Text = obj.Fat_Shortage
                TxtSnfShortage.Text = obj.Snf_Shortage
                TxtTotalFatSnfShortage.Text = obj.Fat_Snf_Shortage
                TxtFatRate.Text = obj.Fat_Rate
                TxtSnfRate.Text = obj.Snf_Rate
                TxtBarelCap.Text = obj.Tanker_Capacity
                TxtKMRate.Text = obj.KM_Rate
                TxtTotalAmount.Text = obj.Total_Amount
                TxtGrossAmount.Text = obj.Gross_Amount
                txtDieselplus.Text = obj.Diesel_Rate_Plus
                TxtDieselMinus.Text = obj.Diesel_Rate_Minus
                TxtBMCDiesel.Text = obj.Total_Diesel
                TxtBMCTotal.Text = obj.Total_Before_Calc
                TxtBMCProrataamt.Text = obj.Prorata_Amt
                Dim qry1 As String = clsDBFuncationality.getSingleValue(" select Tanker_Transporter_Code from TSPL_TANKER_MASTER where Tanker_No='" + txtTankerNo.Value + "'")
                txtTransporter.Text = clsDBFuncationality.getSingleValue(" select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + qry1 + "'")
                Dim fromDate As Date = obj.From_Date
                Dim toDate As Date = obj.To_Date
                Dim totalDays As Integer = DateDiff(DateInterval.Day, fromDate, toDate) + 1
                'Console.WriteLine("Total Days (exclusive): " & totalDays)

                'TxtTotalIceCharge.Text = clsCommon.myCdbl(TxtIceCharge.Text * totalDays)
                ' Check if input is a valid number
                'Dim iceCharge As Double
                'If Double.TryParse(TxtTotalIceCharge.Text, iceCharge) Then
                '    TxtIceCharge.Text = clsCommon.myCdbl(iceCharge / totalDays).ToString()
                'Else
                '    TxtIceCharge.Text = "0"
                'End If

                TxtTollTax.Text = obj.Toll_Tax
                'Dim TollTax As Double
                'If Double.TryParse(TxtTotalTollTax.Text, TollTax) Then
                '    TxtTollTax.Text = clsCommon.myCdbl(TollTax / totalDays).ToString()
                'Else
                '    TxtTollTax.Text = "0"
                'End If

                If obj.Arr IsNot Nothing Then
                    Dim qry As String = " SELECT TSPL_MILK_COLLECTION_MCC.Document_No,Cast(TSPL_MILK_COLLECTION_MCC.Document_Date as Date)Document_Date,Route_Code,Trip_No,TSPL_TANKER_MASTER.Storage_Capacity,TSPL_BULK_ROUTE_MASTER.Distance
                                            FROM TSPL_MILK_COLLECTION_MCC  
                    left outer join TSPL_TANKER_MASTER ON TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_MCC.Tanker_No
                    left outer join TSPL_BULK_ROUTE_MASTER ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code
					where convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= convert(date,'" + txtFromDate.Value + "' ,103)
                    and  convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= convert(date,'" + txtToDate.Value + "' ,103)
                    and TSPL_MILK_COLLECTION_MCC.Tanker_No = '" & txtTankerNo.Value & "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                    'For ii As Integer = 0 To dt.Rows.Count - 1
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
                    '    gv1.Rows.AddNew()
                    'Next
                    Dim i As Integer = 0
                    For Each objrow As clsBMCTransporterBillDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocumentNo).Value = objrow.MCC_Document_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColGPSKM).Value = objrow.GPS_KM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColKM).Value = objrow.KM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = objrow.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDiesel).Value = objrow.Diesel_RD
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStation).Value = objrow.Station_1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStation2).Value = objrow.Station_2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStation3).Value = objrow.Station_3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStation4).Value = objrow.Station_4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTrip).Value = objrow.Trip
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColQuantity).Value = objrow.Quantity_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCategory).Value = "BMC"
                        If dt.Rows.Count > i Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dt.Rows(i)("Document_Date"))
                        End If
                        i += 1
                        AddSummaryRowToGrid()
                    Next

                    Dim rowCount As Integer = i
                    Dim Icecharges As Double = clsCommon.myCdbl(TxtTotalIceCharge.Text)
                    If rowCount > 0 Then
                        TxtIceCharge.Text = clsCommon.myCdbl(Icecharges / rowCount)
                    End If
                End If
            End If
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub AddSummaryRowToGrid()
        Try
            ' Enable footer row
            gv1.ShowColumnHeaders = True

            ' Clear existing summary rows
            gv1.SummaryRowsBottom.Clear()

            ' Calculate totals
            Dim totalTrip As Double = 0
            Dim totalGPSKM As Double = 0
            Dim totalKM As Double = 0
            Dim totalQty As Double = 0
            Dim totalAmt As Double = 0
            Dim totalDiesel As Double = 0

            For Each row As GridViewRowInfo In gv1.Rows
                totalTrip += clsCommon.myCdbl(row.Cells(ColTrip).Value)
                totalGPSKM += clsCommon.myCdbl(row.Cells(ColGPSKM).Value)
                totalKM += clsCommon.myCdbl(row.Cells(ColKM).Value)
                totalQty += clsCommon.myCdbl(row.Cells(ColQuantity).Value)
                totalAmt += clsCommon.myCdbl(row.Cells(ColAmount).Value)
                totalDiesel += clsCommon.myCdbl(row.Cells(ColDiesel).Value)
            Next

            ' Add summary row
            Dim summaryRow As New GridViewSummaryRowItem()
            summaryRow.Add(New GridViewSummaryItem(ColCategory, "Grand Total", GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColTrip, totalTrip.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColGPSKM, totalGPSKM.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColKM, totalKM.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColQuantity, totalQty.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColAmount, totalAmt.ToString("N2"), GridAggregateFunction.Sum))
            summaryRow.Add(New GridViewSummaryItem(ColDiesel, totalDiesel.ToString("N2"), GridAggregateFunction.Sum))

            gv1.SummaryRowsBottom.Add(summaryRow)
        Catch ex As Exception
            MessageBox.Show("Error while adding summary row: " & ex.Message)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
            End If
            If (clsBMCTransporterBill.DeleteData(txtDocNo.Value)) Then
                saveCancelLog(Reason, "Delete", Nothing)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                'SaveData(True)
                If (clsBMCTransporterBill.PostData(txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            '  txtDocNo.Value = clsBMCTransporterBill.getFinder(Nothing, txtDocNo.Value, isButtonClicked)

            Dim qry As String = "select TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code ,convert(varchar,TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_date,103) as Document_date,TSPL_BMC_TRANSPORTER_BILL_HEAD.Tanker_No,case when TSPL_BMC_TRANSPORTER_BILL_HEAD.status =1  then 'Approved' else 'Pending' end as Status   
                             from TSPL_BMC_TRANSPORTER_BILL_HEAD "
            'Str = clsCommon.ShowSelectForm("fndPayProcess", qry, "Document_Code", "", curcode, "Document_Code", isButtonClicked, "Document_date")
            txtDocNo.Value = clsCommon.ShowSelectForm("fmGroup_Code", qry, "Document_Code", "", txtDocNo.Value, "", isButtonClicked)
            'Dim qry As String = "select TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code ,convert(varchar,TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_date,103) as Document_date,TSPL_BMC_TRANSPORTER_BILL_HEAD.Tanker_No,case when TSPL_BMC_TRANSPORTER_BILL_HEAD.status =1  then 'Approved' else 'Pending' end as Status   
            '                 from TSPL_BMC_TRANSPORTER_BILL_HEAD "
            'LoadData(clsCommon.ShowSelectForm("TrsToSav@F", qry, "Document_Code", "", txtDocNo.Value, "Document_Code", isButtonClicked, "Document_date"), NavigatorType.Current)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiImport_Click(sender As Object, e As EventArgs) Handles rmiImport.Click
        Dim ReportID As String = MyBase.Form_ID
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmiExport_Click(sender As Object, e As EventArgs) Handles rmiExport.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a document before reversing.", Me.Text)
                Exit Sub
            Else
                If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '' REASON FOR DELETE 
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If

                    clsBMCTransporterBill.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDieselplus_TextChanged(sender As Object, e As EventArgs) Handles txtDieselplus.TextChanged
        Dim rate As Decimal

        ' Validate and parse the rate
        If Decimal.TryParse(txtDieselplus.Text, rate) Then
            For Each row As GridViewRowInfo In gv1.Rows
                If TypeOf row Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                    'If Not row.IsNewRow Then
                    Dim gpskm As Decimal = 0
                    Dim km As Decimal = 0

                    ' Safely try to read GPSKM and KM
                    Decimal.TryParse(row.Cells("ColGPSKM").Value, gpskm)
                    Decimal.TryParse(row.Cells("ColKM").Value, km)

                    ' Use GPSKM if it's greater than 0, otherwise use KM
                    Dim usedKM As Decimal = If(gpskm > 0, gpskm, km)

                    ' Calculate Amount = usedKM * rate
                    row.Cells("ColDiesel").Value = usedKM * rate
                End If
            Next
        End If
    End Sub

    Private Sub TxtDieselMinus_TextChanged(sender As Object, e As EventArgs) Handles TxtDieselMinus.TextChanged
        Dim rate As Decimal

        ' Validate and parse the rate
        If Decimal.TryParse(TxtDieselMinus.Text, rate) Then
            For Each row As GridViewRowInfo In gv1.Rows
                If TypeOf row Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                    'If Not row.IsNewRow Then
                    Dim gpskm As Decimal = 0
                    Dim km As Decimal = 0

                    ' Safely try to read GPSKM and KM
                    Decimal.TryParse(row.Cells("ColGPSKM").Value, gpskm)
                    Decimal.TryParse(row.Cells("ColKM").Value, km)

                    ' Use GPSKM if it's greater than 0, otherwise use KM
                    Dim usedKM As Decimal = If(gpskm > 0, gpskm, km)

                    ' Calculate Amount = usedKM * rate
                    row.Cells("ColDiesel").Value = usedKM * rate
                End If
            Next
        End If
    End Sub

    Private Sub BMC_Transporter_Bill_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                If MyBase.isReverse Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = clsFixedParameterType.SIR
                    frm.strCode = clsFixedParameterCode.SIReversAndCreate
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnReverse.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim FinalQuery As String = ""
            FinalQuery = "   SELECT ROW_NUMBER() OVER (PARTITION BY TSPL_MILK_COLLECTION_MCC.Document_No ORDER BY TSPL_MILK_COLLECTION_MCC.Document_No) AS SrNo
,
 TSPL_BMC_TRANSPORTER_BILL_DETAIL.Document_Code,TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code,
 TSPL_MILK_COLLECTION_MCC.Document_No,Cast(TSPL_MILK_COLLECTION_MCC.Document_Date as Date)Document_Date,Route_Code,Trip_No,TSPL_TANKER_MASTER.Storage_Capacity,TSPL_BULK_ROUTE_MASTER.Distance
, TSPL_BMC_TRANSPORTER_BILL_DETAIL.STATION_1, TSPL_BMC_TRANSPORTER_BILL_DETAIL.STATION_2,TSPL_BMC_TRANSPORTER_BILL_DETAIL.STATION_3,TSPL_BMC_TRANSPORTER_BILL_DETAIL.STATION_4, TSPL_BMC_TRANSPORTER_BILL_DETAIL.GPS_KM,TSPL_BMC_TRANSPORTER_BILL_DETAIL.KM     ,TSPL_BMC_TRANSPORTER_BILL_DETAIL.AMOUNT,TSPL_BMC_TRANSPORTER_BILL_DETAIL.DIESEL_RD
,TSPL_BMC_TRANSPORTER_BILL_HEAD.Tanker_no,TSPL_BMC_TRANSPORTER_BILL_HEAD.toll_tax,
TSPL_BMC_TRANSPORTER_BILL_HEAD.ice_charge,TSPL_BMC_TRANSPORTER_BILL_HEAD.Fat_Shortage,TSPL_BMC_TRANSPORTER_BILL_HEAD.Snf_Shortage,TSPL_BMC_TRANSPORTER_BILL_HEAD.Fat_Snf_Shortage,TSPL_BMC_TRANSPORTER_BILL_HEAD.Fat_Rate,TSPL_BMC_TRANSPORTER_BILL_HEAD.Snf_Rate
,TSPL_TANKER_MASTER.Description,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.State,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.City_Code
,TSPL_BMC_TRANSPORTER_BILL_HEAD.From_Date,TSPL_BMC_TRANSPORTER_BILL_HEAD.To_Date
,TSPL_BMC_TRANSPORTER_BILL_HEAD.KM_Rate,TSPL_BMC_TRANSPORTER_BILL_HEAD.Tanker_Capacity
,	Diesel_Rate_Plus,	Diesel_Rate_Minus	,Total_Diesel,Total_Amount,	Gross_Amount
FROM TSPL_MILK_COLLECTION_MCC  
left outer join TSPL_TANKER_MASTER ON TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_MCC.Tanker_No
 left outer join TSPL_BULK_ROUTE_MASTER ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code
LEFT OUTER JOIN TSPL_BMC_TRANSPORTER_BILL_DETAIL ON TSPL_BMC_TRANSPORTER_BILL_DETAIL.MCC_Document_Code=TSPL_MILK_COLLECTION_MCC.Document_No
LEFT OUTER JOIN TSPL_BMC_TRANSPORTER_BILL_HEAD ON TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code=TSPL_BMC_TRANSPORTER_BILL_DETAIL.Document_Code 
left outer join TSPL_COMPANY_MASTER on 2=2
where TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code='" & txtDocNo.Value & "'  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "crptBmcTranspoterBill_1", "BMC Transpoter Bill") ''report for both (RCDF And RCDFCF)

                frmCRV = Nothing
            End If



            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "crptBmcTranspoterBill", "BMC Transpoter Bill", Nothing) ''report for both (RCDF And RCDFCF)
            '    frmCRV = Nothing
            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExport_Click_1(sender As Object, e As EventArgs) Handles btnExport.Click
        '=======Update By preeti Gupta Against Ticket No[BM00000008831]
        'sql = "select User_Code,User_Name,Password,Emp_Code,Emp_Name,User_Type,Level1_Code,Level2_Code,Level3_Code,Level4_Code from TSPL_USER_MASTER "
        Dim sql As String = Nothing
        sql = "select '' as Date,'BMC' AS Category,'' AS [Station 1],'' AS [Station 2],'' AS [Station 3],''	AS [Station 4],0 AS [Trip],0 AS [GPS KM],0 AS [KM],0 AS [Quantity KG],0 AS [Amount]	,0 AS [Diesel RD]  "
        ListImpExpColumnsMandatory = New List(Of String)({"Document_Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Document_Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        funImport()
    End Sub
    Public Sub funImport()
        Dim gvimport As New UserControls.MyRadGridView
        Me.Controls.Add(gvimport)
        Dim currentdate As Date = Date.Today
        LoadBlankGrid()
        gv1.Rows.AddNew()
        Dim qry1 As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(txtDocNo.Value) > 0 Then

            '    Dim qry1 As String = "select Document_No as [Document No] from TSPL_MILK_COLLECTION_MCC
            'WHERE CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and
            '                   CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103)
            '                   AND TSPL_MILK_COLLECTION_MCC.Tanker_No = '" + clsCommon.myCstr(txtTankerNo.Value) + "'"
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If transportSql.importExcel(gvimport, "Date", "Category", "Station 1", "Station 2", "Station 3", "Station 4", "Trip", "GPS KM", "KM", "Quantity KG", "Amount", "Diesel RD") Then

                Try
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To gvimport.Rows.Count - 1
                        'If clsCommon.myLen(gvimport.Rows(ii).Cells("Document Code").Value) > 0 Then
                        clsCommon.ProgressBarPercentUpdate((gvimport.Rows(ii).Index + 1) * 100 / (gvimport.Rows.Count + 1), "Importing  : " & (gvimport.Rows(ii).Index + 1) & "/" & gvimport.Rows.Count & "")
                        Try
                            'gv1.Rows(ii).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Document No").Value)

                            gv1.Rows(ii).Cells(colDate).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Date").Value)
                            gv1.Rows(ii).Cells(ColCategory).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Category").Value)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("MCC Document Code").Value)
                            gv1.Rows(ii).Cells(ColStation).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 1").Value)
                            gv1.Rows(ii).Cells(ColStation2).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 2").Value)
                            gv1.Rows(ii).Cells(ColStation3).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 3").Value)
                            gv1.Rows(ii).Cells(ColStation4).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 4").Value)
                            gv1.Rows(ii).Cells(ColTrip).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Trip").Value)
                            gv1.Rows(ii).Cells(ColGPSKM).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("GPS KM").Value)
                            gv1.Rows(ii).Cells(ColKM).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("KM").Value)
                            gv1.Rows(ii).Cells(ColQuantity).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Quantity KG").Value)
                            gv1.Rows(ii).Cells(ColDiesel).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Diesel RD").Value)
                            gv1.Rows(ii).Cells(ColAmount).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Amount").Value)
                            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                                If gv1.Rows.Count = gvimport.Rows.Count Then
                                Else
                                    gv1.Rows.AddNew()

                                End If
                            End If
                            'gv1.Rows.AddNew()
                        Catch ex As Exception
                            gv1.Rows.RemoveAt(ii)
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                        'End If
                    Next

                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
            Me.Controls.Remove(gvimport)
        Else
            'Dim gvimport As New UserControls.MyRadGridView
            'Me.Controls.Add(gvimport)
            'Dim currentdate As Date = Date.Today
            'LoadBlankGrid()
            'gv1.Rows.AddNew()

            If clsCommon.myLen(txtFromDate.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a 'From Date'.", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(txtToDate.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select a 'To Date'.", Me.Text)
                Exit Sub
            End If
            ' Validate Tanker Number
            If clsCommon.myLen(txtTankerNo.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please enter a 'Tanker Number'.", Me.Text)
                Exit Sub
            End If

            ' Optional: Validate that From Date is not greater than To Date
            If CDate(txtFromDate.Value) > CDate(txtToDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "'From Date' cannot be greater than 'To Date'.", Me.Text)
                Exit Sub
            End If

            qry1 = "select Document_No as [Document No] from TSPL_MILK_COLLECTION_MCC
        WHERE CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and
                           CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103)
                           AND TSPL_MILK_COLLECTION_MCC.Tanker_No = '" + clsCommon.myCstr(txtTankerNo.Value) + "'"
            dt = clsDBFuncationality.GetDataTable(qry1)
            If transportSql.importExcel(gvimport, "Date", "Category", "Station 1", "Station 2", "Station 3", "Station 4", "Trip", "GPS KM", "KM", "Quantity KG", "Amount", "Diesel RD") Then

                Try
                    clsCommon.ProgressBarPercentShow()
                    If dt IsNot Nothing AndAlso dt.Rows.Count = gvimport.Rows.Count Then

                        For ii As Integer = 0 To gvimport.Rows.Count - 1
                            'If clsCommon.myLen(gvimport.Rows(ii).Cells("Document Code").Value) > 0 Then
                            clsCommon.ProgressBarPercentUpdate((gvimport.Rows(ii).Index + 1) * 100 / (gvimport.Rows.Count + 1), "Importing  : " & (gvimport.Rows(ii).Index + 1) & "/" & gvimport.Rows.Count & "")
                            Try
                                ' gv1.Rows(ii).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Document No").Value)
                                gv1.Rows(ii).Cells(colDocumentNo).Value = clsCommon.myCstr(dt.Rows(ii)("Document No"))

                                gv1.Rows(ii).Cells(colDate).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Date").Value)
                                gv1.Rows(ii).Cells(ColCategory).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Category").Value)
                                'gv1.Rows(gv1.Rows.Count - 1).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("MCC Document Code").Value)
                                gv1.Rows(ii).Cells(ColStation).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 1").Value)
                                gv1.Rows(ii).Cells(ColStation2).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 2").Value)
                                gv1.Rows(ii).Cells(ColStation3).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 3").Value)
                                gv1.Rows(ii).Cells(ColStation4).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 4").Value)
                                gv1.Rows(ii).Cells(ColTrip).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Trip").Value)
                                gv1.Rows(ii).Cells(ColGPSKM).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("GPS KM").Value)
                                gv1.Rows(ii).Cells(ColKM).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("KM").Value)
                                gv1.Rows(ii).Cells(ColQuantity).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Quantity KG").Value)
                                gv1.Rows(ii).Cells(ColDiesel).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Diesel RD").Value)
                                gv1.Rows(ii).Cells(ColAmount).Value = clsCommon.myCdbl(gvimport.Rows(ii).Cells("Amount").Value)
                                If clsCommon.myLen(txtDocNo.Value) = 0 Then
                                    If gv1.Rows.Count = gvimport.Rows.Count Then
                                    Else
                                        gv1.Rows.AddNew()

                                    End If
                                End If
                                'gv1.Rows.AddNew()
                            Catch ex As Exception
                                gv1.Rows.RemoveAt(ii)
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                            'End If
                        Next
                        common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)

                    Else

                        common.clsCommon.MyMessageBoxShow(Me, "Number Of Documnet Are not Same OF IMPORT ROW ", Me.Text, MessageBoxButtons.OK)

                        ' Handle mismatch - optional logging or warning
                        ' MsgBox("Row count mismatch between database result and import grid.")
                    End If
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    '  clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
            Me.Controls.Remove(gvimport)
        End If
        'Try
        'If clsCommon.myLen(txtFromDate.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "Please select a 'From Date'.", Me.Text)
        '    Exit Sub
        'End If
        'If clsCommon.myLen(txtToDate.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "Please select a 'To Date'.", Me.Text)
        '    Exit Sub
        'End If
        '' Validate Tanker Number
        'If clsCommon.myLen(txtTankerNo.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "Please enter a 'Tanker Number'.", Me.Text)
        '    Exit Sub
        'End If

        '' Optional: Validate that From Date is not greater than To Date
        'If CDate(txtFromDate.Value) > CDate(txtToDate.Value) Then
        '    clsCommon.MyMessageBoxShow(Me, "'From Date' cannot be greater than 'To Date'.", Me.Text)
        '    Exit Sub
        'End If
        'Dim gvimport As New UserControls.MyRadGridView
        'Me.Controls.Add(gvimport)
        'Dim currentdate As Date = Date.Today
        'LoadBlankGrid()
        'gv1.Rows.AddNew()

        'Dim qry1 As String = "select Document_No as [Document No] from TSPL_MILK_COLLECTION_MCC
        'WHERE CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and
        '                   CONVERT(DATE, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103)
        '                   AND TSPL_MILK_COLLECTION_MCC.Tanker_No = '" + clsCommon.myCstr(txtTankerNo.Value) + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        'If transportSql.importExcel(gvimport, "Document No", "Date", "Category", "Station 1", "Station 2", "Station 3", "Station 4", "Trip", "GPS KM", "KM", "Quantity KG", "Amount", "Diesel RD") Then

        '    Try
        '        clsCommon.ProgressBarPercentShow()
        '        For ii As Integer = 0 To gvimport.Rows.Count - 1
        '            'If clsCommon.myLen(gvimport.Rows(ii).Cells("Document Code").Value) > 0 Then
        '            clsCommon.ProgressBarPercentUpdate((gvimport.Rows(ii).Index + 1) * 100 / (gvimport.Rows.Count + 1), "Importing  : " & (gvimport.Rows(ii).Index + 1) & "/" & gvimport.Rows.Count & "")
        '            Try
        '                gv1.Rows(ii).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Document No").Value)

        '                gv1.Rows(ii).Cells(colDate).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Date").Value)
        '                gv1.Rows(ii).Cells(ColCategory).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Category").Value)
        '                'gv1.Rows(gv1.Rows.Count - 1).Cells(colDocumentNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("MCC Document Code").Value)
        '                gv1.Rows(ii).Cells(ColStation).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 1").Value)
        '                gv1.Rows(ii).Cells(ColStation2).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 2").Value)
        '                gv1.Rows(ii).Cells(ColStation3).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 3").Value)
        '                gv1.Rows(ii).Cells(ColStation4).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Station 4").Value)
        '                gv1.Rows(ii).Cells(ColTrip).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Trip").Value)
        '                gv1.Rows(ii).Cells(ColGPSKM).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("GPS KM").Value)
        '                gv1.Rows(ii).Cells(ColKM).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("KM").Value)
        '                gv1.Rows(ii).Cells(ColQuantity).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Quantity KG").Value)
        '                gv1.Rows(ii).Cells(ColDiesel).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Diesel RD").Value)
        '                gv1.Rows(ii).Cells(ColAmount).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Amount").Value)
        '                If clsCommon.myLen(txtDocNo.Value) = 0 Then
        '                    If gv1.Rows.Count = gvimport.Rows.Count Then
        '                    Else
        '                        gv1.Rows.AddNew()

        '                    End If
        '                End If
        '                'gv1.Rows.AddNew()
        '            Catch ex As Exception
        '                gv1.Rows.RemoveAt(ii)
        '                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '            End Try
        '            'End If
        '        Next

        '        clsCommon.ProgressBarPercentHide()
        '        common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        clsCommon.ProgressBarPercentHide()
        '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '    End Try
        'End If
        'Me.Controls.Remove(gvimport)
    End Sub

    'Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    '    Me.Close()
    'End Sub

    'Private Sub txtDieselplus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDieselplus.KeyPress
    '    Dim Total_GPS_KM As Decimal = 0
    '    Dim Total_KM As Decimal = 0
    '    Dim Total_Amount_Grid As Decimal = 0
    '    Dim Diesel_Plus As Decimal = 0
    '    Dim Diesel_Minus As Decimal = 0
    '    Dim TotalAmountGrid As Decimal = 0
    '    Dim TotalDiesel As Decimal = 0

    '    TotalDiesel = Total_KM * Diesel_Plus
    '    For ii As Integer = 1 To gv1.Rows.Count
    '        gv1.Rows(ii - 1).Cells(ColDiesel).Value =
    '    Next
    '    If isInsideLoadData = True Then
    '        UpdateCurrentRow(gv1.CurrentRow.Index)
    '    End If
    'End Sub
End Class