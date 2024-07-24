using System;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace ClassesComplementares
{
    public partial class FormMain : Form
    {
        #region Variáveis

        XmlDocument xml;
        string nomeTabela;
        string connectionString = string.Empty;
        string pastaProjeto = string.Empty;
        string nomeProjeto = string.Empty;
        const string GET_SET = "{ get; set; }";

        #endregion

        #region Inicialização

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracoes();
                CarregarComboTabelas();
                CarregarComboTipos();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível carregar as informações iniciais. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Fechar Janela
        /// </summary>
        private void FecharJanela()
        {
            this.Close();
        }

        /// <summary>
        /// Limpar Caixa de Texto
        /// </summary>
        private void LimparCaixaTexto()
        {
            txtResultado.Clear();
        }

        /// <summary>
        /// Carregar Combo Tabelas
        /// </summary>
        private void CarregarComboTabelas()
        {
            // Variáveis locais.
            string valueMember = "object_id";
            string columnsDisplayMember = "name";

            // Criação da tabela.
            DataTable dt = new DataTable();
            dt.Columns.Add(valueMember);
            dt.Columns.Add(columnsDisplayMember);

            // Adiciona a primeira linha.
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, "0", "Selecione...");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                // Comando do banco de dados
                StringBuilder stringCommand = new StringBuilder();
                stringCommand.Append("SELECT DISTINCT sys.tables.object_id ");
                stringCommand.Append("	, sys.tables.name ");
                stringCommand.Append("FROM sys.tables ");
                stringCommand.Append("INNER JOIN sys.columns ON sys.tables.object_id = sys.columns.object_id ");
                stringCommand.Append("ORDER BY sys.tables.name ");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, reader["object_id"].ToString(), reader["name"].ToString());
                        }
                    }
                }
            }

            // Carrega o controle com a tabela.
            comboTabelas.DisplayMember = columnsDisplayMember;
            comboTabelas.ValueMember = valueMember;
            comboTabelas.DataSource = dt;
        }

        /// <summary>
        /// Carregar Combo Tipos
        /// </summary>
        private void CarregarComboTipos()
        {
            // Variáveis locais.
            string valueMember = "IdTipo";
            string columnsDisplayMember = "Tipo";

            // Criação da tabela.
            DataTable dt = new DataTable();
            dt.Columns.Add(valueMember);
            dt.Columns.Add(columnsDisplayMember);

            // Adiciona as linhas.
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, "0", "Selecione...");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Entity).ToString(), "Entity");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.ViewModel).ToString(), "ViewModel");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Service_Class).ToString(), "Service Class");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Service_Interface).ToString(), "Service Interface");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Repository_Class).ToString(), "Repository Class");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Repository_Interface).ToString(), "Repository Interface");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command).ToString(), "Command");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Simplificado).ToString(), "Command (Simplificado)");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Create).ToString(), "Command Create");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Update).ToString(), "Command Update");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Delete).ToString(), "Command Delete");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Disable).ToString(), "Command Disable");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Validator).ToString(), "Command Validator");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Validator_Update).ToString(), "Command Validator Update");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Validator_Delete).ToString(), "Command Validator Delete");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Command_Handler).ToString(), "Command Handler");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Controller).ToString(), "Controller");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.AutoMapper).ToString(), "AutoMapper");
            AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Sql_Mappings).ToString(), "Sql Mappings");

            //AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Events_EventRequest).ToString(), "Events/Event Request");
            //AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Handlers_CommandHandler).ToString(), "Handlers/Command Handler");
            //AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Handlers_EventHandler).ToString(), "Handlers/Event Handler");
            //AdicionarNovaLinha(dt, ref valueMember, ref columnsDisplayMember, ((int)TiposClassesEnum.Helpers_ActionHelper).ToString(), "Helpers/Action Helper");

            // Carrega o controle com a tabela.
            comboTipos.DisplayMember = columnsDisplayMember;
            comboTipos.ValueMember = valueMember;
            comboTipos.DataSource = dt;
        }

        /// <summary>
        /// Adicionar Nova na Tabela Linha.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="valueMember"></param>
        /// <param name="columnsDisplayMember"></param>
        /// <param name="valor"></param>
        /// <param name="texto"></param>
        private void AdicionarNovaLinha(DataTable dt, ref string valueMember, ref string columnsDisplayMember, string valor, string texto)
        {
            DataRow objDataRow = dt.NewRow();
            objDataRow[valueMember] = valor;
            objDataRow[columnsDisplayMember] = texto;
            dt.Rows.Add(objDataRow);
        }

        /// <summary>
        /// Validações.
        /// </summary>
        /// <returns></returns>
        private string Validacoes()
        {
            StringBuilder erros = new StringBuilder();

            long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);
            int.TryParse(comboTipos.SelectedValue.ToString(), out int idTipo);

            if (object_id == 0)
            {
                erros.AppendLine("Escolha a Tabela.");
            }

            if (idTipo == 0)
            {
                erros.AppendLine("Escolha o Tipo.");
            }

            return erros.ToString();
        }

        /// <summary>
        /// Validações.
        /// </summary>
        /// <returns></returns>
        private string ValidacoesGerarSalvar()
        {
            StringBuilder erros = new StringBuilder();

            long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

            if (object_id == 0)
                erros.AppendLine("Escolha a Tabela.");

            if (!chEntity.Checked && !chViewModel.Checked && !chService.Checked &&
                !chRepository.Checked && !chCommand.Checked && !chCommandHandler.Checked &&
                !chController.Checked && !chAutoMapper.Checked && !chSqlMapping.Checked)
                erros.AppendLine("Marque pelo menos um tipo de arquivo.");

            return erros.ToString();
        }

        /// <summary>
        /// Copiar Texto do Controle
        /// </summary>
        private void CopiarTextoControle()
        {
            if (txtResultado.SelectionLength == 0)
            {
                txtResultado.SelectAll();
            }

            txtResultado.Copy();
        }

        /// <summary>
        /// Salvar no Projeto Texto do Controle.
        /// </summary>
        private void SalvarProjetoTextoControle()
        {
            if (string.IsNullOrWhiteSpace(txtResultado.Text))
            {
                throw new Exception("Nenhum texto para salvar no projeto.");
            }

            Enum.TryParse(comboTipos.SelectedValue.ToString(), out TiposClassesEnum tiposClassesEnum);
            SalvarProjetoArquivoTexto(tiposClassesEnum);
        }

        /// <summary>
        /// Gerar e Salvar arquivos no Projeto.
        /// </summary>
        private void GerarSalvarArquivoProjeto()
        {
            if (chEntity.Checked)
            {
                GerarArquivoTextoEntity();
                SalvarProjetoArquivoTextoEntity();
            }

            if (chViewModel.Checked)
            {
                GerarArquivoTextoViewModel();
                SalvarProjetoArquivoTextoViewModel();
            }

            if (chService.Checked)
            {
                GerarArquivoTextoServiceClass();
                SalvarProjetoArquivoTextoServiceClass();
                
                GerarArquivoTextoServiceInterface();
                SalvarProjetoArquivoTextoServiceInterface();
            }

            if (chRepository.Checked)
            {
                GerarArquivoTextoRepositoryClass();
                SalvarProjetoArquivoTextoRepositoryClass();

                GerarArquivoTextoRepositoryInterface();
                SalvarProjetoArquivoTextoRepositoryInterface();
            }

            if (chCommand.Checked)
            {
                GerarArquivoTextoCommand();
                SalvarProjetoArquivoTextoCommand();

                GerarArquivoTextoCommandCreate();
                SalvarProjetoArquivoTextoCommandCreate();

                GerarArquivoTextoCommandUpdate();
                SalvarProjetoArquivoTextoCommandUpdate();

                GerarArquivoTextoCommandDelete();
                SalvarProjetoArquivoTextoCommandDelete();

                GerarArquivoTextoCommandDisable();
                SalvarProjetoArquivoTextoCommandDisable();

                GerarArquivoTextoCommandValidator();
                SalvarProjetoArquivoTextoCommandValidator();

                GerarArquivoTextoCommandValidatorUpdate();
                SalvarProjetoArquivoTextoCommandValidatorUpdate();

                GerarArquivoTextoCommandValidatorDelete();
                SalvarProjetoArquivoTextoCommandValidatorDelete();
            }

            if (chCommandHandler.Checked)
            {
                GerarArquivoTextoCommandHandler();
                SalvarProjetoArquivoTextoCommandHandler();
            }

            if (chController.Checked)
            {
                GerarArquivoTextoController();
                SalvarProjetoArquivoTextoController();
            }

            if (chAutoMapper.Checked)
            {
                GerarArquivoTextoAutoMapper();
                SalvarProjetoArquivoTextoAutoMapper();
            }

            if (chSqlMapping.Checked)
            {
                GerarArquivoTextoSqlMappings();
                SalvarProjetoArquivoTextoSqlMappings();
            }

            txtResultado.Text = string.Empty;
        }

        /// <summary>
        /// Gerar Arquivo Texto
        /// </summary>
        /// <param name="tiposClassesEnum"></param>
        private void GerarArquivoTexto(TiposClassesEnum tiposClassesEnum)
        {
            switch (tiposClassesEnum)
            {
                case TiposClassesEnum.Entity:
                    GerarArquivoTextoEntity();
                    break;

                case TiposClassesEnum.ViewModel:
                    GerarArquivoTextoViewModel();
                    break;

                case TiposClassesEnum.Service_Class:
                    GerarArquivoTextoServiceClass();
                    break;

                case TiposClassesEnum.Service_Interface:
                    GerarArquivoTextoServiceInterface();
                    break;

                case TiposClassesEnum.Repository_Class:
                    GerarArquivoTextoRepositoryClass();
                    break;

                case TiposClassesEnum.Repository_Interface:
                    GerarArquivoTextoRepositoryInterface();
                    break;

                case TiposClassesEnum.Command:
                    GerarArquivoTextoCommand();
                    break;

                case TiposClassesEnum.Command_Simplificado:
                    GerarArquivoTextoCommandSimplificado();
                    break;

                case TiposClassesEnum.Command_Create:
                    GerarArquivoTextoCommandCreate();
                    break;

                case TiposClassesEnum.Command_Update:
                    GerarArquivoTextoCommandUpdate();
                    break;

                case TiposClassesEnum.Command_Delete:
                    GerarArquivoTextoCommandDelete();
                    break;

                case TiposClassesEnum.Command_Disable:
                    GerarArquivoTextoCommandDisable();
                    break;

                case TiposClassesEnum.Command_Validator:
                    GerarArquivoTextoCommandValidator();
                    break;

                case TiposClassesEnum.Command_Validator_Update:
                    GerarArquivoTextoCommandValidatorUpdate();
                    break;

                case TiposClassesEnum.Command_Validator_Delete:
                    GerarArquivoTextoCommandValidatorDelete();
                    break;

                case TiposClassesEnum.Command_Handler:
                    GerarArquivoTextoCommandHandler();
                    break;

                case TiposClassesEnum.Controller:
                    GerarArquivoTextoController();
                    break;

                case TiposClassesEnum.AutoMapper:
                    GerarArquivoTextoAutoMapper();
                    break;

                case TiposClassesEnum.Sql_Mappings:
                    GerarArquivoTextoSqlMappings();
                    break;
            }
        }

        /// <summary>
        /// Salvar arquivo texto no projeto.
        /// </summary>
        /// <param name="tiposClassesEnum"></param>
        private void SalvarProjetoArquivoTexto(TiposClassesEnum tiposClassesEnum)
        {
            switch (tiposClassesEnum)
            {
                case TiposClassesEnum.Entity:
                    SalvarProjetoArquivoTextoEntity();
                    break;

                case TiposClassesEnum.ViewModel:
                    SalvarProjetoArquivoTextoViewModel();
                    break;

                case TiposClassesEnum.Service_Class:
                    SalvarProjetoArquivoTextoServiceClass();
                    break;

                case TiposClassesEnum.Service_Interface:
                    SalvarProjetoArquivoTextoServiceInterface();
                    break;

                case TiposClassesEnum.Repository_Class:
                    SalvarProjetoArquivoTextoRepositoryClass();
                    break;

                case TiposClassesEnum.Repository_Interface:
                    SalvarProjetoArquivoTextoRepositoryInterface();
                    break;

                case TiposClassesEnum.Command:
                    SalvarProjetoArquivoTextoCommand();
                    break;

                case TiposClassesEnum.Command_Simplificado:
                    SalvarProjetoArquivoTextoCommand();
                    break;

                case TiposClassesEnum.Command_Create:
                    SalvarProjetoArquivoTextoCommandCreate();
                    break;

                case TiposClassesEnum.Command_Update:
                    SalvarProjetoArquivoTextoCommandUpdate();
                    break;

                case TiposClassesEnum.Command_Delete:
                    SalvarProjetoArquivoTextoCommandDelete();
                    break;

                case TiposClassesEnum.Command_Disable:
                    SalvarProjetoArquivoTextoCommandDisable();
                    break;

                case TiposClassesEnum.Command_Validator:
                    SalvarProjetoArquivoTextoCommandValidator();
                    break;

                case TiposClassesEnum.Command_Validator_Update:
                    SalvarProjetoArquivoTextoCommandValidatorUpdate();
                    break;

                case TiposClassesEnum.Command_Validator_Delete:
                    SalvarProjetoArquivoTextoCommandValidatorDelete();
                    break;

                case TiposClassesEnum.Command_Handler:
                    SalvarProjetoArquivoTextoCommandHandler();
                    break;

                case TiposClassesEnum.Controller:
                    SalvarProjetoArquivoTextoController();
                    break;

                case TiposClassesEnum.AutoMapper:
                    SalvarProjetoArquivoTextoAutoMapper();
                    break;

                case TiposClassesEnum.Sql_Mappings:
                    SalvarProjetoArquivoTextoSqlMappings();
                    break;
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Entity
        /// </summary>
        private void GerarArquivoTextoEntity()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringPropriedades = new StringBuilder();
                StringBuilder stringRelacionamentos = new StringBuilder();
                string[] propriedadesEntidadePai = { "Id", "Usuario", "DataCriacao", "DataAtualizacao", "Ativo" };

                // Comando do banco de dados.
                stringCommand.Append("SELECT ");
                stringCommand.Append("   sys.columns.name AS NameColumn, ");
                stringCommand.Append("   sys.types.name AS TypeName, ");
                stringCommand.Append("   sys.columns.is_nullable, ");
                stringCommand.Append("   sys.columns.max_length AS MaxLength, ");
                stringCommand.Append("   sys.foreign_keys.name AS ForeignKeyName ");
                stringCommand.Append("FROM sys.columns ");
                stringCommand.Append("INNER JOIN sys.types ");
                stringCommand.Append("   ON sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_key_columns ");
                stringCommand.Append("   ON sys.columns.column_id = sys.foreign_key_columns.parent_column_id ");
                stringCommand.Append("   AND sys.columns.object_id = sys.foreign_key_columns.parent_object_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_keys ");
                stringCommand.Append("   ON sys.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id ");
                stringCommand.Append("   AND sys.foreign_key_columns.parent_object_id = sys.foreign_keys.parent_object_id ");
                stringCommand.Append("WHERE ");
                stringCommand.Append("   sys.types.name <> 'sysname' ");
                stringCommand.Append("   AND sys.columns.object_id = @object_id ");
                stringCommand.Append("ORDER BY ");
                stringCommand.Append("   sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();
                            
                            if (propriedadesEntidadePai.Contains(nameColumn))
                                continue;

                            stringPropriedades.AppendFormat("        public {0} {1} {2}", RetornaTipoColuna(reader, true), nameColumn, GET_SET);
                            stringPropriedades.AppendLine().AppendLine();

                            var foreignKeyName = reader["ForeignKeyName"].ToString();
                            if (!string.IsNullOrWhiteSpace(foreignKeyName))
                            {
                                stringRelacionamentos.AppendLine();
                                stringRelacionamentos.AppendLine(string.Concat(string.Format("        public {0} {0} ", nameColumn.StartsWith("Id") ? nameColumn.Substring(2) : nameColumn), GET_SET));
                            }
                        }

                        if (stringPropriedades.Length > 0)
                            stringPropriedades.Remove(stringPropriedades.Length - 2, 2);
                    }
                }

                stringArquivoCompleto.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Interfaces;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Common.Entities");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine("    [Table(" + '"' + nomeTabela + '"' + ")]");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela} : Entity, IAggregateRoot");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.Append(stringPropriedades);
                stringArquivoCompleto.Append(stringRelacionamentos);
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - ViewModel
        /// </summary>
        private void GerarArquivoTextoViewModel()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringPropriedades = new StringBuilder();
                StringBuilder stringRelacionamentos = new StringBuilder();

                // Comando do banco de dados.
                stringCommand.Append("SELECT ");
                stringCommand.Append("   sys.columns.name AS NameColumn, ");
                stringCommand.Append("   sys.types.name AS TypeName, ");
                stringCommand.Append("   sys.columns.is_nullable, ");
                stringCommand.Append("   sys.columns.max_length AS MaxLength, ");
                stringCommand.Append("   sys.foreign_keys.name AS ForeignKeyName ");
                stringCommand.Append("FROM sys.columns ");
                stringCommand.Append("INNER JOIN sys.types ");
                stringCommand.Append("   ON sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_key_columns ");
                stringCommand.Append("   ON sys.columns.column_id = sys.foreign_key_columns.parent_column_id ");
                stringCommand.Append("   AND sys.columns.object_id = sys.foreign_key_columns.parent_object_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_keys ");
                stringCommand.Append("   ON sys.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id ");
                stringCommand.Append("   AND sys.foreign_key_columns.parent_object_id = sys.foreign_keys.parent_object_id ");
                stringCommand.Append("WHERE ");
                stringCommand.Append("   sys.types.name <> 'sysname' ");
                stringCommand.Append("   AND sys.columns.object_id = @object_id ");
                stringCommand.Append("ORDER BY ");
                stringCommand.Append("   sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();
                            stringPropriedades.AppendFormat("        public {0} {1} {2}", RetornaTipoColuna(reader), nameColumn, GET_SET);
                            stringPropriedades.AppendLine().AppendLine();

                            var foreignKeyName = reader["ForeignKeyName"].ToString();
                            if (!string.IsNullOrWhiteSpace(foreignKeyName))
                            {
                                stringRelacionamentos.AppendLine();
                                stringRelacionamentos.AppendLine(string.Concat(string.Format("        public {0}ViewModel {0} ", nameColumn.StartsWith("Id") ? nameColumn.Substring(2) : nameColumn), GET_SET));
                            }
                        }

                        if (stringPropriedades.Length > 0)
                            stringPropriedades.Remove(stringPropriedades.Length - 2, 2);
                    }
                }

                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Infrastructure.ViewModel");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}ViewModel");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.Append(stringPropriedades);
                stringArquivoCompleto.Append(stringRelacionamentos);
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Service Class
        /// </summary>
        private void GerarArquivoTextoServiceClass()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Repositories;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Interfaces;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Infra.Extensions;");
                stringArquivoCompleto.AppendLine("using Sabesp.Core.Provider.Api.Lib.Handlers;");
                stringArquivoCompleto.AppendLine("using Sabesp.Core.Provider.Api.Lib.Results;");
                stringArquivoCompleto.AppendLine("using System.Linq.Expressions;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Services");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public sealed class {nomeTabela}Service : ServiceBase<{nomeTabela}Service, {nomeTabela}, I{nomeTabela}Repository>, I{nomeTabela}Service");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public TesteService(IServiceProvider serviceProvider)");
                stringArquivoCompleto.AppendLine("            : base(serviceProvider) { }");
                stringArquivoCompleto.AppendLine();
                
                stringArquivoCompleto.AppendLine($"        public async Task<OperationResult<IEnumerable<{nomeTabela}>>> GetAllAsync(string filter)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            if (string.IsNullOrWhiteSpace(filter))");
                stringArquivoCompleto.AppendLine("                filter = " + '"' + '"' + ";");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            IEnumerable<{nomeTabela}> data;");
                stringArquivoCompleto.AppendLine($"            Expression<Func<{nomeTabela}, bool>> query = x => x.Ativo && x.Nome.ToUpper().Contains(filter.ToUpper());");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("            if (useCaching)");
                stringArquivoCompleto.AppendLine(string.Concat(string.Format("                data = await redisCache.GetEntityAsync<{0}, IEnumerable<{0}>>(BuildCacheKey($", nomeTabela), '"', "GetAllAsync:{filter}", '"', "), () => repository.GetAllAsync(query).Result);"));
                stringArquivoCompleto.AppendLine("            else");
                stringArquivoCompleto.AppendLine("                data = await repository.GetAllAsync(query);");
                stringArquivoCompleto.AppendLine();     
                stringArquivoCompleto.AppendLine("            return ResultEntityList(data);");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine();
                
                stringArquivoCompleto.AppendLine($"        public async Task<OperationResult<PaginationResult<{nomeTabela}>>> GetAllPaginatedAsync(PaginationFilter paginationFilter)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            if (string.IsNullOrEmpty(paginationFilter.Filter))");
                stringArquivoCompleto.AppendLine("                paginationFilter.Filter = " + '"' + '"' + ";");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            Expression<Func<{nomeTabela}, object>> orderBy = x => x.Nome;");
                stringArquivoCompleto.AppendLine($"            Expression<Func<{nomeTabela}, {nomeTabela}>> select = x => x;");
                stringArquivoCompleto.AppendLine($"            Expression<Func<{nomeTabela}, bool>> where = x => x.Nome.ToUpper().Contains(paginationFilter.Filter.ToUpper());");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("            return await GetAllPaginatedAsync(paginationFilter, where, orderBy, select);");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine();

                stringArquivoCompleto.AppendLine($"        private async Task<OperationResult<PaginationResult<{nomeTabela}>>> GetAllPaginatedAsync(PaginationFilter paginationFilter, Expression<Func<{nomeTabela}, bool>> where, Expression<Func<{nomeTabela}, object>> orderBy, Expression<Func<{nomeTabela}, {nomeTabela}>> select)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            (List<{nomeTabela}> data, int total) = await repository.GetAllPaginatedAsync(paginationFilter, where, orderBy, select);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("            if (!data.HasValue())");
                stringArquivoCompleto.AppendLine($"                return NotificationHandler.ErrorEmptyObject<{nomeTabela}>();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            return new PaginationResult<{nomeTabela}>");
                stringArquivoCompleto.AppendLine("            {");
                stringArquivoCompleto.AppendLine("                Data = data,");
                stringArquivoCompleto.AppendLine("                Total = total,");
                stringArquivoCompleto.AppendLine("                CurrentPage = paginationFilter.CurrentPage,");
                stringArquivoCompleto.AppendLine("                QuantityPerPage = paginationFilter.QuantityPerPage");
                stringArquivoCompleto.AppendLine("            };");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");

                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Service Interface
        /// </summary>
        private void GerarArquivoTextoServiceInterface()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Interfaces;");
                stringArquivoCompleto.AppendLine($"using Sabesp.Core.Provider.Api.Lib.Results;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Interfaces");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public interface I{nomeTabela}Service : IServiceReader<{nomeTabela}>, IServiceWriter<{nomeTabela}>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        Task<OperationResult<IEnumerable<{nomeTabela}>>> GetAllAsync(string filter);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"        Task<OperationResult<PaginationResult<{nomeTabela}>>> GetAllPaginatedAsync(PaginationFilter paginationFilter);");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Repository Class
        /// </summary>
        private void GerarArquivoTextoRepositoryClass()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Repositories;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Database.Sql.Repositories");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public sealed class {nomeTabela}Repository : RepositoryBase<{nomeTabela}>, I{nomeTabela}Repository");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}Repository(IServiceProvider serviceProvider)");
                stringArquivoCompleto.AppendLine("            : base(serviceProvider) { }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Repository Interface
        /// </summary>
        private void GerarArquivoTextoRepositoryInterface()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Interfaces;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Common.Repositories");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public interface I{nomeTabela}Repository : IRepositoryWriter<{nomeTabela}>, IRepositoryReader<{nomeTabela}>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command
        /// </summary>
        private void GerarArquivoTextoCommand()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringPropriedades = new StringBuilder();
                StringBuilder stringRelacionamentos = new StringBuilder();
                string[] propriedadesEntidadePai = { "Id", "Responsavel" };

                // Comando do banco de dados.
                stringCommand.Append("select sys.columns.name as NameColumn, sys.types.name AS TypeName, sys.columns.is_nullable ");
                stringCommand.Append("from sys.columns ");
                stringCommand.Append("inner join sys.types on sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("where sys.types.name <> 'sysname' ");
                stringCommand.Append("and sys.columns.object_id = @object_id ");
                stringCommand.Append("order by sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();

                            if (propriedadesEntidadePai.Contains(nameColumn))
                                continue;

                            stringPropriedades.AppendFormat("        public {0} {1} {2}", RetornaTipoColuna(reader), nameColumn, GET_SET);
                            stringPropriedades.AppendLine().AppendLine();
                        }

                        if (stringPropriedades.Length > 0)
                            stringPropriedades.Remove(stringPropriedades.Length - 2, 2);
                    }
                }

                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public abstract class {nomeTabela}Command : Command");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.Append(stringPropriedades);
                stringArquivoCompleto.Append(stringRelacionamentos);
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Simplificado
        /// </summary>
        private void GerarArquivoTextoCommandSimplificado()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringPropriedades = new StringBuilder();
                StringBuilder stringRelacionamentos = new StringBuilder();
                string[] propriedadesEntidadePai = { "Id", "Usuario" };

                // Comando do banco de dados.
                stringCommand.Append("select sys.columns.name as NameColumn, sys.types.name AS TypeName, sys.columns.is_nullable ");
                stringCommand.Append("from sys.columns ");
                stringCommand.Append("inner join sys.types on sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("where sys.types.name <> 'sysname' ");
                stringCommand.Append("and sys.columns.object_id = @object_id ");
                stringCommand.Append("order by sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();

                            if (propriedadesEntidadePai.Contains(nameColumn))
                                continue;

                            stringPropriedades.AppendFormat("        public {0} {1} {2}", RetornaTipoColuna(reader), nameColumn, GET_SET);
                            stringPropriedades.AppendLine().AppendLine();
                        }

                        if (stringPropriedades.Length > 0)
                            stringPropriedades.Remove(stringPropriedades.Length - 2, 2);
                    }
                }

                stringArquivoCompleto.AppendLine("using System;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}Command : Command");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.Append(stringPropriedades);
                stringArquivoCompleto.Append(stringRelacionamentos);
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        public override bool IsValid()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            ValidationResult = new {nomeTabela}Validator().Validate(this);");
                stringArquivoCompleto.AppendLine("            return ValidationResult.IsValid;");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Create
        /// </summary>
        private void GerarArquivoTextoCommandCreate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}CommandCreate : {nomeTabela}Command");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine("        public override bool IsValid()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            ValidationResult = new {nomeTabela}Validator().Validate(this);");
                stringArquivoCompleto.AppendLine("            return ValidationResult.IsValid;");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Update
        /// </summary>
        private void GerarArquivoTextoCommandUpdate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}CommandUpdate : {nomeTabela}Command");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine("        public override bool IsValid()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            ValidationResult = new {nomeTabela}ValidatorUpdate().Validate(this);");
                stringArquivoCompleto.AppendLine("            return ValidationResult.IsValid;");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Delete
        /// </summary>
        private void GerarArquivoTextoCommandDelete()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}CommandDelete : Command");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine("        public override bool IsValid()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            ValidationResult = new {nomeTabela}ValidatorDelete().Validate(this);");
                stringArquivoCompleto.AppendLine("            return ValidationResult.IsValid;");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Disable
        /// </summary>
        private void GerarArquivoTextoCommandDisable()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}CommandDisable : {nomeTabela}CommandDelete");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Validator
        /// </summary>
        private void GerarArquivoTextoCommandValidator()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringValidatesChamadas = new StringBuilder();
                StringBuilder stringValidatesMetodos = new StringBuilder();
                StringBuilder stringRelacionamentos = new StringBuilder();
                string[] propriedadesEntidadePai = { "Id", "DataCriacao", "DataAtualizacao", "Desativado" };

                // Comando do banco de dados.
                stringCommand.Append("select sys.columns.name as NameColumn, sys.types.name AS TypeName, sys.columns.is_nullable ");
                stringCommand.Append("from sys.columns ");
                stringCommand.Append("inner join sys.types on sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("where sys.types.name <> 'sysname' ");
                stringCommand.Append("and sys.columns.object_id = @object_id ");
                stringCommand.Append("order by sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();
                            var typeName = reader["TypeName"].ToString();
                            var isNullable = (bool)reader["is_nullable"];

                            if (propriedadesEntidadePai.Contains(nameColumn) || isNullable)
                                continue;

                            var condicaoValidacao = string.Empty;

                            if (typeName == "int" || typeName == "smallint" || typeName == "tinyint" || typeName == "bigint")
                                condicaoValidacao = "GreaterThan(0)";
                            else if (typeName == "nvarchar" || typeName == "varchar" || typeName == "char" || typeName == "nchar")
                                condicaoValidacao = "NotEmpty()";
                            else 
                                condicaoValidacao = "NotNull()";

                            stringValidatesMetodos.AppendLine();
                            stringValidatesMetodos.AppendLine($"        void Validate{nameColumn}()");
                            stringValidatesMetodos.AppendLine("        {");
                            stringValidatesMetodos.AppendLine($"            RuleFor(c => c.{nameColumn})");
                            stringValidatesMetodos.AppendLine($"                .{condicaoValidacao}");
                            stringValidatesMetodos.AppendLine(string.Format("                .WithMessage(" + '"' + "{0} deve ser informado" + '"' + ");", GetNomeColunaCustomizado(nameColumn, nomeTabela)));
                            stringValidatesMetodos.AppendLine("        }");

                            stringValidatesChamadas.AppendLine($"            Validate{nameColumn}();");
                        }

                        if (stringValidatesChamadas.Length > 0)
                            stringValidatesChamadas.Remove(stringValidatesChamadas.Length - 2, 2);
                    }
                }

                stringArquivoCompleto.AppendLine("using FluentValidation;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}Validator : AbstractValidator<{nomeTabela}Command>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}Validator()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.Append(stringValidatesChamadas).AppendLine();
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.Append(stringValidatesMetodos);
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Validator Update
        /// </summary>
        private void GerarArquivoTextoCommandValidatorUpdate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine("using FluentValidation;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}ValidatorUpdate : {nomeTabela}Validator");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}ValidatorUpdate() : base()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            ValidateId();");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        void ValidateId()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            RuleFor(c => c.Id)");
                stringArquivoCompleto.AppendLine("                .GreaterThan(0)");
                stringArquivoCompleto.AppendLine("                .WithMessage(" + '"' + "Registro não encontrado" + '"' + ");");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Validator Delete
        /// </summary>
        private void GerarArquivoTextoCommandValidatorDelete()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine("using FluentValidation;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Commands.{nomeTabela}.Validations");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}ValidatorDelete : AbstractValidator<{nomeTabela}CommandDelete>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}ValidatorDelete()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            ValidateId();");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        void ValidateId()");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            RuleFor(c => c.Id)");
                stringArquivoCompleto.AppendLine("                .GreaterThan(0)");
                stringArquivoCompleto.AppendLine("                .WithMessage(" + '"' + "Registro não encontrado" + '"' + ");");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Command Handler
        /// </summary>
        private void GerarArquivoTextoCommandHandler()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine("using FluentValidation.Results;");
                stringArquivoCompleto.AppendLine("using MediatR;");
                stringArquivoCompleto.AppendLine("using Microsoft.Extensions.Logging;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela};");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Interfaces;");
                stringArquivoCompleto.AppendLine($"using Sabesp.Core.Provider.Api.Lib.Results;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Domain.Handlers");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}CommandHandler : CommandHandlerBase<I{nomeTabela}OperationService, {nomeTabela}CommandHandler>,");
                stringArquivoCompleto.AppendLine($"        IRequestHandler<{nomeTabela}CommandCreate, ValidationResult>,");
                stringArquivoCompleto.AppendLine($"        IRequestHandler<{nomeTabela}CommandUpdate, ValidationResult>,");
                stringArquivoCompleto.AppendLine($"        IRequestHandler<{nomeTabela}CommandDelete, ValidationResult>,");
                stringArquivoCompleto.AppendLine($"        IRequestHandler<{nomeTabela}CommandDisable, ValidationResult>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}CommandHandler(IServiceProvider serviceProvider)");
                stringArquivoCompleto.AppendLine("            : base(serviceProvider) { }");
                stringArquivoCompleto.AppendLine();
                
                stringArquivoCompleto.AppendLine($"        public async Task<ValidationResult> Handle({nomeTabela}CommandCreate command, CancellationToken cancellationToken)");
                stringArquivoCompleto.AppendLine("            => await HandleDefault(command, x => x.SaveAsync(command));");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"        public async Task<ValidationResult> Handle({nomeTabela}CommandUpdate command, CancellationToken cancellationToken)");
                stringArquivoCompleto.AppendLine("            => await HandleDefault(command, x => x.EditAsync(command));");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"        public async Task<ValidationResult> Handle({nomeTabela}CommandDelete command, CancellationToken cancellationToken)");
                stringArquivoCompleto.AppendLine("            => await HandleDefault(command, x => x.DeleteAsync(command));");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"        public async Task<ValidationResult> Handle({nomeTabela}CommandDisable command, CancellationToken cancellationToken)");
                stringArquivoCompleto.AppendLine("            => await HandleDefault(command, x => x.DisableAsync(command));");
                stringArquivoCompleto.AppendLine();
                
                stringArquivoCompleto.AppendLine($"        private async Task<ValidationResult> HandleDefault(Command command, Func<I{nomeTabela}OperationService, Task<OperationResult<bool>>> action)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine("            logger.LogInformation($" + '"' + "Command: {typeof(Command).Name}, CorrelationId: {command.AggregateId}" + '"' + ");");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("            if (!command.IsValid())");
                stringArquivoCompleto.AppendLine("                return command.ValidationResult;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("            var result = await action(service);");
                stringArquivoCompleto.AppendLine("            return Result(command, result);");
                stringArquivoCompleto.AppendLine("        }");

                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Controller
        /// </summary>
        private void GerarArquivoTextoController()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine("using Microsoft.AspNetCore.Authorization;");
                stringArquivoCompleto.AppendLine("using Microsoft.AspNetCore.Mvc;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela};");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Interfaces;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Infra.Utilities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Lib.ViewModel;");
                stringArquivoCompleto.AppendLine("using Sabesp.Core.Provider.Api.Lib.Results;");
                stringArquivoCompleto.AppendLine();

                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Api.Controllers");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine("    [ProducesResponseType(StatusCodes.Status403Forbidden)]");
                stringArquivoCompleto.AppendLine($"    [ProducesResponseType(typeof(ResponseResult<{nomeTabela}>), StatusCodes.Status400BadRequest)]");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}Controller : ApiController<{nomeTabela}Controller, I{nomeTabela}Service, {nomeTabela}>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public {nomeTabela}Controller(IServiceProvider serviceProvider)");
                stringArquivoCompleto.AppendLine("            : base(serviceProvider) { }");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [HttpGet]");
                stringArquivoCompleto.AppendLine("        [Authorize]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<{nomeTabela}ResponseViewModel>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<{nomeTabela}ResponseViewModel>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        public async Task<ActionResult> GetAsync([FromQuery] int id)");
                stringArquivoCompleto.AppendLine($"            => await ReadCustomAsync<{nomeTabela}ResponseViewModel>(id);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [Authorize]");
                stringArquivoCompleto.AppendLine("        [HttpGet(" + '"' + "[action]" + '"' + ")]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<IEnumerable<{nomeTabela}ResponseViewModel>>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<IEnumerable<{nomeTabela}ResponseViewModel>>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        public async Task<ActionResult> GetAllAsync([FromQuery] string filter)");
                stringArquivoCompleto.AppendLine($"            => await GetAllDefaultAsync<{nomeTabela}ResponseViewModel>(x => x.GetAllAsync(filter));");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [Authorize]");
                stringArquivoCompleto.AppendLine("        [HttpGet(" + '"' + "[action]" + '"' + ")]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<PaginationResult<{nomeTabela}ResponseViewModel>>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine($"        [ProducesResponseType(typeof(ResponseResult<PaginationResult<{nomeTabela}ResponseViewModel>>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        public async Task<ActionResult> GetAllPaginatedAsync([FromQuery] PaginationFilter paginationFilter)");
                stringArquivoCompleto.AppendLine($"            => await GetAllPaginatedDefaultAsync<{nomeTabela}ResponseViewModel>(x => x.GetAllPaginatedAsync(paginationFilter));");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [HttpPost]");
                stringArquivoCompleto.AppendLine("        [Authorize(Roles = Settings.AdminGroup)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status422UnprocessableEntity)]");
                stringArquivoCompleto.AppendLine($"        public async Task<ActionResult> PostAsync({nomeTabela}RequestViewModel viewModel)");
                stringArquivoCompleto.AppendLine($"            => await CommandActionAsync<{nomeTabela}RequestViewModel, {nomeTabela}CommandCreate>(viewModel);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [HttpPut]");
                stringArquivoCompleto.AppendLine("        [Authorize(Roles = Settings.AdminGroup)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status422UnprocessableEntity)]");
                stringArquivoCompleto.AppendLine($"        public async Task<ActionResult> PutAsync({nomeTabela}RequestViewModel viewModel)");
                stringArquivoCompleto.AppendLine($"            => await CommandActionAsync<{nomeTabela}RequestViewModel, {nomeTabela}CommandUpdate>(viewModel);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [HttpDelete]");
                stringArquivoCompleto.AppendLine("        [Authorize(Roles = Settings.AdminGroup)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status422UnprocessableEntity)]");
                stringArquivoCompleto.AppendLine("        public async Task<ActionResult> DeleteAsync([FromQuery] int id)");
                stringArquivoCompleto.AppendLine($"            => await CommandActionAsync<{nomeTabela}CommandDelete>(id);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine("        [HttpPatch(" + '"' + "[action]" + '"' + ")]");
                stringArquivoCompleto.AppendLine("        [Authorize(Roles = Settings.AdminGroup)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status200OK)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status404NotFound)]");
                stringArquivoCompleto.AppendLine("        [ProducesResponseType(typeof(ResponseResult<bool>), StatusCodes.Status422UnprocessableEntity)]");
                stringArquivoCompleto.AppendLine("        public async Task<ActionResult> DisableAsync([FromQuery] int id)");
                stringArquivoCompleto.AppendLine($"            => await CommandActionAsync<{nomeTabela}CommandDisable>(id);");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - AutoMapper
        /// </summary>
        private void GerarArquivoTextoAutoMapper()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringArquivoCompleto = new StringBuilder();

                stringArquivoCompleto.AppendLine("using AutoMapper;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Commands.{nomeTabela};");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Infra.Extensions;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Infra.Utilities;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Lib.ViewModel;");
                stringArquivoCompleto.AppendLine("using Sabesp.Core.Provider.Api.Lib.Results;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Infra.AutoMapper.Mappings");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public static class Mapper{nomeTabela}");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public static void Map{nomeTabela}(IProfileExpression profile)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}, {nomeTabela}ViewModel>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}, {nomeTabela}ResponseViewModel>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}RequestViewModel, {nomeTabela}CommandCreate>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}RequestViewModel, {nomeTabela}CommandUpdate>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}CommandCreate, {nomeTabela}>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<{nomeTabela}CommandUpdate, {nomeTabela}>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"            profile.CreateMap<PaginationResult<{nomeTabela}>, PaginationResult<{nomeTabela}ResponseViewModel>>()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting()");
                stringArquivoCompleto.AppendLine("                .ReverseMap()");
                stringArquivoCompleto.AppendLine("                .IgnoreAllNonExisting();");
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Gerar Arquivo Texto - Sql Mappings
        /// </summary>
        private void GerarArquivoTextoSqlMappings()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Declaração das variáveis.
                StringBuilder stringCommand = new StringBuilder();
                StringBuilder stringArquivoCompleto = new StringBuilder();
                StringBuilder stringPropriedades = new StringBuilder();
                StringBuilder stringForeignKeys = new StringBuilder();

                // Comando do banco de dados.
                stringCommand.Append("SELECT ");
                stringCommand.Append("   sys.columns.name AS NameColumn, ");
                stringCommand.Append("   sys.types.name AS TypeName, ");
                stringCommand.Append("   sys.columns.is_nullable AS IsNullable, ");
                stringCommand.Append("   sys.columns.max_length AS MaxLength, ");
                stringCommand.Append("   sys.foreign_keys.name AS ForeignKeyName ");
                stringCommand.Append("FROM sys.columns ");
                stringCommand.Append("INNER JOIN sys.types ");
                stringCommand.Append("   ON sys.columns.system_type_id = sys.types.system_type_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_key_columns ");
                stringCommand.Append("   ON sys.columns.column_id = sys.foreign_key_columns.parent_column_id ");
                stringCommand.Append("   AND sys.columns.object_id = sys.foreign_key_columns.parent_object_id ");
                stringCommand.Append("LEFT JOIN sys.foreign_keys ");
                stringCommand.Append("   ON sys.foreign_key_columns.constraint_object_id = sys.foreign_keys.object_id ");
                stringCommand.Append("   AND sys.foreign_key_columns.parent_object_id = sys.foreign_keys.parent_object_id ");
                stringCommand.Append("WHERE ");
                stringCommand.Append("   sys.types.name <> 'sysname' ");
                stringCommand.Append("   AND sys.columns.object_id = @object_id ");
                stringCommand.Append("ORDER BY ");
                stringCommand.Append("   sys.columns.column_id ");

                // Carrega as informações do banco.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abre a conexão.
                    SqlCommand command = new SqlCommand(stringCommand.ToString(), connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@object_id", object_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameColumn = reader["NameColumn"].ToString();
                            var typeName = reader["TypeName"].ToString();
                            var isNullable = (bool)reader["IsNullable"];
                            var maxLength = Convert.ToInt32(reader["MaxLength"]);
                            var foreignKeyName = reader["ForeignKeyName"].ToString();

                            stringPropriedades.Append($"            builder.Property(c => c.{nameColumn})");

                            if ((typeName == "nvarchar" || typeName == "varchar" || typeName == "char" || typeName == "nchar") && maxLength > 1)
                                stringPropriedades.Append($".HasMaxLength({maxLength / 2})");

                            if (!isNullable)
                                stringPropriedades.Append(".IsRequired()");

                            stringPropriedades.AppendLine(";");

                            if (!string.IsNullOrWhiteSpace(foreignKeyName))
                            {
                                stringForeignKeys.AppendLine();
                                stringForeignKeys.AppendLine($"            builder.HasOne(d => d.{(nameColumn.StartsWith("Id") ? nameColumn.Substring(2) : nameColumn)})");
                                stringForeignKeys.AppendLine($"                .WithMany(p => p.{nomeTabela}s)");
                                stringForeignKeys.AppendLine($"                .HasForeignKey(d => d.{nameColumn});");
                            }
                        }
                    }
                }

                stringArquivoCompleto.AppendLine("using Microsoft.EntityFrameworkCore;");
                stringArquivoCompleto.AppendLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
                stringArquivoCompleto.AppendLine($"using {nomeProjeto}.Domain.Common.Entities;");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.AppendLine($"namespace {nomeProjeto}.Database.Sql.Mappings");
                stringArquivoCompleto.AppendLine("{");
                stringArquivoCompleto.AppendLine($"    public class {nomeTabela}Mapping : IEntityTypeConfiguration<{nomeTabela}>");
                stringArquivoCompleto.AppendLine("    {");
                stringArquivoCompleto.AppendLine($"        public void Configure(EntityTypeBuilder<{nomeTabela}> builder)");
                stringArquivoCompleto.AppendLine("        {");
                stringArquivoCompleto.AppendLine($"            builder.ToTable(" + '"' + nomeTabela + '"' + ");");
                stringArquivoCompleto.AppendLine("            builder.HasKey(x => x.Id);");
                stringArquivoCompleto.AppendLine();
                stringArquivoCompleto.Append(stringPropriedades);
                stringArquivoCompleto.Append(stringForeignKeys);
                stringArquivoCompleto.AppendLine("        }");
                stringArquivoCompleto.AppendLine("    }");
                stringArquivoCompleto.Append("}");

                // Escreve o texto no controle.
                txtResultado.Text = stringArquivoCompleto.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Entity.
        /// </summary>
        private void SalvarProjetoArquivoTextoEntity()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain.Common\Entities\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - ViewModel.
        /// </summary>
        private void SalvarProjetoArquivoTextoViewModel()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Infrastructure.ViewModel\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}ViewModel.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Service Class.
        /// </summary>
        private void SalvarProjetoArquivoTextoServiceClass()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain\\Services\\{nomeTabela}";
                string nomeArquivo = $"{pasta}\\{nomeTabela}Service.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Service Interface.
        /// </summary>
        private void SalvarProjetoArquivoTextoServiceInterface()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain";
                string pastaInterface = $"{pasta}\\Interfaces\\{nomeTabela}";
                string nomeArquivo = $"{pastaInterface}\\I{nomeTabela}Service.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                if (!Directory.Exists(pastaInterface))
                    Directory.CreateDirectory(pastaInterface);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Repository Class.
        /// </summary>
        private void SalvarProjetoArquivoTextoRepositoryClass()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Database.Sql\\Repositories";
                string nomeArquivo = $"{pasta}\\{nomeTabela}Repository.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Repository Interface.
        /// </summary>
        private void SalvarProjetoArquivoTextoRepositoryInterface()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain.Common\\Repositories";
                string nomeArquivo = $"{pasta}\\I{nomeTabela}Repository.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommand()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain\Commands\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}Command.cs", pastaViewModel, nomeTabela);
                
                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Create.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandCreate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain\Commands\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}CommandCreate.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Update.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandUpdate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain\Commands\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}CommandUpdate.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Delete.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandDelete()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain\Commands\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}CommandDelete.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Disable.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandDisable()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pastaViewModel = string.Format(@"{0}\{1}.Domain\Commands\{2}", pastaProjeto, nomeProjeto, nomeTabela);
                string nomeArquivo = string.Format(@"{0}\{1}CommandDisable.cs", pastaViewModel, nomeTabela);

                if (!Directory.Exists(pastaViewModel))
                    Directory.CreateDirectory(pastaViewModel);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Validator.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandValidator()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain\\Commands\\{nomeTabela}";
                string pastaInterface = $"{pasta}\\Validations";
                string nomeArquivo = $"{pastaInterface}\\{nomeTabela}Validator.cs";
                
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                if (!Directory.Exists(pastaInterface))
                    Directory.CreateDirectory(pastaInterface);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Validator Update.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandValidatorUpdate()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain\\Commands\\{nomeTabela}";
                string pastaInterface = $"{pasta}\\Validations";
                string nomeArquivo = $"{pastaInterface}\\{nomeTabela}ValidatorUpdate.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                if (!Directory.Exists(pastaInterface))
                    Directory.CreateDirectory(pastaInterface);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Validator Delete.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandValidatorDelete()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain\\Commands\\{nomeTabela}";
                string pastaInterface = $"{pasta}\\Validations";
                string nomeArquivo = $"{pastaInterface}\\{nomeTabela}ValidatorDelete.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                if (!Directory.Exists(pastaInterface))
                    Directory.CreateDirectory(pastaInterface);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Command Handler.
        /// </summary>
        private void SalvarProjetoArquivoTextoCommandHandler()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Domain\\Handlers\\{nomeTabela}";
                string nomeArquivo = $"{pasta}\\{nomeTabela}CommandHandler.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Controller.
        /// </summary>
        private void SalvarProjetoArquivoTextoController()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Api\\Controllers\\{nomeTabela}";
                string nomeArquivo = $"{pasta}\\{nomeTabela}Controller.cs";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - AutoMapper.
        /// </summary>
        private void SalvarProjetoArquivoTextoAutoMapper()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Infra.AutoMapper\\Mappings\\{nomeTabela}";
                string nomeArquivo = $"{pasta}\\Mapper{nomeTabela}.cs";
                
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Salvar Projeto Arquivo Texto - Sql Mappings.
        /// </summary>
        private void SalvarProjetoArquivoTextoSqlMappings()
        {
            try
            {
                // Prepara as informações iniciais.
                nomeTabela = comboTabelas.Text;
                long.TryParse(comboTabelas.SelectedValue.ToString(), out long object_id);

                if (object_id == 0)
                    throw new Exception("Não foi selecionada nenhuma tabela.");

                // Nome do arquivo.
                string pasta = $"{pastaProjeto}\\{nomeProjeto}.Database.Sql\\Mappings\\{nomeTabela}";
                string nomeArquivo = $"{pasta}\\{nomeTabela}Mapping.cs";
                

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Salva o novo arquivo.
                StreamWriter valor = new StreamWriter(nomeArquivo, false, Encoding.UTF8);
                valor.Write(txtResultado.Text);
                valor.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar o arquivo. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        /// <summary>
        /// Retornar p tipo de coluna.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private string RetornaTipoColuna(SqlDataReader reader, bool isEntity = false)
        {
            string typeColumn;
            
            switch (reader["TypeName"].ToString())
            {
                case "int":
                case "smallint":
                case "tinyint":
                    typeColumn = "int";
                    break;

                case "bigint":
                    typeColumn = "long";
                    break;

                case "bit":
                    typeColumn = "bool";
                    break;

                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "datetimeoffset":
                    typeColumn = "DateTime";
                    break;

                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    typeColumn = "decimal";
                    break;

                case "float":
                case "real":
                    typeColumn = "double";
                    break;

                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    return "string";

                default:
                    return "string";
            }

            if ((bool)reader["is_nullable"] || (!isEntity && typeColumn != "int" && typeColumn != "long"))
                typeColumn = $"{typeColumn}?";

            return typeColumn;
        }

        /// <summary>
        /// Carregar as configurações do arquivo xml
        /// </summary>
        private void CarregarConfiguracoes()
        {
            //Carrega o arquivo xml
            xml = new XmlDocument();
            xml.Load("Configuracoes.xml");

            //Conexão com Banco de Dados
            var servidor = xml.DocumentElement["servidor"].InnerText;
            var banco = xml.DocumentElement["banco"].InnerText;
            var usuario = xml.DocumentElement["usuario"].InnerText;
            var senha = xml.DocumentElement["senha"].InnerText;
            connectionString = $"Data Source={servidor}; Initial Catalog={banco}; User ID={usuario}; Password={senha};";

            //Informações do Projeto
            pastaProjeto = xml.DocumentElement["pastaProjeto"].InnerText;
            nomeProjeto = xml.DocumentElement["nomeProjeto"].InnerText;
        }

        /// <summary>
        /// Ajustar o nome do campo para informar na validação
        /// </summary>
        /// <param name="nameColumn"></param>
        /// <param name="nomeTabela"></param>
        /// <returns></returns>
        private string GetNomeColunaCustomizado(string nameColumn, string nomeTabela)
        {
            var nameColumnCustom = nameColumn.StartsWith("Id") ? nameColumn.Substring(2) : nameColumn;
            nameColumnCustom = nameColumnCustom.Replace(nomeTabela, "");
            return string.Concat(nameColumnCustom.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
        }

        #endregion

        #region Eventos

        private void btnGerar_Click(object sender, EventArgs e)
        {
            // Validações.
            string erros = Validacoes();

            if (string.IsNullOrWhiteSpace(erros))
            {
                Enum.TryParse(comboTipos.SelectedValue.ToString(), out TiposClassesEnum tiposClassesEnum);
                GerarArquivoTexto(tiposClassesEnum);
                MessageBox.Show("Arquivo gerado com sucesso!!!", "Classes Complementares - Mensagem de Confirmação");
            }
            else
            {
                MessageBox.Show(erros, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCaixaTexto();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível limpar a campo de texto. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            try
            {
                FecharJanela();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível fechar a janela. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            try
            {
                CopiarTextoControle();
                MessageBox.Show("Texto copiado com sucesso!", "Classes Complementares - Mensagem de Confirmação");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível limpar a campo de texto. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnSalvarProjeto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja salvar o arquivo no projeto?", "Classes Complementares", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (confirmacao == DialogResult.Yes)
                {
                    SalvarProjetoTextoControle();
                    MessageBox.Show("Arquivo salvo no projeto com sucesso!", "Classes Complementares - Mensagem de Confirmação");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível salvar o arquivo no projeto. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            new FormConfiguracoes().ShowDialog();
            CarregarConfiguracoes();
            CarregarComboTabelas();
        }

        private void chMarcarTodos_CheckedChanged(object sender, EventArgs e)
        {
            chEntity.Checked = chViewModel.Checked = chService.Checked =
            chRepository.Checked = chCommand.Checked = chCommandHandler.Checked =
            chController.Checked = chAutoMapper.Checked = chSqlMapping.Checked = chMarcarTodos.Checked;
        }

        private void btnGerarSalvar_Click(object sender, EventArgs e)
        {
            // Validações.
            string erros = ValidacoesGerarSalvar();
            if (!string.IsNullOrWhiteSpace(erros))
            {
                MessageBox.Show(erros, "Classes Complementares - Mensagem de Erro");
                return;
            }

            try
            {
                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja gerar e salvar o(s) arquivo(s) no projeto?", "Classes Complementares", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (confirmacao == DialogResult.Yes)
                {
                    GerarSalvarArquivoProjeto();
                    MessageBox.Show("Arquivo(s) gerado(s) e salvo(s) no projeto com sucesso!", "Classes Complementares - Mensagem de Confirmação");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível gerar e salvar o(s) arquivo(s) no projeto. Erro: " + erro.Message, "Classes Complementares - Mensagem de Erro");
            }
        }

        #endregion
    }
}
