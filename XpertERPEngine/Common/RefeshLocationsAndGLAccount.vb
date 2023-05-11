Public Class RefeshLocationsAndGLAccount

    Public Shared Sub RefeshUserApplicableLocationsAndGLAccount()
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            objCommonVar.arrCurrUserLocations = Nothing
            'objCommonVar.arrCurrUserGLAccount = Nothing
            Dim qry As String = "SELECT SEGMENT_CODE FROM TSPL_GL_SEGMENT_CODE WHERE TSPL_GL_SEGMENT_CODE.Seg_No='7'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCommonVar.strCurrUserLocationsSegment = ""
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                        objCommonVar.strCurrUserLocationsSegment += ","
                    End If
                    objCommonVar.strCurrUserLocationsSegment += "'" + clsCommon.myCstr(dr("Segment_Code")) + "'"
                Next
            End If
        Else
            Dim qry As String = "select Segment_Code from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' and GL_Segment='7'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            objCommonVar.strCurrUserLocationsSegment = "''"
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCommonVar.strCurrUserLocationsSegment = ""
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                        objCommonVar.strCurrUserLocationsSegment += ","
                    End If
                    objCommonVar.strCurrUserLocationsSegment += "'" + clsCommon.myCstr(dr("Segment_Code")) + "'"
                Next
            End If

            qry = "select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCommonVar.arrCurrUserLocations = New List(Of String)
                For Each dr As DataRow In dt.Rows
                    objCommonVar.arrCurrUserLocations.Add(clsCommon.myCstr(dr("Location_Code")))
                Next
                objCommonVar.strCurrUserLocations = clsCommon.GetMulcallString(objCommonVar.arrCurrUserLocations)
            End If

            qry = "select Account_Code from TSPL_GL_ACCOUNTS where Account_Seg_Code7 in (select segment_code from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' and GL_Segment='7') "
            qry += " union "
            qry += " select Account_Code from TSPL_GL_ACCOUNT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ''objCommonVar.arrCurrUserGLAccount = New List(Of String)
                ''For Each dr As DataRow In dt.Rows
                ''    objCommonVar.arrCurrUserGLAccount.Add(clsCommon.myCstr(dr("Account_Code")))
                ''Next
                objCommonVar.strCurrUserGLAccount = qry
            End If
        End If
    End Sub

End Class
