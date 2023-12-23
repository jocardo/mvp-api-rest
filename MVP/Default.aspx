<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MVP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-3">
            <br /> <br />
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label><br />
            <asp:TextBox ID="tbDescricao" runat="server" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br /> <br />
            <asp:Label ID="lblDataEntrega" runat="server" Text="Data sugerida para entrega"></asp:Label>&nbsp;
            <asp:Calendar ID="cldData" runat="server" OnSelectionChanged="cldData_SelectionChanged"></asp:Calendar>
            <asp:Label ID="lblAlertData" runat="server" visible="false" ></asp:Label>&nbsp;
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnLimpar" runat="server" Visible="false" Text="Limpar Lista" OnClick="btnLimpar_Click" />
            
            <br /><br />

            <asp:GridView ID="grvDados" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                    <asp:BoundField HeaderText="Data" DataField="Data" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
