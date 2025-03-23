Public Class Form1
    Inherits Form
    Private jugador As Player
    Private gameController As GameController
    Private WithEvents tmrGame As New Timer()
    Private WithEvents tmrBullets As New Timer()
    Private lblScore As New Label()
    Private lblGameOver As New Label()

    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
        Me.BackColor = Color.Black
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.KeyPreview = True

        lblGameOver.AutoSize = True
        lblGameOver.Font = New Font("Arial", 24, FontStyle.Bold)
        lblGameOver.ForeColor = Color.Red
        lblGameOver.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Left
                jugador?.MoveLeft()
            Case Keys.Right
                jugador?.MoveRight(Me.Width)
            Case Keys.Space
                gameController?.PlayerShoot()
            Case Keys.Escape
                Me.Close()
            Case Keys.Enter
                If lblGameOver.Visible Then StartGame()
        End Select
    End Sub

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
        lblScore.Font = New Font("Arial", 14, FontStyle.Bold)
        lblScore.ForeColor = Color.White
        lblScore.Location = New Point(10, 10)
        Me.Controls.Add(lblScore)

        lblGameOver.Text = "Game Over!"
        lblGameOver.Location = New Point(Me.Width \ 2 - lblGameOver.Width \ 2, Me.Height \ 2 - lblGameOver.Height \ 2)
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
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Try
            tmrGame.Stop()
            tmrBullets.Stop()
            SoundManager.Dispose()
            For Each control In Me.Controls
                If TypeOf control Is PictureBox Then
                    DirectCast(control, PictureBox).Dispose()
                End If
            Next
            Me.Controls.Clear()
        Catch ex As Exception
            Debug.WriteLine("Error during form cleanup: " & ex.Message)
        End Try
        MyBase.OnFormClosing(e)
    End Sub
End Class
