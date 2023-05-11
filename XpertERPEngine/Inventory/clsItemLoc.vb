'===================BM00000003069,Updated By Rohit========================
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsItemLoc

#Region "Variables"

    Public Frm_Loc As String
    Public To_Loc As String
    Public GL_Acc As String
    Public Line_No As Integer
    'Public isNewEntry As Integer
    Public Shared ObjListBOM As List(Of clsItemLoc)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As List(Of clsItemLoc)
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            'If (clsCommon.myLen(strCode) <= 0) Then
            '    Throw New Exception("Code not found to Delete")
            'End If

            Dim qry As String
            ' qry = "delete from TSPL_ITEM_LOCATION_MAPPING where Frm_Location ='" + strCode + "'"
            qry = "delete from TSPL_ITEM_LOCATION_MAPPING "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '   trans.Commit()
        Catch ex As Exception
            '  trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As List(Of clsItemLoc)
        Dim obj As New clsItemLoc()
        Dim objtr As New clsItemLoc()

        ObjListBOM = New List(Of clsItemLoc)

        Dim qry As String = "SELECT * from TSPL_ITEM_LOCATION_MAPPING where 2=2 "

        'Select Case NavType
        '    Case NavigatorType.First
        '        qry += " AND Frm_Loc = (select MIN(Frm_Loc) from TSPL_BOM_HEAD_PP)"
        '    Case NavigatorType.Last
        '        qry += " AND Frm_Loc = (select Max(Frm_Loc) from TSPL_BOM_HEAD_PP)"
        '    Case NavigatorType.Next
        '        qry += " AND Frm_Loc = (select Min(Frm_Loc) from TSPL_BOM_HEAD_PP where  Frm_Loc>'" + strCode + "')"
        '    Case NavigatorType.Previous
        '        qry += " AND Frm_Loc = (select Max(Frm_Loc) from TSPL_BOM_HEAD_PP where Frm_Loc<'" + strCode + "')"
        '    Case NavigatorType.Current
        '        qry += " AND Frm_Loc = '" + strCode + "'"
        'End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsItemLoc
                    objtr.Line_No = dr("Line_No")
                    objtr.Frm_Loc = dr("Frm_Location")
                    objtr.To_Loc = clsCommon.myCstr(dr("To_Location"))
                    objtr.GL_Acc = clsCommon.myCstr(dr("GL_Acc"))
                    ObjListBOM.Add(objtr)
                Next
            End If
        End If
        Return ObjListBOM
    End Function

    'Public Function SaveData(ByVal objItemLoc As clsItemLoc, ByVal Arr As List(Of clsItemLoc), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
    '    Return SaveData(objItemLoc, Arr, isNewEntry, strCode, trans)
    'End Function
    Public Function SaveData(ByVal objItemLoc As clsItemLoc, ByVal Arr As List(Of clsItemLoc), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim i As Integer = 0
        Dim RowCount As Integer = 0
        RowCount = Arr.Count
        Dim PerCent As Integer = 0
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                'clsItemLoc.DeleteData(trans)
                clsCommon.ProgressBarPercentShow()
                For Each obj As clsItemLoc In Arr
                    Dim coll As New Hashtable()
                    i = i + 1
                    PerCent = (i * 100) / RowCount
                    clsCommon.ProgressBarPercentUpdate(PerCent, " Saving Record  " & i & " Out Of " & RowCount)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                    'clsComm4on.AddColumnsForChange(coll, "CONSM_ITEM_TYPE", obj.CONSM_ITEM_TYPE, True)
                    clsCommon.AddColumnsForChange(coll, "Frm_Location", obj.Frm_Loc)
                    clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Loc)
                    clsCommon.AddColumnsForChange(coll, "GL_Acc", obj.GL_Acc)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    Dim isExits As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_LOCATION_MAPPING where TSPL_ITEM_LOCATION_MAPPING.Frm_Location='" & obj.Frm_Loc & "' and TSPL_ITEM_LOCATION_MAPPING.To_Location='" & obj.To_Loc & "'", trans)
                    If isExits <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_LOCATION_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_LOCATION_MAPPING", OMInsertOrUpdate.Update, "TSPL_ITEM_LOCATION_MAPPING.Frm_Location='" & obj.Frm_Loc & "' and TSPL_ITEM_LOCATION_MAPPING.To_Location='" & obj.To_Loc & "'", trans)
                    End If

                Next

            End If

            If isSaved Then
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
            End If
        Catch err As Exception
            clsCommon.ProgressBarPercentHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Function SaveData(ByVal Arr As List(Of clsItemLoc)) As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemLoc In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Frm_Location", obj.Frm_Loc)
                    clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Loc)
                    clsCommon.AddColumnsForChange(coll, "GL_Acc", obj.GL_Acc)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    Dim isExits As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_LOCATION_MAPPING where Frm_Location='" & obj.Frm_Loc & "' and To_Location='" & obj.To_Loc & "'", trans)
                    If isExits <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_LOCATION_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_LOCATION_MAPPING", OMInsertOrUpdate.Update, "TSPL_ITEM_LOCATION_MAPPING.Frm_Location='" & obj.Frm_Loc & "' and TSPL_ITEM_LOCATION_MAPPING.To_Location='" & obj.To_Loc & "'", trans)
                    End If
                Next
            End If

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Shared Function FinderForLocation(ByVal strCode As String, ByVal WhrCls As String, ByVal isButtonClicked As Boolean) As String
        'Dim obj As clsBOM = Nothing
        Dim qry As String = "select Location_Code as code,Location_Desc + '(' + Location_Code + ')' as name from TSPL_LOCATION_MASTER "
        strCode = clsCommon.myCstr(clsCommon.ShowSelectForm("LocationFinder", qry, "code", WhrCls, strCode, "Code", isButtonClicked))
        Return strCode
    End Function

    Public Shared Function FinderForGlAccount(ByVal strCode As String, ByVal WhrCls As String, ByVal isButtonClicked As Boolean) As String
        Dim obj As clsBOM = Nothing
        Dim qry As String = "select Account_Code as code,Description + '(' + Account_Code + ')' as Name from TSPL_GL_ACCOUNTS "
        strCode = clsCommon.ShowSelectForm("GLAccountFinder", qry, "code", WhrCls, strCode, "Code", isButtonClicked)
        Return strCode
    End Function

    Public Shared Function SaveDataImport(ByVal Arr As List(Of clsItemLoc), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsItemLoc In Arr
                'Dim qry As String = "DELETE FROM TSPL_BOM_DETAIL_PP WHERE Frm_Loc='" + obj.Frm_Loc + "' AND LINE_NO=" & obj.Line_No & ""
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim qry As String = "DELETE FROM TSPL_ITEM_LOCATION_MAPPING WHERE Frm_Location='" + obj.Frm_Loc + "' AND LINE_NO='" & obj.Line_No & "' and To_Location='" & obj.To_Loc & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                'clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_TYPE", obj.CONSM_ITEM_TYPE, True)
                clsCommon.AddColumnsForChange(coll, "Frm_Loc", obj.Frm_Loc)
                clsCommon.AddColumnsForChange(coll, "To_Loc", obj.To_Loc)
                clsCommon.AddColumnsForChange(coll, "GL_Acc", obj.GL_Acc)
                'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOM_DETAIL_PP", OMInsertOrUpdate.Insert, "TSPL_BOM_DETAIL_PP.Frm_Loc='" + obj.Frm_Loc + "' AND LINE_NO=" & obj.Line_No & "", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_LOCATION_MAPPING", OMInsertOrUpdate.Insert, "TSPL_ITEM_LOCATION_MAPPING.Frm_Location='" + obj.Frm_Loc + "' AND LINE_NO='" & obj.Line_No & "' and To_Location='" & obj.To_Loc & "'", trans)
            Next

        End If

        Return True
    End Function

End Class




