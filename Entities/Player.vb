Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public Class Player
        Inherits Entity
        
        ' Player properties
        Private _moveSpeed As Integer = 8
        Private _shootCooldown As Integer = 0
        Private _shootCooldownMax As Integer = 10
        Private _isInvulnerable As Boolean = False
        Private _invulnerabilityTimer As Integer = 0
        Private _invulnerabilityDuration As Integer = 60 ' Frames of invulnerability
        Private _powerUpTimer As Integer = 0
        Private _powerUpDuration As Integer = 300 ' 5 seconds at 60 FPS
        Private _currentPowerUp As PowerUpType = PowerUpType.None
        
        ' Properties
        Public ReadOnly Property MoveSpeed As Integer
            Get
                Return _moveSpeed
            End Get
        End Property
        
        Public Property IsInvulnerable As Boolean
            Get
                Return _isInvulnerable
            End Get
            Set(value As Boolean)
                _isInvulnerable = value
                If value Then
                    _invulnerabilityTimer = _invulnerabilityDuration
                End If
            End Set
        End Property
        
        Public ReadOnly Property CanShoot As Boolean
            Get
                Return _shootCooldown <= 0
            End Get
        End Property
        
        Public ReadOnly Property CurrentPowerUp As PowerUpType
            Get
                Return _currentPowerUp
            End Get
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer)
            MyBase.New(x, y, 40, 40) ' Player size is 40x40 pixels
        End Sub
        
        ' Initialize player
        Public Overrides Sub Initialize(parentForm As Form)
            ' Create player picture box with blue color
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = Color.Blue
            }
            
            ' Try to load player image
            Try
                Dim imagePath As String = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    "Player.png")
                
                If System.IO.File.Exists(imagePath) Then
                    _pictureBox.Image = Image.FromFile(imagePath)
                    _pictureBox.BackColor = Color.Transparent
                    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            Catch ex As Exception
                ' If image loading fails, use default color
                _pictureBox.BackColor = Color.Blue
            End Try
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Move player left
        Public Sub MoveLeft()
            _x -= _moveSpeed
            UpdatePosition()
        End Sub
        
        ' Move player right
        Public Sub MoveRight(gameWidth As Integer)
            _x += _moveSpeed
            
            ' Ensure player stays within game bounds
            If _x > gameWidth - _width Then
                _x = gameWidth - _width
            End If
            
            UpdatePosition()
        End Sub
        
        ' Update player state
        Public Overrides Sub Update()
            ' Update shoot cooldown
            If _shootCooldown > 0 Then
                _shootCooldown -= 1
            End If
            
            ' Update invulnerability
            If _isInvulnerable Then
                _invulnerabilityTimer -= 1
                
                ' Flash player when invulnerable
                If _pictureBox IsNot Nothing Then
                    _pictureBox.Visible = (_invulnerabilityTimer Mod 6 < 3)
                End If
                
                ' End invulnerability
                If _invulnerabilityTimer <= 0 Then
                    _isInvulnerable = False
                    If _pictureBox IsNot Nothing Then
                        _pictureBox.Visible = True
                    End If
                End If
            End If
            
            ' Update power-up timer
            If _currentPowerUp <> PowerUpType.None Then
                _powerUpTimer -= 1
                
                ' End power-up
                If _powerUpTimer <= 0 Then
                    _currentPowerUp = PowerUpType.None
                End If
            End If
        End Sub
        
        ' Reset shoot cooldown
        Public Sub ResetShootCooldown()
            _shootCooldown = _shootCooldownMax
        End Sub
        
        ' Apply power-up
        Public Sub ApplyPowerUp(powerUpType As PowerUpType)
            _currentPowerUp = powerUpType
            _powerUpTimer = _powerUpDuration
            
            ' Apply power-up effects
            Select Case powerUpType
                Case PowerUpType.DoubleShot
                    ' Double shot is handled in GameController
                Case PowerUpType.SpeedBoost
                    _moveSpeed = 12 ' Increased speed
                Case PowerUpType.Shield
                    IsInvulnerable = True
                    _invulnerabilityTimer = _powerUpDuration
                Case PowerUpType.SmartBomb
                    ' Smart bomb is handled in GameController
            End Select
        End Sub
        
        ' End power-up
        Public Sub EndPowerUp()
            ' Reset power-up effects
            Select Case _currentPowerUp
                Case PowerUpType.SpeedBoost
                    _moveSpeed = 8 ' Reset to normal speed
            End Select
            
            _currentPowerUp = PowerUpType.None
            _powerUpTimer = 0
        End Sub
    End Class
End Namespace