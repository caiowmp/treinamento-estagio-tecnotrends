<%@ Page Title="Gerenciamento de Autores" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GerenciamentoAutores.aspx.cs"
    Inherits="Library_Management.Pages.GerenciamentoAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left">
        <h2>Cadastro de novo autor</h2>
        <table>
            <tr class="d-grid">
                <td>
                    <asp:Label
                        ID="lblCadastroNomeAutor"
                        runat="server"
                        Font-Size="16pt"
                        Text="Nome: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox
                        ID="tbxCadastroNomeAutor"
                        runat="server"
                        CssClass="form-control"
                        Font-Size="16pt"
                        Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvCadastroNomeAutor"
                        runat="server"
                        ControlToValidate="tbxCadastroNomeAutor"
                        ErrorMessage="O nome do autor é obrigatório"
                        Font-Size="16pt"
                        ForeColor="Red"
                        ValidationGroup="CadastroAutor"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label
                        ID="lblCadastroSobrenomeAutor"
                        runat="server"
                        Font-Size="16pt"
                        Text="Sobrenome: ">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox
                        ID="tbxCadastroSobrenomeAutor"
                        runat="server"
                        CssClass="form-control"
                        Font-Size="16pt"
                        Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvCadastroSobrenomeAutor"
                        runat="server"
                        ControlToValidate="tbxCadastroSobrenomeAutor"
                        ErrorMessage="O sobrenome do autor é obrigatório"
                        Font-Size="16pt"
                        ForeColor="Red"
                        ValidationGroup="CadastroAutor"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label
                        ID="lblCadastroEmailAutor"
                        runat="server"
                        Font-Size="16pt"
                        Text="Email: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox
                        ID="tbxCadastroEmailAutor"
                        runat="server"
                        CssClass="form-control"
                        Font-Size="16pt"
                        Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvCadastroEmailAutor"
                        runat="server"
                        ControlToValidate="tbxCadastroEmailAutor"
                        ErrorMessage="O email do autor é obrigatório"
                        Font-Size="16pt"
                        ForeColor="Red"
                        ValidationGroup="CadastroAutor"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button
                        ID="btnCadastrarAutor"
                        runat="server"
                        Text="Salvar"
                        CssClass="btn btn-success"
                        Font-Size="16pt"
                        Height="35px"
                        Width="150px"
                        Style="margin-top: 10px"
                        OnClick="BtnCadastrarAutor_Click"
                        ValidationGroup="CadastroAutor"></asp:Button>
                </td>
            </tr>
        </table>
    </div>

    <!-- Listar Autores Cadastrados -->
    <div class="row">
        <h2 style="text-align: center">Lista de Autores Cadastrados</h2>
        <asp:GridView
            ID="gvGerenciamentoAutores"
            runat="server"
            AutoGenerateColumns="false"
            GridLines="None"
            OnRowCancelingEdit="gvGerenciamentoAutores_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoAutores_RowEditing"
            OnRowUpdating="gvGerenciamentoAutores_RowUpdating"
            OnRowDeleting="gvGerenciamentoAutores_RowDeleting"
            >
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label
                            ID="lblEditIdAutor"
                            runat="server"
                            Text='<%# Eval("aut_id_autor") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label
                            ID="lblTextoIdAutor"
                            runat="server"
                            Style="width: 100%"
                            Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label
                            ID="lblIdAutor"
                            runat="server"
                            Style="text-align: center"
                            Text='<%# Eval("aut_id_autor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox
                            ID="tbxEditNomeAutor"
                            runat="server"
                            CssClass="form-control"
                            Height="35px"
                            Maxlenght="15"
                            Text='<%# Eval("aut_nm_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label
                            ID="lblTextoNomeAutor"
                            runat="server"
                            Style="text-align: center;"
                            Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label
                            ID="lblNomeAutor"
                            runat="server"
                            Style="text-align: left"
                            Text='<%# Eval("aut_nm_name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox
                            ID="tbxEditSobrenomeAutor"
                            runat="server"
                            CssClass="form-control"
                            Height="35px"
                            Maxlenght="15"
                            Text='<%# Eval("aut_nm_sobrenome") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label
                            ID="lblTextoSobrenomeAutor"
                            runat="server"
                            Style="text-align: center;"
                            Text="Sobrenome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label
                            ID="lblSobrenomeAutor"
                            runat="server"
                            Style="text-align: left"
                            Text='<%# Eval("aut_nm_sobrenome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox
                            ID="tbxEditEmailAutor"
                            runat="server"
                            CssClass="form-control"
                            Height="35px"
                            Maxlenght="15"
                            Text='<%# Eval("aut_ds_email") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label
                            ID="lblTextoEmailAutor"
                            runat="server"
                            Style="text-align: center;"
                            Text="Email"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label
                            ID="lblEmailAutor"
                            runat="server"
                            Style="text-align: left"
                            Text='<%# Eval("aut_ds_email") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button
                            ID="btnUpdate"
                            runat="server"
                            CommandName="Update"
                            CssClass="btn btn-success"
                            Text="Atualizar"
                            CausesValidation="false"
                            />&nbsp;
            <asp:Button
                ID="btnCancelar"
                runat="server"
                CommandName="Cancel"
                CssClass="btn btn-danger"
                Text="Cancelar"
                CausesValidation="false" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button
                            ID="btnEditarAutor"
                            runat="server"
                            CssClass="btn btn-success"
                            Text="Editar"
                            CommandName="Edit"
                            CausesValidation="false" />&nbsp;
            <asp:Button
                ID="btnDeletarAutor"
                runat="server"
                CssClass="btn btn-danger"
                Text="Deletar"
                CommandName="Delete"
                CausesValidation="false" />
                        <asp:Button
                            ID="btnCarregarLivrosAutor"
                            runat="server"
                            CssClass="btn btn-success"
                            Text="Livros"
                            CommandName="CarregarLivros"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CausesValidation="false" />&nbsp;
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle backcolor="White" />
            <EditRowStyle BackColor="White" Font-Size="14px" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle
                HorizontalAlign="Center"
                BackColor="#507CD1"
                Font-Bold="True"
                ForeColor="White" />
            <PagerStyle
                BackColor="#2461BF"
                ForeColor="White"
                HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
            <SelectedRowStyle
                BackColor="#D1DDF1"
                Font-Bold="True"
                ForeColor="#333333"
                Font-Size="14px" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</asp:Content>
