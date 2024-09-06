Imports System.Data.SqlClient
Public Class clsERPFuncationality
    Public Shared Function GetScopeIdentityValue(ByVal trans As SqlTransaction) As Integer
        Return clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
    End Function

    Public Shared Function GetGSTStatus(ByVal TransactionDate? As Date) As Boolean
        If objCommonVar.GSTApplicable AndAlso objCommonVar.GSTApplicableDate <= TransactionDate Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, False)
    End Function

    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, isLocationCodeisSegment, True)
    End Function

    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, isLocationCodeisSegment, isIncreaseCounter, False)
    End Function

    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean, ByVal isLocationCodeisState As Boolean) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, isLocationCodeisSegment, isIncreaseCounter, isLocationCodeisState, False)
    End Function
    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean, ByVal isLocationCodeisState As Boolean, ByVal isMonthlyChange As Boolean) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, isLocationCodeisSegment, isIncreaseCounter, isLocationCodeisState, isMonthlyChange, False, "")
    End Function
    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean, ByVal isLocationCodeisState As Boolean, ByVal isMonthlyChange As Boolean, ByVal strRouteNo As String) As String
        Return GetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, isLocationCodeisSegment, isIncreaseCounter, isLocationCodeisState, isMonthlyChange, False, strRouteNo)
    End Function

    Public Shared Function GetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean, ByVal isLocationCodeisState As Boolean, ByVal isMonthlyChange As Boolean, ByVal isLocationCodeisMCC As Boolean, ByVal strRouteNo As String) As String
        Dim qry As String = ""
        Dim strRetCode As String = ""
        Dim strLocatinSegmentCode As String = ""
        Dim dt As DataTable

        Dim isMasterPrefix As Boolean = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Master_Prefix from TSPL_DOCUMENT_TYPE where Doc_Type='" + strDocType + "'", trans)) = 1)
        If objCommonVar.AllowAutoNoForBackLogEntry Then
            If (clsCommon.CompairString(strDocType, "Fresh Dispatch") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Fresh Invoice") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Shipment Product Sale") = CompairStringResult.Equal OrElse clsCommon.CompairString(strDocType, "Product Invoice") = CompairStringResult.Equal) Then
                Dim strNextNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BackLog_Next_Number from TSPL_DOCPREFIX_BACKLOG where  Doc_Type='" + strDocType + "' and  isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocationCode + "' and BackLog_Date > '" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'  ", trans))
                If clsCommon.myLen(strNextNo) > 0 Then
                    qry = "update TSPL_DOCPREFIX_BACKLOG set BackLog_Next_Number ='" + clsCommon.incval(strNextNo) + "'  where  Doc_Type='" + strDocType + "' and  isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocationCode + "' and BackLog_Date > '" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'  "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Return strNextNo
                End If
            End If
        End If
        If isLocationCodeisSegment Then
            qry = "SELECT 1 from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code='" + strLocationCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strLocationCode + " is not a Location Segment")
            End If
            strLocatinSegmentCode = strLocationCode
        ElseIf isLocationCodeisMCC Then
            qry = "select 1 from TSPL_MCC_MASTER where MCC_Code='" + strLocationCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strLocationCode + " is not a MCC")
            End If
            strLocatinSegmentCode = strLocationCode
        ElseIf isLocationCodeisState Then
            qry = "select 1 from TSPL_STATE_MASTER where STATE_CODE='" + strLocationCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strLocationCode + " is not a state")
            End If
            strLocatinSegmentCode = strLocationCode
        Else
            If clsCommon.myLen(strLocationCode) > 0 Then
                strLocatinSegmentCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + strLocationCode + "'", trans))
                If clsCommon.myLen(strLocatinSegmentCode) <= 0 Then
                    Throw New Exception("Location Segment code Not found for Location :" + strLocationCode)
                End If
            End If
        End If

        Dim IntFiscalYear As Integer = dtDocDate.Year
        If dtDocDate.Month >= 1 AndAlso dtDocDate.Month <= 3 Then
            IntFiscalYear -= 1
        End If

        qry = GetQryOFDOCPrefix(trans, dtDocDate, strDocType, strTransType, strLocatinSegmentCode, strRouteNo, IntFiscalYear, False, isMasterPrefix)
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            qry = GetQryOFDOCPrefix(trans, dtDocDate.AddMonths(-1), strDocType, strTransType, strLocatinSegmentCode, strRouteNo, IntFiscalYear, True, isMasterPrefix)
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim flag As Boolean = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso (clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1 OrElse clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1) Then
                flag = True
            End If
            If Not flag Then
                If objCommonVar.AutoGeneratePrefix Then
                    qry = GetQryOFDOCPrefix(trans, dtDocDate.AddYears(-1), strDocType, strTransType, strLocatinSegmentCode, strRouteNo, IntFiscalYear - 1, False, isMasterPrefix)
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Dim dr As DataRow = dt.NewRow()
                        Dim strPrefix As String = ""
                        Try
                            Dim strAddDOC As String = strDocType
                            If clsCommon.myLen(strTransType) > 0 Then
                                strAddDOC += " " + strTransType
                            End If
                            Dim strBreak As String() = clsCommon.myCstr(strAddDOC).Split(New String() {" "}, StringSplitOptions.None)
                            If strBreak.Length >= 5 Then
                                strPrefix += strBreak(0).ToString().Substring(0, 1)
                                strPrefix += strBreak(2).ToString().Substring(0, 1)
                                strPrefix += strBreak(4).ToString().Substring(0, 1)
                            ElseIf strBreak.Length >= 4 Then
                                strPrefix += strBreak(0).ToString().Substring(0, 1)
                                strPrefix += strBreak(2).ToString().Substring(0, 1)
                                strPrefix += strBreak(3).ToString().Substring(0, 1)
                            ElseIf strBreak.Length >= 3 Then
                                strPrefix += strBreak(0).ToString().Substring(0, 1)
                                strPrefix += strBreak(1).ToString().Substring(0, 1)
                                strPrefix += strBreak(2).ToString().Substring(0, 1)
                            ElseIf strBreak.Length >= 2 Then
                                strPrefix += strBreak(0).ToString().Substring(0, 1)
                                strPrefix += strBreak(1).ToString().Substring(0, 2)
                            ElseIf strBreak.Length >= 1 Then
                                Try
                                    strBreak = clsCommon.myCstr(strBreak(0)).Split(New String() {"_"}, StringSplitOptions.None)
                                    If strBreak.Length >= 5 Then
                                        strPrefix += strBreak(0).ToString().Substring(0, 1)
                                        strPrefix += strBreak(2).ToString().Substring(0, 1)
                                        strPrefix += strBreak(4).ToString().Substring(0, 1)
                                    ElseIf strBreak.Length >= 4 Then
                                        strPrefix += strBreak(0).ToString().Substring(0, 1)
                                        strPrefix += strBreak(2).ToString().Substring(0, 1)
                                        strPrefix += strBreak(3).ToString().Substring(0, 1)
                                    ElseIf strBreak.Length >= 3 Then
                                        strPrefix += strBreak(0).ToString().Substring(0, 1)
                                        strPrefix += strBreak(1).ToString().Substring(0, 1)
                                        strPrefix += strBreak(2).ToString().Substring(0, 1)
                                    ElseIf strBreak.Length >= 2 Then
                                        strPrefix += strBreak(0).ToString().Substring(0, 1)
                                        strPrefix += strBreak(1).ToString().Substring(0, 2)
                                    ElseIf strBreak.Length >= 1 Then
                                        Dim mc2 As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(strBreak(0).ToString(), "[A-Z]")
                                        Dim ii As Integer = 0
                                        For Each m As System.Text.RegularExpressions.Match In mc2
                                            strPrefix += m.Value
                                            If ii = 2 Then
                                                Exit For
                                            End If
                                            ii += 1
                                        Next
                                    Else
                                        Throw New Exception("BSP")
                                    End If
                                Catch ex As Exception
                                    strPrefix += strBreak(0).ToString().Substring(0, 3)
                                End Try
                            End If
                            If clsCommon.myLen(strLocatinSegmentCode) > 0 Then
                                strPrefix += "-" + strLocatinSegmentCode
                            End If
                            If clsCommon.myLen(strRouteNo) > 0 Then
                                strPrefix += "-" + strRouteNo
                            End If
                            dr("Doc_Prfeix") = strPrefix.ToUpper()
                            dr("Separator") = "/"
                            dr("Is_Change_monthly") = 0
                            dr("Year_Separator") = ""
                            dr("Is_Change_Daily") = 0
                            dr("dontDisplayYearInSeries") = 0
                            dr("MinSizeofSeries") = 6
                            dt.Rows.Add(dr)
                            flag = True
                        Catch ex As Exception
                        End Try
                    Else
                        flag = True
                    End If
                End If
            End If
            If flag Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Type", strDocType)
                clsCommon.AddColumnsForChange(coll, "Doc_Trans_Type", strTransType)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocatinSegmentCode)
                clsCommon.AddColumnsForChange(coll, "RouteNo", strRouteNo)
                clsCommon.AddColumnsForChange(coll, "Doc_Prfeix", clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")))
                clsCommon.AddColumnsForChange(coll, "Dont_Add_Prefix", clsCommon.myCDecimal(dt.Rows(0)("Dont_Add_Prefix")))
                If clsCommon.myCdbl(dt.Rows(0)("dontDisplayYearInSeries")) = 1 OrElse clsCommon.myCDecimal(dt.Rows(0)("Dont_Add_Prefix")) = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Next_Number", clsCommon.myCdbl(dt.Rows(0)("Next_Number")))
                Else
                    clsCommon.AddColumnsForChange(coll, "Next_Number", 1)
                End If
                clsCommon.AddColumnsForChange(coll, "Fin_Year", IntFiscalYear)


                clsCommon.AddColumnsForChange(coll, "Separator", clsCommon.myCstr(dt.Rows(0)("Separator")))

                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                clsCommon.AddColumnsForChange(coll, "Is_Change_Monthly", clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Curr_Month", dtDocDate.Month)
                End If
                clsCommon.AddColumnsForChange(coll, "Year_Separator", clsCommon.myCstr(dt.Rows(0)("Year_Separator")))
                clsCommon.AddColumnsForChange(coll, "Is_Change_Daily", clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Curr_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "dontDisplayYearInSeries", clsCommon.myCdbl(dt.Rows(0)("dontDisplayYearInSeries")))
                clsCommon.AddColumnsForChange(coll, "Short_Fiscal_Year", clsCommon.myCdbl(dt.Rows(0)("Short_Fiscal_Year")))
                clsCommon.AddColumnsForChange(coll, "MinSizeofSeries", clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCPREFIX_MASTER", OMInsertOrUpdate.Insert, "", trans)

                qry = GetQryOFDOCPrefix(trans, dtDocDate, strDocType, strTransType, strLocatinSegmentCode, strRouteNo, IntFiscalYear, False, isMasterPrefix)
                dt = clsDBFuncationality.GetDataTable(qry, trans)
            End If

        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Dim strException As String = "Please ask your Administrator to Set the Counter  " + Environment.NewLine +
             "Transaction - " + strDocType + Environment.NewLine +
             "Fiscal year - " + clsCommon.myCstr(IntFiscalYear) + Environment.NewLine
            If clsCommon.myLen(strTransType) > 0 Then
                strException += "Transaction Type - " + strTransType + Environment.NewLine
            End If
            If clsCommon.myLen(strLocatinSegmentCode) > 0 Then
                If isLocationCodeisState Then
                    strException += "State - " + strLocatinSegmentCode + Environment.NewLine
                Else
                    strException += "Segment Location - " + strLocatinSegmentCode + Environment.NewLine
                End If
            End If
            Throw New Exception(strException)
        End If
        ''**********Generate Counter ************************
        Dim intCurrCounter As Integer = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("Next_Number")))
        Dim isDailyChange As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1, True, False)
        Dim intCurrDate As Date? = Nothing
        If isDailyChange Then
            intCurrDate = clsCommon.myCDate(dt.Rows(0)("Curr_Date"))
        End If



        isMonthlyChange = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1, True, False)
        Dim intCurrMonth As Integer = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("Curr_Month")))
        Dim strSep = clsCommon.myCstr(dt.Rows(0)("Separator")).Trim()

        Dim strFinYear As String = ""
        If objCommonVar.CounterFinancialYearStyle Then
            Dim intYear As Integer = dtDocDate.Year - 2000
            If dtDocDate.Month >= 1 AndAlso dtDocDate.Month <= 3 Then
                strFinYear = clsCommon.myCstr(intYear - 1) + clsCommon.myCstr(dt.Rows(0)("Year_Separator")).Trim() + clsCommon.myCstr(intYear)
            Else
                strFinYear = clsCommon.myCstr(intYear) + clsCommon.myCstr(dt.Rows(0)("Year_Separator")).Trim() + clsCommon.myCstr(intYear + 1)
            End If
        End If

        If clsCommon.myCBool(dt.Rows(0)("Short_Fiscal_Year")) Then
            If dtDocDate.Month >= 1 AndAlso dtDocDate.Month <= 3 Then
                strFinYear = clsCommon.myCstr(dtDocDate.Year - 1 - 2000)
            Else
                strFinYear = clsCommon.myCstr(dtDocDate.Year - 2000)
            End If
        End If


        If clsCommon.myCBool(dt.Rows(0)("dontDisplayYearInSeries")) OrElse isMasterPrefix Then
            If clsCommon.myCDecimal(dt.Rows(0)("Dont_Add_Prefix")) = 1 Then
                strRetCode = ""
            Else
                strRetCode = clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")).Trim() + strSep
            End If
        Else
            If clsCommon.myCDecimal(dt.Rows(0)("Dont_Add_Prefix")) = 1 Then
                strRetCode = strFinYear + strSep
            Else
                strRetCode = clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")).Trim() + strSep + strFinYear + strSep
            End If
        End If
        Dim intNumPartLen As Integer = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
        If isDailyChange Then
            intNumPartLen = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
            strRetCode += IIf(intCurrDate.Value.Month < 10, "0", "") + clsCommon.myCstr(intCurrDate.Value.Month).Trim() + strSep + IIf(intCurrDate.Value.Day < 10, "0", "") + clsCommon.myCstr(intCurrDate.Value.Day).Trim() + strSep
        ElseIf isMonthlyChange Then
            intNumPartLen = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
            strRetCode += IIf(intCurrMonth < 10, "0", "") + clsCommon.myCstr(intCurrMonth).Trim() + strSep
        End If
        Dim intLen As Integer = clsCommon.myLen(intCurrCounter) ''clsCommon.myLen(dt.Rows(0)("Next_Number"))
        For ii As Integer = 1 To intNumPartLen - intLen
            strRetCode += "0"
        Next
        strRetCode += clsCommon.myCstr(intCurrCounter)
        CheckForValidCounter(strRetCode, strDocType, strTransType, trans)
        'Throw New Exception(strRetCode)

        'Ticket No- TEC/07/03/19-000436 sanjay Unique document code generation check for other than existing client
        If objCommonVar.UniqueDocument Then
            CheckForUniqueCounter(strRetCode, strDocType, strTransType, strLocatinSegmentCode, clsCommon.myCstr(IntFiscalYear), trans)
        End If
        'sanjay Unique document code generation check for other than existing client

        ''**********Increment Current Counter ************************

        If isIncreaseCounter Then
            intCurrCounter = intCurrCounter + 1
            qry = "update TSPL_DOCPREFIX_MASTER set Next_Number=" + clsCommon.myCstr(intCurrCounter) + ""
            qry += " where PK_ID=" + clsCommon.myCstr(dt.Rows(0)("PK_ID")) + ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return strRetCode

    End Function

    Private Shared Function GetQryOFDOCPrefix(ByVal tran As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocatinSegmentCode As String, ByVal strRouteNo As String, ByVal IntFiscalYear As Integer, ByVal isTakingTopOne As Boolean, ByVal isMasterPrefix As Boolean) As String
        Dim whr As String = "select  PK_ID from TSPL_DOCPREFIX_MASTER where Doc_Type='" + strDocType + "' and  isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocatinSegmentCode + "'"
        If clsCommon.myLen(strRouteNo) > 0 Then
            whr += " and isnull(RouteNo,'')='" + strRouteNo + "' "
        End If
        If Not isMasterPrefix Then
            whr += " and Fin_Year='" + clsCommon.myCstr(IntFiscalYear) + "'"
        End If
        If Not isTakingTopOne Then
            whr += " and 2=(case when Is_Change_Daily=1 then case when Curr_Date= '" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'  then 2 else 3  end   else   case when Is_Change_monthly=1 then case when Curr_Month= " + clsCommon.myCstr(dtDocDate.Month) + "  then 2 else 3  end else 2 end end)"
        End If


        Dim qry As String = "select " + IIf(isTakingTopOne, " Top 1", "") + " PK_ID,Doc_Prfeix,Dont_Add_Prefix,Fin_Year,Next_Number,Separator,Is_Change_monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,Short_Fiscal_Year,MinSizeofSeries,Year_Separator from TSPL_DOCPREFIX_MASTER  WITH ( UPDLOCK ) " + Environment.NewLine +
          " where PK_ID in (" + whr + ")"
        Dim Order As String = ""
        If isTakingTopOne Then
            Order = " order by case when Is_Change_Daily=1 then  CONVERT(varchar, Curr_Date,112) else  CONVERT(varchar, Curr_Month ) end desc"
        End If
        Return qry + " " + Order
    End Function

    Public Shared Function ChangeGLAccountLocationSegment(ByVal strAccountCode As String, ByVal strLocation As String) As String
        Return ChangeGLAccountLocationSegment(strAccountCode, strLocation, False, Nothing)
    End Function
    Public Shared Function ChangeGLAccountLocationSegment(ByVal strAccountCode As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Return ChangeGLAccountLocationSegment(strAccountCode, strLocation, False, trans)
    End Function
    ''BM00000007648 add parameter isCheckForUserPermission for not check the Loc segment. 
    Public Shared Function ChangeGLAccountLocationSegment(ByVal strAccountCode As String, ByVal strLocation As String, ByVal isLocationLocationSegment As Boolean, ByVal trans As SqlTransaction) As String
        Return ChangeGLAccountLocationSegment(strAccountCode, strLocation, isLocationLocationSegment, True, trans)
    End Function
    Public Shared Function ChangeGLAccountLocationSegment(ByVal strAccountCode As String, ByVal strLocation As String, ByVal isLocationLocationSegment As Boolean, ByVal isCheckForUserPermission As Boolean, ByVal trans As SqlTransaction) As String
        If clsCommon.myLen(strAccountCode) > 0 Then
            Dim qry As String = ""
            Dim strLocatinSegment As String = ""
            If isLocationLocationSegment Then
                strLocatinSegment = strLocation
            Else
                qry = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
                strLocatinSegment = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            End If

            If clsCommon.myLen(strLocatinSegment) <= 0 Then
                Throw New Exception("Please set the Location Segment For location [" + strLocation + "]")
            End If
            If (strAccountCode.Length >= 3) Then
                'Dim strOldSegment = strAccountCode.Substring(strAccountCode.Length - 3, 3)
                'If (IsNumeric(strOldSegment)) Then
                '    Throw New Exception("GL Account should be with location segment.For GL Account" + strAccountCode)
                'End If
                'strAccountCode = strAccountCode.Replace(strOldSegment, strLocatinSegment)
                strAccountCode = strAccountCode.Substring(0, strAccountCode.Length - 3) + strLocatinSegment
                qry = "select 1 from TSPL_GL_ACCOUNTS where Account_Code='" + strAccountCode + "'"
                If clsCommon.myLen(objCommonVar.strCurrUserGLAccount) > 0 AndAlso isCheckForUserPermission Then
                    qry += " and TSPL_GL_ACCOUNTS.Account_Code in (" + objCommonVar.strCurrUserGLAccount + ")"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Account " + strAccountCode + " Not Exist or Available for Current User.")
                End If
            Else
                Throw New Exception("GL should be of segment location Type")
            End If
        End If
        Return strAccountCode
    End Function

    Public Shared Function ChangeGLAccountWithOutLOcSegment(ByVal strAccountCode As String, ByVal strLocation As String, ByVal isLocationLocationSegment As Boolean, ByVal trans As SqlTransaction) As String
        Dim qry As String = ""
        Dim strLocatinSegment As String = ""
        If isLocationLocationSegment Then
            strLocatinSegment = strLocation
        Else
            qry = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
            strLocatinSegment = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        End If

        strAccountCode = strAccountCode + "-" + strLocatinSegment
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_ACCOUNTS where Account_Code='" + strAccountCode + "'", trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Account Not Exist." + strAccountCode)
        End If
        'If clsCommon.myLen(strLocatinSegment) <= 0 Then
        '    Throw New Exception("Please set the Location Segment For location" + strLocation)
        'End If
        'If (strAccountCode.Length >= 3) Then
        '    Dim strOldSegment = strAccountCode.Substring(strAccountCode.Length - 3, 3)
        '    If (IsNumeric(strOldSegment)) Then
        '        Throw New Exception("GL Account should be with location segment.For GL Account" + strAccountCode)
        '    End If
        '    strAccountCode = strAccountCode.Replace(strOldSegment, strLocatinSegment)
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_ACCOUNTS where Account_Code='" + strAccountCode + "'", trans)
        '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '        Throw New Exception("Account Not Exist." + strAccountCode)
        '    End If
        'Else
        '    Throw New Exception("GL should be of segment location Type")
        'End If
        Return strAccountCode
    End Function

    Public Shared Function GLGetAccountCode(ByVal strAccountCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = ""
        strAccountCode = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_GL_ACCOUNTS where Account_Code='" + strAccountCode + "'", trans)
        If clsCommon.myLen(strAccountCode) <= 0 Then
            Throw New Exception("Account Not Exist." + strAccountCode)
        End If
        Return strAccountCode
    End Function

    Public Shared Sub ValidateLocationSegment(ByVal CompCode As String, ByVal Modulee As String, ByVal transName As String, ByVal Location As String, ByVal DocDate As DateTime, ByVal trans As SqlTransaction)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipLockLocation, clsFixedParameterCode.SkipLockLocation, trans)) > 0 Then
            Exit Sub
        End If
        Dim Qry As String = ""
        Dim AllowLockTransactionUserwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLockTransactionUserwise, clsFixedParameterCode.AllowLockTransactionUserwise, trans))
        Try
            If AllowLockTransactionUserwise = 0 Then
                Qry = "Select Is_Locked, (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange 
from TSPL_LOCK_LOCATION_SEGMENT Where Trans_name='" + transName + "' 
AND  Convert(Date, Start_Date, 103)<=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) 
AND Convert(Date, End_Date , 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry + " AND Location_Segment_Code='" + Location + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCDecimal(dt.Rows(0)("Is_Locked")) = 1 Then
                        Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + clsCommon.myCstr(dt.Rows(0)("DateRange")) + "")
                    End If
                Else
                    dt = clsDBFuncationality.GetDataTable(Qry + " AND Location_Segment_Code=''", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.myCDecimal(dt.Rows(0)("Is_Locked")) = 1 Then
                            Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + clsCommon.myCstr(dt.Rows(0)("DateRange")) + "")
                        End If
                    End If

                End If
            Else
                Qry = "Select (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange,'' as User_Code from TSPL_LOCK_LOCATION_SEGMENT Where " &
                " TSPL_LOCK_LOCATION_SEGMENT.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION_SEGMENT.Module_Name='" + Modulee + "' AND " &
                "TSPL_LOCK_LOCATION_SEGMENT.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code='" + Location + "' " &
                " AND TSPL_LOCK_LOCATION_SEGMENT.Is_Locked='1' AND Convert(Date, Start_Date, 103)<=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) AND Convert(Date, End_Date , 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) " &
                "union all " &
                "Select  (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange,TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code " &
                "from TSPL_LOCK_LOCATION_SEGMENT  " &
                "left outer join TSPL_LOCK_LOCATION_SEGMENT_USER on TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code=TSPL_LOCK_LOCATION_SEGMENT_USER.Location_Segment_Code and " &
                "TSPL_LOCK_LOCATION_SEGMENT.Module_Name=TSPL_LOCK_LOCATION_SEGMENT_USER.Module_Name and TSPL_LOCK_LOCATION_SEGMENT.Trans_Name=TSPL_LOCK_LOCATION_SEGMENT_USER.Trans_Name " &
                "Where TSPL_LOCK_LOCATION_SEGMENT.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION_SEGMENT.Module_Name='" + Modulee + "' AND " &
                "TSPL_LOCK_LOCATION_SEGMENT.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code='" + Location + "' " &
                " AND Is_Locked='1' AND Convert(Date, ToDate, 103)<convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)  And " &
                "isnull(TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code,'') = '" & objCommonVar.CurrentUserCode & "'"
                Dim strSql = "select DateRange,max(user_code) as  user_code from ( " & Qry & " ) a group by DateRange "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                Dim DateRange As String = ""
                Dim strUser As String = ""
                If dt.Rows.Count > 0 Then
                    DateRange = clsCommon.myCstr(dt.Rows(0)("DateRange"))
                    strUser = clsCommon.myCstr(dt.Rows(0)("User_Code"))
                    If clsCommon.myLen(strUser) = 0 Then
                        Dim UserLockDate = clsDBFuncationality.getSingleValue("Select CONVERT(varchar, Todate, 103) from TSPL_LOCK_LOCATION_SEGMENT_USER  " &
                "where TSPL_LOCK_LOCATION_SEGMENT_USER.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION_SEGMENT_USER.Module_Name='" + Modulee + "' AND " &
                "TSPL_LOCK_LOCATION_SEGMENT_USER.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION_SEGMENT_USER.Location_Segment_Code='" + Location + "' " &
                "AND Convert(Date, ToDate, 103)>convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)  And " &
                "isnull(TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code,'') = '" & objCommonVar.CurrentUserCode & "'", trans)
                        If clsCommon.myLen(UserLockDate) = 0 Then
                            Throw New Exception("Transaction is Locked For Location Segment '" + Location + "' from " + DateRange + "")
                        Else
                            Throw New Exception("Transaction is Locked For User '" + objCommonVar.CurrentUserCode + "'  Location Segment '" + Location + "' Till " + UserLockDate + "")
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Try
            'Dim DateRange As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            'If clsCommon.myLen(DateRange) > 0 Then
            '    Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + DateRange + "")
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Sub ValidateLocationSegmentold(ByVal CompCode As String, ByVal Modulee As String, ByVal transName As String, ByVal Location As String, ByVal DocDate As String, ByVal trans As SqlTransaction)
        Dim Qry As String = "Select (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + CompCode + "' AND Module_Name='" + Modulee + "' AND Trans_Name='" + transName + "' AND Location_Segment_Code='" + Location + "'"
        Qry += " AND Is_Locked='1' AND Convert(Date, Start_Date, 103)<=convert(Date, '" + DocDate + "', 103) AND Convert(Date, End_Date , 103)>=convert(Date, '" + DocDate + "', 103)"
        Try
            Dim DateRange As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            If clsCommon.myLen(DateRange) > 0 Then
                Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + DateRange + "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function GetLocationSegment(ByVal locationCode As String) As String
        Dim sql As String = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + locationCode + "'"
        Return connectSql.RunScalar(sql).ToString()
    End Function

    Public Shared Function GetLocationSegment(ByVal locationCode As String, ByVal trans As SqlTransaction) As String
        Dim sql As String = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + locationCode + "'"
        Return connectSql.RunScalar(trans, sql).ToString()
    End Function

    Public Shared Sub ValidateLocationCode(ByVal CompCode As String, ByVal Modulee As String, ByVal transName As String, ByVal Location As String, ByVal DocDate As DateTime, ByVal trans As SqlTransaction)
        Dim Qry As String = ""
        'Dim AllowLockTransactionUserwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLockTransactionUserwise, clsFixedParameterCode.AllowLockTransactionUserwise, trans))
        Try
            If objCommonVar.AllowLockTransactionUserwise = 0 Then
                Qry = "Select Is_Locked, (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange 
from TSPL_LOCK_LOCATION Where Trans_name='" + transName + "' 
AND  Convert(Date, Start_Date, 103)<=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) 
AND Convert(Date, End_Date , 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry + " AND Location_Code='" + Location + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCDecimal(dt.Rows(0)("Is_Locked")) = 1 Then
                        Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + clsCommon.myCstr(dt.Rows(0)("DateRange")) + "")
                    End If
                Else
                    dt = clsDBFuncationality.GetDataTable(Qry + " AND Location_Code=''", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.myCDecimal(dt.Rows(0)("Is_Locked")) = 1 Then
                            Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + clsCommon.myCstr(dt.Rows(0)("DateRange")) + "")
                        End If
                    End If
                End If


                'Qry = "Select  (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange from TSPL_LOCK_LOCATION Where TSPL_LOCK_LOCATION.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION.ModuleCode='" + Modulee + "' AND TSPL_LOCK_LOCATION.TransCode='" + transName + "' AND TSPL_LOCK_LOCATION.Location_Code='" + Location + "'"
                'Qry += " AND TSPL_LOCK_LOCATION.Is_Locked='1' AND Convert(Date, Start_Date, 103)<=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) AND Convert(Date, End_Date , 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)"
                'Dim DateRange As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
                'If clsCommon.myLen(DateRange) > 0 Then
                '    Throw New Exception("Transaction " + transName + "[" + Modulee + "] is Locked For Location '" + Location + "' from " + DateRange + "")
                'End If
            Else
                Qry = "Select (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange,'' as User_Code from TSPL_LOCK_LOCATION Where " &
                " TSPL_LOCK_LOCATION.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION.Module_Name='" + Modulee + "' AND " &
                "TSPL_LOCK_LOCATION.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION.Location_Code='" + Location + "' " &
                " AND TSPL_LOCK_LOCATION.Is_Locked='1' AND Convert(Date, Start_Date, 103)<=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) AND Convert(Date, End_Date , 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103) " &
                "union all " &
                "Select  (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange,TSPL_LOCK_LOCATION_USER.User_Code " &
                "from TSPL_LOCK_LOCATION  " &
                "left outer join TSPL_LOCK_LOCATION_USER on TSPL_LOCK_LOCATION.Location_Code=TSPL_LOCK_LOCATION_USER.Location_Code and " &
                "TSPL_LOCK_LOCATION.Module_Name=TSPL_LOCK_LOCATION_USER.Module_Name and TSPL_LOCK_LOCATION.Trans_Name=TSPL_LOCK_LOCATION_USER.Trans_Name " &
                "Where TSPL_LOCK_LOCATION.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION.Module_Name='" + Modulee + "' AND TSPL_LOCK_LOCATION.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION.Location_Code='" + Location + "' " &
                " AND Is_Locked='1' AND Convert(Date, ToDate, 103)<convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)  And " &
                "isnull(TSPL_LOCK_LOCATION_USER.User_Code,'') = '" & objCommonVar.CurrentUserCode & "'"
                Dim strSql = "select DateRange,max(user_code) as  user_code from ( " & Qry & " ) a group by DateRange "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                Dim DateRange As String = ""
                Dim strUser As String = ""
                If dt.Rows.Count > 0 Then
                    DateRange = clsCommon.myCstr(dt.Rows(0)("DateRange"))
                    strUser = clsCommon.myCstr(dt.Rows(0)("User_Code"))
                    If clsCommon.myLen(strUser) = 0 Then
                        Dim UserLockDate As String = ""
                        Qry = "Select CONVERT(varchar, Todate, 103) from TSPL_LOCK_LOCATION_USER  " &
                                      "where TSPL_LOCK_LOCATION_USER.Comp_Code='" + CompCode + "' AND TSPL_LOCK_LOCATION_USER.Module_Name='" + Modulee + "' AND " &
                                      "TSPL_LOCK_LOCATION_USER.Trans_Name='" + transName + "' AND TSPL_LOCK_LOCATION_USER.Location_Code='" + Location + "' " &
                                      "AND Convert(Date, ToDate, 103)>=convert(Date, '" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "', 103)  And " &
                                      "isnull(TSPL_LOCK_LOCATION_USER.User_Code,'') = '" & objCommonVar.CurrentUserCode & "'"
                        UserLockDate = clsDBFuncationality.getSingleValue(Qry, trans)
                        If clsCommon.myLen(UserLockDate) = 0 Then
                            Throw New Exception("Transaction " + transName + "[" + Modulee + "] is Locked For Location '" + Location + "' from " + DateRange + "")
                        Else
                            Throw New Exception("Transaction " + transName + "[" + Modulee + "] is Locked For User '" + objCommonVar.CurrentUserCode + "'  Location '" + Location + "' Till " + UserLockDate + "")
                        End If


                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Sub ValidateLocationCodeold(ByVal CompCode As String, ByVal Modulee As String, ByVal transName As String, ByVal Location As String, ByVal DocDate As String, ByVal trans As SqlTransaction)
        Dim Qry As String = "Select (CONVERT(varchar, Start_Date, 103) +'  To  '+ CONVERT(varchar, End_Date, 103)) as DateRange from TSPL_LOCK_LOCATION Where Comp_Code='" + CompCode + "' AND Module_Name='" + Modulee + "' AND Trans_Name='" + transName + "' AND Location_Code='" + Location + "'"
        Qry += " AND Is_Locked='1' AND Convert(Date, Start_Date, 103)<=convert(Date, '" + DocDate + "', 103) AND Convert(Date, End_Date , 103)>=convert(Date, '" + DocDate + "', 103)"
        Try
            Dim DateRange As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
            If clsCommon.myLen(DateRange) > 0 Then
                Throw New Exception("Transaction is Locked For Location '" + Location + "' from " + DateRange + "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function closeForm(ByRef f As Object) As Boolean
        f.close()
        f.dispose()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Return True
    End Function

    Public Shared Function IsDocumentAlreadyPosted(TableName As String, DocumentNoColumnName As String, DocumentNo As String, WhrClsForPostingStatusCheck As String, trans As SqlTransaction) As Boolean   ', PostingStatusColumnName As String, PostingColumnNature As PostingColumnType, ValueWhenPostedInPostingStatusColumn As PostingStatusValueList,
        Dim rValue As Boolean = False
        Dim chk As Integer = 0
        Dim qry As String = ""
        Dim qry1 As String = ""
        Try
            If clsCommon.myLen(TableName) <= 0 Then
                Throw New Exception("Table Name Found Missing When Checking Doucment Posting Status")
            End If

            If clsCommon.myLen(DocumentNoColumnName) <= 0 Then
                Throw New Exception("DocumentNo Column Name Found Missing When Checking Doucment Posting Status")
            End If

            If clsCommon.myLen(DocumentNo) <= 0 Then
                Throw New Exception("DocumentNo  Value  Found Blank When Checking Doucment Posting Status")
            End If

            'If clsCommon.myLen(PostingStatusColumnName) <= 0 Then
            '    Throw New Exception("Posting Status Column Name Found Blank When Checking Doucment Posting Status")
            'End If

            'If PostingColumnNature = PostingColumnType.TEXT Then
            '    If ValueWhenPostedInPostingStatusColumn = PostingStatusValueList.Y OrElse ValueWhenPostedInPostingStatusColumn = PostingStatusValueList.Y Then

            '    Else
            '        Throw New Exception("Posting Status Column value Must be Y or Yes when it is of Text Nature")
            '    End If
            'ElseIf PostingColumnNature = PostingColumnType.NUMBER Then
            '    If ValueWhenPostedInPostingStatusColumn = PostingStatusValueList.ONE Then

            '    Else
            '        Throw New Exception("Posting Status Column value Must be ONE when it is of Number Nature")
            '    End If
            'Else
            '    Throw New Exception("Posting Status Column Nature Found Other than Text and Number When Checking Doucment Posting Status")
            'End If

            qry = "select COUNT(*) from " & TableName & " where " & DocumentNoColumnName & " ='" & DocumentNo & "' " & IIf(clsCommon.myLen(WhrClsForPostingStatusCheck) > 0, " and ", "") & WhrClsForPostingStatusCheck
            'If PostingColumnNature = PostingColumnType.NUMBER Then
            '    qry1 = " AND isnull(" & PostingStatusColumnName & ",0)='" & IIf(ValueWhenPostedInPostingStatusColumn = PostingStatusValueList.ONE, 1, 0) & "'"
            'Else
            '    qry1 = " AND isnull(" & PostingStatusColumnName & ",'')='" & IIf(ValueWhenPostedInPostingStatusColumn = PostingStatusValueList.Y, "Y", "YES") & "'"
            'End If
            'qry = qry & qry1
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            If chk = 1 Then
                Throw New Exception("Doument is Already Posted, Please Reload the Doucment")
                rValue = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function




    Public Shared Function ValidationGSTNO(ByVal StateCode As String, ByVal PanNo As String, ByVal GSTNO As String, ByVal trans As SqlTransaction) As Boolean
        Dim msg As String = ""
        Dim TwoDigitForStateCode As String
        Dim TenDigitForPANNO As String
        Try
            If clsCommon.myLen(GSTNO) <> 15 Then
                Throw New Exception("Length of GST must be 15 Character.")
            End If
            If Not (System.Text.RegularExpressions.Regex.IsMatch(clsCommon.myCstr(GSTNO), "^[a-zA-Z0-9]+$")) Then
                Throw New Exception("GST Number must be alphanumeric.")
            End If
            TwoDigitForStateCode = GSTNO.Trim().Substring(0, 2)
            If clsCommon.myLen(StateCode) <> 2 Or StateCode <> TwoDigitForStateCode Or Not IsNumeric(TwoDigitForStateCode) Then
                Throw New Exception("State code must be numeric (First two place).")
            End If
            TenDigitForPANNO = GSTNO.Trim().Substring(2, 10)
            If clsCommon.myLen(PanNo) <> 10 Or PanNo <> TenDigitForPANNO Then
                Throw New Exception("Wrong PAN No.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckPanStructure(ByVal PanValue As String, ByVal PersonName As String) As String
        Dim msg As String = ""
        msg = CheckPanStructure(PanValue, PersonName, Nothing)
        Return msg
    End Function

    Public Shared Function CheckPanStructure(ByVal PanValue As String, ByVal PersonName As String, ByVal trans As SqlTransaction) As String
        Dim msg As String = ""
        Try
            Dim IsValidateCustomerPANwithName As Boolean = True
            IsValidateCustomerPANwithName = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ValidateCustomerPANwithName, clsFixedParameterCode.ValidateCustomerPANwithName, trans)) = 1, True, False)
            PersonName = clsCommon.myCstr(PersonName) '' used to trim person name is space is comming
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}?$")
            Dim fourth_char As String = ""
            Dim Fifth_Char As String = ""
            Dim First_Char As String = ""
            Dim NameSplit() As String = Nothing

            If clsCommon.myLen(PanValue.Trim()) >= 10 Then
                If checkPan.IsMatch(PanValue.Trim()) Then '=when pan contains first 5 Characters followed by 4digits and 1 character then check further validation.
                    fourth_char = clsCommon.myCstr(PanValue).Trim().Substring(3, 1)
                    Fifth_Char = clsCommon.myCstr(PanValue).Trim().Substring(4, 1)

                    If clsCommon.CompairString(fourth_char, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "H") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "A") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "T") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "L") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "J") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "G") <> CompairStringResult.Equal Then
                        msg = "4th character of PAN number should be in (C,P,H,F,A,T,B,L,J,G)."
                    End If

                    If clsCommon.myLen(msg) <= 0 Then
                        NameSplit = PersonName.Split(" ")
                        Dim counter As Integer = 0
                        If NameSplit IsNot Nothing AndAlso NameSplit.Length > 0 Then
                            For ii As Integer = 0 To NameSplit.Length - 1
                                'Check Space
                                If clsCommon.CompairString(clsCommon.myCstr(NameSplit(ii)).Trim(), "") = CompairStringResult.Equal Then
                                    Throw New Exception("Single space is allowed between name,surname and last name of vendor.")
                                End If
                                First_Char = clsCommon.myCstr(NameSplit(ii)).Trim().Substring(0, 1)
                                If clsCommon.CompairString(Fifth_Char, First_Char) = CompairStringResult.Equal Then
                                    counter += 1
                                End If
                            Next
                        End If
                        If IsValidateCustomerPANwithName Then
                            If counter <= 0 Then
                                msg = "5th word of PAN number should be 1st character of Assessee's Last Name/Surname."
                            End If
                        End If
                    End If
                Else
                    msg = "PAN numbers should have 5 characters followed by 4 numbers then a final character" + Environment.NewLine + "4th character should be in (C,P,H,F,A,T,B,L,J,G) "
                    If IsValidateCustomerPANwithName Then
                        msg += " and 5th should be 1st character of Assessee's Last Name/Surname."
                    End If
                End If
            Else
                msg = ""
                If clsCommon.myLen(PanValue.Trim()) = 5 Then
                    Dim checkPan1 As New System.Text.RegularExpressions.Regex("^([A-Z]){5}?$")
                    If Not checkPan1.IsMatch(PanValue.Trim()) Then
                        msg = "PAN numbers should have 5 characters."
                    End If
                End If
                If clsCommon.myLen(PanValue.Trim()) >= 4 Then
                    fourth_char = clsCommon.myCstr(PanValue).Trim().Substring(3, 1)
                    If clsCommon.CompairString(fourth_char, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "H") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "A") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "T") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "L") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "J") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(fourth_char, "G") <> CompairStringResult.Equal Then
                        msg = "4th character of PAN number should be in (C,P,H,F,A,T,B,L,J,G)."
                    End If
                End If
                If clsCommon.myLen(PanValue.Trim()) >= 5 Then
                    Fifth_Char = clsCommon.myCstr(PanValue).Trim().Substring(4, 1)

                    NameSplit = PersonName.Split(" ")
                    Dim counter As Integer = 0
                    If NameSplit IsNot Nothing AndAlso NameSplit.Length > 0 Then
                        For ii As Integer = 0 To NameSplit.Length - 1
                            First_Char = clsCommon.myCstr(NameSplit(ii)).Trim().Substring(0, 1)
                            If clsCommon.CompairString(Fifth_Char, First_Char) = CompairStringResult.Equal Then
                                counter += 1
                            End If
                        Next
                    End If
                    If IsValidateCustomerPANwithName Then
                        If counter <= 0 Then
                            msg = "5th word of PAN number should be 1st character of Assessee's Last Name/Surname."
                        End If
                    End If
                End If
                If clsCommon.myLen(PanValue.Trim()) = 9 Then
                    Dim checkPan1 As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}?$")
                    If Not checkPan1.IsMatch(PanValue.Trim()) Then
                        msg = "PAN numbers should have 5 characters followed by 4 numbers."
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return msg
    End Function
    Public Shared Function CompanyAddresShowinHeaderPartForERODE() As DataTable
        Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Add1 as Company_Add1, TSPL_COMPANY_MASTER.add2 as Company_Add2, TSPL_COMPANY_MASTER.Add3 as Company_Add3,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Tcan_No AS Website,TSPL_COMPANY_MASTER.Access_Officer as FSSAI_NO,TSPL_COMPANY_MASTER.Email AS Comp_Email,TSPL_COMPANY_MASTER.CINNo as CORP_NO,TSPL_COMPANY_MASTER.Pan_No AS Comp_PanNo,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) AS TinNo_Issue_Date ,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) AS PanNo_Issue_Date,GSTReg_No,TSPL_STATE_MASTER.STATE_NAME   from tspl_company_master  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State ")
    End Function
    Public Shared Function CompanyAddresShowinFooter() As DataTable
        Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then ', '+TSPL_STATE_MASTER.STATE_NAME  else '' end + case when LEN(TSPL_COMPANY_MASTER.Pincode)>0 then ' - '+TSPL_COMPANY_MASTER.Pincode else '' end as companyaddress,TSPL_COMPANY_MASTER.CINNo  as cin,TSPL_COMPANY_MASTER.Pan_No as pan,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER .Bank_Name,TSPL_COMPANY_MASTER.BankAccountNo ,TSPL_COMPANY_MASTER.BankIFSCCode ,TSPL_COMPANY_MASTER.BankBranchAddress from TSPL_COMPANY_MASTER  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State")
    End Function
    Public Shared Function CompanyAddresShowinFooter(ByVal trans As SqlTransaction) As DataTable
        Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then ', '+TSPL_STATE_MASTER.STATE_NAME  else '' end + case when LEN(TSPL_COMPANY_MASTER.Pincode)>0 then ' - '+TSPL_COMPANY_MASTER.Pincode else '' end as companyaddress,TSPL_COMPANY_MASTER.CINNo  as cin,TSPL_COMPANY_MASTER.Pan_No as pan,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER .Bank_Name,TSPL_COMPANY_MASTER.BankAccountNo ,TSPL_COMPANY_MASTER.BankIFSCCode ,TSPL_COMPANY_MASTER.BankBranchAddress from TSPL_COMPANY_MASTER  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State", trans)
    End Function
    Public Shared Function CompanyAddresShowinFooterForJakson() As DataTable
        Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then ', '+TSPL_STATE_MASTER.STATE_NAME  else '' end + case when LEN(TSPL_COMPANY_MASTER.Pincode)>0 then ' - '+TSPL_COMPANY_MASTER.Pincode else '' end +case when LEN(TSPL_COMPANY_MASTER.Fax )>0 then ' ,Fax - '+TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end +Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' end +case when LEN(TSPL_COMPANY_MASTER.Email  )>0 then ' ,Email- '+TSPL_COMPANY_MASTER.Email  else '' end+case when LEN(TSPL_COMPANY_MASTER.Pan_No   )>0 then ' ,PAN- '+TSPL_COMPANY_MASTER.Pan_No  else '' end as companyaddress,TSPL_COMPANY_MASTER.CINNo  as cin,TSPL_COMPANY_MASTER.Pan_No as pan,TSPL_COMPANY_MASTER.Logo_Img from TSPL_COMPANY_MASTER  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State")
    End Function
    '===shivani
    Public Shared Function CompanyAddresShowinHeader() As DataTable
        Return clsDBFuncationality.GetDataTable("select Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' '  end  as Loc_Add from TSPL_COMPANY_MASTER ")
    End Function
    Public Shared Function CompanyAddresInvoiceHeader() As DataTable
        Return clsDBFuncationality.GetDataTable("select TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 from TSPL_COMPANY_MASTER  ")
    End Function

    Public Shared Function isValueInUse(ByVal value As String, ByVal tableName As String, ByVal fieldName As String) As Boolean
        Dim rValue As Boolean = False
        Dim tblname As String = ""
        Dim fldname As String = ""
        Dim cnt As Integer = 0
        Dim qry As String = "SELECT R.TABLE_NAME,R.COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE U INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS FK     ON U.CONSTRAINT_CATALOG = FK.UNIQUE_CONSTRAINT_CATALOG     AND U.CONSTRAINT_SCHEMA = FK.UNIQUE_CONSTRAINT_SCHEMA     AND U.CONSTRAINT_NAME = FK.UNIQUE_CONSTRAINT_NAME  INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE R    ON R.CONSTRAINT_CATALOG = FK.CONSTRAINT_CATALOG     AND R.CONSTRAINT_SCHEMA = FK.CONSTRAINT_SCHEMA     AND R.CONSTRAINT_NAME = FK.CONSTRAINT_NAME WHERE U.COLUMN_NAME = '" & fieldName & "'   AND U.TABLE_NAME = '" & tableName & "'    "
        Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
            qry = ""
            For i As Integer = 0 To dtbl.Rows.Count - 1
                tblname = clsCommon.myCstr(dtbl.Rows(i)("TABLE_NAME"))
                fldname = clsCommon.myCstr(dtbl.Rows(i)("COLUMN_NAME"))
                qry = qry & " select " & fldname & " from " & tblname & " where " & fldname & " ='" & value & "'"
                If i <> (dtbl.Rows.Count - 1) Then
                    qry = qry & Environment.NewLine & " UNION ALL " & Environment.NewLine
                End If
            Next
            Dim dtbl1 As DataTable = clsDBFuncationality.GetDataTable("select *  from (" & qry & " ) as x")
            If dtbl1.Rows.Count > 0 Then
                rValue = True
            Else
                rValue = False
            End If
        End If
        Return rValue
    End Function

    '' Anubhooti Perfix For Vendor 01-Sep-2014 -------------------------------------------------
    Private Shared Function GetVendorQryOFDOCPrefix(ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocatinSegmentCode As String, ByVal IntFiscalYear As Integer, ByVal isTakingTopOne As Boolean) As String
        Dim qry As String = "select " + IIf(isTakingTopOne, " Top 1", "") + " Doc_Prfeix,Fin_Year,Next_Number,Separator,Is_Change_monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,MinSizeofSeries from TSPL_DOCPREFIX_MASTER where  Doc_Type='" + strDocType + "' and  isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocatinSegmentCode + "' and Fin_Year='" + clsCommon.myCstr(IntFiscalYear) + "'"
        If isTakingTopOne Then
            qry += " order by case when Is_Change_Daily=1 then  CONVERT(varchar, Curr_Date,112) else  CONVERT(varchar, Curr_Month ) end desc"
        Else
            qry += " and 2=(case when Is_Change_Daily=1 then case when Curr_Date= '" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'  then 2 else 3  end   else   case when Is_Change_monthly=1 then case when Curr_Month= " + clsCommon.myCstr(dtDocDate.Month) + "  then 2 else 3  end else 2 end end)"
        End If
        Return qry
    End Function

    Public Shared Function GetVendorNextCode(ByVal TableName As String, ByVal FieldName As String, ByVal StrVenName As String, ByVal trans As SqlTransaction) As String

        If clsCommon.myLen(StrVenName) <= 0 Then
            Throw New Exception("Please enter Description")
        End If
        StrVenName = StrVenName.Substring(0, 1)
        Dim qry As String = ""
        Dim DigitLen As String = ""
        Dim Digits As Double
        Dim strRetCode As String = ""

        Dim strLocatinSegmentCode As String = ""
        ' Dim dt As DataTable
        If clsCommon.myLen(StrVenName) > 0 Then
            ' Dim dt1 As DataTable
            Dim qry1 As String = "Select COUNT(*) AS Row From " + TableName + "  Where " + FieldName & " like '" + StrVenName + "%'"
            Dim VNameSeries As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1, trans))
            If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForVendor, clsFixedParameterCode.AutoGeneratedDigitsForVendor, trans))
            ElseIf clsCommon.CompairString(TableName, "TSPL_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForCustomer, clsFixedParameterCode.AutoGeneratedDigitsForCustomer, trans))
            End If
            Digits -= clsCommon.myLen(VNameSeries)
            If clsCommon.myLen(Digits) > 0 Then
                For dig As Integer = 1 To Digits
                    DigitLen += "0"
                Next
            End If

            If VNameSeries = 0 Then
                VNameSeries = 1
            Else
                VNameSeries = 1 + VNameSeries
            End If

            strRetCode = StrVenName.ToUpper() + DigitLen + clsCommon.myCstr(VNameSeries)
            Dim dt As DataTable = Nothing
            Dim blCondition As Boolean = True
            While blCondition
                If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From tspl_vendor_master where vendor_code='" + strRetCode + "'", trans)
                ElseIf clsCommon.CompairString(TableName, "TSPL_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From TSPL_CUSTOMER_MASTER where Cust_Code='" + strRetCode + "'", trans)
                End If

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    blCondition = False
                Else
                    blCondition = True
                    strRetCode = clsCommon.incval(strRetCode)
                End If
            End While

        End If
        Return strRetCode
    End Function

    Public Shared Function DemoGetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal strDatabase As String) As String
        Return DemoGetNextCode(trans, dtDocDate, strDocType, strTransType, strLocationCode, False, True, strDatabase)
    End Function


    Public Shared Function DemoGetNextCode(ByVal trans As SqlTransaction, ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isIncreaseCounter As Boolean, ByVal strDatabase As String) As String
        Dim qry As String = ""
        Dim strRetCode As String = ""
        Dim strLocatinSegmentCode As String = ""
        Dim dt As DataTable
        If isLocationCodeisSegment Then
            qry = "SELECT 1 from " + strDatabase + ".dbo.TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code='" + strLocationCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strLocationCode + " is not a Location Segment")
            End If
            strLocatinSegmentCode = strLocationCode
        Else
            If clsCommon.myLen(strLocationCode) > 0 Then
                strLocatinSegmentCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + strLocationCode + "'", trans))
                If clsCommon.myLen(strLocatinSegmentCode) <= 0 Then
                    Throw New Exception("Location Segment code Not found for Location :" + strLocationCode)
                End If
            End If
        End If

        Dim IntFiscalYear As Integer = dtDocDate.Year
        If dtDocDate.Month >= 1 AndAlso dtDocDate.Month <= 3 Then
            IntFiscalYear -= 1
        End If


        qry = DemoGetQryOFDOCPrefix(dtDocDate, strDocType, strTransType, strLocatinSegmentCode, IntFiscalYear, False, strDatabase)
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            qry = DemoGetQryOFDOCPrefix(dtDocDate.AddMonths(-1), strDocType, strTransType, strLocatinSegmentCode, IntFiscalYear, True, strDatabase)
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso (clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1 OrElse clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1) Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Type", strDocType)
                clsCommon.AddColumnsForChange(coll, "Doc_Trans_Type", strTransType)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocatinSegmentCode)
                clsCommon.AddColumnsForChange(coll, "Doc_Prfeix", clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")))
                clsCommon.AddColumnsForChange(coll, "Fin_Year", IntFiscalYear)
                clsCommon.AddColumnsForChange(coll, "Next_Number", 1)
                clsCommon.AddColumnsForChange(coll, "Separator", clsCommon.myCstr(dt.Rows(0)("Separator")))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                clsCommon.AddColumnsForChange(coll, "Is_Change_Monthly", clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Curr_Month", dtDocDate.Month)
                End If

                clsCommon.AddColumnsForChange(coll, "Is_Change_Daily", clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Curr_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "dontDisplayYearInSeries", dt.Rows(0)("dontDisplayYearInSeries"))
                clsCommon.AddColumnsForChange(coll, "MinSizeofSeries", dt.Rows(0)("MinSizeofSeries"))
                clsCommonFunctionality.UpdateDataTable(coll, strDatabase + ".dbo.TSPL_DOCPREFIX_MASTER", OMInsertOrUpdate.Insert, "", trans)

                qry = DemoGetQryOFDOCPrefix(dtDocDate, strDocType, strTransType, strLocatinSegmentCode, IntFiscalYear, False, strDatabase)
                dt = clsDBFuncationality.GetDataTable(qry, trans)
            End If
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Dim strException As String = "Please ask your Administrator to Set the Counter for " + strDocType
            If clsCommon.myLen(strLocatinSegmentCode) > 0 Then
                strException += Environment.NewLine + "Segment Location - " + strLocatinSegmentCode
            End If
            If clsCommon.myLen(strTransType) > 0 Then
                strException += Environment.NewLine + "Transaction Type - " + strTransType
            End If
            strException += Environment.NewLine + "For Fiscal year - " + clsCommon.myCstr(IntFiscalYear)
            Throw New Exception(strException)
        End If
        ''**********Generate Counter ************************
        Dim intCurrCounter As Integer = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("Next_Number")))
        Dim isDailyChange As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Change_Daily")) = 1, True, False)
        Dim intCurrDate As Date? = Nothing
        If isDailyChange Then
            intCurrDate = clsCommon.myCDate(dt.Rows(0)("Curr_Date"))
        End If



        Dim isMonthlyChange As Boolean = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Change_monthly")) = 1, True, False)
        Dim intCurrMonth As Integer = Convert.ToInt32(clsCommon.myCdbl(dt.Rows(0)("Curr_Month")))
        Dim strSep = clsCommon.myCstr(dt.Rows(0)("Separator")).Trim()

        Dim strFinYear As String = clsCommon.myCstr(dtDocDate.Year).Trim() 'clsCommon.myCstr(dt.Rows(0)("Fin_Year")).Trim()
        strFinYear = strFinYear.Substring(clsCommon.myLen(strFinYear) - 2, 2)
        If objCommonVar.CounterFinancialYearStyle Then
            Dim intYear As Integer = clsCommon.myCdbl(strFinYear)
            If dtDocDate.Month >= 1 AndAlso dtDocDate.Month <= 3 Then
                strFinYear = clsCommon.myCstr(intYear - 1) + "-" + clsCommon.myCstr(intYear)
            Else
                strFinYear = clsCommon.myCstr(intYear) + "-" + clsCommon.myCstr(intYear + 1)
            End If
        End If


        'strRetCode = clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")).Trim() + strSep + strFinYear + strSep
        If clsCommon.myCBool(dt.Rows(0)("dontDisplayYearInSeries")) = False Then
            strRetCode = clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")).Trim() + strSep + strFinYear + strSep
        Else
            strRetCode = clsCommon.myCstr(dt.Rows(0)("Doc_Prfeix")).Trim() + strSep
        End If
        Dim intNumPartLen As Integer = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
        If isDailyChange Then
            intNumPartLen = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
            strRetCode += IIf(intCurrDate.Value.Month < 10, "0", "") + clsCommon.myCstr(intCurrDate.Value.Month).Trim() + strSep + IIf(intCurrDate.Value.Day < 10, "0", "") + clsCommon.myCstr(intCurrDate.Value.Day).Trim() + strSep
        ElseIf isMonthlyChange Then
            intNumPartLen = clsCommon.myCdbl(dt.Rows(0)("MinSizeofSeries"))
            strRetCode += IIf(intCurrMonth < 10, "0", "") + clsCommon.myCstr(intCurrMonth).Trim() + strSep
        End If
        Dim intLen As Integer = clsCommon.myLen(intCurrCounter) ''clsCommon.myLen(dt.Rows(0)("Next_Number"))
        For ii As Integer = 1 To intNumPartLen - intLen
            strRetCode += "0"
        Next
        strRetCode += clsCommon.myCstr(intCurrCounter)
        CheckForValidCounter(strRetCode, strDocType, strTransType, trans)
        ''Throw New Exception(strRetCode)
        ''**********Increment Current Counter ************************

        If isIncreaseCounter Then
            intCurrCounter = intCurrCounter + 1
            qry = "update " + strDatabase + ".dbo.TSPL_DOCPREFIX_MASTER set Next_Number=" + clsCommon.myCstr(intCurrCounter) + ""
            qry += " where Fin_Year='" + clsCommon.myCstr(IntFiscalYear) + "' and Doc_Type='" + strDocType + "' and isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocatinSegmentCode + "' "
            If isDailyChange Then
                qry += " and Curr_Date='" + clsCommon.GetPrintDate(intCurrDate, "dd/MMM/yyyy") + "'"
            ElseIf isMonthlyChange Then
                qry += " and Curr_Month='" + clsCommon.myCstr(intCurrMonth) + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return strRetCode
    End Function

    Public Shared Sub ChangeSalesman(ByVal trans As SqlTransaction, ByVal saleInvoiceNo As String, ByRef shipmentNo As String, ByVal salesmanCode As String, ByRef salesmanName As String)
        Try
            Dim strSaleInv As String = "Update TSPL_SALE_INVOICE_HEAD set Salesman_Code ='" + salesmanCode + "' Where Sale_Invoice_No ='" + saleInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(strSaleInv, trans)
            Dim strShipment As String = "Update TSPL_SHIPMENT_MASTER set Salesman_Code ='" + salesmanCode + "' Where Shipment_No ='" + shipmentNo + "'"
            clsDBFuncationality.ExecuteNonQuery(strShipment, trans)
            Dim strEmptyTrans As String = "Update TSPL_ADJUSTMENT_HEADER set EMP_CODE='" + salesmanCode + "', EMP_NAME='" + salesmanName + "' Where ItemType='E' AND Reference_Document='Sale Invoice' and Document_No='" + saleInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(strEmptyTrans, trans)
            Dim strSalesReturn As String = "Update TSPL_SALE_RETURN_HEAD set Salesman_Code ='" + salesmanCode + "' Where Invoice_No ='" + saleInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(strSalesReturn, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function CheckForValidCounter(ByVal strRetCode As String, ByVal strDocType As String, ByVal strTransType As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.CompairString(strDocType, clsDocType.SaleInvoice) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strTransType, clsDocTransactionType.SaleInvoiceExcise) = CompairStringResult.Equal Then
            Dim qry As String = "select code from("
            qry += " select Sale_Invoice_No as Code from TSPL_SALE_INVOICE_HEAD"
            qry += " union all"
            qry += " select Transfer_No as Code from TSPL_TRANSFER_HEAD"
            qry += " union all "
            qry += " select Doc_No as Code from TSPL_IssueReturn_HEAD"
            qry += " union all "
            qry += " select shipment_No as Code from TSPL_SCRAPSALE_HEAD"
            qry += " union all "
            qry += " select invoice_No as Code from TSPL_SCRAPINVOICE_HEAD) xxx where code='" + strRetCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Auto Generated Code " + strRetCode + " is already in use.")
            End If
        End If
        Return True
    End Function


    Public Shared Function CheckForUniqueCounter(ByVal strRetCode As String, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocation As String, ByVal strFiscalYear As String, ByVal trans As SqlTransaction) As Boolean
        Dim StrMessage As String = ""
        Dim StrDoc As String = ""
        Try
            Dim qry As String = ""
            StrDoc = "Document Code [" + strRetCode + "] is already generated."

            qry = "INSERT INTO TSPL_GENERATE_DOCUMENT_CODE (DOCUMENT_CODE,Doc_Type,Doc_Trans_Type,Location_Code,Fin_Year) values ('" + strRetCode + "','" + strDocType + "','" + strTransType + "','" + strLocation + "','" + strFiscalYear + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Dim qry As String = "select * from TSPL_GENERATE_DOCUMENT_CODE where document_code='" + strRetCode + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                StrMessage += "Trying to generate-"

                If clsCommon.myLen(strDocType) > 0 Then
                    StrMessage += Environment.NewLine + "Document Type [" + clsCommon.myCstr(strDocType) + "]"
                End If
                If clsCommon.myLen(strTransType) > 0 Then
                    StrMessage += Environment.NewLine + "Transaction Type [" + clsCommon.myCstr(strTransType) + "]"
                End If
                If clsCommon.myLen(strLocation) > 0 Then
                    StrMessage += Environment.NewLine + "Location Code [" + clsCommon.myCstr(strLocation) + "]"
                End If
                If clsCommon.myLen(strFiscalYear) > 0 Then
                    StrMessage += Environment.NewLine + "Financial Year [" + clsCommon.myCstr(strFiscalYear) + "]"
                End If
                StrMessage += Environment.NewLine + "-----------------------------------------------------------"

                StrMessage += Environment.NewLine + StrDoc

                If clsCommon.myLen(dt.Rows(0).Item("Doc_Type")) > 0 Then
                    StrMessage += Environment.NewLine + "Document Type [" + clsCommon.myCstr(dt.Rows(0).Item("Doc_Type")) + "]"
                End If
                If clsCommon.myLen(dt.Rows(0).Item("Doc_Trans_Type")) > 0 Then
                    StrMessage += Environment.NewLine + "Transaction Type [" + clsCommon.myCstr(dt.Rows(0).Item("Doc_Trans_Type")) + "]"
                End If
                If clsCommon.myLen(dt.Rows(0).Item("Location_Code")) > 0 Then
                    StrMessage += Environment.NewLine + "Location Code [" + clsCommon.myCstr(dt.Rows(0).Item("Location_Code")) + "]"
                End If
                If clsCommon.myLen(dt.Rows(0).Item("Fin_Year")) > 0 Then
                    StrMessage += Environment.NewLine + "Financial Year [" + clsCommon.myCstr(dt.Rows(0).Item("Fin_Year")) + "]"
                End If
            End If

            Throw New Exception(StrMessage)
        End Try
        Return True
    End Function

    Private Shared Function DemoGetQryOFDOCPrefix(ByVal dtDocDate As Date, ByVal strDocType As String, ByVal strTransType As String, ByVal strLocatinSegmentCode As String, ByVal IntFiscalYear As Integer, ByVal isTakingTopOne As Boolean, ByVal strDatabase As String) As String
        Dim qry As String = "select " + IIf(isTakingTopOne, " Top 1", "") + " Doc_Prfeix,Fin_Year,Next_Number,Separator,Is_Change_monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,MinSizeofSeries from " + strDatabase + ".dbo.TSPL_DOCPREFIX_MASTER where  Doc_Type='" + strDocType + "' and  isnull(Doc_Trans_Type,'')='" + strTransType + "' and isnull(Location_Code,'')='" + strLocatinSegmentCode + "' and Fin_Year='" + clsCommon.myCstr(IntFiscalYear) + "'"
        If isTakingTopOne Then
            qry += " order by case when Is_Change_Daily=1 then  CONVERT(varchar, Curr_Date,112) else  CONVERT(varchar, Curr_Month ) end desc"
        Else
            qry += " and 2=(case when Is_Change_Daily=1 then case when Curr_Date= '" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'  then 2 else 3  end   else   case when Is_Change_monthly=1 then case when Curr_Month= " + clsCommon.myCstr(dtDocDate.Month) + "  then 2 else 3  end else 2 end end)"
        End If
        Return qry
    End Function

    Public Shared Function UserAvailableAccountQuery() As String
        Return "select Account_Code as Code from TSPL_GL_ACCOUNT_PERMISSION WHERE User_Code='" + objCommonVar.CurrentUserCode + "' "
    End Function

    Public Shared Function UserAvailableLocationQuery() As String
        ''7 is hardcoded to get only location
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            '  Return "Select Distinct LM.Location_Code as Code,LM.Location_Desc as Description,LM.Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable' from TSPL_LOCATION_MASTER as LM INNER JOIN TSPL_GL_SEGMENT_PERMISSION GSP ON LM.Loc_Segment_Code=GSP.Segment_Code"
            Return "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM where 2=2 "
        Else
            Return "Select Distinct LM.Location_Code as Code,LM.Location_Desc as Description,LM.Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable' from TSPL_LOCATION_MASTER as LM INNER JOIN TSPL_GL_SEGMENT_PERMISSION GSP ON LM.Loc_Segment_Code=GSP.Segment_Code where GSP.User_Code='" + objCommonVar.CurrentUserCode + "'"

        End If
        '  Return "select TSPL_GL_SEGMENT_PERMISSION.Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Description from TSPL_GL_SEGMENT_PERMISSION left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_SEGMENT_PERMISSION.Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No=TSPL_GL_SEGMENT_PERMISSION.GL_Segment where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_CODE.Seg_No=7 "
    End Function

    Public Shared Function UserAvailableLocationQuery(ByRef whrClas As String) As String
        whrClas = " 2=2 "
        ''7 is hardcoded to get only location
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            Return "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM"
        Else
            Return "Select Distinct LM.Location_Code as Code,LM.Location_Desc as Description,LM.Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable' from TSPL_LOCATION_MASTER as LM INNER JOIN TSPL_GL_SEGMENT_PERMISSION GSP ON LM.Loc_Segment_Code=GSP.Segment_Code "
            whrClas = " and GSP.User_Code='" + objCommonVar.CurrentUserCode + "'"
        End If
    End Function

    Public Shared Function UserAvailableLocationCodeQuery() As String
        ''7 is hardcoded to get only location
        Return "select Segment_Code as Code from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' "
    End Function

    Public Shared Function UserAvailableLocationData() As List(Of String)
        Dim Arr As New List(Of String)
        Dim qry As String = "select TSPL_GL_SEGMENT_PERMISSION.Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Description from TSPL_GL_SEGMENT_PERMISSION left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_SEGMENT_PERMISSION.Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No=TSPL_GL_SEGMENT_PERMISSION.GL_Segment where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_CODE.Seg_No=7 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Arr.Add(clsCommon.myCstr(dr("Code")))
            Next
        End If
        Return Arr
    End Function

    Public Shared Function UserWiseAvailableLocationSegment() As String
        Dim strLocationsSegment As String = ""
        Try
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                Dim qry As String = "SELECT SEGMENT_CODE FROM TSPL_GL_SEGMENT_CODE WHERE TSPL_GL_SEGMENT_CODE.Seg_No='7'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(strLocationsSegment) > 0 Then
                            strLocationsSegment += ","
                        End If
                        strLocationsSegment += "'" + clsCommon.myCstr(dr("Segment_Code")) + "'"
                    Next
                End If
            Else
                Dim qry As String = "select TSPL_LOCATION_MASTER.Loc_Segment_Code from tspl_user_master inner join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
                Dim tempSementCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(tempSementCode) > 0 Then
                    strLocationsSegment = "'" + tempSementCode + "'"
                End If

                qry = "select distinct Segment_Code from TSPL_GL_SEGMENT_PERMISSION where User_Code = '" + objCommonVar.CurrentUserCode + "' and GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and Segment_Code<>'" + tempSementCode + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(strLocationsSegment) > 0 Then
                            strLocationsSegment += ","
                        End If
                        strLocationsSegment += "'" + clsCommon.myCstr(dr("Segment_Code")) + "'"
                    Next
                End If

            End If
            If clsCommon.myLen(strLocationsSegment) = 0 Then
                strLocationsSegment = "''"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strLocationsSegment
    End Function


    Public Shared Function UserWiseAvailableLocationCode() As String
        Dim strLocationsCode As String = ""
        Try
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                Dim qry As String = "SELECT location_code FROM tspl_location_master "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(strLocationsCode) > 0 Then
                            strLocationsCode += ","
                        End If
                        strLocationsCode += "'" + clsCommon.myCstr(dr("location_code")) + "'"
                    Next
                End If
            Else
                Dim qry As String = "SELECT tspl_user_master.default_location FROM tspl_user_master where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
                Dim tempLocationCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(tempLocationCode) > 0 Then
                    strLocationsCode = "'" + tempLocationCode + "'"
                End If

                qry = "select distinct TSPL_LOCATION_MASTER.Location_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and tspl_location_master.location_code<>'" + tempLocationCode + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(strLocationsCode) > 0 Then
                            strLocationsCode += ","
                        End If
                        strLocationsCode += "'" + clsCommon.myCstr(dr("Location_Code")) + "'"
                    Next
                End If

            End If
            If clsCommon.myLen(strLocationsCode) = 0 Then
                strLocationsCode = "''"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strLocationsCode
    End Function


    Public Shared Function UserAvailableTaxGroup() As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            Return "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code WHERE 0=0"
        Else
            Return "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code WHERE (Substring(TM.Tax_Liability_Account,6,3) in (" + UserAvailableLocationCodeQuery() + ")  OR TM.Tax_Liability_Account in (" + UserAvailableAccountQuery() + "))"
        End If
    End Function
    Public Shared Function UserAvailableTaxGroup(ByRef out As String) As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            Return "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code "
        Else
            Return "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code"
            out = " (Substring(TM.Tax_Liability_Account,6,3) in (" + UserAvailableLocationCodeQuery() + ")  OR TM.Tax_Liability_Account in (" + UserAvailableAccountQuery() + "))"
        End If
    End Function

    Public Shared Function UserAvailableCustomers() As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            Return "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account"
        Else
            Return "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name],m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account where (Substring(a.Receivable_Control_acct,6,3) in (" + UserAvailableLocationCodeQuery() + ") or a.Receivable_Control_acct in (" + UserAvailableAccountQuery() + ")) "
        End If
    End Function

    Public Shared Function UserAvailableCustomers(ByRef whrClas As String) As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            Return "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account"
        Else
            whrClas = "  (Substring(a.Receivable_Control_acct,6,3) in (" + UserAvailableLocationCodeQuery() + ") or a.Receivable_Control_acct in (" + UserAvailableAccountQuery() + "))"
            Return "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name],m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account "
        End If
    End Function

    Public Shared Function glvendorquery() As String
        Dim query As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            query = "select M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"

        Else
            Dim arrlocation As New ArrayList()
            Dim arraccount As New ArrayList()
            arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
            arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
            Dim countsegment As Integer = arrlocation.Count
            Dim countaccount As Integer = arraccount.Count
            If countaccount <> 0 Then
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = arraccount(0)
                    query = "select M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = "or s.Payable_Account like'" + value + "'"
                            query = query + stcode
                        End If
                    Next
                    For countacct As Integer = 0 To countaccount
                        If countacct < countaccount - 1 Then
                            Dim value As String = arraccount(countacct + 1)
                            Dim stcode As String = "or s.Payable_Account = '" + value + "'"
                            query = query + stcode
                        End If
                    Next
                Else
                    query = "select top 0  M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
                End If
            Else
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = String.Empty
                    query = "select M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = "or s.Payable_Account like'" + value + "'"
                            query = query + stcode
                        End If
                    Next
                    For countacct As Integer = 0 To countaccount
                        If countacct < countaccount - 1 Then
                            Dim value As String = arraccount(countacct + 1)
                            Dim stcode As String = "or s.Payable_Account = '" + value + "'"
                            query = query + stcode
                        End If
                    Next
                Else
                    query = "select top 0 M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
                End If
            End If
        End If
        Return query
    End Function

    '' Added By Pankal''' on  27/01/20121
    ''
    ''RICHA BM00000009847

    Public Shared Function glvendorqueryNew() As String
        Dim query As String
        ''If objCommonVar.CurrentUserCode = "ADMIN" Then
        'query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
        query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name],ISNULL(m.alies_name,'') As [Alies Name],isnull(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,'') as [VLC Uploader Code],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=m.Vendor_Code"

        ''Else
        ''    Dim arrlocation As New ArrayList()
        ''    Dim arraccount As New ArrayList()
        ''    arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
        ''    arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
        ''    Dim countsegment As Integer = arrlocation.Count
        ''    Dim countaccount As Integer = arraccount.Count
        ''    If countaccount <> 0 Then
        ''        If countsegment <> 0 Then
        ''            Dim valuefirstsegment As String = arrlocation(0)
        ''            valuefirstsegment = "%" + valuefirstsegment
        ''            Dim valuefirstacct As String = arraccount(0)
        ''            query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
        ''            For count As Integer = 0 To countsegment - 1
        ''                If count < countsegment - 1 Then
        ''                    Dim value As String = arrlocation(count + 1)
        ''                    value = "%" + value
        ''                    Dim stcode As String = "or s.Payable_Account like'" + value + "'"
        ''                    query = query + stcode
        ''                End If
        ''            Next
        ''            For countacct As Integer = 0 To countaccount
        ''                If countacct < countaccount - 1 Then
        ''                    Dim value As String = arraccount(countacct + 1)
        ''                    Dim stcode As String = "or s.Payable_Account = '" + value + "'"
        ''                    query = query + stcode
        ''                End If
        ''            Next
        ''        Else
        ''            query = "select top 0  M.Vendor_Code AS [Code], m.Vendor_Name as [Name] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
        ''        End If
        ''    Else
        ''        If countsegment <> 0 Then
        ''            Dim valuefirstsegment As String = arrlocation(0)
        ''            valuefirstsegment = "%" + valuefirstsegment
        ''            Dim valuefirstacct As String = String.Empty
        ''            query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
        ''            For count As Integer = 0 To countsegment - 1
        ''                If count < countsegment - 1 Then
        ''                    Dim value As String = arrlocation(count + 1)
        ''                    value = "%" + value
        ''                    Dim stcode As String = "or s.Payable_Account like'" + value + "'"
        ''                    query = query + stcode
        ''                End If
        ''            Next
        ''            For countacct As Integer = 0 To countaccount
        ''                If countacct < countaccount - 1 Then
        ''                    Dim value As String = arraccount(countacct + 1)
        ''                    Dim stcode As String = "or s.Payable_Account = '" + value + "'"
        ''                    query = query + stcode
        ''                End If
        ''            Next
        ''        Else
        ''            query = "select top 0 M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
        ''        End If
        ''    End If
        ''End If
        Return query
    End Function

    Public Shared Function glCustomerQuery() As String
        Dim query As String
        ''If objCommonVar.CurrentUserCode = "ADMIN" Then
        query = "select M.Cust_Code AS [Code], m.Customer_Name as [Name],ISNULL(m.Alies_Name,'') As [Alies Name] from TSPL_CUSTOMER_MASTER m join TSPL_CUSTOMER_ACCOUNT_SET s on m.Cust_Account=s.Cust_Account"
        ' ''Else
        ' ''    Dim arrlocation As New ArrayList()
        ' ''    Dim arraccount As New ArrayList()
        ' ''    arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
        ' ''    arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
        ' ''    Dim countsegment As Integer = arrlocation.Count
        ' ''    Dim countaccount As Integer = arraccount.Count
        ' ''    If countaccount <> 0 Then
        ' ''        If countsegment <> 0 Then
        ' ''            Dim valuefirstsegment As String = arrlocation(0)
        ' ''            valuefirstsegment = "%" + valuefirstsegment
        ' ''            Dim valuefirstacct As String = arraccount(0)
        ' ''            query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
        ' ''            For count As Integer = 0 To countsegment - 1
        ' ''                If count < countsegment - 1 Then
        ' ''                    Dim value As String = arrlocation(count + 1)
        ' ''                    value = "%" + value
        ' ''                    Dim stcode As String = "or s.Payable_Account like'" + value + "'"
        ' ''                    query = query + stcode
        ' ''                End If
        ' ''            Next
        ' ''            For countacct As Integer = 0 To countaccount
        ' ''                If countacct < countaccount - 1 Then
        ' ''                    Dim value As String = arraccount(countacct + 1)
        ' ''                    Dim stcode As String = "or s.Payable_Account = '" + value + "'"
        ' ''                    query = query + stcode
        ' ''                End If
        ' ''            Next
        ' ''        Else
        ' ''            query = "select top 0  M.Vendor_Code AS [Code], m.Vendor_Name as [Name] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
        ' ''        End If
        ' ''    Else
        ' ''        If countsegment <> 0 Then
        ' ''            Dim valuefirstsegment As String = arrlocation(0)
        ' ''            valuefirstsegment = "%" + valuefirstsegment
        ' ''            Dim valuefirstacct As String = String.Empty
        ' ''            query = "select M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where s.Payable_Account LIKE '" + valuefirstsegment + "' OR s.Payable_Account='" + valuefirstacct + "'"
        ' ''            For count As Integer = 0 To countsegment - 1
        ' ''                If count < countsegment - 1 Then
        ' ''                    Dim value As String = arrlocation(count + 1)
        ' ''                    value = "%" + value
        ' ''                    Dim stcode As String = "or s.Payable_Account like'" + value + "'"
        ' ''                    query = query + stcode
        ' ''                End If
        ' ''            Next
        ' ''            For countacct As Integer = 0 To countaccount
        ' ''                If countacct < countaccount - 1 Then
        ' ''                    Dim value As String = arraccount(countacct + 1)
        ' ''                    Dim stcode As String = "or s.Payable_Account = '" + value + "'"
        ' ''                    query = query + stcode
        ' ''                End If
        ' ''            Next
        ' ''        Else
        ' ''            query = "select top 0 M.Vendor_Code AS [Code], m.Vendor_Name as [Name]   from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code"
        ' ''        End If
        ' ''    End If
        ''End If
        Return query
    End Function

    Public Shared Function glbankquery() As String
        Dim query As String
        If objCommonVar.CurrentUserCode = "ADMIN" Then
            query = "select BANK_CODE as [Bank Code], DESCRIPTION  from TSPL_BANK_MASTER "
        Else
            Dim arrlocation As New ArrayList()
            Dim arraccount As New ArrayList()

            arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
            arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
            Dim countsegment As Integer = arrlocation.Count
            Dim countaccount As Integer = arraccount.Count
            If countaccount <> 0 Then
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = arraccount(0)
                    query = "select BANK_CODE as [Bank Code], DESCRIPTION  from TSPL_BANK_MASTER where bankacc like '" + valuefirstsegment + "' OR bankacc ='" + valuefirstacct + "'"
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = "or bankacc like'" + value + "'"
                            query = query + stcode
                        End If
                    Next
                    For countacct As Integer = 0 To countaccount
                        If countacct < countaccount - 1 Then
                            Dim value As String = arraccount(countacct + 1)
                            Dim stcode As String = "or bankacc = '" + value + "'"
                            query = query + stcode
                        End If
                    Next
                Else
                    query = "select top 0 BANK_CODE as [Bank Code], DESCRIPTION  from TSPL_BANK_MASTER"

                End If
            Else
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = String.Empty
                    query = "select BANK_CODE as [Bank Code], DESCRIPTION  from TSPL_BANK_MASTER where bankacc like '" + valuefirstsegment + "' OR bankacc ='" + valuefirstacct + "'"
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = "or bankacc like'" + value + "'"
                            query = query + stcode
                        End If
                    Next
                    For countacct As Integer = 0 To countaccount
                        If countacct < countaccount - 1 Then
                            Dim value As String = arraccount(countacct + 1)
                            Dim stcode As String = "or bankacc = '" + value + "'"
                            query = query + stcode
                        End If
                    Next
                Else
                    query = "select top 0 BANK_CODE as [Bank Code], DESCRIPTION  from TSPL_BANK_MASTER"
                End If
            End If
        End If


        Return query
    End Function

    ''' Added By Pankaj''''on 27/01/2012
    ''' Add By Preeti Gupta 22/07/2014---


    Public Shared Sub GlLOCandACCArray(ByRef Arrloc As ArrayList, ByRef ArrAcc As ArrayList)
        ArrAcc = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
        Arrloc = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
    End Sub


    'Public Shared Function glaccountquery() As String
    '    Dim qry As String = ""
    '    Dim whrCls As String = ""
    '    Dim arrlocation As New ArrayList()
    '    Dim arraccount As New ArrayList()
    '    If clsCommon.myCstr(objCommonVar.CurrentUserCode).ToUpper() = "ADMIN" Then
    '        qry = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '    Else
    '        arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
    '        arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
    '        Dim straccount As String = connectSql.funglaccount(objCommonVar.CurrentUserCode)
    '        Dim countsegment As Integer = arrlocation.Count
    '        Dim countaccount As Integer = arraccount.Count

    '        If countaccount <> 0 Then
    '            If countsegment <> 0 Then
    '                Dim valuefirstsegment As String = arrlocation(0)
    '                valuefirstsegment = "%" + valuefirstsegment
    '                Dim valuefirstacct As String = arraccount(0)
    '                qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = " where Account_Code like '" + valuefirstsegment + "' or Account_Code = '" + valuefirstacct + "' "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '                For countacct As Integer = 0 To countaccount
    '                    If countacct < countaccount - 1 Then
    '                        Dim value As String = arraccount(countacct + 1)
    '                        Dim stcode As String = "or Account_Code = '" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '                qry = qry + whrCls
    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If

    '        Else
    '            If countsegment <> 0 Then
    '                Dim valuefirst As String = arrlocation(0)
    '                valuefirst = "%" + valuefirst
    '                qry = "select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = " where Account_Code like  '" + valuefirst + "'  "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next

    '                qry = qry + whrCls

    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If
    '        End If
    '    End If
    '    If clsCommon.myLen(whrCls) > 0 Then
    '        qry = qry + whrCls + " and ControlAccount ='N'"
    '    Else
    '        qry = qry + " where ControlAccount ='N'"
    '    End If
    '    Return qry

    'End Function

    'Public Shared Function glaccountquery(ByVal code As String) As ArrayList
    '    Dim arrlist As New ArrayList()
    '    Dim qry As String = ""
    '    Dim whrCls As String = ""
    '    Dim arrlocation As New ArrayList()
    '    Dim arraccount As New ArrayList()
    '    If clsCommon.myCstr(objCommonVar.CurrentUserCode).ToUpper() = "ADMIN" Then
    '        qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '        whrCls = ""
    '    Else
    '        arraccount = connectSql.funglaccountmultiple(code)
    '        arrlocation = connectSql.funglsegmentmultiple(code)
    '        Dim straccount As String = connectSql.funglaccount(code)
    '        Dim countsegment As Integer = arrlocation.Count
    '        Dim countaccount As Integer = arraccount.Count
    '        If countaccount <> 0 Then
    '            If countsegment <> 0 Then
    '                Dim valuefirstsegment As String = arrlocation(0)
    '                valuefirstsegment = "%" + valuefirstsegment
    '                Dim valuefirstacct As String = arraccount(0)
    '                qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = " (Account_Code like '" + valuefirstsegment + "' or Account_Code = '" + valuefirstacct + "' "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '                For countacct As Integer = 0 To countaccount
    '                    If countacct < countaccount - 1 Then
    '                        Dim value As String = arraccount(countacct + 1)
    '                        Dim stcode As String = "or Account_Code = '" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If
    '        Else
    '            If countsegment <> 0 Then
    '                Dim valuefirst As String = arrlocation(0)
    '                valuefirst = "%" + valuefirst
    '                qry = "select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = " ( Account_Code like  '" + valuefirst + "'  "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If
    '        End If
    '    End If
    '    If clsCommon.myLen(whrCls) > 0 Then
    '        whrCls = whrCls + " ) and ControlAccount ='N'"
    '    Else
    '        whrCls = whrCls + " ControlAccount ='N'"
    '    End If


    '    arrlist.Add(qry)
    '    arrlist.Add(whrCls)
    '    Return arrlist
    'End Function




    '' Anubhooti 06-Nov-2014
    'Public Shared Function glaccountqueryForControlAcc(ByVal code As String) As ArrayList
    '    Dim arrlist As New ArrayList()
    '    Dim qry As String = ""
    '    Dim whrCls As String = ""
    '    Dim arrlocation As New ArrayList()
    '    Dim arraccount As New ArrayList()
    '    If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
    '        qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '        whrCls = ""
    '    Else
    '        arraccount = connectSql.funglaccountmultiple(code)
    '        arrlocation = connectSql.funglsegmentmultiple(code)
    '        Dim straccount As String = connectSql.funglaccount(code)
    '        Dim countsegment As Integer = arrlocation.Count
    '        Dim countaccount As Integer = arraccount.Count
    '        If countaccount <> 0 Then
    '            If countsegment <> 0 Then
    '                Dim valuefirstsegment As String = arrlocation(0)
    '                valuefirstsegment = "%" + valuefirstsegment
    '                Dim valuefirstacct As String = arraccount(0)
    '                qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = "Account_Code like '" + valuefirstsegment + "' or Account_Code = '" + valuefirstacct + "' "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '                For countacct As Integer = 0 To countaccount
    '                    If countacct < countaccount - 1 Then
    '                        Dim value As String = arraccount(countacct + 1)
    '                        Dim stcode As String = "or Account_Code = '" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If
    '        Else
    '            If countsegment <> 0 Then
    '                Dim valuefirst As String = arrlocation(0)
    '                valuefirst = "%" + valuefirst
    '                qry = "select Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '                whrCls = " Account_Code like  '" + valuefirst + "'  "
    '                For count As Integer = 0 To countsegment - 1
    '                    If count < countsegment - 1 Then
    '                        Dim value As String = arrlocation(count + 1)
    '                        value = "%" + value
    '                        Dim stcode As String = " or Account_Code like'" + value + "'"
    '                        whrCls = whrCls + stcode
    '                    End If
    '                Next
    '            Else
    '                qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
    '            End If
    '        End If
    '    End If
    '    If clsCommon.myLen(whrCls) > 0 Then
    '        whrCls = whrCls + " and ControlAccount ='Y'"
    '    Else
    '        whrCls = whrCls + " ControlAccount ='Y'"
    '    End If

    '    arrlist.Add(qry)
    '    arrlist.Add(whrCls)
    '    Return arrlist
    'End Function


    Public Shared Function glaccountquery() As String
        Dim arr As New ArrayList()
        arr = glaccountMainFunction(objCommonVar.CurrentUserCode, False)
        Return arr.Item(0) + " where " + arr.Item(1)
    End Function

    Public Shared Function glaccountquery(ByVal code As String) As ArrayList
        Return glaccountMainFunction(code, False)
    End Function

    Public Shared Function glaccountqueryForControlAcc(ByVal code As String) As ArrayList
        Return glaccountMainFunction(code, True)
    End Function

    Private Shared Function glaccountMainFunction(ByVal code As String, ByVal isControlAccount As Boolean) As ArrayList
        Dim arrlist As New ArrayList()
        Dim qry As String = ""
        Dim whrCls As String = ""
        Dim arrlocation As New ArrayList()
        Dim arraccount As New ArrayList()
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
            qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
            whrCls = ""
        Else
            arraccount = connectSql.funglaccountmultiple(code)
            arrlocation = connectSql.funglsegmentmultiple(code)
            Dim straccount As String = connectSql.funglaccount(code)
            Dim countsegment As Integer = arrlocation.Count
            Dim countaccount As Integer = arraccount.Count
            If countaccount <> 0 Then
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = arraccount(0)
                    qry = " select Account_Code , Description  from TSPL_GL_ACCOUNTS"
                    whrCls = "Account_Code like '" + valuefirstsegment + "' or Account_Code = '" + valuefirstacct + "' "
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = " or Account_Code like'" + value + "'"
                            whrCls = whrCls + stcode
                        End If
                    Next
                    For countacct As Integer = 0 To countaccount
                        If countacct < countaccount - 1 Then
                            Dim value As String = arraccount(countacct + 1)
                            Dim stcode As String = "or Account_Code = '" + value + "'"
                            whrCls = whrCls + stcode
                        End If
                    Next
                Else
                    qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
                End If
            Else
                If countsegment <> 0 Then
                    Dim valuefirst As String = arrlocation(0)
                    valuefirst = "%" + valuefirst
                    qry = "select Account_Code , Description  from TSPL_GL_ACCOUNTS"
                    whrCls = " Account_Code like  '" + valuefirst + "'  "
                    For count As Integer = 0 To countsegment - 1
                        If count < countsegment - 1 Then
                            Dim value As String = arrlocation(count + 1)
                            value = "%" + value
                            Dim stcode As String = " or Account_Code like'" + value + "'"
                            whrCls = whrCls + stcode
                        End If
                    Next
                Else
                    qry = "select top 0 Account_Code , Description  from TSPL_GL_ACCOUNTS"
                End If
            End If
        End If

        qry += Environment.NewLine + "left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code"
        If clsCommon.myLen(whrCls) <= 0 Then
            whrCls = " 2=2 "
        End If
        whrCls += " and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null "
        If isControlAccount Then
            whrCls = whrCls + " and ControlAccount ='Y' )"
        Else
            whrCls = whrCls + " and ControlAccount ='N' )"
        End If
        arrlist.Add(qry)
        arrlist.Add(whrCls)
        Return arrlist
    End Function

    Public Shared Function getRandomUserCode(tblName As String, UserCodeColumnName As String, LocCode As String, LocCodeColName As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        Try
            Dim qry As String = "select distinct " & UserCodeColumnName & "  from " & tblName & IIf(clsCommon.myLen(LocCodeColName) > 0, " where " & LocCodeColName & " ='" & LocCode & "'", "")
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim i As Integer = Rnd() * (dt.Rows.Count - 1)
                If i >= 0 Then
                    rValue = clsCommon.myCstr(dt.Rows(i)(UserCodeColumnName))
                Else
                    rValue = clsCommon.myCstr(dt.Rows(0)(UserCodeColumnName))
                End If
            Else
                rValue = objCommonVar.CurrentUserCode
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function


    Public Shared Function GetTableColumnNameForQry(ByVal strTableName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + strTableName + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        Dim strInvColumns As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim isFirstTime As Boolean = True
            For Each dr As DataRow In dt.Rows
                If Not isFirstTime Then
                    strInvColumns += ","
                End If
                strInvColumns += clsCommon.myCstr(dr("COLUMN_NAME"))
                isFirstTime = False
            Next
        End If
        Return strInvColumns
    End Function

    Public Shared Function isCurrentUserMCC() As Boolean
        Dim qry As String = "  select count(*) from tspl_mcc_master left outer join tspl_user_master on tspl_user_master.Default_Location=tspl_mcc_master.mcc_code where tspl_user_master.user_code='" & objCommonVar.CurrentUserCode & "' "
        If clsDBFuncationality.getSingleValue(qry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function isLocationMcc(ByVal strLoc As String) As Boolean
        Dim qry As String = "  select COUNT(*) from TSPL_LOCATION_MASTER where Location_Category='MCC' and Location_Code='" & strLoc & "' "
        If clsDBFuncationality.getSingleValue(qry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function isLocationMcc(ByVal strLoc As String, trans As SqlTransaction) As Boolean
        Dim qry As String = "  select COUNT(*) from TSPL_LOCATION_MASTER where Location_Category='MCC' and Location_Code='" & strLoc & "' "
        If clsDBFuncationality.getSingleValue(qry, trans) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function










    '    Public Shared Sub openJournalEntry(ByVal refDocNo As String)
    '        Dim qry As String = " select count(*) from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & refDocNo & "' "
    '        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
    '            clsCommon.MyMessageBoxShow("No Journal Entry Found For Current Document")
    '        Else
    '            Dim jNo As String = clsDBFuncationality.getSingleValue(" select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & refDocNo & "' ")
    '            Dim frm As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
    '            frm.strVoucherNo = jNo

    '            frm.WindowState = FormWindowState.Maximized
    '            frm.MdiParent = MDI
    '            frm.Show()
    '        End If
    '    End Sub

    '    Public Shared Sub openAPInvoiceEntry(ByVal refDocNo As String)
    '        Dim qry As String = " select count(*) from TSPL_VENDOR_INVOICE_HEAD where Description like '%" & refDocNo & "%' "
    '        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
    '            clsCommon.MyMessageBoxShow("No AP Invoice Entry Found For Current Document")
    '        Else
    '            Dim ApNo As String = clsDBFuncationality.getSingleValue(" select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Description like'%" & refDocNo & "%' ")
    '            Dim frm As New FrmAPInvoiceEntry()
    '            frm.strAPInvoice = ApNo
    '            frm.WindowState = FormWindowState.Maximized
    '            frm.MdiParent = MDI
    '            frm.Show()
    '        End If
    '    End Sub




    '#Region "Script Function"
    '    '------------------New Subroutine Make For Script Running-----Done By-Monika-28/05/2014---BM00000003099-------------------------------------------
    '    Private Shared Function CheckPrimaryKey(ByVal table_name As String, ByVal column_name As String, ByVal trans As SqlTransaction, Optional ByVal isDefault_Type As Boolean = False) As Boolean
    '        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where table_name='" + table_name + "'"
    '        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

    '        If check <= 0 Then
    '            Return True
    '        End If

    '        If isDefault_Type = False Then
    '            qry = "select column_name from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where table_name='" + table_name + "' and column_name='" + column_name + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            qry = "Select  SysObjects.[Name] As [Name] From SysObjects Inner Join (Select [Name],[ID] From SysObjects) As Tab On Tab.[ID] = Sysobjects.[Parent_Obj] Inner Join sysconstraints On sysconstraints.Constid = Sysobjects.[ID] Inner Join SysColumns Col On Col.[ColID] = sysconstraints.[ColID] And Col.[ID] = Tab.[ID] where Tab.name='" + table_name + "' and Col.name ='" + column_name + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        End If
    '    End Function

    '    Private Shared Sub DropConstraint(ByVal table_name As String, ByVal column_name As String, ByVal trans As SqlTransaction)
    '        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where table_name='" + table_name + "'"
    '        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

    '        If check <= 0 Then
    '            Exit Sub
    '        End If

    '        qry = "select CONSTRAINT_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where table_name='" + table_name + "' and column_name='" + column_name + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                qry = "alter table " + table_name + " drop constraint " + clsCommon.myCstr(dr("CONSTRAINT_NAME")) + ""
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            Next
    '        Else
    '            'Exit Function
    '        End If

    '        'qry = "select name from sys.objects where type_desc like '%constraint%' and object_name(parent_object_id)='" + table_name + "' and name like '%_" + column_name + "_%'"
    '        'dt = New DataTable()
    '        'dt = clsDBFuncationality.GetDataTable(qry, trans)

    '        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        '    For Each dr As DataRow In dt.Rows
    '        '        qry = "alter table " + table_name + " drop constraint " + clsCommon.myCstr(dr("NAME")) + ""
    '        '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '        '    Next
    '        'Else
    '        '    ' Exit Function
    '        'End If

    '        ''added by richa agarwal on 29/09/2014
    '        qry = "Select  SysObjects.[Name] As [Name] From SysObjects Inner Join (Select [Name],[ID] From SysObjects) As Tab On Tab.[ID] = Sysobjects.[Parent_Obj] Inner Join sysconstraints On sysconstraints.Constid = Sysobjects.[ID] Inner Join SysColumns Col On Col.[ColID] = sysconstraints.[ColID] And Col.[ID] = Tab.[ID] where Tab.name='" + table_name + "' and Col.name ='" + column_name + "'"
    '        dt = clsDBFuncationality.GetDataTable(qry, trans)

    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                qry = "alter table " + table_name + " drop constraint " + clsCommon.myCstr(dr("NAME")) + ""
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            Next
    '        Else
    '            Exit Sub
    '        End If
    '        ''========================================
    '    End Sub

    '    'Private Shared Function CheckColumnExist(ByVal table_name As String, ByVal column_name As String, ByVal datatype As String, ByVal trans As SqlTransaction) As Integer
    '    '    Dim qry As String = ""
    '    '    If clsCommon.myLen(datatype) > 0 Then
    '    '        qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='" + datatype + "'"
    '    '    Else
    '    '        qry = "select count(*) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + table_name + "' and COLUMN_NAME='" + column_name + "'"
    '    '    End If
    '    '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

    '    '    Return check
    '    'End Function
    '    ''By Balwinder due to not work properly on 2014-10-07
    '    Private Shared Function CheckColumnExist(ByVal table_name As String, ByVal column_name As String, ByVal datatype As DBDataType, ByVal MaxLength As Integer, ByVal ScaleForDecimal As Integer, ByVal trans As SqlTransaction) As Integer
    '        Dim qry As String = ""
    '        'If clsCommon.myLen(datatype) > 0 Then
    '        '    qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='" + datatype + "'"
    '        'Else
    '        '    qry = "select count(*) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + table_name + "' and COLUMN_NAME='" + column_name + "'"
    '        'End If

    '        Select Case datatype
    '            Case DBDataType.image_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='image'"
    '            Case DBDataType.int_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='int'"
    '            Case DBDataType.decimal_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='decimal'"
    '                If MaxLength > 0 Then
    '                    qry += " and NUMERIC_PRECISION='" + clsCommon.myCstr(MaxLength) + "'"
    '                End If
    '                If ScaleForDecimal > 0 Then
    '                    qry += " and NUMERIC_SCALE='" + clsCommon.myCstr(ScaleForDecimal) + "'"
    '                End If
    '            Case DBDataType.varbinary_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='varbinary'"
    '            Case DBDataType.text_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='text' "
    '            Case DBDataType.datetime_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='datetime' "
    '            Case DBDataType.time_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='time' "
    '            Case DBDataType.varchar_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='varchar' "
    '                If MaxLength > 0 Then
    '                    qry += " and CHARACTER_MAXIMUM_LENGTH='" + clsCommon.myCstr(MaxLength) + "'"
    '                End If
    '            Case DBDataType.numeric_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='numeric'"
    '                If MaxLength > 0 Then
    '                    qry += " and NUMERIC_PRECISION='" + clsCommon.myCstr(MaxLength) + "'"
    '                End If
    '                If ScaleForDecimal > 0 Then
    '                    qry += " and NUMERIC_SCALE='" + clsCommon.myCstr(ScaleForDecimal) + "'"
    '                End If
    '            Case DBDataType.nchar_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='nchar'"
    '            Case DBDataType.float_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='float'"
    '            Case DBDataType.date_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='date'"
    '            Case DBDataType.char_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='char'"
    '                If MaxLength > 0 Then
    '                    qry += " and CHARACTER_MAXIMUM_LENGTH='" + clsCommon.myCstr(MaxLength) + "'"
    '                End If
    '            Case DBDataType.bigint_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='bigint'"
    '            Case DBDataType.bit_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='bit'"
    '            Case DBDataType.nvarchar_Type
    '                qry = "select count(*) from information_schema.columns where table_name='" + table_name + "' and column_name='" + column_name + "' and data_type='nvarchar'"
    '                If MaxLength > 0 Then
    '                    qry += " and CHARACTER_MAXIMUM_LENGTH='" + clsCommon.myCstr(MaxLength) + "'"
    '                End If
    '            Case Else
    '                qry = "select count(*) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + table_name + "' and COLUMN_NAME='" + column_name + "'"
    '        End Select

    '        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

    '        Return check
    '    End Function

    '    Public Shared Function CheckTriggerExits(ByVal trg_name As String, ByVal trans As SqlTransaction) As Integer
    '        Try
    '            Dim sQuery = "SELECT count(*) FROM sys.triggers where name='" & trg_name & "'"
    '            Dim check As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
    '            Return check
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(ex.ToString)
    '        End Try
    '    End Function

    '    Public Shared Sub Pre_AlterOrUpdateScript(ByVal exeVersion As String)
    '        Dim qry As String = ""
    '        Dim check As Integer = 0
    '        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '        Dim dt As DataTable = Nothing
    '        Try
    '            If (clsCommon.CompairString("5.0.0.92", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.0.92") = CompairStringResult.Equal) Then
    '                '------------check already have primary key or not------------------
    '                If clsERPFuncationality.CheckPrimaryKey("tspl_vendor_master", "vendor_code", trans) = True Then
    '                Else
    '                    qry = "alter table tspl_vendor_master add primary key(vendor_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If
    '                '-----------------------------------------------------------------------------
    '            End If

    '            If (clsCommon.CompairString("5.0.0.98", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.0.98") = CompairStringResult.Equal) Then
    '                If clsERPFuncationality.CheckPrimaryKey("tspl_village_master", "village_code", trans) = True Then
    '                Else
    '                    qry = "alter table tspl_village_master add primary key(village_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If

    '                If clsERPFuncationality.CheckPrimaryKey("tspl_vlc_master_detail", "village_code", trans) = True Then
    '                Else
    '                    qry = "alter table tspl_vlc_master_detail add FOREIGN KEY(village_code) references TSPL_VILLAGE_MASTER(village_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If

    '                If clsERPFuncationality.CheckPrimaryKey("TSPL_MCC_MASTER", "city_code", trans) = True Then
    '                Else
    '                    qry = "alter table TSPL_MCC_MASTER add FOREIGN KEY(city_code) references TSPL_CITY_MASTER(city_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If
    '                If clsERPFuncationality.CheckPrimaryKey("TSPL_MCC_MASTER", "state_code", trans) = True Then
    '                Else
    '                    qry = "alter table TSPL_MCC_MASTER add FOREIGN KEY(state_code) references TSPL_state_MASTER(state_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If
    '                If clsERPFuncationality.CheckPrimaryKey("TSPL_MCC_MASTER", "country_code", trans) = True Then
    '                Else
    '                    qry = "alter table TSPL_MCC_MASTER add FOREIGN KEY(country_code) references TSPL_country_MASTER(country_code)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If
    '            End If
    '            If (clsCommon.CompairString("5.0.1.36", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString("5.0.1.36", exeVersion) = CompairStringResult.Equal) Then
    '                DropConstraint("tspl_location_master", "category_struct_code", trans)
    '                DropConstraint("tspl_vendor_master", "category_struct_code", trans)
    '                DropConstraint("tspl_customer_master", "category_struct_code", trans)
    '                DropConstraint("TSPL_PP_BOM_ITEM_DETAIL", "bom_code", trans)
    '                DropConstraint("TSPL_PP_BOM_STAGE_DETAIL", "bom_code", trans)
    '            End If


    '            If (clsCommon.CompairString("5.0.2.37", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString("5.0.2.37", exeVersion) = CompairStringResult.Equal) Then
    '                'check = CheckColumnExist("tspl_mcc_dispatch_challan", "payment_rate", DBDataType.varchar_Type, -1, 0, trans)
    '                'If check > 0 Then
    '                '    qry = "alter table tspl_mcc_dispatch_challan drop column payment_rate"
    '                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                'End If

    '                'check = CheckColumnExist("tspl_mcc_dispatch_challan", "tanker_transporter_name", DBDataType.varchar_Type, 30, 0, trans)
    '                'If check > 0 Then
    '                '    qry = "alter table tspl_mcc_dispatch_challan drop column tanker_transporter_name"
    '                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                'End If


    '                check = CheckColumnExist("tspl_vendor_master", "city_code", DBDataType.varchar_Type, 12, 0, trans)
    '                If check > 0 Then
    '                    qry = "alter table tspl_vendor_master alter column city_code varchar(50)"
    '                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                End If
    '            End If

    '            If (clsCommon.CompairString("5.0.3.19", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.3.19") = CompairStringResult.Equal) Then

    '                qry = "delete from TSPL_TRANSACTION_REVERSE_LOG where Program_Code in (select Program_Code from  TSPL_PROGRAM_MASTER where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code in(select Program_Code from TSPL_PROGRAM_MASTER where Program_Code='MProduction')))"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                qry = "delete from  TSPL_PROGRAM_MASTER where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code in(select Program_Code from TSPL_PROGRAM_MASTER where Program_Code='MProduction'))"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                qry = "delete from TSPL_PROGRAM_MASTER where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Program_Code='MProduction')"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                qry = "delete from TSPL_PROGRAM_MASTER where Program_Code='MProduction'"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)


    '                '-----------------------------------------------------------------------------
    '            End If
    '            'By pankaj jha to update vendor master delete stored procedure
    '            If (clsCommon.CompairString("5.0.3.58", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.3.58") = CompairStringResult.Equal) Then

    '                qry = " ALTER PROCEDURE [dbo].[sp_TSPL_VENDOR_MASTER_DELETE](@Vendor_Code varchar(12),@form_type varchar(10)) AS  BEGIN  delete from TSPL_MCC_ROUTE_VLC_MAPPING where VLC_CODE=(select VLC_CODE from TSPL_VLC_MASTER_HEad  where VSP_Code=@Vendor_Code ) delete from TSPL_VLC_MASTER_HEAD  where VSP_Code =@Vendor_Code  delete from tSPL_MCC_VSP_ChargeCategory_MAPPING where VSP_CODE =@Vendor_Code  DELETE TSPL_VENDOR_MASTER WHERE Vendor_Code=@Vendor_Code and Form_Type='VSP'  end "
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                '-----------------------------------------------------------------------------
    '            End If

    '            'By Rohit to update vendor master delete stored procedure
    '            If (clsCommon.CompairString("5.0.4.65", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.4.65") = CompairStringResult.Equal) Then

    '                qry = "  ALTER PROCEDURE [dbo].[sp_TSPL_VENDOR_MASTER_DELETE](@Vendor_Code varchar(12),@form_type varchar(10)) AS  if (@form_type='VSP')  begin delete from TSPL_MCC_ROUTE_VLC_MAPPING where VLC_CODE=(select VLC_CODE from TSPL_VLC_MASTER_HEad  where VSP_Code=@Vendor_Code )    delete from TSPL_VLC_MASTER_HEAD  where VSP_Code =@Vendor_Code delete from tSPL_MCC_VSP_ChargeCategory_MAPPING where VSP_CODE =@Vendor_Code DELETE TSPL_VENDOR_MASTER WHERE Vendor_Code=@Vendor_Code and Form_Type=@form_type end ELSE begin DELETE TSPL_VENDOR_MASTER WHERE Vendor_Code=@Vendor_Code and Form_Type=@form_type end"
    '                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                '-----------------------------------------------------------------------------
    '            End If

    '            'If (clsCommon.CompairString("5.0.3.64", exeVersion) = CompairStringResult.Less Or clsCommon.CompairString(exeVersion, "5.0.3.64") = CompairStringResult.Equal) Then

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_VLC_DATA_UPLOADER' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_VLC_DATA_UPLOADER');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_VLC_DATA_UPLOADER_Detail' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_VLC_DATA_UPLOADER_Detail');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_INVENTORY_MOVEMENT_NEW' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_INVENTORY_MOVEMENT_NEW');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_VENDOR_INVOICE_Detail' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_VENDOR_INVOICE_Detail');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_JOURNAL_Details' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_JOURNAL_Details');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '          " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_RECEIPT_DETAIL' " & _
    '            '          " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '          " FROM sys.key_constraints " & _
    '            '          " WHERE [type] = 'PK' " & _
    '            '          " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_RECEIPT_DETAIL');" & _
    '            '          " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '        " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SAMPLE_DETAIL' " & _
    '            '        " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '        " FROM sys.key_constraints " & _
    '            '        " WHERE [type] = 'PK' " & _
    '            '        " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SAMPLE_DETAIL');" & _
    '            '        " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '        " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SAMPLE_DETAIL_History' " & _
    '            '        " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '        " FROM sys.key_constraints " & _
    '            '        " WHERE [type] = 'PK' " & _
    '            '        " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SAMPLE_DETAIL_History');" & _
    '            '        " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '      " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_Shift_End_DETAIL' " & _
    '            '      " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '      " FROM sys.key_constraints " & _
    '            '      " WHERE [type] = 'PK' " & _
    '            '      " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_Shift_End_DETAIL');" & _
    '            '      " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '      " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_Shift_End_Route_DETAIL' " & _
    '            '      " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '      " FROM sys.key_constraints " & _
    '            '      " WHERE [type] = 'PK' " & _
    '            '      " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_Shift_End_Route_DETAIL');" & _
    '            '      " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '     " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SRN_DETAIL' " & _
    '            '     " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '     " FROM sys.key_constraints " & _
    '            '     " WHERE [type] = 'PK' " & _
    '            '     " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SRN_DETAIL');" & _
    '            '     " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SRN_Price_Charge_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SRN_Price_Charge_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SRN_VSP_Charge_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SRN_VSP_Charge_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MILK_SRN_VSP_Charge_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_MILK_SRN_VSP_Charge_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'Tspl_Milk_Truck_Sheet_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('Tspl_Milk_Truck_Sheet_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_Milk_Purchase_Invoice_Incentive_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_Milk_Purchase_Invoice_Incentive_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MCC_RATE_UPLOADER_MCC' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_MCC_RATE_UPLOADER_MCC');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            '    qry = "declare @sql varchar(1000)" & _
    '            '    " SELECT @sql = 'ALTER TABLE ' + 'TSPL_MCC_RATE_UPLOADER_Detail' " & _
    '            '    " + ' DROP CONSTRAINT  ' + name + ';' " & _
    '            '    " FROM sys.key_constraints " & _
    '            '    " WHERE [type] = 'PK' " & _
    '            '    " AND [parent_object_id] = OBJECT_ID('TSPL_MCC_RATE_UPLOADER_Detail');" & _
    '            '    " EXEC(@sql)"
    '            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '            '    '-----------------------------------------------------------------------------

    '            'End If

    '            If (clsCommon.CompairString("5.0.3.74", exeVersion) = CompairStringResult.Greater Or clsCommon.CompairString(exeVersion, "5.0.3.74") = CompairStringResult.Equal) Then
    '                DropConstraint("tspl_physical_stock", "Item_Code", trans)
    '                DropConstraint("tspl_physical_stock", "Location", trans)
    '                DropConstraint("tspl_physical_stock", "MRP", trans)
    '                DropConstraint("tspl_physical_stock", "Stock_Date", trans)
    '            End If
    '            trans.Commit()
    '        Catch ex As Exception
    '            trans.Rollback()
    '            clsCommon.MyMessageBoxShow(ex.Message)
    '        End Try
    '    End Sub




    '    Public Shared Sub UpdateCompCodes(ByVal ParamArray CompCode As String())
    '        Try
    '            If CompCode IsNot Nothing AndAlso CompCode.Length > 0 And clsCommon.myLen(objCommonVar.CurrentCompanyCode) > 0 Then
    '                For Each filedname As String In CompCode
    '                    Dim qry As String = "update " + filedname + " set comp_code='" + objCommonVar.CurrentCompanyCode + "' where isnull(comp_code,'')=''"
    '                    clsDBFuncationality.ExecuteNonQuery(qry)
    '                Next
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(ex.Message)
    '        End Try
    '    End Sub
    '    '-------------------------End By Monika-------------------------------------


    '#End Region



    '    Public Shared Function glbankquery(ByRef out As String) As String
    '        out = ""
    '        Dim query As String
    '        If objCommonVar.CurrentUserCode = "ADMIN" Then
    '            query = "select BANK_CODE as [Bank Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
    '        Else
    '            Dim arrlocation As New ArrayList()
    '            Dim arraccount As New ArrayList()

    '            arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
    '            arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
    '            Dim countsegment As Integer = arrlocation.Count
    '            Dim countaccount As Integer = arraccount.Count
    '            If countaccount <> 0 Then
    '                If countsegment <> 0 Then
    '                    Dim valuefirstsegment As String = arrlocation(0)
    '                    valuefirstsegment = "%" + valuefirstsegment
    '                    Dim valuefirstacct As String = arraccount(0)
    '                    query = "select BANK_CODE as [Bank Code], DESCRIPTION ,BANKACCNUMBER as [Bank Account No] from TSPL_BANK_MASTER "
    '                    out = " bankacc like '" + valuefirstsegment + "' OR bankacc ='" + valuefirstacct + "'"
    '                    For count As Integer = 0 To countsegment - 1
    '                        If count < countsegment - 1 Then
    '                            Dim value As String = arrlocation(count + 1)
    '                            value = "%" + value
    '                            Dim stcode As String = "or bankacc like'" + value + "'"
    '                            out += stcode
    '                        End If
    '                    Next
    '                    For countacct As Integer = 0 To countaccount
    '                        If countacct < countaccount - 1 Then
    '                            Dim value As String = arraccount(countacct + 1)
    '                            Dim stcode As String = "or bankacc = '" + value + "'"
    '                            out += stcode
    '                        End If
    '                    Next
    '                Else
    '                    query = "select top 0 BANK_CODE as [Bank Code], DESCRIPTION, BANKACCNUMBER as [Bank Account No] from TSPL_BANK_MASTER"

    '                End If
    '            Else
    '                If countsegment <> 0 Then
    '                    Dim valuefirstsegment As String = arrlocation(0)
    '                    valuefirstsegment = "%" + valuefirstsegment
    '                    Dim valuefirstacct As String = String.Empty
    '                    query = "select BANK_CODE as [Bank Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
    '                    out = " bankacc like '" + valuefirstsegment + "' OR bankacc ='" + valuefirstacct + "'"
    '                    For count As Integer = 0 To countsegment - 1
    '                        If count < countsegment - 1 Then
    '                            Dim value As String = arrlocation(count + 1)
    '                            value = "%" + value
    '                            Dim stcode As String = "or bankacc like'" + value + "'"
    '                            out += stcode
    '                        End If
    '                    Next
    '                    For countacct As Integer = 0 To countaccount
    '                        If countacct < countaccount - 1 Then
    '                            Dim value As String = arraccount(countacct + 1)
    '                            Dim stcode As String = "or bankacc = '" + value + "'"
    '                            out += stcode
    '                        End If
    '                    Next
    '                Else
    '                    query = "select top 0 BANK_CODE as [Bank Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER"
    '                End If
    '            End If
    '        End If


    '        Return query
    '    End Function


    Public Shared Function GetConstraint(ByVal strTableName As String, ByVal strColumnName As String) As String
        Dim str As String = "SELECT   dc.name AS DefaultConstraintName " +
" FROM   sys.all_columns c " +
" JOIN sys.tables t ON c.object_id = t.object_id " +
" JOIN sys.schemas s ON t.schema_id = s.schema_id " +
"LEFT JOIN sys.default_constraints dc ON c.default_object_id = dc.object_id LEFT JOIN INFORMATION_SCHEMA.COLUMNS SC ON (SC.TABLE_NAME = t.name AND SC.COLUMN_NAME = c.name) " +
"WHERE  SC.COLUMN_DEFAULT IS NOT NULL and t.name = '" + strTableName + "' and c.name = '" + strColumnName + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
    End Function

    Public Shared Function GetConstraintWorking(ByVal strTableName As String, ByVal strColumnName As String) As String
        Dim str As String = "select * from (" + Environment.NewLine +
        " SELECT f.name AS ForeignKey, OBJECT_NAME(f.parent_object_id) AS TableName," + Environment.NewLine +
        " COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName," + Environment.NewLine +
        " OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName," + Environment.NewLine +
        " COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName" + Environment.NewLine +
        " FROM sys.foreign_keys AS f " + Environment.NewLine +
        " INNER JOIN sys.foreign_key_columns AS fc" + Environment.NewLine +
        " ON f.OBJECT_ID = fc.constraint_object_id " + Environment.NewLine +
        " )xx where TableName='" + strTableName + "' and ColumnName='" + strColumnName + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
    End Function

    Public Shared Function GetAllConstraintWorking(ByVal strTableName As String, ByVal strColumnName As String, ByVal tran As SqlTransaction) As DataTable
        Dim str As String = "SELECT     CCU.CONSTRAINT_NAME, CCU.COLUMN_NAME 
FROM         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC INNER JOIN
                      INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON TC.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG AND 
                      TC.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA AND TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME
WHERE     (TC.TABLE_NAME = '" + strTableName + "') and COLUMN_NAME='" + strColumnName + "'"
        Return clsDBFuncationality.GetDataTable(str, tran)
    End Function


    Public Shared Function GetDefaultConstraintWorking(ByVal strTableName As String, ByVal strColumnName As String) As String
        Dim str As String = "select constraint_name from (" + Environment.NewLine +
        " select   t.[name] as TableName," + Environment.NewLine +
    "col.column_id," + Environment.NewLine +
    "col.[name] as ColumnName," + Environment.NewLine +
    "con.[definition]," + Environment.NewLine +
    "con.[name] as constraint_name" + Environment.NewLine +
"from sys.default_constraints con" + Environment.NewLine +
"    left outer join sys.objects t" + Environment.NewLine +
"        on con.parent_object_id = t.object_id" + Environment.NewLine +
"    left outer join sys.all_columns col" + Environment.NewLine +
"        on con.parent_column_id = col.column_id" + Environment.NewLine +
"        and con.parent_object_id = col.object_id" + Environment.NewLine +
        " )xx where TableName='" + strTableName + "' and ColumnName='" + strColumnName + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
    End Function
    'Public Shared Sub GenerateExcelChart(ByVal dt As DataTable, ByVal EnuChartType As Integer, ByVal Title As String, ByVal LabelColumn As String, ByVal ValuColumn1 As String, Optional ByVal ValuColumn2 As String = "")
    '    Try
    '        Dim excel As New Excel.Application
    '        excel.Visible = True
    '        excel.Workbooks.Add()
    '        excel.Range("A1").Value2 = LabelColumn
    '        excel.Range("B1").Value2 = ValuColumn1
    '        If clsCommon.myLen(ValuColumn2) > 0 Then
    '            excel.Range("C1").Value2 = ValuColumn2
    '        End If

    '        Dim ii As Integer = 2
    '        For Each dr As DataRow In dt.Rows
    '            excel.Range("A" & ii).Value2 = dr(LabelColumn)
    '            excel.Range("B" & ii).Value2 = dr(ValuColumn1)
    '            If clsCommon.myLen(ValuColumn2) > 0 Then
    '                excel.Range("C" & ii).Value2 = dr(ValuColumn2)
    '            End If
    '            ii += 1
    '        Next
    '        Dim range As Excel.Range = excel.Range("A1")
    '        Dim chart As Excel.Chart = excel.ActiveWorkbook.Charts.Add()
    '        chart.ChartWizard(Source:=range.CurrentRegion, Title:=Title)
    '        chart.ChartStyle = 21
    '        chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DBarStacked100
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Public Shared Function myDclInZeroPointFive(ByVal val As Object) As Decimal
        Dim retVal As Decimal = 0.0
        Try
            retVal = Math.Truncate(val * 10) / 10
        Catch ex As Exception
        End Try
        Return retVal
    End Function

    Public Shared Function myFloor(ByVal val As Object, ByVal DecimalPlaces As Integer) As Decimal
        Dim retVal As Decimal = 0.0
        Try
            Dim dclval As Decimal = clsCommon.myCdbl(val)
            Dim intdc As Integer = 1
            For ii As Integer = 1 To DecimalPlaces
                intdc = intdc * 10
            Next
            retVal = Math.Truncate(dclval * intdc) / intdc
        Catch ex As Exception
        End Try
        Return retVal
    End Function



    'Ticket No  TEC/30/07/19-000972 ,Sanjay
    Public Shared Function ShowDeletedData(ByVal FromDate As Date, ByVal ToDate As Date, ByVal WhrCls As String, ByVal DocDateColumn As String, ByVal MasterTable As String, Optional DetailColumn1 As String = Nothing, Optional DetailColumn2 As String = Nothing, Optional DetailColumn3 As String = Nothing, Optional DetailColumn4 As String = Nothing, Optional DetailColumn5 As String = Nothing, Optional DetailColumn6 As String = Nothing, Optional DetailColumn7 As String = Nothing, Optional DetailColumn8 As String = Nothing, Optional DetailColumn9 As String = Nothing) As String
        Dim dt As DataTable = Nothing
        Dim Mainqry As String = ""
        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & MasterTable + clsCommon.DeleteTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow("No Delete Table found")
                Return False
            End If
            Dim strMasterCodeColumn As String = ""
            Dim strMasterCodeColumnAS As String = ""
            Dim dtMasterCategory As DataTable = Nothing

            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT c.name as Name,replace(upper(left(c.name,1)) + upper(substring(c.name,2,len(c.name))),'_',' ') as FinalName "
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id"
            Masteryqry += " WHERE t.name = '" & MasterTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry, Nothing)

            If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strMasterCodeColumn += ","
                    End If
                    strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + " as [" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("FinalName")).Trim() + "]"
                Next
                If clsCommon.myLen(DetailColumn1) > 0 Then
                    If clsCommon.myLen(strMasterCodeColumn) > 0 Then
                        strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn1.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn1.Trim()) + "]"
                    Else
                        strMasterCodeColumn += " '" + clsCommon.myCstr(DetailColumn1.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn1.Trim()) + "]"
                    End If
                End If
                If clsCommon.myLen(DetailColumn2) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn2.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn2.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn3) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn3.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn3.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn4) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn4.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn4.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn5) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn5.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn5.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn6) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn6.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn6.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn7) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn7.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn7.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn8) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn8.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn8.Trim()) + "]"
                End If
                If clsCommon.myLen(DetailColumn9) > 0 Then
                    strMasterCodeColumn += ", '" + clsCommon.myCstr(DetailColumn9.Trim()) + "' as [" + clsCommon.myCstr(DetailColumn9.Trim()) + "]"
                End If
            End If
            '' End
            '' =========Final Binding Main Qry=======
            Mainqry = "  select ROW_NUMBER() OVER(ORDER BY  " & clsCommon.DeleteTableColDeleteOn & "  asc) AS Sno,final.* from "
            Mainqry += " ( "
            Mainqry += " select " & clsCommon.DeleteTableColDeleteBy & "," & clsCommon.DeleteTableColDeleteOn & "," & strMasterCodeColumn & " from " & MasterTable + clsCommon.DeleteTablePostFix & ""
            Mainqry += " where 1=1 "

            If clsCommon.myLen(WhrCls) > 0 Then
                Mainqry += " and " + WhrCls
            End If

            If clsCommon.myLen(DocDateColumn) > 0 Then
                Mainqry += " and " & DocDateColumn & ">=convert(date,'" & FromDate + "',103)   and  convert(date," & DocDateColumn & ",103)<= convert(date,'" & ToDate & "',103)  "
            Else
                Mainqry += " and " & clsCommon.DeleteTableColDeleteOn & ">=convert(date,'" & FromDate + "',103)   and  convert(date," & clsCommon.DeleteTableColDeleteOn & ",103)<= convert(date,'" & ToDate & "',103)  "
            End If

            Mainqry += " )final "
            Mainqry += " where 1=1 "
            Mainqry += " order by " & clsCommon.DeleteTableColDeleteOn & " asc "

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return Mainqry
    End Function
    'Ticket No  TEC/30/07/19-000972 ,Sanjay

    Public Shared Function myReverseString(ByVal value As String) As String
        ' Convert to char array.
        Dim arr() As Char = value.ToCharArray()
        ' Use Array.Reverse function.
        Array.Reverse(arr)
        ' Construct new string.
        Return New String(arr)
    End Function

    'sanjay


    Public Shared Function GetForeignKey_Name(ByVal strTableName As String, ByVal strColumnName As String) As String
        Dim RetValue As String = ""
        Try
            Dim qry As String = "select fk_constraint_name from (" + Environment.NewLine +
"select " + Environment.NewLine +
"fk_tab.name as TabName," + Environment.NewLine +
"schema_name(fk_tab.schema_id) + '.' + fk_tab.name as foreign_table," + Environment.NewLine +
"                '>-' as rel," + Environment.NewLine +
"    schema_name(pk_tab.schema_id) + '.' + pk_tab.name as primary_table," + Environment.NewLine +
"    fk_cols.constraint_column_id as no, " + Environment.NewLine +
"    fk_col.name as fk_column_name," + Environment.NewLine +
"                ' = ' as [join]," + Environment.NewLine +
"    pk_col.name as pk_column_name," + Environment.NewLine +
"    fk.name as fk_constraint_name" + Environment.NewLine +
"from sys.foreign_keys fk" + Environment.NewLine +
"    inner join sys.tables fk_tab" + Environment.NewLine +
"        on fk_tab.object_id = fk.parent_object_id" + Environment.NewLine +
"    inner join sys.tables pk_tab" + Environment.NewLine +
"        on pk_tab.object_id = fk.referenced_object_id" + Environment.NewLine +
"    inner join sys.foreign_key_columns fk_cols" + Environment.NewLine +
"        on fk_cols.constraint_object_id = fk.object_id" + Environment.NewLine +
"    inner join sys.columns fk_col" + Environment.NewLine +
"        on fk_col.column_id = fk_cols.parent_column_id" + Environment.NewLine +
"        and fk_col.object_id = fk_tab.object_id" + Environment.NewLine +
"    inner join sys.columns pk_col" + Environment.NewLine +
"        on pk_col.column_id = fk_cols.referenced_column_id" + Environment.NewLine +
"        and pk_col.object_id = pk_tab.object_id" + Environment.NewLine +
"	)xx where TabName='" + strTableName + "' and fk_column_name='" + strColumnName + "'"

            RetValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Catch ex As Exception
        End Try
        Return RetValue
    End Function

    Public Shared Function DropTableKey(ByVal strTableName As String, ByVal strColumnName As String, ByVal Key As EnumTableKeyType) As Boolean
        Try
            Dim strKey As String = GetTableKey(strTableName, strColumnName, Key)
            If clsCommon.myLen(strKey) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("alter table " + strTableName + " drop constraint " & strKey & "")
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Shared Function GetTableKey(ByVal strTableName As String, ByVal strColumnName As String, ByVal Key As EnumTableKeyType) As String
        Dim RetValue As String = ""
        Try
            Dim qry As String = ""
            If Key = EnumTableKeyType.Identity Then
                qry = "select columnproperty(object_id('" + strTableName + "'),'" + strColumnName + "','IsIdentity')"
            Else
                qry = "SELECT   A.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B  
WHERE A.CONSTRAINT_NAME = B.CONSTRAINT_NAME and a.TABLE_NAME='" + strTableName + "' and b.COLUMN_NAME='" + strColumnName + "' "
                Select Case Key

                    Case EnumTableKeyType.Primary
                        qry += " AND  CONSTRAINT_TYPE = 'PRIMARY KEY' "
                    Case EnumTableKeyType.Foreign
                        qry += " AND  CONSTRAINT_TYPE = 'FOREIGN KEY' "
                    Case EnumTableKeyType.Unique
                        qry += " AND  CONSTRAINT_TYPE = 'UNIQUE' "
                    Case Else
                        Throw New Exception("Wrong Key Type")
                End Select
                RetValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If

        Catch ex As Exception
        End Try
        Return RetValue
    End Function

    Public Shared Function ReportingMailIdandPhone(ByVal user_Code As String, ByRef arrMobileNo As List(Of String), ByVal trans As SqlTransaction) As List(Of String)
        Dim dt As New DataTable()
        Dim StrParent As String = ""
        Dim arrMailID As New List(Of String)
        Dim Sql As String = ""
        Try
            Sql = "With Hierarchy(ChildId, ParentId, Parents)" &
             " AS(SELECT User_Code, Level4_Code, CAST('' AS VARCHAR(MAX))" &
                  " FROM TSPL_USER_MASTER AS FirtGeneration" &
                  " WHERE Level4_Code IS NULL OR Level4_Code='' " &
              " UNION ALL " &
              " SELECT NextGeneration.User_Code, Parent.ChildId," &
              " CAST(CASE WHEN Parent.Parents = ''" &
               " THEN(CAST(''''+NextGeneration.Level4_Code+'''' AS VARCHAR(MAX)))" &
               " ELSE(Parent.Parents + ',''' + CAST(NextGeneration.Level4_Code+'''' AS VARCHAR(MAX)))" &
            " END AS VARCHAR(MAX))" &
               " FROM TSPL_USER_MASTER AS NextGeneration" &
               " INNER JOIN Hierarchy AS Parent ON NextGeneration.Level4_Code = Parent.ChildId  " &
                    ")" &
                    " SELECT Parents FROM Hierarchy WHERE ChildId='" & user_Code & "'" &
                    " OPTION(MAXRECURSION 32767)"
            StrParent = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

            If clsCommon.myLen(StrParent) > 0 Then
                Sql = "SELECT distinct isnull(E_Mail,'') as E_Mail,isnull(Mob_No,'') as Mob_No FROM TSPL_USER_MASTER WHERE user_code in (" + StrParent + ")"
                dt = clsDBFuncationality.GetDataTable(Sql, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For I As Integer = 0 To dt.Rows.Count() - 1
                        If clsCommon.myLen(dt.Rows(I).Item("E_Mail")) > 0 Then
                            arrMailID.Add(clsCommon.myCstr(dt.Rows(I).Item("E_Mail")))
                        End If
                        If clsCommon.myLen(dt.Rows(I).Item("Mob_No")) > 0 Then
                            arrMobileNo.Add(clsCommon.myCstr(dt.Rows(I).Item("Mob_No")))
                        End If
                    Next
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
        Return arrMailID
    End Function

    Public Shared Function GetCustomerEInvoiceType(ByVal strCustomerCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select Case when GST_Registered =1 then 'BB' else 'BC' end as Type   from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustomerCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetVendorEInvoiceType(ByVal strCustomerCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select Case when GSTRegistered =1 then 'BB' else 'BC' end as Type   from TSPL_VENDOR_MASTER where Vendor_Code='" + strCustomerCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetCustomerEInvoiceTypeFromTransationTable(ByVal strTableName As String, ByVal strDocumentColumnName As String, ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select  EInvoice_Type  from " + strTableName + " where " + strDocumentColumnName + "='" + strDocumentNo + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetEInvoiceStatus(ByVal TransactionDate? As Date) As Boolean
        Return GetEInvoiceStatus(TransactionDate, Nothing)
    End Function
    Public Shared Function GetEInvoiceStatus(ByVal TransactionDate? As Date, ByVal trans As SqlTransaction) As Boolean
        Dim strtemp As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, trans))
        Dim EInvoiceStartDate As Date? = Nothing
        If clsCommon.myLen(strtemp) > 0 Then
            EInvoiceStartDate = clsCommon.myCDate(strtemp)
        End If

        If EInvoiceStartDate <= TransactionDate Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetBatchWiseApplicableStatus(ByVal TransactionDate? As Date) As Boolean
        Return GetBatchWiseApplicableStatus(TransactionDate, Nothing)
    End Function
    Public Shared Function GetBatchWiseApplicableStatus(ByVal TransactionDate? As Date, ByVal trans As SqlTransaction) As Boolean
        Dim strtemp As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemBatchWiseStartDate, clsFixedParameterCode.ItemBatchWiseStartDate, trans))
        Dim EInvoiceStartDate As Date? = Nothing
        If clsCommon.myLen(strtemp) > 0 Then
            EInvoiceStartDate = clsCommon.myCDate(strtemp)
        Else
            Return True
        End If

        If EInvoiceStartDate <= TransactionDate Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function CheckAllowToCancelEInvoice(ByVal TransactionDate? As Date) As Boolean
        If clsCommon.GETSERVERDATE() <= TransactionDate.Value.AddHours(24) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetQRCodeStatus(ByVal TransactionDate? As Date) As Boolean
        Return GetQRCodeStatus(TransactionDate, Nothing)
    End Function
    Public Shared Function GetQRCodeStatus(ByVal TransactionDate? As Date, ByVal trans As SqlTransaction) As Boolean
        Dim strtemp As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfDynamicQRCodeForB2CInvoiceImplementation, clsFixedParameterCode.DateOfDynamicQRCodeForB2CInvoiceImplementation, trans))
        Dim QRCodeStartDate As Date? = Nothing
        If clsCommon.myLen(strtemp) > 0 Then
            QRCodeStartDate = clsCommon.myCDate(strtemp)
        End If

        If QRCodeStartDate <= TransactionDate Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Shared Function UpdateCostCenterAndHirerachyCodeOnJE(ByVal strDocumentCode As String, ByVal strSourceCode As String, ByVal strCostCenter As String, ByVal strHirerachyCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + strDocumentCode + "' and Source_Code = '" + strSourceCode + "'", trans))
        If clsCommon.myLen(strVoucherNo) > 0 Then
            If clsCommon.myLen(strCostCenter) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_DETAILS set  Cost_Centre_Code = '" + strCostCenter + "' where  Voucher_No = '" + strVoucherNo + "' ", trans)
            End If
            If clsCommon.myLen(strHirerachyCode) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_DETAILS set Hirerachy_Code = '" + strHirerachyCode + "'   where  Voucher_No = '" + strVoucherNo + "' ", trans)
            End If
        End If
        Return True
    End Function


    Public Shared Sub OpenNotepadFile(ByVal ex As String, ByVal FormName As String)
        Try
            Dim logFile As String = FormName.Replace(" ", "") + ".txt"
            clsCommon.ProgressBarUpdate("Checking for log file...")
            If System.IO.File.Exists(logFile) Then
                Dim stream As New IO.StreamWriter(logFile, False)
                stream.WriteLine("")
                stream.Close()
            Else
                Dim fs As IO.FileStream = System.IO.File.Create(logFile)
                fs.Close()
            End If

            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine(ex)
            objWriter.Close()


            Dim objreader As New System.IO.StringReader(logFile)
            If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
                Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText(logFile))
                If clsCommon.myLen(str) > 0 Then
                    System.Diagnostics.Process.Start(logFile)
                End If
            End If
        Catch ex1 As Exception
        End Try
    End Sub



    Public Shared Function GetReportID(ByVal strForm_ID As String, ByVal StrReportType As String) As String
        Dim StrReportId As String = ""
        Try
            StrReportId = strForm_ID
            StrReportId = StrReportId + StrReportType
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
        Return StrReportId
    End Function

    Public Shared Function bankPermission(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = ""
        Dim strvalue As String = ""
        qry = "select distinct bank_code from TSPL_User_Bank_mapping where Item_Code ='" + objCommonVar.CurrentUserCode + "'"
        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
            For Each dr As DataRow In dtNew.Rows
                strvalue = strvalue + ",'" + clsCommon.myCstr(dr("bank_code")) + "'"
                If strvalue.Substring(0, 1) = "," Then
                    strvalue = strvalue.Substring(1, strvalue.Length - 1)
                End If
            Next
        End If
        Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function

    Public Shared Function glbankqueryNew(ByRef strWhrClas As String) As String
        Dim Bank_Code As String = bankPermission(Nothing)
        strWhrClas = "1=1"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrClas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrClas += " AND TSPL_BANK_MASTER.Bank_Code in ( " + Bank_Code + " )"
            End If
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                strWhrClas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        End If
        Dim query As String
        If objCommonVar.CurrentUserCode = objCommonVar.CurrentUserCode Then
            query = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
        Else
            Dim arrlocation As New ArrayList()
            Dim arraccount As New ArrayList()

            arraccount = connectSql.funglaccountmultiple(objCommonVar.CurrentUserCode)
            arrlocation = connectSql.funglsegmentmultiple(objCommonVar.CurrentUserCode)
            Dim countsegment As Integer = arrlocation.Count
            Dim countaccount As Integer = arraccount.Count
            If countaccount <> 0 Then
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = arraccount(0)
                    query = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
                    strWhrClas = " Substring(BANKACC , (LEN(BANKACC)-2), 3) IN (" + clsCommon.GetMulcallString(arrlocation) + ") OR bankacc IN (" + clsCommon.GetMulcallString(arraccount) + ")"
                    If Bank_Code <> "" Then
                        strWhrClas = " and TSPL_BANK_MASTER.bank_code in ( " + Bank_Code + " )"
                    End If
                Else
                    query = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER"
                    If Bank_Code <> "" Then
                        strWhrClas = " TSPL_BANK_MASTER.bank_code in ( " + Bank_Code + " )"
                    End If
                End If
            Else
                If countsegment <> 0 Then
                    Dim valuefirstsegment As String = arrlocation(0)
                    valuefirstsegment = "%" + valuefirstsegment
                    Dim valuefirstacct As String = String.Empty
                    query = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
                    strWhrClas = " Substring(BANKACC , (LEN(BANKACC)-2), 3) IN (" + clsCommon.GetMulcallString(arrlocation) + ") OR bankacc IN (" + clsCommon.GetMulcallString(arraccount) + ")"
                    If Bank_Code <> "" Then
                        strWhrClas = " TSPL_BANK_MASTER.bank_code in ( " + Bank_Code + " )"
                    End If
                Else
                    query = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER"
                    If Bank_Code <> "" Then
                        strWhrClas = " TSPL_BANK_MASTER.bank_code in ( " + Bank_Code + " )"
                    End If
                End If
            End If
        End If

        Return query
        ''''Code Ends Here
    End Function
End Class
