Imports common
Imports System.Data.SqlClient

Public Class clsRCDFRateControl
#Region "Variables"
    Public Code As String
    Public Doc_Date As DateTime
    Public From_Date As Date
    Public To_Date As Date?
    Public ToDate As Boolean
    Public Remarks As String
    Public Comments As String
    Public Status As Decimal
    Public arrDetail As List(Of clsRCDFRateControlDetail)
#End Region

    Public Shared Function SaveData(obj As clsRCDFRateControl, ByVal isNewEntry As Boolean, ByVal isPost As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, isPost, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(obj As clsRCDFRateControl, ByVal isNewEntry As Boolean, ByVal isPost As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If Not isNewEntry Then
                Dim Qry As String = "delete from TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM where Code='" + clsCommon.myCstr(obj.Code) + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "Delete from TSPL_RCDF_RATE_CONTROL_DETAIL Where Code='" + clsCommon.myCstr(obj.Code) + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", obj.To_Date, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isPost Then
                clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Status", 1)
            End If
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.frmRCDFRateControl, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_RATE_CONTROL", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_RATE_CONTROL", OMInsertOrUpdate.Update, "TSPL_RCDF_RATE_CONTROL.Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsRCDFRateControlDetail.SaveData(clsCommon.myCstr(obj.Code), obj.arrDetail, isPost, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsRCDFRateControl
        Try
            Dim obj As clsRCDFRateControl = Nothing
            Dim qry As String = "SELECT * FROM TSPL_RCDF_RATE_CONTROL Where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Code = (select MIN(Code) from TSPL_RCDF_RATE_CONTROL)"
                Case NavigatorType.Last
                    qry += " and Code = (select Max(Code) from TSPL_RCDF_RATE_CONTROL)"
                Case NavigatorType.Next
                    qry += " and Code = (select Min(Code) from TSPL_RCDF_RATE_CONTROL where Code > '" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Code = (select Max(Code) from TSPL_RCDF_RATE_CONTROL where Code < '" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Code = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsRCDFRateControl()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
                If dt.Rows(0)("To_Date") IsNot Nothing AndAlso clsCommon.myLen(dt.Rows(0)("To_Date")) > 0 Then
                    obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                End If
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                obj.Status = clsCommon.myCDecimal(dt.Rows(0)("Status"))

                qry = "Select TSPL_RCDF_RATE_CONTROL_DETAIL.PK_Id,TSPL_RCDF_RATE_CONTROL_DETAIL.Code,TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_RCDF_RATE_CONTROL_DETAIL.UOM,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_RCDF_RATE_CONTROL_DETAIL.Rate,TSPL_RCDF_RATE_CONTROL_DETAIL.Tolerance from TSPL_RCDF_RATE_CONTROL_DETAIL
                       Inner Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code
                       Inner Join TSPL_ITEM_TYPE_MASTER ON TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_ITEM_MASTER.Item_Type
                       Where TSPL_RCDF_RATE_CONTROL_DETAIL.Code='" + clsCommon.myCstr(obj.Code) + "' "
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso (dt.Rows.Count > 0) Then
                    obj.arrDetail = New List(Of clsRCDFRateControlDetail)
                    Dim objTr As clsRCDFRateControlDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsRCDFRateControlDetail
                        objTr.Code = clsCommon.myCstr(dr("Code"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.UOM = clsCommon.myCstr(dr("UOM"))
                        If clsCommon.CompairString(clsCommon.myCstr(dr("UOM")), "F") Then
                            objTr.Item_Type = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
                        End If
                        objTr.Rate = clsCommon.myCDecimal(dr("Rate"))
                        objTr.Tolerance = clsCommon.myCDecimal(dr("Tolerance"))
                        obj.arrDetail.Add(objTr)
                    Next
                End If
            End If
            Return obj
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(strCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_RCDF_RATE_CONTROL_DETAIL where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_RCDF_RATE_CONTROL where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function




End Class

Public Class clsRCDFRateControlDetail
#Region "Variables"
    Public Code As String
    Public Item_Code As String
    Public Item_Name As String
    Public UOM As String
    Public Item_Type As String
    Public Rate As Decimal
    Public Tolerance As Decimal
#End Region
    Public Shared Function SaveData(ByVal strCode As String, arr As List(Of clsRCDFRateControlDetail), ByVal ispost As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True

        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each obj As clsRCDFRateControlDetail In arr
                Dim colm As New Hashtable()
                clsCommon.AddColumnsForChange(colm, "Code", strCode)
                clsCommon.AddColumnsForChange(colm, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(colm, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(colm, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(colm, "Tolerance", obj.Tolerance)
                IsSaved = clsCommonFunctionality.UpdateDataTable(colm, "TSPL_RCDF_RATE_CONTROL_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                If ispost Then
                    Dim Rate As Decimal
                    Dim SubQuer As String = ""
                    Dim whrCls As String = " where TSPL_RCDF_RATE_CONTROL_DETAIL.Code='" + strCode + "' And TSPL_ITEM_UOM_DETAIL.ITEM_code='" + clsCommon.myCstr(obj.Item_Code) + "'"
                    Dim Qry As String = "select TSPL_RCDF_RATE_CONTROL_DETAIL.Pk_Id,TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,
                                         TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_RCDF_RATE_CONTROL_DETAIL.Rate,TSPL_RCDF_RATE_CONTROL_DETAIL.Tolerance
                                         from TSPL_ITEM_UOM_DETAIL 
                                         Inner Join TSPL_RCDF_RATE_CONTROL_DETAIL On TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry + whrCls, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each rows As DataRow In dt.Rows
                            If clsCommon.myCDecimal(rows("Tolerance")) > 0 Then
                                Dim minValue As Decimal
                                Dim maxValue As Decimal
                                Dim Conversion_Factor As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_factor from TSPL_ITEM_UOM_DETAIL where ITEM_code='" + clsCommon.myCstr(obj.Item_Code) + "' And UOM_Code='" + clsCommon.myCstr(obj.UOM) + "' ", trans))
                                Dim DefaultCovFact As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_factor from TSPL_ITEM_UOM_DETAIL where ITEM_code='" + clsCommon.myCstr(obj.Item_Code) + "' And TSPL_ITEM_UOM_DETAIL.Default_UOM=1 ", trans))
                                Dim covFact As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_factor from TSPL_ITEM_UOM_DETAIL where ITEM_code='" + clsCommon.myCstr(obj.Item_Code) + "' And UOM_code='" + clsCommon.myCstr(rows("UOM_Code")) + "'", trans))
                                Rate = clsCommon.myCDecimal(rows("Rate")) / ((Conversion_Factor * DefaultCovFact) / covFact)
                                minValue = Rate - ((Rate * clsCommon.myCDecimal(rows("Tolerance"))) / 100)
                                maxValue = Rate + ((Rate * clsCommon.myCDecimal(rows("Tolerance"))) / 100)
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                                clsCommon.AddColumnsForChange(coll, "Against_PK_Id", clsCommon.myCDecimal(rows("PK_Id")))
                                clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(rows("UOM_Code")))
                                clsCommon.AddColumnsForChange(coll, "Min_Rate", minValue)
                                clsCommon.AddColumnsForChange(coll, "Max_Rate", maxValue)
                                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM", OMInsertOrUpdate.Insert, "", trans)
                            End If
                        Next
                    End If
                End If

            Next
        End If
        Return IsSaved
    End Function

End Class

Public Class clsRCDFRateControlDetailAllUOM
#Region "Variables"
    Public Code As String
    Public Against_PK_Id As Decimal
    Public Min_Rate As Decimal
    Public Max_Rate As Decimal
#End Region


End Class
