using System;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace ClassesComplementares
{
    public partial class FormConfiguracoes : Form
    {
        #region Declaração das Variáveis

        XmlDocument xml;

        public FormConfiguracoes()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void FormConfiguracoes_Load(object sender, EventArgs e)
        {
            try
            {
                //Carrega o arquivo xml
                xml = new XmlDocument();
                xml.Load("Configuracoes.xml");

                //Conexão com Banco de Dados
                txtServidor.Text = xml.DocumentElement["servidor"].InnerText;
                txtBanco.Text = xml.DocumentElement["banco"].InnerText;
                txtUsuario.Text = xml.DocumentElement["usuario"].InnerText;
                txtSenha.Text = xml.DocumentElement["senha"].InnerText;

                //Informações do Projeto
                txtPastaProjeto.Text = xml.DocumentElement["pastaProjeto"].InnerText;
                txtNomeProjeto.Text = xml.DocumentElement["nomeProjeto"].InnerText;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível carregar as configurações. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validações.
            var erros = Validacoes();
            if (!string.IsNullOrWhiteSpace(erros))
            {
                MessageBox.Show(erros, "Classes Complementares - Mensagem de Erro");
                return;
            }

            try
            {
                //Conexão com Banco de Dados
                xml.DocumentElement["servidor"].InnerText = txtServidor.Text.Trim();
                xml.DocumentElement["banco"].InnerText = txtBanco.Text.Trim();
                xml.DocumentElement["usuario"].InnerText = txtUsuario.Text.Trim();
                xml.DocumentElement["senha"].InnerText = txtSenha.Text.Trim();

                //Informações do Projeto
                xml.DocumentElement["pastaProjeto"].InnerText = txtPastaProjeto.Text.Trim();
                xml.DocumentElement["nomeProjeto"].InnerText = txtNomeProjeto.Text.Trim();

                //Salva o arquivo
                xml.Save("Configuracoes.xml");

                this.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível salvar as configurações. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        /// <summary>
        /// Validações.
        /// </summary>
        /// <returns></returns>
        private string Validacoes()
        {
            var erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtServidor.Text))
                erros.AppendLine("Servidor do Banco deve ser informado.");

            if (string.IsNullOrWhiteSpace(txtBanco.Text))
                erros.AppendLine("Nome do Banco deve ser informado.");

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                erros.AppendLine("Usuário do Banco deve ser informado.");

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
                erros.AppendLine("Senha do Banco deve ser informada.");

            if (string.IsNullOrWhiteSpace(txtPastaProjeto.Text))
                erros.AppendLine("Pasta do Projeto deve ser informada.");

            if (string.IsNullOrWhiteSpace(txtNomeProjeto.Text))
                erros.AppendLine("Nome do Projeto deve ser informado.");

            return erros.ToString();
        }
    }
}