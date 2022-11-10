<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="Library_Management.Pages.GerenciamentoLivros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="text-align: left">
        <h2>Cadastro Novo Livro</h2>

        <!--Tabela com Input para Cadastro de Livro-->
        <table>
            <tr class="d-grid">
                <!--Cadastro Titulo Livro-->
                <td>
                    <asp:Label ID="lblCadastroTituloLivro" runat="server" Font-Size="16pt" Text="Titulo: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroTituloLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="rfvCadastroTituloLivro" runat="server" ControlToValidate="tbxCadastroTituloLivro" ErrorMessage="O título do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro Resumo Livro-->
                <td>
                    <asp:Label ID="lblCadastroResumoLivro" runat="server" Font-Size="16pt" Text="Resumo: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroResumoLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="rfvCadastroResumoLivro" runat="server" ControlToValidate="tbxCadastroResumoLivro" ErrorMessage="O resumo do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro Edição Livro-->
                <td>
                    <asp:Label ID="lblCadastroEdicaoLivro" runat="server" Font-Size="16pt" Text="Edição: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroEdicaoLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="rfvCadastroEdicaoLivro" runat="server" ControlToValidate="tbxCadastroEdicaoLivro" ErrorMessage="A edição do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro ID Tipo Livro-->
                <td>
                    <asp:Label ID="lblCadastroIdTipoLivro" runat="server" Font-Size="16pt" Text="ID da Categoria: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroIdTipoLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroIdTipoLivro" ErrorMessage="O id da categoria do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro Preço Livro-->
                <td>
                    <asp:Label ID="lblCadastroPrecoLivro" runat="server" Font-Size="16pt" Text="Preço: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroPrecoLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="rfvCadastroPrecoLivro" runat="server" ControlToValidate="tbxCadastroPrecoLivro" ErrorMessage="O preço do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro ID Editor Livro-->
                <td>
                    <asp:Label ID="lblCadastroIdEditorLivro" runat="server" Font-Size="16pt" Text="ID do Editor: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroIdEditorLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroIdEditorLivro" ErrorMessage="O id do editor do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Cadastro Royalty Livro-->
                <td>
                    <asp:Label ID="lblCadastroRoyaltyLivro" runat="server" Font-Size="16pt" Text="Royalty: " />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroRoyaltyLivro" runat="server" CssClass="form-control" Font-Size="16pt" Height="35px" Width="400px" />
                    <asp:RequiredFieldValidator ID="rfvCadastroRoyaltyLivro" runat="server" ControlToValidate="tbxCadastroRoyaltyLivro" ErrorMessage="O royalty do livro é obrigatório" Font-Size="16pt" ForeColor="Red" ValidationGroup="CadastroLivro" />
                </td>

                <!--Botão Cadastro Livro-->
                <td>
                    <asp:Button ID="btnCadastrarLivro" runat="server" Text="Salvar" CssClass="btn btn-success" Font-Size="16pt" Height="35px" Width="150px" Style="margin-top: 10px" OnClick="btnCadastrarLivro_Click" ValidationGroup="CadastrarLivro" />
                </td>
            </tr>
        </table>
    </div>

    <!--Tabela com Listagem de Livros-->
    <div class="row">
        <h2 style="text-align: center">Lista de Livros Cadastrados</h2>
        <asp:GridView ID="gvGerenciamentoLivros" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCancelingEdit="gvGerenciamentoLivros_RowCancelingEdit" OnRowEditing="gvGerenciamentoLivros_RowEditing" OnRowUpdating="gvGerenciamentoLivros_RowUpdating" OnRowDeleting="gvGerenciamentoLivros_RowDeleting">
            <Columns>
                <%-- Campo de ID --%>
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdLivro" runat="server" Style="width: 100%" Text="ID" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblIdLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_id_livro") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdLivro" runat="server" Text='<%# Eval("liv_id_livro") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de Titulo --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoTituloLivro" runat="server" Style="width: 100%" Text="Título" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblTituloLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_nm_titulo") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditTituloLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_nm_titulo") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de Resumo --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoResumoLivro" runat="server" Style="width: 100%" Text="Resumo" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblResumoLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_ds_resumo") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditResumoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_ds_resumo") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de Número de Edição --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEdicaoLivro" runat="server" Style="width: 100%" Text="Edição" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblEdicaoLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_nu_edicao") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEdicaoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_nu_edicao") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de ID do Tipo de Livro --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdTipoLivro" runat="server" Style="width: 100%" Text="Categoria" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblIdTipoLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_id_tipo_livro") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditIdTipoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_id_tipo_livro") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de Preço --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoPrecoLivro" runat="server" Style="width: 100%" Text="Preço" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblPrecoLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_vl_preco") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditPrecoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_vl_preco") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de ID do Editor --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdEditorLivro" runat="server" Style="width: 100%" Text="Editor" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblIdEditorLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_id_editor") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditIdEditorLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_id_editor") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Campo de Royalty --%>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoRoyaltyLivro" runat="server" Style="width: 100%" Text="Royalty" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblRoyaltyLivro" runat="server" Style="text-align: center" Text='<%# Eval("liv_pc_royalty") %>' />
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_pc_royalty") %>' />
                    </EditItemTemplate>

                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <%-- Botões --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />&nbsp;

                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Button ID="btnEditarLivro" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnDeletarLivro" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false" />&nbsp;
                    </ItemTemplate>
                    <%-- Faltando Header Sytle --%>
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
