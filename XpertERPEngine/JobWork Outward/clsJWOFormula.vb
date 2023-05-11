Imports common
Imports System.Data.SqlClient
Public Class clsJWOFormula
#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Formula As String = Nothing
    Public Structure_Code As String = Nothing
    Public Arr As List(Of clsJWOFormulaDetails) = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_JWO_FORMULA.Code as [Code] ,TSPL_JWO_FORMULA.Description as [Description] ,TSPL_JWO_FORMULA.Formula as [Formula] ,TSPL_JWO_FORMULA.Created_By as [Created By] ,TSPL_JWO_FORMULA.Created_Date as [Created Date] ,TSPL_JWO_FORMULA.Modified_By as [Modified By] ,TSPL_JWO_FORMULA.Modified_Date as [Modified Date]  From TSPL_JWO_FORMULA   "
        str = clsCommon.ShowSelectForm("JWFORMULA@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Function SaveData(ByVal arr As List(Of clsJWOFormula), ByVal Arr2 As List(Of clsJWOFormulaDetails), ByVal isNewEntry As Boolean) As Boolean

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

    Public Function SaveData(ByVal arr As List(Of clsJWOFormula), ByVal Arr2 As List(Of clsJWOFormulaDetails), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsJWOFormula In arr
                Dim coll As New Hashtable()
                Try
                    Dim qry As String = "delete from TSPL_JWO_FORMULA_DETAILS where Code='" + obj.Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Catch ex As Exception
                End Try

                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Formula", obj.Formula)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_JWO_FORMULA where Code='" + obj.Code + "' ", trans) <= 0 Then
                    obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.JWOFormula, "", "")
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_FORMULA", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_FORMULA", OMInsertOrUpdate.Update, "TSPL_JWO_FORMULA.Code='" + obj.Code + "'", trans)
                End If
                isSaved = isSaved AndAlso clsJWOFormulaDetails.SaveData(obj.Code, Arr2, trans)
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWOFormula
        Dim obj As clsJWOFormula = Nothing
        Dim qry As String = "SELECT TSPL_JWO_FORMULA.Code,TSPL_JWO_FORMULA.Description,TSPL_JWO_FORMULA.Formula,TSPL_JWO_FORMULA.Structure_Code FROM TSPL_JWO_FORMULA where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JWO_FORMULA.Code = (select MIN(Code) from TSPL_JWO_FORMULA where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_JWO_FORMULA.Code = (select Max(Code) from TSPL_JWO_FORMULA where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_JWO_FORMULA.Code = (select Min(Code) from TSPL_JWO_FORMULA where Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_JWO_FORMULA.Code = (select Max(Code) from TSPL_JWO_FORMULA where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_JWO_FORMULA.Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWOFormula()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Formula = clsCommon.myCstr(dt.Rows(0)("Formula"))
            obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))

            qry = "select Code,Parameter_Code,Value from TSPL_JWO_FORMULA_DETAILS where Code = '" + obj.Code + "'  "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsJWOFormulaDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsJWOFormulaDetails = New clsJWOFormulaDetails()
                    objtr.Code = clsCommon.myCstr(dr("Code"))
                    objtr.Parameter_Code = clsCommon.myCstr(dr("Parameter_Code"))
                    objtr.Value = clsCommon.myCstr(dr("Value")) 'clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Program_Name from TSPL_PROGRAM_MASTER where Program_Code = '" + objtr.Program_Code + "' "))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        Dim qry As String = "delete from TSPL_JWO_FORMULA_DETAILS where Code='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "delete from TSPL_JWO_FORMULA where Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)

    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_JWO_FORMULA where Code='" + strCode + "'"))
    End Function
End Class

Public Class clsJWOFormulaDetails
    Public Code As String = Nothing
    Public Parameter_Code As String
    Public Value As String

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsJWOFormulaDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWOFormulaDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.Parameter_Code)
                clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_FORMULA_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class
Public Class clsJWOVendorFormulaMapping
#Region "Variables"
    Public DocCode As String = Nothing
    Public DocDate As DateTime = Nothing
    Public Arr As List(Of clsJWOVendorFormulaMappingDetail) = Nothing
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select distinct TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode as [Code] ,TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate as [Date],TSPL_JWO_VENDOR_FORMULA_MAPPING.Created_By as [Created By] ,TSPL_JWO_VENDOR_FORMULA_MAPPING.Created_Date as [Created Date]   From TSPL_JWO_VENDOR_FORMULA_MAPPING   "
        str = clsCommon.ShowSelectForm("JWVM@FND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Function SaveData(ByVal obj As clsJWOVendorFormulaMapping, ByVal isNewEntry As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            qry = "delete from TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL where DocCode='" + obj.DocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocDate", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.DocCode = clsERPFuncationality.GetNextCode(tran, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MM/yyyy"), clsDocType.JWOVendorFormula, "", "")
                clsCommon.AddColumnsForChange(coll, "DocCode", obj.DocCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_VENDOR_FORMULA_MAPPING", OMInsertOrUpdate.Insert, "", tran)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_VENDOR_FORMULA_MAPPING", OMInsertOrUpdate.Update, "TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode='" + obj.DocCode + "'", tran)
            End If
            clsJWOVendorFormulaMappingDetail.SaveData(obj.DocCode, obj.Arr, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal DocCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If clsCommon.myLen(DocCode) <= 0 Then
                Throw New Exception("Document No not found")
            End If
            qry = "select top 1 DocCode from TSPL_JWO_VENDOR_FORMULA_MAPPING where DocCode='" + DocCode + "' and posted=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Already Posted [" + DocCode + "]")
            End If
            qry = "delete from TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL where DocCode='" + DocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_JWO_VENDOR_FORMULA_MAPPING where DocCode='" + DocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function
    Public Function PostData(ByVal DocCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If clsCommon.myLen(DocCode) <= 0 Then
                Throw New Exception("Document No not found")
            End If
            qry = "Update TSPL_JWO_VENDOR_FORMULA_MAPPING set posted='1',Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt") + "'  where DocCode='" + DocCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWOVendorFormulaMapping
        Dim obj As clsJWOVendorFormulaMapping = Nothing
        Dim qry As String = "select TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode,TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,TSPL_JWO_VENDOR_FORMULA_MAPPING.VendorCode,TSPL_JWO_FORMULA.Code,TSPL_JWO_FORMULA.Formula,TSPL_JWO_FORMULA.Structure_Code from TSPL_JWO_VENDOR_FORMULA_MAPPING "
        qry += " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code=TSPL_JWO_VENDOR_FORMULA_MAPPING.FormulaCode where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = (select MIN(TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode) from TSPL_JWO_FORMULA where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = (select Max(TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode) from TSPL_JWO_FORMULA where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = (select Min(TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode) from TSPL_JWO_FORMULA where Code>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = (select Max(TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode) from TSPL_JWO_FORMULA where Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWOVendorFormulaMapping()
            obj.DocCode = clsCommon.myCstr(dt.Rows(0)("DocCode"))
            obj.DocDate = clsCommon.myCstr(dt.Rows(0)("DocDate"))
            obj.Arr = clsJWOVendorFormulaMappingDetail.Getdata(obj.DocCode, trans)
        End If
        Return obj
    End Function

End Class

Public Class clsJWOVendorFormulaMappingDetail
#Region "Variables"
    Public DocCode As String = Nothing
    Public Formulacode As String = Nothing
    Public VendorCode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal arr As List(Of clsJWOVendorFormulaMappingDetail), ByVal trans As SqlTransaction) As Boolean
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each obj As clsJWOVendorFormulaMappingDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Formulacode", obj.Formulacode)
                clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsJWOVendorFormulaMappingDetail)
        Dim Arr As List(Of clsJWOVendorFormulaMappingDetail) = Nothing
        Dim qry As String = "select * from TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL where DocCode='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsJWOVendorFormulaMappingDetail)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsJWOVendorFormulaMappingDetail()
                objtr.DocCode = clsCommon.myCstr(dt.Rows(0)("DocCode"))
                objtr.Formulacode = clsCommon.myCstr(dt.Rows(0)("Formulacode"))
                objtr.VendorCode = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
                Arr.Add(objtr)
            Next
        End If
        Return Arr
    End Function
End Class

Public Class clsJWOFormulaSoln
    Public Type As Integer ''1-FAT,2-SNF
    Public EstFATKG As Decimal
    Public EstSNFKG As Decimal

    Public Shared Function CalculateEstimate(ByVal dtDocDate As DateTime, ByVal StrStructreCode As String, ByVal strLocationCode As String, ByVal FatKG As Decimal, ByVal SNFKg As Decimal) As clsJWOFormulaSoln
        Return CalculateEstimate(dtDocDate, StrStructreCode, strLocationCode, FatKG, SNFKg, Nothing)
    End Function
    Public Shared Function CalculateEstimate(ByVal dtDocDate As DateTime, ByVal StrStructreCode As String, ByVal strLocationCode As String, ByVal FatKG As Decimal, ByVal SNFKg As Decimal, ByVal ArrQCParam As Dictionary(Of String, String)) As clsJWOFormulaSoln
        Dim qry As String = "select top 1  TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode from TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL" + Environment.NewLine +
        "left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode=TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode" + Environment.NewLine +
        "left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code=TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode" + Environment.NewLine +
        "where TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted=1 and TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JWO_FORMULA.Structure_Code in ('" + StrStructreCode + "')" + Environment.NewLine +
        "and TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.VendorCode in (select Jobwork_Vendor from TSPL_LOCATION_MASTER where Location_Code ='" + strLocationCode + "') order by TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim strFormulaCode As String = clsCommon.myCstr(dt.Rows(0)("FormulaCode"))
            If clsCommon.myLen(strFormulaCode) > 0 Then
                Return CalculateFormula(strFormulaCode, FatKG, SNFKg, ArrQCParam)
            End If
        End If
        Return Nothing
    End Function

    Public Shared Function CalculateFormula(ByVal strFormulaCode As String, ByVal FatKG As Decimal, ByVal SNFKg As Decimal, ByVal ArrQCParam As Dictionary(Of String, String))
        Dim FATType As Boolean
        Dim SNFType As Boolean
        Dim obj As clsJWOFormulaSoln = Nothing
        Dim qry As String = "select TSPL_JWO_FORMULA.Formula, TSPL_JWO_FORMULA_DETAILS.Parameter_Code,TSPL_JWO_FORMULA_DETAILS.Value,TSPL_JW_PARAMETER_MASTER.Type,TSPL_PARAMETER_MASTER.Code as MainParameterCode from TSPL_JWO_FORMULA_DETAILS " + Environment.NewLine +
                "left outer join TSPL_JW_PARAMETER_MASTER on TSPL_JW_PARAMETER_MASTER.Code=TSPL_JWO_FORMULA_DETAILS.Parameter_Code" + Environment.NewLine +
                "left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.code=TSPL_JWO_FORMULA_DETAILS.Code" + Environment.NewLine +
                "left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_JWO_FORMULA_DETAILS.Parameter_Code and TSPL_JW_PARAMETER_MASTER.Type='N'" + Environment.NewLine +
                "where TSPL_JWO_FORMULA_DETAILS.Code='" + strFormulaCode + "' and TSPL_JWO_FORMULA.Formula   like  '%'+TSPL_JWO_FORMULA_DETAILS.Parameter_Code+'%'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsJWOFormulaSoln()
            qry = clsCommon.myCstr(dt.Rows(0)("Formula"))
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "F") = CompairStringResult.Equal Then
                    FATType = True
                    qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(FatKG))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "S") = CompairStringResult.Equal Then
                    SNFType = True
                    qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(SNFKg))
                Else
                    If clsCommon.myLen(dr("MainParameterCode")) > 0 Then
                        ''BHA/22/10/18-000634 by balwinder on 24/10/2018
                        If ArrQCParam IsNot Nothing AndAlso ArrQCParam.Count > 0 Then
                            If ArrQCParam.ContainsKey(clsCommon.myCstr(dr("Parameter_Code"))) Then
                                If clsCommon.myLen(ArrQCParam.Item(clsCommon.myCstr(dr("Parameter_Code")))) > 0 Then
                                    qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), ArrQCParam.Item(clsCommon.myCstr(dr("Parameter_Code"))))
                                Else
                                    qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(dr("Value")))
                                End If
                            Else
                                qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(dr("Value")))
                            End If
                        Else
                            qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(dr("Value")))
                        End If
                    Else
                        qry = qry.Replace(clsCommon.myCstr(dr("Parameter_Code")), clsCommon.myCstr(dr("Value")))
                    End If
                End If
            Next
            qry = "select (" + qry + ") "
            If FATType AndAlso SNFType Then
                Throw New Exception("Formula - [" + strFormulaCode + "] has FAT And SNF both type parameters")
            ElseIf Not FATType AndAlso Not SNFType Then
                Throw New Exception("Formula - [" + strFormulaCode + "] has at least one FAT And SNF type parameters")
            Else
                If FATType Then
                    obj.Type = 1
                    Try
                        obj.EstFATKG = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    Catch ex As Exception
                    End Try
                ElseIf SNFType Then
                    obj.Type = 2
                    Try
                        obj.EstSNFKG = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    Catch ex As Exception
                    End Try
                Else
                    Throw New Exception("Formula - [" + strFormulaCode + "] something is wrong")
                End If
            End If
        End If
        Return obj
    End Function
End Class