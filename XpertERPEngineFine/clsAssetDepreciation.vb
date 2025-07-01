Imports common
Imports System.Data.SqlClient

Public Class clsAssetDepreciation
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public Asset_code As String = Nothing
    Public Dep_Method_Code As String = Nothing
    Public Dep_Period_Code As String = Nothing
    Public Value_Before_Depreciation As Double = 0
    Public Asset_value As Double = 0
    Public Dep_Amount As Double = 0
    Public Asset_Disposal_Code As String = String.Empty
    Public Dep_Method_Tax_Code As String = Nothing
    Public Value_Before_Depreciation_Tax As Double = 0
    Public Asset_value_Tax As Double = 0
    Public Dep_Amount_Tax As Double = 0
    Public Is_Permanent As String

    Public Opening_Value As Decimal = 0
    Public Opening_Value_Tax As Decimal = 0
    Public Work_Expense As Decimal = 0
    Public Assemble_Cost As Decimal = 0
    Public Disposable_Value As Decimal = 0
    Public Accumulated_Dep As Decimal = 0
    Public Location_Code As String = ""
    Public Asset_Group_Code As String = ""
    Public Asset_Category_Code As String = ""
    Public Is_Reverse_Dep As Integer = 0
    Public Reverse_Date As Date?

    Public DepRate As Double = 0
    Public DepRateTax As Double = 0
    Public RoundOffRate As Double = 0
    Public RoundOffAmount As Double = 0
    Public Tax_Recoverable As Double = 0
    Public Tax_Non_Recoverable As Double = 0

    ''Not Table Coulmns
    Public Asset_Name As String = Nothing


    Public Shared Function SaveData(ByVal Arr As List(Of clsAssetDepreciation)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData1(Arr, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function SaveData1(ByVal Arr As List(Of clsAssetDepreciation), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()


        For Each obj As clsAssetDepreciation In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Asset_code", obj.Asset_code)
            clsCommon.AddColumnsForChange(coll, "Dep_Method_Code", obj.Dep_Method_Code)
            clsCommon.AddColumnsForChange(coll, "Dep_Period_Code", obj.Dep_Period_Code)
            clsCommon.AddColumnsForChange(coll, "Value_Before_Depreciation", obj.Value_Before_Depreciation)
            clsCommon.AddColumnsForChange(coll, "Asset_value", obj.Asset_value)
            clsCommon.AddColumnsForChange(coll, "Dep_Amount", obj.Dep_Amount)
            clsCommon.AddColumnsForChange(coll, "DepRate", obj.DepRate)
            clsCommon.AddColumnsForChange(coll, "RoundOff", obj.RoundOffRate)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable", obj.Tax_Recoverable)
            clsCommon.AddColumnsForChange(coll, "Tax_Non_Recoverable", obj.Tax_Non_Recoverable)
            clsCommon.AddColumnsForChange(coll, "Dep_Method_Tax_Code", obj.Dep_Method_Tax_Code, True)
            clsCommon.AddColumnsForChange(coll, "Value_Before_Depreciation_Tax", obj.Value_Before_Depreciation_Tax)
            clsCommon.AddColumnsForChange(coll, "Asset_value_Tax", obj.Asset_value_Tax)
            clsCommon.AddColumnsForChange(coll, "Dep_Amount_Tax", obj.Dep_Amount_Tax)
            clsCommon.AddColumnsForChange(coll, "DepRateTax", obj.DepRateTax)
            clsCommon.AddColumnsForChange(coll, "Is_Permanent", obj.Is_Permanent)

            clsCommon.AddColumnsForChange(coll, "Opening_Value", obj.Opening_Value)
            clsCommon.AddColumnsForChange(coll, "Opening_Value_Tax", obj.Opening_Value_Tax)
            clsCommon.AddColumnsForChange(coll, "Work_Expense", obj.Work_Expense)
            clsCommon.AddColumnsForChange(coll, "Assemble_Cost", obj.Assemble_Cost)
            clsCommon.AddColumnsForChange(coll, "Disposable_Value", obj.Disposable_Value)
            clsCommon.AddColumnsForChange(coll, "Accumulated_Dep", obj.Accumulated_Dep)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Asset_Group_Code", obj.Asset_Group_Code, True)
            clsCommon.AddColumnsForChange(coll, "Asset_Category_Code", obj.Asset_Category_Code, True)
            clsCommon.AddColumnsForChange(coll, "Is_Reverse_Dep", obj.Is_Reverse_Dep, True)
            clsCommon.AddColumnsForChange(coll, "Asset_Disposal_Code", obj.Asset_Disposal_Code, True)

            If Not obj.Reverse_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Reverse_Date", clsCommon.GetPrintDate(obj.Reverse_Date, "dd/MMM/yyyy"), True)
            End If


            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.AssetDepreciation, "", "")
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_DEPRECIATION", OMInsertOrUpdate.Insert, "", trans)

            'If clsCommon.CompairString(obj.Is_Permanent, "YES") = CompairStringResult.Equal Then
            ''Create GL Entry
            Dim ArryLst As ArrayList = New ArrayList()
            Dim qry As String = "select TSPL_Dep_AccountSet.Ac_Dep_Account,TSPL_Dep_AccountSet.Ac_Accum_Dep,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_Dep_AccountSet "
            qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.AcSet_Code=TSPL_Dep_AccountSet.AcSet_Code"
            qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code"
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code"
            qry += " where TSPL_ACQUISITION_DETAIL.Asset_Code ='" + obj.Asset_code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim strLocation As String = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            If clsCommon.myLen(strLocation) <= 0 Then
                Throw New Exception("Location not found for asset " + obj.Asset_code)
            End If

            Dim strACCode As String = clsCommon.myCstr(dt.Rows(0)("Ac_Dep_Account"))
            If clsCommon.myLen(strACCode) <= 0 Then
                Throw New Exception("Depreciation A/C not found for asset " + obj.Asset_code)
            End If
            strACCode = clsERPFuncationality.ChangeGLAccountLocationSegment(strACCode, strLocation, trans)
            Dim AccDepAC() As String = {strACCode, obj.Dep_Amount}
            ArryLst.Add(AccDepAC)

            strACCode = clsCommon.myCstr(dt.Rows(0)("Ac_Accum_Dep"))
            If clsCommon.myLen(strACCode) <= 0 Then
                Throw New Exception("Accumulated Depreciation A/C not found for asset " + obj.Asset_code)
            End If
            strACCode = clsERPFuncationality.ChangeGLAccountLocationSegment(strACCode, strLocation, trans)
            Dim AccumulatedDepAC() As String = {strACCode, -1 * obj.Dep_Amount}
            ArryLst.Add(AccumulatedDepAC)
            clsJournalMaster.FunGrnlEntryWithTrans(strLocation, False, trans, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"), "Depreciation Entry no -" + obj.Document_Code + "", "AM-DP", "Depreciation Entry", obj.Document_Code, "", "V", clsCommon.myCstr(dt.Rows(0)("Vendor_Code")), clsCommon.myCstr(dt.Rows(0)("Vendor_Name")), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
            '==========================
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, trans)) = "1", True, False)) = True Then
                Dim strCostCenterCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 CostCenter_Code from TSPL_ACQUISITION_DETAIL where Asset_Code = '" + obj.Asset_code + "'", trans))
                Dim strHirerachyCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Top 1 Hirerachy_Code from TSPL_ACQUISITION_DETAIL where Asset_Code =  '" + obj.Asset_code + "'", trans))
                clsERPFuncationality.UpdateCostCenterAndHirerachyCodeOnJE(obj.Document_Code, "AM-DP", strCostCenterCode, strHirerachyCode, trans)
            End If
            '==========================
            If obj.Is_Reverse_Dep = 1 AndAlso clsCommon.myLen(obj.Reverse_Date) > 0 Then
                Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Voucher_No FROM TSPL_JOURNAL_MASTER WHERE Source_Doc_No ='" & obj.Document_Code & "'", trans))
                clsJournalEntryHeader.AutoReverse(VoucherNo, obj.Reverse_Date, trans, 0)
                Dim AutoRevNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Voucher_No FROM TSPL_JOURNAL_MASTER WHERE Source_Doc_No ='" & VoucherNo & "'", trans))
                clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" & AutoRevNo & "' ", trans)
            End If
        Next
        Return True
    End Function
    Public Shared Function GetAssetDepCount(ByVal Asset_Code As String, ByVal trans As SqlTransaction) As Integer
        Dim qry As String = "select COUNT(Document_Code) from TSPL_ASSET_DEPRECIATION where Asset_Code='" & Asset_Code & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetAssetDepQuery_ArInvoice() As String
        Dim Qry As String = ""
        Qry = "select Asset_Code,sum(Dep_Amount) as Final_Dep_Amount,sum(Dep_Amount_Tax) as Final_Dep_Amount_Tax,sum(DepRate ) as Final_Dep_Rate,sum(DepRateTax ) as Final_Dep_Rate_Tax," & Environment.NewLine &
                   " sum(case when Dep_Type=1 then Dep_Amount else 0 end) as Perm_Dep_Amount, " & Environment.NewLine &
                   " sum(case when Dep_Type=1 then Dep_Amount_Tax else 0 end) as Perm_Dep_Amount_Tax, " & Environment.NewLine &
                    " sum(case when Dep_Type=1 then DepRate  else 0 end) as Per_Dep_Rate, " & Environment.NewLine &
                    " sum(case when Dep_Type=1 then DepRateTax  else 0 end) as Perm_Dep_Rate_Tax, " & Environment.NewLine &
                   " sum(case when Dep_Type in (0,2) then Dep_Amount else 0 end) as Temp_Dep_Amount, " & Environment.NewLine &
                   " sum(case when Dep_Type in (0,2) then Dep_Amount_Tax else 0 end) as Temp_Dep_Amount_Tax ," & Environment.NewLine &
                    " sum(case when Dep_Type in (0,2) then DepRate else 0 end) as temp_Dep_Rate, " & Environment.NewLine &
                    " sum(case when Dep_Type in (0,2) then DepRateTax else 0 end) as temp_Dep_Rate_Tax,sum(RoundOff) as RoundOff " & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                   " select Asset_Code,sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax,1 as Dep_Type ,COALESCE(max(DepRate),0) as DepRate,COALESCE(max(RoundOff),0) as RoundOff,COALESCE(max(DepRateTax),0) as DepRateTax from TSPL_ASSET_DEPRECIATION  " & Environment.NewLine &
                   " group by Asset_Code " & Environment.NewLine &
                   " ) as Dep group by Asset_Code "
        Return Qry
    End Function

    Public Shared Function GetAssetDepQuery() As String
        Dim Qry As String = ""
        Qry = "select Asset_Code,sum(Dep_Amount) as Final_Dep_Amount,sum(Dep_Amount_Tax) as Final_Dep_Amount_Tax,sum(DepRate ) as Final_Dep_Rate,sum(DepRateTax ) as Final_Dep_Rate_Tax," & Environment.NewLine &
                   " sum(case when Dep_Type=1 then Dep_Amount else 0 end) as Perm_Dep_Amount, " & Environment.NewLine &
                   " sum(case when Dep_Type=1 then Dep_Amount_Tax else 0 end) as Perm_Dep_Amount_Tax, " & Environment.NewLine &
                    " sum(case when Dep_Type=1 then DepRate  else 0 end) as Per_Dep_Rate, " & Environment.NewLine &
                    " sum(case when Dep_Type=1 then DepRateTax  else 0 end) as Perm_Dep_Rate_Tax, " & Environment.NewLine &
                   " sum(case when Dep_Type in (0,2) then Dep_Amount else 0 end) as Temp_Dep_Amount, " & Environment.NewLine &
                   " sum(case when Dep_Type in (0,2) then Dep_Amount_Tax else 0 end) as Temp_Dep_Amount_Tax ," & Environment.NewLine &
                    " sum(case when Dep_Type in (0,2) then DepRate else 0 end) as temp_Dep_Rate, " & Environment.NewLine &
                    " sum(case when Dep_Type in (0,2) then DepRateTax else 0 end) as temp_Dep_Rate_Tax,sum(RoundOff) as RoundOff " & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                   " select ACQ.Asset_Code,COALESCE(max(Dep_Amount),0) as Dep_Amount,COALESCE(max(Dep_Amount_Tax),0) as Dep_Amount_Tax,0 as Dep_Type ,COALESCE(max(DepRate),0) as DepRate,COALESCE(max(RoundOff),0) as RoundOff,COALESCE(max(DepRateTax),0) as DepRateTax from " & Environment.NewLine &
                   " (SELECT ACQD.Acquisition_Code,ACQD.Asset_Code,ACQH.Acquisition_Date,ACQD.Start_Date from TSPL_ACQUISITION_DETAIL ACQD " & Environment.NewLine &
                   " inner join TSPL_ACQUISITION_HEAD ACQH ON ACQD.Acquisition_Code=ACQH.Acquisition_Code) AS ACQ " & Environment.NewLine &
                   " LEFT JOIN  (SELECT ASSET_CODE,Document_Date,Dep_Amount,DepRate,Roundoff ,DepRateTax ,Dep_Amount_Tax FROM TSPL_ASSET_DEPRECIATION where Is_Permanent='NO') AD ON ACQ.Asset_Code=AD.Asset_Code " & Environment.NewLine &
                   " and not exists (select top 1 innTable.Document_Date from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=AD.Asset_Code and Is_Permanent='YES' order by innTable.Document_Date desc) " & Environment.NewLine &
                   " and AD.Document_Date > ACQ.Start_Date group by ACQ.Asset_Code " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " select Asset_Code,sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax,1 as Dep_Type ,COALESCE(max(DepRate),0) as DepRate,COALESCE(max(RoundOff),0) as RoundOff,COALESCE(max(DepRateTax),0) as DepRateTax from TSPL_ASSET_DEPRECIATION where Is_Permanent='YES' " & Environment.NewLine &
                   " group by Asset_Code " & Environment.NewLine &
                   " union all " & Environment.NewLine &
                   " select ACQ.Asset_Code,COALESCE(max(Dep_Amount),0) as Dep_Amount,COALESCE(max(Dep_Amount_Tax),0) as Dep_Amount_Tax,2 as Dep_Type ,COALESCE(max(DepRate),0) as DepRate,COALESCE(max(RoundOff),0) as RoundOff,COALESCE(max(DepRateTax),0) as DepRateTax from " & Environment.NewLine &
                   " (SELECT ACQD.Acquisition_Code,ACQD.Asset_Code,ACQH.Acquisition_Date,ACQD.Start_Date from TSPL_ACQUISITION_DETAIL ACQD " & Environment.NewLine &
                   " inner join TSPL_ACQUISITION_HEAD ACQH ON ACQD.Acquisition_Code=ACQH.Acquisition_Code) AS ACQ " & Environment.NewLine &
                   " LEFT JOIN  (SELECT ASSET_CODE,Document_Date,Dep_Amount,DepRate,RoundOff ,DepRateTax,Dep_Amount_Tax FROM TSPL_ASSET_DEPRECIATION where Is_Permanent='NO') AD ON ACQ.Asset_Code=AD.Asset_Code " & Environment.NewLine &
                   " and exists (select top 1 innTable.Document_Date from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=AD.Asset_Code and Is_Permanent='YES' order by innTable.Document_Date desc) " & Environment.NewLine &
                   " and AD.Document_Date > (select top 1 innTable.Document_Date from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=AD.Asset_Code and Is_Permanent='YES' order by innTable.Document_Date desc) " & Environment.NewLine &
                   " group by ACQ.Asset_Code " & Environment.NewLine &
                   " ) as Dep group by Asset_Code "
        Return Qry
    End Function

    Public Shared Function GetAssetCurrentValue(ByVal Asset_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim QryDep As String = GetAssetDepQuery()
        Dim Qry As String = " select (coalesce(ACQD.Book_Source_value,0)-coalesce(Dep.Perm_Dep_Amount,0)) as Asset_Value from TSPL_ACQUISITION_DETAIL ACQD " & Environment.NewLine &
                            " left join (" & QryDep & ") Dep on ACQD.Asset_Code=Dep.Asset_Code where ACQD.Asset_Code='" & Asset_Code & "'"
        Dim AssetValue As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        Return AssetValue
    End Function
    Public Shared Function GetAssetCurrentPermValue(ByVal Asset_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim QryDep As String = GetAssetDepQuery()
        Dim Qry As String = " select (coalesce(ACQD.Book_Source_value,0)-coalesce(Dep.Perm_Dep_Amount,0)) as Asset_Value from TSPL_ACQUISITION_DETAIL ACQD " & Environment.NewLine &
                            " left join (" & QryDep & ") Dep on ACQD.Asset_Code=Dep.Asset_Code where ACQD.Asset_Code='" & Asset_Code & "'"
        Dim AssetValue As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
        Return AssetValue
    End Function
    Public Shared Function GetAssetDepreciationHistoryBaseQuery(ByVal From_Date As Date, ByVal To_Date As Date) As String
        Dim qry As String = " select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value," &
                            " Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep from TSPL_ASSET_DEPRECIATION " &
                            " where cast(Document_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "' "
        Return qry
    End Function
    Public Shared Function GetAssetDepreciationHistoryBookQuery(ByVal From_Date As Date, ByVal To_Date As Date) As String
        Dim qry As String = GetAssetDepreciationHistoryBaseQuery(From_Date, To_Date)
        qry = "select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense as Additional_Amount,Dep_Amount,deprate,Asset_value from (" & qry & ") DepBook"
        Return qry
    End Function

    Public Shared Function GetAssetDepreciationHistoryTaxQuery(ByVal From_Date As Date, ByVal To_Date As Date) As String
        Dim qry As String = GetAssetDepreciationHistoryBaseQuery(From_Date, To_Date)
        qry = "select Document_Code,Document_Date,Asset_Code,Opening_Value_Tax as Opening_Value,Work_Expense as Additional_Amount,Dep_Amount_Tax as Dep_Amount,Asset_value_Tax as Asset_value,deprate from (" & qry & ") DepTax"
        Return qry
    End Function

    Public Shared Function RevereAndDelete(ByVal Form_ID As String, ByVal Reason As String, ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AM-DP' and Source_Doc_No='" + strCode + "'", trans)

            Dim qry As String = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No ='" + VoucherNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No ='" + VoucherNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_ASSET_DEPRECIATION WHERE Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim obj As New clsCancelLog
            obj.Program_Code = Form_ID
            obj.DOCUMENT_NO = strCode
            obj.REASON = Reason
            obj.ACTIVITY_TYPE = "Reverse And Recreate"
            Return clsCancelLog.SaveData(obj, True, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeepCopy(ByVal ArrFrom As List(Of clsDepreciationCalculation)) As List(Of clsAssetDepreciation)
        Dim ArrTo As List(Of clsAssetDepreciation) = Nothing
        If ArrFrom IsNot Nothing AndAlso ArrFrom.Count > 0 Then
            ArrTo = New List(Of clsAssetDepreciation)
            For Each objFrom As clsDepreciationCalculation In ArrFrom
                Dim objTo As New clsAssetDepreciation
                objTo.Document_Date = clsCommon.myCDate(objFrom.colDate)
                objTo.Asset_code = clsCommon.myCstr(objFrom.colAssetCode)
                objTo.Dep_Method_Code = clsCommon.myCstr(objFrom.colDepMethodCode)
                objTo.Dep_Period_Code = clsCommon.myCstr(objFrom.colPeriodCode)
                objTo.Value_Before_Depreciation = clsCommon.myCdbl(objFrom.colAmtBeforeDep)
                objTo.Asset_value = clsCommon.myCdbl(objFrom.colAssetValue)
                objTo.Dep_Amount = clsCommon.myCdbl(objFrom.colDepAmount)
                objTo.Dep_Method_Tax_Code = clsCommon.myCstr(objFrom.colDepMethodTaxCode)
                objTo.Value_Before_Depreciation_Tax = clsCommon.myCdbl(objFrom.colAmtBeforeDepTax)
                objTo.Asset_value_Tax = clsCommon.myCdbl(objFrom.colAssetValueTax)
                objTo.Dep_Amount_Tax = clsCommon.myCdbl(objFrom.colDepAmountTax)
                objTo.Is_Permanent = clsCommon.myCstr(objFrom.colIsPermDep)
                objTo.Opening_Value = clsCommon.myCdbl(objFrom.colOpening_Value)
                objTo.Opening_Value_Tax = clsCommon.myCdbl(objFrom.colOpening_Value_Tax)
                objTo.Work_Expense = clsCommon.myCdbl(objFrom.colWork_Expense)
                objTo.Assemble_Cost = clsCommon.myCdbl(objFrom.colAssemble_Cost)
                objTo.Disposable_Value = clsCommon.myCdbl(objFrom.colDisposable_Value)
                'objTo.Accumulated_Dep = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAccumulated_Dep).Value)
                objTo.Location_Code = clsCommon.myCstr(objFrom.colLocation_Code)
                objTo.Asset_Group_Code = clsCommon.myCstr(objFrom.colAsset_Group_Code)
                objTo.Asset_Category_Code = clsCommon.myCstr(objFrom.colAsset_Category_Code)
                objTo.Tax_Recoverable = clsCommon.myCdbl(objFrom.colTax_Recoverable)
                objTo.Tax_Non_Recoverable = clsCommon.myCdbl(objFrom.colTax_Non_Recoverable)
                '' reverse functionalities
                objTo.Is_Reverse_Dep = clsCommon.myCdbl(objFrom.colIs_Reverse_Dep)
                If objTo.Is_Reverse_Dep = 1 Then
                    objTo.Reverse_Date = clsCommon.myCDate(objFrom.colReverse_Date)
                Else
                    objTo.Reverse_Date = Nothing
                End If
                objTo.Asset_Disposal_Code = objFrom.ColAsset_Disposal_Code
                ArrTo.Add(objTo)
            Next
        End If
        Return ArrTo
    End Function

    'Deprecial Funtion Start From Here
    Shared Function GetAssetLastDepData(ByVal Asset_Code As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = " select Max(Perm_Doc_Date) as Perm_Doc_Date,max(Temp_Doc_Date) as Temp_Doc_Date,min(Perm_Asset_Value) as Perm_Asset_Value, " &
                            " min(Temp_Asset_Value) as Temp_Asset_Value," &
                            " min(Perm_Asset_Value_Tax) as Perm_Asset_Value_Tax,min(Temp_Asset_Value_Tax) as Temp_Asset_Value_Tax from ( " &
                            " Select max(Document_Date) as Perm_Doc_Date," &
                            " max(case when Is_Permanent='No' then Document_Date else null end) as Temp_Doc_Date, " &
                            " case when  datepart(month,max( Document_Date))=3 then (max(Value_Before_Depreciation)-sum(Dep_Amount)) else min(Value_Before_Depreciation) end as Perm_Asset_Value, " &
                            " min(case when Is_Permanent='No' then Asset_value  end) as Temp_Asset_Value, " &
                            " min(case when Is_Permanent='Yes' then Asset_value_Tax end) as Perm_Asset_Value_Tax," &
                            " min(case when Is_Permanent='No' then Asset_value_Tax end) as Temp_Asset_Value_Tax " &
                            " from TSPL_ASSET_DEPRECIATION where Asset_Code='" & Asset_Code & "'" &
                            " union all " &
                            " select Acqusition_Date as Perm_Doc_Date,Null as Temp_Doc_Date,isnull(Book_Source_value,0)+isnull(Tax_Non_Recoverable ,0)  as Perm_Asset_Value,Null as Temp_Asset_Value, " &
                            " isnull(Book_Source_value,0)+isnull(Tax_Non_Recoverable ,0)  as Perm_Asset_Value_Tax, " &
                            " Null as Temp_Asset_Value_Tax  from TSPL_ACQUISITION_DETAIL where Asset_Code='" & Asset_Code & "' and Book_Source_Original_value>Book_Source_value " &
                            " and Book_Source_value>Book_Salvage_Value) as Tab"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Shared Function GetAssetDepreciationAmt(ByVal AssetCode As String, ByVal TransEndDate As Date, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = "select sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax from (
select Asset_Code, Dep_Amount,Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION where Asset_Code='" + AssetCode + "'
union all
select Asset_Code,-1* Dep_Amount as Dep_Amount,-1*Dep_Amount_Tax as Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION_ADJUSTMENT where Asset_Code='" + AssetCode + "' and Adjustment_Date <= '" + clsCommon.GetPrintDate(TransEndDate, "dd/MMM/yyyy") + "'
)xx"
        Return clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
    Public Shared Function GetDepreciationCal(ByRef Arr As List(Of clsDepreciationCalculation), ByVal isAutoProcess As Boolean, ByVal TransactionDate As Date, ByVal chkReverseTempDep As Boolean, ByVal ArrLocaion As ArrayList, ByVal ArrAsset As ArrayList, ByVal ArrGroup As ArrayList, ByVal ArrCostCenter As ArrayList, ByVal ArrCategory As ArrayList) As Boolean
        Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            GetDepreciationCal(Arr, isAutoProcess, TransactionDate, chkReverseTempDep, ArrLocaion, ArrAsset, ArrGroup, ArrCostCenter, ArrCategory, Trans)
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetDepreciationCal(ByRef Arr As List(Of clsDepreciationCalculation), ByVal isAutoProcess As Boolean, ByVal TransactionDate As Date, ByVal chkReverseTempDep As Boolean, ByVal ArrLocaion As ArrayList, ByVal ArrAsset As ArrayList, ByVal ArrGroup As ArrayList, ByVal ArrCostCenter As ArrayList, ByVal ArrCategory As ArrayList, ByVal Trans As SqlTransaction) As Boolean
        Try
            Arr = New List(Of clsDepreciationCalculation)
            Dim ERPStartDate As String = ""
            Try
                ERPStartDate = clsCommon.myCDate(objCommonVar.ERPStartDate)
            Catch ex As Exception
                Throw New Exception("Invalid ERP Start Date")
            End Try
            Dim ITPartialFADays As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PartialFADepDays, clsFixedParameterCode.PartialFADepDays, Trans))
            Dim ITRateMulFA As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RateMultPartialFADepDays, clsFixedParameterCode.RateMultPartialFADepDays, Trans))
            Dim depreciationCalculation As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DepreciationCalculationMethod, clsFixedParameterCode.DepreciationCalculationMethod, Trans))
            Dim AllowDecimalInFixedAsset As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDecimalInFixedAsset, clsFixedParameterCode.AllowDecimalInFixedAsset, Trans))
            Dim AllowRoundInFixedAsset As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundInFixedAsset, clsFixedParameterCode.AllowRoundInFixedAsset, Trans))
            Dim adjustment As Double
            TransactionDate = clsCommon.GetDateWithEndTime(TransactionDate)

            Dim DepQry As String = clsAssetDepreciation.GetAssetDepQuery()
            Dim qry As String = "select xxx.*,Dep.Temp_Dep_Amount,Dep.Temp_Dep_Amount_Tax,xxxTemp.Temp_Doc_Date,FiscalPeriodRun1,FiscalPeriodRun2,FiscalPeriodRun3,FiscalPeriodRun4,FiscalPeriodRun5,FiscalPeriodRun6,FiscalPeriodRun7,FiscalPeriodRun8,FiscalPeriodRun9,FiscalPeriodRun10,FiscalPeriodRun11,FiscalPeriodRun12,TSPL_DEPRECIATION_METHOD.Description as MethodName,TSPL_DEPRECIATION_METHOD.Formula,TSPL_DEPRECIATION_PERIODS.period_Desc,DepMethodTax.Formula as Formula_Tax,DepMethodTax.Description as MethodNameTax,FiscalPeriodRunPerm1,FiscalPeriodRunPerm2,FiscalPeriodRunPerm3,FiscalPeriodRunPerm4,FiscalPeriodRunPerm5,FiscalPeriodRunPerm6,FiscalPeriodRunPerm7,FiscalPeriodRunPerm8,FiscalPeriodRunPerm9,FiscalPeriodRunPerm10,FiscalPeriodRunPerm11,FiscalPeriodRunPerm12,loc.Location_Desc,AG.Description as Group_Desc,AC.Description as Cat_Desc from (" + Environment.NewLine
            qry += " select Asset_Code,Asset_Name,Dep_Method_Code,Dep_Period_Code,0 as RI,Start_Date as Document_Date, isnull(TSPL_ACQUISITION_DETAIL.Book_Source_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Asset_Value,TSPL_ACQUISITION_DETAIL.Dep_Rate,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,isnull(TSPL_ACQUISITION_DETAIL.Book_Source_Original_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Book_Source_Original_value ,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life,  isnull(TSPL_ACQUISITION_DETAIL.Book_Source_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0) as Asset_value_Tax, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate ,TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable ,TSPL_ACQUISITION_DETAIL.Tax_Recoverable  from TSPL_ACQUISITION_DETAIL" + Environment.NewLine
            qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code" + Environment.NewLine
            qry += " where TSPL_ACQUISITION_HEAD.Status=1 and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 and not exists (select 1 from TSPL_ASSET_DEPRECIATION where TSPL_ASSET_DEPRECIATION.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code )   " + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_ASSET_DEPRECIATION.Asset_Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ASSET_DEPRECIATION.Dep_Method_Code,TSPL_ASSET_DEPRECIATION.Dep_Period_Code,1 as RI,TSPL_ASSET_DEPRECIATION.Document_Date,TSPL_ASSET_DEPRECIATION.Asset_value,TSPL_ACQUISITION_DETAIL.Dep_Rate,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,isnull(TSPL_ACQUISITION_DETAIL.Book_Source_Original_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Book_Source_Original_value,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life, TSPL_ASSET_DEPRECIATION.Asset_value_Tax, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate ,TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable ,TSPL_ACQUISITION_DETAIL.Tax_Recoverable  from TSPL_ASSET_DEPRECIATION" + Environment.NewLine
            qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code " + Environment.NewLine
            qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code" + Environment.NewLine
            qry += " Where Document_Code =(select top 1 innTable.Document_Code from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code  order by innTable.Document_Date desc) and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 " + Environment.NewLine
            qry += " )xxx" + Environment.NewLine
            qry += " left join (" &
                   " select Acquisition_Code,Asset_Code,Start_Date as Temp_Doc_Date from TSPL_ACQUISITION_DETAIL where  not exists (select 1 from TSPL_ASSET_DEPRECIATION " &
                   " where TSPL_ASSET_DEPRECIATION.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code ) and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 " &
                   " union all " &
                   " select Document_Code,Asset_Code,Document_Date as Temp_Doc_Date from TSPL_ASSET_DEPRECIATION Where Document_Code =(select top 1 innTable.Document_Code " &
                   " from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code order by innTable.Document_Date desc)) " &
                   " xxxTemp on xxx.Asset_Code=xxxTemp.Asset_Code "
            qry += " left outer join TSPL_DEPRECIATION_PERIODS on TSPL_DEPRECIATION_PERIODS.period_Code=xxx.Dep_Period_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=xxx.Dep_Method_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=Dep_Method_Tax_Code" &
                   " left join TSPL_LOCATION_MASTER Loc on xxx.Loc_Code=Loc.Location_Code" &
                   " left join TSPL_ASSET_GROUP AG on xxx.Group_Code=AG.Group_Code " &
                   " left join TSPL_ASSET_CATEGORY AC on xxx.Category_code=ac.Category_Code " &
                   " left join (" & DepQry & ") as Dep on xxx.Asset_Code=Dep.Asset_Code " &
                   " where 2=2 "
            If ArrCategory IsNot Nothing AndAlso ArrCategory.Count > 0 Then
                qry += " and xxx.Category_code in (" + clsCommon.GetMulcallString(ArrCategory) + ")"
            End If
            If ArrLocaion IsNot Nothing AndAlso ArrLocaion.Count > 0 Then
                qry += " and xxx.Loc_Code in (" + clsCommon.GetMulcallString(ArrLocaion) + ")"
            End If
            If ArrCostCenter IsNot Nothing AndAlso ArrCostCenter.Count > 0 Then
                qry += " and xxx.CostCenter_Code in (" + clsCommon.GetMulcallString(ArrCostCenter) + ")"
            End If
            If ArrGroup IsNot Nothing AndAlso ArrGroup.Count > 0 Then
                qry += " and xxx.Group_Code in (" + clsCommon.GetMulcallString(ArrGroup) + ")"
            End If
            If ArrAsset IsNot Nothing AndAlso ArrAsset.Count > 0 Then
                qry += " and xxx.Asset_Code in (" + clsCommon.GetMulcallString(ArrAsset) + ")"
            End If
            Dim PermDepFlag As Boolean = False
            Dim Asset_Code As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Arr = New List(Of clsDepreciationCalculation)
                'obj.ArrTemp = New List(Of clsDepreciationCalculation)
                Dim objTr As New clsDepreciationCalculation
                clsCommon.ProgressBarPercentShow()
                Dim CurrIndx As Integer = 0
                Dim Total As Integer = dt.Rows.Count
                For Each dr As DataRow In dt.Rows
                    Try
                        If isAutoProcess AndAlso Arr.Count > 0 Then
                            If clsCommon.myLen(Asset_Code) > 0 Then
                                If Not clsCommon.CompairString(Asset_Code, clsCommon.myCstr(dr("Asset_Code"))) = CompairStringResult.Equal Then
                                    clsAssetDepreciation.SaveData1(clsAssetDepreciation.DeepCopy(Arr), Trans)
                                    Arr = New List(Of clsDepreciationCalculation)
                                End If
                            End If
                        End If

                        If clsCommon.myLen(Asset_Code) <= 0 Then
                            PermDepFlag = False
                            Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(dr("Asset_Code")), Asset_Code) = CompairStringResult.Equal Then
                                If PermDepFlag Then
                                    Continue For
                                End If
                            Else
                                PermDepFlag = False
                                Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                            End If
                        End If
                        CurrIndx += 1
                        clsCommon.ProgressBarPercentUpdate(((CurrIndx) * 100 / (Total)), "Asset [" + Asset_Code + "]" & clsCommon.myCstr(CurrIndx) & "/" & clsCommon.myCstr(Total) & "")

                        Dim dblTaxRecoverable As Double = clsCommon.myCdbl(dr("Tax_Recoverable"))
                        Dim dblTaxNonRecoverable As Double = clsCommon.myCdbl(dr("Tax_Non_Recoverable"))

                        Dim dblPrevDep As Double = clsCommon.myCdbl(dr("Temp_Dep_Amount"))
                        Dim dblBaseAmt As Double = clsCommon.myCdbl(dr("Asset_Value"))

                        Dim dblDepRate As Double = clsCommon.myCdbl(dr("Dep_Rate"))
                        Dim dblPrevDepTax As Double = clsCommon.myCdbl(dr("Temp_Dep_Amount_Tax"))
                        Dim dblBaseAmtTax As Double = clsCommon.myCdbl(dr("Asset_value_Tax"))
                        Dim dblDepRateTax As Double = clsCommon.myCdbl(dr("Dep_Tax_Rate"))
                        Dim depStartDate As Date = clsCommon.GetDateWithEndTime(dr.Item("Start_Date"))
                        Dim Book_Salvage_Value As Double = clsCommon.myCdbl(dr("Book_Salvage_Value"))

                        If dblBaseAmt <= Book_Salvage_Value Then
                            Continue For
                        End If
                        dblDepRate = dblDepRate
                        dblDepRateTax = dblDepRateTax

                        qry = "  select SUM(1) from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' "
                        Dim intDepCount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
                        Dim dtStartDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
                        Dim dtStartDate1 As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
                        Dim dtPermDepDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
                        Dim LastTempDepDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Temp_Doc_Date")))
                        Dim dtEndDate As Date = clsCommon.GetDateWithEndTime(depStartDate).AddYears(clsCommon.myCdbl(dr("Book_Estimated_Life"))).AddDays(-1)
                        Dim dtDepData As DataTable = GetAssetLastDepData(clsCommon.myCstr(dr.Item("Asset_Code")), Trans)
                        Dim dtEndMonthDate As Date = clsCommon.GetDateWithEndTime(New Date(dtEndDate.Year, dtEndDate.Month, 1).AddMonths(1).AddDays(-1))
                        If intDepCount > 0 Then
                            dtPermDepDate = clsCommon.GetDateWithEndTime(dtDepData.Rows(0).Item("Perm_Doc_Date"))
                            dtPermDepDate = LastTempDepDate.AddDays(1)

                            dtStartDate = dtStartDate.AddDays(1)
                            dtStartDate1 = dtStartDate1.AddDays(1)
                        Else
                            dtStartDate = dtStartDate
                            dtStartDate1 = dtStartDate1
                        End If
                        qry = "select DateDiff(d,start_date,end_date) as dd,Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" & dtStartDate & "',103) >= convert(date,start_date,103) and convert(date,'" & dtStartDate & "',103) <= convert(date,end_date,103)"
                        Dim dtFisalYEar As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                        If dtFisalYEar Is Nothing OrElse dtFisalYEar.Rows.Count <= 0 Then
                            Throw New Exception("Fiscal Year Not exist where Date [" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "] Exist")
                        End If
                        clsCommon.ProgressBarPercentUpdate(((CurrIndx) * 100 / (Total)), "Asset [" + Asset_Code + "] [" + clsCommon.myCstr(dtFisalYEar.Rows(0)("Fiscal_Name")) + "]" & clsCommon.myCstr(CurrIndx) & "/" & clsCommon.myCstr(Total) & "")
                        qry = " select TSPL_ASSET_SCRAP_HEAD.Document_No,TSPL_ASSET_SCRAP_HEAD.Document_Date,TSPL_ASSET_SCRAP_HEAD.Status,TSPL_ASSET_SCRAP_HEAD.Loc_Code from TSPL_ASSET_SCRAP_HEAD where  TSPL_ASSET_SCRAP_HEAD.Document_No in ( select TSPL_ASSET_SCRAP_DETAIL.Document_No from TSPL_ASSET_SCRAP_DETAIL where TSPL_ASSET_SCRAP_DETAIL.Asset_Code='" + Asset_Code + "')"
                        Dim dtDisposalEntry As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                        If Book_Salvage_Value <= 0 Then
                            Continue For ''Do Not calcualte Dep if salves Value is Zero
                        End If

                        Dim DaysInYear As Integer = clsCommon.myCDecimal(dtFisalYEar.Rows(0)("dd")) + 1
                        Dim dclTotalDep As Decimal = GetAssetDepreciationAmt(clsCommon.myCstr(dr("Asset_Code")), dtStartDate, Trans)


                        While (TransactionDate >= dtStartDate AndAlso dblBaseAmt > Book_Salvage_Value AndAlso (dtStartDate.AddMonths(1).AddDays(-1)) <= dtEndMonthDate)
                            dblBaseAmt = clsCommon.myCdbl(dr.Item("Book_Source_Original_value"))
                            dblBaseAmtTax = clsCommon.myCdbl(dr.Item("Book_Source_Original_value"))
                            Dim dclMaxDepAmt As Decimal = (dblBaseAmt - dclTotalDep - Book_Salvage_Value)
                            Dim dclMaxDepAmtflag As Boolean = False
                            Dim isDisposalEntryFound As Boolean = False
                            If (dblBaseAmt - dclTotalDep) <= Book_Salvage_Value Then
                                Exit While
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                PermDepFlag = True
                            Else
                                If PermDepFlag Then
                                    Continue For
                                End If
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRun" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                Dim dtForChecFrom As Date = New Date(dtStartDate.Year, dtStartDate.Month, 1)
                                dtForChecFrom = clsCommon.GetDateWithEndTime(dtForChecFrom)
                                Dim dtForChecTo As Date = dtForChecFrom.AddMonths(1)
                                dtForChecTo = clsCommon.GetDateWithEndTime(dtForChecTo.AddDays(-1))
                                Dim DocDate As Date = dtForChecTo
                                dtForChecTo = IIf(dtForChecTo < dtEndDate, dtForChecTo, dtEndDate)

                                If dtDisposalEntry IsNot Nothing AndAlso dtDisposalEntry.Rows.Count > 0 Then
                                    If dtForChecTo.Month = clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")).Month AndAlso dtForChecTo.Year = clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")).Year Then
                                        If clsCommon.myCdbl(dtDisposalEntry.Rows(0)("Status")) = 1 Then
                                            ''Now Create Dep Enry of Disposal Entry
                                            qry = " select 1 from TSPL_ASSET_DEPRECIATION where Asset_Code='" + Asset_Code + "' and len(isnull(Asset_Disposal_Code,''))>0 "
                                            Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                                            If dtCheck Is Nothing OrElse dtCheck.Rows.Count <= 0 Then
                                                dtForChecTo = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")))
                                                isDisposalEntryFound = True
                                            End If
                                        End If
                                    End If
                                    If dtStartDate >= clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")) Then
                                        Continue For ''Do Not calcualte Dep of Disposal Entry
                                    End If
                                End If

                                If ITPartialFADays > clsCommon.myFormat(DateDiff(DateInterval.Day, depStartDate, DocDate)) Then
                                    dblDepRateTax = dblDepRateTax * ITRateMulFA
                                End If

                                qry = "select 1 from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtForChecTo), "dd/MMM/yyyy hh:mm tt") + "' AND coalesce(TSPL_ASSET_DEPRECIATION.is_Permanent,'NO')='YES'"
                                Dim isRecordExist As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                                If isRecordExist Is Nothing OrElse isRecordExist.Rows.Count <= 0 Then
                                    qry = "select sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax from (
                                select Dep_Amount,Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and Document_Date<(select Start_Date from TSPL_Fiscal_Year_Master where Start_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "' and End_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "')
                                union all
                                select -1*Dep_Amount as Dep_Amount,-1*Dep_Amount_Tax as Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION_ADJUSTMENT where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and Adjustment_Date <= '" + clsCommon.GetPrintDate(dtForChecFrom, "dd/MMM/yyyy") + "'
                                )xx"
                                    Dim dtPreFiscayYearDep As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)

                                    qry = "select SUM(Net_Amt) as Expense,sum(case when convert(Date, Document_Date,103)<='" & clsCommon.GetPrintDate(dtForChecTo, "dd/MMM/yyyy") & "' then Net_Amt else 0 end) as Current_Expense from (select Asset_Code,Document_Date,Net_Amt from  TSPL_ASSET_WORK_HEAD where Status=1 and Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "'  and Document_Code not in (select Document_Code from TSPL_ASSET_ASSEMBLE_DETAIL where Distribute='Y' and Asset_Code ='" + clsCommon.myCstr(dr("Asset_Code")) + "')" &
                                    " ) AS TSPL_ASSET_WORK_HEAD "
                                    Dim dtExp As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                                    Dim dblExpence As Double = clsCommon.myCdbl(dtExp.Rows(0).Item("Expense"))
                                    Dim dblCurrExpence As Double = clsCommon.myCdbl(dtExp.Rows(0).Item("Current_Expense"))
                                    dtStartDate1 = clsCommon.GetDateWithEndTime(dtForChecTo.AddDays(1))
                                    dblBaseAmt += dblCurrExpence
                                    dblBaseAmtTax += dblCurrExpence
                                    If dtPreFiscayYearDep IsNot Nothing AndAlso dtPreFiscayYearDep.Rows.Count > 0 Then
                                        dblBaseAmt -= clsCommon.myCdbl(dtPreFiscayYearDep.Rows(0)("Dep_Amount"))
                                        dblBaseAmtTax -= clsCommon.myCdbl(dtPreFiscayYearDep.Rows(0)("Dep_Amount_Tax"))
                                    End If

                                    qry = "select Dep_Rate from TSPL_FA_BOOK_MASTER  Where  TSPL_FA_BOOK_MASTER.Book_Code = '" + clsCommon.myCstr(dr("Group_Code")) + "' and start_Date<='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "'"
                                    Dim dtRate As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                                    If dtRate IsNot Nothing AndAlso dtRate.Rows.Count > 0 Then
                                        dblDepRate = clsCommon.myCstr(dtRate.Rows(0)("Dep_Rate"))
                                    End If
                                    objTr = New clsDepreciationCalculation
                                    objTr.colSelect = True
                                    objTr.colDate = DocDate
                                    objTr.colAssetCode = clsCommon.myCstr(dr("Asset_Code"))
                                    objTr.colAssetName = clsCommon.myCstr(dr("Asset_Name"))
                                    objTr.colLocation_Code = clsCommon.myCstr(dr("Loc_Code"))
                                    objTr.colLocation_Desc = clsCommon.myCstr(dr("Location_Desc"))
                                    objTr.colAsset_Group_Code = clsCommon.myCstr(dr("Group_Code"))
                                    objTr.colAsset_Group_Desc = clsCommon.myCstr(dr("Group_Desc"))
                                    objTr.colAsset_Category_Code = clsCommon.myCstr(dr("Category_Code"))
                                    objTr.colAsset_Category_Desc = clsCommon.myCstr(dr("Cat_Desc"))
                                    objTr.colDepMethodCode = clsCommon.myCstr(dr("Dep_Method_Code"))
                                    objTr.colDepMethod = clsCommon.myCstr(dr("MethodName"))
                                    objTr.colPeriodCode = clsCommon.myCstr(dr("Dep_Period_Code"))
                                    objTr.colPeriod = clsCommon.myCstr(dr("period_Desc"))
                                    objTr.colSalvageValue = clsCommon.myCdbl(dr("Book_Salvage_Value"))
                                    objTr.colSourceOrgValue = clsCommon.myCdbl(dr("Book_Source_Original_value"))
                                    objTr.colEstimatedLife = clsCommon.myCdbl(dr("Book_Estimated_Life"))
                                    objTr.colOpening_Value = clsCommon.myCdbl(dr("Asset_Value"))
                                    objTr.colWork_Expense = dblCurrExpence
                                    objTr.colAssemble_Cost = 0
                                    objTr.colOpening_Value_Tax = clsCommon.myCdbl(dr("Asset_Value_Tax"))
                                    objTr.colTax_Non_Recoverable = clsCommon.myCdbl(dr("Tax_Non_Recoverable"))
                                    objTr.colTax_Recoverable = clsCommon.myCdbl(dr("Tax_Recoverable"))
                                    If isDisposalEntryFound Then
                                        objTr.ColAsset_Disposal_Code = clsCommon.myCstr(dtDisposalEntry.Rows(0)("Document_No"))
                                        objTr.colDate = clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date"))
                                    End If
                                    Dim strFormula As String = clsCommon.myCstr(dr("Formula"))
                                    If strFormula.Contains("^") = True Then
                                        Throw New Exception("Formula for depreciation method " & clsCommon.myCstr(dr("Dep_Method_Code")) & " contains invalid charecter(^)")
                                    End If
                                    Dim str As String = clsCommon.myFormat(dblBaseAmt).Replace(",", "")
                                    objTr.colDepFormula = strFormula
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "(" & clsCommon.myCstr(dblBaseAmt) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Estimated_Life"))) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_value")) / IIf(clsCommon.myCdbl(dr("Book_Source_Original_value")) = 0, IIf(dblBaseAmt = 0, 1, dblBaseAmt), clsCommon.myCdbl(dr("Book_Source_Original_value"))) * dblBaseAmt) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Rate")) / IIf(clsCommon.myCdbl(dr("Book_Source_Original_value")) = 0, IIf(dblBaseAmt = 0, 1, dblBaseAmt), clsCommon.myCdbl(dr("Book_Source_Original_value"))) * dblBaseAmt) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "(" & clsCommon.myCstr(intDepCount) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "(" & clsCommon.myCstr(dblDepRate) & "/1.0)")
                                    strFormula = strFormula.Replace(clsDepreciationParameter.BCLD, "(" & clsCommon.myCstr(DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) & "/1.0)")
                                    Dim CheckFormulaType As String = clsDBFuncationality.getSingleValue("select type from TSPL_DEPRECIATION_METHOD where code ='" & objTr.colDepMethodCode & "'", Trans)
                                    Dim dblDepAmt As Double = 0
                                    Dim dblDifference As Double = 0
                                    Dim originalDepRate As Double = 0
                                    If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
                                        dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "", Trans)), 3, MidpointRounding.ToEven)
                                    ElseIf clsCommon.CompairString(CheckFormulaType, "Rate") = CompairStringResult.Equal Then
                                        dblDepRate = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "", Trans)), 3, MidpointRounding.ToEven)
                                        dblDepAmt = dblBaseAmt * dblDepRate / 100
                                        originalDepRate = dblDepRate
                                    Else
                                        dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "", Trans)), 3, MidpointRounding.ToEven)
                                    End If


                                    If clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "DL") = CompairStringResult.Equal Then
                                        dblDepAmt = dblDepAmt * (DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) / DaysInYear
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "MT") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
                                        Else
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
                                        End If
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "QTL") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
                                        Else
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
                                        End If
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "YRY") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, dtPermDepDate, dtForChecTo.AddDays(1)) / 1.0
                                        Else
                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, LastTempDepDate, dtForChecTo.AddDays(1)) / 1.0
                                        End If
                                    End If
                                    objTr.colAmtBeforeDep = dblBaseAmt
                                    If clsCommon.myCdbl(dr("Dep_Rate")) = 0 Then
                                        If dblBaseAmt = 0 Then
                                            objTr.colDepRate = 0
                                        Else
                                            If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
                                                Dim FrmulBaseAmount As Double = (dblDepAmt / dblBaseAmt) * 100
                                                objTr.colDepRate = Math.Round(FrmulBaseAmount, 3, MidpointRounding.ToEven)
                                            Else
                                                objTr.colDepRate = dblDepRate
                                            End If
                                        End If
                                    Else
                                        objTr.colDepRate = dblDepRate
                                    End If
                                    If dblDepAmt < 0 Then
                                        dblDepAmt = 0
                                    End If
                                    If dblDepAmt > dblBaseAmt Then
                                        dblDepAmt = dblBaseAmt
                                    End If
                                    If dblDepAmt > dclMaxDepAmt Then
                                        dblDepAmt = dclMaxDepAmt
                                        dclMaxDepAmtflag = True
                                    End If
                                    adjustment = Math.Pow(10, AllowDecimalInFixedAsset)
                                    If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
                                        dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
                                        dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
                                        dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
                                    Else
                                        dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
                                    End If
                                    If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
                                        dblDepRate = Math.Ceiling(dblDepRate * adjustment) / adjustment
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
                                        dblDepRate = Math.Round(dblDepRate, AllowDecimalInFixedAsset)
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
                                        dblDepRate = Math.Floor(dblDepRate * adjustment) / adjustment
                                    Else
                                        dblDepRate = Math.Round(dblDepRate, 2, MidpointRounding.ToEven)
                                    End If
                                    dblDifference = originalDepRate - dblDepRate
                                    objTr.colRoundOffRate = dblDifference
                                    objTr.colDepAmount = dblDepAmt
                                    objTr.colPrevDepAmount = dblPrevDep
                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                        objTr.colAssetValue = dblBaseAmt - dblDepAmt
                                    Else
                                        objTr.colAssetValue = dblBaseAmt - dblDepAmt
                                    End If

                                    dblBaseAmt -= dblDepAmt

                                    If False Then ''clsCommon.myLen(dr("Dep_Method_Tax_Code")) > 0
                                        objTr.colDepMethodTaxCode = clsCommon.myCstr(dr("Dep_Method_Tax_Code"))
                                        objTr.colDepMethodTax = clsCommon.myCstr(dr("MethodNameTax"))
                                        strFormula = clsCommon.myCstr(dr("Formula_Tax"))
                                        If strFormula.Contains("^") = True Then
                                            Throw New Exception("Formula for depreciation method " & objTr.colDepMethodCode & " contains invalid charecter(^)")
                                        End If
                                        objTr.colDepFormulaTax = strFormula
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "(" & clsCommon.myCstr(dblBaseAmtTax) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Estimated_Life"))) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Value"))) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Rate"))) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "(" & clsCommon.myCstr(intDepCount) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "(" & clsCommon.myCstr(dblDepRateTax) & "/1.0)")
                                        strFormula = strFormula.Replace(clsDepreciationParameter.BCLD, "(" & clsCommon.myCstr(DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) & "/1.0)")
                                        If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
                                        ElseIf clsCommon.CompairString(CheckFormulaType, "Rate") = CompairStringResult.Equal Then
                                            dblDepRateTax = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
                                            dblDepAmt = dblBaseAmt * dblDepRate / 100
                                            originalDepRate = dblDepRateTax
                                        Else
                                            dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
                                        End If
                                        adjustment = Math.Pow(10, AllowDecimalInFixedAsset)
                                        If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
                                        Else
                                            dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
                                        End If

                                        If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
                                            dblDepRateTax = Math.Ceiling(dblDepRateTax * adjustment) / adjustment
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
                                            dblDepRateTax = Math.Round(dblDepRateTax, AllowDecimalInFixedAsset)
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
                                            dblDepRateTax = Math.Floor(dblDepRateTax * adjustment) / adjustment
                                        Else
                                            dblDepRateTax = Math.Round(dblDepRateTax, 2, MidpointRounding.ToEven)
                                        End If
                                        If clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "DL") = CompairStringResult.Equal Then
                                            If clsCommon.GetDateWithEndTime(dtPermDepDate) = depStartDate Then
                                                dblDepAmt = dblDepAmt * (DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) / DaysInYear '365.0
                                            Else
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Day, dtPermDepDate, dtForChecTo) / DaysInYear '365.0
                                            End If
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "MT") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
                                            Else
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
                                            End If
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "QTL") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
                                            Else
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
                                            End If
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "YRY") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, dtPermDepDate, dtForChecTo.AddDays(1)) / 1.0
                                            Else
                                                dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, LastTempDepDate, dtForChecTo.AddDays(1)) / 1.0
                                            End If
                                        End If
                                        objTr.colAmtBeforeDepTax = dblBaseAmtTax
                                        If clsCommon.myCdbl(dr("Dep_Tax_Rate")) = 0 Then
                                            If dblBaseAmtTax = 0 Then
                                                objTr.colDepRateTax = 0
                                            Else
                                                If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
                                                    Dim FrmulBaseAmount As Double = (dblDepAmt / dblBaseAmtTax) * 100
                                                    objTr.colDepRateTax = Math.Round(FrmulBaseAmount, 3, MidpointRounding.ToEven)
                                                Else
                                                    objTr.colDepRateTax = dblDepRateTax
                                                End If
                                            End If
                                        Else
                                            objTr.colDepRateTax = dblDepRateTax
                                        End If
                                        If dblDepAmt < 0 Then
                                            dblDepAmt = 0
                                        End If
                                        If dblDepAmt > dblBaseAmtTax Then
                                            dblDepAmt = dblBaseAmtTax
                                        End If

                                        adjustment = Math.Pow(10, AllowDecimalInFixedAsset)

                                        If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
                                            dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
                                        Else
                                            dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
                                        End If
                                        objTr.colDepAmountTax = dblDepAmt
                                        objTr.colPrevDepAmountTax = dblPrevDepTax
                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                            objTr.colAssetValueTax = dblBaseAmtTax - dblDepAmt
                                        Else
                                            objTr.colAssetValueTax = dblBaseAmtTax - dblDepAmt
                                        End If
                                        dblBaseAmtTax -= dblDepAmt
                                        If clsCommon.myCdbl(objTr.colAssetValueTax) < 0 Then
                                            objTr.colAssetValueTax = 0
                                        End If
                                    End If
                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
                                        objTr.colIsPermDep = "YES"
                                    Else
                                        objTr.colIsPermDep = "NO"
                                    End If
                                    objTr.colManualDep = False
                                    Try
                                        If dr("Start_Date") IsNot DBNull.Value Then
                                            If clsCommon.GetDateWithStartTime(dr("Start_Date")) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
                                                objTr.colManualDep = True
                                            End If
                                        End If
                                    Catch ex As Exception
                                    End Try
                                    intDepCount += 1
                                    dclTotalDep += dblDepAmt
                                End If
                                dtPermDepDate = DocDate.AddDays(1)
                            End If
                            If clsCommon.myLen(objTr.colAssetCode) > 0 Then
                                Arr.Add(objTr)
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "NO") = CompairStringResult.Equal Then
                                If chkReverseTempDep Then
                                    If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                                        dtStartDate = dtStartDate.AddMonths(1)
                                        Continue While
                                    End If
                                    objTr.colIs_Reverse_Dep = 1
                                    objTr.colReverse_Date = clsCommon.myCDate(Arr(Arr.Count - 1).colDate).AddDays(1)
                                Else
                                    If Arr Is Nothing OrElse (Arr.Count - 1) < 0 Then
                                        dtStartDate = dtStartDate.AddMonths(1)
                                        Continue While
                                    End If
                                    objTr.colIs_Reverse_Dep = 0
                                End If
                            Else
                                objTr.colIs_Reverse_Dep = 0
                            End If
                            dtStartDate = dtStartDate.AddMonths(1)
                            If dclMaxDepAmtflag OrElse isDisposalEntryFound Then
                                Continue For
                            End If
                        End While
                    Catch ex As Exception
                        clsCommon.ProgressBarPercentHide()
                        Throw New Exception(ex.Message)
                    End Try
                Next

                If isAutoProcess AndAlso Arr.Count > 0 Then
                    clsAssetDepreciation.SaveData1(clsAssetDepreciation.DeepCopy(Arr), Trans)
                    Arr = New List(Of clsDepreciationCalculation)
                End If
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
End Class

Public Class clsDepreciationCalculation
#Region "Variables"
    Public colSelect As String = Nothing
    Public colDate As Date = Nothing
    Public colAssetCode As String = Nothing
    Public colAssetName As String = Nothing
    Public colDepMethodCode As String = Nothing
    Public colDepMethod As String = Nothing
    Public colPeriodCode As String = Nothing
    Public colPeriod As String = Nothing
    Public colDepFormula As String = Nothing
    Public colAmtBeforeDep As String = Nothing
    Public colDepRate As String = Nothing
    Public colDepAmount As String = Nothing
    Public colPrevDepAmount As String = Nothing
    Public colTotDepAmount As String = Nothing
    Public colAssetValue As String = Nothing
    Public colSourceOrgValue As String = Nothing
    Public colEstimatedLife As String = Nothing
    Public colSalvageValue As String = Nothing
    Public colAmtBeforeDepTax As String = Nothing
    Public colDepFormulaTax As String = Nothing
    Public colDepRateTax As String = Nothing
    Public colPrevDepAmountTax As Decimal = Nothing
    Public colTotDepAmountTax As Decimal = Nothing
    Public colDepAmountTax As Decimal = Nothing
    Public colAssetValueTax As Decimal = Nothing
    Public colDepMethodTaxCode As String = Nothing
    Public colDepMethodTax As String = Nothing
    Public colIsPermDep As String = Nothing

    '' new columns added
    Public colOpening_Value As String = Nothing
    Public colOpening_Value_Tax As String = Nothing
    Public colWork_Expense As String = Nothing
    Public colAssemble_Cost As String = Nothing
    Public colDisposable_Value As String = Nothing
    Public colAccumulated_Dep As String = Nothing
    Public colLocation_Code As String = Nothing
    Public colLocation_Desc As String = Nothing
    Public colAsset_Group_Code As String = Nothing
    Public colAsset_Group_Desc As String = Nothing
    Public colAsset_Category_Code As String = Nothing
    Public colAsset_Category_Desc As String = Nothing

    Public colIs_Reverse_Dep As String = Nothing
    Public colReverse_Date As String = Nothing
    Public ColAsset_Disposal_Code As String = Nothing

    Public colRoundOffRate As Decimal
    Public colManualDep As Boolean
    'Public Arr As List(Of clsDepreciationCalculation) = Nothing
    'Public ArrTemp As List(Of clsDepreciationCalculation) = Nothing

    Public colTax_Recoverable As String = Nothing
    Public colTax_Non_Recoverable As String = Nothing

#End Region

End Class
