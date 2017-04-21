<%@ Page Title="" Language="C#" MasterPageFile="~/Definitions/Definitions.Master" AutoEventWireup="true" CodeBehind="Authenticate.aspx.cs" Inherits="JSONFirebirdWebServiceTest.Definitions.Authenticate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ResourceName" runat="server">
    <h1>Authenticate Resource</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DescriptionOfResource" runat="server">
    <h3>The Authenticate Resource should be used to log in users into the Angular2CRM Website. It provides the check to confirm if a username and password have been correctly supplied.</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ResourceCalls" runat="server">
    <ul>
        <li><strong>Route Path:</strong> {host}/auth/{username}/{password}</li>
        <li><strong>HTTP Verb:</strong> GET</li>
        <li><strong>Description:</strong> In the DATA element, the SUCCESS field returns as TRUE with a good username and password combination (entered as {username} and {password} in the API call). Note that this will need to be reviewed to provide proper security / encryption. </li>
    </ul>
</asp:Content>
