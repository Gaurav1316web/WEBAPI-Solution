Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsStateMaster

#Region "Variables"
    Public status As String = Nothing
    Public Code As String
    Public Name As String
    Public IsWayBillRequired As Boolean
    Public COUNTRY_CODE As String
    Public COUNTRY_NAME As String
    Public Zone_Name As String
    Public ISWayBillReq As String
    Public regioncode As String = Nothing
    Public regionname As String = Nothing
    Public GST_UT As Boolean
    Public GSTStateCode As String = Nothing
    Public IsDefault As Integer = 0



#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_State_MASTER.STATE_CODE as [Code],TSPL_State_MASTER.STATE_NAME as [State Name],TSPL_State_MASTER.COUNTRY_CODE as [Country Code],TSPL_State_MASTER.Created_By as [Created By],TSPL_State_MASTER.Created_Date as [Created Date],TSPL_State_MASTER.Modified_By as [Modify By],TSPL_State_MASTER.Modified_Date as [Modify Date],TSPL_State_MASTER.GST_STATE_Code as [GST State Code]  from tspl_state_master"
        str = clsCommon.ShowSelectForm("STMSTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsStateMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_STATE_MASTER where STATE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "delete from TSPL_STATE_MASTER_DETAIL where state_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsStateMaster
        Dim obj As clsStateMaster = Nothing
        Dim qry As String = "select STATE_CODE, STATE_NAME, TSPL_STATE_MASTER.COUNTRY_CODE,TSPL_STATE_MASTER.Is_WayBill_Reqd, TSPL_COUNTRY_MASTER.COUNTRY_NAME,tspl_state_master.Is_GST_UT,tspl_state_master.GST_STATE_Code,tspl_state_master.IsDefault from TSPL_STATE_MASTER left outer join TSPL_COUNTRY_MASTER on TSPL_COUNTRY_MASTER.COUNTRY_CODE =TSPL_STATE_MASTER.COUNTRY_CODE  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and STATE_CODE = (select MIN(STATE_CODE) from TSPL_STATE_MASTER)"
            Case NavigatorType.Last
                qry += " and STATE_CODE = (select Max(STATE_CODE) from TSPL_STATE_MASTER)"
            Case NavigatorType.Next
                qry += " and STATE_CODE = (select Min(STATE_CODE) from TSPL_STATE_MASTER where  STATE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and STATE_CODE = (select Max(STATE_CODE) from TSPL_STATE_MASTER where STATE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and STATE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsStateMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
            obj.COUNTRY_CODE = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
            obj.COUNTRY_NAME = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
            obj.IsWayBillRequired = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_WayBill_Reqd")) = 1, True, False)
            obj.GST_UT = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_GST_UT")) = 1, True, False)
            obj.GSTStateCode = clsCommon.myCstr(dt.Rows(0)("GST_STATE_Code"))
            obj.IsDefault = clsCommon.myCdbl(dt.Rows(0)("IsDefault"))
        End If
        Return obj
    End Function

    Public Shared Function GetDataALL(ByVal strProgramCode As String) As List(Of clsStateMaster)
        Dim Arr As List(Of clsStateMaster) = Nothing
        Dim qry As String = "select TSPL_REGION_MASTER.Region_Code,TSPL_REGION_MASTER.REGION_NAME,(case when TSPL_STATE_MASTER_DETAIL.Region_Code=TSPL_REGION_MASTER.REGION_CODE then 'Y' else 'N' end) as Status from TSPL_REGION_MASTER left outer join TSPL_STATE_MASTER_DETAIL on TSPL_REGION_MASTER.REGION_CODE=TSPL_STATE_MASTER_DETAIL.Region_Code and TSPL_STATE_MASTER_DETAIL.state_code='" + strProgramCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsStateMaster)
            Dim objTr As clsStateMaster
            For Each dr As DataRow In dt.Rows
                objTr = New clsStateMaster

                objTr.regioncode = clsCommon.myCstr(dr("region_code"))
                objTr.regionname = clsCommon.myCstr(dr("region_name"))
                objTr.status = clsCommon.myCstr(dr("status"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Function SaveData(ByVal obj As clsStateMaster, ByVal isNewEntry As Boolean, ByVal Arr As List(Of clsStateMaster)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If obj.IsDefault = 1 Then
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_STATE_MASTER set IsDefault=0 where IsDefault=1")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "STATE_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "COUNTRY_CODE", obj.COUNTRY_CODE)
            'clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Name)
            clsCommon.AddColumnsForChange(coll, "Is_WayBill_Reqd", IIf(obj.IsWayBillRequired, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Is_GST_UT", IIf(obj.GST_UT, 1, 0))
            clsCommon.AddColumnsForChange(coll, "GST_STATE_Code", obj.GSTStateCode)
            clsCommon.AddColumnsForChange(coll, "IsDefault", obj.IsDefault)
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_STATE_MASTER where STATE_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.StateMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.Code)


                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_STATE_MASTER where STATE_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STATE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_STATE_MASTER", OMInsertOrUpdate.Update, "STATE_CODE='" + obj.Code + "'")
            End If

            '-------------------------------entry for region mapped----------------------
            Dim qry1 As String = "select count(*) from TSPL_STATE_MASTER_DETAIL where state_code='" + obj.Code + "'"
            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)

            If check1 > 0 Then
                qry1 = "delete from TSPL_STATE_MASTER_DETAIL where state_code='" + obj.Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry1)
            End If
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each objTr As clsStateMaster In Arr
                    Dim coll1 As New Hashtable()
                    clsCommon.AddColumnsForChange(coll1, "state_code", obj.Code)
                    clsCommon.AddColumnsForChange(coll1, "state_name", obj.Name)
                    clsCommon.AddColumnsForChange(coll1, "country_code", obj.COUNTRY_CODE)
                    clsCommon.AddColumnsForChange(coll1, "Zone_Code", obj.Zone_Name)
                    clsCommon.AddColumnsForChange(coll1, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll1, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll1, "modified_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll1, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll1, "region_code", objTr.regioncode)
                    clsCommon.AddColumnsForChange(coll1, "region_name", objTr.regionname)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_STATE_MASTER_DETAIL", OMInsertOrUpdate.Insert, "")
                Next
            End If
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Dim qry As String = "Select STATE_NAME from TSPL_STATE_MASTER where STATE_CODE='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function GetCodeByName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select STATE_CODE from TSPL_STATE_MASTER where STATE_NAME = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select STATE_CODE from TSPL_STATE_MASTER where STATE_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function GetDefault(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "Select State_Code from TSPL_State_MASTER where IsDefault=1"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class

Public Class ClsRegionMaster
#Region "Variables"
    Public code As String = Nothing
    Public name As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_REGION_MASTER.REGION_CODE as [Code] ,TSPL_REGION_MASTER.REGION_NAME as [Region Name] ,TSPL_REGION_MASTER.Created_By as [Created By] ,TSPL_REGION_MASTER.Created_Date as [Created Date] ,TSPL_REGION_MASTER.Modified_By as [Modified By] ,TSPL_REGION_MASTER.Modified_Date as [Modified Date]  From TSPL_REGION_MASTER  "
        str = clsCommon.ShowSelectForm("RGNFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveData(ByVal obj As ClsRegionMaster, ByVal strCode As String) As Boolean
        Try

            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_REGION_MASTER where REGION_CODE='" & obj.code & "'")
                If ChkNewEntry = 0 Then
                    obj.code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.RegionMaster, "", "")
                    If clsCommon.myLen(obj.code) <= 0 Then
                        Throw New Exception("Error in Region Code Generation")
                    End If
                End If
            End If

            Dim coll As New Hashtable()
            Dim qry As String = "SELECT Count(*) FROM TSPL_REGION_MASTER where REGION_CODE= '" & obj.code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            clsCommon.AddColumnsForChange(coll, "Region_name", obj.name)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If check > 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REGION_MASTER", OMInsertOrUpdate.Update, "REGION_CODE='" + obj.code + "'")
            Else
                clsCommon.AddColumnsForChange(coll, "REGION_CODE", obj.code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REGION_MASTER", OMInsertOrUpdate.Insert, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsRegionMaster
        Try
            Dim obj As ClsRegionMaster = Nothing
            Dim qry As String = "select region_code, region_name from TSPL_region_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and region_code = (select MIN(region_code) from TSPL_region_MASTER)"
                Case NavigatorType.Last
                    qry += " and region_code = (select Max(region_code) from TSPL_region_MASTER)"
                Case NavigatorType.Next
                    qry += " and region_code = (select Min(region_code) from TSPL_region_MASTER where  region_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and region_code = (select Max(region_code) from TSPL_region_MASTER where region_code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and region_code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsRegionMaster()
                obj.code = clsCommon.myCstr(dt.Rows(0)("region_code"))
                obj.name = clsCommon.myCstr(dt.Rows(0)("region_name"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Dim qry As String = "Select REGION_NAME from TSPL_REGION_MASTER where REGION_CODE='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

End Class

Public Class clsBlockMaster
#Region "Variables"
    Public code As String = Nothing
    Public name As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_BLOCK_MASTER.BLOCK_CODE as [Code] ,TSPL_BLOCK_MASTER.BLOCK_NAME as [Block Name] ,TSPL_BLOCK_MASTER.Created_By as [Created By] ,TSPL_BLOCK_MASTER.Created_Date as [Created Date] ,TSPL_BLOCK_MASTER.Modified_By as [Modified By] ,TSPL_BLOCK_MASTER.Modified_Date as [Modified Date]  From TSPL_BLOCK_MASTER  "
        str = clsCommon.ShowSelectForm("RGNFND@Block@MAster", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveData(ByVal obj As clsBlockMaster, ByVal strCode As String) As Boolean
        Try

            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BLOCK_MASTER where BLOCK_CODE='" & obj.code & "'")
                If ChkNewEntry = 0 Then
                    obj.code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.BlockMaster, "", "")
                    If clsCommon.myLen(obj.code) <= 0 Then
                        Throw New Exception("Error in Block Code Generation")
                    End If
                End If
            End If

            Dim coll As New Hashtable()
            Dim qry As String = "SELECT Count(*) FROM TSPL_BLOCK_MASTER where BLOCK_CODE= '" & obj.code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            clsCommon.AddColumnsForChange(coll, "Block_name", obj.name)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If check > 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLOCK_MASTER", OMInsertOrUpdate.Update, "BLOCK_CODE='" + obj.code + "'")
            Else
                clsCommon.AddColumnsForChange(coll, "BLOCK_CODE", obj.code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLOCK_MASTER", OMInsertOrUpdate.Insert, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBlockMaster
        Try
            Dim obj As clsBlockMaster = Nothing
            Dim qry As String = "select Block_code, Block_name from TSPL_Block_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Block_code = (select MIN(Block_code) from TSPL_Block_MASTER)"
                Case NavigatorType.Last
                    qry += " and Block_code = (select Max(Block_code) from TSPL_Block_MASTER)"
                Case NavigatorType.Next
                    qry += " and Block_code = (select Min(Block_code) from TSPL_Block_MASTER where  Block_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Block_code = (select Max(Block_code) from TSPL_Block_MASTER where Block_code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Block_code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBlockMaster()
                obj.code = clsCommon.myCstr(dt.Rows(0)("Block_code"))
                obj.name = clsCommon.myCstr(dt.Rows(0)("Block_name"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Dim qry As String = "Select Block_NAME from TSPL_Block_MASTER where Block_CODE='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

End Class