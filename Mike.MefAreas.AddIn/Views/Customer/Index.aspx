<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Mike.MefAreas.AddIn.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Customers</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Customers</h1>
    <table>
        <tr>
            <th>Name</th>
            <th>Age</th>
        </tr>
    <% foreach(var customer in ViewData.Model as IEnumerable<CustomerView>){ %>
        <tr>
            <td><%= customer.Name %></td>
            <td><%= customer.Age %></td>
        </tr>
    <%} %>
    </table>
</asp:Content>
