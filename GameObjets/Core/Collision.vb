Public Class Collision
    Public Shared Function CheckCollision(bullet As Bullet, enemies As List(Of Enemy)) As Enemy
        For Each enemy In enemies
            If bullet.PictureBox.Bounds.IntersectsWith(enemy.PictureBox.Bounds) Then
                Return enemy
            End If
        Next
        Return Nothing
    End Function
End Class
