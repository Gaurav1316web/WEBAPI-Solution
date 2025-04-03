Imports System.Data.SqlClient
Imports common
Public Class FrmCreateBMCDCSbyMobile
    Inherits FrmMainTranScreen
    Private Sub FrmCreateBMCDCSbyMobile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtdate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnBlankSheetUploder_Click(sender As Object, e As EventArgs) Handles btnBlankSheetUploder.Click
        Try
            Dim qry As String = "select * from (
select TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID as [PKID], TSPL_MCC_MASTER.MCC_Code as [MCC],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name],TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code as [Route],TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route Name],TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No as [Vehicle No],TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No as [Trip No],TSPL_MILK_COLLECTION_BMCDCS_TRIP.Sample_No as [Sample No]
from TSPL_MILK_COLLECTION_BMCDCS_TRIP 
left outer join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
left outer join TSPL_BULK_ROUTE_MASTER on  TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID
where TSPL_MILK_COLLECTION_BMCDCS.IDate ='" + clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id is null 
)xx order by CAST ([MCC Code] as integer),[Sample No]"
            transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBlankSheetImportUploder_Click(sender As Object, e As EventArgs) Handles btnBlankSheetImportUploder.Click
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Try
            Dim qry As String = ""
            If transportSql.importExcel(gv, "PKID", "MCC", "MCC Code", "MCC Name", "Route", "Route Name", "Vehicle No", "Trip No", "Sample No") Then
                Dim ii As Integer = 0
                Dim Arr As New List(Of clsBMCDCS_Trip)
                Dim dtError As New DataTable
                dtError.Columns.Add("RowNo", GetType(Integer))
                dtError.Columns.Add("PKID", GetType(String))
                dtError.Columns.Add("Error", GetType(String))
                Try
                    If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                        clsCommon.ProgressBarPercentShow()
                        For Each grow As GridViewRowInfo In gv.Rows
                            Try
                                ii += 1
                                clsCommon.ProgressBarPercentUpdate(ii, gv.Rows.Count, "Validating Data...")
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PKID").Value)) <= 0 Then
                                    Throw New Exception("PKID can't be blank !")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Route").Value)) <= 0 Then
                                    Throw New Exception("Route can't be blank !")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Vehicle No").Value)) <= 0 Then
                                    Throw New Exception("Vehicle No can't be blank !")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Trip No").Value)) <= 0 Then
                                    Throw New Exception("Trip No can't be blank !")
                                End If

                                Dim obj As New clsBMCDCS_Trip
                                obj.PK_ID = clsCommon.myCstr(grow.Cells("PKID").Value)
                                qry = "select Document_No from TSPL_MILK_COLLECTION_MCC_DETAIL where REF_PK_ID_BMCDCS_TRIP='" + clsCommon.myCstr(obj.PK_ID) + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    Throw New Exception("PK ID [" + clsCommon.myCstr(grow.Cells("PKID").Value) + "] alreday used in BMC truck sheet No [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "]")
                                End If

                                obj.Route_Code = clsCommon.myCstr(grow.Cells("Route").Value)
                                obj.Route_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NO from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + obj.Route_Code + "'"))
                                If clsCommon.myLen(obj.Route_Code) <= 0 Then
                                    Throw New Exception("Invalid Route [" + clsCommon.myCstr(grow.Cells("Route").Value) + "]")
                                End If

                                obj.Vehicle_No = clsCommon.myCstr(grow.Cells("Vehicle No").Value)
                                obj.Vehicle_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No from TSPL_TANKER_MASTER where Tanker_No='" + obj.Vehicle_No + "'"))
                                If clsCommon.myLen(obj.Vehicle_No) <= 0 Then
                                    Throw New Exception("Invalid Vehicle No [" + clsCommon.myCstr(grow.Cells("Vehicle No").Value) + "]")
                                End If

                                obj.Trip_No = clsCommon.myCDecimal(grow.Cells("Trip No").Value)
                                If obj.Trip_No <= 0 Then
                                    Throw New Exception("Invalid Trip No [" + clsCommon.myCstr(grow.Cells("Trip No").Value) + "]")
                                End If

                                Arr.Add(obj)
                            Catch ex As Exception
                                Dim dr As DataRow = dtError.NewRow()
                                dr("RowNo") = ii
                                dr("PKID") = clsCommon.myCstr(grow.Cells("PKID").Value)
                                dr("Error") = ex.Message
                                dtError.Rows.Add(dr)
                            End Try
                        Next
                        clsCommon.ProgressBarPercentHide()
                    End If

                    Try
                        If dtError.Rows.Count > 0 Then
                            Dim ff As New FrmFreeGrid
                            ff.ReportID = "BMCDCSCorr"
                            ff.Text = "BMC DCS From Mobile Correction"
                            ff.dt = dtError
                            ff.ShowDialog()
                        ElseIf Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                            qry = "Valid Row [" + clsCommon.myCstr(Arr.Count) + "] Do You want to Proceed"
                            If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                clsCommon.ProgressBarPercentShow()
                                ii = 0
                                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                Try
                                    For Each obj As clsBMCDCS_Trip In Arr
                                        ii += 1
                                        clsCommon.ProgressBarPercentUpdate(ii, Arr.Count, "Saving Details..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Arr.Count) & "")
                                        Dim coll As New Hashtable()
                                        clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                                        clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
                                        clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_BMCDCS_TRIP", OMInsertOrUpdate.Update, "PK_ID='" + clsCommon.myCstr(obj.PK_ID) + "'", trans)

                                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.REF_PK_ID, "TSPL_MILK_COLLECTION_BMCDCS", "PK_ID", "TSPL_MILK_COLLECTION_BMCDCS_DCS", "REF_PK_ID", "TSPL_MILK_COLLECTION_BMCDCS_TRIP", "REF_PK_ID", trans)
                                    Next
                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw New Exception(ex.Message)
                                Finally
                                    clsCommon.ProgressBarPercentHide()
                                End Try
                                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                            End If
                        Else
                            Throw New Exception("No Valid Rows Found to Save")
                        End If
                    Catch ex As Exception
                        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                    End Try
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class