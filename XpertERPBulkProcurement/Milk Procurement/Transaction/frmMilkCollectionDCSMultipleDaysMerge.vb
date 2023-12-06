Imports System.Data.SqlClient
Imports common
Imports Telerik

Public Class frmMilkCollectionDCSMultipleDaysMerge
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim SettShowAllMCC As Boolean
    Dim settFillRouteTankerNo As Boolean = False
    Dim isNewEntry As Boolean = False

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Dim SettMilkCollectionFATSNFType As Integer
    Dim SettFATSNFNoDecimalDCS As Boolean
    Dim SettShowAllDCS As Boolean
    Dim settSNFDecimalPlace As Integer = 0
    Dim SettHeaderFATSNFKGDecimalPlaces As Integer = 3
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim corrFactor As Decimal = 0
    Dim SettMilkCollectionFATSNFTypeHeader As Integer

#End Region
    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim coll As New Dictionary(Of String, String)
        coll.Add("Document_No", "Varchar(30) not null Primary key")
        coll.Add("Document_Date", "Datetime NOT NULL")
        coll.Add("Route_Code", "Varchar(30) not null references TSPL_BULK_ROUTE_MASTER(ROUTE_NO)")
        coll.Add("Tanker_No", "Varchar(20) not null references TSPL_TANKER_MASTER(Tanker_No)")
        coll.Add("Entered_Qty", "Decimal(18,3) null")
        coll.Add("Entered_FATKg", "Decimal(18,3) null")
        coll.Add("Entered_SNFKg", "Decimal(18,3) null")
        coll.Add("Description", "Varchar(200) null")
        coll.Add("FAT_SNF_Type", "int Null")
        coll.Add("Status", "Integer NOT NULL DEFAULT 0")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_Date", "datetime null")
        coll.Add("Posted_By", "varchar(12)  NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", coll, Nothing, True, False, "", "Document_No", "Document_Date")

        coll = New Dictionary(Of String, String)
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE(Document_No)")
        coll.Add("Against_DCS_Multiple_Days", "Varchar(30) not null unique references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS(Document_No)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCUMENT", coll, Nothing, True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", "Document_No", "")

        coll = New Dictionary(Of String, String)
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE(Document_No)")
        coll.Add("IDate", "Date NOT NULL")
        coll.Add("Qty", "Decimal(18,2) null")
        coll.Add("FAT", "Decimal(18,2) null")
        coll.Add("SNF", "Decimal(18,2) null")
        coll.Add("FATKG", "Decimal(18,3) null")
        coll.Add("SNFKG", "Decimal(18,3) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL", coll, Nothing, True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", "Document_No", "")



        SettShowAllMCC = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Nothing)) = 1)
        settFillRouteTankerNo = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FillRouteTankerNo, clsFixedParameterCode.FillRouteTankerNo, Nothing)) = 1)
        corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
        isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        SettMilkCollectionFATSNFType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
        SettFATSNFNoDecimalDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFNoDecimalDCS, clsFixedParameterCode.FATSNFNoDecimalDCS, Nothing))
        SettShowAllDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllDCS, clsFixedParameterCode.ShowAllDCS, Nothing))
        settSNFDecimalPlace = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, Nothing))
        SettHeaderFATSNFKGDecimalPlaces = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HeaderFATSNFKGDecimalPlaces, clsFixedParameterCode.HeaderFATSNFKGDecimalPlaces, Nothing))
        SettMilkCollectionFATSNFTypeHeader = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFTypeHeader, clsFixedParameterCode.MilkCollectionFATSNFTypeHeader, Nothing))
        If isPickCLRInsteadOfSNF Then
            MyLabel14.Text = "CLR"
        End If
        MyBase.SetUserMgmt(clsUserMgtCode.MilkCollectionDCS)
        LoadFATSNFType()
        txtDate.Value = clsCommon.GETSERVERDATE()
        AddNew()
        If SettMilkCollectionFATSNFTypeHeader = 0 Then
            txtTotEnteredFATPer.Enabled = True
            txtTotEnteredSNFPer.Enabled = True
            txtTotEnteredFAT.Enabled = False
            txtTotEnteredSNF.Enabled = False
        Else
            txtTotEnteredFATPer.Enabled = False
            txtTotEnteredSNFPer.Enabled = False
            txtTotEnteredFAT.Enabled = True
            txtTotEnteredSNF.Enabled = True
        End If
    End Sub
    Public Sub LoadFATSNFType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "%"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        cboFATSNFType.DataSource = dt
        cboFATSNFType.ValueMember = "Code"
        cboFATSNFType.DisplayMember = "Name"
    End Sub
    Public Function LoadShift() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True

        If SettMilkCollectionFATSNFType = 0 Then
            cboFATSNFType.SelectedValue = "0"
        Else
            cboFATSNFType.SelectedValue = "1"
        End If
        txtDocNo.Value = ""

        txtDesc.Text = ""


        txtRoute.Value = ""
        lblRoute.Text = ""
        txtTankerNo.Value = ""

        txtTotEnteredQty.Text = ""

        isNewEntry = True


        txtTotReceivedQty.Text = ""
        txtTotPendingQty.Text = ""
        txtTotEnteredFATPer.Text = ""
        txtTotEnteredFAT.Text = ""
        txtTotReceivedFAT.Text = ""
        txtTotPendingFAT.Text = ""
        txtTotEnteredSNFPer.Text = ""
        txtTotEnteredSNF.Text = ""
        txtTotReceivedSNF.Text = ""
        txtTotPendingSNF.Text = ""
        txtTotPendingFATPer.Text = ""
        txtTotPendingSNFPer.Text = ""

        UsLock1.Status = ERPTransactionStatus.Pending


        txtDate.Enabled = True
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        'Prevent future date transaction
        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            clsCommon.MyMessageBoxShow(Me, "Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
            txtDate.Focus()
            Return False
        End If
        Return True
    End Function
    Public Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkCollectionDCSMulipleDaysMerge()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Route_Code = txtRoute.Value
                obj.Tanker_No = txtTankerNo.Value
                obj.Entered_Qty = clsCommon.myCDecimal(txtTotEnteredQty.Text)
                obj.Entered_FATKg = clsCommon.myCDecimal(txtTotEnteredFAT.Text)
                obj.Entered_SNFKg = clsCommon.myCDecimal(txtTotEnteredSNF.Text)
                obj.FAT_SNF_Type = cboFATSNFType.SelectedValue
                obj.ArrDoc = New List(Of clsMilkCollectionDCSMulipleDaysMergeDocument)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCBool(gv1.Rows(ii).Cells("sel").Value) Then
                        Dim objtr As New clsMilkCollectionDCSMulipleDaysMergeDocument
                        objtr.Against_DCS_Multiple_Days = clsCommon.myCstr(gv1.Rows(ii).Cells("Document_No").Value)
                        obj.ArrDoc.Add(objtr)
                    End If
                Next
                If (obj.ArrDoc Is Nothing OrElse obj.ArrDoc.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Document")
                End If

                obj.Arr = New List(Of clsMilkCollectionDCSMulipleDaysMergeDayDetail)
                For ii As Integer = 0 To gv2.Rows.Count - 1
                    Dim objtr As New clsMilkCollectionDCSMulipleDaysMergeDayDetail
                    objtr.IDate = clsCommon.myCDate(gv2.Rows(ii).Cells("Collection_Date").Value)
                    objtr.Qty = clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value)
                    objtr.FAT = clsCommon.myCDecimal(gv2.Rows(ii).Cells("FAT").Value)
                    objtr.SNF = clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNF").Value)
                    objtr.FATKG = clsCommon.myCDecimal(gv2.Rows(ii).Cells("FATKG").Value)
                    objtr.SNFKG = clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNFKG").Value)
                    obj.Arr.Add(objtr)
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("No Date details found to save")
                End If

                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            'frmSRN.IsPoSavedAuto = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            gv1.DataSource = Nothing
            gv2.DataSource = Nothing
            Dim obj As New clsMilkCollectionDCSMulipleDaysMerge()
            obj = clsMilkCollectionDCSMulipleDaysMerge.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                txtDate.Enabled = False
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtRoute.Value = obj.Route_Code
                lblRoute.Text = obj.Route_Name
                txtTankerNo.Value = obj.Tanker_No

                txtTotEnteredQty.Value = obj.Entered_Qty
                txtTotEnteredFAT.Value = obj.Entered_FATKg
                txtTotEnteredSNF.Value = obj.Entered_SNFKg
                txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
                txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)


                If isPickCLRInsteadOfSNF Then
                    txtTotEnteredSNFPer.Value = clsEkoPro.getClrOnCalculation(txtTotEnteredFATPer.Value, txtTotEnteredSNFPer.Value, corrFactor)
                End If
                txtDesc.Text = obj.Description



                Dim qry As String = "select cast(1 as bit) as sel ,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Against_DCS_Multiple_Days as Document_No,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_NAME
,(select min(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No) as Collection_Date_Min
,(select max(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No) as Collection_Date_Max
,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_Qty ,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_FATKg,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_SNFKg 
from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS 
left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Against_DCS_Multiple_Days
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
where  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Document_No='" + obj.Document_No + "' order by TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.PK_Id"
                FormatGrid1(clsDBFuncationality.GetDataTable(qry))

                qry = "select IDate as Collection_Date,Qty,FAT,SNF,FATKG,SNFKG  from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL where Document_No ='" + obj.Document_No + "' order by PK_Id"
                FormatGrid2(clsDBFuncationality.GetDataTable(qry))

                UpdateAllTotal(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub FormatGrid2(ByVal dt As DataTable)
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv2.DataSource = dt
            For ii As Integer = 0 To gv2.Columns.Count - 1
                gv2.Columns(ii).ReadOnly = True
                gv2.Columns(ii).FormatString = ""
                gv2.Columns(ii).BestFit()
            Next

            gv2.Columns("Collection_Date").HeaderText = "Date"
            gv2.Columns("Collection_Date").IsVisible = True
            gv2.Columns("Collection_Date").Width = 80

            gv2.Columns("Qty").HeaderText = "Qty"
            gv2.Columns("Qty").IsVisible = True
            gv2.Columns("Qty").Width = 90

            gv2.Columns("FAT").HeaderText = "FAT %"
            gv2.Columns("FAT").IsVisible = True
            gv2.Columns("FAT").Width = 90
            gv2.Columns("FAT").ReadOnly = False

            gv2.Columns("SNF").HeaderText = If(isPickCLRInsteadOfSNF, "CLR %", "SNF %")
            gv2.Columns("SNF").IsVisible = True
            gv2.Columns("SNF").Width = 90
            gv2.Columns("SNF").ReadOnly = False

            gv2.Columns("FATKG").HeaderText = "FAT Kg"
            gv2.Columns("FATKG").IsVisible = True
            gv2.Columns("FATKG").Width = 100

            gv2.Columns("SNFKG").HeaderText = "SNF Kg"
            gv2.Columns("SNFKG").IsVisible = True
            gv2.Columns("SNFKG").Width = 100

            gv2.AllowAddNewRow = False
            gv2.AllowDeleteRow = False
            gv2.AllowRowReorder = False
            gv2.ShowGroupPanel = False
            gv2.EnableFiltering = False
            gv2.ShowFilteringRow = False
            gv2.EnableSorting = False
            gv2.EnableGrouping = False
            gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv2.GridBehavior = New MyBehavior()
        End If
    End Sub

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnClose.Enabled AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            CancelPressed()
        End If
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_No,convert (varchar,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Document_Date,103) as Document_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Description ,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Tanker_No,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Status=1 then 'Posted' else 'Pending' end as Status 
    from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE
    left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE.Route_Code "
        LoadData(clsCommon.ShowSelectForm("SMP3FIOC", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkCollectionDCSMulipleDaysMerge.DeleteData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
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
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkCollectionDCSMulipleDaysMerge.PostData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.Enter Then
            gv1.BeginEdit()
        End If
    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please Select Document No")
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_MILK_COLLECTION_DCS", "TSPL_MILK_COLLECTION_DCS_DETAIL")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDate_Leave(sender As Object, e As EventArgs) Handles txtDate.Leave
        'LoadTransactionData()
    End Sub
    Private Sub txRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            If Not SettShowAllMCC Then
                whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            End If
            txtRoute.Value = clsCommon.ShowSelectForm("dd33ShUp", qry, "Code", whrCls, txtRoute.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtRoute.Value) > 0 Then
                qry = "select  TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Tanker_No,TSPL_TANKER_MASTER.TANKER_NAME from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + txtRoute.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblRoute.Text = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME"))
                    If settFillRouteTankerNo Then
                        txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                    End If
                End If
            End If
            'LoadTransactionData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTotEnteredQty_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTotEnteredQty.Validating, txtTotEnteredFAT.Validating, txtTotEnteredSNF.Validating, txtTotEnteredFATPer.Validating, txtTotEnteredSNFPer.Validating
        UpdateAllTotal(True)
    End Sub
    Function GetBaseQuery(ByVal arrSelectedDoc As List(Of String)) As String
        Dim qry As String = "select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_Qty,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_FATKg,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_SNFKg,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Collection_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNFKG
from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL
left outer join  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code='" + txtRoute.Value + "' and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No='" + txtTankerNo.Value + "' AND TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Status=0"
        If arrSelectedDoc IsNot Nothing AndAlso arrSelectedDoc.Count > 0 Then
            qry += " and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No in (" + clsCommon.GetMulcallString(arrSelectedDoc) + ")"
        End If
        qry += " and not exists (select 1 from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS where TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Against_DCS_Multiple_Days=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No and  TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS.Document_No not in ('" + txtDocNo.Value + "'))"
        Return qry
    End Function
    Private Sub gv1_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not isInsideLoadData Then
            If gv1.CurrentColumn Is gv1.Columns("sel") Then
                LoadDetailData(e.NewValue, True)
            End If
        End If
    End Sub
    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            LoadDocumentData(Nothing)
            LoadDetailData(True, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadDocumentData(ByVal arrSelectedDoc As List(Of String))
        Dim qry As String = "select cast(1 as bit) as sel ,Document_No,max(MCC_Code) as MCC_Code,max(Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,max(MCC_NAME) as MCC_NAME,convert(varchar, min(Collection_Date),103) as Collection_Date_Min,convert(varchar,max(Collection_Date),103) as Collection_Date_Max,max(Entered_Qty) as Entered_Qty,max(Entered_FATKg) as Entered_FATKg,max(Entered_SNFKg) as Entered_SNFKg from (" + GetBaseQuery(arrSelectedDoc) + ")xx group by Document_No"
        FormatGrid1(clsDBFuncationality.GetDataTable(qry))
    End Sub
    Private Sub FormatGrid1(ByVal dt As DataTable)
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = dt
            For ii As Integer = 1 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).FormatString = ""
                gv1.Columns(ii).BestFit()
            Next
            gv1.Columns("sel").HeaderText = " "

            gv1.Columns("Document_No").HeaderText = "Document No"
            gv1.Columns("Document_No").IsVisible = False

            gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            gv1.Columns("MCC_Code").IsVisible = False

            gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC"
            gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
            gv1.Columns("Mcc_Code_VLC_Uploader").Width = 100

            gv1.Columns("MCC_NAME").HeaderText = "BMC Name"
            gv1.Columns("MCC_NAME").IsVisible = True
            gv1.Columns("MCC_NAME").Width = 100

            gv1.Columns("Collection_Date_Min").HeaderText = "From Date"
            gv1.Columns("Collection_Date_Min").IsVisible = True
            gv1.Columns("Collection_Date_Min").Width = 100

            gv1.Columns("Collection_Date_Max").HeaderText = "To Date"
            gv1.Columns("Collection_Date_Max").IsVisible = True
            gv1.Columns("Collection_Date_Max").Width = 100

            gv1.Columns("Entered_Qty").HeaderText = "Qty"
            gv1.Columns("Entered_Qty").IsVisible = False
            gv1.Columns("Entered_FATKg").HeaderText = "FATKg"
            gv1.Columns("Entered_FATKg").IsVisible = False
            gv1.Columns("Entered_SNFKg").HeaderText = "SNFKg"
            gv1.Columns("Entered_SNFKg").IsVisible = False

            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.AllowRowReorder = False
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = False
            gv1.ShowFilteringRow = False
            gv1.EnableSorting = False
            gv1.EnableGrouping = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.GridBehavior = New MyBehavior()
        End If
    End Sub


    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal UpdateTotal As Boolean)
        Dim arr As New List(Of String)
        Dim EnteredQty As Decimal = 0
        Dim EnteredFATKg As Decimal = 0
        Dim EnteredSNFKg As Decimal = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim flag As Boolean = False
            If gv1.CurrentRow.Index = ii Then
                If NewVal Then
                    flag = True
                End If
            Else
                If clsCommon.myCBool(gv1.Rows(ii).Cells("sel").Value) Then
                    flag = True
                End If
            End If
            If flag Then
                arr.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Document_No").Value))
                EnteredQty += clsCommon.myCDecimal(gv1.Rows(ii).Cells("Entered_Qty").Value)
                EnteredFATKg += clsCommon.myCDecimal(gv1.Rows(ii).Cells("Entered_FATKg").Value)
                EnteredSNFKg += clsCommon.myCDecimal(gv1.Rows(ii).Cells("Entered_SNFKg").Value)
            End If
        Next
        If arr Is Nothing OrElse arr.Count = 0 Then
            arr.Add("XXXXYYYYYYYYYZZZZZZZ")
        End If

        Dim qry As String = "select convert(varchar,Collection_Date,103) as Collection_Date,SUM(Qty) as Qty,case when SUM(Qty)=0 then 0.0 else cast( SUM(FATKG)*100/SUM(Qty) as decimal(18,2)) end as FAT,case when SUM(Qty)=0 then 0.0 else CAST( SUM(SNFKG)*100/SUM(Qty) as decimal(18,2)) end as SNF,SUM(FATKG) as FATKG,SUM(SNFKG) as SNFKG  from (" + GetBaseQuery(arr) + ")xx group by Collection_Date order by xx.Collection_Date"
        FormatGrid2(clsDBFuncationality.GetDataTable(qry))
        If isPickCLRInsteadOfSNF Then
            For ii As Integer = 0 To gv2.Rows.Count - 1
                gv2.Rows(ii).Cells("SNF").Value = clsEkoPro.getClrOnCalculation(clsCommon.myCDecimal(gv2.Rows(ii).Cells("FAT").Value), clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNF").Value), corrFactor)
            Next
        End If

        If UpdateTotal Then
            txtTotEnteredQty.Value = EnteredQty
            txtTotEnteredFAT.Value = EnteredFATKg
            txtTotEnteredSNF.Value = EnteredSNFKg
            UpdateAllTotal(False)
        End If
    End Sub



    Private Sub UpdateAllTotal(ByVal isManual As Boolean)
        If isManual Then
            txtTotEnteredFAT.Value = Math.Round(txtTotEnteredQty.Value * txtTotEnteredFATPer.Value / 100, 3, MidpointRounding.ToEven)
            Dim snfPer As Decimal = txtTotEnteredFATPer.Value
            If isPickCLRInsteadOfSNF Then
                snfPer = clsEkoPro.getSnfOnCalculation(txtTotEnteredFATPer.Value, txtTotEnteredFATPer.Value, corrFactor)
            End If
            txtTotEnteredSNF.Value = Math.Round((txtTotEnteredQty.Value * snfPer / 100), 3, MidpointRounding.ToEven)
        Else
            txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
            txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
            If isPickCLRInsteadOfSNF Then
                txtTotEnteredSNFPer.Value = clsEkoPro.getClrOnCalculation(txtTotEnteredFATPer.Value, txtTotEnteredSNFPer.Value, corrFactor)
            End If
        End If


        Dim Qty As Decimal = 0
        Dim FATKg As Decimal = 0
        Dim SNFKg As Decimal = 0
        For ii As Integer = 0 To gv2.Rows.Count - 1
            Qty += clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value)
            FATKg += clsCommon.myCDecimal(gv2.Rows(ii).Cells("FATKG").Value)
            SNFKg += clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNFKG").Value)
        Next
        txtTotReceivedQty.Text = clsCommon.myCstr(Math.Round((Qty), 3, MidpointRounding.ToEven))
        txtTotReceivedFAT.Text = clsCommon.myCstr(Math.Round((FATKg), 3, MidpointRounding.ToEven))
        txtTotReceivedSNF.Text = clsCommon.myCstr(Math.Round((SNFKg), 3, MidpointRounding.ToEven))

        txtTotPendingQty.Text = clsCommon.myCstr(Math.Round((txtTotEnteredQty.Value - (Qty)), 3, MidpointRounding.ToEven))
        txtTotPendingFAT.Text = clsCommon.myCstr(Math.Round((txtTotEnteredFAT.Value - (FATKg)), 3, MidpointRounding.ToEven))
        txtTotPendingSNF.Text = clsCommon.myCstr(Math.Round((txtTotEnteredSNF.Value - (SNFKg)), 3, MidpointRounding.ToEven))

        txtTotPendingFATPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingFAT.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)
        txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingSNF.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)

    End Sub

    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv2.Columns("FAT") OrElse e.Column Is gv2.Columns("SNF") Then
                        UpdateCurrentRowGV2(gv2.CurrentRow.Index)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub UpdateCurrentRowGV2(ByVal ii As Integer)
        If clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 0 Then
            gv2.Rows(ii).Cells("FATKG").Value = Math.Round(clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value) * clsCommon.myCDecimal(gv2.Rows(ii).Cells("FAT").Value) / 100, 3, MidpointRounding.ToEven)
            Dim snfPer As Decimal = clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNF").Value)
            If isPickCLRInsteadOfSNF Then
                snfPer = clsEkoPro.getSnfOnCalculation(clsCommon.myCDecimal(gv2.Rows(ii).Cells("FAT").Value), clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNF").Value), corrFactor)
            End If
            gv2.Rows(ii).Cells("SNFKG").Value = Math.Round(clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value) * snfPer / 100, 3, MidpointRounding.ToEven)

        ElseIf clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 1 Then
            gv2.Rows(ii).Cells("FAT").Value = Math.Round((100 * clsCommon.myCDecimal(gv2.Rows(ii).Cells("FATKG").Value)) / clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value), 1, MidpointRounding.ToEven)
            gv2.Rows(ii).Cells("SNF").Value = Math.Round((100 * clsCommon.myCDecimal(gv2.Rows(ii).Cells("SNFKG").Value)) / clsCommon.myCDecimal(gv2.Rows(ii).Cells("Qty").Value), 2, MidpointRounding.ToEven)
        End If
        UpdateAllTotal(False)
    End Sub
End Class
