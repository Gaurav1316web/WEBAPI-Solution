'==============BM00000003063,Updated By Rohit===========
Imports System.Data.SqlClient
Imports common

Public Class clsSerializeInvenotry
#Region "Variables"
    Public Code As String = ""
    Public Parent_Line_No As Integer = Nothing
    Public Line_No As Integer = Nothing
    Public Auto_Sr_No As String = ""
    Public Auto_BIN_No As String = ""
    Public Item_Code As String = ""
    Public Document_Code As String = ""
    Public Document_Type As String = ""
    Public In_Out_Type As String = ""
    Public Against_Inv_Movement_Trans_Id As Integer = 0
    Public Location_Code As String = ""
    Public Document_Date As DateTime
    Public Tag_No As String = ""
    Public Allow_QC As String = ""
#End Region
    Public Shared Function SaveData(ByVal strDocType As String, ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal strInOutType As String, ByVal strICode As String, ByVal strLocation As String, ByVal intParentLineNo As Integer, ByVal Arr As List(Of clsSerializeInvenotry), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each obj As clsSerializeInvenotry In Arr
                qry = " select max(Code) from TSPL_SERIAL_ITEM"
                obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Code) > 0 Then
                    obj.Code = clsCommon.incval(obj.Code)
                Else
                    obj.Code = "ASI000000000000000000000000001"
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Parent_Line_No", intParentLineNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", counter)
                If clsCommon.myLen(obj.Auto_Sr_No) <= 0 Then
                    obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, trans)
                End If
                clsCommon.AddColumnsForChange(coll, "Auto_Sr_No", obj.Auto_Sr_No)
                clsCommon.AddColumnsForChange(coll, "Auto_Bin_No", obj.Auto_BIN_No)
                clsCommon.AddColumnsForChange(coll, "QC_Complete", obj.Allow_QC)
                clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Type", strDocType)
                clsCommon.AddColumnsForChange(coll, "In_Out_Type", strInOutType)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocation)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SERIAL_ITEM", OMInsertOrUpdate.Insert, "", trans)
                counter += 1
            Next
            qry = "select Item_Code,Auto_Sr_No,1 as Type, sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end) as Rep from TSPL_SERIAL_ITEM group by Item_Code, Auto_Sr_No having sum(case when In_Out_Type='I' then 1 else case when In_Out_Type='O' then -1 else 0 end end)>1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "List of Wrong serial item (some serial no.s are already exist so please enter different serial no.) "
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + "Item " + clsCommon.myCstr(dr("Item_Code"))
                    If clsCommon.myLen(dr("Auto_Sr_No")) > 0 Then
                        qry += " Auto Serial Number - " + clsCommon.myCstr(dr("Auto_Sr_No"))
                    End If
                Next
                Throw New Exception(qry)
            End If
            qry = "select Item_Code,Auto_Sr_No,'' as Manual_Sr_No,1 as Type, sum(1) as Rep from TSPL_SERIAL_ITEM group by Item_Code,Document_Code,In_Out_Type, Auto_Sr_No having sum(1)>1"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "List of Repeated serial item"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + "Item " + clsCommon.myCstr(dr("Item_Code"))
                    If clsCommon.myLen(dr("Auto_Sr_No")) > 0 Then
                        qry += " Auto Serial Number - " + clsCommon.myCstr(dr("Auto_Sr_No"))
                    End If
                Next
                Throw New Exception(qry)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction) As List(Of clsSerializeInvenotry)
        Dim qry As String = "select TSPL_SERIAL_ITEM.*,Tag_No from TSPL_SERIAL_ITEM  LEFT JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "'"
        If clsCommon.CompairString("ISSTRAN", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("PROD_RN", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("PROD_IS", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("Transfer", strDocType) = CompairStringResult.Equal OrElse clsCommon.CompairString("ITransfer", strDocType) = CompairStringResult.Equal Then
            qry += " and TSPL_SERIAL_ITEM.In_Out_Type='O'"
        End If

        qry += "  order by Line_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr As List(Of clsSerializeInvenotry) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsSerializeInvenotry)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsSerializeInvenotry = New clsSerializeInvenotry()
                objTr.Code = clsCommon.myCstr(dr("Code"))
                objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                objTr.Auto_Sr_No = clsCommon.myCstr(dr("Auto_Sr_No"))
                objTr.Auto_BIN_No = clsCommon.myCstr(dr("Auto_BIN_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                objTr.In_Out_Type = clsCommon.myCstr(dr("In_Out_Type"))
                objTr.Against_Inv_Movement_Trans_Id = clsCommon.myCdbl(dr("Against_Inv_Movement_Trans_Id"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                objTr.Tag_No = clsCommon.myCstr(dr("Tag_No"))
                objTr.Allow_QC = clsCommon.myCstr(dr("QC_Complete"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Sub DeleteData(ByVal strDocType As String, ByVal strDocNo As String, ByVal trans As SqlTransaction)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SERIAL_ITEM", "Document_Code", trans)
        Dim qry As String = "Delete from TSPL_SERIAL_ITEM where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    End Sub

    Public Shared Function PostData(ByVal strDocType As String, ByVal strDocNo As String, ByVal strICode As String, ByVal InOutType As String, ByVal intParentLineNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "update TSPL_SERIAL_ITEM set Against_Inv_Movement_Trans_Id=(select max(Trans_Id) from TSPL_INVENTORY_MOVEMENT) where Document_Type='" + strDocType + "' and Document_Code='" + strDocNo + "' and Item_Code='" + strICode + "' and Parent_Line_No='" + clsCommon.myCstr(intParentLineNo) + "' and In_Out_Type='" + InOutType + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
End Class


