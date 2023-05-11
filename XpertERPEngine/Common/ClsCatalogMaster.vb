
'' Created By richa Ticket no BM00000003570 on 21/08/2014
Imports common
Imports System.Data.SqlClient

Public Class ClsCatalogMaster
    Public Catalog_Code As String = Nothing
    Public Catalog_Date As Date? = Nothing
    Public Catalog_Desc As String = Nothing
    Public Bom_Code As String = Nothing
    Public Bom_Desc As String = Nothing
    Public Specification As String = Nothing
    Public Feature As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_image As Byte()
    Public arrCatalogDetail As List(Of ClsCatalogDetail) = Nothing

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_CATALOG_MASTER.Catalog_Code as Code,Convert(date,TSPL_CATALOG_MASTER.Catalog_Date) as Date,TSPL_CATALOG_MASTER.Catalog_Desc from TSPL_CATALOG_MASTER  "
        str = clsCommon.ShowSelectForm("CatalogMaster", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsCatalogMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_CATALOG_DETAIL where Catalog_Code='" + obj.Catalog_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Catalog_Code = clsERPFuncationality.GetNextCode(trans, obj.Catalog_Date, clsDocType.CatalogMaster, "", "")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Catalog_Date", clsCommon.GetPrintDate(obj.Catalog_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Catalog_Desc", obj.Catalog_Desc)
            clsCommon.AddColumnsForChange(coll, "Bom_Code", obj.Bom_Code)
            clsCommon.AddColumnsForChange(coll, "Bom_Desc", obj.Bom_Desc)
            clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
            clsCommon.AddColumnsForChange(coll, "Feature", obj.Feature)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            'clsCommon.AddColumnsForChange(coll, "Item_image", obj.Item_image)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Catalog_Code", obj.Catalog_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATALOG_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATALOG_MASTER", OMInsertOrUpdate.Update, "TSPL_CATALOG_MASTER.Catalog_Code='" + obj.Catalog_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsCatalogDetail.saveData(obj.arrCatalogDetail, obj.Catalog_Code, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsCatalogMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCatalogMaster
        Dim obj As ClsCatalogMaster = Nothing
        Dim Arr As List(Of ClsCatalogMaster) = Nothing
        Dim qry As String = "Select TSPL_CATALOG_MASTER.Catalog_Code,TSPL_CATALOG_MASTER.Catalog_Date,TSPL_CATALOG_MASTER.Catalog_Desc,TSPL_CATALOG_MASTER.Bom_Code,TSPL_CATALOG_MASTER.Bom_Desc,TSPL_CATALOG_MASTER.Specification,TSPL_CATALOG_MASTER.Feature,TSPL_CATALOG_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc  from TSPL_CATALOG_MASTER left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_CATALOG_MASTER.Item_Code where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CATALOG_MASTER.Catalog_Code = (select MIN(Catalog_Code) from TSPL_CATALOG_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CATALOG_MASTER.Catalog_Code = (select Max(Catalog_Code) from TSPL_CATALOG_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CATALOG_MASTER.Catalog_Code = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_CATALOG_MASTER.Catalog_Code = (select Min(Catalog_Code) from TSPL_CATALOG_MASTER where Catalog_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CATALOG_MASTER.Catalog_Code = (select Max(Catalog_Code) from TSPL_CATALOG_MASTER where Catalog_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCatalogMaster()
            obj.Catalog_Code = clsCommon.myCstr(dt.Rows(0)("Catalog_Code"))
            obj.Catalog_Date = clsCommon.myCDate(dt.Rows(0)("Catalog_Date"))
            obj.Catalog_Desc = clsCommon.myCstr(dt.Rows(0)("Catalog_Desc"))
            obj.Bom_Code = clsCommon.myCstr(dt.Rows(0)("Bom_Code"))
            obj.Bom_Desc = clsCommon.myCstr(dt.Rows(0)("Bom_Desc"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Specification = clsCommon.myCstr(dt.Rows(0)("Specification"))
            obj.Feature = clsCommon.myCstr(dt.Rows(0)("Feature"))

            obj.arrCatalogDetail = ClsCatalogDetail.getData(obj.Catalog_Code, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_CATALOG_DETAIL where Catalog_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CATALOG_MASTER where Catalog_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
Public Class ClsCatalogDetail
    Public Catalog_Code As String = Nothing
    Public Line_No As Integer
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Specification As String = Nothing
    Public Feature As String = Nothing
    Public Shared Function saveData(ByVal arrObj As List(Of ClsCatalogDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsCatalogDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Catalog_Code", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                    clsCommon.AddColumnsForChange(coll, "Feature", obj.Feature)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CATALOG_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsCatalogDetail)
        Try
            Dim arrObj As List(Of ClsCatalogDetail) = Nothing
            Dim obj As ClsCatalogDetail = Nothing
            Dim qry As String = "select * from TSPL_CATALOG_DETAIL where Catalog_Code='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsCatalogDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsCatalogDetail()
                    obj.Line_No = clsCommon.myCstr(dt.Rows(i)("Line_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Specification = clsCommon.myCstr(dt.Rows(i)("Specification"))
                    obj.Feature = clsCommon.myCstr(dt.Rows(i)("Feature"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
