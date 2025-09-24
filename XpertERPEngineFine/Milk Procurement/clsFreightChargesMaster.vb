Imports common
Imports System.Data.SqlClient

Public Class clsFreightChargesMaster
#Region "Variables"
    Public Freight_Code As String = Nothing
    Public Freight_Description As String = Nothing
    Public Status As String = Nothing
    Public Price_Km As Decimal = Nothing
    Public Rate_Type As String = String.Empty
    Public Price_Ltr_KG As Double = 0
    Public Shift_Charges As Decimal = Nothing
    Public Avg_Km_Ltr As Decimal = Nothing
    Public Diesel_Rate As Decimal = Nothing
    Public Rental_Type As String = String.Empty
    Public Rental_Amount As Double = 0
    Public Is_Additional As Boolean = False
    Public ArrSlab As List(Of clsFreightChargesSlab) = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As clsFreightChargesMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsFreightChargesMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Freight_Description", obj.Freight_Description)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Price_Km", obj.Price_Km)
            clsCommon.AddColumnsForChange(coll, "Rate_Type", obj.Rate_Type)
            clsCommon.AddColumnsForChange(coll, "Price_Ltr_KG", obj.Price_Ltr_KG)
            clsCommon.AddColumnsForChange(coll, "Shift_Charges", obj.Shift_Charges)
            clsCommon.AddColumnsForChange(coll, "Avg_Km_Ltr", obj.Avg_Km_Ltr)
            clsCommon.AddColumnsForChange(coll, "Diesel_Rate", obj.Diesel_Rate)
            clsCommon.AddColumnsForChange(coll, "Rental_Type", obj.Rental_Type)
            clsCommon.AddColumnsForChange(coll, "Rental_Amount", obj.Rental_Amount)
            clsCommon.AddColumnsForChange(coll, "Is_Additional", IIf(obj.Is_Additional = True, "1", "0"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If clsCommon.myLen(obj.Freight_Code) <= 0 Then
                obj.Freight_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Freight_Code) from TSPL_FREIGHT_CHARGES_MASTER", trans))
                If clsCommon.myLen(obj.Freight_Code) <= 0 Then
                    obj.Freight_Code = "FCM00000000001"
                Else
                    obj.Freight_Code = clsCommon.incval(obj.Freight_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Freight_Code", obj.Freight_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FREIGHT_CHARGES_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FREIGHT_CHARGES_MASTER", OMInsertOrUpdate.Update, " Freight_Code='" + obj.Freight_Code + "'", trans)
            End If
            clsFreightChargesSlab.SaveData(obj.Freight_Code, obj.ArrSlab, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsFreightChargesMaster
        Try
            Dim obj As clsFreightChargesMaster = Nothing
            Dim qry As String = "select TSPL_FREIGHT_CHARGES_MASTER.*  from TSPL_FREIGHT_CHARGES_MASTER "
            Dim whrcls As String = ""
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_FREIGHT_CHARGES_MASTER.Freight_Code='" + strCode + "' " + whrcls + ""
                Case NavigatorType.First
                    qry += " where TSPL_FREIGHT_CHARGES_MASTER.Freight_Code in (select min(Freight_Code) from TSPL_FREIGHT_CHARGES_MASTER where 2=2 " + whrcls + ")"
                Case NavigatorType.Last
                    qry += " where TSPL_FREIGHT_CHARGES_MASTER.Freight_Code in (select max(Freight_Code) from TSPL_FREIGHT_CHARGES_MASTER where 2=2 " + whrcls + ")"
                Case NavigatorType.Next
                    qry += " where TSPL_FREIGHT_CHARGES_MASTER.Freight_Code in (select min(Freight_Code) from TSPL_FREIGHT_CHARGES_MASTER where Freight_Code>'" + strCode + "' " + whrcls + ")"
                Case NavigatorType.Previous
                    qry += " where TSPL_FREIGHT_CHARGES_MASTER.Freight_Code in (select max(Freight_Code) from TSPL_FREIGHT_CHARGES_MASTER where Freight_Code<'" + strCode + "' " + whrcls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsFreightChargesMaster()
                obj.Freight_Code = clsCommon.myCstr(dt.Rows(0)("Freight_Code"))
                obj.Freight_Description = clsCommon.myCstr(dt.Rows(0)("Freight_Description"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Price_Km = clsCommon.myCdbl(dt.Rows(0)("Price_Km"))
                obj.Rate_Type = clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
                obj.Price_Ltr_KG = clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG"))
                obj.Shift_Charges = clsCommon.myCdbl(dt.Rows(0)("Shift_Charges"))
                obj.Avg_Km_Ltr = clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))
                obj.Diesel_Rate = clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))
                obj.Rental_Type = clsCommon.myCstr(dt.Rows(0)("Rental_Type"))
                obj.Rental_Amount = clsCommon.myCdbl(dt.Rows(0)("Rental_Amount"))
                obj.Is_Additional = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Additional")) > 0, True, False)
                obj.ArrSlab = clsFreightChargesSlab.getData(obj.Freight_Code)
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsFreightChargesSlab
#Region "Variables"
    Public Freight_Code As String = String.Empty
    Public SNo As Integer = 0
    Public Slab_Upto As Double = 0
    Public Slab_Rate As Double = 0
#End Region
  
    Public Shared Function SaveData(ByVal strFreightCode As String, ByVal arr As List(Of clsFreightChargesSlab), ByVal Trans As SqlTransaction) As Boolean
        Try
            DeleteData(strFreightCode, Trans)
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    If arr.Item(i).Slab_Upto > 0 AndAlso arr.Item(i).Slab_Rate > 0 Then
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Freight_Code", strFreightCode)
                        clsCommon.AddColumnsForChange(coll, "Slab_Upto", arr.Item(i).Slab_Upto)
                        clsCommon.AddColumnsForChange(coll, "Slab_Rate", arr.Item(i).Slab_Rate)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FREIGHT_CHARGES_SLAB", OMInsertOrUpdate.Insert, "", Trans)
                    End If
                Next

                Dim qry As String = "select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where Freight_Code='" + strFreightCode + "' group by Slab_Upto having sum(1)>1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strException As String = "Repeted slab exists " + Environment.NewLine
                    strException += " Slab Upto -" + clsCommon.myCstr(dt.Rows(0)("Slab_Upto"))
                    Throw New Exception(strException)
                End If

                qry = "select Slab_Upto,Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where Freight_Code='" + strFreightCode + "' order by Slab_Upto"
                dt = clsDBFuncationality.GetDataTable(qry, Trans)
                Dim ii As Integer = 1
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        qry = "Update TSPL_FREIGHT_CHARGES_SLAB set SNO='" + clsCommon.myCstr(ii) + "' where Freight_Code='" + strFreightCode + "' and Slab_Upto='" + clsCommon.myCstr(dr("Slab_Upto")) + "' and Slab_Rate='" + clsCommon.myCstr(dr("Slab_Rate")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                        ii += 1
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strFreightCode As String) As List(Of clsFreightChargesSlab)
        Dim arr As New List(Of clsFreightChargesSlab)
        Try
            Dim obj As New clsFreightChargesSlab
            Dim qry As String = "select * from TSPL_FREIGHT_CHARGES_SLAB where Freight_Code='" & strFreightCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsFreightChargesSlab()
                    obj.Freight_Code = clsCommon.myCstr(dt.Rows(i)("Freight_Code"))
                    obj.Slab_Upto = clsCommon.myCdbl(dt.Rows(i)("Slab_Upto"))
                    obj.Slab_Rate = clsCommon.myCdbl(dt.Rows(i)("Slab_Rate"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function DeleteData(ByVal strFreightCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_FREIGHT_CHARGES_SLAB where  Freight_Code='" & strFreightCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMCCVehicleFreightChargesMapping
#Region "Variables"
    Public MCC_Code As String = String.Empty
    Public MCC_Name As String = String.Empty
    Public Freight_Code As String = String.Empty
    Public Freight_Name As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public Tanker_Name As String = String.Empty
#End Region
    Public Shared Function SaveData(ByVal strMCCCode As String, ByVal arr As List(Of clsMCCVehicleFreightChargesMapping)) As Boolean
        Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strMCCCode, arr, Trans)
            Trans.Commit()
        Catch ex As Exception
            Trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strMCCCode As String, ByVal arr As List(Of clsMCCVehicleFreightChargesMapping), ByVal Trans As SqlTransaction) As Boolean
        Try
            DeleteData(strMCCCode, Trans)
            Dim i As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCCCode)
                    clsCommon.AddColumnsForChange(coll, "Freight_Code", arr.Item(i).Freight_Code)
                    clsCommon.AddColumnsForChange(coll, "Tanker_No", arr.Item(i).Tanker_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING", OMInsertOrUpdate.Insert, "", Trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strMCCCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "Delete from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING where  MCC_Code='" & strMCCCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strMCCCode As String) As List(Of clsMCCVehicleFreightChargesMapping)
        Dim arr As New List(Of clsMCCVehicleFreightChargesMapping)
        Try
            Dim obj As New clsMCCVehicleFreightChargesMapping
            Dim qry As String = "select TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.*,TSPL_MCC_MASTER.MCC_NAME,TSPL_TANKER_MASTER.Tanker_Name,TSPL_FREIGHT_CHARGES_MASTER.Freight_Description from TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.MCC_Code left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Tanker_No left outer join TSPL_FREIGHT_CHARGES_MASTER on TSPL_FREIGHT_CHARGES_MASTER.Freight_Code=TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.Freight_Code where TSPL_MCC_VEHICLE_FREIGHT_CHARGES_MAAPPING.MCC_Code='" & strMCCCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMCCVehicleFreightChargesMapping()
                    obj.MCC_Code = clsCommon.myCstr(dt.Rows(i)("MCC_Code"))
                    obj.MCC_Name = clsCommon.myCstr(dt.Rows(i)("MCC_NAME"))
                    obj.Freight_Code = clsCommon.myCstr(dt.Rows(i)("Freight_Code"))
                    obj.Freight_Name = clsCommon.myCstr(dt.Rows(i)("Freight_Description"))
                    obj.Tanker_No = clsCommon.myCstr(dt.Rows(i)("Tanker_No"))
                    obj.Tanker_Name = clsCommon.myCstr(dt.Rows(i)("Tanker_Name"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function

End Class
