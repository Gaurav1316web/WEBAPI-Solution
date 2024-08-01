Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class RmSecurityDeduction

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select distinct TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_name as Name from TSPL_VENDOR_MASTER 
									 left join TSPL_PI_HEAD on TSPL_PI_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code "
        'WHERE  Status='N'  "
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " where Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            qry += " group by TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_name "
        End If

        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Code", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'qry += " where 2=2 and Seg_No = '7' AND GIT='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click_1(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder1._My_Click
        Dim qry As String = "  select distinct tspl_tender_header.DocumentCode as Ral from tspl_tender_header  
										  left join TSPL_TENDER_DETAIL on TSPL_TENDER_DETAIL.DocumentCode=tspl_tender_header.DocumentCode "
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  where Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
        End If

        TxtMultiSelectFinder1.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Ral", "Ral", TxtMultiSelectFinder1.arrValueMember, TxtMultiSelectFinder1.arrDispalyMember)
    End Sub


    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        TxtMultiSelectFinder1.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
            End If


            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember) + " "))
            End If

            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                arrHeader.Add(("Vendor : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectFinder1.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Payment Details Report", gv1, arrHeader, "Vendor Payment Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
    '    SetUserMgmtNew()
    '    txtToDate.Value = clsCommon.GETSERVERDATE()
    '    txtFromDate.Value = clsCommon.GETSERVERDATE()
    'End Sub

    Private Sub RmSecurityDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1

            Dim strqry As String = " select Location,TSPL_PI_HEAD.Ref_No as Tendor,TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.PI_Date,Vendor_Code,Vendor_name,TSPL_PI_DETAIL.Item_Code,Item_Desc as Item_name,SUM(TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt) AS SECURITY_AMT from TSPL_PI_HEAD
                                     left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No
                                     left outer join TSPL_SRN_TENDER_CALC on TSPL_SRN_TENDER_CALC.SRN_No=TSPL_PI_DETAIL.SRN_Id
                                     left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_PI_DETAIL.SRN_Id
                                     where 2=2 "
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strqry += " and TSPL_PI_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " and TSPL_PI_DETAIL.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                strqry += " and TSPL_PI_HEAD.Ref_No in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
            End If
            strqry += " and CONVERT(DATE,TSPL_PI_HEAD.PI_Date,103)>= convert(date,('" + txtFromDate.Value + "'),103) AND CONVERT(DATE,TSPL_PI_HEAD.PI_Date,103)<= convert(date,('" + txtToDate.Value + "'),103)
                                     GROUP BY Location,TSPL_PI_HEAD.Ref_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_HEAD.PI_No,Vendor_Code,Vendor_name,TSPL_PI_DETAIL.Item_Code,Item_Desc
                                     ORDER BY Location,TSPL_PI_HEAD.Ref_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_HEAD.PI_No,Vendor_Code,TSPL_PI_DETAIL.Item_Code "



            'strqry += " )zzz ) " & Environment.NewLine & "
            '       Select ROW_NUMBER() Over (Order By (Select 1)) As SNo,CTETemp.* " & Environment.NewLine & "
            '       from CTETemp " & Environment.NewLine & "
            '       where 1=1  " & Environment.NewLine & "
            '       group by 
            '       CTETemp.RAL_NO,CTETemp.[Name Of Supplier],CTETemp.Supplied_Material,CTETemp.Weighment_Code,CTETemp.Weighment_Date,CTETemp.Net_Weight,CTETemp.Bill_No,CTETemp.Bill_date,CTETemp.Amount,CTETemp.Due_Date,CTETemp.Release_date,CTETemp.[Delay day]  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                'gv1.Columns("RowNo").IsVisible = False
                'gv1.Columns("Vendor_Code").IsVisible = False

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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



    'Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
    '    If clsCommon.myLen(PageSetupReport_ID) > 0 Then
    '        gv1.MasterTemplate.FilterDescriptors.Clear()
    '        Dim obj As New clsGridLayout()
    '        obj.ReportID = PageSetupReport_ID
    '        obj.UserID = objCommonVar.CurrentUserCode
    '        obj.GridLayout = New MemoryStream()
    '        gv1.SaveLayout(obj.GridLayout)
    '        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '        obj.GridColumns = gv1.ColumnCount
    '        If obj.SaveData() Then
    '            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '        End If

    '        obj.GridLayout.Close()
    '        obj.GridLayout.Dispose()
    '    End If
    'End Sub

    'Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
    '    clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    '    common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    'End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim qry As String = " "

        Try
            qry = " select  convert(date,('" + txtFromDate.Value + "'),103) as from_date, convert(date,('" + txtToDate.Value + "'),103) as To_date,Location,Location_Desc,Add1,Add2,Add3,TSPL_PI_HEAD.Ref_No,TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_HEAD.Vendor_Code,Vendor_Name,TSPL_PI_DETAIL.Item_Code,Item_Desc,SUM(TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt) AS SECURITY_AMT from TSPL_PI_HEAD
                                     left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No
                                     left outer join TSPL_SRN_TENDER_CALC on TSPL_SRN_TENDER_CALC.SRN_No=TSPL_PI_DETAIL.SRN_Id
                                     left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_PI_DETAIL.SRN_Id
                                     left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PI_HEAD.Bill_To_Location
                                     where 2=2 "
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and TSPL_PI_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_PI_DETAIL.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                qry += " and TSPL_PI_HEAD.Ref_No in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ")"
            End If
            qry += " and CONVERT(DATE,TSPL_PI_HEAD.PI_Date,103)>= convert(date,('" + txtFromDate.Value + "'),103) AND CONVERT(DATE,TSPL_PI_HEAD.PI_Date,103)<= convert(date,('" + txtToDate.Value + "'),103)
                                     GROUP BY Location,TSPL_PI_HEAD.Ref_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.Vendor_Code,Vendor_Name,TSPL_PI_DETAIL.Item_Code,Item_Desc,Location_Desc,Add1,Add2,Add3
                                     ORDER BY Location,TSPL_PI_HEAD.Ref_No,TSPL_PI_HEAD.PI_Date,TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.Vendor_Code,TSPL_PI_DETAIL.Item_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "RmSecurityDeduction", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
