Imports common
Public Class FrmCarousal
#Region "variables"
    Private Const CISystemAdmin As String = "CISystemAdmin"
    Private Const CICommonServices As String = "CICommonServices"
    Private Const CIReceivable As String = "CIReceivable"
    Private Const CIPayable As String = "CIPayable"
    Private Const CIGL As String = "CIGL"
    Private Const CISales As String = "CISales"
    Private Const CIMaterial As String = "CIMaterial"
    Private Const CIPurchase As String = "CIPurchase"
    Private Const CITDS As String = "CITDS"
    Private Const CIFixedAsset As String = "CIFixedAsset"
    Private Const CIHR As String = "CIHR"
    Private Const CIProduction As String = "CIProduction"
#End Region
    Dim frmMain As MDI
    Public Sub New(ByRef frm As MDI)
        InitializeComponent()
        'frmMain = frm
        '  Me.Parent = frm
    End Sub


    Private Sub FrmCarousal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadCarousel1.AutoLoopPauseCondition = Telerik.WinControls.UI.AutoLoopPauseConditions.None


        'Dim carouselEllipsePath1 As New Telerik.WinControls.UI.CarouselEllipsePath()
        'carouselEllipsePath1.Center = New Telerik.WinControls.UI.Point3D(49.358974358974358, 46.315789473684212, -20)
        'carouselEllipsePath1.FinalAngle = 60
        'carouselEllipsePath1.InitialAngle = 60
        'carouselEllipsePath1.U = New Telerik.WinControls.UI.Point3D(37.93530426465815, -18.191666666666663, 0)
        'carouselEllipsePath1.V = New Telerik.WinControls.UI.Point3D(-11.489983091663683, -15.391666666666662, -20)
        'carouselEllipsePath1.ZScale = 60

        Dim carouselEllipsePath1 As New Telerik.WinControls.UI.CarouselBezierPath()
        carouselEllipsePath1.CtrlPoint1 = New Telerik.WinControls.UI.Point3D(12.278630460448642, 48.166259168704158, 300)
        carouselEllipsePath1.CtrlPoint2 = New Telerik.WinControls.UI.Point3D(87.839433293978743, 47.677261613691932, 300)
        carouselEllipsePath1.FirstPoint = New Telerik.WinControls.UI.Point3D(3.3754427390791024, 50.8557457212714, -400)
        carouselEllipsePath1.LastPoint = New Telerik.WinControls.UI.Point3D(96.860684769775673, 51.100244498777506, -400)
        carouselEllipsePath1.ZScale = 200
        RadCarousel1.VisibleItemCount = 3


        RadCarousel1.CarouselPath = carouselEllipsePath1
        RadCarousel1.Dock = System.Windows.Forms.DockStyle.Fill
        RadCarousel1.EnableAutoLoop = True
        RadCarousel1.AutoLoopDirection = AutoLoopDirections.Forward
        RadCarousel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
        RadCarousel1.ForeColor = System.Drawing.Color.Black
        RadCarousel1.ImageScalingSize = New System.Drawing.Size(0, 0)
        'RadCarousel1.Items.AddRange(New Telerik.WinControls.RadItem() {radButtonElement1, radButtonElement2, radButtonElement3, radButtonElement4, radButtonElement5, radButtonElement6, radButtonElement7, radButtonElement8, radButtonElement9})
        RadCarousel1.Location = New System.Drawing.Point(0, 132)
        RadCarousel1.MinFadeOpacity = 0.5
        'RadCarousel1.Name = "RadCarousel1"
        RadCarousel1.NavigationButtonsOffset = New System.Drawing.Size(15, 15)


        Dim imagePrimitive = New ImagePrimitive()
        imagePrimitive.Image = Global.ERP.My.Resources.Resources.BackImageXpertERP
        imagePrimitive.Alignment = ContentAlignment.TopRight
        imagePrimitive.StretchHorizontally = False
        imagePrimitive.StretchVertically = False
        imagePrimitive.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed
        RadCarousel1.CarouselElement.Children.Insert(1, imagePrimitive)


    End Sub

    Private Sub RadCarousel1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadCarousel1.DoubleClick
        Try
            Dim strRTV2 As RadTreeView = CType(Me.ParentForm.Controls.Item(0).Controls.Item("ToolTabStrip2").Controls.Item("ToolWindow2").Controls.Item(0), RadTreeView)
            strRTV2.Nodes(0).Nodes(0).Nodes.TreeView.CollapseAll()
            strRTV2.Nodes(0).Expand()
            strRTV2.Nodes(0).Nodes(RadCarousel1.Items(RadCarousel1.SelectedIndex).Name).ExpandAll()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
