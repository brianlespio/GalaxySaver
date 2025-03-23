Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public Class Boss
        Inherits Entity
        
        ' Boss properties
        Private _bossType As Integer = 0
        Private _health As Integer = 10
        Private _maxHealth As Integer = 10
        Private _moveSpeed As Integer = 2
        Private _shootTimer As Integer = 0
        Private _shootFrequency As Integer = 30
        Private _moveDirection As Integer = 1 ' 1 = right, -1 = left
        Private _attackPhase As Integer = 0 ' 0 = normal, 1 = special attack
        Private _attackTimer As Integer = 0
        Private _attackDuration As Integer = 180 ' 3 seconds at 60 FPS
        
        ' Properties
        Public Property BossType As Integer
            Get
                Return _bossType
            End Get
            Set(value As Integer)
                _bossType = value
            End Set
        End Property
        
        Public Property Health As Integer
            Get
                Return _health
            End Get
            Set(value As Integer)
                _health = value
            End Set
        End Property
        
        Public ReadOnly Property MaxHealth As Integer
            Get
                Return _maxHealth
            End Get
        End Property
        
        Public ReadOnly Property IsActive As Boolean
            Get
                Return _health > 0
            End Get
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer, bossType As Integer, level As Integer)
            MyBase.New(x, y, 100, 80) ' Boss size is 100x80 pixels
            _bossType = bossType
            
            ' Adjust boss properties based on level
            _maxHealth = 10 + (level * 2)
            _health = _maxHealth
            _moveSpeed = 1 + (level \ 3)
            _shootFrequency = Math.Max(30 - (level * 2), 10)
        End Sub
        
        ' Initialize boss
        Public Overrides Sub Initialize(parentForm As Form)
            ' Create boss picture box
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = Color.Purple
            }
            
            ' Try to load boss image
            Try
                Dim imagePath As String = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    $"boss{_bossType + 1}.png")
                
                If System.IO.File.Exists(imagePath) Then
                    _pictureBox.Image = Image.FromFile(imagePath)
                    _pictureBox.BackColor = Color.Transparent
                    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            Catch ex As Exception
                ' If image loading fails, use default color
                _pictureBox.BackColor = Color.Purple
            End Try
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Update boss movement and behavior
        Public Overrides Sub Update()
            ' Move boss horizontally
            _x += _moveSpeed * _moveDirection
            
            ' Update position
            UpdatePosition()
            
            ' Update shoot timer
            _shootTimer += 1
            
            ' Update attack timer if in special attack phase
            If _attackPhase = 1 Then
                _attackTimer -= 1
                
                ' End special attack phase
                If _attackTimer <= 0 Then
                    _attackPhase = 0
                End If
            End If
        End Sub
        
        ' Check if boss should change direction
        Public Sub CheckBoundaries(gameWidth As Integer)
            ' Change direction if reaching screen edge
            If (_x <= 0 And _moveDirection = -1) Or (_x >= gameWidth - _width And _moveDirection = 1) Then
                _moveDirection *= -1
            End If
        End Sub
        
        ' Check if boss can shoot
        Public Function CanShoot() As Boolean
            If _shootTimer >= _shootFrequency Then
                _shootTimer = 0
                Return True
            End If
            
            Return False
        End Function
        
        ' Start special attack
        Public Sub StartSpecialAttack()
            _attackPhase = 1
            _attackTimer = _attackDuration
            _shootFrequency = _shootFrequency \ 2 ' Shoot more frequently during special attack
        End Sub
        
        ' End special attack
        Public Sub EndSpecialAttack()
            _attackPhase = 0
            _shootFrequency = _shootFrequency * 2 ' Reset shoot frequency
        End Sub
        
        ' Take damage
        Public Function TakeDamage(damage As Integer) As Boolean
            _health -= damage
            
            ' Check if boss is defeated
            If _health <= 0 Then
                _health = 0
                Return True
            End If
            
            Return False
        End Function
        
        ' Get shoot position
        Public Function GetShootPosition() As Point
            Return New Point(_x + (_width \ 2), _y + _height)
        End Function
        
        ' Get health percentage
        Public Function GetHealthPercentage() As Double
            Return CDbl(_health) / CDbl(_maxHealth)
        End Function
    End Class
End Namespace