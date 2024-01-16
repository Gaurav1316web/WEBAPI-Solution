Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports XpertERPEngine

Public Class form26Q27Q
    Inherits FrmMainTranScreen




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TDSForm26Q)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub




    Private Sub form26Q27Q_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        rdoquarter.IsChecked = True
        cmbtype.Visible = True
        rdoform26.IsChecked = True
        rdofront.IsChecked = True
        rdoannexure.Visible = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub



    Private Sub fdnbranch__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fdnbranch._MYValidating
        Dim qry As String = "select Branch_Code as [Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
        fdnbranch.Value = clsCommon.ShowSelectForm("form26q27q", qry, "Code", "", fdnbranch.Value, "Code", isButtonClicked)
    End Sub

    Private Sub RadGroupBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox3.Click

    End Sub


    Private Sub rdoannual_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoannual.ToggleStateChanged
        If rdoannual.IsChecked = True Then
            cmbtype.Visible = False
            rdoannexure.Visible = True

            If rdoform27.IsChecked = True And rdoannual.IsChecked = True Then
                rdoannexure.Visible = False
            End If

        End If
    End Sub

    Private Sub rdoquarter_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoquarter.ToggleStateChanged

        If rdoquarter.IsChecked = True Then
            cmbtype.Visible = True
            rdoannexure.Visible = True
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndfiscal__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndfiscal._MYValidating
        Dim qry As String = "select Year_Name as [Year_Name],From_Date as [From Date],To_date as [To Date] from TSPL_TDS_FINANCIAL_YEAR"
        fndfiscal.Value = clsCommon.ShowSelectForm("form26q27q1", qry, "Year_Name", "", fndfiscal.Value, "Year_Name", isButtonClicked)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            If fdnbranch.Value = "" And fndfiscal.Value = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Select the value from Branch Code and Fiscal Year", Me.Text)
            ElseIf fdnbranch.Value <> "" And fndfiscal.Value = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Select the value from Branch Code and Fiscal Year", Me.Text)
            ElseIf fdnbranch.Value = "" And fndfiscal.Value <> "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Select the value from Branch Code and Fiscal Year", Me.Text)
            Else


                Dim sql As String = "select Year_Name,YEAR(CONVERT(date,from_Date,103)) as fromdate,YEAR(CONVERT(date,To_date,103)) as todate  from TSPL_TDS_FINANCIAL_YEAR where Year_Name='" + fndfiscal.Value + "'"
                Dim dt1 As DataTable = Nothing
                dt1 = clsDBFuncationality.GetDataTable(sql)
                Dim fromdate As String = Nothing
                Dim todate As String = Nothing
                If dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        fromdate = dt1.Rows(i)("fromdate")
                        todate = dt1.Rows(i)("todate")
                    Next
                End If


                Dim qry As String
                If rdoform26.IsChecked = True And rdoannual.IsChecked = True And rdofront.IsChecked = True Then
                    qry = " select Comp_Name,(Add1) as Address ,Pincode,State,Email,(Phone1+','+Phone2) as telephone ,Pan_No,Tan_No ,TSPL_REMITTANCE_ENTRY.branch_code,TSPL_REMITTANCE_ENTRY.comp_code,TSPL_REMITTANCE_ENTRY_DETAIL.fiscal_year from  TSPL_COMPANY_MASTER left outer join TSPL_REMITTANCE_ENTRY  on TSPL_REMITTANCE_ENTRY.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and branch_code='" + fdnbranch.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26A01", "Form 26A01 Report")

                ElseIf rdoform26.IsChecked = True And rdoannual.IsChecked = True And rdodetail.IsChecked = True Then
                    'qry = "select section_code as Sec,Actual_Total_TDS as tds ,Actual_Surcharge as Surcharge ,Actual_Edu_Cess as edu ,Cheque_No,BSR_Code,Remittance_Date,Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(Actual_Total_TDS,0)+ISNULL(Actual_Surcharge,0)+ISNULL(Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and  TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and  TSPL_REMITTANCE_ENTRY.Fiscal_Year='" + fndfiscal.Value + "'"

                    qry = "select TSPL_REMITTANCE_ENTRY.section_code as Sec,TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS as tds ,TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge as Surcharge ,TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess as edu ,TSPL_REMITTANCE_ENTRY.Cheque_No,TSPL_REMITTANCE_ENTRY.BSR_Code,TSPL_REMITTANCE_ENTRY.Remittance_Date,TSPL_REMITTANCE_ENTRY.Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS,0)+ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge,0)+ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY left outer Join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code left outer join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and  TSPL_REMITTANCE_ENTRY.Branch_Code='DELHI' and  TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "'"


                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26A02", "Form 26A02 Report")
                ElseIf rdoform26.IsChecked = True And rdoquarter.IsChecked = True And rdodetail.IsChecked = True Then
                    'qry = "select section_code as Sec,Actual_Total_TDS as tds ,Actual_Surcharge as Surcharge ,Actual_Edu_Cess as edu ,Cheque_No,BSR_Code,Remittance_Date,Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(Actual_Total_TDS,0)+ISNULL(Actual_Surcharge,0)+ISNULL(Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and  TSPL_REMITTANCE_ENTRY.Fiscal_Year='" + fndfiscal.Value + "' and Quarter='" + cmbtype.Text + "'"
                    qry = "select section_code as Sec,Actual_Total_TDS as tds ,Actual_Surcharge as Surcharge ,Actual_Edu_Cess as edu ,Cheque_No,BSR_Code,Remittance_Date,Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(Actual_Total_TDS,0)+ISNULL(Actual_Surcharge,0)+ISNULL(Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code left outer join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and  TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and Quarter='" + cmbtype.Text + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26Q02", "Form 26Q02 Report")
                ElseIf rdoform27.IsChecked = True And rdoquarter.IsChecked = True And rdofront.IsChecked = True Then
                    'qry = "select Comp_Name,(Add1+','+add2+','+Add3) as Address ,TSPL_COMPANY_MASTER.Pincode,State,TSPL_COMPANY_MASTER.Email,(Phone1+','+Phone2) as telephone ,Pan_No,Tan_No,TSPL_TDS_RESP_PERSON.Person_Name,(TSPL_TDS_RESP_PERSON.Address1+','+TSPL_TDS_RESP_PERSON.Address2) as AddressR,TSPL_TDS_RESP_PERSON.City as rCity,TSPL_TDS_RESP_PERSON.Pincode as rPin,TSPL_TDS_RESP_PERSON.Country as rCountry,TSPL_TDS_RESP_PERSON.Telephone as rTelephone,TSPL_TDS_RESP_PERSON.Fax  as rFax,TSPL_TDS_RESP_PERSON.Email from  TSPL_COMPANY_MASTER join TSPL_TDS_RESP_PERSON on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TDS_RESP_PERSON.Comp_Code where TSPL_TDS_RESP_PERSON.Branch_Code='" + fdnbranch.Value + "' "
                    qry = "select Comp_Name,(Add1+','+add2+','+Add3) as Address ,TSPL_COMPANY_MASTER.Pincode,State,TSPL_COMPANY_MASTER.Email,(Phone1+','+Phone2) as telephone ,Pan_No,Tan_No,TSPL_TDS_RESP_PERSON.Person_Name,(TSPL_TDS_RESP_PERSON.Address1+','+TSPL_TDS_RESP_PERSON.Address2) as AddressR,TSPL_TDS_RESP_PERSON.City as rCity,TSPL_TDS_RESP_PERSON.Pincode as rPin,TSPL_TDS_RESP_PERSON.Country as rCountry,TSPL_TDS_RESP_PERSON.Telephone as rTelephone,TSPL_TDS_RESP_PERSON.Fax  as rFax,TSPL_TDS_RESP_PERSON.Email,'" + fromdate + "' as fromdate,'" + todate + "' as todate  from  TSPL_COMPANY_MASTER join TSPL_TDS_RESP_PERSON on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TDS_RESP_PERSON.Comp_Code where TSPL_TDS_RESP_PERSON.Branch_Code='" + fdnbranch.Value + "' "

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form27Q01", "Form 27Q01 Report")
                ElseIf rdoform26.IsChecked = True And rdoquarter.IsChecked = True And rdofront.IsChecked = True Then
                    qry = "select Comp_Name,(Add1+','+add2+','+Add3) as Address ,TSPL_COMPANY_MASTER.Pincode,State,TSPL_COMPANY_MASTER.Email,(Phone1+','+Phone2) as telephone ,Pan_No,Tan_No,TSPL_TDS_RESP_PERSON.Person_Name,(TSPL_TDS_RESP_PERSON.Address1+','+TSPL_TDS_RESP_PERSON.Address2) as AddressR,TSPL_TDS_RESP_PERSON.City as rCity,TSPL_TDS_RESP_PERSON.Pincode as rPin,TSPL_TDS_RESP_PERSON.Country as rCountry,TSPL_TDS_RESP_PERSON.Telephone as rTelephone,TSPL_TDS_RESP_PERSON.Fax  as rFax,TSPL_TDS_RESP_PERSON.Email,'" + fromdate + "' as fromdate,'" + todate + "' as todate from  TSPL_COMPANY_MASTER join TSPL_TDS_RESP_PERSON on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TDS_RESP_PERSON.Comp_Code where TSPL_TDS_RESP_PERSON.Branch_Code='" + fdnbranch.Value + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26Q01", "Form 26Q01 Report")
                ElseIf rdoform27.IsChecked = True And rdoannual.IsChecked = True And rdofront.IsChecked = True Then
                    qry = "select Comp_Name,(Add1+','+add2+','+Add3) as Address ,TSPL_COMPANY_MASTER.Pincode,State,TSPL_COMPANY_MASTER.Email,(Phone1+','+Phone2) as telephone ,Pan_No,Tan_No,TSPL_TDS_RESP_PERSON.Person_Name,(TSPL_TDS_RESP_PERSON.Address1+','+TSPL_TDS_RESP_PERSON.Address2) as AddressR,TSPL_TDS_RESP_PERSON.City as rCity,TSPL_TDS_RESP_PERSON.Pincode as rPin,TSPL_TDS_RESP_PERSON.Country as rCountry,TSPL_TDS_RESP_PERSON.Telephone as rTelephone,TSPL_TDS_RESP_PERSON.Fax  as rFax,TSPL_TDS_RESP_PERSON.Email,'" + fromdate + "' as fromdate,'" + todate + "' as todate from  TSPL_COMPANY_MASTER join TSPL_TDS_RESP_PERSON on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TDS_RESP_PERSON.Comp_Code where TSPL_TDS_RESP_PERSON.Branch_Code='" + fdnbranch.Value + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form27A01", "Form 27A01 Report")
                ElseIf rdoform27.IsChecked = True And rdoquarter.IsChecked = True And rdodetail.IsChecked = True Then
                    'qry = "select section_code as Sec,Actual_Total_TDS as tds ,Actual_Surcharge as Surcharge ,Actual_Edu_Cess as edu ,Cheque_No,BSR_Code,Remittance_Date,Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(Actual_Total_TDS,0)+ISNULL(Actual_Surcharge,0)+ISNULL(Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and  TSPL_REMITTANCE_ENTRY.Fiscal_Year='" + fndfiscal.Value + "' and Quarter='" + cmbtype.Text + "'"
                    qry = "select section_code as Sec,Actual_Total_TDS as tds ,Actual_Surcharge as Surcharge ,Actual_Edu_Cess as edu ,Cheque_No,BSR_Code,Remittance_Date,Challan_No ,TSPL_TDS_RESP_PERSON.Person_Name,TSPL_TDS_RESP_PERSON.City,TSPL_TDS_RESP_PERSON.Designation,(ISNULL(Actual_Total_TDS,0)+ISNULL(Actual_Surcharge,0)+ISNULL(Actual_Edu_Cess,0)) as Total,0 as intrest ,0 as others  from TSPL_REMITTANCE_ENTRY  left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code left outer join TSPL_TDS_RESP_PERSON on TSPL_REMITTANCE_ENTRY.Branch_Code=TSPL_TDS_RESP_PERSON.Branch_Code where TSPL_REMITTANCE_ENTRY.Posted='Y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and  TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Quarter='" + cmbtype.Text + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form27Q02", "Form 27Q02 Report")
                ElseIf rdoform26.IsChecked = True And rdoquarter.IsChecked = True And rdoannexure.IsChecked = True Then
                    qry = "SELECT     TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Name, TSPL_REMITTANCE_ENTRY.Challan_No, TSPL_REMITTANCE_ENTRY.BSR_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Document_Date, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Sec_Educess, " & _
                          "TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_Total_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS,(ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS,0)+ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge,0)+ISNULL( TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess,0)) as deducted ,TSPL_REMITTANCE_ENTRY.Section_Code, " & _
                          "TSPL_REMITTANCE_ENTRY.Branch_Code,TSPL_REMITTANCE_ENTRY.Remittance_Date, TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction, TSPL_TDS_VENDOR_DETAILS.PAN, " & _
                          "TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe, TSPL_VENDOR_MASTER.Vendor_Name AS Expr1, TSPL_TDS_RESP_PERSON.Person_Name," & _
                          "TSPL_TDS_RESP_PERSON.Designation, TSPL_TDS_RESP_PERSON.City, TSPL_TDS_RESP_PERSON.Branch_Code AS Expr2 " & _
    "                   FROM         TSPL_TDS_RESP_PERSON left outer JOIN " & _
     "                     TSPL_REMITTANCE_ENTRY ON TSPL_TDS_RESP_PERSON.Branch_Code = TSPL_REMITTANCE_ENTRY.Branch_Code left outer join TSPL_REMITTANCE_ENTRY_DETAIL on  TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code LEFT OUTER JOIN " & _
      "                   TSPL_VENDOR_MASTER INNER JOIN " & _
       "                   TSPL_TDS_VENDOR_DETAILS ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code ON  " & _
        "                  TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_REMITTANCE_ENTRY.posted='y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Quarter='" + cmbtype.Text + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26Q03", "Form 26Q03 Report")
                ElseIf rdoform26.IsChecked = True And rdoannual.IsChecked = True And rdoannexure.IsChecked = True Then
                    qry = "SELECT     TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Name, TSPL_REMITTANCE_ENTRY.Challan_No, TSPL_REMITTANCE_ENTRY.BSR_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Document_Date, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Sec_Educess, " & _
                          "TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_Total_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS,(ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS,0)+ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge,0)+ISNULL( TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess,0)) as deducted ,TSPL_REMITTANCE_ENTRY.Section_Code, " & _
                          "TSPL_REMITTANCE_ENTRY.Branch_Code,TSPL_REMITTANCE_ENTRY.Remittance_Date, TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction, TSPL_TDS_VENDOR_DETAILS.PAN, " & _
                          "TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe, TSPL_VENDOR_MASTER.Vendor_Name AS Expr1, TSPL_TDS_RESP_PERSON.Person_Name," & _
                          "TSPL_TDS_RESP_PERSON.Designation, TSPL_TDS_RESP_PERSON.City, TSPL_TDS_RESP_PERSON.Branch_Code AS Expr2 " & _
    "                   FROM         TSPL_TDS_RESP_PERSON left outer JOIN " & _
     "                     TSPL_REMITTANCE_ENTRY ON TSPL_TDS_RESP_PERSON.Branch_Code = TSPL_REMITTANCE_ENTRY.Branch_Code    left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code LEFT OUTER JOIN " & _
      "                   TSPL_VENDOR_MASTER left outer JOIN " & _
       "                   TSPL_TDS_VENDOR_DETAILS ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code ON  " & _
        "                  TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_REMITTANCE_ENTRY.posted='y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Quarter='" + cmbtype.Text + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form26A03", "Form 26A03 Report")
                ElseIf rdoform27.IsChecked = True And rdoquarter.IsChecked = True And rdoannexure.IsChecked = True Then
                    qry = "SELECT     TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Name, TSPL_REMITTANCE_ENTRY.Challan_No, TSPL_REMITTANCE_ENTRY.BSR_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Document_Date, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Sec_Educess, " & _
                          "TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_Total_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS,(ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS,0)+ISNULL(TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Surcharge,0)+ISNULL( TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Edu_Cess,0)) as deducted ,TSPL_REMITTANCE_ENTRY.Section_Code, " & _
                          "TSPL_REMITTANCE_ENTRY.Branch_Code,TSPL_REMITTANCE_ENTRY.Remittance_Date, TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction, TSPL_TDS_VENDOR_DETAILS.PAN, " & _
                          "TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe, TSPL_VENDOR_MASTER.Vendor_Name AS Expr1, TSPL_TDS_RESP_PERSON.Person_Name," & _
                          "TSPL_TDS_RESP_PERSON.Designation, TSPL_TDS_RESP_PERSON.City, TSPL_TDS_RESP_PERSON.Branch_Code AS Expr2 " & _
    "                   FROM         TSPL_TDS_RESP_PERSON  left outer JOIN " & _
     "                     TSPL_REMITTANCE_ENTRY ON TSPL_TDS_RESP_PERSON.Branch_Code = TSPL_REMITTANCE_ENTRY.Branch_Code left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code    LEFT OUTER JOIN " & _
      "                   TSPL_VENDOR_MASTER left outer JOIN " & _
       "                   TSPL_TDS_VENDOR_DETAILS ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code ON  " & _
        "                  TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_REMITTANCE_ENTRY.posted='y' and TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Quarter='" + cmbtype.Text + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form27Q03", "Form 27Q03 Report")
                ElseIf rdoform27.IsChecked = True And rdoannual.IsChecked = True And rdodetail.IsChecked = True Then
                    qry = "SELECT  Vendor_Code, Amt_To_Remit, Actual_TDS,Actual_TDS_Base, Calculated_TDS,tspl_tds_resp_person.Person_Name,tspl_tds_resp_person.City,tspl_tds_resp_person.Designation FROM  TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on  TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code  left outer join tspl_tds_resp_person  on TSPL_REMITTANCE_ENTRY.Branch_Code=tspl_tds_resp_person.Branch_Code  where TSPL_REMITTANCE_ENTRY.posted='y' and  TSPL_REMITTANCE_ENTRY.Branch_Code='" + fdnbranch.Value + "' and TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year='" + fndfiscal.Value + "' and Posted='Y'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "form27A02", "Form 27A02 Report")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        rdoquarter.IsChecked = True
        cmbtype.Visible = True
        rdoform26.IsChecked = True
        rdofront.IsChecked = True
        rdoannexure.Visible = True
        fdnbranch.Value = ""
        fndfiscal.Value = ""

    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "FRM26/27-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function






End Class
