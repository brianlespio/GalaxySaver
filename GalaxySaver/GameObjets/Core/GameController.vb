Imports System.Collections.Generic

Public Class GameController
    Private bullets As List(Of Bullet)
    Private enemies As List(Of Enemy)

    Public Sub InitializeGame(ByVal player As Player, ByVal bullets As List(Of Bullet), ByVal enemies As List(Of Enemy))
        Me.bullets = bullets
        Me.enemies = enemies
    End Sub
End Class


' Gameform.vb
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class GameForm
    Inherits Form

    Private bullets As List(Of Bullet)
    Private enemies As List(Of Enemy)
    Private player As Player

    Public Sub New()
        InitializeComponent()
        player = New Player(100, 100) ' Se inicializa el jugador con coordenadas de inicio
        bullets = New List(Of Bullet)()
        enemies = New List(Of Enemy)()
    End Sub
End Class
