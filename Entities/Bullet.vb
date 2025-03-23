Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public Class Bullet
        Inherits Entity
        
        ' Bullet properties
        Private _isPlayerBullet As Boolean
        Private _speed As Integer
        
        ' Properties
        Public ReadOnly Property IsPlayerBullet As Boolean
            Get
                Return _isPlayerBullet
            End Get
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer, isPlayerBullet As Boolean)
            MyBase.New(x, y, 5, 15) ' Bullet size is 5x15 pixels
            _isPlayerBullet = isPlayerBullet
            
            ' Set bullet speed based on type
            If _isPlayerBullet Then
                _speed = 10 ' Player bullets move upward faster
            Else
                _speed = 5 ' Enemy bullets move downward slower
            End If
        End Sub
        
        ' Initialize bullet
        Public Overrides Sub Initialize(parentForm As Form)
            ' Create bullet picture box
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = If(_isPlayerBullet, Color.Yellow, Color.Red)
            }
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Initialize with color
        Public Sub Initialize(parentForm As Form, color As Color)
            ' Create bullet picture box with specified color
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = color
            }
            
            ' Try to load bullet image
            Try
                Dim imagePath As String = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    If(_isPlayerBullet, "bullet.png", "enemy_bullet.png"))
                
                If System.IO.File.Exists(imagePath) Then
                    _pictureBox.Image = Image.FromFile(imagePath)
                    _pictureBox.BackColor = Color.Transparent
                    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            Catch ex As Exception
                ' If image loading fails, use default color
                _pictureBox.BackColor = color
            End Try
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Update bullet position
        Public Overrides Sub Update()
            ' Move bullet based on type
            If _isPlayerBullet Then
                _y -= _speed ' Player bullets move upward
            Else
                _y += _speed ' Enemy bullets move downward
            End If
            
            ' Update position
            UpdatePosition()
        End Sub
    End Class
End Namespace