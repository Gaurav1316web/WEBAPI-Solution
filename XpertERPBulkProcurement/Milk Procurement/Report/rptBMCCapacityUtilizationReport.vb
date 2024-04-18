Imports common

Public Class rptBMCCapacityUtilizationReport
    Private Sub rptBMCCapacityUtilizationReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select VSP_Code as DCSCode,TSPL_VENDOR_MASTER.Vendor_Name as DCSName,VLC_Code_VLC_Uploader as UploaderNo,VLC_Code as VLCCode,VLC_Name as VLCNAme from TSPL_VENDOR_MASTER
                                 inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where TSPL_VLC_MASTER_HEAD.isOwnBMC = 1"
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "BMCCapacityUtilization", qry, "DCSCode", "", txtBMC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = " select TSPL_ZONE_MASTER.Zone_Code as Code, TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code "

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "BMCCapacityUtilization", qry, "Code", "", txtZone.arrValueMember, Nothing)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class