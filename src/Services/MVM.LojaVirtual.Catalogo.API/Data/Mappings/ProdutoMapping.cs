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
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasColumnType("varchar(500)");

        builder.Property(p => p.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.QuantidadeEstoque)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(p => p.Ativo)
            .IsRequired()
            .HasColumnType("bool");

        builder.Property(p => p.DataCadatro)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(p => p.Imagem)
            .HasColumnType("varchar(250)");

        
    }
}