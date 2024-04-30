Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
''TEC/02/05/18-000221 by balwinder on 09/05/2018
'' Work to be done agaist ticket no. BHA/08/08/18-000396
' Ticket : TEC/28/06/19-000575 By Prabhakar 
'Ticket No-VIJ/27/12/19-000128 ,sanjay
'Sanjay PTM-Add column Care Of,Aadhar No
Public Class frmImplementImportExport
    Inherits FrmMainTranScreen
    'Inherits Telerik.WinControls.UI.RadForm
    Dim qry As String
    Dim AllowItemConversionAutomation As Integer = 0

    Private Sub RadButton87_Click(sender As Object, e As EventArgs) Handles RadButton87.Click
        Try
            Me.Text = "GL Option"
            qry = "Select Seg_No as [SNo], Seg_Name as [Name], Seg_Length as [Length],case when seg_useinclosing='Y' then 'Y' else 'N' end as [Closing(Y/N)],case when Report_Filters=1 then 'Y' else 'N' end as [Report Filter(Y/N)]  from TSPL_GL_SEGMENT"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                qry = "select * from (Select '1' as [SNo], 'Accounts' as [Name], 6 as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)] " + _
                " union all  Select '2' as [SNo], '' as [Name], null as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)]  " + _
                " union all Select '3' as [SNo], '' as [Name], null as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)]  " + _
                " union all Select '4' as [SNo], '' as [Name], null as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)]  " + _
                " union all Select '5' as [SNo], '' as [Name], null as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)]  " + _
                " union all Select '6' as [SNo], '' as [Name], null as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)]  " + _
                " union all Select '7' as [SNo], 'Location' as [Name], 3 as [Length],'N' as  [Closing(Y/N)],'N' as [Report Filter(Y/N)] )xx "
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "GL Option")
        End Try
    End Sub

    Private Sub RadButton86_Click(sender As Object, e As EventArgs) Handles RadButton86.Click
        Dim gv As New RadGridView
        Me.Controls.Add(gv)
        Dim SNO As Integer = 0
        Try
            If transportSql.importExcel(gv, "SNo", "Name", "Length", "Closing(Y/N)", "Report Filter(Y/N)") Then
                Try
                    Dim obj As New clsGLSegment
                    Dim arr As New List(Of clsGLSegment)
                    clsCommon.ProgressBarPercentShow()
                    For Each gvRow As GridViewRowInfo In gv.Rows
                        SNO += 1
                        clsCommon.ProgressBarPercentUpdate((gvRow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (gvRow.Index + 1) & "/" & gv.Rows.Count & "")
                        obj = New clsGLSegment
                        obj.Seg_No = clsCommon.myCdbl(gvRow.Cells("SNo").Value)
                        obj.Seg_Name = clsCommon.myCstr(gvRow.Cells("Name").Value)
                        obj.Seg_Length = clsCommon.myCdbl(gvRow.Cells("Length").Value)
                        If clsCommon.CompairString(clsCommon.myCstr(gvRow.Cells("Closing(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                            obj.seg_useinclosing = True
                        Else
                            obj.seg_useinclosing = False
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gvRow.Cells("Report Filter(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                            obj.Report_Filters = True
                        Else
                            obj.Report_Filters = False
                        End If
                        arr.Add(obj)
                    Next
                    SNO += 1
                    clsCommon.ProgressBarPercentHide()
                    obj.SaveData(arr)
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                    Try
                        Dim isRecExit As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_GLSETTING"))
                        If isRecExit = False Then
                            connectSql.RunSpTransaction(Nothing, "sp_TSPL_GLSETTING_insert", New SqlParameter("@funcurrency", "INR"), New SqlParameter("@multicurrency", "N"), New SqlParameter("@ratetype", ""), New SqlParameter("@accountgroup", "N"), New SqlParameter("@closing_account", ""), New SqlParameter("@postprevious", "N"), New SqlParameter("@provisional_posting", "N"), New SqlParameter("@sourcecode", ""), New SqlParameter("@accountsegment", "1"), New SqlParameter("@structurecode", ""), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(Nothing)), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate(Nothing)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                        Else
                            clsDBFuncationality.ExecuteNonQuery(" update TSPL_GLSETTING set Account_Segment =1 ")
                        End If
                    Catch ex As Exception

                    End Try
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    If SNO <= 7 Then
                        Throw New Exception("Error at Row No " + clsCommon.myCstr(SNO) + Environment.NewLine + ex.Message)
                    Else
                        Throw New Exception(ex.Message)
                    End If
                End Try
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "GL Option")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            Me.Text = "Segment Code"
            Dim query As String = "Select seg_no as 'Segment No',segment_name as 'Segment Name',segment_code as 'Segment Code',Description as 'Description',Account_code as 'Account Code',GIT,state_Code as [State Code] from tspl_gl_segment_code"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Segment Code")
        End Try

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Segment No", "Segment Name", "Segment Code", "Description", "Account Code", "GIT", "State Code") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim LineNo As Integer = 0
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        LineNo += 1
                        Dim strsegmentno As String = clsCommon.myCstr(dgrv.Cells("Segment No").Value)
                        Dim strsegmentname As String = clsCommon.myCstr(dgrv.Cells("Segment Name").Value)
                        Dim strsegmentcode As String = clsCommon.myCstr(dgrv.Cells("Segment Code").Value)
                        Dim strdescription As String = clsCommon.myCstr(dgrv.Cells("Description").Value)
                        Dim straccountcode As String = clsCommon.myCstr(dgrv.Cells("Account Code").Value)
                        Dim strGIT As String = clsCommon.myCstr(dgrv.Cells("GIT").Value)
                        Dim strStateCode As String = clsCommon.myCstr(dgrv.Cells("State Code").Value)

                        If String.IsNullOrEmpty(strsegmentno) Or clsCommon.myCstr(strsegmentno) > 10 Then
                            Throw New Exception("Segment Name has some incorrect values")
                        End If

                        If clsCommon.myLen(strGIT) > 0 Then
                            If clsCommon.CompairString(strGIT.ToUpper().Trim(), "Y") = CompairStringResult.Equal Or clsCommon.CompairString(strGIT.ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("GIT should be between 'Y' or 'N'.")
                            End If
                        End If

                        If clsCommon.CompairString(strsegmentno, "7") = CompairStringResult.Equal Then
                            If String.IsNullOrEmpty(strStateCode) = True Then
                                Throw New Exception("State Code Can't be blank for Segment Code : " + strsegmentcode + "")
                            End If
                            Dim isValidStateCode As Boolean = clsDBFuncationality.getSingleValue("select count (*) from tspl_state_Master where state_Code = '" + strStateCode + "'", trans)
                            If isValidStateCode = False Then
                                Throw New Exception("Invalid State Code For Segment Code : " + strsegmentcode + "")
                            End If
                        Else
                            strStateCode = ""
                        End If

                        Dim sql1 As String = "select count(*)from tspl_gl_segment_code where seg_no='" + strsegmentno + "'and segment_name='" + strsegmentname + "'and segment_code='" + strsegmentcode + "'"
                        Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                        If (i = 0) Then
                            connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", strsegmentno), New SqlParameter("@segmentname", strsegmentname), New SqlParameter("@segmentcode", strsegmentcode), New SqlParameter("@desc", strdescription), New SqlParameter("@acccode", straccountcode), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                            'clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "' WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                        Else
                            connectSql.RunSpTransaction(trans, "sp_tspl_gl_segment_update", New SqlParameter("@segno", strsegmentno), New SqlParameter("@segmentname", strsegmentname), New SqlParameter("@segmentcode", strsegmentcode), New SqlParameter("@desc", strdescription), New SqlParameter("@acccode", straccountcode))
                            ' clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "' WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                        End If
                        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_CODE SET GIT='" & strGIT & "', STATE_CODE =  " + IIf(clsCommon.myLen(strStateCode) > 0, "'" + strStateCode + "'", "null") + " WHERE Seg_No=" & strsegmentno & " and Segment_name ='" & strsegmentname & "' and Segment_code ='" & strsegmentcode & "'", trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no" + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            Me.Text = "Location Master"
            qry = "SELECT TSPL_LOCATION_MASTER.Location_Code as LocationCode,TSPL_LOCATION_MASTER.Location_Desc as LocationDesc,tspl_location_master.Loc_Short_Name as LocationShortName,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.City_Code as CityCode,TSPL_LOCATION_MASTER.State,TSPL_LOCATION_MASTER.Pin_Code as PINCode ,TSPL_LOCATION_MASTER.Country,TSPL_LOCATION_MASTER.Telphone,TSPL_LOCATION_MASTER.Email,TSPL_LOCATION_MASTER.Location_Type as LocationType,TSPL_LOCATION_MASTER.Loc_Segment_Code AS LocationSegmentCode,TSPL_LOCATION_MASTER.Registration_Number AS RegistrationNumber,TSPL_LOCATION_MASTER.Is_Section as [Section(Y/N)],Is_Sub_Location as [SubLocation(Y/N)],CASE When Is_Section ='Y' THEN  Section_Code else '' end AS SectionCode,CASE When Is_Section  ='Y' OR Is_Sub_Location  ='Y' THEN  Main_Location_Code  else '' end AS MainLocationCode,case when tspl_location_master.is_consumption_location=0 then 'N' else 'Y' end as [ConsumptionLocation(Y/N)],tspl_location_master.IsSubLocationWise as [SubLocationWise(Y/N)],tspl_location_master.GSTNo,IsMainPlant as [IsMainPlant(0/1)] FROM TSPL_LOCATION_MASTER"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Location Master")
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "LocationCode", "LocationDesc", "LocationShortName", "Add1", "CityCode", "State", "PINCode", "Country", "Telphone", "Email", "LocationType", "LocationSegmentCode", "RegistrationNumber", "Section(Y/N)", "SubLocation(Y/N)", "SectionCode", "MainLocationCode", "ConsumptionLocation(Y/N)", "SubLocationWise(Y/N)", "GSTNo", "IsMainPlant(0/1)") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim linno As Integer = 0
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        linno += 1
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("LocationCode").Value)) > 0 Then
                            Dim obj As New clsLocation
                            obj.Location_Code = clsCommon.myCstr(grow.Cells("LocationCode").Value)
                            obj.Location_Desc = clsCommon.myCstr(grow.Cells("LocationDesc").Value)
                            obj.Short_Name = clsCommon.myCstr(grow.Cells("LocationShortName").Value)
                            obj.Add1 = clsCommon.myCstr(grow.Cells("Add1").Value)
                            obj.City_Code = clsCommon.myCstr(grow.Cells("CityCode").Value)
                            obj.State = clsCommon.myCstr(grow.Cells("State").Value)
                            If clsCommon.myLen(obj.State) > 0 Then
                                obj.State = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_code from TSPL_STATE_MASTER where state_code='" & obj.State & "'", trans))
                                If clsCommon.myLen(obj.State) <= 0 Then
                                    Throw New Exception("Invalid State/Province Code  ")
                                End If
                            Else
                                Throw New Exception("State/Province cannot be blank ")
                            End If
                            obj.Pin_Code = clsCommon.myCstr(grow.Cells("PINCode").Value)
                            obj.Country = clsCommon.myCstr(grow.Cells("Country").Value)
                            obj.Telphone = clsCommon.myCstr(grow.Cells("Telphone").Value)
                            obj.Email = clsCommon.myCstr(grow.Cells("Email").Value)
                            If clsCommon.myLen(obj.Email) > 0 Then
                                Dim re As Regex = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                                If Not re.IsMatch(obj.Email) Then
                                    Throw New Exception("Email has some incorrect values ")
                                End If
                            End If

                            obj.Location_Type = clsCommon.myCstr(grow.Cells("LocationType").Value)
                            obj.Loc_Segment_Code = clsCommon.myCstr(grow.Cells("LocationSegmentCode").Value)
                            If clsCommon.myLen(obj.Loc_Segment_Code) <= 0 Then
                                Throw New Exception("Please enter Location segment")
                            End If
                            ' qry = "select * from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_name='Location' and Segment_code='" + obj.Loc_Segment_Code + "'"
                            Dim isValidSegCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_GL_SEGMENT_CODE where Segment_code='" + obj.Loc_Segment_Code + "' ", trans))
                            'obj.Loc_Segment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If isValidSegCode = False Then
                                Throw New Exception("Invalid Location segment")
                            End If

                            If clsCommon.CompairString(obj.Location_Type, "Physical") = CompairStringResult.Equal Then
                                If clsCommon.myLen(obj.Loc_Segment_Code) <= 0 Then
                                    Throw New Exception("There Must be Location Segment Code for Location Type Physical ")
                                End If
                            ElseIf clsCommon.CompairString(obj.Location_Type, "Logical") = CompairStringResult.Equal Then
                                'If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Excisable").Value.ToString()), "T") = CompairStringResult.Equal Then
                                '    Throw New Exception("Logical Location Can not be Excisable  ")
                                'End If
                            ElseIf clsCommon.CompairString(obj.Location_Type, "WorkOrder") = CompairStringResult.Equal Then
                            ElseIf clsCommon.CompairString(obj.Location_Type, "Virtual") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("Location Type must be either Physical/ Logical/ WorkOrder or Virtual. ")
                            End If

                            obj.Registration_Number = clsCommon.myCstr(grow.Cells("RegistrationNumber").Value)
                            obj.Is_Section = clsCommon.myCstr(grow.Cells("Section(Y/N)").Value)
                            obj.Is_Sub_Location = clsCommon.myCstr(grow.Cells("SubLocation(Y/N)").Value)
                            obj.Section_Code = clsCommon.myCstr(grow.Cells("SectionCode").Value)
                            obj.Main_Location_Code = clsCommon.myCstr(grow.Cells("MainLocationCode").Value)
                            obj.Is_Consumption_Location = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("ConsumptionLocation(Y/N)").Value), "Y") = CompairStringResult.Equal, 1, 0)

                            If Not (clsCommon.CompairString(obj.Is_Section, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Is_Section, "N") = CompairStringResult.Equal) Then
                                Throw New Exception(" Section(Y/N) should be Y/N for " + obj.Location_Code)
                            Else
                                obj.Is_Section = obj.Is_Section.ToUpper()
                            End If
                            If Not (clsCommon.CompairString(obj.Is_Sub_Location, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Is_Sub_Location, "N") = CompairStringResult.Equal) Then
                                Throw New Exception(" SubLocation(Y/N) should be Y/N for " + obj.Location_Code)
                            Else
                                obj.Is_Sub_Location = obj.Is_Sub_Location.ToUpper()
                            End If
                            If clsCommon.CompairString(obj.Is_Section, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Sub_Location, "Y") = CompairStringResult.Equal Then
                                Throw New Exception("Please Check ! Either Section Or Sub Location Should be 'Y'. for " + obj.Location_Code)
                            End If

                            If clsCommon.myLen(obj.Section_Code) > 0 Then
                                obj.Section_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT Section_Code  FROM TSPL_SECTION_MASTER where Section_Code='" & obj.Section_Code & "'", trans))
                                If clsCommon.myLen(obj.Section_Code) <= 0 Then
                                    Throw New Exception("Invalid Section.Please make it entry first. for  " + obj.Location_Code)
                                End If
                            End If

                            If clsCommon.myLen(obj.Main_Location_Code) > 0 Then
                                obj.Main_Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Code  FROM TSPL_LOCATION_MASTER where Location_Code='" & obj.Main_Location_Code & "'", trans))
                                If clsCommon.myLen(obj.Main_Location_Code) <= 0 Then
                                    Throw New Exception("Invalid Main Location.Please make it entry first.For " + obj.Location_Code)
                                End If
                                If clsCommon.CompairString(obj.Location_Code, obj.Main_Location_Code) = CompairStringResult.Equal Then
                                    Throw New Exception("Location code and main location code can not be same.For " + obj.Location_Code)
                                End If

                                If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_LOCATION_MASTER where Location_Code='" & obj.Main_Location_Code & "' AND Is_Section ='N' AND Is_Sub_Location='N' ", trans) = 0 Then
                                    Throw New Exception("Please check ! main location is either section or sub location.For " + obj.Location_Code)
                                End If
                            End If
                            If clsCommon.CompairString(obj.Is_Section, "Y") = CompairStringResult.Equal AndAlso (clsCommon.myLen(obj.Main_Location_Code) <= 0 Or clsCommon.myLen(obj.Section_Code) <= 0) Then
                                Throw New Exception("Please Check ! Section And Main Location can not be left blank When Is_Section is 'Y'.")
                            End If

                            If (clsCommon.myLen(obj.Main_Location_Code) > 0 AndAlso clsCommon.myLen(obj.Section_Code) > 0) Then
                                If clsCommon.CompairString(obj.Is_Section, "N") = CompairStringResult.Equal Then
                                    Throw New Exception("Please Check ! Is_Section should be 'Y' when Section/Main Location is not empty.")
                                End If
                            ElseIf (clsCommon.myLen(obj.Main_Location_Code) <= 0 AndAlso clsCommon.myLen(obj.Section_Code) > 0) Then
                                If (clsCommon.CompairString(obj.Is_Section, "N") = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(obj.Is_Sub_Location, "N") = CompairStringResult.Equal) Then
                                    Throw New Exception("Please Check ! Is_Sub_Location should be 'Y' when Main Location is not empty.")
                                End If
                            End If

                            Dim MainLocSegment As String = ""
                            If clsCommon.myLen(obj.Main_Location_Code) > 0 Then
                                MainLocSegment = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL( Loc_Segment_Code,'') As Loc_Segment_Code  from TSPL_LOCATION_MASTER Where Location_Code='" & obj.Main_Location_Code & "'", trans))
                            End If

                            If clsCommon.CompairString(obj.Is_Section, "Y") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Is_Sub_Location, "Y") = CompairStringResult.Equal Then
                                If Not clsCommon.CompairString(clsCommon.myCstr(MainLocSegment), obj.Loc_Segment_Code) = CompairStringResult.Equal Then
                                    Throw New Exception("Please check ! Main location segment code and location segment code should be same")
                                End If
                            End If

                            'If (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim()) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim()) Then
                            '    Section_Code = "NULL"
                            '    Main_Location_Code = "NULL"
                            'ElseIf (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "Y".ToUpper().Trim()) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim()) Then
                            '    Section_Code = "'" & strSection & "'"
                            '    Main_Location_Code = "'" & strMainLoc & "'"
                            'ElseIf (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim()) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "Y".ToUpper().Trim()) Then
                            '    Section_Code = "NULL"
                            '    Main_Location_Code = "'" & strMainLoc & "'"
                            'Else
                            '    Section_Code = "'" & strSection & "'"
                            '    Main_Location_Code = "'" & strMainLoc & "'"
                            'End If
                            obj.IsSubLocationWise = clsCommon.myCstr(grow.Cells("SubLocationWise(Y/N)").Value)
                            If Not (clsCommon.CompairString(obj.IsSubLocationWise, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.IsSubLocationWise, "N") = CompairStringResult.Equal) Then
                                Throw New Exception(" SubLocationWise(Y/N) should be Y/N for " + obj.Location_Code)
                            Else
                                obj.IsSubLocationWise = obj.IsSubLocationWise.ToUpper()
                            End If

                            obj.GSTNO = clsCommon.myCstr(grow.Cells("GSTNo").Value)
                            obj.IsMainPlant = clsCommon.myCdbl(grow.Cells("IsMainPlant(0/1)").Value)
                            clsLocation.SaveData(obj, trans, True)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line-" & clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton12_Click(sender As Object, e As EventArgs) Handles RadButton12.Click
        Try
            Me.Text = "Account Main Group"
            qry = "select Account_Main_Group_Code As [Account Main Group Code],Account_Main_Group_Desc as [Description],GROUP_TYPE AS 'Type' from TSPL_ACCOUNT_MAIN_GROUPS"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Main Group")
        End Try
    End Sub

    Private Sub RadButton11_Click(sender As Object, e As EventArgs) Handles RadButton11.Click
        Dim linno As Integer = 0
        Dim gv As New RadGridView()
        Try
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today

            If transportSql.importExcel(gv, "Account Main Group Code", "Description", "Type") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New ClsAccountMainGroup()
                        linno += 1
                        Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If clsCommon.myLen(strcode) > 0 Then
                            If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                                Throw New Exception("Account main group code can't be blank .")
                            ElseIf strcode.Length > 12 Then
                                Throw New Exception("Account main group length can not be more than 12 .")
                            ElseIf Not IsNumeric(strcode) Then
                                Throw New Exception("Please enter numeric value in account main group code .")
                            End If
                            obj.Account_Main_Group_Code = strcode

                            Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                            If strdes.Length <= 0 Or (String.IsNullOrEmpty(strdes)) Then
                                Throw New Exception("Description can't be blank .")
                            ElseIf strdes.Length > 50 Then
                                Throw New Exception("Description length can not be more than 50 .")
                            End If
                            obj.Account_Main_Group_Desc = strdes
                            Dim Type As String = clsCommon.myCstr(grow.Cells(2).Value)
                            If clsCommon.myLen(Type) > 0 Then
                                If clsCommon.CompairString(Type, "Balance Sheet") = CompairStringResult.Equal Or clsCommon.CompairString(Type, "Income Statement") = CompairStringResult.Equal Or clsCommon.CompairString(Type, "Retained Earnings") = CompairStringResult.Equal Then
                                Else
                                    Throw New Exception("Type should be amoung 'Balance Sheet','Income Statement','Retained Earnings' .")
                                End If
                            Else
                                Throw New Exception("Type can not be left blank .")
                            End If
                            obj.Group_Type = Type
                            ClsAccountMainGroup.SaveData(obj, ClsAccountMainGroup.CheckNewEntry(obj.Account_Main_Group_Code, trans), trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no-" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Main Group")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton10_Click(sender As Object, e As EventArgs) Handles RadButton10.Click
        Try
            Me.Text = "Account Group"
            qry = "select account_group_code As [Account Group Code],account_group_desc as [Description] ,ISNULL(Account_Main_Group_Code,'') AS [Account Main Group Code] from tspl_Account_Groups"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Group")
        End Try
    End Sub

    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        Dim gv As New RadGridView()
        Try
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Account Group Code", "Description", "Account Main Group Code") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin() '' added by abhishek as on 12/10/2012
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        linno += 1
                        Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If clsCommon.myLen(strcode) > 12 Then
                            Throw New Exception("Check the length of Group Code")
                        End If

                        Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                        If clsCommon.myLen(strdes) > 100 Then
                            Throw New Exception("Check the length of Description ")
                        End If

                        If String.IsNullOrEmpty(strcode) Then
                            Throw New Exception("Account Group Code can't be blank")
                        End If

                        If String.IsNullOrEmpty(strdes) Then
                            Throw New Exception(" Description can't be blank")
                        End If


                        '' Anubhooti 29-Sep-2014 
                        Dim strAccMainGrp As String = clsCommon.myCstr(grow.Cells("Account Main Group Code").Value)
                        If strAccMainGrp.Length <= 0 Or (String.IsNullOrEmpty(strAccMainGrp)) Then
                            Throw New Exception("Account main group code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf Not IsNumeric(strAccMainGrp) Then
                            Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myLen(strAccMainGrp) > 0 Then
                            Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code='" + strAccMainGrp + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("'" & clsCommon.myCstr(strAccMainGrp) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create account main group entry")
                            End If
                        End If
                        clsGLAccount.GetLinkAccountWithGroup(2, strcode, strAccMainGrp, trans)
                        ''
                        Dim sql1 As String = "select count(*) from tspl_Account_Groups where account_group_code='" + strcode + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i = 0) Then
                            connectSql.RunSpTransaction(trans, "sp_AccountGroups_insert", New SqlParameter("@accgpcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@createby", objCommonVar.CurrentUserCode), New SqlParameter("@createdate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", objCommonVar.CurrentCompanyCode), New SqlParameter("@PrntOrdrno", strcode), New SqlParameter("@Group_Type", ""))
                            If clsCommon.myLen(strAccMainGrp) > 0 Then
                                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(strAccMainGrp) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(strcode) & "'", trans)
                            End If
                        Else
                            connectSql.RunSpTransaction(trans, "sp_AccountGroups_update", New SqlParameter("@accgpcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@modifyby", objCommonVar.CurrentUserCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@companycode", objCommonVar.CurrentCompanyCode), New SqlParameter("@PrntOrdrno", strcode), New SqlParameter("@Group_Type", ""))
                            If clsCommon.myLen(strAccMainGrp) > 0 Then
                                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_ACCOUNT_GROUPS SET Account_Main_Group_Code='" & clsCommon.myCstr(strAccMainGrp) & "' WHERE Account_Group_Code='" & clsCommon.myCstr(strcode) & "'", trans)
                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Group")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        Try
            Me.Text = "Account Sub Group"
            qry = "select Account_Sub_Group_Code As [Account Sub Group Code],Account_Sub_Group_Desc as [Description],Account_Group_Code AS [Account Group Code] from TSPL_ACCOUNT_SUB_GROUPS"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Sub Group")
        End Try
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0
            If transportSql.importExcel(gv, "Account Sub Group Code", "Description", "Account Group Code") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New ClsAccountSubGroup()
                        linno += 1

                        Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                            Throw New Exception("Account sub group code can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf strcode.Length > 30 Then
                            Throw New Exception("Account sub group length can not be more than 30 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Account_Sub_Group_Code = strcode

                        Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                        If strdes.Length <= 0 Or (String.IsNullOrEmpty(strdes)) Then
                            Throw New Exception("Description can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf strdes.Length > 100 Then
                            Throw New Exception("Description length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Account_Sub_Group_Desc = strdes

                        Dim strAccMainGrp As String = clsCommon.myCstr(grow.Cells("Account Group Code").Value)
                        If strAccMainGrp.Length <= 0 Or (String.IsNullOrEmpty(strAccMainGrp)) Then
                            Throw New Exception("Account main group code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myLen(strAccMainGrp) > 0 Then
                            Dim qry As String = "select count(*) As Row from TSPL_ACCOUNT_GROUPS where Account_Group_Code='" + strAccMainGrp + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("'" & clsCommon.myCstr(strAccMainGrp) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create account group entry")
                            End If
                        End If
                        obj.Account_Group_Code = strAccMainGrp
                        ClsAccountSubGroup.SaveData(obj, ClsAccountSubGroup.CheckNewEntry(obj.Account_Sub_Group_Code, trans), trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error as line No-" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Account Sub Group")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton24_Click(sender As Object, e As EventArgs) Handles RadButton24.Click
        Try
            Me.Text = "GL Main Account"
            qry = "select Main_GL_Account as Code,Main_GL_Account_Desc as Name,Sub_Group_Code as [Sub Group] from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "GL Main Account")
        End Try
    End Sub

    Private Sub RadButton23_Click(sender As Object, e As EventArgs) Handles RadButton23.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0
            If transportSql.importExcel(gv, "Code", "Name", "Sub Group") Then
                Dim trans As SqlTransaction
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        clsCommon.ProgressBarPercentShow()
                        For Each grow As GridViewRowInfo In gv.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clsMainGLAccount()
                            linno += 1

                            Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                            If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                                Throw New Exception("Main Account code can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            ElseIf strcode.Length > 30 Then
                                Throw New Exception("Main Account length can not be more than 30 at line no. " + clsCommon.myCstr(linno) + ".")
                            ElseIf Not IsNumeric(strcode) Then
                                Throw New Exception("Please enter numeric value in account main group code at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Main_GL_Account = strcode

                            Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                            If clsCommon.myLen(strdes) <= 0 Then
                                Throw New Exception("Name can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            ElseIf strdes.Length > 100 Then
                                Throw New Exception("Name length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Main_GL_Account_Desc = strdes

                            obj.Sub_Group_Code = clsCommon.myCstr(grow.Cells(2).Value)
                            If clsCommon.myLen(obj.Sub_Group_Code) <= 0 Then
                                Throw New Exception("Name can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Sub_Group_Code = clsDBFuncationality.getSingleValue("select Account_Sub_Group_Code from TSPL_ACCOUNT_SUB_GROUPS where Account_Sub_Group_Code='" + clsCommon.myCstr(grow.Cells(2).Value) + "'", trans)
                            If clsCommon.myLen(obj.Sub_Group_Code) <= 0 Then
                                Throw New Exception("Sub group is not correct" + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Sub_Group_Code = obj.Sub_Group_Code

                            clsMainGLAccount.SaveData(obj, clsMainGLAccount.CheckNewEntry(obj.Main_GL_Account, trans), trans)
                        Next
                        trans.Commit()
                        clsCommon.ProgressBarPercentHide()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at line no" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                    End Try
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "GL Main Account")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton22_Click(sender As Object, e As EventArgs) Handles RadButton22.Click
        Try
            Me.Text = "Tax Authority"
            qry = "Select Tax_Code as 'Tax Authority',Tax_Code_Desc as 'Description',Tax_Liability_Account as 'Tax Liability Account',(case when Tax_Recoverable='Y' then 'Yes' else 'No' END) as 'Tax Recoverable',Tax_Recoverable_Account as 'Tax Recoverable Account',Tax_Recover_Rate as 'Tax Recover Rate',Tax_Net_Payable as 'Tax Net Payable',(Case type when 'V'then 'VAT' when 'E'then 'EXCISE' when 'C'then 'CST' when 'A' then 'ADDTAX' when 'W' then 'WCT' when 'O' then  'OTHER'  when 'S' then 'SERVICE' when 'M' then 'MANDI TAX' when 'IGST' then 'IGST' when 'SGST' then 'SGST' when 'UGST' then 'UGST' when 'CGST' then 'CGST' else ''end)as 'Type',Tax_Recoverable_Account2 as 'Tax Recoverable Account2',Tax_Recover_Rate2 as 'Tax Recover Rate2',Tax_Recoverable_Account3 as 'Tax Recoverable Account3',Tax_Recover_Rate3 as 'Tax Recover Rate3',Tax_Recoverable_Account4 as 'Tax Recoverable Account4',Tax_Recover_Rate4 as 'Tax Recover Rate4',Tax_Recoverable_Account5 as 'Tax Recoverable Account5',Tax_Recover_Rate5 as 'Tax Recover Rate5'  ,PayableControl as [Payable Control],DepositControl as [Deposit Control],GSTActive as [GST Active] from TSPL_TAX_MASTER"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Authority")
        End Try
    End Sub

    Private Sub RadButton21_Click(sender As Object, e As EventArgs) Handles RadButton21.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim LineNo As Integer = 0
            Dim inputs() As String = {}
            inputs = {"Tax Authority", "Description", "Tax Liability Account", "Tax Recoverable", "Tax Recoverable Account", "Tax Recover Rate", "Tax Net Payable", "Type", "Tax Recoverable Account2", "Tax Recover Rate2", "Tax Recoverable Account3", "Tax Recover Rate3", "Tax Recoverable Account4", "Tax Recover Rate4", "Tax Recoverable Account5", "Tax Recover Rate5", "GST Active", "Payable Control", "Deposit Control"}
            Dim Strs As List(Of String) = New List(Of String)(inputs)
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()

                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strTaxCode As String = grow.Cells(0).Value
                        Dim strDesc As String = grow.Cells(1).Value
                        Dim strLiabAcc As String = grow.Cells(2).Value
                        Dim strRecoverable As String = grow.Cells(3).Value
                        Dim strRecoverAcc As String = grow.Cells(4).Value
                        Dim strRecoverRate As String = grow.Cells(5).Value
                        Dim strnetpay As String = grow.Cells(6).Value
                        Dim Type As String = grow.Cells(7).Value.ToString.ToUpper
                        Dim strRecoverAcc2 As String = grow.Cells(8).Value
                        Dim strrecoverRate2 As String = grow.Cells(9).Value
                        Dim strRecoverAcc3 As String = grow.Cells(10).Value
                        Dim strrecoverRate3 As String = grow.Cells(11).Value
                        Dim strRecoverAcc4 As String = grow.Cells(12).Value
                        Dim strrecoverRate4 As String = grow.Cells(13).Value
                        Dim strRecoverAcc5 As String = grow.Cells(14).Value
                        Dim strrecoverRate5 As String = grow.Cells(15).Value
                        Dim GSTActive As Integer = Nothing
                        Dim Payable As String = Nothing
                        Dim Deposit As String = Nothing

                        GSTActive = grow.Cells(16).Value
                        Payable = grow.Cells(17).Value
                        Deposit = grow.Cells(18).Value
                        If Type = "VAT" Then
                            Type = "V"
                        ElseIf Type = "CST" Then
                            Type = "C"
                        ElseIf Type = "EXCISE" Then
                            Type = "E"
                        ElseIf Type = "ADDTAX" Then
                            Type = "A"
                        ElseIf Type = "WCT" Then
                            Type = "W"
                        ElseIf Type = "OTHER" Then
                            Type = "O"
                        ElseIf Type = "SGST" Then
                            Type = "SGST"
                        ElseIf Type = "IGST" Then
                            Type = "IGST"
                        ElseIf Type = "CGST" Then
                            Type = "CGST"
                        ElseIf Type = "UGST" Then
                            Type = "UGST"
                        ElseIf Type = "MANDI TAX" Then
                            Type = "M"
                        Else
                            Throw New Exception("Invalid Tax Type it should be (SGST/IGST/CGST/UGST/VAT/CST/EXCISE/ADDTAX/WCT/MANDI TAX/OTHER)")
                        End If

                        If String.IsNullOrEmpty(strTaxCode) Or strTaxCode.Length > 12 Then
                            Throw New Exception("Tax Authority has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(strDesc) Or strDesc.Length > 50 Then
                            Throw New Exception("Description has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strLiabAcc.Length > 50 Then
                            Throw New Exception("Tax Liability Account has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If (strRecoverable = "Yes" Or strRecoverable = "True") Then
                            strRecoverable = "Y"
                        ElseIf (strRecoverable = "No" Or strRecoverable = "False") Then
                            strRecoverable = "N"
                        Else
                            Throw New Exception("Tax Recoverable has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strRecoverAcc.Length > 50 Then
                            Throw New Exception("Tax Recoverable Account has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strnetpay.Length > 50 Then
                            Throw New Exception("Net Payble  Account has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If

                        Dim re As Regex = New Regex("(^[0-9]{1,3}(\.[0-9]{0,2})?$)")
                        If strRecoverRate.Length > 50 Or Not re.IsMatch(strRecoverRate) Then
                            Throw New Exception("Tax Recover Rate has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strRecoverAcc2.Length > 50 Then
                            Throw New Exception("Tax Recoverable Account2 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        Dim re1 As Regex = New Regex("^[0-9]{1,3}(\.[0-9]{0,2})?$")
                        If strrecoverRate2.Length > 50 Or Not re1.IsMatch(strrecoverRate2) Then
                            Throw New Exception("Tax Recover Rate2 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strRecoverAcc3.Length > 50 Then
                            Throw New Exception("Tax Recoverable Account3 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strrecoverRate3.Length > 50 Or Not re1.IsMatch(strrecoverRate3) Then
                            Throw New Exception("Tax Recover Rate3 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strRecoverAcc4.Length > 50 Then
                            Throw New Exception("Tax Recoverable Account4 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strrecoverRate4.Length > 50 Or Not re1.IsMatch(strrecoverRate4) Then
                            Throw New Exception("Tax Recover Rate4 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strRecoverAcc5.Length > 50 Then
                            Throw New Exception("Tax Recoverable Account5 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If strrecoverRate5.Length > 50 Or Not re1.IsMatch(strrecoverRate5) Then
                            Throw New Exception("Tax Recover Rate5 has some incorrect values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If



                        If Payable.Length > 50 Then
                            Throw New Exception("Fill Payable Account ")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If Deposit.Length > 50 Then
                            Throw New Exception("Fill Deposit Account ")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If



                        Try
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_INSERT", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strDesc), New SqlParameter("@Tax_Liability_Account", strLiabAcc), New SqlParameter("@Tax_Recoverable", strRecoverable), New SqlParameter("@Tax_Recover_Account", strRecoverAcc), New SqlParameter("@Tax_Recover_Rate", strRecoverRate), New SqlParameter("@Tax_Net_Payable", strnetpay), New SqlParameter("@Excisable", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode), New SqlParameter("@Type", Type), New SqlParameter("@taxRecoverableAcc2", strRecoverAcc2), New SqlParameter("@TaxRecoverRate2", strrecoverRate2), New SqlParameter("@taxRecoverableAcc3", strRecoverAcc3), New SqlParameter("@TaxRecoverRate3", strrecoverRate3), New SqlParameter("@taxRecoverableAcc4", strRecoverAcc4), New SqlParameter("@TaxRecoverRate4", strrecoverRate4), New SqlParameter("@taxRecoverableAcc5", strRecoverAcc5), New SqlParameter("@TaxRecoverRate5", strrecoverRate5))

                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Type", Type)
                            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_MASTER", OMInsertOrUpdate.Update, "Tax_Code='" + strTaxCode + "'", trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strTaxCode, "TSPL_TAX_MASTER", "Tax_Code", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_TAX_MASTER set GSTActive='" & GSTActive & "' ,PayableControl='" & Payable & "' , DepositControl='" & Deposit & "' where tax_code='" & strTaxCode & "'", trans)
                        Catch ex As Exception
                            If ex.Message.Contains("Violation of PRIMARY KEY") Then
                                connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_UPDATE", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strDesc), New SqlParameter("@Tax_Liability_Account", strLiabAcc), New SqlParameter("@Tax_Recoverable", strRecoverable), New SqlParameter("@Tax_Recover_Account", strRecoverAcc), New SqlParameter("@Tax_Recover_Rate", strRecoverRate), New SqlParameter("@Tax_Net_Payable", strnetpay), New SqlParameter("@Excisable", ""), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Type", Type), New SqlParameter("@taxRecoverableAcc2", strRecoverAcc2), New SqlParameter("@TaxRecoverRate2", strrecoverRate2), New SqlParameter("@taxRecoverableAcc3", strRecoverAcc3), New SqlParameter("@TaxRecoverRate3", strrecoverRate3), New SqlParameter("@taxRecoverableAcc4", strRecoverAcc4), New SqlParameter("@TaxRecoverRate4", strrecoverRate4), New SqlParameter("@taxRecoverableAcc5", strRecoverAcc5), New SqlParameter("@TaxRecoverRate5", strrecoverRate5))
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Type", Type)
                                clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_MASTER", OMInsertOrUpdate.Update, "Tax_Code='" + strTaxCode + "'", trans)
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strTaxCode, "TSPL_TAX_MASTER", "Tax_Code", trans)

                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strTaxCode), "TSPL_TAX_MASTER", "Tax_Code", trans)
                            End If
                        End Try
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line No " + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Authority")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton20_Click(sender As Object, e As EventArgs) Handles RadButton20.Click
        Try
            Me.Text = "Tax Rate"
            qry = "Select Tax_Code as 'Tax Authority',(Case when Tax_Type='S' then 'Sales' when Tax_Type='P' then  'Purchase' when Tax_Type='T' then 'Transfer' else '' end) as 'Tax Type',Tax_Rate_Code as 'Rate Code',Tax_Rate_Desc as 'Rate Description',Tax_Rate as 'Tax Rate'  ,case when GSTActive=1 then 'Y' else 'N' end as [GST Active(Y/N)]  from TSPL_TAX_RATES"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Rate")
        End Try
    End Sub

    Private Sub RadButton19_Click(sender As Object, e As EventArgs) Handles RadButton19.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim Input() As String = {}
            Input = {"Tax Authority", "Tax Type", "Rate Code", "Rate Description", "Tax Rate", "GST Active(Y/N)"}
            Dim strInputlist As List(Of String) = New List(Of String)(Input)
            If transportSql.importExcel(gv, strInputlist.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Dim lineNo As Integer = 0
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        lineNo += 1
                        Dim strTaxCode As String = grow.Cells(0).Value
                        Dim strTaxType As String = grow.Cells(1).Value
                        Dim strRateCode As String = grow.Cells(2).Value
                        Dim strRateDesc As String = grow.Cells(3).Value
                        Dim strRate As String
                        If IsDBNull(grow.Cells(4).Value) Then
                            strRate = ""
                        Else
                            strRate = grow.Cells(4).Value
                        End If
                        Dim strGSTActive As Integer
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("GST Active(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                            strGSTActive = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("GST Active(Y/N)").Value), "N") = CompairStringResult.Equal Then
                            strGSTActive = 0
                        Else
                            Throw New Exception("GST Aactive should be Y/N")
                        End If
                        If strGSTActive <> 1 AndAlso strGSTActive <> 0 Then
                            Throw New Exception("GST Aactive should be 0 or 1")
                        End If
                        If String.IsNullOrEmpty(strTaxCode) Or strTaxCode.Length > 12 Then
                            Throw New Exception("Tax Authority has some incorrect values")
                        End If
                        If (strTaxType = "Sales") Then
                            strTaxType = "S"
                        ElseIf (strTaxType = "Purchase") Then
                            strTaxType = "P"
                        ElseIf (strTaxType = "Transfer") Then
                            strTaxType = "T"
                        Else
                            Throw New Exception("Tax Type has some incorrect values")
                        End If
                        qry = "select Tax_Code from TSPL_TAX_MASTER  where Tax_Code='" + strTaxCode + "'  "
                        strTaxCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(strTaxCode) <= 0 Then
                            Throw New Exception("Not a valid tax authority")
                        End If

                        If String.IsNullOrEmpty(strRateCode) Or strRateCode.Length > 6 Then
                            Throw New Exception("Tax Rate Code has some incorrect values")
                        End If
                        If String.IsNullOrEmpty(strRateDesc) Or strRateDesc.Length > 50 Then
                            Throw New Exception("Tax Rate Description has some incorrect values")
                        End If
                        If String.IsNullOrEmpty(strRate) Or strRate.Length > 50 Then
                            Throw New Exception("Tax Rate has some incorrect values")
                        End If
                        Dim re As Regex = New Regex("(^[0-9]+$)")
                        If Not IsNumeric(strRate) Then
                            Throw New Exception("Tax Rate has some incorrect values")
                        End If
                        Try
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_RATES_INSERT", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Type", strTaxType), New SqlParameter("@Tax_Rate_Desc", strRateDesc), New SqlParameter("@Tax_Rate", strRate), New SqlParameter("@Tax_Rate_Code", strRateCode), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode), New SqlParameter("@GL_Account_Code", ""), New SqlParameter("@Type", ""))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "GSTActive", clsCommon.myCdbl(strGSTActive))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_RATES", OMInsertOrUpdate.Update, "TSPL_TAX_RATES.Tax_Code='" + strTaxCode + "' and Tax_Type='" + strTaxType + "'", trans)
                        Catch ex As Exception
                            If ex.Message.Contains("Violation of PRIMARY KEY") Then
                                qry = "UPDATE TSPL_TAX_RATES SET Tax_Rate_Desc='" + strRateDesc + "',Tax_Rate='" + strRate + "' WHERE Tax_Code='" + strTaxCode + "' and Tax_Type='" + strTaxType + "' and Tax_Rate_Code='" + strRateCode + "'"
                                connectSql.RunSqlTransaction(trans, qry)
                            End If
                        End Try
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at row no" + clsCommon.myCstr(lineNo) + Environment.NewLine + ex.Message)
                End Try
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Rate")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton18_Click(sender As Object, e As EventArgs) Handles RadButton18.Click
        Try
            Me.Text = "Tax Group"
            qry = "SELECT TSPL_Tax_Group_Master.Tax_Group_Code AS 'Tax Group Code',TSPL_Tax_Group_Master.Tax_Group_Desc AS 'Description', " & _
             "(CASE WHEN TSPL_Tax_Group_Master.Tax_Group_Type = 'S' THEN 'Sales' ELSE 'Purchase' END) AS 'Tax Group Type',TSPL_TAX_GROUP_MASTER.Excisable ,TSPL_TAX_GROUP_MASTER .VAT ,TSPL_TAX_GROUP_MASTER .STax ," & _
             " TSPL_TAX_GROUP_DETAILS.Tax_Code AS 'Tax Authority', TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc AS 'Authority Description'," & _
             " (CASE WHEN TSPL_TAX_GROUP_DETAILS.Taxable = 'N' THEN 'No' ELSE 'Yes' END) AS Taxable, " & _
             " (CASE WHEN TSPL_TAX_GROUP_DETAILS.Surtax = 'Y' THEN 'Yes' ELSE 'No' END) AS Surtax, " & _
             " TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code AS 'Surtax Code', TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code_Desc AS 'Surtax Description' ,case when TSPL_Tax_Group_Master.Active=1 then 'Y' else 'N' end as 'Active(Y/N)' FROM TSPL_Tax_Group_Master INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_Tax_Group_Master.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code AND " & _
             "  TSPL_Tax_Group_Master.Tax_Group_Type = TSPL_TAX_GROUP_DETAILS.Tax_Group_Type "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Group")
        End Try
    End Sub

    Private Sub RadButton17_Click(sender As Object, e As EventArgs) Handles RadButton17.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim input() As String = {}
            input = {"Tax Group Code", "Description", "Tax Group Type", "Excisable", "VAT", "STax", "Tax Authority", "Authority Description", "Taxable", "Surtax", "Surtax Code", "Surtax Description", "Active(Y/N)"}
            Dim inputlist As List(Of String) = New List(Of String)(input)
            If transportSql.importExcel(gv, inputlist.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    Dim transCode As Integer
                    Dim TaxGroupCode As String = ""
                    Dim TaxGroupType As String = ""
                    Dim Active As Integer = 0
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strTaxGrCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        Dim strDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                        Dim strTaxGrType As String = clsCommon.myCstr(grow.Cells(2).Value)
                        Dim strexcisable As String = clsCommon.myCstr(grow.Cells(3).Value)
                        Dim strvat As String = clsCommon.myCstr(grow.Cells(4).Value)
                        Dim strstax As String = clsCommon.myCstr(grow.Cells(5).Value)
                        Dim strTaxCode As String = clsCommon.myCstr(grow.Cells(6).Value)
                        Dim strTaxCodeDesc As String = clsCommon.myCstr(grow.Cells(7).Value)
                        Dim strTaxable As String = clsCommon.myCstr(grow.Cells(8).Value)
                        Dim strSurtax As String = clsCommon.myCstr(grow.Cells(9).Value)
                        Dim strSurtaxCode As String = clsCommon.myCstr(grow.Cells(10).Value)
                        Dim strSurtaxCodeDesc As String = clsCommon.myCstr(grow.Cells(11).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Active(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                            Active = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Active(Y/N)").Value), "N") = CompairStringResult.Equal Then
                            Active = 0
                        Else
                            Throw New Exception("Active(Y/N) shoul be Y/N")
                        End If
                        If String.IsNullOrEmpty(strexcisable) Or strexcisable.Length <= 0 Then
                            Throw New Exception("Excisable cannont be left blank")
                        End If
                        If String.IsNullOrEmpty(strvat) Or strvat.Length <= 0 Then
                            Throw New Exception("Tax cannont be left blank")
                        End If
                        If String.IsNullOrEmpty(strstax) Or strstax.Length <= 0 Then
                            Throw New Exception("STax cannont be left blank")
                        End If
                        If String.IsNullOrEmpty(strTaxGrCode) Or strTaxGrCode.Length > 12 Then
                            Throw New Exception("Tax Authority has some incorrect values")
                        End If
                        If String.IsNullOrEmpty(strDesc) Or strDesc.Length > 50 Then
                            Throw New Exception("Description has some incorrect values")
                        End If
                        If (strTaxGrType = "Sales") Then
                            strTaxGrType = "S"
                        ElseIf (strTaxGrType = "Purchase") Then
                            strTaxGrType = "P"
                        Else
                            Throw New Exception("Taxable has some incorrect values")
                        End If
                        If String.IsNullOrEmpty(strTaxCode) Or strTaxCode.Length > 12 Then
                            Throw New Exception("Tax Authority has some incorrect values")
                        End If
                        If String.IsNullOrEmpty(strTaxCodeDesc) Or strTaxCodeDesc.Length > 50 Then
                            Throw New Exception("Authority Description has some incorrect values")
                        End If
                        If (strTaxable = "Yes" Or strTaxable = "True") Then
                            strTaxable = "Y"
                        ElseIf (strTaxable = "No" Or strTaxable = "False") Then
                            strTaxable = "N"
                        Else
                            Throw New Exception("Taxable has some incorrect values")
                        End If
                        If (strSurtax = "Yes" Or strSurtax = "True") Then
                            strSurtax = "Y"
                        ElseIf (strSurtax = "No" Or strSurtax = "False") Then
                            strSurtax = "N"
                        Else
                            Throw New Exception("Surtax has some incorrect values")
                        End If
                        If strSurtaxCode.Length > 12 Then
                            Throw New Exception("Surtax Code has some incorrect values")
                        End If
                        If strSurtaxCodeDesc.Length > 50 Then
                            Throw New Exception("Surtax Description has some incorrect values")
                        End If
                        Dim qry As String
                        Try
                            If Not (clsCommon.CompairString(TaxGroupCode, strTaxGrCode) = CompairStringResult.Equal And clsCommon.CompairString(TaxGroupType, strTaxGrType) = CompairStringResult.Equal) Then
                                transCode = 1
                                qry = "delete from  TSPL_TAX_GROUP_DETAILS where Tax_Group_Code = '" & strTaxGrCode & "' AND Tax_Group_Type='" + strTaxGrType + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = " Select COUNT(*) From TSPL_TAX_GROUP_MASTER Where Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "' "
                                Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                                If Count <= 0 Then
                                    connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_MASTER_INSERT", New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Desc", strDesc), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Excise", strexcisable), New SqlParameter("@Vat", strvat), New SqlParameter("@STax", strstax), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                                    Dim col1 As New Hashtable()
                                    clsCommon.AddColumnsForChange(col1, "Active", Active)
                                    clsCommon.AddColumnsForChange(col1, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)
                                    clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "'", trans)
                                Else
                                    connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_MASTER_UPDATE", New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Desc", strDesc), New SqlParameter("@Excisable", strexcisable), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Vat", strvat), New SqlParameter("@STax", strstax), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                                    Dim col1 As New Hashtable()
                                    clsCommon.AddColumnsForChange(col1, "Active", Active)
                                    clsCommon.AddColumnsForChange(col1, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)
                                    clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "'", trans)
                                End If
                            End If
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", transCode), New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strTaxCodeDesc), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", strSurtaxCode), New SqlParameter("@Surtax_Tax_Code_Desc", strSurtaxCodeDesc), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                            transCode = transCode + 1
                            TaxGroupCode = strTaxGrCode
                            TaxGroupType = strTaxGrType
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at line no " + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Tax Group")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton40_Click(sender As Object, e As EventArgs) Handles RadButton40.Click
        Try
            Me.Text = "Vendor Account Set"
            Dim query As String = "select acct_set_code as 'Account Set Code',acct_set_desc as 'Description',payable_account as 'Payable Control Account',discount_account as 'Discount_Account'"
            query += " ,Advance_account as 'Advance Account',CURRENCY_CODE  as 'Currency' from tspl_vendor_account_set"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Account Set")
        End Try
    End Sub

    Private Sub RadButton39_Click(sender As Object, e As EventArgs) Handles RadButton39.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Account Set Code", "Description", "Payable Control Account", "Discount_Account", "Advance Account", "Currency") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        linno += 1
                        Dim straccountsetcode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                        Dim strdisc As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                        Dim strpayablecontrol As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                        '' Anubhooti 06-Nov-2014
                        If clsCommon.myLen(strpayablecontrol) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpayablecontrol + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled payable control account (" & strpayablecontrol & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpayablecontrol + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled payable control account (" & strpayablecontrol & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        ''
                        Dim strdiscount As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                        '' Anubhooti 06-Nov-2014
                        If clsCommon.myLen(strdiscount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdiscount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled discount account (" & strdiscount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdiscount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled discount account (" & strdiscount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        ''
                        Dim stradvanceaccount As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                        '' Anubhooti 06-Nov-2014
                        If clsCommon.myLen(stradvanceaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled advance account (" & stradvanceaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled advance account (" & stradvanceaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If

                        Dim strCurrancy As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                        Dim qry3 As String = "select count(*) from TSPL_CURRENCY_MASTER where CURRENCY_CODE='" + strCurrancy + "'"
                        Dim check2 As Integer = clsDBFuncationality.getSingleValue(qry3, trans)
                        If check2 <= 0 Then
                            Throw New Exception("Filled Currancy (" & strCurrancy & ")  does not exist" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If


                        Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        If String.IsNullOrEmpty(straccountsetcode) Or straccountsetcode.Length > 12 Then
                            Throw New Exception("Vendor Acccount has some incorrect values length")
                        End If
                        If (straccountsetcode.Length > 12) Or (strdisc.Length > 100) Then
                            Throw New Exception("Check the length of Description")
                        End If
                        If strpayablecontrol.Length > 50 Then
                            Throw New Exception("Check the length of Payables Control")
                        End If
                        If strdiscount.Length > 50 Then
                            Throw New Exception("Check the length of Discount Account")
                        End If
                        If stradvanceaccount.Length > 50 Then
                            Throw New Exception("Check the length of Advance Account")
                        End If
                        Dim sql1 As String = "select count(*)from tspl_vendor_account_set  where acct_set_code='" + straccountsetcode + "'"
                        Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        If (i = 0) Then
                            Dim query As String = "insert into tspl_vendor_account_set (Acct_Set_Code,Acct_Set_Desc,Payable_Account,Discount_Account,Advance_Account,CURRENCY_CODE,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code)values('" + straccountsetcode + "','" + strdisc + "','" + strpayablecontrol + "','" + strdiscount + "','" + stradvanceaccount + "','" + strCurrancy + "' ,'" + objCommonVar.CurrentUserCode + "','" + Datee + "','" + objCommonVar.CurrentUserCode + "','" + Datee + "','" + objCommonVar.CurrentCompanyCode + "')"
                            clsDBFuncationality.ExecuteNonQuery(query, trans)
                        Else
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, straccountsetcode, "TSPL_VENDOR_ACCOUNT_SET", "Acct_set_code", trans)
                            Dim query1 As String = "update tspl_vendor_account_set set acct_set_desc='" + strdisc + "',Payable_account='" + strpayablecontrol + "',discount_account='" + strdiscount + "',Advance_account='" + stradvanceaccount + "',CURRENCY_CODE='" + strCurrancy + "'  where Acct_set_code='" + straccountsetcode + "'"
                            clsDBFuncationality.ExecuteNonQuery(query1, trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at row no-" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Account Set")
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton38_Click(sender As Object, e As EventArgs) Handles RadButton38.Click
        Try
            Me.Text = "Vendor Group"
            qry = "select ven_group_code as 'Vendor Group Code',group_desc as 'Description',acct_set_code as 'Account Set',acct_set_desc as 'Account Set Description',Terms_code as 'Terms Code',terms_desc as 'Terms Descriptiion',payment_code as 'Payment Code',payment_desc as 'Payment Code Description',Bank_Code as 'Bank Code',Description as 'Bank Code Description',tax_group_code as 'Tax Group',Tax_group_desc as 'Tax Group Description',(case when Is_TDSApplicable='1' then 'Y' else 'N' end) as 'Is TDS Applicable'  from tspl_vendor_Group"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Group")
        End Try
    End Sub

    Private Sub RadButton37_Click(sender As Object, e As EventArgs) Handles RadButton37.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Vendor Group Code", "Description", "Account Set", "Account Set Description", "Terms Code", "Terms Descriptiion", "Payment Code", "Payment Code Description", "Bank Code", "Bank Code Description", "Tax Group", "Tax Group Description", "Is TDS Applicable") Then
                Dim trans As SqlTransaction = Nothing
                Dim lineNo As Integer = 0
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        lineNo += 1
                        Dim ISTDS As Integer = 0
                        Dim strvendorgroupcode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                        Dim strdisc As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                        Dim straccountset As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                        Dim stracctsetdescription As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                        Dim strtermscode As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                        Dim strtermsdescription As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                        Dim strpaymentcode As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                        Dim strpaymentcodedescription As String = clsCommon.myCstr(dgrv.Cells(7).Value)
                        Dim strbankcode As String = clsCommon.myCstr(dgrv.Cells(8).Value)
                        Dim strbankcodedescription As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                        Dim strtaxgroup As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                        Dim strtaxdescription As String = clsCommon.myCstr(dgrv.Cells(11).Value)
                        Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        Dim IsTDSApplicable As String = clsCommon.myCstr(dgrv.Cells("Is TDS Applicable").Value)

                        If String.IsNullOrEmpty(strvendorgroupcode) Or strvendorgroupcode.Length > 12 Then
                            Throw New Exception("Vendor Acccount has some incorrect values")
                            trans.Rollback()
                            Exit Sub
                        End If
                        If clsCommon.myLen(strdisc) > 50 Then
                            Throw New Exception("Check the length of Description")
                        End If

                        If (clsCommon.myLen(straccountset) > 12) Or (clsCommon.myLen(stracctsetdescription) > 50) Then
                            Throw New Exception("Check the length of Account Set")
                        End If

                        If (clsCommon.myLen(strtermscode) > 12) Or (clsCommon.myLen(strtermsdescription) > 50) Then
                            Throw New Exception("Check the length of Terms Code")
                        End If

                        If (clsCommon.myLen(strpaymentcode) > 12) Or (clsCommon.myLen(strpaymentcodedescription) > 50) Then
                            Throw New Exception("Check the length of Payment Code")
                        End If

                        If clsCommon.myLen(strbankcode) > 12 Then
                            Throw New Exception("Check the length of Bank Code")
                        End If
                        If clsCommon.myLen(strbankcode) > 0 Then
                            strbankcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Code from TSPL_Vendor_Bank_MASTER where Bank_Code='" + strbankcode + "'", trans))
                            If clsCommon.myLen(strbankcode) <= 0 Then
                                Throw New Exception("Not a valid vendor bank")
                            End If
                            strbankcodedescription = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Name from TSPL_Vendor_Bank_MASTER where Bank_Code='" + strbankcode + "'", trans))
                        Else
                            strbankcodedescription = ""
                        End If



                        If (clsCommon.myLen(strtaxgroup) > 12) Or (clsCommon.myLen(strtaxdescription) > 50) Then
                            Throw New Exception("Check the length of Tax Group")
                        End If

                        If clsCommon.myLen(strtaxgroup) > 0 Then
                            qry = "select Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + strtaxgroup + "' and Tax_Group_Type='P'"
                            strtaxgroup = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(strtaxgroup) <= 0 Then
                                Throw New Exception("Invalid Tax group")
                            End If
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(IsTDSApplicable), "Y") = CompairStringResult.Equal Then
                            ISTDS = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(IsTDSApplicable), "N") = CompairStringResult.Equal Then
                            ISTDS = 0
                        Else
                            Throw New Exception("Fill 'Is TDS Applicable' in 'Y/N' format")
                        End If

                        Dim sql1 As String = "select count(*)from tspl_vendor_group  where ven_group_code='" + strvendorgroupcode + "'"
                        Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        If (i = 0) Then
                            Dim query As String = "insert into tspl_vendor_group(Ven_Group_Code,Group_Desc,Acct_Set_Code,Acct_Set_Desc,Terms_Code,Terms_Desc,Payment_Code,Payment_Desc,Bank_Code,Description,Tax_Group_Code,Tax_Group_Desc,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Is_TDSApplicable) values('" + strvendorgroupcode + "','" + strdisc + "','" + straccountset + "','" + stracctsetdescription + "','" + strtermscode + "','" + strtermsdescription + "','" + strpaymentcode + "','" + strpaymentcodedescription + "','" + strbankcode + "','" + strbankcodedescription + "','" + strtaxgroup + "','" + strtaxdescription + "','" + objCommonVar.CurrentUserCode + "','" + Datee + "','" + objCommonVar.CurrentUserCode + "','" + Datee + "','" + objCommonVar.CurrentCompanyCode + "','" + clsCommon.myCstr(ISTDS) + "')"
                            connectSql.RunSqlTransaction(trans, query)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strvendorgroupcode), "tspl_vendor_group", "ven_group_code", trans)
                        Else
                            Dim query1 As String = "update tspl_vendor_group set group_desc ='" + strdisc + "',acct_set_code='" + straccountset + "',acct_set_desc ='" + stracctsetdescription + "',terms_code='" + strtermscode + "',terms_desc='" + strtermsdescription + "',payment_code='" + strpaymentcode + "',payment_desc='" + strpaymentcodedescription + "',bank_code='" + strbankcode + "',description='" + strbankcodedescription + "',tax_group_code='" + strtaxgroup + "',tax_group_desc ='" + strtaxdescription + "',Is_TDSApplicable='" + clsCommon.myCstr(ISTDS) + "' where ven_group_code='" + strvendorgroupcode + "'"
                            connectSql.RunSqlTransaction(trans, query1)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strvendorgroupcode), "tspl_vendor_group", "ven_group_code", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no:" + clsCommon.myCstr(lineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Group")
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton36_Click(sender As Object, e As EventArgs) Handles RadButton36.Click
        Try
            Me.Text = "Vendor Master"
            qry = "select Vendor_Code as [Vendor No],Vendor_Name as[Vendor Name],Alies_Name AS [Alias Name],Vendor_Type_CHA As [Vendor Type CHA],Add1 as [Address1] ,Vendor_Group_Code as [Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],City_Code_Desc as [City Code Description],State_Code, Country_Code, Phone1 as [Phone Num1],Email as [Email Id],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],PAN as [PAN] ,Is_Parent_Vendor,Parent_Vendor_Code,Pin_Code,TSPL_VENDOR_MASTER.CURRENCY_CODE as [CURRENCY CODE],DFOption as 'Domestic Foreign', BusinessCondition as 'Business Condition', GSTRegistered as 'GST Register', GST_Composition_scheme as 'GST Composition Scheme', GSTFinalNo as 'GSTIN No' from TSPL_VENDOR_MASTER"
            Dim whrCls = " and (form_type='ALL') "
            transportSql.ExporttoExcel(qry, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Master")
        End Try
    End Sub

    Private Sub RadButton35_Click(sender As Object, e As EventArgs) Handles RadButton35.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim strbankdes As String = ""
            Dim Count As String = ""
            Dim AllowAutoVCode As String = ""
            Dim AllowAutoVCodeForAllCompany As String = ""
            Dim qryNatureDed As String = ""
            Dim strBName As String
            Dim strIFSCCode As String = String.Empty
            Dim strBranchCode As String = String.Empty
            Dim strAliesName As String = ""
            Dim strVenType As String = ""
            Dim Str_Vendor As String = ""
            Dim GSTFinal As String = ""
            Dim DFoption As String = Nothing
            Dim Registered As Integer = 0
            Dim GST_Composition_scheme As Integer = 0
            Dim BusinessCondition As String = ""
            Dim GSTEntity As String = ""
            Dim GSTLastEntity As String = ""
            Dim GSTMiddleEntity As String = ""
            Dim inputs() As String = {}
            inputs = {"Vendor No", "Vendor Name", "Alias Name", "Vendor Type CHA", "Address1", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State_Code", "Country_Code", "Phone Num1", "Email Id", "Vendor Account", "Vendor Account Description", "PAN", "Is_Parent_Vendor", "Parent_Vendor_Code", "Pin_Code", "CURRENCY CODE", "Domestic Foreign", "Business Condition", "GST Register", "GST Composition Scheme", "GSTIN No"}
            Dim Strs As List(Of String) = New List(Of String)(inputs)
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Dim linno As Integer = 0
                Dim IsBlacklisted As Integer = 0
                Dim strbank As String = String.Empty
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Count = clsCommon.myCstr(grow.Index + 2)
                        linno += 1
                        clsCommon.ProgressBarPercentUpdate((linno * 100) / gv.RowCount - 1, "Importing " + clsCommon.myCstr(linno) + "/" + clsCommon.myCstr(gv.RowCount - 1))
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Vendor No").Value)
                        AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, trans))
                        If clsCommon.CompairString(AllowAutoVCode, "0") = CompairStringResult.Equal Then
                            If strvendorNo.Length > 12 Then
                                Throw New Exception("Check the lenght of Vendor No.")
                            End If

                            If String.IsNullOrEmpty(strvendorNo) Then
                                Throw New Exception("Vendor No. can not be blank")
                            End If
                        End If
                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Vendor Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of Vendor Name can not be greater than 100.")
                        End If
                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("Vendor Name can not be blank")
                        End If
                        strAliesName = clsCommon.myCstr(grow.Cells("Alias Name").Value)
                        If clsCommon.myLen(strAliesName) > 200 Then
                            Throw New Exception("Alias Name should be max 200 character")
                        End If

                        Dim CSAType As String = "N"
                        Dim ChillingVen As String = "0"

                        strVenType = clsCommon.myCstr(grow.Cells("Vendor Type CHA").Value).Trim().ToUpper()
                        If clsCommon.myLen(strVenType) > 0 Then
                            If clsCommon.CompairString(strVenType, "BROKER") = CompairStringResult.Equal OrElse clsCommon.CompairString(strVenType, "CSA") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "CHA") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "CV") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "A") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "O") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "J") = CompairStringResult.Equal Then 'clsCommon.CompairString(strVenType, "COM") = CompairStringResult.Equal OrElse
                                If clsCommon.CompairString(strVenType, "CSA") = CompairStringResult.Equal Then
                                    CSAType = "Y"
                                ElseIf clsCommon.CompairString(strVenType, "CV") = CompairStringResult.Equal Then
                                    ChillingVen = "1"
                                End If
                            Else
                                Throw New Exception("Vendor Type CHA should be any one from 'CSA','CHA','BROKER','CV','A','O','M','J'")
                            End If
                        End If
                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the lenght of Group Code")
                        End If
                        Dim strGrpBankCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Code from Tspl_vendor_group where ven_group_code='" + strgroupCode + "'", trans))
                        Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the lenght of Group Code Description")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                        Dim citycodedesc As String = clsCommon.myCstr(grow.Cells("City Code Description").Value)
                        Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                        If clsCommon.myLen(statecode) > 0 Then
                            Dim qryState As String = "select Count(*) As Row from TSPL_STATE_MASTER where State_Code ='" & statecode & "'"
                            Dim checkState As Integer = clsDBFuncationality.getSingleValue(qryState, trans)
                            If checkState <= 0 Then
                                Throw New Exception("Filled state code does not exist" + Environment.NewLine + ".First make the entry for state code")
                            End If
                        End If
                        Dim state As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select State_Name from TSPL_STATE_MASTER WHERE State_Code='" & statecode & "'", trans))
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)


                        Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Vendor Account can not be blank")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the lenght of Vendor Account Set Code")
                        End If
                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                        If clsCommon.myLen(strbank) > 0 Then
                            strbank = strbank
                        Else
                            strbank = strGrpBankCode
                        End If
                        Dim isparent As String = clsCommon.myCstr(grow.Cells("Is_Parent_Vendor").Value)
                        Dim parent_vendor_code As String = clsCommon.myCstr(grow.Cells("Parent_Vendor_Code").Value)
                        If clsCommon.myLen(isparent) <= 0 Then
                            Throw New Exception("Please Fill Parent Status,If Is_Parent_Vendor Then Put '1' Else Put '0'")
                        ElseIf clsCommon.CompairString(isparent, "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(isparent, "0") <> CompairStringResult.Equal Then
                            Throw New Exception("Please Fill Parent Status,If Is_Parent_Vendor Then Put '1' Else Put '0'")
                        End If

                        If clsCommon.CompairString(isparent, "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(parent_vendor_code) <= 0 Then
                            Throw New Exception("Please Fill Parent Vendor Code")
                        ElseIf clsCommon.CompairString(isparent, "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(parent_vendor_code) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where vendor_code='" + parent_vendor_code + "' and is_parent_vendor='1'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("Filled Parent Vendor Code Does Not Exist," + Environment.NewLine + "First Make The Entry For Parent Vendor Then" + Environment.NewLine + "Choose It As Parent of Child Vendor")
                            End If
                        End If
                        If clsCommon.myLen(strbank) > 0 Then
                            Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                            If obj IsNot Nothing Then
                                strbankdes = obj.Bank_Name
                                strBName = obj.Bank_Name
                            End If
                        Else
                            strbankdes = ""
                            strBName = ""
                        End If
                        Dim strPin_Code As String = clsCommon.myCstr(grow.Cells("Pin_Code").Value)
                        If clsCommon.myLen(strPin_Code) > 0 Then
                            If clsCommon.myLen(strPin_Code) > 6 Then
                                Throw New Exception("Length of Pin Code should be max. 6 character")
                            End If
                        End If
                        Dim CURRENCYCode As String
                        Dim CURRENCY_CODE As String = clsCommon.myCstr(grow.Cells("CURRENCY CODE").Value)
                        If clsCommon.myLen(CURRENCY_CODE) <= 0 Then
                            If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                                Throw New Exception("Please set base currency in company master")
                            End If
                            CURRENCY_CODE = objCommonVar.BaseCurrencyCode
                        End If
                        CURRENCYCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CURRENCY_CODE  from TSPL_CURRENCY_MASTER Where CURRENCY_CODE='" + CURRENCY_CODE + "'", trans))
                        If clsCommon.CompairString(CURRENCY_CODE, CURRENCYCode) = CompairStringResult.Equal Then
                            qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(vendoracct) & "' "
                            Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry, trans).ToString
                            If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(CURRENCY_CODE)) <> CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow("Account Set Currency and Vendor Currency must be same in case of Multicurrency.")
                                Exit Sub
                            End If
                        Else
                            Throw New Exception("This Currency Code Does not Exist in Currency Master")
                        End If

                        Dim PanNo As String = ""
                        Dim GSTSate_COde As String = ""
                        Registered = clsCommon.myCstr(grow.Cells("GST Register").Value)
                        If clsCommon.myLen(Registered) > 0 Then
                            If Registered = 1 Then
                                Registered = 1
                            Else
                                Registered = 0
                            End If
                        Else
                            Throw New Exception("Fill GST Registered")
                        End If
                        DFoption = clsCommon.myCstr(grow.Cells("Domestic Foreign").Value)
                        If clsCommon.myLen(DFoption) > 0 Then
                            If DFoption = "Domestic" Then
                                DFoption = "Domestic"
                            Else
                                DFoption = "Foreign"
                            End If
                        Else
                            Throw New Exception("Fill Option Foreign/Domestic")
                        End If
                        BusinessCondition = clsCommon.myCstr(grow.Cells("Business Condition").Value)
                        GST_Composition_scheme = clsCommon.myCstr(grow.Cells("GST Composition Scheme").Value)
                        If GST_Composition_scheme = 1 Then
                            GST_Composition_scheme = 1
                        Else
                            GST_Composition_scheme = 0
                        End If
                        GSTFinal = ""
                        GSTEntity = ""
                        GSTMiddleEntity = ""
                        GSTLastEntity = ""
                        If Registered = 1 Then
                            PanNo = clsCommon.myCstr(grow.Cells("Pan").Value)
                            If clsCommon.myLen(PanNo) <= 0 Then
                                Throw New Exception("Fill Pan Number.")
                            End If
                            Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & statecode & "'", trans)
                            If clsCommon.myLen(check) > 0 Then
                                GSTSate_COde = check
                            Else
                                Throw New Exception("Mapped GST State Code in State Master")
                            End If
                            GSTFinal = clsCommon.myCstr(grow.Cells("GSTIN No").Value)
                            Dim StrMsg As String = clsERPFuncationality.ValidationGSTNO(GSTSate_COde, PanNo, GSTFinal, trans)
                            If clsCommon.myCstr(StrMsg) = "False" Then
                                Exit Sub
                            End If
                            GSTEntity = GSTFinal.Trim().Substring(12, 1)
                            GSTMiddleEntity = GSTFinal.Trim().Substring(13, 1)
                            GSTLastEntity = GSTFinal.Trim().Substring(14, 1)
                        End If
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "form_type", "ALL")
                        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", CURRENCY_CODE)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                        clsCommon.AddColumnsForChange(coll, "add1", add1)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                        clsCommon.AddColumnsForChange(coll, "City_Code", citycode)
                        clsCommon.AddColumnsForChange(coll, "City_Code_Desc", citycodedesc)
                        'Sanjay 9-Aug-2018 BHA/09/08/18-000407 
                        clsCommon.AddColumnsForChange(coll, "state", state)
                        clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                        clsCommon.AddColumnsForChange(coll, "Status", "N")
                        'Sanjay 9-Aug-2018
                        clsCommon.AddColumnsForChange(coll, "Phone1", phonenum1)
                        clsCommon.AddColumnsForChange(coll, "Email", emailid)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account", vendoracct)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account_Desc", vendoracctdesc)
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                        clsCommon.AddColumnsForChange(coll, "Bank_Code_Desc", strbankdes)
                        clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(grow.Cells("PAN").Value))
                        clsCommon.AddColumnsForChange(coll, "country_code", countrycode)
                        clsCommon.AddColumnsForChange(coll, "Is_Parent_Vendor", isparent)
                        clsCommon.AddColumnsForChange(coll, "Parent_Vendor_Code", parent_vendor_code)
                        clsCommon.AddColumnsForChange(coll, "Pin_Code", strPin_Code)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Type_CHA", strVenType)
                        clsCommon.AddColumnsForChange(coll, "BusinessCondition", BusinessCondition)
                        clsCommon.AddColumnsForChange(coll, "GSTRegistered", Registered)
                        clsCommon.AddColumnsForChange(coll, "GST_Composition_scheme", GST_Composition_scheme)
                        clsCommon.AddColumnsForChange(coll, "GSTEntity", GSTEntity)
                        clsCommon.AddColumnsForChange(coll, "GSTLastEntity", GSTLastEntity)
                        clsCommon.AddColumnsForChange(coll, "GSTFinalNo", GSTFinal)
                        clsCommon.AddColumnsForChange(coll, "GSTMiddle", GSTMiddleEntity)
                        clsCommon.AddColumnsForChange(coll, "Alies_Name", strAliesName)
                        clsCommon.AddColumnsForChange(coll, "DFOption", DFoption)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, trans))
                            AllowAutoVCodeForAllCompany = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCodeForAllCompany, clsFixedParameterCode.AutoGeneratedVendorCodeForAllCompany, trans))
                            If clsCommon.CompairString(AllowAutoVCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(strvendorNo.Trim()) <= 0 Then
                                If clsCommon.CompairString(AllowAutoVCodeForAllCompany, "1") = CompairStringResult.Equal Then
                                    strvendorNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VendorMaster, "", "")
                                Else
                                    strvendorNo = clsERPFuncationality.GetVendorNextCode("TSPL_VENDOR_MASTER", "Vendor_Name", strvendorname, trans)
                                End If
                            Else
                                strvendorNo = strvendorNo
                            End If
                            If clsCommon.myLen(strvendorNo) <= 0 Then
                                Throw New Exception("Error in vendor code generation")
                            End If

                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", strvendorNo)

                            clsCommon.AddColumnsForChange(coll, "Created_By", strvendorNo)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strvendorNo, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "TSPL_VENDOR_MASTER.vendor_code='" + strvendorNo + "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no: " + Count + " - " + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Vendor Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton34_Click(sender As Object, e As EventArgs) Handles RadButton34.Click
        Try
            Me.Text = "Customer Account Set"
            Dim query As String = "select cust_account as 'Customer Account',cust_acct_desc as 'Customer Account Description',receivable_control_acct as 'Debtor Control',receipts_discount_acct as 'Receipt Discount',Advance_acct as 'Advance Account',Write_offs as 'Write Offs' , Container_Deposit as 'Container Deposite',SECURITY_ACCOUNT as 'Security Account',CREATE_SECURITY_ACCOUNT as 'Create Security Account',BANK_GUARANTEE as 'Bank Guarantee' from tspl_customer_account_set"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Account Set")
        End Try
    End Sub

    Private Function CheckAccountCode(ByVal AccountCode As String, ByVal AcccountFor As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(AccountCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from tspl_gl_accounts where account_code='" + AccountCode + "' ", trans) > 0 Then
            Else
                Throw New Exception("Invalid " + AcccountFor + " Account Code- " + AccountCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub RadButton33_Click(sender As Object, e As EventArgs) Handles RadButton33.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try

            Dim NullVAlue As String = Nothing
            If transportSql.importExcel(dgv, "Customer Account", "Customer Account Description", "Debtor Control", "Receipt Discount", "Advance Account", "Write Offs", "Container Deposite", "Security Account", "Create Security Account", "Bank Guarantee") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If

                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        linno += 1
                        Dim strcustmaster As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                        Dim strcustaccountdisc As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                        Dim strreceivablecontrol As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                        If clsCommon.myLen(strreceivablecontrol) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreceivablecontrol + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled debtor control (" & strreceivablecontrol & ") does not exist.")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreceivablecontrol + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled debtor control (" & strreceivablecontrol & ") must be control account.")
                            End If
                        End If
                        ''
                        Dim strrecieptdiscount As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                        If clsCommon.myLen(strrecieptdiscount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strrecieptdiscount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled receipt discount (" & strrecieptdiscount & ") does not exist.")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strrecieptdiscount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled receipt discount (" & strrecieptdiscount & ") must be control account.")
                            End If
                        End If
                        ''
                        Dim stradvanceaccount As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                        If clsCommon.myLen(stradvanceaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled advance account (" & stradvanceaccount & ") does not exist.")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled advance account (" & stradvanceaccount & ") must be control account.")
                            End If
                        End If
                        ''

                        Dim strwriteoff As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                        If clsCommon.myLen(strwriteoff) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strwriteoff + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled write offs (" & strwriteoff & ") does not exist.")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strwriteoff + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled write offs (" & strwriteoff & ") must be control account.")
                            End If
                        End If
                        ''

                        Dim ContainerDeposite As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                        If clsCommon.myLen(ContainerDeposite) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ContainerDeposite + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled container deposite (" & ContainerDeposite & ") does not exist.")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ContainerDeposite + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled container deposite (" & ContainerDeposite & ") must be control account.")
                            End If
                        End If
                        ''
                        Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        Dim Debtor As String = clsCommon.myCstr(dgrv.Cells("Debtor Control").Value)
                        Dim ReceiptDiscount As String = clsCommon.myCstr(dgrv.Cells("Receipt Discount").Value)
                        Dim write As String = clsCommon.myCstr(dgrv.Cells("Write Offs").Value)

                        Dim Securityaccount As String = clsCommon.myCstr(dgrv.Cells("Security Account").Value)
                        Dim CreateSecurityaccount As String = clsCommon.myCstr(dgrv.Cells("Create Security Account").Value)
                        Dim BankGuaranteeaccount As String = clsCommon.myCstr(dgrv.Cells("Bank Guarantee").Value)

                        If String.IsNullOrEmpty(Debtor) Or Debtor.Length > 50 Then
                            Throw New Exception("Debtor Control Length cannot be greater than 50 or blank")
                        End If

                        If String.IsNullOrEmpty(ReceiptDiscount) Or ReceiptDiscount.Length > 50 Then
                            Throw New Exception("Receipt Discount Length cannot be greater than 50 or blank")
                        End If

                        If String.IsNullOrEmpty(write) Or write.Length > 50 Then
                            Throw New Exception("Write Offs Length cannot be greater than 50 or blank")
                        End If

                        If String.IsNullOrEmpty(Securityaccount) Or Securityaccount.Length > 50 Then
                            Throw New Exception("Security Account Length cannot be greater than 50 or blank")
                        End If

                        If String.IsNullOrEmpty(CreateSecurityaccount) Or CreateSecurityaccount.Length > 50 Then
                            Throw New Exception("Create Security Account Length cannot be greater than 50  or blank")
                        End If
                        If String.IsNullOrEmpty(BankGuaranteeaccount) Or BankGuaranteeaccount.Length > 50 Then
                            Throw New Exception("Bank Guarantee Account Length cannot be greater than 50  or blank")
                        End If

                        If clsCommon.myLen(Debtor) > 0 Then
                            If CheckAccountCode(Debtor, "Debtor Control", trans) = False Then
                                Exit Sub
                            End If
                        End If
                        If clsCommon.myLen(ReceiptDiscount) > 0 Then
                            If CheckAccountCode(ReceiptDiscount, "Receipt Discount", trans) = False Then
                                Exit Sub
                            End If

                        End If
                        If (String.IsNullOrEmpty(stradvanceaccount)) Or clsCommon.myLen(stradvanceaccount) > 0 Then
                            If CheckAccountCode(stradvanceaccount, "Advance", trans) = False Then
                                Exit Sub
                            End If
                        End If
                        If clsCommon.myLen(write) > 0 Then
                            If CheckAccountCode(write, "Write Offs", trans) = False Then
                                Exit Sub
                            End If

                        End If
                        If (String.IsNullOrEmpty(ContainerDeposite)) Or clsCommon.myLen(ContainerDeposite) > 0 Then
                            If CheckAccountCode(ContainerDeposite, "Container Deposite", trans) = False Then
                                Exit Sub
                            End If
                        End If
                        If clsCommon.myLen(Securityaccount) > 0 Then
                            If CheckAccountCode(Securityaccount, "Security Account", trans) = False Then
                                Exit Sub
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Securityaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled security account (" & Securityaccount & ") must be control account.")
                            End If

                        End If
                        If clsCommon.myLen(CreateSecurityaccount) > 0 Then
                            If CheckAccountCode(CreateSecurityaccount, "Create Security Account", trans) = False Then
                                Exit Sub
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CreateSecurityaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled create security account (" & CreateSecurityaccount & ") must be control account.")
                            End If
                        End If
                        If clsCommon.myLen(BankGuaranteeaccount) > 0 Then
                            If CheckAccountCode(BankGuaranteeaccount, "Bank Guarantee", trans) = False Then
                                Exit Sub
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + BankGuaranteeaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled bank guarantee account (" & BankGuaranteeaccount & ") must be control account.")
                            End If
                        End If
                        If String.IsNullOrEmpty(strcustmaster) Or strcustmaster.Length > 12 Then
                            Throw New Exception("Customer Acccount has some incorrect values")
                        End If
                        Dim sql1 As String = "select count(*) from tspl_customer_account_set  where cust_account='" + strcustmaster + "'"
                        Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        Dim coll As New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "cust_acct_desc", strcustaccountdisc)
                        clsCommon.AddColumnsForChange(coll, "receivable_control_acct", strreceivablecontrol)
                        clsCommon.AddColumnsForChange(coll, "receipts_discount_acct", strrecieptdiscount)
                        clsCommon.AddColumnsForChange(coll, "Advance_acct", stradvanceaccount)
                        clsCommon.AddColumnsForChange(coll, "Write_offs", strwriteoff)
                        clsCommon.AddColumnsForChange(coll, "Container_Deposit", ContainerDeposite)
                        clsCommon.AddColumnsForChange(coll, "SECURITY_ACCOUNT", Securityaccount)
                        clsCommon.AddColumnsForChange(coll, "CREATE_SECURITY_ACCOUNT", CreateSecurityaccount)
                        clsCommon.AddColumnsForChange(coll, "BANK_GUARANTEE", BankGuaranteeaccount)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)
                        If (i = 0) Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Account", strcustmaster)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ACCOUNT_SET", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ACCOUNT_SET", OMInsertOrUpdate.Update, "cust_account='" + strcustmaster + "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no " + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Account Set")
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton32_Click(sender As Object, e As EventArgs) Handles RadButton32.Click
        Try
            Me.Text = "Customer Group"
            qry = "select Cust_Group_Code as GroupCode,Cust_Group_Desc as GroupDescription,Tax_Group as TaxGroup,Cust_Account as CustomerAccountSet,Terms_Code as TermCode from TSPL_CUSTOMER_GROUP_MASTER  "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Group")
        End Try
    End Sub

    Private Sub RadButton31_Click(sender As Object, e As EventArgs) Handles RadButton31.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            If transportSql.importExcel(gv, "GroupCode", "GroupDescription", "TaxGroup", "CustomerAccountSet", "TermCode") Then
                clsCommon.ProgressBarPercentShow()
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strCustGroup As String
                        Dim strCustDesc As String
                        Dim strAccSet As String
                        Dim strTaxGroup As String
                        Dim strTermsCode As String
                        If clsCommon.myLen(grow.Cells(0).Value) <= 0 Then
                            Throw New Exception("Customer Group can't be Blank")
                        ElseIf clsCommon.myLen(grow.Cells(0).Value) > 12 Then
                            Throw New Exception("Customer Group cannot be greater than 12 length")
                        Else
                            strCustGroup = clsCommon.myCstr(grow.Cells(0).Value).ToUpper()
                        End If
                        If clsCommon.myLen(grow.Cells(1).Value) > 50 Then
                            Throw New Exception("Description cannot be greater than 50")
                        Else
                            strCustDesc = clsCommon.myCstr(grow.Cells(1).Value)
                        End If
                        If clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                            Throw New Exception("Tax Group cannot be greater than 12")
                        Else
                            strTaxGroup = clsCommon.myCstr(grow.Cells(2).Value)
                            strTaxGroup = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Code from TSPL_TAX_GROUP_MASTER  Where  Tax_Group_Code='" + strTaxGroup + "'  ", trans))
                            If clsCommon.myLen(strTaxGroup) <= 0 Then
                                Throw New Exception("Invalid Tax Group")
                            End If
                        End If
                        If clsCommon.myLen(grow.Cells(3).Value) > 12 Then
                            Throw New Exception("AccountSet cannot be greater than 12 length.")
                        Else
                            strAccSet = clsCommon.myCstr(grow.Cells(3).Value).ToUpper()
                            strAccSet = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + strAccSet + "'", trans))
                            If clsCommon.myLen(strAccSet) <= 0 Then
                                Throw New Exception("Invalid customer account set")
                            End If
                        End If

                        If clsCommon.myLen(grow.Cells(4).Value) > 50 Then
                            Throw New Exception("Terms Code cannot be greater than 50 length.")
                        Else
                            strTermsCode = clsCommon.myCstr(grow.Cells(4).Value).ToUpper()

                            strTermsCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code from TSPL_TERMS_MASTER where Terms_Code='" + strTermsCode + "'", trans))
                            If clsCommon.myLen(strTermsCode) <= 0 Then
                                Throw New Exception("Invalid Term Code")
                            End If
                        End If
                        Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

                        Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code='" + strCustGroup + "'"
                        Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Cust_Group_Desc", strCustDesc)
                        clsCommon.AddColumnsForChange(coll, "Tax_Group", strTaxGroup)
                        clsCommon.AddColumnsForChange(coll, "Cust_Account", strAccSet)
                        clsCommon.AddColumnsForChange(coll, "Terms_Code", strTermsCode)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        If i = 0 Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", strCustGroup)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_GROUP_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCustGroup), "TSPL_CUSTOMER_GROUP_MASTER", "Cust_Group_Code", trans)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_GROUP_MASTER", OMInsertOrUpdate.Update, "Cust_Group_Code='" + strCustGroup + "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no " + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                Finally
                    clsCommon.ProgressBarPercentHide()
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Group")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton30_Click(sender As Object, e As EventArgs) Handles RadButton30.Click
        Try
            Me.Text = "Customer Master"
            qry = "SELECT TSPL_CUSTOMER_MASTER.[Cust_Code] as [Customer Code],TSPL_CUSTOMER_MASTER.[Customer_Name] as [Name] ,TSPL_CUSTOMER_MASTER.Add1 AS [ADDRESS1],TSPL_CUSTOMER_MASTER.[Cust_Group_Code] as [Group Code],TSPL_CUSTOMER_MASTER.Cust_Type_Code as [Customer Type Code],TSPL_CUSTOMER_MASTER.Route_No as [Route No],TSPL_CUSTOMER_MASTER.Route_Desc as [Route Description],TSPL_CUSTOMER_MASTER.City_Code as [City Code],TSPL_CUSTOMER_MASTER.[State] as [State],TSPL_CUSTOMER_MASTER.Country as [Country],TSPL_CUSTOMER_MASTER.Phone1 as [Phone Num1],TSPL_CUSTOMER_MASTER.Phone2 as [Phone Num2],TSPL_CUSTOMER_MASTER.PIN_NO as [Pin No],TSPL_CUSTOMER_MASTER.Email as [Email Id],TSPL_CUSTOMER_MASTER.[Cust_Account] as [Account Set],TSPL_CUSTOMER_MASTER.Credit_Limit as [Credit Limit],TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Customer No],TSPL_CUSTOMER_MASTER.Price_CodeNon as [Price Code Non-Excise],TSPL_CUSTOMER_MASTER.Parent_Customer_YN as [Parent Customer],TSPL_CUSTOMER_MASTER.CURRENCY_CODE as [CURRENCY CODE],TSPL_CUSTOMER_MASTER.GSTNO AS [GSTIN NO],TSPL_CUSTOMER_MASTER.GST_Registered as [Registered]  " & _
            " ,TSPL_CUSTOMER_MASTER.Customer_Category as [Customer Category],TSPL_CUSTOMER_MASTER.Bank_Name as [Bank Name],TSPL_CUSTOMER_MASTER.IFSC_Code as [IFSC Code],TSPL_CUSTOMER_MASTER.Branch_Name as [Branch Name],TSPL_CUSTOMER_MASTER.Account_No  as [Account No] " & _
            " ,TSPL_CUSTOMER_MASTER.RSM as [RSM Code],TSPL_CUSTOMER_MASTER.ZSM as [ZSM Code],TSPL_CUSTOMER_MASTER.ASM as [ASM Code],TSPL_CUSTOMER_MASTER.ASO as [ASO Code],TSPL_CUSTOMER_MASTER.CheckCreditLimit as [Check Credit Limit]" & _
            " FROM TSPL_CUSTOMER_MASTER "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Master")
        End Try
    End Sub

    Private Sub RadButton29_Click(sender As Object, e As EventArgs) Handles RadButton29.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim AllowAutoCCode As String = ""
            Dim AllowAutoCCodeForallCompnay As String = ""
            Dim strCusCode As String = ""
            Dim currentdate As Date = Date.Today
            Dim Input() As String = {}

            Input = {"Customer Code", "Name", "ADDRESS1", "Group Code", "Customer Type Code", "Route No", "Route Description", "City Code", "State", "Country", "Phone Num1", "Phone Num2", "Pin No", "Email Id", "Account Set", "Credit Limit", "Parent Customer No", "Price Code Non-Excise", "Parent Customer", "CURRENCY CODE", "GSTIN NO", "Registered", "Customer Category", "Bank Name", "IFSC Code", "Branch Name", "Account No", "RSM Code", "ZSM Code", "ASM Code", "ASO Code", "Check Credit Limit"}
            Dim strInputlist As List(Of String) = New List(Of String)(Input)
            If transportSql.importExcel(gv, strInputlist.ToArray()) Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim ii As Integer = 0
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        ii += 1
                        strCusCode = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                        Dim coll As New Hashtable()
                        AllowAutoCCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, trans))
                        If clsCommon.CompairString(AllowAutoCCode, "0") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strCusCode) > 0 Then
                                If clsCommon.myLen(strCusCode) > 12 Then
                                    Throw New Exception("Customer code can not be greater than 12 characters")
                                End If
                            Else
                                Throw New Exception("Customer code can not be left blank .")
                            End If
                        End If



                        Dim CustName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                        If clsCommon.myLen(CustName) > 0 Then
                            If clsCommon.myLen(CustName) > 200 Then
                                Throw New Exception("Please enter customer name against code '" + strCusCode + "' having max length 200.")
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CustomerNameUniqueOnCM & "' and Type ='" & clsFixedParameterType.CustomerNameUniqueOnCM & "'", trans)), "1") = CompairStringResult.Equal Then
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUSTOMER_MASTER where Customer_Name='" & clsCommon.myCstr(CustName) & "' and Cust_Code<>'" & clsCommon.myCstr(strCusCode) & "'", trans)) > 0 Then
                                    Throw New Exception("Same Customer Name is exist with another customer so please change customer name because Customer Name is unique .")
                                End If
                            End If

                        Else
                            Throw New Exception("Please enter customer name against code '" + strCusCode + "' .")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Customer_Name", CustName)



                        Dim strParentCstmrNo As String = clsCommon.myCstr(grow.Cells("Parent Customer No").Value)
                        If clsCommon.myLen(strParentCstmrNo) > 12 Then
                            Throw New Exception("Check the length  of Parent Customer No ")
                            trans.Rollback()
                            Exit Sub
                        End If
                        Dim StateCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_CODE  from TSPL_State_MASTER where STATE_CODE ='" + clsCommon.myCstr(grow.Cells("State").Value) + "'", trans))
                        If clsCommon.myLen(StateCode) <= 0 Then
                            Throw New Exception("Please Enter Valid State at line no:" & (grow.Index + 1))
                        End If

                        Dim strGstNo As String = ""

                        Dim strRegistered As Integer = 0
                        Dim GstStateCode As String = ""
                        Dim StrGstEntity As String = ""
                        Dim strGstDefault As String = ""
                        Dim strGstDigit As String = ""
                        strRegistered = clsCommon.myCdbl(grow.Cells("Registered").Value)

                        If strRegistered <> 1 AndAlso strRegistered <> 0 Then

                            Throw New Exception("Please Enter Register as 0 or 1 at line no:." & (grow.Index + 1))
                        Else
                            If strRegistered = 1 Then
                                strGstNo = clsCommon.myCstr(grow.Cells("GSTIN NO").Value)
                                If clsCommon.myLen(strGstNo) <= 0 Then
                                    Throw New Exception("Please Enter GST No. at line no:." & (grow.Index + 1))
                                End If
                                Dim strqry As String = "select  GST_STATE_Code  from TSPL_State_MASTER where STATE_CODE ='" + StateCode + "'"
                                GstStateCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strqry, trans))

                                Try
                                    'StrMsg = clsERPFuncationality.ValidationGSTNO(GstStateCode, strPanNo, strGstNo, trans)
                                Catch ex As Exception
                                    Throw New Exception(ex.Message & "at line no:." & (grow.Index + 1))
                                End Try



                                StrGstEntity = strGstNo.Trim().Substring(12, 1)
                                strGstDefault = strGstNo.Trim().Substring(13, 1)
                                strGstDigit = strGstNo.Trim().Substring(14, 1)
                            End If

                        End If



                        clsCommon.AddColumnsForChange(coll, "GSTNO", strGstNo)
                        clsCommon.AddColumnsForChange(coll, "GSTEntity", StrGstEntity)
                        clsCommon.AddColumnsForChange(coll, "GSTBlank", strGstDefault)
                        clsCommon.AddColumnsForChange(coll, "GSTDigit", strGstDigit)
                        clsCommon.AddColumnsForChange(coll, "GST_Registered", strRegistered)

                        Dim strCusGrp As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If clsCommon.myLen(strCusGrp) > 0 Then
                            Dim custgroup As String = clsDBFuncationality.getSingleValue("select Cust_Group_Code  from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strCusGrp + "'", trans)
                            If clsCommon.CompairString(custgroup, strCusGrp) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", custgroup)
                            Else
                                Throw New Exception("This Customer Group Code does not Exist Against Customer '" + strCusCode + "' ")
                            End If
                        Else
                            Throw New Exception("Please Fill the Group Code,it is mandatory Against Customer '" + strCusCode + "'")
                            Exit Sub
                        End If









                        clsCommon.AddColumnsForChange(coll, "Price_CodeNon", clsCommon.myCstr(grow.Cells("Price Code Non-Excise").Value))

                        Dim CusAccount As String = clsCommon.myCstr(grow.Cells("Account Set").Value)
                        Dim Cust_Account As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account ='" + CusAccount + "'", trans))
                        If clsCommon.CompairString(Cust_Account, CusAccount) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Account", Cust_Account, True)
                        Else
                            Throw New Exception(" This Customer Account does not exist In Master ")
                        End If



                        Dim Route_No As String = ""
                        Dim RouteNo As String = clsCommon.myCstr(grow.Cells("Route No").Value)
                        If clsCommon.myLen(grow.Cells("Route No").Value) > 0 Then
                            Route_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_No from TSPL_ROUTE_MASTER Where Route_No='" + RouteNo + "'", trans))
                            If clsCommon.CompairString(Route_No, RouteNo) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Route_No", Route_No)
                            Else
                                Throw New Exception("The Route No Does Not Exists for Customer '" + strCusCode + "' ")
                            End If
                        Else
                            clsCommon.AddColumnsForChange(coll, "Route_No", Route_No, True)
                        End If






                        Dim CreditLimit As String = clsCommon.myCdbl(grow.Cells("Credit Limit").Value)
                        clsCommon.AddColumnsForChange(coll, "Credit_Limit", CreditLimit)



                        Dim Cuurency_Code As String
                        Dim CuurencyCode As String = clsCommon.myCstr(grow.Cells("CURRENCY CODE").Value)
                        If clsCommon.myLen(CuurencyCode) <= 0 Then
                            If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                                Throw New Exception("Please set Currency Code in Company Master")
                            End If
                            CuurencyCode = objCommonVar.BaseCurrencyCode
                        End If
                        Cuurency_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CURRENCY_CODE  from TSPL_CURRENCY_MASTER Where CURRENCY_CODE='" + CuurencyCode + "'", trans))
                        If clsCommon.CompairString(Cuurency_Code, CuurencyCode) = CompairStringResult.Equal Then
                            Dim qry As String
                            qry = "select CURRENCY_CODE from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" & clsCommon.myCstr(Cust_Account) & "' "
                            Dim accCurrCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Cuurency_Code)) <> CompairStringResult.Equal Then
                                Throw New Exception("Account Set Currency and Customer Currency must be same in case of Multicurrency  ")
                            End If

                            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", Cuurency_Code)
                        Else
                            Throw New Exception("This Currency Code Does not Exist in Currency Master ")

                        End If



                        clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(grow.Cells("ADDRESS1").Value))
                        Dim strCustType As String = String.Empty
                        Dim CustType As String = clsCommon.myCstr(grow.Cells("Customer Type Code").Value).ToUpper()
                        If clsCommon.myLen(CustType) <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", CustType, True)
                        Else
                            Dim Dt As DataTable = clsDBFuncationality.GetDataTable("SELECT  Cust_Type_Code FROM [TSPL_CUSTOMER_TYPE_MASTER]", trans)
                            Dim arrCustType As New List(Of String)
                            For Each dr As DataRow In Dt.Rows
                                arrCustType.Add(clsCommon.myCstr(dr("Cust_Type_Code")))
                            Next
                            If Not arrCustType.Contains(CustType) Then
                                Throw New Exception("Please Insert Customer Type among (" + clsCommon.GetMulcallString(arrCustType) + ") ")
                            Else
                                strCustType = clsDBFuncationality.getSingleValue("SELECT  Cust_Type_Code FROM [TSPL_CUSTOMER_TYPE_MASTER] Where Cust_Type_Code='" + CustType + "'", trans)
                                clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", strCustType)
                            End If
                        End If
                        Dim Route_Desc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" + Route_No + "'", trans))
                        clsCommon.AddColumnsForChange(coll, "Route_Desc", Route_Desc)

                        Dim ctycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                        Dim city_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Code  from TSPL_CITY_MASTER Where City_Code='" + ctycode + "'", trans))
                        If clsCommon.CompairString(city_Code, ctycode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "City_Code", city_Code)
                        Else
                            Throw New Exception("This City Code does not exist ")
                        End If

                        clsCommon.AddColumnsForChange(coll, "State", clsCommon.myCstr(grow.Cells("State").Value))
                        clsCommon.AddColumnsForChange(coll, "Country", clsCommon.myCstr(grow.Cells("Country").Value))
                        clsCommon.AddColumnsForChange(coll, "Phone1", clsCommon.myCstr(grow.Cells("Phone Num1").Value))
                        clsCommon.AddColumnsForChange(coll, "PIN_NO", clsCommon.myCstr(grow.Cells("Pin No").Value))
                        clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(grow.Cells("Email Id").Value))
                        clsCommon.AddColumnsForChange(coll, "Customer_Class", strCustType)
                        Dim parentcustomer As String
                        parentcustomer = clsCommon.myCstr(grow.Cells("Parent customer").Value)
                        If String.IsNullOrEmpty(parentcustomer) Or clsCommon.CompairString(parentcustomer, "N") = CompairStringResult.Equal Then
                            parentcustomer = "N"
                        ElseIf clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                            parentcustomer = "Y"
                        Else
                            Throw New Exception("Please Enter Only Y or N as Parent Customer ")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                        clsCommon.AddColumnsForChange(coll, "Parent_Customer_YN", parentcustomer)
                        ''richa agarwal
                        If clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                            strParentCstmrNo = ""
                        End If
                        clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", strParentCstmrNo)
                        ''===============

                        Dim Customer_Category = clsCommon.myCstr(grow.Cells("Customer Category").Value)
                        If clsCommon.myLen(Customer_Category) > 0 Then
                            If Customer_Category.ToString.ToUpper <> "VENDOR" AndAlso Customer_Category.ToString.ToUpper <> "INSTITUTION CR" AndAlso Customer_Category.ToString.ToUpper <> "INSTITUTION SO" AndAlso Customer_Category.ToString.ToUpper <> "DISTRIBUTOR" AndAlso Customer_Category.ToString.ToUpper <> "OTHERS" Then
                                Throw New Exception("Customer Category Should be Vendor/Institution CR/Institution SO/Distributor/Others Offer at line no:." & (grow.Index + 1))
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "Customer_Category", Customer_Category, True)

                        'Bank Detail
                        Dim BankName = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                        Dim IFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                        Dim BranchName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                        Dim AccountNo = clsCommon.myCstr(grow.Cells("Account No").Value)
                        clsCommon.AddColumnsForChange(coll, "Bank_Name", BankName, True)
                        clsCommon.AddColumnsForChange(coll, "IFSC_Code", IFSCCode, True)
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", BranchName, True)
                        clsCommon.AddColumnsForChange(coll, "Account_No", AccountNo, True)

                        ''''''''''''''''''''''
                        Dim strRSMCode As String = clsCommon.myCstr(grow.Cells("RSM Code").Value)
                        If String.IsNullOrEmpty(strRSMCode) Then

                        Else
                            Dim iRSM As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strRSMCode + "'", trans))
                            If iRSM = 0 Then
                                Throw New Exception("This RSM Code does not Exist in Master at line no: " & (grow.Index + 1))
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "RSM", strRSMCode, True)

                        Dim strZSMCode As String = clsCommon.myCstr(grow.Cells("ZSM Code").Value)
                        If String.IsNullOrEmpty(strZSMCode) Then

                        Else
                            Dim iZSM As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strZSMCode + "'", trans))
                            If iZSM = 0 Then
                                Throw New Exception("This ZSM Code does not Exist in Master at line no: " & (grow.Index + 1))
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "ZSM", strZSMCode, True)

                        Dim strASMCode As String = clsCommon.myCstr(grow.Cells("ASM Code").Value)
                        If String.IsNullOrEmpty(strASMCode) Then

                        Else
                            Dim iASM As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strASMCode + "'", trans))
                            If iASM = 0 Then
                                Throw New Exception("This ASM Code does not Exist in Master at line no: " & (grow.Index + 1))
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "ASM", strASMCode, True)

                        Dim strASOCode As String = clsCommon.myCstr(grow.Cells("ASO Code").Value)
                        If String.IsNullOrEmpty(strASOCode) Then

                        Else
                            Dim iASO As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strASOCode + "'", trans))
                            If iASO = 0 Then
                                Throw New Exception("This ASO Code does not Exist in Master at line no:" & (grow.Index + 1))
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "ASO", strASOCode, True)
                        ''''''''''''''''''''''''

                        Dim CheckCreditLimit As Integer = clsCommon.myCdbl(grow.Cells("Check Credit Limit").Value)
                        clsCommon.AddColumnsForChange(coll, "CheckCreditLimit", CheckCreditLimit)

                        Dim dblCrateOpeningQuantity As Double = 0
                        clsCommon.AddColumnsForChange(coll, "Crate_Opening", dblCrateOpeningQuantity)
                        clsCommon.AddColumnsForChange(coll, "Crate_Opening_Date", Nothing, True)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        Dim i As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Customer_Master where isnull(Cust_Code,'')<>'' and Cust_Code='" + strCusCode + "'", trans))

                        If (i = 0) Then
                            AllowAutoCCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, trans))
                            AllowAutoCCodeForallCompnay = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCodeForAllCompany, clsFixedParameterCode.AutoGeneratedCustomerCodeForAllCompany, trans))
                            If clsCommon.CompairString(AllowAutoCCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(CustName) > 0 Then
                                If clsCommon.CompairString(AllowAutoCCodeForallCompnay, "1") = CompairStringResult.Equal Then
                                    strCusCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CustomerMaster, "", "")
                                Else
                                    If clsCommon.myLen(strCusCode.Trim()) <= 0 Then
                                        strCusCode = clsERPFuncationality.GetVendorNextCode("TSPL_CUSTOMER_MASTER", "Customer_Name", CustName, trans)
                                    Else
                                        strCusCode = strCusCode
                                    End If
                                End If
                            Else
                                strCusCode = strCusCode
                            End If
                            clsCommon.AddColumnsForChange(coll, "Cust_Code", strCusCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_MASTER.Cust_Code='" + strCusCode + "'", trans)
                        End If
                        If clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CUSTOMER_MASTER set Credit_Limit=" & CreditLimit & " where Parent_Customer_No ='" & strCusCode & "'", trans)
                        End If
                        If clsCommon.myLen(strParentCstmrNo) > 0 Then
                            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CUSTOMER_MASTER set Credit_Limit=(Select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strParentCstmrNo & "') where Cust_Code ='" & strCusCode & "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no - " + clsCommon.myCstr(ii) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton28_Click(sender As Object, e As EventArgs) Handles RadButton28.Click
        Try
            Me.Text = "Item Structure"
            qry = "select m.Structure_Code as [Structure Code], m.Structure_Descq as [Description], m.Item_Structure as [Item Structure], m.Total_Length as [Total Length]  from TSPL_STRUCTURE_MASTER m "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Item Structure")
        End Try
    End Sub

    Private Sub RadButton27_Click(sender As Object, e As EventArgs) Handles RadButton27.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "Structure Code", "Description", "Item Structure", "Total Length") Then
                Dim trans As SqlTransaction = Nothing
                Dim LineNo As Integer = 0
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim str As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                        Dim str1 As String = clsCommon.myCstr(grow.Cells("Description").Value)
                        Dim str2 As String = clsCommon.myCstr(grow.Cells("Item Structure").Value)
                        Dim Totallength As Integer = 0
                        Totallength = clsCommon.myCdbl(grow.Cells("Total Length").Value)
                        If String.IsNullOrEmpty(str) And clsCommon.myLen(str) > 12 Then
                            Throw New Exception("Structure Code has some incorrect values")
                        End If
                        If clsCommon.myLen(str1) > 50 Then
                            Throw New Exception("Description has some incorrect values")
                        End If
                        If clsCommon.myLen(str2) > 50 Then
                            Throw New Exception("Item Structure has some incorrect values")
                        End If
                        Dim sql1 As String = "select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code ='" + str + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i = 0) Then
                            Dim Sql As String = "insert into TSPL_STRUCTURE_MASTER (Structure_Code, Structure_Descq, Item_Structure, Total_Length , Created_By, Create_Date, Modify_By , Modify_Date, Comp_Code ) values ('" + str + "', '" + str1 + "', '" + str2 + "', '" & Totallength & "', '" + objCommonVar.CurrentUserCode + "', '" + connectSql.serverDate(trans) + "', '" + objCommonVar.CurrentUserCode + "', '" + connectSql.serverDate(trans) + "', '" + objCommonVar.CurrentCompanyCode + "')"
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            Dim Sql As String = "UPDATE TSPL_STRUCTURE_MASTER set Structure_Descq='" & str1 & "', Item_Structure='" & str2 & "', Total_Length='" & Totallength & "' where Structure_Code='" & str & "' "
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If

                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line No-" + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Item Structure")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton26_Click(sender As Object, e As EventArgs) Handles RadButton26.Click
        Try
            Me.Text = "Purchase Account Set"
            Dim query As String = "select purchase_class_code as 'Purchase Class Code',purchase_class_desc as 'Purchase Class Description',inv_control_account as 'Inventory Control Account',Inv_payable_clearing as 'Inventory Payable Clearing',Adjustment_Account as [Adjustment Account],Shipment_Clearing as [Shipment Clearing],Wrekage_Account as [Wrekage Account] from tspl_purchase_accounts"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Purchase Account Set")
        End Try
    End Sub

    Private Sub RadButton25_Click(sender As Object, e As EventArgs) Handles RadButton25.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Purchase Class Code", "Purchase Class Description", "Inventory Control Account", "Inventory Payable Clearing", "Adjustment Account", "Shipment Clearing", "Wrekage Account") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    trans = connectSql.OpenConnection.BeginTransaction()
                    clsCommon.ProgressBarPercentShow()

                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        linno += 1
                        Dim ISIndentReq As Integer = 0
                        Dim strpurchaseclasscode As String = clsCommon.myCstr(dgrv.Cells("Purchase Class Code").Value)
                        Dim strpurchaseclassdescription As String = clsCommon.myCstr(dgrv.Cells("Purchase Class Description").Value)
                        Dim strinventorycontrolaccount As String = clsCommon.myCstr(dgrv.Cells("Inventory Control Account").Value)
                        Dim strinventorypayableclearing As String = clsCommon.myCstr(dgrv.Cells("Inventory Payable Clearing").Value)
                        Dim strWreckageAccount As String = clsCommon.myCstr(dgrv.Cells("Wrekage Account").Value)

                        If clsCommon.myLen(strinventorycontrolaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorycontrolaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled Inventory Control Account (" & strinventorycontrolaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorycontrolaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled Inventory Control Account (" & strinventorycontrolaccount & ") must be control account.")
                            End If
                        Else
                            Throw New Exception("Please enter Inventory Control Account")
                        End If


                        If clsCommon.myLen(strinventorypayableclearing) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorypayableclearing + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled inventory payable clearing (" & strinventorypayableclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorypayableclearing + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled inventory payable clearing (" & strinventorypayableclearing & ") must be control account.")
                            End If
                        Else
                            Throw New Exception("Please enter payable clearing account.")
                        End If
                        Dim strshipmentclearing As String = clsCommon.myCstr(dgrv.Cells("Shipment Clearing").Value)
                        If clsCommon.myLen(strshipmentclearing) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strshipmentclearing + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled shipment clearing (" & strshipmentclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strshipmentclearing + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled shipment clearing (" & strshipmentclearing & ") must be control account.")
                            End If
                        Else
                            Throw New Exception("Please enter Shipment Clearing account.")
                        End If

                        Dim stradjustaccount As String = clsCommon.myCstr(dgrv.Cells("Adjustment Account").Value)
                        If clsCommon.myLen(stradjustaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradjustaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled adjustment account (" & stradjustaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradjustaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled adjustment account (" & stradjustaccount & ") must be control account.")
                            End If
                        Else
                            Throw New Exception("Please enter Adjustment Account.")
                        End If
                        If String.IsNullOrEmpty(strpurchaseclasscode) Or clsCommon.myLen(strpurchaseclasscode) > 6 Then
                            Throw New Exception("Purchase Acccount length should not be greater than six")

                        End If

                        If clsCommon.myLen(strWreckageAccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWreckageAccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled inventory payable clearing (" & strWreckageAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWreckageAccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled inventory payable clearing (" & strWreckageAccount & ") must be control account.")
                            End If

                        End If

                        Dim query As String = "select count(*)from tspl_purchase_accounts where purchase_class_code='" + strpurchaseclasscode + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, query))
                        Dim COLL As New Hashtable
                        clsCommon.AddColumnsForChange(COLL, "Purchase_Class_Code", strpurchaseclasscode)
                        clsCommon.AddColumnsForChange(COLL, "purchase_class_desc", strpurchaseclassdescription)
                        clsCommon.AddColumnsForChange(COLL, "inv_control_account", strinventorycontrolaccount)
                        clsCommon.AddColumnsForChange(COLL, "Inv_payable_clearing", strinventorypayableclearing)
                        clsCommon.AddColumnsForChange(COLL, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(COLL, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(COLL, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(COLL, "Adjustment_Account", stradjustaccount, True)
                        clsCommon.AddColumnsForChange(COLL, "Shipment_Clearing", strshipmentclearing, True)
                        clsCommon.AddColumnsForChange(COLL, "Wrekage_Account", strWreckageAccount, True)
                        If (i = 0) Then
                            clsCommon.AddColumnsForChange(COLL, "Chilling_Charges", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Freight_Charges", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Provision_Clearing", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Purchase_Account", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Purchase_Set_Off", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Stock_Transfer_In", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Stock_Transfer_Acc", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Assembly_Cost_Credit", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Non_Stock_Clearing", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Transfer_Clearing", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Disassembly_Expense", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Physical_Inv_Adjustment", Nothing, True)
                            clsCommon.AddColumnsForChange(COLL, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(COLL, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_PURCHASE_ACCOUNTS", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strpurchaseclasscode, "TSPL_PURCHASE_ACCOUNTS", "Purchase_Class_Code", trans)
                            clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_PURCHASE_ACCOUNTS", OMInsertOrUpdate.Update, "tspl_purchase_accounts.Purchase_Class_Code='" + strpurchaseclasscode + "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no-" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                    myMessages.myExceptions(ex)
                End Try
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Purchase Account Set")
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton16_Click(sender As Object, e As EventArgs) Handles RadButton16.Click
        Try
            Me.Text = "Sale Account Set"
            qry = "select sales_class_code as 'Sales Class Code',sales_class_desc as 'Sales Class Description',sales_account as 'Sales Account',sales_return_account as 'Sales Return Account',Cost_of_goods_sold as 'Cost Of Goods Sold',Cost_Variance as 'Cost Variance',Damaged_goods as 'Damaged Goods',Internal_Usage  as 'Internal Usages',Returnable_Container as 'Returnable Container',schemes as [Schemes],promotional as [Promotional],Cogs_InterBranch as [Cogs InterBranch], Suspence_Account as 'Suspence Account',Stock_transfer_ac as [Stock Transfer AC], COGT_AC as [Cost Of Goods Transfer AC] from tspl_sales_accounts "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Sale Account Set")
        End Try
    End Sub

    Private Sub RadButton15_Click(sender As Object, e As EventArgs) Handles RadButton15.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Sales Class Code", "Sales Class Description", "Sales Account", "Sales Return Account", "Cost Of Goods Sold", "Cost Variance", "Damaged Goods", "Internal Usages", "Returnable Container", "Schemes", "Promotional", "Cogs InterBranch", "Suspence Account", "Stock Transfer AC", "Cost Of Goods Transfer AC") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()


                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        linno += 1
                        Dim strsalesclasscode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                        Dim strdescription As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                        Dim strsalesaccount As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                        If clsCommon.myLen(clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value)) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled Stock Transfer account ( " & clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value)) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled Cost Of Goods Transfer AC ( " & clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        If clsCommon.myLen(strsalesaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled sales account ( " & strsalesaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled sales account ( " & strsalesaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strsalesreturnaccount As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                        If clsCommon.myLen(strsalesreturnaccount) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesreturnaccount + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled sales return account ( " & strsalesreturnaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesreturnaccount + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled sales return account ( " & strsalesreturnaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strcostofgoodsold As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                        If clsCommon.myLen(strcostofgoodsold) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofgoodsold + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled cost of goods sold ( " & strcostofgoodsold & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofgoodsold + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled cost of goods sold ( " & strcostofgoodsold & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strcostofvarience As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                        If clsCommon.myLen(strcostofvarience) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofvarience + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled cost variance ( " & strcostofvarience & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofvarience + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled cost variance ( " & strcostofvarience & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strdamagedgoods As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                        If clsCommon.myLen(strdamagedgoods) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdamagedgoods + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled damaged goods ( " & strdamagedgoods & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdamagedgoods + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled damaged goods ( " & strdamagedgoods & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strinternalusages As String = clsCommon.myCstr(dgrv.Cells(7).Value)
                        If clsCommon.myLen(strinternalusages) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinternalusages + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled internal usages ( " & strinternalusages & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinternalusages + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled internal usages ( " & strinternalusages & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strreturn As String = clsCommon.myCstr(dgrv.Cells(8).Value)
                        If clsCommon.myLen(strreturn) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreturn + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled returnable container ( " & strreturn & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreturn + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled returnable container ( " & strreturn & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strschemes As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                        If clsCommon.myLen(strschemes) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strschemes + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled schemes ( " & strschemes & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strschemes + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled schemes ( " & strschemes & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strpromotional As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                        If clsCommon.myLen(strpromotional) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpromotional + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled promotional ( " & strpromotional & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpromotional + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled promotional ( " & strpromotional & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim cogsInterBranch As String = clsCommon.myCstr(dgrv.Cells("Cogs InterBranch").Value)
                        If clsCommon.myLen(cogsInterBranch) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + cogsInterBranch + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled cogs interBranch ( " & cogsInterBranch & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + cogsInterBranch + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled cogs interBranch ( " & cogsInterBranch & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        Dim strSuspenceAc As String = clsCommon.myCstr(dgrv.Cells("Suspence Account").Value)
                        If clsCommon.myLen(strSuspenceAc) > 0 Then
                            Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAc + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Filled suspence account ( " & strSuspenceAc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAc + "' AND ControlAccount ='Y'"
                            Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                            If check1 <= 0 Then
                                Throw New Exception("Filled suspence account ( " & strSuspenceAc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                        If String.IsNullOrEmpty(strsalesclasscode) Or clsCommon.myLen(strsalesclasscode) > 6 Then
                            Throw New Exception("Sales Acccount has some incorrect values")

                        End If
                        Dim sql1 As String = "select count(*)from tspl_sales_accounts where Sales_class_code='" + strsalesclasscode + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i = 0) Then
                            Dim query As String = "insert into tspl_Sales_accounts(Sales_Class_Code,Sales_Class_Desc,Sales_Account,Sales_Return_Account,Cost_Of_Goods_Sold,Cost_Variance,Damaged_Goods,Internal_Usage,Returnable_Container,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Schemes,Promotional,Cogs_InterBranch,Suspence_Account,Stock_Transfer_AC,COGT_AC) values('" + strsalesclasscode + "','" + strdescription + "','" + strsalesaccount + "','" + strsalesreturnaccount + "','" + strcostofgoodsold + "','" + strcostofvarience + "','" + strdamagedgoods + "','" + strinternalusages + "','" + strreturn + "','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentCompanyCode + "','" + strschemes + "','" + strpromotional + "','" + cogsInterBranch + "','" + strSuspenceAc + "','" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "','" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "')"
                            connectSql.RunSqlTransaction(trans, query)
                        Else
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strsalesclasscode, "TSPL_SALES_ACCOUNTS", "Sales_Class_Code", trans)
                            Dim query1 As String = "update tspl_sales_accounts set sales_class_desc='" + strdescription + "',sales_account='" + strsalesaccount + "',sales_return_account='" + strsalesreturnaccount + "',Cost_of_goods_sold='" + strcostofgoodsold + "',Cost_Variance='" + strcostofvarience + "',Damaged_goods='" + strdamagedgoods + "',Internal_Usage='" + strinternalusages + "',Returnable_Container='" + strreturn + "',Schemes='" + strschemes + "',Promotional='" + strpromotional + "',Cogs_InterBranch='" + cogsInterBranch + "',Suspence_Account='" + strSuspenceAc + "',Stock_transfer_Ac='" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "',COGT_AC='" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "' where sales_class_code='" + strsalesclasscode + "'"
                            connectSql.RunSqlTransaction(trans, query1)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at line no :" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Sale Account Set")
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadButton14_Click(sender As Object, e As EventArgs) Handles RadButton14.Click

        Try
            Me.Text = "Unit Of Measure"
            qry = "select Unit_Code,Unit_Desc,Conv_Factor, Weight_Type,Crate_Type from TSPL_UNIT_MASTER "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Unit Of Measure")
        End Try
    End Sub

    Private Sub RadButton13_Click(sender As Object, e As EventArgs) Handles RadButton13.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "Unit_Code", "Unit_Desc", "Conv_Factor", "Weight_Type", "Crate_Type") Then
                Dim trans As SqlTransaction = Nothing
                Dim lineNo As Integer = 0
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    Dim strUnitCode As String
                    Dim strUnitDesc As String
                    Dim strUnitConvFact As Double
                    Dim Weight_Type As String = "N"
                    Dim Crate_Type As String = "N"
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        lineNo += 1
                        If clsCommon.myLen(grow.Cells(0).Value) > 0 Then
                            If clsCommon.myLen(clsCommon.myLen(grow.Cells(0).Value)) > 12 Then
                                Throw New Exception("Unit Of Measure cannot be greather than 12 length.")
                            Else
                                strUnitCode = clsCommon.myCstr(grow.Cells(0).Value)
                            End If

                            If clsCommon.myLen(clsCommon.myLen(grow.Cells(1).Value)) > 50 Then
                                Throw New Exception("Description cannot be greather than 50 length.")
                            Else
                                strUnitDesc = clsCommon.myCstr(grow.Cells(1).Value)
                            End If

                            If clsCommon.myLen(grow.Cells(2).Value) > 0 Then
                                strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)
                            ElseIf clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                                Throw New Exception("Conversion Factor cannot be greather than 12 length.")

                                If IsNumeric(grow.Cells(2).Value) Then
                                    strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)

                                    If clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                                        Throw New Exception("Conversion Factor cannot be greather than 12 length.")
                                    Else
                                        strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)
                                    End If

                                Else
                                    Throw New Exception("Charactor not allow in Conversion Factor.")
                                End If
                            Else
                                strUnitConvFact = 0
                            End If
                            If clsCommon.myCdbl(strUnitConvFact) = 0 Then
                                Throw New Exception("Converson factor cannot be zero")
                            End If

                            If clsCommon.myLen(grow.Cells("Weight_Type").Value) > 0 Then
                                If clsCommon.CompairString(grow.Cells("Weight_Type").Value, "Y") = CompairStringResult.Equal Then
                                    Weight_Type = "Y"
                                ElseIf clsCommon.CompairString(grow.Cells("Weight_Type").Value, "N") = CompairStringResult.Equal Then
                                    Weight_Type = "N"
                                Else
                                    Throw New Exception("Enter Weight Type As 'Y' Or 'N' Or Left Blank.")
                                End If
                            Else
                                Weight_Type = "N"
                            End If
                            If clsCommon.myLen(grow.Cells("Crate_Type").Value) > 0 Then
                                If clsCommon.CompairString(grow.Cells("Crate_Type").Value.ToString().ToUpper().Trim(), "Y") = CompairStringResult.Equal Then
                                    Crate_Type = "Y"
                                ElseIf clsCommon.CompairString(grow.Cells("Crate_Type").Value.ToString().ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                                    Crate_Type = "N"
                                Else
                                    Throw New Exception("Enter Crate Type As 'Y' Or 'N' Or Left Blank.")
                                End If
                            Else
                                Crate_Type = "N"
                            End If
                            Dim sql1 As String = "select count(*) from TSPL_UNIT_MASTER where Unit_Code='" + strUnitCode + "'"
                            Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                            If (i = 0) Then
                                connectSql.RunSpTransaction(trans, "sp_tspl_unit_master_insert", New SqlParameter("@unitcode", Convert.ToString(strUnitCode)), New SqlParameter("@createprice", "N"), New SqlParameter("@empty", "N"), New SqlParameter("@desc", Convert.ToString(strUnitDesc)), New SqlParameter("@ConvFact", Convert.ToString(strUnitConvFact)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@createdby", objCommonVar.CurrentUser), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUser), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                                clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "' WHERE Unit_Code='" & Convert.ToString(grow.Cells(0).Value) & "'", trans)
                            Else
                                connectSql.RunSpTransaction(trans, "sp_tspl_unit_master_update", New SqlParameter("@unitcode", Convert.ToString(strUnitCode)), New SqlParameter("@Create_Price", "N"), New SqlParameter("@empty", "N"), New SqlParameter("@desc", Convert.ToString(strUnitDesc)), New SqlParameter("@ConvFact", Convert.ToString(strUnitConvFact)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@modifiedby", objCommonVar.CurrentUser), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                                clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "' WHERE Unit_Code='" & Convert.ToString(strUnitCode) & "'", trans)
                            End If
                        End If

                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at line no :" + clsCommon.myCstr(lineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Unit Of Measure")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub


    Private Sub RadButton54_Click(sender As Object, e As EventArgs) Handles RadButton54.Click
        Try
            ' Ticket No : ERO/16/07/19-000948,VIJ/08/11/19-000060,VIJ/19/11/19-000066 By prabhakar
            Me.Text = "Item Master"
            Try
                Dim code As String = ""
                Dim whrcls As String = ""
                qry = " Select TSPL_ITEM_MASTER.Item_Code as [Item Code], Item_Desc as [Item Description],Short_Description as [Short Description], "
                qry += " Purchase_Class_Code as [Purchase A/c Set], Sale_Class_Code as [Sale A/c Set], "
                qry += " Unit_Code as [UOM], Item_Type as [Item Type], "
                qry += " Weight_UOM, Weight_Value,Active,Is_FreshItem,Is_Ambient, Is_CrateType,ISNULL(TSPL_ITEM_MASTER.GL_Account,'') AS [GL Account],(SELECT Description  From TSPL_GL_ACCOUNTS where Account_Code=TSPL_ITEM_MASTER.GL_Account) AS [Account Description],Product_type as [Product Type]"
                Dim UOMTotal As String = ""
                Dim UOMConTotal As String = ""
                Dim UOMDefTotal As String = ""
                Dim UOMStockUnitTotal As String = ""

                Dim UOMConversion As String = ""
                Dim UOMDefault As String = ""
                Dim UOMDetail As String = ""
                Dim UOMStockUnit As String = ""

                Dim TotalUOMWeight As String = ""

                Dim TotalUOMGrossWt As String = ""

                For j As Integer = 1 To 5
                    UOMDetail = "(Select UOM_Code From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,UOM_Code,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS UOM" & j & ""
                    UOMConversion = "(Select Convert (varchar,Conversion_Factor) as Conversion_Factor From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Conversion_Factor,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Conversion Factor" & j & "]"
                    UOMDefault = "(Select Default_UOM From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Default_UOM,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Default UOM" & j & "]"
                    UOMStockUnit = "(Select Stocking_Unit From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Stocking_Unit,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Stocking Unit" & j & "]"
                    UOMTotal = UOMTotal + "," + "" + UOMDetail + ""
                    UOMConTotal = UOMConTotal + "," + "" + UOMConversion + ""
                    UOMDefTotal = UOMDefTotal + "," + "" + UOMDefault + ""
                    UOMStockUnitTotal = UOMStockUnitTotal + "," + "" + UOMStockUnit + ""
                Next
                Dim itemCostForStockingUnit = "(Select Convert (varchar,Item_Cost) as Item_Cost  From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,Item_Cost ) As SNo,Item_Cost,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =1 ) "
                qry += UOMTotal + " " + UOMConTotal + " " + UOMDefTotal + " " + UOMStockUnitTotal + " " + TotalUOMWeight + " " + TotalUOMGrossWt + ",IsTaxable,TSPL_ITEM_MASTER.HSN_Code as 'HSN Code',Structure_Code as [Structure Code],Structure_Desc as [Structure Desc],TSPL_ITEM_MASTER.IS_SCRAP_ITEM as [IS SCRAP ITEM],TSPL_ITEM_MASTER.SCrap_Item_Code as [Scrap Item Code], " + itemCostForStockingUnit + " as [Item Cost(Stocking Unit)], CSA_TYPE as [Item Group Type], Alies_Name as [Alies Name],isnull(TSPL_ITEM_MASTER.Is_Insurance,0) as Is_Insurance,TSPL_ITEM_MASTER.InsuranceNo,TSPL_ITEM_MASTER.InsuranceFromDate,TSPL_ITEM_MASTER.InsuranceToDate,isnull(TSPL_ITEM_MASTER.Marketing_Seq,0) as [Marketing Seq No] from TSPL_ITEM_MASTER  " + code + ""
                transportSql.ExporttoExcel(qry, whrcls, Me)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Item Master")
        End Try
    End Sub

    Private Sub RadButton53_Click(sender As Object, e As EventArgs) Handles RadButton53.Click
        Dim settUpdateItemMasterWithoutTransactionValidation As Boolean = (clsFixedParameter.GetData(clsFixedParameterType.UpdateItemMasterWithoutTransactionValidation, clsFixedParameterCode.UpdateItemMasterWithoutTransactionValidation, Nothing) = 1)
        ' Ticket No : TEC/24/06/19-000565 By Prabhakar
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Try
            Dim LineNo As String = "1"
            Dim countDefaultUOM As Integer = 0
            Dim inputs() As String = {}
            Dim Strs As List(Of String) = New List(Of String)(inputs)
            inputs = {"Item Code", "Item Description", "Short Description", "Purchase A/c Set", "Sale A/c Set", "UOM", "Item Type", "Weight_UOM", "Weight_Value", "Active", "Is_FreshItem", "Is_Ambient", "Is_CrateType", "GL Account", "Account Description", "Product Type", "UOM1", "UOM2", "UOM3", "UOM4", "UOM5", "Conversion Factor1", "Conversion Factor2", "Conversion Factor3", "Conversion Factor4", "Conversion Factor5", "Default UOM1", "Default UOM2", "Default UOM3", "Default UOM4", "Default UOM5", "Stocking Unit1", "Stocking Unit2", "Stocking Unit3", "Stocking Unit4", "Stocking Unit5", "IsTaxable", "HSN Code", "Structure Code", "Structure Desc", "IS SCRAP ITEM", "Scrap Item Code", "Item Cost(Stocking Unit)", "Item Group Type", "Alies Name", "Is_Insurance", "InsuranceNo", "InsuranceFromDate", "InsuranceToDate", "Marketing Seq No"}
            If transportSql.importExcel(gv1, Strs.ToArray()) Then
                Dim isSaved As Boolean = True
                Dim currentdate As Date = Date.Today
                Dim trans As SqlTransaction
                Dim gv2 As New RadGridView
                clsCommon.ProgressBarPercentShow()
                Try
                    Dim Item_Code1 As String = ""
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        Dim Item_Code As String
                        Dim Item_Desc As String
                        Dim Item_Short_Desc As String
                        Dim Structure_Code As String
                        Dim Purchase_Class_Code As String
                        Dim Sale_Class_Code As String
                        Dim Unit_Code As String
                        Dim Item_Type As String
                        Dim Weight_UOM As String
                        Dim wt As String = ""
                        Dim cnvrsnfctr As String = ""
                        Dim stckunit As String = ""
                        Dim Product_Type As String = Nothing
                        Dim GL_Account As String = Nothing
                        Dim jj As Integer = -1
                        Dim ErrCount As Integer = 0
                        Dim HSNCode As String = ""
                        Dim HSNDescription As String = ""
                        Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                        Dim ItemCost As Decimal = 0
                        Dim ItemGroupType As String = Nothing
                        Dim AliesName As String = Nothing
                        Dim Is_Insurance As Integer = 0
                        Dim InsuranceNo As String = ""
                        Dim InsuranceFromDate As String
                        Dim InsuranceToDate As String
                        Dim MarseqNo As Integer = 0
                        For Each grow As GridViewRowInfo In gv1.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv1.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv1.Rows.Count & "")
                            LineNo = clsCommon.myCstr(grow.Index + 2)
                            jj = jj + 1

                            If (clsCommon.myLen(grow.Cells("Item Code").Value) > 0 And clsCommon.myLen(grow.Cells("Item Code").Value) <= 50) OrElse clsCommon.myLen(grow.Cells("Item Description").Value) > 0 Then
                                Dim coll As New Hashtable()
                                Item_Type = clsCommon.myCstr(grow.Cells("Item Type").Value)
                                If Not (clsCommon.CompairString(Item_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "O") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "S") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "T") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "J") = CompairStringResult.Equal) Then
                                    Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ITEM_TYPE_CODE from TSPL_ITEM_TYPE_MASTER where ITEM_TYPE_CODE='" + Item_Type + "'", trans))
                                    If clsCommon.myLen(Item_Type) <= 0 Then
                                        Throw New Exception("Item Type can not be other than ('R' or 'O' or 'A' or 'F' or 'S' or 'T' or 'J')")
                                    End If
                                End If
                                clsCommon.AddColumnsForChange(coll, "Item_Type", Item_Type)
                                '-----------------Item Code-------------------------
                                Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                                If clsCommon.myLen(Item_Code) <= 0 Then
                                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, trans)) = 1 Then
                                        Dim Qry1 As String = "SELECT PREFIX FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                        Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry1, trans))

                                        Dim Qry2 As String = "update TSPL_ITEM_TYPE_MASTER set PREFIX='" + clsCommon.incval(Item_Code) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                        clsDBFuncationality.ExecuteNonQuery(Qry2, trans)
                                    End If
                                End If
                                If clsCommon.myLen(Item_Code) <= 0 Then
                                    Throw New Exception("Item Code not found to save")
                                End If
                                clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                                Item_Desc = clsCommon.myCstr(grow.Cells("Item Description").Value)
                                If clsCommon.myLen(Item_Desc) > 100 Then
                                    Throw New Exception("Length of item description  is greater than 100.")
                                End If
                                clsCommon.AddColumnsForChange(coll, "Item_Desc", Item_Desc)
                                Item_Short_Desc = clsCommon.myCstr(grow.Cells("Short Description").Value)
                                If clsCommon.myLen(Item_Short_Desc) > 200 Then
                                    Throw New Exception("Length of Item Short description  is greater than 200.")
                                End If
                                Dim reccount As Integer = 0
                                Dim Is_Fresh As String = clsCommon.myCstr(grow.Cells("Is_FreshItem").Value)
                                If clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                                    If clsCommon.myLen(Item_Short_Desc) <= 0 Then
                                        Throw New Exception("Short description can not be left blank")
                                    Else
                                        Dim ShortDesp As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row from tspl_item_master Where Short_Description ='" & clsCommon.myCstr(Item_Short_Desc.Trim()) & "'  AND Item_Code <>'" & clsCommon.myCstr(Item_Code) & "'", trans))
                                        If ShortDesp > 0 Then
                                            Throw New Exception("Please check ! short description should not be duplicate")
                                        End If
                                    End If
                                End If
                                clsCommon.AddColumnsForChange(coll, "Short_Description", Item_Short_Desc)
                                Dim Is_Ambient As String = clsCommon.myCstr(grow.Cells("Is_Ambient").Value)
                                If clsCommon.CompairString(Is_Ambient, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                                    Throw New Exception("You cannot select Fresh item/Ambient at a time, Please select either fresh Item or Ambient on line")
                                End If
                                If clsCommon.CompairString(Is_Ambient, "0") = CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_Fresh, "0") = CompairStringResult.Equal Then
                                    Throw New Exception("Please select either fresh Item or Ambient on line")
                                End If
                                Dim IsTaxable As Integer = clsCommon.myCdbl(grow.Cells("IsTaxable").Value)
                                HSNCode = clsCommon.myCstr(grow.Cells("HSN Code").Value)
                                If IsTaxable > 0 Then
                                    If clsCommon.myLen(HSNCode) <= 0 Then
                                        Throw New Exception("Please Fill HSN Code.")
                                    End If

                                End If
                                If clsCommon.myLen(HSNCode) > 0 Then
                                    Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from tspl_HSN_master where code='" & HSNCode & "'", trans)
                                    If clsCommon.myLen(qry1) <= 0 Then
                                        Throw New Exception("Invalid HSN Code.")
                                    End If
                                End If

                                HSNCode = clsCommon.myCstr(grow.Cells("HSN Code").Value)

                                Is_Insurance = clsCommon.myCdbl(grow.Cells("Is_Insurance").Value)
                                If Is_Insurance = 1 Then
                                    If clsCommon.myLen(grow.Cells("InsuranceNo").Value.ToString()) = 0 Then
                                        Throw New Exception("Insurance No cannot be blank ")
                                    End If

                                    If clsCommon.myLen(grow.Cells("InsuranceFromDate").Value) = 0 Then
                                        Throw New Exception("Insurance From Date cannot be blank at line '" + LineNo + "'")
                                    End If

                                    If clsCommon.myLen(grow.Cells("InsuranceToDate").Value) = 0 Then
                                        Throw New Exception("Insurance To Date cannot be blank at line '" + LineNo + "'")
                                    End If

                                    If clsCommon.myCDate(grow.Cells("InsuranceFromDate").Value) > clsCommon.myCDate(grow.Cells("InsuranceToDate").Value) Then
                                        Throw New Exception("Insurance To date can not be before than from date.")
                                    End If

                                    InsuranceNo = clsCommon.myCstr(grow.Cells("InsuranceNo").Value.ToString())
                                    InsuranceFromDate = clsCommon.GetPrintDate(grow.Cells("InsuranceFromDate").Value, "dd/MMM/yyyy")
                                    InsuranceToDate = clsCommon.GetPrintDate(grow.Cells("InsuranceToDate").Value, "dd/MMM/yyyy")
                                Else
                                    InsuranceNo = Nothing
                                    InsuranceFromDate = Nothing
                                    InsuranceToDate = Nothing
                                End If
                                clsCommon.AddColumnsForChange(coll, "Is_Insurance", Is_Insurance)
                                clsCommon.AddColumnsForChange(coll, "InsuranceNo", InsuranceNo, True)
                                clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", InsuranceFromDate, True)
                                clsCommon.AddColumnsForChange(coll, "InsuranceToDate", InsuranceToDate, True)

                                '''''''''''''''''
                                Dim Marreccount As Integer = 0
                                MarseqNo = clsCommon.myCdbl(grow.Cells("Marketing Seq No").Value)
                                If clsCommon.myCdbl(MarseqNo) > 0 Then
                                    Marreccount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Marketing_Seq=" & MarseqNo & " and   item_code<>'" & Item_Code & "'", trans))
                                    If Marreccount > 0 Then
                                        Throw New Exception("Duplicate Marketing Sequence No at line '" + LineNo + "'")
                                    End If
                                End If
                                clsCommon.AddColumnsForChange(coll, "Marketing_Seq", MarseqNo)
                                ''''''''''''''''

                                AliesName = clsCommon.myCstr(grow.Cells("Alies Name").Value)
                                If clsCommon.myLen(AliesName) > 200 Then
                                    Throw New Exception("Length of Alies Name Should be less than 200.")
                                End If
                                clsCommon.AddColumnsForChange(coll, "Alies_Name", AliesName)
                                ' Item Group type (CSA Type)
                                ItemGroupType = clsCommon.myCstr(grow.Cells("Item Group Type").Value)
                                If clsCommon.myLen(ItemGroupType) <= 0 Then
                                    ItemGroupType = "None"
                                End If
                                clsCommon.AddColumnsForChange(coll, "CSA_TYPE", ItemGroupType)
                                '----------------------Purchase A/c Set---------------
                                Dim PurchaseAcSet As String = clsCommon.myCstr(grow.Cells("Purchase A/c Set").Value)
                                If PurchaseAcSet <> "" Then
                                    Purchase_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from dbo.TSPL_PURCHASE_ACCOUNTS WHERE Purchase_Class_Code ='" + PurchaseAcSet + "'", trans))
                                    If clsCommon.CompairString(Purchase_Class_Code, PurchaseAcSet) = CompairStringResult.Equal Then
                                        clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", Purchase_Class_Code)
                                    Else
                                        Throw New Exception("Purchase Acccount Set  does not exist.")
                                    End If
                                Else
                                    Throw New Exception("Purchase A/c Set can not be blank")
                                End If
                                '-----------------------------------------------------

                                '------------------------sale A/c Set------------------
                                Dim SaleAcSet As String = clsCommon.myCstr(grow.Cells("Sale A/c Set").Value)
                                If SaleAcSet <> "" Then
                                    Sale_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Code from TSPL_SALES_ACCOUNTS WHERE Sales_Class_Code ='" + SaleAcSet + "'", trans))
                                    If clsCommon.CompairString(Sale_Class_Code, SaleAcSet) = CompairStringResult.Equal Then
                                        clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", Sale_Class_Code)
                                    Else
                                        Throw New Exception("Sale Account Set  does not exist.")
                                    End If
                                Else
                                    Throw New Exception("Sale A/c Set can not be blank")
                                End If
                                '---------------------Unit_Code-------------------------------
                                Dim Uom As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                                If Uom <> "" Then
                                    Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master WHERE unit_code ='" + Uom + "'", trans))
                                    If clsCommon.CompairString(Unit_Code, Uom) = CompairStringResult.Equal Then
                                        clsCommon.AddColumnsForChange(coll, "Unit_Code", Unit_Code)
                                    Else
                                        Throw New Exception("UOM Code  does not exist.")
                                    End If
                                Else
                                    Throw New Exception("UOM can not be blank")
                                End If
                                '----------------Weight UOM-----------------------
                                Dim WeightUOM As String = clsCommon.myCstr(grow.Cells("Weight_UOM").Value)

                                Weight_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER WHERE Weight_Type='Y' AND Unit_Code ='" + WeightUOM + "'", trans))
                                If clsCommon.CompairString(Weight_UOM, WeightUOM) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Weight_UOM", Weight_UOM)
                                Else
                                    Throw New Exception("Weight_UOM  does not exist.")
                                End If


                                '----------------Structure Code-----------------------
                                Dim StructureCode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                                If StructureCode <> "" Then
                                    Structure_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Code from TSPL_STRUCTURE_MASTER WHERE Structure_Code='" + StructureCode + "'", trans))
                                    If clsCommon.myLen(StructureCode) <= 0 Then
                                        Throw New Exception("Structure code Not correct")
                                    End If
                                    clsCommon.AddColumnsForChange(coll, "Structure_Code", StructureCode)

                                Else
                                    Throw New Exception("Structure Code can not be blank ")
                                End If
                                '-----------------------------------------------------

                                '----------------Structure Desc-------------------------
                                Dim Structure_Desc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Structure_Descq from TSPL_STRUCTURE_MASTER Where Structure_Code='" + Structure_Code + "'", trans))
                                clsCommon.AddColumnsForChange(coll, "Structure_Desc", Structure_Desc)

                                '============================= Scrap Item ================================
                                Dim isScrapTypeItem As String = clsCommon.myCstr(grow.Cells("IS SCRAP ITEM").Value)
                                If clsCommon.myLen(isScrapTypeItem) <= 0 Then
                                    isScrapTypeItem = "0"
                                End If
                                Dim ScrapItemCode As String = clsCommon.myCstr(grow.Cells("SCrap Item Code").Value)
                                If clsCommon.CompairString(isScrapTypeItem, "1") = CompairStringResult.Equal Then
                                    If clsCommon.myLen(ScrapItemCode) > 0 Then
                                        If clsCommon.CompairString(ScrapItemCode, Item_Code) = CompairStringResult.Equal Then
                                            Throw New Exception("Item Code and Scrap Item Code can not be Same.")
                                        End If
                                        Dim isValidScrapItem As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count (*) from tspl_item_master  where tspl_item_master.Item_Code = '" + ScrapItemCode + "'", trans))
                                        If isValidScrapItem = False Then
                                            Throw New Exception("Invalid Item Code.")
                                        End If
                                        isValidScrapItem = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count ( Scrap_Item_Code) from tspl_item_master  where Scrap_Item_Code is not null and Scrap_Item_Code = '" + ScrapItemCode + "' Item_code not in ('" + Item_Code + "') ", trans))
                                        If isValidScrapItem = True Then
                                            Throw New Exception("Item Code (" + ScrapItemCode + ") Already used as Scrap Item with another Item code")
                                        End If
                                        isValidScrapItem = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" Select count(*) from tspl_item_master where Structure_Code = '" + Structure_Code + "' and Item_Code = '" + ScrapItemCode + "'", trans))
                                        If isValidScrapItem = False Then
                                            Throw New Exception(" Scrap Item Code and Main Item code should be Same Scrature Code.")
                                        End If
                                    Else
                                        Throw New Exception("SCrap Item Code Can not be blank.")
                                    End If
                                Else
                                    ScrapItemCode = ""
                                End If
                                clsCommon.AddColumnsForChange(coll, "IS_SCRAP_ITEM", isScrapTypeItem)
                                If clsCommon.CompairString(isScrapTypeItem, "1") = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "SCrap_Item_Code", ScrapItemCode)
                                End If
                                '==========================================================================


                                Dim Is_CrateType As String = "N"

                                '------------------GL Account------------------

                                GL_Account = clsCommon.myCstr(grow.Cells("GL Account").Value)
                                If clsCommon.myLen(GL_Account) > 0 Then
                                    Dim GLqry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(GLqry, trans)
                                    If check <= 0 Then
                                        Throw New Exception("Filled GL account (" & GL_Account & ") does not exist" + Environment.NewLine + ".First make its entry")
                                    End If
                                End If
                                clsCommon.AddColumnsForChange(coll, "GL_Account", GL_Account, True)
                                If clsCommon.myLen(grow.Cells("IsTaxable").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsTaxable").Value), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsTaxable").Value), "1") <> CompairStringResult.Equal Then
                                    Throw New Exception("Fill 0 or 1 in IsTaxable for item " & Item_Code & " at Line No " & LineNo & ".")
                                End If
                                '================Added by preeti gupta Against ticket no[BHA/27/05/19-000898]
                                Product_Type = clsCommon.myCstr(grow.Cells("Product Type").Value)
                                '' check for product type                           
                                Dim Initial_Product_Type As String = clsItemMaster.GetItemProductType(Item_Code, trans)
                                Dim Qry As String = ""
                                If clsCommon.CompairString(Product_Type, Initial_Product_Type) <> CompairStringResult.Equal Then
                                    If clsCommon.CompairString(Initial_Product_Type, "MI") = CompairStringResult.Equal Then
                                        Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & Item_Code & "'"
                                        Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
                                        If totalCount > 0 Then
                                            If Not settUpdateItemMasterWithoutTransactionValidation Then
                                                Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & " in Product Type Milk.")
                                            End If
                                        End If
                                    End If
                                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                        Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT where Item_Code='" & Item_Code & "'"
                                        Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
                                        If totalCount > 0 Then
                                            If Not settUpdateItemMasterWithoutTransactionValidation Then
                                                Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & ".")
                                            End If
                                        End If
                                    End If
                                End If


                                If clsCommon.myLen(Product_Type) > 0 AndAlso clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MB") <> CompairStringResult.Equal _
                                    AndAlso clsCommon.CompairString(Product_Type, "CH") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "CU") <> CompairStringResult.Equal _
                                    AndAlso clsCommon.CompairString(Product_Type, "CA") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "DG") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "BU") <> CompairStringResult.Equal _
                                    AndAlso clsCommon.CompairString(Product_Type, "BM") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "PS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MP") <> CompairStringResult.Equal Then
                                    Throw New Exception("Product Type should be MI(Milk),MB(Melted Butter),CH(Cheese),CU(Curd),CA(Cream),DG(Desi-Ghee),BU(Butter),BM(Butter Milk),PS(Paper Seal),MS(Manual Seal) and MP(Milk Product) for item " & Item_Code & " at Line No " & LineNo & ".")
                                End If

                                If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemCostMandatoryForStockingUnit, clsFixedParameterCode.AllowItemCostMandatoryForStockingUnit, trans)) = 1, True, False) = True Then
                                    If clsCommon.myCdbl(grow.Cells("Item Cost(Stocking Unit)").Value) <= 0 Then
                                        Throw New Exception("Item Cost Can not be blank Or Zero for Stocking Unit [Yes] in Item Code " + Item_Code + " at line no " + LineNo + ".")
                                    End If
                                End If

                                ''=========================================================================================
                                '===========================================================
                                clsCommon.AddColumnsForChange(coll, "Product_Type", Product_Type)
                                Dim Weight_Value As Double = clsCommon.myCdbl(grow.Cells("Weight_Value").Value)
                                clsCommon.AddColumnsForChange(coll, "Weight_Value", clsCommon.myCdbl(grow.Cells("Weight_Value").Value))
                                clsCommon.AddColumnsForChange(coll, "Is_FreshItem", clsCommon.myCdbl(grow.Cells("Is_FreshItem").Value))
                                clsCommon.AddColumnsForChange(coll, "Is_Ambient", clsCommon.myCdbl(grow.Cells("Is_Ambient").Value))
                                clsCommon.AddColumnsForChange(coll, "Active", clsCommon.myCdbl(grow.Cells("Active").Value))
                                clsCommon.AddColumnsForChange(coll, "Is_CrateType", clsCommon.myCdbl(grow.Cells("Is_CrateType").Value))
                                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Modify_Date", Datee)
                                clsCommon.AddColumnsForChange(coll, "IsTaxable", clsCommon.myCdbl(grow.Cells("IsTaxable").Value))
                                clsCommon.AddColumnsForChange(coll, "HSN_Code", clsCommon.myCstr(grow.Cells("HSN Code").Value))
                                Qry = "Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'"
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                    clsCommon.AddColumnsForChange(coll, "Created_Date", Datee)
                                    clsCommon.AddColumnsForChange(coll, "Cust_Account", Nothing, True)
                                    clsCommon.AddColumnsForChange(coll, "item_category", Nothing, True)
                                    clsCommon.AddColumnsForChange(coll, "Item_Category_Struct_Code", Nothing, True)
                                    clsCommon.AddColumnsForChange(coll, "PROD_ITEM_CATEGORY_CODE", Nothing, True)
                                    clsCommon.AddColumnsForChange(coll, "WARRANTY_CODE", Nothing, True)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                                Else
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "TSPL_ITEM_MASTER.Item_Code='" + Item_Code + "'", trans)
                                End If

                                Dim UOMCount As Integer = 0
                                Dim DefaultUOMCount As Integer = 0
                                Dim IsDefaultUOM As Integer = 0
                                Dim uom_GrossWt As Double = 0
                                '================Start=========================
                                Dim strStockingUnit As String = ""
                                '=================end========================
                                Dim strProductionFATSNF_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
                                Dim isUomKGExist As Boolean = False
                                Dim dblItemCost = clsCommon.myCdbl(grow.Cells("Item Cost(Stocking Unit)").Value)
                                For j As Integer = 1 To 5
                                    Dim colUOM As New Hashtable()
                                    Dim strUOM As String
                                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM" & clsCommon.myCstr(j) & "").Value)
                                    '=========================================================================
                                    If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                                        If clsCommon.CompairString(UnitCode, strProductionFATSNF_KG_Unit) = CompairStringResult.Equal Then
                                            isUomKGExist = True
                                        End If
                                    End If

                                    '====================start=====================
                                    If j = 1 Then
                                        strStockingUnit = clsCommon.myCstr(grow.Cells("UOM" & clsCommon.myCstr(j) & "").Value)
                                        Qry = "update TSPL_ITEM_MASTER set Unit_Code='" & strStockingUnit & "'  where Item_Code='" & Item_Code & "'"
                                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                    End If
                                    '====================End====================
                                    If clsCommon.myLen(UnitCode) > 0 Then
                                        UOMCount = +1
                                        strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                                        If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                                        Else
                                            Throw New Exception("The UOM " + UnitCode + " Does Not Exist")
                                        End If
                                        Dim FirstStockUnit As String = clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(j) & "").Value)
                                        For i As Integer = j + 1 To 5
                                            Dim SecondStockUnit As String = clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(i) & "").Value)
                                            If clsCommon.CompairString(FirstStockUnit, SecondStockUnit) = CompairStringResult.Equal AndAlso (clsCommon.CompairString(SecondStockUnit, "Y") = CompairStringResult.Equal And clsCommon.CompairString(FirstStockUnit, "Y") = CompairStringResult.Equal) Then
                                                Throw New Exception("More than one Stock Unit [Yes] not accepted at line no '" + LineNo + "' ")
                                            End If
                                        Next
                                        Dim StockCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select SUM(Item_Code) as Item_Code From (Select Count(Item_Code) as Item_Code from tspl_inventory_movement Where Item_Code='" + Item_Code + "' AND Stock_UOM ='" + strUOM + "' union all Select Count(Item_Code) as Item_Code from TSPL_INVENTORY_MOVEMENT_NEW Where Item_Code='" + Item_Code + "' AND Stock_UOM ='" + strUOM + "')mm", trans))
                                        If StockCount >= 1 AndAlso clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value) <> 1 Then
                                            If Not settUpdateItemMasterWithoutTransactionValidation Then
                                                Throw New Exception("Unit '" + UnitCode + "' is used as  Stocking unit,please set Conversion Factor 1 at line no '" + LineNo + "' ")
                                            End If
                                        End If
                                        Dim UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans))
                                        clsCommon.AddColumnsForChange(colUOM, "UOM_Description", UOM_Description)
                                        '====================================================
                                        If AllowItemConversionAutomation = 0 Then
                                            '====================================================
                                            If clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(j) & "").Value) = "Y" Then
                                                clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "Y")
                                                If clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value) > 1 Then
                                                    Throw New Exception("The Coversion Unit Should be [1] for Stocking Unit [Yes] at line no '" + LineNo + "'")
                                                End If
                                            Else
                                                clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "N")
                                            End If
                                            '===========================================================
                                        Else
                                            If j = 1 Then
                                                clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "Y")
                                            Else
                                                clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "N")
                                            End If
                                        End If
                                        Dim dblConvF As Double = 0
                                        '===========================================================
                                        If clsCommon.myLen(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value) > 0 Then
                                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "0") = CompairStringResult.Equal Then
                                                DefaultUOMCount = +1
                                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "1") = CompairStringResult.Equal Then
                                                    IsDefaultUOM = IsDefaultUOM + 1
                                                    clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 1)
                                                ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "0") = CompairStringResult.Equal Then
                                                    clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 0)
                                                End If
                                            Else
                                                Throw New Exception("Please enter Default UOM As '1' Or '0'")
                                            End If
                                        End If
                                        '======================================
                                        If AllowItemConversionAutomation = 1 Then
                                            Dim IsStockingUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", trans))
                                            Dim StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", trans))
                                            If j = 1 Then
                                                If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                                                    Qry = "update TSPL_ITEM_MASTER set Weight_UOM='" & strStockingUnit & "',Weight_Value=1  where Item_Code='" & Item_Code & "'"
                                                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                                End If
                                                clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", 1)
                                                clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost)
                                            Else
                                                '' Automatic Unit conversion start here

                                                Dim IntRowNo As Integer = j
                                                IntRowNo = IntRowNo - 1
                                                Dim IsUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                                Dim IsUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                                Dim IsUnitPackingType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Packet_Type from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                                If clsCommon.CompairString(IsUnitWeight, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IsUnitPackingType, "N") = CompairStringResult.Equal Then
                                                    If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                                                        If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                                                            dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUOM & "' and Contained_UOM='" & strStockingUnit & "'", trans))
                                                        Else
                                                            dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUOM & "' and Contained_UOM='" & strStockingUnit & "' and Structure_Code='" & Structure_Code & "'", trans))
                                                        End If
                                                        If dblConvF = 0 Then

                                                            Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUOM & " Contained Unit - " & strStockingUnit)
                                                        End If
                                                    Else
                                                        If clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal Then
                                                            If clsCommon.myLen(WeightUOM) = 0 Then
                                                                Throw New Exception("Please enter Weight UOM")

                                                            ElseIf clsCommon.myCdbl(Weight_Value) = 0 Then
                                                                Throw New Exception("Please enter Weight UOM Conversion")
                                                            End If
                                                            Dim strStockingUnitWeight = WeightUOM
                                                            If clsCommon.CompairString(strStockingUnitWeight, strUOM) = CompairStringResult.Equal Then
                                                                dblConvF = 1
                                                            Else
                                                                StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnitWeight & "'", trans))
                                                                If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                                                                    dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUOM) & "' and Contained_UOM='" & strStockingUnitWeight & "'", trans))
                                                                Else
                                                                    dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUOM) & "' and Contained_UOM='" & strStockingUnitWeight & "' and Structure_Code='" & clsCommon.myCstr(Structure_Code) & "'", trans))
                                                                End If
                                                                If dblConvF > 0 Then
                                                                    dblConvF = dblConvF / clsCommon.myCdbl(Weight_Value)
                                                                Else
                                                                    Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUOM & " Contained Unit - " & strStockingUnitWeight)
                                                                End If
                                                            End If

                                                        End If

                                                    End If

                                                Else
                                                    dblConvF = clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value)
                                                End If

                                                '' automatic unit conversion end here
                                                If dblConvF > 0 Then
                                                    clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", dblConvF)
                                                    clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost * dblConvF)
                                                Else
                                                    Throw New Exception("Please Insert Conversion Factor for unit " & strUOM)
                                                End If
                                            End If
                                        Else
                                            Dim ConversionFactor As Double = clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value)
                                            If ConversionFactor > 0 Then
                                                clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", ConversionFactor)
                                                clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost * ConversionFactor)
                                            Else
                                                Throw New Exception("Please Insert Convrsion Factor")
                                            End If
                                        End If
                                        '======================================



                                        Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + Item_Code + "' AND UOM_Code='" + UnitCode + "'", trans))
                                        If Count <= 0 Then
                                            clsCommon.AddColumnsForChange(colUOM, "Item_Code", Item_Code)
                                            clsCommon.AddColumnsForChange(colUOM, "UOM_Code", UnitCode)
                                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colUOM, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                        Else
                                            Dim whrClas As String = "Item_Code = '" + Item_Code + "' and uom_code='" + UnitCode + "'"
                                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colUOM, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                                        End If
                                    End If
                                Next
                                If UOMCount <= 0 Then
                                    Throw New Exception("Please insert at least one UOM for item code '" + Item_Code + "' ")
                                End If
                                If DefaultUOMCount <= 0 Then
                                    Throw New Exception("Please enter 1 to make UOM as default UOM for item code '" + Item_Code + "' ")
                                End If
                                If IsDefaultUOM <= 0 Then
                                    Throw New Exception("Please check ! One UOM should be Default UOM for item code '" + Item_Code + "'  ")
                                End If
                                If IsDefaultUOM > 1 Then
                                    Throw New Exception("Please check ! Default UOM should not be more than one for item code '" + Item_Code + "' ")
                                End If
                                If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                                    Dim isProductionFATSNF_KG_UnitPrvExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_ITEM_UOM_DETAIL where item_code = '" + Item_Code + "' and UOM_Code = '" + strProductionFATSNF_KG_Unit + "'", trans))
                                    If clsCommon.CompairString(clsCommon.myCstr(Product_Type), "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Product_Type), "MP") = CompairStringResult.Equal Then
                                        If isUomKGExist = False AndAlso isProductionFATSNF_KG_UnitPrvExist = False Then
                                            Throw New Exception("If Item is of MP or Milk type then " + strProductionFATSNF_KG_Unit + " must be defined as one of the conversion in Item Master.")
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        trans.Commit()
                        clsCommon.ProgressBarPercentHide()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed", Me.Text)
                    Catch ex As Exception
                        trans.Rollback()
                        clsCommon.ProgressBarPercentHide()
                        Throw New Exception(ex.Message)
                    End Try
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    RadMessageBox.Show("Error at Line No " + LineNo + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Item Master")
        Finally
            Me.Controls.Remove(gv1)
        End Try
    End Sub

    Private Sub RadButton52_Click(sender As Object, e As EventArgs) Handles RadButton52.Click
        Try
            Me.Text = "MCC Master"

            'qry = "select TSPL_MCC_MASTER.MCC_Code as [Mcc Code] ,TSPL_MCC_MASTER.MCC_Type as [Mcc Type] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name] , TSPL_MCC_MASTER.Chilling_Vendor as [Chilling Vendor Code] ,TSPL_MCC_MASTER.Add1 as [Address1] , TSPL_MCC_MASTER.City_code as [City Code] ,TSPL_MCC_MASTER.State_Code as [State Code] , TSPL_MCC_MASTER.Country_code as [Country Code] ,TSPL_MCC_MASTER.Pin_code as [Pin Code] ,TSPL_MCC_MASTER.Telphone as [Telphone] , TSPL_MCC_MASTER.Email as [Email] " + _
            '",(select top 1 UOM_Code from TSPL_Mcc_UOM_DETAIL where TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MCC_MASTER.MCC_Code) as [UOM Code]" + _
            '",(select top 1 UOM_Description from TSPL_Mcc_UOM_DETAIL where TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MCC_MASTER.MCC_Code) as [UOM Description],TSPL_MCC_MASTER.Payment_Cycle as [Payment Cycle Code], TSPL_LOCATION_MASTER.Loc_Segment_Code as [Loc Segment Code]" + _
            '" From TSPL_MCC_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_LOCATION_MASTER.Location_Code "
            qry = "select TSPL_MCC_MASTER.MCC_Code as [Mcc Code] ,TSPL_MCC_MASTER.MCC_Type as [Mcc Type] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name] , TSPL_MCC_MASTER.Chilling_Vendor as [Chilling Vendor Code] ,TSPL_MCC_MASTER.Add1 as [Address1] , TSPL_MCC_MASTER.City_code as [City Code] ,TSPL_MCC_MASTER.State_Code as [State Code] , TSPL_MCC_MASTER.Country_code as [Country Code] ,TSPL_MCC_MASTER.Pin_code as [Pin Code] ,TSPL_MCC_MASTER.Telphone as [Telphone] , TSPL_MCC_MASTER.Email as [Email] " + _
            ",TSPL_MCC_MASTER.Unit_Code as [UOM Code]" + _
            ",TSPL_UNIT_MASTER.Unit_Desc as [UOM Description],TSPL_MCC_MASTER.Payment_Cycle as [Payment Cycle Code], TSPL_LOCATION_MASTER.Loc_Segment_Code as [Loc Segment Code],TSPL_MCC_MASTER.plant_code as [Plant Code], TSPL_Location_Master_For_Plant.Location_Desc as [Plant Name], TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Code (For VLC Uploader)],TSPL_MCC_MASTER.Is_MCC as [MCC/BMCC] " + _
            " From TSPL_MCC_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_LOCATION_MASTER.Location_Code " & _
            " left outer Join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code = TSPL_MCC_MASTER.Unit_Code " & _
            " left outer join TSPL_Location_Master as TSPL_Location_Master_For_Plant on TSPL_Location_Master_For_Plant.Location_Code = TSPL_MCC_MASTER.plant_code "
            transportSql.ExporttoExcel(qry, "", "", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "MCC Master")
        End Try
    End Sub

    Private Sub RadButton51_Click(sender As Object, e As EventArgs) Handles RadButton51.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim i As Integer = 0
            connectSql.OpenConnection()
            Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
            If transportSql.importExcel(gv, "Mcc Code", "Mcc Type", "Mcc Name", "Chilling Vendor Code", "Address1", "City Code", "State Code", "Country Code", "Pin Code", "Telphone", "Email", "UOM Code", "UOM Description", "Payment Cycle Code", "Loc Segment Code", "Plant Code", "Plant Name", "Code (For VLC Uploader)", "MCC/BMCC") Then
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New clsMccMaster()
                        i = i + 1
                        Dim strData As String = clsCommon.myCstr(grow.Cells("Mcc Code").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Mcc Code Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 30 Then
                            Throw New Exception("Mcc Code Can Not Be Larger Then 30 Charachter")
                        End If
                        obj.MCC_Code = strData
                        strData = clsCommon.myCstr(grow.Cells("Mcc Type").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("MCC Type Can Not Be Left Blank")
                        End If
                        If clsCommon.CompairString(strData, "Co. Owned") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Co. Leased") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Federation") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "PPP") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "IKP") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "MPCS") = CompairStringResult.Equal Then
                            If clsCommon.myLen(grow.Cells("Chilling Vendor Code").Value) <= 0 AndAlso (clsCommon.CompairString(strData, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(strData, "Co. Leased") = CompairStringResult.Equal) Then
                                Throw New Exception("When MCC type is Chilling Basis/Co. Leased, It Must be Specified the chilling Vendor Code ")
                            End If
                        Else
                            Throw New Exception("MCC Type Can be Either of Co. Owned/Co. Leased/Chilling Basis/Federation/PPP/IKP/MPCS ")
                        End If
                        obj.MCC_Type = strData
                        obj.Chilling_Vendor = clsCommon.myCstr(grow.Cells("Chilling Vendor Code").Value)
                        obj.Is_MCC = IIf(clsCommon.myCdbl(grow.Cells("MCC/BMCC").Value) = 0, 0, 1)
                        strData = clsCommon.myCstr(grow.Cells("Mcc Name").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Mcc Name Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 50 Then
                            Throw New Exception("Mcc Name Can Not Be Larger Then 50 Charachter")
                        End If
                        obj.MCC_NAME = strData
                        strData = clsCommon.myCstr(grow.Cells("Address1").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Address1 Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 50 Then
                            Throw New Exception("Address1 Can Not Be Larger Then 50 Charachter")
                        End If
                        obj.Add1 = strData

                        obj.Payment_Cycle = clsCommon.myCstr(grow.Cells("Payment Cycle Code").Value)
                        If clsCommon.myLen(obj.Payment_Cycle) <= 0 Then
                            Throw New Exception("Please enter payment cycle code")
                        End If
                        obj.Payment_Cycle = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PC_CODE from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" + obj.Payment_Cycle + "'"))
                        If clsCommon.myLen(obj.Payment_Cycle) <= 0 Then
                            Throw New Exception("Invalid payment cycle code")
                        End If

                        strData = clsCommon.myCstr(grow.Cells("Pin Code").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Pin Code Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 20 Then
                            Throw New Exception("Pin code Can Not Be Larger Then 20 Charachter")
                        End If
                        obj.Pin_code = strData
                        obj.agrmnt_date = DateTime.Now
                        obj.expired_date = DateTime.Now
                        strData = clsCommon.myCstr(grow.Cells("City Code").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("City Code Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 20 Then
                            Throw New Exception("City Code Can Not Be Larger Then 20 Charachter")
                        End If
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" & strData & "'") = 0 Then
                            Throw New Exception("City Code Could Not Found In Master")
                        End If
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" & strData & "' and state_code='" & clsCommon.myCstr(grow.Cells("State Code").Value) & "'") = 0 Then
                            Throw New Exception("Invaid City Code : " & strData & " Against State Code: " & clsCommon.myCstr(grow.Cells("State Code").Value) & Environment.NewLine & " This City Is not Mapped With Specified State First Map it in City Master ")
                        End If
                        obj.City_code = strData

                        strData = clsCommon.myCstr(grow.Cells("State Code").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("State Code Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 20 Then
                            Throw New Exception("State Code Can Not Be Larger Then 20 Charachter")
                        End If
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "'") = 0 Then
                            Throw New Exception("State Code Could Not Found In Master")
                        End If
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & strData & "' and country_code='" & clsCommon.myCstr(grow.Cells("Country Code").Value) & "'") = 0 Then
                            Throw New Exception("Invaid State Code : " & strData & " Against Country Code: " & clsCommon.myCstr(grow.Cells("Country Code").Value) & Environment.NewLine & " This State Is not Mapped With Specified Country First Map it in State Master ")
                        End If
                        obj.State_Code = strData
                        strData = clsCommon.myCstr(grow.Cells("Country Code").Value)
                        If clsCommon.myLen(strData) <= 0 Then
                            Throw New Exception("Country Code Can Not Be Left Blank")
                        End If
                        If clsCommon.myLen(strData) > 20 Then
                            Throw New Exception("Country Code Can Not Be Larger Then 20 Charachter")
                        End If
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_country_master where country_code='" & strData & "'") = 0 Then
                            Throw New Exception("Country Code Could Not Found In Master")
                        End If
                        obj.Country_code = strData

                        If clsCommon.myLen(grow.Cells("Email").Value) > 0 Then
                            Dim check As Match = Regex.Match(grow.Cells("Email").Value, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                            If check.Success Then
                                obj.Email = clsCommon.myCstr(grow.Cells("Email").Value)
                            Else
                                Throw New Exception("Email Is In Invalid Format. It Shoud Be as UserName@Domain Format ")
                            End If
                        End If
                        obj.Loc_Segment_Code = clsCommon.myCstr(grow.Cells("Loc Segment Code").Value)
                        '============================================================================
                        ' Ticket No : TEC/16/07/19-000945 By Prabhakar
                        If clsCommon.myLen(clsCommon.myCstr(obj.Loc_Segment_Code)) <= 0 Then
                            Throw New Exception("Loc Segment Code is Manadatory, and it must  not be Blank")
                        Else
                            Dim isValidSegmentcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"))
                            If isValidSegmentcode = False Then
                                Throw New Exception("Invalid Loc Segment Code.")
                            End If
                        End If
                        '============================================================================
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Plant Code").Value)) > 0 Then
                            Dim chkValidPlantCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_LOCATION_MASTER where Location_Code = '" + clsCommon.myCstr(grow.Cells("Plant Code").Value) + "' and  TSPL_LOCATION_MASTER .Type = 'PLANT'"))
                            If chkValidPlantCode = False Then
                                Throw New Exception("Invalid Plant Code For MCC Code " + obj.MCC_Code + ".")
                            Else
                                obj.Plant_Code = clsCommon.myCstr(grow.Cells("Plant Code").Value)
                            End If
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Code (For VLC Uploader)").Value)) > 0 Then
                            Dim isValidUploaderCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count (*) from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + clsCommon.myCstr(grow.Cells("Code (For VLC Uploader)").Value) + "' and MCC_Code <> '" + obj.MCC_Code + "'"))
                            If isValidUploaderCode = True Then
                                Throw New Exception("Duplicate MCC VLC Uploader Code Not Allow.")
                            Else
                                obj.MCC_Code_VLC_Uploader = clsCommon.myCstr(grow.Cells("Code (For VLC Uploader)").Value)
                            End If
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("UOM Code").Value)) > 0 Then
                            Dim chkValidUom As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_UNIT_MASTER where Unit_Code = '" + clsCommon.myCstr(grow.Cells("UOM Code").Value) + "'"))
                            If chkValidUom = False Then
                            Else
                                obj.Unit_Code = clsCommon.myCstr(grow.Cells("UOM Code").Value)
                            End If
                        End If

                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_master where mcc_code='" & obj.MCC_Code & "'") = 0 Then
                            Dim objgn As clsGenSetDetail
                            obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                            For j As Integer = 0 To obj.NoOfDG
                                objgn = New clsGenSetDetail()
                                objgn.Prog_Code = clsUserMgtCode.frmMCCMaster
                                objgn.Trans_Code = obj.MCC_Code
                                objgn.Line_No = (j + 1)
                                objgn.Gen_Set_Desc = "N/A"
                                obj.arrGenSetDetail.Add(objgn)
                            Next
                            Dim objcomp As clsCompressorDetail
                            obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                            For j As Integer = 0 To obj.NoOfCompressor
                                objcomp = New clsCompressorDetail
                                objcomp.Prog_Code = clsUserMgtCode.frmMCCMaster
                                objcomp.Trans_Code = obj.MCC_Code
                                objcomp.Line_No = (j + 1)
                                objcomp.Compressor_Desc = "N/A"
                                obj.arrCompressorDetail.Add(objcomp)
                            Next
                            Dim objSilo As clsSiloDetail
                            obj.arrSiloDetail = New List(Of clsSiloDetail)
                            For j As Integer = 0 To obj.No_Of_SILO
                                objSilo = New clsSiloDetail()
                                objSilo.Prog_Code = clsUserMgtCode.frmMCCMaster
                                objSilo.Trans_Code = obj.MCC_Code
                                objSilo.Line_No = (j + 1)
                                objSilo.Silo_Desc = "N/A"
                                objSilo.Silo_Area = 0
                                objSilo.Silo_Unit = ""
                                obj.arrSiloDetail.Add(objSilo)
                            Next
                            Dim objmilkpump As clsMilkPumpDetail
                            obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                            For j As Integer = 0 To obj.No_Of_MilkPump
                                objmilkpump = New clsMilkPumpDetail()
                                objmilkpump.Prog_Code = clsUserMgtCode.frmMCCMaster
                                objmilkpump.Trans_Code = obj.MCC_Code
                                objmilkpump.Line_No = (j + 1)
                                objmilkpump.Pump_Desc = "N/A"
                                objmilkpump.Pump_Area = 0
                                objmilkpump.Pump_Unit = ""
                                obj.arrMilkPumpDetail.Add(objmilkpump)
                            Next

                            Dim objChiller As clsChillerDetail
                            obj.arrChillerDetail = New List(Of clsChillerDetail)
                            For j As Integer = 0 To obj.No_Of_Chiller
                                objChiller = New clsChillerDetail()
                                ' objChiller = New clsMilkPumpDetail()
                                objChiller.Prog_Code = clsUserMgtCode.frmMCCMaster
                                objChiller.Trans_Code = obj.MCC_Code
                                objChiller.Chiller_Desc = "N/A"
                                objChiller.Chiller_Brand = ""
                                objChiller.Chiller_Capacity = 0
                                obj.arrChillerDetail.Add(objChiller)
                            Next
                            obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                            obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
                            obj.isNewEntry = True
                            obj.Modified_By = objCommonVar.CurrentUserCode
                            obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                            obj.Comp_Code = objCommonVar.CurrentCompanyCode

                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                            clsMccMaster.SaveData(obj)
                        Else
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Gen_Set_Detail where prog_code='" & clsUserMgtCode.frmMCCMaster & "' and trans_code='" & obj.MCC_Code & "'")
                            Dim objgn As clsGenSetDetail
                            obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objgn = New clsGenSetDetail()
                                    objgn.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objgn.Trans_Code = dt.Rows(j)("Trans_Code")
                                    objgn.Line_No = dt.Rows(j)("Line_No")
                                    objgn.Gen_Set_Desc = dt.Rows(j)("Gen_Set_Desc")
                                    obj.arrGenSetDetail.Add(objgn)
                                Next
                            End If
                            If dt.Rows.Count > obj.NoOfDG Then
                                obj.NoOfDG = dt.Rows.Count
                            ElseIf dt.Rows.Count < obj.NoOfDG Then
                                For j As Integer = dt.Rows.Count + 1 To obj.NoOfDG
                                    objgn = New clsGenSetDetail()
                                    objgn.Prog_Code = clsUserMgtCode.frmMCCMaster
                                    objgn.Trans_Code = obj.MCC_Code
                                    objgn.Line_No = j
                                    objgn.Gen_Set_Desc = "N/A"
                                    obj.arrGenSetDetail.Add(objgn)
                                Next
                            End If

                            dt = clsDBFuncationality.GetDataTable("select * from TSPL_Compressor_Detail where prog_code='" & clsUserMgtCode.frmMCCMaster & "' and trans_code='" & obj.MCC_Code & "'")
                            Dim objcomp As clsCompressorDetail
                            obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objcomp = New clsCompressorDetail()
                                    objcomp.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objcomp.Trans_Code = dt.Rows(j)("Trans_Code")
                                    objcomp.Line_No = dt.Rows(j)("Line_No")
                                    objcomp.Compressor_Desc = dt.Rows(j)("Compressor_Desc")
                                    obj.arrCompressorDetail.Add(objcomp)
                                Next
                            End If
                            If dt.Rows.Count > obj.NoOfCompressor Then
                                obj.NoOfCompressor = dt.Rows.Count
                            ElseIf dt.Rows.Count < obj.NoOfCompressor Then
                                For j As Integer = dt.Rows.Count + 1 To obj.NoOfCompressor
                                    objcomp = New clsCompressorDetail
                                    objcomp.Prog_Code = clsUserMgtCode.frmMCCMaster
                                    objcomp.Trans_Code = obj.MCC_Code
                                    objcomp.Line_No = j
                                    objcomp.Compressor_Desc = "N/A"
                                    obj.arrCompressorDetail.Add(objcomp)
                                Next
                            End If
                            '===============================Silo Details====================================================
                            dt = clsDBFuncationality.GetDataTable("select * from TSPL_Silo_Detail where prog_code='" & clsUserMgtCode.frmMCCMaster & "' and trans_code='" & obj.MCC_Code & "'")
                            Dim objSilo As clsSiloDetail
                            obj.arrSiloDetail = New List(Of clsSiloDetail)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objSilo = New clsSiloDetail()
                                    objSilo.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objSilo.Trans_Code = dt.Rows(j)("Trans_Code")
                                    objSilo.Line_No = dt.Rows(j)("Line_No")
                                    objSilo.Silo_Desc = dt.Rows(j)("Silo_Desc")
                                    objSilo.Silo_Area = dt.Rows(j)("Silo_Area")
                                    objSilo.Silo_Unit = dt.Rows(j)("Silo_Unit")
                                    obj.arrSiloDetail.Add(objSilo)
                                Next
                            End If
                            If dt.Rows.Count > obj.No_Of_SILO Then
                                obj.No_Of_SILO = dt.Rows.Count
                            ElseIf dt.Rows.Count < obj.No_Of_SILO Then
                                For j As Integer = dt.Rows.Count + 1 To obj.No_Of_SILO
                                    objSilo = New clsSiloDetail
                                    objSilo.Prog_Code = clsUserMgtCode.frmMCCMaster
                                    objSilo.Trans_Code = obj.MCC_Code
                                    objSilo.Line_No = j
                                    objSilo.Silo_Desc = "N/A"
                                    objSilo.Silo_Area = "0"
                                    objSilo.Silo_Unit = "M"
                                    obj.arrSiloDetail.Add(objSilo)
                                Next
                            End If
                            dt = clsDBFuncationality.GetDataTable("select * from TSPL_Milk_Pump_Detail where prog_code='" & clsUserMgtCode.frmMCCMaster & "' and trans_code='" & obj.MCC_Code & "'")
                            Dim objmilkpump As clsMilkPumpDetail
                            obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objmilkpump = New clsMilkPumpDetail()
                                    objmilkpump.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objmilkpump.Trans_Code = dt.Rows(j)("Trans_Code")
                                    objmilkpump.Line_No = dt.Rows(j)("Line_No")
                                    objmilkpump.Pump_Desc = dt.Rows(j)("Milk_Pump_Desc")
                                    objmilkpump.Pump_Area = dt.Rows(j)("Milk_Pump_Area")
                                    objmilkpump.Pump_Unit = dt.Rows(j)("Milk_Pump_Unit")
                                    obj.arrMilkPumpDetail.Add(objmilkpump)
                                Next
                            End If
                            If dt.Rows.Count > obj.No_Of_MilkPump Then
                                obj.No_Of_MilkPump = dt.Rows.Count
                            ElseIf dt.Rows.Count < obj.No_Of_MilkPump Then
                                For j As Integer = dt.Rows.Count + 1 To obj.No_Of_MilkPump
                                    objmilkpump = New clsMilkPumpDetail
                                    objmilkpump.Prog_Code = clsUserMgtCode.frmMCCMaster
                                    objmilkpump.Trans_Code = obj.MCC_Code
                                    objmilkpump.Line_No = j
                                    objmilkpump.Pump_Desc = "N/A"
                                    objmilkpump.Pump_Area = "0"
                                    objmilkpump.Pump_Unit = "M"
                                    obj.arrMilkPumpDetail.Add(objmilkpump)
                                Next
                            End If
                            '=========================================================================================================
                            '===============================Chiller Details====================================================
                            dt = clsDBFuncationality.GetDataTable("select * from TSPL_Chiller_Detail where prog_code='" & clsUserMgtCode.frmMCCMaster & "' and trans_code='" & obj.MCC_Code & "'")
                            Dim objChiller As clsChillerDetail
                            obj.arrChillerDetail = New List(Of clsChillerDetail)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objChiller = New clsChillerDetail()
                                    objChiller.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objChiller.Trans_Code = dt.Rows(j)("Trans_Code")
                                    objChiller.Line_No = dt.Rows(j)("Line_No")
                                    objChiller.Chiller_Desc = dt.Rows(j)("Chiller_Desc")
                                    objChiller.Chiller_Brand = dt.Rows(j)("Chiller_Brand")
                                    objChiller.Chiller_Capacity = dt.Rows(j)("Chiller_Capacity")
                                    obj.arrChillerDetail.Add(objChiller)
                                Next
                            End If
                            If dt.Rows.Count > obj.No_Of_Chiller Then
                                obj.No_Of_Chiller = dt.Rows.Count
                            ElseIf dt.Rows.Count < obj.No_Of_Chiller Then
                                For j As Integer = dt.Rows.Count + 1 To obj.No_Of_Chiller
                                    objChiller = New clsChillerDetail
                                    objChiller.Prog_Code = clsUserMgtCode.frmMCCMaster
                                    objChiller.Trans_Code = obj.MCC_Code
                                    objChiller.Line_No = j
                                    objChiller.Chiller_Desc = "N/A"
                                    objChiller.Chiller_Brand = ""
                                    objChiller.Chiller_Capacity = "0"
                                    obj.arrChillerDetail.Add(objChiller)
                                Next
                            End If
                            '=========================================================================================================
                            '===========================UOM Details==================================================================
                            Dim objMccUOM As clsMccUOMDetails
                            If clsCommon.myLen(grow.Cells("UOM Code").Value) > 0 Then
                                Dim dtp As DataTable = clsDBFuncationality.GetDataTable("select Unit_Code,Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + clsCommon.myCstr(grow.Cells("UOM Code").Value) + "'")
                                If dtp Is Nothing OrElse dtp.Rows.Count <= 0 Then
                                    Throw New Exception("Wrong UOM Code")
                                End If
                                obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                                objMccUOM = New clsMccUOMDetails()
                                objMccUOM.Mcc_Code = obj.MCC_Code
                                objMccUOM.UOM_Code = clsCommon.myCstr(dtp.Rows(0)("Unit_Code"))
                                objMccUOM.UOM_Description = clsCommon.myCstr(dtp.Rows(0)("Unit_Desc"))
                                objMccUOM.Stocking_Unit = "Y"
                                objMccUOM.Conversion_Factor = 1
                                obj.ArrUomDetails.Add(objMccUOM)
                                '============================================================================================================
                                dt = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_UOM_Detail where MCC_code='" & obj.MCC_Code & "' and Uom_Code <> '" + clsCommon.myCstr(grow.Cells("UOM Code").Value) + "' ")
                                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        objMccUOM = New clsMccUOMDetails()
                                        objMccUOM.Mcc_Code = dt.Rows(j)("Mcc_Code")
                                        objMccUOM.UOM_Code = dt.Rows(j)("UOM_Code")
                                        objMccUOM.UOM_Description = dt.Rows(j)("UOM_Description")
                                        objMccUOM.Stocking_Unit = "N"
                                        objMccUOM.Conversion_Factor = dt.Rows(j)("Conversion_Factor")
                                        obj.ArrUomDetails.Add(objMccUOM)
                                    Next
                                End If
                                '=============================================================================================================
                            Else
                                dt = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_UOM_Detail where MCC_code='" & obj.MCC_Code & "' ")

                                obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                                If dt IsNot Nothing And dt.Rows.Count > 0 Then

                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        objMccUOM = New clsMccUOMDetails()
                                        objMccUOM.Mcc_Code = dt.Rows(j)("Mcc_Code")
                                        objMccUOM.UOM_Code = dt.Rows(j)("UOM_Code")
                                        objMccUOM.UOM_Description = dt.Rows(j)("UOM_Description")
                                        objMccUOM.Stocking_Unit = dt.Rows(j)("Stocking_Unit")
                                        objMccUOM.Conversion_Factor = dt.Rows(j)("Conversion_Factor")
                                        obj.ArrUomDetails.Add(objMccUOM)
                                    Next
                                End If
                            End If


                            '===============================================================================================
                            '===========================Cheque Details==================================================================
                            dt = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_Cheque_Detail where Prog_Code='" & obj.MCC_Code & "' ")
                            Dim objMccCheque As clsMCCChequeDetails
                            obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
                            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    objMccCheque = New clsMCCChequeDetails()
                                    objMccCheque.Prog_Code = dt.Rows(j)("Prog_Code")
                                    objMccCheque.Check_No = dt.Rows(j)("Cheque_No")
                                    objMccCheque.Check_date = dt.Rows(j)("Cheque_Date")
                                    obj.arrChequeDetail.Add(objMccCheque)
                                Next
                            End If
                            '===============================================================================================
                            obj.Modified_By = objCommonVar.CurrentUserCode
                            obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                            obj.isNewEntry = False
                            clsMccMaster.SaveData(obj)
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!, " + Environment.NewLine + " Only Data Regarding [DG set Detail],[Compressor Detail],[Silo Detail],[Milk Pump Detail],[Chiller Detail],[UOM Detail] is Not Updated Please Import Respective Sheets ", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error At Line No : " + clsCommon.myCstr(i) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "MCC Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton48_Click(sender As Object, e As EventArgs) Handles RadButton48.Click
        Try
            Me.Text = "Village Master"
            Dim qry As String = "select count(*) from tspl_village_master"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            Dim strAddInfo As String = ""
            Dim strBlankAddInfo As String = ""
            strAddInfo = ",Surveyor_Name,case when Survey_Date is null then '' else REPLACE( convert(varchar, Survey_Date,106),' ','/') end as Survey_Date,Total_Population,Tehsil,Total_Voting,Pradhan_Name,Pradhan_Contact_No,Distance_From_Center,Irrigation_Source,Village_Area,Distance_From_MCC,Cow_In_Milk,Buffalo_In_Milk,Cow_Dry,Buffalo_Dry ,Milk_Production_Per_Day_Cow,Milk_Production_Per_Day_Buffalo,Marketable_Surplus_Per_Day_Cow,Marketable_Surplus_Per_Day_Buffalo,Expected_Milk_Per_Day_Cow,Expected_Milk_Per_Day_Buffalo "
            strBlankAddInfo = ",'' as Surveyor_Name,'' as Survey_Date,0 as Total_Population,'' as Tehsil,0 as Total_Voting,'' as Pradhan_Name,'' as Pradhan_Contact_No,0 as Distance_From_Center,'' as Irrigation_Source,'' as Village_Area,0 as Distance_From_MCC,0 as Cow_In_Milk,0 as Buffalo_In_Milk,0 as Cow_Dry,0 as Buffalo_Dry ,0 as Milk_Production_Per_Day_Cow,0 as Milk_Production_Per_Day_Buffalo,0 as Marketable_Surplus_Per_Day_Cow,0 as Marketable_Surplus_Per_Day_Buffalo,0 as Expected_Milk_Per_Day_Cow,0 as Expected_Milk_Per_Day_Buffalo "
            If check > 0 Then
                qry = "select TSPL_VILLAGE_MASTER.village_code,TSPL_VILLAGE_MASTER.village_name,TSPL_VILLAGE_MASTER.city_code,tspl_city_master.city_name,TSPL_VILLAGE_MASTER.state_code,tspl_state_master.state_name,TSPL_VILLAGE_MASTER.country_code,tspl_country_master.country_name,TSPL_VILLAGE_MASTER.pin_no" + strAddInfo + " from TSPL_VILLAGE_MASTER left outer join tspl_city_master on tspl_city_master.city_code=TSPL_VILLAGE_MASTER.city_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_VILLAGE_MASTER.state_code left outer join tspl_country_master on tspl_country_master.country_code=TSPL_VILLAGE_MASTER.country_code"
            Else
                qry = "select '' as village_code,'' as village_name, '' as city_code,'' as city_name,'' as state_code,'' as state_name,'' as country_code,'' as country_name,'' as pin_no" + strBlankAddInfo
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton47_Click(sender As Object, e As EventArgs) Handles RadButton47.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = clsCommon.GETSERVERDATE()
            Dim result As Boolean = False

            result = transportSql.importExcel(gv, "village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no", "Surveyor_Name", "Survey_Date", "Total_Population", "Tehsil", "Total_Voting", "Pradhan_Name", "Pradhan_Contact_No", "Distance_From_Center", "Irrigation_Source", "Village_Area", "Distance_From_MCC", "Cow_In_Milk", "Buffalo_In_Milk", "Cow_Dry", "Buffalo_Dry", "Milk_Production_Per_Day_Cow", "Milk_Production_Per_Day_Buffalo", "Marketable_Surplus_Per_Day_Cow", "Marketable_Surplus_Per_Day_Buffalo", "Expected_Milk_Per_Day_Cow", "Expected_Milk_Per_Day_Buffalo")
           
            If result Then
                Dim counter As Integer = 0
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        If clsCommon.myLen(grow.Cells("village_code").Value) > 0 Then
                            Dim obj As New clsfrmVillageMaster
                            obj.villcode = clsCommon.myCstr(grow.Cells("village_code").Value)

                            obj.villname = clsCommon.myCstr(grow.Cells("village_name").Value)
                            If clsCommon.myLen(obj.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(obj.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.citycode = clsCommon.myCstr(grow.Cells("city_code").Value)
                            If clsCommon.myLen(obj.citycode) > 0 Then
                                qry = "select city_code from tspl_city_master where city_code='" + obj.citycode + "'"
                                obj.citycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
                                If clsCommon.myLen(obj.citycode) <= 0 Then
                                    Throw New Exception("First Create City Master(" + obj.citycode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If

                            obj.statecode = clsCommon.myCstr(grow.Cells("state_code").Value)
                            If clsCommon.myLen(obj.statecode) > 0 Then
                                qry = "select state_code from tspl_state_master where state_code='" + obj.statecode + "'"
                                obj.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))

                                If clsCommon.myLen(obj.statecode) <= 0 Then
                                    Throw New Exception("First Create State Master(" + obj.statecode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                Else
                                    qry = "select count(*) from tspl_city_master where city_code='" + obj.citycode + "' and state_code='" + obj.statecode + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                    If check <= 0 Then
                                        Throw New Exception("First Mapped State (" + obj.statecode + ") With City (" + obj.citycode + ") On City Master See At Line No. " + clsCommon.myCstr(counter) + "")
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(obj.citycode) > 0 AndAlso clsCommon.myLen(obj.statecode) <= 0 Then
                                Throw New Exception("Please Fill State Along With City,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            obj.countrycode = clsCommon.myCstr(grow.Cells("country_code").Value)


                            If clsCommon.myLen(obj.countrycode) > 0 Then
                                qry = "select country_code from tspl_country_master where country_code='" + obj.countrycode + "'"
                                obj.countrycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))

                                If clsCommon.myLen(obj.countrycode) <= 0 Then
                                    Throw New Exception("Please First Create Country Master(" + obj.countrycode + " Does Not Exist In Master)")
                                Else
                                    qry = "select count(*) from tspl_state_master where country_code='" + obj.countrycode + "' and state_code='" + obj.statecode + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                    If check <= 0 Then
                                        Throw New Exception("First Mapped Country (" + obj.countrycode + ") With State (" + obj.statecode + ") In State Master See At Line No. " + clsCommon.myCstr(counter) + "")
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(obj.countrycode) <= 0 AndAlso clsCommon.myLen(obj.statecode) > 0 Then
                                Throw New Exception("Please Fill Country Along With State,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.pinno = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("pin_no").Value))


                            obj.Surveyor_Name = clsCommon.myCstr(grow.Cells("Surveyor_Name").Value)
                            If clsCommon.myLen(obj.Surveyor_Name) > 50 Then
                                Throw New Exception("Length Of Surveyor Name Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(grow.Cells("Survey_Date").Value) > 0 Then
                                obj.Survey_Date = clsCommon.myCDate(grow.Cells("Survey_Date").Value)
                            Else
                                obj.Survey_Date = Nothing
                            End If
                            obj.Total_Population = clsCommon.myCdbl(grow.Cells("Total_Population").Value)
                            obj.Tehsil = clsCommon.myCstr(grow.Cells("Tehsil").Value)
                            If clsCommon.myLen(obj.Tehsil) > 50 Then
                                Throw New Exception("Length Of Tehsil Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Total_Voting = clsCommon.myCdbl(grow.Cells("Total_Voting").Value)
                            obj.Pradhan_Name = clsCommon.myCstr(grow.Cells("Pradhan_Name").Value)
                            If clsCommon.myLen(obj.Pradhan_Name) > 50 Then
                                Throw New Exception("Length Of Pradhan_Name Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Pradhan_Contact_No = clsCommon.myCstr(grow.Cells("Pradhan_Contact_No").Value)
                            If clsCommon.myLen(obj.Pradhan_Contact_No) > 50 Then
                                Throw New Exception("Length Of Pradhan Contact No Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Distance_From_Center = clsCommon.myCdbl(grow.Cells("Distance_From_Center").Value)
                            obj.Irrigation_Source = clsCommon.myCstr(grow.Cells("Irrigation_Source").Value)
                            If clsCommon.myLen(obj.Irrigation_Source) > 50 Then
                                Throw New Exception("Length Of Irrigation Source Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Village_Area = clsCommon.myCstr(grow.Cells("Village_Area").Value)
                            If clsCommon.myLen(obj.Village_Area) > 50 Then
                                Throw New Exception("Length Of Village_Area Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Distance_From_MCC = clsCommon.myCdbl(grow.Cells("Distance_From_MCC").Value)
                            obj.Cow_In_Milk = clsCommon.myCdbl(grow.Cells("Cow_In_Milk").Value)
                            obj.Buffalo_In_Milk = clsCommon.myCdbl(grow.Cells("Buffalo_In_Milk").Value)
                            obj.Cow_Dry = clsCommon.myCdbl(grow.Cells("Cow_Dry").Value)
                            obj.Buffalo_Dry = clsCommon.myCdbl(grow.Cells("Buffalo_Dry").Value)
                            obj.Milk_Production_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Milk_Production_Per_Day_Cow").Value)
                            obj.Milk_Production_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Milk_Production_Per_Day_Buffalo").Value)
                            obj.Marketable_Surplus_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Marketable_Surplus_Per_Day_Cow").Value)
                            obj.Marketable_Surplus_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Marketable_Surplus_Per_Day_Buffalo").Value)
                            obj.Expected_Milk_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Expected_Milk_Per_Day_Cow").Value)
                            obj.Expected_Milk_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Expected_Milk_Per_Day_Buffalo").Value)

                            Dim isNewEntry As Boolean = True
                            If clsCommon.myLen(obj.villcode) > 0 Then
                                qry = "select count(*) from TSPL_VILLAGE_MASTER where village_code='" + obj.villcode + "'"
                                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                If check1 > 0 Then
                                    isNewEntry = False
                                End If
                            End If
                            clsfrmVillageMaster.SaveData(obj, isNewEntry, tran)
                        End If
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                    Next
                    tran.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    tran.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at line No " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton46_Click(sender As Object, e As EventArgs) Handles RadButton46.Click

        Try
            Me.Text = "VSP Master"
            Try

                qry = "select Vendor_Code as [VSP No],Vendor_Name as[VSP Name],Add1 as [Address1],Pin_Code as [Pin Code],Vendor_Group_Code as [Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],City_Code_Desc as [City Code Description],State_Code as [State],Country_Code as [Country],Phone1 as [Phone Num1],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],PAN as [PAN],Joint_Name,Bank_Code as [Bank Code],case when Joint_Name <>'' then Joint_bank_Code else null end As [Name of Bank],case when Joint_Name <>'' then  Joint_Account_No else null end As [Bank Account No],Service_Charge_Type,commision_pers,payment_commision_pers,incentive_days,vsp_payment, VSP_Payee_Name, Branch_Name,Account_No,Bank_Name,IFSC_Code ,Agreement,Start_Date As [Agreement Date], End_Date As [Expiry Date],MP_code as [MP Code],MP_Name as [MP Name],IS_DRIP_SAVER AS [Drip Saver],Joint_Branch_Name as [Joint Branch Name],Joint_IFSC_Code as [Joint IFSC Code],CHEQUE_IN_FAVOUR_OF AS [Cheque in Favour of],EMP_Type as [EMPType (FP/SWP/FAFP/FASWP)],EMP_Fixed_Amount,Actual_charges_Slab as [EMP Slab 1],Actual_charges [EMP Slab 1 Value],Actual_charges_Slab2 as [EMP Slab 2],Actual_charges2 [EMP Slab 2 Value],Actual_charges_Slab3 as [EMP Slab 3],Actual_charges3 [EMP Slab 3 Value],Actual_charges_Slab4 as [EMP Slab 4],Actual_charges4 [EMP Slab 4 Value],Actual_charges_Slab5 as [EMP Slab 5],Actual_charges5 [EMP Slab 5 Value],Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100  from TSPL_VENDOR_MASTER "
                Dim whrCls As String = " and form_type='VSP'"

                transportSql.ExporttoExcel(qry, whrCls, Me)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "VSP Master")
        End Try
    End Sub

    Private Sub RadButton45_Click(sender As Object, e As EventArgs) Handles RadButton45.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim Count As String = """"
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
            If transportSql.importExcel(gv, "VSP No", "VSP Name", "Address1", "Pin Code", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State", "Country", "Phone Num1", "Vendor Account", "Vendor Account Description", "PAN", "Joint_Name", "Bank Code", "Name of Bank", "Bank Account No", "Service_Charge_Type", "commision_pers", "payment_commision_pers", "incentive_days", "vsp_payment", "VSP_Payee_Name", "Branch_Name", "Account_No", "Bank_Name", "IFSC_Code", "Agreement", "Agreement Date", "Expiry Date", "MP Code", "MP Name", "Drip Saver", "Joint Branch Name", "Joint IFSC Code", "Cheque in Favour of", "EMPType (FP/SWP/FAFP/FASWP)", "EMP_Fixed_Amount", "EMP Slab 1", "EMP Slab 1 Value", "EMP Slab 2", "EMP Slab 2 Value", "EMP Slab 3", "EMP Slab 3 Value", "EMP Slab 4", "EMP Slab 4 Value", "EMP Slab 5", "EMP Slab 5 Value", "Aadhar_No", "Care_Of", "SecChequeNoLac1", "SecChequeNoRs100") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    Dim FixVSPEMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixVSPEMP, clsFixedParameterCode.FixVSPEMP, Nothing))

                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If

                    Dim counter As Integer = 1
                    Dim IsBlacklisted As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("VSP No").Value)
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of VSP No.,")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("VSP No. can not be blank,")
                        End If

                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of VSP Name can not be greater than 100.,")
                        End If

                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("VSP Name can not be blank")
                        End If
                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim Pin_Code As String = clsCommon.myCstr(grow.Cells("Pin Code").Value)
                        Dim closing_date As String = System.DateTime.Now.Date
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the length of Group Code")
                        End If

                        Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the length of Group Code Description")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                        Dim citycodedesc As String = clsCommon.myCstr(grow.Cells("City Code Description").Value)
                        Dim state As String = ""
                        Dim country As String = ""

                        Dim statecode As String = clsCommon.myCstr(grow.Cells("State").Value)
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("Country").Value)
                        Dim check As Integer = 0

                        If clsCommon.myLen(countrycode) <= 0 Then
                            Throw New Exception("Please Fill Country")
                        End If
                        If clsCommon.myLen(statecode) <= 0 Then
                            Throw New Exception("Please Fill State")
                        End If
                        If clsCommon.myLen(citycode) <= 0 Then
                            Throw New Exception("Please Fill City")
                        End If

                        If clsCommon.myLen(countrycode) > 0 Then
                            qry = "select COUNTRY_CODE,COUNTRY_NAME from tspl_country_master   where country_code='" + countrycode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Country Code Does Not Exist,Please Make Country Master")
                            End If
                            countrycode = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
                            country = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
                        End If
                        If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                            qry = "select STATE_CODE,STATE_NAME from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                            End If
                            statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                            state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                        End If

                        If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                            qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("City Code Does Not Exist,Please Make Its Master First")
                            End If
                        End If
                        Dim MP_Code As String = clsCommon.myCstr(grow.Cells("MP Code").Value)
                        Dim MP_name As String = String.Empty
                        If clsCommon.myLen(MP_Code) > 0 Then
                            qry = "select count(*) from TSPL_MP_MASTER where MP_Code='" + MP_Code + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("MP Code does not exist,please make its master first")
                            End If
                            MP_Code = "'" & MP_Code & "'"
                            MP_name = clsDBFuncationality.getSingleValue("select MP_Name from TSPL_MP_MASTER where MP_Code=" & MP_Code & "", trans)
                        End If
                        ''
                        Dim srvctype As String = clsCommon.myCstr(grow.Cells("Service_Charge_Type").Value)
                        Dim commsn As Decimal = clsCommon.myCdbl(grow.Cells("commision_pers").Value)
                        Dim paymnt_commsn As Decimal = clsCommon.myCdbl(grow.Cells("payment_commision_pers").Value)

                        Dim noofdays As Decimal = clsCommon.myCdbl(grow.Cells("incentive_days").Value)
                        Dim vsppaymnt As String = clsCommon.myCstr(grow.Cells("vsp_payment").Value).Replace("'", "`")
                        Dim payeename As String = clsCommon.myCstr(grow.Cells("vsp_payee_name").Value).Replace("'", "`")
                        Dim jointname As String = clsCommon.myCstr(grow.Cells("Joint_Name").Value).Replace("'", "`")

                        If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                            Throw New Exception("Fill Self/Different in vsp payment at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal AndAlso clsCommon.myLen(jointname) <= 0 Then
                            Throw New Exception("Please Fill Joint Name At Line No. " + clsCommon.myCstr(counter) + "")
                        ElseIf clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal Then
                            payeename = ""
                            jointname = ""
                        End If
                        Dim NameOfBank As String = ""
                        Dim AccountNo As String = ""
                        If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal Then
                            payeename = strvendorname & IIf(clsCommon.myLen(jointname) > 0, " and " & jointname, "")
                            NameOfBank = clsCommon.myCstr(grow.Cells("Name of Bank").Value).Replace("'", "`")
                            If clsCommon.myLen(NameOfBank) > 0 Then
                                Dim qrybank As String = "select count(*) from TSPL_Vendor_Bank_MASTER where Bank_Code='" + NameOfBank + "'"
                                check = clsDBFuncationality.getSingleValue(qrybank, trans)
                                If check <= 0 Then
                                    Throw New Exception("Name of Bank does not exist,please make its master first")
                                End If
                            Else
                                Throw New Exception("Name of bank can not be left blank")
                            End If
                            AccountNo = clsCommon.myCstr(grow.Cells("Bank Account No").Value).Replace("'", "`")
                            If clsCommon.myLen(AccountNo) > 30 Then
                                Throw New Exception("Length of bank account no. should not be more than 30")
                            End If
                        ElseIf clsCommon.CompairString(vsppaymnt, "Self") = CompairStringResult.Equal Then
                            payeename = strvendorname
                        End If
                        ''
                        Dim Joint_Branch_Name As String = String.Empty
                        Dim Joint_IFSC_Code As String = String.Empty
                        Joint_Branch_Name = clsCommon.myCstr(grow.Cells("Joint Branch Name").Value)
                        Joint_IFSC_Code = clsCommon.myCstr(grow.Cells("Joint IFSC Code").Value)

                        If clsCommon.myLen(srvctype) <= 0 Then
                            Throw New Exception("Please Fill Service Type(%(Percentage),Rate/Kg,Rate/Ltr) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(srvctype, "%(Percentage)") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Kg") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Ltr") <> CompairStringResult.Equal Then
                            Throw New Exception("Filled Service Type Should Be Any One From %(Percentage),Rate/Kg,Rate/Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim contct_person_name As String = ""
                        Dim contct_perfson_phone As String = ""
                        Dim contct_person_fax As String = ""
                        Dim contct_person_website As String = ""
                        Dim contct_person_email As String = ""

                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Vendor Account can not be blank")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the length of Vendor Account Set Code")
                        End If

                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                        Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                        If String.IsNullOrEmpty(strbank) Then
                            Throw New Exception("Bank Code can not be blank")
                        End If
                        Dim i5 As String

                        Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                        i5 = connectSql.RunScalar(trans, qry7)
                        If i5 = 0 Then
                            Throw New Exception("Bank code does not exist : " + strbank + "")
                        End If
                        If strbank.Length > 30 Then
                            Throw New Exception("Check the length of bank code")
                        End If
                        Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                        Dim strTagAsFranchise As String = ""
                        If strTagAsFranchise.Length > 1 Then
                            Throw New Exception("Check the length of Tagged as Franchise")
                        ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                            strTagAsFranchise = "N"
                        End If

                        Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                        If clsCommon.myLen(strAccNo) > 50 Then
                            Throw New Exception("Account No. should be max 50 character.")
                        End If

                        Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank_Name").Value)
                        If clsCommon.myLen(strBName) > 50 Then
                            Throw New Exception("Bank Name should be max 50 character.")
                        End If

                        Dim strIFSCCode As String = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                        If clsCommon.myLen(strIFSCCode) > 100 Then
                            Throw New Exception("IFSC Code should be max 100 character")
                        End If
                        'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                        '    Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        'End If
                        Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                        If clsCommon.myLen(strBrachName) > 100 Then
                            Throw New Exception("Branch Name should be max 100 character")
                        End If
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where  TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                            Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        ''-------------------------

                        If clsCommon.myLen(Joint_IFSC_Code) > 100 Then
                            Throw New Exception("Joint IFSC Code should be max 50 character")
                        End If
                        If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal Then
                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + Joint_IFSC_Code + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & NameOfBank & "' ", trans) <= 0 Then
                                Throw New Exception("Joint IFSC Code Does Not Exist :  " + Joint_IFSC_Code + " for bank " + NameOfBank + "  .Please make entry in vendor bank master.")
                            End If
                            If clsCommon.myLen(Joint_Branch_Name) > 100 Then
                                Throw New Exception("Joint Branch Name should be max 50 character")
                            End If
                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + Joint_IFSC_Code + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & NameOfBank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & Joint_Branch_Name & "'", trans) <= 0 Then
                                Throw New Exception("Joint Branch Name Does Not Exist : " + Joint_Branch_Name + " for bank " + NameOfBank + "  .Please make entry in vendor bank master.")
                            End If

                        End If

                        Dim strAgreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                        Dim strAgreementDate As String = clsCommon.myCstr(grow.Cells("Agreement Date").Value)
                        Dim strExpiryDate As String = clsCommon.myCstr(grow.Cells("Expiry Date").Value)
                        If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strAgreementDate) <= 0 Then
                                Throw New Exception("Agreement date can not be left blank")
                            End If
                            If clsCommon.myLen(strExpiryDate) <= 0 Then
                                Throw New Exception("Expiry date can not be left blank")
                            End If
                            Try
                                Convert.ToDateTime(strAgreementDate)
                            Catch exx As Exception
                                Throw New Exception("Agreement date should be in proper date format")
                            End Try
                            Try
                                Convert.ToDateTime(strExpiryDate)
                            Catch exx As Exception
                                Throw New Exception("Expiry date should be in proper date format")
                            End Try
                        End If

                        Dim is_drip_saver As String = clsCommon.myCstr(grow.Cells("Drip saver").Value)
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(grow.Cells("PAN").Value) + "' and Form_Type='VSP' and vendor_Code<>'" & clsCommon.myCstr(grow.Cells("VSP No").Value) & "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check > 0 Then
                                Throw New Exception("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                                Return
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                                If Not checkPan.IsMatch(clsCommon.myCstr(grow.Cells("PAN").Value)) Then
                                    Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                                End If
                            End If
                        End If

                        Dim strEMPType As String = clsCommon.myCstr(grow.Cells("EMPType (FP/SWP/FAFP/FASWP)").Value)
                        If clsCommon.myLen(strEMPType) <= 0 Then
                            Throw New Exception("Please provide emp type.See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If Not (clsCommon.CompairString(strEMPType, "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FASWP") = CompairStringResult.Equal) Then
                            Throw New Exception("Emp type should be FP/SWP/FAFP/FASWP .See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim dblFixedAmount As Double = clsCommon.myCdbl(grow.Cells("EMP_Fixed_Amount").Value)
                        If clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FASWP") = CompairStringResult.Equal Then
                            If dblFixedAmount <= 0 Then
                                Throw New Exception("Please provide EMP Fixed Amount .See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        Dim EMPSlab1, EMPSlab1Value, EMPSlab2, EMPSlab2Value, EMPSlab3, EMPSlab3Value, EMPSlab4, EMPSlab4Value, EMPSlab5, EMPSlab5Value As New Double
                        If clsCommon.CompairString(strEMPType, "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal Then
                            EMPSlab1Value = clsCommon.myCdbl(grow.Cells("EMP Slab 1 Value").Value)
                        Else
                            EMPSlab1 = clsCommon.myCdbl(grow.Cells("EMP Slab 1").Value)
                            EMPSlab1Value = clsCommon.myCdbl(grow.Cells("EMP Slab 1 Value").Value)
                            EMPSlab2 = clsCommon.myCdbl(grow.Cells("EMP Slab 2").Value)
                            EMPSlab2Value = clsCommon.myCdbl(grow.Cells("EMP Slab 2 Value").Value)
                            EMPSlab3 = clsCommon.myCdbl(grow.Cells("EMP Slab 3").Value)
                            EMPSlab3Value = clsCommon.myCdbl(grow.Cells("EMP Slab 3 Value").Value)
                            EMPSlab4 = clsCommon.myCdbl(grow.Cells("EMP Slab 4").Value)
                            EMPSlab4Value = clsCommon.myCdbl(grow.Cells("EMP Slab 4 Value").Value)
                            EMPSlab5 = clsCommon.myCdbl(grow.Cells("EMP Slab 5").Value)
                            EMPSlab5Value = clsCommon.myCdbl(grow.Cells("EMP Slab 5 Value").Value)
                        End If
                        If FixVSPEMP > 0 Then
                            strEMPType = "FP"
                            dblFixedAmount = 0
                            srvctype = "%(Percentage)"
                            EMPSlab1 = 0
                            EMPSlab1Value = FixVSPEMP
                            EMPSlab2 = 0
                            EMPSlab2Value = 0
                            EMPSlab3 = 0
                            EMPSlab3Value = 0
                            EMPSlab4 = 0
                            EMPSlab4Value = 0
                            EMPSlab5 = 0
                            EMPSlab5Value = 0
                        End If

                        Dim strAadharNo As String = clsCommon.myCstr(grow.Cells("Aadhar_No").Value)
                        Dim strCareOf As String = clsCommon.myCstr(grow.Cells("Care_Of").Value)
                        Dim strSecChequeNoLac1 As String = clsCommon.myCstr(grow.Cells("SecChequeNoLac1").Value)
                        Dim strSecChequeNoRs100 As String = clsCommon.myCstr(grow.Cells("SecChequeNoRs100").Value)
                        If clsCommon.myLen(strAadharNo) > 0 Then
                            If clsCommon.myLen(strAadharNo) <> 12 Then
                                Throw New Exception("Aadhar No should be 12 character")
                            End If
                        End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                        clsCommon.AddColumnsForChange(coll, "add1", add1)
                        clsCommon.AddColumnsForChange(coll, "Pin_Code", Pin_Code)
                        clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                        clsCommon.AddColumnsForChange(coll, "City_Code", citycode)
                        clsCommon.AddColumnsForChange(coll, "City_Code_Desc", citycodedesc)
                        clsCommon.AddColumnsForChange(coll, "State", state)
                        clsCommon.AddColumnsForChange(coll, "Country", country)
                        clsCommon.AddColumnsForChange(coll, "Phone1", phonenum1)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account", vendoracct)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account_Desc", vendoracctdesc)
                        clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(grow.Cells("PAN").Value))
                        clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                        clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                        clsCommon.AddColumnsForChange(coll, "country_code", countrycode)
                        clsCommon.AddColumnsForChange(coll, "commision_pers", commsn)
                        'clsCommon.AddColumnsForChange(coll, "incentive", closing_date)
                        clsCommon.AddColumnsForChange(coll, "vsp_payment", vsppaymnt)
                        clsCommon.AddColumnsForChange(coll, "vsp_payee_name", payeename)
                        clsCommon.AddColumnsForChange(coll, "Joint_Name", jointname)
                        clsCommon.AddColumnsForChange(coll, "Service_Charge_Type", srvctype)
                        clsCommon.AddColumnsForChange(coll, "payment_commision_pers", paymnt_commsn)
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                        clsCommon.AddColumnsForChange(coll, "Account_No", strAccNo)
                        clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                        clsCommon.AddColumnsForChange(coll, "Agreement ", strAgreement.ToUpper())
                        If clsCommon.CompairString(strAgreement, "YES") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(strAgreementDate, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(strExpiryDate, "dd/MMM/yyyy"))
                        Else
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Joint_bank_Code", NameOfBank)
                        clsCommon.AddColumnsForChange(coll, "Joint_Account_No", AccountNo)
                        clsCommon.AddColumnsForChange(coll, "Nature", "E")
                        clsCommon.AddColumnsForChange(coll, "MP_code", MP_Code, True)
                        clsCommon.AddColumnsForChange(coll, "MP_Name", MP_name)
                        clsCommon.AddColumnsForChange(coll, "is_Drip_saver", is_drip_saver)
                        clsCommon.AddColumnsForChange(coll, "Joint_IFSC_Code", Joint_IFSC_Code)
                        clsCommon.AddColumnsForChange(coll, "Joint_Branch_Name", Joint_Branch_Name)
                        clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                        clsCommon.AddColumnsForChange(coll, "Cheque_in_Favour_of", Cheque_In_favour_of)
                        clsCommon.AddColumnsForChange(coll, "Status", "N")
                        clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                        clsCommon.AddColumnsForChange(coll, "EMP_Type", strEMPType)
                        clsCommon.AddColumnsForChange(coll, "EMP_Fixed_Amount", dblFixedAmount)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges_Slab", EMPSlab1)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges", EMPSlab1Value)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges_Slab2 ", EMPSlab2)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges2", EMPSlab2Value)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges_Slab3", EMPSlab3)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges3", EMPSlab3Value)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges_Slab4 ", EMPSlab4)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges4", EMPSlab4Value)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges_Slab5 ", EMPSlab5)
                        clsCommon.AddColumnsForChange(coll, "Actual_charges5", EMPSlab5Value)
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                        clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                        clsCommon.AddColumnsForChange(coll, "Aadhar_No", strAadharNo)
                        clsCommon.AddColumnsForChange(coll, "Care_Of", strCareOf)
                        clsCommon.AddColumnsForChange(coll, "SecChequeNoLac1", strSecChequeNoLac1)
                        clsCommon.AddColumnsForChange(coll, "SecChequeNoRs100", strSecChequeNoRs100)
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", strvendorNo)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + strvendorNo + "' and form_type='VSP'", trans)
                        End If
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Line: " + clsCommon.myCstr(LineNo) + " - " + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "VSP Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton44_Click(sender As Object, e As EventArgs) Handles RadButton44.Click
        Try
            Me.Text = "VLC Master"
            qry = "select TSPL_VLC_MASTER_HEAD.vlc_code as [Code],TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.village_code as [Village Code],tspl_village_master.village_name as [Village Name],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],tspl_mcc_route_master.route_name as [Route Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join tspl_village_master on TSPL_VLC_MASTER_HEAD.village_code=tspl_village_master.village_code left outer join tspl_mcc_route_master on TSPL_VLC_MASTER_HEAD.route_code=tspl_mcc_route_master.route_code"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "VLC Master")
        End Try
    End Sub

    Private Sub RadButton43_Click(sender As Object, e As EventArgs) Handles RadButton43.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Code", "VLC Name", "Village Code", "Village Name", "Route Code", "Route Name", "VSP Code", "VSP Name", "MCC Code", "MCC Name", "VLC Uploader Code") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim counter As Integer = 0
                Dim dtt As DataTable = TryCast(gv.DataSource, DataTable)
                dtt.Columns.Add("ErrorDesc", "".GetType())
                Try
                    Dim code As String = "'"
                    Dim name As String = ""
                    Dim vspcode As String = ""
                    Dim vspname As String = ""
                    Dim mcccode As String = ""
                    Dim mccname As String = ""
                    Dim villcode As String = ""
                    Dim villname As String = ""
                    Dim routecode As String = ""
                    Dim routename As String = ""
                    Dim VlcUploaderCode As String = ""
                    Dim ErrCount As Integer = 0
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        code = clsCommon.myCstr(grow.Cells("Code").Value)
                        name = clsCommon.myCstr(grow.Cells("VLC Name").Value).Replace("'", "`")
                        If clsCommon.myLen(name) <= 0 Then
                            'Throw New Exception("Fill VLC Name At Line No. " + clsCommon.myCstr(counter) + "")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Fill VLC Name At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myLen(name) > 150 Then
                            'Throw New Exception("Length Of VLC Name Should Not Exceed 150 Characters")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Length Of VLC Name Should Not Exceed 150 Characters Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        vspcode = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                        vspname = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                        If clsCommon.myLen(vspcode) <= 0 AndAlso clsCommon.myLen(vspname) <= 0 Then
                            'Throw New Exception("Fill VSP Code/VSP Name At Line No. " + clsCommon.myCstr(counter) + "")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Fill VSP Code/VSP Name At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        Dim coll As New Hashtable()
                        If clsCommon.myLen(vspcode) <= 0 AndAlso clsCommon.myLen(vspname) > 0 Then
                            coll = New Hashtable()
                            vspcode = (clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, ""))
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", vspcode)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", vspname)
                            clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "Nature", "E")
                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        End If


                        If clsCommon.myLen(vspcode) > 12 Then
                            'Throw New Exception("Length Of VSP Code Should Not Exceed 12 Characters")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Length Of VSP Code Should Not Exceed 12 Characters At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        Dim qry As String = ""
                        Dim check As Integer = 0
                        If clsCommon.myLen(vspcode) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where form_type='VSP' and vendor_code='" + vspcode + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                'Throw New Exception("VSP Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of VSP Master")
                                dtt.Rows(grow.Index)("ErrorDesc") = "VSP Code Does Not Exist At Line No. " & clsCommon.myCstr(counter + 1) & ",First Do Entry Of VSP Master"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                        End If
                        mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                        VlcUploaderCode = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)
                        If clsCommon.myLen(mcccode) <= 0 AndAlso clsCommon.myLen(mccname) <= 0 Then
                            'Throw New Exception("Fill MCC Code/MCC Name At Line No. " + clsCommon.myCstr(counter) + "")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Fill MCC Code/MCC Name At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myLen(mcccode) > 30 Then
                            'Throw New Exception("Length Of MCC Code Should Not Exceed 30 Characters")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Length Of MCC Code Should Not Exceed 30 Characters At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myLen(mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + mcccode + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                'Throw New Exception("MCC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of MCC Master")
                                dtt.Rows(grow.Index)("ErrorDesc") = "MCC Code Does Not Exist At Line No. " & clsCommon.myCstr(counter + 1) & ",First Do Entry Of MCC Master"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                        End If

                        If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                            'Throw New Exception("Fill VLC Uploader Code At Line No. " + clsCommon.myCstr(counter))
                            dtt.Rows(grow.Index)("ErrorDesc") = "Fill VLC Uploader Code At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & VlcUploaderCode & "' and vlc_code<>'" & code & "'  and mcc='" & mcccode & "'", trans)) >= 1 Then
                            'Throw New Exception("Duplicate VLC Uploader Code At Line No. " + clsCommon.myCstr(counter))
                            dtt.Rows(grow.Index)("ErrorDesc") = "Duplicate VLC Uploader Code At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        '-------------------------------------------------

                        villcode = clsCommon.myCstr(grow.Cells("village code").Value).Replace("'", "`")
                        villname = clsCommon.myCstr(grow.Cells("village name").Value)

                        If clsCommon.myLen(villcode) <= 0 AndAlso clsCommon.myLen(villname) > 0 Then
                            coll = New Hashtable()
                            Dim objVLM As New clsfrmVillageMaster
                            objVLM.villname = villname
                            clsfrmVillageMaster.SaveData(objVLM, True, trans)
                            villcode = objVLM.villcode
                        End If

                        If clsCommon.myLen(villcode) <= 0 AndAlso clsCommon.myLen(villname) <= 0 Then
                            'Throw New Exception("Fill Village Code/Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Fill Village Code/Village Name At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myLen(villcode) > 30 Then
                            'Throw New Exception("Length Of Village Code Should Not Exceed 30 Characters")
                            dtt.Rows(grow.Index)("ErrorDesc") = "Length Of Village Code Should Not Exceed 30 Characters At Line No. " & clsCommon.myCstr(counter + 1) & " "
                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                        End If
                        If clsCommon.myLen(villcode) > 0 Then
                            qry = "select count(*) from tspl_village_master where village_code='" + villcode + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                'Throw New Exception("Village Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Village Master")
                                dtt.Rows(grow.Index)("ErrorDesc") = "Village Code Does Not Exist At Line No. " & clsCommon.myCstr(counter + 1) & ",First Do Entry Of Village Master"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                        End If

                        Dim isSaved As Boolean = True
                        qry = "select count(*) from TSPL_VLC_MASTER_HEAD where vlc_code='" + code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If clsCommon.myLen(code) <= 0 Then
                            code = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                        End If
                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "vlc_code", code)
                        clsCommon.AddColumnsForChange(coll, "vlc_name", name)

                        clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                        clsCommon.AddColumnsForChange(coll, "village_code", villcode)

                        clsCommon.AddColumnsForChange(coll, "MCC", mcccode)



                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Price_Code", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                                VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(code)
                                clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                            Else
                                clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                            End If
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                        Else

                            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + code + "'", trans)
                        End If
                        clsfrmVLCMaster.SaveVLCPriceCode(code, vspcode, mcccode, trans)
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & clsCommon.myCstr(counter) & "/" & clsCommon.myCstr(gv.Rows.Count))
ExitLOOP:
                    Next

                    dtt.DefaultView.RowFilter = "ErrorDesc<>''"
                    dtt = dtt.DefaultView.ToTable

                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully" + IIf(dtt.Rows.Count > 0, " Except " + clsCommon.myCstr(dtt.Rows.Count) + " Records.", ""), Me.Text)

                    If dtt.Rows.Count > 0 Then
                        common.clsCommon.MyMessageBoxShow("Error in " & dtt.Rows.Count & " Records.", Me.Text, MessageBoxButtons.OK)
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "UnImportedListVLCMaster"
                        ff.Text = "Record Could not Loaded"
                        ff.dt = dtt
                        ff.ShowDialog()
                        Exit Sub
                    End If

                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at row No " + clsCommon.myCstr(counter + 1) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "VLC Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton80_Click(sender As Object, e As EventArgs) Handles RadButton80.Click
        Try
            Me.Text = "Primary Transporter Master"
            qry = " select Vendor_Code as [Transporter No],Vendor_Name as [Transporter Name],Add1 as [Address1],Vendor_Group_Code as [Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],State_Code as [State],Country_Code as [Country],Phone1 as [Phone Num1],Email as [Email Id],Terms_Code as [Terms Code] ,Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Bank_Code as [Bank Code],PAN as [PAN], VSP_Payee_Name as [Payee Name],Account_No As [Account No],Industry_Type as [Industry Type],(case when industry_type='Prop.' then industry_person else '' end) as [Prop Name],Agreement,Start_Date As [Agreement Date], End_Date As [Expiry Date],IFSC_Code,Branch_Name,CHEQUE_IN_FAVOUR_OF AS [Cheque in Favour of],Incentive,Apply_Mult_Incentive as [Multiple Incentive(0/1)],Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100 from TSPL_VENDOR_MASTER "
            Dim whrCls As String = " and form_type='PTM' "
            transportSql.ExporttoExcel(qry, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Primary Transporter Master")
        End Try
    End Sub

    Sub OLD()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim Count As String = ""
            If transportSql.importExcel(gv, "Transporter No", "Transporter Name", "Address1", "Address2", "Address3", "Group Code", "Vendor Group Description", "City", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Collectorate", "PAN", "State_Code", "Country_Code", "Zila", "Tehsil", "Payee Name", "Account_No", "Industry_Type", "Prop Name", "Partner Name", "Director Name", "Agreement", "Start_Date", "End_Date", "Security_Cheque", "No_of_Installment", "Amount_of_Installment", "form_type", "IFSC_Code", "Branch_Name", "Cheque in Favour of", "Incentive", "Multiple Incentive(0/1)") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    Dim counter As Integer = 1

                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Count = clsCommon.myCstr(grow.Index + 2)
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Transporter No").Value)
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of Transporter No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("Transporter No. can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of Transporter Name can not be greater than 100.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("Transporter Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim check As Integer = 0
                        'Dim MultipleIncentive As Integer = clsCommon.myCdbl(grow.Cells("Multiple Incentive(0/1)").Value)
                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("VSP Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim incentv As String = clsCommon.myCstr(grow.Cells("incentive").Value).Replace("'", "`")
                        If clsCommon.myLen(incentv) > 0 Then
                            Dim qryincentive As String = "select count(*) from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE='" + incentv + "'"
                            check = clsDBFuncationality.getSingleValue(qryincentive, trans)
                            If check <= 0 Then
                                Throw New Exception("Incentive does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        'If MultipleIncentive = 1 Then
                        '    incentv = ""
                        'End If

                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                        Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                        Dim closing_date As String
                        closing_date = System.DateTime.Now.Date
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the length of Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the length of Group Code Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City").Value)
                        Dim citycodedesc As String = citycode
                        Dim state As String = clsCommon.myCstr(grow.Cells("State").Value)
                        Dim country As String = clsCommon.myCstr(grow.Cells("Country").Value)

                        Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                        If clsCommon.myLen(countrycode) <= 0 Then
                            Throw New Exception("Please Fill Country,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(statecode) <= 0 Then
                            Throw New Exception("Please Fill State,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(citycode) <= 0 Then
                            Throw New Exception("Please Fill City,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.myLen(countrycode) > 0 Then
                            qry = "select count(*) from tspl_country_master where country_code='" + countrycode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("Country Code Does Not Exist,Please Make Country Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                            qry = "select count(*) from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                        Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                        Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                        Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                        Dim contct_person_name As String = ""
                        Dim contct_perfson_phone As String = ""
                        Dim contct_person_fax As String = ""
                        Dim contct_person_website As String = ""
                        Dim contct_person_email As String = ""
                        Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                        If String.IsNullOrEmpty(strtermcode) Then
                            Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i1 As Integer
                        Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                        i1 = connectSql.RunScalar(trans, qry1)
                        If i1 = 0 Then
                            Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If strtermcode.Length > 12 Then
                            Throw New Exception("Check the length of Terms Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strtermdes As String = clsCommon.myCstr(grow.Cells("Terms Description").Value)
                        If strtermdes.Length > 50 Then
                            Throw New Exception("Check the length of Term Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Vendor Account can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the length of Vendor Account Set Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                        Dim paymenttype As String = clsCommon.myCstr(grow.Cells("Payment Code").Value)
                        Dim i4 As String
                        If Not String.IsNullOrEmpty(paymenttype) Then
                            Dim qry5 As String = "select COUNT(*) from TSPL_PAYMENT_CODE  where Payment_Code ='" + paymenttype + "'"
                            i4 = connectSql.RunScalar(trans, qry5)
                            If i4 = 0 Then
                                Throw New Exception("Payment Code Does Not Exist : " + paymenttype + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If paymenttype.Length > 12 Then
                                Throw New Exception("Check the length of Payment Code")
                            End If
                        End If
                        Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                        Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                        If String.IsNullOrEmpty(strbank) Then
                            Throw New Exception("Bank Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i5 As String

                        Dim qry7 As String = "select COUNT(*) from TSPL_Vendor_BANK_MASTER  where Bank_Code ='" + strbank + "'"
                        i5 = connectSql.RunScalar(trans, qry7)
                        If i5 = 0 Then
                            Throw New Exception("Bank Code Does Not Exist : " + strbank + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim strvendortype As String = clsCommon.myCstr(grow.Cells("Vendor Type").Value)
                        Dim strvendortypedes As String = grow.Cells("Vendor Type Description").Value.ToString()
                        If strvendortype.Length > 12 Then
                            Throw New Exception("Check the length of Vendor Type,See At Line No. " + clsCommon.myCstr(counter) + " ")
                        End If
                        If strvendortypedes.Length > 50 Then
                            Throw New Exception("Check the length of Vendor Type Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim strTax As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                        If String.IsNullOrEmpty(strTax) Then
                            Throw New Exception(" Tax Group can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i6 As String
                        Dim qry9 As String = "select COUNT(*) from  TSPL_TAX_GROUP_MASTER   where tax_group_Code ='" + strTax + "'"
                        i6 = connectSql.RunScalar(trans, qry9)
                        If i6 = 0 Then
                            Throw New Exception("Tax Group Code Does Not Exist : " + strTax + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If strTax.Length > 12 Then
                            Throw New Exception("Check the length of Tax Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strtaxdes As String = grow.Cells("Tax Group Description").Value.ToString()
                        If strtaxdes.Length > 50 Then
                            Throw New Exception("Check the length of Tax Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim interbranch As String = ""
                        If interbranch.Length > 1 Then
                            Throw New Exception("Check the length of Inter Branch,See At Line No. " + clsCommon.myCstr(counter) + "")
                        ElseIf String.IsNullOrEmpty(interbranch) Then
                            interbranch = "N"
                        End If

                        Dim strTagAsFranchise As String = ""
                        If strTagAsFranchise.Length > 1 Then
                            Throw New Exception("Check the length of Tagged as Franchise,See At Line No. " + clsCommon.myCstr(counter) + "")
                        ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                            strTagAsFranchise = "N"
                        End If

                        '------------------------------------------------------------------------

                        Dim zila As String = clsCommon.myCstr(grow.Cells("zila").Value)
                        Dim tehsil As String = clsCommon.myCstr(grow.Cells("tehsil").Value)
                        Dim payeename As String = clsCommon.myCstr(grow.Cells("Payee Name").Value)
                        Dim strbankdes As String = String.Empty
                        Dim branchname As String = String.Empty
                        Dim branchcode As String = String.Empty
                        Dim ifcicode As String = String.Empty

                        Dim objVb As clsVendorBankMaster = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                        If objVb IsNot Nothing Then
                            strbankdes = objVb.Bank_Name
                        End If
                        ifcicode = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                        If clsCommon.myLen(ifcicode) > 100 Then
                            Throw New Exception("IFSC Code should be max 100 character")
                        End If
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                            Throw New Exception("IFSC Code Does Not Exist :  " + ifcicode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        branchname = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                        If clsCommon.myLen(branchname) > 100 Then
                            Throw New Exception("Branch Name should be max 100 character")
                        End If

                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & branchname & "'", trans) <= 0 Then
                            Throw New Exception("Branch Name Does Not Exist : " + branchname + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        branchcode = ifcicode
                        ''-------------------------

                        Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                        Dim accno As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                        Dim Industry_Type As String = clsCommon.myCstr(grow.Cells("Industry_Type").Value)
                        Dim Industry_Person As String = ""
                        Dim Agreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                        Dim Start_Date As String = clsCommon.myCstr(grow.Cells("Start_Date").Value)

                        If clsCommon.CompairString(Agreement, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(Start_Date) > 0 AndAlso IsDate(Start_Date) Then
                            Start_Date = clsCommon.GetPrintDate(clsCommon.myCDate(Start_Date), "dd/MMM/yyyy")
                        Else
                            Start_Date = ""
                        End If

                        Dim End_Date As String = clsCommon.myCstr(grow.Cells("End_Date").Value)

                        If clsCommon.CompairString(Agreement, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(End_Date) > 0 AndAlso IsDate(End_Date) Then
                            End_Date = clsCommon.GetPrintDate(clsCommon.myCDate(End_Date), "dd/MMM/yyyy")
                        Else
                            End_Date = ""
                        End If


                        If clsCommon.myLen(Industry_Type) <= 0 Then
                            Throw New Exception("Please Mention Industry Type,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(Industry_Type, "Prop.") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Prop. Person Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Partnership") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Partner Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Public") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Pvt") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim security As String = clsCommon.myCstr(grow.Cells("Security_Cheque").Value)
                        Dim noofinstlmnt As Decimal = clsCommon.myCdbl(grow.Cells("No_of_Installment").Value)
                        Dim amtofinstlmnt As Decimal = clsCommon.myCdbl(grow.Cells("Amount_of_Installment").Value)

                        'If clsCommon.CompairString(security, "NO") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(noofinstlmnt) <= 0 Or clsCommon.myCdbl(amtofinstlmnt) <= 0) Then
                        '    Throw New Exception("Please Fill No of Installment/Amount of Installment At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If
                        '---------------------------------------------------------------------------

                        '---------------------------------------------------------------------------


                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            Dim strcmd As String = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Ven_Type_Code ,Ven_Type_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,Collectorate,PAN,Inter_branch,franchise_yn,Form_Type,state_code,country_code,vsp_payee_name,zila,tehsil,branch_name,ifci_code,account_no,Industry_Type,Industry_Person,Agreement,security_cheque,no_of_installment,amount_of_installment,branch_code,Start_Date,End_Date,IFSC_Code,Cheque_In_favour_of,CURRENCY_CODE) values('" & strvendorNo & "','" & strvendorname & "','" & add1 & "','" & add2 & "','" & add3 & "','" & closing_date & "','" & strgroupCode & "','" & strgroupDes & "','" & citycode & "','" & citycodedesc & "','" & state & "','" & country & "','" & phonenum1 & "','" & phonenum2 & "','" & fax & "','" & emailid & "','" & website & "','" & strtermcode & "','" & strtermdes & "','" & vendoracct & "','" & vendoracctdesc & "','" & paymenttype & "','" & paymenttypedesc & "','" & strbank & "','" & strbankdes & "','" & strvendortype & "','" & strvendortypedes & "','" & strTax & "','" & strtaxdes & "','" & grow.Cells("Tax1").Value.ToString() & "','" & clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value.ToString()) & "','" & grow.Cells("Tax2").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) & "','" & grow.Cells("Tax3").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) & "','" & grow.Cells("Tax4").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) & "','" & grow.Cells("Tax5").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) & "','" & grow.Cells("Tax6").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) & "','" & grow.Cells("Tax7").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) & "','" & grow.Cells("Tax8").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) & "','" & grow.Cells("Tax9").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) & "','" & grow.Cells("Tax10").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) & "','" & grow.Cells("Transporter").Value.ToString() & "','" & objCommonVar.CurrentUserCode & "','" & connectSql.serverDate(trans) & "','" & objCommonVar.CurrentUserCode & "','" & connectSql.serverDate(trans) & "','" & objCommonVar.CurrentCompanyCode & "','" & grow.Cells("Collectorate").Value.ToString() & "','" & grow.Cells("PAN").Value.ToString() & "','" & interbranch & "','" & strTagAsFranchise & "','PTM','" & statecode & "','" & countrycode & "','" & payeename & "','" & zila & "','" & tehsil & "','" & branchname & "','" & ifcicode & "','" & accno & "','" & Industry_Type & "','" & Industry_Person & "','" & Agreement & "','" & security & "','" & noofinstlmnt & "','" & amtofinstlmnt & "','" & branchcode & "'," & IIf(clsCommon.myLen(Start_Date) > 0, "'" & Start_Date & "'", "null") & "," & IIf(clsCommon.myLen(End_Date) > 0, "'" & End_Date & "'", "null") & ",'" & branchcode & "','" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "','" & clsCommon.myCstr(objCommonVar.BaseCurrencyCode) & "')"
                            connectSql.RunSqlTransaction(trans, strcmd)
                        Else
                            Dim strcmd As String = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" & strvendorname & "',add1='" & add1 & "',add2='" & add2 & "',add3='" & add3 & "',Closing_Date='" & closing_date & "',Vendor_Group_Code='" & strgroupCode & "',Vendor_Group_Code_Desc='" & strgroupDes & "',City_Code='" & citycode & "',City_Code_Desc='" & citycodedesc & "',State='" & state & "',Country='" & country & "',Phone1='" & phonenum1 & "',Phone2='" & phonenum2 & "',Fax='" & fax & "',Email='" & emailid & "',WebSite='" & website & "',Contact_Person_Name='" & contct_person_name & "',Contact_Person_Phone='" & contct_perfson_phone & "',Contact_Person_Fax='" & contct_person_fax & "',Contact_Person_Website='" & contct_person_website & "',Contact_Person_Email='" & contct_person_email & "',Terms_Code='" & strtermcode & "',Terms_Code_Desc='" & strtermdes & "' ,Vendor_Account='" & vendoracct & "',Vendor_Account_Desc='" & vendoracctdesc & "',Payment_Code='" & paymenttype & "',Payment_Code_Desc='" & paymenttypedesc & "',Bank_Code='" & strbank & "', Bank_Code_Desc='" & strbankdes & "',Ven_Type_Code='" & strvendortype & "',Ven_Type_Desc='" & strvendortypedes & "' ,Tax_Group='" & strTax & "',Tax_Group_Desc='" & strtaxdes & "' ,TAX1='" & grow.Cells("Tax1").Value.ToString() & "',TAX1_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) & "',TAX2='" & grow.Cells("Tax2").Value.ToString() & "',TAX2_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) & "',TAX3='" & grow.Cells("Tax3").Value.ToString() & "',TAX3_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) & "',TAX4='" & grow.Cells("Tax4").Value.ToString() & "',TAX4_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) & "',TAX5='" & grow.Cells("Tax5").Value.ToString() & "',TAX5_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) & "',TAX6='" & grow.Cells("Tax6").Value.ToString() & "',TAX6_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) & "',TAX7='" & grow.Cells("Tax7").Value.ToString() & "',TAX7_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) & "',TAX8='" & grow.Cells("Tax8").Value.ToString() & "',TAX8_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) & "',TAX9='" & grow.Cells("Tax9").Value.ToString() & "',TAX9_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) & "',TAX10='" & grow.Cells("Tax10").Value.ToString() & "',TAX10_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) & "',Transporter='" & grow.Cells("Transporter").Value.ToString() & "',modify_by='" & objCommonVar.CurrentUserCode & "',modify_date='" & connectSql.serverDate(trans) & "',comp_code='" & objCommonVar.CurrentCompanyCode & "',Collectorate='" & grow.Cells("Collectorate").Value.ToString() & "',PAN='" & grow.Cells("PAN").Value.ToString() & "',Inter_Branch='" & interbranch & "', franchise_yn='" & strTagAsFranchise & "',form_type='PTM',state_code='" & statecode & "',country_code='" & countrycode & "',vsp_payee_name='" & payeename & "',zila='" & zila & "',tehsil='" & tehsil & "',branch_name='" & branchname & "',ifci_code='" & ifcicode & "',account_no='" & accno & "',Industry_Type='" & Industry_Type & "',Industry_Person='" & Industry_Person & "',Agreement='" & Agreement & "',security_cheque='" & security & "',no_of_installment='" & noofinstlmnt & "',amount_of_installment='" & amtofinstlmnt & "',branch_code='" & branchcode & "' , start_date=" & IIf(clsCommon.myLen(Start_Date) > 0, "'" & Start_Date & "'", "null") & ", End_Date=" & IIf(clsCommon.myLen(End_Date) > 0, "'" & End_Date & "'", "null") & ",IFSC_Code='" & branchcode & "',Cheque_In_favour_of='" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "',CURRENCY_CODE='" & clsCommon.myCstr(objCommonVar.BaseCurrencyCode) & "' where vendor_code='" & strvendorNo & "' and form_type='PTM'"
                            connectSql.RunSqlTransaction(trans, strcmd)
                        End If
                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Line No: " + Count + " - " + ex.Message)
                End Try

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Primary Transporter Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton79_Click(sender As Object, e As EventArgs) Handles RadButton79.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim Count As String = """"
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
            If transportSql.importExcel(gv, "Transporter No", "Transporter Name", "Address1", "Group Code", "Vendor Group Description", "City Code", "State", "Country", "Phone Num1", "Email Id", "Terms Code", "Vendor Account", "Vendor Account Description", "Bank Code", "PAN", "Payee Name ", "Account No", "Industry Type", "Prop Name", "Agreement", "Agreement Date", "Expiry Date", "IFSC_Code", "Branch_Name", "Cheque in Favour of", "Incentive", "Multiple Incentive(0/1)", "Aadhar_No", "Care_Of", "SecChequeNoLac1", "SecChequeNoRs100") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim FixVSPEMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixVSPEMP, clsFixedParameterCode.FixVSPEMP, trans))
                    clsCommon.ProgressBarPercentShow()
                    Dim counter As Integer = 1
                    Dim IsBlacklisted As Integer = 0
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Transporter No").Value)
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of Transporter No.,")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("Transporter No. can not be blank,")
                        End If

                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of Transporter Name can not be greater than 100.,")
                        End If

                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("Transporter Name can not be blank")
                        End If
                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim closing_date As String = System.DateTime.Now.Date
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the length of Group Code")
                        End If

                        Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the length of Group Code Description")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)

                        Dim state As String = ""
                        Dim country As String = ""

                        Dim statecode As String = clsCommon.myCstr(grow.Cells("State").Value)
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("Country").Value)
                        Dim check As Integer = 0

                        If clsCommon.myLen(countrycode) <= 0 Then
                            Throw New Exception("Please Fill Country")
                        End If
                        If clsCommon.myLen(statecode) <= 0 Then
                            Throw New Exception("Please Fill State")
                        End If
                        If clsCommon.myLen(citycode) <= 0 Then
                            Throw New Exception("Please Fill City")
                        End If

                        If clsCommon.myLen(countrycode) > 0 Then
                            qry = "select COUNTRY_CODE,COUNTRY_NAME from tspl_country_master   where country_code='" + countrycode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Country Code Does Not Exist,Please Make Country Master")
                            End If
                            countrycode = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
                            country = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
                        End If
                        If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                            qry = "select STATE_CODE,STATE_NAME from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                            End If
                            statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                            state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                        End If

                        If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                            qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("City Code Does Not Exist,Please Make Its Master First")
                            End If
                        End If


                        Dim payeename As String = clsCommon.myCstr(grow.Cells("Payee Name").Value).Replace("'", "`")
                        Dim AccountNo As String = ""

                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim contct_person_name As String = ""
                        Dim contct_perfson_phone As String = ""
                        Dim contct_person_fax As String = ""
                        Dim contct_person_website As String = ""
                        Dim contct_person_email As String = ""

                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Vendor Account can not be blank")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the length of Vendor Account Set Code")
                        End If

                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                        Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                        'If String.IsNullOrEmpty(strbank) Then
                        '    Throw New Exception("Bank Code can not be blank")
                        'End If
                        'Dim i5 As String

                        'Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                        'i5 = connectSql.RunScalar(trans, qry7)
                        'If i5 = 0 Then
                        '    Throw New Exception("Bank code does not exist : " + strbank + "")
                        'End If
                        If strbank.Length > 30 Then
                            Throw New Exception("Check the length of bank code")
                        End If
                        Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                        Dim strTagAsFranchise As String = ""
                        If strTagAsFranchise.Length > 1 Then
                            Throw New Exception("Check the length of Tagged as Franchise")
                        ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                            strTagAsFranchise = "N"
                        End If
                        Dim strIFSCCode As String = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                        'If clsCommon.myLen(strIFSCCode) > 100 Then
                        '    Throw New Exception("IFSC Code should be max 100 character")
                        'End If
                        'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                        '    Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        'End If
                        Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                        If clsCommon.myLen(strBrachName) > 100 Then
                            Throw New Exception("Branch Name should be max 100 character")
                        End If
                        'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                        '    Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        'End If
                        ' ''-------------------------

                        Dim strAgreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                        Dim strAgreementDate As String = clsCommon.myCstr(grow.Cells("Agreement Date").Value)
                        Dim strExpiryDate As String = clsCommon.myCstr(grow.Cells("Expiry Date").Value)
                        If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strAgreementDate) <= 0 Then
                                Throw New Exception("Agreement date can not be left blank")
                            End If
                            If clsCommon.myLen(strExpiryDate) <= 0 Then
                                Throw New Exception("Expiry date can not be left blank")
                            End If
                            Try
                                Convert.ToDateTime(strAgreementDate)
                            Catch exx As Exception
                                Throw New Exception("Agreement date should be in proper date format")
                            End Try
                            Try
                                Convert.ToDateTime(strExpiryDate)
                            Catch exx As Exception
                                Throw New Exception("Expiry date should be in proper date format")
                            End Try
                        End If


                        'If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                        '    qry = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(grow.Cells("PAN").Value) + "' and Form_Type='PTM' and vendor_Code<>'" & clsCommon.myCstr(grow.Cells("Transporter No").Value) & "'"
                        '    check = clsDBFuncationality.getSingleValue(qry, trans)
                        '    If check > 0 Then
                        '        Throw New Exception("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                        '        Return
                        '    End If
                        '    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                        '        If Not checkPan.IsMatch(clsCommon.myCstr(grow.Cells("PAN").Value)) Then
                        '            Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                        '        End If
                        '    End If
                        'End If

                        Dim incentv As String = clsCommon.myCstr(grow.Cells("incentive").Value).Replace("'", "`")
                        If clsCommon.myLen(incentv) > 0 Then
                            Dim qryincentive As String = "select count(*) from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE='" + incentv + "'"
                            check = clsDBFuncationality.getSingleValue(qryincentive, trans)
                            If check <= 0 Then
                                Throw New Exception("Incentive does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                        If String.IsNullOrEmpty(strtermcode) Then
                            Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i1 As Integer
                        Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                        i1 = connectSql.RunScalar(trans, qry1)
                        If i1 = 0 Then
                            Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If strtermcode.Length > 12 Then
                            Throw New Exception("Check the length of Terms Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim strAadharNo As String = clsCommon.myCstr(grow.Cells("Aadhar_No").Value)
                        Dim strCareOf As String = clsCommon.myCstr(grow.Cells("Care_Of").Value)
                        Dim strSecChequeNoLac1 As String = clsCommon.myCstr(grow.Cells("SecChequeNoLac1").Value)
                        Dim strSecChequeNoRs100 As String = clsCommon.myCstr(grow.Cells("SecChequeNoRs100").Value)
                        If clsCommon.myLen(strAadharNo) > 0 Then
                            If clsCommon.myLen(strAadharNo) <> 12 Then
                                Throw New Exception("Aadhar No should be 12 character")
                            End If
                        End If

                        Dim Industry_Type As String = clsCommon.myCstr(grow.Cells("Industry Type").Value)
                        Dim Industry_Person As String = Nothing
                        'If clsCommon.myLen(Industry_Type) <= 0 Then
                        '    Throw New Exception("Please Mention Industry Type,See At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If

                        'If clsCommon.CompairString(Industry_Type, "Prop.") = CompairStringResult.Equal Then
                        '    Industry_Person = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                        '    If clsCommon.myLen(Industry_Person) <= 0 Then
                        '        Throw New Exception("Please Mention Prop. Person Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        '    End If
                        'ElseIf clsCommon.CompairString(Industry_Type, "Partnership") = CompairStringResult.Equal Then
                        '    Industry_Person = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                        '    If clsCommon.myLen(Industry_Person) <= 0 Then
                        '        Throw New Exception("Please Mention Partner Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        '    End If
                        'ElseIf clsCommon.CompairString(Industry_Type, "Public") = CompairStringResult.Equal Then
                        '    Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                        '    If clsCommon.myLen(Industry_Person) <= 0 Then
                        '        Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        '    End If
                        'ElseIf clsCommon.CompairString(Industry_Type, "Pvt") = CompairStringResult.Equal Then
                        '    Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                        '    If clsCommon.myLen(Industry_Person) <= 0 Then
                        '        Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        '    End If
                        'End If


                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                        clsCommon.AddColumnsForChange(coll, "add1", add1)
                        clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                        clsCommon.AddColumnsForChange(coll, "City_Code", citycode)
                        clsCommon.AddColumnsForChange(coll, "State", state)
                        clsCommon.AddColumnsForChange(coll, "Country", country)
                        clsCommon.AddColumnsForChange(coll, "Phone1", phonenum1)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account", vendoracct)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account_Desc", vendoracctdesc)
                        clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(grow.Cells("PAN").Value))
                        clsCommon.AddColumnsForChange(coll, "form_type", "PTM")
                        clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                        clsCommon.AddColumnsForChange(coll, "country_code", countrycode)
                        clsCommon.AddColumnsForChange(coll, "branch_code", strIFSCCode)
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                        clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(grow.Cells("Account No").Value))
                        clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                        clsCommon.AddColumnsForChange(coll, "Agreement ", strAgreement.ToUpper())
                        If clsCommon.CompairString(strAgreement, "YES") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(strAgreementDate, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(strExpiryDate, "dd/MMM/yyyy"))
                        Else
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                        End If


                        clsCommon.AddColumnsForChange(coll, "Joint_Account_No", AccountNo)
                        clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                        clsCommon.AddColumnsForChange(coll, "Cheque_in_Favour_of", Cheque_In_favour_of)
                        clsCommon.AddColumnsForChange(coll, "Status", "N")
                        clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                        clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                        clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                        clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(grow.Cells("Email Id").Value))
                        clsCommon.AddColumnsForChange(coll, "Terms_Code", strtermcode)
                        clsCommon.AddColumnsForChange(coll, "Industry_Type", Industry_Type)
                        clsCommon.AddColumnsForChange(coll, "industry_person", Industry_Person)
                        clsCommon.AddColumnsForChange(coll, "incentive", incentv)
                        clsCommon.AddColumnsForChange(coll, "Apply_Mult_Incentive", clsCommon.myCstr(grow.Cells("Multiple Incentive(0/1)").Value))
                        clsCommon.AddColumnsForChange(coll, "Aadhar_No", strAadharNo)
                        clsCommon.AddColumnsForChange(coll, "Care_Of", strCareOf)
                        clsCommon.AddColumnsForChange(coll, "SecChequeNoLac1", strSecChequeNoLac1)
                        clsCommon.AddColumnsForChange(coll, "SecChequeNoRs100", strSecChequeNoRs100)
                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", strvendorNo)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + strvendorNo + "' and form_type='PTM'", trans)
                        End If
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Line: " + clsCommon.myCstr(LineNo) + " - " + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Primary Transporter Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton78_Click(sender As Object, e As EventArgs) Handles RadButton78.Click
        Try
            Me.Text = "Primary Transporter Vehicle"
            qry = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description,TSPL_Primary_Vehicle_Master.vendor_code as [Primary Transporter Code],tspl_vendor_master.vendor_name as [Primary Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_Primary_Vehicle_Master.storage_capacity as [Storage Capacity],TSPL_Primary_Vehicle_Master.manufacturing_year as [Manufacturing Year],TSPL_Primary_Vehicle_Master.STATUS as [Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)],Shift_Charges as [Charges per Day],Avg_Km_Ltr as [Average KM per Ltr],Diesel_Rate as [Rate of Diesel],Rental_type as [Rental Type],Rental_Amount as [Rental Amount],TSPL_Primary_Vehicle_Master.price_km as [Rate per KM],Rate_type as [Rate Type],Price_ltr_kg as [Price Ltr/KG],TSPL_Primary_Vehicle_Master.Vehicle_Weight as [Vehicle Weight] "
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) = 1 Then
                qry += ",TSPL_Primary_Vehicle_Master.Vehicle "
            End If
            qry += " from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Primary Transporter Vehicle")
        End Try
    End Sub

    Private Sub RadButton77_Click(sender As Object, e As EventArgs) Handles RadButton77.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
            Dim inputs() As String = {}
            If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                inputs = {"Code", "Description", "Primary Transporter Code", "Primary Transporter Name", "MCC Code", "MCC Name", "Storage Capacity", "Manufacturing Year", "Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate per KM", "Rate Type", "Price Ltr/KG", "Vehicle Weight", "Vehicle"}
            Else
                inputs = {"Code", "Description", "Primary Transporter Code", "Primary Transporter Name", "MCC Code", "MCC Name", "Storage Capacity", "Manufacturing Year", "Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate per KM", "Rate Type", "Price Ltr/KG", "Vehicle Weight"}
            End If
            Dim Strs As List(Of String) = New List(Of String)(inputs)
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                ', "Veh_Fitness_No", "Veh_Fitness_Date", "Veh_Insurance_No", "Veh_Insurance_Date"

                Dim counter As Integer = 1
                Try
                    Dim obj As clsfrmPrimaryTransporterVehicalMaster
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        obj = New clsfrmPrimaryTransporterVehicalMaster()
                        Dim index As Integer = 0

                        obj.docno = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.docno) <= 0 Then
                            Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 30 Then
                            Throw New Exception("Length of Vehicle No.(Code) Should Not Exceed 30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            index = obj.docno.IndexOf(" ")
                            If index > 0 AndAlso index < clsCommon.myLen(obj.docno) Then
                                Throw New Exception("There Should Be No white Space Between Vehicle No. At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        obj.desc = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                        If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                            Throw New Exception("Please Fill Description And Length Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim qry As String = ""
                        obj.primarycode = clsCommon.myCstr(grow.Cells("Primary Transporter Code").Value)
                        obj.primaryname = clsCommon.myCstr(grow.Cells("Primary Transporter Name").Value).Replace("'", "`")
                        If clsCommon.myLen(obj.primarycode) <= 0 AndAlso clsCommon.myLen(obj.primaryname) <= 0 Then
                            Throw New Exception("Please Fill Primary Transporter Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.primarycode) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where vendor_code='" + obj.primarycode + "' and form_type='PTM'"
                            index = clsDBFuncationality.getSingleValue(qry)

                            If index <= 0 Then
                                qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                                obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                                If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                    Throw New Exception("Filled Primary Transporter Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.primaryname) > 0 Then
                            qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                            obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                            If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                Throw New Exception("Filled Primary Transporter Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '-------------------------------------------------------------

                        obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value).Replace("'", "`")
                        If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                            index = clsDBFuncationality.getSingleValue(qry)

                            If index <= 0 Then
                                qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                                If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                    Throw New Exception("Filled MCC Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.mccname) > 0 Then
                            qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                            If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                Throw New Exception("Filled MCC Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '---------------------

                        '------------check primary transporter mapped with other mcc-----------------
                        Dim checkmcccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_code from tspl_primary_vehicle_master where vendor_code='" + obj.primarycode + "'"))
                        If clsCommon.myLen(checkmcccode) > 0 AndAlso clsCommon.CompairString(checkmcccode, obj.mcccode) <> CompairStringResult.Equal Then
                            Throw New Exception("Filled MCC Code/Name Is Invalid" + Environment.NewLine + "Primary Transporter Code Is Mapped With Other MCC Code i.e (" + checkmcccode + ") At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        '------------------------------------------------------------------------

                        obj.capacity = clsCommon.myCdbl(grow.Cells("Storage Capacity").Value)
                        obj.year = clsCommon.myCstr(grow.Cells("Manufacturing Year").Value)
                        If clsCommon.myLen(obj.year) <= 0 Then
                            Throw New Exception("Please Fill Manufacturing Year At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        '--------------------
                        obj.Vehicle_Weight = clsCommon.myCdbl(grow.Cells("Vehicle Weight").Value)
                        obj.chagrshift = clsCommon.myCdbl(grow.Cells("Charges per Day").Value)
                        obj.avgrate = clsCommon.myCdbl(grow.Cells("Average KM per Ltr").Value)
                        obj.dieselrate = clsCommon.myCdbl(grow.Cells("Rate of Diesel").Value)
                        obj.RentalType = clsCommon.myCstr(grow.Cells("Rental Type").Value)
                        obj.RentalAmount = clsCommon.myCdbl(grow.Cells("Rental Amount").Value)
                        obj.pricekm = clsCommon.myCdbl(grow.Cells("Rate per KM").Value)
                        obj.Rate_Type = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                        obj.Price_Ltr_KG = clsCommon.myCdbl(grow.Cells("Price Ltr/KG").Value)
                        'obj.Veh_Fitness_No = clsCommon.myCstr(grow.Cells("Veh_Fitness_No").Value)
                        'obj.Veh_Fitness_Date = clsCommon.myCstr(grow.Cells("Veh_Fitness_Date").Value)
                        'obj.Veh_Insurance_No = clsCommon.myCstr(grow.Cells("Veh_Insurance_No").Value)
                        'obj.Veh_Insurance_Date = clsCommon.myCstr(grow.Cells("Veh_Insurance_Date").Value)
                        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                            obj.Vehicle = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                        End If

                        obj.status = clsCommon.myCstr(grow.Cells("Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)").Value)
                        If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                            If obj.chagrshift <= 0 Then
                                Throw New Exception("Please Fill Charges per Day At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If obj.avgrate <= 0 Then
                                Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If obj.dieselrate <= 0 Then
                                Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                            If obj.RentalAmount <= 0 Then
                                Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If Not (clsCommon.CompairString(obj.RentalType, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Year") = CompairStringResult.Equal) Then
                                Throw New Exception("Rental Type should be Day,Month,Year  At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                            If obj.Price_Ltr_KG <= 0 Then
                                Throw New Exception("Please Fill Price Ltr/KG At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "KG") = CompairStringResult.Equal) Then
                                Throw New Exception("Rate Type should be LTR,KG  At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                            If obj.pricekm <= 0 Then
                                Throw New Exception("Please Fill Rate per KM At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                            If obj.RentalAmount <= 0 Then
                                Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If obj.avgrate <= 0 Then
                                Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If obj.dieselrate <= 0 Then
                                Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                        ElseIf clsCommon.myLen(obj.status) > 0 Then
                            Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.docno + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                        Dim isNewEntry As Boolean = True
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                            If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                            Else
                                Throw New Exception("No Data Transfer")
                            End If
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at line no " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Primary Transporter Vehicle")
        Finally
        End Try
    End Sub

    Private Sub RadButton76_Click(sender As Object, e As EventArgs) Handles RadButton76.Click
        Try
            Me.Text = "Milk Price Chart "
            qry = "select price_code as Code,Description,Convert (varchar, Effective_Date,103) as [Effective Date], Convert (varchar,Inactive_Date,103) as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Declared_Rate as [Declared Rate] from TSPL_MILK_PRICE_MASTER"
            Dim whrcls As String = " and [price_type]='MCC'"
            transportSql.ExporttoExcel(qry, whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Milk Price Chart ")
        End Try
    End Sub

    Private Sub RadButton75_Click(sender As Object, e As EventArgs) Handles RadButton75.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim counter As Integer = 1
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Code", "Description", "Effective Date", "Inactive Date", "FAT Ratio", "SNF Ratio", "FAT %", "SNF %", "Milk Rate", "Declared Rate") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim obj As clsfrmPriceChartMaster = Nothing
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        obj = New clsfrmPriceChartMaster()
                        obj.code = clsCommon.myCstr(grow.Cells("code").Value)
                        obj.desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                            Throw New Exception("Length Of Price Description Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        'obj.ratetype = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                        obj.declrd_rate = clsCommon.myCdbl(grow.Cells("Declared Rate").Value)
                        obj.effctv_rate = 0
                        'obj.axis = clsCommon.myCstr(grow.Cells("axis type").Value)
                        'obj.matrix = clsCommon.myCstr(grow.Cells("matrix type").Value)
                        'Try
                        '    obj.effct_dt = clsCommon.myCDate(grow.Cells("Effective Date").Value)
                        'Catch exx As Exception
                        '    obj.effct_dt = clsCommon.GETSERVERDATE(trans)
                        'End Try
                        'Try
                        '    obj.inactv_dt = (grow.Cells("Inactive Date").Value)
                        'Catch exx As Exception
                        '    obj.inactv_dt = clsCommon.GETSERVERDATE(trans)
                        'End Try
                        '==============================================
                        Dim effectiveDate As String = clsCommon.myCstr((grow.Cells("Effective Date").Value))
                        If IsDate(effectiveDate) = False Then
                            Throw New Exception("Invalid Formate of Effective Date.Date Formate should be dd/mm/yyyy.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        obj.effct_dt = clsCommon.myCDate(grow.Cells("Effective Date").Value)

                        Dim inactiveDate As String = clsCommon.myCstr((grow.Cells("Inactive Date").Value))
                        If IsDate(inactiveDate) = False Then
                            Throw New Exception("Invalid Formate of Inactive Date.Date Formate should be dd/mm/yyyy.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        obj.inactv_dt = clsCommon.myCDate(grow.Cells("Inactive Date").Value)
                        If Not clsCommon.myCDate(obj.inactv_dt) >= clsCommon.myCDate(obj.effct_dt) Then
                            Throw New Exception("Inactive Date Should Be Greater Or Equal To Effective Date.At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        '==============================================

                        obj.ratio = clsCommon.myCstr(grow.Cells("FAT Ratio").Value)
                        obj.snf_ratio = clsCommon.myCstr(grow.Cells("SNF Ratio").Value)
                        If (clsCommon.myCdbl(obj.ratio) + clsCommon.myCdbl(obj.snf_ratio)) < 100 Then
                            Throw New Exception("Sum of ratio of fat and snf should be equal to 100 ,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        obj.fat_pers = clsCommon.myCdbl(grow.Cells("FAT %").Value)
                        obj.snf_pers = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                        obj.rate = clsCommon.myCdbl(grow.Cells("Milk Rate").Value)
                        obj.price_type = "MCC"
                        '=======================================
                        Dim isposted As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_PRICE_MASTER where price_code='" + obj.code + "' and price_type='" + obj.price_type + "' and Posted = 1", trans))
                        If isposted = True Then
                            Throw New Exception("Price Code ('" + obj.code + "') already posted.You can not update this price code.,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        '========================================
                        Dim qry As String = "select count(*) from TSPL_MILK_PRICE_MASTER where price_code='" + obj.code + "' and price_type='" + obj.price_type + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        Dim isNewEntry As Boolean = True
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                            'obj.code = clsERPFuncationality.GetNextCode(trans, obj.effct_dt, clsDocType.MILKPRCMASTER, "", "")
                        End If
                        clsfrmPriceChartMaster.SaveData(obj.code, isNewEntry, obj, trans)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Row No " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Milk Price Chart ")
        Finally

        End Try
    End Sub

    Private Sub RadButton72_Click(sender As Object, e As EventArgs) Handles RadButton72.Click
        Try
            Me.Text = "Incentive Master"
            Dim sQuery As String = "select TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE as [Incentive Code],Description,INCENTIVE_DATE as [Incentive Date],START_DATE as [Start Date]," _
        & " end_date as [End Date],case when starting_shift='M' then 'Morning' else 'Evening' end as [Starting Shift],case when ending_shift='M' then" _
        & " 'Morning' else 'Evening' end as [Ending Shift],case when TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QB' then 'Quantity Based' " _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QSLAB' then 'Slab Based'" _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='TSB' then 'TS Based' " _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QTSSLAB' then 'Quantity And TS Based'" _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QSLABTSSLAB' then 'Slab and TS Based' end as [Incentive Type],Case when Calc_Type='F' then 'Flat' when Calc_Type='A' then 'Avg' end as [Calculation Type]," _
        & " SCHEME_FOR as [Scheme For],case when Rate_Type='Q' then 'Quantitative' when rate_type='F' then 'Fat Rate' end as [Rate Type],case when TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE='QB' then SLAB_FROM else 0 end as [Qty]," _
        & " case when TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE<>'QB' then SLAB_FROM else 0 end as [Slab From],SLAB_TO as [Slab To],TS_FROM as [TS From]," _
        & " TS_TO as [TS To],Rate,RATE_UOM as [Rate UOM],TSPL_INCENTIVE_MASTER_HEAD.Qty_Type as [Quantity Type],TSPL_INCENTIVE_DETAIL.Parameter_Type as [Parameter Type],TSPL_INCENTIVE_DETAIL.OPERATER_TYPE as [Operater Type] from TSPL_INCENTIVE_MASTER_HEAD left join TSPL_INCENTIVE_DETAIL on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE" _
        & " =TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE"
            transportSql.ExporttoExcel(sQuery, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Incentive Master")
        End Try
    End Sub

    Private Sub RadButton71_Click(sender As Object, e As EventArgs) Handles RadButton71.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Incentive Code", "Description", "Incentive Date", "Start Date", "End Date", "Starting Shift", "Ending Shift", "Incentive Type", "Calculation Type", "Scheme For", "Rate Type", "Qty", "Slab From", "Slab To", "TS From", "TS To", "Rate", "Rate UOM", "Quantity Type", "Parameter Type", "Operater Type") Then
                Try
                    Dim obj As clsIncentiveMaster = Nothing
                    Dim counter As Integer = 1
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1

                        obj = New clsIncentiveMaster()
                        obj.INCENTIVE_CODE = clsCommon.myCstr(grow.Cells("Incentive Code").Value)
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Incentive Date").Value)) > 0 Then
                            obj.INCENTIVE_DATE = clsCommon.myCDate(grow.Cells("Incentive Date").Value)
                        Else
                            Throw New Exception("Please Enter Incentive Date At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(obj.INCENTIVE_CODE) <= 0 Then
                            obj.INCENTIVE_CODE = clsERPFuncationality.GetNextCode(Nothing, obj.INCENTIVE_DATE, clsDocType.IncentiveMaster, "", "")
                        End If

                        obj.DESCRIPTION = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.DESCRIPTION) <= 0 Or clsCommon.myLen(obj.DESCRIPTION) > 150 Then
                            Throw New Exception("Length Of Price Description Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If



                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Start Date").Value)) > 0 Then
                            obj.START_DATE = clsCommon.myCDate(grow.Cells("Start Date").Value)
                        Else
                            Throw New Exception("Please Enter Start Date At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("End Date").Value)) > 0 Then
                            obj.END_DATE = clsCommon.myCDate(grow.Cells("End Date").Value)
                        Else
                            Throw New Exception("Please End End Date At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Starting Shift").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Starting Shift").Value), "Morning") = CompairStringResult.Equal Then
                                obj.Starting_Shift = "M"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Starting Shift").Value), "Evening") = CompairStringResult.Equal Then
                                obj.Starting_Shift = "E"
                            Else
                                Throw New Exception("Please End Starting Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                            End If
                        Else
                            Throw New Exception("Please End Starting Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Ending Shift").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Ending Shift").Value), "Morning") = CompairStringResult.Equal Then
                                obj.Ending_Shift = "M"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Ending Shift").Value), "Evening") = CompairStringResult.Equal Then
                                obj.Ending_Shift = "E"
                            Else
                                Throw New Exception("Please End Ending Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                            End If

                        Else
                            Throw New Exception("Please End Ending Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Incentive Type").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Quantity Based") = CompairStringResult.Equal Then
                                obj.INCENTIVE_TYPE = "QB"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Slab Based") = CompairStringResult.Equal Then
                                obj.INCENTIVE_TYPE = "QSLAB"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "TS Based") = CompairStringResult.Equal Then
                                obj.INCENTIVE_TYPE = "TSB"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Quantity and TS Based") = CompairStringResult.Equal Then
                                obj.INCENTIVE_TYPE = "QTSSLAB"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Slab and TS Based") = CompairStringResult.Equal Then
                                obj.INCENTIVE_TYPE = "QSLABTSSLAB"
                            Else
                                Throw New Exception("Please End Incentive Type At Line No. " + clsCommon.myCstr(counter) + ".")
                            End If
                        Else
                            Throw New Exception("Please End Incentive Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Calculation Type").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Flat") = CompairStringResult.Equal Then
                                obj.Calc_Type = "F"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Avg") = CompairStringResult.Equal Then
                                obj.Calc_Type = "A"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Avg.") = CompairStringResult.Equal Then
                                obj.Calc_Type = "A"
                            Else
                                Throw New Exception("Please End Calculation Type At Line No. " + clsCommon.myCstr(counter) + ".")
                            End If

                        Else
                            Throw New Exception("Please End Calculation Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        '' validation for Quantity Type 
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Quantity Type").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "ACTQ") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "Actual Quantity") = CompairStringResult.Equal Then
                                obj.Calc_Type = "ACTQ"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "STDQ") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "Standard Quantity") = CompairStringResult.Equal Then
                                obj.Calc_Type = "STDQ"
                            Else
                                Throw New Exception("Please enter valid Quantity Type At Line No. " + clsCommon.myCstr(counter) + ".")
                            End If
                        Else
                            Throw New Exception("Please enter Quantity Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Scheme For").Value)) > 0 Then
                            obj.SCHEME_FOR = clsCommon.myCstr(grow.Cells("Scheme For").Value)
                        Else
                            Throw New Exception("Please End Scheme For At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Rate Type").Value)) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "FAT Rate") = CompairStringResult.Equal Then
                                obj.Rate_type = "F"
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "Quantitative") = CompairStringResult.Equal Then
                                obj.Rate_type = "Q"
                            End If
                        Else
                            Throw New Exception("Please Enter Rate Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                        '===============Detail=============================================
                        Dim obj1 As clsIncentiveMasterDetail
                        Dim ObjList As New List(Of clsIncentiveMasterDetail)
                        Dim line_No As Integer = 1
                        For Each grows As GridViewRowInfo In gv.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Code").Value), clsCommon.myCstr(grows.Cells("Incentive Code").Value)) = CompairStringResult.Equal Then
                                obj1 = New clsIncentiveMasterDetail()

                                obj1.INCENTIVE_CODE = obj.INCENTIVE_CODE
                                obj1.LINE_NO = line_No 'clsCommon.myCdbl(grows.Cells(colLineNo).Value)
                                obj1.INCENTIVE_TYPE = clsCommon.myCstr(grows.Cells("Incentive Type").Value)
                                obj1.RATE_UOM = clsCommon.myCstr(grows.Cells("Rate Uom").Value)
                                obj1.RATE = clsCommon.myCstr(grows.Cells("Rate").Value)
                                obj1.FOR_PERIOD = clsCommon.myCstr(grows.Cells("Scheme For").Value)

                                obj1.SLAB_TO = clsCommon.myCdbl(grows.Cells("Slab To").Value)

                                obj1.INCENTIVE_TYPE = obj.INCENTIVE_TYPE
                                obj1.TS_FROM = clsCommon.myCdbl(grows.Cells("TS From").Value)
                                obj1.TS_TO = clsCommon.myCdbl(grows.Cells("TS To").Value)
                                obj1.Parameter_Type = clsCommon.myCstr(grows.Cells("Parameter Type").Value)
                                obj1.OPERATER_TYPE = clsCommon.myCstr(grows.Cells("Operater Type").Value)

                                If obj.INCENTIVE_TYPE = "QB" Or obj.INCENTIVE_TYPE = "QTSSLAB" Then
                                    obj1.SLAB_FROM = clsCommon.myCdbl(grows.Cells("Qty").Value)
                                    If obj1.SLAB_FROM <= 0 Then
                                        Throw New Exception("Please Enter Quantity At Line No. " + clsCommon.myCstr(counter) + ".")
                                    End If
                                    If obj.INCENTIVE_TYPE = "QTSSLAB" Then
                                        If obj1.TS_FROM <= 0 Or obj1.TS_TO <= 0 Then
                                            Throw New Exception("Please Enter TS At Line No. " + clsCommon.myCstr(counter) + ".")
                                        End If
                                    End If
                                Else
                                    obj1.SLAB_FROM = clsCommon.myCdbl(grows.Cells("Slab From").Value)
                                    If obj.INCENTIVE_TYPE = "TSB" Or obj.INCENTIVE_TYPE = "QSLABTSSLAB" Then
                                        If obj1.TS_FROM <= 0 Or obj1.TS_TO <= 0 Then
                                            Throw New Exception("Please Enter TS At Line No. " + clsCommon.myCstr(counter) + ".")
                                        End If
                                    End If
                                    If obj.INCENTIVE_TYPE = "QSLAB" Or obj.INCENTIVE_TYPE = "QSLABTSSLAB" Then
                                        If obj1.SLAB_FROM <= 0 AndAlso obj1.SLAB_TO <= 0 Then
                                            Throw New Exception("Please Enter Slab Details At Line No. " + clsCommon.myCstr(counter) + ".")
                                        End If
                                    End If
                                    '' update [Parameter Type] and [Operater Type]
                                    If obj.INCENTIVE_TYPE = "QLTY" Then
                                        If clsCommon.myLen(obj1.Parameter_Type) <= 0 Then
                                            Throw New Exception("Please Enter Parameter Type At Line No. " & clsCommon.myCstr(counter) & ".")
                                        ElseIf Not (clsCommon.CompairString(obj1.Parameter_Type, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.Parameter_Type, "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.Parameter_Type, "CLR") = CompairStringResult.Equal) Then
                                            Throw New Exception("Parameter Type entered at line No. " & clsCommon.myCstr(counter) & " is not valid(FAT,SNF,CLR).")
                                        ElseIf Not (clsCommon.CompairString(obj1.OPERATER_TYPE, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "Continue") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "OR") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "XOR") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "AND") = CompairStringResult.Equal) Then
                                            Throw New Exception("Operater Type entered at line No. " & clsCommon.myCstr(counter) & " is not valid(None,Continue,OR,AND).")
                                        End If
                                    End If
                                End If

                                line_No += 1
                                ObjList.Add(obj1)
                            End If
                        Next
                        '==================================================================
                        Dim qry As String = "select count(*) from TSPL_Incentive_MASTER_Head where Incentive_code='" + obj.INCENTIVE_CODE + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                        Dim isNewEntry As Boolean = False
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If obj.SaveData(obj, ObjList, isNewEntry) Then
                            counter += 1
                        End If
                    Next

                    clsCommon.ProgressBarPercentHide()
                    Reset()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Incentive Master")
        Finally

        End Try
    End Sub

    Private Sub RadButton70_Click(sender As Object, e As EventArgs) Handles RadButton70.Click

        Try
            Me.Text = "MCC Reason Master"
            Dim str As String
            str = "select Code as [Code],Name As [Name]  from Tspl_Mcc_Reason_Master"
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "MCC Reason Master")
        End Try
    End Sub

    Private Sub RadButton69_Click(sender As Object, e As EventArgs) Handles RadButton69.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Try
            Me.Controls.Add(gv)
            If transportSql.importExcel(gv, "Code", "Name") Then
                Dim linno As Integer = 1
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        linno += 1
                        Dim obj As New clsTrainingMaster()
                        Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(strCode) <= 0 Then
                            Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim strName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                        If clsCommon.myLen(strName) <= 0 Then
                            Throw New Exception("Name should not be left blank" + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Code = strCode
                        obj.Name = strName
                        obj.tb_Name = "Tspl_Mcc_Reason_Master"
                        clsTrainingMaster.SaveData(obj, IsNewEntry)
                    Next
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow("Error at Line No:" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "MCC Reason Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton68_Click(sender As Object, e As EventArgs) Handles RadButton68.Click
        Try
            Me.Text = "Secondry Transporter Master"
            qry = " select Vendor_Code as [Transporter No],Vendor_Name as [Transporter Name],Add1 as [Address1],Vendor_Group_Code as [Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],State_Code as [State],Country_Code as [Country],Phone1 as [Phone Num1],Email as [Email Id],Terms_Code as [Terms Code] ,Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Bank_Code as [Bank Code],PAN as [PAN], VSP_Payee_Name as [Payee Name],Account_No As [Account No],Industry_Type as [Industry Type],(case when industry_type='Prop.' then industry_person else '' end) as [Prop Name] ,IFSC_Code,branch_code,Branch_Name from TSPL_VENDOR_MASTER "
            Dim whrCls As String = " and form_type='TTM' "
            transportSql.ExporttoExcel(qry, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub OLD2()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim Count As String = ""
            If transportSql.importExcel(gv, "Tanker Transporter No", "Tanker Transporter Name", "Address1", "Address2", "Address3", "Group Code", "Tanker Transporter Group Description", "City", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Tanker Transporter Account", "Tanker Transporter Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Tanker Transporter Type", "Tanker Transporter Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Collectorate", "PAN", "State_Code", "Country_Code", "Zila", "Tehsil", "Payee Name", "Account_No", "Industry_Type", "Prop Name", "Partner Name", "Director Name", "IsPermanent", "IsTemporary", "Agreement", "Security_Cheque", "form_type", "IFSC_Code", "Branch_Name", "Cheque in Favour of", "Minimum Qty Required pertrip") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    Dim counter As Integer = 1

                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Count = clsCommon.myCstr(grow.Index + 2)
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Tanker Transporter No").Value)
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of Tanker Transporter No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("Tanker Transporter No. can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Tanker Transporter Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of Tanker Transporter Name can not be greater than 100.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("Tanker Transporter Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                        Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                        Dim closing_date As String
                        closing_date = System.DateTime.Now.Date
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Tanker Transporter Group Code does not exist : " + strgroupCode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the length of Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strgroupDes As String = grow.Cells("Tanker Transporter Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the length of Group Code Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City").Value)
                        Dim citycodedesc As String = citycode
                        Dim state As String = clsCommon.myCstr(grow.Cells("State").Value)
                        Dim country As String = clsCommon.myCstr(grow.Cells("Country").Value)
                        Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                        Dim check As Integer = 0

                        If clsCommon.myLen(countrycode) <= 0 Then
                            Throw New Exception("Please Fill Country,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(statecode) <= 0 Then
                            Throw New Exception("Please Fill State,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(citycode) <= 0 Then
                            Throw New Exception("Please Fill City,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.myLen(countrycode) > 0 Then
                            qry = "select count(*) from tspl_country_master where country_code='" + countrycode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("Country Code Does Not Exist,Please Make Country Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                            qry = "select count(*) from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                        Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                        Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                        Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                        Dim contct_person_name As String = ""
                        Dim contct_perfson_phone As String = ""
                        Dim contct_person_fax As String = ""
                        Dim contct_person_website As String = ""
                        Dim contct_person_email As String = ""
                        Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                        If String.IsNullOrEmpty(strtermcode) Then
                            Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i1 As Integer
                        Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                        i1 = connectSql.RunScalar(trans, qry1)
                        If i1 = 0 Then
                            Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If strtermcode.Length > 12 Then
                            Throw New Exception("Check the length of Terms Code")
                        End If

                        Dim strtermdes As String = clsCommon.myCstr(grow.Cells("Terms Description").Value)
                        If strtermdes.Length > 50 Then
                            Throw New Exception("Check the length of Term Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Tanker Transporter Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Tanker Transporter Account can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Tanker Transporter Account Does Not Exist : " + vendoracct + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the length of Tanker Transporter Account Set Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Tanker Transporter Account Description").Value)

                        Dim paymenttype As String = clsCommon.myCstr(grow.Cells("Payment Code").Value)
                        Dim i4 As String
                        If Not String.IsNullOrEmpty(paymenttype) Then
                            Dim qry5 As String = "select COUNT(*) from TSPL_PAYMENT_CODE  where Payment_Code ='" + paymenttype + "'"
                            i4 = connectSql.RunScalar(trans, qry5)
                            If i4 = 0 Then
                                Throw New Exception("Payment Code Does Not Exist : " + paymenttype + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If paymenttype.Length > 12 Then
                                Throw New Exception("Check the length of Payment Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                        Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                        If String.IsNullOrEmpty(strbank) Then
                            Throw New Exception("Bank Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i5 As String

                        Dim qry7 As String = "select COUNT(*) from TSPL_Vendor_BANK_MASTER  where Bank_Code ='" + strbank + "'"
                        i5 = connectSql.RunScalar(trans, qry7)
                        If i5 = 0 Then
                            Throw New Exception("Bank Code Does Not Exist : " + strbank + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim strvendortype As String = clsCommon.myCstr(grow.Cells("Tanker Transporter Type").Value)
                        Dim strvendortypedes As String = grow.Cells("Tanker Transporter Type Description").Value.ToString()
                        If strvendortype.Length > 12 Then
                            Throw New Exception("Check the length of Tanker Transporter Type,See At Line No. " + clsCommon.myCstr(counter) + " ")
                        End If
                        If strvendortypedes.Length > 50 Then
                            Throw New Exception("Check the length of Tanker Transporter Type Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim strTax As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                        If String.IsNullOrEmpty(strTax) Then
                            Throw New Exception(" Tax Group can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i6 As String
                        Dim qry9 As String = "select COUNT(*) from  TSPL_TAX_GROUP_MASTER   where tax_group_Code ='" + strTax + "'"
                        i6 = connectSql.RunScalar(trans, qry9)
                        If i6 = 0 Then
                            Throw New Exception("Tax Group Code Does Not Exist : " + strTax + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If strTax.Length > 12 Then
                            Throw New Exception("Check the length of Tax Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim strtaxdes As String = grow.Cells("Tax Group Description").Value.ToString()
                        If strtaxdes.Length > 50 Then
                            Throw New Exception("Check the length of Tax Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim interbranch As String = "" ' grow.Cells("Inter Branch").Value.ToString()
                        If interbranch.Length > 1 Then
                            Throw New Exception("Check the length of Inter Branch,See At Line No. " + clsCommon.myCstr(counter) + "")
                        ElseIf String.IsNullOrEmpty(interbranch) Then
                            interbranch = "N"
                        End If

                        Dim strTagAsFranchise As String = "" 'grow.Cells("Tagged as Franchise").Value.ToString()
                        If strTagAsFranchise.Length > 1 Then
                            Throw New Exception("Check the length of Tagged as Franchise,See At Line No. " + clsCommon.myCstr(counter) + "")
                        ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                            strTagAsFranchise = "N"
                        End If


                        '------------------------------------------------------------------------

                        Dim zila As String = clsCommon.myCstr(grow.Cells("zila").Value)
                        Dim tehsil As String = clsCommon.myCstr(grow.Cells("tehsil").Value)
                        Dim payeename As String = clsCommon.myCstr(grow.Cells("Payee Name").Value)
                        Dim strbankdes As String = String.Empty
                        Dim branchname As String = String.Empty
                        Dim branchcode As String = String.Empty
                        Dim ifcicode As String = String.Empty

                        Dim objVb As clsVendorBankMaster = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                        If objVb IsNot Nothing Then
                            strbankdes = objVb.Bank_Name
                        End If
                        ifcicode = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                        If clsCommon.myLen(ifcicode) > 100 Then
                            Throw New Exception("IFSC Code should be max 100 character")
                        End If
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                            Throw New Exception("IFSC Code Does Not Exist :  " + ifcicode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        branchname = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                        If clsCommon.myLen(branchname) > 100 Then
                            Throw New Exception("Branch Name should be max 100 character")
                        End If

                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & branchname & "'", trans) <= 0 Then
                            Throw New Exception("Branch Name Does Not Exist : " + branchname + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        branchcode = ifcicode
                        Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                        Dim accno As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                        Dim Industry_Type As String = clsCommon.myCstr(grow.Cells("Industry_Type").Value)
                        Dim Industry_Person As String = ""
                        Dim ispermanent As String = clsCommon.myCstr(grow.Cells("IsPermanent").Value)
                        Dim istemp As String = clsCommon.myCstr(grow.Cells("IsTemporary").Value)
                        Dim security As String = clsCommon.myCstr(grow.Cells("Security_Cheque").Value).ToUpper()
                        Dim Agreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value).ToUpper()

                        If (ispermanent <> "1" AndAlso ispermanent <> "0") AndAlso (istemp <> "1" AndAlso istemp <> "0") Then
                            Throw New Exception("Please Select One From IsPermanent Or IsTemporary,(0 For No And 1 For Yes) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If ispermanent = "1" AndAlso (Agreement <> "NO" AndAlso Agreement <> "YES") Then
                            Throw New Exception("Please Fill YES/NO For Agreement At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If ispermanent = "1" AndAlso (security <> "NO" AndAlso security <> "YES") Then
                            Throw New Exception("Please Fill YES/NO For Security Cheque At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.myLen(Industry_Type) <= 0 Then
                            Throw New Exception("Please Mention Industry Type,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If Not IsNumeric(grow.Cells("Minimum Qty Required pertrip").Value) Then
                            Throw New Exception("Minimum Qty Required pertrip should be numeric,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(Industry_Type, "Prop.") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Prop. Person Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Partnership") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Partner Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Public") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Pvt") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '---------------------------------------------------------------------------
                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            Dim strcmd As String = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name,Contact_Person_Phone ,Contact_Person_Fax ,Contact_Person_Website,Contact_Person_Email,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Ven_Type_Code ,Ven_Type_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,Collectorate,PAN,Inter_branch,franchise_yn,Form_Type,state_code,country_code,vsp_payee_name,zila,tehsil,branch_name,ifci_code,account_no,Industry_Type,Industry_Person,Agreement,Security_Cheque,IsPermanent,IsTemporary,branch_code,IFSC_Code,Cheque_In_favour_of,MinimumQtyRequired_pertrip,CURRENCY_CODE) values('" + strvendorNo + "','" + strvendorname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + closing_date + "','" + strgroupCode + "','" + strgroupDes + "','" + citycode + "','" + citycodedesc + "','" + state + "','" + country + "','" + phonenum1 + "','" + phonenum2 + "','" + fax + "','" + emailid + "','" + website + "','" + contct_person_name + "','" + contct_perfson_phone + "','" + contct_person_fax + "','" + contct_person_website + "','" + contct_person_email + "','" + strtermcode + "','" + strtermdes + "','" + vendoracct + "','" + vendoracctdesc + "','" + paymenttype + "','" + paymenttypedesc + "','" + strbank + "','" + strbankdes + "','" + strvendortype + "','" + strvendortypedes + "','" + strTax + "','" + strtaxdes + "','" + grow.Cells("Tax1").Value.ToString() + "','" + grow.Cells("Tax1 Rate").Value.ToString() + "','" + grow.Cells("Tax2").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "','" + grow.Cells("Tax3").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "','" + grow.Cells("Tax4").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "','" + grow.Cells("Tax5").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "','" + grow.Cells("Tax6").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "','" + grow.Cells("Tax7").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "','" + grow.Cells("Tax8").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "','" + grow.Cells("Tax9").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "','" + grow.Cells("Tax10").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "','" + grow.Cells("Transporter").Value.ToString() + "','" + objCommonVar.CurrentUser + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentUser + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentCompanyCode + "','" + grow.Cells("Collectorate").Value.ToString() + "','" + grow.Cells("PAN").Value.ToString() + "','" + interbranch + "','" + strTagAsFranchise + "','TTM','" + statecode + "','" + countrycode + "','" + payeename + "','" + zila + "','" + tehsil + "','" + branchname + "','" + ifcicode + "','" + accno + "','" + Industry_Type + "','" + Industry_Person + "','" + Agreement + "','" + security + "','" + ispermanent + "','" + istemp + "','" & branchcode & "','" & branchcode & "','" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "'," & clsCommon.myCstr(grow.Cells("Minimum Qty Required pertrip").Value) & ",'" & clsCommon.myCstr(objCommonVar.BaseCurrencyCode) & "')"
                            connectSql.RunSqlTransaction(trans, strcmd)
                        Else
                            Dim strcmd As String = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" + strvendorname + "',add1='" + add1 + "',add2='" + add2 + "',add3='" + add3 + "',Closing_Date='" + closing_date + "',Vendor_Group_Code='" + strgroupCode + "',Vendor_Group_Code_Desc='" + strgroupDes + "',City_Code='" + citycode + "',City_Code_Desc='" + citycodedesc + "',State='" + state + "',Country='" + country + "',Phone1='" + phonenum1 + "',Phone2='" + phonenum2 + "',Fax='" + fax + "',Email='" + emailid + "',WebSite='" + website + "',Contact_Person_Name='" + contct_person_name + "',Contact_Person_Phone='" + contct_perfson_phone + "',Contact_Person_Fax='" + contct_person_fax + "',Contact_Person_Website='" + contct_person_website + "',Contact_Person_Email='" + contct_person_email + "',Terms_Code='" + strtermcode + "',Terms_Code_Desc='" + strtermdes + "' ,Vendor_Account='" + vendoracct + "',Vendor_Account_Desc='" + vendoracctdesc + "',Payment_Code='" + paymenttype + "',Payment_Code_Desc='" + paymenttypedesc + "',Bank_Code='" + strbank + "', Bank_Code_Desc='" + strbankdes + "',Ven_Type_Code='" + strvendortype + "',Ven_Type_Desc='" + strvendortypedes + "' ,Tax_Group='" + strTax + "',Tax_Group_Desc='" + strtaxdes + "' ,TAX1='" + grow.Cells("Tax1").Value.ToString() + "',TAX1_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "',TAX2='" + grow.Cells("Tax2").Value.ToString() + "',TAX2_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "',TAX3='" + grow.Cells("Tax3").Value.ToString() + "',TAX3_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "',TAX4='" + grow.Cells("Tax4").Value.ToString() + "',TAX4_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "',TAX5='" + grow.Cells("Tax5").Value.ToString() + "',TAX5_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "',TAX6='" + grow.Cells("Tax6").Value.ToString() + "',TAX6_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "',TAX7='" + grow.Cells("Tax7").Value.ToString() + "',TAX7_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "',TAX8='" + grow.Cells("Tax8").Value.ToString() + "',TAX8_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "',TAX9='" + grow.Cells("Tax9").Value.ToString() + "',TAX9_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "',TAX10='" + grow.Cells("Tax10").Value.ToString() + "',TAX10_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "',Transporter='" + grow.Cells("Transporter").Value.ToString() + "',modify_by='" + objCommonVar.CurrentUser + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + objCommonVar.CurrentCompanyCode + "',Collectorate='" + grow.Cells("Collectorate").Value.ToString() + "',PAN='" + grow.Cells("PAN").Value.ToString() + "',Inter_Branch='" + interbranch + "', franchise_yn='" + strTagAsFranchise + "',form_type='TTM',state_code='" + statecode + "',country_code='" + countrycode + "',vsp_payee_name='" + payeename + "',zila='" + zila + "',tehsil='" + tehsil + "',branch_name='" + branchname + "',ifci_code='" + ifcicode + "',account_no='" + accno + "',Industry_Type='" + Industry_Type + "',Industry_Person='" + Industry_Person + "',Agreement='" + Agreement + "',Security_Cheque='" + security + "',IsPermanent='" + ispermanent + "',IsTemporary='" + istemp + "',branch_code='" & branchcode & "',IFSC_Code='" & branchcode & "' ,Cheque_In_favour_of='" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "',MinimumQtyRequired_pertrip=" & clsCommon.myCstr(grow.Cells("Minimum Qty Required pertrip").Value) & ",CURRENCY_CODE='" & clsCommon.myCstr(objCommonVar.BaseCurrencyCode) & "' where vendor_code='" + strvendorNo + "' and form_type='TTM'"
                            connectSql.RunSqlTransaction(trans, strcmd)
                        End If

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)
                End Try

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Secondry Transport Vehicle Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub
    Private Sub RadButton67_Click(sender As Object, e As EventArgs) Handles RadButton67.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim Count As String = ""
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
            If transportSql.importExcel(gv, "Transporter No", "Transporter Name", "Address1", "Group Code", "Vendor Group Description", "City Code", "State", "Country", "Phone Num1", "Email Id", "Terms Code", "Vendor Account", "Vendor Account Description", "Bank Code", "PAN", "Payee Name ", "Account No", "Industry Type", "Prop Name", "IFSC_Code", "Branch_Name") Then
                Dim trans As SqlTransaction = Nothing
                Try
                    Dim FixVSPEMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixVSPEMP, clsFixedParameterCode.FixVSPEMP, Nothing))
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    Dim counter As Integer = 1
                    Dim IsBlacklisted As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Transporter No").Value)
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of Transporter No.,")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("Transporter No. can not be blank,")
                        End If

                        Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                        Dim strvendorname As String = strvendorname1.Replace("'", "''")
                        If strvendorname.Length > 100 Then
                            Throw New Exception("Length of Transporter Name can not be greater than 100.,")
                        End If

                        If String.IsNullOrEmpty(strvendorname) Then
                            Throw New Exception("Transporter Name can not be blank")
                        End If
                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim closing_date As String = System.DateTime.Now.Date
                        Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        If String.IsNullOrEmpty(strgroupCode) Then
                            Throw New Exception(" Group Code can not be blank")
                        End If
                        Dim i As Integer
                        Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                        i = connectSql.RunScalar(trans, qry)
                        If i = 0 Then
                            Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                        Else
                        End If
                        If strgroupCode.Length > 12 Then
                            Throw New Exception("Check the length of Group Code")
                        End If

                        Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                        If strgroupDes.Length > 50 Then
                            Throw New Exception("Check the length of Group Code Description")
                        End If
                        Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)

                        Dim state As String = ""
                        Dim country As String = ""

                        Dim statecode As String = clsCommon.myCstr(grow.Cells("State").Value)
                        Dim countrycode As String = clsCommon.myCstr(grow.Cells("Country").Value)
                        Dim check As Integer = 0

                        If clsCommon.myLen(countrycode) <= 0 Then
                            Throw New Exception("Please Fill Country")
                        End If
                        If clsCommon.myLen(statecode) <= 0 Then
                            Throw New Exception("Please Fill State")
                        End If
                        If clsCommon.myLen(citycode) <= 0 Then
                            Throw New Exception("Please Fill City")
                        End If

                        If clsCommon.myLen(countrycode) > 0 Then
                            qry = "select COUNTRY_CODE,COUNTRY_NAME from tspl_country_master   where country_code='" + countrycode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Country Code Does Not Exist,Please Make Country Master")
                            End If
                            countrycode = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
                            country = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
                        End If
                        If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                            qry = "select STATE_CODE,STATE_NAME from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                            End If
                            statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                            state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                        End If

                        If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                            qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            If check <= 0 Then
                                Throw New Exception("City Code Does Not Exist,Please Make Its Master First")
                            End If
                        End If


                        Dim payeename As String = clsCommon.myCstr(grow.Cells("Payee Name").Value).Replace("'", "`")



                        Dim AccountNo As String = ""


                        Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim contct_person_name As String = ""
                        Dim contct_perfson_phone As String = ""
                        Dim contct_person_fax As String = ""
                        Dim contct_person_website As String = ""
                        Dim contct_person_email As String = ""

                        Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                        If String.IsNullOrEmpty(vendoracct) Then
                            Throw New Exception(" Vendor Account can not be blank")
                        End If
                        Dim i3 As String

                        Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                        i3 = connectSql.RunScalar(trans, qry3)
                        If i3 = 0 Then
                            Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + "")
                        End If
                        If vendoracct.Length > 12 Then
                            Throw New Exception("Check the length of Vendor Account Set Code")
                        End If

                        Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                        Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                        If String.IsNullOrEmpty(strbank) Then
                            Throw New Exception("Bank Code can not be blank")
                        End If
                        Dim i5 As String

                        Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                        i5 = connectSql.RunScalar(trans, qry7)
                        If i5 = 0 Then
                            Throw New Exception("Bank code does not exist : " + strbank + "")
                        End If
                        If strbank.Length > 30 Then
                            Throw New Exception("Check the length of bank code")
                        End If
                        'Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                        Dim strTagAsFranchise As String = ""
                        If strTagAsFranchise.Length > 1 Then
                            'Throw New Exception("Check the length of Tagged as Franchise")
                        ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                            strTagAsFranchise = "N"
                        End If
                        Dim strIFSCCode As String = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                        If clsCommon.myLen(strIFSCCode) > 100 Then
                            Throw New Exception("IFSC Code should be max 100 character")
                        End If
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                            Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                        If clsCommon.myLen(strBrachName) > 100 Then
                            Throw New Exception("Branch Name should be max 100 character")
                        End If
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                            Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                        ''-------------------------

                        'Dim strAgreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                        'Dim strAgreementDate As String = clsCommon.myCstr(grow.Cells("Agreement Date").Value)
                        'Dim strExpiryDate As String = clsCommon.myCstr(grow.Cells("Expiry Date").Value)
                        'If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                        '    If clsCommon.myLen(strAgreementDate) <= 0 Then
                        '        Throw New Exception("Agreement date can not be left blank")
                        '    End If
                        '    If clsCommon.myLen(strExpiryDate) <= 0 Then
                        '        Throw New Exception("Expiry date can not be left blank")
                        '    End If
                        '    Try
                        '        Convert.ToDateTime(strAgreementDate)
                        '    Catch exx As Exception
                        '        Throw New Exception("Agreement date should be in proper date format")
                        '    End Try
                        '    Try
                        '        Convert.ToDateTime(strExpiryDate)
                        '    Catch exx As Exception
                        '        Throw New Exception("Expiry date should be in proper date format")
                        '    End Try
                        'End If


                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(grow.Cells("PAN").Value) + "' and Form_Type='TTM' and vendor_Code<>'" & clsCommon.myCstr(grow.Cells("Transporter No").Value) & "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check > 0 Then
                                Throw New Exception("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                                Return
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                                If Not checkPan.IsMatch(clsCommon.myCstr(grow.Cells("PAN").Value)) Then
                                    Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                                End If
                            End If
                        End If

                        'Dim incentv As String = clsCommon.myCstr(grow.Cells("incentive").Value).Replace("'", "`")
                        'If clsCommon.myLen(incentv) > 0 Then
                        '    Dim qryincentive As String = "select count(*) from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE='" + incentv + "'"
                        '    check = clsDBFuncationality.getSingleValue(qryincentive, trans)
                        '    If check <= 0 Then
                        '        Throw New Exception("Incentive does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                        '    End If
                        'End If

                        Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                        If String.IsNullOrEmpty(strtermcode) Then
                            Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim i1 As Integer
                        Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                        i1 = connectSql.RunScalar(trans, qry1)
                        If i1 = 0 Then
                            Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If strtermcode.Length > 12 Then
                            Throw New Exception("Check the length of Terms Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If


                        Dim Industry_Type As String = clsCommon.myCstr(grow.Cells("Industry Type").Value)
                        Dim Industry_Person As String = Nothing
                        If clsCommon.myLen(Industry_Type) <= 0 Then
                            Throw New Exception("Please Mention Industry Type,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(Industry_Type, "Prop.") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Prop. Person Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Partnership") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Partner Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Public") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(Industry_Type, "Pvt") = CompairStringResult.Equal Then
                            Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                            If clsCommon.myLen(Industry_Person) <= 0 Then
                                Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If


                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                        clsCommon.AddColumnsForChange(coll, "add1", add1)
                        clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                        clsCommon.AddColumnsForChange(coll, "City_Code", citycode)
                        clsCommon.AddColumnsForChange(coll, "State", state)
                        clsCommon.AddColumnsForChange(coll, "Country", country)
                        clsCommon.AddColumnsForChange(coll, "Phone1", phonenum1)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account", vendoracct)
                        clsCommon.AddColumnsForChange(coll, "Vendor_Account_Desc", vendoracctdesc)
                        clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(grow.Cells("PAN").Value))
                        clsCommon.AddColumnsForChange(coll, "form_type", "TTM")
                        clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                        clsCommon.AddColumnsForChange(coll, "country_code", countrycode)
                        clsCommon.AddColumnsForChange(coll, "branch_code", strIFSCCode)
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                        clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(grow.Cells("Account No").Value))
                        clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                        clsCommon.AddColumnsForChange(coll, "Joint_Account_No", AccountNo)
                        clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                        clsCommon.AddColumnsForChange(coll, "Cheque_in_Favour_of", "")
                        clsCommon.AddColumnsForChange(coll, "Status", "N")
                        clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                        clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                        clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                        clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(grow.Cells("Email Id").Value))
                        clsCommon.AddColumnsForChange(coll, "Terms_Code", strtermcode)
                        clsCommon.AddColumnsForChange(coll, "Industry_Type", Industry_Type)
                        clsCommon.AddColumnsForChange(coll, "industry_person", Industry_Person)
                        'clsCommon.AddColumnsForChange(coll, "incentive", incentv)
                        'clsCommon.AddColumnsForChange(coll, "Apply_Mult_Incentive", clsCommon.myCstr(grow.Cells("Multiple Incentive(0/1)").Value))

                        Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", strvendorNo)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + strvendorNo + "' and form_type='TTM'", trans)
                        End If
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line: " + clsCommon.myCstr(LineNo) + " - " + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Secondary Transporter Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton66_Click(sender As Object, e As EventArgs) Handles RadButton66.Click
        Try
            Me.Text = "Secondry Transporter Vehicle"

            qry = "select TSPL_TANKER_MASTER.Tanker_No as TankerNo,TSPL_TANKER_MASTER.Tanker_Name as TankerName,TSPL_VENDOR_MASTER.Vendor_Code as TankerTransporterCode,TSPL_VENDOR_MASTER.Vendor_Name as TankerTransporterName,TSPL_TANKER_MASTER.Storage_Capacity as [StorageCapacity],TSPL_TANKER_MASTER.StorageCapacityDesc,TSPL_TANKER_MASTER.Year  as ManufacturingYear,TSPL_TANKER_MASTER.Inner_SS as InnerSS,TSPL_TANKER_MASTER.Outer_SS as OuterSS,Shift_Charges as ShiftCharges,Avg_KM_Ltr as AvgKMLtr,Diesel_Rate as DieselRate,Price_KM as PriceKM,Rate_Type as RateType,Price_Ltr_Kg as PriceLtrKg,Rental_Type as RentalType,Rental_Amount as RentalAmount,Provision_Min_Qty  as ProvisionMinQty from TSPL_VENDOR_MASTER right outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code and TSPL_VENDOR_MASTER.form_type='TTM'"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Secondry Transporter Vehicle")
        End Try
    End Sub

    Private Sub RadButton65_Click(sender As Object, e As EventArgs) Handles RadButton65.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim result As Boolean = False
            Dim counter As Integer = 0
            result = transportSql.importExcel(gv, "TankerNo", "TankerName", "TankerTransporterCode", "TankerTransporterName", "StorageCapacity", "StorageCapacityDesc", "ManufacturingYear", "InnerSS", "OuterSS", "ShiftCharges", "AvgKMLtr", "DieselRate", "PriceKM", "RateType", "PriceLtrKg", "RentalType", "RentalAmount", "ProvisionMinQty")
            If result Then
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        counter += 1
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New clsfrmTankerMaster()
                        obj.code = clsCommon.myCstr(grow.Cells("TankerTransporterCode").Value)
                        obj.desc = clsCommon.myCstr(grow.Cells("TankerTransporterName").Value)
                        If clsCommon.myLen(obj.code) <= 0 Then
                            Throw New Exception("Please First Make The Tanker Transporter Of No. " + obj.code + " See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        obj.code = clsDBFuncationality.getSingleValue("select vendor_code from tspl_vendor_master where vendor_code='" + obj.code + "' and form_type='TTM'", tran)
                        If clsCommon.myLen(obj.code) <= 0 Then
                            Throw New Exception("Not a valid The Tanker Transporter.See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        obj.tankerno = clsCommon.myCstr(grow.Cells("TankerNo").Value)
                        obj.tanker_name = clsCommon.myCstr(grow.Cells("TankerName").Value)
                        If clsCommon.myLen(obj.tanker_name) > 150 Then
                            Throw New Exception("Tanker Name Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myCdbl(grow.Cells("StorageCapacity").Value) = 0 Then
                            Throw New Exception("Storage Capacity cannot be left blank or 0,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        obj.Provision_Min_Qty = clsCommon.myCdbl(grow.Cells("ProvisionMinQty").Value)
                        obj.storagecap = clsCommon.myCdbl(grow.Cells("StorageCapacity").Value)
                        obj.year = clsCommon.myCstr(grow.Cells("ManufacturingYear").Value)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("StorageCapacityDesc").Value).ToUpper(), "LITRE") = CompairStringResult.Equal Then
                            obj.StorageCapacityDesc = "Litre"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("StorageCapacityDesc").Value).ToUpper(), "KG") = CompairStringResult.Equal Then
                            obj.StorageCapacityDesc = "KG"
                        ElseIf clsCommon.myLen(grow.Cells("StorageCapacityDesc").Value) <= 0 Then
                            Throw New Exception("Storage Capacity Desc cannot be left blank At Line No. " + clsCommon.myCstr(counter) + "")
                        Else
                            Throw New Exception("Storage Capacity Desc should be Litre/ KG ,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.year) <= 0 Then
                            Throw New Exception("Please fill year of manufacturing See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Try
                            If clsCommon.myLen(obj.tankerno) > 0 Then
                                Dim index As Integer = 0
                                index = clsCommon.myCstr(obj.tankerno).IndexOf(" ")
                                If index > 0 AndAlso index < clsCommon.myLen(obj.tankerno) Then
                                    Throw New Exception("No White Space Allowed In Tanker No. See At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        Catch exx As Exception
                            Throw New Exception(exx.Message)
                        End Try
                        Try
                            If clsCommon.myLen(obj.storagecap) > 0 Then
                                Convert.ToDecimal(obj.storagecap)
                            End If
                        Catch exx As Exception
                            Throw New Exception("Storage Capacity Should Be In Numeric See At Line No. " + clsCommon.myCstr(counter) + "")
                        End Try

                        obj.inner = clsCommon.myCstr(grow.Cells("InnerSS").Value)
                        If Not (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("InnerSS").Value), "NO") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("InnerSS").Value), "YES") = CompairStringResult.Equal) Then
                            Throw New Exception("InnerSS Should be YES/NO")
                        End If

                        obj.outer = clsCommon.myCstr(grow.Cells("OuterSS").Value)
                        If Not (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("OuterSS").Value), "NO") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("OuterSS").Value), "YES") = CompairStringResult.Equal) Then
                            Throw New Exception("OuterSS Should be YES/NO")
                        End If
                        obj.shift_chrg = clsCommon.myCdbl(grow.Cells("ShiftCharges").Value)
                        obj.avg_km_rate = clsCommon.myCdbl(grow.Cells("AvgKMLtr").Value)
                        obj.diesel_rate = clsCommon.myCdbl(grow.Cells("DieselRate").Value)
                        If clsCommon.myCdbl(obj.shift_chrg) > 0 Or clsCommon.myCdbl(obj.avg_km_rate) > 0 Or clsCommon.myCdbl(obj.diesel_rate) > 0 Then
                            obj.Status = "Day/Diesel"
                        End If
                        obj.RentalType = clsCommon.myCstr(grow.Cells("RentalType").Value)
                        obj.RentalAmount = clsCommon.myCdbl(grow.Cells("RentalAmount").Value)
                        If clsCommon.myLen(obj.RentalType) > 0 Or clsCommon.myCdbl(obj.RentalAmount) > 0 Then
                            obj.Status = "Rental"
                        End If
                        obj.rate_km = clsCommon.myCdbl(grow.Cells("PriceKM").Value)
                        If clsCommon.myCdbl(obj.rate_km) > 0 Then
                            obj.Status = "Rate/K.M"
                        End If
                        obj.Rate_Type = clsCommon.myCstr(grow.Cells("RateType").Value)
                        obj.Price_Ltr_KG = clsCommon.myCdbl(grow.Cells("PriceLtrKg").Value)
                        If clsCommon.myLen(obj.Rate_Type) > 0 Or clsCommon.myCdbl(obj.Price_Ltr_KG) > 0 Then
                            obj.Status = "Rate/Ltr"
                        End If
                        If clsCommon.myCdbl(grow.Cells("RentalAmount").Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells("DieselRate").Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells("AvgKMLtr").Value) > 0 Then
                            obj.Status = "Rental/Diesel"
                            obj.RentalAmount = clsCommon.myCdbl(grow.Cells("RentalAmount").Value)
                            obj.diesel_rate = clsCommon.myCdbl(grow.Cells("DieselRate").Value)
                            obj.avg_km_rate = clsCommon.myCdbl(grow.Cells("AvgKMLtr").Value)
                        End If

                        If clsCommon.myCdbl(grow.Cells("ShiftCharges").Value) <= 0 AndAlso clsCommon.myCdbl(grow.Cells("AvgKMLtr").Value) <= 0 AndAlso clsCommon.myCdbl(grow.Cells("DieselRate").Value) <= 0 AndAlso clsCommon.myCdbl(grow.Cells("PriceLtrKg").Value) <= 0 AndAlso clsCommon.myCdbl(grow.Cells("RentalAmount").Value) <= 0 AndAlso clsCommon.myCdbl(grow.Cells("PriceKM").Value) <= 0 Then
                            obj.Status = "KM_Range"
                        End If
                        If clsCommon.myLen(obj.Status) <= 0 Then
                            Throw New Exception("Please Select Fill Any One Rate Form Basic of Freight Payments At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(obj.shift_chrg) <= 0 Or clsCommon.myCdbl(obj.avg_km_rate) <= 0 Or clsCommon.myCdbl(obj.diesel_rate) <= 0) Then
                            Throw New Exception("Please Fill Rate Per Day + Diesel(Charges per Day/Average KM per Ltr./Rate of Diesel) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(obj.Status, "Rate/K.M") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(obj.rate_km) <= 0 Then
                            Throw New Exception("Please Fill Rate per K.M At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(obj.Status, "Rate/Ltr") = CompairStringResult.Equal AndAlso (clsCommon.myLen(obj.Rate_Type) <= 0 Or clsCommon.myCdbl(obj.Price_Ltr_KG) <= 0) Then
                            Throw New Exception("Please Fill Rate Type : Ltr./Kg And  Price Ltr/Kg At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        qry = "select count(*) from TSPL_TANKER_MASTER where Tanker_No='" + obj.tankerno + "'"
                        Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, tran))
                        Dim isNewEntry As Boolean = True
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        clsfrmTankerMaster.SaveData(obj.tankerno, isNewEntry, obj, tran)
                    Next
                    tran.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    tran.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Secondry Transporter Vehicle")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton62_Click(sender As Object, e As EventArgs) Handles RadButton62.Click
        Try
            Me.Text = "Route Master"
            qry = "Select Route_No as [Route No],Route_Desc as [Route Desc], Type,Employee_Code as [Employee Code],Off_Day as [Off Day],City_Code as [City Code],District ,Category_Code as [Category Code],Length,Employee_Name as [Employee Name],Depot_Id as [Depot Id],Price_Code as [Price Code],Price_Code_Desc as[Price Code Desc],NONPrice_Code as [Non Price Code],vehicle_code as [Vehicle Code],Status as [Status],SDate as [Status Date],RoutePrice_Code as [Route Price Code],IsEarlyRoute as [IsEarlyRoute(0/1)] from TSPL_ROUTE_MASTER"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Milk Route Master")
        End Try
    End Sub

    Private Sub RadButton61_Click(sender As Object, e As EventArgs) Handles RadButton61.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim ii As Integer = 0
        Try
            Dim trans As SqlTransaction = Nothing
            If transportSql.importExcel(gv, "Route No", "Route Desc", "Type", "Employee Code", "Off Day", "City Code", "District", "Category Code", "Length", "Employee Name", "Depot Id", "Price Code", "Price Code Desc", "Non Price Code", "Vehicle Code", "Status", "Status Date", "Route Price Code", "IsEarlyRoute(0/1)") Then
                clsCommon.ProgressBarPercentShow()
                Try
                    connectSql.OpenConnection()
                    trans = clsDBFuncationality.GetTransactin()
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim strRoute_no As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If clsCommon.myLen(strRoute_no) > 0 Then
                            If clsCommon.myLen(strRoute_no) > 12 Then
                                Throw New Exception("Route Code Can not be left Blank or size can not be grater than 12")
                            End If
                            Dim strstrRoute_desc As String = clsCommon.myCstr(grow.Cells(1).Value)
                            If String.IsNullOrEmpty(strstrRoute_desc) Or clsCommon.myLen(strstrRoute_desc) > 50 Then
                                Throw New Exception("Route Description Can not be left Blank or size can not be grater than 50")
                            End If
                            Dim strtypeTemp As String = clsCommon.myCstr(grow.Cells(2).Value)
                            Dim qry As String = "select  * from TSPL_ROUTE_TYPE where Route_Type_Id='" + strtypeTemp + "'"
                            Dim strtype As String = clsCommon.myCstr(connectSql.RunScalar(trans, qry))
                            If clsCommon.myLen(strtype) <= 0 And clsCommon.myLen(strtypeTemp) > 0 Then
                                Throw New Exception("Route type " + strtypeTemp + " is not Exist")
                            End If
                            Dim stremp_code As String = clsCommon.myCstr(grow.Cells(3).Value)
                            If clsCommon.myLen(stremp_code) > 12 Then
                                Throw New Exception("Employee Code Can not be left Blank or size can not be grater than 12")
                            End If
                            Dim stroffday As String = clsCommon.myCstr(grow.Cells(4).Value)
                            If clsCommon.myLen(stroffday) > 12 Then 'String.IsNullOrEmpty(stroffday) Or 
                                Throw New Exception("Off Day Can not be left Blank or size can not be grater than 12")
                            End If
                            Dim strCitycode As String = clsCommon.myCstr(grow.Cells(5).Value)
                            If clsCommon.myLen(strCitycode) > 12 Then
                                Throw New Exception("City code can not be greater than 12")
                            End If
                            Dim strDistrict As String = clsCommon.myCstr(grow.Cells(6).Value)
                            If clsCommon.myLen(strDistrict) > 50 Then
                                Throw New Exception("District can not be greater than 50")
                            End If
                            Dim strcat_code As String = clsCommon.myCstr(grow.Cells(7).Value)
                            If clsCommon.myLen(strcat_code) > 12 Then 'String.IsNullOrEmpty(strcat_code) Or 
                                Throw New Exception("Category Code Can not be left Blank or size can not be grater than 12")
                            End If
                            Dim re As Regex = New Regex("(^[0-9]*[1-9]+[0-9]*\.[0-9]*$)|(^[0-9]*\.[0-9]*[1-9]+[0-9]*$)|(^[0-9]*[1-9]+[0-9]*$)")
                            Dim strLength As String = clsCommon.myCdbl(grow.Cells(8).Value)
                            If clsCommon.myLen(strLength) > 8 Or Not IsNumeric(strLength) Then
                                Throw New Exception("Length can not be greater than 8 and You must Enter only Numeric Values")
                            End If
                            Dim stremp_name As String = clsCommon.myCstr(grow.Cells(9).Value)
                            If clsCommon.myLen(stremp_name) > 50 Then
                                Throw New Exception("Employee Name can not be greater than 50")
                            End If
                            Dim strDepoetID As String = clsCommon.myCstr(grow.Cells(10).Value)
                            If clsCommon.myLen(strDepoetID) > 12 Then
                                Throw New Exception("Depot ID can not be grater than 12")
                            End If
                            Dim strprice_code As String = clsCommon.myCstr(grow.Cells(11).Value)
                            If clsCommon.myLen(strprice_code) > 12 Then
                                Throw New Exception("Price Code Can not be left Blank or size can not be grater than 12")
                            End If
                            Dim strprice_code_desc As String = clsCommon.myCstr(grow.Cells(12).Value)
                            If clsCommon.myLen(strprice_code_desc) > 100 Then
                                Throw New Exception("Price Code Description Can not be left Blank or size can not be grater than 100")
                            End If
                            Dim NOnPriceCode As String = clsCommon.myCstr(grow.Cells("Non Price Code").Value)

                            Dim RoutePriceCode As String = clsCommon.myCstr(grow.Cells("Route Price Code").Value)
                            Dim IsEarlyRoute As Decimal = clsCommon.myCdbl(grow.Cells("IsEarlyRoute(0/1)").Value)
                            If clsCommon.myLen(RoutePriceCode) > 0 Then
                                Dim ExistRoutepricecode = clsDBFuncationality.getSingleValue("select 1 from TSPL_ITEM_PRICE_MASTER where Price_Code='" & RoutePriceCode & "'", trans)
                                If clsCommon.myLen(ExistRoutepricecode) <= 0 Then
                                    Throw New Exception("Route Price Code does not exist")
                                End If
                            End If
                            Dim VCode As String = clsCommon.myCstr(grow.Cells("Vehicle Code").Value)
                            Dim StrStatus As String = IIf(clsCommon.myCstr(grow.Cells("Status").Value) = "", "A", clsCommon.myCstr(grow.Cells("Status").Value))
                            Dim strDate As String = clsCommon.GetPrintDate(IIf(clsCommon.myLen(grow.Cells("Status Date").Value.ToString) <= 0, clsCommon.GetPrintDate(Now(), "yyyy-MM-dd"), grow.Cells("Status Date").Value), "yyyy-MM-dd")
                            Dim strquery As String = "select count(*) from TSPL_Route_Master where Route_No='" + strRoute_no + "'"
                            Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))
                            If (i = 0) Then
                                connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_MASTER_INSERT", New SqlParameter("@Route_No", strRoute_no), New SqlParameter("@Route_Desc", strstrRoute_desc), New SqlParameter("@Type", strtype), New SqlParameter("@Employee_Code", stremp_code), New SqlParameter("@Off_Day", stroffday), New SqlParameter("@City_Code", strCitycode), New SqlParameter("@District", strDistrict), New SqlParameter("@Category_Code", strcat_code), New SqlParameter("@Length", strLength), New SqlParameter("@Employee_Name", stremp_name), New SqlParameter("@Depot_Id", ""), New SqlParameter("@Price_Code", strprice_code), New SqlParameter("@Price_Code_Desc", strprice_code_desc), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode), New SqlParameter("@Status", StrStatus), New SqlParameter("@SDate", strDate))
                                connectSql.RunSqlTransaction(trans, "update TSPL_ROUTE_MASTER set RoutePrice_Code = '" + RoutePriceCode + "',NonPrice_Code = '" + NOnPriceCode + "',vehicle_code='" + VCode + "', IsEarlyRoute ='" & IsEarlyRoute & "' where Route_No = '" + strRoute_no + "'")
                            Else
                                connectSql.RunSpTransaction(trans, "SP_TSPL_ROUTE_MASTER_UPDATE", New SqlParameter("@Route_No", strRoute_no), New SqlParameter("@Route_Desc", strstrRoute_desc), New SqlParameter("@Type", strtype), New SqlParameter("@Employee_Code", stremp_code), New SqlParameter("@Off_Day", stroffday), New SqlParameter("@City_Code", strCitycode), New SqlParameter("@District", strDistrict), New SqlParameter("@Category_Code", strcat_code), New SqlParameter("@Length", strLength), New SqlParameter("@Employee_Name", stremp_name), New SqlParameter("@Depot_Id", ""), New SqlParameter("@Price_Code", strprice_code), New SqlParameter("@Price_Code_Desc", strprice_code_desc), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode), New SqlParameter("@Status", StrStatus), New SqlParameter("@SDate", strDate))
                                connectSql.RunSqlTransaction(trans, "update TSPL_ROUTE_MASTER set RoutePrice_Code = '" + RoutePriceCode + "',NonPrice_Code = '" + NOnPriceCode + "',vehicle_code='" + VCode + "', IsEarlyRoute ='" & IsEarlyRoute & "' where Route_No = '" + strRoute_no + "'")
                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at row No:" + clsCommon.myCstr(ii) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Milk Route Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton102_Click(sender As Object, e As EventArgs) Handles RadButton102.Click
        Try
            Me.Text = "Price Compenent Master"
            Dim query As String = "Select price_comp_code as 'Price Component Code',Price_comp_desc as 'Price Component Description',Serial_Number as 'Serial Number',Price_Comp_account_code as 'Price Component Account Code',Gl_account_App as 'Gl Account App' ,tpt_type as 'TPT Type'   from tspl_price_component_master"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Compenent Master")
        End Try
    End Sub

    Private Sub RadButton101_Click(sender As Object, e As EventArgs) Handles RadButton101.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If transportSql.importExcel(dgv, "Price Component Code", "Price Component Description", "Serial Number", "Price Component Account Code", "Gl Account App", "TPT Type") Then
                Try
                    Dim LineNo As String
                    clsCommon.ProgressBarPercentShow()
                    Dim Arr As New List(Of clsPriceComponent)
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Importing  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        LineNo = clsCommon.myCstr(dgrv.Index + 2)
                        Dim obj As New clsPriceComponent()
                        obj.Price_Comp_code = clsCommon.myCstr(dgrv.Cells("Price Component Code").Value)
                        If clsCommon.myLen(obj.Price_Comp_code) > 0 Then
                            If clsCommon.myLen(obj.Price_Comp_code) > 12 Then
                                Throw New Exception("Line " + LineNo + " : Length of Price Component Code Should be less than 12.")
                            End If

                            obj.Price_Comp_Desc = clsCommon.myCstr(dgrv.Cells("Price Component Description").Value)
                            If clsCommon.myLen(obj.Price_Comp_Desc) > 100 Then
                                Throw New Exception("Line " + LineNo + " : Length of Price Component Description Should be less than 100.")
                            End If

                            obj.Serial_Number = clsCommon.myCdbl(dgrv.Cells("Serial Number").Value)
                            If obj.Serial_Number <= 0 Or obj.Serial_Number > 10 Then
                                Throw New Exception("Line " + LineNo + " : Enter Serial Number between 1 to 10.")
                            End If

                            obj.GL_Account_App = clsCommon.myCstr(dgrv.Cells("Gl Account App").Value)
                            If Not (clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal Or clsCommon.CompairString(obj.GL_Account_App, "F") = CompairStringResult.Equal) Then
                                Throw New Exception("Line " + LineNo + " : Enter 'Gl Account App' as 'T' OR 'F'.")
                            End If

                            If clsCommon.CompairString(obj.GL_Account_App, "T") = CompairStringResult.Equal Then
                                obj.Price_Comp_Account_Code = clsCommon.myCstr(dgrv.Cells("Price Component Account Code").Value)
                                obj.Price_Comp_Account_Code = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE Account_Code='" + obj.Price_Comp_Account_Code + "'")
                                If clsCommon.myLen(obj.Price_Comp_Account_Code) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Enter a valid 'Price Component Account Code'.")
                                End If
                            Else
                                obj.Price_Comp_Account_Code = ""
                            End If

                            obj.TPT_Type = clsCommon.myCstr(dgrv.Cells("TPT Type").Value)
                            If Not (clsCommon.CompairString(obj.TPT_Type, "Y") = CompairStringResult.Equal Or clsCommon.CompairString(obj.TPT_Type, "N") = CompairStringResult.Equal) Then
                                Throw New Exception("Line " + LineNo + " : Enter 'TPT Type' as 'Y' OR 'N'.")
                            End If
                            Arr.Add(obj)
                        End If
                    Next
                    If (clsPriceComponent.SaveData(Arr)) Then
                        clsCommon.ProgressBarPercentHide()
                        common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                    End If
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    myMessages.myExceptions(ex)
                End Try
            End If
            Me.Controls.Remove(dgv)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Compenent Master")
        Finally

        End Try
    End Sub

    Private Sub RadButton96_Click(sender As Object, e As EventArgs) Handles RadButton96.Click
        Try
            Me.Text = "Customer Vendor Mapping"
            qry = " SELECT Cust_Code AS [Customer Code],Vendor_Code  As [Vendor Code] FROM TSPL_CUSTOMER_VENDOR_MAPPING "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Vendor Mapping")
        End Try

    End Sub

    Private Sub RadButton95_Click(sender As Object, e As EventArgs) Handles RadButton95.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim CustMapEntry As Double = 0
            Dim VenMapEntry As Double = 0
            Dim DuplicateEntry As String = ""
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Customer Code", "Vendor Code") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = Nothing
                Try
                    connectSql.OpenConnection()
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        linno += 1
                        Dim Cust_Code As String = ""
                        Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                        If clsCommon.myLen(Cust_Code) > 0 Then
                            Dim CustQry As String = "select Count(*) As Row from TSPL_CUSTOMER_MASTER where Cust_Code ='" + Cust_Code + "'"
                            Dim checkCust As Integer = clsDBFuncationality.getSingleValue(CustQry, trans)
                            If checkCust <= 0 Then
                                Throw New Exception("Please check ! Customer Code (" & clsCommon.myCstr(Cust_Code) & ") does not exists in customer master at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        Else
                            Throw New Exception("Customer code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Cust_Code = Cust_Code

                        Dim Vendor_Code As String = ""
                        Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)

                        If clsCommon.myLen(Vendor_Code) > 0 Then
                            Dim CustQry As String = "select Count(*) As Row from TSPL_VENDOR_MASTER where Vendor_Code  ='" + Vendor_Code + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(CustQry, trans)
                            If check <= 0 Then
                                Throw New Exception("Please check ! Vendor Code (" & clsCommon.myCstr(Vendor_Code) & ") does not exists in vendor master at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        Else
                            Throw New Exception("Vendor code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Vendor_Code = Vendor_Code
                        ''
                        Dim NewEntry As Boolean
                        Dim NewCheck As Double = 0
                        Dim qry As String = ""
                        If clsCommon.myLen(Cust_Code) > 0 AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
                            NewCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS Row FROM TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & Cust_Code & "'", trans))
                            If NewCheck > 0 Then
                                NewEntry = False
                            Else
                                NewEntry = True
                            End If
                        End If
                        If NewEntry = True Then
                            qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + Cust_Code + "','" + Vendor_Code + "') "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        ElseIf NewEntry = False Then
                            qry = "update TSPL_CUSTOMER_VENDOR_MAPPING set vendor_code='" + Vendor_Code + "' where cust_code='" + Cust_Code + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                    Next
                    DuplicateEntry = "select Vendor_Code, SUM(1) as Repeated from TSPL_CUSTOMER_VENDOR_MAPPING group by Vendor_Code having SUM(1) > 1 "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Please check ! vendor (" & clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                    End If

                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at line no : " + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Customer Vendor Mapping")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton94_Click(sender As Object, e As EventArgs) Handles RadButton94.Click
        Try
            Me.Text = "CSA Price Master"
            Dim qry As String = "select TSPL_CSA_PRICE_HEAD.Doc_no as [Doc Code],convert(varchar,TSPL_CSA_PRICE_HEAD.Doc_Date,103) as Doc_Date,TSPL_CSA_PRICE_HEAD.[Description],TSPL_CSA_PRICE_HEAD.Tax as Included_Tax,TSPL_CSA_PRICE_HEAD.CSA_Rate as [Ceka Rate],case when coalesce(TSPL_CSA_PRICE_HEAD.For_Other_Item,0)=0 then 0 else 1 end as For_Other_Item,convert(varchar,TSPL_CSA_PRICE_HEAD.[Expiry_Date],103) as Expiry_On,TSPL_CSA_PRICE_DETAIL.Item_Code,TSPL_CSA_PRICE_DETAIL.CASE_UOM as [UnitCode],TSPL_CSA_PRICE_DETAIL.Diff_Rate as [CPD_Diff_Rate],TSPL_CSA_PRICE_CSA_DETAIL.Cust_Code,TSPL_CSA_PRICE_DETAIL.MRP,TSPL_CSA_PRICE_STATE_DETAIL.State_Code,TSPL_CSA_LOCATION_DETAIL.Location_Code from TSPL_CSA_PRICE_HEAD left outer join TSPL_CSA_PRICE_DETAIL on TSPL_CSA_PRICE_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_STATE_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "CSA Price Master")
        End Try
    End Sub

    Private Sub LoadDefaultDocinImport(ByVal gv1 As RadGridView)
        Try
            Dim repoLineNo As New GridViewTextBoxColumn()

            ''===============head=======================================
            repoLineNo = New GridViewTextBoxColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "Doc Code"
            repoLineNo.Name = "colImpDocCode"
            repoLineNo.Width = 50
            repoLineNo.WrapText = True
            repoLineNo.IsVisible = False
            repoLineNo.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoLineNo)

            Dim arr As New ArrayList()
            Dim Unique_Identity As String = ""
            Dim strDocCode As String = ""
            For Each grow As GridViewRowInfo In gv1.Rows
                Unique_Identity = clsCommon.myCstr(grow.Cells("Doc Code").Value) + "&" + clsCommon.myCstr(grow.Cells("Doc_Date").Value) + "&" + clsCommon.myCstr(grow.Cells("Included_Tax").Value) + "&" + clsCommon.myCstr(grow.Cells("Ceka Rate").Value) + "&" + clsCommon.myCstr(grow.Cells("For_Other_Item").Value)

                If clsCommon.myLen(Unique_Identity) > 0 AndAlso Not arr.Contains(Unique_Identity) Then
                    arr.Add(Unique_Identity)
                    If clsCommon.myLen(strDocCode) <= 0 Then
                        strDocCode = "CPDUP00000000001"
                    Else
                        strDocCode = clsCommon.incval(strDocCode)
                    End If
                End If
                grow.Cells("colImpDocCode").Value = strDocCode
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadButton93_Click(sender As Object, e As EventArgs) Handles RadButton93.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)

        Try
            Dim AllowOtherItems As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = 1, True, False)
            Dim ExciseEnableOnSalePatti As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableExciseONCSASalePatti, clsFixedParameterCode.EnableExciseONCSASalePatti, Nothing)) = 1, True, False)
            Dim arrDocNo As New ArrayList()
            Dim Doc_Date As DateTime? = Nothing
            Dim Description As String = Nothing
            Dim CerkaRate As Decimal = 0
            Dim For_Other_Item As String = Nothing
            Dim Expiry_On As DateTime? = Nothing
            Dim Item_Code As String = Nothing
            Dim UnitCode As String = Nothing
            Dim CPD_Diff_Rate As Decimal = 0
            Dim Cust_Code As String = Nothing
            Dim State_Code As String = Nothing
            Dim Location_Code As String = Nothing
            Dim qry As String = Nothing
            Dim check As Integer = 0
            Dim Is_MRP As Integer = 0
            Dim Doc_No As String = Nothing
            Dim coll As New Hashtable
            Dim CSA_UOM As String = Nothing
            Dim Revision_No As String = Nothing
            Dim Line_No As Integer = 0
            Dim ltr_per_case As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim stockingunit As String = ""
            Dim cnvrsn As Decimal = 1
            Dim case_cnvrsn As Decimal = 0
            Dim diffrate As Decimal = 0
            Dim org_rate As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_rate As Decimal = 0
            Dim StateCodeExist As String = Nothing
            Dim CustCodeExist As String = Nothing
            Dim LocationCodeExist As String = Nothing
            Dim IsDetailExist As String = Nothing
            Dim Wherecls As String = Nothing
            Dim MRP As Decimal = Nothing
            Dim doc_code As String = Nothing
            Dim IncludingTax As String = Nothing
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                If transportSql.importExcel(gv1, "Doc Code", "Doc_Date", "Description", "Included_Tax", "Ceka Rate", "For_Other_Item", "Expiry_On", "Item_Code", "UnitCode", "CPD_Diff_Rate", "Cust_Code", "MRP", "State_Code", "Location_Code") Then
                    ''do sorting of records for easy saving purpose.
                    Dim dt As New DataTable()
                    dt = gv1.DataSource()
                    dt.DefaultView.Sort = " [Doc Code],[Doc_Date],[Included_Tax],[Ceka Rate],[For_Other_Item] "
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()

                    gv1.DataSource = dt.DefaultView.ToTable()

                    LoadDefaultDocinImport(gv1)
                    ''======================end here========================

                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv1.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv1.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv1.Rows.Count & "")
                        doc_code = clsCommon.myCstr(grow.Cells("doc code").Value)
                        IncludingTax = clsCommon.myCstr(grow.Cells("included_tax").Value)

                        If clsCommon.myLen(IncludingTax) <= 0 Then
                            IncludingTax = "No"
                        End If
                        If clsCommon.myLen(IncludingTax) > 0 AndAlso clsCommon.CompairString(IncludingTax, "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(IncludingTax, "Yes") <> CompairStringResult.Equal Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Included_Tax should be Yes/No at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        For_Other_Item = clsCommon.myCstr(grow.Cells("For_Other_Item").Value)
                        CerkaRate = clsCommon.myCdbl(grow.Cells("Ceka Rate").Value)
                        Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If grow.Cells("Doc_Date").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Doc_Date").Value) > 0 AndAlso IsDate(grow.Cells("Doc_Date").Value) Then
                            Doc_Date = clsCommon.myCDate(grow.Cells("Doc_Date").Value)
                        Else
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If (clsCommon.myLen(For_Other_Item) <= 0) Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter For Other Item at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "1") <> CompairStringResult.Equal Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter For Other Item 1 or 0 only at line no. " + clsCommon.myCstr(grow.Index + 1))
                        End If

                        If clsCommon.myLen(For_Other_Item) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(CerkaRate) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCdbl(CerkaRate), 0) = CompairStringResult.Equal Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Ceka Rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(CerkaRate) > 0 Then
                            If (clsCommon.CompairString(CerkaRate, "0")) = CompairStringResult.Equal Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Ceka rate can not be 0 at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        End If
                        If (clsCommon.myLen(grow.Cells("Expiry_On").Value) > 0) Then
                            If IsDate(grow.Cells("Expiry_On").Value) Then
                                Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                            ElseIf AllowOtherItems Then ''when setting is ON for other item then expiry is mandatory
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Enter Valid Expiry Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            ElseIf Not AllowOtherItems AndAlso Not IsDate(grow.Cells("Expiry_On").Value) Then
                                Expiry_On = Nothing
                            End If
                        End If

                        If AllowOtherItems = True Then
                            If clsCommon.myLen(grow.Cells("Expiry_On").Value) <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Enter Expire Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            Else
                                Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                            End If
                        End If

                        If clsCommon.myLen(grow.Cells("Expiry_On").Value) > 0 Then
                            If clsCommon.myCDate(grow.Cells("Expiry_On").Value) <= clsCommon.myCDate(grow.Cells("Doc_Date").Value) Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Expiry Date must be greater than Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            Else
                                Expiry_On = clsCommon.myCDate(grow.Cells("Expiry_On").Value)
                            End If
                        End If

                        Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                        If clsCommon.myLen(Item_Code) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Item Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.myLen(Item_Code) > 0 Then
                            qry = "select count(*) from TSPL_ITEM_MASTER where Item_Code='" + Item_Code + "' and Item_Type in ('F','T') and isnull(Is_FreshItem,0)<>1 "
                            If clsCommon.myCstr(For_Other_Item) = "1" Then
                                qry += " and CSA_Type<>'CPD-DESI GHEE'"
                            Else
                                qry += " and CSA_Type='CPD-DESI GHEE'"
                            End If
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Filled Item Code " + Item_Code + "is not valid or not finished, Trading and CPD Type,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        End If

                        UnitCode = clsCommon.myCstr(grow.Cells("UnitCode").Value)

                        If clsCommon.myLen(UnitCode) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Unit Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If


                        If clsCommon.myLen(UnitCode) > 0 Then
                            qry = Nothing
                            qry = "select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code='" + Item_Code + "' and UOM_Code='" + UnitCode + "' "
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Filled Unit Code is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        End If

                        CPD_Diff_Rate = clsCommon.myCdbl(grow.Cells("CPD_Diff_Rate").Value)
                        If clsCommon.myLen(CPD_Diff_Rate) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter CPD Diff. rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.myLen(CPD_Diff_Rate) > 0 AndAlso Not IsNumeric(CPD_Diff_Rate) Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("CPD Diff. rate should be numeric at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        'If clsCommon.myLen(CPD_Diff_Rate) > 0 Then
                        '    If (clsCommon.CompairString(CPD_Diff_Rate, "0")) = CompairStringResult.Equal Then
                        '        clsCommon.ProgressBarPercentHide()
                        '        Throw New Exception("CPD Diff. rate can not be 0 at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        '    End If
                        'End If


                        Cust_Code = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                        If clsCommon.myLen(Cust_Code) > 0 Then
                            qry = Nothing
                            qry = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Cust_Code + "' and CSA_Type='Y' "
                            check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Filled Customer Code " + Cust_Code + "is not valid. First create Customer, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                            End If
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(Cust_Code) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Cust Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        qry = Nothing
                        qry = "select Is_Mrp from TSPL_Item_Master where Item_Code='" + Item_Code + "'  "
                        Is_MRP = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.CompairString(clsCommon.myCstr(Is_MRP), 1) = CompairStringResult.Equal Then
                            MRP = clsCommon.myCdbl(grow.Cells("MRP").Value)
                        End If

                        If ExciseEnableOnSalePatti = True AndAlso clsCommon.CompairString(clsCommon.myCstr(Is_MRP), 1) = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(MRP) <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Enter MRP at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            End If
                        End If



                        State_Code = clsCommon.myCstr(grow.Cells("State_Code").Value)
                        If clsCommon.myLen(State_Code) > 0 Then
                            qry = Nothing
                            qry = "select count(*) from tspl_STate_Master where STATE_CODE='" + State_Code + "'  "
                            check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Filled State Code " + State_Code + "is not valid, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                            End If
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(For_Other_Item), "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(State_Code) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter State Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        Location_Code = clsCommon.myCstr(grow.Cells("Location_Code").Value)
                        If clsCommon.myLen(Location_Code) <= 0 Then
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Enter Location Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If

                        If clsCommon.myLen(Location_Code) > 0 Then
                            qry = Nothing
                            qry = "select count(*) from TSPL_LOCATION_MASTER where Location_Code='" + Location_Code + "'  "
                            check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If check <= 0 Then
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception("Filled Location Code " + Location_Code + "is not valid, see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                            End If
                        End If


                        'DONE BY STUTI ON 08/12/2016 FOR DATE

                        If Not arrDocNo.Contains(clsCommon.myCstr(grow.Cells("colImpDocCode").Value)) Then
                            arrDocNo.Add(clsCommon.myCstr(grow.Cells("colImpDocCode").Value))

                            qry = "select TSPL_CSA_PRICE_HEAD.doc_no from TSPL_CSA_PRICE_HEAD " & _
                        " left outer join TSPL_CSA_PRICE_STATE_DETAIL on TSPL_CSA_PRICE_HEAD.Doc_No=TSPL_CSA_PRICE_STATE_DETAIL.Doc_No left outer join TSPL_CSA_LOCATION_DETAIL on TSPL_CSA_LOCATION_DETAIL.Doc_No=TSPL_CSA_PRICE_HEAD.Doc_No " & _
                        " left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                            qry += " where TSPL_CSA_PRICE_HEAD.doc_no <>'" + doc_code + "' and TSPL_CSA_PRICE_HEAD.tax='" + IncludingTax + "' "
                            If AllowOtherItems AndAlso For_Other_Item > 0 Then
                                qry += " and TSPL_CSA_PRICE_HEAD.For_Other_Item=1 and TSPL_CSA_PRICE_CSA_DETAIL.cust_code in ('" + Cust_Code + "') "
                            Else
                                qry += " and TSPL_CSA_PRICE_STATE_DETAIL.State_Code in ('" + State_Code + "')  and TSPL_CSA_PRICE_HEAD.csa_type='CPD-DESI GHEE' and isnull(TSPL_CSA_PRICE_HEAD.For_Other_Item,0) =0 "
                            End If

                            qry += " and TSPL_CSA_LOCATION_DETAIL.Location_Code in ('" + Location_Code + "')  AND convert(date,TSPL_CSA_PRICE_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy") + "'"

                            Dim check1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(check1) > 0 Then
                                If AllowOtherItems AndAlso For_Other_Item > 0 Then
                                    Throw New Exception("Selected Customer already mapped ,for more change edit old record (" + clsCommon.myCstr(check1) + ") dated (" + clsCommon.GetPrintDate(Doc_Date, "dd/MM/yyyy") + ").")
                                Else
                                    Throw New Exception("Selected locations already mapped with selected states,for more change edit old record (" + clsCommon.myCstr(check1) + ") dated (" + clsCommon.GetPrintDate(Doc_Date, "dd/MM/yyyy") + ").")
                                End If
                            End If
                        End If
                        '=================END HERE=============


                        'Insert TSPL_CSA_PRICE_HEAD
                        CSA_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'", trans))


                        Dim Doc_NoExist As String = ""
                        If clsCommon.myLen(doc_code) > 0 Then
                            qry = "select Doc_No from tspl_CSA_Price_Head where doc_no='" + doc_code + "' and For_Other_Item='" + For_Other_Item + "'"
                            Doc_NoExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        Else
                            qry = "select max(Doc_No) from tspl_CSA_Price_Head where Doc_Date='" + clsCommon.GetPrintDate(Doc_Date) + "' and For_Other_Item='" + For_Other_Item + "' "
                            Doc_NoExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If


                        If clsCommon.CompairString(Doc_NoExist, "") = CompairStringResult.Equal Then
                            Doc_No = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.CSAPRICEMAASTER, "", ""))
                        Else
                            Doc_No = Doc_NoExist
                            qry = Nothing
                            qry = "select Revision_No from TSPL_CSA_PRICE_head where doc_no='" + Doc_No + "'"
                            Revision_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(Revision_No) > 0 Then
                                Revision_No = clsCommon.incval(Revision_No)
                            ElseIf clsCommon.myLen(Revision_No) <= 0 Then
                                If Cust_Code IsNot Nothing AndAlso clsCommon.myLen(Revision_No) > 0 Then
                                    If clsCommon.myLen(Cust_Code) > 3 Then
                                        Revision_No = clsCommon.myCstr(Cust_Code).Substring(0, 3) + "0000000001"
                                    Else
                                        Revision_No = clsCommon.myCstr(Cust_Code) + "0000000001"
                                    End If
                                ElseIf Location_Code IsNot Nothing AndAlso clsCommon.myLen(Location_Code) > 0 Then
                                    Revision_No = ""
                                    If clsCommon.myLen(State_Code) > 3 Then
                                        Revision_No += clsCommon.myCstr(State_Code).Substring(0, 3)
                                    Else
                                        Revision_No += clsCommon.myCstr(State_Code)
                                    End If
                                    If clsCommon.myLen(Location_Code) > 3 Then
                                        Revision_No += clsCommon.myCstr(Location_Code).Substring(0, 3)
                                    Else
                                        Revision_No += clsCommon.myCstr(Location_Code)
                                    End If

                                    Revision_No += "0000000001"

                                End If
                            End If
                        End If
                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)
                        clsCommon.AddColumnsForChange(coll, "Description", Description)
                        clsCommon.AddColumnsForChange(coll, "CSA_Type", "CPD-DESI GHEE")
                        clsCommon.AddColumnsForChange(coll, "CSA_UOM", CSA_UOM)
                        clsCommon.AddColumnsForChange(coll, "CSA_Rate", CerkaRate)
                        clsCommon.AddColumnsForChange(coll, "TAX", IncludingTax)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                        clsCommon.AddColumnsForChange(coll, "For_Other_Item", For_Other_Item)
                        clsCommon.AddColumnsForChange(coll, "Revision_No", Revision_No)

                        If Expiry_On IsNot Nothing AndAlso clsCommon.myLen(Expiry_On) > 0 AndAlso IsDate(Expiry_On) Then
                            clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(Expiry_On, "dd/MMM/yyyy"))
                        End If

                        If Doc_Date IsNot Nothing AndAlso clsCommon.myLen(Doc_Date) > 0 AndAlso IsDate(Doc_Date) Then
                            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(Doc_Date, "dd/MMM/yyyy"))
                        End If
                        If clsCommon.CompairString(Doc_NoExist, "") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                        Else

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_HEAD", OMInsertOrUpdate.Update, "Doc_No='" + Doc_No + "'", trans)
                        End If



                        'End of Insert TSPL_CSA_PRICE_HEAD

                        'Insert TSPL_CSA_PRICE_Detail

                        coll = New Hashtable()

                        Line_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull((max(isnull(Line_No,1))+1),1) from TSPL_CSA_PRICE_DETAIL where Doc_No='" + Doc_No + "'", trans))

                        clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)

                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll, "UOM", CSA_UOM)
                        clsCommon.AddColumnsForChange(coll, "Diff_Rate", CPD_Diff_Rate)
                        If For_Other_Item = "0" Then
                            Dim uom_ltr As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and UOM_Code='" + CSA_UOM + "'", trans))
                            Dim pcs_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and stocking_unit='Y'", trans))
                            Dim case_uom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and UOM_Code = '" + CSA_UOM + "'", trans))

                            If uom_ltr <= 0 Then
                                uom_ltr = 1
                            End If
                            If pcs_uom <= 0 Then
                                pcs_uom = 1
                            End If
                            If case_uom <= 0 Then
                                ltr_per_case = 0
                                pcs_per_case = 0
                            Else
                                ltr_per_case = System.Math.Round(case_uom / uom_ltr, 2)
                                pcs_per_case = System.Math.Round(case_uom / pcs_uom, 2)
                            End If
                            qry = Nothing
                            qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and uom_code='" + UnitCode + "'"
                            cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If cnvrsn <= 0 Then
                                cnvrsn = 1
                            End If

                            stockingunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and stocking_unit='Y'", trans))
                            case_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(Item_Code) + "' and uom_code='" + UnitCode + "'", trans))

                            diffrate = CPD_Diff_Rate
                            org_rate = (clsCommon.myCdbl(CerkaRate) + clsCommon.myCdbl(diffrate))

                            '========case rate====            





                            case_rate = System.Math.Round((clsCommon.myCdbl(org_rate) * case_cnvrsn) / cnvrsn, 2)

                            '====pcs rate=================           

                            If clsCommon.myCdbl(case_cnvrsn) > 0 Then
                                pcs_rate = System.Math.Round((clsCommon.myCdbl(case_rate) / clsCommon.myCdbl(case_cnvrsn)), 2)
                            Else
                                pcs_rate = 0

                            End If
                        End If

                        clsCommon.AddColumnsForChange(coll, "Ltr_Rate", org_rate)
                        clsCommon.AddColumnsForChange(coll, "Case_Rate", case_rate)
                        clsCommon.AddColumnsForChange(coll, "Pcs_Rate", pcs_rate)
                        clsCommon.AddColumnsForChange(coll, "Ltr_Per_Case", ltr_per_case)
                        clsCommon.AddColumnsForChange(coll, "Pcs_Per_Case", pcs_per_case)
                        clsCommon.AddColumnsForChange(coll, "CASE_UOM", UnitCode, True)
                        clsCommon.AddColumnsForChange(coll, "MRP", MRP)

                        IsDetailExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans))
                        If IsDetailExist = "0" Then
                            clsCommon.AddColumnsForChange(coll, "Line_no", Line_No)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_DETAIL", OMInsertOrUpdate.Update, "Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans)
                        End If
                        'End of Insert TSPL_CSA_PRICE_Detail

                        'Insert TSPL_CSA_Location_Detail
                        LocationCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CSA_LOCATION_DETAIL where Doc_No='" + Doc_No + "' and Location_Code='" + Location_Code + "'", trans))
                        If LocationCodeExist = "0" Then
                            coll = New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                            clsCommon.AddColumnsForChange(coll, "Location_Code", Location_Code)

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_LOCATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        End If
                        'End of Insert TSPL_CSA_Location_Detail

                        'Insert TSPL_CSA_PRICE_STATE_DETAIL
                        If For_Other_Item = "0" Then
                            StateCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_STATE_DETAIL where Doc_No='" + Doc_No + "' and State_Code='" + State_Code + "'", trans))
                            If StateCodeExist = "0" Then
                                coll = New Hashtable()

                                clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                                clsCommon.AddColumnsForChange(coll, "State_Code", State_Code, True)

                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_STATE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                'End of Insert TSPL_CSA_PRICE_STATE_DETAIL
                            End If
                        End If

                        'Insert TSPL_CSA_PRICE_CSA_DETAIL
                        If For_Other_Item = "1" Then
                            CustCodeExist = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_PRICE_CSA_DETAIL where Doc_No='" + Doc_No + "' and Cust_Code='" + Cust_Code + "'", trans))
                            If CustCodeExist = "0" Then
                                coll = New Hashtable()

                                clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                                clsCommon.AddColumnsForChange(coll, "Cust_Code", Cust_Code)

                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_CSA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                            End If
                        End If
                        'End Insert TSPL_CSA_PRICE_CSA_DETAIL

                        'Insert in TSPL_CSA_PRICE_OTHER_ITEM_DETAIL
                        If For_Other_Item = "1" Then

                            Dim UnitRate As Decimal = CPD_Diff_Rate
                            Dim MRPAmt As Decimal = MRP
                            Dim whrcls As String = ""

                            qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as [Code],tspl_unit_master.unit_desc as [Unit],round(cast((" + clsCommon.myCstr(UnitRate) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [Unit Rate],round(cast((" + clsCommon.myCstr(MRPAmt) + " * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / (case when isnull(BaseUOm.Conversion_Factor,0)=0 then 1 else BaseUOm.Conversion_Factor end) as float),2) as [MRP] " & _
                                      " from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code left outer join TSPL_ITEM_UOM_DETAIL as BaseUOm on BaseUOm.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and BaseUOm.UOM_Code='" + clsCommon.myCstr(CSA_UOM) + "' "
                            whrcls = " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(Item_Code) + "' "
                            dt = New DataTable()

                            '  If OpenDialogBox Then
                            'Dim strcode As String = ""
                            'strcode = clsCommon.myCstr(clsCommon.ShowSelectForm("OTHRITMPRC", qry, "Code", whrcls, strcode, "", True)) ''for show on double click
                            ' End If

                            ''===for fill in grid
                            qry += " where " + whrcls
                            dt = clsDBFuncationality.GetDataTable(qry, trans)

                            Dim ArrItemExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Count(*) from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans))
                            If ArrItemExist <> "0" Then
                                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CSA_PRICE_OTHER_ITEM_DETAIL where Doc_No='" + Doc_No + "' and Item_Code='" + Item_Code + "'", trans)
                            End If
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each dr As DataRow In dt.Rows
                                    coll = New Hashtable()
                                    Dim uom As String = clsCommon.myCstr(dr("code"))
                                    Dim diffrateImp As String = System.Math.Round(clsCommon.myCdbl(dr("unit rate")), 2)
                                    Dim MRP_Val As String = System.Math.Round(clsCommon.myCdbl(dr("MRP")), 2)
                                    clsCommon.AddColumnsForChange(coll, "doc_no", Doc_No)
                                    clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                                    clsCommon.AddColumnsForChange(coll, "UOM", uom)
                                    clsCommon.AddColumnsForChange(coll, "Unit_Rate", diffrateImp)
                                    clsCommon.AddColumnsForChange(coll, "MRP", MRP_Val)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                Next
                            End If
                        End If
                        'End of Insert TSPL_CSA_PRICE_OTHER_ITEM_DETAIL 
                    Next

                    ''============history======================
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_HEAD", "doc_no", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_DETAIL", "doc_no", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_LOCATION_DETAIL", "doc_no", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_STATE_DETAIL", "doc_no", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_OTHER_ITEM_DETAIL", "doc_no", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CSA_PRICE_CSA_DETAIL", "doc_no", trans)
                    ''========================================================

                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "CSA Price Master")
        Finally
            Me.Controls.Remove(gv1)
        End Try
    End Sub

    Private Sub RadButton92_Click(sender As Object, e As EventArgs) Handles RadButton92.Click
        Try
            Me.Text = "Price Chart Master Bulk"
            Dim IsItemMilkType As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
            Dim IsPriceChartGradeWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
            Dim SettBulkProcurementApplyTotalSoidRate As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementApplyTotalSoidRate, clsFixedParameterCode.BulkProcurementApplyTotalSoidRate, Nothing)) > 0)
            Dim AllowCreateBulkProcPriceChartItemWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
            Dim str As String
            Dim Qry As String = ""
            If IsItemMilkType = 1 And AllowCreateBulkProcPriceChartItemWise = 1 Then
                'str = "select Price_Code as [Price Code],Price_Date As [Price Date] ,Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Percentage as[Fat Percentage],Snf_Percentage as [SNF Percentage],Standard_Rate as [Standard Rate],vendor_code as [Vendor Code],Tolerance,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch],effective_Date as [effective Date],Milk_Type_Code as [Milk Type Code]  from TSPL_Bulk_Price_MASTER"
                str = "select Price_Code as [Price Code],convert (varchar,Price_Date,103) As [Price Date] ,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch],effective_Date as [effective Date],Milk_Type_Code as [Milk Type Code]  from TSPL_Bulk_Price_MASTER"
            ElseIf IsItemMilkType = 1 And IsPriceChartGradeWise = 1 Then
                str = "select Price_Code as [Price Code],convert (varchar,Price_Date,103) As [Price Date] ,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch] ,effective_Date as [effective Date],Milk_Type_Code as [Milk Type Code] from TSPL_Bulk_Price_MASTER"
            Else
                str = "select Price_Code as [Price Code],convert (varchar,Price_Date,103) As [Price Date] ,Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Percentage as[Fat Percentage],Snf_Percentage as [SNF Percentage],Standard_Rate as [Standard Rate],vendor_code as [Vendor Code],Tolerance,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch] ,TSPL_Bulk_Price_MASTER.Total_Solid_Rate as [Total Solid Rate] , TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code  as [Total Solid Unit Code]  from TSPL_Bulk_Price_MASTER"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Chart Master")
        End Try
    End Sub

    Private Sub RadButton91_Click(sender As Object, e As EventArgs) Handles RadButton91.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing
        Try
            Dim IsItemMilkType As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
            Dim IsPriceChartGradeWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
            Dim SettBulkProcurementApplyTotalSoidRate As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementApplyTotalSoidRate, clsFixedParameterCode.BulkProcurementApplyTotalSoidRate, Nothing)) > 0)
            Dim StandardRateWithZero As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing)) = 1, True, False)
            Dim AllowCreateBulkProcPriceChartItemWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
            Dim IsNewEntry As Boolean
            If IsItemMilkType = 1 And AllowCreateBulkProcPriceChartItemWise = 1 Then
                If transportSql.importExcel(gv, "Price Code", "Price Date", "Is Default For Tanker Dispatch", "effective Date", "Milk Type Code") Then
                    'If transportSql.importExcel(gv, "Price Code", "Price Date", "Fat Weightage", "SNF Weightage", "Fat Percentage", "SNF Percentage", "Standard Rate", "Vendor Code", "Tolerance", "Is Default For Tanker Dispatch", "effective Date", "Milk Type Code") Then
                    Dim linno As Integer = 0
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        For Each grow As GridViewRowInfo In gv.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clsPriceChartBulkProc
                            Dim effective_Date As String = Nothing
                            Dim strPriceDate As String
                            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                            If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                                strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                            Else
                                strPriceDate = ""
                            End If
                            Dim DblFatWeightage As Double = 0 'clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                            Dim DblFatPercentage As Double = 0 'clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                            Dim DblSnfWeightage As Double = 0 'clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                            Dim DblSnfPercentage As Double = 0 'clsCommon.myCdbl(grow.Cells("SNF Percentage").Value)
                            Dim DblStandardRate As Double = 0 ' clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                            Dim DblTolerance As Double = 0 'clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                            Dim vendorCode As String = ""  'clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            Dim IsDefaultForTankerDispatch As Integer = 0
                            If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                            ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = 1
                            Else
                                IsDefaultForTankerDispatch = 0
                            End If
                            If clsCommon.myLen(grow.Cells("effective Date").Value) > 0 AndAlso IsDate(grow.Cells("effective Date").Value) Then
                                effective_Date = clsCommon.myCDate(grow.Cells("effective Date").Value)
                            Else
                                effective_Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
                            End If
                            obj.effective_Date = effective_Date
                            Dim Milk_Type_Code As String = clsCommon.myCstr(grow.Cells("Milk Type Code").Value)
                            If clsCommon.myLen(Milk_Type_Code) <= 0 Then
                                Throw New Exception("Milk type code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(Milk_Type_Code) <= 0 Then
                                Dim isValidMilkType As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("   select Count (*) from TSPL_MILK_TYPE_MASTER where MILK_TYPE_CODE = '" + Milk_Type_Code + "' ", trans))
                                If isValidMilkType = False Then
                                    Throw New Exception("Invalid Milk type code  At Line No. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If
                            obj.Milk_Type_Code = Milk_Type_Code
                            If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                                Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Code = strPriceCode

                            If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                                Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Date = strPriceDate
                            'If clsCommon.myCdbl(DblFatWeightage) <= 0 Then
                            '    Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.Fat_Weightage = DblFatWeightage
                            'If clsCommon.myCdbl(DblSnfWeightage) <= 0 Then
                            '    Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.Snf_Weightage = DblSnfWeightage

                            'If clsCommon.myCdbl(DblFatPercentage) <= 0 Then
                            '    Throw New Exception("Fat Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.Fat_Percentage = DblFatPercentage

                            'If clsCommon.myCdbl(DblSnfPercentage) <= 0 Then
                            '    Throw New Exception("SNF Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.Snf_Percentage = DblSnfPercentage

                            'If clsCommon.myCdbl(DblStandardRate) <= 0 Then
                            '    Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            'End If
                            obj.Standard_Rate = DblStandardRate

                            If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                                IsNewEntry = False
                            Else
                                IsNewEntry = True
                            End If
                            obj.vendor_code = vendorCode
                            obj.Tolerance = clsCommon.myCdbl(DblTolerance)
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                            clsCommon.AddColumnsForChange(coll, "tolerance", obj.Tolerance)
                            clsCommon.AddColumnsForChange(coll, "Milk_Type_Code", obj.Milk_Type_Code)
                            clsCommon.AddColumnsForChange(coll, "effective_Date", clsCommon.GetPrintDate(obj.effective_Date, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "IsPrice_ItemWise", "1")

                            'If clsCommon.myLen(obj.vendor_code) > 0 Then
                            '    obj.vendor_desc = clsVendorMaster.GetName(obj.vendor_code, trans)
                            'Else
                            '    obj.vendor_desc = ""
                            'End If
                            ' clsCommon.AddColumnsForChange(coll, "Vendor_desc", obj.vendor_desc)
                            clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            If IsNewEntry Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                            End If
                            If IsDefaultForTankerDispatch = 1 Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                            End If
                            linno += 1
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at row no" + clsCommon.myCstr(linno) + ex.Message)
                    End Try
                End If
            ElseIf IsItemMilkType = 1 And IsPriceChartGradeWise = 1 Then
                If transportSql.importExcel(gv, "Price Code", "Price Date", "Is Default For Tanker Dispatch", "effective Date", "Milk Type Code") Then
                    Dim linno As Integer = 0
                    Try
                        trans = clsDBFuncationality.GetTransactin()
                        For Each grow As GridViewRowInfo In gv.Rows
                            linno += 1
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clsPriceChartBulkProc
                            Dim strPriceDate As String
                            Dim effective_Date As String = Nothing
                            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                            Dim DblFatWeightage As Double = 0 'clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                            Dim DblFatPercentage As Double = 0 'clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                            Dim DblSnfWeightage As Double = 0 'clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                            Dim DblSnfPercentage As Double = 0 'clsCommon.myCdbl(grow.Cells("SNF Percentage").Value)
                            Dim DblStandardRate As Double = 0 ' clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                            Dim DblTolerance As Double = 0 'clsCommon.myCdbl(grow.Cells("Tolerance").Value)


                            If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                                strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                            Else
                                strPriceDate = ""
                            End If
                            Dim IsDefaultForTankerDispatch As Integer = 0
                            If clsCommon.myLen(grow.Cells("effective Date").Value) > 0 AndAlso IsDate(grow.Cells("effective Date").Value) Then
                                effective_Date = clsCommon.myCDate(grow.Cells("effective Date").Value)
                            Else
                                effective_Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
                            End If
                            obj.effective_Date = effective_Date
                            Dim Milk_Type_Code As String = clsCommon.myCstr(grow.Cells("Milk Type Code").Value)
                            obj.Milk_Type_Code = Milk_Type_Code
                            If clsCommon.myLen(Milk_Type_Code) <= 0 Then
                                Throw New Exception("Milk type code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(Milk_Type_Code) <= 0 Then
                                Dim isValidMilkType As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("   select Count (*) from TSPL_MILK_TYPE_MASTER where MILK_TYPE_CODE = '" + Milk_Type_Code + "' ", trans))
                                If isValidMilkType = False Then
                                    Throw New Exception("Invalid Milk type code  At Line No. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If

                            If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                            ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = 1
                            Else
                                IsDefaultForTankerDispatch = 0
                            End If
                            If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                                Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Code = strPriceCode

                            If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                                Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Date = strPriceDate

                            '========================================

                            obj.Fat_Weightage = DblFatWeightage
                            obj.Snf_Weightage = DblSnfWeightage
                            obj.Fat_Percentage = DblFatPercentage
                            obj.Snf_Percentage = DblSnfPercentage
                            obj.Standard_Rate = DblStandardRate
                            '==========================================

                            If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                                IsNewEntry = False
                            Else
                                IsNewEntry = True
                            End If
                            obj.Tolerance = clsCommon.myCdbl(DblTolerance)
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))

                            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                            clsCommon.AddColumnsForChange(coll, "tolerance", obj.Tolerance)

                            clsCommon.AddColumnsForChange(coll, "effective_Date", clsCommon.GetPrintDate(obj.effective_Date, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Milk_Type_Code", obj.Milk_Type_Code, True)
                            clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "IsPrice_GradeWise", "1")
                            If IsNewEntry Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                            End If
                            If IsDefaultForTankerDispatch = 1 Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                            End If
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at row no" + clsCommon.myCstr(linno) + ex.Message)
                    End Try
                End If
            Else
                If transportSql.importExcel(gv, "Price Code", "Price Date", "Fat Weightage", "SNF Weightage", "Fat Percentage", "SNF Percentage", "Standard Rate", "Vendor Code", "Tolerance", "Is Default For Tanker Dispatch", "Total Solid Rate", "Total Solid Unit Code") Then
                    Dim linno As Integer = 0
                    Try
                        trans = clsDBFuncationality.GetTransactin()
                        connectSql.OpenConnection()
                        For Each grow As GridViewRowInfo In gv.Rows
                            linno += 1
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clsPriceChartBulkProc
                            Dim strPriceDate As String
                            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                            If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                                strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                            Else
                                strPriceDate = ""
                            End If
                            Dim DblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                            Dim DblFatPercentage As Double = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                            Dim DblSnfWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                            Dim DblSnfPercentage As Double = clsCommon.myCdbl(grow.Cells("SNF Percentage").Value)
                            Dim DblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                            Dim DblTolerance As Double = clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                            Dim vendorCode As String = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            Dim DblTotalSolidRate As Double = clsCommon.myCdbl(grow.Cells("Total Solid Rate").Value)
                            Dim totalSolidUnitCode As String = clsCommon.myCstr(grow.Cells("Total Solid Unit Code").Value)

                            Dim IsDefaultForTankerDispatch As Integer = 0
                            If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                            ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                                IsDefaultForTankerDispatch = 1
                            Else
                                IsDefaultForTankerDispatch = 0
                            End If

                            If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                                Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Code = strPriceCode
                            If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                                Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Price_Date = strPriceDate
                            If clsCommon.myCdbl(DblFatWeightage) <= 0 Then
                                Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Fat_Weightage = DblFatWeightage
                            If clsCommon.myCdbl(DblSnfWeightage) <= 0 AndAlso SettBulkProcurementApplyTotalSoidRate = False Then
                                Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Snf_Weightage = DblSnfWeightage

                            If clsCommon.myCdbl(DblFatPercentage) <= 0 AndAlso SettBulkProcurementApplyTotalSoidRate = False Then
                                Throw New Exception("Fat Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Fat_Percentage = DblFatPercentage

                            If clsCommon.myCdbl(DblSnfPercentage) <= 0 AndAlso SettBulkProcurementApplyTotalSoidRate = False Then
                                Throw New Exception("SNF Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Snf_Percentage = DblSnfPercentage

                            If clsCommon.myCdbl(DblStandardRate) <= 0 AndAlso StandardRateWithZero = False AndAlso SettBulkProcurementApplyTotalSoidRate = False Then
                                Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Standard_Rate = DblStandardRate

                            If clsCommon.myCdbl(DblTotalSolidRate) <= 0 AndAlso SettBulkProcurementApplyTotalSoidRate = True Then
                                Throw New Exception("Total Solid Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(totalSolidUnitCode) <= 0 AndAlso SettBulkProcurementApplyTotalSoidRate = True Then
                                Throw New Exception("Total Solid Unit code should not be left blank  At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(totalSolidUnitCode) > 0 AndAlso SettBulkProcurementApplyTotalSoidRate = True Then
                                Dim isValidUnitCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_UNIT_MASTER where Unit_Code = '" + totalSolidUnitCode + "'", trans))
                                If isValidUnitCode = False Then
                                    Throw New Exception("Invalid Total Solid Unit code At Line No. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If
                            If SettBulkProcurementApplyTotalSoidRate = True Then
                                obj.Total_Solid_Rate = DblTotalSolidRate
                                obj.Total_Solid_Unit_Code = totalSolidUnitCode
                            End If
                            If clsCommon.myLen(vendorCode) > 0 Then
                                Dim chkVendor_Code As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   Select count (*) from TSPL_VENDOR_MASTER where Vendor_Code = '" + vendorCode + "'", trans))
                                If chkVendor_Code = False Then
                                    Throw New Exception("Invalid Vendor Code At Line No. " + clsCommon.myCstr(linno) + ".")
                                End If
                            End If
                            obj.vendor_code = vendorCode

                            obj.Tolerance = clsCommon.myCdbl(DblTolerance)

                            If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                                IsNewEntry = False
                            Else
                                IsNewEntry = True
                            End If


                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                            clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                            clsCommon.AddColumnsForChange(coll, "tolerance", obj.Tolerance)
                            clsCommon.AddColumnsForChange(coll, "Vendor_code", obj.vendor_code)
                            If SettBulkProcurementApplyTotalSoidRate = True Then
                                clsCommon.AddColumnsForChange(coll, "Total_Solid_Rate", obj.Total_Solid_Rate)
                                clsCommon.AddColumnsForChange(coll, "Total_Solid_Unit_Code", obj.Total_Solid_Unit_Code)
                            End If
                            If clsCommon.myLen(obj.vendor_code) > 0 Then
                                obj.vendor_desc = clsVendorMaster.GetName(obj.vendor_code, trans)
                            Else
                                obj.vendor_desc = ""
                            End If
                            clsCommon.AddColumnsForChange(coll, "Vendor_desc", obj.vendor_desc)
                            clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            If IsNewEntry Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                            End If
                            If IsDefaultForTankerDispatch = 1 Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                            End If
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at row no" + clsCommon.myCstr(linno) + ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Chart Master Bulk")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton90_Click(sender As Object, e As EventArgs) Handles RadButton90.Click
        Try
            Me.Text = "Parameter Master"
            Dim qry As String = "select count(*) from tspl_parameter_master"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            Dim WHRCLS As String = ""
            If check <= 0 Then
                qry = "select '' as Code,'' as Description,'' as Type,'' as Nature,'' as IsMandatory,'' as [Applicable For],'' as [For Bulk Sale],'' as [For Milk Grade],'' as [Parameter Type] "
            Else
                qry = "select Code,Description,(case when Type='CF' then 'CORRECTION FACTOR' else type end) as Type,(case when Nature='A' then 'Alphanumeric' else case" _
                    & " when nature='B' then 'Boolean' else 'Range' end end) as Nature,Case when IsMandatory=0 then 'No' else 'Yes' end as IsMandatory,Param_for as [Applicable For]" _
                    & " ,case when coalesce(IsBulkSale,0)=1 then 'True' else 'False' end as [For Bulk Sale] ,case when coalesce(IsForMilkGrade,0)=1 then 'True' else 'False' end as [For Milk Grade],tspl_parameter_master.parametertype as [Parameter Type] from tspl_parameter_master"
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") <> CompairStringResult.Equal Then
                    WHRCLS += " AND Type not in ('ABB','AAB') "
                End If
            End If
            transportSql.ExporttoExcel(qry, WHRCLS, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Parameter Master")
        End Try
    End Sub

    Private Sub RadButton89_Click(sender As Object, e As EventArgs) Handles RadButton89.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Code", "Description", "Type", "Nature", "IsMandatory", "Applicable For", "For Bulk Sale", "For Milk Grade", "Parameter Type") Then
                Try
                    Dim counter As Integer = 1
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim code As String = ""
                        Dim desc As String = ""
                        Dim type As String = ""
                        Dim ParamFor As String = ""
                        Dim ParameterType As String = ""
                        Dim ForBulksale As Integer = 0
                        Dim ForMilkGrade As Integer = 0
                        code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(code) <= 0 Then
                            Throw New Exception("Please Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(code) > 30 Then
                            Throw New Exception("Length of Code Should Not Exceed Max.30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(desc) <= 0 Then
                            Throw New Exception("Please Fill Description At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(desc) > 150 Then
                            Throw New Exception("Length of Description Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        type = clsCommon.myCstr(grow.Cells("Type").Value)
                        ForBulksale = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("For Bulk Sale").Value)), "True") = CompairStringResult.Equal, 1, 0)
                        ForMilkGrade = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("For Milk Grade").Value)), "True") = CompairStringResult.Equal, 1, 0)
                        If clsCommon.myLen(type) <= 0 Then
                            Throw New Exception("Please Fill Parameter Type At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        ParamFor = clsCommon.myCstr(grow.Cells("Applicable For").Value)
                        If clsCommon.myLen(ParamFor) <= 0 Then
                            Throw New Exception("Please Fill Applicable For At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(ParamFor, "BOTH") = CompairStringResult.Equal Or clsCommon.CompairString(ParamFor, "MCC") = CompairStringResult.Equal Or clsCommon.CompairString(ParamFor, "PLANT") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("' Applicable For ' should be either MCC/PLANT/BOTH At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        ParameterType = clsCommon.myCstr(grow.Cells("Parameter Type").Value)
                        If clsCommon.myLen(ParameterType) > 0 Then
                            If clsCommon.CompairString(ParameterType, "None") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "Purchase") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "Procurement") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "Job Work") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("' Parameter Type ' should be either Purchase/Procurement/Job Work At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(type, "FAT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "SNF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CLR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CORRECTION FACTOR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "TIME") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CHANNA") <> CompairStringResult.Equal Then
                                Throw New Exception("Please Fill Parameter Type Any One From " + Environment.NewLine + "FAT/SNF/CLR/CORRECTION FACTOR/TIME/OTHERS/Channa At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        Else
                            If clsCommon.CompairString(type, "FAT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "SNF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CLR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CORRECTION FACTOR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "TIME") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "ABB") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "AAB") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CHANNA") <> CompairStringResult.Equal Then
                                Throw New Exception("Please Fill Parameter Type Any One From " + Environment.NewLine + "FAT/SNF/CLR/CORRECTION FACTOR/TIME/OTHERS/ABB/AAB/Channa At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        If clsCommon.CompairString(type, "CORRECTION FACTOR") = CompairStringResult.Equal Then
                            type = "CF"
                        End If

                        Dim qry As String = "select count(*) from tspl_parameter_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" + code + "'"
                        Dim chk As Integer = clsDBFuncationality.getSingleValue(qry)

                        If clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                            qry = "select count(*) from TSPL_PARAMETER_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and type='" + clsCommon.myCstr(type) + "' and code<>'" + code + "'"
                            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                            If check > 0 Then
                                Throw New Exception("This Type (" + clsCommon.myCstr(type) + ") Is Already Exist,Please Change The Type At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim nature As String = ""
                        nature = clsCommon.myCstr(grow.Cells("Nature").Value)

                        If clsCommon.myLen(nature) <= 0 Then
                            Throw New Exception("Fill nature of parameter as Range/Alphanumeric/Boolean At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If clsCommon.CompairString(nature, "Range") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Alphanumeric") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Boolean") <> CompairStringResult.Equal Then
                            Throw New Exception("Fill nature of parameter as Range/Alphanumeric/Boolean At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(type, "ABB") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "AAB") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(nature, "Range") <> CompairStringResult.Equal Then
                                    Throw New Exception("Fill nature of parameter as Range At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        End If
                        Dim ismandatoryvalue As Integer = 0
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsMandatory").Value).ToUpper(), "YES") = CompairStringResult.Equal Then
                            ismandatoryvalue = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsMandatory").Value).ToUpper(), "NO") = CompairStringResult.Equal Then
                            ismandatoryvalue = 0
                        ElseIf clsCommon.myLen(grow.Cells("IsMandatory").Value) <= 0 Then
                            Throw New Exception("IsMandatory cannot be left blank At Line No. " + clsCommon.myCstr(counter) + "")
                        Else
                            Throw New Exception("IsMandatory should be Yes/No At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Dim obj As clsfrmParameterMaster = New clsfrmParameterMaster()
                        obj.code = code
                        obj.desc = desc
                        obj.type = type.ToUpper()
                        obj.nature = nature
                        obj.Param_For = ParamFor
                        obj.IsMandatory = ismandatoryvalue
                        obj.IsBulkSale = ForBulksale
                        obj.IsForMilkGrade = ForMilkGrade
                        If clsCommon.myCstr(ParameterType) = "" Then
                            obj.Parametertype = "None"
                        Else
                            obj.Parametertype = ParameterType
                        End If
                        If clsCommon.CompairString(obj.nature, "Range") = CompairStringResult.Equal Then
                            obj.nature = "R"
                        ElseIf clsCommon.CompairString(obj.nature, "Alphanumeric") = CompairStringResult.Equal Then
                            obj.nature = "A"
                        ElseIf clsCommon.CompairString(obj.nature, "Boolean") = CompairStringResult.Equal Then
                            obj.nature = "B"
                        End If
                        Dim isNewEntry As Boolean = True
                        If chk > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsfrmParameterMaster.SaveData(code, isNewEntry, trans, obj) Then
                            counter += 1
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Parameter Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton88_Click(sender As Object, e As EventArgs) Handles RadButton88.Click
        Try
            Me.Text = "Parameter Range "
            Dim qry As String = "select count(*) from tspl_parameter_range_master where comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select tspl_parameter_range_master.Code ,tspl_parameter_master.Description,tspl_parameter_range_master.Loc_Code,tspl_location_master.location_desc as [Location Description],tspl_parameter_range_master.Effective_Date,tspl_parameter_range_master.Lower_Range ,tspl_parameter_range_master.Upper_Range ,tspl_parameter_range_master.Value,tspl_parameter_range_master.Condition_Value  ,tspl_parameter_range_master.Status  as [Status],tspl_parameter_range_master.Vendor_Class ,tspl_parameter_range_master.IsReject   from tspl_parameter_range_master left outer join tspl_parameter_master on tspl_parameter_range_master.code=tspl_parameter_master.code and tspl_parameter_master.comp_code=tspl_parameter_range_master.comp_code left outer join tspl_location_master on tspl_location_master.location_code=tspl_parameter_range_master.loc_code"
            Else
                qry = "select '' as Code,'' as Description,'' as [Loc_Code],'' as [Location Description],'' as [Effective_Date],'' as [Lower_Range],'' as [Upper_Range],'' as [Value],'' as Condition_Value,'' as Status,'' as [Vendor_Class],'' as [IsReject]"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Parameter Range")
        End Try
    End Sub

    Function chkIdenticalRowsInMaster(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal VendorCls As String, ByVal isReject As Integer) As Boolean
        Dim rValue As Boolean = False
        If isReject = 1 Then
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select tspl_parameter_range_master.*,TSPL_PARAMETER_MASTER.Nature  from tspl_parameter_range_master left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code  where TSPL_PARAMETER_MASTER.nature='r' and TSPL_PARAMETER_RANGE_MASTER.isReject=1  and TSPL_PARAMETER_RANGE_MASTER.code='" & paramCode & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & LocCode & "' and TSPL_PARAMETER_RANGE_MASTER.Vendor_class='" & VendorCls & "' and convert(date,TSPL_PARAMETER_RANGE_MASTER.effective_date,103)='" & clsCommon.GetPrintDate(strEffectiveDate, "dd/MMM/yyyy") & "'")
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), LowerRange, UpperRange) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) Then
                        rValue = True
                        Exit For
                    End If
                Next
            End If

        Else
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select tspl_parameter_range_master.*,TSPL_PARAMETER_MASTER.Nature  from tspl_parameter_range_master left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code  where TSPL_PARAMETER_MASTER.nature='r' and TSPL_PARAMETER_RANGE_MASTER.isReject=0  and TSPL_PARAMETER_RANGE_MASTER.code='" & paramCode & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & LocCode & "' and TSPL_PARAMETER_RANGE_MASTER.Vendor_class='" & VendorCls & "' and convert(date,TSPL_PARAMETER_RANGE_MASTER.effective_date,103)='" & clsCommon.GetPrintDate(strEffectiveDate, "dd/MMM/yyyy") & "'")
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    If isBetween(LowerRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) OrElse isBetween(clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), LowerRange, UpperRange) OrElse isBetween(UpperRange, clsCommon.myCdbl(dtt.Rows(i)("Lower_range")), clsCommon.myCdbl(dtt.Rows(i)("Upper_range"))) Then
                        rValue = True
                        Exit For
                    End If
                Next
            End If
        End If
        Return rValue
    End Function
    Function isBetween(ByVal val As Double, ByVal LowerRange As Double, ByVal UpperRange As Double) As Boolean
        If (val >= LowerRange AndAlso val <= UpperRange) Then
            Return True
        Else
            Return False
        End If
    End Function
    Dim Row2 As Integer = 0
    Function CheckDuplicateParameterInGrid(ByVal ParamCode As String, ByVal nature As String, ByVal LowerRange As Double, ByVal upperRange As Double, ByVal ConditionValue As String, ByVal Status As String, ByVal isreject As Integer, ByVal locCode As String, ByVal vendorclass As String, ByVal effectiveDate As Date, ByVal value As Double, ByVal RowNo As Integer, ByVal gv1 As RadGridView) As Boolean
        Dim rValue As Boolean = False
        For i As Integer = 0 To gv1.Rows.Count - 1
            If i <> RowNo Then
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso LowerRange = clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value) AndAlso upperRange = clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value) AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells("Value").Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso LowerRange = clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value) AndAlso upperRange = clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value) AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ConditionValue, gv1.Rows(i).Cells("Condition_Value").Value) = CompairStringResult.Equal AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells("Value").Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ConditionValue, gv1.Rows(i).Cells("Condition_Value").Value) = CompairStringResult.Equal AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 1 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, gv1.Rows(i).Cells("Status").Value) = CompairStringResult.Equal AndAlso value = clsCommon.myCdbl(gv1.Rows(i).Cells("Value").Value) AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 0 Then
                    If clsCommon.CompairString(ParamCode, gv1.Rows(i).Cells("Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, gv1.Rows(i).Cells("Status").Value) = CompairStringResult.Equal AndAlso isreject = 0 AndAlso clsCommon.CompairString(locCode, gv1.Rows(i).Cells("Loc_Code").Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(vendorclass, gv1.Rows(i).Cells("Vendor_Class").Value) = CompairStringResult.Equal AndAlso clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value) Then
                        rValue = True
                        Row2 = i
                        Exit For
                    End If
                End If
            End If
        Next

        Return rValue
    End Function

    Function CheckDuplicateParameter(ByVal ParamCode As String, ByVal nature As String, ByVal LowerRange As Double, ByVal upperRange As Double, ByVal ConditionValue As String, ByVal Status As String, ByVal isreject As Integer, ByVal locCode As String, ByVal vendorclass As String, ByVal effectiveDate As Date, ByVal value As Double) As Boolean
        Dim rValue As Boolean = False
        Dim qry As String = " select count(*) from tspl_parameter_range_master where 1=1 and  "
        Dim whrCls As String = ""
        Dim whrCls1 As String = ""
        If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Lower_range=" & LowerRange & " and Upper_range=" & upperRange & " and  value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "r") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Lower_range=" & LowerRange & " and Upper_range=" & upperRange & " and isReject=" & isreject & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Condition_value='" & ConditionValue & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "a") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Condition_value='" & ConditionValue & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 1 Then
            whrCls = "  code='" & ParamCode & "' and Status='" & Status & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        ElseIf clsCommon.CompairString(nature, "b") = CompairStringResult.Equal And isreject = 0 Then
            whrCls = "  code='" & ParamCode & "' and Status='" & Status & "' and value=" & value & " and Loc_code='" & locCode & "' and Vendor_class='" & vendorclass & "' and convert(date,Effective_date,103)='" & clsCommon.GetPrintDate(effectiveDate, "dd/MMM/yyyy") & "'"
        End If
        If clsCommon.myLen(whrCls) > 0 Then
            qry = qry & whrCls
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                rValue = True
            Else
                rValue = False
            End If
        End If
        Return rValue
    End Function

    Function AllowToImport(ByRef gv1 As RadGridView) As Boolean
        Try
            Dim code As String = ""
            Dim LocCode As String = ""
            Dim LocCode1 As String = ""
            Dim lrange As Decimal = Nothing
            Dim urange As Decimal = Nothing
            Dim value As String = Nothing
            Dim condition As String = Nothing
            Dim conditionValue As String = Nothing
            Dim code1 As String = ""
            Dim lrange1 As Decimal = Nothing
            Dim urange1 As Decimal = Nothing
            Dim value1 As String = Nothing
            Dim condition1 As String = Nothing
            Dim conditionValue1 As String = Nothing
            Dim Status As String = Nothing
            Dim Status1 As String = Nothing
            Dim VendorClass As String = Nothing
            Dim VendorClass1 As String = Nothing
            Dim nature As String = Nothing
            Dim EffDate As String = Nothing
            Dim EffDate1 As String = Nothing
            Dim isReject As Decimal = Nothing
            Dim isReject1 As Decimal = Nothing
            gv1.Columns.Add("isOK", "isOK")
            code = clsCommon.myCstr(gv1.Rows(0).Cells("Code").Value)
            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please fill atleast one parameter range")
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                code = clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value)
                If clsCommon.myLen(code) <= 0 Then
                    Throw New Exception("please fill Code At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_parameter_master where code='" & code & "'")) = 0 Then
                    Throw New Exception("Parameter Code " & code & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells("Effective_Date").Value) > 0 Then
                    EffDate = clsCommon.GetPrintDate(gv1.Rows(ii).Cells("Effective_Date").Value, "dd/MMM/yyyy")
                Else
                    EffDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                End If

                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select nature from tspl_parameter_master where code='" & code & "'"))
                lrange = clsCommon.myCdbl(gv1.Rows(ii).Cells("Lower_Range").Value)
                urange = clsCommon.myCdbl(gv1.Rows(ii).Cells("Upper_Range").Value)
                value = clsCommon.myCstr(gv1.Rows(ii).Cells("Value").Value)
                LocCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Loc_Code").Value)
                VendorClass = clsCommon.myCstr(gv1.Rows(ii).Cells("Vendor_Class").Value)

                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("IsReject").Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("IsReject").Value), "1") = CompairStringResult.Equal Then
                    isReject = 1
                Else
                    isReject = 0
                End If
                If clsCommon.myLen(VendorClass) <= 0 Then
                    Throw New Exception("Please Fill Vendor Class at row no. " & (ii + 1))
                End If
                If clsCommon.CompairString(VendorClass, "A") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "B") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, "Other") <> CompairStringResult.Equal Then
                    Throw New Exception("Vendor Class Can be Either A/B/C/Other at row no. " & (ii + 1))
                End If
                If clsCommon.myLen(LocCode) <= 0 Then
                    Throw New Exception("please fill Location Code At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_location_master where Location_code='" & LocCode & "'")) = 0 Then
                    Throw New Exception("Location Code " & LocCode & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                End If

                conditionValue = clsCommon.myCstr(gv1.Rows(ii).Cells("Condition_Value").Value)
                If clsCommon.CompairString(nature, "a") = CompairStringResult.Equal Then
                    If clsCommon.myLen(conditionValue) <= 0 Then
                        Throw New Exception("please fill Alphanumeric Condition Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(*) from tspl_Parameter_value_master  where Parameter_CODE='" & code & "' and Value='" & conditionValue & "'")) = 0 Then
                        Throw New Exception("Parameter Alphanumeric Value " & conditionValue & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " Not found in master")
                    End If
                End If

                If clsCommon.myLen(value) > 0 AndAlso isReject = 1 AndAlso clsCommon.myCdbl(value) <> 0 Then
                    Throw New Exception("You can not fill incentive/Deduction For Parameter of QC Rejection  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                Status = clsCommon.myCstr(gv1.Rows(ii).Cells("Status").Value)

                If clsCommon.CompairString(nature, "b") = CompairStringResult.Equal Then
                    If clsCommon.myLen(Status) <= 0 Then
                        Throw New Exception("please fill either Yes/No in Status Field  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If Not (clsCommon.CompairString(Status, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(Status, "No") = CompairStringResult.Equal) Then
                        Throw New Exception("Status Can be Either Yes/No, But You have Field " & Status & "  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + " ")
                    End If
                End If
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) <= 0 Then
                        Throw New Exception("please fill lower range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) <= 0 Then
                        Throw New Exception("please fill Upper range  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso (Not IsNumeric(lrange)) Then
                        Throw New Exception("lower range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso (Not IsNumeric(urange)) Then
                        Throw New Exception(" Upper range must be a number  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myCdbl(lrange) > clsCommon.myCdbl(urange) Then
                        Throw New Exception(" Lower range must not be larger than Upper range   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If isReject = 0 AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(lrange) > 0 AndAlso clsCommon.myLen(urange) > 0 AndAlso IsNumeric(lrange) AndAlso IsNumeric(urange) AndAlso clsCommon.myLen(value) <= 0 Then
                        Throw New Exception(" Please fill Incentive/Deduction Value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If

                    If isReject = 1 Then
                        value = ""
                    End If
                ElseIf clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen("Status") <= 0 Then
                        Throw New Exception("please select Status At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen("Status") > 0 AndAlso clsCommon.myLen(value) <= 0 AndAlso isReject = 0 Then
                        Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 1 Then
                        value = ""
                    End If
                Else
                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen("Condition_Value") <= 0 Then
                        Throw New Exception("please Fill Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 0 AndAlso clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen("Condition_Value") > 0 AndAlso clsCommon.myLen(value) <= 0 Then
                        Throw New Exception("please fill Incentive/Deduction value   At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    If isReject = 1 Then
                        value = ""
                    End If
                End If
                If CheckDuplicateParameterInGrid(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value), ii, gv1) Then
                    Throw New Exception("Duplicate Values At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                End If
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If chkIdenticalRowsInGrid(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject, ii, gv1) Then
                        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " and " & (Row2 + 1))
                    End If
                End If
                If CheckDuplicateParameter(code, nature, lrange, urange, conditionValue, Status, isReject, LocCode, VendorClass, EffDate, clsCommon.myCdbl(value)) Then
                    Throw New Exception("Value At row No " & clsCommon.myCstr(CInt(ii) + 1) & " is already in master ")
                End If
                If clsCommon.CompairString(nature, "r") = CompairStringResult.Equal Then
                    If chkIdenticalRowsInMaster(code, LocCode, lrange, urange, value, EffDate, VendorClass, isReject) Then
                        Throw New Exception("Identical Rows Found   At Row No. " & clsCommon.myCstr(CInt(ii) + 1) & " ( When Comparing existing records in master ) ")
                    End If
                End If
                Dim dtt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_parameter_range_master")
                If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                    For jj As Integer = 0 To dtt.Rows.Count - 1
                        code1 = clsCommon.myCstr(dtt.Rows(jj)("Code"))
                        LocCode1 = clsCommon.myCstr(dtt.Rows(jj)("Loc_Code"))
                        lrange1 = clsCommon.myCdbl(dtt.Rows(jj)("Lower_Range"))
                        urange1 = clsCommon.myCdbl(dtt.Rows(jj)("Upper_Range"))
                        value1 = clsCommon.myCstr(dtt.Rows(jj)("Value"))
                        conditionValue1 = clsCommon.myCstr(dtt.Rows(jj)("Condition_Value"))
                        Status1 = clsCommon.myCstr(dtt.Rows(jj)("Status"))
                        VendorClass1 = clsCommon.myCstr(dtt.Rows(jj)("Vendor_Class"))
                        If clsCommon.myLen(dtt.Rows(jj)("Effective_Date")) > 0 Then
                            EffDate1 = clsCommon.GetPrintDate(dtt.Rows(jj)("Effective_Date"), "dd/MMM/yyyy")
                        Else
                            EffDate1 = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtt.Rows(jj)("IsReject")), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtt.Rows(jj)("IsReject")), "1") = CompairStringResult.Equal Then
                            isReject1 = 1
                        Else
                            isReject1 = 0
                        End If
                        If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(code, code1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(lrange, lrange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(urange, urange1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(conditionValue, conditionValue1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Status, Status1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(LocCode, LocCode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(EffDate, EffDate1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(VendorClass, VendorClass1) = CompairStringResult.Equal AndAlso isReject = isReject1 Then
                            gv1.Rows(ii).Cells("isOK").Value = "N"
                            Exit For
                        Else
                            gv1.Rows(ii).Cells("isOK").Value = "Y"
                        End If
                    Next
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function chkIdenticalRowsInGrid(ByVal paramCode As String, ByVal LocCode As String, ByVal LowerRange As Double, ByVal UpperRange As Double, ByVal value As String, ByVal strEffectiveDate As String, ByVal VendorCls As String, ByVal isReject As Integer, ByVal RowNo As Integer, ByRef gv1 As RadGridView) As Boolean
        Dim rValue As Boolean = False
        If isReject = 1 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(i).Cells("Code").Value, paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells("Loc_Code").Value, LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value, "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells("Vendor_Class").Value, VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells("IsReject").Value) = isReject AndAlso RowNo <> i Then
                    If isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value), clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value)) OrElse isBetween(UpperRange, clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value), clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value)) OrElse isBetween(clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value), LowerRange, UpperRange) OrElse isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value), clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value)) Then
                        Row2 = i
                        rValue = True
                        Exit For
                    End If
                End If
            Next
        Else
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(i).Cells("Code").Value, paramCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells("Loc_Code").Value, LocCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(gv1.Rows(i).Cells("Effective_Date").Value, "dd/MMM/yyyy"), strEffectiveDate) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(i).Cells("Vendor_Class").Value, VendorCls) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells("IsReject").Value) = isReject AndAlso RowNo <> i Then
                    If isBetween(LowerRange, clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value), clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value)) OrElse isBetween(UpperRange, clsCommon.myCdbl(gv1.Rows(i).Cells("Lower_Range").Value), clsCommon.myCdbl(gv1.Rows(i).Cells("Upper_Range").Value)) Then
                        Row2 = i
                        rValue = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return rValue
    End Function

    Private Sub RadButton85_Click(sender As Object, e As EventArgs) Handles RadButton85.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Try
            Dim currentdate As Date = Date.Today

            If transportSql.importExcel(gv1, "Code", "Description", "Loc_Code", "Location Description", "Effective_Date", "Lower_Range", "Upper_Range", "Value", "Condition_Value", "Status", "Vendor_Class", "IsReject") Then
                Try
                    If AllowToImport(gv1) Then
                        Dim arr As New List(Of clsfrmParameterRangeMaster)
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Dim LineNo As Integer = 0
                        Try
                            clsCommon.ProgressBarPercentShow()
                            For Each grow As GridViewRowInfo In gv1.Rows
                                LineNo += 1
                                clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv1.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv1.Rows.Count & "")
                                Dim obj As New clsfrmParameterRangeMaster()
                                obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                                obj.Loc_Code = clsCommon.myCstr(grow.Cells("Loc_Code").Value)
                                obj.Lrange = clsCommon.myCdbl(grow.Cells("Lower_Range").Value)
                                obj.Urange = clsCommon.myCdbl(grow.Cells("Upper_Range").Value)
                                obj.Value = clsCommon.myCstr(grow.Cells("Value").Value)
                                obj.Status = clsCommon.myCstr(grow.Cells("Status").Value)
                                obj.Vendor_Class = clsCommon.myCstr(grow.Cells("Vendor_Class").Value)
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsReject").Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsReject").Value), "1") = CompairStringResult.Equal Then
                                    obj.IsReject = 1
                                Else
                                    obj.IsReject = 0
                                End If
                                Try
                                    obj.Eff_date = Convert.ToDateTime(grow.Cells("Effective_Date").Value)
                                Catch exx As Exception
                                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                                End Try
                                Try
                                    If clsCommon.myCstr(obj.Eff_date).Substring(6, 4) = "0001" Then
                                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                                    End If
                                Catch exx As Exception
                                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                                End Try
                                obj.Condition_Value = clsCommon.myCstr(grow.Cells("Condition_Value").Value)
                                If clsCommon.myLen(obj.code) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("isOK").Value), "N") <> CompairStringResult.Equal Then
                                    arr.Add(obj)
                                End If
                            Next
                            If clsfrmParameterRangeMaster.SaveData(arr, trans, True) Then
                                trans.Commit()
                                clsCommon.ProgressBarPercentHide()
                                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                            Else
                                trans.Rollback()
                                clsCommon.ProgressBarPercentHide()
                                clsCommon.MyMessageBoxShow("No Data Transfer", Me.Text)
                            End If
                        Catch ex As Exception
                            trans.Rollback()
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Error at row no " + clsCommon.myCstr(LineNo) + ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Parameter Range")
        Finally
            Me.Controls.Remove(gv1)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        Try
            MDI.ShowForm(clsUserMgtCode.accountStructure, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            MDI.ShowForm(clsUserMgtCode.frmWeightConversion, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton60_Click(sender As Object, e As EventArgs) Handles RadButton60.Click
        Try
            Me.Text = ""
            qry = "select ITEM_CATEGORY_CODE as [Code],DESCRIPTION as [Description] ,CATEGORY_LEVEL as [Category Level]  from TSPL_ITEM_CATEGORY_LEVEL "
            transportSql.ExporttoExcel(qry, " and isnull(form_type,'ITEM')='ITEM'", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton59_Click(sender As Object, e As EventArgs) Handles RadButton59.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "Code", "Description", "Category Level") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim counter As Integer = 0
                Try
                    Dim Code As String = ""
                    Dim Description As String = ""
                    Dim Category_Level As Integer = "0"
                    Dim Bin_Mapping As Integer = "0"
                    clsCommon.ProgressBarPercentShow()
                    Dim obj As New clsItemCategoryLevel()
                    Dim count As Integer = 0
                    Dim checkcode As String = ""
                    Dim codevalue As String = ""
                    Dim qry As String = ""
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If Code IsNot Nothing Then
                            qry = "Select ITEM_CATEGORY_CODE from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE ='" + Code + "' and isnull(form_type,'ITEM')='ITEM'"
                            checkcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Code) > 20 Then
                                Throw New Exception("Length Of Code Should Not Exceed 20 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        Else
                            clsCommon.ProgressBarHide()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(Description) <= 0 Then
                            Throw New Exception("Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Description  ")
                        ElseIf clsCommon.myLen(Description) > 100 Then
                            Throw New Exception("Length Of Description Should Not Exceed 100 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Category_Level = clsCommon.myCdbl(grow.Cells("Category Level").Value)
                        If (CInt(Category_Level) <= 0) Then
                            Throw New Exception("Fill Category Level At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        count = (clsDBFuncationality.getSingleValue("select  Count(*) from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL ='" + clsCommon.myCstr(Category_Level) + "' and isnull(form_type,'ITEM')='ITEM'", trans))
                        If count > 0 Then
                            Dim newcount As Integer
                            newcount = (clsDBFuncationality.getSingleValue("select  Count(*) from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL ='" + clsCommon.myCstr(Category_Level) + "'and Item_category_code ='" + clsCommon.myCstr(Code) + "' and isnull(form_type,'ITEM')='ITEM'", trans))
                            If newcount > 0 Then
                            Else
                                Throw New Exception(" Category Level Should Not be Same as previous Level value,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim exitqry As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL where TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(Code) + "'and TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION ='" + Description + "' and isnull(form_type,'ITEM')='ITEM'"
                        Dim check As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(exitqry, trans))
                        Dim isSaved As Boolean = True
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", Code)
                        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", Description)
                        clsCommon.AddColumnsForChange(coll, "CATEGORY_LEVEL", Category_Level)
                        'clsCommon.AddColumnsForChange(coll, "Bin_Mapping", Bin_Mapping)
                        clsCommon.AddColumnsForChange(coll, "form_type", "ITEM")
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                        If (check) > 0 Then
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Update, "ITEM_CATEGORY_CODE='" + Code + "' and isnull(form_type,'ITEM')='ITEM'", trans)
                        Else
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    Next
                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at line no: " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton58_Click(sender As Object, e As EventArgs) Handles RadButton58.Click
        Try
            Me.Text = ""
            Dim qry As String = "select TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE as [Item Category Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as [Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as [Description] from TSPL_ITEM_CATEGORY_LEVEL_VALUES left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL .ITEM_CATEGORY_CODE =TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type "
            transportSql.ExporttoExcel(qry, " and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'ITEM')='ITEM'", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton57_Click(sender As Object, e As EventArgs) Handles RadButton57.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim trans As SqlTransaction = Nothing
            Dim counter As Integer = 0
            Try
                If transportSql.importExcel(gv, "Item Category Code", "Code", "Description") Then
                    trans = clsDBFuncationality.GetTransactin()
                    Dim Item_Category_Code As String = ""
                    Dim Description As String = ""
                    Dim Code As String = ""

                    clsCommon.ProgressBarPercentShow()

                    Dim checkcode As String = ""
                    Dim codevalue As String = ""
                    Dim qry As String = ""
                    Dim arrCategory As New List(Of String)
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        counter += 1
                        Item_Category_Code = clsCommon.myCstr(grow.Cells("Item Category Code").Value)

                        qry = "select ITEM_CATEGORY_CODE from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE='" + Item_Category_Code + "' and  isnull(form_type,'ITEM')='ITEM'"
                        Item_Category_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(Item_Category_Code) <= 0 Then
                            Throw New Exception("Wrong item category code " + Item_Category_Code)
                        End If
                        If clsCommon.myLen(Item_Category_Code) > 20 Then
                            Throw New Exception("Length Of Code Should Not Exceed 20 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(Description) <= 0 Then
                            Throw New Exception("ITEM Description  Code and Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code  and Description Correctly ")
                        End If
                        If clsCommon.myLen(Description) > 100 Then
                            Throw New Exception("Length Of Description Should Not Exceed 100 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(Code) <= 0 Then
                            Throw New Exception("Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(Code) >= 60 Then
                            Throw New Exception("Length Of Code Should Not Exceed ,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim exitquery As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL_VALUES "
                        exitquery += " where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE ='" + Item_Category_Code + "' and Code='" + Code + "' and Description='" + Description + "' and isnull(form_type,'ITEM')='ITEM'"
                        Dim check As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(exitquery, trans))

                        Dim isSaved As Boolean = True
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", Item_Category_Code)
                        clsCommon.AddColumnsForChange(coll, "CODE", Code)
                        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", Description)
                        clsCommon.AddColumnsForChange(coll, "form_type", "ITEM")
                        'clsCommon.AddColumnsForChange(coll, "Bin_No", BinNo)
                        If check <= 0 Then
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Update, "ITEM_CATEGORY_CODE='" + Item_Category_Code + "' and isnull(form_type,'ITEM')='ITEM' and CODE='" + Code + "'", trans)
                        End If
                        If Not arrCategory.Contains(Item_Category_Code) Then
                            arrCategory.Add(Item_Category_Code)
                        End If
                    Next
                    If arrCategory IsNot Nothing AndAlso arrCategory.Count > 0 Then
                        qry = "select ITEM_CATEGORY_CODE,CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE in (" + clsCommon.GetMulcallString(arrCategory) + ") group by ITEM_CATEGORY_CODE,CODE having sum(1)>1"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Repeated Category:" + clsCommon.myCstr(dt.Rows(0)("ITEM_CATEGORY_CODE")) + " And Value:" + clsCommon.myCstr(dt.Rows(0)("CODE")) + " And Description:" + clsCommon.myCstr(dt.Rows(0)("DESCRIPTION")))
                        End If
                    End If

                  

                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                Throw New Exception("Error at line no" + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton55_Click(sender As Object, e As EventArgs) Handles RadButton55.Click
        Try
            MDI.ShowForm(clsUserMgtCode.frmItemCategoryStructure, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton50_Click(sender As Object, e As EventArgs) Handles RadButton50.Click
        Try
            Me.Text = "Vendor Bank Master"
            qry = "select Bank_Code as 'Bank Code' ,Bank_Name as 'Bank Name',Add1 as 'Add1',Country_Code as 'Country Code' ,State_Code as 'State Code' ,City_Code as 'City Code' from TSPL_Vendor_Bank_Master"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ChkCountry(ByVal CountryCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsDBFuncationality.getSingleValue("Select count(*) from tspl_country_master where country_code='" + CountryCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("Country code is invalid,It could not found in Country Master")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ChkStateCode(ByVal StateCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" + StateCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("State code is invalid,It could not found in State Master")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ChkCityCode(ByVal CityCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(CityCode) > 0 AndAlso clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" + CityCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("City code is invalid,It could not found in City Master")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub RadButton49_Click(sender As Object, e As EventArgs) Handles RadButton49.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim trans As SqlTransaction = Nothing
            If transportSql.importExcel(gv, "Bank Code", "Bank Name", "Add1", "Country Code", "State Code", "City Code") Then
                Dim linno As Integer = 0
                trans = clsDBFuncationality.GetTransactin()
                Try
                    'connectSql.OpenConnection()
                    clsCommon.ProgressBarPercentShow()
                    linno = 0
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New clsVendorBankMaster()
                        linno += 1
                        Dim strBankcode As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                        If clsCommon.myLen(strBankcode) > 30 Then
                            Throw New Exception("Length of Bank Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Bank_code = strBankcode

                        Dim strBankName As String = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                        If (String.IsNullOrEmpty(strBankName)) Or clsCommon.myLen(strBankName) > 200 Then
                            Throw New Exception("Length of Bank Name should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Bank_Name = strBankName

                        Dim strAdd1 As String = clsCommon.myCstr(grow.Cells("Add1").Value)
                        If clsCommon.myLen(strAdd1) > 150 Then
                            Throw New Exception("Length of Add1 should be max. 150 character ")
                        End If
                        obj.add1 = strAdd1

                        Dim strCountry As String = clsCommon.myCstr(grow.Cells("Country Code").Value)
                        If clsCommon.myLen(strCountry) > 30 Then
                            Throw New Exception("Length of Country code should be max. 30 character ")
                        End If
                        obj.country_code = strCountry

                        If clsCommon.myLen(strCountry) > 0 Then
                            ChkCountry(strCountry, trans)
                        End If
                        Dim strstate As String = clsCommon.myCstr(grow.Cells("State Code").Value)
                        If clsCommon.myLen(strstate) > 30 Then
                            Throw New Exception("Length of State Code should be max. 30 character ")
                        End If
                        obj.state_code = strstate
                        If clsCommon.myLen(strstate) > 0 Then
                            ChkStateCode(strstate, trans)
                        End If
                        Dim strcity As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                        If clsCommon.myLen(strcity) > 30 Then
                            Throw New Exception("Length of City Code should be max. 30 character ")
                        End If
                        obj.city_code = strcity
                        If clsCommon.myLen(strcity) > 0 Then
                            ChkCityCode(strcity, trans)
                        End If
                        Dim isNewEntry As Boolean = True
                        If clsCommon.myLen(strBankcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Master where Bank_Code ='" + strBankcode + "' ", trans) > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        clsVendorBankMaster.SaveDataImport(isNewEntry, obj, trans)
                    Next

                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error At Line No. " & clsCommon.myCstr(linno) & Environment.NewLine + ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally

        End Try
    End Sub
   

    Private Sub RadButton42_Click(sender As Object, e As EventArgs) Handles RadButton42.Click
        Try
            Me.Text = "Milk Route Master"
            qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name]," + _
            " TSPL_MCC_ROUTE_MASTER.vehicle_code as [Vehicle Code],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name]," + _
            " TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Code],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No]," + _
            " TSPL_MCC_ROUTE_MASTER.email_id as [Email ID] from TSPL_MCC_ROUTE_MASTER " + _
            " left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton41_Click(sender As Object, e As EventArgs) Handles RadButton41.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim counter As Integer = 1
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Code", "Route Name", "Vehicle Code", "MCC Code", "MCC Name", "KiloMeter", "Supervisor Code", "Contact No", "Email ID") Then
                Try
                    clsCommon.ProgressBarPercentShow()
                    Dim obj As clsfrmMilkRouteMaster
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        obj = New clsfrmMilkRouteMaster()
                        clsfrmMilkRouteMaster.arr_VLC_Detail = Nothing
                        obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                        obj = clsfrmMilkRouteMaster.GetData(obj.code, Nothing, NavigatorType.Current)
                        If obj Is Nothing Then
                            obj = New clsfrmMilkRouteMaster
                        End If
                        obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                        'If clsCommon.myLen(obj.code) <= 0 Or clsCommon.myLen(obj.code) > 30 Then
                        '    Throw New Exception("Please Fill Route Code(Max. 30 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If
                        obj.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
                        If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                            Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Dim check As Integer = 0
                        obj.vehiclecode = clsCommon.myCstr(grow.Cells("vehicle code").Value)

                        obj.Active = 1

                        If clsCommon.myLen(obj.vehiclecode) <= 0 Then
                            Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.vehiclecode) > 0 Then
                            qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + obj.vehiclecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)
                            If check <= 0 Then
                                Throw New Exception("Filled Vehicle Code Is Invlaid Or Does Not Exist in Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                        If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                check = clsDBFuncationality.getSingleValue(qry)

                                If check <= 0 Then
                                    obj.mcccode = ""
                                    Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        End If
                        If clsCommon.myLen(obj.mcccode) <= 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        obj.kilometer = clsCommon.myCdbl(grow.Cells("KiloMeter").Value)
                        If clsCommon.myLen(obj.kilometer) <= 0 Or clsCommon.myCdbl(obj.kilometer) <= 0 Then
                            Throw New Exception("Please Fill KiloMeter Value And It Should Be Greater Than Zero(0) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        obj.supervisorname = clsCommon.myCstr(grow.Cells("Supervisor Code").Value)
                        If clsCommon.myLen(obj.supervisorname) <= 0 Or clsCommon.myLen(obj.supervisorname) > 100 Then
                            Throw New Exception("Please Fill Supervisor Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_employee_master where emp_code='" & obj.supervisorname & "'"))
                        If cnt <= 0 Then
                            Throw New Exception("Invalid Supervisor Code. Not Found in master,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        qry = "select PRESENT_MOBILE_NO,PRESENT_CITY_CODE,City_Name ,PRESENT_STATE_CODE,STATE_NAME ,PRESENT_COUNTRY_CODE,COUNTRY_NAME  ,EMail_ID  from TSPL_EMPLOYEE_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_EMPLOYEE_MASTER.PRESENT_CITY_CODE left outer join TSPL_state_MASTER on TSPL_state_MASTER.STATE_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_STATE_CODE left outer join TSPL_COUNTRY_MASTER  on TSPL_COUNTRY_MASTER .COUNTRY_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_COUNTRY_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" & obj.supervisorname & "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            obj.contactno = clsCommon.myCstr(dt.Rows(0)("PRESENT_MOBILE_NO"))
                            obj.email = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
                        End If
                        qry = "select count(*) from tspl_mcc_route_master where route_code='" + obj.code + "'"
                        check = clsDBFuncationality.getSingleValue(qry)
                        Dim isNewEntry As Boolean = True
                        If check <= 0 Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If

                        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        If clsfrmMilkRouteMaster.SaveData(obj.code, obj, isNewEntry, True) Then
                        Else
                            Throw New Exception("No Data Transfer")
                        End If
                        counter += 1
                    Next
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Line No- " + clsCommon.myCstr(counter) + Environment.NewLine + ex.Message)
                End Try
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton56_Click(sender As Object, e As EventArgs) Handles RadButton56.Click
        Try
            MDI.ShowForm(clsUserMgtCode.FrmPriceChartUploader, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton63_Click(sender As Object, e As EventArgs) Handles RadButton63.Click
        Try
            MDI.ShowForm(clsUserMgtCode.LocationDistanceMapping, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton84_Click(sender As Object, e As EventArgs) Handles RadButton84.Click
        Try
            MDI.ShowForm(clsUserMgtCode.frmParameterRangeMasterForQC, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton98_Click(sender As Object, e As EventArgs) Handles RadButton98.Click
        Try
            Me.Text = "Bulk Sale Price Chart"
            qry = "select Price_Code as [Price Code],Convert (varchar, Price_Date,103) As [Price Date] ,Location_Code as [Location Code], Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Ratio as[Fat Ratio],Snf_Ratio as [SNF Ratio],Standard_Rate as [Standard Rate],TSRate as [TS Rate] from TSPL_BulkSalePrice_MASTER"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Bulk Sale Price Chart")
        End Try
    End Sub

    Private Sub RadButton97_Click(sender As Object, e As EventArgs) Handles RadButton97.Click
        Dim ApplyTSPriceAtBulkSale As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim IsNewEntry As Boolean
            If transportSql.importExcel(gv, "Price Code", "Price Date", "Location Code", "Fat Weightage", "SNF Weightage", "Fat Ratio", "SNF Ratio", "Standard Rate", "TS Rate") Then
                Dim linno As Integer = 1
                Dim obj As New ClsBulkSalePriceChart()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        linno += 1
                        Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                        Dim strPriceDate As String = clsCommon.myCDate(grow.Cells("Price Date").Value)
                        Dim DblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                        Dim DblFatRatio As Double = clsCommon.myCdbl(grow.Cells("Fat Ratio").Value)
                        Dim DblSnfWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                        Dim DblSnfRatio As Double = clsCommon.myCdbl(grow.Cells("SNF Ratio").Value)
                        Dim DblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                        Dim DblTSRate As Double = clsCommon.myCdbl(grow.Cells("TS Rate").Value)

                        obj.Price_Code = strPriceCode

                        If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                            Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Date = strPriceDate

                        If clsCommon.myCdbl(DblFatWeightage) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                            Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Weightage = DblFatWeightage

                        If clsCommon.myCdbl(DblSnfWeightage) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                            Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Weightage = DblSnfWeightage

                        If clsCommon.myCdbl(DblFatRatio) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                            Throw New Exception("Fat Ratio should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Ratio = DblFatRatio

                        If clsCommon.myCdbl(DblSnfRatio) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                            Throw New Exception("SNF Ratio should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Ratio = DblSnfRatio

                        If clsCommon.myCdbl(DblStandardRate) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                            Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Standard_Rate = DblStandardRate

                        obj.Location_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                        If clsCommon.myLen(obj.Location_Code) <= 0 Then
                            Throw New Exception("Please enter location Code")
                        End If
                        obj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "'", trans))
                        If clsCommon.myLen(obj.Location_Code) <= 0 Then
                            Throw New Exception("not a valid location Code")
                        End If

                        If clsCommon.myCdbl(DblTSRate) <= 0 AndAlso ApplyTSPriceAtBulkSale = True Then
                            Throw New Exception("TS should not be left blank or zero or string At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.TSRate = DblTSRate

                        If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                            IsNewEntry = False
                        Else
                            IsNewEntry = True
                        End If
                        ClsBulkSalePriceChart.SaveData(obj, IsNewEntry, trans)
                    Next
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception("Error at Line No " + clsCommon.myCstr(linno) + ex.Message)
                Finally
                    obj = Nothing
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Private Sub RadButton73_Click(sender As Object, e As EventArgs) Handles RadButton73.Click
        Try
            qry = "Select Bank_Code as [Bank Code],Branch_Name as [Branch],Bank_IFSC_Code as [IFSC Code],Bank_Swift_Code as [Swift Code] from TSPL_Vendor_Bank_Branch_Details "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton64_Click(sender As Object, e As EventArgs) Handles RadButton64.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, "Bank Code", "Branch", "IFSC Code", "Swift Code") Then
                Dim linno As Integer = 0
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsCommon.ProgressBarPercentShow()
                    linno = 0
                    Dim strBankcode As String = Nothing
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim obj As New clsVendorBankBranchDetail()
                        linno += 1
                        strBankcode = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                        If clsCommon.myLen(strBankcode) > 30 Then
                            Throw New Exception("Length of Bank Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Bank_Code = strBankcode


                        Dim strBranchName As String = clsCommon.myCstr(grow.Cells("Branch").Value)
                        If (String.IsNullOrEmpty(strBranchName)) Or clsCommon.myLen(strBranchName) > 100 Then
                            Throw New Exception("Length of Branch should be max. 100 character ")
                        End If

                        obj.Branch_Name = strBranchName

                        Dim strIFSC As String = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                        If (String.IsNullOrEmpty(strIFSC)) Or clsCommon.myLen(strIFSC) > 100 Then
                            Throw New Exception("Length of IFSC Code should be max. 100 character ")
                        End If
                        obj.Bank_IFSC_Code = strIFSC

                        Dim strSwift As String = clsCommon.myCstr(grow.Cells("Swift Code").Value)
                        If clsCommon.myLen(strSwift) > 20 Then
                            Throw New Exception("Length of Swift Code should be max. 20 character ")
                        End If
                        obj.Bank_Swift_Code = strSwift


                        Dim coll As Hashtable

                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Master where Bank_Code ='" + strBankcode + "'  ", trans) <= 0 Then
                            Throw New Exception("Bank Code Does Not Exist : " + strBankcode + ".Please make entry in vendor bank master.")
                        End If

                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Bank_Code", strBankcode)
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name)
                        clsCommon.AddColumnsForChange(coll, "Bank_IFSC_Code", obj.Bank_IFSC_Code)
                        clsCommon.AddColumnsForChange(coll, "Bank_Swift_Code", obj.Bank_Swift_Code)
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSC + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & obj.Bank_Code & "'", trans) <= 0 Then
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Branch_Details", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Branch_Details", OMInsertOrUpdate.Update, " TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code='" & obj.Bank_IFSC_Code & "' and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & obj.Bank_Code & "'", trans)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(ex.Message & " At Line No. " & clsCommon.myCstr(linno) & "")
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton74_Click(sender As Object, e As EventArgs) Handles RadButton74.Click
        Try
            MDI.ShowForm(clsUserMgtCode.PricePlan, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton81_Click(sender As Object, e As EventArgs) Handles RadButton81.Click
        Try
            MDI.ShowForm(clsUserMgtCode.createAccounts, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton82_Click(sender As Object, e As EventArgs) Handles RadButton82.Click
        Try
            MDI.ShowForm(clsUserMgtCode.FrmItemTypeMaster, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton99_Click(sender As Object, e As EventArgs) Handles RadButton99.Click
        Try
            Me.Text = "HSN Master"
            Dim query As String = "select Code, Description from TSPL_HSN_MASTER"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "HSN Master")
        End Try
    End Sub

    Private Sub RadButton83_Click(sender As Object, e As EventArgs) Handles RadButton83.Click
        Dim isNewEntry As Boolean = True
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description") Then
            Try
                clsCommon.ProgressBarShow()
                'Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New ClsHSNMaster()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Code can not be blank.")
                        End If
                        If clsCommon.myLen(obj.Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        ClsHSNMaster.SaveData(obj, isNewEntry)

                    Next
                Catch ex As Exception
                    ' tran.Rollback()
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                '  tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub frmImplementImportExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowItemConversionAutomation = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemConversionAutomation, clsFixedParameterCode.AllowItemConversionAutomation, Nothing))
    End Sub

    Private Sub RadButton103_Click(sender As Object, e As EventArgs) Handles RadButton103.Click
        Try
            Me.Text = "Payment Terms"
            Dim query As String = "Select Terms_Code as [Terms Code],Terms_Desc as [Terms Desc],No_Days as [No Days],LCRequired as [LC Required],isAdvance as [isAdvance]  , convert(varchar,Advance_Per) as [Advance(%)]  from TSPL_TERMS_MASTER"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Payment Terms")
        End Try

    End Sub

    Private Sub RadButton100_Click(sender As Object, e As EventArgs) Handles RadButton100.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Terms Code", "Terms Desc", "No Days", "LC Required", "isAdvance", "Advance(%)") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strtermscode As String = grow.Cells(0).Value.ToString()
                    Dim strtermsdesc As String = grow.Cells(1).Value.ToString()
                    Dim strno_of_days As String = grow.Cells(2).Value.ToString()
                    Dim strLCrequired As String = grow.Cells(3).Value.ToString()
                    Dim strisAdvanc As String = grow.Cells(4).Value.ToString()
                    Dim strAdvancePer As String = grow.Cells(5).Value.ToString()
                    '-------------------------------------
                    If strtermscode = String.Empty Then
                        myMessages.blankValue(Me, "Terms Code", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If

                    If strtermsdesc = String.Empty Then
                        myMessages.blankValue(Me, "Terms Description", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strno_of_days = String.Empty Then
                        myMessages.blankValue(Me, "No_Of_Days", Me.Text)
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strtermscode.Length > 10 Then
                        common.clsCommon.MyMessageBoxShow("Terms Code length cannot be more than 10")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strtermsdesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Terms Description length cannot be more than 50")
                        trans.Rollback()
                        Exit Sub
                    End If
                    
                    If IsNumeric(grow.Cells(2).Value) Then

                        Dim dc As Decimal = Decimal.Parse(grow.Cells(2).Value)
                        If dc > 999 Or dc < 0 Then
                            common.clsCommon.MyMessageBoxShow("No_Of_Days must between 0 - 999")
                            trans.Rollback()
                            Exit Sub
                        Else
                            strno_of_days = grow.Cells(2).Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("No_of_days only accept numeric value")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If IsNumeric(grow.Cells("LC Required").Value) Then

                        Dim dcv As Decimal = Decimal.Parse(grow.Cells("LC Required").Value)
                        If dcv > 1 Or dcv < 0 Then
                            common.clsCommon.MyMessageBoxShow("LC Required must be 0 or 1")
                            trans.Rollback()
                            Exit Sub
                        Else
                            strLCrequired = grow.Cells("LC Required").Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("LC Required only accept numeric value")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If IsNumeric(grow.Cells("isAdvance").Value) Then
                        Dim dcv As Decimal = Decimal.Parse(grow.Cells("isAdvance").Value)
                        If dcv > 1 Or dcv < 0 Then
                            common.clsCommon.MyMessageBoxShow("isAdvance must be 0 or 1")
                            trans.Rollback()
                            Exit Sub
                        Else
                            strisAdvanc = grow.Cells("isAdvance").Value.ToString()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("isAdvance only accept numeric value")
                        trans.Rollback()
                        Exit Sub
                    End If

                    If strisAdvanc = 1 Then
                        If IsNumeric(grow.Cells("Advance(%)").Value) Then
                            Dim dcv As Decimal = Decimal.Parse(grow.Cells("Advance(%)").Value)
                            If dcv > 99 Then
                                common.clsCommon.MyMessageBoxShow("Advance(%) less then 100")
                                trans.Rollback()
                                Exit Sub
                            Else
                                strAdvancePer = grow.Cells("Advance(%)").Value.ToString()
                            End If
                        Else
                            common.clsCommon.MyMessageBoxShow("Advance(%) only accept numeric value")
                            trans.Rollback()
                            Exit Sub
                        End If
                    Else
                        strAdvancePer = 0
                    End If


                    Dim sql1 As String = "select count(*) from TSPL_TERMS_MASTER where Terms_Code='" + strtermscode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_tspl_terms_masterinsert", New SqlParameter("@code", strtermscode), New SqlParameter("@desc", strtermsdesc), New SqlParameter("@no_days", strno_of_days), New SqlParameter("@crt_by", objCommonVar.CurrentUserCode), New SqlParameter("@crtdate", connectSql.serverDate(trans)), New SqlParameter("@mod_by", objCommonVar.CurrentUserCode), New SqlParameter("@mod_date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", objCommonVar.CurrentCompanyCode), New SqlParameter("@LCRequired", strLCrequired))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_tspl_terms_masterUpdate", New SqlParameter("@code", strtermscode), New SqlParameter("@desc", strtermsdesc), New SqlParameter("@no_days", strno_of_days), New SqlParameter("@crt_by", objCommonVar.CurrentUserCode), New SqlParameter("@crtdate", connectSql.serverDate(trans)), New SqlParameter("@mod_by", objCommonVar.CurrentUserCode), New SqlParameter("@mod_date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", objCommonVar.CurrentCompanyCode), New SqlParameter("@LCRequired", strLCrequired))
                    End If

                    Dim coll As New Hashtable()
                    If clsCommon.myLen(strtermscode) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "isAdvance", IIf(strisAdvanc, 1, 0))
                        clsCommon.AddColumnsForChange(coll, "Advance_Per", strAdvancePer)
                        ' clsCommon.AddColumnsForChange(coll, "Due_Date_By", cboDueDate.SelectedValue)
                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_terms_master", OMInsertOrUpdate.Update, "Terms_Code='" + strtermscode + "'", trans)
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadButton111_Click(sender As Object, e As EventArgs) Handles RadButton111.Click
        Try
            Me.Text = "Region Master"
            Dim query As String = "select Region_code AS Code, Region_name as [Region Name] from TSPL_REGION_MASTER"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Region Master")
        End Try
    End Sub

    Private Sub RadButton110_Click(sender As Object, e As EventArgs) Handles RadButton110.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Region Name") Then

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsRegionMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Name can not be blank or incorrect.")
                    End If
                    obj.name = strName

                    ClsRegionMaster.SaveData(obj, obj.code)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadButton109_Click(sender As Object, e As EventArgs) Handles RadButton109.Click
        Try
            Me.Text = "District Master"
            Dim query As String = "select Code, Name, State_Code,Region_Code from TSPL_DISTRICT_MASTER"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "District Master")
        End Try
    End Sub


    Private Sub RadButton108_Click(sender As Object, e As EventArgs) Handles RadButton108.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Name", "State_Code", "Region_Code") Then
            Try
                clsCommon.ProgressBarShow()
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsDistrictMaster()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Code can not be blank or incorrect.")
                        End If

                        obj.Name = clsCommon.myCstr(grow.Cells("Name").Value)
                        If clsCommon.myLen(obj.Name) <= 0 Then
                            Throw New Exception("Name can not be blank or incorrect.")
                        End If

                        obj.Region_Code = clsCommon.myCstr(grow.Cells("Region_Code").Value)
                        If clsCommon.myLen(obj.Region_Code) <= 0 Then
                            Throw New Exception("Region Code can not be blank.")
                        End If
                        obj.Region_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select region_code from tspl_region_master where region_code='" + obj.Region_Code + "'", tran))
                        If clsCommon.myLen(obj.Region_Code) <= 0 Then
                            Throw New Exception("Region Code is not valid.")
                        End If


                        obj.State_Code = clsCommon.myCstr(grow.Cells("State_Code").Value)
                        If clsCommon.myLen(obj.Region_Code) <= 0 Then
                            Throw New Exception("State Code can not be blank")
                        End If
                        obj.State_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STATE_CODE from TSPL_STATE_MASTER where STATE_CODE='" + obj.State_Code + "'", tran))
                        If clsCommon.myLen(obj.State_Code) <= 0 Then
                            Throw New Exception("State Code is not valid.")
                        End If

                        clsDistrictMaster.SaveData(obj, clsDistrictMaster.CheckNewEntry(obj.Code, tran), tran)
                    Next
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + ex.Message)
                End Try
                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadButton107_Click(sender As Object, e As EventArgs) Handles RadButton107.Click
        Try
            Me.Text = "City Master"
            Dim query As String = "select city_Code As [City Code], city_name as [City Name],STATE_CODE as [State Code],Region_code as [Region Code],DISTRICT_Code as [District Code] from tspl_city_master"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "City Master")
        End Try
    End Sub

    Private Sub RadButton106_Click(sender As Object, e As EventArgs) Handles RadButton106.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "City Code", "City Name", "State Code", "Region Code", "District Code") Then
            ' Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Try
                ' connectSql.OpenConnection()
                ' trans = clsDBFuncationality.GetTransactin()

                clsCommon.ProgressBarShow()


                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCityMaster()
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 50 Then
                        Throw New Exception("Length of City Code should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.City_Code = strcode

                    Dim strname As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If (String.IsNullOrEmpty(strname)) Or clsCommon.myLen(strname) > 50 Then
                        Throw New Exception("Length of City Name should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.City_Name = strname

                    Dim strState As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If (String.IsNullOrEmpty(strState)) Or clsCommon.myLen(strState) > 30 Then
                        Throw New Exception("Length of State Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.STATE_CODE = strState

                    obj.regioncode = clsCommon.myCstr(grow.Cells("region code").Value)
                    If clsCommon.myLen(obj.regioncode) > 30 Then
                        Throw New Exception("Length of Region Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    obj.DISTRICT_Code = clsCommon.myCstr(grow.Cells("District Code").Value)

                    If clsCommon.myLen(obj.DISTRICT_Code) > 0 Then
                        If clsCommon.myLen(obj.DISTRICT_Code) > 30 Then
                            Throw New Exception("Length of District Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        obj.DISTRICT_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_DISTRICT_MASTER where Code='" + obj.DISTRICT_Code + "'"))
                        If clsCommon.myLen(obj.DISTRICT_Code) <= 0 Then
                            Throw New Exception("Not valid District Code : " + clsCommon.myCstr(grow.Cells("District Code").Value) + " . " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If


                    Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_STATE_MASTER_DETAIL where STATE_CODE='" + obj.STATE_CODE + "'")
                    If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                        dtCheck = clsDBFuncationality.GetDataTable("select 1 from TSPL_STATE_MASTER_DETAIL where STATE_CODE='" + obj.STATE_CODE + "' and Region_Code='" + obj.regioncode + "'")
                        If dtCheck Is Nothing OrElse dtCheck.Rows.Count <= 0 Then
                            Throw New Exception("State-" + obj.STATE_CODE + " Region-" + obj.regioncode + "  is not mapped in state master.")
                        End If
                    End If
                    obj.SaveData(obj, True)
                Next
                'trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadButton105_Click(sender As Object, e As EventArgs) Handles RadButton105.Click
        Try
            Me.Text = "Payment Cycle"
            Dim query As String = "select PC_CODE AS Code, DESCRIPTION AS Description,PC_TYPE AS [Type],PC_VALUE AS [Value] from TSPL_PAYMENT_CYCLE_MASTER"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Payment Cycle")
        End Try
    End Sub

    Private Sub RadButton104_Click(sender As Object, e As EventArgs) Handles RadButton104.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Type", "Value") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsPaymentCycleMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.PC_CODE = strCode

                    Dim strDes As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strDes.Length > 200 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.Description = strDes

                    Dim strTYPE As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strTYPE.Length > 30 Then
                        Throw New Exception("Type can not be blank or incorrect.")
                    End If
                    obj.PC_TYPE = strTYPE

                    Dim strvalue As String = clsCommon.myCdbl(grow.Cells(3).Value)
                    obj.PC_VALUE = strvalue

                    obj.SaveData(obj, clsPaymentCycleMaster.CheckNewEntry(obj.PC_CODE))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadButton113_Click(sender As Object, e As EventArgs) Handles RadButton113.Click
        'Export
        Try
            Me.Text = "Milk Route Master"
            Dim extraColumn As String = ""
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnShow, clsFixedParameterCode.VLCTimeTableColumnShow, Nothing)) > 0, True, False) = True Then
                extraColumn = " ,TSPL_MCC_ROUTE_VLC_MAPPING.SNo as  [Sequence No],TSPL_MCC_ROUTE_VLC_MAPPING.Distance,SUBSTRING( convert(varchar, TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll ,108),0,6) as [Morning Reaching Time],SUBSTRING( convert(varchar, TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll ,108),0,6) as [Evening Reaching Time] "
            End If
            Dim query As String = "select TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE AS [Route Code],TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE AS [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name] ,VSP_Code AS [VSP Code] ,vendor_name as [VSP Name],case when coalesce(Is_Active,0)=1 then 'Open' else 'Close' end As [Status] " + extraColumn + " From TSPL_MCC_ROUTE_VLC_MAPPING" & _
                  " LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE " & _
                  " left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=VSP_Code"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Milk Route Master")
        End Try

    End Sub

    Private Sub RadButton112_Click(sender As Object, e As EventArgs) Handles RadButton112.Click
        'Import
        Dim arrLoc As String = Nothing
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else

            End If
        Catch ex As Exception
        End Try

        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        Dim boolresult As Boolean = False
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnShow, clsFixedParameterCode.VLCTimeTableColumnShow, Nothing)) > 0, True, False) = True Then
            boolresult = transportSql.importExcel(gvCharges, "Route Code", "VLC Code", "VLC Name", "VSP Code", "VSP Name", "Status", "Sequence No", "Distance", "Morning Reaching Time", "Evening Reaching Time")
        Else
            boolresult = transportSql.importExcel(gvCharges, "Route Code", "VLC Code", "VLC Name", "VSP Code", "VSP Name", "Status")
        End If

        If boolresult Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Dim arrRoute As New List(Of String)
            Dim arrVlcCode As New List(Of String)
            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gvCharges.Rows

                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim RouteCode As String = clsCommon.myCstr(grow.Cells("Route Code").Value)
                    If clsCommon.myLen(RouteCode) >= 0 Then
                        RouteCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER Where Route_Code ='" + RouteCode + "'", trans))
                        If clsCommon.myLen(RouteCode) <= 0 Then
                            Throw New Exception("Route Code '" + RouteCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert route code at line no '" + LineNo + "' ")
                    End If
                    If Not arrRoute.Contains(RouteCode) Then
                        arrRoute.Add(RouteCode)
                    End If

                    Dim strVLCCode As String
                    Dim VLCCode As String = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                    If clsCommon.myLen(VLCCode) > 0 Then
                        strVLCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Code from TSPL_VLC_MASTER_HEAD Where VLC_Code ='" + VLCCode + "'", trans))
                        If clsCommon.myLen(strVLCCode) <= 0 Then
                            Throw New Exception("VLC Code '" + VLCCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please fill VLC code at line no '" + LineNo + "' ")
                    End If
                    Dim VLCName As String
                    ' Dim VehicleName As String
                    Dim VSPCode As String
                    Dim QueryVSPName As String

                    Dim MCCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Code From TSPL_MCC_ROUTE_MASTER Where Route_Code ='" & RouteCode & "'", trans))
                    Dim sQuery As String = "select vlc_code as Code,vlc_name as Name,Vehical_name as [Vehicle Name],VSP_Code as [VSP Code],vendor_name as [VSP Name]" & _
                                           " from TSPL_VLC_MASTER_HEAD inner join tspl_mcc_Master  on mcc=mcc_Code  left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=VSP_Code "
                    Dim whrcls As String = sQuery + " tspl_mcc_master.mcc_code in (" + arrLoc + ")"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        VLCName = clsCommon.myCstr(dt.Rows(0)("Name"))
                        VSPCode = clsCommon.myCstr(dt.Rows(0)("VSP Code"))
                        QueryVSPName = clsCommon.myCstr(dt.Rows(0)("VSP Name"))
                    End If

                    Dim Status As String = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Status").Value), "Open") = CompairStringResult.Equal, 1, 0)

                    If clsCommon.CompairString(Status, "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(Status, "0") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Status should be 1 at Line No '" + LineNo + "'")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Route_CODE", RouteCode)
                    clsCommon.AddColumnsForChange(coll, "VLC_CODE", VLCCode)
                    clsCommon.AddColumnsForChange(coll, "Is_Active", Status)

                    If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnShow, clsFixedParameterCode.VLCTimeTableColumnShow, trans)) > 0, True, False) = True Then
                        clsCommon.AddColumnsForChange(coll, "SNo", clsCommon.myCdbl(grow.Cells("Sequence No").Value))
                        clsCommon.AddColumnsForChange(coll, "Distance", clsCommon.myCdbl(grow.Cells("Distance").Value))
                        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnMandatory, clsFixedParameterCode.VLCTimeTableColumnMandatory, trans)) > 0, True, False) = True Then
                            If clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells("Sequence No").Value)) <= 0 Then
                                Throw New Exception("Please provide Sequence No of VLC " + VLCCode)
                            End If
                            If clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells("Distance").Value)) <= 0 Then
                                Throw New Exception("Please provide Distance of VLC " + VLCCode)
                            End If
                        End If
                        If clsCommon.myLen(grow.Cells("Morning Reaching Time").Value) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Mor_Mik_Coll", clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Morning Reaching Time").Value), "dd/MMM/yyyy hh:mm tt"))
                        ElseIf IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnMandatory, clsFixedParameterCode.VLCTimeTableColumnMandatory, trans)) > 0, True, False) = True Then
                            Throw New Exception("Please provide morning reaching time of VLC " + VLCCode)
                        End If

                        If clsCommon.myLen(grow.Cells("Evening Reaching Time").Value) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Eve_Milk_Coll", clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Evening Reaching Time").Value), "dd/MMM/yyyy hh:mm tt"))
                        ElseIf IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnMandatory, clsFixedParameterCode.VLCTimeTableColumnMandatory, trans)) > 0, True, False) = True Then
                            Throw New Exception("Please provide evening reaching time of VLC " + VLCCode)
                        End If
                    End If

                    '*********************************************************************************************************
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_MCC_ROUTE_VLC_MAPPING  set Is_Active = 0  where VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "' and Route_CODE <> '" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_MCC_ROUTE_VLC_MAPPING where Route_CODE = '" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "' and  VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update tspl_vlc_master_head set Route_Code=null where Route_Code='" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "'  and  VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "' ", trans)
                    '*********************************************************************************************************

                    sQuery = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & RouteCode & "' and vlc_code='" & VLCCode & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
                    sQuery = "update TSPL_MCC_ROUTE_VLC_MAPPING set is_active=0 where route_code<>'" & RouteCode & "' and vlc_code='" & VLCCode & "'"
                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    sQuery = "update TSPL_VLC_MASTER_HEAD set route_code='" & RouteCode & "' where vlc_code='" & VLCCode & "'"
                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    If check <= 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & RouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                    End If
                Next
                If isSaved Then
                    If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnMandatory, clsFixedParameterCode.VLCTimeTableColumnMandatory, trans)) > 0, True, False) Then
                        If Not (arrRoute Is Nothing) AndAlso arrRoute.Count > 0 Then
                            For Each str As String In arrRoute
                                clsfrmMilkRouteMaster.CheckSequenceOfVLC(str, trans)
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_MCC_ROUTE_MASTER set KiloMeter=(select sum(isnull(Distance,0)) as Distance  from TSPL_MCC_ROUTE_VLC_MAPPING where TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE=TSPL_MCC_ROUTE_MASTER.Route_CODE) where Route_CODE= '" + str + "'", trans)
                            Next
                        End If
                    End If
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully.")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gvCharges)
                arrRoute = Nothing
            End Try
        End If

    End Sub

    Private Sub RadButton114_Click(sender As Object, e As EventArgs) Handles RadButton114.Click
        Try
            MDI.ShowForm(clsUserMgtCode.FrmItemWiseTax, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton116_Click(sender As Object, e As EventArgs) Handles RadButton116.Click
        Try
            Me.Text = "Price Chart Master Bulk Details"
            Dim query As String = ""
            Dim IsItemMilkType As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
            Dim IsPriceChartGradeWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
            Dim AllowCreateBulkProcPriceChartItemWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
            If IsItemMilkType = 1 AndAlso AllowCreateBulkProcPriceChartItemWise = 1 Then
                query = " select tspl_bulk_price_detail_item_wise.Price_Code as [Price Code] , tspl_bulk_price_detail_item_wise.Item_code as [Item Code] , Fat_Weightage as [Fat Weightage], Snf_Weightage as [Snf Weightage], Fat_Percentage as [Fat Percentage], Snf_Percentage  as [Snf Percentage], Standard_Rate  as [Standard Rate], Tolerance  from tspl_bulk_price_detail_item_wise "
            ElseIf IsItemMilkType = 1 AndAlso IsPriceChartGradeWise = 1 Then
                query = " select tspl_bulk_price_detail.Price_Code as [Price Code], tspl_bulk_price_detail.Milk_Grade_code  as [Milk Grade Code], Fat_Weightage as [Fat Weightage], Snf_Weightage as [Snf Weightage] , Fat_Percentage as [Fat Percentage] , Snf_Percentage  as [Snf Percentage], Standard_Rate as [Standard Rate] , Tolerance from tspl_bulk_price_detail "
            Else
                clsCommon.MyMessageBoxShow("Details Part Only available for Price Chart Grade/Item Wise", Me.Text)
                Return
            End If
            ' "Price Code", "Item Code","Fat Weightage", "Snf Weightage", "Fat Percentage", "Snf Percentage", "Standard Rate", "Tolerance"
            transportSql.ExporttoExcel(query, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Chart Master Bulk Details")
        End Try
    End Sub
    ' Ticket No : TEC/30/07/19-000969 By Prabhakar 
    Private Sub RadButton115_Click(sender As Object, e As EventArgs) Handles RadButton115.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing
        Try
            Dim IsItemMilkType As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
            Dim IsPriceChartGradeWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
            Dim SettBulkProcurementApplyTotalSoidRate As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementApplyTotalSoidRate, clsFixedParameterCode.BulkProcurementApplyTotalSoidRate, Nothing)) > 0)
            Dim StandardRateWithZero As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing)) = 1, True, False)
            Dim AllowCreateBulkProcPriceChartItemWise As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
            Dim IsNewEntry As Boolean
            If IsItemMilkType = 1 And AllowCreateBulkProcPriceChartItemWise = 1 Then
                If transportSql.importExcel(gv, "Price Code", "Item Code", "Fat Weightage", "Snf Weightage", "Fat Percentage", "Snf Percentage", "Standard Rate", "Tolerance") Then
                    Dim linno As Integer = 0
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        For Each grow As GridViewRowInfo In gv.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clspriceCodeBulkProcDetailItemWise
                            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                            Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                            Dim dblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                            Dim dblSnfWeigtage As Double = clsCommon.myCdbl(grow.Cells("Snf Weightage").Value)
                            Dim dblFatPercetage As Double = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                            Dim dblSnfPercentage As Double = clsCommon.myCdbl(grow.Cells("Snf Percentage").Value)
                            Dim dblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                            Dim dblTolerance As Double = clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                            If clsCommon.myLen(strPriceCode) <= 0 Then
                                Throw New Exception("Price Code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsCommon.myLen(strItemCode) <= 0 Then
                                Throw New Exception("Item Code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            Dim isValidItemCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count (*) from TSPL_ITEM_MASTER where Item_Code = '" + strItemCode + "'", trans))
                            If isValidItemCode = False Then
                                Throw New Exception("Invalid Item Code  At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) <= 0 Then
                                Throw New Exception("Invalid Price Code At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' and IsPrice_ItemWise =1 ", trans) <= 0 Then
                                Throw New Exception("Price Code should be ItemWise At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsCommon.myCdbl(dblFatWeightage) <= 0 Then
                                Throw New Exception("Fat Weightage cannot be Zero or blank for Item Code " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblSnfWeigtage) <= 0 Then
                                Throw New Exception("SNF Weightage cannot be Zero or blank for Item Code " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            Dim totalW As Double = clsCommon.myCdbl(dblSnfWeigtage) + clsCommon.myCdbl(dblFatWeightage)
                            If totalW <> 100 Then
                                Throw New Exception("Total FAT and SNF Weightage must be 100 for Item Code " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblFatPercetage) <= 0 Then
                                Throw New Exception("Fat Percentage cannot be Zero or blank for Item Code " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblSnfPercentage) <= 0 Then
                                Throw New Exception("SNF Percentage cannot be Zero or blank for Grade " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If StandardRateWithZero = False Then
                                If clsCommon.myCdbl(dblStandardRate) <= 0 Then
                                    Throw New Exception("Standard Rate cannot be Zero or blank for Grade " + strItemCode + ". At Line No" + clsCommon.myCstr(linno))
                                End If
                            End If

                            IsNewEntry = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count (*) from tspl_bulk_price_detail_item_wise where Price_Code = '" + strPriceCode + "' and Item_code = '" + strItemCode + "'", trans))
                            Dim SNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) + 1 from tspl_bulk_price_detail_item_wise where Price_Code='" + strPriceCode + "'", trans))
                            Dim coll As New Hashtable()
                            

                            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", dblFatWeightage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", dblSnfWeigtage)
                            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", dblFatPercetage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", dblSnfPercentage)
                            clsCommon.AddColumnsForChange(coll, "Standard_Rate", dblStandardRate)
                            clsCommon.AddColumnsForChange(coll, "Tolerance", dblTolerance)
                            If IsNewEntry = False Then
                                clsCommon.AddColumnsForChange(coll, "Line_No", SNo)
                                clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(strPriceCode))
                                clsCommon.AddColumnsForChange(coll, "Item_code", clsCommon.myCstr(strItemCode))
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail_item_wise", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail_item_wise", OMInsertOrUpdate.Update, "tspl_bulk_price_detail_item_wise.Price_Code='" + clsCommon.myCstr(strPriceCode) + "' and Item_code = '" + clsCommon.myCstr(strItemCode) + "'", trans)
                            End If
                            linno += 1
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at row no" + clsCommon.myCstr(linno) + ex.Message)
                    End Try
                End If
            ElseIf IsItemMilkType = 1 And IsPriceChartGradeWise = 1 Then
                If transportSql.importExcel(gv, "Price Code", "Milk Grade Code", "Fat Weightage", "Snf Weightage", "Fat Percentage", "Snf Percentage", "Standard Rate", "Tolerance") Then
                    Dim linno As Integer = 0
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        For Each grow As GridViewRowInfo In gv.Rows
                            clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                            Dim obj As New clspriceCodeBulkProcDetailItemWise
                            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                            Dim strMilkGradCode As String = clsCommon.myCstr(grow.Cells("Milk Grade Code").Value)
                            Dim dblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                            Dim dblSnfWeigtage As Double = clsCommon.myCdbl(grow.Cells("Snf Weightage").Value)
                            Dim dblFatPercetage As Double = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                            Dim dblSnfPercentage As Double = clsCommon.myCdbl(grow.Cells("Snf Percentage").Value)
                            Dim dblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                            Dim dblTolerance As Double = clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                            If clsCommon.myLen(strPriceCode) <= 0 Then
                                Throw New Exception("Price Code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(strMilkGradCode) <= 0 Then
                                Throw New Exception("Milk Grad Code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            Dim isValidMilkGradCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*)  from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE = '" + strMilkGradCode + "'", trans))
                            If isValidMilkGradCode = False Then
                                Throw New Exception("Invalid Milk Grad Code  At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) <= 0 Then
                                Throw New Exception("Invalid Price Code At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' and IsPrice_GradeWise =1 ", trans) <= 0 Then
                                Throw New Exception("Price Code should be Grade wise At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If clsCommon.myCdbl(dblFatWeightage) <= 0 Then
                                Throw New Exception("Fat Weightage cannot be Zero or blank for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblSnfWeigtage) <= 0 Then
                                Throw New Exception("SNF Weightage cannot be Zero or blank for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            Dim totalW As Double = clsCommon.myCdbl(dblSnfWeigtage) + clsCommon.myCdbl(dblFatWeightage)
                            If totalW <> 100 Then
                                Throw New Exception("Total FAT and SNF Weightage must be 100 for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblFatPercetage) <= 0 Then
                                Throw New Exception("Fat Percentage cannot be Zero or blank for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If clsCommon.myCdbl(dblSnfPercentage) <= 0 Then
                                Throw New Exception("SNF Percentage cannot be Zero or blank for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                            End If
                            If StandardRateWithZero = False Then
                                If clsCommon.myCdbl(dblStandardRate) <= 0 Then
                                    Throw New Exception("Standard Rate cannot be Zero or blank for Milk Grade Code " + strMilkGradCode + ". At Line No" + clsCommon.myCstr(linno))
                                End If
                            End If
                            IsNewEntry = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count (*) from tspl_bulk_price_detail where Price_Code = '" + strPriceCode + "' and Milk_Grade_code = '" + strMilkGradCode + "'", trans))
                            Dim SNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) + 1 from tspl_bulk_price_detail where Price_Code='" + strPriceCode + "'", trans))
                            Dim coll As New Hashtable()
                           

                            clsCommon.AddColumnsForChange(coll, "Fat_Weightage", dblFatWeightage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Weightage", dblSnfWeigtage)
                            clsCommon.AddColumnsForChange(coll, "Fat_Percentage", dblFatPercetage)
                            clsCommon.AddColumnsForChange(coll, "Snf_Percentage", dblSnfPercentage)
                            clsCommon.AddColumnsForChange(coll, "Standard_Rate", dblStandardRate)
                            clsCommon.AddColumnsForChange(coll, "Tolerance", dblTolerance)
                            If IsNewEntry = False Then
                                clsCommon.AddColumnsForChange(coll, "Line_No", SNo)
                                clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(strPriceCode))
                                clsCommon.AddColumnsForChange(coll, "Milk_Grade_code", clsCommon.myCstr(strMilkGradCode))
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail", OMInsertOrUpdate.Update, "tspl_bulk_price_detail.Price_Code='" + clsCommon.myCstr(strPriceCode) + "' and Milk_Grade_code = '" + strMilkGradCode + "'", trans)
                            End If
                            linno += 1
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception("Error at row no" + clsCommon.myCstr(linno) + ex.Message)
                    End Try
                End If
            Else
                clsCommon.MyMessageBoxShow("Details Part Only available for Price Chart Grade/Item Wise (When setting ON)", Me.Text)
                Return
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Price Chart Master Bulk")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub btnExportMultipleMaster_Click(sender As Object, e As EventArgs) Handles btnExportMultipleMaster.Click
        Try
            Me.Text = "Upload Multiple Master "
            Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
            If settApplyEffectiveStartDate = True Then
                qry = "select '' as [MCC Code],'' as [MCC Name],'' as [Route Code],'' as [Route Name],'' as [Route Distance],'' as [Route Effective Start Date],'' as [Vehicle],'' as [Vehicle payment basis],'' as [Payment Per Km],'' as [Vehicle Effective Start Date],'' as [Transporter Code],'' as [Transporter Name],'' as [Transporter group code],'' as [VLC Code],'' as [VLC Name],'' as [VLC Uploader Code],'' as [Village Name],'' as [VSP Code],'' as [VSP Name],'' as [VSP Address],'' as [State],'' as [VSP Group Code],'' as [Create customer],'' as [Customer Group Code],'' as [VSP Payment type],'' as [Bank Code],'' as [Bank Name],'' as [IFSC Code],'' as [Branch Name],'' as [Account No],'' as [Buffalow TIP],'' as [Cow TIP]"
            Else
                qry = "select '' as [MCC Code],'' as [MCC Name],'' as [Route Code],'' as [Route Name],'' as [Route Distance],'' as [Vehicle],'' as [Vehicle payment basis],'' as [Payment Per Km],'' as [Transporter Code],'' as [Transporter Name],'' as [Transporter group code],'' as [VLC Code],'' as [VLC Name],'' as [VLC Uploader Code],'' as [Village Name],'' as [VSP Code],'' as [VSP Name],'' as [VSP Address],'' as [State],'' as [VSP Group Code],'' as [Create customer],'' as [Customer Group Code],'' as [VSP Payment type],'' as [Bank Code],'' as [Bank Name],'' as [IFSC Code],'' as [Branch Name],'' as [Account No],'' as [Buffalow TIP],'' as [Cow TIP]"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Parameter Range")
        End Try
    End Sub

    Private Sub btnImportMultipleMaster_Click(sender As Object, e As EventArgs) Handles btnImportMultipleMaster.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim LineNo As Integer = 0
        Try
            Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
            Dim Count As String = """"
            ''Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
            Dim inputs() As String = {}
            If settApplyEffectiveStartDate = True Then
                inputs = {"MCC Code", "MCC Name", "Route Code", "Route Name", "Route Distance", "Route Effective Start Date", "Vehicle", "Vehicle payment basis", "Payment Per Km", "Vehicle Effective Start Date", "Transporter Code", "Transporter Name", "Transporter group code", "VLC Code", "VLC Name", "VLC Uploader Code", "Village Name", "VSP Code", "VSP Name", "VSP Address", "State", "VSP Group Code", "Create customer", "Customer Group Code", "VSP Payment type", "Bank Code", "Bank Name", "IFSC Code", "Branch Name", "Account No", "Buffalow TIP", "Cow TIP"}
            Else
                inputs = {"MCC Code", "MCC Name", "Route Code", "Route Name", "Route Distance", "Vehicle", "Vehicle payment basis", "Payment Per Km", "Transporter Code", "Transporter Name", "Transporter group code", "VLC Code", "VLC Name", "VLC Uploader Code", "Village Name", "VSP Code", "VSP Name", "VSP Address", "State", "VSP Group Code", "Create customer", "Customer Group Code", "VSP Payment type", "Bank Code", "Bank Name", "IFSC Code", "Branch Name", "Account No", "Buffalow TIP", "Cow TIP"}
            End If

            Dim Strs As List(Of String) = New List(Of String)(inputs)
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    clsCommon.ProgressBarPercentShow()
                    Dim counter As Integer = 1
                    Dim IsBlacklisted As Integer = 0
                    If clsCommon.myLen(objCommonVar.BaseCurrencyCode) <= 0 Then
                        Throw New Exception("Please set currency code in company master")
                    End If
                    For Each grow As GridViewRowInfo In gv.Rows
                        clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                        LineNo += 1
                        trans = clsDBFuncationality.GetTransactin()
                        ''Primary Transport Master
                        Dim strvendorNo As String = String.Empty
                        Dim strvendorname1 As String = String.Empty
                        Dim strvendorname As String = String.Empty
                        Dim StrVdrNo As String = String.Empty
                        Dim check As Integer = 0
                        Dim i2 As Integer = 0
                        Dim sql1 As String

                        Dim coll As New Hashtable()
                        Dim strBrachName As String = String.Empty
                        Dim strIFSCCode As String = String.Empty
                        Dim strbank As String = String.Empty
                        Dim qry As String = Nothing
                        Dim statecode As String = String.Empty
                        Dim state As String = String.Empty
                        Dim closing_date As String = String.Empty
                        Dim strgroupCode As String = String.Empty
                        Dim strgroupDes As String = String.Empty
                        Try

                            strvendorNo = clsCommon.myCstr(grow.Cells("Transporter Code").Value)
                            If strvendorNo.Length > 12 Then
                                Throw New Exception("Check the length of Transporter Code,")
                            End If

                            strvendorname1 = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                            strvendorname = strvendorname1.Replace("'", "''")
                            If strvendorname.Length > 100 Then
                                Throw New Exception("Length of Transporter Name can not be greater than 100.,")
                            End If

                            If String.IsNullOrEmpty(strvendorname) Then
                                Throw New Exception("Transporter Name can not be blank")
                            End If
                            closing_date = System.DateTime.Now.Date

                            strgroupCode = clsCommon.myCstr(grow.Cells("Transporter group code").Value)
                            If String.IsNullOrEmpty(strgroupCode) Then
                                Throw New Exception(" Transporter group code can not be blank")
                            End If
                            Dim i As Integer
                            qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                            i = connectSql.RunScalar(trans, qry)
                            If i = 0 Then
                                Throw New Exception("Vendor group code does not exist : " + strgroupCode + "")
                            Else
                            End If
                            If strgroupCode.Length > 12 Then
                                Throw New Exception("Check the length of Group Code")
                            End If
                            strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                            statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            check = 0


                            If clsCommon.myLen(statecode) > 0 Then
                                qry = "select STATE_CODE,STATE_NAME from tspl_state_master where  state_code='" + statecode + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                End If
                                statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                            End If


                            strbank = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                            If strbank.Length > 30 Then
                                Throw New Exception("Check the length of bank code")
                            End If

                            strIFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)


                            strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                            If clsCommon.myLen(strBrachName) > 100 Then
                                Throw New Exception("Branch Name should be max 100 character")
                            End If


                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                            clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                            clsCommon.AddColumnsForChange(coll, "State", state)
                            clsCommon.AddColumnsForChange(coll, "form_type", "PTM")
                            clsCommon.AddColumnsForChange(coll, "state_code", statecode, True)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                            'clsCommon.AddColumnsForChange(coll, "branch_code", strIFSCCode)
                            'clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                            'clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(grow.Cells("Account No").Value))
                            'clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                            clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                            ' clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                            clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                            sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM' "
                            i2 = CInt(connectSql.RunScalar(trans, sql1))

                            StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM'", trans))

                            If (i2 = 0) Then
                                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='PTM'", trans)
                            End If
                            trans.Commit()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                        ''end of Primary Transporter Master

                        ''Primary Transporter Vehiclee Master
                        trans = Nothing
                        Dim obj As clsfrmPrimaryTransporterVehicalMaster

                        obj = New clsfrmPrimaryTransporterVehicalMaster()
                        Dim index As Integer = 0

                        obj.docno = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                        If clsCommon.myLen(obj.docno) <= 0 Then
                            Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 30 Then
                            Throw New Exception("Length of Vehicle No.(Code) Should Not Exceed 30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            index = obj.docno.IndexOf(" ")
                            If index > 0 AndAlso index < clsCommon.myLen(obj.docno) Then
                                Throw New Exception("There Should Be No white Space Between Vehicle No. At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "'", trans))
                        obj.primarycode = StrVdrNo
                        obj.primaryname = strvendorname
                        If clsCommon.myLen(obj.primarycode) <= 0 AndAlso clsCommon.myLen(obj.primaryname) <= 0 Then
                            Throw New Exception("Please Fill Primary Transporter Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.primarycode) > 0 Then
                            qry = "select count(*) from tspl_vendor_master where vendor_code='" + obj.primarycode + "' and form_type='PTM'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)

                            If index <= 0 Then
                                qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                                obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                    Throw New Exception("Filled Primary Transporter Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.primaryname) > 0 Then
                            qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                            obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                Throw New Exception("Filled Primary Transporter Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '-------------------------------------------------------------

                        obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value).Replace("'", "`")
                        If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)

                            If index <= 0 Then
                                qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                    Throw New Exception("Filled MCC Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        ElseIf clsCommon.myLen(obj.mccname) > 0 Then
                            qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                Throw New Exception("Filled MCC Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        '---------------------

                        '------------check primary transporter mapped with other mcc-----------------
                        Dim checkmcccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_code from tspl_primary_vehicle_master where vendor_code='" + obj.primarycode + "'", trans))
                        If clsCommon.myLen(checkmcccode) > 0 AndAlso clsCommon.CompairString(checkmcccode, obj.mcccode) <> CompairStringResult.Equal Then
                            Throw New Exception("Filled MCC Code/Name Is Invalid" + Environment.NewLine + "Primary Transporter Code Is Mapped With Other MCC Code i.e (" + checkmcccode + ") At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        '------------------------------------------------------------------------

                        obj.pricekm = clsCommon.myCdbl(grow.Cells("Payment Per Km").Value)


                        obj.status = clsCommon.myCstr(grow.Cells("Vehicle payment basis").Value)
                        If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                            'If obj.chagrshift <= 0 Then
                            '    Throw New Exception("Please Fill Charges per Day At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.avgrate <= 0 Then
                            '    Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.dieselrate <= 0 Then
                            '    Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                            'If obj.RentalAmount <= 0 Then
                            '    Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If Not (clsCommon.CompairString(obj.RentalType, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Year") = CompairStringResult.Equal) Then
                            '    Throw New Exception("Rental Type should be Day,Month,Year  At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                            'If obj.Price_Ltr_KG <= 0 Then
                            '    Throw New Exception("Please Fill Price Ltr/KG At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "KG") = CompairStringResult.Equal) Then
                            '    Throw New Exception("Rate Type should be LTR,KG  At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                            If obj.pricekm <= 0 Then
                                Throw New Exception("Please Fill Rate per KM At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                            'If obj.RentalAmount <= 0 Then
                            '    Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.avgrate <= 0 Then
                            '    Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                            'If obj.dieselrate <= 0 Then
                            '    Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                            'End If
                        ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                        ElseIf clsCommon.myLen(obj.status) > 0 Then
                            Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If settApplyEffectiveStartDate = True Then
                            If clsCommon.myLen(grow.Cells("Vehicle Effective Start Date").Value) <= 0 Then
                                Throw New Exception("Please Fill Vehicle Effective Start Date At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.Effective_Start_Date = clsCommon.GetPrintDate(grow.Cells("Vehicle Effective Start Date").Value, "dd/MMM/yyyy")
                        End If

                        qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.docno + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        Dim isNewEntry As Boolean = True
                        If check > 0 Then
                            isNewEntry = False
                        Else
                            isNewEntry = True
                        End If
                        If clsCommon.myLen(obj.docno) > 0 Then
                            'trans = clsDBFuncationality.GetTransactin()
                            If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                            Else
                                Throw New Exception("No Data Transfer")
                            End If
                        End If


                        ''----------- end of Primary Transporter Vehicle Master


                        '' Milk Route Master
                        trans = Nothing
                        Dim objMRM As clsfrmMilkRouteMaster
                        objMRM = New clsfrmMilkRouteMaster()
                        clsfrmMilkRouteMaster.arr_VLC_Detail = Nothing
                        objMRM.code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                        objMRM = clsfrmMilkRouteMaster.GetData(objMRM.code, Nothing, NavigatorType.Current)
                        If objMRM Is Nothing Then
                            objMRM = New clsfrmMilkRouteMaster
                        End If
                        objMRM.code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                        objMRM.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
                        If clsCommon.myLen(objMRM.desc) <= 0 Or clsCommon.myLen(objMRM.desc) > 150 Then
                            Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        check = 0
                        objMRM.vehiclecode = clsCommon.myCstr(grow.Cells("Vehicle").Value)

                        objMRM.Active = 1

                        If clsCommon.myLen(objMRM.vehiclecode) <= 0 Then
                            Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(objMRM.vehiclecode) > 0 Then
                            qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + objMRM.vehiclecode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)
                            If check <= 0 Then
                                Throw New Exception("Filled Vehicle Code Is Invalid Or Does Not Exist in Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        objMRM.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        objMRM.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                        If clsCommon.myLen(objMRM.mcccode) <= 0 AndAlso clsCommon.myLen(objMRM.mccname) <= 0 Then
                            Throw New Exception("Please Fill MCC Details At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(objMRM.mcccode) > 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_code='" + objMRM.mcccode + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                                check = clsDBFuncationality.getSingleValue(qry)

                                If check <= 0 Then
                                    objMRM.mcccode = ""
                                    Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                        End If
                        If clsCommon.myLen(objMRM.mcccode) <= 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        objMRM.kilometer = clsCommon.myCdbl(grow.Cells("Route Distance").Value)
                        If clsCommon.myLen(objMRM.kilometer) <= 0 Or clsCommon.myCdbl(objMRM.kilometer) <= 0 Then
                            Throw New Exception("Please Fill Route Distance And It Should Be Greater Than Zero(0) At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        qry = "select count(*) from tspl_mcc_route_master where route_Name='" + objMRM.desc + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If settApplyEffectiveStartDate = True Then
                            If clsCommon.myLen(grow.Cells("Route Effective Start Date").Value) <= 0 Then
                                Throw New Exception("Please Fill Route Effective Start Date At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            objMRM.Effective_Start_Date = clsCommon.GetPrintDate(grow.Cells("Route Effective Start Date").Value, "dd/MMM/yyyy")
                        End If

                        '' Dim isNewEntry As Boolean = True
                        If check <= 0 Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If

                        'trans = clsDBFuncationality.GetTransactin()
                        If clsfrmMilkRouteMaster.SaveData(objMRM.code, objMRM, isNewEntry, True) Then
                        Else
                            Throw New Exception("No Data Transfer")
                        End If
                        ''end of Milk Route master

                        ''VSP Master

                        trans = clsDBFuncationality.GetTransactin()
                        Try

                            strvendorNo = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                            If strvendorNo.Length > 12 Then
                                Throw New Exception("Check the length of VSP Code,")
                            End If


                            strvendorname1 = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                            strvendorname = strvendorname1.Replace("'", "''")
                            If strvendorname.Length > 100 Then
                                Throw New Exception("Length of VSP Name can not be greater than 100.,")
                            End If

                            If String.IsNullOrEmpty(strvendorname) Then
                                Throw New Exception("VSP Name can not be blank")
                            End If
                            Dim add1 As String = clsCommon.myCstr(grow.Cells("VSP Address").Value)
                            closing_date = System.DateTime.Now.Date

                            statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            check = 0


                            If clsCommon.myLen(statecode) > 0 Then
                                qry = "select STATE_CODE,STATE_NAME from tspl_state_master where  state_code='" + statecode + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                End If
                                statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                            End If

                            Dim vsppaymnt As String = clsCommon.myCstr(grow.Cells("VSP Payment type").Value).Replace("'", "`")
                            'Dim jointname As String = clsCommon.myCstr(grow.Cells("Joint_Name").Value).Replace("'", "`")

                            If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Self/Different in vsp payment at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                            Dim NameOfBank As String = ""
                            Dim AccountNo As String = ""


                            strbank = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                            If String.IsNullOrEmpty(strbank) Then
                                Throw New Exception("Bank Code can not be blank")
                            End If
                            Dim i5 As String
                            Dim EnableBankFromMaster As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, trans)) = 1, True, False)

                            If EnableBankFromMaster = True Then
                                Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                                i5 = connectSql.RunScalar(trans, qry7)
                                If i5 = 0 Then
                                    Throw New Exception("Bank code does not exist : " + strbank + "")
                                End If
                            End If

                            If strbank.Length > 30 Then
                                Throw New Exception("Check the length of bank code")
                            End If

                            Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account No").Value)
                            If clsCommon.myLen(strAccNo) > 50 Then
                                Throw New Exception("Account No. should be max 50 character.")
                            End If

                            Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                            If clsCommon.myLen(strBName) > 50 Then
                                Throw New Exception("Bank Name should be max 50 character.")
                            End If

                            strIFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                            If clsCommon.myLen(strIFSCCode) > 100 Then
                                Throw New Exception("IFSC Code should be max 100 character")
                            End If
                            strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                            If clsCommon.myLen(strBrachName) > 100 Then
                                Throw New Exception("Branch Name should be max 100 character")
                            End If
                            'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where  TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                            '    Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            'End If
                            ' ''-------------------------
                            strgroupCode = clsCommon.myCstr(grow.Cells("VSP Group Code").Value)
                            If String.IsNullOrEmpty(strgroupCode) Then
                                Throw New Exception("VSP Group Code can not be blank")
                            End If
                            Dim i As Integer
                            qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                            i = connectSql.RunScalar(trans, qry)
                            If i = 0 Then
                                Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                            Else
                            End If
                            If strgroupCode.Length > 12 Then
                                Throw New Exception("Check the length of VSP Group Code")
                            End If

                            strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                            clsCommon.AddColumnsForChange(coll, "add1", add1)
                            clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                            clsCommon.AddColumnsForChange(coll, "State", state)
                            clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                            clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                            clsCommon.AddColumnsForChange(coll, "vsp_payment", vsppaymnt)
                            clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName)
                            clsCommon.AddColumnsForChange(coll, "Account_No", strAccNo)
                            clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode)
                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                            clsCommon.AddColumnsForChange(coll, "Nature", "E")
                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                            clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                            clsCommon.AddColumnsForChange(coll, "Tip_Buffalo", clsCommon.myCdbl(grow.Cells("Buffalow TIP").Value))
                            clsCommon.AddColumnsForChange(coll, "Tip_Cow", clsCommon.myCdbl(grow.Cells("Cow TIP").Value))
                            clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='VSP'"
                            i2 = CInt(connectSql.RunScalar(trans, sql1))
                            StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='VSP'", trans))
                            If (i2 = 0) Then
                                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='VSP'", trans)
                            End If



                            '' End of VSP Master
                            ''create customer as VSP
                            Dim createCustomer = clsCommon.myCstr(grow.Cells("Create customer").Value)
                            If clsCommon.CompairString(createCustomer, "0") <> CompairStringResult.Equal And clsCommon.CompairString(createCustomer, "1") <> CompairStringResult.Equal Then
                                Throw New Exception("Please Fill Create customer And It Should Be 0 or 1 At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.CompairString(createCustomer, "1") = CompairStringResult.Equal Then
                                Dim objCustomer As New clsCustomerMaster()
                                objCustomer.Cust_Code = StrVdrNo
                                objCustomer.Customer_Name = strvendorname
                                objCustomer.Add1 = add1
                                objCustomer.State = statecode
                                objCustomer.CUSTOMER_FORM_TYPE = "VSP"
                                strgroupCode = clsCommon.myCstr(grow.Cells("Customer Group Code").Value)
                                If String.IsNullOrEmpty(strgroupCode) Then
                                    Throw New Exception("Customer Group Code can not be blank")
                                End If

                                qry = "select Count(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strgroupCode + "'"
                                i = connectSql.RunScalar(trans, qry)
                                If i = 0 Then
                                    Throw New Exception("Customer Group Code does not exist : " + strgroupCode + "")
                                Else
                                End If
                                If strgroupCode.Length > 12 Then
                                    Throw New Exception("Check the length of Customer Group Code")
                                End If

                                Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
                                  " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + strgroupCode + "' "
                                Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
                                If myDs.Tables(0).Rows.Count > 0 Then
                                    Dim row As DataRow = myDs.Tables(0).Rows(0)
                                    objCustomer.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                                    objCustomer.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                                    objCustomer.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                                    objCustomer.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
                                End If
                                objCustomer.Credit_Customer = "N"

                                objCustomer.LastInvoice_No = Nothing
                                objCustomer.LastInvoice_Date = Nothing
                                objCustomer.Inter_Branch = "N"

                                objCustomer.IsDistributor = "N"

                                objCustomer.prntcustyn = "N"

                                objCustomer.CSA_Type = "N"
                                objCustomer.ManualCustomer = "N"

                                objCustomer.Comp_Code = objCommonVar.CurrentCompanyCode

                                Dim arrDBName As New List(Of String)
                                arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

                                sql1 = "select count(*) from TSPL_CUSTOMER_MASTER where cust_code ='" + StrVdrNo + "' and CUSTOMER_FORM_TYPE='VSP'"
                                i2 = CInt(connectSql.RunScalar(trans, sql1))
                                If (i2 = 0) Then
                                    ' objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, arrDBName, trans)
                                    objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, trans)
                                Else
                                    ' objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, arrDBName, trans)
                                    objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, trans)
                                End If

                                'Customer Vendor mapping
                                Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + StrVdrNo + "'", trans)
                                If ii = 0 Then
                                    qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + StrVdrNo + "','" + StrVdrNo + "') "
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If


                            End If

                            ''-----create customer as VSP



                            '' Village Master
                            Dim objVillage As New clsfrmVillageMaster
                            ''objVillage.villcode = clsCommon.myCstr(grow.Cells("village_code").Value)

                            objVillage.villname = clsCommon.myCstr(grow.Cells("Village Name").Value)
                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            ' objVillage.citycode = clsCommon.myCstr(grow.Cells("city_code").Value)

                            objVillage.statecode = clsCommon.myCstr(grow.Cells("State").Value)
                            If clsCommon.myLen(objVillage.statecode) > 0 Then
                                qry = "select state_code from tspl_state_master where state_code='" + objVillage.statecode + "'"
                                objVillage.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                If clsCommon.myLen(objVillage.statecode) <= 0 Then
                                    Throw New Exception("First Create State Master(" + objVillage.statecode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If

                            isNewEntry = True
                            objVillage.villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Village_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + objVillage.villname + "'", trans))

                            If clsCommon.myLen(objVillage.villcode) > 0 Then

                                isNewEntry = False
                            End If
                            clsfrmVillageMaster.SaveData(objVillage, isNewEntry, trans)

                            '' End of Village MAster 

                            '' VLC Master


                            Dim mcccode As String = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                            If clsCommon.myLen(mcccode) <= 0 Then
                                Throw New Exception("Please Fill MCC Code At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            Dim VlcUploaderCode As String = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)


                            If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            Dim villcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ViLLage_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + clsCommon.myCstr(grow.Cells("village name").Value) + "'", trans))
                            Dim vspcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + clsCommon.myCstr(grow.Cells("VSP NAME").Value) + "' and Form_type='VSP'", trans))

                            Dim MilkRouteCode As String = clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER where route_name='" & clsCommon.myCstr(grow.Cells("Route Name").Value) & "' ", trans)

                            Dim isSaved As Boolean = True
                            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(grow.Cells("VLC Name").Value) + "'"
                            Dim VLCCode As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If clsCommon.myLen(VLCCode) <= 0 Then
                                VLCCode = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                            End If
                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)
                            clsCommon.AddColumnsForChange(coll, "vlc_name", clsCommon.myCstr(grow.Cells("VLC Name").Value))

                            clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                            clsCommon.AddColumnsForChange(coll, "village_code", villcode)

                            clsCommon.AddColumnsForChange(coll, "MCC", mcccode)
                            clsCommon.AddColumnsForChange(coll, "Route_Code", MilkRouteCode)

                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                            qry = "select count(VLC_Code) from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(grow.Cells("VLC Name").Value) + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                            If check <= 0 Then
                                clsCommon.AddColumnsForChange(coll, "Price_Code", Nothing, True)
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                                    VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(VLCCode)
                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                Else
                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                End If
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                            Else

                                clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + VLCCode + "'", trans)
                            End If
                            clsfrmVLCMaster.SaveVLCPriceCode(VLCCode, vspcode, mcccode, trans)



                            '' End Of VLC Master

                            ''MILK ROUTE VLC MAPPING DETAIL


                            qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)

                            coll = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Is_Active", 1)
                            clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)

                            If check <= 0 Then
                                clsCommon.AddColumnsForChange(coll, "route_code", MilkRouteCode)
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                            End If

                            ''END OF MILK ROUTE VLC MAPPING DETAIL

                            trans.Commit()

                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                        clsCommon.ProgressBarUpdate("Imported Records  : " & counter & "/" & gv.Rows.Count)
                    Next

                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Line: " + clsCommon.myCstr(LineNo) + " - " + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Upload Multiple Master")
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub
End Class