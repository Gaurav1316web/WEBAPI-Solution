'======Sanjeet(09/01/2017)========================
Imports common
Imports System.Data.SqlClient
Public Class RptAClassMilkRate
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptAClassMilkRate)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub
    Private Sub RptAClassMilkRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub Reset()
        rbtnAclassMilkRate.Checked = True
        RadGroupBox1.Enabled = True
        RadGroupBox2.Enabled = False
        txtPriceCode.arrValueMember = Nothing
        txtVendorCode.arrValueMember = Nothing
        txtMilkTypeCode.arrValueMember = Nothing
        fndPriceCode.Value = ""
        fndIncentiveCode.Value = ""
        txtDock.Text = ""

    End Sub
    Private Sub txtPriceCode__My_Click(sender As Object, e As EventArgs) Handles txtPriceCode._My_Click
        Try
            Dim Query As String = Nothing
            Query = "select TSPL_Bulk_Price_MASTER.Price_Code as Code ,TSPL_Bulk_Price_MASTER.Price_Code as Name  From TSPL_Bulk_Price_MASTER "
            txtPriceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Price_Code", Query, "Code", "Name", txtPriceCode.arrValueMember, txtPriceCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtVendorCode__My_Click(sender As Object, e As EventArgs) Handles txtVendorCode._My_Click
        Try
            Dim Query As String = Nothing
            Query = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER Where Status='N' "
            txtVendorCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor_Code", Query, "Code", "Name", txtVendorCode.arrValueMember, txtVendorCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMilkTypeCode__My_Click(sender As Object, e As EventArgs) Handles txtMilkTypeCode._My_Click
        Try
            Dim Query As String = Nothing
            Query = "select TSPL_MILK_TYPE_MASTER.MILK_TYPE_CODE as Code ,TSPL_MILK_TYPE_MASTER.DESCRIPTION as Name ,TSPL_MILK_TYPE_MASTER.MILK_TYPE as [MILK TYPE] ,TSPL_MILK_TYPE_MASTER.Created_By as [Created By] ,TSPL_MILK_TYPE_MASTER.Created_Date as [Created Date] ,TSPL_MILK_TYPE_MASTER.Modified_By as [Modified By] ,TSPL_MILK_TYPE_MASTER.Modified_Date as [Modified Date]  From TSPL_MILK_TYPE_MASTER "
            txtMilkTypeCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Milk_Type_Code", Query, "Code", "Name", txtMilkTypeCode.arrValueMember, txtMilkTypeCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub AClassPrint()
        Dim strQuery As String = Nothing
        strQuery = " select Main_Query.* from ( select dense_rank() OVER  (partition by tspl_bulk_price_detail.Price_Code  ORDER BY TSPL_VENDOR_MASTER.vendor_Code) as SMNO," & _
        "TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, tspl_bulk_price_detail.Price_Code, tspl_bulk_price_detail.Line_No, TSPL_MILK_GRADE_MASTER.GRADE_TYPE as Milk_Grade_code, tspl_bulk_price_detail.Fat_Weightage, tspl_bulk_price_detail.Snf_Weightage " & _
        ",tspl_bulk_price_detail.Fat_Percentage, tspl_bulk_price_detail.Snf_Percentage,tspl_bulk_price_detail.Standard_Rate*100 as Standard_Rate,tspl_bulk_price_detail.Tolerance, " & _
        "'Milk Rates for A-Class milk contractors (at ' + cast(tspl_bulk_price_detail.Fat_Percentage as varchar) + ' % / '+ cast(tspl_bulk_price_detail.Snf_Percentage as varchar) +' % Fat & SNF)' as Milk_Rate,TSPL_Bulk_Price_MASTER.effective_Date,TSPL_Bulk_Price_MASTER.EffectiveTime,TSPL_Bulk_Price_MASTER.Price_Date,convert(varchar(13),TSPL_Bulk_Price_MASTER.Price_Date,103) as Fomated_PriceDate,TSPL_Bulk_Price_MASTER.Milk_Type_Code, " & _
        "TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.Tan_No,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.Access_Officer,TSPL_COMPANY_MASTER.Tcan_No ,'1' as CopyType " & _
        "from tspl_bulk_price_detail " & _
        "LEFT OUTER JOIN TSPL_Bulk_Price_MASTER ON TSPL_Bulk_Price_MASTER.Price_Code=tspl_bulk_price_detail.Price_Code " & _
        "LEFT OUTER JOIN TSPL_VENDOR_PRICE_CHART_MAPPING ON TSPL_VENDOR_PRICE_CHART_MAPPING.PriceCode=tspl_bulk_price_detail.Price_Code and " & _
        " tspl_bulk_price_detail.Milk_Grade_code=Tspl_Vendor_Price_Chart_mapping.Milk_Grade_Code " & _
        "left outer join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_PRICE_CHART_MAPPING.VendorCode " & _
        "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_MASTER.Comp_Code left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=tspl_bulk_price_detail.Milk_Grade_code where  1=1 "
        If txtPriceCode.arrValueMember IsNot Nothing Then
            If txtPriceCode.arrValueMember.Count > 0 Then
                strQuery += " AND  TSPL_VENDOR_PRICE_CHART_MAPPING.PriceCode in( " + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + ")"
            End If
        End If
        If txtVendorCode.arrValueMember IsNot Nothing Then
            If txtVendorCode.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_VENDOR_PRICE_CHART_MAPPING.VendorCode in(" + clsCommon.GetMulcallString(txtVendorCode.arrValueMember) + ")"
            End If
        End If
        If txtMilkTypeCode.arrValueMember IsNot Nothing Then
            If txtMilkTypeCode.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_Bulk_Price_MASTER.Milk_Type_Code in (" + clsCommon.GetMulcallString(txtMilkTypeCode.arrValueMember) + ")"
            End If
        End If
        'strQuery += " and convert(varchar(15),TSPL_Bulk_Price_MASTER.Price_Date,103) >=Convert(varchar(15), '" + StartDate + "', 103)  and convert(varchar(15),TSPL_Bulk_Price_MASTER.Price_Date,103) <=Convert(varchar(15), '" + EndDate + "', 103) "
        strQuery += " ) Main_Query order by Main_Query.Vendor_Name "

        'strQuery += "LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'COPY:  Head-Finance & Accounts (HO)' as CopyType1 UNION Select '1' as COL1, 2 as COL2, " & _
        '" 'COPY:  ACCOUNTS  (PLANT)' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'COPY:  MILK PROCUREMENT DEPARTMENT' as CopyType1) Copy_Table ON Copy_Table.COL1=Main_Query.CopyType  " & _
        '"order by Main_Query.Vendor_Name"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptAClass_MilkRate", "A Class Milk Rates")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("No Data to Print ..")
        End If

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If rbtnAclassMilkRate.Checked = True Then
                AClassPrint()
            Else
                MccMilkRatePrint()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub MccMilkRatePrint()
        If clsCommon.myLen(fndPriceCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Price Code.")
        ElseIf clsCommon.myLen(fndIncentiveCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Incentive Code.")
        ElseIf clsCommon.myLen(txtDock.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter Dock.")
        Else
            Dim strPriceQuery As String = Nothing
            Dim dtPrice As DataTable
            Dim strIncentiveQry As String = Nothing
            Dim dtIncentive As DataTable

            strPriceQuery = "select tspl_milk_price_master.Price_Code,tspl_milk_price_master.Description,CONVERT(VARCHAR(13),tspl_milk_price_master.Effective_Date,103) AS Effective_Date," & _
                " tspl_milk_price_master.Effective_Date as EffectiveDate,tspl_milk_price_master.FAT_Pers,tspl_milk_price_master.SNF_Pers,tspl_milk_price_master.Ratio AS Fat_Ratio," & _
                " tspl_milk_price_master.SNF_Ratio,tspl_milk_price_master.Milk_Rate,tspl_milk_price_master.Milk_Rate*100 as MilkRate, TSPL_COMPANY_MASTER.Comp_Name,'" + txtDock.Text + "' as Dock ,'--' as Variation " & _
                " from tspl_milk_price_master LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=tspl_milk_price_master.comp_code where Price_Code= '" + fndPriceCode.Value + "' "
            dtPrice = clsDBFuncationality.GetDataTable(strPriceQuery)

            strIncentiveQry = " SELECT   INCENTIVE_CODE,INCENTIVE_TYPE,LINE_NO,SLAB_FROM,SLAB_TO,RATE ,RATE_UOM FROM TSPL_INCENTIVE_DETAIL where  INCENTIVE_CODE = '" + fndIncentiveCode.Value + "'"
            dtIncentive = clsDBFuncationality.GetDataTable(strIncentiveQry)
            Dim dt As New DataTable()
            dt.Columns.Add("Rate", GetType(String))
            dt.Columns.Add("Rate_Des", GetType(String))

            For Each dr As DataRow In dtIncentive.Rows
                Dim drow As DataRow = dt.NewRow()
                drow("Rate") = "RS. " + clsCommon.myCstr(dr("RATE")) + " per " + clsCommon.myCstr(dr("RATE_UOM"))
                drow("Rate_Des") = "Extra on total milk for more than " + clsCommon.myCstr(dr("SLAB_FROM")) + " " + clsCommon.myCstr(dr("RATE_UOM")) + " standard milk quantity per day."
                dt.Rows.Add(drow)
            Next
            If dtPrice.Rows.Count > 0 AndAlso dt.Rows.Count > 0 Then
                ' frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptAClass_MilkRate", "A Class Milk Rates")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtPrice, dt, "rptMccMilkRate", "MCC Milk Rate", "RptIncentiveDetailForMccRate.rpt")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data to Print ..")
            End If
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rbtnAclassMilkRate_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAclassMilkRate.CheckedChanged
        If rbtnAclassMilkRate.Checked = True Then
            RadGroupBox1.Enabled = True
            RadGroupBox2.Enabled = False
        Else
            RadGroupBox1.Enabled = False
            RadGroupBox2.Enabled = True
        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub fndPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPriceCode._MYValidating
        Try
            Dim qry As String = "select Price_Code as Code,Description from tspl_milk_price_master "
            fndPriceCode.Value = clsCommon.ShowSelectForm("Group", qry, "Code", "", fndPriceCode.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
      
    End Sub

    Private Sub fndIncentiveCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndIncentiveCode._MYValidating
        Try
            Dim qry As String = "select INCENTIVE_CODE as Code,DESCRIPTION from TSPL_INCENTIVE_MASTER_HEAD "
            fndIncentiveCode.Value = clsCommon.ShowSelectForm("Group", qry, "Code", "", fndIncentiveCode.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
