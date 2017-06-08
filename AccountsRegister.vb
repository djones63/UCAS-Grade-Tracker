''' <summary>
''' A class to handle account registration.
''' </summary>
''' <remarks></remarks>
Public Class AccountsRegister

    ''' <summary>
    ''' What to do when the form loads up?
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub initiateForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    ''' <summary>
    ''' Allows for the user to toggle password visibility.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub togglePasswordVisibility(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = Not txtPassword.UseSystemPasswordChar
    End Sub

    ''' <summary>
    ''' Allows for new users to register an account.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        ConnectionUtilities.register(txtForename.Text, txtSurname.Text, txtPassword.Text)
        Me.Hide()
        Accounts.Show()
    End Sub

    ''' <summary>
    ''' Takes us back to the Accounts window.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub backToAccounts(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkAccounts.LinkClicked
        Me.Hide()
        Accounts.Show()
    End Sub
End Class
