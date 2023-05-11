Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsHostSettings

#Region "Variables"
    Public HOST_ID As String
    Public SERVER_TYPE As String
    Public SERVER_NAME_IP As String
    Public DATABASE_NAME As String
    Public SCHEMA_NAME As String
    Public DB_USER_ID As String
    Public DB_PWD As String
    Public REMARKS As String
    Public TEST_CNNECTED_SUCCESS As Boolean
    Public objSyncList As New List(Of clsSyncTables)
#End Region

    Public Shared Function DisableTableTriggers(ByVal TableName As String, ByVal trans As SqlTransaction) As Boolean
        Dim lstTriggers As New List(Of String)
        Dim status As Boolean = True
        lstTriggers = getTableTriggers(TableName, trans)
        For Each trg As String In lstTriggers
            status = status AndAlso DisableTrigger(trg, TableName, trans)
        Next
        Return status
    End Function
    Public Shared Function EnableTableTriggers(ByVal TableName As String, ByVal trans As SqlTransaction) As Boolean
        Dim lstTriggers As New List(Of String)
        Dim status As Boolean = True
        lstTriggers = getTableTriggers(TableName, trans)
        For Each trg As String In lstTriggers
            status = status AndAlso EnableTrigger(trg, TableName, trans)
        Next
        Return status
    End Function
    Public Shared Function getTableTriggers(ByVal TableName As String, ByVal trans As SqlTransaction) As List(Of String)
        Dim qry As String = ""
        Dim lstTriggers As New List(Of String)
        qry = "select o.name from sysobjects AS o INNER JOIN sysobjects AS o2 ON o.parent_obj = o2.id left join sys.triggers trg on trg.name=o.name WHERE o.type = 'TR' and o2.name='" & TableName & "' and trg.is_disabled=0"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            lstTriggers.Add(clsCommon.myCstr(dr.Item("name")))
        Next
        Return lstTriggers
    End Function
    Public Shared Function DisableTrigger(ByVal TableName As String, ByVal TriggerName As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "alter table  " & TableName & " disable TRIGGER " & TriggerName & " "
        Dim status As Boolean = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return status
    End Function
    Public Shared Function EnableTrigger(ByVal TableName As String, ByVal TriggerName As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "ENABLE TRIGGER " & TriggerName & " on " & TableName & ""
        Dim status As Boolean = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return status
    End Function

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SYNC_HOST_SETTINGS.HOST_ID as [Code] ,TSPL_SYNC_HOST_SETTINGS.SERVER_TYPE as [Server Type] ,TSPL_SYNC_HOST_SETTINGS.SERVER_NAME_IP as [Host Name/IP] ,TSPL_SYNC_HOST_SETTINGS.DATABASE_NAME as [Database Name] ,TSPL_SYNC_HOST_SETTINGS.DB_USER_ID as [User Id] ,TSPL_SYNC_HOST_SETTINGS.DB_PWD as [Password] ,TSPL_SYNC_HOST_SETTINGS.REMARKS as [REMARKS]  From TSPL_SYNC_HOST_SETTINGS   "
        str = clsCommon.ShowSelectForm("HostSetting", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getPrimaryKey(ByVal TableName As String) As String
        Dim qry As String = " SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " & _
                            " WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1" & _
                            " AND table_name = '" & TableName & "'"
        Dim PK As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return PK
    End Function
    Public Shared Function getPrimaryKeyDT(ByVal TableName As String) As DataTable
        Dim qry As String = " SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " & _
                            " WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1" & _
                            " AND table_name = '" & TableName & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry) 'clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return dt
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal HostType As String) As clsHostSettings
        Return GetData(HostType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal ServerType As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(ServerType) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_SYNC_HOST_SETTINGS where HOST_ID ='" + ServerType + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            qry = "delete FROM TSPL_SYNC_TABLES "
            isSaved = isSaved AndAlso clsDBFuncationality.getSingleValue(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal SERVER_TYPE As String, ByVal trans As SqlTransaction) As clsHostSettings
        Dim obj As clsHostSettings = Nothing
        Dim qry As String = "select * from TSPL_SYNC_HOST_SETTINGS where SERVER_TYPE='" & SERVER_TYPE & "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsHostSettings()
            obj.HOST_ID = clsCommon.myCstr(dt.Rows(0)("HOST_ID"))
            obj.SERVER_TYPE = clsCommon.myCstr(dt.Rows(0)("SERVER_TYPE"))
            obj.SCHEMA_NAME = clsCommon.myCstr(dt.Rows(0)("SCHEMA_NAME"))
            obj.SERVER_NAME_IP = clsCommon.myCstr(dt.Rows(0)("SERVER_NAME_IP"))
            obj.DATABASE_NAME = clsCommon.myCstr(dt.Rows(0)("DATABASE_NAME"))
            obj.DB_USER_ID = clsCommon.myCstr(dt.Rows(0)("DB_USER_ID"))
            obj.DB_PWD = clsCommon.myCstr(dt.Rows(0)("DB_PWD"))
            obj.REMARKS = clsCommon.myCstr(dt.Rows(0)("REMARKS"))
            obj.TEST_CNNECTED_SUCCESS = clsCommon.myCBool(dt.Rows(0)("TEST_CNNECTED_SUCCESS"))
            obj.objSyncList = clsSyncTables.GetData(trans)
        End If


        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsHostSettings) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsHostSettings, ByVal tran As SqlTransaction) As Boolean
        Dim isNewEntry As Boolean
        Dim qry As String = "SELECT Count(*) FROM TSPL_SYNC_HOST_SETTINGS where SERVER_TYPE= '" & obj.SERVER_TYPE & "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
        If check = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SERVER_TYPE", obj.SERVER_TYPE)
            clsCommon.AddColumnsForChange(coll, "SERVER_NAME_IP", obj.SERVER_NAME_IP)
            clsCommon.AddColumnsForChange(coll, "DATABASE_NAME", obj.DATABASE_NAME)
            clsCommon.AddColumnsForChange(coll, "SCHEMA_NAME", obj.SCHEMA_NAME)
            clsCommon.AddColumnsForChange(coll, "DB_USER_ID", obj.DB_USER_ID)
            clsCommon.AddColumnsForChange(coll, "DB_PWD", obj.DB_PWD)
            clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)

            If isNewEntry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_HOST_SETTINGS", OMInsertOrUpdate.Insert, "", tran)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_HOST_SETTINGS", OMInsertOrUpdate.Update, "SERVER_TYPE='" + obj.SERVER_TYPE + "'", tran)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select HOST_ID from TSPL_SYNC_HOST_SETTINGS where HOST_ID ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function GetTableColumnsForUpdate(ByVal TableName As String, ByVal PK As String, ByVal PK2 As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TableName & "' AND COLUMN_NAME NOT IN ('" & PK & "','" & PK2 & "') order by ORDINAL_POSITION"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCols As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = dt.Rows.Count - 1 Then
                strCols = strCols & "A." & dr.Item("COLUMN_NAME") & "=" & "TA." & dr.Item("COLUMN_NAME")
            Else
                strCols = strCols & "A." & dr.Item("COLUMN_NAME") & "=" & "TA." & dr.Item("COLUMN_NAME") & ","
            End If
        Next
        Return strCols
    End Function

    Public Shared Function GetTableColumnsForInsert(ByVal TableName As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TableName & "' order by ORDINAL_POSITION"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCols As String = "("
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = dt.Rows.Count - 1 Then
                strCols = strCols & dr.Item("COLUMN_NAME") & ")"
            Else
                strCols = strCols & dr.Item("COLUMN_NAME") & ","
            End If
        Next
        Return strCols
    End Function

    Public Shared Function GetTableColumnsForSelect(ByVal TableName As String, ByVal PK As String, ByVal PK2 As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TableName & "'  order by ORDINAL_POSITION"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCols As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = dt.Rows.Count - 1 Then
                strCols = strCols & dr.Item("COLUMN_NAME")
            Else
                strCols = strCols & dr.Item("COLUMN_NAME") & ","
            End If
        Next
        Return strCols
    End Function
    Public Shared Function GetTableColumnsForTransSyncSelect(ByVal TableName As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = " select COL.name AS COLUMN_NAME from sys.all_columns COL INNER JOIN sys.tables TBL ON COL.object_id=TBL.object_id " & _
                            " LEFT JOIN sys.identity_columns IDNT_COL ON COL.object_id=IDNT_COL.object_id AND COL.name= IDNT_COL.name " & _
                            " WHERE TBL.name='" & TableName & "' AND COALESCE(IDNT_COL.IS_IDENTITY,0)=0 ORDER BY COL.column_id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCols As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = dt.Rows.Count - 1 Then
                strCols = strCols & dr.Item("COLUMN_NAME")
            Else
                strCols = strCols & dr.Item("COLUMN_NAME") & ","
            End If
        Next
        Return strCols
    End Function
    Public Shared Function GetTableColumnsForSyncUpdate(ByVal TableName As String, ByVal PK As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        'Dim qry As String = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TableName & "' AND COLUMN_NAME NOT IN ('" & PK & "') order by ORDINAL_POSITION"
        Dim qry As String = " select COL.name AS COLUMN_NAME from sys.all_columns COL INNER JOIN sys.tables TBL ON COL.object_id=TBL.object_id " & _
                            " LEFT JOIN sys.identity_columns IDNT_COL ON COL.object_id=IDNT_COL.object_id AND COL.name= IDNT_COL.name " & _
                            " WHERE TBL.name='" & TableName & "' AND COL.name NOT IN ('" & PK & "') AND COALESCE(IDNT_COL.IS_IDENTITY,0)=0 ORDER BY COL.column_id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCols As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = dt.Rows.Count - 1 Then
                strCols = strCols & "" & TableName & "." & dr.Item("COLUMN_NAME") & "=" & "TA." & dr.Item("COLUMN_NAME")
            Else
                strCols = strCols & "" & TableName & "." & dr.Item("COLUMN_NAME") & "=" & "TA." & dr.Item("COLUMN_NAME") & ","
            End If
        Next
        Return strCols
    End Function

    Public Shared Function SynchronizeClientTable(ByVal TableName As String, ByVal PK As String, ByVal PK2 As String, ByVal _Cond As String, ByVal strClient As String, ByVal strServer As String, ByVal serverConn As SqlConnection, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = ""
        'qry = "set identity_insert " & TableName & " On ;"
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

        qry = "select IDENT_CURRENT( '" & TableName & "' )"
        If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            qry = "set identity_insert " & TableName & " On ;"
        Else
            qry = ""
        End If

        Dim strSelect As String = clsHostSettings.GetTableColumnsForSelect(TableName, PK, PK2, trans)
        Dim qryCond As String = ""
        If clsCommon.myLen(_Cond) > 0 Then
            qryCond = " where " & _Cond
        Else
            qryCond = ""
        End If
        If clsCommon.myLen(PK) > 0 And clsCommon.myLen(PK2) > 0 Then
            qry += " MERGE INTO " & strClient & "." & TableName & "" & "  A" & _
                          " USING( " & _
                          " SELECT " & strSelect & " FROM " & strServer & "." & TableName & " " & qryCond & ") TA " & _
                          " ON (A." & PK & " =TA." & PK & " AND A." & PK2 & " =TA." & PK2 & ")" & _
                          " WHEN MATCHED THEN " & _
                          " update " & _
                          " SET " & clsHostSettings.GetTableColumnsForUpdate(TableName, PK, PK2, trans) & " " & _
                          " WHEN NOT MATCHED THEN " & _
                          " insert " & _
                          " (" & strSelect & ")" & _
                          " VALUES " & _
                          " (" & strSelect & ");"
        ElseIf (clsCommon.myLen(PK) > 0) Then
            qry = " MERGE INTO " & strClient & "." & TableName & "" & "  A" & _
                  " USING( " & _
                  " SELECT " & strSelect & " FROM " & strServer & "." & TableName & " " & qryCond & ") TA " & _
                  " ON (A." & PK & " =TA." & PK & ")" & _
                  " WHEN MATCHED THEN " & _
                  " update " & _
                  " SET " & clsHostSettings.GetTableColumnsForUpdate(TableName, PK, PK2, trans) & " " & _
                  " WHEN NOT MATCHED THEN " & _
                  " insert " & _
                  " (" & strSelect & ")" & _
                  " VALUES " & _
                  " (" & strSelect & ");"
        End If
        'qry = " MERGE INTO " & strClient & "." & TableName & "" & "  A" & _
        '      " USING( " & _
        '      " SELECT " & strSelect & " FROM " & strServer & "." & TableName & "" & ") TA " & _
        '      " ON (A." & PK & " =TA." & PK & ")" & _
        '      " WHEN MATCHED THEN " & _
        '      " update " & _
        '      " SET " & clsHostSettings.GetTableColumnsForUpdate(TableName, trans) & " " & _
        '      " WHEN NOT MATCHED THEN " & _
        '      " insert " & _
        '      " (" & strSelect & ")" & _
        '      " VALUES " & _
        '      " (" & strSelect & ");"
        Return ExecuteNonQueryWithDropContraint(qry, TableName, trans)

    End Function
    Public Shared Function SynchronizeClientDetailTable(ByVal TableName As String, ByVal PK As String, ByVal PK2 As String, ByVal _Cond As String, ByVal strClient As String, ByVal strServer As String, ByVal serverConn As SqlConnection, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = ""
        Dim strSelect As String = clsHostSettings.GetTableColumnsForSelect(TableName, PK, PK2, trans)
        'qry = " MERGE INTO " & strClient & "." & TableName & "" & "  A" & _
        '      " USING( " & _
        '      " SELECT " & strSelect & " FROM " & strServer & "." & TableName & "" & ") TA " & _
        '      " ON (A." & PK & " =TA." & PK & ")" & _
        '      " WHEN MATCHED THEN " & _
        '      " update " & _
        '      " SET " & clsHostSettings.GetTableColumnsForUpdate(TableName, trans) & " " & _
        '      " WHEN NOT MATCHED THEN " & _
        '      " insert " & _
        '      " (" & strSelect & ")" & _
        '      " VALUES " & _
        '      " (" & strSelect & ");"
        Dim isSaved As Boolean = True
        If clsCommon.myLen(_Cond) > 0 Then
            _Cond = " where " & _Cond
        Else
            _Cond = ""
        End If
        qry = "delete from " & TableName & _Cond
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If isSaved Then
            qry = " insert into " & TableName & "" & _
             " (" & strSelect & ")" & _
             " (SELECT " & strSelect & " FROM " & strServer & "." & TableName & "" & _Cond & ");"
            Return clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Else
            Return False
        End If


    End Function

    Public Shared Function SynchronizeClientTableScheme(ByVal TableName As String, ByVal PK As String, ByVal PK2 As String, ByVal _Cond As String, ByVal strClient As String, ByVal strServer As String, ByVal serverConn As SqlConnection, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim qry As String = ""
            Dim isQueryExecuted As Boolean = False
            qry = "SELECT * FROM  " & strServer & ".INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = '" & TableName & "'  order by ORDINAL_POSITION"
            Dim dtColumns_Server As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            qry = "SELECT * FROM  " & strClient & ".INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = '" & TableName & "'  order by ORDINAL_POSITION"
            Dim dtColumns_Client As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            For Each row As DataRow In dtColumns_Server.Rows()
                isQueryExecuted = False
                If dtColumns_Client.Select("Column_Name='" & clsCommon.myCstr(row.Item("COlumn_Name")) & "'").Length > 0 Then
                    Dim dr As DataRow = dtColumns_Client.Select("Column_Name='" & clsCommon.myCstr(row.Item("COlumn_Name")) & "'")(0)
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Is_Nullable")), clsCommon.myCstr(row("Is_Nullable"))) Then
                        qry = "Alter table " & TableName & " alter column " & clsCommon.myCstr(row.Item("COlumn_Name")) & " " & clsCommon.myCstr(row("Data_Type")) & " (" & clsCommon.myCdbl(row("character_maximum_length")) & ") " & IIf(clsCommon.myCstr(row("Is_Nullable")) = "Yes", " Null", " Not null") & ""
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        isQueryExecuted = True
                    End If
                    If isQueryExecuted = False And clsCommon.CompairString(clsCommon.myCstr(dr("Data_Type")), clsCommon.myCstr(row("Data_Type"))) Then
                        qry = "Alter table " & TableName & " alter column " & clsCommon.myCstr(row.Item("COlumn_Name")) & " " & clsCommon.myCstr(row("Data_Type")) & " (" & clsCommon.myCdbl(row("character_maximum_length")) & ") " & IIf(clsCommon.myCstr(row("Is_Nullable")) = "Yes", " Null", " Not null") & ""
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        isQueryExecuted = True
                    End If
                    If isQueryExecuted = False And clsCommon.CompairString(clsCommon.myCstr(dr("character_maximum_length")), clsCommon.myCstr(row("character_maximum_length"))) Then
                        qry = "Alter table " & TableName & " alter column " & clsCommon.myCstr(row.Item("COlumn_Name")) & " " & clsCommon.myCstr(row("Data_Type")) & " (" & clsCommon.myCdbl(row("character_maximum_length")) & ") " & IIf(clsCommon.myCstr(row("Is_Nullable")) = "Yes", " Null", " Not null") & ""
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Function AddLinkedServer(ByVal ServerIP As String, ByVal DBUserID As String, ByVal dbPWD As String) As Boolean
        Try
            Dim qry As String = "IF EXISTS(SELECT * FROM sys.servers WHERE name = '" & ServerIP & "') EXEC master.sys.sp_dropserver '" & ServerIP & "' "
            If clsDBFuncationality.ExecuteNonQuery(qry) Then
                qry = "exec sp_addlinkedserver '" & ServerIP & "';"
                Return clsDBFuncationality.ExecuteNonQuery(qry)
                qry = "EXEC sp_addlinkedsrvlogin '" & ServerIP & "', FALSE, '" & DBUserID & "', '" & dbPWD & "';"
                Return clsDBFuncationality.ExecuteNonQuery(qry)

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Shared Function ExecuteNonQueryWithDropContraint(ByVal strQuery As String, ByVal TableName As String, ByVal tran As SqlTransaction) As Boolean
line1:
        Try
            Dim lstTrig As New List(Of String)
            If clsCommon.CompairString(TableName, "TSPL_INVENTORY_MOVEMENT") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_INVENTORY_MOVEMENT_NEW") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_JOURNAL_MASTER") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_JOURNAL_DETAILS") = CompairStringResult.Equal Then
            Else
                '' get active triggers on table
                lstTrig = clsHostSettings.getTableTriggers(TableName, tran)
                '' disable all trigger
                For Each trg As String In lstTrig
                    clsHostSettings.DisableTrigger(TableName, trg, tran)
                Next
            End If

            clsDBFuncationality.ExecuteNonQuery(strQuery, tran)
            If clsCommon.CompairString(TableName, "TSPL_INVENTORY_MOVEMENT") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_INVENTORY_MOVEMENT_NEW") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_JOURNAL_MASTER") = CompairStringResult.Equal OrElse clsCommon.CompairString(TableName, "TSPL_JOURNAL_DETAILS") = CompairStringResult.Equal Then
            Else
                '' enable all trigger
                For Each trg As String In lstTrig
                    clsHostSettings.EnableTrigger(TableName, trg, tran)
                Next
            End If

        Catch ex As Exception
            If ex.Message.Contains("statement conflicted with ") Then
                Dim TestArray() As String = ex.Message.Split("""")
                For i As Integer = 0 To TestArray.Length - 1
                    clsDBFuncationality.ExecuteNonQuery("Alter table " & TableName & " drop constraint " & TestArray(1), tran)
                    GoTo line1
                Next
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
        Return True
    End Function
End Class
Public Class clsSyncTables
#Region "Variables"
    Public SYNC_ID As String
    Public TABLE_NAME As String
    Public PRIMARY_KEY As String
    Public PRIMARY_KEY2 As String
    Public SEQ_NO As Double
    Public Is_Detail As Integer
    Public _Type As String
    Public _COND As String
    Public _Manual As String   '' Y or N
#End Region

    Public Function SaveData(ByVal objList As List(Of clsSyncTables), Optional ByVal DeleteExisting As Boolean = True) As Boolean
        Dim isSaved As Boolean = True
        Dim qry As String
        If DeleteExisting Then
            qry = "delete FROM TSPL_SYNC_TABLES "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        End If

        If Not objList Is Nothing AndAlso objList.Count > 0 Then
            For Each obj As clsSyncTables In objList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SEQ_NO", obj.SEQ_NO)
                clsCommon.AddColumnsForChange(coll, "TABLE_NAME", obj.TABLE_NAME)
                clsCommon.AddColumnsForChange(coll, "PRIMARY_KEY", obj.PRIMARY_KEY)
                clsCommon.AddColumnsForChange(coll, "PRIMARY_KEY2", obj.PRIMARY_KEY2)
                clsCommon.AddColumnsForChange(coll, "Is_Detail", obj.Is_Detail)
                clsCommon.AddColumnsForChange(coll, "_Type", obj._Type)
                clsCommon.AddColumnsForChange(coll, "_COND", obj._COND)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_TABLES", OMInsertOrUpdate.Insert, "")
            Next
        End If
        Return isSaved
    End Function
    Public Function UpdateNewTables(ByVal trans As SqlTransaction) As Integer
        Dim lstTables As New List(Of clsSyncTables)
        Dim lstTablesNew As New List(Of clsSyncTables)
        lstTables = GetMasterSyncTableList()
        Dim intCount As Integer = 0
        For Each obj As clsSyncTables In lstTables
            '' check existing
            Dim qry As String = "select Table_Name FROM TSPL_SYNC_TABLES where Table_Name='" & obj.TABLE_NAME & "'"
            If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
                lstTablesNew.Add(obj)
                intCount = intCount + 1
            End If
        Next
        SaveData(lstTablesNew, False)
        Return intCount
    End Function
    Public Shared Function GetData(ByVal trans As SqlTransaction) As List(Of clsSyncTables)
        Dim objTr As New clsSyncTables
        Dim objList As New List(Of clsSyncTables)
        Dim qry As String = "select * from TSPL_SYNC_TABLES order by SEQ_NO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            objTr = New clsSyncTables
            objTr.SEQ_NO = dr.Item("SEQ_NO")
            objTr.TABLE_NAME = dr.Item("TABLE_NAME")
            objTr.PRIMARY_KEY = clsCommon.myCstr(dr.Item("PRIMARY_KEY"))
            objTr.PRIMARY_KEY2 = clsCommon.myCstr(dr.Item("PRIMARY_KEY2"))
            objTr.Is_Detail = dr.Item("Is_Detail")
            objTr._Type = clsCommon.myCstr(dr.Item("_Type"))
            objTr._COND = clsCommon.myCstr(dr.Item("_COND"))
            objList.Add(objTr)
        Next
        Return objList
    End Function
    Public Shared Function GetMasterSyncTableList() As List(Of clsSyncTables)
        Dim lstTable As New List(Of clsSyncTables)
        Dim objTable As New clsSyncTables
        '' Table Name- TSPL_COUNTRY_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_COUNTRY_MASTER"
        objTable.PRIMARY_KEY = "COUNTRY_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "1"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_STATE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_STATE_MASTER"
        objTable.PRIMARY_KEY = "STATE_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "2"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_CITY_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_CITY_MASTER"
        objTable.PRIMARY_KEY = "City_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "3"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_Department_Master
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_Department_Master"
        objTable.PRIMARY_KEY = "Department_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "4"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GL_STRUCTURE
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_GL_STRUCTURE"
        objTable.PRIMARY_KEY = "Str_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "5"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_SECTION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_SECTION_MASTER"
        objTable.PRIMARY_KEY = "Section_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "6"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_ACCOUNT_MAIN_GROUPS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ACCOUNT_MAIN_GROUPS"
        objTable.PRIMARY_KEY = "Account_Main_Group_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "7"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_ACCOUNT_MAIN_GROUPS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ACCOUNT_GROUPS"
        objTable.PRIMARY_KEY = "Account_Group_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "8"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_ACCOUNT_SUB_GROUPS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ACCOUNT_SUB_GROUPS"
        objTable.PRIMARY_KEY = "Account_Sub_Group_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "9"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_OT_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_OT_MASTER"
        objTable.PRIMARY_KEY = "OT_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "10"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_ACCOUNT_MAIN_GL_ACCOUNT
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        objTable.PRIMARY_KEY = "Main_GL_Account"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "11"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_ACCOUNT_MAIN_GL_ACCOUNT
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_TDS_DEDUCTION_HEAD"
        objTable.PRIMARY_KEY = "Deduction_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "12"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)


        '' Table Name- TSPL_ATTENDANCE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ATTENDANCE_MASTER"
        objTable.PRIMARY_KEY = "Attendance_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "13"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_BONUS_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_BONUS_MASTER"
        objTable.PRIMARY_KEY = "BONUS_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "14"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_DESIGNATION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_DESIGNATION_MASTER"
        objTable.PRIMARY_KEY = "Designation_Id"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "15"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_OCCUPATION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_OCCUPATION_MASTER"
        objTable.PRIMARY_KEY = "Occupation_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "16"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_DEVISION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_DEVISION_MASTER"
        objTable.PRIMARY_KEY = "Devision_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "17"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GRADE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_GRADE_MASTER"
        objTable.PRIMARY_KEY = "Grade_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "18"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GRADE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_CAST_CATEGORY_MASTER"
        objTable.PRIMARY_KEY = "Cast_Category_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "19"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_RELIGION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_RELIGION_MASTER"
        objTable.PRIMARY_KEY = "Religion_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "20"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_LOCATION_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_LOCATION_MASTER"
        objTable.PRIMARY_KEY = "Location_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "21"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GL_ACCOUNTS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_GL_ACCOUNTS"
        objTable.PRIMARY_KEY = "Account_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "22"
        objTable.Is_Detail = "0"
        objTable._Type = "GL"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GL_ACCOUNT_PERMISSION
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_GL_ACCOUNT_PERMISSION"
        objTable.PRIMARY_KEY = "Account_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "23"
        objTable.Is_Detail = "0"
        objTable._Type = "GL"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_GL_ACCOUNT_DETAILS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_GL_ACCOUNT_DETAILS"
        objTable.PRIMARY_KEY = "Account_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "24"
        objTable.Is_Detail = "0"
        objTable._Type = "GL"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_BANK_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_BANK_MASTER"
        objTable.PRIMARY_KEY = "BANK_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "25"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_PAYROLL_ACCOUNTSETS
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PAYROLL_ACCOUNTSETS"
        objTable.PRIMARY_KEY = "Account_Set_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "26"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_VILLAGE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VILLAGE_MASTER"
        objTable.PRIMARY_KEY = "Village_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "27"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_VENDOR_GROUP
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VENDOR_GROUP"
        objTable.PRIMARY_KEY = "Ven_Group_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "28"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_PAYMENT_CYCLE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PAYMENT_CYCLE_MASTER"
        objTable.PRIMARY_KEY = "PC_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "29"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_INCENTIVE_MASTER_HEAD
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_INCENTIVE_MASTER_HEAD"
        objTable.PRIMARY_KEY = "INCENTIVE_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "30"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_INCENTIVE_DETAIL
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_INCENTIVE_DETAIL"
        objTable.PRIMARY_KEY = "INCENTIVE_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "31"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_Vendor_Bank_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_Vendor_Bank_MASTER"
        objTable.PRIMARY_KEY = "Bank_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "32"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_Charge_Category
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_Charge_Category"
        objTable.PRIMARY_KEY = "Charge_Cat_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "33"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_VENDOR_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VENDOR_MASTER"
        objTable.PRIMARY_KEY = "Vendor_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "34"
        objTable.Is_Detail = "0"
        objTable._Type = "Vendor"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_EMPLOYEE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_EMPLOYEE_MASTER"
        objTable.PRIMARY_KEY = "EMP_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "35"
        objTable.Is_Detail = "0"
        objTable._Type = "Employee"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_UNIT_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_UNIT_MASTER"
        objTable.PRIMARY_KEY = "Unit_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "36"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MCC_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MCC_MASTER"
        objTable.PRIMARY_KEY = "MCC_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "37"
        objTable.Is_Detail = "0"
        objTable._Type = "MCC"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_VLC_MASTER_HEAD
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VLC_MASTER_HEAD"
        objTable.PRIMARY_KEY = "VLC_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "38"
        objTable.Is_Detail = "0"
        objTable._Type = "VLC"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)



        '' Table Name- TSPL_VLC_MASTER_DETAIL
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VLC_MASTER_DETAIL"
        objTable.PRIMARY_KEY = "Village_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "39"
        objTable.Is_Detail = "1"
        objTable._Type = "VLC"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_PRIMARY_REASON_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PRIMARY_REASON_MASTER"
        objTable.PRIMARY_KEY = "REASON_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "40"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_Primary_Vehicle_Master
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_Primary_Vehicle_Master"
        objTable.PRIMARY_KEY = "Vehicle_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "41"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MCC_ROUTE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MCC_ROUTE_MASTER"
        objTable.PRIMARY_KEY = "Route_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "42"
        objTable.Is_Detail = "0"
        objTable._Type = "MCC"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MCC_ROUTE_VLC_MAPPING ''MIL/29/01/19-000033 by balwinder on 29/01/2019
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MCC_ROUTE_VLC_MAPPING"
        objTable.PRIMARY_KEY = "Route_CODE"
        objTable.PRIMARY_KEY2 = "VLC_CODE"
        objTable.SEQ_NO = "59"
        objTable.Is_Detail = "1"
        objTable._Type = "MCC"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)


        '' Table Name- TSPL_MILK_PRICE_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MILK_PRICE_MASTER"
        objTable.PRIMARY_KEY = "Price_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "43"
        objTable.Is_Detail = "0"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)




        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PRICE_CHART_PLANNING"
        objTable.PRIMARY_KEY = "Planning_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "44"
        objTable.Is_Detail = "0"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_FAT_SNF_UPLOADER_MCC
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PRICE_CHART_PLANNING_MCC"
        objTable.PRIMARY_KEY = "Planning_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "45"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_FAT_SNF_UPLOADER_VLC
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PRICE_CHART_PLANNING_VLC"
        objTable.PRIMARY_KEY = "Planning_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "46"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)




        '' Table Name- TSPL_FAT_SNF_UPLOADER_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_FAT_SNF_UPLOADER_MASTER"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "47"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = "" ''code in (select distinct top 2  TSPL_FAT_SNF_UPLOADER_MCC.Code from [DESKTOP-CSEQE1P].KDIL.DBO.TSPL_FAT_SNF_UPLOADER_MCC inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.MCC_CODE=TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code order by TSPL_FAT_SNF_UPLOADER_MCC.code desc)
        lstTable.Add(objTable)

        '' Table Name- TSPL_FAT_SNF_UPLOADER_MCC
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_FAT_SNF_UPLOADER_MCC"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "48"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_FAT_SNF_UPLOADER_VLC
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_FAT_SNF_UPLOADER_VLC"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "49"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- Tspl_Mcc_Reason_Master
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "Tspl_Mcc_Reason_Master"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "50"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_FAT_SNF_UPLOADER_Chart_Detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_FAT_SNF_UPLOADER_Chart_Detail"
        objTable.PRIMARY_KEY = "Charge_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "51"
        objTable.Is_Detail = "1"
        objTable._Type = "Price"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_Milk_Pump_Detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_Milk_Pump_Detail"
        objTable.PRIMARY_KEY = "Trans_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "52"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MCC_UOM_Detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MCC_UOM_Detail"
        objTable.PRIMARY_KEY = "UOM_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "53"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- tspl_Silo_detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "tspl_Silo_detail"
        objTable.PRIMARY_KEY = "Trans_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "54"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- tspl_compressor_detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "tspl_compressor_detail"
        objTable.PRIMARY_KEY = "Trans_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "55"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- tspl_gen_set_detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "tspl_gen_set_detail"
        objTable.PRIMARY_KEY = "Trans_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "56"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MCC_Cheque_Detail
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MCC_Cheque_Detail"
        objTable.PRIMARY_KEY = "Prog_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "57"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- tspl_Mcc_Employee
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "tspl_Mcc_Employee"
        objTable.PRIMARY_KEY = "Prog_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "58"
        objTable.Is_Detail = "1"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_MP_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MP_MASTER"
        objTable.PRIMARY_KEY = "MP_CODE"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "59"
        objTable.Is_Detail = "0"
        objTable._Type = "MCC"
        objTable._Manual = "N"
        objTable._COND = ""   ''vlc_code in (select vlc_Code from [DESKTOP-CSEQE1P].KDIL.dbo.tspl_vlc_Master_Head inner join tspl_milk_receipt_Head on tspl_milk_receipt_Head.mcc_Code=tspl_vlc_Master_Head.mcc)
        lstTable.Add(objTable)

        '' Table Name- TSPL_CAN_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_CAN_MASTER"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "60"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        '' Table Name- TSPL_CAN_MASTER
        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_DOCK_MASTER"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "61"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)


        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ITEM_MASTER"
        objTable.PRIMARY_KEY = "Item_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "62"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_ITEM_UOM_DETAIL"
        objTable.PRIMARY_KEY = "Item_Code"
        objTable.PRIMARY_KEY2 = "UOM_Code"
        objTable.SEQ_NO = "63"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_MILK_REJECT_TYPE"
        objTable.PRIMARY_KEY = "Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "64"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VENDOR_ACCOUNT_SET"
        objTable.PRIMARY_KEY = "Acct_Set_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "65"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_PURCHASE_ACCOUNTS"
        objTable.PRIMARY_KEY = "Purchase_Class_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "66"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)

        objTable = New clsSyncTables
        objTable.TABLE_NAME = "TSPL_VENDOR_GROUP"
        objTable.PRIMARY_KEY = "Ven_Group_Code"
        objTable.PRIMARY_KEY2 = ""
        objTable.SEQ_NO = "67"
        objTable.Is_Detail = "0"
        objTable._Type = "Common"
        objTable._Manual = "N"
        objTable._COND = ""
        lstTable.Add(objTable)


        Return lstTable
    End Function
End Class
Public Class clsSyncHeadTables
#Region "Variables"
    Public TABLE_NAME As String
    Public KEY_COLUMN As String
    Public KEY_COLUMN_2 As String
    Public KEY_COLUMN_3 As String
    Public DATE_COLUMN As String
    Public SHIFT_COLUMN As String
    Public MCC_COLUMN As String
    Public SEQ_NO As Integer
    Public SYNC_TEMP_TABLE As String
    Public SYNC_TYPE As String
    Public SYNC_START_FLAG As Integer
    Public Arr As List(Of clsSyncDetailTables)
#End Region
    Public Shared Function SaveData(ByVal obj As clsSyncHeadTables, ByVal Trans As SqlTransaction) As Boolean
        'Dim transa As SqlTransaction = clsDBFuncationality.GetTransactin(True)
        Dim isSaved As Boolean = True
        If Not obj Is Nothing Then
            Dim qry As String = "delete FROM TSPL_SYNC_HEAD_TBL where TABLE_NAME='" & obj.TABLE_NAME & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)
            qry = "delete from TSPL_SYNC_DETAIL_TBL where PARENT_TABLE_NAME='" & obj.TABLE_NAME & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TABLE_NAME", obj.TABLE_NAME)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN", obj.KEY_COLUMN)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN_2", obj.KEY_COLUMN_2)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN_3", obj.KEY_COLUMN_3)
            clsCommon.AddColumnsForChange(coll, "DATE_COLUMN", obj.DATE_COLUMN)
            clsCommon.AddColumnsForChange(coll, "SHIFT_COLUMN", obj.SHIFT_COLUMN, True)
            clsCommon.AddColumnsForChange(coll, "MCC_COLUMN", obj.MCC_COLUMN)
            clsCommon.AddColumnsForChange(coll, "SEQ_NO", obj.SEQ_NO)
            clsCommon.AddColumnsForChange(coll, "SYNC_TEMP_TABLE", obj.SYNC_TEMP_TABLE, True)
            clsCommon.AddColumnsForChange(coll, "SYNC_TYPE", obj.SYNC_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "SYNC_START_FLAG", obj.SYNC_START_FLAG)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_HEAD_TBL", OMInsertOrUpdate.Insert, "", Trans)
            '' INSERT DETAIL TABLES 
            isSaved = isSaved AndAlso clsSyncDetailTables.SaveData(obj, Trans)

        End If
        Return isSaved
    End Function

    Public Shared Function SaveBIOData(ByVal obj As clsSyncHeadTables, ByVal Trans As SqlTransaction) As Boolean
        'Dim transa As SqlTransaction = clsDBFuncationality.GetTransactin(True)
        Dim isSaved As Boolean = True
        If Not obj Is Nothing Then
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TABLE_NAME", obj.TABLE_NAME)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN", obj.KEY_COLUMN)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN_2", obj.KEY_COLUMN_2)
            clsCommon.AddColumnsForChange(coll, "KEY_COLUMN_3", obj.KEY_COLUMN_3)
            clsCommon.AddColumnsForChange(coll, "DATE_COLUMN", obj.DATE_COLUMN)
            clsCommon.AddColumnsForChange(coll, "SHIFT_COLUMN", obj.SHIFT_COLUMN, True)
            clsCommon.AddColumnsForChange(coll, "MCC_COLUMN", obj.MCC_COLUMN)
            clsCommon.AddColumnsForChange(coll, "SEQ_NO", obj.SEQ_NO)
            clsCommon.AddColumnsForChange(coll, "SYNC_TEMP_TABLE", obj.SYNC_TEMP_TABLE, True)
            clsCommon.AddColumnsForChange(coll, "SYNC_TYPE", obj.SYNC_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "SYNC_START_FLAG", obj.SYNC_START_FLAG)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BIO_SYNC_HEAD_TBL", OMInsertOrUpdate.Insert, "", Trans)
            '' INSERT DETAIL TABLES 
            'isSaved = isSaved AndAlso clsSyncDetailTables.SaveData(obj, Trans)
        End If
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal Table_Name As String, ByVal trans As SqlTransaction) As clsSyncHeadTables
        Dim obj As New clsSyncHeadTables
        Dim objList As New List(Of clsSyncDetailTables)
        Dim qry As String = "select * from TSPL_SYNC_HEAD_TBL where Table_Name='" & Table_Name & "' order by SEQ_NO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            obj = New clsSyncHeadTables
            obj.SEQ_NO = clsCommon.myCdbl(dt.Rows(0).Item("SEQ_NO"))
            obj.TABLE_NAME = clsCommon.myCstr(dt.Rows(0).Item("TABLE_NAME"))
            obj.KEY_COLUMN = clsCommon.myCstr(dt.Rows(0).Item("KEY_COLUMN"))
            obj.KEY_COLUMN_2 = clsCommon.myCstr(dt.Rows(0).Item("KEY_COLUMN_2"))
            obj.KEY_COLUMN_3 = clsCommon.myCstr(dt.Rows(0).Item("KEY_COLUMN_3"))
            obj.DATE_COLUMN = clsCommon.myCstr(dt.Rows(0).Item("DATE_COLUMN"))
            obj.SHIFT_COLUMN = clsCommon.myCstr(dt.Rows(0).Item("SHIFT_COLUMN"))
            obj.MCC_COLUMN = clsCommon.myCstr(dt.Rows(0).Item("MCC_COLUMN"))
            obj.SYNC_START_FLAG = clsCommon.myCdbl(dt.Rows(0).Item("SYNC_START_FLAG"))
            '' detail data
            obj.Arr = clsSyncDetailTables.GetList(Table_Name, trans)
        End If
        Return obj
    End Function
    Public Shared Function AutoUpdateSyncTables(ByVal trans As SqlTransaction)
        Dim IsSave As Boolean = True
        Dim obj As New clsSyncHeadTables
        Dim objTr As New clsSyncDetailTables
        Dim objList As New List(Of clsSyncDetailTables)
        Dim qry As String = ""
        Try
            qry = "delete from TSPL_SYNC_DETAIL_TBL"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SYNC_HEAD_TBL"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' TSPL_OPEN_MCC_SHIFT
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_OPEN_MCC_SHIFT"
            obj.KEY_COLUMN = "MCC_SHIFT_CODE"
            obj.DATE_COLUMN = "MCC_SHIFT_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 1
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_OPEN_MCC_SHIFT_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_MILK_PROCUREMENT_UPLOADER_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD"
            obj.KEY_COLUMN = "Document_No"
            obj.DATE_COLUMN = "Document_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 2
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0

            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL"
            objTr.PARENT_TABLE_NAME = "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD"
            objTr.KEY_COLUMN = "Document_No"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            clsSyncHeadTables.SaveData(obj, trans)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL"
            objTr.PARENT_TABLE_NAME = "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD"
            objTr.KEY_COLUMN = "Document_No"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            clsSyncHeadTables.SaveData(obj, trans)


            '' TSPL_MILK_SHIFT_UPLOADER_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_SHIFT_UPLOADER_HEAD"
            obj.KEY_COLUMN = "Document_No"
            obj.DATE_COLUMN = "Shift_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "MCC_Code"
            obj.SEQ_NO = 3
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_SHIFT_UPLOADER_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0

            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SHIFT_UPLOADER_DETAIL"
            objTr.PARENT_TABLE_NAME = "TSPL_MILK_SHIFT_UPLOADER_HEAD"
            objTr.KEY_COLUMN = "Document_No"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SHIFT_UPLOADER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            clsSyncHeadTables.SaveData(obj, trans)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL"
            objTr.PARENT_TABLE_NAME = "TSPL_MILK_SHIFT_UPLOADER_HEAD"
            objTr.KEY_COLUMN = "Document_No"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            clsSyncHeadTables.SaveData(obj, trans)


            '' TSPL_MILK_RECEIPT_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_RECEIPT_HEAD"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 4
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_RECEIPT_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_RECEIPT_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_RECEIPT_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            'clsSyncHeadTables.SaveData(obj, trans)

            '' add detail table
            'obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "LOG_CODE"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)


            '' TSPL_MILK_SAMPLE_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_SAMPLE_HEAD"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 5
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_SAMPLE_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SAMPLE_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SAMPLE_DETAIL_SYNC"
            obj.Arr.Add(objTr)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)


            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SAMPLE_READING_LOG"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "Sample_Code"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SAMPLE_READING_LOG_SYNC"
            obj.Arr.Add(objTr)
            clsSyncHeadTables.SaveData(obj, trans)



            '' TSPL_MILK_REJECT_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_REJECT_HEAD"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 6
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_REJECT_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_REJECT_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_REJECT_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)












            '' TSPL_MILK_SRN_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_SRN_HEAD"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 7
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_SRN_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SRN_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SRN_DETAIL_SYNC"
            obj.Arr.Add(objTr)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SRN_Price_Charge_Detail"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SRN_Price_Charge_Detail_SYNC"
            obj.Arr.Add(objTr)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_SRN_VSP_Charge_Detail"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 3
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_SRN_VSP_Charge_Detail_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_INVENTORY_MOVEMENT_NEW
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_INVENTORY_MOVEMENT_NEW"
            obj.KEY_COLUMN = "Source_Doc_No"
            obj.DATE_COLUMN = "Punching_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "Location_Code"
            obj.SEQ_NO = 8
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_INVENTORY_MOVEMENT_NEW_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)


            '' TSPL_MILK_Shift_End_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_MILK_Shift_End_HEAD"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 9
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_MILK_Shift_End_HEAD_SYNC"
            obj.SYNC_START_FLAG = 1
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_Shift_End_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_Shift_End_DETAIL_SYNC"
            obj.Arr.Add(objTr)

            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_MILK_Shift_End_Route_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 2
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_MILK_Shift_End_Route_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' Tspl_Milk_Truck_Sheet_Head
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "Tspl_Milk_Truck_Sheet_Head"
            obj.KEY_COLUMN = "DOC_CODE"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 10
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "Tspl_Milk_Truck_Sheet_Head_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "Tspl_Milk_Truck_Sheet_Detail"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "DOC_CODE"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "Tspl_Milk_Truck_Sheet_Detail_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_VLC_DATA_UPLOADER
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_VLC_DATA_UPLOADER"
            obj.KEY_COLUMN = "Doc_No"
            obj.DATE_COLUMN = "DOC_DATE"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "MCC_CODE"
            obj.SEQ_NO = 11
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_VLC_DATA_UPLOADER_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_VLC_DATA_UPLOADER_MASTER
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_VLC_DATA_UPLOADER_MASTER"
            obj.KEY_COLUMN = "Document_Code"
            obj.DATE_COLUMN = "Document_Date"
            obj.SHIFT_COLUMN = "SHIFT"
            obj.MCC_COLUMN = "VLC_CODE" '' SPECIAL COND WILL BE APPLIED
            obj.SEQ_NO = 12
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_VLC_DATA_UPLOADER_MASTER_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_VLC_DATA_UPLOADER_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "Document_Code"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_VLC_DATA_UPLOADER_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_PROVISION_ENTRY
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_PROVISION_ENTRY"
            obj.KEY_COLUMN = "DOC_NO"
            obj.DATE_COLUMN = "Doc_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "Loc_Code"
            obj.SEQ_NO = 13
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_PROVISION_ENTRY_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_JOURNAL_MASTER
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_JOURNAL_MASTER"
            obj.KEY_COLUMN = "Voucher_No"
            obj.DATE_COLUMN = "Voucher_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "Segment_code"  '' SPECIAL CONDITION WILL BE APPLIED
            obj.SEQ_NO = 14
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_JOURNAL_MASTER_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_JOURNAL_DETAILS"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "Voucher_No"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_JOURNAL_DETAILS_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_LOCK_MP_PC
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_LOCK_MP_PC"
            obj.KEY_COLUMN = "LOCK_CODE"
            obj.DATE_COLUMN = "TO_DATE"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = "MCC_Code"
            obj.SEQ_NO = 15
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_LOCK_MP_PC_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)



            '' TSPL_SMS_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_SMS_HEAD"
            obj.KEY_COLUMN = "Code"
            obj.DATE_COLUMN = "Created_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = ""
            obj.SEQ_NO = 16
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_SMS_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = "TSPL_SMS_DETAIL"
            objTr.PARENT_TABLE_NAME = obj.TABLE_NAME
            objTr.KEY_COLUMN = "Code"
            objTr.SEQ_NO = 1
            objTr.SYNC_TYPE = "B"
            objTr.SYNC_TEMP_TABLE = "TSPL_SMS_DETAIL_SYNC"
            obj.Arr.Add(objTr)
            '' end detail tables
            clsSyncHeadTables.SaveData(obj, trans)

            '' TSPL_EMAIL_HEAD
            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_EMAIL_HEAD"
            obj.KEY_COLUMN = "Code"
            obj.DATE_COLUMN = "Created_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = ""
            obj.SEQ_NO = 17
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_EMAIL_HEAD_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            obj.Arr = New List(Of clsSyncDetailTables)
            clsSyncHeadTables.SaveData(obj, trans)



            'obj = New clsSyncHeadTables
            'obj.TABLE_NAME = "TSPL_BIOMETRIC_RAW_DATA"
            'obj.KEY_COLUMN = "Machine_Sr_No"
            'obj.KEY_COLUMN_2 = "Emp_ID"
            'obj.KEY_COLUMN_3 = "In_Out_Date"
            'obj.DATE_COLUMN = "In_Out_Date"
            'obj.SHIFT_COLUMN = ""
            'obj.MCC_COLUMN = ""
            'obj.SEQ_NO = 18
            'obj.SYNC_TYPE = "B"
            'obj.SYNC_TEMP_TABLE = "TSPL_BIOMETRIC_RAW_DATA_SYNC"
            'obj.SYNC_START_FLAG = 0
            '''' add detail table
            'obj.Arr = New List(Of clsSyncDetailTables)
            'clsSyncHeadTables.SaveData(obj, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return IsSave
    End Function

    Public Shared Function AutoUpdateBioSyncTables(ByVal trans As SqlTransaction)
        Dim IsSave As Boolean = True
        Dim obj As New clsSyncHeadTables
        Dim objTr As New clsSyncDetailTables
        Dim objList As New List(Of clsSyncDetailTables)
        Dim qry As String = ""
        Try
            qry = "delete from TSPL_BIO_SYNC_HEAD_TBL"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            obj = New clsSyncHeadTables
            obj.TABLE_NAME = "TSPL_BIOMETRIC_RAW_DATA"
            obj.KEY_COLUMN = "Machine_Sr_No"
            obj.KEY_COLUMN_2 = "Emp_ID"
            obj.KEY_COLUMN_3 = "In_Out_Date"
            obj.DATE_COLUMN = "In_Out_Date"
            obj.SHIFT_COLUMN = ""
            obj.MCC_COLUMN = ""
            obj.SEQ_NO = 1
            obj.SYNC_TYPE = "B"
            obj.SYNC_TEMP_TABLE = "TSPL_BIOMETRIC_RAW_DATA_SYNC"
            obj.SYNC_START_FLAG = 0
            '' add detail table
            'obj.Arr = New List(Of clsSyncDetailTables)
            clsSyncHeadTables.SaveBIOData(obj, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return IsSave
    End Function
    Public Shared Function ConfigureSynchronization(ByVal MCC_Code As String, ByVal Start_Date As Date, ByVal ManualTable As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Dim isSaved As Boolean = True
        clsCommon.ProgressBarShow()
        Try
            Dim Qry As String
            Dim dt As DataTable
            Dim objHost As clsHostSettings = clsHostSettings.GetData("Server", trans)
            Dim strServer As String = "[" & objHost.SERVER_NAME_IP & "]" & "." & objHost.DATABASE_NAME & "." & objHost.SCHEMA_NAME
            'Dim coll As Dictionary(Of String, String)
            Dim table_name As String = ""
            Dim DATE_COLUMN As String = ""
            If ManualTable = False Then
                clsSyncHeadTables.AutoUpdateSyncTables(trans)
                clsSyncHeadTables.AutoUpdateBioSyncTables(trans)
            End If
            Qry = "select TABLE_NAME,KEY_COLUMN,DATE_COLUMN,SEQ_NO from TSPL_SYNC_HEAD_TBL ORDER BY SEQ_NO"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            For Each dr As DataRow In dt.Rows
                clsCommon.ProgressBarUpdate("Processing tables :" & (dt.Rows.IndexOf(dr) + 1) & "/" & dt.Rows.Count)
                table_name = clsCommon.myCstr(dr.Item("TABLE_NAME"))
                DATE_COLUMN = clsCommon.myCstr(dr.Item("DATE_COLUMN"))
                '' local server
                If clsPostCreateTable.CheckColumnExist(table_name, "SYNC_STATUS", DBDataType.int_Type, 0, 0, trans) = False Then
                    Qry = String.Empty
                    Qry = "ALTER TABLE " & table_name & " ADD SYNC_STATUS INT NULL"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If

                ' '' remote server
                'If clsPostCreateTable.CheckColumnExist(strServer & "." & table_name, "SYNC_STATUS", DBDataType.int_Type, 0, 0, trans) = False Then
                '    Qry = String.Empty
                '    Qry = "ALTER TABLE " & strServer & "." & table_name & " ADD SYNC_STATUS INT NULL"
                '    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                'End If

                '' update SYNC_STATUS=0 AFTER START DATE
                If clsCommon.CompairString(table_name, "TSPL_INVENTORY_MOVEMENT_NEW") = CompairStringResult.Equal Then
                    Qry = "UPDATE " & table_name & " SET SYNC_STATUS=0 WHERE CAST(" & DATE_COLUMN & " as date)>='" & clsCommon.GetPrintDate(Start_Date, "dd-MMM-yyyy") & "' "
                Else
                    Qry = "UPDATE " & table_name & " SET SYNC_STATUS=0 WHERE CAST(" & DATE_COLUMN & " as date)>='" & clsCommon.GetPrintDate(Start_Date, "dd-MMM-yyyy") & "' "
                End If
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Next
            Qry = "delete from TSPL_SYNC_REINIT "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "insert into TSPL_SYNC_REINIT(MCC_Code,REINIT_DATE,MANUAL_TABLE) VALUES('" & MCC_Code & "','" & clsCommon.GetPrintDate(Start_Date, "dd-MMM-yyyy") & "','" & If(ManualTable = True, 1, 0) & "')"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function SynchronizeData() As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Dim objHost As clsHostSettings = clsHostSettings.GetData("Server", trans)
        Dim strServer As String = "[" & objHost.SERVER_NAME_IP & "]" & "." & objHost.DATABASE_NAME & "." & objHost.SCHEMA_NAME
        Dim objLog As New clsSyncLog
        Dim isSaved As Boolean = True
        'Dim strClient As String = ""
        Try
            Dim Qry As String
            Qry = "select MCC_Code,max(REINIT_DATE) as REINIT_DATE from TSPL_SYNC_REINIT group by MCC_Code"
            Dim dtReinit As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            For Each drMCC As DataRow In dtReinit.Rows
                Dim dt As DataTable
                Qry = String.Empty
                Qry = "select TABLE_NAME,KEY_COLUMN,DATE_COLUMN,SEQ_NO from TSPL_SYNC_HEAD_TBL ORDER BY SEQ_NO"
                dt = clsDBFuncationality.GetDataTable(Qry, trans)
                Dim table_name As String = ""
                Dim KEY_COLUMN As String = ""
                Dim DATE_COLUMN As String = ""
                Dim SYNC_ID As String = ""
                Dim ReInitDate As Date = clsCommon.myCDate(drMCC.Item("REINIT_DATE"))
                Dim Key_Doc_No As String = ""
                Dim IsNewEntry As Boolean = False

                For Each dr As DataRow In dt.Rows
                    '' map table name ,date colum and key column
                    table_name = clsCommon.myCstr(dr.Item("TABLE_NAME"))
                    DATE_COLUMN = clsCommon.myCstr(dr.Item("DATE_COLUMN"))
                    KEY_COLUMN = clsCommon.myCstr(dr.Item("KEY_COLUMN"))
                    ''MAINTAIN  log

                    objLog = New clsSyncLog
                    objLog.MCC_Code = clsCommon.myCstr(drMCC.Item("MCC_Code"))
                    objLog.TABLE_NAME = table_name
                    objLog.MACHINE_NAME = ""
                    objLog.SYNC_BY = "admin"
                    objLog.SYNC_TYPE = "T"
                    objLog.SYNC_START_DATE = clsCommon.GETSERVERDATE(trans)
                    objLog.SYNC_ID = SYNC_ID
                    clsSyncLog.SaveData(objLog, trans)
                    SYNC_ID = objLog.SYNC_ID

                    '' get list of all the columns in table
                    Dim strSelect As String = clsHostSettings.GetTableColumnsForTransSyncSelect(table_name, trans)
                    Dim strUpdateCols As String = clsHostSettings.GetTableColumnsForSyncUpdate(table_name, KEY_COLUMN, trans)
                    '' get data of all the tables after reinit date that are not synchronized
                    Qry = String.Empty
                    If clsCommon.CompairString(table_name, "TSPL_INVENTORY_MOVEMENT_NEW") = CompairStringResult.Equal Then
                        Qry = "select " & KEY_COLUMN & " from " & table_name & " where CONVERT(DATE," & DATE_COLUMN & ",105)>='" & clsCommon.GetPrintDate(ReInitDate, "dd-MMM-yyyy") & "'"
                    Else
                        Qry = "select " & KEY_COLUMN & " from " & table_name & " where cast(" & DATE_COLUMN & " as date)>='" & clsCommon.GetPrintDate(ReInitDate, "dd-MMM-yyyy") & "'"
                    End If
                    Dim dtData As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                    For Each drData As DataRow In dtData.Rows
                        Key_Doc_No = clsCommon.myCstr(dr.Item(KEY_COLUMN))
                        '' check that this doc no exists on server or not
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(" & KEY_COLUMN & ") as total from " & strServer & "." & table_name & " where " & KEY_COLUMN & "='" & Key_Doc_No & "'", trans)) > 0 Then
                            IsNewEntry = False
                        Else
                            IsNewEntry = True
                        End If
                        Qry = String.Empty
                        Qry = "select IDENT_CURRENT( '" & table_name & "' )"
                        If clsCommon.myLen(clsDBFuncationality.getSingleValue(Qry, trans)) > 0 Then
                            Qry = "set identity_insert " & table_name & " On ;"
                        Else
                            Qry = ""
                        End If

                        Qry = String.Empty
                        If IsNewEntry Then
                            Qry = Qry & " insert into  " & strServer & "." & table_name & " (" & strSelect & ") select " & strSelect & " from " & table_name & " where " & KEY_COLUMN & "='" & Key_Doc_No & "' "
                        Else
                            Qry = Qry & " update  " & strServer & "." & table_name & "  set " & strUpdateCols & " from " & table_name & " TA where TA." & KEY_COLUMN & "='" & Key_Doc_No & "' "
                        End If
                        '' update server header data
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        '' get detail tables
                        Qry = String.Empty
                        Qry = "select TABLE_NAME as Detail_Table_Name,KEY_COLUMN as Detail_Key_Column from TSPL_SYNC_DETAIL_TBL where PARENT_TABLE_NAME='" & table_name & "' order by Seq_No"
                        Dim dtDtl As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                        Dim Detail_Table_Name As String = String.Empty
                        Dim Detail_Key_Column As String = String.Empty
                        For Each drDtl As DataRow In dtDtl.Rows
                            '' delete existing data agaist the doc no from remote server
                            Qry = String.Empty
                            Qry = "delete from " & strServer & "." & Detail_Table_Name & " where " & Detail_Key_Column & "='" & Key_Doc_No & "'"
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                            '' get column list of detail table
                            Qry = String.Empty
                            strSelect = String.Empty
                            strSelect = clsHostSettings.GetTableColumnsForSelect(Detail_Table_Name, Detail_Key_Column, "", trans)
                            ''insert new data from mcc server to remote server
                            Qry = Qry & " insert into  " & strServer & "." & Detail_Table_Name & " (" & strSelect & ") select " & strSelect & " from " & Detail_Table_Name & " where " & Detail_Key_Column & "='" & Key_Doc_No & "' "
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Next
                        '' release memory
                        dtDtl = Nothing
                        Detail_Table_Name = String.Empty
                        Detail_Key_Column = String.Empty
                    Next
                    '' update SYNC_STATUS=0 AFTER START DATE
                    Qry = String.Empty
                    Qry = "update " & table_name & " set SYNC_STATUS=1 where " & KEY_COLUMN & "='" & Key_Doc_No & "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    '' Maintain log
                    objLog.SYNC_END_DATE = clsCommon.GETSERVERDATE(trans)
                    objLog.ERROR_LOG = "Successfully Synchronized"
                    isSaved = isSaved AndAlso clsSyncLog.SaveData(objLog, trans)
                    objLog = Nothing
                    '' end loop for header tables
                Next
                '' Release memory
                table_name = String.Empty
                KEY_COLUMN = String.Empty
                DATE_COLUMN = String.Empty
                SYNC_ID = String.Empty
                ReInitDate = Nothing
                Key_Doc_No = String.Empty
                '' end loop for mcc
            Next
            objHost = Nothing
            strServer = String.Empty

            '' commit transaction
            trans.Commit()
        Catch ex As Exception
            '' Maintain log
            objLog.SYNC_END_DATE = clsCommon.GETSERVERDATE(trans)
            objLog.ERROR_LOG = ex.Message
            isSaved = isSaved AndAlso clsSyncLog.SaveData(objLog, trans)
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveSyncDelete(ByVal Table_Name As String, ByVal Doc_No As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TABLE_NAME", Table_Name)
            clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_DELETE", OMInsertOrUpdate.Insert, "", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteFromServer(ByVal strServer As String) As Boolean
        Dim qry As String = " select TSPL_SYNC_DELETE.TABLE_NAME,TSPL_SYNC_DELETE.DOC_NO,TSPL_SYNC_HEAD_TBL.KEY_COLUMN from TSPL_SYNC_DELETE " & _
                            " inner join TSPL_SYNC_HEAD_TBL on TSPL_SYNC_DELETE.TABLE_NAME=TSPL_SYNC_HEAD_TBL.TABLE_NAME order by TSPL_SYNC_HEAD_TBL.SEQ_NO desc"
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Table_Name As String = ""
            Dim Doc_No As String = ""
            Dim dt As DataTable
            Dim dtD As DataTable

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Table_Name = clsCommon.myCstr(dr.Item("TABLE_NAME"))
                Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                qry = "select TABLE_NAME,PARENT_TABLE_NAME,KEY_COLUMN from TSPL_SYNC_DETAIL_TBL where PARENT_TABLE_NAME='" & Table_Name & "' order by SEQ_NO desc"
                dtD = clsDBFuncationality.GetDataTable(qry, trans)
                For Each drD As DataRow In dtD.Rows
                    qry = "delete from " & strServer & "." & clsCommon.myCstr(drD.Item("TABLE_NAME")) & " where " & clsCommon.myCstr(drD.Item("TABLE_NAME")) & "." & clsCommon.myCstr(drD.Item("KEY_COLUMN")) & "='" & Doc_No & "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
                ' '' get key column of header table
                'qry = "select KEY_COLUMN from TSPL_SYNC_HEAD_TBL where TABLE_NAME='" & Table_Name & "'"
                Dim Key_Col As String = clsCommon.myCstr(dr.Item("KEY_COLUMN")) ''clsDBFuncationality.getSingleValue(qry, trans)
                qry = "delete from " & strServer & "." & clsCommon.myCstr(dr.Item("TABLE_NAME")) & " where " & clsCommon.myCstr(dr.Item("TABLE_NAME")) & "." & Key_Col & "='" & Doc_No & "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next
            '' finally empty TSPL_SYNC_DELETE
            qry = "delete from TSPL_SYNC_DELETE"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsSyncDetailTables
#Region "Variables"
    Public TABLE_NAME As String
    Public PARENT_TABLE_NAME As String
    Public KEY_COLUMN As String = ""
    Public SEQ_NO As Integer
    Public SYNC_TEMP_TABLE As String
    Public SYNC_TYPE As String
#End Region
    Public Shared Function SaveData(ByVal obj As clsSyncHeadTables, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        If Not obj Is Nothing AndAlso obj.Arr.Count > 0 Then
            Dim qry As String = "delete FROM TSPL_SYNC_DETAIL_TBL where PARENT_TABLE_NAME='" & obj.TABLE_NAME & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)
            For Each objTr As clsSyncDetailTables In obj.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TABLE_NAME", objTr.TABLE_NAME)
                clsCommon.AddColumnsForChange(coll, "KEY_COLUMN", objTr.KEY_COLUMN)
                clsCommon.AddColumnsForChange(coll, "PARENT_TABLE_NAME", objTr.PARENT_TABLE_NAME)
                clsCommon.AddColumnsForChange(coll, "SEQ_NO", objTr.SEQ_NO)
                clsCommon.AddColumnsForChange(coll, "SYNC_TEMP_TABLE", objTr.SYNC_TEMP_TABLE, True)
                clsCommon.AddColumnsForChange(coll, "SYNC_TYPE", objTr.SYNC_TYPE, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_DETAIL_TBL", OMInsertOrUpdate.Insert, "", Trans)
            Next

        End If
        Return isSaved
    End Function
    Public Shared Function GetList(ByVal Parent_Table_Name As String, ByVal Trans As SqlTransaction) As List(Of clsSyncDetailTables)
        Dim objTr As New clsSyncDetailTables
        Dim objList As New List(Of clsSyncDetailTables)
        Dim qry As String = "select * from TSPL_SYNC_TABLES_TRANS where PARENT_TABLE_NAME='" & Parent_Table_Name & "' order by SEQ_NO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        For Each dr As DataRow In dt.Rows
            objTr = New clsSyncDetailTables
            objTr.TABLE_NAME = clsCommon.myCstr(dr.Item("TABLE_NAME"))
            objTr.PARENT_TABLE_NAME = clsCommon.myCstr(dr.Item("PARENT_TABLE_NAME"))
            objTr.KEY_COLUMN = clsCommon.myCstr(dr.Item("KEY_COLUMN"))
            objTr.SEQ_NO = clsCommon.myCdbl(dr.Item("SEQ_NO"))
            objList.Add(objTr)
        Next
        Return objList
    End Function
End Class
Public Class clsSyncLog
#Region "Variables"
    Public SYNC_ID As String
    Public MCC_Code As String
    Public TABLE_NAME As String
    Public SYNC_TYPE As String
    Public SYNC_START_DATE As Date
    Public SYNC_BY As String
    Public MACHINE_NAME As String
    Public SYNC_END_DATE As Date?
    Public ERROR_LOG As String

#End Region
    Public Shared Function SaveData(ByVal obj As clsSyncLog, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim isNewEntry As Boolean = False
        Dim qry As String = ""
        If Not obj Is Nothing Then
            Dim coll As New Hashtable()
            If clsCommon.myLen(obj.SYNC_ID) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
                qry = "select max(SYNC_ID) AS SYNC_ID from TSPL_SYNC_LOG where MCC_CODE='" & obj.MCC_Code & "'"
                Dim SYNC_ID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Trans))
                If clsCommon.myLen(SYNC_ID) > 0 Then
                    obj.SYNC_ID = clsCommon.incval(SYNC_ID)
                Else
                    obj.SYNC_ID = obj.MCC_Code & "/" & "0000001"
                End If
            End If

            clsCommon.AddColumnsForChange(coll, "SYNC_ID", obj.SYNC_ID)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "TABLE_NAME", obj.TABLE_NAME)
            clsCommon.AddColumnsForChange(coll, "SYNC_TYPE", obj.SYNC_TYPE)
            clsCommon.AddColumnsForChange(coll, "SYNC_START_DATE", clsCommon.GetPrintDate(obj.SYNC_START_DATE, "dd-MMM-yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SYNC_BY", obj.SYNC_BY)
            clsCommon.AddColumnsForChange(coll, "MACHINE_NAME", obj.MACHINE_NAME)
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If Not obj.SYNC_END_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "SYNC_END_DATE", clsCommon.GetPrintDate(obj.SYNC_END_DATE, "dd-MMM-yyyy hh:mm:ss tt"))
            End If

            clsCommon.AddColumnsForChange(coll, "ERROR_LOG", obj.ERROR_LOG)
            If isNewEntry = True Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_LOG", OMInsertOrUpdate.Insert, "", Trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SYNC_LOG", OMInsertOrUpdate.Update, "SYNC_ID='" & obj.SYNC_ID & "'", Trans)
            End If
        End If
        Return isSaved
    End Function

End Class