Public Class Sounds
    Private Shared shootSound As New System.Media.SoundPlayer("Resources/shoot.wav")
    Private Shared explosionSound As New System.Media.SoundPlayer("Resources/explosion.wav")

    Public Shared Sub PlayShoot()
        shootSound.Play()
    End Sub

    Public Shared Sub PlayExplosion()
        explosionSound.Play()
    End Sub
End Class
