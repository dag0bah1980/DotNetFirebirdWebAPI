<%@ Page Title="" Language="C#" MasterPageFile="~/Definitions/Definitions.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="JSONFirebirdWebServiceTest.Definitions.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ResourceName" runat="server">
    <h1>Users Resource</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DescriptionOfResource" runat="server">
    <h3>The Users Resource allows you to get information regarding the users of the software. This is primarily staff who use Angular2CRM.</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ResourceCalls" runat="server">
    <ul>
        <li><strong>Route Path:</strong> {host}/api/USERS</li>
        <li><strong>HTTP Verb:</strong> GET</li>
        <li><strong>Description:</strong> Returns all columns and all records from the USERS table. There are no filters with this command.</li>
    </ul>
    <ul>
        <li><strong>Route Path:</strong> {host}/api/USERS/{requestedid}</li>
        <li><strong>HTTP Verb:</strong> GET</li>
        <li><strong>Description:</strong> Returns all columns for the record with a matching id as {requestedid}</li>
    </ul>
    <ul>
        <li><strong>Route Path:</strong> </li>
        <li><strong>HTTP Verb:</strong> </li>
        <li><strong>Description:</strong> </li>
    </ul>
    <ul>
        <li><strong>Route Path:</strong> </li>
        <li><strong>HTTP Verb:</strong> </li>
        <li><strong>Description:</strong> </li>
    </ul>
</asp:Content>
