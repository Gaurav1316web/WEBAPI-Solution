Imports common
Imports System.Data.SqlClient
Public Class clsJWEStimate
#Region "Variables"
    Public Document_NO As String = Nothing
    Public Document_Date As Date = Nothing
    Public Location_Code As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Item_Structure_FAT As String = String.Empty
    Public Formula_code_FAT As String = String.Empty
    Public Formula_Date_FAT As DateTime = Nothing
    Public Formula_FAT As String = String.Empty
    Public Item_Structure_SNF As String = String.Empty
    Public Formula_code_SNF As String = String.Empty
    Public Formula_Date_SNF As DateTime = Nothing
    Public Formula_SNF As String = String.Empty
    Public Qty_Weighment As Decimal = 0
    Public FAT_KG_Weighment As Decimal = 0
    Public SNF_KG_Weighment As Decimal = 0
    Public Estimated_FAT_KG_Weighment As Decimal = 0
    Public Estimated_SNF_KG_Weighment As Decimal = 0
    Public FAT_KG_Raw_Item As Decimal = 0
    Public SNF_KG_Raw_Item As Decimal = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public ArrWeighment As List(Of clsJWIEstimateWeighment) = Nothing
    Public ArrFATProduction As List(Of clsJWIEstimateFATProduction) = Nothing
    Public ArrSNFProducion As List(Of clsJWIEstimateSNFProduction) = Nothing
    Public ArrRawItem As List(Of clsJWIEstimateRawItem) = Nothing
#End Region

    Public Function saveData(ByVal obj As clsJWEStimate, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_NO, "TSPL_JWI_ESTIMATION_SNF_PRODUCTION", "Document_NO", "TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER", "Document_NO", "TSPL_JWI_ESTIMATION_RAW_ITEM", "Document_NO", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_NO, "TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER", "Document_NO", "TSPL_JWI_ESTIMATION_FAT_PRODUCTION", "Document_NO", "TSPL_JWI_ESTIMATION_WEIGHMENT", "Document_NO", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_NO, "TSPL_JWI_ESTIMATION_HEAD", "Document_NO", trans)
            End If
            Dim qry As String = "delete from TSPL_JWI_ESTIMATION_RAW_ITEM where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_SNF_PRODUCTION where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_FAT_PRODUCTION where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_WEIGHMENT where Document_NO='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Structure_FAT", obj.Item_Structure_FAT)
            clsCommon.AddColumnsForChange(coll, "Formula_code_FAT", obj.Formula_code_FAT)
            clsCommon.AddColumnsForChange(coll, "Formula_Date_FAT", clsCommon.GetPrintDate(obj.Formula_Date_FAT, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Formula_FAT", obj.Formula_FAT)
            clsCommon.AddColumnsForChange(coll, "Item_Structure_SNF", obj.Item_Structure_SNF)
            clsCommon.AddColumnsForChange(coll, "Formula_code_SNF", obj.Formula_code_SNF)
            clsCommon.AddColumnsForChange(coll, "Formula_Date_SNF", clsCommon.GetPrintDate(obj.Formula_Date_SNF, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Formula_SNF", obj.Formula_SNF)
            clsCommon.AddColumnsForChange(coll, "Qty_Weighment", obj.Qty_Weighment)
            clsCommon.AddColumnsForChange(coll, "FAT_KG_Weighment", obj.FAT_KG_Weighment)
            clsCommon.AddColumnsForChange(coll, "SNF_KG_Weighment", obj.SNF_KG_Weighment)
            clsCommon.AddColumnsForChange(coll, "Estimated_FAT_KG_Weighment", obj.Estimated_FAT_KG_Weighment)
            clsCommon.AddColumnsForChange(coll, "Estimated_SNF_KG_Weighment", obj.Estimated_SNF_KG_Weighment)
            clsCommon.AddColumnsForChange(coll, "FAT_KG_Raw_Item", obj.FAT_KG_Raw_Item)
            clsCommon.AddColumnsForChange(coll, "SNF_KG_Raw_Item", obj.SNF_KG_Raw_Item)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_NO = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.JWEstimate, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_NO) <= 0 Then
                    Throw New Exception("Error in document generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_NO", obj.Document_NO)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_HEAD", OMInsertOrUpdate.Update, "TSPL_JWI_ESTIMATION_HEAD.Document_NO='" + obj.Document_NO + "'", trans)
            End If
            clsJWIEstimateWeighment.SaveData(obj.Document_NO, obj.Document_Date, ArrWeighment, trans)
            clsJWIEstimateFATProduction.SaveData(obj.Document_NO, obj.Document_Date, ArrFATProduction, trans)
            clsJWIEstimateSNFProduction.SaveData(obj.Document_NO, obj.Document_Date, ArrSNFProducion, trans)
            clsJWIEstimateRawItem.SaveData(obj.Document_NO, obj.Document_Date, ArrRawItem, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_JWI_ESTIMATION_HEAD.Document_NO as  Code , convert (varchar,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) as Date,case when  TSPL_JWI_ESTIMATION_HEAD.Status = 1 then 'Posted' else 'Pending' end Status ,TSPL_JWI_ESTIMATION_HEAD.Location_Code as Location , TSPL_LOCATION_MASTER.Location_Desc as [Location Desc] ,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code as [Vendor Code],tspl_vendor_master.Vendor_Name as [Vendor Name],TSPL_JWI_ESTIMATION_HEAD.Item_Structure_FAT as [FAT Structure Code],TSPL_STRUCTURE_MASTERFATFAT.Structure_Descq as [FAT Structure Desc],TSPL_JWI_ESTIMATION_HEAD.Formula_code_FAT as [FAT Formula Code],TSPL_JWI_ESTIMATION_HEAD.Formula_Date_FAT as [FAT Formula Date],TSPL_JWI_ESTIMATION_HEAD.Formula_FAT as [FAT Formula],TSPL_JWI_ESTIMATION_HEAD.Item_Structure_SNF as [SNF Structure Code],TSPL_STRUCTURE_MASTERSNFSNF.Structure_Descq as [SNF Structure Desc],TSPL_JWI_ESTIMATION_HEAD.Formula_code_SNF as [SNF Formula Code],TSPL_JWI_ESTIMATION_HEAD.Formula_Date_SNF as [SNF Formula Date],TSPL_JWI_ESTIMATION_HEAD.Formula_SNF as [SNF Formula]" + Environment.NewLine + _
        "From TSPL_JWI_ESTIMATION_HEAD" + Environment.NewLine + _
        "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JWI_ESTIMATION_HEAD.Location_Code   " + Environment.NewLine + _
        "left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_JWI_ESTIMATION_HEAD.Vendor_Code  " + Environment.NewLine + _
        "left outer join TSPL_STRUCTURE_MASTER as TSPL_STRUCTURE_MASTERFATFAT on TSPL_STRUCTURE_MASTERFATFAT.Structure_Code = TSPL_JWI_ESTIMATION_HEAD.Item_Structure_FAT " + Environment.NewLine + _
        "left outer join TSPL_STRUCTURE_MASTER as TSPL_STRUCTURE_MASTERSNFSNF on TSPL_STRUCTURE_MASTERSNFSNF.Structure_Code = TSPL_JWI_ESTIMATION_HEAD.Item_Structure_SNF "
        str = clsCommon.ShowSelectForm("JWESTIMATE@FND", qry, "Code", whrcls, curcode, "TSPL_JWI_ESTIMATION_HEAD.Document_NO", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsJWEStimate
        Dim obj As clsJWEStimate = Nothing
        Dim qry As String = "SELECT  TSPL_JWI_ESTIMATION_HEAD.* FROM TSPL_JWI_ESTIMATION_HEAD where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JWI_ESTIMATION_HEAD.Document_NO = (select MIN(Document_NO) from TSPL_JWI_ESTIMATION_HEAD where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_JWI_ESTIMATION_HEAD.Document_NO = (select Max(Document_NO) from TSPL_JWI_ESTIMATION_HEAD where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_JWI_ESTIMATION_HEAD.Document_NO = (select Min(Document_NO) from TSPL_JWI_ESTIMATION_HEAD where Document_NO>'" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_JWI_ESTIMATION_HEAD.Document_NO = (select Max(Document_NO) from TSPL_JWI_ESTIMATION_HEAD where Document_NO<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_JWI_ESTIMATION_HEAD.Document_NO = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsJWEStimate()
            obj.Document_NO = clsCommon.myCstr(dt.Rows(0)("Document_NO"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Item_Structure_FAT = clsCommon.myCstr(dt.Rows(0)("Item_Structure_FAT"))
            obj.Formula_code_FAT = clsCommon.myCstr(dt.Rows(0)("Formula_code_FAT"))
            obj.Formula_Date_FAT = clsCommon.myCDate(dt.Rows(0)("Formula_Date_FAT"))
            obj.Formula_FAT = clsCommon.myCstr(dt.Rows(0)("Formula_FAT"))
            obj.Item_Structure_SNF = clsCommon.myCstr(dt.Rows(0)("Item_Structure_SNF"))
            obj.Formula_code_SNF = clsCommon.myCstr(dt.Rows(0)("Formula_code_SNF"))
            obj.Formula_Date_SNF = clsCommon.myCDate(dt.Rows(0)("Formula_Date_SNF"))
            obj.Formula_SNF = clsCommon.myCstr(dt.Rows(0)("Formula_SNF"))
            obj.Qty_Weighment = clsCommon.myCdbl(dt.Rows(0)("Qty_Weighment"))
            obj.FAT_KG_Weighment = clsCommon.myCdbl(dt.Rows(0)("FAT_KG_Weighment"))
            obj.SNF_KG_Weighment = clsCommon.myCdbl(dt.Rows(0)("SNF_KG_Weighment"))
            obj.Estimated_FAT_KG_Weighment = clsCommon.myCdbl(dt.Rows(0)("Estimated_FAT_KG_Weighment"))
            obj.Estimated_SNF_KG_Weighment = clsCommon.myCdbl(dt.Rows(0)("Estimated_SNF_KG_Weighment"))
            obj.FAT_KG_Raw_Item = clsCommon.myCdbl(dt.Rows(0)("FAT_KG_Raw_Item"))
            obj.SNF_KG_Raw_Item = clsCommon.myCdbl(dt.Rows(0)("SNF_KG_Raw_Item"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.ArrWeighment = clsJWIEstimateWeighment.GetData(obj.Document_NO, trans)
            obj.ArrFATProduction = clsJWIEstimateFATProduction.GetData(obj.Document_NO, trans)
            obj.ArrSNFProducion = clsJWIEstimateSNFProduction.GetData(obj.Document_NO, trans)
            obj.ArrRawItem = clsJWIEstimateRawItem.GetData(obj.Document_NO, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = "select status from TSPL_JWI_ESTIMATION_HEAD where Document_NO='" + strCode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                Throw New Exception("Posted document [" + strCode + "] can not be delete")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_JWI_ESTIMATION_SNF_PRODUCTION", "Document_NO", "TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER", "Document_NO", "TSPL_JWI_ESTIMATION_RAW_ITEM", "Document_NO", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER", "Document_NO", "TSPL_JWI_ESTIMATION_FAT_PRODUCTION", "Document_NO", "TSPL_JWI_ESTIMATION_WEIGHMENT", "Document_NO", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_JWI_ESTIMATION_HEAD", "Document_NO", trans)

            qry = "delete from TSPL_JWI_ESTIMATION_RAW_ITEM where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_SNF_PRODUCTION where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_FAT_PRODUCTION where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_WEIGHMENT where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JWI_ESTIMATION_HEAD where Document_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As clsJWEStimate = clsJWEStimate.GetData(strCode, NavigatorType.Current, trans)
        If obj.Status = ERPTransactionStatus.Posted Then
            Throw New Exception("already posted transaction")
        End If
        ''Allow To Post
        If obj.Estimated_FAT_KG_Weighment > 0 Then
            If obj.ArrFATProduction Is Nothing OrElse obj.ArrFATProduction.Count <= 0 Then
                Throw New Exception("Please Fill FAT Items")
            End If
            For Each objFAT As clsJWIEstimateFATProduction In obj.ArrFATProduction
                If clsCommon.myLen(objFAT.Batch_No) <= 0 Then
                    Throw New Exception("FAT Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objFAT.SNo) + Environment.NewLine + "Please enter Batch No")
                End If
                If clsCommon.myLen(objFAT.Item_Code) <= 0 Then
                    Throw New Exception("FAT Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objFAT.SNo) + Environment.NewLine + "Please select item")
                End If
                If clsCommon.myLen(objFAT.BOM_CODE) <= 0 Then
                    Throw New Exception("FAT Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objFAT.SNo) + Environment.NewLine + "Please select BOM")
                End If
                If objFAT.Qty <= 0 Then
                    Throw New Exception("FAT Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objFAT.SNo) + Environment.NewLine + "Please enter Qty")
                End If
            Next
        End If
        For Each objFAT As clsJWIEstimateFATProduction In obj.ArrFATProduction  ''By balwinder on 30/06/2021 ERO/30/06/21-001423
            If objFAT.Qty > 0 AndAlso objFAT.Estimated_Qty <= 0 Then
                Throw New Exception("FAT Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objFAT.SNo) + Environment.NewLine + "Estimated Qty can't be Zero")
            End If
        Next

        If obj.Estimated_SNF_KG_Weighment > 0 Then
            If obj.ArrSNFProducion Is Nothing OrElse obj.ArrSNFProducion.Count <= 0 Then
                Throw New Exception("Please Fill FAT Items")
            End If
            For Each objSNF As clsJWIEstimateSNFProduction In obj.ArrSNFProducion
                If clsCommon.myLen(objSNF.Batch_No) <= 0 Then
                    Throw New Exception("SNF Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objSNF.SNo) + Environment.NewLine + "Please enter Batch No")
                End If
                If clsCommon.myLen(objSNF.Item_Code) <= 0 Then
                    Throw New Exception("SNF Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objSNF.SNo) + Environment.NewLine + "Please select item")
                End If
                If clsCommon.myLen(objSNF.BOM_CODE) <= 0 Then
                    Throw New Exception("SNF Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objSNF.SNo) + Environment.NewLine + "Please select BOM")
                End If
                If objSNF.Qty <= 0 Then
                    Throw New Exception("SNF Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objSNF.SNo) + Environment.NewLine + "Please enter Qty")
                End If
            Next
        End If
        For Each objSNF As clsJWIEstimateSNFProduction In obj.ArrSNFProducion ''By balwinder on 30/06/2021 ERO/30/06/21-001423
            If objSNF.Qty > 0 AndAlso objSNF.Estimated_Qty <= 0 Then
                Throw New Exception("SNF Production." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objSNF.SNo) + Environment.NewLine + "Estimated Qty can't be Zero")
            End If
        Next
        ''End of Allow To Post

        SaveInventoryMovement(obj, trans, False)
        Dim CreateJVofPackingMaterialofJWInwardinJWEstimate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJVofPackingMaterialofJWInwardinJWEstimate, clsFixedParameterCode.CreateJVofPackingMaterialofJWInwardinJWEstimate, trans)) = 1, True, False)
        If CreateJVofPackingMaterialofJWInwardinJWEstimate Then
            CreateJournalEntry(trans, obj.Document_Date, obj.Document_NO, obj.Location_Code, "")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Status", 1)
        clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt "))
        clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_HEAD", OMInsertOrUpdate.Update, "TSPL_JWI_ESTIMATION_HEAD.Document_NO='" + obj.Document_NO + "'", trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_NO, "TSPL_JWI_ESTIMATION_HEAD", "Document_NO", trans)
        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal trans As SqlTransaction, ByVal Document_date As Date, ByVal Doc_Code As String, ByVal Location_code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            If clsCommon.myLen(Doc_Code) > 0 Then
                Dim VoucherNo As String
                If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    VoucherNo = strVourcherNoForRecreateOnly
                Else
                    VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='JW-ES' and Source_Doc_No='" & Doc_Code & "'", trans))
                End If

                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "' ", trans)
                End If

                Dim StrLoc As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnterLocationForJWEStimationOutPackingMaterial, clsFixedParameterCode.EnterLocationForJWEStimationOutPackingMaterial, trans))

                Dim qry As String = " select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption AS GL_account ,TSPL_INVENTORY_MOVEMENT.Avg_Cost,TSPL_JWI_ESTIMATION_HEAD.Location_Code ,TSPL_JWI_ESTIMATION_HEAD.Document_NO ,TSPL_JWI_ESTIMATION_HEAD.Document_Date   from TSPL_INVENTORY_MOVEMENT  " & Environment.NewLine &
" Left OUTER JOIN TSPL_JWI_ESTIMATION_HEAD On TSPL_JWI_ESTIMATION_HEAD.Document_NO =TSPL_INVENTORY_MOVEMENT.Source_Doc_No  " & Environment.NewLine &
" LEFT OUTER join tspl_item_master on TSPL_INVENTORY_MOVEMENT.Item_Code =tspl_item_master.Item_Code   " & Environment.NewLine &
" LEFT OUTER JOIN TSPL_PURCHASE_ACCOUNTS On TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=tspl_item_master.Purchase_Class_Code  " & Environment.NewLine &
" where TSPL_INVENTORY_MOVEMENT.Source_Doc_No ='" & clsCommon.myCstr(Doc_Code) & "' and isnull(tspl_item_master.Product_Type,'') <>'MI' AND TSPL_INVENTORY_MOVEMENT.InOut ='O' AND TSPL_INVENTORY_MOVEMENT.Location_Code <>'" & clsCommon.myCstr(Location_code) & "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim ArryLstGLAC As ArrayList = New ArrayList()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(clsCommon.myCstr(dr("Purchase_Class_Code"))) <= 0 Then
                            Throw New Exception("Please set purchase account set for item code " + clsCommon.myCstr(dr("Item_Code")))
                        End If
                        Dim strInv_Control_Account As String = clsCommon.myCstr(dr("Inv_Control_Account"))
                        If clsCommon.myLen(strInv_Control_Account) <= 0 Then
                            Throw New Exception("Please set Inv Control Account of Purchase account set " + clsCommon.myCstr(dr("Purchase_Class_Code")) + " For item " + dr("Item_Code"))
                        End If
                        Dim strGL_Account As String = clsCommon.myCstr(dr("GL_account"))
                        If clsCommon.myLen(strGL_Account) <= 0 Then
                            Throw New Exception("Please set RM Consumption Account of Purchase account set " + clsCommon.myCstr(dr("Purchase_Class_Code")) + " For item " + dr("Item_Code"))
                        End If

                        Dim dblAmt As Double = clsCommon.myCdbl(dr("Avg_Cost"))

                        strGL_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(strGL_Account, StrLoc, True, trans)
                        Dim Acc1() As String = {strGL_Account, 1 * dblAmt}
                        ArryLstGLAC.Add(Acc1)


                        strInv_Control_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(strInv_Control_Account, StrLoc, True, trans)
                        Dim Acc2() As String = {strInv_Control_Account, dblAmt * -1}
                        ArryLstGLAC.Add(Acc2)
                    Next

                    Dim GLDesc As String = "Journal Entry Against JW Estimate - Doc No." & Doc_Code & " "
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        transportSql.FunGrnlEntryWithTrans(StrLoc, False, VoucherNo, trans, Document_date, GLDesc, "JW-ES", "JW Inward", Doc_Code, "JW Estimation", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
                    Else
                        transportSql.FunGrnlEntryWithTrans(StrLoc, False, trans, Document_date, GLDesc, "JW-ES", "JW Inward", Doc_Code, "JW Estimation", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
                    End If
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveInventoryMovement(ByVal obj As clsJWEStimate, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
        Dim ArrOutItem As New List(Of String)
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        If UpdateInventory = True Then
            clsDBFuncationality.ExecuteNonQuery("delete from tspl_inventory_movement where trans_type='" + clsUserMgtCode.FrmSRNJobWorkEstimate + "' and source_doc_no='" + obj.Document_NO + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from tspl_inventory_movement_new where trans_type='" + clsUserMgtCode.FrmSRNJobWorkEstimate + "' and source_doc_no='" + obj.Document_NO + "'", trans)
        End If

        If obj.ArrWeighment IsNot Nothing AndAlso obj.ArrWeighment.Count > 0 Then
            For Each objtr As clsJWIEstimateWeighment In obj.ArrWeighment
                Dim objInventoryMovemnt As New clsInventoryMovement()
                Dim objInventoryMovemntNew As New clsInventoryMovementNew
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    objInventoryMovemntNew.InOut = "I"
                    objInventoryMovemntNew.Location_Code = obj.Location_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Name
                    objInventoryMovemntNew.Qty = objtr.Qty
                    objInventoryMovemntNew.UOM = objtr.UOM
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    objInventoryMovemntNew.FAT_Per = objtr.FAT_PER
                    objInventoryMovemntNew.FAT_KG = objtr.FAT_KG
                    objInventoryMovemntNew.SNF_KG = objtr.SNF_KG
                    objInventoryMovemntNew.SNF_Per = objtr.SNF_PER
                    objInventoryMovemntNew.CalculateAvgCost = False
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = True
                    objInventoryMovemntNew.Fat_Rate = 0
                    objInventoryMovemntNew.SNF_Rate = 0
                    objInventoryMovemntNew.Fat_Amt = 0
                    objInventoryMovemntNew.SNF_Amt = 0
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)

                    objInventoryMovemntNew = New clsInventoryMovementNew()
                    objInventoryMovemntNew.InOut = "O"
                    objInventoryMovemntNew.Location_Code = obj.Location_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Name
                    objInventoryMovemntNew.Qty = objtr.Qty
                    objInventoryMovemntNew.UOM = objtr.UOM
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    objInventoryMovemntNew.FAT_Per = objtr.FAT_PER
                    objInventoryMovemntNew.FAT_KG = objtr.FAT_KG
                    objInventoryMovemntNew.SNF_KG = objtr.SNF_KG
                    objInventoryMovemntNew.SNF_Per = objtr.SNF_PER
                    objInventoryMovemntNew.CalculateAvgCost = False
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = True
                    objInventoryMovemntNew.Fat_Rate = 0
                    objInventoryMovemntNew.SNF_Rate = 0
                    objInventoryMovemntNew.Fat_Amt = 0
                    objInventoryMovemntNew.SNF_Amt = 0
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)
                Else
                    Throw New Exception("All Weighment item type should be MI.Item Code" + objtr.Item_Code)
                End If
            Next
        End If

        If obj.ArrRawItem IsNot Nothing AndAlso obj.ArrRawItem.Count > 0 Then
            Dim CreateJVofPackingMaterialofJWInwardinJWEstimate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJVofPackingMaterialofJWInwardinJWEstimate, clsFixedParameterCode.CreateJVofPackingMaterialofJWInwardinJWEstimate, trans)) = 1, True, False)
            For Each objtr As clsJWIEstimateRawItem In obj.ArrRawItem
                Dim objInventoryMovemntNew As New clsInventoryMovementNew
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Raw_Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") <> CompairStringResult.Equal Then
                    ArrOutItem.Add(objtr.Raw_Item_Code)
                    If CreateJVofPackingMaterialofJWInwardinJWEstimate = False Then
                        Dim objInventoryMovemnt As New clsInventoryMovement()
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = obj.Location_Code
                        objInventoryMovemnt.Item_Code = objtr.Raw_Item_Code
                        objInventoryMovemnt.Item_Desc = objtr.Raw_Item_Name
                        objInventoryMovemnt.Qty = objtr.Raw_Item_Qty
                        objInventoryMovemnt.UOM = objtr.Raw_Item_UOM
                        objInventoryMovemnt.MRP = Nothing
                        objInventoryMovemnt.Add_Cost = Nothing
                        objInventoryMovemnt.FAT_Per = objtr.Raw_Item_FAT_Per
                        objInventoryMovemnt.FAT_KG = objtr.Raw_Item_FAT_KG
                        objInventoryMovemnt.SNF_KG = objtr.Raw_Item_SNF_KG
                        objInventoryMovemnt.SNF_Per = objtr.Raw_Item_SNF_Per
                        objInventoryMovemnt.CalculateAvgCost = False
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    Else
                        '-------------richa
                        Dim BalanceQty As Decimal = objtr.Raw_Item_Qty
                        Dim strItemLoc As String = ""
                        Dim StrLoc As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnterLocationForJWEStimationOutPackingMaterial, clsFixedParameterCode.EnterLocationForJWEStimationOutPackingMaterial, trans))
                        Dim CheckStockServerDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)) = 1, True, False)
                        Dim settTankerDispatchAvgFATSNFPer As Integer = settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, trans)) = 1)
                        Dim strqry As String = "select Location_Code   from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(StrLoc) & "'"
                        strqry += " and TSPL_Location_MASTER.Rejected_Type='N' and Is_Jobwork =0 order by TSPL_Location_MASTER.Location_Code"


                        Dim dtSubLocation As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
                        dtSubLocation.Rows.Add(StrLoc)
                        For Each drSubLocation As DataRow In dtSubLocation.Rows
                            Dim From_SubLocation_YN As Integer = 1
                            If clsCommon.CompairString(StrLoc, clsCommon.myCstr(drSubLocation("location_code"))) = CompairStringResult.Equal Then
                                From_SubLocation_YN = 2
                            End If
                            Dim dtStock As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objtr.Raw_Item_Code, StrLoc, clsCommon.myCstr(drSubLocation("location_code")), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), objtr.TR_Date), trans, objtr.Raw_Item_UOM, From_SubLocation_YN)
                            If dtStock IsNot Nothing AndAlso dtStock.Rows.Count > 0 Then
                                If clsCommon.myCdbl(dtStock.Rows(0)("qty")) > 0 Then
                                    Dim Product_Type As String = clsItemMaster.GetItemProductType(objtr.Raw_Item_Code, trans)
                                    Dim FutureBalanceQty As Decimal = 0
                                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                        FutureBalanceQty = clsInventoryMovementNew.getBalance(objtr.Raw_Item_Code, clsLocation.GetMainLocationMilk(clsCommon.myCstr(drSubLocation("location_code")), trans), clsCommon.myCstr(drSubLocation("location_code")), "", objtr.TR_Date, trans, objtr.Raw_Item_UOM)
                                    Else
                                        FutureBalanceQty = clsItemLocationDetails.getBalance(objtr.Raw_Item_Code, clsCommon.myCstr(drSubLocation("location_code")), "", objtr.TR_Date, trans, objtr.Raw_Item_UOM, 0)
                                    End If
                                    Dim DecimalPoint As Integer = 3
                                    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                                    If FutureBalanceQty < clsCommon.myCdbl(dtStock.Rows(0)("qty")) Then
                                        Dim decimalRatio As Decimal = FutureBalanceQty / clsCommon.myCdbl(dtStock.Rows(0)("qty"))
                                        dtStock.Rows(0)("qty") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("qty")) * decimalRatio, DecimalPoint)
                                        dtStock.Rows(0)("fat_kg") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("fat_kg")) * decimalRatio, DecimalPoint)
                                        dtStock.Rows(0)("snf_kg") = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("snf_kg")) * decimalRatio, DecimalPoint)
                                    End If
                                    If clsCommon.myCdbl(dtStock.Rows(0)("qty")) > 1 AndAlso BalanceQty > 0 Then
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        objInventoryMovemnt.InOut = "O"
                                        objInventoryMovemnt.Location_Code = clsCommon.myCstr(drSubLocation("location_code"))
                                        objInventoryMovemnt.Item_Code = objtr.Raw_Item_Code
                                        objInventoryMovemnt.Item_Desc = objtr.Raw_Item_Name
                                        objInventoryMovemnt.UOM = objtr.Raw_Item_UOM
                                        objInventoryMovemnt.MRP = Nothing
                                        objInventoryMovemnt.Add_Cost = Nothing

                                        Dim avail_qty As Decimal = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("qty")), DecimalPoint)
                                        Dim avail_fat_kg As Decimal = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("fat_kg")), DecimalPoint)
                                        Dim avail_snf_kg As Decimal = Math.Round(clsCommon.myCdbl(dtStock.Rows(0)("snf_kg")), DecimalPoint)
                                        Dim avail_fat_pers As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(objtr.Raw_Item_Code, objtr.Raw_Item_UOM, avail_qty, avail_fat_kg, trans, settTankerDispatchAvgFATSNFPer)
                                        Dim avail_snf_pers As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(objtr.Raw_Item_Code, objtr.Raw_Item_UOM, avail_qty, avail_snf_kg, trans, settTankerDispatchAvgFATSNFPer)
                                        If BalanceQty > avail_qty Then
                                            objInventoryMovemnt.Qty = avail_qty
                                            objInventoryMovemnt.FAT_KG = avail_fat_kg
                                            objInventoryMovemnt.FAT_Per = avail_fat_pers
                                            objInventoryMovemnt.SNF_KG = avail_snf_kg
                                            objInventoryMovemnt.SNF_Per = avail_snf_pers
                                            BalanceQty -= objtr.Raw_Item_Qty
                                        Else
                                            objInventoryMovemnt.Qty = BalanceQty
                                            objInventoryMovemnt.FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(objtr.Raw_Item_Code, objtr.Raw_Item_UOM, BalanceQty, avail_fat_pers, trans)
                                            objInventoryMovemnt.FAT_Per = avail_fat_pers
                                            objInventoryMovemnt.SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(objtr.Raw_Item_Code, objtr.Raw_Item_UOM, BalanceQty, avail_snf_pers, trans)
                                            objInventoryMovemnt.SNF_Per = avail_snf_pers
                                            BalanceQty = 0
                                        End If
                                        objInventoryMovemnt.CalculateAvgCost = False
                                        Dim objCost As New MIlkComponentType
                                        objCost = clsInventoryMovementNew.GetAvgCost(Product_Type, objtr.Raw_Item_Code, objInventoryMovemnt.Location_Code, objInventoryMovemnt.Qty, objtr.Raw_Item_UOM, objInventoryMovemnt.FAT_KG, objInventoryMovemnt.SNF_KG, obj.Document_Date, obj.Document_Date, False, trans)
                                        objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                                        objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                                        'objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                                        'objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                                        objInventoryMovemnt.Avg_Cost = objCost.FAT_Cost + objCost.SNF_Cost
                                        objInventoryMovemnt.Basic_Cost = If(objInventoryMovemnt.Qty <= 0, 0, objInventoryMovemnt.Avg_Cost / objInventoryMovemnt.Qty)
                                        ArrInventoryMovement.Add(objInventoryMovemnt)

                                        'to in inventory movement of jobwork location
                                        'Dim objInventoryMovemnt1 As New clsInventoryMovement()
                                        'objInventoryMovemnt1.InOut = "I"
                                        'objInventoryMovemnt1.Location_Code = obj.Location_Code
                                        'objInventoryMovemnt1.Item_Code = objInventoryMovemnt.Item_Code
                                        'objInventoryMovemnt1.Item_Desc = objInventoryMovemnt.Item_Desc
                                        'objInventoryMovemnt1.Qty = objInventoryMovemnt.Qty
                                        'objInventoryMovemnt1.UOM = objInventoryMovemnt.UOM
                                        'objInventoryMovemnt1.MRP = Nothing
                                        'objInventoryMovemnt1.Add_Cost = Nothing
                                        'objInventoryMovemnt1.FAT_Per = objInventoryMovemnt.FAT_Per
                                        'objInventoryMovemnt1.FAT_KG = objInventoryMovemnt.FAT_KG
                                        'objInventoryMovemnt1.SNF_KG = objInventoryMovemnt.SNF_KG
                                        'objInventoryMovemnt1.SNF_Per = objInventoryMovemnt.SNF_Per
                                        'objInventoryMovemnt1.CalculateAvgCost = False
                                        'objInventoryMovemnt1.Avg_Cost = objInventoryMovemnt.Avg_Cost
                                        'objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt.Basic_Cost
                                        'ArrInventoryMovement.Add(objInventoryMovemnt1)


                                        'to out inventory movement from jobwork location
                                        'objInventoryMovemnt1 = New clsInventoryMovement()
                                        'objInventoryMovemnt1.InOut = "O"
                                        'objInventoryMovemnt1.Location_Code = obj.Location_Code
                                        'objInventoryMovemnt1.Item_Code = objInventoryMovemnt.Item_Code
                                        'objInventoryMovemnt1.Item_Desc = objInventoryMovemnt.Item_Desc
                                        'objInventoryMovemnt1.Qty = objInventoryMovemnt.Qty
                                        'objInventoryMovemnt1.UOM = objInventoryMovemnt.UOM
                                        'objInventoryMovemnt1.MRP = Nothing
                                        'objInventoryMovemnt1.Add_Cost = Nothing
                                        'objInventoryMovemnt1.FAT_Per = objInventoryMovemnt.FAT_Per
                                        'objInventoryMovemnt1.FAT_KG = objInventoryMovemnt.FAT_KG
                                        'objInventoryMovemnt1.SNF_KG = objInventoryMovemnt.SNF_KG
                                        'objInventoryMovemnt1.SNF_Per = objInventoryMovemnt.SNF_Per
                                        'objInventoryMovemnt1.CalculateAvgCost = False
                                        'objInventoryMovemnt1.Avg_Cost = objInventoryMovemnt.Avg_Cost
                                        'objInventoryMovemnt1.Basic_Cost = objInventoryMovemnt.Basic_Cost

                                        'ArrInventoryMovement.Add(objInventoryMovemnt1)


                                        If BalanceQty <= 0 Then
                                            Exit For
                                        End If

                                    End If
                                End If
                            End If
                        Next

                        If BalanceQty > 0 Then
                            Throw New Exception("Item [" + objtr.Raw_Item_Code + "].Qty [" + clsCommon.myCstr(objtr.Raw_Item_Qty) + "] Not available")
                        Else
                            Continue For
                        End If

                        '----------------------
                    End If
                End If
            Next
        End If
        If obj.ArrFATProduction IsNot Nothing AndAlso obj.ArrFATProduction.Count > 0 Then
            For Each objtr As clsJWIEstimateFATProduction In obj.ArrFATProduction
                Dim objInventoryMovemnt As New clsInventoryMovement()
                Dim objInventoryMovemntNew As New clsInventoryMovementNew
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    objInventoryMovemntNew.InOut = "I"
                    objInventoryMovemntNew.Location_Code = obj.Location_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Name
                    objInventoryMovemntNew.Qty = objtr.Qty
                    objInventoryMovemntNew.UOM = objtr.UOM
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    objInventoryMovemntNew.FAT_Per = objtr.FAT_Per
                    objInventoryMovemntNew.FAT_KG = objtr.FAT_KG
                    objInventoryMovemntNew.SNF_KG = 0
                    objInventoryMovemntNew.SNF_Per = 0
                    objInventoryMovemntNew.CalculateAvgCost = False
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = True
                    objInventoryMovemntNew.Fat_Rate = 0
                    objInventoryMovemntNew.SNF_Rate = 0
                    objInventoryMovemntNew.Fat_Amt = 0
                    objInventoryMovemntNew.SNF_Amt = 0
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)
                Else
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Location_Code
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Name
                    objInventoryMovemnt.Qty = objtr.Qty
                    objInventoryMovemnt.UOM = objtr.UOM
                    objInventoryMovemnt.MRP = Nothing
                    objInventoryMovemnt.Add_Cost = Nothing
                    objInventoryMovemnt.FAT_Per = objtr.FAT_Per
                    objInventoryMovemnt.FAT_KG = objtr.FAT_KG
                    objInventoryMovemnt.SNF_KG = 0
                    objInventoryMovemnt.SNF_Per = 0
                    objInventoryMovemnt.CalculateAvgCost = False
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
                If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) = True Then
                    Dim objBatchInv As clsBatchInventory = New clsBatchInventory()
                    objBatchInv.arr = New List(Of clsBatchInventory)
                    objBatchInv.Parent_Line_No = objtr.SNo
                    objBatchInv.Line_No = 1
                    objBatchInv.Batch_No = objtr.Batch_No
                    objBatchInv.Manufacture_Date = obj.Document_Date
                    objBatchInv.Expiry_Date = obj.Document_Date.AddDays(clsItemMaster.GetSelfLife(objtr.Item_Code, trans))
                    objBatchInv.Qty = objtr.Qty
                    objBatchInv.UOM = objtr.UOM
                    objBatchInv.Item_Code = objtr.Item_Code
                    objBatchInv.Location_Code = obj.Location_Code
                    objBatchInv.Manual_BatchNo = objtr.Batch_No
                    Dim arrB As New List(Of clsBatchInventory)
                    arrB.Add(objBatchInv)
                    clsBatchInventory.SaveData(clsUserMgtCode.FrmSRNJobWorkEstimate, obj.Document_NO, obj.Document_Date, "I", objtr.Item_Code, obj.Location_Code, objtr.SNo, 0, objtr.UOM, arrB, trans)
                End If
            Next
        End If
        If obj.ArrSNFProducion IsNot Nothing AndAlso obj.ArrSNFProducion.Count > 0 Then
            For Each objtr As clsJWIEstimateSNFProduction In obj.ArrSNFProducion
                Dim objInventoryMovemnt As New clsInventoryMovement()
                Dim objInventoryMovemntNew As New clsInventoryMovementNew
                Dim strProductType As String
                strProductType = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    objInventoryMovemntNew.InOut = "I"
                    objInventoryMovemntNew.Location_Code = obj.Location_Code
                    objInventoryMovemntNew.Item_Code = objtr.Item_Code
                    objInventoryMovemntNew.Item_Desc = objtr.Item_Name
                    objInventoryMovemntNew.Qty = objtr.Qty
                    objInventoryMovemntNew.UOM = objtr.UOM
                    objInventoryMovemntNew.MRP = Nothing
                    objInventoryMovemntNew.Add_Cost = Nothing
                    objInventoryMovemntNew.Net_Cost = Nothing
                    objInventoryMovemntNew.FAT_Per = 0
                    objInventoryMovemntNew.FAT_KG = 0
                    objInventoryMovemntNew.SNF_KG = objtr.SNF_KG
                    objInventoryMovemntNew.SNF_Per = objtr.SNF_Per
                    objInventoryMovemntNew.CalculateAvgCost = False
                    objInventoryMovemntNew.DonNotCalculateAvgFATSNFCost = True
                    objInventoryMovemntNew.Fat_Rate = 0
                    objInventoryMovemntNew.SNF_Rate = 0
                    objInventoryMovemntNew.Fat_Amt = 0
                    objInventoryMovemntNew.SNF_Amt = 0
                    objInventoryMovemntNew.MFG_Date = Nothing
                    objInventoryMovemntNew.Expiry_Date = Nothing
                    ArrInventoryMovementNew.Add(objInventoryMovemntNew)
                Else
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Location_Code
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Name
                    objInventoryMovemnt.Qty = objtr.Qty
                    objInventoryMovemnt.UOM = objtr.UOM
                    objInventoryMovemnt.MRP = Nothing
                    objInventoryMovemnt.Add_Cost = Nothing
                    objInventoryMovemnt.FAT_Per = 0
                    objInventoryMovemnt.FAT_KG = 0
                    objInventoryMovemnt.SNF_KG = objtr.SNF_KG
                    objInventoryMovemnt.SNF_Per = objtr.SNF_Per
                    objInventoryMovemnt.CalculateAvgCost = False
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
                If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) = True Then
                    Dim objBatchInv As clsBatchInventory = New clsBatchInventory()
                    objBatchInv.arr = New List(Of clsBatchInventory)
                    objBatchInv.Parent_Line_No = objtr.SNo
                    objBatchInv.Line_No = 1
                    objBatchInv.Batch_No = objtr.Batch_No
                    objBatchInv.Manufacture_Date = obj.Document_Date
                    objBatchInv.Expiry_Date = obj.Document_Date.AddDays(clsItemMaster.GetSelfLife(objtr.Item_Code, trans))
                    objBatchInv.Qty = objtr.Qty
                    objBatchInv.UOM = objtr.UOM
                    objBatchInv.Item_Code = objtr.Item_Code
                    objBatchInv.Location_Code = obj.Location_Code
                    objBatchInv.Manual_BatchNo = objtr.Batch_No
                    Dim arrB As New List(Of clsBatchInventory)
                    arrB.Add(objBatchInv)
                    clsBatchInventory.SaveData(clsUserMgtCode.FrmSRNJobWorkEstimate, obj.Document_NO, obj.Document_Date, "I", objtr.Item_Code, obj.Location_Code, objtr.SNo, 0, objtr.UOM, arrB, trans)
                End If
            Next
        End If


        If ArrInventoryMovement.Count > 0 Then
            clsInventoryMovement.SaveData(clsUserMgtCode.FrmSRNJobWorkEstimate, obj.Document_NO, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        End If
        If ArrInventoryMovementNew.Count > 0 Then
            clsInventoryMovementNew.SaveData(clsUserMgtCode.FrmSRNJobWorkEstimate, obj.Document_NO, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
        End If

        'If ArrOutItem IsNot Nothing AndAlso ArrOutItem.Count > 0 Then
        '    For Each strIcode As String In ArrOutItem
        '        Dim bal As Decimal = clsItemLocationDetails.getBalance(strIcode, obj.Location_Code, "", obj.Document_Date, trans, clsItemMaster.GetStockUnit(strIcode, trans), 0)
        '        If bal < 0 Then
        '            Throw New Exception("Stock Balance going to negative " + Environment.NewLine + " Item - " + strIcode + " and location - " + obj.Location_Code)
        '        End If
        '    Next
        'End If

        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Document No not found to unpost")
            End If

            Dim obj As clsJWEStimate = clsJWEStimate.GetData(strCode, NavigatorType.Current, trans)
            If Not obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction should be Posted")
            End If
            Dim qry As String = String.Empty
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='JW-ES' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            clsBatchInventory.DeleteData(clsUserMgtCode.FrmSRNJobWorkEstimate, obj.Document_NO, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_NO), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_NO), "tspl_inventory_movement_new", "Source_Doc_No", trans)
            qry = "delete from tspl_inventory_movement where trans_type='" + clsUserMgtCode.FrmSRNJobWorkEstimate + "' and source_doc_no='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + clsUserMgtCode.FrmSRNJobWorkEstimate + "' and source_doc_no='" + obj.Document_NO + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Post_Date", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Post_By", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_HEAD", OMInsertOrUpdate.Update, "TSPL_JWI_ESTIMATION_HEAD.Document_NO='" + obj.Document_NO + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_NO, "TSPL_JWI_ESTIMATION_HEAD", "Document_NO", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsJWIEstimateWeighment
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Document_NO As String = Nothing
    Public SNo As Integer
    Public Parent_SNo As Integer
    Public Weighment_Code As String
    Public Weighment_Date As DateTime
    Public Tanker_No As String ''Not a table column
    Public Item_Code As String
    Public Item_Name As String ''Not a table column
    Public Qty As Decimal = 0
    Public UOM As String = Nothing
    Public FAT_PER As Decimal = 0
    Public CLR As Decimal = 0
    Public Correction_Factor As Decimal = 0
    Public SNF_PER As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public Estimated_FAT_KG As Decimal = 0
    Public Estimated_SNF_KG As Decimal = 0
    Public GE_FAT_PER As Decimal = 0
    Public GE_SNF_PER As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsJWIEstimateWeighment), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIEstimateWeighment In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Document_NO", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Parent_SNo", obj.Parent_SNo)
                clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT_PER", obj.FAT_PER)
                clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                clsCommon.AddColumnsForChange(coll, "Correction_Factor", obj.Correction_Factor)
                clsCommon.AddColumnsForChange(coll, "SNF_PER", obj.SNF_PER)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Estimated_FAT_KG", obj.Estimated_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Estimated_SNF_KG", obj.Estimated_SNF_KG)
                clsCommon.AddColumnsForChange(coll, "GE_FAT_PER", obj.GE_FAT_PER)
                clsCommon.AddColumnsForChange(coll, "GE_SNF_PER", obj.GE_SNF_PER)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_WEIGHMENT", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateWeighment)
        Dim arrObj As List(Of clsJWIEstimateWeighment) = Nothing
        Try
            Dim obj As clsJWIEstimateWeighment = Nothing
            Dim qry As String = "select TSPL_JWI_ESTIMATION_WEIGHMENT.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_GENERAL_WEIGHMENT_DETAIL.Vehicle_No_Manual from TSPL_JWI_ESTIMATION_WEIGHMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JWI_ESTIMATION_WEIGHMENT.item_code left outer join TSPL_GENERAL_WEIGHMENT_DETAIL on TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No=TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code where Document_NO='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateWeighment)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateWeighment()
                    obj.TR_Code = clsCommon.myCstr(dt.Rows(i)("TR_Code"))
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Parent_SNo = clsCommon.myCdbl(dt.Rows(i)("Parent_SNo"))
                    obj.Weighment_Code = clsCommon.myCstr(dt.Rows(i)("Weighment_Code"))
                    obj.Weighment_Date = clsCommon.myCDate(dt.Rows(i)("Weighment_Date"))
                    obj.Tanker_No = clsCommon.myCstr(dt.Rows(i)("Vehicle_No_Manual"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.FAT_PER = clsCommon.myCdbl(dt.Rows(i)("FAT_PER"))
                    obj.CLR = clsCommon.myCdbl(dt.Rows(i)("CLR"))
                    obj.Correction_Factor = clsCommon.myCdbl(dt.Rows(i)("Correction_Factor"))
                    obj.SNF_PER = clsCommon.myCdbl(dt.Rows(i)("SNF_PER"))
                    obj.FAT_KG = clsCommon.myCdbl(dt.Rows(i)("FAT_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.Estimated_FAT_KG = clsCommon.myCdbl(dt.Rows(i)("Estimated_FAT_KG"))
                    obj.Estimated_SNF_KG = clsCommon.myCdbl(dt.Rows(i)("Estimated_SNF_KG"))
                    obj.GE_SNF_PER = clsCommon.myCdbl(dt.Rows(i)("GE_SNF_PER"))
                    obj.GE_FAT_PER = clsCommon.myCdbl(dt.Rows(i)("GE_FAT_PER"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class

Public Class clsJWIEstimateFATProduction
#Region "Variables"
    Public TR_Code As String = Nothing
    Public TR_Date As DateTime
    Public Document_NO As String = Nothing
    Public SNo As Integer
    Public Batch_No As String
    Public Item_Code As String
    Public Item_Name As String = Nothing
    Public BOM_CODE As String
    Public Qty As Decimal = 0
    Public UOM As String = Nothing
    Public FAT_Per As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public Estimated_Qty As Decimal = 0
    Public Estimated_FAT_KG As Decimal = 0
    Public ArrQC As List(Of clsJWIEstimateFATProductionQCParam) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsJWIEstimateFATProduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIEstimateFATProduction In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Date", clsCommon.GetPrintDate(obj.TR_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Document_NO", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM, True)
                clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Estimated_Qty", obj.Estimated_Qty)
                clsCommon.AddColumnsForChange(coll, "Estimated_FAT_KG", obj.Estimated_FAT_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_FAT_PRODUCTION", OMInsertOrUpdate.Insert, "", trans)
                clsJWIEstimateFATProductionQCParam.saveData(strDocNo, obj.TR_Code, obj.ArrQC, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateFATProduction)
        Dim arrObj As List(Of clsJWIEstimateFATProduction) = Nothing
        Try
            Dim obj As clsJWIEstimateFATProduction = Nothing
            Dim qry As String = "select TSPL_JWI_ESTIMATION_FAT_PRODUCTION.*,TSPL_ITEM_MASTER.Item_Desc as Item_Name from TSPL_JWI_ESTIMATION_FAT_PRODUCTION left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code where TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_NO='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateFATProduction)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateFATProduction()
                    obj.TR_Code = clsCommon.myCstr(dt.Rows(i)("TR_Code"))
                    obj.TR_Date = clsCommon.myCDate(dt.Rows(i)("TR_Date"))
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Batch_No = clsCommon.myCstr(dt.Rows(i)("Batch_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dt.Rows(i)("Item_Name"))
                    obj.BOM_CODE = clsCommon.myCstr(dt.Rows(i)("BOM_CODE"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.FAT_Per = clsCommon.myCdbl(dt.Rows(i)("FAT_Per"))
                    obj.FAT_KG = clsCommon.myCdbl(dt.Rows(i)("FAT_KG"))
                    obj.Estimated_Qty = clsCommon.myCdbl(dt.Rows(i)("Estimated_Qty"))
                    obj.Estimated_FAT_KG = clsCommon.myCdbl(dt.Rows(i)("Estimated_FAT_KG"))
                    obj.ArrQC = clsJWIEstimateFATProductionQCParam.GetData(obj.TR_Code, trans)
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class

Public Class clsJWIEstimateFATProductionQCParam
#Region "Variables"
    Public Document_NO As String = String.Empty
    Public Parent_TR_Code As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
#End Region

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRNo As String, ByVal arrObj As List(Of clsJWIEstimateFATProductionQCParam)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, strTRNo, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRNo As String, ByVal arrObj As List(Of clsJWIEstimateFATProductionQCParam), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsJWIEstimateFATProductionQCParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_NO", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Parent_TR_Code", strTRNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateFATProductionQCParam)
        Dim arrObj As List(Of clsJWIEstimateFATProductionQCParam) = Nothing
        Try
            Dim obj As clsJWIEstimateFATProductionQCParam = Nothing
            Dim qry As String = "select * from TSPL_JWI_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER where Parent_TR_Code='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateFATProductionQCParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateFATProductionQCParam()
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.Parent_TR_Code = clsCommon.myCstr(dt.Rows(i)("Parent_TR_Code"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class


Public Class clsJWIEstimateSNFProduction
#Region "Variables"
    Public TR_Code As String = Nothing
    Public TR_Date As DateTime
    Public Document_NO As String = Nothing
    Public SNo As Integer
    Public Batch_No As String
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public BOM_CODE As String = Nothing
    Public Qty As Decimal = 0
    Public UOM As String = Nothing
    Public SNF_Per As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public Estimated_Qty As Decimal = 0
    Public Estimated_SNF_KG As Decimal = 0
    Public ArrQC As List(Of clsJWIEstimateSNFProductionQCParam) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsJWIEstimateSNFProduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIEstimateSNFProduction In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Document_NO", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM, True)
                clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Estimated_Qty", obj.Estimated_Qty)
                clsCommon.AddColumnsForChange(coll, "Estimated_SNF_KG", obj.Estimated_SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_SNF_PRODUCTION", OMInsertOrUpdate.Insert, "", trans)
                clsJWIEstimateSNFProductionQCParam.saveData(strDocNo, obj.TR_Code, obj.ArrQC, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateSNFProduction)
        Dim arrObj As List(Of clsJWIEstimateSNFProduction) = Nothing
        Try
            Dim obj As clsJWIEstimateSNFProduction = Nothing
            Dim qry As String = "select TSPL_JWI_ESTIMATION_SNF_PRODUCTION.*,TSPL_ITEM_MASTER.Item_Desc as Item_Name from TSPL_JWI_ESTIMATION_SNF_PRODUCTION left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JWI_ESTIMATION_SNF_PRODUCTION.item_code where TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_NO='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateSNFProduction)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateSNFProduction()
                    obj.TR_Code = clsCommon.myCstr(dt.Rows(i)("TR_Code"))
                    obj.TR_Date = clsCommon.myCDate(dt.Rows(i)("TR_Date"))
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Batch_No = clsCommon.myCstr(dt.Rows(i)("Batch_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Name = clsCommon.myCstr(dt.Rows(i)("Item_Name"))
                    obj.BOM_CODE = clsCommon.myCstr(dt.Rows(i)("BOM_CODE"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.SNF_Per = clsCommon.myCdbl(dt.Rows(i)("SNF_Per"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.Estimated_Qty = clsCommon.myCdbl(dt.Rows(i)("Estimated_Qty"))
                    obj.Estimated_SNF_KG = clsCommon.myCdbl(dt.Rows(i)("Estimated_SNF_KG"))
                    obj.ArrQC = clsJWIEstimateSNFProductionQCParam.GetData(obj.TR_Code, trans)
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class

Public Class clsJWIEstimateSNFProductionQCParam
#Region "Variables"
    Public Document_NO As String = String.Empty
    Public Parent_TR_Code As String = String.Empty
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty
    Public LINE_NO As Integer = 0
#End Region

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRNo As String, ByVal arrObj As List(Of clsJWIEstimateSNFProductionQCParam)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, strTRNo, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRNo As String, ByVal arrObj As List(Of clsJWIEstimateSNFProductionQCParam), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsJWIEstimateSNFProductionQCParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_NO", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Parent_TR_Code", strTRNo)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateSNFProductionQCParam)
        Dim arrObj As List(Of clsJWIEstimateSNFProductionQCParam) = Nothing
        Try
            Dim obj As clsJWIEstimateSNFProductionQCParam = Nothing
            Dim qry As String = "select * from TSPL_JWI_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER where Parent_TR_Code='" & strQCNo & "' order by LINE_NO"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateSNFProductionQCParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateSNFProductionQCParam()
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.Parent_TR_Code = clsCommon.myCstr(dt.Rows(i)("Parent_TR_Code"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.LINE_NO = clsCommon.myCdbl(dt.Rows(i)("LINE_NO"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class

Public Class clsJWIEstimateRawItem
#Region "Variables"
    Public TR_Code As String = Nothing
    Public TR_Date As DateTime
    Public Document_NO As String = Nothing
    Public SNo As Integer
    Public Parent_SNo As Integer
    Public Parent_Type As String
    Public Main_Item_Code As String
    Public Main_Item_Name As String ''Not a table column
    Public Main_Item_Qty As Decimal = 0
    Public Main_Item_UOM As String = Nothing
    Public Main_BOM_CODE As String = Nothing
    Public Raw_Item_Code As String
    Public Raw_Item_Name As String ''Not a table column
    Public Raw_Item_Qty As Decimal = 0
    Public Raw_Item_UOM As String = Nothing
    Public Raw_Item_FAT_Per As Decimal = 0
    Public Raw_Item_FAT_KG As Decimal = 0
    Public Raw_Item_SNF_Per As Decimal = 0
    Public Raw_Item_SNF_KG As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsJWIEstimateRawItem), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsJWIEstimateRawItem In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "TR_Date", clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "Document_NO", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Parent_SNo", obj.Parent_SNo)
                clsCommon.AddColumnsForChange(coll, "Parent_Type", obj.Parent_Type)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Qty", obj.Main_Item_Qty)
                clsCommon.AddColumnsForChange(coll, "Main_Item_UOM", obj.Main_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Main_BOM_CODE", obj.Main_BOM_CODE)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_Code", obj.Raw_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_Qty", obj.Raw_Item_Qty)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_UOM", obj.Raw_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_FAT_Per", obj.Raw_Item_FAT_Per)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_FAT_KG", obj.Raw_Item_FAT_KG)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_SNF_Per", obj.Raw_Item_SNF_Per)
                clsCommon.AddColumnsForChange(coll, "Raw_Item_SNF_KG", obj.Raw_Item_SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JWI_ESTIMATION_RAW_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strQCNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJWIEstimateRawItem)
        Dim arrObj As List(Of clsJWIEstimateRawItem) = Nothing
        Try
            Dim obj As clsJWIEstimateRawItem = Nothing
            Dim qry As String = "select TSPL_JWI_ESTIMATION_RAW_ITEM.*,TSPL_ITEM_MASTERMain.Item_Desc as Main_Item_Name,TSPL_ITEM_MASTERRaw.Item_Desc as Raw_Item_Name  from TSPL_JWI_ESTIMATION_RAW_ITEM left outer join TSPL_ITEM_MASTER as TSPL_ITEM_MASTERMain on  TSPL_ITEM_MASTERMain.Item_Code=TSPL_JWI_ESTIMATION_RAW_ITEM.Main_Item_Code left outer join TSPL_ITEM_MASTER as TSPL_ITEM_MASTERRaw on  TSPL_ITEM_MASTERRaw.Item_Code=TSPL_JWI_ESTIMATION_RAW_ITEM.Raw_Item_Code where Document_NO='" & strQCNo & "' order by SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJWIEstimateRawItem)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJWIEstimateRawItem()
                    obj.Document_NO = clsCommon.myCstr(dt.Rows(i)("Document_NO"))
                    obj.TR_Code = clsCommon.myCstr(dt.Rows(i)("TR_Code"))
                    obj.TR_Date = clsCommon.myCDate(dt.Rows(i)("TR_Date"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Parent_SNo = clsCommon.myCdbl(dt.Rows(i)("Parent_SNo"))
                    obj.Parent_Type = clsCommon.myCstr(dt.Rows(i)("Parent_Type"))
                    obj.Main_Item_Code = clsCommon.myCstr(dt.Rows(i)("Main_Item_Code"))
                    obj.Main_Item_Name = clsCommon.myCstr(dt.Rows(i)("Main_Item_Name"))
                    obj.Main_Item_Qty = clsCommon.myCdbl(dt.Rows(i)("Main_Item_Qty"))
                    obj.Main_Item_UOM = clsCommon.myCstr(dt.Rows(i)("Main_Item_UOM"))
                    obj.Main_BOM_CODE = clsCommon.myCstr(dt.Rows(i)("Main_BOM_CODE"))
                    obj.Raw_Item_Code = clsCommon.myCstr(dt.Rows(i)("Raw_Item_Code"))
                    obj.Raw_Item_Name = clsCommon.myCstr(dt.Rows(i)("Raw_Item_Name"))
                    obj.Raw_Item_Qty = clsCommon.myCdbl(dt.Rows(i)("Raw_Item_Qty"))
                    obj.Raw_Item_UOM = clsCommon.myCstr(dt.Rows(i)("Raw_Item_UOM"))
                    obj.Raw_Item_FAT_Per = clsCommon.myCdbl(dt.Rows(i)("Raw_Item_FAT_Per"))
                    obj.Raw_Item_FAT_KG = clsCommon.myCdbl(dt.Rows(i)("Raw_Item_FAT_KG"))
                    obj.Raw_Item_SNF_Per = clsCommon.myCdbl(dt.Rows(i)("Raw_Item_SNF_Per"))
                    obj.Raw_Item_SNF_KG = clsCommon.myCdbl(dt.Rows(i)("Raw_Item_SNF_KG"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class
