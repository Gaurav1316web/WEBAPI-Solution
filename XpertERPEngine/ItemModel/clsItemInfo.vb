Imports common
Imports System.Data.SqlClient

Public Class clsWeightConversionInfo
    Public Container_Qty As Decimal = 0
    Public Container_UOM As String = ""
    Public Container_UOM_Dsec As String = ""
    Public Contained_Qty As Decimal = 0
    Public Contained_UOM As String = ""
    Public Contained_UOM_Desc As String = ""
    Public Product_Type As String = Nothing
    Public CurrentDate As String = Nothing  'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    Public structure_code As String = ""

    Public Shared Function GetData(ByVal trans As SqlTransaction) As List(Of clsWeightConversionInfo)
        Dim arr As New List(Of clsWeightConversionInfo)
        Dim qry As String = "Select Container_Qty, Container_UOM, UM1.Unit_Desc as Container_UOM_Desc,Contained_Qty, Contained_UOM, UM2.Unit_Desc as Contained_UOM_Desc,product_type,structure_code" & _
        " from TSPL_WEIGHT_CONVERSION" & _
        " LEFT OUTER JOIN TSPL_UNIT_MASTER as UM1 ON UM1.Unit_Code=TSPL_WEIGHT_CONVERSION.Container_UOM" & _
        " LEFT OUTER JOIN TSPL_UNIT_MASTER UM2 ON UM2.Unit_Code=TSPL_WEIGHT_CONVERSION.Contained_UOM"
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsWeightConversionInfo
                obj.Container_Qty = clsCommon.myCdbl(dr("Container_Qty"))
                obj.Container_UOM = clsCommon.myCstr(dr("Container_UOM"))
                obj.Container_UOM_Dsec = clsCommon.myCstr(dr("Container_UOM_Desc"))
                obj.Contained_Qty = clsCommon.myCdbl(dr("Contained_Qty"))
                obj.Contained_UOM = clsCommon.myCstr(dr("Contained_UOM"))
                obj.Contained_UOM_Desc = clsCommon.myCstr(dr("Contained_UOM_Desc"))
                obj.Product_Type = clsCommon.myCstr(dr("product_type"))
                obj.structure_code = clsCommon.myCstr(dr("structure_code"))
                arr.Add(obj)
            Next
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsWeightConversionInfo)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_WEIGHT_CONVERSION", trans)
            For Each obj As clsWeightConversionInfo In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Container_Qty", obj.Container_Qty)
                clsCommon.AddColumnsForChange(coll, "Container_UOM", obj.Container_UOM)
                clsCommon.AddColumnsForChange(coll, "Contained_Qty", obj.Contained_Qty)
                clsCommon.AddColumnsForChange(coll, "Contained_UOM", obj.Contained_UOM)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "product_type", obj.Product_Type)
                clsCommon.AddColumnsForChange(coll, "structure_code", obj.structure_code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WEIGHT_CONVERSION", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function LoadProductTypeWithALL() As DataTable
        Dim dt As DataTable = clsItemMaster.LoadItemProductType()
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "ALL"
        dr("Name") = "ALL"
        dt.Rows.Add(dr)

        'dt.Rows(0).Delete()
        dt.DefaultView.RowFilter = " Code <> ''"
        Return dt
    End Function

    Public Shared Function GetWeightConverionFactor(ByVal strICode As String, ByVal fromUOM As String, ByVal toUOM As String, ByVal trans As SqlTransaction) As Double
        Dim strStrCode As String = ""
        Dim retVal As Double = 1
        If Not clsCommon.CompairString(fromUOM, toUOM) = CompairStringResult.Equal Then
            Dim Whrcls As String = ""

            If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
                If clsCommon.myLen(strICode) <= 0 Then
                    Throw New Exception("Item code can not be blank for Weight conversion")
                End If
                strStrCode = clsItemMaster.GetItemStructureCode(strICode, trans)
                Whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in ('" + strStrCode + "')"
            End If
            Dim qry As String = "(Select CF From (Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + Whrcls + " UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + Whrcls + " UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + Whrcls + " UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION Where 2=2 " + Whrcls + " ) yyy where yyy.FromUOM='" & fromUOM & "' and yyy.TOUOM ='" & toUOM & "')"
            retVal = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        End If
        If retVal = 0 Then
            Throw New Exception("Please provide weight conversion " + IIf(clsCommon.myLen(strStrCode) > 0, "[ " + strStrCode + " ]", "") + " from " + fromUOM + " -> " + toUOM + " or " + toUOM + " -> " + fromUOM + "")
        End If
        Return retVal
    End Function

    Public Shared Function GetConversion_factor(ByVal FromUOM As String, ByVal ToUOM As String, ByVal trans As SqlTransaction)
        Return GetConversion_factor(objCommonVar.DefaultMilkItemCode, FromUOM, ToUOM, trans)
    End Function

    Public Shared Function GetConversion_factor(ByVal strICode As String, ByVal FromUOM As String, ByVal ToUOM As String, ByVal trans As SqlTransaction)
        Dim conv_fac As Double = 0
        Try
            Dim qry As String = "select coalesce(Contained_Qty,1) as Contained_Qty from  TSPL_WEIGHT_CONVERSION where Product_Type in ('MI','ALL')"
            If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
                If clsCommon.myLen(strICode) <= 0 Then
                    Throw New Exception("Please first define the MCC Default Milk item in milk setting")
                End If
                qry += " and TSPL_WEIGHT_CONVERSION.Structure_Code in (select Structure_Code from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' ) "
            End If
            Dim ExtrWhrCls As String = " and  Container_UOM='" & FromUOM & "' and Contained_UOM='" + ToUOM + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + ExtrWhrCls + " order by Product_Type desc", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                conv_fac = clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
            Else
                ExtrWhrCls = " and Container_UOM='" & ToUOM & "' and Contained_UOM='" + FromUOM + "'"
                dt = clsDBFuncationality.GetDataTable(qry + ExtrWhrCls + " order by Product_Type desc", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    conv_fac = 1 / clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
                Else
                    qry = "Please porvide Weight conversion from " + FromUOM + " To " + ToUOM + ""
                    If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
                        qry += " for Structure Code [" + clsItemMaster.GetItemStructureCode(strICode, trans) + "]"
                    End If
                    Throw New Exception(qry)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return conv_fac
    End Function

    Public Shared Function GetWeightConverionFactorMilkType(ByVal strICode As String, ByVal fromUOM As String, ByVal toUOM As String, ByVal trans As SqlTransaction) As Double ''By Balwinder on 23/11/2016 for Milk Type
        Dim Whrcls As String = ""
        If objCommonVar.ItemSturctureMandatoryOnWeightConversion Then
            If clsCommon.myLen(strICode) <= 0 Then
                Throw New Exception("Item code can not be blank for Weight conversion")
            End If
            Whrcls = " and TSPL_WEIGHT_CONVERSION.Structure_Code in (select Structure_Code from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' ) "
        End If

        Dim retVal As Double = 0
        Dim qry As String = "Select CF From (  " + Environment.NewLine + _
        " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Product_Type='MI'  " + Whrcls + " " + Environment.NewLine + _
        " UNION All " + Environment.NewLine + _
        " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='MI' " + Whrcls + "  " + Environment.NewLine + _
        " UNION All " + Environment.NewLine + _
        " Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='MI'   " + Whrcls + "  " + Environment.NewLine + _
        " UNION All " + Environment.NewLine + _
        " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='MI'  " + Whrcls + "  " + Environment.NewLine + _
        " ) yyy where yyy.FromUOM='" + fromUOM + "' and yyy.TOUOM ='" + toUOM + "'"
        retVal = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        If retVal = 0 Then
            qry = "Select CF From (  " + Environment.NewLine + _
           " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Product_Type='ALL'   " + Whrcls + "  " + Environment.NewLine + _
           " UNION All " + Environment.NewLine + _
           " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='ALL'   " + Whrcls + "  " + Environment.NewLine + _
           " UNION All " + Environment.NewLine + _
           " Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='ALL'  " + Whrcls + "  " + Environment.NewLine + _
           " UNION All " + Environment.NewLine + _
           " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  where Product_Type='ALL' " + Whrcls + "  " + Environment.NewLine + _
           " ) yyy where yyy.FromUOM='" + fromUOM + "' and yyy.TOUOM ='" + toUOM + "'"
            retVal = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)))
        End If
        If retVal = 0 Then
            Throw New Exception("Please provide weight conversion from " + fromUOM + " -> " + toUOM + " or " + toUOM + " -> " + fromUOM + "")
        End If
        Return retVal
    End Function
End Class

Public Class clsUOMInfo
    Public Unit_Code As String = ""
    Public Unit_Desc As String = ""

    Public Shared Function GetUnitDesc(ByVal strUnitCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER WHERE Unit_Code='" + strUnitCode + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CheckUnitCode(ByVal strUnitCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable("Select Unit_Desc from TSPL_UNIT_MASTER WHERE Unit_Code='" + strUnitCode + "'", trans)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_UNIT_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_UNIT_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("uom1", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function

End Class