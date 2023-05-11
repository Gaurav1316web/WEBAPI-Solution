Imports common
Imports System.Data.SqlClient
Public Class clsfrmItemChrgFranMapMaster
#Region "Variables"
    Public chrcatcode As String = Nothing
    Public chrcatdesc As String = Nothing
    Public itemcode As String = Nothing
    Public itemname As String = Nothing
    Public vendrcode As String = Nothing
    Public vendrname As String = Nothing
    Public chrgs As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal strDesc As String, ByVal Arr As List(Of clsfrmItemChrgFranMapMaster)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_ITEM_FRANCHISE_MAPPING where charge_cat_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsfrmItemChrgFranMapMaster In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "charge_cat_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "description", strDesc)
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "item_code", obj.itemcode)
                    clsCommon.AddColumnsForChange(coll, "item_desc", obj.itemname)
                    clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendrcode)
                    clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendrname)
                    clsCommon.AddColumnsForChange(coll, "charges", obj.chrgs)
                    clsCommon.AddColumnsForChange(coll, "SNO", counter)

                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_FRANCHISE_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsfrmItemChrgFranMapMaster
        Dim obj1 As clsfrmItemChrgFranMapMaster = Nothing
        Dim qry As String = "SELECT * FROM TSPL_ITEM_MASTER"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " where item_Code = (select MIN(item_Code) from TSPL_ITEM_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " where item_Code = (select Max(item_Code) from TSPL_ITEM_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " where item_Code = (select Min(item_Code) from TSPL_ITEM_MASTER where item_Code>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " where item_Code = (select Max(item_Code) from TSPL_ITEM_MASTER where item_Code<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " where item_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj1 = New clsfrmItemChrgFranMapMaster()
            obj1.itemcode = clsCommon.myCstr(dt.Rows(0)("item_Code"))
            obj1.itemname = clsCommon.myCstr(dt.Rows(0)("item_desc"))
        End If
        Return obj1
    End Function
    Public Shared Function GetDataALL(ByVal strProgramCode As String) As List(Of clsfrmItemChrgFranMapMaster)
        Dim Arr As List(Of clsfrmItemChrgFranMapMaster) = Nothing
        Dim qry As String = "SELECT TSPL_ITEM_FRANCHISE_MAPPING.* FROM TSPL_ITEM_FRANCHISE_MAPPING where charge_cat_code='" + strProgramCode + "' "
        qry += "order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsfrmItemChrgFranMapMaster)
            Dim objTr As clsfrmItemChrgFranMapMaster
            For Each dr As DataRow In dt.Rows
                objTr = New clsfrmItemChrgFranMapMaster
                objTr.chrcatcode = clsCommon.myCstr(dr("charge_cat_code"))
                objTr.chrcatdesc = clsCommon.myCstr(dr("description"))
                objTr.itemcode = clsCommon.myCstr(dr("item_code"))
                objTr.itemname = clsCommon.myCstr(dr("item_desc"))
                objTr.vendrcode = clsCommon.myCstr(dr("vendor_code"))
                objTr.vendrname = clsCommon.myCstr(dr("vendor_desc"))
                objTr.chrgs = clsCommon.myCdbl(clsCommon.myCdbl(dr("charges")))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
    Public Shared Function Deletdata(ByVal strcode As String) As Boolean
        Dim qry As String = "delete from TSPL_ITEM_FRANCHISE_MAPPING where charge_cat_code='" + strcode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    Public Shared Function GetData1(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsfrmItemChrgFranMapMaster
        Dim obj1 As clsfrmItemChrgFranMapMaster = Nothing
        Dim qry As String = "SELECT * FROM TSPL_VENDOR_MASTER"
        Dim whrClas As String = " and franchise_yn='Y'"
        Select Case NavType
            Case NavigatorType.First
                qry += " where vendor_Code = (select MIN(vendor_Code) from TSPL_VENDOR_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " where vendor_Code = (select Max(vendor_Code) from TSPL_VENDOR_MASTER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " where vendor_Code = (select Min(vendor_Code) from TSPL_VENDOR_MASTER where vendor_Code>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " where vendor_Code = (select Max(vendor_Code) from TSPL_VENDOR_MASTER where vendor_Code<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " where vendor_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj1 = New clsfrmItemChrgFranMapMaster()
            obj1.vendrcode = clsCommon.myCstr(dt.Rows(0)("vendor_Code"))
            obj1.vendrname = clsCommon.myCstr(dt.Rows(0)("vendor_name"))
        End If
        Return obj1
    End Function
End Class
