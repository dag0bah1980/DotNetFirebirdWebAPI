<%@ Page Title="" Language="C#" MasterPageFile="~/Definitions/Definitions.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="JSONFirebirdWebServiceTest.Definitions.Index" %>
<%@ MasterType VirtualPath="~/Definitions/Definitions.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ResourceName" runat="server">
    <h1>Index - Listing of all Resources</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DescriptionOfResource" runat="server">
    <h3>This page lists all resources currently available for this WEB API and defines them so that users understand what they are accessing.</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ResourceCalls" runat="server">
    <ul>
        <li><a href="./Users.aspx">Users</a></li>
        <li><a href="./Authenticate.aspx">Authenticate</a></li>
    </ul>
</asp:Content>
