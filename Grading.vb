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
''' Basic utilties for grading.
''' </summary>
Namespace Grading

    ''' <summary>
    ''' Utilities for grade calculation.
    ''' </summary>
    ''' <remarks></remarks>
    Class CalculateGrade
        Public Shared Sub totalUp(ByVal award As Awards, ByVal result As Integer)
            Select Case award
                Case Awards.NINETY_CREDIT_DIPLOMA
                    Main.gradeTxt.Text = ninetyCreditDiploma(result).ToString
                Case Else
                    MessageBox.Show("An unexpected error has occured.")
            End Select
        End Sub

        ''' <summary>
        ''' A function to calculate the grade for a subsidiary diploma.
        ''' </summary>
        ''' <param name="result"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function subsidiaryDiploma(ByVal result As Integer) As SubsidiaryDipBoundaries
            If result > CredDipBoundaries.DsDs Then
                MessageBox.Show("You have exceeded the maximum calculation.")

            End If
            Select Case result
                Case Is <= SubsidiaryDipBoundaries.FAIL
                    Return SubsidiaryDipBoundaries.FAIL
                Case Is <= SubsidiaryDipBoundaries.P
                    Return SubsidiaryDipBoundaries.P
                Case Is <= SubsidiaryDipBoundaries.M
                    Return SubsidiaryDipBoundaries.M
                Case Is <= SubsidiaryDipBoundaries.D
                    Return SubsidiaryDipBoundaries.D
                Case Is <= SubsidiaryDipBoundaries.Ds
                    Return SubsidiaryDipBoundaries.Ds
                Case Else
                    Return SubsidiaryDipBoundaries.FAIL
            End Select
        End Function

        ''' <summary>
        ''' A function to calculate the grade for a ninenty credit diploma.
        ''' </summary>
        ''' <param name="result"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ninetyCreditDiploma(ByVal result As Integer) As CredDipBoundaries
            Select Case result
                Case Is <= CredDipBoundaries.FAIL
                    Return CredDipBoundaries.FAIL
                Case Is <= CredDipBoundaries.PP
                    Return CredDipBoundaries.PP
                Case Is <= CredDipBoundaries.MP
                    Return CredDipBoundaries.MP
                Case Is <= CredDipBoundaries.MM
                    Return CredDipBoundaries.MM
                Case Is <= CredDipBoundaries.DM
                    Return CredDipBoundaries.DM
                Case Is <= CredDipBoundaries.DD
                    Return CredDipBoundaries.DD
                Case Is <= CredDipBoundaries.DsD
                    Return CredDipBoundaries.DsD
                Case Is <= CredDipBoundaries.DsDs
                    Return CredDipBoundaries.DsD
                Case Else
                    Return CredDipBoundaries.FAIL
            End Select
        End Function
    End Class

    ''' <summary>
    ''' The boundaries for the subsidiary diploma.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SubsidiaryDipBoundaries
        FAIL = 15
        P = 16
        M = 32
        D = 48
        Ds = 56
    End Enum

    ''' <summary>
    ''' The boundaries for the 90-credit diploma.
    ''' </summary>
    ''' <remarks></remarks>
    Enum CredDipBoundaries
        FAIL = 23
        PP = 24
        MP = 36
        MM = 48
        DM = 60
        DD = 72
        DsD = 78
        DsDs = 84
    End Enum

    ''' <summary>
    ''' The boundaries for the diploma course.
    ''' </summary>
    ''' <remarks></remarks>
    Enum DipBoundaries
        PP = 32
        MP = 48
        MM = 64
        DM = 80
        DD = 96
        DsD = 104
        DsDs = 112
    End Enum

    ''' <summary>
    ''' The boundaries for the extended diploma course.
    ''' </summary>
    ''' <remarks></remarks>
    Enum ExtDipBoundaries
        PPP = 48
        MPP = 64
        MMP = 80
        MMM = 96
        DMM = 112
        DDM = 128
        DDD = 144
        DsDD = 152
        DsDsD = 160
        DsDsDs = 168
    End Enum
End Namespace
