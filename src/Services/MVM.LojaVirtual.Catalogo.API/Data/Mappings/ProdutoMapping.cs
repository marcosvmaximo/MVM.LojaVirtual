using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVM.LojaVirtual.Catalogo.API.Models;

namespace MVM.LojaVirtual.Catalogo.API.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasColumnType("VARCHAR(500)");

        builder.Property(p => p.Valor)
            .IsRequired()
            .HasColumnType("DECIMAL(10,2)");

        builder.Property(p => p.QuantidadeEstoque)
            .IsRequired()
            .HasColumnType("INT");

        builder.Property(p => p.Ativo)
            .IsRequired()
            .HasColumnType("tinyint(1)");

        builder.Property(p => p.DataCadastro)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(p => p.Imagem)
            .HasColumnType("VARCHAR(250)");
    }
}