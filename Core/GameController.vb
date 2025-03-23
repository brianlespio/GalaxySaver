Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Core
    Public Class GameController
        ' Game state constants
        Public Enum GameState
            MainMenu
            Playing
            Paused
            GameOver
            LevelComplete
        End Enum

        ' Current game state
        Private _currentState As GameState = GameState.MainMenu
        Public Property CurrentState As GameState
            Get
                Return _currentState
            End Get
            Set(value As GameState)
                _currentState = value
            End Set
        End Property

        ' Game objects
        Private _player As GalaxySaver.Entities.Player
        Private _bullets As New List(Of GalaxySaver.Entities.Bullet)
        Private _enemyBullets As New List(Of GalaxySaver.Entities.Bullet)
        Private _enemies As New List(Of GalaxySaver.Entities.Enemy)
        Private _powerUps As New List(Of GalaxySaver.Entities.PowerUp)
        
        ' Game properties
        Private _score As Integer = 0
        Private _level As Integer = 1
        Private _lives As Integer = 3
        Private _parentForm As Form
        Private _random As New Random()
        Private _enemyShootTimer As Integer = 0
        Private _enemyShootFrequency As Integer = 100 ' Higher = less frequent
        Private _formationDirection As Integer = 1 ' 1 = right, -1 = left
        Private _formationSpeed As Integer = 1
        Private _formationDropDistance As Integer = 10
        Private _formationEdgeBuffer As Integer = 50
        Private _enemyDiveProbability As Integer = 200 ' Higher = less frequent
        Private _enemyFormation As GalaxySaver.Entities.EnemyFormation

        ' Game settings
        Private _gameWidth As Integer
        Private _gameHeight As Integer

        ' Properties
        Public ReadOnly Property Score As Integer
            Get
                Return _score
            End Get
        End Property

        Public ReadOnly Property Level As Integer
            Get
                Return _level
            End Get
        End Property

        Public ReadOnly Property Lives As Integer
            Get
                Return _lives
            End Get
        End Property

        Public ReadOnly Property Player As GalaxySaver.Entities.Player
            Get
                Return _player
            End Get
        End Property

        Public ReadOnly Property Enemies As List(Of GalaxySaver.Entities.Enemy)
            Get
                Return _enemies
            End Get
        End Property

        Public ReadOnly Property Bullets As List(Of GalaxySaver.Entities.Bullet)
            Get
                Return _bullets
            End Get
        End Property

        Public ReadOnly Property EnemyBullets As List(Of GalaxySaver.Entities.Bullet)
            Get
                Return _enemyBullets
            End Get
        End Property

        Public ReadOnly Property PowerUps As List(Of GalaxySaver.Entities.PowerUp)
            Get
                Return _powerUps
            End Get
        End Property

        ' Constructor
        Public Sub New(form As Form, gameWidth As Integer, gameHeight As Integer)
            _parentForm = form
            _gameWidth = gameWidth
            _gameHeight = gameHeight
        End Sub

        ' Initialize the game
        Public Sub InitializeGame()
            ' Reset game state
            _score = 0
            _level = 1
            _lives = 3
            _currentState = GameState.Playing
            
            ' Clear all game objects
            _bullets.Clear()
            _enemyBullets.Clear()
            _enemies.Clear()
            _powerUps.Clear()
            
            ' Initialize player
            InitializePlayer()
            
            ' Initialize enemy formation
            InitializeEnemies()
            
            ' Initialize sound
            GalaxySaver.Utils.SoundManager.Initialize()
        End Sub

        ' Initialize the player
        Private Sub InitializePlayer()
            ' Create player at the bottom center of the screen
            _player = New GalaxySaver.Entities.Player(_gameWidth \ 2, _gameHeight - 60)
            _player.Initialize(_parentForm)
        End Sub

        ' Initialize enemies in formation
        Private Sub InitializeEnemies()
            ' Create enemy formation
            _enemyFormation = New GalaxySaver.Entities.EnemyFormation(_gameWidth, _level)
            _enemies = _enemyFormation.CreateFormation(_parentForm)
        End Sub

        ' Update game state - called every frame
        Public Sub Update()
            If _currentState <> GameState.Playing Then Return

            ' Update player
            _player.Update()

            ' Update enemy formation movement
            UpdateEnemyFormation()

            ' Update enemy dive bombing
            UpdateEnemyDiving()

            ' Update enemy shooting
            UpdateEnemyShooting()

            ' Update bullets
            UpdateBullets()

            ' Update enemy bullets
            UpdateEnemyBullets()

            ' Update power-ups
            UpdatePowerUps()

            ' Check collisions
            CheckCollisions()

            ' Check level completion
            CheckLevelCompletion()
        End Sub

        ' Update enemy formation movement
        Private Sub UpdateEnemyFormation()
            ' Move formation left/right
            Dim reachedEdge As Boolean = False
            
            ' Check if any enemy has reached the edge
            For Each enemy In _enemies
                If Not enemy.IsDiving Then
                    If (enemy.X <= _formationEdgeBuffer And _formationDirection = -1) Or 
                       (enemy.X >= _gameWidth - enemy.Width - _formationEdgeBuffer And _formationDirection = 1) Then
                        reachedEdge = True
                        Exit For
                    End If
                End If
            Next

            ' If reached edge, change direction and move down
            If reachedEdge Then
                _formationDirection *= -1
                For Each enemy In _enemies
                    If Not enemy.IsDiving Then
                        enemy.Y += _formationDropDistance
                    End If
                Next
            Else
                ' Move all enemies in formation horizontally
                For Each enemy In _enemies
                    If Not enemy.IsDiving Then
                        enemy.X += _formationDirection * _formationSpeed
                        enemy.UpdatePosition()
                    End If
                Next
            End If
        End Sub

        ' Update enemy dive bombing
        Private Sub UpdateEnemyDiving()
            ' Randomly select enemies to dive
            For Each enemy In _enemies
                If Not enemy.IsDiving AndAlso _random.Next(0, _enemyDiveProbability) = 0 Then
                    enemy.StartDiving(_player.X, _player.Y)
                End If

                ' Update diving enemies
                If enemy.IsDiving Then
                    enemy.UpdateDiving(_player.X, _player.Y)
                    
                    ' Check if diving is complete
                    If enemy.DivingComplete Then
                        enemy.StopDiving()
                    End If
                End If
            Next
        End Sub

        ' Update enemy shooting
        Private Sub UpdateEnemyShooting()
            _enemyShootTimer += 1
            
            If _enemyShootTimer >= _enemyShootFrequency Then
                _enemyShootTimer = 0
                
                ' Find enemies that can shoot (lowest in each column)
                Dim shootingEnemies As New List(Of GalaxySaver.Entities.Enemy)()
                
                ' Simple approach: randomly select an enemy to shoot
                If _enemies.Count > 0 Then
                    Dim shootingEnemy = _enemies(_random.Next(0, _enemies.Count))
                    EnemyShoot(shootingEnemy)
                End If
            End If
        End Sub

        ' Enemy shoots a bullet
        Private Sub EnemyShoot(enemy As GalaxySaver.Entities.Enemy)
            Dim bullet As New GalaxySaver.Entities.Bullet(enemy.X + enemy.Width \ 2, enemy.Y + enemy.Height, False)
            bullet.Initialize(_parentForm, Color.Red)
            _enemyBullets.Add(bullet)
            GalaxySaver.Utils.SoundManager.PlayEnemyShoot()
        End Sub

        ' Update bullets
        Private Sub UpdateBullets()
            For i As Integer = _bullets.Count - 1 To 0 Step -1
                _bullets(i).Update()
                
                ' Remove bullets that are off-screen
                If _bullets(i).Y < -_bullets(i).Height Then
                    _bullets(i).Dispose()
                    _bullets.RemoveAt(i)
                End If
            Next
        End Sub

        ' Update enemy bullets
        Private Sub UpdateEnemyBullets()
            For i As Integer = _enemyBullets.Count - 1 To 0 Step -1
                _enemyBullets(i).Update()
                
                ' Remove bullets that are off-screen
                If _enemyBullets(i).Y > _gameHeight Then
                    _enemyBullets(i).Dispose()
                    _enemyBullets.RemoveAt(i)
                End If
            Next
        End Sub

        ' Update power-ups
        Private Sub UpdatePowerUps()
            For i As Integer = _powerUps.Count - 1 To 0 Step -1
                _powerUps(i).Update()
                
                ' Remove power-ups that are off-screen
                If _powerUps(i).Y > _gameHeight Then
                    _powerUps(i).Dispose()
                    _powerUps.RemoveAt(i)
                End If
            Next
        End Sub

        ' Player shoots a bullet
        Public Sub PlayerShoot()
            If _currentState <> GameState.Playing Then Return
            
            ' Create bullet at player position
            Dim bullet As New GalaxySaver.Entities.Bullet(_player.X + _player.Width \ 2, _player.Y, True)
            bullet.Initialize(_parentForm, Color.Yellow)
            _bullets.Add(bullet)
            
            ' Play sound
            GalaxySaver.Utils.SoundManager.PlayShoot()
        End Sub

        ' Check collisions between game objects
        Private Sub CheckCollisions()
            ' Check player bullets vs enemies
            For i As Integer = _bullets.Count - 1 To 0 Step -1
                For j As Integer = _enemies.Count - 1 To 0 Step -1
                    If GalaxySaver.Utils.Collision.CheckCollision(_bullets(i), _enemies(j)) Then
                        ' Enemy hit
                        Dim pointValue As Integer = 100
                        
                        ' Bonus points for diving enemies
                        If _enemies(j).IsDiving Then
                            pointValue = 300
                        End If
                        
                        ' Add score
                        _score += pointValue
                        
                        ' Remove bullet
                        _bullets(i).Dispose()
                        _bullets.RemoveAt(i)
                        
                        ' Remove enemy
                        _enemies(j).Explode()
                        _enemies.RemoveAt(j)
                        
                        ' Play sound
                        GalaxySaver.Utils.SoundManager.PlayExplosion()
                        
                        ' Chance to spawn power-up
                        If _random.Next(0, 10) = 0 Then
                            SpawnPowerUp(_enemies(j).X, _enemies(j).Y)
                        End If
                        
                        Exit For
                    End If
                Next
            Next

            ' Check enemy bullets vs player
            For i As Integer = _enemyBullets.Count - 1 To 0 Step -1
                If GalaxySaver.Utils.Collision.CheckCollision(_enemyBullets(i), _player) Then
                    ' Player hit
                    _enemyBullets(i).Dispose()
                    _enemyBullets.RemoveAt(i)
                    
                    ' Decrease lives
                    _lives -= 1
                    
                    ' Play sound
                    GalaxySaver.Utils.SoundManager.PlayPlayerHit()
                    
                    ' Check game over
                    If _lives <= 0 Then
                        GameOver()
                    End If
                    
                    Exit For
                End If
            Next

            ' Check enemies vs player
            For i As Integer = _enemies.Count - 1 To 0 Step -1
                If GalaxySaver.Utils.Collision.CheckCollision(_enemies(i), _player) Then
                    ' Player hit
                    _enemies(i).Explode()
                    _enemies.RemoveAt(i)
                    
                    ' Decrease lives
                    _lives -= 1
                    
                    ' Play sound
                    GalaxySaver.Utils.SoundManager.PlayPlayerHit()
                    
                    ' Check game over
                    If _lives <= 0 Then
                        GameOver()
                    End If
                    
                    Exit For
                End If
            Next

            ' Check power-ups vs player
            For i As Integer = _powerUps.Count - 1 To 0 Step -1
                If GalaxySaver.Utils.Collision.CheckCollision(_powerUps(i), _player) Then
                    ' Apply power-up effect
                    ApplyPowerUp(_powerUps(i))
                    
                    ' Remove power-up
                    _powerUps(i).Dispose()
                    _powerUps.RemoveAt(i)
                    
                    ' Play sound
                    GalaxySaver.Utils.SoundManager.PlayPowerUp()
                    
                    Exit For
                End If
            Next
        End Sub

        ' Spawn a power-up
        Private Sub SpawnPowerUp(x As Integer, y As Integer)
            Dim powerUpType As GalaxySaver.Entities.PowerUpType = CType(_random.Next(0, [Enum].GetValues(GetType(GalaxySaver.Entities.PowerUpType)).Length), GalaxySaver.Entities.PowerUpType)
            Dim powerUp As New GalaxySaver.Entities.PowerUp(x, y, powerUpType)
            powerUp.Initialize(_parentForm)
            _powerUps.Add(powerUp)
        End Sub

        ' Apply power-up effect
        Private Sub ApplyPowerUp(powerUp As GalaxySaver.Entities.