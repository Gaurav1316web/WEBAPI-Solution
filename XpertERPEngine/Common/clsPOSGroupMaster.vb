Imports System.Data.SqlClient

Public Class clsPOSGroupMaster

#Region "Variables"
    Public GROUP_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public LEVEL As Integer = Nothing
    Public DOC_DATE As Date = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsPOSGroupMaster, ByVal IsNew As Boolean) As Boolean
        Dim result As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            result = SaveData(obj, IsNew, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return result

    End Function
    Public Function SaveData(ByVal obj As clsPOSGroupMaster, ByVal IsNew As Boolean, ByVal trans As SqlTransaction) As Boolean


        Dim result As Boolean = False

        If clsCommon.myLen(obj.GROUP_CODE) = 0 Then
            Dim qry As String = "SELECT MAX(GROUP_CODE) FROM TSPL_POS_GROUP_MASTER WHERE GROUP_CODE LIKE '%GC%'"
            obj.GROUP_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(obj.GROUP_CODE) <= 0 Then
                obj.GROUP_CODE = "GC0000001"
            Else
                obj.GROUP_CODE = clsCommon.incval(obj.GROUP_CODE)
            End If
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "GROUP_CODE", obj.GROUP_CODE)
        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
        clsCommon.AddColumnsForChange(coll, "LEVEL", clsCommon.myCstr(obj.LEVEL))
        clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "MODIFY_DATE", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
        clsCommon.AddColumnsForChange(coll, "MODIFY_BY", objCommonVar.CurrentUserCode)
        If IsNew = False Then

            If IsValid(obj, trans) Then
                result = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_POS_GROUP_MASTER", OMInsertOrUpdate.Update, "GROUP_CODE='" + obj.GROUP_CODE + "'", trans)
            End If
        Else
            If IsValid(obj, trans) Then
                clsCommon.AddColumnsForChange(coll, "CREATE_DATE", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                clsCommon.AddColumnsForChange(coll, "CREATE_BY", objCommonVar.CurrentUserCode)
                result = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_POS_GROUP_MASTER", OMInsertOrUpdate.Insert, "", trans)
            End If
        End If
        Return result
    End Function

    Public Function IsValid(ByVal obj As clsPOSGroupMaster, ByVal tran As SqlTransaction) As Boolean
        Dim qry = "SELECT 1 FROM TSPL_POS_GROUP_MASTER WHERE LEVEL='" + clsCommon.myCstr(obj.LEVEL) + "' and group_code <> '" + obj.GROUP_CODE + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt.Rows.Count > 0 Then
            Throw New Exception("This Level Already Exists")
            Return False
        Else
            Return True
        End If
    End Function


    Public Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsPOSGroupMaster
        Dim qry = "SELECT * FROM TSPL_POS_GROUP_MASTER WHERE 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and GROUP_CODE =  (select MIN(GROUP_CODE) from TSPL_POS_GROUP_MASTER)"
            Case NavigatorType.Last
                qry += " and GROUP_CODE = (select Max(GROUP_CODE) from TSPL_POS_GROUP_MASTER)"
            Case NavigatorType.Next
                qry += " and GROUP_CODE = (select Min(GROUP_CODE) from TSPL_POS_GROUP_MASTER where  GROUP_CODE>'" + strDocumentNo + "')"
            Case NavigatorType.Previous
                qry += " and GROUP_CODE = (select Max(GROUP_CODE) from TSPL_POS_GROUP_MASTER where GROUP_CODE<'" + strDocumentNo + "')"
            Case NavigatorType.Current
                qry += " and GROUP_CODE = '" + strDocumentNo + "'"
        End Select
        qry += " ORDER BY LEVEL ASC"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim obj As New clsPOSGroupMaster

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                obj.GROUP_CODE = clsCommon.myCstr(dr("GROUP_CODE"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.LEVEL = clsCommon.myCdbl(dr("LEVEL"))
                obj.DOC_DATE = clsCommon.myCstr(dr("DOC_DATE"))
            Next
        End If

        Return obj
    End Function
    Public Shared Function Delete(ByVal DocCode As String) As Boolean
        Dim Check As Boolean = False
        Try
            Dim IsEXisit As Integer = 0
            Dim qry1 = "SELECT COUNT(Pos_Type) FROM TSPL_SECONDARY_CUSTOMER_MASTER WHERE POS_TYPE='" + DocCode + "'"
            IsEXisit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1))

            Dim qry2 = "SELECT COUNT(login_type) FROM TSPL_USER_MASTER WHERE Login_Type='" + DocCode + "'"
            IsEXisit += clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry2))

            If IsEXisit <= 0 Then
                Dim qry = "DELETE  TSPL_POS_GROUP_MASTER WHERE GROUP_CODE='" + DocCode + "'"
                Check = clsDBFuncationality.ExecuteNonQuery(qry)
            Else
                Throw New Exception("Can't deleted,Record already in used.")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Check

    End Function

    Public Shared Function getGroupFinder(ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "SELECT GROUP_CODE as Code,[Description],[Level] FROM  TSPL_POS_GROUP_MASTER"
        str = clsCommon.ShowSelectForm("TSPL_POS_GROUP_MASTER", qry, "Code", "", "", "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getGroupFinderName(ByVal Code As String) As String
        Dim str As String = ""
        Dim qry As String = "SELECT DESCRIPTION as Name FROM  TSPL_POS_GROUP_MASTER WHERE GROUP_CODE='" + Code + "'"
        str = clsDBFuncationality.getSingleValue(qry)
        Return str
    End Function
End Class


Public Class clsPOSCommissionMapping
#Region "Variables"
    Public GROUP_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public LEVEL As Integer = Nothing
    Public DOC_DATE As DateTime = Nothing
    Public ITEM_PRICE_ID As Integer = 0
    Public COMMISSION_AMOUNT As Double = 0
    Public COMMISSION_TYPE As String = Nothing
    Public BASIC_RATE As Double = 0
    Public arr As List(Of clsPOSCommissionMapping) = Nothing

#End Region



    Public Shared Function GetData(ByVal PriceId As Integer) As DataTable


        'Dim qry = "SELECT TSPL_POS_GROUP_MASTER.GROUP_CODE AS [Group Code],TSPL_POS_GROUP_MASTER.DESCRIPTION as [POS Type],TSPL_POS_GROUP_MASTER.LEVEL as [Level],'Rupees' as [Comm. Type],isnull(TSPL_POS_COMMISSION_MAPPING.COMMISSION,0) as [Comm. Amount],isnull(TSPL_POS_COMMISSION_MAPPING.BASIC_RATE,0) as [Basic Rate] " & _
        '           " FROM TSPL_POS_GROUP_MASTER " & _
        '           " LEFT JOIN TSPL_POS_COMMISSION_MAPPING ON TSPL_POS_GROUP_MASTER.GROUP_CODE=TSPL_POS_COMMISSION_MAPPING.GROUP_CODE  " & _
        '           " LEFT JOIN TSPL_ITEM_PRICE_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Price_ID=TSPL_POS_COMMISSION_MAPPING.ITEM_PRICE_ID ORDER BY TSPL_POS_GROUP_MASTER.LEVEL " ' WHERE TSPL_ITEM_PRICE_MASTER.Item_Price_ID='" + clsCommon.myCstr(PriceId) + "' ORDER BY TSPL_POS_GROUP_MASTER.LEVEL "

        Dim Qry1 = "IF  EXISTS(SELECT * FROM TSPL_POS_COMMISSION_MAPPING WHERE TSPL_POS_COMMISSION_MAPPING.ITEM_PRICE_ID='" + clsCommon.myCstr(PriceId) + "')   " & _
       " BEGIN " & _
          "  SELECT TSPL_POS_GROUP_MASTER.GROUP_CODE AS [Group Code],TSPL_POS_GROUP_MASTER.DESCRIPTION as [POS Type],TSPL_POS_GROUP_MASTER.LEVEL as [Level],'Rupees' as [Comm. Type],isnull(TSPL_POS_COMMISSION_MAPPING.COMMISSION,0) as [Comm. Amount],isnull(TSPL_POS_COMMISSION_MAPPING.BASIC_RATE,0) as [Basic Rate]  FROM TSPL_POS_GROUP_MASTER  LEFT JOIN TSPL_POS_COMMISSION_MAPPING ON TSPL_POS_GROUP_MASTER.GROUP_CODE=TSPL_POS_COMMISSION_MAPPING.GROUP_CODE   LEFT JOIN TSPL_ITEM_PRICE_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Price_ID=TSPL_POS_COMMISSION_MAPPING.ITEM_PRICE_ID WHERE TSPL_POS_COMMISSION_MAPPING.ITEM_PRICE_ID='" + clsCommon.myCstr(PriceId) + "'  ORDER BY TSPL_POS_GROUP_MASTER.LEVEL   " & _
       " End   " & _
        "    ELSE   " & _
       " BEGIN   " & _
          "  SELECT TSPL_POS_GROUP_MASTER.GROUP_CODE AS [Group Code],TSPL_POS_GROUP_MASTER.DESCRIPTION as [POS Type],TSPL_POS_GROUP_MASTER.LEVEL as [Level],'Rupees' as [Comm. Type],0 as [Comm. Amount],0 as [Basic Rate]  FROM TSPL_POS_GROUP_MASTER    ORDER BY TSPL_POS_GROUP_MASTER.LEVEL   " & _
               " End"
        Dim dt = clsDBFuncationality.GetDataTable(Qry1)
        Return dt
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsPOSCommissionMapping), Optional ByVal PriceId As String = Nothing, Optional ByVal isNew As Boolean = True) As Boolean
        Dim isSave As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSave = SaveData(arr, trans, PriceId, isNew)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSave
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsPOSCommissionMapping), ByVal trans As SqlTransaction, Optional ByVal PriceId As String = Nothing, Optional ByVal isNew As Boolean = True) As Boolean
        Dim isSave As Boolean = False

        'Dim qry As String = "Delete TSPL_POS_COMMISSION_MAPPING where ITEM_PRICE_ID='" + clsCommon.myCstr(PriceId) + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)
        ' Dim arr As New List(Of clsPOSCommissionMapping)
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            Dim qry As String = "delete from TSPL_POS_COMMISSION_MAPPING where ITEM_PRICE_ID = '" + clsCommon.myCstr(PriceId) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsPOSCommissionMapping In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ITEM_PRICE_ID", clsCommon.myCstr(PriceId))
                clsCommon.AddColumnsForChange(coll, "GROUP_CODE", clsCommon.myCstr(obj.GROUP_CODE))
                clsCommon.AddColumnsForChange(coll, "COMMISSION_TYPE", clsCommon.myCstr(obj.COMMISSION_TYPE))
                clsCommon.AddColumnsForChange(coll, "COMMISSION", clsCommon.myCstr(obj.COMMISSION_AMOUNT))
                clsCommon.AddColumnsForChange(coll, "BASIC_RATE", clsCommon.myCstr(obj.BASIC_RATE))
                clsCommon.AddColumnsForChange(coll, "CREATED_BY", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "CREATED_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "MODIFIED_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "MODIFIED_BY", objCommonVar.CurrentUserCode)
                isSave = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_POS_COMMISSION_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next

        End If
        Return isSave
    End Function
End Class