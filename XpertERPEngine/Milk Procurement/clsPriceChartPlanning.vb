Imports common
Imports System.Data.SqlClient

Public Class clsPriceChartPlanning
#Region "variables"
    Public Planning_Code As String = Nothing
    Public Planning_Date As Date
    Public Planning_Description As String = Nothing
    Public Price_Chart_Code As String = Nothing
    Public Price_Chart_FAT_Ratio As Double
    Public Price_Chart_SNF_Ratio As Double
    Public Price_Chart_FAT_Per As Double
    Public Price_Chart_SNF_Per As Double
    Public Price_Chart_Rate As Double
    Public Single_Axis_FAT_Per As Double
    Public Single_Axis_SNF_Per As Double
    Public Single_Axis_SNFDed_FAT_Per As Double
    Public Single_Axis_SNFDed_SNF_Per As Double
    Public Double_Axis_FAT_Per As Double
    Public Double_Axis_SNF_Per As Double
    Public Double_Axis_Ded_FAT_Per As Double
    Public Double_Axis_Ded_SNF_Per As Double
    Public Double_Axis_Ded_Per As Double
    Public Shift As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    ''
    Public Dock_Collection_Milk_Type As String
    Public Buffelo_FAT_Rate As Double
    Public Buffelo_SNF_Min As Double
    Public Buffelo_SNF_Plus_Minus As Double

    ''
    Public GK_Min_FAT_Per As Double
    Public GK_Max_FAT_Per As Double
    Public GK_Min_SNF_Per As Double
    Public GK_Max_SNF_Per As Double
    Public GK_Is_FAT_Rate_Zero_After_Max As Boolean
    Public GK_Is_SNF_Rate_Zero_After_Max As Boolean


    Public UCDF_SNF_Ded_Below As Decimal
    Public UCDF_SNF_Ded_Rate As Decimal

    'Public arrFATSNF As List(Of clsPriceChartPlanningFATSNF)
    Public arrMCC As List(Of clsPriceChartPlanningMCC)
    Public arrVLC As List(Of clsPriceChartPlanningVLC)
    Public arrTS As List(Of clsPriceChartPlanningTS)

    Public arrFATDed As List(Of clsPriceChartPlanningFATDed)
    Public arrSNFDed As List(Of clsPriceChartPlanningSNFDed)

    Public TSDDCS_Rate As Decimal
    Public TSDDCS_Calcualtion_Method As Integer

    Public arrTSDDCS As List(Of clsPriceChartPlanningTSDDCF)

    Public arrException As List(Of clsPriceChartPlanningException)
#End Region

    Public Shared Function SaveData(obj As clsPriceChartPlanning, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Delete from TSPL_PRICE_CHART_PLANNING_VLC where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PRICE_CHART_PLANNING_MCC where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PRICE_CHART_PLANNING_TOTAL_SOLID where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PRICE_CHART_PLANNING_FAT_DEDUCTION where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PRICE_CHART_PLANNING_SNF_DEDUCTION where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PRICE_CHART_PLANNING_TSDDCF where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PRICE_CHART_PLANNING_TSDDCF_FAT_DEDUCTION where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PRICE_CHART_PLANNING_TSDDCF_SNF_DEDUCTION where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Planning_Date", clsCommon.GetPrintDate(obj.Planning_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Planning_Description", obj.Planning_Description)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_Code", obj.Price_Chart_Code)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_FAT_Ratio", obj.Price_Chart_FAT_Ratio)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_SNF_Ratio", obj.Price_Chart_SNF_Ratio)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_FAT_Per", obj.Price_Chart_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_SNF_Per", obj.Price_Chart_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Price_Chart_Rate", obj.Price_Chart_Rate)
            clsCommon.AddColumnsForChange(coll, "Single_Axis_FAT_Per", obj.Single_Axis_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Single_Axis_SNF_Per", obj.Single_Axis_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Single_Axis_SNFDed_FAT_Per", obj.Single_Axis_SNFDed_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Single_Axis_SNFDed_SNF_Per", obj.Single_Axis_SNFDed_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Double_Axis_FAT_Per", obj.Double_Axis_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Double_Axis_SNF_Per", obj.Double_Axis_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Double_Axis_Ded_FAT_Per", obj.Double_Axis_Ded_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Double_Axis_Ded_SNF_Per", obj.Double_Axis_Ded_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Double_Axis_Ded_Per", obj.Double_Axis_Ded_Per)
            clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
            clsCommon.AddColumnsForChange(coll, "Buffelo_FAT_Rate", obj.Buffelo_FAT_Rate)
            clsCommon.AddColumnsForChange(coll, "Buffelo_SNF_Min", obj.Buffelo_SNF_Min)
            clsCommon.AddColumnsForChange(coll, "Buffelo_SNF_Plus_Minus", obj.Buffelo_SNF_Plus_Minus)
            clsCommon.AddColumnsForChange(coll, "GK_Min_FAT_Per", obj.GK_Min_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "GK_Max_FAT_Per", obj.GK_Max_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "GK_Min_SNF_Per", obj.GK_Min_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "GK_Max_SNF_Per", obj.GK_Max_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "GK_Is_FAT_Rate_Zero_After_Max", IIf(obj.GK_Is_FAT_Rate_Zero_After_Max, 1, 0))
            clsCommon.AddColumnsForChange(coll, "GK_Is_SNF_Rate_Zero_After_Max", IIf(obj.GK_Is_SNF_Rate_Zero_After_Max, 1, 0))

            clsCommon.AddColumnsForChange(coll, "TSDDCS_Rate", obj.TSDDCS_Rate)
            clsCommon.AddColumnsForChange(coll, "TSDDCS_Calcualtion_Method", obj.TSDDCS_Calcualtion_Method)

            clsCommon.AddColumnsForChange(coll, "UCDF_SNF_Ded_Below", obj.UCDF_SNF_Ded_Below)
            clsCommon.AddColumnsForChange(coll, "UCDF_SNF_Ded_Rate", obj.UCDF_SNF_Ded_Rate)

            If IsNewEntry Then
                'qry = "select max(Planning_Code) as Planning_Code from TSPL_PRICE_CHART_PLANNING "
                'obj.Planning_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                'If clsCommon.myLen(obj.Planning_Code) <= 0 Then
                '    obj.Planning_Code = "PL00001"
                'Else
                '    obj.Planning_Code = clsCommon.incval(obj.Planning_Code)
                'End If
                obj.Planning_Code = clsERPFuncationality.GetNextCode(trans, obj.Planning_Date, clsDocType.MatrixPricePlan, "", "")
                If clsCommon.myLen(obj.Planning_Code) < 0 Then
                    Throw New Exception("Eroor in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Planning_Code", obj.Planning_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING", OMInsertOrUpdate.Update, "Planning_Code = '" + obj.Planning_Code + "'", trans)
            End If
            clsPriceChartPlanningMCC.SaveData(obj.Planning_Code, obj.arrMCC, trans)
            clsPriceChartPlanningVLC.SaveData(obj.Planning_Code, obj.arrVLC, trans)
            clsPriceChartPlanningTS.SaveData(obj.Planning_Code, obj.arrTS, trans)

            clsPriceChartPlanningFATDed.SaveData(obj.Planning_Code, obj.arrFATDed, trans)
            clsPriceChartPlanningSNFDed.SaveData(obj.Planning_Code, obj.arrSNFDed, trans)

            clsPriceChartPlanningTSDDCF.SaveData(obj.Planning_Code, obj.arrTSDDCS, trans)

            clsPriceChartPlanningException.SaveData(obj.Planning_Code, obj.arrException, trans)
            'Throw New Exception("BSP")
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim obj As clsPriceChartPlanning = clsPriceChartPlanning.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            If obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Already Posted Transaction")
            End If
            Dim qry As String = "Delete from TSPL_PRICE_CHART_PLANNING_VLC where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PRICE_CHART_PLANNING_MCC where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_PRICE_CHART_PLANNING_TOTAL_SOLID where Planning_Code='" + obj.Planning_Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PRICE_CHART_PLANNING where Planning_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPriceChartPlanning
        Dim obj As clsPriceChartPlanning = Nothing
        Try
            Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING.* from TSPL_PRICE_CHART_PLANNING Where 1=1"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_PRICE_CHART_PLANNING.Planning_Code = (select MIN(TSPL_PRICE_CHART_PLANNING.Planning_Code) from TSPL_PRICE_CHART_PLANNING )"
                Case NavigatorType.Last
                    qry += " and TSPL_PRICE_CHART_PLANNING.Planning_Code = (select MAX(TSPL_PRICE_CHART_PLANNING.Planning_Code) from TSPL_PRICE_CHART_PLANNING )"
                Case NavigatorType.Next
                    qry += " and TSPL_PRICE_CHART_PLANNING.Planning_Code = (select MIN(TSPL_PRICE_CHART_PLANNING.Planning_Code) from TSPL_PRICE_CHART_PLANNING where TSPL_PRICE_CHART_PLANNING.Planning_Code > '" + strCode + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_PRICE_CHART_PLANNING.Planning_Code = (select MAX(TSPL_PRICE_CHART_PLANNING.Planning_Code) from TSPL_PRICE_CHART_PLANNING where TSPL_PRICE_CHART_PLANNING.Planning_Code < '" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_PRICE_CHART_PLANNING.Planning_Code = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsPriceChartPlanning()
                obj.Planning_Code = clsCommon.myCstr(dt.Rows(0)("Planning_Code"))
                obj.Planning_Date = clsCommon.myCDate(dt.Rows(0)("Planning_Date"))
                obj.Planning_Description = clsCommon.myCstr(dt.Rows(0)("Planning_Description"))
                obj.Shift = clsCommon.myCstr(dt.Rows(0)("Shift"))
                obj.Price_Chart_Code = clsCommon.myCstr(dt.Rows(0)("Price_Chart_Code"))
                obj.Price_Chart_FAT_Ratio = clsCommon.myCDecimal(dt.Rows(0)("Price_Chart_FAT_Ratio"))
                obj.Price_Chart_SNF_Ratio = clsCommon.myCDecimal(dt.Rows(0)("Price_Chart_SNF_Ratio"))
                obj.Price_Chart_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("Price_Chart_FAT_Per"))
                obj.Price_Chart_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("Price_Chart_SNF_Per"))
                obj.Price_Chart_Rate = clsCommon.myCDecimal(dt.Rows(0)("Price_Chart_Rate"))
                obj.Single_Axis_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_FAT_Per"))
                obj.Single_Axis_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNF_Per"))
                obj.Single_Axis_SNFDed_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNFDed_FAT_Per"))
                obj.Single_Axis_SNFDed_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("Single_Axis_SNFDed_SNF_Per"))
                obj.Double_Axis_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("Double_Axis_FAT_Per"))
                obj.Double_Axis_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("Double_Axis_SNF_Per"))
                obj.Double_Axis_Ded_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("Double_Axis_Ded_FAT_Per"))
                obj.Double_Axis_Ded_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("Double_Axis_Ded_SNF_Per"))
                obj.Double_Axis_Ded_Per = clsCommon.myCDecimal(dt.Rows(0)("Double_Axis_Ded_Per"))
                obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)

                obj.Dock_Collection_Milk_Type = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
                obj.Buffelo_FAT_Rate = clsCommon.myCDecimal(dt.Rows(0)("Buffelo_FAT_Rate"))
                obj.Buffelo_SNF_Min = clsCommon.myCDecimal(dt.Rows(0)("Buffelo_SNF_Min"))
                obj.Buffelo_SNF_Plus_Minus = clsCommon.myCDecimal(dt.Rows(0)("Buffelo_SNF_Plus_Minus"))

                obj.GK_Min_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("GK_Min_FAT_Per"))
                obj.GK_Max_FAT_Per = clsCommon.myCDecimal(dt.Rows(0)("GK_Max_FAT_Per"))
                obj.GK_Min_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("GK_Min_SNF_Per"))
                obj.GK_Max_SNF_Per = clsCommon.myCDecimal(dt.Rows(0)("GK_Max_SNF_Per"))
                obj.GK_Is_FAT_Rate_Zero_After_Max = IIf(clsCommon.myCDecimal(dt.Rows(0)("GK_Is_FAT_Rate_Zero_After_Max")) = 1, True, False)
                obj.GK_Is_SNF_Rate_Zero_After_Max = IIf(clsCommon.myCDecimal(dt.Rows(0)("GK_Is_SNF_Rate_Zero_After_Max")) = 1, True, False)

                obj.TSDDCS_Rate = clsCommon.myCDecimal(dt.Rows(0)("TSDDCS_Rate"))
                obj.TSDDCS_Calcualtion_Method = clsCommon.myCDecimal(dt.Rows(0)("TSDDCS_Calcualtion_Method"))

                obj.UCDF_SNF_Ded_Below = clsCommon.myCDecimal(dt.Rows(0)("UCDF_SNF_Ded_Below"))
                obj.UCDF_SNF_Ded_Rate = clsCommon.myCDecimal(dt.Rows(0)("UCDF_SNF_Ded_Rate"))

                obj.arrMCC = clsPriceChartPlanningMCC.GetData(obj.Planning_Code)
                obj.arrVLC = clsPriceChartPlanningVLC.GetData(obj.Planning_Code)
                obj.arrTS = clsPriceChartPlanningTS.GetData(obj.Planning_Code)

                obj.arrFATDed = clsPriceChartPlanningFATDed.GetData(obj.Planning_Code)
                obj.arrSNFDed = clsPriceChartPlanningSNFDed.GetData(obj.Planning_Code)

                obj.arrTSDDCS = clsPriceChartPlanningTSDDCF.GetData(obj.Planning_Code)
                obj.arrException = clsPriceChartPlanningException.GetData(obj.Planning_Code)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
End Class

Public Class clsPriceChartPlanningMCC
#Region "variables"
    Public Planning_Code As String = Nothing
    Public MCC_Code As String
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningMCC), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningMCC In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningMCC)
        Dim arr As New List(Of clsPriceChartPlanningMCC)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_MCC.* from TSPL_PRICE_CHART_PLANNING_MCC Where Planning_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningMCC()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                arr.Add(obj)
            Next

        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningVLC
#Region "variables"
    Public Planning_Code As String = Nothing
    Public VLC_Code As String
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningVLC), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningVLC In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_VLC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningVLC)
        Dim arr As New List(Of clsPriceChartPlanningVLC)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_VLC.* from TSPL_PRICE_CHART_PLANNING_VLC Where Planning_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningVLC()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningTS
#Region "variables"
    Public Planning_Code As String = Nothing
    Public Min_Range As Double
    Public Value As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningTS), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningTS In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Min_Range", obj.Min_Range)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_TOTAL_SOLID", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningTS)
        Dim arr As New List(Of clsPriceChartPlanningTS)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_TOTAL_SOLID.* from TSPL_PRICE_CHART_PLANNING_TOTAL_SOLID Where Planning_Code='" + strCode + "' order by Min_Range "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningTS()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.Min_Range = clsCommon.myCDecimal(dr("Min_Range"))
                obj.Value = clsCommon.myCDecimal(dr("Value"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsStandardPrice
    Public Milk_Rate As Decimal
    Public Weightage_FAT As Decimal
    Public Weightage_SNF As Decimal
    Public Std_Percent_FAT As Decimal
    Public Std_Percent_SNF As Decimal

    Public Shared Function GetStandartPrice(ByVal strMilkPriceCode As String, ByVal trans As SqlTransaction) As clsStandardPrice
        Dim obj As clsStandardPrice = Nothing
        Dim qry As String = "select top 1 TSPL_MILK_PRICE_MASTER.Milk_Rate,TSPL_MILK_PRICE_MASTER.Ratio,TSPL_MILK_PRICE_MASTER.FAT_Pers,TSPL_MILK_PRICE_MASTER.SNF_Ratio,TSPL_MILK_PRICE_MASTER.SNF_Pers " + Environment.NewLine +
        " from TSPL_FAT_SNF_UPLOADER_MASTER " + Environment.NewLine +
        " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code" + Environment.NewLine +
        " where TSPL_FAT_SNF_UPLOADER_MASTER.Code='" + strMilkPriceCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsStandardPrice()
            obj.Milk_Rate = clsCommon.myCDecimal(dt.Rows(0)("Milk_Rate"))
            obj.Weightage_FAT = clsCommon.myCDecimal(dt.Rows(0)("Ratio"))
            obj.Weightage_SNF = clsCommon.myCDecimal(dt.Rows(0)("SNF_Ratio"))
            obj.Std_Percent_FAT = clsCommon.myCDecimal(dt.Rows(0)("FAT_Pers"))
            obj.Std_Percent_SNF = clsCommon.myCDecimal(dt.Rows(0)("SNF_Pers"))
            dt = Nothing
        End If
        Return obj
    End Function
End Class


Public Class clsPriceChartPlanningFATDed
#Region "variables"
    Public Planning_Code As String = Nothing
    Public Per As Double
    Public Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningFATDed), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningFATDed In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Per", obj.Per)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_FAT_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningFATDed)
        Dim arr As New List(Of clsPriceChartPlanningFATDed)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_FAT_DEDUCTION.* from TSPL_PRICE_CHART_PLANNING_FAT_DEDUCTION Where Planning_Code='" + strCode + "' order by Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningFATDed()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.Per = clsCommon.myCDecimal(dr("Per"))
                obj.Amount = clsCommon.myCDecimal(dr("Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningSNFDed
#Region "variables"
    Public Planning_Code As String = Nothing
    Public Per As Double
    Public Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningSNFDed), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningSNFDed In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Per", obj.Per)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_SNF_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningSNFDed)
        Dim arr As New List(Of clsPriceChartPlanningSNFDed)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_SNF_DEDUCTION.* from TSPL_PRICE_CHART_PLANNING_SNF_DEDUCTION Where Planning_Code='" + strCode + "' order by Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningSNFDed()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.Per = clsCommon.myCDecimal(dr("Per"))
                obj.Amount = clsCommon.myCDecimal(dr("Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class


Public Class clsPriceChartPlanningTSDDCF
#Region "variables"
    Public Planning_Code As String = Nothing
    Public SNo As Integer
    Public FAT_From As Decimal
    Public FAT_To As Decimal
    Public Apply_FAT As Decimal
    Public SNF_From As Decimal
    Public SNF_To As Decimal
    Public Apply_SNF As Decimal
    Public Rate_Per As Decimal
    Public Fixed_Rate As Decimal
    Public Below_SNF_Rate As Decimal
    Public Deduction_Per As Decimal
    Public arrFAT As Dictionary(Of Decimal, Decimal)
    Public arrSNF As Dictionary(Of Decimal, Decimal)
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningTSDDCF), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim index As Integer = 1
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningTSDDCF In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", index)
                    clsCommon.AddColumnsForChange(coll, "FAT_From", obj.FAT_From)
                    clsCommon.AddColumnsForChange(coll, "FAT_To", obj.FAT_To)
                    clsCommon.AddColumnsForChange(coll, "Apply_FAT", obj.Apply_FAT)
                    clsCommon.AddColumnsForChange(coll, "SNF_From", obj.SNF_From)
                    clsCommon.AddColumnsForChange(coll, "SNF_To", obj.SNF_To)
                    clsCommon.AddColumnsForChange(coll, "Apply_SNF", obj.Apply_SNF)
                    clsCommon.AddColumnsForChange(coll, "Rate_Per", obj.Rate_Per)
                    clsCommon.AddColumnsForChange(coll, "Fixed_Rate", obj.Fixed_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Below_SNF_Rate", obj.Below_SNF_Rate, True)
                    clsCommon.AddColumnsForChange(coll, "Deduction_Per", obj.Deduction_Per, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_TSDDCF", OMInsertOrUpdate.Insert, "", trans)
                    clsPriceChartPlanningTSDDCFSNFDed.SaveData(strCode, index, obj.arrSNF, trans)
                    clsPriceChartPlanningTSDDCFFATDed.SaveData(strCode, index, obj.arrFAT, trans)
                    index += 1
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningTSDDCF)
        Dim arr As New List(Of clsPriceChartPlanningTSDDCF)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_TSDDCF.* from TSPL_PRICE_CHART_PLANNING_TSDDCF Where Planning_Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningTSDDCF()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.SNo = clsCommon.myCDecimal(dr("SNo"))
                obj.FAT_From = clsCommon.myCDecimal(dr("FAT_From"))
                obj.FAT_To = clsCommon.myCDecimal(dr("FAT_To"))
                obj.Apply_FAT = clsCommon.myCDecimal(dr("Apply_FAT"))
                obj.SNF_From = clsCommon.myCDecimal(dr("SNF_From"))
                obj.SNF_To = clsCommon.myCDecimal(dr("SNF_To"))
                obj.Apply_SNF = clsCommon.myCDecimal(dr("Apply_SNF"))
                obj.Rate_Per = clsCommon.myCDecimal(dr("Rate_Per"))
                obj.Fixed_Rate = clsCommon.myCDecimal(dr("Fixed_Rate"))
                obj.Below_SNF_Rate = clsCommon.myCDecimal(dr("Below_SNF_Rate"))
                obj.Deduction_Per = clsCommon.myCDecimal(dr("Deduction_Per"))
                obj.arrSNF = clsPriceChartPlanningTSDDCFSNFDed.GetData(obj.Planning_Code, obj.SNo)
                obj.arrFAT = clsPriceChartPlanningTSDDCFFATDed.GetData(obj.Planning_Code, obj.SNo)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningTSDDCFSNFDed
#Region "variables"
    Public Planning_Code As String = Nothing
    Public SNo As Integer
    Public SNF_Per As Double
    Public Ded_Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal intSNo As Integer, ByVal Arr As Dictionary(Of Decimal, Decimal), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For ii As Integer = 0 To Arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", intSNo)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", Arr.Keys(ii))
                    clsCommon.AddColumnsForChange(coll, "Ded_Amount", Arr(Arr.Keys(ii)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_TSDDCF_SNF_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal intSNo As Integer) As Dictionary(Of Decimal, Decimal)
        Return GetData(strCode, intSNo, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal intSNo As Integer, ByVal tran As SqlTransaction) As Dictionary(Of Decimal, Decimal)
        Dim arr As New Dictionary(Of Decimal, Decimal)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_TSDDCF_SNF_DEDUCTION.* from TSPL_PRICE_CHART_PLANNING_TSDDCF_SNF_DEDUCTION Where Planning_Code='" + strCode + "' and SNo='" + clsCommon.myCstr(intSNo) + "' order by SNF_Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCDecimal(dr("SNF_Per")), clsCommon.myCDecimal(dr("Ded_Amount")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningTSDDCFFATDed
#Region "variables"
    Public Planning_Code As String = Nothing
    Public SNo As Integer
    Public FAT_Per As Double
    Public Ded_Amount As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal intSNo As Integer, ByVal Arr As Dictionary(Of Decimal, Decimal), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For ii As Integer = 0 To Arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", intSNo)
                    clsCommon.AddColumnsForChange(coll, "FAT_Per", Arr.Keys(ii))
                    clsCommon.AddColumnsForChange(coll, "Ded_Amount", Arr(Arr.Keys(ii)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_TSDDCF_FAT_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal intSNo As Integer) As Dictionary(Of Decimal, Decimal)
        Return GetData(strCode, intSNo, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal intSNo As Integer, ByVal tran As SqlTransaction) As Dictionary(Of Decimal, Decimal)
        Dim arr As New Dictionary(Of Decimal, Decimal)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_TSDDCF_FAT_DEDUCTION.* from TSPL_PRICE_CHART_PLANNING_TSDDCF_FAT_DEDUCTION Where Planning_Code='" + strCode + "' and SNo='" + clsCommon.myCstr(intSNo) + "' order by FAT_Per "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCDecimal(dr("FAT_Per")), clsCommon.myCDecimal(dr("Ded_Amount")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsPriceChartPlanningException
#Region "variables"
    Public Planning_Code As String = Nothing
    Public SNo As Integer
    Public FAT As Decimal
    Public SNF As Decimal
    Public Rate As Decimal
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPriceChartPlanningException), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim index As Integer = 1
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsPriceChartPlanningException In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Planning_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", index)
                    clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                    clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING_EXCEPTION", OMInsertOrUpdate.Insert, "", trans)
                    index += 1
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsPriceChartPlanningException)
        Dim arr As New List(Of clsPriceChartPlanningException)
        Dim qry As String = "Select TSPL_PRICE_CHART_PLANNING_EXCEPTION.* from TSPL_PRICE_CHART_PLANNING_EXCEPTION Where Planning_Code='" + strCode + "' order by SNo "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPriceChartPlanningException()
                obj.Planning_Code = clsCommon.myCstr(dr("Planning_Code"))
                obj.SNo = clsCommon.myCDecimal(dr("SNo"))
                obj.FAT = clsCommon.myCDecimal(dr("FAT"))
                obj.SNF = clsCommon.myCDecimal(dr("SNF"))
                obj.Rate = clsCommon.myCDecimal(dr("Rate"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
