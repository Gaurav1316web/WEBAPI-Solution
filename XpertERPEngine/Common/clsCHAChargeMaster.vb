'==========created by Monika BM00000003447 
Imports common
Imports System.Data.SqlClient
Public Class clsCHAChargeMaster
#Region "variables"
    Public DocNo As String = Nothing
    Public DocDate As Date = Nothing
    Public Doc_Desc As String = Nothing
    Public CHA_Type As String = Nothing
    Public amount As Decimal = Nothing

    Public arr As List(Of clsCHAChargeMaster) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select distinct doc_no as [Code],doc_date as [Date],[Description],(select ','+(case when cha_type='ICD' then 'Dry Port-ICD(Inland Container Depot)' else case when cha_type='ISD' then 'Sea Port' else case when cha_type='THC' then 'Terminal Handling Charges at ICD& Sea Port' else case when cha_type='IHC' then 'Inland Haulage Charges at ICD& Sea Port' else case when cha_type='OTH' then 'Other' else 'None' end end end end end) from TSPL_CHA_CHARGE_MASTER cha where cha.Doc_No=TSPL_CHA_CHARGE_MASTER.Doc_No for xml path('')) as [CHA Type],Amount from TSPL_CHA_CHARGE_MASTER"
        str = clsCommon.ShowSelectForm("CHAFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetVendorCHA_Detail(ByVal VendorCode As String, ByVal trans As SqlTransaction) As clsCHAChargeMaster
        Dim obj As New clsCHAChargeMaster()
        obj.DocNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cha_doc_no from tspl_vendor_master where vendor_code='" + VendorCode + "'"))
        obj.CHA_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct (select ','+(case when cha_type='ICD' then 'Dry Port-ICD(Inland Container Depot)' else case when cha_type='ISD' then 'Sea Port' else case when cha_type='THC' then 'Terminal Handling Charges at ICD& Sea Port' else case when cha_type='IHC' then 'Inland Haulage Charges at ICD& Sea Port' else case when cha_type='OTH' then 'Other' else 'None' end end end end end) from tspl_cha_charge_master where doc_no='" + obj.DocNo + "' for xml path('')) as cha_type"))
        obj.amount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select amount from tspl_cha_charge_master where doc_no='" + obj.DocNo + "'"))

        If clsCommon.myLen(obj.CHA_Type) > 0 AndAlso obj.CHA_Type.Substring(0, 1) = "," Then
            obj.CHA_Type = obj.CHA_Type.Substring(1, obj.CHA_Type.Length - 1)
        End If

        Return obj
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsCHAChargeMaster, ByVal isNewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(strCode, obj, isNewentry, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsCHAChargeMaster, ByVal isNewentry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()

            If isNewentry Then
                'Comment by Prabhakar 
                'obj.DocNo = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.CHACHARGEMASTER, "", ""))
                obj.DocNo = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.CHACHARGEMASTER, "", ""))
                strCode = obj.DocNo
            End If


            Dim qry As String = "select count(*) from TSPL_CHA_CHARGE_MASTER where doc_no='" + obj.DocNo + "'"
            clsDBFuncationality.getSingleValue(qry, trans)


            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                For Each objtr As clsCHAChargeMaster In obj.arr
                    If isNewentry AndAlso clsCommon.myLen(strCode) > 0 Then
                        objtr.DocNo = strCode
                    End If

                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", objtr.DocNo)
                    clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(objtr.DocDate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "description", objtr.Doc_Desc)
                    clsCommon.AddColumnsForChange(coll, "cha_type", objtr.CHA_Type)
                    clsCommon.AddColumnsForChange(coll, "amount", objtr.amount)
                    Dim ModifiedBy As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", ModifiedBy)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    strCode = objtr.DocNo
                    Dim CreatedBy As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", CreatedBy)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CHA_CHARGE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            obj.DocNo = strCode
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCHAChargeMaster
        Try
            Dim obj As clsCHAChargeMaster = GetData(strCode, NavType, Nothing)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCHAChargeMaster
        Dim objtr As New clsCHAChargeMaster()
        Dim dt As New DataTable()
        Try
            Dim obj As New clsCHAChargeMaster()
            obj.arr = New List(Of clsCHAChargeMaster)

            Dim qry As String = "select * from TSPL_CHA_CHARGE_MASTER where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and doc_no in (select min(doc_no) from TSPL_CHA_CHARGE_MASTER)"
                Case NavigatorType.Last
                    qry += " and doc_no in (select max(doc_no) from TSPL_CHA_CHARGE_MASTER)"
                Case NavigatorType.Next
                    qry += " and doc_no in (select min(doc_no) from TSPL_CHA_CHARGE_MASTER where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and doc_no in (select max(doc_no) from TSPL_CHA_CHARGE_MASTER where doc_no<'" + strCode + "')"
            End Select
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsCHAChargeMaster()

                    objtr.DocNo = clsCommon.myCstr(dr("doc_no"))
                    objtr.DocDate = clsCommon.myCDate(dr("doc_date"))
                    objtr.Doc_Desc = clsCommon.myCstr(dr("description"))
                    objtr.CHA_Type = clsCommon.myCstr(dr("cha_type"))
                    objtr.amount = clsCommon.myCdbl(dr("amount"))

                    obj.arr.Add(objtr)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objtr = Nothing
            dt = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CHA_CHARGE_MASTER where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function LoadCHAType() As DataTable
        Try
            Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'ICD' as Code,'Dry Port-ICD(Inland Container Depot)' as Name"
            qry += " union all select 'ISD' as Code,'Sea Port' as Name"
            qry += " union all select 'THC' as Code,'Terminal Handling Charges at ICD& Sea Port' as Name"
            qry += " union all select 'IHC' as Code,'Inland Haulage Charges at ICD& Sea Port' as Name"
            qry += " union all select 'OTH' as Code,'Other' as Name)a"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetCHAType(ByVal CHA_NAME As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(CHA_NAME, "None") = CompairStringResult.Equal Then
                str = ""
            ElseIf clsCommon.CompairString(CHA_NAME, "Dry Port-ICD(Inland Container Depot)") = CompairStringResult.Equal Then
                str = "ICD"
            ElseIf clsCommon.CompairString(CHA_NAME, "Sea Port") = CompairStringResult.Equal Then
                str = "ISD"
            ElseIf clsCommon.CompairString(CHA_NAME, "Terminal Handling Charges at ICD& Sea Port") = CompairStringResult.Equal Then
                str = "THC"
            ElseIf clsCommon.CompairString(CHA_NAME, "Inland Haulage Charges at ICD& Sea Port") = CompairStringResult.Equal Then
                str = "IHC"
            ElseIf clsCommon.CompairString(CHA_NAME, "Other") = CompairStringResult.Equal Then
                str = "OTH"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetCHATypeDescription(ByVal CHA_Code As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(CHA_Code, "") = CompairStringResult.Equal Then
                str = "None"
            ElseIf clsCommon.CompairString(CHA_Code, "ICD") = CompairStringResult.Equal Then
                str = "Dry Port-ICD(Inland Container Depot)"
            ElseIf clsCommon.CompairString(CHA_Code, "ISD") = CompairStringResult.Equal Then
                str = "Sea Port"
            ElseIf clsCommon.CompairString(CHA_Code, "THC") = CompairStringResult.Equal Then
                str = "Terminal Handling Charges at ICD& Sea Port"
            ElseIf clsCommon.CompairString(CHA_Code, "IHC") = CompairStringResult.Equal Then
                str = "Inland Haulage Charges at ICD& Sea Port"
            ElseIf clsCommon.CompairString(CHA_Code, "OTH") = CompairStringResult.Equal Then
                str = "Other"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
