'--------Created By Richa 18/07/2014 Against Ticket No BM00000003245
Imports System.Data.SqlClient
Imports common



Public Class ClsBulkSalePriceChart
#Region "variables"
    Public Price_Code As String = Nothing
    Public Price_Date As Date?
    Public UOM As String = String.Empty
    Public Fat_Weightage As Double = 0
    Public Snf_Weightage As Double = 0
    Public Fat_Ratio As Double = 0
    Public Snf_Ratio As Double = 0
    Public Standard_Rate As Double = 0
    Public Location_Code As String = Nothing
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public TolerancePerPlus As Double = 0
    Public TolerancePerMinus As Double = 0
    Public AbandonmentNo As Integer = 0
    Public Posted As Integer = 0
    Public ValidTill As Date? = Nothing
    Public isUseInCanSale As Boolean = False
    Public TSRate As Decimal = 0
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim Qry As String = "Select Price_Code as Code ,Convert(varchar,Price_Date,103) as Date,Location_Code as [Location Code],Fat_Weightage as [Fat Weightage],Snf_Weightage as [SNF Weightage],Fat_Ratio As [Fat Ratio],Snf_Ratio as [SNF Ratio],Standard_Rate as [Standard Rate],Posted,case when  UseInCanSale =1 then 'True' else 'False' end as [Use In Cancel],TSRate as [TS Rate],uom from TSPL_BulkSalePrice_MASTER "
        'Dim qry As String = " select TSPL_BulkSalePrice_MASTER.Price_Code as [Code] ,TSPL_BulkSalePrice_MASTER.Price_Date as [Price Date],Fat_Weightage as [Fat Weightage],Snf_Weightage as [SNF Weightage],Fat_Ratio As [Fat Ratio],Snf_Ratio as [SNF Ratio],Standard_Rate as [Standard Rate]   From TSPL_BulkSalePrice_MASTER   "
        '   Dim qry As String = "select TSPL_BulkSalePrice_MASTER.Price_Code as Code,Convert(varchar,TSPL_BulkSalePrice_MASTER.Price_Date,103) as Date,TSPL_BulkSalePrice_MASTER.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_BulkSalePrice_MASTER.Fat_Weightage as [Fat Weightage],TSPL_BulkSalePrice_MASTER.Snf_Weightage as [SNF Weightage],TSPL_BulkSalePrice_MASTER.Fat_Ratio as [Fat Ratio],TSPL_BulkSalePrice_MASTER.Snf_Ratio as [SNF Ratio],TSPL_BulkSalePrice_MASTER.FatRate as [Fat Rate],TSPL_BulkSalePrice_MASTER.SNFRate as [SNF Rate],TSPL_BulkSalePrice_MASTER.Standard_Rate as [Standard Rate],TSPL_BulkSalePrice_MASTER.TolerancePerPlus as [Tolerance % (+)],TSPL_BulkSalePrice_MASTER.TolerancePerMinus as [Tolerance % (-)] from  TSPL_BulkSalePrice_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER .Location_Code "
        str = clsCommon.ShowSelectForm("BulkSalePriceChart", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function


    Public Shared Function SaveData(ByVal obj As ClsBulkSalePriceChart, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As ClsBulkSalePriceChart, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim qry As String = ""

        Try
            If isNewEntry Then
                obj.Price_Code = clsERPFuncationality.GetNextCode(trans, obj.Price_Date, clsDocType.BulkSalePriceChart, "", "")
            Else
                obj.AbandonmentNo = clsDBFuncationality.getSingleValue("Select AbandonmentNo from TSPL_BulkSalePrice_MASTER where Price_Code='" + obj.Price_Code + "' ", trans)
                clsBulkSalePriceChartHistory.SaveDataForHistory(obj.Price_Code, clsCommon.myCdbl(obj.AbandonmentNo), trans)
                obj.AbandonmentNo = obj.AbandonmentNo + 1
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
            clsCommon.AddColumnsForChange(coll, "TSRate", obj.TSRate)
            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
            clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
            clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
            ''richa against Ticket no BM00000003849 on 10/09/2014
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM, True)
            clsCommon.AddColumnsForChange(coll, "TolerancePerPlus", obj.TolerancePerPlus)
            clsCommon.AddColumnsForChange(coll, "TolerancePerMinus", obj.TolerancePerMinus)
            clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
            clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
            clsCommon.AddColumnsForChange(coll, "AbandonmentNo", obj.AbandonmentNo)
            clsCommon.AddColumnsForChange(coll, "UseInCanSale", IIf(obj.isUseInCanSale, 1, 0))
            If obj.ValidTill IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "ValidTill", clsCommon.GetPrintDate(obj.ValidTill, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ValidTill", Nothing, True)
            End If

            '=================================================
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BulkSalePrice_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BulkSalePrice_MASTER", OMInsertOrUpdate.Update, "TSPL_BulkSalePrice_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsBulkSalePriceChart
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsBulkSalePriceChart
        Dim obj As ClsBulkSalePriceChart = Nothing
        Dim Arr As List(Of ClsBulkSalePriceChart) = Nothing
        Dim qry As String = "select UOM,TSRate,Price_Code ,Price_Date,Snf_Weightage,Snf_Ratio,Fat_Weightage,Fat_Ratio,Standard_Rate,Location_Code,FatRate,SNFRate,TolerancePerPlus,TolerancePerMinus,ValidTill,Posted,UseInCanSale from TSPL_BulkSalePrice_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BulkSalePrice_MASTER.Price_Code = (select MIN(Price_Code) from TSPL_BulkSalePrice_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_BulkSalePrice_MASTER.Price_Code = (select Max(Price_Code) from TSPL_BulkSalePrice_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_BulkSalePrice_MASTER.Price_Code ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_BulkSalePrice_MASTER.Price_Code = (select Min(Price_Code) from TSPL_BulkSalePrice_MASTER where Price_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_BulkSalePrice_MASTER.Price_Code = (select Max(Price_Code) from TSPL_BulkSalePrice_MASTER where Price_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsBulkSalePriceChart()
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
            obj.Price_Date = clsCommon.myCDate(dt.Rows(0)("Price_Date"))
            obj.Snf_Weightage = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            obj.Snf_Ratio = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
            obj.Fat_Weightage = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            obj.Fat_Ratio = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
            obj.Standard_Rate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            ''richa against Ticket no BM00000003849 on 10/09/2014
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.TolerancePerPlus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
            obj.TolerancePerMinus = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
            obj.FatRate = clsCommon.myCdbl(dt.Rows(0)("FatRate"))
            obj.SNFRate = clsCommon.myCdbl(dt.Rows(0)("SNFRate"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.isUseInCanSale = IIf(clsCommon.myCdbl(dt.Rows(0)("UseInCanSale")) = 1, True, False)
            If dt.Rows(0)("ValidTill") IsNot DBNull.Value Then
                obj.ValidTill = clsCommon.myCDate(dt.Rows(0)("ValidTill"))
            Else
                obj.ValidTill = Nothing

            End If
            obj.TSRate = clsCommon.myCdbl(dt.Rows(0)("TSRate"))
            ''====================================
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_BulkSalePrice_MASTER where Price_Code='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Bulk Sales Price No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_BulkSalePrice_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Price_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'trans.Commit()

        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
''richa against Ticket no BM00000003849 on 10/09/2014
Public Class clsBulkSalePriceChartHistory

    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_BulkSalePrice_MASTER", trans)
            Dim qry As String = "INSERT INTO TSPL_BulkSalePrice_MASTER_History (" + strInvColumns + ",AbandonmentDate) SELECT " + strInvColumns.Replace("Abandonment_No", "" + clsCommon.myCstr(intAmbandentNo) + "") + ",'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_BulkSalePrice_MASTER where Price_Code='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
''==================================================
