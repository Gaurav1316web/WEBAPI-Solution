'--------Created By Richa 19/09/2014 Against Ticket No BM00000003638,BM00000005840
Imports System.Data.SqlClient
Imports common
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class ClsLCRequest
#Region "variables"
    Public LCRequestNo As String = Nothing
    Public LCRequest_Date As Date? = Nothing
    Public Bank_Code As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public LCAmount As Double = 0
    Public FDPer As Double = 0
    Public Bank_Name As String = Nothing
    Public Posted As Integer = 0
    Public LCPeriod As Double = 0
    Public LCPeriodType As String = Nothing
    Public LCType As String = Nothing
    Public FDPeriod As Double = 0
    Public LCExtendPeriod As Double = 0
    Public LCExtendPeriodType As String = Nothing
    Public FDPeriodType As String = Nothing
    Public Drawee_Bank_Code As String = Nothing
    Public Drawee_Bank_Name As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public SGS_Waiver_Ref_no As String = Nothing
    Public SGS_Waiver_Context As String = Nothing
    Public Merchant_Dec_Ref_no As String = Nothing
    Public Merchant_Dec_Context As String = Nothing
    Public AD_Code_No As String = Nothing
    Public Form_No As String = Nothing
    Public Serial_No As String = Nothing
    Public Purpose_Group_Name As String = Nothing
    Public Purpose_Code As String = Nothing
    Public LCExpiryDate As Date? = Nothing
    Public CURRENCY_CODE As String = Nothing
    Public ConvRate As Decimal = Nothing
    Public MixedPaymentDetails As String = Nothing
    Public DeferredPaymentDetails As String = Nothing
    Public PartialShipment As String = Nothing
    Public TransShipment As String = Nothing
    Public AvailableBy As String = Nothing
    Public Place As String = Nothing
    Public DraftsRemarks As String = Nothing
    Public DraftsFrom As String = Nothing
    Public DraftsNoofDays As Integer = 0
    Public DescriptionofGoods As String = String.Empty
    Public PurchaseInvoice_No As String = String.Empty
    Public Against As String = String.Empty
    Public arrLCRequestDetail As List(Of ClsLCRequestDetail) = Nothing
    Public arrLCRequestISSUEDetail As List(Of ClsLCRequestIssueApplicationDetail) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        ' Dim qry As String = "Select TSPL_LC_REQUEST_MT.LCRequestNo as [Code],Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as Date,TSPL_LC_REQUEST_MT.Bank_Code as [Bank Code],TSPL_BANK_MASTER.DESCRIPTION as [Bank Name],TSPL_LC_REQUEST_MT.PurchaseOrder_No [Purchase Order No],TSPL_LC_REQUEST_MT.VendorCode AS [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_LC_REQUEST_MT.LCAmount as [LC Amount],TSPL_LC_REQUEST_MT.FDPer as [FD %],case when TSPL_LC_REQUEST_MT.Posted=1 then 'Approved' else 'Pending' end as Status, TSPL_LC_REQUEST_MT.Created_By as [Created By] ,convert(varchar,TSPL_LC_REQUEST_MT.Created_Date,103) as [Created Date],TSPL_LC_REQUEST_MT.Modified_By as [Modified By],convert(varchar,TSPL_LC_REQUEST_MT.Modified_Date,103) as [Modified Date] from TSPL_LC_REQUEST_MT Left Outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE left Outer Join TSPL_VENDOR_MASTER on TSPL_LC_REQUEST_MT.VendorCode=TSPL_VENDOR_MASTER.Vendor_Code"
        Dim qry As String = "Select TSPL_LC_REQUEST_MT.LCRequestNo as [Code],Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as Date,TSPL_LC_REQUEST_MT.Bank_Code as [Bank Code],TSPL_LC_REQUEST_MT.Bank_Name as [Bank Name],isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as [Purchase Order No],isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as [Proforma Invoice No],TSPL_LC_REQUEST_MT.Location_Code AS [Location Code],TSPL_LC_REQUEST_MT.Location_Desc as [Location Name],TSPL_LC_REQUEST_MT.VendorCode AS [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_LC_REQUEST_MT.Drawee_Bank_Code AS [Drawee Bank Code],TSPL_LC_REQUEST_MT.Drawee_Bank_Name as [Drawee Bank Name],TSPL_LC_REQUEST_MT.LCAmount as [LC Amount],TSPL_LC_REQUEST_MT.FDPer as [FD %],TSPL_LC_REQUEST_MT.LCPeriod AS [LC Period],case when TSPL_LC_REQUEST_MT.LCPeriodType='D' then 'Days'  when TSPL_LC_REQUEST_MT.LCPeriodType='Y' then 'Year'  when TSPL_LC_REQUEST_MT.LCPeriodType='M' then 'Month' end as [LC Period Type],TSPL_LC_REQUEST_MT.FDPeriod  AS [FD Period],case when TSPL_LC_REQUEST_MT.FDPeriodType='D' then 'Days'  when TSPL_LC_REQUEST_MT.FDPeriodType='Y' then 'Year'  when TSPL_LC_REQUEST_MT.FDPeriodType='M' then 'Month' end as [FD Period Type],case when TSPL_LC_REQUEST_MT.Posted=1 then 'Approved' else 'Pending' end as Status from TSPL_LC_REQUEST_MT left Outer Join TSPL_VENDOR_MASTER on TSPL_LC_REQUEST_MT.VendorCode=TSPL_VENDOR_MASTER.Vendor_Code"
        str = clsCommon.ShowSelectForm("LCRequestCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    'Public Shared Function CompanyAddresShowinFooter() As DataTable
    '    Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ',  '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code  else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then ', '+TSPL_STATE_MASTER.STATE_NAME  else '' end + case when LEN(TSPL_COMPANY_MASTER.Pincode)>0 then ', '+TSPL_COMPANY_MASTER.Pincode else ' ' end + case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end   + case when LEN(TSPL_COMPANY_MASTER.Phone1  )>0 then ', '+TSPL_COMPANY_MASTER.Phone1 else ' ' end + case when LEN(TSPL_COMPANY_MASTER.Fax   )>0 then ', '+TSPL_COMPANY_MASTER.Fax else ' ' end + case when LEN(TSPL_COMPANY_MASTER.Email   )>0 then ', '+TSPL_COMPANY_MASTER.Email else ' ' end as companyaddress,TSPL_COMPANY_MASTER.CINNo  as cin,TSPL_COMPANY_MASTER.Pan_No as pan from TSPL_COMPANY_MASTER  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State")
    'End Function
    Public Shared Function SaveData(ByVal obj As ClsLCRequest, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_LC_REQUEST_DETAIL_MT where LCRequestNo='" + obj.LCRequestNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT where LCRequestNo='" + obj.LCRequestNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.LCRequestNo = clsERPFuncationality.GetNextCode(trans, obj.LCRequest_Date, clsDocType.LCRequest, "", obj.Location_Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "LCRequest_Date", clsCommon.GetPrintDate(obj.LCRequest_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode, True)
            clsCommon.AddColumnsForChange(coll, "LCAmount", obj.LCAmount)
            clsCommon.AddColumnsForChange(coll, "FDPer", obj.FDPer)
            clsCommon.AddColumnsForChange(coll, "LCPeriod", obj.LCPeriod)
            clsCommon.AddColumnsForChange(coll, "LCPeriodType", obj.LCPeriodType)
            clsCommon.AddColumnsForChange(coll, "LCType", obj.LCType)
            clsCommon.AddColumnsForChange(coll, "FDPeriod", obj.FDPeriod)
            clsCommon.AddColumnsForChange(coll, "FDPeriodType", obj.FDPeriodType)
            clsCommon.AddColumnsForChange(coll, "LCExtendPeriod", obj.LCExtendPeriod)
            clsCommon.AddColumnsForChange(coll, "LCExtendPeriodType", obj.LCExtendPeriodType)
            clsCommon.AddColumnsForChange(coll, "Drawee_Bank_Code", obj.Drawee_Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Drawee_Bank_Name", obj.Drawee_Bank_Name)
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "DescriptionofGoods", obj.DescriptionofGoods)
            ''SGS Waiver------------------
            clsCommon.AddColumnsForChange(coll, "SGS_Waiver_Ref_no", obj.SGS_Waiver_Ref_no)
            clsCommon.AddColumnsForChange(coll, "SGS_Waiver_Context", obj.SGS_Waiver_Context)
            ''Merchant Declaration'--------
            clsCommon.AddColumnsForChange(coll, "Merchant_Dec_Ref_no", obj.Merchant_Dec_Ref_no)
            clsCommon.AddColumnsForChange(coll, "Merchant_Dec_Context", obj.Merchant_Dec_Context)
            ''---------------------------
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)

            ''Form A2------------------
            clsCommon.AddColumnsForChange(coll, "AD_Code_No", obj.AD_Code_No)
            clsCommon.AddColumnsForChange(coll, "Form_No", obj.Form_No)
            clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
            clsCommon.AddColumnsForChange(coll, "Purpose_Group_Name", obj.Purpose_Group_Name)
            clsCommon.AddColumnsForChange(coll, "Purpose_Code", obj.Purpose_Code)
            ''---------------------------
            ''-LC Issuing 
            clsCommon.AddColumnsForChange(coll, "MixedPaymentDetails", obj.MixedPaymentDetails)
            clsCommon.AddColumnsForChange(coll, "DeferredPaymentDetails", obj.DeferredPaymentDetails)
            clsCommon.AddColumnsForChange(coll, "PartialShipment", obj.PartialShipment)
            clsCommon.AddColumnsForChange(coll, "TransShipment", obj.TransShipment)
            clsCommon.AddColumnsForChange(coll, "AvailableBy", obj.AvailableBy)
            clsCommon.AddColumnsForChange(coll, "Place", obj.Place)
            clsCommon.AddColumnsForChange(coll, "DraftsFrom", obj.DraftsFrom)
            clsCommon.AddColumnsForChange(coll, "DraftsNoofDays", obj.DraftsNoofDays)
            clsCommon.AddColumnsForChange(coll, "DraftsRemarks", obj.DraftsRemarks)
            '----------------------
            clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
            clsCommon.AddColumnsForChange(coll, "Against", obj.Against)
            ' clsCommon.AddColumnsForChange(coll, "LCExpiryDate", clsCommon.GetPrintDate(obj.LCExpiryDate, "dd/MMM/yyyy"), True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "LCRequestNo", obj.LCRequestNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_REQUEST_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_REQUEST_MT", OMInsertOrUpdate.Update, "TSPL_LC_REQUEST_MT.LCRequestNo='" + obj.LCRequestNo + "'", trans)
            End If
            ClsLCRequestDetail.saveData(obj.arrLCRequestDetail, obj.LCRequestNo, trans)
            ClsLCRequestIssueApplicationDetail.saveData(obj.arrLCRequestISSUEDetail, obj.LCRequestNo, trans)
            trans.Commit()

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsLCRequest
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsLCRequest
        Dim obj As ClsLCRequest = Nothing
        Dim Arr As List(Of ClsLCRequest) = Nothing
        Dim qry As String = ""
        qry = "Select TSPL_LC_REQUEST_MT.LCExtendPeriodType,TSPL_LC_REQUEST_MT.LCExtendPeriod,TSPL_LC_REQUEST_MT.PurchaseInvoice_No,TSPL_LC_REQUEST_MT.Against,TSPL_LC_REQUEST_MT.AD_Code_No,TSPL_LC_REQUEST_MT.ConvRate,TSPL_LC_REQUEST_MT.CURRENCY_CODE,TSPL_LC_REQUEST_MT.Form_No, TSPL_LC_REQUEST_MT.Serial_No, TSPL_LC_REQUEST_MT.Purpose_Code, TSPL_LC_REQUEST_MT.Purpose_Group_Name ,TSPL_LC_REQUEST_MT.SGS_Waiver_Ref_no, TSPL_LC_REQUEST_MT.SGS_Waiver_Context,TSPL_LC_REQUEST_MT.Merchant_Dec_Ref_no,TSPL_LC_REQUEST_MT.Merchant_Dec_Context,TSPL_LC_REQUEST_MT.LCRequestNo,TSPL_LC_REQUEST_MT.Location_Code,TSPL_LC_REQUEST_MT.Location_Desc,TSPL_LC_REQUEST_MT.LCRequest_Date,TSPL_LC_REQUEST_MT.Bank_Code,TSPL_LC_REQUEST_MT.Posted,TSPL_LC_REQUEST_MT.PurchaseOrder_No,TSPL_LC_REQUEST_MT.VendorCode,TSPL_LC_REQUEST_MT.LCAmount,TSPL_LC_REQUEST_MT.FDPer,TSPL_LC_REQUEST_MT.Bank_Name as Bank_Name,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_LC_REQUEST_MT.FDPeriod, TSPL_LC_REQUEST_MT.LCPeriod, TSPL_LC_REQUEST_MT.FDPeriodType, TSPL_LC_REQUEST_MT.LCPeriodType,TSPL_LC_REQUEST_MT.LCType, TSPL_LC_REQUEST_MT.Drawee_Bank_Code, TSPL_LC_REQUEST_MT.Drawee_Bank_Name, TSPL_LC_REQUEST_MT.LCExpiryDate,TSPL_LC_REQUEST_MT.MixedPaymentDetails,TSPL_LC_REQUEST_MT.DeferredPaymentDetails,TSPL_LC_REQUEST_MT.PartialShipment,TSPL_LC_REQUEST_MT.TransShipment,TSPL_LC_REQUEST_MT.AvailableBy,TSPL_LC_REQUEST_MT.Place,TSPL_LC_REQUEST_MT.DraftsNoofDays,TSPL_LC_REQUEST_MT.DescriptionofGoods,TSPL_LC_REQUEST_MT.DraftsRemarks,TSPL_LC_REQUEST_MT.DraftsFrom from TSPL_LC_REQUEST_MT left Outer Join TSPL_VENDOR_MASTER on TSPL_LC_REQUEST_MT.VendorCode=TSPL_VENDOR_MASTER.Vendor_Code where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LC_REQUEST_MT.LCRequestNo = (select MIN(LCRequestNo) from TSPL_LC_REQUEST_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_LC_REQUEST_MT.LCRequestNo = (select Max(LCRequestNo) from TSPL_LC_REQUEST_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_LC_REQUEST_MT.LCRequestNo = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_LC_REQUEST_MT.LCRequestNo = (select Min(LCRequestNo) from TSPL_LC_REQUEST_MT where LCRequestNo>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_LC_REQUEST_MT.LCRequestNo = (select Max(LCRequestNo) from TSPL_LC_REQUEST_MT where LCRequestNo<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsLCRequest()
            obj.LCRequestNo = clsCommon.myCstr(dt.Rows(0)("LCRequestNo"))
            obj.LCRequest_Date = clsCommon.myCDate(dt.Rows(0)("LCRequest_Date"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.VendorCode = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
            obj.VendorName = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.LCAmount = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
            obj.FDPer = clsCommon.myCdbl(dt.Rows(0)("FDPer"))
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.FDPeriod = clsCommon.myCdbl(dt.Rows(0)("FDPeriod"))
            obj.LCPeriod = clsCommon.myCdbl(dt.Rows(0)("LCPeriod"))
            obj.FDPeriodType = clsCommon.myCstr(dt.Rows(0)("FDPeriodType"))
            obj.LCPeriodType = clsCommon.myCstr(dt.Rows(0)("LCPeriodType"))
            obj.LCExtendPeriod = clsCommon.myCdbl(dt.Rows(0)("LCExtendPeriod"))
            obj.LCExtendPeriodType = clsCommon.myCstr(dt.Rows(0)("LCExtendPeriodType"))
            obj.LCType = clsCommon.myCstr(dt.Rows(0)("LCType"))
            obj.Drawee_Bank_Code = clsCommon.myCstr(dt.Rows(0)("Drawee_Bank_Code"))
            obj.Drawee_Bank_Name = clsCommon.myCstr(dt.Rows(0)("Drawee_Bank_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            ' obj.LCExpiryDate = clsCommon.myCDate(dt.Rows(0)("LCExpiryDate"))
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            ''SGS Waiver------------------
            obj.SGS_Waiver_Ref_no = clsCommon.myCstr(dt.Rows(0)("SGS_Waiver_Ref_no"))
            obj.SGS_Waiver_Context = clsCommon.myCstr(dt.Rows(0)("SGS_Waiver_Context"))
            ''Merchant Declaration'--------
            obj.Merchant_Dec_Ref_no = clsCommon.myCstr(dt.Rows(0)("Merchant_Dec_Ref_no"))
            obj.Merchant_Dec_Context = clsCommon.myCstr(dt.Rows(0)("Merchant_Dec_Context"))
            ''Form A2------------------
            obj.AD_Code_No = clsCommon.myCstr(dt.Rows(0)("AD_Code_No"))
            obj.Form_No = clsCommon.myCstr(dt.Rows(0)("Form_No"))
            obj.Serial_No = clsCommon.myCstr(dt.Rows(0)("Serial_No"))
            obj.Purpose_Code = clsCommon.myCstr(dt.Rows(0)("Purpose_Code"))
            obj.Purpose_Group_Name = clsCommon.myCstr(dt.Rows(0)("Purpose_Group_Name"))
            ''LC Issuing Application
            obj.MixedPaymentDetails = clsCommon.myCstr(dt.Rows(0)("MixedPaymentDetails"))
            obj.DeferredPaymentDetails = clsCommon.myCstr(dt.Rows(0)("DeferredPaymentDetails"))
            obj.PartialShipment = clsCommon.myCstr(dt.Rows(0)("PartialShipment"))
            obj.TransShipment = clsCommon.myCstr(dt.Rows(0)("TransShipment"))
            obj.AvailableBy = clsCommon.myCstr(dt.Rows(0)("AvailableBy"))
            obj.Place = clsCommon.myCstr(dt.Rows(0)("Place"))
            obj.DraftsFrom = clsCommon.myCstr(dt.Rows(0)("DraftsFrom"))
            obj.DraftsNoofDays = clsCommon.myCdbl(dt.Rows(0)("DraftsNoofDays"))
            obj.DraftsRemarks = clsCommon.myCstr(dt.Rows(0)("DraftsRemarks"))
            ''----------------------
            obj.PurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
            obj.Against = clsCommon.myCstr(dt.Rows(0)("Against"))

            obj.DescriptionofGoods = clsCommon.myCstr(dt.Rows(0)("DescriptionofGoods"))
            obj.arrLCRequestDetail = ClsLCRequestDetail.getData(obj.LCRequestNo, trans)
            obj.arrLCRequestISSUEDetail = ClsLCRequestIssueApplicationDetail.getData(obj.LCRequestNo, trans)
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Loading No not found to Post")
            End If
            Dim obj As ClsLCRequest = ClsLCRequest.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.LCRequestNo) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_LC_REQUEST_MT set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where LCRequestNo='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_LC_REQUEST_DETAIL_MT where LCRequestNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT where LCRequestNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_LC_REQUEST_MT where LCRequestNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
Public Class ClsLCRequestDetail
    Public LCRequestNo As String = String.Empty
    Public Line_No As Double = 0
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public Unit_code As String = String.Empty
    Public PurchaseOrder_No As String = String.Empty
    Public PurchaseInvoice_No As String = String.Empty
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of ClsLCRequestDetail), ByVal strLCRequestNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsLCRequestDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LCRequestNo", strLCRequestNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
                    clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_REQUEST_DETAIL_MT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strLCRequestNo As String, ByVal trans As SqlTransaction) As List(Of ClsLCRequestDetail)
        Try
            Dim arrObj As List(Of ClsLCRequestDetail) = Nothing
            Dim obj As ClsLCRequestDetail = Nothing
            Dim qry As String = "select * from TSPL_LC_REQUEST_DETAIL_MT where LCRequestNo='" & strLCRequestNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsLCRequestDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsLCRequestDetail()
                    obj.LCRequestNo = clsCommon.myCstr(dt.Rows(i)("LCRequestNo"))
                    obj.Line_No = clsCommon.myCdbl(dt.Rows(i)("Line_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(i)("PurchaseOrder_No"))
                    obj.PurchaseInvoice_No = clsCommon.myCstr(dt.Rows(i)("PurchaseInvoice_No"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
    '    Try
    '        Dim isDeleted As Boolean = True
    '        Dim qry As String = "delete from TSPL_QC_Parameter_Detail_BulKSALE where QC_No='" & strQCNo & "'"
    '        isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
    '        Return isDeleted
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function

End Class

''richa 16/1/2015
Public Class ClsLCRequestIssueApplicationDetail
    Public LCRequestNo As String = String.Empty
    Public IssueType As String = String.Empty
    Public IssueTag As String = String.Empty
    Public IssueFieldname As String = String.Empty
    Public IssueDetails As String = String.Empty
    Public SNo As Integer = 0

    Public Shared Function saveData(ByVal arrObj As List(Of ClsLCRequestIssueApplicationDetail), ByVal strLCRequestNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsLCRequestIssueApplicationDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "LCRequestNo", strLCRequestNo)
                    clsCommon.AddColumnsForChange(coll, "IssueType", obj.IssueType)
                    clsCommon.AddColumnsForChange(coll, "IssueTag", obj.IssueTag)
                    clsCommon.AddColumnsForChange(coll, "IssueFieldname", obj.IssueFieldname)
                    clsCommon.AddColumnsForChange(coll, "IssueDetails", obj.IssueDetails)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strLCRequestNo As String, ByVal trans As SqlTransaction) As List(Of ClsLCRequestIssueApplicationDetail)
        Try
            Dim arrObj As List(Of ClsLCRequestIssueApplicationDetail) = Nothing
            Dim obj As ClsLCRequestIssueApplicationDetail = Nothing
            Dim qry As String = "select * from TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT where LCRequestNo='" & strLCRequestNo & "' ORDER BY SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsLCRequestIssueApplicationDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsLCRequestIssueApplicationDetail()
                    obj.LCRequestNo = clsCommon.myCstr(dt.Rows(i)("LCRequestNo"))
                    obj.IssueType = clsCommon.myCstr(dt.Rows(i)("IssueType"))
                    obj.IssueTag = clsCommon.myCstr(dt.Rows(i)("IssueTag"))
                    obj.IssueFieldname = clsCommon.myCstr(dt.Rows(i)("IssueFieldname"))
                    obj.IssueDetails = clsCommon.myCstr(dt.Rows(i)("IssueDetails"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class


''----------------------------dh
Public Class ClsLCCreation
#Region "variables"
    Public LCCreationNo As String = Nothing
    Public LCCreation_Date As Date = Nothing
    Public LCRequestNo As String = Nothing
    Public Bank_Code As String = Nothing
    Public Bank_name As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseInvoice_No As String = Nothing
    Public BenefecieryCode As String = Nothing
    Public BenefecieryName As String = Nothing
    Public LCAmount As Double = 0
    Public AbandonmentNo As Integer = 0
    Public LCNo As String = Nothing
    Public LCCharges As Double = 0
    Public LCType As String = Nothing
    Public LCPeriod As Double = 0
    Public LCPeriodType As String = Nothing
    'Public FDPeriod As Double = 0
    'Public FDPeriodType As String = Nothing
    Public Posted As Integer = 0
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public CURRENCY_CODE As String = Nothing
    Public Status As String = Nothing
    Public ApprovalRequired As String = Nothing
    Public Approved As String = Nothing
    Public ConvRate As Double = 1
    Public LCExpiryDate As Date = Nothing
    Public LCCreationType As String = Nothing
    Public FDType As String = Nothing
    Public FD_No As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_LC_CREATION_MT.LCCreationNo as [Code],Convert(varchar,TSPL_LC_CREATION_MT.LCCreation_Date,103) as Date,TSPL_LC_CREATION_MT.LCRequestNo as [LC Request No],TSPL_LC_CREATION_MT.Bank_Code as [Bank Code],TSPL_LC_CREATION_MT.Bank_Name as [Bank Name],isnull(TSPL_LC_CREATION_MT.PurchaseOrder_No,'') as [Purchase Order No],isnull(TSPL_LC_CREATION_MT.PurchaseInvoice_No,'') as [Proforma Invoice No],TSPL_LC_CREATION_MT.Location_Code AS [Location Code],TSPL_LC_CREATION_MT.Location_Desc as [Location Name],TSPL_LC_CREATION_MT.BenefecieryCode AS [Benefeciery Code],TSPL_LC_CREATION_MT.BenefecieryName as [Benefeciery Name],TSPL_LC_CREATION_MT.LCAmount as [LC Amount],TSPL_LC_CREATION_MT.LCType as [LC Type],TSPL_LC_CREATION_MT.LCPeriod as [LC Period],case when TSPL_LC_CREATION_MT.LCPeriodType='D' then 'Days'  when TSPL_LC_CREATION_MT.LCPeriodType='Y' then 'Year'  when TSPL_LC_CREATION_MT.LCPeriodType='M' then 'Month' end as [LC Period Type],case when TSPL_LC_CREATION_MT.LCCreationType='General' then case when TSPL_LC_CREATION_MT.Posted=1 then 'Approved' else 'Pending' end else 'Cancelled' end  as Status from TSPL_LC_CREATION_MT"
        str = clsCommon.ShowSelectForm("LCCreationCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As ClsLCCreation, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                obj.LCCreationNo = clsERPFuncationality.GetNextCode(trans, obj.LCCreation_Date, clsDocType.LCCreation, "", obj.Location_Code)
            Else
                obj.AbandonmentNo = clsDBFuncationality.getSingleValue("Select AbandonmentNo from TSPL_LC_CREATION_MT where LCCreationNo='" + obj.LCCreationNo + "' ", trans)
                clsLCCreationHistory.SaveDataForHistory(obj.LCCreationNo, clsCommon.myCdbl(obj.AbandonmentNo), trans)
                obj.AbandonmentNo = obj.AbandonmentNo + 1
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "LCCreation_Date", clsCommon.GetPrintDate(obj.LCCreation_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "LCRequestNo", obj.LCRequestNo)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_name)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
            clsCommon.AddColumnsForChange(coll, "BenefecieryCode", obj.BenefecieryCode)
            clsCommon.AddColumnsForChange(coll, "BenefecieryName", obj.BenefecieryName)
            clsCommon.AddColumnsForChange(coll, "LCAmount", obj.LCAmount)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            clsCommon.AddColumnsForChange(coll, "LCType", obj.LCType)
            clsCommon.AddColumnsForChange(coll, "LCPeriod", obj.LCPeriod)
            clsCommon.AddColumnsForChange(coll, "LCCharges", obj.LCCharges)
            clsCommon.AddColumnsForChange(coll, "LCPeriodType", obj.LCPeriodType)
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "AbandonmentNo", obj.AbandonmentNo)
            clsCommon.AddColumnsForChange(coll, "LCCreationType", obj.LCCreationType)
            clsCommon.AddColumnsForChange(coll, "FDType", obj.FDType)
            clsCommon.AddColumnsForChange(coll, "FD_No", obj.FD_No, True)
            clsCommon.AddColumnsForChange(coll, "LCNo", obj.LCNo)
            clsCommon.AddColumnsForChange(coll, "ApprovalRequired", obj.ApprovalRequired)
            'clsCommon.AddColumnsForChange(coll, "LCExpiryDate", clsCommon.GetPrintDate(obj.LCExpiryDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                ''richa 31/12/2014
                If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmLCCreation, trans) Then
                    If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
                        qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                        "values ('LC Creation','" & clsUserMgtCode.FrmLCCreation & "','" & obj.LCCreationNo & "','" & clsCommon.GetPrintDate(obj.LCCreation_Date, "dd-MMM-yyyy") & "','Increase Amount',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                ''-----------------------------
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "LCCreationNo", obj.LCCreationNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_CREATION_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                ''richa 31/12/2014
                If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmLCCreation, trans) Then
                    If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
                        Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.FrmLCCreation & "' and Document_No='" & obj.LCCreationNo & "' ", trans))
                        If intExist = 0 Then
                            qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                         "values ('LC Creation','" & clsUserMgtCode.FrmLCCreation & "','" & obj.LCCreationNo & "','" & clsCommon.GetPrintDate(obj.LCCreation_Date, "dd-MMM-yyyy") & "','Increase Amount',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    End If
                End If
                ''-----------------------------
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LC_CREATION_MT", OMInsertOrUpdate.Update, "TSPL_LC_CREATION_MT.LCCreationNo='" + obj.LCCreationNo + "'", trans)
                End If
                If clsCommon.CompairString(obj.FDType, "N") = CompairStringResult.Equal Or clsCommon.CompairString(obj.FDType, "Ex") = CompairStringResult.Equal Then
                    Dim qry1 As String = "Update TSPL_FIXED_DEPOSIT_MASTER_MT set LCRequestNo='" & obj.LCRequestNo & "' " & _
                    " where Document_No='" + obj.FD_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                End If
                trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsLCCreation
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsLCCreation
        Dim obj As ClsLCCreation = Nothing
        Dim Arr As List(Of ClsLCCreation) = Nothing
        Dim qry As String = ""
        qry = "Select TSPL_LC_CREATION_MT.PurchaseInvoice_No,TSPL_LC_CREATION_MT.FDType,TSPL_LC_CREATION_MT.LCNo,TSPL_LC_CREATION_MT.FD_No,TSPL_LC_CREATION_MT.LCCreationNo,TSPL_LC_CREATION_MT.LCCreationType,TSPL_LC_CREATION_MT.LCCharges,TSPL_LC_CREATION_MT.LCCreation_Date,TSPL_LC_CREATION_MT.LCRequestNo,TSPL_LC_CREATION_MT.Bank_Code,TSPL_LC_CREATION_MT.Bank_Name as [BankName],TSPL_LC_CREATION_MT.Location_Code,TSPL_LC_CREATION_MT.Location_Desc ,TSPL_LC_CREATION_MT.PurchaseOrder_No,TSPL_LC_CREATION_MT.BenefecieryCode,TSPL_LC_CREATION_MT.BenefecieryName,TSPL_LC_CREATION_MT.LCAmount,TSPL_LC_CREATION_MT.LCType,TSPL_LC_CREATION_MT.LCPeriod,TSPL_LC_CREATION_MT.LCPeriodType,TSPL_LC_CREATION_MT.CURRENCY_CODE,TSPL_LC_CREATION_MT.LCExpiryDate,TSPL_LC_CREATION_MT.ConvRate, TSPL_LC_CREATION_MT.Posted from TSPL_LC_CREATION_MT where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LC_CREATION_MT.LCCreationNo = (select MIN(LCCreationNo) from TSPL_LC_CREATION_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_LC_CREATION_MT.LCCreationNo = (select Max(LCCreationNo) from TSPL_LC_CREATION_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_LC_CREATION_MT.LCCreationNo = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_LC_CREATION_MT.LCCreationNo = (select Min(LCCreationNo) from TSPL_LC_CREATION_MT where LCCreationNo>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_LC_CREATION_MT.LCCreationNo = (select Max(LCCreationNo) from TSPL_LC_CREATION_MT where LCCreationNo<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsLCCreation()
            obj.LCCreationNo = clsCommon.myCstr(dt.Rows(0)("LCCreationNo"))
            obj.LCCreation_Date = clsCommon.myCDate(dt.Rows(0)("LCCreation_Date"))
            obj.LCRequestNo = clsCommon.myCstr(dt.Rows(0)("LCRequestNo"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Bank_name = clsCommon.myCstr(dt.Rows(0)("BankName"))
            obj.BenefecieryCode = clsCommon.myCstr(dt.Rows(0)("BenefecieryCode"))
            obj.BenefecieryName = clsCommon.myCstr(dt.Rows(0)("BenefecieryName"))
            obj.LCAmount = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.LCType = clsCommon.myCstr(dt.Rows(0)("LCType"))
            'obj.LCExpiryDate = clsCommon.myCDate(dt.Rows(0)("LCExpiryDate"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            obj.LCPeriod = clsCommon.myCdbl(dt.Rows(0)("LCPeriod"))
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.LCPeriodType = clsCommon.myCstr(dt.Rows(0)("LCPeriodType"))
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            obj.PurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.LCCharges = clsCommon.myCdbl(dt.Rows(0)("LCCharges"))
            obj.LCCreationType = clsCommon.myCstr(dt.Rows(0)("LCCreationType"))
            obj.FDType = clsCommon.myCstr(dt.Rows(0)("FDType"))
            obj.FD_No = clsCommon.myCstr(dt.Rows(0)("FD_No"))
            obj.LCNo = clsCommon.myCstr(dt.Rows(0)("LCNo"))

        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Loading No not found to Post")
            End If
            Dim obj As ClsLCCreation = ClsLCCreation.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.LCCreationNo) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_LC_CREATION_MT set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where LCCreationNo='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_LC_CREATION_MT where LCCreationNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

    

End Class
''richa against Ticket no BM00000003849 on 10/09/2014
Public Class clsLCCreationHistory

    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        If common.clsCommon.MyMessageBoxShow("Do you want to save Amendment history?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
            Try
                Dim qry As String = "insert into TSPL_LC_CREATION_MT_HISTORY (LCCreationNo,LCCreation_Date,LCRequestNo,Bank_Code,Bank_Name,PurchaseOrder_No,BenefecieryCode,BenefecieryName,Location_Code,Location_Desc,CURRENCY_CODE,ConvRate,LCAmount,LCCharges,LCType,LCPeriod,LCPeriodType,LCExpiryDate,Posted,Posting_Date,ApprovalRequired,Approved,LCCreationType,Status,Comp_Code,Created_By,Created_Date,Modified_By,Modified_Date,AbandonmentNo,AbandonmentDate,User_Code,FDType,FD_No,LCNo,PurchaseInvoice_No) select LCCreationNo,LCCreation_Date,LCRequestNo,Bank_Code,Bank_Name,PurchaseOrder_No,BenefecieryCode,BenefecieryName,Location_Code,Location_Desc,CURRENCY_CODE,ConvRate,LCAmount,LCCharges,LCType,LCPeriod,LCPeriodType,LCExpiryDate,Posted,Posting_Date,ApprovalRequired,Approved,LCCreationType,Status,Comp_Code,Created_By,Created_Date,Modified_By,Modified_Date," & intAmbandentNo & ",'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") & "','" & objCommonVar.CurrentUserCode & "',FDType,FD_No,LCNo,PurchaseInvoice_No from TSPL_LC_CREATION_MT where LCCreationNo='" + clsCommon.myCstr(strCode) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

End Class
