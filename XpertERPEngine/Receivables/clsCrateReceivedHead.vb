'' wrk to be done related SMS agaist ticket no. BHA/01/11/18-000658
Imports common
Imports System.Data.SqlClient
Public Class clsCrateReceivedHead

#Region "Variable"
    Public TotalCrateQty As Double = 0
    Public TotalCanQty As Double = 0
    Public Vehicle_Code As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public Invoice_Date As String = Nothing
    Public Location_Code As String = Nothing
    Public Comments As String = Nothing
    Public Type As String = Nothing
    Public ShiftType As String = String.Empty
    Public Posted As Integer
    Public Posting_Date As String = Nothing
    Public Arr As List(Of clsCrateReceivedDetail) = Nothing
    Public Closing_Customer As Integer
    Public Trans_Type As String = "Crate"
    Dim qry As String
    Public Crate_Item As String = String.Empty
    Public Crate_ItemUnit As String = String.Empty
    Public Crate_ItemRate As Double = 0
    Public Can_Item As String = String.Empty
    Public Can_ItemUnit As String = String.Empty
    Public Can_ItemRate As Double = 0
    Public Route_code As String = Nothing
    Public Route_Name As String = Nothing
    Public Driver As String = Nothing
    Public SalesMan As String = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsCrateReceivedHead, ByVal isNewEntry As Boolean) As Boolean
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
    ' Ticket No : TEC/18/06/19-000541 By Prabhakar
    Public Function SaveData(ByVal obj As clsCrateReceivedHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fresh Sale", "Fresh Crate Received", obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanReceived, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmCrateReceviedDairySale, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)

        Dim isSaved As Boolean = True

        If Not isNewEntry Then
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", "Document_No", "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE", "Document_No", trans)
        End If
        qry = "delete from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""
        If isNewEntry Then
            If clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCanReceived, "", obj.Location_Code)
            Else
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmCreateReceived, "", obj.Location_Code)
            End If
        End If

        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()

        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
        clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
        clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
        clsCommon.AddColumnsForChange(coll, "Closing_Cust", obj.Closing_Customer)
        clsCommon.AddColumnsForChange(coll, "TotalCanQty", obj.TotalCanQty)
        clsCommon.AddColumnsForChange(coll, "TotalCrateQty", obj.TotalCrateQty)
        clsCommon.AddColumnsForChange(coll, "Route_code", obj.Route_code)
        clsCommon.AddColumnsForChange(coll, "Driver", obj.Driver)
        clsCommon.AddColumnsForChange(coll, "SalesMan", obj.SalesMan)
        clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", OMInsertOrUpdate.Update, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No='" + obj.Document_No + "'", trans)
        End If

        isSaved = isSaved AndAlso clsCrateReceivedDetail.SaveData(obj.Document_No, Arr, trans)
        ' done by priti BHA/09/05/18-000022
        Dim strCrateCan As String = ""
        Dim AllowCratePhysicalStock As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCratePhysicalStock, clsFixedParameterCode.AllowCratePhysicalStock, trans))
        If AllowCratePhysicalStock = 1 Or clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
            Dim strCrateItem As String = String.Empty
            Dim strCrateUOM As String = ""
            Dim dblCrateRate As Integer = 0
            Dim strCanItem As String = ""
            Dim strCanUOM As String = ""
            Dim dblCanRate As Integer = 0

            If clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
                strCanItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(Can,0)=1", trans)
                If clsCommon.myLen(strCanItem) > 0 Then
                    dblCanRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, trans))
                    If dblCanRate > 0 Then
                        strCanUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCanItem & "' and Default_UOM=1", trans)
                        qry = "Update TSPL_CRATE_RECEIVED_HEAD_FRESHSALE set Can_Item='" & strCanItem & "',Can_ItemUnit='" & strCanUOM & "',Can_ItemRate='" & dblCanRate & "' where Document_No='" & obj.Document_No & "' "
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        Throw New Exception("Please enter Can Rate in Fixed parameter Item default can Rate. ")
                    End If
                Else
                    Throw New Exception("Please Create Item as Can Type. ")
                End If
            Else              
                If obj.TotalCrateQty > 0 Then
                    strCrateItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(CRATE,0)=1", trans)
                    If clsCommon.myLen(strCrateItem) > 0 Then
                        dblCrateRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, trans))
                        If dblCrateRate > 0 Then
                            strCrateUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCrateItem & "' and Default_UOM=1", trans)
                            qry = "Update TSPL_CRATE_RECEIVED_HEAD_FRESHSALE set Crate_Item='" & strCrateItem & "',Crate_ItemUnit='" & strCrateUOM & "',Crate_ItemRate='" & dblCrateRate & "' where Document_No='" & obj.Document_No & "' "
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Else
                            Throw New Exception("Please enter Crate Rate in Fixed parameter Item default Crate Rate. ")
                        End If
                    Else
                        Throw New Exception("Please Create Item as Crate Type. ")
                    End If
                End If
                If obj.TotalCanQty > 0 Then
                    strCanItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(Can,0)=1", trans)
                    If clsCommon.myLen(strCanItem) > 0 Then
                        dblCanRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, trans))
                        If dblCanRate > 0 Then
                            strCanUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCanItem & "' and Default_UOM=1", trans)
                            qry = "Update TSPL_CRATE_RECEIVED_HEAD_FRESHSALE set Can_Item='" & strCanItem & "',Can_ItemUnit='" & strCanUOM & "',Can_ItemRate='" & dblCanRate & "' where Document_No='" & obj.Document_No & "' "
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Else
                            Throw New Exception("Please enter Can Rate in Fixed parameter Item default Can Rate. ")
                        End If
                    Else
                        Throw New Exception("Please Create Item as Can Type. ")
                    End If
                End If

            End If

        End If

        Return isSaved

    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsCrateReceivedHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCrateReceivedHead
        Dim obj As clsCrateReceivedHead = Nothing
        Dim qry = "select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Driver,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.salesMan,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Can_Item,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Can_ItemUnit,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Can_ItemRate,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.TotalCanQty,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.TotalCrateQty,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Crate_Item,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Crate_ItemUnit,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Crate_ItemRate,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Trans_Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Closing_Cust,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date, " &
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posting_Date, " &
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comments,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comp_Code,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Created_By, " &
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Created_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Modified_By,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Modified_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.route_code,tspl_route_master.route_desc,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType " &
        "From TSPL_CRATE_RECEIVED_HEAD_FRESHSALE left join tspl_route_master on tspl_route_master.route_No=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.route_Code where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = (select MIN(Document_No) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = (select Min(Document_No) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = (select Max(Document_No) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCrateReceivedHead()
            obj.TotalCrateQty = clsCommon.myCdbl(dt.Rows(0)("TotalCrateQty"))
            obj.TotalCanQty = clsCommon.myCdbl(dt.Rows(0)("TotalCanQty"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Invoice_Date = clsCommon.myCDate(dt.Rows(0)("Invoice_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.ShiftType = clsCommon.myCstr(dt.Rows(0)("ShiftType"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))

            obj.Crate_Item = clsCommon.myCstr(dt.Rows(0)("Crate_Item"))
            obj.Crate_ItemUnit = clsCommon.myCstr(dt.Rows(0)("Crate_ItemUnit"))
            obj.Crate_ItemRate = clsCommon.myCdbl(dt.Rows(0)("Crate_ItemRate"))

            obj.Can_Item = clsCommon.myCstr(dt.Rows(0)("Can_Item"))
            obj.Can_ItemUnit = clsCommon.myCstr(dt.Rows(0)("Can_ItemUnit"))
            obj.Can_ItemRate = clsCommon.myCdbl(dt.Rows(0)("Can_ItemRate"))

            obj.Closing_Customer = clsCommon.myCdbl(dt.Rows(0)("Closing_Cust"))
            obj.Route_code = clsCommon.myCstr(dt.Rows(0)("Route_code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("route_desc"))
            obj.Driver = clsCommon.myCstr(dt.Rows(0)("Driver"))
            obj.SalesMan = clsCommon.myCstr(dt.Rows(0)("SalesMan"))

            qry = "select TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Route_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyPreviousDay,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyManual ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.BoxQtyRecd ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.jaaliOutQty ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.boxOutQty ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.jaaliAdjustment ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.boxAdjustment, TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.jaali,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.box,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.OutQty,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Adjustment,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQty,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Balance,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Line_No, " &
            "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Sale_Invoice_No, " &
            "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Sale_Invoice_Date,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Salesman_Code, " &
            "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Salesman_Name,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code, " &
            "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd, " &
            "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Remarks,isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.LinerQty,0) as LinerQty ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANQty,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANQtyRec,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANOutQty,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANAdjustment , TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.DamageCrateQtyRecd From TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE where Document_No='" & obj.Document_No & "' order by line_no"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsCrateReceivedDetail)
                Dim objTr As clsCrateReceivedDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCrateReceivedDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                    'objTr.Sale_Invoice_No = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    'objTr.Sale_Invoice_Date = clsCommon.myCDate(dr("Sale_Invoice_Date"))
                    'objTr.Salesman_Code = clsCommon.myCstr(dr("Salesman_Code"))
                    'objTr.Salesman_Name = clsCommon.myCstr(dr("Salesman_Name"))
                    objTr.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_Code"))
                    objTr.VehicleNo = clsCommon.myCstr(dr("VehicleNo"))
                    objTr.CrateQty = clsCommon.myCdbl(dr("CrateQty"))
                    objTr.CrateQtyRecd = clsCommon.myCdbl(dr("CrateQtyRecd"))
                    objTr.CrateQtyPreviousDay = clsCommon.myCdbl(dr("CrateQtyPreviousDay"))
                    objTr.Balance = clsCommon.myCdbl(dr("Balance"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.OutQty = clsCommon.myCdbl(dr("OutQty"))
                    objTr.Adjustment = clsCommon.myCdbl(dr("Adjustment"))
                    objTr.Jaali = clsCommon.myCdbl(dr("jaali"))
                    objTr.Box = clsCommon.myCdbl(dr("box"))

                    objTr.CrateQtyManual = clsCommon.myCdbl(dr("CrateQtyManual"))
                    objTr.JaaliQtyRecd = clsCommon.myCdbl(dr("JaaliQtyRecd"))
                    objTr.BoxQtyRecd = clsCommon.myCdbl(dr("BoxQtyRecd"))
                    objTr.jaaliOutQty = clsCommon.myCdbl(dr("jaaliOutQty"))
                    objTr.boxOutQty = clsCommon.myCdbl(dr("boxOutQty"))
                    objTr.jaaliAdjustment = clsCommon.myCdbl(dr("jaaliAdjustment"))
                    objTr.boxAdjustment = clsCommon.myCdbl(dr("boxAdjustment"))
                    objTr.LinerQty = clsCommon.myCdbl(dr("LinerQty"))


                    objTr.CANQty = clsCommon.myCdbl(dr("CANQty"))
                    objTr.CANRecQty = clsCommon.myCdbl(dr("canQtyRec"))
                    objTr.CANOutQty = clsCommon.myCdbl(dr("CANOutQty"))
                    objTr.CANAdjustment = clsCommon.myCdbl(dr("CANAdjustment"))
                    objTr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    objTr.DamageCrateQtyRecd = clsCommon.myCstr(dr("DamageCrateQtyRecd"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCrateReceivedHead = clsCrateReceivedHead.GetData(strCode, NavigatorType.Current)

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanReceived, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmCrateReceviedDairySale, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)


                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", "Document_No", "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE", "Document_No", trans)
                Dim qry = "delete from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            '=============added by preeti gupta Against ticket no[BHA/14/08/18-000425]===================
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
            '=========================================================================
            Dim isSaved As Boolean = True
            Dim dblRate As Integer = 0
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsCrateReceivedHead = clsCrateReceivedHead.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCanReceived, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmCrateReceviedDairySale, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            'Ticket No-TEC/17/05/19-000497 Sanjay
            '"Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
            '"Modified_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' " & _
            Dim qry = "Update TSPL_CRATE_RECEIVED_HEAD_FRESHSALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'" & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", "Document_No", trans)

            Dim AllowCratePhysicalStock As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCratePhysicalStock, clsFixedParameterCode.AllowCratePhysicalStock, trans))
            If AllowCratePhysicalStock = 1 Or clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
                Dim strCrateItem As String = String.Empty
                Dim strCrateUOM As String = ""
                Dim dblCrateRate As Double = 0
                Dim strCanItem As String = ""
                Dim strCanUOM As String = ""
                Dim dblCanRate As Integer = 0
                Dim strItemType As String = ""
                Dim strItemTypeToSave As String = ""
                If clsCommon.myCDate(obj.Document_Date) >= clsCommon.myCDate(objCommonVar.ERPStartDate) Then
                    If clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
                        strCanItem = obj.Can_Item
                        strCanUOM = obj.Can_ItemUnit
                        dblCanRate = obj.Can_ItemRate
                        If clsCommon.myLen(strCanItem) > 0 Then
                            Dim dblEnteredQty As Double = 0
                            strItemType = clsItemMaster.GetItemType(strCrateItem, trans)
                            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                strItemTypeToSave = "RM"
                            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                strItemTypeToSave = "OT"
                            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                strItemTypeToSave = "FT"
                            Else
                                strItemTypeToSave = strItemType
                            End If
                            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                                For Each objTr As clsCrateReceivedDetail In obj.Arr
                                    dblEnteredQty += objTr.CANRecQty + objTr.CANAdjustment
                                Next
                                For Each objTr As clsCrateReceivedDetail In obj.Arr
                                    Dim objInventoryMovemnt As New clsInventoryMovement()
                                    Dim ArrInventoryMovementCan As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                                    objInventoryMovemnt.InOut = "I"
                                    objInventoryMovemnt.Location_Code = obj.Location_Code

                                    objInventoryMovemnt.Cust_Code = objTr.Customer_Code
                                    objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(objTr.Customer_Code, trans)

                                    objInventoryMovemnt.Item_Code = strCanItem
                                    objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCanItem, trans)
                                    objInventoryMovemnt.Qty = objTr.CrateQtyRecd + objTr.CANAdjustment
                                    objInventoryMovemnt.UOM = strCanUOM
                                    objInventoryMovemnt.Basic_Cost = dblCanRate
                                    objInventoryMovemnt.MRP = 0
                                    objInventoryMovemnt.Add_Cost = 0
                                    objInventoryMovemnt.Net_Cost = 0
                                    objInventoryMovemnt.ItemType = strItemTypeToSave
                                    ArrInventoryMovementCan.Add(objInventoryMovemnt)
                                    clsInventoryMovement.SaveData("CAN-REC", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCan, trans)
                                Next
                                CreateJournalEntry(obj.Trans_Type, obj.Document_No, 0, dblEnteredQty, trans)
                            End If
                        End If
                    Else
                        If obj.TotalCrateQty > 0 OrElse obj.TotalCanQty > 0 Then
                            strCrateItem = obj.Crate_Item
                            strCrateUOM = obj.Crate_ItemUnit
                            dblCrateRate = obj.Crate_ItemRate

                            strCanItem = obj.Can_Item
                            strCanUOM = obj.Can_ItemUnit
                            dblCanRate = obj.Can_ItemRate
                            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                                Dim dblCrateEnteredQty As Double = obj.TotalCrateQty
                                Dim dblCanEnteredQty As Double = obj.TotalCanQty
                                For Each objTr As clsCrateReceivedDetail In obj.Arr
                                    If clsCommon.myCdbl(objTr.CrateQtyRecd + objTr.Adjustment > 0) Then
                                        strItemType = clsItemMaster.GetItemType(strCrateItem, trans)
                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "OT"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "FT"
                                        Else
                                            strItemTypeToSave = strItemType
                                        End If
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        Dim ArrInventoryMovementCrate As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                                        objInventoryMovemnt.InOut = obj.Type
                                        objInventoryMovemnt.Location_Code = obj.Location_Code

                                        objInventoryMovemnt.Cust_Code = objTr.Customer_Code
                                        objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(objTr.Customer_Code, trans)
                                        'done by priti BHA/26/06/18-000087 for correction of item code.
                                        objInventoryMovemnt.Item_Code = obj.Crate_Item
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(obj.Crate_ItemUnit, trans)
                                        objInventoryMovemnt.Qty = objTr.CrateQtyRecd + objTr.Adjustment
                                        objInventoryMovemnt.UOM = obj.Crate_ItemUnit
                                        objInventoryMovemnt.Basic_Cost = dblCrateRate
                                        objInventoryMovemnt.MRP = 0
                                        objInventoryMovemnt.Add_Cost = 0
                                        objInventoryMovemnt.Net_Cost = 0
                                        objInventoryMovemnt.ItemType = strItemTypeToSave
                                        ArrInventoryMovementCrate.Add(objInventoryMovemnt)
                                        clsInventoryMovement.SaveData("CRATE-REC", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCrate, trans)
                                    End If
                                    If clsCommon.myCdbl(objTr.CANRecQty + objTr.CANAdjustment > 0) Then
                                        strItemType = clsItemMaster.GetItemType(strCanItem, trans)
                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "OT"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "FT"
                                        Else
                                            strItemTypeToSave = strItemType
                                        End If
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        Dim ArrInventoryMovementCan As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                                        objInventoryMovemnt.InOut = obj.Type
                                        objInventoryMovemnt.Location_Code = obj.Location_Code

                                        objInventoryMovemnt.Cust_Code = objTr.Customer_Code
                                        objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(objTr.Customer_Code, trans)

                                        objInventoryMovemnt.Item_Code = obj.Can_Item
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(obj.Can_ItemUnit, trans)
                                        objInventoryMovemnt.Qty = objTr.CANRecQty + objTr.CANAdjustment
                                        objInventoryMovemnt.UOM = obj.Can_ItemUnit
                                        objInventoryMovemnt.Basic_Cost = dblCanRate
                                        objInventoryMovemnt.MRP = 0
                                        objInventoryMovemnt.Add_Cost = 0
                                        objInventoryMovemnt.Net_Cost = 0
                                        objInventoryMovemnt.ItemType = strItemTypeToSave
                                        ArrInventoryMovementCan.Add(objInventoryMovemnt)
                                        clsInventoryMovement.SaveData("CRATE-REC", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCan, trans)
                                    End If
                                    '==============Added by preeti Gupta [In case Of Trans Type Out] AGAINST TICKET NO[BHA/17/08/18-000458,BHA/16/08/18-000433,BHA/26/06/19-000914]
                                    If clsCommon.myCdbl(objTr.OutQty) Then
                                        '============Added by preeti gupta[17/08/2018]============
                                        Dim dblBalQty As Double = 0
                                        dblBalQty = clsItemLocationDetails.getBalance(obj.Crate_Item, obj.Location_Code, obj.Document_No, obj.Document_Date, trans, obj.Crate_ItemUnit, 0)
                                        If objTr.OutQty > dblBalQty Then
                                            Throw New Exception("Item - " + obj.Crate_Item + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(objTr.OutQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))

                                        End If

                                        '=========================================================

                                        strItemType = clsItemMaster.GetItemType(strCrateItem, trans)
                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "OT"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "FT"
                                        Else
                                            strItemTypeToSave = strItemType
                                        End If
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        Dim ArrInventoryMovementCrate As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                                        objInventoryMovemnt.InOut = obj.Type
                                        objInventoryMovemnt.Location_Code = obj.Location_Code

                                        objInventoryMovemnt.Cust_Code = objTr.Customer_Code
                                        objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(objTr.Customer_Code, trans)
                                        objInventoryMovemnt.Item_Code = obj.Crate_Item
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(obj.Crate_ItemUnit, trans)
                                        objInventoryMovemnt.Qty = objTr.OutQty
                                        objInventoryMovemnt.UOM = obj.Crate_ItemUnit
                                        objInventoryMovemnt.Basic_Cost = dblCrateRate
                                        objInventoryMovemnt.MRP = 0
                                        objInventoryMovemnt.Add_Cost = 0
                                        objInventoryMovemnt.Net_Cost = 0
                                        objInventoryMovemnt.ItemType = strItemTypeToSave
                                        ArrInventoryMovementCrate.Add(objInventoryMovemnt)
                                        clsInventoryMovement.SaveData("CRATE-REC", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCrate, trans)
                                    End If
                                    If clsCommon.myCdbl(objTr.CANOutQty) Then

                                        '============Added by preeti gupta[17/08/2018]============
                                        Dim dblBalQty As Double = 0
                                        dblBalQty = clsItemLocationDetails.getBalance(obj.Can_Item, obj.Location_Code, obj.Document_No, obj.Document_Date, trans, obj.Can_ItemUnit, 0)
                                        If objTr.CANOutQty > dblBalQty Then
                                            Throw New Exception("Item - " + obj.Can_Item + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(objTr.CANOutQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))

                                        End If

                                        '=========================================================

                                        strItemType = clsItemMaster.GetItemType(strCanItem, trans)
                                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "RM"
                                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "OT"
                                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                            strItemTypeToSave = "FT"
                                        Else
                                            strItemTypeToSave = strItemType
                                        End If
                                        Dim objInventoryMovemnt As New clsInventoryMovement()
                                        Dim ArrInventoryMovementCan As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                                        objInventoryMovemnt.InOut = obj.Type
                                        objInventoryMovemnt.Location_Code = obj.Location_Code

                                        objInventoryMovemnt.Cust_Code = objTr.Customer_Code
                                        objInventoryMovemnt.Cust_Name = clsCustomerMaster.GetName(objTr.Customer_Code, trans)

                                        objInventoryMovemnt.Item_Code = obj.Can_Item
                                        objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(obj.Can_ItemUnit, trans)
                                        objInventoryMovemnt.Qty = objTr.CANOutQty
                                        objInventoryMovemnt.UOM = obj.Can_ItemUnit
                                        objInventoryMovemnt.Basic_Cost = dblCanRate
                                        objInventoryMovemnt.MRP = 0
                                        objInventoryMovemnt.Add_Cost = 0
                                        objInventoryMovemnt.Net_Cost = 0
                                        objInventoryMovemnt.ItemType = strItemTypeToSave
                                        ArrInventoryMovementCan.Add(objInventoryMovemnt)
                                        clsInventoryMovement.SaveData("CRATE-REC", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCan, trans)
                                    End If


                                    '================================================================
                                Next
                                CreateJournalEntry(obj.Trans_Type, obj.Document_No, dblCrateEnteredQty, dblCanEnteredQty, trans)
                            End If

                        End If
                    End If
                End If
            End If
            '' SMS Related Fuuntion
            CreateSMSContent(obj, trans)
            '' end
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Shared Sub CreateSMSContent(ByVal obj As clsCrateReceivedHead, ByVal trans As SqlTransaction)
        Dim Form_ID As String = clsUserMgtCode.frmCrateReceviedDairySale
        Dim itemName As String = ""
        Dim DO_Date As Date

        Dim dtCustomerOutstanding As DataTable = Nothing
        Dim dtCrateCan As DataTable = Nothing
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            Dim qry As String = "select distinct tspl_customer_master.Customer_Name,substring (tspl_customer_master.Phone1 ,6,10) as MobileNo,Cust_Code,tspl_customer_master.Email from tspl_customer_master"
            qry += " left outer join TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE on TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code=tspl_customer_master.Cust_Code"
            qry += " where 2=2 and len(replace( isnull(substring(tspl_customer_master.Phone1,6,10),''),'_',''))>0 and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No='" + obj.Document_No + "' and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.crateqtyrecd <>0 "
            Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


            If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then
                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.arrMobilNo = New List(Of String)()

                    For Each dr As DataRow In dtParty.Rows
                        objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                        Dim qry1 As String = "select max(convert(varchar(12),TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)) as Do_Date ,max(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code) as Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,sum(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd) as CrateQtyRecd,sum(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANQtyRec) as CANQtyRec   "
                        qry1 += " from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE left outer join TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE on TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.DOCUMENT_No=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No"
                        qry1 += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code "
                        qry1 += "  where TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No='" & obj.Document_No & "' and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.customer_code='" & clsCommon.myCstr(dr("Cust_Code")) & "' "
                        qry1 += " group by Customer_Name "
                        Dim dtDocWise As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)

                        For Each drDetail As DataRow In dtDocWise.Rows

                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(drDetail("Customer_Name")))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.Document_No)
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ReturnCRT, clsCommon.myFormat(clsCommon.myCstr(drDetail("CrateQtyRecd"))))
                            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ReturnCAN, clsCommon.myFormat(clsCommon.myCstr(drDetail("CANQtyRec"))))


                            DO_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy")

                            '' Outstanding Amount
                            dtCustomerOutstanding = getCustomerOutstandingOfAmt_Can_Crate("'" & clsCommon.myCstr(drDetail("Customer_Code")) & "'", clsCommon.GetPrintDate(clsCommon.myCDate(DO_Date).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(DO_Date), "dd/MMM/yyyy"), trans)
                            ''end
                            If dtCustomerOutstanding.Rows.Count > 0 Then
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.OutStandingAmt, clsCommon.myFormat(dtCustomerOutstanding.Rows(0)("BalAmt")))

                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CRT, clsCommon.myFormat(dtCustomerOutstanding.Rows(0)("CrateClosing")))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CAN, clsCommon.myFormat(dtCustomerOutstanding.Rows(0)("CanQtyClosing")))
                            End If

                            objSMSH.arrMobilNo = New List(Of String)()
                            objSMSH.arrMobilNo.Add(clsCommon.myCstr(dr("MobileNo")))
                            objSMSH.SaveData(Form_ID, objSMSH, trans)
                        Next
                    Next
                    objSMSH = Nothing
                End If

            End If


        End If
    End Sub
    Public Shared Function getCustomerOutstandingAmtWithOPeningAndClosing(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal ConvRate As String, ByVal Trans As SqlTransaction) As String
        Try
            Dim BaseQryForCustomer As String = Nothing
            Dim BaseQryForCustomerforOpening As String = Nothing
            Dim BaseQry As String = Nothing

            BaseQryForCustomer = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, ConvRate, strCustomer, False, strfromdate, strtodate, False, False, False, Trans)
            BaseQryForCustomerforOpening = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, ConvRate, strCustomer, True, strfromdate, strtodate, False, False, False, Trans)

            BaseQry = " Select '" + clsCommon.GetPrintDate(strtodate, "dd/MM/yyyy") + "' as DocDate, ACode,  MAX(AName) as AName, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt,  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt  From (" + Environment.NewLine & _
            "  Select max(DocDate) as DocDate, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,  '' as CurrencyCode, null as ConvRate, SUM(DrAmt*ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt from  ( " + BaseQryForCustomerforOpening + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strfromdate + "' AND LEN(ACode)>0 AND TSPL_CUSTOMER_MASTER.Status='N'  GROUP BY ACode" + Environment.NewLine & _
            Environment.NewLine + " UNION ALL" + Environment.NewLine & _
            " Select  max(DocDate) as DocDate, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,  MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt*  Final.ConvRate)) as DrAmt, " & Environment.NewLine & _
            " SUM(convert(decimal(18,2),CrAmt)) as CrAmt FROM ( " + BaseQryForCustomer + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
            " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
            "where  CONVERT(DATE,final.DocDate,103) >= '" + strfromdate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strtodate + "' AND LEN(ACode)>0 AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode ) XXX GROUP BY ACode  " + Environment.NewLine

            Return BaseQry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getCustomerOutstandingCrateWithOPeningAndClosing(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal ConvRate As String) As String
        Try
            Dim BaseQry As String = Nothing

            BaseQry = " ( Select convert(varchar,Doc_Date,103) as Doc_Date,Customer_Code,Customer_Name, OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateAdjQty ,SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CrateQtyClosing,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,SUM(CanQtyClosing ) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CanQtyClosing from ( " & Environment.NewLine & _
            " select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " & Environment.NewLine & _
            " select  max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (select  max(convert(date,'" + strfromdate + "' ,103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code , Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  ( " + Environment.NewLine + _
            " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 " + Environment.NewLine + _
            " union all  select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1  " + Environment.NewLine + _
            " union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " + Environment.NewLine + _
            " union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   " + Environment.NewLine + _
            " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + strfromdate + "' ,103)) group by Customer_Code " & Environment.NewLine & _
            " UNION All " & Environment.NewLine & _
            " select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec, " & Environment.NewLine & _
            " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " & Environment.NewLine & _
            " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
            " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " & Environment.NewLine & _
            "where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 ) " & Environment.NewLine & _
            " union all" & Environment.NewLine & _
            " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
            " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine & _
            " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " & Environment.NewLine & _
            " union all " & Environment.NewLine & _
            " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1   )" + _
            Environment.NewLine + " union all " + Environment.NewLine + _
            "select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1  " + Environment.NewLine + _
            "  ) as Closing " & Environment.NewLine & _
            " WHERE convert(date,Document_Date ,103)>= convert(date,'" + strfromdate + "' ,103) AND convert(date,Document_Date,103)<=convert(date,'" + strtodate + "' ,103) " & Environment.NewLine & _
            " ) as xx where 2=2   and xx.Customer_Code  in (" & strCustomer & ") " & Environment.NewLine & _
            " GROUP BY Customer_Code " & Environment.NewLine & _
            " ) as pp   left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " & Environment.NewLine & _
            " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2  ) YYY )"


            Return BaseQry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getCustomerOutstandingOfAmt_Can_Crate(ByVal strCustomer As String, ByVal strfromdate As String, ByVal strtodate As String, ByVal Trans As SqlTransaction) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim BaseQryForCustomer As String = Nothing
            Dim BaseQryForCustomerforOpening As String = Nothing
            Dim BaseQry As String = Nothing
            Dim ConvRate As String = "ConvRate"

            Dim BaseqryfromDate_Amount As String = Nothing
            Dim BaseqryToDate_Amount As String = Nothing

            Dim BaseqryfromDate_Crate As String = Nothing
            Dim BaseqryToDate_Crate As String = Nothing
            '' for Customer outstanding
            BaseqryfromDate_Amount = getCustomerOutstandingAmtWithOPeningAndClosing(strCustomer, strfromdate, strfromdate, ConvRate, Trans)
            BaseqryToDate_Amount = getCustomerOutstandingAmtWithOPeningAndClosing(strCustomer, strtodate, strtodate, ConvRate, Trans)
            ''for crate outstanding
            BaseqryfromDate_Crate = getCustomerOutstandingCrateWithOPeningAndClosing(strCustomer, strfromdate, strfromdate, ConvRate)
            BaseqryToDate_Crate = getCustomerOutstandingCrateWithOPeningAndClosing(strCustomer, strtodate, strtodate, ConvRate)

            BaseQry = "  Select convert(varchar,FinalGroup.DocDate,103) as Date ,FinalGroup.acode as Cust_code ,max(FinalGroup.AName) as Name,sum(FinalGroup.OpngBal) as OpngBal,sum(FinalGroup.DrAmt) as DrAmt,sum(FinalGroup.CrAmt) as CrAmt,sum(FinalGroup.BalAmt) as BalAmt,sum(FinalGroup.CrateOpng) as CrateOpng,sum(FinalGroup.CrateReceived) as CrateReceived,sum(FinalGroup.CrateIssue) as CrateIssue,sum(FinalGroup.CrateClosing) as CrateClosing,sum(FinalGroup.OpenCanQty) as OpenCanQty ,sum(FinalGroup.CanQtyRecd) as CanQtyRecd  ,sum(FinalGroup.CanOutQty) as CanOutQty ,sum(FinalGroup.CanAdjQty) as CanAdjQty ,sum(FinalGroup.CanQtyClosing) as CanQtyClosing  from  ( " & Environment.NewLine & _
            " Select  DocDate, ACode, AName, OpngBal, DrAmt, CrAmt, BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from (" & BaseqryfromDate_Amount & ") Z " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " Select  DocDate, ACode, AName, OpngBal, DrAmt, CrAmt, BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from (" & BaseqryToDate_Amount & ") X " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " Select Doc_Date,Customer_Code,Customer_Name,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,  OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateQtyClosing ,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,CanQtyClosing from (" & BaseqryfromDate_Crate & ") Y " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " Select Doc_Date,Customer_Code,Customer_Name,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,  OpencrateQty, CrateQtyRecd ,CrateOutQty, CrateQtyClosing ,OpenCanQty , CanQtyRecd  ,CanOutQty , CanAdjQty ,CanQtyClosing from (" & BaseqryToDate_Crate & ") S " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " select '" + clsCommon.GetPrintDate(strfromdate, "dd/MM/yyyy") + "' as Date,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  as Name ,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing  from TSPL_CUSTOMER_MASTER where  TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ") " & Environment.NewLine & _
            " Union All " & Environment.NewLine & _
            " select '" + clsCommon.GetPrintDate(strtodate, "dd/MM/yyyy") + "'   as Date ,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name  as Name ,0 as OpngBal, 0 as DrAmt, 0 as  CrAmt, 0 as  BalAmt,0 as CrateOpng,0 as CrateReceived,0 as CrateIssue,0 as CrateClosing,0 as OpenCanQty ,0 as CanQtyRecd  ,0 as CanOutQty ,0 as CanAdjQty ,0 as CanQtyClosing   from TSPL_CUSTOMER_MASTER where  TSPL_CUSTOMER_MASTER.Cust_Code in (" & strCustomer & ")" & Environment.NewLine & _
                " ) FinalGroup group by FinalGroup.DocDate ,FinalGroup.acode order by FinalGroup.DocDate desc"

            Return clsDBFuncationality.GetDataTable(BaseQry, Trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    ' done by priti ERO/13/06/18-000344
    '=Update by preeti Gupta Against ticket no[BHA/16/08/18-000437]
    Public Shared Sub CreateJournalEntry(ByVal TransType As String, ByVal strCode As String, ByVal TotalCrateQty As Double, ByVal TotalCanQty As Double, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing)
        Try
            Dim obj As New clsCrateReceivedHead
            obj = clsCrateReceivedHead.GetData(strCode, NavigatorType.Current, trans)
            Dim ArryLst As ArrayList = New ArrayList()
            Dim strRemarks As String = ""
            Dim strReturnable_ContainerAC As String = ""
            Dim Acc() As String = Nothing
            Dim RecoControlACC As String = ""

            'do not delete, Comment for future change-  RecoControlACC and InventorymovementDr/Cr
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            '    RecoControlACC = "I"
            'End If
            'do not delete, Comment for future change-  RecoControlACC and InventorymovementDr/Cr

            If clsCommon.CompairString(TransType, "Can") = CompairStringResult.Equal Then
                strReturnable_ContainerAC = clsCommon.myCstr(clsItemMaster.GetReturnableConGLAC(obj.Crate_Item, trans))
                If clsCommon.myLen(strReturnable_ContainerAC) = 0 Then
                    Throw New Exception("Please set Returnable Container Account for item")
                End If
                strReturnable_ContainerAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strReturnable_ContainerAC, obj.Location_Code, False, trans)
                Acc = {strReturnable_ContainerAC, 1 * (TotalCanQty * obj.Crate_ItemRate)}
                ArryLst.Add(Acc)
            Else
                If TotalCrateQty > 0 Then
                    strReturnable_ContainerAC = clsCommon.myCstr(clsItemMaster.GetReturnableConGLAC(obj.Crate_Item, trans))
                    If clsCommon.myLen(strReturnable_ContainerAC) = 0 Then
                        Throw New Exception("Please set Returnable Container Account for item")
                    End If
                    strReturnable_ContainerAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strReturnable_ContainerAC, obj.Location_Code, False, trans)

                    If clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                        Acc = {strReturnable_ContainerAC, 1 * (TotalCrateQty * obj.Crate_ItemRate), "", "", "", "", "", "", RecoControlACC}
                        ArryLst.Add(Acc)
                    Else
                        Acc = {strReturnable_ContainerAC, -1 * (TotalCrateQty * obj.Crate_ItemRate), "", "", "", "", "", "", RecoControlACC}
                        ArryLst.Add(Acc)
                    End If
                    If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                        clsInventoryMovement.UpdateInvControlAccount(obj.Document_No, "CRATE-REC", clsCommon.myCstr(obj.Crate_Item), strReturnable_ContainerAC, "", "", trans)
                    End If
                End If
                If TotalCanQty > 0 Then
                    strReturnable_ContainerAC = clsCommon.myCstr(clsItemMaster.GetReturnableConGLAC(obj.Can_Item, trans))
                    If clsCommon.myLen(strReturnable_ContainerAC) = 0 Then
                        Throw New Exception("Please set Returnable Container Account for item")
                    End If
                    strReturnable_ContainerAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strReturnable_ContainerAC, obj.Location_Code, False, trans)

                    If clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                        Acc = {strReturnable_ContainerAC, 1 * (TotalCanQty * obj.Can_ItemRate), "", "", "", "", "", "", RecoControlACC}
                        ArryLst.Add(Acc)
                    Else
                        Acc = {strReturnable_ContainerAC, -1 * (TotalCanQty * obj.Can_ItemRate), "", "", "", "", "", "", RecoControlACC}
                        ArryLst.Add(Acc)
                    End If
                    If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                        clsInventoryMovement.UpdateInvControlAccount(obj.Document_No, "CRATE-REC", clsCommon.myCstr(obj.Can_Item), strReturnable_ContainerAC, "", "", trans)
                    End If
                End If
              
            End If

            For Each objTr As clsCrateReceivedDetail In obj.Arr
                Dim strContainerDepositAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit ,'') as Container_Deposit from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Account  =TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code ='" & objTr.Customer_Code & "'", trans))
                If clsCommon.myLen(strContainerDepositAC) = 0 Then
                    Throw New Exception("Please set Container Deposit Account for customer")
                End If
                strContainerDepositAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strContainerDepositAC, obj.Location_Code, False, trans)
                Dim Acc1() As String = Nothing
                Dim dblamount As Double = 0
                If clsCommon.CompairString(TransType, "Can") = CompairStringResult.Equal Then
                    dblamount = TotalCanQty * obj.Crate_ItemRate
                ElseIf clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                    dblamount = ((objTr.CANRecQty + objTr.CANAdjustment) * obj.Can_ItemRate) + ((objTr.CrateQtyRecd + objTr.Adjustment) * obj.Crate_ItemRate)
                ElseIf clsCommon.CompairString(obj.Type, "O") = CompairStringResult.Equal Then
                    dblamount = ((objTr.CANOutQty) * obj.Can_ItemRate) + ((objTr.OutQty) * obj.Crate_ItemRate)
                Else
                    dblamount = ((objTr.CANRecQty + objTr.CANAdjustment) * obj.Can_ItemRate) + ((objTr.CrateQtyRecd + objTr.Adjustment) * obj.Crate_ItemRate)
                End If
                If clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                    Acc1 = {strContainerDepositAC, -1 * dblamount}
                    ArryLst.Add(Acc1)
                Else
                    Acc1 = {strContainerDepositAC, 1 * dblamount}
                    ArryLst.Add(Acc1)
                End If

            Next
            If clsCommon.CompairString(obj.Trans_Type, "Can") = CompairStringResult.Equal Then
                strRemarks = " Journal Entry Created  For Can Receipt No " & obj.Document_No & " "
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, strRemarks, "CN-RC", "CanReceipt", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , "", strRemarks)
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, strRemarks, "CN-RC", "CanReceipt", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , "", strRemarks)
                End If
            Else
                strRemarks = " Journal Entry Created  For Crate Receipt No " & obj.Document_No & " "
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, strRemarks, "CR-RC", "CrateReceipt", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , "", strRemarks)
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, strRemarks, "CR-RC", "CrateReceipt", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , "", strRemarks)
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '=============update by preeti gupta Against ticket no[UDL/21/01/19-000259]
    Public Shared Function ReverseAndRecrate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If
            Dim qry As String = String.Empty

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_code='CR-RC' and source_doc_no='" & clsCommon.myCstr(strCode) & "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            qry = "Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No ='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Update TSPL_CRATE_RECEIVED_HEAD_FRESHSALE set Posted = 0 where Document_No='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsCrateReceivedDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Sale_Invoice_Date As String = Nothing
    Public Customer_Code As String = Nothing
    Public Sale_Invoice_No As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public VehicleNo As String = Nothing
    Public CrateQtyPreviousDay As Double = 0
    Public CrateQtyRecd As Double = 0
    Public CrateQty As Double = 0
    Public Balance As Double = 0

    Public OutQty As Double = 0
    Public Adjustment As Double = 0
    Public Remarks As String = Nothing
    '===============Added by preeti gupta==============
    Public Jaali As Double = 0
    Public Box As Double = 0

    Public CrateQtyManual As Double = 0
    Public JaaliQtyRecd As Double = 0
    Public BoxQtyRecd As Double = 0
    Public jaaliOutQty As Double = 0
    Public boxOutQty As Double = 0
    Public jaaliAdjustment As Double = 0
    Public boxAdjustment As Double = 0
    Public LinerQty As Double = 0

    Public CANQty As Double = 0
    Public CANRecQty As Double = 0
    Public CANOutQty As Double = 0
    Public CANAdjustment As Double = 0
    Public DamageCrateQtyRecd As Double = 0
    Public Route_Code As String = String.Empty
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCrateReceivedDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim i As Integer = 1
            For Each obj As clsCrateReceivedDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", i)
                clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                'clsCommon.AddColumnsForChange(coll, "Sale_Invoice_No", obj.Sale_Invoice_No, True)
                clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Sale_Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
                'clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
                'clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
                clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
                clsCommon.AddColumnsForChange(coll, "CrateQtyRecd", obj.CrateQtyRecd)
                clsCommon.AddColumnsForChange(coll, "CrateQtyPreviousDay", obj.CrateQtyPreviousDay)
                clsCommon.AddColumnsForChange(coll, "Balance", obj.Balance)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "OutQty", obj.OutQty)
                clsCommon.AddColumnsForChange(coll, "Adjustment", obj.Adjustment)
                clsCommon.AddColumnsForChange(coll, "jaali", obj.Jaali)
                clsCommon.AddColumnsForChange(coll, "box", obj.Box)

                clsCommon.AddColumnsForChange(coll, "CrateQtyManual", obj.CrateQtyManual)
                clsCommon.AddColumnsForChange(coll, "JaaliQtyRecd", obj.JaaliQtyRecd)
                clsCommon.AddColumnsForChange(coll, "BoxQtyRecd", obj.BoxQtyRecd)
                clsCommon.AddColumnsForChange(coll, "jaaliOutQty", obj.jaaliOutQty)
                clsCommon.AddColumnsForChange(coll, "boxOutQty", obj.boxOutQty)
                clsCommon.AddColumnsForChange(coll, "jaaliAdjustment", obj.jaaliAdjustment)
                clsCommon.AddColumnsForChange(coll, "boxAdjustment", obj.boxAdjustment)
                clsCommon.AddColumnsForChange(coll, "LinerQty", obj.LinerQty)

                clsCommon.AddColumnsForChange(coll, "CANQty", obj.CANQty)
                clsCommon.AddColumnsForChange(coll, "CANQtyRec", obj.CANRecQty)
                clsCommon.AddColumnsForChange(coll, "CANOutQty", obj.CANOutQty)
                clsCommon.AddColumnsForChange(coll, "CANAdjustment", obj.CANAdjustment)
                clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                clsCommon.AddColumnsForChange(coll, "DamageCrateQtyRecd", obj.DamageCrateQtyRecd)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)
                i = i + 1
            Next

        End If
        Return True
    End Function

End Class
