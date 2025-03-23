Imports System.Drawing

Namespace GalaxySaver.Utils
    Public Class Collision
        ' Check collision between two entities
        Public Shared Function CheckCollision(entity1 As GalaxySaver.Entities.Entity, entity2 As GalaxySaver.Entities.Entity) As Boolean
            ' If either entity is not active, no collision
            If Not entity1.IsActive Or Not entity2.IsActive Then
                Return False
            End If
            
            ' Get entity bounds
            Dim bounds1 As Rectangle = entity1.GetBounds()
            Dim bounds2 As Rectangle = entity2.GetBounds()
            
            ' Check if bounds intersect
            Return bounds1.IntersectsWith(bounds2)
        End Function
        
        ' Check collision between bullet and enemy
        Public Shared Function CheckCollision(bullet As GalaxySaver.Entities.Bullet, enemy As GalaxySaver.Entities.Enemy) As Boolean
            ' If either entity is not active, no collision
            If Not bullet.IsActive Or Not enemy.IsActive Then
                Return False
            End If
            
            ' Only check player bullets against enemies
            If Not bullet.IsPlayerBullet Then
                Return False
            End If
            
            ' Get entity bounds
            Dim bulletBounds As Rectangle = bullet.GetBounds()
            Dim enemyBounds As Rectangle = enemy.GetBounds()
            
            ' Check if bounds intersect
            Return bulletBounds.IntersectsWith(enemyBounds)
        End Function
        
        ' Check collision between bullet and player
        Public Shared Function CheckCollision(bullet As GalaxySaver.Entities.Bullet, player As GalaxySaver.Entities.Player) As Boolean
            ' If either entity is not active, no collision
            If Not bullet.IsActive Or Not player.IsActive Then
                Return False
            End If
            
            ' Only check enemy bullets against player
            If bullet.IsPlayerBullet Then
                Return False
            End If
            
            ' If player is invulnerable, no collision
            If player.IsInvulnerable Then
                Return False
            End If
            
            ' Get entity bounds
            Dim bulletBounds As Rectangle = bullet.GetBounds()
            Dim playerBounds As Rectangle = player.GetBounds()
            
            ' Check if bounds intersect
            Return bulletBounds.IntersectsWith(playerBounds)
        End Function
        
        ' Check collision between enemy and player
        Public Shared Function CheckCollision(enemy As GalaxySaver.Entities.Enemy, player As GalaxySaver.Entities.Player) As Boolean
            ' If either entity is not active, no collision
            If Not enemy.IsActive Or Not player.IsActive Then
                Return False
            End If
            
            ' If player is invulnerable, no collision
            If player.IsInvulnerable Then
                Return False
            End If
            
            ' Get entity bounds
            Dim enemyBounds As Rectangle = enemy.GetBounds()
            Dim playerBounds As Rectangle = player.GetBounds()
            
            ' Check if bounds intersect
            Return enemyBounds.IntersectsWith(playerBounds)
        End Function
        
        ' Check collision between bullet and boss
        Public Shared Function CheckCollision(bullet As GalaxySaver.Entities.Bullet, boss As GalaxySaver.Entities.Boss) As Boolean
            ' If either entity is not active, no collision
            If Not bullet.IsActive Or Not boss.IsActive Then
                Return False
            End If
            
            ' Only check player bullets against boss
            If Not bullet.IsPlayerBullet Then
                Return False
            End If
            
            ' Get entity bounds
            Dim bulletBounds As Rectangle = bullet.GetBounds()
            Dim bossBounds As Rectangle = boss.GetBounds()
            
            ' Check if bounds intersect
            Return bulletBounds.IntersectsWith(bossBounds)
        End Function
        
        ' Check collision between power-up and player
        Public Shared Function CheckCollision(powerUp As GalaxySaver.Entities.PowerUp, player As GalaxySaver.Entities.Player) As Boolean
            ' If either entity is not active, no collision
            If Not powerUp.IsActive Or Not player.IsActive Then
                Return False
            End If
            
            ' Get entity bounds
            Dim powerUpBounds As Rectangle = powerUp.GetBounds()
            Dim playerBounds As Rectangle = player.GetBounds()
            
            ' Check if bounds intersect
            Return powerUpBounds.IntersectsWith(playerBounds)
        End Function
    End Class
End Namespace