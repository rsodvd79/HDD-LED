<Serializable>
<System.Xml.Serialization.XmlRoot("Config")>
Public Class ClassConfig
    Public Luminosita As Integer
    Public Leggi As Integer
    Public Scrivi As Integer

    Public Sub New()
        RestoreDefaultValue()

    End Sub

    Public Sub RestoreDefaultValue()
        Luminosita = 32
        Leggi = 10
        Scrivi = 100

    End Sub

    Public Shared Function FileXML() As String
        Return IO.Path.Combine(My.Application.Info.DirectoryPath, "Config.xml")
    End Function

    Public Function Clone() As ClassConfig
        Return DirectCast(MemberwiseClone(), ClassConfig)
    End Function

    Public Function Salva() As Boolean

        Try
            Dim file As String = FileXML()

            If IO.File.Exists(file) Then
                IO.File.Delete(file)
            End If

            Dim XmlWrt As Xml.XmlWriter = Xml.XmlWriter.Create(file, New Xml.XmlWriterSettings() With {.Indent = True})
            Dim Srlzr As New Xml.Serialization.XmlSerializer(GetType(ClassConfig))
            Srlzr.Serialize(XmlWrt, Me)
            XmlWrt.Flush()
            XmlWrt.Close()

            Return True

        Catch ex As Exception
            Throw

        End Try

        Return False
    End Function

    Public Shared Function Carica() As ClassConfig
        Dim rtn As ClassConfig = Nothing
        Dim file As String = FileXML()

        Try
            If IO.File.Exists(file) Then
                Dim XmlRdr As Xml.XmlReader = Xml.XmlReader.Create(New IO.StringReader(IO.File.ReadAllText(file)))

                Dim Srlzr As New Xml.Serialization.XmlSerializer(GetType(ClassConfig))
                ' AddHandler Srlzr.UnknownNode, AddressOf Srlzr_UnknownNode
                ' AddHandler Srlzr.UnknownAttribute, AddressOf Srlzr_UnknownAttribute

                If Srlzr.CanDeserialize(XmlRdr) Then
                    rtn = DirectCast(Srlzr.Deserialize(XmlRdr), ClassConfig)

                End If

                XmlRdr.Close()

            Else
                rtn = New ClassConfig()
                rtn.Salva()

            End If

        Catch ex As Exception
            Throw

        End Try

        Return rtn
    End Function

End Class
