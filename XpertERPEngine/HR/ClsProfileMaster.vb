Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsProfileMaster

#Region "Variables"
    Public Profile_Code As String
    Public Description As String

    Public ObjList As List(Of ClsProfileMasterDetail) = Nothing
    Dim objDetail As New ClsProfileMasterDetail()
#End Region

   
    Public Shared Function SaveData(ByVal arr As List(Of ClsProfileMaster)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsProfileMaster.SaveData(arr, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsProfileMaster), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsProfileMaster In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Profile_Code", obj.Profile_Code)
                clsCommon.AddColumnsForChange(coll, "Profile_Name", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_PROFILE_MASTER WHERE Profile_Code='" + obj.Profile_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_PROFILE_MASTER where Profile_Code= '" & obj.Profile_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PROFILE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PROFILE_MASTER", OMInsertOrUpdate.Update, "Profile_Code='" + obj.Profile_Code + "'", trans)
                End If
                ClsProfileMasterDetail.SaveData(obj.Profile_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsProfileMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsProfileMaster
        Dim obj As ClsProfileMaster = Nothing

        Dim qry As String = "Select TSPL_HR_PROFILE_MASTER.Profile_Code,TSPL_HR_PROFILE_MASTER.Profile_Name from TSPL_HR_PROFILE_MASTER LEFT OUTER JOIN  TSPL_HR_PROFILE_Detail on TSPL_HR_PROFILE_MASTER.Profile_Code =TSPL_HR_PROFILE_Detail.Profile_Code where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_PROFILE_MASTER.Profile_Code = (select MIN(Profile_Code) from TSPL_HR_PROFILE_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_PROFILE_MASTER.Profile_Code = (select Max(Profile_Code) from TSPL_HR_PROFILE_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_PROFILE_MASTER.Profile_Code = (select Min(Profile_Code) from TSPL_HR_PROFILE_MASTER where  Profile_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_PROFILE_MASTER.Profile_Code = (select Max(Profile_Code) from TSPL_HR_PROFILE_MASTER where Profile_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_PROFILE_MASTER.Profile_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsProfileMaster()
            obj.Profile_Code = clsCommon.myCstr(dt.Rows(0)("Profile_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Profile_Name"))
            obj.ObjList = ClsProfileMasterDetail.GetData(obj.Profile_Code, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "Delete From TSPL_HR_PROFILE_DETAIL Where Profile_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_PROFILE_MASTER Where Profile_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
                isSaved = False
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
Public Class ClsProfileMasterDetail

#Region "Variables"
    Public Profile_Code As String
    Public Round_Code As String
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_PROFILE_DETAIL where Profile_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsProfileMasterDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_PROFILE_DETAIL where Profile_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsProfileMasterDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Profile_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Round_Code", obj.Round_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PROFILE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsProfileMasterDetail)
        Dim obj As ClsProfileMasterDetail = Nothing
        Dim ObjList As New List(Of ClsProfileMasterDetail)
        Dim qry As String = " select *  from TSPL_HR_PROFILE_Detail WHERE Profile_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsProfileMasterDetail()
                obj.Profile_Code = clsCommon.myCstr(dr("Profile_Code"))
                obj.Round_Code = clsCommon.myCstr(dr("Round_Code"))

                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
   
End Class
