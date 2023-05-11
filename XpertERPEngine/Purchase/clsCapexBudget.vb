Imports common
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCapexBudget


#Region "Variables"
    Public Code As String
    Public Description As String
    Public CapexCode As String
    Public Budget As Decimal
    Public RevBudget As Decimal
    Public Tolerence As Decimal
    Public DocDate As DateTime
    Public RevNo As String
    Public IncBudget As Decimal
    Public CurrentBudget As Decimal
    Public Provisional As Boolean
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_CAPEX_BUDGET_MASTER.CODE as [Code], TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [Description],TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [Capex Code],TSPL_CAPEX_BUDGET_MASTER.Budget as [Budget],TSPL_CAPEX_BUDGET_MASTER.Revised_Budget  as [Revised Budget],TSPL_CAPEX_BUDGET_MASTER.Tolerence as [Tolerence],TSPL_CAPEX_BUDGET_MASTER.Doc_Date as [Date],TSPL_CAPEX_BUDGET_MASTER.Revision_No as [Revision No] from TSPL_CAPEX_BUDGET_MASTER"
        str = clsCommon.ShowSelectForm("CAPMTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function GetName(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetCapexCode(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_CAPEX_BUDGET_MASTER.Capex_Code from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBudget(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "SELECT (case when ISNULL(TSPL_CAPEX_BUDGET_MASTER .Revised_Budget,0 )>0 then TSPL_CAPEX_BUDGET_MASTER.Revised_Budget else TSPL_CAPEX_BUDGET_MASTER .Budget end) as [Budget] FROM TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBudgetWithTolerence(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "SELECT (case when ISNULL(TSPL_CAPEX_BUDGET_MASTER .Revised_Budget,0 )>0 then TSPL_CAPEX_BUDGET_MASTER.Revised_Budget" & _
                                 " else TSPL_CAPEX_BUDGET_MASTER .Budget end)+(case when ISNULL(TSPL_CAPEX_BUDGET_MASTER .Revised_Budget,0 )>0 then TSPL_CAPEX_BUDGET_MASTER.Revised_Budget" & _
                                 " else TSPL_CAPEX_BUDGET_MASTER .Budget end)*(ISNULL(TSPL_CAPEX_BUDGET_MASTER .Tolerence,0) /100) FROM TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetSubCapexValue(ByVal strcode As String, ByVal PONo As String, ByVal trans As SqlTransaction, Optional ByVal InOutType As String = "B") As Decimal
        Dim val As Decimal = 0
        Try
            Dim condInOut As String = ""
            If clsCommon.CompairString(InOutType, "B") = CompairStringResult.Equal Then
                condInOut = ""
            ElseIf clsCommon.CompairString(InOutType, "I") = CompairStringResult.Equal Then
                condInOut = "I"
            ElseIf clsCommon.CompairString(InOutType, "I") = CompairStringResult.Equal Then
                condInOut = "O"
            Else
                condInOut = ""
            End If

            Dim qry As String = "select sum(PO_Total_Amt) as PO_Total_Amt from ( "
            qry += " select sum(case when TSPL_PURCHASE_ORDER_HEAD.close_yn='N' then TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt " & _
                " else (coalesce(TSPL_SRN_HEAD.SRN_Total_Amt,0)-(coalesce(TSPL_SRN_HEAD.Total_Rejected_Amount,0)+coalesce(TSPL_SRN_HEAD.Rejected_tax_Amt,0))) end) as PO_Total_Amt,max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'I' as InOut from TSPL_PURCHASE_ORDER_HEAD " & _
                " left join (select TSPL_SRN_HEAD.Against_PO,sum(TSPL_SRN_HEAD.SRN_Total_Amt) as SRN_Total_Amt,sum(TSPL_SRN_DETAIL.Rejected_Amount) as Total_Rejected_Amount ," & _
                " sum(case when TSPL_SRN_DETAIL.Taxable_Amount<=0 then 0 else ((TSPL_SRN_DETAIL.Total_Tax_Amt/TSPL_SRN_DETAIL.Taxable_Amount)*TSPL_SRN_DETAIL.Rejected_Amount) end) as Rejected_tax_Amt from TSPL_SRN_DETAIL " & _
                " inner join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No group by TSPL_SRN_HEAD.Against_PO ) TSPL_SRN_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_HEAD.Against_PO inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                " TSPL_CAPEX_BUDGET_MASTER.CODE=TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_PURCHASE_ORDER_HEAD.Capex_Code" & _
                " where 2=2 "
            qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No <>'" + clsCommon.myCstr(PONo) + "'"
            qry += " and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "' " & If(clsCommon.myLen(condInOut) > 0, " and  '" & condInOut & "' ='I'", "")
            If clsCommon.myLen(condInOut) > 0 Then

            End If
            ''===========Sanjeet(22/01/2018- to adding the Store issue Return Doc_Amt when it is Transfer Capex to store Type===============
            qry += "union all select sum(TSPL_IssueReturn_HEAD.Doc_Amt ),max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'I' as InOut from TSPL_IssueReturn_HEAD " & _
                " inner join TSPL_CAPEX_BUDGET_MASTER on  TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_IssueReturn_HEAD.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_IssueReturn_HEAD.Capex_Code"
            qry += "  where 2 = 2 and TSPL_IssueReturn_HEAD.Doc_No<>'" + clsCommon.myCstr(PONo) + "' and Doc_Type='TransferCX' and TSPL_CAPEX_BUDGET_MASTER.CODE ='" + clsCommon.myCstr(strcode) + "' " & If(clsCommon.myLen(condInOut) > 0, " and '" & condInOut & "' ='I'", "")
            ''==============================
            qry += " union all "
            qry += " select sum(TSPL_ACQUISITION_detail.Item_Net_Amt ),max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'I' as InOut from TSPL_ACQUISITION_detail inner join TSPL_CAPEX_BUDGET_MASTER on "
            qry += " TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_ACQUISITION_detail.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_ACQUISITION_detail.Capex_Code"
            qry += " where 2 = 2"
            qry += "  and TSPL_ACQUISITION_detail.Acquisition_Code  <>'" + clsCommon.myCstr(PONo) + "'"
            qry += "  and isnull(TSPL_ACQUISITION_detail.IsCapex,0)=1 and TSPL_ACQUISITION_detail.CapexType ='Regular'"
            qry += " and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "'" & If(clsCommon.myLen(condInOut) > 0, " and '" & condInOut & "' ='I'", "")
            qry += " union all "
            qry += " select sum(TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt),max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'I' as Inout " & _
                   " from TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No  "
            qry += " inner join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_IssueItemToAssembledAsset_Detail.Capex_Code"
            qry += " where TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Issue' "
            qry += "  and TSPL_IssueItemToAssembledAsset_Detail.Doc_No  <>'" + clsCommon.myCstr(PONo) + "'"
            qry += "  and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "'"
            '' commented by panch raj (due to change in capex code and sub code on the screen , now will come from asset not item) ->22-Jan-2017 rollabck of the work Panch Raj->Shruti mam and Manish '' richa use ANd condition in place of OR UDL/29/04/19-000294
            qry += "  and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1   and (TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular' and TSPL_IssueItemToAssembledAsset_Detail.CheckCapexLimit=1)" & If(clsCommon.myLen(condInOut) > 0, " and '" & condInOut & "' ='I'", "")
            qry += " union all "
            qry += " select sum(- TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt),max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'O' as Inout " & _
                   " from TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No  "
            qry += " inner join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_IssueItemToAssembledAsset_Detail.Capex_Code"
            qry += " where TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Return' "
            qry += "  and TSPL_IssueItemToAssembledAsset_Detail.Doc_No  <>'" + clsCommon.myCstr(PONo) + "'"
            qry += "  and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "'"
            '' commented by panch raj (due to change in capex code and sub code on the screen , now will come from asset not item) ->22-Jan-2017 rollabck of the work Panch Raj->Shruti mam and Manish '' richa use ANd condition in place of OR UDL/29/04/19-000294
            qry += "  and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1   and (TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular' and TSPL_IssueItemToAssembledAsset_Detail.CheckCapexLimit=1)" & If(clsCommon.myLen(condInOut) > 0, " and '" & condInOut & "' ='O'", "")
            qry += " union all "
            qry += " select sum(TSPL_ASSET_WORK_HEAD.Net_Amt ),max(TSPL_CAPEX_BUDGET_MASTER .CODE) as SubCode,'I' as InOut from TSPL_ASSET_WORK_HEAD inner join TSPL_CAPEX_BUDGET_MASTER on "
            qry += " TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_ASSET_WORK_HEAD.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_ASSET_WORK_HEAD.Capex_Code"
            qry += " where coalesce(RefDocType,'') not in ('WO')"
            qry += "   and TSPL_ASSET_WORK_HEAD.Document_Code  <>'" + clsCommon.myCstr(PONo) + "'"
            qry += "  and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + strcode + "'" & If(clsCommon.myLen(condInOut) > 0, " and '" & condInOut & "' ='I'", "")
            qry += " ) as final"

            val = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return val
    End Function


    Public Shared Function GetReBudget(ByVal strcode As String, ByVal PONo As String, ByVal trans As SqlTransaction, Optional ByVal InOutType As String = "B") As String

        Try
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudget(strcode, trans)) - GetSubCapexValue(strcode, PONo, trans, InOutType))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetReBudgetWithTolerence(ByVal strcode As String, ByVal PONo As String, ByVal trans As SqlTransaction, Optional ByVal InOutType As String = "B") As String
        Try

            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudgetWithTolerence(strcode, trans)) - GetSubCapexValue(strcode, PONo, trans, InOutType))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetReBudgetAcquition(ByVal strcode As String, ByVal PONo As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_ACQUISITION_HEAD.Net_Amt) from TSPL_ACQUISITION_HEAD inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                "  TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_ACQUISITION_HEAD.CapexSub_Code and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_ACQUISITION_HEAD.Capex_Code" & _
                                " where TSPL_ACQUISITION_HEAD.Acquisition_Code <>'" + clsCommon.myCstr(PONo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "'"
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudget(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetReBudgetWithTolerenceAcquition(ByVal strcode As String, ByVal PONo As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_ACQUISITION_HEAD.Net_Amt) from TSPL_ACQUISITION_HEAD inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                "  TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_ACQUISITION_HEAD.CapexSub_Code and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_ACQUISITION_HEAD.Capex_Code" & _
                                " where TSPL_ACQUISITION_HEAD.Acquisition_Code <>'" + clsCommon.myCstr(PONo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "'"
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudgetWithTolerence(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetReBudgetAsset(ByVal strcode As String, ByVal DOCNo As String, ByVal ItemCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt) from TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                "  TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_IssueItemToAssembledAsset_Detail.Capex_Code " & _
                                " where TSPL_IssueItemToAssembledAsset_Detail.Doc_No <>'" + clsCommon.myCstr(DOCNo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "' and TSPL_IssueItemToAssembledAsset_Detail.Item_Code= '" + clsCommon.myCstr(ItemCode) + "' and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1 and TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular'  "
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudget(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetReBudgetAssetWithTolerence(ByVal strcode As String, ByVal DocNo As String, ByVal ItemCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt) from TSPL_IssueItemToAssembledAsset_Detail inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                " TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_IssueItemToAssembledAsset_Detail.Capex_Code" & _
                                " where TSPL_IssueItemToAssembledAsset_Detail.Doc_No <>'" + clsCommon.myCstr(DocNo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "' and TSPL_IssueItemToAssembledAsset_Detail.Item_Code= '" + clsCommon.myCstr(ItemCode) + "' and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1 and TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular'  "
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudgetWithTolerence(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '============================Added by preeti gupta=================================
    Public Shared Function GetReBudgetAssetAcquisition(ByVal strcode As String, ByVal DOCNo As String, ByVal ItemCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_ACQUISITION_DETAIL.Item_Net_Amt) from TSPL_ACQUISITION_DETAIL inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                "  TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_ACQUISITION_DETAIL.Capex_SubCode and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_ACQUISITION_DETAIL.Capex_Code " & _
                                " where TSPL_ACQUISITION_DETAIL.Acquisition_Code <>'" + clsCommon.myCstr(DOCNo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "' " & _
                                " and isnull(TSPL_ACQUISITION_DETAIL.IsCapex,0)=1 and TSPL_ACQUISITION_DETAIL.CapexType ='Regular'  "
            'and TSPL_ACQUISITION_DETAIL.Item_Code= '" + clsCommon.myCstr(ItemCode) + "'
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudget(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetReBudgetAssetAcquisitionWithTolerence(ByVal strcode As String, ByVal DocNo As String, ByVal ItemCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select sum(TSPL_ACQUISITION_DETAIL.Item_Net_Amt) from TSPL_ACQUISITION_DETAIL inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                                " TSPL_CAPEX_BUDGET_MASTER.CODE   =TSPL_ACQUISITION_DETAIL.Capex_SubCode and TSPL_CAPEX_BUDGET_MASTER.Capex_Code  =TSPL_ACQUISITION_DETAIL.Capex_Code" & _
                                " where TSPL_ACQUISITION_DETAIL.Acquisition_Code <>'" + clsCommon.myCstr(DocNo) + "' and TSPL_CAPEX_BUDGET_MASTER .CODE ='" + clsCommon.myCstr(strcode) + "' " & _
                                " and isnull(TSPL_ACQUISITION_DETAIL.IsCapex,0)=1 and TSPL_ACQUISITION_DETAIL.CapexType ='Regular'  "
            'and TSPL_IssueItemToAssembledAsset_Detail.Item_Code= '" + clsCommon.myCstr(ItemCode) + "' 
            Return clsCommon.myCstr(clsCommon.myCdbl(GetBudgetWithTolerence(strcode, trans)) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function IssueAgainstCapexPurchaseValueBaseQry(ByVal Sub_Capex_Code As String, ByVal CurrDoc As String) As String
        Try
            Dim qry As String = ""            
            qry = " select Capex_SubCode,sum(Issue_Amount) as Issue_Amount from ( " &
                   " select TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode,((case when TSPL_IssueItemToAssembledAsset_Head.Doc_Type='Issue' then 1 else -1 end) * TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt) as Issue_Amount " & _
                   " from TSPL_IssueItemToAssembledAsset_Detail " & _
                   " inner join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No=TSPL_IssueItemToAssembledAsset_Head.Doc_No  " & _
                   " inner join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode " & _
                   " And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_IssueItemToAssembledAsset_Detail.Capex_Code" & _
                   " where 2 = 2 " & _
                   " and TSPL_IssueItemToAssembledAsset_Detail.Capex_SubCode ='" & Sub_Capex_Code & "'" & _
                   " and isnull(TSPL_IssueItemToAssembledAsset_Detail.IsCapex,0)=1   " & _
                   " and (TSPL_IssueItemToAssembledAsset_Detail.CapexType ='Regular' OR TSPL_IssueItemToAssembledAsset_Detail.CheckCapexLimit=0) and TSPL_IssueItemToAssembledAsset_Head.Doc_No<>'" & CurrDoc & "'" & _
                   " union all " & _
                   " select TSPL_ASSET_WORK_HEAD.Capex_SubCode,TSPL_ASSET_WORK_HEAD.Net_Amt as Issue_Amount from TSPL_ASSET_WORK_HEAD inner join TSPL_CAPEX_BUDGET_MASTER on " & _
                   " TSPL_CAPEX_BUDGET_MASTER.CODE = TSPL_ASSET_WORK_HEAD.Capex_SubCode And TSPL_CAPEX_BUDGET_MASTER.Capex_Code = TSPL_ASSET_WORK_HEAD.Capex_Code" & _
                   " where coalesce(RefDocType,'') in ('WO') and TSPL_ASSET_WORK_HEAD.Document_Code  <>'" & CurrDoc & "' and TSPL_ASSET_WORK_HEAD.Capex_SubCode ='" & Sub_Capex_Code & "'" & _
                   " ) as Final group by Capex_SubCode"

            Return qry
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNegativeIssueAgainstCapexPurchaseValue(ByVal Sub_Capex_Code As String, ByVal CurrDoc As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = IssueAgainstCapexPurchaseValueBaseQry(Sub_Capex_Code, CurrDoc)
            Dim PurchaseValue As Decimal = clsCapexBudget.GetSubCapexValue(Sub_Capex_Code, "", trans, "I")
            Dim IssueAgainstPValue As Decimal = 0
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt.Rows.Count > 0 Then
                IssueAgainstPValue = clsCommon.myCdbl(dt.Rows(0).Item("Issue_Amount"))
            End If            
            If (PurchaseValue - IssueAgainstPValue) < 0 Then
                Throw New Exception("Trying to issue more value than purchase against Sub Capex " & Sub_Capex_Code & "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNegativeSubCapexBalance(ByVal Sub_Capex_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim CapexBal As Decimal = clsCapexBudget.GetReBudgetWithTolerence(Sub_Capex_Code, "", trans, "B")
            If CapexBal < 0 Then
                Throw New Exception("Trying to issue more value than Budget including tolerance for Sub Capex " & Sub_Capex_Code & "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '=========================================================================================================================================================

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCapexBudget
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
            qry = "delete from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCapexBudget
        Dim obj As clsCapexBudget = Nothing
        Dim qry As String = "select TSPL_CAPEX_BUDGET_MASTER.CODE as [Code], TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [Description],TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [Capex Code],TSPL_CAPEX_BUDGET_MASTER.Budget as [Budget],TSPL_CAPEX_BUDGET_MASTER.Revised_Budget  as [Revised Budget],TSPL_CAPEX_BUDGET_MASTER.Tolerence as [Tolerence],TSPL_CAPEX_BUDGET_MASTER.Doc_Date as [Date],TSPL_CAPEX_BUDGET_MASTER.Revision_No as [Revision No],TSPL_CAPEX_BUDGET_MASTER.Inc_Budget as [Inc Budget],Current_Budget,TSPL_CAPEX_BUDGET_MASTER.Provisional from TSPL_CAPEX_BUDGET_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CAPEX_BUDGET_MASTER.CODE = (select MIN(TSPL_CAPEX_BUDGET_MASTER.CODE) from TSPL_CAPEX_BUDGET_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_CAPEX_BUDGET_MASTER.CODE = (select Max(TSPL_CAPEX_BUDGET_MASTER.CODE) from TSPL_CAPEX_BUDGET_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_CAPEX_BUDGET_MASTER.CODE = (select Min(TSPL_CAPEX_BUDGET_MASTER.CODE) from TSPL_CAPEX_BUDGET_MASTER where  TSPL_CAPEX_BUDGET_MASTER.CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_CAPEX_BUDGET_MASTER.CODE = (select Max(TSPL_CAPEX_BUDGET_MASTER.CODE) from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_CAPEX_BUDGET_MASTER.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCapexBudget()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.CapexCode = clsCommon.myCstr(dt.Rows(0)("Capex Code"))
            obj.Budget = clsCommon.myCstr(dt.Rows(0)("Budget"))
            obj.RevBudget = clsCommon.myCstr(dt.Rows(0)("Revised Budget"))
            obj.Tolerence = clsCommon.myCdbl(dt.Rows(0)("Tolerence"))
            obj.DocDate = clsCommon.myCstr(dt.Rows(0)("Date"))
            obj.RevNo = clsCommon.myCstr(dt.Rows(0)("Revision No"))
            obj.IncBudget = clsCommon.myCdbl(dt.Rows(0)("Inc Budget"))
            obj.CurrentBudget = clsCommon.myCdbl(dt.Rows(0)("Current_Budget"))
            obj.Provisional = (clsCommon.myCdbl(dt.Rows(0)("Provisional")) = 1)
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsCapexBudget, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsCapexBudget, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim rbudget As String = Nothing
        Dim revno As String = Nothing
        Try

            Dim coll As New Hashtable()
            revno = "0"
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.CapexCode, True)
            clsCommon.AddColumnsForChange(coll, "Budget", obj.Budget)
            clsCommon.AddColumnsForChange(coll, "Revised_Budget", obj.RevBudget)
            clsCommon.AddColumnsForChange(coll, "Tolerence", obj.Tolerence)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Revision_No", clsCommon.myCstr(obj.RevNo))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Inc_Budget", clsCommon.myCstr(obj.IncBudget))
            clsCommon.AddColumnsForChange(coll, "Current_Budget", clsCommon.myCstr(obj.CurrentBudget))
            clsCommon.AddColumnsForChange(coll, "Provisional", IIf(obj.Provisional, 1, 0))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.CAPEXBUDGET, "", "")
                clsCommon.AddColumnsForChange(coll, "CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.CODE= '" & obj.Code & "'"
                Dim check As Integer = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPEX_BUDGET_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If

            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Code), "TSPL_CAPEX_BUDGET_MASTER", "CODE", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPEX_BUDGET_MASTER", OMInsertOrUpdate.Update, "TSPL_CAPEX_BUDGET_MASTER.CODE='" + obj.Code + "'", trans)
            End If
            '' check budget and sub budget limit 
            isSaved = isSaved AndAlso clsCapexMaster.chkLimitBugetMaster(obj.CapexCode, trans)
            '' check sub capex is used in po
            isSaved = isSaved AndAlso clsCapexBudget.CheckPOAmount(obj.Code, trans)
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_CAPEX_BUDGET_MASTER.CODE from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
   

    Public Shared Function BudgetRevised(ByVal Code As String, ByVal RevBudget As Double, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_CAPEX_BUDGET_MASTER.Revised_Budget from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.CODE ='" + Code + "'"
        Dim Budget As Double = clsCommon.myCdbl(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans)))
        If Budget <> RevBudget Then
            Return True
        Else
            Return False
        End If
        Return False
    End Function
    Public Shared Function CheckPOAmount(ByVal StrSubCapexCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim strPoAmount As Decimal = 0
        Dim strBudget As Decimal = 0
        strPoAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(amt_after_tax ) as PO_AMOUNT from tspl_purchase_order_head where Capex_SubCode ='" & StrSubCapexCode & "' ", trans))
        strBudget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where CODE ='" & StrSubCapexCode & "' )AS XX)AS XXX", trans))
        If strPoAmount > strBudget Then
            Throw New Exception(" Budget amount (" & strPoAmount & ") already used in PO for the Capex")
        Else
            Return True
        End If
        Return True
    End Function
    Public Shared Function ChkAcquisitionEntry(ByVal StrSubCapexCode As String) As Boolean
        Dim Count As Decimal = 0

        Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as net_Amt from TSPL_ACQUISITION_DETAIL" & _
                             " left join TSPL_ACQUISITION_head on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_head.Acquisition_Code" & _
                             " where TSPL_ACQUISITION_DETAIL.Capex_SubCode='" & StrSubCapexCode & "' and TSPL_ACQUISITION_head.status=1"))
        If Count > 0 Then
            Return False
        Else
            Return True
        End If
        Return True
    End Function
End Class
