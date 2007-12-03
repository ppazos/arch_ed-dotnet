'
'
'	component:   "openEHR Archetype Project"
'	description: "Control that represents the constraints applied to all nodes in an archetype - occurrences, description and runtime name"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Strict On

Public Class ArchetypeNodeConstraintControl
    Inherits System.Windows.Forms.UserControl

    '    Private AnyConstraints As AnyConstraintControl
    Private mConstraintControl As ConstraintControl
    Private mFileManager As FileManagerLocal
    Private mDataView As DataView
    Friend WithEvents tabConstraint As System.Windows.Forms.TabControl
    Friend WithEvents tpConstraint As System.Windows.Forms.TabPage
    Friend WithEvents tpConstraintDetails As System.Windows.Forms.TabPage
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents gbComments As System.Windows.Forms.GroupBox
    Friend WithEvents gbTerminology As System.Windows.Forms.GroupBox
    Friend WithEvents dgNodeBindings As System.Windows.Forms.DataGridView
    Friend WithEvents dgValueSets As System.Windows.Forms.DataGridView
    Friend WithEvents gbValueSets As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents termLookUp As OTSControls.Term
    Friend WithEvents terminology As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Path As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gbNullFlavours As System.Windows.Forms.GroupBox
    Friend WithEvents chkListNull As System.Windows.Forms.CheckedListBox
    Friend WithEvents mOccurrences As OccurrencesPanel

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal a_file_manager As FileManagerLocal)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        mIsLoading = True

        mFileManager = a_file_manager
        mDataView = New DataView(mFileManager.OntologyManager.TermBindingsTable)

        mOccurrences = New OccurrencesPanel(mFileManager)

        Select Case OceanArchetypeEditor.Instance.Options.OccurrencesView
            Case "lexical"
                mOccurrences.Mode = OccurrencesMode.Lexical
            Case "numeric"
                mOccurrences.Mode = OccurrencesMode.Numeric
        End Select

        Me.PanelGenericConstraint.Controls.Add(mOccurrences)

        mOccurrences.Dock = DockStyle.Fill

        If OceanArchetypeEditor.DefaultLanguageCode <> "en" Then
            TranslateGUI()
        End If

        mIsLoading = False

        Me.HelpProviderCommonConstraint.HelpNamespace = OceanArchetypeEditor.Instance.Options.HelpLocationPath

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Protected components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PanelGenericConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelDataConstraint As System.Windows.Forms.Panel
    Friend WithEvents PanelNonAnonymous As System.Windows.Forms.Panel
    Friend WithEvents txtRuntimeName As System.Windows.Forms.TextBox
    Friend WithEvents txtTermDescription As System.Windows.Forms.TextBox
    Friend WithEvents PanelLower As System.Windows.Forms.Panel
    Friend WithEvents butSetRuntimeName As System.Windows.Forms.Button
    Friend WithEvents HelpProviderCommonConstraint As System.Windows.Forms.HelpProvider
    Friend WithEvents lblRunTimeName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents labelAny As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PanelGenericConstraint = New System.Windows.Forms.Panel
        Me.PanelDataConstraint = New System.Windows.Forms.Panel
        Me.labelAny = New System.Windows.Forms.Label
        Me.PanelNonAnonymous = New System.Windows.Forms.Panel
        Me.butSetRuntimeName = New System.Windows.Forms.Button
        Me.txtRuntimeName = New System.Windows.Forms.TextBox
        Me.lblRunTimeName = New System.Windows.Forms.Label
        Me.txtTermDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.PanelLower = New System.Windows.Forms.Panel
        Me.HelpProviderCommonConstraint = New System.Windows.Forms.HelpProvider
        Me.tabConstraint = New System.Windows.Forms.TabControl
        Me.tpConstraint = New System.Windows.Forms.TabPage
        Me.tpConstraintDetails = New System.Windows.Forms.TabPage
        Me.gbComments = New System.Windows.Forms.GroupBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.gbTerminology = New System.Windows.Forms.GroupBox
        Me.dgNodeBindings = New System.Windows.Forms.DataGridView
        Me.terminology = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.code = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Path = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.termLookUp = New OTSControls.Term
        Me.gbNullFlavours = New System.Windows.Forms.GroupBox
        Me.chkListNull = New System.Windows.Forms.CheckedListBox
        Me.gbValueSets = New System.Windows.Forms.GroupBox
        Me.dgValueSets = New System.Windows.Forms.DataGridView
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelDataConstraint.SuspendLayout()
        Me.PanelNonAnonymous.SuspendLayout()
        Me.PanelLower.SuspendLayout()
        Me.tabConstraint.SuspendLayout()
        Me.tpConstraint.SuspendLayout()
        Me.tpConstraintDetails.SuspendLayout()
        Me.gbComments.SuspendLayout()
        Me.gbTerminology.SuspendLayout()
        CType(Me.dgNodeBindings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbNullFlavours.SuspendLayout()
        Me.gbValueSets.SuspendLayout()
        CType(Me.dgValueSets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelGenericConstraint
        '
        Me.PanelGenericConstraint.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelGenericConstraint.Location = New System.Drawing.Point(3, 3)
        Me.PanelGenericConstraint.Name = "PanelGenericConstraint"
        Me.PanelGenericConstraint.Size = New System.Drawing.Size(420, 51)
        Me.PanelGenericConstraint.TabIndex = 0
        '
        'PanelDataConstraint
        '
        Me.PanelDataConstraint.Controls.Add(Me.labelAny)
        Me.PanelDataConstraint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDataConstraint.Location = New System.Drawing.Point(0, 96)
        Me.PanelDataConstraint.Name = "PanelDataConstraint"
        Me.PanelDataConstraint.Size = New System.Drawing.Size(420, 260)
        Me.PanelDataConstraint.TabIndex = 32
        '
        'labelAny
        '
        Me.labelAny.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelAny.Location = New System.Drawing.Point(16, 8)
        Me.labelAny.Name = "labelAny"
        Me.labelAny.Size = New System.Drawing.Size(136, 24)
        Me.labelAny.TabIndex = 0
        Me.labelAny.Text = "Any"
        '
        'PanelNonAnonymous
        '
        Me.PanelNonAnonymous.Controls.Add(Me.butSetRuntimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.txtRuntimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.lblRunTimeName)
        Me.PanelNonAnonymous.Controls.Add(Me.txtTermDescription)
        Me.PanelNonAnonymous.Controls.Add(Me.lblDescription)
        Me.PanelNonAnonymous.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelNonAnonymous.Location = New System.Drawing.Point(0, 0)
        Me.PanelNonAnonymous.Name = "PanelNonAnonymous"
        Me.PanelNonAnonymous.Size = New System.Drawing.Size(420, 96)
        Me.PanelNonAnonymous.TabIndex = 31
        '
        'butSetRuntimeName
        '
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me.butSetRuntimeName, "HowTo/Edit data/Set_runtime_name.html")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me.butSetRuntimeName, System.Windows.Forms.HelpNavigator.Topic)
        Me.butSetRuntimeName.Location = New System.Drawing.Point(334, 63)
        Me.butSetRuntimeName.Name = "butSetRuntimeName"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me.butSetRuntimeName, True)
        Me.butSetRuntimeName.Size = New System.Drawing.Size(26, 20)
        Me.butSetRuntimeName.TabIndex = 8
        Me.butSetRuntimeName.Text = "..."
        '
        'txtRuntimeName
        '
        Me.txtRuntimeName.Location = New System.Drawing.Point(136, 62)
        Me.txtRuntimeName.Name = "txtRuntimeName"
        Me.txtRuntimeName.ReadOnly = True
        Me.txtRuntimeName.Size = New System.Drawing.Size(192, 20)
        Me.txtRuntimeName.TabIndex = 7
        '
        'lblRunTimeName
        '
        Me.lblRunTimeName.Location = New System.Drawing.Point(8, 62)
        Me.lblRunTimeName.Name = "lblRunTimeName"
        Me.lblRunTimeName.Size = New System.Drawing.Size(120, 32)
        Me.lblRunTimeName.TabIndex = 6
        Me.lblRunTimeName.Text = "Runtime name constraint:"
        Me.lblRunTimeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTermDescription
        '
        Me.txtTermDescription.Location = New System.Drawing.Point(136, 3)
        Me.txtTermDescription.Multiline = True
        Me.txtTermDescription.Name = "txtTermDescription"
        Me.txtTermDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermDescription.Size = New System.Drawing.Size(224, 53)
        Me.txtTermDescription.TabIndex = 5
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(19, 8)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(112, 16)
        Me.lblDescription.TabIndex = 26
        Me.lblDescription.Text = "Description:"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelLower
        '
        Me.PanelLower.Controls.Add(Me.PanelDataConstraint)
        Me.PanelLower.Controls.Add(Me.PanelNonAnonymous)
        Me.PanelLower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLower.Location = New System.Drawing.Point(3, 54)
        Me.PanelLower.Name = "PanelLower"
        Me.PanelLower.Size = New System.Drawing.Size(420, 356)
        Me.PanelLower.TabIndex = 33
        '
        'tabConstraint
        '
        Me.tabConstraint.Controls.Add(Me.tpConstraint)
        Me.tabConstraint.Controls.Add(Me.tpConstraintDetails)
        Me.tabConstraint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabConstraint.Location = New System.Drawing.Point(0, 0)
        Me.tabConstraint.Name = "tabConstraint"
        Me.tabConstraint.SelectedIndex = 0
        Me.tabConstraint.Size = New System.Drawing.Size(434, 439)
        Me.tabConstraint.TabIndex = 34
        '
        'tpConstraint
        '
        Me.tpConstraint.BackColor = System.Drawing.Color.LemonChiffon
        Me.tpConstraint.Controls.Add(Me.PanelLower)
        Me.tpConstraint.Controls.Add(Me.PanelGenericConstraint)
        Me.tpConstraint.Location = New System.Drawing.Point(4, 22)
        Me.tpConstraint.Margin = New System.Windows.Forms.Padding(0)
        Me.tpConstraint.Name = "tpConstraint"
        Me.tpConstraint.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConstraint.Size = New System.Drawing.Size(426, 413)
        Me.tpConstraint.TabIndex = 0
        Me.tpConstraint.Text = "Constraint"
        Me.tpConstraint.UseVisualStyleBackColor = True
        '
        'tpConstraintDetails
        '
        Me.tpConstraintDetails.BackColor = System.Drawing.Color.LemonChiffon
        Me.tpConstraintDetails.Controls.Add(Me.gbComments)
        Me.tpConstraintDetails.Controls.Add(Me.gbTerminology)
        Me.tpConstraintDetails.Controls.Add(Me.termLookUp)
        Me.tpConstraintDetails.Controls.Add(Me.gbNullFlavours)
        Me.tpConstraintDetails.Controls.Add(Me.gbValueSets)
        Me.tpConstraintDetails.Controls.Add(Me.Splitter1)
        Me.tpConstraintDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpConstraintDetails.Name = "tpConstraintDetails"
        Me.tpConstraintDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpConstraintDetails.Size = New System.Drawing.Size(426, 413)
        Me.tpConstraintDetails.TabIndex = 1
        Me.tpConstraintDetails.Text = "Details"
        Me.tpConstraintDetails.UseVisualStyleBackColor = True
        '
        'gbComments
        '
        Me.gbComments.Controls.Add(Me.txtComments)
        Me.gbComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbComments.Location = New System.Drawing.Point(3, 6)
        Me.gbComments.Name = "gbComments"
        Me.gbComments.Size = New System.Drawing.Size(420, 74)
        Me.gbComments.TabIndex = 4
        Me.gbComments.TabStop = False
        Me.gbComments.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtComments.Location = New System.Drawing.Point(3, 16)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(414, 55)
        Me.txtComments.TabIndex = 0
        '
        'gbTerminology
        '
        Me.gbTerminology.Controls.Add(Me.dgNodeBindings)
        Me.gbTerminology.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbTerminology.Location = New System.Drawing.Point(3, 80)
        Me.gbTerminology.Name = "gbTerminology"
        Me.gbTerminology.Size = New System.Drawing.Size(420, 112)
        Me.gbTerminology.TabIndex = 5
        Me.gbTerminology.TabStop = False
        Me.gbTerminology.Text = "Node meaning in terminologies"
        '
        'dgNodeBindings
        '
        Me.dgNodeBindings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgNodeBindings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.terminology, Me.code, Me.Path})
        Me.dgNodeBindings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgNodeBindings.Location = New System.Drawing.Point(3, 16)
        Me.dgNodeBindings.Name = "dgNodeBindings"
        Me.dgNodeBindings.RowTemplate.Height = 24
        Me.dgNodeBindings.Size = New System.Drawing.Size(414, 93)
        Me.dgNodeBindings.TabIndex = 2
        '
        'terminology
        '
        Me.terminology.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.terminology.FillWeight = 70.0!
        Me.terminology.HeaderText = "Terminology"
        Me.terminology.MinimumWidth = 150
        Me.terminology.Name = "terminology"
        Me.terminology.Width = 150
        '
        'code
        '
        Me.code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.code.FillWeight = 30.0!
        Me.code.HeaderText = "Code"
        Me.code.MinimumWidth = 20
        Me.code.Name = "code"
        Me.code.Width = 57
        '
        'Path
        '
        Me.Path.HeaderText = "Path"
        Me.Path.Name = "Path"
        Me.Path.Visible = False
        '
        'termLookUp
        '
        Me.termLookUp.AccessibleDescription = ""
        Me.termLookUp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.termLookUp.Location = New System.Drawing.Point(3, 192)
        Me.termLookUp.Margin = New System.Windows.Forms.Padding(0)
        Me.termLookUp.MinimumSize = New System.Drawing.Size(60, 55)
        Me.termLookUp.Name = "termLookUp"
        Me.termLookUp.Size = New System.Drawing.Size(420, 55)
        Me.termLookUp.TabIndex = 7
        Me.termLookUp.Tag = ""
        Me.termLookUp.TermCaption = "SNOMED"
        Me.termLookUp.TermId = Nothing
        Me.termLookUp.TerminologyName = OTSControls.OTSServer.TerminologyName.Snomed
        Me.termLookUp.TermLanguage = "en-GB"
        Me.termLookUp.TermName = Nothing
        Me.termLookUp.TermQueryName = "AllSnomed"
        Me.termLookUp.Visible = False
        '
        'gbNullFlavours
        '
        Me.gbNullFlavours.Controls.Add(Me.chkListNull)
        Me.gbNullFlavours.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbNullFlavours.Location = New System.Drawing.Point(3, 247)
        Me.gbNullFlavours.Name = "gbNullFlavours"
        Me.gbNullFlavours.Size = New System.Drawing.Size(420, 110)
        Me.gbNullFlavours.TabIndex = 9
        Me.gbNullFlavours.TabStop = False
        Me.gbNullFlavours.Text = "Reasons why null"
        '
        'chkListNull
        '
        Me.chkListNull.CheckOnClick = True
        Me.chkListNull.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkListNull.FormattingEnabled = True
        Me.chkListNull.Location = New System.Drawing.Point(3, 16)
        Me.chkListNull.Name = "chkListNull"
        Me.chkListNull.Size = New System.Drawing.Size(414, 79)
        Me.chkListNull.TabIndex = 0
        '
        'gbValueSets
        '
        Me.gbValueSets.Controls.Add(Me.dgValueSets)
        Me.gbValueSets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbValueSets.Location = New System.Drawing.Point(3, 357)
        Me.gbValueSets.Name = "gbValueSets"
        Me.gbValueSets.Size = New System.Drawing.Size(420, 53)
        Me.gbValueSets.TabIndex = 8
        Me.gbValueSets.TabStop = False
        Me.gbValueSets.Text = "Value sets in external terminologies"
        Me.gbValueSets.Visible = False
        '
        'dgValueSets
        '
        Me.dgValueSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgValueSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgValueSets.Location = New System.Drawing.Point(3, 16)
        Me.dgValueSets.Name = "dgValueSets"
        Me.dgValueSets.RowTemplate.Height = 24
        Me.dgValueSets.Size = New System.Drawing.Size(414, 34)
        Me.dgValueSets.TabIndex = 3
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(3, 3)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(420, 3)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'ArchetypeNodeConstraintControl
        '
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Controls.Add(Me.tabConstraint)
        Me.HelpProviderCommonConstraint.SetHelpKeyword(Me, "HowTo/Edit data/set_common_constraints.htm")
        Me.HelpProviderCommonConstraint.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "ArchetypeNodeConstraintControl"
        Me.HelpProviderCommonConstraint.SetShowHelp(Me, True)
        Me.Size = New System.Drawing.Size(434, 439)
        Me.PanelDataConstraint.ResumeLayout(False)
        Me.PanelNonAnonymous.ResumeLayout(False)
        Me.PanelNonAnonymous.PerformLayout()
        Me.PanelLower.ResumeLayout(False)
        Me.tabConstraint.ResumeLayout(False)
        Me.tpConstraint.ResumeLayout(False)
        Me.tpConstraintDetails.ResumeLayout(False)
        Me.gbComments.ResumeLayout(False)
        Me.gbComments.PerformLayout()
        Me.gbTerminology.ResumeLayout(False)
        CType(Me.dgNodeBindings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbNullFlavours.ResumeLayout(False)
        Me.gbValueSets.ResumeLayout(False)
        CType(Me.dgValueSets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mArchetypeNode As ArchetypeNode

    Private mIsLoading As Boolean = True
    Protected ReadOnly Property IsLoading() As Boolean
        Get
            Return mIsLoading
        End Get
    End Property

    Public WriteOnly Property LocalFileManager() As FileManagerLocal
        Set(ByVal Value As FileManagerLocal)
            mFileManager = Value
            mOccurrences.LocalFileManager = Value
        End Set
    End Property

    Public Sub TranslateGUI()
        Me.lblDescription.Text = Filemanager.GetOpenEhrTerm(113, Me.lblDescription.Text)
        Me.lblRunTimeName.Text = Filemanager.GetOpenEhrTerm(114, Me.lblRunTimeName.Text)
    End Sub

    Public Sub ShowConstraint(ByVal aStructureType As StructureType, _
            ByVal IsState As Boolean, ByVal an_archetype_node As ArchetypeNode, ByVal a_file_manager As FileManagerLocal)

        'If a_file_manager.OntologyManager.NumberOfSpecialisations() <> OceanArchetypeEditor.Instance.CountInString(an_archetype_node.RM_Class.NodeId, ".") Then
        '    Me.Enabled = False
        'End If
        mFileManager = a_file_manager
        mIsLoading = True
        Me.SuspendLayout()

        Try
            ' hide the label if there is no constraint (for ANY or Cluster) - see below
            Me.labelAny.Visible = False
            Me.termLookUp.TermName = ""

            If Not mConstraintControl Is Nothing Then
                Me.PanelDataConstraint.Controls.Remove(mConstraintControl)
                mConstraintControl = Nothing
            End If

            'Hide Occurrences and show null flavours if an Element archetype
            If mFileManager.Archetype.RmEntity = StructureType.Element Then
                Me.PanelGenericConstraint.Visible = False
                Me.gbNullFlavours.Visible = True
            Else
                Me.PanelGenericConstraint.Visible = True
                Me.gbNullFlavours.Visible = False
            End If

            'JAR: 07MAY2007, EDT-34 Slot does not support details fields                        
            If an_archetype_node.RM_Class.Type = StructureType.Slot Then
                If tabConstraint.TabPages.Contains(tpConstraintDetails) Then
                    tabConstraint.TabPages.Remove(tpConstraintDetails)      'Hide details tab if a Slot
                End If
            ElseIf Not tabConstraint.TabPages.Contains(tpConstraintDetails) Then
                tabConstraint.TabPages.Add(tpConstraintDetails)         'Show details tab if not a slot
            End If

            Select Case an_archetype_node.RM_Class.Type
                Case StructureType.Element, StructureType.Reference

                    Dim archetypeElem As ArchetypeElement = CType(an_archetype_node, ArchetypeElement)

                    SetUpNullFlavours(archetypeElem)

                    Select Case archetypeElem.Constraint.Type
                        Case ConstraintType.Any
                            Me.labelAny.Text = AE_Constants.Instance.Any
                            Me.labelAny.Visible = True

                        Case Else
                            If archetypeElem.Constraint.Type = ConstraintType.Text AndAlso CType(archetypeElem.Constraint, Constraint_Text).TypeOfTextConstraint = TextConstrainType.Terminology Then
                                Me.gbValueSets.Visible = True
                            Else
                                Me.gbValueSets.Visible = False
                            End If

                            mConstraintControl = ConstraintControl.CreateConstraintControl( _
                                                           archetypeElem.Constraint.Type, mFileManager)


                            Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                            ' Ensures the ZOrder leads to no overlap
                            mConstraintControl.Dock = DockStyle.Fill

                            mConstraintControl.ShowConstraint(IsState, archetypeElem)
                    End Select

                Case StructureType.Slot

                    mConstraintControl = ConstraintControl.CreateConstraintControl( _
                               ConstraintType.Slot, mFileManager)

                    Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                    ' Ensures the ZOrder leads to no overlap
                    mConstraintControl.Dock = DockStyle.Fill

                    ' HKF: 1620
                    mConstraintControl.ShowConstraint(IsState, CType(CType(an_archetype_node, ArchetypeNodeAnonymous).RM_Class, RmSlot).SlotConstraint)

                Case StructureType.Cluster
                    ' Me.labelAnyCluster.Text = AE_Constants.Instance.Cluster
                    Me.labelAny.Visible = False
                    mConstraintControl = New ClusterControl(a_file_manager)
                    CType(mConstraintControl, ClusterControl).Item = CType(an_archetype_node, ArchetypeComposite)
                    Me.PanelDataConstraint.Controls.Add(mConstraintControl)

                    CType(mConstraintControl, ClusterControl).Header = 50
                    mConstraintControl.Dock = DockStyle.Fill

            End Select

            mArchetypeNode = an_archetype_node

            If aStructureType = StructureType.Single Then
                mOccurrences.SetSingle = True
            Else
                mOccurrences.SetSingle = False
            End If

            If IsState Then
                Me.tpConstraint.BackColor = System.Drawing.Color.LightSteelBlue
                Me.tpConstraintDetails.BackColor = System.Drawing.Color.LightSteelBlue
            Else
                Me.tpConstraint.BackColor = System.Drawing.Color.LemonChiffon
                Me.tpConstraintDetails.BackColor = System.Drawing.Color.LemonChiffon
            End If

            SetControlValues(IsState)

            'Enable the node code if not anonymous and there is a terminology added
            If Not an_archetype_node.IsAnonymous Then
                Me.dgNodeBindings.Visible = True
                Dim nodeID As String = CType(an_archetype_node, ArchetypeNodeAbstract).NodeId
                mDataView.Table.Columns(1).DefaultValue = nodeID
                mDataView.RowFilter = "Path = '" & CType(an_archetype_node, ArchetypeNodeAbstract).NodeId & "'"
                'Hide the termLookUp if there are no rows
                If mDataView.Count = 0 Then
                    termLookUp.Visible = False
                End If


            Else
                Me.dgNodeBindings.Visible = False
            End If

            If OceanArchetypeEditor.IsDefaultLanguageRightToLeft Then
                OceanArchetypeEditor.Reflect(mConstraintControl)
            End If



        Catch ex As Exception
            Debug.Assert(False, ex.ToString)
        End Try

        Me.ResumeLayout(False)
        mIsLoading = False
    End Sub

    Private Sub SetUpNullFlavours(ByVal archetypeElmnt As ArchetypeElement)

        Dim cp As CodePhrase = archetypeElmnt.RM_Class.ConstrainedNullFlavours
        Dim t As Term
        
        If chkListNull.Items.Count = 0 Then
            Dim dr As DataRow() = TerminologyServer.Instance.CodesForGrouperID(15)
            For Each r As DataRow In dr
                t = New Term(CStr(r(1)))
                t.Text = CStr(r(2))
                chkListNull.Items.Add(t, SetNullFlavorChecked(cp, t.Code))
            Next
        Else
            For i As Integer = 0 To chkListNull.Items.Count - 1
                t = CType(chkListNull.Items(i), Term)
                chkListNull.SetItemChecked(i, SetNullFlavorChecked(cp, t.Code))
            Next
        End If

        gbNullFlavours.Visible = True

    End Sub

    Private Function SetNullFlavorChecked(ByVal nullFlavors As CodePhrase, ByVal cde As String) As Boolean
        If nullFlavors.Codes.Count > 0 Then
            If nullFlavors.Codes.Contains(cde) Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function


    Protected Overridable Sub SetControlValues(ByVal IsState As Boolean) '(ByVal aArchetypeNode As ArchetypeNode)

        ' ToDo: set constraint values on control

        ' set the cardinality

        mOccurrences.Cardinality = mArchetypeNode.Occurrences

        If mArchetypeNode.IsAnonymous Then
            Me.PanelNonAnonymous.Visible = False
            Me.gbTerminology.Visible = False


        Else
            Me.PanelNonAnonymous.Visible = True
            Me.gbTerminology.Visible = True

            ' set the description of the term
            Me.txtTermDescription.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).Description
            Me.txtComments.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).Comment

            ' set the runtime name text
            Me.txtRuntimeName.Text = CType(mArchetypeNode, ArchetypeNodeAbstract).RuntimeNameText

            'Disable all but occurrences for References
            If mArchetypeNode.RM_Class.Type = StructureType.Reference Then
                Me.PanelDataConstraint.Enabled = False
                Me.PanelNonAnonymous.Enabled = False
            Else
                Me.PanelDataConstraint.Enabled = True
                Me.PanelNonAnonymous.Enabled = True
            End If

        End If

    End Sub


    Private Sub txtTermDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTermDescription.TextChanged

        If Not mIsLoading Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Description = Me.txtTermDescription.Text

            mFileManager.FileEdited = True

        End If

    End Sub


    Private Sub butSetRuntimeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSetRuntimeName.Click
        Dim frm As New ConstraintForm
        Dim has_constraint As Boolean
        Dim t As Constraint_Text = Nothing

        has_constraint = mArchetypeNode.RM_Class.HasNameConstraint
        If has_constraint Then
            t = CType(mArchetypeNode.RM_Class.NameConstraint.Copy, Constraint_Text)
        End If

        frm.ShowConstraint(False, mArchetypeNode.RM_Class.NameConstraint, mFileManager)
        Select Case frm.ShowDialog(Me)
            Case Windows.Forms.DialogResult.OK
                'no action
                mFileManager.FileEdited = True
            Case Windows.Forms.DialogResult.Cancel
                ' put it back to null if it was before
                If Not has_constraint Then
                    mArchetypeNode.RM_Class.HasNameConstraint = False
                Else
                    mArchetypeNode.RM_Class.NameConstraint = t
                End If
            Case Windows.Forms.DialogResult.Ignore
                mArchetypeNode.RM_Class.HasNameConstraint = False
                mFileManager.FileEdited = True
        End Select

        If mArchetypeNode.RM_Class.HasNameConstraint Then
            Me.txtRuntimeName.Text = mArchetypeNode.RM_Class.NameConstraint.ToString
        Else
            Me.txtRuntimeName.Text = ""
        End If
    End Sub

    Private Sub txtTermDescription_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTermDescription.KeyPress
        ' work around as acceptsreturn = false does not deal with stop Enter unless there is a AcceptButton
        If e.KeyChar = Chr(13) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtComments_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComments.TextChanged
        If Not mIsLoading Then
            CType(mArchetypeNode, ArchetypeNodeAbstract).Comment = Me.txtComments.Text
            mFileManager.FileEdited = True
        End If
    End Sub

    Private Sub ArchetypeNodeConstraintControl_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        If mFileManager.OntologyManager.TerminologiesTable.Rows.Count = 0 Then
            Me.dgNodeBindings.Enabled = False
        Else
            Me.dgNodeBindings.Enabled = True
        End If
    End Sub

    Private Sub ArchetypeNodeConstraintControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Me.dgNodeBindings.Columns(0), DataGridViewComboBoxColumn).DataSource = mFileManager.OntologyManager.TerminologiesTable
        CType(Me.dgNodeBindings.Columns(0), DataGridViewComboBoxColumn).ValueMember = "Terminology"
        Me.dgNodeBindings.Columns(0).DataPropertyName = "Terminology"
        Me.dgNodeBindings.Columns(1).DataPropertyName = "Code"
        Me.dgNodeBindings.Columns(2).DataPropertyName = "Path"
        Me.dgNodeBindings.DataSource = mDataView
    End Sub

    Private Sub SetTermLookUpVisibility(ByVal termID As String)

        If OceanArchetypeEditor.Instance.Options.AllowTerminologyLookUp AndAlso _
            Not String.IsNullOrEmpty(termID) AndAlso _
            OceanArchetypeEditor.Instance.ServiceTerminology(termID) Then
            termLookUp.TermCaption = termID
            'ToDo: work this from the terminology server
            Select Case termID
                Case "SNOMED-CT"
                    termLookUp.TerminologyName = OTSControls.OTSServer.TerminologyName.Snomed
                    termLookUp.TermQueryName = "AllSnomed"
                    termLookUp.Visible = True

                Case "LNC205"
                    termLookUp.TerminologyName = OTSControls.OTSServer.TerminologyName.LOINC
                    termLookUp.TermQueryName = "LOINC"
                    termLookUp.Visible = True

                Case Else
                    Debug.Assert(False, String.Format("{0} terminology available but fails case statement", termID))
                    termLookUp.Visible = False
            End Select

        Else
            termLookUp.Visible = False
        End If

    End Sub


    Private Sub termLookUp_TermChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles termLookUp.TermChanged
        If Not dgNodeBindings.CurrentRow Is Nothing AndAlso Not termLookUp.TermId Is Nothing Then
            dgNodeBindings.CurrentRow.Cells(2).Value = termLookUp.TermId
        End If
    End Sub

    'Private Sub dgNodeBindings_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgNodeBindings.CellClick
    '    SetTermLookUpVisibility(CType(dgNodeBindings.CurrentRow.Cells(0), DataGridViewComboBoxCell).EditedFormattedValue.ToString)
    '    Debug.WriteLine(String.Format("Cell click - Row index:{0}, Visible{1}", dgNodeBindings.CurrentRow.Index, Me.termLookUp.Visible))
    'End Sub

    'Private Sub dgNodeBindings_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgNodeBindings.RowEnter
    '    SetTermLookUpVisibility(CStr(CType(dgNodeBindings.Rows(e.RowIndex).Cells(0), DataGridViewComboBoxCell).Value))
    '    Debug.WriteLine(String.Format("Row enter - Row index:{0}, Visible{1}", e.RowIndex, Me.termLookUp.Visible))
    'End Sub

    Private Sub dgNodeBindings_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles dgNodeBindings.RowPostPaint
        If (Not dgNodeBindings.CurrentRow Is Nothing) AndAlso (Not TypeOf dgNodeBindings.Rows(e.RowIndex).Cells(0).Value Is System.DBNull) AndAlso (dgNodeBindings.CurrentRow.Index = e.RowIndex) Then
            SetTermLookUpVisibility(CStr(CType(dgNodeBindings.Rows(e.RowIndex).Cells(0), DataGridViewComboBoxCell).Value))
            'If termLookUp.Visible Then
            '    'Need to display term if there is one
            '    Dim s As String = CStr(CType(dgNodeBindings.Rows(e.RowIndex).Cells(1), DataGridViewTextBoxCell).Value)
            '    If s <> String.Empty AndAlso mFileManager.HasTermBindings("en-GB", s) Then
            '        s = mFileManager.BindingText("en-GB", "Snomed", s)
            '        If s <> String.Empty Then
            '            Me.termLookUp.TermName = s
            '        End If
            '    End If
            'End If
            'Debug.WriteLine(String.Format("Row paint - Row index:{0}, Visible{1}", e.RowIndex, Me.termLookUp.Visible))
        End If
    End Sub

    Private Sub chkListNull_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListNull.ItemCheck
        If Not IsLoading Then
            Dim cp As CodePhrase = CType(mArchetypeNode, ArchetypeElement).RM_Class.ConstrainedNullFlavours
            Dim t As Term = CType(chkListNull.Items(e.Index), Term)
            If e.NewValue = CheckState.Checked Then
                If chkListNull.CheckedItems.Count = chkListNull.Items.Count Then
                    cp.Codes.Clear()
                Else
                    cp.Codes.Add(t.Code)
                End If
            Else
                If cp.Codes.Count > 0 Then
                    cp.Codes.Remove(t.Code)
                Else
                    'Add the checked codes
                    For Each tt As Term In chkListNull.Items
                        If Not tt Is t Then
                            cp.Codes.Add(tt.Code)
                        End If
                    Next
                End If
            End If
        End If
    End Sub
End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is ArchetypeNodeConstraintControl.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'
