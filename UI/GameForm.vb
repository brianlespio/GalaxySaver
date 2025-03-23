Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.UI
    Public Class GameForm
        Inherits Form
        
        ' Game controller
        Private _gameController As GalaxySaver.Core.GameController
        
        ' Game timers
        Private WithEvents _gameTimer As New Timer()
        Private WithEvents _enemyTimer As New Timer()
        Private WithEvents _bulletTimer As New Timer()
        
        ' UI elements
        Private _lblScore As New Label()
        Private _lblLevel As New Label()
        Private _lblLives As New Label()
        Private _lblGameOver As New Label()
        Private _lblPaused As New Label()
        
        ' Constructor
        Public Sub New()
            InitializeComponent()
        End Sub
        
        ' Initialize form components
        Private Sub InitializeComponent()
            ' Form settings
            Me.Text = "GalaxySaver"
            Me.BackColor = Color.Black
            Me.WindowState = FormWindowState.Maximized
            Me.FormBorderStyle = FormBorderStyle.None
            Me.KeyPreview = True
            Me.DoubleBuffered = True
            
            ' Score label
            _lblScore.AutoSize = True
            _lblScore.Font = New Font("Arial", 14, FontStyle.Bold)
            _lblScore.ForeColor = Color.White
            _lblScore.Location = New Point(10, 10)
            _lblScore.Text = "Score: 0"
            Me.Controls.Add(_lblScore)
            
            ' Level label
            _lblLevel.AutoSize = True
            _lblLevel.Font = New Font("Arial", 14, FontStyle.Bold)
            _lblLevel.ForeColor = Color.White
            _lblLevel.Location = New Point(10, 40)
            _lblLevel.Text = "Level: 1"
            Me.Controls.Add(_lblLevel)
            
            ' Lives label
            _lblLives.AutoSize = True
            _lblLives.Font = New Font("Arial", 14, FontStyle.Bold)
            _lblLives.ForeColor = Color.White
            _lblLives.Location = New Point(10, 70)
            _lblLives.Text = "Lives: 3"
            Me.Controls.Add(_lblLives)
            
            ' Game over label
            _lblGameOver.AutoSize = True
            _lblGameOver.Font = New Font("Arial", 36, FontStyle.Bold)
            _lblGameOver.ForeColor = Color.Red
            _lblGameOver.TextAlign = ContentAlignment.MiddleCenter
            _lblGameOver.Text = "GAME OVER"
            _lblGameOver.Visible = False
            Me.Controls.Add(_lblGameOver)
            
            ' Paused label
            _lblPaused.AutoSize = True
            _lblPaused.Font = New Font("Arial", 36, FontStyle.Bold)
            _lblPaused.ForeColor = Color.Yellow
            _lblPaused.TextAlign = ContentAlignment.MiddleCenter
            _lblPaused.Text = "PAUSED"
            _lblPaused.Visible = False
            Me.Controls.Add(_lblPaused)
            
            ' Game timer
            _gameTimer.Interval = 16 ' ~60 FPS
            
            ' Enemy timer
            _enemyTimer.Interval = 50
            
            ' Bullet timer
            _bulletTimer.Interval = 16
        End Sub
        
        ' Form load event
        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            
            ' Center game over and paused labels
            _lblGameOver.Location = New Point((Me.Width - _lblGameOver.Width) \ 2, (Me.Height - _lblGameOver.Height) \ 2)
            _lblPaused.Location = New Point((Me.Width - _lblPaused.Width) \ 2, (Me.Height - _lblPaused.Height) \ 2)
            
            ' Initialize game controller
            _gameController = New GalaxySaver.Core.GameController(Me, Me.Width, Me.Height)
            
            ' Start game
            StartGame()
        End Sub
        
        ' Start game
        Public Sub StartGame()
            ' Clear controls except labels
            For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                If Not (TypeOf Me.Controls(i) Is Label) Then
                    Me.Controls.RemoveAt(i)
                End If
            Next
            
            ' Initialize game
            _gameController.InitializeGame()
            
            ' Reset labels
            _lblScore.Text = "Score: 0"
            _lblLevel.Text = "Level: 1"
            _lblLives.Text = "Lives: 3"
            _lblGameOver.Visible = False
            _lblPaused.Visible = False
            
            ' Start timers
            _gameTimer.Start()
            _enemyTimer.Start