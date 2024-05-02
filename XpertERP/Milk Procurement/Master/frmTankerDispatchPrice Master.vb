Imports System.Data.SqlClient
Imports common

' Ticket No : ERO/07/05/18-000293  By Prabhakar Anand
Public Class FrmTankerDispatchPrice_Master
    Inherits FrmMainTranScreen
    Public isInsideLoadData As Boolean = False
    Private isNewEntry As Boolean = False

    Private Sub FrmTankerDispatchPrice_Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Function AllowToSave() As Boolean
        If String.IsNullOrEmpty(txtrate.Text) Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Total Solid  Rate", Me.Text)
            txtrate.Focus()
            Return False
        End If
        If chkJobWork.Checked = True Then
            If clsCommon.myLen(txtItemCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
                txtItemCode.Focus()
                Return False
            End If
        End If
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            Return True
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Please Select atleast one Mcc.", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master "
        If chkJobWork.Checked = True Then
            qry = " select TSPL_LOCATION_MASTER.Location_Code as [Code] ,case when len (TSPL_LOCATION_MASTER.Location_Desc) > 0 then  TSPL_LOCATION_MASTER.Location_Desc else TSPL_LOCATION_MASTER.Loc_Short_Name end  as [Name] from TSPL_LOCATION_MASTER where location_type = 'Physical' "
        End If
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("Mcc@MultSel", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                isNewEntry = False
                isInsideLoadData = True
                btnsave.Text = "Update"

                Dim obj As New clsTankerDispatchPriceMaster()

                obj.PRICE_CODE = fndcode.Value
                obj.PRICE_DESC = txtname.Text
                obj.EFFECTIVE_DATE = txtefctdate.Value
                obj.TOTAL_SOLID_RATE = txtrate.Value
                obj.isJobWork = chkJobWork.Checked
                If chkJobWork.Checked = True Then
                    obj.Item_Code = txtItemCode.Value
                End If
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                End If

                obj.ArrMccCode = New List(Of clsTankerDispatchPriceDetails)
                If clsCommon.myLen(txtMCC.arrValueMember) > 0 Then
                    For Each strMccCode As String In txtMCC.arrValueMember
                        Dim objTrTr As New clsTankerDispatchPriceDetails()
                        objTrTr.PRICE_CODE = clsCommon.myCstr(fndcode.Value)
                        objTrTr.MCC_CODE = strMccCode
                        If (clsCommon.myLen(objTrTr.MCC_CODE) > 0) Then
                            obj.ArrMccCode.Add(objTrTr)
                        End If
                    Next
                End If
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count (*) from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE = '" + obj.PRICE_CODE + "'"))
                If count <= 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsTankerDispatchPriceMaster.SaveData(isNewEntry, obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.PRICE_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            'If clsCommon.myLen(strCode) > 0 Then
            '    Return
            'End If
            Dim obj As New clsTankerDispatchPriceMaster()
            obj = clsTankerDispatchPriceMaster.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PRICE_CODE) > 0) Then
                isInsideLoadData = True
                btnsave.Text = "Update"
                fndcode.Value = obj.PRICE_CODE
                txtname.Text = obj.PRICE_DESC
                txtefctdate.Value = obj.EFFECTIVE_DATE
                txtrate.Value = obj.TOTAL_SOLID_RATE
                chkJobWork.Checked = obj.isJobWork
                txtItemCode.Value = obj.Item_Code
                lblItemCode.Text = clsItemMaster.GetItemName(txtItemCode.Value, Nothing)
                If obj.isJobWork = True Then
                    penalItemCode.Visible = True
                Else
                    penalItemCode.Visible = False
                End If
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    chkJobWork.Enabled = False
                Else
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                    btndelete.Enabled = True
                    chkJobWork.Enabled = True
                End If
                UsLock1.Status = obj.Posted
                Dim DocCode As New ArrayList
                If obj.ArrMccCode IsNot Nothing AndAlso obj.ArrMccCode.Count > 0 Then
                    For Each ob As clsTankerDispatchPriceDetails In obj.ArrMccCode
                        DocCode.Add(ob.MCC_CODE)
                    Next
                    txtMCC.arrValueMember = DocCode
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                If (AllowToSave()) Then
                    SaveData()
                    If (clsTankerDispatchPriceMaster.PostData(fndcode.Value)) Then
                        msg = "Successfully Posted"
                    End If
                    If clsCommon.myLen(msg) > 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    End If
                    LoadData(fndcode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then

                If (clsTankerDispatchPriceMaster.DeleteData(fndcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Sub Reset()
        fndcode.Value = ""
        txtname.Text = ""
        txtMCC.arrValueMember = Nothing
        txtefctdate.Value = clsCommon.GETSERVERDATE
        txtrate.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        chkJobWork.Checked = False
        txtItemCode.Enabled = False
        txtItemCode.Value = Nothing
        lblItemCode.Text = Nothing
        penalItemCode.Visible = False
        chkJobWork.Enabled = True
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcode._MYValidating
        Dim qry As String = "select * from TSPL_TANKER_DISPATCH_PRICE_MASTER"

        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("SRGEDOCFINDER", qry, "PRICE_CODE", whrClas, fndcode.Value, "TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE", isButtonClicked, "TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_DATE"), NavigatorType.Current)
    End Sub

    Private Sub fndcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndcode._MYNavigator
        Try
            Dim qst As String = ""
            qst = "select count(*) from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE='" + fndcode.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                fndcode.MyReadOnly = False
            Else
                fndcode.MyReadOnly = True
            End If
            LoadData(fndcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = " select XXX.PRICE_CODE as [PRICE CODE] ,TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_DESC as [PRICE DESC], TSPL_TANKER_DISPATCH_PRICE_MASTER.TOTAL_SOLID_RATE as [TOTAL SOLID RATE], TSPL_TANKER_DISPATCH_PRICE_MASTER.EFFECTIVE_DATE as [EFFECTIVE DATE], XXX.[MCC CODE] ,  TSPL_TANKER_DISPATCH_PRICE_MASTER.Posted from ( " & _
                            " SELECT     SS.PRICE_CODE,    STUFF((SELECT ',' + US.MCC_CODE            FROM TSPL_TANKER_DISPATCH_PRICE_DETAILS US           WHERE US.PRICE_CODE = SS.PRICE_CODE           ORDER BY PRICE_CODE           FOR XML PATH('')), 1, 1, '') [MCC CODE]  FROM TSPL_TANKER_DISPATCH_PRICE_DETAILS SS " & _
                            " GROUP BY SS.PRICE_CODE, SS.PRICE_CODE " & _
                            " ) XXX  left outer join TSPL_TANKER_DISPATCH_PRICE_MASTER on TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE = XXX.PRICE_CODE "
        Dim whrCls As String = ""
        transportSql.ExporttoExcel(qry, whrCls, Me)
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
       

        'Dim gv As New RadGridView()
        'Dim IsNewEntry As Boolean
        'Me.Controls.Add(gv)
        'If transportSql.importExcel(gv, "PRICE CODE", "PRICE DESC", "TOTAL SOLID RATE", "EFFECTIVE DATE", "MCC CODE", "Posted") Then
        '    Dim linno As Integer = 1
        '    Try
        '        For Each grow As GridViewRowInfo In gv.Rows

        '            Dim obj As New clsTankerDispatchPriceMaster()
        '            Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("PRICE CODE").Value)
        '            Dim strPriceDesc As String = clsCommon.myCstr(grow.Cells("PRICE DESC").Value)
        '            Dim totalSolidRate As String = clsCommon.myCstr(grow.Cells("TOTAL SOLID RATE").Value)
        '            Dim strEffectiveDate As String = clsCommon.myCstr(grow.Cells("EFFECTIVE DATE").Value)
        '            Dim strMccCode As String = clsCommon.myCstr(grow.Cells("MCC CODE").Value)
        '            Dim strPosted As String = clsCommon.myCstr(grow.Cells("Posted").Value)
        '            linno += 1
        '            If clsCommon.myLen(strPriceCode) <= 0 Then
        '                Throw New Exception("Price Code should not be left blank line no. " + clsCommon.myCstr(linno) + ".")
        '            ElseIf clsCommon.myLen(strPriceCode) > 30 Then
        '                Throw New Exception("Please check ! length of Price code should be 30 at line no. " + clsCommon.myCstr(linno) + ".")
        '            End If

        '            If clsCommon.myLen(totalSolidRate) <= 0 Then
        '                Throw New Exception("TOTAL SOLID RATE should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
        '            End If

        '            If clsCommon.CompairString(strPosted, "0") = CompairStringResult.Equal Then
        '                strPosted = "0"
        '            ElseIf clsCommon.CompairString(strPosted, "1") = CompairStringResult.Equal Then
        '                strPosted = "1"
        '            Else
        '                Throw New Exception("Please Enter Posted as 0 Or 1  at Line No '" + linno + "'")
        '            End If
        '            If clsCommon.myLen(strMccCode) <= 0 Then
        '                Throw New Exception("MCC Code should not be left blank line no. " + clsCommon.myCstr(linno) + ".")
        '            End If

        '            Dim strArrMccCode() As String
        '            strArrMccCode = strMccCode.Split(",")

        '            If strArrMccCode.Count > 0 Then
        '                For value As Integer = 0 To strArrMccCode.Count - 1
        '                    Dim chckModuleCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count (*) from TSPL_MCC_MASTER where MCC_CODE ='" + strArrMccCode(value).Trim() + "' "))
        '                    If chckModuleCode <= 0 Then
        '                        Throw New Exception(" " + strArrMccCode(value).Trim() + "  Invalid MCC Code at line no. " + clsCommon.myCstr(linno) + ".")
        '                    End If
        '                Next
        '            Else
        '                Dim chckModuleCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count (*) from TSPL_MCC_MASTER where MCC_CODE ='" + strMccCode.Trim() + "' "))
        '                If chckModuleCode <= 0 Then
        '                    Throw New Exception(" " + strMccCode + "  Invalid MCC Code at line no. " + clsCommon.myCstr(linno) + ".")
        '                End If
        '            End If

        '            If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE='" + strPriceCode + "' ") > 0 Then
        '                IsNewEntry = False
        '            Else
        '                IsNewEntry = True
        '            End If
        '            obj.PRICE_CODE = strPriceCode
        '            obj.PRICE_DESC = strPriceDesc
        '            obj.EFFECTIVE_DATE = strEffectiveDate
        '            obj.TOTAL_SOLID_RATE = totalSolidRate

        '            obj.ArrMccCode = New List(Of clsTankerDispatchPriceDetails)
        '            Dim strArr() As String
        '            strArr = strMccCode.Split(",")
        '            Dim objTrTr As New clsTankerDispatchPriceDetails()
        '            If strArr.Count > 0 Then
        '                For value As Integer = 0 To strArr.Count - 1
        '                    objTrTr.PRICE_CODE = clsCommon.myCstr(strPriceCode)
        '                    objTrTr.MCC_CODE = strArr(value).Trim()
        '                    If (clsCommon.myLen(objTrTr.MCC_CODE) > 0) Then
        '                        obj.ArrMccCode.Add(objTrTr)
        '                    End If
        '                Next
        '            Else
        '                objTrTr.PRICE_CODE = clsCommon.myCstr(strPriceCode)
        '                objTrTr.MCC_CODE = strMccCode.Trim()
        '                If (clsCommon.myLen(objTrTr.MCC_CODE) > 0) Then
        '                    obj.ArrMccCode.Add(objTrTr)
        '                End If
        '            End If
        '            obj.ArrMccCode = New List(Of clsTankerDispatchPriceDetails)
        '            'If clsCommon.myLen(txtMCC.arrValueMember) > 0 Then
        '            '    For Each strMccCode As String In txtMCC.arrValueMember
        '            '        Dim objTrTr As New clsTankerDispatchPriceDetails()
        '            '        objTrTr.PRICE_CODE = clsCommon.myCstr(fndcode.Value)
        '            '        objTrTr.MCC_CODE = strMccCode
        '            '        If (clsCommon.myLen(objTrTr.MCC_CODE) > 0) Then
        '            '            obj.ArrMccCode.Add(objTrTr)
        '            '        End If
        '            '    Next
        '            'End If
        '            clsTankerDispatchPriceMaster.SaveData(IsNewEntry, obj)


        '        Next
        '        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        '    End Try
        'End If
        'Me.Controls.Remove(gv)

    End Sub
    ' Ticket No : ERO/01/04/19-000531 by Prabhakar 
    Private Sub chkJobWork_CheckedChanged(sender As Object, e As EventArgs) Handles chkJobWork.CheckedChanged
        If chkJobWork.Checked = True Then
            txtItemCode.Enabled = True
            penalItemCode.Visible = True
            txtMCC.arrValueMember = Nothing
        Else
            txtItemCode.Enabled = False
            txtItemCode.Value = ""
            lblItemCode.Text = ""
            penalItemCode.Visible = False
            txtMCC.arrValueMember = Nothing
        End If
    End Sub

    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Dim qry As String = "select TSPL_ITEM_MASTER.item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        Dim WhrCls As String = "    "
        txtItemCode.Value = clsCommon.ShowSelectForm("ItemCode@findT", qry, "Code", WhrCls, txtItemCode.Value, "Code", isButtonClicked)
        lblItemCode.Text = clsItemMaster.GetItemName(txtItemCode.Value, Nothing)
    End Sub
End Class
