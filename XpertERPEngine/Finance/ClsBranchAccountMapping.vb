Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsBranchAccountMapping

#Region "Variables"
    Public Branch_Account_Map_Code As String = Nothing
    Public From_Location As String = Nothing
    Public To_Location As String = Nothing
    Public Branch_Account As String = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account_Map_Code as [Code] ,TSPL_BRANCH_ACCOUNT_MAPPING.From_Location,TSPL_BRANCH_ACCOUNT_MAPPING.To_Location,TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account ,TSPL_BRANCH_ACCOUNT_MAPPING.Created_By as [Created By] ,Convert(varchar,TSPL_BRANCH_ACCOUNT_MAPPING.Created_Date,103) as [Created Date] ,TSPL_BRANCH_ACCOUNT_MAPPING.Modified_By as [Modified By] ,Convert(varchar,TSPL_BRANCH_ACCOUNT_MAPPING.Modified_Date,103) as [Modified Date]  From TSPL_BRANCH_ACCOUNT_MAPPING "
        str = clsCommon.ShowSelectForm("BranchAccMap", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsBranchAccountMapping
    '    Return GetData(strCode, NavType, Nothing)
    'End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_BRANCH_ACCOUNT_MAPPING where Branch_Account_Map_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal trans As SqlTransaction) As List(Of ClsBranchAccountMapping)
        Dim arr As New List(Of ClsBranchAccountMapping)
        Dim qry As String = "select * from TSPL_BRANCH_ACCOUNT_MAPPING where 2=2"
        'Select Case NavType
        '    Case NavigatorType.First
        '        qry += " and Branch_Account_Map_Code = (select MIN(Branch_Account_Map_Code) from TSPL_BRANCH_ACCOUNT_MAPPING)"
        '    Case NavigatorType.Last
        '        qry += " and Branch_Account_Map_Code = (select Max(Branch_Account_Map_Code) from TSPL_BRANCH_ACCOUNT_MAPPING)"
        '    Case NavigatorType.Next
        '        qry += " and Branch_Account_Map_Code = (select Min(Branch_Account_Map_Code) from TSPL_BRANCH_ACCOUNT_MAPPING where  Branch_Account_Map_Code>'" + strCode + "')"
        '    Case NavigatorType.Previous
        '        qry += " and Branch_Account_Map_Code = (select Max(Branch_Account_Map_Code) from TSPL_BRANCH_ACCOUNT_MAPPING where Branch_Account_Map_Code<'" + strCode + "')"
        '    Case NavigatorType.Current
        '        qry += " and Branch_Account_Map_Code = '" + strCode + "'"
        'End Select
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim obj As New ClsBranchAccountMapping
                'obj.Branch_Account_Map_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Account_Map_Code"))
                obj.From_Location = clsCommon.myCstr(dr("From_Location"))
                obj.To_Location = clsCommon.myCstr(dr("To_Location"))
                obj.Branch_Account = clsCommon.myCstr(dr("Branch_Account"))
                arr.Add(obj)
            Next
            Return arr
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    obj = New ClsBranchAccountMapping()
            '    obj.Branch_Account_Map_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Account_Map_Code"))
            '    obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
            '    obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
            '    obj.Branch_Account = clsCommon.myCstr(dt.Rows(0)("Branch_Account"))
            'End If
            'Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetDataTable(ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_BRANCH_ACCOUNT_MAPPING.From_Location as [From Location],FLocMaster.Description as [From Location Name],TSPL_BRANCH_ACCOUNT_MAPPING.To_Location as [To Location],TLocMaster.Description as [To Location Name],TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account as [Branch Account],TSPL_GL_ACCOUNTS.Description as [Branch Account Name]" + _
        " from TSPL_BRANCH_ACCOUNT_MAPPING " + Environment.NewLine + _
        " left outer join TSPL_GL_SEGMENT_CODE as FLocMaster on FLocMaster.Segment_code=TSPL_BRANCH_ACCOUNT_MAPPING.From_Location " + Environment.NewLine + _
        " left outer join TSPL_GL_SEGMENT_CODE as TLocMaster on TLocMaster.Segment_code=TSPL_BRANCH_ACCOUNT_MAPPING.To_Location" + Environment.NewLine + _
        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code= TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function

    'Public Function SaveData(ByVal obj As ClsBranchAccountMapping, ByVal isNewEntry As Boolean) As Boolean
    '    Dim isSaved As Boolean = True
    '    Try
    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "Branch_Account", obj.Branch_Account)
    '        clsCommon.AddColumnsForChange(coll, "From_Location", obj.From_Location)
    '        clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location)
    '        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
    '        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
    '        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
    '        If isNewEntry Then
    '            clsCommon.AddColumnsForChange(coll, "Branch_Account_Map_Code", obj.Branch_Account_Map_Code)
    '            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
    '            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
    '            Dim qry As String = "SELECT Count(*) FROM TSPL_BRANCH_ACCOUNT_MAPPING WHERE Branch_Account_Map_Code= '" & obj.Branch_Account_Map_Code & "'"
    '            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
    '            If check = 0 Then
    '                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRANCH_ACCOUNT_MAPPING", OMInsertOrUpdate.Insert, "")
    '            Else
    '                common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
    '                Exit Function
    '            End If

    '        Else
    '            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRANCH_ACCOUNT_MAPPING", OMInsertOrUpdate.Update, "Branch_Account_Map_Code='" + obj.Branch_Account_Map_Code + "'")
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return isSaved
    'End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsBranchAccountMapping)) As Boolean
        Dim trans As SqlTransaction = Nothing
        clsCommon.ProgressBarPercentShow()
        Try
            trans = clsDBFuncationality.GetTransactin()
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_BRANCH_ACCOUNT_MAPPING", trans)
            Dim dtCurrent As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Branch_Account", arr(ii).Branch_Account)
                    clsCommon.AddColumnsForChange(coll, "From_Location", arr(ii).From_Location)
                    clsCommon.AddColumnsForChange(coll, "To_Location", arr(ii).To_Location)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", dtCurrent)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", dtCurrent)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRANCH_ACCOUNT_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100 / (arr.Count + 1)), "Saving : " & clsCommon.myCstr(ii + 1) & "/" & clsCommon.myCstr(arr.Count) & "")
                Next
            End If

            Dim qry As String = "select From_Location ,To_Location, SUM(1) as Repeated from tspl_branch_account_Mapping group by From_Location,To_Location  having SUM(1) > 1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Please check ! from location (" & clsCommon.myCstr(dt.Rows(0)("From_Location")) & ") with to location (" & clsCommon.myCstr(dt.Rows(0)("To_Location")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
            End If

            '--Check From location is valid segment
            qry = "select From_Location,to_location,Branch_account from  TSPL_BRANCH_ACCOUNT_MAPPING  where not exists(select 1 from TSPL_GL_SEGMENT_CODE  WHERE TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_BRANCH_ACCOUNT_MAPPING.from_location AND TSPL_GL_SEGMENT_CODE.Seg_No = '7' AND TSPL_GL_SEGMENT_CODE.GIT='N')"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("From location Segment not exists !" + Environment.NewLine + " From location (" & clsCommon.myCstr(dt.Rows(0)("From_Location")) & ") and To location (" & clsCommon.myCstr(dt.Rows(0)("To_Location")) & ")  "" And Branch Account:" & clsCommon.myCstr(dt.Rows(0)("Branch_account")) & " ")
            End If

            '--Check To location is valid segment
            qry = "select From_Location,to_location,Branch_account from  TSPL_BRANCH_ACCOUNT_MAPPING  where not exists( select 1 from TSPL_GL_SEGMENT_CODE   WHERE TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_BRANCH_ACCOUNT_MAPPING.to_location AND TSPL_GL_SEGMENT_CODE.Seg_No = '7' AND TSPL_GL_SEGMENT_CODE.GIT='N')"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("To location Segment not exists !" + Environment.NewLine + " From location (" & clsCommon.myCstr(dt.Rows(0)("From_Location")) & ") and To location (" & clsCommon.myCstr(dt.Rows(0)("To_Location")) & ")  "" And Branch Account:" & clsCommon.myCstr(dt.Rows(0)("Branch_account")) & " ")
            End If

            '--Check Branch Account seg should equalt to from location
            qry = "select From_Location,to_location,Branch_account from ( select From_Location,to_location,Branch_account,isnull( Account_Seg_Code7,'') as Account_Seg_Code7  from  TSPL_BRANCH_ACCOUNT_MAPPING   left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_BRANCH_ACCOUNT_MAPPING.Branch_account )xxx where From_Location<>Account_Seg_Code7"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("From location segmetn and Branch account segment should be same !" + Environment.NewLine + " From location (" & clsCommon.myCstr(dt.Rows(0)("From_Location")) & ") and To location (" & clsCommon.myCstr(dt.Rows(0)("To_Location")) & ")  "" And Branch Account:" & clsCommon.myCstr(dt.Rows(0)("Branch_account")) & " ")
            End If
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBranchAccount(ByVal strFromLocation As String, ByVal strToLocation As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where From_Location='" + strFromLocation + "' and To_Location='" + strToLocation + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
'    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
'        Dim qry As String = "select Cost_Center_Fin_Code from TSPL_BRANCH_ACCOUNT_MAPPING where Cost_Center_Fin_Code ='" + Code + "'   "
'        Dim dt As DataTable
'        dt = clsDBFuncationality.GetDataTable(qry)
'        If dt.Rows.Count > 0 Then
'            Return False
'        Else
'            Return True
'        End If

'    End Function
'End Class
