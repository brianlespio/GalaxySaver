Public Class Enemy
    Public X As Integer
    Public Y As Integer

    Public Sub New(startX As Integer, startY As Integer)
        X = startX
        Y = startY
    End Sub
End Class


' Program.vb
Imports System.Windows.Forms

Public Class Program
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New GameForm())
    End Sub
End Class
