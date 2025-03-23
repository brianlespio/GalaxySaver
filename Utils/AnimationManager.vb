Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic

Namespace GalaxySaver.Utils
    Public Class AnimationManager
        ' Animation types
        Public Enum AnimationType
            Explosion
            PlayerExplosion
            PowerUp
            BossExplosion
        End Enum
        
        ' Animation class
        Public Class Animation
            Public Property X As Integer
            Public Property Y As Integer
            Public Property Width As Integer
            Public Property Height As Integer
            Public Property CurrentFrame As Integer
            Public Property TotalFrames As Integer
            Public Property FrameDelay As Integer
            Public Property CurrentDelay As Integer
            Public Property Type As AnimationType
            Public Property PictureBox As PictureBox
            Public Property IsActive As Boolean
            
            ' Constructor
            Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer, totalFrames As Integer, frameDelay As Integer, type As AnimationType)
                Me.X = x
                Me.Y = y
                Me.Width = width
                Me.Height = height
                Me.CurrentFrame = 0
                Me.TotalFrames = totalFrames
                Me.FrameDelay = frameDelay
                Me.CurrentDelay = 0
                Me.Type = type
                Me.IsActive = True
            End Sub
            
            ' Initialize animation
            Public Sub Initialize(parentForm As Form)
                ' Create picture box
                PictureBox = New PictureBox() With {
                    .Size = New Size(Width, Height),
                    .Location = New Point(X, Y),
                    .BackColor = Color.Transparent,
                    .SizeMode = PictureBoxSizeMode.StretchImage
                }
                
                ' Set initial frame
                SetFrame(0)
                
                ' Add to form
                parentForm.Controls.Add(PictureBox)
            End Sub
            
            ' Update animation
            Public Function Update() As Boolean
                ' Increment delay counter
                CurrentDelay += 1
                
                ' Check if it's time to advance to next frame
                If CurrentDelay >= FrameDelay Then
                    CurrentDelay = 0
                    CurrentFrame += 1
                    
                    ' Check if animation is complete
                    If CurrentFrame >= TotalFrames Then
                        IsActive = False
                        Return False
                    End If
                    
                    ' Set new frame
                    SetFrame(CurrentFrame)
                End If
                
                Return True
            End Function
            
            ' Set animation frame
            Private Sub SetFrame(frame As Integer)
                Try
                    ' Get frame image path
                    Dim imagePath As String = ""
                    
                    Select Case Type
                        Case AnimationType.Explosion
                            imagePath = System.IO.Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory, 
                                "Resources", 
                                $"explosion{frame + 1}.png")
                            
                        Case AnimationType.PlayerExplosion
                            imagePath