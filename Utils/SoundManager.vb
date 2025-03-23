Imports System.Media
Imports System.IO

Namespace GalaxySaver.Utils
    Public Class SoundManager
        ' Sound players
        Private Shared _shootSound As SoundPlayer
        Private Shared _enemyShootSound As SoundPlayer
        Private Shared _explosionSound As SoundPlayer
        Private Shared _playerHitSound As SoundPlayer
        Private Shared _gameOverSound As SoundPlayer
        Private Shared _levelCompleteSound As SoundPlayer
        Private Shared _powerUpSound As SoundPlayer
        
        ' Resource path
        Private Shared ReadOnly _resourcePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources")
        
        ' Initialize sound manager
        Public Shared Sub Initialize()
            Try
                ' Initialize sound players
                _shootSound = New SoundPlayer(Path.Combine(_resourcePath, "shoot.wav"))
                _enemyShootSound = New SoundPlayer(Path.Combine(_resourcePath, "enemy_shoot.wav"))
                _explosionSound = New SoundPlayer(Path.Combine(_resourcePath, "explosion.wav"))
                _playerHitSound = New SoundPlayer(Path.Combine(_resourcePath, "player_explosion.wav"))
                _gameOverSound = New SoundPlayer(Path.Combine(_resourcePath, "game_over.wav"))
                _levelCompleteSound = New SoundPlayer(Path.Combine(_resourcePath, "victory.wav"))
                _powerUpSound = New SoundPlayer(Path.Combine(_resourcePath, "powerup.wav"))
                
                ' Load sounds
                _shootSound.LoadAsync()
                _enemyShootSound.LoadAsync()
                _explosionSound.LoadAsync()
                _playerHitSound.LoadAsync()
                _gameOverSound.LoadAsync()
                _levelCompleteSound.LoadAsync()
                
                ' If powerup sound doesn't exist, use a default sound
                If Not File.Exists(Path.Combine(_resourcePath, "powerup.wav")) Then
                    _powerUpSound = _levelCompleteSound
                Else
                    _powerUpSound.LoadAsync()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error initializing sound manager: " & ex.Message)
            End Try
        End Sub
        
        ' Play shoot sound
        Public Shared Sub PlayShoot()
            Try
                If _shootSound IsNot Nothing Then
                    _shootSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing shoot sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play enemy shoot sound
        Public Shared Sub PlayEnemyShoot()
            Try
                If _enemyShootSound IsNot Nothing Then
                    _enemyShootSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing enemy shoot sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play explosion sound
        Public Shared Sub PlayExplosion()
            Try
                If _explosionSound IsNot Nothing Then
                    _explosionSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing explosion sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play player hit sound
        Public Shared Sub PlayPlayerHit()
            Try
                If _playerHitSound IsNot Nothing Then
                    _playerHitSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing player hit sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play game over sound
        Public Shared Sub PlayGameOver()
            Try
                If _gameOverSound IsNot Nothing Then
                    _gameOverSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing game over sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play level complete sound
        Public Shared Sub PlayLevelComplete()
            Try
                If _levelCompleteSound IsNot Nothing Then
                    _levelCompleteSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing level complete sound: " & ex.Message)
            End Try
        End Sub
        
        ' Play power-up sound
        Public Shared Sub PlayPowerUp()
            Try
                If _powerUpSound IsNot Nothing Then
                    _powerUpSound.Play()
                End If
            Catch ex As Exception
                Debug.WriteLine("Error playing power-up sound: " & ex.Message)
            End Try
        End Sub
        
        ' Dispose sound resources
        Public Shared Sub Dispose()
            Try
                If _shootSound IsNot Nothing Then _shootSound.Dispose()
                If _enemyShootSound IsNot Nothing Then _enemyShootSound.Dispose()
                If _explosionSound IsNot Nothing Then _explosionSound.Dispose()
                If _playerHitSound IsNot Nothing Then _playerHitSound.Dispose()
                If _gameOverSound IsNot Nothing Then _gameOverSound.Dispose()
                If _levelCompleteSound IsNot Nothing Then _levelCompleteSound.Dispose()
                If _powerUpSound IsNot Nothing Then _powerUpSound.Dispose()
            Catch ex As Exception
                Debug.WriteLine("Error disposing sound resources: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace