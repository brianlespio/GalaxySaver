Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public Class Enemy
        Inherits Entity
        
        ' Enemy properties
        Private _enemyType As Integer = 0 ' 0 = basic, 1 = medium, 2 = advanced
        Private _isDiving As Boolean = False
        Private _divePhase As Integer = 0 ' 0 = start, 1 = diving, 2 = returning
        Private _originalX As Integer
        Private _originalY As Integer
        Private _diveSpeed As Integer = 5
        Private _diveAngle As Double = 0
        Private _divingComplete As Boolean = False
        
        ' Properties
        Public Property EnemyType As Integer
            Get
                Return _enemyType
            End Get
            Set(value As Integer)
                _enemyType = value
            End Set
        End Property
        
        Public Property IsDiving As Boolean
            Get
                Return _isDiving
            End Get
            Set(value As Boolean)
                _isDiving = value
            End Set
        End Property
        
        Public ReadOnly Property DivingComplete As Boolean
            Get
                Return _divingComplete
            End Get
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer, enemyType As Integer)
            MyBase.New(x, y, 30, 30) ' Enemy size is 30x30 pixels
            _enemyType = enemyType
            _originalX = x
            _originalY = y
        End Sub
        
        ' Initialize enemy
        Public Overrides Sub Initialize(parentForm As Form)
            ' Create enemy picture box with color based on type
            Dim enemyColor As Color
            
            Select Case _enemyType
                Case 0 ' Basic enemy
                    enemyColor = Color.Red
                Case 1 ' Medium enemy
                    enemyColor = Color.Orange
                Case 2 ' Advanced enemy
                    enemyColor = Color.Magenta
                Case Else
                    enemyColor = Color.Red
            End Select
            
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = enemyColor
            }
            
            ' Try to load enemy image
            Try
                Dim imagePath As String = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    $"Enemy{_enemyType + 1}.png")
                
                If System.IO.File.Exists(imagePath) Then
                    _pictureBox.Image = Image.FromFile(imagePath)
                    _pictureBox.BackColor = Color.Transparent
                    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            Catch ex As Exception
                ' If image loading fails, use default color
                _pictureBox.BackColor = enemyColor
            End Try
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Start diving toward player
        Public Sub StartDiving(playerX As Integer, playerY As Integer)
            If _isDiving Then Return
            
            _isDiving = True
            _divePhase = 0
            _originalX = _x
            _originalY = _y
            _divingComplete = False
            
            ' Calculate angle to player
            _diveAngle = Math.Atan2(playerY - _y, playerX - _x)
        End Sub
        
        ' Stop diving and return to formation
        Public Sub StopDiving()
            _isDiving = False
            _divePhase = 0
            _divingComplete = False
        End Sub
        
        ' Update diving movement
        Public Sub UpdateDiving(playerX As Integer, playerY As Integer)
            If Not _isDiving Then Return
            
            Select Case _divePhase
                Case 0 ' Start dive
                    ' Initial dive movement
                    _x += CInt(Math.Cos(_diveAngle) * _diveSpeed)
                    _y += CInt(Math.Sin(_diveAngle) * _diveSpeed)
                    
                    ' Check if enemy has dived far enough
                    If _y > playerY - 100 Then
                        _divePhase = 1
                    End If
                    
                Case 1 ' Continue dive
                    ' Continue dive movement
                    _x += CInt(Math.Cos(_diveAngle) * _diveSpeed)
                    _y += CInt(Math.Sin(_diveAngle) * _diveSpeed)
                    
                    ' Check if enemy has gone past player
                    If _y > playerY + 100 Then
                        _divePhase = 2
                        
                        ' Calculate return angle to original position
                        _diveAngle = Math.Atan2(_originalY - _y, _originalX - _x)
                    End If
                    
                Case 2 ' Return to formation
                    ' Move back toward original position
                    _x += CInt(Math.Cos(_diveAngle) * _diveSpeed)
                    _y += CInt(Math.Sin(_diveAngle) * _diveSpeed)
                    
                    ' Check if enemy is close to original position
                    Dim distanceX As Integer = Math.Abs(_x - _originalX)
                    Dim distanceY As Integer = Math.Abs(_y - _originalY)
                    
                    If distanceX < 10 And distanceY < 10 Then
                        ' Return to exact original position
                        _x = _originalX
                        _y = _originalY
                        _divingComplete = True
                    End If
            End Select
            
            ' Update position
            UpdatePosition()
        End Sub
        
        ' Explode enemy
        Public Sub Explode()
            ' Play explosion animation
            Try
                ' Change image to explosion
                Dim explosionPath As String = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    "explosion1.png")
                
                If System.IO.File.Exists(explosionPath) And _pictureBox IsNot Nothing Then
                    _pictureBox.Image = Image.FromFile(explosionPath)
                End If
            Catch ex As Exception
                ' If explosion animation fails, just dispose
                Dispose()
            End Try
        End Sub
        
        ' Update enemy state
        Public Overrides Sub Update()
            ' Base update logic
            MyBase.Update()
            
            ' Enemy-specific update logic can be added here
        End Sub
    End Class
End Namespace