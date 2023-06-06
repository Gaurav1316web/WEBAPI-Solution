'==created by shivani tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptServiceTaxDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim Todate As Date = Nothing
    Dim First As Date = Nothing
    Dim Second As Date = Nothing
    Dim third As Date = Nothing
    Dim Forth As Date = Nothing
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Dim variable1 As String = Nothing
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptServiceTaxDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport


    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(ChkServiceRegister.Checked = True, "SR", "")
        TemplateGridview = gv
        Load_report()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtAdditional.arrDispalyMember IsNot Nothing AndAlso txtAdditional.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Additional Charge : " + clsCommon.GetMulcallStringWithComma(txtAdditional.arrDispalyMember))
            End If
            If chkQuarterSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgQuarter.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgQuarter.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Quarter : " + strlocName + " "))

            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Service Tax Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Service Tax Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        'print(EnumExportTo.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Sub LoadQuarter()
        Todate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,start_date,103) as Date from TSPL_Fiscal_Year_Master where Is_current_year='1'"))
        First = clsCommon.GetPrintDate(Todate.AddMonths(3), "dd/MMM/yyyy")
        Second = clsCommon.GetPrintDate(Todate.AddMonths(6), "dd/MMM/yyyy")
        third = clsCommon.GetPrintDate(Todate.AddMonths(9), "dd/MMM/yyyy")
        Forth = clsCommon.GetPrintDate(Todate.AddMonths(12), "dd/MMM/yyyy")
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Quarter", GetType(String))
        dt.Columns.Add("From", GetType(String))


        Dim dr As DataRow = dt.NewRow()
        dr("Quarter") = "1"
        dr("From") = "" & Todate & " - " & First.AddDays(-1) & ""

        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Quarter") = "2"
        dr("From") = "" & First & " - " & Second.AddDays(-1) & ""

        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Quarter") = "3"
        dr("From") = "" & Second & " - " & third.AddDays(-1) & ""

        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Quarter") = "4"
        dr("From") = "" & third & " - " & Forth.AddDays(-1) & ""

        dt.Rows.Add(dr)

        isInsideLoadData = True
        cbgQuarter.DataSource = dt
        cbgQuarter.ValueMember = "Quarter"
        cbgQuarter.DisplayMember = "From"
        isInsideLoadData = False
    End Sub
    Function Check()
        Dim Qry As String = ""
        Dim arr As ArrayList = cbgQuarter.CheckedValue
        If cbgQuarter.CheckedValue.Count = 1 Then
            'For Each Str As String In cbgQuarter.CheckedValue
            If arr.Contains("4") Then
                Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103) "

            ElseIf arr.Contains("3") Then
                Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103) "


            ElseIf arr.Contains("2") Then
                Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) "

            ElseIf arr.Contains("1") Then
                Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103) "
            End If
            'Next
        End If

        If cbgQuarter.CheckedValue.Count = 2 Then
            'For Each Str As String In cbgQuarter.CheckedValue
            If arr.Contains("1") And arr.Contains("2") Then ' If clsCommon.myCstr(Str) = "1" And clsCommon.myCstr(Str) = "2" Then
                Qry += " and  (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103)  or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) )"

            ElseIf arr.Contains("1") And arr.Contains("3") Then
                Qry += " and  (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103))  "


            ElseIf arr.Contains("1") And arr.Contains("4") Then
                Qry += "  and  (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103)) "

            ElseIf arr.Contains("2") And arr.Contains("3") Then
                Qry += " and (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103) )"
            ElseIf arr.Contains("2") And arr.Contains("4") Then
                Qry += "  and (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103) )"
            ElseIf arr.Contains("3") And arr.Contains("4") Then
                Qry += "  and (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103) )"
            End If
            'Next
        End If
        If cbgQuarter.CheckedValue.Count = 3 Then
            'For Each Str As String In cbgQuarter.CheckedValue
            If arr.Contains("1") And arr.Contains("2") And arr.Contains("3") Then
                Qry += " and  (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103)  or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103) )"

            ElseIf arr.Contains("1") And arr.Contains("2") And arr.Contains("4") Then
                Qry += " and  (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103)  or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) or  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103) )"


            ElseIf arr.Contains("2") And arr.Contains("3") And arr.Contains("4") Then
                Qry += "  and (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103) or   convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103)) "

            End If
            'Next
        End If
        If cbgQuarter.CheckedValue.Count = 4 Then
            'For Each Str As String In cbgQuarter.CheckedValue
            If arr.Contains("1") And arr.Contains("2") And arr.Contains("3") And arr.Contains("4") Then
                Qry += " and ( convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103))  "


            End If
            'Next
        End If
        Return Qry
    End Function
    Sub Load_report()
        '=============created by preeti gupta Against ticket no [BM00000009383] 18/10/2016
        Try
            Dim variable2 As String = Nothing
            Dim headervalue1 As String = Nothing

            Dim variable3 As String = Nothing
            Dim headervalue2 As String = Nothing

            Dim variable4 As String = Nothing
            Dim headervalue3 As String = Nothing

            'Dim variable1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) ) XXX For XML Path('')),1,1,'')"))
            variable1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')),1,1,'')"))
            Dim headervalue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',isnull(['+Tax_Code_Desc+'],0) as ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))


            ' variable3 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')),1,1,'')"))
            'headervalue2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',max(isnull(['+Tax_Code_Desc+'],0)) as ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))
            headervalue2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',sum(isnull(['+Tax_Code_Desc+'],0)) as ['+Tax_Code_Desc+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))


            If ChkServiceRegister.Checked = True Then
                variable2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')),1,1,'')"))

                headervalue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',convert(decimal(18,2),isnull(['+Tax_Code_Desc+' Rate'+'],0)) as ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))

                'headervalue3 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',max(convert(decimal(18,2),isnull(['+Tax_Code_Desc+' Rate'+'],0))) as ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))
                headervalue3 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',sum(convert(decimal(18,2),isnull(['+Tax_Code_Desc+' Rate'+'],0))) as ['+Tax_Code_Desc+' Rate'+']' from (Select Distinct TSPL_TAX_MASTER.Tax_Code_Desc from TSPL_VENDOR_INVOICE_HEAD LEFt OUTER JOIN TSPL_VENDOR_INVOICE_DEtail on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_VENDOR_INVOICE_DEtail.Document_No LEFT OUTER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_VENDOR_INVOICE_HEAD.Tax_Group LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code WHERE Tax_Recoverable='Y' AND TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS'  ) XXX For XML Path('')))"))
            End If



            If clsCommon.myLen(variable1) <= 0 Or clsCommon.myLen(headervalue) <= 0 Then
                Throw New Exception("No service type invoice found to display")
            End If



            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
                txtFromDate.Focus()
                Exit Sub
            End If

            If chkQuarterSelect.IsChecked AndAlso cbgQuarter.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please select atleast single Quarter or select all.")
                Exit Sub
            End If
            'Change By Prabhakar : Total_Amount replace to TSPL_VENDOR_INVOICE_DETAIL.Item_Rate as Total_Amount Ticket : BM00000009442'
            Dim Qry As String = "select fn.Quarter ,fn.month,fn.Vendor_Code ,fn.Vendor_Name"

            If ChkServiceRegister.Checked = True Then
                Qry += " ,fn.add1+' '+fn.add2+' '+fn.add3 as Vendor_address,fn.GSTFinalNo as Vendor_GST_No,fn.Vendor_GST_State,fn.Vendor_Registered,Place_of_Supply as [Place Of Supply] ,fn.[Amount in foreign currency],fn.[Amount of tax],fn.[Amount paid in advance],fn.[Date of advance paid],fn.[Amount of refund],fn.[Tax of refund] "
            End If
            ''KDI/23/05/18-000330
            Qry += " ,fn.Loc_Code ,fn.Location_Desc ,fn.GL_Account_Code ,fn.GL_Account_Name ,fn.Remarks ,fn.AddChargeCode ,fn.Add_desc,fn.SAC_Code ,fn.Invoice_No ,fn.Invoice_Date ,convert(decimal(18,2),fn.Total_Amount,2) as Total_Amount ,fn.Abatement_Per,convert(decimal(18,2),fn.Reverse_Charge_Amount,2) as Reverse_Charge_Amount, 100-fn.Abatement_Per as Abat_Per ,convert(decimal(18,2),fn.Abatement,2) as Abatement,convert(decimal(18,2),fn.Net_Amt,2) as Net_Amt ,fn.Reverse_Charge_Per,fn.[Reverse %],fn.[Purchase_Tax_Invoice],fn.[Purchase_Tax_Invoice_Type]  ,fn.doc_no as Document_No,fn.Document_Type ,fn.Doc_Date" + headervalue1 + headervalue + "  from (select * from (select   case when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "' ,103)) then 1 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103)) then 2 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "' ,103)) then 3 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103)) then 4 end as Quarter,"
            Qry += " case when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First.AddDays(-1) + "'  ,103)) then 04 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second.AddDays(-1) + "' ,103)) then 07 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third.AddDays(-1) + "'  ,103)) then 10 when (convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth.AddDays(-1) + "' ,103)) then 01 end as Month1 ,MONTH(convert(date,(TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date),103)) as Month,  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.add1,TSPL_VENDOR_MASTER.add2,TSPL_VENDOR_MASTER.add3,tspl_vendor_master.GSTFinalNo,TSPL_STATE_MASTER.GST_STATE_Code as Vendor_GST_State, " & _
                " case when tspl_vendor_master.GSTRegistered=1 then 'YES' else 'No' end as Vendor_Registered,TSPL_LOCATION_MASTER.City_Code as Place_of_Supply,case when TSPL_VENDOR_MASTER.CURRENCY_CODE=TSPL_COMPANY_MASTER.BaseCurrencyCode then 'No' else 'Yes' end  as [Amount in foreign currency],tspl_payment_header.Payment_Amount as [Amount paid in advance],convert(varchar,tspl_payment_header.Payment_Date,103) as [Date of advance paid],tspl_payment_header.Tax_Amount_Advance as [Amount of tax],'' as [Amount of refund],'' as [Tax of refund],Loc_Code,coalesce(Location_Desc,TSPL_GL_SEGMENT_CODE.description) as Location_Desc,GL_Account_Code,TSPL_GL_ACCOUNTS.Description as GL_Account_Name,TSPL_VENDOR_INVOICE_HEAD.Description as Remarks,TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode, tspl_Additional_Charges.Description as Add_desc,tspl_Additional_Charges.SAC_Code ,Vendor_Invoice_No as Invoice_No,Vendor_Invoice_Date as Invoice_Date, TSPL_VENDOR_INVOICE_DETAIL.amount as Total_Amount,Abatement_Per,(Item_Rate-Abatement_Amt)as Abatement,Abatement_Amt as Net_Amt,"
            '" case when tspl_vendor_master.GSTRegistered=1 and TSPL_VENDOR_INVOICE_head.RCM=1 and TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit=1 then 0 else case when tspl_vendor_master.GSTRegistered=0 and TSPL_VENDOR_INVOICE_head.RCM=0 and TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit=1 then 0 else case when tspl_vendor_master.GSTRegistered=1 and TSPL_VENDOR_INVOICE_head.RCM=1 then 100 else case when tspl_vendor_master.GSTRegistered=0 and TSPL_VENDOR_INVOICE_head.RCM=0 then 100 else case when tspl_vendor_master.GSTRegistered=1 and TSPL_VENDOR_INVOICE_head.RCM=0 then 0 else case when TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit=1 then 0 end end end end end end as [Reverse %], " & _
            ''RICHA AS PER CONDITION WHICH WAS GIVEN BY RANJANA MAM ON 3 nOV,2017
            'Qry += "CASE WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=0 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=0 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=0 then 100 " & _
            '        " WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=0 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=0 then 100 WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=0 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=1 then 0 " & _
            '        " WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=0 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=0 then 0 WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=0 then 100 " & _
            '        " WHEN ISNULL(tspl_vendor_master.GSTRegistered,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 and ISNULL(TSPL_VENDOR_INVOICE_HEAD.No_GST_Credit,0)=1 then 0 END as [Reverse %], " & _
            Qry += "CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=0 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=0 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=0 THEN 100" & _
            "  WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=0 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=0 THEN 100 " & _
            "  WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=0 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=1 THEN 0 " & _
            "  WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=0 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=0 THEN 0 " & _
            "  WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=0 THEN 100 " & _
            " WHEN ISNULL(TSPL_VENDOR_INVOICE_head.GSTRegistered,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.RCM,0)=1 AND ISNULL(TSPL_VENDOR_INVOICE_head.No_GST_Credit,0)=1 THEN 0 ELSE 100 END AS [Reverse %]," & _
        " TSPL_VENDOR_INVOICE_head.Purchase_Tax_Invoice,TSPL_VENDOR_INVOICE_head.Purchase_Tax_Invoice_Type,TSPL_VENDOR_INVOICE_DETAIL.Reverse_Charge_Per,  Reverse_Charge_Amount, TSPL_VENDOR_INVOICE_HEAD.Document_No as Doc_No,Invoice_Entry_Date as Doc_Date ,TSPL_VENDOR_INVOICE_DETAIL.detail_line_no,TSPL_VENDOR_INVOICE_HEAD.Document_Type "
            Qry += "  from TSPL_VENDOR_INVOICE_HEAD left join TSPL_VENDOR_INVOICE_DETAIL  on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VENDOR_MASTER.City_Code left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Seg_No =7 and TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_VENDOR_INVOICE_HEAD.Loc_code left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS .Account_Code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  left join tspl_Additional_Charges on tspl_Additional_Charges.Code=TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode left outer join tspl_payment_header on tspl_payment_header.PurchaseOrder_No_GST=TSPL_VENDOR_INVOICE_HEAD.RefDocNo left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.BaseCurrencyCode=TSPL_VENDOR_MASTER.CURRENCY_CODE where  TSPL_VENDOR_INVOICE_HEAD.Invoice_type='VS' and Posting_Date <>'' "
            If chkQuarterSelect.IsChecked And cbgQuarter.CheckedValue.Count > 0 Then
                txtFromDate.Enabled = False
                txtToDate.Enabled = False
                '    For Each Str As String In cbgQuarter.CheckedValue
                '        If clsCommon.myCstr(Str) = "4" Then
                '            Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth + "' ,103) "

                '        ElseIf clsCommon.myCstr(Str) = "3" Then
                '            Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third + "' ,103) "


                '        ElseIf clsCommon.myCstr(Str) = "2" Then
                '            Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second + "' ,103) "

                '        ElseIf clsCommon.myCstr(Str) = "1" Then
                '            Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First + "' ,103) "
                '        End If
                '    Next
                'Else
                Qry += Check()
            Else
                Qry += "  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            End If
            ''richa KDI/15/05/18-000317 show unclamied tax data in report
            'Qry += " and isnull(Total_Amount,0)>0 and is_Unclaimed_Tax='0'"
            Qry += " and isnull(Total_Amount,0)>0 "

            If txtAdditional.arrValueMember IsNot Nothing AndAlso txtAdditional.arrValueMember.Count > 0 Then
                Qry += " and TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode  IN (" + clsCommon.GetMulcallString(txtAdditional.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                Qry += " and TSPL_VENDOR_MASTER.VENDOR_CODE  IN (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            Dim strWhrCatg As String = ""


            If rbtnLocationSelect.IsChecked Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                        If IsApplicable Then
                            strWhrCatg += " Or "
                        End If
                        strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then tspl_location_master.Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            strWhrCatg += " and Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    strWhrCatg += ","
                                End If
                                strWhrCatg += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            strWhrCatg += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
                Qry += " and (" + strWhrCatg + ")"
            End If

            Qry += " )as d"
            'Qry += " left outer join (select * from ( select f.Document_No, MAX(Tax_Code) as Tax_Code, MAX(Tax) as Tax, SUM(tax_amt) as tax_amt from ("
            Qry += " left outer join "
            If ChkServiceRegister.Checked = True Then
                Qry += " (select * from "
            End If

            Qry += " (select * from ( select f.detail_line_no as Line_no, f.Document_No,  Tax,taxpivot,  convert(decimal(18,2),tax_amt,2) as tax_amt,convert(decimal(18,2),Tax_Rate) as Tax_Rate from ("
            Qry += " select TSPL_VENDOR_INVOICE_Detail.detail_line_no, TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX1) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate'  as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax1_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax1_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX1_Rate as Tax_Rate from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax1 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "
            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax1_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "
            Qry += " union all"
            Qry += "  select TSPL_VENDOR_INVOICE_Detail.detail_line_no,TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX2) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax2_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax2_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX2_Rate as Tax_Rate from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax2 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No  WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax2_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "

            Qry += " union all"
            Qry += " select TSPL_VENDOR_INVOICE_Detail.detail_line_no, TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX3) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax3_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax3_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX3_Rate as Tax_Rate  from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax3 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y'  "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax3_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "


            Qry += " union all"
            Qry += " select TSPL_VENDOR_INVOICE_Detail.detail_line_no, TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX4) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax4_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax4_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX4_Rate as Tax_Rate from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax4 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax4_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "


            Qry += " union all"
            Qry += "  select  TSPL_VENDOR_INVOICE_Detail.detail_line_no,TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX5) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax5_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax5_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX5_Rate as Tax_Rate from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax5 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax5_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "

            Qry += " union all"
            Qry += " select TSPL_VENDOR_INVOICE_Detail.detail_line_no,  TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX6) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax6_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax6_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX6_Rate as Tax_Rate  from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax6 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax6_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "


            Qry += " union all"
            Qry += "   select TSPL_VENDOR_INVOICE_Detail.detail_line_no,TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX7) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax7_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax7_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX7_Rate as Tax_Rate from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax7 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax7_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "

            Qry += " union all"
            Qry += "  select TSPL_VENDOR_INVOICE_Detail.detail_line_no, TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX8) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax8_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax8_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX8_Rate as Tax_Rate  from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax8 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax8_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "

            Qry += "  union all"
            Qry += "  select TSPL_VENDOR_INVOICE_Detail.detail_line_no, TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX9) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax9_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax9_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX9_Rate as Tax_Rate  from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax9 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "

            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax9_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0'  "

            Qry += "  union all"
            Qry += " select  TSPL_VENDOR_INVOICE_Detail.detail_line_no,TSPL_VENDOR_INVOICE_Detail.Document_No, (TSPL_VENDOR_INVOICE_Detail.TAX10) as Tax_Code, (tspl_tax_master.tax_code_desc) as Tax, (tspl_tax_master.tax_code_desc) + ' Rate' as taxpivot, case when Document_Type='D' then (-1*(TSPL_VENDOR_INVOICE_Detail.tax10_amt)) else (TSPL_VENDOR_INVOICE_Detail.tax10_amt) end as tax_amt,TSPL_VENDOR_INVOICE_Detail.TAX10_Rate as Tax_Rate  from TSPL_VENDOR_INVOICE_Detail left outer join tspl_tax_master on tspl_tax_master.tax_code=TSPL_VENDOR_INVOICE_Detail.tax10 left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_Detail.Document_No WHERE tspl_tax_master.Tax_Recoverable='Y' "
            If ChkServiceRegister.Checked = False Then
                Qry += " and isnull(TSPL_VENDOR_INVOICE_Detail.tax7_amt,0)<=0  "
            End If
            Qry += " and is_Unclaimed_Tax='0' )f left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = f.Document_No  where  Invoice_type='VS' "
            If chkQuarterSelect.IsChecked And cbgQuarter.CheckedValue.Count > 0 Then
                txtFromDate.Enabled = False
                txtToDate.Enabled = False
                'For Each Str As String In cbgQuarter.CheckedValue
                '    If clsCommon.myCstr(Str) = "4" Then
                '        Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + third + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Forth + "' ,103) "

                '    ElseIf clsCommon.myCstr(Str) = "3" Then
                '        Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Second + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + third + "' ,103) "


                '    ElseIf clsCommon.myCstr(Str) = "2" Then
                '        Qry += " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + First + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + Second + "' ,103) "

                '    ElseIf clsCommon.myCstr(Str) = "1" Then
                '        Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + Todate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + First + "' ,103) "
                '    End If
                'Next
                Qry += Check()
            Else
                Qry += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            End If
            Qry += "  )  final "
            Qry += " pivot (sum(final.tax_amt) for final.Tax in ( " + variable1 + " ))t ) "
            If ChkServiceRegister.Checked = True Then
                Qry += " final1 "
                Qry += " pivot (avg(final1.tax_rate) for final1.taxpivot in ( " + variable2 + " ))t1)"
            End If
            Qry += "  as XXX  ON XXX.Document_No=d.Doc_No  and XXX.Line_no =d.Detail_Line_No)as FN "
            'If ChkServiceRegister.Checked = True Then
            '    Qry = "Select Quarter ,month,Vendor_Code ,Vendor_Name ,Vendor_address, Vendor_GST_No,Vendor_GST_State,Vendor_Registered,[Place Of Supply] ,[Amount in foreign currency],[Amount of tax],[Amount paid in advance],[Date of advance paid],[Amount of refund],[Tax of refund]  ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date " & headervalue2 & " " & headervalue3 & " from (" & Qry & ") lastFinal group by Quarter ,month,Vendor_Code ,Vendor_Name ,Vendor_address, Vendor_GST_No,Vendor_GST_State,Vendor_Registered,[Place Of Supply] ,[Amount in foreign currency],[Amount of tax],[Amount paid in advance],[Date of advance paid],[Amount of refund],[Tax of refund]  ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date order by convert(date,Doc_Date,103),Document_No"
            'Else
            '    Qry = "Select Quarter ,month,Vendor_Code ,Vendor_Name ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date , Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date " & headervalue2 & " from (" & Qry & ") lastFinal group by Quarter ,month,Vendor_Code ,Vendor_Name ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date order by convert(date,Doc_Date,103),Document_No"
            'End If
            
            If ChkServiceRegister.Checked = True Then
                Qry = "Select Quarter ,month,Vendor_Code ,Vendor_Name ,Vendor_address, Vendor_GST_No,Vendor_GST_State,Vendor_Registered,[Place Of Supply] ,[Amount in foreign currency],[Amount of tax],[Amount paid in advance],[Date of advance paid],[Amount of refund],[Tax of refund]  ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,case when isnull(Document_Type,'')='D' then -1* Total_Amount else Total_Amount end as Total_Amount ,Abatement_Per, case when isnull(Document_Type,'')='D' then -1* Reverse_Charge_Amount else Reverse_Charge_Amount end as Reverse_Charge_Amount,  Abat_Per , case when isnull(Document_Type,'')='D' then -1* Abatement else Abatement end as Abatement,case when isnull(Document_Type,'')='D' then -1* Net_Amt else Net_Amt end as Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date " & headervalue2 & " " & headervalue3 & " from (" & Qry & ") lastFinal group by Quarter ,month,Vendor_Code ,Vendor_Name ,Vendor_address, Vendor_GST_No,Vendor_GST_State,Vendor_Registered,[Place Of Supply] ,[Amount in foreign currency],[Amount of tax],[Amount paid in advance],[Date of advance paid],[Amount of refund],[Tax of refund]  ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Document_Type,Doc_Date order by convert(date,Doc_Date,103),Document_No"
            Else
                Qry = "Select Quarter ,month,Vendor_Code ,Vendor_Name ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,case when isnull(Document_Type,'')='D' then -1* Total_Amount else Total_Amount end as Total_Amount ,Abatement_Per, case when isnull(Document_Type,'')='D' then -1* Reverse_Charge_Amount else Reverse_Charge_Amount end as Reverse_Charge_Amount,  Abat_Per , case when isnull(Document_Type,'')='D' then -1* Abatement else Abatement end as Abatement,case when isnull(Document_Type,'')='D' then -1* Net_Amt else Net_Amt end as Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Doc_Date " & headervalue2 & " from (" & Qry & ") lastFinal group by Quarter ,month,Vendor_Code ,Vendor_Name ,Loc_Code ,Location_Desc ,GL_Account_Code ,GL_Account_Name ,Remarks ,AddChargeCode ,Add_desc,SAC_Code ,Invoice_No ,Invoice_Date ,Total_Amount ,Abatement_Per, Reverse_Charge_Amount,  Abat_Per , Abatement,Net_Amt ,Reverse_Charge_Per,[Reverse %],[Purchase_Tax_Invoice],[Purchase_Tax_Invoice_Type]  ,Document_No ,Document_Type,Doc_Date order by convert(date,Doc_Date,103),Document_No"
            End If

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(Qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                If ChkServiceRegister.Checked = True Then
                    If dtgv.Columns.Count > 37 Then
                        For i As Integer = dtgv.Columns.Count - 1 To 37 Step -1
                            Try
                                Dim count As Decimal = Nothing
                                Dim count1 As Decimal = Nothing
                                Dim columname As String = clsCommon.myCstr(dtgv.Columns(i).ColumnName)
                                count = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0.00 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0.00 "))
                                count1 = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0.00 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0.00 "))
                                If count = 0.0 AndAlso count1 = 0.0 Then
                                    dtgv.Columns.RemoveAt(i)
                                End If
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                Else
                    If dtgv.Columns.Count > 26 Then
                        For i As Integer = dtgv.Columns.Count - 1 To 26 Step -1
                            Try
                                Dim count As Decimal = Nothing
                                Dim count1 As Decimal = Nothing
                                Dim columname As String = clsCommon.myCstr(dtgv.Columns(i).ColumnName)
                                count = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0.00 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0.00 "))
                                count1 = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0.00 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0.00 "))
                                If count = 0.0 AndAlso count1 = 0.0 Then
                                    dtgv.Columns.RemoveAt(i)
                                End If
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                End If
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                'For ii As Integer = 1 To gv.Columns.Count - 22
                '    gv.Columns(ii).ReadOnly = True
                '    gv.Columns(ii).IsVisible = False
                'Next
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox2.Enabled = False
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 1 To gv.Columns.Count - 22
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        'gv.Columns("Quarter").IsVisible = True
        'gv.Columns("Quarter").Width = 100
        'gv.Columns("Quarter").HeaderText = "Quarter"
        'gv.Columns("Quarter").ReadOnly = True

        gv.Columns("Quarter").IsVisible = True
        gv.Columns("Quarter").Width = 100
        gv.Columns("Quarter").HeaderText = "Quarter"


        gv.Columns("Month").IsVisible = True
        gv.Columns("Month").Width = 100
        gv.Columns("Month").HeaderText = "Month"


        gv.Columns("Vendor_Code").IsVisible = True
        gv.Columns("Vendor_Code").Width = 100
        gv.Columns("Vendor_Code").HeaderText = "Vendor Code"


        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

        If ChkServiceRegister.Checked = True Then
            gv.Columns("Vendor_address").IsVisible = True
            gv.Columns("Vendor_address").Width = 100
            gv.Columns("Vendor_address").HeaderText = "Vendor Address"

            gv.Columns("Vendor_GST_No").IsVisible = True
            gv.Columns("Vendor_GST_No").Width = 100
            gv.Columns("Vendor_GST_No").HeaderText = "Vendor GST No"

            gv.Columns("Vendor_GST_State").IsVisible = True
            gv.Columns("Vendor_GST_State").Width = 100
            gv.Columns("Vendor_GST_State").HeaderText = "Vendor GST State"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Vendor_Registered").IsVisible = True
            gv.Columns("Vendor_Registered").Width = 100
            gv.Columns("Vendor_Registered").HeaderText = "Vendor Register"

            gv.Columns("Place Of Supply").IsVisible = True
            gv.Columns("Place Of Supply").Width = 100
            gv.Columns("Place Of Supply").HeaderText = "Place Of Supply"

            gv.Columns("Amount in foreign currency").IsVisible = True
            gv.Columns("Amount in foreign currency").Width = 100
            gv.Columns("Amount in foreign currency").HeaderText = "Amount in foreign currency"

            gv.Columns("Amount of tax").IsVisible = True
            gv.Columns("Amount of tax").Width = 100
            gv.Columns("Amount of tax").HeaderText = "Amount of tax"

            gv.Columns("Amount paid in advance").IsVisible = True
            gv.Columns("Amount paid in advance").Width = 100
            gv.Columns("Amount paid in advance").HeaderText = "Amount paid in advance"

            gv.Columns("Date of advance paid").IsVisible = True
            gv.Columns("Date of advance paid").Width = 100
            gv.Columns("Date of advance paid").HeaderText = "Date of advance paid"

            gv.Columns("Amount of refund").IsVisible = True
            gv.Columns("Amount of refund").Width = 100
            gv.Columns("Amount of refund").HeaderText = "Amount of refund"

            gv.Columns("Tax of refund").IsVisible = True
            gv.Columns("Tax of refund").Width = 100
            gv.Columns("Tax of refund").HeaderText = "Tax of refund"

           

        End If

        gv.Columns("Loc_Code").IsVisible = True
        gv.Columns("Loc_Code").Width = 100
        gv.Columns("Loc_Code").HeaderText = "Location"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location Name"

        gv.Columns("GL_Account_Code").IsVisible = True
        gv.Columns("GL_Account_Code").Width = 100
        gv.Columns("GL_Account_Code").HeaderText = "GL Acct Code"
        gv.Columns("GL_Account_Code").ReadOnly = True

        gv.Columns("GL_Account_Name").IsVisible = True
        gv.Columns("GL_Account_Name").Width = 100
        gv.Columns("GL_Account_Name").HeaderText = "GL Acct Name"
        gv.Columns("GL_Account_Name").ReadOnly = True

        gv.Columns("Remarks").IsVisible = True
        gv.Columns("Remarks").Width = 100
        gv.Columns("Remarks").HeaderText = "Remarks"
        gv.Columns("Remarks").ReadOnly = True

        gv.Columns("Doc_Date").IsVisible = True
        gv.Columns("Doc_Date").Width = 100
        gv.Columns("Doc_Date").HeaderText = " Document Date"
        gv.Columns("Doc_Date").FormatString = "{0:d}"
        gv.Columns("Doc_Date").ReadOnly = True

        gv.Columns("AddChargeCode").IsVisible = True
        gv.Columns("AddChargeCode").Width = 100
        gv.Columns("AddChargeCode").HeaderText = "Additional Charge Code"
        gv.Columns("AddChargeCode").ReadOnly = True

        gv.Columns("Add_desc").IsVisible = True
        gv.Columns("Add_desc").Width = 150
        gv.Columns("Add_desc").HeaderText = "Additional Charge Name"
        gv.Columns("Add_desc").ReadOnly = True

        ' Additional Charge SAC Code
        gv.Columns("SAC_Code").IsVisible = True
        gv.Columns("SAC_Code").Width = 150
        gv.Columns("SAC_Code").HeaderText = "Additional Charge SAC Code"
        gv.Columns("SAC_Code").ReadOnly = True

        gv.Columns("Net_Amt").IsVisible = True
        gv.Columns("Net_Amt").Width = 100
        gv.Columns("Net_Amt").HeaderText = "Net Amount"
        gv.Columns("Net_Amt").ReadOnly = True

        gv.Columns("Reverse_Charge_Amount").IsVisible = True
        gv.Columns("Reverse_Charge_Amount").Width = 100
        gv.Columns("Reverse_Charge_Amount").HeaderText = "Taxable Amount"
        gv.Columns("Reverse_Charge_Amount").ReadOnly = True

        gv.Columns("Invoice_No").IsVisible = True
        gv.Columns("Invoice_No").Width = 100
        gv.Columns("Invoice_No").HeaderText = "Invoice No."
        gv.Columns("Invoice_No").ReadOnly = True

        gv.Columns("Reverse_Charge_Per").IsVisible = False
        gv.Columns("Reverse_Charge_Per").VisibleInColumnChooser = True
        gv.Columns("Reverse_Charge_Per").Width = 100
        gv.Columns("Reverse_Charge_Per").HeaderText = "Reverse % "


        gv.Columns("Reverse %").IsVisible = True
        gv.Columns("Reverse %").Width = 100
        gv.Columns("Reverse %").ReadOnly = True
        gv.Columns("Reverse %").HeaderText = "Reverse % "

        gv.Columns("AddChargeCode").ReadOnly = True

        gv.Columns("Abatement_Per").IsVisible = True
        gv.Columns("Abatement_Per").Width = 100
        gv.Columns("Abatement_Per").HeaderText = "Taxable % "
        gv.Columns("Abatement_Per").ReadOnly = True

        gv.Columns("Abat_Per").IsVisible = True
        gv.Columns("Abat_Per").Width = 100
        gv.Columns("Abat_Per").HeaderText = "Abatement % "
        gv.Columns("Abat_Per").ReadOnly = True

        gv.Columns("Abatement").IsVisible = True
        gv.Columns("Abatement").Width = 100
        gv.Columns("Abatement").HeaderText = "Taxable After Abatement"
        gv.Columns("Abatement").ReadOnly = True

        gv.Columns("Document_No").IsVisible = True
        gv.Columns("Document_No").Width = 100
        gv.Columns("Document_No").HeaderText = "Document Code"
        gv.Columns("Document_No").ReadOnly = True

        gv.Columns("Invoice_Date").IsVisible = True
        gv.Columns("Invoice_Date").Width = 100
        gv.Columns("Invoice_Date").HeaderText = "Invoice Date"
        gv.Columns("Invoice_Date").FormatString = "{0:d}"
        gv.Columns("Invoice_Date").ReadOnly = True

        gv.Columns("Total_Amount").IsVisible = True
        gv.Columns("Total_Amount").Width = 100
        gv.Columns("Total_Amount").HeaderText = "Billing Amount"
        gv.Columns("Total_Amount").ReadOnly = True

        gv.Columns("Purchase_Tax_Invoice").IsVisible = True
        gv.Columns("Purchase_Tax_Invoice").Width = 100
        gv.Columns("Purchase_Tax_Invoice").HeaderText = "Purchase Tax Invoice"
        gv.Columns("Purchase_Tax_Invoice").ReadOnly = True

        gv.Columns("Purchase_Tax_Invoice_Type").IsVisible = False
        gv.Columns("Purchase_Tax_Invoice_Type").Width = 100
        gv.Columns("Purchase_Tax_Invoice_Type").HeaderText = "Purchase Tax Invoice Type"
        gv.Columns("Purchase_Tax_Invoice_Type").ReadOnly = True




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.BestFitColumns()
        For Each col As GridViewColumn In gv.Columns
            If clsCommon.CompairString(col.Name, "Total_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Net_Amt") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Reverse_Charge_Amount") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Abatement") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            ElseIf variable1.Contains(col.Name) = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                gv.Columns(col.Name).ReadOnly = True
            End If
            'Dim item1 As New GridViewSummaryItem("Total_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)
            'Dim item2 As New GridViewSummaryItem("Net_Amt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item2)
            'Dim item3 As New GridViewSummaryItem("Reverse_Charge_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item3)
        Next
       


        '=============================================
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0
        'For Each col As GridViewColumn In gv1.Columns
        '    If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Then
        '        Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item1)

        '    ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Or strPivotForAddChargeFinalOutersumQuery.Contains(col.Name) Then
        '        Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item)
        '        'ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
        '        '    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
        '        '    summaryRowItem.Add(item)
        '    End If
        'Next

        '    ==========================================

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        RadGroupBox2.Enabled = True
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub ChkAddChargeAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkAddChargeAll.ToggleStateChanged
        cbgAddCharge.Enabled = Not ChkAddChargeAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub chkQuarterAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkQuarterAll.ToggleStateChanged
        cbgQuarter.Enabled = Not chkQuarterAll.IsChecked
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name] from TSPL_VENDOR_MASTER  Where TSPL_VENDOR_MASTER.Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub
    Sub LoadAdditionalCharges()
        Dim Qry As String = " select distinct AddChargeCode as [Code],tspl_Additional_Charges.Description as [Name] from TSPL_VENDOR_INVOICE_DETAIL left join tspl_Additional_Charges on tspl_Additional_Charges.Code=TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode where AddChargeCode<>'' and isnull(Abatement_Amt,0) > 0 and isnull(Total_Amount,0)>0  "
        cbgAddCharge.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgAddCharge.ValueMember = "Code"
        cbgAddCharge.DisplayMember = "Name"
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
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
    Private Sub RptServiceTaxDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadVendor()
        'ChkAddChargeAll.CheckState = CheckState.Checked
        'chkVendorAll.CheckState = CheckState.Checked
        chkQuarterAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'LoadAdditionalCharges()
        LoadQuarter()
        LoadLocation()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        rbtnLocationAll.IsChecked = True
        If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
            rbtnLocationSelect.IsChecked = True
            For Each str As String In arrLoc.Keys
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                        gvLocation.Rows(ii).Cells("SEL").Value = True
                        gvLocation.Rows(ii).Tag = arrLoc(str)
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub RptServiceTaxDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            'Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name] from TSPL_VENDOR_MASTER Where TSPL_VENDOR_MASTER.Status='N' "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtAdditional__My_Click(sender As Object, e As EventArgs) Handles txtAdditional._My_Click
        Dim qry As String = "select distinct AddChargeCode as [Code],tspl_Additional_Charges.Description as [Name] from TSPL_VENDOR_INVOICE_DETAIL left join tspl_Additional_Charges on tspl_Additional_Charges.Code=TSPL_VENDOR_INVOICE_DETAIL.AddChargeCode where AddChargeCode<>'' and isnull(Abatement_Amt,0) > 0 and isnull(Total_Amount,0)>0  "
        txtAdditional.arrValueMember = clsCommon.ShowMultipleSelectForm("AddCharge", qry, "Code", "Name", txtAdditional.arrValueMember, txtAdditional.arrDispalyMember)
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged

    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub
    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub
    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where ((Is_Section='N' and Is_Sub_Location='N' and Location_Type='Physical' ) or (CSA_Type='Y') ) "
        qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("Document_No").Value)
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strTransType)
        End If
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtAdditional.arrDispalyMember IsNot Nothing AndAlso txtAdditional.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Additional Charge : " + clsCommon.GetMulcallStringWithComma(txtAdditional.arrDispalyMember))
            End If
            If chkQuarterSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgQuarter.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgQuarter.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Quarter : " + strlocName + " "))

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
                clsCommon.MyExportToPDF("Service Tax Report", gv, arrHeader, "Service Tax Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class