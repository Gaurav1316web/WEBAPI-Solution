Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsStandardRateItem

#Region "Variables"

    Public StdRateCode As String
    Public FomeDate As DateTime
    Public Valied_Date As DateTime
    Public Cust_Code As String
    Public Descraption As String
    Public IsValied_Date As Boolean
    Public isCust As Boolean = True
    Public ObjList As List(Of clsStandardRateItemDetail)

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_STANDARD_RATE_ITEM.StdRateCode as [Code] ,TSPL_STANDARD_RATE_ITEM.FomeDate as [From Date] ,TSPL_STANDARD_RATE_ITEM.IsValied_Date as [Isvalid Date] ,TSPL_STANDARD_RATE_ITEM.Valied_Date as [Valid Date] ,TSPL_STANDARD_RATE_ITEM.Cust_Code as [Customer Code] ,TSPL_STANDARD_RATE_ITEM.Descraption as [Description] ,TSPL_STANDARD_RATE_ITEM.Created_By as [Created By] ,TSPL_STANDARD_RATE_ITEM.Created_Date as [Created Date] ,TSPL_STANDARD_RATE_ITEM.Modified_By as [Modified By] ,TSPL_STANDARD_RATE_ITEM.Modified_Date as [Modified Date] ,TSPL_STANDARD_RATE_ITEM.IsCustomer as [IsCustomer]  From TSPL_STANDARD_RATE_ITEM  "
        str = clsCommon.ShowSelectForm("STDRTITMFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsStandardRateItem
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_STANDARD_RATE_ITEM_DETAIL where StdRateCode ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_STANDARD_RATE_ITEM where StdRateCode ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsStandardRateItem
        Dim obj As New clsStandardRateItem()
        Dim objtr As New clsStandardRateItemDetail()
        ObjList = New List(Of clsStandardRateItemDetail)

        Dim qry As String = " SELECT * FROM TSPL_STANDARD_RATE_ITEM " _
                            & " where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and StdRateCode = (select MIN(StdRateCode) from TSPL_STANDARD_RATE_ITEM)"
            Case NavigatorType.Last
                qry += " and StdRateCode = (select Max(StdRateCode) from TSPL_STANDARD_RATE_ITEM)"
            Case NavigatorType.Next
                qry += " and StdRateCode = (select Min(StdRateCode) from TSPL_STANDARD_RATE_ITEM where  StdRateCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and StdRateCode = (select Max(StdRateCode) from TSPL_STANDARD_RATE_ITEM where StdRateCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and StdRateCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.StdRateCode = clsCommon.myCstr(dt.Rows(0)("StdRateCode"))
            obj.FomeDate = clsCommon.myCDate(dt.Rows(0)("FomeDate"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("IsCustomer")), "Y") = CompairStringResult.Equal Then
                obj.isCust = True
            Else
                obj.isCust = False
            End If
            obj.Descraption = clsCommon.myCstr(dt.Rows(0)("Descraption"))
            If clsCommon.myCBool(dt.Rows(0)("IsValied_Date")) Then
                obj.IsValied_Date = clsCommon.myCBool(dt.Rows(0)("IsValied_Date"))
                obj.Valied_Date = clsCommon.myCDate(dt.Rows(0)("Valied_Date"))
            Else
                obj.IsValied_Date = clsCommon.myCBool(dt.Rows(0)("IsValied_Date"))
                obj.Valied_Date = Nothing
            End If
            obj.ObjList = objtr.GetData(obj.StdRateCode, trans)
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsStandardRateItem, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        If isNewEntry Then
            If clsCommon.myLen(obj.StdRateCode) < 1 Then
                obj.StdRateCode = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.StandardRateItem, "", "")
                If (clsCommon.myLen(obj.StdRateCode) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
        End If

        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FomeDate", clsCommon.GetPrintDate(obj.FomeDate, "dd/MMM/yyyy"))
            If obj.Valied_Date.Year > 1900 Then
                clsCommon.AddColumnsForChange(coll, "Valied_Date", clsCommon.GetPrintDate(obj.Valied_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Valied_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "IsValied_Date", obj.IsValied_Date)
            clsCommon.AddColumnsForChange(coll, "Descraption", obj.Descraption)
            If obj.isCust Then
                clsCommon.AddColumnsForChange(coll, "IsCustomer", "Y")
            Else
                clsCommon.AddColumnsForChange(coll, "IsCustomer", "N")
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "StdRateCode", obj.StdRateCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM", OMInsertOrUpdate.Update, "TSPL_STANDARD_RATE_ITEM.StdRateCode='" + obj.StdRateCode + "'", trans)
            End If
            isSaved = isSaved AndAlso clsStandardRateItemDetail.SaveData(obj.StdRateCode, obj.ObjList, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    
End Class

Public Class clsStandardRateItemDetail
#Region "Variables"

    Public StdRateCode As String
    Public Line_No As String
    Public Item_Code As String
    Public Descraption As String
    Public Unit As String
    Public MRP As Decimal
    Public rate As Decimal
    Public PurchasepricebeforeVAT As Decimal
    Public CST As Decimal
    Public VAT As Decimal
    Public exciseonPurhaseRS As Decimal
    Public exciseOnPurchasePercnt As Decimal
    Public Frieghtcharges As Decimal
    Public othercharges As Decimal
    Public totallandingCost As Decimal

        
    Public ObjList As List(Of clsStandardRateItemDetail)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsStandardRateItemDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_STANDARD_RATE_ITEM_DETAIL where StdRateCode ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsStandardRateItemDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "StdRateCode", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Descraption", obj.Descraption)
                    clsCommon.AddColumnsForChange(coll, "Unit ", obj.Unit)
                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.rate)
                    clsCommon.AddColumnsForChange(coll, " PurchasePriceBeforeVATnCST", obj.PurchasepricebeforeVAT)
                    clsCommon.AddColumnsForChange(coll, "CSTonPurchase", obj.CST)
                    clsCommon.AddColumnsForChange(coll, "VATonPurchase", obj.VAT)
                    clsCommon.AddColumnsForChange(coll, "ExciseOnPurchaseRs", obj.exciseonPurhaseRS)
                    clsCommon.AddColumnsForChange(coll, "ExciseOnPurchasePrcnt", obj.exciseOnPurchasePercnt)
                    clsCommon.AddColumnsForChange(coll, "FreightChargesOnPurchaseRs", obj.Frieghtcharges)
                    clsCommon.AddColumnsForChange(coll, "OtherChargesOnPurchase", obj.othercharges)
                    clsCommon.AddColumnsForChange(coll, "TotalLandingCost ", obj.totallandingCost)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STANDARD_RATE_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsStandardRateItemDetail)
        Dim qry As String = " "

        qry += " select * FROM TSPL_STANDARD_RATE_ITEM_DETAIL "
        qry += " where StdRateCode = '" + strDocNo + "'"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsStandardRateItemDetail
        ObjList = New List(Of clsStandardRateItemDetail)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsStandardRateItemDetail()
                objtr.StdRateCode = clsCommon.myCstr(dr("StdRateCode"))
                objtr.Line_No = clsCommon.myCstr(dr("Line_No"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Descraption = clsCommon.myCstr(dr("Descraption"))
                objtr.Unit = clsCommon.myCstr(dr("Unit"))
                objtr.MRP = clsCommon.myCdbl(dr("MRP"))
                objtr.rate = clsCommon.myCdbl(dr("Rate"))

                objtr.PurchasepricebeforeVAT = clsCommon.myCdbl(dr("PurchasePriceBeforeVATnCST"))
                objtr.CST = clsCommon.myCdbl(dr("CSTonPurchase"))
                objtr.VAT = clsCommon.myCdbl(dr("VATonPurchase"))
                objtr.exciseonPurhaseRS = clsCommon.myCdbl(dr("ExciseOnPurchaseRs"))
                objtr.exciseOnPurchasePercnt = clsCommon.myCdbl(dr("ExciseOnPurchasePrcnt"))
                objtr.Frieghtcharges = clsCommon.myCdbl(dr("FreightChargesOnPurchaseRs"))
                objtr.othercharges = clsCommon.myCdbl(dr("OtherChargesOnPurchase"))
                objtr.totallandingCost = clsCommon.myCdbl(dr("TotalLandingCost"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
