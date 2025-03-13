Imports GameObjets.Core ' Asegúrate de importar las clases necesarias

Public Class GameForm
    Private gameController As GameController
    Private player As Player
    Private bullets As List(Of Bullet)
    Private enemies As List(Of Enemy)

    Public Sub New()
        InitializeComponent()

        ' Inicializa las variables
        gameController = New GameController()
        player = New Player()
        bullets = New List(Of Bullet)()
        enemies = New List(Of Enemy)()

        ' Configura el juego
        gameController.InitializeGame(player, bullets, enemies)
    End Sub

    ' Aquí van todos los demás métodos y controladores de eventos relacionados con el formulario
    ' Por ejemplo, eventos para mover al jugador y disparar
End Class
