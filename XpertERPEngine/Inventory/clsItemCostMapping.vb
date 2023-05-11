Imports common
Imports System.Data.SqlClient

Public Class clsItemCostMapping
#Region "Variables"
    Public HCODE As String
    Public DOC_DATE As Date?
    Public Item_Code As String
    Public Item_Desc As String
    Public UOM As String
    Public UOM_DESC As String
    Public GROUP_CODE As String
    Public GROUP_DECS As String
    Public Description As String
    Public Status As Integer = 0
    Public Start_Date As Date?
    Public End_Date As Date?
    Public Created_By As String
    Public Created_Date As Date?
    Public Modify_By As String
    Public Modify_Date As Date?
    Public Posted_By As String
    Public Posted_Date As Date?
    Public Comp_code As String
    Public TOTAL_COST As Double
    Public ArrDetails As List(Of clsItemCostMappingDetails) = Nothing
    Public ArrDetailsAll As List(Of clsItemCostMappingDetailsAll) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsItemCostMapping, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE='" + obj.HCODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITEM_COST_MAPPING_DETAIL where HCODE='" + obj.HCODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "GROUP_CODE", obj.GROUP_CODE)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy hh:mm tt"))
            If obj.End_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                'obj.HCODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.ItemWiseTax, "", "") 'clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.DOC_DATE), clsDocType.ItemCostMapping, "", "")
                'If clsCommon.myLen(obj.HCODE) <= 0 Then
                '    Throw New Exception("Error in code generation")
                'End If
                clsCommon.AddColumnsForChange(coll, "HCODE", obj.HCODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COST_MAPPING_HEADS", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COST_MAPPING_HEADS", OMInsertOrUpdate.Update, "HCODE='" & obj.HCODE & "'", trans)
            End If
            Dim objtr As New clsItemCostMappingDetails
            objtr.SaveData(obj.HCODE, obj.ArrDetails, obj.ArrDetailsAll, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsItemCostMapping
        Dim qry As String = "select * from TSPL_ITEM_COST_MAPPING_HEADS Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ITEM_COST_MAPPING_HEADS.HCODE = (select MIN(HCODE) from TSPL_ITEM_COST_MAPPING_HEADS where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ITEM_COST_MAPPING_HEADS.HCODE = (select Max(HCODE) from TSPL_ITEM_COST_MAPPING_HEADS where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ITEM_COST_MAPPING_HEADS.HCODE = (select Min(HCODE) from TSPL_ITEM_COST_MAPPING_HEADS where HCODE>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ITEM_COST_MAPPING_HEADS.HCODE = (select Max(HCODE) from TSPL_ITEM_COST_MAPPING_HEADS where HCODE<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ITEM_COST_MAPPING_HEADS.HCODE = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsItemCostMapping = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemCostMapping()
            obj.HCODE = clsCommon.myCstr(dt.Rows(0)("HCODE"))
            obj.DOC_DATE = clsCommon.myCDate(dt.Rows(0)("DOC_DATE"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
            obj.GROUP_CODE = clsCommon.myCstr(dt.Rows(0)("GROUP_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
            obj.Start_Date = clsCommon.myCstr(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCstr(dt.Rows(0)("End_Date"))
            Else
                obj.End_Date = Nothing
            End If

            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))

            qry = " select DCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST,RatePerHour,Hours,NO from TSPL_ITEM_COST_MAPPING_DETAIL where HCODE= '" + obj.HCODE + "' order by SNO asc "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrDetails = New List(Of clsItemCostMappingDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsItemCostMappingDetails
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.HCODE = clsCommon.myCstr(dr("HCODE"))
                    objtr.DCODE = clsCommon.myCstr(dr("DCODE"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.UOM = clsCommon.myCstr(dr("UOM"))
                    objtr.COST_CODE = clsCommon.myCstr(dr("COST_CODE"))
                    objtr.COST = clsCommon.myCdbl(dr("COST"))
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("RatePerHour"))) = False Then
                        objtr.RatePerHour = clsCommon.myCdbl(dr("RatePerHour"))
                    Else
                        objtr.RatePerHour = 0
                    End If
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("Hours"))) = False Then
                        objtr.Hours = clsCommon.myCdbl(dr("Hours"))
                    Else
                        objtr.Hours = 0
                    End If
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("NO"))) = False Then
                        objtr.NO = clsCommon.myCdbl(dr("NO"))
                    Else
                        objtr.NO = 0
                    End If
                    obj.ArrDetails.Add(objtr)
                Next
            End If
            ', RatePerHour,Hours,NO 
            qry = " select DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE= '" + obj.HCODE + "' order by SNO asc "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrDetailsAll = New List(Of clsItemCostMappingDetailsAll)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsItemCostMappingDetailsAll
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.DDCODE = clsCommon.myCstr(dr("DDCODE"))
                    objtr.HCODE = clsCommon.myCstr(dr("HCODE"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.UOM = clsCommon.myCstr(dr("UOM"))
                    objtr.COST_CODE = clsCommon.myCstr(dr("COST_CODE"))
                    objtr.COST = clsCommon.myCdbl(dr("COST"))
                    'If String.IsNullOrEmpty(clsCommon.myCstr(dr("RatePerHour"))) = False Then
                    '    objtr.RatePerHour = clsCommon.myCdbl(dr("RatePerHour"))
                    'Else
                    '    objtr.RatePerHour = 0
                    'End If
                    'If String.IsNullOrEmpty(clsCommon.myCstr(dr("Hours"))) = False Then
                    '    objtr.Hours = clsCommon.myCdbl(dr("Hours"))
                    'Else
                    '    objtr.Hours = 0
                    'End If
                    'If String.IsNullOrEmpty(clsCommon.myCstr(dr("NO"))) = False Then
                    '    objtr.NO = clsCommon.myCdbl(dr("NO"))
                    'Else
                    '    objtr.NO = 0
                    'End If
                    obj.ArrDetailsAll.Add(objtr)
                Next
            End If

        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document Number not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsItemCostMapping = clsItemCostMapping.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.HCODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted on :" & obj.Modify_Date & " ")
            End If

            Dim qry As String = "Update TSPL_ITEM_COST_MAPPING_HEADS set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where HCODE='" + strDocNo + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As clsItemCostMapping = clsItemCostMapping.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HCODE) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_ITEM_COST_MAPPING_DETAILS_ALL where HCODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_ITEM_COST_MAPPING_DETAIL where HCODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_ITEM_COST_MAPPING_HEADS where HCODE='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function getGroupFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select GROUP_CODE as Code ,Description , Created_By ,convert(varchar, Created_Date,103) as Created_Date  from TSPL_OVERHEAD_COST_GROUP_HEAD "
        str = clsCommon.ShowSelectForm("OverCostGroupFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getItemFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select Item_Code as Code, Item_Desc as Descriiption , HSN_code From TSPL_ITEM_MASTER"
        str = clsCommon.ShowSelectForm("OverCostGroupFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getUOMFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select UOM_Code as Code,UOM_Description as Description from TSPL_ITEM_UOM_DETAIL "
        str = clsCommon.ShowSelectForm("OverCostGroupFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetGroupData(ByVal strGroupCode As String, ByVal tran As SqlTransaction) As clsItemCostMapping
        Dim qry As String = ""
        If IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True) = True Then
            qry = " select  TSPL_OVERHEAD_COST_GROUP_DETAILS.SNO as SNo,TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE as [Cost Code],TSPL_OVERHEAD_COST.Description as [Cost Description] , TSPL_OVERHEAD_COST_GROUP_DETAILS.RatePerHour , TSPL_OVERHEAD_COST_GROUP_DETAILS.Hours, TSPL_OVERHEAD_COST_GROUP_DETAILS.No ,TSPL_OVERHEAD_COST_GROUP_DETAILS.Cost from TSPL_OVERHEAD_COST_GROUP_DETAILS left outer join TSPL_OVERHEAD_COST on  TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE =TSPL_OVERHEAD_COST.COST_CODE where TSPL_OVERHEAD_COST_GROUP_DETAILS.GROUP_CODE = '" + strGroupCode + "' order by TSPL_OVERHEAD_COST_GROUP_DETAILS.SNO "
        Else
            qry = " select  TSPL_OVERHEAD_COST_GROUP_DETAILS.SNO as SNo,TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE as [Cost Code],TSPL_OVERHEAD_COST.Description as [Cost Description] , 0.0 as Cost from TSPL_OVERHEAD_COST_GROUP_DETAILS left outer join TSPL_OVERHEAD_COST on  TSPL_OVERHEAD_COST_GROUP_DETAILS.COST_CODE =TSPL_OVERHEAD_COST.COST_CODE where TSPL_OVERHEAD_COST_GROUP_DETAILS.GROUP_CODE = '" + strGroupCode + "' order by TSPL_OVERHEAD_COST_GROUP_DETAILS.SNO "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

        Dim obj As clsItemCostMapping = Nothing
        obj = New clsItemCostMapping()
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.ArrDetails = New List(Of clsItemCostMappingDetails)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsItemCostMappingDetails
                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.COST_CODE = clsCommon.myCstr(dr("Cost Code"))
                objtr.COST_DESC = clsCommon.myCstr(dr("Cost Description"))
                objtr.COST = clsCommon.myCdbl(dr("Cost"))
                If IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True) = True Then
                    objtr.RatePerHour = clsCommon.myCdbl(dr("RatePerHour"))
                    objtr.Hours = clsCommon.myCdbl(dr("Hours"))
                    objtr.NO = clsCommon.myCdbl(dr("NO"))
                End If
                obj.ArrDetails.Add(objtr)
            Next
        End If
        Return obj
    End Function

End Class
Public Class clsItemCostMappingDetails
#Region "Variables"
    Public DCODE As String
    Public HCODE As String
    Public Item_Code As String
    Public UOM As String
    Public SNO As Integer
    Public COST_CODE As String
    Public COST_DESC As String
    Public COST As Double

    Public RatePerHour As Double
    Public Hours As Double
    Public NO As Double



#End Region
    Public Function SaveData(ByVal strCode As String, ByVal ArrGroup As List(Of clsItemCostMappingDetails), ByVal ArrAuth As List(Of clsItemCostMappingDetailsAll), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtr As clsItemCostMappingDetails In ArrGroup
                'ii += 1
                'objtr.SNO = ii
                'objtr.DCODE = clsCommon.myCstr(strCode) + "-" + clsCommon.myCstr(objtr.SNO)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "HCODE", objtr.HCODE)
                clsCommon.AddColumnsForChange(coll, "DCODE", objtr.DCODE)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                clsCommon.AddColumnsForChange(coll, "COST_CODE", objtr.COST_CODE)
                clsCommon.AddColumnsForChange(coll, "COST", objtr.COST)
                clsCommon.AddColumnsForChange(coll, "RatePerHour", objtr.RatePerHour, True)
                clsCommon.AddColumnsForChange(coll, "Hours", objtr.Hours, True)
                clsCommon.AddColumnsForChange(coll, "NO", objtr.NO, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COST_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
            Dim objtrDetails As New clsItemCostMappingDetailsAll
            objtrDetails.SaveData("", strCode, ArrAuth, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsItemCostMappingDetailsAll
#Region "Variables"
    Public DDCODE As String
    Public HCODE As String
    Public Item_Code As String
    Public UOM As String
    Public SNO As Integer
    Public COST_CODE As String
    Public COST_DESC As String
    Public COST As Decimal
    'Public RatePerHour As Double
    'Public Hours As Double
    'Public NO As Double
    Public Function SaveData(ByVal strCode As String, ByVal strHCode As String, ByVal Arr As List(Of clsItemCostMappingDetailsAll), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtrDetailsAll As clsItemCostMappingDetailsAll In Arr
                'ii += 1
                ' objtrAuth.SNO = ii
                ' objtrAuth.DDCODE = clsCommon.myCstr(strCode) + "-" + clsCommon.myCstr(objtrAuth.SNO)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DDCODE", objtrDetailsAll.DDCODE)
                clsCommon.AddColumnsForChange(coll, "HCODE", objtrDetailsAll.HCODE)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtrDetailsAll.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtrDetailsAll.UOM)
                clsCommon.AddColumnsForChange(coll, "SNO", objtrDetailsAll.SNO)
                clsCommon.AddColumnsForChange(coll, "COST_CODE", objtrDetailsAll.COST_CODE)
                clsCommon.AddColumnsForChange(coll, "COST", objtrDetailsAll.COST)
                'clsCommon.AddColumnsForChange(coll, "RatePerHour", objtrDetailsAll.RatePerHour, True)
                'clsCommon.AddColumnsForChange(coll, "Hours", objtrDetailsAll.Hours, True)
                'clsCommon.AddColumnsForChange(coll, "NO", objtrDetailsAll.Hours, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COST_MAPPING_DETAILS_ALL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    'Public Shared Function GetAutoItemwiseTaxRate(ByVal strItemCode As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strDocDate As Date, ByVal strTaxType As String) As clsItemWiseTaxAuthority
    '    Dim obj As clsItemWiseTaxAuthority = Nothing
    '    Dim dblTaxRate As Double = 0
    '    Dim intExemptedType As Integer = clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & strTaxGroup & "'")
    '    If intExemptedType = 0 Then
    '        If clsCommon.myLen(strItemCode) = 0 Then
    '            Throw New Exception("Please enter Item Code")
    '        ElseIf clsCommon.myLen(strTaxGroup) = 0 Then
    '            Throw New Exception("Please enter Tax Group")
    '        ElseIf clsCommon.myLen(strTaxCode) = 0 Then
    '            Throw New Exception("Please enter Tax Code")
    '        ElseIf clsCommon.myLen(strTaxType) = 0 Then
    '            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
    '        End If
    '        Dim qry = "select top 1 * from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " & _
    '            "left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " & _
    '            "and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " & _
    '            "where Status=1 and DOC_DATE < ='" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' and   Tax_Group_Code='" & strTaxGroup & "' and Tax_Authority='" & strTaxCode & "' and Item_Code='" & strItemCode & "' order by DOC_DATE desc "
    '        Dim dt As DataTable
    '        dt = clsDBFuncationality.GetDataTable(qry)
    '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '            obj = New clsItemWiseTaxAuthority
    '            obj.HCODE = clsCommon.myCstr(dt.Rows(0)("HCODE"))
    '            obj.TAX_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX_Rate"))
    '        Else
    '            Throw New Exception("Please enter Tax Rate For  " + Environment.NewLine + _
    '                                       "Item    --  " & strItemCode & " " + Environment.NewLine + _
    '                                       "Tax Group  --   " & strTaxGroup & " " + Environment.NewLine + _
    '                                       "Tax Code  --   " & strTaxCode & " " + Environment.NewLine + _
    '                                       "Tax Type --   " & strTaxType & " ")

    '        End If


    '    End If
    '    Return obj
    'End Function
#End Region
End Class

