Imports Microsoft.VisualBasic
Imports System
Imports XpertERPEngine
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common

Public Class FrmCompleteLoadout
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public Sub New(ByVal user As String, ByVal company As String)

        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            l1User = dr.Rows(0)(0).ToString()
            l2User = dr.Rows(0)(1).ToString()
            l3User = dr.Rows(0)(2).ToString()
            l4User = dr.Rows(0)(3).ToString()
            l5User = dr.Rows(0)(4).ToString()
        End If
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.LoadOutStatus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnClose.Visible = MyBase.isDeleteFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub FrmCompleteLoadout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        'If objCommonVar.CurrentUserCode <> "admin" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellValueChanged

    End Sub

    Private Sub RadGridView1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridView1.CellValueChanged
        If e.ColumnIndex = 0 Then
            Dim strSql, strLoadOutNo, strLoadIn, strComplete As String
            '' '' ''strSql = "select Shipment_No as [Code],convert(varchar(10),Shipment_Date,103) as [LoadOut Date] from TSPL_SHIPMENT_MASTER"
            ' '' '' ''strLoadOutNo = clsCommon.ShowSelectForm("LoadoutDetails", strSql, "Code", "is_post='Y' and isnull(is_complete,'N')='N'", clsCommon.myCstr(RadGridView1.CurrentRow.Cells("LoadOutNo").Value), "Code", False)

            strSql = " select "
            strSql += " aa.LoadoutNo,isnull(convert(varchar,aa.LoadoutDate,103),'') as LoadoutDate,aa.Location,aa.Route_Desc,aa.Salesman_Code,aa.Vehicle_Code,aa.Type  "

            strSql += "  from ("

            strSql += " select Sale_Invoice_No as LoadoutNo,Sale_Invoice_Date as LoadoutDate,Location ,Vehicle_Code,Route_Desc,Salesman_Code,'sale' as Type,Shipment_Type,'' as referenceno ,Is_Post  from TSPL_SALE_INVOICE_HEAD "
            strSql += " union all"
            strSql += " select Transfer_No as LoadoutNo,Load_Out_Date as LoadoutDate,From_Location as location  ,Vehicle_Code,Route_Desc,Salesmancode as Salesman_Code,'Transfer' as Type,Trans_Type as Shipment_Type ,Reference_Doc_No as referenceno,post as Is_Post from TSPL_TRANSFER_HEAD  "
            strSql += " )aa"
            Dim whrcls As String = " (Shipment_Type='Excise' and ISNULL(referenceno ,'')='' ) or (Shipment_Type='depot' and ISNULL(referenceno ,'')='' )or Shipment_Type='route' or Type='sale' and Is_Post='y'"
            strLoadOutNo = clsCommon.ShowSelectForm("LoadoutDetails", strSql, "LoadoutNo", whrcls, clsCommon.myCstr(RadGridView1.CurrentRow.Cells("LoadOutNo").Value), "LoadoutNo", False)
            RadGridView1.CurrentRow.Cells("LoadOutNo").Value = strLoadOutNo
            If strLoadOutNo <> "" Then
                'dr = clsDBFuncationality.GetDataTable("select convert(varchar(10),Shipment_Date,103) as [LoadOut Date],Cust_Name,Salesman_Code,Vehicle_Code,Is_Complete from TSPL_SHIPMENT_MASTER  where Shipment_No='" + RadGridView1.CurrentRow.Cells("LoadOutNo").Value + "'")

                strSql = "   select "
                strSql += " aa.LoadoutNo,aa.Cust_Name,isnull(convert(varchar,aa.LoadoutDate,103),'') as LoadoutDate,aa.Location,aa.Route_Desc,aa.Salesman_Code,aa.Type ,aa.Vehicle_Code ,Is_Complete "
                strSql += " from ("
                strSql += " select Sale_Invoice_No as LoadoutNo,Sale_Invoice_Date as LoadoutDate,Location ,Vehicle_Code,Route_Desc,Salesman_Code,'sale' as Type,Shipment_Type,'' as referenceno,Is_Post,Status  as  Is_Complete ,Cust_Name from TSPL_SALE_INVOICE_HEAD "
                strSql += " union all"
                strSql += " select Transfer_No as LoadoutNo,Load_Out_Date as LoadoutDate,'' as location  ,Vehicle_Code,Route_Desc,Salesmancode as Salesman_Code,'Transfer' as Type,Trans_Type as Shipment_Type ,Reference_Doc_No as referenceno,Post as Is_Post,Is_Complete,'' as Cust_Name   from TSPL_TRANSFER_HEAD  "
                strSql += " )aa "
                strSql += "  where aa.LoadoutNo='" + RadGridView1.CurrentRow.Cells("LoadOutNo").Value + "'"
                dr = clsDBFuncationality.GetDataTable(strSql)
                If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    RadGridView1.CurrentRow.Cells("LoadoutDate").Value = dr.Rows(0)("LoadoutDate").ToString()
                    RadGridView1.CurrentRow.Cells("Customer").Value = dr.Rows(0)("Cust_Name").ToString()
                    RadGridView1.CurrentRow.Cells("Salesperson").Value = dr.Rows(0)("Salesman_Code").ToString()
                    RadGridView1.CurrentRow.Cells("VehicleNo").Value = dr.Rows(0)("Vehicle_Code").ToString()
                    strComplete = dr.Rows(0)("Is_Complete").ToString
                    If strComplete = "Y" Then
                        RadGridView1.CurrentRow.Cells("VehicleRT").Value = True
                    Else
                        RadGridView1.CurrentRow.Cells("VehicleRT").Value = False
                    End If
                End If


                'dr.Close()

                'strLoadIn = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_TRANSFER_HEAD.Transfer_No from TSPL_TRANSFER_HEAD,TSPL_SHIPMENT_MASTER where TSPL_TRANSFER_HEAD.Load_Out_No=TSPL_SHIPMENT_MASTER.Transfer_No and  Transfer_Type='LI' and Shipment_No='" + RadGridView1.CurrentRow.Cells("LoadOutNo").Value + "'"))
                strLoadIn = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_TRANSFER_HEAD.Transfer_No from TSPL_TRANSFER_HEAD,TSPL_SALE_INVOICE_HEAD  where TSPL_TRANSFER_HEAD.Load_Out_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  and  Transfer_Type='LI' and Sale_Invoice_No ='" + RadGridView1.CurrentRow.Cells("LoadOutNo").Value + "'"))

                If strLoadIn = "" Then
                    RadGridView1.CurrentRow.Cells("LoadIn").Value = "No"
                Else
                    RadGridView1.CurrentRow.Cells("LoadIn").Value = "Yes"
                End If
                'dr.Close()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Dim trans As SqlTransaction
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim grow As GridViewDataRowInfo
            Dim strComplete As String

            For Each grow In RadGridView1.Rows
                strComplete = grow.Cells("VehicleRT").Value
                If strComplete = "True" Then
                    strComplete = "Y"
                Else
                    strComplete = "N"
                End If
                sql = "select count(Transfer_No)  from TSPL_TRANSFER_HEAD where Transfer_No='" + grow.Cells("LoadoutNo").Value + "'"
                Dim Count As Integer = Convert.ToInt32(clsDBFuncationality.getSingleValue(sql, trans))
                If (Count = 1) Then
                    sql = "update TSPL_TRANSFER_HEAD set Is_Complete='" + strComplete + "' where Transfer_No ='" + grow.Cells("LoadoutNo").Value + "'"
                Else
                    sql = "update TSPL_SALE_INVOICE_HEAD set status='" + strComplete + "' where Sale_Invoice_No ='" + grow.Cells("LoadoutNo").Value + "'"

                End If
                'sql = "update TSPL_SHIPMENT_MASTER set iS_cOMPLETE='" + strComplete + "' where Shipment_No ='" + grow.Cells("LoadoutNo").Value + "'"
                clsDBFuncationality.ExecuteNonQuery(sql, trans)

            Next
            trans.Commit()
            myMessages.insert()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
            ' myMessages.myExceptions(ex.Message.ToString())
        End Try
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        reset()
    End Sub
    Sub reset()
        Dim j As Integer = RadGridView1.Rows.Count
        If j = 0 Then
            RadGridView1.ReadOnly = False
            RadGridView1.Rows.AddNew()
            RadGridView1.DataSource = Nothing
            RadGridView1.Rows.Clear()

        Else
            RadGridView1.ReadOnly = False
            RadGridView1.Rows.AddNew()
            RadGridView1.DataSource = Nothing
            RadGridView1.Rows.Clear()

        End If
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "LOUT-STATUS"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
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
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    Private Sub FrmCompleteLoadout_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
End Class
