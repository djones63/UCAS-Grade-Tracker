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
Public Class Main

    'The default stage of this calculator.
    Private calculatorStep As CalculatorStep = calculatorStep.SAVE_COURSE

    'The award type we are using.
    Private awardType As Awards

    'The amount of points awarded per pass.
    Dim pointPerPass As Byte = 7

    'The amount of points awarded per merit.
    Dim pointPerMerit As Byte = 8

    'The amount of points awarded per distinction.
    Dim pointPerDistinction As Byte = 9

    'The multiplier for the final result.
    Dim tariffMultiplier As Byte = 10

    ''' <summary>
    ''' What to do on the form load up.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub intiiateForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConnectionUtilities.loadCourses(courseList)
        courseList.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    ''' <summary>
    ''' Allows us to save our course relative to the username in our database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub saveCourse(sender As System.Object, e As System.EventArgs) Handles btnSaveCourse.Click
        If calculatorStep = calculatorStep.SAVE_COURSE Then

            If courseList.SelectedIndex = -1 Then
                MessageBox.Show("You must change the course you have selected.")
                Return
            End If

            ConnectionUtilities.save(ConnectionUtilities.username, courseList.SelectedText)
            courseList.Enabled = False
            calculatorStep = calculatorStep.POINTS_ENTERED_P
        Else
            MessageBox.Show("It seems you have already completed this stage.")
        End If
    End Sub

    ''' <summary>
    ''' A timer to ensure stuff is being opened when needed.
    ''' </summary>
    ''' <param name="sender">The </param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub processTick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles process.Tick
        If txtPasses.TextLength > 0 Then
            calculatorStep = WindowsApplication1.CalculatorStep.POINTS_ENTERED_M
        End If

        If txtMerits.TextLength > 0 Then
            calculatorStep = WindowsApplication1.CalculatorStep.POINTS_ENTERED_D
        End If

        If txtDistinctions.TextLength > 0 Then
            calculatorStep = WindowsApplication1.CalculatorStep.POINTS_ENTERED_NP
        End If

        If txtNotPassed.TextLength > 0 Then
            calculatorStep = WindowsApplication1.CalculatorStep.CALCULATED
        End If

        Select Case calculatorStep
            Case WindowsApplication1.CalculatorStep.POINTS_ENTERED_P
                txtPasses.Enabled = True
            Case WindowsApplication1.CalculatorStep.POINTS_ENTERED_M
                txtMerits.Enabled = True
            Case WindowsApplication1.CalculatorStep.POINTS_ENTERED_D
                txtDistinctions.Enabled = True
            Case WindowsApplication1.CalculatorStep.POINTS_ENTERED_NP
                txtNotPassed.Enabled = True
            Case WindowsApplication1.CalculatorStep.CALCULATED
                btnCalculate.Enabled = True
        End Select
    End Sub

    ''' <summary>
    ''' Resets all variables for a new calculation.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub reset(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        calculatorStep = calculatorStep.SAVE_COURSE
        txtNotPassed.Enabled = False
        txtNotPassed.Text = ""
        txtDistinctions.Enabled = False
        txtDistinctions.Text = ""
        txtMerits.Enabled = False
        txtMerits.Text = ""
        txtPasses.Enabled = False
        txtPasses.Text = ""
        courseList.Enabled = True
        blankNP.Visible = False
        not_passeda.Visible = False
        blank1.Text = ""
        blank2.Text = ""
        blank3.Text = ""
        blank4.Text = ""
        blank5.Text = ""
        blank6.Text = ""
        blank7.Text = ""
        blank8.Text = ""
        blank9.Text = ""
        blank10.Text = ""
        gradeTxt.Text = ""
    End Sub

    ''' <summary>
    ''' Allows us to calculate the points.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub calculate(sender As System.Object, e As System.EventArgs) Handles btnCalculate.Click
        Dim amtOfNotPassed As Integer = IIf(txtNotPassed.TextLength < 1, 0, CInt(txtNotPassed.Text))

        If amtOfNotPassed > 0 Then
            blankNP.Visible = True
            not_passeda.Visible = True
            gradeTxt.Text = "FAIL"
            Return
        End If

        Dim amtOfPasses As Integer = CInt(txtPasses.Text)
        Dim amtOfMerits As Integer = CInt(txtMerits.Text)
        Dim amtOfDistinctions As Integer = CInt(txtDistinctions.Text)

        blank1.Text = amtOfPasses
        blank2.Text = pointPerPass
        blank3.Text = amtOfPasses * pointPerPass

        blank4.Text = amtOfMerits
        blank5.Text = pointPerMerit
        blank6.Text = amtOfMerits * pointPerMerit

        blank7.Text = amtOfDistinctions
        blank8.Text = pointPerDistinction
        blank9.Text = amtOfDistinctions * pointPerDistinction

        blank10.Text = (amtOfPasses * pointPerPass) + (amtOfMerits * pointPerMerit) + (amtOfDistinctions * pointPerDistinction)
        Grading.CalculateGrade.totalUp(awardType, CInt(blank10.Text))
    End Sub

    ''' <summary>
    ''' Allows us to logout.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub logoutMethod(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles logout.LinkClicked
        Accounts.Show()
        Me.Dispose()
    End Sub

    ''' <summary>
    ''' Detect when we have changed the course listing and adapt it to our <code>awardType</code>.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub courseChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles courseList.SelectedIndexChanged
        Select Case courseList.SelectedIndex
            Case 0
                awardType = Awards.EXTENDED_DIPLOMA
            Case 1
                awardType = Awards.DIPLOMA
            Case 2
                awardType = Awards.NINETY_CREDIT_DIPLOMA
            Case 3
                awardType = Awards.SUBSIDIARY_DIPLOMA
            Case Else
                MessageBox.Show("#001 Unsupported operation exception.")
        End Select
    End Sub
End Class
