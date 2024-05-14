'-----------Created By Monika 29/05/2014
Imports common
Imports System.Data.SqlClient

Public Class FrmVLCUploader
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVLCUploader)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
            Me.Close()
            Exit Sub
        End If
    End Sub

    Sub Reset()
        fndvlc.Value = ""
        txtvlcname.Text = ""
        txtdate.Text = clsCommon.GETSERVERDATE()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub FrmVLCUploader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()

        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for Refersh Trasnaction")
        ButtonToolTip.SetToolTip(btnshow, "Press Alt+S Show Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        'RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        If clsCommon.myLen(fndvlc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select VLC Code/Name First", Me.Text)
            fndvlc.Focus()
            fndvlc.Select()
            Errorcontrol.SetError(fndvlc, "Please Select VLC Code/Name First")
            Return
        Else
            Errorcontrol.ResetError(fndvlc)
        End If

        If clsCommon.myLen(txtdate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Errorcontrol.SetError(txtdate, "Please Fill Date")
            Return
        Else
            Errorcontrol.ResetError(txtdate)
        End If

        Dim qry As String = "select TSPL_VLC_UPLOADER_MASTER.vlc_code as [VLC Code],TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_UPLOADER_MASTER.Date,TSPL_VLC_UPLOADER_MASTER.mp_code as [MP Code],TSPL_MP_MASTER.MP_name as [MP Name],TSPL_VLC_UPLOADER_MASTER.milk_qty as [Milk Qty],TSPL_VLC_UPLOADER_MASTER.fat_pers as [FAT%],TSPL_VLC_UPLOADER_MASTER.snf_pers as [SNF%],TSPL_VLC_UPLOADER_MASTER.fat_kg as [FAT In KG],TSPL_VLC_UPLOADER_MASTER.snf_kg as [SNF In KG],TSPL_VLC_UPLOADER_MASTER.Amount from TSPL_VLC_UPLOADER_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_UPLOADER_MASTER.vlc_code=TSPL_VLC_MASTER_HEAD.vlc_code left outer join TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_CODE=TSPL_VLC_UPLOADER_MASTER.MP_CODE where TSPL_VLC_UPLOADER_MASTER.vlc_code='" + fndvlc.Value + "' and TSPL_VLC_UPLOADER_MASTER.date='" + clsCommon.GetPrintDate(txtdate.Text, "MM/dd/yyyy") + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.DataSource = dt
            gv.AllowDeleteRow = True
            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
            gv.AllowColumnReorder = False
            gv.AllowRowReorder = False
            gv.EnableSorting = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.MasterTemplate.ShowRowHeaderColumn = False
            gv.ReadOnly = True

            gv.Columns("VLC Code").Width = 80
            gv.Columns("VLC Name").Width = 80
            gv.Columns("Date").Width = 80
            gv.Columns("MP Code").Width = 80
            gv.Columns("MP Name").Width = 80
            gv.Columns("Milk Qty").Width = 80
            gv.Columns("FAT%").Width = 80
            gv.Columns("SNF%").Width = 80
            gv.Columns("FAT In KG").Width = 80
            gv.Columns("SNF In KG").Width = 80
            gv.Columns("Amount").Width = 80
            UcAttachment1.LoadData(fndvlc.Value)
        Else
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            clsCommon.MyMessageBoxShow(Me, "No Data Found For Selected VLC And Date", Me.Text)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click

        Dim qry As String = "select count(*) from tspl_vlc_uploader_master"
        Dim check As Integer = CInt(clsDBFuncationality.getSingleValue(qry))

        If check > 0 Then
            qry = "select TSPL_VLC_UPLOADER_MASTER.mp_code as [MP Code],TSPL_MP_MASTER.MP_name as [MP Name],TSPL_VLC_UPLOADER_MASTER.milk_qty as [Milk Qty],TSPL_VLC_UPLOADER_MASTER.fat_pers as [FAT%],TSPL_VLC_UPLOADER_MASTER.snf_pers as [SNF%],TSPL_VLC_UPLOADER_MASTER.fat_kg as [FAT In KG],TSPL_VLC_UPLOADER_MASTER.snf_kg as [SNF In KG],TSPL_VLC_UPLOADER_MASTER.Amount from TSPL_VLC_UPLOADER_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_UPLOADER_MASTER.vlc_code=TSPL_VLC_MASTER_HEAD.vlc_code left outer join TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_CODE=TSPL_VLC_UPLOADER_MASTER.MP_CODE"
        Else
            qry = "select '' as [MP Code],'' as [MP Name],'' as [Milk Qty],'' as [FAT%],'' as [SNF%],'' as [FAT In KG],'' as [SNF In KG],'' as Amount"
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today

        If clsCommon.myLen(fndvlc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select VLC Code For Which The Data Is Being Uploaded/Showing", Me.Text)
            fndvlc.Focus()
            fndvlc.Select()
            Errorcontrol.SetError(fndvlc, "Please Select VLC Code For Which The Data Is Being Uploaded/Showing")
            Return
        Else
            Errorcontrol.ResetError(fndvlc)
        End If

        If clsCommon.myLen(txtdate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Date", Me.Text)
            txtdate.Focus()
            txtdate.Select()
            Errorcontrol.SetError(txtdate, "Please Fill Date")
            Return
        Else
            Errorcontrol.ResetError(txtdate)
        End If


        If transportSql.importExcel(gv1, "MP Code", "MP Name", "Milk Qty", "FAT%", "SNF%", "FAT In KG", "SNF In KG", "Amount") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim counter As Integer = 0
                Dim code As String = ""
                Dim name As String = ""
                Dim tdate As Date = Nothing
                Dim mpcode As String = ""
                Dim mpname As String = ""
                Dim qty As Decimal = Nothing
                Dim fat_pers As Decimal = Nothing
                Dim snf_pers As Decimal = Nothing
                Dim fat_kg As Decimal = Nothing
                Dim snf_kg As Decimal = Nothing
                Dim amt As Decimal = Nothing
                Dim qry As String = ""
                Dim check As Integer = 0


                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows
                    counter += 1

                    'code = clsCommon.myCstr(grow.Cells("vlc code").Value)
                    'name = clsCommon.myCstr(grow.Cells("vlc name").Value)
                    'If clsCommon.myLen(code) <= 0 And clsCommon.myLen(name) <= 0 Then
                    '    Throw New Exception("Please Fill VLC Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    'If clsCommon.myLen(code) > 0 Then
                    '    qry = "select count(*) from TSPL_VLC_MASTER_HEAD where vlc_code='" + code + "'"
                    '    check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                    '    If check <= 0 Then
                    '        Throw New Exception("First Create VLC Master Of VLC Code Exist At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'Else
                    '    Throw New Exception("Please Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    'Try
                    '    tdate = clsCommon.myCDate(grow.Cells("date").Value)
                    'Catch exx As Exception
                    '    Throw New Exception("Please Fill Date At Line No. " + clsCommon.myCstr(counter) + "")
                    'End Try

                    'If clsCommon.myLen(tdate) <= 0 Then
                    '    Throw New Exception("Please Fill Date At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    mpcode = clsCommon.myCstr(grow.Cells("mp code").Value)
                    mpname = clsCommon.myCstr(grow.Cells("mp name").Value)
                    If clsCommon.myLen(mpcode) <= 0 And clsCommon.myLen(mpname) <= 0 Then
                        Throw New Exception("Please Fill MP Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(mpcode) > 0 Then
                        qry = "select count(*) from TSPL_MP_MASTER where mp_code='" + mpcode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("First Create MP Master Of MP Code Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill MP Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Try
                        qty = clsCommon.myCdbl(grow.Cells("milk qty").Value)
                    Catch exx As Exception
                        qty = 0
                    End Try

                    Try
                        fat_pers = clsCommon.myCdbl(grow.Cells("fat%").Value)
                    Catch exx As Exception
                        fat_pers = 0
                    End Try

                    Try
                        snf_pers = clsCommon.myCdbl(grow.Cells("snf%").Value)
                    Catch exx As Exception
                        snf_pers = 0
                    End Try

                    Try
                        fat_kg = clsCommon.myCdbl(grow.Cells("fat in kg").Value)
                    Catch exx As Exception
                        fat_kg = 0
                    End Try

                    If fat_kg <= 0 Then
                        fat_kg = (qty * fat_pers) / 100
                    End If

                    Try
                        snf_kg = clsCommon.myCdbl(grow.Cells("snf in kg").Value)
                    Catch exx As Exception
                        snf_kg = 0
                    End Try


                    If snf_kg <= 0 Then
                        snf_kg = (qty * snf_pers) / 100
                    End If

                    Try
                        amt = clsCommon.myCdbl(grow.Cells("amount").Value)
                    Catch exx As Exception
                        amt = 0
                    End Try



                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "vlc_code", clsCommon.myCstr(fndvlc.Value))
                    clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(txtdate.Text, "MM/dd/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "mp_code", mpcode)
                    clsCommon.AddColumnsForChange(coll, "milk_qty", qty)
                    clsCommon.AddColumnsForChange(coll, "fat_pers", fat_pers)
                    clsCommon.AddColumnsForChange(coll, "snf_pers", snf_pers)
                    clsCommon.AddColumnsForChange(coll, "fat_kg", fat_kg)
                    clsCommon.AddColumnsForChange(coll, "snf_kg", snf_kg)
                    clsCommon.AddColumnsForChange(coll, "amount", amt)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                    Dim isSaved As Boolean = True

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next

                clsCommon.ProgressBarHide()
                UcAttachment1.SaveData(fndvlc.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Reset()
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub fndvlc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvlc._MYValidating
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.vlc_code as [Code],TSPL_VLC_MASTER_HEAD.vlc_name as [DCS Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehical Name],TSPL_VLC_MASTER_HEAD.vsp_code as [Secretary Code],TSPL_VENDOR_MASTER.Vendor_Name as [Secretary Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name],TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc"
        fndvlc.Value = clsCommon.ShowSelectForm("VLCFND4", qry, "Code", "", fndvlc.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndvlc.Value) > 0 Then
            txtvlcname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where vlc_code='" + fndvlc.Value + "'"))
        Else
            txtvlcname.Text = ""
        End If
    End Sub
End Class
