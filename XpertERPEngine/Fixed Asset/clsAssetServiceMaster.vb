'==============BM00000003063,Updated By Rohit===========
Imports common
Imports System.Data.SqlClient

Public Class clsassetservicemaster
#Region "Variables"
    Public asstcode As String = Nothing
    Public asstdesc As String = Nothing
    Public catcode As String = Nothing
    Public catdesc As String = Nothing
    Public lev1code As String = Nothing
    Public lev1desc As String = Nothing
    Public lev2code As String = Nothing
    Public lev2desc As String = Nothing
    Public lev3code As String = Nothing
    Public lev3desc As String = Nothing
    Public lev4code As String = Nothing
    Public lev4desc As String = Nothing
    Public lev5code As String = Nothing
    Public lev5desc As String = Nothing
    Public tagno As String = Nothing
    Public serialno As String = Nothing

    Public lbl1 As String = Nothing
    Public lbl2 As String = Nothing
    Public lbl3 As String = Nothing
    Public lbl4 As String = Nothing
    Public lbl5 As String = Nothing

    Public comlevel1 As String = Nothing
    Public comlevel2 As String = Nothing
    Public comlevel3 As String = Nothing
    Public comlevel4 As String = Nothing
    Public comlevel5 As String = Nothing
#End Region

    Public Shared Function DeleteDate(ByVal Strvisi As String, ByVal strAsset As String)
        Try
            Dim qry As String = "delete from TSPL_VISI_MASTER where visi_id='" + Strvisi + "' and asset_no='" + strAsset + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Return clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Shared Function SaveData(ByVal obj As clsassetservicemaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "visi_id", obj.serialno)

            '-------------------make--------
            If obj.comlevel1 = "MAKE" Then
                clsCommon.AddColumnsForChange(coll, "visimake", obj.lev1code)
            ElseIf obj.comlevel2 = "MAKE" Then
                clsCommon.AddColumnsForChange(coll, "visimake", obj.lev2code)
            ElseIf obj.comlevel3 = "MAKE" Then
                clsCommon.AddColumnsForChange(coll, "visimake", obj.lev3code)
            ElseIf obj.comlevel4 = "MAKE" Then
                clsCommon.AddColumnsForChange(coll, "visimake", obj.lev4code)
            ElseIf obj.comlevel5 = "MAKE" Then
                clsCommon.AddColumnsForChange(coll, "visimake", obj.lev5code)
            Else
                clsCommon.AddColumnsForChange(coll, "visimake", "")
            End If

            clsCommon.AddColumnsForChange(coll, "visi_installation_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MM/dd/yyyy"))
            clsCommon.AddColumnsForChange(coll, "asset_no", obj.asstcode)
            clsCommon.AddColumnsForChange(coll, "serial_no", obj.serialno)
            clsCommon.AddColumnsForChange(coll, "tag_no", obj.tagno)

            '-------------------size
            If obj.comlevel1 = "SIZE" Then
                clsCommon.AddColumnsForChange(coll, "visi_size", obj.lev1code)
            ElseIf obj.comlevel2 = "SIZE" Then
                clsCommon.AddColumnsForChange(coll, "visi_size", obj.lev2code)
            ElseIf obj.comlevel3 = "SIZE" Then
                clsCommon.AddColumnsForChange(coll, "visi_size", obj.lev3code)
            ElseIf obj.comlevel4 = "SIZE" Then
                clsCommon.AddColumnsForChange(coll, "visi_size", obj.lev4code)
            ElseIf obj.comlevel5 = "SIZE" Then
                clsCommon.AddColumnsForChange(coll, "visi_size", obj.lev5code)
            End If

            '-------------------model no-----------
            If obj.comlevel1 = "MODEL" Then
                clsCommon.AddColumnsForChange(coll, "model_no", obj.lev1code)
            ElseIf obj.comlevel2 = "MODEL" Then
                clsCommon.AddColumnsForChange(coll, "model_no", obj.lev2code)
            ElseIf obj.comlevel3 = "MODEL" Then
                clsCommon.AddColumnsForChange(coll, "model_no", obj.lev3code)
            ElseIf obj.comlevel4 = "MODEL" Then
                clsCommon.AddColumnsForChange(coll, "model_no", obj.lev4code)
            ElseIf obj.comlevel5 = "MODEL" Then
                clsCommon.AddColumnsForChange(coll, "model_no", obj.lev5code)
            End If

            '---------------------------type-------------------
            If obj.comlevel1 = "TYPE" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.lev1code)
            ElseIf obj.comlevel2 = "TYPE" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.lev2code)
            ElseIf obj.comlevel3 = "TYPE" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.lev3code)
            ElseIf obj.comlevel4 = "TYPE" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.lev4code)
            ElseIf obj.comlevel5 = "TYPE" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Type", obj.lev5code)
            End If

            '--------------------other------------------------------
            If obj.comlevel1 = "OTHER" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Other", obj.lev1code)
            ElseIf obj.comlevel2 = "OTHER" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Other", obj.lev2code)
            ElseIf obj.comlevel3 = "OTHER" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Other", obj.lev3code)
            ElseIf obj.comlevel4 = "OTHER" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Other", obj.lev4code)
            ElseIf obj.comlevel5 = "OTHER" Then
                clsCommon.AddColumnsForChange(coll, "Asset_Other", obj.lev5code)
            End If
            '------------------------------------------------
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                If (clsCommon.myLen(obj.asstcode) = 0) Then
                    Throw New Exception("Please Fill Asset Code")
                    isSaved = False
                End If
                If (clsCommon.myLen(obj.tagno) = 0) Then
                    Throw New Exception("Please Fill Tag No. For Selecting Asset")
                    isSaved = False
                End If

                Dim qry1 As String
                qry1 = "select count(*) from TSPL_VISI_MASTER where VISI_ID='" + obj.serialno + "' and asset_no='" + obj.asstcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                If check1 = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VISI_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VISI_MASTER ", OMInsertOrUpdate.Update, "visi_id='" + obj.serialno + "' and asset_no='" + obj.asstcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal strctcode As String)
        Dim obj As clsassetservicemaster = Nothing
        Dim qry As String = "select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE,TSPL_ITEM_CATEGORY_STRUCTURE.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as item_cat_desc,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value_desc from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" + strctcode + "' left outer join TSPL_ITEM_CATEGORY_STRUCTURE on TSPL_ITEM_CATEGORY_STRUCTURE.ITEM_CATEGORY_STRUCT_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Try
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsassetservicemaster
                obj.catcode = clsCommon.myCstr(dt.Rows(0)("ITEM_CATEGORY_STRUCT_CODE"))
                obj.catdesc = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))

                obj.lbl1 = clsCommon.myCstr(dt.Rows(0)("item_cat_desc"))
                obj.lev1code = clsCommon.myCstr(dt.Rows(0)("Item_Cagetory_values"))
                obj.lev1desc = clsCommon.myCstr(dt.Rows(0)("cat_value_desc"))

                obj.lbl2 = clsCommon.myCstr(dt.Rows(1)("item_cat_desc"))
                obj.lev2code = clsCommon.myCstr(dt.Rows(1)("Item_Cagetory_values"))
                obj.lev2desc = clsCommon.myCstr(dt.Rows(1)("cat_value_desc"))

                obj.lbl3 = clsCommon.myCstr(dt.Rows(2)("item_cat_desc"))
                obj.lev3code = clsCommon.myCstr(dt.Rows(2)("Item_Cagetory_values"))
                obj.lev3desc = clsCommon.myCstr(dt.Rows(2)("cat_value_desc"))

                obj.lbl4 = clsCommon.myCstr(dt.Rows(3)("item_cat_desc"))
                obj.lev4code = clsCommon.myCstr(dt.Rows(3)("Item_Cagetory_values"))
                obj.lev4desc = clsCommon.myCstr(dt.Rows(3)("cat_value_desc"))

                obj.lbl5 = clsCommon.myCstr(dt.Rows(4)("item_cat_desc"))
                obj.lev5code = clsCommon.myCstr(dt.Rows(4)("Item_Cagetory_values"))
                obj.lev5desc = clsCommon.myCstr(dt.Rows(4)("cat_value_desc"))

            End If

        Catch ex As Exception

        End Try
        Return obj
    End Function
End Class
