<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128548670/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4774)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs)
* [Script.js](./CS/WebSite/Script.js) (VB: [Script.js](./VB/WebSite/Script.js))
<!-- default file list end -->
# How to get invisible / collapsed selected nodes' keys in virtual mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e4774/)**
<!-- run online end -->


<p>This example illustrates how to manually get invisible / collapsed selected nodes' keys when ASPxTreeList operates in  virtual mode.</p><p>It is then possible to get these keys on the server side for further processing.</p><br />
<p>Selected nodes have two states: visible and invisible (if a parent node is collapsed).</p><p>These states are saved as string arrays within the ASPxHiddenField.</p><p>Then, these arrays are synchronized on the client side in the EndCallback event.<br />
</p>


<h3>Description</h3>

<p><br />
</p>

<br/>


