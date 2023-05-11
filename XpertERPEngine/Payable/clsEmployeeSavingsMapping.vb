Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmployeeSavingsMappingHead

#Region "Variables"
    Public DOCUMENT_CODE As String
    Public DOCUMENT_DATE As Date? = Nothing
    Public EMP_CODE As String = Nothing
    Public Fiscal_Code As String = Nothing
    Public ArrEmployeeSavingsMappingDetails As List(Of clsEmployeeSavingsMappingDetail) = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE as [Code] ,TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Fiscal_Code,TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.EMP_CODE as [Employee Code],TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Created_By as [Created By] ,TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Created_Date as [Created Date] ,TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Modified_By as [Modified By] ,TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Modified_Date as [Modified Date]  From TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER   "
        ' " left join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code=TSPL_SAVINGS_MASTER.Section_Code "
        str = clsCommon.ShowSelectForm("SavingsMaster@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal arr As List(Of clsEmployeeSavingsMappingHead), ByVal Arr2 As List(Of clsEmployeeSavingsMappingDetail), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            isSaved = SaveData(arr, Arr2, isNewEntry, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

    Public Function SaveData(ByVal arr As List(Of clsEmployeeSavingsMappingHead), ByVal Arr2 As List(Of clsEmployeeSavingsMappingDetail), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsEmployeeSavingsMappingHead In arr
                Dim coll As New Hashtable()
                '=================
                Try
                    Dim qry As String = "delete from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL where DOCUMENT_CODE='" + obj.DOCUMENT_CODE + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Catch ex As Exception

                End Try

                '========================
                'clsCommon.AddColumnsForChange(coll, "DOCUMENT_DATE", clsCommon.GetPrintDate(obj.DOCUMENT_DATE, "dd/MMM/yyyy hh:mm:ss tt "))
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "Fiscal_Code", obj.Fiscal_Code)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE='" + obj.DOCUMENT_CODE + "' ", trans) <= 0 Then
                    isNewEntry = True
                    obj.DOCUMENT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsDocType.InvestmentDeclarationMaster, "", "")  'InvestmentDeclarationMaster
                    clsCommon.AddColumnsForChange(coll, "DOCUMENT_CODE", obj.DOCUMENT_CODE)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isNewEntry = False
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE='" + obj.DOCUMENT_CODE + "'", trans)
                End If
                clsEmployeeSavingsMappingDetail.SaveData(obj.DOCUMENT_CODE, Arr2, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), trans)
                If isNewEntry = False Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.DOCUMENT_CODE, "TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER", "DOCUMENT_CODE", "TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL", "DOCUMENT_CODE", trans)
                End If
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeSavingsMappingHead
        Dim obj As clsEmployeeSavingsMappingHead = Nothing
        Dim qry As String = "SELECT TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.* FROM TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE = (select MIN(DOCUMENT_CODE) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE = (select Min(DOCUMENT_CODE) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeSavingsMappingHead()
            obj.DOCUMENT_CODE = clsCommon.myCstr(dt.Rows(0)("DOCUMENT_CODE"))
            ' obj.DOCUMENT_DATE = clsCommon.myCDate(dt.Rows(0)("DOCUMENT_DATE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.ArrEmployeeSavingsMappingDetails = clsEmployeeSavingsMappingDetail.GetData(obj.DOCUMENT_CODE, trans)
        End If

        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim qry As String = "delete from TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment  where DOCUMENT_CODE='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        qry = "delete from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL  where DOCUMENT_CODE='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        qry = "delete from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE='" + strCode + "'"))
    End Function

    Public Shared Function GetDocumentByte(ByVal strDocumentCode As String, ByVal strTRCode As String) As DataTable
        Dim qry As String = " select * from TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment where DOCUMENT_CODE='" + strDocumentCode + "' and TR_CODE = '" + strTRCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("FileData")
        Return dt
    End Function
End Class

Public Class clsEmployeeSavingsMappingDetail
#Region "Variables"
    Public SNO As Integer = 0
    Public TR_CODE As String = Nothing
    Public DOCUMENT_CODE As String = Nothing
    Public SAVINGS_CODE As String = Nothing
    Public Section_Code As String = Nothing
    Public MaxLimit As Double = 0.0
    Public AMOUNT As Double = 0.0
    ' For Attachment
    Public FileName As String = Nothing
    Public FileData As Byte()
    Public Path As String = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocumentCode As String, ByVal Arr As List(Of clsEmployeeSavingsMappingDetail), ByVal dtDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEmployeeSavingsMappingDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "DOCUMENT_CODE", strDocumentCode)
                clsCommon.AddColumnsForChange(coll, "SAVINGS_CODE", obj.SAVINGS_CODE)
                clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
                clsCommon.AddColumnsForChange(coll, "MaxLimit", obj.MaxLimit)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.AMOUNT)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                ' for Attachment---------
                If clsCommon.myLen(obj.FileName) > 0 Then
                    If obj.FileData.Length > 0 Then
                        Dim str As String
                        str = " Insert into TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment  (TR_CODE,DOCUMENT_CODE,FileName,FileData) values ('" + obj.TR_CODE + "','" + strDocumentCode + "','" + obj.FileName + "',@BLOBData ) "
                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection, trans)
                        Dim prm As New SqlParameter("@BLOBData", obj.FileData)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                    End If
                End If
                If clsCommon.myLen(obj.Path) > 0 Then
                    Dim qry As String = "Update TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment set TR_CODE = '" + obj.TR_CODE + "' where TR_CODE = '" + obj.Path + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsEmployeeSavingsMappingDetail)
        Dim arr As New List(Of clsEmployeeSavingsMappingDetail)
        Dim qry As String = " Select TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.*, TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment.FileName from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL Left Outer Join TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment   on TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.DOCUMENT_CODE =TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment.DOCUMENT_CODE and TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.TR_CODE = TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment.TR_CODE   Where TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.DOCUMENT_CODE='" + strCode + "' order by SNO asc "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsEmployeeSavingsMappingDetail()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.SNO = clsCommon.myCdbl(dr("SNO"))
                obj.DOCUMENT_CODE = clsCommon.myCstr(dr("DOCUMENT_CODE"))
                obj.SAVINGS_CODE = clsCommon.myCstr(dr("SAVINGS_CODE"))
                obj.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                obj.MaxLimit = clsCommon.myCdbl(dr("MaxLimit"))
                obj.AMOUNT = clsCommon.myCdbl(dr("AMOUNT"))
                obj.FileName = clsCommon.myCstr(dr("FileName"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
