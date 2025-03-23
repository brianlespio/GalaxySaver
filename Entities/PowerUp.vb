Imports System.Drawing
Imports System.Windows.Forms

Namespace GalaxySaver.Entities
    ' Power-up types
    Public Enum PowerUpType
        None = 0
        DoubleShot = 1
        SpeedBoost = 2
        Shield = 3
        SmartBomb = 4
    End Enum
    
    Public Class PowerUp
        Inherits Entity
        
        ' Power-up properties
        Private _type As PowerUpType
        Private _fallSpeed As Integer = 2
        
        ' Properties
        Public ReadOnly Property Type As PowerUpType
            Get
                Return _type
            End Get
        End Property
        
        ' Constructor
        Public Sub New(x As Integer, y As Integer, type As PowerUpType)
            MyBase.New(x, y, 20, 20) ' Power-up size is 20x20 pixels
            _type = type
        End Sub
        
        ' Initialize power-up
        Public Overrides Sub Initialize(