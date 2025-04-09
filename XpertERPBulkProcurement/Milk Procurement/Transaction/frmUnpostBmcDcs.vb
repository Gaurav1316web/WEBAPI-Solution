Imports System.ComponentModel
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmUnpostBmcDcs
    Inherits FrmMainTranScreen
    Const Check As String = "Check"

    Private Sub frmUnpostBmcDcs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate1.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtToDate1.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub

    Private Sub txtMultBMC__My_Click(sender As Object, e As EventArgs)
        Try
            'Dim Qry As String = clsMilkCollectionMCC.GetQuery(txtQCDate.Value, 3, True)
            Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"

            txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "BMC@", qry, "MCC_Code", "", txtMultBmc.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGridGv2()
        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Check").IsVisible = True
        gv1.Columns("Check").Width = 100
        gv1.Columns("Check").HeaderText = " Check"
        gv1.Columns("Check").ReadOnly = False

        gv1.Columns("PK_ID").IsVisible = True
        gv1.Columns("PK_ID").Width = 80
        gv1.Columns("PK_ID").HeaderText = "PK ID"

        gv1.Columns("Status").IsVisible = False
        gv1.Columns("Status").Width = 80
        gv1.Columns("Status").HeaderText = "Status"

        gv1.Columns("MCC_Code").IsVisible = False
        gv1.Columns("MCC_Code").Width = 100
        gv1.Columns("MCC_Code").HeaderText = " MCC Code"

        gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
        gv1.Columns("Mcc_Code_VLC_Uploader").Width = 120
        gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = " Mcc Uploader Code"

        gv1.Columns("MCC_NAME").IsVisible = True
        gv1.Columns("MCC_NAME").Width = 100
        gv1.Columns("MCC_NAME").HeaderText = "Mcc Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv1.ShowGroupPanel = False
        gv1.AllowAddNewRow = False
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim sQuery As String = Nothing
            Dim WhrCls As String = " and 2=2 "
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            sQuery = " select Cast(0 as BIT) as 'Check',  TSPL_MILK_COLLECTION_BMCDCS.Status, TSPL_MILK_COLLECTION_BMCDCS.PK_ID, TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,Mcc_Code_VLC_Uploader,MCC_NAME from TSPL_MILK_COLLECTION_BMCDCS
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
left outer join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID
where 2=2 and TSPL_MILK_COLLECTION_BMCDCS.Status='1' and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id is null
and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103)>=convert(date,'" + clsCommon.GetPrintDate(txtToDate1.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103) <=convert(date,'" + clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MMM/yyyy") + "' ,103) "

            If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_MILK_COLLECTION_BMCDCS.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ") "
            End If

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGridGv2()
                btnPost.Visible = True
            Else
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs)
        Unpost()
    End Sub
    Public Sub LoadData(ByVal PKIDNO As String, ByVal NavTyep As NavigatorType)
        Try
            If Not String.IsNullOrEmpty(PKIDNO) Then
                Dim qrypi As String = ""
                Dim dt1 As New DataTable()
                qrypi = " select Cast(0 as BIT) as 'Check',  TSPL_MILK_COLLECTION_BMCDCS.Status, TSPL_MILK_COLLECTION_BMCDCS.PK_ID, TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,Mcc_Code_VLC_Uploader,MCC_NAME from TSPL_MILK_COLLECTION_BMCDCS
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
left outer join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
where 2=2 and TSPL_MILK_COLLECTION_BMCDCS.Status='1' and and TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID is null
and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103)>=convert(date,'" + clsCommon.GetPrintDate(txtToDate1.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103) <=convert(date,'" + clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MMM/yyyy") + "' ,103) "

                If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                    qrypi += " and TSPL_MILK_COLLECTION_BMCDCS.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ") "
                End If

                If clsCommon.myLen(qrypi) > 0 Then
                    dt1 = clsDBFuncationality.GetDataTable(qrypi)
                End If

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.GroupDescriptors.Clear()
                    gv1.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt1
                    gv1.BestFitColumns()

                    For Each row As DataRow In dt1.Rows
                        'Gv2.Rows.AddNew()
                        'gv1.Rows(gv1.Rows.Count - 1).Cells("PK_ID").Value = False
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells("PK_ID").Value = clsCommon.myCstr(row("PK_ID"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells("MCC_Code").Value = clsCommon.myCstr(row("MCC_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells("Mcc_Code_VLC_Uploader").Value = clsCommon.myCstr(row("Mcc_Code_VLC_Uploader"))
                    Next
                    FormatGridGv2()
                Else
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Unpost(Optional ByVal DocNo As String = "")
        Dim ii As Integer = 1
        Dim Total As Integer = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                Total += 1
            End If
        Next
        Dim Qry As String = Nothing
        Dim listPkId As New List(Of String)
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                listPkId.Add(clsCommon.myCstr(grow.Cells("PK_ID").Value))
                ii += 1
            End If
        Next
        Try
            If listPkId.Count <= 0 Then
                myMessages.blankValue(Me, "PK ID not found", Me.Text)
            Else
                Dim PKNO As String = clsCommon.GetMulcallString(listPkId)
                Dim StrSql As String = "select Status from TSPL_MILK_COLLECTION_BMCDCS where PK_ID in(" + PKNO + ")"
                StrSql = "select 1 from TSPL_MILK_COLLECTION_BMCDCS_TRIP inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_trip.PK_ID where TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID in (" + PKNO + ")"

            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub txtMultBmc__My_Click_2(sender As Object, e As EventArgs) Handles txtMultBmc._My_Click
        Try
            Dim qry As String = "select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Uploader Code] ,TSPL_MCC_MASTER.MCC_NAME as Name
from TSPL_MCC_MASTER "
            txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm(True, "BMC@UnpoD", qry, "Code", "", txtMultBmc.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click_1(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            Dim ii As Integer = 1
            Dim Total As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    Total += 1
                End If
            Next
            Dim Qry As String = Nothing
            Dim objMultPrintInvoice As New FrmPrintFreshInvoice
            Dim Pkid As New List(Of String)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    Pkid.Add(clsCommon.myCstr(grow.Cells("PK_ID").Value))
                    ii += 1
                End If
                'ii += 1
            Next
            If Pkid.Count <= 0 Then
                myMessages.blankValue(Me, "PK ID not found", Me.Text)
            Else
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                Dim PKIDNO As String = clsCommon.GetMulcallString(Pkid)
                Dim StrSql As String = "select Status,PK_ID from TSPL_MILK_COLLECTION_BMCDCS where PK_ID in(" + PKIDNO + ")"


                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    ReverseAndUnpost(PKIDNO)
                    'clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)


                    Dim sqlload As String = Nothing
                    sqlload = " select Cast(0 as BIT) as 'Check',  TSPL_MILK_COLLECTION_BMCDCS.Status, TSPL_MILK_COLLECTION_BMCDCS.PK_ID, TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,Mcc_Code_VLC_Uploader,MCC_NAME from TSPL_MILK_COLLECTION_BMCDCS
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
left outer join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID
where 2=2 and TSPL_MILK_COLLECTION_BMCDCS.Status='1' and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id is null
and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103)>=convert(date,'" + clsCommon.GetPrintDate(txtToDate1.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_MILK_COLLECTION_BMCDCS.IDate,103) <=convert(date,'" + clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MMM/yyyy") + "' ,103) "

                    If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                        sqlload += " and TSPL_MILK_COLLECTION_BMCDCS.MCC_Code in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ") "
                    End If
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sqlload)
                    ' If dt2 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    'gv1.Rows.Clear()
                    '    gv1.Columns.Clear()
                    gv1.DataSource = Nothing
                    gv1.GroupDescriptors.Clear()
                    gv1.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt2
                    gv1.BestFitColumns()
                    FormatGridGv2()
                    'End If
                    ' LoadData(PKIDNO, NavigatorType.Current)

                    'gv1.DataSource = Nothing
                    'gv1.Rows.Clear()
                    'gv1.Columns.Clear()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Public Function ReverseAndUnpost(ByVal PKIDNO As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try

            Dim Qry As String = "select 1 from TSPL_MILK_COLLECTION_BMCDCS_TRIP inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_trip.PK_ID where TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID in (" + PKIDNO + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Document is used you cannot unpost it")
            End If

            Qry = clsCommon.myCstr("select Status from TSPL_MILK_COLLECTION_BMCDCS where PK_ID  in(" + PKIDNO + ")")
            dt = clsDBFuncationality.GetDataTable(Qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Not clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 Then
                    Throw New Exception("Transaction status should be posted for reverse and unpost")
                End If
                Qry = "Update TSPL_MILK_COLLECTION_BMCDCS set Status = 0 where PK_ID  in(" + PKIDNO + ")"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, PKIDNO, "TSPL_MILK_COLLECTION_BMCDCS", "PK_ID", trans)

                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly ", Me.Text)
            Else
                Throw New Exception("Document No [" + PKIDNO + "] not found for reverse and unpost")
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtMultBmc.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


End Class