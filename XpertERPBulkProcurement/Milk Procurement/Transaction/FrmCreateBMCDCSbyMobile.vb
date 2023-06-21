Imports common
Public Class FrmCreateBMCDCSbyMobile
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = False
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim lstObj As New List(Of clsBMCDCSMobile)
            'Dim lstObj_DCS As New List(Of clsBMCDCS_DCS_Head)
            'Get MCC Truck Sheet Entered Data by Mobile
            For Each lst As clsBMCDCSMobile In clsBMCDCSMobile.GetData(txtdate.Value)
                lstObj.Add(lst)
            Next
            ' Add MCC Truck Sheet Entry
            For Each lst As clsBMCDCSMobile In lstObj
                isNewEntry = True
                BMCEntry(lst)
            Next
            ''Get BMCDCS_DCS Data
            'For Each lst1 As clsBMCDCS_DCS_Head In clsBMCDCS_DCS_Head.GetDCSData(txtdate.Value)
            '    lstObj_DCS.Add(lst1)
            'Next
            'For Each lst1 As clsBMCDCS_DCS_Head In lstObj_DCS

            '    isNewEntry = True
            '    DCSEntry(lst1)
            'Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub BMCEntry(ByRef lst As clsBMCDCSMobile)
        Try
            Dim obj As New clsMilkCollectionMCC()
            'obj.REF_PK_ID = lst.REF_PK_ID
            obj.Document_No = lst.Document_No
            obj.Document_Date = lst.Document_Date
            obj.Route_Code = lst.Route_Code
            obj.Tanker_No = lst.Tanker_No
            obj.Vehicle_No = lst.Vehicle_No
            obj.Trip_No = lst.Trip_No
            obj.Entered_Qty = lst.Entered_Qty
            obj.Entered_FATKg = lst.Entered_FATKg
            obj.Entered_SNFKg = lst.Entered_SNFKg
            obj.Description = "Uploaded By Mobile APP"
            obj.Arr = GetTRData(False, lst)
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                Throw New Exception("Please Fill at list one Item")
            End If
            obj.SaveData(obj, isNewEntry)
            'DCSEntry(lst)
            'clsCommon.MyMessageBoxShow(Me, "BMC Truck Sheet Data saved successfully", Me.Text)
            'LoadData(obj.Document_No, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetTRData(ByVal isMissingOnly As Boolean, ByRef lst As clsBMCDCSMobile) As List(Of clsMilkCollectionMCCDetail)
        Dim Arr As New List(Of clsMilkCollectionMCCDetail)
        For ii As Integer = 0 To lst.Arr_BMCDCS_Trip.Count - 1
            If clsCommon.myLen(lst.MCC_Code) > 0 Then
                If clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty) > 0 Then
                    Dim flag As Boolean = True
                    If clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).PK_ID) > 0 AndAlso isMissingOnly Then
                        flag = False
                    End If
                    If flag Then
                        Dim objTr As New clsMilkCollectionMCCDetail()
                        objTr.SNo = ii + 1
                        'objTr.Sample_No = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSampleNo).Value)
                        objTr.MCC_Code = clsCommon.myCstr(lst.Arr_BMCDCS_Trip(ii).MCC_Code)
                        objTr.Milk_Type = clsCommon.myCstr("Good")
                        objTr.Qty = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty)
                        objTr.FAT = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FAT), 2, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNF), 2, MidpointRounding.ToEven)
                        objTr.FATKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FATKG)
                        objTr.SNFKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNFKG)
                        objTr.Temp = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Temp)
                        objTr.REF_PK_ID_BMCDCS_TRIP = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).PK_ID)
                        'objTr.Gaze_Reading_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colGazeReadingCode).Value)
                        'objTr.Gaze_Reading = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colGazeReading).Value)
                        'objTr.Silo_Capacity = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMCCSiloCapacity).Value)
                        Arr.Add(objTr)
                    End If
                End If
            End If
        Next
        Return Arr
    End Function

    'Sub DCSEntry(ByRef lst As clsBMCDCSMobile)
    '    Try
    '        Dim obj_Dcs As New clsMilkCollectionDCS()
    '        obj_Dcs.Document_No = lst.Document_No
    '        obj_Dcs.Document_Date = lst.Document_Date
    '        obj_Dcs.Description = "Uploaded By Mobile App"
    '        'obj_Dcs.Slip_No = txtSlipNo.Text
    '        obj_Dcs.Arr = GetDCSTRData(False, lst)
    '        If (obj_Dcs.Arr Is Nothing OrElse obj_Dcs.Arr.Count <= 0) Then
    '            Throw New Exception("Please Fill at list one Item")
    '        End If
    '        obj_Dcs.ArrMCC = New List(Of clsMilkCollectionDCSMCCDetail)
    '        For ii As Integer = 0 To lst.Arr_BMCDCS_Trip.Count - 1
    '            Dim objtrMCC As New clsMilkCollectionDCSMCCDetail
    '            objtrMCC.Against_Milk_Collection_MCC_Detail = clsCommon.myCstr(lst.Arr_BMCDCS_Trip(ii).PK_ID) 'clsDBFuncationality.getSingleValue("select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id as PK_Id from TSPL_MILK_COLLECTION_MCC_DETAIL where REF_PK_ID_BMCDCS_TRIP=" + clsCommon.myCstr(lst.Arr_BMCDCS_Trip(ii).re)))
    '            obj_Dcs.ArrMCC.Add(objtrMCC)
    '        Next
    '        If (obj_Dcs.ArrMCC Is Nothing OrElse obj_Dcs.ArrMCC.Count <= 0) Then
    '            Throw New Exception("Please Fill at list one BMC Details")
    '        End If
    '        obj_Dcs.SaveData(obj_Dcs, isNewEntry)
    '        clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
    '        'LoadData(obj.Document_No, NavigatorType.Current)
    '        'End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    'Function GetDCSTRData(ByVal isMissingOnly As Boolean, ByRef lst As clsBMCDCSMobile) As List(Of clsMilkCollectionDCSDetail)
    '    Dim Arr As New List(Of clsMilkCollectionDCSDetail)
    '    For ii As Integer = 0 To lst.Arr_BMCDCS_DCS.Count - 1
    '        If clsCommon.myLen(lst.Arr_BMCDCS_DCS(ii).VLC_Code) > 0 Then
    '            Dim flag As Boolean = True
    '            If clsCommon.CompairString(lst.Arr_BMCDCS_DCS(ii).IShift, "E") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).Qty) > 0 Then
    '                'If clsCommon.myCDecimal(lst.Arr_BMCDCS_DCS(ii).REF_PK_ID) > 0 AndAlso isMissingOnly Then
    '                'flag = False
    '                'End If
    '                If flag Then
    '                    Dim objTr As New clsMilkCollectionDCSDetail()
    '                    objTr.SNo = ii + 1
    '                    objTr.VLC_Code = clsCommon.myCstr(lst.Arr_BMCDCS_DCS(ii).VLC_Code)
    '                    objTr.Shift = "E"
    '                    objTr.Milk_Type = clsCommon.myCstr("Good")
    '                    'objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocCollectionMilkType).Value)
    '                    objTr.Qty = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).Qty)
    '                    objTr.FAT = Math.Round(clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).FAT), 1, MidpointRounding.ToEven)
    '                    objTr.SNF = Math.Round(clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).SNF), 2, MidpointRounding.ToEven)
    '                    objTr.FATKG = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).FATKG)
    '                    objTr.SNFKG = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).SNFKG)
    '                    Dim intRejectApplicableOn As Integer = clsMilkRejectType.GetApplicableOn(objTr.Milk_Type, Nothing)
    '                    If intRejectApplicableOn <> 1 Then
    '                        If objTr.FAT <= 0 Then
    '                            Throw New Exception("FAT Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                        If objTr.SNF <= 0 Then
    '                            Throw New Exception("SNF Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                    End If
    '                    Arr.Add(objTr)
    '                End If
    '            End If
    '            flag = True
    '            If clsCommon.CompairString(lst.Arr_BMCDCS_DCS(ii).IShift, "M") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).Qty) > 0 Then
    '                'If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningPKID).Value) > 0 AndAlso isMissingOnly Then
    '                '    flag = False
    '                'End If
    '                If flag Then
    '                    Dim objTr As New clsMilkCollectionDCSDetail()
    '                    objTr.SNo = ii + 1
    '                    objTr.VLC_Code = clsCommon.myCstr(lst.Arr_BMCDCS_DCS(ii).VLC_Code)
    '                    objTr.Shift = "M"
    '                    objTr.Milk_Type = clsCommon.myCstr("Good")
    '                    'objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocCollectionMilkType).Value)
    '                    objTr.Qty = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).Qty)
    '                    objTr.FAT = Math.Round(clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).FAT), 1, MidpointRounding.ToEven)
    '                    objTr.SNF = Math.Round(clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).SNF), 2, MidpointRounding.ToEven)
    '                    objTr.FATKG = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).FATKG)
    '                    objTr.SNFKG = clsCommon.myCdbl(lst.Arr_BMCDCS_DCS(ii).SNFKG)
    '                    Dim intRejectApplicableOn As Integer = clsMilkRejectType.GetApplicableOn(objTr.Milk_Type, Nothing)
    '                    If intRejectApplicableOn <> 1 Then
    '                        If objTr.FAT <= 0 Then
    '                            Throw New Exception("FAT Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                        If objTr.SNF <= 0 Then
    '                            Throw New Exception("SNF Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                    End If
    '                    Arr.Add(objTr)
    '                End If
    '            End If
    '        End If
    '    Next
    '    Return Arr
    'End Function
End Class