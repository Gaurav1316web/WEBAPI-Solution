Imports common

Public Class VisiDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Dim query As String
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.visiDetail1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub VisiDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            chkdocAll1.IsChecked = True
        End If
    End Sub
    Private Sub VisiDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkdocAll1.IsChecked = True
        LoadVisi()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+P for Print ")


    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VISI-DT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Sub LoadVisi()
        query = "select Visi_Id as [VisiId],Customer_Id as [CustomerId] ,Customer_name as [CustomerName],Asset_no as [AssetNo],Visi_size as [VisiSize]  from TSPL_VISI_MASTER"
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(query)
        cbgdoc.ValueMember = "VisiId"
        cbgdoc.DisplayMember = "CustomerId"
    End Sub

    Private Sub chkdocAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll1.ToggleStateChanged, chkDoc_select1.ToggleStateChanged
        cbgdoc.Enabled = Not chkdocAll1.IsChecked
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        chkdocAll1.IsChecked = True
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        funPrint()
    End Sub
    Sub funPrint()
        'If chkdocAll1.IsChecked Then
        '    query = "select Visi_Id ,VisiMake ,Visi_Chasis_No ,Visi_Installation_date ,Customer_Id ,Customer_name ,Logo_Img ,Logo_Img2  from TSPL_VISI_MASTER left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_VISI_MASTER.Comp_Code "
        'ElseIf chkDoc_select1.IsChecked And cbgdoc.CheckedValue.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select atleast one visi")
        '    Exit Sub
        'Else
        '    query = "select Visi_Id ,VisiMake ,Visi_Chasis_No ,Visi_Installation_date ,Customer_Id ,Customer_name ,Logo_Img ,Logo_Img2  from TSPL_VISI_MASTER left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_VISI_MASTER.Comp_Code   where Visi_Id in (" + clsCommon.GetMulcallString(cbgdoc.CheckedValue) + ") "
        'End If
        If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Visi")
            Exit Sub
        End If

        Dim query As String = "select TSPL_VISI_MASTER.Visi_Id , TSPL_VISI_MASTER.VisiMake , TSPL_VISI_MASTER.Visi_Chasis_No , " & _
            " TSPL_VISI_MASTER.Visi_Installation_date , TSPL_VISI_MASTER.Customer_Id , TSPL_CUSTOMER_MASTER.Customer_Name , " & _
            " (TSPL_ROUTE_MASTER.Route_No+' - '+ Convert(Varchar, TSPL_ROUTE_MASTER.Route_Desc, 102)) as [Route] , Logo_Img , Logo_Img2  " & _
            " from TSPL_VISI_MASTER" & _
            " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_VISI_MASTER.Comp_Code " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER  on TSPL_VISI_MASTER.Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_ROUTE_MASTER On TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  Where 1=1 "

        If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count > 0 Then
            query += " AND TSPL_VISI_MASTER.Visi_Id in (" + clsCommon.GetMulcallString(cbgdoc.CheckedValue) + ")"
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "VisiDetail", "VisiDetail")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
End Class
