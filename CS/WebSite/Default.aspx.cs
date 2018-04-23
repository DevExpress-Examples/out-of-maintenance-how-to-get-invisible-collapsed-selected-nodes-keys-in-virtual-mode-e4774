using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using DevExpress.Web.ASPxTreeList;

public partial class Default : System.Web.UI.Page {

    protected void Page_Load() {
        if(!IsPostBack) {
            treeList.ExpandAll();

            treeList.FindNodeByKeyValue("0").Selected = true;
            treeList.FindNodeByKeyValue("7").Selected = true;
            treeList.FindNodeByKeyValue("8").Selected = true;
            treeList.FindNodeByKeyValue("10").Selected = true;

            treeList.FindNodeByKeyValue("1").Expanded = false;

            HF["visibleSelectedKeys"] = new string[] { "0", "7" };
            HF["invisibleSelectedKeys"] = new string[] { "8", "10" };
        }
    }
    protected void treeList_VirtualModeCreateChildren(object sender, TreeListVirtualModeCreateChildrenEventArgs e) {
        var path = e.NodeObject == null ? Page.MapPath("~/") : e.NodeObject.ToString();
        var children = new List<string>();
        if(Directory.Exists(path)) {
            children.AddRange(Directory.GetDirectories(path).Where(d => !IsSystemName(d)).OrderBy(n => n));
            children.AddRange(Directory.GetFiles(path).Where(d => !IsSystemName(d)).OrderBy(n => n));
        }
        e.Children = children;
    }
    protected void treeList_VirtualModeNodeCreating(object sender, TreeListVirtualModeNodeCreatingEventArgs e) {
        string nodePath = e.NodeObject.ToString();
        var id = GetNodeID(nodePath);
        e.NodeKeyValue = id;
        e.IsLeaf = !Directory.Exists(nodePath);
        e.SetNodeValue("id", id);
        e.SetNodeValue("name", Path.GetFileName(nodePath));
        e.SetNodeValue("date", Directory.GetCreationTime(nodePath));
    }

    int GetNodeID(string path) {
        if(!Map.ContainsKey(path))
            Map[path] = Map.Count;
        return Map[path];
    }
    Dictionary<string, int> Map {
        get {
            const string key = "DX_PATH_ID_MAP";
            if(Session[key] == null)
                Session[key] = new Dictionary<string, int>();
            return (Dictionary<string, int>)Session[key];
        }
    }
    bool IsSystemName(string name) {
        name = Path.GetFileName(name).ToLower();
        return name.StartsWith("app_") || name == "bin"
            || name.EndsWith(".aspx.cs") || name.EndsWith(".aspx.vb");
    }
}
