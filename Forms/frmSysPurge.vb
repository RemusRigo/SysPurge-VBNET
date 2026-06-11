Imports System.Configuration
Imports System.Text.RegularExpressions

Public Class frmSysPurge

   Dim grp As ListViewGroup = Nothing

   Public Sub ResizeColumns()
      Dim w As Integer

      ' Setting column width to -1 in WinForms ListView auto-sizes the column
      lvSysPurge.Columns(0).Width = -1
      lvSysPurge.Columns(1).Width = -1

      ' Retrieve column widths via P/Invoke
      w = SendMessage(lvSysPurge.Handle, LVM_GETCOLUMNWIDTH, New IntPtr(0), IntPtr.Zero).ToInt32()
      w += SendMessage(lvSysPurge.Handle, LVM_GETCOLUMNWIDTH, New IntPtr(1), IntPtr.Zero).ToInt32()

      ' Calculate and set the width of the third column (index 2)
      lvSysPurge.Columns(2).Width = lvSysPurge.ClientSize.Width - w - GetSystemMetrics(SM_CXVSCROLL)
   End Sub

   Private Sub LV_AddGroup(name As String)
      grp = New ListViewGroup(name)
      lvSysPurge.Groups.Add(grp)
   End Sub

   Private Sub LV_AddItem(name As String)
      Dim item As New ListViewItem(name)
      item.SubItems.Add("")
      item.Group = grp
      lvSysPurge.Items.Add(item)
   End Sub

   Public Sub BuildOptions()
      LV_AddGroup("Microsoft Windows FileSystem")
      If IsAppElevated() Then LV_AddItem("EventViewer logs")
      LV_AddItem("Log files (inside Windows")
      LV_AddItem("Log files (System drive)")
      LV_AddItem("Prefetch files")
      LV_AddItem("Temp files (Current User)")
      LV_AddItem("Temp files (Windows)")
      If IsAppElevated then LV_AddItem("Windows Update cache")

      LV_AddGroup("Microsoft Windows Registry")
      LV_AddItem("MRU list: Run")
      If IsAppElevated() Then LV_AddItem("Shared DLL's)")

      ResizeColumns()
   End Sub

   Public Sub ProcessActions()
      '
   End Sub


   Private Sub frmSysPurge_Load(sender As Object, e As EventArgs) Handles Me.Load
      BuildOptions()
   End Sub

   Private Sub btnTSPurge_Click(sender As Object, e As EventArgs) Handles btnTSPurge.Click
      ProcessActions()
   End Sub
End Class
