''' <license> 
''' Copyright (C) 2017  Dominick Jones
''' 
''' This program is free software: you can redistribute it and/or modify
''' it under the terms of the GNU General Public License as published by 
''' the Free Software Foundation, either version 3 of the License, or
''' any later version.
''' 
''' This program is distributed in the hope that it will be useful,
''' but WITHOUT ANY WARRANTY; without even the implied warranty of
''' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
''' GNU General Public License for more details.
''' 
''' You should have received a copy of the GNU General Public License 
''' along with this program. If not, see <a>http://www.gnu.org/licenses/"</a>.
''' </license>
''' 
''' <summary>
''' Standard accounts logging in.
''' </summary>
Public Class AccountsLogin

    ''' <summary>
    ''' What to do once the form has loaded.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub initiateForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub

    ''' <summary>
    ''' Takes you back to the main accounts screen.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub backToAccounts(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkAccounts.LinkClicked
        Accounts.Show()
        Me.Hide()
    End Sub

    ''' <summary>
    ''' Logs you in to the system.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub login(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        ConnectionUtilities.login(txtForename.Text, txtSurname.Text, txtPassword.Text)
        Me.Hide()
        Main.Show()
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
End Class
