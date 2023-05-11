Imports common
Imports System.Data.SqlClient
Public Class clsBarCodeGenerator
#Region "variables"
    Public Bar_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Item_Cost As Double = 0
    Public Item_MRP As Double = 0
    Public Item_Selling_Price As Double = 0
    Public Item_color As String = Nothing
    Public Item_Size As String = Nothing
#End Region

    'Public Shared Function SaveData(ByVal ItemType As String, ByVal ArrItem As List(Of clsBarCodeGenerator)) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        If (ArrItem IsNot Nothing AndAlso ArrItem.Count > 0) Then
    '            For Each objItem As clsBarCodeGenerator In ArrItem
    '                Dim coll As New Hashtable()
    '                clsCommon.AddColumnsForChange(coll, "Item_Code", objItem.Item_Code)
    '                clsCommon.AddColumnsForChange(coll, "Item_Type", objItem.Item_Type)
    '                clsCommon.AddColumnsForChange(coll, "Item_Cost", objItem.Item_Cost)
    '                clsCommon.AddColumnsForChange(coll, "Item_MRP", objItem.Item_MRP)
    '                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", objItem.Item_Selling_Price)
    '                clsCommon.AddColumnsForChange(coll, "Item_color", objItem.Item_color)
    '                clsCommon.AddColumnsForChange(coll, "Item_Size", objItem.Item_Size)
    '                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
    '                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
    '                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
    '                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
    '                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

    '                Dim Qry As String = "Select Bar_Code FROM TSPL_ITEM_BARCODE WHERE Item_Type ='" + objItem.Item_Type + "' AND Item_Code='" + objItem.Item_Code + "' AND Item_MRP=" + clsCommon.myCstr(objItem.Item_MRP) + " "
    '                objItem.Bar_Code = clsDBFuncationality.getSingleValue(Qry, trans)
    '                If clsCommon.myLen(objItem.Bar_Code) <= 0 Then
    '                    objItem.Bar_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.BarCode, "", "")
    '                    clsCommon.AddColumnsForChange(coll, "Bar_Code", objItem.Bar_Code)
    '                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_BARCODE", OMInsertOrUpdate.Insert, "", trans)
    '                End If
    '            Next
    '            trans.Commit()
    '            Return True
    '        End If
    '    Catch err As Exception
    '        trans.Rollback()
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return True
    'End Function

    ''Chnage by balwinder due to need to enter bar code mannually.
    Public Shared Function SaveData(ByVal ArrItem As List(Of clsBarCodeGenerator)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrItem IsNot Nothing AndAlso ArrItem.Count > 0) Then
                For Each objItem As clsBarCodeGenerator In ArrItem
                    Dim Qry As String = "delete FROM TSPL_ITEM_BARCODE WHERE Item_Type ='" + objItem.Item_Type + "' AND Item_Code='" + objItem.Item_Code + "' AND Item_MRP=" + clsCommon.myCstr(objItem.Item_MRP) + " "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objItem.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", objItem.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objItem.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Item_MRP", objItem.Item_MRP)
                    clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", objItem.Item_Selling_Price)
                    clsCommon.AddColumnsForChange(coll, "Item_color", objItem.Item_color)
                    clsCommon.AddColumnsForChange(coll, "Item_Size", objItem.Item_Size)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Bar_Code", objItem.Bar_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_BARCODE", OMInsertOrUpdate.Insert, "", trans)
                Next
                trans.Commit()
                Return True
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As clsBarCodeGenerator
        Dim objTr As clsBarCodeGenerator = Nothing
        Try
            Dim qry As String = "Select * from TSPL_ITEM_BARCODE  where Bar_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                objTr = New clsBarCodeGenerator()
                objTr.Bar_Code = clsCommon.myCstr(dt.Rows(0)("Bar_Code"))
                objTr.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                objTr.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                objTr.Item_Cost = clsCommon.myCdbl(dt.Rows(0)("Item_Cost"))
                objTr.Item_MRP = clsCommon.myCdbl(dt.Rows(0)("Item_MRP"))
                objTr.Item_Selling_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Selling_Price"))
                objTr.Item_color = clsCommon.myCstr(dt.Rows(0)("Item_color"))
                objTr.Item_Size = clsCommon.myCstr(dt.Rows(0)("Item_Size"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return objTr
    End Function

    Public Shared Function GetBarCodes(ByVal arrItemCode As List(Of String))
        Dim Arr As New List(Of clsBarCodeGenerator)
        Try
            Dim qry As String = "Select TSPL_ITEM_BARCODE.Bar_Code, TSPL_ITEM_BARCODE.Item_Type, TSPL_ITEM_BARCODE.Item_Code, TSPL_ITEM_MASTER.Item_Desc, "
            qry += " TSPL_ITEM_BARCODE.Item_Cost, TSPL_ITEM_BARCODE.Item_MRP, TSPL_ITEM_BARCODE.Item_Selling_Price, TSPL_ITEM_BARCODE.Item_Color, "
            qry += " TSPL_ITEM_BARCODE.Item_Size   from TSPL_ITEM_BARCODE "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_BARCODE.Item_Code=TSPL_ITEM_MASTER.Item_Code "
            qry += " where TSPL_ITEM_BARCODE.Item_Code in (" + clsCommon.GetMulcallString(arrItemCode) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            For Each dr As DataRow In dt.Rows
                Dim objTr As New clsBarCodeGenerator()
                objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                objTr.Item_MRP = clsCommon.myCdbl(dr("Item_MRP"))
                objTr.Item_Selling_Price = clsCommon.myCdbl(dr("Item_Selling_Price"))
                objTr.Item_color = clsCommon.myCstr(dr("Item_color"))
                objTr.Item_Size = clsCommon.myCstr(dr("Item_Size"))
                Arr.Add(objTr)
            Next
            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Arr
    End Function

End Class






























Public Class clsBCGonSAGE
#Region "variables"
    Public Bar_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Item_Cost As Double = 0
    Public Item_MRP As Double = 0
    Public Item_Selling_Price As Double = 0
    Public Item_color As String = Nothing
    Public Item_Size As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal ItemType As String, ByVal ArrItem As List(Of clsBCGonSAGE)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrItem IsNot Nothing AndAlso ArrItem.Count > 0) Then
                For Each objItem As clsBCGonSAGE In ArrItem
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objItem.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", objItem.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objItem.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Item_MRP", objItem.Item_MRP)
                    clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", objItem.Item_Selling_Price)
                    clsCommon.AddColumnsForChange(coll, "Item_color", objItem.Item_color)
                    clsCommon.AddColumnsForChange(coll, "Item_Size", objItem.Item_Size)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    Dim Qry As String = "Select Bar_Code FROM FNPCOM.dbo.TSPL_ITEM_BARCODE WHERE Item_Type ='" + objItem.Item_Type + "' AND Item_Code='" + objItem.Item_Code + "' AND Item_MRP=" + clsCommon.myCstr(objItem.Item_MRP) + " "
                    objItem.Bar_Code = clsDBFuncationality.getSingleValue(Qry, trans)
                    If clsCommon.myLen(objItem.Bar_Code) <= 0 Then
                        objItem.Bar_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.BarCode, "", "")
                        clsCommon.AddColumnsForChange(coll, "Bar_Code", objItem.Bar_Code)
                        clsCommonFunctionality.UpdateDataTable(coll, "FNPCOM.dbo.TSPL_ITEM_BARCODE", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next
                trans.Commit()
                Return True
            End If
        Catch err As Exception
            trans.Rollback()
            Return False
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As clsBCGonSAGE
        Dim objTr As clsBCGonSAGE = Nothing
        Try
            Dim qry As String = "Select * from FNPCOM.dbo.TSPL_ITEM_BARCODE  where Bar_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                objTr = New clsBCGonSAGE()
                objTr.Bar_Code = clsCommon.myCdbl(dt.Rows(0)("Bar_Code"))
                objTr.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                objTr.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                objTr.Item_Cost = clsCommon.myCdbl(dt.Rows(0)("Item_Cost"))
                objTr.Item_MRP = clsCommon.myCdbl(dt.Rows(0)("Item_MRP"))
                objTr.Item_Selling_Price = clsCommon.myCdbl(dt.Rows(0)("Item_Selling_Price"))
                objTr.Item_color = clsCommon.myCstr(dt.Rows(0)("Item_color"))
                objTr.Item_Size = clsCommon.myCstr(dt.Rows(0)("Item_Size"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return objTr
    End Function

    Public Shared Function GetBarCodes(ByVal arrItemCode As List(Of String))
        Dim Arr As New List(Of clsBCGonSAGE)
        Try
            Dim qry As String = "Select FNPCOM.dbo.TSPL_ITEM_BARCODE.Bar_Code, FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Type, FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Code, FNPCOM.dbo.ICITEM.[DESC] as Item_Desc, "
            qry += " FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Cost, FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_MRP, FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Selling_Price, FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Color, "
            qry += " FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Size   from FNPCOM.dbo.TSPL_ITEM_BARCODE "
            qry += " LEFT OUTER JOIN FNPCOM.dbo.ICITEM on FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Code= FNPCOM.dbo.ICITEM.ITEMNO "
            qry += " where FNPCOM.dbo.TSPL_ITEM_BARCODE.Item_Code in (" + clsCommon.GetMulcallString(arrItemCode) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            For Each dr As DataRow In dt.Rows
                Dim objTr As New clsBCGonSAGE()
                objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                objTr.Item_MRP = clsCommon.myCdbl(dr("Item_MRP"))
                objTr.Item_Selling_Price = clsCommon.myCdbl(dr("Item_Selling_Price"))
                objTr.Item_color = clsCommon.myCstr(dr("Item_color"))
                objTr.Item_Size = clsCommon.myCstr(dr("Item_Size"))
                Arr.Add(objTr)
            Next
            Return Arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Arr
    End Function

End Class


