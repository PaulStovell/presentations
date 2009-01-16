Option Strict On
Option Explicit On

Imports System.Runtime.CompilerServices

Module Program

    Sub Main()
        Dim s As String = "Hello {0}, today is {1}" _
                          .FormatWith("Paul", DateTime.Today.ToLongDateString())

        Console.WriteLine(s)
        Console.ReadKey()


        Dim v As XElement = <Company> _
                                 <Contact> _
                                     <Name>Paul</Name> _
                                 </Contact> _
                             </Company>


    End Sub

End Module

Public Module MyExtensions

    <Extension()> _
    Public Function FormatWith(ByVal format As String, ByVal ParamArray args As String()) As String
        Return String.Format(format, args)
    End Function

End Module