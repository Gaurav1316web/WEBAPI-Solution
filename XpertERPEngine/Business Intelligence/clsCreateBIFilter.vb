Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Linq

Public Class clsCreateBIFilter

#Region "Variables"
    Public Code As String = ""
    Public Description As String = ""
    Public Is_Security_Location As Boolean = False
    Public Qry As String = ""
    Public Is_Create_By_Developer As Boolean = False
    Public Filter_Type As String = ""
    Public Tree_Level As Integer = 0
    Public arr As List(Of clsCreateBIFilterDetails)
#End Region

    Public Function SaveData(ByVal obj As clsCreateBIFilter, ByVal isNewEntry As Boolean) As Boolean
        Return SaveData(obj, isNewEntry, False)

    End Function

    Public Function SaveData(ByVal obj As clsCreateBIFilter, ByVal isNewEntry As Boolean, ByVal isNewEntryAccordingToCode As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Is_Security_Location", IIf(obj.Is_Security_Location, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Qry", obj.Qry)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Is_Create_By_Developer", IIf(obj.Is_Create_By_Developer, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Filter_Type", obj.Filter_Type)
            clsCommon.AddColumnsForChange(coll, "Tree_Level", obj.Tree_Level)
            If isNewEntryAccordingToCode Then
                qry = " select 1 from TSPL_CREATE_BI_FILTER where code='" + obj.Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If
            End If
            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    qry = " select max(Code) from TSPL_CREATE_BI_FILTER where Is_Create_By_Developer=0"
                    obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(obj.Code) > 0 Then
                        obj.Code = clsCommon.incval(obj.Code)
                    Else
                        obj.Code = "BIFLTR000001"
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_BI_FILTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_BI_FILTER", OMInsertOrUpdate.Update, " code ='" + obj.Code + "'", trans)
            End If
            clsCreateBIFilterDetails.SaveData(obj.Code, obj.arr, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal ShowAllReport As Boolean, ByVal NavType As NavigatorType) As clsCreateBIFilter
        Dim qry As String = "select TSPL_CREATE_BI_FILTER.* from TSPL_CREATE_BI_FILTER  where 2=2"
        Dim whrclas As String = ""
        If Not ShowAllReport Then
            whrclas += " and TSPL_CREATE_BI_FILTER.Is_Create_By_Developer=0"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CREATE_BI_FILTER.Code = (select MIN(Code) from TSPL_CREATE_BI_FILTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CREATE_BI_FILTER.Code = (select Max(Code) from TSPL_CREATE_BI_FILTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CREATE_BI_FILTER.Code = '" + strCode + "'  " + whrclas + ""
            Case NavigatorType.Next
                qry += " and TSPL_CREATE_BI_FILTER.Code = (select Min(Code) from TSPL_CREATE_BI_FILTER where  Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CREATE_BI_FILTER.Code = (select Max(Code) from TSPL_CREATE_BI_FILTER where Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim obj As clsCreateBIFilter = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsCreateBIFilter) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCreateBIFilter()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Is_Security_Location = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Security_Location")) = 1, True, False)
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Qry = clsCommon.myCstr(dt.Rows(0)("Qry"))
            obj.Is_Create_By_Developer = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_By_Developer")) = 1, True, False)
            obj.Filter_Type = clsCommon.myCstr(dt.Rows(0)("Filter_Type"))
            obj.Tree_Level = clsCommon.myCdbl(dt.Rows(0)("Tree_Level"))
            obj.arr = New List(Of clsCreateBIFilterDetails)
            qry = "select * from TSPL_CREATE_BI_FILTER_DETAIL where code='" + obj.Code + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    Dim objtr As New clsCreateBIFilterDetails()
                    objtr.Code = clsCommon.myCstr(dr("Code"))
                    objtr.Filter_Column = clsCommon.myCstr(dr("Filter_Column"))
                    objtr.Is_Select = clsCommon.myCdbl(dr("Is_Select"))
                    obj.arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_GROUP_PROGRAM_MAPPING where Program_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PROGRAM_MASTER where Program_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "Delete from TSPL_CREATE_BI_FILTER_DETAIL where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_CREATE_BI_FILTER where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetQuery(ByVal strCode As String, ByRef strValueMember As String, ByRef strDisplayMembter As String, ByRef strParentValue As String, ByRef isLocationSecurity As Boolean) As String
        Dim strRet As String = ""
        Dim qry As String = "select TSPL_CREATE_BI_FILTER.Qry,TSPL_CREATE_BI_FILTER_Detail.Filter_column,TSPL_CREATE_BI_FILTER_Detail.Is_Select,TSPL_CREATE_BI_FILTER.Is_Security_Location from TSPL_CREATE_BI_FILTER left outer join TSPL_CREATE_BI_FILTER_Detail on TSPL_CREATE_BI_FILTER_Detail.Code=TSPL_CREATE_BI_FILTER.Code where TSPL_CREATE_BI_FILTER.Code='" + strCode + "' and TSPL_CREATE_BI_FILTER_Detail.Is_Select>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                strRet = clsCommon.myCstr(dr("Qry"))
                If clsCommon.myCdbl(dr("Is_Select")) = 1 Then
                    strValueMember = clsCommon.myCstr(dr("Filter_column"))
                ElseIf clsCommon.myCdbl(dr("Is_Select")) = 2 Then
                    strDisplayMembter = clsCommon.myCstr(dr("Filter_column"))
                ElseIf clsCommon.myCdbl(dr("Is_Select")) = 3 Then
                    strParentValue = clsCommon.myCstr(dr("Filter_column"))
                End If
            Next
            If clsCommon.myCdbl(dt.Rows(0)("Is_Security_Location")) = 1 Then
                isLocationSecurity = True
            End If
        End If
        Return strRet
    End Function
End Class

Public Class clsCreateBIFilterDetails
#Region "Variables"
    Public Code As String = ""
    Public Filter_Column As String = ""
    Public Is_Select As Integer = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsCreateBIFilterDetails), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CREATE_BI_FILTER_DETAIL where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            For Each objtr As clsCreateBIFilterDetails In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Is_Select", objtr.Is_Select)
                clsCommon.AddColumnsForChange(coll, "Filter_Column", objtr.Filter_Column)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_BI_FILTER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsBIFilterDeveloper

    Public Shared Sub CreateDeveloperFilter()
        Try
            StateMaster()
            MCCMaster()
            MCCMasterLine()
            MCCRouteMaster()
            ShiftMaster()
            VendorMaster()
            DocumentType()
            LocationMaster()
            TankerMaster()
            CustomerMaster()
            ItemMaster()
            VehicleMaster()
            MILKSAMPLE()
            MilkReceiptCode()
            SRNTransction()
            MCCRouteVLC()
            MCCVLCMASTER()
            DailyProgVLCWiseReport()
            PendingDocumentList()
            LocName()
            CustGroupMST()
            CustZoneMST()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Shared Sub LocName()

        Dim obj As New clsCreateBIFilter()
        obj.Code = "LocName"
        obj.Description = "Loc Master"
        obj.Qry = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Name From TSPL_LOCATION_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"

        obj.Is_Security_Location = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "LocName"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "LocName"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub



    Private Shared Sub DailyProgVLCWiseReport()
        Dim obj As New clsCreateBIReport()
        obj.Code = "DailyProVLC"
        obj.Description = "Daily Progress VLC Wise Report"
        obj.Type = "Pivot Grid"
        obj.Qry = "Select xx.STATE_CODE,  Max(xx.[State Name]) As [State Name],  xx.MCC_CODE,  Max(xx.[MCC Name]) As [MCC Name],  (xx.VLC_CODE) As VLC_CODE,  Max(xx.[VLC Uploader Code]) As [VLC Uploader Code],  Max(xx.VLC_Name) As [VLC Name],  Convert(varchar,xx.DOC_DATE,103) As Date,  xx.DOC_DATE As DOC_DATE,  Convert(decimal(18,2),Sum(xx.QTY)) As QTY,  Convert(decimal(18,2),Sum(xx.FAT) * 100 / Sum(xx.QTY)) As [FAT %],  Convert(decimal(18,2),Sum(xx.SNF) * 100 / Sum(xx.QTY)) As [SNF %]From (Select x.MCC_CODE,    x.MCC_NAME As [MCC Name],    x.STATE_CODE,    x.STATE_NAME As [State Name],    x.VLC_CODE,    x.VLC_Code_VLC_Uploader As [VLC Uploader Code],    x.VLC_Name,    x.DOC_DATE,    x.Item_Code,    x.QTY As Org_QTY,    x.UOM_Code As Org_UOM_Code,    x.FatPer As Org_FatPer,    x.SNPer As Org_SNPer,    x.Fat As Org_Fat,    x.SNF As Org_SNF,    ConvertToLiter.Contained_UOM As UOM,    x.QTY * Case      When x.UOM_Code = ConvertToLiter.Contained_UOM Then      ConvertToLiter.Container_Qty Else ConvertToLiter.Contained_Qty End As QTY,    x.Fat * Case      When x.UOM_Code = ConvertToLiter.Contained_UOM Then      ConvertToLiter.Container_Qty Else ConvertToLiter.Contained_Qty End As FAT,    x.SNF * Case      When x.UOM_Code = ConvertToLiter.Contained_UOM Then      ConvertToLiter.Container_Qty Else ConvertToLiter.Contained_Qty End As SNF  From (Select TSPL_MILK_RECEIPT_HEAD.MCC_CODE,      TSPL_MCC_MASTER.MCC_NAME,      TSPL_STATE_MASTER.STATE_CODE,      TSPL_STATE_MASTER.STATE_NAME,      TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,      TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,      TSPL_VLC_MASTER_HEAD.VLC_Name,      Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As DOC_DATE,      TSPL_MILK_RECEIPT_DETAIL.Item_Code,      TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As QTY,      TSPL_MILK_RECEIPT_DETAIL.UOM_Code,      TSPL_MILK_SAMPLE_DETAIL.FAT As FatPer,      TSPL_MILK_SAMPLE_DETAIL.SNF As SNPer,      (TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT /      100) As Fat,      (TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT /      100) As SNF    From TSPL_MILK_RECEIPT_DETAIL      Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE        = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE      Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =        TSPL_MILK_RECEIPT_HEAD.MCC_CODE      Left Outer Join TSPL_MILK_SAMPLE_DETAIL        On TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =        TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE      Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_RECEIPT_DETAIL.VLC_CODE      Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE =        TSPL_MCC_MASTER.State_Code    Where TSPL_MILK_RECEIPT_DETAIL.UOM_Code In ('LTR', 'KG')) x    Left Outer Join TSPL_WEIGHT_CONVERSION As ConvertToLiter      On ConvertToLiter.Container_UOM = 'LTR' And ConvertToLiter.Contained_UOM =      'KG') xx Group By xx.STATE_CODE,  xx.MCC_CODE,  xx.DOC_DATE,  xx.VLC_CODE"
        Dim strGridLayout As String = "<RadPivotGrid RowsSubTotalsPosition=""None"" ColumnsSubTotalsPosition=""None"" AggregatesPosition=""Columns"" Text=""RadPivotGrid1"" Cursor=""Default"" TabIndex=""36""><RowGroupDescriptions><Telerik.Pivot.Core.PropertyGroupDescription PropertyName=""VLC Uploader Code"" ShowGroupsWithNoData=""False"" SortOrder=""Ascending"" CustomName=""""><GroupComparer xsi:type=""Telerik.Pivot.Core.GroupNameComparer"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyGroupDescription><Telerik.Pivot.Core.PropertyGroupDescription PropertyName=""VLC Name"" ShowGroupsWithNoData=""False"" SortOrder=""Ascending"" CustomName=""""><GroupComparer xsi:type=""Telerik.Pivot.Core.GroupNameComparer"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyGroupDescription></RowGroupDescriptions><ColumnGroupDescriptions><Telerik.Pivot.Core.PropertyGroupDescription PropertyName=""Date"" ShowGroupsWithNoData=""False"" SortOrder=""Ascending"" CustomName=""""><GroupComparer xsi:type=""Telerik.Pivot.Core.GroupNameComparer"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyGroupDescription></ColumnGroupDescriptions><AggregateDescriptions><Telerik.Pivot.Core.PropertyAggregateDescription PropertyName=""QTY"" StringFormat="""" CustomName=""""><AggregateFunction xsi:type=""Telerik.Pivot.Core.SumAggregateFunction"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyAggregateDescription><Telerik.Pivot.Core.PropertyAggregateDescription PropertyName=""FAT %"" StringFormat="""" CustomName=""""><AggregateFunction xsi:type=""Telerik.Pivot.Core.MaxAggregateFunction"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyAggregateDescription><Telerik.Pivot.Core.PropertyAggregateDescription PropertyName=""SNF %"" StringFormat="""" CustomName=""""><AggregateFunction xsi:type=""Telerik.Pivot.Core.MaxAggregateFunction"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" /></Telerik.Pivot.Core.PropertyAggregateDescription></AggregateDescriptions></RadPivotGrid>"
        Dim byteArray As Byte() = Encoding.ASCII.GetBytes(strGridLayout)
        obj.Layout = New MemoryStream(byteArray)
        obj.Report_Module = "SMMPROCRPT"
        obj.Chart_Type = ""
        obj.Chart_Category_Member = ""
        obj.Chart_Series_Member = ""
        obj.Chart_Value_Member = ""
        obj.Is_Create_By_Developer = 1
        obj.is_For_Dashboard = "0"
        obj.arr = New List(Of clsCreateBIReportFilterDetails)
        Dim objtr As clsCreateBIReportFilterDetails
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "STATE_CODE"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "State Name"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "MCC_CODE"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "MCC Name"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "VLC_CODE"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "1"
        objtr.Total_Formula = ""
        objtr.Against_Filter = "MCCVLCMST"
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "VLC Uploader Code"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "Date"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "1"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "QTY"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "1"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "FAT %"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "1"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "SNF %"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "1"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "VLC Name"
        objtr.Is_Date_Range = "0"
        objtr.Is_Date_Type_Column = "0"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "0"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIReportFilterDetails
        objtr.Code = "DailyProVLC"
        objtr.Filter_Column = "DOC_DATE"
        objtr.Is_Date_Range = "1"
        objtr.Is_Date_Type_Column = "1"
        objtr.Is_Show_Total = "0"
        objtr.Is_Numeric_Type_Column = "0"
        objtr.Is_Select = "1"
        objtr.Total_Formula = ""
        objtr.Against_Filter = ""
        objtr.Tree_Level = "0"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
        ''stuti regarding memory leakage
        obj.Layout.Close()
        obj.Layout.Dispose()
    End Sub

    Private Shared Sub StateMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "STATEMASTER"
        obj.Description = "State Master"
        obj.Qry = "Select TSPL_STATE_MASTER.STATE_CODE As Code,TSPL_STATE_MASTER.STATE_NAME As Name From TSPL_STATE_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "STATEMASTER"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "STATEMASTER"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub



    Private Shared Sub MCCMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MCCMaster"
        obj.Description = "MCC Master"
        obj.Qry = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Name From TSPL_LOCATION_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "1"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCMaster"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCMaster"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub
    Private Shared Sub MCCMasterLine()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MCCMasterLine"
        obj.Description = "MCC Master"
        obj.Qry = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Name From TSPL_LOCATION_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = ""
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "1"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCMasterLine"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCMasterLine"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub


    Private Shared Sub MCCRouteMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MccRouteMast"
        obj.Description = "MCCRouteMaster"
        obj.Qry = "Select TSPL_MCC_ROUTE_MASTER.Route_Code As Code,  TSPL_MCC_ROUTE_MASTER.Route_Name As Name From TSPL_MCC_ROUTE_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MccRouteMast"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MccRouteMast"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub ShiftMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "ShiftMaster"
        obj.Description = "ShiftMaster"
        obj.Qry = "  Select 'Morning' As Code,'Morning' As Name  Union  Select 'Evening' As Code,'Evening' As Name "
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "ShiftMaster"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "ShiftMaster"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub VendorMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "VenMaster"
        obj.Description = "Vendor Master"
        obj.Qry = "Select TSPL_VENDOR_MASTER.Vendor_Code As Code,  TSPL_VENDOR_MASTER.Vendor_Name As Name From TSPL_VENDOR_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "VenMaster"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "VenMaster"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub PendingDocumentList()

        Dim obj As New clsCreateBIFilter()
        obj.Code = "PENDOCLIST"
        obj.Description = "Pending Document List"
        obj.Qry = "Select xxx.Code,  xxx.Name From (Select 'Payable' As Code,    'Payable' As Name  Union  Select 'Receivable' As Code,    'Receivable' As Name  Union  Select 'General Ledger' As Code,    'General Ledger' As Name  Union  Select 'Product Sale' As Code,    'Product Sale' As Name  Union  Select 'Fresh Sale' As Code,    'Fresh Sale' As Name  Union  Select 'Purchase' As Code,    'Purchase' As Name  Union  Select 'Bulk Sale' As Code,    'Bulk Sale' As Name  Union  Select 'Bulk Procurement' As Code,    'Bulk Procurement' As Name  Union  Select 'Material Management' As Code,    'Material Management' As Name  Union  Select 'Common Service' As Code,    'Common Service' As Name  Union  Select 'CSA Sale' As Code,    'CSA Sale' As Name  Union  Select 'Process Production' As Code,    'Process Production' As Name  Union  Select 'MCC Milk Procurement' As Code,    'MCC Milk Procurement' As Name  Union  Select 'Export Sale' As Code,    'Export Sale' As Name  Union  Select 'Merchant Trade' As Code,    'Merchant Trade' As Name  Union  Select 'Milk Job Work' As Code,    'Milk Job Work' As Name  Union  Select 'Service' As Code,    'Service' As Name  Union  Select 'Fixed Asset' As Code,    'Fixed Asset' As Name  Union  Select 'Payroll' As Code,    'Payroll' As Name Union  Select 'Misc Sale' As Code,    'Misc Sale' As Name) xxx"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "PENDOCLIST"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "PENDOCLIST"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub


    Private Shared Sub DocumentType()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "Doc_Type"
        obj.Description = "Document Type"
        obj.Qry = "Select xxx.Code,  xxx.Name From (Select 'Bulk In' As Code,    'Bulk In' As Name  Union  Select 'MCC In' As Code,    'MCC In' As Name) xxx"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Doc_Type"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Doc_Type"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub


    Private Shared Sub LocationMaster()

        Dim obj As New clsCreateBIFilter()
        obj.Code = "LocationMast"
        obj.Description = "Location"
        obj.Qry = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Name From TSPL_LOCATION_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "1"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "LocationMast"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "LocationMast"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub


    Private Shared Sub TankerMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "TankerMast"
        obj.Description = "Tanker Master"
        obj.Qry = "Select TSPL_TANKER_MASTER.Tanker_No As Code,  TSPL_TANKER_MASTER.Tanker_Name As Name From TSPL_TANKER_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "TankerMast"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "TankerMast"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub ItemMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "Item_Master"
        obj.Description = "Item Master"
        obj.Qry = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Name From TSPL_ITEM_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Item_Master"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Item_Master"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub CustomerMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "Cust_mast"
        obj.Description = "Customer"
        obj.Qry = "Select TSPL_CUSTOMER_MASTER.Cust_Code As Code,  TSPL_CUSTOMER_MASTER.Customer_Name As Name From TSPL_CUSTOMER_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Cust_mast"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "Cust_mast"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub VehicleMaster()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "veh_Mast"
        obj.Description = "Vehicle master"
        obj.Qry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "veh_Mast"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "veh_Mast"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub MILKSAMPLE()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "milkSample"
        obj.Description = "Milk Sample"
        obj.Qry = "Select TSPL_MILK_SAMPLE_HEAD.DOC_CODE,TSPL_MILK_SAMPLE_HEAD.DOC_CODE as Name From TSPL_MILK_SAMPLE_HEAD"
        obj.Is_Create_By_Developer = 1
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "milkSample"
        objtr.Filter_Column = "DOC_CODE"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "milkSample"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub MilkReceiptCode()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MLKRecCode"
        obj.Description = "Milk Receipt Code"
        obj.Qry = "Select TSPL_MILK_RECEIPT_HEAD.DOC_CODE,TSPL_MILK_RECEIPT_HEAD.DOC_CODE as Name From TSPL_MILK_RECEIPT_HEAD"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MLKRecCode"
        objtr.Filter_Column = "DOC_CODE"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MLKRecCode"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub
    Private Shared Sub MCCVLCMASTER()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MCCVLCMST"
        obj.Description = "MCC VLC Master"
        obj.Qry = "Select TSPL_VLC_MASTER_HEAD.VLC_Code As Code,  TSPL_VLC_MASTER_HEAD.VLC_Name As Name From TSPL_VLC_MASTER_HEAD"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCVLCMST"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCVLCMST"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub SRNTransction()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "SRNTrans"
        obj.Description = "SRN Transaction"
        obj.Qry = "Select TSPL_MILK_SRN_HEAD.DOC_CODE From TSPL_MILK_SRN_HEAD"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "SRNTrans"
        objtr.Filter_Column = "DOC_CODE"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    'Private Shared Sub MCCRouteVLC()
    '    Dim obj As New clsCreateBIFilter()
    '    obj.Code = "MCCRouteVLC"
    '    obj.Description = "MCC Route VLC"
    '    obj.Qry = "Select TSPL_VLC_MASTER_HEAD.VLC_Code As Code,  TSPL_VLC_MASTER_HEAD.VLC_Name As Name,  TSPL_VLC_MASTER_HEAD.Route_Code As ParentCode,  3 As Lvl From TSPL_VLC_MASTER_HEAD Where Len(IsNull(TSPL_VLC_MASTER_HEAD.Route_Code, '')) > 0 Union All Select TSPL_MCC_ROUTE_MASTER.Route_Code As Code,  TSPL_MCC_ROUTE_MASTER.Route_Name As Name,  TSPL_MCC_ROUTE_MASTER.MCC_Code As ParentCode,  2 As Lvl From TSPL_MCC_ROUTE_MASTER Where Len(IsNull(TSPL_MCC_ROUTE_MASTER.MCC_Code, '')) > 0 Union All Select TSPL_MCC_MASTER.MCC_Code As Code,  TSPL_MCC_MASTER.MCC_NAME As Name,  Null As ParentCode,  1 As Lvl From TSPL_MCC_MASTER"
    '    obj.Is_Create_By_Developer = 1
    '    obj.Filter_Type = "T"
    '    obj.Tree_Level = "3"
    '    obj.arr = New List(Of clsCreateBIFilterDetails)
    '    Dim objtr As clsCreateBIFilterDetails
    '    objtr = New clsCreateBIFilterDetails
    '    objtr.Code = "MCCRouteVLC"
    '    objtr.Filter_Column = "Code"
    '    objtr.Is_Select = "0"
    '    obj.arr.Add(objtr)
    '    objtr = New clsCreateBIFilterDetails
    '    objtr.Code = "MCCRouteVLC"
    '    objtr.Filter_Column = "Name"
    '    objtr.Is_Select = "0"
    '    obj.arr.Add(objtr)
    '    objtr = New clsCreateBIFilterDetails
    '    objtr.Code = "MCCRouteVLC"
    '    objtr.Filter_Column = "ParentCode"
    '    objtr.Is_Select = "0"
    '    obj.arr.Add(objtr)
    '    objtr = New clsCreateBIFilterDetails
    '    objtr.Code = "MCCRouteVLC"
    '    objtr.Filter_Column = "Lvl"
    '    objtr.Is_Select = "0"
    '    obj.arr.Add(objtr)
    '    obj.SaveData(obj, False, True)
    'End Sub
    Private Shared Sub MCCRouteVLC()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "MCCRouteVLC"
        obj.Description = "MCC Route VLC"
        obj.Qry = "Select TSPL_VLC_MASTER_HEAD.VLC_Code As Code,  TSPL_VLC_MASTER_HEAD.VLC_Name As Name,  TSPL_VLC_MASTER_HEAD.Route_Code As ParentCode,  3 As Lvl From TSPL_VLC_MASTER_HEAD Where Len(IsNull(TSPL_VLC_MASTER_HEAD.Route_Code, '')) > 0 Union All Select TSPL_MCC_ROUTE_MASTER.Route_Code As Code,  TSPL_MCC_ROUTE_MASTER.Route_Name As Name,  TSPL_MCC_ROUTE_MASTER.MCC_Code As ParentCode,  2 As Lvl From TSPL_MCC_ROUTE_MASTER Where Len(IsNull(TSPL_MCC_ROUTE_MASTER.MCC_Code, '')) > 0 Union All Select TSPL_MCC_MASTER.MCC_Code As Code,  TSPL_MCC_MASTER.MCC_NAME As Name,  Null As ParentCode,  1 As Lvl From TSPL_MCC_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "T"
        obj.Tree_Level = "3"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCRouteVLC"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCRouteVLC"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCRouteVLC"
        objtr.Filter_Column = "ParentCode"
        objtr.Is_Select = "3"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "MCCRouteVLC"
        objtr.Filter_Column = "Lvl"
        objtr.Is_Select = "0"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub
    Private Shared Sub CustGroupMST()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "custgroupmst"
        obj.Description = "Customer Group"
        obj.Qry = "Select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code As Code,  TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc As Name From TSPL_CUSTOMER_GROUP_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "custgroupmst"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "custgroupmst"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub

    Private Shared Sub CustZoneMST()
        Dim obj As New clsCreateBIFilter()
        obj.Code = "custzonemst"
        obj.Description = "Zone"
        obj.Qry = "Select TSPL_ZONE_MASTER.Zone_Code As Code,  TSPL_ZONE_MASTER.Description As Name From TSPL_ZONE_MASTER"
        obj.Is_Create_By_Developer = 1
        obj.Filter_Type = "G"
        obj.Tree_Level = "0"
        obj.Is_Security_Location = "0"
        obj.arr = New List(Of clsCreateBIFilterDetails)
        Dim objtr As clsCreateBIFilterDetails
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "custzonemst"
        objtr.Filter_Column = "Code"
        objtr.Is_Select = "1"
        obj.arr.Add(objtr)
        objtr = New clsCreateBIFilterDetails
        objtr.Code = "custzonemst"
        objtr.Filter_Column = "Name"
        objtr.Is_Select = "2"
        obj.arr.Add(objtr)
        obj.SaveData(obj, False, True)
    End Sub



End Class
