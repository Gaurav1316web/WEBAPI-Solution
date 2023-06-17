Imports common

Public Class FrmCreateBMCDCSbyMobile
    Inherits FrmMainTranScreen

    Dim isNewEntry As Boolean = False
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try


            Dim lstObj As New List(Of clsBMCDCSMobile)
            For Each lst As clsBMCDCSMobile In clsBMCDCSMobile.GetData(txtdate.Value)
                lstObj.Add(lst)
            Next

            For Each lst As clsBMCDCSMobile In lstObj

                Dim qryStr As String = "select *
from TSPL_MILK_COLLECTION_MCC, TSPL_MILK_COLLECTION_BMCDCS where TSPL_MILK_COLLECTION_MCC.REF_PK_ID=" + clsCommon.myCstr(lst.REF_PK_ID)

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryStr)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    clsCommon.MyMessageBoxShow("Data found!")

                Else
                    isNewEntry = True
                    ' insert value in MCC & MCC_Details
                    Dim obj As New clsMilkCollectionMCC()
                    obj.REF_PK_ID = lst.REF_PK_ID
                    obj.Document_No = lst.Document_No
                    obj.Document_Date = lst.Document_Date
                    obj.Route_Code = lst.Route_Code
                    obj.Tanker_No = lst.Vehicle_No
                    obj.Vehicle_No = lst.Vehicle_No
                    obj.Trip_No = lst.Trip_No
                    obj.Entered_Qty = lst.Entered_Qty
                    obj.Entered_FATKg = lst.Entered_FATKg
                    obj.Entered_SNFKg = lst.Entered_SNFKg

                    obj.Arr = GetTRData(False, lst)
                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        Throw New Exception("Please Fill at list one Item")
                    End If
                    obj.SaveData(obj, isNewEntry)
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    'LoadData(obj.Document_No, NavigatorType.Current)
                End If

            Next

        Catch ex As Exception

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
                        objTr.MCC_Code = clsCommon.myCstr(lst.MCC_Code)
                        objTr.Milk_Type = clsCommon.myCstr("Good")
                        objTr.Qty = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty)
                        objTr.FAT = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FAT), 2, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNF), 2, MidpointRounding.ToEven)
                        objTr.FATKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FATKG)
                        objTr.SNFKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNFKG)
                        objTr.Temp = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Temp)
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
End Class