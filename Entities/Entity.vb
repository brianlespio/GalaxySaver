Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public MustInherit Class Entity
        ' Position and size
        Protected _x As Integer
        Protected _y As Integer
        Protected _width As Integer
        Protected _height As Integer
        
        ' Visual representation
        Protected _pictureBox As PictureBox
        
        ' State
        Protected _isActive As Boolean = True
        
        ' Properties
        Public Property X As Integer
            Get
                Return _x
            End Get
            Set(value As Integer)
                _x = value
                UpdatePosition()
            End Set
        End Property
        
        Public Property Y As Integer
            Get
                Return _y
            End Get
            Set(value As Integer)
                _y = value
                UpdatePosition()
            End Set
        End Property
        
        Public ReadOnly Property Width As Integer
            Get
                Return _width
            End Get
        End Property
        
        Public ReadOnly Property Height As Integer
            Get
                Return _height
            End Get
        End Property
        
        Public Property PictureBox As PictureBox
            Get
                Return _pictureBox
            End Get
            Set(value As PictureBox)
                _pictureBox = value
            End Set
        End Property
        
        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get
            Set(value As Boolean)
                _isActive = value
            End Set
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer)
            _x = x
            _y = y
            _width = width
            _height = height
        End Sub
        
        ' Initialize the entity
        Public Overridable Sub Initialize(parentForm As Form)
            ' Create picture box
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = Color.White
            }
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Initialize with image
        Public Overridable Sub Initialize(parentForm As Form, imagePath As String)
            ' Create picture box with image
            _pictureBox = New PictureBox() With {
                .Size = New Size(_width, _height),
                .Location = New Point(_x, _y),
                .BackColor = Color.Transparent,
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            
            ' Load image if file exists
            Try
                If System.IO.File.Exists(imagePath) Then
                    _pictureBox.Image = Image.FromFile(imagePath)
                End If
            Catch ex As Exception
                ' If image loading fails, use default color
                _pictureBox.BackColor = Color.White
            End Try
            
            ' Add to form
            parentForm.Controls.Add(_pictureBox)
        End Sub
        
        ' Update position
        Public Overridable Sub UpdatePosition()
            If _pictureBox IsNot Nothing Then
                _pictureBox.Location = New Point(_x, _y)
            End If
        End Sub
        
        ' Update entity state
        Public Overridable Sub Update()
            ' Base update logic
        End Sub
        
        ' Dispose entity
        Public Overridable Sub Dispose()
            If _pictureBox IsNot Nothing Then
                _pictureBox.Dispose()
                _pictureBox = Nothing
            End If
        End Sub
        
        ' Get bounds for collision detection
        Public Function GetBounds() As Rectangle
            Return New Rectangle(_x, _y, _width, _height)
        End Function
    End Class
End Namespace