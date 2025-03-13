Namespace GameObjets.Core
    Public Class Bullet
        Public Property X As Integer
        Public Property Y As Integer
        Public Property IsActive As Boolean

        Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
            X = startX
            Y = startY
            IsActive = True
        End Sub

        ' Aquí van los métodos relacionados con las balas, como moverse, colisiones, etc.
    End Class
End Namespace
