﻿Imports System.Data
Imports System.Data.SqlClient

Public Class FormPending

    Private Sub FormPending_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connection = New SqlConnection(conStr)
        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If
        For i = 1 To 12
            ComboMonth.Items.Add(MonthName(i))
        Next
        ComboMonth.SelectedIndex = _
        ComboMonth.FindStringExact(MonthName(DateTime.Now.Month))
        Dim y As Integer = DateTime.Now.Year
        ComboYear.Items.Add(y + 543)
        ComboYear.Items.Add((y - 1) + 543)
        ComboYear.SelectedIndex = 0
    End Sub

    Private Sub ButtonShowData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonShowData.Click
        sql = "SELECT RoomNo, (Rental + WaterSupply + Electricity + " & _
                "Telephone + Others) AS Total " & _
                "FROM Payment " & _
                "WHERE (Month = @m AND Year = @y AND Paid = 0)"
        command = New SqlCommand(sql, connection)
        command.Parameters.AddWithValue("m", ComboMonth.SelectedItem)
        command.Parameters.AddWithValue("y", ComboYear.SelectedItem)
        adapter = New SqlDataAdapter(command)
        dataSt = New DataSet()
        adapter.Fill(dataSt, "pending")
        DataGridView1.DisplayedRowCount(True)
        DataGridView1.DataSource = dataSt.Tables("pending")
        DataGridView1.Columns(0).HeaderText = "ห้อง"
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).HeaderText = "ยอดที่ต้องชำระ"
        DataGridView1.Columns(1).Width = 150
    End Sub
End Class