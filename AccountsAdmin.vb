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
Public Class AccountsAdmin

    ''' <summary>
    ''' Logs us into the administrator control panel.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub login(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click
        ConnectionUtilities.login(txtForename.Text, txtSurname.Text, txtPassword.Text, True)
    End Sub

    ''' <summary>
    ''' Toggles password visibility.
    ''' </summary>
    ''' <param name="sender">the object to send.</param>
    ''' <param name="e">the event as a result of the stimulus.</param>
    ''' <remarks></remarks>
    Private Sub togglePasswordVisibility(sender As System.Object, e As System.EventArgs) Handles cbPassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = Not txtPassword.UseSystemPasswordChar
    End Sub
End Class
