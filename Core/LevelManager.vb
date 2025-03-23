Imports System.Collections.Generic

Namespace GalaxySaver.Core
    Public Class LevelManager
        ' Level properties
        Private _currentLevel As Integer = 1
        Private _maxLevel As Integer = 10
        Private _enemySpeed As Integer
        Private _enemyShootFrequency As Integer
        Private _enemyDiveProbability As Integer
        Private _formationSpeed As Integer
        
        ' Level configuration
        Private _levelConfigurations As New Dictionary(Of Integer, LevelConfig)
        
        ' Constructor
        Public Sub New()
            InitializeLevelConfigurations()
        End Sub
        
        ' Properties
        Public ReadOnly Property CurrentLevel As Integer
            Get
                Return _currentLevel
            End Get
        End Property
        
        Public ReadOnly Property EnemySpeed As Integer
            Get
                Return _enemySpeed
            End Get
        End Property
        
        Public ReadOnly Property EnemyShootFrequency As Integer
            Get
                Return _enemyShootFrequency
            End Get
        End Property
        
        Public ReadOnly Property EnemyDiveProbability As Integer
            Get
                Return _enemyDiveProbability
            End Get
        End Property
        
        Public ReadOnly Property FormationSpeed As Integer
            Get
                Return _formationSpeed
            End Get
        End Property
        
        Public ReadOnly Property HasBoss As Boolean
            Get
                Return _currentLevel Mod 5 = 0 ' Boss every 5 levels
            End Get
        End Property
        
        ' Initialize level configurations
        Private Sub InitializeLevelConfigurations()
            ' Level 1 - Basic enemy formation with slow movement
            _levelConfigurations.Add(1, New LevelConfig() With {
                .EnemySpeed = 1,
                .EnemyShootFrequency = 150, ' Higher = less frequent
                .EnemyDiveProbability = 300, ' Higher = less frequent
                .FormationSpeed = 1,
                .EnemyRows = 3,
                .EnemyColumns = 5,
                .HasBoss = False
            })
            
            ' Level 2 - Slightly faster enemies
            _levelConfigurations.Add(2, New LevelConfig() With {
                .EnemySpeed = 1,
                .EnemyShootFrequency = 130,
                .EnemyDiveProbability = 250,
                .FormationSpeed = 2,
                .EnemyRows = 3,
                .EnemyColumns = 6,
                .HasBoss = False
            })
            
            ' Level 3 - More enemies
            _levelConfigurations.Add(3, New LevelConfig() With {
                .EnemySpeed = 2,
                .EnemyShootFrequency = 120,
                .EnemyDiveProbability = 200,
                .FormationSpeed = 2,
                .EnemyRows = 4,
                .EnemyColumns = 6,
                .HasBoss = False
            })
            
            ' Level 4 - Faster enemies with more frequent shooting
            _levelConfigurations.Add(4, New LevelConfig() With {
                .EnemySpeed = 2,
                .EnemyShootFrequency = 100,
                .EnemyDiveProbability = 150,
                .FormationSpeed = 3,
                .EnemyRows = 4,
                .EnemyColumns = 7,
                .HasBoss = False
            })
            
            ' Level 5 - First boss level
            _levelConfigurations.Add(5, New LevelConfig() With {
                .EnemySpeed = 2,
                .EnemyShootFrequency = 90,
                .EnemyDiveProbability = 120,
                .FormationSpeed = 3,
                .EnemyRows = 4,
                .EnemyColumns = 7,
                .HasBoss = True
            })
            
            ' Levels 6-10 - Increasing difficulty
            For i As Integer = 6 To 10
                _levelConfigurations.Add(i, New LevelConfig() With {
                    .EnemySpeed = Math.Min(i \ 2, 5),
                    .EnemyShootFrequency = Math.Max(150 - (i * 10), 50),
                    .EnemyDiveProbability = Math.Max(300 - (i * 20), 80),
                    .FormationSpeed = Math.Min(i \ 2 + 1, 5),
                    .EnemyRows = Math.Min(3 + (i \ 2), 6),
                    .EnemyColumns = Math.Min(5 + (i \ 3), 8),
                    .HasBoss = (i Mod 5 = 0)
                })
            Next
        End Sub
        
        ' Load level configuration
        Public Sub LoadLevel(level As Integer)
            _currentLevel = Math.Max(1, Math.Min(level, _maxLevel))
            
            ' Get level configuration
            Dim config As LevelConfig = _levelConfigurations(_currentLevel)
            
            ' Set level properties
            _enemySpeed = config.EnemySpeed
            _enemyShootFrequency = config.EnemyShootFrequency
            _enemyDiveProbability = config.EnemyDiveProbability
            _formationSpeed = config.FormationSpeed
        End Sub
        
        ' Advance to next level
        Public Sub NextLevel()
            LoadLevel(_currentLevel + 1)
        End Sub
        
        ' Get level completion bonus
        Public Function GetLevelCompletionBonus() As Integer
            Return _currentLevel * 1000
        End Function
        
        ' Get formation completion bonus
        Public Function GetFormationCompletionBonus() As Integer
            Return _currentLevel * 500
        End Function
        
        ' Get boss defeat bonus
        Public Function GetBossDefeatBonus() As Integer
            Return _currentLevel * 2000
        End Function
        
        ' Get enemy rows for current level
        Public Function GetEnemyRows() As Integer
            Return _levelConfigurations(_currentLevel).EnemyRows
        End Function
        
        ' Get enemy columns for current level
        Public Function GetEnemyColumns() As Integer
            Return _levelConfigurations(_currentLevel).EnemyColumns
        End Function
    End Class
    
    ' Level configuration class
    Public Class LevelConfig
        Public Property EnemySpeed As Integer
        Public Property EnemyShootFrequency As Integer
        Public Property EnemyDiveProbability As Integer
        Public Property FormationSpeed As Integer
        Public Property EnemyRows As Integer
        Public Property EnemyColumns As Integer
        Public Property HasBoss As Boolean
    End Class
End Namespace