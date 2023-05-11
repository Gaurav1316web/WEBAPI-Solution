
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSalesHierarchy
#Region "Variables"

    Public DOC_CODE As String
    Public LevelCode As String
    Public LevelName As String
    Public ParentStructCode As String
    Public ParentStructName As String
    Public Description As String
    Public CREATED_BY As String
    Public Applicable_From As Date?
    Public Level_Type As String = ""
    Public Sub_Type As String = ""
    Public Source_Doc As String = ""
    Public Shared trans As SqlTransaction = Nothing
#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_Sales_Hierarchy_Structure where Struct_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = "select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ," & _
            " TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description as [Level Description], " & _
            " TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code],TSPL_Sales_Hierarchy_Structure.Source_Doc as [Source Doc]  from TSPL_Sales_Hierarchy_Structure " & _
            " left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code  "
        str = clsCommon.ShowSelectForm("SalesHierarchy", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getFinder1(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = "select level_code,description from TSPL_Sales_Hierarchy_Levels "
        str = clsCommon.ShowSelectForm("SalesHierarchy", qry, "level_code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function CheckNewEntry(ByVal Struct_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(Struct_Code) as total from TSPL_Sales_Hierarchy_Structure where Struct_Code='" & Struct_Code & "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    

    Public Shared Function savedata(ByVal SalesHier As clsSalesHierarchy, ByVal isnewentry As Boolean)
        trans = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsSalesHierarchy = New clsSalesHierarchy


            Dim coll As New Hashtable()
            

            clsCommon.AddColumnsForChange(coll, "Description", SalesHier.Description)
            clsCommon.AddColumnsForChange(coll, "Level_Code", SalesHier.LevelCode)
            clsCommon.AddColumnsForChange(coll, "Parent_Struct_Code", SalesHier.ParentStructCode, True)
            clsCommon.AddColumnsForChange(coll, "Source_Doc", SalesHier.Source_Doc, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If clsCommon.myLen(SalesHier.Applicable_From) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Applicable_From", clsCommon.GetPrintDate(SalesHier.Applicable_From, "dd/MMM/yyyy"))
            End If
            If isnewentry Then

                clsCommon.AddColumnsForChange(coll, "Struct_Code", SalesHier.DOC_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sales_Hierarchy_Structure", OMInsertOrUpdate.Insert, "", trans)
                Dim qry As String = " insert into TSPL_Sales_Hierarchy_Structure_Hist select TSPL_Sales_Hierarchy_Structure.struct_code,TSPL_Sales_Hierarchy_Structure.description,TSPL_Sales_Hierarchy_Structure.Level_Code ,TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code ,Created_By ,Created_Date ,Modified_By ,Modified_Date ,Comp_Code ,'" + objCommonVar.CurrentUserCode + "',Applicable_From,Source_Doc from TSPL_Sales_Hierarchy_Structure where Struct_Code='" + SalesHier.DOC_CODE + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                Dim qry As String = " insert into TSPL_Sales_Hierarchy_Structure_Hist select TSPL_Sales_Hierarchy_Structure.struct_code,TSPL_Sales_Hierarchy_Structure.description,TSPL_Sales_Hierarchy_Structure.Level_Code ,TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code ,Created_By ,Created_Date ,Modified_By ,Modified_Date ,Comp_Code ,'" + objCommonVar.CurrentUserCode + "',CURRENT_TIMESTAMP,Source_Doc from TSPL_Sales_Hierarchy_Structure where Struct_Code='" + SalesHier.DOC_CODE + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sales_Hierarchy_Structure", OMInsertOrUpdate.Update, " Struct_Code='" + SalesHier.DOC_CODE + "'", trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType) As clsSalesHierarchy
        Try
            Dim obj As clsSalesHierarchy = Nothing
            Dim qst As String = " select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ," & _
                                " TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description as [Level Description]," & _
                                " TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code],TSPL_Sales_Hierarchy_Structure_for_parent.description as [Parent Struct Name], " & _
                                " TSPL_Sales_Hierarchy_Structure.Created_Date,TSPL_Sales_Hierarchy_Structure.Applicable_From,TSPL_Sales_Hierarchy_Levels.Level_Type,TSPL_Sales_Hierarchy_Levels.Sub_Type,TSPL_Sales_Hierarchy_Structure.Source_Doc " & _
                                " from TSPL_Sales_Hierarchy_Structure left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code " & _
                                " left outer join TSPL_Sales_Hierarchy_Structure as TSPL_Sales_Hierarchy_Structure_for_parent on TSPL_Sales_Hierarchy_Structure_for_parent.Struct_Code=TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code " & _
                                " where 2=2"
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and TSPL_Sales_Hierarchy_Structure.Struct_Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and TSPL_Sales_Hierarchy_Structure.Struct_Code in (select  min(Struct_Code)from TSPL_Sales_Hierarchy_Structure  where TSPL_Sales_Hierarchy_Structure.Struct_Code  >'" + code + "')"
                Case navigatortype.First
                    qst += "and TSPL_Sales_Hierarchy_Structure.Struct_Code in (select MIN(Struct_Code)from TSPL_Sales_Hierarchy_Structure )"

                Case navigatortype.Last
                    qst += "and TSPL_Sales_Hierarchy_Structure.Struct_Code in (select Max(Struct_Code)from TSPL_Sales_Hierarchy_Structure )"
                Case navigatortype.Previous
                    qst += "and TSPL_Sales_Hierarchy_Structure.Struct_Code in (select  max(Struct_Code)from TSPL_Sales_Hierarchy_Structure  where TSPL_Sales_Hierarchy_Structure.Struct_Code <'" + code + "')"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsSalesHierarchy
                obj.DOC_CODE = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                obj.LevelCode = clsCommon.myCstr(dt1.Rows(0)("Level Code"))
                obj.LevelName = clsCommon.myCstr(dt1.Rows(0)("Level Description"))
                obj.ParentStructCode = clsCommon.myCstr(dt1.Rows(0)("Parent Struct Code"))
                obj.ParentStructName = clsCommon.myCstr(dt1.Rows(0)("Parent Struct Name"))
                obj.Source_Doc = clsCommon.myCstr(dt1.Rows(0)("Source_Doc"))
                obj.Level_Type = clsCommon.myCstr(dt1.Rows(0)("Level_Type"))
                obj.Sub_Type = clsCommon.myCstr(dt1.Rows(0)("Sub_Type"))

                If clsCommon.myLen(dt1.Rows(0)("Applicable_From")) > 0 Then
                    obj.Applicable_From = clsCommon.myCDate(dt1.Rows(0)("Applicable_From"))
                Else
                    obj.Applicable_From = clsCommon.myCDate(dt1.Rows(0)("Created_Date"))
                End If

            End If


            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
