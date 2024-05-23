Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class clsProvisionEntry
    'Public AP_Invoice_No As String = Nothing
    Public KnockOffAmount As Double = 0
    Public EmptyCharge As Double = 0
    Public FixedCharge As Double = 0
    Public FixedAmount As Decimal = 0
    Public Freight_Type As String = Nothing
    Public Doc_No As String = Nothing
    Public Doc_Date As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Desc As String = Nothing
    Public Vendor_Type As String = Nothing
    Public Status As String = Nothing
    Public Ref_Doc_No As String = Nothing
    Public Prov_type As String = Nothing
    Public Amount As Double = 0
    Public Prog_Code As String = Nothing
    Public Prov_Month As String = Nothing
    Public Prov_Year As Integer
    Public Comp_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public isNewEntry As Boolean = False
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public Status_Update_Doc_No As String = Nothing
    Public KnockOffamt As Double = 0
    Public Route_Code As String = String.Empty
    Public Toll_Amt As Double = 0
    Public GL_Location_Seg As String = Nothing

    Public Shared Function LoadMonthName(Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        Dim qry As String = " select DATENAME(month,'1-JAN-2001') as Monthname,MONTH('1-JAN-2001') as monthNumber union all select DATENAME(month,'1-FEB-2001') as Monthname,MONTH('1-FEB-2001') as monthNumber union all select DATENAME(month,'1-MAR-2001') as Monthname,MONTH('1-MAR-2001') as monthNumber union all select DATENAME(month,'1-APR-2001') as Monthname,MONTH('1-APR-2001') as monthNumber union all select DATENAME(month,'1-MAY-2001') as Monthname,MONTH('1-MAY-2001') as monthNumber union all select DATENAME(month,'1-JUN-2001') as Monthname,MONTH('1-JUN-2001') as monthNumber union all select DATENAME(month,'1-JUL-2001') as Monthname,MONTH('1-JUL-2001') as monthNumber union all select DATENAME(month,'1-AUG-2001') as Monthname,MONTH('1-AUG-2001') as monthNumber union all select DATENAME(month,'1-SEP-2001') as Monthname,MONTH('1-SEP-2001') as monthNumber union all select DATENAME(month,'1-OCT-2001') as Monthname,MONTH('1-OCT-2001') as monthNumber union all select DATENAME(month,'1-NOV-2001') as Monthname,MONTH('1-NOV-2001') as monthNumber union all select DATENAME(month,'1-DEC-2001') as Monthname,MONTH('1-DEC-2001') "
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function LoadProvisionGridData(ByVal whrCls As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        Dim qry As String = String.Empty

        qry = " select * from TSPL_PROVISION_ENTRY  where 1=1 "
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function getVendorType(Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable(getVendorTypeQuery, trans)
        Return dt
    End Function

    Public Shared Function getVendorTypeQuery() As String
        ''Balwinder-IF you change any please consider fixed parameter 'CreateJEForProvisionEntry'
        Return " select	'Secondary Transporter' as value union all select 'MCC Lease Vendor' as value union all select 	'Transporter For Fresh Sale' as value union all select 	'Transporter For Product Sale' as value union all select 	'Transporter For Bulk Sale' as value union all select 	'Others' union all select 'Primary Transporter' union all select 'Transporter For Transfer' union all select 'Transporter For CSA Transfer' "
    End Function

    Public Shared Function getProvisionType(Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        Dim qry As String = " select	'Lease' as value	 union all select 'Freight' as value union all select 	'Chilling Charge' as value union all select 	'Others' as value  "
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_PROVISION_ENTRY.Doc_No as [DocNo] ,convert(varchar,TSPL_PROVISION_ENTRY.Doc_Date,103) as [Doc Date] ,TSPL_PROVISION_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_PROVISION_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_PROVISION_ENTRY.Vendor_Type as [Vendor Type] ,TSPL_PROVISION_ENTRY.Status as [Status] ,TSPL_PROVISION_ENTRY.Ref_Doc_No as [Ref Doc No] ,TSPL_PROVISION_ENTRY.Prov_type as [Prov Type] ,TSPL_PROVISION_ENTRY.Toll_Amt as [Toll Amount] ,TSPL_PROVISION_ENTRY.Amount as [Amount] ,TSPL_PROVISION_ENTRY.Prog_Code as [Prog Code] ,TSPL_PROVISION_ENTRY.Prov_Month as [Prov Month] ,TSPL_PROVISION_ENTRY.Prov_Year as [Prov Year] ,TSPL_PROVISION_ENTRY.Comp_Code as [Comp Code] ,TSPL_PROVISION_ENTRY.Created_By as [Created By] ,TSPL_PROVISION_ENTRY.Created_Date as [Created Date] ,TSPL_PROVISION_ENTRY.Modified_By as [Modified By] ,TSPL_PROVISION_ENTRY.Modified_Date as [Modified Date] ,TSPL_PROVISION_ENTRY.isPosted as [Isposted] ,TSPL_PROVISION_ENTRY.Posting_Date as [Posting Date] ,TSPL_PROVISION_ENTRY.Loc_Code as [Loc Code] ,TSPL_PROVISION_ENTRY.Loc_Desc as [Loc Desc] ,TSPL_PROVISION_ENTRY.Status_Update_Doc_No as [Status Update Doc No] ,TSPL_PROVISION_ENTRY.Route_Code as [Route Code]  From TSPL_PROVISION_ENTRY "
            str = clsCommon.ShowSelectForm("DISPTRNS", qry, "DocNo", whrcls, curcode, "TSPL_PROVISION_ENTRY.Doc_Date desc", isButtonClicked, "Doc_Date")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As clsProvisionEntry, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmProvisionEntry, obj.Loc_Code, obj.Doc_Date, trans)
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            If obj.isNewEntry Then
                If clsCommon.CompairString(obj.Prog_Code, clsUserMgtCode.frmMilkShiftEndMCC) = CompairStringResult.Equal AndAlso objCommonVar.ShowMCCFinderInPaymentProcess Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy")), clsDocType.ProvisionEntryMilk, clsDocTransactionType.MccProc, obj.Loc_Code, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
                Else
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy")), clsDocType.ProvisionEntry, "", obj.Loc_Code)
                End If
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error In Doc No Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(obj.Doc_No))
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(obj.Vendor_Code), True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desc", clsCommon.myCstr(obj.Vendor_Desc))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", clsCommon.myCstr(obj.Loc_Code))
            clsCommon.AddColumnsForChange(coll, "Loc_Desc", clsCommon.myCstr(obj.Loc_Desc))
            clsCommon.AddColumnsForChange(coll, "Vendor_Type", clsCommon.myCstr(obj.Vendor_Type))
            clsCommon.AddColumnsForChange(coll, "Status", clsCommon.myCstr(obj.Status))
            clsCommon.AddColumnsForChange(coll, "Status_Update_Doc_No", clsCommon.myCstr(obj.Status_Update_Doc_No))
            clsCommon.AddColumnsForChange(coll, "Ref_Doc_No", clsCommon.myCstr(obj.Ref_Doc_No))
            clsCommon.AddColumnsForChange(coll, "Prov_type", clsCommon.myCstr(obj.Prov_type))
            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
            clsCommon.AddColumnsForChange(coll, "Prog_Code", clsCommon.myCstr(obj.Prog_Code))
            clsCommon.AddColumnsForChange(coll, "Prov_Month", clsCommon.myCstr(obj.Prov_Month))
            clsCommon.AddColumnsForChange(coll, "Prov_Year", clsCommon.myCdbl(obj.Prov_Year))
            clsCommon.AddColumnsForChange(coll, "Route_Code", clsCommon.myCstr(obj.Route_Code))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Freight_Type", clsCommon.myCstr(obj.Freight_Type))
            clsCommon.AddColumnsForChange(coll, "FixedCharge", clsCommon.myCdbl(obj.FixedCharge))
            clsCommon.AddColumnsForChange(coll, "FixedAmount", obj.FixedAmount)
            clsCommon.AddColumnsForChange(coll, "EmptyCharge", clsCommon.myCdbl(obj.EmptyCharge))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            clsCommon.AddColumnsForChange(coll, "Toll_Amt", clsCommon.myCdbl(obj.Toll_Amt))
            clsCommon.AddColumnsForChange(coll, "GL_Location_Seg", obj.GL_Location_Seg, True)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROVISION_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROVISION_ENTRY", OMInsertOrUpdate.Update, "tspl_provision_entry.Doc_No='" + obj.Doc_No + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function SaveData(ByVal StatusUpdateDocNo As String, ByVal arrProvisionDocNo As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim qry As String = " update tspl_provision_Entry set status='No',Status_Update_Doc_No='' where Status_Update_Doc_No='" & StatusUpdateDocNo & "' "
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If arrProvisionDocNo IsNot Nothing AndAlso arrProvisionDocNo.Count > 0 Then
                For i As Integer = 0 To arrProvisionDocNo.Count - 1
                    qry = " update tspl_provision_Entry set status='Yes',Status_Update_Doc_No='" & StatusUpdateDocNo & "' where Doc_No='" & arrProvisionDocNo.Item(i).ToString & "' "
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveDataProvisionKnockOff(ByVal StatusUpdateDocNo As String, ByVal arrProvisionDocNo As List(Of clsProvisionEntry), ByVal trans As SqlTransaction, ByVal dblTotalAmt As Double) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim qry As String = " update tspl_provision_Entry set status='No',Status_Update_Doc_No='' where Status_Update_Doc_No='" & StatusUpdateDocNo & "' "
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If arrProvisionDocNo IsNot Nothing AndAlso arrProvisionDocNo.Count > 0 Then
                For i As Integer = 0 To arrProvisionDocNo.Count - 1
                    qry = " update tspl_provision_Entry set status='Yes',Status_Update_Doc_No='" & StatusUpdateDocNo & "' where Doc_No='" & arrProvisionDocNo.Item(i).ToString & "' "
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
                If dblTotalAmt > 0 Then
                    Dim dblBalance As Double = dblTotalAmt
                    For Each obj As clsProvisionEntry In arrProvisionDocNo
                        If dblBalance > obj.Amount Then
                            dblBalance = dblBalance - obj.Amount
                            obj.KnockOffamt = obj.Amount
                        Else
                            obj.KnockOffamt = dblBalance
                        End If
                        If obj.KnockOffamt > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("Insert into TSPL_PROVISION_ENTRY_KNOCKOFF_APINVOICE values ('" & obj.Doc_No & "','" & StatusUpdateDocNo & "'," & obj.Amount & "," & obj.KnockOffamt & ") ", trans)
                        End If
                    Next
                End If
            End If

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getProvisionDocNo(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of String)
        Dim arr As List(Of String) = New List(Of String)
        Dim qry As String = " select doc_No from TSPL_PROVISION_ENTRY where Status_Update_Doc_No='" & strDocNo & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                arr.Add(clsCommon.myCstr(dt.Rows(i)("Doc_No")))
            Next
        End If
        Return arr
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsProvisionEntry
        Return getData(strCode, navtype, Nothing)
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsProvisionEntry
        Dim obj As New clsProvisionEntry
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From tspl_provision_entry   where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_provision_entry.Doc_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and tspl_provision_entry.Doc_No in (select min(Doc_No ) from tspl_provision_entry where Doc_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and tspl_provision_entry.Doc_No in (select MIN(Doc_No ) from tspl_provision_entry where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and tspl_provision_entry.Doc_No in (select Max(Doc_No ) from tspl_provision_entry where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and tspl_provision_entry.Doc_No in (select Max(Doc_No ) from tspl_provision_entry where Doc_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                obj.Loc_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Desc"))
                obj.Vendor_Type = clsCommon.myCstr(dt.Rows(0)("Vendor_Type"))
                obj.Status_Update_Doc_No = clsCommon.myCstr(dt.Rows(0)("Status_Update_Doc_No"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Ref_Doc_No = clsCommon.myCstr(dt.Rows(0)("Ref_Doc_No"))
                obj.Prov_type = clsCommon.myCstr(dt.Rows(0)("Prov_type"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Toll_Amt = clsCommon.myCdbl(dt.Rows(0)("Toll_Amt"))
                obj.Prog_Code = clsCommon.myCstr(dt.Rows(0)("Prog_Code"))
                obj.Prov_Month = clsCommon.myCstr(dt.Rows(0)("Prov_Month"))
                obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
                obj.Prov_Year = clsCommon.myCdbl(dt.Rows(0)("Prov_Year"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                obj.GL_Location_Seg = clsCommon.myCstr(dt.Rows(0)("GL_Location_Seg"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PROVISION_ENTRY.Loc_Code,TSPL_PROVISION_ENTRY.Doc_Date from TSPL_PROVISION_ENTRY where Doc_no='" + DocNo + "'", tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmProvisionEntry, clsCommon.myCstr(dt.Rows(0)("Loc_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), tran)
            End If

            Dim Qry As String = "select isPosted from tspl_provision_entry where Doc_no='" + DocNo + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, tran)) = 1 Then
                Throw New Exception("Transaction status should be Unposted for delete")
            End If

            Qry = "delete from tspl_provision_Entry where  Doc_No='" & DocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, tran)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal CreateGLentry As Boolean) As Boolean
        Try
            Dim qry As String
            Dim obj As clsProvisionEntry = clsProvisionEntry.getData(strDocNo, NavigatorType.Current, trans)
            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                Throw New Exception("No Provision code found to post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmProvisionEntry, obj.Loc_Code, obj.Doc_Date, trans)
            If obj.isPosted = 1 Then
                Throw New Exception("Provision no " + obj.Doc_No + " is already posted")
            End If

            If CreateGLentry Then
                If clsCommon.CompairString(obj.Prog_Code, "MCC-DISP") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionJournalEntryForTankerDispatch, clsFixedParameterCode.CreateProvisionJournalEntryForTankerDispatch, trans)) = 1 Then
                        CreateJournalEntryTankerDispatch(obj, "", trans)
                    End If
                ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionJournalEntryForSale, clsFixedParameterCode.CreateProvisionJournalEntryForSale, trans)) = 1 Then
                    CreateJournalEntryForSale(obj, "", trans)
                End If
            End If

            qry = " update tspl_provision_entry set isPosted='1',Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Doc_no='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_GATEPASS_master_ProductSale set Provision_No='" & obj.Doc_No & "' where GPCode='" + obj.Ref_Doc_No + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Update TSPL_GATEPASS_master_ProductSale set Provision_Amt='" & obj.Amount & "' where GPCode='" + obj.Ref_Doc_No + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '==========preeti Gupta=========
            Dim InvoiceNo As String = Nothing
            Dim Provision_Amt As Decimal = 0
            Dim Total_Gross_Weight As Decimal = 0
            Dim Gross_Weight As Decimal = 0
            Dim Freight_Charges As Decimal = 0
            qry = " select TSPL_GATEPASS_DETAIL_ProductSale.Invoice_No,TSPL_GATEPASS_MASTER_PRODUCTSALE.Provision_Amt ,TSPL_GATEPASS_MASTER_PRODUCTSALE.Total_Gross_Weight ,TSPL_GATEPASS_DETAIL_ProductSale.Documnet_Amount,TSPL_GATEPASS_DETAIL_ProductSale.Gross_Weight    from TSPL_GATEPASS_DETAIL_ProductSale" &
                 " left join TSPL_GATEPASS_MASTER_PRODUCTSALE on TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode =TSPL_GATEPASS_DETAIL_ProductSale.GPCode  where TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode='" + obj.Ref_Doc_No + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    InvoiceNo = clsCommon.myCstr(dt.Rows(i)("Invoice_No"))
                    Provision_Amt = clsCommon.myCdbl(dt.Rows(i)("Provision_Amt"))
                    Total_Gross_Weight = clsCommon.myCdbl(dt.Rows(i)("Total_Gross_Weight"))
                    Gross_Weight = clsCommon.myCdbl(dt.Rows(i)("Gross_Weight"))
                    If Total_Gross_Weight > 0 Then
                        Freight_Charges = Math.Round(Provision_Amt * Gross_Weight / Total_Gross_Weight, 2)
                        qry = "Update TSPL_SD_SHIPMENT_HEAD set Freight_Charges='" & Freight_Charges & "' where Sale_Invoice_No='" + InvoiceNo + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "Update TSPL_GATEPASS_DETAIL_ProductSale set Provision_Amount='" & Freight_Charges & "' where Invoice_No='" + InvoiceNo + "' and  GPCode='" + obj.Ref_Doc_No + "'  "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    End If
                Next
            End If
            '===============================

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select isPosted from tspl_provision_entry where Doc_no='" + strCode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-EN' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "Update TSPL_GATEPASS_master_ProductSale set Provision_No= null where GPCode='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_GATEPASS_master_ProductSale set Provision_Amt=0 where GPCode='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "update tspl_provision_entry set isPosted='0' where Doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntry(ByVal obj As clsProvisionEntry, ByVal strICode As String, ByVal strVoucherNoRecreateOnly As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim intCounter As Integer = 0
            intCounter = intCounter + 1

            qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Provision_Clearing from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + strICode)
            End If

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + strICode)
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Loc_Code, trans)
            Dim AccDr() As String = {strInvCtrlAC, Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccDr)


            Dim strProvisionalCleanigAC As String = clsCommon.myCstr(dt.Rows(0)("Provision_Clearing"))
            If clsCommon.myLen(strProvisionalCleanigAC) <= 0 Then
                Throw New Exception("Please set Provision Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + strICode + "")
            End If
            strProvisionalCleanigAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strProvisionalCleanigAC, obj.Loc_Code, trans)
            Dim AccCr() As String = {strProvisionalCleanigAC, -1 * Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccCr)

            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoRecreateOnly, trans, obj.Doc_Date, "Against Provisional Entry " + obj.Doc_No, "PR-EN", "Provisional Entry", obj.Doc_No, "", "V", obj.Vendor_Code, obj.Vendor_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntryForSale(ByVal obj As clsProvisionEntry, ByVal strVoucherNoRecreateOnly As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim intCounter As Integer = 0
            intCounter = intCounter + 1


            qry = "select  Freight_Provision as ProvisionAccount,TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account where TSPL_VENDOR_MASTER.vendor_code='" + obj.Vendor_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Vendor Account set for Vendor " + obj.Vendor_Code)
            End If

            Dim strProvisionAcct As String = clsCommon.myCstr(dt.Rows(0)("ProvisionAccount"))
            If clsCommon.myLen(strProvisionAcct) <= 0 Then
                Throw New Exception("Please set Provision Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + " and Vendor : " + obj.Vendor_Code)
            End If
            strProvisionAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strProvisionAcct, obj.Loc_Code, trans)
            Dim AccDr() As String = {strProvisionAcct, -1 * Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccDr)


            Dim FreightAcct As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightProvisionAccount, clsFixedParameterCode.FreightProvisionAccount, trans))
            If clsCommon.myLen(FreightAcct) = 0 Then
                Throw New Exception("Please set Freight Account in Fixed parameter Freight Provision Account .")
            End If
            FreightAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(FreightAcct, obj.Loc_Code, trans)
            Dim AccCr() As String = {FreightAcct, 1 * Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccCr)

            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoRecreateOnly, trans, obj.Doc_Date, "Against Provisional Entry " + obj.Doc_No, "PR-EN", "Provisional Entry", obj.Doc_No, "", "V", obj.Vendor_Code, obj.Vendor_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntryTankerDispatch(ByVal obj As clsProvisionEntry, ByVal strVoucherNoRecreateOnly As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim intCounter As Integer = 0
            intCounter = intCounter + 1


            qry = "select  Freight_Provision as ProvisionAccount,TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account where TSPL_VENDOR_MASTER.vendor_code='" + obj.Vendor_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Vendor Account set for Vendor " + obj.Vendor_Code)
            End If

            Dim strProvisionAcct As String = clsCommon.myCstr(dt.Rows(0)("ProvisionAccount"))
            If clsCommon.myLen(strProvisionAcct) <= 0 Then
                Throw New Exception("Please set Provision Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + " and Vendor : " + obj.Vendor_Code)
            End If
            If clsCommon.myLen(obj.GL_Location_Seg) > 0 Then
                strProvisionAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strProvisionAcct, obj.GL_Location_Seg, True, trans)
            Else
                strProvisionAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strProvisionAcct, obj.Loc_Code, trans)
            End If

            Dim AccDr() As String = {strProvisionAcct, -1 * Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccDr)


            Dim FreightAcct As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightProvisionAccountInward, clsFixedParameterCode.FreightProvisionAccountInward, trans))
            If clsCommon.myLen(FreightAcct) = 0 Then
                Throw New Exception("Please set Freight Account Inward in Fixed parameter Freight Provision Account .")
            End If
            If clsCommon.myLen(obj.GL_Location_Seg) > 0 Then
                FreightAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(FreightAcct, obj.GL_Location_Seg, True, trans)
            Else
                FreightAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(FreightAcct, obj.Loc_Code, trans)
            End If

            Dim AccCr() As String = {FreightAcct, 1 * Math.Round(((obj.Amount)), 2, MidpointRounding.ToEven)}
            ArryLstGLAC.Add(AccCr)
            If clsCommon.myLen(obj.GL_Location_Seg) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.GL_Location_Seg, True, strVoucherNoRecreateOnly, trans, obj.Doc_Date, "Against Provisional Entry " + obj.Doc_No, "PR-EN", "Provisional Entry", obj.Doc_No, "", "V", obj.Vendor_Code, obj.Vendor_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoRecreateOnly, trans, obj.Doc_Date, "Against Provisional Entry " + obj.Doc_No, "PR-EN", "Provisional Entry", obj.Doc_No, "", "V", obj.Vendor_Code, obj.Vendor_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
