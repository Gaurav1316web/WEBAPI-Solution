'===============Created By Rohit
Imports common
Imports System.Data.SqlClient

Public Class FrmMCCMaterialSalePriceChart
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Private isInsideLoadData As Boolean = False
    Dim AllowPlandDeptMCCLocation As Boolean = False
#End Region

    Sub Reset()
        isNewEntry = True
        isInsideLoadData = False
        txtDocNo.Value = ""
        txtMCC_Code.arrValueMember = Nothing

        txtDocNo.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtdate.Text
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        LoadGrid()
        txtdate.Enabled = True
        txtEndDate.Enabled = True
        'UcAttachment1.Form_ID = Me.Form_ID
        'UcAttachment1.BlankAllControls()
    End Sub
    Sub LoadGrid()
        gv.Columns.Add("Item Code")
        gv.Columns.Add("Item Name")
        gv.Columns.Add("Unit Code")
        gv.Columns.Add("Unit Desc")
        gv.Columns.Add("Price")
        gv.Columns.Add("Issaved")

        gv.Columns("Item Code").Width = 100
        gv.Columns("Item Name").Width = 300
        gv.Columns("Item Name").ReadOnly = True
        gv.Columns("Unit Code").Width = 100
        gv.Columns("Unit Desc").Width = 100
        gv.Columns("Unit Desc").ReadOnly = True
        gv.Columns("Price").Width = 100
        gv.Columns("Issaved").Width = 0
        gv.Columns("Issaved").IsVisible = False

        gv.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
        'gv.Width = 80
        ' gv.ReadOnly = False
        gv.AllowAddNewRow = True
        gv.AllowDeleteRow = False

    End Sub


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMCCMaterialSalePriceChart)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub FrmMCCMaterialSalePriceChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AllowPlandDeptMCCLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_Plant_Depot_MCC_typeLocation, clsFixedParameterCode.Allow_Plant_Depot_MCC_typeLocation, Nothing)) = "1", True, False))
        If AllowPlandDeptMCCLocation Then
            Try
                Dim qry As String = clsERPFuncationality.GetForeignKey_Name("TSPL_MCC_RATE_UPLOADER_MCC", "MCC_Code")
                If clsCommon.myLen(qry) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MCC_RATE_UPLOADER_MCC drop constraint " + qry)
                End If
            Catch ex As Exception
            End Try
        End If
        Reset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnrefresh, "Press Alt+R for Refresh the Window")
        ButtonToolTip.SetToolTip(btnshow, "Press Alt+S  for Show Data In Grid")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed

    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        ' LoadData()
    End Sub



    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select tspl_item_master.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_UNIT_MASTER. unit_code as [Unit Code],TSPL_UNIT_MASTER. unit_desc as [Unit Desc],Price from tspl_item_master left join " _
        & " TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_MASTER .Unit_Code left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.item_code" _
        & " =TSPL_ITEM_MASTER.item_code and TSPL_MCC_RATE_UPLOADER_Detail.rate_uom=TSPL_UNIT_MASTER.Unit_Code "
        Dim whrcls As String = " and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type not in ('A') "
        transportSql.ExporttoExcel(qry, whrcls, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click

        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim columnsname As String = transportSql.GetExcelColumnsName(gv1)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMCCMaterailSalePriceChat()
                obj.Code = clsCommon.myCstr(txtDocNo.Value)
                obj.DOCDate = clsCommon.myCDate(txtdate.Value)
                obj.Effective_Date = clsCommon.myCDate(txtEndDate.Value)
                If txtMCC_Code.arrValueMember IsNot Nothing AndAlso txtMCC_Code.arrDispalyMember IsNot Nothing Then
                    obj.ArrMCCRate = txtMCC_Code.arrDispalyMember
                Else
                    obj.ArrMCCRate = Nothing
                End If
                obj.Arr = New List(Of clsMCCMaterailSalePriceChatDetail)
                For Each Grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(Grow.Cells("Item Code").Value) > 0 Then

                        Dim objTr As New clsMCCMaterailSalePriceChatDetail()
                        objTr.Item_Code = clsCommon.myCstr(Grow.Cells("Item Code").Value)
                        objTr.Rate_UOM = clsCommon.myCstr(Grow.Cells("Unit Code").Value)
                        objTr.Price = clsCommon.myCdbl(Grow.Cells("Price").Value)
                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtdate.Text) <= 0 Then
                txtdate.Focus()
                txtdate.Select()
                Throw New Exception("Please Fill Date")
            End If
            If clsCommon.myCDate(txtdate.Value) > clsCommon.myCDate(txtEndDate.Value) Then
                txtdate.Focus()
                txtdate.Select()
                Throw New Exception("Document Date will be Less then Effective Date")
            End If
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMCCMaterialSalePriceChart, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Throw New Exception("Invalid Password! ")

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMCCMaterailSalePriceChat()
                obj.Code = clsCommon.myCstr(txtDocNo.Value)
                obj.DOCDate = clsCommon.myCDate(txtdate.Value)
                obj.Effective_Date = clsCommon.myCDate(txtEndDate.Value)
                If txtMCC_Code.arrValueMember IsNot Nothing AndAlso txtMCC_Code.arrDispalyMember IsNot Nothing Then
                    obj.ArrMCCRate = txtMCC_Code.arrDispalyMember
                Else
                    obj.ArrMCCRate = Nothing
                End If
                obj.Arr = New List(Of clsMCCMaterailSalePriceChatDetail)
                Dim introw As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells("Item Code").Value) > 0 Then
                        Dim objTr As New clsMCCMaterailSalePriceChatDetail()
                        objTr.Item_Code = clsCommon.myCstr(gv.Rows(introw).Cells("Item Code").Value)
                        objTr.Rate_UOM = clsCommon.myCstr(gv.Rows(introw).Cells("Unit Code").Value)
                        objTr.Price = clsCommon.myCdbl(gv.Rows(introw).Cells("Price").Value)
                        introw += 1
                        obj.Arr.Add(objTr)
                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Save()
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True
                If e.Column Is gv.Columns("Item Code") Then
                    OpenICodeList(True)
                ElseIf e.Column Is gv.Columns("Unit Code") Then
                    OpenUOMList(True)
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry As String
        qry = "select * from (select TSPL_ITEM_MASTER.item_code as Item,TSPL_ITEM_MASTER.item_desc as [ItemDesc], TSPL_ITEM_MASTER.Unit_Code as Unit," _
        & " UOM_Description as [Unit Name]  from  TSPL_ITEM_MASTER  left join tspl_Item_Uom_Detail on tspl_Item_Uom_Detail.item_Code=" _
        & " TSPL_ITEM_MASTER.item_COde and stocking_unit='Y'   where  TSPL_ITEM_MASTER.Active=1 and (Item_Used_as='S' OR Item_Used_as='I' ) ) as s"
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
        '    qry += " OR Item_Used_as='I' "       'refer by ashok sir and done by stuti on 16/10/2016
        'End If
        '' KDIL > DATE : 19-01-2017
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
        '    qry += " OR Item_Used_as='I' "
        'End If

        'qry += ") ) as s"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MCCS@I", qry)
        If Not dr Is Nothing Then
            If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                gv.CurrentRow.Cells("Item Code").Value = clsCommon.myCstr(dr("Item"))
                gv.CurrentRow.Cells("Item Name").Value = clsCommon.myCstr(dr("ItemDesc"))
                gv.CurrentRow.Cells("Unit Code").Value = clsCommon.myCstr(dr("Unit"))
                gv.CurrentRow.Cells("Unit desc").Value = clsCommon.myCstr(dr("Unit Name"))
                gv.CurrentRow.Cells("IsSaved").Value = 0
            End If
        Else
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv.CurrentRow.Cells("Item Code").Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv.CurrentRow.Cells("Unit Code").Value = clsCommon.ShowSelectForm("Unit_Fndr", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells("Item Code").Value), "Code", isButtonClick)
            gv.CurrentRow.Cells("Unit desc").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_Unit_master where Unit_Code='" & gv.CurrentRow.Cells("Unit Code").Value & "'"))
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Price Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Code", "TSPL_MCC_RATE_UPLOADER_master", "TSPL_MCC_RATE_UPLOADER_Detail")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub



    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MCC_RATE_UPLOADER_master where Code='" + txtDocNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_MCC_RATE_UPLOADER_master.Code as DocumentCode,convert(varchar(12),TSPL_MCC_RATE_UPLOADER_master.Date,103) as DocumentDate from TSPL_MCC_RATE_UPLOADER_master "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentCode", "", txtDocNo.Value, "date DESC", isButtonClicked, " TSPL_MCC_RATE_UPLOADER_master.date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC_Code__My_Click(sender As Object, e As EventArgs) Handles txtMCC_Code._My_Click
        Try
            Dim qry As String = Nothing
            If AllowPlandDeptMCCLocation Then
                qry = "select Location_Code AS Code, Location_Desc as Name  from TSPL_LOCATION_MASTER where Is_Sub_Location = 'N' AND Location_Category <> 'MCC' and GIT_Type  <> 'Y' order by Code "
            Else
                qry = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER "
            End If
            txtMCC_Code.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneCodesearch", qry, "Code", "Code", txtMCC_Code.arrValueMember, txtMCC_Code.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsMCCMaterailSalePriceChat
            obj = clsMCCMaterailSalePriceChat.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                Reset()
                isNewEntry = False
                'LoadGrid()
                isInsideLoadData = True

                txtdate.Enabled = False
                txtEndDate.Enabled = False
                txtDocNo.Value = obj.Code
                txtdate.Value = obj.DOCDate
                txtEndDate.Value = obj.Effective_Date
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim intRow As Integer = 0

                    For Each Items As clsMCCMaterailSalePriceChatDetail In obj.Arr
                        gv.Rows.AddNew()
                        gv.Rows(intRow).Cells("Item Code").Value = Items.Item_Code
                        gv.Rows(intRow).Cells("Item Name").Value = Items.Item_Name
                        gv.Rows(intRow).Cells("Unit Code").Value = Items.Rate_UOM
                        gv.Rows(intRow).Cells("Unit Desc").Value = Items.Unit_DESC
                        gv.Rows(intRow).Cells("Price").Value = Items.Price
                        gv.Rows(intRow).Cells("Issaved").Value = Items.issaved
                        intRow += 1

                    Next
                End If
                If obj.ArrMCCRate IsNot Nothing AndAlso obj.ArrMCCRate.Count > 0 Then
                    txtMCC_Code.arrValueMember = obj.ArrMCCRate
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try


    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved

    End Sub
End Class
