Imports common
Imports System.Data.SqlClient
Public Class ClsCardSale
#Region "Variables"
    Public Card_No As String = Nothing
    Public Card_Date As DateTime? = Nothing
    Public FROM_DATE As DateTime? = Nothing
    Public TO_DATE As DateTime? = Nothing
    Public No_Of_Days As Decimal
    Public Free_Days As Decimal
    Public isFirstSpell As Boolean = False
    Public isSecondSpell As Boolean = False
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region
    Public Function SaveData(ByVal obj As ClsCardSale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As ClsCardSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Card_Date", clsCommon.GetPrintDate(obj.Card_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "No_Of_Days", obj.No_Of_Days)
            clsCommon.AddColumnsForChange(coll, "Free_Day", obj.Free_Days)
            clsCommon.AddColumnsForChange(coll, "isFirstSpell", IIf(obj.isFirstSpell = True, 1, 0))
            clsCommon.AddColumnsForChange(coll, "isSecondSpell", IIf(obj.isSecondSpell = True, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Card_No = clsERPFuncationality.GetNextCode(trans, obj.Card_Date, clsDocType.CardSale, "", "")
                clsCommon.AddColumnsForChange(coll, "Card_No", obj.Card_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Card_Sale", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Card_Sale", OMInsertOrUpdate.Update, "Card_No='" + obj.Card_No + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCardNo As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As ClsCardSale
        Dim obj As ClsCardSale = Nothing
        Dim qry As String = " SELECT tspl_Card_sale.* FROM tspl_Card_sale " & _
                            " where  2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_Card_sale.Card_No=(select MIN(Card_No) from tspl_Card_sale Where 1=1 )"
            Case NavigatorType.Last
                qry += " and tspl_Card_sale.Card_No=(select Max(Card_No) from tspl_Card_sale Where 1=1 )"
            Case NavigatorType.Next
                qry += " and tspl_Card_sale.Card_No=(select Min(Card_No) from tspl_Card_sale where Card_No > '" + strCardNo + "' )"
            Case NavigatorType.Previous
                qry += " and tspl_Card_sale.Card_No=(select Max(Card_No) from tspl_Card_sale where Card_No < '" + strCardNo + "' )"
            Case NavigatorType.Current
                qry += " and tspl_Card_sale.Card_No='" + strCardNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCardSale()
            obj.Card_No = clsCommon.myCstr(dt.Rows(0)("Card_No"))
            obj.Card_Date = clsCommon.myCDate(dt.Rows(0)("Card_Date"))
            obj.FROM_DATE = clsCommon.myCDate(dt.Rows(0)("FROM_DATE"))
            obj.TO_DATE = clsCommon.myCDate(dt.Rows(0)("TO_DATE"))
            obj.isFirstSpell = clsCommon.myCBool(dt.Rows(0)("isFirstSpell"))
            obj.isSecondSpell = clsCommon.myCBool(dt.Rows(0)("isSecondSpell"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strCardNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim obj As ClsCardSale
            If clsCommon.myLen(strCardNo) > 0 Then
                obj = ClsCardSale.GetData(strCardNo, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from tspl_card_sale WHERE card_No ='" + obj.Card_No + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrCardNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrCardNo) <= 0) Then
                Throw New Exception(" Card No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("tspl_Card_Sale", "Card_No", StrCardNo, "Status=1", trans)
            Dim obj As ClsCardSale = ClsCardSale.GetData(StrCardNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Card_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Card_Date, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update tspl_Card_Sale set Status='1' where card_No='" & StrCardNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Card_No as Code,convert(varchar,Card_Date,103) as [Card Date],  FROM_DATE as [From Date] , TO_DATE as [To Date], STATUS as Status, case when  isFirstSpell = 1 then '1st Spell' when isSecondSpell =1 then '2nd Spell'  else '' end [Spell] from tspl_Card_Sale  "
        str = clsCommon.ShowSelectForm("CardSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

End Class
