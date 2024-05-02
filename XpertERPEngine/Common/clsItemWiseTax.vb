Imports common
Imports System.Data.SqlClient

Public Class clsItemWiseTax
#Region "Variables"
    Public HCODE As String
    Public DOC_DATE As Date?
    Public Type As String
    Public Status As Integer = 0
    Public Description As String
    Public Created_By As String
    Public Created_Date As Date?
    Public Modify_By As String
    Public Modify_Date As Date?
    Public Comp_code As String
    Public ArrGroup As List(Of clsItemWiseTaxGroup) = Nothing
    Public ArrAuth As List(Of clsItemWiseTaxAuthority) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsItemWiseTax, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_ITEM_WISE_TAX_AUTHORITY where HCODE='" + obj.HCODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITEM_WISE_TAX_GROUP where HCODE='" + obj.HCODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                'obj.HCODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.ItemWiseTax, "", "") 'clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.DOC_DATE), clsDocType.ItemWiseTax, "", "")
                'If clsCommon.myLen(obj.HCODE) <= 0 Then
                '    Throw New Exception("Error in code generation")
                'End If
                clsCommon.AddColumnsForChange(coll, "HCODE", obj.HCODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_WISE_TAX", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_WISE_TAX", OMInsertOrUpdate.Update, "HCODE='" & obj.HCODE & "'", trans)
            End If
            Dim objtr As New clsItemWiseTaxGroup
            objtr.SaveData(obj.HCODE, obj.ArrGroup, obj.ArrAuth, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsItemWiseTax
        Dim qry As String = "select * from TSPL_ITEM_WISE_TAX Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ITEM_WISE_TAX.HCODE = (select MIN(HCODE) from TSPL_ITEM_WISE_TAX where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ITEM_WISE_TAX.HCODE = (select Max(HCODE) from TSPL_ITEM_WISE_TAX where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ITEM_WISE_TAX.HCODE = (select Min(HCODE) from TSPL_ITEM_WISE_TAX where HCODE>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ITEM_WISE_TAX.HCODE = (select Max(HCODE) from TSPL_ITEM_WISE_TAX where HCODE<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ITEM_WISE_TAX.HCODE = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsItemWiseTax = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemWiseTax()
            obj.HCODE = clsCommon.myCstr(dt.Rows(0)("HCODE"))
            obj.DOC_DATE = clsCommon.myCDate(dt.Rows(0)("DOC_DATE"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))

            qry = " select max (xxxx.HCODE) as HCODE, xxxx.TAX_Group_SNo, xxxx.Item_Code,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx where xxxx.HCODE = '" + obj.HCODE + "' group by xxxx.Item_Code, xxxx.TAX_Group_SNo order by xxxx.TAX_Group_SNo asc  "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrGroup = New List(Of clsItemWiseTaxGroup)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsItemWiseTaxGroup
                    objtr.SNO = clsCommon.myCdbl(dr("TAX_Group_SNo"))
                    objtr.HCODE = clsCommon.myCstr(dr("HCODE"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Tax_Group_Code = clsCommon.myCstr(dr("Tax_Group_Code"))
                    objtr.Tax1_Code = clsCommon.myCstr(dr("Tax1_Code"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.Tax2_Code = clsCommon.myCstr(dr("Tax2_Code"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.Tax3_Code = clsCommon.myCstr(dr("Tax3_Code"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.Tax4_Code = clsCommon.myCstr(dr("Tax4_Code"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.Tax5_Code = clsCommon.myCstr(dr("Tax5_Code"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    obj.ArrGroup.Add(objtr)
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
                Throw New Exception("Transaction Number not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsItemWiseTax = clsItemWiseTax.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.HCODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted on :" & obj.Modify_Date & " ")
            End If

            Dim qry As String = "Update TSPL_ITEM_WISE_TAX set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where HCODE='" + strDocNo + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Transaction Number not found to Post")
            End If
            Dim strReverceDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsItemWiseTax = clsItemWiseTax.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.HCODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 0) Then
                Throw New Exception("Already UnPosted.")
            End If

            Dim qry As String = "Update TSPL_ITEM_WISE_TAX set Status=0, Modify_Date='" + strReverceDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where HCODE='" + strDocNo + "' "
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
        Dim obj As clsItemWiseTax = clsItemWiseTax.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HCODE) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_ITEM_WISE_TAX_AUTHORITY where HCODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_ITEM_WISE_TAX_GROUP where HCODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_ITEM_WISE_TAX where HCODE='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

End Class
Public Class clsItemWiseTaxGroup
#Region "Variables"
    Public DCODE As String
    Public SNO As Integer
    Public HCODE As String
    Public Item_Code As String
    Public Tax_Group_Code As String
    Public Tax1_Code As String
    Public TAX1_Rate As Double
    Public Tax2_Code As String
    Public TAX2_Rate As Double
    Public Tax3_Code As String
    Public TAX3_Rate As Double
    Public Tax4_Code As String
    Public TAX4_Rate As Double
    Public Tax5_Code As String
    Public TAX5_Rate As Double

#End Region
    Public Function SaveData(ByVal strCode As String, ByVal ArrGroup As List(Of clsItemWiseTaxGroup), ByVal ArrAuth As List(Of clsItemWiseTaxAuthority), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtr As clsItemWiseTaxGroup In ArrGroup
                'ii += 1
                'objtr.SNO = ii
                'objtr.DCODE = clsCommon.myCstr(strCode) + "-" + clsCommon.myCstr(objtr.SNO)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "HCODE", objtr.HCODE)
                clsCommon.AddColumnsForChange(coll, "DCODE", objtr.DCODE)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Tax_Group_Code", objtr.Tax_Group_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_WISE_TAX_GROUP", OMInsertOrUpdate.Insert, "", trans)

            Next
            Dim objtrAuth As New clsItemWiseTaxAuthority
            objtrAuth.SaveData("", strCode, ArrAuth, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsItemWiseTaxAuthority
#Region "Variables"
    Public DDCODE As String
    Public SNO As Integer
    Public DCODE As String
    Public HCODE As String
    Public Tax_Authority As String
    Public TAX_Rate As Decimal
    Public Function SaveData(ByVal strCode As String, ByVal strHCode As String, ByVal Arr As List(Of clsItemWiseTaxAuthority), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtrAuth As clsItemWiseTaxAuthority In Arr
                'ii += 1
                ' objtrAuth.SNO = ii
                ' objtrAuth.DDCODE = clsCommon.myCstr(strCode) + "-" + clsCommon.myCstr(objtrAuth.SNO)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DDCODE", objtrAuth.DDCODE)
                clsCommon.AddColumnsForChange(coll, "SNO", objtrAuth.SNO)
                clsCommon.AddColumnsForChange(coll, "HCODE", objtrAuth.HCODE)
                clsCommon.AddColumnsForChange(coll, "DCODE", objtrAuth.DCODE)
                clsCommon.AddColumnsForChange(coll, "Tax_Authority", objtrAuth.Tax_Authority)
                clsCommon.AddColumnsForChange(coll, "TAX_Rate", objtrAuth.TAX_Rate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_WISE_TAX_AUTHORITY", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetAutoItemwiseTaxRate(ByVal strItemCode As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strDocDate As Date, ByVal strTaxType As String) As clsItemWiseTaxAuthority
        Return GetAutoItemwiseTaxRate(strItemCode, strTaxGroup, strTaxCode, strDocDate, strTaxType, Nothing)
    End Function
    Public Shared Function GetAutoItemwiseTaxRate(ByVal strItemCode As String, ByVal strTaxGroup As String, ByVal strTaxCode As String, ByVal strDocDate As Date, ByVal strTaxType As String, ByVal trans As SqlTransaction) As clsItemWiseTaxAuthority
        Dim obj As clsItemWiseTaxAuthority = Nothing
        Try
            Dim dblTaxRate As Double = 0
            Dim intExemptedType As Integer = 0
            Dim IsExmpted As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + strTaxGroup + "'", trans))
            If Not IsExmpted Then
                Dim intTCSCount As Integer = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_TAX_MASTER inner join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_MASTER.Is_TCS='Y' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" & strTaxGroup & "'", trans)
                If intTCSCount > 0 Then
                    intExemptedType = 0
                Else
                    intExemptedType = clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & strTaxGroup & "'", trans)
                End If

                If intExemptedType = 0 Then
                    If clsCommon.myLen(strItemCode) = 0 Then
                        Throw New Exception("Please enter Item Code")
                    ElseIf clsCommon.myLen(strTaxGroup) = 0 Then
                        Throw New Exception("Please enter Tax Group")
                    ElseIf clsCommon.myLen(strTaxCode) = 0 Then
                        Throw New Exception("Please enter Tax Code")
                    ElseIf clsCommon.myLen(strTaxType) = 0 Then
                        Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
                    End If
                    Dim qry = "select top 1 * from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " &
                        "left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " &
                        "and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " &
                        "where Status=1 and DOC_DATE < ='" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' and   Tax_Group_Code='" & strTaxGroup & "' and Tax_Authority='" & strTaxCode & "' and Item_Code='" & strItemCode & "' order by DOC_DATE desc "
                    Dim dt As DataTable
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj = New clsItemWiseTaxAuthority
                        obj.HCODE = clsCommon.myCstr(dt.Rows(0)("HCODE"))
                        obj.TAX_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX_Rate"))
                    Else
                        Throw New Exception("Please enter Tax Rate For  " + Environment.NewLine +
                                                   "Item    --  " & strItemCode & " " + Environment.NewLine +
                                                   "Tax Group  --   " & strTaxGroup & " " + Environment.NewLine +
                                                   "Tax Code  --   " & strTaxCode & " " + Environment.NewLine +
                                                   "Tax Type --   " & strTaxType & " ")

                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    ''created function by richa 25 Nov,2020 get tax ItemWise
    Public Shared Function GetTaxGroupItemWise(ByVal strLocalORInterstate As String, ByVal strTaxType As String, ByVal strDocDate As Date) As String
        Dim qry As String = "select  TOP 1 TSPL_ITEM_WISE_TAX_GROUP.TAX_GROUP_CODE from " &
    " TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " &
    " left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE And TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE " &
    " where Status=1 And DOC_DATE < ='" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' and Type='" & strTaxType & "' " &
    " And   Tax_Group_Code IN (select  TSPL_TAX_GROUP_MASTER.Tax_group_code from TSPL_TAX_GROUP_DETAILS " &
    " Left OUTER JOIN TSPL_TAX_GROUP_MASTER On TSPL_TAX_GROUP_MASTER.TAX_GROUP_CODE=TSPL_TAX_GROUP_DETAILS.TAX_GROUP_CODE" &
    " left outer join TSPL_TAX_MASTER on  TSPL_TAX_MASTER.tax_code=TSPL_TAX_GROUP_DETAILS.Tax_Code  " &
    " where 1=1 "
        If clsCommon.CompairString(strLocalORInterstate, "L") = CompairStringResult.Equal Then
            qry += " AND TSPL_TAX_MASTER.Type IN ('CGST','SGST') "
        Else
            qry += " AND TSPL_TAX_MASTER.Type IN ('IGST')"
        End If
        qry += "   AND TSPL_TAX_GROUP_mASTER.tAX_GROUP_TYPE='" & strTaxType & "' AND TSPL_TAX_GROUP_mASTER.IS_TAX_EXEMPTED=0" &
" )order by DOC_DATE desc "

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Function
    Public Shared Function GetTaxGroupItemWise(ByVal strLocalORInterstate As String, ByVal strTaxType As String, ByVal strDocDate As Date, ByVal ICode As String) As String
        Dim qry As String = "select  TOP 1 TSPL_ITEM_WISE_TAX_GROUP.TAX_GROUP_CODE from " &
    " TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " &
    " left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE And TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE " &
    " where Status=1 And TSPL_ITEM_WISE_TAX_GROUP.Item_Code='" + ICode + "' And DOC_DATE < ='" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' and Type='" & strTaxType & "' " &
    " And   Tax_Group_Code IN (select  TSPL_TAX_GROUP_MASTER.Tax_group_code from TSPL_TAX_GROUP_DETAILS " &
    " Left OUTER JOIN TSPL_TAX_GROUP_MASTER On TSPL_TAX_GROUP_MASTER.TAX_GROUP_CODE=TSPL_TAX_GROUP_DETAILS.TAX_GROUP_CODE" &
    " left outer join TSPL_TAX_MASTER on  TSPL_TAX_MASTER.tax_code=TSPL_TAX_GROUP_DETAILS.Tax_Code  " &
    " where 1=1 "
        If clsCommon.CompairString(strLocalORInterstate, "L") = CompairStringResult.Equal Then
            qry += " AND TSPL_TAX_MASTER.Type IN ('CGST','SGST') "
        Else
            qry += " AND TSPL_TAX_MASTER.Type IN ('IGST')"
        End If
        qry += "   AND TSPL_TAX_GROUP_mASTER.tAX_GROUP_TYPE='" & strTaxType & "' AND TSPL_TAX_GROUP_mASTER.IS_TAX_EXEMPTED=0" &
" )order by DOC_DATE desc "

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Function
    Public Shared Function GetTaxGroupItemWise(ByVal strLocalORInterstate As String, ByVal strTaxType As String, ByVal strDocDate As Date, ByVal ICode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select  TOP 1 TSPL_ITEM_WISE_TAX_GROUP.TAX_GROUP_CODE from " &
    " TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " &
    " left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE And TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE " &
    " where Status=1 And TSPL_ITEM_WISE_TAX_GROUP.Item_Code='" + ICode + "' And DOC_DATE < ='" & clsCommon.GetPrintDate(strDocDate, "dd/MMM/yyyy") & "' and Type='" & strTaxType & "' " &
    " And   Tax_Group_Code IN (select  TSPL_TAX_GROUP_MASTER.Tax_group_code from TSPL_TAX_GROUP_DETAILS " &
    " Left OUTER JOIN TSPL_TAX_GROUP_MASTER On TSPL_TAX_GROUP_MASTER.TAX_GROUP_CODE=TSPL_TAX_GROUP_DETAILS.TAX_GROUP_CODE" &
    " left outer join TSPL_TAX_MASTER on  TSPL_TAX_MASTER.tax_code=TSPL_TAX_GROUP_DETAILS.Tax_Code  " &
    " where 1=1 "
        If clsCommon.CompairString(strLocalORInterstate, "L") = CompairStringResult.Equal Then
            qry += " AND TSPL_TAX_MASTER.Type IN ('CGST','SGST') "
        Else
            qry += " AND TSPL_TAX_MASTER.Type IN ('IGST')"
        End If
        qry += "   AND TSPL_TAX_GROUP_mASTER.tAX_GROUP_TYPE='" & strTaxType & "' AND TSPL_TAX_GROUP_mASTER.IS_TAX_EXEMPTED=0" &
" )order by DOC_DATE desc "

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
#End Region
End Class

