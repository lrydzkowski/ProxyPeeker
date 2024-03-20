using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProxyPeeker.Storage;

public class RequestConfiguration : IEntityTypeConfiguration<RequestEntity>
{
    public void Configure(EntityTypeBuilder<RequestEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private void SetTableName(EntityTypeBuilder<RequestEntity> builder)
    {
        builder.ToTable("request");
    }

    private void SetPrimaryKey(EntityTypeBuilder<RequestEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    private void ConfigureColumns(EntityTypeBuilder<RequestEntity> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.SendAt)
            .HasColumnName("send_at")
            .IsRequired();

        builder.Property(x => x.RequestUrl)
            .HasColumnName("request_url");

        builder.Property(x => x.RequestMethod)
            .HasColumnName("request_method");

        builder.Property(x => x.ResponseBody)
            .HasColumnName("request_body");

        builder.Property(x => x.ResponseStatusCode)
            .HasColumnName("response_status_code");

        builder.Property(x => x.ResponseBody)
            .HasColumnName("response_body");
    }
}
