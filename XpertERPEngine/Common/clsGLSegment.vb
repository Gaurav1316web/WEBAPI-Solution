Imports System.Data.SqlClient
Public Class clsGLSegment
    Public Seg_No As Integer
    Public Seg_Name As String
    Public Seg_Length As Integer
    Public seg_useinclosing As Boolean
    Public Report_Filters As Boolean

    Public Function SaveData(ByVal Arr As List(Of clsGLSegment)) As Boolean
        Dim qry As String
        Dim dt As DataTable
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                Dim ii As Integer = 0
                For Each obj As clsGLSegment In Arr
                    ii += 1
                    If ii <> obj.Seg_No Then
                        Throw New Exception("Segment No should be in sequence")
                    End If
                    Dim isChangeColumnSize As Boolean = True
                    If obj.Seg_No = 1 Then
                        If Not clsCommon.CompairString(obj.Seg_Name, "Accounts") = CompairStringResult.Equal Then
                            Throw New Exception("Segment No " + clsCommon.myCstr(obj.Seg_No) + " Name should be Accounts")
                        End If

                        obj.Seg_Name = "Accounts"
                        qry = "select top 1 Main_GL_Account from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            isChangeColumnSize = False
                        End If
                    Else
                        If obj.Seg_No = 7 Then
                            If Not clsCommon.CompairString(obj.Seg_Name, "Location") = CompairStringResult.Equal Then
                                Throw New Exception("Segment No " + clsCommon.myCstr(obj.Seg_No) + " Name should be Location")
                            End If
                            obj.Seg_Name = "Location"
                        ElseIf obj.Seg_No < 1 Or obj.Seg_No > 10 Then
                            Throw New Exception("Segment No should be 1 to 10")
                        End If
                        qry = "select top 1 Segment_code  from TSPL_GL_SEGMENT_CODE where Segment_Name='" + obj.Seg_Name + "'"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            isChangeColumnSize = False
                        End If
                    End If

                    Dim coll As New Hashtable()
                    If isChangeColumnSize Then
                        clsCommon.AddColumnsForChange(coll, "Seg_Name", obj.Seg_Name)
                        clsCommon.AddColumnsForChange(coll, "Seg_Length", obj.Seg_Length)
                    End If
                    clsCommon.AddColumnsForChange(coll, "seg_useinclosing", IIf(obj.seg_useinclosing, "Y", "N"))
                    clsCommon.AddColumnsForChange(coll, "Report_Filters", IIf(obj.Report_Filters, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    qry = "select 1 from TSPL_GL_SEGMENT where Seg_No='" + clsCommon.myCstr(obj.Seg_No) + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Seg_No", obj.Seg_No)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT", OMInsertOrUpdate.Update, "Seg_No='" + clsCommon.myCstr(obj.Seg_No) + "'", trans)
                        qry = "update TSPL_GL_SEGMENT_CODE set Segment_name='" + obj.Seg_Name + "' where Seg_No='" + clsCommon.myCstr(obj.Seg_No) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
