<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.1" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Script.js" type="text/javascript"></script>
    <script type="text/javascript">
        function treeList_Init(s, e) {
            WriteInfo();
        }
        function treeList_SelectionChanged(s, e) {
            hf.Set("visibleSelectedKeys", s.GetVisibleSelectedNodeKeys());
            WriteInfo();
        }
        function treeList_EndCallback(s, e) {
            SyncTreeListSelectionAfterCallback(s);
            WriteInfo();
        }
        function SyncTreeListSelectionAfterCallback(tree) {
            var visible = hf.Get("visibleSelectedKeys");
            var invisible = hf.Get("invisibleSelectedKeys");
            var selection = GetSelectionAfterCallback(tree.GetVisibleSelectedNodeKeys(), visible, invisible);
            if (selection) {
                hf.Set("visibleSelectedKeys", selection.visible);
                hf.Set("invisibleSelectedKeys", selection.invisible);
            }
        }
        function GetSelectionAfterCallback(currentVisible, oldVisible, invisible) {
            if (currentVisible.length === oldVisible.length)
                return;
            var hide = oldVisible.length > currentVisible.length;

            var exceptTarget = currentVisible;
            var exceptSource = oldVisible;
            if (hide) {
                exceptTarget = oldVisible;
                exceptSource = currentVisible;
            }
            var newValues = ArrayHelper.Except(exceptTarget, exceptSource);

            var updateFunc = hide ? ArrayHelper.Union : ArrayHelper.Except;
            invisible = updateFunc(invisible, newValues);

            return { visible: currentVisible, invisible: invisible };
        }
        function WriteInfo() {
            var visible = hf.Get("visibleSelectedKeys");
            var invisible = hf.Get("invisibleSelectedKeys");
            VisibleKeysLabel.SetText(visible.join(", "));
            InvisibleKeysLabel.SetText(invisible.join(", "));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxHiddenField ID="HF" runat="server" ClientInstanceName="hf" />
        <dx:ASPxTreeList ID="treeList" runat="server" Width="500"
            OnVirtualModeCreateChildren="treeList_VirtualModeCreateChildren"
            OnVirtualModeNodeCreating="treeList_VirtualModeNodeCreating">
            <Columns>
                <dx:TreeListDataColumn FieldName="id" Caption="Key" />
                <dx:TreeListDataColumn FieldName="name" Caption="File name" />
                <dx:TreeListDataColumn FieldName="date" Caption="Creation Date" DisplayFormat="{0:g}" />
            </Columns>
            <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
            <SettingsSelection Enabled="true" />
            <ClientSideEvents Init="treeList_Init" SelectionChanged="treeList_SelectionChanged" EndCallback="treeList_EndCallback" />
        </dx:ASPxTreeList>
        <br />
        <dx:ASPxLabel runat="server" Text="Visible Keys: " Font-Size="Large" />
        <dx:ASPxLabel ID="VisibleKeysLabel" runat="server" ClientInstanceName="VisibleKeysLabel" Font-Size="X-Large" />
        <br />
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Invisible Keys: " Font-Size="Large" />
        <dx:ASPxLabel ID="InvisibleKeysLabel" runat="server" ClientInstanceName="InvisibleKeysLabel" Font-Size="X-Large" />
    </form>
</body>
</html>