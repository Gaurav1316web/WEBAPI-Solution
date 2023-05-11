Imports common
Imports System.Data.SqlClient

Public Class clsIncomeTaxTDSCalculationHead
#Region "Variables"
    Public Code As String = Nothing
    Public Doc_Date As DateTime
    Public Description As String = Nothing
    Public Fiscal_Code As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As Date? = Nothing

    Public ArrEmp As List(Of clsIncomeTaxTDSCalculationEmp) = Nothing
    Public ArrDetail As List(Of clsIncomeTaxTDSCalculationDetail) = Nothing
    Public ArrTax As List(Of clsIncomeTaxTDSCalculationTax) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsIncomeTaxTDSCalculationHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsIncomeTaxTDSCalculationHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX WHERE Code ='" + obj.Code + "'", trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Code", obj.Fiscal_Code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.IncomeTaxCalculation, "", "")
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            clsIncomeTaxTDSCalculationEmp.SaveData(obj.Code, obj.Doc_Date, obj.Fiscal_Code, obj.ArrEmp, trans)
            clsIncomeTaxTDSCalculationDetail.SaveData(obj.Code, obj.Doc_Date, obj.ArrDetail, trans)
            clsIncomeTaxTDSCalculationTax.SaveData(obj.Code, obj.Doc_Date, obj.ArrTax, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strSchemeCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsIncomeTaxTDSCalculationHead
        Dim obj As clsIncomeTaxTDSCalculationHead = Nothing
        Dim qry As String = " SELECT TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.* FROM TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD " & _
                            " where  2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=(select MIN(Code) from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=(select Max(Code) from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=(select Min(Code) from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD where Code > '" + strSchemeCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=(select Max(Code) from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD where Code < '" + strSchemeCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code='" + strSchemeCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsIncomeTaxTDSCalculationHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.ArrEmp = clsIncomeTaxTDSCalculationEmp.GetData(obj.Code, trans)
            obj.ArrDetail = clsIncomeTaxTDSCalculationDetail.GetData(obj.Code, trans)
            obj.ArrTax = clsIncomeTaxTDSCalculationTax.GetData(obj.Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strIncentiveCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsIncomeTaxTDSCalculationHead
            If clsCommon.myLen(strIncentiveCode) > 0 Then
                obj = clsIncomeTaxTDSCalculationHead.GetData(strIncentiveCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD Where Code='" + obj.Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrIncentiveNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrIncentiveNo) <= 0) Then
                Throw New Exception(" Incentive No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD", "Code", StrIncentiveNo, "Status=1", trans)
            Dim obj As clsIncomeTaxTDSCalculationHead = clsIncomeTaxTDSCalculationHead.GetData(StrIncentiveNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD set Status='1', Post_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "' , Post_By = '" + objCommonVar.CurrentUserCode + "' where Code='" & StrIncentiveNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Code,Doc_Date,Description,Fiscal_Code,case when Status=1 then 'Approved' else 'Pending' end as Status from TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD"
        str = clsCommon.ShowSelectForm("ITC@Mfin", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
End Class

Public Class clsIncomeTaxTDSCalculationEmp
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public Code As String = Nothing
    Public SNo As Integer = 0
    Public Emp_Code As String
    Public Emp_Name As String ''Not a Table Column
    Public Gross_Amt As Decimal = 0.0
    Public Allowance_Amt As Decimal = 0.0
    Public Section_Amt As Decimal = 0.0
    Public Taxable_Amt As Decimal = 0.0
    Public Tax_Group As String
    Public Total_TDS_Amt As Decimal = 0.0
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal dtDocDate As DateTime, ByVal strFiscalCode As String, ByVal Arr As List(Of clsIncomeTaxTDSCalculationEmp), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIncomeTaxTDSCalculationEmp In Arr
                Dim qry As String = "select TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD on TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code where TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code='" + strFiscalCode + "' and TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code ='" + obj.Emp_Code + "' and TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code not in ('" + obj.Code + "')"
                qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(qry) > 0 Then
                    Throw New Exception("Already calculated TDS Document No [" + qry + "] having same Fiscal Code [" + strFiscalCode + "]  and Employee [" + obj.Emp_Code + "]")
                End If

                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Gross_Amt", obj.Gross_Amt)
                clsCommon.AddColumnsForChange(coll, "Allowance_Amt", obj.Allowance_Amt)
                clsCommon.AddColumnsForChange(coll, "Section_Amt", obj.Section_Amt)
                clsCommon.AddColumnsForChange(coll, "Taxable_Amt", obj.Taxable_Amt)
                clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
                clsCommon.AddColumnsForChange(coll, "Total_TDS_Amt", obj.Total_TDS_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsIncomeTaxTDSCalculationEmp)
        Dim arr As New List(Of clsIncomeTaxTDSCalculationEmp)
        Dim qry As String = "Select TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.*,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code Where TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsIncomeTaxTDSCalculationEmp()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                obj.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                obj.Gross_Amt = clsCommon.myCdbl(dr("Gross_Amt"))
                obj.Allowance_Amt = clsCommon.myCdbl(dr("Allowance_Amt"))
                obj.Section_Amt = clsCommon.myCdbl(dr("Section_Amt"))
                obj.Taxable_Amt = clsCommon.myCdbl(dr("Taxable_Amt"))
                obj.Total_TDS_Amt = clsCommon.myCdbl(dr("Total_TDS_Amt"))
                obj.Tax_Group = clsCommon.myCstr(dr("Tax_Group"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsIncomeTaxTDSCalculationDetail
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public Code As String = Nothing
    Public SNo As Integer = 0
    Public Emp_Code As String
    Public Emp_Name As String ''Not a Table Column
    Public Type As String
    Public Type_Code As String
    Public Gross_Amt As Decimal = 0.0
    Public Limit_Amt As Decimal = 0.0
    Public Applicable_Amt As Decimal = 0.0
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsIncomeTaxTDSCalculationDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIncomeTaxTDSCalculationDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                clsCommon.AddColumnsForChange(coll, "Type_Code", obj.Type_Code, True)
                clsCommon.AddColumnsForChange(coll, "Gross_Amt", obj.Gross_Amt)
                clsCommon.AddColumnsForChange(coll, "Limit_Amt", obj.Limit_Amt)
                clsCommon.AddColumnsForChange(coll, "Applicable_Amt", obj.Applicable_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsIncomeTaxTDSCalculationDetail)
        Dim arr As New List(Of clsIncomeTaxTDSCalculationDetail)
        Dim qry As String = "Select TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.*,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Emp_Code  Where TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsIncomeTaxTDSCalculationDetail()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                obj.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                obj.Type = clsCommon.myCstr(dr("Type"))
                obj.Type_Code = clsCommon.myCstr(dr("Type_Code"))
                obj.Gross_Amt = clsCommon.myCdbl(dr("Gross_Amt"))
                obj.Limit_Amt = clsCommon.myCdbl(dr("Limit_Amt"))
                obj.Applicable_Amt = clsCommon.myCdbl(dr("Applicable_Amt"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsIncomeTaxTDSCalculationTax
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public Code As String = Nothing
    Public SNo As Integer = 0
    Public Emp_Code As String
    Public Emp_Name As String ''Not a Table Column
    Public Slab_Code As String
    Public Slab_Code_TR As String
    Public Slab_Taxable_Amt As Decimal = 0.0
    Public TAX1_Base_Amount As Decimal = 0.0
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Decimal = 0.0
    Public TAX1_Amt As Decimal = 0.0
    Public TAX2_Base_Amount As Decimal = 0.0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Decimal = 0.0
    Public TAX2_Amt As Decimal = 0.0
    Public TAX3_Base_Amount As Decimal = 0.0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Decimal = 0.0
    Public TAX3_Amt As Decimal = 0.0
    Public TAX4_Base_Amount As Decimal = 0.0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Decimal = 0.0
    Public TAX4_Amt As Decimal = 0.0
    Public TAX5_Base_Amount As Decimal = 0.0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Decimal = 0.0
    Public TAX5_Amt As Decimal = 0.0
    Public TAX6_Base_Amount As Decimal = 0.0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Decimal = 0.0
    Public TAX6_Amt As Decimal = 0.0
    Public TAX7_Base_Amount As Decimal = 0.0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Decimal = 0.0
    Public TAX7_Amt As Decimal = 0.0
    Public TAX8_Base_Amount As Decimal = 0.0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Decimal = 0.0
    Public TAX8_Amt As Decimal = 0.0
    Public TAX9_Base_Amount As Decimal = 0.0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Decimal = 0.0
    Public TAX9_Amt As Decimal = 0.0
    Public TAX10_Base_Amount As Decimal = 0.0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Decimal = 0.0
    Public TAX10_Amt As Decimal = 0.0
    Public TDS_Amt As Decimal = 0.0
#End Region

    Public Shared Function SaveData(ByVal strIncentiveCode As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsIncomeTaxTDSCalculationTax), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIncomeTaxTDSCalculationTax In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "Code", strIncentiveCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Slab_Code", obj.Slab_Code)
                clsCommon.AddColumnsForChange(coll, "Slab_Code_TR", obj.Slab_Code_TR)
                clsCommon.AddColumnsForChange(coll, "Slab_Taxable_Amt", obj.Slab_Taxable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amount", obj.TAX1_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amount", obj.TAX2_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amount", obj.TAX3_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amount", obj.TAX4_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amount", obj.TAX5_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amount", obj.TAX6_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amount", obj.TAX7_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amount", obj.TAX8_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amount", obj.TAX9_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amount", obj.TAX10_Base_Amount)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "TDS_Amt", obj.TDS_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsIncomeTaxTDSCalculationTax)
        Dim arr As New List(Of clsIncomeTaxTDSCalculationTax)
        Dim qry As String = "Select TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.*,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.Emp_Code  Where TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsIncomeTaxTDSCalculationTax()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_Code"))
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                obj.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                obj.Slab_Code = clsCommon.myCstr(dr("Slab_Code"))
                obj.Slab_Code_TR = clsCommon.myCstr(dr("Slab_Code_TR"))
                obj.Slab_Taxable_Amt = clsCommon.myCdbl(dr("Slab_Taxable_Amt"))
                obj.TAX1_Base_Amount = clsCommon.myCdbl(dr("TAX1_Base_Amount"))
                obj.TAX1 = clsCommon.myCstr(dr("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                obj.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                obj.TAX2_Base_Amount = clsCommon.myCdbl(dr("TAX2_Base_Amount"))
                obj.TAX2 = clsCommon.myCstr(dr("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                obj.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                obj.TAX3_Base_Amount = clsCommon.myCdbl(dr("TAX3_Base_Amount"))
                obj.TAX3 = clsCommon.myCstr(dr("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                obj.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                obj.TAX4_Base_Amount = clsCommon.myCdbl(dr("TAX4_Base_Amount"))
                obj.TAX4 = clsCommon.myCstr(dr("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                obj.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                obj.TAX5_Base_Amount = clsCommon.myCdbl(dr("TAX5_Base_Amount"))
                obj.TAX5 = clsCommon.myCstr(dr("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                obj.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                obj.TAX6_Base_Amount = clsCommon.myCdbl(dr("TAX6_Base_Amount"))
                obj.TAX6 = clsCommon.myCstr(dr("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                obj.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                obj.TAX2_Base_Amount = clsCommon.myCdbl(dr("TAX2_Base_Amount"))
                obj.TAX7 = clsCommon.myCstr(dr("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                obj.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                obj.TAX8_Base_Amount = clsCommon.myCdbl(dr("TAX8_Base_Amount"))
                obj.TAX8 = clsCommon.myCstr(dr("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                obj.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                obj.TAX9_Base_Amount = clsCommon.myCdbl(dr("TAX9_Base_Amount"))
                obj.TAX9 = clsCommon.myCstr(dr("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                obj.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                obj.TAX10_Base_Amount = clsCommon.myCdbl(dr("TAX10_Base_Amount"))
                obj.TAX10 = clsCommon.myCstr(dr("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                obj.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                obj.TDS_Amt = clsCommon.myCdbl(dr("TDS_Amt"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class