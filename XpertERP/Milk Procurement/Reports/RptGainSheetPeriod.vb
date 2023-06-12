'--Created by Preeti Gupta ticket no[BM00000004286]
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptGainSheetPeriod
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim arrLoc As String = Nothing
    Dim StrPermission As String


    'Private Sub LOCATIONRIGTHS()
    '    Try
    '        Dim obj As New clsMCCCodes()
    '        obj = clsMCCCodes.GetData()

    '        If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
    '            arrLoc = obj.arrLocCodes
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptGainSheetPeriod)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
                Me.Close()
                Exit Sub
            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Print(EnumExportTo.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptGainSheetPeriod & "'"))
                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add((" MCC Name: " + strMCCName + " "))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Milk Purchase Bill", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeletelayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RptGainSheetPeriod_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub RptGainSheetPeriod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

       
        Reset()
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(StrPermission) > 0 Then
            qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + StrPermission + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If
    End Sub
    Public Sub Load_Report()
        Try
            ''adding column Transport Code,Transport Name by shivani against[BM00000008360]
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
                txtFromDate.Focus()
                Exit Sub
            End If
            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
                Exit Sub
            End If

            Dim whrcls As String = " where 2=2 "
            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                whrcls += " and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            Else
                whrcls += " And TSPL_MCC_Dispatch_Challan.mcc_Code in (" & StrPermission & ") "
            End If

            whrcls += "  and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
         
            ''============== dispatch return effect excel points by Parteek  on 23/12-2016============''

            ' done by priti KDI/04/07/18-000385 for impact of milk transfer in return

            whrcls += " and   (isnull(TSPL_MILK_TRANSFER_IN.In_Return,0)=0  or  ( select count(Receipt_Challan_No) from TSPL_MILK_TRANSFER_IN where TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO)= (select count(Receipt_Challan_No) from TSPL_MILK_TRANSFER_IN_RETURN where TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO) ) "
            Dim sQuery As String = " select aa.*,convert(decimal(18,2),(coalesce(aa.DisNewQty,0) -coalesce(aa.QcNewQty,0)-coalesce(aa.ReturnQty,0) )) as DIF_QTY," &
                                   " convert(decimal(18,2),(coalesce(aa.DisFatKg,0) -coalesce(aa.QCFATKG,0)-coalesce(aa.retFATKG,0) )) as DIF_FAT_KG,convert(decimal(18,2), " &
                                   " (coalesce(aa.DisSNfKg,0) -coalesce(aa.QCSNFKG,0)-coalesce(aa.RetSNFKG,0) )) as DIF_SNF_KG from(select convert(varchar,zz.Dispatch_Date,103) as Dispatch_Date" &
                                    ",zz.Chalan_NO ,zz.Tanker_No ,zz.Mcc_Or_Plant_Code ,zz.Dispatch_TO ,zz.MCC_Code,zz.MCCCode ,zz.MCC_NAME ,convert(decimal(18,2),zz.DisNewQty) as DisNewQty , " &
                                    "zz.dispUOM ,zz.Conversion_Factor ,convert(decimal(18,2),zz.DisFatKg)as DisFatKg ,convert(decimal(18,2),zz.DisSNfKg) as DisSNfKg , " &
                                    "convert(decimal(18,1),(zz.DisFatKg *100)/zz.DisNewQty) as DisFatPer, " &
                                    " convert(decimal(18,1),(zz.DisSNfKg  *100)/zz.DisNewQty) as DisSNFPer,convert(varchar,zz.Receipt_Challan_Date,103) as Receipt_Challan_Date , " &
                                    "convert(decimal(18,2),zz.QcNewQty) as QcNewQty ,zz.TrnsfrInUOM ,convert(decimal(18,2),zz.QCFATKG ) as QCFATKG , " &
                                    "convert(decimal(18,2),zz.QCSNFKG) as QCSNFKG , " &
                                    "case when zz.QcNewQty > 0 then convert(decimal(18,1),(zz.QCFATKG  *100)/zz.QcNewQty) else 0 end as QCFATPER, " &
                                    "case when zz.QcNewQty > 0 then convert(decimal(18,1),(zz.QCSNFKG   *100)/zz.QcNewQty) else 0 end as QCSNFPER, " &
                                    "zz.Tanker_Transporter_Code ,zz.Description, " &
                                    "zz.Return_NO,zz.Return_Date,zz.ReturnQty,zz.RetFatKG, " &
                                    "zz.RetSNFKG,zz.dispRetFatPer,zz.dispRetSNFPer  from " &
                                    " (select yy.Dispatch_Date ,yy.Chalan_NO ,yy.Tanker_No ,yy.Mcc_Or_Plant_Code,yy.Dispatch_TO  , " &
                                    "yy.MCC_Code,yy.MCCCode ,yy.MCC_NAME ,yy.DisNewQty ,yy.dispUOM ,yy.Conversion_Factor , " &
                                    "(yy.DisNewQty *yy.dispFatPer /100) as DisFatKg,(yy.DisNewQty * yy.dispSNFPer/100 ) as DisSNfKg," &
                                    " yy.Receipt_Challan_Date ,yy.QcNewQty,yy.TrnsfrInUOM  ,(yy.QcNewQty *yy.QCFATPer /100) as QCFATKG, " &
                                    "(yy.QcNewQty *yy.qcSnfPer /100) as QCSNFKG,yy.Description,yy.Tanker_Transporter_Code,yy.Return_NO,yy.Return_Date, " &
                                    "yy.ReturnQty,yy.dispRetFatPer,yy.dispRetSNFPer,yy.RetFatKG,yy.RetSNFKG from ( " &
                                    "select xx.Dispatch_Date ,xx.Chalan_NO ,xx.Tanker_No ,xx.Mcc_Or_Plant_Code ,xx.Dispatch_TO ," &
                                    " xx.MCC_Code,xx.MCCCode ,xx.MCC_NAME,xx.Net_Qty ,xx.Net_Qty *xx.Conversion_Factor  as DisNewQty, " &
                                    "xx.dispUOM ,xx.Conversion_Factor ,xx.dispFatPer ,xx.dispSNFPer ,xx.Receipt_Challan_Date  , " &
                                    "xx.Net_Weight ,xx.Net_Weight *xx.Conversion_Factor as QcNewQty,xx.TrnsfrInUOM,xx.QCFATPer , " &
                                    "xx.qcSnfPer,xx.Tanker_Transporter_Code  ,xx.Description,xx.Return_NO,xx.Return_Date,xx.ReturnQty,xx.dispRetFatPer, " &
                                    "xx.dispRetSNFPer,xx.RetFatKG,xx.RetSNFKG " &
                                    "  from (select TSPL_MCC_Dispatch_Challan.Dispatch_Date,TSPL_MCC_Dispatch_Challan.Chalan_NO,TSPL_MCC_Dispatch_Challan.Tanker_No, " &
                                    "TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code,TSPL_LOCATION_MASTER.Location_Desc as Dispatch_TO, " &
                                    "TSPL_MCC_Dispatch_Challan.MCC_Code+'  - '+TSPL_LOCATION_MASTER_for_Mcc.Location_Code as MCC_Code, " &
                                    "TSPL_MCC_Dispatch_Challan.MCC_Code as MCCCode,TSPL_LOCATION_MASTER_for_Mcc.Location_Desc as MCC_NAME," &
                                    " TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG as Net_Qty ,'" + cboUnit.Text + "' as dispUOM,TSPL_ITEM_UOM_DETAIL.Conversion_Factor , " &
                                    "DispFat.Param_Field_Value as dispFatPer,DispSNF.Param_Field_Value as dispSNFPer,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date , " &
                                    "case when isnull (TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No,'')='' then TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight else 0 end as Net_Weight,'" + cboUnit.Text + "' as TrnsfrInUOM,QcFat.Param_Field_Value as QCFATPer, " &
                                    "Qcsnf.Param_Field_Value as qcSnfPer,TSPL_TANKER_MASTER.Tanker_Transporter_Code ,TSPL_TANKER_MASTER.Description, " &
                                    "isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO,0) as Return_No,isnull(convert(varchar,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_Date,103),0) as Return_Date, " &
                                    "isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Qty_KG,0) as ReturnQty,DispRetFat.Param_Field_Value as dispRetFatPer, " &
                                    "DispRetSNF.Param_Field_Value as dispRetSNFPer, (isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Qty_KG,0)*DispRetFat.Param_Field_Value/100) as RetFatKG, " &
                                    "(isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Qty_KG,0)*DispRetSNF.Param_Field_Value/100) as RetSNFKG " &
                                    " from TSPL_MCC_Dispatch_Challan " &
                                    " left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No " &
                                    " left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail DispFat on DispFat.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and DispFat.Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispFat.sno " &
                                    " left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail DispSNF on Dispsnf.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and DispSNF .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispSNF.sno " &
                                    " left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO " &
                                    " left outer join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No " &
                                    " left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No=TSPL_MILK_TRANSFER_IN.Weighment_No " &
                                    " left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No=TSPL_Weighment_Detail.Weighment_No and TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no= TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no " &
                                    " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_MILK_TRANSFER_IN.Qc_No " &
                                    " left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No =TSPL_MCC_Dispatch_Challan.Tanker_No " &
                                    " left outer join TSPL_QC_Parameter_Detail QcFat on QcFat.QC_No=TSPL_QUALITY_CHECK.QC_No  and QcFat .Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=QcFat.line_no " &
                                    " left outer join TSPL_QC_Parameter_Detail Qcsnf on Qcsnf .QC_No=TSPL_QUALITY_CHECK.QC_No and Qcsnf  .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=Qcsnf.line_no " &
                                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code " &
                                    " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_for_Mcc   on TSPL_LOCATION_MASTER_for_Mcc .Location_Code =TSPL_MCC_Dispatch_Challan.MCC_Code " &
                                    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_MCC_Dispatch_Challan.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + cboUnit.Text + "' " &
                                    " left outer join TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD on TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Chalan_No= TSPL_MCC_Dispatch_Challan.Chalan_NO " &
                                    " left outer join TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL on TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Return_no= TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_no and TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.chamber_no=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no " &
                                    " left outer join TSPL_MCC_DISPATCH_CHALAN_RETURN_PARAMETER_DETAIL DispRetFat on DispRetFat.Return_No=TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_No and DispRetFat.Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispRetFat.sno " &
                                    " left outer join TSPL_MCC_DISPATCH_CHALAN_RETURN_PARAMETER_DETAIL DispRetSNF on DispRetSNF.Return_No=TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_No and DispRetSNF .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispRetSNF.sno" &
                                    "  " & whrcls & ")xx) yy) zz where  zz.Chalan_NO not in(select Challan_No from TSPL_MCC_DISPATCH_CHALLAN_RETURN) )aa " &
                                    "order by convert(date,aa.Dispatch_Date ,103)"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            ReStoreGridLayout()
            Setting()
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

   
    Sub FormatGrid()

        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Dispatch_Date").IsVisible = True
        gv.Columns("Dispatch_Date").Width = 100
        gv.Columns("Dispatch_Date").HeaderText = " Date"
        gv.Columns("Dispatch_Date").FormatString = "{0:d}"

        gv.Columns("MCCCode").IsVisible = False
        gv.Columns("MCCCode").Width = 100
        gv.Columns("MCCCode").HeaderText = "MCC Code"

        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = "MCC NAME"

        gv.Columns("Chalan_NO").IsVisible = True
        gv.Columns("Chalan_NO").Width = 100
        gv.Columns("Chalan_NO").HeaderText = " Challan No."

        gv.Columns("Tanker_No").IsVisible = True
        gv.Columns("Tanker_No").Width = 100
        gv.Columns("Tanker_No").HeaderText = " Tanker No."

        gv.Columns("Dispatch_TO").IsVisible = True
        gv.Columns("Dispatch_TO").Width = 100
        gv.Columns("Dispatch_TO").HeaderText = "Dispatch To"

        gv.Columns("Tanker_Transporter_Code").IsVisible = True
        gv.Columns("Tanker_Transporter_Code").Width = 80
        gv.Columns("Tanker_Transporter_Code").HeaderText = "Transporter Code"

        gv.Columns("Description").IsVisible = True
        gv.Columns("Description").Width = 80
        gv.Columns("Description").HeaderText = "Transporter Name"

        gv.Columns("MCC_Code").IsVisible = False
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = " MCC "

        gv.Columns("DisNewQty").IsVisible = True
        gv.Columns("DisNewQty").Width = 100
        gv.Columns("DisNewQty").HeaderText = " QTY"

        gv.Columns("DisFatKg").IsVisible = True
        gv.Columns("DisFatKg").Width = 100
        gv.Columns("DisFatKg").HeaderText = " FAT Kg"

        gv.Columns("DisSNfKg").IsVisible = True
        gv.Columns("DisSNfKg").Width = 100
        gv.Columns("DisSNfKg").HeaderText = "SNF Kg"

        gv.Columns("DisFatPer").IsVisible = True
        gv.Columns("DisFatPer").Width = 100
        gv.Columns("DisFatPer").HeaderText = "FAT%"

        gv.Columns("DisSNFPer").IsVisible = True
        gv.Columns("DisSNFPer").Width = 100
        gv.Columns("DisSNFPer").HeaderText = "SNF%"

        gv.Columns("Receipt_Challan_Date").IsVisible = True
        gv.Columns("Receipt_Challan_Date").Width = 100
        gv.Columns("Receipt_Challan_Date").HeaderText = "Date"
        gv.Columns("Receipt_Challan_Date").FormatString = "{0:d}"

        gv.Columns("Return_NO").IsVisible = True
        gv.Columns("Return_NO").Width = 100
        gv.Columns("Return_NO").HeaderText = "Return No"

        gv.Columns("Return_Date").IsVisible = True
        gv.Columns("Return_Date").Width = 100
        gv.Columns("Return_Date").HeaderText = "Return Date"

        gv.Columns("ReturnQty").IsVisible = True
        gv.Columns("ReturnQty").Width = 100
        gv.Columns("ReturnQty").HeaderText = "Return Qty"

        gv.Columns("dispRetFatPer").IsVisible = True
        gv.Columns("dispRetFatPer").Width = 100
        gv.Columns("dispRetFatPer").HeaderText = "Return Fat%"

        gv.Columns("dispRetSNFPer").IsVisible = True
        gv.Columns("dispRetSNFPer").Width = 100
        gv.Columns("dispRetSNFPer").HeaderText = "Return SNF%"

        gv.Columns("RetFatKG").IsVisible = True
        gv.Columns("RetFatKG").Width = 100
        gv.Columns("RetFatKG").HeaderText = "Return Fat KG"

        gv.Columns("RetSNFKG").IsVisible = True
        gv.Columns("RetSNFKG").Width = 100
        gv.Columns("RetSNFKG").HeaderText = "Return SNF KG"


        gv.Columns("QcNewQty").IsVisible = True
        gv.Columns("QcNewQty").Width = 100
        gv.Columns("QcNewQty").HeaderText = "QTY"

        gv.Columns("QCFATKG").IsVisible = True
        gv.Columns("QCFATKG").Width = 100
        gv.Columns("QCFATKG").HeaderText = "FAT Kg"

        gv.Columns("QCSNFKG").IsVisible = True
        gv.Columns("QCSNFKG").Width = 100
        gv.Columns("QCSNFKG").HeaderText = "SNF Kg"

        gv.Columns("QCFATPER").IsVisible = True
        gv.Columns("QCFATPER").Width = 100
        gv.Columns("QCFATPER").HeaderText = "FAT%"

        gv.Columns("QCSNFPER").IsVisible = True
        gv.Columns("QCSNFPER").Width = 100
        gv.Columns("QCSNFPER").HeaderText = "SNF%"

        gv.Columns("DIF_QTY").IsVisible = True
        gv.Columns("DIF_QTY").Width = 100
        gv.Columns("DIF_QTY").HeaderText = "QTY"


        gv.Columns("DIF_FAT_KG").IsVisible = True
        gv.Columns("DIF_FAT_KG").Width = 100
        gv.Columns("DIF_FAT_KG").HeaderText = "FAT Kg"

        gv.Columns("DIF_SNF_KG").IsVisible = True
        gv.Columns("DIF_SNF_KG").Width = 100
        gv.Columns("DIF_SNF_KG").HeaderText = "SNF Kg"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("DisNewQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("DisFatKg", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("DisSNfKg", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("QcNewQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        'Dim item5 As New GridViewSummaryItem("QCFATKG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("QCSNFKG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("DIF_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("ReturnQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        'Dim item8 As New GridViewSummaryItem("DIF_FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)
        'Dim item9 As New GridViewSummaryItem("DIF_SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_CODE as Item format ""{0}: {1}"" Group By MCC_CODE"))



       
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Setting()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("TANKER DISPATCH"))

            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_NAME").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Chalan_NO").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_TO").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_Transporter_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Description").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DisNewQty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DisFatKg").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DisSNfKg").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DisFatPer").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DisSNFPer").Name)
            'gv.ViewDefinition = view

            view.ColumnGroups.Add(New GridViewColumnGroup("Return Dispatch"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Return_NO").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Return_Date").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("ReturnQty").Name)

            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("RetFatKG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("RetSNFKG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("dispRetFatPer").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("dispRetSNFPer").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("PLANT RECEIPT"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Receipt_Challan_Date").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("QcNewQty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("QCFATKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("QCSNFKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("QCFATPER").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("QCSNFPER").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("DIFFERENCE"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("DIF_QTY").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("DIF_FAT_KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("DIF_SNF_KG").Name)

            gv.ViewDefinition = view
        End If
    End Sub
    Sub Reset()
        'LOCATIONRIGTHS()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        cboUnit.Text = "ltr"
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
