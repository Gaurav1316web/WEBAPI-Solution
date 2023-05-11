Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Windows.Forms

Public Class ControlDesigner
    'This class contains the complete designer logic
#Region " Declarations "
    Public mouseLocation As New Point

    Private Const HANDLESIZE As Integer = 8
    Private Const MINSIZE As Integer = 5

    Private intSnapMargin As Integer = 0
    Private frmCurrent As Form = Nothing
    Private frmBackColor As Color = Nothing
    Private frmContextMenu As ContextMenuStrip = Nothing
    Private ctlCurrent As Control = Nothing
    Private lblHandle(7) As Label
    Private start_Left As Integer
    Private start_Top As Integer
    Private start_Width As Integer
    Private start_Height As Integer
    Private start_X As Integer
    Private start_Y As Integer
    Private bolDragging As Boolean
    Private lstControls As List(Of ControlWrapper) = Nothing
    Private lstExceptCtls As List(Of Control) = Nothing
    Private lstSelectedControls As List(Of Control) = Nothing
    Private WithEvents mnuContext As ContextMenuStrip = Nothing
    Private mnuItem_Properties As ToolStripMenuItem = Nothing
    Private mnuItem_Alignment As ToolStripMenuItem = Nothing
    Private mnuItem_AlignLeft As ToolStripMenuItem = Nothing
    Private mnuItem_AlignRight As ToolStripMenuItem = Nothing
    Private mnuItem_AlignTop As ToolStripMenuItem = Nothing
    Private mnuItem_AlignBottom As ToolStripMenuItem = Nothing
    Private arrArrow() As Cursor = New Cursor() {Cursors.SizeNWSE, _
                                                 Cursors.SizeNS, _
                                                 Cursors.SizeNESW, _
                                                 Cursors.SizeWE, _
                                                 Cursors.SizeNWSE, _
                                                 Cursors.SizeNS, _
                                                 Cursors.SizeNESW, _
                                                 Cursors.SizeWE}

    Private frmProps As Form = Nothing
    Private intPropsWidth As Integer = 0
    Private intPropsHeight As Integer = 0
#End Region

#Region " Public interface "
    Public Sub New(ByRef Form As Form, ByVal ExceptControls As List(Of Control), ByVal EditColor As Color, Optional ByVal PropDlgWidth As Integer = 350, Optional ByVal PropDlgHeight As Integer = 500, Optional ByVal SnapMargin As Integer = 8)
        'Creating this class with the parameters above starts the design mode for the passed form

        'Set snap width (used on mouse dragging with Ctrl key down)
        intSnapMargin = SnapMargin

        'Get the form to activate designer in and save some settings to be replaced while in design mode
        frmCurrent = Form
        frmBackColor = frmCurrent.BackColor
        frmContextMenu = frmCurrent.ContextMenuStrip

        'Set dimensions of the properties dialog
        intPropsWidth = PropDlgWidth
        intPropsHeight = PropDlgHeight

        'Create control collections
        lstExceptCtls = ExceptControls              'contains the controls excepted from design
        lstControls = New List(Of ControlWrapper)   'contains wrappers for the controls allowed to design
        lstSelectedControls = New List(Of Control)  'will contain the selected controls at any one time

        'Create the context menu
        mnuContext = New ContextMenuStrip

        mnuItem_Properties = New System.Windows.Forms.ToolStripMenuItem()
        mnuItem_Alignment = New System.Windows.Forms.ToolStripMenuItem()
        mnuItem_AlignLeft = New System.Windows.Forms.ToolStripMenuItem()
        mnuItem_AlignRight = New System.Windows.Forms.ToolStripMenuItem()
        mnuItem_AlignTop = New System.Windows.Forms.ToolStripMenuItem()
        mnuItem_AlignBottom = New System.Windows.Forms.ToolStripMenuItem()

        mnuContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {mnuItem_Properties, mnuItem_Alignment})
        With mnuContext
            .ShowCheckMargin = False
            .ShowImageMargin = False
            .ShowItemToolTips = False
            .BackColor = EditColor
        End With

        With mnuItem_Properties
            .Name = "mnuItem_Properties"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Properties"
        End With

        With mnuItem_Alignment
            .DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {mnuItem_AlignLeft, mnuItem_AlignRight, mnuItem_AlignTop, mnuItem_AlignBottom})
            .Name = "mnuItem_Alignment"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Alignment"
        End With

        With CType(mnuItem_Alignment.DropDown, ToolStripDropDownMenu)
            .ShowImageMargin = False
            .BackColor = EditColor
        End With

        With mnuItem_AlignLeft
            .Name = "mnuItem_AlignLeft"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Left"
        End With

        With mnuItem_AlignRight
            .Name = "mnuItem_AlignRight"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Right"
        End With

        With mnuItem_AlignTop
            .Name = "mnuItem_AlignTop"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Top"
        End With

        With mnuItem_AlignBottom
            .Name = "mnuItem_AlignBottom"
            .Size = New System.Drawing.Size(50, 22)
            .Text = "Bottom"
        End With

        AddHandler mnuItem_Properties.Click, AddressOf mnuItem_Properties_Click
        AddHandler mnuItem_AlignLeft.Click, AddressOf mnuItem_AlignLeft_Click
        AddHandler mnuItem_AlignRight.Click, AddressOf mnuItem_AlignRight_Click
        AddHandler mnuItem_AlignTop.Click, AddressOf mnuItem_AlignTop_Click
        AddHandler mnuItem_AlignBottom.Click, AddressOf mnuItem_AlignBottom_Click

        'Create sizing handles displayed around selected control(s)
        For i As Integer = 0 To 7
            lblHandle(i) = New Label

            With lblHandle(i)
                .TabIndex = i
                .FlatStyle = 0
                .BorderStyle = BorderStyle.FixedSingle
                .BackColor = Color.White
                .Cursor = arrArrow(i)
                .Text = ""
                .Visible = False
            End With

            AddHandler frmCurrent.Click, AddressOf Me.Parent_Click
            AddHandler lblHandle(i).MouseDown, AddressOf Me.Handle_MouseDown
            AddHandler lblHandle(i).MouseMove, AddressOf Me.Handle_MouseMove
            AddHandler lblHandle(i).MouseUp, AddressOf Me.Handle_MouseUp
        Next

        'Add allowed controls of the passed form to the control wrapper collection
        AddControls(frmCurrent)

        'Replace context menu and backcolor of the passed form
        frmCurrent.ContextMenuStrip = mnuContext
        frmCurrent.BackColor = EditColor
    End Sub

    Public Sub Dispose()
        'Call this method to terminate design mode of the passed form

        'Release all controls allowed for design
        For Each ctl As ControlWrapper In lstControls
            'Remove event handlers appended by this class
            RemoveHandler ctl.Control.MouseDown, AddressOf Me.Control_MouseDown
            RemoveHandler ctl.Control.MouseMove, AddressOf Me.Control_MouseMove
            RemoveHandler ctl.Control.MouseUp, AddressOf Me.Control_MouseUp
            RemoveHandler ctl.Control.PreviewKeyDown, AddressOf Me.Control_KeyDown
            RemoveHandler ctl.Control.Click, AddressOf SelectControl

            If ctl.GetType.ToString = "System.Windows.Forms.TextBox" Then
                RemoveHandler CType(ctl.Control, System.Windows.Forms.TextBox).MultilineChanged, AddressOf UpdateHandles
            End If

            If Not ctl.Control.Parent.Equals(frmCurrent) Then
                RemoveHandler ctl.Control.Parent.Click, AddressOf Me.Parent_Click
            End If

            'Reset replaced control settings with original values
            ctl.Control.Cursor = ctl.Cursor
            ctl.Control.ContextMenuStrip = ctl.ContextMenuStrip
        Next

        'Clear all collections
        lstControls.Clear()
        lstExceptCtls.Clear()
        lstSelectedControls.Clear()

        'Remove sizing handles
        For i As Integer = 0 To 7
            RemoveHandler lblHandle(i).MouseDown, AddressOf Me.Handle_MouseDown
            RemoveHandler lblHandle(i).MouseMove, AddressOf Me.Handle_MouseMove
            RemoveHandler lblHandle(i).MouseUp, AddressOf Me.Handle_MouseUp

            If lblHandle(i).Parent IsNot Nothing Then
                lblHandle(i).Parent.Controls.Remove(lblHandle(i))
            End If
        Next

        'Reset replaced from settings with original values
        RemoveHandler frmCurrent.Click, AddressOf Me.Parent_Click
        frmCurrent.BackColor = frmBackColor
        frmCurrent.ContextMenuStrip = frmContextMenu

        'Release objects of this class
        lstControls = Nothing
        lstExceptCtls = Nothing
        lstSelectedControls = Nothing
    End Sub
#End Region

#Region " Sizing handle events "
    Private Sub Handle_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Starts the drag mode for a sizing handle
        bolDragging = True

        start_Left = e.X
        start_Top = e.Y

        HideHandles()
    End Sub

    Private Sub Handle_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Performs dragging of a sizing handle
        If bolDragging Then
            Dim DiffWidth As Integer = (e.X - start_Left)
            Dim DiffHeight As Integer = (e.Y - start_Top)

            start_Left = e.X
            start_Top = e.Y

            For Each ctl As Control In lstSelectedControls
                With ctl
                    Select Case (CType(sender, Label)).TabIndex
                        Case 0 ' bolDragging top-left sizing box
                            .SetBounds(IIf(.Bounds.Left + DiffWidth < 0, 0, .Bounds.Left + DiffWidth), IIf(.Bounds.Top + DiffHeight < 0, 0, .Bounds.Top + DiffHeight), IIf(.Bounds.Width - DiffWidth < MINSIZE, MINSIZE, .Bounds.Width - DiffWidth), IIf(.Bounds.Height - DiffHeight < MINSIZE, MINSIZE, .Bounds.Height - DiffHeight))
                        Case 1 ' bolDragging top-center sizing box
                            .SetBounds(.Bounds.Left, IIf(.Bounds.Top + DiffHeight < 0, 0, .Bounds.Top + DiffHeight), .Bounds.Width, IIf(.Bounds.Height - DiffHeight < MINSIZE, MINSIZE, .Bounds.Height - DiffHeight))
                        Case 2 ' bolDragging top-right sizing box
                            .SetBounds(.Bounds.Left, IIf(.Bounds.Top + DiffHeight < 0, 0, .Bounds.Top + DiffHeight), IIf(.Bounds.Width + DiffWidth < MINSIZE, MINSIZE, .Bounds.Width + DiffWidth), IIf(.Bounds.Height - DiffHeight < MINSIZE, MINSIZE, .Bounds.Height - DiffHeight))
                        Case 3 ' bolDragging right-middle sizing box
                            .SetBounds(.Bounds.Left, .Bounds.Top, IIf(.Bounds.Width + DiffWidth < MINSIZE, MINSIZE, .Bounds.Width + DiffWidth), .Bounds.Height)
                        Case 4 ' bolDragging right-bottom sizing box
                            .SetBounds(.Bounds.Left, .Bounds.Top, IIf(.Bounds.Width + DiffWidth < MINSIZE, MINSIZE, .Bounds.Width + DiffWidth), IIf(.Bounds.Height + DiffHeight < MINSIZE, MINSIZE, .Bounds.Height + DiffHeight))
                        Case 5 ' bolDragging center-bottom sizing box
                            .SetBounds(.Bounds.Left, .Bounds.Top, .Bounds.Width, IIf(.Bounds.Height + DiffHeight < MINSIZE, MINSIZE, .Bounds.Height + DiffHeight))
                        Case 6 ' bolDragging left-bottom sizing box
                            .SetBounds(IIf(.Bounds.Left + DiffWidth < 0, 0, .Bounds.Left + DiffWidth), .Bounds.Top, IIf(.Bounds.Width - DiffWidth < MINSIZE, MINSIZE, .Bounds.Width - DiffWidth), IIf(.Bounds.Height + DiffHeight < MINSIZE, MINSIZE, .Bounds.Height + DiffHeight))
                        Case 7 ' bolDragging left-middle sizing box
                            .SetBounds(IIf(.Bounds.Left + DiffWidth < 0, 0, .Bounds.Left + DiffWidth), .Bounds.Top, IIf(.Bounds.Width - DiffWidth < MINSIZE, MINSIZE, .Bounds.Width - DiffWidth), .Bounds.Height)
                    End Select
                End With
            Next
        End If
    End Sub

    Private Sub Handle_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Ends the drag mode for a sizing handle
        bolDragging = False

        UpdateHandles(ctlCurrent, New EventArgs)
    End Sub
#End Region

#Region " Control events "
    Private Sub SelectControl(ByVal sender As Object, ByVal e As EventArgs)
        'Manages single or multiple control selection on left mouse click
        Try
            'Check shift state and new selection
            If (Not My.Computer.Keyboard.ShiftKeyDown) AndAlso (Not lstSelectedControls.Contains(sender)) Then
                'Single selection mode - remove all selected controls if shift is not down
                lstSelectedControls.Clear()

                If ctlCurrent IsNot Nothing Then
                    'Restore standard cursor for deselected control
                    ctlCurrent.Cursor = Cursors.Arrow
                    ctlCurrent = Nothing
                End If
            End If

            'Get currently selected control
            ctlCurrent = CType(sender, Control)

            If TypeOf ctlCurrent Is Control Then
                'On propertygrids don't allow to select a sub-control
                If ctlCurrent.Parent.GetType.ToString = "System.Windows.Forms.PropertyGrid" Then
                    ctlCurrent = ctlCurrent.Parent
                End If

                'Add selected control to collection of selected controls if not already contained
                If Not lstSelectedControls.Contains(ctlCurrent) Then
                    Dim bolOK As Boolean = True

                    If lstSelectedControls.Count > 0 Then
                        bolOK = lstSelectedControls.Item(0).Parent.Equals(ctlCurrent.Parent)
                    End If

                    If bolOK Then
                        lstSelectedControls.Add(ctlCurrent)

                        'Assign sizing handles to control's container (or form on multiple selection)
                        For i As Integer = 0 To 7
                            lblHandle(i).Parent = ctlCurrent.Parent
                            lblHandle(i).BringToFront()
                        Next

                        'Reposition an show sizing handles
                        UpdateHandles(ctlCurrent, New EventArgs)

                        'Set cursor
                        ctlCurrent.Cursor = Cursors.SizeAll
                    Else
                        MsgBox("Please select only controls within the same container", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Multiple selection")

                        ctlCurrent = lstSelectedControls.Item(lstSelectedControls.Count - 1)
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub SelectControl(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Manages single or multiple control selection on right mouse click
        If e.Button = MouseButtons.Right Then
            Try
                'Check shift state and new selection
                If (Not My.Computer.Keyboard.ShiftKeyDown) AndAlso (Not lstSelectedControls.Contains(sender)) Then
                    'Single selection mode - remove all selected controls if shift is not down
                    lstSelectedControls.Clear()

                    If ctlCurrent IsNot Nothing Then
                        'Restore standard cursor for deselected control
                        ctlCurrent.Cursor = Cursors.Arrow
                        ctlCurrent = Nothing
                    End If
                End If

                'Get currently selected control
                ctlCurrent = CType(sender, Control)

                If TypeOf ctlCurrent Is Control Then
                    'On propertygrids don't allow to select a sub-control
                    If ctlCurrent.Parent.GetType.ToString = "System.Windows.Forms.PropertyGrid" Then
                        ctlCurrent = ctlCurrent.Parent
                    End If

                    'Add selected control to collection of selected controls if not already contained
                    If Not lstSelectedControls.Contains(ctlCurrent) Then
                        Dim bolOK As Boolean = True

                        If lstSelectedControls.Count > 0 Then
                            bolOK = lstSelectedControls.Item(0).Parent.Equals(ctlCurrent.Parent)
                        End If

                        If bolOK Then
                            lstSelectedControls.Add(ctlCurrent)

                            'Assign sizing handles to control's container (or form on multiple selection)
                            For i As Integer = 0 To 7
                                lblHandle(i).Parent = ctlCurrent.Parent
                                lblHandle(i).BringToFront()
                            Next

                            'Reposition an show sizing handles
                            UpdateHandles(ctlCurrent, New EventArgs)

                            'Set cursor
                            ctlCurrent.Cursor = Cursors.SizeAll
                        Else
                            MsgBox("Please select only controls within the same container", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Multiple selection")

                            ctlCurrent = lstSelectedControls.Item(lstSelectedControls.Count - 1)
                        End If
                    End If
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub Control_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Starts the drag mode for all selected controls

        'If neccessary, select control first
        If (ctlCurrent Is Nothing) Or ((ctlCurrent IsNot Nothing) AndAlso (Not ctlCurrent.Equals(sender))) Then
            SelectControl(sender, New EventArgs)
        End If

        'If not selection denied
        If sender.Equals(ctlCurrent) Then
            'Start dragging
            bolDragging = True

            start_X = e.X
            start_Y = e.Y

            HideHandles()
        End If
    End Sub

    Private Sub Control_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Performs dragging of all selected controls
        If bolDragging Then
            For Each ctl As Control In lstSelectedControls
                ctl.Location = New Point(ctl.Location.X + e.X - start_X, ctl.Location.Y + e.Y - start_Y)
                frmCurrent.Refresh()
            Next
        End If
    End Sub

    Private Sub Control_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        'Ends the drag mode for all selected controls
        bolDragging = False

        'Check if Ctrl key down
        If My.Computer.Keyboard.CtrlKeyDown Then
            'Snap all selected controls to next grid point
            For Each ctl As Control In lstSelectedControls
                'Calculate left and top of final position
                Dim l As Integer = ctl.Location.X + e.X - start_X
                Dim t As Integer = ctl.Location.Y + e.Y - start_Y

                '1) Horizontal snap
                'Calculate distance to right border of control container
                Dim intDist As Integer = Math.Abs(ctl.Parent.DisplayRectangle.Right - (l + ctl.Width))

                'If distance is less than snap margin
                If (intDist < intSnapMargin) Then
                    'Snap control to right border of container
                    l = (ctl.Parent.DisplayRectangle.Right - ctl.Width)
                Else
                    'If distance to left border of container is less than snap margin
                    If Math.Abs(l) < intSnapMargin Then
                        'Snap control to left border of container
                        l = 0
                    Else
                        'Otherwise snap control to next imaginery vertical snap line
                        l = Math.Round(ctl.Left / intSnapMargin) * intSnapMargin
                    End If
                End If

                '2) Vertical snap
                'Calculate distance to bottom border of control container
                intDist = Math.Abs(ctl.Parent.DisplayRectangle.Bottom - (t + ctl.Height))

                'If distance is less than snap margin
                If (intDist < intSnapMargin) Then
                    'Snap control to bottom border of container
                    t = (ctl.Parent.DisplayRectangle.Bottom - ctl.Height)
                Else
                    'If distance to top border of container is less than snap margin
                    If Math.Abs(t) < intSnapMargin Then
                        'Snap control to top border of container
                        t = 0
                    Else
                        'Otherwise snap control to next imaginery horizontal snap line
                        t = Math.Round(ctl.Top / intSnapMargin) * intSnapMargin
                    End If
                End If

                'Set control location to snap point
                ctl.Location = New Point(l, t)
            Next
        End If

        'Reposition and show sizing handles
        UpdateHandles(ctlCurrent, New EventArgs)
    End Sub

    Private Sub Control_KeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        'Move/size selected control(s) bei arrow keys

        'If one or more controls selected
        If lstSelectedControls.Count > 0 Then
            Select Case e.KeyCode
                Case Keys.Left
                    'If shift key down
                    If e.Shift Then
                        'Decrease width of all selected controls by 1 unit
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Size = New Size(.Size.Width - 1, .Size.Height)
                            End With
                        Next
                    Else
                        'Otherwise move all selected controls 1 unit to the left
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Location = New Point(.Location.X - 1, .Location.Y)
                            End With
                        Next
                    End If

                    RepositionHandles()
                Case Keys.Right
                    'If shift key down
                    If e.Shift Then
                        'Increase width of all selected controls by 1 unit
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Size = New Size(.Size.Width + 1, .Size.Height)
                            End With
                        Next
                    Else
                        'Otherwise move all selected controls 1 unit to the right
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Location = New Point(.Location.X + 1, .Location.Y)
                            End With
                        Next
                    End If

                    RepositionHandles()
                Case Keys.Up
                    'If shift key down
                    If e.Shift Then
                        'Decrease height of all selected controls by 1 unit
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Size = New Size(.Size.Width, .Size.Height - 1)
                            End With
                        Next
                    Else
                        'Otherwise move all selected controls 1 unit up
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Location = New Point(.Location.X, .Location.Y - 1)
                            End With
                        Next
                    End If

                    RepositionHandles()
                Case Keys.Down
                    'If shift key down
                    If e.Shift Then
                        'Increase height of all selected controls by 1 unit
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Size = New Size(.Size.Width, .Size.Height + 1)
                            End With
                        Next
                    Else
                        'Otherwise move all selected controls 1 unit down
                        For Each ctl As Control In lstSelectedControls
                            With ctl
                                .Location = New Point(.Location.X, .Location.Y + 1)
                            End With
                        Next
                    End If

                    RepositionHandles()
            End Select
        End If
    End Sub
#End Region

#Region " Context menu events "
    Private Sub mnuContext_Opening(sender As Object, e As CancelEventArgs) Handles mnuContext.Opening
        'Hide alignment menu items if not in multiple selection mode
        mnuItem_Alignment.Visible = (lstSelectedControls.Count > 1)
    End Sub

    Private Sub mnuItem_Properties_Click()
        'Show properties dialog for selected control(s)
        ShowProperties()
    End Sub

    Private Sub mnuItem_AlignLeft_Click()
        'Align all selected controls to the left
        Dim intLeft As Integer = Integer.MaxValue

        'Find smallest left position
        For Each ctl As Control In lstSelectedControls
            If ctl.Left < intLeft Then
                intLeft = ctl.Left
            End If
        Next

        'Reposition all controls to the smallest left position
        For Each ctl As Control In lstSelectedControls
            ctl.Left = intLeft
        Next
    End Sub

    Private Sub mnuItem_AlignRight_Click()
        'Align all selected controls to the right
        Dim intRight As Integer = 0

        'Find highest right position
        For Each ctl As Control In lstSelectedControls
            If ctl.Right > intRight Then
                intRight = ctl.Right
            End If
        Next

        'Reposition all controls to the highest right position
        For Each ctl As Control In lstSelectedControls
            ctl.Location = New Point(intRight - ctl.Width, ctl.Top)
        Next
    End Sub

    Private Sub mnuItem_AlignTop_Click()
        'Align all selected controls to the right
        Dim intTop As Integer = Integer.MaxValue

        'Find smallest top position
        For Each ctl As Control In lstSelectedControls
            If ctl.Top < intTop Then
                intTop = ctl.Top
            End If
        Next

        'Reposition all controls to the smallest top position
        For Each ctl As Control In lstSelectedControls
            ctl.Top = intTop
        Next
    End Sub

    Private Sub mnuItem_AlignBottom_Click()
        'Align all selected controls to the bottom
        Dim intBottom As Integer = 0

        'Find highest bottom position
        For Each ctl As Control In lstSelectedControls
            If ctl.Bottom > intBottom Then
                intBottom = ctl.Bottom
            End If
        Next

        'Reposition all controls to the highest bottom position
        For Each ctl As Control In lstSelectedControls
            ctl.Location = New Point(ctl.Left, intBottom - ctl.Height)
        Next
    End Sub
#End Region

#Region " Handle methods "
    Private Sub UpdateHandles(ByVal sender As Object, ByVal e As EventArgs)
        'Position sizing handles around selected control(s)
        RepositionHandles()

        'Display sizing handles
        ShowHandles()
    End Sub

    Private Sub ShowHandles()
        'Displays the sizing handles depending on selected control(s)
        If lstSelectedControls.Count > 1 Then
            'Multiple selection - show all sizing handles
            For i As Integer = 0 To 7
                lblHandle(i).Visible = True
                lblHandle(i).BringToFront()
            Next
        Else
            'Single selection - if control selected
            If ctlCurrent IsNot Nothing Then
                'Check control type
                Select Case ctlCurrent.GetType.ToString
                    Case "System.Windows.Forms.TextBox"
                        Dim tempText As TextBox = ctlCurrent

                        If tempText.Multiline = True Then
                            'Multi-line text box - show all sizing handles
                            For i As Integer = 0 To 7
                                lblHandle(i).Visible = True
                                lblHandle(i).BringToFront()
                            Next
                        Else
                            'Single line text box - show horizontal sizing handles only
                            For i As Integer = 0 To 7
                                Select Case i
                                    Case 3, 7
                                        lblHandle(i).Visible = True
                                        lblHandle(i).BringToFront()
                                    Case Else
                                        lblHandle(i).Visible = False
                                End Select
                            Next
                        End If
                    Case "System.Windows.Forms.DateTimePicker"
                        'Show only horizontal sizing handles
                        For i As Integer = 0 To 7
                            Select Case i
                                Case 3, 7
                                    lblHandle(i).Visible = True
                                    lblHandle(i).BringToFront()
                                Case Else
                                    lblHandle(i).Visible = False
                            End Select
                        Next
                    Case "System.Windows.Forms.ComboBox"
                        Dim tempCombo As ComboBox = ctlCurrent

                        If tempCombo.DropDownStyle = ComboBoxStyle.Simple = True Then
                            'Static dropped down combobox - show all sizing handles
                            For i As Integer = 0 To 7
                                lblHandle(i).Visible = True
                                lblHandle(i).BringToFront()
                            Next
                        Else
                            'Single line combo box - show horizontal sizing handles only
                            For i As Integer = 0 To 7
                                Select Case i
                                    Case 3, 7
                                        lblHandle(i).Visible = True
                                        lblHandle(i).BringToFront()
                                    Case Else
                                        lblHandle(i).Visible = False
                                End Select
                            Next
                        End If
                    Case Else
                        'Standard control - assume that all sizing directions possible
                        For i As Integer = 0 To 7
                            lblHandle(i).Visible = True
                            lblHandle(i).BringToFront()
                        Next
                End Select
            End If
        End If
    End Sub

    Private Sub HideHandles()
        For i As Integer = 0 To 7
            lblHandle(i).Visible = False
        Next
    End Sub

    Private Sub RepositionHandles()
        Dim sX As Integer = Integer.MaxValue
        Dim sY As Integer = Integer.MaxValue
        Dim sW As Integer = 0
        Dim sH As Integer = 0
        Dim hB As Integer = HANDLESIZE / 2

        'On multiple selection mode
        If lstSelectedControls.Count > 1 Then
            'Find overall selection area (including all selected controls)
            For Each ctl As Control In lstSelectedControls
                If ctl.Bounds.Left < sX Then
                    sX = ctl.Left
                End If

                If ctl.Bounds.Top < sY Then
                    sY = ctl.Top
                End If

                If ctl.Bounds.Right > sW Then
                    sW = ctl.Bounds.Right
                End If

                If ctl.Bounds.Bottom > sH Then
                    sH = ctl.Bounds.Bottom
                End If
            Next

            'Calculate final left, top, width and height
            sX = sX - HANDLESIZE
            sY = sY - HANDLESIZE
            sW = sW - sX + HANDLESIZE
            sH = sH - sY + HANDLESIZE
        Else
            'Single selection mode - calculate left, top, width and height for current control
            sX = ctlCurrent.Left - HANDLESIZE
            sY = ctlCurrent.Top - HANDLESIZE
            sW = ctlCurrent.Width + HANDLESIZE
            sH = ctlCurrent.Height + HANDLESIZE
        End If

        'Populate array of horizontal position coordinates for all sizing handles in
        'the following order: left-top, middle-top, right-top, right-middle, 
        '                     right-bottom, middle-bottom, left-bottom, left-middle
        Dim arrPosX = New Integer() {sX + hB, sX + sW / 2, sX + sW - hB,
                                     sX + sW - hB, sX + sW - hB, sX + sW / 2, _
                                     sX + hB, sX + hB}

        'Populate array of vertical position coordinates for all sizing handles in
        'the same order
        Dim arrPosY = New Integer() {sY + hB, sY + hB, sY + hB, sY + sH / 2, _
                                     sY + sH - hB, sY + sH - hB, sY + sH - hB, _
                                     sY + sH / 2}

        'Reposition all handles
        For i As Integer = 0 To 7
            lblHandle(i).SetBounds(arrPosX(i), arrPosY(i), HANDLESIZE, HANDLESIZE)
            'Use black sizing handles on multiple selection, white sizing handles otherwise
            lblHandle(i).BackColor = IIf(lstSelectedControls.Count > 1, Color.Black, Color.White)
            lblHandle(i).BringToFront()
        Next
    End Sub
#End Region

#Region " Helper methods "
    Private Sub AddControls(ByVal ctlContainer As Object)
        'Loop through all form controls recursively
        For Each ctl As Control In ctlContainer.Controls
            'If control is not in exception list and not a sub-control of a combined control type
            If (Not lstExceptCtls.Contains(ctl)) AndAlso (ctl.GetType.ToString <> "System.Windows.Forms.ListViewGroup") AndAlso (ctl.GetType.ToString <> "System.Windows.Forms.ListViewItem") AndAlso (ctl.GetType.ToString <> "System.Windows.Forms.TreeNode") Then
                'Wrap control and add it to the list of allowed controls for design
                lstControls.Add(New ControlWrapper(ctl))

                'Hook control events used for design mode
                AddHandler ctl.Click, AddressOf SelectControl
                AddHandler ctl.MouseDown, AddressOf Me.Control_MouseDown
                AddHandler ctl.MouseMove, AddressOf Me.Control_MouseMove
                AddHandler ctl.MouseUp, AddressOf Me.Control_MouseUp
                AddHandler ctl.PreviewKeyDown, AddressOf Me.Control_KeyDown

                ''Code to be uncommented
                'If Not ctl.Parent.Equals(frmCurrent) Then
                '    'Also hook click event for all container controls
                '    AddHandler ctl.Parent.Click, AddressOf Me.Parent_Click
                'End If
                '''''''''''''''''
                If ctl.GetType.ToString = "System.Windows.Forms.TextBox" Then
                    'On textboxes hook multi-line change event to update sizing handles in that case
                    AddHandler CType(ctl, System.Windows.Forms.TextBox).MultilineChanged, AddressOf UpdateHandles
                End If

                'Change control cursor and context menu (original values are automatically stored in wrappers)
                ctl.Cursor = Cursors.Arrow
                ctl.ContextMenuStrip = mnuContext
            End If

            If TypeOf (ctl.Parent) Is common.UserControls.txtFinder Then

            End If
            'If control is a container (but not a propertygrid)
            '  If Not (TypeOf (ctl) Is common.UserControls.txtFinder OrElse TypeOf (ctl) Is common.Controls.MyCheckBox OrElse TypeOf (ctl) Is common.Controls.MyComboBox OrElse TypeOf (ctl) Is common.Controls.MyDateTimePicker OrElse TypeOf (ctl) Is common.Controls.MyRadioButton OrElse TypeOf (ctl) Is common.Controls.MyTextBox OrElse TypeOf (ctl) Is common.MyNumBox) Then
            If (ctl.Controls.Count > 0) AndAlso (ctl.GetType.ToString <> "System.Windows.Forms.PropertyGrid") Then
                'Loop through its sub-controls too (recursion)
                If TypeOf (ctl.Parent) Is common.UserControls.txtFinder Then
                    Exit Sub
                End If
                AddControls(ctl)
            End If
            'End If
        Next
    End Sub

    Private Sub Parent_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Parent container of a control clicked (deselection)
        Try
            'If current parent is the passed form itself
            If sender.Equals(frmCurrent) Then
                'Remove all selected controls from collection
                lstSelectedControls.Clear()
            End If
        Catch
        End Try

        HideHandles()
    End Sub
#End Region

#Region " Properties window "
    Private Sub ShowProperties()
        'Show the property dialog for selected control(s) or the form itself
        Dim strTitle As String = ""
        Dim btnOK As New Button
        Dim grdProps As New PropertyGrid
        Dim objFormProps As FormPropertiesWrapper = Nothing

        'Set window title depending on selection
        Select Case lstSelectedControls.Count
            Case 0
                'Form
                strTitle = "Properties for " & frmCurrent.Name
            Case 1
                'Single selection
                strTitle = "Properties for " & lstSelectedControls.Item(0).Name
            Case Else
                'Multiple selection
                strTitle = "Shared properties"
        End Select

        'Configure property grid
        With grdProps
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
            .Size = New Size(intPropsWidth - 25, intPropsHeight - 80)
            .Location = New Point(5, 5)
            .BackColor = Color.WhiteSmoke
            .Text = ""
            .HelpVisible = True
            .ToolbarVisible = True

            'Depending on selection mode
            If lstSelectedControls.Count > 0 Then
                'Assign selected control wrappers to the property grid
                .SelectedObject = New PropertyWrapper(lstSelectedControls)
            Else
                'Assign form wrapper to the property grid
                objFormProps = New FormPropertiesWrapper(frmCurrent, frmBackColor)
                .SelectedObject = objFormProps
            End If
        End With

        'Configure OK button
        With btnOK
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            .Size = New Size(50, 25)
            .Location = New Point(intPropsWidth - 70, intPropsHeight - 70)
            .BackColor = Color.Gainsboro
            .Text = "OK"
        End With

        'Create dialog window
        frmProps = New Form

        'Assign button click event handler
        AddHandler btnOK.Click, AddressOf dlgPropsOK_Click

        'Configure dialog and add property grid an OK button
        With frmProps
            .Text = strTitle
            .Size = New Size(intPropsWidth, intPropsHeight)
            .BackColor = Color.White
            .Controls.AddRange(New Control() {grdProps, btnOK})
            .Owner = frmCurrent
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Normal
            .FormBorderStyle = FormBorderStyle.SizableToolWindow

            .ShowDialog()

            'If form properties shown
            If objFormProps IsNot Nothing Then
                'Update form backcolor (will be restored when exiting design mode)
                frmBackColor = objFormProps.BackColor
            End If
        End With
    End Sub

    Private Sub dlgPropsOK_Click()
        'OK button on properties dialog clicked - close form
        frmProps.Close()
        frmProps = Nothing
    End Sub
#End Region

#Region " Control wrapper class "
    'This class is used store the original settings of each control, that are replaced while in design mode
    Private Class ControlWrapper
        'Original settings
        Private Org_Control As Control = Nothing
        Private Org_Cursor As Cursor = Nothing
        Private Org_ContextMenuStrip As ContextMenuStrip = Nothing

        'cstor
        Public Sub New(ByVal ctl As Control)
            'Store original values
            Org_Control = ctl
            Org_Cursor = ctl.Cursor
            Org_ContextMenuStrip = ctl.ContextMenuStrip
        End Sub

        Public ReadOnly Property Control() As Control
            Get
                Return Org_Control
            End Get
        End Property

        Public ReadOnly Property Cursor() As Cursor
            Get
                Return Org_Cursor
            End Get
        End Property

        Public ReadOnly Property ContextMenuStrip() As ContextMenuStrip
            Get
                Return Org_ContextMenuStrip
            End Get
        End Property
    End Class
#End Region

#Region " Control properties wrapper class "
    'This class is used to provide only supported control properties to the propertygrid of the properties dialog
    'including property grid category and according help texts
    Private Class PropertyWrapper
        Private _lstControls As List(Of Control) = Nothing

        'cstor
        Public Sub New(ByRef lstControls As List(Of Control))
            _lstControls = lstControls
        End Sub

        <DescriptionAttribute("Defines the edges of the control container, the control is anchored to. The distance to these edges will be kept when container is resized."),
        CategoryAttribute("Display")> _
        Public Property Anchor() As System.Windows.Forms.AnchorStyles
            Get
                Return _lstControls.Item(0).Anchor
            End Get
            Set(ByVal Value As System.Windows.Forms.AnchorStyles)
                For Each ctl As Control In _lstControls
                    ctl.Anchor = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Defines the control's backcolor."),
        CategoryAttribute("Display")> _
        Public Property BackColor() As Color
            Get
                Return _lstControls.Item(0).BackColor
            End Get
            Set(ByVal Value As Color)
                For Each ctl As Control In _lstControls
                    ctl.BackColor = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Defines the edge of the controls container, the control is docked to."),
        CategoryAttribute("Display")> _
        Public Property Dock() As System.Windows.Forms.DockStyle
            Get
                Return _lstControls.Item(0).Dock
            End Get
            Set(ByVal Value As System.Windows.Forms.DockStyle)
                For Each ctl As Control In _lstControls
                    ctl.Dock = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Defines the font and attributes of the control."),
        CategoryAttribute("Display")> _
        Public Property Font() As System.Drawing.Font
            Get
                Return _lstControls.Item(0).Font
            End Get
            Set(ByVal Value As System.Drawing.Font)
                For Each ctl As Control In _lstControls
                    ctl.Font = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Defines the control's forecolor."),
        CategoryAttribute("Display")> _
        Public Property ForeColor() As Color
            Get
                Return _lstControls.Item(0).ForeColor
            End Get
            Set(ByVal Value As Color)
                For Each ctl As Control In _lstControls
                    ctl.ForeColor = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Detemines the control's height."),
        CategoryAttribute("Display")> _
        Public Property Height() As Integer
            Get
                Return _lstControls.Item(0).Height
            End Get
            Set(ByVal Value As Integer)
                For Each ctl As Control In _lstControls
                    ctl.Height = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's distance from its containers left border."),
        CategoryAttribute("Display")> _
        Public Property Left() As Integer
            Get
                Return _lstControls.Item(0).Left
            End Get
            Set(ByVal Value As Integer)
                For Each ctl As Control In _lstControls
                    ctl.Left = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's distance from its containers left and top borders."),
        CategoryAttribute("Display")> _
        Public Property Location() As System.Drawing.Point
            Get
                Return _lstControls.Item(0).Location
            End Get
            Set(ByVal Value As System.Drawing.Point)
                For Each ctl As Control In _lstControls
                    ctl.Location = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the distance between this and other controls for all 4 edges."),
        CategoryAttribute("Display")> _
        Public Property Margin() As System.Windows.Forms.Padding
            Get
                Return _lstControls.Item(0).Margin
            End Get
            Set(ByVal Value As System.Windows.Forms.Padding)
                For Each ctl As Control In _lstControls
                    ctl.Margin = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's maximum size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property MaximumSize() As System.Drawing.Size
            Get
                Return _lstControls.Item(0).MaximumSize
            End Get
            Set(ByVal Value As System.Drawing.Size)
                For Each ctl As Control In _lstControls
                    ctl.MaximumSize = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's minimum size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property MinimumSize() As System.Drawing.Size
            Get
                Return _lstControls.Item(0).MinimumSize
            End Get
            Set(ByVal Value As System.Drawing.Size)
                For Each ctl As Control In _lstControls
                    ctl.MinimumSize = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Detemines the controls inner margin."),
        CategoryAttribute("Display")> _
        Public Property Padding() As System.Windows.Forms.Padding
            Get
                Return _lstControls.Item(0).Padding
            End Get
            Set(ByVal Value As System.Windows.Forms.Padding)
                For Each ctl As Control In _lstControls
                    ctl.Padding = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's actual size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property Size() As System.Drawing.Size
            Get
                Return _lstControls.Item(0).Size
            End Get
            Set(ByVal Value As System.Drawing.Size)
                For Each ctl As Control In _lstControls
                    ctl.Size = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's position within the tab order of it's container."),
        CategoryAttribute("Display")> _
        Public Property TabIndex() As Integer
            Get
                Return _lstControls.Item(0).TabIndex
            End Get
            Set(ByVal Value As Integer)
                For Each ctl As Control In _lstControls
                    ctl.TabIndex = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the text, assigned to the control."),
        CategoryAttribute("Display")> _
        Public Property Text() As String
            Get
                Return _lstControls.Item(0).Text
            End Get
            Set(ByVal Value As String)
                For Each ctl As Control In _lstControls
                    ctl.Text = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the control's distance from its containers top border."),
        CategoryAttribute("Display")> _
        Public Property Top() As Integer
            Get
                Return _lstControls.Item(0).Top
            End Get
            Set(ByVal Value As Integer)
                For Each ctl As Control In _lstControls
                    ctl.Top = Value
                Next
            End Set
        End Property

        <DescriptionAttribute("Determines the width of the control."),
        CategoryAttribute("Display")> _
        Public Property Width() As Integer
            Get
                Return _lstControls.Item(0).Width
            End Get
            Set(ByVal Value As Integer)
                For Each ctl As Control In _lstControls
                    ctl.Width = Value
                Next
            End Set
        End Property
    End Class
#End Region

#Region " Form properties wrapper class "
    'This class is used to provide only supported form properties to the propertygrid of the properties dialog
    'including property grid category and according help texts
    Private Class FormPropertiesWrapper
        Private _Form As Form
        Private _Form_BackColor As Color

        Public Sub New(ByVal Form As Form, ByVal BackColor As Color)
            _Form = Form
            _Form_BackColor = BackColor
        End Sub
        <DescriptionAttribute("Defines the form's backcolor."),
        CategoryAttribute("Display")> _
        Public Property BackColor() As Color
            Get
                Return _Form_BackColor
            End Get
            Set(ByVal Value As Color)
                _Form_BackColor = Value
            End Set
        End Property

        <DescriptionAttribute("Defines the forms font and attributes."),
        CategoryAttribute("Display")> _
        Public Property Font() As System.Drawing.Font
            Get
                Return _Form.Font
            End Get
            Set(ByVal Value As System.Drawing.Font)
                _Form.Font = Value
            End Set
        End Property

        <DescriptionAttribute("Defines the form's forecolor."),
        CategoryAttribute("Display")> _
        Public Property ForeColor() As Color
            Get
                Return _Form.ForeColor
            End Get
            Set(ByVal Value As Color)
                _Form.ForeColor = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the form's distance to the left and top border of the desktop."),
        CategoryAttribute("Display")> _
        Public Property Location() As System.Drawing.Point
            Get
                Return _Form.Location
            End Get
            Set(ByVal Value As System.Drawing.Point)
                _Form.Location = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the form's maximum size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property MaximumSize() As System.Drawing.Size
            Get
                Return _Form.MaximumSize
            End Get
            Set(ByVal Value As System.Drawing.Size)
                _Form.MaximumSize = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the form's minimum size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property MinimumSize() As System.Drawing.Size
            Get
                Return _Form.MinimumSize
            End Get
            Set(ByVal Value As System.Drawing.Size)
                _Form.MinimumSize = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the form's inner margin."),
        CategoryAttribute("Display")> _
        Public Property Padding() As System.Windows.Forms.Padding
            Get
                Return _Form.Padding
            End Get
            Set(ByVal Value As System.Windows.Forms.Padding)
                _Form.Padding = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the form's actual size (width and height)."),
        CategoryAttribute("Display")> _
        Public Property Size() As System.Drawing.Size
            Get
                Return _Form.Size
            End Get
            Set(ByVal Value As System.Drawing.Size)
                _Form.Size = Value
            End Set
        End Property

        <DescriptionAttribute("Determines the window title."),
        CategoryAttribute("Display")> _
        Public Property Text() As String
            Get
                Return _Form.Text
            End Get
            Set(ByVal Value As String)
                _Form.Text = Value
            End Set
        End Property
    End Class
#End Region
End Class

Public Class ControlManager
    'This class contains the methods for loading and saving the settings of a complete form
#Region " Declarations "
    Public lstCtlInfo As List(Of ControlInfo) = Nothing
    Public lstExceptControls As List(Of Control) = Nothing
#End Region

#Region " Public interface "
    Public Sub SaveProperties(ByRef Form As Form, ByVal ExceptControls As List(Of Control), Optional ByVal FilePath As String = "")
        'Saves the supported properties of the passed form and all its controls (except those in exception list).

        'Create collection of controls to save (will be serialized to xml file)
        lstCtlInfo = New List(Of ControlInfo)

        'Get list of controls to exclude from saving
        lstExceptControls = ExceptControls

        Dim strFilePath As String = ""

        'If no path and file name specified where to save the control settings
        If FilePath.Length = 0 Then
            'Use standard path and file name
            strFilePath = Path.Combine(Application.StartupPath, objCommonVar.CurrentUserCode)
            If Not My.Computer.FileSystem.DirectoryExists(strFilePath) Then
                Directory.CreateDirectory(strFilePath)
            End If
            strFilePath = Path.Combine(strFilePath, Form.Name & ".config")
        Else
            strFilePath = FilePath
        End If

        'Create form wrapper
        Dim objFrmInfo As New ControlInfo

        'Get supported form settings
        With objFrmInfo
            .Name = Form.Name

            Try
                .BackColor = Form.BackColor
            Catch
            End Try

            Try
                .Font = Form.Font
            Catch
            End Try

            Try
                .ForeColor = Form.ForeColor
            Catch
            End Try

            Try
                .Location = Form.Location
            Catch
            End Try

            Try
                .MaximumSize = Form.MaximumSize
            Catch
            End Try

            Try
                .MinimumSize = Form.MinimumSize
            Catch
            End Try

            Try
                .Padding = Form.Padding
            Catch
            End Try

            Try
                .Size = Form.Size
            Catch
            End Try

            Try
                .Text = Form.Text
            Catch
            End Try
        End With

        'Add form wrapper to control collection
        lstCtlInfo.Add(objFrmInfo)

        'Add settings for all controls in passed form (except those in exception list)
        AddControls(Form)

        'Serialize the control collection as xml file
        Serialize(Of List(Of ControlInfo))(lstCtlInfo, strFilePath)
    End Sub

    Public Sub RestoreProperties(ByRef Form As Form, Optional ByVal FilePath As String = "")
        'Restores the supported properties of the passed form and all controls as stored in xml file.
        Dim strFilePath As String = ""

        'If no path and file name specified where to find the control settings
        If FilePath.Length = 0 Then
            'Use standard path and file name
            strFilePath = Path.Combine(Application.StartupPath, objCommonVar.CurrentUserCode)
            If Not My.Computer.FileSystem.DirectoryExists(strFilePath) Then
                Directory.CreateDirectory(strFilePath)
            End If
            strFilePath = Path.Combine(strFilePath, Form.Name & ".config")
        Else
            strFilePath = FilePath
        End If

        'If file exists
        If My.Computer.FileSystem.FileExists(strFilePath) Then
            'Restore  controls collection from xml file
            lstCtlInfo = Deserialize(Of List(Of ControlInfo))(strFilePath)

            'With each control info 
            For Each ctlInfo As ControlInfo In lstCtlInfo
                'If info belongs to form
                If ctlInfo.Name = Form.Name Then
                    'Restore supported form settings
                    With Form
                        Try
                            .BackColor = ctlInfo.BackColor
                        Catch
                        End Try

                        Try
                            .Font = ctlInfo.Font
                        Catch
                        End Try

                        Try
                            .ForeColor = ctlInfo.ForeColor
                        Catch
                        End Try

                        Try
                            .Location = ctlInfo.Location
                        Catch
                        End Try

                        Try
                            .MaximumSize = ctlInfo.MaximumSize
                        Catch
                        End Try

                        Try
                            .MinimumSize = ctlInfo.MinimumSize
                        Catch
                        End Try

                        Try
                            .Padding = ctlInfo.Padding
                        Catch
                        End Try

                        Try
                            .Size = ctlInfo.Size
                        Catch
                        End Try

                        Try
                            .Text = ctlInfo.Text
                        Catch
                        End Try
                    End With
                Else
                    'Otherwise find belonging control in passed form
                    Dim ctl As Control = GetControl(Form, ctlInfo.Name)

                    'If found
                    If ctl IsNot Nothing Then
                        'Restore supported control settings
                        With ctl
                            Try
                                .Anchor = ctlInfo.Anchor
                            Catch
                            End Try

                            Try
                                .BackColor = ctlInfo.BackColor
                            Catch
                            End Try

                            Try
                                .Dock = ctlInfo.Dock
                            Catch
                            End Try

                            Try
                                .Font = ctlInfo.Font
                            Catch
                            End Try

                            Try
                                .ForeColor = ctlInfo.ForeColor
                            Catch
                            End Try

                            Try
                                .Location = ctlInfo.Location
                            Catch
                            End Try

                            Try
                                .Margin = ctlInfo.Margin
                            Catch
                            End Try

                            Try
                                .MaximumSize = ctlInfo.MaximumSize
                            Catch
                            End Try

                            Try
                                .MinimumSize = ctlInfo.MinimumSize
                            Catch
                            End Try

                            Try
                                .Padding = ctlInfo.Padding
                            Catch
                            End Try

                            Try
                                .Size = ctlInfo.Size
                            Catch
                            End Try

                            Try
                                .TabIndex = ctlInfo.TabIndex
                            Catch
                            End Try

                            Try
                                .Text = ctlInfo.Text
                            Catch
                            End Try
                        End With
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub Dispose()
        'Terminate the control manager and release its objects
        If lstCtlInfo IsNot Nothing Then
            lstCtlInfo.Clear()

            lstCtlInfo = Nothing
        End If
    End Sub

    Private Sub AddControls(ByVal ctlContainer As Object)
        'Loop through all form controls recursively
        For Each ctl As Control In ctlContainer.Controls
            'If control is not in exception list
            If Not lstExceptControls.Contains(ctl) Then
                'Create a control info object
                Dim objCtlInfo As New ControlInfo

                'Get all supported control properties
                With objCtlInfo
                    .Name = ctl.Name

                    Try
                        .Anchor = ctl.Anchor
                    Catch
                    End Try

                    Try
                        .BackColor = ctl.BackColor
                    Catch
                    End Try

                    Try
                        .Dock = ctl.Dock
                    Catch
                    End Try

                    Try
                        .Font = ctl.Font
                    Catch
                    End Try

                    Try
                        .ForeColor = ctl.ForeColor
                    Catch
                    End Try

                    Try
                        .Location = ctl.Location
                    Catch
                    End Try

                    Try
                        .Margin = ctl.Margin
                    Catch
                    End Try

                    Try
                        .MaximumSize = ctl.MaximumSize
                    Catch
                    End Try

                    Try
                        .MinimumSize = ctl.MinimumSize
                    Catch
                    End Try

                    Try
                        .Padding = ctl.Padding
                    Catch
                    End Try

                    Try
                        .Size = ctl.Size
                    Catch
                    End Try

                    Try
                        .TabIndex = ctl.TabIndex
                    Catch
                    End Try

                    Try
                        .Text = ctl.Text
                    Catch
                    End Try
                End With

                'Add control info to control collection
                lstCtlInfo.Add(objCtlInfo)

                'If control is a container (but not a propertygrid)
                If Not (TypeOf (ctl) Is common.UserControls.txtFinder OrElse TypeOf (ctl) Is common.Controls.MyCheckBox OrElse TypeOf (ctl) Is common.Controls.MyComboBox OrElse TypeOf (ctl) Is common.Controls.MyDateTimePicker OrElse TypeOf (ctl) Is common.Controls.MyRadioButton OrElse TypeOf (ctl) Is common.Controls.MyTextBox OrElse TypeOf (ctl) Is common.MyNumBox) Then
                    If ctl.Controls.Count > 0 Then
                        'Loop through its sub-controls too (recursion)
                        AddControls(ctl)
                    End If
                End If
            End If
        Next
    End Sub

    Private Function GetControl(ByVal ctlContainer As Object, ByVal ControlName As String) As Control
        'Find a control recursively by its name in passed form
        Dim objControl As Control = Nothing

        'Loop through all form controls
        For Each ctl As Control In ctlContainer.Controls
            If ctl.Name = ControlName Then
                objControl = ctl
            Else
                'If control is a container
                If Not (TypeOf (ctl) Is common.UserControls.txtFinder OrElse TypeOf (ctl) Is common.Controls.MyCheckBox OrElse TypeOf (ctl) Is common.Controls.MyComboBox OrElse TypeOf (ctl) Is common.Controls.MyDateTimePicker OrElse TypeOf (ctl) Is common.Controls.MyRadioButton OrElse TypeOf (ctl) Is common.Controls.MyTextBox OrElse TypeOf (ctl) Is common.MyNumBox) Then
                    If ctl.Controls.Count > 0 Then
                        'Search in its sub.controls too (recursion)
                        objControl = GetControl(ctl, ControlName)
                    End If
                End If
                End If

                'If found
                If objControl IsNot Nothing Then
                    'exit loop/recursion
                    Exit For
                End If
        Next

        Return objControl
    End Function
#End Region

#Region " Serialization methods "
    Private Sub Serialize(Of T)(ByVal objToSave As T, ByVal FilePath As String)
        'Serializes any object as xml file to the specified path
        Dim objXMLSerializer As New XmlSerializer(GetType(T))
        Dim objSettings As New XmlWriterSettings()

        With objSettings
            .Indent = True
            .CloseOutput = True
        End With

        Dim objWriter As XmlWriter = XmlWriter.Create(FilePath, objSettings)

        objXMLSerializer.Serialize(objWriter, objToSave)

        With objWriter
            .Flush()
            .Close()
        End With
    End Sub

    Private Function Deserialize(Of T)(ByVal FilePath As String) As T
        'Deserializes any object from the specified xml file
        Dim objXMLSerializer As New XmlSerializer(GetType(T))
        Dim objFS As New FileStream(FilePath, FileMode.Open)
        Dim objReader As New XmlTextReader(objFS)

        If objXMLSerializer.CanDeserialize(objReader) Then
            Dim objTemp As Object = DirectCast(objXMLSerializer.Deserialize(objReader), T)
            objReader.Close()
            objFS.Close()
            Return objTemp
        Else
            Return Nothing
        End If
    End Function
#End Region
End Class

<System.Serializable> _
Public Class ControlInfo
    'This class is used by ControlManager for serialization and converts some settings to an xml-conform notation
#Region " Declarations "
    Private _Anchor As Integer
    Private _BackColor As Color
    Private _Dock As Integer
    Private _ForeColor As Color
    Private _Margin As System.Windows.Forms.Padding
    Private _Name As String
    Private _Padding As System.Windows.Forms.Padding
    Private _TabIndex As Integer
    Private _Text As String
    Private _Left As Integer
    Private _Top As Integer
    Private _Width As Integer
    Private _Height As Integer
    Private _MinWidth As Integer
    Private _MinHeight As Integer
    Private _MaxWidth As Integer
    Private _MaxHeight As Integer
    Private _Pad_Bottom As Integer
    Private _Pad_Left As Integer
    Private _Pad_Right As Integer
    Private _Pad_Top As Integer
    Private _Marg_Bottom As Integer
    Private _Marg_Left As Integer
    Private _Marg_Right As Integer
    Private _Marg_Top As Integer
    Private _Font_Name As String
    Private _Font_Size As Single
    Private _Font_Style As Integer
    Private _Font_Unit As Integer
#End Region

#Region " Members "
    Public Property Anchor() As Integer
        Get
            Return _Anchor
        End Get
        Set(ByVal Value As Integer)
            _Anchor = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal Value As Color)
            _BackColor = Value
        End Set
    End Property

    <Xml.Serialization.XmlElement("BackColor")> _
    Public Property BackColorXML() As String
        Get
            Return ColorTranslator.ToHtml(_BackColor)
        End Get
        Set(ByVal Value As String)
            _BackColor = ColorTranslator.FromHtml(Value)
        End Set
    End Property

    Public Property Dock() As Integer
        Get
            Return _Dock
        End Get
        Set(ByVal Value As Integer)
            _Dock = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property Font() As System.Drawing.Font
        Get
            Return New Font(_Font_Name, _Font_Size, _Font_Style, _Font_Unit)
        End Get
        Set(ByVal Value As System.Drawing.Font)
            _Font_Name = Value.FontFamily.Name
            _Font_Size = Value.Size
            _Font_Style = Value.Style
            _Font_Unit = Value.Unit
        End Set
    End Property

    Public Property Font_Name() As String
        Get
            Return _Font_Name
        End Get
        Set(ByVal Value As String)
            _Font_Name = Value
        End Set
    End Property

    Public Property Font_Size() As Single
        Get
            Return _Font_Size
        End Get
        Set(ByVal Value As Single)
            _Font_Size = Value
        End Set
    End Property

    Public Property Font_Style() As Integer
        Get
            Return _Font_Style
        End Get
        Set(ByVal Value As Integer)
            _Font_Style = Value
        End Set
    End Property

    Public Property Font_Unit() As Integer
        Get
            Return _Font_Unit
        End Get
        Set(ByVal Value As Integer)
            _Font_Unit = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property ForeColor() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal Value As Color)
            _ForeColor = Value
        End Set
    End Property

    <Xml.Serialization.XmlElement("ForeColor")> _
    Public Property ForeColorXML() As String
        Get
            Return ColorTranslator.ToHtml(_ForeColor)
        End Get
        Set(ByVal Value As String)
            _ForeColor = ColorTranslator.FromHtml(Value)
        End Set
    End Property

    Public Property Height() As Integer
        Get
            Return _Height
        End Get
        Set(ByVal Value As Integer)
            _Height = Value
        End Set
    End Property

    Public Property Left() As Integer
        Get
            Return _Left
        End Get
        Set(ByVal Value As Integer)
            _Left = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property Location() As System.Drawing.Point
        Get
            Return New Point(_Left, _Top)
        End Get
        Set(ByVal Value As System.Drawing.Point)
            _Left = Value.X
            _Top = Value.Y
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property Margin() As System.Windows.Forms.Padding
        Get
            Return New Padding(_Marg_Left, _Marg_Top, _Marg_Right, _Marg_Bottom)
        End Get
        Set(ByVal Value As System.Windows.Forms.Padding)
            _Marg_Left = Value.Left
            _Marg_Top = Value.Top
            _Marg_Right = Value.Right
            _Marg_Bottom = Value.Bottom
        End Set
    End Property

    Public Property Margin_Bottom() As Integer
        Get
            Return _Marg_Bottom
        End Get
        Set(ByVal Value As Integer)
            _Marg_Bottom = Value
        End Set
    End Property

    Public Property Margin_Left() As Integer
        Get
            Return _Marg_Left
        End Get
        Set(ByVal Value As Integer)
            _Marg_Left = Value
        End Set
    End Property

    Public Property Margin_Right() As Integer
        Get
            Return _Marg_Right
        End Get
        Set(ByVal Value As Integer)
            _Marg_Right = Value
        End Set
    End Property

    Public Property Margin_Top() As Integer
        Get
            Return _Marg_Top
        End Get
        Set(ByVal Value As Integer)
            _Marg_Top = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property MaximumSize() As System.Drawing.Size
        Get
            Return New Size(_MaxWidth, _MaxHeight)
        End Get
        Set(ByVal Value As System.Drawing.Size)
            _MaxWidth = Value.Width
            _MaxHeight = Value.Height
        End Set
    End Property

    Public Property MaxHeight() As Integer
        Get
            Return _MaxHeight
        End Get
        Set(ByVal Value As Integer)
            _MaxHeight = Value
        End Set
    End Property

    Public Property MaxWidth() As Integer
        Get
            Return _MaxWidth
        End Get
        Set(ByVal Value As Integer)
            _MaxWidth = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property MinimumSize() As System.Drawing.Size
        Get
            Return New Size(_MinWidth, _MinHeight)
        End Get
        Set(ByVal Value As System.Drawing.Size)
            _MinWidth = Value.Width
            _MinHeight = Value.Height
        End Set
    End Property

    Public Property MinHeight() As Integer
        Get
            Return _MinHeight
        End Get
        Set(ByVal Value As Integer)
            _MinHeight = Value
        End Set
    End Property

    Public Property MinWidth() As Integer
        Get
            Return _MinWidth
        End Get
        Set(ByVal Value As Integer)
            _MinWidth = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property Padding() As System.Windows.Forms.Padding
        Get
            Return New Padding(_Pad_Left, _Pad_Top, _Pad_Right, _Pad_Bottom)
        End Get
        Set(ByVal Value As System.Windows.Forms.Padding)
            _Pad_Left = Value.Left
            _Pad_Top = Value.Top
            _Pad_Right = Value.Right
            _Pad_Bottom = Value.Bottom
        End Set
    End Property

    Public Property Padding_Bottom() As Integer
        Get
            Return _Pad_Bottom
        End Get
        Set(ByVal Value As Integer)
            _Pad_Bottom = Value
        End Set
    End Property

    Public Property Padding_Left() As Integer
        Get
            Return _Pad_Left
        End Get
        Set(ByVal Value As Integer)
            _Pad_Left = Value
        End Set
    End Property

    Public Property Padding_Right() As Integer
        Get
            Return _Pad_Right
        End Get
        Set(ByVal Value As Integer)
            _Pad_Right = Value
        End Set
    End Property

    Public Property Padding_Top() As Integer
        Get
            Return _Pad_Top
        End Get
        Set(ByVal Value As Integer)
            _Pad_Top = Value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore> _
    Public Property Size() As System.Drawing.Size
        Get
            Return New Size(_Width, _Height)
        End Get
        Set(ByVal Value As System.Drawing.Size)
            _Width = Value.Width
            _Height = Value.Height
        End Set
    End Property

    Public Property TabIndex() As Integer
        Get
            Return _TabIndex
        End Get
        Set(ByVal Value As Integer)
            _TabIndex = Value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal Value As String)
            _Text = Value
        End Set
    End Property

    Public Property Top() As Integer
        Get
            Return _Top
        End Get
        Set(ByVal Value As Integer)
            _Top = Value
        End Set
    End Property

    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal Value As Integer)
            _Width = Value
        End Set
    End Property
#End Region
End Class