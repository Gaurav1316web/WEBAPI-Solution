Imports common
Imports System.Data.SqlClient

Public Class clsProfitAndLossPerforma
#Region "Variables"
    Public SNo As Integer = Nothing
    Public MainParticular As String = Nothing
    Public Particular As String = Nothing
    Public GroupCode As String = Nothing
    Public GroupName As String = Nothing
    Public Type As String = Nothing
    Public Note As String = Nothing
    Public FONTSTYLE As String = Nothing
    Public Formula As String = Nothing
    Public arrGLMainAccount As ArrayList
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsProfitAndLossPerforma)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If clsProfitAndLossPerforma.SaveData(Arr, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
   
    Public Shared Function SaveData(ByVal Arr As List(Of clsProfitAndLossPerforma), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                DeleteData(trans)
                For Each obj As clsProfitAndLossPerforma In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "S_No", clsCommon.myCdbl(obj.SNo))
                    clsCommon.AddColumnsForChange(coll, "Main_Particular", clsCommon.myCstr(obj.MainParticular))
                    clsCommon.AddColumnsForChange(coll, "Particular", clsCommon.myCstr(obj.Particular))
                    clsCommon.AddColumnsForChange(coll, "Group_Code", clsCommon.myCstr(obj.GroupCode))
                    clsCommon.AddColumnsForChange(coll, "Group_Name", clsCommon.myCstr(obj.GroupName))
                    clsCommon.AddColumnsForChange(coll, "Note", obj.Note)
                    If clsCommon.myCstr(obj.Type) = "Add" Then
                        obj.Type = 1
                    Else
                        obj.Type = -1
                    End If
                    clsCommon.AddColumnsForChange(coll, "Type", clsCommon.myCdbl(obj.Type))
                    If clsCommon.myCstr(obj.FONTSTYLE) = "Bold" Then
                        obj.FONTSTYLE = 2
                    ElseIf clsCommon.myCstr(obj.FONTSTYLE) = "Italic" Then
                        obj.FONTSTYLE = 3
                    ElseIf clsCommon.myCstr(obj.FONTSTYLE) = "Underline" Then
                        obj.FONTSTYLE = 4
                    Else
                        obj.FONTSTYLE = 1
                    End If
                    clsCommon.AddColumnsForChange(coll, "Font_Style", clsCommon.myCdbl(obj.FONTSTYLE))
                    clsCommon.AddColumnsForChange(coll, "Formula", obj.Formula)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROFIT_AND_LOSS_PERFORMA ", OMInsertOrUpdate.Insert, "", trans)
                    If obj.arrGLMainAccount IsNot Nothing AndAlso obj.arrGLMainAccount.Count > 0 Then
                        For Each strGLCode As String In obj.arrGLMainAccount
                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "SNO", obj.SNo)
                            clsCommon.AddColumnsForChange(coll, "Main_GL_Account", strGLCode)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If
                Next obj

                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData() As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            DeleteData(trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal trans As SqlTransaction) As Boolean
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN", trans)
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_PROFIT_AND_LOSS_PERFORMA", trans)
        Return True
    End Function

    Public Shared Function GetData() As List(Of clsProfitAndLossPerforma)
        Dim arr As List(Of clsProfitAndLossPerforma) = Nothing
        Dim qry As String = "select TSPL_PROFIT_AND_LOSS_PERFORMA.S_No,TSPL_PROFIT_AND_LOSS_PERFORMA.Main_Particular,TSPL_PROFIT_AND_LOSS_PERFORMA.Particular," _
        & " TSPL_PROFIT_AND_LOSS_PERFORMA.Group_Code,TSPL_PROFIT_AND_LOSS_PERFORMA.Group_Name ,TSPL_PROFIT_AND_LOSS_PERFORMA.Type,TSPL_PROFIT_AND_LOSS_PERFORMA.Note," _
        & " TSPL_PROFIT_AND_LOSS_PERFORMA.Font_Style,Formula from TSPL_PROFIT_AND_LOSS_PERFORMA "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProfitAndLossPerforma)
            For Each dr As DataRow In dt.Rows
                Dim obj As clsProfitAndLossPerforma = New clsProfitAndLossPerforma()
                obj.SNo = clsCommon.myCdbl(dr("S_No"))
                obj.MainParticular = clsCommon.myCstr(dr("Main_Particular"))
                obj.Particular = clsCommon.myCstr(dr("Particular"))
                obj.GroupCode = clsCommon.myCstr(dr("Group_Code"))
                obj.GroupName = clsCommon.myCstr(dr("Group_Name"))
                obj.Type = clsCommon.myCstr(dr("Type"))
                obj.Note = clsCommon.myCstr(dr("Note"))
                obj.FONTSTYLE = clsCommon.myCstr(dr("Font_Style"))
                obj.Formula = clsCommon.myCstr(dr("Formula"))

                qry = "select Main_GL_Account  from TSPL_PROFIT_AND_LOSS_PERFORMA_GL_MAIN where SNO='" + clsCommon.myCstr(obj.SNo) + "'"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTemp IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.arrGLMainAccount = New ArrayList
                    For Each drtemp As DataRow In dtTemp.Rows
                        obj.arrGLMainAccount.Add(clsCommon.myCstr(drtemp("Main_GL_Account")))
                    Next
                End If

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class
