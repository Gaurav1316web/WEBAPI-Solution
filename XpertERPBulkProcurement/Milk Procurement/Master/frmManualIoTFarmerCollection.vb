Imports common
Imports System.Data.SqlClient
Public Class frmManualIoTFarmerCollection
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColManaulFarmerCollection As String = "Manaul Farmer Collection"
    Const ColReilIntegrated As String = "Reil Integrated"
    Const ColDCSUploaderCode As String = "DCS Uploader Code"
    Const colDCSCode As String = "DCS Code"
    Const ColDCSName As String = "DCS Name"
    Const ColBMCUploaderCode As String = "BMC Uploader Code"
    Const colBMCCode As String = "BMC Code"
    Const ColBMCName As String = "BMC Name"
    Public ThirtPartyFarmerCollectionIntegration As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub
    Private Sub frmManualIoTFarmerCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThirtPartyFarmerCollectionIntegration = clsCommon.myCBool(clsFixedParameter.GetData(clsFixedParameterType.ThirtPartyFarmerCollectionIntegration, clsFixedParameterCode.ThirtPartyFarmerCollectionIntegration, Nothing) > 0)
        SetUserMgmtNew()
        AddNew()
    End Sub
    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoManaulFarmerCollection As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoManaulFarmerCollection.HeaderText = "Manaul Farmer Collection"
        repoManaulFarmerCollection.Name = ColManaulFarmerCollection
        repoManaulFarmerCollection.ReadOnly = False
        repoManaulFarmerCollection.Width = 100
        repoManaulFarmerCollection.ReadOnly = False
        repoManaulFarmerCollection.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoManaulFarmerCollection)

        Dim repoCombobox = New GridViewComboBoxColumn()
        repoCombobox.FormatString = ""
        repoCombobox.HeaderText = "Reil Integrated"
        repoCombobox.Name = ColReilIntegrated
        repoCombobox.DataSource = clsDBFuncationality.GetDataTable("select 0 as Code,'Select' as Name union all select 1 as Code,'REIL' as Name union all select 2 as Code,'KTPL' as Name")
        repoCombobox.ValueMember = "Code"
        repoCombobox.DisplayMember = "Name"
        repoCombobox.IsVisible = ThirtPartyFarmerCollectionIntegration
        repoCombobox.ReadOnly = False
        repoCombobox.Width = 100
        repoCombobox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoCombobox)

        Dim TxtBoxCol As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Uploader Code"
        TxtBoxCol.Name = ColDCSUploaderCode
        TxtBoxCol.IsVisible = True
        TxtBoxCol.Width = 100
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Code"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = colDCSCode
        TxtBoxCol.IsVisible = False
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Name"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = ColDCSName
        TxtBoxCol.Width = 200
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)


        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "BMC Uploader Code"
        TxtBoxCol.Name = ColBMCUploaderCode
        TxtBoxCol.IsVisible = True
        TxtBoxCol.Width = 100
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "BMC Code"
        TxtBoxCol.Width = 100
        TxtBoxCol.Name = colBMCCode
        TxtBoxCol.IsVisible = False
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "BMC Name"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = ColBMCName
        TxtBoxCol.Width = 150
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)


        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.EnableFiltering = True
        gv1.EnableSorting = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = False
        gv1.BestFitColumns()
    End Sub
    Public Sub AddNew()
        'txtZone.arrValueMember = Nothing
        'txtZone.arrDispalyMember = Nothing
        LoadBlankGrid()
        RadGroupBox1.Enabled = True
    End Sub
    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = ""
            qry = "  select TSPL_ZONE_MASTER.Zone_Code as ZoneCode,TSPL_ZONE_MASTER.Description as [Zone Name] from TSPL_ZONE_MASTER "
            If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
                qry += " inner join TSPL_USER_CUSTOMER_ZONE On TSPL_USER_CUSTOMER_ZONE.User_Code = '" & objCommonVar.CurrentUserCode & "' and TSPL_USER_CUSTOMER_ZONE.Zone_Code =TSPL_ZONE_MASTER.Zone_Code "
            End If

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ITEMSMUL", qry, "ZoneCode", "Zone Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadGridData()
    End Sub
    Public Sub LoadGridData()
        Try
            Dim qry As String = "select isnull(TSPL_VLC_MASTER_HEAD.Manual_Farmer_Collection,0) as Manual_Farmer_Collection,case when ISNULL(TSPL_VLC_MASTER_HEAD.REIL_Integrated,0) = 0 then 'Select' when ISNULL(TSPL_VLC_MASTER_HEAD.REIL_Integrated,0) = 1 then 'REIL' when ISNULL(TSPL_VLC_MASTER_HEAD.REIL_Integrated,0) = 2 then 'KTPL' end as REIL_Integrated , VLC_Code_VLC_Uploader ,VLC_Code ,VLC_Name,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME  from TSPL_VLC_MASTER_HEAD
    inner join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code  inner join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC "

            If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
                qry += " inner join TSPL_USER_CUSTOMER_ZONE on TSPL_USER_CUSTOMER_ZONE.User_Code = '" & objCommonVar.CurrentUserCode & "' and TSPL_VENDOR_MASTER.ZONE_CODE= TSPL_USER_CUSTOMER_ZONE.ZONE_CODE "
            End If
            qry += " where 2=2 "
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                qry += " and TSPL_VENDOR_MASTER.Zone_Code in (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ") "
            End If
            qry += " order by cast(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as int) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                Dim sl As Integer = 1
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColManaulFarmerCollection).Value = clsCommon.myCdbl(dr("Manual_Farmer_Collection"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColManaulFarmerCollection).Tag = clsCommon.myCBool(dr("Manual_Farmer_Collection"))
                        If clsCommon.CompairString(clsCommon.myCstr(dr("REIL_Integrated")), "Select") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Tag = 0
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("REIL_Integrated")), "REIL") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Value = 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Tag = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("REIL_Integrated")), "KTPL") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Value = 2
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReilIntegrated).Tag = 2
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSUploaderCode).Value = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = clsCommon.myCstr(dr("VLC_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSName).Value = clsCommon.myCstr(dr("VLC_Name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBMCUploaderCode).Value = clsCommon.myCstr(dr("Mcc_Code_VLC_Uploader"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBMCCode).Value = clsCommon.myCstr(dr("MCC_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBMCName).Value = clsCommon.myCstr(dr("MCC_NAME"))
                        sl += 1
                    Next
                    RadGroupBox1.Enabled = False
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            Dim obj As New clsfrmVLCMaster()
            obj.arr = New List(Of clsfrmVLCMaster)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsfrmVLCMaster()
                objTr.vlcCode = clsCommon.myCstr(grow.Cells(colDCSCode).Value)
                objTr.Manual_Farmer_Collection = clsCommon.myCdbl(grow.Cells(ColManaulFarmerCollection).Value)
                objTr.REIL_Integrated = clsCommon.myCdbl(grow.Cells(ColReilIntegrated).Value)
                If (clsCommon.myCdbl(grow.Cells(ColManaulFarmerCollection).Tag) <> clsCommon.myCdbl(grow.Cells(ColManaulFarmerCollection).Value)) OrElse (clsCommon.myCdbl(grow.Cells(ColReilIntegrated).Tag) <> clsCommon.myCdbl(grow.Cells(ColReilIntegrated).Value)) Then
                    obj.arr.Add(objTr)
                End If
            Next
            If obj.arr.Count <= 0 Then
                Throw New Exception("Please update at least one DCS")
            End If
            obj.UpdateDCSManualFarmerCollection(obj.arr)
            clsCommon.MyMessageBoxShow(Me, "Data update successfully", Me.Text)
            LoadGridData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
End Class