Imports System.Data.OleDb

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
''' Connection utilities so that we can interact and connect to a database.
''' </summary>
''' <remarks></remarks>
Public Class ConnectionUtilities

    'The current username logged in.
    Public Shared username As String = ""

    'The provider of our database.
    Const DATA_PROVIDER As String = "Provider=Microsoft.ACE.OLEDB.12.0;"

    'The source of our data.
    Const DATA_SOURCE As String = "Data Source = N:\BTEC Year 2\[17] Project Planning with IT\4_Assignment\Grade Tracker UCAS\UCAS Data.accdb"

    'Represents a connection.
    Shared connection As New OleDbConnection

    'Our adapter for our data.
    Shared adapter As OleDbDataAdapter

    'Holds temporary data to import onto our database.
    Shared dataSet As New DataSet

    'Querys to test and execute on our database.
    Shared query As String

    ''' <summary>
    ''' Initiates a conenction for us.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub initiateConnection()
        If connection.State = ConnectionState.Open Then
            Return
        End If

        connection.ConnectionString = DATA_PROVIDER & DATA_SOURCE
        connection.Open()
    End Sub

    ''' <summary>
    ''' Loads all the courses on the main form.
    ''' </summary>
    ''' <param name="c">The combo box to load data into.</param>
    ''' <remarks></remarks>
    Public Shared Sub loadCourses(ByRef c As ComboBox)
        initiateConnection()

        Dim command As New OleDbCommand
        command.Connection = connection
        command.CommandText = "Select CourseName from tblCourses"
        Dim dataReader As OleDbDataReader = command.ExecuteReader
        While dataReader.Read
            c.Items.Add(dataReader.Item(0))
        End While
        dataReader.Close()
        disconnect()
    End Sub

    ''' <summary>
    ''' Saves the username to the database with the course they have selected.
    ''' </summary>
    ''' <param name="testedUsername"></param>
    ''' <param name="course"></param>
    ''' <remarks></remarks>
    Public Shared Sub save(ByVal testedUsername As String, ByVal course As String)
        initiateConnection()

        Dim commandBuilder As New OleDbCommandBuilder(adapter)

        While Not testedUsername = username
            For i As Integer = 0 To dataSet.Tables("tblStudents").Rows.Count - 1
                dataSet.Tables("tblStudents").Rows(i).Item("Course") = course
                adapter.Update(dataSet, "tblStudents")
            Next i
        End While

        MessageBox.Show("You have saved your course, please proceed.")
    End Sub

    ''' <summary>
    ''' Checking if a member exists.
    ''' </summary>
    ''' <param name="forename">The first name to check if it exists.</param>
    ''' <param name="surname">The surname to check if it exists.</param>
    ''' <returns>true if it does, false if not.</returns>
    ''' <remarks></remarks>
    Private Shared Function memberExists(ByRef forename As String, ByRef surname As String) As Boolean
        initiateConnection()
        query = "Select * From tblMembers WHERE Forename = '" & forename & "' AND Surname = '" & surname & "'"
        adapter = New OleDbDataAdapter(query, connection)
        adapter.Fill(dataSet, "tblMembers")
        If dataSet.Tables("tblMembers").Rows.Count > 0 Then
            Return True
        End If

        Return False
        disconnect()
    End Function

    ''' <summary>
    ''' Logs our user in by checking the database (this is not for administrative use.)
    ''' </summary>
    ''' <param name="testedForename">The forename to be interrogated.</param>
    ''' <param name="testedSurname">The surname to be interrogated.</param>
    ''' <param name="testedPassword">The password to be interrogated.</param>
    ''' <remarks></remarks>
    Public Shared Sub login(ByRef testedForename As String, ByRef testedSurname As String, ByRef testedPassword As String)
        Dim count As Integer = 0
        If Not memberExists(testedForename, testedSurname) Then
            MessageBox.Show("Please register an account.")
        Else
            initiateConnection()

            Dim table As String = "tblMembers"
            query = "Select * From " & table
            adapter = New OleDbDataAdapter(query, connection)
            adapter.Fill(dataSet, table)
            For index = 0 To dataSet.Tables(table).Rows.Count - 1
                If testedForename = dataSet.Tables(table).Rows(index).Item("Forename") Then
                    If testedSurname = dataSet.Tables(table).Rows(index).Item("Surname") Then
                        If testedPassword = dataSet.Tables(table).Rows(index).Item("Pswd") Then
                            If count = 1 Then
                                Return
                            End If
                            MessageBox.Show("Successful login, thank you " & testedForename & " " & testedSurname & "!")
                            username = testedForename & " " & testedSurname
                            count += 1
                        Else
                            MessageBox.Show("Incorrect password.")
                        End If
                    End If
                End If
            Next
        End If
        disconnect()
    End Sub

    ''' <summary>
    ''' Logs our user in by checking the database (this is for administrative use.)
    ''' </summary>
    ''' <param name="testedForename">The forename to be interrogated.</param>
    ''' <param name="testedSurname">The surname to be interrogated.</param>
    ''' <param name="testedPassword">The password to be interrogated.</param>
    ''' <remarks></remarks>
    Public Shared Sub login(ByRef testedForename As String, ByRef testedSurname As String, ByRef testedPassword As String, ByRef admin As Boolean)
        Dim count As Integer = 0
        If Not memberExists(testedForename, testedSurname) Then
            MessageBox.Show("Please register an account.")
        Else
            initiateConnection()

            Dim table As String = "tblMembers"
            query = "Select * From " & table
            adapter = New OleDbDataAdapter(query, connection)
            adapter.Fill(dataSet, table)
            For index = 0 To dataSet.Tables(table).Rows.Count - 1
                If testedForename = dataSet.Tables(table).Rows(index).Item("Forename") Then
                    If testedSurname = dataSet.Tables(table).Rows(index).Item("Surname") Then
                        If testedPassword = dataSet.Tables(table).Rows(index).Item("Pswd") Then
                            If admin = dataSet.Tables(table).Rows(index).Item("Administrator") Then
                                If count = 1 Then
                                    Return
                                End If
                                MessageBox.Show("Successful login, thank you " & testedForename & " " & testedSurname & "!")
                                username = testedForename & " " & testedSurname
                                count += 1
                            Else
                                MessageBox.Show("You do not have the correct permissions to do this.")
                                AccountsAdmin.Dispose()
                                Accounts.Show()
                            End If
                        Else
                            MessageBox.Show("Incorrect password.")
                        End If
                    End If
                End If
            Next
        End If
        disconnect()
    End Sub

    ''' <summary>
    ''' Registers our user into the database.
    ''' </summary>
    ''' <param name="forename">The forename to register.</param>
    ''' <param name="surname">The surname to register.</param>
    ''' <param name="password">The password to register.</param>
    ''' <remarks></remarks>
    Public Shared Sub register(ByRef forename As String, ByRef surname As String, ByRef password As String)
        initiateConnection()

        Dim table As String = "tblMembers"

        query = "Select * From " & table
        adapter = New OleDbDataAdapter(query, connection)
        adapter.Fill(dataSet, table)

        Dim commandBuilder As New OleDbCommandBuilder(adapter)
        Dim newRow As DataRow = dataSet.Tables(table).NewRow()
        newRow.Item("Forename") = forename
        newRow.Item("Surname") = surname
        newRow.Item("Pswd") = password

        dataSet.Tables(table).Rows.Add(newRow)
        adapter.Update(dataSet, "tblMembers")
        MessageBox.Show("Successfully registered your account.")

        disconnect()
    End Sub

    ''' <summary>
    ''' Closes and disposes of our connection.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub disconnect()
        connection.Close()
        connection.Dispose()
    End Sub
End Class
