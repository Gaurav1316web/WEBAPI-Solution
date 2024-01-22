''created By Richa Agarwal 04 Nov,2019 VIJ/16/10/19-000028,ERO/24/10/19-001077
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmCustomerAdvanceAgainstBooking
    Inherits FrmMainTranScreen
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing
    Dim qry As String = String.Empty
    Private CheckNoOfDaysforCardSaleBooking As Double = 0

    Private Sub frmCustomerAdvanceAgainstBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckNoOfDaysforCardSaleBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckNoOfDaysforCardSaleBooking, clsFixedParameterCode.CheckNoOfDaysforCardSaleBooking, Nothing))
            Gv1.Visible = True
            addNew()
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtLocation.Value + "' "))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location First")
            End If

            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                Throw New Exception("Please select Bank")
            End If

            If clsCommon.myLen(FndPaymentMode.Value) <= 0 Then
                Throw New Exception("Please select Payment Mode")
            End If

            LoadBooking()
            chkread.Checked = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadBooking()
        Try

            qry = " select cast (1 as bit) as Sel, ROW_NUMBER() OVER(ORDER BY final.[Booking No] ASC) as [SL No.],Final.* from " & Environment.NewLine &
            " ( " & Environment.NewLine &
            "  select  convert(varchar,TSPL_BOOKING_MATSER.Document_Date,103) as [Booking Date],TSPL_BOOKING_MATSER.Document_No as [Booking No]," & Environment.NewLine &
            " TSPL_BOOKING_MATSER.location_code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name]," & Environment.NewLine &
            " TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code],tspl_customer_master.Customer_Name as [Customer Name] ,TSPL_BOOKING_PAYMENT_MODE_DETAIL .Amount as [Billed Amount], " & Environment.NewLine &
            " isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode ,'') as [Payment Mode],tspl_customer_master.CURRENCY_CODE " & Environment.NewLine &
            " from TSPL_BOOKING_MATSER" & Environment.NewLine &
            " left outer join TSPL_BOOKING_PAYMENT_MODE_DETAIL on TSPL_BOOKING_MATSER .Document_No=TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No " & Environment.NewLine &
            " left outer join ( Select distinct Cust_Code,Document_No from  TSPL_BOOKING_DETAIL) TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER .Document_No=TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
            " left outer join tspl_customer_master on TSPL_BOOKING_DETAIL .Cust_Code=tspl_customer_master.Cust_Code " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.location_code" & Environment.NewLine &
                  " where isnull(TSPL_BOOKING_MATSER.Booking_type,'')='CD' and convert(date ,TSPL_BOOKING_MATSER.Document_Date ,103) between convert(date ,'" & txtFromDate.Value & "' ,103) and convert(date ,'" & txtToDate.Value & "' ,103) " & Environment.NewLine &
                  " and TSPL_BOOKING_MATSER.IsSampling =0 and ISNULL(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')='' and ISNULL(TSPL_BOOKING_MATSER.Against_Booking_No  ,'')='' and TSPL_BOOKING_MATSER.location_code='" & clsCommon.myCstr(txtLocation.Value) & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & "' and TSPL_BOOKING_MATSER.Is_Cancelled=0 " & Environment.NewLine

            If txtCustomerNo.arrValueMember IsNot Nothing AndAlso txtCustomerNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerNo.arrValueMember) + ")  "
            End If
            qry += "   ) Final "
            If clsCommon.myLen(FndPaymentMode.Value) > 0 Then
                qry += " where final.[Payment Mode] ='" & clsCommon.myCstr(FndPaymentMode.Value) & "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.BestFitColumns()
                Gv1.AllowAddNewRow = False
                For j As Integer = 1 To Gv1.MasterTemplate.Columns.Count - 1
                    Gv1.MasterTemplate.Columns(j).ReadOnly = True
                Next
            Else
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
            End If
            TotalAmount()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        
    End Sub
    Sub TotalAmount()
        Try
            If Gv1.Rows.Count > 0 Then
                Dim dbltotalAmount As Double = 0
                For i As Integer = 0 To Gv1.Rows.Count - 1
                    dbltotalAmount = dbltotalAmount + clsCommon.myCdbl(Gv1.Rows(i).Cells("Billed Amount").Value)
                Next
                lblTotalAmount.Text = dbltotalAmount
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveAndPost_Click(sender As Object, e As EventArgs) Handles btnSaveAndPost.Click
        SaveAndPost()
    End Sub


    Sub SaveAndPost()
        'Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Try
            ValidatedCount = 0
            For Each grow As GridViewRowInfo In Gv1.Rows
                If clsCommon.myCBool(grow.Cells("Sel").Value) Then
                    ValidatedCount = ValidatedCount + 1
                    Exit For
                End If
            Next

            If ValidatedCount > 0 Then
                clsCommon.ProgressBarPercentShow()
                CreateAutoInvoiceAgainstMultipleDispatch()
                'objCommonVar.CurrentUserCode = CurrentUserCode

                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadBooking()
            Else
                Throw New Exception("Please select atleast one Row")
            End If

        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(Me, ex.Message & " At Row No " & (i + 1), Me.Text)
        End Try
    End Sub


    Private Sub CreateAutoInvoiceAgainstMultipleDispatch()
        Dim strCustomer As String = String.Empty
        Dim Main_Location As String = String.Empty
        Dim strPaymentMode As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            Dim dt1 As DataTable = Nothing
            dt1 = clsDBFuncationality.GetDataTable(" select '' Booking_Date,'' as [Booking No],'' as Location_Code,'' as [Location Name],'' as Customer_Code,'' as [Customer Name] ,'' as [Billed Amount],'' as [Payment Mode],'' AS CURRENCY_CODE")
            dt1.Rows.RemoveAt(0)
            If ValidatedCount > 0 Then
                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myCBool(grow.Cells("Sel").Value) Then
                        dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Booking Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Booking No").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Name").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer Name").Value) + "", "" + clsCommon.myCstr(grow.Cells("Billed Amount").Value) + "", " " + clsCommon.myCstr(grow.Cells("Payment Mode").Value) + "", " " + clsCommon.myCstr(grow.Cells("CURRENCY_CODE").Value) + "")
                    End If
                Next
            End If

            Dim dtout As DataTable = Nothing
            dt1.DefaultView.Sort = "Location_Code,Customer_Code,Booking_Date,[Payment Mode]"
            dtout = dt1.DefaultView.ToTable()

            dtmain = clsDBFuncationality.GetDataTable("Select '' as SrNo,'' Booking_Date,'' as [Booking No],'' as Location_Code,'' as [Location Name],'' as Customer_Code,'' as [Customer Name] ,'' as [Billed Amount],'' as [Payment Mode],'' AS CURRENCY_CODE")
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("Booking_Date"))) = CompairStringResult.Equal And clsCommon.CompairString(Main_Location, clsCommon.myCstr(dr("Location_Code"))) = CompairStringResult.Equal And clsCommon.CompairString(strCustomer, clsCommon.myCstr(dr("Customer_Code"))) = CompairStringResult.Equal And clsCommon.CompairString(strPaymentMode, clsCommon.myCstr(dr("Payment Mode"))) = CompairStringResult.Equal Then
                        'InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("Amount"))
                    Else
                        CustomerCount = CustomerCount + 1
                    End If
                    strCustomer = clsCommon.myCstr(dr("Customer_Code"))
                    Main_Location = clsCommon.myCstr(dr("Location_Code"))
                    strdocdate = clsCommon.myCDate(dr("Booking_Date"))
                    strPaymentMode = clsCommon.myCstr(dr("Payment Mode"))


                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("Booking_Date")) + "", "" + clsCommon.myCstr(dr("Booking No")) + "", "" + clsCommon.myCstr(dr("Location_Code")) + "", "" + clsCommon.myCstr(dr("Location Name")) + "", "" + clsCommon.myCstr(dr("Customer_Code")) + "", "" + clsCommon.myCstr(dr("Customer Name")) + "", "" + clsCommon.myCstr(dr("Billed Amount")) + "", "" + clsCommon.myCstr(dr("Payment Mode")) + "", " " + clsCommon.myCstr(dr("CURRENCY_CODE")) + "")
                Next

                InvoiceSaveData()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub InvoiceSaveData()

        Dim strcountno As String = ""
        Dim obj As clsRcptEntryHeader = Nothing
        Dim dblreceiptAmount As Double = 0
        Dim strBookingNumber As String = ""
        Try


            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            For Each dr As DataRow In dtmain.Rows
                j += 1

                clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating Receipt Records " & j & " of Total " & dtmain.Rows.Count)
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("SrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("SrNo"))) <> CompairStringResult.Equal Then

                    obj = New clsRcptEntryHeader()
                    obj.memorndmamt = "0"
                    'obj.Entry_Desc = "Advance created Against Booking for CD"
                    obj.Receipt_Date = clsCommon.myCDate(txtReceiptDate.Value)
                    obj.Receipt_Post_Date = clsCommon.myCDate(txtReceiptDate.Value)
                    obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                    obj.Payment_Code = clsCommon.myCstr(dr("Payment Mode"))
                    obj.Customer_Name = clsCommon.myCstr(dr("Customer Name"))
                    ' obj.Location_GL_Code = clsCommon.myCstr(dr("Location_Code"))
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code where Seg_No = '7' AND GIT='N' and  TSPL_LOCATION_MASTER.Location_Code='" & dr("Location_Code") & "' "))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dr("CURRENCY_CODE"))
                    'obj.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("BASE_CURRENCY_CODE"))
                    ''obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                    'obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_RECEIPT_HEADER.ConvRate,1) as ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.receipt_no=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Receipt_Date), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_RECEIPT_HEADER where Receipt_No ='" & txtDocumentNo.Value & "' )xx", Nothing))
                    'obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))

                    obj.ApplicableFrom = Nothing
                    obj.Receipt_Type = "P"
                    obj.Cust_Code = clsCommon.myCstr(dr("Customer_Code"))
                    dblreceiptAmount = clsCommon.myCdbl(dr("Billed Amount"))
                    obj.IsSalesmanType = "N"
                    obj.SecurityDeposit = "N"
                    obj.CFormRecd = "N"
                    obj.CHECK_PRINT = 0
                    obj.IsApplyDocAuto = 1
                    strBookingNumber = "'" + clsCommon.myCstr(dr("Booking No")) + "',"
                Else
                    dblreceiptAmount = dblreceiptAmount + clsCommon.myCdbl(dr("Billed Amount"))
                    strBookingNumber = strBookingNumber + "'" + clsCommon.myCstr(dr("Booking No")) + "',"
                End If

                strcountno = intCurrInvNo
                obj.Receipt_Amount = dblreceiptAmount
                obj.UnApply_Amt = obj.Receipt_Amount
                obj.Balance_Amt = obj.Receipt_Amount
                obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount

                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("SrNo"))
                End If


                If Not (intCurrInvNo = intNextInvNo) Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        If clsCommon.myLen(strBookingNumber) > 0 Then
                            strBookingNumber = strBookingNumber.Substring(0, strBookingNumber.Length - 1)
                        End If
                        obj.Entry_Desc = "Advance created Against Booking for CD " & strBookingNumber & ""
                        obj.SaveData(obj, True, trans)
                        'clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans)

                        ''clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_MATSER set Against_Receipt_No ='" & obj.Receipt_No & "' where Document_No in (" & strBookingNumber & ") and isnull(TSPL_BOOKING_MATSER.Booking_type,'')='CD' ", trans)
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_PAYMENT_MODE_DETAIL set Against_Receipt_No ='" & obj.Receipt_No & "' where Document_No in (" & strBookingNumber & ") and TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode='" & obj.Payment_Code & "' ", trans)
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If

                intCounter += 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            strcountno = Nothing
            obj = Nothing
        End Try

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            qry = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("AdvanceLocFinder", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        Try
            Dim strWhrcls As String = ""
            qry = clsERPFuncationality.glbankqueryNew(strWhrcls)
            strWhrcls += " and TSPL_bank_master.INACTIVE ='Active' "
            fndBankCode.Value = clsCommon.ShowSelectForm("AdvanceBankFinder", qry, "Code", strWhrcls, fndBankCode.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            addNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub addNew()
        Try
            'txtLocation.Value = ""
            'lblLocation.Text = ""
            fndBankCode.Value = ""
            FndPaymentMode.Value = ""
            lblTotalAmount.Text = ""
            Dim strCardSaleCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Card_no from Tspl_Card_Sale where status=1 and CONVERT(date,Tspl_Card_Sale.FROM_DATE,103)>='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE().AddDays(CheckNoOfDaysforCardSaleBooking), "dd/MMM/yyyy") & "' and CONVERT(date,Tspl_Card_Sale.TO_Date,103)>='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "'   order by convert(datetime,Tspl_Card_Sale.FROM_DATE,103),card_no"))
            If clsCommon.myLen(strCardSaleCode) > 0 Then
                txtFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select cast(from_date as date) from Tspl_Card_Sale where Card_No ='" & strCardSaleCode & "'"))
                txtToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select cast(TO_DATE as date) from Tspl_Card_Sale where Card_No ='" & strCardSaleCode & "'"))
            Else
                txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                txtToDate.Value = clsCommon.GETSERVERDATE()
            End If


           
            txtReceiptDate.Value = clsCommon.GETSERVERDATE()
            txtCustomerNo.arrValueMember = Nothing
            txtCustomerNo.arrDispalyMember = Nothing
            Gv1.DataSource = Nothing
            chkread.Checked = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerNo__My_Click(sender As Object, e As EventArgs) Handles txtCustomerNo._My_Click
        Try
           
            Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No" & Environment.NewLine & _
             " from TSPL_CUSTOMER_MASTER "

            txtCustomerNo.arrValueMember = clsCommon.ShowMultipleSelectForm("ADvMulSelCust", qry, "Code", "Name", txtCustomerNo.arrValueMember, txtCustomerNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''richa agarwal VIJ/08/11/19-000059 
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
            arrHeader.Add(" Location : " + clsCommon.myCstr(txtLocation.Value))

            If Gv1.Rows.Count > 0 Then
                Gv1.Columns("Sel").IsVisible = False
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                LoadBooking()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''richa VIJ/17/12/19-000122
    Private Sub chkread_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkread.ToggleStateChanged
        If chkread.Checked = True Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                Gv1.Rows(i).Cells("Sel").Value = True
            Next
        ElseIf chkread.Checked = False Then

            For i As Integer = 0 To Gv1.Rows.Count - 1
                Gv1.Rows(i).Cells("Sel").Value = False
            Next

        End If
    End Sub

    Private Sub FndPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndPaymentMode._MYValidating
        Try
            Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
            FndPaymentMode.Value = clsCommon.ShowSelectForm("PaymentMode_Selector1", Qry1, "PaymentMode", "", FndPaymentMode.Value, "PaymentMode", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class