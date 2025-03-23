Imports System.Windows.Forms
Imports System.Drawing

Namespace GameObjets.Core
    Public Class Bullet
        Public Property X As Integer
        Public Property Y As Integer
        Public Property IsActive As Boolean
        Public Property PictureBox As PictureBox

        Public Sub New(ByVal startX As Integer, ByVal startY As Integer)
            X = startX
            Y = startY
            IsActive = True
        End Sub
    End Class
End Namespace
