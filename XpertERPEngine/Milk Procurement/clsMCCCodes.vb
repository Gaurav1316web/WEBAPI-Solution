Imports System.Data.SqlClient

Public Class clsMCCCodes
#Region "Variables"
    Public arrLocCodes As String = Nothing
    Public LocType As String = Nothing
    Shared xvalues As String = Nothing
    Public Default_LocCode As String = Nothing
    Public Default_LocName As String = Nothing
    Public Default_HO As Boolean = Nothing
    Public arrLocCodeListOfString As List(Of String) = Nothing
#End Region

    Public Shared Function GetData(ByVal trans As SqlTransaction, Optional ByVal isMCC As Boolean = False) As clsMCCCodes
        Try
            Dim obj As New clsMCCCodes()
            obj.arrLocCodes = ""
            Dim qry As String = "select tspl_location_master.location_category,tspl_user_master.default_location from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(qry, trans))

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    xvalues = clsCommon.myCstr(dr("default_location"))
                    obj.Default_LocCode = clsCommon.myCstr(dr("default_location"))
                    obj.LocType = clsCommon.myCstr(dr("location_category"))

                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues

                Next
            End If

            obj.Default_LocName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.Default_LocCode + "'", trans))
            'obj.Default_HO = True
            '--------------if default location is MCC then check whether user is mapped with location segment
            '----------------if mapped,then pick all that locations which are mapped with that segment code.
            'If clsCommon.CompairString(obj.LocType, "MCC") = CompairStringResult.Equal Then
            obj.Default_HO = False
            qry = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_GL_SEGMENT_PERMISSION.GL_Segment,TSPL_GL_SEGMENT_PERMISSION.Segment_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and tspl_location_master.location_code<>'" + xvalues + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    xvalues = clsCommon.myCstr(dr("Location_Code"))

                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
                Next
            End If
            'Else
            'obj.Default_HO = True
            'obj.arrLocCodes = ""
            'End If

            If clsCommon.myLen(obj.arrLocCodes) > 0 Then
                obj.arrLocCodes = obj.arrLocCodes + "'"
                If obj.arrLocCodes.Substring(0, 3) = "','" Then
                    obj.arrLocCodes = obj.arrLocCodes.Substring(2, obj.arrLocCodes.Length - 2)
                End If
            End If

            If isMCC = True Then
                qry = "select * from tspl_mcc_master where mcc_code='" & obj.Default_LocCode & "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt.Rows.Count <= 0 Then
                    obj.Default_LocCode = "_"
                    obj.Default_LocName = "_"
                End If
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(Optional ByVal isMCC As Boolean = False) As clsMCCCodes
        Try
            Dim obj As New clsMCCCodes()
            obj.arrLocCodes = ""
            obj.arrLocCodeListOfString = New List(Of String)
            Dim qry As String = "select tspl_location_master.location_category,tspl_user_master.default_location from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(qry))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    xvalues = clsCommon.myCstr(dr("default_location"))
                    obj.Default_LocCode = clsCommon.myCstr(dr("default_location"))
                    obj.LocType = clsCommon.myCstr(dr("location_category"))
                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
                    obj.arrLocCodeListOfString.Add(xvalues)
                Next
            End If

            obj.Default_LocName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.Default_LocCode + "'"))
            'obj.Default_HO = True
            '--------------if default location is MCC then check whether user is mapped with location segment
            '----------------if mapped,then pick all that locations which are mapped with that segment code.
            'If clsCommon.CompairString(obj.LocType, "MCC") = CompairStringResult.Equal Then
            obj.Default_HO = False
            qry = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_GL_SEGMENT_PERMISSION.GL_Segment,TSPL_GL_SEGMENT_PERMISSION.Segment_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and tspl_location_master.location_code<>'" + xvalues + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'obj.arrLocCodeListOfString = New List(Of String)
                For Each dr As DataRow In dt.Rows
                    xvalues = clsCommon.myCstr(dr("Location_Code"))
                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
                    '===============Update By preeti gupta against ticket no [BM00000008315]
                    obj.arrLocCodeListOfString.Add(xvalues)
                Next
            End If
            'Else
            'obj.Default_HO = True
            'obj.arrLocCodes = ""
            'End If

            If clsCommon.myLen(obj.arrLocCodes) > 0 Then
                obj.arrLocCodes = obj.arrLocCodes + "'"
                If obj.arrLocCodes.Substring(0, 3) = "','" Then
                    obj.arrLocCodes = obj.arrLocCodes.Substring(2, obj.arrLocCodes.Length - 2)
                End If
            End If

            If isMCC = True Then
                qry = "select * from tspl_mcc_master where mcc_code='" & obj.Default_LocCode & "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count <= 0 Then
                    obj.Default_LocCode = "_"
                    obj.Default_LocName = "_"
                End If
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function checkisMcc(ByVal mcc_code As String)
        Dim squery As String = "select mcc_code from tspl_mcc_master where mcc_code='" & mcc_code & "'"
        Dim mcc_code_return As String = clsDBFuncationality.getSingleValue(squery)
        Return mcc_code_return
    End Function
End Class