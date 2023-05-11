Imports common
Imports System.Data.SqlClient
Public Class clsRemittance
#Region "Variables"
    Public Remittance_Code As String = Nothing
    Public Remit_TDS As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Document_Type As String = Nothing
    Public Document_Amount As Double = 0
    Public Service_Type As String = Nothing
    Public Actual_TDS_Base As Double = 0
    Public Calculated_TDS_Base As Double = 0
    Public TDS_Per As Double = 0
    Public Actual_TDS As Double = 0
    Public Calculated_TDS As Double = 0
    Public Surcharge_Per As Double = 0
    Public Actual_Surcharge As Double = 0
    Public Calculated_Surcharge As Double = 0
    Public Edu_Cess_Per As Double = 0
    Public Actual_Edu_Cess As Double = 0
    Public Calculated_Edu_Cess As Double = 0
    Public Sec_Educess_Per As Double = 0
    Public Actual_Sec_Educess As Double = 0
    Public Calculated_Sec_Educess As Double = 0
    Public Actual_Total_TDS As Double = 0
    Public Calculated_Total_TDS As Double = 0
    Public Section_Code As String = Nothing
    Public Section_Description As String = Nothing
    Public Branch_Code As String = Nothing
    Public Deduction_Code As String = Nothing
    Public Select_By As String = Nothing
    Public Quarter As String = Nothing
    Public Fiscal_Year As String = Nothing
    Public Branch_GL_AC As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public Previous_TDS_Amt As Double = 0
    'Public Include_Tax As String = Nothing

    Public IsTDSOverride As Boolean = False 'Not a table column
    Public IsApplyTDS As Boolean = True 'Not a table column
#End Region
    Public Shared Function SaveData(ByVal obj As clsRemittance, ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As Boolean
        Return SaveData(obj, strDocumentNo, "", trans)
    End Function


    Public Shared Function ValidateTDS(ByVal Document_Date As DateTime, ByVal Vendor_Code As String, ByVal Deduction_Code As String, ByVal trans As SqlTransaction) As Boolean
        If objCommonVar.TDSValidationFrom IsNot Nothing Then
            If Document_Date >= objCommonVar.TDSValidationFrom Then
                Dim dtNatureOfDeduction As DataTable = clsDBFuncationality.GetDataTable("select IsBuyerFileReturnInLastTwoYears,IsTCS_TDSAmountGreaterThan50KPreviousYear from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + Deduction_Code + "'", trans)
                Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable("select IsBuyerFileReturnInLastTwoYears,IsTCS_TDSAmountGreaterThan50KPreviousYear from TSPL_VENDOR_MASTER where Vendor_Code='" + Vendor_Code + "'", trans)
                If dtNatureOfDeduction IsNot Nothing AndAlso dtVendor IsNot Nothing Then
                    If clsCommon.myCdbl(dtNatureOfDeduction.Rows(0)("IsBuyerFileReturnInLastTwoYears")) <> clsCommon.myCdbl(dtVendor.Rows(0)("IsBuyerFileReturnInLastTwoYears")) Then
                        Throw New Exception("Vendor [" + Vendor_Code + "] Nature of Deduction [" + Deduction_Code + "] [Buyer File Return In Last Two Years] Should be same")
                    End If
                    If clsCommon.myCdbl(dtNatureOfDeduction.Rows(0)("IsTCS_TDSAmountGreaterThan50KPreviousYear")) <> clsCommon.myCdbl(dtVendor.Rows(0)("IsTCS_TDSAmountGreaterThan50KPreviousYear")) Then
                        Throw New Exception("Vendor [" + Vendor_Code + "] Nature of Deduction [" + Deduction_Code + "] [TCS TDS Amount Greater Than 50K Previous Year] Should be same")
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsRemittance, ByVal strDocumentNo As String, ByVal strLocSegCode As String, ByVal trans As SqlTransaction) As Boolean

        If obj IsNot Nothing Then

            clsRemittance.ValidateTDS(obj.Document_Date, obj.Vendor_Code, obj.Deduction_Code, trans)

            Dim strDocNo As String = ""
            Dim qry As String = "select MAX(Remittance_Code) as DocNo from TSPL_REMITTANCE"
            Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If (clsCommon.myLen(strCode) <= 0) Then
                strCode = "REM000001"
            Else
                strCode = clsCommon.incval(strCode)
            End If
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Error in Code Generation of Remittance")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remittance_Code", strCode)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "Document_No", strDocumentNo)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
            clsCommon.AddColumnsForChange(coll, "Actual_TDS_Base", obj.Actual_TDS_Base)
            clsCommon.AddColumnsForChange(coll, "Calculated_TDS_Base", obj.Calculated_TDS_Base)
            clsCommon.AddColumnsForChange(coll, "Actual_TDS", obj.Actual_TDS)
            clsCommon.AddColumnsForChange(coll, "Calculated_TDS", obj.Calculated_TDS)
            clsCommon.AddColumnsForChange(coll, "Actual_Surcharge", obj.Actual_Surcharge)
            clsCommon.AddColumnsForChange(coll, "Calculated_Surcharge", obj.Calculated_Surcharge)
            clsCommon.AddColumnsForChange(coll, "Actual_Edu_Cess", obj.Actual_Edu_Cess)
            clsCommon.AddColumnsForChange(coll, "Calculated_Edu_Cess", obj.Calculated_Edu_Cess)
            clsCommon.AddColumnsForChange(coll, "Actual_Sec_Educess", obj.Actual_Sec_Educess)
            clsCommon.AddColumnsForChange(coll, "Calculated_Sec_Educess", obj.Calculated_Sec_Educess)
            clsCommon.AddColumnsForChange(coll, "Actual_Total_TDS", Math.Round(obj.Actual_Total_TDS, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "Calculated_Total_TDS", obj.Calculated_Total_TDS)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Year", obj.Fiscal_Year)
            clsCommon.AddColumnsForChange(coll, "Quarter", obj.Quarter)

            '==========================
            clsCommon.AddColumnsForChange(coll, "Previous_TDS_Amt", obj.Previous_TDS_Amt)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            'obj.Branch_GL_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Account from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + obj.Branch_Code + "'", trans))
            obj.Branch_GL_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD Where Deduction_Code='" + obj.Deduction_Code + "'", trans)) '-- replaced By---Pankaj Kumar
            If clsCommon.myLen(strLocSegCode) > 0 Then
                obj.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Branch_GL_AC, strLocSegCode, True, trans)
            End If
            clsCommon.AddColumnsForChange(coll, "Branch_GL_AC", obj.Branch_GL_AC, True)
            clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)

            Dim SectionCode As String = clsDBFuncationality.getSingleValue("select TDS_Section  from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" + obj.Deduction_Code + "'", trans)
            Dim SectionDeccription As String = clsDBFuncationality.getSingleValue("select Description  from TSPL_TDS_SECTION_MASTER where TDS_Group ='" + SectionCode + "'", trans)
            'If clsCommon.myLen(SectionCode) > 0 Then
            '    clsDBFuncationality.ExecuteNonQuery("update TSPL_REMITTANCE set Section_Code ='" + SectionCode + "' where Document_No ='" + obj.Document_No + "' ", trans)
            '    clsDBFuncationality.ExecuteNonQuery("update TSPL_REMITTANCE set Section_Description ='" + SectionDeccription + "' where Document_No ='" + obj.Document_No + "' ", trans)
            'Else
            clsCommon.AddColumnsForChange(coll, "Section_Code", SectionCode)
            clsCommon.AddColumnsForChange(coll, "Section_Description", SectionDeccription)
            'End If
            '===========================


            clsCommon.AddColumnsForChange(coll, "Select_By", obj.Select_By)
            clsCommon.AddColumnsForChange(coll, "TDS_Per", obj.TDS_Per)
            clsCommon.AddColumnsForChange(coll, "Surcharge_Per", obj.Surcharge_Per)
            clsCommon.AddColumnsForChange(coll, "Edu_Cess_Per", obj.Edu_Cess_Per)
            clsCommon.AddColumnsForChange(coll, "Sec_Educess_Per", obj.Sec_Educess_Per)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)

            clsCommon.AddColumnsForChange(coll, "Is_TDS_Override", IIf(obj.IsTDSOverride, 1, 0))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REMITTANCE", OMInsertOrUpdate.Insert, "", trans)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String) As clsRemittance
        Return GetData(strDocumentCode, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal Trans As SqlTransaction) As clsRemittance
        Dim obj As clsRemittance = Nothing
        Dim qry As String = "Select * from TSPL_REMITTANCE where Document_No='" + strDocumentCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRemittance()
            obj.Remittance_Code = clsCommon.myCstr(dt.Rows(0)("Remittance_Code"))
            obj.Remit_TDS = clsCommon.myCstr(dt.Rows(0)("Remit_TDS"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))
            obj.Service_Type = clsCommon.myCstr(dt.Rows(0)("Service_Type"))
            obj.Actual_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS_Base"))
            obj.Calculated_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("Calculated_TDS_Base"))
            obj.Actual_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS"))
            obj.Calculated_TDS = clsCommon.myCdbl(dt.Rows(0)("Calculated_TDS"))
            obj.Actual_Surcharge = clsCommon.myCdbl(dt.Rows(0)("Actual_Surcharge"))
            obj.Calculated_Surcharge = clsCommon.myCdbl(dt.Rows(0)("Calculated_Surcharge"))
            obj.Actual_Edu_Cess = clsCommon.myCdbl(dt.Rows(0)("Actual_Edu_Cess"))
            obj.Calculated_Edu_Cess = clsCommon.myCdbl(dt.Rows(0)("Calculated_Edu_Cess"))
            obj.Actual_Sec_Educess = clsCommon.myCdbl(dt.Rows(0)("Actual_Sec_Educess"))
            obj.Calculated_Sec_Educess = clsCommon.myCdbl(dt.Rows(0)("Calculated_Sec_Educess"))
            obj.Actual_Total_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_Total_TDS"))
            obj.Calculated_Total_TDS = clsCommon.myCdbl(dt.Rows(0)("Calculated_Total_TDS"))
            obj.Fiscal_Year = clsCommon.myCstr(dt.Rows(0)("Fiscal_Year"))
            obj.Quarter = clsCommon.myCstr(dt.Rows(0)("Quarter"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Section_Description = clsCommon.myCstr(dt.Rows(0)("Section_Description"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Branch_GL_AC = clsCommon.myCstr(dt.Rows(0)("Branch_GL_AC"))
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.TDS_Per = clsCommon.myCdbl(dt.Rows(0)("TDS_Per"))
            obj.Surcharge_Per = clsCommon.myCdbl(dt.Rows(0)("Surcharge_Per"))
            obj.Edu_Cess_Per = clsCommon.myCdbl(dt.Rows(0)("Edu_Cess_Per"))
            obj.Sec_Educess_Per = clsCommon.myCstr(dt.Rows(0)("Sec_Educess_Per"))
            obj.Select_By = clsCommon.myCstr(dt.Rows(0)("Select_By"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Previous_TDS_Amt = clsCommon.myCdbl(dt.Rows(0)("Previous_TDS_Amt"))
            obj.IsTDSOverride = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_TDS_Override")) = 1, True, False)

            If (clsCommon.myCdbl(obj.Actual_TDS) > 0) Then
                obj.IsApplyTDS = True
            Else
                obj.IsApplyTDS = False
            End If
        End If
        Return obj
    End Function

    Public Shared Function GetDataForCreateRemittance(ByVal strFilterBy As String, ByVal strFrom As String, ByVal strTo As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal strBranchCode As String, ByVal strSectionCode As String, ByVal isCompanyWise As Boolean) As DataTable
        ''Dim qry As String = "select cast(0 as bit) as [Post],Document_No as [Document No],Document_Date as [Document Date],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Document_Amount as [Document Amount],Actual_TDS_Base as [Actual TDS Base],Calculated_TDS_Base as [Calculated TDS Base],TDS_Per as [TDS %],Actual_TDS as [Actual TDS],Calculated_TDS as [Calculated TDS],Surcharge_Per as [Surcharge %],Actual_Surcharge as [Actual Surcharge],Calculated_Surcharge as [Calculated Surcharge],Edu_Cess_Per as [Edu Cess %],Actual_Edu_Cess as [Actual Edu Cess],Calculated_Edu_Cess as [Calculated Edu Cess],Sec_Educess_Per as [Sec Educess %],Actual_Sec_Educess as [Actual Sec Educess],Calculated_Sec_Educess as [Calculated Sec Educess],Actual_Total_TDS as [Actual Total TDS],Calculated_Total_TDS as [Calculated Total TDS],Section_Code as [Section Code],Section_Description as [Section Description],Branch_Code as [Branch Code],case when Select_By='C' then 'Company' else 'Non Company' end as [Select By],Fiscal_Year as [Fiscal Year],Quarter,Deduction_Code as [Deduction Code],Remittance_Code as [Code]  from Tspl_remittance where 2=2 and Remit_TDS='N' and LEN(RTRIM(ISNULL(Deduction_Code,'')))>0 "
        Dim whrClas As String = ""
        Dim whrDate As String = ""
        If clsCommon.myLen(strBranchCode) > 0 Then
            whrClas += " and Branch_Code='" + strBranchCode + "' "
        End If

        If clsCommon.myLen(strSectionCode) > 0 Then
            whrClas += " and Section_Code='" + strSectionCode + "' "
        End If



        If clsCommon.CompairString("Vendor", strFilterBy) = CompairStringResult.Equal Then
            If (clsCommon.myLen(strFrom) > 0) Then
                whrClas += " and Vendor_Code>='" + strFrom + "' "
            End If
            If (clsCommon.myLen(strTo) > 0) Then
                whrClas += " and Vendor_Code<='" + strTo + "' "
            End If
        ElseIf clsCommon.CompairString("Document No", strFilterBy) = CompairStringResult.Equal Then
            If (clsCommon.myLen(strFrom) > 0) Then
                whrClas += " and Document_No>='" + strFrom + "' "
            End If
            If (clsCommon.myLen(strTo) > 0) Then
                whrClas += " and Document_No<='" + strTo + "' "
            End If
        ElseIf clsCommon.CompairString("Document Date", strFilterBy) = CompairStringResult.Equal Then
            strFrom = clsCommon.GetPrintDate(FromDate, "dd/MM/yyyy")
            strTo = clsCommon.GetPrintDate(ToDate, "dd/MM/yyyy")
            whrDate += " and convert(date,Document_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,Document_Date,103)<= convert(date,'" + ToDate + "',103)"
        ElseIf clsCommon.CompairString("Nature Of Deduction", strFilterBy) = CompairStringResult.Equal Then
            If (clsCommon.myLen(strFrom) > 0) Then
                whrClas += " and Deduction_Code>='" + strFrom + "' "
            End If
            If (clsCommon.myLen(strTo) > 0) Then
                whrClas += " and Deduction_Code<='" + strTo + "' "
            End If
        End If
        If (isCompanyWise) Then
            whrClas += " and Select_By='C'"
        Else
            whrClas += " and Select_By='N'"
        End If
        whrClas += " and Remit_TDS='N'"
        Dim qry As String = "  select cast(0 as bit) as [Post],Tspl_remittance.Document_No as [Document No],Tspl_remittance.Document_Date as [Document Date],Tspl_remittance.Vendor_Code as [Vendor Code],Tspl_remittance.Vendor_Name as [Vendor Name],Tspl_remittance.Document_Amount as [Document Amount],Tspl_remittance.Actual_TDS_Base as [Actual TDS Base],Tspl_remittance.Calculated_TDS_Base as [Calculated TDS Base],Tspl_remittance.TDS_Per as [TDS %],Tspl_remittance.Actual_TDS as [Actual TDS],Tspl_remittance.Calculated_TDS as [Calculated TDS],Tspl_remittance.Surcharge_Per as [Surcharge %],Tspl_remittance.Actual_Surcharge as [Actual Surcharge],Tspl_remittance.Calculated_Surcharge as [Calculated Surcharge],Tspl_remittance.Edu_Cess_Per as [Edu Cess %],Tspl_remittance.Actual_Edu_Cess as [Actual Edu Cess],Tspl_remittance.Calculated_Edu_Cess as [Calculated Edu Cess],Tspl_remittance.Sec_Educess_Per as [Sec Educess %],Tspl_remittance.Actual_Sec_Educess as [Actual Sec Educess],Tspl_remittance.Calculated_Sec_Educess as [Calculated Sec Educess],xxxxx.Actual_Total_TDS as [Actual Total TDS],Tspl_remittance.Calculated_Total_TDS as [Calculated Total TDS],Tspl_remittance.Section_Code as [Section Code],Tspl_remittance.Section_Description as [Section Description],Tspl_remittance.Branch_Code as [Branch Code],case when Tspl_remittance.Select_By='C' then 'Company' else 'Non Company' end as [Select By],Tspl_remittance.Fiscal_Year as [Fiscal Year],Tspl_remittance.Quarter,Tspl_remittance.Deduction_Code as [Deduction Code],Tspl_remittance.Remittance_Code as [Code],Tspl_remittance.Vendor_Invoice_No as [Vendor Invoice No] ,'' as AllRemitCode "
        qry += " from( "
        qry += " select xxxx.Document_No,SUM(Actual_Total_TDS*RI) as Actual_Total_TDS from ("
        qry += " select Document_No ,Actual_Total_TDS,1 as RI"
        qry += " from Tspl_remittance "
        qry += " where 2=2 " + whrClas + " " + whrDate + " and Document_Type in ('I','A','O') "
        qry += " union all "
        qry += " select case when LEN(ISNULL(RefDocNo,''))>0 then RefDocNo else Document_No end  as Document_No,Actual_Total_TDS,RI    "
        qry += " from("
        qry += " select  Document_No,(select top 1 RefDocNo from  TSPL_VENDOR_INVOICE_HEAD where Document_No=Tspl_remittance.Document_No) as RefDocNo,Actual_Total_TDS ,case when Document_Type='C' then 1 else -1 end as RI"
        qry += " from Tspl_remittance "
        qry += " where 2=2  " + whrClas + " " + whrDate + " and Document_Type   in ('D','C')"
        qry += " )xxx"
        qry += " )xxxx group by Document_No"
        qry += " )xxxxx left outer join Tspl_remittance on Tspl_remittance.Document_No= xxxxx.Document_No where LEN(ISNULL( Tspl_remittance.Document_No,''))>0 Order by Tspl_remittance.Remittance_Code"

        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Public Shared Function PostRemit(ByVal arr As List(Of String), ByVal strDrNodeAgainstAP As String) As Boolean

        If arr Is Nothing AndAlso arr.Count <= 0 Then
            Throw New Exception("No Remit is selected to Post")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strMainCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.CreateRemmitance, "", "")
            If clsCommon.myLen(strMainCode) <= 0 Then
                Throw New Exception("Error in Code Generation")
            End If

            Dim qry As String = "Update TSPL_REMITTANCE set Remit_TDS='Y',Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "',Remittance_Main_Code='" + strMainCode + "' where Remittance_Code in (" + clsCommon.GetMulcallString(arr) + " " + strDrNodeAgainstAP + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDataForMainRemittance(ByVal strMainRemitCode As String) As List(Of clsRemittance)
        Dim arr As List(Of clsRemittance) = Nothing
        Dim qry As String = "  select Tspl_remittance.Remittance_Code,Tspl_remittance.Remit_TDS,Tspl_remittance.Vendor_Code,Tspl_remittance.Vendor_Name ,Tspl_remittance.Document_No  ,Tspl_remittance.Document_Date, Tspl_remittance.Document_Type,Tspl_remittance.Document_Amount,Tspl_remittance.Service_Type,Tspl_remittance.Actual_TDS_Base,Tspl_remittance.Calculated_TDS_Base,Tspl_remittance.Actual_TDS,Tspl_remittance.Calculated_TDS,Tspl_remittance.Actual_Surcharge,Tspl_remittance.Calculated_Surcharge,Tspl_remittance.Actual_Edu_Cess,Tspl_remittance.Calculated_Edu_Cess,Tspl_remittance.Actual_Sec_Educess,Tspl_remittance.Calculated_Sec_Educess,xxxxx.Actual_Total_TDS,Tspl_remittance.Calculated_Total_TDS,Tspl_remittance.Fiscal_Year,Tspl_remittance.Quarter,Tspl_remittance.Section_Code,Tspl_remittance.Section_Description,Tspl_remittance.Branch_Code,Tspl_remittance.Branch_GL_AC,Tspl_remittance.Deduction_Code,Tspl_remittance.TDS_Per,Tspl_remittance.Surcharge_Per,Tspl_remittance.Edu_Cess_Per,Tspl_remittance.Sec_Educess_Per,Tspl_remittance.Select_By,Tspl_remittance.Vendor_Invoice_No  from("
        qry += " select xxxx.Document_No,SUM(Actual_Total_TDS*RI) as Actual_Total_TDS from ("
        qry += " select Document_No ,Actual_Total_TDS,1 as RI from Tspl_remittance  where 2=2  and TSPL_REMITTANCE.Remittance_Main_Code='" + strMainRemitCode + "'"
        qry += " and Document_Type   in ('I','AV')  "
        qry += " union all "
        qry += " select case when LEN(ISNULL(RefDocNo,''))>0 then RefDocNo else Document_No end  as Document_No,Actual_Total_TDS,RI     from( select  Document_No,(select top 1 RefDocNo from  TSPL_VENDOR_INVOICE_HEAD where Document_No=Tspl_remittance.Document_No) as RefDocNo,Actual_Total_TDS ,case when Document_Type='C' then 1 else  -1 end as RI from Tspl_remittance  where 2=2   and TSPL_REMITTANCE.Remittance_Main_Code='" + strMainRemitCode + "' and Document_Type   in ('C','D') )xxx "
        qry += " )xxxx group by Document_No having SUM(Actual_Total_TDS*RI)<>0"
        qry += " )xxxxx left outer join Tspl_remittance on Tspl_remittance.Document_No= xxxxx.Document_No Order by Tspl_remittance.Remittance_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsRemittance)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsRemittance = New clsRemittance()
                objTr.Remittance_Code = clsCommon.myCstr(dr("Remittance_Code"))
                objTr.Remit_TDS = clsCommon.myCstr(dr("Remit_TDS"))
                objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Document_Date = clsCommon.myCstr(dr("Document_Date"))
                objTr.Document_Type = clsCommon.myCstr(dr("Document_Type"))
                objTr.Document_Amount = clsCommon.myCdbl(dr("Document_Amount"))
                objTr.Service_Type = clsCommon.myCstr(dr("Service_Type"))
                objTr.Actual_TDS_Base = clsCommon.myCdbl(dr("Actual_TDS_Base"))
                objTr.Calculated_TDS_Base = clsCommon.myCdbl(dr("Calculated_TDS_Base"))
                objTr.Actual_TDS = clsCommon.myCdbl(dr("Actual_TDS"))
                objTr.Calculated_TDS = clsCommon.myCdbl(dr("Calculated_TDS"))
                objTr.Actual_Surcharge = clsCommon.myCdbl(dr("Actual_Surcharge"))
                objTr.Calculated_Surcharge = clsCommon.myCdbl(dr("Calculated_Surcharge"))
                objTr.Actual_Edu_Cess = clsCommon.myCdbl(dr("Actual_Edu_Cess"))
                objTr.Calculated_Edu_Cess = clsCommon.myCdbl(dr("Calculated_Edu_Cess"))
                objTr.Actual_Sec_Educess = clsCommon.myCdbl(dr("Actual_Sec_Educess"))
                objTr.Calculated_Sec_Educess = clsCommon.myCdbl(dr("Calculated_Sec_Educess"))
                objTr.Actual_Total_TDS = clsCommon.myCdbl(dr("Actual_Total_TDS"))
                objTr.Calculated_Total_TDS = clsCommon.myCdbl(dr("Calculated_Total_TDS"))
                objTr.Fiscal_Year = clsCommon.myCstr(dr("Fiscal_Year"))
                objTr.Quarter = clsCommon.myCstr(dr("Quarter"))
                objTr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objTr.Section_Description = clsCommon.myCstr(dr("Section_Description"))
                objTr.Branch_Code = clsCommon.myCstr(dr("Branch_Code"))
                objTr.Branch_GL_AC = clsCommon.myCstr(dr("Branch_GL_AC"))
                objTr.Deduction_Code = clsCommon.myCstr(dr("Deduction_Code"))
                objTr.TDS_Per = clsCommon.myCdbl(dr("TDS_Per"))
                objTr.Surcharge_Per = clsCommon.myCdbl(dr("Surcharge_Per"))
                objTr.Edu_Cess_Per = clsCommon.myCdbl(dr("Edu_Cess_Per"))
                objTr.Sec_Educess_Per = clsCommon.myCstr(dr("Sec_Educess_Per"))
                objTr.Select_By = clsCommon.myCstr(dr("Select_By"))
                objTr.Vendor_Invoice_No = clsCommon.myCstr(dr("Vendor_Invoice_No"))
                objTr.IsTDSOverride = Not (objTr.Actual_TDS_Base = objTr.Calculated_TDS_Base)
                If (clsCommon.myLen(objTr.Deduction_Code) > 0) Then
                    objTr.IsApplyTDS = True
                Else
                    objTr.IsApplyTDS = False
                End If
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function


    Public Shared Function Convert(ByVal objPIRemittance As clsPIRemittance, ByRef dblPreviousTDSAmt As Decimal) As clsRemittance
        Dim objRemittance As clsRemittance = Nothing
        If objPIRemittance IsNot Nothing Then
            objRemittance = New clsRemittance()
            objRemittance.Vendor_Code = objPIRemittance.Vendor_Code
            objRemittance.Vendor_Name = objPIRemittance.Vendor_Name
            objRemittance.Document_No = objPIRemittance.Document_No
            objRemittance.Document_Date = objPIRemittance.Document_Date
            objRemittance.Document_Type = objPIRemittance.Document_Type
            objRemittance.Document_Amount = objPIRemittance.Document_Amount
            objRemittance.Service_Type = objPIRemittance.Service_Type
            objRemittance.Actual_TDS_Base = objPIRemittance.Actual_TDS_Base
            objRemittance.Calculated_TDS_Base = objPIRemittance.Calculated_TDS_Base
            objRemittance.Actual_TDS = objPIRemittance.Actual_TDS
            objRemittance.Calculated_TDS = objPIRemittance.Calculated_TDS
            objRemittance.Actual_Surcharge = objPIRemittance.Actual_Surcharge
            objRemittance.Calculated_Surcharge = objPIRemittance.Calculated_Surcharge
            objRemittance.Actual_Edu_Cess = objPIRemittance.Actual_Edu_Cess
            objRemittance.Calculated_Edu_Cess = objPIRemittance.Calculated_Edu_Cess
            objRemittance.Actual_Sec_Educess = objPIRemittance.Actual_Sec_Educess
            objRemittance.Calculated_Sec_Educess = objPIRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objPIRemittance.Actual_Total_TDS
            objRemittance.Calculated_Total_TDS = objPIRemittance.Calculated_Total_TDS
            objRemittance.Fiscal_Year = objPIRemittance.Fiscal_Year
            objRemittance.Quarter = objPIRemittance.Quarter
            objRemittance.Section_Code = objPIRemittance.Section_Code
            objRemittance.Section_Description = objPIRemittance.Section_Description
            objRemittance.Branch_Code = objPIRemittance.Branch_Code
            objRemittance.Deduction_Code = objPIRemittance.Deduction_Code
            objRemittance.TDS_Per = objPIRemittance.TDS_Per
            objRemittance.Surcharge_Per = objPIRemittance.Surcharge_Per
            objRemittance.Edu_Cess_Per = objPIRemittance.Edu_Cess_Per
            objRemittance.Sec_Educess_Per = objPIRemittance.Sec_Educess_Per
            objRemittance.Select_By = objPIRemittance.Select_By
            objRemittance.IsTDSOverride = objPIRemittance.IsTDSOverride
            objRemittance.IsApplyTDS = objPIRemittance.IsApplyTDS

            dblPreviousTDSAmt = objPIRemittance.Previous_TDS_Amt
            'End If
        End If
        Return objRemittance
    End Function
End Class

Public Class clsPIRemittance
#Region "Variables"
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Document_Type As String = Nothing
    Public Document_Amount As Double = 0
    Public Service_Type As String = Nothing
    Public Actual_TDS_Base As Double = 0
    Public Calculated_TDS_Base As Double = 0
    Public TDS_Per As Double = 0
    Public Actual_TDS As Double = 0
    Public Calculated_TDS As Double = 0
    Public Surcharge_Per As Double = 0
    Public Actual_Surcharge As Double = 0
    Public Calculated_Surcharge As Double = 0
    Public Edu_Cess_Per As Double = 0
    Public Actual_Edu_Cess As Double = 0
    Public Calculated_Edu_Cess As Double = 0
    Public Sec_Educess_Per As Double = 0
    Public Actual_Sec_Educess As Double = 0
    Public Calculated_Sec_Educess As Double = 0
    Public Actual_Total_TDS As Double = 0
    Public Calculated_Total_TDS As Double = 0
    Public Section_Code As String = Nothing
    Public Section_Description As String = Nothing
    Public Branch_Code As String = Nothing
    Public Deduction_Code As String = Nothing
    Public Select_By As String = Nothing
    Public Quarter As String = Nothing
    Public Fiscal_Year As String = Nothing
    Public IsTDSOverride As Boolean = False 'Not a table column
    Public IsApplyTDS As Boolean = True 'Not a table column
    Public Previous_TDS_Amt As Double = 0
#End Region


    Public Shared Function SaveData(ByVal obj As clsPIRemittance, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        If obj IsNot Nothing Then
            clsRemittance.ValidateTDS(obj.Document_Date, obj.Vendor_Code, obj.Deduction_Code, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "Document_No", strDocumentNo)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(dtDocumentDate, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "Service_Type", obj.Service_Type)
            clsCommon.AddColumnsForChange(coll, "Actual_TDS_Base", obj.Actual_TDS_Base)
            clsCommon.AddColumnsForChange(coll, "Calculated_TDS_Base", obj.Calculated_TDS_Base)
            clsCommon.AddColumnsForChange(coll, "Actual_TDS", Math.Round(obj.Actual_TDS, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "Calculated_TDS", obj.Calculated_TDS)
            clsCommon.AddColumnsForChange(coll, "Actual_Surcharge", Math.Round(obj.Actual_Surcharge, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "Calculated_Surcharge", obj.Calculated_Surcharge)
            clsCommon.AddColumnsForChange(coll, "Actual_Edu_Cess", Math.Round(obj.Actual_Edu_Cess, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "Calculated_Edu_Cess", obj.Calculated_Edu_Cess)
            clsCommon.AddColumnsForChange(coll, "Actual_Sec_Educess", Math.Round(obj.Actual_Sec_Educess, 0, MidpointRounding.AwayFromZero))
            clsCommon.AddColumnsForChange(coll, "Calculated_Sec_Educess", obj.Calculated_Sec_Educess)

            obj.Actual_Total_TDS = Math.Round(obj.Actual_TDS, 0, MidpointRounding.AwayFromZero) + Math.Round(obj.Actual_Surcharge, 0, MidpointRounding.AwayFromZero) + Math.Round(obj.Actual_Edu_Cess, 0, MidpointRounding.AwayFromZero) + Math.Round(obj.Actual_Sec_Educess, 0, MidpointRounding.AwayFromZero)
            clsCommon.AddColumnsForChange(coll, "Actual_Total_TDS", obj.Actual_Total_TDS)
            clsCommon.AddColumnsForChange(coll, "Calculated_Total_TDS", obj.Calculated_Total_TDS)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Year", obj.Fiscal_Year)
            clsCommon.AddColumnsForChange(coll, "Quarter", obj.Quarter)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Section_Description", obj.Section_Description)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)
            clsCommon.AddColumnsForChange(coll, "Select_By", obj.Select_By)
            clsCommon.AddColumnsForChange(coll, "TDS_Per", obj.TDS_Per)
            clsCommon.AddColumnsForChange(coll, "Surcharge_Per", obj.Surcharge_Per)
            clsCommon.AddColumnsForChange(coll, "Edu_Cess_Per", obj.Edu_Cess_Per)
            clsCommon.AddColumnsForChange(coll, "Sec_Educess_Per", obj.Sec_Educess_Per)
            clsCommon.AddColumnsForChange(coll, "Previous_TDS_Amt", obj.Previous_TDS_Amt)
            clsCommon.AddColumnsForChange(coll, "Is_TDS_Override", IIf(obj.IsTDSOverride, 1, 0))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_REMITTANCE", OMInsertOrUpdate.Insert, "", trans)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String) As clsPIRemittance
        Return GetData(strDocumentCode, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal Trans As SqlTransaction) As clsPIRemittance
        Dim obj As clsPIRemittance = Nothing
        Dim qry As String = "Select * from TSPL_PI_REMITTANCE where Document_No='" + strDocumentCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPIRemittance()
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))
            obj.Service_Type = clsCommon.myCstr(dt.Rows(0)("Service_Type"))
            obj.Actual_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS_Base"))
            obj.Calculated_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("Calculated_TDS_Base"))
            obj.Actual_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS"))
            obj.Calculated_TDS = clsCommon.myCdbl(dt.Rows(0)("Calculated_TDS"))
            obj.Actual_Surcharge = clsCommon.myCdbl(dt.Rows(0)("Actual_Surcharge"))
            obj.Calculated_Surcharge = clsCommon.myCdbl(dt.Rows(0)("Calculated_Surcharge"))
            obj.Actual_Edu_Cess = clsCommon.myCdbl(dt.Rows(0)("Actual_Edu_Cess"))
            obj.Calculated_Edu_Cess = clsCommon.myCdbl(dt.Rows(0)("Calculated_Edu_Cess"))
            obj.Actual_Sec_Educess = clsCommon.myCdbl(dt.Rows(0)("Actual_Sec_Educess"))
            obj.Calculated_Sec_Educess = clsCommon.myCdbl(dt.Rows(0)("Calculated_Sec_Educess"))
            obj.Actual_Total_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_Total_TDS"))
            obj.Calculated_Total_TDS = clsCommon.myCdbl(dt.Rows(0)("Calculated_Total_TDS"))
            obj.Fiscal_Year = clsCommon.myCstr(dt.Rows(0)("Fiscal_Year"))
            obj.Quarter = clsCommon.myCstr(dt.Rows(0)("Quarter"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Section_Description = clsCommon.myCstr(dt.Rows(0)("Section_Description"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.TDS_Per = clsCommon.myCdbl(dt.Rows(0)("TDS_Per"))
            obj.Surcharge_Per = clsCommon.myCdbl(dt.Rows(0)("Surcharge_Per"))
            obj.Edu_Cess_Per = clsCommon.myCdbl(dt.Rows(0)("Edu_Cess_Per"))
            obj.Sec_Educess_Per = clsCommon.myCstr(dt.Rows(0)("Sec_Educess_Per"))
            obj.Select_By = clsCommon.myCstr(dt.Rows(0)("Select_By"))
            obj.Previous_TDS_Amt = clsCommon.myCdbl(dt.Rows(0)("Previous_TDS_Amt"))
            obj.IsTDSOverride = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_TDS_Override")) = 1, True, False)
            If (clsCommon.myCdbl(obj.Actual_TDS) > 0) Then
                obj.IsApplyTDS = True
            Else
                obj.IsApplyTDS = False
            End If
        End If
        Return obj
    End Function

    Public Shared Function IsTDSApplied(ByVal strDocNo As String) As Boolean
        Return IsTDSApplied(strDocNo, Nothing)
    End Function
    Public Shared Function IsTDSApplied(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim Count As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_REMITTANCE Where Document_No='" + strDocNo + "'", trans)
        If Count <= 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function Convert(ByVal objRemittance As clsRemittance, ByVal dblPreviousTDSAmt As Decimal) As clsPIRemittance
        Dim objPIRemittance As clsPIRemittance = Nothing
        If objRemittance IsNot Nothing Then
            objPIRemittance = New clsPIRemittance()
            objPIRemittance.Vendor_Code = objRemittance.Vendor_Code
            objPIRemittance.Vendor_Name = objRemittance.Vendor_Name
            objPIRemittance.Document_No = objRemittance.Document_No
            objPIRemittance.Document_Date = objRemittance.Document_Date
            objPIRemittance.Document_Type = objRemittance.Document_Type
            objPIRemittance.Document_Amount = objRemittance.Document_Amount
            objPIRemittance.Service_Type = objRemittance.Service_Type
            objPIRemittance.Actual_TDS_Base = objRemittance.Actual_TDS_Base
            objPIRemittance.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
            objPIRemittance.Actual_TDS = objRemittance.Actual_TDS
            objPIRemittance.Calculated_TDS = objRemittance.Calculated_TDS
            objPIRemittance.Actual_Surcharge = objRemittance.Actual_Surcharge
            objPIRemittance.Calculated_Surcharge = objRemittance.Calculated_Surcharge
            objPIRemittance.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
            objPIRemittance.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
            objPIRemittance.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
            objPIRemittance.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
            objPIRemittance.Actual_Total_TDS = objRemittance.Actual_Total_TDS
            objPIRemittance.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
            objPIRemittance.Fiscal_Year = objRemittance.Fiscal_Year
            objPIRemittance.Quarter = objRemittance.Quarter
            objPIRemittance.Section_Code = objRemittance.Section_Code
            objPIRemittance.Section_Description = objRemittance.Section_Description
            objPIRemittance.Branch_Code = objRemittance.Branch_Code
            objPIRemittance.Deduction_Code = objRemittance.Deduction_Code
            objPIRemittance.TDS_Per = objRemittance.TDS_Per
            objPIRemittance.Surcharge_Per = objRemittance.Surcharge_Per
            objPIRemittance.Edu_Cess_Per = objRemittance.Edu_Cess_Per
            objPIRemittance.Sec_Educess_Per = objRemittance.Sec_Educess_Per
            objPIRemittance.Select_By = objRemittance.Select_By
            objPIRemittance.IsTDSOverride = objRemittance.IsTDSOverride
            objPIRemittance.IsApplyTDS = objRemittance.IsApplyTDS
            objPIRemittance.Previous_TDS_Amt = dblPreviousTDSAmt
        End If
        Return objPIRemittance
    End Function
End Class

Public Class clsTDSDeductionDetails
#Region "Variables"
    Public Detail_Line_No As String = Nothing
    Public Deduction_Code As String = Nothing
    Public From_Range As String = Nothing
    Public To_Range As String = Nothing
    Public TDS As String = Nothing
    Public Surcharge As String = Nothing
    Public Educess As String = Nothing
    Public Seceducess As Double = 0

#End Region

    Public Shared Function GetApplicableTDRate(ByVal strDedCode As String, ByVal dblAmount As Double) As clsTDSDeductionDetails
        Return GetApplicableTDRate(strDedCode, dblAmount, Nothing)
    End Function

    Public Shared Function GetApplicableTDRate(ByVal strDedCode As String, ByVal dblAmount As Double, ByVal trans As SqlTransaction) As clsTDSDeductionDetails
        Return GetApplicableTDRate(strDedCode, dblAmount, trans, False)
    End Function
    Public Shared Function GetApplicableTDRate(ByVal strDedCode As String, ByVal dblAmount As Double, ByVal trans As SqlTransaction, ByVal isForService As Boolean) As clsTDSDeductionDetails
        Return GetApplicableTDRate(strDedCode, dblAmount, trans, isForService, "")
    End Function
    Public Shared Function GetApplicableTDRate(ByVal strDedCode As String, ByVal dblAmount As Double, ByVal trans As SqlTransaction, ByVal isForService As Boolean, ByVal strVendorCode As String) As clsTDSDeductionDetails
        Dim obj As clsTDSDeductionDetails = Nothing
        Dim qry As String = "select isnull(IsBuyerFileReturnInLastTwoYears,0) as IsBuyerFileReturnInLastTwoYears,TSPL_TDS_DEDUCTION_HEAD.Min_Service_Per,TSPL_TDS_DEDUCTION_DETAIL.TDS,TSPL_TDS_DEDUCTION_DETAIL.Surcharge,TSPL_TDS_DEDUCTION_DETAIL.Educess,TSPL_TDS_DEDUCTION_DETAIL.Seceducess from TSPL_TDS_DEDUCTION_DETAIL left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_DEDUCTION_DETAIL.Deduction_Code where TSPL_TDS_DEDUCTION_HEAD.Deduction_Code='" + strDedCode + "'  and '" + clsCommon.myCstr(dblAmount) + "'>= TSPL_TDS_DEDUCTION_DETAIL.From_Range  and '" + clsCommon.myCstr(dblAmount) + "'<=TSPL_TDS_DEDUCTION_DETAIL.To_Range and TSPL_TDS_DEDUCTION_HEAD.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTDSDeductionDetails()
            obj.TDS = clsCommon.myCdbl(dt.Rows(0)("TDS"))
            If isForService Then
                If clsCommon.myLen(strVendorCode) <= 0 Then
                    Throw New Exception("Please Provide Service Vendor to Get TDS Rate")
                End If
                Dim PAN As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PAN from tspl_Vendor_master where vendor_code='" + strVendorCode + "'", trans))
                If (clsCommon.myCdbl(dt.Rows(0)("IsBuyerFileReturnInLastTwoYears")) <= 0 OrElse clsCommon.myLen(PAN) <= 0) Then
                    obj.TDS = clsCommon.myCdbl(dt.Rows(0)("TDS")) * 2
                    If obj.TDS < clsCommon.myCdbl(dt.Rows(0)("Min_Service_Per")) Then
                        obj.TDS = clsCommon.myCdbl(dt.Rows(0)("Min_Service_Per"))
                    End If
                End If
            End If
            obj.Surcharge = clsCommon.myCdbl(dt.Rows(0)("Surcharge"))
            obj.Educess = clsCommon.myCdbl(dt.Rows(0)("Educess"))
            obj.Seceducess = clsCommon.myCdbl(dt.Rows(0)("Seceducess"))
        End If
        Return obj
    End Function
End Class

Public Class clsTDSVendorDetails
#Region "Variables"
    Public Vendor_Code As String = Nothing
    Public Nature_Of_Deduction As String = Nothing
    Public State_Code As String = Nothing
    Public PAN As String = Nothing
    Public Vendor_TYpe As String = Nothing
    Public Status As String = Nothing
    Public Branch_Code As String = Nothing
    Public Comp_Code As String = Nothing
    'Public Include_Tax As String = Nothing

    Public TDSSection As String = Nothing 'Not Table Column
    Public TDSSectionDescription As String = Nothing 'Not Table Column
    Public VendorTypeCode As String = Nothing 'Not Table Column

#End Region

    'Public Shared Function GetData(ByVal strVendorCode As String) As clsTDSVendorDetails
    '    Return GetData(strVendorCode, False)
    'End Function
    Public Shared Function GetData(ByVal strVendorCode As String) As clsTDSVendorDetails
        Return GetData(strVendorCode, False)
    End Function
    Public Shared Function GetData(ByVal strVendorCode As String, ByVal IsService As Boolean) As clsTDSVendorDetails
        Dim obj As clsTDSVendorDetails = Nothing
        Dim qry As String = "select TSPL_TDS_VENDOR_DETAILS.Vendor_Code,TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,TSPL_TDS_VENDOR_DETAILS.State_Code,TSPL_TDS_VENDOR_DETAILS.PAN,TSPL_TDS_VENDOR_DETAILS.Vendor_Type,TSPL_TDS_VENDOR_DETAILS.Status,TSPL_TDS_VENDOR_DETAILS.Branch_Code,TSPL_TDS_VENDOR_DETAILS.Comp_Code,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as TDSSection, TSPL_TDS_SECTION_MASTER.Description as TDSSectionDescription,TSPL_TDS_SECTION_MASTER.Include_Tax ,case when Vendor_Type='Domestic Company    ' then 'C' else 'N' end as VendorTypeCode from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + strVendorCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTDSVendorDetails()
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Nature_Of_Deduction = clsCommon.myCstr(dt.Rows(0)("Nature_Of_Deduction"))
            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
            obj.Vendor_TYpe = clsCommon.myCstr(dt.Rows(0)("Vendor_TYpe"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.TDSSection = clsCommon.myCstr(dt.Rows(0)("TDSSection"))
            obj.TDSSectionDescription = clsCommon.myCstr(dt.Rows(0)("TDSSectionDescription"))
            obj.VendorTypeCode = clsCommon.myCstr(dt.Rows(0)("VendorTypeCode"))

            If IsService Then ''UDL/12/07/21-001042 by balwinder on 15/07/2021
                If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTDSforServiceVendorSeparately, clsFixedParameterCode.EnableTDSforServiceVendorSeparately, Nothing)) = 1) Then
                    qry = "select TSPL_VENDOR_MASTER.TDS_Branch_Code_Service,TSPL_VENDOR_MASTER.Deduction_Code_Service,TSPL_VENDOR_MASTER.TDS_Status_Service,TSPL_VENDOR_MASTER.TDS_Vendor_Type_Service,TSPL_VENDOR_MASTER.TDS_State_Code_Service,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as TDSSection, TSPL_TDS_SECTION_MASTER.Description as TDSSectionDescription from TSPL_VENDOR_MASTER left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_VENDOR_MASTER.Deduction_Code_Service left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section  where Vendor_Code='" + strVendorCode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("TDS_Branch_Code_Service"))
                        obj.Nature_Of_Deduction = clsCommon.myCstr(dt.Rows(0)("Deduction_Code_Service"))
                        obj.Status = clsCommon.myCstr(dt.Rows(0)("TDS_Status_Service"))
                        'obj.VendorTypeCode = clsCommon.myCstr(dt.Rows(0)("TDS_Vendor_Type_Service"))
                        obj.State_Code = clsCommon.myCstr(dt.Rows(0)("TDS_State_Code_Service"))
                        obj.TDSSectionDescription = clsCommon.myCstr(dt.Rows(0)("TDSSectionDescription"))
                        obj.TDSSection = clsCommon.myCstr(dt.Rows(0)("TDSSection"))
                    End If
                End If
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetData(ByVal strVendorCode As String, ByVal trans As SqlTransaction) As clsTDSVendorDetails
        Dim obj As clsTDSVendorDetails = Nothing
        Dim qry As String = "select TSPL_TDS_VENDOR_DETAILS.Vendor_Code,TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,TSPL_TDS_VENDOR_DETAILS.State_Code,TSPL_TDS_VENDOR_DETAILS.PAN,TSPL_TDS_VENDOR_DETAILS.Vendor_Type,TSPL_TDS_VENDOR_DETAILS.Status,TSPL_TDS_VENDOR_DETAILS.Branch_Code,TSPL_TDS_VENDOR_DETAILS.Comp_Code,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as TDSSection, TSPL_TDS_SECTION_MASTER.Description as TDSSectionDescription,TSPL_TDS_SECTION_MASTER.Include_Tax ,case when Vendor_Type='Domestic Company    ' then 'C' else 'N' end as VendorTypeCode from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + strVendorCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTDSVendorDetails()
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Nature_Of_Deduction = clsCommon.myCstr(dt.Rows(0)("Nature_Of_Deduction"))
            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
            obj.Vendor_TYpe = clsCommon.myCstr(dt.Rows(0)("Vendor_TYpe"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))

            'obj.Include_Tax = clsCommon.myCstr(dt.Rows(0)("Include_Tax"))

            obj.TDSSection = clsCommon.myCstr(dt.Rows(0)("TDSSection"))
            obj.TDSSectionDescription = clsCommon.myCstr(dt.Rows(0)("TDSSectionDescription"))
            obj.VendorTypeCode = clsCommon.myCstr(dt.Rows(0)("VendorTypeCode"))
        End If
        Return obj
    End Function
End Class

Public Class clsTDSSection
    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description from TSPL_TDS_SECTION_MASTER where TDS_Group='" + strCode + "'"))
    End Function
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_TDS_SECTION_MASTER.TDS_Group as [Code] ,TSPL_TDS_SECTION_MASTER.Description as [Description] ,TSPL_TDS_SECTION_MASTER.Report_Section as [Report Section] ,TSPL_TDS_SECTION_MASTER.Cumulative_Cutoff as [Cumulative Cutoff] ,TSPL_TDS_SECTION_MASTER.Include_Tax as [Include Tax] ,TSPL_TDS_SECTION_MASTER.Created_By as [Created By] ,TSPL_TDS_SECTION_MASTER.Created_Date as [Created Date] ,TSPL_TDS_SECTION_MASTER.Modify_By as [Modify By] ,TSPL_TDS_SECTION_MASTER.Modify_Date as [Modify Date] ,TSPL_TDS_SECTION_MASTER.Comp_Code as [Company Code]  From TSPL_TDS_SECTION_MASTER   "
        str = clsCommon.ShowSelectForm("TDSSECFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
End Class