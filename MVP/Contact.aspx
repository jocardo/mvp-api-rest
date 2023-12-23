<%@ Page Title="Contato" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MVP.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Página de contato.</h3>
    <address>
        Rocha Miranda - Rio de Janeiro, RJ<br />
        CEP 21510-101<br />
        <abbr title="Celular">P:</abbr>
        +55 (21) 99441-9642
    </address>

    <address>
        <strong>E-mail:</strong>   <a href="mailto:josericarmoreira96@gmail.com">josericarmoreira96@gmail.com</a><br />
        <strong>Linkedin:</strong> <a href="https://www.linkedin.com/in/jocardo96" target="_blank">https://www.linkedin.com/in/jocardo96</a><br />
        <strong>GitHub:</strong>   <a href="https://github.com/jocardo" target="_blank">https://github.com/jocardo</a><br />
        <strong>Dio:</strong>   <a href="https://www.dio.me/users/josericardomoreira96" target="_blank">https://www.dio.me/users/josericardomoreira96</a>
    </address>
</asp:Content>
