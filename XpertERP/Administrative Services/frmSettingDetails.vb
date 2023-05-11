Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions


Public Class frmSettingDetails
    Inherits FrmMainTranScreen
    Public Const colSlNo As String = "SLNO"
    Public Const colValue As String = "Value"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim QrySheet As String
    Const colVendorCode As String = "colVendorCode"
    Const colVendorDesc As String = "colVendorDesc"


  

    
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmContractTanker)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If      
    End Sub

    Private Sub FrmParameterValueMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
      If e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterValueMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadModule()
        LoadScreen()
    End Sub
    Public Sub LoadModule()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Bulk Proc"
        dr("Name") = "Bulk Proc"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.ValueMember = "Code"
        cboModule.DisplayMember = "Name"
    End Sub
    Private Sub LoadScreen()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Common"
        dr("Name") = "Common"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "frmGateEntry"
        dr("Name") = "frmGateEntry"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "frmWeighment"
        dr("Name") = "frmWeighment"
        dt.Rows.Add(dr)

        cboScreen.DataSource = dt
        cboScreen.ValueMember = "Code"
        cboScreen.DisplayMember = "Name"
    End Sub
    Sub LoadSettings()
        gv1.DataSource = Nothing
        Dim qry As String = Nothing

        qry = "Select 'Allow GateEntry Against PO' as SettingCode,'On - Gate entry will be created only after PO. and PO is mandatory for Gate entry.' as Description,'Gho' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        " union all " + Environment.NewLine + _
        "Select 'Allow Manual Price ON Bulk PO' as SettingCode,'On -  Price will entered manually On PO screen.No use of Master Price chart and This setting works only if PO is mandotory before gate entry.' as Description,'Gho' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Allow Job Work ON Gate Enty BulkProc' as SettingCode,'On - Both Type contractor and MCC can be done Job wise.  Off - W/O Job work Procurement.'   as Description,'On - KDIL, Off - ' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'AllowGateEntryInPrevDate' as SettingCode,'On -Previous date Gate entry allowed Off - Previous date Gate entry not allowed .' as Description,'Common' as Client,'Common' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'AllowToSaveTimeWithDocumentDate' as SettingCode,'On -Document Saved with Time - Document Saved withOut Time .' as Description,'UDL' as Client,'Common' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Gate Entry tanker From Master' as SettingCode,'On - Bulk procurement will Run Multiple Chamber wise.Tanker will come form Contracted tanker in case of Contractor milk option on gate entry .Bulk Milk SRN and Milk Transfer In created automatically on weighment screen.    Off- tanker entered manually in case of contracted tanker and Only One Chabmber is allowed. ' as Description,'On - UDL, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Hide Rate and Dispatch Centre Code' as SettingCode,'This setting works only in case of TankerFromMaster- On .If tanker from master setting is Off then dispatch centre code is not visible w/o Hide setting. On - Dispatch Centre Code is visible.    Off- Dispatch centre code not visible. ' as Description,'On - UDL, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Bulk Procurement Counter On Entry Type' as SettingCode,'On - Purchase and Job work sereis created based on gate entry type.    Off- Only one series gate entry. ' as Description,'On - UDL, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Allow Tanker Based on Vendor of GE' as SettingCode,'This setting works only in case of TankerFromMaster- On . On - Tanker is selected absed on vendor mapped in contracted tanker.    Off- No mapping required of tanker and vendor. ' as Description,'On - UDL, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Allow Bulk Proc MCC without Tanker Dispatch' as SettingCode,'On - MCC cycle will run w/o tanker dispatch MCC.Tanker and challan selected manually .    Off- Tanker will come from tanker dispatch in MCC cycle. ' as Description,'On - GHO, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
         "union all " + Environment.NewLine + _
        "Select 'Show GateEntryType on GateEntry BulkProc' as SettingCode,'On - If setting On then Gate entry Type (Purchase/Job work) visible on gate entry screen . Off - Gate entry type not visible on screen'   as Description,'On - GK dairy, Off - KDIL' as Client,'frmGateEntry' as Screen,'Bulk Proc' as Module " & _
        "union all " + Environment.NewLine + _
        "Select 'Allow Random OnlyOne SecondaryQC' as SettingCode,'This Setting and Seconday QC screen works only in case of (Gate Entry tanker From Master) setting is On.  If this setting is ON then secondary QC is not mandatory for every QC.system will pick random QC on seconady QC screen OFF - System will not create Auto SRN'   as Description,'On - GHO, Off - KDIL' as Client,'frmWeighment' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Allow Bulk Procurement Sequence wise' as SettingCode,'On - This setting allows bulk proc run sequence wise or not (weightment and QC sequence) OFF - Weighment and QC both screen works independent.'   as Description,'On - UDL, Off - KDIL' as Client,'frmWeighment' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'First QC then Weighment' as SettingCode,'This setting works only if Sequence wise setting is ON  If On then weighment can be created after QC .  OFF - If bulk proc run sequence wise On then  QC can be created only after weighment'   as Description,'On - UDL, Off - KDIL' as Client,'frmWeighment' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Bulk Proc NetWeight Calculation by Vendor Weight' as SettingCode,'On - Net Wt calculated with reduction vendor wt entered on vedor master .  OFF - If bulk proc run sequence wise On then  QC can be created only after weighment'   as Description,'On - WHOLLYJOY, Off - KDIL' as Client,'frmWeighment' as Screen,'Bulk Proc' as Module " + Environment.NewLine + _
        "union all " + Environment.NewLine + _
        "Select 'Auto Tanker Weightment' as SettingCode,'On - If setting On then weighment done by machine. Off - weighment done manually.'   as Description,'On - WHOLLYJOY, Off - KDIL' as Client,'frmWeighment' as Screen,'Bulk Proc' as Module " & _
         "union all " + Environment.NewLine + _
        "Select 'Allow Zero Standardrate On BulkProc PriceChart' as SettingCode,'On - If setting On then zero is allowed in Standard Rate field . Off - Standard Rate is mandatory.'   as Description,'On - GK dairy, Off - KDIL' as Client,'frmPriceChartBulkProc' as Screen,'Bulk Proc' as Module " & _
          "union all " + Environment.NewLine + _
        "Select 'Allow Auto BulkMilkSRN on Weighment BulkProc' as SettingCode,'On - If setting On then Bulk Ilk SRN is auto created at the time of Weighment posted. Off - No auto SRN created.'   as Description,'On - GK dairy, Off - KDIL' as Client,'frmBulkMilkSRN' as Screen,'Bulk Proc' as Module " & _
          "union all " + Environment.NewLine + _
          "Select 'Allow Auto BulkMilkSRN on Weighment BulkProc' as SettingCode,'On - If setting On then Bulk Ilk SRN is auto created at the time of Weighment posted. Off - No auto SRN created.'   as Description,'On - GK dairy, Off - KDIL' as Client,'frmQualityCheck' as Screen,'Bulk Proc' as Module " & _
          "union all " + Environment.NewLine + _
           "Select 'Allow Use Boiling Parameter on Parameter Master' as SettingCode,'On - If setting On then parameter added Acid before boiling and acid after boiling at the time of QC. Off - Acid boiling parameters not added.'   as Description,'On - KDIL, Off - '' as Client,'frmQualityCheck' as Screen,'Bulk Proc' as Module " & _
          "union all " + Environment.NewLine + _
        "Select 'Run Bulk Proc without Milk Grade' as SettingCode,'On - This setting works only in case of TankerFromMaster- On If setting On then in case of chamber wise Milk grade is not required. Off - Milk grade is requied in chamber wise bulk proc.'   as Description,'On - GK Diary, Off - '' as Client,'frmQualityCheck' as Screen,'Bulk Proc' as Module " & _
         "union all " + Environment.NewLine + _
        "Select 'Check ParameterRange ProcurementType wise' as SettingCode,'On - If setting On then parameter range checked on Procurement type (Contractor/MCC) QC. Off - Parameter range checked w/o Proc Type.'   as Description,'On - GK Diary, Off - '' as Client,'frmQualityCheck' as Screen,'Bulk Proc' as Module " & _
        "union all " + Environment.NewLine + _
        "Select 'Pick CorrectionFactor ProcurementType wise' as SettingCode,'On - If setting On then Correction factor used based on procurement type(Purchase/MCC/Job) MCCdefaultCorrectionFactorBS For MCC/JOBdefaultCorrectionFactorBS For Job/PurchasedefaultCorrectionFactorBS for Purchase same are also used for chamber wise like UDL Off - Common correction factor for All Type defaultCorrectionFactor.'   as Description,'On - GK Dairy, Off - KDIL' as Client,'frmQualityCheck' as Screen,'Bulk Proc' as Module "

        Dim SqlQyery = "Select * from ( " & qry & " ) xx where Screen='" & cboScreen.SelectedValue & "' and module='" & cboModule.SelectedValue & "'"

        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = clsDBFuncationality.GetDataTable(qry)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableFiltering = True
        gv1.Enabled = True
        gv1.BestFitColumns()
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
   
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
        GC.Collect()
    End Sub

   
    Private Sub rbtnReset_Click(sender As Object, e As EventArgs) Handles rbtnReset.Click
        reset()
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            If cboModule.SelectedValue = "" Then
                clsCommon.MyMessageBoxShow("Please select Module First ", Me.Text)
                Exit Sub
                'ElseIf cboScreen.SelectedValue = "" Then
                '    clsCommon.MyMessageBoxShow("Please select Screen First ", Me.Text)
                '    Exit Sub
            End If
            LoadSettings()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

   
    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
End Class
