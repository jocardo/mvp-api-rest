<%@ Page Title="Sobre" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MVP.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Descrição do aplicativo.</h3>
    <p>Nesta primeira versão é armazenado minimamente uma breve descrição da
    necessidade e data sugerida para entrega, no caso da data sugerida para entrega é interessante
    validar se é um feriado com o uso da API do site invertexto.com.</p>
       
</asp:Content>
