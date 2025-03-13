Public Class Form1
    Private jugador As Player
    Private gameController As GameController
    Private WithEvents tmrGame As New Timer()
    Private WithEvents tmrBullets As New Timer()
    Private lblScore As New Label()
    Private lblGameOver As New Label()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartGame()
    End Sub

    Private Sub StartGame()
        Me.Controls.Clear()
        jugador = New Player(New PictureBox() With {.Size = New Size(40, 40), .BackColor = Color.Blue, .Location = New Point(Me.Width \ 2, Me.Height - 60)})
        Me.Controls.Add(jugador.PictureBox)
        gameController = New GameController(Me, jugador)

        tmrGame.Interval = 50
        tmrGame.Start()
        tmrBullets.Interval = 30
        tmrBullets.Start()

        lblScore.Text = "Score: 0"
        lblScore.ForeColor = Color.White
        lblScore.Location = New Point(10, 10)
        Me.Controls.Add(lblScore)

        lblGameOver.Visible = False
        Me.Controls.Add(lblGameOver)
    End Sub

    Private Sub tmrGame_Tick(sender As Object, e As EventArgs) Handles tmrGame.Tick
        gameController.MoveEnemies()
    End Sub

    Private Sub tmrBullets_Tick(sender As Object, e As EventArgs) Handles tmrBullets.Tick
        gameController.MoveBullets()
        lblScore.Text = "Score: " & gameController.Score
    End Sub
End Class
