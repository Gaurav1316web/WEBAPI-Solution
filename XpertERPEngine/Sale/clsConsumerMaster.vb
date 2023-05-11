Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class clsConsumerMaster
#Region "Variable"
    Public Consumer_Code As String
    Public Title As String
    Public First_Name As String
    Public Middle_Name As String
    Public Last_Name As String
    Public DOB As DateTime
    Public Father_Name As String
    Public Marital_Status As String
    Public Gender As String
    Public Education As String
    Public C_Add1 As String
    Public C_Add2 As String
    Public C_Add3 As String
    Public C_Country As String
    Public C_State As String
    Public C_City As String
    Public C_Pin_No As String
    Public Same_Address As Integer = 0
    Public P_Add1 As String
    Public P_Add2 As String
    Public P_Add3 As String
    Public P_Country As String
    Public P_State As String
    Public P_City As String
    Public P_Pin_No As String
    Public Mobile_No As String
    Public Land_Line_No As String
    Public Email As String
    Public Alternate_Email As String
    Public Product_Used As String
    Public Specify_Product_Used As String
    Public How_To_Know As String
    Public Specify_How_To_Know As String
    Public Details_Date As DateTime

#End Region

    Public Shared Function SaveData(ByVal obj As clsConsumerMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsConsumerMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Title", obj.Title)
            clsCommon.AddColumnsForChange(coll, "First_Name", obj.First_Name)
            clsCommon.AddColumnsForChange(coll, "Middle_Name", obj.Middle_Name)
            clsCommon.AddColumnsForChange(coll, "Last_Name", obj.Last_Name)
            'If obj.DOB IsNot Nothing AndAlso clsCommon.myLen(obj.DOB) > 0 AndAlso IsDate(obj.DOB) Then
            '    clsCommon.AddColumnsForChange(coll, "DOB", clsCommon.GetPrintDate(obj.DOB, "dd/MM/yyyy hh:mm tt"))
            'End If
            'clsCommon.AddColumnsForChange(coll, "DOB", obj.DOB)
            clsCommon.AddColumnsForChange(coll, "DOB", clsCommon.GetPrintDate(obj.DOB, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Father_Name", obj.Father_Name)
            clsCommon.AddColumnsForChange(coll, "Marital_Status", obj.Marital_Status)
            clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)
            clsCommon.AddColumnsForChange(coll, "Education", obj.Education)
            clsCommon.AddColumnsForChange(coll, "C_Add1", obj.C_Add1)
            clsCommon.AddColumnsForChange(coll, "C_Add2", obj.C_Add2)
            clsCommon.AddColumnsForChange(coll, "C_Add3", obj.C_Add3)
            clsCommon.AddColumnsForChange(coll, "C_Country", obj.C_Country)
            clsCommon.AddColumnsForChange(coll, "C_State", obj.C_State)
            clsCommon.AddColumnsForChange(coll, "C_City", obj.C_City)
            clsCommon.AddColumnsForChange(coll, "C_Pin_No", obj.C_Pin_No)
            clsCommon.AddColumnsForChange(coll, "Same_Address", obj.Same_Address)
            clsCommon.AddColumnsForChange(coll, "P_Add1", obj.P_Add1)
            clsCommon.AddColumnsForChange(coll, "P_Add2", obj.P_Add2)
            clsCommon.AddColumnsForChange(coll, "P_Add3", obj.P_Add3)
            clsCommon.AddColumnsForChange(coll, "P_Country", obj.P_Country)
            clsCommon.AddColumnsForChange(coll, "P_State", obj.P_State)
            clsCommon.AddColumnsForChange(coll, "P_City", obj.P_City)
            clsCommon.AddColumnsForChange(coll, "P_Pin_No", obj.P_Pin_No)
            clsCommon.AddColumnsForChange(coll, "Mobile_No", obj.Mobile_No)
            clsCommon.AddColumnsForChange(coll, "Land_Line_No", obj.Land_Line_No)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "Alternate_Email", obj.Alternate_Email)
            clsCommon.AddColumnsForChange(coll, "Product_Used", obj.Product_Used)
            clsCommon.AddColumnsForChange(coll, "Specify_Product_Used", obj.Specify_Product_Used)
            clsCommon.AddColumnsForChange(coll, "How_To_Know", obj.How_To_Know)
            clsCommon.AddColumnsForChange(coll, "Specify_How_To_Know", obj.Specify_How_To_Know)
            clsCommon.AddColumnsForChange(coll, "Details_Date", clsCommon.GetPrintDate(obj.Details_Date, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Consumer_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy hh:mm tt"), clsDocType.ConsumerDetails, "", "")
                clsCommon.AddColumnsForChange(coll, "Consumer_Code", obj.Consumer_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONSUMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONSUMER_MASTER", OMInsertOrUpdate.Update, "Consumer_Code='" + obj.Consumer_Code + "' ", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strConsumerCode As String, ByVal NavType As NavigatorType) As clsConsumerMaster
        Return GetData(strConsumerCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strConsumerCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsConsumerMaster
        Dim obj As clsConsumerMaster = Nothing
        Dim qry As String = "SELECT Consumer_Code,Title,First_Name,Middle_Name,Last_Name,DOB,Father_Name,Marital_Status,Gender,Education,C_Add1,C_Add2,C_Add3,C_Country,C_State,C_City,C_Pin_No,Same_Address,P_Add1,P_Add2,P_Add3,P_Country,P_State,P_City,P_Pin_No,Mobile_No,Land_Line_No,Email,Alternate_Email,Product_Used,Specify_Product_Used,How_To_Know,Specify_How_To_Know  from TSPL_CONSUMER_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Consumer_Code =  (select MIN(Consumer_Code) from TSPL_CONSUMER_MASTER)"
            Case NavigatorType.Last
                qry += " and Consumer_Code = (select Max(Consumer_Code) from TSPL_CONSUMER_MASTER)"
            Case NavigatorType.Next
                qry += " and Consumer_Code = (select Min(Consumer_Code) from TSPL_CONSUMER_MASTER where  Consumer_Code>'" + strConsumerCode + "')"
            Case NavigatorType.Previous
                qry += " and Consumer_Code = (select Max(Consumer_Code) from TSPL_CONSUMER_MASTER where Consumer_Code<'" + strConsumerCode + "')"
            Case NavigatorType.Current
                qry += " and Consumer_Code = '" + strConsumerCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsConsumerMaster()

            obj.Consumer_Code = clsCommon.myCstr(dt.Rows(0)("Consumer_Code"))
            obj.Title = clsCommon.myCstr(dt.Rows(0)("Title"))
            obj.First_Name = clsCommon.myCstr(dt.Rows(0)("First_Name"))
            obj.Middle_Name = clsCommon.myCstr(dt.Rows(0)("Middle_Name"))
            obj.Last_Name = clsCommon.myCstr(dt.Rows(0)("Last_Name"))
            'If dt.Rows(0)("DOB") IsNot Nothing AndAlso clsCommon.myLen(dt.Rows(0)("DOB")) > 0 AndAlso IsDate(dt.Rows(0)("DOB")) Then
            '    obj.DOB = clsCommon.myCDate(dt.Rows(0)("DOB"))
            'End If
            obj.DOB = clsCommon.myCDate(dt.Rows(0)("DOB"))
            obj.Father_Name = clsCommon.myCstr(dt.Rows(0)("Father_Name"))
            obj.Marital_Status = clsCommon.myCstr(dt.Rows(0)("Marital_Status"))
            obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
            obj.Education = clsCommon.myCstr(dt.Rows(0)("Education"))
            obj.C_Add1 = clsCommon.myCstr(dt.Rows(0)("C_Add1"))
            obj.C_Add2 = clsCommon.myCstr(dt.Rows(0)("C_Add2"))
            obj.C_Add3 = clsCommon.myCstr(dt.Rows(0)("C_Add3"))
            obj.C_Country = clsCommon.myCstr(dt.Rows(0)("C_Country"))
            obj.C_State = clsCommon.myCstr(dt.Rows(0)("C_State"))
            obj.C_City = clsCommon.myCstr(dt.Rows(0)("C_City"))
            obj.C_Pin_No = clsCommon.myCstr(dt.Rows(0)("C_Pin_No"))
            obj.Same_Address = clsCommon.myCdbl(dt.Rows(0)("Same_Address"))
            obj.P_Add1 = clsCommon.myCstr(dt.Rows(0)("P_Add1"))
            obj.P_Add2 = clsCommon.myCstr(dt.Rows(0)("P_Add2"))
            obj.P_Add3 = clsCommon.myCstr(dt.Rows(0)("P_Add3"))
            obj.P_Country = clsCommon.myCstr(dt.Rows(0)("P_Country"))
            obj.P_State = clsCommon.myCstr(dt.Rows(0)("P_State"))
            obj.P_City = clsCommon.myCstr(dt.Rows(0)("P_City"))
            obj.P_Pin_No = clsCommon.myCstr(dt.Rows(0)("P_Pin_No"))
            obj.Mobile_No = clsCommon.myCstr(dt.Rows(0)("Mobile_No"))
            obj.Land_Line_No = clsCommon.myCstr(dt.Rows(0)("Land_Line_No"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.Alternate_Email = clsCommon.myCstr(dt.Rows(0)("Alternate_Email"))
            obj.Product_Used = clsCommon.myCstr(dt.Rows(0)("Product_Used"))
            obj.Specify_Product_Used = clsCommon.myCstr(dt.Rows(0)("Specify_Product_Used"))
            obj.How_To_Know = clsCommon.myCstr(dt.Rows(0)("How_To_Know"))
            obj.Specify_How_To_Know = clsCommon.myCstr(dt.Rows(0)("Specify_How_To_Know"))
        End If
        Return obj
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
            Dim qry As String = "delete from TSPL_CONSUMER_MASTER where Consumer_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinderCountry(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select COUNTRY_CODE as [Code],COUNTRY_NAME as [Country Name] from tspl_country_master "
        str = clsCommon.ShowSelectForm("CNTRYMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getFinderState(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select STATE_CODE as [Code],STATE_NAME as [State Name] from tspl_state_master"
        str = clsCommon.ShowSelectForm("STMSTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderCity(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select City_Code as [Code],City_Name as [City Name] from TSPL_CITY_MASTER  "
        str = clsCommon.ShowSelectForm("RPTCITYFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

End Class
