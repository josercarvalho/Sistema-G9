namespace SistemaG9.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arquivo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Recebedor = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        AnexoBytes = c.Binary(),
                        AnexoExtensao = c.String(maxLength: 150, unicode: false),
                        AnexoTipo = c.String(maxLength: 150, unicode: false),
                        DataEnvio = c.DateTime(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        PerfilUsuarioId = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 150, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 2048, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        DataNascimento = c.DateTime(nullable: false),
                        CPF = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 200, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 150, unicode: false),
                        Numero = c.String(nullable: false, maxLength: 150, unicode: false),
                        Complemento = c.String(maxLength: 150, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 150, unicode: false),
                        CEP = c.String(nullable: false, maxLength: 150, unicode: false),
                        PaisId = c.Int(nullable: false),
                        CidadeId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 150, unicode: false),
                        Operadora = c.String(nullable: false, maxLength: 5, unicode: false),
                        WhatsApp = c.String(maxLength: 150, unicode: false),
                        SKYPE = c.String(maxLength: 150, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                        Status = c.Int(),
                        Nivel_NivelId = c.Int(),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Nivel", t => t.Nivel_NivelId)
                .ForeignKey("dbo.PerfilUsuarios", t => t.PerfilUsuarioId)
                .Index(t => t.PerfilUsuarioId)
                .Index(t => t.Nivel_NivelId);
            
            CreateTable(
                "dbo.DadosBancarios",
                c => new
                    {
                        DadosId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        BancoId = c.Int(nullable: false),
                        Titular = c.String(nullable: false, maxLength: 150, unicode: false),
                        TipoConta = c.Int(nullable: false),
                        Agencia = c.String(nullable: false, maxLength: 25, unicode: false),
                        Conta = c.String(nullable: false, maxLength: 25, unicode: false),
                        Operacao = c.String(maxLength: 25, unicode: false),
                    })
                .PrimaryKey(t => t.DadosId)
                .ForeignKey("dbo.Bancos", t => t.BancoId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId)
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.Bancos",
                c => new
                    {
                        BancoId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.BancoId);
            
            CreateTable(
                "dbo.Doacao",
                c => new
                    {
                        DoacoesId = c.Int(nullable: false, identity: true),
                        MatrizId = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Doador = c.String(maxLength: 150, unicode: false),
                        Recebedor = c.String(maxLength: 150, unicode: false),
                        Tipo = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCadastro = c.DateTime(nullable: false),
                        ExpiraData = c.DateTime(precision: 7, storeType: "datetime2"),
                        ConfirmaDoacao = c.DateTime(precision: 7, storeType: "datetime2"),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoacoesId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Matriz", t => t.MatrizId)
                .Index(t => t.MatrizId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Matriz",
                c => new
                    {
                        MatrizId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 25, unicode: false),
                        Niveis = c.Int(nullable: false),
                        Integrantes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatrizId);
            
            CreateTable(
                "dbo.Nivel",
                c => new
                    {
                        NivelId = c.Int(nullable: false, identity: true),
                        MatrizId = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Inicio = c.Int(nullable: false),
                        Fim = c.Int(nullable: false),
                        Entradas = c.Int(nullable: false),
                        ValorEntrada = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reentradas = c.Int(nullable: false),
                        ValorReentrada = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.NivelId)
                .ForeignKey("dbo.Matriz", t => t.MatrizId)
                .Index(t => t.MatrizId);
            
            CreateTable(
                "dbo.PerfilUsuarios",
                c => new
                    {
                        PerfilUsuarioId = c.Int(nullable: false, identity: true),
                        NomPerfil = c.String(nullable: false, maxLength: 200, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                        FlAdminMaster = c.Boolean(nullable: false),
                        FlAtivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PerfilUsuarioId);
            
            CreateTable(
                "dbo.Rede",
                c => new
                    {
                        RedeId = c.Int(nullable: false, identity: true),
                        MatrizId = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 150, unicode: false),
                        Apelido = c.String(nullable: false, maxLength: 50, unicode: false),
                        RecebedorId = c.Int(nullable: false),
                        ApelidoRecebedor = c.String(maxLength: 50, unicode: false),
                        Posicao = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RedeId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.UpLine",
                c => new
                    {
                        UpLineId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        OrigemId = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UpLineId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        EstadoId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CidadeId)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        PaisId = c.Int(nullable: false),
                        Sigla = c.String(nullable: false, maxLength: 150, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.EstadoId)
                .ForeignKey("dbo.Pais", t => t.PaisId)
                .Index(t => t.PaisId);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        PaisId = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false, maxLength: 2, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.PaisId);
            
            CreateTable(
                "dbo.Entradas",
                c => new
                    {
                        EntradaId = c.Int(nullable: false, identity: true),
                        Apelido = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Amigo = c.Int(nullable: false),
                        Deposito = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntradaId)
                .ForeignKey("dbo.Rede", t => t.Apelido)
                .Index(t => t.Apelido);
            
            CreateTable(
                "dbo.Reentradas",
                c => new
                    {
                        ReentradaId = c.Int(nullable: false, identity: true),
                        Apelido = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Amigo = c.Int(nullable: false),
                        Deposito = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReentradaId)
                .ForeignKey("dbo.Rede", t => t.Apelido)
                .Index(t => t.Apelido);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reentradas", "Apelido", "dbo.Rede");
            DropForeignKey("dbo.Entradas", "Apelido", "dbo.Rede");
            DropForeignKey("dbo.Cidade", "EstadoId", "dbo.Estado");
            DropForeignKey("dbo.Estado", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.Arquivo", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.UpLine", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Rede", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "PerfilUsuarioId", "dbo.PerfilUsuarios");
            DropForeignKey("dbo.Cliente", "Nivel_NivelId", "dbo.Nivel");
            DropForeignKey("dbo.Doacao", "MatrizId", "dbo.Matriz");
            DropForeignKey("dbo.Nivel", "MatrizId", "dbo.Matriz");
            DropForeignKey("dbo.Doacao", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.DadosBancarios", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.DadosBancarios", "BancoId", "dbo.Bancos");
            DropIndex("dbo.Reentradas", new[] { "Apelido" });
            DropIndex("dbo.Entradas", new[] { "Apelido" });
            DropIndex("dbo.Estado", new[] { "PaisId" });
            DropIndex("dbo.Cidade", new[] { "EstadoId" });
            DropIndex("dbo.UpLine", new[] { "ClienteId" });
            DropIndex("dbo.Rede", new[] { "ClienteId" });
            DropIndex("dbo.Nivel", new[] { "MatrizId" });
            DropIndex("dbo.Doacao", new[] { "ClienteId" });
            DropIndex("dbo.Doacao", new[] { "MatrizId" });
            DropIndex("dbo.DadosBancarios", new[] { "BancoId" });
            DropIndex("dbo.DadosBancarios", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "Nivel_NivelId" });
            DropIndex("dbo.Cliente", new[] { "PerfilUsuarioId" });
            DropIndex("dbo.Arquivo", new[] { "ClienteId" });
            DropTable("dbo.Reentradas");
            DropTable("dbo.Entradas");
            DropTable("dbo.Pais");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.UpLine");
            DropTable("dbo.Rede");
            DropTable("dbo.PerfilUsuarios");
            DropTable("dbo.Nivel");
            DropTable("dbo.Matriz");
            DropTable("dbo.Doacao");
            DropTable("dbo.Bancos");
            DropTable("dbo.DadosBancarios");
            DropTable("dbo.Cliente");
            DropTable("dbo.Arquivo");
        }
    }
}
