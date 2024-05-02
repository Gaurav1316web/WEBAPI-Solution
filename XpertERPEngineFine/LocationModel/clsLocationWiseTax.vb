Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class clsLocationWiseTax
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Tax_Group_Code As String
    Public Tax_Code As String
    Public TAX_Rate As Double = 0
    Public Is_Default_Tax As Boolean = False
    Public Is_Default_Tax_Group As Boolean = False
    Public Is_Default_Tax_Group_GST As Boolean = False
    Public Tax_Type As String = Nothing ''S-Sale,P-Purchase
    Public Tax_Category As String = Nothing ''L-Local,I-InterState
#End Region

    Public Shared Function SaveData(ByVal strLocationCode As String, ByVal Arr As List(Of clsLocationWiseTax), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsLocationWiseTax In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                clsCommon.AddColumnsForChange(coll, "Tax_Group_Code", obj.Tax_Group_Code)
                clsCommon.AddColumnsForChange(coll, "Tax_Code", obj.Tax_Code)
                clsCommon.AddColumnsForChange(coll, "TAX_Rate", obj.TAX_Rate)
                clsCommon.AddColumnsForChange(coll, "Is_Default_Tax", IIf(obj.Is_Default_Tax, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_Default_Tax_Group", IIf(obj.Is_Default_Tax_Group, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_Default_Tax_Group_GST", IIf(obj.Is_Default_Tax_Group_GST, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Tax_Type", obj.Tax_Type)
                clsCommon.AddColumnsForChange(coll, "Tax_Category", obj.Tax_Category)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_WISE_TAX_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function FinderForMandiTaxGroup(ByRef strTransLocation As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String

        Dim qry As String = "select distinct Tax_Group_Code as Code,Tax_Group_Desc as Description from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from ("
        qry += " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in ('L')"
        qry += " )xxx"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code " &
            "left outer join  TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code  and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' " + Environment.NewLine &
        " where    Tax_Code in (select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y') and 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " + Environment.NewLine +
        " ) xxxx "
        Return clsCommon.ShowSelectForm("mandiTransfer", qry, "Code", "", strCurrCode, "Code", isButtonClicked)
    End Function


    Public Shared Function FinderForVendorServeiceTaxGroup(ByRef strTransLocation As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String

        Dim qry As String = "select distinct Tax_Group_Code as Code,Tax_Group_Desc as Description from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from ("
        qry += " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in ('L')"
        qry += " )xxx"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code " &
            "left outer join  TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code  and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' " + Environment.NewLine &
        " where  2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " + Environment.NewLine +
        " ) xxxx "
        Return clsCommon.ShowSelectForm("VSTaxGroupfnddTransfer", qry, "Code", "", strCurrCode, "Code", isButtonClicked)


    End Function
    Public Shared Function IsMandiTax(ByVal strTaxGroup As String, ByVal tran As SqlTransaction) As Boolean
        Dim Manditax As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & strTaxGroup & "'and Tax_Code in (select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y')", tran))
        If Manditax > 0 Then
            Return True
        Else
            Return False
        End If
        Return True
    End Function
    Public Shared Function IsTaxable(ByVal strFromLocation As String, ByVal strVendorCustomerCodeLocation As String, ByVal tranDate As Date?, ByVal tran As SqlTransaction) As Boolean
        If clsERPFuncationality.GetGSTStatus(tranDate) Then
            Dim qry As String = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code in ('" + strFromLocation + "','" + strVendorCustomerCodeLocation + "') "
            qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                    Return False
                End If
            End If

            dt = Nothing
        End If

        Return True
    End Function
    Public Shared Function TaxType(ByVal strFromLocation As String, ByVal strVendorCustomerCodeLocation As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByVal tran As SqlTransaction) As String
        Dim strType As String = Nothing
        Dim qry As String = Nothing
        If clsERPFuncationality.GetGSTStatus(tranDate) Then
            If clsCommon.CompairString(strTaxType, "T") = CompairStringResult.Equal Then
                qry = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code in ('" + strFromLocation + "','" + strVendorCustomerCodeLocation + "') "
                qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
            Else
                qry = " select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end as LocalOrInterState  from  ( " &
                    "select State   from TSPL_LOCATION_MASTER where Location_Code='" + strFromLocation + "' union all   "
                If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
                    qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCodeLocation + "' "
                ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
                    qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCodeLocation + "' "
                Else
                    Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
                End If
                qry += " )x "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                strType = clsCommon.myCstr(dt.Rows(0)("LocalOrInterState"))
            End If

            dt = Nothing
        End If

        Return strType
    End Function
    Public Shared Function GetTaxGroup(ByVal strLocation As String, ByVal strType As String, ByVal strCategory As String) As DataTable
        Dim qry As String = "select  Tax_Group_Code,MAX(Is_Default_Tax_Group) as Is_Default_Tax_Group,MAX(Is_Default_Tax_Group_GST) as Is_Default_Tax_Group_GST from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" + strLocation + "' and Tax_Type='" + strType + "' and Tax_Category='" + strCategory + "' group by Tax_Group_Code"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Public Shared Function GetTaxWithRate(ByVal strLocation As String, ByVal strGrpCode As String, ByVal strType As String, ByVal strCategory As String) As DataTable
        Dim qry As String = "select Tax_Code,TAX_Rate,Is_Default_Tax from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" + strLocation + "' and  Tax_Group_Code='" + strGrpCode + "' and Tax_Type='" + strType + "' and Tax_Category='" + strCategory + "'"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Public Shared Function FinderForTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean, Optional ByVal Without_State_Condition As Boolean = False) As String
        If clsCommon.myLen(strTransLocation) <= 0 Then
            Throw New Exception("Please first select Transaction location")
        End If
        If clsCommon.myLen(strVendorCustomerCode) <= 0 Then
            Throw New Exception("Please first select Vendor / Customer ")
        End If
        Dim whrCls As String = " and Tax_Type='" + strTaxType + "' "
        Dim whrCls_taxGrp As String = " and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' "
        ''========when no state cond. checked and is for sale then transfer rate also seen on finder
        If Without_State_Condition AndAlso clsCommon.CompairString(strTaxType, "S") = CompairStringResult.Equal Then
            whrCls = " and Tax_Type in ('" + strTaxType + "','T') "
            whrCls_taxGrp = " and TSPL_TAX_GROUP_MASTER.Tax_Group_Type in ('" + strTaxType + "','T')  "
        End If
        '==============================end here===(27/04/2015)===============================================================

        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from ("
        qry += " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Location_Code = '" + strTransLocation + "' " + whrCls + " "
        If Not Without_State_Condition Then ''when false then without state check condition tax finder open all taxes
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "
            If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
                qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
            ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
                qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
            Else
                Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
            End If
            qry += " )x) "
        End If
        qry += " group by Tax_Group_Code"
        qry += " )xxx"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code " + whrCls_taxGrp + " " + Environment.NewLine +
        " where 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " + Environment.NewLine +
        " ) xxxx "
        Return clsCommon.ShowSelectForm("POTaxGroupfndd", qry, "Code", "", strCurrCode, "Code", isButtonClicked)
    End Function
    Public Shared Function FinderForTaxGroupFinance(ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String

        Dim Qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "

        Dim WhrClause As String = "(  select count(TSPL_TAX_GROUP_DETAILS.Tax_Code)  from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_MASTER on " &
         " TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
         "  )=(  select count(TSPL_TAX_GROUP_DETAILS.Tax_Code)  from TSPL_TAX_GROUP_DETAILS left outer join " &
       " TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code where " &
         " TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code   ) and Tax_Group_Type='" & strTaxType & "'" &
        " and 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end)"

        Return clsCommon.ShowSelectForm("TaxGrpSFNDgst", Qry, "Code", WhrClause, strCurrCode, "Code", isButtonClicked)
    End Function
    Public Shared Function FinderForTaxGroupLocationSegment(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        If clsCommon.myLen(strTransLocation) <= 0 Then
            Throw New Exception("Please first select Transaction location")
        End If
        If clsCommon.myLen(strVendorCustomerCode) <= 0 Then
            Throw New Exception("Please first select Vendor / Customer ")
        End If

        Dim strLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(STATE_CODE,'') from TSPL_GL_SEGMENT_CODE where Seg_No ='7' and Segment_code ='" & strTransLocation & "'"))
        Dim strVendCusState As String = String.Empty
        If clsCommon.CompairString(strTaxType, "P") = CompairStringResult.Equal Then
            strVendCusState = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(State_Code,'') from TSPL_VENDOR_MASTER  where Vendor_Code='" & strVendorCustomerCode & "'"))
        Else
            strVendCusState = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(State,'') from TSPL_CUSTOMER_MASTER where Cust_Code='" & strVendorCustomerCode & "'"))
        End If

        If clsCommon.myLen(strLocationState) <= 0 Then
            Throw New Exception("Please enter State Code for Location Segment " & strTransLocation & " ")
        End If

        If clsCommon.myLen(strVendCusState) <= 0 Then
            Throw New Exception("Please enter State Code for Vendor / Customer " & strVendCusState & " ")
        End If
        Dim strTaxselect As String = String.Empty
        If clsCommon.CompairString(strLocationState, strVendCusState) = CompairStringResult.Equal Then
            strTaxselect = "'SGST','CGST','UGST'"
        Else
            strTaxselect = "'IGST'"
        End If

        Dim Qry As String = "select  distinct TSPL_TAX_GROUP_MASTER.Tax_Group_Code as Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER left outer join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code"

        Dim WhrClause As String = "Tax_Code in (select Tax_Code from TSPL_TAX_MASTER where Type in (" & strTaxselect & "))" &
        " and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" & strTaxType & "' and 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " &
        " or (TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted =1 and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" & strTaxType & "')"


        Return clsCommon.ShowSelectForm("TaxGrpSFNDgst", Qry, "Code", WhrClause, strCurrCode, "Code", isButtonClicked)
    End Function

    Public Shared Function FinderForTaxGroupForTransfer(ByVal strTransLocation As String, ByVal strTransLocationTo As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        If clsCommon.myLen(strTransLocation) <= 0 Then
            Throw New Exception("Please first select Transaction From Location")
        End If

        If clsCommon.myLen(strTransLocationTo) <= 0 Then
            Throw New Exception("Please first select Transaction To Location")
        End If

        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from ("
        qry += " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocationTo + "'  "

        'If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
        '    qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
        'ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
        '    qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
        'Else
        '    Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
        'End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"
        qry += " )xxx"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' " + Environment.NewLine &
        " where 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " + Environment.NewLine +
        " ) xxxx "
        Return clsCommon.ShowSelectForm("POTaxGroupfnddTransfer", qry, "Code", "", strCurrCode, "Code", isButtonClicked)

    End Function
    Public Shared Function FinderForTaxRate(ByVal strTransLocation As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, Optional ByVal Without_State_Condition As Boolean = False, Optional ByVal IsLocationSegment As Boolean = False) As Double
        Return FinderForTaxRate(False, strTransLocation, strTaxGroup, strTaxCode, strVendorCustomerCode, strTaxType, Without_State_Condition, IsLocationSegment)
    End Function
    Public Shared Function FinderForTaxRate(ByRef isCancelButtonClicked As Boolean, ByVal strTransLocation As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, Optional ByVal Without_State_Condition As Boolean = False, Optional ByVal IsLocationSegment As Boolean = False) As Double
        isCancelButtonClicked = False
        Dim qry As String
        If IsLocationSegment = True Then
            If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal OrElse clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            Else
                Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
            End If
            qry = "  select Tax_Rate  from TSPL_TAX_RATES where  Tax_Code='" & strTaxCode & "' and Tax_Type='P' and TAX_Code not in('TCS')"
            qry = "select Tax_Rate as Rate from (" & qry & " ) a"
        Else
            Dim whrCls As String = " and Tax_Type='" + strTaxType + "' "
            ''========when no state cond. checked and is for sale then transfer rate also seen on finder
            If Without_State_Condition AndAlso clsCommon.CompairString(strTaxType, "S") = CompairStringResult.Equal Then
                whrCls = " and Tax_Type in ('" + strTaxType + "','T') "
            End If
            '==============================end here===(27/04/2015)===============================================================

            qry = "select Tax_Rate as [Rate]  from ( Select Tax_Rate "
            qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
            qry += " where Tax_Group_Code='" + strTaxGroup + "' and Tax_Code='" + strTaxCode + "' and TAX_Code not in('TCS') and  Location_Code = '" + strTransLocation + "' " + whrCls + "  "
            If Not Without_State_Condition Then
                qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "

                If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
                    qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
                ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
                    qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
                Else
                    Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
                End If
                qry += " )x)"
            End If

            qry += " )xx "

        End If
        Dim strretval As String = clsCommon.ShowSelectForm("FndVendorTaxRate", qry, "Rate", "", "", "", True)
        If clsCommon.myLen(strretval) <= 0 Then
            isCancelButtonClicked = True
        End If

        Return clsCommon.myCdbl(strretval)

    End Function

    Public Shared Function FinderForTaxRateForTransfer(ByVal strTransLocation As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strLocationToCode As String, ByVal strTaxType As String) As Double
        Return FinderForTaxRateForTransfer(False, strTransLocation, strTaxGroup, strTaxCode, strLocationToCode, strTaxType)
    End Function
    Public Shared Function FinderForTaxRateForTransfer(ByRef isCancelButtonClicked As Boolean, ByVal strTransLocation As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strLocationToCode As String, ByVal strTaxType As String) As Double
        isCancelButtonClicked = False
        Dim qry As String = "select Tax_Rate as [Rate]  from ( Select Tax_Rate "
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Tax_Group_Code='" + strTaxGroup + "' and Tax_Code='" + strTaxCode + "' and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all select State   from TSPL_LOCATION_MASTER where Location_Code='" + strLocationToCode + "'   "
        qry += " )x))xx "
        Dim strretval As String = clsCommon.ShowSelectForm("FndVendorTaxRateTransfer", qry, "Rate", "", "", "", True)
        If clsCommon.myLen(strretval) <= 0 Then
            isCancelButtonClicked = True
        End If
        Return clsCommon.myCdbl(strretval)

    End Function

    Public Shared Function GetDefaultTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String) As String
        Return GetDefaultTaxGroup(strTransLocation, strVendorCustomerCode, strTaxType, Nothing)
    End Function

    Public Shared Function GetDefaultTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?) As String
        Dim qry As String = " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where 2=2 and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        If clsERPFuncationality.GetGSTStatus(tranDate) Then
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group_GST=1 "
        Else
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group=1 "
        End If
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "


        If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
            qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
        ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
        Else
            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
        End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Function
    Public Shared Function GetDefaultTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where 2=2 and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        If clsERPFuncationality.GetGSTStatus(tranDate) Then
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group_GST=1 "
        Else
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group=1 "
        End If
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "


        If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
            qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
        ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
        Else
            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
        End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
    Public Shared Function GetExempedDefaultTaxGroup(ByVal IsExempted As Boolean, ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?) As String
        Dim qry As String = " select TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code from TSPL_LOCATION_WISE_TAX_MASTER left outer join " &
            "TSPL_TAX_GROUP_MASTER on TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
            "where Location_Code='" & strTransLocation & "' and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 and Tax_Type='" & strTaxType & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetExempedDefaultTaxGroup(ByVal IsExempted As Boolean, ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code from TSPL_LOCATION_WISE_TAX_MASTER left outer join " &
            "TSPL_TAX_GROUP_MASTER on TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
            "where Location_Code='" & strTransLocation & "' and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 and Tax_Type='" & strTaxType & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetExempedDefaultTaxGroup(ByVal IsExempted As Boolean, ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByVal IsTCS As String) As String
        Dim qry As String = " select TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code from TSPL_LOCATION_WISE_TAX_MASTER left outer join " &
            "TSPL_TAX_GROUP_MASTER on TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
            " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code = TSPL_LOCATION_WISE_TAX_MASTER.Tax_Code " &
            " where Location_Code='" & strTransLocation & "' and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 and Tax_Type='" & strTaxType & "' and isnull(TSPL_TAX_MASTER.Is_TCS,'') = '" & IsTCS & "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function IsValidTaxGroupForTransfer(ByVal strTaxGroup As String, ByVal strFromLocation As String, ByVal strToLocation As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByRef strcheckNonTaxable As Boolean, ByVal tran As SqlTransaction) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.chkGSTTaxGroupValidity, clsFixedParameterCode.chkGSTTaxGroupValidity, tran)) > 0 Then
            If clsERPFuncationality.GetGSTStatus(tranDate) Then
                If clsCommon.myLen(strTaxGroup) > 0 Then
                    Dim qry As String = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code in ('" + strFromLocation + "','" + strToLocation + "') "
                    qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
                    Dim dtLorI As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    Dim dt As DataTable
                    If dtLorI IsNot Nothing AndAlso dtLorI.Rows.Count > 0 Then
                        qry = " Select Tax_Group_Code"
                        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
                        qry += " where TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code='" + strTaxGroup + "' and  Location_Code = '" + strFromLocation + "' and Tax_Type='" + strTaxType + "'  "
                        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in ( '" + dtLorI.Rows(0)("LocalOrInterState") + "') "
                        qry += " group by Tax_Group_Code"
                        dt = clsDBFuncationality.GetDataTable(qry, tran)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Not a valid tax group [" + strTaxGroup + "]")
                        End If

                        qry = "select distinct TSPL_TAX_MASTER.Type from (Select Tax_Code from TSPL_LOCATION_WISE_TAX_MASTER  where Location_Code in ('" + strFromLocation + "','" + strToLocation + "') and Tax_Group_Code='" + strTaxGroup + "'  group by Tax_Code )xx left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=xx.Tax_Code where 2=2 "
                        If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                            Dim intExemptedType As Integer = clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & strTaxGroup & "'")
                            Dim Manditax As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & strTaxGroup & "'and Tax_Code in (select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y')", tran))
                            If intExemptedType = 0 AndAlso Manditax = 0 Then
                                Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be Exempted and Mandi")
                            End If
                        Else
                            qry += " and TSPL_TAX_MASTER.Type in ('IGST')"
                            dt = clsDBFuncationality.GetDataTable(qry, tran)
                            Dim intExemptedType As Integer = clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & strTaxGroup & "'")
                            If dt Is Nothing OrElse dt.Rows.Count <> 1 AndAlso intExemptedType = 0 Then
                                Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be IGST or Tax Group Type should be Exempted.")
                            End If
                        End If
                        'If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                        '    strcheckNonTaxable = True
                        'End If
                    Else
                        Throw New Exception("Not able to decide location wise tax Local Or Inter State")
                    End If
                    dtLorI = Nothing
                    dt = Nothing
                End If
            End If
        End If
        Return True
    End Function
    Public Shared Function IsValidTaxGroupForCSATransferSalePatti(ByVal strTaxGroup As String, ByVal strFromLocation As String, ByVal strToLocation As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByRef strcheckNonTaxable As Boolean, ByVal tran As SqlTransaction) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.chkGSTTaxGroupValidity, clsFixedParameterCode.chkGSTTaxGroupValidity, tran)) > 0 Then
            If clsERPFuncationality.GetGSTStatus(tranDate) Then
                If clsCommon.myLen(strTaxGroup) > 0 Then
                    Dim qry As String = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code in ('" + strFromLocation + "','" + strToLocation + "') "
                    qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
                    Dim dtLorI As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    Dim dt As DataTable
                    If dtLorI IsNot Nothing AndAlso dtLorI.Rows.Count > 0 Then
                        qry = " Select Tax_Group_Code"
                        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
                        qry += " where TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code='" + strTaxGroup + "' and  Location_Code = '" + strFromLocation + "' and Tax_Type='" + strTaxType + "'  "
                        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in ( '" + dtLorI.Rows(0)("LocalOrInterState") + "') "
                        qry += " group by Tax_Group_Code"
                        dt = clsDBFuncationality.GetDataTable(qry, tran)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Not a valid tax group [" + strTaxGroup + "]")
                        End If

                        qry = "select distinct TSPL_TAX_MASTER.Type from (Select Tax_Code from TSPL_LOCATION_WISE_TAX_MASTER  where Location_Code in ('" + strFromLocation + "','" + strToLocation + "') and Tax_Group_Code='" + strTaxGroup + "'  group by Tax_Code )xx left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=xx.Tax_Code where 2=2 "
                        If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtLorI.Rows(0)("Is_GST_UT")) = 1 Then
                                qry += " and TSPL_TAX_MASTER.Type in ('CGST','UGST')"
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                If dt Is Nothing OrElse dt.Rows.Count <> 2 Then
                                    Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be CGST and UGST")
                                End If
                            Else
                                qry += " and TSPL_TAX_MASTER.Type in ('CGST','SGST')"
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                If dt Is Nothing OrElse dt.Rows.Count <> 2 Then
                                    Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be CGST and SGST")
                                End If
                            End If
                        Else
                            qry += " and TSPL_TAX_MASTER.Type in ('IGST')"
                            dt = clsDBFuncationality.GetDataTable(qry, tran)
                            If dt Is Nothing OrElse dt.Rows.Count <> 1 Then
                                Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be IGST")
                            End If
                        End If
                        'If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                        '    strcheckNonTaxable = True
                        'End If
                    Else
                        Throw New Exception("Not able to decide location wise tax Local Or Inter State")
                    End If
                    dtLorI = Nothing
                    dt = Nothing
                End If
            End If
        End If
        Return True
    End Function
    Public Shared Function IsValidTaxGroup(ByVal strTaxGroup As String, ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal tranDate As Date?, ByVal tran As SqlTransaction) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.chkGSTTaxGroupValidity, clsFixedParameterCode.chkGSTTaxGroupValidity, tran)) > 0 Then
            If clsERPFuncationality.GetGSTStatus(tranDate) Then
                Dim qry As String = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "'  union all"
                If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
                    qry += "  select State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
                ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
                    qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
                Else
                    Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
                End If
                qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
                Dim dtLorI As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                Dim dt As DataTable
                If dtLorI IsNot Nothing AndAlso dtLorI.Rows.Count > 0 Then
                    qry = " Select TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code,max(TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted) as Is_Tax_Exempted" + Environment.NewLine +
                    " from TSPL_LOCATION_WISE_TAX_MASTER " + Environment.NewLine +
                    " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Type=TSPL_TAX_GROUP_MASTER.Tax_Group_Type " + Environment.NewLine +
                    " where TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code='" + strTaxGroup + "' and  TSPL_LOCATION_WISE_TAX_MASTER.Location_Code = '" + strTransLocation + "' and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Type='" + strTaxType + "'" + Environment.NewLine +
                    " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in ( '" + dtLorI.Rows(0)("LocalOrInterState") + "') " + Environment.NewLine +
                    " group by TSPL_LOCATION_WISE_TAX_MASTER.Tax_Group_Code"
                    dt = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Not a valid tax group [" + strTaxGroup + "]")
                    End If
                    qry = "select distinct TSPL_TAX_MASTER.Type from (Select Tax_Code from TSPL_LOCATION_WISE_TAX_MASTER  where Location_Code='" + strTransLocation + "' and Tax_Group_Code='" + strTaxGroup + "'  group by Tax_Code )xx left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=xx.Tax_Code where 2=2 "
                    If clsCommon.myCdbl(dt.Rows(0)("Is_Tax_Exempted")) <= 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then

                            If clsCommon.myCdbl(dtLorI.Rows(0)("Is_GST_UT")) = 1 Then
                                qry += " and TSPL_TAX_MASTER.Type in ('UGST')"
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                If dt Is Nothing OrElse dt.Rows.Count <> 1 Then
                                    Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be CGST and UGST")
                                End If
                            Else
                                qry += " and TSPL_TAX_MASTER.Type in ('SGST')"
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                If dt Is Nothing OrElse dt.Rows.Count <> 1 Then
                                    Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be CGST and SGST")
                                End If
                            End If
                        Else
                            qry += " and TSPL_TAX_MASTER.Type in ('IGST')"
                            dt = clsDBFuncationality.GetDataTable(qry, tran)
                            If dt Is Nothing OrElse dt.Rows.Count <> 1 Then
                                Throw New Exception("Tax Group : " + strTaxGroup + ".Tax Authority type should be IGST")
                            End If
                        End If
                    End If
                Else
                    Throw New Exception("Not able to decide location wise tax Local Or Inter State")
                End If
                dtLorI = Nothing
                dt = Nothing
            End If
        End If
        Return True
    End Function
    '===============Added by preeti gupta =================
    Public Shared Function GetDefaultTaxGroupforTransfer(ByVal strTransLocation As String, ByVal strTransToLocation As String, ByVal strTaxType As String) As String
        Return GetDefaultTaxGroup(strTransLocation, strTransToLocation, strTaxType, Nothing)
    End Function

    Public Shared Function GetDefaultTaxGroupforTransfer(ByVal strTransLocation As String, ByVal strTransToLocation As String, ByVal strTaxType As String, ByVal tranDate As Date?) As String
        Dim qry As String = " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where 2=2 and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        If clsERPFuncationality.GetGSTStatus(tranDate) Then
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group_GST=1 "
        Else
            qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group=1 "
        End If
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "


        If clsCommon.CompairString("T", strTaxType) = CompairStringResult.Equal Then
            qry += "  select   State from tspl_location_master where Location_Code='" + strTransToLocation + "' "

        Else
            Throw New Exception("Please enter valid Tax Type it should be 'T'")
        End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Function
    '=======================================================
End Class