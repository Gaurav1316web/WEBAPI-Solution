Imports System.Data.SqlClient
Public Class clsJournalMaster
    Private Shared Function GetMergedAccCode(ByVal StrAccCode As ArrayList, ByVal SrcType As String, ByVal trans As SqlTransaction) As List(Of clsJournalDetailTemp)
        Dim ArrReturn As List(Of clsJournalDetailTemp) = Nothing
        Dim arrLocSeg As New List(Of String)
        If StrAccCode IsNot Nothing AndAlso StrAccCode.Count > 0 Then
            Dim dtSourceCode As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_SOURCECODE where Do_Not_Merge=1 and SourceCode='" + SrcType + "'", trans)
            ArrReturn = New List(Of clsJournalDetailTemp)

            For Each Str() As String In StrAccCode
                Dim strCode As String = clsCommon.myCstr(Str(0))
                Dim Amount As Double = Math.Round(clsCommon.myCdbl(Str(1)), 2, MidpointRounding.ToEven)
                Dim strDesc As String = ""
                Dim strRef As String = ""
                Dim strHierarchyCode As String = ""
                Dim strCostCenterCode As String = ""
                Dim strHierarchyCode3 As String = ""
                Dim strHierarchyCode4 As String = ""
                Dim strRecoControlAccount As String = ""

                If Str.Length = 3 Then
                    strDesc = Convert.ToString(Str(2))
                ElseIf Str.Length = 4 Then
                    strDesc = Convert.ToString(Str(2))
                    strRef = Convert.ToString(Str(3))
                ElseIf Str.Length = 6 Then
                    strDesc = Convert.ToString(Str(2))
                    strRef = Convert.ToString(Str(3))
                    strHierarchyCode = Convert.ToString(Str(4))
                    strCostCenterCode = Convert.ToString(Str(5))
                ElseIf Str.Length = 8 Then
                    strDesc = Convert.ToString(Str(2))
                    strRef = Convert.ToString(Str(3))
                    strHierarchyCode = Convert.ToString(Str(4))
                    strCostCenterCode = Convert.ToString(Str(5))
                    strHierarchyCode3 = Convert.ToString(Str(6))
                    strHierarchyCode4 = Convert.ToString(Str(7))
                ElseIf Str.Length = 9 Then
                    strDesc = Convert.ToString(Str(2))
                    strRef = Convert.ToString(Str(3))
                    strHierarchyCode = Convert.ToString(Str(4))
                    strCostCenterCode = Convert.ToString(Str(5))
                    strHierarchyCode3 = Convert.ToString(Str(6))
                    strHierarchyCode4 = Convert.ToString(Str(7))
                    strRecoControlAccount = Convert.ToString(Str(8))
                End If
                Dim isFound As Boolean = False
                Dim segCode As String = strCode.Substring(clsCommon.myLen(strCode) - 3, 3)
                If Not arrLocSeg.Contains(segCode) Then
                    arrLocSeg.Add(segCode)
                End If

                If dtSourceCode Is Nothing OrElse dtSourceCode.Rows.Count <= 0 Then
                    If ArrReturn IsNot Nothing AndAlso ArrReturn.Count > 0 Then
                        For ii As Integer = 0 To ArrReturn.Count - 1
                            If clsCommon.CompairString(ArrReturn(ii).Account_code, strCode) = CompairStringResult.Equal And Not (clsCommon.CompairString(SrcType, "VC-GL") = CompairStringResult.Equal) Then
                                If clsCommon.CompairString(ArrReturn(ii).Hierarchy_Code, strHierarchyCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ArrReturn(ii).Cost_Center_Code, strCostCenterCode) = CompairStringResult.Equal Then
                                    isFound = True
                                    ArrReturn(ii).Amount += Amount

                                    If clsCommon.myLen(ArrReturn(ii).Description) > 0 Then
                                        ArrReturn(ii).Description += ", "
                                    End If
                                    ArrReturn(ii).Description += strDesc
                                    If clsCommon.myLen(strHierarchyCode) > 0 Then
                                        ArrReturn(ii).Hierarchy_Code = strHierarchyCode
                                    End If
                                    If clsCommon.myLen(strCostCenterCode) > 0 Then
                                        ArrReturn(ii).Cost_Center_Code = strCostCenterCode
                                    End If

                                    If clsCommon.myLen(ArrReturn(ii).Reference) > 0 Then
                                        ArrReturn(ii).Reference += ", "
                                    End If
                                    ArrReturn(ii).Reference += strRef
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If
                If Not isFound Then
                    Dim obj As clsJournalDetailTemp = New clsJournalDetailTemp()
                    obj.Account_code = strCode
                    obj.Amount = Amount
                    obj.Description = strDesc
                    obj.Reference = strRef
                    obj.Hierarchy_Code = strHierarchyCode
                    obj.Cost_Center_Code = strCostCenterCode
                    obj.Hirerachy_Code3 = strHierarchyCode3
                    obj.Hirerachy_Code4 = strHierarchyCode4
                    obj.Reco_Control_Account = strRecoControlAccount
                    ArrReturn.Add(obj)
                End If
            Next

            For Each Str As String In arrLocSeg
                Dim dblTotDrAmt As Decimal = 0
                Dim dblTotCrAmt As Decimal = 0
                Dim firstAccountindex As Integer = -1

                For ii As Integer = 0 To ArrReturn.Count - 1
                    Dim segCode As String = ArrReturn(ii).Account_code.Substring(clsCommon.myLen(ArrReturn(ii).Account_code) - 3, 3)
                    If clsCommon.CompairString(segCode, Str) = CompairStringResult.Equal Then
                        If firstAccountindex < 0 Then
                            firstAccountindex = ii
                        End If
                        If ArrReturn(ii).Amount > 0 Then
                            dblTotDrAmt += Math.Round(clsCommon.myCdbl(ArrReturn(ii).Amount), 2, MidpointRounding.ToEven)
                        Else
                            dblTotCrAmt += -1 * Math.Round(clsCommon.myCdbl(ArrReturn(ii).Amount), 2, MidpointRounding.ToEven)
                        End If
                    End If
                Next
                Dim dblDiffence As Double = dblTotDrAmt - dblTotCrAmt
                dblDiffence = Math.Round(dblDiffence, 2, MidpointRounding.ToEven)
                If Math.Abs(dblDiffence) <= 0.99 Then
                    If clsCommon.CompairString(SrcType, "AR-IN") = CompairStringResult.Equal Then
                        ArrReturn(ArrReturn.Count - 1).Amount = ArrReturn(ArrReturn.Count - 1).Amount - dblDiffence ''Working for all four conditions.
                    Else
                        ArrReturn(firstAccountindex).Amount = ArrReturn(firstAccountindex).Amount - dblDiffence ''Working for all four conditions.
                    End If

                End If
            Next


        End If
        Return ArrReturn
    End Function

    Public Shared Function FunGrnlEntryWithTrans(ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Return FunGrnlEntryWithTrans(strLocationCode, isLocationCodeisSegment, "", trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
    End Function
    Public Shared Function FunGrnlEntryWithTrans(ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Return FunGrnlEntryWithTrans(strLocationCode, isLocationCodeisSegment, False, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
    End Function
    Public Shared Function FunGrnlEntryWithTrans(ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isForUnpostedTransaction As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Return FunGrnlEntryWithTrans("", "N", strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
    End Function

    Public Shared Function FunGrnlEntryWithTrans(ByVal strPrefixTransType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isForUnpostedTransaction As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Return FunGrnlEntryWithTrans(0, strPrefixTransType, strTransType, strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
    End Function

    Public Shared Function FunGrnlEntryWithTrans(ByVal intIND_AS As Integer, ByVal strPrefixTransType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isForUnpostedTransaction As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Return FunGrnlEntryWithTrans(False, intIND_AS, strPrefixTransType, strTransType, strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
    End Function
    Public Shared Function FunGrnlEntryWithTrans(ByVal SettCreateOpeningEntryAutomatically As Boolean, ByVal intIND_AS As Integer, ByVal strPrefixTransType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isForUnpostedTransaction As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        Dim JEWithOPTables As Boolean = False
        If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
            If clsCommon.CompairString(SrcType, "GL-JE") = CompairStringResult.Equal Then
                If dt <= dtERPStartDate Then
                    JEWithOPTables = SettCreateOpeningEntryAutomatically
                End If
            Else
                If dt <= dtERPStartDate Then
                    JEWithOPTables = True
                End If
            End If
        Else
            Throw New Exception("Please set ERP Start Date")
        End If
        If JEWithOPTables Then
            If SettCreateOpeningEntryAutomatically Then
                JEWithOPTables = False
                JEMainFunction(intIND_AS, "TSPL_JOURNAL_MASTER", "TSPL_JOURNAL_DETAILS", "sp_TSPL_JOURNAL_MASTER_INSERT", "sp_TSPL_JOURNAL_DETAILS_INSERT", JEWithOPTables, strPrefixTransType, strTransType, strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
                JEWithOPTables = True
            End If
            strVourcherNoForRecreateOnly = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where Source_Doc_No='" + SrcDocNo + "'", trans))
            JEMainFunction(intIND_AS, "TSPL_JOURNAL_MASTER_OP", "TSPL_JOURNAL_DETAILS_OP", "sp_TSPL_JOURNAL_MASTER_OP_INSERT", "sp_TSPL_JOURNAL_DETAILS_OP_INSERT", JEWithOPTables, strPrefixTransType, strTransType, strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
        Else
            JEMainFunction(intIND_AS, "TSPL_JOURNAL_MASTER", "TSPL_JOURNAL_DETAILS", "sp_TSPL_JOURNAL_MASTER_INSERT", "sp_TSPL_JOURNAL_DETAILS_INSERT", JEWithOPTables, strPrefixTransType, strTransType, strLocationCode, isLocationCodeisSegment, isForUnpostedTransaction, strVourcherNoForRecreateOnly, trans, dt, EntryDesc, SrcType, SrcTypeDesc, SrcDocNo, SrcDocDesc, strSrcType, strSrcTypeCode, strSrcTypeDesc, User, CompCode, StrAccCode, narration, strremarks, strReference, coll, objJE)
        End If
        Return True
    End Function

    Private Shared Function JEMainFunction(ByVal intIND_AS As Integer, ByVal strJEHead As String, ByVal strJEDetail As String, ByVal strJEHeadStoreProcudureName As String, ByVal strJEDetailStoreProcudureName As String, ByVal JEWithOPTables As Boolean, ByVal strPrefixTransType As String, ByVal strTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isForUnpostedTransaction As Boolean, ByVal strVourcherNoForRecreateOnly As String, ByVal trans As SqlTransaction, ByVal dt As Date, ByVal EntryDesc As String, ByVal SrcType As String, ByVal SrcTypeDesc As String, ByVal SrcDocNo As String, ByVal SrcDocDesc As String, ByVal strSrcType As String, ByVal strSrcTypeCode As String, ByVal strSrcTypeDesc As String, ByVal User As String, ByVal CompCode As String, ByVal StrAccCode As ArrayList, Optional ByVal narration As String = vbNullString, Optional ByVal strremarks As String = Nothing, Optional ByVal strReference As String = Nothing, Optional ByVal coll As Hashtable = Nothing, Optional ByVal objJE As clsJEExtraColumns = Nothing) As Boolean
        If objCommonVar.StopJournalEntry Then
            Return True
        End If
        Dim dblTotal As Double = 0
        Dim StrTransTypeforHead As String = Nothing
        Dim arr As List(Of clsJournalDetailTemp) = GetMergedAccCode(StrAccCode, SrcType, trans)
        Dim StrVoucher As String = ""
        Dim Sql As String = ""
        Dim EntryDate As String = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
        Dim settLockDate As String = clsFixedParameter.GetData(clsFixedParameterType.LockDate, clsFixedParameterCode.LockDate, trans)
        If clsCommon.myLen(settLockDate) > 0 Then
            If clsCommon.GetDateWithStartTime(dt) < clsCommon.GetDateWithStartTime(clsCommon.myCDate(settLockDate)) Then
                Throw New Exception("Can not create Financial transaction before Lock Date [" + settLockDate + "]")
            End If
        End If

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objTotal As clsJournalDetailTemp In arr
                If objTotal.Amount > 0 Then
                    dblTotal += objTotal.Amount
                End If
            Next
            If dblTotal > 0 Then
                If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    Dim qry1 As String = "delete from " + strJEDetail + " where Voucher_No='" + strVourcherNoForRecreateOnly + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                    qry1 = "delete from " + strJEHead + " where Voucher_No='" + strVourcherNoForRecreateOnly + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                    StrVoucher = strVourcherNoForRecreateOnly
                Else
                    Dim strLocalPrefixTransType As String = clsDocTransactionType.JournalEntryOther
                    If clsCommon.CompairString(SrcType, "MI-SR") = CompairStringResult.Equal Then
                        strLocalPrefixTransType = clsDocTransactionType.JournalEntryMilkSRN
                        StrVoucher = fnAutoGenerateNo(JEWithOPTables, trans, dt, strLocalPrefixTransType, strLocationCode, isLocationCodeisSegment, objCommonVar.ShowMCCFinderInPaymentProcess)
                    ElseIf clsCommon.CompairString(SrcType, "PR-EN") = CompairStringResult.Equal Then
                        If objCommonVar.ShowMCCFinderInPaymentProcess AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Prog_Code from tspl_provision_entry where Doc_No='" + SrcDocNo + "'", trans)), clsUserMgtCode.frmMilkShiftEndMCC) = CompairStringResult.Equal Then
                            strLocalPrefixTransType = clsDocTransactionType.JournalEntryMilkSRN
                            StrVoucher = fnAutoGenerateNo(JEWithOPTables, trans, dt, strLocalPrefixTransType, strLocationCode, isLocationCodeisSegment, objCommonVar.ShowMCCFinderInPaymentProcess)
                        Else
                            If clsCommon.myLen(strPrefixTransType) > 0 Then
                                strLocalPrefixTransType = strPrefixTransType
                            End If
                            StrVoucher = fnAutoGenerateNo(JEWithOPTables, trans, dt, strLocalPrefixTransType, strLocationCode, isLocationCodeisSegment)
                        End If
                    Else
                        If clsCommon.myLen(strPrefixTransType) > 0 Then
                            strLocalPrefixTransType = strPrefixTransType
                        End If
                        StrVoucher = fnAutoGenerateNo(JEWithOPTables, trans, dt, strLocalPrefixTransType, strLocationCode, isLocationCodeisSegment)
                    End If
                End If
                Dim strJrnl As String = "select max(journal_no) from " + strJEHead + " "
                Dim Jrnl As String = clsCommon.myCstr(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(strJrnl, trans)) + 1)
                If strReference = Nothing Then
                    strReference = ""
                End If
                If strremarks = Nothing Then
                    strremarks = ""
                End If
                Dim SrcTypeFlag As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SourceCode  from TSPL_GL_SOURCECODE where SourceCode='" + SrcType + "'", trans))
                If SrcTypeFlag = "" OrElse SrcTypeFlag = Nothing Then
                    clsDBFuncationality.ExecuteNonQuery("insert into 	TSPL_GL_SOURCECODE values ('" & SrcType & "',left('" & SrcType & "',2),right('" & SrcType & "',2),'" & SrcTypeDesc & "', " &
                    "'" & User & "','" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "','" & User & "', " &
                    "'" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "','" & CompCode & "','',0)", trans)
                End If

                clsDBFuncationality.SaveAStorePorcedure(trans, strJEHeadStoreProcudureName, New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", EntryDate), New SqlParameter("@Source_Code", SrcType), New SqlParameter("@Source_Desc", SrcTypeDesc), New SqlParameter("@Source_Doc_No", SrcDocNo), New SqlParameter("@Source_Doc_Date", EntryDate), New SqlParameter("@Posting_Date", EntryDate), New SqlParameter("@Voucher_Desc", EntryDesc), New SqlParameter("@Source_Narration", SrcDocDesc), New SqlParameter("@Remarks", strremarks), New SqlParameter("@Comments", strReference), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", EntryDate), New SqlParameter("@Source_Type", strSrcType), New SqlParameter("@CustVend_Code", strSrcTypeCode), New SqlParameter("@CustVend_Name", strSrcTypeDesc), New SqlParameter("@Transaction_Type", strTransType), New SqlParameter("@Total_Debit_Amt", 0.0), New SqlParameter("@Total_Credit_Amt", 0.0), New SqlParameter("@Created_By", User), New SqlParameter("@Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@Modify_By", User), New SqlParameter("@Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@Comp_Code", CompCode))

                Dim qry As String = "Update " + strJEHead + " set "
                If isLocationCodeisSegment Then
                    qry += "Segment_code= '" + strLocationCode + "'"
                Else
                    qry += "Segment_code= (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocationCode + "')"
                End If
                qry += ",IND_AS='" + clsCommon.myCstr(intIND_AS) + "' where Voucher_No='" + StrVoucher + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim strJrnl1 As String = "select journal_no from " + strJEHead + " where Voucher_No='" + StrVoucher + "'"
                Dim Jrnl1 As String
                Jrnl1 = clsDBFuncationality.getSingleValue(strJrnl1, trans)
                Dim AccountCode As String = ""
                Dim i As Integer = 1
                For Each obj As clsJournalDetailTemp In arr
                    Dim Query As String = "Select Description  from TSPL_GL_ACCOUNTS where Account_Code ='" + obj.Account_code + "' "
                    Dim strAccDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Query, trans))
                    If clsCommon.myLen(strAccDesc) = 0 Then
                        Throw New Exception("'" + obj.Account_code + "' Account does not exist.")
                        Return False
                    End If
                    ''richa 03/03/2015
                    Dim Amt As Double = clsCommon.myCdbl(obj.Amount)
                    ''------------------
                    Dim strQ1 As String = " SELECT Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " &
                          " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," &
                          " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " &
                          " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + obj.Account_code + "'"
                    Dim AccType As String = ""
                    Dim AccGrp As String = ""
                    Dim SegC1 As String = ""
                    Dim SegDesc1 As String = ""
                    Dim SegC2 As String = ""
                    Dim SegDesc2 As String = ""
                    Dim SegC3 As String = ""
                    Dim SegDesc3 As String = ""
                    Dim SegC4 As String = ""
                    Dim SegDesc4 As String = ""
                    Dim SegC5 As String = ""
                    Dim SegDesc5 As String = ""
                    Dim SegC6 As String = ""
                    Dim SegDesc6 As String = ""
                    Dim SegC7 As String = ""
                    Dim SegDesc7 As String = ""
                    Dim SegC8 As String = ""
                    Dim SegDesc8 As String = ""
                    Dim SegC9 As String = ""
                    Dim SegDesc9 As String = ""
                    Dim SegC10 As String = ""
                    Dim SegDesc10 As String = ""
                    Dim myreader As DataTable = clsDBFuncationality.GetDataTable(strQ1, trans)
                    If myreader IsNot Nothing AndAlso myreader.Rows.Count > 0 Then
                        AccType = myreader.Rows(0)(0).ToString()
                        AccGrp = myreader.Rows(0)(1).ToString()

                        SegC1 = myreader.Rows(0)(2).ToString()
                        SegDesc1 = myreader.Rows(0)(3).ToString()

                        SegC2 = myreader.Rows(0)(4).ToString()
                        SegDesc2 = myreader.Rows(0)(5).ToString()

                        SegC3 = myreader.Rows(0)(6).ToString()
                        SegDesc3 = myreader.Rows(0)(7).ToString()

                        SegC4 = myreader.Rows(0)(8).ToString()
                        SegDesc4 = myreader.Rows(0)(9).ToString()

                        SegC5 = myreader.Rows(0)(10).ToString()
                        SegDesc5 = myreader.Rows(0)(11).ToString()

                        SegC6 = myreader.Rows(0)(12).ToString()
                        SegDesc6 = myreader.Rows(0)(13).ToString()

                        SegC7 = myreader.Rows(0)(14).ToString()
                        SegDesc7 = myreader.Rows(0)(15).ToString()

                        SegC8 = myreader.Rows(0)(16).ToString()
                        SegDesc8 = myreader.Rows(0)(17).ToString()

                        SegC9 = myreader.Rows(0)(18).ToString()
                        SegDesc9 = myreader.Rows(0)(19).ToString()

                        SegC10 = myreader.Rows(0)(20).ToString()
                        SegDesc10 = myreader.Rows(0)(21).ToString()

                    End If
                    If Not (clsCommon.myCdbl(Amt) = 0) Then
                        clsDBFuncationality.SaveAStorePorcedure(trans, strJEDetailStoreProcudureName, New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", EntryDate), New SqlParameter("@Detail_Line_No", i), New SqlParameter("@Account_code", obj.Account_code), New SqlParameter("@Account_Desc", strAccDesc), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", obj.Description), New SqlParameter("@Reference", obj.Reference), New SqlParameter("@Posting_Date", EntryDate), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))
                        If clsCommon.myLen(obj.Hierarchy_Code) > 0 OrElse clsCommon.myLen(obj.Cost_Center_Code) > 0 OrElse clsCommon.myLen(obj.Hirerachy_Code3) OrElse clsCommon.myLen(obj.Hirerachy_Code4) Then
                            If clsCommon.CompairString(strJEDetail, "TSPL_JOURNAL_DETAILS") = CompairStringResult.Equal Then
                                Sql = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + obj.Hierarchy_Code + "',Cost_Centre_Code='" + obj.Cost_Center_Code + "',Hirerachy_Code3= " + IIf(clsCommon.myLen(obj.Hirerachy_Code3) > 0, " '" & obj.Hirerachy_Code3 & "' ", "NULL") + ",Hirerachy_Code4=" + IIf(clsCommon.myLen(obj.Hirerachy_Code4) > 0, " '" & obj.Hirerachy_Code4 & "' ", "NULL") + " WHERE Voucher_No='" + StrVoucher + "' and Detail_Line_No='" + clsCommon.myCstr(i) + "' "
                                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                            End If
                        End If
                        If clsCommon.myLen(obj.Reco_Control_Account) Then
                            If clsCommon.CompairString(strJEDetail, "TSPL_JOURNAL_DETAILS") = CompairStringResult.Equal Then
                                Sql = "update TSPL_JOURNAL_DETAILS SET Reco_Control_Account='" & obj.Reco_Control_Account & "' WHERE Voucher_No='" + StrVoucher + "' and Detail_Line_No='" + clsCommon.myCstr(i) + "' "
                                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                            End If
                        End If
                        i = i + 1
                    End If
                Next


                '' multicurrency done for conversion value in base currency 15/04/2015 (Monika)
                If Not coll Is Nothing AndAlso coll.Count > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "" + strJEHead + "", OMInsertOrUpdate.Update, "" + strJEHead + ".Voucher_No='" + StrVoucher + "'", trans)
                    Dim cnvrsnrate As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select ConvRate from " + strJEHead + " where Voucher_No='" + StrVoucher + "'", trans))
                    Dim strsource As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Source_Code from " + strJEHead + " where Voucher_No='" + StrVoucher + "'", trans))
                    If cnvrsnrate > 0 AndAlso (clsCommon.CompairString(strsource, "AP-PY") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-MI") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-MR") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-RF") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-OA") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-PY") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-PI") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AR-DC") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AP-IN") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AP-CN") <> CompairStringResult.Equal And clsCommon.CompairString(strsource, "AP-DN") <> CompairStringResult.Equal) Then
                        Sql = "update " + strJEDetail + " SET amount=(amount * " + clsCommon.myCstr(cnvrsnrate) + ") WHERE Voucher_No='" + StrVoucher + "' "
                        clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                    End If
                End If
                '' end multicurrency


                UpdateRecoControlAccount(SrcType, StrVoucher, SrcDocNo, strJEDetail, trans, strSrcTypeCode, strSrcType)

                ''RICHA 18/5/18 UDL/16/05/18-000167

                If clsCommon.CompairString(SrcType, "AR-IN") = CompairStringResult.Equal Then
                    StrTransTypeforHead = "Invoice AR"
                ElseIf clsCommon.CompairString(SrcType, "AR-DN") = CompairStringResult.Equal Then
                    StrTransTypeforHead = "DebitNote AR"
                ElseIf clsCommon.CompairString(SrcType, "AR-CR") = CompairStringResult.Equal Then
                    StrTransTypeforHead = "CreditNote AR"
                Else
                    ''richa agarwal 10 Ovt,2019 to update type against receipt entry into Journal Master table
                    Sql = " select tspl_receipt_header.Receipt_Type,tspl_receipt_header.Receipt_No ,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_BANK_MASTER .Bank_type   from tspl_receipt_header left outer join tspl_journal_master on tspl_receipt_header.Receipt_No = tspl_journal_master.Source_Doc_No" &
                    " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code " &
                    " where tspl_receipt_header.Receipt_No ='" & clsCommon.myCstr(SrcDocNo) & "'  "
                    Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                    If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "R") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Receipt bank"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Receipt Settlement"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") <> CompairStringResult.Equal Then
                                StrTransTypeforHead = "Receipt Cash"
                            End If

                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "P") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Advance bank Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Advance Settlement Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") <> CompairStringResult.Equal Then
                                StrTransTypeforHead = "Advance Cash Receipt"
                            End If

                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "U") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "UnApplied bank Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "UnApplied Settlement Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") <> CompairStringResult.Equal Then
                                StrTransTypeforHead = "UnApplied Cash Receipt"
                            End If


                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "M") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Miscellaneous bank Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "Miscellaneous Settlement Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") <> CompairStringResult.Equal Then
                                StrTransTypeforHead = "Miscellaneous Cash Receipt"
                            End If

                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "O") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "OnAccount bank Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") = CompairStringResult.Equal Then
                                StrTransTypeforHead = "OnAccount Settlement Receipt"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Bank_type")), "C") <> CompairStringResult.Equal Then
                                StrTransTypeforHead = "OnAccount Cash Receipt"
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtReceipt.Rows(0)("Receipt_Type")), "A") = CompairStringResult.Equal Then
                            StrTransTypeforHead = "Apply Document Receipt"
                        End If

                    End If
                    ''-------------end of to update type against receipt entry into Journal Master table
                End If


                If clsCommon.myLen(StrTransTypeforHead) > 0 Then
                    Sql = "update " + strJEHead + " SET Type='" & StrTransTypeforHead & "' WHERE Voucher_No='" + StrVoucher + "' "
                    clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                End If


                Sql = "update " + strJEHead + " SET Total_Credit_Amt=-1*(select ISNULL(sum(amount* case when Amount >0 then 0 else 1 end),0) as CreditAmt from " + strJEDetail + " where Voucher_No='" + StrVoucher + "') ,Total_Debit_Amt=(select ISNULL(sum(amount* case when Amount >0 then 1 else 0 end),0) as DebitAmt from " + strJEDetail + " where Voucher_No='" + StrVoucher + "') WHERE Voucher_No='" + StrVoucher + "' "
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)

                If objJE IsNot Nothing Then
                    If clsCommon.myLen(objJE.TapalNo) > 0 Or clsCommon.myLen(objJE.DateAndTime) > 0 Or clsCommon.myLen(objJE.VSP_CODE) > 0 Then
                        clsJEExtraColumns.SaveData(objJE, trans, StrVoucher)
                    End If
                End If

                Sql = "select sum(amount) from " + strJEDetail + " where voucher_no='" + StrVoucher + "'"
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sql, trans)) = 0 Then
                    If Not isForUnpostedTransaction Then
                        Try
                            Sql = "update " + strJEHead + " SET Authorized = 'A' WHERE Voucher_No='" + StrVoucher + "' "
                            clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                            'Dim objSendToTally As New clsSendToTally()
                            'objSendToTally.SendToTally_JournalEntry(StrVoucher, trans) Comment by balwinder on 12/03/2024
                        Catch ex As Exception
                            If clsCommon.CompairString(ex.Message, "Location Wise Debit is not Equal To Credit.Voucher Cannot Be Posted.") = CompairStringResult.Equal Then
                                Throw New Exception(ex.Message + Environment.NewLine + GetJounalEntryException(strJEDetail, StrVoucher, trans))
                            Else
                                Throw New Exception(ex.Message)
                            End If
                        End Try

                    End If
                Else
                    Throw New Exception(GetJounalEntryException(strJEDetail, StrVoucher, trans))
                End If

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PopupJE, clsFixedParameterCode.PopupJE, trans)) = 1 Then
                    Throw New Exception(GetJounalEntryException(strJEDetail, StrVoucher, trans))
                End If
            Else
                Return False
            End If
            If Not clsCommon.CompairString(strTransType, "X") = CompairStringResult.Equal Then
                Dim qry As String = "select is_End_Year_Proceed from TSPL_Fiscal_Year_Master where convert(date, '" + EntryDate + "',103)>= convert(date, Start_Date,103) and convert(date, '" + EntryDate + "',103)<=CONVERT(date, End_Date,103)"
                Dim dtable As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtable Is Nothing OrElse dtable.Rows.Count <= 0 Then
                    Throw New Exception("Please create financial year which contains " + EntryDate)
                End If
                clsGLAccount.CheckYearEndAccountFilledInSegment(StrVoucher, trans)
                If clsCommon.myCdbl(dtable.Rows(0)("is_End_Year_Proceed")) = 1 Then
                    ''richa agarwal changes done against ticket no.BM00000009404 on 4Aug,2016
                    CreateJEForEndYear(StrVoucher, EntryDate, trans)
                    '------------
                End If
            End If
        End If
        If Not objCommonVar.NoOfJournalEnteryLicence = -1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) from " + strJEHead + "", trans)) > objCommonVar.NoOfJournalEnteryLicence Then
                Throw New Exception("Please ask your administrator to purchase licence" + Environment.NewLine + objCommonVar.LicenceMessageContactPersion)
            End If
        End If

        Return True
    End Function

    Private Shared Function UpdateRecoControlAccount(ByVal SrcType As String, ByVal strVoucherNo As String, ByVal SrcDocNo As String, ByVal strJEDetail As String, ByVal trans As SqlTransaction, ByVal strVendorCustomerCode As String, ByVal strSrcType As String)
        Dim strRecoControlAccount As String = Nothing
        Dim strControlAccountSeg1 As String = Nothing
        Dim dt As DataTable
        Select Case SrcType
            Case "AP-CN", "AP-IN", "AP-DN"
                strRecoControlAccount = "V"
                strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Seg_Code1 from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC where Document_No='" + SrcDocNo + "'", trans), "Account_Seg_Code1")
            Case "AP-PY", "AP-AD"
                strRecoControlAccount = "V"
                strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable(GetVendorQuery(strVendorCustomerCode), trans), "Account_Seg_Code1")
            Case "RV-TA"
                If clsCommon.CompairString(strSrcType, "C") = CompairStringResult.Equal Then
                    strRecoControlAccount = "C"
                    strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable(GetCustomerQuery(strVendorCustomerCode), trans), "Account_Seg_Code1")
                ElseIf clsCommon.CompairString(strSrcType, "V") = CompairStringResult.Equal Then
                    strRecoControlAccount = "V"
                    strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable(GetVendorQuery(strVendorCustomerCode), trans), "Account_Seg_Code1")
                End If
            Case "AR-CR", "AR-IN", "AR-DN"
                strRecoControlAccount = "C"
                '==================Added by preeti Gupta Against Ticket No[ADV/08/05/18-000029]
                strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Seg_Code1  from TSPL_Customer_Invoice_Head  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_Customer_Invoice_Head.Customer_Control_AC where Document_No='" + SrcDocNo + "'", trans), "Account_Seg_Code1")
            Case "AR-DC", "AR-AD", "AR-OA", "AR-PI", "AR-PY", "AR-RF"
                strRecoControlAccount = "C"
                strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable(GetCustomerQuery(strVendorCustomerCode), trans), "Account_Seg_Code1")
        End Select
        If clsCommon.myLen(strRecoControlAccount) > 0 AndAlso clsCommon.myLen(strControlAccountSeg1) > 0 Then
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Reco_Control_Account", strRecoControlAccount)
            clsCommonFunctionality.UpdateDataTable(coll, strJEDetail, OMInsertOrUpdate.Update, "Voucher_No='" + strVoucherNo + "' and Account_Seg_Code1 in (" + strControlAccountSeg1 + ")", trans)
        End If
        Dim isApplyPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)
        Dim AgainstPurchaseReturn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No from TSPL_JOURNAL_MASTER left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_JOURNAL_MASTER.source_Doc_No where Voucher_No ='" + strVoucherNo + "'", trans))
        If isApplyPurchaseAccounting Then
            Select Case SrcType
                Case "AP-CN", "AP-IN", "AP-DN"
                    strRecoControlAccount = "P"
                    strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable("select  Account_Seg_Code1 from TSPL_VENDOR_INVOICE_DETAIL inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where Document_No='" + SrcDocNo + "'", trans), "Account_Seg_Code1")
            End Select
            If clsCommon.myLen(strRecoControlAccount) > 0 AndAlso clsCommon.myLen(strControlAccountSeg1) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Reco_Control_Account", strRecoControlAccount)
                clsCommonFunctionality.UpdateDataTable(coll, strJEDetail, OMInsertOrUpdate.Update, "Voucher_No='" + strVoucherNo + "' and Account_Seg_Code1 in (" + strControlAccountSeg1 + ")", trans)
            End If
        Else
            '==========Added by preeti Gupta 14/12/2018[BHA/27/11/18-000727]
            If clsCommon.myLen(AgainstPurchaseReturn) > 0 Then
                Select Case SrcType
                    Case "AP-CN", "AP-IN", "AP-DN"
                        strRecoControlAccount = "I"
                        strControlAccountSeg1 = clsCommon.GetMulcallString(clsDBFuncationality.GetDataTable("select  Account_Seg_Code1 from TSPL_VENDOR_INVOICE_DETAIL inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  where Document_No='" + SrcDocNo + "'", trans), "Account_Seg_Code1")
                End Select
                If clsCommon.myLen(strRecoControlAccount) > 0 AndAlso clsCommon.myLen(strControlAccountSeg1) > 0 Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Reco_Control_Account", strRecoControlAccount)
                    clsCommonFunctionality.UpdateDataTable(coll, strJEDetail, OMInsertOrUpdate.Update, "Voucher_No='" + strVoucherNo + "' and Account_Seg_Code1 in (" + strControlAccountSeg1 + ")", trans)
                End If
            End If

        End If

        dt = Nothing
        Return True
    End Function


    Shared Function GetVendorQuery(ByVal strVendorCustomerCode As String) As String
        'Return "select Account_Seg_Code1 from TSPL_VENDOR_MASTER left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_ACCOUNT_SET.Payable_Account where Vendor_Code='" + strVendorCustomerCode + "'"
        Return "select distinct TSPL_GL_ACCOUNTS.Account_Seg_Code1 from (" + Environment.NewLine +
        "select distinct GL_Code from (" + Environment.NewLine +
        "select TSPL_VENDOR_ACCOUNT_SET.Payable_Account,TSPL_VENDOR_ACCOUNT_SET.Discount_Account,TSPL_VENDOR_ACCOUNT_SET.Advance_Account,TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary,TSPL_VENDOR_ACCOUNT_SET.Employee_Salary,TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Travelling,TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Imprest" + Environment.NewLine +
        "from TSPL_VENDOR_MASTER " + Environment.NewLine +
        "left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account " + Environment.NewLine +
        "where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendorCustomerCode + "')xx" + Environment.NewLine +
        "UNPIVOT ( GL_Code FOR AC_Type IN  (Payable_Account, Discount_Account, Advance_Account, Advance_Against_Salary, Employee_Salary,Advance_Against_Travelling,Advance_Against_Imprest)" + Environment.NewLine +
        ")AS unpvt" + Environment.NewLine +
        ")xxx " + Environment.NewLine +
        "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=xxx.GL_Code "
    End Function

    Shared Function GetCustomerQuery(ByVal strVendorCustomerCode As String) As String
        Return "select distinct TSPL_GL_ACCOUNTS.Account_Seg_Code1 from (" + Environment.NewLine +
        "select distinct GL_Code from (" + Environment.NewLine +
        "select TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct, Receipts_Discount_acct,Advance_acct,Write_Offs" + Environment.NewLine +
        "from TSPL_CUSTOMER_MASTER " + Environment.NewLine +
        "left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " + Environment.NewLine +
        "where TSPL_CUSTOMER_MASTER.Cust_Code='" + strVendorCustomerCode + "'" + Environment.NewLine +
        ")xx" + Environment.NewLine +
        "UNPIVOT ( GL_Code FOR AC_Type IN  (Receivable_Control_acct, Receipts_Discount_acct, Advance_acct,Write_Offs)" + Environment.NewLine +
        ")AS unpvt" + Environment.NewLine +
        ")xxx " + Environment.NewLine +
        "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=xxx.GL_Code "
    End Function

    Public Shared Function CreateJEForEndYear(ByVal strVoucherNo As String, ByVal strEntryDate As Date, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Account_code,SUM(-1*Amount) as Amount,max(Account_Seg_Code7) as SegCode,IND_AS from (" + Environment.NewLine &
                        " select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date,TSPL_JOURNAL_DETAILS.Account_code ,Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7,TSPL_JOURNAL_MASTER.IND_AS" + Environment.NewLine &
                        " from TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                        " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine &
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code" + Environment.NewLine &
                        " left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  " + Environment.NewLine &
                        " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine &
                        " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code " + Environment.NewLine &
                        " left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code" + Environment.NewLine &
                        " where 2=2 "
        ''richa agarwal changes done against ticket no.BM00000009404 on 4Aug,2016
        Dim strcurrentfisyearenddate As DateTime? = Nothing
        Dim strCurrentfinancialYear As String = String.Empty
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select End_Date,Fiscal_Code from TSPL_Fiscal_Year_Master where convert(date, '" + strEntryDate + "',103)>= convert(date, Start_Date,103) and convert(date, '" + strEntryDate + "',103)<=CONVERT(date, End_Date,103)", trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            strcurrentfisyearenddate = dt1.Rows(0)("End_Date")
            strCurrentfinancialYear = clsCommon.myCstr(dt1.Rows(0)("Fiscal_Code"))
        End If
        ''-------------------------


        If clsCommon.myLen(strVoucherNo) > 0 Then
            qry += " and TSPL_JOURNAL_MASTER.Voucher_No = '" + strVoucherNo + "'"
        Else
            qry += " and TSPL_JOURNAL_MASTER.Voucher_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objCommonVar.CurrFiscalStartDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_JOURNAL_MASTER.Voucher_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(strcurrentfisyearenddate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
        End If
        qry += " and TSPL_ACCOUNT_MAIN_GROUPS.Group_Type='Income Statement' and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine &
                " )xxx" + Environment.NewLine &
                " group by Account_code,IND_AS " + Environment.NewLine &
                " order by SegCode,IND_AS"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim strSegCode As String = clsCommon.myCstr(dt.Rows(0)("SegCode"))
            Dim intINDAs As Integer = clsCommon.myCdbl(dt.Rows(0)("IND_AS"))
            Dim ArryLstNew As ArrayList = New ArrayList()
            Dim dblbal As Double = 0
            For Each dr As DataRow In dt.Rows
                If Not (clsCommon.CompairString(strSegCode, clsCommon.myCstr(dr("SegCode"))) = CompairStringResult.Equal AndAlso intINDAs = clsCommon.myCstr(dr("IND_AS"))) Then
                    ''Create Journal Entry
                    qry = "select Account_Code from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code='" + strSegCode + "'"
                    Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Please set gl account in Segment code master for segment : " + strSegCode)
                    End If

                    Dim Acc2() As String = {strCode, -1 * dblbal}
                    ArryLstNew.Add(Acc2)
                    FunGrnlEntryWithTrans(intINDAs, "", "X", strSegCode, True, False, "", trans, strcurrentfisyearenddate, "Fiscal Year End for " + strCurrentfinancialYear, "GL-JE", "", "", "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
                    ''Reset Variables
                    ArryLstNew = New ArrayList()
                    dblbal = 0
                    strSegCode = clsCommon.myCstr(dr("SegCode"))
                    intINDAs = clsCommon.myCdbl(dr("IND_AS"))
                End If
                Dim Acc1() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                dblbal += clsCommon.myCdbl(dr("Amount"))
                ArryLstNew.Add(Acc1)
            Next

            ''Create Journal Entry of Last Segment
            If ArryLstNew IsNot Nothing AndAlso ArryLstNew.Count > 0 Then
                qry = "select Account_Code from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code='" + strSegCode + "'"
                Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(strCode) <= 0 Then
                    Throw New Exception("Please set gl account in Segment code master for segment")
                End If

                Dim Acc2() As String = {strCode, -1 * dblbal}
                ArryLstNew.Add(Acc2)
                '  clsJournalMaster.FunGrnlEntryWithTrans("", "X", strSegCode, True, False, "", trans, objCommonVar.CurrFiscalEndDate, "Fiscal Year End for " + objCommonVar.CurrFiscalYear, "GL-JE", "", "", "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
                FunGrnlEntryWithTrans(intINDAs, "", "X", strSegCode, True, False, "", trans, strcurrentfisyearenddate, "Fiscal Year End for " + strCurrentfinancialYear, "GL-JE", "", "", "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetJounalEntryException(ByVal strJEDetail As String, ByVal VoucherNo As String, ByVal trans As SqlTransaction) As String
        Dim sql As String = "Select Account_code,Account_Desc,case when Amount>0 then Amount end as DrAmt,case when Amount<0 then -1*Amount end as CrAmt from " + strJEDetail + " WHERE Voucher_No='" + VoucherNo + "'"
        Dim dtError As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
        Dim msg As String = "Please Check Journal Entry [" + VoucherNo + "]" + Environment.NewLine
        msg += "--------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine
        Dim counter As Integer = 1
        Dim TotDrAmt As Double = 0
        Dim TotCrAmt As Double = 0
        For Each dr As DataRow In dtError.Rows
            msg += GetBlankSpaceNew(clsCommon.myCstr(counter), clsCommon.myCstr(dr("Account_code")), clsCommon.myCstr(dr("Account_Desc")), clsCommon.myCdbl(dr("DrAmt")), clsCommon.myCdbl(dr("CrAmt")))
            TotDrAmt += clsCommon.myCdbl(dr("DrAmt"))
            TotCrAmt += clsCommon.myCdbl(dr("CrAmt"))
            counter += 1
        Next
        msg += "--------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine
        msg += GetBlankSpaceNew("", "", "", TotDrAmt, TotCrAmt)
        msg += "--------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine
        Return msg
    End Function

    Private Shared Function GetBlankSpaceNew(ByVal SNo As String, ByVal AccountCode As String, ByVal AccountDes As String, ByVal DrAmt As Decimal, ByVal CrAmt As Decimal) As String
        Dim strBlankSpace As String = ""
        For ii As Integer = clsCommon.myLen(SNo) To 3 - 1
            strBlankSpace += " "
        Next
        strBlankSpace += clsCommon.myCstr(SNo) + " "

        strBlankSpace += clsCommon.myCstr(AccountCode)
        For ii As Integer = clsCommon.myLen(AccountCode) To 15 - 1
            strBlankSpace += " "
        Next



        For ii As Integer = clsCommon.myLen(DrAmt) To 15 - 1
            strBlankSpace += " "
        Next
        strBlankSpace += clsCommon.myFormat(DrAmt) + " "

        For ii As Integer = clsCommon.myLen(CrAmt) To 15 - 1
            strBlankSpace += " "
        Next
        strBlankSpace += clsCommon.myFormat(CrAmt) + " "


        strBlankSpace += "     " + clsCommon.myCstr(AccountDes)
        For ii As Integer = clsCommon.myLen(AccountDes) To 80 - 1
            strBlankSpace += " "
        Next



        Return strBlankSpace + Environment.NewLine
    End Function

    Public Shared Function fnAutoGenerateNo(ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal strPrefixTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean) As String
        Return fnAutoGenerateNo(False, trans, TranDate, strPrefixTransType, strLocationCode, isLocationCodeisSegment)
    End Function
    Public Shared Function fnAutoGenerateNo(ByVal JEWithOPTables As Boolean, ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal strPrefixTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean) As String
        Return fnAutoGenerateNo(JEWithOPTables, trans, TranDate, strPrefixTransType, strLocationCode, isLocationCodeisSegment, False)
    End Function
    Public Shared Function fnAutoGenerateNo(ByVal JEWithOPTables As Boolean, ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal strPrefixTransType As String, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, ByVal isLocationCodeIsMCC As Boolean) As String
        If clsCommon.myLen(strLocationCode) <= 0 Then
            Throw New Exception("First Account Should have location Segment")
        End If
        Return clsERPFuncationality.GetNextCode(trans, TranDate, IIf(JEWithOPTables, clsDocType.JournalEntryOP, clsDocType.JournalEntry), strPrefixTransType, strLocationCode, isLocationCodeisSegment, True, False, False, isLocationCodeIsMCC)
    End Function
End Class

Public Class clsJournalDetailTemp
    Public Account_code As String = Nothing
    Public Amount As String = Nothing
    Public Description As String = Nothing
    Public Reference As String = Nothing
    Public Hierarchy_Code As String = Nothing
    Public Cost_Center_Code As String = Nothing
    Public Hirerachy_Code3 As String = Nothing
    Public Hirerachy_Code4 As String = Nothing
    Public Reco_Control_Account As String = Nothing
End Class
Public Class clsJEExtraColumns
    Public TapalNo As String = Nothing
    Public DateAndTime As DateTime?
    Public VSP_CODE As String = Nothing

    Public Shared Function SaveData(ByVal obj As clsJEExtraColumns, ByVal tran As SqlTransaction, ByVal strVoucherNo As String) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TapalNo", obj.TapalNo, True)
            clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE, True)
            If clsCommon.myLen(obj.DateAndTime) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DateAndTime", clsCommon.GetPrintDate(obj.DateAndTime, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "DateAndTime", Nothing, True)
            End If

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOURNAL_MASTER", OMInsertOrUpdate.Update, "TSPL_JOURNAL_MASTER.Voucher_No='" + strVoucherNo + "'", tran)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsGLAccount
#Region "Variables"
    Public Account_Code As String = Nothing
    Public Description As String = Nothing
    Public Str_Code As String = Nothing
    Public Str_Description As String = Nothing
    Public Account_Balance As String = Nothing
    Public Status As String = Nothing
    Public ControlAccount As String = Nothing
    Public AutoAllocation As String = Nothing
    Public multicurrency As String = Nothing
    Public Account_Seg_Code1 As String = Nothing
    Public Account_Seg_Desc1 As String = Nothing
    Public Account_Seg_Code2 As String = Nothing
    Public Account_Seg_Desc2 As String = Nothing
    Public Account_Seg_Code3 As String = Nothing
    Public Account_Seg_Desc3 As String = Nothing
    Public Account_Seg_Code4 As String = Nothing
    Public Account_Seg_Desc4 As String = Nothing
    Public Account_Seg_Code5 As String = Nothing
    Public Account_Seg_Desc5 As String = Nothing
    Public Account_Seg_Code6 As String = Nothing
    Public Account_Seg_Desc6 As String = Nothing
    Public Account_Seg_Code7 As String = Nothing
    Public Account_Seg_Desc7 As String = Nothing
    Public Account_Seg_Code8 As String = Nothing
    Public Account_Seg_Desc8 As String = Nothing
    Public Account_Seg_Code9 As String = Nothing
    Public Account_Seg_Desc9 As String = Nothing
    Public Account_Seg_Code10 As String = Nothing
    Public Account_Seg_Desc10 As String = Nothing
    Public Close_To_Seg As String = Nothing
    Public Close_To_Acct As String = Nothing
    Public TallyAccName As String = Nothing
    Public Tax_Type As String = Nothing
    Public Purchase_Sale_Type As Integer = 0
    Public GL_Main_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsGLAccount) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsGLAccount, ByVal trans As SqlTransaction) As Boolean
        clsGLAccount.GetLinkAccountWithGroup(5, obj.Account_Code, obj.GL_Main_Code, trans)

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
        clsCommon.AddColumnsForChange(coll, "Str_Code", obj.Str_Code)
        clsCommon.AddColumnsForChange(coll, "Str_Description", obj.Str_Description)
        clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
        clsCommon.AddColumnsForChange(coll, "ControlAccount", obj.ControlAccount)
        clsCommon.AddColumnsForChange(coll, "AutoAllocation", obj.AutoAllocation)
        clsCommon.AddColumnsForChange(coll, "multicurrency", obj.multicurrency)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code1", obj.Account_Seg_Code1)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc1", obj.Account_Seg_Desc1)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code2", obj.Account_Seg_Code2)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc2", obj.Account_Seg_Desc2)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code3", obj.Account_Seg_Code3)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc3", obj.Account_Seg_Desc3)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code4", obj.Account_Seg_Code4)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc4", obj.Account_Seg_Desc4)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code5", obj.Account_Seg_Code5)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc5", obj.Account_Seg_Desc5)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code6", obj.Account_Seg_Code6)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc6", obj.Account_Seg_Desc6)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code7", obj.Account_Seg_Code7)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc7", obj.Account_Seg_Desc7)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code8", obj.Account_Seg_Code8)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc8", obj.Account_Seg_Desc8)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code9", obj.Account_Seg_Code9)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc9", obj.Account_Seg_Desc9)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Code10", obj.Account_Seg_Code10)
        clsCommon.AddColumnsForChange(coll, "Account_Seg_Desc10", obj.Account_Seg_Desc10)

        clsCommon.AddColumnsForChange(coll, "Close_To_Seg", obj.Close_To_Seg)
        clsCommon.AddColumnsForChange(coll, "Close_To_Acct", obj.Close_To_Acct)
        clsCommon.AddColumnsForChange(coll, "TallyAccName", obj.TallyAccName)
        clsCommon.AddColumnsForChange(coll, "Tax_Type", obj.Tax_Type)
        clsCommon.AddColumnsForChange(coll, "Purchase_Sale_Type", obj.Purchase_Sale_Type)
        clsCommon.AddColumnsForChange(coll, "GL_Main_Code", obj.GL_Main_Code)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Account_Code + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_ACCOUNTS", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_ACCOUNTS", OMInsertOrUpdate.Update, "Account_Code='" + obj.Account_Code + "'", trans)
        End If
        Return True
    End Function

    Public Shared Function GetName(ByVal Code As String) As String
        Return GetName(Code, Nothing)
    End Function

    Public Shared Function GetName(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + Code + "'", trans))
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = " select Account_Code as [Code] ,Description as [Description],TSPL_GL_ACCOUNTS.GL_Main_Code as [GL Main Account Code],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc AS [GL Main Account Description],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code As [Sub Group Code],TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Sub Group Description],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type] ,Str_Code as [Account Structure Code] ,Str_Description as [Account Structure Description] ,(case when status='Y' then 'Active' else 'In Active' end) as [Status] ,ControlAccount as [Control Account]  ,multicurrency as [Multi Currency] ,Account_Seg_Code1 as [Account Segment Code1] ,Account_Seg_Desc1 as [Account Segment Description1] ,Account_Seg_Code2 as [Account Segment Code2] ,Account_Seg_Desc2 as [Account Segment Description2] ,Account_Seg_Code3 as [Account Segment Code3] ,Account_Seg_Desc3 as [Account Segment Description3] ,Account_Seg_Code4 as [Account Segment Code4] ,Account_Seg_Desc4 as [Account Segment Description4] ,Account_Seg_Code5 as [Account Segment Code5] ,Account_Seg_Desc5 as [Account Segment Description5] ,Account_Seg_Code6 as [Account Segment Code6] ,Account_Seg_Desc6 as [Account Segment Description6] ,Account_Seg_Code7 as [Account Segment Code7] ,Account_Seg_Desc7 as [Account Segment Description7] ,Account_Seg_Code8 as [Account Segment Code8] ,Account_Seg_Desc8 as [Account Segment Description8] ,Account_Seg_Code9 as [Account Segment Code9] ,Account_Seg_Desc9 as [Account Segment Description9] ,Account_Seg_Code10 as [Account Segment Code10] ,Account_Seg_Desc10 as [Account Segment Description10] ,Close_To_Seg as [Close To Segment] ,Close_To_Acct as [Close To Account]  ,Rollup_Seq as [Roll Up Sequence] ,TallyAccName as [Tally Account Name] ,Tax_Type as [Tax Type] ,Purchase_Sale_Type as [Purchase Sale Type],TSPL_GL_ACCOUNTS.Created_By as [Created By] ,TSPL_GL_ACCOUNTS.Created_Date as [Created Date] ,TSPL_GL_ACCOUNTS.Modify_By as [Modify By] ,TSPL_GL_ACCOUNTS.Modify_Date as [Modify Date] ,TSPL_GL_ACCOUNTS.Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS  left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code "
        str = clsCommon.ShowSelectForm("RPTGLACFND", str, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function Get_Location_Segment(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + Code + "'", trans))
    End Function

    Public Shared Function CheckControlAccount(ByVal strGLCode As String, ByVal ControlAccountStatus As Boolean, ByVal trans As SqlTransaction) As Boolean
        ''Ticket no BM00000009848 by balwinder on 24/10/2016
        Dim qry As String = "select ControlAccount from TSPL_GL_ACCOUNTS where Account_Code='" + strGLCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim res As Boolean = clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("ControlAccount")), "Y") = CompairStringResult.Equal
            If Not res = ControlAccountStatus Then
                ''IF Before save status is control account than only check it is used in GL Entry or any account set table
                qry = "select 1 from TSPL_JOURNAL_DETAILS where Account_code='" + strGLCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Account - " + strGLCode + Environment.NewLine + " Can't change control account to Normal account becuase it is used in GL Entry")
                End If
                qry = "select 'select '''+COLUMN_NAME+''' as FaultCol,''Customer Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+'' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_CUSTOMER_ACCOUNT_SET'  and COLUMN_NAME not in ('CURRENCY_CODE','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Cust_Account','Cust_Acct_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Vendor Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_VENDOR_ACCOUNT_SET'  and COLUMN_NAME not in ('CURRENCY_CODE','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Acct_Set_Code','Acct_Set_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Purchase Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_PURCHASE_ACCOUNTS'  and COLUMN_NAME not in ('Costing_Method','Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Purchase_Class_Code','Purchase_Class_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Sales Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_SALES_ACCOUNTS'  and COLUMN_NAME not in ('Created_By','Created_Date','Modify_By','Modify_Date','Comp_Code','Sales_Class_Code','Sales_Class_Desc') and DATA_TYPE like '%char%' " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Fixed Asset Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_Dep_AccountSet' and COLUMN_NAME not in ('AcSet_Code','AcSet_Desc','Inactive','Created_By','Created_Date','Modified_By','Modified_Date','Comp_Code') and DATA_TYPE like '%char%' " + Environment.NewLine +
                " union all " + Environment.NewLine +
                " select 'select '''+COLUMN_NAME+''' as FaultCol,''Payroll Account Set'' as FaultAccountSet  from '+TABLE_NAME+' where '+ COLUMN_NAME +'='+'''" + strGLCode + "'''+''  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='TSPL_PAYROLL_ACCOUNTSETS'  and COLUMN_NAME not in ('ACCOUNT_SET_CODE','DESCRIPTION','BANK_CODE','Created_By','Created_Date','Modified_By','Modified_Date') and DATA_TYPE like '%char%'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = ""
                    Dim isFirstTime As Boolean = True
                    For Each dr As DataRow In dt.Rows
                        If Not isFirstTime Then
                            qry += " Union all " + Environment.NewLine
                        End If
                        qry += clsCommon.myCstr(dr(0))
                        isFirstTime = False
                    Next
                    qry += "union all " + Environment.NewLine +
                    " select 'BANKACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where BANKACC='" + strGLCode + "'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    " select 'WRITEOFFACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where WRITEOFFACC='" + strGLCode + "'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    " select 'CREDITACC' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where CREDITACC='" + strGLCode + "'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    " select 'Transfer_Clearing_Account' as FaultCol,'Bank Master' as FaultAccountSet  from TSPL_BANK_MASTER where Transfer_Clearing_Account='" + strGLCode + "'" + Environment.NewLine +
                    "union all " + Environment.NewLine +
                    " select 'Closing_Account' as FaultCol,'GL Option' as FaultAccountSet  from TSPL_GLSETTING where Closing_Account='" + strGLCode + "' " + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    " select 'Clearing_Account' as FaultCol,'GL Option' as FaultAccountSet  from TSPL_GLSETTING where Clearing_Account='" + strGLCode + "'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    " select 'Gl_Account' as FaultCol,'Nature of Deduction' as FaultAccountSet  from TSPL_TDS_DEDUCTION_HEAD where Gl_Account='" + strGLCode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        qry = "Account - " + strGLCode + Environment.NewLine + " Can't change control account to Normal account because Account is mapped with following account Set"
                        For Each dr As DataRow In dt.Rows
                            qry += Environment.NewLine + clsCommon.myCstr(dr("FaultAccountSet")) + " [" + clsCommon.myCstr(dr("FaultCol")) + "]"
                        Next
                        Throw New Exception(qry)
                    End If
                End If
            End If
            qry = Nothing
            dt = Nothing
        End If
        Return True
    End Function

    Public Shared Function GetLinkAccountWithGroup(ByVal level As Integer, ByVal strCode As String, ByVal strNewValue As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select TSPL_ACCOUNT_MAIN_GROUPS.Group_Type,TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code ,TSPL_ACCOUNT_GROUPS.Account_Group_Code,TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account,TSPL_GL_ACCOUNTS.Account_Code " + Environment.NewLine +
        " from TSPL_GL_ACCOUNTS" + Environment.NewLine +
        "inner join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  " + Environment.NewLine +
        "inner join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  " + Environment.NewLine +
        "inner join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code " + Environment.NewLine +
        "inner join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " + Environment.NewLine +
        "inner join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code" + Environment.NewLine +
        "where 2=2"
        If level = 1 Then
            qry += " and TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code='" + strCode + "'"
        ElseIf level = 2 Then
            qry += " and TSPL_ACCOUNT_GROUPS.Account_Group_Code='" + strCode + "'"
        ElseIf level = 3 Then
            qry += " and TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code='" + strCode + "'"
        ElseIf level = 4 Then
            qry += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account='" + strCode + "'"
        ElseIf level = 5 Then
            qry += " and TSPL_GL_ACCOUNTS.Account_Code='" + strCode + "'"
        Else
            Throw New Exception("Wrong Level it should be from 1-5")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If Not clsCommon.CompairString(strNewValue, clsCommon.myCstr(dt.Rows(0)(level - 1))) = CompairStringResult.Equal Then
                Throw New Exception("Sorry ! You cannot change Value [" + clsCommon.myCstr(dt.Rows(0)(level - 1)) + "] To [" + strNewValue + "] For " + strCode + Environment.NewLine + " Becuase account is used in GL Segment Account for Fiscal End year")
            End If
        End If
        Return True
    End Function

    Public Shared Function CheckYearEndAccountFilledInSegment(ByVal strVoucherNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select distinct TSPL_JOURNAL_DETAILS.Account_Seg_Code7 from TSPL_JOURNAL_DETAILS" + Environment.NewLine +
        "TSPL_JOURNAL_DETAILS" + Environment.NewLine +
        "left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode,TSPL_GL_SEGMENT_CODE.Segment_code as SegCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.SegCode=TSPL_JOURNAL_DETAILS.Account_Seg_Code7" + Environment.NewLine +
        "where TSPL_JOURNAL_DETAILS.Voucher_No='" + strVoucherNo + "' and len(isnull( segTable.AccCode,''))=0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("First set Fiscal End year Account in GL Segment for Segment Code" + clsCommon.myCstr(dt.Rows(0)("Account_Seg_Code7")))
        End If
        Return True
    End Function
    Public Shared Function GetQueryGLAccountsUsedInAllAccountSet() As String
        ''select Account_Code from TSPL_GL_ACCOUNTS where substring(Account_Code,0,10) in (SELECT substring(Payable_Account,0,10) as Control_Account from TSPL_VENDOR_ACCOUNT_SET)
        Dim qry As String = ""
        qry = "SELECT Payable_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Payable_Account,''))>0" & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Discount_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Discount_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Advance_Account as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Advance_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT EXCHANGE_GAIN_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(EXCHANGE_GAIN_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT EXCHANGE_LOSS_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(EXCHANGE_LOSS_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Commission_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Commission_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Incentive_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Incentive_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT SECURITY_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Head_Load_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Head_Load_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Own_Asset_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Own_Asset_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Deduction_ACCOUNT as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Deduction_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Advance_Against_Salary as Control_Account FROM TSPL_VENDOR_ACCOUNT_SET where len(coalesce(Advance_Against_Salary,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Inv_Control_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Inv_Control_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Inv_Payable_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Inv_Payable_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Adjustment_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Adjustment_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Assembly_Cost_Credit as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Assembly_Cost_Credit,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Non_Stock_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Non_Stock_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Transfer_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Transfer_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Shipment_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Shipment_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Disassembly_Expense as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Disassembly_Expense,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Physical_Inv_Adjustment as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Physical_Inv_Adjustment,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Credit_Debit_Note_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Credit_Debit_Note_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Reserve_Stock as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Reserve_Stock,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Breakage_Gl_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Breakage_Gl_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT WIP_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(WIP_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT RM_Consumption as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(RM_Consumption,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Other_1 as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Other_1,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Other_2 as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Other_2,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Purchase_Control_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Control_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Transfer_Gain_Loss_Ac as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Transfer_Gain_Loss_Ac,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Job_Work_Ac as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Job_Work_Ac,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Stock_Transfer_In as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Stock_Transfer_In,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Stock_Transfer_Acc as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Stock_Transfer_Acc,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Provision_Clearing as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Provision_Clearing,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Chilling_Charges as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Chilling_Charges,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Freight_Charges as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Freight_Charges,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Purchase_Account as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Purchase_Set_Off as Control_Account FROM TSPL_PURCHASE_ACCOUNTS where len(coalesce(Purchase_Set_Off,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Receivable_Control_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Receivable_Control_acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Receipts_Discount_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Receipts_Discount_acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Advance_acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Advance_acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Write_Offs as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Write_Offs,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Container_Deposit as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Container_Deposit,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT EXCHANGE_LOSS_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(EXCHANGE_LOSS_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT EXCHANGE_GAIN_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(EXCHANGE_GAIN_ACCOUNT,''))>0 " & Environment.NewLine &
         " union" & Environment.NewLine &
         " SELECT SECURITY_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT CREATE_SECURITY_ACCOUNT as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(CREATE_SECURITY_ACCOUNT,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT BANK_GUARANTEE as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(BANK_GUARANTEE,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT ACCOUNT1 as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(ACCOUNT1,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT ACCOUNT2 as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(ACCOUNT2,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT GSOC_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(GSOC_Acct,''))>0 " & Environment.NewLine &
         " union  " & Environment.NewLine &
         " SELECT Consignment_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Consignment_Acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Gain_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Gain_Acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Loss_Acct as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Loss_Acct,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Foreign_Bank_Charges_Account as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Foreign_Bank_Charges_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Bank_Charges_Other_Account as Control_Account FROM TSPL_CUSTOMER_ACCOUNT_SET where len(coalesce(Bank_Charges_Other_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Sales_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Sales_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Sales_Return_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Sales_Return_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Cost_Of_Goods_Sold as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cost_Of_Goods_Sold,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Cost_Variance as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cost_Variance,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT  Damaged_Goods as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Damaged_Goods,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Internal_Usage as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Internal_Usage,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Returnable_Container as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Returnable_Container,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Schemes as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Schemes,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Promotional as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Promotional,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Cogs_InterBranch as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Cogs_InterBranch,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Suspence_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Suspence_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Gain_Loss_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Gain_Loss_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT Stock_Transfer_AC as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(Stock_Transfer_AC,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT COGT_AC as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(COGT_AC,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " SELECT DisplayPurpose_Account as Control_Account FROM TSPL_SALES_ACCOUNTS where len(coalesce(DisplayPurpose_Account,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " select BANKACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(BANKACC,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " select WRITEOFFACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(WRITEOFFACC,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " select CREDITACC as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(CREDITACC,''))>0 " & Environment.NewLine &
         " union " & Environment.NewLine &
         " select Transfer_Clearing_Account as Control_Account FROM TSPL_BANK_MASTER where len(coalesce(Transfer_Clearing_Account,''))>0"
        qry = "select Account_Code as Control_Account from TSPL_GL_ACCOUNTS where substring(Account_Code,0,10) in (SELECT substring(Control_Account,0,10) as Control_Account from (" & qry & ") as Acc)"
        Return qry
    End Function
    Public Shared Function GetGLAccountsUsedInAllAccountSet(ByVal trans As SqlTransaction) As ArrayList
        Dim arr As New ArrayList
        Dim qry As String = GetQueryGLAccountsUsedInAllAccountSet()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            arr.Add(clsCommon.myCstr(dr.Item("Control_Account")))
        Next
        Return arr
    End Function
End Class