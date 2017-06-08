''' <summary>
''' The main screen for all accounts.
''' </summary>
''' <remarks></remarks>
Public Class Accounts

    ''' <summary>
    ''' Login as an existing user.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub existingUser(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Me.Hide()
        AccountsLogin.Show()
    End Sub

    ''' <summary>
    ''' Register as a new user.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub registerUser(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        AccountsRegister.Show()
    End Sub

    ''' <summary>
    ''' Login as an Administrator.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub adminControlPanel(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkAdministrator.LinkClicked
        Me.Hide()
        AccountsAdmin.Show()
    End Sub

    ''' <summary>
    ''' What to do when the form is loaded?
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub initiateForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub
End Class
