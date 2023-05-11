Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAssetRequisition

#Region "Variables"

    Public ASSET_REQ_CODE As String
    Public REMARKS As String
    Public REQ_DATE As Date
    Public LOCATION_CODE As String
    Public LOCATION_Desc As String
    Public LOCATION_ADDRESS As String
    Public LOCATION_TOWN As String
    Public LOCATION_CHANNEL As String
    Public LOCATION_CONTACT_PERSON As String
    Public LOCATION_TELEPHONE As String
    Public Customer_Code As String
    Public Customer_Name As String
    Public CUST_ADDRESS As String
    Public CUST_TOWN As String
    Public CUST_CHANNEL As String
    Public CUST_CONTACT_PERSON As String
    Public CUST_TELEPHONE As String
    Public MOVE_TYPE As String
    Public CREATED_BY As String
    Public APPROVED_BY As String

    Public POSTED As Boolean
    Public Posting_Date As Date

    '' grid columns
    Public Asset_Code As String
    Public ASSET_DESC As String
    Public QUANTITY As Decimal
    Public ITEM_REMARKS As String
    Public ASSET_SIZE As String
    Public OUTLET_STATUS As String
    Public Shared ObjList As List(Of clsAssetRequisition)
#End Region


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAssetRequisition
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
            qry = "delete from TSPL_ASSET_REQUISITION_DETAIL where ASSET_REQ_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ASSET_REQUISITION where ASSET_REQ_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssetRequisition
        Dim obj As New clsAssetRequisition()
        Dim objtr As New clsAssetRequisition()

        ObjList = New List(Of clsAssetRequisition)

        Dim qry As String = "SELECT TSPL_ASSET_REQUISITION.ASSET_REQ_CODE,TSPL_ASSET_REQUISITION.REQ_DATE,TSPL_ASSET_REQUISITION.MOVE_TYPE," & _
        " TSPL_ASSET_REQUISITION.REMARKS,TSPL_ASSET_REQUISITION.LOCATION_CODE,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ASSET_REQUISITION.LOCATION_ADDRESS,TSPL_ASSET_REQUISITION.LOCATION_TOWN, " & _
        " TSPL_ASSET_REQUISITION.LOCATION_CHANNEL,TSPL_ASSET_REQUISITION.LOCATION_CONTACT_PERSON,TSPL_ASSET_REQUISITION.LOCATION_TELEPHONE, " & _
        " TSPL_ASSET_REQUISITION.Customer_Code AS CUST_CODE,TSPL_CUSTOMER_MASTER.CUSTOMER_NAME,TSPL_CUSTOMER_MASTER.ADD1 AS CUST_ADDRESS," & _
        " TSPL_CUSTOMER_MASTER.City_Code AS  CUST_TOWN,TSPL_CUSTOMER_MASTER.Channel_Code AS CUST_CHANNEL,TSPL_CUSTOMER_MASTER.Contact_Person_Name AS CUST_CONTACT_PERSON, " & _
        " TSPL_CUSTOMER_MASTER.Contact_Person_Phone AS CUST_TELEPHONE,TSPL_ASSET_REQUISITION.CREATED_BY,TSPL_ASSET_REQUISITION.Modify_By from TSPL_ASSET_REQUISITION " & _
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_ASSET_REQUISITION.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " & _
        " LEFT JOIN TSPL_CUSTOMER_MASTER ON  TSPL_ASSET_REQUISITION.Customer_Code=TSPL_CUSTOMER_MASTER.CUST_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND ASSET_REQ_CODE = (select MIN(ASSET_REQ_CODE) from TSPL_ASSET_REQUISITION)"
            Case NavigatorType.Last
                qry += " AND ASSET_REQ_CODE = (select Max(ASSET_REQ_CODE) from TSPL_ASSET_REQUISITION)"
            Case NavigatorType.Next
                qry += " AND ASSET_REQ_CODE = (select Min(ASSET_REQ_CODE) from TSPL_ASSET_REQUISITION where  ASSET_REQ_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND ASSET_REQ_CODE = (select Max(ASSET_REQ_CODE) from TSPL_ASSET_REQUISITION where ASSET_REQ_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND ASSET_REQ_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.ASSET_REQ_CODE = dt.Rows(0)("ASSET_REQ_CODE")
            obj.REMARKS = clsCommon.myCstr(dt.Rows(0)("REMARKS"))
            obj.REQ_DATE = clsCommon.GetPrintDate(dt.Rows(0)("REQ_DATE"), "dd/MMM/yyyy")
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_Desc = clsCommon.myCstr(dt.Rows(0)("LOCATION_Desc"))
            obj.LOCATION_ADDRESS = clsCommon.myCstr(dt.Rows(0)("LOCATION_ADDRESS"))
            obj.LOCATION_TOWN = clsCommon.myCstr(dt.Rows(0)("LOCATION_TOWN"))
            obj.LOCATION_CHANNEL = clsCommon.myCstr(dt.Rows(0)("LOCATION_CHANNEL"))
            obj.LOCATION_CONTACT_PERSON = clsCommon.myCstr(dt.Rows(0)("LOCATION_CONTACT_PERSON"))
            obj.LOCATION_TELEPHONE = clsCommon.myCstr(dt.Rows(0)("LOCATION_TELEPHONE"))

            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("CUST_CODE"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.CUST_ADDRESS = clsCommon.myCstr(dt.Rows(0)("CUST_ADDRESS"))
            obj.CUST_TOWN = clsCommon.myCstr(dt.Rows(0)("CUST_TOWN"))
            obj.CUST_CHANNEL = clsCommon.myCstr(dt.Rows(0)("CUST_CHANNEL"))
            obj.CUST_TELEPHONE = clsCommon.myCstr(dt.Rows(0)("CUST_TELEPHONE"))
            obj.MOVE_TYPE = clsCommon.myCstr(dt.Rows(0)("MOVE_TYPE"))
            'obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            'obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            'obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))

            strCode = dt.Rows(0)("ASSET_REQ_CODE")

            'If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
            '    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            'Else
            '    obj.Posting_Date = Nothing
            'End If
        End If
        qry = "SELECT * FROM TSPL_ASSET_REQUISITION_DETAIL WHERE ASSET_REQ_CODE = '" + strCode + "' ORDER BY Asset_Code"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsAssetRequisition()
                objtr.ASSET_REQ_CODE = clsCommon.myCstr(dr("ASSET_REQ_CODE"))
                objtr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                objtr.ASSET_DESC = clsCommon.myCstr(dr("ASSET_DESC"))
                objtr.QUANTITY = clsCommon.myCdbl(dr("QUANTITY"))
                objtr.ASSET_SIZE = clsCommon.myCstr(dr("ASSET_SIZE"))
                objtr.OUTLET_STATUS = clsCommon.myCstr(dr("OUTLET_STATUS"))
                ObjList.Add(objtr)
            Next
        End If

        clsAssetRequisition.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsAssetRequisition, ByVal objList As List(Of clsAssetRequisition), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True


        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.ASSET_REQ_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.AssetRequisition, "", obj.LOCATION_CODE)
                Else
                    obj.ASSET_REQ_CODE = strCode
                End If
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fixed Asset", "Asset Requisition", obj.LOCATION_CODE, obj.REQ_DATE, trans)
            Dim qry As String = "DELETE FROM TSPL_ASSET_REQUISITION_DETAIL WHERE ASSET_REQ_CODE='" + obj.ASSET_REQ_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.ASSET_REQ_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "ASSET_REQ_CODE", obj.ASSET_REQ_CODE)
            clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
            clsCommon.AddColumnsForChange(coll, "REQ_DATE", clsCommon.GetPrintDate(obj.REQ_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "LOCATION_ADDRESS", obj.LOCATION_ADDRESS)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CHANNEL", obj.LOCATION_CHANNEL)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CONTACT_PERSON", obj.LOCATION_CONTACT_PERSON)
            clsCommon.AddColumnsForChange(coll, "LOCATION_TELEPHONE", obj.LOCATION_TELEPHONE)
            clsCommon.AddColumnsForChange(coll, "LOCATION_TOWN", obj.LOCATION_TOWN)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "MOVE_TYPE", obj.MOVE_TYPE)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "ASSET_REQ_CODE", obj.ASSET_REQ_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_ASSET_REQUISITION where ASSET_REQ_CODE = '" & obj.ASSET_REQ_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_REQUISITION", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.ASSET_REQ_CODE + " Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_REQUISITION", OMInsertOrUpdate.Update, "TSPL_ASSET_REQUISITION.ASSET_REQ_CODE='" + obj.ASSET_REQ_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAssetRequisitionDetail.SaveData(obj.ASSET_REQ_CODE, objList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsAssetRequisition = clsAssetRequisition.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.ASSET_REQ_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_ASSET_REQUISITION set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ASSET_REQ_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class


Public Class clsAssetRequisitionDetail
#Region "Variables"

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssetRequisition), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssetRequisition In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ASSET_REQ_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code)
                clsCommon.AddColumnsForChange(coll, "ASSET_DESC", obj.ASSET_DESC)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", obj.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "ASSET_SIZE", obj.ASSET_SIZE)
                clsCommon.AddColumnsForChange(coll, "OUTLET_STATUS", obj.OUTLET_STATUS)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_REQUISITION_DETAIL", OMInsertOrUpdate.Insert, "TSPL_ASSET_REQUISITION_DETAIL.ASSET_REQ_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function

End Class
