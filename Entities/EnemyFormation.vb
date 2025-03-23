Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    Public Class EnemyFormation
        ' Formation properties
        Private _gameWidth As Integer
        Private _level As Integer
        Private _rows As Integer = 3
        Private _columns As Integer = 5
        Private _horizontalSpacing As Integer = 60
        Private _verticalSpacing As Integer = 50
        Private _startX As Integer
        Private _startY As Integer = 50
        
        ' Constructor
        Public Sub New(gameWidth As Integer, level As Integer)
            _gameWidth = gameWidth
            _level = level
            
            ' Adjust formation based on level
            AdjustFormationForLevel()
            
            ' Calculate starting X position to center formation
            _startX = (_gameWidth - (_columns * _horizontalSpacing)) \ 2
        End Sub
        
        ' Adjust formation based on level
        Private Sub AdjustFormationForLevel()
            ' Increase formation size based on level
            If _level >= 3 Then
                _rows = 4
            End If
            
            If _level >= 2 Then
                _columns = 6
            End If
            
            If _level >= 4 Then
                _columns = 7
            End If
            
            ' Cap at maximum size
            _rows = Math.Min(_rows, 6)
            _columns = Math.Min(_columns, 8)
        End Sub
        
        ' Create enemy formation
        Public Function CreateFormation(parentForm As Form) As List(Of Enemy)
            Dim enemies As New List(Of Enemy)()
            
            For row As Integer = 0 To _rows - 1
                For col As Integer = 0 To _columns - 1
                    ' Calculate enemy position
                    Dim x As Integer = _startX + (col * _horizontalSpacing)
                    Dim y As Integer = _startY + (row * _verticalSpacing)
                    
                    ' Determine enemy type based on row
                    Dim enemyType As Integer = 0
                    
                    If row = 0 Then
                        enemyType = 2 ' Advanced enemies in top row
                    ElseIf row = 1 Then
                        enemyType = 1 ' Medium enemies in second row
                    Else
                        enemyType = 0 ' Basic enemies in other rows
                    End If
                    
                    ' Create enemy
                    Dim enemy As New Enemy(x, y, enemyType)
                    enemy.Initialize(parentForm)
                    enemies.Add(enemy)
                Next
            Next
            
            Return enemies
        End Function
        
        ' Get formation width
        Public Function GetFormationWidth() As Integer
            Return _columns * _horizontalSpacing
        End Function
        
        ' Get formation height
        Public Function GetFormationHeight() As Integer
            Return _rows * _verticalSpacing
        End Function
        
        ' Get number of enemies in formation
        Public Function GetEnemyCount() As Integer
            Return _rows * _columns
        End Function
    End Class
End Namespace