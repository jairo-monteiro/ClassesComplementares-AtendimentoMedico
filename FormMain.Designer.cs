
namespace ClassesComplementares
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.comboTabelas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.comboTipos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnSalvarProjeto = new System.Windows.Forms.Button();
            this.btnConfiguracoes = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chViewModel = new System.Windows.Forms.CheckBox();
            this.chAutoMapper = new System.Windows.Forms.CheckBox();
            this.chCommandHandler = new System.Windows.Forms.CheckBox();
            this.chRepository = new System.Windows.Forms.CheckBox();
            this.chMarcarTodos = new System.Windows.Forms.CheckBox();
            this.btnGerarSalvar = new System.Windows.Forms.Button();
            this.chController = new System.Windows.Forms.CheckBox();
            this.chSqlMapping = new System.Windows.Forms.CheckBox();
            this.chCommand = new System.Windows.Forms.CheckBox();
            this.chService = new System.Windows.Forms.CheckBox();
            this.chEntity = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Geração de Classes Complementares";
            // 
            // comboTabelas
            // 
            this.comboTabelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTabelas.FormattingEnabled = true;
            this.comboTabelas.Location = new System.Drawing.Point(91, 23);
            this.comboTabelas.Name = "comboTabelas";
            this.comboTabelas.Size = new System.Drawing.Size(273, 24);
            this.comboTabelas.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tabela:";
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerar.Location = new System.Drawing.Point(12, 413);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(100, 25);
            this.btnGerar.TabIndex = 3;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Location = new System.Drawing.Point(118, 413);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 25);
            this.btnLimpar.TabIndex = 3;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Location = new System.Drawing.Point(850, 413);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 25);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultado.Location = new System.Drawing.Point(12, 167);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(938, 240);
            this.txtResultado.TabIndex = 4;
            // 
            // comboTipos
            // 
            this.comboTipos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTipos.FormattingEnabled = true;
            this.comboTipos.Location = new System.Drawing.Point(91, 60);
            this.comboTipos.Name = "comboTipos";
            this.comboTipos.Size = new System.Drawing.Size(273, 24);
            this.comboTipos.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Controls.Add(this.comboTabelas);
            this.groupBox1.Controls.Add(this.comboTipos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações para gerar Classes";
            // 
            // btnCopiar
            // 
            this.btnCopiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopiar.Location = new System.Drawing.Point(224, 413);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(110, 25);
            this.btnCopiar.TabIndex = 3;
            this.btnCopiar.Text = "Copiar Texto";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnSalvarProjeto
            // 
            this.btnSalvarProjeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvarProjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarProjeto.Location = new System.Drawing.Point(340, 413);
            this.btnSalvarProjeto.Name = "btnSalvarProjeto";
            this.btnSalvarProjeto.Size = new System.Drawing.Size(132, 25);
            this.btnSalvarProjeto.TabIndex = 3;
            this.btnSalvarProjeto.Text = "Salvar no Projeto";
            this.btnSalvarProjeto.UseVisualStyleBackColor = true;
            this.btnSalvarProjeto.Click += new System.EventHandler(this.btnSalvarProjeto_Click);
            // 
            // btnConfiguracoes
            // 
            this.btnConfiguracoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfiguracoes.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnConfiguracoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracoes.Location = new System.Drawing.Point(728, 413);
            this.btnConfiguracoes.Name = "btnConfiguracoes";
            this.btnConfiguracoes.Size = new System.Drawing.Size(116, 25);
            this.btnConfiguracoes.TabIndex = 3;
            this.btnConfiguracoes.Text = "Configurações";
            this.btnConfiguracoes.UseVisualStyleBackColor = true;
            this.btnConfiguracoes.Click += new System.EventHandler(this.btnConfiguracoes_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.chViewModel);
            this.groupBox2.Controls.Add(this.chAutoMapper);
            this.groupBox2.Controls.Add(this.chCommandHandler);
            this.groupBox2.Controls.Add(this.chRepository);
            this.groupBox2.Controls.Add(this.chMarcarTodos);
            this.groupBox2.Controls.Add(this.btnGerarSalvar);
            this.groupBox2.Controls.Add(this.chController);
            this.groupBox2.Controls.Add(this.chSqlMapping);
            this.groupBox2.Controls.Add(this.chCommand);
            this.groupBox2.Controls.Add(this.chService);
            this.groupBox2.Controls.Add(this.chEntity);
            this.groupBox2.Location = new System.Drawing.Point(398, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Marque os Tipos de Arquivos para serem gerados";
            // 
            // chViewModel
            // 
            this.chViewModel.AutoSize = true;
            this.chViewModel.Checked = true;
            this.chViewModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chViewModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chViewModel.Location = new System.Drawing.Point(19, 46);
            this.chViewModel.Name = "chViewModel";
            this.chViewModel.Size = new System.Drawing.Size(94, 21);
            this.chViewModel.TabIndex = 0;
            this.chViewModel.Text = "ViewModel";
            this.chViewModel.UseVisualStyleBackColor = true;
            // 
            // chAutoMapper
            // 
            this.chAutoMapper.AutoSize = true;
            this.chAutoMapper.Checked = true;
            this.chAutoMapper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chAutoMapper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chAutoMapper.Location = new System.Drawing.Point(291, 46);
            this.chAutoMapper.Name = "chAutoMapper";
            this.chAutoMapper.Size = new System.Drawing.Size(104, 21);
            this.chAutoMapper.TabIndex = 0;
            this.chAutoMapper.Text = "AutoMapper";
            this.chAutoMapper.UseVisualStyleBackColor = true;
            // 
            // chCommandHandler
            // 
            this.chCommandHandler.AutoSize = true;
            this.chCommandHandler.Checked = true;
            this.chCommandHandler.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chCommandHandler.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chCommandHandler.Location = new System.Drawing.Point(129, 73);
            this.chCommandHandler.Name = "chCommandHandler";
            this.chCommandHandler.Size = new System.Drawing.Size(144, 21);
            this.chCommandHandler.TabIndex = 0;
            this.chCommandHandler.Text = "Command Handler";
            this.chCommandHandler.UseVisualStyleBackColor = true;
            // 
            // chRepository
            // 
            this.chRepository.AutoSize = true;
            this.chRepository.Checked = true;
            this.chRepository.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chRepository.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chRepository.Location = new System.Drawing.Point(129, 19);
            this.chRepository.Name = "chRepository";
            this.chRepository.Size = new System.Drawing.Size(95, 21);
            this.chRepository.TabIndex = 0;
            this.chRepository.Text = "Repository";
            this.chRepository.UseVisualStyleBackColor = true;
            // 
            // chMarcarTodos
            // 
            this.chMarcarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chMarcarTodos.AutoSize = true;
            this.chMarcarTodos.Checked = true;
            this.chMarcarTodos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chMarcarTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chMarcarTodos.Location = new System.Drawing.Point(479, 9);
            this.chMarcarTodos.Name = "chMarcarTodos";
            this.chMarcarTodos.Size = new System.Drawing.Size(67, 21);
            this.chMarcarTodos.TabIndex = 0;
            this.chMarcarTodos.Text = "Todos";
            this.chMarcarTodos.UseVisualStyleBackColor = true;
            this.chMarcarTodos.CheckedChanged += new System.EventHandler(this.chMarcarTodos_CheckedChanged);
            // 
            // btnGerarSalvar
            // 
            this.btnGerarSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarSalvar.Location = new System.Drawing.Point(414, 45);
            this.btnGerarSalvar.Name = "btnGerarSalvar";
            this.btnGerarSalvar.Size = new System.Drawing.Size(132, 49);
            this.btnGerarSalvar.TabIndex = 3;
            this.btnGerarSalvar.Text = "Gerar e Salvar no Projeto";
            this.btnGerarSalvar.UseVisualStyleBackColor = true;
            this.btnGerarSalvar.Click += new System.EventHandler(this.btnGerarSalvar_Click);
            // 
            // chController
            // 
            this.chController.AutoSize = true;
            this.chController.Checked = true;
            this.chController.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chController.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chController.Location = new System.Drawing.Point(291, 19);
            this.chController.Name = "chController";
            this.chController.Size = new System.Drawing.Size(88, 21);
            this.chController.TabIndex = 0;
            this.chController.Text = "Controller";
            this.chController.UseVisualStyleBackColor = true;
            // 
            // chSqlMapping
            // 
            this.chSqlMapping.AutoSize = true;
            this.chSqlMapping.Checked = true;
            this.chSqlMapping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chSqlMapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chSqlMapping.Location = new System.Drawing.Point(291, 73);
            this.chSqlMapping.Name = "chSqlMapping";
            this.chSqlMapping.Size = new System.Drawing.Size(105, 21);
            this.chSqlMapping.TabIndex = 0;
            this.chSqlMapping.Text = "Sql Mapping";
            this.chSqlMapping.UseVisualStyleBackColor = true;
            // 
            // chCommand
            // 
            this.chCommand.AutoSize = true;
            this.chCommand.Checked = true;
            this.chCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chCommand.Location = new System.Drawing.Point(129, 46);
            this.chCommand.Name = "chCommand";
            this.chCommand.Size = new System.Drawing.Size(90, 21);
            this.chCommand.TabIndex = 0;
            this.chCommand.Text = "Command";
            this.chCommand.UseVisualStyleBackColor = true;
            // 
            // chService
            // 
            this.chService.AutoSize = true;
            this.chService.Checked = true;
            this.chService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chService.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chService.Location = new System.Drawing.Point(19, 73);
            this.chService.Name = "chService";
            this.chService.Size = new System.Drawing.Size(74, 21);
            this.chService.TabIndex = 0;
            this.chService.Text = "Service";
            this.chService.UseVisualStyleBackColor = true;
            // 
            // chEntity
            // 
            this.chEntity.AutoSize = true;
            this.chEntity.Checked = true;
            this.chEntity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chEntity.Location = new System.Drawing.Point(19, 19);
            this.chEntity.Name = "chEntity";
            this.chEntity.Size = new System.Drawing.Size(62, 21);
            this.chEntity.TabIndex = 0;
            this.chEntity.Text = "Entity";
            this.chEntity.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(962, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnConfiguracoes);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnSalvarProjeto);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Classes Complementares";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboTabelas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.ComboBox comboTipos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnSalvarProjeto;
        private System.Windows.Forms.Button btnConfiguracoes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chEntity;
        private System.Windows.Forms.CheckBox chViewModel;
        private System.Windows.Forms.CheckBox chController;
        private System.Windows.Forms.CheckBox chCommandHandler;
        private System.Windows.Forms.CheckBox chRepository;
        private System.Windows.Forms.CheckBox chSqlMapping;
        private System.Windows.Forms.CheckBox chCommand;
        private System.Windows.Forms.CheckBox chService;
        private System.Windows.Forms.CheckBox chAutoMapper;
        private System.Windows.Forms.CheckBox chMarcarTodos;
        private System.Windows.Forms.Button btnGerarSalvar;
    }
}

