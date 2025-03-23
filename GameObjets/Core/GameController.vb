Imports System.Collections.Generic

Public Class GameController
    Private bullets As New List(Of Bullet)
    Private enemies As New List(Of Enemy)
    Private player As Player
    Private parentForm As Form
    Private _score As Integer = 0

    Public ReadOnly Property Score As Integer
        Get
            Return _score
        End Get
    End Property

    Public Sub New(form As Form, player As Player)
        Me.parentForm = form
        Me.player = player
        InitializeEnemies()
    End Sub

    Private Sub InitializeEnemies()
        For i As Integer = 0 To 4
            For j As Integer = 0 To 2
                Dim enemy As New Enemy(50 + i * 100, 50 + j * 70)
                enemy.PictureBox = New PictureBox With {
                    .Size = New Size(30, 30),
                    .BackColor = Color.Red,
                    .Location = New Point(enemy.X, enemy.Y)
                }
                enemies.Add(enemy)
                parentForm.Controls.Add(enemy.PictureBox)
            Next
        Next
    End Sub

    Public Sub MoveEnemies()
        For Each enemy In enemies
            enemy.Y += 2
            enemy.PictureBox.Location = New Point(enemy.X, enemy.Y)

            If enemy.Y > parentForm.Height Then
                enemy.Y = -50
                enemy.X = Random.Shared.Next(0, parentForm.Width - 30)
            End If
        Next
        CheckCollisions()
    End Sub

    Public Sub MoveBullets()
        For i As Integer = bullets.Count - 1 To 0 Step -1
            bullets(i).Y -= 10
            bullets(i).PictureBox.Location = New Point(bullets(i).X, bullets(i).Y)

            If bullets(i).Y < -10 Then
                parentForm.Controls.Remove(bullets(i).PictureBox)
                bullets.RemoveAt(i)
            End If
        Next
    End Sub

    Public Sub PlayerShoot()
        Dim bullet As New Bullet(player.X + player.PictureBox.Width \ 2, player.Y)
        bullet.PictureBox = New PictureBox With {
            .Size = New Size(5, 15),
            .BackColor = Color.Yellow,
            .Location = New Point(bullet.X, bullet.Y)
        }
        bullets.Add(bullet)
        parentForm.Controls.Add(bullet.PictureBox)
        SoundManager.PlayShoot()
    End Sub

    Private Sub CheckCollisions()
        For i As Integer = bullets.Count - 1 To 0 Step -1
            For j As Integer = enemies.Count - 1 To 0 Step -1
                If bullets(i).PictureBox.Bounds.IntersectsWith(enemies(j).PictureBox.Bounds) Then
                    parentForm.Controls.Remove(bullets(i).PictureBox)
                    bullets.RemoveAt(i)
                    enemies(j).Y = -50
                    enemies(j).X = Random.Shared.Next(0, parentForm.Width - 30)
                    _score += 100
                    SoundManager.PlayExplosion()
                    Exit For
                End If
            Next
        Next

        For Each enemy In enemies
            If enemy.PictureBox.Bounds.IntersectsWith(player.PictureBox.Bounds) Then
                GameOver()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub GameOver()
        For Each control In parentForm.Controls
            If TypeOf control Is Timer Then
                DirectCast(control, Timer).Stop()
            End If
        Next
        SoundManager.PlayGameOver()
        For Each control In parentForm.Controls
            If TypeOf control Is Label AndAlso control.Name = "lblGameOver" Then
                control.Text = "Game Over! Score: " & _score
                control.Visible = True
                Exit For
            End If
        Next
            End If
        Next
        SoundManager.PlayGameOver()
        For Each control In parentForm.Controls
            If TypeOf control Is Label AndAlso control.Name = "lblGameOver" Then
                control.Text = "Game Over! Score: " & score
                control.Visible = True
                Exit For
            End If
        Next
    End Sub
End Class
