Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO

Public Class FrmBagReceipt
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Const colGunnySNo As String = "colGunnySNo"
    Const colGunnyICode As String = "colGunnyICode"
    Const colGunnyIName As String = "colGunnyIName"
    Const colGunnyUOM As String = "colGunnyUOM"
    Const colGunnyQty As String = "colGunnyQty"
    Dim isCellValueChangedOpenGunny As Boolean = False
    Dim SettGunnyBagTollerance As Decimal = 10
    Dim obj As New ClsBagReceipt
    Dim arrLoc As String = Nothing
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGridGunny()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "SNo"
        repoNum.Name = colGunnySNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)


        Dim repoTxt As New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Item Code"
        repoTxt.Width = 100
        repoTxt.Name = colGunnyICode
        repoTxt.ReadOnly = False
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Item"
        repoTxt.Width = 200
        repoTxt.Name = colGunnyIName
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "UOM"
        repoTxt.Width = 100
        repoTxt.Name = colGunnyUOM
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n0}"
        repoNum.HeaderText = "Qty"
        repoNum.Name = colGunnyQty
        repoNum.Width = 100
        repoNum.ReadOnly = False
        repoNum.DecimalPlaces = 0
        repoNum.ShowUpDownButtons = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.Rows.AddNew()
    End Sub

    Private Sub FrmBagReceipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "datetime NOT NULL")
        coll.Add("Location", "varchar(40)  NULL")
        coll.Add("Remarks", "varchar(250)  NULL")
        coll.Add("Status", "integer null")
        coll.Add("Created_By", "varchar(12)  NOT NULL")
        coll.Add("Created_Date", "datetime  NOT NULL")
        coll.Add("Modify_By", "varchar(12)  NOT NULL")
        coll.Add("Modify_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BAG_RECEIPT_HEAD", coll, Nothing, True, False, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION PRIMARY KEY")
        coll.Add("Document_Code", "varchar(30) NOT NULL References TSPL_BAG_RECEIPT_HEAD(Document_Code)")
        coll.Add("Item_Code", "varchar(50) NOT NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("UOM", "varchar(12) NULL")
        coll.Add("Qty", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BAG_RECEIPT_DETAIL", coll, Nothing, True, False, "TSPL_BAG_RECEIPT_HEAD", "Document_Code", "", True)

        SetUserMgmtNew()
        LoadBlankGridGunny()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGridGunny()
        'ReStoreGridLayout()
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtRemarks.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        gv1.Rows.Clear()
        gv1.SummaryRowsBottom.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenGunny Then
                    isCellValueChangedOpenGunny = True
                    If e.Column Is gv1.Columns(colGunnyICode) Then
                        Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as ICode,Item_Desc as IName,TSPL_ITEM_UOM_DETAIL.UOM_Code as [UOM],cast(TSPL_ITEM_UOM_DETAIL.Net_Weight as varchar) as [Weight], TSPL_ITEM_MASTER.Short_Description as [Short Description],TSPL_ITEM_MASTER.Structure_Code as [Structure Code] ,TSPL_ITEM_MASTER.Structure_Desc as [Structure Desc],TSPL_ITEM_UOM_DETAIL.Conversion_Factor 
from TSPL_ITEM_MASTER 
left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER.item_category
left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code = TSPL_ITEM_MASTER.Sub_item_category 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
where TSPL_ITEM_UOM_DETAIL.Net_Weight > 0"
                        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("spF@gn", qry, "ICode", "")
                        If dr IsNot Nothing Then
                            gv1.CurrentRow.Cells(colGunnyICode).Value = clsCommon.myCstr(dr("ICode"))
                            gv1.CurrentRow.Cells(colGunnyIName).Value = clsCommon.myCstr(dr("IName"))
                            gv1.CurrentRow.Cells(colGunnyUOM).Value = clsCommon.myCstr(dr("UOM"))
                        Else
                            gv1.CurrentRow.Cells(colGunnyICode).Value = ""
                            gv1.CurrentRow.Cells(colGunnyIName).Value = ""
                            gv1.CurrentRow.Cells(colGunnyUOM).Value = ""
                            gv1.CurrentRow.Cells(colGunnyQty).Value = 0
                        End If
                    End If
                End If
                isCellValueChangedOpenGunny = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colGunnySNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Function AllowToSave() As Boolean
        Try
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBagReceipt()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location = txtLocation.Value
                obj.Remarks = txtRemarks.Text

                obj.ArrGunny = New List(Of clsBagReceiptDetail)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim objtr As New clsBagReceiptDetail
                    objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colGunnyICode).Value)
                    objtr.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colGunnyUOM).Value)
                    objtr.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colGunnyQty).Value)
                    If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso objtr.Qty > 0 Then
                        obj.ArrGunny.Add(objtr)
                    End If
                Next

                If (obj.SaveData(obj, isNewEntry)) Then
                    If Not isPost Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        txtDocNo.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isInsideLoadData = True
        obj = ClsBagReceipt.GetData(strCode, arrLoc, NavTyep)

        If obj IsNot Nothing Then
            isNewEntry = False
            btnSave.Text = "Update"
            If obj.Status Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            LoadBlankGridGunny()
            txtDocNo.Value = obj.Document_Code
            Me.txtDate.Value = obj.Document_Date
            txtLocation.Value = obj.Location
            txtRemarks.Text = obj.Remarks

            If obj.ArrGunny IsNot Nothing AndAlso obj.ArrGunny.Count > 0 Then
                For Each objtr As clsBagReceiptDetail In obj.ArrGunny
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGunnySNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGunnyICode).Value = objtr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGunnyIName).Value = objtr.Item_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGunnyUOM).Value = objtr.UOM
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGunnyQty).Value = objtr.Qty
                    gv1.Rows.AddNew()
                Next
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (ClsBagReceipt.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", txtDate.Value) = False Then
                    Exit Sub
                End If
                ClsBagReceipt.PostData(Form_ID, txtDocNo.Value, arrLoc, True)
                common.clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class