'--------Created By Richa 29/01/2015 Against Ticket No BM00000005036
Imports common
Imports System.Data.SqlClient
Public Class ClsDocumentAcceptance

#Region "variables"
    Public DocumentAcceptanceNo As String = String.Empty
    Public DocumentAcceptance_Date As Date? = Nothing
    Public LCCreationNo As String = String.Empty
    Public VendorCode As String = String.Empty
    Public VendorName As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public LCAmount As Double = 0
    Public ShipmentType As String = String.Empty
    Public DueDate As DateTime = Nothing
    Public ReferenceNo As String = String.Empty
    Public TrustReceiptLetter As Integer = 0
    Public F2Letter As Integer = 0
    Public AcceptanceLetter As Integer = 0
    Public Posted As Integer = 0
    Public AcceptanceReferenceNo As String = String.Empty
    Public Is_Create_Auto_GRN As Integer = 0
    Public Is_Create_Auto_MRN As Integer = 0
    Public Is_Create_Auto_SRN As Integer = 0
    Public DocumentAmount As Double = 0
    Public PurchaseOrder_No As String = String.Empty
    Public PurchaseInvoice_No As String = String.Empty
    Public arrDocumentAcceptanceDetail As List(Of ClsDocumentAcceptanceDetail) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo as Code,Convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date,103) as Date,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo,TSPL_DOCUMENT_ACCEPTANCE_MT.VendorCode as [Vendor Code],TSPL_DOCUMENT_ACCEPTANCE_MT.VendorName as [Vendor Name],TSPL_DOCUMENT_ACCEPTANCE_MT.Location_Code as [Location Code],TSPL_DOCUMENT_ACCEPTANCE_MT.Location_Desc as [Location Desc],TSPL_DOCUMENT_ACCEPTANCE_MT.LCAmount,case when TSPL_DOCUMENT_ACCEPTANCE_MT.ShipmentType='P' then 'Partial' else 'Full' end as [Shipment Type],convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DueDate,103) as [Due Date],TSPL_DOCUMENT_ACCEPTANCE_MT.ReferenceNo,case when TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1 then 'Approved' else 'Pending' end as Status  from TSPL_DOCUMENT_ACCEPTANCE_MT"
        str = clsCommon.ShowSelectForm("DocAccCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As ClsDocumentAcceptance, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT where DocumentAcceptanceNo='" + obj.DocumentAcceptanceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.DocumentAcceptanceNo = clsERPFuncationality.GetNextCode(trans, obj.DocumentAcceptance_Date, clsDocType.DocumentAcceptance, "", obj.Location_Code)
            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "DocumentAcceptance_Date", clsCommon.GetPrintDate(obj.DocumentAcceptance_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "LCCreationNo", obj.LCCreationNo)
            clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
            clsCommon.AddColumnsForChange(coll, "VendorName", obj.VendorName)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            clsCommon.AddColumnsForChange(coll, "LCAmount", obj.LCAmount)
            clsCommon.AddColumnsForChange(coll, "ShipmentType", obj.ShipmentType)
            clsCommon.AddColumnsForChange(coll, "DueDate", clsCommon.GetPrintDate(obj.DueDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ReferenceNo", obj.ReferenceNo)
            clsCommon.AddColumnsForChange(coll, "TrustReceiptLetter", obj.TrustReceiptLetter)
            clsCommon.AddColumnsForChange(coll, "AcceptanceLetter", obj.AcceptanceLetter)
            clsCommon.AddColumnsForChange(coll, "AcceptanceReferenceNo", obj.AcceptanceReferenceNo)
            clsCommon.AddColumnsForChange(coll, "F2Letter", obj.F2Letter)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_GRN", obj.Is_Create_Auto_GRN)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_MRN", obj.Is_Create_Auto_MRN)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_SRN", obj.Is_Create_Auto_SRN)
            clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
            clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "DocumentAcceptanceNo", obj.DocumentAcceptanceNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCUMENT_ACCEPTANCE_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCUMENT_ACCEPTANCE_MT", OMInsertOrUpdate.Update, "TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo='" + obj.DocumentAcceptanceNo + "'", trans)
            End If
            ClsDocumentAcceptanceDetail.saveData(obj.arrDocumentAcceptanceDetail, obj.DocumentAcceptanceNo, trans)
            trans.Commit()

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsDocumentAcceptance
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsDocumentAcceptance
        Dim obj As ClsDocumentAcceptance = Nothing
        Dim Arr As List(Of ClsDocumentAcceptance) = Nothing
        Dim qry As String = ""
        qry = "Select TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAmount,TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,TSPL_DOCUMENT_ACCEPTANCE_MT.Is_Create_Auto_GRN,TSPL_DOCUMENT_ACCEPTANCE_MT.Is_Create_Auto_MRN,TSPL_DOCUMENT_ACCEPTANCE_MT.Is_Create_Auto_SRN,TSPL_DOCUMENT_ACCEPTANCE_MT.AcceptanceReferenceNo,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo,TSPL_DOCUMENT_ACCEPTANCE_MT.VendorCode,TSPL_DOCUMENT_ACCEPTANCE_MT.VendorName,TSPL_DOCUMENT_ACCEPTANCE_MT.Location_Code,TSPL_DOCUMENT_ACCEPTANCE_MT.Location_Desc,TSPL_DOCUMENT_ACCEPTANCE_MT.LCAmount,TSPL_DOCUMENT_ACCEPTANCE_MT.ShipmentType,TSPL_DOCUMENT_ACCEPTANCE_MT.DueDate,TSPL_DOCUMENT_ACCEPTANCE_MT.ReferenceNo,TSPL_DOCUMENT_ACCEPTANCE_MT.TrustReceiptLetter,TSPL_DOCUMENT_ACCEPTANCE_MT.F2Letter,TSPL_DOCUMENT_ACCEPTANCE_MT.AcceptanceLetter,TSPL_DOCUMENT_ACCEPTANCE_MT.Posted from TSPL_DOCUMENT_ACCEPTANCE_MT where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo = (select MIN(DocumentAcceptanceNo) from TSPL_DOCUMENT_ACCEPTANCE_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo = (select Max(DocumentAcceptanceNo) from TSPL_DOCUMENT_ACCEPTANCE_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo = (select Min(DocumentAcceptanceNo) from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo = (select Max(DocumentAcceptanceNo) from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDocumentAcceptance()

            obj.DocumentAcceptanceNo = clsCommon.myCstr(dt.Rows(0)("DocumentAcceptanceNo"))
            obj.DocumentAcceptance_Date = clsCommon.myCDate(dt.Rows(0)("DocumentAcceptance_Date"))
            obj.LCCreationNo = clsCommon.myCstr(dt.Rows(0)("LCCreationNo"))
            obj.VendorCode = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
            obj.VendorName = clsCommon.myCstr(dt.Rows(0)("VendorName"))
            obj.LCAmount = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.ShipmentType = clsCommon.myCstr(dt.Rows(0)("ShipmentType"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.TrustReceiptLetter = clsCommon.myCdbl(dt.Rows(0)("TrustReceiptLetter"))
            obj.F2Letter = clsCommon.myCdbl(dt.Rows(0)("F2Letter"))
            obj.AcceptanceLetter = clsCommon.myCdbl(dt.Rows(0)("AcceptanceLetter"))
            obj.ReferenceNo = clsCommon.myCstr(dt.Rows(0)("ReferenceNo"))
            obj.DueDate = clsCommon.myCDate(dt.Rows(0)("DueDate"))
            obj.AcceptanceReferenceNo = clsCommon.myCstr(dt.Rows(0)("AcceptanceReferenceNo"))
            obj.Is_Create_Auto_GRN = clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_GRN"))
            obj.Is_Create_Auto_MRN = clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_MRN"))
            obj.Is_Create_Auto_SRN = clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_SRN"))
            obj.DocumentAmount = clsCommon.myCdbl(dt.Rows(0)("DocumentAmount"))
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            obj.PurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
            obj.arrDocumentAcceptanceDetail = ClsDocumentAcceptanceDetail.getData(obj.DocumentAcceptanceNo, trans)
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
            Dim obj As ClsDocumentAcceptance = ClsDocumentAcceptance.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DocumentAcceptanceNo) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            Dim qry As String = String.Empty
            ''richa agarwal on 16/04/2015 mrn,srn,grn will be created only in case of po
            If clsCommon.myLen(clsCommon.myCstr(obj.PurchaseOrder_No)) > 0 Then
                If obj.Is_Create_Auto_GRN = 1 AndAlso obj.Is_Create_Auto_MRN = 1 Then
                    Dim objGRN As clsGRNHead = ConvertDAToGRN(obj, trans)
                    objGRN.SaveData(objGRN, True, trans)
                    clsGRNHead.PostData(objGRN.GRN_No, trans)
                    qry = "Update TSPL_DOCUMENT_ACCEPTANCE_MT set GRN_No='" & objGRN.GRN_No & "' " & _
                    " where DocumentAcceptanceNo='" + strDocNo + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    Dim obj1 As clsGRNHead = clsGRNHead.GetData(objGRN.GRN_No, NavigatorType.Current, trans)
                    objGRN = Nothing
                    Dim objMRN As clsMRNHead = ConvertDAToMRN(obj1, trans)
                    objMRN.SaveData(objMRN, True, trans)
                    clsMRNHead.PostData(objMRN.MRN_No, trans)
                    qry = "Update TSPL_DOCUMENT_ACCEPTANCE_MT set MRN_No='" & objMRN.MRN_No & "' " & _
                     " where DocumentAcceptanceNo='" + strDocNo + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    obj1 = Nothing

                    Dim obj2 As clsMRNHead = clsMRNHead.GetData(objMRN.MRN_No, NavigatorType.Current, trans)
                    objMRN = Nothing

                    Dim objSRN As clsSRNHead = ConvertDAToSRN(obj2, trans)
                    objSRN.SaveData(objSRN, True, trans)
                    clsSRNHead.PostData("", objSRN.SRN_No, trans, False)

                    qry = "Update TSPL_DOCUMENT_ACCEPTANCE_MT set SRN_No='" & objSRN.SRN_No & "' " & _
                     " where DocumentAcceptanceNo='" + strDocNo + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    objSRN = Nothing
                    obj2 = Nothing
                Else
                    Dim objSRN As clsSRNHead = ConvertDAToDirectSRN(obj, trans)
                    objSRN.SaveData(objSRN, True, trans)
                    clsSRNHead.PostData("", objSRN.SRN_No, trans, False)
                    qry = "Update TSPL_DOCUMENT_ACCEPTANCE_MT set SRN_No='" & objSRN.SRN_No & "' " & _
                     " where DocumentAcceptanceNo='" + strDocNo + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    objSRN = Nothing

                End If
            End If
            qry = "Update TSPL_DOCUMENT_ACCEPTANCE_MT set Posted=1, " & _
                        "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
                        " where DocumentAcceptanceNo='" + strDocNo + "'"
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
            qry = "delete from TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT where DocumentAcceptanceNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Private Shared Function ConvertDAToGRN(ByVal objDA As ClsDocumentAcceptance, ByVal trans As SqlTransaction) As clsGRNHead
        Dim obj As New clsGRNHead()
        obj = New clsGRNHead()
        obj.GRN_Date = objDA.DocumentAcceptance_Date
        obj.Vendor_Code = objDA.VendorCode
        obj.Vendor_Name = objDA.VendorName
        obj.Bill_To_Location = objDA.Location_Code
        obj.BillToLocationName = objDA.Location_Desc
        obj.Discount_Base = objDA.DocumentAmount
        obj.Amount_Less_Discount = objDA.DocumentAmount
        obj.GRN_Total_Amt = objDA.DocumentAmount
        obj.Against_PO = objDA.PurchaseOrder_No
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Ref_No,Dept,Dept_Desc,Item_Type ,Terms_Code,CURRENCY_CODE,ConvRate,PurchaseOrder_No from TSPL_PURCHASE_ORDER_HEAD where  PurchaseOrder_No ='" & clsCommon.myCstr(objDA.PurchaseOrder_No) & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                obj.Ref_No = clsCommon.myCstr(dr("Ref_No"))
                obj.Dept = clsCommon.myCstr(dr("Dept"))
                obj.Dept_Desc = clsCommon.myCstr(dr("Dept_Desc"))
                obj.Terms_Code = clsCommon.myCstr(dr("Terms_Code"))
                obj.CURRENCY_CODE = clsCommon.myCstr(dr("CURRENCY_CODE"))
                obj.ConvRate = clsCommon.myCdbl(dr("ConvRate"))
            Next

        End If



        If (objDA.arrDocumentAcceptanceDetail IsNot Nothing AndAlso objDA.arrDocumentAcceptanceDetail.Count > 0) Then
            obj.Arr = New List(Of clsGRNDetail)
            Dim objTr As clsGRNDetail
            For Each objAccDetail As ClsDocumentAcceptanceDetail In objDA.arrDocumentAcceptanceDetail
                objTr = New clsGRNDetail
                objTr.PO_Id = objAccDetail.PurchaseOrder_No
                objTr.Row_Type = "Item"
                objTr.Line_No = objAccDetail.Line_No
                objTr.Item_Code = objAccDetail.Item_Code
                objTr.Item_Desc = objAccDetail.Item_Desc
                objTr.GRN_Qty = objAccDetail.Qty
                objTr.Unit_code = objAccDetail.Unit_code
                objTr.Item_Cost = objAccDetail.Rate
                objTr.Amount = objAccDetail.Amount
                objTr.Amt_Less_Discount = objAccDetail.Amount
                objTr.Item_Net_Amt = objAccDetail.Amount
                objTr.Location = objDA.Location_Code
                objTr.LocationName = objDA.Location_Desc
                obj.Arr.Add(objTr)

            Next

        End If
        Return obj
    End Function
    Private Shared Function ConvertDAToMRN(ByVal objgrn As clsGRNHead, ByVal trans As SqlTransaction) As clsMRNHead
        Dim obj As New clsMRNHead()
        obj = New clsMRNHead()
        obj.MRN_Date = objgrn.GRN_Date
        obj.Vendor_Code = objgrn.Vendor_Code
        obj.Vendor_Name = objgrn.Vendor_Name
        obj.Ref_No = objgrn.Ref_No
        obj.Bill_To_Location = objgrn.Bill_To_Location
        obj.BillToLocationName = objgrn.BillToLocationName
        obj.Discount_Base = objgrn.Discount_Base
        obj.Amount_Less_Discount = objgrn.Amount_Less_Discount
        obj.MRN_Total_Amt = objgrn.GRN_Total_Amt
        obj.Item_Type = objgrn.Item_Type
        obj.Terms_Code = objgrn.Terms_Code
        obj.Dept = objgrn.Dept
        obj.Dept_Desc = objgrn.Dept_Desc
        obj.Against_PO = objgrn.Against_PO
        obj.Against_GRN = objgrn.GRN_No
        obj.CURRENCY_CODE = objgrn.CURRENCY_CODE
        obj.ConvRate = objgrn.ConvRate

        If (objgrn.Arr IsNot Nothing AndAlso objgrn.Arr.Count > 0) Then
            obj.Arr = New List(Of clsMRNDetail)
            Dim objTr As clsMRNDetail
            For Each objgrnDetail As clsGRNDetail In objgrn.Arr
                objTr = New clsMRNDetail

                objTr.Row_Type = objgrnDetail.Row_Type
                objTr.Line_No = objgrnDetail.Line_No
                objTr.Item_Code = objgrnDetail.Item_Code
                objTr.Item_Desc = objgrnDetail.Item_Desc
                objTr.MRN_Qty = objgrnDetail.GRN_Qty
                objTr.GRN_Id = objgrnDetail.GRN_No
                objTr.Unit_code = objgrnDetail.Unit_code
                objTr.Location = objgrnDetail.Location
                objTr.LocationName = objgrnDetail.LocationName
                objTr.Item_Cost = objgrnDetail.Item_Cost
                objTr.Amount = objgrnDetail.Amount
                objTr.Amt_Less_Discount = objgrnDetail.Amt_Less_Discount
                objTr.Item_Net_Amt = objgrnDetail.Item_Net_Amt

                obj.Arr.Add(objTr)

            Next

        End If
        Return obj
    End Function

    Private Shared Function ConvertDAToSRN(ByVal objmrn As clsMRNHead, ByVal trans As SqlTransaction) As clsSRNHead
        Dim obj As New clsSRNHead()
        obj = New clsSRNHead()
        obj.SRN_Date = objmrn.MRN_Date
        obj.Vendor_Code = objmrn.Vendor_Code
        obj.Vendor_Name = objmrn.Vendor_Name
        obj.Ref_No = objmrn.Ref_No
        obj.Bill_To_Location = objmrn.Bill_To_Location
        obj.BillToLocationName = objmrn.BillToLocationName
        obj.Discount_Base = objmrn.Discount_Base
        obj.Amount_Less_Discount = objmrn.Amount_Less_Discount
        obj.SRN_Total_Amt = objmrn.MRN_Total_Amt
        obj.Item_Type = objmrn.Item_Type
        obj.Terms_Code = objmrn.Terms_Code
        obj.Dept = objmrn.Dept
        obj.Dept_Desc = objmrn.Dept_Desc
        obj.Against_PO = objmrn.Against_PO
        obj.Against_GRN = objmrn.Against_GRN
        obj.Against_MRN = objmrn.MRN_No
        obj.CURRENCY_CODE = objmrn.CURRENCY_CODE
        obj.ConvRate = objmrn.ConvRate
        obj.Inv_Date = objmrn.MRN_Date
        obj.Challan_Date = objmrn.MRN_Date

        If (objmrn.Arr IsNot Nothing AndAlso objmrn.Arr.Count > 0) Then
            obj.Arr = New List(Of clsSRNDetail)
            Dim objTr As clsSRNDetail
            For Each objmrnDetail As clsMRNDetail In objmrn.Arr
                objTr = New clsSRNDetail
                objTr.Row_Type = objmrnDetail.Row_Type
                objTr.Line_No = objmrnDetail.Line_No
                objTr.Item_Code = objmrnDetail.Item_Code
                objTr.Item_Desc = objmrnDetail.Item_Desc
                objTr.SRN_Qty = objmrnDetail.MRN_Qty
                objTr.MRN_Qty = objmrnDetail.MRN_Qty
                objTr.PO_Qty = objmrnDetail.MRN_Qty
                objTr.MRN_Id = objmrnDetail.MRN_No
                objTr.PO_ID = objmrn.Against_PO
                objTr.Unit_code = objmrnDetail.Unit_code
                objTr.Location = objmrnDetail.Location
                objTr.LocationName = objmrnDetail.LocationName
                objTr.Item_Cost = objmrnDetail.Item_Cost
                objTr.Amount = objmrnDetail.Amount
                objTr.Amt_Less_Discount = objmrnDetail.Amt_Less_Discount
                objTr.Item_Net_Amt = objmrnDetail.Item_Net_Amt
                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
    End Function

    Private Shared Function ConvertDAToDirectSRN(ByVal objDA As ClsDocumentAcceptance, ByVal trans As SqlTransaction) As clsSRNHead
        Dim obj As New clsSRNHead()
        obj = New clsSRNHead()
        obj.SRN_Date = objDA.DocumentAcceptance_Date
        obj.Vendor_Code = objDA.VendorCode
        obj.Vendor_Name = objDA.VendorName
        obj.Bill_To_Location = objDA.Location_Code
        obj.BillToLocationName = objDA.Location_Desc
        obj.Discount_Base = objDA.DocumentAmount
        obj.Amount_Less_Discount = objDA.DocumentAmount
        obj.SRN_Total_Amt = objDA.DocumentAmount
        obj.Against_PO = objDA.PurchaseOrder_No
        ' obj.Against_GRN = objmrn.Against_GRN
        'obj.Against_MRN = objmrn.MRN_No
        obj.Inv_Date = objDA.DocumentAcceptance_Date
        obj.Challan_Date = objDA.DocumentAcceptance_Date

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Ref_No,Dept,Dept_Desc,Item_Type ,Terms_Code,CURRENCY_CODE,ConvRate,PurchaseOrder_No from TSPL_PURCHASE_ORDER_HEAD where  PurchaseOrder_No ='" & clsCommon.myCstr(objDA.PurchaseOrder_No) & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                obj.Ref_No = clsCommon.myCstr(dr("Ref_No"))
                obj.Dept = clsCommon.myCstr(dr("Dept"))
                obj.Dept_Desc = clsCommon.myCstr(dr("Dept_Desc"))
                obj.Terms_Code = clsCommon.myCstr(dr("Terms_Code"))
                obj.CURRENCY_CODE = clsCommon.myCstr(dr("CURRENCY_CODE"))
                obj.ConvRate = clsCommon.myCdbl(dr("ConvRate"))
            Next
        End If



        If (objDA.arrDocumentAcceptanceDetail IsNot Nothing AndAlso objDA.arrDocumentAcceptanceDetail.Count > 0) Then
            obj.Arr = New List(Of clsSRNDetail)
            Dim objTr As clsSRNDetail
            For Each objAccDetail As ClsDocumentAcceptanceDetail In objDA.arrDocumentAcceptanceDetail
                objTr = New clsSRNDetail
                objTr.PO_ID = objAccDetail.PurchaseOrder_No
                objTr.Row_Type = "Item"
                objTr.Line_No = objAccDetail.Line_No
                objTr.Item_Code = objAccDetail.Item_Code
                objTr.Item_Desc = objAccDetail.Item_Desc
                objTr.SRN_Qty = objAccDetail.Qty
                objTr.Unit_code = objAccDetail.Unit_code
                objTr.Item_Cost = objAccDetail.Rate
                objTr.Amount = objAccDetail.Amount
                objTr.Amt_Less_Discount = objAccDetail.Amount
                objTr.Item_Net_Amt = objAccDetail.Amount
                objTr.Location = objDA.Location_Code
                objTr.LocationName = objDA.Location_Desc
                obj.Arr.Add(objTr)
            Next
        End If

        Return obj
    End Function
End Class
Public Class ClsDocumentAcceptanceDetail
    Public DocumentAcceptanceNo As String = Nothing
    Public Line_No As Integer = 0
    Public LCRequestNo As String = Nothing
    Public ParticularsofGoodsLCNo As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseInvoice_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public Qty As Double = 0
    Public CURRENCY_CODE As String = Nothing
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of ClsDocumentAcceptanceDetail), ByVal strDocAcceptanceno As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsDocumentAcceptanceDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "DocumentAcceptanceNo", strDocAcceptanceno)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "LCRequestNo", obj.LCRequestNo)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No, True)
                    clsCommon.AddColumnsForChange(coll, "PurchaseInvoice_No", obj.PurchaseInvoice_No, True)
                    clsCommon.AddColumnsForChange(coll, "ParticularsofGoodsLCNo", obj.ParticularsofGoodsLCNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strDocAcceptanceno As String, ByVal trans As SqlTransaction) As List(Of ClsDocumentAcceptanceDetail)
        Try
            Dim arrObj As List(Of ClsDocumentAcceptanceDetail) = Nothing
            Dim obj As ClsDocumentAcceptanceDetail = Nothing
            Dim qry As String = "select * from TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT where DocumentAcceptanceNo='" & strDocAcceptanceno & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsDocumentAcceptanceDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsDocumentAcceptanceDetail()
                    obj.DocumentAcceptanceNo = clsCommon.myCstr(dt.Rows(i)("DocumentAcceptanceNo"))
                    obj.Line_No = clsCommon.myCdbl(dt.Rows(i)("Line_No"))
                    obj.LCRequestNo = clsCommon.myCstr(dt.Rows(i)("LCRequestNo"))
                    obj.ParticularsofGoodsLCNo = clsCommon.myCstr(dt.Rows(i)("ParticularsofGoodsLCNo"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(i)("CURRENCY_CODE"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
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
End Class
