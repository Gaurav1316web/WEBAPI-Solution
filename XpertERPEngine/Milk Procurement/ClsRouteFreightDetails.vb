Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsRouteFreightDetails
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public ToLocation_Code As String = Nothing
    Public ToLocation_Name As String = Nothing
    Public City_Code As String = Nothing
    Public City_Name As String = Nothing
    Public Transport_Id As String = Nothing
    Public Transport_Name As String = Nothing
    Public CapacityMT As String = Nothing
    Public Freight As String = Nothing
    Public Fixed As String = Nothing
    Public EffectiveDate As String = Nothing
    Public Type As String = Nothing
    Public TransType As String = Nothing
    Public UpdateCity As String = Nothing
    Public UpdateLocation As String = Nothing
    Public UpdateTransporter As String = Nothing
    Public UpdateToLocation As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal StType As String, ByVal StTransType As String, ByVal StLocation_Code As String, ByVal StToLocation_Code As String, ByVal StCity_Code As String, ByVal StTransport_Id As String, ByVal StCapacityMT As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            'If (clsCommon.myLen(strCode) <= 0) Then
            '    Throw New Exception("Code not found to Delete")
            'End If

            Dim qry As String
            qry = "Delete From TSPL_ROUTE_FREIGHT_DETAILS where TSPL_ROUTE_FREIGHT_DETAILS.Type='" + StType + "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" + StTransType + "' and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" + StLocation_Code + "' and CapacityMT='" + clsCommon.myCstr(StCapacityMT) + "' and Transport_Id ='" + clsCommon.myCstr(StTransport_Id) + "'"

            If clsCommon.myLen(StToLocation_Code) > 0 Then
                qry += " and TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code='" + StToLocation_Code + "'"
            Else
                qry += " and ToLocation_Code  is null "
            End If
            If clsCommon.myLen(StCity_Code) > 0 Then
                qry += " and TSPL_ROUTE_FREIGHT_DETAILS.City_Code='" + StCity_Code + "'"
            Else
                qry += " and City_Code  is null "
            End If


            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal DocType As String, ByVal TransType As String, ByVal trans As SqlTransaction) As List(Of ClsRouteFreightDetails)

        Dim arr As New List(Of ClsRouteFreightDetails)
        Dim qry As String = "select TSPL_ROUTE_FREIGHT_DETAILS.Location_Code,FromLoc.Location_Desc as From_Location,TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code,ToLoc.Location_Desc as To_Loc_Name  ,TSPL_ROUTE_FREIGHT_DETAILS.City_Code,TSPL_CITY_MASTER.City_Name  ,TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT ,TSPL_ROUTE_FREIGHT_DETAILS.Freight ,TSPL_ROUTE_FREIGHT_DETAILS.Fixed,convert(varchar,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103) as Effective_Date ,TSPL_ROUTE_FREIGHT_DETAILS.Comp_Code ,TSPL_ROUTE_FREIGHT_DETAILS.Created_By ,convert(varchar,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103) as Effective_Date ,TSPL_ROUTE_FREIGHT_DETAILS.Modify_By ,convert(varchar,TSPL_ROUTE_FREIGHT_DETAILS.Modified_Date,103) as Modified_Date ,TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_ROUTE_FREIGHT_DETAILS.Type,TSPL_ROUTE_FREIGHT_DETAILS.TransType  from TSPL_ROUTE_FREIGHT_DETAILS" & _
                            " left join TSPL_LOCATION_MASTER as FromLoc on FromLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code" & _
                            " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code" & _
                            " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code" & _
                            " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id where 2=2 and TSPL_ROUTE_FREIGHT_DETAILS.Type='" & DocType & "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" & TransType & "' order by convert(date,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103)"
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim obj As New ClsRouteFreightDetails
                obj.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                obj.Location_Name = clsCommon.myCstr(dr("From_Location"))
                obj.ToLocation_Code = clsCommon.myCstr(dr("ToLocation_Code"))
                obj.ToLocation_Name = clsCommon.myCstr(dr("To_Loc_Name"))
                obj.City_Code = clsCommon.myCstr(dr("City_Code"))
                obj.City_Name = clsCommon.myCstr(dr("City_Name"))
                obj.Transport_Id = clsCommon.myCstr(dr("Transport_Id"))
                obj.Transport_Name = clsCommon.myCstr(dr("Transport_Id"))
                obj.CapacityMT = clsCommon.myCdbl(dr("CapacityMT"))
                obj.Freight = clsCommon.myCdbl(dr("Freight"))
                obj.Fixed = clsCommon.myCdbl(dr("Fixed"))
                obj.EffectiveDate = clsCommon.myCstr(dr("Effective_Date"))
                obj.Type = clsCommon.myCstr(dr("Type"))
                obj.TransType = clsCommon.myCstr(dr("TransType"))
                arr.Add(obj)
            Next
            Return arr

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal DocType As String, ByVal TransType As String, ByVal arr As List(Of ClsRouteFreightDetails)) As Boolean
        Dim trans As SqlTransaction = Nothing
     
        clsCommon.ProgressBarPercentShow()
        Try

            trans = clsDBFuncationality.GetTransactin()
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocType, "TSPL_ROUTE_FREIGHT_DETAILS", "Type", trans)
            'clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_ROUTE_FREIGHT_DETAILS where Type='" & DocType & "' and TransType='" & TransType & "'", trans)
            Dim dtCurrent As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            For ii As Integer = 0 To arr.Count - 1
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", arr(ii).Location_Code)
                clsCommon.AddColumnsForChange(coll, "ToLocation_Code", arr(ii).ToLocation_Code, True)
                clsCommon.AddColumnsForChange(coll, "City_Code", arr(ii).City_Code, True)
                clsCommon.AddColumnsForChange(coll, "Transport_Id", arr(ii).Transport_Id)
                clsCommon.AddColumnsForChange(coll, "CapacityMT", arr(ii).CapacityMT)
                clsCommon.AddColumnsForChange(coll, "Freight", arr(ii).Freight)
                clsCommon.AddColumnsForChange(coll, "Fixed", arr(ii).Fixed)
                clsCommon.AddColumnsForChange(coll, "Type", arr(ii).Type)
                clsCommon.AddColumnsForChange(coll, "TransType", arr(ii).TransType)
                If arr(ii).EffectiveDate IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(arr(ii).EffectiveDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", dtCurrent)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", dtCurrent)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

               

                If CheckNewEntry(arr(ii).Type, arr(ii).TransType, arr(ii).Location_Code, arr(ii).ToLocation_Code, arr(ii).City_Code, arr(ii).Transport_Id, arr(ii).CapacityMT, trans) = True Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_FREIGHT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Dim qry1 As String = "delete from TSPL_ROUTE_FREIGHT_DETAILS where Type='" + clsCommon.myCstr(arr(ii).Type) + "' and TransType='" + clsCommon.myCstr(arr(ii).TransType) + "' and Location_Code='" + clsCommon.myCstr(arr(ii).Location_Code) + "'"
                    If clsCommon.myLen(arr(ii).ToLocation_Code) > 0 Then
                        qry1 += " and ToLocation_Code='" + clsCommon.myCstr(arr(ii).ToLocation_Code) + "'"
                    Else
                        qry1 += " and ToLocation_Code  is null "
                    End If
                    If clsCommon.myLen(arr(ii).City_Code) > 0 Then
                        qry1 += " and City_Code='" + clsCommon.myCstr(arr(ii).City_Code) + "'"
                    Else
                        qry1 += " and City_Code  is null "
                    End If

                    qry1 += " and CapacityMT='" + clsCommon.myCstr(arr(ii).CapacityMT) + "' and Transport_Id ='" + clsCommon.myCstr(arr(ii).Transport_Id) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_FREIGHT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                End If

                clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100 / (arr.Count + 1)), "Saving : " & clsCommon.myCstr(ii + 1) & "/" & clsCommon.myCstr(arr.Count) & "")
            Next
            
            Dim qry As String = "select Location_Code,ToLocation_Code ,City_Code,CapacityMT,Transport_Id,Effective_Date,Type,TransType, SUM(1) as Repeated from TSPL_ROUTE_FREIGHT_DETAILS group by Location_Code,ToLocation_Code ,City_Code,CapacityMT,Transport_Id,Effective_Date,Type,TransType  having SUM(1) > 1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(TransType, "P") = CompairStringResult.Equal Then
                    Throw New Exception("Please check ! Location, City and transpoter already used type is " & clsCommon.myCstr(dt.Rows(0)("Type")) & " and transaction type is " & clsCommon.myCstr(dt.Rows(0)("TransType")) & " repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                Else
                    Throw New Exception("Please check ! Location, To Location and transpoter already used type is " & clsCommon.myCstr(dt.Rows(0)("Type")) & " and transaction type is " & clsCommon.myCstr(dt.Rows(0)("TransType")) & " repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If
            End If

            '--Check location code
            qry = "select Location_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where Type='" & DocType & "' and TransType='" & TransType & "' and not exists(select 1 from TSPL_LOCATION_MASTER  WHERE TSPL_LOCATION_MASTER.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Location code: " + clsCommon.myCstr(dt.Rows(0)("LocCode")) + " not exists !" + Environment.NewLine + "")
            End If

            '--Check to location code
            qry = "select ToLocation_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(ToLocation_Code,''))>0 and Type='" & DocType & "' and TransType='" & TransType & "' and not exists(select 1 from TSPL_LOCATION_MASTER  WHERE TSPL_LOCATION_MASTER.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("To Location code:" + clsCommon.myCstr(dt.Rows(0)("ToLocation_Code")) + " not exists !" + Environment.NewLine + "")
            End If

            '--Check City code
            qry = "select City_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(City_Code,''))>0 and Type='" & DocType & "' and TransType='" & TransType & "' and not exists(select 1 from TSPL_CITY_MASTER  WHERE TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("City code:" + clsCommon.myCstr(dt.Rows(0)("City_Code")) + " not exists !")
            End If

            '--Check From Transporter code
            qry = "select Transport_Id from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(Transport_Id,''))>0 and Type='" & DocType & "' and TransType='" & TransType & "' and not exists(select 1 from TSPL_TRANSPORT_MASTER  WHERE TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Transporter ID :" + clsCommon.myCstr(dt.Rows(0)("Transport_Id")) + " not exists !")
            End If

            trans.Commit()
            clsCommon.ProgressBarPercentHide()

        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CheckNewEntry(ByVal StType As String, ByVal StTransType As String, ByVal StLocation_Code As String, ByVal StToLocation_Code As String, ByVal StCity_Code As String, ByVal StTransport_Id As String, ByVal StCapacityMT As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select * from TSPL_ROUTE_FREIGHT_DETAILS where TSPL_ROUTE_FREIGHT_DETAILS.Type='" & StType & "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" & StTransType & "' and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" & StLocation_Code & "'"

        If clsCommon.myLen(StToLocation_Code) > 0 Then
            qry += " and ToLocation_Code='" + clsCommon.myCstr(StToLocation_Code) + "'"
        Else
            qry += " and ToLocation_Code  is null "
        End If
        If clsCommon.myLen(StCity_Code) > 0 Then
            qry += " and City_Code='" + clsCommon.myCstr(StCity_Code) + "'"
        Else
            qry += " and City_Code  is null "
        End If

        qry += " and CapacityMT='" + clsCommon.myCstr(StCapacityMT) + "' and Transport_Id ='" + clsCommon.myCstr(StTransport_Id) + "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function


    Public Shared Function GetDataTable(ByVal DocType As String, ByVal TransType As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select TSPL_ROUTE_FREIGHT_DETAILS.Location_Code as [Location Code],FromLoc.Location_Desc as [Location Name],TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code as [To Location Code],ToLoc.Location_Desc as [To Location Name],TSPL_ROUTE_FREIGHT_DETAILS.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name] ,TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT as [Capacity(MT)],TSPL_ROUTE_FREIGHT_DETAILS.Freight,TSPL_ROUTE_FREIGHT_DETAILS.Fixed as [FixedAmt], TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date as [Effective Date]  ,TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id as [Transport Id],TSPL_TRANSPORT_MASTER.Transporter_Name as [Transport Name],TSPL_ROUTE_FREIGHT_DETAILS.Type from TSPL_ROUTE_FREIGHT_DETAILS" & _
                            " left join TSPL_LOCATION_MASTER as FromLoc on FromLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code" & _
                            " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code" & _
                            " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code" & _
                            " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id where 2=2 and TSPL_ROUTE_FREIGHT_DETAILS.Type='" & DocType & "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" & TransType & "' order by convert(date,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103)"
        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function

    Public Shared Function SaveDataChild(ByVal obj As ClsRouteFreightDetails, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Return SaveDataNew(obj, isNewEntry, trans)
       
    End Function

    Public Shared Function SaveDataNew(ByVal obj As ClsRouteFreightDetails, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim dtCurrent As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")

            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "ToLocation_Code", obj.ToLocation_Code, True)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code, True)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
            clsCommon.AddColumnsForChange(coll, "CapacityMT", obj.CapacityMT)
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Fixed", obj.Fixed)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "TransType", obj.TransType)



            If obj.EffectiveDate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.EffectiveDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Effective_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", dtCurrent)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry = True Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", dtCurrent)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_FREIGHT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Else
                If clsCommon.CompairString(obj.TransType, "T") = CompairStringResult.Equal Then

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_FREIGHT_DETAILS", OMInsertOrUpdate.Update, "TSPL_ROUTE_FREIGHT_DETAILS.city_code is null and TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code='" + obj.UpdateToLocation + "' and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" + obj.UpdateLocation + "' and TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id='" + obj.UpdateTransporter + "' and TSPL_ROUTE_FREIGHT_DETAILS.Type='" + obj.Type + "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" + obj.TransType + "' and  TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT='" + obj.CapacityMT + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_FREIGHT_DETAILS", OMInsertOrUpdate.Update, "TSPL_ROUTE_FREIGHT_DETAILS.city_code='" + obj.UpdateCity + "'and TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code is null and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" + obj.UpdateLocation + "' and TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id='" + obj.UpdateTransporter + "' and TSPL_ROUTE_FREIGHT_DETAILS.Type='" + obj.Type + "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" + obj.TransType + "' and  TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT='" + obj.CapacityMT + "'", trans)
                End If

            End If

            Dim qry As String = "select Location_Code,ToLocation_Code ,City_Code,CapacityMT,Transport_Id,Effective_Date,Type,TransType, SUM(1) as Repeated from TSPL_ROUTE_FREIGHT_DETAILS where TransType='" & obj.TransType & "' group by Location_Code,ToLocation_Code ,City_Code,CapacityMT,Transport_Id,Effective_Date,Type,TransType  having SUM(1) > 1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(obj.TransType, "P") = CompairStringResult.Equal Then
                    Throw New Exception("Please check ! Location, City and transpoter already used type is " & clsCommon.myCstr(dt.Rows(0)("Type")) & " and transaction type is " & clsCommon.myCstr(dt.Rows(0)("TransType")) & " repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                Else
                    Throw New Exception("Please check ! Location, To Location and transpoter already used type is " & clsCommon.myCstr(dt.Rows(0)("Type")) & " and transaction type is " & clsCommon.myCstr(dt.Rows(0)("TransType")) & " repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If
            End If

            '--Check location code
            qry = "select Location_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where Type='" & obj.Type & "' and TransType='" & obj.TransType & "' and not exists(select 1 from TSPL_LOCATION_MASTER  WHERE TSPL_LOCATION_MASTER.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Location code: " + clsCommon.myCstr(dt.Rows(0)("LocCode")) + " not exists !" + Environment.NewLine + "")
            End If

            '--Check to location code
            qry = "select ToLocation_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(ToLocation_Code,''))>0 and Type='" & obj.Type & "' and TransType='" & obj.TransType & "' and not exists(select 1 from TSPL_LOCATION_MASTER  WHERE TSPL_LOCATION_MASTER.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("To Location code:" + clsCommon.myCstr(dt.Rows(0)("ToLocation_Code")) + " not exists !" + Environment.NewLine + "")
            End If

            '--Check City code
            qry = "select City_Code from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(City_Code,''))>0 and Type='" & obj.Type & "' and TransType='" & obj.TransType & "' and not exists(select 1 from TSPL_CITY_MASTER  WHERE TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("City code:" + clsCommon.myCstr(dt.Rows(0)("City_Code")) + " not exists !")
            End If

            '--Check From Transporter code
            qry = "select Transport_Id from  TSPL_ROUTE_FREIGHT_DETAILS  where len(isnull(Transport_Id,''))>0 and Type='" & obj.Type & "' and TransType='" & obj.TransType & "' and not exists(select 1 from TSPL_TRANSPORT_MASTER  WHERE TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id) "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Transporter ID :" + clsCommon.myCstr(dt.Rows(0)("Transport_Id")) + " not exists !")
            End If



            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try

    End Function
End Class
