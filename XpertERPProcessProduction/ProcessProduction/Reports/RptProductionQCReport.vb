'--------------created by - sanjay, Ticket No-BHA/05/09/18-000510 Client - Bharat-------------
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class RptProductionQCReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim dt As DataTable
    Const colSno As String = "colSno"
    Const colStructureCode As String = "colStructureCode"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colWeightUOM As String = "colWeightUOM"
    Const colWeightValue As String = "colWeightValue"
    Const colStockingUOM As String = "colStockingUOM"
    Const colStockingConversion As String = "colStockingConversion"
    Const colDefaultUOM As String = "colDefaultUOM"
    Const colDefaultConversion As String = "colDefaultConversion"
    Const colWeightUOM1 As String = "colWeightUOM1"
    Const colWeightConversion1 As String = "colWeightConversion1"
    Const colWeightUOM2 As String = "colWeightUOM2"
    Const colWeightConversion2 As String = "colWeightConversion2"
    Const colOtherUOM1 As String = "colOtherUOM1"
    Const colOtherConversion1 As String = "colOtherConversion1"
    Const colOtherUOM2 As String = "colOtherUOM2"
    Const colOtherConversion2 As String = "colOtherConversion2"
    Const colItemType As String = "colItemType"
    Const colPurchaseAccountSet As String = "colPurchaseAccountSet"
    Const colSaleAccountSet As String = "colSaleAccountSet"
    Const colBatchWise As String = "colBatchWise"
    Const colFresh_Ambient As String = "colFresh_Ambient"
    Const colTaxable As String = "colTaxable"
    Const colMRPWise As String = "colMRPWise"
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub RptProductionQCReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub RptProductionQCReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        LoadStatus()
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for view report")
    End Sub

    Sub LoadStatus()
        ddlQCStatus.DataSource = GetStatus()
        ddlQCStatus.ValueMember = "Code"
        ddlQCStatus.DisplayMember = "Name"
    End Sub

    Public Shared Function GetStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Accept"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Reject"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Reprocessed"
        dt.Rows.Add(dr)
        Return dt
    End Function


    Private Sub FunReset()
        'gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        txtItemCode.arrValueMember = Nothing
        txtBatchNoMult.arrValueMember = Nothing
        txtCategory.arrValueMember = Nothing
        ddlQCStatus.SelectedValue = "All"
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Print()
    End Sub

    Private Sub Print()
        Try

            Dim qry As String
            Dim TempQry As String
            Dim ParameterDesc As String
            Dim strWhrClauseSFG As String = String.Empty
            Dim strWhrClauseFG As String = String.Empty
            Dim FATParameterDesc As String = ""
            Dim SNFParameterDesc As String = ""
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
                fromDate.Focus()
                Exit Sub
            End If

            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                strWhrClauseSFG += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClauseFG += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If

            If txtCategory.arrValueMember IsNot Nothing AndAlso txtCategory.arrValueMember.Count > 0 Then
                strWhrClauseSFG += " and tspl_item_master.Structure_Code in (" + clsCommon.GetMulcallString(txtCategory.arrValueMember) + ")  "
                strWhrClauseFG += " and tspl_item_master.Structure_Code in (" + clsCommon.GetMulcallString(txtCategory.arrValueMember) + ")  "
            End If

            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                strWhrClauseSFG += " and TSPL_PP_BATCH_ORDER_HEAD.Batch_code in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
                strWhrClauseFG += " and TSPL_PP_BATCH_ORDER_HEAD.Batch_code in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
            End If

            If Not clsCommon.CompairString(ddlQCStatus.SelectedValue, "All") = CompairStringResult.Equal Then
                strWhrClauseSFG += " and TSPL_PP_STD_FINALQC_HEAD.status = '" + ddlQCStatus.SelectedValue + "' "
                strWhrClauseFG += " and TSPL_PE_FINALQC_HEAD.status = '" + ddlQCStatus.SelectedValue + "' "
            End If

            strWhrClauseSFG += " and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            strWhrClauseFG += " and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "

            'BHA/11/09/18-000536, QC Parameter order as per Final QC Screen- Type=NA -1  ,Type=FAT -2,Type=SNF  - 3 ,Type= CLR - 4  Type=OTHERS - 5 
            'TempQry = "SELECT STUFF((SELECT distinct ',' + QUOTENAME(tspl_parameter_master.Description) as Description FROM tspl_parameter_master where IsProduction=1 FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
            TempQry = "SELECT STUFF((SELECT ',' + QUOTENAME(tspl_parameter_master.Description) as Description FROM (select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  where 1=1 and IsProduction=1)tspl_parameter_master order by Ordering FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
            ParameterDesc = clsDBFuncationality.getSingleValue(TempQry)
            'Ticket  ERO/17/07/19-000953 Add TS% and TSKG
            FATParameterDesc = clsDBFuncationality.getSingleValue("select ISNULL(tspl_parameter_master.description,'')  from  tspl_parameter_master where tspl_parameter_master.type='FAT'")
            SNFParameterDesc = clsDBFuncationality.getSingleValue("select ISNULL(tspl_parameter_master.description,'')  from  tspl_parameter_master where tspl_parameter_master.type='SNF'")
            qry = "select ROW_NUMBER() OVER (ORDER BY seq,item_code) AS [Sno], " & _
                    "Batch_Code as [Batch Order No],Prod_Entry_QC_No as [Production Entry QC No],Standardization_No as [Standardization No],Standardization_QC_No as [Standardization QC No],ManualBatchNo as [Batch No.],Item_Code as [Item Code],Item_Desc as [Item Name],FATPer as [Fat %(Std. Range)],SNFPer as [SNF%(Std. Range)] " & _
                    "," & ParameterDesc & ""
            If clsCommon.myLen(FATParameterDesc) > 0 AndAlso clsCommon.myLen(SNFParameterDesc) > 0 Then
                qry += " ," + "convert(decimal(18,2),[" + FATParameterDesc + "]) + convert(decimal(18,2),[" + SNFParameterDesc + "]) as [TS %]"
            End If
            qry += ",(Case when status='A' then 'Accept' when status='R' then 'Reject' when status='P' then 'Reprocessed' else '' end) as [QC Final Status] " & _
                    "from (select 0 as seq,TSPL_PP_STD_FINALQC_HEAD.Main_Batch_Code AS Batch_Code " & _
                    ",'' AS Prod_Entry_QC_No,TSPL_PP_STD_FINALQC_HEAD.ManualBatchNo,TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code as Standardization_No " & _
                    ",TSPL_PP_STD_FINALQC_HEAD.QC_Code as Standardization_QC_No ,TSPL_ITEM_MASTER.Item_Code " & _
                    ",TSPL_ITEM_MASTER.Item_Desc,FATPer.Actual_Range as FATPer, SNFPer.Actual_Range as SNFPer ,TSPL_PP_STD_FINALQC_HEAD.Status" & _
                    ",TSPL_PP_STD_FINALQC_QC_PARAMETER.param_field_desc,TSPL_PP_STD_FINALQC_QC_PARAMETER.Param_Field_Value  " & _
                    "from TSPL_PP_STD_FINALQC_DETAIL   " & _
                    "LEFT JOIN  TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code  " & _
                    "LEFT JOIN TSPL_PP_BATCH_ORDER_HEAD ON TSPL_PP_BATCH_ORDER_HEAD.Batch_code=TSPL_PP_STD_FINALQC_HEAD.Main_batch_code " & _
                    "LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_DETAIL.Item_Code " & _
                    "left join (select tspl_parameter_master.type,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,Actual_Range,tspl_parameter_master.description  " & _
                    "from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                    "left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code where tspl_parameter_master.type='FAT' " & _
                    ") as FATPer on FATPer.item_code=TSPL_ITEM_MASTER.Item_Code  " & _
                    "left join (select tspl_parameter_master.type,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,Actual_Range,tspl_parameter_master.description " & _
                    "from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                    "left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code where tspl_parameter_master.type='SNF' " & _
                    ") as SNFPer on SNFPer.item_code=TSPL_ITEM_MASTER.Item_Code  " & _
                    "LEFT JOIN TSPL_PP_STD_FINALQC_QC_PARAMETER ON TSPL_PP_STD_FINALQC_QC_PARAMETER.QC_CODE=TSPL_PP_STD_FINALQC_HEAD.QC_Code " & _
                    "and (TSPL_PP_STD_FINALQC_QC_PARAMETER.line_no+1) =TSPL_PP_STD_FINALQC_DETAIL.line_no " & _
                    "WHERE 1=1 " & strWhrClauseSFG & " " & _
                    "union all " & _
                    "select 1 as seq,TSPL_PE_FINALQC_HEAD.Batch_Code,TSPL_PE_FINALQC_HEAD.QC_CODE AS Prod_Entry_QC_No " & _
                    ",TSPL_PE_FINALQC_HEAD.ManualBatchNo,'' as Standardization_No,'' as Standardization_QC_No ,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc " & _
                    ",FATPer.Actual_Range as FATPer, SNFPer.Actual_Range as SNFPer,TSPL_PE_FINALQC_HEAD.status,TSPL_PE_FINALQC_QC_PARAMETER.param_field_desc,TSPL_PE_FINALQC_QC_PARAMETER.Param_Field_Value  " & _
                    "from TSPL_PE_FINALQC_DETAIL   " & _
                    "LEFT JOIN  TSPL_PE_FINALQC_HEAD on TSPL_PE_FINALQC_DETAIL.QC_Code=TSPL_PE_FINALQC_HEAD.QC_Code   " & _
                    "LEFT JOIN TSPL_PP_BATCH_ORDER_HEAD ON TSPL_PP_BATCH_ORDER_HEAD.Batch_code=TSPL_PE_FINALQC_HEAD.batch_code " & _
                    "LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_PE_FINALQC_DETAIL.Item_Code   " & _
                    "left join (select tspl_parameter_master.type,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,Actual_Range,tspl_parameter_master.description  " & _
                    "from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                    "left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code where tspl_parameter_master.type='FAT' " & _
                    ") as FATPer on FATPer.item_code=TSPL_ITEM_MASTER.Item_Code  " & _
                    "left join (select tspl_parameter_master.type,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,Actual_Range,tspl_parameter_master.description " & _
                    "from TSPL_ITEM_QC_PARAMETER_MASTER " & _
                    "left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code where tspl_parameter_master.type='SNF' " & _
                    ") as SNFPer on SNFPer.item_code=TSPL_ITEM_MASTER.Item_Code " & _
                    "LEFT JOIN TSPL_PE_FINALQC_QC_PARAMETER ON TSPL_PE_FINALQC_QC_PARAMETER.QC_CODE=TSPL_PE_FINALQC_HEAD.QC_Code " & _
                    "WHERE 1=1 " & strWhrClauseFG & " " & _
                    ") as final pivot " & _
                    "(max(final.Param_Field_Value) for Param_Field_Desc in (" & _
                    "" & ParameterDesc & "" & _
                    ")) as tt " & _
                    "order by seq,item_code "




            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                gv.DataSource = dt
                FormatGrid()

                ReStoreGridLayout()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No Data Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub FormatGrid()

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False

        gv.Columns("SNo").ReadOnly = True
        gv.Columns("SNo").IsVisible = True
        gv.Columns("SNo").Width = 50
        gv.Columns("SNo").WrapText = True

        gv.Columns("Batch Order No").ReadOnly = True
        gv.Columns("Batch Order No").IsVisible = True
        gv.Columns("Batch Order No").Width = 150
        gv.Columns("Batch Order No").WrapText = True

        gv.Columns("Production Entry QC No").ReadOnly = True
        gv.Columns("Production Entry QC No").IsVisible = True
        gv.Columns("Production Entry QC No").Width = 150
        gv.Columns("Production Entry QC No").WrapText = True

        gv.Columns("Standardization No").ReadOnly = True
        gv.Columns("Standardization No").IsVisible = True
        gv.Columns("Standardization No").Width = 150
        gv.Columns("Standardization No").WrapText = True

        gv.Columns("Standardization QC No").ReadOnly = True
        gv.Columns("Standardization QC No").IsVisible = True
        gv.Columns("Standardization QC No").Width = 150
        gv.Columns("Standardization QC No").WrapText = True

        gv.Columns("Batch No.").ReadOnly = True
        gv.Columns("Batch No.").IsVisible = True
        gv.Columns("Batch No.").Width = 100
        gv.Columns("Batch No.").WrapText = True

        gv.Columns("Item Code").ReadOnly = True
        gv.Columns("Item Code").IsVisible = True
        gv.Columns("Item Code").Width = 100
        gv.Columns("Item Code").WrapText = True

        gv.Columns("Item Name").ReadOnly = True
        gv.Columns("Item Name").IsVisible = True
        gv.Columns("Item Name").Width = 250
        gv.Columns("Item Name").WrapText = True

        gv.Columns("Fat %(Std. Range)").ReadOnly = True
        gv.Columns("Fat %(Std. Range)").IsVisible = True
        gv.Columns("Fat %(Std. Range)").Width = 120
        gv.Columns("Fat %(Std. Range)").WrapText = True

        gv.Columns("SNF%(Std. Range)").ReadOnly = True
        gv.Columns("SNF%(Std. Range)").IsVisible = True
        gv.Columns("SNF%(Std. Range)").Width = 120
        gv.Columns("SNF%(Std. Range)").WrapText = True

        For ii As Integer = 10 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).WrapText = True
            gv.Columns(ii).Width = 100
        Next
        

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        'Print()

        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Production QC Report")
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
            End If
            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtCategory.arrValueMember IsNot Nothing AndAlso txtCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(" Category : " + clsCommon.GetMulcallStringWithComma(txtCategory.arrDispalyMember))
            End If
            If Not clsCommon.CompairString(ddlQCStatus.SelectedValue, "All") = CompairStringResult.Equal Then
                arrHeader.Add(" Status : " + clsCommon.myCstr(ddlQCStatus.SelectedText))
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Production QC Report", gv, arrHeader, "Production QC Report")
        End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Production QC Report")
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
            End If
            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtCategory.arrValueMember IsNot Nothing AndAlso txtCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(" Category : " + clsCommon.GetMulcallStringWithComma(txtCategory.arrDispalyMember))
            End If
            If Not clsCommon.CompairString(ddlQCStatus.SelectedValue, "All") = CompairStringResult.Equal Then
                arrHeader.Add(" Status : " + clsCommon.myCstr(ddlQCStatus.SelectedText))
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Production QC Report", gv, arrHeader, "Production QC Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

 

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click

        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

   
    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("txtItemCode", qry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub


    Private Sub txtCategory__My_Click(sender As Object, e As EventArgs) Handles txtCategory._My_Click
        Dim qry As String = "select Structure_Code as Code,Structure_Descq as Name  from TSPL_STRUCTURE_MASTER"
        txtCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("txtCategory", qry, "Code", "Name", txtCategory.arrValueMember, txtCategory.arrDispalyMember)
    End Sub

    Private Sub txtBatchNoMult__My_Click(sender As Object, e As EventArgs) Handles txtBatchNoMult._My_Click
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Name  from TSPL_PP_BATCH_ORDER_HEAD "
        txtBatchNoMult.arrValueMember = clsCommon.ShowMultipleSelectForm("multBatchNo", qry, "Code", "Name", txtBatchNoMult.arrValueMember, txtBatchNoMult.arrDispalyMember)
    End Sub
End Class
