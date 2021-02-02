Option Infer On

Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web.UI
Imports DevExpress.Web.ASPxTreeList

Partial Public Class [Default]
	Inherits System.Web.UI.Page

	Protected Sub Page_Load()
		If Not IsPostBack Then
			treeList.ExpandAll()

			treeList.FindNodeByKeyValue("0").Selected = True
			treeList.FindNodeByKeyValue("7").Selected = True
			treeList.FindNodeByKeyValue("8").Selected = True
			treeList.FindNodeByKeyValue("10").Selected = True

			treeList.FindNodeByKeyValue("1").Expanded = False

			HF("visibleSelectedKeys") = New String() { "0", "7" }
			HF("invisibleSelectedKeys") = New String() { "8", "10" }
		End If
	End Sub
	Protected Sub treeList_VirtualModeCreateChildren(ByVal sender As Object, ByVal e As TreeListVirtualModeCreateChildrenEventArgs)
		Dim path As String = If(e.NodeObject Is Nothing, Page.MapPath("~/"), e.NodeObject.ToString())
		Dim children = New List(Of String)()
		If Directory.Exists(path) Then
			children.AddRange(Directory.GetDirectories(path).Where(Function(d) Not IsSystemName(d)).OrderBy(Function(n) n))
			children.AddRange(Directory.GetFiles(path).Where(Function(d) Not IsSystemName(d)).OrderBy(Function(n) n))
		End If
		e.Children = children
	End Sub
	Protected Sub treeList_VirtualModeNodeCreating(ByVal sender As Object, ByVal e As TreeListVirtualModeNodeCreatingEventArgs)
		Dim nodePath As String = e.NodeObject.ToString()
		Dim id = GetNodeID(nodePath)
		e.NodeKeyValue = id
		e.IsLeaf = Not Directory.Exists(nodePath)
		e.SetNodeValue("id", id)
		e.SetNodeValue("name", Path.GetFileName(nodePath))
		e.SetNodeValue("date", Directory.GetCreationTime(nodePath))
	End Sub

	Private Function GetNodeID(ByVal path As String) As Integer
		If Not Map.ContainsKey(path) Then
			Map(path) = Map.Count
		End If
		Return Map(path)
	End Function
	Private ReadOnly Property Map() As Dictionary(Of String, Integer)
		Get
			Const key As String = "DX_PATH_ID_MAP"
			If Session(key) Is Nothing Then
				Session(key) = New Dictionary(Of String, Integer)()
			End If
			Return CType(Session(key), Dictionary(Of String, Integer))
		End Get
	End Property
	Private Function IsSystemName(ByVal name As String) As Boolean
		name = Path.GetFileName(name).ToLower()
		Return name.StartsWith("app_") OrElse name = "bin" OrElse name.EndsWith(".aspx.cs") OrElse name.EndsWith(".aspx.vb")
	End Function
End Class
