using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Books;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    internal static readonly Guid Book1Guid = new Guid("a29b0d03-75bd-4590-a0c6-bec4bcc2908e");
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Author)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasData(GetSampleBookData());
    }

    private IEnumerable<Book> GetSampleBookData()
    {
        var tolkien = "J.R.R. Tolkien";
        yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
    }
}
