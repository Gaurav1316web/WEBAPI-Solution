Imports common
Imports System.Data
Imports System.Data.SqlClient
'====================================Created by Preeti Gupta============
Public Class ClsIncomeTaxSlab
#Region "Variables"
    Public IT_CODE As String
    Public Description As String
    Public Applied_For As String

    Public ObjList As List(Of ClsIncomeTaxSlabDetail) = Nothing
    Dim objDetail As New ClsIncomeTaxSlabDetail()
#End Region

    Public Function SaveData(ByVal obj As ClsIncomeTaxSlab, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "IT_CODE", obj.IT_CODE)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Applied_For", obj.Applied_For)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_INCOME_TAX_SLAB where IT_CODE= '" & obj.IT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    'Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB", OMInsertOrUpdate.Update, "IT_CODE='" + obj.IT_CODE + "'", trans)
            End If
            isSaved = objDetail.SaveData(obj.IT_CODE, obj.ObjList, trans)

        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsIncomeTaxSlab
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsIncomeTaxSlab
        Dim obj As ClsIncomeTaxSlab = Nothing

        Dim qry As String = " Select TSPL_INCOME_TAX_SLAB.IT_CODE As [IT CODE],TSPL_INCOME_TAX_SLAB.Description As [Description],TSPL_INCOME_TAX_SLAB.Applied_For As [AppliedFor]"
        qry += " ,TSPL_INCOME_TAX_SLAB_DETAIL.SLAB_FROM As [SLAB FROM],TSPL_INCOME_TAX_SLAB_DETAIL.SLAB_TO [SLAB TO],TSPL_INCOME_TAX_SLAB_DETAIL.TAX_RATE As [TAX RATE] From TSPL_INCOME_TAX_SLAB "
        qry += " LEFT OUTER JOIN TSPL_INCOME_TAX_SLAB_DETAIL ON TSPL_INCOME_TAX_SLAB.IT_CODE =TSPL_INCOME_TAX_SLAB_DETAIL.IT_CODE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INCOME_TAX_SLAB.IT_CODE = (select MIN(IT_CODE) from TSPL_INCOME_TAX_SLAB)"
            Case NavigatorType.Last
                qry += " and TSPL_INCOME_TAX_SLAB.IT_CODE = (select Max(IT_CODE) from TSPL_INCOME_TAX_SLAB)"
            Case NavigatorType.Next
                qry += " and TSPL_INCOME_TAX_SLAB.IT_CODE = (select Min(IT_CODE) from TSPL_INCOME_TAX_SLAB where  IT_CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_INCOME_TAX_SLAB.IT_CODE = (select Max(IT_CODE) from TSPL_INCOME_TAX_SLAB where IT_CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_INCOME_TAX_SLAB.IT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsIncomeTaxSlab()
            obj.IT_CODE = clsCommon.myCstr(dt.Rows(0)("IT CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Applied_For = clsCommon.myCstr(dt.Rows(0)("AppliedFor"))
            obj.ObjList = ClsIncomeTaxSlabDetail.GetData(obj.IT_CODE, trans)
        End If
        Return obj
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
            qry = "Delete From TSPL_INCOME_TAX_SLAB_DETAIL Where IT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_INCOME_TAX_SLAB Where IT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
Public Class ClsIncomeTaxSlabDetail

#Region "Variables"
    Public IT_CODE As String
    Public Slab_From As String
    Public Slab_To As Decimal
    Public Tax_Rate As String
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsIncomeTaxSlabDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_INCOME_TAX_SLAB_DETAIL where IT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsIncomeTaxSlabDetail In ObjList
                Dim coll As New Hashtable()
                If clsCommon.myLen(obj.Slab_From) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "IT_CODE", obj.IT_CODE)
                    clsCommon.AddColumnsForChange(coll, "SLAB_FROM", obj.Slab_From)
                    clsCommon.AddColumnsForChange(coll, "SLAB_TO", obj.Slab_To)
                    clsCommon.AddColumnsForChange(coll, "TAX_RATE", obj.Tax_Rate)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCOME_TAX_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsIncomeTaxSlabDetail)
        Dim obj As ClsIncomeTaxSlabDetail = Nothing
        Dim ObjList As New List(Of ClsIncomeTaxSlabDetail)
        Dim qry As String = " select *  from TSPL_INCOME_TAX_SLAB_DETAIL WHERE IT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsIncomeTaxSlabDetail()
                obj.IT_CODE = clsCommon.myCstr(dr("IT_CODE"))
                obj.Slab_From = clsCommon.myCdbl(dr("SLAB_FROM"))
                obj.Slab_To = clsCommon.myCdbl(dr("SLAB_TO"))
                obj.Tax_Rate = clsCommon.myCstr(dr("TAX_RATE"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " Delete From TSPL_INCOME_TAX_SLAB_DETAIL Where IT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class
