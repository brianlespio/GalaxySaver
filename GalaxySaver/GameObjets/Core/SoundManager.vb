Imports System.Media

Public Class SoundManager
    Private Shared shootSound As New SoundPlayer("Resources/shoot.wav")
    Private Shared explosionSound As New SoundPlayer("Resources/explosion.wav")
    Private Shared gameOverSound As New SoundPlayer("Resources/gameover.wav")

    Public Shared Sub PlayShoot()
        shootSound.Play()
    End Sub

    Public Shared Sub PlayExplosion()
        explosionSound.Play()
    End Sub

    Public Shared Sub PlayGameOver()
        gameOverSound.Play()
    End Sub
End Class
