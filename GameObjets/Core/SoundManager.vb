Imports System.Media
Imports System.IO

Public Class SoundManager
    Private Shared ReadOnly ResourcePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources")
    Private Shared shootSound As SoundPlayer
    Private Shared explosionSound As SoundPlayer
    Private Shared gameOverSound As SoundPlayer

    Shared Sub New()
        Try
            shootSound = New SoundPlayer(Path.Combine(ResourcePath, "shoot.wav"))
            explosionSound = New SoundPlayer(Path.Combine(ResourcePath, "explosion.wav"))
            gameOverSound = New SoundPlayer(Path.Combine(ResourcePath, "gameover.wav"))
        Catch ex As Exception
            Debug.WriteLine("Error loading sound resources: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub PlayShoot()
        Try
            If shootSound IsNot Nothing Then shootSound.Play()
        Catch ex As Exception
            Debug.WriteLine("Error playing shoot sound: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub PlayExplosion()
        Try
            If explosionSound IsNot Nothing Then explosionSound.Play()
        Catch ex As Exception
            Debug.WriteLine("Error playing explosion sound: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub PlayGameOver()
        Try
            If gameOverSound IsNot Nothing Then gameOverSound.Play()
        Catch ex As Exception
            Debug.WriteLine("Error playing game over sound: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub Dispose()
        Try
            If shootSound IsNot Nothing Then shootSound.Dispose()
            If explosionSound IsNot Nothing Then explosionSound.Dispose()
            If gameOverSound IsNot Nothing Then gameOverSound.Dispose()
        Catch ex As Exception
            Debug.WriteLine("Error disposing sound resources: " & ex.Message)
        End Try
    End Sub
End Class
