Imports common
Imports System.Data.SqlClient

Public Class clsPurchaseSchedule

#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Description As String = Nothing
    Public Vendor_Code As String = Nothing
    Public PO_Type As String = Nothing
    Public Schedule_Month As Date = Nothing
    Public Schedule_Type As String = Nothing
    Public PO_Code As String = Nothing
    Public Is_Post As Integer = Nothing
    Public Vendor_Name As String = Nothing
    Public PO_Desc As String = Nothing
    Public Revision_No As String = Nothing

    Public Arr As List(Of clsPurchaseScheduleDetail) = Nothing
    Public Arr_Vendor As List(Of clsPurchaseScheduleVendorDetail) = Nothing
#End Region


    '=========NOTE======if change in table then do in history table also

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_PO_SCH_HEAD.Document_Code as Code,TSPL_PO_SCH_HEAD.document_date as [Date],TSPL_PO_SCH_HEAD.Description,tspl_vendor_master.vendor_name as [Vendor],(case when Schedule_Type='D' then 'Daily' else case when Schedule_Type='W' then 'Weekly' else case when Schedule_Type='M' then 'Monthly' end end end) as [Schedule Type],(case when Schedule_Type='M' then cast(year(TSPL_PO_SCH_HEAD.schedule_month) as varchar) else cast(datename(month,TSPL_PO_SCH_HEAD.schedule_month) as varchar)+' '+cast(year(TSPL_PO_SCH_HEAD.schedule_month) as varchar) end) as [Schedule Period],TSPL_PO_SCH_HEAD.Revision_No"
        qry += " from TSPL_PO_SCH_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PO_SCH_HEAD.vendor_code "
        str = clsCommon.myCstr(clsCommon.ShowSelectForm("PURSCHFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked))

        Return str
    End Function

    Public Shared Function GetBillToLocation(ByVal POCode As String) As String
        Dim str As String = ""
        Dim qry As String = "select bill_to_location from tspl_purchase_order_head where purchaseorder_no='" + POCode + "'"
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        Return str
    End Function

    Public Shared Function IsValidVendorForSchedule(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select document_code as RGP_No,TSPL_PO_SCH_HEAD.Vendor_Code,Vendor_Name from TSPL_PO_SCH_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PO_SCH_HEAD.vendor_code where document_code in (" + clsCommon.GetMulcallString(Arr) + ") and TSPL_PO_SCH_HEAD.Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "Schedule No:" + clsCommon.myCstr(dr("RGP_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetNoOfWeekInMonth(ByVal dtpMonth As Date, Optional ByVal GetWeekUptoSelectedDateDay As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim days As Integer = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth, "MM"))))
        If GetWeekUptoSelectedDateDay Then
            days = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth, "dd")))
        End If
        Dim MonthName As String = clsCommon.GetPrintDate(dtpMonth, "MMM")
        Dim Year As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth, "yyyy")))
        Dim wk As Integer = 0
        Dim firstwkdays As Integer = 0
        Dim xdate As String = "01/" + clsCommon.myCstr(MonthName) + "/" + clsCommon.myCstr(Year)
        Dim qry As String = "select datename(weekday,'" + xdate + "')"
        Dim wkName As String = clsDBFuncationality.getSingleValue(qry, trans)

        If clsCommon.CompairString(wkName, "Sunday") = CompairStringResult.Equal Then
            firstwkdays = 7
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Monday") = CompairStringResult.Equal Then
            firstwkdays = 6
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Tuesday") = CompairStringResult.Equal Then
            firstwkdays = 5
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Wednesday") = CompairStringResult.Equal Then
            firstwkdays = 4
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Thursday") = CompairStringResult.Equal Then
            firstwkdays = 3
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Friday") = CompairStringResult.Equal Then
            firstwkdays = 2
            wk += 1
        ElseIf clsCommon.CompairString(wkName, "Saturday") = CompairStringResult.Equal Then
            firstwkdays = 1
            wk += 1
        End If

        Dim leftdays As Integer = days - firstwkdays
        If leftdays > 0 Then
            Dim modevalue As Double = 0
            modevalue = leftdays Mod 7
            If modevalue < 7 AndAlso modevalue > 0 Then
                wk += 1
            End If
            leftdays = leftdays - modevalue
            leftdays = leftdays / 7
            wk += leftdays
        End If


        Return CInt(wk)
    End Function

    Public Shared Function SaveData(ByVal obj As clsPurchaseSchedule, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)
            trans.Commit()

            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsPurchaseSchedule, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"), clsDocType.POSCHEDULE, "", "")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "PO_Type", obj.PO_Type)
            clsCommon.AddColumnsForChange(coll, "Schedule_Month", clsCommon.GetPrintDate(obj.Schedule_Month, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Schedule_Type", obj.Schedule_Type)
            clsCommon.AddColumnsForChange(coll, "PO_Code", obj.PO_Code)
            clsCommon.AddColumnsForChange(coll, "Is_Post", "0")
            clsCommon.AddColumnsForChange(coll, "Revision_No", obj.Revision_No)
            clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_SCH_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_SCH_HEAD", OMInsertOrUpdate.Update, " Document_Code='" + obj.Document_Code + "'", trans)
            End If

            isSaved = isSaved AndAlso clsPurchaseScheduleDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            isSaved = isSaved AndAlso clsPurchaseScheduleVendorDetail.SaveData(obj.Document_Code, obj.Arr_Vendor, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPurchaseSchedule
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As New clsPurchaseSchedule()
            obj = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPurchaseSchedule
        Try
            Dim obj As New clsPurchaseSchedule()
            obj.Arr = New List(Of clsPurchaseScheduleDetail)
            obj.Arr_Vendor = New List(Of clsPurchaseScheduleVendorDetail)

            Dim qry As String = "select TSPL_PO_SCH_HEAD.*,tspl_purchase_order_head.description as po_desc,tspl_vendor_master.vendor_name from TSPL_PO_SCH_HEAD"
            qry += " left outer join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=TSPL_PO_SCH_HEAD.po_code left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PO_SCH_HEAD.vendor_code where 1=1 "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_PO_SCH_HEAD.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_PO_SCH_HEAD.document_code in (select min(document_code) from TSPL_PO_SCH_HEAD)"
                Case NavigatorType.Last
                    qry += " and TSPL_PO_SCH_HEAD.document_code in (select max(document_code) from TSPL_PO_SCH_HEAD)"
                Case NavigatorType.Next
                    qry += " and TSPL_PO_SCH_HEAD.document_code in (select min(document_code) from TSPL_PO_SCH_HEAD where document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_PO_SCH_HEAD.document_code in (select max(document_code) from TSPL_PO_SCH_HEAD where document_code<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                obj.PO_Type = clsCommon.myCstr(dt.Rows(0)("PO_Type"))
                obj.Schedule_Month = clsCommon.myCDate(dt.Rows(0)("Schedule_Month"))
                obj.Schedule_Type = clsCommon.myCstr(dt.Rows(0)("Schedule_Type"))
                obj.PO_Code = clsCommon.myCstr(dt.Rows(0)("PO_Code"))
                obj.PO_Desc = clsCommon.myCstr(dt.Rows(0)("PO_Desc"))
                obj.Is_Post = CInt(clsCommon.myCdbl(dt.Rows(0)("is_post")))
                obj.Revision_No = clsCommon.myCstr(dt.Rows(0)("Revision_No"))

                qry = "select * from TSPL_PO_SCH_DETAIL where document_code='" + obj.Document_Code + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsPurchaseScheduleDetail()

                        objtr.Document_Code = clsCommon.myCstr(dr("document_code"))
                        objtr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Item_Name = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                        objtr.Item_Type = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                        objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.PO_Code = clsCommon.myCstr(dr("PO_Code"))
                        If IsDBNull(dr("po_date")) = True Then
                            objtr.PO_Date = Nothing
                        Else
                            objtr.PO_Date = clsCommon.myCDate(dr("PO_Date"))
                        End If

                        objtr.Pers_Value = clsCommon.myCdbl(dr("Pers_Value"))
                        objtr.Pers_Type = clsCommon.myCstr(dr("Pers_Type"))
                        objtr.PO_Qty = clsCommon.myCdbl(dr("PO_Qty"))
                        objtr.Schedule_Qty = clsCommon.myCdbl(dr("Schedule_Qty"))
                        objtr.Week1_Qty = clsCommon.myCdbl(dr("Week1_Qty"))
                        objtr.Week2_Qty = clsCommon.myCdbl(dr("Week2_Qty"))
                        objtr.Week3_Qty = clsCommon.myCdbl(dr("Week3_Qty"))
                        objtr.Week4_Qty = clsCommon.myCdbl(dr("Week4_Qty"))
                        objtr.Week5_Qty = clsCommon.myCdbl(dr("Week5_Qty"))
                        objtr.Week6_Qty = clsCommon.myCdbl(dr("Week6_Qty"))
                        objtr.Day1_Qty = clsCommon.myCdbl(dr("Day1_Qty"))
                        objtr.Day2_Qty = clsCommon.myCdbl(dr("Day2_Qty"))
                        objtr.Day3_Qty = clsCommon.myCdbl(dr("Day3_Qty"))
                        objtr.Day4_Qty = clsCommon.myCdbl(dr("Day4_Qty"))
                        objtr.Day5_Qty = clsCommon.myCdbl(dr("Day5_Qty"))
                        objtr.Day6_Qty = clsCommon.myCdbl(dr("Day6_Qty"))
                        objtr.Day7_Qty = clsCommon.myCdbl(dr("Day7_Qty"))
                        objtr.Day8_Qty = clsCommon.myCdbl(dr("Day8_Qty"))
                        objtr.Day9_Qty = clsCommon.myCdbl(dr("Day9_Qty"))
                        objtr.Day10_Qty = clsCommon.myCdbl(dr("Day10_Qty"))
                        objtr.Day11_Qty = clsCommon.myCdbl(dr("Day11_Qty"))
                        objtr.Day12_Qty = clsCommon.myCdbl(dr("Day12_Qty"))
                        objtr.Day13_Qty = clsCommon.myCdbl(dr("Day13_Qty"))
                        objtr.Day14_Qty = clsCommon.myCdbl(dr("Day14_Qty"))
                        objtr.Day15_Qty = clsCommon.myCdbl(dr("Day15_Qty"))
                        objtr.Day16_Qty = clsCommon.myCdbl(dr("Day16_Qty"))
                        objtr.Day17_Qty = clsCommon.myCdbl(dr("Day17_Qty"))
                        objtr.Day18_Qty = clsCommon.myCdbl(dr("Day18_Qty"))
                        objtr.Day19_Qty = clsCommon.myCdbl(dr("Day19_Qty"))
                        objtr.Day20_Qty = clsCommon.myCdbl(dr("Day20_Qty"))
                        objtr.Day21_Qty = clsCommon.myCdbl(dr("Day21_Qty"))
                        objtr.Day22_Qty = clsCommon.myCdbl(dr("Day22_Qty"))
                        objtr.Day23_Qty = clsCommon.myCdbl(dr("Day23_Qty"))
                        objtr.Day24_Qty = clsCommon.myCdbl(dr("Day24_Qty"))
                        objtr.Day25_Qty = clsCommon.myCdbl(dr("Day25_Qty"))
                        objtr.Day26_Qty = clsCommon.myCdbl(dr("Day26_Qty"))
                        objtr.Day27_Qty = clsCommon.myCdbl(dr("Day27_Qty"))
                        objtr.Day28_Qty = clsCommon.myCdbl(dr("Day28_Qty"))
                        objtr.Day29_Qty = clsCommon.myCdbl(dr("Day29_Qty"))
                        objtr.Day30_Qty = clsCommon.myCdbl(dr("Day30_Qty"))
                        objtr.Day31_Qty = clsCommon.myCdbl(dr("Day31_Qty"))
                        objtr.Month1_Qty = clsCommon.myCdbl(dr("Month1_Qty"))
                        objtr.Month2_Qty = clsCommon.myCdbl(dr("Month2_Qty"))
                        objtr.Month3_Qty = clsCommon.myCdbl(dr("Month3_Qty"))
                        objtr.Month4_Qty = clsCommon.myCdbl(dr("Month4_Qty"))
                        objtr.Month5_Qty = clsCommon.myCdbl(dr("Month5_Qty"))
                        objtr.Month6_Qty = clsCommon.myCdbl(dr("Month6_Qty"))
                        objtr.Month7_Qty = clsCommon.myCdbl(dr("Month7_Qty"))
                        objtr.Month8_Qty = clsCommon.myCdbl(dr("Month8_Qty"))
                        objtr.Month9_Qty = clsCommon.myCdbl(dr("Month9_Qty"))
                        objtr.Month10_Qty = clsCommon.myCdbl(dr("Month10_Qty"))
                        objtr.Month11_Qty = clsCommon.myCdbl(dr("Month11_Qty"))
                        objtr.Month12_Qty = clsCommon.myCdbl(dr("Month12_Qty"))
                        objtr.Remarks = clsCommon.myCstr(dr("Remarks"))

                        obj.Arr.Add(objtr)
                    Next
                End If

                qry = "select * from TSPL_PO_VENDOR_SCH_DETAIL where document_code='" + obj.Document_Code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsPurchaseScheduleVendorDetail()

                        objtr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Item_Name = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                        objtr.Item_Type = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                        objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.PO_Code = clsCommon.myCstr(dr("PO_Code"))
                        If IsDBNull(dr("po_date")) = True Then
                            objtr.PO_Date = Nothing
                        Else
                            objtr.PO_Date = clsCommon.myCDate(dr("PO_Date"))
                        End If

                        objtr.PO_Qty = clsCommon.myCdbl(dr("PO_Qty"))
                        objtr.Schedule_Qty = clsCommon.myCdbl(dr("Schedule_Qty"))
                        objtr.Week1_Qty = clsCommon.myCdbl(dr("Week1_Qty"))
                        objtr.Week2_Qty = clsCommon.myCdbl(dr("Week2_Qty"))
                        objtr.Week3_Qty = clsCommon.myCdbl(dr("Week3_Qty"))
                        objtr.Week4_Qty = clsCommon.myCdbl(dr("Week4_Qty"))
                        objtr.Week5_Qty = clsCommon.myCdbl(dr("Week5_Qty"))
                        objtr.Week6_Qty = clsCommon.myCdbl(dr("Week6_Qty"))
                        objtr.Day1_Qty = clsCommon.myCdbl(dr("Day1_Qty"))
                        objtr.Day2_Qty = clsCommon.myCdbl(dr("Day2_Qty"))
                        objtr.Day3_Qty = clsCommon.myCdbl(dr("Day3_Qty"))
                        objtr.Day4_Qty = clsCommon.myCdbl(dr("Day4_Qty"))
                        objtr.Day5_Qty = clsCommon.myCdbl(dr("Day5_Qty"))
                        objtr.Day6_Qty = clsCommon.myCdbl(dr("Day6_Qty"))
                        objtr.Day7_Qty = clsCommon.myCdbl(dr("Day7_Qty"))
                        objtr.Day8_Qty = clsCommon.myCdbl(dr("Day8_Qty"))
                        objtr.Day9_Qty = clsCommon.myCdbl(dr("Day9_Qty"))
                        objtr.Day10_Qty = clsCommon.myCdbl(dr("Day10_Qty"))
                        objtr.Day11_Qty = clsCommon.myCdbl(dr("Day11_Qty"))
                        objtr.Day12_Qty = clsCommon.myCdbl(dr("Day12_Qty"))
                        objtr.Day13_Qty = clsCommon.myCdbl(dr("Day13_Qty"))
                        objtr.Day14_Qty = clsCommon.myCdbl(dr("Day14_Qty"))
                        objtr.Day15_Qty = clsCommon.myCdbl(dr("Day15_Qty"))
                        objtr.Day16_Qty = clsCommon.myCdbl(dr("Day16_Qty"))
                        objtr.Day17_Qty = clsCommon.myCdbl(dr("Day17_Qty"))
                        objtr.Day18_Qty = clsCommon.myCdbl(dr("Day18_Qty"))
                        objtr.Day19_Qty = clsCommon.myCdbl(dr("Day19_Qty"))
                        objtr.Day20_Qty = clsCommon.myCdbl(dr("Day20_Qty"))
                        objtr.Day21_Qty = clsCommon.myCdbl(dr("Day21_Qty"))
                        objtr.Day22_Qty = clsCommon.myCdbl(dr("Day22_Qty"))
                        objtr.Day23_Qty = clsCommon.myCdbl(dr("Day23_Qty"))
                        objtr.Day24_Qty = clsCommon.myCdbl(dr("Day24_Qty"))
                        objtr.Day25_Qty = clsCommon.myCdbl(dr("Day25_Qty"))
                        objtr.Day26_Qty = clsCommon.myCdbl(dr("Day26_Qty"))
                        objtr.Day27_Qty = clsCommon.myCdbl(dr("Day27_Qty"))
                        objtr.Day28_Qty = clsCommon.myCdbl(dr("Day28_Qty"))
                        objtr.Day29_Qty = clsCommon.myCdbl(dr("Day29_Qty"))
                        objtr.Day30_Qty = clsCommon.myCdbl(dr("Day30_Qty"))
                        objtr.Day31_Qty = clsCommon.myCdbl(dr("Day31_Qty"))
                        objtr.Month1_Qty = clsCommon.myCdbl(dr("Month1_Qty"))
                        objtr.Month2_Qty = clsCommon.myCdbl(dr("Month2_Qty"))
                        objtr.Month3_Qty = clsCommon.myCdbl(dr("Month3_Qty"))
                        objtr.Month4_Qty = clsCommon.myCdbl(dr("Month4_Qty"))
                        objtr.Month5_Qty = clsCommon.myCdbl(dr("Month5_Qty"))
                        objtr.Month6_Qty = clsCommon.myCdbl(dr("Month6_Qty"))
                        objtr.Month7_Qty = clsCommon.myCdbl(dr("Month7_Qty"))
                        objtr.Month8_Qty = clsCommon.myCdbl(dr("Month8_Qty"))
                        objtr.Month9_Qty = clsCommon.myCdbl(dr("Month9_Qty"))
                        objtr.Month10_Qty = clsCommon.myCdbl(dr("Month10_Qty"))
                        objtr.Month11_Qty = clsCommon.myCdbl(dr("Month11_Qty"))
                        objtr.Month12_Qty = clsCommon.myCdbl(dr("Month12_Qty"))
                        objtr.Remarks = clsCommon.myCstr(dr("Remarks"))

                        obj.Arr_Vendor.Add(objtr)
                    Next
                End If '=========

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(strCode, trans)
            trans.Commit()

            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If clsCommon.myLen(strCode) <= 0 Then
                isSaved = False
                Throw New Exception("No document found.")
            End If

            Dim qry As String = "update TSPL_PO_SCH_HEAD set is_post=1,modified_by='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',modified_date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnPostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso UnPostData(strCode, trans)
            trans.Commit()

            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnPostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = ""

            If clsCommon.myLen(strCode) <= 0 Then
                isSaved = False
                Throw New Exception("No document found.")
            End If

            qry = "select count(*) from TSPL_PO_SCH_HEAD where is_post=1 and document_code='" + strCode + "'"
            Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

            If count <= 0 Then
                Throw New Exception("Document is not posted.")
            End If

            'qry = "select count(*) from tspl_grn_detail where against_schedule_code='" + strCode + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            'If check > 0 Then
            '    Throw New Exception("Cannot unpost the current document.It is used in Gate Receipt Note.")
            'End If

            'qry = "select count(*) from tspl_srn_detail where against_schedule_code='" + strCode + "'"
            'check = clsDBFuncationality.getSingleValue(qry, trans)
            'If check > 0 Then
            '    Throw New Exception("Cannot unpost the current document.It is used in Store Receipt Note.")
            'End If

            'qry = "select count(*) from tspl_rgp_detail where against_schedule_code='" + strCode + "'"
            'check = clsDBFuncationality.getSingleValue(qry, trans)
            'If check > 0 Then
            '    Throw New Exception("Cannot unpost the current document.It is used in RGP.")
            'End If
            qry = "select max(Revision_No) from TSPL_PO_SCH_HEAD where document_code='" + strCode + "'"
            Dim rev_no As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(rev_no) > 0 Then
                rev_no = clsCommon.incval(rev_no)
            Else
                rev_no = clsCommon.myCstr(strCode) + ".1"
            End If

            qry = "insert into TSPL_PO_VENDOR_SCH_DETAIL_HISTORY (Document_Code,Line_No,Item_Code,Unit_Code,PO_Code,PO_Date,Schedule_Qty,Week1_Qty,Week2_Qty,Week3_Qty,Week4_Qty,Week5_Qty,Week6_Qty,Day1_Qty,Day2_Qty,Day3_Qty,Day4_Qty,Day5_Qty,Day6_Qty,Day7_Qty,Day8_Qty,Day9_Qty,Day10_Qty,Day11_Qty,Day12_Qty,Day13_Qty,Day14_Qty,Day15_Qty,Day16_Qty,Day17_Qty,Day18_Qty,Day19_Qty,Day20_Qty,Day21_Qty,Day22_Qty,Day23_Qty,Day24_Qty,Day25_Qty,Day26_Qty,Day27_Qty,Day28_Qty,Day29_Qty,Day30_Qty,Day31_Qty,Month1_Qty,Month2_Qty,Month3_Qty,Month4_Qty,Month5_Qty,Month6_Qty,Month7_Qty,Month8_Qty,Month9_Qty,Month10_Qty,Month11_Qty,Month12_Qty,Remarks,PO_Qty) select Document_Code,Line_No,Item_Code,Unit_Code,PO_Code,PO_Date,Schedule_Qty,Week1_Qty,Week2_Qty,Week3_Qty,Week4_Qty,Week5_Qty,Week6_Qty,Day1_Qty,Day2_Qty,Day3_Qty,Day4_Qty,Day5_Qty,Day6_Qty,Day7_Qty,Day8_Qty,Day9_Qty,Day10_Qty,Day11_Qty,Day12_Qty,Day13_Qty,Day14_Qty,Day15_Qty,Day16_Qty,Day17_Qty,Day18_Qty,Day19_Qty,Day20_Qty,Day21_Qty,Day22_Qty,Day23_Qty,Day24_Qty,Day25_Qty,Day26_Qty,Day27_Qty,Day28_Qty,Day29_Qty,Day30_Qty,Day31_Qty,Month1_Qty,Month2_Qty,Month3_Qty,Month4_Qty,Month5_Qty,Month6_Qty,Month7_Qty,Month8_Qty,Month9_Qty,Month10_Qty,Month11_Qty,Month12_Qty,Remarks,PO_Qty from TSPL_PO_VENDOR_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "insert into TSPL_PO_SCH_DETAIL_HISTORY (Document_Code,Line_No,Item_Code,Unit_Code,PO_Code,PO_Date,Schedule_Qty,Week1_Qty,Week2_Qty,Week3_Qty,Week4_Qty,Week5_Qty,Week6_Qty,Day1_Qty,Day2_Qty,Day3_Qty,Day4_Qty,Day5_Qty,Day6_Qty,Day7_Qty,Day8_Qty,Day9_Qty,Day10_Qty,Day11_Qty,Day12_Qty,Day13_Qty,Day14_Qty,Day15_Qty,Day16_Qty,Day17_Qty,Day18_Qty,Day19_Qty,Day20_Qty,Day21_Qty,Day22_Qty,Day23_Qty,Day24_Qty,Day25_Qty,Day26_Qty,Day27_Qty,Day28_Qty,Day29_Qty,Day30_Qty,Day31_Qty,Month1_Qty,Month2_Qty,Month3_Qty,Month4_Qty,Month5_Qty,Month6_Qty,Month7_Qty,Month8_Qty,Month9_Qty,Month10_Qty,Month11_Qty,Month12_Qty,Remarks,PO_Qty,Pers_Type,Pers_Value) select Document_Code,Line_No,Item_Code,Unit_Code,PO_Code,PO_Date,Schedule_Qty,Week1_Qty,Week2_Qty,Week3_Qty,Week4_Qty,Week5_Qty,Week6_Qty,Day1_Qty,Day2_Qty,Day3_Qty,Day4_Qty,Day5_Qty,Day6_Qty,Day7_Qty,Day8_Qty,Day9_Qty,Day10_Qty,Day11_Qty,Day12_Qty,Day13_Qty,Day14_Qty,Day15_Qty,Day16_Qty,Day17_Qty,Day18_Qty,Day19_Qty,Day20_Qty,Day21_Qty,Day22_Qty,Day23_Qty,Day24_Qty,Day25_Qty,Day26_Qty,Day27_Qty,Day28_Qty,Day29_Qty,Day30_Qty,Day31_Qty,Month1_Qty,Month2_Qty,Month3_Qty,Month4_Qty,Month5_Qty,Month6_Qty,Month7_Qty,Month8_Qty,Month9_Qty,Month10_Qty,Month11_Qty,Month12_Qty,Remarks,PO_Qty,Pers_Type,Pers_Value from TSPL_PO_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "insert into TSPL_PO_SCH_HEAD_HISTORY (Comp_Code,Document_Code,Document_Date,Description,Vendor_Code,PO_Type,Schedule_Month,Schedule_Type,PO_Code,Is_Post,Created_By,Created_Date,Modified_By,Modified_Date,Revision_No) select Comp_Code,Document_Code,Document_Date,Description,Vendor_Code,PO_Type,Schedule_Month,Schedule_Type,PO_Code,Is_Post,Created_By,Created_Date,Modified_By,Modified_Date,Revision_No from TSPL_PO_SCH_HEAD where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PO_SCH_HEAD set is_post=0,Revision_No='" + rev_no + "',modified_by='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',modified_date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PO_VENDOR_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_SCH_HEAD where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPurchaseScheduleDetail
#Region "Variables"
    Public balance_qty As Double = Nothing
    Public Document_Code As String = Nothing
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_Type As String = Nothing
    Public Unit_Code As String = Nothing
    Public PO_Code As String = Nothing
    Public PO_Date As Date = Nothing
    Public PO_Qty As Double = Nothing
    Public Schedule_Qty As Double = Nothing
    Public Week1_Qty As Double = Nothing
    Public Week2_Qty As Double = Nothing
    Public Week3_Qty As Double = Nothing
    Public Week4_Qty As Double = Nothing
    Public Week5_Qty As Double = Nothing
    Public Week6_Qty As Double = Nothing
    Public Day1_Qty As Double = Nothing
    Public Day2_Qty As Double = Nothing
    Public Day3_Qty As Double = Nothing
    Public Day4_Qty As Double = Nothing
    Public Day5_Qty As Double = Nothing
    Public Day6_Qty As Double = Nothing
    Public Day7_Qty As Double = Nothing
    Public Day8_Qty As Double = Nothing
    Public Day9_Qty As Double = Nothing
    Public Day10_Qty As Double = Nothing
    Public Day11_Qty As Double = Nothing
    Public Day12_Qty As Double = Nothing
    Public Day13_Qty As Double = Nothing
    Public Day14_Qty As Double = Nothing
    Public Day15_Qty As Double = Nothing
    Public Day16_Qty As Double = Nothing
    Public Day17_Qty As Double = Nothing
    Public Day18_Qty As Double = Nothing
    Public Day19_Qty As Double = Nothing
    Public Day20_Qty As Double = Nothing
    Public Day21_Qty As Double = Nothing
    Public Day22_Qty As Double = Nothing
    Public Day23_Qty As Double = Nothing
    Public Day24_Qty As Double = Nothing
    Public Day25_Qty As Double = Nothing
    Public Day26_Qty As Double = Nothing
    Public Day27_Qty As Double = Nothing
    Public Day28_Qty As Double = Nothing
    Public Day29_Qty As Double = Nothing
    Public Day30_Qty As Double = Nothing
    Public Day31_Qty As Double = Nothing
    Public Month1_Qty As Double = Nothing
    Public Month2_Qty As Double = Nothing
    Public Month3_Qty As Double = Nothing
    Public Month4_Qty As Double = Nothing
    Public Month5_Qty As Double = Nothing
    Public Month6_Qty As Double = Nothing
    Public Month7_Qty As Double = Nothing
    Public Month8_Qty As Double = Nothing
    Public Month9_Qty As Double = Nothing
    Public Month10_Qty As Double = Nothing
    Public Month11_Qty As Double = Nothing
    Public Month12_Qty As Double = Nothing
    Public Remarks As String = Nothing
    Public Pers_Type As String = Nothing
    Public Pers_Value As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsPurchaseScheduleDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PO_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseScheduleDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "PO_Code", objtr.PO_Code, True)
                    If clsCommon.myLen(objtr.PO_Date) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "PO_Date", clsCommon.GetPrintDate(objtr.PO_Date, "dd/MMM/yyyy"))
                    End If

                    clsCommon.AddColumnsForChange(coll, "PO_Qty", objtr.PO_Qty)
                    clsCommon.AddColumnsForChange(coll, "Schedule_Qty", objtr.Schedule_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week1_Qty", objtr.Week1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week2_Qty", objtr.Week2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week3_Qty", objtr.Week3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week4_Qty", objtr.Week4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week5_Qty", objtr.Week5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week6_Qty", objtr.Week6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day1_Qty", objtr.Day1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day2_Qty", objtr.Day2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day3_Qty", objtr.Day3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day4_Qty", objtr.Day4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day5_Qty", objtr.Day5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day6_Qty", objtr.Day6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day7_Qty", objtr.Day7_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day8_Qty", objtr.Day8_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day9_Qty", objtr.Day9_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day10_Qty", objtr.Day10_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day11_Qty", objtr.Day11_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day12_Qty", objtr.Day12_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day13_Qty", objtr.Day13_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day14_Qty", objtr.Day14_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day15_Qty", objtr.Day15_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day16_Qty", objtr.Day16_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day17_Qty", objtr.Day17_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day18_Qty", objtr.Day18_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day19_Qty", objtr.Day19_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day20_Qty", objtr.Day20_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day21_Qty", objtr.Day21_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day22_Qty", objtr.Day22_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day23_Qty", objtr.Day23_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day24_Qty", objtr.Day24_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day25_Qty", objtr.Day25_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day26_Qty", objtr.Day26_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day27_Qty", objtr.Day27_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day28_Qty", objtr.Day28_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day29_Qty", objtr.Day29_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day30_Qty", objtr.Day30_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day31_Qty", objtr.Day31_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month1_Qty", objtr.Month1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month2_Qty", objtr.Month2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month3_Qty", objtr.Month3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month4_Qty", objtr.Month4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month5_Qty", objtr.Month5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month6_Qty", objtr.Month6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month7_Qty", objtr.Month7_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month8_Qty", objtr.Month8_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month9_Qty", objtr.Month9_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month10_Qty", objtr.Month10_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month11_Qty", objtr.Month11_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month12_Qty", objtr.Month12_Qty)
                    clsCommon.AddColumnsForChange(coll, "Pers_Type", objtr.Pers_Type)
                    clsCommon.AddColumnsForChange(coll, "Pers_Value", objtr.Pers_Value)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_SCH_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBalanceScheduleQty(ByVal strScheduleCode As String, ByVal strICode As String, ByVal strDocumentNo As String, ByVal strCurrDate As Date, ByVal strUOM As String, Optional ByVal IsCheckWholeMonth As Boolean = False) As Double
        Dim days As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "dd")))
        Dim Week_of_sch As Integer = clsPurchaseSchedule.GetNoOfWeekInMonth(strCurrDate, True) ' CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select datepart(ww,'" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "')")))
        Dim month As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "MM")))

        If IsCheckWholeMonth Then 'this cond. is add so that when data save or cellvalue changed occur and transaction is done in mid of month,but qty. entered more then bal. of that current day or week then it check for whole month available qty.
            days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "MM"))))
            Week_of_sch = clsPurchaseSchedule.GetNoOfWeekInMonth(strCurrDate, False)
        End If

        Dim weekColumns As String = "0"
        Dim monthColumns As String = "0"
        Dim dayColumns As String = "0"

        If clsCommon.myLen(strCurrDate) > 0 Then
            For ii As Integer = 1 To days
                dayColumns = dayColumns + " + isnull(Day" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
            For ii As Integer = 1 To Week_of_sch
                weekColumns = weekColumns + " + isnull(Week" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
            For ii As Integer = 1 To month
                monthColumns = monthColumns + " + isnull(Month" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
        End If

        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_PO_SCH_DETAIL.Item_Code as ICode,(" + dayColumns + ") as Qty,1 as RI from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_DETAIL.document_code ='" + strScheduleCode + "' and TSPL_PO_SCH_DETAIL.Item_Code='" + strICode + "' and  TSPL_PO_SCH_DETAIL.Unit_code='" + strUOM + "' " & _
            " union all " & _
            " select TSPL_PO_SCH_DETAIL.Item_Code as ICode,(" + weekColumns + ") as Qty,1 as RI from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_DETAIL.document_code ='" + strScheduleCode + "' and TSPL_PO_SCH_DETAIL.Item_Code='" + strICode + "' and  TSPL_PO_SCH_DETAIL.Unit_code='" + strUOM + "' " & _
            " union all " & _
            " select TSPL_PO_SCH_DETAIL.Item_Code as ICode,(" + monthColumns + ") as Qty,1 as RI from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_DETAIL.document_code ='" + strScheduleCode + "' and TSPL_PO_SCH_DETAIL.Item_Code='" + strICode + "' and  TSPL_PO_SCH_DETAIL.Unit_code='" + strUOM + "' " & _
            " union all "

        qry += "   select  TSPL_GRN_DETAIL.Item_Code as ICode,((TSPL_GRN_DETAIL.GRN_Qty)) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.Against_Schedule_Code='" + strScheduleCode + "' and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strDocumentNo + "') and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0  " & _
               "   union all select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.srn_no=TSPL_SRN_DETAIL.srn_no where TSPL_SRN_DETAIL.Against_Schedule_Code='" + strScheduleCode + "' and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and TSPL_SRN_DETAIL.srn_no not in ('" + strDocumentNo + "') and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0  " & _
               "   union all select TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,(TSPL_RGP_JOB_WORK_DETAIL.rgp_qty) as Qty,-1 as RI from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no where TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code='" + strScheduleCode + "' and TSPL_RGP_JOB_WORK_DETAIL.Item_Code='" + strICode + "' and  TSPL_RGP_JOB_WORK_DETAIL.Unit_code='" + strUOM + "' and TSPL_RGP_JOB_WORK_DETAIL.rgp_no not in ('" + strDocumentNo + "')  " & _
               " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class

Public Class clsPurchaseScheduleVendorDetail
#Region "Variables"
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_Type As String = Nothing
    Public Unit_Code As String = Nothing
    Public PO_Code As String = Nothing
    Public PO_Date As Date = Nothing
    Public PO_Qty As Double = Nothing
    Public Schedule_Qty As Double = Nothing
    Public Week1_Qty As Double = Nothing
    Public Week2_Qty As Double = Nothing
    Public Week3_Qty As Double = Nothing
    Public Week4_Qty As Double = Nothing
    Public Week5_Qty As Double = Nothing
    Public Week6_Qty As Double = Nothing
    Public Day1_Qty As Double = Nothing
    Public Day2_Qty As Double = Nothing
    Public Day3_Qty As Double = Nothing
    Public Day4_Qty As Double = Nothing
    Public Day5_Qty As Double = Nothing
    Public Day6_Qty As Double = Nothing
    Public Day7_Qty As Double = Nothing
    Public Day8_Qty As Double = Nothing
    Public Day9_Qty As Double = Nothing
    Public Day10_Qty As Double = Nothing
    Public Day11_Qty As Double = Nothing
    Public Day12_Qty As Double = Nothing
    Public Day13_Qty As Double = Nothing
    Public Day14_Qty As Double = Nothing
    Public Day15_Qty As Double = Nothing
    Public Day16_Qty As Double = Nothing
    Public Day17_Qty As Double = Nothing
    Public Day18_Qty As Double = Nothing
    Public Day19_Qty As Double = Nothing
    Public Day20_Qty As Double = Nothing
    Public Day21_Qty As Double = Nothing
    Public Day22_Qty As Double = Nothing
    Public Day23_Qty As Double = Nothing
    Public Day24_Qty As Double = Nothing
    Public Day25_Qty As Double = Nothing
    Public Day26_Qty As Double = Nothing
    Public Day27_Qty As Double = Nothing
    Public Day28_Qty As Double = Nothing
    Public Day29_Qty As Double = Nothing
    Public Day30_Qty As Double = Nothing
    Public Day31_Qty As Double = Nothing
    Public Month1_Qty As Double = Nothing
    Public Month2_Qty As Double = Nothing
    Public Month3_Qty As Double = Nothing
    Public Month4_Qty As Double = Nothing
    Public Month5_Qty As Double = Nothing
    Public Month6_Qty As Double = Nothing
    Public Month7_Qty As Double = Nothing
    Public Month8_Qty As Double = Nothing
    Public Month9_Qty As Double = Nothing
    Public Month10_Qty As Double = Nothing
    Public Month11_Qty As Double = Nothing
    Public Month12_Qty As Double = Nothing
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsPurchaseScheduleVendorDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_PO_VENDOR_SCH_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseScheduleVendorDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "PO_Code", objtr.PO_Code, True)
                    If clsCommon.myLen(objtr.PO_Date) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "PO_Date", clsCommon.GetPrintDate(objtr.PO_Date, "dd/MMM/yyyy"))
                    End If

                    clsCommon.AddColumnsForChange(coll, "PO_Qty", objtr.PO_Qty)
                    clsCommon.AddColumnsForChange(coll, "Schedule_Qty", objtr.Schedule_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week1_Qty", objtr.Week1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week2_Qty", objtr.Week2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week3_Qty", objtr.Week3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week4_Qty", objtr.Week4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week5_Qty", objtr.Week5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Week6_Qty", objtr.Week6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day1_Qty", objtr.Day1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day2_Qty", objtr.Day2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day3_Qty", objtr.Day3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day4_Qty", objtr.Day4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day5_Qty", objtr.Day5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day6_Qty", objtr.Day6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day7_Qty", objtr.Day7_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day8_Qty", objtr.Day8_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day9_Qty", objtr.Day9_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day10_Qty", objtr.Day10_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day11_Qty", objtr.Day11_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day12_Qty", objtr.Day12_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day13_Qty", objtr.Day13_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day14_Qty", objtr.Day14_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day15_Qty", objtr.Day15_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day16_Qty", objtr.Day16_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day17_Qty", objtr.Day17_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day18_Qty", objtr.Day18_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day19_Qty", objtr.Day19_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day20_Qty", objtr.Day20_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day21_Qty", objtr.Day21_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day22_Qty", objtr.Day22_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day23_Qty", objtr.Day23_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day24_Qty", objtr.Day24_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day25_Qty", objtr.Day25_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day26_Qty", objtr.Day26_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day27_Qty", objtr.Day27_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day28_Qty", objtr.Day28_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day29_Qty", objtr.Day29_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day30_Qty", objtr.Day30_Qty)
                    clsCommon.AddColumnsForChange(coll, "Day31_Qty", objtr.Day31_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month1_Qty", objtr.Month1_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month2_Qty", objtr.Month2_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month3_Qty", objtr.Month3_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month4_Qty", objtr.Month4_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month5_Qty", objtr.Month5_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month6_Qty", objtr.Month6_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month7_Qty", objtr.Month7_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month8_Qty", objtr.Month8_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month9_Qty", objtr.Month9_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month10_Qty", objtr.Month10_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month11_Qty", objtr.Month11_Qty)
                    clsCommon.AddColumnsForChange(coll, "Month12_Qty", objtr.Month12_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_VENDOR_SCH_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class