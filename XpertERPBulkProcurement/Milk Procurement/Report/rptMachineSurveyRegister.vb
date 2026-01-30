Imports common
Public Class rptMachineSurveyRegister

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptMachineSurveyRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
    End Sub

    Sub EnableDisableFields(ByVal isBool As Boolean)
        'RadGroupBox1.Enabled = isBool
        'RadGroupBox2.Enabled = isBool
        'btngo.Enabled = isBool
    End Sub

    Sub Reset()
        BlankGrid()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            EnableDisableFields(True)
            Reset()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found !", Me.Text)
                Exit Sub
            End If

            Dim Qry As String = Nothing
            If rbtnDetail.Checked Then
                Qry = "Select * from(" & ReturnBaseQry() & ")finalQry"
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnBaseQry() As String
        Dim dt As DataTable = Nothing
        Dim arrUnion As New ArrayList()
        arrUnion.Add(objCommonVar.CurrComp_Code1)
        If objCommonVar.RCDFCFP Then
            dt = clsMilkUnion.UnionDBName()
        Else
            dt = clsMilkUnion.UnionDBName1(arrUnion)
        End If
        Dim Qry As String = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each strUnion In dt.Rows
                If i <> 0 Then
                    Qry &= " Union All "
                End If
                Qry = "Select Row_Number() Over (Order By (Select 1)) As [S.No.],'" & clsCommon.myCstr(strUnion("DataBase_Name")) & "' As [Union],TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME ,TSPL_ZONE_MASTER.Zone_Code ,TSPL_ZONE_MASTER.Description,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.IsAMCU,TSPL_VENDOR_MASTER.BrandName,TSPL_VENDOR_MASTER.IsWeighing,TSPL_VENDOR_MASTER.Weighing_BrandName
from " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_VLC_MASTER_HEAD
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code And TSPL_VLC_MASTER_HEAD.Active=1
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_ZONE_MASTER On TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code"
            Next
        End If
        Return Qry
    End Function

End Class