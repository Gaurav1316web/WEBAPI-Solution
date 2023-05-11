Imports common
Imports System.Data.SqlClient

Public Class clsIncomeTaxTDSSlabHead
#Region "Variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Fiscal_Code As String = Nothing
    Public Type As String = Nothing
    Public Tax_Group As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As Date? = Nothing
    Public In_Active As Boolean = False
    Public Arr As List(Of clsIncomeTaxTDSSlabDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsIncomeTaxTDSSlabHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsIncomeTaxTDSSlabHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "select Code from TSPL_HR_TDS_INCOME_TAX_SLAB where Fiscal_Code='" + obj.Fiscal_Code + "' and Type='" + obj.Type + "' and Code not in ('" + obj.Code + "')"
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(qry) > 0 Then
                Throw New Exception("Already Created Slap [" + qry + "] having same Fiscal Code [" + obj.Fiscal_Code + "] and Type [" + obj.Type + "]")
            End If

            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL WHERE Code ='" + obj.Code + "'", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Code", obj.Fiscal_Code)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            qry = "select Start_Date from TSPL_Fiscal_Year_Master where fiscal_code='" + obj.Fiscal_Code + "'"
            Dim dtFiscalYearFromDate As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry, trans))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, dtFiscalYearFromDate, clsDocType.IncomeTaxSlab, "", "")
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "In_Active_By", Nothing, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_SLAB", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_SLAB", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            clsIncomeTaxTDSSlabDetail.SaveData(obj.Code, obj.Arr, dtFiscalYearFromDate, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strSchemeCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsIncomeTaxTDSSlabHead
        Dim obj As clsIncomeTaxTDSSlabHead = Nothing
        Dim qry As String = " SELECT TSPL_HR_TDS_INCOME_TAX_SLAB.* FROM TSPL_HR_TDS_INCOME_TAX_SLAB " & _
                            " where  2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Code=(select MIN(Code) from TSPL_HR_TDS_INCOME_TAX_SLAB Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Code=(select Max(Code) from TSPL_HR_TDS_INCOME_TAX_SLAB Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Code=(select Min(Code) from TSPL_HR_TDS_INCOME_TAX_SLAB where Code > '" + strSchemeCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Code=(select Max(Code) from TSPL_HR_TDS_INCOME_TAX_SLAB where Code < '" + strSchemeCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Code='" + strSchemeCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsIncomeTaxTDSSlabHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.In_Active = (clsCommon.myCdbl(dt.Rows(0)("In_Active")) = 1)
            obj.Arr = clsIncomeTaxTDSSlabDetail.GetData(obj.Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strIncentiveCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsIncomeTaxTDSSlabHead
            If clsCommon.myLen(strIncentiveCode) > 0 Then
                obj = clsIncomeTaxTDSSlabHead.GetData(strIncentiveCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL WHERE Code ='" + obj.Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_HR_TDS_INCOME_TAX_SLAB Where Code='" + obj.Code + "'", trans)
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
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_HR_TDS_INCOME_TAX_SLAB", "Code", StrIncentiveNo, "Status=1", trans)
            Dim obj As clsIncomeTaxTDSSlabHead = clsIncomeTaxTDSSlabHead.GetData(StrIncentiveNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_HR_TDS_INCOME_TAX_SLAB set Status='1', Post_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "' , Post_By = '" + objCommonVar.CurrentUserCode + "' where Code='" & StrIncentiveNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function InActiveData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Incentive No not found to Post")
            End If
            Dim obj As clsIncomeTaxTDSSlabHead = clsIncomeTaxTDSSlabHead.GetData(strCode, NavigatorType.Current, Nothing)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Document Should be posted for inactive")
            End If
            Dim strQry As String = " update TSPL_HR_TDS_INCOME_TAX_SLAB set In_Active='1',In_Active_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt") & "' , In_Active_By = '" + objCommonVar.CurrentUserCode + "' where Code='" & strCode & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Code,Description,Fiscal_Code,Type,Tax_Group,case when Status=1 then 'Approved' else 'Pending' end as Status  from TSPL_HR_TDS_INCOME_TAX_SLAB"
        str = clsCommon.ShowSelectForm("ITS@Mfin", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
End Class

Public Class clsIncomeTaxTDSSlabDetail
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public Code As String = Nothing
    Public SNo As Integer = 0
    Public From_Range As Decimal = 0.0
    Public To_Range As Decimal = 0.0
    Public Taxable_Amt As Decimal = 0.0
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Decimal = 0.0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Decimal = 0.0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Decimal = 0.0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Decimal = 0.0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Decimal = 0.0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Decimal = 0.0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Decimal = 0.0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Decimal = 0.0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Decimal = 0.0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Decimal = 0.0
#End Region

    Public Shared Function SaveData(ByVal strIncentiveCode As String, ByVal Arr As List(Of clsIncomeTaxTDSSlabDetail), ByVal dtFiscalYearFromDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsIncomeTaxTDSSlabDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtFiscalYearFromDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "Code", strIncentiveCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "From_Range", obj.From_Range)
                clsCommon.AddColumnsForChange(coll, "To_Range", obj.To_Range)
                clsCommon.AddColumnsForChange(coll, "Taxable_Amt", obj.Taxable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsIncomeTaxTDSSlabDetail)
        Dim arr As New List(Of clsIncomeTaxTDSSlabDetail)
        Dim qry As String = "Select TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL.* from TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL Where Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsIncomeTaxTDSSlabDetail()
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.From_Range = clsCommon.myCdbl(dr("From_Range"))
                obj.To_Range = clsCommon.myCdbl(dr("To_Range"))
                obj.Taxable_Amt = clsCommon.myCdbl(dr("Taxable_Amt"))

                obj.TAX1 = clsCommon.myCstr(dr("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                obj.TAX2 = clsCommon.myCstr(dr("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                obj.TAX3 = clsCommon.myCstr(dr("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                obj.TAX4 = clsCommon.myCstr(dr("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                obj.TAX5 = clsCommon.myCstr(dr("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                obj.TAX6 = clsCommon.myCstr(dr("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                obj.TAX7 = clsCommon.myCstr(dr("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                obj.TAX8 = clsCommon.myCstr(dr("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                obj.TAX9 = clsCommon.myCstr(dr("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                obj.TAX10 = clsCommon.myCstr(dr("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class