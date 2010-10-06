<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mike.MefAreas.Core.Services.INavigation[]>" %>

<ul id="menu">
<% foreach(var navigation in Model) { %>
    <li><%= Html.ActionLink(navigation.Text, navigation.Action, navigation.Controller) %></li>
<%} %>
</ul>

