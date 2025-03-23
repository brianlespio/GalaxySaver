Imports System.Windows.Forms
Imports System.Drawing

Public Class Player
    Public Property X As Integer
    Public Property Y As Integer
    Public Property PictureBox As PictureBox

    Public Sub New(pb As PictureBox)
        PictureBox = pb
        X = pb.Location.X
        Y = pb.Location.Y
    End Sub

    Public Sub MoveLeft()
        If X > 0 Then
            X -= 10
            PictureBox.Location = New Point(X, Y)
        End If
    End Sub

    Public Sub MoveRight(formWidth As Integer)
        If X < formWidth - PictureBox.Width Then
            X += 10
            PictureBox.Location = New Point(X, Y)
        End If
    End Sub
End Class


' Bullet.vb
Public Class Bullet
    Public X As Integer
    Public Y As Integer

    Public Sub New(startX As Integer, startY As Integer)
        X = startX
        Y = startY
    End Sub
End Class
