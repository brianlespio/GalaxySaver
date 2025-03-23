Imports System.Windows.Forms

Public Class Enemy
    Public Property X As Integer
    Public Property Y As Integer
    Public Property PictureBox As PictureBox

    Public Sub New(startX As Integer, startY As Integer)
        X = startX
        Y = startY
    End Sub
End Class
