Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptPurchaseMaterialRegister
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub RptPurchaseMaterialRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtItem.arrValueMember = Nothing
        txtSupplier.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData(False)
    End Sub

    Sub LoadData(ByVal isPrint As Boolean)
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qry As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim whr As String = ""

            fromdate = clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy")
            If txtSupplier.arrValueMember IsNot Nothing AndAlso txtSupplier.arrValueMember.Count > 0 Then
                whr = " and TSPL_GRN_HEAD.Vendor_Code in ( " & clsCommon.GetMulcallString(txtSupplier.arrValueMember) & ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr = whr + " and TSPL_GRN_DETAIL.Item_Code in ( " & clsCommon.GetMulcallString(txtItem.arrValueMember) & ")"
            End If
            If objCommonVar.ApplyLocationFilterBasedOnPermission AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whr += " and TSPL_GRN_HEAD.Bill_To_Location in (" & objCommonVar.strCurrUserLocations & ")"
            End If

            qry = " select TSPL_GRN_HEAD.GRN_No,convert (varchar, FORMAT (TSPL_GRN_HEAD.GRN_Date,'dd/MM/yyyy HH:mm:ss' )) as GRN_Date,TSPL_GRN_HEAD.Vendor_Code,TSPL_GRN_HEAD.Vendor_Name,TSPL_GRN_HEAD.VehicleNo,TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.Item_Desc,TSPL_GRN_DETAIL.PO_Id, TSPL_PO_WEIGHTMENT_HEAD_Temp.Weighment_Code, convert (varchar, FORMAT (TSPL_PO_WEIGHTMENT_HEAD_Temp.Weighment_Date,'dd/MM/yyyy HH:mm:ss' )) as Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,CinBag.Conversion_Factor As CFInBag from TSPL_GRN_DETAIL " &
                   " left outer join TSPL_GRN_HEAD on TSPL_GRN_DETAIL.GRN_No = TSPL_GRN_HEAD.GRN_No " &
                   " left outer join (select * from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Status = 1 ) TSPL_PO_WEIGHTMENT_HEAD_Temp on TSPL_PO_WEIGHTMENT_HEAD_Temp.Against_GRN_No = TSPL_GRN_HEAD.GRN_No " &
                   " left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code =TSPL_PO_WEIGHTMENT_HEAD_Temp.Weighment_Code " &
            " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code " &
            " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_GRN_DETAIL.Unit_code " &
            " Left Outer Join TSPL_ITEM_UOM_DETAIL As CinBag On CinBag.Item_Code=TSPL_ITEM_MASTER.Item_Code And CinBag.UOM_Code='Bag'" &
            " where TSPL_GRN_HEAD.Status = 1  and  Convert(date,TSPL_GRN_HEAD.GRN_Date,103) >= convert (date, '" & fromdate & "',103) and Convert(date,TSPL_GRN_HEAD.GRN_Date,103) <= convert (date, '" & Todate & "',103) " & whr & " "

            Dim finalQry As String = " Select xyz.GRN_No As [GRN No],xyz.GRN_Date As [GRN Date],xyz.Vendor_Code As [Vendor Code],xyz.Vendor_Name As [Vendor Name],xyz.VehicleNo As [Vehicle No],xyz.Item_Code As [Item Code],xyz.Item_Desc As [Item Desc],xyz.PO_Id As [PO ID],xyz.Weighment_Code As [Weighment Code],xyz.Weighment_Date As [Weighment Date],xyz.Gross_Weight As [Gross Weight],xyz.Tare_Weight As [Tare Weight],xyz.Net_Weight As [Net Weight],Convert(Decimal(18,0),((Net_Weight*Conversion_Factor)/CFInBag)) As [No Of Bag],Convert(Decimal(18,2),Case When (Net_Weight*Conversion_Factor)>0 And CFInBag>0 Then  Net_Weight/((Net_Weight*Conversion_Factor)/CFInBag) Else 0 End) As [Avg Weight Per Bag]"
            If isPrint Then
                finalQry += " ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME"
            End If
            finalQry += " from(" & qry & ")xyz"
            If isPrint Then
                finalQry += " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
 Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State"
            End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(finalQry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                If isPrint Then
                    Dim frm As New frmCrystalReportViewer()
                    frm.funreport(Form_ID, CrystalReportFolder.PurchaseOrder, dtgv, "crptPurchaseMaterialWeighmentReport", "Purchase Material Weighment Report")
                    frm = Nothing
                Else
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dtgv
                    gv.BestFitColumns()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptPurchaseMaterialRegister & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) & " ")


            If exporter = EnumExportTo.Excel Then
          
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                '    End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                '    Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Purchase Material Register Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub txtSupplier__My_Click(sender As Object, e As EventArgs) Handles txtSupplier._My_Click
        Dim qry As String = " select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER "
        txtSupplier.arrValueMember = clsCommon.ShowMultipleSelectForm("SUPPLIER", qry, "Code", "Name", txtSupplier.arrValueMember, txtSupplier.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code as Code, Item_Desc as Name from TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ITEM", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If obj IsNot Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
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

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class
