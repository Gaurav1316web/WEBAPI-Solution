Imports System.Data.SqlClient

Public Class clsBullMasters
    Public Bull_Code As String = Nothing
    Public Species_Code As String = Nothing
    Public Category_Code As String = Nothing
    Public Sub_Category_Code As String = Nothing
    Public SS_Centre_Code As String = Nothing
    Public Breed_Code As String = Nothing
    Public Shed_Code As String = Nothing
    Public Pen_Code As String = Nothing
    Public Status_Code As String = Nothing
    Public Sub_Status_Code As String = Nothing
    Public is_Semen As Boolean = False
    Public RadioButton2 As String = Nothing
    Public RadioButton1 As String = Nothing
    Public Exotic_Blood_Per As String = Nothing
    Public Bull_Book_Value As String = Nothing
    Public Country_Code As String = Nothing
    Public Digit_Bull_Id As String = Nothing
    Public Prev_Bull_Id As String = Nothing
    Public Date_Of_Birth As Date
    Public Registration_Date As Date
    Public SS_Bull_Id As String = Nothing
    Public Status_Changed_Date As Date
    Public Bull_Alia_Name As String = Nothing
    Public Exit_Date As Date
    Public Bull_Rating As String = Nothing
    Public Dam_Location_Yeild As String = Nothing
    Public Bull_source_Printing_Straws As String = Nothing
    Public Bull_RFID As String = Nothing
    Public Remark As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime



    Public Function SaveData(ByVal obj As clsBullMasters, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsBullMasters, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_MASTER where Bull_Code='" + obj.Bull_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Bull_Code", obj.Bull_Code)

            clsCommon.AddColumnsForChange(coll, "Is_Semen", IIf(obj.is_Semen, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Species_Code", obj.Species_Code)
            clsCommon.AddColumnsForChange(coll, "Category_Code", obj.Category_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Category_Code", obj.Sub_Category_Code)
            clsCommon.AddColumnsForChange(coll, "SS_Centre_Code", obj.SS_Centre_Code)
            clsCommon.AddColumnsForChange(coll, "Breed_Code", obj.Breed_Code)
            clsCommon.AddColumnsForChange(coll, "Shed_Code", obj.Shed_Code)
            clsCommon.AddColumnsForChange(coll, "Pen_Code", obj.Pen_Code)
            clsCommon.AddColumnsForChange(coll, "Status_Code", obj.Status_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Status_Code", obj.Sub_Status_Code)
            'clsCommon.AddColumnsForChange(coll, "Is_Semen", obj.is_Semen)
            clsCommon.AddColumnsForChange(coll, "Exotic_Blood_Per", obj.Exotic_Blood_Per)
            clsCommon.AddColumnsForChange(coll, "Bull_Book_Value", obj.Bull_Book_Value)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.Country_Code)
            clsCommon.AddColumnsForChange(coll, "Prev_Bull_Id", obj.Prev_Bull_Id)
            clsCommon.AddColumnsForChange(coll, "SS_Bull_Id", obj.SS_Bull_Id)
            clsCommon.AddColumnsForChange(coll, "Bull_Alia_Name", obj.Bull_Alia_Name)
            clsCommon.AddColumnsForChange(coll, "Bull_Rating", obj.Bull_Rating)
            clsCommon.AddColumnsForChange(coll, "Dam_Location_Yeild", obj.Dam_Location_Yeild)
            clsCommon.AddColumnsForChange(coll, "Bull_source_Printing_Straws", obj.Bull_source_Printing_Straws)
            'clsCommon.AddColumnsForChange(coll, "Digit_Bull_Id", obj.Digit_Bull_Id)
            clsCommon.AddColumnsForChange(coll, "Bull_RFID", obj.Bull_RFID)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            'clsCommon.AddColumnsForChange(coll, "Species_Code", IIf(obj.IS_Transpoter, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Registration_Date", clsCommon.GetPrintDate(obj.Registration_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Date_Of_Birth", clsCommon.GetPrintDate(obj.Date_Of_Birth, "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Date_Of_Birth", obj.Date_Of_Birth)

            clsCommon.AddColumnsForChange(coll, "Status_Changed_Date", clsCommon.GetPrintDate(obj.Status_Changed_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Exit_Date", clsCommon.GetPrintDate(obj.Exit_Date, "dd/MMM/yyyy"))

            'clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType, True)

            'If obj.Exit_Date IsNot Nothing Then
            '    clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            'Else
            '    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            'End If
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.BullMasters, "", "")
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_MASTER.Bull_Code='" + obj.Bull_Code + "'", trans)
            End If

            'IsSaved = IsSaved AndAlso clsDistributorRouteTaggingDetail.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BULL_MASTER where Bull_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullMasters
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullMasters = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullMasters
        Dim obj As clsBullMasters = Nothing

        Try
            Dim strQry As String = "select * from TSPL_BULL_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Bull_Code = (select MIN(Code) from TSPL_BULL_MOVEMENT_TYPE where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Bull_Code = (Select Max(Code) from TSPL_BULL_MOVEMENT_TYPE where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Bull_Code = (Select Min(Code) from TSPL_BULL_MOVEMENT_TYPE where Bull_Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Bull_Code = (select Max(Code) from TSPL_BULL_MOVEMENT_TYPE where Bull_Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Bull_Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullMasters()
                obj.Bull_Code = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
                obj.Species_Code = clsCommon.myCstr(dt.Rows(0)("Species_Code"))
                obj.Category_Code = clsCommon.myCstr(dt.Rows(0)("Category_Code"))
                obj.Sub_Category_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Category_Code"))
                obj.SS_Centre_Code = clsCommon.myCstr(dt.Rows(0)("SS_Centre_Code"))
                obj.Breed_Code = clsCommon.myCstr(dt.Rows(0)("Breed_Code"))
                obj.Shed_Code = clsCommon.myCstr(dt.Rows(0)("Shed_Code"))
                obj.Pen_Code = clsCommon.myCstr(dt.Rows(0)("Pen_Code"))
                obj.Status_Code = clsCommon.myCstr(dt.Rows(0)("Status_Code"))
                obj.Sub_Status_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Status_Code"))
                obj.is_Semen = clsCommon.myCstr(dt.Rows(0)("is_Semen"))
                obj.Exotic_Blood_Per = clsCommon.myCstr(dt.Rows(0)("Exotic_Blood_Per"))
                obj.Bull_Rating = clsCommon.myCstr(dt.Rows(0)("Bull_Rating"))
                obj.Bull_source_Printing_Straws = clsCommon.myCstr(dt.Rows(0)("Bull_source_Printing_Straws"))

                obj.Bull_Book_Value = clsCommon.myCstr(dt.Rows(0)("Bull_Book_Value"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))

                obj.Country_Code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                'obj.Digit_Bull_Id = clsCommon.myCstr(dt.Rows(0)("Digit_Bull_Id"))
                obj.Prev_Bull_Id = clsCommon.myCstr(dt.Rows(0)("Prev_Bull_Id"))
                obj.Date_Of_Birth = clsCommon.myCDate(dt.Rows(0)("Date_Of_Birth"))
                obj.Registration_Date = clsCommon.myCDate(dt.Rows(0)("Registration_Date"))
                obj.Status_Changed_Date = clsCommon.myCDate(dt.Rows(0)("Status_Changed_Date"))
                obj.Exit_Date = clsCommon.myCDate(dt.Rows(0)("Exit_Date"))



            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function
End Class
