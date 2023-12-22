Imports common
Imports System.Reflection

Public Class FrmPOSCommissionMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Public ITEM_PRICE_ID As Integer = 0
    Public BASIC_RATE As Integer = 0
#End Region
    Public Sub LoadGrid()
        gvGroup.DataSource = clsPOSCommissionMapping.GetData(ITEM_PRICE_ID)
        gvGroup.Columns("Group Code").IsVisible = False
        gvGroup.Columns("POS Type").ReadOnly = True
        gvGroup.Columns("Level").ReadOnly = True
        'gvGroup.Columns("Comm. Type").IsVisible = False
        'gvGroup.Columns("Comm. Amount").IsVisible = False
        gvGroup.Columns("Basic Rate").ReadOnly = True
        gvGroup.Columns("Group Code").Width = 100
        gvGroup.Columns("POS Type").Width = 100
        gvGroup.Columns("Level").Width = 100
        gvGroup.Columns("Comm. Type").Width = 100
        gvGroup.Columns("Comm. Amount").Width = 100
        gvGroup.Columns("Basic Rate").Width = 100
        gvGroup.EnableFiltering = False
        gvGroup.ShowFilteringRow = False
        gvGroup.AllowDeleteRow = False
        gvGroup.AllowAddNewRow = False
        gvGroup.ShowGroupPanel = False
        gvGroup.AllowColumnReorder = False
        gvGroup.AllowRowReorder = False
        gvGroup.EnableSorting = False
        lblBasicPrice.Text = ("Basic Price : " + clsCommon.myCstr(BASIC_RATE))
        lblTotalPOSGroups.Text = ("Total POS Groups : " + clsCommon.myCstr(gvGroup.RowCount))
        
    End Sub
    Private Sub FrmPOSCommissionMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Sub gvGroup_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvGroup.CellValueChanged
        Try
            If e.RowIndex >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(e.Column.FieldName), "Comm. Amount") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(e.Column), 0) = CompairStringResult.Greater Then
                        'If clsCommon.CompairString(clsCommon.myCstr(e.Value), BASIC_RATE) = CompairStringResult.Less Then
                        For i As Integer = (gvGroup.RowCount - 1) To 0 Step -1
                            If (i = (gvGroup.RowCount - 1)) Then
                                gvGroup.Rows(i).Cells("Basic Rate").Value = (BASIC_RATE - (gvGroup.Rows(i).Cells("Comm. Amount").Value))
                            Else
                                gvGroup.Rows(i).Cells("Basic Rate").Value = (gvGroup.Rows(i + 1).Cells("Basic Rate").Value) - (gvGroup.Rows(i).Cells("Comm. Amount").Value)
                            End If
                        Next i
                        'Else
                        '    Throw New Exception("Please Enter Commission Less then Basic Rate")
                        '    Return
                        'End If

                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        CollectCommission()
    End Sub
    Public Sub CollectCommission()
        Try
            Dim arr = New List(Of clsPOSCommissionMapping)
            For Each dgrv As GridViewRowInfo In gvGroup.Rows
                Dim objArr As New clsPOSCommissionMapping()
                objArr.GROUP_CODE = clsCommon.myCstr(dgrv.Cells("Group Code").Value)
                objArr.COMMISSION_TYPE = clsCommon.myCstr(dgrv.Cells("Comm. Type").Value)
                objArr.COMMISSION_AMOUNT = clsCommon.myCstr(dgrv.Cells("Comm. Amount").Value)
                objArr.BASIC_RATE = clsCommon.myCstr(dgrv.Cells("Basic Rate").Value)
                arr.Add(objArr)
            Next
            Dim objArr1 As Object() = New Object() {arr}
            SetValueMDIChild("SetData", objArr1)
            Throw New Exception("Commission Mapped.")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Sub SetValueMDIChild(MethodName As String, Optional pram As Object() = Nothing)
        Try
            If MDI.ActiveMdiChild IsNot Nothing Then
                Dim frmObject As Type = Application.OpenForms(TryCast(MDI.ActiveMdiChild, FrmMainTranScreen).Name).[GetType]()
                Dim methodInfos As MethodInfo = frmObject.GetMethod(MethodName)
                If methodInfos IsNot Nothing Then
                    methodInfos.Invoke(DirectCast(Application.OpenForms(TryCast(MDI.ActiveMdiChild, FrmMainTranScreen).Name), FrmMainTranScreen), pram)
                End If
            End If
        Catch eX As Exception

        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If clsCommon.myLen(gvGroup.Rows(0).Cells("Comm. Amount").Value) > 0 Then
            For i As Integer = (gvGroup.RowCount - 1) To 0 Step -1
                If (i = (gvGroup.RowCount - 1)) Then
                    gvGroup.Rows(i).Cells("Basic Rate").Value = (BASIC_RATE - (gvGroup.Rows(i).Cells("Comm. Amount").Value))
                Else
                    gvGroup.Rows(i).Cells("Basic Rate").Value = (gvGroup.Rows(i + 1).Cells("Basic Rate").Value) - (gvGroup.Rows(i).Cells("Comm. Amount").Value)
                End If
            Next i
        End If
    End Sub
End Class
