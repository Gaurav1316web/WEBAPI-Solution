Imports common
Imports System.Data.SqlClient
Public Class clsReconciliationSetting
    Public Report_Name As String = Nothing
    Public Report_Component As String = Nothing
    Public Account_Code As String = Nothing

    Public Shared Function SaveData(ByVal Arr As List(Of clsReconciliationSetting), ByVal strReportName As String, ByVal strReportComponent As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_RECONCILIATION_SETTING where Report_Name='" + strReportName + "' and Report_Component='" + strReportComponent + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsReconciliationSetting In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Report_Name", strReportName)
                    clsCommon.AddColumnsForChange(coll, "Report_Component", strReportComponent)
                    clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECONCILIATION_SETTING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal StrReportName As String, ByVal strReportComponent As String) As ArrayList
        Dim qry As String = "SELECT  * from TSPL_RECONCILIATION_SETTING where Report_Name='" + StrReportName + "' and Report_Component='" + strReportComponent + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim arr As ArrayList = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Account_Code")))
            Next
        End If
        Return arr
    End Function

    Public Shared Function GetAccounts(ByVal ReportName As String, ByVal ReportComponent As String, ByVal dtFrom As DateTime, ByVal dtTo As DateTime) As DataTable
        Dim qry As String = "select Source_Doc_No as docNo,Source_Code as DocType,case when Amount>0 then Amount else 0 end as SubledgerDrAmt,case when Amount<0 then -1*Amount else 0 end as SubledgerCrAmt,Amount as SubledgerAmt from ("
        qry += " select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Code,SUM( TSPL_JOURNAL_DETAILS.Amount) as Amount from TSPL_JOURNAL_DETAILS"
        qry += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No"
        qry += " where  TSPL_JOURNAL_DETAILS.Account_code in (select Account_Code from TSPL_RECONCILIATION_SETTING where Report_Name='" + ReportName + "' and Report_Component='" + ReportComponent + "') and TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_JOURNAL_MASTER.Voucher_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_JOURNAL_MASTER.Voucher_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "'"
        qry += " group by TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Code having SUM(Amount)<>0"
        qry += " )xxx"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
End Class

Public Class clsRecoSettingReportName
    Public Const Bank As String = "Bank"
    Public Const Customer As String = "Customer"
    Public Const Vendor As String = "Vendor"
    Public Const PurchaseBook As String = "Purchase Book"
    Public Const SaleRecoChart As String = "Sale Reco Chart"
    Public Const SaleRegister As String = "Sale Register"


    Public Shared Function GetReportName() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.Bank
        dr("Name") = clsRecoSettingReportName.Bank
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.Customer
        dr("Name") = clsRecoSettingReportName.Customer
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.Vendor
        dr("Name") = clsRecoSettingReportName.Vendor
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.PurchaseBook
        dr("Name") = clsRecoSettingReportName.PurchaseBook
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.SaleRecoChart
        dr("Name") = clsRecoSettingReportName.SaleRecoChart
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsRecoSettingReportName.SaleRegister
        dr("Name") = clsRecoSettingReportName.SaleRegister
        dt.Rows.Add(dr)

        Return dt
    End Function
End Class

Public Class clsRecoSettingReportComponent
    Public Const BankAccount As String = "Bank Account"
    Public Const CustomerAccount As String = "Customer Account"
    Public Const VendorAccount As String = "Vendor Account"

    Public Const PurchaseBookFAAccountFG As String = "FA Account (Finish Goods)"
    Public Const PurchaseBookFAAccountOG As String = "FA Account (Other Goods)"

    Public Const SaleRecoChartSaleAccount As String = "Sale Account"
    Public Const SaleRecoChartExciseAmount As String = "Excise Account"
    Public Const SaleRecoChartTPT As String = "TPT"
    Public Const SaleRecoChartECess As String = "Education Cess"
    Public Const SaleRecoChartHCess As String = "Higher Education Cess"

    Public Const SaleRegisterVat As String = "VAT"
    Public Const SaleRegisterCST As String = "CST"



    Public Shared Function GetReportComponent(ByVal strReportName As String) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        Dim dr As DataRow = dt.NewRow()
        If clsCommon.CompairString(strReportName, clsRecoSettingReportName.Bank) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.BankAccount
            dr("Name") = clsRecoSettingReportComponent.BankAccount
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strReportName, clsRecoSettingReportName.Customer) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.CustomerAccount
            dr("Name") = clsRecoSettingReportComponent.CustomerAccount
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strReportName, clsRecoSettingReportName.Vendor) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.VendorAccount
            dr("Name") = clsRecoSettingReportComponent.VendorAccount
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strReportName, clsRecoSettingReportName.PurchaseBook) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.PurchaseBookFAAccountFG
            dr("Name") = clsRecoSettingReportComponent.PurchaseBookFAAccountFG
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.PurchaseBookFAAccountOG
            dr("Name") = clsRecoSettingReportComponent.PurchaseBookFAAccountOG
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strReportName, clsRecoSettingReportName.SaleRecoChart) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.SaleRecoChartECess
            dr("Name") = clsRecoSettingReportComponent.SaleRecoChartECess
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.SaleRecoChartExciseAmount
            dr("Name") = clsRecoSettingReportComponent.SaleRecoChartExciseAmount
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.SaleRecoChartHCess
            dr("Name") = clsRecoSettingReportComponent.SaleRecoChartHCess
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.SaleRecoChartSaleAccount
            dr("Name") = clsRecoSettingReportComponent.SaleRecoChartSaleAccount
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.SaleRecoChartTPT
            dr("Name") = clsRecoSettingReportComponent.SaleRecoChartTPT
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(strReportName, clsRecoSettingReportName.SaleRegister) = CompairStringResult.Equal Then
            dr("Code") = clsRecoSettingReportComponent.SaleRegisterVat
            dr("Name") = clsRecoSettingReportComponent.SaleRegisterVat
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = clsRecoSettingReportComponent.SaleRegisterCST
            dr("Name") = clsRecoSettingReportComponent.SaleRegisterCST
            dt.Rows.Add(dr)
        End If
        Return dt
    End Function
End Class
