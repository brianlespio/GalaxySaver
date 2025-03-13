Public Class Player
    Public X As Integer
    Public Y As Integer

    Public Sub New(startX As Integer, startY As Integer)
        X = startX
        Y = startY
    End Sub

    Public Function Shoot() As Bullet
        Return New Bullet(X + 20, Y - 10) ' Se ajusta la posición para que salga desde el centro
    End Function
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
