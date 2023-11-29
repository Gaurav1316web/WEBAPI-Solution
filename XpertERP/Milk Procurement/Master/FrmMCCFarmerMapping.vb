Imports System.Data.SqlClient
Imports common
Imports System.Text.RegularExpressions
'==========================shivani Tyagi against [BM00000008398]
Public Class FrmMCCFarmerMapping
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Is_Load As Boolean = False
    
    Public Sub VSPInformation()
        Dim qry As String

        gv1.DataSource = Nothing
        qry = "select Vendor_Code as [Code],Vendor_Name as [Name] from TSPL_VENDOR_MASTER "
        If clsCommon.myLen(FndMCC.Value) > 0 Then
            qry &= "  Left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.vendor_code and " _
                & " mcc = '" & FndMCC.Value & "' " _
                & " where ((Form_Type='VSP' and mcc = '" & FndMCC.Value & "'))"
            If clsCommon.myLen(fndRouteCode.Value) > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code ='" & fndRouteCode.Value & "' "
            End If

            If clsCommon.myLen(fndVLCCode.Value) > 0 Then
                qry += "  and TSPL_VLC_MASTER_HEAD.VLC_Code  ='" & fndVLCCode.Value & "' "
            End If
            cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgVSP.ValueMember = "Code"
            cbgVSP.DisplayMember = "Name"
            Refreshsub()
        Else
            qry &= " where 2=2 and  (Form_Type='VSP') "
            cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgVSP.ValueMember = "Code"
            cbgVSP.DisplayMember = "Name"
        End If

       


    End Sub
    Public Sub Refreshsub()
        'If (clsCommon.myLen(FndMCC.Value) <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("Please select MCC ")
        '    FndMCC.Focus()
        '    Return
        'End If

        Dim isget As Boolean = True
        Dim qry As String = "select Vendor_Code as [Code],Vendor_Name as [Name] from TSPL_VENDOR_MASTER " & _
                                    "  Left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.vendor_code and " & _
                                    " mcc = '" & FndMCC.Value & "' " & _
                                     " where ((Form_Type='VSP' and mcc = '" & FndMCC.Value & "')) and tspl_vendor_master.VSP_Farmer_Billing =1"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Dim arr As New ArrayList()
        For Each dr As DataRow In dt.Rows
            arr.Add(dr("Code").ToString())
        Next
        cbgVSP.CheckedValue = arr

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMCCFarmerMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag

    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmMCCFarmerMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim MP As String = ""
            Dim qry As String = "Update TSPL_MCC_MASTER set MCC_Farmer_Billing = 1 where MCC_Code='" & FndMCC.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '==========================first refresh data--------------------
            qry = "update tspl_vendor_master set VSP_Farmer_Billing = 0 where Form_Type='VSP' and Vendor_Code in (select VSP_Code from TSPL_VLC_MASTER_HEAD where mcc='" & FndMCC.Value & "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_MP_MASTER set MP_Farmer_Billing = 0 where VLC_Code in (SELECT VLC_CODE FROM TSPL_VLC_MASTER_HEAD WHERE VSP_CODE IN (" & clsCommon.GetMulcallString(cbgVSP.CheckedValue) & "))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '=======================================================================

            Dim qry1 As String = "update tspl_vendor_master set VSP_Farmer_Billing = 1 where Vendor_Code in (" & clsCommon.GetMulcallString(cbgVSP.CheckedValue) & ") "
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)


            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCBool(grow.Cells("Farmer Payment applied").Value) Then
                    Dim qry2 As String = "Update TSPL_MP_MASTER set MP_Farmer_Billing = 1 where MP_Code = '" + clsCommon.myCstr(grow.Cells("MP Code").Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry2, trans)
                End If
            Next


            '' check for vsps that have no single MP for Farmer payment 
            Dim VSP_Code As String = ""
            qry = " select TSPL_VLC_MASTER_HEAD.VSP_Code,sum(TSPL_MP_MASTER.MP_Farmer_Billing) as tot from  TSPL_MP_MASTER left join TSPL_VLC_MASTER_HEAD on TSPL_MP_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code" &
                  " where TSPL_MP_MASTER.VLC_CODE in (SELECT VLC_CODE FROM TSPL_VLC_MASTER_HEAD WHERE VSP_CODE IN (" & clsCommon.GetMulcallString(cbgVSP.CheckedValue) & ")) group by TSPL_VLC_MASTER_HEAD.VSP_Code having sum(TSPL_MP_MASTER.MP_Farmer_Billing)<=0"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dt.Rows.IndexOf(dr) = 0 Then
                        VSP_Code = clsCommon.myCstr(dr.Item("VSP_Code"))
                    Else
                        VSP_Code = VSP_Code & "," & clsCommon.myCstr(dr.Item("VSP_Code"))
                    End If
                Next
                If clsCommon.myLen(VSP_Code) > 0 Then
                    Throw New Exception("Please Map at least one MP for VSPs:" & VSP_Code)
                End If
            End If
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data Map Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub FrmMCCFarmerMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Is_Load = True
        VSPInformation()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        Is_Load = False
    End Sub
    Sub Reset()
        FndMCC.Value = ""
        fndRouteCode.Value = ""
        fndVLCCode.Value = ""
        lblRouteName.Text = ""
        lblVLCName.Text = ""
        lblMCCName.Text = ""
        gv1.DataSource = Nothing
        cbgVSP.DataSource = Nothing
        btnUnSelect.Text = "Select All"
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub LoadMP()
        Dim qry As String = "select cast(tspl_mp_master.MP_Farmer_Billing as bit) as [Farmer Payment applied], TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code],TSPL_VEndor_master.Vendor_Name as [VSP Name] ,TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code] ,VLC_Name as [VLC Name],tspl_mp_master.MP_Code as [MP Code] ,tspl_mp_master.MP_Name as [MP Name] from tspl_mp_master left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_mp_master.VLC_Code left join TSPL_VEndor_master on TSPL_VEndor_master.vendor_code=TSPL_VLC_MASTER_HEAD.vsp_code where form_type='VSP' and TSPL_VLC_MASTER_HEAD.vsp_code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dtgv
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 1 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True

            Next
            gv1.BestFitColumns()
        End If
    End Sub

   
    Private Sub FndMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndMCC._MYValidating
        Dim qry As String = " select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master "
        FndMCC.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MCC", qry, "Code", "", FndMCC.Value, "Code", isButtonClicked))

        If clsCommon.myLen(clsCommon.myCstr(FndMCC.Value)) > 0 Then
            lblMCCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where Mcc_Code='" & FndMCC.Value & "'"))
            VSPInformation()
            fndRouteCode.Value = ""
            fndVLCCode.Value = ""
            lblRouteName.Text = ""
            lblVLCName.Text = ""

        Else
            lblMCCName.Text = ""
            fndRouteCode.Value = ""
            fndVLCCode.Value = ""
            lblRouteName.Text = ""
            lblVLCName.Text = ""
           
           
        End If

    End Sub

    Private Sub cbgVSP__MyCheckChanged(sender As Object, e As EventArgs) Handles cbgVSP._MyCheckChanged
        If Is_Load = False Then
            If cbgVSP.CheckedValue.Count >= 1 And clsCommon.myLen(FndMCC.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Uncheck All Selected Mcc First.", Me.Text)
                Exit Sub
            End If
            gv1.DataSource = Nothing
            LoadMP()
        Else


        End If
    End Sub

    Private Sub FrmMCCFarmerMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim qry As String = "select TSPL_MP_MASTER.MP_Code ,TSPL_MP_MASTER.MP_Name ,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name ," & _
                            " tspl_vendor_master.Vendor_Code as VSP_Code,tspl_vendor_master.Vendor_Name as VSP_Name,TSPL_MCC_MASTER.MCC_Code ,TSPL_MCC_MASTER.MCC_NAME   from TSPL_MP_MASTER " & _
                            " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MP_MASTER.VLC_Code " & _
                            " left join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_VLC_MASTER_HEAD.VSP_Code and tspl_vendor_master.VSP_Farmer_Billing=1" & _
                            " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC and TSPL_MCC_MASTER. MCC_Farmer_Billing = 1" & _
                            " where  MP_Farmer_Billing=1 and TSPL_MCC_MASTER.MCC_Code ='" + strCode + "' "
       
    End Sub


    Private Sub fndRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(FndMCC.Value)) <= 0 Then
                Throw New Exception("Please select MCC ")
            End If
            Dim qry As String = "select Route_Code as [Code],Route_Name as [Name] from TSPL_MCC_ROUTE_MASTER  "
            fndRouteCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("Route", qry, "Code", " MCC_Code ='" & FndMCC.Value & "'", fndRouteCode.Value, "Code", isButtonClicked))

            If clsCommon.myLen(clsCommon.myCstr(fndRouteCode.Value)) > 0 Then
                lblRouteName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCode.Value & "'"))
                VSPInformation()
            Else
                lblVLCName.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            FndMCC.Focus()
        End Try
    End Sub

    Private Sub fndVLCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVLCCode._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(fndRouteCode.Value)) <= 0 Then
                Throw New Exception("Please select Route ")
            End If
            Dim qry As String = "select VLC_Code as [Code],VLC_Name as [Name] from TSPL_VLC_MASTER_HEAD  "
            fndVLCCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("VLC", qry, "Code", "Route_Code ='" & fndRouteCode.Value & "'", fndVLCCode.Value, "Code", isButtonClicked))

            If clsCommon.myLen(clsCommon.myCstr(fndVLCCode.Value)) > 0 Then
                lblVLCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndVLCCode.Value & "'"))
                VSPInformation()
            Else
                lblVLCName.Text = ""
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            fndRouteCode.Focus()
        End Try
            End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
End Class
